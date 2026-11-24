"""Offline NFC-e intake and explicit BSON mapping for SuiteG6 Movimentacoes."""
from __future__ import annotations

import base64
import hashlib
import json
import re
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from xml.etree import ElementTree as ET

MONGO_DOCUMENT_BYTES = 16 * 1024 * 1024
COLLECTION = 'Movimentacoes'
NS = {'n': 'http://www.portalfiscal.inf.br/nfe'}
NOTICE = ('Inserção direta de venda histórica em Movimentacoes. Não executa os serviços de '
          'estoque, caixa, financeiro, numeração ou sincronização do SuiteG6. '
          'Compatibilidade analisada estaticamente; sem validação em instalação nativa. '
          'Assinatura digital e situação atual na SEFAZ não verificadas.')


def digits(value):
    return re.sub(r'\D', '', value)


def key_valid(key):
    if not re.fullmatch(r'\d{44}', key):
        return False
    total = sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(key[:43])))
    check = 11 - total % 11
    return int(key[-1]) == (0 if check >= 10 else check)


def parse_xml(raw: bytes, *, issuer: str, environment: str = '1') -> dict:
    """Strict offline structural checks; retain bytes, decimals and original timezone."""
    if not raw:
        raise ValueError('XML vazio.')
    # Restrict encodings before parsing so UTF-16 cannot bypass the DTD check.
    try:
        text = raw.decode('utf-8-sig')
    except UnicodeDecodeError:
        raise ValueError('Use XML em UTF-8.') from None
    if re.search(r'<!\s*(DOCTYPE|ENTITY)', text, re.I):
        raise ValueError('DTD e entidades não são permitidos.')
    if environment not in {'1', '2'}:
        raise ValueError('Ambiente deve ser 1 (produção) ou 2 (homologação).')
    issuer = digits(issuer)
    if len(issuer) != 14:
        raise ValueError('Informe o CNPJ do emitente com 14 dígitos.')
    try:
        root = ET.fromstring(text)
    except ET.ParseError:
        raise ValueError('XML inválido.') from None
    if root.tag != '{' + NS['n'] + '}nfeProc':
        raise ValueError('Informe nfeProc completo com protocolo de autorização.')
    infos = root.findall('n:NFe/n:infNFe', NS)
    protocols = root.findall('n:protNFe/n:infProt', NS)
    if len(infos) != 1 or len(protocols) != 1:
        raise ValueError('Esperado exatamente uma NFC-e e um protocolo.')
    info, protocol = infos[0], protocols[0]

    def get(node, path, required=True):
        value = node.findtext('/'.join('n:' + part for part in path.split('/')), namespaces=NS)
        value = (value or '').strip()
        if required and not value:
            raise ValueError('Campo obrigatório ausente: ' + path)
        return value

    def amount(node, path, required=True):
        value = get(node, path, required)
        if not value and not required:
            return Decimal('0')
        if not re.fullmatch(r'\d{1,15}(?:\.\d{1,10})?', value):
            raise ValueError('Valor decimal inválido: ' + path)
        try:
            return Decimal(value)
        except InvalidOperation:
            raise ValueError('Valor inválido: ' + path) from None

    key = info.get('Id', '')
    if not key.startswith('NFe') or not key_valid(key[3:]):
        raise ValueError('Chave de acesso inválida (incluindo dígito verificador).')
    key = key[3:]
    cnpj = get(info, 'emit/CNPJ')
    model = get(info, 'ide/mod')
    env = get(info, 'ide/tpAmb')
    number, series = get(info, 'ide/nNF'), get(info, 'ide/serie')
    if not number.isdigit() or not series.isdigit():
        raise ValueError('Número ou série inválidos.')
    if model not in {'55', '65'} or key[20:22] != model:
        raise ValueError('Aceitos NF-e modelo 55 e NFC-e modelo 65; modelo deve conferir com a chave.')
    if cnpj != issuer or key[6:20] != issuer:
        raise ValueError('CNPJ do XML/chave difere do emitente selecionado.')
    if int(series) != int(key[22:25]) or int(number) != int(key[25:34]):
        raise ValueError('Número/série do XML não conferem com a chave.')
    if env != environment or get(protocol, 'tpAmb') != environment:
        raise ValueError('Ambiente do XML/protocolo difere do selecionado.')
    if get(protocol, 'chNFe') != key or get(protocol, 'cStat') not in {'100', '150'}:
        raise ValueError('Protocolo não autoriza esta chave de acesso.')
    issued_text = get(info, 'ide/dhEmi')
    try:
        issued = datetime.fromisoformat(issued_text.replace('Z', '+00:00'))
        if issued.tzinfo is None:
            raise ValueError()
    except ValueError:
        raise ValueError('dhEmi deve incluir data, hora e fuso válidos.') from None
    if key[2:6] != issued.strftime('%y%m'):
        raise ValueError('Mês/ano de emissão não confere com a chave.')
    items = []
    sequences = set()
    for det in info.findall('n:det', NS):
        seq = det.get('nItem', '')
        if not seq.isdigit() or int(seq) < 1 or int(seq) in sequences:
            raise ValueError('Sequência de item inválida ou duplicada.')
        sequences.add(int(seq))
        quantity = amount(det, 'prod/qCom')
        price = amount(det, 'prod/vUnCom')
        product_total = amount(det, 'prod/vProd')
        if quantity <= 0:
            raise ValueError('Quantidade do item deve ser positiva.')
        included = get(det, 'prod/indTot')
        if included not in {'0', '1'}:
            raise ValueError('indTot inválido.')
        items.append({'sequence': int(seq), 'code': get(det, 'prod/cProd'),
                      'description': get(det, 'prod/xProd'), 'unit': get(det, 'prod/uCom'),
                      'quantity': str(quantity), 'unit_price': str(price),
                      'product_total': str(product_total), 'included_in_total': included == '1',
                      'discount': str(amount(det, 'prod/vDesc', False)),
                      'cfop': get(det, 'prod/CFOP'), 'ncm': get(det, 'prod/NCM')})
    if not items or len(items) > 990:
        raise ValueError('NFC-e deve conter entre 1 e 990 itens.')
    product_total = amount(info, 'total/ICMSTot/vProd')
    if sum((Decimal(i['product_total']) for i in items if i['included_in_total']), Decimal(0)) != product_total:
        raise ValueError('Soma dos produtos diverge de total/ICMSTot/vProd.')
    total = amount(info, 'total/ICMSTot/vNF')
    warnings = [NOTICE, 'Protocolo aceito conforme o XML; cancelamentos posteriores exigem consulta/eventos separados.']
    return {'_id': environment + ':' + key, 'schema': 'insight.nfce.archive.v1',
            'access_key': key, 'issuer_cnpj': issuer, 'environment': environment, 'model': model,
            'number': number, 'series': series, 'issued_at': issued.astimezone(timezone.utc),
            'issued_at_original': issued_text, 'issuer_name': get(info, 'emit/xNome'),
            'authorization_protocol': get(protocol, 'nProt'), 'protocol_status': get(protocol, 'cStat'),
            'total': str(total), 'product_total': str(product_total), 'items': items,
            'xml_sha256': hashlib.sha256(raw).hexdigest(),
            'xml_base64': base64.b64encode(raw).decode('ascii'), 'warnings': warnings,
            'native_movement_created': False}


def export_json(documents):
    from bson import json_util
    return json_util.dumps(documents, json_options=json_util.CANONICAL_JSON_OPTIONS, ensure_ascii=False, indent=2)

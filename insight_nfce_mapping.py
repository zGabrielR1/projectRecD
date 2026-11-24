"""Declarative native BSON profiles. No eval, scripting or implicit reference guessing."""
from __future__ import annotations

import base64
import hashlib
import re
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation, ROUND_HALF_UP
from xml.etree import ElementTree as ET

from bson import BSON, Int64, ObjectId, json_util
from insight_nfce import NS, NOTICE, MONGO_DOCUMENT_BYTES, parse_xml

def load_profile(text):
    try:
        profile = json_util.loads(text)
    except Exception:
        raise ValueError('Perfil BSON/JSON inválido.') from None
    if not isinstance(profile, dict) or profile.get('schema') != 'insight.nfce.mapping.v1':
        raise ValueError('Perfil deve usar schema insight.nfce.mapping.v1.')
    for key in ('database', 'issuer_cnpj', 'company_id', 'movement', 'item', 'products', 'payments'):
        if not profile.get(key):
            raise ValueError('Perfil incompleto: ' + key)
    if not isinstance(profile['company_id'], ObjectId):
        raise ValueError('company_id deve ser ObjectId em Extended JSON.')
    if profile['company_id'] == ObjectId('000000000000000000000000'):
        raise ValueError('Substitua o company_id de exemplo.')
    if not re.fullmatch(r'\d{14}', profile['issuer_cnpj']):
        raise ValueError('issuer_cnpj deve ter 14 dígitos.')
    if not isinstance(profile['products'], dict) or not isinstance(profile['payments'], dict):
        raise ValueError('products e payments devem ser objetos de mapeamento.')
    if profile.get('environment') not in {'1', '2'}:
        raise ValueError('environment deve ser 1 ou 2.')
    return profile


def path_get(context, path):
    value = context
    for key in path.split('.'):
        if not isinstance(value, dict) or key not in value:
            raise ValueError('Referência interna não mapeada: ' + path)
        value = value[key]
    if value is None:
        raise ValueError('Referência interna vazia: ' + path)
    return value


def render(template, context, node, depth=0):
    if depth > 25:
        raise ValueError('Perfil excede 25 níveis.')
    if isinstance(template, list):
        return [render(v, context, node, depth + 1) for v in template]
    if not isinstance(template, dict):
        return template
    if '$bind' in template:
        source = template['$bind']
        if not isinstance(source, str):
            raise ValueError('$bind deve ser texto.')
        if source.startswith('xml:'):
            path = source[4:]
            if not re.fullmatch(r'[A-Za-z0-9_/*]+', path):
                raise ValueError('Caminho XML inválido no perfil.')
            path = '/'.join('*' if p == '*' else 'n:' + p for p in path.split('/'))
            found = node.findall(path, NS)
            if len(found) > 1:
                raise ValueError('Caminho XML ambíguo no perfil: ' + source)
            if not found or found[0].text is None:
                if 'default' not in template:
                    raise ValueError('Campo XML não encontrado: ' + source)
                value = template['default']
            else:
                value = found[0].text.strip()
        else:
            value = path_get(context, source)
        kind = template.get('type', 'value')
        try:
            if kind == 'value': return value
            if kind == 'string': return str(value)
            if kind == 'objectId': return ObjectId(value)
            if kind == 'int64': return Int64(value)
            if kind == 'int': return int(value)
            if kind == 'double':
                decimal = Decimal(str(value))
                if not decimal.is_finite() or abs(decimal) > Decimal('1e15'):
                    raise ValueError()
                return float(decimal)
        except Exception:
            raise ValueError('Tipo inválido para ' + source + ': ' + kind) from None
        raise ValueError('Tipo de binding desconhecido: ' + kind)
    result = {}
    for key, value in template.items():
        if key.startswith('$') or '.' in key or '\x00' in key:
            raise ValueError('Campo BSON inválido no perfil: ' + key)
        result[key] = render(value, context, node, depth + 1)
    return result


def discriminator(value, expected):
    actual = value.get('_t') if isinstance(value, dict) else None
    return actual == expected or isinstance(actual, list) and actual and actual[-1] == expected


def fiscal_coverage(template, context):
    """Identify tax leaf bindings, including templates obtained via explicit profile objects."""
    paths = set()
    if isinstance(template, dict):
        source = template.get('$bind')
        if isinstance(source, str) and source.startswith('xml:'):
            paths.add(source[4:])
        for value in template.values():
            paths.update(fiscal_coverage(value, context))
    elif isinstance(template, list):
        for value in template: paths.update(fiscal_coverage(value, context))
    return paths


def tax_values(node, path='imposto'):
    for child in node:
        child_path = path + '/' + child.tag.split('}')[-1]
        if len(child):
            yield from tax_values(child, child_path)
        elif child_path.split('/')[-1].startswith(('v', 'p', 'q')) and child.text:
            try: nonzero = Decimal(child.text) != 0
            except InvalidOperation: raise ValueError('Valor fiscal inválido: ' + child_path) from None
            if nonzero: yield child_path


def matches_path(pattern, path):
    left, right = pattern.split('/'), path.split('/')
    return len(left) == len(right) and all(a == '*' or a == b for a, b in zip(left, right))


def build_sale(raw, profile):
    parsed = parse_xml(raw, issuer=profile['issuer_cnpj'], environment=profile['environment'])
    root = ET.fromstring(raw.decode('utf-8-sig'))
    info = root.find('n:NFe/n:infNFe', NS)
    # Native DateTimeSerializerEx relabels wall clock as UTC; do not shift dhEmi by 3h.
    wall = datetime.fromisoformat(parsed['issued_at_original'].replace('Z', '+00:00')).replace(tzinfo=timezone.utc)
    key = parsed['access_key']
    identity = profile['environment'] + ':' + key
    native_id = ObjectId(hashlib.sha256(('insight-nfce:' + identity).encode()).digest()[:12])
    context = {'profile': profile, 'invoice': parsed, 'id': native_id, 'issued_at': wall,
               'number': Int64(parsed['number']), 'series': parsed['series'], 'items': [], 'payments': []}
    for det, item in zip(info.findall('n:det', NS), parsed['items']):
        product = profile['products'].get(item['code'])
        if not isinstance(product, dict):
            raise ValueError('Produto sem mapeamento interno: ' + item['code'])
        child = dict(context, product=product, item=item)
        if not item['included_in_total']:
            raise ValueError('Itens indTot=0 exigem outro perfil de integração; não são suportados nesta importação.')
        coverage = fiscal_coverage(profile['item'], child)
        taxes = det.find('n:imposto', NS)
        if taxes is not None:
            for path in tax_values(taxes):
                if not any(matches_path(pattern, path) for pattern in coverage):
                    raise ValueError('Valor fiscal do XML sem binding explícito: ' + path)
        context['items'].append(render(profile['item'], child, det))
    for payment in info.findall('n:pag/n:detPag', NS):
        code = payment.findtext('n:tPag', namespaces=NS)
        template = profile['payments'].get(code)
        if template is None:
            raise ValueError('Pagamento sem mapeamento interno: ' + str(code))
        context['payments'].append(render(template, context, payment))
    if not context['payments']:
        raise ValueError('XML sem detPag; não é possível reconstruir pagamentos.')
    # XML's paid total less change must agree before BSON payment mapping is accepted.
    try:
        paid = sum((Decimal(p.findtext('n:vPag', default='0', namespaces=NS)) for p in info.findall('n:pag/n:detPag', NS)), Decimal(0))
        change = Decimal(info.findtext('n:pag/n:vTroco', default='0', namespaces=NS))
    except InvalidOperation:
        raise ValueError('Valor de pagamento/troco inválido.') from None
    if not paid.is_finite() or not change.is_finite() or paid - change != Decimal(parsed['total']):
        raise ValueError('Soma dos pagamentos menos troco diverge de vNF.')
    try:
        native_paid = sum((Decimal(str(p['Valor'])) for p in context['payments']), Decimal(0))
    except (KeyError, TypeError, InvalidOperation):
        raise ValueError('Cada pagamento nativo deve fornecer Valor numérico.') from None
    if not native_paid.is_finite() or native_paid != Decimal(parsed['total']):
        raise ValueError('Soma de Valor nos pagamentos nativos deve ser vNF líquido de troco.')
    document = render(profile['movement'], context, info)
    expected_type = 'NotaFiscalConsumidorEletronica' if parsed['model'] == '65' else 'NotaFiscalEletronicaSaida'
    if not discriminator(document, expected_type):
        raise ValueError('movement._t deve identificar ' + expected_type)
    if info.findtext('n:ide/n:tpNF', default='1', namespaces=NS) != '1' or info.findtext('n:ide/n:finNFe', default='1', namespaces=NS) != '1':
        raise ValueError('Este conversor exige saída com finalidade normal.')
    # Bind identity ourselves; a profile cannot redirect the operation to an existing sale.
    document.update(_id=native_id, ChaveAcesso=key, Numero=Int64(parsed['number']),
                    Serie=parsed['series'], EmpresaReferencia=profile['company_id'],
                    DataHoraEmissao=wall, ItensBase=context['items'], ItensPagamentos=context['payments'])
    for field in ('Empresa', 'Pessoa', 'Situacao', 'FinalidadeEmissaoNfe', 'NaturezaOperacao'):
        if not isinstance(document.get(field), dict) or not document[field].get('_t'):
            raise ValueError('Perfil deve preencher o objeto nativo ' + field)
    if document['Empresa'].get('PessoaReferencia') != profile['company_id']:
        raise ValueError('Empresa.PessoaReferencia deve coincidir com company_id.')
    if document.get('MovimentacaoItemFinanceiro') is not False or document.get('Ativo') is not True:
        raise ValueError('Perfil deve definir Ativo=true e MovimentacaoItemFinanceiro=false.')
    if not discriminator(document['Situacao'], 'Concluido') or document['Situacao'].get('Codigo') != 2:
        raise ValueError('Importação histórica exige situação nativa Concluido, código 2.')
    total = Decimal(0)
    for item in context['items']:
        for field in ('ProdutoServicoEmpresaReferencia', 'ProdutoServico', 'Empresa', 'Destinatario', 'Percentual', 'IPI', 'PIS', 'COFINS', 'OperacaoFiscal', 'EstoqueMovimentacao'):
            if not item.get(field):
                raise ValueError('Item nativo incompleto: ' + field)
        if not isinstance(item['ProdutoServicoEmpresaReferencia'], ObjectId):
            raise ValueError('ProdutoServicoEmpresaReferencia deve ser ObjectId.')
        if item['ProdutoServicoEmpresaReferencia'] == ObjectId('000000000000000000000000'):
            raise ValueError('Substitua a referência de produto de exemplo.')
        gross = (Decimal(str(item.get('Quantidade', 0))) * Decimal(str(item.get('PrecoUnitario', 0)))).quantize(Decimal('.01'), rounding=ROUND_HALF_UP)
        net = gross + sum((Decimal(str(item.get(f, 0))) for f in ('OutrasDespesasProporcional', 'OutrasDespesasDigitado', 'Frete', 'Seguro')), Decimal(0))
        net -= Decimal(str(item.get('DescontoDigitado', 0))) + Decimal(str(item.get('DescontoProporcional', 0)))
        net += Decimal(str(item['Percentual'].get('ValorSt', 0))) + Decimal(str(item['IPI'].get('Valor', 0)))
        total += net
    if total != Decimal(parsed['total']):
        raise ValueError('Total calculado pelos itens nativos diverge de vNF: ' + str(total))
    xml = {'_id': ObjectId(hashlib.sha256(('insight-nfce-xml:' + identity).encode()).digest()[:12]),
           'Data': wall, 'ChaveAcesso': key, 'Xml': raw.decode('utf-8-sig'),
           'TipoMovimentacao': 11 if parsed['model'] == '65' else profile.get('xml_movement_type'), 'EventoCancelamento': False, 'Cancelada': False}
    if type(xml['TipoMovimentacao']) is not int:
        raise ValueError('Perfil NF-e exige xml_movement_type nativo inteiro revisado.')
    validate_native_documents(document, xml)
    return {'movement': document, 'xml': xml, 'summary': {k: parsed[k] for k in
            ('access_key', 'number', 'series', 'total', 'issued_at_original', 'xml_sha256')}, 'notice': NOTICE}


def validate_native_documents(*documents):
    for document in documents:
        if len(BSON.encode(document)) > MONGO_DOCUMENT_BYTES:
            raise ValueError('O documento BSON excede o limite nativo de 16 MiB do MongoDB.')

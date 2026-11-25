"""Declarative native BSON profiles. No eval, scripting or implicit reference guessing."""
from __future__ import annotations

import base64
import hashlib
import re
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation, ROUND_HALF_UP
from xml.etree import ElementTree as ET

from bson import BSON, Decimal128, Int64, ObjectId, json_util
from insight_nfce import NS, NOTICE, MONGO_DOCUMENT_BYTES, digits, parse_xml

def load_profile(text):
    try:
        profile = json_util.loads(text)
    except Exception:
        raise ValueError('Perfil BSON/JSON inválido.') from None
    if not isinstance(profile, dict) or profile.get('schema') != 'insight.nfce.mapping.v1':
        raise ValueError('Perfil deve usar schema insight.nfce.mapping.v1.')
    for key in ('database', 'issuer_cnpj', 'company_id', 'movement', 'item', 'products', 'payments'):
        if key == 'products':
            if not isinstance(profile.get(key), dict):
                raise ValueError('Perfil incompleto: ' + key)
        elif not profile.get(key):
            raise ValueError('Perfil incompleto: ' + key)
    if not isinstance(profile['company_id'], ObjectId):
        raise ValueError('company_id deve ser ObjectId em Extended JSON.')
    if profile['company_id'] == ObjectId('000000000000000000000000'):
        raise ValueError('Substitua o company_id de exemplo.')
    if not re.fullmatch(r'\d{14}', profile['issuer_cnpj']):
        raise ValueError('issuer_cnpj deve ter 14 dígitos.')
    if not isinstance(profile['products'], dict) or not isinstance(profile['payments'], dict):
        raise ValueError('products e payments devem ser objetos de mapeamento.')
    if not profile['products'] and not isinstance(profile.get('product_template'), dict):
        raise ValueError('Perfil deve mapear products ou declarar product_template.')
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
            if node.tag.startswith('{'+NS['n']+'}'):
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


def generated_id(kind, company_id, code, extra=''):
    return ObjectId(hashlib.sha256(
        ('insight-dfe-' + kind + ':' + str(company_id) + ':' + code + extra).encode()).digest()[:12])


REGISTRATION_COLLECTIONS = ('Estoques', 'EstoquesQuantidade', 'ProdutosServicos',
                            'Precos', 'ProdutosServicosEmpresa')


def product_registration(profile, item, context, node, *, identifier=None, use_barcode=False):
    """Build product/link/price/stock documents from an explicit profile template.

    The template supplies every native field; this function only binds the
    generated identities so a purchase line can reference the new company
    product without guessing defaults from another product.
    """
    template = profile.get('product_template')
    if not isinstance(template, dict):
        raise ValueError('Produto sem mapeamento interno: ' + item['code'])
    code = identifier or item['code']
    company_id = profile['company_id']
    generated = {
        'product_id': generated_id('product', company_id, code),
        'company_product_id': generated_id('product-link', company_id, code),
        'price_id': generated_id('price', company_id, code),
        'stock_id': generated_id('stock', company_id, code),
        'stock_quantity_id': generated_id('stock-quantity', company_id, code),
    }
    child = dict(context, generated=generated, code=code)
    document_templates = {}
    for key, collection in (('product', 'ProdutosServicos'), ('company', 'ProdutosServicosEmpresa'),
                            ('price', 'Precos'), ('stock', 'Estoques'),
                            ('stock_quantity', 'EstoquesQuantidade')):
        value = template.get(key)
        if value is None:
            continue
        if not isinstance(value, dict):
            raise ValueError('product_template.' + key + ' deve ser um objeto.')
        document_templates[collection] = render(value, child, node)
    for collection in ('ProdutosServicos', 'ProdutosServicosEmpresa', 'Precos'):
        if collection not in document_templates:
            raise ValueError('product_template incompleto: ' + collection)
    mapping_template = template.get('mapping')
    if not isinstance(mapping_template, dict):
        raise ValueError('product_template.mapping deve descrever o item nativo.')
    mapping = render(mapping_template, child, node)
    mapping['reference'] = generated['company_product_id']
    for field in ('snapshot', 'percentual', 'ipi', 'pis', 'cofins', 'operacao_fiscal',
                  'estoque_movimentacao'):
        if not isinstance(mapping.get(field), dict) or not mapping[field].get('_t'):
            raise ValueError('product_template.mapping incompleto: ' + field)
    snapshot_reference = mapping['snapshot'].get('ProdutoServicoReferencia')
    if snapshot_reference is not None and snapshot_reference != generated['product_id']:
        raise ValueError('product_template.mapping.snapshot deve usar generated.product_id.')

    documents = document_templates
    product = documents['ProdutosServicos']
    product['_id'] = generated['product_id']
    if use_barcode:
        bind_product_barcode(product, code)
    if not product.get('_t') or product.get('Ativo') is not True:
        raise ValueError('product_template.product deve ser um cadastro ativo e tipado.')
    link = documents['ProdutosServicosEmpresa']
    link['_id'] = generated['company_product_id']
    link['ProdutoServicoReferencia'] = generated['product_id']
    link['EmpresaReferencia'] = company_id
    link['PrecoReferencia'] = generated['price_id']
    if not link.get('_t') or link.get('Ativo') is not True:
        raise ValueError('product_template.company deve ser um vínculo ativo e tipado.')
    price = documents['Precos']
    price['_id'] = generated['price_id']
    if not price.get('_t') or not isinstance(price.get('Custo'), dict) or 'Valor' not in price['Custo']:
        raise ValueError('product_template.price deve conter _t e Custo.Valor.')
    if 'Estoques' in documents:
        stock = documents['Estoques']
        stock['_id'] = generated['stock_id']
        if not stock.get('_t'):
            raise ValueError('product_template.stock deve conter _t.')
        link['EstoqueReferencia'] = generated['stock_id']
    if 'EstoquesQuantidade' in documents:
        quantity = documents['EstoquesQuantidade']
        if 'Estoques' not in documents:
            raise ValueError('product_template.stock_quantity exige product_template.stock.')
        quantity['_id'] = generated['stock_quantity_id']
        quantity['EstoqueReferencia'] = generated['stock_id']
        quantity.setdefault('EstoqueReferenciaParent', generated['stock_id'])
        quantity.setdefault('Quantidade', 0.0)
        if not quantity.get('_t'):
            raise ValueError('product_template.stock_quantity deve conter _t.')
    validate_native_documents(*documents.values())
    return mapping, {'code': code, 'documents': documents}


def finance_kind(document):
    native_type = document.get('_t')
    if isinstance(native_type, list):
        native_type = native_type[-1] if native_type else None
    return 'payable' if native_type in {'NotaFiscalCompra', 'NotaFiscalEletronicaEntrada'} else 'receivable'


def build_finance(parsed, profile, context, document, node, *, required=False):
    """Build native parcela documents from XML duplicatas using a profile template.

    The XML proves due date and amount only. The template supplies the native
    account/plan/species; no settlement is invented and no reference is copied
    from another sale.
    """
    if not parsed['installments']:
        return None
    config = profile.get('finance')
    kind = finance_kind(document)
    if not isinstance(config, dict):
        if required:
            raise ValueError('O perfil precisa definir finance.' + kind + ' para importar as duplicatas deste XML.')
        return None
    side = config.get(kind)
    if side is None:
        if required:
            raise ValueError('O perfil precisa definir finance.' + kind + ' para importar as duplicatas deste XML.')
        return None
    expected = {'payable': ('Pagamentos', 'Pagamento'),
                'receivable': ('Recebimentos', 'Recebimento')}[kind]
    if not isinstance(side, dict):
        raise ValueError('finance.' + kind + ' deve ser um objeto.')
    collection, payment_type = side.get('collection'), side.get('type')
    if collection != expected[0] or payment_type != expected[1]:
        raise ValueError('finance.' + kind + ' deve usar ' + expected[0] + '/' + expected[1] + '.')
    template = side.get('parcela')
    if not isinstance(template, dict):
        raise ValueError('finance.' + kind + '.parcela deve ser um objeto.')
    person_template = side.get('person')
    if person_template is not None and not isinstance(person_template, dict):
        raise ValueError('finance.' + kind + '.person deve ser um objeto.')
    identity = profile['environment'] + ':' + parsed['access_key']
    person_reference = document['Pessoa'].get('PessoaReferencia')
    if not isinstance(person_reference, ObjectId) or person_reference == ObjectId('000000000000000000000000'):
        raise ValueError('Movimentação sem pessoa para vincular as parcelas financeiras.')
    documents = []
    for order, installment in enumerate(parsed['installments'], 1):
        if not installment['date']:
            raise ValueError('Duplicata sem vencimento exige revisão antes de gerar o título financeiro.')
        if not installment['amount']:
            raise ValueError('Duplicata sem valor exige revisão antes de gerar o título financeiro.')
        child = dict(context, installment=dict(installment, order=order))
        parcela = render(template, child, node)
        if not isinstance(parcela, dict) or not parcela.get('_t'):
            raise ValueError('finance.' + kind + '.parcela deve produzir um documento tipado.')
        parcela['_id'] = ObjectId(hashlib.sha256(
            ('insight-nfce-title:' + identity + ':' + str(order)).encode()).digest()[:12])
        parcela['Ordem'] = order
        if installment['number']:
            parcela['Documento'] = installment['number']
        parcela['Vencimento'] = datetime.strptime(installment['date'], '%Y-%m-%d').replace(tzinfo=timezone.utc)
        parcela['DataEmissao'] = context['issued_at']
        parcela['EmpresaReferencia'] = profile['company_id']
        parcela['PessoaReferencia'] = person_reference
        parcela['Ativo'] = True
        if person_template is not None:
            person = render(person_template, child, node)
            if not isinstance(person, dict) or not person.get('_t'):
                raise ValueError('finance.' + kind + '.person deve produzir um cadastro tipado.')
            parcela['Pessoa'] = person
        status = parcela.get('Situacao')
        if not isinstance(status, dict) or status.get('_t') != 'Pendente' or status.get('Codigo') != 1:
            raise ValueError('Título financeiro importado deve iniciar Pendente, código 1.')
        account = parcela.get('ContaReferencia')
        if not isinstance(account, ObjectId) or account == ObjectId('000000000000000000000000'):
            raise ValueError('finance.' + kind + '.parcela deve informar ContaReferencia.')
        history = parcela.get('Historico')
        if (not isinstance(history, list) or len(history) != 1
                or not discriminator(history[0], 'HistoricoAguardando')):
            raise ValueError('Título financeiro exige apenas o histórico inicial HistoricoAguardando.')
        amount = Decimal(installment['amount'])
        for entry in history:
            entry.update(EmpresaReferencia=profile['company_id'], ContaReferencia=account)
            entry.setdefault('Data', context['issued_at'])
            try:
                value = Decimal(str(entry.get('Valor')))
            except (InvalidOperation, TypeError):
                raise ValueError('Histórico financeiro sem Valor numérico.') from None
            if not value.is_finite() or value != amount:
                raise ValueError('Histórico financeiro deve registrar o valor da duplicata.')
        validate_native_documents(parcela)
        documents.append(parcela)
    return {'kind': kind, 'collection': collection, 'type': payment_type, 'documents': documents}


def sale_documents(sale):
    """Yield (collection, document) in dependency order for export and writes."""
    for registration in sale.get('product_registrations', []):
        for collection in REGISTRATION_COLLECTIONS:
            document = registration['documents'].get(collection)
            if document is not None:
                yield collection, document
    yield 'XmlMovimentacoes', sale['xml']
    yield 'Movimentacoes', sale['movement']
    finance = sale.get('finance')
    if finance:
        for document in finance['documents']:
            yield finance['collection'], document


def sale_collections(sales):
    names = {'Movimentacoes', 'XmlMovimentacoes'}
    for sale in sales:
        for registration in sale.get('product_registrations', []):
            names.update(registration['documents'])
        if sale.get('finance'):
            names.add(sale['finance']['collection'])
    return names


def product_identifier(line, use_barcode):
    if not use_barcode:
        return line['code']
    barcode = line.get('barcode', '').strip()
    if not re.fullmatch(r'[0-9]{8}|[0-9]{12,14}', barcode) or not barcode.strip('0'):
        raise ValueError('Item sem código de barras comercial utilizável: ' + line['code'])
    return barcode


def bind_product_barcode(product, barcode):
    configured = product.get('CodigoBarras')
    if configured not in (None, '', barcode):
        raise ValueError('Código de barras do cadastro diverge do XML: ' + barcode)
    product['CodigoBarras'] = barcode


def profile_for_source(profile, source):
    source_profiles = profile.get('source_profiles', {})
    if not isinstance(source_profiles, dict):
        raise ValueError('source_profiles deve ser um objeto de mapeamento.')
    if source in source_profiles:
        overrides = source_profiles[source]
        allowed = {'issuer_cnpj', 'movement', 'item', 'products', 'payments',
                   'company', 'customer', 'xml_movement_type', 'purchase_suppliers',
                   'product_template', 'product_catalog', 'person_template', 'finance'}
        if source == 'cfe':
            allowed.add('cfe')
        if not isinstance(overrides, dict) or set(overrides) - allowed:
            raise ValueError('Configuração inválida no perfil da pasta de XML.')
        return dict(profile, **overrides)
    return profile


def native_issue_time(value):
    # SuiteG6 labels the fiscal wall clock as UTC. BSON stores milliseconds.
    wall = datetime.fromisoformat(value.replace('Z', '+00:00'))
    return wall.replace(tzinfo=timezone.utc, microsecond=wall.microsecond // 1000 * 1000)


def build_sale(raw, profile, *, source=None, include_finance=True, include_products=True, include_people=True,
               use_barcode=False, require_finance=False, catalog_only=False):
    profile = profile_for_source(profile, source)
    if source == 'purchase' and 'purchase_suppliers' in profile:
        from insight_nfce_discovery import read_xml

        issuer = read_xml(raw).findtext('n:NFe/n:infNFe/n:emit/n:CNPJ', namespaces=NS)
        suppliers = profile['purchase_suppliers']
        supplier = suppliers.get(issuer) if isinstance(suppliers, dict) else None
        if (not issuer or not re.fullmatch(r'[0-9]{14}', issuer)
                or not isinstance(supplier, dict)
                or not isinstance(supplier.get('customer'), dict)
                or not isinstance(supplier.get('products'), dict)):
            raise ValueError('Fornecedor ou produtos sem mapeamento no perfil de compra.')
        profile = dict(profile, issuer_cnpj=issuer, customer=supplier['customer'],
                       products=supplier['products'])
    parsed = parse_xml(raw, issuer=profile['issuer_cnpj'], environment=profile['environment'])
    root = ET.fromstring(raw.decode('utf-8-sig'))
    info = root.find('n:NFe/n:infNFe', NS)
    wall = native_issue_time(parsed['issued_at_original'])
    key = parsed['access_key']
    identity = profile['environment'] + ':' + key
    native_id = ObjectId(hashlib.sha256(('insight-nfce:' + identity).encode()).digest()[:12])
    context = {'profile': profile, 'invoice': parsed, 'id': native_id, 'issued_at': wall,
               'number': Int64(parsed['number']), 'series': parsed['series'], 'items': [], 'payments': []}
    if catalog_only:
        from insight_nfce_discovery import nfe_route

        nfe_route(parsed, source)
        return build_catalog_review(profile, context, info, source, info.findall('n:det', NS),
                                    include_people, include_products, use_barcode, profile['item'])
    registrations = {}
    for det, item in zip(info.findall('n:det', NS), parsed['items']):
        identifier = product_identifier(item, use_barcode)
        product = profile['products'].get(identifier)
        if not isinstance(product, dict):
            if not include_products:
                raise ValueError('Produto sem vínculo. Selecione Produtos ou mapeie um cadastro existente: ' + item['code'])
            product, registration = product_registration(profile, item, context, det, identifier=identifier,
                                                         use_barcode=use_barcode)
            if registrations.setdefault(identifier, registration) != registration:
                raise ValueError('Linhas do mesmo produto geram cadastros diferentes: ' + identifier)
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
    payment_nodes = info.findall('n:pag/n:detPag', NS)
    payment_codes = [node.findtext('n:tPag', namespaces=NS) for node in payment_nodes]
    if '90' in payment_codes:
        if (parsed['model'] != '55' or payment_codes != ['90']
                or payment_nodes[0].findtext('n:vPag', default='0', namespaces=NS) not in {'0', '0.0', '0.00'}
                or info.findtext('n:pag/n:vTroco', default='0', namespaces=NS) not in {'0', '0.0', '0.00'}):
            raise ValueError('NF-e sem pagamento exige tPag=90, valor zero e ausência de troco.')
    elif payment_nodes or source != 'purchase':
        for payment in payment_nodes:
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
    if parsed['model'] == '55' and parsed['direction'] == '0':
        expected_type = 'NotaFiscalEletronicaEntrada'
    if source == 'purchase':
        from insight_nfce_discovery import nfe_route
        expected_type, company_cnpj, supplier_cnpj = nfe_route(parsed, source)
        company, supplier = document.get('Empresa'), document.get('Pessoa')
        if (not isinstance(company, dict) or not isinstance(supplier, dict)
                or digits(str(company.get('Documento', ''))) != company_cnpj
                or digits(str(supplier.get('Documento', ''))) != supplier_cnpj
                or not isinstance(supplier.get('PessoaReferencia'), ObjectId)
                or supplier['PessoaReferencia'] in (ObjectId('000000000000000000000000'), profile['company_id'])):
            raise ValueError('Para nota de compra, Empresa deve identificar o destinatário e Pessoa o fornecedor.')
    if not discriminator(document, expected_type):
        raise ValueError('movement._t deve identificar ' + expected_type)
    if (parsed['direction'] not in {'0', '1'} or (parsed['model'] == '65' and parsed['direction'] != '1')
            or info.findtext('n:ide/n:finNFe', default='1', namespaces=NS) != '1'):
        raise ValueError('Este conversor exige finalidade normal e direção compatível com o modelo.')
    # Bind identity ourselves; a profile cannot redirect the operation to an existing sale.
    document.update(_id=native_id, ChaveAcesso=key, Numero=Int64(parsed['number']),
                    Serie=parsed['series'], EmpresaReferencia=profile['company_id'],
                    DataHoraEmissao=wall, ItensBase=context['items'])
    if expected_type == 'NotaFiscalCompra':
        document.pop('ItensPagamentos', None)
    else:
        document['ItensPagamentos'] = context['payments']
    for field in ('Empresa', 'Pessoa', 'Situacao', 'FinalidadeEmissaoNfe', 'NaturezaOperacao'):
        if not isinstance(document.get(field), dict) or not document[field].get('_t'):
            raise ValueError('Perfil deve preencher o objeto nativo ' + field)
    if document['Empresa'].get('PessoaReferencia') != profile['company_id']:
        raise ValueError('Empresa.PessoaReferencia deve coincidir com company_id.')
    if source != 'purchase':
        company_document = digits(str(document['Empresa'].get('Documento') or ''))
        person_document = digits(str(document['Pessoa'].get('Documento') or ''))
        recipient = digits(parsed['recipient_document'])
        if company_document and company_document != parsed['issuer_cnpj']:
            raise ValueError('O documento da empresa no perfil difere do CNPJ emitente do XML.')
        if recipient and person_document and person_document != recipient:
            raise ValueError('O documento da pessoa no perfil difere do destinatário do XML.')
    finance_flag = (document.get('MovimentacaoItemFinanceiro') if parsed['model'] == '65'
                    else document.pop('MovimentacaoItemFinanceiro', False))
    if finance_flag is not False or document.get('Ativo') is not True:
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
    finance = (build_finance(parsed, profile, context, document, info, required=require_finance)
               if include_finance else None)
    if finance:
        document['PagamentoRecebimento'] = {'_t': finance['type'], 'Parcelas': finance['documents']}
    native_xml_type = {'NotaFiscalCompra': 4, 'NotaFiscalEletronicaEntrada': 5,
                       'NotaFiscalEletronicaSaida': 10, 'NotaFiscalConsumidorEletronica': 11}[expected_type]
    xml = {'_id': ObjectId(hashlib.sha256(('insight-nfce-xml:' + identity).encode()).digest()[:12]),
           'Data': wall, 'ChaveAcesso': key, 'Xml': raw.decode('utf-8-sig'),
           'TipoMovimentacao': native_xml_type if parsed['model'] == '65' else profile.get('xml_movement_type', native_xml_type),
           'EventoCancelamento': False, 'Cancelada': False}
    if type(xml['TipoMovimentacao']) is not int:
        raise ValueError('Perfil NF-e exige xml_movement_type nativo inteiro revisado.')
    validate_native_documents(document, xml)
    result = {'movement': document, 'xml': xml, 'summary': {k: parsed[k] for k in
            ('access_key', 'number', 'series', 'total', 'issued_at_original', 'xml_sha256')}, 'notice': NOTICE}
    if registrations:
        result['product_registrations'] = list(registrations.values())
    if finance:
        result['finance'] = finance
    if parsed['installments']:
        result['installments'] = parsed['installments']
    result['parties'] = parsed['parties']
    person_template = profile.get('person_template') if include_people else None
    if person_template is not None:
        result['people'] = build_people(parsed, person_template, context, info, source, document['Pessoa'])
    result['products'] = product_review(parsed, document)
    if include_products and 'product_catalog' in profile:
        result['catalog_products'] = build_catalog_products(
            profile, context, parsed['items'], info.findall('n:det', NS), use_barcode=use_barcode)
    if source == 'purchase':
        result['purchase_cost_candidates'] = [
            {'access_key': key, 'issued_at': parsed['issued_at_original'],
             'line': line['sequence'], 'product_code': line['code'],
             'company_id': profile['company_id'],
             'company_product_id': item['ProdutoServicoEmpresaReferencia'],
             'unit_cost': line['unit_price']}
            for line, item in zip(parsed['items'], context['items'])
        ]
    return result


def product_review(parsed, document):
    return [
        {'line': line['sequence'], 'code': line['code'], 'barcode': line['barcode'],
         'description': line['description'],
         'company_product_id': item['ProdutoServicoEmpresaReferencia']}
        for line, item in zip(parsed['items'], document['ItensBase'], strict=True)
    ]


def build_people(parsed, templates, context, node, source, snapshot=None):
    """Prepare reviewed customer/supplier records from identifiable XML parties."""
    if not isinstance(templates, dict):
        raise ValueError('person_template deve mapear CPF e CNPJ.')
    role = 'issuer' if source == 'purchase' else 'recipient'
    parties = [party for party in parsed['parties'] if party['role'] == role]
    if not parties:
        return []
    party = parties[0]
    document = digits(party['document'])
    field = {11: 'Cpf', 14: 'Cnpj'}.get(len(document))
    if field is None or not document.strip('0') or not party['name']:
        raise ValueError('Pessoa do XML sem nome ou documento utilizável.')
    template = templates.get(field)
    if not isinstance(template, dict):
        raise ValueError('Perfil sem mapeamento de pessoa: ' + field)
    result = render(template, dict(context, person=dict(party, document=document)), node)
    if not isinstance(result, dict) or not result.get('_t') or result.get('Ativo') is not True:
        raise ValueError('Perfil deve definir cadastro de pessoa ativo e tipado.')
    result.update(_id=ObjectId(hashlib.sha256(('insight-dfe-person:' + document).encode()).digest()[:12]),
                  Nome=party['name'])
    result[field] = document
    if isinstance(snapshot, dict):
        reference = snapshot.get('PessoaReferencia')
        if (isinstance(reference, ObjectId) and reference.binary.strip(b'\0')
                and digits(str(snapshot.get('Documento', ''))) == document):
            result['_id'] = reference
    validate_native_documents(result)
    return [result]


def build_catalog_review(profile, context, info, source, details, include_people, include_products,
                         use_barcode, item_template):
    parsed = context['invoice']
    result = {'parties': parsed['parties']}
    if include_people and 'person_template' in profile:
        if source != 'cfe' or any(party['name'] for party in parsed['parties']):
            result['people'] = build_people(parsed, profile['person_template'], context, info,
                                            source, profile.get('customer'))
    if include_products:
        if 'product_catalog' not in profile:
            raise ValueError('A pasta exige product_catalog para importar Produtos separadamente.')
        result['catalog_products'] = build_catalog_products(
            profile, context, parsed['items'], details, use_barcode=use_barcode, item_template=item_template)
    return result


def build_catalog_products(profile, context, lines, details, *, use_barcode=False, item_template=None):
    """Prepare the catalog record and its company link from reviewed BSON templates."""
    templates = profile['product_catalog']
    if not isinstance(templates, dict) or any(not isinstance(templates.get(key), dict)
                                              for key in ('product', 'company_product')):
        raise ValueError('product_catalog exige os modelos product e company_product.')
    records = {}
    items = context['items'] if item_template is None else [None] * len(lines)
    for line, detail, item in zip(lines, details, items, strict=True):
        identifier = product_identifier(line, use_barcode)
        product_mapping = profile['products'].get(identifier)
        if product_mapping is None:
            if item_template is not None:
                raise ValueError('Produto sem mapeamento no catálogo: ' + identifier)
            continue  # The generated registration already supplies this product.
        child = dict(context, item=line, product=product_mapping)
        if item_template is not None:
            snapshot = item_template.get('ProdutoServico')
            if isinstance(snapshot, dict) and '$bind' not in snapshot:
                snapshot = {'ProdutoServicoReferencia': snapshot.get('ProdutoServicoReferencia')}
            item = render({'ProdutoServico': snapshot, 'ProdutoServicoEmpresaReferencia':
                           item_template.get('ProdutoServicoEmpresaReferencia')}, child, detail)
        product = render(templates['product'], child, detail)
        link = render(templates['company_product'], child, detail)
        if use_barcode:
            bind_product_barcode(product, identifier)
        if any(not record.get('_t') or record.get('Ativo') is not True for record in (product, link)):
            raise ValueError('O perfil exige produto e vínculo de empresa ativos e tipados.')
        product_id = product.get('_id')
        snapshot = item.get('ProdutoServico')
        item_product = snapshot.get('ProdutoServicoReferencia') if isinstance(snapshot, dict) else None
        code = product.get('CodigoInterno')
        link_id = item['ProdutoServicoEmpresaReferencia']
        if (not isinstance(product_id, ObjectId) or not product_id.binary.strip(b'\0')
                or not isinstance(link_id, ObjectId) or not link_id.binary.strip(b'\0')
                or product_id != item_product or type(code) not in (str, int, Int64)
                or not str(code).strip()):
            raise ValueError('Produto do catálogo sem código interno ou referência compatível com o item.')
        link.update(_id=link_id, EmpresaReferencia=profile['company_id'],
                    ProdutoServicoReferencia=product_id)
        validate_native_documents(product, link)
        pair = {'product': product, 'company_product': link}
        if records.setdefault(link['_id'], pair) != pair:
            raise ValueError('O XML produziu cadastros incompatíveis para o mesmo vínculo de produto.')
    return list(records.values())


def build_cfe(raw, profile, *, include_products=True, include_people=True, use_barcode=False, catalog_only=False):
    from insight_nfce_discovery import parse_cfe, read_xml

    root = read_xml(raw)
    parsed = parse_cfe(root)
    config = profile.get('cfe')
    if not isinstance(config, dict) or type(config.get('xml_movement_type', 12)) is not int:
        raise ValueError('CF-e exige mapeamento cfe revisado e xml_movement_type inteiro quando informado.')
    if parsed['issuer_cnpj'] != profile['issuer_cnpj'] or profile['environment'] != '1':
        raise ValueError('Emitente ou ambiente do perfil incompatível com a importação de CF-e.')
    info = root.find('infCFe')
    key = parsed['access_key']
    wall = native_issue_time(parsed['issued_at_original'])
    ident = ObjectId(hashlib.sha256(('insight-cfe:' + key).encode()).digest()[:12])
    context = {'profile': profile, 'invoice': parsed, 'id': ident, 'issued_at': wall,
               'number': Int64(parsed['number']), 'series': parsed['series']}
    if catalog_only:
        return build_catalog_review(profile, context, info, 'cfe', info.findall('det'),
                                    include_people, include_products, use_barcode, config.get('item', {}))
    items, payments = [], []
    if not parsed['items']:
        raise ValueError('CF-e sem itens.')
    for line, detail in zip(parsed['items'], info.findall('det'), strict=True):
        product = profile['products'].get(product_identifier(line, use_barcode))
        if not isinstance(product, dict):
            raise ValueError('Produto CF-e sem mapeamento: ' + line['code'])
        child = dict(context, product=product, item=line)
        taxes = detail.find('imposto')
        if taxes is not None:
            coverage = fiscal_coverage(config.get('item'), child)
            for path in tax_values(taxes):
                if not any(matches_path(pattern, path) for pattern in coverage):
                    raise ValueError('Valor fiscal do CF-e sem binding explícito: ' + path)
        item = render(config.get('item'), child, detail)
        if not isinstance(item, dict) or not item.get('_t') or not isinstance(item.get('ProdutoServicoEmpresaReferencia'), ObjectId):
            raise ValueError('Perfil CF-e não definiu o item nativo e seu vínculo de produto.')
        for field, expected in (('Quantidade', line['quantity']), ('PrecoUnitario', line['unit_price']),
                                ('DescontoDigitado', line['discount']), ('OutrasDespesasDigitado', line['addition'])):
            try:
                actual = Decimal(str(item.get(field, 0)))
            except InvalidOperation:
                raise ValueError('Valor inválido no item nativo CF-e: ' + field) from None
            if actual != Decimal(expected):
                raise ValueError('Item nativo CF-e diverge do XML: ' + field)
        item['Numero'] = line['sequence']
        items.append(item)
    payment_templates = config.get('payments')
    if not isinstance(payment_templates, dict):
        raise ValueError('Perfil CF-e exige mapeamento de pagamentos.')
    change = Decimal(parsed['change'])
    cash_entries = [index for index, payment in enumerate(parsed['payments']) if payment['code'] == '01']
    change_entry = 0 if len(parsed['payments']) == 1 else cash_entries[0] if len(cash_entries) == 1 else None
    if change_entry is not None and Decimal(parsed['payments'][change_entry]['amount']) < change:
        change_entry = None
    for index, (payment, entry) in enumerate(zip(parsed['payments'], info.findall('pgto/MP'), strict=True)):
        template = payment_templates.get(payment['code'])
        if not isinstance(template, dict):
            raise ValueError('Pagamento CF-e sem mapeamento: ' + payment['code'])
        values = dict(payment)
        if not change or change_entry is not None:
            net = Decimal(payment['amount']) - (change if index == change_entry else Decimal(0))
            values['net_amount'] = str(net)
        payments.append(render(template, dict(context, payment=values), entry))
    try:
        paid = sum(Decimal(str(payment['Valor'])) for payment in payments)
    except (KeyError, InvalidOperation, TypeError):
        raise ValueError('Pagamento CF-e nativo sem Valor válido.') from None
    if paid != Decimal(parsed['total']):
        raise ValueError('Pagamentos nativos do CF-e devem somar o total líquido de troco.')
    document = render(config.get('movement'), dict(context, items=items, payments=payments), info)
    if not discriminator(document, 'CupomFiscalEletronico'):
        raise ValueError('Perfil CF-e exige o tipo CupomFiscalEletronico.')
    company = document.get('Empresa')
    status = document.get('Situacao')
    legacy_finance_flag = document.pop('MovimentacaoItemFinanceiro', False)
    if (not isinstance(company, dict) or company.get('PessoaReferencia') != profile['company_id']
            or not discriminator(status, 'Concluido') or status.get('Codigo') != 2
            or legacy_finance_flag is not False or document.get('Ativo') is not True):
        raise ValueError('Perfil CF-e exige empresa vinculada e movimento histórico ativo e concluído.')
    company_document = digits(str(company.get('Documento') or ''))
    if company_document and company_document != parsed['issuer_cnpj']:
        raise ValueError('O documento da empresa no perfil difere do CNPJ emitente do CF-e.')
    person = document.get('Pessoa')
    if isinstance(person, dict) and parsed['parties']:
        person_document = digits(str(person.get('Documento') or ''))
        if person_document and person_document != digits(parsed['parties'][0]['document']):
            raise ValueError('O documento da pessoa no perfil difere do destinatário do CF-e.')
    document.update(_id=ident, EmpresaReferencia=profile['company_id'], ChaveAcesso=key,
                    Numero=context['number'], Serie=context['series'], NumeroFabricacaoSat=parsed['series'],
                    DataHoraEmissao=wall, ItensBase=items, Pagamentos=payments)
    xml = {'_id': ObjectId(hashlib.sha256(('insight-cfe-xml:' + key).encode()).digest()[:12]),
           'Data': wall, 'ChaveAcesso': key, 'Xml': raw.decode('utf-8-sig'),
           'TipoMovimentacao': config.get('xml_movement_type', 12), 'EventoCancelamento': False, 'Cancelada': False}
    validate_native_documents(document, xml)
    result = {'movement': document, 'xml': xml, 'summary': parsed, 'notice': NOTICE,
              'products': product_review(parsed, document), 'parties': parsed['parties']}
    if include_people and 'person_template' in profile and any(party['name'] for party in parsed['parties']):
        result['people'] = build_people(parsed, profile['person_template'], context, info,
                                        'cfe', document.get('Pessoa'))
    if include_products and 'product_catalog' in profile:
        result['catalog_products'] = build_catalog_products(
            profile, dict(context, items=items), parsed['items'], info.findall('det'), use_barcode=use_barcode)
    return result


def validate_native_documents(*documents):
    for document in documents:
        if len(BSON.encode(document)) > MONGO_DOCUMENT_BYTES:
            raise ValueError('O documento BSON excede o limite nativo de 16 MiB do MongoDB.')


def purchase_cost_change(price, unit_cost):
    """Prepare the native Custo object while preserving Venda and its BSON type."""
    from copy import deepcopy

    cost = price.get('Custo')
    if not isinstance(cost, dict):
        raise ValueError('Cadastro de preço sem objeto Custo.')
    previous = cost.get('Valor')
    amount = Decimal(unit_cost)
    if not amount.is_finite() or amount < 0:
        raise ValueError('Custo unitário de compra inválido.')
    if isinstance(previous, Decimal128):
        value = Decimal128(amount)
    elif type(previous) is float:
        value = float(amount)
    else:
        raise ValueError('Tipo BSON do preço de custo não reconhecido.')
    if value == previous:
        return None
    return dict(price, Custo=dict(deepcopy(cost), Valor=value))

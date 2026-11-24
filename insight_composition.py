"""Purchases of composition components and how many compositions they support.

Read-only: the report never writes to the SuiteG6 database. The calculation uses
the quantity that entered stock (converted quantity), not only the document
quantity, because a purchase unit can differ from the stock unit used by the
composition.
"""
from __future__ import annotations

from collections import Counter, defaultdict
from datetime import date, datetime, time, timedelta
from decimal import Decimal, InvalidOperation, ROUND_FLOOR
import math
from time import monotonic

from bson import ObjectId
from fastapi import HTTPException
from pydantic import BaseModel, ConfigDict, Field, field_validator
from pymongo.errors import PyMongoError

import insight_store as store
from insight_version import VERSION

PURCHASE_TYPES = ('NotaFiscalCompra', 'NotaFiscalEletronicaEntrada')
MAX_COMPOSITIONS = 2000
MAX_PURCHASE_LINES = 20000
MAX_PERIOD_DAYS = 3660


class CompositionRequest(BaseModel):
    model_config = ConfigDict(extra='forbid')
    company_id: str
    start_date: date
    end_date: date
    composition_id: str = ''
    search: str = Field(default='', max_length=100)
    concluded_only: bool = True
    include_optional: bool = True

    @field_validator('company_id')
    @classmethod
    def valid_company(cls, value):
        if not ObjectId.is_valid(value):
            raise ValueError('Selecione uma empresa válida.')
        return str(ObjectId(value))

    @field_validator('end_date')
    @classmethod
    def valid_period(cls, value, info):
        start = info.data.get('start_date')
        if start is not None and value < start:
            raise ValueError('A data final não pode ser anterior à data inicial.')
        if start is not None and (value - start).days > MAX_PERIOD_DAYS:
            raise ValueError('O período não pode exceder 10 anos.')
        return value

    @field_validator('composition_id')
    @classmethod
    def valid_composition(cls, value):
        value = str(value or '').strip()
        if value and not ObjectId.is_valid(value):
            raise ValueError('Selecione uma composição válida.')
        return str(ObjectId(value)) if value else ''

    @field_validator('search')
    @classmethod
    def clean_search(cls, value):
        return str(value or '').strip()


def quantity(value, label='quantidade'):
    try:
        if hasattr(value, 'to_decimal'):
            value = value.to_decimal()
        result = Decimal(str(value))
        if not result.is_finite() or not math.isfinite(float(result)):
            raise InvalidOperation
        return result
    except (InvalidOperation, TypeError, ValueError, OverflowError):
        raise HTTPException(422, f'{label} inválida. Confira o cadastro e os movimentos de compra.') from None


def text(value):
    return str(value or '').strip()


def moment(value):
    if isinstance(value, datetime):
        return value
    if isinstance(value, date):
        return datetime.combine(value, time.min)
    return None


def natural(value):
    """Sort numbers naturally while keeping non-numeric movement numbers stable."""
    value = text(value)
    try:
        return (0, int(value), value)
    except ValueError:
        return (1, 0, value)


def day(value):
    when = moment(value)
    return when.date().isoformat() if when else ''


def identifier(value):
    if value is None:
        return ''
    if isinstance(value, float) and value.is_integer():
        return str(int(value))
    return str(value)


def unique(values):
    seen = set()
    result = []
    for value in values:
        if value and value not in seen:
            seen.add(value)
            result.append(value)
    return result


def company_object(value):
    if not ObjectId.is_valid(value):
        raise HTTPException(422, 'Selecione uma empresa válida.')
    return ObjectId(value)


def composition_pipeline(company, composition_id=''):
    match = {'Ativo': True, '_t': 'Composicao'}
    if composition_id:
        match['_id'] = ObjectId(composition_id)
    return [
        {'$match': match},
        {'$lookup': {'from': 'ProdutosServicosEmpresa', 'localField': '_id',
                     'foreignField': 'ProdutoServicoReferencia', 'as': 'link'}},
        {'$unwind': '$link'},
        {'$match': {'link.EmpresaReferencia': company, 'link._t': 'ComposicaoEmpresa'}},
        {'$project': {'_id': 1, 'CodigoInterno': 1, 'Descricao': 1, 'UnidadeMedida': 1,
                      'Componentes': 1, 'link_id': '$link._id'}},
    ]


def read_compositions(db, company, composition_id=''):
    cursor = db.ProdutosServicos.aggregate(composition_pipeline(company, composition_id),
                                           maxTimeMS=20000, allowDiskUse=False, batchSize=50)
    try:
        rows = list(cursor)
    finally:
        cursor.close()
    if len(rows) > MAX_COMPOSITIONS:
        raise HTTPException(422, f'A consulta encontrou mais de {MAX_COMPOSITIONS} composições. '
                                 'Selecione uma composição específica.')
    return rows


def read_products(db, component_ids):
    if not component_ids:
        return {}
    cursor = db.ProdutosServicos.find(
        {'_id': {'$in': list(component_ids)}},
        {'CodigoInterno': 1, 'Descricao': 1, 'UnidadeMedida': 1, 'Ativo': 1, '_t': 1},
    ).max_time_ms(20000)
    return {str(row['_id']): row for row in cursor}


def read_links(db, company, component_ids):
    if not component_ids:
        return {}
    cursor = db.ProdutosServicosEmpresa.find(
        {'EmpresaReferencia': company, 'ProdutoServicoReferencia': {'$in': list(component_ids)}},
        {'ProdutoServicoReferencia': 1},
    ).max_time_ms(20000)
    return {row['_id']: row['ProdutoServicoReferencia'] for row in cursor}


def movement_pipeline(req, company, product_ids, link_ids):
    start = datetime.combine(req.start_date, time.min)
    end = datetime.combine(req.end_date + timedelta(days=1), time.min)
    product_match = [
        {'ItensBase.ProdutoServico.ProdutoServicoReferencia': {'$in': list(product_ids)}},
    ]
    if link_ids:
        product_match.append({'ItensBase.ProdutoServicoEmpresaReferencia': {'$in': list(link_ids)}})
    return [
        {'$match': {'_t': {'$in': list(PURCHASE_TYPES)}, 'EmpresaReferencia': company}},
        {'$match': {'$and': [
            {'$or': [
                {'DataHoraEntradaSaida': {'$gte': start, '$lt': end}},
                {'$and': [{'DataHoraEntradaSaida': None},
                          {'DataHoraEmissao': {'$gte': start, '$lt': end}}]},
                {'$and': [{'DataHoraEntradaSaida': {'$exists': False}},
                          {'DataHoraEmissao': {'$gte': start, '$lt': end}}]},
            ]},
            {'$or': product_match},
        ]}},
        {'$unwind': '$ItensBase'},
        {'$project': {'_id': 1, 'Numero': 1, 'Serie': 1, 'DataHoraEmissao': 1,
                      'DataHoraEntradaSaida': 1, 'Situacao': 1, 'Pessoa.Nome': 1,
                      'Pessoa.NomeFantasia': 1, 'types': '$_t', 'item': '$ItensBase'}},
    ]


def document_type(types):
    types = types or []
    if isinstance(types, str):
        types = [types]
    if 'NotaFiscalEletronicaEntrada' in types:
        return 'NF-e de entrada'
    if 'NotaFiscalCompra' in types:
        return 'Nota de compra'
    return 'Compra'


def purchase_lines(records, product_ids, link_to_product, req, *, product_units=None, deadline=None):
    lines = []
    stats = Counter()
    product_units = product_units or {}
    examined = 0
    for record in records:
        stats['documents'] += 1
        examined += 1
        if examined % 512 == 0 and deadline is not None and monotonic() > deadline:
            raise HTTPException(422, 'A consulta de compras excedeu 20 segundos. '
                                     'Tente novamente em um horário de menor carga.')
        item = record.get('item') or {}
        history = item.get('ProdutoServico') or {}
        product_ref = history.get('ProdutoServicoReferencia')
        component = product_ref if isinstance(product_ref, ObjectId) else None
        if component not in product_ids:
            component = link_to_product.get(item.get('ProdutoServicoEmpresaReferencia'))
        if component not in product_ids:
            stats['unmatched_items'] += 1
            continue
        if bool(item.get('Cancelado')):
            stats['cancelled_items'] += 1
            continue

        situation = record.get('Situacao') or {}
        code = situation.get('Codigo')
        description = text(situation.get('Descricao'))
        concluded = code == 2 or 'conclu' in description.casefold()
        unknown_status = code is None and not description
        if req.concluded_only and not concluded:
            if unknown_status:
                stats['unknown_status_items'] += 1
                continue
            stats['pending_items'] += 1
            continue

        when = moment(record.get('DataHoraEntradaSaida')) or moment(record.get('DataHoraEmissao'))
        if when is None:
            stats['undated_items'] += 1
            continue

        purchased = quantity(item.get('Quantidade'), 'Quantidade comprada')
        stock = None
        for key in ('EstoqueMovimentacao', 'ConversaoQuantidadePreco'):
            rounded = item.get(key) or {}
            if rounded.get('Quantidade') is not None:
                converted = quantity(rounded.get('Quantidade'), 'Quantidade de estoque')
                if converted != 0:
                    stock = converted
                    break
        if stock is None:
            stock = purchased
        if purchased < 0 or stock < 0:
            stats['negative_items'] += 1

        document_unit = text((history.get('UnidadeMedida') or {}).get('Sigla'))
        unit = text(product_units.get(str(component))) or document_unit
        person = record.get('Pessoa') or {}
        lines.append({
            'movement_id': str(record.get('_id') or ''),
            'component_id': str(component),
            'number': identifier(record.get('Numero')),
            'series': identifier(record.get('Serie')),
            'date': when.date().isoformat(),
            'issued_at': day(record.get('DataHoraEmissao')),
            'supplier': text(person.get('Nome') or person.get('NomeFantasia')),
            'quantity': float(purchased),
            'stock_quantity': float(stock),
            'document_unit': document_unit,
            'unit': unit,
            'situation': description or ('Concluído' if concluded else f'Código {code}'),
            'concluded': bool(concluded),
            'unknown_status': bool(unknown_status),
            'item_number': identifier(item.get('Numero')),
            'document_type': document_type(record.get('types')),
            '_id': record.get('_id'),
            '_order': (when, natural(record.get('Numero')), natural(item.get('Numero'))),
        })
        if len(lines) > MAX_PURCHASE_LINES:
            raise HTTPException(422, f'A consulta excede {MAX_PURCHASE_LINES} linhas de compra. '
                                     'Reduza o período ou selecione uma composição.')
    lines.sort(key=lambda row: row['_order'])
    for row in lines:
        row.pop('_order', None)
        row.pop('_id', None)
    return lines, stats


def composition_components(composition, products, lines, include_optional=True):
    """Build one composition and its component purchase detail."""
    grouped = defaultdict(list)
    for line in lines:
        grouped[line['component_id']].append(line)
    code = text(composition.get('CodigoInterno'))
    description = text(composition.get('Descricao')) or '(sem descrição)'
    unit = text((composition.get('UnidadeMedida') or {}).get('Sigla'))
    components = []
    warnings = []
    for raw in composition.get('Componentes') or []:
        reference = raw.get('ProdutoReferencia')
        if not isinstance(reference, ObjectId) or reference == ObjectId('000000000000000000000000'):
            warnings.append(f'Composição {code or description}: componente sem referência de produto foi ignorado.')
            continue
        product = products.get(str(reference)) or {}
        optional = bool(raw.get('Opcional'))
        if optional and not include_optional:
            continue
        per = quantity(raw.get('Quantidade'), 'Quantidade do componente na composição')
        entries = grouped.get(str(reference), [])
        total = sum((quantity(entry['stock_quantity']) for entry in entries), Decimal(0))
        possible = None
        if per > 0:
            possible = max(0, int((total / per).to_integral_value(rounding=ROUND_FLOOR)))
        else:
            warnings.append(f'Composição {code or description}: o componente '
                            f'{text(raw.get("ProdutoDescricao"))} tem quantidade zero ou negativa e não entra no cálculo.')
        component_code = text(product.get('CodigoInterno'))
        component_description = text(product.get('Descricao')) or text(raw.get('ProdutoDescricao')) or '(sem descrição)'
        component_unit = text((product.get('UnidadeMedida') or {}).get('Sigla'))
        document_units = {entry['document_unit'] for entry in entries if entry['document_unit']}
        stock_units = {entry['unit'] for entry in entries if entry['unit']}
        if len(stock_units) > 1:
            warnings.append(f'Composição {code or description}: o componente {component_description} '
                            'possui compras com unidades de estoque diferentes; confira a soma.')
        components.append({
            'product_id': str(reference),
            'code': component_code,
            'description': component_description,
            'unit': component_unit,
            'optional': optional,
            'quantity_per_composition': float(per),
            'purchased_total': float(total),
            'possible_compositions': possible,
            'purchase_count': len(entries),
            'purchase_documents': len({entry['movement_id'] for entry in entries}),
            'document_units': sorted(document_units),
            'stock_units': sorted(stock_units),
            'purchases': entries,
        })
    components.sort(key=lambda item: (item['optional'], item['description'].casefold(), item['code']))
    mandatory = [item for item in components if not item['optional'] and item['quantity_per_composition'] > 0]
    possible = None
    limiting = None
    if mandatory:
        limiting = min(mandatory, key=lambda item: (item['possible_compositions'] or 0, item['description'].casefold()))
        possible = limiting['possible_compositions']
    elif components:
        warnings.append(f'Composição {code or description}: não há componente obrigatório com quantidade válida; '
                        'não foi possível calcular quantas composições podem ser montadas.')
    return {
        'id': str(composition.get('_id')),
        'code': code,
        'description': description,
        'unit': unit,
        'component_count': len(components),
        'mandatory_count': len(mandatory),
        'possible_compositions': possible,
        'limiting_component': ({'code': limiting['code'], 'description': limiting['description'],
                                'possible_compositions': limiting['possible_compositions']}
                               if limiting else None),
        'purchase_lines': sum(item['purchase_count'] for item in components),
        'purchase_documents': len({entry['movement_id'] for item in components for entry in item['purchases']}),
        'components': components,
    }, warnings


def build_report(compositions, products, lines, req):
    report = []
    warnings = []
    for composition in compositions:
        item, extra = composition_components(composition, products, lines, req.include_optional)
        if req.search:
            needle = req.search.casefold()
            haystack = ' '.join([item['code'], item['description']] + [
                f'{component["code"]} {component["description"]}' for component in item['components']
            ]).casefold()
            if needle not in haystack:
                continue
        report.append(item)
        warnings.extend(extra)
    report.sort(key=lambda item: (natural(item['code']), item['description'].casefold()))
    return report, unique(warnings)


def flatten_rows(report):
    rows = []
    for composition in report:
        label = ' - '.join(part for part in (composition['code'], composition['description']) if part)
        for component in composition['components']:
            for line in component['purchases']:
                rows.append({
                    'Composicao': label,
                    'Componente': component['description'],
                    'CodigoComponente': component['code'],
                    'QuantidadePorComposicao': component['quantity_per_composition'],
                    'Movimento': line['number'],
                    'Serie': line['series'],
                    'Item': line['item_number'],
                    'Data': line['date'],
                    'Fornecedor': line['supplier'],
                    'QuantidadeDocumento': line['quantity'],
                    'UnidadeDocumento': line['document_unit'],
                    'QuantidadeEstoque': line['stock_quantity'],
                    'Unidade': line['unit'],
                    'Situacao': line['situation'],
                    'TipoDocumento': line['document_type'],
                    'ComposicoesPossiveis': composition['possible_compositions'],
                })
    return rows


def summarize(report, stats, req):
    components = sum(item['component_count'] for item in report)
    documents = len({line['movement_id'] for item in report for component in item['components']
                     for line in component['purchases']})
    lines = sum(item['purchase_lines'] for item in report)
    return {'compositions': len(report), 'components': components,
            'purchase_documents': documents, 'purchase_lines': lines,
            'pending_items_ignored': stats['pending_items'], 'cancelled_items_ignored': stats['cancelled_items']}


def companies():
    import app
    try:
        with app.mongo_client() as client:
            db = client[app.MONGO_DATABASE]
            refs = db.ProdutosServicosEmpresa.distinct('EmpresaReferencia', {'_t': 'ComposicaoEmpresa'},
                                                        maxTimeMS=20000)
            if len(refs) > 200:
                raise HTTPException(422, 'Mais de 200 empresas com composições encontradas. '
                                         'Configure uma base com escopo adequado.')
            names = {row['_id']: row.get('Nome') for row in
                     db.Pessoas.find({'_id': {'$in': refs}}, {'Nome': 1}).max_time_ms(20000)}
            return {'companies': [{'id': str(ref), 'name': names.get(ref) or str(ref)}
                                  for ref in sorted(refs, key=str)]}
    except PyMongoError:
        raise HTTPException(503, 'Não foi possível carregar as empresas com composições. Confira a conexão.') from None


def options(company_id):
    import app
    company = company_object(company_id)
    try:
        with app.mongo_client() as client:
            db = client[app.MONGO_DATABASE]
            if 'ProdutosServicos' not in db.list_collection_names():
                raise HTTPException(422, 'Esta base não possui a coleção de produtos.')
            rows = read_compositions(db, company)
    except PyMongoError:
        raise HTTPException(503, 'Não foi possível carregar as composições. Confira a conexão.') from None
    result = [{'id': str(row['_id']), 'code': text(row.get('CodigoInterno')),
               'description': text(row.get('Descricao')) or '(sem descrição)',
               'unit': text((row.get('UnidadeMedida') or {}).get('Sigla')),
               'components': len(row.get('Componentes') or [])} for row in rows]
    result.sort(key=lambda item: (natural(item['code']), item['description'].casefold()))
    return {'compositions': result}


def query_composition_purchases(req: CompositionRequest):
    import app
    started = monotonic()
    company = company_object(req.company_id)
    try:
        with app.mongo_client() as client:
            db = client[app.MONGO_DATABASE]
            required = {'ProdutosServicos', 'ProdutosServicosEmpresa', 'Movimentacoes'}
            if not required.issubset(db.list_collection_names()):
                raise HTTPException(422, 'Esta base não possui as coleções necessárias para consultar compras de composição.')
            compositions = read_compositions(db, company, req.composition_id)
            if not compositions:
                raise HTTPException(422, 'Nenhuma composição ativa foi encontrada para a empresa selecionada.')
            component_ids = {item.get('ProdutoReferencia') for comp in compositions
                             for item in (comp.get('Componentes') or [])
                             if isinstance(item.get('ProdutoReferencia'), ObjectId)
                             and item.get('ProdutoReferencia') != ObjectId('000000000000000000000000')}
            products = read_products(db, component_ids)
            link_to_product = read_links(db, company, component_ids)
            cursor = db.Movimentacoes.aggregate(
                movement_pipeline(req, company, component_ids, set(link_to_product)),
                maxTimeMS=20000, allowDiskUse=False, batchSize=200)
            product_units = {key: text((row.get('UnidadeMedida') or {}).get('Sigla'))
                             for key, row in products.items()}
            try:
                lines, stats = purchase_lines(cursor, component_ids, link_to_product, req,
                                              product_units=product_units, deadline=started + 20)
            finally:
                cursor.close()
    except PyMongoError:
        raise HTTPException(503, 'Não foi possível consultar as compras no prazo. '
                                 'Confira a conexão ou tente novamente.') from None

    report, calculation_warnings = build_report(compositions, products, lines, req)
    rows = flatten_rows(report)
    summary = summarize(report, stats, req)
    nested = any(products.get(component['product_id'], {}).get('_t') and
                 'Composicao' in (products.get(component['product_id'], {}).get('_t') or [])
                 for composition in report for component in composition['components'])
    warnings = unique([
        *calculation_warnings,
        'Quantidade comprada para o cálculo: soma das quantidades de estoque dos itens não cancelados; '
        'a quantidade do documento é exibida quando difere.',
        'Composições possíveis: menor resultado inteiro entre quantidade comprada ÷ quantidade por composição '
        'dos componentes obrigatórios. Componentes opcionais não limitam o resultado.',
        'Um mesmo componente pode aparecer em mais de uma composição; as compras são compartilhadas e não são '
        'rateadas entre elas.',
        'Somente compras com situação concluída entraram no total.' if req.concluded_only
        else 'Todas as situações de compra foram incluídas, inclusive pendentes.',
        *([f'{stats["pending_items"]} item(ns) de compra pendente(s) foram ignorados.']
          if stats['pending_items'] else []),
        *([f'{stats["unknown_status_items"]} item(ns) sem situação foram ignorados; '
           'não foi possível confirmar a conclusão.'] if stats['unknown_status_items'] else []),
        *([f'{stats["cancelled_items"]} item(ns) cancelado(s) foram ignorados.']
          if stats['cancelled_items'] else []),
        *([f'{stats["negative_items"]} linha(s) com quantidade negativa foram incluídas; confira devoluções.']
          if stats['negative_items'] else []),
        *([f'{stats["undated_items"]} item(ns) sem data de entrada ou emissão foram ignorados.']
          if stats['undated_items'] else []),
        *(['Há componentes que também são composições; a compra desse componente não foi expandida '
           'em subcomponentes.'] if nested else []),
        *(['Nenhuma compra foi encontrada para os componentes no período. As quantidades possíveis ficam zeradas.']
          if not rows else []),
    ])
    result = {
        'module': 'composition_purchases',
        'compositions': report,
        'rows': rows,
        'summary': summary,
        'interpretation': {
            'kind_label': 'Compras de componentes de composição',
            'summary': [
                f'Empresa: {req.company_id}',
                f'Período: {req.start_date.isoformat()} a {req.end_date.isoformat()} (data de entrada, '
                'com a emissão como alternativa).',
                f'Composições analisadas: {summary["compositions"]}; componentes: {summary["components"]}.',
                f'Compras: {summary["purchase_documents"]} documento(s) e {summary["purchase_lines"]} linha(s).',
                'Cálculo: quantidade de estoque comprada ÷ quantidade do componente na composição, '
                'arredondada para baixo, limitada pelo componente obrigatório com menor resultado.',
            ],
        },
        'warnings': warnings,
        'provenance': {'version': VERSION, 'database': app.MONGO_DATABASE, 'source': app.MODE,
                       'queried_at': store.now(), 'start_date': str(req.start_date),
                       'end_date': str(req.end_date), 'collections': ['ProdutosServicos',
                       'ProdutosServicosEmpresa', 'Movimentacoes']},
        'duration_ms': round((monotonic() - started) * 1000, 1),
    }
    run_id = store.save_run({'module': 'composition_purchases', **req.model_dump(mode='json')}, result)
    return {'run_id': run_id, 'result': result}

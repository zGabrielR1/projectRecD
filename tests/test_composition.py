"""Synthetic composition purchases: BOM math, conversion, status and provenance."""
from datetime import date, datetime
import os
from pathlib import Path
import tempfile
import unittest
from unittest.mock import MagicMock, patch

from bson import ObjectId, Decimal128
from fastapi import HTTPException
from pydantic import ValidationError

import insight_composition as composition
import insight_store as store

COMPANY = ObjectId()
COMPOSITION = ObjectId()
HAMBURGER = ObjectId()
BACON = ObjectId()
LINK = ObjectId()
OTHER = ObjectId()


def request(**changes):
    return composition.CompositionRequest(**{
        'company_id': str(COMPANY),
        'start_date': date(2026, 9, 1),
        'end_date': date(2026, 9, 30),
        **changes,
    })


def product(code, description, unit='UN', types=None):
    return {'CodigoInterno': code, 'Descricao': description, 'UnidadeMedida': {'Sigla': unit},
            '_t': types or ['ProdutoServico', 'Produto']}


def composition_doc(**changes):
    document = {
        '_id': COMPOSITION,
        'CodigoInterno': '58',
        'Descricao': 'Composição',
        'UnidadeMedida': {'Sigla': 'UN'},
        'Componentes': [
            {'ProdutoDescricao': 'X-Salada', 'ProdutoReferencia': HAMBURGER, 'Opcional': False,
             'Quantidade': 1},
            {'ProdutoDescricao': 'Bacon', 'ProdutoReferencia': BACON, 'Opcional': True,
             'Quantidade': Decimal128('0.5')},
        ],
    }
    document.update(changes)
    return document


def purchase(product_id=HAMBURGER, *, number=6, series='3', quantity=1, stock=12,
             component_link=None, cancelled=False, code=2, description='Concluído',
             entry=datetime(2026, 9, 16, 13, 56), issue=datetime(2026, 9, 16, 13, 56)):
    item = {
        'Numero': 1,
        'ProdutoServicoEmpresaReferencia': component_link or LINK,
        'ProdutoServico': {'ProdutoServicoReferencia': product_id,
                           'UnidadeMedida': {'Sigla': 'CX'}},
        'Quantidade': quantity,
        'Cancelado': cancelled,
        'EstoqueMovimentacao': {'Quantidade': stock},
        'ConversaoQuantidadePreco': {'Quantidade': stock},
    }
    return {
        '_id': ObjectId(),
        '_t': ['Movimentacao', 'DocumentoFiscal', 'NotaFiscal', 'NotaFiscalCompra'],
        'Numero': number,
        'Serie': series,
        'DataHoraEmissao': issue,
        'DataHoraEntradaSaida': entry,
        'Situacao': {'Codigo': code, 'Descricao': description},
        'Pessoa': {'Nome': 'Fornecedor de teste'},
        'item': item,
    }


class RequestRules(unittest.TestCase):
    def test_company_period_and_composition_are_validated(self):
        for changes in [
            {'company_id': '$where'},
            {'company_id': str(OTHER) + 'x'},
            {'start_date': date(2026, 10, 1)},
            {'composition_id': 'not-an-object-id'},
            {'search': 'x' * 101},
            {'extra': True},
        ]:
            with self.subTest(changes=changes), self.assertRaises(ValidationError):
                request(**changes)

    def test_long_period_is_rejected(self):
        with self.assertRaises(ValidationError):
            request(start_date=date(2000, 1, 1), end_date=date(2026, 1, 1))

    def test_search_is_trimmed_and_empty_composition_is_allowed(self):
        req = request(search='  x-salada  ')
        self.assertEqual(req.search, 'x-salada')
        self.assertEqual(req.composition_id, '')


class PurchaseLines(unittest.TestCase):
    def test_document_and_stock_quantities_are_both_preserved(self):
        records = [purchase(quantity=1, stock=12)]
        lines, stats = composition.purchase_lines(records, {HAMBURGER}, {LINK: HAMBURGER},
                                                  request(), product_units={str(HAMBURGER): 'UN'})
        self.assertEqual(len(lines), 1)
        self.assertEqual(lines[0]['quantity'], 1)
        self.assertEqual(lines[0]['stock_quantity'], 12)
        self.assertEqual(lines[0]['document_unit'], 'CX')
        self.assertEqual(lines[0]['unit'], 'UN')
        self.assertEqual(lines[0]['supplier'], 'Fornecedor de teste')
        self.assertEqual(stats['cancelled_items'], 0)

    def test_link_reference_is_used_when_product_history_is_missing(self):
        record = purchase(component_link=LINK)
        record['item']['ProdutoServico']['ProdutoServicoReferencia'] = None
        lines, _ = composition.purchase_lines([record], {HAMBURGER}, {LINK: HAMBURGER}, request())
        self.assertEqual(len(lines), 1)
        self.assertEqual(lines[0]['component_id'], str(HAMBURGER))

    def test_cancelled_and_pending_items_are_excluded_by_default(self):
        records = [purchase(cancelled=True), purchase(code=1, description='Aguardando'), purchase(number=7)]
        lines, stats = composition.purchase_lines(records, {HAMBURGER}, {LINK: HAMBURGER}, request())
        self.assertEqual([line['number'] for line in lines], ['7'])
        self.assertEqual(stats['cancelled_items'], 1)
        self.assertEqual(stats['pending_items'], 1)

    def test_pending_items_can_be_included(self):
        records = [purchase(code=1, description='Aguardando')]
        lines, _ = composition.purchase_lines(records, {HAMBURGER}, {LINK: HAMBURGER},
                                              request(concluded_only=False))
        self.assertEqual(len(lines), 1)
        self.assertFalse(lines[0]['concluded'])
        self.assertEqual(lines[0]['situation'], 'Aguardando')

    def test_unknown_status_is_not_treated_as_concluded(self):
        record = purchase(code=None, description='')
        lines, stats = composition.purchase_lines([record], {HAMBURGER}, {}, request())
        self.assertEqual(lines, [])
        self.assertEqual(stats['unknown_status_items'], 1)
        lines, _ = composition.purchase_lines(
            [record], {HAMBURGER}, {}, request(concluded_only=False))
        self.assertEqual(len(lines), 1)
        self.assertFalse(lines[0]['concluded'])

    def test_entry_date_falls_back_to_issue_and_missing_dates_are_skipped(self):
        record = purchase(entry=None, issue=datetime(2026, 9, 20, 8, 0))
        lines, _ = composition.purchase_lines([record], {HAMBURGER}, {LINK: HAMBURGER}, request())
        self.assertEqual(lines[0]['date'], '2026-09-20')
        record = purchase(entry=None, issue=None)
        lines, stats = composition.purchase_lines([record], {HAMBURGER}, {LINK: HAMBURGER}, request())
        self.assertEqual(lines, [])
        self.assertEqual(stats['undated_items'], 1)

    def test_unmatched_and_invalid_quantities(self):
        lines, stats = composition.purchase_lines([purchase(product_id=OTHER)], {HAMBURGER}, {}, request())
        self.assertEqual(lines, [])
        self.assertEqual(stats['unmatched_items'], 1)
        with self.assertRaises(HTTPException):
            composition.purchase_lines([purchase(quantity='bad')], {HAMBURGER}, {LINK: HAMBURGER}, request())

    def test_line_limit_fails_closed(self):
        with patch.object(composition, 'MAX_PURCHASE_LINES', 0), self.assertRaises(HTTPException):
            composition.purchase_lines([purchase()], {HAMBURGER}, {LINK: HAMBURGER}, request())


class BOMCalculation(unittest.TestCase):
    def rows(self, quantity=1, stock=12):
        return [{'component_id': str(HAMBURGER), 'movement_id': str(ObjectId()), 'number': '6',
                 'series': '3', 'date': '2026-09-16', 'supplier': 'Fornecedor',
                 'quantity': quantity, 'stock_quantity': stock, 'document_unit': 'CX', 'unit': 'UN',
                 'situation': 'Concluído', 'concluded': True, 'document_type': 'Nota de compra',
                 'item_number': '1'}]

    def test_possible_is_limited_by_mandatory_component(self):
        report, warnings = composition.build_report(
            [composition_doc()],
            {str(HAMBURGER): product('27', 'X-Salada'),
             str(BACON): product('28', 'Bacon')},
            self.rows(stock=12),
            request(),
        )
        item = report[0]
        self.assertEqual(item['possible_compositions'], 12)
        self.assertEqual(item['limiting_component']['description'], 'X-Salada')
        self.assertEqual(item['components'][0]['purchased_total'], 12)
        self.assertEqual(item['components'][1]['purchased_total'], 0)
        self.assertEqual(warnings, [])

    def test_optional_component_does_not_limit_and_floor_is_applied(self):
        report, _ = composition.build_report(
            [composition_doc()],
            {str(HAMBURGER): product('27', 'X-Salada'), str(BACON): product('28', 'Bacon')},
            self.rows(stock=10),
            request(),
        )
        item = report[0]
        self.assertEqual(item['possible_compositions'], 10)
        optional = next(component for component in item['components'] if component['optional'])
        self.assertEqual(optional['quantity_per_composition'], 0.5)
        self.assertEqual(optional['possible_compositions'], 0)

    def test_zero_component_does_not_divide_and_warns(self):
        document = composition_doc()
        document['Componentes'][1]['Quantidade'] = 0
        report, warnings = composition.build_report(
            [document],
            {str(HAMBURGER): product('27', 'X-Salada'), str(BACON): product('28', 'Bacon')},
            self.rows(stock=10),
            request(),
        )
        self.assertEqual(report[0]['possible_compositions'], 10)
        self.assertTrue(any('quantidade zero' in warning for warning in warnings))

    def test_shared_component_is_evaluated_independently(self):
        second = composition_doc(_id=ObjectId(), CodigoInterno='59', Descricao='Outra composição',
                                 Componentes=[{'ProdutoDescricao': 'X-Salada',
                                               'ProdutoReferencia': HAMBURGER,
                                               'Opcional': False, 'Quantidade': 2}])
        report, _ = composition.build_report(
            [composition_doc(), second],
            {str(HAMBURGER): product('27', 'X-Salada')},
            self.rows(stock=10),
            request(),
        )
        self.assertEqual([item['possible_compositions'] for item in report], [10, 5])

    def test_search_filters_composition_or_component(self):
        products = {str(HAMBURGER): product('27', 'X-Salada'), str(BACON): product('28', 'Bacon')}
        for search, expected in [('58', 1), ('x-salada', 1), ('bacon', 1), ('inexistente', 0)]:
            with self.subTest(search=search):
                report, _ = composition.build_report([composition_doc()], products, self.rows(),
                                                     request(search=search))
                self.assertEqual(len(report), expected)

    def test_flat_rows_keep_movement_and_quantities(self):
        report, _ = composition.build_report(
            [composition_doc()],
            {str(HAMBURGER): product('27', 'X-Salada'), str(BACON): product('28', 'Bacon')},
            self.rows(quantity=1, stock=12),
            request(),
        )
        rows = composition.flatten_rows(report)
        self.assertEqual(len(rows), 1)
        self.assertEqual(rows[0]['Movimento'], '6')
        self.assertEqual(rows[0]['QuantidadeDocumento'], 1)
        self.assertEqual(rows[0]['QuantidadeEstoque'], 12)
        self.assertEqual(rows[0]['ComposicoesPossiveis'], 12)


class LiveShape(unittest.TestCase):
    def test_query_saves_a_run_with_provenance(self):
        import app
        records = [purchase()]
        composition_cursor = MagicMock(); composition_cursor.__iter__.return_value = iter([composition_doc()])
        movement_cursor = MagicMock(); movement_cursor.__iter__.return_value = iter(records)
        products_cursor = MagicMock(); products_cursor.__iter__.return_value = iter([
            {'_id': HAMBURGER, **product('27', 'X-Salada')},
            {'_id': BACON, **product('28', 'Bacon')},
        ])
        links_cursor = MagicMock(); links_cursor.__iter__.return_value = iter([
            {'_id': LINK, 'ProdutoServicoReferencia': HAMBURGER},
        ])
        db = MagicMock()
        db.list_collection_names.return_value = ['ProdutosServicos', 'ProdutosServicosEmpresa', 'Movimentacoes']
        db.ProdutosServicos.aggregate.return_value = composition_cursor
        db.ProdutosServicos.find.return_value = products_cursor
        db.ProdutosServicosEmpresa.find.return_value = links_cursor
        db.Movimentacoes.aggregate.return_value = movement_cursor
        client = MagicMock(); client.__enter__.return_value.__getitem__.return_value = db
        with tempfile.TemporaryDirectory() as folder, \
             patch.dict(os.environ, {'INSIGHT_DATA_DIR': folder}), \
             patch.object(app, 'mongo_client', return_value=client), \
             patch.object(app, 'MODE', 'live'):
            response = composition.query_composition_purchases(request())
            result = response['result']
            self.assertEqual(result['module'], 'composition_purchases')
            self.assertEqual(result['compositions'][0]['possible_compositions'], 12)
            self.assertEqual(result['provenance']['source'], 'live')
            self.assertEqual(result['summary']['purchase_documents'], 1)
            self.assertTrue(response['run_id'])
            self.assertTrue((Path(folder) / 'insight.sqlite3').exists())
            self.assertEqual(store.get_run(response['run_id'])['result'], result)


class HTTPIntegration(unittest.TestCase):
    def test_report_page_and_local_assets_are_served(self):
        import app
        from fastapi.testclient import TestClient
        with TestClient(app.app) as client:
            response = client.get('/composition-purchases')
            self.assertEqual(response.status_code, 200)
            self.assertIn('/static/composition.js', response.text)
            for asset in ('composition.js', 'composition.css', 'light.css', 'fonts/InterVariable.woff2'):
                with self.subTest(asset=asset):
                    self.assertEqual(client.get('/static/' + asset).status_code, 200)

    def test_api_authentication_and_request_validation(self):
        import app
        from fastapi.testclient import TestClient
        with patch.object(app, 'INSIGHT_USER', 'fixture'), \
             patch.object(app, 'INSIGHT_PASSWORD', 'fixture-password'), \
             patch.object(app, 'mongo_client') as mongo, TestClient(app.app) as client:
            response = client.post('/api/workspace/composition/purchases', json={})
            self.assertEqual(response.status_code, 401)
            response = client.post('/api/workspace/composition/purchases', json={},
                                   auth=('fixture', 'fixture-password'))
            self.assertEqual(response.status_code, 422)
            mongo.assert_not_called()

    def test_api_dispatches_validated_purchase_request(self):
        import app
        import insight_workspace
        from fastapi.testclient import TestClient
        with patch.object(app, 'INSIGHT_USER', ''), \
             patch.object(insight_workspace, 'query_composition_purchases',
                          return_value={'run_id': 'fixture', 'result': {'rows': []}}) as query, \
             TestClient(app.app) as client:
            response = client.post('/api/workspace/composition/purchases',
                                   json=request().model_dump(mode='json'))
            self.assertEqual(response.status_code, 200)
            self.assertEqual(response.json()['run_id'], 'fixture')
            self.assertEqual(query.call_args.args[0].company_id, str(COMPANY))


if __name__ == '__main__':
    unittest.main()

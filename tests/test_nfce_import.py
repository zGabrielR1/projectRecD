"""Synthetic/offline checks only: no MongoDB or native installation is used."""
import base64
import copy
import json
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch, MagicMock

from bson import ObjectId, Int64, json_util
from fastapi import HTTPException

import insight_nfce as parser
import insight_nfce_mapping as mapping
import insight_nfce_api as api


def fixture():
    head = '35' + '2611' + '12345678000195' + '65' + '001' + '000000123' + '1' + '12345678'
    total = sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(head)))
    check = 11 - total % 11
    key = head + str(0 if check >= 10 else check)
    return f'''<?xml version="1.0" encoding="UTF-8"?>
<nfeProc xmlns="http://www.portalfiscal.inf.br/nfe" versao="4.00"><NFe><infNFe Id="NFe{key}">
<ide><mod>65</mod><serie>1</serie><nNF>123</nNF><dhEmi>2026-11-13T10:30:00-03:00</dhEmi><tpAmb>1</tpAmb></ide>
<emit><CNPJ>12345678000195</CNPJ><xNome>Emitente sintético</xNome></emit>
<det nItem="1"><prod><cProd>A</cProd><xProd>Produto sintético</xProd><NCM>12345678</NCM><CFOP>5102</CFOP><uCom>UN</uCom><qCom>3.0000</qCom><vUnCom>3.335</vUnCom><vProd>10.01</vProd><vDesc>0.01</vDesc><indTot>1</indTot></prod></det>
<total><ICMSTot><vProd>10.01</vProd><vNF>10.00</vNF></ICMSTot></total><pag><detPag><tPag>01</tPag><vPag>10.00</vPag></detPag></pag>
</infNFe></NFe><protNFe><infProt><tpAmb>1</tpAmb><chNFe>{key}</chNFe><cStat>100</cStat><nProt>123456789012345</nProt></infProt></protNFe></nfeProc>'''.encode()


def profile():
    value = json_util.loads(Path('static/nfce-profile.example.json').read_text(encoding='utf-8'))
    value.update(database='SyntheticOnly', issuer_cnpj='12345678000195', company_id=ObjectId('111111111111111111111111'))
    value['company'] = {'_t':'EmpresaHistorico', 'PessoaReferencia':value['company_id']}
    value['customer'] = {'_t':'FisicaHistorico', 'PessoaReferencia':ObjectId('222222222222222222222222')}
    value['products'] = {'A': {'reference':ObjectId('333333333333333333333333'),
        'snapshot':{'_t':'ProdutoHistorico'}, 'percentual':{'_t':'SyntheticPercentual','ValorSt':0.0},
        'ipi':{'_t':'SyntheticIPI','Valor':0.0}, 'pis':{'_t':'SyntheticPIS'}, 'cofins':{'_t':'SyntheticCOFINS'},
        'operacao_fiscal':{'_t':'SyntheticOperacao'}, 'estoque_movimentacao':{'_t':'SyntheticEstoque'}}}
    return value


class ParserTests(unittest.TestCase):
    def test_key_protocol_totals_and_byte_preservation(self):
        raw = fixture()
        value = parser.parse_xml(raw, issuer='12.345.678/0001-95')
        self.assertEqual(value['total'], '10.00')
        self.assertEqual(base64.b64decode(value['xml_base64']), raw)
        self.assertEqual(value['issued_at'].hour, 13)

    def test_rejects_wrong_issuer_environment_and_protocol(self):
        for raw, issuer, env in [(fixture(),'99999999999999','1'), (fixture(),'12345678000195','2'),
              (fixture().replace(b'<cStat>100',b'<cStat>101'),'12345678000195','1')]:
            with self.subTest(issuer=issuer, env=env), self.assertRaises(ValueError):
                parser.parse_xml(raw, issuer=issuer, environment=env)

    def test_rejects_dtd_utf16_and_invalid_xml(self):
        for raw in [b'<!DOCTYPE x [<!ENTITY e "x">]>'+fixture(), fixture().decode().encode('utf-16'), b'not XML']:
            with self.assertRaises(ValueError): parser.parse_xml(raw, issuer='12345678000195')

    def test_rejects_product_total_and_key_mismatch(self):
        for raw in [fixture().replace(b'<vProd>10.01', b'<vProd>20.00',1), fixture().replace(b'<nNF>123',b'<nNF>124')]:
            with self.assertRaises(ValueError): parser.parse_xml(raw, issuer='12345678000195')


class MappingTests(unittest.TestCase):
    def test_purchase_cost_prepares_update_in_existing_editor_journal(self):
        company, product, price_id = ObjectId(), ObjectId(), ObjectId()
        candidate = {'company_id': company, 'company_product_id': product,
                     'unit_cost': '3.335', 'conflict': False}
        links, prices = MagicMock(), MagicMock()
        links.find_one.return_value = {'PrecoReferencia': price_id}
        before = {'_id': price_id, 'Custo': {'Valor': 2.0}, 'Venda': {'Valor': 9.0}}
        prices.find_one.return_value = before
        with (patch.object(api.editor, 'session', return_value={'grants': []}),
              patch.object(api.editor, 'permitted', return_value=True),
              patch.object(api.editor, 'selected_collection', side_effect=[links, prices]),
              patch.object(api.editor, 'journal_preview', return_value={'id': 'preview'}) as journal):
            result = api.prepare_purchase_cost(candidate, 'SyntheticOnly', 'token', 'owner', client=object())
        self.assertEqual(result, {'id': 'preview'})
        args = journal.call_args.args
        self.assertEqual(args[:5], ('SyntheticOnly', 'Precos', 'update', price_id, before))
        self.assertEqual(args[5]['Custo']['Valor'], 3.335)
        self.assertEqual(args[5]['Venda'], before['Venda'])

    def test_purchase_cost_change_preserves_sale_price_and_native_cost_type(self):
        from bson import Decimal128
        for original, expected in [(2.0, 3.335), (Decimal128('2.00'), Decimal128('3.335'))]:
            price = {'_id': ObjectId(), 'Venda': {'Valor': 9.0},
                     'Custo': {'_t': 'Custo', 'Valor': original}}
            changed = mapping.purchase_cost_change(price, '3.335')
            self.assertEqual(changed['Custo'], {'_t': 'Custo', 'Valor': expected})
            self.assertEqual(changed['Venda'], price['Venda'])
            self.assertEqual(price['Custo']['Valor'], original)
            self.assertIsNone(mapping.purchase_cost_change(changed, '3.335'))

    def test_native_wall_clock_double_and_idempotent_identity(self):
        sale = mapping.build_sale(fixture(), profile())
        self.assertEqual(sale['movement']['DataHoraEmissao'].hour, 10)
        self.assertIsInstance(sale['movement']['Numero'], Int64)
        self.assertIsInstance(sale['movement']['ItensBase'][0]['Quantidade'], float)
        self.assertEqual(sale['movement']['_id'], mapping.build_sale(fixture(), profile())['movement']['_id'])
        self.assertEqual(sale['xml']['TipoMovimentacao'], 11)
        # Independent cent calculation: 3 * 3.335 = 10.005 -> 10.01; minus 0.01 = 10.
        self.assertEqual(sale['summary']['total'], '10.00')

    def test_blocks_missing_product_and_wrong_company(self):
        for field in ['product','company']:
            p = profile()
            if field == 'product': p['products']={}
            else: p['company']['PessoaReferencia']=ObjectId()
            with self.assertRaises(ValueError): mapping.build_sale(fixture(),p)

    def test_blocks_native_reconciliation_difference(self):
        p = profile(); p['item']['DescontoDigitado'] = 0
        with self.assertRaisesRegex(ValueError,'diverge'): mapping.build_sale(fixture(),p)

    def test_profile_cannot_redirect_identity(self):
        p=profile(); p['movement'].update(_id=ObjectId(), ChaveAcesso='wrong', Numero=999)
        sale=mapping.build_sale(fixture(),p)
        self.assertNotEqual(sale['movement']['_id'],p['movement']['_id'])
        self.assertEqual(sale['movement']['Numero'],123)

    def test_binding_rejects_ambiguous_paths_and_code(self):
        import xml.etree.ElementTree as ET
        node=ET.fromstring('<a xmlns="http://www.portalfiscal.inf.br/nfe"><v>1</v><v>2</v></a>')
        with self.assertRaisesRegex(ValueError,'ambíguo'): mapping.render({'$bind':'xml:v'}, {}, node)
        with self.assertRaises(ValueError): mapping.render({'$bind':'xml:../bad'}, {}, node)

    def test_starter_profile_does_not_enable_import(self):
        with self.assertRaisesRegex(ValueError,'company_id'): mapping.load_profile(Path('static/nfce-profile.example.json').read_text(encoding='utf-8'))

    def test_blocks_unmapped_nonzero_tax_and_wrong_payment(self):
        raw=fixture().replace(b'</prod></det>',b'</prod><imposto><ICMS><ICMS00><vBC>10.00</vBC><vICMS>1.80</vICMS></ICMS00></ICMS></imposto></det>')
        with self.assertRaisesRegex(ValueError,'fiscal.*binding'):mapping.build_sale(raw,profile())
        p=profile();p['payments']['01']['Valor']=9.0
        with self.assertRaisesRegex(ValueError,'pagamentos nativos'):mapping.build_sale(fixture(),p)

    def test_payment_input_and_tax_path_groups(self):
        with self.assertRaises(ValueError):mapping.build_sale(fixture().replace(b'<vPag>10.00',b'<vPag>bad'),profile())
        self.assertFalse(mapping.matches_path('imposto/ICMS/*/vBC','imposto/PIS/PISAliq/vBC'))
        self.assertTrue(mapping.matches_path('imposto/ICMS/*/vBC','imposto/ICMS/ICMS00/vBC'))


def product_template_profile():
    value = profile()
    value['products'] = {}
    value['product_template'] = {
        'product': {'_t': 'Produto', 'Ativo': True, 'CodigoInterno': {'$bind': 'code'},
                    'Descricao': {'$bind': 'xml:prod/xProd'}},
        'company': {'_t': 'ProdutoEmpresa', 'Ativo': True, 'Vendavel': True},
        'price': {'_t': 'Preco', 'Custo': {'Valor': {'$bind': 'xml:prod/vUnCom', 'type': 'double'}},
                  'Venda': {'Valor': 0.0}},
        'stock': {'_t': 'Estoque'},
        'stock_quantity': {'_t': 'EstoqueQuantidade'},
        'mapping': {
            'snapshot': {'_t': 'ProdutoHistorico',
                         'ProdutoServicoReferencia': {'$bind': 'generated.product_id', 'type': 'objectId'}},
            'percentual': {'_t': 'SyntheticPercentual', 'ValorSt': 0.0},
            'ipi': {'_t': 'SyntheticIPI', 'Valor': 0.0},
            'pis': {'_t': 'SyntheticPIS'}, 'cofins': {'_t': 'SyntheticCOFINS'},
            'operacao_fiscal': {'_t': 'SyntheticOperacao'},
            'estoque_movimentacao': {'_t': 'SyntheticEstoque'},
        },
    }
    return value


def finance_profile(kind='receivable'):
    value = profile()
    collection, native_type = (('Recebimentos', 'Recebimento') if kind == 'receivable'
                               else ('Pagamentos', 'Pagamento'))
    value['finance'] = {kind: {
        'collection': collection, 'type': native_type,
        'parcela': {
            '_t': 'Parcela' + native_type, 'Descricao': 'Duplicata do XML',
            'Situacao': {'_t': 'Pendente', 'Codigo': 1},
            'ContaReferencia': ObjectId('555555555555555555555555'),
            'Historico': [{'_t': 'HistoricoAguardando',
                           'Valor': {'$bind': 'installment.amount', 'type': 'double'},
                           'EspeciePagamento': {'_t': 'Boleto'},
                           'ContaReferencia': ObjectId('555555555555555555555555')}],
        },
    }}
    return value


def billed_fixture(amount='10.00'):
    billing = (f'<cobr><fat><vLiq>{amount}</vLiq></fat><dup><nDup>001</nDup>'
               f'<dVenc>2026-12-01</dVenc><vDup>{amount}</vDup></dup></cobr>').encode()
    return fixture().replace(b'<pag>', billing + b'<pag>')


class ParityTests(unittest.TestCase):
    def test_product_template_generates_company_link_price_and_stock(self):
        sale = mapping.build_sale(fixture(), product_template_profile())
        registration = sale['product_registrations'][0]
        documents = registration['documents']
        self.assertEqual(set(documents), {'Estoques', 'EstoquesQuantidade', 'ProdutosServicos',
                                          'Precos', 'ProdutosServicosEmpresa'})
        product, link = documents['ProdutosServicos'], documents['ProdutosServicosEmpresa']
        self.assertEqual(link['ProdutoServicoReferencia'], product['_id'])
        self.assertEqual(link['PrecoReferencia'], documents['Precos']['_id'])
        self.assertEqual(link['EstoqueReferencia'], documents['Estoques']['_id'])
        self.assertEqual(documents['EstoquesQuantidade']['EstoqueReferencia'], documents['Estoques']['_id'])
        self.assertEqual(link['EmpresaReferencia'], product_template_profile()['company_id'])
        self.assertEqual(documents['ProdutosServicos']['CodigoInterno'], 'A')
        self.assertEqual(documents['Precos']['Custo']['Valor'], 3.335)
        item = sale['movement']['ItensBase'][0]
        self.assertEqual(item['ProdutoServicoEmpresaReferencia'], link['_id'])
        self.assertEqual(item['ProdutoServico']['ProdutoServicoReferencia'], product['_id'])

    def test_generated_product_uses_xml_barcode_when_selected(self):
        barcode = '0789123456789'
        raw = fixture().replace(b'<cProd>A</cProd>', f'<cProd>A</cProd><cEAN>{barcode}</cEAN>'.encode())
        config = product_template_profile()
        sale = mapping.build_sale(raw, config, use_barcode=True)
        product = sale['product_registrations'][0]['documents']['ProdutosServicos']
        self.assertEqual(product['CodigoBarras'], barcode)
        config['product_template']['product']['CodigoBarras'] = '7891234567890'
        with self.assertRaisesRegex(ValueError, 'cadastro diverge do XML'):
            mapping.build_sale(raw, config, use_barcode=True)

    def test_repeated_product_lines_share_one_generated_registration(self):
        raw = fixture()
        start = raw.index(b'<det nItem=')
        end = raw.index(b'</det>', start) + len(b'</det>')
        second = raw[start:end].replace(b'nItem="1"', b'nItem="2"')
        raw = (raw[:end] + second + raw[end:]
               .replace(b'<vProd>10.01', b'<vProd>20.02')
               .replace(b'<vNF>10.00', b'<vNF>20.00')
               .replace(b'<vPag>10.00', b'<vPag>20.00'))
        sale = mapping.build_sale(raw, product_template_profile())
        self.assertEqual(len(sale['product_registrations']), 1)
        items = sale['movement']['ItensBase']
        self.assertEqual(len(items), 2)
        self.assertEqual(items[0]['ProdutoServicoEmpresaReferencia'], items[1]['ProdutoServicoEmpresaReferencia'])

    def test_finance_template_embeds_parcelas_and_uses_pending_state(self):
        sale = mapping.build_sale(billed_fixture(), finance_profile())
        finance = sale['finance']
        self.assertEqual(finance['collection'], 'Recebimentos')
        self.assertEqual(len(finance['documents']), 1)
        parcela = finance['documents'][0]
        self.assertEqual(parcela['Ordem'], 1)
        self.assertEqual(parcela['Documento'], '001')
        self.assertEqual(parcela['Vencimento'].strftime('%Y-%m-%d'), '2026-12-01')
        self.assertEqual(parcela['PessoaReferencia'], ObjectId('222222222222222222222222'))
        self.assertEqual(parcela['EmpresaReferencia'], finance_profile()['company_id'])
        self.assertEqual(sale['movement']['PagamentoRecebimento'],
                         {'_t': 'Recebimento', 'Parcelas': [parcela]})
        self.assertEqual(mapping.build_sale(billed_fixture(), finance_profile())['finance'],
                         finance)

    def test_finance_requires_pending_state_and_due_date(self):
        changed = finance_profile()
        changed['finance']['receivable']['parcela']['Situacao'] = {'_t': 'Quitada', 'Codigo': 2}
        with self.assertRaisesRegex(ValueError, 'Pendente'):
            mapping.build_sale(billed_fixture(), changed)
        raw = billed_fixture().replace(b'<dVenc>2026-12-01</dVenc>', b'<dVenc></dVenc>')
        with self.assertRaisesRegex(ValueError, 'vencimento'):
            mapping.build_sale(raw, finance_profile())

    def test_apply_writes_registrations_before_movement_and_finance_after(self):
        sale = mapping.build_sale(billed_fixture(), finance_profile())
        sale.update(product_registrations=mapping.build_sale(
            fixture(), product_template_profile())['product_registrations'])
        coll = MagicMock(); coll.find_one.return_value = None
        with patch.object(api.editor, 'editor_client'), \
             patch.object(api.editor, 'selected_collection', return_value=coll), \
             patch.object(api.editor, 'prepare_import_insert', side_effect=lambda document, *a, **k: {'id': str(document['_id'])}), \
             patch.object(api.editor, 'apply_change') as apply_change:
            result = api.apply_sale(sale, 'SyntheticOnly', 'token', 'owner')
        self.assertEqual(result['status'], 'imported', result)
        collections = [call.args[3] for call in apply_change.call_args_list]
        self.assertEqual(collections, ['Estoques', 'EstoquesQuantidade', 'ProdutosServicos', 'Precos',
                                       'ProdutosServicosEmpresa', 'XmlMovimentacoes',
                                       'Movimentacoes', 'Recebimentos'])
        self.assertEqual(len(result['changes']), 8)

    def test_apply_accepts_identical_prior_documents_and_blocks_divergence(self):
        sale = mapping.build_sale(billed_fixture(), finance_profile())
        parcela = sale['finance']['documents'][0]
        coll = MagicMock()
        coll.find_one.side_effect = lambda query, *a, **k: (
            parcela if isinstance(query, dict) and query.get('_id') == parcela['_id'] else None)
        with patch.object(api.editor, 'editor_client'), \
             patch.object(api.editor, 'selected_collection', return_value=coll), \
             patch.object(api.editor, 'prepare_import_insert', side_effect=lambda document, *a, **k: {'id': 'x'}), \
             patch.object(api.editor, 'apply_change'):
            result = api.apply_sale(sale, 'SyntheticOnly', 'token', 'owner')
        self.assertEqual(result['status'], 'imported', result)
        coll.find_one.side_effect = lambda query, *a, **k: (
            dict(parcela, Ordem=9) if isinstance(query, dict) and query.get('_id') == parcela['_id'] else None)
        with patch.object(api.editor, 'editor_client'), \
             patch.object(api.editor, 'selected_collection', return_value=coll), \
             patch.object(api.editor, 'prepare_import_insert'), \
             patch.object(api.editor, 'apply_change'):
            result = api.apply_sale(sale, 'SyntheticOnly', 'token', 'owner')
        self.assertEqual(result['status'], 'stopped')
        self.assertIn('diferente', result['message'])


class ApiTests(unittest.TestCase):
    def setUp(self):
        api._plans.clear()
        self.fingerprint=patch.object(api.editor,'fingerprint',return_value='synthetic-connection');self.fingerprint.start()

    def tearDown(self): self.fingerprint.stop();api._plans.clear()

    def make_plan(self, files=None):
        return api.make_plan({'profile':json_util.dumps(profile()),'files':files or [{'name':'synthetic.xml','base64':base64.b64encode(fixture()).decode()}]},'owner')

    def test_preview_no_database_access_and_actor_bound(self):
        with patch.object(api.editor,'editor_client',side_effect=AssertionError('No Mongo allowed')):
            plan=self.make_plan()
        self.assertTrue(plan['valid'])
        with self.assertRaises(HTTPException):api.plan_for(plan['id'],'other')

    def test_invalid_lot_cannot_export(self):
        plan=self.make_plan([{'name':'bad.xml','base64':'!!!'}])
        self.assertFalse(plan['valid'])

    def test_connection_change_invalidates_plan(self):
        plan=self.make_plan()
        with patch.object(api.editor,'fingerprint',return_value='changed'), self.assertRaises(HTTPException):
            api.plan_for(plan['id'],'owner')

    def test_duplicate_lot_is_one_sale(self):
        entry={'name':'same.xml','base64':base64.b64encode(fixture()).decode()}
        plan=self.make_plan([entry,entry]);self.assertEqual(plan['ready'],1)
        self.assertEqual(plan['rows'][1]['status'],'duplicate')

    def test_apply_checks_duplicate_and_consumes_plan(self):
        plan=self.make_plan()
        key = parser.parse_xml(fixture(), issuer='12345678000195')['access_key']
        coll = MagicMock()
        coll.find_one.side_effect = lambda query, *args, **kwargs: (
            {'_id': ObjectId(), 'ChaveAcesso': key} if {'ChaveAcesso': key} in query.get('$or', []) else None)
        with patch.object(api.editor,'session',return_value={'grants':[]}), patch.object(api.editor,'permitted',return_value=True), patch.object(api.editor,'editor_client'), patch.object(api.editor,'selected_collection',return_value=coll), patch.object(api.editor,'apply_change') as write:
            result=api.apply_plan(plan['id'],'IMPORTAR SyntheticOnly','token','owner')
            self.assertEqual(result['results'][0]['status'],'duplicate');write.assert_not_called()
            with self.assertRaises(HTTPException):api.apply_plan(plan['id'],'IMPORTAR SyntheticOnly','token','owner')

    def test_partial_write_failure_stops_batch_and_reports_journal(self):
        plan=self.make_plan();coll=MagicMock();coll.find_one.return_value=None
        with patch.object(api.editor,'session',return_value={'grants':[]}), patch.object(api.editor,'permitted',return_value=True), patch.object(api.editor,'editor_client'), patch.object(api.editor,'selected_collection',return_value=coll), patch.object(api.editor,'prepare_import_insert',side_effect=[{'id':'xml-change'},{'id':'sale-change'}]), patch.object(api.editor,'apply_change',side_effect=[None,RuntimeError('network')]):
            result=api.apply_plan(plan['id'],'IMPORTAR SyntheticOnly','token','owner')
        self.assertEqual(result['results'][0]['status'],'stopped')
        self.assertEqual(result['results'][0]['changes'],['xml-change','sale-change'])

    def test_http_preview_and_zip_export_without_mongo(self):
        import io
        import zipfile
        from fastapi import FastAPI
        from fastapi.testclient import TestClient
        server=FastAPI();server.include_router(api.router)
        server.dependency_overrides[api.editor.same_origin]=lambda:None
        client=TestClient(server)
        payload={'profile':json_util.dumps(profile()),'files':[{'name':'synthetic.xml','base64':base64.b64encode(fixture()).decode()}]}
        with patch.object(api.editor,'editor_client',side_effect=AssertionError('No Mongo allowed')):
            response=client.post('/api/nfce-import/preview',json=payload)
            self.assertEqual(response.status_code,200)
            exported=client.post('/api/nfce-import/export/'+response.json()['id'])
        self.assertEqual(exported.status_code,200)
        with zipfile.ZipFile(io.BytesIO(exported.content)) as archive:
            self.assertEqual(set(archive.namelist()),{'Movimentacoes.json','XmlMovimentacoes.json','LEIA-ME.txt'})
            document=json_util.loads(archive.read('Movimentacoes.json'))[0]
            self.assertIsInstance(document['Numero'],Int64)
            self.assertIsInstance(document['EmpresaReferencia'],ObjectId)

    def test_http_invalid_json_and_wrong_confirmation(self):
        from fastapi import FastAPI
        from fastapi.testclient import TestClient
        server=FastAPI();server.include_router(api.router)
        server.dependency_overrides[api.editor.same_origin]=lambda:None
        client=TestClient(server)
        self.assertEqual(client.post('/api/nfce-import/preview',content='x'*101).status_code,422)
        plan=self.make_plan()
        with patch.object(api.editor,'session',return_value={'grants':[]}), self.assertRaises(HTTPException):
            api.apply_plan(plan['id'],'IMPORTAR Wrong','token','owner')
        self.assertIn(plan['id'],api._plans)


if __name__=='__main__': unittest.main()

"""Read-only database doubles: discovery must never write or borrow transaction state."""
from copy import deepcopy
from pathlib import Path
import base64
import hashlib
import sys
import unittest
from unittest.mock import patch
from xml.etree import ElementTree as ET

from bson import ObjectId
from fastapi import HTTPException

sys.path.insert(0, str(Path(__file__).resolve().parent))
from test_nfce_import import fixture, profile
from insight_nfce import NS
from insight_nfce_mapping import build_sale, build_cfe, profile_for_source
from insight_nfce_discovery import Discovery, prepare_files, customer_matches, nfe_route, parse_cfe
import insight_nfce_api as api


def xml(number=123):
    raw=fixture().replace(b'<tpAmb>1</tpAmb></ide>', b'<tpAmb>1</tpAmb><finNFe>1</finNFe><tpNF>1</tpNF><indPres>1</indPres></ide>')
    raw=raw.replace(b'</prod></det>', b'</prod><imposto><ICMS><ICMSSN102><orig>0</orig><CSOSN>102</CSOSN></ICMSSN102></ICMS><PIS><PISNT><CST>07</CST></PISNT></PIS><COFINS><COFINSNT><CST>07</CST></COFINSNT></COFINS></imposto></det>')
    raw=raw.replace(b'<pag>',b'<transp><modFrete>9</modFrete></transp><pag>')
    if number != 123:
        root=ET.fromstring(raw)
        key=root.find('n:NFe/n:infNFe',NS).get('Id')[3:]
        head=key[:25]+str(number).zfill(9)+key[34:43]
        remainder=11-sum(int(n)*(2+i%8) for i,n in enumerate(reversed(head)))%11
        updated=head+str(0 if remainder>=10 else remainder)
        raw=raw.replace(key.encode(),updated.encode()).replace(b'<nNF>123</nNF>',f'<nNF>{number}</nNF>'.encode())
    return raw


def matches(doc, query):
    for field, expected in query.items():
        actual=doc
        for part in field.split('.'):
            actual=actual.get(part) if isinstance(actual,dict) else None
        if isinstance(expected,dict) and '$in' in expected:
            if actual not in expected['$in']:return False
        elif isinstance(actual,list):
            if expected not in actual:return False
        elif expected != actual:return False
    return True


class Cursor:
    def __init__(self,rows):self.rows=rows
    def limit(self,n):self.rows=self.rows[:n];return self
    def batch_size(self,n):return self
    def sort(self,*args):return self
    def max_time_ms(self,n):return self
    def close(self):pass
    def __iter__(self):return iter(deepcopy(self.rows))


class Collection:
    def __init__(self,rows):self.rows=deepcopy(rows);self.reads=0
    def find(self,query,projection=None):
        self.reads+=1;return Cursor([r for r in self.rows if matches(r,query)])
    def find_one(self,query,projection=None,**kwargs):
        self.reads+=1;return next((deepcopy(r) for r in self.rows if matches(r,query)),None)
    def insert_one(self,*a,**kw):raise AssertionError('Discovery attempted a database write')
    update_one=insert_one
    delete_one=insert_one


class Database:
    def __init__(self):
        p=profile();p['company']['Documento']=p['issuer_cnpj']
        p['customer']={'_t':'ConsumidorHistorico','PessoaReferencia':ObjectId('000000000000000000000000'),'Documento':''}
        source=build_sale(xml(),p)['movement']
        source.update(Versao='source-version',ModalidadeFrete={'_t':'SemFrete'},IndicadorPresencaComprador={'_t':'Presencial'},
                      Modelo={'_t':'NotaFiscalConsumidorEletronica','Codigo':65},
                      PagamentoRecebimento={'_t':'Recebimento','Parcelas':[{'private_old_reference':'DO_NOT_COPY'}]},
                      IdPedidoMobile='DO_NOT_COPY',Convenio={'private_old_reference':'DO_NOT_COPY'})
        item=source['ItensBase'][0]
        product_id=ObjectId('444444444444444444444444')
        item['ProdutoServico'].update(CodigoInterno='A',ProdutoServicoReferencia=product_id)
        item['EstoqueMovimentacao']={'_t':'EstoqueMovimentacao','_id':ObjectId(),'Quantidade':3.0,'Itens':[],'Quantidades':[]}
        item['Vendedor']={'private_old_reference':'DO_NOT_COPY'}
        self.Pessoas=Collection([{'_id':p['company_id'],'Cnpj':p['issuer_cnpj'],'Ativo':True}])
        self.Movimentacoes=Collection([source])
        self.XmlMovimentacoes=Collection([{'_id':ObjectId(),'ChaveAcesso':source['ChaveAcesso'],
                                          'EventoCancelamento':False,'Xml':xml().decode()}])
        self.ProdutosServicosEmpresa=Collection([{'_id':item['ProdutoServicoEmpresaReferencia'],'EmpresaReferencia':p['company_id'],
                                                  'ProdutoServicoReferencia':product_id,'Ativo':True,'Versao':'link-version'}])
        self.ProdutosServicos=Collection([{'_id':product_id,'Ativo':True,'CodigoInterno':'A','Versao':'product-version'}])
    def __getitem__(self,key):return getattr(self,key)
    def list_collection_names(self,**kwargs):return ['Pessoas','Movimentacoes','XmlMovimentacoes','ProdutosServicos','ProdutosServicosEmpresa']


def prepared(raw=None):return prepare_files([('incoming.xml',raw or xml(124))])[0][0]


class DiscoveryTests(unittest.TestCase):
    def test_purchase_duplicate_query_scopes_number_to_supplier(self):
        company, supplier = ObjectId(), ObjectId()
        query = api.native_document_filter({
            '_t': ['Movimentacao', 'NotaFiscalCompra'], 'EmpresaReferencia': company,
            'Numero': 123, 'Serie': '1', 'ChaveAcesso': 'purchase-key',
            'Pessoa': {'PessoaReferencia': supplier},
        })
        self.assertEqual(query, {
            'EmpresaReferencia': company, '_t': 'NotaFiscalCompra',
            '$or': [{'ChaveAcesso': 'purchase-key'},
                    {'Numero': 123, 'Serie': '1', 'Pessoa.PessoaReferencia': supplier}],
        })

    def test_installments_are_read_from_billing_without_inventing_settlement(self):
        billing = (b'<cobr><dup><nDup>001</nDup><dVenc>2026-12-01</dVenc>'
                   b'<vDup>12.30</vDup></dup></cobr>')
        raw = xml(124).replace(b'<pag>', billing + b'<pag>')
        self.assertEqual(prepared(raw)['parsed']['installments'],
                         [{'number': '001', 'date': '2026-12-01', 'amount': '12.30'}])
        bad = raw.replace(b'2026-12-01', b'2026-02-30')
        accepted, rejected = prepare_files([('invalid.xml', bad)])
        self.assertFalse(accepted)
        self.assertIn('vencimento', rejected[0]['message'])
        bad_total = raw.replace(b'<cobr>', b'<cobr><fat><vLiq>12.31</vLiq></fat>')
        accepted, rejected = prepare_files([('divergente.xml', bad_total)])
        self.assertFalse(accepted)
        self.assertIn('valor líquido', rejected[0]['message'])

    def test_sat_payment_and_change_match_receipt_total(self):
        cnpj, serial, number = '12345678000190', '000000123', '000456'
        head = '352611' + cnpj + '59' + serial + number + '123456'
        remainder = 11 - sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(head))) % 11
        key = head + str(0 if remainder >= 10 else remainder)
        raw = (f'<CFe><infCFe Id="CFe{key}"><ide><nserieSAT>{serial}</nserieSAT>'
               f'<nCFe>{number}</nCFe><dEmi>20261124</dEmi><hEmi>102030</hEmi></ide><emit><CNPJ>{cnpj}</CNPJ></emit>'
               '<det nItem="1"><prod><cProd>A</cProd><qCom>3</qCom><vUnCom>5.00</vUnCom>'
               '<vProd>15.00</vProd></prod></det><total><ICMSTot><vProd>15.00</vProd></ICMSTot>'
               '<vCFe>15.00</vCFe></total><pgto><MP><cMP>01</cMP><vMP>20.00</vMP>'
               '</MP><vTroco>5.00</vTroco></pgto></infCFe></CFe>')
        parsed = parse_cfe(ET.fromstring(raw))
        self.assertEqual(parsed['change'], '5.00')
        self.assertEqual(parsed['issued_at_original'], '2026-11-24T10:20:30')
        with self.assertRaisesRegex(ValueError, 'Mês/ano'):
            parse_cfe(ET.fromstring(raw.replace('20261124', '20261024')))
        self.assertEqual(parsed['payments'], [{'code': '01', 'amount': '20.00'}])
        self.assertEqual(parsed['items'][0]['gross'], '15.00')
        cfg = profile()
        cfg['issuer_cnpj'] = cnpj
        cfg['cfe'] = {
            'xml_movement_type': 123,
            'movement': {'_t': 'CupomFiscalEletronico', 'Empresa': cfg['company'],
                         'Situacao': {'_t': 'Concluido', 'Codigo': 2},
                         'Ativo': True, 'MovimentacaoItemFinanceiro': False},
            'item': {'_t': 'ItemProdutoSaida',
                     'Quantidade': {'$bind': 'item.quantity', 'type': 'double'},
                     'PrecoUnitario': {'$bind': 'item.unit_price', 'type': 'double'},
                     'ProdutoServicoEmpresaReferencia': {'$bind': 'product.reference'}},
            'payments': {'01': {'Valor': 15.0}},
        }
        sale = build_cfe(raw.encode(), cfg)
        batch_profile = profile()
        batch_profile['source_profiles'] = {'cfe': {'issuer_cnpj': cnpj, 'cfe': cfg['cfe']}}
        self.assertEqual(build_cfe(raw.encode(), profile_for_source(batch_profile, 'cfe')), sale)
        self.assertEqual(sale['movement']['NumeroFabricacaoSat'], serial)
        self.assertEqual(sale['movement']['ChaveAcesso'], key)
        self.assertEqual(sale['xml']['Xml'], raw)
        cfg['cfe']['item']['Quantidade'] = 2.0
        with self.assertRaisesRegex(ValueError, 'Quantidade'):
            build_cfe(raw.encode(), cfg)
        with self.assertRaisesRegex(ValueError, 'total bruto'):
            parse_cfe(ET.fromstring(raw.replace('<vProd>15.00', '<vProd>14.00', 1)))
        with self.assertRaisesRegex(ValueError, 'troco'):
            parse_cfe(ET.fromstring(raw.replace('<vTroco>5.00', '<vTroco>4.00')))

    def test_purchase_reverses_parties_but_self_issued_entry_does_not(self):
        issuer, recipient = '12345678000190', '98765432000110'
        parsed = {'model': '55', 'direction': '1', 'issuer_cnpj': issuer,
                  'recipient_document': recipient}
        self.assertEqual(nfe_route(parsed, 'purchase'), ('NotaFiscalCompra', recipient, issuer))
        self.assertEqual(nfe_route(dict(parsed, direction='0'), 'incoming'),
                         ('NotaFiscalEletronicaEntrada', issuer, recipient))
        with self.assertRaises(ValueError):
            nfe_route(parsed, 'incoming')

    def test_barcode_resolves_different_code_and_rejects_ambiguous_catalog(self):
        db = Database()
        barcode = '0789123456789'
        tag = f'<cEAN>{barcode}</cEAN>'.encode()
        db.XmlMovimentacoes.rows[0]['Xml'] = xml().replace(b'</cProd>', b'</cProd>' + tag).decode()
        product = db.ProdutosServicos.rows[0]
        product['CodigoBarras'] = barcode
        raw = xml(124).replace(b'<cProd>A</cProd>', b'<cProd>OTHER</cProd>' + tag)
        incoming = prepared(raw)
        sale = Discovery(db, use_barcode=True).build(incoming)
        self.assertEqual(sale['movement']['ItensBase'][0]['ProdutoServico']['ProdutoServicoReferencia'],
                         product['_id'])
        db.ProdutosServicos.rows.append(dict(product, _id=ObjectId()))
        with self.assertRaisesRegex(ValueError, 'sem vínculo único'):
            Discovery(db, use_barcode=True).build(incoming)

    def test_automatic_sale_has_new_identity_and_no_old_financial_or_seller_state(self):
        db=Database();sale=Discovery(db).build(prepared())
        self.assertEqual(sale['movement']['Numero'],124)
        self.assertNotEqual(sale['movement']['_id'],db.Movimentacoes.rows[0]['_id'])
        self.assertEqual(sale['movement']['PagamentoRecebimento']['Parcelas'],[])
        self.assertNotIn('DO_NOT_COPY',str(sale))
        self.assertEqual(sale['movement']['DataHoraEmissao'].hour,10)
        self.assertTrue(sale['guards'])

    def test_standalone_signed_xml_is_skipped_only_for_identical_authorized_xml(self):
        raw=xml(124);root=ET.fromstring(raw)
        signed=ET.tostring(root.find('n:NFe',NS))
        ready,rows=prepare_files([('signed.xml',signed),('authorized.xml',raw)])
        self.assertEqual(len(ready),1);self.assertEqual(rows[0]['status'],'duplicate')
        ready,rows=prepare_files([('signed.xml',signed)])
        self.assertFalse(ready);self.assertEqual(rows[0]['status'],'invalid')

    def test_mismatched_signed_copy_is_blocked(self):
        raw=xml(124);root=ET.fromstring(raw)
        signed=ET.tostring(root.find('n:NFe',NS)).replace(b'Produto sint',b'Changed sint')
        ready,rows=prepare_files([('signed.xml',signed),('authorized.xml',raw)])
        self.assertEqual(len(ready),1);self.assertEqual(rows[0]['status'],'invalid')

    def test_no_matching_issuer_is_cached_and_does_not_infer_company(self):
        db=Database();db.Pessoas.rows=[];discovery=Discovery(db)
        for _ in range(3):
            with self.assertRaisesRegex(ValueError,'Emitente'):discovery.build(prepared())
        self.assertEqual(db.Pessoas.reads,1)

    def test_ambiguous_company_and_no_native_examples(self):
        for missing in [False,True]:
            db=Database()
            if missing:db.Movimentacoes.rows=[]
            else:db.Pessoas.rows.append(deepcopy(db.Pessoas.rows[0]))
            with self.subTest(missing=missing),self.assertRaises(ValueError):Discovery(db).build(prepared())

    def test_named_customer_not_borrowed_for_anonymous_xml(self):
        db=Database();db.Movimentacoes.rows[0]['Pessoa']={'_t':'FisicaHistorico','Documento':'12345678900'}
        with self.assertRaisesRegex(ValueError,'Destinatário'):Discovery(db).build(prepared())

    def test_product_company_link_and_lot_stock_are_checked(self):
        for kind in ['link','stock','inactive']:
            db=Database()
            if kind=='link':db.ProdutosServicosEmpresa.rows[0]['EmpresaReferencia']=ObjectId()
            if kind=='stock':db.Movimentacoes.rows[0]['ItensBase'][0]['EstoqueMovimentacao']['Itens']=[{'lot':'old'}]
            if kind=='inactive':db.ProdutosServicos.rows[0]['Ativo']=False
            with self.subTest(kind=kind),self.assertRaises(ValueError):Discovery(db).build(prepared())

    def test_tef_history_and_unmapped_payment_are_not_copied(self):
        for kind in ['tef','payment']:
            db=Database()
            if kind=='tef':db.Movimentacoes.rows[0]['ItensPagamentos'][0]['InformacoesTransacao']=[{'NSU':'old'}]
            raw=xml(124).replace(b'<tPag>01',b'<tPag>03') if kind=='payment' else xml(124)
            with self.subTest(kind=kind),self.assertRaises(ValueError):Discovery(db).build(prepared(raw))

    def test_new_nonzero_tax_not_supported(self):
        raw=xml(124).replace(b'</ICMSSN102>',b'<vICMS>1.00</vICMS></ICMSSN102>')
        with self.assertRaisesRegex(ValueError,'Tributos'):Discovery(Database()).build(prepared(raw))

    def test_api_automatic_mode_requires_no_profile_or_file_export(self):
        import app
        db=Database()
        class Client:
            def __enter__(self):return self
            def __exit__(self,*args):pass
            def __getitem__(self,name):return db
        body={'mode':'automatic','files':[{'name':'incoming.xml','base64':base64.b64encode(xml(124)).decode()}]}
        with patch.object(app,'MONGO_DATABASE','SyntheticOnly'),patch.object(api.editor,'editor_client',return_value=Client()),patch.object(api.editor,'fingerprint',return_value='offline'):
            plan=api.make_plan(body,'owner')
            self.assertTrue(plan['valid']);self.assertEqual(plan['ready'],1)
            self.assertEqual(plan['discovery']['database_writes'],0)
            self.assertEqual(plan['discovery']['companies'][0]['paired_xmls'],1)
            with patch.object(app,'MONGO_DATABASE','Other'),self.assertRaises(HTTPException):api.plan_for(plan['id'],'owner')
        api._plans.clear()

    def test_reference_changed_without_version_change_blocks_before_first_write(self):
        import app
        db=Database()
        class Client:
            def __enter__(self):return self
            def __exit__(self,*args):pass
            def __getitem__(self,name):return db
        body={'mode':'automatic','files':[{'name':'incoming.xml','base64':base64.b64encode(xml(124)).decode()}]}
        with patch.object(app,'MONGO_DATABASE','SyntheticOnly'),patch.object(api.editor,'editor_client',return_value=Client()),patch.object(api.editor,'fingerprint',return_value='offline'):
            plan=api.make_plan(body,'owner')
            db.ProdutosServicosEmpresa.rows[0]['EmpresaReferencia']=ObjectId()
            with patch.object(api.editor,'session',return_value={'grants':[]}),patch.object(api.editor,'permitted',return_value=True),patch.object(api.editor,'selected_collection',side_effect=lambda c,d,n,**kw:db[n]),patch.object(api.editor,'prepare_import_insert') as writes:
                result=api.apply_plan(plan['id'],'IMPORTAR SyntheticOnly','token','owner')
                self.assertEqual(result['results'][0]['status'],'stopped')
                writes.assert_not_called()
        api._plans.clear()


if __name__=='__main__':unittest.main()

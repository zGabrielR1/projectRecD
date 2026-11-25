"""Real import writes on a new disposable loopback MongoDB; never a customer DB."""
from contextlib import ExitStack
from datetime import date
import os
from pathlib import Path
import socket
import subprocess
import tempfile
import time
import unittest
from unittest.mock import patch
from xml.etree import ElementTree as ET

from dotenv import dotenv_values
from bson import ObjectId, json_util
from fastapi import HTTPException
from pymongo import MongoClient
from pymongo.errors import ConnectionFailure

import app
import insight_connection as connection
import insight_editor as editor
import insight_finance as finance
from insight_nfce import NS
import insight_nfce_jobs as jobs
from insight_nfce_jobs_api import prepare_cost
from test_xml_release import mixed_database, nfe
from test_nfce_discovery import xml
from test_nfce_import import finance_profile, product_template_profile, profile


class XmlIsolatedMongoTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        binary = Path(os.environ.get('INSIGHT_TEST_MONGOD', r'C:\Digisat\SuiteG6\MongoDB\bin\mongod.exe'))
        if not binary.is_file():
            raise unittest.SkipTest('Requires an isolated local mongod binary.')
        root = Path(__file__).resolve().parents[1] / '.release-build'
        root.mkdir(exist_ok=True)
        cls.folder = tempfile.TemporaryDirectory(prefix='xml-isolated-', dir=root)
        cls.path = Path(cls.folder.name).resolve()
        if not cls.path.is_relative_to(root.resolve()):
            raise RuntimeError('Refusing fixture outside workspace')
        with socket.socket() as sock:
            sock.bind(('127.0.0.1', 0));cls.port = sock.getsockname()[1]
        if cls.port in {12220, 12231}:
            raise RuntimeError('Refusing SuiteG6 port')
        cls.uri = f'mongodb://127.0.0.1:{cls.port}/?directConnection=true'
        cls.process = subprocess.Popen([str(binary), '--dbpath', str(cls.path), '--port', str(cls.port),
            '--bind_ip', '127.0.0.1', '--wiredTigerCacheSizeGB', '0.25', '--auth',
            '--logpath', str(cls.path / 'mongo.log')], stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL,
            creationflags=subprocess.CREATE_NO_WINDOW if os.name == 'nt' else 0)
        cls.client = MongoClient(cls.uri, serverSelectionTimeoutMS=300)
        try:
            deadline = time.monotonic() + 30
            while True:
                try: cls.client.admin.command('ping');break
                except ConnectionFailure:
                    if cls.process.poll() is not None or time.monotonic() > deadline:
                        raise RuntimeError('Isolated Mongo failed to start')
            cls.secret = "synthetic '$hash#\\${literal}"
            # The localhost exception permits creation of the first admin only.
            cls.client.admin.command('createUser', 'xml-test', pwd=cls.secret, roles=['root'])
            cls.client.close()
            cls.client = MongoClient(cls.uri, username='xml-test', password=cls.secret, authSource='admin')
            actual = cls.client.admin.command('getCmdLineOpts')['parsed']['storage']['dbPath']
            if Path(actual).resolve() != cls.path:
                raise RuntimeError('Wrong Mongo storage; refusing writes')
            cls.database = 'InsightXmlSynthetic'
            db = mixed_database()
            for name in db.list_collection_names():
                cls.client[cls.database][name].insert_many(getattr(db, name).rows)
        except Exception:
            cls.tearDownClass();raise

    @classmethod
    def tearDownClass(cls):
        cls.client.close()
        cls.process.terminate();cls.process.wait(timeout=20)
        cls.folder.cleanup()

    def test_purchase_persists_product_and_payable_with_matching_references(self):
        database = 'InsightPurchaseSynthetic'
        config = product_template_profile()
        config['product_template']['price']['Custo']['Valor'] = 1.0
        config['product_template']['price']['Venda']['Valor'] = 42.0
        config.update(database=database, finance=finance_profile('payable')['finance'])
        config['movement']['_t'] = 'NotaFiscalCompra'
        config['company'] = dict(config['company'], Documento='98765432000110')
        supplier = ObjectId()
        config['customer'] = {'_t': 'JuridicaHistorico', 'PessoaReferencia': supplier,
                              'Documento': config['issuer_cnpj']}
        raw = nfe(xml()).replace(b'</emit>', b'</emit><dest><CNPJ>98765432000110</CNPJ></dest>')
        barcode = '0789123456789'
        raw = raw.replace(b'<cProd>A</cProd>', f'<cProd>A</cProd><cEAN>{barcode}</cEAN>'.encode())
        raw = raw.replace(b'<pag>', b'<cobr><fat><vLiq>10.00</vLiq></fat><dup><nDup>001</nDup>'
                          b'<dVenc>2026-12-01</dVenc><vDup>10.00</vDup></dup></cobr><pag>')
        payment_start = raw.index(b'<pag>')
        raw = raw[:payment_start] + raw[raw.index(b'</pag>', payment_start) + len(b'</pag>'):]
        db = self.client[database]
        db.Pessoas.insert_many([
            {'_id': supplier, 'Nome': 'Fornecedor de teste', 'Ativo': True},
            {'_id': config['company_id'], 'Nome': 'Empresa de teste', 'Ativo': True},
        ])
        account = config['finance']['payable']['parcela']['ContaReferencia']
        db.Contas.insert_one({'_id': account, 'Descricao': 'Conta de teste', 'Ativo': True})
        for name in ('Movimentacoes', 'XmlMovimentacoes', 'Pagamentos', 'ProdutosServicos',
                     'ProdutosServicosEmpresa', 'Precos', 'Estoques', 'EstoquesQuantidade'):
            db.create_collection(name)

        with tempfile.TemporaryDirectory(dir=self.path) as state, ExitStack() as cleanup:
            cleanup.callback(jobs.close_connections)
            key_file = Path(state) / 'fixture.local.key'
            key_file.write_text(self.secret, encoding='utf-8')
            with patch.dict(os.environ, {'INSIGHT_DATA_DIR': state, 'INSIGHT_REQUIRE_READ_ONLY': '0'}), \
                 patch.multiple(app, MONGO_URI=self.uri, MONGO_DATABASE=database, MONGO_USERNAME='xml-test',
                                MONGO_PASSWORD_FILE=str(key_file), MONGO_AUTH_SOURCE='admin', MODE='live'):
                owner = 'isolated-purchase-test'
                job_id = jobs.create(owner, 'manual', json_util.dumps(config), use_barcode=True,
                                     import_finance=True)['id']
                path = Path(state) / 'purchase.xml'
                path.write_bytes(raw)
                slot = jobs.upload_slot(job_id, owner, {'name': path.name, 'size': len(raw), 'source': 'purchase'})
                jobs.upload_local(job_id, owner, path, upload_id=slot['upload_id'])
                review = jobs.launch(job_id, owner, 'validate', background=False)
                self.assertEqual(review['counts'], {'ready': 1}, review)
                token = editor.unlock(editor.UnlockRequest(database=database, confirmation='EDITAR ' + database), owner)['token']
                jobs.authorize_apply(job_id, owner, token, 'IMPORTAR ' + database)
                result = jobs.launch(job_id, owner, 'apply', session_token=token, background=False)
                self.assertEqual(result['counts'], {'imported': 1}, result)
                movement = db.Movimentacoes.find_one({'_t': 'NotaFiscalCompra'})
                self.assertNotIn('ItensPagamentos', movement)
                self.assertNotIn('MovimentacaoItemFinanceiro', movement)
                self.assertEqual(db.XmlMovimentacoes.find_one({'ChaveAcesso': movement['ChaveAcesso']})['TipoMovimentacao'], 4)
                title = db.Pagamentos.find_one()
                self.assertEqual(movement['PagamentoRecebimento']['Parcelas'], [title])
                self.assertEqual(title['PessoaReferencia'], supplier)
                self.assertEqual(title['Historico'][0]['EmpresaReferencia'], title['EmpresaReferencia'])
                self.assertEqual(title['Historico'][0]['ContaReferencia'], account)
                self.assertEqual(title['Historico'][0]['Data'], title['DataEmissao'])
                report = finance.execute(finance.FinanceRequest(
                    report_id='Pagamento.PagamentosEmAberto', start=date(2026, 12, 1), end=date(2026, 12, 1),
                    company_id=str(config['company_id']),
                ), reference_date=date(2026, 11, 25))
                self.assertEqual(report['record_count'], 1)
                row = report['rows'][0]
                self.assertEqual(row['id'], str(title['_id']))
                self.assertEqual(row['company'], 'Empresa de teste')
                self.assertEqual(row['account'], 'Conta de teste')
                self.assertEqual(row['value'], '10.00')
                self.assertEqual(row['settled'], '0.00')
                link = db.ProdutosServicosEmpresa.find_one({'_id': movement['ItensBase'][0]['ProdutoServicoEmpresaReferencia']})
                product = db.ProdutosServicos.find_one({'CodigoBarras': barcode})
                self.assertIsNotNone(product)
                self.assertEqual(product['_id'], link['ProdutoServicoReferencia'])
                price_query = {'_id': link['PrecoReferencia']}
                before = db.Precos.find_one(price_query)
                self.assertEqual(before['Custo']['Valor'], 1.0)
                with patch.object(editor, 'actor', return_value=owner):
                    product_id = str(link['_id'])
                    preview = prepare_cost(job_id, product_id, None, token)['preview']
                    self.assertEqual(db.Precos.find_one(price_query), before)
                    editor.apply_change(preview['id'], token, owner, 'Precos')
                    after = db.Precos.find_one(price_query)
                    self.assertEqual(after['Custo']['Valor'], 3.335)
                    self.assertEqual(after['Venda'], before['Venda'])
                    self.assertIsNone(prepare_cost(job_id, product_id, None, token)['preview'])
                    db.Precos.replace_one(price_query, before)
                    relinked = prepare_cost(job_id, product_id, None, token)['preview']
                    link_query = {'_id': link['_id']}
                    db.ProdutosServicosEmpresa.update_one(link_query, {'$set': {'PrecoReferencia': ObjectId()}})
                    with self.assertRaises(HTTPException) as link_error:
                        editor.apply_change(relinked['id'], token, owner, 'Precos')
                    self.assertEqual(link_error.exception.status_code, 409)
                    self.assertEqual(db.Precos.find_one(price_query), before)
                    db.ProdutosServicosEmpresa.update_one(link_query, {'$set': {'PrecoReferencia': before['_id']}})
                    stale = prepare_cost(job_id, product_id, None, token)['preview']
                    db.Movimentacoes.insert_one(dict(movement, _id=ObjectId(), ChaveAcesso='newer-purchase'))
                    with self.assertRaises(HTTPException) as stale_error:
                        editor.apply_change(stale['id'], token, owner, 'Precos')
                    self.assertEqual(stale_error.exception.status_code, 409)
                    self.assertEqual(db.Precos.find_one(price_query), before)
                    with self.assertRaises(HTTPException) as blocked:
                        prepare_cost(job_id, product_id, None, token)
                    self.assertEqual(blocked.exception.status_code, 409)

    def test_gui_setup_bulk_import_journal_duplicate_and_reconnect(self):
        with tempfile.TemporaryDirectory(dir=self.path) as state:
            settings = connection.ConnectionSettings(host='127.0.0.1', port=self.port, database=self.database,
                username='xml-test', password=self.secret, auth_source='admin')
            with patch.dict(os.environ, {'INSIGHT_DATA_DIR': state, 'INSIGHT_REQUIRE_READ_ONLY': '0'}), \
                 patch.object(connection, 'runtime_dir', return_value=Path(state)), \
                 patch.multiple(app, MONGO_URI=self.uri, MONGO_DATABASE=self.database, MONGO_USERNAME='xml-test',
                     MONGO_PASSWORD_FILE='', MONGO_AUTH_SOURCE='admin', MODE='live'):
                saved = connection.save_connection(settings)
                self.assertTrue(saved['saved'])
                config = dotenv_values(Path(state) / '.env', interpolate=False)
                self.assertNotIn('MONGO_PASSWORD', config)
                persisted_password = (Path(state) / config['MONGO_PASSWORD_FILE']).read_text(encoding='utf-8').strip()
                # Reconnect using exactly the values persisted by the GUI.
                with MongoClient(config['MONGO_URI'], username=config['MONGO_USERNAME'],
                        password=persisted_password, authSource=config['MONGO_AUTH_SOURCE']) as reconnected:
                    self.assertEqual(reconnected[self.database].Movimentacoes.count(), 2)
                owner = 'isolated-xml-test'
                jobs._stopping.clear()
                job_id = jobs.create(owner)['id']
                for index, raw in enumerate([nfe(), xml(124), nfe()]):
                    path = Path(state) / f'{index}.xml';path.write_bytes(raw)
                    jobs.upload_local(job_id, owner, path, upload_id=str(index))
                validated = jobs.launch(job_id, owner, 'validate', background=False)
                self.assertEqual(validated['counts'], {'duplicate': 1, 'ready': 2})
                token = editor.unlock(editor.UnlockRequest(database=self.database,
                    confirmation='EDITAR ' + self.database), owner)['token']
                jobs.authorize_apply(job_id, owner, token, 'IMPORTAR ' + self.database)
                result = jobs.launch(job_id, owner, 'apply', session_token=token, background=False)
                self.assertEqual(result['state'], 'complete')
                self.assertEqual(result['counts']['imported'], 2)
                db = self.client[self.database]
                inserted = list(db.Movimentacoes.find({'Numero': 124}))
                self.assertEqual(len(inserted), 2)
                self.assertEqual({d['_t'][-1] for d in inserted},
                    {'NotaFiscalEletronicaSaida', 'NotaFiscalConsumidorEletronica'})
                self.assertEqual(db.XmlMovimentacoes.count(), 4)
                for document in inserted:
                    self.assertEqual(document['ItensBase'][0]['PrecoUnitario'], 3.335)
                    self.assertEqual(document['ItensBase'][0]['DescontoDigitado'], .01)
                    is_nfce = document['_t'][-1] == 'NotaFiscalConsumidorEletronica'
                    for field in ('MovimentacaoItemFinanceiro', 'NomeUsuario'):
                        self.assertEqual(field in document, is_nfce)
                # New batch detects both already-imported identities without writing.
                retry = jobs.create(owner)['id']
                for index, raw in enumerate([xml(124), nfe()]):
                    path = Path(state) / f'retry-{index}.xml';path.write_bytes(raw)
                    jobs.upload_local(retry, owner, path, upload_id=str(index))
                repeated = jobs.launch(retry, owner, 'validate', background=False)
                self.assertEqual(repeated['counts'], {'duplicate': 2})
                self.assertEqual(db.Movimentacoes.count(), 4)
                import insight_store
                with insight_store.connect() as local:
                    entries = local.execute('SELECT body FROM editor_changes').fetchall()
                self.assertEqual(len(entries), 4)
                self.assertNotIn(self.secret, str(entries))

                outgoing = nfe()
                old_key = ET.fromstring(outgoing).find('n:NFe/n:infNFe', NS).get('Id')[3:]
                head = old_key[:35] + '12345679'
                digit = 11 - sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(head))) % 11
                incoming_key = head + str(0 if digit >= 10 else digit)
                raw = outgoing.replace(old_key.encode(), incoming_key.encode()).replace(b'<tpNF>1', b'<tpNF>0')
                mapping = profile()
                mapping['database'] = self.database
                mapping['source_profiles'] = {'incoming': {
                    'movement': dict(mapping['movement'], _t='NotaFiscalEletronicaEntrada'),
                }}
                incoming_job = jobs.create(owner, 'manual', json_util.dumps(mapping))['id']
                archive_id = db.XmlMovimentacoes.insert_one({
                    'ChaveAcesso': incoming_key, 'Xml': raw.decode(), 'EventoCancelamento': False,
                    'TipoMovimentacao': 5,
                }).inserted_id
                staged = jobs.import_keys(incoming_job, owner, [incoming_key], source='incoming')
                self.assertEqual(staged['results'][0]['status'], 'stored', staged)
                review = jobs.launch(incoming_job, owner, 'validate', background=False)
                self.assertEqual(review['counts'], {'ready': 1}, review)
                jobs.authorize_apply(incoming_job, owner, token, 'IMPORTAR ' + self.database)
                result = jobs.launch(incoming_job, owner, 'apply', session_token=token, background=False)
                self.assertEqual(result['counts'], {'imported': 1}, result)
                incoming_move = db.Movimentacoes.find_one({'ChaveAcesso': incoming_key})
                outgoing_move = db.Movimentacoes.find_one({'ChaveAcesso': old_key})
                self.assertEqual(incoming_move['_t'], 'NotaFiscalEletronicaEntrada')
                self.assertIn('ItensPagamentos', incoming_move)
                self.assertNotIn('MovimentacaoItemFinanceiro', incoming_move)
                self.assertEqual((incoming_move['Numero'], incoming_move['Serie']),
                                 (outgoing_move['Numero'], outgoing_move['Serie']))
                archived = list(db.XmlMovimentacoes.find({'ChaveAcesso': incoming_key}))
                self.assertEqual([(item['_id'], item['Xml']) for item in archived], [(archive_id, raw.decode())])

                serial, number = '000000123', '000456'
                head = '352611' + mapping['issuer_cnpj'] + '59' + serial + number + '123456'
                digit = 11 - sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(head))) % 11
                sat_key = head + str(0 if digit >= 10 else digit)
                receipt = (f'<CFe><infCFe Id="CFe{sat_key}"><ide><nserieSAT>{serial}</nserieSAT>'
                           f'<nCFe>{number}</nCFe><dEmi>20261124</dEmi><hEmi>102030</hEmi></ide>'
                           f'<emit><CNPJ>{mapping["issuer_cnpj"]}</CNPJ></emit>'
                           '<det nItem="1"><prod><cProd>A</cProd><qCom>3</qCom><vUnCom>5.00</vUnCom>'
                           '<vProd>15.00</vProd></prod></det><total><ICMSTot><vProd>15.00</vProd></ICMSTot>'
                           '<vCFe>15.00</vCFe></total><pgto><MP><cMP>01</cMP><vMP>15.00</vMP>'
                           '</MP></pgto></infCFe></CFe>').encode()
                mapping['source_profiles']['cfe'] = {'cfe': {
                    'movement': {'_t': 'CupomFiscalEletronico', 'Empresa': mapping['company'],
                                 'Situacao': {'_t': 'Concluido', 'Codigo': 2},
                                 'Ativo': True, 'MovimentacaoItemFinanceiro': False},
                    'item': {'_t': 'ItemProdutoSaida',
                             'Quantidade': {'$bind': 'item.quantity', 'type': 'double'},
                             'PrecoUnitario': {'$bind': 'item.unit_price', 'type': 'double'},
                             'ProdutoServicoEmpresaReferencia': {'$bind': 'product.reference'}},
                    'payments': {'01': {'Valor': 15.0}},
                }}
                path = Path(state) / 'sat.xml'
                path.write_bytes(receipt)
                for expected in ('imported', 'duplicate'):
                    sat_job = jobs.create(owner, 'manual', json_util.dumps(mapping))['id']
                    if expected == 'imported':
                        slot = jobs.upload_slot(sat_job, owner, {'name': path.name, 'size': len(receipt), 'source': 'cfe'})
                        jobs.upload_local(sat_job, owner, path, upload_id=slot['upload_id'])
                    else:
                        staged = jobs.import_keys(sat_job, owner, [sat_key], source='cfe')
                        self.assertEqual(staged['results'][0]['status'], 'stored', staged)
                    review = jobs.launch(sat_job, owner, 'validate', background=False)
                    self.assertEqual(review['counts'], {'ready': 1}, review)
                    jobs.authorize_apply(sat_job, owner, token, 'IMPORTAR ' + self.database)
                    result = jobs.launch(sat_job, owner, 'apply', session_token=token, background=False)
                    self.assertEqual(result['counts'], {expected: 1}, result)
                sat_move = db.Movimentacoes.find_one({'ChaveAcesso': sat_key})
                self.assertEqual(sat_move['_t'], 'CupomFiscalEletronico')
                self.assertEqual(sat_move['NumeroFabricacaoSat'], serial)
                self.assertEqual(sat_move['Pagamentos'][0]['Valor'], 15.0)
                self.assertTrue({'ItensPagamentos', 'DataHoraEntradaSaida', 'MovimentacaoItemFinanceiro'}.isdisjoint(sat_move))
                self.assertEqual(db.XmlMovimentacoes.find_one({'ChaveAcesso': sat_key})['Xml'], receipt.decode())
                self.assertEqual(db.XmlMovimentacoes.find_one({'ChaveAcesso': sat_key})['TipoMovimentacao'], 12)
                self.assertEqual(db.Movimentacoes.count_documents({'ChaveAcesso': sat_key}), 1)
                self.assertEqual(db.XmlMovimentacoes.count_documents({'ChaveAcesso': sat_key}), 1)
                jobs.close_connections()

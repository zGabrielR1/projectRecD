"""Real import writes on a new disposable loopback MongoDB; never a customer DB."""
import os
from pathlib import Path
import socket
import subprocess
import tempfile
import time
import unittest
from unittest.mock import patch

from dotenv import dotenv_values
from pymongo import MongoClient
from pymongo.errors import ConnectionFailure

import app
import insight_connection as connection
import insight_editor as editor
import insight_nfce_jobs as jobs
from test_xml_release import mixed_database, nfe
from test_nfce_discovery import xml


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

    def test_gui_setup_bulk_import_journal_duplicate_and_reconnect(self):
        with tempfile.TemporaryDirectory(dir=self.path) as state:
            settings = connection.ConnectionSettings(host='127.0.0.1', port=self.port, database=self.database,
                username='xml-test', password=self.secret, auth_source='admin')
            with patch.dict(os.environ, {'INSIGHT_DATA_DIR': state, 'INSIGHT_REQUIRE_READ_ONLY': '0'}), \
                 patch.object(connection, 'runtime_dir', return_value=Path(state)), \
                 patch.multiple(app, MONGO_URI=self.uri, MONGO_DATABASE=self.database, MONGO_USERNAME='xml-test',
                     MONGO_PASSWORD=self.secret, MONGO_PASSWORD_FILE='', MONGO_AUTH_SOURCE='admin', MODE='live'):
                saved = connection.save_connection(settings)
                self.assertTrue(saved['saved'])
                config = dotenv_values(Path(state) / '.env', interpolate=False)
                self.assertEqual(config['MONGO_PASSWORD'], self.secret)
                self.assertNotIn('MONGO_PASSWORD_FILE', config)
                # Reconnect using exactly the values persisted by the GUI.
                with MongoClient(config['MONGO_URI'], username=config['MONGO_USERNAME'],
                        password=config['MONGO_PASSWORD'], authSource=config['MONGO_AUTH_SOURCE']) as reconnected:
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
                jobs.close_connections()

"""Independent mixed-model and GUI credential regression checks; synthetic data."""
from copy import deepcopy
import os
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch
from xml.etree import ElementTree as ET

from bson import ObjectId
from dotenv import dotenv_values
import app
import insight_connection as connection
from insight_nfce import NS, parse_xml
from insight_nfce_discovery import Discovery, prepare_files
from insight_nfce_mapping import build_sale
from test_nfce_discovery import Database, xml
from test_nfce_import import profile


def nfe(raw=None):
    raw = raw or xml(124)
    root = ET.fromstring(raw)
    key = root.find('n:NFe/n:infNFe', NS).get('Id')[3:]
    head = key[:20] + '55' + key[22:43]
    digit = 11 - sum(int(n) * (2 + i % 8) for i, n in enumerate(reversed(head))) % 11
    new_key = head + str(0 if digit >= 10 else digit)
    return raw.replace(key.encode(), new_key.encode()).replace(b'<mod>65</mod>', b'<mod>55</mod>')


def mixed_database():
    db = Database()
    movement = deepcopy(db.Movimentacoes.rows[0])
    movement['_id'] = ObjectId()
    movement['_t'] = ['Movimentacao', 'NotaFiscalEletronicaSaida']
    movement['Modelo'] = {'_t': 'NotaFiscalEletronica', 'Codigo': 55}
    raw = nfe(xml())
    movement['ChaveAcesso'] = parse_xml(raw, issuer='12345678000195')['access_key']
    db.Movimentacoes.rows.append(movement)
    db.XmlMovimentacoes.rows.append({'_id': ObjectId(), 'ChaveAcesso': movement['ChaveAcesso'],
        'Xml': raw.decode(), 'EventoCancelamento': False, 'TipoMovimentacao': 7})
    return db


class XmlReleaseTests(unittest.TestCase):
    def test_mixed_models_use_separate_shapes_and_native_xml_types(self):
        discovery = Discovery(mixed_database())
        for raw, kind, xml_type in [(nfe(), 'NotaFiscalEletronicaSaida', 7),
                                    (xml(124), 'NotaFiscalConsumidorEletronica', 11)]:
            incoming = prepare_files([('invoice.xml', raw)])[0][0]
            sale = discovery.build(incoming)
            self.assertEqual(sale['movement']['_t'][-1], kind)
            self.assertEqual(sale['xml']['TipoMovimentacao'], xml_type)
            self.assertEqual(sale['movement']['Numero'], 124)
            # 3 * 3.335 rounds to 10.01, less 0.01 = 10.00.
            self.assertEqual(sale['summary']['total'], '10.00')
        self.assertEqual(len(discovery.cache), 2)

    def test_nfe_cannot_borrow_nfce_shape_or_missing_xml_type(self):
        incoming = prepare_files([('nfe.xml', nfe())])[0][0]
        with self.assertRaises(ValueError):
            Discovery(Database()).build(incoming)
        db = mixed_database()
        del db.XmlMovimentacoes.rows[-1]['TipoMovimentacao']
        with self.assertRaises(ValueError):
            Discovery(db).build(incoming)

    def test_nfe_profile_requires_matching_discriminator_and_defaults_native_xml_type(self):
        p = profile()
        with self.assertRaises(ValueError): build_sale(nfe(), p)
        p['movement']['_t'] = ['Movimentacao', 'NotaFiscalEletronicaSaida']
        self.assertEqual(build_sale(nfe(), p)['xml']['TipoMovimentacao'], 10)
        p['xml_movement_type'] = 7
        self.assertEqual(build_sale(nfe(), p)['xml']['TipoMovimentacao'], 7)

    def test_wrong_key_model_and_non_sale_purpose_are_blocked(self):
        with self.assertRaises(ValueError):
            parse_xml(nfe().replace(b'<mod>55', b'<mod>65'), issuer='12345678000195')
        for field, original, changed in [('tpNF', '1', '0'), ('finNFe', '1', '4')]:
            raw = nfe().replace(f'<{field}>{original}'.encode(), f'<{field}>{changed}'.encode())
            with self.assertRaises(ValueError):
                Discovery(mixed_database()).build(prepare_files([('blocked.xml', raw)])[0][0])


class GuiPasswordTests(unittest.TestCase):
    def test_gui_connection_options_persist_and_secrets_stay_redacted(self):
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', return_value={'connected': True}), patch.object(connection, 'event'), \
             patch.multiple(app, MONGO_PASSWORD_FILE='', MONGO_URI=app.MONGO_URI,
                 MONGO_DATABASE=app.MONGO_DATABASE, MONGO_USERNAME=app.MONGO_USERNAME, MONGO_AUTH_SOURCE=app.MONGO_AUTH_SOURCE,
                 MODE=app.MODE), patch.dict(os.environ):
            secret = 'private-mongo-fixture'
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', username='importer',
                password=secret, require_read_only=True))
            values = dotenv_values(Path(folder) / '.env', interpolate=False)
            self.assertEqual(values['INSIGHT_REQUIRE_READ_ONLY'], '1')
            self.assertNotIn(secret, str(values))
            settings = connection.current_settings()
            self.assertTrue(settings['require_read_only'])
            self.assertTrue(settings['password_file_exists'])
            self.assertNotIn(secret, str(settings))

    def test_password_roundtrip_reuses_new_key_file(self):
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', return_value={'connected': True}), \
             patch.object(connection, 'event'), patch.multiple(app, MONGO_PASSWORD_FILE='old.key',
                 MONGO_URI=app.MONGO_URI, MONGO_DATABASE=app.MONGO_DATABASE, MONGO_USERNAME=app.MONGO_USERNAME,
                 MONGO_AUTH_SOURCE=app.MONGO_AUTH_SOURCE, MODE=app.MODE), patch.dict(os.environ):
            path = Path(folder) / '.env'
            path.write_text('MONGO_PASSWORD_FILE=old.key\nMAX_REPORT_ROWS=500\n')
            secret = "quote' slash\\ dollar${UNCHANGED} hash# unicodeç"
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', username='importer', password=secret))
            parsed = dotenv_values(path, interpolate=False)
            self.assertNotIn('MONGO_PASSWORD', parsed)
            key_file = Path(folder) / parsed['MONGO_PASSWORD_FILE']
            self.assertEqual(key_file.read_text(encoding='utf-8'), secret + '\n')
            self.assertEqual(parsed['MAX_REPORT_ROWS'], '500')
            self.assertEqual(connection.password_from_current(), secret)
            self.assertNotIn(secret, str(connection.current_settings()))
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', username='importer'))
            self.assertEqual(connection.password_from_current(), secret)
            self.assertEqual(list(Path(folder).glob('*.key')), [key_file])

    def test_failed_connection_never_changes_configuration(self):
        from fastapi import HTTPException
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', side_effect=HTTPException(422, 'Rejected')):
            path = Path(folder) / '.env';path.write_text('MONGO_DATABASE=Before\n')
            with self.assertRaises(HTTPException):
                connection.save_connection(connection.ConnectionSettings(database='After', password='wrong'))
            self.assertEqual(path.read_text(), 'MONGO_DATABASE=Before\n')

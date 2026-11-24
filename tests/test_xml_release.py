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

    def test_nfe_profile_requires_matching_discriminator_and_explicit_xml_type(self):
        p = profile()
        with self.assertRaises(ValueError): build_sale(nfe(), p)
        p['movement']['_t'] = ['Movimentacao', 'NotaFiscalEletronicaSaida']
        with self.assertRaisesRegex(ValueError, 'xml_movement_type'): build_sale(nfe(), p)
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
    def test_gui_application_options_persist_and_secrets_stay_redacted(self):
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', return_value={'connected': True}), patch.object(connection, 'event'), \
             patch.multiple(app, MONGO_PASSWORD='', MONGO_PASSWORD_FILE='', MONGO_URI=app.MONGO_URI,
                 MONGO_DATABASE=app.MONGO_DATABASE, MONGO_USERNAME=app.MONGO_USERNAME, MONGO_AUTH_SOURCE=app.MONGO_AUTH_SOURCE,
                 MODE=app.MODE, MAX_ROWS=app.MAX_ROWS, AI_ENDPOINT=app.AI_ENDPOINT, AI_MODEL=app.AI_MODEL, AI_API_KEY=app.AI_API_KEY), patch.dict(os.environ):
            original_user = app.INSIGHT_USER
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', max_report_rows=750,
                ai_endpoint='http://127.0.0.1:11434', ai_model='synthetic', ai_api_key='private-api-fixture',
                panel_username='operator', panel_password='private-panel-fixture'))
            values = dotenv_values(Path(folder) / '.env', interpolate=False)
            self.assertEqual(values['MAX_REPORT_ROWS'], '750')
            self.assertEqual(app.MAX_ROWS, 750)
            self.assertEqual(values['AI_API_KEY'], 'private-api-fixture')
            self.assertEqual(values['INSIGHT_PASSWORD'], 'private-panel-fixture')
            self.assertEqual(app.INSIGHT_USER, original_user)  # Authentication changes together on restart.
            self.assertNotIn('private-api-fixture', str(connection.current_settings()))
            self.assertNotIn('private-panel-fixture', str(connection.current_settings()))

    def test_password_roundtrip_migrates_legacy_reference_without_key_files(self):
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', return_value={'connected': True}), \
             patch.object(connection, 'event'), patch.multiple(app, MONGO_PASSWORD='', MONGO_PASSWORD_FILE='old.key',
                 MONGO_URI=app.MONGO_URI, MONGO_DATABASE=app.MONGO_DATABASE, MONGO_USERNAME=app.MONGO_USERNAME,
                 MONGO_AUTH_SOURCE=app.MONGO_AUTH_SOURCE, MODE=app.MODE), patch.dict(os.environ):
            path = Path(folder) / '.env'
            path.write_text('MONGO_PASSWORD_FILE=old.key\nMAX_REPORT_ROWS=500\n')
            secret = " quote' slash\\ dollar${UNCHANGED} hash# unicodeç "
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', username='importer', password=secret))
            parsed = dotenv_values(path, interpolate=False)
            self.assertEqual(parsed['MONGO_PASSWORD'], secret)
            self.assertNotIn('MONGO_PASSWORD_FILE', parsed)
            self.assertEqual(parsed['MAX_REPORT_ROWS'], '500')
            self.assertEqual(connection.password_from_current(), secret)
            self.assertNotIn(secret, str(connection.current_settings()))
            self.assertFalse(list(Path(folder).glob('*.key')))
            connection.save_connection(connection.ConnectionSettings(database='Synthetic', username='importer'))
            self.assertEqual(dotenv_values(path, interpolate=False)['MONGO_PASSWORD'], secret)

    def test_failed_connection_never_changes_configuration(self):
        from fastapi import HTTPException
        with tempfile.TemporaryDirectory() as folder, patch.object(connection, 'runtime_dir', return_value=Path(folder)), \
             patch.object(connection, 'test_connection', side_effect=HTTPException(422, 'Rejected')):
            path = Path(folder) / '.env';path.write_text('MONGO_DATABASE=Before\n')
            with self.assertRaises(HTTPException):
                connection.save_connection(connection.ConnectionSettings(database='After', password='wrong'))
            self.assertEqual(path.read_text(), 'MONGO_DATABASE=Before\n')

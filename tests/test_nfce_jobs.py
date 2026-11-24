"""Disk-backed intake tests; synthetic XMLs only, no Mongo server or native sale writes."""
import hashlib
import io
import json
import os
from pathlib import Path
import subprocess
import sys
import tempfile
import time
import unittest
from unittest.mock import patch, MagicMock
import zipfile

from bson import json_util
from fastapi import FastAPI, HTTPException
from fastapi.testclient import TestClient

sys.path.insert(0,str(Path(__file__).resolve().parent))
from test_nfce_discovery import xml
from test_nfce_import import profile
import insight_nfce_jobs as jobs
from insight_nfce_jobs_api import router
import insight_editor as editor
from insight_nfce_mapping import build_sale,validate_native_documents


def padded_xml(number=124,size=0):
    return xml(number).replace(b'</infNFe>',b'<infAdic><infCpl>'+b'x'*size+b'</infCpl></infAdic></infNFe>')


class JobTests(unittest.TestCase):
    def setUp(self):
        self.temp=tempfile.TemporaryDirectory()
        self.environment=patch.dict(os.environ,{'INSIGHT_DATA_DIR':self.temp.name})
        self.environment.start()
        self.identity=patch.object(editor,'connection_identity',return_value='offline-connection')
        self.identity.start();jobs._stopping.clear()
        server=FastAPI();server.include_router(router)
        server.dependency_overrides[editor.same_origin]=lambda:None
        self.client=TestClient(server)

    def tearDown(self):
        self.client.close();jobs.close_connections();self.identity.stop();self.environment.stop();self.temp.cleanup()

    def create(self):return jobs.create('owner','manual',json_util.dumps(profile()))['id']

    def add(self,job_id,raw,index=0):
        path=Path(self.temp.name)/('input-'+str(index)+'.xml');path.write_bytes(raw)
        jobs.upload_local(job_id,'owner',path,upload_id=str(index))

    def test_access_key_lookup_stages_only_matching_authorized_xml_and_resumes(self):
        created = self.client.post('/api/nfce-import/jobs', json={
            'mode': 'manual', 'profile': json_util.dumps(profile()),
        }).json()
        raw = xml()
        key = build_sale(raw, profile())['summary']['access_key']
        missing = build_sale(xml(124), profile())['summary']['access_key']
        collection = MagicMock()
        collection.find_one.return_value = None
        cursor = collection.find.return_value.max_time_ms.return_value.batch_size.return_value
        cursor.__enter__.return_value = [{'Xml': raw.decode()}]
        url = f"/api/nfce-import/jobs/{created['id']}/keys"
        with patch.object(editor, 'editor_client'), patch.object(editor, 'selected_collection', return_value=collection):
            result = self.client.post(url, json={'keys': [key, ' '.join([key[:10], key[10:]]), missing, '123']})
            self.assertEqual(result.status_code, 200)
            self.assertEqual([row['status'] for row in result.json()['results']], ['stored', 'unavailable', 'unavailable'])
            self.assertEqual(self.client.post(url, json={'keys': [key]}).json()['results'][0]['status'], 'stored')
            collection.find_one.return_value = {'_id': 'cancelled'}
            self.assertIn('cancelamento', self.client.post(url, json={'keys': [key]}).json()['results'][0]['message'])
        with jobs.connect() as db:
            owner = db.execute('SELECT owner FROM jobs WHERE id=?', (created['id'],)).fetchone()[0]
        result = jobs.launch(created['id'], owner, 'validate', background=False)
        self.assertEqual(result['total'], 1)
        self.assertEqual(result['counts']['ready'], 1)
        self.assertEqual(result['rows'][0]['access_key'], key)
        from insight_nfce_api import apply_sale
        sale = jobs.document(created['id'], owner, result['rows'][0]['id'])
        movements = MagicMock()
        movements.find_one.return_value = None
        collection.find_one.side_effect = [None, {'Xml': raw.decode()}]
        with patch.object(editor, 'selected_collection', side_effect=[movements, collection]), \
             patch.object(editor, 'prepare_import_insert', return_value={'id': 'movement-change'}) as prepare, \
             patch.object(editor, 'apply_change'):
            applied = apply_sale(sale, 'SyntheticOnly', 'token', owner, client=MagicMock())
        self.assertEqual(applied['status'], 'imported')
        self.assertEqual(prepare.call_count, 1)
        self.assertEqual(prepare.call_args.args[2], 'Movimentacoes')

    def test_more_than_250_files_and_over_8_mib_validate_with_paged_results(self):
        job_id=self.create()
        for index in range(275):self.add(job_id,padded_xml(1000+index,33000),index)
        with patch.object(editor,'editor_client',side_effect=AssertionError('Offline manual mode must not access Mongo')):
            result=jobs.launch(job_id,'owner','validate',background=False)
        self.assertEqual(result['state'],'ready')
        self.assertEqual(result['total'],275)
        self.assertEqual(result['counts']['ready'],275)
        self.assertGreater(result['bytes'],8*1024*1024)
        self.assertEqual(len(result['rows']),50)
        self.assertTrue(result['has_more'])
        last=jobs.snapshot(job_id,'owner',250)
        self.assertEqual(len(last['rows']),25);self.assertFalse(last['has_more'])

    def test_streamed_http_xml_over_one_mib_and_resume_hash_check(self):
        created=self.client.post('/api/nfce-import/jobs',json={'mode':'manual','profile':json_util.dumps(profile())})
        self.assertEqual(created.status_code,200)
        job_id=created.json()['id'];raw=padded_xml(size=2*1024*1024)
        def chunks():
            for at in range(0,len(raw),65536):yield raw[at:at+65536]
        response=self.client.post(f'/api/nfce-import/jobs/{job_id}/files/stable?name=synthetic.xml',content=chunks(),headers={'Content-Type':'application/xml'})
        self.assertEqual(response.status_code,200)
        self.assertEqual(response.json()['bytes'],len(raw))
        self.assertTrue(self.client.get(f'/api/nfce-import/jobs/{job_id}/files/stable').json()['stored'])
        again=self.client.post(f'/api/nfce-import/jobs/{job_id}/files/stable',content=chunks())
        self.assertTrue(again.json()['resumed'])
        conflict=self.client.post(f'/api/nfce-import/jobs/{job_id}/files/stable',content=b'different')
        self.assertEqual(conflict.status_code,409)
        from insight_editor import actor
        # Same authenticated actor as TestClient's request, not the CLI owner.
        with jobs.connect() as db:owner=db.execute('SELECT owner FROM jobs WHERE id=?',(job_id,)).fetchone()[0]
        result=jobs.launch(job_id,owner,'validate',background=False)
        self.assertEqual(result['counts']['ready'],1)
        with jobs.connect() as db:row=dict(db.execute('SELECT * FROM files WHERE job_id=?',(job_id,)).fetchone())
        self.assertEqual(jobs.read_sale(job_id,row)['xml']['Xml'].encode(),raw)

    def test_signed_and_authorized_pair_deduplicates_across_file_order(self):
        from xml.etree import ElementTree as ET
        from insight_nfce import NS
        raw=xml(124);signed=ET.tostring(ET.fromstring(raw).find('n:NFe',NS))
        job_id=self.create();self.add(job_id,signed,0);self.add(job_id,raw,1)
        result=jobs.launch(job_id,'owner','validate',background=False)
        self.assertEqual(result['counts'],{'duplicate':1,'ready':1})
        path=jobs.export_zip(job_id,'owner')
        with zipfile.ZipFile(path) as archive:
            self.assertEqual(len(json_util.loads(archive.read('Movimentacoes.json'))),1)
            self.assertEqual(len(json_util.loads(archive.read('XmlMovimentacoes.json'))),1)
        self.assertEqual(jobs.export_zip(job_id,'owner'),path)

    def test_pause_and_resume_does_not_reindex_completed_files(self):
        job_id=self.create()
        for index in range(4):self.add(job_id,xml(124+index),index)
        original=jobs.index_file;processed=[]
        def pause_after_one(j,row):
            original(j,row);processed.append(row['id'])
            if len(processed)==1:jobs.pause(j,'owner')
        with patch.object(jobs,'index_file',side_effect=pause_after_one):
            result=jobs.launch(job_id,'owner','validate',background=False)
        self.assertEqual(result['state'],'paused')
        self.assertEqual(result['counts']['uploaded'],3)
        with patch.object(jobs,'index_file',wraps=original) as indexer:
            result=jobs.launch(job_id,'owner','validate',background=False)
        self.assertEqual(indexer.call_count,3);self.assertEqual(result['counts']['ready'],4)

    def test_upload_left_receiving_by_a_crashed_process_can_resume(self):
        job_id=self.create();row,target=jobs.reserve(job_id,'owner','interrupted','synthetic.xml')
        target.write_bytes(b'partial')
        jobs._uploads.discard(row['id'])  # Simulated process exit; no active HTTP upload remains.
        self.add(job_id,xml(),0)
        path=Path(self.temp.name)/'resumed.xml';path.write_bytes(xml(125))
        jobs.upload_local(job_id,'owner',path,upload_id='interrupted')
        result=jobs.launch(job_id,'owner','validate',background=False)
        self.assertEqual(result['counts']['ready'],2)

    def test_persistence_in_another_process_and_no_preview_expiry(self):
        job_id=self.create();self.add(job_id,xml())
        with jobs.connect() as db:db.execute('UPDATE jobs SET created=0,updated=0 WHERE id=?',(job_id,))
        code="import json; import insight_nfce_jobs as j; j.editor.connection_identity=lambda:'offline-connection'; s=j.snapshot('"+job_id+"','owner'); print(json.dumps({'total':s['total'],'state':s['state']}))"
        output=subprocess.run([sys.executable,'-c',code],capture_output=True,text=True,check=True)
        self.assertEqual(json.loads(output.stdout),{'total':1,'state':'uploading'})

    def test_actor_and_connection_changes_are_rejected(self):
        job_id=self.create()
        with self.assertRaises(HTTPException):jobs.snapshot(job_id,'other')
        with patch.object(editor,'connection_identity',return_value='different'),self.assertRaises(HTTPException):jobs.snapshot(job_id,'owner')

    def test_physical_mongo_bson_limit_is_checked_not_one_mib(self):
        sale=build_sale(padded_xml(size=2*1024*1024),profile())
        validate_native_documents(sale['movement'],sale['xml'])
        with self.assertRaisesRegex(ValueError,'nativo'):
            validate_native_documents({'Xml':'x'*(16*1024*1024)})

    def test_import_journal_accepts_large_document_but_http_editor_limit_remains(self):
        sale=build_sale(padded_xml(size=2*1024*1024),profile());document=sale['xml']
        collection=MagicMock();collection.find_one.return_value=None
        access={'grants':[],'fingerprint':'fp','connection_identity':'offline-connection'}
        with patch.object(editor,'session',return_value=access),patch.object(editor,'permitted',return_value=True),patch.object(editor,'selected_collection',return_value=collection),patch.object(editor,'fingerprint',return_value='fp'):
            change=editor.prepare_import_insert(document,'SyntheticOnly','XmlMovimentacoes','token','owner',client=MagicMock())
            result=editor.apply_change(change['id'],'token','owner','XmlMovimentacoes',document_limit=None,client=MagicMock())
        self.assertEqual(result['status'],'applied')
        self.assertEqual(collection.insert_one.call_args.args[0]['Xml'],document['Xml'])
        from pydantic import ValidationError
        with self.assertRaises(ValidationError):
            editor.ChangeRequest(database='SyntheticOnly',collection='XmlMovimentacoes',operation='insert',document_json=json_util.dumps(document))

    def test_uncertain_write_is_not_automatically_retried(self):
        job_id=self.create();self.add(job_id,xml())
        jobs.launch(job_id,'owner','validate',background=False)
        with patch.object(editor,'editor_client'),patch.object(editor,'session',return_value={'expires':time.time()+600}),patch('insight_nfce_api.apply_sale',return_value={'status':'stopped','changes':['one'],'message':'unknown'}) as apply:
            result=jobs.launch(job_id,'owner','apply',session_token='token',background=False)
        self.assertEqual(result['counts']['unknown'],1)
        self.assertEqual(apply.call_count,1)
        with self.assertRaises(HTTPException):jobs.launch(job_id,'owner','apply',session_token='token',background=False)

    def test_long_job_renews_private_worker_session_instead_of_expiring(self):
        job_id=self.create();self.add(job_id,xml());jobs.launch(job_id,'owner','validate',background=False)
        def session(token,*args):return {'expires':time.time()+(1 if token=='browser' else 600)}
        with patch.object(editor,'editor_client'),patch.object(editor,'session',side_effect=session),patch.object(editor,'unlock',return_value={'token':'private-worker'}) as renew,patch('insight_nfce_api.apply_sale',return_value={'status':'imported','changes':[]}) as apply:
            result=jobs.launch(job_id,'owner','apply',session_token='browser',background=False)
        self.assertEqual(result['counts']['imported'],1)
        renew.assert_called_once();self.assertEqual(apply.call_args.args[2],'private-worker')

    def test_metrics_and_discard_do_not_touch_customer_database(self):
        job_id=self.create();raw=xml();self.add(job_id,raw)
        state=jobs.snapshot(job_id,'owner');self.assertEqual(state['bytes'],len(raw))
        with patch.object(editor,'editor_client',side_effect=AssertionError('No Mongo writes')):
            self.assertEqual(jobs.discard(job_id,'owner')['mongo_writes'],0)
        with self.assertRaises(HTTPException):jobs.snapshot(job_id,'owner')


if __name__=='__main__':unittest.main()

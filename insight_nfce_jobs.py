"""Disk-backed NFC-e jobs. Input count and total bytes are not capped.

Files, indexed identities and converted BSON stay on disk. Workers process one
document at a time; SQLite indexes handle deduplication and paged progress.
"""
from contextlib import contextmanager, nullcontext, ExitStack
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path
from decimal import Decimal
import hashlib
import json
import os
import re
import secrets
import sqlite3
import threading
import time
import zipfile
import shutil
from collections import OrderedDict

from bson import BSON, json_util
from fastapi import HTTPException

import insight_store as store
import insight_editor as editor
from insight_nfce import ARCHIVED_CANCELLATION, NOTICE, MONGO_DOCUMENT_BYTES, key_valid, parse_xml
from insight_nfce_mapping import (load_profile, build_sale, build_cfe, sale_collections,
                                  sale_documents, profile_for_source, validate_native_documents, native_issue_time)
from insight_nfce_discovery import nfe_route, parse_cfe, Discovery, read_xml, tree_signature, NS

_pool=ThreadPoolExecutor(max_workers=2,thread_name_prefix='nfce')
_active={}
_lock=threading.RLock()
_worker=secrets.token_hex(16)
_stopping=threading.Event()
_heartbeat_started=False
_initialized=set()
_uploads=set()
_anchors=OrderedDict()
DFE_SOURCES = {'purchase', 'incoming', 'outgoing', 'nfce', 'cfe'}


def take_connection(path):
    with _lock:
        pool=_anchors.setdefault(str(path),[])
        _anchors.move_to_end(str(path))
        connection=pool.pop() if pool else sqlite3.connect(path,timeout=30,check_same_thread=False)
    connection.row_factory=sqlite3.Row
    return connection


def return_connection(path,connection):
    with _lock:
        if path.exists():
            _anchors.setdefault(str(path),[]).append(connection);_anchors.move_to_end(str(path))
            # A pool window limits idle handles, never input files or job size.
            while sum(len(pool) for pool in _anchors.values())>32:
                oldest=next(key for key,pool in _anchors.items() if pool)
                _anchors[oldest].pop().close()
                if not _anchors[oldest]:del _anchors[oldest]
        else:connection.close()


@contextmanager
def connect(*,durable=False):
    path=store.data_dir()/'nfce-import.sqlite3'
    existed=path.exists()
    connection=take_connection(path)
    with _lock:
      if not existed or str(path) not in _initialized:
        connection.execute('PRAGMA journal_mode=WAL')
        connection.executescript('''
      CREATE TABLE IF NOT EXISTS jobs(
        id TEXT PRIMARY KEY, owner TEXT NOT NULL, connection_key TEXT NOT NULL,
        database_name TEXT NOT NULL, mode TEXT NOT NULL, profile TEXT,
        state TEXT NOT NULL, phase TEXT NOT NULL DEFAULT 'upload', error TEXT,
        worker TEXT, lease REAL NOT NULL DEFAULT 0, created REAL NOT NULL, updated REAL NOT NULL);
      CREATE TABLE IF NOT EXISTS files(
        id INTEGER PRIMARY KEY, job_id TEXT NOT NULL, upload_id TEXT NOT NULL,
        name TEXT NOT NULL, path TEXT NOT NULL, bytes INTEGER NOT NULL DEFAULT 0,
        sha TEXT, key TEXT, signature TEXT, full_signature TEXT, kind TEXT,
        status TEXT NOT NULL, message TEXT, summary TEXT, sale_path TEXT, result TEXT, raw BLOB, sale BLOB,
        UNIQUE(job_id,upload_id));
      CREATE INDEX IF NOT EXISTS file_queue ON files(job_id,status,id);
      CREATE INDEX IF NOT EXISTS file_identity ON files(job_id,key,kind,id);
      CREATE INDEX IF NOT EXISTS job_owner ON jobs(owner,connection_key,created DESC);
      CREATE TABLE IF NOT EXISTS metrics(job_id TEXT,status TEXT,n INTEGER NOT NULL,bytes INTEGER NOT NULL,PRIMARY KEY(job_id,status));
      INSERT OR IGNORE INTO metrics SELECT job_id,status,COUNT(*),SUM(bytes) FROM files GROUP BY job_id,status;
      CREATE TRIGGER IF NOT EXISTS files_added AFTER INSERT ON files BEGIN
        INSERT INTO metrics VALUES(new.job_id,new.status,1,new.bytes)
        ON CONFLICT(job_id,status) DO UPDATE SET n=n+1,bytes=bytes+new.bytes;
      END;
      CREATE TRIGGER IF NOT EXISTS files_changed AFTER UPDATE OF status,bytes ON files BEGIN
        UPDATE metrics SET n=n-1,bytes=bytes-old.bytes WHERE job_id=old.job_id AND status=old.status;
        INSERT INTO metrics VALUES(new.job_id,new.status,1,new.bytes)
        ON CONFLICT(job_id,status) DO UPDATE SET n=n+1,bytes=bytes+new.bytes;
      END;
      CREATE TRIGGER IF NOT EXISTS files_removed AFTER DELETE ON files BEGIN
        UPDATE metrics SET n=n-1,bytes=bytes-old.bytes WHERE job_id=old.job_id AND status=old.status;
      END;
        ''')
        columns={row[1] for row in connection.execute('PRAGMA table_info(files)')}
        for column in ('raw','sale'):
            if column not in columns:connection.execute('ALTER TABLE files ADD COLUMN '+column+' BLOB')
        if 'source' not in columns:
            connection.execute('ALTER TABLE files ADD COLUMN source TEXT')
        if 'collections' not in columns:
            connection.execute('ALTER TABLE files ADD COLUMN collections TEXT')
        if 'use_barcode' not in {row[1] for row in connection.execute('PRAGMA table_info(jobs)')}:
            connection.execute('ALTER TABLE jobs ADD COLUMN use_barcode INTEGER NOT NULL DEFAULT 0')
        if 'import_movements' not in {row[1] for row in connection.execute('PRAGMA table_info(jobs)')}:
            connection.execute('ALTER TABLE jobs ADD COLUMN import_movements INTEGER NOT NULL DEFAULT 1')
        if 'import_people' not in {row[1] for row in connection.execute('PRAGMA table_info(jobs)')}:
            connection.execute('ALTER TABLE jobs ADD COLUMN import_people INTEGER NOT NULL DEFAULT 0')
        if 'import_products' not in {row[1] for row in connection.execute('PRAGMA table_info(jobs)')}:
            connection.execute('ALTER TABLE jobs ADD COLUMN import_products INTEGER')
        if 'import_finance' not in {row[1] for row in connection.execute('PRAGMA table_info(jobs)')}:
            connection.execute('ALTER TABLE jobs ADD COLUMN import_finance INTEGER')
        _initialized.add(str(path))
    # Re-creatable staging/progress uses WAL NORMAL; the before-write uncertainty
    # marker uses FULL. The independent editor write journal remains durable too.
    connection.execute('PRAGMA synchronous='+('FULL' if durable else 'NORMAL'))
    try:
        with connection:yield connection
    finally:return_connection(path,connection)


def folder(job_id):
    if not re.fullmatch(r'[A-Za-z0-9_-]{32}',job_id):
        raise HTTPException(404,'Importação não encontrada.')
    root=store.data_dir()/'nfce-import'/job_id
    root.mkdir(parents=True,exist_ok=True)
    return root


@contextmanager
def payload_connection(job_id,*,durable=False):
    path=folder(job_id)/'documents.sqlite3';existed=path.exists()
    connection=take_connection(path)
    with _lock:
        if not existed or str(path) not in _initialized:
            connection.execute('PRAGMA journal_mode=WAL')
            connection.execute('CREATE TABLE IF NOT EXISTS payloads(id INTEGER PRIMARY KEY,raw BLOB,sale BLOB)')
            connection.commit();_initialized.add(str(path))
    connection.execute('PRAGMA synchronous='+('FULL' if durable else 'NORMAL'))
    try:
        with connection:yield connection
    finally:return_connection(path,connection)


def identity():return editor.connection_identity()


def owned(job_id,owner):
    with connect() as db:row=db.execute('SELECT * FROM jobs WHERE id=?',(job_id,)).fetchone()
    if not row or row['owner']!=owner:raise HTTPException(404,'Importação não encontrada.')
    import app
    if row['connection_key']!=identity() or (row['mode']=='automatic' and row['database_name']!=app.MONGO_DATABASE):
        raise HTTPException(409,'A conexão/banco mudou. Selecione a conexão original ou crie outra importação.')
    return dict(row)


def create(owner,mode='automatic',profile=None,use_barcode=False,import_movements=True,import_people=False,
           import_products=None, import_finance=False):
    import app
    if type(import_movements) is not bool:
        raise HTTPException(422, 'Seleção de movimentações inválida.')
    if type(import_people) is not bool or (import_people and mode != 'manual'):
        raise HTTPException(422, 'A seleção Pessoas exige um perfil JSON revisado.')
    if import_products is not None and (type(import_products) is not bool or (import_products and mode != 'manual')):
        raise HTTPException(422, 'A seleção Produtos exige um perfil JSON revisado.')
    if type(import_finance) is not bool or (import_finance and (mode != 'manual' or not import_movements)):
        raise HTTPException(422, 'Movimentações Financeiras exigem movimentações de entrada e saída e perfil JSON.')
    if type(use_barcode) is not bool:
        raise HTTPException(422, 'Seleção de código de barras inválida.')
    if mode not in {'automatic','manual'}:raise HTTPException(422,'Modo inválido.')
    try:
        config = load_profile(profile) if mode == 'manual' else None
        database = config['database'] if config else app.MONGO_DATABASE
        configurations = [config] + [profile_for_source(config, source) for source in DFE_SOURCES] if config else []
    except (ValueError,TypeError,AttributeError) as exc:raise HTTPException(422,str(exc)) from None
    if import_people and not any(isinstance(value.get('person_template'), dict) for value in configurations):
        raise HTTPException(422, 'O perfil precisa definir person_template para importar Pessoas.')
    if import_products:
        if not any(isinstance(value.get('product_catalog'), dict) or (
                import_movements and isinstance(value.get('product_template'), dict)) for value in configurations):
            raise HTTPException(422, 'Use product_catalog ou selecione movimentações com um product_template no perfil.')
    if import_finance and not any(isinstance(value.get('finance'), dict) for value in configurations):
        raise HTTPException(422, 'O perfil precisa definir finance para importar Movimentações Financeiras.')
    editor.database_name(database)
    if database in editor.SYSTEM_DATABASES:raise HTTPException(403,'Banco de sistema não permitido.')
    job_id=secrets.token_urlsafe(24);now=time.time()
    with _lock,connect() as db:
        db.execute('INSERT INTO jobs(id,owner,connection_key,database_name,mode,profile,state,created,updated,use_barcode,import_movements,import_people,import_products,import_finance) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)',
            (job_id,owner,identity(),database,mode,profile,'uploading',now,now,use_barcode,import_movements,import_people,import_products,import_finance))
    folder(job_id)
    return snapshot(job_id,owner)


def reserve(job_id,owner,upload_id,name,*,source=None):
    owned(job_id,owner)
    with _lock,connect() as db:
        db.execute('BEGIN IMMEDIATE')
        job=db.execute('SELECT state FROM jobs WHERE id=?',(job_id,)).fetchone()
        if job['state']!='uploading':raise HTTPException(409,'Os envios desta importação já foram encerrados.')
        existing=db.execute('SELECT id,job_id,upload_id,name,path,bytes,sha,status FROM files WHERE job_id=? AND upload_id=?',(job_id,upload_id)).fetchone()
        if existing and existing['status']=='receiving' and existing['id'] in _uploads:
            raise HTTPException(409,'Este envio ainda está em andamento.')
        leaf=secrets.token_hex(16)+'.xml'
        if existing and existing['status'] not in {'upload_error','receiving','awaiting_upload'}:return dict(existing),None
        if existing:
            db.execute("UPDATE files SET path=?,status='receiving',message=NULL,raw=NULL WHERE id=?",(leaf,existing['id']))
            file_id=existing['id']
        else:
            file_id=db.execute('INSERT INTO files(job_id,upload_id,name,path,status,source) VALUES(?,?,?,?,?,?)',
                (job_id,upload_id,name,leaf,'receiving',source)).lastrowid
        _uploads.add(file_id)
    return {'id':file_id,'path':leaf,'status':'receiving'},folder(job_id)/leaf


def stored_upload(job_id,owner,upload_id):
    owned(job_id,owner)
    with connect() as db:
        row=db.execute('SELECT id,status,bytes,sha FROM files WHERE job_id=? AND upload_id=?',(job_id,upload_id)).fetchone()
    return {'stored':bool(row and row['status'] not in {'receiving','upload_error','awaiting_upload'}),
            'bytes':row['bytes'] if row else 0}


def upload_slot(job_id,owner,metadata):
    job=owned(job_id,owner)
    if job['state']!='uploading':raise HTTPException(409,'Os envios já foram encerrados.')
    name=metadata.get('name')
    if not isinstance(name,str) or not name:raise HTTPException(422,'Informe o nome do arquivo.')
    source = metadata.get('source')
    if source is not None and (not isinstance(source, str) or source not in DFE_SOURCES):
        raise HTTPException(422, 'Categoria de XML inválida.')
    identity_fields = [name, metadata.get('size'), metadata.get('modified')]
    if source is not None:
        identity_fields.append(source)
    digest=hashlib.sha256(json.dumps(identity_fields,
                                  ensure_ascii=False,separators=(',',':')).encode('utf-8')).hexdigest()
    with connect() as db:
        db.execute('INSERT OR IGNORE INTO files(job_id,upload_id,name,path,status,source) VALUES(?,?,?,?,?,?)',
                   (job_id,digest,name,secrets.token_hex(16)+'.xml','awaiting_upload',source))
    return {'upload_id':digest,**stored_upload(job_id,owner,digest)}


def uploaded(file_id,size,sha):
    with connect() as db:
        db.execute("UPDATE files SET bytes=?,sha=?,status='uploaded' WHERE id=?",(size,sha,file_id))
    with _lock:_uploads.discard(file_id)


class BlobUpload:
    """Known-length uploads stream into an incremental SQLite BLOB, not RAM."""
    def __init__(self,file_id,size):
        with connect() as metadata:job_id=metadata.execute('SELECT job_id FROM files WHERE id=?',(file_id,)).fetchone()[0]
        self.context=payload_connection(job_id);self.db=self.context.__enter__();self.expected=size;self.written=0;self.closed=False
        try:
            self.db.execute('BEGIN IMMEDIATE')
            self.db.execute('INSERT INTO payloads(id,raw) VALUES(?,zeroblob(?)) ON CONFLICT(id) DO UPDATE SET raw=excluded.raw',(file_id,size))
            self.file_id=file_id;self.db.commit()
        except BaseException:
            self.context.__exit__(*__import__('sys').exc_info());raise
    def write(self,chunk):
        # Release SQLite's writer between chunks, so a slow upload cannot block
        # the other uploads or progress updates for the duration of its body.
        with self.db.blobopen('payloads','raw',self.file_id,readonly=False) as blob:
            blob.seek(self.written);blob.write(chunk)
        self.db.commit();self.written+=len(chunk)
    def finish(self):
        if self.written!=self.expected:
            self.abort();raise ValueError('O tamanho recebido difere do arquivo anunciado. Retome o envio.')
        self.closed=True;self.context.__exit__(None,None,None)
    def abort(self):
        if self.closed:return
        self.closed=True
        error=ValueError('Envio interrompido')
        self.context.__exit__(ValueError,error,None)


def upload_error(file_id):
    with connect() as db:db.execute("UPDATE files SET status='upload_error',message='Envio interrompido; selecione os arquivos novamente para retomar.' WHERE id=?",(file_id,))
    with _lock:_uploads.discard(file_id)


def import_keys(job_id, owner, keys, source=None):
    """Stage authorized XML already archived in the job's target database."""
    from pymongo.errors import PyMongoError

    job = owned(job_id, owner)
    if source is not None and (not isinstance(source, str) or source not in DFE_SOURCES):
        raise HTTPException(422, 'Tipo de documento inválido para a lista de chaves.')
    if source in ('purchase', 'incoming', 'cfe') and job['mode'] != 'manual':
        raise HTTPException(422, 'O tipo selecionado exige um perfil JSON revisado.')
    if job['state'] != 'uploading':
        raise HTTPException(409, 'Os envios desta importação já foram encerrados.')
    if not isinstance(keys, list) or not 1 <= len(keys) <= 50:
        raise HTTPException(422, 'Envie de 1 a 50 chaves por consulta.')
    if any(not isinstance(key, str) or len(key) > 64 for key in keys):
        raise HTTPException(422, 'Informe as chaves como texto.')
    keys = dict.fromkeys(re.sub(r'[\s.-]+', '', key) for key in keys)
    allow_cfe = job['mode'] == 'manual' and 'cfe' in profile_for_source(load_profile(job['profile']), 'cfe')
    models = {'55', '65', '59'} if allow_cfe else {'55', '65'}
    selected_model = {'purchase': '55', 'incoming': '55', 'outgoing': '55', 'nfce': '65', 'cfe': '59'}.get(source)
    results = []
    try:
        with editor.editor_client() as client:
            collection = editor.selected_collection(client, job['database_name'], 'XmlMovimentacoes')
            for key in keys:
                result = {'key': key, 'status': 'unavailable'}
                results.append(result)
                if not re.fullmatch(r'[0-9]{44}', key) or not key_valid(key):
                    result['message'] = 'Chave inválida. Confira os 44 dígitos, o modelo e o dígito verificador.'
                    continue
                if key[20:22] not in models:
                    result['message'] = ('CF-e por chave exige um perfil JSON com mapeamento cfe.'
                                         if key[20:22] == '59' else 'Modelo de documento não suportado.')
                    continue
                if selected_model and key[20:22] != selected_model:
                    result['message'] = 'A chave não corresponde ao tipo de documento selecionado.'
                    continue
                try:
                    raw = archived_xml(collection, key, source=source,
                                       allow_manual_entry=job['mode'] == 'manual')
                except ValueError as exc:
                    result['message'] = str(exc)
                    continue
                upload_id = 'key-' + key + ('-' + source if source else '')
                digest = hashlib.sha256(raw).hexdigest()
                suffix = '-CFe.xml' if key[20:22] == '59' else '-procNFe.xml'
                row, target = reserve(job_id, owner, upload_id, key + suffix, source=source)
                if target is None:
                    if row['sha'] != digest:
                        result['message'] = 'O XML guardado mudou. Crie outra importação para revisar a nova versão.'
                        continue
                else:
                    try:
                        with target.open('xb') as output:
                            output.write(raw)
                        uploaded(row['id'], len(raw), digest)
                    except BaseException:
                        upload_error(row['id'])
                        raise
                result.update(status='stored', message='XML autorizado localizado na base.')
    except PyMongoError:
        raise HTTPException(503, 'Não foi possível consultar XmlMovimentacoes. Confira a conexão e a permissão de leitura.') from None
    return {'results': results}


def archived_xml(collection, key, *, source=None, allow_manual_entry=True):
    if collection.find_one({'ChaveAcesso': key, **ARCHIVED_CANCELLATION}, {'_id': 1}, max_time_ms=5000):
        raise ValueError('Há evento de cancelamento para esta chave na base.')
    raw = None
    selected = None
    query = {'ChaveAcesso': key, 'EventoCancelamento': {'$ne': True}}
    with collection.find(query, {'Xml': 1, '_id': 0}).max_time_ms(5000).batch_size(1) as candidates:
        for candidate in candidates:
            xml = candidate.get('Xml')
            if not isinstance(xml, str):
                continue
            content = xml.encode('utf-8')
            try:
                parsed = (parse_cfe(read_xml(content)) if key[20:22] == '59'
                          else parse_xml(content, issuer=key[6:20]))
            except ValueError:
                continue
            if parsed['access_key'] != key:
                continue
            if raw is not None and raw != content:
                raise ValueError('Há versões diferentes do XML autorizado para esta chave.')
            raw = content
            selected = parsed
    if raw is None:
        if key[20:22] == '59':
            raise ValueError('XML de venda CF-e não encontrado na base. Selecione o arquivo original.')
        raise ValueError('XML autorizado de produção não encontrado na base. Envie o nfeProc se conseguir recuperá-lo.')
    if key[20:22] != '59':
        native_type, _, _ = nfe_route(selected, source)
        if not allow_manual_entry and native_type == 'NotaFiscalEletronicaEntrada':
            raise ValueError('A NF-e de entrada exige um perfil JSON revisado.')
    return raw


def upload_local(job_id,owner,path,upload_id=None):
    """CLI copy uses bounded I/O too; it never loads the whole directory in RAM."""
    path=Path(path);upload_id=upload_id or secrets.token_hex(16)
    row,target=reserve(job_id,owner,upload_id,path.name)
    digest=hashlib.sha256();size=0
    blob=None
    try:
        if target and path.stat().st_size<=MONGO_DOCUMENT_BYTES+3:
            blob=BlobUpload(row['id'],path.stat().st_size)
        with path.open('rb') as source, (target.open('xb') if target and blob is None else nullcontext(None)) as output:
            while True:
                chunk=source.read(1024*1024)
                if not chunk:break
                digest.update(chunk);size+=len(chunk)
                if blob:blob.write(chunk)
                elif output:output.write(chunk)
        if blob:blob.finish();blob=None
        if target:uploaded(row['id'],size,digest.hexdigest())
        elif row['sha']!=digest.hexdigest():raise ValueError('Um envio retomado tem conteúdo diferente. Crie outra importação.')
    except Exception:
        if blob:blob.abort()
        if target:upload_error(row['id'])
        raise


def update_file(file_id,*,durable=False,**changes):
    fields=','.join(key+'=?' for key in changes)
    with connect(durable=durable) as db:db.execute('UPDATE files SET '+fields+' WHERE id=?',(*changes.values(),file_id))


def next_file(job_id,status,*,kind=None):
    with connect() as db:
        projection='id,job_id,upload_id,name,path,bytes,sha,key,signature,full_signature,kind,status,message,summary,sale_path,result,sale' if status=='ready' else '*'
        sql='SELECT '+projection+' FROM files WHERE job_id=? AND status=?'
        args=[job_id,status]
        if kind:sql+=' AND kind=?';args.append(kind)
        row=db.execute(sql+' ORDER BY id LIMIT 1',args).fetchone()
    return dict(row) if row else None


def signature(node):
    return hashlib.sha256(json.dumps(tree_signature(node),ensure_ascii=False).encode('utf-8')).hexdigest()


def index_file(job_id,row,*,allow_manual_entry=False,allow_cfe=False):
    path=folder(job_id)/row['path']
    # Every original XML is stored in one Mongo document. Avoid reading an
    # impossible document into memory; this is Mongo's bound, not an upload cap.
    if row['bytes'] > MONGO_DOCUMENT_BYTES+3:
        raise ValueError('O XML não cabe em um documento BSON de 16 MiB, limite nativo do MongoDB.')
    raw=raw_bytes(job_id,row)
    if hashlib.sha256(raw).hexdigest()!=row['sha']:
        raise ValueError('O arquivo local diverge do envio original. Envie novamente em outra importação.')
    if raw[:1] in b'\x03\x83\x8b\xcb\xf5' and (row['name'].lower().endswith('.dbf') or len(raw) > 32):
        # DBF legacy import: parse and expose for review/mapping; no blind native write.
        try:
            from insight_dbf import dbf_preview
            preview = dbf_preview(raw, row['name'])
            update_file(row['id'], kind='dbf', summary=json.dumps({'dbf_rows': preview['rows'], 'columns': preview['columns'][:40]}),
                        status='invalid', message='Arquivo DBF identificado. Confira as colunas e use perfil/mapeamento revisado antes da conversão nativa.')
            return
        except ValueError as exc:
            if row['name'].lower().endswith('.dbf'):
                raise
    root=read_xml(raw)
    if root.tag == 'CFe':
        if row.get('source') not in (None, 'cfe'):
            raise ValueError('CF-e não corresponde à pasta selecionada.')
        summary = parse_cfe(root)
        if allow_cfe:
            update_file(row['id'], kind='authorized', key='1:CFe'+summary['access_key'],
                        signature=signature(root), full_signature=signature(root),
                        summary=json.dumps(summary), status='indexed')
            return
        update_file(row['id'], kind='cfe', summary=json.dumps(summary), status='invalid',
                    message='CF-e identificado. A conversão nativa ainda não está disponível.')
        return
    if row.get('source') == 'cfe':
        raise ValueError('A pasta de CF-e aceita apenas XML de venda CF-e/SAT.')
    if root.tag=='{'+NS['n']+'}NFe':
        info=root.find('n:infNFe',NS);node=root;kind='signed'
    else:
        info=root.find('n:NFe/n:infNFe',NS);node=root.find('n:NFe',NS);kind='authorized'
    if info is None or node is None:raise ValueError('XML não é uma NFC-e NFe/nfeProc.')
    source = row.get('source')
    expected = {'purchase': ('55', '1'), 'incoming': ('55', '0'),
                'outgoing': ('55', '1'), 'nfce': ('65', '1')}
    if source in expected:
        model = info.findtext('n:ide/n:mod', namespaces=NS)
        direction = info.findtext('n:ide/n:tpNF', namespaces=NS)
        if (model, direction) != expected[source]:
            raise ValueError('Modelo ou direção do XML não corresponde à pasta selecionada.')
    environment=info.findtext('n:ide/n:tpAmb',namespaces=NS) or ''
    if kind=='authorized':
        parsed=parse_xml(raw,issuer=info.findtext('n:emit/n:CNPJ',namespaces=NS) or '',environment=environment)
        native_type, _, _ = nfe_route(parsed, source)
        summary={key:parsed[key] for key in ('access_key','number','series','total','issued_at_original','xml_sha256','model','direction')}
        summary['installment_count'] = len(parsed['installments'])
        if native_type in {'NotaFiscalCompra', 'NotaFiscalEletronicaEntrada'} and not allow_manual_entry:
            update_file(row['id'], kind=kind, summary=json.dumps(summary), status='invalid',
                        message='A conversão nativa desta categoria ainda não está disponível.')
            return
    else:summary={'access_key':info.get('Id','')[3:]}
    key=environment+':'+info.get('Id','')
    update_file(row['id'],kind=kind,key=key,signature=signature(node),full_signature=signature(root),
                status='indexed',summary=json.dumps(summary),message=None)


def resolve_pair(row):
    identity = (row['job_id'], row['key'])
    with connect() as db:
        sources = db.execute(
            "SELECT DISTINCT source FROM files WHERE job_id=? AND key=? AND kind IN ('authorized', 'signed') "
            "AND source IS NOT NULL LIMIT 2", identity).fetchall()
        first = db.execute(
            "SELECT id,source,signature,full_signature FROM files WHERE job_id=? AND key=? AND kind='authorized' "
            "ORDER BY source IS NULL, id LIMIT 1", identity).fetchone()
    if len(sources) > 1:
        update_file(row['id'], status='invalid',
                    message='A mesma chave está em pastas com tipos diferentes. Revise a origem do XML.')
        return False
    if row['kind']=='signed':
        same=first is not None and first['signature']==row['signature']
        if same and row.get('source') == 'purchase' and first['source'] is None:
            update_file(row['id'], status='invalid',
                        message='Selecione o nfeProc autorizado na pasta de notas de compra.')
            return False
        update_file(row['id'],status='duplicate' if same else 'invalid',message=
            'NFe sem protocolo representada pelo nfeProc autorizado.' if same else 'NFe sem protocolo autorizado correspondente ou conteúdo diferente.')
        return False
    if first and first['id']!=row['id']:
        same=first['full_signature']==row['full_signature']
        update_file(row['id'],status='duplicate' if same else 'invalid',message=
            'XML autorizado repetido.' if same else 'Mesma chave com XMLs autorizados diferentes.')
        return False
    return True


def incoming(job_id,row):
    raw=raw_bytes(job_id,row);root=read_xml(raw)
    info=root.find('n:NFe/n:infNFe',NS)
    parsed=parse_xml(raw,issuer=info.findtext('n:emit/n:CNPJ',namespaces=NS),environment=info.findtext('n:ide/n:tpAmb',namespaces=NS))
    return {'name':row['name'],'source':row.get('source'),'raw':raw,'info':info,'parsed':parsed}


def write_sale(job_id,row,sale):
    if 'movement' in sale:
        validate_native_documents(sale['movement'],sale['xml'])
    with payload_connection(job_id) as db:
        db.execute('INSERT INTO payloads(id,sale) VALUES(?,?) ON CONFLICT(id) DO UPDATE SET sale=excluded.sale',
                   (row['id'],sqlite3.Binary(BSON.encode(sale))))
    update_file(row['id'],status='ready',sale_path='sqlite',message=None,
                collections=json.dumps(sorted(sale_collections([sale])) if 'movement' in sale else []))


def validation_checkpoint(job_id):
    # Group re-creatable validation progress instead of fsyncing every XML.
    # Flush payloads first, then the metadata that refers to them. Row zero is
    # an internal checkpoint marker and is never part of the import manifest.
    with payload_connection(job_id,durable=True) as db:
        db.execute('INSERT INTO payloads(id,sale) VALUES(0,NULL) ON CONFLICT(id) DO UPDATE SET sale=NULL')
    with connect(durable=True) as db:db.execute('UPDATE jobs SET updated=? WHERE id=?',(time.time(),job_id))


def raw_bytes(job_id,row):
    if row.get('raw') is not None:return row['raw']
    with payload_connection(job_id) as db:payload=db.execute('SELECT raw FROM payloads WHERE id=?',(row['id'],)).fetchone()
    if payload and payload['raw'] is not None:return payload['raw']
    return (folder(job_id)/row['path']).read_bytes()


def read_sale(job_id,row):
    if row.get('sale') is not None:return BSON(row['sale']).decode()
    if row['sale_path']=='sqlite':
        with payload_connection(job_id) as db:payload=db.execute('SELECT sale FROM payloads WHERE id=?',(row['id'],)).fetchone()
        if payload and payload['sale'] is not None:return BSON(payload['sale']).decode()
        raise ValueError('Documento local convertido indisponível. Valide novamente antes de inserir.')
    return BSON((folder(job_id)/row['sale_path']).read_bytes()).decode()


def running(job_id):
    if _stopping.is_set():return False
    with connect() as db:row=db.execute('SELECT state FROM jobs WHERE id=?',(job_id,)).fetchone()
    return row is not None and row['state'] not in {'pausing','paused'}


def heartbeat():
    while True:
        time.sleep(5)
        with _lock:jobs=list(_active)
        if jobs:
            try:
                with connect() as db:
                    db.executemany('UPDATE jobs SET lease=? WHERE id=? AND worker=?',[(time.time()+30,j,_worker) for j in jobs])
            except (OSError,sqlite3.Error):pass


def launch(job_id,owner,phase,*,session_token=None,background=True):
    global _heartbeat_started
    job=owned(job_id,owner)
    if phase == 'apply' and not job['import_movements']:
        raise HTTPException(409, 'A gravação de movimentações está desmarcada neste lote.')
    with _lock,connect() as db:
        db.execute('BEGIN IMMEDIATE')
        current=db.execute('SELECT * FROM jobs WHERE id=?',(job_id,)).fetchone()
        if job_id in _active or (current['lease']>time.time() and current['worker']!=_worker):
            raise HTTPException(409,'Esta importação já tem um processamento em andamento.')
        receiving=db.execute("SELECT COUNT(*) FROM files WHERE job_id=? AND status IN ('receiving','upload_error','awaiting_upload')",(job_id,)).fetchone()[0]
        if receiving:raise HTTPException(409,'Retome os envios interrompidos antes de processar.')
        if not db.execute('SELECT COUNT(*) FROM files WHERE job_id=?',(job_id,)).fetchone()[0]:
            raise HTTPException(422,'Envie os XMLs antes de validar.')
        uncertain=db.execute("SELECT COUNT(*) FROM files WHERE job_id=? AND status IN ('applying','unknown')",(job_id,)).fetchone()[0]
        if uncertain:raise HTTPException(409,'Há escrita de resultado incerto. Confira o diário antes de criar outra tentativa.')
        if phase=='validate' and db.execute("SELECT COUNT(*) FROM files WHERE job_id=? AND status='imported'",(job_id,)).fetchone()[0]:
            raise HTTPException(409,'Esta importação já tem vendas aplicadas. Retome apenas as pendentes.')
        if phase=='apply':
            invalid=db.execute("SELECT COUNT(*) FROM files WHERE job_id=? AND status IN ('invalid','uploaded','indexed')",(job_id,)).fetchone()[0]
            if invalid:raise HTTPException(422,'Conclua a validação e resolva os erros antes de inserir.')
        db.execute('UPDATE jobs SET state=?,phase=?,error=NULL,worker=?,lease=?,updated=? WHERE id=?',
            ('queued',phase,_worker,time.time()+30,time.time(),job_id))
        _active[job_id]=None
        _stopping.clear()
        if not _heartbeat_started:
            threading.Thread(target=heartbeat,name='nfce-heartbeat',daemon=True).start();_heartbeat_started=True
    if background:
        future=_pool.submit(work,job_id,owner,phase,session_token)
        with _lock:
            if job_id in _active:_active[job_id]=future
    else:work(job_id,owner,phase,session_token)
    return snapshot(job_id,owner)


def work(job_id,owner,phase,session_token):
    try:
        job=owned(job_id,owner)
        if not running(job_id):return
        with connect() as db:db.execute('UPDATE jobs SET state=? WHERE id=?',(phase+'ing' if phase=='apply' else 'validating',job_id))
        if phase=='validate':validate_job(job_id,job,owner)
        else:apply_job(job_id,job,owner,session_token)
    except Exception as exc:
        message=str(exc.detail) if isinstance(exc,HTTPException) else str(exc) if isinstance(exc,ValueError) else 'Processamento interrompido: '+type(exc).__name__+'. Retome para continuar.'
        try:
            with connect() as db:db.execute("UPDATE jobs SET state='paused',error=?,updated=? WHERE id=?",(message,time.time(),job_id))
        except (OSError,sqlite3.Error):pass
    finally:
        with _lock:_active.pop(job_id,None)
        try:
            with connect() as db:
                db.execute("UPDATE jobs SET state='paused' WHERE id=? AND state='pausing'",(job_id,))
                db.execute('UPDATE jobs SET lease=0,worker=NULL,updated=? WHERE id=?',(time.time(),job_id))
        except (OSError,sqlite3.Error):pass


def validate_job(job_id,job,owner):
    from insight_nfce_api import native_document_filter

    profile=load_profile(job['profile']) if job['mode']=='manual' else None
    cfe_profile = profile_for_source(profile, 'cfe') if profile else None
    catalog_only = bool(not job['import_movements'] and (job['import_people'] or job['import_products']))
    while running(job_id):
        row=next_file(job_id,'uploaded')
        if not row:break
        try:
            if job['mode'] == 'manual':
                index_file(job_id, row, allow_manual_entry=True, allow_cfe='cfe' in cfe_profile)
            else:
                index_file(job_id, row)
        except ValueError as exc:update_file(row['id'],status='invalid',message=str(exc))
    if not running(job_id):return
    with ExitStack() as stack:
        client=None;discovery=None;processed=0
        while running(job_id):
            owned(job_id,owner)
            row=next_file(job_id,'indexed')
            if not row:break
            if not resolve_pair(row):continue
            try:
                if profile:
                    raw = raw_bytes(job_id, row)
                    sale = (build_cfe(raw, cfe_profile, include_products=job['import_products'] != 0,
                                      include_people=bool(job['import_people']), use_barcode=bool(job['use_barcode']),
                                      catalog_only=catalog_only)
                            if row['key'].startswith('1:CFe')
                            else build_sale(raw, profile, source=row.get('source'),
                                            include_finance=job['import_finance'] != 0,
                                            require_finance=job['import_finance'] == 1,
                                            include_products=job['import_products'] != 0,
                                            include_people=bool(job['import_people']), use_barcode=bool(job['use_barcode']),
                                            catalog_only=catalog_only))
                else:
                    if client is None:
                        client=stack.enter_context(editor.editor_client())
                        database = client[job['database_name']]
                        discovery = Discovery(database, use_barcode=True) if job['use_barcode'] else Discovery(database)
                    item=incoming(job_id,row)
                    native_type, company_cnpj, _ = nfe_route(item['parsed'], item['source'])
                    company_id, _ = discovery.company(company_cnpj, item['parsed']['model'], native_type=native_type)
                    query = native_document_filter({
                        'EmpresaReferencia': company_id, '_t': native_type,
                        'Numero': int(item['parsed']['number']), 'Serie': item['parsed']['series'],
                        'ChaveAcesso': item['parsed']['access_key'],
                    })
                    previous=client[job['database_name']].Movimentacoes.find_one(query,{'ChaveAcesso':1},max_time_ms=5000)
                    if previous:
                        if previous.get('ChaveAcesso')!=item['parsed']['access_key']:raise ValueError('Número/série existem com outra chave.')
                        update_file(row['id'],status='duplicate',message='Venda já existe; não será alterada.');continue
                    sale=discovery.build(item)
                write_sale(job_id,row,sale)
                processed+=1
                if processed%64==0:validation_checkpoint(job_id)
            except ValueError as exc:update_file(row['id'],status='invalid',message=str(exc))
    if running(job_id):
        validation_checkpoint(job_id)
        with connect(durable=True) as db:
            bad=db.execute("SELECT COUNT(*) FROM files WHERE job_id=? AND status='invalid'",(job_id,)).fetchone()[0]
            db.execute('UPDATE jobs SET state=? WHERE id=?',('blocked' if bad else 'ready',job_id))


def authorized_collections(job_id):
    with connect() as db:
        rows = db.execute("SELECT collections FROM files WHERE job_id=? AND collections IS NOT NULL", (job_id,)).fetchall()
    names = {'Movimentacoes', 'XmlMovimentacoes'}
    for row in rows:
        names.update(json.loads(row['collections']))
    return names


def authorize_apply(job_id,owner,token,confirmation):
    job=owned(job_id,owner)
    if not job['import_movements']:
        raise HTTPException(409, 'A gravação de movimentações está desmarcada neste lote.')
    if confirmation!='IMPORTAR '+job['database_name']:raise HTTPException(422,'Confirme o banco de destino.')
    access=editor.session(token,owner,job['database_name'])
    collections = authorized_collections(job_id)
    for collection in collections:
        for action in ('insert','find'):
            if not editor.permitted(access['grants'],job['database_name'],collection,action):
                raise HTTPException(403,'Permissão ausente: '+action+' em '+collection)
    if collections.intersection({'Pagamentos', 'Recebimentos'}):
        check_finance_dependencies(job_id, job['database_name'], access)
    if job['import_people']:
        database = job['database_name']
        if not editor.permitted(access['grants'], database, 'Pessoas', 'find'):
            raise HTTPException(403, 'Permissão ausente: find em Pessoas')
        with editor.editor_client() as client:
            people = editor.selected_collection(client, database, 'Pessoas')
            for sale in iter_sales(job_id, status='ready'):
                for expected in sale.get('people', []):
                    reference = sale['movement'].get('Pessoa', {}).get('PessoaReferencia')
                    field = 'Cpf' if expected.get('Cpf') else 'Cnpj'
                    actual = people.find_one({'_id': reference}, {field: 1, 'Ativo': 1}, max_time_ms=5000)
                    if (actual is None or actual.get('Ativo') is not True
                            or re.sub(r'\D', '', str(actual.get(field, ''))) != expected[field]):
                        raise HTTPException(409, 'Confira o vínculo e cadastre a pessoa revisada antes de gravar a movimentação.')
    if job['import_products']:
        products, links = reviewed_products(job_id, owner, ready_only=True)
        if products or links:
            database = job['database_name']
            with editor.editor_client() as client:
                for collection, documents in (('ProdutosServicos', products),
                                               ('ProdutosServicosEmpresa', links)):
                    if not editor.permitted(access['grants'], database, collection, 'find'):
                        raise HTTPException(403, 'Permissão ausente: find em ' + collection)
                    coll = editor.selected_collection(client, database, collection)
                    for ident, expected in documents.items():
                        actual = coll.find_one({'_id': ident}, max_time_ms=5000)
                        if actual is None or any(actual.get(field) != expected.get(field)
                                                 for field in ('Ativo', 'CodigoInterno', 'CodigoBarras',
                                                               'EmpresaReferencia', 'ProdutoServicoReferencia')):
                            raise HTTPException(409, 'Cadastre os produtos e vínculos revisados antes de gravar as movimentações.')


def check_finance_dependencies(job_id, database, access):
    for collection in ('Pessoas', 'Contas'):
        if not editor.permitted(access['grants'], database, collection, 'find'):
            raise HTTPException(403, 'Permissão ausente: find em ' + collection)
    with editor.editor_client() as client:
        people = editor.selected_collection(client, database, 'Pessoas')
        accounts = editor.selected_collection(client, database, 'Contas')
        checked = set()
        for sale in iter_sales(job_id, status='ready'):
            finance = sale.get('finance')
            if not finance:
                continue
            titles = editor.selected_collection(client, database, finance['collection'])
            for title in finance['documents']:
                for collection, entity, reference in (
                    ('Pessoas', people, title['PessoaReferencia']),
                    ('Contas', accounts, title['ContaReferencia']),
                ):
                    key = (collection, reference)
                    if key not in checked:
                        found = entity.find_one({'_id': reference}, {'Ativo': 1}, max_time_ms=5000)
                        if found is None or found.get('Ativo') is False:
                            raise HTTPException(409, 'Título financeiro com vínculo ausente ou inativo em ' + collection + '.')
                        checked.add(key)
                existing = titles.find_one({'_id': title['_id']}, max_time_ms=5000)
                if existing is not None and not editor.same_document(existing, title):
                    raise HTTPException(409, 'Já existe um título diferente com a identidade preparada.')


def apply_job(job_id,job,owner,session_token):
    from insight_nfce_api import apply_sale
    private_tokens=[]
    try:
      with editor.editor_client() as client:
        while running(job_id):
            owned(job_id,owner)
            access=editor.session(session_token,owner,job['database_name'])
            # The explicit immutable job remains authorized. Renew a private worker
            # session through the normal privilege checks, without prolonging the
            # browser's general editor token or imposing a total job deadline.
            if access['expires']-time.time()<30:
                renewed=editor.unlock(editor.UnlockRequest(database=job['database_name'],confirmation='EDITAR '+job['database_name']),owner)
                session_token=renewed['token'];private_tokens.append(session_token)
            row=next_file(job_id,'ready')
            if not row:break
            sale=read_sale(job_id,row)
            update_file(row['id'],status='applying',durable=True)
            result=apply_sale(sale,job['database_name'],session_token,owner,client=client)
            status={'imported':'imported','duplicate':'duplicate','stopped':'unknown'}[result['status']]
            update_file(row['id'],status=status,result=json.dumps(result),message=result.get('message'),durable=True)
            if status=='unknown':
                with connect() as db:db.execute("UPDATE jobs SET state='interrupted',error='Confira o diário antes de repetir uma escrita incerta.' WHERE id=?",(job_id,))
                return
    finally:
        with editor._lock:
            for token in private_tokens:editor._sessions.pop(editor.digest(token),None)
    if running(job_id):
        with connect() as db:db.execute("UPDATE jobs SET state='complete' WHERE id=?",(job_id,))


def pause(job_id,owner):
    owned(job_id,owner)
    with connect() as db:db.execute("UPDATE jobs SET state=CASE WHEN lease>0 THEN 'pausing' ELSE 'paused' END WHERE id=?",(job_id,))
    return snapshot(job_id,owner)


def snapshot(job_id,owner,offset=0,page_size=50):
    job=owned(job_id,owner)
    # Pagination is a rendering window, not a limit on the job's number of files.
    offset=max(0,int(offset));page_size=max(1,min(200,int(page_size)))
    with connect() as db:
        metrics=db.execute('SELECT status,n,bytes FROM metrics WHERE job_id=? AND n>0',(job_id,)).fetchall()
        counts={row['status']:row['n'] for row in metrics}
        total=sum(counts.values())
        size=sum(row['bytes'] for row in metrics)
        rows=db.execute('SELECT id,name,status,message,summary,result,source FROM files WHERE job_id=? ORDER BY id LIMIT ? OFFSET ?',
                        (job_id,page_size,offset)).fetchall()
    values=[]
    for row in rows:
        value={'id':row['id'],'file':row['name'],'status':row['status'],'message':row['message'],'source':row['source']}
        if row['summary']:
            summary = json.loads(row['summary'])
            if summary.get('model') == '59':
                summary.pop('items', None)
                summary.pop('payments', None)
            value.update(summary)
        if row['result']:value['result']=json.loads(row['result'])
        values.append(value)
    in_progress=job['state'] in {'queued','validating','applying','pausing'}
    orphan=in_progress and job['lease']<time.time()
    return {'id':job_id,'database':job['database_name'],'mode':job['mode'],'use_barcode':bool(job['use_barcode']),
            'import_movements': bool(job['import_movements']),
            'import_people': bool(job['import_people']),
            'import_products': bool(job['import_products']),
            'import_finance': None if job['import_finance'] is None else bool(job['import_finance']),
            'state':'interrupted' if orphan and job['phase']=='apply' else 'paused' if orphan else job['state'],
            'phase':job['phase'],'error':job['error'],'total':total,'bytes':size,'counts':counts,
            'ready':counts.get('ready',0),'valid':not counts.get('invalid') and not counts.get('unknown') and
              not counts.get('applying') and counts.get('ready',0)>0 and (job['state'] in {'ready','paused','interrupted'} or orphan),
            'offset':offset,'page_size':page_size,'has_more':offset+len(rows)<total,'rows':values,'notice':NOTICE}


def document(job_id,owner,file_id):
    owned(job_id,owner)
    with connect() as db:row=db.execute('SELECT * FROM files WHERE id=? AND job_id=?',(file_id,job_id)).fetchone()
    if row and row['kind'] == 'cfe' and row['summary']:
        return {'cfe_review': json.loads(row['summary'])}
    if row and row['kind'] == 'dbf':
        from insight_dbf import dbf_preview
        return {'dbf_review': dbf_preview(raw_bytes(job_id, dict(row)), row['name'], preset='generico')}
    if not row or not row['sale_path']:raise HTTPException(404,'Documento convertido não disponível.')
    return read_sale(job_id,dict(row))


def iter_sales(job_id, *, status=None):
    previous=0
    while True:
        with connect() as db:
            sql = 'SELECT id,sale,sale_path FROM files WHERE job_id=? AND sale_path IS NOT NULL AND id>?'
            params = (job_id, previous)
            if status is not None:
                sql += ' AND status=?'
                params += (status,)
            row = db.execute(sql + ' ORDER BY id LIMIT 1', params).fetchone()
        if not row:break
        previous=row['id'];yield read_sale(job_id,dict(row))


def latest_purchase_costs(sales):
    """Select one cost per company product from prepared purchase documents."""
    latest = {}
    for sale in sales:
        for candidate in sale.get('purchase_cost_candidates', []):
            product = candidate['company_product_id']
            moment = native_issue_time(candidate['issued_at'])
            current = latest.get(product)
            if current is None or moment > current[0]:
                latest[product] = (moment, candidate, False)
            elif moment == current[0]:
                existing = current[1]
                conflict = current[2] or Decimal(candidate['unit_cost']) != Decimal(existing['unit_cost'])
                chosen = min((existing, candidate), key=lambda value: (value['access_key'], value['line']))
                latest[product] = (moment, chosen, conflict)
    return [dict(candidate, conflict=conflict) for _, candidate, conflict in latest.values()]


def reviewed_people(job_id, owner):
    job = owned(job_id, owner)
    if not job['import_people'] or job['state'] not in {'ready', 'complete'}:
        raise HTTPException(409, 'Selecione Pessoas e conclua a validação do lote.')
    people, references = {}, {}
    for sale in iter_sales(job_id):
        for person in sale.get('people', []):
            ident = person['_id']
            document = person.get('Cpf') or person.get('Cnpj')
            if references.setdefault(document, ident) != ident:
                raise HTTPException(409, 'O mesmo CPF ou CNPJ aponta para cadastros diferentes no lote.')
            previous = people.setdefault(ident, person)
            if previous != person:
                raise HTTPException(409, 'O lote contém cadastros diferentes para o mesmo CPF ou CNPJ.')
    return people


def reviewed_products(job_id, owner, *, ready_only=False):
    job = owned(job_id, owner)
    states = {'ready', 'complete', 'paused', 'interrupted'} if ready_only else {'ready', 'complete'}
    if not job['import_products'] or job['state'] not in states:
        raise HTTPException(409, 'Selecione Produtos e conclua a validação do lote.')
    products, links = {}, {}
    for sale in iter_sales(job_id, status='ready' if ready_only else None):
        for pair in sale.get('catalog_products', []):
            product, link = pair['product'], pair['company_product']
            if (products.setdefault(product['_id'], product) != product
                    or links.setdefault(link['_id'], link) != link):
                raise HTTPException(409, 'O lote contém cadastros incompatíveis para o mesmo produto.')
    return products, links


def purchase_cost_preview(job_id, owner):
    job = owned(job_id, owner)
    if job['state'] not in {'ready', 'complete'}:
        raise HTTPException(422, 'Conclua a validação antes de conferir os custos de compra.')
    if job['state'] == 'ready':
        return latest_purchase_costs(iter_sales(job_id, status='ready'))
    # Choose across the whole batch first so a newer duplicate never makes
    # an older imported purchase appear to be the latest.
    latest = latest_purchase_costs(iter_sales(job_id))
    imported_keys = {sale['summary']['access_key'] for sale in iter_sales(job_id, status='imported')
                     if sale.get('purchase_cost_candidates')}
    return [candidate for candidate in latest if candidate['access_key'] in imported_keys]


def export_zip(job_id,owner):
    job=owned(job_id,owner);state=snapshot(job_id,owner)
    if state['counts'].get('invalid') or job['state'] not in {'ready', 'complete'}:
        raise HTTPException(422,'Conclua a validação antes de exportar.')
    path=folder(job_id)/'export-dfe.zip'
    if path.exists():return path
    temporary = folder(job_id) / (secrets.token_hex(16) + '.zip.tmp')
    spool = temporary.with_suffix('.sqlite3')
    db = sqlite3.connect(spool)
    try:
        db.execute('CREATE TABLE records (collection TEXT, ident TEXT, document BLOB, UNIQUE(collection, ident))')

        def add(collection, document):
            ident = json_util.dumps(document['_id'], json_options=json_util.CANONICAL_JSON_OPTIONS)
            data = BSON.encode(document)
            inserted = db.execute('INSERT OR IGNORE INTO records VALUES (?, ?, ?)', (collection, ident, data))
            if not inserted.rowcount:
                previous = db.execute('SELECT document FROM records WHERE collection=? AND ident=?',
                                      (collection, ident)).fetchone()[0]
                if not editor.same_document(BSON(previous).decode(), document):
                    raise HTTPException(409, 'O lote contém registros diferentes com a mesma identidade em ' + collection + '.')

        if job['import_people']:
            for person in reviewed_people(job_id, owner).values():
                add('Pessoas', person)
        if job['import_products']:
            products, links = reviewed_products(job_id, owner)
            for collection, records in (('ProdutosServicos', products), ('ProdutosServicosEmpresa', links)):
                for document in records.values():
                    add(collection, document)
        if job['import_movements'] or not (job['import_people'] or job['import_products']):
            for sale in iter_sales(job_id):
                for collection, document in sale_documents(sale):
                    add(collection, document)

        with zipfile.ZipFile(temporary, 'w', zipfile.ZIP_DEFLATED, compresslevel=1, allowZip64=True) as output:
            for (collection,) in db.execute('SELECT collection FROM records GROUP BY collection ORDER BY MIN(rowid)'):
                with output.open(collection + '.json', 'w', force_zip64=True) as entry:
                    entry.write(b'[')
                    rows = db.execute('SELECT document FROM records WHERE collection=? ORDER BY rowid', (collection,))
                    for index, (data,) in enumerate(rows):
                        if index:
                            entry.write(b',\n')
                        entry.write(json_util.dumps(BSON(data).decode(), json_options=json_util.CANONICAL_JSON_OPTIONS,
                                                     ensure_ascii=False).encode('utf-8'))
                    entry.write(b']')
            output.writestr('LEIA-ME.txt', NOTICE + '\nDestino: ' + job['database_name'] +
                            '\nOrdem: cadastros de pessoas e produtos (quando houver), XmlMovimentacoes, Movimentacoes, '
                            'Pagamentos/Recebimentos (quando houver). Importar por inserção; nunca replace/upsert. '
                            'Verifique chaves existentes.')
    except BaseException:
        temporary.unlink(missing_ok=True)
        raise
    finally:
        db.close()
        spool.unlink(missing_ok=True)
    if path.exists():temporary.unlink()
    else:temporary.replace(path)
    return path


def list_jobs(owner,offset=0):
    offset=max(0,int(offset))
    with connect() as db:
        rows=db.execute('SELECT id,database_name,state,created FROM jobs WHERE owner=? AND connection_key=? ORDER BY created DESC LIMIT 51 OFFSET ?',
                        (owner,identity(),offset)).fetchall()
    return {'jobs':[dict(row) for row in rows[:50]],'has_more':len(rows)>50,'offset':offset}


def discard(job_id,owner):
    job=owned(job_id,owner)
    with _lock:
        if job_id in _active or job['lease']>time.time():raise HTTPException(409,'Pause e aguarde o processamento antes de descartar.')
        with connect() as db:
            receiving=db.execute("SELECT id FROM files WHERE job_id=? AND status='receiving'",(job_id,)).fetchall()
        if any(row['id'] in _uploads for row in receiving):raise HTTPException(409,'Aguarde os envios em andamento antes de descartar.')
        path=folder(job_id).resolve();root=(store.data_dir()/'nfce-import').resolve()
        path.relative_to(root)
        if path==root:raise HTTPException(403,'Caminho local inválido.')
        for name in list(_anchors):
            if Path(name).resolve().parent==path:
                for connection in _anchors.pop(name):connection.close()
        shutil.rmtree(path)
        with connect() as db:
            db.execute('DELETE FROM files WHERE job_id=?',(job_id,))
            db.execute('DELETE FROM metrics WHERE job_id=?',(job_id,))
            db.execute('DELETE FROM jobs WHERE id=?',(job_id,))
    return {'discarded':True,'mongo_writes':0}


def stop_workers():
    _stopping.set()


def close_connections():
    with _lock:
        for pool in _anchors.values():
            for connection in pool:connection.close()
        _anchors.clear()

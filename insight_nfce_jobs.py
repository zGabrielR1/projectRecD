"""Disk-backed NFC-e jobs. Input count and total bytes are not capped.

Files, indexed identities and converted BSON stay on disk. Workers process one
document at a time; SQLite indexes handle deduplication and paged progress.
"""
from contextlib import contextmanager, nullcontext, ExitStack
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path
from datetime import datetime, timezone
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
from insight_nfce_mapping import load_profile, build_sale, validate_native_documents
from insight_nfce_discovery import movement_type, Discovery, read_xml, tree_signature, NS

_pool=ThreadPoolExecutor(max_workers=2,thread_name_prefix='nfce')
_active={}
_lock=threading.RLock()
_worker=secrets.token_hex(16)
_stopping=threading.Event()
_heartbeat_started=False
_initialized=set()
_uploads=set()
_anchors=OrderedDict()


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


def create(owner,mode='automatic',profile=None):
    import app
    if mode not in {'automatic','manual'}:raise HTTPException(422,'Modo inválido.')
    try:database=load_profile(profile)['database'] if mode=='manual' else app.MONGO_DATABASE
    except (ValueError,TypeError,AttributeError) as exc:raise HTTPException(422,str(exc)) from None
    editor.database_name(database)
    if database in editor.SYSTEM_DATABASES:raise HTTPException(403,'Banco de sistema não permitido.')
    job_id=secrets.token_urlsafe(24);now=time.time()
    with _lock,connect() as db:
        db.execute('INSERT INTO jobs(id,owner,connection_key,database_name,mode,profile,state,created,updated) VALUES(?,?,?,?,?,?,?,?,?)',
            (job_id,owner,identity(),database,mode,profile,'uploading',now,now))
    folder(job_id)
    return snapshot(job_id,owner)


def reserve(job_id,owner,upload_id,name):
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
            file_id=db.execute('INSERT INTO files(job_id,upload_id,name,path,status) VALUES(?,?,?,?,?)',
                (job_id,upload_id,name,leaf,'receiving')).lastrowid
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
    digest=hashlib.sha256(json.dumps([name,metadata.get('size'),metadata.get('modified')],
                                  ensure_ascii=False,separators=(',',':')).encode('utf-8')).hexdigest()
    with connect() as db:
        db.execute('INSERT OR IGNORE INTO files(job_id,upload_id,name,path,status) VALUES(?,?,?,?,?)',
                   (job_id,digest,name,secrets.token_hex(16)+'.xml','awaiting_upload'))
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


def import_keys(job_id, owner, keys):
    """Stage authorized XML already archived in the job's target database."""
    from pymongo.errors import PyMongoError

    job = owned(job_id, owner)
    if job['state'] != 'uploading':
        raise HTTPException(409, 'Os envios desta importação já foram encerrados.')
    if not isinstance(keys, list) or not 1 <= len(keys) <= 50:
        raise HTTPException(422, 'Envie de 1 a 50 chaves por consulta.')
    if any(not isinstance(key, str) or len(key) > 64 for key in keys):
        raise HTTPException(422, 'Informe as chaves como texto.')
    keys = dict.fromkeys(re.sub(r'[\s.-]+', '', key) for key in keys)
    results = []
    try:
        with editor.editor_client() as client:
            collection = editor.selected_collection(client, job['database_name'], 'XmlMovimentacoes')
            for key in keys:
                result = {'key': key, 'status': 'unavailable'}
                results.append(result)
                if not re.fullmatch(r'[0-9]{44}', key) or not key_valid(key) or key[20:22] not in {'55', '65'}:
                    result['message'] = 'Chave inválida. Confira os 44 dígitos, o modelo e o dígito verificador.'
                    continue
                try:
                    raw = archived_xml(collection, key)
                except ValueError as exc:
                    result['message'] = str(exc)
                    continue
                upload_id = 'key-' + key
                digest = hashlib.sha256(raw).hexdigest()
                row, target = reserve(job_id, owner, upload_id, key + '-procNFe.xml')
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


def archived_xml(collection, key):
    if collection.find_one({'ChaveAcesso': key, **ARCHIVED_CANCELLATION}, {'_id': 1}, max_time_ms=5000):
        raise ValueError('Há evento de cancelamento para esta chave na base.')
    raw = None
    query = {'ChaveAcesso': key, 'EventoCancelamento': {'$ne': True}}
    with collection.find(query, {'Xml': 1, '_id': 0}).max_time_ms(5000).batch_size(1) as candidates:
        for candidate in candidates:
            xml = candidate.get('Xml')
            if not isinstance(xml, str):
                continue
            content = xml.encode('utf-8')
            try:
                parsed = parse_xml(content, issuer=key[6:20])
            except ValueError:
                continue
            if parsed['access_key'] != key:
                continue
            if raw is not None and raw != content:
                raise ValueError('Há versões diferentes do XML autorizado para esta chave.')
            raw = content
    if raw is None:
        raise ValueError('XML autorizado de produção não encontrado na base. Envie o nfeProc se conseguir recuperá-lo.')
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


def index_file(job_id,row):
    path=folder(job_id)/row['path']
    # Every original XML is stored in one Mongo document. Avoid reading an
    # impossible document into memory; this is Mongo's bound, not an upload cap.
    if row['bytes'] > MONGO_DOCUMENT_BYTES+3:
        raise ValueError('O XML não cabe em um documento BSON de 16 MiB, limite nativo do MongoDB.')
    raw=raw_bytes(job_id,row)
    if hashlib.sha256(raw).hexdigest()!=row['sha']:
        raise ValueError('O arquivo local diverge do envio original. Envie novamente em outra importação.')
    root=read_xml(raw)
    if root.tag=='{'+NS['n']+'}NFe':
        info=root.find('n:infNFe',NS);node=root;kind='signed'
    else:
        info=root.find('n:NFe/n:infNFe',NS);node=root.find('n:NFe',NS);kind='authorized'
    if info is None or node is None:raise ValueError('XML não é uma NFC-e NFe/nfeProc.')
    environment=info.findtext('n:ide/n:tpAmb',namespaces=NS) or ''
    if kind=='authorized':
        parsed=parse_xml(raw,issuer=info.findtext('n:emit/n:CNPJ',namespaces=NS) or '',environment=environment)
        summary={key:parsed[key] for key in ('access_key','number','series','total','issued_at_original','xml_sha256')}
    else:summary={'access_key':info.get('Id','')[3:]}
    key=environment+':'+info.get('Id','')
    update_file(row['id'],kind=kind,key=key,signature=signature(node),full_signature=signature(root),
                status='indexed',summary=json.dumps(summary),message=None)


def resolve_pair(row):
    with connect() as db:
        first=db.execute("SELECT id,signature,full_signature FROM files WHERE job_id=? AND key=? AND kind='authorized' ORDER BY id LIMIT 1",
                         (row['job_id'],row['key'])).fetchone()
    if row['kind']=='signed':
        same=first is not None and first['signature']==row['signature']
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
    return {'name':row['name'],'raw':raw,'info':info,'parsed':parsed}


def write_sale(job_id,row,sale):
    validate_native_documents(sale['movement'],sale['xml'])
    with payload_connection(job_id) as db:
        db.execute('INSERT INTO payloads(id,sale) VALUES(?,?) ON CONFLICT(id) DO UPDATE SET sale=excluded.sale',
                   (row['id'],sqlite3.Binary(BSON.encode(sale))))
    update_file(row['id'],status='ready',sale_path='sqlite',message=None)


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
    while running(job_id):
        row=next_file(job_id,'uploaded')
        if not row:break
        try:index_file(job_id,row)
        except ValueError as exc:update_file(row['id'],status='invalid',message=str(exc))
    if not running(job_id):return
    profile=load_profile(job['profile']) if job['mode']=='manual' else None
    with ExitStack() as stack:
        client=None;discovery=None;processed=0
        while running(job_id):
            owned(job_id,owner)
            row=next_file(job_id,'indexed')
            if not row:break
            if not resolve_pair(row):continue
            try:
                if profile:sale=build_sale(raw_bytes(job_id,row),profile)
                else:
                    if client is None:
                        client=stack.enter_context(editor.editor_client())
                        discovery=Discovery(client[job['database_name']])
                    item=incoming(job_id,row)
                    company_id,_=discovery.company(item['parsed']['issuer_cnpj'], item['parsed']['model'])
                    query={'EmpresaReferencia':company_id,'_t':movement_type(item['parsed']['model']),
                           'Numero':int(item['parsed']['number']),'Serie':item['parsed']['series']}
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


def authorize_apply(job_id,owner,token,confirmation):
    job=owned(job_id,owner)
    if confirmation!='IMPORTAR '+job['database_name']:raise HTTPException(422,'Confirme o banco de destino.')
    access=editor.session(token,owner,job['database_name'])
    for collection in ('Movimentacoes','XmlMovimentacoes'):
        for action in ('insert','find'):
            if not editor.permitted(access['grants'],job['database_name'],collection,action):
                raise HTTPException(403,'Permissão ausente: '+action+' em '+collection)


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
        rows=db.execute('SELECT id,name,status,message,summary,result FROM files WHERE job_id=? ORDER BY id LIMIT ? OFFSET ?',
                        (job_id,page_size,offset)).fetchall()
    values=[]
    for row in rows:
        value={'id':row['id'],'file':row['name'],'status':row['status'],'message':row['message']}
        if row['summary']:value.update(json.loads(row['summary']))
        if row['result']:value['result']=json.loads(row['result'])
        values.append(value)
    in_progress=job['state'] in {'queued','validating','applying','pausing'}
    orphan=in_progress and job['lease']<time.time()
    return {'id':job_id,'database':job['database_name'],'mode':job['mode'],
            'state':'interrupted' if orphan and job['phase']=='apply' else 'paused' if orphan else job['state'],
            'phase':job['phase'],'error':job['error'],'total':total,'bytes':size,'counts':counts,
            'ready':counts.get('ready',0),'valid':not counts.get('invalid') and not counts.get('unknown') and
              not counts.get('applying') and counts.get('ready',0)>0 and (job['state'] in {'ready','paused','interrupted'} or orphan),
            'offset':offset,'page_size':page_size,'has_more':offset+len(rows)<total,'rows':values,'notice':NOTICE}


def document(job_id,owner,file_id):
    owned(job_id,owner)
    with connect() as db:row=db.execute('SELECT * FROM files WHERE id=? AND job_id=?',(file_id,job_id)).fetchone()
    if not row or not row['sale_path']:raise HTTPException(404,'Documento convertido não disponível.')
    return read_sale(job_id,dict(row))


def iter_sales(job_id):
    previous=0
    while True:
        with connect() as db:
            row=db.execute("SELECT id,sale,sale_path FROM files WHERE job_id=? AND sale_path IS NOT NULL AND id>? ORDER BY id LIMIT 1",(job_id,previous)).fetchone()
        if not row:break
        previous=row['id'];yield read_sale(job_id,dict(row))


def export_zip(job_id,owner):
    job=owned(job_id,owner);state=snapshot(job_id,owner)
    if state['counts'].get('invalid') or job['state'] not in {'ready','complete','paused'}:
        raise HTTPException(422,'Conclua a validação antes de exportar.')
    path=folder(job_id)/'export.zip'
    if path.exists():return path
    temporary=folder(job_id)/(secrets.token_hex(16)+'.zip.tmp')
    with zipfile.ZipFile(temporary,'w',zipfile.ZIP_DEFLATED,compresslevel=1,allowZip64=True) as output:
        for field,name in [('movement','Movimentacoes.json'),('xml','XmlMovimentacoes.json')]:
            with output.open(name,'w',force_zip64=True) as entry:
                entry.write(b'[');first=True
                for sale in iter_sales(job_id):
                    if not first:entry.write(b',\n')
                    entry.write(json_util.dumps(sale[field],json_options=json_util.CANONICAL_JSON_OPTIONS,ensure_ascii=False).encode('utf-8'))
                    first=False
                entry.write(b']')
        output.writestr('LEIA-ME.txt',NOTICE+'\nDestino: '+job['database_name']+'\nImportar por inserção; nunca replace/upsert. Verifique chaves existentes.')
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

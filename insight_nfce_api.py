"""Explicit, journaled XML -> Movimentacoes import, independent of report privileges."""
import base64
import binascii
import json
import secrets
import threading
import time

from fastapi import APIRouter, Depends, Header, HTTPException, Request
from fastapi.responses import Response
from starlette.concurrency import run_in_threadpool

import insight_editor as editor
from insight_editor_query import ejson
from insight_nfce import ARCHIVED_CANCELLATION, NOTICE
from insight_nfce_mapping import build_sale, load_profile, validate_native_documents

router = APIRouter(prefix='/api/nfce-import', dependencies=[Depends(editor.same_origin)])
_plans = {}
_lock = threading.RLock()


async def body_json(request):
    raw = bytearray()
    async for chunk in request.stream():
        raw.extend(chunk)
    try:
        value = json.loads(raw)
    except (ValueError, UnicodeDecodeError):
        raise HTTPException(422, 'JSON inválido.') from None
    if not isinstance(value, dict):
        raise HTTPException(422, 'Esperado objeto JSON.')
    return value


def make_plan(body, owner):
    if body.get('mode') == 'automatic':
        return make_discovery_plan(body, owner)
    try:
        profile = load_profile(body.get('profile', ''))
        editor.database_name(profile['database'])
        if profile['database'] in editor.SYSTEM_DATABASES:
            raise ValueError('Banco de sistema não permitido.')
        files = body.get('files')
        if not isinstance(files, list) or not files:
            raise ValueError('Selecione os XMLs.')
    except (ValueError, TypeError, AttributeError) as exc:
        raise HTTPException(422, str(exc)) from None
    rows, sales, seen = [], [], {}
    for entry in files:
        try:
            if not isinstance(entry, dict) or not isinstance(entry.get('base64'), str):
                raise ValueError('Arquivo deve conter nome e base64.')
            raw = base64.b64decode(entry['base64'], validate=True)
            sale = build_sale(raw, profile)
            key, sha = sale['summary']['access_key'], sale['summary']['xml_sha256']
            if key in seen:
                if seen[key] != sha:
                    raise ValueError('Mesma chave com XMLs diferentes no lote.')
                rows.append({'file': str(entry.get('name', 'XML'))[:180], 'status': 'duplicate', 'access_key': key})
                continue
            seen[key] = sha
            validate_native_documents(sale['movement'],sale['xml'])
            sales.append(sale)
            rows.append({'file': str(entry.get('name', 'XML'))[:180], 'status': 'ready', **sale['summary']})
        except (ValueError, TypeError, KeyError, binascii.Error) as exc:
            rows.append({'file': str(entry.get('name', 'XML'))[:180] if isinstance(entry, dict) else 'XML',
                         'status': 'invalid', 'message': str(exc)})
    return register_plan(profile['database'], sales, rows, owner)


def register_plan(database, sales, rows, owner, *, discovery=None):
    size = len(ejson(sales).encode())
    with _lock:
        for key in list(_plans):
            if _plans[key]['expires'] < time.time():
                del _plans[key]
        token = secrets.token_urlsafe(24)
        _plans[token] = {'owner': owner, 'fingerprint': editor.fingerprint(), 'database': database,
                         'sales': sales, 'rows': rows, 'expires': time.time() + 300, 'size': size,
                         'valid': bool(sales) and not any(r['status'] == 'invalid' for r in rows)}
        if discovery is not None: _plans[token]['configured_database'] = database
    return {'id': token, 'database': database, 'collection': 'Movimentacoes',
            'rows': rows, 'ready': len(sales), 'valid': _plans[token]['valid'],
            'notice': NOTICE + (' Vendedor não informado. Valores aproximados dos tributos permanecem no XML e podem ser recalculados pelo cadastro na tela nativa.' if discovery is not None else ''),
            'discovery': discovery,
            'documents': [{'movement': ejson(s['movement']), 'xml': ejson(s['xml'])} for s in sales]}


def make_discovery_plan(body, owner):
    import app
    from pymongo.errors import PyMongoError
    from insight_nfce_discovery import movement_type, Discovery, prepare_files
    files = body.get('files')
    if not isinstance(files, list) or not files:
        raise HTTPException(422, 'Selecione os XMLs.')
    decoded = []
    try:
        for entry in files:
            if not isinstance(entry, dict) or not isinstance(entry.get('base64'), str):
                raise ValueError('Arquivo deve conter nome e base64.')
            raw = base64.b64decode(entry['base64'], validate=True)
            decoded.append((str(entry.get('name','XML'))[:180], raw))
        prepared, rows = prepare_files(decoded)
    except (ValueError, TypeError, binascii.Error) as exc:
        raise HTTPException(422, str(exc)) from None
    database = editor.database_name(app.MONGO_DATABASE)
    if database in editor.SYSTEM_DATABASES:
        raise HTTPException(422,'Selecione o banco do cliente na configuração da conexão.')
    sales = []
    notes = []
    # Import discovery is a read-only administrative workflow, not a reporting query.
    try:
        if prepared:
            with editor.editor_client() as client:
                db = client[database]
                names = db.list_collection_names(maxTimeMS=5000)
                if any(n not in names for n in ('Movimentacoes','XmlMovimentacoes','Pessoas','ProdutosServicos','ProdutosServicosEmpresa')):
                    raise HTTPException(422,'A base selecionada não contém as coleções nativas necessárias.')
                discovery = Discovery(db)
                for incoming in prepared:
                    try:
                        company_id, _ = discovery.company(incoming['parsed']['issuer_cnpj'], incoming['parsed']['model'])
                        native_filter = {'EmpresaReferencia':company_id, '_t':movement_type(incoming['parsed']['model']),
                                         'Numero':int(incoming['parsed']['number']), 'Serie':incoming['parsed']['series']}
                        existing = db.Movimentacoes.find_one(native_filter, {'ChaveAcesso':1}, max_time_ms=5000)
                        if existing:
                            if existing.get('ChaveAcesso') != incoming['parsed']['access_key']:
                                raise ValueError('Número/série já existem com outra chave; confira a venda na base.')
                            rows.append({'file':incoming['name'],'status':'duplicate',
                                         'access_key':incoming['parsed']['access_key'],'message':'Venda já existe; não será alterada.'})
                            continue
                        sale = discovery.build(incoming)
                        validate_native_documents(sale['movement'],sale['xml'])
                        sales.append(sale)
                        rows.append({'file':incoming['name'],'status':'ready',**sale['summary']})
                    except ValueError as exc:
                        rows.append({'file':incoming['name'],'status':'invalid','message':str(exc)})
                notes = discovery.notes
    except PyMongoError:
        raise HTTPException(503,'Não foi possível concluir a descoberta em leitura. Confira conexão, permissões e índices; nenhuma venda foi gravada.') from None
    return register_plan(database, sales, rows, owner, discovery={'mode':'automatic','companies':notes,
        'authorized_xmls':len(prepared),'input_files':len(files),'database_writes':0,
        'sample_limit':100,'message':'Vínculos observados na base; referências ambíguas permanecem bloqueadas.'})


def plan_for(token, owner):
    with _lock:
        plan = _plans.get(token)
        if not plan or plan['expires'] < time.time() or plan['owner'] != owner:
            raise HTTPException(409, 'Prévia ausente ou expirada. Valide novamente.')
        if plan['fingerprint'] != editor.fingerprint():
            raise HTTPException(409, 'Conexão alterada. Valide novamente.')
        if 'configured_database' in plan:
            import app
            if app.MONGO_DATABASE != plan['configured_database']:
                raise HTTPException(409,'O banco selecionado mudou. Execute a descoberta novamente.')
        return plan


@router.post('/preview')
async def preview(request: Request):
    body = await body_json(request)
    return await run_in_threadpool(make_plan, body, editor.actor(request))


@router.post('/export/{token}')
def export(token: str, request: Request):
    plan = plan_for(token, editor.actor(request))
    if not plan['valid']:
        raise HTTPException(422, 'Corrija todos os XMLs antes de exportar.')
    import io
    import zipfile
    data = io.BytesIO()
    with zipfile.ZipFile(data, 'w', zipfile.ZIP_DEFLATED) as output:
        for field, collection in [('movement', 'Movimentacoes'), ('xml', 'XmlMovimentacoes')]:
            output.writestr(collection + '.json', ejson([s[field] for s in plan['sales']]))
        output.writestr('LEIA-ME.txt', NOTICE + '\nDestino: ' + plan['database'] +
                        '\nImportar XmlMovimentacoes primeiro. Usar inserção; nunca replace/upsert. '
                        'O ZIP offline não verifica duplicatas existentes com outros _ids. '
                        'Prefira Aplicar no Insight para conferir a chave antes de gravar.')
    return Response(data.getvalue(), media_type='application/zip',
                    headers={'Content-Disposition': 'attachment; filename="nfce-import.zip"', 'Cache-Control': 'no-store'})


@router.post('/apply/{token}')
async def apply(token: str, request: Request, x_insight_editor_session: str = Header(default='')):
    body = await body_json(request)
    return await run_in_threadpool(apply_plan, token, body.get('confirmation'),
                                   x_insight_editor_session, editor.actor(request))


def apply_plan(token, confirmation, session_token, owner):
    plan = plan_for(token, owner)
    database = plan['database']
    access = editor.session(session_token, owner, database)
    if confirmation != 'IMPORTAR ' + database or not plan['valid']:
        raise HTTPException(422, 'Corrija o lote e digite IMPORTAR seguido do nome do banco.')
    for collection in ('Movimentacoes', 'XmlMovimentacoes'):
        for action in ('insert', 'find'):
            if not editor.permitted(access['grants'], database, collection, action):
                raise HTTPException(403, 'Permissão ausente: ' + action + ' em ' + collection)
    with _lock:
        if _plans.pop(token, None) is not plan:
            raise HTTPException(409, 'Esta prévia já foi enviada. Confira os resultados antes de repetir.')
    results = []
    for sale in plan['sales']:
        result=apply_sale(sale,database,session_token,owner)
        results.append(result)
        if result['status']=='stopped':break
    return {'database': database, 'results': results, 'unprocessed': len(plan['sales']) - len(results), 'notice': NOTICE}


def apply_sale(sale,database,session_token,owner,*,client=None):
    """One resumable write unit; optional pooled client belongs to the import worker."""
    from contextlib import nullcontext
    key=sale['summary']['access_key'];changes=[]
    try:
        validate_native_documents(sale['movement'],sale['xml'])
        with (nullcontext(client) if client is not None else editor.editor_client()) as current:
            moves=editor.selected_collection(current,database,'Movimentacoes',write=True)
            movement=sale['movement']
            native_filter={'EmpresaReferencia':movement['EmpresaReferencia'],'Numero':movement['Numero'],
                           'Serie':movement['Serie'],'_t':movement['_t'][-1] if isinstance(movement['_t'],list) else movement['_t']}
            existing=moves.find_one(native_filter,{'_id':1,'ChaveAcesso':1},max_time_ms=5000)
            if existing is not None:
                if existing.get('ChaveAcesso') not in (None,key):
                    raise HTTPException(409,'Número/série já existem com outra chave.')
                return {'access_key':key,'status':'duplicate','message':'Venda já existe; não alterada.'}
            xmls=editor.selected_collection(current,database,'XmlMovimentacoes',write=True)
            if xmls.find_one({'ChaveAcesso':key,**ARCHIVED_CANCELLATION},{'_id':1},max_time_ms=5000):
                raise HTTPException(409,'Há evento de cancelamento para esta chave na base.')
            existing_xml=xmls.find_one({'ChaveAcesso':key,'EventoCancelamento':{'$ne':True}},max_time_ms=5000)
            if existing_xml and (not isinstance(existing_xml.get('Xml'), str)
                                 or existing_xml['Xml'].removeprefix('\ufeff') != sale['xml']['Xml']):
                raise HTTPException(409,'XML existente difere do selecionado.')
            for guard in sale.get('guards',[]):
                from insight_nfce_discovery import guard_digest
                projection={f:1 for f in guard['fields']} if 'fields' in guard else {'Versao':1}
                source=current[database][guard['collection']].find_one({'_id':guard['id']},projection,max_time_ms=5000)
                if source is None or ('digest' in guard and guard_digest(source,guard['fields'])!=guard['digest']) or source.get('Versao')!=guard['version']:
                    raise HTTPException(409,'Uma referência usada na descoberta mudou. Valide novamente.')
            for field,collection in [('xml','XmlMovimentacoes'),('movement','Movimentacoes')]:
                if field=='xml' and existing_xml:continue
                change=editor.prepare_import_insert(sale[field],database,collection,session_token,owner,client=current)
                changes.append(change['id'])
                editor.apply_change(change['id'],session_token,owner,collection,document_limit=None,client=current)
        return {'access_key':key,'status':'imported','changes':changes}
    except Exception:
        return {'access_key':key,'status':'stopped','changes':changes,
                'message':'Escrita interrompida. Confira o diário e as duas coleções; o resultado pode ser parcial ou incerto.'}

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
from insight_nfce_mapping import (build_sale, load_profile, sale_collections, sale_documents,
                                  validate_native_documents)

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
                        native_filter = native_document_filter({
                            'EmpresaReferencia': company_id, '_t': movement_type(incoming['parsed']['model']),
                            'Numero': int(incoming['parsed']['number']), 'Serie': incoming['parsed']['series'],
                            'ChaveAcesso': incoming['parsed']['access_key'],
                        })
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
    groups = {}
    for sale in plan['sales']:
        for collection, document in sale_documents(sale):
            groups.setdefault(collection, {})[document['_id']] = document
    with zipfile.ZipFile(data, 'w', zipfile.ZIP_DEFLATED) as output:
        for collection, documents in groups.items():
            output.writestr(collection + '.json', ejson(list(documents.values())))
        output.writestr('LEIA-ME.txt', NOTICE + '\nDestino: ' + plan['database'] +
                        '\nOrdem: cadastros de produto (quando houver), XmlMovimentacoes, Movimentacoes, '
                        'Pagamentos/Recebimentos (quando houver). Usar inserção; nunca replace/upsert. '
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
    for collection in sale_collections(plan['sales']):
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


def prepare_person_insert(person, database, session_token, owner, *, client):
    access = editor.session(session_token, owner, database)
    for action in ('find', 'insert'):
        if not editor.permitted(access['grants'], database, 'Pessoas', action):
            raise HTTPException(403, 'Permissão ausente: ' + action + ' em Pessoas')
    field = 'Cpf' if person.get('Cpf') else 'Cnpj'
    document = person[field]
    if field == 'Cpf':
        formatted = f'{document[:3]}.{document[3:6]}.{document[6:9]}-{document[9:]}'
    else:
        formatted = f'{document[:2]}.{document[2:5]}.{document[5:8]}/{document[8:12]}-{document[12:]}'
    people = editor.selected_collection(client, database, 'Pessoas', write=True)
    if people.find_one({field: {'$in': [document, formatted]}}, {'_id': 1}, max_time_ms=5000):
        return None
    return editor.prepare_import_insert(person, database, 'Pessoas', session_token, owner, client=client)


def check_purchase_cost_source(candidate, database, access, *, client):
    from bson import ObjectId
    from bson.decimal128 import Decimal128
    from insight_nfce_mapping import native_issue_time

    for collection in ('Movimentacoes', 'ProdutosServicosEmpresa'):
        if not editor.permitted(access['grants'], database, collection, 'find'):
            raise HTTPException(403, 'Permissão ausente: find em ' + collection)
    purchases = editor.selected_collection(client, database, 'Movimentacoes')
    key = candidate['access_key']
    issued = native_issue_time(candidate['issued_at'])
    query = {'EmpresaReferencia': candidate['company_id'], '_t': 'NotaFiscalCompra',
             'Situacao.Codigo': 2, 'ItensBase.ProdutoServicoEmpresaReferencia': candidate['company_product_id']}
    cost = candidate['unit_cost']
    source_line = {'ProdutoServicoEmpresaReferencia': candidate['company_product_id'],
                   'PrecoUnitario': {'$in': [float(cost), Decimal128(cost)]}}
    if not purchases.find_one(dict(query, ChaveAcesso=key, DataHoraEmissao=issued,
                                   ItensBase={'$elemMatch': source_line}), {'_id': 1}, max_time_ms=5000):
        raise HTTPException(409, 'A compra ou o custo unitário revisado mudou. Prepare uma nova prévia.')
    if purchases.find_one(dict(query, DataHoraEmissao={'$gte': issued}, ChaveAcesso={'$ne': key}),
                          {'_id': 1}, max_time_ms=5000):
        raise HTTPException(409, 'Há outra compra da mesma data e hora ou mais recente na base. Revise o custo.')
    products = editor.selected_collection(client, database, 'ProdutosServicosEmpresa')
    link = products.find_one({'_id': candidate['company_product_id'],
                              'EmpresaReferencia': candidate['company_id'], 'Ativo': True},
                             {'PrecoReferencia': 1}, max_time_ms=5000)
    price_id = link.get('PrecoReferencia') if link else None
    if not isinstance(price_id, ObjectId):
        raise HTTPException(409, 'Vínculo ativo de preço não encontrado para o produto da empresa.')
    return price_id


def prepare_purchase_cost(candidate, database, session_token, owner, *, client):
    """Journal a cost change for a validated purchase line."""
    from insight_nfce_mapping import purchase_cost_change

    if candidate['conflict']:
        raise HTTPException(409, 'Resolva o conflito entre custos da última compra.')
    access = editor.session(session_token, owner, database)
    price_id = check_purchase_cost_source(candidate, database, access, client=client)
    for action in ('find', 'update'):
        if not editor.permitted(access['grants'], database, 'Precos', action):
            raise HTTPException(403, 'Permissão ausente: ' + action + ' em Precos')
    prices = editor.selected_collection(client, database, 'Precos', write=True)
    before = prices.find_one({'_id': price_id}, max_time_ms=5000)
    if before is None:
        raise HTTPException(409, 'Cadastro de preço não encontrado.')
    after = purchase_cost_change(before, candidate['unit_cost'])
    if after is None:
        return None
    validate_native_documents(after)
    return editor.journal_preview(database, 'Precos', 'update', price_id, before, after,
                                  session_token, owner, access, purchase_cost=candidate)


def native_document_filter(movement):
    discriminator = movement['_t']
    native_type = discriminator[-1] if isinstance(discriminator, list) else discriminator
    company = movement['EmpresaReferencia']
    number_filter = {'Numero': movement['Numero'], 'Serie': movement['Serie']}
    if native_type == 'NotaFiscalCompra':
        number_filter['Pessoa.PessoaReferencia'] = movement['Pessoa']['PessoaReferencia']
    elif native_type == 'CupomFiscalEletronico':
        serial = movement.get('NumeroFabricacaoSat') or movement['NumeroFabricacao']
        number_filter['$or'] = [{'NumeroFabricacaoSat': serial}, {'NumeroFabricacao': serial}]
    return {'EmpresaReferencia': company, '_t': native_type,
            '$or': [{'ChaveAcesso': movement['ChaveAcesso']}, number_filter]}


def insert_document(document, database, collection, session_token, owner, *, client, changes):
    """Insert one prepared document, tolerating an identical journaled prior write."""
    existing = editor.selected_collection(client, database, collection).find_one(
        {'_id': document['_id']}, max_time_ms=5000)
    if existing is not None:
        if not editor.same_document(existing, document):
            raise HTTPException(409, 'Já existe um documento diferente com o _id preparado em ' + collection + '.')
        return False
    change = editor.prepare_import_insert(document, database, collection, session_token, owner, client=client)
    changes.append(change['id'])
    editor.apply_change(change['id'], session_token, owner, collection, document_limit=None, client=client)
    return True


def apply_sale(sale,database,session_token,owner,*,client=None):
    """One resumable write unit; optional pooled client belongs to the import worker."""
    from contextlib import nullcontext
    from insight_nfce_mapping import REGISTRATION_COLLECTIONS
    key=sale['summary']['access_key'];changes=[]
    try:
        validate_native_documents(sale['movement'],sale['xml'])
        with (nullcontext(client) if client is not None else editor.editor_client()) as current:
            moves=editor.selected_collection(current,database,'Movimentacoes',write=True)
            movement=sale['movement']
            native_filter = native_document_filter(movement)
            existing=moves.find_one(native_filter,{'_id':1,'ChaveAcesso':1},max_time_ms=5000)
            finance = sale.get('finance')
            resume_titles = False
            if existing is not None:
                if existing.get('ChaveAcesso') not in (None,key):
                    raise HTTPException(409,'Número/série já existem com outra chave.')
                if not finance or existing.get('_id') != movement['_id'] or existing.get('ChaveAcesso') != key:
                    return {'access_key':key,'status':'duplicate','message':'Movimentação já existe; não alterada.'}
                previous = moves.find_one({'_id': movement['_id']}, max_time_ms=5000)
                if not editor.same_document(previous, movement):
                    raise HTTPException(409, 'A movimentação existente difere da prévia financeira.')
                resume_titles = True
            xmls=editor.selected_collection(current,database,'XmlMovimentacoes',write=True)
            if xmls.find_one({'ChaveAcesso':key,**ARCHIVED_CANCELLATION},{'_id':1},max_time_ms=5000):
                raise HTTPException(409,'Há evento de cancelamento para esta chave na base.')
            existing_xml=xmls.find_one({'ChaveAcesso':key,'EventoCancelamento':{'$ne':True}},max_time_ms=5000)
            if existing_xml and (not isinstance(existing_xml.get('Xml'), str)
                                 or existing_xml['Xml'].removeprefix('\ufeff') != sale['xml']['Xml']):
                raise HTTPException(409,'XML existente difere do selecionado.')
            if resume_titles and not existing_xml:
                raise HTTPException(409, 'O XML da movimentação existente precisa estar na base antes de retomar os títulos.')
            for guard in sale.get('guards',[]):
                from insight_nfce_discovery import guard_digest
                projection={f:1 for f in guard['fields']} if 'fields' in guard else {'Versao':1}
                source=current[database][guard['collection']].find_one({'_id':guard['id']},projection,max_time_ms=5000)
                if source is None or ('digest' in guard and guard_digest(source,guard['fields'])!=guard['digest']) or source.get('Versao')!=guard['version']:
                    raise HTTPException(409,'Uma referência usada na descoberta mudou. Valide novamente.')
            registrations = sale.get('product_registrations', [])
            if registrations:
                catalog = editor.selected_collection(current, database, 'ProdutosServicos')
                identifiers = {}
                for registration in registrations:
                    product = registration['documents']['ProdutosServicos']
                    for field in ('CodigoInterno', 'CodigoBarras'):
                        value = product.get(field)
                        if not isinstance(value, (str, int)) or not value or value == 'SEM GTIN':
                            continue
                        identity = (field, value)
                        if identifiers.setdefault(identity, product['_id']) != product['_id']:
                            raise HTTPException(409, 'O XML gera produtos com código ou código de barras duplicado.')
                        conflict = catalog.find_one({field: value, '_id': {'$ne': product['_id']}},
                                                    {'_id': 1}, max_time_ms=5000)
                        if conflict is not None:
                            raise HTTPException(409, 'Outro produto usa o código ou código de barras preparado. Revise o vínculo.')
            for registration in registrations:
                documents = registration['documents']
                for collection in REGISTRATION_COLLECTIONS:
                    if collection in documents:
                        insert_document(documents[collection],database,collection,session_token,
                                        owner,client=current,changes=changes)
            for field,collection in [('xml','XmlMovimentacoes'),('movement','Movimentacoes')]:
                if resume_titles or (field=='xml' and existing_xml):continue
                change=editor.prepare_import_insert(sale[field],database,collection,session_token,owner,client=current)
                changes.append(change['id'])
                editor.apply_change(change['id'],session_token,owner,collection,document_limit=None,client=current)
            if finance:
                for document in finance['documents']:
                    insert_document(document,database,finance['collection'],session_token,
                                    owner,client=current,changes=changes)
            if resume_titles and not changes:
                return {'access_key': key, 'status': 'duplicate', 'message': 'Movimentação e títulos já existem.'}
        return {'access_key':key,'status':'imported','changes':changes}
    except Exception as exc:
        detail = exc.detail if isinstance(exc, HTTPException) else None
        return {'access_key':key,'status':'stopped','changes':changes,
                'message':('Escrita interrompida: ' + str(detail) if detail else
                           'Escrita interrompida. Confira o diário e as duas coleções; o resultado pode ser parcial ou incerto.')}

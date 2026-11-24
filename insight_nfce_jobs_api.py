"""Streaming HTTP intake for persistent NFC-e jobs."""
import hashlib

import anyio
from fastapi import APIRouter, Depends, Header, Request, HTTPException
from fastapi.responses import FileResponse
from starlette.concurrency import run_in_threadpool

import insight_editor as editor
from insight_editor_query import ejson
import insight_nfce_jobs as jobs

router=APIRouter(prefix='/api/nfce-import/jobs',dependencies=[Depends(editor.same_origin)])


@router.get('')
def history(request:Request,offset:int=0):return jobs.list_jobs(editor.actor(request),offset)


async def configuration(request):
    try:body=await request.json()
    except ValueError:raise HTTPException(422,'JSON inválido.') from None
    if not isinstance(body,dict):raise HTTPException(422,'Configuração inválida.')
    return body


@router.post('')
async def create(request:Request):
    body=await configuration(request)
    return await run_in_threadpool(jobs.create,editor.actor(request),body.get('mode','automatic'),body.get('profile'),
                                   body.get('use_barcode', False), body.get('import_movements', True),
                                   body.get('import_people', False))


@router.get('/{job_id}')
def status(job_id:str,request:Request,offset:int=0):
    return jobs.snapshot(job_id,editor.actor(request),offset)


@router.get('/{job_id}/files/{upload_id}')
def stored(job_id:str,upload_id:str,request:Request):
    return jobs.stored_upload(job_id,editor.actor(request),upload_id)


@router.post('/{job_id}/slots')
async def slot(job_id:str,request:Request):
    body=await configuration(request)
    return await run_in_threadpool(jobs.upload_slot,job_id,editor.actor(request),body)


@router.post('/{job_id}/keys')
async def keys(job_id: str, request: Request):
    body = await configuration(request)
    return await run_in_threadpool(jobs.import_keys, job_id, editor.actor(request), body.get('keys'))


@router.post('/{job_id}/files/{upload_id}')
async def upload(job_id:str,upload_id:str,request:Request,name:str='XML'):
    row,target=await run_in_threadpool(jobs.reserve,job_id,editor.actor(request),upload_id,name)
    digest=hashlib.sha256();size=0;blob=None
    try:
        if target:
            length=request.headers.get('content-length')
            if length is not None and length.isdigit() and int(length)<=jobs.MONGO_DOCUMENT_BYTES+3:
                blob=await run_in_threadpool(jobs.BlobUpload,row['id'],int(length))
                async for chunk in request.stream():
                    await run_in_threadpool(blob.write,chunk);digest.update(chunk);size+=len(chunk)
                await run_in_threadpool(blob.finish);blob=None
            else:
              async with await anyio.open_file(target,'xb') as output:
                async for chunk in request.stream():
                    await output.write(chunk);digest.update(chunk);size+=len(chunk)
            await run_in_threadpool(jobs.uploaded,row['id'],size,digest.hexdigest())
        else:
            async for chunk in request.stream():digest.update(chunk);size+=len(chunk)
            if row['sha']!=digest.hexdigest():
                raise HTTPException(409,'O conteúdo deste envio mudou. Crie outra importação.')
    except BaseException:
        if target:
            # Shield the durable failure marker even when the client disconnects.
            with anyio.CancelScope(shield=True):
                if blob:await run_in_threadpool(blob.abort)
                await run_in_threadpool(jobs.upload_error,row['id'])
        raise
    return {'id':row['id'],'bytes':size,'resumed':target is None}


@router.post('/{job_id}/validate')
def validate(job_id:str,request:Request):return jobs.launch(job_id,editor.actor(request),'validate')


@router.post('/{job_id}/pause')
def pause(job_id:str,request:Request):return jobs.pause(job_id,editor.actor(request))


@router.delete('/{job_id}')
def discard(job_id:str,request:Request):return jobs.discard(job_id,editor.actor(request))


@router.post('/{job_id}/apply')
async def apply(job_id:str,request:Request,x_insight_editor_session:str=Header(default='')):
    body=await configuration(request);owner=editor.actor(request)
    confirmation=body.get('confirmation') if isinstance(body,dict) else None
    await run_in_threadpool(jobs.authorize_apply,job_id,owner,x_insight_editor_session,confirmation)
    return await run_in_threadpool(jobs.launch,job_id,owner,'apply',session_token=x_insight_editor_session)


@router.get('/{job_id}/documents/{file_id}')
def document(job_id:str,file_id:int,request:Request):
    sale=jobs.document(job_id,editor.actor(request),file_id)
    if 'cfe_review' in sale:
        return sale
    if 'dbf_review' in sale:
        return sale
    result = {'movement': ejson(sale['movement']), 'xml': ejson(sale['xml'])}
    if sale.get('parties'):
        result['parties'] = sale['parties']
    if sale.get('people'):
        result['people'] = ejson(sale['people'])
    if sale.get('products'):
        result['products'] = [
            dict(product, company_product_id=str(product['company_product_id']))
            for product in sale['products']
        ]
    if 'purchase_cost_candidates' in sale:
        result['purchase_cost_candidates'] = ejson(sale['purchase_cost_candidates'])
    if sale.get('product_registrations'):
        result['product_registrations'] = [
            {'code': registration['code'], 'documents': ejson(registration['documents'])}
            for registration in sale['product_registrations']
        ]
    if sale.get('finance'):
        result['finance'] = {'collection': sale['finance']['collection'],
                             'type': sale['finance']['type'],
                             'documents': ejson(sale['finance']['documents'])}
    if sale.get('installments'):
        result['installments'] = sale['installments']
        native_type = sale['movement']['_t']
        if isinstance(native_type, list):
            native_type = native_type[-1]
        result['installment_kind'] = ('payable' if native_type in
                                      {'NotaFiscalCompra', 'NotaFiscalEletronicaEntrada'} else 'receivable')
    return result


@router.get('/{job_id}/purchase-costs')
def purchase_costs(job_id: str, request: Request):
    return {'costs': ejson(jobs.purchase_cost_preview(job_id, editor.actor(request)))}


@router.post('/{job_id}/documents/{file_id}/person/preview')
def prepare_person(job_id: str, file_id: int, request: Request,
                   x_insight_editor_session: str = Header(default='')):
    from insight_nfce_api import prepare_person_insert

    owner = editor.actor(request)
    job = jobs.owned(job_id, owner)
    if not job['import_people']:
        raise HTTPException(409, 'A opção Pessoas está desmarcada neste lote.')
    if job['state'] not in {'ready', 'complete'}:
        raise HTTPException(409, 'Conclua a validação antes de cadastrar pessoas.')
    people = jobs.document(job_id, owner, file_id).get('people', [])
    if len(people) != 1:
        raise HTTPException(404, 'Cadastro de pessoa indisponível neste documento.')
    with editor.editor_client() as client:
        preview = prepare_person_insert(people[0], job['database_name'],
                                        x_insight_editor_session, owner, client=client)
    return {'preview': preview}


@router.get('/{job_id}/people')
def people_preview(job_id: str, request: Request):
    people = jobs.reviewed_people(job_id, editor.actor(request))
    return {'people': [{'id': str(ident), 'name': person['Nome'],
                        'document': person.get('Cpf') or person.get('Cnpj')}
                       for ident, person in people.items()]}


@router.post('/{job_id}/people/preview')
async def prepare_people(job_id: str, request: Request,
                         x_insight_editor_session: str = Header(default='')):
    from insight_nfce_api import prepare_person_insert

    owner = editor.actor(request)
    body = await configuration(request)

    def prepare():
        job = jobs.owned(job_id, owner)
        if body.get('confirmation') != 'IMPORTAR ' + job['database_name']:
            raise HTTPException(422, 'Confirme o banco de destino.')
        people = jobs.reviewed_people(job_id, owner)
        if body.get('ids') != sorted(str(ident) for ident in people):
            raise HTTPException(409, 'A lista de pessoas mudou. Revise o lote novamente.')
        with editor.editor_client() as client:
            previews = [prepare_person_insert(person, job['database_name'],
                                              x_insight_editor_session, owner, client=client)
                        for person in people.values()]
        return {'previews': [preview['id'] for preview in previews if preview],
                'existing': sum(preview is None for preview in previews)}

    return await run_in_threadpool(prepare)


@router.post('/{job_id}/purchase-costs/{product_id}/preview')
def prepare_cost(job_id: str, product_id: str, request: Request,
                 x_insight_editor_session: str = Header(default='')):
    from bson import ObjectId
    from insight_nfce_api import prepare_purchase_cost

    owner = editor.actor(request)
    job = jobs.owned(job_id, owner)
    if job['state'] != 'complete':
        raise HTTPException(409, 'Conclua a importação antes de atualizar preços de custo.')
    if not ObjectId.is_valid(product_id):
        raise HTTPException(422, 'Identificador de produto inválido.')
    candidate = next((cost for cost in jobs.purchase_cost_preview(job_id, owner)
                      if cost['company_product_id'] == ObjectId(product_id)), None)
    if candidate is None:
        raise HTTPException(404, 'Produto sem custo de compra neste lote.')
    try:
        with editor.editor_client() as client:
            preview = prepare_purchase_cost(candidate, job['database_name'],
                                            x_insight_editor_session, owner, client=client)
    except ValueError as exc:
        raise HTTPException(422, str(exc)) from None
    return {'preview': preview}


@router.api_route('/{job_id}/export',methods=['GET','POST'])
def export(job_id:str,request:Request):
    path=jobs.export_zip(job_id,editor.actor(request))
    return FileResponse(path,media_type='application/zip',filename='nfce-import.zip',headers={'Cache-Control':'no-store'})


@router.on_event('shutdown')
def shutdown():jobs.stop_workers()

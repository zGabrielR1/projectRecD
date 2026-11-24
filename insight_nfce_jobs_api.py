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
    return await run_in_threadpool(jobs.create,editor.actor(request),body.get('mode','automatic'),body.get('profile'))


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
    return {'movement':ejson(sale['movement']),'xml':ejson(sale['xml'])}


@router.api_route('/{job_id}/export',methods=['GET','POST'])
def export(job_id:str,request:Request):
    path=jobs.export_zip(job_id,editor.actor(request))
    return FileResponse(path,media_type='application/zip',filename='nfce-import.zip',headers={'Cache-Control':'no-store'})


@router.on_event('shutdown')
def shutdown():jobs.stop_workers()

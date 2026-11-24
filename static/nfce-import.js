'use strict';
(() => {
  const $=id=>document.getElementById(id),root='/api/nfce-import/jobs',storage='digisat-nfce-job';
  let job=null,auth='',uploading=false,offset=0,epoch=0,historyOffset=0;
  const active=()=>job&&['queued','validating','applying','pausing'].includes(job.state);
  const num=n=>new Intl.NumberFormat('pt-BR').format(n||0);
  function message(text){$('error').textContent=text||'';$('error').hidden=!text;}
  function remember(id){try{if(id)localStorage.setItem(storage,id);else localStorage.removeItem(storage);}catch(_){}}
  function login(){return new Promise((resolve,reject)=>{
    const dialog=$('authDialog'),form=$('authForm');
    function clean(){form.removeEventListener('submit',submit);dialog.removeEventListener('cancel',cancel);$('authCancel').onclick=null;$('authPassword').value='';dialog.close();}
    function submit(e){e.preventDefault();auth='Basic '+btoa(String.fromCharCode(...new TextEncoder().encode($('authUser').value+':'+$('authPassword').value)));clean();resolve();}
    function cancel(e){e?.preventDefault();clean();reject(new Error('Autenticação cancelada.'));}
    form.addEventListener('submit',submit);dialog.addEventListener('cancel',cancel);$('authCancel').onclick=cancel;dialog.showModal();
  });}
  async function request(path,options={},retry=true){
    const {method='GET',body,raw=false,headers={}}=options;
    const response=await fetch(path,{method,headers:{...(auth?{Authorization:auth}:{}),...(!raw&&body!==undefined?{'Content-Type':'application/json'}:{}),...headers},body:body===undefined?undefined:raw?body:JSON.stringify(body)});
    if(response.status===401&&retry){await login();return request(path,options,false);}
    if(!response.ok){const result=await response.json().catch(()=>({}));throw new Error(typeof result.detail==='string'?result.detail:'Não foi possível concluir a operação.');}
    return response;
  }
  async function api(path,options){return (await request(path,options)).json();}
  function controls(){
    const locked=uploading||active()||Boolean(job&&job.state!=='uploading');
    for(const id of ['mappingMode','profile','xmls','folders'])$(id).disabled=locked;
    $('mappingMode').disabled=uploading||active()||Boolean(job);$('profile').disabled=uploading||active()||Boolean(job);
    $('validate').disabled=locked;$('apply').disabled=uploading||active()||!job?.valid;
    $('download').disabled=uploading||active()||!job||Boolean(job.counts.invalid)||!((job.counts.ready||0)+(job.counts.imported||0));
    $('pause').hidden=!active();$('resume').hidden=!job||!['paused','interrupted'].includes(job.state)||job.phase==='apply';
    $('newJob').disabled=uploading||active();$('jobHistory').disabled=uploading||active();
    $('discard').hidden=!job;$('discard').disabled=uploading||active();
    $('previousPage').disabled=!job||offset===0;$('nextPage').disabled=!job?.has_more;
    document.body.setAttribute('aria-busy',String(uploading||active()));
  }
  $('mappingMode').onchange=()=>{const manual=$('mappingMode').value==='manual';$('manualProfile').hidden=!manual;$('automaticHint').hidden=manual;$('profile').required=manual;};
  function render(value){
    job=value;remember(job.id);$('mappingMode').value=job.mode;$('manualProfile').hidden=job.mode!=='manual';$('automaticHint').hidden=job.mode==='manual';$('profile').required=false;$('results').hidden=false;$('rows').replaceChildren();
    const labels={awaiting_upload:'Aguardando envio',receiving:'Recebendo',uploaded:'Enviado',indexed:'Indexado',ready:'Pronto para revisão',invalid:'Bloqueado',duplicate:'Duplicado / já existente',imported:'Inserido',applying:'Gravando',unknown:'Conferir diário',upload_error:'Retomar envio'};
    for(const row of job.rows){
      const tr=document.createElement('tr'),name=document.createElement('td');
      if(['ready','imported','unknown'].includes(row.status)){const button=document.createElement('button');button.type='button';button.className='nfce-document-link';button.textContent=row.file;button.onclick=()=>showDocument(row.id);name.append(button);}else name.textContent=row.file;
      if(row.access_key){const code=document.createElement('code');code.textContent=row.access_key;name.append(code);}tr.append(name);
      for(const text of [row.number?row.series+'/'+row.number:'—',row.total?Number(row.total).toLocaleString('pt-BR',{style:'currency',currency:'BRL'}):'—',row.message||labels[row.status]||row.status]){const cell=document.createElement('td');cell.textContent=text;tr.append(cell);}
      if(['invalid','unknown','upload_error'].includes(row.status))tr.className='invalid';$('rows').append(tr);
    }
    const c=job.counts;
    $('scope').textContent='Banco: '+job.database+' · '+num(job.total)+' arquivos · '+num(c.ready)+' prontos · '+num(c.imported)+' inseridos · '+num(c.invalid)+' bloqueados · '+num(c.duplicate)+' duplicados';
    $('pageLabel').textContent=job.total?num(offset+1)+'–'+num(offset+job.rows.length)+' de '+num(job.total):'Nenhum arquivo enviado';
    const states={uploading:'Selecione os mesmos arquivos para retomar envios interrompidos.',queued:'Aguardando processamento.',validating:'Validando e identificando vínculos…',ready:'Validação concluída. Confira os documentos antes de inserir.',blocked:'Validação concluída com impedimentos. Nenhuma venda foi gravada.',paused:'Processamento pausado. Os arquivos concluídos foram preservados.',applying:'Inserindo e registrando cada documento no diário…',complete:'Processamento concluído.',interrupted:'Processamento interrompido; confira o diário antes de repetir escritas.',pausing:'Concluindo o arquivo atual antes de pausar…'};
    if(!uploading)$('status').textContent=(states[job.state]||job.state)+(job.error?' '+job.error:'');
    $('notice').textContent=job.notice+' A prévia é persistente. Escritas incertas não são repetidas automaticamente.';
    $('confirmation').placeholder='IMPORTAR '+job.database;controls();
  }
  async function refresh(){if(job)render(await api(root+'/'+job.id+'?offset='+offset));}
  async function poll(){const current=++epoch;while(job&&active()&&current===epoch){await new Promise(resolve=>setTimeout(resolve,900));if(current!==epoch)return;try{await refresh();}catch(error){message(error.message+' O processamento local pode continuar. Recarregue para consultar o progresso.');break;}}controls();}
  async function history(append=false){try{if(!append)historyOffset=0;const data=await api(root+'?offset='+historyOffset),select=$('jobHistory');if(!append)select.replaceChildren(new Option('Nova importação',''));select.querySelector('option[value="__more"]')?.remove();for(const item of data.jobs)select.add(new Option(new Date(item.created*1000).toLocaleString('pt-BR')+' · '+item.database_name+' · '+item.state,item.id));if(data.has_more)select.add(new Option('Carregar importações anteriores…','__more'));select.value=job?.id||'';}catch(error){message(error.message);}}
  function* selectedFiles(){for(const list of [$('xmls').files,$('folders').files])for(const file of list)if(file.name.toLowerCase().endsWith('.xml'))yield file;}
  $('importForm').onsubmit=async event=>{
    event.preventDefault();if(uploading||active())return;message('');
    const iterator=selectedFiles(),first=iterator.next();if(first.done){message('Selecione arquivos XML ou uma pasta.');return;}
    uploading=true;controls();const pending=new Set();let sent=0,failed=null;
    try{
      if(!job){const mode=$('mappingMode').value,body={mode};if(mode==='manual'){const profile=$('profile').files[0];if(!profile||!profile.name.toLowerCase().endsWith('.json'))throw new Error('Selecione um perfil JSON revisado, ou use a identificação automática.');body.profile=await profile.text();}job=await api(root,{method:'POST',body});remember(job.id);}
      if(job.state!=='uploading')throw new Error('Crie uma nova importação para selecionar outros arquivos.');
      const enqueue=async file=>{const slot=await api(root+'/'+job.id+'/slots',{method:'POST',body:{name:file.webkitRelativePath||file.name,size:file.size,modified:file.lastModified}});if(!slot.stored)await api(root+'/'+job.id+'/files/'+slot.upload_id,{method:'POST',raw:true,body:file,headers:{'Content-Type':'application/xml'}});sent++;$('status').textContent=num(sent)+' arquivos enviados ou retomados. Arquivos já guardados não são retransmitidos.';};
      async function send(file){if(failed)throw failed;const task=enqueue(file);pending.add(task);task.then(()=>pending.delete(task),error=>{pending.delete(task);failed=failed||error;});if(pending.size>=4)await Promise.race(pending);if(failed)throw failed;}
      await send(first.value);for(const file of iterator)await send(file);await Promise.all(pending);if(failed)throw failed;
      uploading=false;render(await api(root+'/'+job.id+'/validate',{method:'POST'}));history();await poll();
    }catch(error){await Promise.allSettled(pending);message(error.message+' Os envios concluídos permanecem guardados para retomada.');}
    finally{uploading=false;if(job)await refresh().catch(()=>{});controls();}
  };
  async function showDocument(fileId){try{const data=await api(root+'/'+job.id+'/documents/'+fileId),xml=JSON.parse(data.xml);xml.Xml='[XML original preservado; não repetido nesta visualização]';$('bson').textContent='Movimentacoes\n'+JSON.stringify(JSON.parse(data.movement),null,2)+'\nXmlMovimentacoes\n'+JSON.stringify(xml,null,2);$('bson').closest('details').open=true;}catch(error){message(error.message);}}
  $('previousPage').onclick=async()=>{offset=Math.max(0,offset-job.page_size);await refresh().catch(e=>message(e.message));};
  $('nextPage').onclick=async()=>{offset+=job.page_size;await refresh().catch(e=>message(e.message));};
  $('pause').onclick=async()=>{try{render(await api(root+'/'+job.id+'/pause',{method:'POST'}));}catch(error){message(error.message);}};
  $('resume').onclick=async()=>{try{message('');render(await api(root+'/'+job.id+'/validate',{method:'POST'}));await poll();}catch(error){message(error.message);}};
  $('newJob').onclick=()=>{epoch++;job=null;offset=0;remember(null);$('results').hidden=true;$('xmls').value='';$('folders').value='';$('profile').value='';$('mappingMode').onchange();$('status').textContent='Selecione os XMLs. Sem cota de arquivos ou tamanho total.';message('');history();controls();};
  $('jobHistory').onchange=async()=>{if($('jobHistory').value==='__more'){historyOffset+=50;await history(true);return;}if(!$('jobHistory').value){$('newJob').click();return;}try{epoch++;offset=0;render(await api(root+'/'+$('jobHistory').value));await poll();}catch(error){message(error.message);}};
  $('discard').onclick=async()=>{try{await api(root+'/'+job.id,{method:'DELETE'});$('newJob').click();}catch(error){message(error.message);}};
  $('download').onclick=async()=>{try{message('');if(auth&&window.showSaveFilePicker){const handle=await showSaveFilePicker({suggestedName:'nfce-import.zip',types:[{description:'Arquivo ZIP',accept:{'application/zip':['.zip']}}]});const response=await request(root+'/'+job.id+'/export');await response.body.pipeTo(await handle.createWritable());}else{const link=document.createElement('a');link.href=root+'/'+job.id+'/export';link.download='nfce-import.zip';document.body.append(link);link.click();link.remove();}}catch(error){if(error.name!=='AbortError')message(error.message);}};
  $('apply').onclick=async()=>{
    if(uploading||active()||!job?.valid)return;
    if($('confirmation').value!=='IMPORTAR '+job.database){message('Digite IMPORTAR '+job.database+' para confirmar o destino.');return;}
    try{message('');const session=await api('/api/editor/unlock',{method:'POST',body:{database:job.database,confirmation:'EDITAR '+job.database}});render(await api(root+'/'+job.id+'/apply',{method:'POST',body:{confirmation:$('confirmation').value},headers:{'X-Insight-Editor-Session':session.token}}));await poll();}catch(error){message(error.message+' Confira o progresso e o diário antes de repetir.');}
  };
  (async()=>{try{let id;try{id=localStorage.getItem(storage);}catch(_){}if(id){render(await api(root+'/'+id));poll();}await history();}catch(error){message(error.message);}controls();})();
})();

'use strict';
(() => {
  const $=id=>document.getElementById(id),root='/api/nfce-import/jobs',storage='digisat-nfce-job';
  const folderInputs = [...document.querySelectorAll('[data-dfe-source]')];
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
    for (const input of folderInputs) input.disabled = locked;
    for (const id of ['incomingFolder', 'purchaseFolder']) {
      $(id).disabled = locked || $('mappingMode').value !== 'manual';
    }
    for(const id of ['mappingMode','profile','xmls','folders','accessKeys','keyFile'])$(id).disabled=locked;
    $('mappingMode').disabled=uploading||active()||Boolean(job);$('profile').disabled=uploading||active()||Boolean(job);
    $('useBarcode').disabled=Boolean(job)||uploading||$('mappingMode').value==='manual';
    $('importMovements').disabled = Boolean(job) || uploading;
    $('importPeople').disabled = Boolean(job) || uploading || $('mappingMode').value !== 'manual';
    $('validate').disabled=locked;$('apply').disabled=uploading||active()||!job?.valid||job?.import_movements===false;
    $('download').disabled=uploading||active()||!job||Boolean(job.counts.invalid)||!((job.counts.ready||0)+(job.counts.imported||0));
    $('pause').hidden=!active();$('resume').hidden=!job||!['paused','interrupted'].includes(job.state)||job.phase==='apply';
    $('newJob').disabled=uploading||active();$('jobHistory').disabled=uploading||active();
    $('discard').hidden=!job;$('discard').disabled=uploading||active();
    $('previousPage').disabled=!job||offset===0;$('nextPage').disabled=!job?.has_more;
    document.body.setAttribute('aria-busy',String(uploading||active()));
  }
  $('mappingMode').onchange=()=>{const manual=$('mappingMode').value==='manual';$('manualProfile').hidden=!manual;$('automaticHint').hidden=manual;$('profile').required=manual;controls();};
  function render(value){
    const costReview = $('purchaseCostReview');
    if (job?.id !== value.id || job?.state !== value.state) {
      costReview.open = false;
      $('purchaseCostList').textContent = '';
      $('purchaseCostActions').replaceChildren();
      $('purchaseCostBatch').hidden = true;
      $('useLastPurchaseCost').checked = false;
      $('applyPurchaseCosts').disabled = true;
    }
    job=value;$('importPeople').checked=Boolean(job.import_people);$('importMovements').checked=job.import_movements!==false;$('useBarcode').checked=Boolean(job.use_barcode);remember(job.id);$('mappingMode').value=job.mode;$('manualProfile').hidden=job.mode!=='manual';$('automaticHint').hidden=job.mode==='manual';$('profile').required=false;$('results').hidden=false;$('rows').replaceChildren();
    const labels={awaiting_upload:'Aguardando envio',receiving:'Recebendo',uploaded:'Enviado',indexed:'Indexado',ready:'Pronto para revisão',invalid:'Bloqueado',duplicate:'Duplicado / já existente',imported:'Inserido',applying:'Gravando',unknown:'Conferir diário',upload_error:'Retomar envio'};
    for(const row of job.rows){
      const tr=document.createElement('tr'),name=document.createElement('td');
      if(['ready','imported','unknown'].includes(row.status)||row.model==='59'){const button=document.createElement('button');button.type='button';button.className='nfce-document-link';button.textContent=row.file;button.onclick=()=>showDocument(row.id);name.append(button);}else name.textContent=row.file;
      if(row.access_key){const code=document.createElement('code');code.textContent=row.access_key;name.append(code);}tr.append(name);
      for(const text of [row.number?row.series+'/'+row.number:'—',row.total?Number(row.total).toLocaleString('pt-BR',{style:'currency',currency:'BRL'}):'—',row.message||labels[row.status]||row.status]){const cell=document.createElement('td');cell.textContent=text;tr.append(cell);}
      if(['invalid','unknown','upload_error'].includes(row.status))tr.className='invalid';$('rows').append(tr);
    }
    const c=job.counts;
    $('scope').textContent='Banco: '+job.database+' · '+num(job.total)+' arquivos · '+num(c.ready)+' prontos · '+num(c.imported)+' inseridos · '+num(c.invalid)+' bloqueados · '+num(c.duplicate)+' duplicados';
    $('pageLabel').textContent=job.total?num(offset+1)+'–'+num(offset+job.rows.length)+' de '+num(job.total):'Nenhum arquivo enviado';
    const states={uploading:'Informe chaves ou selecione arquivos. Os documentos já guardados podem seguir para validação.',queued:'Aguardando processamento.',validating:'Validando e identificando vínculos…',ready:'Validação concluída. Confira os documentos antes de inserir.',blocked:'Validação concluída com impedimentos. Nenhuma venda foi gravada.',paused:'Processamento pausado. Os arquivos concluídos foram preservados.',applying:'Inserindo e registrando cada documento no diário…',complete:'Processamento concluído.',interrupted:'Processamento interrompido; confira o diário antes de repetir escritas.',pausing:'Concluindo o arquivo atual antes de pausar…'};
    if(!uploading)$('status').textContent=(states[job.state]||job.state)+(job.error?' '+job.error:'');
    $('notice').textContent=job.notice+' A prévia é persistente. Escritas incertas não são repetidas automaticamente.';
    $('confirmation').placeholder='IMPORTAR '+job.database;controls();
    costReview.hidden = job.mode !== 'manual' || !['ready', 'complete'].includes(job.state);
    $('insertPeople').hidden = !job.import_people || !['ready', 'complete'].includes(job.state);
  }
  $('insertPeople').onclick = async () => {
    const button = $('insertPeople');
    const {id, database} = job;
    const path = root+'/'+id+'/people';
    let applied = 0;
    button.disabled = true;
    try {
      const {people} = await api(path);
      if (job?.id !== id) return;
      if (!people.length) { message('Nenhuma pessoa preparada neste lote.'); return; }
      const sample = people.slice(0, 10).map(person => person.name+' · '+person.document).join('\n');
      if (!window.confirm('Cadastrar '+people.length+' pessoas no banco '+database+'?\n'+sample+
                          (people.length > 10 ? '\n…' : ''))) return;
      const session = await api('/api/editor/unlock', {
        method:'POST', body:{database, confirmation:'EDITAR '+database},
      });
      if (job?.id !== id) return;
      const headers = {'X-Insight-Editor-Session':session.token};
      const result = await api(path+'/preview', {method:'POST', headers,
        body:{confirmation:'IMPORTAR '+database, ids:people.map(person => person.id).sort()}});
      for (const preview of result.previews) {
        if (job?.id !== id) return;
        await api('/api/editor/apply/'+preview, {method:'POST', headers, body:{confirmation:'Pessoas'}});
        applied++;
      }
      if (job?.id === id) $('status').textContent = applied+' pessoas cadastradas · '+result.existing+' já existentes.';
    } catch (error) {
      if (job?.id === id) message(applied+' pessoas cadastradas antes da interrupção. '+error.message);
    } finally {
      button.disabled = false;
    }
  };
  $('purchaseCostReview').addEventListener('toggle', async event => {
    if (!event.currentTarget.open || !job) return;
    const jobId = job.id;
    const list = $('purchaseCostList');
    const actions = $('purchaseCostActions');
    actions.replaceChildren();
    list.textContent = 'Carregando custos das compras…';
    try {
      const data = await api(root+'/'+jobId+'/purchase-costs');
      if (job?.id !== jobId) return;
      const costs = JSON.parse(data.costs);
      list.textContent = costs.length ? costs.map(cost =>
        [cost.product_code, 'R$ '+cost.unit_cost.replace('.', ','), cost.issued_at,
         'Chave '+cost.access_key+' · item '+cost.line,
         cost.conflict ? 'Conflito entre custos com a mesma data de emissão.' : ''].filter(Boolean).join(' · ')
      ).join('\n') : 'Nenhuma nota de compra validada neste lote.';
      if (job.state === 'complete') {
        const eligible = costs.filter(value => !value.conflict);
        $('purchaseCostBatch').hidden = !eligible.length;
        $('applyPurchaseCosts').onclick = () => applyPurchaseCosts(jobId, eligible);
        for (const cost of eligible) {
          const button = document.createElement('button');
          button.type = 'button';
          button.className = 'secondary';
          button.textContent = 'Revisar atualização de custo · '+cost.product_code;
          button.onclick = () => reviewCost(jobId, cost, button);
          actions.append(button);
        }
      }
    } catch (error) {
      if (job?.id === jobId) list.textContent = error.message;
    }
  });
  $('useLastPurchaseCost').onchange = event => {
    $('applyPurchaseCosts').disabled = !event.target.checked;
  };
  async function applyPurchaseCosts(jobId, costs) {
    const checkbox = $('useLastPurchaseCost');
    const submit = $('applyPurchaseCosts');
    if (!checkbox.checked || job?.id !== jobId) return;
    const database = job.database;
    if (!window.confirm('Aplicar os custos revisados de '+costs.length+' produtos no banco '+database+'?')) return;
    checkbox.disabled = submit.disabled = true;
    let applied = 0;
    try {
      const session = await api('/api/editor/unlock', {
        method: 'POST', body: {database, confirmation: 'EDITAR '+database},
      });
      const headers = {'X-Insight-Editor-Session': session.token};
      for (const cost of costs) {
        if (job?.id !== jobId) break;
        const {preview} = await api(root+'/'+jobId+'/purchase-costs/'+cost.company_product_id.$oid+'/preview', {
          method: 'POST', headers,
        });
        if (job?.id !== jobId) break;
        if (!preview) continue;
        await api('/api/editor/apply/'+preview.id, {method: 'POST', headers, body: {confirmation: 'Precos'}});
        applied++;
      }
      if (job?.id === jobId) $('purchaseCostList').append('\n\n'+applied+' preços de custo atualizados.');
    } catch (error) {
      if (job?.id === jobId) message(applied+' preços atualizados antes da interrupção. '+error.message);
    } finally {
      checkbox.disabled = false;
      checkbox.checked = false;
      submit.disabled = true;
    }
  }
  async function reviewCost(jobId, cost, button) {
    const database = job.database;
    button.disabled = true;
    try {
      const session = await api('/api/editor/unlock', {
        method: 'POST', body: {database, confirmation: 'EDITAR '+database},
      });
      if (job?.id !== jobId) return;
      const headers = {'X-Insight-Editor-Session': session.token};
      const {preview} = await api(root+'/'+jobId+'/purchase-costs/'+cost.company_product_id.$oid+'/preview', {
        method: 'POST', headers,
      });
      if (job?.id !== jobId) return;
      if (!preview) {
        button.textContent = 'Custo já atualizado · '+cost.product_code;
        return;
      }
      const current = JSON.parse(preview.before_json).Custo.Valor;
      const formatted = typeof current === 'object' ? Object.values(current)[0] : current;
      if (!window.confirm('Atualizar o custo de '+cost.product_code+' de R$ '+formatted+
                          ' para R$ '+cost.unit_cost+' no banco '+database+'?')) return;
      await api('/api/editor/apply/'+preview.id, {method: 'POST', headers, body: {confirmation: 'Precos'}});
      button.textContent = 'Custo atualizado · '+cost.product_code;
    } catch (error) {
      message(error.message);
    } finally {
      button.disabled = false;
    }
  }
  async function refresh(){if(job)render(await api(root+'/'+job.id+'?offset='+offset));}
  async function poll(){const current=++epoch;while(job&&active()&&current===epoch){await new Promise(resolve=>setTimeout(resolve,900));if(current!==epoch)return;try{await refresh();}catch(error){message(error.message+' O processamento local pode continuar. Recarregue para consultar o progresso.');break;}}controls();}
  async function history(append=false){try{if(!append)historyOffset=0;const data=await api(root+'?offset='+historyOffset),select=$('jobHistory');if(!append)select.replaceChildren(new Option('Nova importação',''));select.querySelector('option[value="__more"]')?.remove();for(const item of data.jobs)select.add(new Option(new Date(item.created*1000).toLocaleString('pt-BR')+' · '+item.database_name+' · '+item.state,item.id));if(data.has_more)select.add(new Option('Carregar importações anteriores…','__more'));select.value=job?.id||'';}catch(error){message(error.message);}}
  function* selectedFiles(){
    for (const input of [$('xmls'), $('folders'), ...folderInputs]) {
      if (['incoming', 'purchase'].includes(input.dataset.dfeSource) && $('mappingMode').value !== 'manual') continue;
      for (const file of input.files) {
        if (file.name.toLowerCase().endsWith('.xml') || file.name.toLowerCase().endsWith('.dbf')) yield {file, source: input.dataset.dfeSource};
      }
    }
  }
  function splitKeys(text){
    const entries=text.split(/[\r\n\t,;]+/).map(value=>value.trim().replace(/^"(.*)"$/, '$1'));
    const header=entries.findIndex(Boolean);
    if(header>=0&&/^chave(?:[ _-]*(?:de[ _-]*)?acesso)?$/i.test(entries[header]))entries.splice(header,1);
    return entries.map(value=>value.replace(/[\s.-]+/g,'')).filter(Boolean);
  }
  function showKeyIssues(issues){
    const list=$('keyIssueList');
    list.replaceChildren();
    for(const issue of issues){
      const item=document.createElement('li');
      item.textContent=(issue.key||'Chave em branco')+' — '+issue.message;
      list.append(item);
    }
    $('keyIssues').hidden=!issues.length;
  }
  $('importForm').onsubmit=async event=>{
    event.preventDefault();if(uploading||active())return;message('');
    const iterator=selectedFiles(),first=iterator.next();
    showKeyIssues([]);
    uploading=true;controls();const pending=new Set();let sent=0,failed=null;
    try{
      const keyFile=$('keyFile').files[0];
      let fileKeys=[];
      if(keyFile){
        if(!/\.(txt|csv|tsv)$/i.test(keyFile.name))throw new Error('Selecione um arquivo de chaves TXT, CSV ou TSV.');
        const bytes=new Uint8Array(await keyFile.arrayBuffer());
        const encoding=bytes[0]===255&&bytes[1]===254?'utf-16le':
          bytes[0]===254&&bytes[1]===255?'utf-16be':'utf-8';
        fileKeys=splitKeys(new TextDecoder(encoding).decode(bytes));
        if(!fileKeys.length)throw new Error('O arquivo de chaves está vazio.');
        // Keep all entries editable if the lookup reports unresolved keys.
        $('accessKeys').value=[...new Set([...splitKeys($('accessKeys').value),...fileKeys])].join('\n');
        $('keyFile').value='';
      }
      const keys=[...new Set(splitKeys($('accessKeys').value))];
      if(first.done&&!keys.length&&!job?.counts?.uploaded){message('Informe chaves de acesso ou selecione arquivos XML.');return;}
      if(!job){const mode=$('mappingMode').value,body={mode,import_people:mode==='manual'&&$('importPeople').checked,import_movements:$('importMovements').checked,use_barcode:mode==='automatic'&&$('useBarcode').checked};if(mode==='manual'){const profile=$('profile').files[0];if(!profile||!profile.name.toLowerCase().endsWith('.json'))throw new Error('Selecione um perfil JSON revisado, ou use a identificação automática.');body.profile=await profile.text();}job=await api(root,{method:'POST',body});remember(job.id);}
      if(job.state!=='uploading')throw new Error('Crie uma nova importação para selecionar outros arquivos.');
      const issues=[];
      for(let index=0;index<keys.length;index+=50){
        $('status').textContent='Consultando chaves '+num(index+1)+'–'+num(Math.min(index+50,keys.length))+' de '+num(keys.length)+'…';
        const lookup=await api(root+'/'+job.id+'/keys',{method:'POST',body:{keys:keys.slice(index,index+50)}});
        issues.push(...lookup.results.filter(item=>item.status!=='stored'));
      }
      if(issues.length){
        showKeyIssues(issues);
        message('Revise as chaves indicadas. Para continuar só com os XMLs encontrados, retire da lista as chaves pendentes.');
        return;
      }
      const enqueue=async ({file, source})=>{const slot=await api(root+'/'+job.id+'/slots',{method:'POST',body:{name:file.webkitRelativePath||file.name,size:file.size,modified:file.lastModified,source}});if(!slot.stored){const type=file.name.toLowerCase().endsWith('.dbf')?'application/x-dbf':'application/xml';await api(root+'/'+job.id+'/files/'+slot.upload_id,{method:'POST',raw:true,body:file,headers:{'Content-Type':type}});}sent++;$('status').textContent=num(sent)+' arquivos enviados ou retomados. Arquivos já guardados não são retransmitidos.';};
      async function send(file){if(failed)throw failed;const task=enqueue(file);pending.add(task);task.then(()=>pending.delete(task),error=>{pending.delete(task);failed=failed||error;});if(pending.size>=4)await Promise.race(pending);if(failed)throw failed;}
      if(!first.done)await send(first.value);for(const file of iterator)await send(file);await Promise.all(pending);if(failed)throw failed;
      $('accessKeys').value='';
      uploading=false;render(await api(root+'/'+job.id+'/validate',{method:'POST'}));history();await poll();
    }catch(error){await Promise.allSettled(pending);message(error.message+' Os envios concluídos permanecem guardados para retomada.');}
    finally{uploading=false;if(job)await refresh().catch(()=>{});controls();}
  };
  async function showDocument(fileId, preloaded) {
    const personButton = $('insertPerson');
    const jobId = job.id;
    personButton.hidden = true;
    try {
      const data = preloaded || await api(root+'/'+job.id+'/documents/'+fileId);
      if (job?.id !== jobId) return;
      if (data.cfe_review) {
        $('bson').textContent = 'Conferência do XML CF-e/SAT\n'+JSON.stringify(data.cfe_review, null, 2);
        $('bson').closest('details').open = true;
        return;
      }
      if (data.dbf_review) {
        const review = data.dbf_review;
        const targets = review.target_fields || [];
        const mapping = review.suggested_mapping || {};
        const wrap = document.createElement('div');
        wrap.className = 'dbf-mapping';
        const title = document.createElement('p');
        title.textContent = 'Conferência de arquivo DBF · '+review.rows+' registros · mapeamento manual/automático por nome similar.';
        wrap.appendChild(title);
        if (review.presets) {
          const pLabel = document.createElement('label'); pLabel.textContent = 'Sistema de origem / preset';
          const pSel = document.createElement('select');
          review.presets.forEach(p => { const o=document.createElement('option'); o.value=p.id; o.textContent=p.label; if (p.id===review.preset) o.selected=true; pSel.appendChild(o); });
          pSel.onchange = async () => {
            try { const d = await api(root+'/'+job.id+'/documents/'+fileId+'?preset='+encodeURIComponent(pSel.value)); if (job?.id===jobId) showDocument(fileId, d); }
            catch (e) { message(e.message); }
          };
          pLabel.appendChild(pSel); wrap.appendChild(pLabel);
        }
        const table = document.createElement('table');
        table.innerHTML = '<thead><tr><th>Coluna DBF</th><th>Campo destino</th><th>Auto</th></tr></thead>';
        const tb = document.createElement('tbody');
        (review.columns || []).forEach(col => {
          const tr = document.createElement('tr');
          const tdCol = document.createElement('td'); tdCol.textContent = col; tr.appendChild(tdCol);
          const tdSel = document.createElement('td');
          const sel = document.createElement('select');
          sel.dataset.col = col;
          const empty = document.createElement('option'); empty.value=''; empty.textContent='(não mapear)'; sel.appendChild(empty);
          targets.forEach(t => { const o=document.createElement('option'); o.value=t.id; o.textContent=t.label; sel.appendChild(o); });
          sel.value = (mapping[col] && mapping[col].target) || '';
          tdSel.appendChild(sel); tr.appendChild(tdSel);
          const tdAuto = document.createElement('td'); tdAuto.textContent = mapping[col]?.auto ? 'auto' : 'manual'; tr.appendChild(tdAuto);
          tb.appendChild(tr);
        });
        table.appendChild(tb); wrap.appendChild(table);
        const actions = document.createElement('div'); actions.className='nfce-actions';
        const dl = document.createElement('button'); dl.type='button'; dl.className='secondary'; dl.textContent='Baixar mapeamento JSON';
        dl.onclick = () => {
          const out = {};
          wrap.querySelectorAll('select[data-col]').forEach(s => { if (s.value) out[s.dataset.col] = s.value; });
          const blob = new Blob([JSON.stringify({schema:'insight.dbf.mapping.v1',file:review.file,preset:review.preset,mapping:out}, null, 2)], {type:'application/json'});
          const a=document.createElement('a'); a.href=URL.createObjectURL(blob); a.download=(review.file||'dbf')+'.mapping.json'; a.click(); URL.revokeObjectURL(a.href);
        };
        actions.appendChild(dl);
        const note = document.createElement('p'); note.className='nfce-note'; note.textContent='Mapeamento automático por similaridade de nome. Presets ARPA/SGBR/G4/G5 são pontos de partida e devem ser revisados. A conversão nativa campo-a-campo depende de perfil/mapeamento revisado para o layout do sistema de origem.';
        actions.appendChild(note);
        wrap.appendChild(actions);
        const pre = document.createElement('pre'); pre.textContent = JSON.stringify(review.sample||[], null, 2);
        wrap.appendChild(pre);
        const box = $('bson'); box.replaceChildren(wrap); box.closest('details').open = true;
        return;
      }
      const xml = JSON.parse(data.xml);
      xml.Xml = '[XML original preservado; não repetido nesta visualização]';
      const sections = [
        'Movimentacoes\n'+JSON.stringify(JSON.parse(data.movement), null, 2),
        'XmlMovimentacoes\n'+JSON.stringify(xml, null, 2),
      ];
      if (data.purchase_cost_candidates) {
        sections.push('Custos unitários do XML de compra\n'+JSON.stringify(JSON.parse(data.purchase_cost_candidates), null, 2));
      }
      if (data.product_registrations) {
        sections.push('Cadastros de produto preparados\n'+JSON.stringify(data.product_registrations, null, 2));
      }
      if (data.finance) {
        sections.push('Títulos financeiros · '+data.finance.collection+' · sem baixa\n'+
          JSON.stringify(data.finance, null, 2));
      }
      if (data.installments?.length) {
        const title = data.installment_kind === 'payable' ? 'Parcelas a pagar' : 'Parcelas a receber';
        sections.push(title+' informadas no XML\n'+data.installments.map(
          entry => [entry.number || '—', entry.date || 'Sem vencimento', 'R$ '+entry.amount.replace('.', ',')].join(' · ')
        ).join('\n'));
      }
      if (data.parties?.length) {
        sections.push('Pessoas informadas no XML\n'+data.parties.map(person => [
          person.role === 'issuer' ? 'Emitente' : 'Destinatário',
          person.name, person.document, person.address.xMun, person.address.UF,
        ].filter(Boolean).join(' · ')).join('\n'));
      }
      if (data.people) {
        sections.push('Cadastros de pessoas preparados\n'+JSON.stringify(JSON.parse(data.people), null, 2));
        personButton.hidden = !job.import_people || !['ready', 'complete'].includes(job.state);
        personButton.onclick = () => insertReviewedPerson(jobId, fileId, JSON.parse(data.people)[0].Nome);
      }
      if (data.products?.length) {
        sections.push('Produtos informados no XML\n'+data.products.map(product => [
          'Item '+product.line, product.code, product.description, product.barcode,
          'Vínculo '+product.company_product_id,
        ].filter(Boolean).join(' · ')).join('\n'));
      }
      $('bson').textContent = sections.join('\n\n');
      $('bson').closest('details').open = true;
    } catch (error) {
      message(error.message);
    }
  }
  async function insertReviewedPerson(jobId, fileId, name) {
    if (job?.id !== jobId) return;
    const database = job.database;
    if (!window.confirm('Cadastrar '+name+' no banco '+database+'?')) return;
    const button = $('insertPerson');
    button.disabled = true;
    try {
      const session = await api('/api/editor/unlock', {
        method: 'POST', body: {database, confirmation: 'EDITAR '+database},
      });
      if (job?.id !== jobId) return;
      const headers = {'X-Insight-Editor-Session': session.token};
      const {preview} = await api(root+'/'+jobId+'/documents/'+fileId+'/person/preview', {method: 'POST', headers});
      if (job?.id !== jobId) return;
      if (preview) await api('/api/editor/apply/'+preview.id, {method: 'POST', headers, body: {confirmation: 'Pessoas'}});
      button.textContent = preview ? 'Pessoa cadastrada' : 'Documento já cadastrado';
    } catch (error) {
      message(error.message);
    } finally {
      button.disabled = false;
    }
  }
  $('previousPage').onclick=async()=>{offset=Math.max(0,offset-job.page_size);await refresh().catch(e=>message(e.message));};
  $('nextPage').onclick=async()=>{offset+=job.page_size;await refresh().catch(e=>message(e.message));};
  $('pause').onclick=async()=>{try{render(await api(root+'/'+job.id+'/pause',{method:'POST'}));}catch(error){message(error.message);}};
  $('resume').onclick=async()=>{try{message('');render(await api(root+'/'+job.id+'/validate',{method:'POST'}));await poll();}catch(error){message(error.message);}};
  $('newJob').onclick=()=>{epoch++;job=null;offset=0;for(const input of folderInputs)input.value='';remember(null);$('results').hidden=true;$('xmls').value='';$('folders').value='';$('profile').value='';$('accessKeys').value='';$('keyFile').value='';showKeyIssues([]);$('mappingMode').onchange();$('status').textContent='Informe as chaves ou selecione os XMLs para começar.';message('');history();controls();};
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

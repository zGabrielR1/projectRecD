/* Read-only composition purchases. All figures come from the authenticated API. */
'use strict';
(() => {
  const $ = id => document.getElementById(id);
  const esc = value => String(value ?? '').replace(/[&<>"']/g, c => ({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[c]));
  const number = (value, digits = 3) => value == null || value === ''
    ? '—'
    : new Intl.NumberFormat('pt-BR', {maximumFractionDigits: digits}).format(Number(value));
  const integer = value => value == null ? '—' : new Intl.NumberFormat('pt-BR', {maximumFractionDigits: 0}).format(Number(value));
  const date = value => value ? String(value).slice(0, 10).split('-').reverse().join('/') : '—';
  const iso = value => `${value.getFullYear()}-${String(value.getMonth() + 1).padStart(2, '0')}-${String(value.getDate()).padStart(2, '0')}`;
  const option = (select, value, label) => select.add(new Option(label, value));
  const state = {auth: null, authWait: null, controller: null, result: null, applied: null};

  function error(message) {
    $('error').hidden = !message;
    $('error').textContent = message || '';
  }

  function notice(message) {
    $('notice').hidden = !message;
    $('notice').textContent = message || '';
  }

  function busy(on) {
    document.body.setAttribute('aria-busy', String(on));
    $('run').disabled = on;
    $('run').textContent = on ? 'Consultando…' : 'Consultar compras';
  }

  function authenticate() {
    if (state.authWait) return state.authWait;
    state.authWait = new Promise((resolve, reject) => {
      const dialog = $('authDialog');
      const form = $('authForm');
      const cancelButton = $('authCancel');
      const cleanup = () => {
        form.removeEventListener('submit', submit);
        cancelButton.removeEventListener('click', cancel);
        dialog.removeEventListener('cancel', cancel);
        if (dialog.open) dialog.close();
        $('authPassword').value = '';
      };
      const submit = event => {
        event.preventDefault();
        const bytes = new TextEncoder().encode(`${$('authUser').value}:${$('authPassword').value}`);
        state.auth = 'Basic ' + btoa(String.fromCharCode(...bytes));
        cleanup();
        resolve();
      };
      const cancel = () => {
        cleanup();
        reject(new Error('Acesso cancelado. Confira as credenciais e tente novamente.'));
      };
      form.addEventListener('submit', submit);
      cancelButton.addEventListener('click', cancel);
      dialog.addEventListener('cancel', cancel);
      dialog.showModal();
    }).finally(() => { state.authWait = null; });
    return state.authWait;
  }

  async function api(path, options = {}) {
    const send = () => fetch(path, {
      ...options,
      headers: {
        ...(options.body ? {'Content-Type': 'application/json'} : {}),
        ...(state.auth ? {Authorization: state.auth} : {}),
        ...(options.headers || {}),
      },
    });
    let response = await send();
    if (response.status === 401) {
      await authenticate();
      response = await send();
    }
    let data;
    try {
      data = await response.json();
    } catch (_) {
      throw new Error('O serviço não retornou uma resposta válida. Confira se o Insight está em execução.');
    }
    if (!response.ok) {
      let detail = data.detail;
      if (Array.isArray(detail)) detail = detail.map(item => item.msg).join(' · ');
      throw new Error(typeof detail === 'string' ? detail : 'Não foi possível concluir a consulta.');
    }
    return data;
  }

  function setDates() {
    const now = new Date();
    $('start').value = iso(new Date(now.getFullYear(), now.getMonth(), 1));
    $('end').value = iso(now);
  }

  async function loadCompanies() {
    $('filterStatus').textContent = 'Carregando empresas…';
    const data = await api('/api/workspace/composition/companies');
    const select = $('company');
    select.replaceChildren(new Option('Selecione uma empresa', ''));
    for (const company of data.companies) option(select, company.id, company.name);
    let saved = null;
    try { saved = localStorage.getItem('insight.composition.company'); } catch (_) { /* Storage may be blocked. */ }
    if (saved && data.companies.some(company => company.id === saved)) select.value = saved;
    else if (data.companies.length === 1) select.value = data.companies[0].id;
    if (!data.companies.length) {
      $('filterStatus').textContent = 'Nenhuma empresa com composição encontrada.';
      $('run').disabled = true;
      return;
    }
    $('filterStatus').textContent = `${data.companies.length} empresa(s) com composição.`;
    await loadOptions();
  }

  async function loadOptions() {
    const company = $('company').value;
    const select = $('composition');
    const current = select.value;
    select.replaceChildren(new Option('Todas as composições', ''));
    if (!company) return;
    try {
      const data = await api(`/api/workspace/composition/options?company_id=${encodeURIComponent(company)}`);
      for (const composition of data.compositions) {
        const label = `${composition.code ? composition.code + ' · ' : ''}${composition.description}`
          + ` (${composition.components} componente${composition.components === 1 ? '' : 's'})`;
        option(select, composition.id, label);
      }
      select.value = data.compositions.some(composition => composition.id === current) ? current : '';
      if (!data.compositions.length) $('filterStatus').textContent = 'Nenhuma composição ativa nesta empresa.';
    } catch (err) {
      if (err.name !== 'AbortError') error(err.message);
    }
  }

  function requestBody() {
    return {
      company_id: $('company').value,
      start_date: $('start').value,
      end_date: $('end').value,
      composition_id: $('composition').value || '',
      search: $('search').value.trim(),
      concluded_only: $('concluded').checked,
      include_optional: $('optional').checked,
    };
  }

  async function run(event) {
    event.preventDefault();
    error('');
    notice('');
    if (!$('company').value) { error('Selecione uma empresa com composição.'); return; }
    if (!$('start').value || !$('end').value) { error('Informe a data inicial e a data final.'); return; }
    if ($('start').value > $('end').value) { error('A data final não pode ser anterior à data inicial.'); return; }
    const body = requestBody();
    const request = new AbortController();
    state.controller?.abort();
    state.controller = request;
    busy(true);
    try {
      const data = await api('/api/workspace/composition/purchases', {
        method: 'POST',
        body: JSON.stringify(body),
        signal: request.signal,
      });
      state.applied = body;
      state.result = data.result;
      render(data.result);
      if (!data.result.compositions.length) {
        notice('Nenhuma composição corresponde aos filtros. Ajuste a pesquisa ou selecione outra composição.');
      } else if (!data.result.summary.purchase_lines) {
        notice('As composições foram encontradas, mas nenhuma compra de componente entrou no período com os filtros atuais.');
      }
    } catch (err) {
      if (err.name !== 'AbortError') error(err.message);
    } finally {
      if (state.controller === request) busy(false);
    }
  }

  function render(data) {
    $('empty').hidden = true;
    $('result').hidden = false;
    const company = $('company').selectedOptions[0]?.textContent || 'Empresa';
    $('resultTitle').textContent = data.compositions.length === 1
      ? 'Compras de uma composição'
      : `${data.compositions.length} composições analisadas`;
    $('resultPeriod').textContent = `${date(state.applied.start_date)} a ${date(state.applied.end_date)} · ${company}`;
    $('resultBadges').innerHTML = `<span class="chip">${integer(data.summary.compositions)} composições</span>`
      + `<span class="chip">${integer(data.summary.purchase_documents)} documentos</span>`;
    renderSummary(data.summary);
    $('compositions').innerHTML = data.compositions.length
      ? data.compositions.map(renderComposition).join('')
      : '<div class="composition-empty panel"><h2>Nenhuma composição no recorte</h2><p>Verifique a empresa, o período e a pesquisa informada.</p></div>';
    renderMethod(data);
    renderWarnings(data.warnings);
    $('provenance').textContent = `Base: ${data.provenance.database} · consulta em ${new Date().toLocaleString('pt-BR')} · ${number(data.duration_ms, 0)} ms`;
    $('exportCsv').disabled = !data.rows.length;
  }

  function renderSummary(summary) {
    const cards = [
      ['Composições', summary.compositions, 'analisadas no período'],
      ['Componentes', summary.components, 'somados nas composições'],
      ['Documentos de compra', summary.purchase_documents, 'com ao menos um componente'],
      ['Linhas de compra', summary.purchase_lines, 'itens não cancelados'],
    ];
    $('summary').innerHTML = cards.map(([label, value, note]) =>
      `<div class="composition-stat"><span>${esc(label)}</span><strong>${integer(value)}</strong><small>${esc(note)}</small></div>`
    ).join('');
  }

  function renderComposition(composition) {
    const possible = composition.possible_compositions == null ? '—' : integer(composition.possible_compositions);
    const limiting = composition.limiting_component
      ? `limitado por ${esc(composition.limiting_component.description || composition.limiting_component.code)}`
      : 'sem componente obrigatório com quantidade válida';
    const count = `${integer(composition.mandatory_count)} obrigatório${composition.mandatory_count === 1 ? '' : 's'}`
      + ` · ${integer(composition.component_count)} componente${composition.component_count === 1 ? '' : 's'}`;
    const components = composition.components.length
      ? composition.components.map(renderComponent).join('')
      : '<p class="component-empty">Esta composição não possui componentes cadastrados.</p>';
    return `<article class="composition-card">
      <header class="composition-card-head">
        <div class="composition-title">
          <span class="composition-code">${esc(composition.code || '—')}</span>
          <div><h2>${esc(composition.description)}</h2><p>${esc(composition.unit || 'sem unidade')} · ${esc(count)}</p></div>
        </div>
        <div class="possible-box${composition.possible_compositions ? ' has-value' : ''}">
          <small>Composições possíveis</small>
          <strong>${possible}</strong>
          <span>${limiting}</span>
        </div>
      </header>
      <div class="composition-components">${components}</div>
    </article>`;
  }

  function renderComponent(component) {
    const hasPurchases = component.purchases.length > 0;
    const rows = component.purchases.map(purchase => `<tr>
      <td><strong>${esc(purchase.number || '—')}</strong><small>${esc(purchase.document_type || '')}</small></td>
      <td>${esc(purchase.series || '—')}</td>
      <td>${date(purchase.date)}</td>
      <td>${esc(purchase.supplier || '—')}</td>
      <td class="numeric">${number(purchase.quantity)} <small>${esc(purchase.document_unit || '')}</small></td>
      <td class="numeric">${number(purchase.stock_quantity)} <small>${esc(purchase.unit || '')}</small></td>
      <td><span class="composition-status ${purchase.concluded ? 'is-concluded' : 'is-pending'}">${esc(purchase.situation || '—')}</span></td>
    </tr>`).join('');
    const table = hasPurchases
      ? `<div class="table-wrap"><table>
          <caption class="sr-only">Compras do componente ${esc(component.description)}</caption>
          <thead><tr><th scope="col">Movimento</th><th scope="col">Série</th><th scope="col">Data</th><th scope="col">Fornecedor</th><th scope="col" class="numeric">Qtd. comprada</th><th scope="col" class="numeric">Qtd. estoque</th><th scope="col">Situação</th></tr></thead>
          <tbody>${rows}</tbody>
        </table></div>`
      : '<p class="component-empty">Nenhuma compra deste componente no período com os filtros atuais.</p>';
    return `<details class="component-row" ${hasPurchases ? 'open' : ''}>
      <summary>
        <span class="component-identity"><strong>${esc(component.description)}</strong><small>${esc(component.code || 'sem código')} · ${esc(component.unit || 'sem unidade')}${component.optional ? ' · opcional' : ''}</small></span>
        <span class="component-metrics">
          <span><small>Por composição</small><strong>${number(component.quantity_per_composition)}</strong></span>
          <span><small>Comprado</small><strong>${number(component.purchased_total)}</strong></span>
          <span><small>Possíveis</small><strong>${component.possible_compositions == null ? '—' : integer(component.possible_compositions)}</strong></span>
        </span>
        <span class="component-chevron" aria-hidden="true">⌄</span>
      </summary>
      <div class="component-body">
        ${table}
        <p class="component-foot">${integer(component.purchase_documents)} documento(s) · ${integer(component.purchase_count)} linha(s) de compra${component.optional ? ' · componente opcional, não limita o resultado' : ''}.</p>
      </div>
    </details>`;
  }

  function renderMethod(data) {
    const summary = data.interpretation?.summary || [];
    const formula = 'Composições possíveis = menor valor inteiro entre (quantidade de estoque comprada ÷ quantidade do componente na composição) para os componentes obrigatórios.';
    $('method').innerHTML = `<ul>${summary.map(line => `<li>${esc(line)}</li>`).join('')}</ul><p>${esc(formula)}</p>`
      + '<p>A quantidade de estoque é a quantidade convertida do item de compra. Quando a conversão não existe, a quantidade do documento é usada.</p>';
  }

  function renderWarnings(warnings) {
    $('warningCount').textContent = warnings.length ? `(${warnings.length})` : '';
    $('warnings').innerHTML = warnings.map(warning => `<li>${esc(warning)}</li>`).join('') || '<li>Nenhum aviso adicional.</li>';
  }

  function csvCell(value) {
    const text = String(value ?? '');
    return /[";\r\n]/.test(text) ? `"${text.replace(/"/g, '""')}"` : text;
  }

  function exportCsv() {
    if (!state.result?.rows?.length) return;
    const columns = [
      ['Composicao', 'Composição'], ['Componente', 'Componente'], ['CodigoComponente', 'Código do componente'],
      ['QuantidadePorComposicao', 'Quantidade por composição'], ['Movimento', 'Movimento'], ['Serie', 'Série'],
      ['Item', 'Item'], ['Data', 'Data'], ['Fornecedor', 'Fornecedor'], ['QuantidadeDocumento', 'Quantidade do documento'],
      ['UnidadeDocumento', 'Unidade do documento'], ['QuantidadeEstoque', 'Quantidade de estoque'],
      ['Unidade', 'Unidade de estoque'], ['Situacao', 'Situação'], ['TipoDocumento', 'Tipo de documento'],
      ['ComposicoesPossiveis', 'Composições possíveis'],
    ];
    const lines = [columns.map(column => csvCell(column[1])).join(';')];
    for (const row of state.result.rows) lines.push(columns.map(([key]) => csvCell(row[key])).join(';'));
    const blob = new Blob(['\ufeff' + lines.join('\r\n')], {type: 'text/csv;charset=utf-8'});
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = `compras-composicao-${state.applied.start_date}-a-${state.applied.end_date}.csv`;
    document.body.append(link);
    link.click();
    link.remove();
    URL.revokeObjectURL(url);
  }

  document.addEventListener('DOMContentLoaded', async () => {
    setDates();
    $('compositionForm').addEventListener('submit', run);
    $('company').addEventListener('change', async () => {
      try { localStorage.setItem('insight.composition.company', $('company').value); } catch (_) { /* Storage may be blocked. */ }
      error('');
      notice('');
      await loadOptions();
    });
    $('exportCsv').addEventListener('click', exportCsv);
    try {
      await loadCompanies();
    } catch (err) {
      if (err.name !== 'AbortError') error(err.message);
      $('filterStatus').textContent = 'Não foi possível carregar as empresas.';
    }
  });
})();

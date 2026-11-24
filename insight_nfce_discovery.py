"""Bounded, read-only discovery of native NFC-e shapes from the selected database.

Only observed company/customer/product/fiscal shapes are accepted. No fuzzy IDs,
no copying a sale's finance/cash/TEF history, and no database writes here.
"""
from copy import deepcopy
from datetime import datetime, timezone
from decimal import Decimal, ROUND_HALF_UP
import hashlib
import re
from time import monotonic
from xml.etree import ElementTree as ET
from collections import OrderedDict

from bson import BSON, Int64, ObjectId, json_util
from insight_nfce import NS, NOTICE, digits, parse_xml
from insight_nfce_mapping import discriminator, tax_values, validate_native_documents

MAX_SAMPLES = 100
MAX_ITEMS = 2000
MAX_BYTES = 12 * 1024 * 1024


def movement_type(model):
    return {'65': 'NotaFiscalConsumidorEletronica', '55': 'NotaFiscalEletronicaSaida'}[model]


def read_xml(raw):
    if not raw:
        raise ValueError('XML vazio.')
    try: text = raw.decode('utf-8-sig')
    except UnicodeDecodeError: raise ValueError('Use XML em UTF-8.') from None
    if re.search(r'<!\s*(DOCTYPE|ENTITY)', text, re.I):
        raise ValueError('DTD e entidades não são permitidos.')
    try: return ET.fromstring(text)
    except ET.ParseError: raise ValueError('XML inválido.') from None


def prepare_files(files):
    """Prefer nfeProc over the identical standalone signed NFe; never invent a protocol."""
    complete, signed, errors = {}, [], []
    for name, raw in files:
        try:
            root = read_xml(raw)
            if root.tag == '{' + NS['n'] + '}NFe':
                info = root.find('n:infNFe', NS)
                if info is None: raise ValueError('NFe sem infNFe.')
                signed.append((name, info.get('Id'), ET.tostring(root)))
                continue
            node = root.find('n:NFe', NS)
            info = root.find('n:NFe/n:infNFe', NS)
            if info is None or node is None:
                raise ValueError('Selecione NFC-e nfeProc autorizado; eventos não são vendas.')
            parsed = parse_xml(raw, issuer=info.findtext('n:emit/n:CNPJ', namespaces=NS) or '',
                               environment=info.findtext('n:ide/n:tpAmb', namespaces=NS) or '')
            key = info.get('Id')
            if key in complete:
                if complete[key]['parsed']['xml_sha256'] != parsed['xml_sha256']:
                    raise ValueError('Chave repetida com XMLs autorizados diferentes.')
                errors.append({'file': name, 'status': 'duplicate', 'message': 'XML autorizado repetido no lote.'})
                continue
            complete[key] = {'name': name, 'raw': raw, 'info': info, 'parsed': parsed,
                             'signed': ET.tostring(node)}
        except ValueError as exc:
            errors.append({'file': name, 'status': 'invalid', 'message': str(exc)})
    for name, key, signed_node in signed:
        match = complete.get(key)
        same = match is not None and tree_signature(read_xml(signed_node)) == tree_signature(read_xml(match['signed']))
        errors.append({'file': name, 'status': 'duplicate' if same else 'invalid',
                       'message': 'NFe sem protocolo já representada pelo nfeProc autorizado.' if same else
                       'NFe sem protocolo: selecione também o nfeProc autorizado correspondente.'})
    return list(complete.values()), errors


def tree_signature(node, ignore=()):
    return (node.tag.split('}')[-1], (node.text or '').strip(), tuple(sorted(node.attrib.items())),
            tuple(tree_signature(child, ignore) for child in node if child.tag.split('}')[-1] not in ignore))


def get(info, path, default=''):
    return info.findtext('/'.join('n:' + p for p in path.split('/')), default=default, namespaces=NS)


def fiscal_signature(det):
    taxes = det.find('n:imposto', NS)
    if taxes is None: raise ValueError('Item sem imposto; descoberta fiscal indisponível.')
    # Nonzero tax calculation requires an explicit converter, not a borrowed old base.
    if any(path != 'imposto/vTotTrib' for path in tax_values(taxes)):
        raise ValueError('Tributos com valores/alíquotas positivos exigem mapeamento fiscal específico; não serão copiados de outra venda.')
    return (get(det, 'prod/CFOP'), get(det, 'prod/NCM'), get(det, 'prod/uCom'),
            tree_signature(taxes, ignore={'vTotTrib'}))


def bounded(collection, query, projection=None, *, limit=MAX_SAMPLES, sort=None):
    cursor = collection.find(query, projection).limit(limit).batch_size(5).max_time_ms(5000)
    if sort: cursor = cursor.sort(*sort)
    rows, size = [], 0
    try:
        for row in cursor:
            size += len(BSON.encode(row))
            # A working window controls memory, never the user's import size.
            if size > MAX_BYTES and rows: break
            rows.append(row)
    finally: cursor.close()
    return rows


def pick(source, fields):
    return {field: deepcopy(source[field]) for field in fields if field in source}


def customer_matches(person, recipient):
    document = digits(str(person.get('Documento','')))
    if recipient: return document == recipient
    kind = person.get('_t', '')
    if isinstance(kind, list): kind = kind[-1] if kind else ''
    return not document.strip('0') and ('Consumidor' in kind or person.get('PessoaReferencia') == ObjectId('000000000000000000000000'))


def guard_digest(document, fields):
    content = json_util.dumps(pick(document,fields),sort_keys=True,json_options=json_util.CANONICAL_JSON_OPTIONS)
    return hashlib.sha256(content.encode('utf-8')).hexdigest()


def make_guard(collection, document, fields):
    fields = sorted(set(fields) | {'_id'})
    return {'collection':collection,'id':document['_id'],'fields':fields,
            'digest':guard_digest(document,fields),'version':document.get('Versao')}


class Discovery:
    def __init__(self, db):
        self.db = db
        self.cache = OrderedDict()
        self.failures = OrderedDict()
        self.company_guards = {}
        self.xml_guards = {}
        self.xml_types = {}
        self.started = monotonic()
        self.notes = []

    def check_time(self):
        # Each database query has its own timeout; there is no job-wide deadline.
        pass

    def company(self, cnpj, model='65'):
        # Keep independent samples and failures for each fiscal model.
        cache_key = cnpj + ':' + model
        self.check_time()
        if cache_key in self.cache:
            self.cache.move_to_end(cache_key);return self.cache[cache_key]
        if cache_key in self.failures: raise ValueError(self.failures[cache_key])
        try: return self._company(cnpj, model)
        except ValueError as exc:
            self.failures[cache_key] = str(exc)
            if len(self.failures)>512:self.failures.popitem(last=False)
            raise

    def _company(self, cnpj, model):
        formatted = f'{cnpj[:2]}.{cnpj[2:5]}.{cnpj[5:8]}/{cnpj[8:12]}-{cnpj[12:]}'
        companies = bounded(self.db.Pessoas, {'Cnpj': {'$in': [cnpj, formatted]}, 'Ativo': True},
                            {'_id':1,'Cnpj':1,'Ativo':1,'Versao':1}, limit=3)
        if len(companies) != 1:
            raise ValueError('Emitente do XML não encontrado na base selecionada.' if not companies else
                             'Mais de uma empresa com o CNPJ do emitente; vínculo ambíguo.')
        company_id = companies[0]['_id']
        self.company_guards[cnpj] = make_guard('Pessoas',companies[0],('Cnpj','Ativo','Versao'))
        samples = bounded(self.db.Movimentacoes,
            {'EmpresaReferencia': company_id, '_t': movement_type(model), 'Situacao.Codigo': 2},
            sort=('DataHoraEmissao', -1))
        if not samples:
            raise ValueError('Empresa localizada, mas não há NFC-e concluída para identificar o formato nativo. Nenhum formato será presumido.')
        keys = [row['ChaveAcesso'] for row in samples if row.get('ChaveAcesso')]
        xml_rows = bounded(self.db.XmlMovimentacoes,
                          {'ChaveAcesso': {'$in': keys}, 'EventoCancelamento': False},
                          {'ChaveAcesso': 1, 'Xml': 1, 'TipoMovimentacao': 1}, limit=MAX_SAMPLES + 1)
        xmls = {}
        for row in xml_rows:
            raw = row.get('Xml')
            if not isinstance(raw, str): continue
            try:
                root = read_xml(raw.encode('utf-8'))
                info = root.find('n:NFe/n:infNFe', NS)
                if info is not None: xmls.setdefault(row['ChaveAcesso'], []).append((info,row))
            except ValueError: continue
        paired = []
        for sample in samples:
            candidates = xmls.get(sample.get('ChaveAcesso'), [])
            if len(candidates) != 1: continue
            info, xml_record = candidates[0]
            if digits(get(info, 'emit/CNPJ')) != cnpj or get(info, 'ide/mod') != model: continue
            paired.append((sample, info))
            self.xml_guards[sample['_id']] = make_guard('XmlMovimentacoes',xml_record,('ChaveAcesso','Xml','TipoMovimentacao'))
            if model == '55' and type(xml_record.get('TipoMovimentacao')) is not int:
                paired.pop();continue
            self.xml_types[sample['_id']] = xml_record.get('TipoMovimentacao', 11)
        if not paired:
            raise ValueError('Não há pares únicos de NFC-e e XML na amostra para conferir os vínculos fiscais.')
        self.cache[cnpj + ':' + model] = (company_id, paired)
        while len(self.cache)>4:
            old,(_,old_pairs)=self.cache.popitem(last=False)
            for sample,_ in old_pairs:self.xml_guards.pop(sample['_id'],None)
            for sample,_ in old_pairs:self.xml_types.pop(sample['_id'],None)
        self.notes.append({'company_id': str(company_id), 'sampled_movements': len(samples),
                           'paired_xmls': len(paired)})
        return self.cache[cnpj + ':' + model]

    def build(self, incoming):
        self.check_time()
        parsed, info = incoming['parsed'], incoming['info']
        company_id, paired = self.company(parsed['issuer_cnpj'], parsed['model'])
        recipient = digits(get(info, 'dest/CNPJ') or get(info, 'dest/CPF'))
        options = [(doc, xml) for doc, xml in paired if digits(get(xml, 'dest/CNPJ') or get(xml, 'dest/CPF')) == recipient
                   and get(xml, 'ide/tpAmb') == parsed['environment']
                   and get(xml, 'ide/finNFe', '1') == get(info, 'ide/finNFe', '1')
                   and get(xml, 'ide/tpNF', '1') == get(info, 'ide/tpNF', '1')
                   and get(xml, 'ide/natOp') == get(info, 'ide/natOp')
                   and customer_matches(doc.get('Pessoa',{}),recipient)]
        if not options:
            raise ValueError('Destinatário/ambiente sem exemplo compatível nesta empresa. Não será usado o cliente de outra venda.')
        template, _ = options[0]
        used_templates = {template['_id']:template}
        if digits(str(template.get('Empresa', {}).get('Documento', ''))) != parsed['issuer_cnpj']:
            raise ValueError('Histórico de empresa do exemplo diverge do CNPJ do XML.')
        if recipient and digits(str(template.get('Pessoa', {}).get('Documento', ''))) != recipient:
            raise ValueError('Histórico do destinatário diverge do XML.')
        key = parsed['access_key']
        identity = parsed['environment'] + ':' + key
        ident = ObjectId(hashlib.sha256(('insight-nfce:' + identity).encode()).digest()[:12])
        wall = datetime.fromisoformat(parsed['issued_at_original'].replace('Z', '+00:00')).replace(tzinfo=timezone.utc)
        static_fields = ('_t', 'Empresa', 'Modelo', 'FinalidadeEmissaoNfe', 'NaturezaOperacao',
                         'OrientacaoDanfe', 'IndicadorOperacaoConsumidorFinal', 'SequenciaMovimentacaoReferencia')
        doc = pick(template, static_fields)
        if not discriminator(doc, movement_type(parsed['model'])):
            raise ValueError('Discriminador NFC-e nativo não reconhecido.')
        if get(info, 'ide/finNFe', '1') != '1' or get(info, 'ide/tpNF', '1') != '1':
            raise ValueError('Somente NFC-e de saída com finalidade normal é suportada.')
        doc.update(_id=ident, Ativo=True, Numero=Int64(parsed['number']), Serie=parsed['series'],
            EmpresaReferencia=company_id, Pessoa=deepcopy(template['Pessoa']), DataHoraEmissao=wall,
            DataHoraEntradaSaida=wall, ChaveAcesso=key, PrimeiraChaveAcesso=key,
            ImportouXml=True, MovimentacaoItemFinanceiro=False, Versao='00:00:00',
            OrigemMobile=False, OrigemPedirMenu=False, Convertida=False, SequenciaEvento=0,
            NomeUsuario='DigiSat Insight XML', Observacao=get(info,'infAdic/infCpl'),
            ObservacaoDocumentoImportado='Importação histórica pelo DigiSat Insight',
            MovimentacoesReferenciadas=[], DocumentosFiscaisReferenciados=[], Autorizacoes=[],
            UtilizouCashback=False, ValorCashbackUtilizado=0.0, ItensBase=[], ItensPagamentos=[])
        for field, path in [('ModalidadeFrete','transp/modFrete'), ('IndicadorPresencaComprador','ide/indPres')]:
            code = get(info, path)
            choices = [d for d,x in options if field in d and get(x,path) == code]
            if not choices: raise ValueError('Sem exemplo nativo compatível: ' + field)
            doc[field] = deepcopy(choices[0][field])
            used_templates[choices[0]['_id']] = choices[0]
        status = template.get('Situacao', {})
        if not discriminator(status, 'Concluido') or status.get('Codigo') != 2:
            raise ValueError('Situação nativa de conclusão não reconhecida.')
        doc['Situacao'] = deepcopy(status)
        doc['Historicos'] = [{'SituacaoMovimentacao': deepcopy(status), 'NomeUsuario': 'DigiSat Insight XML',
                             'Observacao': 'Importação de XML autorizado; protocolo ' + parsed['authorization_protocol'],
                             'DataHora': datetime.now(timezone.utc), 'ChaveAcessoCorreta': key}]
        guard_fields = ('_t','EmpresaReferencia','Empresa','Pessoa','ItensBase','ItensPagamentos','Situacao',
                        'Modelo','FinalidadeEmissaoNfe','NaturezaOperacao','OrientacaoDanfe',
                        'ModalidadeFrete','IndicadorPresencaComprador','PagamentoRecebimento','Versao')
        guards = [self.company_guards[parsed['issuer_cnpj']],
                  make_guard('Movimentacoes',template,guard_fields),self.xml_guards[template['_id']]]
        for line, det in zip(parsed['items'], info.findall('n:det',NS)):
            self.check_time()
            if not line['included_in_total']: raise ValueError('Itens indTot=0 não são suportados na descoberta automática.')
            signature = fiscal_signature(det)
            candidates = []
            for source, source_xml in paired:
                for source_det in source_xml.findall('n:det',NS):
                    if get(source_det,'prod/cProd') != line['code']: continue
                    try:
                        if fiscal_signature(source_det) != signature: continue
                    except ValueError: continue
                    # Match the source XML line to its native product, not just an array position.
                    for candidate in source.get('ItensBase',[]):
                        product = candidate.get('ProdutoServico',{})
                        if candidate.get('Cancelado') or str(product.get('CodigoInterno')) != line['code']: continue
                        if Decimal(str(candidate.get('Quantidade',0))) != Decimal(get(source_det,'prod/qCom','0')): continue
                        if Decimal(str(candidate.get('PrecoUnitario',0))) != Decimal(get(source_det,'prod/vUnCom','0')): continue
                        candidates.append((candidate, source))
            refs = {c.get('ProdutoServicoEmpresaReferencia') for c,s in candidates}
            if len(refs) != 1 or not candidates:
                raise ValueError('Produto sem vínculo fiscal único na amostra: ' + line['code'])
            fiscal_shapes = {BSON.encode(pick(c,('_t','Percentual','IPI','PIS','COFINS','OperacaoFiscal'))) for c,s in candidates}
            if len(fiscal_shapes) != 1:
                raise ValueError('Mais de um formato fiscal interno para o produto; revise o vínculo: ' + line['code'])
            item_source, source = candidates[0]
            link_id = item_source['ProdutoServicoEmpresaReferencia']
            link = self.db.ProdutosServicosEmpresa.find_one({'_id':link_id, 'EmpresaReferencia':company_id}, max_time_ms=5000)
            product_id = item_source['ProdutoServico'].get('ProdutoServicoReferencia')
            if not link or link.get('ProdutoServicoReferencia') != product_id or link.get('Ativo') is False:
                raise ValueError('Vínculo do produto removido/inativo ou de outra empresa.')
            current_product = self.db.ProdutosServicos.find_one({'_id':product_id, 'Ativo':True},
                {'_id':1,'Versao':1,'CodigoInterno':1,'Ativo':1},max_time_ms=5000)
            if current_product is None:
                raise ValueError('Produto ausente ou inativo no cadastro atual.')
            if str(current_product.get('CodigoInterno')) != line['code']:
                raise ValueError('Código atual do produto diverge do XML; confirme o vínculo manualmente.')
            item = pick(item_source, ('_t','ProdutoServicoEmpresaReferencia','ProdutoServico','NCMNBS',
                'OperacaoFiscal','Percentual','IPI','PIS','COFINS','DecimaisQuantidade','DecimaisPrecoUnitario',
                'CodigoEspecificadorSubstituicaoTributaria','IndicadorEscalaRelevante',
                'BloquearAlteracaoOperacaoFiscal','FormulaDifal','FormulaCreditoPresumido'))
            if any(not isinstance(item.get(field),dict) or not item[field].get('_t')
                   for field in ('ProdutoServico','Percentual','IPI','PIS','COFINS','OperacaoFiscal')):
                raise ValueError('Exemplo nativo não contém todos os objetos fiscais tipados do item.')
            if Decimal(str(item['Percentual'].get('ValorSt',0))) != 0 or Decimal(str(item['IPI'].get('Valor',0))) != 0:
                raise ValueError('Valores do exemplo nativo divergem dos tributos zerados do XML.')
            stock = item_source.get('EstoqueMovimentacao',{})
            if stock.get('Itens') or stock.get('Quantidades'):
                raise ValueError('Produto com lote/grade/serial exige vínculo de estoque explícito; dados de outra venda não serão copiados.')
            item['EstoqueMovimentacao'] = {'_t':deepcopy(stock.get('_t')),
                '_id':ObjectId(hashlib.sha256((identity+':item:'+str(line['sequence'])).encode()).digest()[:12]),
                'Quantidade':float(line['quantity']), 'QuantidadeParaMontarComposicao':0.0,'Quantidades':[],'Itens':[]}
            item.update(Numero=line['sequence'], Quantidade=float(line['quantity']), PrecoUnitario=float(line['unit_price']),
                DescontoDigitado=float(line['discount']), DescontoProporcional=0.0, OutrasDespesasProporcional=0.0,
                OutrasDespesasDigitado=float(get(det,'prod/vOutro','0')), Frete=float(get(det,'prod/vFrete','0')),
                Seguro=float(get(det,'prod/vSeg','0')), Cancelado=False, DataHora=wall,
                VendedorReferencia=ObjectId('000000000000000000000000'), TecnicoReferencia=ObjectId('000000000000000000000000'),
                Observacao=get(det,'infAdProd'), ObservacaoDigitada='', NumeroPedidoCompra='', NumeroItemPedidoCompra='')
            if not item['EstoqueMovimentacao']['_t']: raise ValueError('Tipo de estoque não reconhecido no exemplo nativo.')
            doc['ItensBase'].append(item)
            guards.extend((make_guard('Movimentacoes',source,guard_fields),self.xml_guards[source['_id']],
                make_guard('ProdutosServicosEmpresa',link,('EmpresaReferencia','ProdutoServicoReferencia','Ativo','Versao')),
                make_guard('ProdutosServicos',current_product,('Ativo','CodigoInterno','Versao'))))
        for payment_source in self.payments(doc, info, paired):
            used_templates[payment_source['_id']] = payment_source
        total = sum(((Decimal(str(i['Quantidade']))*Decimal(str(i['PrecoUnitario']))).quantize(Decimal('.01'),rounding=ROUND_HALF_UP)
            - Decimal(str(i['DescontoDigitado'])) + Decimal(str(i['OutrasDespesasDigitado']))
            + Decimal(str(i['Frete'])) + Decimal(str(i['Seguro'])) for i in doc['ItensBase']), Decimal(0))
        if total != Decimal(parsed['total']): raise ValueError('Total nativo reconstruído diverge de vNF.')
        xml = {'_id':ObjectId(hashlib.sha256(('insight-nfce-xml:'+identity).encode()).digest()[:12]),
               'Data':wall,'ChaveAcesso':key,'Xml':incoming['raw'].decode('utf-8-sig'),
               'TipoMovimentacao':self.xml_types[template['_id']],
               'EventoCancelamento':False,'Cancelada':False}
        validate_native_documents(doc, xml)
        for source in used_templates.values():
            guards.append(make_guard('Movimentacoes',source,guard_fields))
            guards.append(self.xml_guards[source['_id']])
        guards = list({(g['collection'],g['id']):g for g in guards}.values())
        return {'movement':doc,'xml':xml,'guards':guards,
                'summary':{k:parsed[k] for k in ('access_key','number','series','total','issued_at_original','xml_sha256')},
                'notice':NOTICE + ' Vendedor não informado. Tributos aproximados permanecem no XML; a exibição nativa pode recalculá-los pelo cadastro.'}

    def payments(self, document, info, paired):
        used = []
        change = Decimal(get(info,'pag/vTroco','0'))
        if change != 0: raise ValueError('Troco exige definição da espécie responsável; descoberta automática interrompida.')
        for payment in info.findall('n:pag/n:detPag',NS):
            code = get(payment,'tPag')
            candidates = []
            for source, xml in paired:
                codes = [get(p,'tPag') for p in xml.findall('n:pag/n:detPag',NS)]
                native = source.get('ItensPagamentos',[])
                if len(codes) == 1 and codes[0] == code and len(native) == 1:
                    candidates.append((native[0],source))
            if not candidates: raise ValueError('Espécie de pagamento sem exemplo único: ' + code)
            allowed = {'_t','EspeciePagamento','Descricao','Valor','ValorDigitado','InformacoesTransacao'}
            if any(set(p) - allowed for p,s in candidates):
                raise ValueError('Espécie exige vínculos adicionais; seleção automática não é segura: ' + code)
            # TEF/NSU authorization is never borrowed from the source sale.
            item = pick(candidates[0][0], ('_t','EspeciePagamento','Descricao'))
            if any(pick(p,('_t','EspeciePagamento')) != pick(candidates[0][0],('_t','EspeciePagamento')) for p,s in candidates):
                raise ValueError('Mais de uma espécie interna para o pagamento XML: ' + code)
            value = Decimal(get(payment,'vPag','0'))
            item.update(Valor=float(value), ValorDigitado=float(value))
            if 'InformacoesTransacao' in candidates[0][0]:
                raise ValueError('Pagamento TEF exige dados completos da transação; o XML não reconstrói o histórico do terminal.')
            document['ItensPagamentos'].append(item)
            used.append(candidates[0][1])
        if not document['ItensPagamentos'] or sum((Decimal(str(p['Valor'])) for p in document['ItensPagamentos']),Decimal(0)) != Decimal(get(info,'total/ICMSTot/vNF')):
            raise ValueError('Pagamentos reconstruídos não conferem com o total da nota.')
        document['Troco'] = 0.0
        receipts = [s.get('PagamentoRecebimento',{}) for s in used]
        types = [r.get('_t') for r in receipts if r.get('_t')]
        if types: document['PagamentoRecebimento']={'_t':deepcopy(types[0]),'Parcelas':[]}
        return used

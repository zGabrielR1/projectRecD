using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DI.SimpleInjector;
using Akka.Util;
using DigisatServer.Application.Core.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Akka.Main.Actors;
using DigisatServer.Distributed.Boot;
using DigisatServer.Domain.Core;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Abastecimentos;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Configuracoes;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Mensagens;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Restaurantes;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Cardapios;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Fisicas;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Juridicas.Emitentes;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.PostosCombustiveis;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos.Pbm;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos.Produtos;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.TributacoesEstadual.Percentuais;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.TributacoesReformaTributaria.Percentuais;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.Itens;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.PessoasHistorico;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.ProdutosServicosHistorico;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.RegistrosPafEcf;
using DigisatServer.Domain.Main.Services.AuxiliaresSer.Abastecimentos;
using DigisatServer.Infrastructure.Crosscutting;
using DigisatServer.Infrastructure.Crosscutting.Concentrador.Akka;
using DigisatServer.Infrastructure.Crosscutting.Concentrador.Classes;
using DigisatServer.Infrastructure.Crosscutting.Concentrador.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Concentrador.Types;
using DigisatServer.Infrastructure.Crosscutting.DFe.CupomFiscalEletronico.Classes;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Habilitacao;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Logging;
using DigisatServer.Infrastructure.Crosscutting.Model.ProdutosServicos;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.Actor;
using DigisatServer.Infrastructure.Crosscutting.RegistrosPafEcf.Types;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.CadastrosVO.Municipios;
using DigisatServer.Infrastructure.Data.Core;
using DigisatServer.Infrastructure.Data.Core.Interfaces;
using MongoDB.Bson;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("DigisatServer.Distributed.Akka.Main")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("DigisatServer.Distributed.Akka.Main")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("b55a5ddf-fc32-4bcd-bb9c-292bc75059db")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace DigisatServer.Distributed.Akka.Main
{
	public class AkkaFactory : DServerActorSystemBase<IServiceProvider, SimpleInjectorDependencyResolver>
	{
		protected override IEnumerable<Type> ActorTypes => new List<Type>
		{
			typeof(WcfServerActor),
			typeof(HabilitacaoServerActor),
			typeof(SincronizacaoServerActor),
			typeof(PostoServerActorTestConnection),
			typeof(PostoServerActorWcf),
			typeof(MicroTerminalServerActor),
			typeof(ConsultaPrecoServerActor),
			typeof(ConsultaProdutoPbmServerActor),
			typeof(SatServerActorWcf),
			typeof(RestauranteServerActor),
			typeof(MensagemServerActor),
			typeof(PedidoIfoodServerActor)
		};

		public AkkaFactory()
			: base((IServiceProvider)Bootstrapper.Container)
		{
		}
	}
}
namespace DigisatServer.Distributed.Akka.Main.Actors
{
	public class ConsultaPrecoServerActor : ReceiveActor
	{
		public ConsultaPrecoServerActor(IRepositoryRead<Pessoa> pessoaRepositoryRead, IProdutoServicoEmpresaRepositoryRead produtoServicoEmpresaRepositoryRead)
		{
			ConsultaPrecoServerActor consultaPrecoServerActor = this;
			Receive<ConnectRequest>(delegate
			{
				Dictionary<string, string> empresas = pessoaRepositoryRead.Find(PessoaSpecifications.PorEmitente().PorAtivo()).ToDictionary((Pessoa empresa) => empresa.Id.ToString(), (Pessoa empresa) => $"{empresa.CpfCnpjFormatado} {empresa.Nome}");
				consultaPrecoServerActor.Sender.Tell(new ConnectEmpresaResponse(empresas), consultaPrecoServerActor.Self);
			});
			Receive(delegate(PesquisarProdutoRequest x)
			{
				ProdutoConsultaPreco produtoConsultaPreco = produtoServicoEmpresaRepositoryRead.ObterProdutoConsultaPreco(x.IdEmpresa, x.CodigoBarras);
				if (produtoConsultaPreco.IsNull())
				{
					consultaPrecoServerActor.Sender.Tell(new ProdutoNaoLocalizadoResponse());
				}
				else
				{
					consultaPrecoServerActor.Sender.Tell(new PesquisarProdutoResponse(produtoConsultaPreco.Descricao, produtoConsultaPreco.Preco, produtoConsultaPreco.Imagem));
				}
			});
		}
	}
	public class ConsultaProdutoPbmServerActor : ReceiveActor
	{
		public ConsultaProdutoPbmServerActor(IProdutoServicoEmpresaRepositoryRead produtoServicoEmpresaRepositoryRead)
		{
			ConsultaProdutoPbmServerActor consultaProdutoPbmServerActor = this;
			Receive(delegate(PesquisarProdutoVidaLinkRequest x)
			{
				ItemTabelaPrecoVidaLink itemTabelaPrecoVidaLink = produtoServicoEmpresaRepositoryRead.ObterPrecosVidaLink(x.IdEmpresa, x.CodigoBarras);
				if (itemTabelaPrecoVidaLink.IsNull())
				{
					consultaProdutoPbmServerActor.Sender.Tell(new CodigoBarrasNaoLocalizadoResponse(x.CodigoBarras));
				}
				else
				{
					consultaProdutoPbmServerActor.Sender.Tell(new PesquisarMedicamentoResponse(itemTabelaPrecoVidaLink.Obter()));
				}
			});
		}
	}
	public class HabilitacaoServerActor : ReceiveActor
	{
		public HabilitacaoServerActor(Habilitacao habilitacao)
		{
			HabilitacaoServerActor habilitacaoServerActor = this;
			Receive<HabilitacaoSatRequest>(delegate
			{
				HabilitacaoSatResponse habilitacaoSatResponse = new HabilitacaoSatResponse();
				if (habilitacao.DadosArquivoHabilitacao.Matriz.SatConfigurado)
				{
					habilitacaoSatResponse.DadosEmpresas.Add(habilitacaoServerActor.RetornarDadosEmpresaHabilitacaoSat(habilitacao.DadosArquivoHabilitacao.Matriz));
				}
				habilitacaoSatResponse.DadosEmpresas.AddRange(from a in habilitacao.DadosArquivoHabilitacao.Filiais
					where a.Empresa.SatConfigurado
					select a into x
					select habilitacaoServerActor.RetornarDadosEmpresaHabilitacaoSat(x.Empresa));
				habilitacaoServerActor.Sender.Tell(habilitacaoSatResponse, habilitacaoServerActor.Self);
			});
		}

		private DadosEmpresasHabilitacaoSat RetornarDadosEmpresaHabilitacaoSat(InformacaoEmpresa dadosEmpresa)
		{
			return DadosEmpresasHabilitacaoSat.Criar().ComCnpj(dadosEmpresa.Cnpj).ComAssinaturaSat(dadosEmpresa.AssinaturaDoAplicativoComercial)
				.ComSiglaUf(new MunicipioValueObjectRepository().PorCodigoIbge(dadosEmpresa.Endereco.CodigoIbgeMunicipio).Uf.Sigla)
				.Build();
		}
	}
	public class MensagemServerActor : ReceiveActor
	{
		private readonly IRepositoryBaseWrite<Mensagem> _mensagemRepositoryBaseWrite;

		private readonly IRepositoryRead<Mensagem> _mensagemRepositoryRead;

		private readonly ConcurrentDictionary<string, IActorRef> _clientes = new ConcurrentDictionary<string, IActorRef>();

		private readonly ConcurrentDictionary<string, NotificacaoMensagem> _mensagens;

		public MensagemServerActor(IRepositoryBaseWrite<Mensagem> mensagemRepositoryBaseWrite, IRepositoryRead<Mensagem> mensagemRepositoryRead, IEventPublisher eventPublisher)
		{
			_mensagemRepositoryRead = mensagemRepositoryRead;
			_mensagemRepositoryBaseWrite = mensagemRepositoryBaseWrite;
			List<Mensagem> list = mensagemRepositoryRead.Find(MensagemSpecifications.PorNaoAtendida()).ToListEmptyWhenNull();
			HashSet<ObjectId> repetidas = AtenderRepetidasHospedagemFNRH(list);
			Dictionary<string, NotificacaoMensagem> collection = list.Where((Mensagem mensagem) => !repetidas.Contains(mensagem.Id)).ToDictionary((Mensagem mensagem) => mensagem.Id.ToString(), (Mensagem mensagem) => mensagem.ObterNotificacaoMensagem());
			_mensagens = new ConcurrentDictionary<string, NotificacaoMensagem>(collection);
			eventPublisher.GetEvent<MensagemRequest>().Subscribe(ProcessarMensagemRequest);
			eventPublisher.GetEvent<NotificacaoAtendidaRequest>().Subscribe(ProcessarNotificacaoAtendida);
			Receive(delegate(ConnectUserRequest request)
			{
				LoggerFactory.CreateLog().LogInfo($"MensagemServerActor: cliente conectado {request.Id}");
				AdicionarCliente(request, base.Sender);
				List<NotificacaoMensagem> list2 = _mensagens.Values.Where((NotificacaoMensagem x) => x.TipoMensagemNotificacao != TipoMensagemNotificacao.Agendamento && x.IdEmpresa.Equals(request.IdEmpresa)).ToList();
				list2.AddRange(_mensagens.Values.Where((NotificacaoMensagem x) => x.TipoMensagemNotificacao == TipoMensagemNotificacao.Agendamento).ToList());
				base.Sender.Tell(new ConnectUserResponse(list2), base.Self);
			});
			Receive(delegate(DisconnectUserRequest request)
			{
				RemoverCliente(request.Id);
			});
			Receive(delegate(MensagemRequest request)
			{
				ProcessarMensagemRequest(request);
			});
			Receive(delegate(NotificacaoAtendidaRequest request)
			{
				ProcessarNotificacaoAtendida(request);
			});
		}

		private HashSet<ObjectId> AtenderRepetidasHospedagemFNRH(IEnumerable<Mensagem> naoAtendidas)
		{
			HashSet<ObjectId> hashSet = new HashSet<ObjectId>(from mensagem in (from mensagem in naoAtendidas
					where mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.HospedagemFNRH
					group mensagem by mensagem.Identificador).SelectMany((IGrouping<string, Mensagem> grupo) => grupo.OrderByDescending((Mensagem mensagem) => mensagem.DataHora).Skip(1))
				select mensagem.Id);
			if (!hashSet.Any())
			{
				return hashSet;
			}
			try
			{
				_mensagemRepositoryBaseWrite.AtualizarCampo(MensagemSpecifications.PorIds(hashSet), (Mensagem mensagem) => mensagem.DataHoraAtendimento, DateTime.Now);
				return hashSet;
			}
			catch (Exception ex)
			{
				LoggerFactory.CreateLog().LogError(ex.Message, ex);
				return new HashSet<ObjectId>();
			}
		}

		private void ProcessarMensagemRequest(MensagemRequest request)
		{
			if (!request.Mensagem.Persistir)
			{
				foreach (KeyValuePair<string, IActorRef> cliente in _clientes)
				{
					cliente.Value.Tell(new MensagemResponse(request.Mensagem));
				}
				return;
			}
			if (request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.Hospedagem && _mensagemRepositoryRead.Find(MensagemSpecifications.PorIdentificador(request.Mensagem.Identificador).PorNaoAtendida()).Any())
			{
				return;
			}
			if (request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.MonitorDocumentoFiscalEletronico || request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.MonitorSincronizacaoScanntech || request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.AutomacaoIMendes)
			{
				foreach (Mensagem item in _mensagemRepositoryRead.Find(MensagemSpecifications.PorTipoMensagemNotificacao(request.Mensagem.TipoMensagemNotificacao).PorNaoAtendida()))
				{
					ProcessarNotificacaoAtendida(new NotificacaoAtendidaRequest(item.Id.ToString(), item.NomeUsuario));
				}
			}
			if (request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.SaltoNumeracaoDocumentoFiscalEletronico || request.Mensagem.TipoMensagemNotificacao == TipoMensagemNotificacao.HospedagemFNRH)
			{
				foreach (Mensagem item2 in _mensagemRepositoryRead.Find(MensagemSpecifications.PorTipoMensagemNotificacao(request.Mensagem.TipoMensagemNotificacao).PorNaoAtendida().PorIdentificador(request.Mensagem.Identificador)))
				{
					ProcessarNotificacaoAtendida(new NotificacaoAtendidaRequest(item2.Id.ToString(), item2.NomeUsuario));
				}
			}
			Mensagem mensagem = Mensagem.Criar(request.Mensagem);
			_mensagemRepositoryBaseWrite.Add(mensagem);
			NotificacaoMensagem notificacaoMensagem = mensagem.ObterNotificacaoMensagem();
			_mensagens.TryAdd(notificacaoMensagem.Id, notificacaoMensagem);
			foreach (KeyValuePair<string, IActorRef> cliente2 in _clientes)
			{
				cliente2.Value.Tell(new MensagemResponse(notificacaoMensagem));
			}
		}

		private void ProcessarNotificacaoAtendida(NotificacaoAtendidaRequest request)
		{
			Mensagem mensagem = _mensagemRepositoryRead.FindOne(MensagemSpecifications.PorIdentificador(request.IdMensagem).PorNaoAtendida().OrPorId(request.IdMensagem));
			if (mensagem.IsNull())
			{
				return;
			}
			mensagem.SetarAtendido(request.NomeUsuario);
			_mensagemRepositoryBaseWrite.Update(mensagem);
			_mensagens.TryRemove(mensagem.Id.ToString(), out var value);
			if (value == null)
			{
				return;
			}
			foreach (KeyValuePair<string, IActorRef> cliente in _clientes)
			{
				cliente.Value.Tell(new NotificacaoAtendidaResponse(value.Id));
			}
		}

		private void AdicionarCliente(ConnectUserRequest connectUserRequest, IActorRef actorRef)
		{
			if (!string.IsNullOrEmpty(connectUserRequest.Id))
			{
				RemoverCliente(connectUserRequest.Id);
				_clientes.TryAdd(connectUserRequest.Id, actorRef);
			}
		}

		private void RemoverCliente(string nomeUsuario)
		{
			if (string.IsNullOrEmpty(nomeUsuario))
			{
				return;
			}
			try
			{
				_clientes.TryRemove(nomeUsuario, out var _);
			}
			catch (Exception)
			{
			}
		}
	}
	public class MicroTerminalServerActor : ReceiveActor
	{
		private readonly IRepositoryRead<Pessoa> _pessoaRepositoryRead;

		private readonly ICardapioRepositoryRead _cardapioRepositoryRead;

		private readonly IProdutoServicoEmpresaRepositoryRead _produtoServicoEmpresaRepositoryRead;

		private readonly IRepositoryBaseWrite<ItemMesaConta> _itemMesaContaRepositoryWrite;

		private readonly IRegistroPafEcfRepositoryWrite _registroPafEcfRepositoryWrite;

		private readonly IConfiguracaoRepositoryRead _configuracaoRepositoryRead;

		private readonly IMovimentacaoRepositoryWrite _movimentacaoRepositoryWrite;

		private readonly IMovimentacaoRepositoryRead _movimentacaoRepositoryRead;

		private readonly IRepositoryRead<MesaContaClienteBloqueada> _mesaContaBloqueadaRepositoryRead;

		public MicroTerminalServerActor(IRepositoryRead<Pessoa> pessoaRepositoryRead, ICardapioRepositoryRead cardapioRepositoryRead, IProdutoServicoEmpresaRepositoryRead produtoServicoEmpresaRepositoryRead, IRepositoryBaseWrite<ItemMesaConta> itemMesaContaRepositoryWrite, IRegistroPafEcfRepositoryWrite registroPafEcfRepositoryWrite, IConfiguracaoRepositoryRead configuracaoRepositoryRead, IMovimentacaoRepositoryWrite movimentacaoRepositoryWrite, IMovimentacaoRepositoryRead movimentacaoRepositoryRead, IRepositoryRead<MesaContaClienteBloqueada> mesaContaBloqueadaRepositoryRead)
		{
			_pessoaRepositoryRead = pessoaRepositoryRead;
			_cardapioRepositoryRead = cardapioRepositoryRead;
			_produtoServicoEmpresaRepositoryRead = produtoServicoEmpresaRepositoryRead;
			_itemMesaContaRepositoryWrite = itemMesaContaRepositoryWrite;
			_registroPafEcfRepositoryWrite = registroPafEcfRepositoryWrite;
			_configuracaoRepositoryRead = configuracaoRepositoryRead;
			_movimentacaoRepositoryWrite = movimentacaoRepositoryWrite;
			_movimentacaoRepositoryRead = movimentacaoRepositoryRead;
			_mesaContaBloqueadaRepositoryRead = mesaContaBloqueadaRepositoryRead;
			Create();
		}

		public void Create()
		{
			Receive<ConnectMicroTerminalRequest>(delegate
			{
				Dictionary<string, string> empresas = _pessoaRepositoryRead.Find(PessoaSpecifications.PorEmitente().PorAtivo()).ToDictionary((Pessoa empresa) => empresa.Id.ToString(), (Pessoa empresa) => $"{empresa.CpfCnpjFormatado} {empresa.Nome}");
				base.Sender.Tell(new ConnectMicroTerminalResponse(empresas), base.Self);
			});
			Receive(delegate(ObterProdutosMicroTerminalRequest x)
			{
				Pessoa byId = _pessoaRepositoryRead.GetById(ObjectId.Parse(x.IdEmpresa));
				List<ProdutoMicroTerminal> list = new List<ProdutoMicroTerminal>();
				Configuracao configuracao = _configuracaoRepositoryRead.Get(x.IdEmpresa);
				if (byId.To<Emitente>().RamoAtividade.Tipo == RamoDeAtividade.Restaurante)
				{
					foreach (Cardapio cardapio in _cardapioRepositoryRead.ObterParaRestaurante(x.IdEmpresa, TimeSpan.Zero))
					{
						list.AddRange(cardapio.ItensCardapio.Select((ItemCardapio itemCardapio) => new ProdutoMicroTerminal(itemCardapio.ProdutoServicoEmpresa.Id.ToString(), itemCardapio.ProdutoServicoEmpresa.ProdutoServico.CodigoInterno, itemCardapio.ProdutoServicoEmpresa.ProdutoServico.To<Produto>().CodigoBarras, itemCardapio.DescricaoTela, itemCardapio.ObterImpressora(cardapio.Impressora)?.Caminho, itemCardapio.ProdutoServicoEmpresa.ProdutoServico.To<Produto>().Pesavel, itemCardapio.ObterImpressora(cardapio.Impressora)?.AvancoPapel ?? 0, itemCardapio.ObterImpressora(cardapio.Impressora)?.Id.ToString(), itemCardapio.ProdutoServicoEmpresa.ProdutoServico.UnidadeMedida.Sigla, itemCardapio.ProdutoServicoEmpresa.ProdutoServico.UnidadeMedida.IsUnidade, itemCardapio.ProdutoServicoEmpresa.SetarTabelaPreco(configuracao.Saida.TabelaPrecoPadrao.ReferenciaObjectId).Preco.PrecoVendaVigente, itemCardapio.ProdutoServicoEmpresa.To<ProdutoEmpresa>()?.Tara ?? 0m)));
					}
				}
				else
				{
					IEnumerable<ProdutoServicoEmpresa> source = _produtoServicoEmpresaRepositoryRead.Find(EntityBaseEmpresaSpecification<ProdutoServicoEmpresa>.PorEmpresa(x.IdEmpresa).AndSpecification(ProdutoServicoEmpresaSpecifications.PorAtivo()).PorTipoProduto());
					list.AddRange(from i in source
						where i.Vendavel
						select i into item
						select new ProdutoMicroTerminal(item.Id.ToString(), item.ProdutoServico.CodigoInterno, item.ProdutoServico.To<Produto>().CodigoBarras, item.ProdutoServico.Descricao.RemoverCaracteresNaoImprimiveis(), item.Impressora?.Caminho, item.ProdutoServico.To<Produto>().Pesavel, item.Impressora?.AvancoPapel ?? 0, item.Impressora?.Id.ToString(), item.ProdutoServico.UnidadeMedida.Sigla, item.ProdutoServico.UnidadeMedida.IsUnidade, item.SetarTabelaPreco(configuracao.Saida.TabelaPrecoPadrao.ReferenciaObjectId).Preco.PrecoVendaVigente, item.ProdutoServico.To<ProdutoEmpresa>()?.Tara ?? 0m));
				}
				if (!list.Any())
				{
					string mensagem = ((byId.To<Emitente>().RamoAtividade.Tipo == RamoDeAtividade.Restaurante) ? "Nenhum cardápio localizado. Verifique o cadastro dos cardápios!" : "Nenhum produto localizado!");
					base.Sender.Tell(new ProdutosNaoCadastradosResponse(mensagem), base.Self);
				}
				else
				{
					IEnumerable<MesaContaClienteBloqueadaMicroTerminal> mesasContasBloqueadas = from b in _mesaContaBloqueadaRepositoryRead.Find(EntityEmpresaSpecification<MesaContaClienteBloqueada>.AtivoPorEmpresa(x.IdEmpresa))
						select new MesaContaClienteBloqueadaMicroTerminal(b.Numero);
					IEnumerable<VendedorGarcom> vendedores = from pessoa in _pessoaRepositoryRead.Find(PessoaSpecifications.AtivoPorVendedor()).OfType<Fisica>()
						select new VendedorGarcom(pessoa.Id.ToString(), pessoa.Nome, pessoa.Vendedor.CodigoInterno, pessoa.Vendedor.PercentualComissao);
					base.Sender.Tell(new ObterProdutosMicroTerminalResponse(list, vendedores, mesasContasBloqueadas, configuracao.Saida.NumeroMaximoMesasContasCliente.Valor), base.Self);
				}
			});
			ReceiveAsync(async delegate(SalvarPedidoRequest message)
			{
				Pessoa empresa2 = _pessoaRepositoryRead.GetById(ObjectId.Parse(message.IdEmpresa));
				if (empresa2.To<Emitente>().RamoAtividade.Tipo == RamoDeAtividade.Restaurante)
				{
					await ProcessarItensMesaConta(message);
				}
				else
				{
					await ProcessarPreVenda(message, empresa2);
				}
			});
		}

		private async Task ProcessarItensMesaConta(SalvarPedidoRequest message)
		{
			IEnumerable<ProdutoServicoEmpresa> produtos = default(IEnumerable<ProdutoServicoEmpresa>);
			_ = produtos;
			produtos = await _produtoServicoEmpresaRepositoryRead.AggregateIn(message.Pedido.Itens.Select((ProdutoMicroTerminalQuantidade item) => ObjectId.Parse(item.Produto.Id)));
			decimal percentualGarcom = message.Pedido.VendedorGarcom?.PercentualComissao ?? 0m;
			List<ItemMesaConta> itens = message.Pedido.Itens.Select((ProdutoMicroTerminalQuantidade x) => ItemMesaConta.Criar().ComDescricao(x.Produto.Descricao).ComCodigo(x.Produto.CodigoInterno)
				.ComDataHora(DateTime.Now)
				.ComEmpresaReferencia(message.IdEmpresa)
				.ComGarcomReferencia(message.Pedido.VendedorGarcom?.Id)
				.ComPercentualComissao(ObterPercentualComissao(produtos, x.Produto.Id, percentualGarcom))
				.ComImpresso(impresso: true)
				.ComImpressoraReferencia(x.Produto.ImpressoraReferencia)
				.ComNumeroMesaConta(message.Pedido.Numero)
				.ComProdutoServicoEmpresaReferencia(x.Produto.Id)
				.ComQuantidade(x.Quantidade)
				.ComSituacaoMesaConta(SituacaoMesaConta.Aberta)
				.ComValorUnitario(x.Produto.Preco)
				.ComUnidade(x.Produto.Unidade)
				.ComGeradoItemPedido(geradoItemPedido: false)
				.Build()).ToListEmptyWhenNull();
			await _itemMesaContaRepositoryWrite.UpdateAsync(itens);
			await _registroPafEcfRepositoryWrite.SalvarRegistrosS(itens);
		}

		private static decimal ObterPercentualComissao(IEnumerable<ProdutoServicoEmpresa> produtos, string produtoReferencia, decimal percentualGarcom)
		{
			decimal num = produtos.ToListEmptyWhenNull().FirstOrDefault((ProdutoServicoEmpresa x) => x.Id.ToString().Equals(produtoReferencia))?.PercentualComissao ?? 0m;
			if (!(num > 0m))
			{
				return percentualGarcom;
			}
			return num;
		}

		private async Task ProcessarPreVenda(SalvarPedidoRequest message, Pessoa empresa)
		{
			PessoaHistorico clienteConsumidor = _pessoaRepositoryRead.GetById(Consumidor.Criar().Id).ObterPessoaHistorico();
			PessoaHistorico empresaHistorico = empresa.ObterPessoaHistorico();
			PessoaHistorico vendedorHistorico = ((message.Pedido.VendedorGarcom != null) ? _pessoaRepositoryRead.GetById(ObjectId.Parse(message.Pedido.VendedorGarcom.Id)).ObterPessoaHistorico() : null);
			IEnumerable<ProdutoServicoEmpresa> produtos = default(IEnumerable<ProdutoServicoEmpresa>);
			_ = produtos;
			produtos = await _produtoServicoEmpresaRepositoryRead.AggregateIn(message.Pedido.Itens.Select((ProdutoMicroTerminalQuantidade item) => ObjectId.Parse(item.Produto.Id)));
			List<IItem> list = message.Pedido.Itens.Select(delegate(ProdutoMicroTerminalQuantidade item)
			{
				ProdutoServicoEmpresa produtoServicoEmpresa = produtos.FirstOrDefault((ProdutoServicoEmpresa p) => p.Id.ToString().Equals(item.Produto.Id));
				IProdutoServicoProxy produtoServicoProxy = produtoServicoEmpresa.ProdutoServico.ProdutoServicoProxy;
				Percentual percentual = produtoServicoEmpresa.To<ProdutoEmpresa>().TributacaoEstadual.ObterPercentualNaoContribuinte();
				PercentualReformaTributaria percentualReformaTributaria = produtoServicoEmpresa.To<ProdutoEmpresa>().TributacaoIbsCbs.ObterPercentualNaoContribuinte();
				string codigoBeneficioFiscalUf = ((!percentual.EnviarCodigoBeneficioFiscal) ? null : ((!string.IsNullOrEmpty(percentual.CodigoBeneficioFiscalUf)) ? percentual.CodigoBeneficioFiscalUf.ToStringEmptyWhenNull() : produtoServicoEmpresa.To<ProdutoEmpresa>()?.CodigoBeneficioFiscalUf.ToStringEmptyWhenNull()));
				return FluentFactory<ItemDocumentoAuxiliarFactory>.Init().SetarCalculadoPeloSistema(calculadoPeloSistema: true).SetarMovimentoEntrada(movimentoEntrada: false)
					.SetarProdutoServicoEmpresaReferencia(produtoServicoEmpresa.Id)
					.SetarProdutoServico(FluentFactory<ProdutoServicoHistoricoFactory>.Init().SetarTipoProdutoServico(produtoServicoProxy.TipoProdutoServico).SetarProdutoServicoReferencia(produtoServicoProxy.Id)
						.SetarDescricao(produtoServicoProxy.Descricao)
						.SetarCodigoInterno(produtoServicoProxy.CodigoInterno)
						.SetarUnidadeMedida(UnidadeMedidaHistorico.ComUnidadeMedida(produtoServicoEmpresa.ProdutoServico.UnidadeMedida))
						.SetarUnidadeMedidaTributavel(produtoServicoEmpresa.ProdutoServico.UnidadeMedidaTributavel.IsNull() ? null : UnidadeMedidaHistorico.ComUnidadeMedida(produtoServicoEmpresa.ProdutoServico.UnidadeMedidaTributavel))
						.SetarFatorUnidadeMedidaTributavel(produtoServicoEmpresa.ProdutoServico.FatorUnidadeMedidaTributavel)
						.SetarCodigoBarras(produtoServicoProxy.CodigoBarras)
						.SetarCodigoAtividade(produtoServicoProxy.CodigoAtividade)
						.SetarDesdobroNacionalCodigoAtividade(produtoServicoProxy.DesdobroNacionalCodigoAtividade)
						.SetarVasilhameReferencia(produtoServicoEmpresa.To<ProdutoEmpresa>()?.VasilhameReferencia.ToStringEmptyWhenNull())
						.SetarIndicadorOperacaoIbsCbs(produtoServicoProxy.IndicadorOperacaoIbsCbs)
						.SetarCnae(produtoServicoProxy.Cnae)
						.SetarCfps(produtoServicoProxy.Cfps)
						.SetarCbo(produtoServicoProxy.Cbo)
						.SetarServicoPrestadoEmViaPublica(produtoServicoProxy.ServicoPrestadoEmViaPublica)
						.SetarCodigoAnp(produtoServicoProxy.CodigoAnp)
						.SetarPercentualComissao(produtoServicoEmpresa.PercentualComissao)
						.SetarIAT(produtoServicoEmpresa.ProdutoServico.IndicadorArredondamentoTruncamento)
						.SetarIPPT(produtoServicoEmpresa.ProdutoServico.IndicadorProducaoPropriaTerceiro)
						.SetarCodigoContabil(produtoServicoEmpresa.ProdutoServico.CodigoContabil)
						.SetarCodigoContabilEntrada(produtoServicoEmpresa.ProdutoServico.CodigoContabilEntrada)
						.SetarLocalizacao(produtoServicoEmpresa.To<ProdutoEmpresa>()?.Localizacao)
						.SetarMarca(produtoServicoEmpresa.To<ProdutoEmpresa>().Produto.Marca)
						.SetarPrecoMaximoConsumidor(produtoServicoProxy.PrecoMaximoConsumidor)
						.SetarMotivoIsencaoAnvisa(produtoServicoProxy.MotivoIsencaoAnvisa)
						.Criar())
					.SetarEstoque(produtoServicoEmpresa.To<ProdutoEmpresa>().Estoque.Criar(item.Quantidade))
					.SetarDecimaisQuantidade(3)
					.SetarPrecoUnitario(item.Produto.Preco)
					.SetarDecimaisPrecoUnitario(2)
					.SetarDataHora(DateTime.Now)
					.SetarNCMNBS(produtoServicoEmpresa.NcmNbs)
					.SetarEmpresa(empresaHistorico)
					.SetarDestinatario(clienteConsumidor)
					.SetarVendedor(vendedorHistorico)
					.SetarOperacaoFiscal(percentual.OperacaoFiscal)
					.SetarIpi(produtoServicoEmpresa.TributacaoFederal.TributacaoIpi.IpiSaida)
					.SetarPercentual(percentual)
					.SetarPercentualReformaTributaria(percentualReformaTributaria)
					.SetarPis(produtoServicoEmpresa.TributacaoFederal.TributacaoPisCofins.PISSaida)
					.SetarCofins(produtoServicoEmpresa.TributacaoFederal.TributacaoPisCofins.COFINSSaida)
					.SetarCodigoEspecificadorSubstituicaoTributaria(produtoServicoEmpresa.To<ProdutoEmpresa>().CodigoEspecificadorSubstituicaoTributaria)
					.SetarCodigoBeneficioFiscalUf(codigoBeneficioFiscalUf)
					.Criar();
			}).ToList();
			Movimentacao movimentacao = FluentFactory<MovimentacaoFactory>.Init().AdicionarItens(list.ToArray()).SetarTipo(TipoMovimentacao.PreVenda)
				.SetarNumero(message.Pedido.Numero)
				.SetarDataHoraEmissao(DateTime.Now)
				.SetarEmpresa(empresaHistorico)
				.SetarPessoa(clienteConsumidor)
				.SetarNomeUsuario(message.Pedido.VendedorGarcom?.Nome ?? "Criado pelo Microterminal")
				.SetarIsPaf(empresa.To<Emitente>().PontoDeVenda.Ecfs.Any())
				.Criar();
			Movimentacao movimentacao2 = _movimentacaoRepositoryRead.FindOne(MovimentacaoSpecification.PorOrigemSituacaoNumero(1, 4, movimentacao.Numero));
			if (movimentacao2.IsNull())
			{
				movimentacao2 = movimentacao;
			}
			else
			{
				movimentacao2.AdicionarItens(movimentacao.Itens);
			}
			_movimentacaoRepositoryWrite.Update(movimentacao2);
		}
	}
	public class PedidoIfoodServerActor : ReceiveActor
	{
		private readonly ConcurrentDictionary<string, PedidoIfoodClient> _clientes = new ConcurrentDictionary<string, PedidoIfoodClient>();

		private bool _restauranteAberto;

		public PedidoIfoodServerActor(IEventPublisher eventPublisher)
		{
			eventPublisher.GetEvent<AlterouStatusRestauranteEvent>().Subscribe(delegate(AlterouStatusRestauranteEvent alterouStatusRestauranteEvent)
			{
				_restauranteAberto = alterouStatusRestauranteEvent.Aberto;
				foreach (PedidoIfoodClient item in _clientes.Values.Where((PedidoIfoodClient x) => x.EmpresaReferencia.Equals(alterouStatusRestauranteEvent.IdEmpresa)))
				{
					item.ActorRef.Tell(alterouStatusRestauranteEvent);
				}
			});
			eventPublisher.GetEvent<NovoPedidoIfoodEvent>().Subscribe(delegate(NovoPedidoIfoodEvent novoPedidoIfoodEvent)
			{
				PedidoIfoodClient pedidoIfoodClient = _clientes.Values.FirstOrDefault((PedidoIfoodClient client) => client.EmpresaReferencia.Equals(novoPedidoIfoodEvent.IdEmpresa));
				foreach (PedidoIfoodClient item2 in _clientes.Values.Where((PedidoIfoodClient x) => x.EmpresaReferencia.Equals(novoPedidoIfoodEvent.IdEmpresa)))
				{
					item2.ActorRef.Tell(novoPedidoIfoodEvent.SetarPodeConfirmarAutomaticamente(pedidoIfoodClient.Id.Equals(item2.Id)));
				}
			});
			eventPublisher.GetEvent<AtualizouPedidoIfoodEvent>().Subscribe(delegate(AtualizouPedidoIfoodEvent atualizouPedidoIfoodEvent)
			{
				foreach (PedidoIfoodClient item3 in _clientes.Values.Where((PedidoIfoodClient x) => x.EmpresaReferencia.Equals(atualizouPedidoIfoodEvent.IdEmpresa)))
				{
					item3.ActorRef.Tell(atualizouPedidoIfoodEvent);
				}
			});
			eventPublisher.GetEvent<AlteracaoEnderecoPedidoIfoodEvent>().Subscribe(delegate(AlteracaoEnderecoPedidoIfoodEvent alteracaoEnderecoPedidoIfoodEvent)
			{
				foreach (PedidoIfoodClient item4 in _clientes.Values.Where((PedidoIfoodClient x) => x.EmpresaReferencia.Equals(alteracaoEnderecoPedidoIfoodEvent.IdEmpresa)))
				{
					item4.ActorRef.Tell(alteracaoEnderecoPedidoIfoodEvent);
				}
			});
			Receive(delegate(ConnectUserRequest request)
			{
				AdicionarCliente(request, base.Sender);
				base.Sender.Tell(new ConnectUserPedidoIfoodResponse
				{
					RestauranteAberto = _restauranteAberto
				}, base.Self);
			});
			Receive(delegate(DisconnectUserRequest request)
			{
				RemoverCliente(request.Id);
			});
		}

		private void AdicionarCliente(ConnectUserRequest connectUserRequest, IActorRef actorRef)
		{
			if (!string.IsNullOrEmpty(connectUserRequest.Id))
			{
				RemoverCliente(connectUserRequest.Id);
				PedidoIfoodClient value = new PedidoIfoodClient(actorRef, connectUserRequest.Id, connectUserRequest.IdEmpresa);
				_clientes.TryAdd(connectUserRequest.Id, value);
			}
		}

		private void RemoverCliente(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			try
			{
				_clientes.TryRemove(id, out var _);
			}
			catch (Exception)
			{
			}
		}
	}
	public class PedidoIfoodClient
	{
		public IActorRef ActorRef { get; }

		public string Id { get; }

		public string EmpresaReferencia { get; }

		public PedidoIfoodClient(IActorRef actorRef, string id, string empresaReferencia)
		{
			ActorRef = actorRef;
			Id = id;
			EmpresaReferencia = empresaReferencia;
		}
	}
	public class PostoServerActorTestConnection : ReceiveActor
	{
		private readonly IRepositoryRead<Pessoa> _pessoaRepositoryRead;

		public PostoServerActorTestConnection(IRepositoryRead<Pessoa> pessoaRepositoryRead)
		{
			_pessoaRepositoryRead = pessoaRepositoryRead;
			Create();
		}

		public void Create()
		{
			Receive<ConnectPostoTestRequest>(delegate
			{
				List<KeyValuePair<string, string>> empresas = (from empresa in _pessoaRepositoryRead.Find(PessoaSpecifications.PorEmitente().PorAtivo())
					select new KeyValuePair<string, string>(empresa.Id.ToString(), $"{empresa.CpfCnpjFormatado} {empresa.Nome}")).ToList();
				List<KeyValuePair<string, string>> frentistas = (from frentista in _pessoaRepositoryRead.Find(PessoaSpecifications.AtivoPorTipoCadastro(ativo: true, TipoPessoaCadastro.Frentista))
					select new KeyValuePair<string, string>(((Fisica)frentista).Frentista.Codigo, $"{frentista.Nome}")).ToList();
				base.Sender.Tell(new ConnectPostoTestResponse(empresas, frentistas), base.Self);
			});
		}
	}
	public class PostoServerActorWcf : ReceiveActor
	{
		private readonly IDictionary<string, IActorRef> _clients = new Dictionary<string, IActorRef>();

		private readonly ITanqueRepositoryRead _tanqueRepositoryRead;

		private readonly IConfiguracaoRepositoryRead _configuracaoRepositoryRead;

		private readonly IRegistroPafEcfRepositoryWrite _registroPafEcfRepositoryWrite;

		private readonly IPessoaRepositoryRead _pessoaRepositoryRead;

		private readonly IRepositoryRead<RegistroAbastecimento> _abastecimentoRepositoryRead;

		private readonly IAbastecimentoService _abastecimentoService;

		private readonly IRepositoryWrite<RegistroQuebraContinuidadeEncerrante> _registroQuebraContinuidadeRepositoryWrite;

		private readonly IRepositoryRead<RegistroQuebraContinuidadeEncerrante> _registroQuebraContinuidadeRepositoryRead;

		private readonly IRepositoryBaseWrite<SituacaoComunicacaoBico> _situacaoComunicacaoBicoRepositoryWrite;

		private readonly IRepositoryRead<SituacaoComunicacaoBico> _situacaoComunicacaoBicoRepositoryRead;

		public PostoServerActorWcf(ITanqueRepositoryRead tanqueRepositoryRead, IRegistroPafEcfRepositoryWrite registroPafEcfRepositoryWrite, IPessoaRepositoryRead pessoaRepositoryRead, IRepositoryRead<RegistroAbastecimento> abastecimentoRepositoryRead, IAbastecimentoService abastecimentoService, IRepositoryWrite<RegistroQuebraContinuidadeEncerrante> registroQuebraContinuidadeRepositoryWrite, IRepositoryRead<RegistroQuebraContinuidadeEncerrante> registroQuebraContinuidadeRepositoryRead, IRepositoryBaseWrite<SituacaoComunicacaoBico> situacaoComunicacaoBicoRepositoryWrite, IRepositoryRead<SituacaoComunicacaoBico> situacaoComunicacaoBicoRepositoryRead, IEventPublisher eventPublisher, IConfiguracaoRepositoryRead configuracaoRepositoryRead)
		{
			_tanqueRepositoryRead = tanqueRepositoryRead;
			_registroPafEcfRepositoryWrite = registroPafEcfRepositoryWrite;
			_pessoaRepositoryRead = pessoaRepositoryRead;
			_abastecimentoRepositoryRead = abastecimentoRepositoryRead;
			_abastecimentoService = abastecimentoService;
			_registroQuebraContinuidadeRepositoryWrite = registroQuebraContinuidadeRepositoryWrite;
			_registroQuebraContinuidadeRepositoryRead = registroQuebraContinuidadeRepositoryRead;
			_situacaoComunicacaoBicoRepositoryWrite = situacaoComunicacaoBicoRepositoryWrite;
			_situacaoComunicacaoBicoRepositoryRead = situacaoComunicacaoBicoRepositoryRead;
			_configuracaoRepositoryRead = configuracaoRepositoryRead;
			eventPublisher.GetEvent(delegate(RemoverAbastecimentosRequest message)
			{
				foreach (KeyValuePair<string, IActorRef> client in _clients)
				{
					client.Value.Tell(message);
				}
			});
			eventPublisher.GetEvent(delegate(AdicionarAbastecimentosRequest message)
			{
				foreach (KeyValuePair<string, IActorRef> client2 in _clients)
				{
					client2.Value.Tell(message);
				}
			});
			eventPublisher.GetEvent(delegate(TornarAbastecimentosPendentesRequest message)
			{
				IEnumerable<RegistroAbastecimento> in2 = _abastecimentoRepositoryRead.GetIn((RegistroAbastecimento x) => x.Abastecimento.Id, message.IdsAbastecimentos);
				_registroPafEcfRepositoryWrite.Atualizar(in2.Select((RegistroAbastecimento x) => x.AtualizarStatus(StatusDoAbastecimento.Pendente)));
				foreach (RegistroAbastecimento item in in2)
				{
					ResponderAbastecimento(new NovoAbastecimentoResponse(item.EmpresaReferencia.ToStringEmptyWhenDefault(), item.Abastecimento));
				}
			});
			eventPublisher.GetEvent(delegate(TornarAbastecimentosPendentesMobileRequest message)
			{
				IEnumerable<RegistroAbastecimento> @in = _abastecimentoRepositoryRead.GetIn((RegistroAbastecimento x) => x.Abastecimento.Id, message.IdsAbastecimentos);
				_registroPafEcfRepositoryWrite.Atualizar(@in.Select((RegistroAbastecimento x) => x.AtualizarStatus(StatusDoAbastecimento.Pendente)));
				foreach (RegistroAbastecimento registroAbastecimento in @in)
				{
					foreach (KeyValuePair<string, IActorRef> item2 in _clients.Where((KeyValuePair<string, IActorRef> x) => x.Key.Equals(registroAbastecimento.EmpresaReferencia.ToStringEmptyWhenDefault())))
					{
						item2.Value.Tell(new NovoAbastecimentoResponse(registroAbastecimento.EmpresaReferencia.ToStringEmptyWhenDefault(), registroAbastecimento.Abastecimento));
					}
				}
			});
			Create();
		}

		private void Create()
		{
			Receive(delegate(ConnectPostoRequest message)
			{
				KeyValuePair<string, IActorRef> item = _clients.FirstOrDefault((KeyValuePair<string, IActorRef> x) => x.Key.Equals(message.IdEmpresa));
				if (item.Value.IsNotNull())
				{
					_clients.Remove(item);
				}
				_clients.Add(new KeyValuePair<string, IActorRef>(message.IdEmpresa, base.Sender));
				List<BombaAkka> bombas = null;
				if (!string.IsNullOrEmpty(message.IdEmpresa))
				{
					Configuracao configuracao = _configuracaoRepositoryRead.Get(message.IdEmpresa);
					IEnumerable<Tanque> source = _tanqueRepositoryRead.Find(EntityEmpresaSpecification<Tanque>.AtivoPorEmpresa(message.IdEmpresa));
					List<Abastecimento> abastecimentos = (from x in (from x in _abastecimentoRepositoryRead.Find(EntityBaseEmpresaSpecification<RegistroAbastecimento>.PorEmpresa(message.IdEmpresa).PorStatus(StatusDoAbastecimento.Pendente))
							orderby x.Id descending
							select x).Take(10000)
						select x.Abastecimento).ToListEmptyWhenNull();
					IEnumerable<SituacaoComunicacaoBico> situacoes = _situacaoComunicacaoBicoRepositoryRead.GetByFields((SituacaoComunicacaoBico x) => x.EmpresaReferencia.Equals(message.IdEmpresa.TryParseReferencia()));
					bombas = source.SelectMany((Tanque tanque) => (from x in tanque.Bombas
						where x.Ativo
						select x into b
						select new BombaAkka(b.Numero, tanque.Numero.ToString(), b.Bicos.Where((DigisatServer.Domain.Main.Aggregates.CadastrosAgg.PostosCombustiveis.Bico x) => x.Ativo).Select(delegate(DigisatServer.Domain.Main.Aggregates.CadastrosAgg.PostosCombustiveis.Bico bi)
						{
							SituacaoComunicacaoBico situacaoComunicacaoBico = situacoes.FirstOrDefault((SituacaoComunicacaoBico x) => x.BicoNoSoftware.Equals(bi.Numero));
							bi.ProdutoEmpresa.SetarTabelaPreco(bi.TabelaPrecoReferencia);
							return new BicoAkka(bi.Id.ToString(), bi.Numero, bi.BicoConcentrador, bi.PosicaoRetorno, bi.ProdutoEmpresaReferencia.ToString(), bi.ProdutoEmpresa.Preco.PrecoVendaVigente, bi.ProdutoEmpresa.ProdutoServico.Descricao.RemoverCaracteresNaoImprimiveis(), abastecimentos.Where((Abastecimento x) => x.IdBico == bi.Id.ToString()).ToListEmptyWhenNull(), tanque.CapacidadeNominal, situacaoComunicacaoBico?.DataHora ?? DateTime.MinValue, situacaoComunicacaoBico?.FalhaTotal ?? false, configuracao?.Geral?.CasasDecimaisEnviarConcentrador?.Valor ?? 3);
						}).ToList(), b.Impressora?.Caminho.ToStringEmptyWhenNull() ?? string.Empty)).ToList()).ToList();
				}
				base.Sender.Tell(new ConnectPostoResponse(bombas), base.Self);
			});
			Receive(delegate(TornarAbastecimentosPendentesRequest message)
			{
				IEnumerable<RegistroAbastecimento> @in = _abastecimentoRepositoryRead.GetIn((RegistroAbastecimento x) => x.Abastecimento.Id, message.IdsAbastecimentos);
				_registroPafEcfRepositoryWrite.Atualizar(@in.Select((RegistroAbastecimento x) => x.AtualizarStatus(StatusDoAbastecimento.Pendente)));
				foreach (RegistroAbastecimento item2 in @in)
				{
					ResponderAbastecimento(new NovoAbastecimentoResponse(item2.EmpresaReferencia.ToStringEmptyWhenDefault(), item2.Abastecimento));
				}
			});
			Receive(delegate(NovoAbastecimentoRequest message)
			{
				Pessoa pessoa = _pessoaRepositoryRead.FindOne(EntitySpecification<Pessoa>.PorId(message.IdEmpresa));
				_abastecimentoService.VerificarAbastecimento(message.Abastecimento, pessoa.Id.ToString());
				foreach (IAbastecimento abastecimento in _abastecimentoService.Abastecimentos)
				{
					RegistroAbastecimento registroAbastecimento2 = new RegistroAbastecimento(abastecimento, pessoa);
					registroAbastecimento2.BaixarEstoque();
					_registroPafEcfRepositoryWrite.Salvar(registroAbastecimento2);
					ResponderAbastecimento(new NovoAbastecimentoResponse(message.IdEmpresa, registroAbastecimento2.Abastecimento));
				}
				if (_abastecimentoService.QuebraContinuidadeEncerrante != null)
				{
					foreach (KeyValuePair<string, IActorRef> client in _clients)
					{
						if (message.IdEmpresa == client.Key)
						{
							client.Value.Tell(_abastecimentoService.QuebraContinuidadeEncerrante, base.Self);
						}
					}
				}
				if (_abastecimentoService.AbastecimentoRepetido.IsNotNull())
				{
					foreach (KeyValuePair<string, IActorRef> client2 in _clients)
					{
						if (message.IdEmpresa == client2.Key)
						{
							client2.Value.Tell(new RemoverAbastecimentoRepetidoRequest(message.IdEmpresa, message.Abastecimento), base.Self);
						}
					}
				}
				_abastecimentoService.Limpar();
			});
			Receive(delegate(AbastecimentoValidoRequest message)
			{
				RegistroAbastecimento registroAbastecimento = _abastecimentoService.GerarNovoAbastecimentoAPartirDoUltimoAbastecimentoBico(message.IdBico, message.Volume, message.IdEmpresa);
				registroAbastecimento.BaixarEstoque();
				_registroPafEcfRepositoryWrite.Salvar(registroAbastecimento);
				ResponderAbastecimento(new NovoAbastecimentoResponse(message.IdEmpresa, registroAbastecimento.Abastecimento));
			});
			Receive(delegate(RemoverQuebraContinuidadeEncerranteRequest message)
			{
				DesativarQuebraContinuidadeEncerrante(message.IdEmpresa, message.IdBico);
				foreach (KeyValuePair<string, IActorRef> client3 in _clients)
				{
					if (message.IdEmpresa == client3.Key)
					{
						client3.Value.Tell(new RemoverQuebraContinuidadeEncerranteResponse(message.IdEmpresa, message.IdBico), base.Self);
					}
				}
			});
			Receive(delegate(PerdeuComunicacaoTotalResponse perdeuComunicacaoTotal)
			{
				SalvarNotificacoesSituacaoComunicacaoBicos(perdeuComunicacaoTotal.Notificacoes, perdeuComunicacaoTotal: true);
			});
			Receive(delegate(RetornouComunicacaoTotalResponse retornouComunicacaoTotal)
			{
				SalvarNotificacoesSituacaoComunicacaoBicos(retornouComunicacaoTotal.Notificacoes);
			});
			Receive(delegate(NotificouSituacaoComunicacaoBicoResponse informacaoComunicacao)
			{
				SalvarNotificacaoSituacaoComunicacaoBico(informacaoComunicacao);
			});
		}

		private void DesativarQuebraContinuidadeEncerrante(string idEmpresa, string idBico)
		{
			try
			{
				RegistroQuebraContinuidadeEncerrante registroQuebraContinuidadeEncerrante = _registroQuebraContinuidadeRepositoryRead.FindOne(EntityEmpresaSpecification<RegistroQuebraContinuidadeEncerrante>.AtivoPorEmpresa(idEmpresa).PorIdBico(idBico));
				registroQuebraContinuidadeEncerrante?.Desativar();
				_registroQuebraContinuidadeRepositoryWrite.Update(registroQuebraContinuidadeEncerrante);
			}
			catch (Exception ex)
			{
				LoggerFactory.CreateLog().LogError("DesativarQuebraContinuidadeEncerrante: " + ex.Message, ex);
			}
		}

		private void ResponderAbastecimento(NovoAbastecimentoResponse message)
		{
			foreach (KeyValuePair<string, IActorRef> client in _clients)
			{
				if (message.IdEmpresa == client.Key)
				{
					client.Value.Tell(message, base.Self);
				}
			}
		}

		private void SalvarNotificacoesSituacaoComunicacaoBicos(IEnumerable<NotificouSituacaoComunicacaoBicoResponse> notificacoes, bool perdeuComunicacaoTotal = false)
		{
			foreach (NotificouSituacaoComunicacaoBicoResponse notificaco in notificacoes)
			{
				SalvarNotificacaoSituacaoComunicacaoBico(notificaco, perdeuComunicacaoTotal);
			}
		}

		private void SalvarNotificacaoSituacaoComunicacaoBico(NotificouSituacaoComunicacaoBicoResponse notificacao, bool perdeuComunicacaoTotal = false)
		{
			try
			{
				if (notificacao.Situacao == SituacaoComunicacaoConcentrador.Finalizada)
				{
					return;
				}
				SituacaoComunicacaoBico situacaoComunicacaoBico = _situacaoComunicacaoBicoRepositoryRead.GetByFields((SituacaoComunicacaoBico x) => x.EmpresaReferencia.Equals(notificacao.IdEmpresa.TryParseReferencia()), (SituacaoComunicacaoBico x) => x.BicoNoSoftware.Equals(notificacao.BicoNoSoftware)).FirstOrDefault();
				if (situacaoComunicacaoBico != null)
				{
					if (notificacao.Situacao == SituacaoComunicacaoConcentrador.Perda && perdeuComunicacaoTotal && !situacaoComunicacaoBico.FalhaTotal)
					{
						_situacaoComunicacaoBicoRepositoryWrite.Update(situacaoComunicacaoBico.ComFalhaTotal(falhaTotal: true));
					}
					if (notificacao.Situacao == SituacaoComunicacaoConcentrador.Retorno)
					{
						_situacaoComunicacaoBicoRepositoryWrite.Delete(situacaoComunicacaoBico);
						VerificarQuebraContinuidadeEncerrante(notificacao);
					}
				}
				else if (notificacao.Situacao == SituacaoComunicacaoConcentrador.Perda)
				{
					_situacaoComunicacaoBicoRepositoryWrite.Add(SituacaoComunicacaoBico.Criar().ComBicoNoSoftware(notificacao.BicoNoSoftware).ComEmpresaReferencia(notificacao.IdEmpresa.TryParseReferencia())
						.ComDataHora(notificacao.DataHora)
						.ComSituacao(notificacao.Situacao)
						.ComFalhaTotal(perdeuComunicacaoTotal));
				}
			}
			catch (Exception exception)
			{
				LoggerFactory.CreateLog().LogError("SalvarNotificacaoSituacaoComunicacaoBico Bico: " + notificacao.BicoNoSoftware, exception);
			}
		}

		private void VerificarQuebraContinuidadeEncerrante(NotificouSituacaoComunicacaoBicoResponse notificacao)
		{
			_abastecimentoService.VerificarQuebraContinuidadeEncerrante(notificacao);
			if (_abastecimentoService.QuebraContinuidadeEncerrante == null)
			{
				return;
			}
			foreach (KeyValuePair<string, IActorRef> client in _clients)
			{
				if (notificacao.IdEmpresa == client.Key)
				{
					client.Value.Tell(_abastecimentoService.QuebraContinuidadeEncerrante, base.Self);
				}
			}
			_abastecimentoService.Limpar();
		}
	}
	public class RestauranteServerActor : ReceiveActor
	{
		private readonly ConcurrentDictionary<string, RestauranteClient> _clients = new ConcurrentDictionary<string, RestauranteClient>();

		public RestauranteServerActor(IEventPublisher eventPublisher)
		{
			eventPublisher.GetEvent(delegate(AtualizarItensPedidosRequest request)
			{
				foreach (RestauranteClient item in _clients.Values.Where((RestauranteClient x) => x.IdEmpresa.Equals(request.IdEmpresa)))
				{
					item.Actor.Tell(request);
				}
			});
			Receive(delegate(ConnectRestauranteRequest request)
			{
				AdicionarClient(request, base.Sender);
				base.Sender.Tell(new ConnectResponse("Conexão estabelecida com sucesso 'Restaurante Server Actor'."), base.Self);
			});
			Receive(delegate(string request)
			{
				RemoverClient(request);
			});
		}

		private void AdicionarClient(ConnectRestauranteRequest connectRestauranteRequest, IActorRef actorRef)
		{
			try
			{
				RemoverClient(connectRestauranteRequest.Id);
				RestauranteClient value = new RestauranteClient(connectRestauranteRequest.Id, connectRestauranteRequest.IdEmpresa, actorRef);
				_clients.TryAdd(connectRestauranteRequest.Id, value);
			}
			catch (Exception exception)
			{
				LoggerFactory.CreateLog().LogError("Falha ao adicionar cliente RestauranteServerActor", exception);
			}
		}

		private void RemoverClient(string id)
		{
			try
			{
				string text = _clients.Keys.FirstOrDefault((string x) => x.Equals(id));
				if (!string.IsNullOrEmpty(text))
				{
					_clients.TryRemove(text, out var _);
				}
			}
			catch (Exception exception)
			{
				LoggerFactory.CreateLog().LogError("Falha ao remover cliente RestauranteServerActor", exception);
			}
		}
	}
	public class RestauranteClient
	{
		public string Id { get; }

		public string IdEmpresa { get; }

		public IActorRef Actor { get; }

		public RestauranteClient(string id, string idEmpresa, IActorRef actor)
		{
			Id = id;
			IdEmpresa = idEmpresa;
			Actor = actor;
		}
	}
	public class SatServerActorWcf : ReceiveActor
	{
		private IActorRef _servidorSat;

		public SatServerActorWcf(IEventPublisher eventPublisher)
		{
			SatServerActorWcf satServerActorWcf = this;
			eventPublisher.GetEvent(delegate(EnviarDadosVendaRequest enviarDadosVendaRequest)
			{
				if (satServerActorWcf._servidorSat.IsNull())
				{
					eventPublisher.Publish(new ErroCFeResponse("Serviço do S@t não está iniciado!"));
				}
				else
				{
					satServerActorWcf._servidorSat.Tell(enviarDadosVendaRequest);
				}
			});
			Receive<ConnectRequest>(delegate
			{
				_servidorSat = base.Sender;
				UntypedActor.Context.Watch(base.Sender);
				base.Sender.Tell(new ConnectResponse("Conectado com o SatServerActorWcf"), base.Self);
			});
			Receive(delegate(EnviarDadosVendaResponse r)
			{
				eventPublisher.Publish(r);
			});
			Receive(delegate(ErroCFeResponse r)
			{
				eventPublisher.Publish(r);
			});
			Receive(delegate(FalhaCFeResponse r)
			{
				eventPublisher.Publish(r);
			});
			Receive<Terminated>(delegate
			{
				_servidorSat = null;
			});
		}
	}
	public class SincronizacaoClient
	{
		public IActorRef ActorRef { get; }

		public DateTime DataHoraOffline { get; protected set; }

		public bool OfflineAMaisDeUmDia
		{
			get
			{
				if (DataHoraOffline > DateTime.MinValue)
				{
					return DataHoraOffline < DateTime.Now.AddDays(-1.0);
				}
				return false;
			}
		}

		public SincronizacaoClient(IActorRef actorRef)
		{
			ActorRef = actorRef;
		}

		public void SetarOffline()
		{
			DataHoraOffline = DateTime.Now.AddMinutes(-5.0);
		}
	}
	public class SincronizacaoServerActor : ReceiveActor
	{
		private readonly ConcurrentSet<SincronizacaoClient> _clients = new ConcurrentSet<SincronizacaoClient>();

		private readonly RepositorySincronizacao _repositorySincronizacao;

		private readonly ISincronizacaoAppService _sincronizacaoAppService;

		private OpLogTail _tail;

		private IEnumerable<string> ColecoesASincronizar => new List<string>
		{
			"Usuarios", "Perfis", "Configuracoes", "Pessoas", "UnidadesMedida", "ProdutosServicos", "ProdutosServicosEmpresa", "Precos", "Promocoes", "TabelasPreco",
			"Estoques", "Kits", "Grades", "LinhasColunas", "TributacoesEstadual", "TributacoesMunicipal", "TributacoesFederal", "TributacoesIPI", "TributacoesPISCOFINS", "Grupos",
			"SubGrupos", "Contas", "PlanosConta", "CentrosCusto", "OperacoesFiscais", "FormasPagamento", "Cardapios", "Impressoras", "OrdensCardapio", "ObservacoesRestaurante",
			"SaldosIcmsStRetido", "RelacoesEspecieConta", "PromocoesScanntech", "Moedas", "MenusRapido", "GruposMenuRapido", "OrdensMenuRapido", "PredefinicoesMenuRapido", "TributacoesIbsCbs", "TributacoesIs"
		};

		public SincronizacaoServerActor(RepositorySincronizacao repositorySincronizacao, ISincronizacaoAppService sincronizacaoAppService, IEventPublisher eventPublisher)
		{
			_repositorySincronizacao = repositorySincronizacao;
			_sincronizacaoAppService = sincronizacaoAppService;
			eventPublisher.GetEvent<SincronizarClientes>().Subscribe(delegate(SincronizarClientes clientes)
			{
				if (clientes.DarCarga)
				{
					foreach (SincronizacaoClient item in (IEnumerable<SincronizacaoClient>)_clients)
					{
						item.ActorRef.Tell(new RestaurouBackupRequest());
					}
					_clients.Clear();
				}
			});
			eventPublisher.GetEvent<AntesRestaurarBaseDadosEvent>().Subscribe(delegate(AntesRestaurarBaseDadosEvent x)
			{
				_tail?.Dispose();
				foreach (SincronizacaoClient item2 in (IEnumerable<SincronizacaoClient>)_clients)
				{
					item2.ActorRef.Tell(x);
				}
			});
			Running();
			(from x in Observable.Interval(TimeSpan.FromMinutes(30.0))
				select _clients.Where((SincronizacaoClient z) => z.OfflineAMaisDeUmDia).ToListEmptyWhenNull()).Subscribe(delegate(List<SincronizacaoClient> clients)
			{
				foreach (SincronizacaoClient client in clients)
				{
					_clients.TryRemove(client);
				}
			});
			(from x in Observable.Interval(TimeSpan.FromMinutes(1.0))
				select _clients.ToListEmptyWhenNull()).Subscribe(delegate(List<SincronizacaoClient> clients)
			{
				foreach (SincronizacaoClient client2 in clients)
				{
					GravarLog($"Cliente conectado de dentro do observable {client2.ActorRef.Path.Name} - {DateTime.Now}");
				}
			});
		}

		private void InserirClient(IActorRef actorRef)
		{
			SincronizacaoClient sincronizacaoClient = _clients.FirstOrDefault((SincronizacaoClient x) => x.ActorRef.Path.Name.Equals(actorRef.Path.Name));
			if (sincronizacaoClient.IsNotNull())
			{
				GravarLog($"Removeu cliente {actorRef.Path.Name} - {DateTime.Now}");
				_clients.TryRemove(sincronizacaoClient);
			}
			_clients.TryAdd(new SincronizacaoClient(actorRef));
			GravarLog($"Conectou cliente {actorRef.Path.Name} - {DateTime.Now}");
		}

		public void GravarLog(string informacao)
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogSincronizador.txt");
			if (!File.Exists(path))
			{
				return;
			}
			using StreamWriter streamWriter = new StreamWriter(path, append: true);
			streamWriter.WriteLine(informacao);
			streamWriter.Close();
		}

		private void Running()
		{
			if (Environment.GetCommandLineArgs().Any((string x) => x.Contains("Conversor")))
			{
				return;
			}
			_tail = OpLog.LoadFrom(_repositorySincronizacao.Database).Tail(Operations.InsertUpdateAndDelete);
			foreach (string item in ColecoesASincronizar)
			{
				CollectionReplicator.From($"from_{item}_server", _tail, _repositorySincronizacao.Database.DatabaseNamespace.DatabaseName, item).To(_clients.Select((SincronizacaoClient x) => x.ActorRef), base.Self, $"from_{item}_client");
			}
			_tail.Run();
			Receive<SincronizacaoConnectRequest>(delegate
			{
				InserirClient(base.Sender);
				UntypedActor.Context.Watch(base.Sender);
				base.Sender.Tell(new SincronizacaoConnectResponse(), base.Self);
			});
			Receive(delegate(Terminated terminated)
			{
				SincronizacaoClient sincronizacaoClient = _clients.FirstOrDefault((SincronizacaoClient x) => x.ActorRef.Path.Name.Equals(terminated.ActorRef.Path.Name));
				sincronizacaoClient?.SetarOffline();
				GravarLog($"Setou client offline {sincronizacaoClient?.ActorRef.Path.Name}");
			});
			Receive<SincronizacaoReConnectRequest>(delegate
			{
				DateTime dateTime = _clients.FirstOrDefault((SincronizacaoClient x) => x.ActorRef.Path.Name.Equals(base.Sender.Path.Name))?.DataHoraOffline ?? DateTime.MinValue;
				InserirClient(base.Sender);
				UntypedActor.Context.Watch(base.Sender);
				base.Sender.Tell(new SincronizacaoConnectResponse(), base.Self);
				if (dateTime == DateTime.MinValue)
				{
					EnviarCarga(base.Sender);
				}
				else
				{
					OpLogTail tailReconnect = OpLog.LoadFrom(_repositorySincronizacao.Database).Tail(Operations.InsertUpdateAndDelete);
					foreach (string item2 in ColecoesASincronizar)
					{
						CollectionReplicator.From($"from_{item2}_server", tailReconnect, _repositorySincronizacao.Database.DatabaseNamespace.DatabaseName, item2).To(new List<ICanTell> { base.Sender }, base.Self, $"from_{item2}_client", updateLastTs: false);
					}
					tailReconnect.RunBetween(dateTime, DateTime.Now).ContinueWith(delegate
					{
						tailReconnect.Dispose();
					});
				}
			});
			Receive<SincronizacaoDataRequest>(delegate
			{
				EnviarCarga(base.Sender);
			});
			ReceiveAsync((OpLogInsertRequest request) => _sincronizacaoAppService.ReplaceOne(request.CollectionAffected, request.Id, request.Data));
		}

		private async void EnviarCarga(IActorRef sender)
		{
			int numeroRegistrosEnviarPorPagina = GetNumeroRegistros();
			GravarLog($"Carga completa: enviando {numeroRegistrosEnviarPorPagina} registros por página. {DateTime.Now}");
			try
			{
				foreach (string colecao in ColecoesASincronizar)
				{
					await Task.Run(delegate
					{
						ObjectId? lastId = null;
						bool dropCollection = true;
						while (true)
						{
							List<string> list = _repositorySincronizacao.Get(colecao, lastId, numeroRegistrosEnviarPorPagina).ToList();
							if (!list.Any())
							{
								break;
							}
							sender.Tell(new SincronizacaoDataResponse(colecao, dropCollection, list));
							dropCollection = false;
							lastId = BsonDocument.Parse(list.Last())["_id"].AsObjectId;
						}
					}).ContinueWith(delegate(Task task)
					{
						if (task.IsFaulted)
						{
							GravarLog($"{colecao}: {task.Exception?.GetBaseException().Message}\n{task.Exception?.StackTrace}\n ---");
						}
					});
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				GravarLog($"Terminou em {DateTime.Now}---");
			}
		}

		private int GetNumeroRegistros()
		{
			try
			{
				int num = Convert.ToInt32(Environment.GetEnvironmentVariable("SINCRONIZADOR_NUMERO_REGISTROS", EnvironmentVariableTarget.Machine));
				return (num <= 0) ? 100 : num;
			}
			catch (Exception)
			{
				return 100;
			}
		}
	}
	public class WcfServerActor : ReceiveActor
	{
		public WcfServerActor()
		{
			Receive<ConnectRequest>(delegate
			{
				base.Sender.Tell(new ConnectResponse("Conexão estabelecida com sucesso"), base.Self);
			});
		}
	}
}

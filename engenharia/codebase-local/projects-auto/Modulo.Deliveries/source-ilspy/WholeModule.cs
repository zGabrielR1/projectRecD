using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraPrinting.Native;
using DigisatServer.Application.Core.Projections.AuxiliaresProjection;
using DigisatServer.Application.Core.Projections.MovimentacoesProjection;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Autorizacoes;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Cadastros.Fretes;
using DigisatServer.Application.Main.Dto.Cadastros.Kits;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Application.Main.Dto.Financeiros.CentrosCusto;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Financeiros.PlanosConta;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Domain.Core;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Impressoras;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.IndicadoresPresencaConsumidor;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Classificacoes;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Paises;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Dto.Financeiros.EspeciesPagamento;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.IndicadoresOperacaoConsumidorFinal;
using DigisatServer.Infrastructure.Crosscutting.Model.Prioridades;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesEntrega;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Projection.Financeiros;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ConsultaCep;
using DigisatServer.Infrastructure.Crosscutting.Service.IdentificacaoChamada;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using DigisatServer.Infrastructure.Crosscutting.ValueObject;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Especies.Pagamentos;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Modulo.Deliveries.Events;
using Otus.Modulo.Deliveries.Resources;
using Otus.Modulo.Deliveries.ViewModels.Atendimentos;
using Otus.Modulo.Deliveries.ViewModels.Base;
using Otus.Modulo.Deliveries.ViewModels.Base.Documentos;
using Otus.Modulo.Deliveries.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Deliveries")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Deliveries")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Deliveries
{
	[Module(ModuleName = "DeliveryModulo", OnDemand = true)]
	[ModuleDependency("FreteModulo")]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("EntregaDeliveryModulo")]
	[ModuleDependency("CentroCustoModulo")]
	[ModuleDependency("PlanoContaModulo")]
	[ModuleDependency("MovimentacaoModulo")]
	public class DeliveryModulo : IModule
	{
		private readonly IUnityContainer _container;

		public DeliveryModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<DeliveryFichaPrincipalView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DeliveryView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Deliveries.Views
{
	public class DeliveryFichaPrincipalView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DeliveryFichaPrincipalView()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Deliveries;component/views/deliveryfichaprincipalview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			_contentLoaded = true;
		}
	}
	public class DeliveryView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DeliveryView()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Deliveries;component/views/deliveryview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			_contentLoaded = true;
		}
	}
}
namespace Otus.Modulo.Deliveries.Views.DeliveryFichaViewDetalhes.Atendimentos
{
	public class IdentificacaoChamadaView : UserControl, IComponentConnector
	{
		internal ButtonInfo RemoverTabelaPreco;

		internal ButtonInfo PesquisarTabelaPreco;

		internal ButtonInfo RemoverVendedor;

		internal ButtonInfo PesquisarVendedor;

		private bool _contentLoaded;

		public IdentificacaoChamadaView()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Deliveries;component/views/deliveryfichaviewdetalhes/atendimentos/identificacaochamadaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				RemoverTabelaPreco = (ButtonInfo)target;
				break;
			case 2:
				PesquisarTabelaPreco = (ButtonInfo)target;
				break;
			case 3:
				RemoverVendedor = (ButtonInfo)target;
				break;
			case 4:
				PesquisarVendedor = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class ItensFichaView : CustomUserControl, IComponentConnector
	{
		internal ListViewFocusable ListaItensMovimentacao;

		private bool _contentLoaded;

		public ItensFichaView()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Deliveries;component/views/deliveryfichaviewdetalhes/atendimentos/itensfichaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			if (connectionId == 1)
			{
				ListaItensMovimentacao = (ListViewFocusable)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class TotalizacaoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterFrete;

		internal ButtonInfo ParameterDesconto;

		internal ButtonInfo ParameterPlanoConta;

		internal ButtonInfo ParameterCentroCusto;

		private bool _contentLoaded;

		public TotalizacaoView()
		{
			InitializeComponent();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Deliveries;component/views/deliveryfichaviewdetalhes/atendimentos/totalizacaoview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParameterFrete = (ButtonInfo)target;
				break;
			case 2:
				ParameterDesconto = (ButtonInfo)target;
				break;
			case 3:
				ParameterPlanoConta = (ButtonInfo)target;
				break;
			case 4:
				ParameterCentroCusto = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.Deliveries.ViewModels
{
	public class DeliveryFichaPrincipalViewModel : FichaMultiAbaBaseViewModel
	{
		private readonly IWService _service;

		private readonly ISessao _sessao;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IProdutoServicoEmpresaRepositoryRead _produtoServicoEmpresaRepositoryRead;

		public DelegateCommand EfetivarMovimentacaoCommand { get; set; }

		public DelegateCommand CancelarMovimentacaoCommand { get; set; }

		public DelegateCommand AbrirEntregasCommand { get; set; }

		public DeliveryFichaPrincipalViewModel(IWService service, INotificacao notificacao, IIdentificadorDeChamadaService identificadorDeChamadaService, ISessaoService sessao, IImpressoraNaoFiscalService impressoraNaoFiscalService, IInteracaoBarraTarrefaService interacaoBarraTarrefaService, IAutenticacaoService autenticacaoService, IProdutoServicoEmpresaRepositoryRead produtoServicoEmpresaRepositoryRead, IBalancaService balancaService)
		{
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Expected O, but got Unknown
			_service = service;
			_sessao = sessao.Sessao;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_produtoServicoEmpresaRepositoryRead = produtoServicoEmpresaRepositoryRead;
			((FichaMultiAbaBaseViewModel)this).AdicionarFormulario((FichaMultiAbaFormularioBaseViewModel)(object)new AtendimentoFormulario(service, (NavigationBase)(object)this, notificacao, identificadorDeChamadaService, interacaoBarraTarrefaService, autenticacaoService, balancaService));
			((FichaMultiAbaBaseViewModel)this).ListaFormularios.PosicionarNoPrimeiro();
			EfetivarMovimentacaoCommand = new DelegateCommand((Action)EfetivarMovimentacao, (Func<bool>)PodeEfetivarMovimentacao, (bool?)null);
			CancelarMovimentacaoCommand = new DelegateCommand((Action)CancelarMovimentacao, (Func<bool>)PodeCancelarMovimentacao, (bool?)null);
			AbrirEntregasCommand = new DelegateCommand((Action)AbrirEntregas);
			((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).ValidouEvent += OnValidouEvent;
		}

		private void AbrirEntregas()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)85);
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("EntregasDeliveryFichaPrincipalView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private void OnValidouEvent(object sender, bool b)
		{
			((CommandBase<object>)(object)CancelarMovimentacaoCommand).RaiseCanExecuteChanged();
			((CommandBase<object>)(object)EfetivarMovimentacaoCommand).RaiseCanExecuteChanged();
		}

		private bool PodeEfetivarMovimentacao()
		{
			if (((FichaMultiAbaBaseViewModel)this).ListaFormularios != null && HelperObject.Is<AtendimentoFormulario>((object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado))
			{
				return ((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Valido;
			}
			return false;
		}

		private async void EfetivarMovimentacao()
		{
			try
			{
				((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).TotalizarMovimentacao();
				MovimentacaoDto movimentacao = ((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).MovimentacaoDto;
				if (movimentacao.TotalFrete > 0m)
				{
					movimentacao.ModalidadeFrete.Codigo = 1;
				}
				GerarParcelas(ref movimentacao);
				movimentacao.SituacaoEntrega = (SituacaoEntregaDto)new AguardandoEntregaDto();
				MovimentacaoDto obj = movimentacao;
				object vendedorResponsavel;
				if (!HelperObject.Is<PessoaDtoNull>((object)movimentacao.VendedorResponsavel))
				{
					vendedorResponsavel = movimentacao.VendedorResponsavel;
				}
				else
				{
					ItemDto obj2 = movimentacao.Itens.FirstOrDefault((ItemDto x) => !HelperObject.Is<PessoaDtoNull>((object)x.VendedorResponsavel));
					vendedorResponsavel = ((object)((obj2 != null) ? obj2.VendedorResponsavel : null)) ?? ((object)new PessoaDtoNull());
				}
				obj.VendedorResponsavel = (PessoaDto)vendedorResponsavel;
				MovimentacaoDto val = await _service.Canal<IDeliveryModuleService, Task<MovimentacaoDto>>((Func<IDeliveryModuleService, Task<MovimentacaoDto>>)((IDeliveryModuleService service) => service.EfetivarAsync(movimentacao)));
				ImprimirAmbienteProducao(val);
				if (DialogoFactory.MostrarDialogoSimNao("Movimentação concluída com sucesso. Deseja imprimir o pedido agora?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).ReiniciarFormulario();
					return;
				}
				ImpressaoPedidoService val2 = new ImpressaoPedidoService(_service, val, movimentacao.TotalFrete, _sessao, _impressoraNaoFiscalService);
				for (int i = 0; i < DadosSistema.ConfiguracaoDto.Saida.NumeroViasDelivery.Valor; i++)
				{
					val2.ImprimirPedido();
				}
				((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).ReiniciarFormulario();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
			finally
			{
				((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Valido = false;
			}
		}

		private async void ImprimirAmbienteProducao(MovimentacaoDto movimentacaoDto)
		{
			IEnumerable<string> source = movimentacaoDto.Itens.Select((ItemDto x) => x.ProdutoServicoEmpresaReferencia);
			IEnumerable<ProdutoServicoEmpresa> source2 = await _produtoServicoEmpresaRepositoryRead.AggregateIn(source.Select((string x) => HelperObjectId.TryParseReferencia(x)));
			IEnumerable<ProdutoServicoEmpresa> source3 = source2.Where((ProdutoServicoEmpresa p) => HelperObject.IsNotNull((object)p.Impressora));
			int num = 0;
			foreach (IGrouping<ObjectId, ProdutoServicoEmpresa> itensImpressora in from x in source2.Where(delegate(ProdutoServicoEmpresa p)
				{
					Impressora impressora2 = p.Impressora;
					return !string.IsNullOrEmpty((impressora2 != null) ? impressora2.Caminho : null);
				})
				group x by ((EntityBase)x.Impressora).Id)
			{
				num++;
				Impressora impressora = itensImpressora.First().Impressora;
				IEnumerable<ItemDto> itens = movimentacaoDto.Itens.Where((ItemDto x) => itensImpressora.Select(delegate(ProdutoServicoEmpresa i)
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					ObjectId id = ((EntityBase)i).Id;
					return ((object)(ObjectId)(ref id)).ToString();
				}).Contains(x.ProdutoServicoEmpresaReferencia));
				string ambientesProducao = string.Format("{0}/{1} - {2}", num, (from x in source3
					group x by ((EntityBase)x.Impressora).Id).Distinct().Count(), string.Join(",", source3.Select((ProdutoServicoEmpresa x) => x.Impressora.Descricao).Distinct()));
				IEnumerable<string> enumerable = CriarImpresso(movimentacaoDto, itens, ambientesProducao, impressora.Descricao);
				if (DadosSistema.ConfiguracaoDto.Saida.UtilizarFonteMaiorImprimirPedidoNoAmbienteDeProducao.Valor)
				{
					_impressoraNaoFiscalService.ImprimirComDestaque(enumerable, impressora.Caminho, impressora.AvancoPapel);
				}
				else
				{
					_impressoraNaoFiscalService.Imprimir(enumerable, impressora.Caminho, impressora.AvancoPapel);
				}
			}
		}

		private IEnumerable<string> CriarImpresso(MovimentacaoDto movimentacaoDto, IEnumerable<ItemDto> itens, string ambientesProducao, string ambiente)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Invalid comparison between Unknown and I4
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Invalid comparison between Unknown and I4
			int caracteres = (((int)DadosSistema.TamanhoBobina == 1) ? 32 : 42);
			List<string> list = new List<string>();
			AdicionarInformacaoDeliveryImpressoPedido(caracteres, list);
			BoleanoDto imprimirDescricaoAmbienteImpressoAmbienteDeProducao = DadosSistema.ConfiguracaoDto.Saida.ImprimirDescricaoAmbienteImpressoAmbienteDeProducao;
			if (imprimirDescricaoAmbienteImpressoAmbienteDeProducao != null && imprimirDescricaoAmbienteImpressoAmbienteDeProducao.Valor)
			{
				AdicionarInformacaoAmbienteProducao(ambientesProducao, ambiente, caracteres, list);
			}
			if ((int)DadosSistema.TamanhoBobina == 1)
			{
				list.Add($"IMPRESSO PEDIDO: {((MovimentacaoBaseDto)movimentacaoDto).Numero}");
				list.Add("".PadRight(32, '-'));
				list.Add(DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoItemImpressoAmbienteDeProducao.Valor ? string.Format("{0,-27} QTD", "CODIGO - DESCRICAO") : string.Format("{0,-27} QTD", "DESCRICAO"));
				list.Add("".PadRight(32, '-'));
				foreach (ItemDto iten in itens)
				{
					if (iten.Cancelado)
					{
						list.Add("".PadLeft(10, '#') + " CANCELADO ".PadRight(10, '#'));
					}
					string text = (DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoItemImpressoAmbienteDeProducao.Valor ? $"{iten.ProdutoServico.CodigoInterno} - {iten.ProdutoServico.Descricao.ToUpper()}" : $"{iten.ProdutoServico.Descricao.ToUpper()}");
					if (text.Length > 25)
					{
						list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text, false), 0, 32, true),-32}");
						list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text, false), 32, 24, true),-25} {iten.Quantidade,6:0.###}");
					}
					else
					{
						list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text, false), 0, 24, true).ToUpper(),-25} {iten.Quantidade,6:0.###}");
					}
					if (!string.IsNullOrEmpty(iten.ObservacaoDigitada))
					{
						list.Add($"-- {iten.ObservacaoDigitada}");
					}
					list.Add("".PadRight(32, '-'));
				}
				list.Add($"Soma da quantidade de itens: {itens.Where((ItemDto x) => !x.Cancelado).Sum((ItemDto x) => x.Quantidade)}");
				list.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
				list.Add(new string('-', 32));
				AdicionarInformacaoDeliveryImpressoPedido(caracteres, list);
				return list;
			}
			list.Add($"IMPRESSO PEDIDO: {((MovimentacaoBaseDto)movimentacaoDto).Numero}");
			list.Add("".PadRight(40, '-'));
			list.Add(DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoItemImpressoAmbienteDeProducao.Valor ? string.Format("{0,-36} QTD", "CODIGO - DESCRICAO") : string.Format("{0,-36} QTD", "DESCRICAO"));
			list.Add("".PadRight(40, '-'));
			foreach (ItemDto iten2 in itens)
			{
				if (iten2.Cancelado)
				{
					list.Add("".PadLeft(14, '#') + " CANCELADO ".PadRight(26, '#'));
				}
				string text2 = (DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoItemImpressoAmbienteDeProducao.Valor ? $"{iten2.ProdutoServico.CodigoInterno} - {iten2.ProdutoServico.Descricao.ToUpper()}" : $"{iten2.ProdutoServico.Descricao.ToUpper()}");
				if (text2.Length > 33)
				{
					list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text2, false), 0, 40, true),-40}");
					list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text2, false), 40, 33, true),-33} {iten2.Quantidade,6:0.000}");
				}
				else
				{
					list.Add($"{HelperString.CopiarTexto(HelperString.RemoverAcentos(text2, false), 0, 32, true),-33} {iten2.Quantidade,6:0.000}");
				}
				if (!string.IsNullOrEmpty(iten2.ObservacaoDigitada))
				{
					list.Add($"-- {iten2.ObservacaoDigitada}");
				}
				list.Add("".PadRight(40, '-'));
			}
			list.Add($"Soma da quantidade de itens: {itens.Where((ItemDto x) => !x.Cancelado).Sum((ItemDto x) => x.Quantidade)}");
			list.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").PadLeft(15));
			list.Add(new string('-', 40));
			AdicionarInformacaoDeliveryImpressoPedido(caracteres, list);
			return list;
		}

		private static void AdicionarInformacaoAmbienteProducao(string ambientesToText, string ambiente, int caracteres, ICollection<string> linhas)
		{
			int num = caracteres - ambiente.Length;
			string arg = "".PadLeft((num - 1) / 2, '-');
			string arg2 = "".PadRight((num - 2) / 2, '-');
			linhas.Add("".PadRight(caracteres, '-'));
			linhas.Add($"{arg} {ambiente.ToUpper()} {arg2}");
			linhas.Add("".PadRight(caracteres, '-'));
			linhas.Add($"{ambientesToText.ToUpper()}");
			linhas.Add("".PadRight(caracteres, '-'));
		}

		private static void AdicionarInformacaoDeliveryImpressoPedido(int caracteres, ICollection<string> linhas)
		{
			string arg = "".PadLeft((caracteres - 20) / 2, '#');
			string arg2 = "".PadRight((caracteres - 20) / 2, '#');
			linhas.Add("".PadRight(caracteres, '#'));
			linhas.Add($"{arg} DELIVERY - ENTREGA {arg2}");
			linhas.Add("".PadRight(caracteres, '#'));
		}

		private void GerarParcelas(ref MovimentacaoDto movimentacaoDto)
		{
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Expected O, but got Unknown
			bool flag = false;
			IAbaViewModelBase val = ((Formulario)(PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).Abas.FirstOrDefault((IAbaViewModelBase aba) => aba is TotalizacaoAbaViewModel);
			if (HelperObject.IsNotNull((object)val))
			{
				IViewModelDtoBase obj = (val.ViewModel as TotalizacaoViewModel)?.OpcoesPagamento?.ItemSelecionado;
				flag = obj != null && HelperObject.Is<Cartao>((object)obj);
			}
			IEnumerable<EspeciePagamentoDto> source = EspeciePagamentoProjection.ProjectedAsCollectionDto((IEnumerable<EspeciePagamento>)((ValueObjectRepository<EspeciePagamento>)(object)ValueObjectService<EspeciePagamento, EspeciePagamentoValueObjectRepository>.ValueObjectRepository).Dados);
			List<PagamentoRecebimentoDto> list = new List<PagamentoRecebimentoDto>();
			PagamentoRecebimentoDto val2 = (PagamentoRecebimentoDto)new RecebimentoDto();
			val2.Conta = movimentacaoDto.Contas.FirstOrDefault();
			val2.EspeciePagamento = (flag ? source.FirstOrDefault((EspeciePagamentoDto x) => x.Codigo == 7) : source.FirstOrDefault((EspeciePagamentoDto x) => x.Codigo == 1));
			val2.Ordem = 1;
			val2.Valor = ((MovimentacaoBaseDto)movimentacaoDto).Total;
			val2.DataVencimento = DateTime.Today;
			val2.Descricao = "Gerado pelo sistema.";
			val2.PlanoConta = movimentacaoDto.PlanoContaPadrao;
			val2.CentroCusto = movimentacaoDto.CentroCustoPadrao;
			HelperObject.ToDo<RecebimentoDto>((object)val2, (Action<RecebimentoDto>)delegate(RecebimentoDto recebimentoDto)
			{
				recebimentoDto.Juro = DadosSistema.ConfiguracaoDto.Financeiro.PercentualJuro.Valor;
				recebimentoDto.Multa = DadosSistema.ConfiguracaoDto.Financeiro.PercentualMulta.Valor;
			});
			list.Add(val2);
			((MovimentacaoBaseDto)movimentacaoDto).PagamentosRecebimentos = list;
		}

		private bool PodeCancelarMovimentacao()
		{
			if (((FichaMultiAbaBaseViewModel)this).ListaFormularios != null && HelperObject.Is<AtendimentoFormulario>((object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado))
			{
				return HelperObject.IsExactlyObject<MovimentacaoDto>((object)((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).MovimentacaoDto);
			}
			return false;
		}

		private void CancelarMovimentacao()
		{
			((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)(AtendimentoFormulario)(object)((FichaMultiAbaBaseViewModel)this).ListaFormularios.ItemSelecionado).Formulario).ReiniciarFormulario();
		}

		public override void Dispose()
		{
			foreach (FichaMultiAbaFormularioBaseViewModel iten in ((FichaMultiAbaBaseViewModel)this).ListaFormularios.Itens)
			{
				iten.Dispose();
			}
			((BaseMinimizar)this).Dispose();
		}
	}
	public class DeliveryViewModel : ModuloBase
	{
		public DeliveryViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloDeliveriesRegion, typeof(DeliveryFichaPrincipalView).Name, (IResolucao)new ResolucaoTotalTrabalho())
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			DadosSistema.GetEventPublisher().GetEvent<RestaurarResolucaoEvent>((Action<RestaurarResolucaoEvent>)delegate
			{
				((ResolucaoBase)HelperObject.To<ResolucaoTotalTrabalho>((object)((ModuloBase)this).Resolucao)).Update();
			});
		}
	}
	public class AtendimentoFormulario : FichaMultiAbaFormularioBaseViewModel
	{
		public override string Titulo => "ATENDIMENTOS";

		public override string Descricao => "Identificação do cliente/Ligação";

		public override string DescricaoAtalho => "Ctrl+1";

		public override bool Valido
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ((FichaMultiAbaFormularioBaseViewModel)this).Valido));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ((FichaMultiAbaFormularioBaseViewModel)this).Valido), value);
			}
		}

		public AtendimentoFormulario(IWService service, NavigationBase navigation, INotificacao notificacao, IIdentificadorDeChamadaService identificadorDeChamadaService, IInteracaoBarraTarrefaService interacaoBarraTarrefaService, IAutenticacaoService autenticacaoService, IBalancaService balancaService)
		{
			((FichaMultiAbaFormularioBaseViewModel)this).Formulario = (IFormulario)(object)new PedidosFormulario(service, navigation, notificacao, identificadorDeChamadaService, interacaoBarraTarrefaService, autenticacaoService, balancaService);
			((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)this).Formulario).ValidouEvent += OnValidouEvent;
		}

		private void OnValidouEvent(object sender, bool b)
		{
			((FichaMultiAbaFormularioBaseViewModel)this).Valido = b;
		}

		public override void Dispose()
		{
			((PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)this).Formulario).ValidouEvent -= OnValidouEvent;
			((Formulario)(PedidosFormulario)(object)((FichaMultiAbaFormularioBaseViewModel)this).Formulario).Dispose();
			((FichaMultiAbaFormularioBaseViewModel)this).Dispose();
		}
	}
}
namespace Otus.Modulo.Deliveries.ViewModels.Base
{
	public class DadosEnderecoViewModel : ViewModelPartialBase<EnderecoDto>
	{
		private readonly IConsultaCepService _consultaCepService;

		public bool InformouCepComLogradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Logradouro' é obrigatório")]
		public string Logradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Logradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Logradouro), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'CEP' é obrigatório")]
		public string Cep
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cep));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cep), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Número' é obrigatório")]
		public string Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Numero), value);
			}
		}

		public string Complemento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Complemento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Complemento), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Bairro' é obrigatório")]
		public string Bairro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Bairro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Bairro), value);
			}
		}

		public MunicipioDto Municipio
		{
			get
			{
				return ((BindableBase)this).GetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio));
			}
			set
			{
				((BindableBase)this).SetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio), value, (Action)delegate
				{
					MunicipioDto municipio = Municipio;
					MunicipioDescricao = ((municipio != null) ? HelperString.ToStringEmptyWhenNull(municipio.Nome) : null);
				});
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Município' é obrigatório.")]
		public string MunicipioDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MunicipioDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MunicipioDescricao), value);
			}
		}

		public ISelecaoCommandParameter BuscarMunicipioCommandParameter { get; set; }

		public ICommand PesquisarCepCommand { get; set; }

		public DadosEnderecoViewModel(EnderecoDto dto)
			: base(dto)
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			BuscarMunicipioCommandParameter = (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MunicipioDto>("MunicipioListaView", (Action<MunicipioDto>)delegate(MunicipioDto municipio)
			{
				Municipio = municipio;
			}, true);
			PesquisarCepCommand = (ICommand)new DelegateCommand((Action)PesquisarCep);
			ConsultaCepServiceFactory.SetCurrent((IConsultaCepServiceFactory)new ConsultaViaCepServiceFactory());
			ConsultaCepServiceFactory.SetWebProxy(DadosSistema.ObterProxy());
			_consultaCepService = ConsultaCepServiceFactory.Create();
		}

		private void PesquisarCep()
		{
			try
			{
				OnConsultouCepEvent(_consultaCepService.ObterCep(HelperString.RemoveDiacritics(Cep)));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				Cep = string.Empty;
			}
		}

		private void OnConsultouCepEvent(CepInfo cepInfo)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Expected O, but got Unknown
			//IL_00eb: Expected O, but got Unknown
			//IL_00f0: Expected O, but got Unknown
			if (HelperObject.Is<CepInvalido>((object)cepInfo))
			{
				throw new Exception("CEP inválido ou não encontrado.");
			}
			Logradouro = cepInfo.Logradouro;
			Bairro = cepInfo.Bairro;
			Municipio = new MunicipioDto
			{
				Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Nome),
				CodigoIbge = cepInfo.Municipio.CodigoIbge,
				UF = new UFDto
				{
					Nome = cepInfo.Municipio.Uf.Nome,
					Sigla = cepInfo.Municipio.Uf.Sigla,
					CodigoIbge = cepInfo.Municipio.Uf.CodigoIbge,
					Pais = new PaisDto
					{
						Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Uf.Pais.Nome),
						CodigoBacen = cepInfo.Municipio.Uf.Pais.CodigoBacen
					}
				}
			};
			if (!string.IsNullOrWhiteSpace(Logradouro))
			{
				InformouCepComLogradouro = !string.IsNullOrWhiteSpace(Logradouro);
			}
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Bairro = base.Dto.Bairro;
			Cep = base.Dto.Cep;
			Numero = base.Dto.Numero;
			Logradouro = base.Dto.Logradouro;
			Complemento = base.Dto.Complemento;
			Municipio = base.Dto.Municipio;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			object obj = dto;
			PessoaDto val = (PessoaDto)((obj is PessoaDto) ? obj : null);
			if (HelperObject.IsNotNull((object)val))
			{
				val.EnderecoPrincipal = new EnderecoDto
				{
					Bairro = Bairro,
					Cep = Cep,
					Numero = Numero,
					Logradouro = Logradouro,
					Complemento = Complemento,
					Municipio = Municipio
				};
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)this).Dispose();
			BuscarMunicipioCommandParameter = null;
			Municipio = null;
		}
	}
	public class DadosMovimentacaoViewModel : ViewModelPartialBase<MovimentacaoDto>
	{
		public SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>> ObservacaoCommandParameter => new SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>>("ObservacaoDadoAdicionalListaView", (Action<IEnumerable<ObservacaoDadoAdicionalDto>>)delegate(IEnumerable<ObservacaoDadoAdicionalDto> observacao)
		{
			if (!string.IsNullOrEmpty(Observacao))
			{
				Observacao = Observacao.TrimEnd() + " ";
			}
			Observacao += string.Join(" ", observacao.Select((ObservacaoDadoAdicionalDto x) => x.Conteudo));
		}, true);

		public string Observacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Observacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Observacao), value);
			}
		}

		public Lista<CodigoDescricaoDto> Prioridades
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Prioridades));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Prioridades), value);
			}
		}

		public DadosMovimentacaoViewModel(MovimentacaoDto dto)
			: base(dto)
		{
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Observacao = base.Dto.Observacao;
			Prioridades = new Lista<CodigoDescricaoDto>();
			IEnumerable<CodigoDescricaoDto> enumerable = CodigoDescricaoProjection.ProjectedAsCollectionDto((IEnumerable<CodigoDescricao>)Prioridades.Dados);
			Prioridades.Adicionar(enumerable);
			Prioridades.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == 2));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is MovimentacaoDto) ? obj : null);
			((MovimentacaoDto)obj2).Observacao = Observacao;
			((MovimentacaoDto)obj2).Prioridade = Prioridades.ItemSelecionado;
		}
	}
	public class DadosPessoaViewModel : ViewModelPartialBase<PessoaDto>
	{
		private EnderecoDto EnderecoEntregaSelecionado => EnderecosEntrega?.ItemSelecionado ?? base.Dto.EnderecoEntregaPrincipal;

		public Lista<EnderecoDto> EnderecosEntrega
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<EnderecoDto>>((Expression<Func<Lista<EnderecoDto>>>)(() => EnderecosEntrega));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<EnderecoDto>>((Expression<Func<Lista<EnderecoDto>>>)(() => EnderecosEntrega), value);
			}
		}

		public Visibility MostrarEnderecosEntrega
		{
			get
			{
				if (!HelperObject.IsNotNull((object)EnderecosEntrega) || EnderecosEntrega.Numero <= 1)
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public string Id { get; set; }

		public TimeSpan Versao { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'nome' é obrigatório")]
		public string Nome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Nome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Nome), value);
			}
		}

		public string Documento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Documento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Documento), value);
			}
		}

		public Lista<TipoDocumento> TiposDocumento
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TipoDocumento>>((Expression<Func<Lista<TipoDocumento>>>)(() => TiposDocumento));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TipoDocumento>>((Expression<Func<Lista<TipoDocumento>>>)(() => TiposDocumento), value);
			}
		}

		[Valid]
		public DadosTelefoneViewModel DadosTelefoneViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosTelefoneViewModel>((Expression<Func<DadosTelefoneViewModel>>)(() => DadosTelefoneViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosTelefoneViewModel>((Expression<Func<DadosTelefoneViewModel>>)(() => DadosTelefoneViewModel), value);
			}
		}

		[Valid]
		public DadosEnderecoViewModel DadosEnderecoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => DadosEnderecoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => DadosEnderecoViewModel), value);
			}
		}

		public DadosPessoaViewModel(PessoaDto dto)
			: base(dto)
		{
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Nome = base.Dto.Nome;
			Id = ((EntityBaseDto)base.Dto).Id;
			Versao = ((EntityVersaoDto)base.Dto).Versao;
			TiposDocumento = new Lista<TipoDocumento>();
			TiposDocumento.Adicionar((TipoDocumento)new CPF());
			TiposDocumento.Adicionar((TipoDocumento)new CNPJ());
			TiposDocumento.Posicionar((Expression<Func<TipoDocumento, bool>>)((TipoDocumento x) => x.GetType() == ((((ViewModelPartialBase<PessoaDto>)this).Dto.CpfCnpj.Length == 14) ? typeof(CNPJ) : typeof(CPF))));
			Documento = base.Dto.CpfCnpj;
			DadosTelefoneViewModel = new DadosTelefoneViewModel(base.Dto.Celulares.Any() ? base.Dto.Celulares.FirstOrDefault() : (HelperObject.Is<TelefoneDtoNull>((object)base.Dto.TelefoneWhatsApp) ? base.Dto.TelefonePrincipal : base.Dto.TelefoneWhatsApp));
			MontarEnderecosEntrega();
			DadosEnderecoViewModel = new DadosEnderecoViewModel(EnderecoEntregaSelecionado);
			((BindableBase)DadosTelefoneViewModel).PropertyChanged += DadosTelefoneViewModelOnPropertyChanged;
			((BindableBase)DadosEnderecoViewModel).PropertyChanged += DadosEnderecoViewModelOnPropertyChanged;
		}

		private void MontarEnderecosEntrega()
		{
			List<EnderecoDto> list = (from x in HelperIEnumerable.ToListEmptyWhenNull<EnderecoDto>(base.Dto.EnderecosEntrega)
				where !HelperObject.Is<EnderecoDtoNull>((object)x)
				select x).ToList();
			DisposeEnderecosEntrega();
			EnderecosEntrega = new Lista<EnderecoDto>();
			EnderecosEntrega.AdicionarSemPosicionar((IEnumerable<EnderecoDto>)(list.Any() ? list : new List<EnderecoDto> { base.Dto.EnderecoEntregaPrincipal }));
			EnderecosEntrega.PosicionarNoPrimeiro();
			EnderecosEntrega.AlterouItemSelecionadoEvent += EnderecosEntregaOnAlterouItemSelecionadoEvent;
		}

		private void EnderecosEntregaOnAlterouItemSelecionadoEvent(object sender, EnderecoDto enderecoDto)
		{
			if (!HelperObject.IsNull((object)DadosEnderecoViewModel) && !HelperObject.IsNull((object)enderecoDto))
			{
				((ViewModelPartialBase<EnderecoDto>)DadosEnderecoViewModel).AtualizarInformacoes(enderecoDto);
				((BindableBase)this).RaisePropertyChanged<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => DadosEnderecoViewModel));
			}
		}

		private void DisposeEnderecosEntrega()
		{
			if (!HelperObject.IsNull((object)EnderecosEntrega))
			{
				EnderecosEntrega.AlterouItemSelecionadoEvent -= EnderecosEntregaOnAlterouItemSelecionadoEvent;
				((ViewModelValidationBase)EnderecosEntrega).Dispose();
			}
		}

		private void DadosEnderecoViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => DadosEnderecoViewModel));
		}

		private void DadosTelefoneViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<DadosTelefoneViewModel>((Expression<Func<DadosTelefoneViewModel>>)(() => DadosTelefoneViewModel));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Expected O, but got Unknown
			object obj = dto;
			PessoaDto val = (PessoaDto)((obj is PessoaDto) ? obj : null);
			val.Nome = Nome;
			((EntityBaseDto)val).Id = Id;
			if (!string.IsNullOrWhiteSpace(Documento))
			{
				if (TiposDocumento.ItemSelecionado.GetType() == typeof(CPF))
				{
					val.Cpf = Documento;
				}
				else if (HelperObject.Is<JuridicaDtoNull>((object)val.Juridica))
				{
					val.Juridica = new JuridicaDto
					{
						Cnpj = Documento,
						NomeFantasia = Nome
					};
				}
				else
				{
					val.Juridica.Cnpj = Documento;
					val.Juridica.NomeFantasia = Nome;
				}
			}
			if (HelperObject.Is<ClassificacaoDtoNull>((object)val.Classificacao))
			{
				val.Classificacao = base.Dto.Classificacao;
			}
			val.Ie = base.Dto.Ie;
			val.EmailPrincipal = base.Dto.EmailPrincipal;
			((ViewModelPartialBase<EnderecoDto>)DadosEnderecoViewModel).AtualizarDto<PessoaDto>(ref val);
			((ViewModelPartialBase<TelefoneDto>)DadosTelefoneViewModel).AtualizarDto<PessoaDto>(ref val);
		}

		public override void Dispose()
		{
			DisposeEnderecosEntrega();
			((BindableBase)DadosTelefoneViewModel).PropertyChanged -= DadosTelefoneViewModelOnPropertyChanged;
			((ViewModelValidationBase)this).Dispose();
			((ViewModelValidationBase)DadosTelefoneViewModel).Dispose();
			DadosTelefoneViewModel = null;
			((ViewModelValidationBase)DadosEnderecoViewModel).Dispose();
			DadosEnderecoViewModel = null;
		}
	}
	public class DadosTelefoneViewModel : ViewModelPartialBase<TelefoneDto>
	{
		[Required(ErrorMessage = "O campo 'telefone' é obrigatório", AllowEmptyStrings = false)]
		public string Telefone
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Telefone));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Telefone), value);
			}
		}

		public DadosTelefoneViewModel(TelefoneDto dto)
			: base(dto)
		{
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Telefone = base.Dto.Numero;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			object obj = dto;
			PessoaDto val = (PessoaDto)((obj is PessoaDto) ? obj : null);
			if (HelperObject.IsNull((object)val) || string.IsNullOrEmpty(Telefone))
			{
				val.TelefonePrincipal = (TelefoneDto)new TelefoneDtoNull();
				return;
			}
			val.TelefonePrincipal = new TelefoneDto
			{
				Numero = Telefone
			};
		}
	}
	public class DadosUltimaMovimentacao : ViewModelPartialBase<MovimentacaoDto>
	{
		public DateTime DataEmissao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataEmissao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataEmissao), value);
			}
		}

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value);
			}
		}

		public string Observacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Observacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Observacao), value);
			}
		}

		public ObservableCollection<ItemDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ItemDto>>((Expression<Func<ObservableCollection<ItemDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ItemDto>>((Expression<Func<ObservableCollection<ItemDto>>>)(() => Itens), value);
			}
		}

		public DadosUltimaMovimentacao(MovimentacaoDto dto)
			: base(dto)
		{
		}

		protected override void ProjectedAsPartialViewModel()
		{
			DataEmissao = ((MovimentacaoBaseDto)base.Dto).DataHoraEmissao;
			Total = ((MovimentacaoBaseDto)base.Dto).Total;
			Observacao = base.Dto.Observacao;
			Itens = new ObservableCollection<ItemDto>(base.Dto.Itens);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
		}
	}
	public class NavegacaoNaFicha<TDto, TNavigationBase> : ViewModelDtoBase<TDto> where TDto : class where TNavigationBase : NavigationBase
	{
		private Action _callbackNavegar;

		private ISelecaoCommandParameter _navegarCommandParameter;

		private TNavigationBase Navigation { get; }

		public NavegacaoNaFicha(TDto dto, TNavigationBase navigation)
			: base(dto)
		{
			Navigation = navigation;
			((NavigationBase)Navigation).ParametroInformado += OnParametroInformado;
		}

		public void Navegar(object parametro)
		{
			ISelecaoCommandParameter val = (ISelecaoCommandParameter)((parametro is ISelecaoCommandParameter) ? parametro : null);
			if (val == null)
			{
				throw new Exception("Parâmetros do comando inválidos");
			}
			Navegar(val.View, delegate
			{
			}, val.Parametros);
		}

		private void Navegar(string view, Action<NavigationResult> navigationResultAction, NavigationParameters navigationParameters)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			NavigationParameters val = (NavigationParameters)(((object)navigationParameters) ?? ((object)new NavigationParameters()));
			((NavigationBase)Navigation).Navegar(view, navigationResultAction, val);
		}

		protected void Navegar(ISelecaoCommandParameter commandParameter, Action callback)
		{
			_callbackNavegar = callback;
			_navegarCommandParameter = commandParameter;
			if (VerificarNavegandoProdutoServicoListaView())
			{
				AssinarEventoFimNavegacaoProdutoServicoListaView();
			}
			else
			{
				NavegarEstoqueView();
			}
		}

		private bool VerificarNavegandoProdutoServicoListaView()
		{
			return ((NavigationBase)Navigation).NavigationService.Journal.CurrentEntry.Uri.ToString() == "ProdutoServicoListaView";
		}

		private void AssinarEventoFimNavegacaoProdutoServicoListaView()
		{
			((NavigationBase)Navigation).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			if (!(regionNavigationEventArgs.Uri.ToString() == "ProdutoServicoListaView") && !HelperObject.IsNull((object)_navegarCommandParameter))
			{
				((NavigationBase)Navigation).NavigationService.Navigated -= NavigationServiceOnNavigated;
				NavegarEstoqueView();
			}
		}

		protected void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			ProcessarCallbackEstoqueView();
		}

		private void ProcessarCallbackEstoqueView()
		{
			if (!HelperObject.IsNotNull((object)_navegarCommandParameter))
			{
				_callbackNavegar?.Invoke();
				_callbackNavegar = null;
			}
		}

		private void NavegarEstoqueView()
		{
			Navegar(_navegarCommandParameter.View, delegate
			{
				_navegarCommandParameter = null;
			}, _navegarCommandParameter.Parametros);
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.Deliveries.ViewModels.Base.Documentos
{
	public abstract class TipoDocumento
	{
		public abstract string Mascara { get; }

		public abstract string Descricao { get; }

		public abstract int Codigo { get; }
	}
	public class CPF : TipoDocumento
	{
		public override string Mascara => "000.000.000-00";

		public override string Descricao => "C.P.F";

		public override int Codigo => 1;
	}
	public class CNPJ : TipoDocumento
	{
		public override string Mascara => "AA.AAA.AAA/AAAA-AA";

		public override string Descricao => "C.N.P.J";

		public override int Codigo => 2;
	}
}
namespace Otus.Modulo.Deliveries.ViewModels.Atendimentos
{
	public interface IBasePagamento
	{
		int Codigo { get; }

		string Descricao { get; }
	}
	public class Dinheiro : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		private readonly Func<decimal> _retornarTotalComFrete;

		public decimal ValorParaCalculoTroco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorParaCalculoTroco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorParaCalculoTroco), value, (Action)OnAlterouValorParaCalculoTroco);
			}
		}

		public decimal TrocoCalculado
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TrocoCalculado));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TrocoCalculado), value);
			}
		}

		public Visibility TrocoCalculadoVisivel
		{
			get
			{
				if (!(TrocoCalculado > 0m))
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public int Codigo { get; }

		public string Descricao { get; }

		public Dinheiro(Func<decimal> retornarTotalComFrete)
		{
			_retornarTotalComFrete = retornarTotalComFrete;
			Codigo = 0;
			Descricao = "Dinheiro";
		}

		private void OnAlterouValorParaCalculoTroco()
		{
			decimal num = _retornarTotalComFrete();
			TrocoCalculado = ValorParaCalculoTroco - num;
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => TrocoCalculadoVisivel));
		}

		public override void ProjectedAsViewModel()
		{
			ValorParaCalculoTroco = base.Dto.ValorParaCalculoTroco;
		}
	}
	public class Cartao : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Cartao()
		{
			Codigo = 1;
			Descricao = "Cartão";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class Pix : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Pix()
		{
			Codigo = 3;
			Descricao = "PIX";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class Outros : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Outros()
		{
			Codigo = 2;
			Descricao = "Outros";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class IdentificacaoChamadaAbaViewModel : IAbaViewModelBase, IDisposable
	{
		public string Titulo => "IDENTIFICAÇÃO DO CLIENTE";

		public short? Numero { get; }

		public string SubTitulo => "Identificar chamadas";

		public bool Valido => ViewModel.Valido;

		public IViewModelBase ViewModel { get; }

		public IdentificacaoChamadaAbaViewModel(IViewModelBase vm)
		{
			ViewModel = vm;
		}

		public void Dispose()
		{
			ViewModel.Dispose();
		}

		public IAbaViewModelBase SetarNumero(short numero)
		{
			return (IAbaViewModelBase)(object)this;
		}
	}
	public class IdentificacaoChamadaViewModel : NavegacaoNaFicha<MovimentacaoDto, NavigationBase>
	{
		private readonly IWService _service;

		private readonly INotificacao _notificacao;

		private readonly IIdentificadorDeChamadaService _identificadorDeChamadaService;

		private readonly IInteracaoBarraTarrefaService _interacaoBarraTarrefaService;

		private bool _pedidoAberto;

		public string NumeroEmEspera;

		private bool _recebendoChamada;

		public ICommand RemoverTabelaPrecoCommand { get; set; }

		public virtual SelecaoCommandParameter<TabelaPrecoDto> TabelaPrecoCommandParameter => new SelecaoCommandParameter<TabelaPrecoDto>("TabelaPrecoListaView", (Action<TabelaPrecoDto>)delegate(TabelaPrecoDto tabelaPreco)
		{
			TabelaPreco = tabelaPreco;
			VerificarRecalcularComNovaTabelaPreco(tabelaPreco);
		}, true);

		public ICommand RemoverVendedorCommand { get; set; }

		public ISelecaoCommandParameter VendedorCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)CallbackSetVendedor, (ValidateParameter<PessoaDto>)(object)ValidacaoVendedor);

		private ValidateParameterPessoa<PessoaDto> ValidacaoVendedor => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => HelperObject.IsExactlyObject<VendedorDto>((object)pessoaDto.Fisica.Vendedor)), Messages.CadastroTipoVendedor, (TipoPessoaCadastro)6, HorizontalAlignment.Center);

		public ICommand PesquisarVendedorCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarVendedor);

		public bool MostrarTabelaPrecoVendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarTabelaPrecoVendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarTabelaPrecoVendedor), value);
			}
		}

		public SelecaoCommandParameter<PessoaDto> PessoaCommandParameter { get; set; }

		public ICommand GravarPessoaCommand { get; set; }

		public ICommand MostrarHistoricoCommand { get; set; }

		public ICommand PesquisarPessoaCommand { get; set; }

		[Valid]
		public DadosPessoaViewModel DadosPessoaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosPessoaViewModel>((Expression<Func<DadosPessoaViewModel>>)(() => DadosPessoaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosPessoaViewModel>((Expression<Func<DadosPessoaViewModel>>)(() => DadosPessoaViewModel), value);
			}
		}

		public TabelaPrecoDto TabelaPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value);
			}
		}

		public PessoaDto Vendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor), value, (Action)delegate
				{
					NomeVendedor = ObterNomeVendedor();
				});
			}
		}

		public string NomeVendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeVendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeVendedor), value, (Action)RemoverVendedorQuandoNomeApagado);
			}
		}

		public DadosUltimaMovimentacao DadosUltimaMovimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosUltimaMovimentacao>((Expression<Func<DadosUltimaMovimentacao>>)(() => DadosUltimaMovimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosUltimaMovimentacao>((Expression<Func<DadosUltimaMovimentacao>>)(() => DadosUltimaMovimentacao), value);
			}
		}

		public DadosMovimentacaoViewModel DadosMovimentacaoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosMovimentacaoViewModel>((Expression<Func<DadosMovimentacaoViewModel>>)(() => DadosMovimentacaoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosMovimentacaoViewModel>((Expression<Func<DadosMovimentacaoViewModel>>)(() => DadosMovimentacaoViewModel), value);
			}
		}

		public string Informacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Informacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Informacao), value);
			}
		}

		public string EstadoDaAnimacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => EstadoDaAnimacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => EstadoDaAnimacao), value);
			}
		}

		public event EventHandler<TabelaPrecoDto> AlterouTabelaPrecoCalcularItensEvent;

		public event EventHandler<PessoaDto> AlterouVendedorEvent;

		public event EventHandler<KeyValuePair<MovimentacaoDto, bool>> IniciouNovaMovimentacao;

		public IdentificacaoChamadaViewModel(IWService service, MovimentacaoDto dto, NavigationBase navigation, INotificacao notificacao, IIdentificadorDeChamadaService identificadorDeChamadaService, IInteracaoBarraTarrefaService interacaoBarraTarrefaService)
			: base(dto, navigation)
		{
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Expected O, but got Unknown
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_0120: Expected O, but got Unknown
			_service = service;
			_notificacao = notificacao;
			_identificadorDeChamadaService = identificadorDeChamadaService;
			_interacaoBarraTarrefaService = interacaoBarraTarrefaService;
			PesquisarPessoaCommand = (ICommand)new DelegateCommand((Action)PesquisarPessoa, (Func<bool>)PodePesquisarPessoa, (bool?)null);
			GravarPessoaCommand = (ICommand)new DelegateCommand((Action)GravarPessoa, (Func<bool>)PodeGravarPessoa, (bool?)null);
			MostrarHistoricoCommand = (ICommand)new DelegateCommand((Action)MostrarHistorico);
			DadosUltimaMovimentacao = new DadosUltimaMovimentacao((MovimentacaoDto)new MovimentacaoDtoNull());
			EstadoDaAnimacao = "0";
			_identificadorDeChamadaService.RecebendoChamadaEvent += IdentificadorDeChamadaServiceOnRecebendoChamadaEvent;
			RemoverTabelaPrecoCommand = (ICommand)new DelegateCommand((Action)RemoverTabelaPreco, (Func<bool>)PodeRemoverTabelaPreco, (bool?)null);
			RemoverVendedorCommand = (ICommand)new DelegateCommand((Action)RemoverVendedor, (Func<bool>)PodeRemoverVendedor, (bool?)null);
		}

		private async void PesquisarPessoa()
		{
			try
			{
				PesquisaDto objetoPesquisaTodos = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
					{
						Informacao = HelperString.ToStringEmptyWhenNull(Informacao),
						TipoPessoaCadastro = (TipoPessoaCadastro)11
					},
					Pagina = 1,
					Tamanho = 100
				};
				ProcessarRetornoPesquisa((await _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisaTodos)))).Lista.OfType<PessoaDto>());
			}
			catch (Exception ex)
			{
				_pedidoAberto = false;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private async void ProcessarRetornoPesquisa(IEnumerable<PessoaDto> resultado)
		{
			List<PessoaDto> list = HelperIEnumerable.ToListEmptyWhenNull<PessoaDto>(resultado);
			if (!list.Any())
			{
				IniciarMovimentacaoComNovoCadastroDePessoa();
				return;
			}
			if (list.Count == 1)
			{
				await SelecionouPessoa(list.FirstOrDefault());
				return;
			}
			PessoaCommandParameter = new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)CallbackSelecaoPessoaCommandParameter, true);
			PessoaCommandParameter.Parametros.Add("InformacaoPesquisa", (object)Informacao);
			Navegar(PessoaCommandParameter);
		}

		protected async void CallbackSelecaoPessoaCommandParameter(PessoaDto pessoa)
		{
			await SelecionouPessoa(pessoa);
		}

		private bool PodePesquisarPessoa()
		{
			return !_pedidoAberto;
		}

		private bool PodeGravarPessoa()
		{
			return ((Validation)this).Valid;
		}

		private async void GravarPessoa()
		{
			_ = 1;
			try
			{
				PessoaDto pessoaDto = ((ViewModelPartialBase<PessoaDto>)DadosPessoaViewModel).Dto;
				((ViewModelPartialBase<PessoaDto>)DadosPessoaViewModel).AtualizarDto<PessoaDto>(ref pessoaDto);
				PessoaDto val;
				if (string.IsNullOrEmpty(((EntityBaseDto)pessoaDto).Id))
				{
					val = await _service.Canal<IPessoaModuleService, Task<PessoaDto>>((Func<IPessoaModuleService, Task<PessoaDto>>)((IPessoaModuleService service) => ((IAdicionar<PessoaDto>)(object)service).AdicionarAsync(pessoaDto)));
					_notificacao.MostrarNotificacao("Pessoa cadastrada com sucesso!");
				}
				else
				{
					val = await _service.Canal<IPessoaModuleService, Task<PessoaDto>>((Func<IPessoaModuleService, Task<PessoaDto>>)((IPessoaModuleService service) => ((IAtualizar<PessoaDto>)(object)service).AtualizarAsync(pessoaDto)));
					_notificacao.MostrarNotificacao("Pessoa atualizada com sucesso!");
				}
				((ViewModelPartialBase<PessoaDto>)DadosPessoaViewModel).AtualizarInformacoes(val);
				OnIniciouNovaMovimentacao(((ViewModelPartialBase<MovimentacaoDto>)DadosMovimentacaoViewModel).Dto, replicando: false);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void DadosPessoaViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<DadosPessoaViewModel>((Expression<Func<DadosPessoaViewModel>>)(() => DadosPessoaViewModel));
		}

		private void IdentificadorDeChamadaServiceOnRecebendoChamadaEvent(object sender, string numero)
		{
			if (!_recebendoChamada && !string.IsNullOrEmpty(numero))
			{
				NumeroEmEspera = numero;
				if (!_pedidoAberto)
				{
					IniciarAtendimento(numero);
				}
			}
		}

		public void IniciarAtendimento(string numero)
		{
			_recebendoChamada = true;
			MessageBoxResult num = DialogoFactory.MostrarDialogoSimNao($"Nova ligação de: {numero}. Deseja abrir um pedido agora?", false, 0, HorizontalAlignment.Center);
			_recebendoChamada = false;
			if (num == MessageBoxResult.Yes)
			{
				_interacaoBarraTarrefaService.ChamarModulo(typeof(DeliveryViewModel));
				Informacao = numero;
				PesquisarPessoa();
				NumeroEmEspera = string.Empty;
			}
		}

		private void MostrarHistorico()
		{
			EstadoDaAnimacao = ((EstadoDaAnimacao == "0") ? "1" : "0");
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
			VerificarRecalcularComNovaTabelaPreco(TabelaPreco);
		}

		private bool PodeRemoverTabelaPreco()
		{
			return !HelperObject.Is<TabelaPrecoDtoNull>((object)TabelaPreco);
		}

		protected void VerificarRecalcularComNovaTabelaPreco(TabelaPrecoDto tabelaPrecoDto)
		{
			if (((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens.Any() && DialogoFactory.MostrarDialogoSimNao("Deseja recalcular os preços dos itens da movimentação?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.No)
			{
				OnAlterouTabelaPrecoCalcularItensEvent(tabelaPrecoDto);
			}
		}

		private void OnAlterouTabelaPrecoCalcularItensEvent(TabelaPrecoDto e)
		{
			this.AlterouTabelaPrecoCalcularItensEvent?.Invoke(this, e);
		}

		private bool PodeRemoverVendedor()
		{
			return !HelperObject.Is<PessoaDtoNull>((object)Vendedor);
		}

		private void RemoverVendedor()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = (PessoaDto)new PessoaDtoNull();
			}
		}

		private async void PesquisarVendedor(ICommand navegarCommand)
		{
			string nomeVendedor = NomeVendedor;
			Action<PessoaDto> action = CallbackSetVendedor;
			Action<ISelecaoCommandParameter> obj = delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			};
			ValidateParameterPessoa<PessoaDto> validacaoVendedor = ValidacaoVendedor;
			PessoaDto vendedor = Vendedor;
			await PesquisaPessoa.ExecutarAsync(nomeVendedor, (TipoPessoaCadastro)6, action, obj, true, validacaoVendedor, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (vendedor != null) ? ((EntityBaseDto)vendedor).Id : null);
		}

		private void CallbackSetVendedor(PessoaDto dto)
		{
			try
			{
				CallbackSetVendedorEx.ValidarPin(dto, (Action)delegate
				{
					SetarVendedor(dto);
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void SetarVendedor(PessoaDto dto)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || !(((EntityBaseDto)Vendedor).Id != ((EntityBaseDto)dto).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = dto;
				if (((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens.Any() && DialogoFactory.MostrarDialogoSimNao("Deseja atribuir o vendedor selecionado para os produtos que não possuem vendedor?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					OnAlterouVendedorEvent(dto);
				}
			}
		}

		private void OnAlterouVendedorEvent(PessoaDto e)
		{
			this.AlterouVendedorEvent?.Invoke(this, e);
		}

		protected virtual async Task SelecionouPessoa(PessoaDto pessoa)
		{
			try
			{
				if (HelperObject.IsNotNull((object)pessoa.Segmentacao) && HelperObject.IsNot<SegmentacaoDtoNull>((object)pessoa.Segmentacao) && pessoa.Segmentacao.AlertarOperadorAoIndicarNaMovimentacao)
				{
					DialogoFactory.MostrarDialogoConfirma($"Você está indicando uma pessoa que possui segmentação:\n{pessoa.Segmentacao.SegmentacaoToText}", false, HorizontalAlignment.Left);
				}
				PessoaDto pessoaDtoAtualizado = default(PessoaDto);
				_ = pessoaDtoAtualizado;
				pessoaDtoAtualizado = await _service.Canal<IPessoaModuleService, Task<PessoaDto>>((Func<IPessoaModuleService, Task<PessoaDto>>)((IPessoaModuleService service) => ((IReferenciaAtualizarService<PessoaDto>)(object)service).AtualizarDtoAsync(pessoa)));
				if (pessoaDtoAtualizado.Cliente.ValorEmAtraso > 0m)
				{
					if (DadosSistema.ConfiguracaoDto.Saida.AlertarClienteInadimplente.Valor && DialogoFactory.MostrarDialogoConfirmaInformacaoOculta(pessoaDtoAtualizado.Cliente.AvisoValorEmAtraso, "Situação 1. Verifique!", false) != MessageBoxResult.OK)
					{
						pessoa = (PessoaDto)new PessoaDtoNull();
						return;
					}
					if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderParaInadimplente>(0m, (string)null) == null)
					{
						pessoa = (PessoaDto)new PessoaDtoNull();
						return;
					}
				}
				_pedidoAberto = true;
				DeliveryDto val = await _service.Canal<IDeliveryModuleService, Task<DeliveryDto>>((Func<IDeliveryModuleService, Task<DeliveryDto>>)((IDeliveryModuleService service) => service.ObterDadosInicializacaoDeliveryAsync(((EntityBaseDto)pessoaDtoAtualizado).Id)));
				if (!HelperString.IsNullOrEmpty(pessoa.IndicadorOperacao.Descricao))
				{
					val.NovaMovimentacao.IndicadorOperacao = pessoa.IndicadorOperacao;
				}
				else if (!HelperObject.IsExactlyObject<EstrangeiroDto>((object)pessoa.Estrangeiro) && (pessoa.Classificacao.Codigo == 6 || pessoa.Classificacao.Codigo == 7 || pessoa.Classificacao.Codigo == 8))
				{
					val.NovaMovimentacao.IndicadorOperacao = CodigoDescricaoProjection.ProjectedAsDto((CodigoDescricao)(object)IndicadoresOperacaoConsumidorFinal.ObterConsumidorFinal());
				}
				else
				{
					val.NovaMovimentacao.IndicadorOperacao = CodigoDescricaoProjection.ProjectedAsDto((CodigoDescricao)(object)IndicadoresOperacaoConsumidorFinal.ObterNormal());
				}
				if (val.NovaMovimentacao.IndicadorPresencaComprador.Codigo != 5)
				{
					MovimentacaoDto novaMovimentacao = val.NovaMovimentacao;
					EnderecoDto enderecoPrincipal = pessoa.EnderecoPrincipal;
					object obj;
					if (enderecoPrincipal == null)
					{
						obj = null;
					}
					else
					{
						MunicipioDto municipio = enderecoPrincipal.Municipio;
						if (municipio == null)
						{
							obj = null;
						}
						else
						{
							UFDto uF = municipio.UF;
							obj = ((uF != null) ? uF.Sigla : null);
						}
					}
					EnderecoDto enderecoPrincipal2 = val.NovaMovimentacao.Empresa.EnderecoPrincipal;
					object obj2;
					if (enderecoPrincipal2 == null)
					{
						obj2 = null;
					}
					else
					{
						MunicipioDto municipio2 = enderecoPrincipal2.Municipio;
						if (municipio2 == null)
						{
							obj2 = null;
						}
						else
						{
							UFDto uF2 = municipio2.UF;
							obj2 = ((uF2 != null) ? uF2.Sigla : null);
						}
					}
					novaMovimentacao.IndicadorPresencaComprador = (((string)obj != (string)obj2) ? CodigoDescricaoProjection.ProjectedAsDto((CodigoDescricao)(object)IndicadoresPresencaComprador.ObterPorCodigo(9)) : CodigoDescricaoProjection.ProjectedAsDto((CodigoDescricao)(object)IndicadoresPresencaComprador.ObterPorCodigo(1)));
				}
				DadosPessoaViewModel = new DadosPessoaViewModel(pessoaDtoAtualizado);
				((BindableBase)DadosPessoaViewModel).PropertyChanged -= DadosPessoaViewModelOnPropertyChanged;
				((BindableBase)DadosPessoaViewModel).PropertyChanged += DadosPessoaViewModelOnPropertyChanged;
				TabelaPreco = (((EntityDto)pessoaDtoAtualizado.Cliente.TabelaPreco).Ativo ? pessoaDtoAtualizado.Cliente.TabelaPreco : val.NovaMovimentacao.TabelaPreco);
				Vendedor = val.NovaMovimentacao.VendedorResponsavel;
				if (!HelperObjectId.IsDefaultOrEmpty(HelperObjectId.TryParseReferencia(pessoaDtoAtualizado.Cliente.VendedorReferencia)))
				{
					PessoaDto val2 = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(pessoaDtoAtualizado.Cliente.VendedorReferencia)));
					Vendedor = (PessoaDto)(((EntityDto)val2).Ativo ? ((object)val2) : ((object)new PessoaDtoNull()));
				}
				MostrarTabelaPrecoVendedor = true;
				if ((HelperObject.Is<MovimentacaoDtoNull>((object)val.UltimaMovimentacao) ? MessageBoxResult.No : (DadosSistema.SolicitarReplicacaoUltimoDelivey ? DialogoFactory.MostrarDialogoSimNao("Deseja replicar o último pedido?", false, 0, HorizontalAlignment.Center) : MessageBoxResult.No)) == MessageBoxResult.Yes)
				{
					ConfiguracaoDepartamentoVenda val3 = DadosSistema.ObterDepartamentoVenda();
					foreach (ItemDto iten in val.UltimaMovimentacao.Itens)
					{
						iten.DepartamentoReferencia = val3.DepartamentoVendaReferencia;
					}
					((MovimentacaoBaseDto)val.UltimaMovimentacao).Pessoa = (PessoaDto)new PessoaDtoNull();
					DadosMovimentacaoViewModel = new DadosMovimentacaoViewModel(val.UltimaMovimentacao);
					OnIniciouNovaMovimentacao(val.UltimaMovimentacao, replicando: true);
					DadosUltimaMovimentacao = new DadosUltimaMovimentacao(val.UltimaMovimentacao);
				}
				else
				{
					DadosMovimentacaoViewModel = new DadosMovimentacaoViewModel(val.NovaMovimentacao);
					DadosUltimaMovimentacao = new DadosUltimaMovimentacao(val.UltimaMovimentacao);
					OnIniciouNovaMovimentacao(val.NovaMovimentacao, replicando: false);
				}
			}
			catch (Exception ex)
			{
				_pedidoAberto = false;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		public async void IniciarMovimentacaoComNovoCadastroDePessoa()
		{
			try
			{
				if (DialogoFactory.MostrarDialogoSimNao("Cliente não localizado, deseja fazer um novo cadastro?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					DeliveryDto val = await _service.Canal<IDeliveryModuleService, Task<DeliveryDto>>((Func<IDeliveryModuleService, Task<DeliveryDto>>)((IDeliveryModuleService service) => service.ObterNovaMovimentacaoAsync()));
					DadosPessoaViewModel = new DadosPessoaViewModel(new PessoaDto
					{
						Classificacao = new ClassificacaoDto
						{
							Codigo = 6
						},
						IndicadorOperacao = new CodigoDescricaoDto
						{
							Codigo = -1,
							Descricao = "Não definido"
						}
					});
					MostrarTabelaPrecoVendedor = true;
					if (HelperString.ApenasNumeros(Informacao).Length >= 8)
					{
						DadosPessoaViewModel.DadosTelefoneViewModel.Telefone = Informacao;
					}
					else
					{
						DadosPessoaViewModel.Nome = Informacao;
					}
					((BindableBase)DadosPessoaViewModel).PropertyChanged -= DadosPessoaViewModelOnPropertyChanged;
					((BindableBase)DadosPessoaViewModel).PropertyChanged += DadosPessoaViewModelOnPropertyChanged;
					DadosMovimentacaoViewModel = new DadosMovimentacaoViewModel(val.NovaMovimentacao);
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private string ObterNomeVendedor()
		{
			if (!HelperObject.IsNull((object)Vendedor) && !HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return Vendedor.Nome;
			}
			return string.Empty;
		}

		private void RemoverVendedorQuandoNomeApagado()
		{
			if (!HelperString.IsNullOrEmpty(NomeVendedor) || HelperObject.IsNull((object)Vendedor) || HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			RemoverVendedor();
			if (HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			NomeVendedor = Vendedor.Nome;
			((UpdateViewBindableBase)this).DormirAsync(50, (Action<object>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeVendedor));
			});
		}

		protected virtual void OnIniciouNovaMovimentacao(MovimentacaoDto e, bool replicando)
		{
			this.IniciouNovaMovimentacao?.Invoke(this, new KeyValuePair<MovimentacaoDto, bool>(e, replicando));
		}

		public override void Dispose()
		{
			if (DadosPessoaViewModel != null)
			{
				((BindableBase)DadosPessoaViewModel).PropertyChanged -= DadosPessoaViewModelOnPropertyChanged;
				((ViewModelValidationBase)DadosPessoaViewModel).Dispose();
				DadosPessoaViewModel = null;
			}
			_identificadorDeChamadaService.RecebendoChamadaEvent -= IdentificadorDeChamadaServiceOnRecebendoChamadaEvent;
			((ViewModelValidationBase)DadosUltimaMovimentacao).Dispose();
			DadosUltimaMovimentacao = null;
			DadosMovimentacaoViewModel dadosMovimentacaoViewModel = DadosMovimentacaoViewModel;
			if (dadosMovimentacaoViewModel != null)
			{
				((ViewModelValidationBase)dadosMovimentacaoViewModel).Dispose();
			}
			DadosMovimentacaoViewModel = null;
			PessoaCommandParameter = null;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			object obj = dto;
			MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			if (val != null)
			{
				val.TabelaPreco = (TabelaPrecoDto)(((object)TabelaPreco) ?? ((object)new TabelaPrecoDtoNull()));
				val.VendedorResponsavel = (PessoaDto)(((object)Vendedor) ?? ((object)new PessoaDtoNull()));
				PessoaDto pessoa = (PessoaDto)((!HelperObject.Is<PessoaDtoNull>((object)((MovimentacaoBaseDto)val).Pessoa)) ? ((object)((MovimentacaoBaseDto)val).Pessoa) : ((object)new PessoaDto()));
				((ViewModelPartialBase<PessoaDto>)DadosPessoaViewModel)?.AtualizarDto<PessoaDto>(ref pessoa);
				((MovimentacaoBaseDto)val).Pessoa = pessoa;
				((ViewModelPartialBase<MovimentacaoDto>)DadosMovimentacaoViewModel)?.AtualizarDto<MovimentacaoDto>(ref val);
				((MovimentacaoBaseDto)val).Tipo = (TipoMovimentacao)13;
				((MovimentacaoBaseDto)val).CalculadoPeloSistema = true;
				((MovimentacaoBaseDto)val).Modelo = new ModeloDocumentoFiscalDto();
				((MovimentacaoBaseDto)val).DataHoraEmissao = DateTime.Now;
				((ViewModelDtoBase<MovimentacaoDto>)this).AtualizarDto<MovimentacaoDto>(ref val);
			}
		}
	}
	public class Item : ViewModelPartialBase<ItemDto>
	{
		private bool _calculando;

		private readonly Action<ItemDto> _recalcularItem;

		private bool _inserindo = true;

		public bool Cancelado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Cancelado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Cancelado), value, (Action)delegate
				{
					base.Dto.Cancelado = Cancelado;
				});
			}
		}

		public int NumeroItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => NumeroItem));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => NumeroItem), value);
			}
		}

		public ProdutoServicoDto Produto
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoDto>((Expression<Func<ProdutoServicoDto>>)(() => Produto));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoDto>((Expression<Func<ProdutoServicoDto>>)(() => Produto), value);
			}
		}

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value, (Action)AlterouQuantidade);
			}
		}

		public decimal PrecoUnitario
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario), value);
			}
		}

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value);
			}
		}

		public string Observacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Observacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Observacao), value);
			}
		}

		public string VendedorToText
		{
			get
			{
				if (Vendedor != null && !HelperObject.Is<PessoaDtoNull>((object)Vendedor))
				{
					return $"Vendedor: {Vendedor.Nome}";
				}
				return string.Empty;
			}
		}

		public PessoaDto Vendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => VendedorToText));
				});
			}
		}

		public decimal PercentualComissao
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PercentualComissao));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PercentualComissao), value);
			}
		}

		public decimal Desconto
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Desconto));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Desconto), value);
			}
		}

		public decimal Acrescimo
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Acrescimo));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Acrescimo), value);
			}
		}

		public bool PodeInformarQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade), value);
			}
		}

		public ICommand AdicionarQuantidadeCommand { get; set; }

		public ICommand AdicionarDecimoDeQuantidadeCommand { get; set; }

		public ICommand RemoverQuantidadeCommand { get; set; }

		public ICommand RemoverDecimoDeQuantidadeCommand { get; set; }

		public Item(ItemDto dto, Action<ItemDto> recalcularItem)
			: base(dto)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Expected O, but got Unknown
			_recalcularItem = recalcularItem;
			AdicionarQuantidadeCommand = (ICommand)new DelegateCommand((Action)AdicionarQuantidade, (Func<bool>)PodeAlterarQuantidade, (bool?)null);
			AdicionarDecimoDeQuantidadeCommand = (ICommand)new DelegateCommand((Action)AdicionarDecimoDeQuantidade, (Func<bool>)PodeAlterarQuantidade, (bool?)null);
			RemoverQuantidadeCommand = (ICommand)new DelegateCommand((Action)RemoverQuantidade, (Func<bool>)PodeAlterarQuantidade, (bool?)null);
			RemoverDecimoDeQuantidadeCommand = (ICommand)new DelegateCommand((Action)RemoverDecimoDeQuantidade, (Func<bool>)PodeAlterarQuantidade, (bool?)null);
		}

		public void AlterarObservacao()
		{
			string observacao = Observacao;
			if (DialogoFactory.MostrarDialogoEntradaString(ref observacao, "Observação", "Informe a observação", (IIcone)null, false, false, false, observacao, false, true) == MessageBoxResult.OK)
			{
				Observacao = observacao;
			}
		}

		public void AlterarPrecoUnitario()
		{
			if (base.Dto.LimiteDesconto > 0m)
			{
				DialogoFactory.MostrarDialogoConfirma("Alteração de preço não permitida para item com limite de desconto", false, HorizontalAlignment.Center);
				return;
			}
			if (DadosSistema.LimiteDescontoUsuario > 0m)
			{
				DialogoFactory.MostrarDialogoConfirma("Alteração de preço não permitida usuário com limite de desconto", false, HorizontalAlignment.Center);
				return;
			}
			try
			{
				decimal num = default(decimal);
				if (DialogoFactory.MostrarDialogoEntradaDecimal(ref num, "Valor", "Informe o preço de venda unitário", HorizontalAlignment.Center) == MessageBoxResult.OK && !(num <= 0m))
				{
					if (Desconto > 0m || Acrescimo > 0m)
					{
						DialogoFactory.MostrarDialogoConfirma("Desconto/acréscimo do item será removido", false, HorizontalAlignment.Center);
						Desconto = 0m;
						Acrescimo = 0m;
					}
					PrecoUnitario = num;
					ObterItemCalculado();
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma($"Falha ao alterar preço de venda do item {Environment.NewLine}{ex.Message}", false, HorizontalAlignment.Center);
			}
		}

		public void AlterarVendedor()
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			IEnumerable<PessoaDto> vendedores = ItensViewModel.GetVendedores();
			if (!vendedores.Any())
			{
				return;
			}
			List<PessoaDto> list = new List<PessoaDto>
			{
				new PessoaDto
				{
					Nome = "Nenhum"
				}
			};
			list.AddRange(vendedores);
			PessoaDto vendedor = DialogoFactory.MostrarDialogoComboBox<PessoaDto>("Informe o vendedor", "Vendedor", (IEnumerable<PessoaDto>)list, "Nome", true);
			PessoaDto obj = vendedor;
			if (string.IsNullOrEmpty((obj != null) ? ((EntityBaseDto)obj).Id : null))
			{
				return;
			}
			try
			{
				CallbackSetVendedorEx.ValidarPin(vendedor, (Action)delegate
				{
					SetarVendedor(vendedor);
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public void SetarVendedor(PessoaDto vendedor)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || !(((EntityBaseDto)Vendedor).Id != ((EntityBaseDto)vendedor).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = vendedor;
				VendedorDto vendedor2 = Vendedor.Fisica.Vendedor;
				if (vendedor2 != null && ((ColaboradorDto)vendedor2).PercentualComissao > default(decimal))
				{
					VendedorDto vendedor3 = Vendedor.Fisica.Vendedor;
					PercentualComissao = ((vendedor3 != null) ? ((ColaboradorDto)vendedor3).PercentualComissao : 0m);
				}
			}
		}

		public void Cancelar()
		{
			Cancelado = true;
		}

		private bool PodeAlterarQuantidade()
		{
			return PodeInformarQuantidade;
		}

		private void RemoverDecimoDeQuantidade()
		{
			if (!(Quantidade <= 0.1m))
			{
				Quantidade -= 0.1m;
			}
		}

		private void RemoverQuantidade()
		{
			if (!(Quantidade <= 1m))
			{
				Quantidade -= 1m;
			}
		}

		private void AdicionarDecimoDeQuantidade()
		{
			Quantidade += 0.1m;
		}

		private void AdicionarQuantidade()
		{
			Quantidade += 1m;
		}

		private void AlterouQuantidade()
		{
			Desconto = 0m;
			Acrescimo = 0m;
			ObterItemCalculado();
		}

		public void AplicarDesconto()
		{
			Desconto = DialogoFactory.MostrarDialogoDesconto(base.Dto.TotalLiquido + Desconto, 0m);
			ValidarDesconto();
		}

		public void AplicarAcrescimo()
		{
			Acrescimo = DialogoFactory.MostrarDialogoAcrescimo(base.Dto.TotalLiquido - Acrescimo);
			ValidarAcrescimo();
		}

		public void ObterItemCalculado()
		{
			if (!_calculando && !(Quantidade < 0m) && !_inserindo)
			{
				_calculando = true;
				ItemDto dto = base.Dto;
				((ViewModelPartialBase<ItemDto>)this).AtualizarDto<ItemDto>(ref dto);
				_recalcularItem?.Invoke(dto);
				base.Dto.DescontoDigitado = Desconto;
				base.Dto.TotalOutrasDespesas = Acrescimo;
				_calculando = false;
			}
		}

		private void ValidarDesconto()
		{
			if (Total == 0m || _calculando)
			{
				return;
			}
			if (Desconto >= Total || Desconto < 0m)
			{
				Desconto = 0m;
				_calculando = false;
				DialogoFactory.MostrarDialogoConfirma("Desconto inválido!", false, HorizontalAlignment.Center);
			}
			decimal num = Desconto * 100m / Total;
			decimal num2 = base.Dto.LimiteDesconto;
			if (DadosSistema.LimiteDescontoUsuario > num2)
			{
				num2 = DadosSistema.LimiteDescontoUsuario;
			}
			if (num2 > 0m && num > num2)
			{
				if (DialogoFactory.MostrarDialogoSimNao("Limite de desconto atingido. Continuar?", false, 0, HorizontalAlignment.Center).Equals(MessageBoxResult.Yes))
				{
					AutorizacaoDto val = DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderAcimaDoLimiteDeDesconto>(num, $"Item {base.Dto.ProdutoServico.Descricao}: de R$ {Total:N2} por R$ {Total - Desconto:N2} ({num:N2}% de desconto).");
					if (HelperObject.IsNotNull((object)val))
					{
						if (((object)val.Direito).GetType() != typeof(DireitoDtoNull) || val.LimiteDesconto >= num)
						{
							base.Dto.LimiteDesconto = val.LimiteDesconto;
							ObterItemCalculado();
							if (((object)val.Direito).GetType() != typeof(DireitoDtoNull))
							{
								base.Dto.LimiteDesconto = 99.99m;
							}
							return;
						}
						DialogoFactory.MostrarDialogoConfirma($"Excedido o limite de desconto do usuario informado : {val.LimiteDesconto}%", false, HorizontalAlignment.Center);
					}
					Desconto = 0m;
					_calculando = false;
					return;
				}
				Desconto = 0m;
				_calculando = false;
			}
			ObterItemCalculado();
		}

		private void ValidarAcrescimo()
		{
			if (_calculando || Total == 0m)
			{
				return;
			}
			if (Acrescimo <= 0m || Acrescimo > Total)
			{
				if (!(Acrescimo == 0m))
				{
					DialogoFactory.MostrarDialogoConfirma("Acréscimo inválido!", false, HorizontalAlignment.Center);
					Acrescimo = 0m;
				}
			}
			else
			{
				ObterItemCalculado();
			}
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Produto = base.Dto.ProdutoServico;
			NumeroItem = base.Dto.Numero;
			Quantidade = base.Dto.Quantidade;
			PrecoUnitario = base.Dto.PrecoUnitario;
			Total = base.Dto.TotalLiquido;
			Observacao = (Helper.IsEmpty((IEnumerable)base.Dto.ObservacaoDigitada) ? Observacao : base.Dto.ObservacaoDigitada);
			Vendedor = base.Dto.VendedorResponsavel;
			if (PercentualComissao == 0m)
			{
				PercentualComissao = base.Dto.PercentualComissao;
			}
			_inserindo = false;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is ItemDto) ? obj : null);
			((ItemDto)obj2).ProdutoServico = Produto;
			((ItemDto)obj2).Numero = NumeroItem;
			((ItemDto)obj2).Estoque.Quantidade = Quantidade;
			EstoqueDto estoque = ((ItemDto)obj2).Estoque;
			ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
			bool? obj3;
			if (configuracaoDto == null)
			{
				obj3 = null;
			}
			else
			{
				SaidaDto saida = configuracaoDto.Saida;
				if (saida == null)
				{
					obj3 = null;
				}
				else
				{
					BoleanoDto montarComposicaoAutomaticamente = saida.MontarComposicaoAutomaticamente;
					obj3 = ((montarComposicaoAutomaticamente != null) ? new bool?(montarComposicaoAutomaticamente.Valor) : null);
				}
			}
			estoque.QuantidadeParaMontarComposicao = ((obj3 ?? false) ? Quantidade : 0m);
			decimal precoUnitarioInformado = (((ItemDto)obj2).PrecoUnitario = PrecoUnitario);
			((ItemDto)obj2).PrecoUnitarioInformado = precoUnitarioInformado;
			((ItemDto)obj2).TotalLiquido = Total;
			((ItemDto)obj2).ObservacaoDigitada = HelperString.ToStringEmptyWhenNull(Observacao);
			((ItemDto)obj2).VendedorResponsavel = Vendedor;
			((ItemDto)obj2).PercentualComissao = PercentualComissao;
			((ItemDto)obj2).DescontoDigitado = Desconto;
			((ItemDto)obj2).OutrasDespesasDigitado = Acrescimo;
			((ItemDto)obj2).Cancelado = base.Dto.Cancelado;
		}
	}
	public class ItensAbaViewModel : IAbaViewModelBase, IDisposable
	{
		public string Titulo => "ITENS";

		public short? Numero { get; }

		public string SubTitulo => "Itens do pedido";

		public bool Valido => ViewModel.Valido;

		public IViewModelBase ViewModel { get; }

		public ItensAbaViewModel(IViewModelBase vm)
		{
			ViewModel = vm;
		}

		public void Dispose()
		{
			ViewModel.Dispose();
		}

		public IAbaViewModelBase SetarNumero(short numero)
		{
			return (IAbaViewModelBase)(object)this;
		}
	}
	public class ItensViewModel : NavegacaoNaFicha<MovimentacaoDto, NavigationBase>
	{
		private readonly IWService _service;

		private decimal _quantidadeInformada;

		private AutorizacaoDto _autorizacao;

		private static IEnumerable<PessoaDto> _vendedores = new List<PessoaDto>();

		private readonly Func<AutorizacaoDto> _permitirVenderEstoqueSaldoNegativo;

		private readonly CondicaoEspecialService _condicaoEspecialService;

		private readonly Func<Task<decimal>> _capturarPeso;

		public ISelecaoCommandParameter KitCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<KitDto>("KitListaView", (Action<KitDto>)CallbackSelecionouKit, true);

		public TipoEstoqueBaseViewModel TipoEstoque
		{
			get
			{
				return ((BindableBase)this).GetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque));
			}
			set
			{
				((BindableBase)this).SetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque), value, (Action)delegate
				{
					PodeInformarQuantidade = TipoEstoque == null;
				});
			}
		}

		[NaoPermitirValorMenorIgualZero]
		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value);
			}
		}

		public bool PodeInformarQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade), value);
			}
		}

		private decimal QuantidadeSaldoEQuantidadeMovimentacaoAtual
		{
			get
			{
				decimal num = ItensMovimentacao.Itens.Where((Item x) => ((ViewModelPartialBase<ItemDto>)x).Dto.ProdutoServicoEmpresaReferencia.Equals(((EntityBaseDto)ProdutoServicoEmpresa).Id)).Sum((Item x) => x.Quantidade);
				return ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeSaldo - num;
			}
		}

		public SelecaoCommandParameter<ProdutoServicoEmpresaDto> ProdutoServicoCommandParameter { get; set; }

		public ProdutoServicoEmpresaDto ProdutoServicoEmpresa { get; set; }

		public ProdutoServicoDto ProdutoServico { get; set; }

		public int NumeroUltimoItem { get; set; }

		public string Informacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Informacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Informacao), value);
			}
		}

		[Valid]
		public Lista<Item> ItensMovimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<Item>>((Expression<Func<Lista<Item>>>)(() => ItensMovimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<Item>>((Expression<Func<Lista<Item>>>)(() => ItensMovimentacao), value);
			}
		}

		[NaoPermitirValorMenorIgualZero]
		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value);
			}
		}

		public ICommand PesquisarProdutoCommand { get; set; }

		public ICommand<object> RemoverItemCommand { get; set; }

		public ICommand<Item> AplicarDescontoCommand { get; set; }

		public ICommand<Item> AplicarAcrescimoCommand { get; set; }

		public ICommand<Item> AlterarVendedorCommand { get; set; }

		public ICommand<Item> AlterarObservacaoCommand { get; set; }

		public ICommand<Item> AlterarPrecoUnitarioCommand { get; set; }

		public ItensViewModel(MovimentacaoDto dto, IWService service, NavigationBase navigation, Func<AutorizacaoDto> permitirVenderEstoqueSaldoNegativo, Func<Task<decimal>> capturarPeso, IEnumerable<PessoaDto> vendedores)
			: base(dto, navigation)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0149: Expected O, but got Unknown
			_service = service;
			_permitirVenderEstoqueSaldoNegativo = permitirVenderEstoqueSaldoNegativo;
			_capturarPeso = capturarPeso;
			PesquisarProdutoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto);
			RemoverItemCommand = (ICommand<object>)(object)new DelegateCommand<object>((Action<object>)RemoverItem, (Func<object, bool>)PodeRemoverItem, (bool?)null);
			AplicarDescontoCommand = (ICommand<Item>)(object)new DelegateCommand<Item>((Action<Item>)AplicarDesconto, (Func<Item, bool>)PodeAplicarDesconto, (bool?)null);
			AplicarAcrescimoCommand = (ICommand<Item>)(object)new DelegateCommand<Item>((Action<Item>)AplicarAcrescimo, (Func<Item, bool>)PodeAplicarAcrescimo, (bool?)null);
			AlterarVendedorCommand = (ICommand<Item>)(object)new DelegateCommand<Item>((Action<Item>)AlterarVendedor, (Func<Item, bool>)PodeAlterarVendedor, (bool?)null);
			AlterarObservacaoCommand = (ICommand<Item>)(object)new DelegateCommand<Item>((Action<Item>)AlterarObservacao, (Func<Item, bool>)PodeAlterarObservacao, (bool?)null);
			AlterarPrecoUnitarioCommand = (ICommand<Item>)(object)new DelegateCommand<Item>((Action<Item>)AlterarPrecoUnitario, (Func<Item, bool>)PodeAlterarPrecoUnitario, (bool?)null);
			_condicaoEspecialService = new CondicaoEspecialService();
			_vendedores = vendedores;
			NumeroUltimoItem = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens.Count();
		}

		private async void CallbackSelecionouKit(KitDto kit)
		{
			await VenderKit(kit, 1m, barrasComInformacao: false);
		}

		private async Task<bool> LancouKit(string codigoBarras, string codigoInterno)
		{
			KitDto val = _service.Canal<IKitModuleService, KitDto>((Func<IKitModuleService, KitDto>)((IKitModuleService service) => service.ObterKitPorCodigoInterno(codigoInterno)));
			if (HelperObject.Is<KitDtoNull>((object)val))
			{
				return false;
			}
			if (!HelperIEnumerable.ToListEmptyWhenNull<ItemKitDto>(val.Itens).Any() || val.Itens.Any((ItemKitDto x) => HelperObject.Is<ProdutoServicoEmpresaDtoNull>((object)x.ProdutoEmpresa)))
			{
				DialogoFactory.MostrarDialogoConfirma("Kit não importado: um dos produtos da composição não está disponível nesta empresa.", false, HorizontalAlignment.Center);
				return true;
			}
			decimal quantidadeKit = default(decimal);
			if (!ProcessarCodigoDeBarrasService.TentarObterQuantidadeDeKit(codigoBarras, val.Total, ref quantidadeKit))
			{
				DialogoFactory.MostrarDialogoConfirma("Não foi possível determinar a quantidade de kits pela etiqueta. Gere novamente o arquivo para a balança e repita a leitura.", false, HorizontalAlignment.Center);
				return true;
			}
			await VenderKit(val, quantidadeKit, barrasComInformacao: true);
			return true;
		}

		private async Task VenderKit(KitDto kit, decimal quantidadeKit, bool barrasComInformacao)
		{
			Informacao = string.Empty;
			foreach (ItemKitDto iten in kit.Itens)
			{
				ItemKitDto val = iten;
				if (kit.UtilizarPrecoDosItensAoVender)
				{
					val.ProdutoEmpresa.Preco.PrecoVendaVigente = iten.PrecoVenda;
				}
				await VenderItemKit(val, quantidadeKit, barrasComInformacao);
			}
		}

		private async Task VenderItemKit(ItemKitDto itemKit, decimal quantidadeKit, bool barrasComInformacao)
		{
			Quantidade = itemKit.Quantidade * quantidadeKit;
			await SelecionouProdutoServico(itemKit.ProdutoEmpresa, barrasComInformacao).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private bool PodeAlterarObservacao(Item arg)
		{
			if (HelperObject.IsNotNull((object)arg))
			{
				return !arg.Cancelado;
			}
			return false;
		}

		private void AlterarObservacao(Item obj)
		{
			obj.AlterarObservacao();
		}

		private bool PodeAlterarVendedor(Item arg)
		{
			if (HelperObject.IsNotNull((object)arg) && !arg.Cancelado)
			{
				return _vendedores.Any();
			}
			return false;
		}

		private void AlterarVendedor(Item obj)
		{
			obj.AlterarVendedor();
		}

		private void AlterarPrecoUnitario(Item obj)
		{
			ItensMovimentacao.Posicionar((Expression<Func<Item, bool>>)((Item x) => x.NumeroItem == obj.NumeroItem));
			obj.AlterarPrecoUnitario();
		}

		private bool PodeAlterarPrecoUnitario(Item arg)
		{
			if (HelperObject.IsNotNull((object)arg) && !arg.Cancelado)
			{
				return DadosSistema.VerificarPermissao<MovimentacaoSaida, PermitirDesconto>();
			}
			return false;
		}

		private bool PodeAplicarDesconto(Item arg)
		{
			if (HelperObject.IsNotNull((object)arg) && !arg.Cancelado)
			{
				return DadosSistema.VerificarPermissao<MovimentacaoSaida, PermitirDesconto>();
			}
			return false;
		}

		private bool PodeAplicarAcrescimo(Item arg)
		{
			if (HelperObject.IsNotNull((object)arg))
			{
				return !arg.Cancelado;
			}
			return false;
		}

		private void AplicarDesconto(Item obj)
		{
			ItensMovimentacao.Posicionar((Expression<Func<Item, bool>>)((Item x) => x.NumeroItem == obj.NumeroItem));
			obj.AplicarDesconto();
		}

		private void AplicarAcrescimo(Item obj)
		{
			ItensMovimentacao.Posicionar((Expression<Func<Item, bool>>)((Item x) => x.NumeroItem == obj.NumeroItem));
			obj.AplicarAcrescimo();
		}

		public override void ProjectedAsViewModel()
		{
			ItensMovimentacao = new Lista<Item>();
			foreach (ItemDto item in HelperIEnumerable.ToListEmptyWhenNull<ItemDto>(((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens))
			{
				Quantidade = item.Quantidade;
				item.TotalLiquido -= item.Frete;
				item.Frete = 0m;
				CriarItem(item);
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			if (val != null)
			{
				val.Itens = HelperIEnumerable.ToListEmptyWhenNull<Item>((IEnumerable<Item>)ItensMovimentacao.Itens).Select(delegate(Item x)
				{
					((ViewModelPartialBase<ItemDto>)x).Dto.ObservacaoDigitada = x.Observacao ?? string.Empty;
					((ViewModelPartialBase<ItemDto>)x).Dto.VendedorResponsavel = x.Vendedor;
					return ((ViewModelPartialBase<ItemDto>)x).Dto;
				});
			}
			((ViewModelDtoBase<MovimentacaoDto>)this).AtualizarDto<T>(ref dto);
		}

		private async void PesquisarProduto()
		{
			_ = 1;
			try
			{
				string informacao = HelperString.ToStringEmptyWhenNull(Informacao);
				if (informacao.Contains("*"))
				{
					informacao = HelperString.ToStringEmptyWhenNull(informacao.Split('*')[1]);
				}
				string codigoInterno = ProcessarCodigoDeBarrasService.RetornarCodigoInternoDoCodigoBarras(informacao);
				PesquisaDto objetoPesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaProdutoServicoDto
					{
						Informacao = (HelperObject.IsNotNull((object)codigoInterno) ? codigoInterno : informacao)
					},
					Pagina = 1,
					Tamanho = 2
				};
				List<ProdutoServicoEmpresaDto> produtosEncontrados = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>((await _service.Canal<IProdutoServicoModuleService, Task<PaginaEntityDto>>((Func<IProdutoServicoModuleService, Task<PaginaEntityDto>>)((IProdutoServicoModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisa)))).Lista.OfType<ProdutoServicoEmpresaDto>());
				bool flag = !produtosEncontrados.Any() && HelperObject.IsNotNull((object)codigoInterno);
				if (flag)
				{
					flag = await LancouKit(informacao, codigoInterno);
				}
				if (flag)
				{
					Informacao = string.Empty;
				}
				else
				{
					ProcessarRetornoPesquisa(produtosEncontrados, informacao, HelperObject.IsNotNull((object)codigoInterno));
				}
			}
			catch (Exception ex)
			{
				Informacao = string.Empty;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private async void ProcessarRetornoPesquisa(IEnumerable<ProdutoServicoEmpresaDto> resultado, string informacaoPesquisa, bool barrasComInformacao)
		{
			Quantidade = 1m;
			List<ProdutoServicoEmpresaDto> list = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(resultado);
			if (list.Count == 1)
			{
				await SelecionouProdutoServico(list.FirstOrDefault(), barrasComInformacao).ContinueWith(delegate(Task task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
				});
				return;
			}
			ProdutoServicoCommandParameter = new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto x)
			{
				DadosSistema.GetEventPublisher().Publish<RestaurarResolucaoEvent>(new RestaurarResolucaoEvent());
				CallbackSelecaoProdutoServicoCommandParameter(x);
			}, true);
			ProdutoServicoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)informacaoPesquisa);
			Navegar(ProdutoServicoCommandParameter);
		}

		protected async void CallbackSelecaoProdutoServicoCommandParameter(ProdutoServicoEmpresaDto produtoServico)
		{
			await SelecionouProdutoServico(produtoServico, barrasComInformacao: false).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		protected virtual async Task SelecionouProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresa, bool barrasComInformacao)
		{
			TratarQuantidade();
			if (!HelperObject.Is<ServicoDtoNull>((object)produtoServicoEmpresa.ProdutoServico.Servico))
			{
				throw new Exception("Selecione um item do tipo produto");
			}
			if (!produtoServicoEmpresa.Vendavel)
			{
				throw new Exception("Selecione um produto 'Vendável'!");
			}
			ProdutoServico = produtoServicoEmpresa.ProdutoServico;
			ProdutoServicoEmpresa = produtoServicoEmpresa;
			string informacao = HelperString.ToStringEmptyWhenNull(Informacao);
			if (HelperString.ToStringEmptyWhenNull(Informacao).Contains("*"))
			{
				informacao = HelperString.ToStringEmptyWhenNull(Informacao).Split('*')[1];
			}
			ItensViewModel itensViewModel = this;
			ProdutoServicoDto produtoServico = ProdutoServicoEmpresa.ProdutoServico;
			int? obj;
			if (produtoServico == null)
			{
				obj = null;
			}
			else
			{
				ProdutoDto produto = produtoServico.Produto;
				if (produto == null)
				{
					obj = null;
				}
				else
				{
					IEnumerable<CodigoBarraAlternativoDto> codigosBarrasAlternativos = produto.CodigosBarrasAlternativos;
					if (codigosBarrasAlternativos == null)
					{
						obj = null;
					}
					else
					{
						CodigoBarraAlternativoDto obj2 = codigosBarrasAlternativos.FirstOrDefault((CodigoBarraAlternativoDto x) => x.CodigoBarras.ToLower().Equals(informacao.ToLower()));
						obj = ((obj2 != null) ? new int?(obj2.FatorConversaoVenda) : null);
					}
				}
			}
			decimal quantidade;
			if ((obj ?? 0) <= 0)
			{
				quantidade = Quantidade;
			}
			else
			{
				ProdutoServicoDto produtoServico2 = ProdutoServicoEmpresa.ProdutoServico;
				int? obj3;
				if (produtoServico2 == null)
				{
					obj3 = null;
				}
				else
				{
					ProdutoDto produto2 = produtoServico2.Produto;
					if (produto2 == null)
					{
						obj3 = null;
					}
					else
					{
						IEnumerable<CodigoBarraAlternativoDto> codigosBarrasAlternativos2 = produto2.CodigosBarrasAlternativos;
						if (codigosBarrasAlternativos2 == null)
						{
							obj3 = null;
						}
						else
						{
							CodigoBarraAlternativoDto obj4 = codigosBarrasAlternativos2.FirstOrDefault((CodigoBarraAlternativoDto x) => x.CodigoBarras.Equals(informacao));
							obj3 = ((obj4 != null) ? new int?(obj4.FatorConversaoVenda) : null);
						}
					}
				}
				quantidade = ((decimal?)obj3 * (decimal?)Quantidade) ?? Quantidade;
			}
			itensViewModel.Quantidade = quantidade;
			if (ProdutoServicoEmpresa.ProdutoServico.Produto.Pesavel && !ProdutoServicoEmpresa.ProdutoServico.UnidadeMedida.IsUnidade && !barrasComInformacao)
			{
				try
				{
					Quantidade = await _capturarPeso();
					if (Quantidade <= 0m)
					{
						SolicitarPeso();
					}
				}
				catch (Exception)
				{
					SolicitarPeso();
				}
			}
			if (HelperObject.Is<ServicoEmpresaDtoNull>((object)ProdutoServicoEmpresa.ServicoEmpresa) && !ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.IsNormal)
			{
				CriarTipoEstoque();
				((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
				TipoEstoque.ActionSetarQuantidade((Action)SetarQuantidade);
				Navegar(TipoEstoque.EstoqueCommandParameter, async delegate
				{
					await AdicionarItemMovimentacao();
				});
				return;
			}
			if (HelperObject.IsNotNull((object)Informacao) && Informacao.StartsWith("2") && Informacao.Length == 13)
			{
				string sigla = ProdutoServicoEmpresa.ProdutoServico.UnidadeMedida.Sigla;
				decimal precoVendaVigente = ProdutoServicoEmpresa.Preco.PrecoVendaVigente;
				Quantidade = ProcessarCodigoDeBarrasService.RetornarQuantidadeDoCodigoBarras(Informacao, sigla, precoVendaVigente);
			}
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				((ViewModelValidationBase)tipoEstoque).Dispose();
			}
			TipoEstoque = null;
			await AdicionarItemMovimentacao();
		}

		private void SolicitarPeso()
		{
			Quantidade = DialogoFactory.MostrarDialogoLancamentoManual(ProdutoServicoEmpresa.Preco.PrecoVendaVigente, DadosSistema.IsPafEcf);
		}

		private void TratarQuantidade()
		{
			if (string.IsNullOrEmpty(Informacao) || !Informacao.Contains("*"))
			{
				return;
			}
			try
			{
				decimal num = ObjectToDecimalConverter.TryConvertToDecimal((object)Informacao.Split('*')[0]);
				if (num > 0m)
				{
					Quantidade = num;
				}
			}
			catch (Exception)
			{
				Quantidade = 1m;
			}
		}

		protected void CriarTipoEstoque()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			TipoEstoque = ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel((EstoqueDto)null, ProdutoServicoEmpresa.ProdutoEmpresa.Estoque)).ObterTipoEstoque().ViewModelBase;
		}

		protected void SetarQuantidade()
		{
			Quantidade = TipoEstoque.Quantidade;
		}

		protected async Task AdicionarItemMovimentacao()
		{
			try
			{
				_quantidadeInformada = Quantidade;
				if (Quantidade == 0m || !PodeAdicionarItem())
				{
					LimparPropriedades();
					return;
				}
				ItemDto itemDto = ObterDtoCriarItem();
				ItemDto result = _service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(itemDto))).Result;
				if (HelperObject.Is<ItemDtoNull>((object)result))
				{
					return;
				}
				result.Numero = ++NumeroUltimoItem;
				_condicaoEspecialService.VerificarCondicaoEspecial(result);
				CriarItem(result);
				_condicaoEspecialService.VerificarCondicaoEspecial(result, from x in ItensMovimentacao.Itens
					where !x.Cancelado
					select ((ViewModelPartialBase<ItemDto>)x).Dto);
				foreach (Item item in ItensMovimentacao.Itens.Where((Item x) => !x.Cancelado))
				{
					item.Desconto = ((ViewModelPartialBase<ItemDto>)item).Dto.DescontoDigitado;
					item.Acrescimo = ((ViewModelPartialBase<ItemDto>)item).Dto.TotalOutrasDespesas;
					item.ObterItemCalculado();
				}
				((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens = new List<ItemDto>(((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens) { result };
				await SalvarDav(result);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private ItemDto ObterDtoCriarItem()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_013e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			return new ItemDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)ProdutoServicoEmpresa).Id,
				QuantidadeInformada = Quantidade,
				Estoque = (EstoqueDto)(HelperObject.IsNotNull((object)TipoEstoque) ? ((object)((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto) : ((object)new EstoqueDto
				{
					Id = ((EntityBaseDto)ProdutoServicoEmpresa.ProdutoEmpresa.Estoque).Id,
					Quantidade = Quantidade
				})),
				PrecoUnitarioInformado = ObterPreco(),
				CalculadoPeloSistema = true,
				QuantidadeVendidaMovimentacao = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Itens.Where((ItemDto x) => x.ProdutoServicoEmpresaReferencia.Equals(((EntityBaseDto)ProdutoServicoEmpresa).Id)).Sum((ItemDto x) => x.QuantidadeInformada),
				TabelaPreco = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.TabelaPreco,
				VendedorResponsavel = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.VendedorResponsavel,
				PessoaMovimentacao = ((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Pessoa,
				EmpresaMovimentacao = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.Empresa,
				IndicadorOperacaoMovimentacao = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.IndicadorOperacao,
				IndicadorPresencaCompradorMovimentacao = ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.IndicadorPresencaComprador,
				NaturezaOperacaoMovimentacao = ((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).NaturezaOperacao,
				TipoMovimentacao = ((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Tipo,
				ObservacaoDigitada = string.Empty,
				DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia
			};
		}

		private async Task SalvarDav(params ItemDto[] itensDto)
		{
			if (!((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Dav || !DadosSistema.IsPafEcf)
			{
				return;
			}
			MovimentacaoDto movimentacaoDto = (MovimentacaoDto)((((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Numero == 0L) ? ((object)((ViewModelDtoBase<MovimentacaoDto>)this).Dto) : ((object)new MovimentacaoDto
			{
				Numero = ((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Numero,
				Versao = ((EntityVersaoDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Versao,
				Id = ((EntityBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Id,
				Itens = itensDto
			}));
			await _service.Canal<IMovimentacaoSaidaModuleService, Task<InformacaoMovimentacaoDto>>((Func<IMovimentacaoSaidaModuleService, Task<InformacaoMovimentacaoDto>>)((IMovimentacaoSaidaModuleService service) => service.SalvarDav(movimentacaoDto))).ContinueWith(delegate(Task<InformacaoMovimentacaoDto> task)
			{
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					InformacaoMovimentacaoDto result = task.Result;
					((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Numero = result.Numero;
					((EntityVersaoDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Versao = result.Versao;
					((EntityBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Id = result.Id;
				}
			});
		}

		private decimal ObterPreco()
		{
			if (!HelperObject.IsNot<TabelaPrecoDtoNull>((object)((ViewModelDtoBase<MovimentacaoDto>)this).Dto.TabelaPreco))
			{
				return ProdutoServicoEmpresa.Preco.PrecoVendaVigente;
			}
			if (!(ProdutoServicoEmpresa.Preco.ItensTabelaPreco.FirstOrDefault((ItemTabelaPrecoProdutoServicoDto x) => x.TabelaPrecoReferencia.Equals(((EntityBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto.TabelaPreco).Id)).Preco == 0m))
			{
				return ProdutoServicoEmpresa.Preco.ItensTabelaPreco.FirstOrDefault((ItemTabelaPrecoProdutoServicoDto x) => x.TabelaPrecoReferencia.Equals(((EntityBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto.TabelaPreco).Id)).Preco;
			}
			return ProdutoServicoEmpresa.Preco.PrecoVendaVigente;
		}

		private bool PodeAdicionarItem()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Invalid comparison between Unknown and I4
			ValidarQuantidadeMinima();
			if (DadosSistema.RetornarPermitirEstoqueNegativoPorTipoMovimentacao(((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Tipo) || _quantidadeInformada == 0m)
			{
				return true;
			}
			if ((int)ProdutoServicoEmpresa.ProdutoServico.Tipo == 4 && ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.IsNormal && DadosSistema.ConfiguracaoDto.Saida.MontarComposicaoAutomaticamente.Valor)
			{
				return true;
			}
			if (_quantidadeInformada <= QuantidadeSaldoEQuantidadeMovimentacaoAtual)
			{
				return true;
			}
			if (_quantidadeInformada <= ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.Quantidade)
			{
				if (DialogoFactory.MostrarDialogoSimNao(MessagemQuantidadeSaldoNegativa(), false, 0, HorizontalAlignment.Center).Equals(MessageBoxResult.Yes))
				{
					_autorizacao = _permitirVenderEstoqueSaldoNegativo();
					if (HelperObject.IsNotNull((object)_autorizacao))
					{
						return true;
					}
				}
			}
			else
			{
				DialogoFactory.MostrarDialogoConfirma($"{ProdutoServicoEmpresa.ProdutoServico.Descricao} atingiu estoque negativo!", false, HorizontalAlignment.Center);
			}
			return false;
		}

		private void LimparPropriedades()
		{
			Informacao = string.Empty;
			_quantidadeInformada = default(decimal);
			_autorizacao = null;
		}

		private void ValidarQuantidadeMinima()
		{
			if (!(ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima == 0m) && DadosSistema.ConfiguracaoDto.Saida.AlertarQuantidadeMinimaMaximaAtingida.Valor && ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.Quantidade - _quantidadeInformada <= ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima)
			{
				DialogoFactory.MostrarDialogoConfirma($"Quantidade mínima em estoque atingida. Quantidade mínima: {ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima}.", false, HorizontalAlignment.Center);
			}
		}

		private string MessagemQuantidadeSaldoNegativa()
		{
			return $"{ProdutoServicoEmpresa.ProdutoServico.Descricao} atingiu quantidade saldo negativa! " + $"Porém, a quantidade em estoque é de {ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.Quantidade}. " + "Continuar?";
		}

		public void RecalcularItem(ItemDto item)
		{
			ItensMovimentacao.Posicionar((Expression<Func<Item, bool>>)((Item x) => x.NumeroItem == item.Numero));
			ItemDto val = ItemProjection.ProjectedAsDto(ItemProjection.Morfar(item));
			if (!HelperObject.IsNull((object)val))
			{
				val.Numero = item.Numero;
				if (ItensMovimentacao.PossuiItemSelecionado)
				{
					((ViewModelPartialBase<ItemDto>)ItensMovimentacao.ItemSelecionado).AtualizarInformacoes(val);
				}
				AtualizarTotal();
			}
		}

		private void CriarItem(ItemDto itemDto)
		{
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			bool flag = HelperObject.IsNot<CondicaoEspecialDtoNull>((object)itemDto.CondicaoEspecial) && DateTime.Now >= itemDto.CondicaoEspecial.DataVigenciaInicial.Date && DateTime.Now <= HelperDate.ToFinalDia(itemDto.CondicaoEspecial.DataVigenciaFinal);
			Item item = new Item(itemDto, RecalcularItem)
			{
				PodeInformarQuantidade = ((!flag && DadosSistema.RetornarPermitirEstoqueNegativoPorTipoMovimentacao(itemDto.TipoMovimentacao)) || PodeInformarQuantidade)
			};
			ItensMovimentacao.Adicionar(item);
			AtualizarTotal();
			Informacao = string.Empty;
			if (DadosSistema.ConfiguracaoDto.Saida.SolicitarVendedorOuTecnicoAoLancarItem.Valor)
			{
				item.AlterarVendedor();
			}
		}

		private void AtualizarTotal()
		{
			Total = ItensMovimentacao.Itens.Where((Item item) => !((ViewModelPartialBase<ItemDto>)item).Dto.Cancelado).Sum((Item x) => ((ViewModelPartialBase<ItemDto>)x).Dto.TotalLiquido);
		}

		private bool PodeRemoverItem(object o)
		{
			return o is Item;
		}

		private void RemoverItem(object o)
		{
			if (!(o is Item item) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirCancelarItemDocumentoFiscal>(0m, (string)null) == null)
			{
				return;
			}
			if (HelperObject.IsNot<CondicaoEspecialDtoNull>((object)((ViewModelPartialBase<ItemDto>)ItensMovimentacao.ItemSelecionado).Dto.CondicaoEspecial) && DateTime.Now >= ((ViewModelPartialBase<ItemDto>)ItensMovimentacao.ItemSelecionado).Dto.CondicaoEspecial.DataVigenciaInicial.Date && DateTime.Now <= HelperDate.ToFinalDia(((ViewModelPartialBase<ItemDto>)ItensMovimentacao.ItemSelecionado).Dto.CondicaoEspecial.DataVigenciaFinal))
			{
				if (DialogoFactory.MostrarDialogoSimNao("Todos os itens já lançados com o mesmo Produto/Serviço serão cancelados por possuírem condição especial no cadastro. Confirmar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					_condicaoEspecialService.RemoverItemDasCondicoesEspeciais(((ViewModelPartialBase<ItemDto>)ItensMovimentacao.ItemSelecionado).Dto);
					foreach (Item item2 in ItensMovimentacao.Itens.Where(delegate(Item x)
					{
						string id = ((EntityBaseDto)x.Produto).Id;
						Item itemSelecionado = ItensMovimentacao.ItemSelecionado;
						return id == ((itemSelecionado != null) ? ((EntityBaseDto)itemSelecionado.Produto).Id : null);
					}).ToList())
					{
						item2.Cancelado = true;
					}
				}
			}
			else
			{
				item.Cancelar();
			}
			AtualizarTotal();
		}

		public static IEnumerable<PessoaDto> GetVendedores()
		{
			return _vendedores;
		}

		public override void Dispose()
		{
			Lista<Item> itensMovimentacao = ItensMovimentacao;
			if (itensMovimentacao != null)
			{
				((ViewModelValidationBase)itensMovimentacao).Dispose();
			}
			ItensMovimentacao = null;
			ProdutoServicoEmpresa = null;
			ProdutoServico = null;
			ProdutoServicoCommandParameter = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class TotalizacaoAbaViewModel : IAbaViewModelBase, IDisposable
	{
		public string Titulo => "TOTALIZAÇÃO";

		public short? Numero { get; }

		public string SubTitulo => "Total do pedido e frete";

		public bool Valido => ViewModel.Valido;

		public IViewModelBase ViewModel { get; }

		public TotalizacaoAbaViewModel(IViewModelBase vm)
		{
			ViewModel = vm;
		}

		public void Dispose()
		{
			ViewModel.Dispose();
		}

		public IAbaViewModelBase SetarNumero(short numero)
		{
			return (IAbaViewModelBase)(object)this;
		}
	}
	public class TotalizacaoViewModel : ViewModelDtoBase<MovimentacaoDto>
	{
		private readonly IWService _service;

		private readonly IAutenticacaoService _autenticacaoService;

		public decimal TotalDescontoItens
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontoItens));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontoItens), value);
			}
		}

		public bool MostrarTotalDescontoAplicado
		{
			get
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Invalid comparison between Unknown and I4
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Invalid comparison between Unknown and I4
				if ((int)((MovimentacaoBaseDto)base.Dto).Tipo != 4)
				{
					return (int)((MovimentacaoBaseDto)base.Dto).Tipo != 3;
				}
				return false;
			}
		}

		public decimal TotalDescontos
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontos));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontos), value);
			}
		}

		public virtual bool PermitirDesconto => !DadosSistema.PermitirDesconto;

		public decimal TotalDescontoAplicado
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontoAplicado));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalDescontoAplicado), value, (Action)ValidarDesconto);
			}
		}

		public Func<decimal> RetornarTotalComFrete
		{
			get
			{
				AtualizarTotal();
				return () => TotalComFrete;
			}
		}

		public Lista<IViewModelDtoBase> OpcoesPagamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<IViewModelDtoBase>>((Expression<Func<Lista<IViewModelDtoBase>>>)(() => OpcoesPagamento));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<IViewModelDtoBase>>((Expression<Func<Lista<IViewModelDtoBase>>>)(() => OpcoesPagamento), value);
			}
		}

		public bool AdicionarFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AdicionarFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AdicionarFrete), value);
			}
		}

		public Lista<FreteDto> Fretes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<FreteDto>>((Expression<Func<Lista<FreteDto>>>)(() => Fretes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<FreteDto>>((Expression<Func<Lista<FreteDto>>>)(() => Fretes), value);
			}
		}

		public PlanoContaDto PlanoConta
		{
			get
			{
				return ((BindableBase)this).GetProperty<PlanoContaDto>((Expression<Func<PlanoContaDto>>)(() => PlanoConta));
			}
			set
			{
				((BindableBase)this).SetProperty<PlanoContaDto>((Expression<Func<PlanoContaDto>>)(() => PlanoConta), value, (Action)delegate
				{
					PlanoContaDescricao = value.Descricao;
				});
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Plano de contas' é obrigatório!")]
		public string PlanoContaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PlanoContaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PlanoContaDescricao), value);
			}
		}

		public CentroCustoDto CentroCusto
		{
			get
			{
				return ((BindableBase)this).GetProperty<CentroCustoDto>((Expression<Func<CentroCustoDto>>)(() => CentroCusto));
			}
			set
			{
				((BindableBase)this).SetProperty<CentroCustoDto>((Expression<Func<CentroCustoDto>>)(() => CentroCusto), value, (Action)delegate
				{
					CentroCustoDescricao = value.Descricao;
				});
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Centro de custos' é obrigatório!")]
		public string CentroCustoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CentroCustoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CentroCustoDescricao), value);
			}
		}

		public decimal TotalComFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalComFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalComFrete), value);
			}
		}

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value, (Action)OnAlterouTotal);
			}
		}

		public ISelecaoCommandParameter FreteCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<FreteDto>("FreteListaView", (Action<FreteDto>)delegate(FreteDto dto)
		{
			if (Fretes.Existe((Expression<Func<FreteDto, bool>>)((FreteDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id)))
			{
				Fretes.Remover(Fretes.Itens.FirstOrDefault((FreteDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id));
			}
			Fretes.Adicionar(dto);
		}, true);

		public ISelecaoCommandParameter PlanoContaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PlanoContaDto>("PlanoContaListaView", (Action<PlanoContaDto>)delegate(PlanoContaDto dto)
		{
			PlanoConta = dto;
		}, true);

		public ISelecaoCommandParameter CentroCustoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<CentroCustoDto>("CentroCustoListaView", (Action<CentroCustoDto>)delegate(CentroCustoDto dto)
		{
			CentroCusto = dto;
		}, true);

		public ICommand CalcularDescontoCommand { get; set; }

		public TotalizacaoViewModel(MovimentacaoDto dto, IWService service, IAutenticacaoService autenticacaoService)
			: base(dto)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			_autenticacaoService = autenticacaoService;
			_service = service;
			CalcularDescontoCommand = (ICommand)new DelegateCommand((Action)CalcularDesconto, (Func<bool>)(() => DadosSistema.PermitirDesconto), (bool?)null);
		}

		public override async void ProjectedAsViewModel()
		{
			try
			{
				PlanoContaDto planoContaPadrao = await _service.Canal<IPlanoContaModuleService, Task<PlanoContaDto>>((Func<IPlanoContaModuleService, Task<PlanoContaDto>>)((IPlanoContaModuleService service) => ((IPorCodigoUnico<PlanoContaDto>)(object)service).ObterPorCodigoUnicoAsync(DadosSistema.ConfiguracaoDto.Financeiro.PlanoContaEntradaPadrao.Referencia)));
				CentroCustoDto centroCustoPadrao = await _service.Canal<ICentroCustoModuleService, Task<CentroCustoDto>>((Func<ICentroCustoModuleService, Task<CentroCustoDto>>)((ICentroCustoModuleService service) => ((IPorCodigoUnico<CentroCustoDto>)(object)service).ObterPorCodigoUnicoAsync(DadosSistema.ConfiguracaoDto.Financeiro.CentroCustoEntradaPadrao.Referencia)));
				Total = ((MovimentacaoBaseDto)((ViewModelDtoBase<MovimentacaoDto>)this).Dto).Total;
				Fretes = new Lista<FreteDto>();
				PaginaEntityDto val = await _service.Canal<IFreteModuleService, Task<PaginaEntityDto>>((Func<IFreteModuleService, Task<PaginaEntityDto>>)((IFreteModuleService service) => ((IPesquisarAtivos)service).PesquisarAtivosAsync(string.Empty, (short)1, short.MaxValue)));
				Fretes.Adicionar(val.Lista.Cast<FreteDto>());
				Fretes.PosicionarNoPrimeiro();
				Fretes.AlterouItemSelecionadoEvent += FretesOnAlterouItemSelecionadoEvent;
				PlanoConta = (HelperObject.Is<PlanoContaDtoNull>((object)((ViewModelDtoBase<MovimentacaoDto>)this).Dto.PlanoContaPadrao) ? planoContaPadrao : ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.PlanoContaPadrao);
				CentroCusto = (HelperObject.Is<CentroCustoDtoNull>((object)((ViewModelDtoBase<MovimentacaoDto>)this).Dto.CentroCustoPadrao) ? centroCustoPadrao : ((ViewModelDtoBase<MovimentacaoDto>)this).Dto.CentroCustoPadrao);
				AtualizarTotal();
				Totalizar();
				OpcoesPagamento = new Lista<IViewModelDtoBase>();
				OpcoesPagamento.Adicionar((IViewModelDtoBase[])(object)new IViewModelDtoBase[4]
				{
					(IViewModelDtoBase)new Dinheiro(RetornarTotalComFrete),
					(IViewModelDtoBase)new Cartao(),
					(IViewModelDtoBase)new Pix(),
					(IViewModelDtoBase)new Outros()
				});
				if (((ViewModelDtoBase<MovimentacaoDto>)this).Dto.DescricaoTipoPagamentoDelivery.Equals("Cartão"))
				{
					OpcoesPagamento.PosicionarNoUltimo();
				}
				else
				{
					OpcoesPagamento.PosicionarNoPrimeiro();
				}
				((IViewModelBase)OpcoesPagamento.ItemSelecionado).ProjectedAsViewModel();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void CalcularDesconto()
		{
			TotalDescontoAplicado = DialogoFactory.MostrarDialogoDesconto(Total, 0m);
		}

		private void ValidarDesconto()
		{
			decimal total = Total;
			if (total == 0m)
			{
				return;
			}
			decimal num = (decimal)base.Dto.Itens.Count((ItemDto x) => !x.Cancelado) * 0.01m;
			if (TotalDescontoAplicado >= 0m && TotalDescontoAplicado < total && total - TotalDescontoAplicado >= num)
			{
				decimal limiteDescontoUsuario = DadosSistema.LimiteDescontoUsuario;
				decimal num2 = base.Dto.Itens.Where((ItemDto a) => !a.Cancelado).Sum((ItemDto a) => a.ValorLimiteDesconto) - TotalDescontoItens;
				decimal num3 = base.Dto.Itens.Where((ItemDto a) => !a.Cancelado).Sum((ItemDto a) => a.TotalBruto * (limiteDescontoUsuario / 100m)) - TotalDescontoItens;
				decimal num4 = base.Dto.Itens.Where((ItemDto a) => !a.Cancelado).Sum((ItemDto a) => a.TotalBruto * (a.LimiteDesconto / 100m)) - TotalDescontoItens;
				if (limiteDescontoUsuario > 0m)
				{
					num2 = ((num3 > num4) ? num3 : num4);
				}
				if (!(TotalDescontoAplicado > num2) || !base.Dto.Saida)
				{
					base.Dto.TotalDescontoAplicado = TotalDescontoAplicado;
					AtualizarTotal();
					return;
				}
				if (DialogoFactory.MostrarDialogoSimNao("Limite de desconto atingido. Continuar?", false, 0, HorizontalAlignment.Center).Equals(MessageBoxResult.Yes))
				{
					decimal num5 = TotalDescontoAplicado * 100m / total;
					AutorizacaoDto autorizacao = PermitirVenderAcimaDoLimiteDeDesconto(num5, $"Total da venda: de R$ {total:N2} por R$ {total - TotalDescontoAplicado:N2} ({num5:N2}% de desconto).");
					if (HelperObject.IsNotNull((object)autorizacao))
					{
						num2 = base.Dto.Itens.Where((ItemDto a) => !a.Cancelado).Sum((ItemDto a) => a.TotalBruto * (autorizacao.LimiteDesconto / 100m)) - TotalDescontoItens;
						if (((object)autorizacao.Direito).GetType() != typeof(DireitoDtoNull) || num2 >= TotalDescontoAplicado)
						{
							base.Dto.TotalDescontoAplicado = TotalDescontoAplicado;
							if (((object)autorizacao.Direito).GetType() != typeof(DireitoDtoNull))
							{
								foreach (ItemDto iten in base.Dto.Itens)
								{
									List<AutorizacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<AutorizacaoDto>(iten.Autorizacoes);
									list.Add(autorizacao);
									iten.Autorizacoes = list;
									iten.LimiteDesconto = 99.99m;
								}
								return;
							}
							{
								foreach (ItemDto iten2 in base.Dto.Itens)
								{
									iten2.LimiteDesconto = autorizacao.LimiteDesconto;
								}
								return;
							}
						}
					}
					DialogoFactory.MostrarDialogoConfirma($"Excedido o limite de desconto do usuario informado : {autorizacao.LimiteDesconto}%", false, HorizontalAlignment.Center);
					TotalDescontoAplicado = 0m;
					return;
				}
			}
			DialogoFactory.MostrarDialogoConfirma("Desconto inválido!", false, HorizontalAlignment.Center);
			TotalDescontoAplicado = 0m;
		}

		private AutorizacaoDto PermitirVenderAcimaDoLimiteDeDesconto(decimal descontoPercentual, string detalheSolicitacao)
		{
			return _autenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderAcimaDoLimiteDeDesconto>(descontoPercentual, detalheSolicitacao);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			val.PlanoContaPadrao = PlanoConta;
			val.CentroCustoPadrao = CentroCusto;
			if (AdicionarFrete)
			{
				Lista<FreteDto> fretes = Fretes;
				decimal? obj2;
				if (fretes == null)
				{
					obj2 = null;
				}
				else
				{
					FreteDto itemSelecionado = fretes.ItemSelecionado;
					obj2 = ((itemSelecionado != null) ? new decimal?(itemSelecionado.Valor) : null);
				}
				val.TotalFrete = obj2 ?? 0m;
			}
			val.DescricaoTipoPagamentoDelivery = HelperObject.To<IBasePagamento>((object)OpcoesPagamento.ItemSelecionado)?.Descricao ?? "Não informado";
			if (val.DescricaoTipoPagamentoDelivery.Equals("Dinheiro"))
			{
				val.ValorParaCalculoTroco = HelperObject.To<Dinheiro>((object)OpcoesPagamento.ItemSelecionado)?.ValorParaCalculoTroco ?? 0m;
			}
			base.AtualizarDto<T>(ref dto);
		}

		private void FretesOnAlterouItemSelecionadoEvent(object sender, FreteDto freteDto)
		{
			AtualizarTotal();
		}

		public void AtualizarTotal()
		{
			if (AdicionarFrete)
			{
				decimal total = Total;
				Lista<FreteDto> fretes = Fretes;
				decimal? obj;
				if (fretes == null)
				{
					obj = null;
				}
				else
				{
					FreteDto itemSelecionado = fretes.ItemSelecionado;
					obj = ((itemSelecionado != null) ? new decimal?(itemSelecionado.Valor) : null);
				}
				TotalComFrete = total + (obj ?? 0m) - TotalDescontoAplicado;
			}
			else
			{
				TotalComFrete = Total - TotalDescontoAplicado;
			}
			List<ItemDto> source = HelperIEnumerable.ToListEmptyWhenNull<ItemDto>(base.Dto.Itens.Where((ItemDto x) => !x.Cancelado && x.OperacaoFiscal.CompoeTotalNota));
			TotalDescontoItens = source.Sum((ItemDto x) => x.DescontoDigitado);
			TotalDescontos = TotalDescontoItens + TotalDescontoAplicado;
		}

		public void Totalizar()
		{
			List<ItemDto> source = HelperIEnumerable.ToListEmptyWhenNull<ItemDto>(base.Dto.Itens.Where((ItemDto x) => !x.Cancelado && x.OperacaoFiscal.CompoeTotalNota));
			TotalDescontoAplicado = source.Sum((ItemDto x) => x.DescontoProporcional);
			TotalDescontoItens = source.Sum((ItemDto x) => x.DescontoDigitado);
			TotalDescontos = TotalDescontoItens + TotalDescontoAplicado;
		}

		private void OnAlterouTotal()
		{
			AdicionarFrete = !DadosSistema.ConfiguracaoDto.Saida.DeliveryEntregaGratis.Valor || Total < DadosSistema.ConfiguracaoDto.Saida.ValorMinimoDeliveryEntregaGratis.Valor;
		}

		public override void Dispose()
		{
			Fretes.AlterouItemSelecionadoEvent -= FretesOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class PedidosFormulario : Formulario
	{
		private readonly IInteracaoBarraTarrefaService _interacaoBarraTarrefaService;

		private readonly IWService _service;

		private readonly NavigationBase _navigation;

		private readonly INotificacao _notificacao;

		private readonly IIdentificadorDeChamadaService _identificadorDeChamadaService;

		private IdentificacaoChamadaViewModel _identificacaoChamadaViewModel;

		private ItensViewModel _itemViewModel;

		private TotalizacaoViewModel _totalizacaoViewModel;

		public MovimentacaoDto MovimentacaoDto = (MovimentacaoDto)new MovimentacaoDtoNull();

		private readonly IAutenticacaoService _autenticacaoService;

		private readonly IBalancaService _balancaService;

		private static IEnumerable<PessoaDto> _vendedores = new List<PessoaDto>();

		public event EventHandler<bool> ValidouEvent;

		public PedidosFormulario(IWService service, NavigationBase navigation, INotificacao notificacao, IIdentificadorDeChamadaService identificadorDeChamadaService, IInteracaoBarraTarrefaService interacaoBarraTarrefaService, IAutenticacaoService autenticacaoService, IBalancaService balancaService)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			_interacaoBarraTarrefaService = interacaoBarraTarrefaService;
			_autenticacaoService = autenticacaoService;
			_balancaService = balancaService;
			try
			{
				_service = service;
				_navigation = navigation;
				_notificacao = notificacao;
				_identificadorDeChamadaService = identificadorDeChamadaService;
				CriarAbaPrincipal();
				((Formulario)this).AlterouAbaSelecionadaEvent += OnAlterouAbaSelecionadaEvent;
				_identificadorDeChamadaService.Conectar();
				_identificadorDeChamadaService.IniciarMonitoramento();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message + Environment.NewLine + ex.InnerException?.Message, false, HorizontalAlignment.Center);
			}
		}

		private void CriarAbaPrincipal()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			_identificacaoChamadaViewModel = new IdentificacaoChamadaViewModel(_service, new MovimentacaoDto(), _navigation, _notificacao, _identificadorDeChamadaService, _interacaoBarraTarrefaService);
			_identificacaoChamadaViewModel.IniciouNovaMovimentacao += IdentificacaoChamadaViewModelOnIniciouNovaMovimentacao;
			_identificacaoChamadaViewModel.AlterouTabelaPrecoCalcularItensEvent += PrincipalViewModelOnAlterouTabelaPrecoCalcularItensEvent;
			_identificacaoChamadaViewModel.AlterouVendedorEvent += PrincipalViewModelOnAlterouVendedorEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1]
			{
				new IdentificacaoChamadaAbaViewModel((IViewModelBase)(object)_identificacaoChamadaViewModel)
			});
		}

		private void IdentificacaoChamadaViewModelOnIniciouNovaMovimentacao(object sender, KeyValuePair<MovimentacaoDto, bool> parametros)
		{
			MovimentacaoDto = parametros.Key;
			CarregarVendedores();
			((ViewModelBase<MovimentacaoDto>)(object)_identificacaoChamadaViewModel).UpdateModel(parametros.Key);
			CriarViewModels();
			SolicitarVendedor(parametros.Value);
		}

		private void CarregarVendedores()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			if (!_vendedores.Any())
			{
				PesquisaDto objetoPesquisaVendedores = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
					{
						TipoPessoaCadastro = (TipoPessoaCadastro)6
					},
					Pagina = 1,
					Tamanho = 100
				};
				_vendedores = _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService x) => ((IPesquisaAtivosInativosCustom)x).PesquisarAtivosAsync(objetoPesquisaVendedores))).Result.Lista.Cast<PessoaDto>();
			}
		}

		private void SolicitarVendedor(bool replicando)
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			if (!DadosSistema.ConfiguracaoDto.Saida.SolicitarVendedorAoLancarMovimentacaoSaida.Valor || (HelperObject.IsExactlyObject<PessoaDto>((object)_identificacaoChamadaViewModel.Vendedor) && !replicando))
			{
				return;
			}
			List<PessoaDto> list = new List<PessoaDto>();
			if (DadosSistema.VerificarPermissao<MovimentacaoSaida, PermitirIndicarGarcomNenhum>())
			{
				list.Add(new PessoaDto
				{
					Nome = "Nenhum"
				});
			}
			list.AddRange(_vendedores);
			PessoaDto vendedor = DialogoFactory.MostrarDialogoComboBox<PessoaDto>("Informe o vendedor", "Vendedor", (IEnumerable<PessoaDto>)list, "Nome", false);
			if (vendedor == null || HelperString.IsObjectIdDefaultOrEmpty(((EntityBaseDto)vendedor).Id))
			{
				return;
			}
			try
			{
				CallbackSetVendedorEx.ValidarPin(vendedor, (Action)delegate
				{
					SetarVendedor(vendedor, replicando);
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public void SetarVendedor(PessoaDto vendedor, bool replicando)
		{
			if (HelperObject.IsExactlyObject<PessoaDto>((object)MovimentacaoDto.VendedorResponsavel) && ((EntityBaseDto)MovimentacaoDto.VendedorResponsavel).Id != ((EntityBaseDto)vendedor).Id && DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) == null)
			{
				return;
			}
			MovimentacaoDto.VendedorResponsavel = vendedor;
			_identificacaoChamadaViewModel.Vendedor = vendedor;
			if (!replicando)
			{
				return;
			}
			foreach (Item iten in _itemViewModel.ItensMovimentacao.Itens)
			{
				iten.Vendedor = vendedor;
			}
		}

		private void PrincipalViewModelOnAlterouTabelaPrecoCalcularItensEvent(object sender, TabelaPrecoDto tabelaPrecoDto)
		{
			bool autorizouVenderAbaixoPrecoCusto = false;
			foreach (Item itemDto in _itemViewModel.ItensMovimentacao.Itens)
			{
				((ViewModelPartialBase<ItemDto>)itemDto).Dto.TabelaPreco = tabelaPrecoDto;
				PesquisarProdutoServico(((EntityBaseDto)((ViewModelPartialBase<ItemDto>)itemDto).Dto.ProdutoServico).Id, delegate(ProdutoServicoEmpresaDto produtoServicoEmpresa)
				{
					decimal num = (HelperObject.Is<TabelaPrecoDtoNull>((object)tabelaPrecoDto) ? produtoServicoEmpresa.Preco.PrecoVendaVigente : produtoServicoEmpresa.Preco.ItensTabelaPreco.FirstOrDefault((ItemTabelaPrecoProdutoServicoDto x) => x.TabelaPrecoReferencia == ((EntityBaseDto)tabelaPrecoDto).Id).Preco);
					if (num < produtoServicoEmpresa.Preco.Custo.Valor)
					{
						if (autorizouVenderAbaixoPrecoCusto)
						{
							AlterarPercentualComissaoTabelaPreco(tabelaPrecoDto, ((ViewModelPartialBase<ItemDto>)itemDto).Dto, produtoServicoEmpresa);
							((ViewModelPartialBase<ItemDto>)itemDto).Dto.PrecoUnitarioInformado = num;
						}
						else if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderAbaixoPrecoCusto>(0m, (string)null) != null)
						{
							AlterarPercentualComissaoTabelaPreco(tabelaPrecoDto, ((ViewModelPartialBase<ItemDto>)itemDto).Dto, produtoServicoEmpresa);
							((ViewModelPartialBase<ItemDto>)itemDto).Dto.PrecoUnitarioInformado = num;
							autorizouVenderAbaixoPrecoCusto = true;
						}
					}
					else
					{
						AlterarPercentualComissaoTabelaPreco(tabelaPrecoDto, ((ViewModelPartialBase<ItemDto>)itemDto).Dto, produtoServicoEmpresa);
						((ViewModelPartialBase<ItemDto>)itemDto).Dto.PrecoUnitarioInformado = num;
					}
				});
				_itemViewModel.RecalcularItem(((ViewModelPartialBase<ItemDto>)itemDto).Dto);
			}
			TotalizarMovimentacao();
		}

		private static void AlterarPercentualComissaoTabelaPreco(TabelaPrecoDto tabelaPrecoDto, ItemDto itemDto, ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Invalid comparison between Unknown and I4
			if (!HelperObject.IsNot<TabelaPrecoDtoNull>((object)tabelaPrecoDto) || !(tabelaPrecoDto.PercentualComissao > 0m))
			{
				return;
			}
			if ((int)tabelaPrecoDto.TipoItensTabelaPreco == 1)
			{
				if (itemDto.PrecoUnitarioInformado != produtoServicoEmpresa.Preco.ItensTabelaPreco.FirstOrDefault((ItemTabelaPrecoProdutoServicoDto x) => x.TabelaPrecoReferencia == ((EntityBaseDto)tabelaPrecoDto).Id).Preco)
				{
					itemDto.PercentualComissao = tabelaPrecoDto.PercentualComissao;
				}
			}
			else
			{
				itemDto.PercentualComissao = tabelaPrecoDto.PercentualComissao;
			}
		}

		private void PrincipalViewModelOnAlterouVendedorEvent(object sender, PessoaDto pessoaDto)
		{
			foreach (Item item in _itemViewModel.ItensMovimentacao.Itens.Where((Item item) => HelperObject.Is<PessoaDtoNull>((object)item.Vendedor)))
			{
				item.Vendedor = pessoaDto;
			}
		}

		public void PesquisarProdutoServico(string informacaoPesquisa, Action<ProdutoServicoEmpresaDto> callback)
		{
			try
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>((Func<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdentificador(informacaoPesquisa)));
				if (HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)val) && ValidarProdutoServicoSelecionado(informacaoPesquisa).Execute(val))
				{
					callback(val);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public ValidateParameter<ProdutoServicoEmpresaDto> ValidarProdutoServicoSelecionado(string informacaoPesquisa)
		{
			return new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsExactlyObject<ProdutoDto>((object)x.ProdutoServico.Produto) && x.Vendavel && (int)x.ProdutoServico.Tipo != 5), "Selecione um cadastro do tipo 'Produto' vendável!", HorizontalAlignment.Center);
		}

		protected AutorizacaoDto PermitirVenderEstoqueSaldoNegativo()
		{
			return _autenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderEstoqueSaldoNegativo>(0m, (string)null);
		}

		public void CriarViewModels()
		{
			AtualizarDto();
			if (HelperObject.IsNull((object)_itemViewModel) && HelperObject.IsNull((object)_totalizacaoViewModel))
			{
				_itemViewModel = new ItensViewModel(MovimentacaoDto, _service, _navigation, PermitirVenderEstoqueSaldoNegativo, CapturarPeso, _vendedores);
				_totalizacaoViewModel = new TotalizacaoViewModel(MovimentacaoDto, _service, _autenticacaoService);
				AssinarEventosDasAbas();
				AdicionarAbas();
			}
		}

		private void AssinarEventosDasAbas()
		{
			((BindableBase)_identificacaoChamadaViewModel).PropertyChanged += ItemViewModelOnPropertyChanged;
			((BindableBase)_itemViewModel).PropertyChanged += ItemViewModelOnPropertyChanged;
			((BindableBase)_totalizacaoViewModel).PropertyChanged += ItemViewModelOnPropertyChanged;
		}

		private void ItemViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			OnValidouEvent(((Formulario)this).Valido);
		}

		public void AdicionarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1]
			{
				new ItensAbaViewModel((IViewModelBase)(object)_itemViewModel)
			});
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1]
			{
				new TotalizacaoAbaViewModel((IViewModelBase)(object)_totalizacaoViewModel)
			});
		}

		public void RemoverAbas()
		{
			DesassinarEventosDasAbas();
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is ItensAbaViewModel));
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is TotalizacaoAbaViewModel));
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is IdentificacaoChamadaAbaViewModel));
			_itemViewModel = null;
			_totalizacaoViewModel = null;
		}

		private void DesassinarEventosDasAbas()
		{
			_identificacaoChamadaViewModel.IniciouNovaMovimentacao -= IdentificacaoChamadaViewModelOnIniciouNovaMovimentacao;
			_identificacaoChamadaViewModel.AlterouTabelaPrecoCalcularItensEvent -= PrincipalViewModelOnAlterouTabelaPrecoCalcularItensEvent;
			_identificacaoChamadaViewModel.AlterouVendedorEvent -= PrincipalViewModelOnAlterouVendedorEvent;
			((BindableBase)_identificacaoChamadaViewModel).PropertyChanged -= ItemViewModelOnPropertyChanged;
			((BindableBase)_itemViewModel).PropertyChanged -= ItemViewModelOnPropertyChanged;
			((BindableBase)_totalizacaoViewModel).PropertyChanged -= ItemViewModelOnPropertyChanged;
		}

		private void OnAlterouAbaSelecionadaEvent(object sender, IAbaViewModelBase abaViewModelBase)
		{
			TotalizarMovimentacao();
		}

		public void TotalizarMovimentacao()
		{
			AtualizarDto();
			if (_totalizacaoViewModel != null)
			{
				TotalizacaoViewModel totalizacaoViewModel = _totalizacaoViewModel;
				decimal total = (((MovimentacaoBaseDto)MovimentacaoDto).Total = MovimentacaoDto.Itens.Where((ItemDto x) => !x.Cancelado).Sum((ItemDto x) => x.TotalLiquido));
				totalizacaoViewModel.Total = total;
				_totalizacaoViewModel?.AtualizarTotal();
			}
		}

		public void AtualizarDto()
		{
			ToDto(ref MovimentacaoDto);
			((MovimentacaoBaseDto)MovimentacaoDto).CalculadoPeloSistema = true;
			foreach (ItemDto iten in MovimentacaoDto.Itens)
			{
				iten.CalculadoPeloSistema = ((MovimentacaoBaseDto)MovimentacaoDto).CalculadoPeloSistema;
			}
		}

		private void ToDto(ref MovimentacaoDto movimentacaoDto)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			foreach (IAbaViewModelBase aba in ((Formulario)this).Abas)
			{
				((IViewModelDtoBase)aba.ViewModel).AtualizarDto<MovimentacaoDto>(ref movimentacaoDto);
			}
		}

		public void ReiniciarFormulario()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			string numeroEmEspera = _identificacaoChamadaViewModel.NumeroEmEspera;
			RemoverAbas();
			MovimentacaoDto = (MovimentacaoDto)new MovimentacaoDtoNull();
			CriarAbaPrincipal();
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((Formulario)this).Valido));
			if (!string.IsNullOrEmpty(numeroEmEspera))
			{
				_identificacaoChamadaViewModel.IniciarAtendimento(numeroEmEspera);
			}
		}

		public override void Dispose()
		{
			((Formulario)this).Dispose();
			((Formulario)this).AlterouAbaSelecionadaEvent -= OnAlterouAbaSelecionadaEvent;
			_identificadorDeChamadaService.Desconectar();
			_identificadorDeChamadaService.PararMonitoramento();
		}

		protected virtual void OnValidouEvent(bool e)
		{
			this.ValidouEvent?.Invoke(this, e);
		}

		protected async Task<decimal> CapturarPeso()
		{
			return (await _balancaService.LerPeso()).Peso;
		}
	}
}
namespace Otus.Modulo.Deliveries.Resources
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Messages
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("Otus.Modulo.Deliveries.Resources.Messages", typeof(Messages).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string CadastroTipoVendedor => ResourceManager.GetString("CadastroTipoVendedor", resourceCulture);

		internal Messages()
		{
		}
	}
}
namespace Otus.Modulo.Deliveries.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					resourceMan = new ResourceManager("Otus.Modulo.Deliveries.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal Resources()
		{
		}
	}
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;
	}
}
namespace Otus.Modulo.Deliveries.Events
{
	public class RestaurarResolucaoEvent
	{
	}
}

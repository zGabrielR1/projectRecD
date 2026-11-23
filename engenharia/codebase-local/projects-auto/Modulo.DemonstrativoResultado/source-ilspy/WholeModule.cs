using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.ArquivosFiscais;
using DigisatServer.Application.Main.Dto.Auxiliares.DemonstrativoResultados;
using DigisatServer.Application.Main.Dto.Financeiros.Balancetes;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.FluxosCaixa;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto.Relatorios;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Events;
using DigisatServer.Infrastructure.Crosscutting.Service;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Tipos;
using Otus.Modulo.DemonstrativoResultado.Interfaces;
using Otus.Modulo.DemonstrativoResultado.Reports;
using Otus.Modulo.DemonstrativoResultado.Services;
using Otus.Modulo.DemonstrativoResultado.Types;
using Otus.Modulo.DemonstrativoResultado.ViewModels.AnaliseFinanceira;
using Otus.Modulo.DemonstrativoResultado.ViewModels.AnaliseGeral;
using Otus.Modulo.DemonstrativoResultado.ViewModels.CustoLucroDetalhesViewModel;
using Otus.Modulo.DemonstrativoResultado.ViewModels.DetalhesViewModel;
using Otus.Modulo.DemonstrativoResultado.ViewModels.FinanceiroDetalhesViewModel;
using Otus.Modulo.DemonstrativoResultado.ViewModels.InventarioDetalhesViewModel;
using Otus.Modulo.DemonstrativoResultado.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.DemonstrativoResultado")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.DemonstrativoResultado")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.DemonstrativoResultado
{
	[Module(ModuleName = "DemonstrativoResultadoModulo", OnDemand = true)]
	public class DemonstrativoResultadoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public DemonstrativoResultadoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<DemonstrativoResultadoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DemonstrativoResultadoPrincipalView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<IModoFuncionamentoService, ModoFuncionamentoService>(_container, Array.Empty<InjectionMember>());
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.Views
{
	public class DemonstrativoResultadoPrincipalView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DemonstrativoResultadoPrincipalView()
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
				Uri resourceLocator = new Uri("/Modulo.DemonstrativoResultado;component/views/demonstrativoresultadoprincipalview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class DemonstrativoResultadoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DemonstrativoResultadoView()
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
				Uri resourceLocator = new Uri("/Modulo.DemonstrativoResultado;component/views/demonstrativoresultadoview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
namespace Otus.Modulo.DemonstrativoResultado.ViewModels
{
	public abstract class AnaliseBaseDetalheViewModel<TDto> : BaseDetalheViewModel<TDto> where TDto : class
	{
		protected AnaliseBaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class DemonstrativoResultadoPrincipalViewModel : BaseMinimizar
	{
		private readonly IModoFuncionamentoService _modoFuncionamentoService;

		public Lista<IViewModelDemonstrativoResultado> ViewModels
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<IViewModelDemonstrativoResultado>>((Expression<Func<Lista<IViewModelDemonstrativoResultado>>>)(() => ViewModels));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<IViewModelDemonstrativoResultado>>((Expression<Func<Lista<IViewModelDemonstrativoResultado>>>)(() => ViewModels), value);
			}
		}

		public ICommand ExibirDadosFinanceirosCommand { get; set; }

		public ICommand ExibirDadosInventarioCommand { get; set; }

		public ICommand ExibirDadosCustoLucroMercadoriaCommand { get; set; }

		public ICommand ExibirAnaliseFinanceiraCommand { get; set; }

		public ICommand ExibirAnaliseGeralCommand { get; set; }

		public DemonstrativoResultadoPrincipalViewModel(IModoFuncionamentoService modoFuncionamentoService)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			_modoFuncionamentoService = modoFuncionamentoService;
			ExibirDadosFinanceirosCommand = (ICommand)new DelegateCommand((Action)ExibirDadosFinanceiros, (Func<bool>)PodeExibirDadosFinanceiros, (bool?)null);
			ExibirDadosInventarioCommand = (ICommand)new DelegateCommand((Action)ExibirDadosInventario, (Func<bool>)PodeExibirDadosInventario, (bool?)null);
			ExibirDadosCustoLucroMercadoriaCommand = (ICommand)new DelegateCommand((Action)ExibirDadosCustoLucroMercadoria, (Func<bool>)PodeExibirDadosCustoLucroMercadoria, (bool?)null);
			ExibirAnaliseFinanceiraCommand = (ICommand)new DelegateCommand((Action)ExibirAnaliseFinanceira);
			ExibirAnaliseGeralCommand = (ICommand)new DelegateCommand((Action)ExibirAnaliseGeral);
		}

		private void ExibirAnaliseFinanceira()
		{
			foreach (IViewModelDemonstrativoResultado iten in ViewModels.Itens)
			{
				if (HelperObject.Is<FinanceiroViewModel>((object)iten) && !HelperObject.To<FinanceiroViewModel>((object)iten).RetornouBalanceteAnualPlanosConta)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do balancete anual antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (HelperObject.Is<FinanceiroViewModel>((object)iten) && !HelperObject.To<FinanceiroViewModel>((object)iten).RetornouSaldosContas)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do saldo das contas antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
			}
			ViewModels.Posicionar((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<AnaliseFinanceiraViewModel>((object)vm)));
			HelperObject.To<AnaliseFinanceiraDetalheViewModel>((object)HelperObject.To<AnaliseFinanceiraViewModel>((object)ViewModels.ItemSelecionado).Formulario.ViewModelAbaSelecionada).SetarViewModels(ViewModels);
		}

		private void ExibirAnaliseGeral()
		{
			foreach (IViewModelDemonstrativoResultado iten in ViewModels.Itens)
			{
				if (HelperObject.Is<FinanceiroViewModel>((object)iten) && !HelperObject.To<FinanceiroViewModel>((object)iten).RetornouBalanceteAnualPlanosConta)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do balancete anual antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (HelperObject.Is<FinanceiroViewModel>((object)iten) && !HelperObject.To<FinanceiroViewModel>((object)iten).RetornouSaldosContas)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do saldo das contas antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (HelperObject.Is<FinanceiroViewModel>((object)iten) && !HelperObject.To<FinanceiroViewModel>((object)iten).RetornouRecebimentosPagamentos)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa dos recebimentos e pagamentos antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (HelperObject.Is<InventarioViewModel>((object)iten) && !HelperObject.To<InventarioViewModel>((object)iten).RetornouInventario)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do inventário antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (HelperObject.Is<CustoLucroMercadoriaViewModel>((object)iten) && !HelperObject.To<CustoLucroMercadoriaViewModel>((object)iten).RetornouCustoLucroMercadoria)
				{
					DialogoFactory.MostrarDialogoConfirma("Execute a pesquisa do custo/lucro das mercadorias antes de prosseguir", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
			}
			ViewModels.Posicionar((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<AnaliseGeralViewModel>((object)vm)));
			HelperObject.To<AnaliseGeralDetalheViewModel>((object)HelperObject.To<AnaliseGeralViewModel>((object)ViewModels.ItemSelecionado).Formulario.ViewModelAbaSelecionada).SetarViewModels(ViewModels);
		}

		private void ExibirDadosFinanceiros()
		{
			ViewModels.Posicionar((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<FinanceiroViewModel>((object)vm)));
		}

		private bool PodeExibirDadosFinanceiros()
		{
			return ViewModels?.Existe((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<FinanceiroViewModel>((object)vm))) ?? false;
		}

		private void ExibirDadosInventario()
		{
			ViewModels.Posicionar((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<InventarioViewModel>((object)vm)));
		}

		private bool PodeExibirDadosInventario()
		{
			if (HelperObject.IsNotNull((object)ViewModels) && ViewModels.Existe((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado v) => HelperObject.Is<InventarioViewModel>((object)v))))
			{
				return ViewModels.Itens.Count > 1;
			}
			return false;
		}

		private void ExibirDadosCustoLucroMercadoria()
		{
			ViewModels.Posicionar((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado vm) => HelperObject.Is<CustoLucroMercadoriaViewModel>((object)vm)));
		}

		private bool PodeExibirDadosCustoLucroMercadoria()
		{
			if (HelperObject.IsNotNull((object)ViewModels) && ViewModels.Existe((Expression<Func<IViewModelDemonstrativoResultado, bool>>)((IViewModelDemonstrativoResultado v) => HelperObject.Is<CustoLucroMercadoriaViewModel>((object)v))))
			{
				return ViewModels.Itens.Count > 1;
			}
			return false;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
			ViewModels = new Lista<IViewModelDemonstrativoResultado>();
			ViewModels.Adicionar(_modoFuncionamentoService.ObterViewModels(navigationParameters));
			ViewModels.PosicionarNoPrimeiro();
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= ((BaseMinimizar)this).OnParametroInformado;
			Lista<IViewModelDemonstrativoResultado> viewModels = ViewModels;
			if (viewModels != null)
			{
				((ViewModelValidationBase)viewModels).Dispose();
			}
			ViewModels = null;
			((BaseMinimizar)this).Dispose();
		}
	}
	public class DemonstrativoResultadoViewModel : ModuloBase
	{
		public DemonstrativoResultadoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloDemonstrativoResultadoRegion, typeof(DemonstrativoResultadoPrincipalView).Name, (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.InventarioDetalhesViewModel
{
	public class InventarioAnualDetalheAbaViewModel : AbaViewModelBase
	{
		public InventarioAnualDetalheAbaViewModel(IViewModelBase viewModel)
			: base("INVENTÁRIO ANUAL", "Estoques mensais", viewModel)
		{
		}
	}
	public class InventarioAnualDetalheViewModel : InventarioBaseDetalheViewModel<InventarioAnualItemDto>
	{
		private readonly Action<GeradorArquivoInventarioDto> _pesquisarInventario;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<KeyValuePair<string, ValorEscriturarInventario>> ValoresEscriturarInventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, ValorEscriturarInventario>>>((Expression<Func<Lista<KeyValuePair<string, ValorEscriturarInventario>>>>)(() => ValoresEscriturarInventario));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, ValorEscriturarInventario>>>((Expression<Func<Lista<KeyValuePair<string, ValorEscriturarInventario>>>>)(() => ValoresEscriturarInventario), value);
			}
		}

		public bool GerarEstoqueZeradoOuNegativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GerarEstoqueZeradoOuNegativo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GerarEstoqueZeradoOuNegativo), value);
			}
		}

		public bool MostrarSomenteProdutosAtivos
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarSomenteProdutosAtivos));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarSomenteProdutosAtivos), value);
			}
		}

		public Visibility GraficoItemVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => GraficoItemVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => GraficoItemVisivel), value);
			}
		}

		public Visibility GraficoGeralVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => GraficoGeralVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => GraficoGeralVisivel), value);
			}
		}

		public override string TituloRelatorio => "Inventário anual";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty
		{
			get
			{
				if (GraficoItemVisivel != 0)
				{
					return "InventarioAnualGeralGridControl";
				}
				return "InventarioAnualGridControl";
			}
		}

		public override string NomeArquivoLayoutPadrao => "InventarioAnual.xml";

		public GraficoItem GraficoItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<GraficoItem>((Expression<Func<GraficoItem>>)(() => GraficoItem));
			}
			set
			{
				((BindableBase)this).SetProperty<GraficoItem>((Expression<Func<GraficoItem>>)(() => GraficoItem), value);
			}
		}

		public GraficoItem GraficoGeral
		{
			get
			{
				return ((BindableBase)this).GetProperty<GraficoItem>((Expression<Func<GraficoItem>>)(() => GraficoGeral));
			}
			set
			{
				((BindableBase)this).SetProperty<GraficoItem>((Expression<Func<GraficoItem>>)(() => GraficoGeral), value);
			}
		}

		public ICommand MostrarGraficoItemCommand { get; set; }

		public ICommand MostrarGraficoGeralCommand { get; set; }

		public InventarioAnualDetalheViewModel(Action<GeradorArquivoInventarioDto> pesquisarInventario, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			_pesquisarInventario = pesquisarInventario;
			GraficoGeralVisivel = Visibility.Collapsed;
			GraficoItemVisivel = Visibility.Visible;
			MostrarGraficoItemCommand = (ICommand)new DelegateCommand((Action)MostrarGraficoItem);
			MostrarGraficoGeralCommand = (ICommand)new DelegateCommand((Action)MostrarGraficoGeral);
		}

		private void MostrarGraficoGeral()
		{
			GraficoGeralVisivel = Visibility.Visible;
			GraficoItemVisivel = Visibility.Collapsed;
		}

		private void MostrarGraficoItem()
		{
			GraficoItemVisivel = Visibility.Visible;
			GraficoGeralVisivel = Visibility.Collapsed;
		}

		public override bool PodeImprimirExportar(object arg)
		{
			Lista<InventarioAnualItemDto> lista = base.Lista;
			if (lista != null && lista.Numero > 0)
			{
				return GraficoItemVisivel == Visibility.Visible;
			}
			return false;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			if (DialogoFactory.MostrarDialogoSimNao("Esse processo pode demorar vários minutos. Continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				GeradorArquivoInventarioDto obj = new GeradorArquivoInventarioDto
				{
					Data = AnoBase,
					GerarEstoqueZeradoOuNegativo = GerarEstoqueZeradoOuNegativo,
					ValorEscriturar = ValoresEscriturarInventario.ItemSelecionado.Value,
					MostrarSomenteProdutosAtivos = MostrarSomenteProdutosAtivos
				};
				_pesquisarInventario(obj);
			}
		}

		public void SetarResultado(IEnumerable<InventarioAnualItemDto> resultado)
		{
			base.Lista = new Lista<InventarioAnualItemDto>();
			base.Lista.Adicionar(resultado);
			base.Lista.AlterouItemSelecionadoEvent += GraficoOnAlterouItemSelecionadoEvent;
			base.Lista.PosicionarNoPrimeiro();
			ProcessarGraficoGeral();
		}

		private void GraficoOnAlterouItemSelecionadoEvent(object sender, InventarioAnualItemDto inventarioAnualItemDto)
		{
			if (!HelperObject.IsNull((object)inventarioAnualItemDto))
			{
				GraficoItem = new GraficoItem
				{
					Quantidades = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = inventarioAnualItemDto.InventarioJaneiro.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = inventarioAnualItemDto.InventarioFevereiro.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = inventarioAnualItemDto.InventarioMarco.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = inventarioAnualItemDto.InventarioAbril.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = inventarioAnualItemDto.InventarioMaio.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = inventarioAnualItemDto.InventarioJunho.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = inventarioAnualItemDto.InventarioJulho.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = inventarioAnualItemDto.InventarioAgosto.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = inventarioAnualItemDto.InventarioSetembro.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = inventarioAnualItemDto.InventarioOutubro.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = inventarioAnualItemDto.InventarioNovembro.Quantidade
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = inventarioAnualItemDto.InventarioDezembro.Quantidade
						}
					},
					Precos = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = inventarioAnualItemDto.InventarioJaneiro.Preco
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = inventarioAnualItemDto.InventarioFevereiro.Preco
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = inventarioAnualItemDto.InventarioMarco.Preco
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = inventarioAnualItemDto.InventarioAbril.Preco
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = inventarioAnualItemDto.InventarioMaio.Preco
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = inventarioAnualItemDto.InventarioJunho.Preco
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = inventarioAnualItemDto.InventarioJulho.Preco
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = inventarioAnualItemDto.InventarioAgosto.Preco
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = inventarioAnualItemDto.InventarioSetembro.Preco
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = inventarioAnualItemDto.InventarioOutubro.Preco
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = inventarioAnualItemDto.InventarioNovembro.Preco
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = inventarioAnualItemDto.InventarioDezembro.Preco
						}
					},
					Totais = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = inventarioAnualItemDto.InventarioJaneiro.Total
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = inventarioAnualItemDto.InventarioFevereiro.Total
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = inventarioAnualItemDto.InventarioMarco.Total
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = inventarioAnualItemDto.InventarioAbril.Total
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = inventarioAnualItemDto.InventarioMaio.Total
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = inventarioAnualItemDto.InventarioJunho.Total
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = inventarioAnualItemDto.InventarioJulho.Total
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = inventarioAnualItemDto.InventarioAgosto.Total
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = inventarioAnualItemDto.InventarioSetembro.Total
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = inventarioAnualItemDto.InventarioOutubro.Total
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = inventarioAnualItemDto.InventarioNovembro.Total
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = inventarioAnualItemDto.InventarioDezembro.Total
						}
					}
				};
			}
		}

		private void ProcessarGraficoGeral()
		{
			if (base.Lista.Itens.Any())
			{
				GraficoGeral = new GraficoItem
				{
					Quantidades = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJaneiro.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioFevereiro.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMarco.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAbril.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMaio.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJunho.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJulho.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAgosto.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioSetembro.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioOutubro.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioNovembro.Quantidade)
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioDezembro.Quantidade)
						}
					},
					Precos = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJaneiro.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioFevereiro.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMarco.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAbril.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMaio.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJunho.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJulho.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAgosto.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioSetembro.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioOutubro.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioNovembro.Preco)
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioDezembro.Preco)
						}
					},
					Totais = new List<GraficoItemMes>
					{
						new GraficoItemMes
						{
							Mes = "Janeiro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJaneiro.Total)
						},
						new GraficoItemMes
						{
							Mes = "Feveriro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioFevereiro.Total)
						},
						new GraficoItemMes
						{
							Mes = "Março",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMarco.Total)
						},
						new GraficoItemMes
						{
							Mes = "Abril",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAbril.Total)
						},
						new GraficoItemMes
						{
							Mes = "Maio",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioMaio.Total)
						},
						new GraficoItemMes
						{
							Mes = "Junho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJunho.Total)
						},
						new GraficoItemMes
						{
							Mes = "Julho",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioJulho.Total)
						},
						new GraficoItemMes
						{
							Mes = "Agosto",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioAgosto.Total)
						},
						new GraficoItemMes
						{
							Mes = "Setembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioSetembro.Total)
						},
						new GraficoItemMes
						{
							Mes = "Outubro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioOutubro.Total)
						},
						new GraficoItemMes
						{
							Mes = "Novembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioNovembro.Total)
						},
						new GraficoItemMes
						{
							Mes = "Dezembro",
							Valor = base.Lista.Itens.Sum((InventarioAnualItemDto x) => x.InventarioDezembro.Total)
						}
					}
				};
			}
		}

		public override void Dispose()
		{
			base.Lista.AlterouItemSelecionadoEvent -= GraficoOnAlterouItemSelecionadoEvent;
			base.Dispose();
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			base.Lista = new Lista<InventarioAnualItemDto>();
			AnoBase = DateTime.Now;
			ValoresEscriturarInventario = new Lista<KeyValuePair<string, ValorEscriturarInventario>>();
			ValoresEscriturarInventario.Adicionar((IEnumerable<KeyValuePair<string, ValorEscriturarInventario>>)HelperEnum.ObterParDeValores<ValorEscriturarInventario>());
			ValoresEscriturarInventario.Posicionar((Expression<Func<KeyValuePair<string, ValorEscriturarInventario>, bool>>)((KeyValuePair<string, ValorEscriturarInventario> a) => (int)a.Value == 1));
			MostrarSomenteProdutosAtivos = true;
		}
	}
	public class GraficoItem
	{
		public IEnumerable<GraficoItemMes> Quantidades { get; set; }

		public IEnumerable<GraficoItemMes> Precos { get; set; }

		public IEnumerable<GraficoItemMes> Totais { get; set; }
	}
	public class GraficoItemMes
	{
		public string Mes { get; set; }

		public decimal Valor { get; set; }
	}
	public abstract class InventarioBaseDetalheViewModel<TDto> : BaseDetalheViewModel<TDto> where TDto : class
	{
		protected InventarioBaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class InventarioFormulario : Formulario
	{
		public InventarioFormulario(Action<GeradorArquivoInventarioDto> pesquisarInventario, IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new InventarioAnualDetalheAbaViewModel((IViewModelBase)(object)new InventarioAnualDetalheViewModel(pesquisarInventario, impressaoRelatorioService)) });
		}
	}
	public class InventarioViewModel : ViewModelValidationBase, IViewModelDemonstrativoResultado
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		private InventarioAnualDto _resultado;

		public bool RetornouInventario;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public InventarioViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
			RetornouInventario = false;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new InventarioFormulario(Executar, _impressaoRelatorioService);
		}

		private void Executar(GeradorArquivoInventarioDto geradorArquivoInventarioDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, InventarioAnualDto>((Func<IDemonstrativoResultadoModuleService, InventarioAnualDto>)((IDemonstrativoResultadoModuleService service) => _resultado = service.ObterInventarioMensal(geradorArquivoInventarioDto).Result));
			}, (Action<object>)Retorno);
		}

		private void Retorno(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				RetornouInventario = true;
				if (!_resultado.Itens.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto foi encontrado.", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<InventarioFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<InventarioAnualDetalheViewModel>((object)aba.ViewModel, (Action<InventarioAnualDetalheViewModel>)delegate(InventarioAnualDetalheViewModel x)
					{
						x.SetarResultado(_resultado.Itens);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.FinanceiroDetalhesViewModel
{
	public class FinanceiroBalanceteAnualCentrosCustoDetalheAbaViewModel : AbaViewModelBase
	{
		public FinanceiroBalanceteAnualCentrosCustoDetalheAbaViewModel(IViewModelBase viewModel)
			: base("CENTROS DE CUSTO", "Balancete anual", viewModel)
		{
		}
	}
	public class FinanceiroBalanceteAnualCentrosCustoDetalheViewModel : FinanceiroBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly Action<BalanceteBaseDto> _pesquisarBalanceteCentroCustos;

		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public Lista<KeyValuePair<string, TipoFiltroZerado>> TiposFiltroZerado { get; set; }

		public BalanceteAnualDto Balancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete), value);
			}
		}

		public override string TituloRelatorio => "Balancete anual por centros de custo";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty => "BalanceteAnualCentrosCustoGridControl";

		public override string NomeArquivoLayoutPadrao => "BalanceteAnualCentrosCusto.xml";

		public FinanceiroBalanceteAnualCentrosCustoDetalheViewModel(Action<BalanceteBaseDto> pesquisarBalanceteCentroCustos, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarBalanceteCentroCustos = pesquisarBalanceteCentroCustos;
			_obterContas = obterContas;
			ObterContas();
		}

		private void ObterContas()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			IEnumerable<ContaDto> collection = _obterContas();
			List<ContaDto> list = new List<ContaDto>
			{
				new ContaDto
				{
					Id = string.Empty,
					Descricao = "Todas as contas"
				}
			};
			list.AddRange(collection);
			Contas = new Lista<ContaDto>();
			Contas.Adicionar((IEnumerable<ContaDto>)list);
			Contas.PosicionarNoPrimeiro();
		}

		public override bool PodeImprimirExportar(object arg)
		{
			BalanceteAnualDto balancete = Balancete;
			return ((balancete == null) ? null : balancete.Itens?.Any()) ?? false;
		}

		public override bool PodeImprimirGrafico(object arg)
		{
			return false;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			BalanceteBaseDto obj = new BalanceteBaseDto
			{
				Consolidado = false,
				DataInicial = new DateTime(AnoBase.Year, 1, 1, 0, 0, 0),
				DataFinal = new DateTime(AnoBase.Year, 12, 31, 23, 59, 59),
				ContaReferencia = ((EntityBaseDto)Contas.ItemSelecionado).Id
			};
			_pesquisarBalanceteCentroCustos(obj);
		}

		public void SetarResultado(BalanceteAnualDto resultadoObterBalanceteAnual)
		{
			switch (TiposFiltroZerado.ItemSelecionado.Value)
			{
			case TipoFiltroZerado.MostrarTodos:
				Balancete = resultadoObterBalanceteAnual;
				break;
			case TipoFiltroZerado.EsconderZeradosNoAno:
				resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => !x.ValoresZerados));
				Balancete = resultadoObterBalanceteAnual;
				break;
			case TipoFiltroZerado.EsconderZeradosNoMes:
				switch (DateTime.Now.Month)
				{
				case 1:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJaneiro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 2:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorFevereiro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 3:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorMarco != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 4:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorAbril != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 5:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorMaio != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 6:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJunho != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 7:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJulho != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 8:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorAgosto != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 9:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorSetembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 10:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorOutubro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 11:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorNovembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 12:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorDezembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				}
				break;
			}
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			AnoBase = DateTime.Now;
			base.Lista = new Lista<ContaMovimentacaoDto>();
			TiposFiltroZerado = new Lista<KeyValuePair<string, TipoFiltroZerado>>();
			TiposFiltroZerado.Adicionar((IEnumerable<KeyValuePair<string, TipoFiltroZerado>>)HelperEnum.ObterParDeValores<TipoFiltroZerado>());
			TiposFiltroZerado.PosicionarNoPrimeiro();
		}

		public override void ImprimirExportar(object obj)
		{
			ImpressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)TiposFiltroZerado).Dispose();
			base.Dispose();
		}
	}
	public class FinanceiroBalanceteAnualPlanosContaDetalheViewModel : FinanceiroBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly Action<BalanceteBaseDto> _pesquisarBalancetePlanoContas;

		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public Lista<KeyValuePair<string, TipoFiltroZerado>> TiposFiltroZerado { get; set; }

		public bool MostrarSomenteNiveisQueSomamNoRegistroPai
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarSomenteNiveisQueSomamNoRegistroPai));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarSomenteNiveisQueSomamNoRegistroPai), value);
			}
		}

		public BalanceteAnualDto Balancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete), value);
			}
		}

		public BalanceteAnualDto BalanceteTodos
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => BalanceteTodos));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => BalanceteTodos), value);
			}
		}

		public BalanceteAnualDto BalanceteVisivelDre
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => BalanceteVisivelDre));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => BalanceteVisivelDre), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> Receitas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Receitas));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Receitas), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> ReceitasGrafico
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => ReceitasGrafico));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => ReceitasGrafico), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> Despesas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Despesas));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Despesas), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> DespesasGrafico
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => DespesasGrafico));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => DespesasGrafico), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> Neutras
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Neutras));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => Neutras), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> NeutrasGrafico
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => NeutrasGrafico));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => NeutrasGrafico), value);
			}
		}

		public IEnumerable<ResumoTotais> ResumoTotais
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ResumoTotais>>((Expression<Func<IEnumerable<ResumoTotais>>>)(() => ResumoTotais));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ResumoTotais>>((Expression<Func<IEnumerable<ResumoTotais>>>)(() => ResumoTotais), value);
			}
		}

		public override string TituloRelatorio => "Balancete anual por planos de conta";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty => "BalanceteAnualPlanosContaGridControl";

		public string TagSegundoGridProperty => "ResumoBalanceteAnualPlanosContaGridControl";

		public override string NomeArquivoLayoutPadrao => "BalanceteAnualPlanosConta.xml";

		public BalanceteBaseDto Parametro { get; set; }

		public FinanceiroBalanceteAnualPlanosContaDetalheViewModel(Action<BalanceteBaseDto> pesquisarBalancetePlanoContas, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarBalancetePlanoContas = pesquisarBalancetePlanoContas;
			_obterContas = obterContas;
			ObterContas();
		}

		private void ObterContas()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			IEnumerable<ContaDto> collection = _obterContas();
			List<ContaDto> list = new List<ContaDto>
			{
				new ContaDto
				{
					Id = string.Empty,
					Descricao = "Todas as contas"
				}
			};
			list.AddRange(collection);
			Contas = new Lista<ContaDto>();
			Contas.Adicionar((IEnumerable<ContaDto>)list);
			Contas.PosicionarNoPrimeiro();
		}

		public override bool PodeImprimirExportar(object arg)
		{
			BalanceteAnualDto balancete = Balancete;
			return ((balancete == null) ? null : balancete.Itens?.Any()) ?? false;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Expected O, but got Unknown
			Parametro = new BalanceteBaseDto
			{
				Consolidado = false,
				DataInicial = new DateTime(AnoBase.Year, 1, 1, 0, 0, 0),
				DataFinal = new DateTime(AnoBase.Year, 12, 31, 23, 59, 59),
				ContaReferencia = ((EntityBaseDto)Contas.ItemSelecionado).Id,
				ValorVisivelDre = MostrarSomenteNiveisQueSomamNoRegistroPai
			};
			_pesquisarBalancetePlanoContas(Parametro);
		}

		public void SetarResultado(BalanceteAnualDto resultadoObterBalanceteAnual)
		{
			if (MostrarSomenteNiveisQueSomamNoRegistroPai)
			{
				resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.IsVisivelDre));
			}
			switch (TiposFiltroZerado.ItemSelecionado.Value)
			{
			case TipoFiltroZerado.MostrarTodos:
				Balancete = resultadoObterBalanceteAnual;
				break;
			case TipoFiltroZerado.EsconderZeradosNoAno:
				resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => !x.ValoresZerados));
				Balancete = resultadoObterBalanceteAnual;
				break;
			case TipoFiltroZerado.EsconderZeradosNoMes:
				switch (DateTime.Now.Month)
				{
				case 1:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJaneiro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 2:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorFevereiro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 3:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorMarco != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 4:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorAbril != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 5:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorMaio != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 6:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJunho != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 7:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorJulho != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 8:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorAgosto != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 9:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorSetembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 10:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorOutubro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 11:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorNovembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				case 12:
					resultadoObterBalanceteAnual.Itens = HelperIEnumerable.ToListEmptyWhenNull<BalanceteAnualItemDto>(resultadoObterBalanceteAnual.Itens.Where((BalanceteAnualItemDto x) => x.ValorDezembro != 0m));
					Balancete = resultadoObterBalanceteAnual;
					break;
				}
				break;
			}
			Receitas = Balancete.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Contains("Receita"));
			List<TotalizacaoItemAnualDto> receitasGrafico = Receitas.Select((Func<TotalizacaoItemAnualDto, TotalizacaoItemAnualDto>)((TotalizacaoItemAnualDto receita) => new TotalizacaoItemAnualDto
			{
				Mes = receita.Mes,
				Tipo = receita.Tipo,
				Valor = ((receita.Valor < 0m) ? (receita.Valor * -1m) : receita.Valor)
			})).ToList();
			ReceitasGrafico = receitasGrafico;
			Despesas = Balancete.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Contains("Despesa"));
			List<TotalizacaoItemAnualDto> despesasGrafico = Despesas.Select((Func<TotalizacaoItemAnualDto, TotalizacaoItemAnualDto>)((TotalizacaoItemAnualDto despesa) => new TotalizacaoItemAnualDto
			{
				Mes = despesa.Mes,
				Tipo = despesa.Tipo,
				Valor = ((despesa.Valor < 0m) ? (despesa.Valor * -1m) : despesa.Valor)
			})).ToList();
			DespesasGrafico = despesasGrafico;
			Neutras = Balancete.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Contains("Neutra"));
			List<TotalizacaoItemAnualDto> neutrasGrafico = Neutras.Select((Func<TotalizacaoItemAnualDto, TotalizacaoItemAnualDto>)((TotalizacaoItemAnualDto neutra) => new TotalizacaoItemAnualDto
			{
				Mes = neutra.Mes,
				Tipo = neutra.Tipo,
				Valor = ((neutra.Valor < 0m) ? (neutra.Valor * -1m) : neutra.Valor)
			})).ToList();
			NeutrasGrafico = neutrasGrafico;
			ResumoTotais = ProcessarTotais();
		}

		public void SetarResultadoTodos(BalanceteAnualDto resultadoObterBalanceteAnual)
		{
			BalanceteTodos = resultadoObterBalanceteAnual;
		}

		public void SetarResultadoVisivelDre(BalanceteAnualDto resultadoObterBalanceteAnual)
		{
			BalanceteVisivelDre = resultadoObterBalanceteAnual;
		}

		private IEnumerable<ResumoTotais> ProcessarTotais()
		{
			List<ResumoTotais> list = new List<ResumoTotais>();
			ResumoTotais item = new ResumoTotais
			{
				Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Neutras),
				Janeiro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Janeiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Fevereiro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Fevereiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Marco = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Março")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Abril = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Abril")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Maio = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Maio")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Junho = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Junho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Julho = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Julho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Agosto = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Agosto")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Setembro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Setembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Outubro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Outubro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Novembro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Novembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Dezembro = Neutras.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Dezembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor)
			};
			ResumoTotais resumoTotais = new ResumoTotais
			{
				Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Receitas),
				Janeiro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Janeiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Fevereiro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Fevereiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Marco = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Março")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Abril = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Abril")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Maio = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Maio")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Junho = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Junho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Julho = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Julho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Agosto = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Agosto")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Setembro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Setembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Outubro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Outubro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Novembro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Novembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Dezembro = Receitas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Dezembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor)
			};
			ResumoTotais resumoTotais2 = new ResumoTotais
			{
				Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Despesas),
				Janeiro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Janeiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Fevereiro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Fevereiro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Marco = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Março")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Abril = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Abril")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Maio = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Maio")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Junho = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Junho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Julho = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Julho")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Agosto = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Agosto")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Setembro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Setembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Outubro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Outubro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Novembro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Novembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor),
				Dezembro = Despesas.Where((TotalizacaoItemAnualDto x) => x.Mes.Equals("Dezembro")).Sum((TotalizacaoItemAnualDto x) => x.Valor)
			};
			ResumoTotais item2 = new ResumoTotais
			{
				Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Saldo),
				Janeiro = resumoTotais.Janeiro - resumoTotais2.Janeiro * -1m,
				Fevereiro = resumoTotais.Fevereiro - resumoTotais2.Fevereiro * -1m,
				Marco = resumoTotais.Marco - resumoTotais2.Marco * -1m,
				Abril = resumoTotais.Abril - resumoTotais2.Abril * -1m,
				Maio = resumoTotais.Maio - resumoTotais2.Maio * -1m,
				Junho = resumoTotais.Junho - resumoTotais2.Junho * -1m,
				Julho = resumoTotais.Julho - resumoTotais2.Julho * -1m,
				Agosto = resumoTotais.Agosto - resumoTotais2.Agosto * -1m,
				Setembro = resumoTotais.Setembro - resumoTotais2.Setembro * -1m,
				Outubro = resumoTotais.Outubro - resumoTotais2.Outubro * -1m,
				Novembro = resumoTotais.Novembro - resumoTotais2.Novembro * -1m,
				Dezembro = resumoTotais.Dezembro - resumoTotais2.Dezembro * -1m
			};
			list.Add(item);
			list.Add(resumoTotais);
			list.Add(resumoTotais2);
			list.Add(item2);
			return list;
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			AnoBase = DateTime.Now;
			base.Lista = new Lista<ContaMovimentacaoDto>();
			MostrarSomenteNiveisQueSomamNoRegistroPai = true;
			TiposFiltroZerado = new Lista<KeyValuePair<string, TipoFiltroZerado>>();
			TiposFiltroZerado.Adicionar((IEnumerable<KeyValuePair<string, TipoFiltroZerado>>)HelperEnum.ObterParDeValores<TipoFiltroZerado>());
			TiposFiltroZerado.PosicionarNoPrimeiro();
		}

		public override void ImprimirExportar(object obj)
		{
			ImpressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, TagSegundoGridProperty);
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)TiposFiltroZerado).Dispose();
			base.Dispose();
		}
	}
	public abstract class FinanceiroBaseDetalheViewModel<TDto> : BaseDetalheViewModel<TDto> where TDto : class
	{
		protected FinanceiroBaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class FinanceiroBalanceteAnualPlanosContaDetalheAbaViewModel : AbaViewModelBase
	{
		public FinanceiroBalanceteAnualPlanosContaDetalheAbaViewModel(IViewModelBase viewModel)
			: base("PLANOS DE CONTA", "Balancete anual", viewModel)
		{
		}
	}
	public class ResumoTotais
	{
		public TipoTitulo TipoTitulo { get; set; }

		public string Tipo { get; set; }

		public decimal Janeiro { get; set; }

		public decimal Fevereiro { get; set; }

		public decimal Marco { get; set; }

		public decimal Abril { get; set; }

		public decimal Maio { get; set; }

		public decimal Junho { get; set; }

		public decimal Julho { get; set; }

		public decimal Agosto { get; set; }

		public decimal Setembro { get; set; }

		public decimal Outubro { get; set; }

		public decimal Novembro { get; set; }

		public decimal Dezembro { get; set; }

		public decimal Total => Janeiro + Fevereiro + Marco + Abril + Maio + Junho + Julho + Agosto + Setembro + Outubro + Novembro + Dezembro;

		public decimal? TotalInformado { get; set; }
	}
	public class FinanceiroFormulario : Formulario
	{
		public FinanceiroFormulario(Action<BalanceteBaseDto> pesquisarBalancetePlanoContas, Action<BalanceteBaseDto> pesquisarBalanceteCentroCustos, Action<FluxoCaixaBaseDto> pesquisarContasSaldos, Action<FluxoCaixaBaseDto> pesquisarRecebimentosPagamentos, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[4]
			{
				(IAbaViewModelBase)new FinanceiroBalanceteAnualPlanosContaDetalheAbaViewModel((IViewModelBase)(object)new FinanceiroBalanceteAnualPlanosContaDetalheViewModel(pesquisarBalancetePlanoContas, obterContas, impressaoRelatorioService)),
				(IAbaViewModelBase)new FinanceiroBalanceteAnualCentrosCustoDetalheAbaViewModel((IViewModelBase)(object)new FinanceiroBalanceteAnualCentrosCustoDetalheViewModel(pesquisarBalanceteCentroCustos, obterContas, impressaoRelatorioService)),
				(IAbaViewModelBase)new FinanceiroSaldosContasAnualDetalheAbaViewModel((IViewModelBase)(object)new FinanceiroSaldosContasAnualDetalheViewModel(pesquisarContasSaldos, obterContas, impressaoRelatorioService)),
				(IAbaViewModelBase)new FinanceiroRecebimentoPagamentoAnualDetalheAbaViewModel((IViewModelBase)(object)new FinanceiroRecebimentoPagamentoAnualDetalheViewModel(pesquisarRecebimentosPagamentos, obterContas, impressaoRelatorioService))
			});
		}
	}
	public class FinanceiroRecebimentoPagamentoAnualDetalheViewModel : FinanceiroBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		private readonly Action<FluxoCaixaBaseDto> _pesquisarRecebimentosPagamentos;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public override string TituloRelatorio => "Recebimentos e Pagamentos";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty => "RecebimentoPagamentoAnualGridControl";

		public override string NomeArquivoLayoutPadrao => "RecebimentoPagamentoAnual.xml";

		public RecebimentosPagamentosAnualDto RecebimentosPagamentosAnual
		{
			get
			{
				return ((BindableBase)this).GetProperty<RecebimentosPagamentosAnualDto>((Expression<Func<RecebimentosPagamentosAnualDto>>)(() => RecebimentosPagamentosAnual));
			}
			set
			{
				((BindableBase)this).SetProperty<RecebimentosPagamentosAnualDto>((Expression<Func<RecebimentosPagamentosAnualDto>>)(() => RecebimentosPagamentosAnual), value);
			}
		}

		public FinanceiroRecebimentoPagamentoAnualDetalheViewModel(Action<FluxoCaixaBaseDto> pesquisarRecebimentosPagamentos, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarRecebimentosPagamentos = pesquisarRecebimentosPagamentos;
			_obterContas = obterContas;
			ObterContas();
		}

		private void ObterContas()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			IEnumerable<ContaDto> collection = _obterContas();
			List<ContaDto> list = new List<ContaDto>
			{
				new ContaDto
				{
					Id = string.Empty,
					Descricao = "Todas as contas"
				}
			};
			list.AddRange(collection);
			Contas = new Lista<ContaDto>();
			Contas.Adicionar((IEnumerable<ContaDto>)list);
			Contas.PosicionarNoPrimeiro();
		}

		public override bool PodeImprimirExportar(object arg)
		{
			RecebimentosPagamentosAnualDto recebimentosPagamentosAnual = RecebimentosPagamentosAnual;
			return ((recebimentosPagamentosAnual == null) ? null : recebimentosPagamentosAnual.ValoresMensais?.Any()) ?? false;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			FluxoCaixaBaseDto obj = new FluxoCaixaBaseDto
			{
				Consolidado = false,
				DataInicial = new DateTime(AnoBase.Year, 1, 1, 0, 0, 0),
				DataFinal = new DateTime(AnoBase.Year, 12, 31, 23, 59, 59),
				ContaId = ((EntityBaseDto)Contas.ItemSelecionado).Id
			};
			_pesquisarRecebimentosPagamentos(obj);
		}

		public void SetarResultado(RecebimentosPagamentosAnualDto resultadoObterRecebimentosPagamentos)
		{
			RecebimentosPagamentosAnual = resultadoObterRecebimentosPagamentos;
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			AnoBase = DateTime.Now;
			base.Lista = new Lista<ContaMovimentacaoDto>();
		}
	}
	public class FinanceiroRecebimentoPagamentoAnualDetalheAbaViewModel : AbaViewModelBase
	{
		public FinanceiroRecebimentoPagamentoAnualDetalheAbaViewModel(IViewModelBase viewModel)
			: base("RECEBER/PAGAR", "Valor a receber/pagar e atrasadas", viewModel)
		{
		}
	}
	public class FinanceiroSaldosContasAnualDetalheAbaViewModel : AbaViewModelBase
	{
		public FinanceiroSaldosContasAnualDetalheAbaViewModel(IViewModelBase viewModel)
			: base("SALDO DAS CONTAS", "Saldo real e projetado", viewModel)
		{
		}
	}
	public class FinanceiroSaldosContasAnualDetalheViewModel : FinanceiroBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		private readonly Action<FluxoCaixaBaseDto> _pesquisarSaldosContas;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public bool ProjetadoVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ProjetadoVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ProjetadoVisivel), value);
			}
		}

		public override string TituloRelatorio => "Saldo das contas";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty => "SaldoContaAnualGridControl";

		public override string NomeArquivoLayoutPadrao => "SaldoContaAnual.xml";

		public SaldoContaAnualDto SaldoConta
		{
			get
			{
				return ((BindableBase)this).GetProperty<SaldoContaAnualDto>((Expression<Func<SaldoContaAnualDto>>)(() => SaldoConta));
			}
			set
			{
				((BindableBase)this).SetProperty<SaldoContaAnualDto>((Expression<Func<SaldoContaAnualDto>>)(() => SaldoConta), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> TotaisReal
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisReal));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisReal), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> TotaisProjetado
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisProjetado));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisProjetado), value);
			}
		}

		public IEnumerable<TotalizacaoItemAnualDto> TotaisProjetadoAPartirDaDataAtual
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisProjetadoAPartirDaDataAtual));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<TotalizacaoItemAnualDto>>((Expression<Func<IEnumerable<TotalizacaoItemAnualDto>>>)(() => TotaisProjetadoAPartirDaDataAtual), value);
			}
		}

		public FinanceiroSaldosContasAnualDetalheViewModel(Action<FluxoCaixaBaseDto> pesquisarSaldosContas, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarSaldosContas = pesquisarSaldosContas;
			_obterContas = obterContas;
			ObterContas();
		}

		private void ObterContas()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			IEnumerable<ContaDto> collection = _obterContas();
			List<ContaDto> list = new List<ContaDto>
			{
				new ContaDto
				{
					Id = string.Empty,
					Descricao = "Todas as contas"
				}
			};
			list.AddRange(collection);
			Contas = new Lista<ContaDto>();
			Contas.Adicionar((IEnumerable<ContaDto>)list);
			Contas.PosicionarNoPrimeiro();
		}

		public override bool PodeImprimirExportar(object arg)
		{
			SaldoContaAnualDto saldoConta = SaldoConta;
			return ((saldoConta == null) ? null : saldoConta.SaldosContas?.Any()) ?? false;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			FluxoCaixaBaseDto obj = new FluxoCaixaBaseDto
			{
				Consolidado = false,
				DataInicial = new DateTime(AnoBase.Year, 1, 1, 0, 0, 0),
				DataFinal = new DateTime(AnoBase.Year, 12, 31, 23, 59, 59),
				ContaId = ((EntityBaseDto)Contas.ItemSelecionado).Id
			};
			_pesquisarSaldosContas(obj);
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			AnoBase = DateTime.Now;
			base.Lista = new Lista<ContaMovimentacaoDto>();
			ProjetadoVisivel = true;
		}

		public void SetarResultado(SaldoContaAnualDto resultadoObterSaldosContas)
		{
			SaldoConta = resultadoObterSaldosContas;
			TotaisReal = SaldoConta.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Equals("Real"));
			TotaisProjetado = SaldoConta.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Equals("Projetado"));
			TotaisProjetadoAPartirDaDataAtual = SaldoConta.Totais.Where((TotalizacaoItemAnualDto x) => x.Tipo.Equals("ProjetadoAPartirDaDataAtual"));
		}
	}
	public class FinanceiroViewModel : ViewModelValidationBase, IViewModelDemonstrativoResultado
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		private RecebimentosPagamentosAnualDto _resultadoObterRecebimentosPagamentos;

		private SaldoContaAnualDto _resultadoObterSaldosContas;

		private BalancetesAnuaisPlanoContasDto _resultadoObterBalancetesAnuaisPlanosConta;

		private BalanceteAnualDto _resultadoObterBalanceteAnualCentrosCusto;

		public bool RetornouBalanceteAnualPlanosConta;

		public bool RetornouBalanceteAnualCentrosCusto;

		public bool RetornouSaldosContas;

		public bool RetornouRecebimentosPagamentos;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public FinanceiroViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
			RetornouBalanceteAnualPlanosConta = false;
			RetornouBalanceteAnualCentrosCusto = false;
			RetornouSaldosContas = false;
			RetornouRecebimentosPagamentos = false;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new FinanceiroFormulario(ObterBalanceteAnualPlanoContas, ObterBalanceteAnualCentroCustos, ObterSaldosContas, ObterRecebimentosPagamentos, ObterContas(), _impressaoRelatorioService);
		}

		private Func<IEnumerable<ContaDto>> ObterContas()
		{
			List<ContaDto> contas = null;
			return () => contas ?? (contas = HelperIEnumerable.ToListEmptyWhenNull<ContaDto>(_service.Canal<IContaModuleService, IEnumerable<ContaDto>>((Func<IContaModuleService, IEnumerable<ContaDto>>)((IContaModuleService x) => x.ObterContas()))));
		}

		public void ObterBalanceteAnualPlanoContas(BalanceteBaseDto parametroPesquisaBalanceteDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, BalancetesAnuaisPlanoContasDto>((Func<IDemonstrativoResultadoModuleService, BalancetesAnuaisPlanoContasDto>)((IDemonstrativoResultadoModuleService service) => _resultadoObterBalancetesAnuaisPlanosConta = service.ObterBalancetesAnuaisPlanoContas(parametroPesquisaBalanceteDto).Result));
			}, (Action<object>)delegate(object obj)
			{
				RetornoObterBalanceteAnualPlanosConta(obj, parametroPesquisaBalanceteDto.ValorVisivelDre);
			});
		}

		public void ObterBalanceteAnualCentroCustos(BalanceteBaseDto parametroPesquisaBalanceteDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, BalanceteAnualDto>((Func<IDemonstrativoResultadoModuleService, BalanceteAnualDto>)((IDemonstrativoResultadoModuleService service) => _resultadoObterBalanceteAnualCentrosCusto = service.ObterBalanceteAnualCentroCustos(parametroPesquisaBalanceteDto).Result));
			}, (Action<object>)RetornoObterBalanceteAnualCentrosCusto);
		}

		private void RetornoObterBalanceteAnualPlanosConta(object obj, bool valorVisivelDre)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				BalancetesAnuaisPlanoContasDto resultado = _resultadoObterBalancetesAnuaisPlanosConta;
				if (!HelperObject.IsNotNull((object)resultado))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<FinanceiroFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>)delegate(FinanceiroBalanceteAnualPlanosContaDetalheViewModel x)
					{
						x.SetarResultadoTodos(resultado.Todos);
						x.SetarResultadoVisivelDre(resultado.VisivelDre);
						x.SetarResultado(Copiar(valorVisivelDre ? resultado.VisivelDre : resultado.Todos));
						RetornouBalanceteAnualPlanosConta = true;
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private static BalanceteAnualDto Copiar(BalanceteAnualDto balancete)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Expected O, but got Unknown
			return new BalanceteAnualDto
			{
				IdEmpresa = ((BalanceteBaseDto)balancete).IdEmpresa,
				ValorVisivelDre = ((BalanceteBaseDto)balancete).ValorVisivelDre,
				ContaReferencia = ((BalanceteBaseDto)balancete).ContaReferencia,
				DataFinal = ((BalanceteBaseDto)balancete).DataFinal,
				DataInicial = ((BalanceteBaseDto)balancete).DataInicial,
				Consolidado = ((BalanceteBaseDto)balancete).Consolidado,
				Itens = balancete.Itens,
				Totais = balancete.Totais
			};
		}

		private void RetornoObterBalanceteAnualCentrosCusto(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_resultadoObterBalanceteAnualCentrosCusto))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<FinanceiroFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<FinanceiroBalanceteAnualCentrosCustoDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroBalanceteAnualCentrosCustoDetalheViewModel>)delegate(FinanceiroBalanceteAnualCentrosCustoDetalheViewModel x)
					{
						x.SetarResultado(_resultadoObterBalanceteAnualCentrosCusto);
						RetornouBalanceteAnualCentrosCusto = true;
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public void ObterSaldosContas(FluxoCaixaBaseDto parametroPesquisaSaldoContaDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, SaldoContaAnualDto>((Func<IDemonstrativoResultadoModuleService, SaldoContaAnualDto>)((IDemonstrativoResultadoModuleService service) => _resultadoObterSaldosContas = service.ObterSaldosContasAnual(parametroPesquisaSaldoContaDto).Result));
			}, (Action<object>)RetornoObterSaldosContas);
		}

		private void RetornoObterSaldosContas(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_resultadoObterSaldosContas))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<FinanceiroFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<FinanceiroSaldosContasAnualDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroSaldosContasAnualDetalheViewModel>)delegate(FinanceiroSaldosContasAnualDetalheViewModel x)
					{
						x.SetarResultado(_resultadoObterSaldosContas);
						RetornouSaldosContas = true;
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public void ObterRecebimentosPagamentos(FluxoCaixaBaseDto parametroPesquisaSaldoContaDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, RecebimentosPagamentosAnualDto>((Func<IDemonstrativoResultadoModuleService, RecebimentosPagamentosAnualDto>)((IDemonstrativoResultadoModuleService service) => _resultadoObterRecebimentosPagamentos = service.ObterRecebimentosPagamentosAnual(parametroPesquisaSaldoContaDto).Result));
			}, (Action<object>)RetornoObterRecebimentosPagamentos);
		}

		private void RetornoObterRecebimentosPagamentos(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_resultadoObterRecebimentosPagamentos))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<FinanceiroFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<FinanceiroRecebimentoPagamentoAnualDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroRecebimentoPagamentoAnualDetalheViewModel>)delegate(FinanceiroRecebimentoPagamentoAnualDetalheViewModel x)
					{
						x.SetarResultado(_resultadoObterRecebimentosPagamentos);
						RetornouRecebimentosPagamentos = true;
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.DetalhesViewModel
{
	public abstract class BaseDetalheViewModel<TDto> : ViewModelValidationBase, IViewModelBase, IViewModelImprimirGrid, IViewModelImprimir, IGridControlPesquisa where TDto : class
	{
		public readonly IImpressaoRelatorioService ImpressaoRelatorioService;

		public ICommand PesquisarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand ImprimirGraficoCommand { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public abstract string TituloRelatorio { get; }

		public abstract string SubTituloRelatorio { get; }

		public abstract string TagProperty { get; }

		public abstract string NomeArquivoLayoutPadrao { get; }

		public short Pagina { get; protected set; } = 1;


		public int RegistrosPorPagina
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => RegistrosPorPagina));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => RegistrosPorPagina), value);
			}
		}

		public bool Retrato => false;

		public Lista<TDto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista), value);
			}
		}

		protected BaseDetalheViewModel()
		{
		}

		protected BaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			ImpressaoRelatorioService = impressaoRelatorioService;
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirExportar, (Func<object, bool>)PodeImprimirExportar, (bool?)null);
			ImprimirGraficoCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirGrafico, (Func<object, bool>)PodeImprimirGrafico, (bool?)null);
			RegistrosPorPagina = 30;
		}

		public virtual bool PodeImprimirGrafico(object arg)
		{
			return true;
		}

		public void ImprimirGrafico(object obj)
		{
			DadosSistema.GetImpressaoGraficoService().ImprimirGrafico(obj, TagProperty, TituloRelatorio);
		}

		public abstract bool PodeImprimirExportar(object arg);

		private void Pesquisar()
		{
			Lista.Limpar();
			Pagina = 1;
			ExecutarPesquisa();
		}

		public virtual void ImprimirExportar(object obj)
		{
			ImpressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		public abstract void ExecutarPesquisa();

		public virtual void ProjectedAsViewModel()
		{
		}

		public virtual void ProjectedAsListaViewModel()
		{
			ProjectedAsViewModel();
		}

		public override void Dispose()
		{
			Lista<TDto> lista = Lista;
			if (lista != null)
			{
				((ViewModelValidationBase)lista).Dispose();
			}
			Lista = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.CustoLucroDetalhesViewModel
{
	public class CustoLucroMercadoriaAnualDetalheAbaViewModel : AbaViewModelBase
	{
		public CustoLucroMercadoriaAnualDetalheAbaViewModel(IViewModelBase viewModel)
			: base("CUSTO/LUCRO ANUAL", "Dados mensais", viewModel)
		{
		}
	}
	public class CustoLucroMercadoriaAnualDetalheViewModel : CustoLucroMercadoriaBaseDetalheViewModel<LucroProdutoVendidoDemonstrativoResultadoDto>
	{
		private readonly Action<CustoLucroMercadoriaDto> _pesquisarCustoLucroMercadoria;

		public DateTime AnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => AnoBase), value);
			}
		}

		public Lista<KeyValuePair<string, ValorEscriturarInventario>> ValoresBaseCalculoCusto
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, ValorEscriturarInventario>>>((Expression<Func<Lista<KeyValuePair<string, ValorEscriturarInventario>>>>)(() => ValoresBaseCalculoCusto));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, ValorEscriturarInventario>>>((Expression<Func<Lista<KeyValuePair<string, ValorEscriturarInventario>>>>)(() => ValoresBaseCalculoCusto), value);
			}
		}

		public override string TituloRelatorio => "Custo/Lucro das mercadorias";

		public override string SubTituloRelatorio => $"Ano base: {AnoBase:yyyy}";

		public override string TagProperty => "CustoLucroMercadoriaAnualGridControl";

		public override string NomeArquivoLayoutPadrao => "CustoLucroMercadoriaAnual.xml";

		public IList<ResumoTotais> Resumos
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Resumos));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Resumos), value);
			}
		}

		public Lista<GraficoItem> Grafico
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<GraficoItem>>((Expression<Func<Lista<GraficoItem>>>)(() => Grafico));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<GraficoItem>>((Expression<Func<Lista<GraficoItem>>>)(() => Grafico), value);
			}
		}

		public CustoLucroMercadoriaAnualDetalheViewModel(Action<CustoLucroMercadoriaDto> pesquisarCustoLucroMercadoria, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarCustoLucroMercadoria = pesquisarCustoLucroMercadoria;
		}

		public override bool PodeImprimirExportar(object arg)
		{
			Lista<LucroProdutoVendidoDemonstrativoResultadoDto> lista = base.Lista;
			if (lista == null)
			{
				return false;
			}
			return lista.Numero > 0;
		}

		public override void ExecutarPesquisa()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			if (DialogoFactory.MostrarDialogoSimNao("Esse processo pode demorar vários minutos. Continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				CustoLucroMercadoriaDto obj = new CustoLucroMercadoriaDto
				{
					DataInicial = new DateTime(AnoBase.Year, 1, 1, 0, 0, 0),
					DataFinal = new DateTime(AnoBase.Year, 12, 31, 23, 59, 59),
					ValorEscriturar = ValoresBaseCalculoCusto.ItemSelecionado.Value
				};
				_pesquisarCustoLucroMercadoria(obj);
			}
		}

		public void SetarResultado(IEnumerable<LucroProdutoVendidoDemonstrativoResultadoDto> resultado)
		{
			Grafico = new Lista<GraficoItem>();
			Grafico.Adicionar(resultado.Select((LucroProdutoVendidoDemonstrativoResultadoDto x) => new GraficoItem
			{
				Mes = x.Mes,
				CustoTotal = x.CustoTotal,
				VendaTotal = x.VendaTotalMenosDevolucao,
				ValorLucro = x.ValorLucroMenosDevolucao,
				Devolucao = x.Devolucao
			}));
			base.Lista = new Lista<LucroProdutoVendidoDemonstrativoResultadoDto>();
			base.Lista.Adicionar(resultado);
			base.Lista.PosicionarNoPrimeiro();
			Processar();
		}

		private void Processar()
		{
			Resumos = new List<ResumoTotais>
			{
				new ResumoTotais
				{
					TipoTitulo = TipoTitulo.VendaTotal,
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.VendaTotal),
					Janeiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Janeiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Fevereiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Fevereiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Marco = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Março")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Abril = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Abril")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Maio = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Maio")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Junho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Junho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Julho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Julho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Agosto = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Agosto")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Setembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Setembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Outubro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Outubro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Novembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Novembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					Dezembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Dezembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal),
					TotalInformado = base.Lista.Itens.Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.VendaTotal)
				},
				new ResumoTotais
				{
					TipoTitulo = TipoTitulo.Devolucao,
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Devolucao),
					Janeiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Janeiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Fevereiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Fevereiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Marco = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Março")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Abril = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Abril")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Maio = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Maio")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Junho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Junho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Julho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Julho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Agosto = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Agosto")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Setembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Setembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Outubro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Outubro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Novembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Novembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					Dezembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Dezembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao),
					TotalInformado = base.Lista.Itens.Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Devolucao)
				},
				new ResumoTotais
				{
					TipoTitulo = TipoTitulo.Custo,
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Custo),
					Janeiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Janeiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Fevereiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Fevereiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Marco = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Março")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Abril = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Abril")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Maio = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Maio")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Junho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Junho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Julho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Julho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Agosto = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Agosto")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Setembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Setembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Outubro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Outubro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Novembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Novembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					Dezembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Dezembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal),
					TotalInformado = base.Lista.Itens.Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.CustoTotal)
				},
				new ResumoTotais
				{
					TipoTitulo = TipoTitulo.PercentualLucro,
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.PercentualLucro),
					Janeiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Janeiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Fevereiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Fevereiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Marco = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Março")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Abril = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Abril")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Maio = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Maio")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Junho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Junho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Julho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Julho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Agosto = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Agosto")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Setembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Setembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Outubro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Outubro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Novembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Novembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro),
					Dezembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Dezembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.PercentualLucro)
				},
				new ResumoTotais
				{
					TipoTitulo = TipoTitulo.Lucro,
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.Lucro),
					Janeiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Janeiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Fevereiro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Fevereiro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Marco = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Março")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Abril = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Abril")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Maio = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Maio")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Junho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Junho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Julho = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Julho")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Agosto = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Agosto")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Setembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Setembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Outubro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Outubro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Novembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Novembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					Dezembro = base.Lista.Itens.Where((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.Mes.Equals("Dezembro")).Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao),
					TotalInformado = base.Lista.Itens.Sum((LucroProdutoVendidoDemonstrativoResultadoDto x) => x.ValorLucroMenosDevolucao)
				}
			};
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			base.Lista = new Lista<LucroProdutoVendidoDemonstrativoResultadoDto>();
			AnoBase = DateTime.Now;
			ValoresBaseCalculoCusto = new Lista<KeyValuePair<string, ValorEscriturarInventario>>();
			ValoresBaseCalculoCusto.Adicionar((IEnumerable<KeyValuePair<string, ValorEscriturarInventario>>)HelperEnum.ObterParDeValores<ValorEscriturarInventario>());
			ValoresBaseCalculoCusto.Posicionar((Expression<Func<KeyValuePair<string, ValorEscriturarInventario>, bool>>)((KeyValuePair<string, ValorEscriturarInventario> a) => (int)a.Value == 1));
		}

		public override void Dispose()
		{
			base.Dispose();
			Lista<GraficoItem> grafico = Grafico;
			if (grafico != null)
			{
				((ViewModelValidationBase)grafico).Dispose();
			}
			Grafico = null;
		}
	}
	public class GraficoItem
	{
		public string Mes { get; set; } = string.Empty;


		public decimal CustoTotal { get; set; }

		public decimal VendaTotal { get; set; }

		public decimal ValorLucro { get; set; }

		public decimal Devolucao { get; set; }
	}
	public abstract class CustoLucroMercadoriaBaseDetalheViewModel<TDto> : BaseDetalheViewModel<TDto> where TDto : class
	{
		protected CustoLucroMercadoriaBaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class CustoLucroMercadoriaFormulario : Formulario
	{
		public CustoLucroMercadoriaFormulario(Action<CustoLucroMercadoriaDto> pesquisarInventario, IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new CustoLucroMercadoriaAnualDetalheAbaViewModel((IViewModelBase)(object)new CustoLucroMercadoriaAnualDetalheViewModel(pesquisarInventario, impressaoRelatorioService)) });
		}
	}
	public class CustoLucroMercadoriaViewModel : ViewModelValidationBase, IViewModelDemonstrativoResultado
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		private IEnumerable<LucroProdutoVendidoDemonstrativoResultadoDto> _resultado;

		public bool RetornouCustoLucroMercadoria;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public CustoLucroMercadoriaViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
			RetornouCustoLucroMercadoria = false;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new CustoLucroMercadoriaFormulario(Executar, _impressaoRelatorioService);
		}

		private void Executar(CustoLucroMercadoriaDto custoLucroMercadoriaDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IDemonstrativoResultadoModuleService, IEnumerable<LucroProdutoVendidoDemonstrativoResultadoDto>>((Func<IDemonstrativoResultadoModuleService, IEnumerable<LucroProdutoVendidoDemonstrativoResultadoDto>>)((IDemonstrativoResultadoModuleService service) => _resultado = service.ObterCustoLucroMercadoriaAnual(custoLucroMercadoriaDto).Result));
			}, (Action<object>)Retorno);
		}

		private void Retorno(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				RetornouCustoLucroMercadoria = true;
				if (!_resultado.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto foi encontrado.", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<CustoLucroMercadoriaFormulario>((object)Formulario)).Abas)
				{
					HelperObject.ToDo<CustoLucroMercadoriaAnualDetalheViewModel>((object)aba.ViewModel, (Action<CustoLucroMercadoriaAnualDetalheViewModel>)delegate(CustoLucroMercadoriaAnualDetalheViewModel x)
					{
						x.SetarResultado(_resultado);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.AnaliseGeral
{
	public class AnaliseGeralDetalheViewModel : AnaliseBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private BalanceteAnualDto _balancete;

		public bool MostrarSubNiveisBalancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarSubNiveisBalancete));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarSubNiveisBalancete), value, (Action)ChangedCallback);
			}
		}

		public bool MostrarTotalDisponibilidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarTotalDisponibilidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarTotalDisponibilidade), value, (Action)OnMostrarTotalDisponibilidade);
			}
		}

		public bool MostrarReceitasFaturamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarReceitasFaturamento));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarReceitasFaturamento), value);
			}
		}

		public BalanceteAnualDto Balancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete), value);
			}
		}

		public IList<ResumoTotais> CustoLucroMercadorias
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => CustoLucroMercadorias));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => CustoLucroMercadorias), value);
			}
		}

		public IList<ResumoTotais> ResultadoOperacional
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => ResultadoOperacional));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => ResultadoOperacional), value);
			}
		}

		public IList<SaldoContaAnualItemDto> SaldosContas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<SaldoContaAnualItemDto>>((Expression<Func<IList<SaldoContaAnualItemDto>>>)(() => SaldosContas));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<SaldoContaAnualItemDto>>((Expression<Func<IList<SaldoContaAnualItemDto>>>)(() => SaldosContas), value);
			}
		}

		public RecebimentosPagamentosAnualDto RecebimentosPagamentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<RecebimentosPagamentosAnualDto>((Expression<Func<RecebimentosPagamentosAnualDto>>)(() => RecebimentosPagamentos));
			}
			set
			{
				((BindableBase)this).SetProperty<RecebimentosPagamentosAnualDto>((Expression<Func<RecebimentosPagamentosAnualDto>>)(() => RecebimentosPagamentos), value);
			}
		}

		public IList<ResumoTotais> Recebimentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Recebimentos));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Recebimentos), value);
			}
		}

		public IList<ResumoTotais> Pagamentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Pagamentos));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => Pagamentos), value);
			}
		}

		public IEnumerable<InventarioAnualItemDto> Inventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<InventarioAnualItemDto>>((Expression<Func<IEnumerable<InventarioAnualItemDto>>>)(() => Inventario));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<InventarioAnualItemDto>>((Expression<Func<IEnumerable<InventarioAnualItemDto>>>)(() => Inventario), value);
			}
		}

		public IList<ResumoTotais> InventarioAnual
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => InventarioAnual));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => InventarioAnual), value);
			}
		}

		public IList<ResumoTotais> TotalGeralDisponibilidades
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => TotalGeralDisponibilidades));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => TotalGeralDisponibilidades), value);
			}
		}

		public override string TituloRelatorio => string.Empty;

		public override string SubTituloRelatorio => string.Empty;

		public override string TagProperty => string.Empty;

		public override string NomeArquivoLayoutPadrao => string.Empty;

		public AnaliseGeralDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		private void ChangedCallback()
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			Balancete = (MostrarSubNiveisBalancete ? new BalanceteAnualDto
			{
				Itens = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsVisivelDre && !x.ValoresZerados)
			} : new BalanceteAnualDto
			{
				Itens = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal)
			});
		}

		private void OnMostrarTotalDisponibilidade()
		{
			ProcessarTotalGeralDisponibilidade();
		}

		public override void ImprimirExportar(object obj)
		{
			IList<ResumoTotais> totalGeralDisponibilidades = (MostrarTotalDisponibilidade ? TotalGeralDisponibilidades : ((IList<ResumoTotais>)Enumerable.Empty<ResumoTotais>()));
			Otus.Modulo.DemonstrativoResultado.Reports.AnaliseGeral analiseGeral = new Otus.Modulo.DemonstrativoResultado.Reports.AnaliseGeral(ObjetoImpressaoAnaliseGeral.Criar().ComBalanceteItens(Balancete.Itens).ComSaldosContas(SaldosContas)
				.ComCustoLucroMercadorias(CustoLucroMercadorias)
				.ComInventarioAnual(InventarioAnual)
				.ComResultadoOperacional(ResultadoOperacional)
				.ComPagamentos(Pagamentos)
				.ComRecebimentos(Recebimentos)
				.ComTotalGeralDisponibilidades(totalGeralDisponibilidades));
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)analiseGeral);
		}

		public override bool PodeImprimirExportar(object arg)
		{
			return true;
		}

		public void SetarViewModels(Lista<IViewModelDemonstrativoResultado> viewModels)
		{
			foreach (IViewModelDemonstrativoResultado viewModel in viewModels.Itens)
			{
				HelperObject.ToDo<FinanceiroViewModel>((object)viewModel, (Action<FinanceiroViewModel>)delegate
				{
					foreach (IAbaViewModelBase aba in viewModel.Formulario.Abas)
					{
						HelperObject.ToDo<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>)delegate(FinanceiroBalanceteAnualPlanosContaDetalheViewModel a)
						{
							//IL_0001: Unknown result type (might be due to invalid IL or missing references)
							//IL_0006: Unknown result type (might be due to invalid IL or missing references)
							//IL_001c: Expected O, but got Unknown
							_balancete = new BalanceteAnualDto
							{
								Itens = a.BalanceteVisivelDre.Itens
							};
						});
						HelperObject.ToDo<FinanceiroSaldosContasAnualDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroSaldosContasAnualDetalheViewModel>)delegate(FinanceiroSaldosContasAnualDetalheViewModel a)
						{
							ProcessarTotalRealSaldosContas(HelperIEnumerable.ToListEmptyWhenNull<SaldoContaAnualItemDto>(a.SaldoConta.SaldosContas));
						});
						HelperObject.ToDo<FinanceiroRecebimentoPagamentoAnualDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroRecebimentoPagamentoAnualDetalheViewModel>)delegate(FinanceiroRecebimentoPagamentoAnualDetalheViewModel a)
						{
							RecebimentosPagamentos = a.RecebimentosPagamentosAnual;
							ProcessarRecebimentos();
							ProcessarPagamentos();
						});
					}
				});
				HelperObject.ToDo<CustoLucroMercadoriaViewModel>((object)viewModel, (Action<CustoLucroMercadoriaViewModel>)delegate
				{
					foreach (IAbaViewModelBase aba2 in viewModel.Formulario.Abas)
					{
						HelperObject.ToDo<CustoLucroMercadoriaAnualDetalheViewModel>((object)aba2.ViewModel, (Action<CustoLucroMercadoriaAnualDetalheViewModel>)delegate(CustoLucroMercadoriaAnualDetalheViewModel a)
						{
							CustoLucroMercadorias = a.Resumos;
						});
					}
				});
				HelperObject.ToDo<InventarioViewModel>((object)viewModel, (Action<InventarioViewModel>)delegate
				{
					foreach (IAbaViewModelBase aba3 in viewModel.Formulario.Abas)
					{
						HelperObject.ToDo<InventarioAnualDetalheViewModel>((object)aba3.ViewModel, (Action<InventarioAnualDetalheViewModel>)delegate(InventarioAnualDetalheViewModel a)
						{
							Inventario = a.Lista.Itens;
							ProcessarInventario();
						});
					}
				});
			}
			ProcessarResultadoOperacional();
			ChangedCallback();
		}

		private void ProcessarTotalRealSaldosContas(IList<SaldoContaAnualItemDto> saldosContas)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Expected O, but got Unknown
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Expected O, but got Unknown
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Expected O, but got Unknown
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_0262: Expected O, but got Unknown
			//IL_0262: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Unknown result type (might be due to invalid IL or missing references)
			//IL_0298: Expected O, but got Unknown
			//IL_0299: Expected O, but got Unknown
			SaldoContaAnualItemDto item = new SaldoContaAnualItemDto
			{
				Descricao = "SALDO TOTAL",
				ValorJaneiro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJaneiro.ValorReal)
				},
				ValorFevereiro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorFevereiro.ValorReal)
				},
				ValorMarco = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorMarco.ValorReal)
				},
				ValorAbril = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorAbril.ValorReal)
				},
				ValorMaio = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorMaio.ValorReal)
				},
				ValorJunho = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJunho.ValorReal)
				},
				ValorJulho = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJulho.ValorReal)
				},
				ValorAgosto = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorAgosto.ValorReal)
				},
				ValorSetembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorSetembro.ValorReal)
				},
				ValorOutubro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorOutubro.ValorReal)
				},
				ValorNovembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorNovembro.ValorReal)
				},
				ValorDezembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorDezembro.ValorReal)
				}
			};
			saldosContas.Add(item);
			SaldosContas = new List<SaldoContaAnualItemDto>(saldosContas);
		}

		private void ProcessarResultadoOperacional()
		{
			if (!HelperObject.IsNull((object)CustoLucroMercadorias))
			{
				ResultadoOperacional = new List<ResumoTotais>
				{
					new ResumoTotais
					{
						Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.ResultadoOperacional),
						Janeiro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Janeiro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJaneiro),
						Fevereiro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Fevereiro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorFevereiro),
						Marco = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Marco) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorMarco),
						Abril = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Abril) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorAbril),
						Maio = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Maio) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorMaio),
						Junho = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Junho) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJunho),
						Julho = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Julho) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJulho),
						Agosto = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Agosto) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorAgosto),
						Setembro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Setembro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorSetembro),
						Outubro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Outubro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorOutubro),
						Novembro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Novembro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorNovembro),
						Dezembro = CustoLucroMercadorias.Where((ResumoTotais b) => b.TipoTitulo == TipoTitulo.Lucro).Sum((ResumoTotais x) => x.Dezembro) + _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorDezembro)
					}
				};
			}
		}

		private void ProcessarRecebimentos()
		{
			if (!HelperObject.IsNull((object)RecebimentosPagamentos))
			{
				Recebimentos = new List<ResumoTotais>
				{
					new ResumoTotais
					{
						Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.ContaReceberAberto),
						Janeiro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Janeiro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Fevereiro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Fevereiro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Marco = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Março")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Abril = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Abril")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Maio = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Maio")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Junho = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Junho")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Julho = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Julho")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Agosto = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Agosto")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Setembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Setembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Outubro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Outubro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Novembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Novembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes),
						Dezembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Dezembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalReceberPendentes)
					}
				};
			}
		}

		private void ProcessarPagamentos()
		{
			if (!HelperObject.IsNull((object)RecebimentosPagamentos))
			{
				Pagamentos = new List<ResumoTotais>
				{
					new ResumoTotais
					{
						Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.ContaPagarAberto),
						Janeiro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Janeiro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Fevereiro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Fevereiro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Marco = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Março")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Abril = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Abril")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Maio = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Maio")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Junho = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Junho")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Julho = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Julho")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Agosto = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Agosto")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Setembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Setembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Outubro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Outubro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Novembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Novembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes),
						Dezembro = RecebimentosPagamentos.ValoresMensais.Where((RecebimentosPagamentosAnualMesDto x) => x.Mes.Equals("Dezembro")).Sum((RecebimentosPagamentosAnualMesDto x) => x.ValorTotalPagarPendentes)
					}
				};
			}
		}

		private void ProcessarInventario()
		{
			if (!HelperObject.IsNull((object)Inventario))
			{
				InventarioAnual = new List<ResumoTotais>
				{
					new ResumoTotais
					{
						Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.TotalEstoque),
						Janeiro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioJaneiro.Total),
						Fevereiro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioFevereiro.Total),
						Marco = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioMarco.Total),
						Abril = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioAbril.Total),
						Maio = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioMaio.Total),
						Junho = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioJunho.Total),
						Julho = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioJulho.Total),
						Agosto = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioAgosto.Total),
						Setembro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioSetembro.Total),
						Outubro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioOutubro.Total),
						Novembro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioNovembro.Total),
						Dezembro = Inventario.Sum((InventarioAnualItemDto x) => x.InventarioDezembro.Total)
					}
				};
			}
		}

		private void ProcessarTotalGeralDisponibilidade()
		{
			if (MostrarTotalDisponibilidade)
			{
				SaldoContaAnualItemDto val = SaldosContas.LastOrDefault();
				TotalGeralDisponibilidades = new List<ResumoTotais>
				{
					new ResumoTotais
					{
						Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.TotalDisponibilidade),
						Janeiro = val.ValorJaneiro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Janeiro) - Pagamentos.Sum((ResumoTotais x) => x.Janeiro) + InventarioAnual.Sum((ResumoTotais x) => x.Janeiro),
						Fevereiro = val.ValorFevereiro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Fevereiro) - Pagamentos.Sum((ResumoTotais x) => x.Fevereiro) + InventarioAnual.Sum((ResumoTotais x) => x.Fevereiro),
						Marco = val.ValorMarco.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Marco) - Pagamentos.Sum((ResumoTotais x) => x.Marco) + InventarioAnual.Sum((ResumoTotais x) => x.Marco),
						Abril = val.ValorAbril.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Abril) - Pagamentos.Sum((ResumoTotais x) => x.Abril) + InventarioAnual.Sum((ResumoTotais x) => x.Abril),
						Maio = val.ValorMaio.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Maio) - Pagamentos.Sum((ResumoTotais x) => x.Maio) + InventarioAnual.Sum((ResumoTotais x) => x.Maio),
						Junho = val.ValorJunho.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Junho) - Pagamentos.Sum((ResumoTotais x) => x.Junho) + InventarioAnual.Sum((ResumoTotais x) => x.Junho),
						Julho = val.ValorJulho.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Julho) - Pagamentos.Sum((ResumoTotais x) => x.Julho) + InventarioAnual.Sum((ResumoTotais x) => x.Julho),
						Agosto = val.ValorAgosto.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Agosto) - Pagamentos.Sum((ResumoTotais x) => x.Agosto) + InventarioAnual.Sum((ResumoTotais x) => x.Agosto),
						Setembro = val.ValorSetembro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Setembro) - Pagamentos.Sum((ResumoTotais x) => x.Setembro) + InventarioAnual.Sum((ResumoTotais x) => x.Setembro),
						Outubro = val.ValorOutubro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Outubro) - Pagamentos.Sum((ResumoTotais x) => x.Outubro) + InventarioAnual.Sum((ResumoTotais x) => x.Outubro),
						Novembro = val.ValorNovembro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Novembro) - Pagamentos.Sum((ResumoTotais x) => x.Novembro) + InventarioAnual.Sum((ResumoTotais x) => x.Novembro),
						Dezembro = val.ValorDezembro.ValorReal + Recebimentos.Sum((ResumoTotais x) => x.Dezembro) - Pagamentos.Sum((ResumoTotais x) => x.Dezembro) + InventarioAnual.Sum((ResumoTotais x) => x.Dezembro)
					}
				};
			}
		}

		public override void ExecutarPesquisa()
		{
		}

		public override bool PodeImprimirGrafico(object arg)
		{
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			Balancete = null;
			_balancete = null;
			CustoLucroMercadorias = null;
			ResultadoOperacional = null;
			SaldosContas = null;
			Recebimentos = null;
			Pagamentos = null;
			Inventario = null;
			InventarioAnual = null;
			TotalGeralDisponibilidades = null;
		}
	}
	public class AnaliseGeralViewModel : ViewModelValidationBase, IViewModelDemonstrativoResultado
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public AnaliseGeralViewModel(IImpressaoRelatorioService impressaoRelatorioService)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new AnaliseGeralFormulario(_impressaoRelatorioService);
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnaliseGeralAbaViewModel : AbaViewModelBase
	{
		public AnaliseGeralAbaViewModel(IViewModelBase viewModel)
			: base("ANÁLISE GERAL", "Demonstrativo gerencial", viewModel)
		{
		}
	}
	public class AnaliseGeralFormulario : Formulario
	{
		public AnaliseGeralFormulario(IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnaliseGeralAbaViewModel((IViewModelBase)(object)new AnaliseGeralDetalheViewModel(impressaoRelatorioService)) });
		}
	}
	public class ObjetoImpressaoAnaliseGeral
	{
		public IEnumerable<BalanceteAnualItemDto> BalanceteItens { get; set; }

		public IList<ResumoTotais> CustoLucroMercadorias { get; set; }

		public IList<ResumoTotais> ResultadoOperacional { get; set; }

		public IEnumerable<SaldoContaAnualItemDto> SaldosContas { get; set; }

		public IList<ResumoTotais> Recebimentos { get; set; }

		public IList<ResumoTotais> Pagamentos { get; set; }

		public IList<ResumoTotais> InventarioAnual { get; set; }

		public IList<ResumoTotais> TotalGeralDisponibilidades { get; set; }

		private ObjetoImpressaoAnaliseGeral()
		{
		}

		private ObjetoImpressaoAnaliseGeral(IEnumerable<BalanceteAnualItemDto> balanceteItens, IList<ResumoTotais> custoLucroMercadorias, IList<ResumoTotais> resultadooperacional, IEnumerable<SaldoContaAnualItemDto> saldoscontas, IList<ResumoTotais> recebimentos, IList<ResumoTotais> pagamentos, IList<ResumoTotais> inventarioanual, IList<ResumoTotais> totalgeraldisponibilidades)
		{
			BalanceteItens = balanceteItens;
			CustoLucroMercadorias = custoLucroMercadorias;
			ResultadoOperacional = resultadooperacional;
			SaldosContas = saldoscontas;
			Recebimentos = recebimentos;
			Pagamentos = pagamentos;
			InventarioAnual = inventarioanual;
			TotalGeralDisponibilidades = totalgeraldisponibilidades;
		}

		protected ObjetoImpressaoAnaliseGeral Build()
		{
			return new ObjetoImpressaoAnaliseGeral(BalanceteItens, CustoLucroMercadorias, ResultadoOperacional, SaldosContas, Recebimentos, Pagamentos, InventarioAnual, TotalGeralDisponibilidades);
		}

		public static ObjetoImpressaoAnaliseGeral Criar()
		{
			return new ObjetoImpressaoAnaliseGeral();
		}

		public ObjetoImpressaoAnaliseGeral ComBalanceteItens(IEnumerable<BalanceteAnualItemDto> balanceteItens)
		{
			BalanceteItens = balanceteItens;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComCustoLucroMercadorias(IList<ResumoTotais> custoLucroMercadorias)
		{
			CustoLucroMercadorias = custoLucroMercadorias;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComResultadoOperacional(IList<ResumoTotais> resultadoOperacional)
		{
			ResultadoOperacional = resultadoOperacional;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComSaldosContas(IEnumerable<SaldoContaAnualItemDto> saldosContas)
		{
			SaldosContas = saldosContas;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComRecebimentos(IList<ResumoTotais> recebimentos)
		{
			Recebimentos = recebimentos;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComPagamentos(IList<ResumoTotais> pagamentos)
		{
			Pagamentos = pagamentos;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComInventarioAnual(IList<ResumoTotais> inventarioAnual)
		{
			InventarioAnual = inventarioAnual;
			return Build();
		}

		public ObjetoImpressaoAnaliseGeral ComTotalGeralDisponibilidades(IList<ResumoTotais> totalGeralDisponibilidades)
		{
			TotalGeralDisponibilidades = totalGeralDisponibilidades;
			return Build();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.ViewModels.AnaliseFinanceira
{
	public class AnaliseFinanceiraAbaViewModel : AbaViewModelBase
	{
		public AnaliseFinanceiraAbaViewModel(IViewModelBase viewModel)
			: base("ANÁLISE FINANCEIRA", "Regime de caixa", viewModel)
		{
		}
	}
	public class AnaliseFinanceiraViewModel : ViewModelValidationBase, IViewModelDemonstrativoResultado
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public AnaliseFinanceiraViewModel(IImpressaoRelatorioService impressaoRelatorioService)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new AnaliseFinanceiraFormulario(_impressaoRelatorioService);
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnaliseFinanceiraFormulario : Formulario
	{
		public AnaliseFinanceiraFormulario(IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnaliseFinanceiraAbaViewModel((IViewModelBase)(object)new AnaliseFinanceiraDetalheViewModel(impressaoRelatorioService)) });
		}
	}
	public class AnaliseFinanceiraDetalheViewModel : AnaliseBaseDetalheViewModel<ContaMovimentacaoDto>
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private BalanceteAnualDto _balancete;

		public bool MostrarSubNiveisBalancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarSubNiveisBalancete));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarSubNiveisBalancete), value, (Action)ChangedCallback);
			}
		}

		public BalanceteAnualDto Balancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteAnualDto>((Expression<Func<BalanceteAnualDto>>)(() => Balancete), value);
			}
		}

		public IList<ResumoTotais> FluxoCaixa
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => FluxoCaixa));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<ResumoTotais>>((Expression<Func<IList<ResumoTotais>>>)(() => FluxoCaixa), value);
			}
		}

		public IList<SaldoContaAnualItemDto> SaldosContas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<SaldoContaAnualItemDto>>((Expression<Func<IList<SaldoContaAnualItemDto>>>)(() => SaldosContas));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<SaldoContaAnualItemDto>>((Expression<Func<IList<SaldoContaAnualItemDto>>>)(() => SaldosContas), value);
			}
		}

		public override string TituloRelatorio => string.Empty;

		public override string SubTituloRelatorio => string.Empty;

		public override string TagProperty => string.Empty;

		public override string NomeArquivoLayoutPadrao => string.Empty;

		public AnaliseFinanceiraDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		private void ChangedCallback()
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			Balancete = (MostrarSubNiveisBalancete ? new BalanceteAnualDto
			{
				Itens = _balancete.Itens.Where((BalanceteAnualItemDto x) => !x.ValoresZerados)
			} : new BalanceteAnualDto
			{
				Itens = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal)
			});
		}

		public override void ImprimirExportar(object obj)
		{
			Otus.Modulo.DemonstrativoResultado.Reports.AnaliseFinanceira analiseFinanceira = new Otus.Modulo.DemonstrativoResultado.Reports.AnaliseFinanceira(ObjetoImpressaoAnaliseFinanceira.Criar().ComBalanceteItens(Balancete.Itens).ComSaldosContas(SaldosContas)
				.ComFluxoCaixa(FluxoCaixa));
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)analiseFinanceira);
		}

		public override bool PodeImprimirExportar(object arg)
		{
			return true;
		}

		public override bool PodeImprimirGrafico(object arg)
		{
			return false;
		}

		public void SetarViewModels(Lista<IViewModelDemonstrativoResultado> viewModels)
		{
			foreach (IViewModelDemonstrativoResultado viewModel in viewModels.Itens)
			{
				HelperObject.ToDo<FinanceiroViewModel>((object)viewModel, (Action<FinanceiroViewModel>)delegate
				{
					foreach (IAbaViewModelBase aba in viewModel.Formulario.Abas)
					{
						HelperObject.ToDo<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroBalanceteAnualPlanosContaDetalheViewModel>)delegate(FinanceiroBalanceteAnualPlanosContaDetalheViewModel a)
						{
							//IL_0001: Unknown result type (might be due to invalid IL or missing references)
							//IL_0006: Unknown result type (might be due to invalid IL or missing references)
							//IL_001c: Expected O, but got Unknown
							_balancete = new BalanceteAnualDto
							{
								Itens = a.BalanceteTodos.Itens
							};
						});
						HelperObject.ToDo<FinanceiroSaldosContasAnualDetalheViewModel>((object)aba.ViewModel, (Action<FinanceiroSaldosContasAnualDetalheViewModel>)delegate(FinanceiroSaldosContasAnualDetalheViewModel a)
						{
							ProcessarTotalRealSaldosContas(HelperIEnumerable.ToListEmptyWhenNull<SaldoContaAnualItemDto>(a.SaldoConta.SaldosContas));
						});
					}
				});
			}
			ProcessarFluxoCaixa();
			ChangedCallback();
		}

		private void ProcessarTotalRealSaldosContas(IList<SaldoContaAnualItemDto> saldosContas)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Expected O, but got Unknown
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Expected O, but got Unknown
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Expected O, but got Unknown
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_0262: Expected O, but got Unknown
			//IL_0262: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Unknown result type (might be due to invalid IL or missing references)
			//IL_0298: Expected O, but got Unknown
			//IL_0299: Expected O, but got Unknown
			SaldoContaAnualItemDto item = new SaldoContaAnualItemDto
			{
				Descricao = "SALDO TOTAL",
				ValorJaneiro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJaneiro.ValorReal)
				},
				ValorFevereiro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorFevereiro.ValorReal)
				},
				ValorMarco = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorMarco.ValorReal)
				},
				ValorAbril = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorAbril.ValorReal)
				},
				ValorMaio = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorMaio.ValorReal)
				},
				ValorJunho = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJunho.ValorReal)
				},
				ValorJulho = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorJulho.ValorReal)
				},
				ValorAgosto = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorAgosto.ValorReal)
				},
				ValorSetembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorSetembro.ValorReal)
				},
				ValorOutubro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorOutubro.ValorReal)
				},
				ValorNovembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorNovembro.ValorReal)
				},
				ValorDezembro = new SaldoContaAnualMesDto
				{
					ValorReal = saldosContas.Sum((SaldoContaAnualItemDto x) => x.ValorDezembro.ValorReal)
				}
			};
			saldosContas.Add(item);
			SaldosContas = new List<SaldoContaAnualItemDto>(saldosContas);
		}

		private void ProcessarFluxoCaixa()
		{
			FluxoCaixa = new List<ResumoTotais>
			{
				new ResumoTotais
				{
					Tipo = HelperEnum.RetornarDescricao((Enum)TipoTitulo.FluxoCaixa),
					Janeiro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJaneiro),
					Fevereiro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorFevereiro),
					Marco = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorMarco),
					Abril = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorAbril),
					Maio = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorMaio),
					Junho = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJunho),
					Julho = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorJulho),
					Agosto = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorAgosto),
					Setembro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorSetembro),
					Outubro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorOutubro),
					Novembro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorNovembro),
					Dezembro = _balancete.Itens.Where((BalanceteAnualItemDto x) => x.IsPrincipal).Sum((BalanceteAnualItemDto x) => x.ValorDezembro)
				}
			};
		}

		public override void ExecutarPesquisa()
		{
		}

		public override void Dispose()
		{
			base.Dispose();
			Balancete = null;
			_balancete = null;
			FluxoCaixa = null;
			SaldosContas = null;
		}
	}
	public class ObjetoImpressaoAnaliseFinanceira
	{
		public IEnumerable<BalanceteAnualItemDto> BalanceteItens { get; set; }

		public IEnumerable<SaldoContaAnualItemDto> SaldosContas { get; set; }

		public IEnumerable<ResumoTotais> FluxosCaixa { get; set; }

		private ObjetoImpressaoAnaliseFinanceira()
		{
		}

		private ObjetoImpressaoAnaliseFinanceira(IEnumerable<BalanceteAnualItemDto> balanceteItens, IEnumerable<SaldoContaAnualItemDto> saldoscontas, IEnumerable<ResumoTotais> fluxosCaixa)
		{
			BalanceteItens = balanceteItens;
			SaldosContas = saldoscontas;
			FluxosCaixa = fluxosCaixa;
		}

		protected ObjetoImpressaoAnaliseFinanceira Build()
		{
			return new ObjetoImpressaoAnaliseFinanceira(BalanceteItens, SaldosContas, FluxosCaixa);
		}

		public static ObjetoImpressaoAnaliseFinanceira Criar()
		{
			return new ObjetoImpressaoAnaliseFinanceira();
		}

		public ObjetoImpressaoAnaliseFinanceira ComBalanceteItens(IEnumerable<BalanceteAnualItemDto> balanceteItens)
		{
			BalanceteItens = balanceteItens;
			return Build();
		}

		public ObjetoImpressaoAnaliseFinanceira ComSaldosContas(IEnumerable<SaldoContaAnualItemDto> saldosContas)
		{
			SaldosContas = saldosContas;
			return Build();
		}

		public ObjetoImpressaoAnaliseFinanceira ComFluxoCaixa(IEnumerable<ResumoTotais> fluxosCaixa)
		{
			FluxosCaixa = fluxosCaixa;
			return Build();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.Types
{
	public enum TipoTitulo
	{
		[Descricao("Custo das mercadorias")]
		Custo,
		[Descricao("Lucro bruto")]
		Lucro,
		[Descricao("Venda total")]
		VendaTotal,
		[Descricao("Percentual de lucro")]
		PercentualLucro,
		[Descricao("* FLUXO DE CAIXA")]
		FluxoCaixa,
		[Descricao("** RESULTADO OPERACIONAL")]
		ResultadoOperacional,
		[Descricao("CONTAS A RECEBER (EM ABERTO)")]
		ContaReceberAberto,
		[Descricao("CONTAS A PAGAR (EM ABERTO)")]
		ContaPagarAberto,
		[Descricao("TOTAL ESTOQUE")]
		TotalEstoque,
		[Descricao("*** TOTAL DE DISPONIBILIDADE")]
		TotalDisponibilidade,
		[Descricao("Neutras")]
		Neutras,
		[Descricao("Receitas")]
		Receitas,
		[Descricao("Despesas")]
		Despesas,
		[Descricao("Saldo")]
		Saldo,
		[Descricao("Devolução")]
		Devolucao
	}
	public enum TipoFiltroZerado
	{
		[Descricao("Mostrar todos")]
		MostrarTodos,
		[Descricao("Esconder zerados no ano")]
		EsconderZeradosNoAno,
		[Descricao("Esconder zerados no mês atual")]
		EsconderZeradosNoMes
	}
}
namespace Otus.Modulo.DemonstrativoResultado.Services
{
	public class ModoFuncionamentoService : IModoFuncionamentoService
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		public ModoFuncionamentoService(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
		}

		public IViewModelDemonstrativoResultado[] ObterViewModels(NavigationParameters parametros)
		{
			return new IViewModelDemonstrativoResultado[5]
			{
				CriarProjetarViewModel(typeof(FinanceiroViewModel), _service, _impressaoRelatorioService, _executarProcessoService),
				CriarProjetarViewModel(typeof(InventarioViewModel), _service, _impressaoRelatorioService, _executarProcessoService),
				CriarProjetarViewModel(typeof(CustoLucroMercadoriaViewModel), _service, _impressaoRelatorioService, _executarProcessoService),
				CriarProjetarViewModel(typeof(AnaliseFinanceiraViewModel), _impressaoRelatorioService),
				CriarProjetarViewModel(typeof(AnaliseGeralViewModel), _impressaoRelatorioService)
			};
		}

		private IViewModelDemonstrativoResultado CriarProjetarViewModel(Type tipoViewModel, params object[] parametros)
		{
			IViewModelDemonstrativoResultado viewModelDemonstrativoResultado = CriarViewModel(tipoViewModel, parametros);
			viewModelDemonstrativoResultado.CriarFormulario();
			return viewModelDemonstrativoResultado;
		}

		private IViewModelDemonstrativoResultado CriarViewModel(Type tipoViewModel, params object[] parametros)
		{
			return Activator.CreateInstance(tipoViewModel, parametros) as IViewModelDemonstrativoResultado;
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.Reports
{
	public class AnaliseGeral : XtraReport, IRelatorioParametros, IViewModelItemImprimirRelatorio, IViewModelImprimir, IDisposable
	{
		private readonly ObjetoImpressaoAnaliseGeral _objetoImpressaoAnaliseGeral;

		private bool _favorito;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRLabel lblCodigoUnico;

		private XRLabel xrLabel14;

		private XRLabel xrLabel47;

		private XRLabel xrLabel37;

		private XRLabel xrLabel40;

		private XRLabel xrLabel41;

		private XRLabel xrLabel42;

		private XRLabel xrLabel43;

		private XRLabel xrLabel44;

		private XRLabel xrLabel45;

		private XRLabel xrLabel46;

		private XRLabel xrLabel48;

		private XRLabel xrLabel49;

		private XRLabel xrLabel50;

		private XRLabel xrLabel51;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel lblValorTotal;

		private XRLabel lblValorDezembro;

		private XRLabel lblValorNovembro;

		private XRLabel lblValorOutubro;

		private XRLabel lblValorSetembro;

		private XRLabel lblValorAgosto;

		private XRLabel lblValorJulho;

		private XRLabel lblValorJunho;

		private XRLabel lblValorMaio;

		private XRLabel lblValorAbril;

		private XRLabel lblValorMarco;

		private XRLabel lblValorFevereiro;

		private XRLabel lblValorJaneiro;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLabel xrLabel22;

		private XRLabel xrLabel23;

		private XRLabel xrLabel24;

		private XRLabel xrLabel30;

		private XRLabel xrLabel31;

		private XRLabel xrLabel32;

		private XRLabel xrLabel34;

		private XRLabel xrLabel39;

		private XRLabel xrLabel52;

		private XRLabel xrLabel53;

		private XRLabel xrLabel54;

		private XRLabel xrLabel55;

		private XRLabel xrLabel56;

		private XRLabel xrLabel57;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private XRLabel xrLabel38;

		private XRLabel xrLabel58;

		private XRLabel xrLabel59;

		private XRLabel xrLabel60;

		private XRLabel xrLabel61;

		private XRLabel xrLabel62;

		private XRLabel xrLabel63;

		private XRLabel xrLabel64;

		private XRLabel xrLabel65;

		private XRLabel xrLabel66;

		private XRLabel xrLabel67;

		private XRLabel xrLabel68;

		private XRLabel xrLabel69;

		private XRLabel xrLabel70;

		private DetailReportBand DetailReport4;

		private DetailBand Detail5;

		private XRLabel xrLabel73;

		private XRLabel xrLabel74;

		private XRLabel xrLabel75;

		private XRLabel xrLabel76;

		private XRLabel xrLabel77;

		private XRLabel xrLabel78;

		private XRLabel xrLabel79;

		private XRLabel xrLabel80;

		private XRLabel xrLabel81;

		private XRLabel xrLabel82;

		private XRLabel xrLabel83;

		private XRLabel xrLabel84;

		private XRLabel xrLabel85;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel71;

		private DetailReportBand DetailReport5;

		private DetailBand Detail6;

		private XRLabel xrLabel72;

		private XRLabel xrLabel86;

		private XRLabel xrLabel87;

		private XRLabel xrLabel88;

		private XRLabel xrLabel89;

		private XRLabel xrLabel90;

		private XRLabel xrLabel91;

		private XRLabel xrLabel92;

		private XRLabel xrLabel93;

		private XRLabel xrLabel94;

		private XRLabel xrLabel95;

		private XRLabel xrLabel96;

		private XRLabel xrLabel97;

		private XRLabel xrLabel98;

		private DetailReportBand DetailReport6;

		private DetailBand Detail7;

		private XRLabel xrLabel99;

		private XRLabel xrLabel100;

		private XRLabel xrLabel101;

		private XRLabel xrLabel102;

		private XRLabel xrLabel103;

		private XRLabel xrLabel104;

		private XRLabel xrLabel105;

		private XRLabel xrLabel106;

		private XRLabel xrLabel107;

		private XRLabel xrLabel108;

		private XRLabel xrLabel109;

		private XRLabel xrLabel110;

		private XRLabel xrLabel111;

		private XRLabel xrLabel112;

		private DetailReportBand DetailReport7;

		private DetailBand Detail8;

		private XRLabel xrLabel113;

		private XRLabel xrLabel114;

		private XRLabel xrLabel115;

		private XRLabel xrLabel116;

		private XRLabel xrLabel117;

		private XRLabel xrLabel118;

		private XRLabel xrLabel119;

		private XRLabel xrLabel120;

		private XRLabel xrLabel121;

		private XRLabel xrLabel122;

		private XRLabel xrLabel123;

		private XRLabel xrLabel124;

		private XRLabel xrLabel125;

		private XRLabel xrLabel126;

		private DetailReportBand DetailReport8;

		private DetailBand Detail9;

		private XRLabel xrLabel127;

		private XRLabel xrLabel128;

		private XRLabel xrLabel129;

		private XRLabel xrLabel130;

		private XRLabel xrLabel131;

		private XRLabel xrLabel132;

		private XRLabel xrLabel133;

		private XRLabel xrLabel134;

		private XRLabel xrLabel135;

		private XRLabel xrLabel136;

		private XRLabel xrLabel137;

		private XRLabel xrLabel138;

		private XRLabel xrLabel139;

		private XRLabel xrLabel140;

		private FormattingRule UltimoNivelComNegrito;

		private FormattingRule UltimoNivelSemNegrito;

		private GroupHeaderBand GroupHeader2;

		public string TituloRelatorio => "Análise geral do demonstrativo de resultados";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => false;

		public IList<TipoViewModelBase> Parametros { get; protected set; } = new List<TipoViewModelBase>();


		public string DescricaoDetalhada => string.Empty;

		public bool Favorito
		{
			get
			{
				return _favorito;
			}
			set
			{
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0018: Expected O, but got Unknown
				_favorito = value;
				DadosSistema.GetEventPublisher().Publish<FavoritouRelatorioEvent>(new FavoritouRelatorioEvent((XtraReport)(object)this, value));
			}
		}

		public bool FavoritouRelatorio { get; set; }

		public AnaliseGeral(ObjetoImpressaoAnaliseGeral objetoImpressaoAnaliseGeral)
		{
			_objetoImpressaoAnaliseGeral = objetoImpressaoAnaliseGeral;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			((XtraReportBase)this).DataSource = new List<ObjetoImpressaoAnaliseGeral> { _objetoImpressaoAnaliseGeral };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		public void SetarService(IWService service)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((XtraReport)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Expected O, but got Unknown
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected O, but got Unknown
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Expected O, but got Unknown
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Expected O, but got Unknown
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Expected O, but got Unknown
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Expected O, but got Unknown
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Expected O, but got Unknown
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Expected O, but got Unknown
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Expected O, but got Unknown
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Expected O, but got Unknown
			//IL_0153: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Expected O, but got Unknown
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0168: Expected O, but got Unknown
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Expected O, but got Unknown
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Expected O, but got Unknown
			//IL_017f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0189: Expected O, but got Unknown
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Expected O, but got Unknown
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Expected O, but got Unknown
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Expected O, but got Unknown
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Expected O, but got Unknown
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Expected O, but got Unknown
			//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Expected O, but got Unknown
			//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Expected O, but got Unknown
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Expected O, but got Unknown
			//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fd: Expected O, but got Unknown
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Expected O, but got Unknown
			//IL_0209: Unknown result type (might be due to invalid IL or missing references)
			//IL_0213: Expected O, but got Unknown
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Expected O, but got Unknown
			//IL_021f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0229: Expected O, but got Unknown
			//IL_022a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0234: Expected O, but got Unknown
			//IL_0235: Unknown result type (might be due to invalid IL or missing references)
			//IL_023f: Expected O, but got Unknown
			//IL_0240: Unknown result type (might be due to invalid IL or missing references)
			//IL_024a: Expected O, but got Unknown
			//IL_024b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0255: Expected O, but got Unknown
			//IL_0256: Unknown result type (might be due to invalid IL or missing references)
			//IL_0260: Expected O, but got Unknown
			//IL_0261: Unknown result type (might be due to invalid IL or missing references)
			//IL_026b: Expected O, but got Unknown
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0276: Expected O, but got Unknown
			//IL_0277: Unknown result type (might be due to invalid IL or missing references)
			//IL_0281: Expected O, but got Unknown
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Expected O, but got Unknown
			//IL_028d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0297: Expected O, but got Unknown
			//IL_0298: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a2: Expected O, but got Unknown
			//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ad: Expected O, but got Unknown
			//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b8: Expected O, but got Unknown
			//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c3: Expected O, but got Unknown
			//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ce: Expected O, but got Unknown
			//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Expected O, but got Unknown
			//IL_02da: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Expected O, but got Unknown
			//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ef: Expected O, but got Unknown
			//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fa: Expected O, but got Unknown
			//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0305: Expected O, but got Unknown
			//IL_0306: Unknown result type (might be due to invalid IL or missing references)
			//IL_0310: Expected O, but got Unknown
			//IL_0311: Unknown result type (might be due to invalid IL or missing references)
			//IL_031b: Expected O, but got Unknown
			//IL_031c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0326: Expected O, but got Unknown
			//IL_0327: Unknown result type (might be due to invalid IL or missing references)
			//IL_0331: Expected O, but got Unknown
			//IL_0332: Unknown result type (might be due to invalid IL or missing references)
			//IL_033c: Expected O, but got Unknown
			//IL_033d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0347: Expected O, but got Unknown
			//IL_0348: Unknown result type (might be due to invalid IL or missing references)
			//IL_0352: Expected O, but got Unknown
			//IL_0353: Unknown result type (might be due to invalid IL or missing references)
			//IL_035d: Expected O, but got Unknown
			//IL_035e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0368: Expected O, but got Unknown
			//IL_0369: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Expected O, but got Unknown
			//IL_0374: Unknown result type (might be due to invalid IL or missing references)
			//IL_037e: Expected O, but got Unknown
			//IL_037f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0389: Expected O, but got Unknown
			//IL_038a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0394: Expected O, but got Unknown
			//IL_0395: Unknown result type (might be due to invalid IL or missing references)
			//IL_039f: Expected O, but got Unknown
			//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Expected O, but got Unknown
			//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b5: Expected O, but got Unknown
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Expected O, but got Unknown
			//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Expected O, but got Unknown
			//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d6: Expected O, but got Unknown
			//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e1: Expected O, but got Unknown
			//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ec: Expected O, but got Unknown
			//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Expected O, but got Unknown
			//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0402: Expected O, but got Unknown
			//IL_0403: Unknown result type (might be due to invalid IL or missing references)
			//IL_040d: Expected O, but got Unknown
			//IL_040e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0418: Expected O, but got Unknown
			//IL_0419: Unknown result type (might be due to invalid IL or missing references)
			//IL_0423: Expected O, but got Unknown
			//IL_0424: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Expected O, but got Unknown
			//IL_042f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Expected O, but got Unknown
			//IL_043a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0444: Expected O, but got Unknown
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_044f: Expected O, but got Unknown
			//IL_0450: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Expected O, but got Unknown
			//IL_045b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0465: Expected O, but got Unknown
			//IL_0466: Unknown result type (might be due to invalid IL or missing references)
			//IL_0470: Expected O, but got Unknown
			//IL_0471: Unknown result type (might be due to invalid IL or missing references)
			//IL_047b: Expected O, but got Unknown
			//IL_047c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0486: Expected O, but got Unknown
			//IL_0487: Unknown result type (might be due to invalid IL or missing references)
			//IL_0491: Expected O, but got Unknown
			//IL_0492: Unknown result type (might be due to invalid IL or missing references)
			//IL_049c: Expected O, but got Unknown
			//IL_049d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a7: Expected O, but got Unknown
			//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b2: Expected O, but got Unknown
			//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bd: Expected O, but got Unknown
			//IL_04be: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c8: Expected O, but got Unknown
			//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Expected O, but got Unknown
			//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04de: Expected O, but got Unknown
			//IL_04df: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e9: Expected O, but got Unknown
			//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f4: Expected O, but got Unknown
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ff: Expected O, but got Unknown
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_050a: Expected O, but got Unknown
			//IL_050b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0515: Expected O, but got Unknown
			//IL_0516: Unknown result type (might be due to invalid IL or missing references)
			//IL_0520: Expected O, but got Unknown
			//IL_0521: Unknown result type (might be due to invalid IL or missing references)
			//IL_052b: Expected O, but got Unknown
			//IL_052c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0536: Expected O, but got Unknown
			//IL_0537: Unknown result type (might be due to invalid IL or missing references)
			//IL_0541: Expected O, but got Unknown
			//IL_0542: Unknown result type (might be due to invalid IL or missing references)
			//IL_054c: Expected O, but got Unknown
			//IL_054d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0557: Expected O, but got Unknown
			//IL_0558: Unknown result type (might be due to invalid IL or missing references)
			//IL_0562: Expected O, but got Unknown
			//IL_0563: Unknown result type (might be due to invalid IL or missing references)
			//IL_056d: Expected O, but got Unknown
			//IL_056e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0578: Expected O, but got Unknown
			//IL_0579: Unknown result type (might be due to invalid IL or missing references)
			//IL_0583: Expected O, but got Unknown
			//IL_0584: Unknown result type (might be due to invalid IL or missing references)
			//IL_058e: Expected O, but got Unknown
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0599: Expected O, but got Unknown
			//IL_059a: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a4: Expected O, but got Unknown
			//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05af: Expected O, but got Unknown
			//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ba: Expected O, but got Unknown
			//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c5: Expected O, but got Unknown
			//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d0: Expected O, but got Unknown
			//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05db: Expected O, but got Unknown
			//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e6: Expected O, but got Unknown
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f1: Expected O, but got Unknown
			//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fc: Expected O, but got Unknown
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0607: Expected O, but got Unknown
			//IL_0608: Unknown result type (might be due to invalid IL or missing references)
			//IL_0612: Expected O, but got Unknown
			//IL_0613: Unknown result type (might be due to invalid IL or missing references)
			//IL_061d: Expected O, but got Unknown
			//IL_061e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0628: Expected O, but got Unknown
			//IL_0629: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Expected O, but got Unknown
			//IL_0634: Unknown result type (might be due to invalid IL or missing references)
			//IL_063e: Expected O, but got Unknown
			//IL_063f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0649: Expected O, but got Unknown
			//IL_064a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0654: Expected O, but got Unknown
			//IL_0655: Unknown result type (might be due to invalid IL or missing references)
			//IL_065f: Expected O, but got Unknown
			//IL_0660: Unknown result type (might be due to invalid IL or missing references)
			//IL_066a: Expected O, but got Unknown
			//IL_066b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0675: Expected O, but got Unknown
			//IL_0676: Unknown result type (might be due to invalid IL or missing references)
			//IL_0680: Expected O, but got Unknown
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_068b: Expected O, but got Unknown
			//IL_068c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0696: Expected O, but got Unknown
			//IL_0697: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a1: Expected O, but got Unknown
			//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ac: Expected O, but got Unknown
			//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b7: Expected O, but got Unknown
			//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_082e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0857: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_095a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1046: Unknown result type (might be due to invalid IL or missing references)
			//IL_106f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1149: Unknown result type (might be due to invalid IL or missing references)
			//IL_1172: Unknown result type (might be due to invalid IL or missing references)
			//IL_124c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1275: Unknown result type (might be due to invalid IL or missing references)
			//IL_134f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1378: Unknown result type (might be due to invalid IL or missing references)
			//IL_1452: Unknown result type (might be due to invalid IL or missing references)
			//IL_147b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1556: Unknown result type (might be due to invalid IL or missing references)
			//IL_157f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1643: Unknown result type (might be due to invalid IL or missing references)
			//IL_1698: Unknown result type (might be due to invalid IL or missing references)
			//IL_16e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ee: Expected O, but got Unknown
			//IL_1759: Unknown result type (might be due to invalid IL or missing references)
			//IL_1782: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a53: Expected O, but got Unknown
			//IL_1abe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ae7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ba1: Expected O, but got Unknown
			//IL_1c0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c35: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ce9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cef: Expected O, but got Unknown
			//IL_1d5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d83: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e37: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e3d: Expected O, but got Unknown
			//IL_1ea8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ed1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f85: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f8b: Expected O, but got Unknown
			//IL_1ff6: Unknown result type (might be due to invalid IL or missing references)
			//IL_201f: Unknown result type (might be due to invalid IL or missing references)
			//IL_20d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_20d9: Expected O, but got Unknown
			//IL_2144: Unknown result type (might be due to invalid IL or missing references)
			//IL_216d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2221: Unknown result type (might be due to invalid IL or missing references)
			//IL_2227: Expected O, but got Unknown
			//IL_2292: Unknown result type (might be due to invalid IL or missing references)
			//IL_22bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_236f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2375: Expected O, but got Unknown
			//IL_23e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2409: Unknown result type (might be due to invalid IL or missing references)
			//IL_24bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_24c3: Expected O, but got Unknown
			//IL_252e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2557: Unknown result type (might be due to invalid IL or missing references)
			//IL_260b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2611: Expected O, but got Unknown
			//IL_267c: Unknown result type (might be due to invalid IL or missing references)
			//IL_26a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2759: Unknown result type (might be due to invalid IL or missing references)
			//IL_275f: Expected O, but got Unknown
			//IL_27ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_27f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_28a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_28ad: Expected O, but got Unknown
			//IL_2918: Unknown result type (might be due to invalid IL or missing references)
			//IL_2941: Unknown result type (might be due to invalid IL or missing references)
			//IL_29f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_29fb: Expected O, but got Unknown
			//IL_2a66: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cde: Expected O, but got Unknown
			//IL_2d1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d46: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e34: Expected O, but got Unknown
			//IL_2e73: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f50: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f56: Expected O, but got Unknown
			//IL_2f95: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_3072: Unknown result type (might be due to invalid IL or missing references)
			//IL_3078: Expected O, but got Unknown
			//IL_30b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_30e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3194: Unknown result type (might be due to invalid IL or missing references)
			//IL_319a: Expected O, but got Unknown
			//IL_31d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3202: Unknown result type (might be due to invalid IL or missing references)
			//IL_32b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_32bc: Expected O, but got Unknown
			//IL_32fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3324: Unknown result type (might be due to invalid IL or missing references)
			//IL_33d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_33de: Expected O, but got Unknown
			//IL_341d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3446: Unknown result type (might be due to invalid IL or missing references)
			//IL_34fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_3500: Expected O, but got Unknown
			//IL_353f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3568: Unknown result type (might be due to invalid IL or missing references)
			//IL_361c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3622: Expected O, but got Unknown
			//IL_3661: Unknown result type (might be due to invalid IL or missing references)
			//IL_368a: Unknown result type (might be due to invalid IL or missing references)
			//IL_373e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3744: Expected O, but got Unknown
			//IL_3783: Unknown result type (might be due to invalid IL or missing references)
			//IL_37ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_3860: Unknown result type (might be due to invalid IL or missing references)
			//IL_3866: Expected O, but got Unknown
			//IL_38a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_38ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_3982: Unknown result type (might be due to invalid IL or missing references)
			//IL_3988: Expected O, but got Unknown
			//IL_39c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_39f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3aa4: Unknown result type (might be due to invalid IL or missing references)
			//IL_3aaa: Expected O, but got Unknown
			//IL_3ae9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b12: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bcc: Expected O, but got Unknown
			//IL_3c0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c34: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e19: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e1f: Expected O, but got Unknown
			//IL_3e5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e87: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f75: Expected O, but got Unknown
			//IL_3fb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_4091: Unknown result type (might be due to invalid IL or missing references)
			//IL_4097: Expected O, but got Unknown
			//IL_40d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_40ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_41b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_41b9: Expected O, but got Unknown
			//IL_41f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_4221: Unknown result type (might be due to invalid IL or missing references)
			//IL_42d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_42db: Expected O, but got Unknown
			//IL_431a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4343: Unknown result type (might be due to invalid IL or missing references)
			//IL_43f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_43fd: Expected O, but got Unknown
			//IL_443c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4465: Unknown result type (might be due to invalid IL or missing references)
			//IL_4519: Unknown result type (might be due to invalid IL or missing references)
			//IL_451f: Expected O, but got Unknown
			//IL_455e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4587: Unknown result type (might be due to invalid IL or missing references)
			//IL_463b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4641: Expected O, but got Unknown
			//IL_4680: Unknown result type (might be due to invalid IL or missing references)
			//IL_46a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_475d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4763: Expected O, but got Unknown
			//IL_47a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_47cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_487f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4885: Expected O, but got Unknown
			//IL_48c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_48ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_49a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_49a7: Expected O, but got Unknown
			//IL_49e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ac3: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ac9: Expected O, but got Unknown
			//IL_4b08: Unknown result type (might be due to invalid IL or missing references)
			//IL_4b31: Unknown result type (might be due to invalid IL or missing references)
			//IL_4be5: Unknown result type (might be due to invalid IL or missing references)
			//IL_4beb: Expected O, but got Unknown
			//IL_4c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4c53: Unknown result type (might be due to invalid IL or missing references)
			//IL_4d07: Unknown result type (might be due to invalid IL or missing references)
			//IL_4d0d: Expected O, but got Unknown
			//IL_4d4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4d75: Unknown result type (might be due to invalid IL or missing references)
			//IL_4f5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4f64: Expected O, but got Unknown
			//IL_4fa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_4fcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_5080: Unknown result type (might be due to invalid IL or missing references)
			//IL_5086: Expected O, but got Unknown
			//IL_50c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_50ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_51a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_51a8: Expected O, but got Unknown
			//IL_51e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_5210: Unknown result type (might be due to invalid IL or missing references)
			//IL_52c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_52ca: Expected O, but got Unknown
			//IL_5309: Unknown result type (might be due to invalid IL or missing references)
			//IL_5332: Unknown result type (might be due to invalid IL or missing references)
			//IL_53e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_53ec: Expected O, but got Unknown
			//IL_542b: Unknown result type (might be due to invalid IL or missing references)
			//IL_5454: Unknown result type (might be due to invalid IL or missing references)
			//IL_5508: Unknown result type (might be due to invalid IL or missing references)
			//IL_550e: Expected O, but got Unknown
			//IL_554d: Unknown result type (might be due to invalid IL or missing references)
			//IL_5576: Unknown result type (might be due to invalid IL or missing references)
			//IL_562a: Unknown result type (might be due to invalid IL or missing references)
			//IL_5630: Expected O, but got Unknown
			//IL_566f: Unknown result type (might be due to invalid IL or missing references)
			//IL_5698: Unknown result type (might be due to invalid IL or missing references)
			//IL_574c: Unknown result type (might be due to invalid IL or missing references)
			//IL_5752: Expected O, but got Unknown
			//IL_5791: Unknown result type (might be due to invalid IL or missing references)
			//IL_57ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_586e: Unknown result type (might be due to invalid IL or missing references)
			//IL_5874: Expected O, but got Unknown
			//IL_58b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_58dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_5990: Unknown result type (might be due to invalid IL or missing references)
			//IL_5996: Expected O, but got Unknown
			//IL_59d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_59fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_5ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_5ab8: Expected O, but got Unknown
			//IL_5af7: Unknown result type (might be due to invalid IL or missing references)
			//IL_5b20: Unknown result type (might be due to invalid IL or missing references)
			//IL_5bd4: Unknown result type (might be due to invalid IL or missing references)
			//IL_5bda: Expected O, but got Unknown
			//IL_5c19: Unknown result type (might be due to invalid IL or missing references)
			//IL_5c42: Unknown result type (might be due to invalid IL or missing references)
			//IL_5cf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_5cf7: Expected O, but got Unknown
			//IL_5d36: Unknown result type (might be due to invalid IL or missing references)
			//IL_5d5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_5eae: Unknown result type (might be due to invalid IL or missing references)
			//IL_5ed7: Unknown result type (might be due to invalid IL or missing references)
			//IL_60d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_60d7: Expected O, but got Unknown
			//IL_6116: Unknown result type (might be due to invalid IL or missing references)
			//IL_613f: Unknown result type (might be due to invalid IL or missing references)
			//IL_61f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_61f9: Expected O, but got Unknown
			//IL_6238: Unknown result type (might be due to invalid IL or missing references)
			//IL_6261: Unknown result type (might be due to invalid IL or missing references)
			//IL_6315: Unknown result type (might be due to invalid IL or missing references)
			//IL_631b: Expected O, but got Unknown
			//IL_635a: Unknown result type (might be due to invalid IL or missing references)
			//IL_6383: Unknown result type (might be due to invalid IL or missing references)
			//IL_6437: Unknown result type (might be due to invalid IL or missing references)
			//IL_643d: Expected O, but got Unknown
			//IL_647c: Unknown result type (might be due to invalid IL or missing references)
			//IL_64a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_6559: Unknown result type (might be due to invalid IL or missing references)
			//IL_655f: Expected O, but got Unknown
			//IL_659e: Unknown result type (might be due to invalid IL or missing references)
			//IL_65c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_667b: Unknown result type (might be due to invalid IL or missing references)
			//IL_6681: Expected O, but got Unknown
			//IL_66c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_66e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_679d: Unknown result type (might be due to invalid IL or missing references)
			//IL_67a3: Expected O, but got Unknown
			//IL_67e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_680b: Unknown result type (might be due to invalid IL or missing references)
			//IL_68bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_68c5: Expected O, but got Unknown
			//IL_6904: Unknown result type (might be due to invalid IL or missing references)
			//IL_692d: Unknown result type (might be due to invalid IL or missing references)
			//IL_69e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_69e7: Expected O, but got Unknown
			//IL_6a26: Unknown result type (might be due to invalid IL or missing references)
			//IL_6a4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_6b03: Unknown result type (might be due to invalid IL or missing references)
			//IL_6b09: Expected O, but got Unknown
			//IL_6b48: Unknown result type (might be due to invalid IL or missing references)
			//IL_6b71: Unknown result type (might be due to invalid IL or missing references)
			//IL_6c25: Unknown result type (might be due to invalid IL or missing references)
			//IL_6c2b: Expected O, but got Unknown
			//IL_6c6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_6c93: Unknown result type (might be due to invalid IL or missing references)
			//IL_6d47: Unknown result type (might be due to invalid IL or missing references)
			//IL_6d4d: Expected O, but got Unknown
			//IL_6d8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_6db5: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e69: Unknown result type (might be due to invalid IL or missing references)
			//IL_6e6f: Expected O, but got Unknown
			//IL_6eae: Unknown result type (might be due to invalid IL or missing references)
			//IL_6ed7: Unknown result type (might be due to invalid IL or missing references)
			//IL_6f86: Unknown result type (might be due to invalid IL or missing references)
			//IL_6f8c: Expected O, but got Unknown
			//IL_6fcb: Unknown result type (might be due to invalid IL or missing references)
			//IL_6ff4: Unknown result type (might be due to invalid IL or missing references)
			//IL_720f: Unknown result type (might be due to invalid IL or missing references)
			//IL_7215: Expected O, but got Unknown
			//IL_7254: Unknown result type (might be due to invalid IL or missing references)
			//IL_727d: Unknown result type (might be due to invalid IL or missing references)
			//IL_7367: Unknown result type (might be due to invalid IL or missing references)
			//IL_736d: Expected O, but got Unknown
			//IL_73ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_73d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7489: Unknown result type (might be due to invalid IL or missing references)
			//IL_748f: Expected O, but got Unknown
			//IL_74ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_74f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_75ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_75b1: Expected O, but got Unknown
			//IL_75f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_7619: Unknown result type (might be due to invalid IL or missing references)
			//IL_76cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_76d3: Expected O, but got Unknown
			//IL_7712: Unknown result type (might be due to invalid IL or missing references)
			//IL_773b: Unknown result type (might be due to invalid IL or missing references)
			//IL_77ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_77f5: Expected O, but got Unknown
			//IL_7834: Unknown result type (might be due to invalid IL or missing references)
			//IL_785d: Unknown result type (might be due to invalid IL or missing references)
			//IL_7911: Unknown result type (might be due to invalid IL or missing references)
			//IL_7917: Expected O, but got Unknown
			//IL_7956: Unknown result type (might be due to invalid IL or missing references)
			//IL_797f: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a33: Unknown result type (might be due to invalid IL or missing references)
			//IL_7a39: Expected O, but got Unknown
			//IL_7a78: Unknown result type (might be due to invalid IL or missing references)
			//IL_7aa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_7b55: Unknown result type (might be due to invalid IL or missing references)
			//IL_7b5b: Expected O, but got Unknown
			//IL_7b9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_7bc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_7c77: Unknown result type (might be due to invalid IL or missing references)
			//IL_7c7d: Expected O, but got Unknown
			//IL_7cbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ce5: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d99: Unknown result type (might be due to invalid IL or missing references)
			//IL_7d9f: Expected O, but got Unknown
			//IL_7dde: Unknown result type (might be due to invalid IL or missing references)
			//IL_7e07: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ebb: Unknown result type (might be due to invalid IL or missing references)
			//IL_7ec1: Expected O, but got Unknown
			//IL_7f00: Unknown result type (might be due to invalid IL or missing references)
			//IL_7f29: Unknown result type (might be due to invalid IL or missing references)
			//IL_7fdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_7fe3: Expected O, but got Unknown
			//IL_8022: Unknown result type (might be due to invalid IL or missing references)
			//IL_804b: Unknown result type (might be due to invalid IL or missing references)
			//IL_80ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_8105: Expected O, but got Unknown
			//IL_8144: Unknown result type (might be due to invalid IL or missing references)
			//IL_816d: Unknown result type (might be due to invalid IL or missing references)
			//IL_8357: Unknown result type (might be due to invalid IL or missing references)
			//IL_835d: Expected O, but got Unknown
			//IL_839c: Unknown result type (might be due to invalid IL or missing references)
			//IL_83c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_8479: Unknown result type (might be due to invalid IL or missing references)
			//IL_847f: Expected O, but got Unknown
			//IL_84be: Unknown result type (might be due to invalid IL or missing references)
			//IL_84e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_859b: Unknown result type (might be due to invalid IL or missing references)
			//IL_85a1: Expected O, but got Unknown
			//IL_85e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_8609: Unknown result type (might be due to invalid IL or missing references)
			//IL_86bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_86c3: Expected O, but got Unknown
			//IL_8702: Unknown result type (might be due to invalid IL or missing references)
			//IL_872b: Unknown result type (might be due to invalid IL or missing references)
			//IL_87df: Unknown result type (might be due to invalid IL or missing references)
			//IL_87e5: Expected O, but got Unknown
			//IL_8824: Unknown result type (might be due to invalid IL or missing references)
			//IL_884d: Unknown result type (might be due to invalid IL or missing references)
			//IL_8901: Unknown result type (might be due to invalid IL or missing references)
			//IL_8907: Expected O, but got Unknown
			//IL_8946: Unknown result type (might be due to invalid IL or missing references)
			//IL_896f: Unknown result type (might be due to invalid IL or missing references)
			//IL_8a23: Unknown result type (might be due to invalid IL or missing references)
			//IL_8a29: Expected O, but got Unknown
			//IL_8a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_8a91: Unknown result type (might be due to invalid IL or missing references)
			//IL_8b45: Unknown result type (might be due to invalid IL or missing references)
			//IL_8b4b: Expected O, but got Unknown
			//IL_8b8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_8bb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c67: Unknown result type (might be due to invalid IL or missing references)
			//IL_8c6d: Expected O, but got Unknown
			//IL_8cac: Unknown result type (might be due to invalid IL or missing references)
			//IL_8cd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_8d89: Unknown result type (might be due to invalid IL or missing references)
			//IL_8d8f: Expected O, but got Unknown
			//IL_8dce: Unknown result type (might be due to invalid IL or missing references)
			//IL_8df7: Unknown result type (might be due to invalid IL or missing references)
			//IL_8eab: Unknown result type (might be due to invalid IL or missing references)
			//IL_8eb1: Expected O, but got Unknown
			//IL_8ef0: Unknown result type (might be due to invalid IL or missing references)
			//IL_8f19: Unknown result type (might be due to invalid IL or missing references)
			//IL_8fcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_8fd3: Expected O, but got Unknown
			//IL_9012: Unknown result type (might be due to invalid IL or missing references)
			//IL_903b: Unknown result type (might be due to invalid IL or missing references)
			//IL_90ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_90f5: Expected O, but got Unknown
			//IL_9134: Unknown result type (might be due to invalid IL or missing references)
			//IL_915d: Unknown result type (might be due to invalid IL or missing references)
			//IL_920c: Unknown result type (might be due to invalid IL or missing references)
			//IL_9212: Expected O, but got Unknown
			//IL_9251: Unknown result type (might be due to invalid IL or missing references)
			//IL_927a: Unknown result type (might be due to invalid IL or missing references)
			//IL_949a: Unknown result type (might be due to invalid IL or missing references)
			//IL_94a0: Expected O, but got Unknown
			//IL_94df: Unknown result type (might be due to invalid IL or missing references)
			//IL_9508: Unknown result type (might be due to invalid IL or missing references)
			//IL_95bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_95c2: Expected O, but got Unknown
			//IL_9601: Unknown result type (might be due to invalid IL or missing references)
			//IL_962a: Unknown result type (might be due to invalid IL or missing references)
			//IL_96de: Unknown result type (might be due to invalid IL or missing references)
			//IL_96e4: Expected O, but got Unknown
			//IL_9723: Unknown result type (might be due to invalid IL or missing references)
			//IL_974c: Unknown result type (might be due to invalid IL or missing references)
			//IL_9800: Unknown result type (might be due to invalid IL or missing references)
			//IL_9806: Expected O, but got Unknown
			//IL_9845: Unknown result type (might be due to invalid IL or missing references)
			//IL_986e: Unknown result type (might be due to invalid IL or missing references)
			//IL_9922: Unknown result type (might be due to invalid IL or missing references)
			//IL_9928: Expected O, but got Unknown
			//IL_9967: Unknown result type (might be due to invalid IL or missing references)
			//IL_9990: Unknown result type (might be due to invalid IL or missing references)
			//IL_9a44: Unknown result type (might be due to invalid IL or missing references)
			//IL_9a4a: Expected O, but got Unknown
			//IL_9a89: Unknown result type (might be due to invalid IL or missing references)
			//IL_9ab2: Unknown result type (might be due to invalid IL or missing references)
			//IL_9b66: Unknown result type (might be due to invalid IL or missing references)
			//IL_9b6c: Expected O, but got Unknown
			//IL_9bab: Unknown result type (might be due to invalid IL or missing references)
			//IL_9bd4: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c88: Unknown result type (might be due to invalid IL or missing references)
			//IL_9c8e: Expected O, but got Unknown
			//IL_9ccd: Unknown result type (might be due to invalid IL or missing references)
			//IL_9cf6: Unknown result type (might be due to invalid IL or missing references)
			//IL_9daa: Unknown result type (might be due to invalid IL or missing references)
			//IL_9db0: Expected O, but got Unknown
			//IL_9def: Unknown result type (might be due to invalid IL or missing references)
			//IL_9e18: Unknown result type (might be due to invalid IL or missing references)
			//IL_9ecc: Unknown result type (might be due to invalid IL or missing references)
			//IL_9ed2: Expected O, but got Unknown
			//IL_9f11: Unknown result type (might be due to invalid IL or missing references)
			//IL_9f3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_9fee: Unknown result type (might be due to invalid IL or missing references)
			//IL_9ff4: Expected O, but got Unknown
			//IL_a033: Unknown result type (might be due to invalid IL or missing references)
			//IL_a05c: Unknown result type (might be due to invalid IL or missing references)
			//IL_a110: Unknown result type (might be due to invalid IL or missing references)
			//IL_a116: Expected O, but got Unknown
			//IL_a155: Unknown result type (might be due to invalid IL or missing references)
			//IL_a17e: Unknown result type (might be due to invalid IL or missing references)
			//IL_a232: Unknown result type (might be due to invalid IL or missing references)
			//IL_a238: Expected O, but got Unknown
			//IL_a277: Unknown result type (might be due to invalid IL or missing references)
			//IL_a2a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_a34f: Unknown result type (might be due to invalid IL or missing references)
			//IL_a355: Expected O, but got Unknown
			//IL_a394: Unknown result type (might be due to invalid IL or missing references)
			//IL_a3bd: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			XRSummary val3 = new XRSummary();
			XRSummary val4 = new XRSummary();
			XRSummary val5 = new XRSummary();
			XRSummary val6 = new XRSummary();
			XRSummary val7 = new XRSummary();
			XRSummary val8 = new XRSummary();
			Detail = new DetailBand();
			xrLabel14 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel40 = new XRLabel();
			xrLabel41 = new XRLabel();
			xrLabel42 = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel44 = new XRLabel();
			xrLabel45 = new XRLabel();
			xrLabel46 = new XRLabel();
			xrLabel48 = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel50 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel47 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			lblCodigoUnico = new XRLabel();
			UltimoNivelComNegrito = new FormattingRule();
			UltimoNivelSemNegrito = new FormattingRule();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			lblValorTotal = new XRLabel();
			lblValorDezembro = new XRLabel();
			lblValorNovembro = new XRLabel();
			lblValorOutubro = new XRLabel();
			lblValorSetembro = new XRLabel();
			lblValorAgosto = new XRLabel();
			lblValorJulho = new XRLabel();
			lblValorJunho = new XRLabel();
			lblValorMaio = new XRLabel();
			lblValorAbril = new XRLabel();
			lblValorMarco = new XRLabel();
			lblValorFevereiro = new XRLabel();
			lblValorJaneiro = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			bindingSource1 = new BindingSource(components);
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel22 = new XRLabel();
			xrLabel23 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel31 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel34 = new XRLabel();
			xrLabel39 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel54 = new XRLabel();
			xrLabel55 = new XRLabel();
			xrLabel56 = new XRLabel();
			xrLabel57 = new XRLabel();
			DetailReport3 = new DetailReportBand();
			Detail4 = new DetailBand();
			xrLabel38 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel59 = new XRLabel();
			xrLabel60 = new XRLabel();
			xrLabel61 = new XRLabel();
			xrLabel62 = new XRLabel();
			xrLabel63 = new XRLabel();
			xrLabel64 = new XRLabel();
			xrLabel65 = new XRLabel();
			xrLabel66 = new XRLabel();
			xrLabel67 = new XRLabel();
			xrLabel68 = new XRLabel();
			xrLabel69 = new XRLabel();
			xrLabel70 = new XRLabel();
			DetailReport4 = new DetailReportBand();
			Detail5 = new DetailBand();
			xrLabel73 = new XRLabel();
			xrLabel74 = new XRLabel();
			xrLabel75 = new XRLabel();
			xrLabel76 = new XRLabel();
			xrLabel77 = new XRLabel();
			xrLabel78 = new XRLabel();
			xrLabel79 = new XRLabel();
			xrLabel80 = new XRLabel();
			xrLabel81 = new XRLabel();
			xrLabel82 = new XRLabel();
			xrLabel83 = new XRLabel();
			xrLabel84 = new XRLabel();
			xrLabel85 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel71 = new XRLabel();
			DetailReport5 = new DetailReportBand();
			Detail6 = new DetailBand();
			xrLabel72 = new XRLabel();
			xrLabel86 = new XRLabel();
			xrLabel87 = new XRLabel();
			xrLabel88 = new XRLabel();
			xrLabel89 = new XRLabel();
			xrLabel90 = new XRLabel();
			xrLabel91 = new XRLabel();
			xrLabel92 = new XRLabel();
			xrLabel93 = new XRLabel();
			xrLabel94 = new XRLabel();
			xrLabel95 = new XRLabel();
			xrLabel96 = new XRLabel();
			xrLabel97 = new XRLabel();
			xrLabel98 = new XRLabel();
			DetailReport6 = new DetailReportBand();
			Detail7 = new DetailBand();
			xrLabel99 = new XRLabel();
			xrLabel100 = new XRLabel();
			xrLabel101 = new XRLabel();
			xrLabel102 = new XRLabel();
			xrLabel103 = new XRLabel();
			xrLabel104 = new XRLabel();
			xrLabel105 = new XRLabel();
			xrLabel106 = new XRLabel();
			xrLabel107 = new XRLabel();
			xrLabel108 = new XRLabel();
			xrLabel109 = new XRLabel();
			xrLabel110 = new XRLabel();
			xrLabel111 = new XRLabel();
			xrLabel112 = new XRLabel();
			DetailReport7 = new DetailReportBand();
			Detail8 = new DetailBand();
			xrLabel113 = new XRLabel();
			xrLabel114 = new XRLabel();
			xrLabel115 = new XRLabel();
			xrLabel116 = new XRLabel();
			xrLabel117 = new XRLabel();
			xrLabel118 = new XRLabel();
			xrLabel119 = new XRLabel();
			xrLabel120 = new XRLabel();
			xrLabel121 = new XRLabel();
			xrLabel122 = new XRLabel();
			xrLabel123 = new XRLabel();
			xrLabel124 = new XRLabel();
			xrLabel125 = new XRLabel();
			xrLabel126 = new XRLabel();
			DetailReport8 = new DetailReportBand();
			Detail9 = new DetailBand();
			xrLabel127 = new XRLabel();
			xrLabel128 = new XRLabel();
			xrLabel129 = new XRLabel();
			xrLabel130 = new XRLabel();
			xrLabel131 = new XRLabel();
			xrLabel132 = new XRLabel();
			xrLabel133 = new XRLabel();
			xrLabel134 = new XRLabel();
			xrLabel135 = new XRLabel();
			xrLabel136 = new XRLabel();
			xrLabel137 = new XRLabel();
			xrLabel138 = new XRLabel();
			xrLabel139 = new XRLabel();
			xrLabel140 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[13]
			{
				(XRControl)xrLabel14,
				(XRControl)xrLabel37,
				(XRControl)xrLabel40,
				(XRControl)xrLabel41,
				(XRControl)xrLabel42,
				(XRControl)xrLabel43,
				(XRControl)xrLabel44,
				(XRControl)xrLabel45,
				(XRControl)xrLabel46,
				(XRControl)xrLabel48,
				(XRControl)xrLabel49,
				(XRControl)xrLabel50,
				(XRControl)xrLabel51
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail).HeightF = 47.84f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel14).Borders = (BorderSide)0;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel14).ForeColor = Color.Gray;
			((XRControl)xrLabel14).LocationFloat = new PointFloat(384.6528f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseForeColor = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Janeiro";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel37).Borders = (BorderSide)0;
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel37).ForeColor = Color.Gray;
			((XRControl)xrLabel37).LocationFloat = new PointFloat(566.6528f, 0f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel37).StylePriority.UseBorders = false;
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseForeColor = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "Fevereiro";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel40).Borders = (BorderSide)0;
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel40).ForeColor = Color.Gray;
			((XRControl)xrLabel40).LocationFloat = new PointFloat(748.6528f, 0f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseFont = false;
			((XRControl)xrLabel40).StylePriority.UseForeColor = false;
			((XRControl)xrLabel40).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel40).Text = "Março";
			((XRControl)xrLabel40).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel41).Borders = (BorderSide)0;
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel41).ForeColor = Color.Gray;
			((XRControl)xrLabel41).LocationFloat = new PointFloat(930.6528f, 0f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel41).StylePriority.UseBorders = false;
			((XRControl)xrLabel41).StylePriority.UseFont = false;
			((XRControl)xrLabel41).StylePriority.UseForeColor = false;
			((XRControl)xrLabel41).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel41).Text = "Abril";
			((XRControl)xrLabel41).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel42).Borders = (BorderSide)0;
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel42).ForeColor = Color.Gray;
			((XRControl)xrLabel42).LocationFloat = new PointFloat(1112.653f, 0f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseFont = false;
			((XRControl)xrLabel42).StylePriority.UseForeColor = false;
			((XRControl)xrLabel42).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel42).Text = "Maio";
			((XRControl)xrLabel42).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel43).Borders = (BorderSide)0;
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel43).ForeColor = Color.Gray;
			((XRControl)xrLabel43).LocationFloat = new PointFloat(1294.653f, 0f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel43).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel43).StylePriority.UseForeColor = false;
			((XRControl)xrLabel43).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel43).Text = "Junho";
			((XRControl)xrLabel43).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel44).Borders = (BorderSide)0;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel44).ForeColor = Color.Gray;
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1476.653f, 0f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UseForeColor = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).Text = "Julho";
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel45).Borders = (BorderSide)0;
			((XRControl)xrLabel45).Dpi = 254f;
			((XRControl)xrLabel45).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel45).ForeColor = Color.Gray;
			((XRControl)xrLabel45).LocationFloat = new PointFloat(1658.653f, 0f);
			((XRControl)xrLabel45).Name = "xrLabel45";
			((XRControl)xrLabel45).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel45).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel45).StylePriority.UseBorders = false;
			((XRControl)xrLabel45).StylePriority.UseFont = false;
			((XRControl)xrLabel45).StylePriority.UseForeColor = false;
			((XRControl)xrLabel45).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel45).Text = "Agosto";
			((XRControl)xrLabel45).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel46).Borders = (BorderSide)0;
			((XRControl)xrLabel46).Dpi = 254f;
			((XRControl)xrLabel46).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel46).ForeColor = Color.Gray;
			((XRControl)xrLabel46).LocationFloat = new PointFloat(1840.653f, 0f);
			((XRControl)xrLabel46).Name = "xrLabel46";
			((XRControl)xrLabel46).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel46).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel46).StylePriority.UseBorders = false;
			((XRControl)xrLabel46).StylePriority.UseFont = false;
			((XRControl)xrLabel46).StylePriority.UseForeColor = false;
			((XRControl)xrLabel46).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel46).Text = "Setembro";
			((XRControl)xrLabel46).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel48).Borders = (BorderSide)0;
			((XRControl)xrLabel48).Dpi = 254f;
			((XRControl)xrLabel48).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel48).ForeColor = Color.Gray;
			((XRControl)xrLabel48).LocationFloat = new PointFloat(2022.653f, 0f);
			((XRControl)xrLabel48).Name = "xrLabel48";
			((XRControl)xrLabel48).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel48).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel48).StylePriority.UseBorders = false;
			((XRControl)xrLabel48).StylePriority.UseFont = false;
			((XRControl)xrLabel48).StylePriority.UseForeColor = false;
			((XRControl)xrLabel48).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel48).Text = "Outubro";
			((XRControl)xrLabel48).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel49).Borders = (BorderSide)0;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel49).ForeColor = Color.Gray;
			((XRControl)xrLabel49).LocationFloat = new PointFloat(2204.653f, 0f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).StylePriority.UseForeColor = false;
			((XRControl)xrLabel49).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel49).Text = "Novembro";
			((XRControl)xrLabel49).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel50).Borders = (BorderSide)0;
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel50).ForeColor = Color.Gray;
			((XRControl)xrLabel50).LocationFloat = new PointFloat(2386.653f, 0f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel50).StylePriority.UseForeColor = false;
			((XRControl)xrLabel50).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel50).Text = "Dezembro";
			((XRControl)xrLabel50).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel51).Borders = (BorderSide)0;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI Semibold", 8f);
			((XRControl)xrLabel51).ForeColor = Color.Gray;
			((XRControl)xrLabel51).LocationFloat = new PointFloat(2568.653f, 0f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseForeColor = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "TOTAL";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel47).BackColor = Color.LightGray;
			((XRControl)xrLabel47).Borders = (BorderSide)8;
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(2768.653f, 50.4825f);
			((XRControl)xrLabel47).StylePriority.UseBackColor = false;
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel47).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel47).Text = "PLANO DE CONTAS";
			((XRControl)xrLabel47).TextAlignment = (TextAlignment)16;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 58.20833f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)lblCodigoUnico).BackColor = Color.Transparent;
			((XRControl)lblCodigoUnico).Borders = (BorderSide)0;
			((XRControl)lblCodigoUnico).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.CodigoUnicoDescricaoToText")
			});
			((XRControl)lblCodigoUnico).Dpi = 254f;
			((XRControl)lblCodigoUnico).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblCodigoUnico).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblCodigoUnico).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblCodigoUnico).LocationFloat = new PointFloat(35.61445f, 5f);
			((XRControl)lblCodigoUnico).Name = "lblCodigoUnico";
			((XRControl)lblCodigoUnico).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblCodigoUnico).SizeF = new SizeF(349.0384f, 47.84f);
			((XRControl)lblCodigoUnico).StylePriority.UseBackColor = false;
			((XRControl)lblCodigoUnico).StylePriority.UseBorders = false;
			((XRControl)lblCodigoUnico).StylePriority.UseFont = false;
			((XRControl)lblCodigoUnico).StylePriority.UsePadding = false;
			((XRControl)lblCodigoUnico).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:n2}";
			lblCodigoUnico.Summary = val;
			((XRControl)lblCodigoUnico).TextAlignment = (TextAlignment)16;
			((XRControl)lblCodigoUnico).WordWrap = false;
			UltimoNivelComNegrito.Condition = "[IsUltimoNivel] == False";
			((DataContainerComponent)UltimoNivelComNegrito).DataMember = "BalanceteItens";
			((BrickStyle)UltimoNivelComNegrito.Formatting).Font = new Font("Segoe UI Semibold", 7.5f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			UltimoNivelComNegrito.Name = "UltimoNivelComNegrito";
			UltimoNivelSemNegrito.Condition = "[BalanceteItens.IsUltimoNivel] == True";
			((DataContainerComponent)UltimoNivelSemNegrito).DataMember = "BalanceteItens";
			((BrickStyle)UltimoNivelSemNegrito.Formatting).Font = new Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			UltimoNivelSemNegrito.Name = "UltimoNivelSemNegrito";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader2
			});
			((XtraReportBase)DetailReport).DataMember = "BalanceteItens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 1;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)lblValorTotal,
				(XRControl)lblValorDezembro,
				(XRControl)lblValorNovembro,
				(XRControl)lblValorOutubro,
				(XRControl)lblValorSetembro,
				(XRControl)lblValorAgosto,
				(XRControl)lblValorJulho,
				(XRControl)lblValorJunho,
				(XRControl)lblValorMaio,
				(XRControl)lblValorAbril,
				(XRControl)lblValorMarco,
				(XRControl)lblValorFevereiro,
				(XRControl)lblValorJaneiro,
				(XRControl)lblCodigoUnico
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 58.8643f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)lblValorTotal).BackColor = Color.Transparent;
			((XRControl)lblValorTotal).Borders = (BorderSide)0;
			((XRControl)lblValorTotal).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.Total", "{0:n2}")
			});
			((XRControl)lblValorTotal).Dpi = 254f;
			((XRControl)lblValorTotal).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorTotal).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorTotal).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorTotal).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)lblValorTotal).Name = "lblValorTotal";
			((XRControl)lblValorTotal).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorTotal).SizeF = new SizeF(200f, 47.84f);
			((XRControl)lblValorTotal).StylePriority.UseBackColor = false;
			((XRControl)lblValorTotal).StylePriority.UseBorders = false;
			((XRControl)lblValorTotal).StylePriority.UseFont = false;
			((XRControl)lblValorTotal).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorTotal).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorDezembro).BackColor = Color.Transparent;
			((XRControl)lblValorDezembro).Borders = (BorderSide)0;
			((XRControl)lblValorDezembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorDezembro", "{0:n2}")
			});
			((XRControl)lblValorDezembro).Dpi = 254f;
			((XRControl)lblValorDezembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorDezembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorDezembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorDezembro).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)lblValorDezembro).Name = "lblValorDezembro";
			((XRControl)lblValorDezembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorDezembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorDezembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorDezembro).StylePriority.UseBorders = false;
			((XRControl)lblValorDezembro).StylePriority.UseFont = false;
			((XRControl)lblValorDezembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorDezembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorNovembro).BackColor = Color.Transparent;
			((XRControl)lblValorNovembro).Borders = (BorderSide)0;
			((XRControl)lblValorNovembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorNovembro", "{0:n2}")
			});
			((XRControl)lblValorNovembro).Dpi = 254f;
			((XRControl)lblValorNovembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorNovembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorNovembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorNovembro).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)lblValorNovembro).Name = "lblValorNovembro";
			((XRControl)lblValorNovembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorNovembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorNovembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorNovembro).StylePriority.UseBorders = false;
			((XRControl)lblValorNovembro).StylePriority.UseFont = false;
			((XRControl)lblValorNovembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorNovembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorOutubro).BackColor = Color.Transparent;
			((XRControl)lblValorOutubro).Borders = (BorderSide)0;
			((XRControl)lblValorOutubro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorOutubro", "{0:n2}")
			});
			((XRControl)lblValorOutubro).Dpi = 254f;
			((XRControl)lblValorOutubro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorOutubro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorOutubro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorOutubro).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)lblValorOutubro).Name = "lblValorOutubro";
			((XRControl)lblValorOutubro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorOutubro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorOutubro).StylePriority.UseBackColor = false;
			((XRControl)lblValorOutubro).StylePriority.UseBorders = false;
			((XRControl)lblValorOutubro).StylePriority.UseFont = false;
			((XRControl)lblValorOutubro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorOutubro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorSetembro).BackColor = Color.Transparent;
			((XRControl)lblValorSetembro).Borders = (BorderSide)0;
			((XRControl)lblValorSetembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorSetembro", "{0:n2}")
			});
			((XRControl)lblValorSetembro).Dpi = 254f;
			((XRControl)lblValorSetembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorSetembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorSetembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorSetembro).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)lblValorSetembro).Name = "lblValorSetembro";
			((XRControl)lblValorSetembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorSetembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorSetembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorSetembro).StylePriority.UseBorders = false;
			((XRControl)lblValorSetembro).StylePriority.UseFont = false;
			((XRControl)lblValorSetembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorSetembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorAgosto).BackColor = Color.Transparent;
			((XRControl)lblValorAgosto).Borders = (BorderSide)0;
			((XRControl)lblValorAgosto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorAgosto", "{0:n2}")
			});
			((XRControl)lblValorAgosto).Dpi = 254f;
			((XRControl)lblValorAgosto).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAgosto).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAgosto).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorAgosto).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)lblValorAgosto).Name = "lblValorAgosto";
			((XRControl)lblValorAgosto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorAgosto).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorAgosto).StylePriority.UseBackColor = false;
			((XRControl)lblValorAgosto).StylePriority.UseBorders = false;
			((XRControl)lblValorAgosto).StylePriority.UseFont = false;
			((XRControl)lblValorAgosto).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorAgosto).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJulho).BackColor = Color.Transparent;
			((XRControl)lblValorJulho).Borders = (BorderSide)0;
			((XRControl)lblValorJulho).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJulho", "{0:n2}")
			});
			((XRControl)lblValorJulho).Dpi = 254f;
			((XRControl)lblValorJulho).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJulho).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJulho).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJulho).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)lblValorJulho).Name = "lblValorJulho";
			((XRControl)lblValorJulho).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJulho).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJulho).StylePriority.UseBackColor = false;
			((XRControl)lblValorJulho).StylePriority.UseBorders = false;
			((XRControl)lblValorJulho).StylePriority.UseFont = false;
			((XRControl)lblValorJulho).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJulho).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJunho).BackColor = Color.Transparent;
			((XRControl)lblValorJunho).Borders = (BorderSide)0;
			((XRControl)lblValorJunho).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJunho", "{0:n2}")
			});
			((XRControl)lblValorJunho).Dpi = 254f;
			((XRControl)lblValorJunho).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJunho).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJunho).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJunho).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)lblValorJunho).Name = "lblValorJunho";
			((XRControl)lblValorJunho).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJunho).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJunho).StylePriority.UseBackColor = false;
			((XRControl)lblValorJunho).StylePriority.UseBorders = false;
			((XRControl)lblValorJunho).StylePriority.UseFont = false;
			((XRControl)lblValorJunho).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJunho).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorMaio).BackColor = Color.Transparent;
			((XRControl)lblValorMaio).Borders = (BorderSide)0;
			((XRControl)lblValorMaio).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorMaio", "{0:n2}")
			});
			((XRControl)lblValorMaio).Dpi = 254f;
			((XRControl)lblValorMaio).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMaio).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMaio).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorMaio).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)lblValorMaio).Name = "lblValorMaio";
			((XRControl)lblValorMaio).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorMaio).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorMaio).StylePriority.UseBackColor = false;
			((XRControl)lblValorMaio).StylePriority.UseBorders = false;
			((XRControl)lblValorMaio).StylePriority.UseFont = false;
			((XRControl)lblValorMaio).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorMaio).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorAbril).BackColor = Color.Transparent;
			((XRControl)lblValorAbril).Borders = (BorderSide)0;
			((XRControl)lblValorAbril).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorAbril", "{0:n2}")
			});
			((XRControl)lblValorAbril).Dpi = 254f;
			((XRControl)lblValorAbril).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAbril).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAbril).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorAbril).LocationFloat = new PointFloat(930.6528f, 5f);
			((XRControl)lblValorAbril).Name = "lblValorAbril";
			((XRControl)lblValorAbril).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorAbril).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorAbril).StylePriority.UseBackColor = false;
			((XRControl)lblValorAbril).StylePriority.UseBorders = false;
			((XRControl)lblValorAbril).StylePriority.UseFont = false;
			((XRControl)lblValorAbril).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorAbril).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorMarco).BackColor = Color.Transparent;
			((XRControl)lblValorMarco).Borders = (BorderSide)0;
			((XRControl)lblValorMarco).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorMarco", "{0:n2}")
			});
			((XRControl)lblValorMarco).Dpi = 254f;
			((XRControl)lblValorMarco).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMarco).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMarco).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorMarco).LocationFloat = new PointFloat(748.6528f, 5f);
			((XRControl)lblValorMarco).Name = "lblValorMarco";
			((XRControl)lblValorMarco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorMarco).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorMarco).StylePriority.UseBackColor = false;
			((XRControl)lblValorMarco).StylePriority.UseBorders = false;
			((XRControl)lblValorMarco).StylePriority.UseFont = false;
			((XRControl)lblValorMarco).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorMarco).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorFevereiro).BackColor = Color.Transparent;
			((XRControl)lblValorFevereiro).Borders = (BorderSide)0;
			((XRControl)lblValorFevereiro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorFevereiro", "{0:n2}")
			});
			((XRControl)lblValorFevereiro).Dpi = 254f;
			((XRControl)lblValorFevereiro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorFevereiro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorFevereiro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorFevereiro).LocationFloat = new PointFloat(566.6528f, 5f);
			((XRControl)lblValorFevereiro).Name = "lblValorFevereiro";
			((XRControl)lblValorFevereiro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorFevereiro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorFevereiro).StylePriority.UseBackColor = false;
			((XRControl)lblValorFevereiro).StylePriority.UseBorders = false;
			((XRControl)lblValorFevereiro).StylePriority.UseFont = false;
			((XRControl)lblValorFevereiro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorFevereiro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJaneiro).BackColor = Color.Transparent;
			((XRControl)lblValorJaneiro).Borders = (BorderSide)0;
			((XRControl)lblValorJaneiro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJaneiro", "{0:n2}")
			});
			((XRControl)lblValorJaneiro).Dpi = 254f;
			((XRControl)lblValorJaneiro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJaneiro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJaneiro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJaneiro).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)lblValorJaneiro).Name = "lblValorJaneiro";
			((XRControl)lblValorJaneiro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJaneiro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJaneiro).StylePriority.UseBackColor = false;
			((XRControl)lblValorJaneiro).StylePriority.UseBorders = false;
			((XRControl)lblValorJaneiro).StylePriority.UseFont = false;
			((XRControl)lblValorJaneiro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJaneiro).TextAlignment = (TextAlignment)64;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel47 });
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 50.4825f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			bindingSource1.DataSource = typeof(ObjetoImpressaoAnaliseGeral);
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail2 });
			((XtraReportBase)DetailReport1).DataMember = "CustoLucroMercadorias";
			((XtraReportBase)DetailReport1).DataSource = bindingSource1;
			((XRControl)DetailReport1).Dpi = 254f;
			DetailReport1.Level = 0;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel22,
				(XRControl)xrLabel23,
				(XRControl)xrLabel24,
				(XRControl)xrLabel30,
				(XRControl)xrLabel31,
				(XRControl)xrLabel32,
				(XRControl)xrLabel34,
				(XRControl)xrLabel39,
				(XRControl)xrLabel52,
				(XRControl)xrLabel53,
				(XRControl)xrLabel54,
				(XRControl)xrLabel55,
				(XRControl)xrLabel56,
				(XRControl)xrLabel57
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).HeightF = 59.45461f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)xrLabel22).BackColor = Color.LightGray;
			((XRControl)xrLabel22).Borders = (BorderSide)0;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Tipo")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(0f, 5f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(384.6522f, 47.84f);
			((XRControl)xrLabel22).StylePriority.UseBackColor = false;
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UsePadding = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:n2}";
			xrLabel22.Summary = val2;
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel22).WordWrap = false;
			((XRControl)xrLabel23).BackColor = Color.LightGray;
			((XRControl)xrLabel23).Borders = (BorderSide)0;
			((XRControl)xrLabel23).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel23).StylePriority.UseBackColor = false;
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel24).BackColor = Color.LightGray;
			((XRControl)xrLabel24).Borders = (BorderSide)0;
			((XRControl)xrLabel24).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(566.6528f, 5f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel30).BackColor = Color.LightGray;
			((XRControl)xrLabel30).Borders = (BorderSide)0;
			((XRControl)xrLabel30).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Marco", "{0:n2}")
			});
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(748.6528f, 5f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel31).BackColor = Color.LightGray;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Abril", "{0:n2}")
			});
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(930.6528f, 5f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel31).StylePriority.UseBackColor = false;
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel32).BackColor = Color.LightGray;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Maio", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel32).StylePriority.UseBackColor = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel34).BackColor = Color.LightGray;
			((XRControl)xrLabel34).Borders = (BorderSide)0;
			((XRControl)xrLabel34).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Junho", "{0:n2}")
			});
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel34).StylePriority.UseBackColor = false;
			((XRControl)xrLabel34).StylePriority.UseBorders = false;
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel34).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel39).BackColor = Color.LightGray;
			((XRControl)xrLabel39).Borders = (BorderSide)0;
			((XRControl)xrLabel39).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Julho", "{0:n2}")
			});
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel39).StylePriority.UseBackColor = false;
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel52).BackColor = Color.LightGray;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel52).StylePriority.UseBackColor = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel53).BackColor = Color.LightGray;
			((XRControl)xrLabel53).Borders = (BorderSide)0;
			((XRControl)xrLabel53).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel54).BackColor = Color.LightGray;
			((XRControl)xrLabel54).Borders = (BorderSide)0;
			((XRControl)xrLabel54).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel54).Dpi = 254f;
			((XRControl)xrLabel54).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel54).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel54).Name = "xrLabel54";
			((XRControl)xrLabel54).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel54).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel54).StylePriority.UseBackColor = false;
			((XRControl)xrLabel54).StylePriority.UseBorders = false;
			((XRControl)xrLabel54).StylePriority.UseFont = false;
			((XRControl)xrLabel54).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel54).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel55).BackColor = Color.LightGray;
			((XRControl)xrLabel55).Borders = (BorderSide)0;
			((XRControl)xrLabel55).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel55).Dpi = 254f;
			((XRControl)xrLabel55).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel55).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel55).Name = "xrLabel55";
			((XRControl)xrLabel55).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel55).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel55).StylePriority.UseBackColor = false;
			((XRControl)xrLabel55).StylePriority.UseBorders = false;
			((XRControl)xrLabel55).StylePriority.UseFont = false;
			((XRControl)xrLabel55).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel55).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel56).BackColor = Color.LightGray;
			((XRControl)xrLabel56).Borders = (BorderSide)0;
			((XRControl)xrLabel56).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel56).Dpi = 254f;
			((XRControl)xrLabel56).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel56).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel56).Name = "xrLabel56";
			((XRControl)xrLabel56).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel56).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel56).StylePriority.UseBackColor = false;
			((XRControl)xrLabel56).StylePriority.UseBorders = false;
			((XRControl)xrLabel56).StylePriority.UseFont = false;
			((XRControl)xrLabel56).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel56).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel57).BackColor = Color.LightGray;
			((XRControl)xrLabel57).Borders = (BorderSide)0;
			((XRControl)xrLabel57).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoLucroMercadorias.TotalInformado", "{0:n2}")
			});
			((XRControl)xrLabel57).Dpi = 254f;
			((XRControl)xrLabel57).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel57).LocationFloat = new PointFloat(2568.653f, 5.000031f);
			((XRControl)xrLabel57).Name = "xrLabel57";
			((XRControl)xrLabel57).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel57).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel57).StylePriority.UseBackColor = false;
			((XRControl)xrLabel57).StylePriority.UseBorders = false;
			((XRControl)xrLabel57).StylePriority.UseFont = false;
			((XRControl)xrLabel57).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel57).TextAlignment = (TextAlignment)64;
			((XtraReportBase)DetailReport3).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail4 });
			((XtraReportBase)DetailReport3).DataMember = "ResultadoOperacional";
			((XtraReportBase)DetailReport3).DataSource = bindingSource1;
			((XRControl)DetailReport3).Dpi = 254f;
			DetailReport3.Level = 2;
			((XRControl)DetailReport3).Name = "DetailReport3";
			((XRControl)Detail4).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel38,
				(XRControl)xrLabel58,
				(XRControl)xrLabel59,
				(XRControl)xrLabel60,
				(XRControl)xrLabel61,
				(XRControl)xrLabel62,
				(XRControl)xrLabel63,
				(XRControl)xrLabel64,
				(XRControl)xrLabel65,
				(XRControl)xrLabel66,
				(XRControl)xrLabel67,
				(XRControl)xrLabel68,
				(XRControl)xrLabel69,
				(XRControl)xrLabel70
			});
			((XRControl)Detail4).Dpi = 254f;
			((XRControl)Detail4).HeightF = 55.56252f;
			((XRControl)Detail4).Name = "Detail4";
			((XRControl)xrLabel38).BackColor = Color.LightGray;
			((XRControl)xrLabel38).Borders = (BorderSide)0;
			((XRControl)xrLabel38).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Tipo")
			});
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel38).LocationFloat = new PointFloat(0.3366699f, 5f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(384.3155f, 47.84f);
			((XRControl)xrLabel38).StylePriority.UseBackColor = false;
			((XRControl)xrLabel38).StylePriority.UseBorders = false;
			((XRControl)xrLabel38).StylePriority.UseFont = false;
			((XRControl)xrLabel38).StylePriority.UsePadding = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			val3.FormatString = "{0:n2}";
			xrLabel38.Summary = val3;
			((XRControl)xrLabel38).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel38).WordWrap = false;
			((XRControl)xrLabel58).BackColor = Color.LightGray;
			((XRControl)xrLabel58).Borders = (BorderSide)0;
			((XRControl)xrLabel58).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel58).Dpi = 254f;
			((XRControl)xrLabel58).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel58).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)xrLabel58).Name = "xrLabel58";
			((XRControl)xrLabel58).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel58).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel58).StylePriority.UseBackColor = false;
			((XRControl)xrLabel58).StylePriority.UseBorders = false;
			((XRControl)xrLabel58).StylePriority.UseFont = false;
			((XRControl)xrLabel58).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel58).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel59).BackColor = Color.LightGray;
			((XRControl)xrLabel59).Borders = (BorderSide)0;
			((XRControl)xrLabel59).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel59).Dpi = 254f;
			((XRControl)xrLabel59).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel59).LocationFloat = new PointFloat(566.6528f, 5f);
			((XRControl)xrLabel59).Name = "xrLabel59";
			((XRControl)xrLabel59).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel59).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel59).StylePriority.UseBackColor = false;
			((XRControl)xrLabel59).StylePriority.UseBorders = false;
			((XRControl)xrLabel59).StylePriority.UseFont = false;
			((XRControl)xrLabel59).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel59).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel60).BackColor = Color.LightGray;
			((XRControl)xrLabel60).Borders = (BorderSide)0;
			((XRControl)xrLabel60).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Marco", "{0:n2}")
			});
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(748.6528f, 5f);
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel60).StylePriority.UseBackColor = false;
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrLabel60).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel60).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel61).BackColor = Color.LightGray;
			((XRControl)xrLabel61).Borders = (BorderSide)0;
			((XRControl)xrLabel61).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Abril", "{0:n2}")
			});
			((XRControl)xrLabel61).Dpi = 254f;
			((XRControl)xrLabel61).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel61).LocationFloat = new PointFloat(930.6528f, 5f);
			((XRControl)xrLabel61).Name = "xrLabel61";
			((XRControl)xrLabel61).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel61).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel61).StylePriority.UseBackColor = false;
			((XRControl)xrLabel61).StylePriority.UseBorders = false;
			((XRControl)xrLabel61).StylePriority.UseFont = false;
			((XRControl)xrLabel61).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel61).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel62).BackColor = Color.LightGray;
			((XRControl)xrLabel62).Borders = (BorderSide)0;
			((XRControl)xrLabel62).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Maio", "{0:n2}")
			});
			((XRControl)xrLabel62).Dpi = 254f;
			((XRControl)xrLabel62).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel62).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel62).Name = "xrLabel62";
			((XRControl)xrLabel62).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel62).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel62).StylePriority.UseBackColor = false;
			((XRControl)xrLabel62).StylePriority.UseBorders = false;
			((XRControl)xrLabel62).StylePriority.UseFont = false;
			((XRControl)xrLabel62).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel62).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel63).BackColor = Color.LightGray;
			((XRControl)xrLabel63).Borders = (BorderSide)0;
			((XRControl)xrLabel63).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Junho", "{0:n2}")
			});
			((XRControl)xrLabel63).Dpi = 254f;
			((XRControl)xrLabel63).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel63).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel63).Name = "xrLabel63";
			((XRControl)xrLabel63).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel63).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel63).StylePriority.UseBackColor = false;
			((XRControl)xrLabel63).StylePriority.UseBorders = false;
			((XRControl)xrLabel63).StylePriority.UseFont = false;
			((XRControl)xrLabel63).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel63).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel64).BackColor = Color.LightGray;
			((XRControl)xrLabel64).Borders = (BorderSide)0;
			((XRControl)xrLabel64).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Julho", "{0:n2}")
			});
			((XRControl)xrLabel64).Dpi = 254f;
			((XRControl)xrLabel64).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel64).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel64).Name = "xrLabel64";
			((XRControl)xrLabel64).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel64).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel64).StylePriority.UseBackColor = false;
			((XRControl)xrLabel64).StylePriority.UseBorders = false;
			((XRControl)xrLabel64).StylePriority.UseFont = false;
			((XRControl)xrLabel64).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel64).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel65).BackColor = Color.LightGray;
			((XRControl)xrLabel65).Borders = (BorderSide)0;
			((XRControl)xrLabel65).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel65).Dpi = 254f;
			((XRControl)xrLabel65).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel65).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel65).Name = "xrLabel65";
			((XRControl)xrLabel65).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel65).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel65).StylePriority.UseBackColor = false;
			((XRControl)xrLabel65).StylePriority.UseBorders = false;
			((XRControl)xrLabel65).StylePriority.UseFont = false;
			((XRControl)xrLabel65).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel65).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel66).BackColor = Color.LightGray;
			((XRControl)xrLabel66).Borders = (BorderSide)0;
			((XRControl)xrLabel66).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel66).Dpi = 254f;
			((XRControl)xrLabel66).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel66).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel66).Name = "xrLabel66";
			((XRControl)xrLabel66).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel66).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel66).StylePriority.UseBackColor = false;
			((XRControl)xrLabel66).StylePriority.UseBorders = false;
			((XRControl)xrLabel66).StylePriority.UseFont = false;
			((XRControl)xrLabel66).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel66).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel67).BackColor = Color.LightGray;
			((XRControl)xrLabel67).Borders = (BorderSide)0;
			((XRControl)xrLabel67).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel67).Dpi = 254f;
			((XRControl)xrLabel67).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel67).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel67).Name = "xrLabel67";
			((XRControl)xrLabel67).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel67).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel67).StylePriority.UseBackColor = false;
			((XRControl)xrLabel67).StylePriority.UseBorders = false;
			((XRControl)xrLabel67).StylePriority.UseFont = false;
			((XRControl)xrLabel67).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel67).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel68).BackColor = Color.LightGray;
			((XRControl)xrLabel68).Borders = (BorderSide)0;
			((XRControl)xrLabel68).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel68).Dpi = 254f;
			((XRControl)xrLabel68).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel68).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel68).Name = "xrLabel68";
			((XRControl)xrLabel68).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel68).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel68).StylePriority.UseBackColor = false;
			((XRControl)xrLabel68).StylePriority.UseBorders = false;
			((XRControl)xrLabel68).StylePriority.UseFont = false;
			((XRControl)xrLabel68).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel68).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel69).BackColor = Color.LightGray;
			((XRControl)xrLabel69).Borders = (BorderSide)0;
			((XRControl)xrLabel69).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel69).Dpi = 254f;
			((XRControl)xrLabel69).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel69).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel69).Name = "xrLabel69";
			((XRControl)xrLabel69).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel69).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel69).StylePriority.UseBackColor = false;
			((XRControl)xrLabel69).StylePriority.UseBorders = false;
			((XRControl)xrLabel69).StylePriority.UseFont = false;
			((XRControl)xrLabel69).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel69).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel70).BackColor = Color.LightGray;
			((XRControl)xrLabel70).Borders = (BorderSide)0;
			((XRControl)xrLabel70).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResultadoOperacional.Total", "{0:n2}")
			});
			((XRControl)xrLabel70).Dpi = 254f;
			((XRControl)xrLabel70).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel70).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel70).Name = "xrLabel70";
			((XRControl)xrLabel70).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel70).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel70).StylePriority.UseBackColor = false;
			((XRControl)xrLabel70).StylePriority.UseBorders = false;
			((XRControl)xrLabel70).StylePriority.UseFont = false;
			((XRControl)xrLabel70).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel70).TextAlignment = (TextAlignment)64;
			((XtraReportBase)DetailReport4).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail5,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport4).DataMember = "SaldosContas";
			((XtraReportBase)DetailReport4).DataSource = bindingSource1;
			((XRControl)DetailReport4).Dpi = 254f;
			DetailReport4.Level = 3;
			((XRControl)DetailReport4).Name = "DetailReport4";
			((XRControl)Detail5).Controls.AddRange((XRControl[])(object)new XRControl[13]
			{
				(XRControl)xrLabel73,
				(XRControl)xrLabel74,
				(XRControl)xrLabel75,
				(XRControl)xrLabel76,
				(XRControl)xrLabel77,
				(XRControl)xrLabel78,
				(XRControl)xrLabel79,
				(XRControl)xrLabel80,
				(XRControl)xrLabel81,
				(XRControl)xrLabel82,
				(XRControl)xrLabel83,
				(XRControl)xrLabel84,
				(XRControl)xrLabel85
			});
			((XRControl)Detail5).Dpi = 254f;
			((XRControl)Detail5).HeightF = 57.24968f;
			((XRControl)Detail5).Name = "Detail5";
			((XRControl)xrLabel73).BackColor = Color.Transparent;
			((XRControl)xrLabel73).Borders = (BorderSide)0;
			((XRControl)xrLabel73).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorDezembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel73).Dpi = 254f;
			((XRControl)xrLabel73).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel73).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel73).Name = "xrLabel73";
			((XRControl)xrLabel73).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel73).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel73).StylePriority.UseBackColor = false;
			((XRControl)xrLabel73).StylePriority.UseBorders = false;
			((XRControl)xrLabel73).StylePriority.UseFont = false;
			((XRControl)xrLabel73).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel73).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel74).BackColor = Color.Transparent;
			((XRControl)xrLabel74).Borders = (BorderSide)0;
			((XRControl)xrLabel74).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorNovembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel74).Dpi = 254f;
			((XRControl)xrLabel74).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel74).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel74).Name = "xrLabel74";
			((XRControl)xrLabel74).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel74).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel74).StylePriority.UseBackColor = false;
			((XRControl)xrLabel74).StylePriority.UseBorders = false;
			((XRControl)xrLabel74).StylePriority.UseFont = false;
			((XRControl)xrLabel74).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel74).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel75).BackColor = Color.Transparent;
			((XRControl)xrLabel75).Borders = (BorderSide)0;
			((XRControl)xrLabel75).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorOutubro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel75).Dpi = 254f;
			((XRControl)xrLabel75).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel75).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel75).Name = "xrLabel75";
			((XRControl)xrLabel75).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel75).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel75).StylePriority.UseBackColor = false;
			((XRControl)xrLabel75).StylePriority.UseBorders = false;
			((XRControl)xrLabel75).StylePriority.UseFont = false;
			((XRControl)xrLabel75).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel75).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel76).BackColor = Color.Transparent;
			((XRControl)xrLabel76).Borders = (BorderSide)0;
			((XRControl)xrLabel76).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorSetembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel76).Dpi = 254f;
			((XRControl)xrLabel76).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel76).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel76).Name = "xrLabel76";
			((XRControl)xrLabel76).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel76).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel76).StylePriority.UseBackColor = false;
			((XRControl)xrLabel76).StylePriority.UseBorders = false;
			((XRControl)xrLabel76).StylePriority.UseFont = false;
			((XRControl)xrLabel76).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel76).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel77).BackColor = Color.Transparent;
			((XRControl)xrLabel77).Borders = (BorderSide)0;
			((XRControl)xrLabel77).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorAgosto.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel77).Dpi = 254f;
			((XRControl)xrLabel77).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel77).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel77).Name = "xrLabel77";
			((XRControl)xrLabel77).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel77).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel77).StylePriority.UseBackColor = false;
			((XRControl)xrLabel77).StylePriority.UseBorders = false;
			((XRControl)xrLabel77).StylePriority.UseFont = false;
			((XRControl)xrLabel77).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel77).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel78).BackColor = Color.Transparent;
			((XRControl)xrLabel78).Borders = (BorderSide)0;
			((XRControl)xrLabel78).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJulho.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel78).Dpi = 254f;
			((XRControl)xrLabel78).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel78).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel78).Name = "xrLabel78";
			((XRControl)xrLabel78).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel78).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel78).StylePriority.UseBackColor = false;
			((XRControl)xrLabel78).StylePriority.UseBorders = false;
			((XRControl)xrLabel78).StylePriority.UseFont = false;
			((XRControl)xrLabel78).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel78).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel79).BackColor = Color.Transparent;
			((XRControl)xrLabel79).Borders = (BorderSide)0;
			((XRControl)xrLabel79).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJunho.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel79).Dpi = 254f;
			((XRControl)xrLabel79).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel79).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel79).Name = "xrLabel79";
			((XRControl)xrLabel79).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel79).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel79).StylePriority.UseBackColor = false;
			((XRControl)xrLabel79).StylePriority.UseBorders = false;
			((XRControl)xrLabel79).StylePriority.UseFont = false;
			((XRControl)xrLabel79).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel79).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel80).BackColor = Color.Transparent;
			((XRControl)xrLabel80).Borders = (BorderSide)0;
			((XRControl)xrLabel80).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorMaio.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel80).Dpi = 254f;
			((XRControl)xrLabel80).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel80).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel80).Name = "xrLabel80";
			((XRControl)xrLabel80).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel80).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel80).StylePriority.UseBackColor = false;
			((XRControl)xrLabel80).StylePriority.UseBorders = false;
			((XRControl)xrLabel80).StylePriority.UseFont = false;
			((XRControl)xrLabel80).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel80).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel81).BackColor = Color.Transparent;
			((XRControl)xrLabel81).Borders = (BorderSide)0;
			((XRControl)xrLabel81).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorAbril.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel81).Dpi = 254f;
			((XRControl)xrLabel81).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel81).LocationFloat = new PointFloat(930.6527f, 5f);
			((XRControl)xrLabel81).Name = "xrLabel81";
			((XRControl)xrLabel81).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel81).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel81).StylePriority.UseBackColor = false;
			((XRControl)xrLabel81).StylePriority.UseBorders = false;
			((XRControl)xrLabel81).StylePriority.UseFont = false;
			((XRControl)xrLabel81).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel81).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel82).BackColor = Color.Transparent;
			((XRControl)xrLabel82).Borders = (BorderSide)0;
			((XRControl)xrLabel82).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorMarco.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel82).Dpi = 254f;
			((XRControl)xrLabel82).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel82).LocationFloat = new PointFloat(748.6527f, 5f);
			((XRControl)xrLabel82).Name = "xrLabel82";
			((XRControl)xrLabel82).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel82).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel82).StylePriority.UseBackColor = false;
			((XRControl)xrLabel82).StylePriority.UseBorders = false;
			((XRControl)xrLabel82).StylePriority.UseFont = false;
			((XRControl)xrLabel82).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel82).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel83).BackColor = Color.Transparent;
			((XRControl)xrLabel83).Borders = (BorderSide)0;
			((XRControl)xrLabel83).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorFevereiro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel83).Dpi = 254f;
			((XRControl)xrLabel83).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel83).LocationFloat = new PointFloat(566.6527f, 5f);
			((XRControl)xrLabel83).Name = "xrLabel83";
			((XRControl)xrLabel83).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel83).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel83).StylePriority.UseBackColor = false;
			((XRControl)xrLabel83).StylePriority.UseBorders = false;
			((XRControl)xrLabel83).StylePriority.UseFont = false;
			((XRControl)xrLabel83).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel83).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel84).BackColor = Color.Transparent;
			((XRControl)xrLabel84).Borders = (BorderSide)0;
			((XRControl)xrLabel84).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJaneiro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel84).Dpi = 254f;
			((XRControl)xrLabel84).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel84).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)xrLabel84).Name = "xrLabel84";
			((XRControl)xrLabel84).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel84).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel84).StylePriority.UseBackColor = false;
			((XRControl)xrLabel84).StylePriority.UseBorders = false;
			((XRControl)xrLabel84).StylePriority.UseFont = false;
			((XRControl)xrLabel84).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel84).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel85).BackColor = Color.Transparent;
			((XRControl)xrLabel85).Borders = (BorderSide)0;
			((XRControl)xrLabel85).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.Descricao")
			});
			((XRControl)xrLabel85).Dpi = 254f;
			((XRControl)xrLabel85).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel85).LocationFloat = new PointFloat(35.61445f, 4.999964f);
			((XRControl)xrLabel85).Name = "xrLabel85";
			((XRControl)xrLabel85).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel85).SizeF = new SizeF(349.0377f, 47.84f);
			((XRControl)xrLabel85).StylePriority.UseBackColor = false;
			((XRControl)xrLabel85).StylePriority.UseBorders = false;
			((XRControl)xrLabel85).StylePriority.UseFont = false;
			((XRControl)xrLabel85).StylePriority.UsePadding = false;
			((XRControl)xrLabel85).StylePriority.UseTextAlignment = false;
			val4.FormatString = "{0:n2}";
			xrLabel85.Summary = val4;
			((XRControl)xrLabel85).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel85).WordWrap = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel71 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 55.59025f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel71).BackColor = Color.LightGray;
			((XRControl)xrLabel71).Borders = (BorderSide)8;
			((XRControl)xrLabel71).Dpi = 254f;
			((XRControl)xrLabel71).Font = new Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel71).LocationFloat = new PointFloat(0f, 5f);
			((XRControl)xrLabel71).Name = "xrLabel71";
			((XRControl)xrLabel71).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel71).SizeF = new SizeF(2768.653f, 47.84f);
			((XRControl)xrLabel71).StylePriority.UseBackColor = false;
			((XRControl)xrLabel71).StylePriority.UseBorders = false;
			((XRControl)xrLabel71).StylePriority.UseFont = false;
			((XRControl)xrLabel71).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel71).Text = "SALDO DAS CONTAS";
			((XRControl)xrLabel71).TextAlignment = (TextAlignment)16;
			((XtraReportBase)DetailReport5).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail6 });
			((XtraReportBase)DetailReport5).DataMember = "Recebimentos";
			((XtraReportBase)DetailReport5).DataSource = bindingSource1;
			((XRControl)DetailReport5).Dpi = 254f;
			DetailReport5.Level = 4;
			((XRControl)DetailReport5).Name = "DetailReport5";
			((XRControl)Detail6).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel72,
				(XRControl)xrLabel86,
				(XRControl)xrLabel87,
				(XRControl)xrLabel88,
				(XRControl)xrLabel89,
				(XRControl)xrLabel90,
				(XRControl)xrLabel91,
				(XRControl)xrLabel92,
				(XRControl)xrLabel93,
				(XRControl)xrLabel94,
				(XRControl)xrLabel95,
				(XRControl)xrLabel96,
				(XRControl)xrLabel97,
				(XRControl)xrLabel98
			});
			((XRControl)Detail6).Dpi = 254f;
			((XRControl)Detail6).HeightF = 57.76739f;
			((XRControl)Detail6).Name = "Detail6";
			((XRControl)xrLabel72).BackColor = Color.LightGray;
			((XRControl)xrLabel72).Borders = (BorderSide)0;
			((XRControl)xrLabel72).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Total", "{0:n2}")
			});
			((XRControl)xrLabel72).Dpi = 254f;
			((XRControl)xrLabel72).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel72).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel72).Name = "xrLabel72";
			((XRControl)xrLabel72).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel72).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel72).StylePriority.UseBackColor = false;
			((XRControl)xrLabel72).StylePriority.UseBorders = false;
			((XRControl)xrLabel72).StylePriority.UseFont = false;
			((XRControl)xrLabel72).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel72).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel86).BackColor = Color.LightGray;
			((XRControl)xrLabel86).Borders = (BorderSide)0;
			((XRControl)xrLabel86).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel86).Dpi = 254f;
			((XRControl)xrLabel86).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel86).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel86).Name = "xrLabel86";
			((XRControl)xrLabel86).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel86).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel86).StylePriority.UseBackColor = false;
			((XRControl)xrLabel86).StylePriority.UseBorders = false;
			((XRControl)xrLabel86).StylePriority.UseFont = false;
			((XRControl)xrLabel86).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel86).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel87).BackColor = Color.LightGray;
			((XRControl)xrLabel87).Borders = (BorderSide)0;
			((XRControl)xrLabel87).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel87).Dpi = 254f;
			((XRControl)xrLabel87).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel87).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel87).Name = "xrLabel87";
			((XRControl)xrLabel87).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel87).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel87).StylePriority.UseBackColor = false;
			((XRControl)xrLabel87).StylePriority.UseBorders = false;
			((XRControl)xrLabel87).StylePriority.UseFont = false;
			((XRControl)xrLabel87).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel87).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel88).BackColor = Color.LightGray;
			((XRControl)xrLabel88).Borders = (BorderSide)0;
			((XRControl)xrLabel88).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel88).Dpi = 254f;
			((XRControl)xrLabel88).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel88).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel88).Name = "xrLabel88";
			((XRControl)xrLabel88).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel88).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel88).StylePriority.UseBackColor = false;
			((XRControl)xrLabel88).StylePriority.UseBorders = false;
			((XRControl)xrLabel88).StylePriority.UseFont = false;
			((XRControl)xrLabel88).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel88).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel89).BackColor = Color.LightGray;
			((XRControl)xrLabel89).Borders = (BorderSide)0;
			((XRControl)xrLabel89).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel89).Dpi = 254f;
			((XRControl)xrLabel89).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel89).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel89).Name = "xrLabel89";
			((XRControl)xrLabel89).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel89).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel89).StylePriority.UseBackColor = false;
			((XRControl)xrLabel89).StylePriority.UseBorders = false;
			((XRControl)xrLabel89).StylePriority.UseFont = false;
			((XRControl)xrLabel89).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel89).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel90).BackColor = Color.LightGray;
			((XRControl)xrLabel90).Borders = (BorderSide)0;
			((XRControl)xrLabel90).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel90).Dpi = 254f;
			((XRControl)xrLabel90).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel90).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel90).Name = "xrLabel90";
			((XRControl)xrLabel90).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel90).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel90).StylePriority.UseBackColor = false;
			((XRControl)xrLabel90).StylePriority.UseBorders = false;
			((XRControl)xrLabel90).StylePriority.UseFont = false;
			((XRControl)xrLabel90).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel90).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel91).BackColor = Color.LightGray;
			((XRControl)xrLabel91).Borders = (BorderSide)0;
			((XRControl)xrLabel91).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Julho", "{0:n2}")
			});
			((XRControl)xrLabel91).Dpi = 254f;
			((XRControl)xrLabel91).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel91).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel91).Name = "xrLabel91";
			((XRControl)xrLabel91).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel91).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel91).StylePriority.UseBackColor = false;
			((XRControl)xrLabel91).StylePriority.UseBorders = false;
			((XRControl)xrLabel91).StylePriority.UseFont = false;
			((XRControl)xrLabel91).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel91).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel92).BackColor = Color.LightGray;
			((XRControl)xrLabel92).Borders = (BorderSide)0;
			((XRControl)xrLabel92).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Junho", "{0:n2}")
			});
			((XRControl)xrLabel92).Dpi = 254f;
			((XRControl)xrLabel92).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel92).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel92).Name = "xrLabel92";
			((XRControl)xrLabel92).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel92).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel92).StylePriority.UseBackColor = false;
			((XRControl)xrLabel92).StylePriority.UseBorders = false;
			((XRControl)xrLabel92).StylePriority.UseFont = false;
			((XRControl)xrLabel92).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel92).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel93).BackColor = Color.LightGray;
			((XRControl)xrLabel93).Borders = (BorderSide)0;
			((XRControl)xrLabel93).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Maio", "{0:n2}")
			});
			((XRControl)xrLabel93).Dpi = 254f;
			((XRControl)xrLabel93).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel93).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel93).Name = "xrLabel93";
			((XRControl)xrLabel93).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel93).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel93).StylePriority.UseBackColor = false;
			((XRControl)xrLabel93).StylePriority.UseBorders = false;
			((XRControl)xrLabel93).StylePriority.UseFont = false;
			((XRControl)xrLabel93).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel93).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel94).BackColor = Color.LightGray;
			((XRControl)xrLabel94).Borders = (BorderSide)0;
			((XRControl)xrLabel94).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Abril", "{0:n2}")
			});
			((XRControl)xrLabel94).Dpi = 254f;
			((XRControl)xrLabel94).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel94).LocationFloat = new PointFloat(930.6531f, 5f);
			((XRControl)xrLabel94).Name = "xrLabel94";
			((XRControl)xrLabel94).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel94).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel94).StylePriority.UseBackColor = false;
			((XRControl)xrLabel94).StylePriority.UseBorders = false;
			((XRControl)xrLabel94).StylePriority.UseFont = false;
			((XRControl)xrLabel94).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel94).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel95).BackColor = Color.LightGray;
			((XRControl)xrLabel95).Borders = (BorderSide)0;
			((XRControl)xrLabel95).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Marco", "{0:n2}")
			});
			((XRControl)xrLabel95).Dpi = 254f;
			((XRControl)xrLabel95).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel95).LocationFloat = new PointFloat(748.6531f, 5f);
			((XRControl)xrLabel95).Name = "xrLabel95";
			((XRControl)xrLabel95).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel95).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel95).StylePriority.UseBackColor = false;
			((XRControl)xrLabel95).StylePriority.UseBorders = false;
			((XRControl)xrLabel95).StylePriority.UseFont = false;
			((XRControl)xrLabel95).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel95).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel96).BackColor = Color.LightGray;
			((XRControl)xrLabel96).Borders = (BorderSide)0;
			((XRControl)xrLabel96).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel96).Dpi = 254f;
			((XRControl)xrLabel96).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel96).LocationFloat = new PointFloat(566.6531f, 5f);
			((XRControl)xrLabel96).Name = "xrLabel96";
			((XRControl)xrLabel96).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel96).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel96).StylePriority.UseBackColor = false;
			((XRControl)xrLabel96).StylePriority.UseBorders = false;
			((XRControl)xrLabel96).StylePriority.UseFont = false;
			((XRControl)xrLabel96).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel96).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel97).BackColor = Color.LightGray;
			((XRControl)xrLabel97).Borders = (BorderSide)0;
			((XRControl)xrLabel97).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel97).Dpi = 254f;
			((XRControl)xrLabel97).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel97).LocationFloat = new PointFloat(384.653f, 5f);
			((XRControl)xrLabel97).Name = "xrLabel97";
			((XRControl)xrLabel97).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel97).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel97).StylePriority.UseBackColor = false;
			((XRControl)xrLabel97).StylePriority.UseBorders = false;
			((XRControl)xrLabel97).StylePriority.UseFont = false;
			((XRControl)xrLabel97).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel97).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel98).BackColor = Color.LightGray;
			((XRControl)xrLabel98).Borders = (BorderSide)0;
			((XRControl)xrLabel98).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Recebimentos.Tipo")
			});
			((XRControl)xrLabel98).Dpi = 254f;
			((XRControl)xrLabel98).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel98).LocationFloat = new PointFloat(0.5048828f, 5f);
			((XRControl)xrLabel98).Name = "xrLabel98";
			((XRControl)xrLabel98).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel98).SizeF = new SizeF(384.1473f, 47.84f);
			((XRControl)xrLabel98).StylePriority.UseBackColor = false;
			((XRControl)xrLabel98).StylePriority.UseBorders = false;
			((XRControl)xrLabel98).StylePriority.UseFont = false;
			((XRControl)xrLabel98).StylePriority.UsePadding = false;
			((XRControl)xrLabel98).StylePriority.UseTextAlignment = false;
			val5.FormatString = "{0:n2}";
			xrLabel98.Summary = val5;
			((XRControl)xrLabel98).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel98).WordWrap = false;
			((XtraReportBase)DetailReport6).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail7 });
			((XtraReportBase)DetailReport6).DataMember = "Pagamentos";
			((XtraReportBase)DetailReport6).DataSource = bindingSource1;
			((XRControl)DetailReport6).Dpi = 254f;
			DetailReport6.Level = 5;
			((XRControl)DetailReport6).Name = "DetailReport6";
			((XRControl)Detail7).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel99,
				(XRControl)xrLabel100,
				(XRControl)xrLabel101,
				(XRControl)xrLabel102,
				(XRControl)xrLabel103,
				(XRControl)xrLabel104,
				(XRControl)xrLabel105,
				(XRControl)xrLabel106,
				(XRControl)xrLabel107,
				(XRControl)xrLabel108,
				(XRControl)xrLabel109,
				(XRControl)xrLabel110,
				(XRControl)xrLabel111,
				(XRControl)xrLabel112
			});
			((XRControl)Detail7).Dpi = 254f;
			((XRControl)Detail7).HeightF = 57.76739f;
			((XRControl)Detail7).Name = "Detail7";
			((XRControl)xrLabel99).BackColor = Color.LightGray;
			((XRControl)xrLabel99).Borders = (BorderSide)0;
			((XRControl)xrLabel99).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Tipo")
			});
			((XRControl)xrLabel99).Dpi = 254f;
			((XRControl)xrLabel99).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel99).LocationFloat = new PointFloat(0.5048828f, 5f);
			((XRControl)xrLabel99).Name = "xrLabel99";
			((XRControl)xrLabel99).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel99).SizeF = new SizeF(384.1473f, 47.84f);
			((XRControl)xrLabel99).StylePriority.UseBackColor = false;
			((XRControl)xrLabel99).StylePriority.UseBorders = false;
			((XRControl)xrLabel99).StylePriority.UseFont = false;
			((XRControl)xrLabel99).StylePriority.UsePadding = false;
			((XRControl)xrLabel99).StylePriority.UseTextAlignment = false;
			val6.FormatString = "{0:n2}";
			xrLabel99.Summary = val6;
			((XRControl)xrLabel99).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel99).WordWrap = false;
			((XRControl)xrLabel100).BackColor = Color.LightGray;
			((XRControl)xrLabel100).Borders = (BorderSide)0;
			((XRControl)xrLabel100).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel100).Dpi = 254f;
			((XRControl)xrLabel100).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel100).LocationFloat = new PointFloat(384.6522f, 5f);
			((XRControl)xrLabel100).Name = "xrLabel100";
			((XRControl)xrLabel100).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel100).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel100).StylePriority.UseBackColor = false;
			((XRControl)xrLabel100).StylePriority.UseBorders = false;
			((XRControl)xrLabel100).StylePriority.UseFont = false;
			((XRControl)xrLabel100).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel100).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel101).BackColor = Color.LightGray;
			((XRControl)xrLabel101).Borders = (BorderSide)0;
			((XRControl)xrLabel101).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel101).Dpi = 254f;
			((XRControl)xrLabel101).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel101).LocationFloat = new PointFloat(566.6522f, 5f);
			((XRControl)xrLabel101).Name = "xrLabel101";
			((XRControl)xrLabel101).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel101).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel101).StylePriority.UseBackColor = false;
			((XRControl)xrLabel101).StylePriority.UseBorders = false;
			((XRControl)xrLabel101).StylePriority.UseFont = false;
			((XRControl)xrLabel101).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel101).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel102).BackColor = Color.LightGray;
			((XRControl)xrLabel102).Borders = (BorderSide)0;
			((XRControl)xrLabel102).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Marco", "{0:n2}")
			});
			((XRControl)xrLabel102).Dpi = 254f;
			((XRControl)xrLabel102).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel102).LocationFloat = new PointFloat(748.6522f, 5f);
			((XRControl)xrLabel102).Name = "xrLabel102";
			((XRControl)xrLabel102).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel102).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel102).StylePriority.UseBackColor = false;
			((XRControl)xrLabel102).StylePriority.UseBorders = false;
			((XRControl)xrLabel102).StylePriority.UseFont = false;
			((XRControl)xrLabel102).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel102).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel103).BackColor = Color.LightGray;
			((XRControl)xrLabel103).Borders = (BorderSide)0;
			((XRControl)xrLabel103).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Abril", "{0:n2}")
			});
			((XRControl)xrLabel103).Dpi = 254f;
			((XRControl)xrLabel103).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel103).LocationFloat = new PointFloat(930.6522f, 5f);
			((XRControl)xrLabel103).Name = "xrLabel103";
			((XRControl)xrLabel103).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel103).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel103).StylePriority.UseBackColor = false;
			((XRControl)xrLabel103).StylePriority.UseBorders = false;
			((XRControl)xrLabel103).StylePriority.UseFont = false;
			((XRControl)xrLabel103).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel103).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel104).BackColor = Color.LightGray;
			((XRControl)xrLabel104).Borders = (BorderSide)0;
			((XRControl)xrLabel104).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Maio", "{0:n2}")
			});
			((XRControl)xrLabel104).Dpi = 254f;
			((XRControl)xrLabel104).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel104).LocationFloat = new PointFloat(1112.652f, 5f);
			((XRControl)xrLabel104).Name = "xrLabel104";
			((XRControl)xrLabel104).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel104).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel104).StylePriority.UseBackColor = false;
			((XRControl)xrLabel104).StylePriority.UseBorders = false;
			((XRControl)xrLabel104).StylePriority.UseFont = false;
			((XRControl)xrLabel104).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel104).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel105).BackColor = Color.LightGray;
			((XRControl)xrLabel105).Borders = (BorderSide)0;
			((XRControl)xrLabel105).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Junho", "{0:n2}")
			});
			((XRControl)xrLabel105).Dpi = 254f;
			((XRControl)xrLabel105).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel105).LocationFloat = new PointFloat(1294.652f, 5f);
			((XRControl)xrLabel105).Name = "xrLabel105";
			((XRControl)xrLabel105).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel105).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel105).StylePriority.UseBackColor = false;
			((XRControl)xrLabel105).StylePriority.UseBorders = false;
			((XRControl)xrLabel105).StylePriority.UseFont = false;
			((XRControl)xrLabel105).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel105).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel106).BackColor = Color.LightGray;
			((XRControl)xrLabel106).Borders = (BorderSide)0;
			((XRControl)xrLabel106).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Julho", "{0:n2}")
			});
			((XRControl)xrLabel106).Dpi = 254f;
			((XRControl)xrLabel106).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel106).LocationFloat = new PointFloat(1476.652f, 5f);
			((XRControl)xrLabel106).Name = "xrLabel106";
			((XRControl)xrLabel106).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel106).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel106).StylePriority.UseBackColor = false;
			((XRControl)xrLabel106).StylePriority.UseBorders = false;
			((XRControl)xrLabel106).StylePriority.UseFont = false;
			((XRControl)xrLabel106).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel106).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel107).BackColor = Color.LightGray;
			((XRControl)xrLabel107).Borders = (BorderSide)0;
			((XRControl)xrLabel107).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel107).Dpi = 254f;
			((XRControl)xrLabel107).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel107).LocationFloat = new PointFloat(1658.652f, 5f);
			((XRControl)xrLabel107).Name = "xrLabel107";
			((XRControl)xrLabel107).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel107).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel107).StylePriority.UseBackColor = false;
			((XRControl)xrLabel107).StylePriority.UseBorders = false;
			((XRControl)xrLabel107).StylePriority.UseFont = false;
			((XRControl)xrLabel107).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel107).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel108).BackColor = Color.LightGray;
			((XRControl)xrLabel108).Borders = (BorderSide)0;
			((XRControl)xrLabel108).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel108).Dpi = 254f;
			((XRControl)xrLabel108).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel108).LocationFloat = new PointFloat(1840.652f, 5f);
			((XRControl)xrLabel108).Name = "xrLabel108";
			((XRControl)xrLabel108).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel108).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel108).StylePriority.UseBackColor = false;
			((XRControl)xrLabel108).StylePriority.UseBorders = false;
			((XRControl)xrLabel108).StylePriority.UseFont = false;
			((XRControl)xrLabel108).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel108).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel109).BackColor = Color.LightGray;
			((XRControl)xrLabel109).Borders = (BorderSide)0;
			((XRControl)xrLabel109).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel109).Dpi = 254f;
			((XRControl)xrLabel109).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel109).LocationFloat = new PointFloat(2022.652f, 5f);
			((XRControl)xrLabel109).Name = "xrLabel109";
			((XRControl)xrLabel109).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel109).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel109).StylePriority.UseBackColor = false;
			((XRControl)xrLabel109).StylePriority.UseBorders = false;
			((XRControl)xrLabel109).StylePriority.UseFont = false;
			((XRControl)xrLabel109).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel109).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel110).BackColor = Color.LightGray;
			((XRControl)xrLabel110).Borders = (BorderSide)0;
			((XRControl)xrLabel110).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel110).Dpi = 254f;
			((XRControl)xrLabel110).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel110).LocationFloat = new PointFloat(2204.652f, 5f);
			((XRControl)xrLabel110).Name = "xrLabel110";
			((XRControl)xrLabel110).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel110).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel110).StylePriority.UseBackColor = false;
			((XRControl)xrLabel110).StylePriority.UseBorders = false;
			((XRControl)xrLabel110).StylePriority.UseFont = false;
			((XRControl)xrLabel110).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel110).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel111).BackColor = Color.LightGray;
			((XRControl)xrLabel111).Borders = (BorderSide)0;
			((XRControl)xrLabel111).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel111).Dpi = 254f;
			((XRControl)xrLabel111).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel111).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel111).Name = "xrLabel111";
			((XRControl)xrLabel111).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel111).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel111).StylePriority.UseBackColor = false;
			((XRControl)xrLabel111).StylePriority.UseBorders = false;
			((XRControl)xrLabel111).StylePriority.UseFont = false;
			((XRControl)xrLabel111).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel111).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel112).BackColor = Color.LightGray;
			((XRControl)xrLabel112).Borders = (BorderSide)0;
			((XRControl)xrLabel112).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamentos.Total", "{0:n2}")
			});
			((XRControl)xrLabel112).Dpi = 254f;
			((XRControl)xrLabel112).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel112).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel112).Name = "xrLabel112";
			((XRControl)xrLabel112).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel112).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel112).StylePriority.UseBackColor = false;
			((XRControl)xrLabel112).StylePriority.UseBorders = false;
			((XRControl)xrLabel112).StylePriority.UseFont = false;
			((XRControl)xrLabel112).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel112).TextAlignment = (TextAlignment)64;
			((XtraReportBase)DetailReport7).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail8 });
			((XtraReportBase)DetailReport7).DataMember = "InventarioAnual";
			((XtraReportBase)DetailReport7).DataSource = bindingSource1;
			((XRControl)DetailReport7).Dpi = 254f;
			DetailReport7.Level = 6;
			((XRControl)DetailReport7).Name = "DetailReport7";
			((XRControl)Detail8).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel113,
				(XRControl)xrLabel114,
				(XRControl)xrLabel115,
				(XRControl)xrLabel116,
				(XRControl)xrLabel117,
				(XRControl)xrLabel118,
				(XRControl)xrLabel119,
				(XRControl)xrLabel120,
				(XRControl)xrLabel121,
				(XRControl)xrLabel122,
				(XRControl)xrLabel123,
				(XRControl)xrLabel124,
				(XRControl)xrLabel125,
				(XRControl)xrLabel126
			});
			((XRControl)Detail8).Dpi = 254f;
			((XRControl)Detail8).HeightF = 57.76739f;
			((XRControl)Detail8).Name = "Detail8";
			((XRControl)xrLabel113).BackColor = Color.LightGray;
			((XRControl)xrLabel113).Borders = (BorderSide)0;
			((XRControl)xrLabel113).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Total", "{0:n2}")
			});
			((XRControl)xrLabel113).Dpi = 254f;
			((XRControl)xrLabel113).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel113).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel113).Name = "xrLabel113";
			((XRControl)xrLabel113).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel113).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel113).StylePriority.UseBackColor = false;
			((XRControl)xrLabel113).StylePriority.UseBorders = false;
			((XRControl)xrLabel113).StylePriority.UseFont = false;
			((XRControl)xrLabel113).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel113).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel114).BackColor = Color.LightGray;
			((XRControl)xrLabel114).Borders = (BorderSide)0;
			((XRControl)xrLabel114).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel114).Dpi = 254f;
			((XRControl)xrLabel114).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel114).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel114).Name = "xrLabel114";
			((XRControl)xrLabel114).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel114).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel114).StylePriority.UseBackColor = false;
			((XRControl)xrLabel114).StylePriority.UseBorders = false;
			((XRControl)xrLabel114).StylePriority.UseFont = false;
			((XRControl)xrLabel114).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel114).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel115).BackColor = Color.LightGray;
			((XRControl)xrLabel115).Borders = (BorderSide)0;
			((XRControl)xrLabel115).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel115).Dpi = 254f;
			((XRControl)xrLabel115).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel115).LocationFloat = new PointFloat(2204.652f, 5f);
			((XRControl)xrLabel115).Name = "xrLabel115";
			((XRControl)xrLabel115).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel115).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel115).StylePriority.UseBackColor = false;
			((XRControl)xrLabel115).StylePriority.UseBorders = false;
			((XRControl)xrLabel115).StylePriority.UseFont = false;
			((XRControl)xrLabel115).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel115).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel116).BackColor = Color.LightGray;
			((XRControl)xrLabel116).Borders = (BorderSide)0;
			((XRControl)xrLabel116).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel116).Dpi = 254f;
			((XRControl)xrLabel116).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel116).LocationFloat = new PointFloat(2022.652f, 5f);
			((XRControl)xrLabel116).Name = "xrLabel116";
			((XRControl)xrLabel116).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel116).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel116).StylePriority.UseBackColor = false;
			((XRControl)xrLabel116).StylePriority.UseBorders = false;
			((XRControl)xrLabel116).StylePriority.UseFont = false;
			((XRControl)xrLabel116).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel116).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel117).BackColor = Color.LightGray;
			((XRControl)xrLabel117).Borders = (BorderSide)0;
			((XRControl)xrLabel117).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel117).Dpi = 254f;
			((XRControl)xrLabel117).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel117).LocationFloat = new PointFloat(1840.652f, 5f);
			((XRControl)xrLabel117).Name = "xrLabel117";
			((XRControl)xrLabel117).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel117).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel117).StylePriority.UseBackColor = false;
			((XRControl)xrLabel117).StylePriority.UseBorders = false;
			((XRControl)xrLabel117).StylePriority.UseFont = false;
			((XRControl)xrLabel117).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel117).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel118).BackColor = Color.LightGray;
			((XRControl)xrLabel118).Borders = (BorderSide)0;
			((XRControl)xrLabel118).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel118).Dpi = 254f;
			((XRControl)xrLabel118).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel118).LocationFloat = new PointFloat(1658.652f, 5f);
			((XRControl)xrLabel118).Name = "xrLabel118";
			((XRControl)xrLabel118).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel118).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel118).StylePriority.UseBackColor = false;
			((XRControl)xrLabel118).StylePriority.UseBorders = false;
			((XRControl)xrLabel118).StylePriority.UseFont = false;
			((XRControl)xrLabel118).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel118).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel119).BackColor = Color.LightGray;
			((XRControl)xrLabel119).Borders = (BorderSide)0;
			((XRControl)xrLabel119).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Julho", "{0:n2}")
			});
			((XRControl)xrLabel119).Dpi = 254f;
			((XRControl)xrLabel119).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel119).LocationFloat = new PointFloat(1476.652f, 5f);
			((XRControl)xrLabel119).Name = "xrLabel119";
			((XRControl)xrLabel119).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel119).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel119).StylePriority.UseBackColor = false;
			((XRControl)xrLabel119).StylePriority.UseBorders = false;
			((XRControl)xrLabel119).StylePriority.UseFont = false;
			((XRControl)xrLabel119).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel119).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel120).BackColor = Color.LightGray;
			((XRControl)xrLabel120).Borders = (BorderSide)0;
			((XRControl)xrLabel120).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Junho", "{0:n2}")
			});
			((XRControl)xrLabel120).Dpi = 254f;
			((XRControl)xrLabel120).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel120).LocationFloat = new PointFloat(1294.652f, 5f);
			((XRControl)xrLabel120).Name = "xrLabel120";
			((XRControl)xrLabel120).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel120).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel120).StylePriority.UseBackColor = false;
			((XRControl)xrLabel120).StylePriority.UseBorders = false;
			((XRControl)xrLabel120).StylePriority.UseFont = false;
			((XRControl)xrLabel120).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel120).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel121).BackColor = Color.LightGray;
			((XRControl)xrLabel121).Borders = (BorderSide)0;
			((XRControl)xrLabel121).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Maio", "{0:n2}")
			});
			((XRControl)xrLabel121).Dpi = 254f;
			((XRControl)xrLabel121).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel121).LocationFloat = new PointFloat(1112.652f, 5f);
			((XRControl)xrLabel121).Name = "xrLabel121";
			((XRControl)xrLabel121).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel121).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel121).StylePriority.UseBackColor = false;
			((XRControl)xrLabel121).StylePriority.UseBorders = false;
			((XRControl)xrLabel121).StylePriority.UseFont = false;
			((XRControl)xrLabel121).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel121).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel122).BackColor = Color.LightGray;
			((XRControl)xrLabel122).Borders = (BorderSide)0;
			((XRControl)xrLabel122).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Abril", "{0:n2}")
			});
			((XRControl)xrLabel122).Dpi = 254f;
			((XRControl)xrLabel122).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel122).LocationFloat = new PointFloat(930.6522f, 5f);
			((XRControl)xrLabel122).Name = "xrLabel122";
			((XRControl)xrLabel122).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel122).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel122).StylePriority.UseBackColor = false;
			((XRControl)xrLabel122).StylePriority.UseBorders = false;
			((XRControl)xrLabel122).StylePriority.UseFont = false;
			((XRControl)xrLabel122).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel122).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel123).BackColor = Color.LightGray;
			((XRControl)xrLabel123).Borders = (BorderSide)0;
			((XRControl)xrLabel123).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Marco", "{0:n2}")
			});
			((XRControl)xrLabel123).Dpi = 254f;
			((XRControl)xrLabel123).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel123).LocationFloat = new PointFloat(748.6522f, 5f);
			((XRControl)xrLabel123).Name = "xrLabel123";
			((XRControl)xrLabel123).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel123).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel123).StylePriority.UseBackColor = false;
			((XRControl)xrLabel123).StylePriority.UseBorders = false;
			((XRControl)xrLabel123).StylePriority.UseFont = false;
			((XRControl)xrLabel123).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel123).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel124).BackColor = Color.LightGray;
			((XRControl)xrLabel124).Borders = (BorderSide)0;
			((XRControl)xrLabel124).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel124).Dpi = 254f;
			((XRControl)xrLabel124).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel124).LocationFloat = new PointFloat(566.6522f, 5f);
			((XRControl)xrLabel124).Name = "xrLabel124";
			((XRControl)xrLabel124).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel124).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel124).StylePriority.UseBackColor = false;
			((XRControl)xrLabel124).StylePriority.UseBorders = false;
			((XRControl)xrLabel124).StylePriority.UseFont = false;
			((XRControl)xrLabel124).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel124).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel125).BackColor = Color.LightGray;
			((XRControl)xrLabel125).Borders = (BorderSide)0;
			((XRControl)xrLabel125).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel125).Dpi = 254f;
			((XRControl)xrLabel125).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel125).LocationFloat = new PointFloat(384.6522f, 5f);
			((XRControl)xrLabel125).Name = "xrLabel125";
			((XRControl)xrLabel125).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel125).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel125).StylePriority.UseBackColor = false;
			((XRControl)xrLabel125).StylePriority.UseBorders = false;
			((XRControl)xrLabel125).StylePriority.UseFont = false;
			((XRControl)xrLabel125).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel125).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel126).BackColor = Color.LightGray;
			((XRControl)xrLabel126).Borders = (BorderSide)0;
			((XRControl)xrLabel126).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "InventarioAnual.Tipo")
			});
			((XRControl)xrLabel126).Dpi = 254f;
			((XRControl)xrLabel126).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel126).LocationFloat = new PointFloat(0.5048828f, 5f);
			((XRControl)xrLabel126).Name = "xrLabel126";
			((XRControl)xrLabel126).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel126).SizeF = new SizeF(384.1473f, 47.84f);
			((XRControl)xrLabel126).StylePriority.UseBackColor = false;
			((XRControl)xrLabel126).StylePriority.UseBorders = false;
			((XRControl)xrLabel126).StylePriority.UseFont = false;
			((XRControl)xrLabel126).StylePriority.UsePadding = false;
			((XRControl)xrLabel126).StylePriority.UseTextAlignment = false;
			val7.FormatString = "{0:n2}";
			xrLabel126.Summary = val7;
			((XRControl)xrLabel126).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel126).WordWrap = false;
			((XtraReportBase)DetailReport8).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail9 });
			((XtraReportBase)DetailReport8).DataMember = "TotalGeralDisponibilidades";
			((XtraReportBase)DetailReport8).DataSource = bindingSource1;
			((XRControl)DetailReport8).Dpi = 254f;
			DetailReport8.Level = 7;
			((XRControl)DetailReport8).Name = "DetailReport8";
			((XRControl)Detail9).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel127,
				(XRControl)xrLabel128,
				(XRControl)xrLabel129,
				(XRControl)xrLabel130,
				(XRControl)xrLabel131,
				(XRControl)xrLabel132,
				(XRControl)xrLabel133,
				(XRControl)xrLabel134,
				(XRControl)xrLabel135,
				(XRControl)xrLabel136,
				(XRControl)xrLabel137,
				(XRControl)xrLabel138,
				(XRControl)xrLabel139,
				(XRControl)xrLabel140
			});
			((XRControl)Detail9).Dpi = 254f;
			((XRControl)Detail9).HeightF = 59.53125f;
			((XRControl)Detail9).Name = "Detail9";
			((XRControl)xrLabel127).BackColor = Color.LightGray;
			((XRControl)xrLabel127).Borders = (BorderSide)0;
			((XRControl)xrLabel127).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Total", "{0:n2}")
			});
			((XRControl)xrLabel127).Dpi = 254f;
			((XRControl)xrLabel127).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel127).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel127).Name = "xrLabel127";
			((XRControl)xrLabel127).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel127).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel127).StylePriority.UseBackColor = false;
			((XRControl)xrLabel127).StylePriority.UseBorders = false;
			((XRControl)xrLabel127).StylePriority.UseFont = false;
			((XRControl)xrLabel127).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel127).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel128).BackColor = Color.LightGray;
			((XRControl)xrLabel128).Borders = (BorderSide)0;
			((XRControl)xrLabel128).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel128).Dpi = 254f;
			((XRControl)xrLabel128).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel128).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel128).Name = "xrLabel128";
			((XRControl)xrLabel128).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel128).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel128).StylePriority.UseBackColor = false;
			((XRControl)xrLabel128).StylePriority.UseBorders = false;
			((XRControl)xrLabel128).StylePriority.UseFont = false;
			((XRControl)xrLabel128).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel128).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel129).BackColor = Color.LightGray;
			((XRControl)xrLabel129).Borders = (BorderSide)0;
			((XRControl)xrLabel129).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel129).Dpi = 254f;
			((XRControl)xrLabel129).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel129).LocationFloat = new PointFloat(2204.652f, 5f);
			((XRControl)xrLabel129).Name = "xrLabel129";
			((XRControl)xrLabel129).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel129).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel129).StylePriority.UseBackColor = false;
			((XRControl)xrLabel129).StylePriority.UseBorders = false;
			((XRControl)xrLabel129).StylePriority.UseFont = false;
			((XRControl)xrLabel129).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel129).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel130).BackColor = Color.LightGray;
			((XRControl)xrLabel130).Borders = (BorderSide)0;
			((XRControl)xrLabel130).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Outubro", "{0:n2}")
			});
			((XRControl)xrLabel130).Dpi = 254f;
			((XRControl)xrLabel130).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel130).LocationFloat = new PointFloat(2022.652f, 5f);
			((XRControl)xrLabel130).Name = "xrLabel130";
			((XRControl)xrLabel130).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel130).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel130).StylePriority.UseBackColor = false;
			((XRControl)xrLabel130).StylePriority.UseBorders = false;
			((XRControl)xrLabel130).StylePriority.UseFont = false;
			((XRControl)xrLabel130).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel130).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel131).BackColor = Color.LightGray;
			((XRControl)xrLabel131).Borders = (BorderSide)0;
			((XRControl)xrLabel131).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel131).Dpi = 254f;
			((XRControl)xrLabel131).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel131).LocationFloat = new PointFloat(1840.652f, 5f);
			((XRControl)xrLabel131).Name = "xrLabel131";
			((XRControl)xrLabel131).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel131).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel131).StylePriority.UseBackColor = false;
			((XRControl)xrLabel131).StylePriority.UseBorders = false;
			((XRControl)xrLabel131).StylePriority.UseFont = false;
			((XRControl)xrLabel131).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel131).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel132).BackColor = Color.LightGray;
			((XRControl)xrLabel132).Borders = (BorderSide)0;
			((XRControl)xrLabel132).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel132).Dpi = 254f;
			((XRControl)xrLabel132).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel132).LocationFloat = new PointFloat(1658.652f, 5f);
			((XRControl)xrLabel132).Name = "xrLabel132";
			((XRControl)xrLabel132).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel132).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel132).StylePriority.UseBackColor = false;
			((XRControl)xrLabel132).StylePriority.UseBorders = false;
			((XRControl)xrLabel132).StylePriority.UseFont = false;
			((XRControl)xrLabel132).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel132).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel133).BackColor = Color.LightGray;
			((XRControl)xrLabel133).Borders = (BorderSide)0;
			((XRControl)xrLabel133).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Julho", "{0:n2}")
			});
			((XRControl)xrLabel133).Dpi = 254f;
			((XRControl)xrLabel133).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel133).LocationFloat = new PointFloat(1476.652f, 5f);
			((XRControl)xrLabel133).Name = "xrLabel133";
			((XRControl)xrLabel133).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel133).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel133).StylePriority.UseBackColor = false;
			((XRControl)xrLabel133).StylePriority.UseBorders = false;
			((XRControl)xrLabel133).StylePriority.UseFont = false;
			((XRControl)xrLabel133).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel133).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel134).BackColor = Color.LightGray;
			((XRControl)xrLabel134).Borders = (BorderSide)0;
			((XRControl)xrLabel134).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Junho", "{0:n2}")
			});
			((XRControl)xrLabel134).Dpi = 254f;
			((XRControl)xrLabel134).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel134).LocationFloat = new PointFloat(1294.652f, 5f);
			((XRControl)xrLabel134).Name = "xrLabel134";
			((XRControl)xrLabel134).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel134).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel134).StylePriority.UseBackColor = false;
			((XRControl)xrLabel134).StylePriority.UseBorders = false;
			((XRControl)xrLabel134).StylePriority.UseFont = false;
			((XRControl)xrLabel134).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel134).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel135).BackColor = Color.LightGray;
			((XRControl)xrLabel135).Borders = (BorderSide)0;
			((XRControl)xrLabel135).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Maio", "{0:n2}")
			});
			((XRControl)xrLabel135).Dpi = 254f;
			((XRControl)xrLabel135).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel135).LocationFloat = new PointFloat(1112.652f, 5f);
			((XRControl)xrLabel135).Name = "xrLabel135";
			((XRControl)xrLabel135).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel135).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel135).StylePriority.UseBackColor = false;
			((XRControl)xrLabel135).StylePriority.UseBorders = false;
			((XRControl)xrLabel135).StylePriority.UseFont = false;
			((XRControl)xrLabel135).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel135).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel136).BackColor = Color.LightGray;
			((XRControl)xrLabel136).Borders = (BorderSide)0;
			((XRControl)xrLabel136).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Abril", "{0:n2}")
			});
			((XRControl)xrLabel136).Dpi = 254f;
			((XRControl)xrLabel136).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel136).LocationFloat = new PointFloat(930.6522f, 5f);
			((XRControl)xrLabel136).Name = "xrLabel136";
			((XRControl)xrLabel136).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel136).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel136).StylePriority.UseBackColor = false;
			((XRControl)xrLabel136).StylePriority.UseBorders = false;
			((XRControl)xrLabel136).StylePriority.UseFont = false;
			((XRControl)xrLabel136).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel136).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel137).BackColor = Color.LightGray;
			((XRControl)xrLabel137).Borders = (BorderSide)0;
			((XRControl)xrLabel137).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Marco", "{0:n2}")
			});
			((XRControl)xrLabel137).Dpi = 254f;
			((XRControl)xrLabel137).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel137).LocationFloat = new PointFloat(748.6522f, 5f);
			((XRControl)xrLabel137).Name = "xrLabel137";
			((XRControl)xrLabel137).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel137).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel137).StylePriority.UseBackColor = false;
			((XRControl)xrLabel137).StylePriority.UseBorders = false;
			((XRControl)xrLabel137).StylePriority.UseFont = false;
			((XRControl)xrLabel137).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel137).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel138).BackColor = Color.LightGray;
			((XRControl)xrLabel138).Borders = (BorderSide)0;
			((XRControl)xrLabel138).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel138).Dpi = 254f;
			((XRControl)xrLabel138).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel138).LocationFloat = new PointFloat(566.6522f, 5f);
			((XRControl)xrLabel138).Name = "xrLabel138";
			((XRControl)xrLabel138).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel138).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel138).StylePriority.UseBackColor = false;
			((XRControl)xrLabel138).StylePriority.UseBorders = false;
			((XRControl)xrLabel138).StylePriority.UseFont = false;
			((XRControl)xrLabel138).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel138).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel139).BackColor = Color.LightGray;
			((XRControl)xrLabel139).Borders = (BorderSide)0;
			((XRControl)xrLabel139).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel139).Dpi = 254f;
			((XRControl)xrLabel139).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel139).LocationFloat = new PointFloat(384.6522f, 5f);
			((XRControl)xrLabel139).Name = "xrLabel139";
			((XRControl)xrLabel139).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel139).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel139).StylePriority.UseBackColor = false;
			((XRControl)xrLabel139).StylePriority.UseBorders = false;
			((XRControl)xrLabel139).StylePriority.UseFont = false;
			((XRControl)xrLabel139).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel139).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel140).BackColor = Color.LightGray;
			((XRControl)xrLabel140).Borders = (BorderSide)0;
			((XRControl)xrLabel140).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalGeralDisponibilidades.Tipo")
			});
			((XRControl)xrLabel140).Dpi = 254f;
			((XRControl)xrLabel140).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel140).LocationFloat = new PointFloat(0.5048828f, 5f);
			((XRControl)xrLabel140).Name = "xrLabel140";
			((XRControl)xrLabel140).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel140).SizeF = new SizeF(384.1473f, 47.84f);
			((XRControl)xrLabel140).StylePriority.UseBackColor = false;
			((XRControl)xrLabel140).StylePriority.UseBorders = false;
			((XRControl)xrLabel140).StylePriority.UseFont = false;
			((XRControl)xrLabel140).StylePriority.UsePadding = false;
			((XRControl)xrLabel140).StylePriority.UseTextAlignment = false;
			val8.FormatString = "{0:n2}";
			xrLabel140.Summary = val8;
			((XRControl)xrLabel140).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel140).WordWrap = false;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[11]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)DetailReport1,
				(Band)DetailReport3,
				(Band)DetailReport4,
				(Band)DetailReport5,
				(Band)DetailReport6,
				(Band)DetailReport7,
				(Band)DetailReport8
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { UltimoNivelSemNegrito, UltimoNivelComNegrito });
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(0, 0, 0, 58);
			((XtraReport)this).PageHeight = 2159;
			((XtraReport)this).PageWidth = 2770;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class AnaliseFinanceira : XtraReport, IRelatorioParametros, IViewModelItemImprimirRelatorio, IViewModelImprimir, IDisposable
	{
		private readonly ObjetoImpressaoAnaliseFinanceira _objetoImpressaoAnaliseFinanceira;

		private bool _favorito;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRLabel lblCodigoUnico;

		private XRLabel xrLabel14;

		private XRLabel xrLabel47;

		private XRLabel xrLabel37;

		private XRLabel xrLabel40;

		private XRLabel xrLabel41;

		private XRLabel xrLabel42;

		private XRLabel xrLabel43;

		private XRLabel xrLabel44;

		private XRLabel xrLabel45;

		private XRLabel xrLabel46;

		private XRLabel xrLabel48;

		private XRLabel xrLabel49;

		private XRLabel xrLabel50;

		private XRLabel xrLabel51;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel lblValorTotal;

		private XRLabel lblValorDezembro;

		private XRLabel lblValorNovembro;

		private XRLabel lblValorOutubro;

		private XRLabel lblValorSetembro;

		private XRLabel lblValorAgosto;

		private XRLabel lblValorJulho;

		private XRLabel lblValorJunho;

		private XRLabel lblValorMaio;

		private XRLabel lblValorAbril;

		private XRLabel lblValorMarco;

		private XRLabel lblValorFevereiro;

		private XRLabel lblValorJaneiro;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private XRLabel xrLabel38;

		private XRLabel xrLabel58;

		private XRLabel xrLabel59;

		private XRLabel xrLabel60;

		private XRLabel xrLabel61;

		private XRLabel xrLabel62;

		private XRLabel xrLabel63;

		private XRLabel xrLabel64;

		private XRLabel xrLabel65;

		private XRLabel xrLabel66;

		private XRLabel xrLabel67;

		private XRLabel xrLabel68;

		private XRLabel xrLabel69;

		private XRLabel xrLabel70;

		private DetailReportBand DetailReport4;

		private DetailBand Detail5;

		private XRLabel xrLabel73;

		private XRLabel xrLabel74;

		private XRLabel xrLabel75;

		private XRLabel xrLabel76;

		private XRLabel xrLabel77;

		private XRLabel xrLabel78;

		private XRLabel xrLabel79;

		private XRLabel xrLabel80;

		private XRLabel xrLabel81;

		private XRLabel xrLabel82;

		private XRLabel xrLabel83;

		private XRLabel xrLabel84;

		private XRLabel xrLabel85;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel71;

		private FormattingRule UltimoNivelComNegrito;

		private FormattingRule UltimoNivelSemNegrito;

		public string TituloRelatorio => "Fluxo de caixa anual";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => false;

		public IList<TipoViewModelBase> Parametros { get; protected set; } = new List<TipoViewModelBase>();


		public string DescricaoDetalhada => string.Empty;

		public bool Favorito
		{
			get
			{
				return _favorito;
			}
			set
			{
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0018: Expected O, but got Unknown
				_favorito = value;
				DadosSistema.GetEventPublisher().Publish<FavoritouRelatorioEvent>(new FavoritouRelatorioEvent((XtraReport)(object)this, value));
			}
		}

		public bool FavoritouRelatorio { get; set; }

		public AnaliseFinanceira(ObjetoImpressaoAnaliseFinanceira objetoImpressaoAnaliseFinanceira)
		{
			_objetoImpressaoAnaliseFinanceira = objetoImpressaoAnaliseFinanceira;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			((XtraReportBase)this).DataSource = new List<ObjetoImpressaoAnaliseFinanceira> { _objetoImpressaoAnaliseFinanceira };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		public void SetarService(IWService service)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((XtraReport)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Expected O, but got Unknown
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Expected O, but got Unknown
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Expected O, but got Unknown
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Expected O, but got Unknown
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Expected O, but got Unknown
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Expected O, but got Unknown
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Expected O, but got Unknown
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Expected O, but got Unknown
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Expected O, but got Unknown
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Expected O, but got Unknown
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Expected O, but got Unknown
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_015c: Expected O, but got Unknown
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Expected O, but got Unknown
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Expected O, but got Unknown
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Expected O, but got Unknown
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Expected O, but got Unknown
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_0193: Expected O, but got Unknown
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Expected O, but got Unknown
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Expected O, but got Unknown
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Expected O, but got Unknown
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Expected O, but got Unknown
			//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Expected O, but got Unknown
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Expected O, but got Unknown
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Expected O, but got Unknown
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Expected O, but got Unknown
			//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Expected O, but got Unknown
			//IL_0208: Unknown result type (might be due to invalid IL or missing references)
			//IL_0212: Expected O, but got Unknown
			//IL_0213: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Expected O, but got Unknown
			//IL_021e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0228: Expected O, but got Unknown
			//IL_0229: Unknown result type (might be due to invalid IL or missing references)
			//IL_0233: Expected O, but got Unknown
			//IL_0234: Unknown result type (might be due to invalid IL or missing references)
			//IL_023e: Expected O, but got Unknown
			//IL_023f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0249: Expected O, but got Unknown
			//IL_024a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Expected O, but got Unknown
			//IL_0255: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Expected O, but got Unknown
			//IL_0260: Unknown result type (might be due to invalid IL or missing references)
			//IL_026a: Expected O, but got Unknown
			//IL_026b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0275: Expected O, but got Unknown
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_0280: Expected O, but got Unknown
			//IL_0281: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Expected O, but got Unknown
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Expected O, but got Unknown
			//IL_0297: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a1: Expected O, but got Unknown
			//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ac: Expected O, but got Unknown
			//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b7: Expected O, but got Unknown
			//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c2: Expected O, but got Unknown
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cd: Expected O, but got Unknown
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Expected O, but got Unknown
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e3: Expected O, but got Unknown
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Expected O, but got Unknown
			//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f9: Expected O, but got Unknown
			//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Expected O, but got Unknown
			//IL_0305: Unknown result type (might be due to invalid IL or missing references)
			//IL_030f: Expected O, but got Unknown
			//IL_0310: Unknown result type (might be due to invalid IL or missing references)
			//IL_031a: Expected O, but got Unknown
			//IL_041e: Unknown result type (might be due to invalid IL or missing references)
			//IL_049b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_059f: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe7: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1305: Unknown result type (might be due to invalid IL or missing references)
			//IL_1355: Unknown result type (might be due to invalid IL or missing references)
			//IL_135b: Expected O, but got Unknown
			//IL_13c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b7: Expected O, but got Unknown
			//IL_1722: Unknown result type (might be due to invalid IL or missing references)
			//IL_174b: Unknown result type (might be due to invalid IL or missing references)
			//IL_17ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1805: Expected O, but got Unknown
			//IL_1870: Unknown result type (might be due to invalid IL or missing references)
			//IL_1899: Unknown result type (might be due to invalid IL or missing references)
			//IL_194d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1953: Expected O, but got Unknown
			//IL_19be: Unknown result type (might be due to invalid IL or missing references)
			//IL_19e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa1: Expected O, but got Unknown
			//IL_1b0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b35: Unknown result type (might be due to invalid IL or missing references)
			//IL_1be9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bef: Expected O, but got Unknown
			//IL_1c5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c83: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d37: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d3d: Expected O, but got Unknown
			//IL_1da8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dd1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e85: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e8b: Expected O, but got Unknown
			//IL_1ef6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd9: Expected O, but got Unknown
			//IL_2044: Unknown result type (might be due to invalid IL or missing references)
			//IL_206d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2121: Unknown result type (might be due to invalid IL or missing references)
			//IL_2127: Expected O, but got Unknown
			//IL_2192: Unknown result type (might be due to invalid IL or missing references)
			//IL_21bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_226f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2275: Expected O, but got Unknown
			//IL_22e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2309: Unknown result type (might be due to invalid IL or missing references)
			//IL_23bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_23c3: Expected O, but got Unknown
			//IL_242e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2457: Unknown result type (might be due to invalid IL or missing references)
			//IL_250b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2511: Expected O, but got Unknown
			//IL_257c: Unknown result type (might be due to invalid IL or missing references)
			//IL_25a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2659: Unknown result type (might be due to invalid IL or missing references)
			//IL_265f: Expected O, but got Unknown
			//IL_26ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_26f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_28ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_28f3: Expected O, but got Unknown
			//IL_2932: Unknown result type (might be due to invalid IL or missing references)
			//IL_295b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a43: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a49: Expected O, but got Unknown
			//IL_2a88: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ab1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b65: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b6b: Expected O, but got Unknown
			//IL_2baa: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c87: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c8d: Expected O, but got Unknown
			//IL_2ccc: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cf5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2da4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2daa: Expected O, but got Unknown
			//IL_2de9: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e12: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ec6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ecc: Expected O, but got Unknown
			//IL_2f0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f34: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fe8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fee: Expected O, but got Unknown
			//IL_302d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3056: Unknown result type (might be due to invalid IL or missing references)
			//IL_310a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3110: Expected O, but got Unknown
			//IL_314f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3178: Unknown result type (might be due to invalid IL or missing references)
			//IL_322c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3232: Expected O, but got Unknown
			//IL_3271: Unknown result type (might be due to invalid IL or missing references)
			//IL_329a: Unknown result type (might be due to invalid IL or missing references)
			//IL_334e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3354: Expected O, but got Unknown
			//IL_3393: Unknown result type (might be due to invalid IL or missing references)
			//IL_33bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_346b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3471: Expected O, but got Unknown
			//IL_34b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_34d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_358d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3593: Expected O, but got Unknown
			//IL_35d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_35fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_36af: Unknown result type (might be due to invalid IL or missing references)
			//IL_36b5: Expected O, but got Unknown
			//IL_36f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_371d: Unknown result type (might be due to invalid IL or missing references)
			//IL_37cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_37d2: Expected O, but got Unknown
			//IL_3811: Unknown result type (might be due to invalid IL or missing references)
			//IL_383a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a23: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a29: Expected O, but got Unknown
			//IL_3a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a91: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b45: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b4b: Expected O, but got Unknown
			//IL_3b8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c67: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c6d: Expected O, but got Unknown
			//IL_3cac: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d89: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d8f: Expected O, but got Unknown
			//IL_3dce: Unknown result type (might be due to invalid IL or missing references)
			//IL_3df7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3eab: Unknown result type (might be due to invalid IL or missing references)
			//IL_3eb1: Expected O, but got Unknown
			//IL_3ef0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f19: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fd3: Expected O, but got Unknown
			//IL_4012: Unknown result type (might be due to invalid IL or missing references)
			//IL_403b: Unknown result type (might be due to invalid IL or missing references)
			//IL_40ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_40f5: Expected O, but got Unknown
			//IL_4134: Unknown result type (might be due to invalid IL or missing references)
			//IL_415d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4211: Unknown result type (might be due to invalid IL or missing references)
			//IL_4217: Expected O, but got Unknown
			//IL_4256: Unknown result type (might be due to invalid IL or missing references)
			//IL_427f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4333: Unknown result type (might be due to invalid IL or missing references)
			//IL_4339: Expected O, but got Unknown
			//IL_4378: Unknown result type (might be due to invalid IL or missing references)
			//IL_43a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_4455: Unknown result type (might be due to invalid IL or missing references)
			//IL_445b: Expected O, but got Unknown
			//IL_449a: Unknown result type (might be due to invalid IL or missing references)
			//IL_44c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_4577: Unknown result type (might be due to invalid IL or missing references)
			//IL_457d: Expected O, but got Unknown
			//IL_45bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_45e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_4699: Unknown result type (might be due to invalid IL or missing references)
			//IL_469f: Expected O, but got Unknown
			//IL_46de: Unknown result type (might be due to invalid IL or missing references)
			//IL_4707: Unknown result type (might be due to invalid IL or missing references)
			//IL_47b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_47bc: Expected O, but got Unknown
			//IL_47fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_4824: Unknown result type (might be due to invalid IL or missing references)
			//IL_4973: Unknown result type (might be due to invalid IL or missing references)
			//IL_499c: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			XRSummary val3 = new XRSummary();
			Detail = new DetailBand();
			xrLabel14 = new XRLabel();
			xrLabel47 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel40 = new XRLabel();
			xrLabel41 = new XRLabel();
			xrLabel42 = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel44 = new XRLabel();
			xrLabel45 = new XRLabel();
			xrLabel46 = new XRLabel();
			xrLabel48 = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel50 = new XRLabel();
			xrLabel51 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			lblCodigoUnico = new XRLabel();
			UltimoNivelComNegrito = new FormattingRule();
			UltimoNivelSemNegrito = new FormattingRule();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			lblValorTotal = new XRLabel();
			lblValorDezembro = new XRLabel();
			lblValorNovembro = new XRLabel();
			lblValorOutubro = new XRLabel();
			lblValorSetembro = new XRLabel();
			lblValorAgosto = new XRLabel();
			lblValorJulho = new XRLabel();
			lblValorJunho = new XRLabel();
			lblValorMaio = new XRLabel();
			lblValorAbril = new XRLabel();
			lblValorMarco = new XRLabel();
			lblValorFevereiro = new XRLabel();
			lblValorJaneiro = new XRLabel();
			bindingSource1 = new BindingSource(components);
			DetailReport3 = new DetailReportBand();
			Detail4 = new DetailBand();
			xrLabel38 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel59 = new XRLabel();
			xrLabel60 = new XRLabel();
			xrLabel61 = new XRLabel();
			xrLabel62 = new XRLabel();
			xrLabel63 = new XRLabel();
			xrLabel64 = new XRLabel();
			xrLabel65 = new XRLabel();
			xrLabel66 = new XRLabel();
			xrLabel67 = new XRLabel();
			xrLabel68 = new XRLabel();
			xrLabel69 = new XRLabel();
			xrLabel70 = new XRLabel();
			DetailReport4 = new DetailReportBand();
			Detail5 = new DetailBand();
			xrLabel73 = new XRLabel();
			xrLabel74 = new XRLabel();
			xrLabel75 = new XRLabel();
			xrLabel76 = new XRLabel();
			xrLabel77 = new XRLabel();
			xrLabel78 = new XRLabel();
			xrLabel79 = new XRLabel();
			xrLabel80 = new XRLabel();
			xrLabel81 = new XRLabel();
			xrLabel82 = new XRLabel();
			xrLabel83 = new XRLabel();
			xrLabel84 = new XRLabel();
			xrLabel85 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel71 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel14,
				(XRControl)xrLabel47,
				(XRControl)xrLabel37,
				(XRControl)xrLabel40,
				(XRControl)xrLabel41,
				(XRControl)xrLabel42,
				(XRControl)xrLabel43,
				(XRControl)xrLabel44,
				(XRControl)xrLabel45,
				(XRControl)xrLabel46,
				(XRControl)xrLabel48,
				(XRControl)xrLabel49,
				(XRControl)xrLabel50,
				(XRControl)xrLabel51
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail).HeightF = 103.3225f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel14).Borders = (BorderSide)0;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel14).ForeColor = Color.Gray;
			((XRControl)xrLabel14).LocationFloat = new PointFloat(384.6528f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseForeColor = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Janeiro";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel47).BackColor = Color.LightGray;
			((XRControl)xrLabel47).Borders = (BorderSide)8;
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(0.6734619f, 52.84f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(2768.653f, 50.4825f);
			((XRControl)xrLabel47).StylePriority.UseBackColor = false;
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel47).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel47).Text = "PLANO DE CONTAS";
			((XRControl)xrLabel47).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel37).Borders = (BorderSide)0;
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel37).ForeColor = Color.Gray;
			((XRControl)xrLabel37).LocationFloat = new PointFloat(566.6528f, 0f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel37).StylePriority.UseBorders = false;
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseForeColor = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "Fevereiro";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel40).Borders = (BorderSide)0;
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel40).ForeColor = Color.Gray;
			((XRControl)xrLabel40).LocationFloat = new PointFloat(748.6528f, 0f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseFont = false;
			((XRControl)xrLabel40).StylePriority.UseForeColor = false;
			((XRControl)xrLabel40).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel40).Text = "Março";
			((XRControl)xrLabel40).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel41).Borders = (BorderSide)0;
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel41).ForeColor = Color.Gray;
			((XRControl)xrLabel41).LocationFloat = new PointFloat(930.6528f, 0f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel41).StylePriority.UseBorders = false;
			((XRControl)xrLabel41).StylePriority.UseFont = false;
			((XRControl)xrLabel41).StylePriority.UseForeColor = false;
			((XRControl)xrLabel41).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel41).Text = "Abril";
			((XRControl)xrLabel41).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel42).Borders = (BorderSide)0;
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel42).ForeColor = Color.Gray;
			((XRControl)xrLabel42).LocationFloat = new PointFloat(1112.653f, 0f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseFont = false;
			((XRControl)xrLabel42).StylePriority.UseForeColor = false;
			((XRControl)xrLabel42).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel42).Text = "Maio";
			((XRControl)xrLabel42).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel43).Borders = (BorderSide)0;
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel43).ForeColor = Color.Gray;
			((XRControl)xrLabel43).LocationFloat = new PointFloat(1294.653f, 0f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel43).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel43).StylePriority.UseForeColor = false;
			((XRControl)xrLabel43).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel43).Text = "Junho";
			((XRControl)xrLabel43).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel44).Borders = (BorderSide)0;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel44).ForeColor = Color.Gray;
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1476.653f, 0f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UseForeColor = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).Text = "Julho";
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel45).Borders = (BorderSide)0;
			((XRControl)xrLabel45).Dpi = 254f;
			((XRControl)xrLabel45).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel45).ForeColor = Color.Gray;
			((XRControl)xrLabel45).LocationFloat = new PointFloat(1658.653f, 0f);
			((XRControl)xrLabel45).Name = "xrLabel45";
			((XRControl)xrLabel45).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel45).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel45).StylePriority.UseBorders = false;
			((XRControl)xrLabel45).StylePriority.UseFont = false;
			((XRControl)xrLabel45).StylePriority.UseForeColor = false;
			((XRControl)xrLabel45).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel45).Text = "Agosto";
			((XRControl)xrLabel45).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel46).Borders = (BorderSide)0;
			((XRControl)xrLabel46).Dpi = 254f;
			((XRControl)xrLabel46).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel46).ForeColor = Color.Gray;
			((XRControl)xrLabel46).LocationFloat = new PointFloat(1840.653f, 0f);
			((XRControl)xrLabel46).Name = "xrLabel46";
			((XRControl)xrLabel46).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel46).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel46).StylePriority.UseBorders = false;
			((XRControl)xrLabel46).StylePriority.UseFont = false;
			((XRControl)xrLabel46).StylePriority.UseForeColor = false;
			((XRControl)xrLabel46).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel46).Text = "Setembro";
			((XRControl)xrLabel46).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel48).Borders = (BorderSide)0;
			((XRControl)xrLabel48).Dpi = 254f;
			((XRControl)xrLabel48).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel48).ForeColor = Color.Gray;
			((XRControl)xrLabel48).LocationFloat = new PointFloat(2022.653f, 0f);
			((XRControl)xrLabel48).Name = "xrLabel48";
			((XRControl)xrLabel48).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel48).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel48).StylePriority.UseBorders = false;
			((XRControl)xrLabel48).StylePriority.UseFont = false;
			((XRControl)xrLabel48).StylePriority.UseForeColor = false;
			((XRControl)xrLabel48).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel48).Text = "Outubro";
			((XRControl)xrLabel48).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel49).Borders = (BorderSide)0;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel49).ForeColor = Color.Gray;
			((XRControl)xrLabel49).LocationFloat = new PointFloat(2204.653f, 0f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).StylePriority.UseForeColor = false;
			((XRControl)xrLabel49).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel49).Text = "Novembro";
			((XRControl)xrLabel49).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel50).Borders = (BorderSide)0;
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel50).ForeColor = Color.Gray;
			((XRControl)xrLabel50).LocationFloat = new PointFloat(2386.653f, 0f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel50).StylePriority.UseForeColor = false;
			((XRControl)xrLabel50).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel50).Text = "Dezembro";
			((XRControl)xrLabel50).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel51).Borders = (BorderSide)0;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI Semibold", 8f);
			((XRControl)xrLabel51).ForeColor = Color.Gray;
			((XRControl)xrLabel51).LocationFloat = new PointFloat(2568.653f, 0f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseForeColor = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "TOTAL";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)4;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 58.20833f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)lblCodigoUnico).BackColor = Color.Transparent;
			((XRControl)lblCodigoUnico).Borders = (BorderSide)0;
			((XRControl)lblCodigoUnico).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.CodigoUnicoDescricaoToText")
			});
			((XRControl)lblCodigoUnico).Dpi = 254f;
			((XRControl)lblCodigoUnico).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblCodigoUnico).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblCodigoUnico).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblCodigoUnico).LocationFloat = new PointFloat(35.61445f, 5f);
			((XRControl)lblCodigoUnico).Name = "lblCodigoUnico";
			((XRControl)lblCodigoUnico).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblCodigoUnico).SizeF = new SizeF(349.0384f, 47.84f);
			((XRControl)lblCodigoUnico).StylePriority.UseBackColor = false;
			((XRControl)lblCodigoUnico).StylePriority.UseBorders = false;
			((XRControl)lblCodigoUnico).StylePriority.UseFont = false;
			((XRControl)lblCodigoUnico).StylePriority.UsePadding = false;
			((XRControl)lblCodigoUnico).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:n2}";
			lblCodigoUnico.Summary = val;
			((XRControl)lblCodigoUnico).TextAlignment = (TextAlignment)16;
			((XRControl)lblCodigoUnico).WordWrap = false;
			UltimoNivelComNegrito.Condition = "[IsUltimoNivel] == False";
			((DataContainerComponent)UltimoNivelComNegrito).DataMember = "BalanceteItens";
			((BrickStyle)UltimoNivelComNegrito.Formatting).Font = new Font("Segoe UI Semibold", 7.5f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			UltimoNivelComNegrito.Name = "UltimoNivelComNegrito";
			UltimoNivelSemNegrito.Condition = "[BalanceteItens.IsUltimoNivel] == True";
			((DataContainerComponent)UltimoNivelSemNegrito).DataMember = "BalanceteItens";
			((BrickStyle)UltimoNivelSemNegrito.Formatting).Font = new Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			UltimoNivelSemNegrito.Name = "UltimoNivelSemNegrito";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail1 });
			((XtraReportBase)DetailReport).DataMember = "BalanceteItens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)lblValorTotal,
				(XRControl)lblValorDezembro,
				(XRControl)lblValorNovembro,
				(XRControl)lblValorOutubro,
				(XRControl)lblValorSetembro,
				(XRControl)lblValorAgosto,
				(XRControl)lblValorJulho,
				(XRControl)lblValorJunho,
				(XRControl)lblValorMaio,
				(XRControl)lblValorAbril,
				(XRControl)lblValorMarco,
				(XRControl)lblValorFevereiro,
				(XRControl)lblValorJaneiro,
				(XRControl)lblCodigoUnico
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 58.8643f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)lblValorTotal).BackColor = Color.Transparent;
			((XRControl)lblValorTotal).Borders = (BorderSide)0;
			((XRControl)lblValorTotal).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.Total", "{0:n2}")
			});
			((XRControl)lblValorTotal).Dpi = 254f;
			((XRControl)lblValorTotal).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorTotal).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorTotal).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorTotal).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)lblValorTotal).Name = "lblValorTotal";
			((XRControl)lblValorTotal).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorTotal).SizeF = new SizeF(200f, 47.84f);
			((XRControl)lblValorTotal).StylePriority.UseBackColor = false;
			((XRControl)lblValorTotal).StylePriority.UseBorders = false;
			((XRControl)lblValorTotal).StylePriority.UseFont = false;
			((XRControl)lblValorTotal).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorTotal).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorDezembro).BackColor = Color.Transparent;
			((XRControl)lblValorDezembro).Borders = (BorderSide)0;
			((XRControl)lblValorDezembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorDezembro", "{0:n2}")
			});
			((XRControl)lblValorDezembro).Dpi = 254f;
			((XRControl)lblValorDezembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorDezembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorDezembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorDezembro).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)lblValorDezembro).Name = "lblValorDezembro";
			((XRControl)lblValorDezembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorDezembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorDezembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorDezembro).StylePriority.UseBorders = false;
			((XRControl)lblValorDezembro).StylePriority.UseFont = false;
			((XRControl)lblValorDezembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorDezembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorNovembro).BackColor = Color.Transparent;
			((XRControl)lblValorNovembro).Borders = (BorderSide)0;
			((XRControl)lblValorNovembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorNovembro", "{0:n2}")
			});
			((XRControl)lblValorNovembro).Dpi = 254f;
			((XRControl)lblValorNovembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorNovembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorNovembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorNovembro).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)lblValorNovembro).Name = "lblValorNovembro";
			((XRControl)lblValorNovembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorNovembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorNovembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorNovembro).StylePriority.UseBorders = false;
			((XRControl)lblValorNovembro).StylePriority.UseFont = false;
			((XRControl)lblValorNovembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorNovembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorOutubro).BackColor = Color.Transparent;
			((XRControl)lblValorOutubro).Borders = (BorderSide)0;
			((XRControl)lblValorOutubro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorOutubro", "{0:n2}")
			});
			((XRControl)lblValorOutubro).Dpi = 254f;
			((XRControl)lblValorOutubro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorOutubro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorOutubro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorOutubro).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)lblValorOutubro).Name = "lblValorOutubro";
			((XRControl)lblValorOutubro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorOutubro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorOutubro).StylePriority.UseBackColor = false;
			((XRControl)lblValorOutubro).StylePriority.UseBorders = false;
			((XRControl)lblValorOutubro).StylePriority.UseFont = false;
			((XRControl)lblValorOutubro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorOutubro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorSetembro).BackColor = Color.Transparent;
			((XRControl)lblValorSetembro).Borders = (BorderSide)0;
			((XRControl)lblValorSetembro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorSetembro", "{0:n2}")
			});
			((XRControl)lblValorSetembro).Dpi = 254f;
			((XRControl)lblValorSetembro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorSetembro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorSetembro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorSetembro).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)lblValorSetembro).Name = "lblValorSetembro";
			((XRControl)lblValorSetembro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorSetembro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorSetembro).StylePriority.UseBackColor = false;
			((XRControl)lblValorSetembro).StylePriority.UseBorders = false;
			((XRControl)lblValorSetembro).StylePriority.UseFont = false;
			((XRControl)lblValorSetembro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorSetembro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorAgosto).BackColor = Color.Transparent;
			((XRControl)lblValorAgosto).Borders = (BorderSide)0;
			((XRControl)lblValorAgosto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorAgosto", "{0:n2}")
			});
			((XRControl)lblValorAgosto).Dpi = 254f;
			((XRControl)lblValorAgosto).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAgosto).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAgosto).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorAgosto).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)lblValorAgosto).Name = "lblValorAgosto";
			((XRControl)lblValorAgosto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorAgosto).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorAgosto).StylePriority.UseBackColor = false;
			((XRControl)lblValorAgosto).StylePriority.UseBorders = false;
			((XRControl)lblValorAgosto).StylePriority.UseFont = false;
			((XRControl)lblValorAgosto).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorAgosto).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJulho).BackColor = Color.Transparent;
			((XRControl)lblValorJulho).Borders = (BorderSide)0;
			((XRControl)lblValorJulho).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJulho", "{0:n2}")
			});
			((XRControl)lblValorJulho).Dpi = 254f;
			((XRControl)lblValorJulho).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJulho).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJulho).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJulho).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)lblValorJulho).Name = "lblValorJulho";
			((XRControl)lblValorJulho).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJulho).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJulho).StylePriority.UseBackColor = false;
			((XRControl)lblValorJulho).StylePriority.UseBorders = false;
			((XRControl)lblValorJulho).StylePriority.UseFont = false;
			((XRControl)lblValorJulho).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJulho).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJunho).BackColor = Color.Transparent;
			((XRControl)lblValorJunho).Borders = (BorderSide)0;
			((XRControl)lblValorJunho).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJunho", "{0:n2}")
			});
			((XRControl)lblValorJunho).Dpi = 254f;
			((XRControl)lblValorJunho).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJunho).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJunho).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJunho).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)lblValorJunho).Name = "lblValorJunho";
			((XRControl)lblValorJunho).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJunho).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJunho).StylePriority.UseBackColor = false;
			((XRControl)lblValorJunho).StylePriority.UseBorders = false;
			((XRControl)lblValorJunho).StylePriority.UseFont = false;
			((XRControl)lblValorJunho).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJunho).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorMaio).BackColor = Color.Transparent;
			((XRControl)lblValorMaio).Borders = (BorderSide)0;
			((XRControl)lblValorMaio).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorMaio", "{0:n2}")
			});
			((XRControl)lblValorMaio).Dpi = 254f;
			((XRControl)lblValorMaio).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMaio).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMaio).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorMaio).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)lblValorMaio).Name = "lblValorMaio";
			((XRControl)lblValorMaio).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorMaio).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorMaio).StylePriority.UseBackColor = false;
			((XRControl)lblValorMaio).StylePriority.UseBorders = false;
			((XRControl)lblValorMaio).StylePriority.UseFont = false;
			((XRControl)lblValorMaio).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorMaio).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorAbril).BackColor = Color.Transparent;
			((XRControl)lblValorAbril).Borders = (BorderSide)0;
			((XRControl)lblValorAbril).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorAbril", "{0:n2}")
			});
			((XRControl)lblValorAbril).Dpi = 254f;
			((XRControl)lblValorAbril).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAbril).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorAbril).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorAbril).LocationFloat = new PointFloat(930.6528f, 5f);
			((XRControl)lblValorAbril).Name = "lblValorAbril";
			((XRControl)lblValorAbril).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorAbril).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorAbril).StylePriority.UseBackColor = false;
			((XRControl)lblValorAbril).StylePriority.UseBorders = false;
			((XRControl)lblValorAbril).StylePriority.UseFont = false;
			((XRControl)lblValorAbril).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorAbril).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorMarco).BackColor = Color.Transparent;
			((XRControl)lblValorMarco).Borders = (BorderSide)0;
			((XRControl)lblValorMarco).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorMarco", "{0:n2}")
			});
			((XRControl)lblValorMarco).Dpi = 254f;
			((XRControl)lblValorMarco).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMarco).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorMarco).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorMarco).LocationFloat = new PointFloat(748.6528f, 5f);
			((XRControl)lblValorMarco).Name = "lblValorMarco";
			((XRControl)lblValorMarco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorMarco).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorMarco).StylePriority.UseBackColor = false;
			((XRControl)lblValorMarco).StylePriority.UseBorders = false;
			((XRControl)lblValorMarco).StylePriority.UseFont = false;
			((XRControl)lblValorMarco).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorMarco).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorFevereiro).BackColor = Color.Transparent;
			((XRControl)lblValorFevereiro).Borders = (BorderSide)0;
			((XRControl)lblValorFevereiro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorFevereiro", "{0:n2}")
			});
			((XRControl)lblValorFevereiro).Dpi = 254f;
			((XRControl)lblValorFevereiro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorFevereiro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorFevereiro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorFevereiro).LocationFloat = new PointFloat(566.6528f, 5f);
			((XRControl)lblValorFevereiro).Name = "lblValorFevereiro";
			((XRControl)lblValorFevereiro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorFevereiro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorFevereiro).StylePriority.UseBackColor = false;
			((XRControl)lblValorFevereiro).StylePriority.UseBorders = false;
			((XRControl)lblValorFevereiro).StylePriority.UseFont = false;
			((XRControl)lblValorFevereiro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorFevereiro).TextAlignment = (TextAlignment)64;
			((XRControl)lblValorJaneiro).BackColor = Color.Transparent;
			((XRControl)lblValorJaneiro).Borders = (BorderSide)0;
			((XRControl)lblValorJaneiro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "BalanceteItens.ValorJaneiro", "{0:n2}")
			});
			((XRControl)lblValorJaneiro).Dpi = 254f;
			((XRControl)lblValorJaneiro).Font = new Font("Segoe UI", 7.5f);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJaneiro).FormattingRules).Add(UltimoNivelComNegrito);
			((Collection<FormattingRule>)(object)((XRControl)lblValorJaneiro).FormattingRules).Add(UltimoNivelSemNegrito);
			((XRControl)lblValorJaneiro).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)lblValorJaneiro).Name = "lblValorJaneiro";
			((XRControl)lblValorJaneiro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblValorJaneiro).SizeF = new SizeF(182f, 47.84f);
			((XRControl)lblValorJaneiro).StylePriority.UseBackColor = false;
			((XRControl)lblValorJaneiro).StylePriority.UseBorders = false;
			((XRControl)lblValorJaneiro).StylePriority.UseFont = false;
			((XRControl)lblValorJaneiro).StylePriority.UseTextAlignment = false;
			((XRControl)lblValorJaneiro).TextAlignment = (TextAlignment)64;
			bindingSource1.DataSource = typeof(ObjetoImpressaoAnaliseFinanceira);
			((XtraReportBase)DetailReport3).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail4 });
			((XtraReportBase)DetailReport3).DataMember = "FluxosCaixa";
			((XtraReportBase)DetailReport3).DataSource = bindingSource1;
			((XRControl)DetailReport3).Dpi = 254f;
			DetailReport3.Level = 1;
			((XRControl)DetailReport3).Name = "DetailReport3";
			((XRControl)Detail4).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrLabel38,
				(XRControl)xrLabel58,
				(XRControl)xrLabel59,
				(XRControl)xrLabel60,
				(XRControl)xrLabel61,
				(XRControl)xrLabel62,
				(XRControl)xrLabel63,
				(XRControl)xrLabel64,
				(XRControl)xrLabel65,
				(XRControl)xrLabel66,
				(XRControl)xrLabel67,
				(XRControl)xrLabel68,
				(XRControl)xrLabel69,
				(XRControl)xrLabel70
			});
			((XRControl)Detail4).Dpi = 254f;
			((XRControl)Detail4).HeightF = 55.56252f;
			((XRControl)Detail4).Name = "Detail4";
			((XRControl)xrLabel38).BackColor = Color.LightGray;
			((XRControl)xrLabel38).Borders = (BorderSide)0;
			((XRControl)xrLabel38).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Tipo")
			});
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel38).LocationFloat = new PointFloat(0.3366699f, 5f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(384.3155f, 47.84f);
			((XRControl)xrLabel38).StylePriority.UseBackColor = false;
			((XRControl)xrLabel38).StylePriority.UseBorders = false;
			((XRControl)xrLabel38).StylePriority.UseFont = false;
			((XRControl)xrLabel38).StylePriority.UsePadding = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:n2}";
			xrLabel38.Summary = val2;
			((XRControl)xrLabel38).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel38).WordWrap = false;
			((XRControl)xrLabel58).BackColor = Color.LightGray;
			((XRControl)xrLabel58).Borders = (BorderSide)0;
			((XRControl)xrLabel58).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Janeiro", "{0:n2}")
			});
			((XRControl)xrLabel58).Dpi = 254f;
			((XRControl)xrLabel58).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel58).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)xrLabel58).Name = "xrLabel58";
			((XRControl)xrLabel58).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel58).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel58).StylePriority.UseBackColor = false;
			((XRControl)xrLabel58).StylePriority.UseBorders = false;
			((XRControl)xrLabel58).StylePriority.UseFont = false;
			((XRControl)xrLabel58).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel58).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel59).BackColor = Color.LightGray;
			((XRControl)xrLabel59).Borders = (BorderSide)0;
			((XRControl)xrLabel59).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Fevereiro", "{0:n2}")
			});
			((XRControl)xrLabel59).Dpi = 254f;
			((XRControl)xrLabel59).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel59).LocationFloat = new PointFloat(566.6528f, 5f);
			((XRControl)xrLabel59).Name = "xrLabel59";
			((XRControl)xrLabel59).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel59).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel59).StylePriority.UseBackColor = false;
			((XRControl)xrLabel59).StylePriority.UseBorders = false;
			((XRControl)xrLabel59).StylePriority.UseFont = false;
			((XRControl)xrLabel59).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel59).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel60).BackColor = Color.LightGray;
			((XRControl)xrLabel60).Borders = (BorderSide)0;
			((XRControl)xrLabel60).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Marco", "{0:n2}")
			});
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(748.6528f, 5f);
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel60).StylePriority.UseBackColor = false;
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrLabel60).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel60).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel61).BackColor = Color.LightGray;
			((XRControl)xrLabel61).Borders = (BorderSide)0;
			((XRControl)xrLabel61).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Abril")
			});
			((XRControl)xrLabel61).Dpi = 254f;
			((XRControl)xrLabel61).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel61).LocationFloat = new PointFloat(930.6528f, 5f);
			((XRControl)xrLabel61).Name = "xrLabel61";
			((XRControl)xrLabel61).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel61).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel61).StylePriority.UseBackColor = false;
			((XRControl)xrLabel61).StylePriority.UseBorders = false;
			((XRControl)xrLabel61).StylePriority.UseFont = false;
			((XRControl)xrLabel61).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel61).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel62).BackColor = Color.LightGray;
			((XRControl)xrLabel62).Borders = (BorderSide)0;
			((XRControl)xrLabel62).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Maio", "{0:n2}")
			});
			((XRControl)xrLabel62).Dpi = 254f;
			((XRControl)xrLabel62).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel62).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel62).Name = "xrLabel62";
			((XRControl)xrLabel62).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel62).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel62).StylePriority.UseBackColor = false;
			((XRControl)xrLabel62).StylePriority.UseBorders = false;
			((XRControl)xrLabel62).StylePriority.UseFont = false;
			((XRControl)xrLabel62).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel62).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel63).BackColor = Color.LightGray;
			((XRControl)xrLabel63).Borders = (BorderSide)0;
			((XRControl)xrLabel63).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Junho", "{0:n2}")
			});
			((XRControl)xrLabel63).Dpi = 254f;
			((XRControl)xrLabel63).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel63).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel63).Name = "xrLabel63";
			((XRControl)xrLabel63).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel63).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel63).StylePriority.UseBackColor = false;
			((XRControl)xrLabel63).StylePriority.UseBorders = false;
			((XRControl)xrLabel63).StylePriority.UseFont = false;
			((XRControl)xrLabel63).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel63).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel64).BackColor = Color.LightGray;
			((XRControl)xrLabel64).Borders = (BorderSide)0;
			((XRControl)xrLabel64).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Julho", "{0:n2}")
			});
			((XRControl)xrLabel64).Dpi = 254f;
			((XRControl)xrLabel64).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel64).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel64).Name = "xrLabel64";
			((XRControl)xrLabel64).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel64).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel64).StylePriority.UseBackColor = false;
			((XRControl)xrLabel64).StylePriority.UseBorders = false;
			((XRControl)xrLabel64).StylePriority.UseFont = false;
			((XRControl)xrLabel64).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel64).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel65).BackColor = Color.LightGray;
			((XRControl)xrLabel65).Borders = (BorderSide)0;
			((XRControl)xrLabel65).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Agosto", "{0:n2}")
			});
			((XRControl)xrLabel65).Dpi = 254f;
			((XRControl)xrLabel65).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel65).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel65).Name = "xrLabel65";
			((XRControl)xrLabel65).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel65).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel65).StylePriority.UseBackColor = false;
			((XRControl)xrLabel65).StylePriority.UseBorders = false;
			((XRControl)xrLabel65).StylePriority.UseFont = false;
			((XRControl)xrLabel65).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel65).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel66).BackColor = Color.LightGray;
			((XRControl)xrLabel66).Borders = (BorderSide)0;
			((XRControl)xrLabel66).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Setembro", "{0:n2}")
			});
			((XRControl)xrLabel66).Dpi = 254f;
			((XRControl)xrLabel66).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel66).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel66).Name = "xrLabel66";
			((XRControl)xrLabel66).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel66).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel66).StylePriority.UseBackColor = false;
			((XRControl)xrLabel66).StylePriority.UseBorders = false;
			((XRControl)xrLabel66).StylePriority.UseFont = false;
			((XRControl)xrLabel66).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel66).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel67).BackColor = Color.LightGray;
			((XRControl)xrLabel67).Borders = (BorderSide)0;
			((XRControl)xrLabel67).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Outubro")
			});
			((XRControl)xrLabel67).Dpi = 254f;
			((XRControl)xrLabel67).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel67).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel67).Name = "xrLabel67";
			((XRControl)xrLabel67).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel67).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel67).StylePriority.UseBackColor = false;
			((XRControl)xrLabel67).StylePriority.UseBorders = false;
			((XRControl)xrLabel67).StylePriority.UseFont = false;
			((XRControl)xrLabel67).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel67).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel68).BackColor = Color.LightGray;
			((XRControl)xrLabel68).Borders = (BorderSide)0;
			((XRControl)xrLabel68).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Novembro", "{0:n2}")
			});
			((XRControl)xrLabel68).Dpi = 254f;
			((XRControl)xrLabel68).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel68).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel68).Name = "xrLabel68";
			((XRControl)xrLabel68).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel68).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel68).StylePriority.UseBackColor = false;
			((XRControl)xrLabel68).StylePriority.UseBorders = false;
			((XRControl)xrLabel68).StylePriority.UseFont = false;
			((XRControl)xrLabel68).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel68).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel69).BackColor = Color.LightGray;
			((XRControl)xrLabel69).Borders = (BorderSide)0;
			((XRControl)xrLabel69).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Dezembro", "{0:n2}")
			});
			((XRControl)xrLabel69).Dpi = 254f;
			((XRControl)xrLabel69).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel69).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel69).Name = "xrLabel69";
			((XRControl)xrLabel69).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel69).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel69).StylePriority.UseBackColor = false;
			((XRControl)xrLabel69).StylePriority.UseBorders = false;
			((XRControl)xrLabel69).StylePriority.UseFont = false;
			((XRControl)xrLabel69).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel69).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel70).BackColor = Color.LightGray;
			((XRControl)xrLabel70).Borders = (BorderSide)0;
			((XRControl)xrLabel70).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FluxosCaixa.Total")
			});
			((XRControl)xrLabel70).Dpi = 254f;
			((XRControl)xrLabel70).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel70).LocationFloat = new PointFloat(2568.653f, 5f);
			((XRControl)xrLabel70).Name = "xrLabel70";
			((XRControl)xrLabel70).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel70).SizeF = new SizeF(200f, 47.84f);
			((XRControl)xrLabel70).StylePriority.UseBackColor = false;
			((XRControl)xrLabel70).StylePriority.UseBorders = false;
			((XRControl)xrLabel70).StylePriority.UseFont = false;
			((XRControl)xrLabel70).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel70).TextAlignment = (TextAlignment)64;
			((XtraReportBase)DetailReport4).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail5,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport4).DataMember = "SaldosContas";
			((XtraReportBase)DetailReport4).DataSource = bindingSource1;
			((XRControl)DetailReport4).Dpi = 254f;
			DetailReport4.Level = 2;
			((XRControl)DetailReport4).Name = "DetailReport4";
			((XRControl)Detail5).Controls.AddRange((XRControl[])(object)new XRControl[13]
			{
				(XRControl)xrLabel73,
				(XRControl)xrLabel74,
				(XRControl)xrLabel75,
				(XRControl)xrLabel76,
				(XRControl)xrLabel77,
				(XRControl)xrLabel78,
				(XRControl)xrLabel79,
				(XRControl)xrLabel80,
				(XRControl)xrLabel81,
				(XRControl)xrLabel82,
				(XRControl)xrLabel83,
				(XRControl)xrLabel84,
				(XRControl)xrLabel85
			});
			((XRControl)Detail5).Dpi = 254f;
			((XRControl)Detail5).HeightF = 57.24968f;
			((XRControl)Detail5).Name = "Detail5";
			((XRControl)xrLabel73).BackColor = Color.Transparent;
			((XRControl)xrLabel73).Borders = (BorderSide)0;
			((XRControl)xrLabel73).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorDezembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel73).Dpi = 254f;
			((XRControl)xrLabel73).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel73).LocationFloat = new PointFloat(2386.653f, 5f);
			((XRControl)xrLabel73).Name = "xrLabel73";
			((XRControl)xrLabel73).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel73).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel73).StylePriority.UseBackColor = false;
			((XRControl)xrLabel73).StylePriority.UseBorders = false;
			((XRControl)xrLabel73).StylePriority.UseFont = false;
			((XRControl)xrLabel73).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel73).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel74).BackColor = Color.Transparent;
			((XRControl)xrLabel74).Borders = (BorderSide)0;
			((XRControl)xrLabel74).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorNovembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel74).Dpi = 254f;
			((XRControl)xrLabel74).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel74).LocationFloat = new PointFloat(2204.653f, 5f);
			((XRControl)xrLabel74).Name = "xrLabel74";
			((XRControl)xrLabel74).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel74).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel74).StylePriority.UseBackColor = false;
			((XRControl)xrLabel74).StylePriority.UseBorders = false;
			((XRControl)xrLabel74).StylePriority.UseFont = false;
			((XRControl)xrLabel74).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel74).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel75).BackColor = Color.Transparent;
			((XRControl)xrLabel75).Borders = (BorderSide)0;
			((XRControl)xrLabel75).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorOutubro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel75).Dpi = 254f;
			((XRControl)xrLabel75).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel75).LocationFloat = new PointFloat(2022.653f, 5f);
			((XRControl)xrLabel75).Name = "xrLabel75";
			((XRControl)xrLabel75).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel75).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel75).StylePriority.UseBackColor = false;
			((XRControl)xrLabel75).StylePriority.UseBorders = false;
			((XRControl)xrLabel75).StylePriority.UseFont = false;
			((XRControl)xrLabel75).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel75).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel76).BackColor = Color.Transparent;
			((XRControl)xrLabel76).Borders = (BorderSide)0;
			((XRControl)xrLabel76).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorSetembro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel76).Dpi = 254f;
			((XRControl)xrLabel76).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel76).LocationFloat = new PointFloat(1840.653f, 5f);
			((XRControl)xrLabel76).Name = "xrLabel76";
			((XRControl)xrLabel76).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel76).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel76).StylePriority.UseBackColor = false;
			((XRControl)xrLabel76).StylePriority.UseBorders = false;
			((XRControl)xrLabel76).StylePriority.UseFont = false;
			((XRControl)xrLabel76).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel76).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel77).BackColor = Color.Transparent;
			((XRControl)xrLabel77).Borders = (BorderSide)0;
			((XRControl)xrLabel77).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorAgosto.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel77).Dpi = 254f;
			((XRControl)xrLabel77).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel77).LocationFloat = new PointFloat(1658.653f, 5f);
			((XRControl)xrLabel77).Name = "xrLabel77";
			((XRControl)xrLabel77).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel77).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel77).StylePriority.UseBackColor = false;
			((XRControl)xrLabel77).StylePriority.UseBorders = false;
			((XRControl)xrLabel77).StylePriority.UseFont = false;
			((XRControl)xrLabel77).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel77).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel78).BackColor = Color.Transparent;
			((XRControl)xrLabel78).Borders = (BorderSide)0;
			((XRControl)xrLabel78).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJulho.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel78).Dpi = 254f;
			((XRControl)xrLabel78).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel78).LocationFloat = new PointFloat(1476.653f, 5f);
			((XRControl)xrLabel78).Name = "xrLabel78";
			((XRControl)xrLabel78).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel78).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel78).StylePriority.UseBackColor = false;
			((XRControl)xrLabel78).StylePriority.UseBorders = false;
			((XRControl)xrLabel78).StylePriority.UseFont = false;
			((XRControl)xrLabel78).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel78).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel79).BackColor = Color.Transparent;
			((XRControl)xrLabel79).Borders = (BorderSide)0;
			((XRControl)xrLabel79).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJunho.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel79).Dpi = 254f;
			((XRControl)xrLabel79).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel79).LocationFloat = new PointFloat(1294.653f, 5f);
			((XRControl)xrLabel79).Name = "xrLabel79";
			((XRControl)xrLabel79).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel79).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel79).StylePriority.UseBackColor = false;
			((XRControl)xrLabel79).StylePriority.UseBorders = false;
			((XRControl)xrLabel79).StylePriority.UseFont = false;
			((XRControl)xrLabel79).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel79).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel80).BackColor = Color.Transparent;
			((XRControl)xrLabel80).Borders = (BorderSide)0;
			((XRControl)xrLabel80).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorMaio.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel80).Dpi = 254f;
			((XRControl)xrLabel80).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel80).LocationFloat = new PointFloat(1112.653f, 5f);
			((XRControl)xrLabel80).Name = "xrLabel80";
			((XRControl)xrLabel80).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel80).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel80).StylePriority.UseBackColor = false;
			((XRControl)xrLabel80).StylePriority.UseBorders = false;
			((XRControl)xrLabel80).StylePriority.UseFont = false;
			((XRControl)xrLabel80).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel80).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel81).BackColor = Color.Transparent;
			((XRControl)xrLabel81).Borders = (BorderSide)0;
			((XRControl)xrLabel81).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorAbril.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel81).Dpi = 254f;
			((XRControl)xrLabel81).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel81).LocationFloat = new PointFloat(930.6527f, 5f);
			((XRControl)xrLabel81).Name = "xrLabel81";
			((XRControl)xrLabel81).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel81).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel81).StylePriority.UseBackColor = false;
			((XRControl)xrLabel81).StylePriority.UseBorders = false;
			((XRControl)xrLabel81).StylePriority.UseFont = false;
			((XRControl)xrLabel81).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel81).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel82).BackColor = Color.Transparent;
			((XRControl)xrLabel82).Borders = (BorderSide)0;
			((XRControl)xrLabel82).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorMarco.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel82).Dpi = 254f;
			((XRControl)xrLabel82).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel82).LocationFloat = new PointFloat(748.6527f, 5f);
			((XRControl)xrLabel82).Name = "xrLabel82";
			((XRControl)xrLabel82).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel82).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel82).StylePriority.UseBackColor = false;
			((XRControl)xrLabel82).StylePriority.UseBorders = false;
			((XRControl)xrLabel82).StylePriority.UseFont = false;
			((XRControl)xrLabel82).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel82).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel83).BackColor = Color.Transparent;
			((XRControl)xrLabel83).Borders = (BorderSide)0;
			((XRControl)xrLabel83).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorFevereiro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel83).Dpi = 254f;
			((XRControl)xrLabel83).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel83).LocationFloat = new PointFloat(566.6527f, 5f);
			((XRControl)xrLabel83).Name = "xrLabel83";
			((XRControl)xrLabel83).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel83).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel83).StylePriority.UseBackColor = false;
			((XRControl)xrLabel83).StylePriority.UseBorders = false;
			((XRControl)xrLabel83).StylePriority.UseFont = false;
			((XRControl)xrLabel83).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel83).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel84).BackColor = Color.Transparent;
			((XRControl)xrLabel84).Borders = (BorderSide)0;
			((XRControl)xrLabel84).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.ValorJaneiro.ValorReal", "{0:n2}")
			});
			((XRControl)xrLabel84).Dpi = 254f;
			((XRControl)xrLabel84).Font = new Font("Segoe UI", 7.5f);
			((XRControl)xrLabel84).LocationFloat = new PointFloat(384.6528f, 5f);
			((XRControl)xrLabel84).Name = "xrLabel84";
			((XRControl)xrLabel84).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel84).SizeF = new SizeF(182f, 47.84f);
			((XRControl)xrLabel84).StylePriority.UseBackColor = false;
			((XRControl)xrLabel84).StylePriority.UseBorders = false;
			((XRControl)xrLabel84).StylePriority.UseFont = false;
			((XRControl)xrLabel84).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel84).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel85).BackColor = Color.Transparent;
			((XRControl)xrLabel85).Borders = (BorderSide)0;
			((XRControl)xrLabel85).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "SaldosContas.Descricao")
			});
			((XRControl)xrLabel85).Dpi = 254f;
			((XRControl)xrLabel85).Font = new Font("Segoe UI Semibold", 7.5f);
			((XRControl)xrLabel85).LocationFloat = new PointFloat(35.61445f, 4.999964f);
			((XRControl)xrLabel85).Name = "xrLabel85";
			((XRControl)xrLabel85).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel85).SizeF = new SizeF(349.0377f, 47.84f);
			((XRControl)xrLabel85).StylePriority.UseBackColor = false;
			((XRControl)xrLabel85).StylePriority.UseBorders = false;
			((XRControl)xrLabel85).StylePriority.UseFont = false;
			((XRControl)xrLabel85).StylePriority.UsePadding = false;
			((XRControl)xrLabel85).StylePriority.UseTextAlignment = false;
			val3.FormatString = "{0:n2}";
			xrLabel85.Summary = val3;
			((XRControl)xrLabel85).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel85).WordWrap = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel71 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 55.59025f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel71).BackColor = Color.LightGray;
			((XRControl)xrLabel71).Borders = (BorderSide)8;
			((XRControl)xrLabel71).Dpi = 254f;
			((XRControl)xrLabel71).Font = new Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel71).LocationFloat = new PointFloat(0f, 5f);
			((XRControl)xrLabel71).Name = "xrLabel71";
			((XRControl)xrLabel71).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel71).SizeF = new SizeF(2768.653f, 47.84f);
			((XRControl)xrLabel71).StylePriority.UseBackColor = false;
			((XRControl)xrLabel71).StylePriority.UseBorders = false;
			((XRControl)xrLabel71).StylePriority.UseFont = false;
			((XRControl)xrLabel71).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel71).Text = "SALDO DAS CONTAS";
			((XRControl)xrLabel71).TextAlignment = (TextAlignment)16;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)DetailReport3,
				(Band)DetailReport4
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { UltimoNivelSemNegrito, UltimoNivelComNegrito });
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(0, 0, 0, 58);
			((XtraReport)this).PageHeight = 2159;
			((XtraReport)this).PageWidth = 2770;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.DemonstrativoResultado.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.DemonstrativoResultado.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.DemonstrativoResultado.Interfaces
{
	public interface IGridControlPesquisa
	{
		void ExecutarPesquisa();
	}
	public interface IModoFuncionamentoService
	{
		IViewModelDemonstrativoResultado[] ObterViewModels(NavigationParameters parametros);
	}
	public interface IViewModelDemonstrativoResultado
	{
		Formulario Formulario { get; }

		void CriarFormulario();
	}
	public interface IViewModelGraficoDemonstrativoResultado : IViewModelBase
	{
		Formulario Formulario { get; }
	}
}

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
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Financeiros.Balancetes;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.Balancetes.Reports;
using Otus.Modulo.Balancetes.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Balancetes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Balancetes")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Balancetes
{
	[Module(ModuleName = "BalanceteModulo", OnDemand = true)]
	public class BalanceteModulo : IModule
	{
		private readonly IUnityContainer _container;

		public BalanceteModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<BalancetesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<BalanceteFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Balancetes.Views
{
	public class BalanceteFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public BalanceteFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Balancetes;component/views/balancetefichaview.xaml", UriKind.Relative);
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
	public class BalancetesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public BalancetesView()
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
				Uri resourceLocator = new Uri("/Modulo.Balancetes;component/views/balancetesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Balancetes.ViewModels
{
	public class BalanceteAbaViewModel : AbaViewModelBase
	{
		public BalanceteAbaViewModel(IViewModelInformacaoAba viewModel)
			: base(viewModel.Titulo, viewModel.SubTitulo, (IViewModelBase)(object)viewModel)
		{
		}
	}
	public class BalanceteFichaViewModel : FichaBase<BalanceteBaseDto, BalanceteViewModel>
	{
		private readonly ISessao _sessao;

		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ICommand ImprimirCommand { get; set; }

		public BalanceteFichaViewModel(ISessao sessao, IWService service, IImpressaoRelatorioService impressaoRelatorioService)
			: base("Balancetes")
		{
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			_sessao = sessao;
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
		}

		private bool PodeImprimir()
		{
			IFormulario formulario = base.Formulario;
			return ((formulario != null) ? HelperObject.To<Formulario>((object)formulario).ViewModelAbaSelecionada : null) is IViewModelItemImprimirRelatorio;
		}

		private void Imprimir()
		{
			IViewModelBase viewModelAbaSelecionada = HelperObject.To<Formulario>((object)base.Formulario).ViewModelAbaSelecionada;
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)((viewModelAbaSelecionada is IViewModelItemImprimirRelatorio) ? viewModelAbaSelecionada : null));
		}

		protected override void CriarFormulario()
		{
			base.Formulario = (IFormulario)(object)new BalanceteFormulario(_sessao, _service);
		}
	}
	public class BalanceteFormulario : Formulario
	{
		private readonly ISessao _sessao;

		private readonly IWService _service;

		public BalanceteFormulario(ISessao sessao, IWService service)
		{
			_sessao = sessao;
			_service = service;
			CriarAbas();
		}

		private void CriarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				CriarAbaPlanoConta(),
				CriarAbaCentroCusto()
			});
		}

		private IAbaViewModelBase CriarAbaPlanoConta()
		{
			return (IAbaViewModelBase)(object)new BalanceteAbaViewModel((IViewModelInformacaoAba)(object)new BalanceteViewModel(_sessao, (BalanceteBaseDto parametro) => _service.Canal<IBalanceteModuleService, BalanceteDto>((Func<IBalanceteModuleService, BalanceteDto>)((IBalanceteModuleService x) => x.ObterBalancetePlanoContas(parametro))), () => _service.Canal<IContaModuleService, IEnumerable<ContaDto>>((Func<IContaModuleService, IEnumerable<ContaDto>>)((IContaModuleService x) => x.ObterContas())), "Plano de Contas"));
		}

		private IAbaViewModelBase CriarAbaCentroCusto()
		{
			return (IAbaViewModelBase)(object)new BalanceteAbaViewModel((IViewModelInformacaoAba)(object)new BalanceteViewModel(_sessao, (BalanceteBaseDto parametro) => _service.Canal<IBalanceteModuleService, BalanceteDto>((Func<IBalanceteModuleService, BalanceteDto>)((IBalanceteModuleService x) => x.ObterBalanceteCentroCustos(parametro))), () => _service.Canal<IContaModuleService, IEnumerable<ContaDto>>((Func<IContaModuleService, IEnumerable<ContaDto>>)((IContaModuleService x) => x.ObterContas())), "Centro de Custos"));
		}
	}
	public class BalancetesViewModel : ModuloBase
	{
		public BalancetesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloBalancetesRegion, "BalanceteFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class BalanceteViewModel : ViewModelBase<BalanceteBaseDto>, IViewModelInformacaoAba, IViewModelBase, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly ISessao _sessao;

		private readonly Func<BalanceteBaseDto, BalanceteDto> _obterBalancete;

		private readonly Func<IEnumerable<ContaDto>> _obterContas;

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

		public BalanceteDto Balancete
		{
			get
			{
				return ((BindableBase)this).GetProperty<BalanceteDto>((Expression<Func<BalanceteDto>>)(() => Balancete));
			}
			set
			{
				((BindableBase)this).SetProperty<BalanceteDto>((Expression<Func<BalanceteDto>>)(() => Balancete), value);
			}
		}

		public DateTime DataInicial
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial), value);
			}
		}

		public DateTime DataFinal
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinal));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinal), value);
			}
		}

		public bool VisualizarTotais
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => VisualizarTotais));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => VisualizarTotais), value);
			}
		}

		public bool Consolidado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Consolidado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Consolidado), value);
			}
		}

		public bool VisualizarZerados
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => VisualizarZerados));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => VisualizarZerados), value);
			}
		}

		public Visibility PermiteConsolidado
		{
			get
			{
				if (!_sessao.Perfis.ItemSelecionado.Empresa.IsMatriz || _sessao.Perfis.Numero <= 1)
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public ICommand PesquisarCommand { get; set; }

		public string Titulo { get; }

		public string SubTitulo => "Gerar balancete por período";

		public string TituloRelatorio => "Balancete " + Titulo;

		public string SubTituloRelatorio => $"Conta: {Contas.ItemSelecionado.Descricao}. Data inicial: {DataInicial:dd/MM/yyyy} - Data final: {DataFinal:dd/MM/yyyy}";

		public bool Retrato => true;

		public BalanceteViewModel(ISessao sessao, Func<BalanceteBaseDto, BalanceteDto> obterBalancete, Func<IEnumerable<ContaDto>> obterContas, string tituloFicha)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			_sessao = sessao;
			_obterBalancete = obterBalancete;
			_obterContas = obterContas;
			Titulo = tituloFicha;
			ObterContas();
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar, (Func<bool>)PodePesquisar, (bool?)null);
		}

		private void Pesquisar()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Expected O, but got Unknown
			BalanceteBaseDto arg = new BalanceteBaseDto
			{
				Consolidado = Consolidado,
				DataInicial = DataInicial,
				DataFinal = DataFinal,
				IdEmpresa = _sessao.Perfis.ItemSelecionado.Empresa.Id,
				ContaReferencia = ((EntityBaseDto)Contas.ItemSelecionado).Id
			};
			BalanceteDto val = _obterBalancete(arg);
			List<BalanceteItemDto> itens = val.Itens.OrderBy((BalanceteItemDto item) => HelperVersion.GenerateSortKey(item.CodigoUnicoOrdenacao)).ToList();
			val.Itens = itens;
			if (VisualizarZerados)
			{
				Balancete = val;
			}
			else
			{
				Balancete = new BalanceteDto
				{
					Itens = val.Itens.Where((BalanceteItemDto x) => x.Valor != 0m),
					Totais = val.Totais,
					Consolidado = ((BalanceteBaseDto)val).Consolidado,
					ContaReferencia = ((BalanceteBaseDto)val).ContaReferencia,
					DataFinal = ((BalanceteBaseDto)val).DataFinal,
					DataInicial = ((BalanceteBaseDto)val).DataInicial,
					IdEmpresa = ((BalanceteBaseDto)val).IdEmpresa
				};
			}
			VisualizarTotais = Balancete.Totais.Any();
		}

		private bool PodePesquisar()
		{
			return DataInicial <= DataFinal;
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

		public override void ProjectedAsViewModel()
		{
			DataInicial = DateTime.Today;
			DataFinal = DateTime.Today.AddMonths(1);
			VisualizarZerados = true;
		}

		public virtual async Task<XtraReport> ObterRelatorio()
		{
			string arg = (Titulo.Contains("Plano de Contas") ? "centro de custos?" : "plano de contas?");
			bool mostrar = DialogoFactory.MostrarDialogoSimNao($"Mostrar detalhamento por {arg}", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes;
			return await Task.Factory.StartNew(() => Relatorio.Criar().ComDto(Balancete).ComMostrarDetalhamento(mostrar)
				.Build());
		}
	}
}
namespace Otus.Modulo.Balancetes.Reports
{
	public class Relatorio : XtraReport
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ObjectDataSource objectDataSource1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel3;

		private FormattingRule NegritoPai;

		private XRLabel xrLabel1;

		private CalculatedField CodigoDescricao;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLabel xrLabel4;

		private XRLabel xrLabel2;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel5;

		private FormattingRule visualizarTotais;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private XRLabel xrLabel6;

		private XRLabel xrLabel7;

		public bool MostrarDetalhamento { get; set; }

		public Relatorio()
		{
			InitializeComponent();
		}

		public static Relatorio Criar()
		{
			return new Relatorio();
		}

		public Relatorio ComDto(BalanceteDto dto)
		{
			((XtraReportBase)this).DataSource = new List<BalanceteDto> { dto };
			return this;
		}

		public Relatorio ComMostrarDetalhamento(bool mostrarDetalhamento)
		{
			MostrarDetalhamento = mostrarDetalhamento;
			return this;
		}

		public XtraReport Build()
		{
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void DetailReport2_BeforePrint(object sender, PrintEventArgs e)
		{
			e.Cancel = !MostrarDetalhamento;
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Unknown result type (might be due to invalid IL or missing references)
			//IL_032f: Expected O, but got Unknown
			//IL_036a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0393: Unknown result type (might be due to invalid IL or missing references)
			//IL_0477: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c1: Expected O, but got Unknown
			//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0526: Unknown result type (might be due to invalid IL or missing references)
			//IL_069b: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a1: Expected O, but got Unknown
			//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Expected O, but got Unknown
			//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0824: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ed: Expected O, but got Unknown
			//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abe: Expected O, but got Unknown
			//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c11: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			objectDataSource1 = new ObjectDataSource(components);
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel3 = new XRLabel();
			NegritoPai = new FormattingRule();
			xrLabel1 = new XRLabel();
			DetailReport2 = new DetailReportBand();
			Detail3 = new DetailBand();
			xrLabel6 = new XRLabel();
			xrLabel7 = new XRLabel();
			CodigoDescricao = new CalculatedField();
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel4 = new XRLabel();
			xrLabel2 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel5 = new XRLabel();
			visualizarTotais = new FormattingRule();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 0f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			objectDataSource1.DataSource = typeof(BalanceteDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)DetailReport2
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = objectDataSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel3,
				(XRControl)xrLabel1
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 65.60416f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Valor", "{0:n2}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)xrLabel3).FormattingRules).Add(NegritoPai);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1563.833f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(325.5831f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)64;
			NegritoPai.Condition = "[IsPrincipal] == True";
			((DataContainerComponent)NegritoPai).DataMember = "Itens";
			((BrickStyle)NegritoPai.Formatting).BackColor = Color.Gainsboro;
			((BrickStyle)NegritoPai.Formatting).BorderDashStyle = (BorderDashStyle)0;
			((BrickStyle)NegritoPai.Formatting).Font = new Font("Segoe UI Semibold", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((BrickStyle)NegritoPai.Formatting).ForeColor = Color.Black;
			((BrickStyle)NegritoPai.Formatting).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			NegritoPai.Name = "NegritoPai";
			((XRControl)xrLabel1).Borders = (BorderSide)8;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoDescricao")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)xrLabel1).FormattingRules).Add(NegritoPai);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(50, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1563.833f, 58.42f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UsePadding = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XtraReportBase)DetailReport2).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail3 });
			((XtraReportBase)DetailReport2).DataMember = "Itens.Itens";
			((XtraReportBase)DetailReport2).DataSource = objectDataSource1;
			((XRControl)DetailReport2).Dpi = 254f;
			DetailReport2.Level = 0;
			((XRControl)DetailReport2).Name = "DetailReport2";
			((XRControl)DetailReport2).BeforePrint += DetailReport2_BeforePrint;
			((XRControl)Detail3).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel6,
				(XRControl)xrLabel7
			});
			((XRControl)Detail3).Dpi = 254f;
			((XRControl)Detail3).HeightF = 58.42f;
			((XRControl)Detail3).Name = "Detail3";
			((XRControl)xrLabel6).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Itens.CodigoUnicoDescricaoToText")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).ForeColor = Color.DimGray;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(127.0001f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(1436.833f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBackColor = false;
			((XRControl)xrLabel6).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel6).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseForeColor = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Itens.Valor", "{0:n2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).ForeColor = Color.DimGray;
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1563.833f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(325.5831f, 58.42f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseForeColor = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)64;
			((DataContainerComponent)CodigoDescricao).DataMember = "Itens";
			CodigoDescricao.Expression = "Concat([CodigoUnico], '     ', [Descricao])";
			CodigoDescricao.FieldType = (FieldType)1;
			CodigoDescricao.Name = "CodigoDescricao";
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail2,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport1).DataMember = "Totais";
			((XtraReportBase)DetailReport1).DataSource = objectDataSource1;
			((XRControl)DetailReport1).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)DetailReport1).FormattingRules).Add(visualizarTotais);
			DetailReport1.Level = 1;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel4,
				(XRControl)xrLabel2
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).HeightF = 58.42f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Totais.Valor", "{0:n2}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1563.833f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(325.5831f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel2).Borders = (BorderSide)8;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Totais.Tipo")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1563.833f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel5 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 98.10757f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 39.68758f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(1889.416f, 58.41999f);
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Totais";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)2;
			visualizarTotais.Condition = "[Totais].Count()  ==  0";
			visualizarTotais.Formatting.Visible = (DefaultBoolean)1;
			visualizarTotais.Name = "visualizarTotais";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)DetailReport1
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { CodigoDescricao });
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { NegritoPai, visualizarTotais });
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.Balancetes.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Balancetes.Properties.Resources", typeof(Resources).Assembly);
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
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;
	}
}

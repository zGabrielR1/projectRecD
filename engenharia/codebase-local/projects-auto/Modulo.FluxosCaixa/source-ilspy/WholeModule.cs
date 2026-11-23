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
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.FluxosCaixa;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.FluxosCaixa.Reports;
using Otus.Modulo.FluxosCaixa.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.FluxosCaixa")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.FluxosCaixa")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.FluxosCaixa
{
	[Module(ModuleName = "FluxoCaixaModulo", OnDemand = true)]
	public class FluxoCaixaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public FluxoCaixaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<FluxosCaixaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<FluxoCaixaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.FluxosCaixa.Views
{
	public class FluxoCaixaFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public FluxoCaixaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.FluxosCaixa;component/views/fluxocaixafichaview.xaml", UriKind.Relative);
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
	public class FluxosCaixaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public FluxosCaixaView()
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
				Uri resourceLocator = new Uri("/Modulo.FluxosCaixa;component/views/fluxoscaixaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.FluxosCaixa.ViewModels
{
	public class FluxoCaixaFichaViewModel : NavigationBase, IViewModelImprimirGrid, IViewModelImprimir, IViewModelItemImprimirRelatorio
	{
		private readonly IWService _service;

		private readonly ISessao _sessao;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private XtraReport _subReportGridControl;

		private IEnumerable<ContaDto> _saldosConta;

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

		public bool SomenteAtivas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SomenteAtivas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SomenteAtivas), value, (Action)delegate
				{
					AdicionarContas(value);
				});
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

		public bool Mensal
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Mensal));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Mensal), value, (Action)Pesquisar);
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

		public IEnumerable<FluxoCaixaItemDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<FluxoCaixaItemDto>>((Expression<Func<IEnumerable<FluxoCaixaItemDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<FluxoCaixaItemDto>>((Expression<Func<IEnumerable<FluxoCaixaItemDto>>>)(() => Itens), ProcessarItens(value));
			}
		}

		public FluxoCaixaDto FluxoCaixaDto
		{
			get
			{
				return ((BindableBase)this).GetProperty<FluxoCaixaDto>((Expression<Func<FluxoCaixaDto>>)(() => FluxoCaixaDto));
			}
			set
			{
				((BindableBase)this).SetProperty<FluxoCaixaDto>((Expression<Func<FluxoCaixaDto>>)(() => FluxoCaixaDto), value);
			}
		}

		public ICommand PesquisarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public string TituloRelatorio
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(((EntityBaseDto)Contas.ItemSelecionado).Id))
				{
					return $"Fluxo de Caixa - Conta: {Contas.ItemSelecionado.Descricao}";
				}
				return "Fluxo de Caixa";
			}
		}

		public string SubTituloRelatorio => $"Data inicial: {DataInicial:dd/MM/yyyy}  -  Data final: {DataFinal:dd/MM/yyyy}";

		public string TagProperty => "FluxoCaixaGridControl";

		public bool Retrato => true;

		public FluxoCaixaFichaViewModel(IWService service, ISessao sessao, IImpressaoRelatorioService impressaoRelatorioService)
			: base("")
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			SomenteAtivas = true;
			_service = service;
			_sessao = sessao;
			_impressaoRelatorioService = impressaoRelatorioService;
			ObterContas();
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar, (Func<bool>)PodePesquisar, (bool?)null);
			DataInicial = DateTime.Today;
			DataFinal = DateTime.Today.AddMonths(1);
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)Imprimir, (Func<object, bool>)PodeImprimir, (bool?)null);
			Pesquisar();
		}

		private IEnumerable<FluxoCaixaItemDto> ProcessarItens(IEnumerable<FluxoCaixaItemDto> itens)
		{
			FluxoCaixaDto.ItensPendentes = ((!Mensal) ? itens : itens.GroupBy((FluxoCaixaItemDto x) => new
			{
				x.Data.Month,
				x.Data.Year
			}, (key, IEnumerable<FluxoCaixaItemDto> lista) => new FluxoCaixaItemDto
			{
				Pagar = lista.Sum((FluxoCaixaItemDto x) => x.Pagar),
				Receber = lista.Sum((FluxoCaixaItemDto x) => x.Receber),
				SaldoAnterior = lista.First().SaldoAnterior,
				Saldo = lista.Sum((FluxoCaixaItemDto x) => x.Receber) - lista.Sum((FluxoCaixaItemDto x) => x.Pagar),
				SaldoFinal = lista.Last().SaldoFinal,
				Data = lista.Last().Data
			}));
			return FluxoCaixaDto.ItensPendentes;
		}

		private void ObterContas()
		{
			_saldosConta = _service.Canal<IContaModuleService, IEnumerable<ContaDto>>((Func<IContaModuleService, IEnumerable<ContaDto>>)((IContaModuleService x) => x.ObterContasComSaldo(Consolidado)));
			AdicionarContas(SomenteAtivas);
		}

		private void AdicionarContas(bool somenteAtivas)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)_saldosConta))
			{
				IEnumerable<ContaDto> enumerable = (somenteAtivas ? _saldosConta.Where((ContaDto x) => ((EntityDto)x).Ativo) : _saldosConta);
				List<ContaDto> list = new List<ContaDto>
				{
					new ContaDto
					{
						Descricao = "Todas as contas",
						Saldo = enumerable.Sum((ContaDto x) => x.Saldo)
					}
				};
				list.AddRange(enumerable);
				Contas = new Lista<ContaDto>();
				Contas.Adicionar((IEnumerable<ContaDto>)list);
				Contas.PosicionarNoPrimeiro();
				ObservableExtensions.Subscribe<ContaDto>(Contas.WhenAlterouItemSelecionado, (Action<ContaDto>)delegate
				{
					Pesquisar();
				});
			}
		}

		private void Pesquisar()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			FluxoCaixaBaseDto parametro = new FluxoCaixaBaseDto
			{
				Consolidado = Consolidado,
				DataInicial = DataInicial,
				DataFinal = DataFinal,
				ContaId = ((EntityBaseDto)Contas.ItemSelecionado).Id,
				IdEmpresa = _sessao.Perfis.ItemSelecionado.Empresa.Id
			};
			FluxoCaixaDto = _service.Canal<IFluxoCaixaModuleService, FluxoCaixaDto>((Func<IFluxoCaixaModuleService, FluxoCaixaDto>)((IFluxoCaixaModuleService x) => x.Obter(parametro)));
			Itens = FluxoCaixaDto.ItensPendentes;
		}

		protected virtual bool PodePesquisar()
		{
			return DataInicial <= DataFinal;
		}

		private bool PodeImprimir(object arg)
		{
			return FluxoCaixaDto != null;
		}

		private void Imprimir(object obj)
		{
			_subReportGridControl = _impressaoRelatorioService.ObterSubReportGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)this);
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return await Task.Factory.StartNew(() => Relatorio.Criar().ComGrafico(Mensal).ComDto(FluxoCaixaDto)
				.ComSubReport(_subReportGridControl)
				.Build());
		}
	}
	public class FluxosCaixaViewModel : ModuloBase
	{
		public FluxosCaixaViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloFluxosCaixaRegion, "FluxoCaixaFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.FluxosCaixa.Reports
{
	public class Relatorio : XtraReport
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ObjectDataSource objectDataSource1;

		private XRSubreport ReportDetail;

		private XRChart xrChart1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel xrLabel1;

		private XRLabel xrLabel10;

		private FormattingRule mostrarValorChequeEmitido;

		private XRLabel xrLabel9;

		private FormattingRule mostrarValorChequeRecebido;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		public Relatorio()
		{
			InitializeComponent();
		}

		public static Relatorio Criar()
		{
			return new Relatorio();
		}

		public Relatorio ComDto(FluxoCaixaDto dto)
		{
			((XtraReportBase)this).DataSource = new List<FluxoCaixaDto> { dto };
			return this;
		}

		public Relatorio ComSubReport(XtraReport subreport)
		{
			((SubreportBase)ReportDetail).ReportSource = subreport;
			return this;
		}

		public Relatorio ComGrafico(bool mensal)
		{
			((XRControl)xrChart1).Visible = mensal;
			((XRControl)ReportDetail).Location = (mensal ? new System.Drawing.Point(50, 980) : new System.Drawing.Point(50, 0));
			((XRControl)ReportDetail).Size = (mensal ? new System.Drawing.Size(2000, 50) : new System.Drawing.Size(2000, 1030));
			return this;
		}

		public XtraReport Build()
		{
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
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
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Expected O, but got Unknown
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Expected O, but got Unknown
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Expected O, but got Unknown
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Expected O, but got Unknown
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Expected O, but got Unknown
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Expected O, but got Unknown
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Expected O, but got Unknown
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Expected O, but got Unknown
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Expected O, but got Unknown
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0203: Unknown result type (might be due to invalid IL or missing references)
			//IL_034c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0496: Unknown result type (might be due to invalid IL or missing references)
			//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0650: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Unknown result type (might be due to invalid IL or missing references)
			//IL_0769: Unknown result type (might be due to invalid IL or missing references)
			//IL_079e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0854: Unknown result type (might be due to invalid IL or missing references)
			//IL_085a: Expected O, but got Unknown
			//IL_0895: Unknown result type (might be due to invalid IL or missing references)
			//IL_08be: Unknown result type (might be due to invalid IL or missing references)
			//IL_0940: Unknown result type (might be due to invalid IL or missing references)
			//IL_0946: Expected O, but got Unknown
			//IL_0981: Unknown result type (might be due to invalid IL or missing references)
			//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c24: Expected O, but got Unknown
			//IL_0c49: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0a: Expected O, but got Unknown
			//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e01: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XYDiagram val = new XYDiagram();
			Series val2 = new Series();
			SideBySideBarSeriesLabel val3 = new SideBySideBarSeriesLabel();
			Series val4 = new Series();
			SideBySideBarSeriesLabel val5 = new SideBySideBarSeriesLabel();
			Series val6 = new Series();
			PointSeriesLabel val7 = new PointSeriesLabel();
			SplineSeriesView val8 = new SplineSeriesView();
			Detail = new DetailBand();
			ReportDetail = new XRSubreport();
			xrChart1 = new XRChart();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			objectDataSource1 = new ObjectDataSource(components);
			GroupHeader1 = new GroupHeaderBand();
			xrLabel10 = new XRLabel();
			mostrarValorChequeEmitido = new FormattingRule();
			xrLabel9 = new XRLabel();
			mostrarValorChequeRecebido = new FormattingRule();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel1 = new XRLabel();
			((ISupportInitialize)xrChart1).BeginInit();
			((ISupportInitialize)val).BeginInit();
			((ISupportInitialize)val2).BeginInit();
			((ISupportInitialize)val3).BeginInit();
			((ISupportInitialize)val4).BeginInit();
			((ISupportInitialize)val5).BeginInit();
			((ISupportInitialize)val6).BeginInit();
			((ISupportInitialize)val7).BeginInit();
			((ISupportInitialize)val8).BeginInit();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)ReportDetail,
				(XRControl)xrChart1
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 1050f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)ReportDetail).Dpi = 254f;
			((XRControl)ReportDetail).LocationFloat = new PointFloat(50f, 980f);
			((XRControl)ReportDetail).Name = "ReportDetail";
			((XRControl)ReportDetail).SizeF = new SizeF(2000f, 50f);
			((XRControl)xrChart1).BorderColor = Color.Black;
			((XRControl)xrChart1).Borders = (BorderSide)0;
			((ScaleOptionsBase)((AxisBase)val.AxisX).DateTimeScaleOptions).AggregateFunction = (AggregateFunction)0;
			((ScaleOptionsBase)((AxisBase)val.AxisX).DateTimeScaleOptions).AutoGrid = false;
			((AxisBase)val.AxisX).DateTimeScaleOptions.GridAlignment = (DateTimeGridAlignment)6;
			((AxisBase)val.AxisX).DateTimeScaleOptions.MeasureUnit = (DateTimeMeasureUnit)6;
			((Axis2D)val.AxisX).Label.TextPattern = "{A:MM/yyyy}";
			((AxisBase)val.AxisX).MinorCount = 1;
			((Axis2D)val.AxisX).VisibleInPanesSerializable = "-1";
			((Axis2D)val.AxisY).Label.TextPattern = "{V:c2}";
			((Axis2D)val.AxisY).VisibleInPanesSerializable = "-1";
			xrChart1.Diagram = (Diagram)(object)val;
			((XRControl)xrChart1).Dpi = 254f;
			xrChart1.Legend.AlignmentHorizontal = (LegendAlignmentHorizontal)3;
			xrChart1.Legend.AlignmentVertical = (LegendAlignmentVertical)4;
			xrChart1.Legend.Direction = (LegendDirection)2;
			((XRControl)xrChart1).LocationFloat = new PointFloat(74.08334f, 0f);
			((XRControl)xrChart1).Name = "xrChart1";
			((SeriesBase)val2).ArgumentDataMember = "Itens.Data";
			((BarSeriesLabel)val3).Position = (BarSeriesLabelPosition)1;
			((SeriesLabelBase)val3).TextPattern = "{V:n2}";
			((SeriesBase)val2).Label = (SeriesLabelBase)(object)val3;
			val2.Name = "Recebimentos";
			((SeriesBase)val2).ValueDataMembersSerializable = "Itens.Receber";
			((SeriesBase)val4).ArgumentDataMember = "Itens.Data";
			((BarSeriesLabel)val5).Position = (BarSeriesLabelPosition)1;
			((SeriesLabelBase)val5).TextPattern = "{V:n2}";
			((SeriesBase)val4).Label = (SeriesLabelBase)(object)val5;
			val4.Name = "Pagamentos";
			((SeriesBase)val4).ValueDataMembersSerializable = "Itens.Pagar";
			((SeriesBase)val6).ArgumentDataMember = "Itens.Data";
			((SeriesLabelBase)val7).TextPattern = "{V:n2}";
			((SeriesBase)val6).Label = (SeriesLabelBase)(object)val7;
			val6.Name = "Saldo";
			((SeriesBase)val6).ValueDataMembersSerializable = "Itens.SaldoFinal";
			((SeriesBase)val6).View = (SeriesViewBase)(object)val8;
			xrChart1.SeriesSerializable = (Series[])(object)new Series[3] { val2, val4, val6 };
			((XRControl)xrChart1).SizeF = new SizeF(1946.542f, 957.7917f);
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
			objectDataSource1.DataSource = typeof(FluxoCaixaDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[10]
			{
				(XRControl)xrLabel10,
				(XRControl)xrLabel9,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel6,
				(XRControl)xrLabel5,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)xrLabel1
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupHeader1).HeightF = 246.9091f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel10).Borders = (BorderSide)8;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Italic);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel10).FormattingRules).Add(mostrarValorChequeEmitido);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1076.583f, 184.385f);
			xrLabel10.Multiline = true;
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(998.4169f, 39.89914f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).Text = "Soma dos valores de cheques (com parcelas vinculadas) ao total a pagar.\r\n";
			mostrarValorChequeEmitido.Condition = "[ValorChequeEmitidoPreDatado] == 0";
			mostrarValorChequeEmitido.Formatting.Visible = (DefaultBoolean)1;
			mostrarValorChequeEmitido.Name = "mostrarValorChequeEmitido";
			((XRControl)xrLabel9).Borders = (BorderSide)8;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Italic);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel9).FormattingRules).Add(mostrarValorChequeRecebido);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(1076.584f, 83.42004f);
			xrLabel9.Multiline = true;
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(998.4166f, 42.54496f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Text = "Soma dos valores de cheques (com parcelas vinculadas) ao total a receber.\r\n";
			mostrarValorChequeRecebido.Condition = "[ValorChequeRecebidoPreDatado] == 0";
			mostrarValorChequeRecebido.Formatting.Visible = (DefaultBoolean)1;
			mostrarValorChequeRecebido.Name = "mostrarValorChequeRecebido";
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorChequeRecebidoPreDatado", "{0:c2}")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)xrLabel8).FormattingRules).Add(mostrarValorChequeRecebido);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1669.52f, 25.00001f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(405.4796f, 58.41999f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorChequeEmitidoPreDatado", "{0:c2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)xrLabel7).FormattingRules).Add(mostrarValorChequeEmitido);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1669.52f, 125.965f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(405.4796f, 58.41998f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel6).FormattingRules).Add(mostrarValorChequeRecebido);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1076.583f, 25.00001f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(592.9373f, 58.41999f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "Valor cheque recebido pré-datado:";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel5).FormattingRules).Add(mostrarValorChequeEmitido);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1076.583f, 125.965f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(592.937f, 58.41998f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Valor cheque emitido pré-datado:";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorAtrasoReceber", "{0:c2}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(468.0416f, 67.54493f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(529.1664f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorAtrasoPagar", "{0:c2}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(468.0416f, 125.9649f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(529.1664f, 58.41999f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "xrLabel3";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel2).Borders = (BorderSide)8;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(50f, 125.9649f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(418.0416f, 58.42001f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Valor em atraso pagar:";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).Borders = (BorderSide)8;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(50f, 67.54493f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(418.0416f, 58.41999f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Valor em atraso receber:";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { mostrarValorChequeEmitido, mostrarValorChequeRecebido });
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)val).EndInit();
			((ISupportInitialize)val3).EndInit();
			((ISupportInitialize)val2).EndInit();
			((ISupportInitialize)val5).EndInit();
			((ISupportInitialize)val4).EndInit();
			((ISupportInitialize)val7).EndInit();
			((ISupportInitialize)val8).EndInit();
			((ISupportInitialize)val6).EndInit();
			((ISupportInitialize)xrChart1).EndInit();
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.FluxosCaixa.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.FluxosCaixa.Properties.Resources", typeof(Resources).Assembly);
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

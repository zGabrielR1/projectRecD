using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Auxiliares.GestaoSincronizacaoClubeConstrudata;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GestaoSincronizacaoClubeConstrudata.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Modulo.GestaoSincronizacaoClubeConstrudata")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Modulo.GestaoSincronizacaoClubeConstrudata")]
[assembly: AssemblyCopyright("Copyright Digisat")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoSincronizacaoClubeConstrudata
{
	[Module(ModuleName = "GestaoSincronizacaoClubeConstrudataModulo", OnDemand = true)]
	public class GestaoSincronizacaoClubeConstrudataModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoSincronizacaoClubeConstrudataModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacoesClubeConstrudataView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacaoClubeConstrudataFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoSincronizacaoClubeConstrudata.Views
{
	public class GestaoSincronizacaoClubeConstrudataFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacaoClubeConstrudataFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacaoClubeConstrudata;component/views/gestaosincronizacaoclubeconstrudatafichaview.xaml", UriKind.Relative);
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
	public class GestaoSincronizacoesClubeConstrudataView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacoesClubeConstrudataView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacaoClubeConstrudata;component/views/gestaosincronizacoesclubeconstrudataview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacaoClubeConstrudata.Views.GestaoSincronizacaoClubeConstrudataFichaViewDetalhes
{
	public class SincronizacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SincronizacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacaoClubeConstrudata;component/views/gestaosincronizacaoclubeconstrudatafichaviewdetalhes/sincronizacaoview.xaml", UriKind.Relative);
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
	public class MovimentacoesRetroativasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MovimentacoesRetroativasView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacaoClubeConstrudata;component/views/gestaosincronizacaoclubeconstrudatafichaviewdetalhes/movimentacoesretroativasview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacaoClubeConstrudata.ViewModels
{
	public class GestaoSincronizacaoClubeConstrudataFichaViewModel : FichaBase<VendaSincronizacaoClubeConstrudataDto, SincronizacaoViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<VendaSincronizacaoClubeConstrudataDto>> _objetos;

		private Task<int> _retroativas;

		public ICommand ForcarSincronismoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ForcarSincronismoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ForcarSincronismoCommand), value);
			}
		}

		public GestaoSincronizacaoClubeConstrudataFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
			: base(string.Empty)
		{
			_service = service;
			_executarProcessoService = executarProcessoService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return;
			}
			try
			{
				GestaoSincronizacaoClubeConstrudataFormulario gestaoSincronizacaoClubeConstrudataFormulario = new GestaoSincronizacaoClubeConstrudataFormulario(ObterObjetos, Reenfileirar, GerarRetroativas);
				ForcarSincronismoCommand = (ICommand)new DelegateCommand<object>((Action<object>)gestaoSincronizacaoClubeConstrudataFormulario.Reenfileirar, (Func<object, bool>)gestaoSincronizacaoClubeConstrudataFormulario.PodeReenfileirar, (bool?)null);
				base.Formulario = (IFormulario)(object)gestaoSincronizacaoClubeConstrudataFormulario;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
		}

		public void ObterObjetos(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoClubeConstrudataModuleService, Task<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>>((Func<IGestaoSincronizacaoClubeConstrudataModuleService, Task<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>>)((IGestaoSincronizacaoClubeConstrudataModuleService service) => _objetos = service.PesquisarPorData(dataInicial, dataFinal)));
			}, (Action<object>)RetornoObjetos);
		}

		private void RetornoObjetos(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_objetos))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoSincronizacaoClubeConstrudataFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<SincronizacaoViewModel>((object)aba.ViewModel, (Action<SincronizacaoViewModel>)delegate(SincronizacaoViewModel x)
					{
						x.SetarResultado(_objetos.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void Reenfileirar(IEnumerable<VendaSincronizacaoClubeConstrudataDto> objetos)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoClubeConstrudataModuleService>((Action<IGestaoSincronizacaoClubeConstrudataModuleService>)delegate(IGestaoSincronizacaoClubeConstrudataModuleService service)
				{
					service.Reenfileirar(objetos);
				});
			}, (Action<object>)delegate
			{
				DialogoFactory.MostrarDialogoConfirma("Os registros serão reenviados em breve!", false, HorizontalAlignment.Center);
			});
		}

		public void GerarRetroativas(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoClubeConstrudataModuleService, Task<int>>((Func<IGestaoSincronizacaoClubeConstrudataModuleService, Task<int>>)((IGestaoSincronizacaoClubeConstrudataModuleService service) => _retroativas = service.GerarRetroativas(dataInicial, dataFinal)));
			}, (Action<object>)RetornoGerarRetroativas);
		}

		private void RetornoGerarRetroativas(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (HelperObject.IsNotNull((object)_retroativas))
				{
					int result = _retroativas.Result;
					DialogoFactory.MostrarDialogoConfirma((result > 0) ? $"{result} movimentação(ões) adicionada(s) e serão enviadas em breve!" : "Nenhuma movimentação nova encontrada no período informado.", false, HorizontalAlignment.Center);
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}
	}
	public class GestaoSincronizacaoClubeConstrudataFormulario : Formulario
	{
		private readonly Action<IEnumerable<VendaSincronizacaoClubeConstrudataDto>> _reenfileirar;

		private SincronizacaoViewModel _sincronizacaoViewModel;

		private SincronizacaoAbaViewModel _sincronizacaoAbaViewModel;

		private MovimentacoesRetroativasViewModel _movimentacoesRetroativasViewModel;

		private MovimentacoesRetroativasAbaViewModel _movimentacoesRetroativasAbaViewModel;

		public GestaoSincronizacaoClubeConstrudataFormulario(Action<DateTime, DateTime> obterObjetos, Action<IEnumerable<VendaSincronizacaoClubeConstrudataDto>> reenfileirar, Action<DateTime, DateTime> gerarRetroativas)
		{
			GestaoSincronizacaoClubeConstrudataFormulario gestaoSincronizacaoClubeConstrudataFormulario = this;
			_reenfileirar = reenfileirar;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoSincronizacaoClubeConstrudataFormulario.ProjetarAbas(obterObjetos, gerarRetroativas);
			});
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		public bool PodeReenfileirar(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public void Reenfileirar(object obj)
		{
			List<VendaSincronizacaoClubeConstrudataDto> list = HelperIEnumerable.ToListEmptyWhenNull<VendaSincronizacaoClubeConstrudataDto>(_sincronizacaoViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum registro selecionado para sincronizar", false, HorizontalAlignment.Center);
			}
			else
			{
				_reenfileirar(list);
			}
		}

		private void ProjetarAbas(Action<DateTime, DateTime> obterObjetos, Action<DateTime, DateTime> gerarRetroativas)
		{
			_sincronizacaoViewModel = new SincronizacaoViewModel(obterObjetos);
			_sincronizacaoAbaViewModel = new SincronizacaoAbaViewModel((IViewModelDtoBase)(object)_sincronizacaoViewModel);
			_movimentacoesRetroativasViewModel = new MovimentacoesRetroativasViewModel(gerarRetroativas);
			_movimentacoesRetroativasAbaViewModel = new MovimentacoesRetroativasAbaViewModel((IViewModelDtoBase)(object)_movimentacoesRetroativasViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)_sincronizacaoAbaViewModel,
				(IAbaViewModelBase)_movimentacoesRetroativasAbaViewModel
			});
		}
	}
	public class GestaoSincronizacoesClubeConstrudataViewModel : ModuloBase
	{
		public GestaoSincronizacoesClubeConstrudataViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoSincronizacoesClubeConstrudataRegion, "GestaoSincronizacaoClubeConstrudataFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class MovimentacoesRetroativasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public MovimentacoesRetroativasAbaViewModel(IViewModelDtoBase viewModel)
			: base("RETROATIVAS", "Gerar registros retroativos", viewModel)
		{
		}
	}
	public class MovimentacoesRetroativasViewModel : ViewModelDtoBase<VendaSincronizacaoClubeConstrudataDto>
	{
		private readonly Action<DateTime, DateTime> _gerarRetroativas;

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

		public ICommand GerarCommand { get; set; }

		public MovimentacoesRetroativasViewModel()
		{
		}

		public MovimentacoesRetroativasViewModel(Action<DateTime, DateTime> gerarRetroativas)
			: base((VendaSincronizacaoClubeConstrudataDto)null)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			_gerarRetroativas = gerarRetroativas;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = DateTime.Now.Date;
			GerarCommand = (ICommand)new DelegateCommand((Action)Gerar);
		}

		private void Gerar()
		{
			if (DataFinal.Date < DataInicial.Date)
			{
				DialogoFactory.MostrarDialogoConfirma("A data final não pode ser menor que a data inicial.", false, HorizontalAlignment.Center);
			}
			else if (DataInicial.Date.AddMonths(1) < DataFinal.Date)
			{
				DialogoFactory.MostrarDialogoConfirma("O período informado não pode ser maior que 1 mês.", false, HorizontalAlignment.Center);
			}
			else
			{
				_gerarRetroativas(DataInicial, DataFinal);
			}
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class SincronizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SincronizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SINCRONIZAÇÃO", "Resumo das sincronizações", viewModel)
		{
		}
	}
	public class SincronizacaoViewModel : ViewModelDtoBase<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>
	{
		private readonly Action<DateTime, DateTime> _obterObjetos;

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

		public IEnumerable<VendaSincronizacaoClubeConstrudataDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>((Expression<Func<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>((Expression<Func<IEnumerable<VendaSincronizacaoClubeConstrudataDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoObjetos";

		public ICommand PesquisarCommand { get; set; }

		public SincronizacaoViewModel()
		{
		}

		public SincronizacaoViewModel(Action<DateTime, DateTime> obterObjetos)
			: base((IEnumerable<VendaSincronizacaoClubeConstrudataDto>)null)
		{
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			_obterObjetos = obterObjetos;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
		}

		private void Pesquisar()
		{
			_obterObjetos(DataInicial, DataFinal);
		}

		public IEnumerable<VendaSincronizacaoClubeConstrudataDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<VendaSincronizacaoClubeConstrudataDto>();
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(NomeGridControl);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			if (HelperObject.IsNull((object)val.Property))
			{
				throw new Exception(NomeGridControl + " nao encontrado");
			}
			return val.Property;
		}

		public void SetarResultado(IEnumerable<VendaSincronizacaoClubeConstrudataDto> objetos)
		{
			Itens = objetos;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}

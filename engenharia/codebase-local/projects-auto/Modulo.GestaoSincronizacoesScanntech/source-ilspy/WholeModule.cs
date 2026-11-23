using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
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
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Auxiliares.GestaoSincronizacaoScanntech;
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
using Otus.Modulo.GestaoSincronizacoesScanntech.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoSincronizacoesScanntech")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoSincronizacoesScanntech")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoSincronizacoesScanntech
{
	[Module(ModuleName = "GestaoSincronizacaoScanntechModulo", OnDemand = true)]
	public class GestaoSincronizacaoScanntechModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoSincronizacaoScanntechModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacoesScanntechView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacaoScanntechFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoSincronizacoesScanntech.Views
{
	public class GestaoSincronizacoesScanntechView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacoesScanntechView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesScanntech;component/views/gestaosincronizacoesscanntechview.xaml", UriKind.Relative);
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
	public class GestaoSincronizacaoScanntechFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacaoScanntechFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesScanntech;component/views/gestaosincronizacaoscanntechfichaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacoesScanntech.Views.GestaoSincronizacaoScanntechFichaViewDetalhes
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesScanntech;component/views/gestaosincronizacaoscanntechfichaviewdetalhes/sincronizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacoesScanntech.ViewModels
{
	public class GestaoSincronizacoesScanntechViewModel : ModuloBase
	{
		public GestaoSincronizacoesScanntechViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoSincronizacoesScanntechRegion, "GestaoSincronizacaoScanntechFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class GestaoSincronizacaoScanntechFichaViewModel : FichaBase<ObjetoSincronizacaoScanntechDto, SincronizacaoViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<ObjetoSincronizacaoScanntechDto>> _objetos;

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

		public ICommand SetarSincronizadoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => SetarSincronizadoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => SetarSincronizadoCommand), value);
			}
		}

		public GestaoSincronizacaoScanntechFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
			: base(string.Empty)
		{
			_service = service;
			_executarProcessoService = executarProcessoService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return;
			}
			try
			{
				GestaoSincronizacaoScanntechFormulario gestaoSincronizacaoScanntechFormulario = new GestaoSincronizacaoScanntechFormulario(ObterObjetos, SetarNaoSincronizado, SetarSincronizado);
				ForcarSincronismoCommand = (ICommand)new DelegateCommand<object>((Action<object>)gestaoSincronizacaoScanntechFormulario.ForcarSincronismo, (Func<object, bool>)gestaoSincronizacaoScanntechFormulario.PodeForcarSincronismo, (bool?)null);
				SetarSincronizadoCommand = (ICommand)new DelegateCommand((Action)gestaoSincronizacaoScanntechFormulario.SetarSincronizado, gestaoSincronizacaoScanntechFormulario.PodeSetarSincronizado());
				base.Formulario = (IFormulario)(object)gestaoSincronizacaoScanntechFormulario;
				string text = navigationParameters["Identificador"] as string;
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				string[] referencias = text.Split('|');
				_executarProcessoService.AdicionarProcesso((Action)delegate
				{
					_service.Canal<IGestaoSincronizacaoScanntechModuleService, Task<IEnumerable<ObjetoSincronizacaoScanntechDto>>>((Func<IGestaoSincronizacaoScanntechModuleService, Task<IEnumerable<ObjetoSincronizacaoScanntechDto>>>)((IGestaoSincronizacaoScanntechModuleService service) => _objetos = service.PesquisarPorReferencias((IEnumerable<string>)referencias)));
				}, (Action<object>)RetornoObjetos);
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
				_service.Canal<IGestaoSincronizacaoScanntechModuleService, Task<IEnumerable<ObjetoSincronizacaoScanntechDto>>>((Func<IGestaoSincronizacaoScanntechModuleService, Task<IEnumerable<ObjetoSincronizacaoScanntechDto>>>)((IGestaoSincronizacaoScanntechModuleService service) => _objetos = service.PesquisarPorDataInclusao(dataInicial, dataFinal)));
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
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoSincronizacaoScanntechFormulario>((object)base.Formulario)).Abas)
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

		public void SetarNaoSincronizado(IEnumerable<ObjetoSincronizacaoScanntechDto> objetos)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoScanntechModuleService>((Action<IGestaoSincronizacaoScanntechModuleService>)delegate(IGestaoSincronizacaoScanntechModuleService service)
				{
					service.SetarNaoSincronizado(objetos);
				});
			}, (Action<object>)delegate
			{
				DialogoFactory.MostrarDialogoConfirma("Os registros serão sincronizados novamente em breve!", false, HorizontalAlignment.Center);
			});
		}

		public void SetarSincronizado()
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoScanntechModuleService>((Action<IGestaoSincronizacaoScanntechModuleService>)delegate(IGestaoSincronizacaoScanntechModuleService service)
				{
					service.SetarSincronizado();
				});
			}, (Action<object>)delegate
			{
				DialogoFactory.MostrarDialogoConfirma("A situação de todos os registros foi alterada para sincronizado!", false, HorizontalAlignment.Center);
			});
		}
	}
	public class GestaoSincronizacaoScanntechFormulario : Formulario
	{
		private readonly Action<IEnumerable<ObjetoSincronizacaoScanntechDto>> _setarNaoSincronizado;

		private readonly Action _setarSincronizado;

		private SincronizacaoViewModel _sincronizacaoViewModel;

		private SincronizacaoAbaViewModel _sincronizacaoAbaViewModel;

		public GestaoSincronizacaoScanntechFormulario(Action<DateTime, DateTime> obterObjetos, Action<IEnumerable<ObjetoSincronizacaoScanntechDto>> setarNaoSincronizado, Action setarSincronizado)
		{
			GestaoSincronizacaoScanntechFormulario gestaoSincronizacaoScanntechFormulario = this;
			_setarNaoSincronizado = setarNaoSincronizado;
			_setarSincronizado = setarSincronizado;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoSincronizacaoScanntechFormulario.ProjetarAbas(obterObjetos);
			});
		}

		public bool PodeForcarSincronismo(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public void ForcarSincronismo(object obj)
		{
			List<ObjetoSincronizacaoScanntechDto> list = HelperIEnumerable.ToListEmptyWhenNull<ObjetoSincronizacaoScanntechDto>(_sincronizacaoViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum registro selecionado para sincronizar", false, HorizontalAlignment.Center);
			}
			else
			{
				_setarNaoSincronizado(list);
			}
		}

		public bool PodeSetarSincronizado()
		{
			return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public void SetarSincronizado()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Alterar situação de todos os registros para sincronizado?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				_setarSincronizado();
			}
		}

		private void ProjetarAbas(Action<DateTime, DateTime> obterObjetos)
		{
			_sincronizacaoViewModel = new SincronizacaoViewModel(obterObjetos);
			_sincronizacaoAbaViewModel = new SincronizacaoAbaViewModel((IViewModelDtoBase)(object)_sincronizacaoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)_sincronizacaoAbaViewModel });
		}
	}
	public class SincronizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SincronizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SINCRONIZAÇÃO", "Resumo das sincronizações", viewModel)
		{
		}
	}
	public class SincronizacaoViewModel : ViewModelDtoBase<IEnumerable<ObjetoSincronizacaoScanntechDto>>
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

		public IEnumerable<ObjetoSincronizacaoScanntechDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ObjetoSincronizacaoScanntechDto>>((Expression<Func<IEnumerable<ObjetoSincronizacaoScanntechDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ObjetoSincronizacaoScanntechDto>>((Expression<Func<IEnumerable<ObjetoSincronizacaoScanntechDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoObjetos";

		public ICommand PesquisarCommand { get; set; }

		public SincronizacaoViewModel()
		{
		}

		public SincronizacaoViewModel(Action<DateTime, DateTime> obterObjetos)
			: base((IEnumerable<ObjetoSincronizacaoScanntechDto>)null)
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

		public IEnumerable<ObjetoSincronizacaoScanntechDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ObjetoSincronizacaoScanntechDto>();
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(NomeGridControl);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			if (HelperObject.IsNull((object)val.Property))
			{
				throw new Exception($"{NomeGridControl} não encontrado");
			}
			return val.Property;
		}

		public void SetarResultado(IEnumerable<ObjetoSincronizacaoScanntechDto> objetos)
		{
			Itens = objetos;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.GestaoSincronizacoesScanntech.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoSincronizacoesScanntech.Properties.Resources", typeof(Resources).Assembly);
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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Usuarios;
using DigisatServer.Infrastructure.Crosscutting;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.KeyGesture;
using DigisatServer.Infrastructure.Crosscutting.Logging;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.Actor;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Actors;
using Otus.Infraestrutura.Actors.Interfaces;
using Otus.Infraestrutura.Events;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Cores;
using Otus.Infraestrutura.Models.Icones;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.Models.Tiles;
using Otus.Infraestrutura.Services;
using Otus.Modulo.Menus.Models.Icones;
using Otus.Modulo.Menus.Models.Menus.BarraTarefa;
using Otus.Modulo.Menus.Models.Menus.Classificacao;
using Otus.Modulo.Menus.Models.Menus.Grupo;
using Otus.Modulo.Menus.Models.Menus.Lateral;
using Otus.Modulo.Menus.Models.Menus.Principal;
using Otus.Modulo.Menus.Services;
using Otus.Modulo.Menus.Views;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Menus")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Menus")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Menus
{
	[Module(ModuleName = "MenuModulo")]
	public class MenuModulo : IModule
	{
		private readonly IUnityContainer _container;

		public MenuModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Expected O, but got Unknown
			//IL_015c: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Expected O, but got Unknown
			UnityExtensions.RegisterTypeForNavigation<BarraTituloMinimizarFecharView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<BarraTituloView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MenuLateralView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MenuPrincipalView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<BarraTarefaView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<INavegacaoPreAutenticacaoService, MenuNavegacaoService>(_container, $"p{typeof(MenuNavegacaoService).Name}", Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoInicialService, MenuNavegacaoService>(_container, typeof(MenuNavegacaoService).Name, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoBloqueioService, MenuNavegacaoService>(_container, $"b{typeof(MenuNavegacaoService).Name}", Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoAlterarSenhaService, MenuNavegacaoService>(_container, $"b{typeof(MenuNavegacaoService).Name}", Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoConfiguracaoService, MenuNavegacaoService>(_container, $"n{typeof(MenuNavegacaoService).Name}", Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<ITilesService, TilesService>(_container, (LifetimeManager)new ContainerControlledLifetimeManager(), Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<IMensagemClientActorSystem>(_container, (LifetimeManager)new ContainerControlledLifetimeManager(), (InjectionMember[])(object)new InjectionMember[1] { (InjectionMember)new InjectionFactory((Func<IUnityContainer, object>)((IUnityContainer container) => ((DActorSystemBase<IUnityContainer>)(object)UnityContainerExtensions.Resolve<MensagemClientActorSystem>(container, Array.Empty<ResolverOverride>())).Criar("MensagemClient"))) });
			UnityContainerExtensions.Resolve<IMensagemClientActorSystem>(_container, Array.Empty<ResolverOverride>()).Iniciar();
			UnityContainerExtensions.RegisterType<IEnvioBoletoPendenteService, EnvioBoletoPendenteService>(_container, (LifetimeManager)new ContainerControlledLifetimeManager(), Array.Empty<InjectionMember>());
		}
	}
}
namespace Otus.Modulo.Menus.Views
{
	public class BarraTarefaView : UserControl, IComponentConnector
	{
		internal ItemsControl items;

		private bool _contentLoaded;

		public BarraTarefaView()
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
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/barratarefaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				items = (ItemsControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class BarraTituloMinimizarFecharView : UserControl, IComponentConnector
	{
		internal Button btnMinimizarSistema;

		internal Button btnFecharSistema;

		private bool _contentLoaded;

		public BarraTituloMinimizarFecharView()
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
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/barratitulominimizarfecharview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				btnMinimizarSistema = (Button)target;
				break;
			case 2:
				btnFecharSistema = (Button)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class BarraTituloView : UserControl, IComponentConnector
	{
		internal TextBlock txtDescricaoMei;

		internal Button ButMostrarInformacao;

		internal Button btnAbrirAjuda;

		internal Button btnTrocarUsuario;

		internal Button btnBloquearSistema;

		internal Button btnAlterarSenhaSistema;

		internal Button btnMinimizarSistema;

		internal Button btnFecharSistema;

		private bool _contentLoaded;

		public BarraTituloView()
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
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/barratituloview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				txtDescricaoMei = (TextBlock)target;
				break;
			case 2:
				ButMostrarInformacao = (Button)target;
				break;
			case 3:
				btnAbrirAjuda = (Button)target;
				break;
			case 4:
				btnTrocarUsuario = (Button)target;
				break;
			case 5:
				btnBloquearSistema = (Button)target;
				break;
			case 6:
				btnAlterarSenhaSistema = (Button)target;
				break;
			case 7:
				btnMinimizarSistema = (Button)target;
				break;
			case 8:
				btnFecharSistema = (Button)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class MenuLateralView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MenuLateralView()
		{
			InitializeComponent();
		}

		private void MenuItem_OnChecked(object sender, RoutedEventArgs e)
		{
			MenuItem obj = sender as MenuItem;
			obj.IsChecked = !obj.IsChecked;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/menulateralview.xaml", UriKind.Relative);
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
	public class MenuPrincipalView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MenuPrincipalView()
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
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/menuprincipalview.xaml", UriKind.Relative);
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
	public class UsuarioView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public UsuarioView()
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
				Uri resourceLocator = new Uri("/Modulo.Menus;component/views/usuarioview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Menus.ViewModels
{
	public class BarraTarefaViewModel : NavigationBase
	{
		private readonly IRegionManager _regionManager;

		private readonly IInteracaoBarraTarrefaService _interacaoBarraTarrefaService;

		public ObservableCollection<IItemBarraTarefa> Itens { get; protected set; } = new ObservableCollection<IItemBarraTarefa>();


		public BarraTarefaViewModel(IRegionManager regionManager, IInteracaoBarraTarrefaService interacaoBarraTarrefaService)
			: base("")
		{
			_regionManager = regionManager;
			_interacaoBarraTarrefaService = interacaoBarraTarrefaService;
			AtualizarItens();
			DestacarViewSelecionada();
			_regionManager.Regions[Regioes.TrabalhoRegion].NavigationService.Navigated += NavigationServiceOnNavigated;
			Itens.CollectionChanged += ItensOnCollectionChanged;
			_interacaoBarraTarrefaService.ChamandoModulo += InteracaoBarraTarrefaServiceOnChamandoModulo;
		}

		private void InteracaoBarraTarrefaServiceOnChamandoModulo(object sender, Type type)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			IItemBarraTarefa obj = Itens.FirstOrDefault((IItemBarraTarefa x) => x.ViewModel.GetType() == type);
			ModuloBase val = (ModuloBase)((obj != null) ? obj.ViewModel : null);
			if ((int)val != 0)
			{
				((CommandBase<object>)(object)val.AbrirViewCommand)?.Execute((object)null);
			}
		}

		private void ItensOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			if (notifyCollectionChangedEventArgs.Action != 0)
			{
				return;
			}
			foreach (IItemBarraTarefa newItem in notifyCollectionChangedEventArgs.NewItems)
			{
				((ModuloBase)newItem.ViewModel).AbrirViewCommand.KeyGesture = (KeyGestureCustom)((Itens.Count < 11) ? new KeyGestureAlt(new KeyCustom((Key)(34 + Itens.Count - 1), (Itens.Count - 1).ToString())) : new KeyGestureAlt(new KeyCustom(Key.None, "None")));
			}
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			string nomeView = regionNavigationEventArgs.NavigationContext.Uri.OriginalString;
			if (Itens.Any((IItemBarraTarefa x) => x.NomeView == nomeView) && ((IEnumerable<object>)regionNavigationEventArgs.NavigationContext.NavigationService.Region.Views).Count() == Itens.Count)
			{
				DestacarViewSelecionada();
				return;
			}
			AtualizarItens();
			DestacarViewSelecionada();
		}

		private void AtualizarItens()
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			IViewsCollection views = _regionManager.Regions[Regioes.TrabalhoRegion].Views;
			foreach (IItemBarraTarefa iten in Itens)
			{
				((ModuloBase)iten.ViewModel).AbrirViewCommand.KeyGesture = null;
			}
			Itens.Clear();
			foreach (object item in (IEnumerable<object>)views)
			{
				ItemBarraTarefa itemBarraTarefa = new ItemBarraTarefa(item as FrameworkElement);
				if (itemBarraTarefa.NomeView != null)
				{
					Itens.Add((IItemBarraTarefa)(object)itemBarraTarefa);
				}
			}
			_interacaoBarraTarrefaService.SetarItensAbertos(Itens);
		}

		private void DestacarViewSelecionada()
		{
			foreach (IItemBarraTarefa iten in Itens)
			{
				iten.Desmarcar();
			}
			IItemBarraTarefa obj = Itens.FirstOrDefault((IItemBarraTarefa x) => x.ViewModel.GetType() == (((IEnumerable<object>)_regionManager.Regions[Regioes.TrabalhoRegion].ActiveViews).FirstOrDefault() as FrameworkElement)?.DataContext.GetType());
			if (obj != null)
			{
				obj.Marcar();
			}
			IItemBarraTarefa obj2 = Itens.FirstOrDefault();
			if (obj2 != null)
			{
				obj2.Desmarcar();
			}
		}

		public override void Dispose()
		{
			_regionManager.Regions[Regioes.TrabalhoRegion].NavigationService.Navigated -= NavigationServiceOnNavigated;
			Itens.CollectionChanged -= ItensOnCollectionChanged;
			_interacaoBarraTarrefaService.ChamandoModulo -= InteracaoBarraTarrefaServiceOnChamandoModulo;
			((NavigationBase)this).Dispose();
		}
	}
	public class BarraTituloMinimizarFecharViewModel : NavigationBase
	{
		public ICommand FecharCommand { get; set; }

		public ICommand MinimizarCommand { get; set; }

		public bool SinalParaMinimizarSistema
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SinalParaMinimizarSistema));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SinalParaMinimizarSistema), value);
			}
		}

		public BarraTituloMinimizarFecharViewModel()
			: base("")
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			MinimizarCommand = (ICommand)new DelegateCommand((Action)Minimizar);
			FecharCommand = (ICommand)new AsyncCommand((Func<Task>)Fechar);
		}

		private async Task Fechar()
		{
			await Task.Factory.StartNew(delegate
			{
				Process.GetCurrentProcess().Kill();
			});
		}

		private void Minimizar()
		{
			SinalParaMinimizarSistema = false;
			SinalParaMinimizarSistema = true;
		}
	}
	public class BarraTituloViewModel : NavigationBase
	{
		private readonly IAutenticacaoService _autenticacaoService;

		private readonly IEventPublisher _eventPublisher;

		private readonly IAcessoRapidoService _acessoRapidoService;

		private readonly IRegionManager _regionManager;

		private readonly IModuleManager _moduleManager;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IEnvioBoletoPendenteService _envioBoletoPendenteService;

		private IDisposable _sessaoAtualizadaEvent;

		private IDisposable _mensagensEvent;

		public static string DiretorioAjuda => Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "DigiSat", "SuiteG6", "Sistema");

		public bool InformacaoSistemaAberta
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => InformacaoSistemaAberta));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => InformacaoSistemaAberta), value);
			}
		}

		public string InformacaoSistema
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoSistema));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoSistema), value);
			}
		}

		public string DescricaoSistema
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoSistema));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoSistema), value);
			}
		}

		public string DescricaoMei
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoMei));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoMei), value);
			}
		}

		public bool PodeTrocarEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeTrocarEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeTrocarEmpresa), value);
			}
		}

		public bool SinalParaMinimizarSistema
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SinalParaMinimizarSistema));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SinalParaMinimizarSistema), value);
			}
		}

		public ISessao Sessao
		{
			get
			{
				return ((BindableBase)this).GetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao));
			}
			set
			{
				((BindableBase)this).SetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao), value, (Action)AlterouSessao);
			}
		}

		public Visibility MostrarUsuario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarUsuario));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarUsuario), value, (Action)delegate
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					//IL_000b: Expected O, but got Unknown
					((Base)this).Icone = (IIcone)new Usuario();
				});
			}
		}

		public Visibility MostrarNotificacaoMensagens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarNotificacaoMensagens));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarNotificacaoMensagens), value);
			}
		}

		public Visibility MostrarRotinasDiarias
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarRotinasDiarias));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarRotinasDiarias), value);
			}
		}

		public int NumeroNotificacaoMensagens
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => NumeroNotificacaoMensagens));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => NumeroNotificacaoMensagens), value);
			}
		}

		public string TextoNotificacaoMensagens
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TextoNotificacaoMensagens));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TextoNotificacaoMensagens), value);
			}
		}

		public Visibility MostrarGerenciadorSenhas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarGerenciadorSenhas));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarGerenciadorSenhas), value);
			}
		}

		public ICommand FecharCommand { get; set; }

		public ICommand AbrirAjudaCommand { get; set; }

		public ICommand AbrirInformacaoSistemaCommand { get; set; }

		public ICommand MinimizarCommand { get; set; }

		public IDelegateCommand BloquearCommand { get; set; }

		public IDelegateCommand AlterarSenhaCommand { get; set; }

		public IDelegateCommand TrocarUsuarioCommand { get; set; }

		public IDelegateCommand TrocarEmpresaCommand { get; set; }

		public ICommand AbrirAcessoRapidoCommand { get; set; }

		public ICommand AbrirMensagensCommand { get; set; }

		public ICommand AbrirGerenciadorSenhasCommand { get; set; }

		public ICommand AbrirRotinasDiariasCommand { get; set; }

		public BarraTituloViewModel(IAutenticacaoService autenticacaoService, IEventPublisher eventPublisher, IAcessoRapidoService acessoRapidoService, IRegionManager regionManager, IModuleManager moduleManager, IImpressoraNaoFiscalService impressoraNaoFiscalService, IEnvioBoletoPendenteService envioBoletoPendenteService)
			: base("")
		{
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected O, but got Unknown
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Expected O, but got Unknown
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Expected O, but got Unknown
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Expected O, but got Unknown
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Expected O, but got Unknown
			//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Expected O, but got Unknown
			//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0203: Expected O, but got Unknown
			_autenticacaoService = autenticacaoService;
			_eventPublisher = eventPublisher;
			_acessoRapidoService = acessoRapidoService;
			_regionManager = regionManager;
			_moduleManager = moduleManager;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_envioBoletoPendenteService = envioBoletoPendenteService;
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			AbrirInformacaoSistemaCommand = (ICommand)new DelegateCommand((Action)AbrirInformacaoSistema, (Func<bool>)PodeTrocarUsuario, (bool?)null);
			AbrirAjudaCommand = (ICommand)new DelegateCommand((Action)AbrirAjuda);
			MinimizarCommand = (ICommand)new DelegateCommand((Action)Minimizar);
			FecharCommand = (ICommand)new DelegateCommand((Action)Fechar);
			BloquearCommand = (IDelegateCommand)new AsyncCommand((Func<Task>)Bloquear, (Func<bool>)PodeBloquear, (bool?)null);
			AlterarSenhaCommand = (IDelegateCommand)new AsyncCommand((Func<Task>)AlterarSenha, (Func<bool>)PodeAlterarSenha, (bool?)null);
			TrocarUsuarioCommand = (IDelegateCommand)new AsyncCommand((Func<Task>)TrocarUsuario, (Func<bool>)PodeTrocarUsuario, (bool?)null);
			TrocarEmpresaCommand = (IDelegateCommand)new AsyncCommand((Func<Task>)TrocarEmpresa, (Func<bool>)(() => PodeTrocarEmpresa), (bool?)null);
			AbrirAcessoRapidoCommand = acessoRapidoService.AbrirAcessoRapidoCommand;
			AbrirMensagensCommand = (ICommand)new DelegateCommand((Action)AbrirMensagens, (Func<bool>)PodeAbrirMensagens, (bool?)null);
			AbrirGerenciadorSenhasCommand = (ICommand)new DelegateCommand((Action)AbrirGerenciadorSenhas, (Func<bool>)PodeAbrirGerenciadorSenhas, (bool?)null);
			AbrirRotinasDiariasCommand = (ICommand)new DelegateCommand((Action)AbrirRotinasDiarias, (Func<bool>)PodeTrocarUsuario, (bool?)null);
			MostrarUsuario = Visibility.Collapsed;
			MostrarNotificacaoMensagens = Visibility.Collapsed;
			MostrarRotinasDiarias = Visibility.Collapsed;
			MostrarGerenciadorSenhas = Visibility.Collapsed;
			NumeroNotificacaoMensagens = 0;
		}

		private void AbrirInformacaoSistema()
		{
			InformacaoSistemaAberta = !InformacaoSistemaAberta;
		}

		private void AbrirAjuda()
		{
			string text = Path.Combine(DiretorioAjuda, "AjudaG6.chm");
			if (!File.Exists(text))
			{
				DialogoFactory.MostrarDialogoConfirma("Arquivo de ajuda não localizado.", false, HorizontalAlignment.Center);
				return;
			}
			Process process = new Process();
			process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			process.StartInfo.FileName = text;
			process.Start();
			process.Dispose();
		}

		private void OnParametroInformado(object sender, NavigationParameters e)
		{
			AssinarEventos();
			TestarSessao();
		}

		private void AssinarEventos()
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			if (HelperObject.IsNull((object)_sessaoAtualizadaEvent))
			{
				_sessaoAtualizadaEvent = ObservableExtensions.Subscribe<SessaoAtualizadaEvent>(_eventPublisher.GetEvent<SessaoAtualizadaEvent>(), (Action<SessaoAtualizadaEvent>)delegate(SessaoAtualizadaEvent @event)
				{
					Sessao = @event.Sessao;
					((UpdateViewBindableBase)this).RecuperarFocoAsync();
					if (!HelperObject.IsNull((object)Sessao))
					{
						string arg = (DadosSistema.EmpresaLogada.ModulosAdicionais.Any() ? ($"Módulos adicionais: {Environment.NewLine}" + "+ " + string.Join(Environment.NewLine + "+ ", DadosSistema.EmpresaLogada.ModulosAdicionais)) : string.Empty);
						Sistema sistema = DadosSistema.Sistema;
						DescricaoSistema = ((sistema != null) ? sistema.Descricao : null);
						DescricaoMei = (DadosSistema.IsMEI ? "Microempreendedor Individual" : string.Empty);
						InformacaoSistema = $"Versão: {DadosSistema.ObterVersaoSistema()}" + $"{Environment.NewLine}" + $"Release: {DadosSistema.ObterReleaseSistema()}" + $"{Environment.NewLine}" + $"{arg}";
						MostrarRotinasDiarias = ((!DadosSistema.IsGerencial) ? Visibility.Collapsed : Visibility.Visible);
						MostrarGerenciadorSenhas = ((!DadosSistema.IsRestauranteOuHotel) ? Visibility.Collapsed : Visibility.Visible);
					}
				});
			}
			if (!HelperObject.IsNull((object)_mensagensEvent))
			{
				return;
			}
			CompositeDisposable val = new CompositeDisposable();
			val.Add(ObservableExtensions.Subscribe<ConnectUserResponse>(_eventPublisher.GetEvent<ConnectUserResponse>(), (Action<ConnectUserResponse>)delegate(ConnectUserResponse connectUserResponse)
			{
				do
				{
					Thread.Sleep(100);
				}
				while (!HelperObject.IsNotNull((object)DadosSistema.ConfiguracaoDto));
				int num = connectUserResponse.Mensagens.Count((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao != 4);
				int num2 = ((DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor == 1) ? connectUserResponse.Mensagens.Count((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 4 && x.IdUsuario == DadosSistema.IdUsuario) : connectUserResponse.Mensagens.Count((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 4 && x.IdEmpresa == DadosSistema.EmpresaLogada.Id));
				AlterarNumeroNotificacaoMensagens(num2 + num);
			}));
			val.Add(ObservableExtensions.Subscribe<MensagemResponse>(_eventPublisher.GetEvent<MensagemResponse>(), (Action<MensagemResponse>)delegate(MensagemResponse response)
			{
				//IL_003a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0040: Invalid comparison between Unknown and I4
				if ((DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor != 2 || !(response.Mensagem.IdEmpresa != DadosSistema.EmpresaLogada.Id)) && ((int)response.Mensagem.TipoMensagemNotificacao != 4 || DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor != 1 || !(response.Mensagem.IdUsuario != DadosSistema.IdUsuario)))
				{
					if (response.Mensagem.Persistir)
					{
						AlterarNumeroNotificacaoMensagens(++NumeroNotificacaoMensagens);
					}
					MostrarNotificacaoToast(response);
				}
			}));
			val.Add(ObservableExtensions.Subscribe<NotificacaoAtendidaResponse>(_eventPublisher.GetEvent<NotificacaoAtendidaResponse>(), (Action<NotificacaoAtendidaResponse>)delegate
			{
				AlterarNumeroNotificacaoMensagens(--NumeroNotificacaoMensagens);
			}));
			val.Add(ObservableExtensions.Subscribe<DisconnectUserRequest>(_eventPublisher.GetEvent<DisconnectUserRequest>(), (Action<DisconnectUserRequest>)delegate
			{
				AlterarNumeroNotificacaoMensagens(0);
			}));
			_mensagensEvent = (IDisposable)val;
		}

		private bool PodeVisualizarNotificacao()
		{
			return DadosSistema.VerificarPermissao<Mensagem, Visualizar>();
		}

		private void MostrarNotificacaoToast(MensagemResponse response)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Invalid comparison between Unknown and I4
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Invalid comparison between Unknown and I4
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Invalid comparison between Unknown and I4
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Invalid comparison between Unknown and I4
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			if (PodeVisualizarNotificacao() && (int)response.Mensagem.TipoMensagemNotificacao != 0 && (int)response.Mensagem.TipoMensagemNotificacao != 6 && (int)response.Mensagem.TipoMensagemNotificacao != 7 && (int)response.Mensagem.TipoMensagemNotificacao != 10 && ((int)response.Mensagem.TipoMensagemNotificacao != 13 || !(response.Mensagem.IdUsuario != DadosSistema.IdUsuario)))
			{
				AlertaSonoro.TocarSomNotificacao(HelperEnum.RetornarValorPelaDescription<SomNotificacao>(DadosSistema.ConfiguracaoDto.Geral.SomAlertaNotificacao.Opcoes.FirstOrDefault((CodigoDescricaoDto x) => x.Codigo.Equals(DadosSistema.ConfiguracaoDto.Geral.SomAlertaNotificacao.Valor)).Descricao));
				_eventPublisher.Publish<NotificacaoToastEvent>(new NotificacaoToastEvent(response.Mensagem.Titulo, response.Mensagem.MensagemNotificacaoToast, AcaoNotificacaoToast(response.Mensagem)));
				ImprimirNotificacao(response);
			}
		}

		private Action AcaoNotificacaoToast(NotificacaoMensagem mensagem)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Invalid comparison between Unknown and I4
			if ((int)mensagem.TipoMensagemNotificacao == 13)
			{
				return delegate
				{
					_envioBoletoPendenteService.Enviar(mensagem);
				};
			}
			return AbrirMensagens;
		}

		private void ImprimirNotificacao(MensagemResponse mensagemResponse)
		{
			if (!DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.ImprimirNotificacoesAutoatendimentoMobile.Valor)
			{
				return;
			}
			Task.Run(delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Invalid comparison between Unknown and I4
				int num = (((int)DadosSistema.TamanhoBobina == 1) ? 32 : 42);
				List<string> list = new List<string>
				{
					"".PadRight(num, '-'),
					mensagemResponse.Mensagem.Titulo,
					"".PadRight(num, '-'),
					mensagemResponse.Mensagem.MensagemNotificacaoToast,
					"".PadRight(num, '-')
				};
				string[] array = mensagemResponse.Mensagem.Mensagem.Split('\n');
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(HelperString.RemoveDiacritics(text)))
					{
						list.Add(HelperString.CopiarTexto(text, num));
					}
				}
				list.Add("".PadRight(num, '-'));
				list.Add($"{mensagemResponse.Mensagem.DataHora:dd/MM/yyyy HH:mm}");
				list.Add("".PadRight(num, '-'));
				_impressoraNaoFiscalService.Imprimir((IEnumerable<string>)list);
			}).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					LoggerFactory.CreateLog().LogError(task.Exception.Message, (Exception)task.Exception, Array.Empty<object>());
				}
			});
		}

		private void AlterarNumeroNotificacaoMensagens(int numero)
		{
			if (PodeVisualizarNotificacao() && Application.Current != null)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					NumeroNotificacaoMensagens = numero;
					TextoNotificacaoMensagens = ((numero > 99) ? "99+" : numero.ToString());
					MostrarNotificacaoMensagens = ((numero <= 0) ? Visibility.Collapsed : Visibility.Visible);
				});
			}
		}

		private void TestarSessao()
		{
			if (HelperObject.IsNull((object)Sessao) && !HelperObject.IsNull((object)_autenticacaoService.Instancia.Sessao))
			{
				Sessao = _autenticacaoService.Instancia.Sessao;
			}
		}

		private async Task Bloquear()
		{
			_acessoRapidoService.SistemaBloqueado = true;
			await _autenticacaoService.Instancia.Bloquear();
		}

		private bool PodeBloquear()
		{
			return _autenticacaoService.Instancia.PodeBloquear;
		}

		private async Task AlterarSenha()
		{
			_acessoRapidoService.SistemaBloqueado = true;
			await _autenticacaoService.Instancia.AlterarSenha();
		}

		private bool PodeAlterarSenha()
		{
			return _autenticacaoService.Instancia.PodeAlterarSenha;
		}

		private async Task TrocarUsuario()
		{
			_acessoRapidoService.SistemaBloqueado = true;
			await _autenticacaoService.Instancia.ExecutarLogOut();
		}

		private bool PodeTrocarUsuario()
		{
			return _autenticacaoService.Instancia.PodeAutenticar;
		}

		private async Task TrocarEmpresa()
		{
			_acessoRapidoService.SistemaBloqueado = true;
			await _autenticacaoService.Instancia.TrocarEmpresa();
		}

		private void Fechar()
		{
			Application.Current.MainWindow.Close();
		}

		private void Minimizar()
		{
			SinalParaMinimizarSistema = false;
			SinalParaMinimizarSistema = true;
		}

		private void AbrirRotinasDiarias()
		{
			if (!DadosSistema.VerificarPermissao<GerenciamentoRotinaDiaria, Visualizar>())
			{
				DialogoFactory.MostrarDialogoConfirma("Acesso negado!", false, HorizontalAlignment.Center);
				return;
			}
			_moduleManager.LoadModule("GerenciamentoRotinaDiariaModulo");
			ItemAcessoRapido val = HelperObject.To<AcessoRapidoService>((object)_acessoRapidoService).Itens.FirstOrDefault((ItemAcessoRapido x) => x.NomeView.Equals("GerenciamentoRotinasDiariaView"));
			if (!HelperObject.IsNull((object)val))
			{
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)_regionManager.Regions[Regioes.TrabalhoRegion], val.NomeView, val.Parameters);
			}
		}

		private void AbrirMensagens()
		{
			_moduleManager.LoadModule("MensagemModulo");
			ItemAcessoRapido val = HelperObject.To<AcessoRapidoService>((object)_acessoRapidoService).Itens.FirstOrDefault((ItemAcessoRapido x) => x.NomeView.Equals("MensagensView"));
			if (!HelperObject.IsNull((object)val))
			{
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)_regionManager.Regions[Regioes.TrabalhoRegion], val.NomeView, val.Parameters);
			}
		}

		private bool PodeAbrirMensagens()
		{
			return PodeVisualizarNotificacao();
		}

		private void AbrirGerenciadorSenhas()
		{
			_moduleManager.LoadModule("GerenciadorSenhaModulo");
			ItemAcessoRapido val = HelperObject.To<AcessoRapidoService>((object)_acessoRapidoService).Itens.FirstOrDefault((ItemAcessoRapido x) => x.NomeView.Equals("GerenciadorSenhasView"));
			if (!HelperObject.IsNull((object)val))
			{
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)_regionManager.Regions[Regioes.TrabalhoRegion], val.NomeView, val.Parameters);
			}
		}

		private bool PodeAbrirGerenciadorSenhas()
		{
			return DadosSistema.VerificarPermissao<GerenciadorSenha, Visualizar>();
		}

		private void AlterouSessao()
		{
			MostrarUsuario = ((Sessao == null) ? Visibility.Collapsed : Visibility.Visible);
			ISessao sessao = Sessao;
			PodeTrocarEmpresa = sessao != null && sessao.Perfis.Numero > 1;
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= OnParametroInformado;
			_sessaoAtualizadaEvent?.Dispose();
			((NavigationBase)this).Dispose();
		}
	}
	public class MenuLateralViewModel : NavigationBase
	{
		private ISessao _sessao;

		private readonly IRegionManager _regionManager;

		private readonly IEventPublisher _eventPublisher;

		private readonly ITilesService _tilesService;

		private readonly IAcessoRapidoService _acessoRapidoService;

		private IDisposable _sessaoAtualizadaEvent;

		public ObservableCollection<ItemMenuLateral> Itens { get; set; } = new ObservableCollection<ItemMenuLateral>();


		public DelegateCommand<object> AbrirViewCommand { get; set; }

		public bool MostrarMenu
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarMenu));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarMenu), value);
			}
		}

		public MenuLateralViewModel(ISessao sessao, IRegionManager regionManager, IEventPublisher eventPublisher, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base("")
		{
			_sessao = sessao;
			_regionManager = regionManager;
			_eventPublisher = eventPublisher;
			_tilesService = tilesService;
			_acessoRapidoService = acessoRapidoService;
			AbrirViewCommand = new DelegateCommand<object>((Action<object>)AbrirView);
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			AssinarEventos();
			Itens.Clear();
			Task.Factory.StartNew(AdicionarItens, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void AssinarEventos()
		{
			_regionManager.Regions[Regioes.TrabalhoRegion].NavigationService.Navigated += NavigationServiceOnNavigated;
			if (HelperObject.IsNull((object)_sessaoAtualizadaEvent))
			{
				_sessaoAtualizadaEvent = ObservableExtensions.Subscribe<SessaoAtualizadaEvent>(_eventPublisher.GetEvent<SessaoAtualizadaEvent>(), (Action<SessaoAtualizadaEvent>)delegate(SessaoAtualizadaEvent @event)
				{
					_sessao = @event.Sessao;
				});
			}
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			MostrarMenu = regionNavigationEventArgs.Uri.ToString().Contains("IniciaisView");
		}

		private void AdicionarItens()
		{
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Expected O, but got Unknown
			ConfiguracaoCores val = ConfiguracaoCores.Carregar();
			Itens.Add(new CadastroItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorCadastros));
			Itens.Add(new EntradaItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorEntradas));
			Itens.Add(new SaidaItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorSaidas));
			Itens.Add(new FinanceiroItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorFinanceiros));
			if (DadosSistema.PodeExecutarFuncao(new Type[1] { typeof(AdministradorIndustria) }))
			{
				Itens.Add(new IndustriaItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorIndustrias));
			}
			Itens.Add(new ArquivosFiscaisEletronicosItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, _eventPublisher, HelperObject.IsNullUse<string>(val.CodigoCorArquivosFiscaisEletronicos, "#8c1186")));
			Itens.Add(new AuxiliarItemMenuLateral(_sessao, _tilesService, _acessoRapidoService, val.CodigoCorAuxiliares));
			_eventPublisher.Publish<CriouMenuLateralViewEvent>(new CriouMenuLateralViewEvent());
		}

		private void AbrirView(object obj)
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			MostrarMenu = false;
			if (obj is ItemMenuLateral itemMenuLateral)
			{
				_regionManager.Regions[Regioes.MenuRegion].NavigationService.Journal.Clear();
				IRegion obj2 = _regionManager.Regions[Regioes.MenuRegion];
				string name = typeof(MenuPrincipalView).Name;
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(ItemMenuLateral).Name, (object)itemMenuLateral);
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)obj2, name, val);
			}
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= OnParametroInformado;
			foreach (ItemMenuLateral iten in Itens)
			{
				iten.Dispose();
			}
			Itens.Clear();
			Itens = null;
			_sessaoAtualizadaEvent?.Dispose();
			_regionManager.Regions[Regioes.TrabalhoRegion].NavigationService.Navigated -= NavigationServiceOnNavigated;
			((NavigationBase)this).Dispose();
		}

		public void SetarMostrarMenu()
		{
			MostrarMenu = ((IEnumerable<object>)_regionManager.Regions[Regioes.TrabalhoRegion].Views).All((object a) => a.ToString().Contains("IniciaisView"));
		}
	}
	public class MenuPrincipalViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
	{
		private readonly IRegionManager _regionManager;

		private readonly IModuleManager _moduleManager;

		private readonly IInteracaoBarraTarrefaService _interacaoBarraTarrefaService;

		private readonly IEventPublisher _eventPublisher;

		private ItemMenuLateral _menu;

		public DelegateCommand FecharViewCommand { get; set; }

		public IRegionNavigationService NavigationService { get; private set; }

		public ItemMenuLateral Menu
		{
			get
			{
				return _menu;
			}
			set
			{
				((BindableBase)this).SetProperty<ItemMenuLateral>(ref _menu, value, "Menu");
			}
		}

		public bool KeepAlive { get; protected set; }

		public MenuPrincipalViewModel(IRegionManager regionManager, IModuleManager moduleManager, IInteracaoBarraTarrefaService interacaoBarraTarrefaService, IEventPublisher eventPublisher)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			_regionManager = regionManager;
			_moduleManager = moduleManager;
			_interacaoBarraTarrefaService = interacaoBarraTarrefaService;
			_eventPublisher = eventPublisher;
			FecharViewCommand = new DelegateCommand((Action)FecharView);
		}

		public void FecharView()
		{
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			KeepAlive = false;
			foreach (ItemMenuPrincipal iten in Menu.Itens)
			{
				iten.AbrirViewEvent -= ItemOnAbrirViewEvent;
			}
			foreach (object item in (IEnumerable<object>)NavigationService.Region.Views)
			{
				NavigationService.Region.Deactivate(item);
			}
			NavigationService.Journal.Clear();
			((((IEnumerable<object>)_regionManager.Regions[Regioes.MenuLateralRegion].Views).FirstOrDefault() as FrameworkElement).DataContext as MenuLateralViewModel).SetarMostrarMenu();
			_eventPublisher.Publish<FocoNosTilesEvent>(default(FocoNosTilesEvent));
		}

		private void AbrirView(ItemMenuPrincipal item)
		{
			if (item != null)
			{
				if (_interacaoBarraTarrefaService.ModuloAberto(item.NomeView))
				{
					_interacaoBarraTarrefaService.Navegar(item.NomeView, "TrabalhoRegion", (NavigationParameters)null);
				}
				else
				{
					_moduleManager.LoadModule(item.NomeModulo);
					NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)_regionManager.Regions[Regioes.TrabalhoRegion], item.NomeView, item.Parameters);
				}
			}
			FecharView();
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			NavigationService = navigationContext.NavigationService;
			Menu = navigationContext.Parameters[typeof(ItemMenuLateral).Name] as ItemMenuLateral;
			if (Menu == null)
			{
				return;
			}
			foreach (ItemMenuPrincipal iten in Menu.Itens)
			{
				iten.AbrirViewEvent += ItemOnAbrirViewEvent;
			}
		}

		private void ItemOnAbrirViewEvent(object sender, ItemMenuPrincipal item)
		{
			AbrirView(item);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}
	}
}
namespace Otus.Modulo.Menus.Services
{
	public interface ITilesService
	{
		event EventHandler<Tuple<string, string>> AdicionouTileEvent;

		void AdicionarTile(ItemMenuPrincipal item);

		void AdicionarTileSemSalvar(ItemMenuPrincipal item);

		void RemoverTile(ItemMenuPrincipal item);
	}
	public class MenuNavegacaoService : INavegacaoPreAutenticacaoService, INavegacaoInicialService, INavegacaoBloqueioService, INavegacaoConfiguracaoService, INavegacaoAlterarSenhaService
	{
		private readonly IRegionManagerService _regionManagerService;

		public MenuNavegacaoService(IRegionManagerService regionManagerService)
		{
			_regionManagerService = regionManagerService;
		}

		public void PreAutenticar()
		{
			RemoverEDesativarVisoes();
			_regionManagerService.AbrirVisao(Regioes.BarraTituloRegion, "BarraTituloView", true, (NavigationParameters)null);
		}

		public void TrocarEmpresa()
		{
			RemoverEDesativarVisoes();
			_regionManagerService.AbrirVisao(Regioes.BarraTituloRegion, "BarraTituloView", true, (NavigationParameters)null);
		}

		public void Bloquear()
		{
			RemoverEDesativarVisoes();
		}

		private void RemoverEDesativarVisoes()
		{
			_regionManagerService.DesativarVisao(Regioes.MenuLateralRegion);
			_regionManagerService.DesativarVisao(Regioes.BarraTarefaRegion);
			_regionManagerService.DesativarVisao(Regioes.MenuRegion);
		}

		public void Desbloquear()
		{
			_regionManagerService.AbrirVisao(Regioes.MenuLateralRegion, "MenuLateralView", true, (NavigationParameters)null);
			_regionManagerService.AbrirVisao(Regioes.BarraTarefaRegion, "BarraTarefaView", true, (NavigationParameters)null);
		}

		public void AlterarSenha()
		{
			RemoverEDesativarVisoes();
		}

		public void Navegar()
		{
			_regionManagerService.AbrirVisao(Regioes.MenuLateralRegion, "MenuLateralView", true, (NavigationParameters)null);
			_regionManagerService.AbrirVisao(Regioes.BarraTarefaRegion, "BarraTarefaView", true, (NavigationParameters)null);
			_regionManagerService.AbrirVisao(Regioes.BarraTituloRegion, "BarraTituloView", true, (NavigationParameters)null);
		}

		public void Configurar()
		{
			_regionManagerService.AbrirVisao(Regioes.BarraTituloRegion, "BarraTituloMinimizarFecharView", true, (NavigationParameters)null);
		}
	}
	public class TilesService : UpdateViewBindableBase, ITilesService
	{
		private readonly IModuleManager _moduleManager;

		private readonly IRegionManager _regionManager;

		private readonly IEventPublisher _eventPublisher;

		private readonly IWService _service;

		private readonly ISessaoService _sessaoService;

		private readonly List<Tuple<string, string>> _modulosBaseComercial = new List<Tuple<string, string>>
		{
			new Tuple<string, string>("MovimentacaoModulo", "MovimentacoesEntradaView"),
			new Tuple<string, string>("MovimentacaoModulo", "MovimentacoesSaidaView"),
			new Tuple<string, string>("PontoDeVendaModulo", "PontoDeVendasView")
		};

		private readonly List<Tuple<string, string>> _modulosBase = new List<Tuple<string, string>>
		{
			new Tuple<string, string>("PessoaModulo", "PessoasView"),
			new Tuple<string, string>("ProdutoServicoModulo", "ProdutosServicosView"),
			new Tuple<string, string>("MovimentacaoModulo", "MovimentacoesEntradaView"),
			new Tuple<string, string>("MovimentacaoModulo", "MovimentacoesSaidaView"),
			new Tuple<string, string>("PontoDeVendaModulo", "PontoDeVendasView"),
			new Tuple<string, string>("PagamentoModulo", "PagamentosView"),
			new Tuple<string, string>("RecebimentoModulo", "RecebimentosView")
		};

		public List<Tuple<string, string>> Tiles { get; set; } = new List<Tuple<string, string>>();


		public event EventHandler<Tuple<string, string>> AdicionouTileEvent;

		public TilesService(IModuleManager moduleManager, IRegionManager regionManager, IEventPublisher eventPublisher, IWService service, ISessaoService sessaoService)
		{
			_moduleManager = moduleManager;
			_regionManager = regionManager;
			_eventPublisher = eventPublisher;
			_service = service;
			_sessaoService = sessaoService;
			_eventPublisher.GetEvent<CarregouInicialViewEvent>((Action<CarregouInicialViewEvent>)delegate
			{
				Tiles.Clear();
				ObterModulosPinadosDoUsuario();
			});
		}

		private void ObterModulosPinadosDoUsuario()
		{
			List<ModuloPinado> modulos = HelperIEnumerable.ToListEmptyWhenNull<ModuloPinado>(_sessaoService.Sessao.ModulosPinados);
			bool pinAtualizado = _sessaoService.Sessao.PinAtualizado;
			if (DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Comercial),
				typeof(DigiSatMEI)
			}))
			{
				foreach (Tuple<string, string> item in _modulosBaseComercial)
				{
					OnAdicionouTileEvent(item);
				}
				return;
			}
			if (DadosSistema.PodeExecutarFuncao(new Type[1] { typeof(Gerencial) }))
			{
				AdicionarModulosBase(pinAtualizado, modulos, 7);
			}
			else if (DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				AdicionarModulosBase(pinAtualizado, modulos, int.MaxValue);
			}
		}

		private void AdicionarModulosBase(bool pinAtualizado, IEnumerable<ModuloPinado> modulos, int limite)
		{
			int count = ((modulos.Count() < limite) ? modulos.Count() : ((modulos.Count() > limite) ? limite : modulos.Count()));
			if (!pinAtualizado)
			{
				AdicionarTileModulo(_modulosBase);
				return;
			}
			IEnumerable<Tuple<string, string>> modulos2 = modulos.Select((ModuloPinado x) => new Tuple<string, string>(x.Modulo, x.View)).Take(count);
			AdicionarTileModulo(modulos2);
		}

		private void AdicionarTileModulo(IEnumerable<Tuple<string, string>> modulos)
		{
			IEnumerable<Tuple<string, string>> enumerable2;
			if (!DadosSistema.PodeAbrirPontoDeVenda())
			{
				IEnumerable<Tuple<string, string>> enumerable = HelperIEnumerable.ToListEmptyWhenNull<Tuple<string, string>>(modulos.Where((Tuple<string, string> x) => !x.Item1.Equals("PontoDeVendaModulo")));
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = modulos;
			}
			foreach (Tuple<string, string> item in enumerable2)
			{
				OnAdicionouTileEvent(item);
			}
		}

		public void AdicionarTile(ItemMenuPrincipal itemMenuPrincipal)
		{
			try
			{
				if (DadosSistema.PodeExecutarFuncao(new Type[1] { typeof(Gerencial) }) && Tiles.Count >= 7)
				{
					throw new Exception("Número máximo de itens na tela inicial excedido para versão atual do sistema.");
				}
				if (DadosSistema.PodeExecutarFuncao(new Type[1] { typeof(Comercial) }))
				{
					throw new Exception("Não é possível adicionar mais módulos na tela inicial nesta versão do sistema.");
				}
				AdicionarTileSemSalvar(itemMenuPrincipal);
				Salvar();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public void AdicionarTileSemSalvar(ItemMenuPrincipal itemMenuPrincipal)
		{
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			if (!Tiles.Any((Tuple<string, string> x) => x.Item1 == itemMenuPrincipal.NomeModulo && x.Item2 == itemMenuPrincipal.NomeView))
			{
				Tiles.Add(new Tuple<string, string>(itemMenuPrincipal.NomeModulo, itemMenuPrincipal.NomeView));
				itemMenuPrincipal.AbrirViewEvent += ItemMenuPrincipalOnAbrirViewEvent;
				_eventPublisher.Publish<AdicionouTilePrincipalEvent>(new AdicionouTilePrincipalEvent(itemMenuPrincipal.Tile));
			}
		}

		private void ItemMenuPrincipalOnAbrirViewEvent(object sender, ItemMenuPrincipal itemMenuPrincipal)
		{
			_moduleManager.LoadModule(itemMenuPrincipal.NomeModulo);
			NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)_regionManager.Regions[Regioes.TrabalhoRegion], itemMenuPrincipal.NomeView, itemMenuPrincipal.Parameters);
		}

		private async void Salvar()
		{
			try
			{
				await _service.Canal<IUsuarioModuleService, Task>((Func<IUsuarioModuleService, Task>)((IUsuarioModuleService s) => s.AtualizarModulosPinados(((IEnumerable<Tuple<string, string>>)Tiles).Select((Func<Tuple<string, string>, ModuloPinadoDto>)((Tuple<string, string> x) => new ModuloPinadoDto
				{
					Modulo = x.Item1,
					View = x.Item2
				})))));
			}
			catch (Exception)
			{
			}
		}

		public void RemoverTile(ItemMenuPrincipal itemMenuPrincipal)
		{
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			try
			{
				if (DadosSistema.PodeExecutarFuncao(new Type[2]
				{
					typeof(Comercial),
					typeof(DigiSatMEI)
				}))
				{
					throw new Exception("Não é possível remover módulos da tela inicial nesta versão do sistema.");
				}
				Tiles.Remove(Tiles.FirstOrDefault((Tuple<string, string> x) => x.Item1 == itemMenuPrincipal.NomeModulo && x.Item2 == itemMenuPrincipal.NomeView));
				itemMenuPrincipal.AbrirViewEvent += ItemMenuPrincipalOnAbrirViewEvent;
				_eventPublisher.Publish<RemoveuTilePrincipalEvent>(new RemoveuTilePrincipalEvent(itemMenuPrincipal.Tile));
				Salvar();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		protected virtual void OnAdicionouTileEvent(Tuple<string, string> e)
		{
			this.AdicionouTileEvent?.Invoke(this, e);
		}
	}
}
namespace Otus.Modulo.Menus.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Menus.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.Menus.Models.Menus.Lateral
{
	public class ArquivosFiscaisEletronicosItemMenuLateral : ItemMenuLateral
	{
		public ArquivosFiscaisEletronicosItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, IEventPublisher eventPublisher, string codigoCor)
			: base((IGrupo)(object)new ArquivosFiscaisEletronicos(codigoCor), (IIcone)new IconeArquivosFiscaisEletronicos(), sessao, tilesService, acessoRapidoService)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			ArquivosFiscaisEletronicosItemMenuLateral arquivosFiscaisEletronicosItemMenuLateral = this;
			eventPublisher.GetEvent<TestouWebServiceAtivoEvent>((Action<TestouWebServiceAtivoEvent>)delegate(TestouWebServiceAtivoEvent testouEvent)
			{
				if (testouEvent.Ativo)
				{
					arquivosFiscaisEletronicosItemMenuLateral.Adicionar(new GestaoArquivoPafEcfItemMenuPrincipal(arquivosFiscaisEletronicosItemMenuLateral.Grupo, tilesService, acessoRapidoService));
				}
			});
			eventPublisher.GetEvent<HabilitarModuloEspecificoRSEvent>((Action<HabilitarModuloEspecificoRSEvent>)delegate
			{
				arquivosFiscaisEletronicosItemMenuLateral.Adicionar(new GeradorArquivoDevolucaoIcmsStSimplesNacionalItemMenuPrincipal(arquivosFiscaisEletronicosItemMenuLateral.Grupo, tilesService, acessoRapidoService));
			});
		}
	}
	public class AuxiliarItemMenuLateral : ItemMenuLateral
	{
		public AuxiliarItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Auxiliar(codigoCor), (IIcone)new IconeAuxiliar(), sessao, tilesService, acessoRapidoService)
		{
		}//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown

	}
	public class CadastroItemMenuLateral : ItemMenuLateral
	{
		public CadastroItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Cadastro(codigoCor), (IIcone)(object)new IconeCadastro(), sessao, tilesService, acessoRapidoService)
		{
		}
	}
	public class EntradaItemMenuLateral : ItemMenuLateral
	{
		public EntradaItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Entrada(codigoCor), (IIcone)(object)new IconeEntrada(), sessao, tilesService, acessoRapidoService)
		{
		}
	}
	public class FinanceiroItemMenuLateral : ItemMenuLateral
	{
		public FinanceiroItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Financeiro(codigoCor), (IIcone)(object)new IconeFinanceiro(), sessao, tilesService, acessoRapidoService)
		{
		}
	}
	public class IndustriaItemMenuLateral : ItemMenuLateral
	{
		public IndustriaItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Industria(codigoCor), (IIcone)new IconeIndustria(), sessao, tilesService, acessoRapidoService)
		{
		}//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown

	}
	public abstract class ItemMenuLateral : IDisposable
	{
		private IEnumerable<ItemMenuPrincipal> _itens = new List<ItemMenuPrincipal>();

		public IGrupo Grupo { get; protected set; }

		public IIcone Icone { get; protected set; }

		public IEnumerable<ItemMenuPrincipal> Itens
		{
			get
			{
				return (from o in (from c in _itens
						group c by c.Classificacao.Descricao into o
						select o.OrderBy((ItemMenuPrincipal z) => z.Descricao).Select((ItemMenuPrincipal s, int index) => s.SetarSequencia(index))).SelectMany((IEnumerable<ItemMenuPrincipal> l) => (l as IList<ItemMenuPrincipal>) ?? l.ToList()).ToList()
					orderby o.Classificacao.Descricao
					select o).ThenBy((ItemMenuPrincipal oo) => oo.Descricao);
			}
			set
			{
				_itens = value;
			}
		}

		protected ItemMenuLateral(IGrupo grupo, IIcone icone, ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected I4, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			Grupo = grupo;
			Icone = icone;
			Sistema tipo = DadosSistema.Sistema.Tipo;
			List<int> list = (int)tipo switch
			{
				0 => HelperIEnumerable.ToListEmptyWhenNull<int>(from x in Modulos.ObterRestricoesPorSistema<ISistemaComercial>(DadosSistema.Ramo)
					select x.ModuloReferencia), 
				1 => HelperIEnumerable.ToListEmptyWhenNull<int>(from x in Modulos.ObterRestricoesPorSistema<ISistemaGerencial>(DadosSistema.Ramo)
					select x.ModuloReferencia), 
				2 => HelperIEnumerable.ToListEmptyWhenNull<int>(from x in Modulos.ObterRestricoesPorSistema<ISistemaAdministrador>(DadosSistema.Ramo)
					select x.ModuloReferencia), 
				3 => HelperIEnumerable.ToListEmptyWhenNull<int>(from x in Modulos.ObterRestricoesPorSistema<ISistemaAdministradorIndustria>(DadosSistema.Ramo)
					select x.ModuloReferencia), 
				4 => HelperIEnumerable.ToListEmptyWhenNull<int>(from x in Modulos.ObterRestricoesPorSistema<ISistemaDigiSatMEI>(DadosSistema.Sistema.RamoAtividade)
					select x.ModuloReferencia), 
				_ => throw new ArgumentOutOfRangeException(), 
			};
			list.AddRange(DadosSistema.EmpresaLogada.CodigosModulosAdicionais);
			foreach (Restricao item in sessao.Perfis.ItemSelecionado.Restricoes.Where((Restricao x) => x.Direitos.Any((IDireito y) => HelperObject.Is<Visualizar>((object)y))))
			{
				if (list.Contains(item.ModuloReferencia))
				{
					Adicionar(FluentFactory<ItemMenuPrincipalFactory>.Init().SetarModulo(item.Modulo).SetarGrupo(Grupo)
						.SetarTileService(tilesService)
						.SetarAcessoRapidoService(acessoRapidoService)
						.Criar());
				}
			}
			if (((object)grupo).GetType() == typeof(Auxiliar) && (DadosSistema.UsuarioSuporteLogado || DadosSistema.UsuarioRevendaLogado || DadosSistema.UsuarioAdministradorLogado))
			{
				Adicionar(new AlteracaoRapidaItemMenuPrincipal(grupo, tilesService, acessoRapidoService));
			}
		}

		protected void Adicionar(ItemMenuPrincipal item)
		{
			if (item != null)
			{
				((List<ItemMenuPrincipal>)_itens).Add(item);
			}
		}

		public void Dispose()
		{
			foreach (ItemMenuPrincipal iten in _itens)
			{
				iten.Dispose();
			}
		}
	}
	public class SaidaItemMenuLateral : ItemMenuLateral
	{
		public SaidaItemMenuLateral(ISessao sessao, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string codigoCor)
			: base((IGrupo)(object)new Saida(codigoCor), (IIcone)(object)new IconeSaida(), sessao, tilesService, acessoRapidoService)
		{
		}
	}
}
namespace Otus.Modulo.Menus.Models.Menus.Grupo
{
	public class ArquivosFiscaisEletronicos : GrupoBase
	{
		public override string Descricao => "Fiscal/Contábil";

		public ArquivosFiscaisEletronicos(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorArquivosFiscaisEletronicos(codigoCor);
		}
	}
	public class Auxiliar : GrupoBase
	{
		public override string Descricao => "Auxiliares";

		public Auxiliar(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorAuxiliares(codigoCor);
		}
	}
	public class Cadastro : GrupoBase
	{
		public override string Descricao => "Cadastros";

		public Cadastro(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorCadastro(codigoCor);
		}
	}
	public class Entrada : GrupoBase
	{
		public override string Descricao => "Entradas";

		public Entrada(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorEntrada(codigoCor);
		}
	}
	public class Financeiro : GrupoBase
	{
		public override string Descricao => "Financeiro";

		public Financeiro(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorFinanceiro(codigoCor);
		}
	}
	public abstract class GrupoBase : IGrupo
	{
		public abstract string Descricao { get; }

		public string DescricaoUnderLine => "_" + Descricao;

		public ICor Cor { get; set; }
	}
	public class Industria : GrupoBase
	{
		public override string Descricao => "Indústria";

		public Industria(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorIndustria(codigoCor);
		}
	}
	public class Saida : GrupoBase
	{
		public override string Descricao => "Saídas";

		public Saida(string codigoCor)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			base.Cor = (ICor)new CorSaida(codigoCor);
		}
	}
}
namespace Otus.Modulo.Menus.Models.Menus.Classificacao
{
	public abstract class Classificacao : IClassificacao
	{
		public virtual string Descricao => GetType().Name;

		public abstract KeyGestureCustom ObterTecla(int sequencia);
	}
	public class Primario : Classificacao
	{
		public IEnumerable<string> Detalhes { get; set; }

		public Primario(string detalhes = "")
		{
			Detalhes = detalhes.Split('|').ToList();
		}

		public override KeyGestureCustom ObterTecla(int sequencia)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			int num;
			switch (sequencia)
			{
			default:
				return null;
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				num = 35 + sequencia;
				break;
			case 9:
				num = 34;
				break;
			}
			string text = ((sequencia + 1) % 10).ToString();
			return (KeyGestureCustom)new KeyGestureControl(new KeyCustom((Key)num, text));
		}
	}
	public class Secundario : Classificacao
	{
		private static readonly Key[] TeclasDisponiveis = ObterTeclasDisponiveis();

		private static Key[] ObterTeclasDisponiveis()
		{
			Key[] array = new Key[11]
			{
				Key.A,
				Key.C,
				Key.F,
				Key.L,
				Key.P,
				Key.S,
				Key.U,
				Key.V,
				Key.X,
				Key.Y,
				Key.Z
			};
			List<Key> list = new List<Key>();
			for (int i = 44; i <= 69; i++)
			{
				Key key = (Key)i;
				if (Array.IndexOf(array, key) < 0)
				{
					list.Add(key);
				}
			}
			return list.ToArray();
		}

		public override KeyGestureCustom ObterTecla(int sequencia)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			if (sequencia < 0)
			{
				return null;
			}
			int num = sequencia % TeclasDisponiveis.Length;
			int num2 = sequencia / TeclasDisponiveis.Length;
			KeyCustom val = new KeyCustom(TeclasDisponiveis[num], TeclasDisponiveis[num].ToString());
			return (KeyGestureCustom)(num2 switch
			{
				0 => (object)new KeyGestureControl(val), 
				1 => (object)new KeyGestureControlShift(val), 
				_ => null, 
			});
		}
	}
}
namespace Otus.Modulo.Menus.Models.Menus.BarraTarefa
{
	public class ItemBarraTarefa : BindableBase, IItemBarraTarefa
	{
		public object ViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<object>((Expression<Func<object>>)(() => ViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<object>((Expression<Func<object>>)(() => ViewModel), value);
			}
		}

		public VisualBrush View
		{
			get
			{
				return ((BindableBase)this).GetProperty<VisualBrush>((Expression<Func<VisualBrush>>)(() => View));
			}
			set
			{
				((BindableBase)this).SetProperty<VisualBrush>((Expression<Func<VisualBrush>>)(() => View), value);
			}
		}

		public string NomeView
		{
			get
			{
				object viewModel = ViewModel;
				object obj = ((viewModel is ModuloBase) ? viewModel : null);
				if (obj == null)
				{
					return null;
				}
				return ((ModuloBase)obj).NomeView;
			}
		}

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			protected set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value);
			}
		}

		public ItemBarraTarefa(Visual view)
		{
			ViewModel = ((FrameworkElement)view).DataContext;
			View = new VisualBrush(view);
		}

		public void Marcar()
		{
			Selecionado = true;
		}

		public void Desmarcar()
		{
			Selecionado = false;
		}
	}
}
namespace Otus.Modulo.Menus.Models.Menus.Principal
{
	public class CotacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public CotacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeCotacao(), "Cotação de compra", "CotacaoModulo", "CotacoesView", tilesService, acessoRapidoService, "CotacaoListaView")
		{
			base.OpcoesBusca = new List<string> { "cotação", "cotacao", "cotação de compra", "cotação de preços", "orçamento de compra", "concorrência", "melhor preço", "fornecedores" };
		}
	}
	public class AgendamentoItemMenuPrincipal : ItemMenuPrincipal
	{
		public AgendamentoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAgendamento(), "Agendamentos", "AgendamentoModulo", "AgendamentosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class AlteracaoRapidaItemMenuPrincipal : ItemMenuPrincipal
	{
		public AlteracaoRapidaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAlteracaoRapida(), "Alterações rápidas", "AlteracaoRapidaModulo", "AlteracoesRapidasView", tilesService, acessoRapidoService, "AlteracaoRapidaFichaView")
		{
		}
	}
	public class AnuncioShopeeItemMenuPrincipal : ItemMenuPrincipal
	{
		public AnuncioShopeeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeShopee(), "Anúncios da Shopee", "AnuncioShopeeModulo", "AnunciosShopeeView", tilesService, acessoRapidoService)
		{
		}
	}
	public class AnuncioMercadoLivreItemMenuPrincipal : ItemMenuPrincipal
	{
		public AnuncioMercadoLivreItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMercadoLivre(), "Anúncios do Mercado Livre", "AnuncioMercadoLivreModulo", "AnunciosMercadoLivreView", tilesService, acessoRapidoService)
		{
		}
	}
	public class AtualizacaoCustoMateriaPrimaItemMenuPrincipal : ItemMenuPrincipal
	{
		public AtualizacaoCustoMateriaPrimaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAtualizacaoCustoMateriaPrima(), "Atualização dos custos das matérias-primas", "AtualizacaoCustoMateriaPrimaModulo", "AtualizacoesCustoMateriaPrimaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class AtualizacaoGrupoComplementoItemMenuPrincipal : ItemMenuPrincipal
	{
		public AtualizacaoGrupoComplementoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAtualizacaoGrupoComplemento(), "Atualização dos menus", "AtualizacaoGrupoComplementoModulo", "AtualizacoesGrupoComplementoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GerenciadorTicketItemMenuPrincipal : ItemMenuPrincipal
	{
		public GerenciadorTicketItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGerenciadorTicket(), "Gerenciador de ticket", "GerenciadorTicketModulo", "GerenciadorTicketsView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GestaoCodigoIdentificadorOperacaoTransporteItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoCodigoIdentificadorOperacaoTransporteItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoCodigoIdentificadorOperacaoTransporte(), "Gestão de CIOTs", "GestaoCodigoIdentificadorOperacaoTransporteModulo", "GestaoCodigosIdentificadoresOperacoesTransporteView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GestaoSincronizacaoHospedagemFNRHItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoSincronizacaoHospedagemFNRHItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoSincronizacaoHospedagemFNRH(), "Sincronizações - FNRH", "GestaoSincronizacaoHospedagemFNRHModulo", "GestaoSincronizacoesHospedagemFNRHView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GestaoSincronizacaoRodoilProItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoSincronizacaoRodoilProItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoSincronizacaoRodoilPro(), "Sincronizações - Rodoil", "GestaoSincronizacaoRodoilProModulo", "GestaoSincronizacoesRodoilProView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GestaoSincronizacaoClubeConstrudataItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoSincronizacaoClubeConstrudataItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoSincronizacaoClubeConstrudata(), "Sincronizações - Clube Construdata", "GestaoSincronizacaoClubeConstrudataModulo", "GestaoSincronizacoesClubeConstrudataView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class AlteracoesPrecoAgendadasItemMenuPrincipal : ItemMenuPrincipal
	{
		public AlteracoesPrecoAgendadasItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTabelaPreco(), "Alterações de preço agendadas", "AlteracaoPrecoAgendadaModulo", "AlteracoesPrecoAgendadasView", tilesService, acessoRapidoService)
		{
		}
	}
	public class PedidoShopeeItemMenuPrincipal : ItemMenuPrincipal
	{
		public PedidoShopeeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeShopee(), "Pedidos da Shopee", "PedidoShopeeModulo", "PedidosShopeeView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ShopeeItemMenuPrincipal : ItemMenuPrincipal
	{
		public ShopeeItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeShopee(), "Autenticação Shopee", "ShopeeModulo", "ShopeesView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class RomaneioConhecimentoTransporteEletronicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public RomaneioConhecimentoTransporteEletronicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeRomaneioConhecimentoTransporteEletronico(), "Romaneio de CT-es", "RomaneioConhecimentoTransporteEletronicoModulo", "RomaneiosConhecimentoTransporteEletronicoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GerenciadorSenhaItemMenuPrincipal : ItemMenuPrincipal
	{
		public GerenciadorSenhaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGerenciadorSenha(), "Gerenciador de senhas", "GerenciadorSenhaModulo", "GerenciadorSenhasView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class SubstituicaoMateriaPrimaItemMenuPrincipal : ItemMenuPrincipal
	{
		public SubstituicaoMateriaPrimaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeSubstituicaoMateriaPrima(), "Substituição das matérias-primas", "SubstituicaoMateriaPrimaModulo", "SubstituicoesMateriaPrimaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class AutoatendimentoBalancaItemMenuPrincipal : ItemMenuPrincipal
	{
		public AutoatendimentoBalancaItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAutoatendimentoBalanca(), "Autoatendimento", "AutoatendimentoBalancaModulo", "AutoatendimentoBalancasView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class BalanceteItemMenuPrincipal : ItemMenuPrincipal
	{
		public BalanceteItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeBalancete(), "Balancetes", "BalanceteModulo", "BalancetesView", tilesService, acessoRapidoService)
		{
		}
	}
	public class CardapioItemMenuPrincipal : ItemMenuPrincipal
	{
		public CardapioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeCardapio(), "Cardápios", "CardapioModulo", "CardapiosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class CentroCustoItemMenuPrincipal : ItemMenuPrincipal
	{
		public CentroCustoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeCentroCusto(), "Centro de custos", "CentroCustoModulo", "CentrosCustoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ChequeItemMenuPrincipal : ItemMenuPrincipal
	{
		public ChequeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeCheque(), "Cheques", "ChequeModulo", "ChequesView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class ComissaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public ComissaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeComissao(), "Comissões", "ComissaoModulo", "ComissoesView", tilesService, acessoRapidoService, "ComissaoFichaView")
		{
		}
	}
	public class ConferenciaEstoqueItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConferenciaEstoqueItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeConferenciaEstoque(), "Conferência de estoque", "ConferenciaEstoqueModulo", "ConferenciasEstoqueView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ConfiguracaoDFeItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConfiguracaoDFeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeConfiguracao(), "Configurações DF-e", "ConfiguracaoDFeModulo", "ConfiguracoesDFeView", tilesService, acessoRapidoService, "ConfiguracaoDFeFichaView")
		{
		}
	}
	public class ConfiguracaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConfiguracaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Geral|Financeiro|Periféricos|E mais..."), (IIcone)(object)new IconeConfiguracao(), "Configurações", "ConfiguracaoModulo", "ConfiguracoesView", tilesService, acessoRapidoService, "ConfiguracaoFichaView")
		{
		}
	}
	public class ConhecimentoTransporteEletronicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConhecimentoTransporteEletronicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeCte(), "CT-e", "ConhecimentoTransporteEletronicoModulo", "ConhecimentosTransporteEletronicoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class ConsultaProdutoIMendesItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConsultaProdutoIMendesItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeConsultaProduto(), "Consulta tributária IMendes", "ConsultaProdutoIMendesModulo", "ConsultasProdutoIMendesView", tilesServicer, acessoRapidoService, "ConsultaProdutoIMendesFichaView")
		{
			base.OpcoesBusca = new List<string> { "Tche Mendes" };
			AdicionarItemAcessoRapido();
		}
	}
	public class ConsultaProdutoParametrizaSoftAdvIntegraItemMenuPrincipal : ItemMenuPrincipal
	{
		public ConsultaProdutoParametrizaSoftAdvIntegraItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeConsultaProduto(), "Consulta tributária Parametriza/ADV Integra", "ConsultaProdutoModulo", "ConsultasProdutoView", tilesServicer, acessoRapidoService, "ConsultaProdutoFichaView")
		{
		}
	}
	public class ContaItemMenuPrincipal : ItemMenuPrincipal
	{
		public ContaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeConta(), "Contas", "ContaModulo", "ContasView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ControleEntregaItemMenuPrincipal : ItemMenuPrincipal
	{
		public ControleEntregaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeControleEntrega(), "Controle de entregas", "ControleEntregaModulo", "ControlesEntregaView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class CustoIndiretoItemMenuPrincipal : ItemMenuPrincipal
	{
		public CustoIndiretoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeCustoIndireto(), "Custos indiretos", "CustoIndiretoModulo", "CustosIndiretosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class CustoItemMenuPrincipal : ItemMenuPrincipal
	{
		public CustoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeCusto(), "Custos", "CustoModulo", "CustosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class DeclaracaoExportacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public DeclaracaoExportacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeDeclaracaoExportacao(), "Declarações de exportação", "DeclaracaoExportacaoModulo", "DeclaracoesExportacaoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class DeliveryItemMenuPrincipal : ItemMenuPrincipal
	{
		public DeliveryItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeDelivery(), "Delivery", "DeliveryModulo", "DeliveryView", tilesServicer, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class DemonstrativoResultadoItemMenuPrincipal : ItemMenuPrincipal
	{
		public DemonstrativoResultadoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Financeiro|Inventário anual|Custo e Lucro|E mais..."), (IIcone)(object)new IconeDemonstrativoResultado(), "Demonstrativo de resultados", "DemonstrativoResultadoModulo", "DemonstrativoResultadoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class DevolucaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public DevolucaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeDevolucao(), "Devoluções", "DevolucaoModulo", "DevolucoesView", tilesService, acessoRapidoService, "DevolucaoListaView")
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GeradorArquivoDevolucaoIcmsStSimplesNacionalItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoDevolucaoIcmsStSimplesNacionalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoDevolucaoIcmsStSimplesNacional(), "Gerador de arquivo devolução ICMS-ST", "GeradorArquivoDevolucaoIcmsStSimplesNacionalModulo", "GeradorArquivosDevolucaoIcmsStSimplesNacionalView", tilesService, acessoRapidoService, "GeradorArquivoDevolucaoIcmsStSimplesNacionalFichaView")
		{
		}
	}
	public class GestaoSincronizacaoDominioItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoSincronizacaoDominioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoSincronizacaoDominio(), "Sincronizações - Domínio", "GestaoSincronizacaoDominioModulo", "GestaoSincronizacoesDominioView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class LocacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public LocacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeLocacao(), "Locações", "LocacaoModulo", "LocacoesView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class MensagemWhatsappModeloItemMenuPrincipal : ItemMenuPrincipal
	{
		public MensagemWhatsappModeloItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMensagemWhatsappModelo(), "Modelos de mensagem WhatsApp", "MensagemWhatsappModeloModulo", "MensagensWhatsappModeloView", tilesService, acessoRapidoService)
		{
		}
	}
	public class EmailModeloItemMenuPrincipal : ItemMenuPrincipal
	{
		public EmailModeloItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeEmailModelo(), "Modelos de e-mail", "EmailModeloModulo", "EmailsModeloView", tilesService, acessoRapidoService)
		{
		}
	}
	public class EncargoSocialItemMenuPrincipal : ItemMenuPrincipal
	{
		public EncargoSocialItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeEncargoSocial(), "Encargos Sociais", "EncargoSocialModulo", "EncargosSocialView", tilesService, acessoRapidoService)
		{
		}
	}
	public class EstoqueFisicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public EstoqueFisicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeEstoqueFisico(), "Estoques físicos", "EstoqueFisicoModulo", "EstoquesFisicosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class EtiquetaItemMenuPrincipal : ItemMenuPrincipal
	{
		public EtiquetaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeEtiqueta(), "Gerador de etiquetas", "EtiquetaModulo", "EtiquetasView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ExportarArquivoContabilidadeItemMenuPrincipal : ItemMenuPrincipal
	{
		public ExportarArquivoContabilidadeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeExportarArquivoContabilidade(), "Exportar arquivo para contabilidade", "ExportarArquivoContabilidadeModulo", "ExportarArquivosContabilidadeView", tilesService, acessoRapidoService, "ExportarArquivoContabilidadeFichaView")
		{
		}
	}
	public class FarmaciaPopularItemMenuPrincipal : ItemMenuPrincipal
	{
		public FarmaciaPopularItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFarmaciaPopular(), "Farmácia Popular", "FarmaciaPopularModulo", "FarmaciasPopularesView", tilesService, acessoRapidoService)
		{
		}
	}
	public class FaseProducaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public FaseProducaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFaseProducao(), "Fases de produção", "FaseProducaoModulo", "FasesProducaoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class FatorConversaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public FatorConversaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFatorConversao(), "Fatores de Conversão", "FatorConversaoModulo", "FatoresConversaoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class FeriadoItemMenuPrincipal : ItemMenuPrincipal
	{
		public FeriadoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFeriado(), "Feriados", "FeriadoModulo", "FeriadosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class FluxoCaixaItemMenuPrincipal : ItemMenuPrincipal
	{
		public FluxoCaixaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFinanceiro(), "Fluxo de caixa", "FluxoCaixaModulo", "FluxosCaixaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class FormaPagamentoMenuPrincipal : ItemMenuPrincipal
	{
		public FormaPagamentoMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeFormaPagamento(), "Formas de pagamento", "FormaPagamentoModulo", "FormasPagamentoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GeradorArquivoBalancaItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoBalancaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoBalanca(), "Gerador de arquivo para balanças", "GeradorArquivoBalancaModulo", "GeradorArquivosBalancaView", tilesService, acessoRapidoService, "GeradorArquivoBalancaFichaView")
		{
		}
	}
	public class GeradorArquivoContabilidadeItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoContabilidadeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoContabilidade(), "Envio arquivos para contabilidade", "GeradorArquivoContabilidadeModulo", "GeradorArquivosContabilidadeView", tilesService, acessoRapidoService, "GeradorArquivoContabilidadeFichaView")
		{
		}
	}
	public class GeradorArquivoEfdContribuicaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoEfdContribuicaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoEfdContribuicao(), "Gerador de arquivo EFD Contribuições", "GeradorArquivoEfdContribuicaoModulo", "GeradorArquivosEfdContribuicaoView", tilesService, acessoRapidoService, "GeradorArquivoEfdContribuicaoFichaView")
		{
		}
	}
	public class GeradorArquivoInventarioItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoInventarioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoInventario(), "Gerador de arquivo Inventário", "GeradorArquivoInventarioModulo", "GeradorArquivosInventarioView", tilesService, acessoRapidoService, "GeradorArquivoInventarioFichaView")
		{
		}
	}
	public class GeradorArquivoLivroCaixaDigitalProdutorRuralItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoLivroCaixaDigitalProdutorRuralItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoLivroCaixaDigitalProdutorRural(), "Gerador de arquivo LCDPR", "GeradorArquivoLivroCaixaDigitalProdutorRuralModulo", "GeradorArquivosLivroCaixaDigitalProdutorRuralView", tilesService, acessoRapidoService, "GeradorArquivoLivroCaixaDigitalProdutorRuralFichaView")
		{
		}
	}
	public class GeradorArquivoSintegraItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoSintegraItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoSintegra(), "Gerador de arquivo Sintegra", "GeradorArquivoSintegraModulo", "GeradorArquivosSintegraView", tilesService, acessoRapidoService, "GeradorArquivoSintegraFichaView")
		{
		}
	}
	public class GeradorArquivoSpedFiscalItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoSpedFiscalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoSpedFiscal(), "Gerador de arquivo Sped Fiscal", "GeradorArquivoSpedFiscalModulo", "GeradorArquivosSpedFiscalView", tilesService, acessoRapidoService, "GeradorArquivoSpedFiscalFichaView")
		{
		}
	}
	public class GeradorArquivoTerminalConsultaItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoTerminalConsultaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoTerminalConsulta(), "Gerador de arquivo para terminal de consulta", "GeradorArquivoTerminalConsultaModulo", "GeradorArquivosTerminalConsultaView", tilesService, acessoRapidoService, "GeradorArquivoTerminalConsultaFichaView")
		{
		}
	}
	public class GeradorArquivoEtiquetasItemMenuPrincipal : ItemMenuPrincipal
	{
		public GeradorArquivoEtiquetasItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGeradorArquivoEtiquetas(), "Gerador de arquivo para etiquetas eletrônicas", "GeradorArquivoEtiquetasModulo", "GeradorArquivosEtiquetasView", tilesService, acessoRapidoService, "GeradorArquivoEtiquetasFichaView")
		{
		}
	}
	public class GestaoArquivoPafEcfItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoArquivoPafEcfItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeReduzaoZ(), "Gestão de arquivos do PAF-ECF", "GestaoArquivoPafEcfModulo", "GestaoArquivosPafEcfView", tilesService, acessoRapidoService, "GestaoArquivoPafEcfFichaView")
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class GestaoBoletoItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoBoletoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGerenciadorBoleto(), "Gestão de boletos", "GestaoBoletoModulo", "GestaoBoletoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GestaoCashbackItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoCashbackItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Créditos gerados|Débitos gerados|Transferência|E mais..."), (IIcone)new IconeGestaoCashback(), "Gestão de cashback", "GestaoCashbackModulo", "GestaoCashbacksView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class GestaoContratoItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoContratoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGestaoContrato(), "Gestão de contratos", "GestaoContratoModulo", "GestaoContratosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GestaoPainelContabilItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoPainelContabilItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario(), (IIcone)new IconeGestaoPainelContabil(), "Gestão do portal do contador", "GestaoPainelContabilModulo", "GestaoPainelContabeisView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class GestaoSincronizacaoScanntechItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoSincronizacaoScanntechItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGestaoSincronizacaoScanntech(), "Sincronizações - Scanntech", "GestaoSincronizacaoScanntechModulo", "GestaoSincronizacoesScanntechView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class AgrupamentoQuartoItemMenuPrincipal : ItemMenuPrincipal
	{
		public AgrupamentoQuartoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeAgrupamentoQuarto(), "Agrupamento de quartos", "AgrupamentoQuartoModulo", "AgrupamentosQuartoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ImportacaoGuiaDaFarmaciaItemMenuPrincipal : ItemMenuPrincipal
	{
		public ImportacaoGuiaDaFarmaciaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeImportacaoGuiaDaFarmacia(), "Importar Guia da farmácia", "ImportacaoGuiaDaFarmaciaModulo", "ImportacoesGuiaDaFarmaciaView", tilesService, acessoRapidoService, "ImportacaoGuiaDaFarmaciaFichaView")
		{
		}
	}
	public class InternacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public InternacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeInternacao(), "Internações", "InternacaoModulo", "InternacoesView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class ManifestacaoDestinatarioCteItemMenuPrincipal : ItemMenuPrincipal
	{
		public ManifestacaoDestinatarioCteItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeManifestacaoDestinatarioCte(), "Manifestação do destinatário do CT-e", "ManifestacaoDestinatarioCteModulo", "ManifestacoesDestinatarioCteView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GrupoMenuRapidoItemMenuPrincipal : ItemMenuPrincipal
	{
		public GrupoMenuRapidoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGrupoMenuRapido(), "Grupos de complementos", "GrupoMenuRapidoModulo", "GruposMenuView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MenuRapidoItemMenuPrincipal : ItemMenuPrincipal
	{
		public MenuRapidoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeMenuRapido(), "Menus", "MenuRapidoModulo", "MenusRapidoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MetaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MetaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeMeta(), "Metas", "MetaModulo", "MetasView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class OrdemDesmontagemItemMenuPrincipal : ItemMenuPrincipal
	{
		public OrdemDesmontagemItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeOrdemDesmontagem(), "Ordens de Desmontagem", "OrdemDesmontagemModulo", "OrdensDesmontagemView", tilesService, acessoRapidoService)
		{
		}
	}
	public class PromocaoScanntechItemMenuPrincipal : ItemMenuPrincipal
	{
		public PromocaoScanntechItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePromocaoScanntech(), "Clube de Promoções", "PromocaoScanntechModulo", "PromocoesScanntechView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class HospedagemItemMenuPrincipal : ItemMenuPrincipal
	{
		public HospedagemItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Hospedagens|Mapa de quartos|E mais..."), (IIcone)(object)new IconeHospedagem(), "Hospedagens", "HospedagemModulo", "HospedagensView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class PropriedadeRuralItemMenuPrincipal : ItemMenuPrincipal
	{
		public PropriedadeRuralItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePropriedadeRural(), "Propriedades rurais", "PropriedadeRuralModulo", "PropriedadesRuraisView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class RelacaoCfopDevolucaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoCfopDevolucaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacaoCfopDevolucao(), "Relação CFOP e Devolução", "RelacaoCfopDevolucaoModulo", "RelacoesCfopDevolucaoView", tilesService, acessoRapidoService, "RelacaoCfopDevolucaoListaView")
		{
		}
	}
	public class RelacaoDadosFornecedorEntradaItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoDadosFornecedorEntradaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacaoDadosFornecedorEntrada(), "Relação dados fornecedor entrada", "RelacaoDadosFornecedorEntradaModulo", "RelacoesDadosFornecedorEntradaView", tilesService, acessoRapidoService, "RelacaoDadosFornecedorEntradaListaView")
		{
		}
	}
	public class AnimalItemMenuPrincipal : ItemMenuPrincipal
	{
		public AnimalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Pets|Vacinas|Consultas|E mais..."), (IIcone)(object)new IconeAnimal(), "Pets", "AnimalModulo", "AnimaisView", tilesService, acessoRapidoService)
		{
		}
	}
	public class RelacaoEspecieContaItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoEspecieContaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacaoEspecieConta(), "Relação Espécie e Conta (PDV)", "RelacaoEspecieContaModulo", "RelacoesEspecieContaView", tilesService, acessoRapidoService, "RelacaoEspecieContaListaView")
		{
		}
	}
	public class GerenciamentoRotinaDiariaItemMenuPrincipal : ItemMenuPrincipal
	{
		public GerenciamentoRotinaDiariaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGerenciamentoRotinaDiaria(), "Gerenciamento de rotinas diárias (GRD)", "GerenciamentoRotinaDiariaModulo", "GerenciamentoRotinasDiariaView", tilesService, acessoRapidoService, "GerenciamentoRotinaDiariaFichaView")
		{
		}
	}
	public class RelacaoPisCofinsEntradaItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoPisCofinsEntradaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacaoPisCofinsEntrada(), "Relação PIS/COFINS entrada", "RelacaoPisCofinsEntradaModulo", "RelacoesPisCofinsEntradaView", tilesService, acessoRapidoService, "RelacaoPisCofinsEntradaFichaView")
		{
		}
	}
	public class SugestaoCompraItemMenuPrincipal : ItemMenuPrincipal
	{
		public SugestaoCompraItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeSugestaoCompra(), "Sugestão de compra", "SugestaoCompraModulo", "SugestaoComprasView", tilesService, acessoRapidoService, "SugestaoCompraFichaView")
		{
		}
	}
	public class RelacionamentoClientelItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacionamentoClientelItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacionamentoCliente(), "Gestão de relacionamento (CRM)", "RelacionamentoClienteModulo", "RelacionamentoClientesView", tilesService, acessoRapidoService, "RelacionamentoClienteFichaView")
		{
		}
	}
	public class GestaoEmpresarialItemMenuPrincipal : ItemMenuPrincipal
	{
		public GestaoEmpresarialItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Movimentações|Financeiro|E mais..."), (IIcone)(object)new IconeGestaoEmpresarial(), "Gestão empresarial", "GestaoEmpresarialModulo", "GestaoEmpresarialView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GradeItemMenuPrincipal : ItemMenuPrincipal
	{
		public GradeItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeGrade(), "Grades", "GradeModulo", "GradesView", tilesService, acessoRapidoService)
		{
		}
	}
	public class GrupoItemMenuPrincipal : ItemMenuPrincipal
	{
		public GrupoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeGrupo(), "Grupos", "GrupoModulo", "GruposView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class ImportacaoAbcFarmaItemMenuPrincipal : ItemMenuPrincipal
	{
		public ImportacaoAbcFarmaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeImportacaoAbcFarma(), "Importar ABC Farma", "ImportacaoAbcFarmaModulo", "ImportacoesAbcFarmaView", tilesService, acessoRapidoService, "ImportacaoAbcFarmaFichaView")
		{
		}
	}
	public class InutilizacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public InutilizacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeInutilizacao(), "Inutilizações", "InutilizacaoModulo", "InutilizacoesView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ItemMenuPrincipal : BindableBase, IDisposable
	{
		private readonly ITilesService _tilesService;

		private readonly IAcessoRapidoService _acessoRapidoService;

		public IEnumerable<string> OpcoesBusca { get; protected set; }

		public string Id { get; }

		public IGrupo Grupo { get; }

		public IClassificacao Classificacao { get; }

		public IIcone Icone { get; }

		public string Descricao { get; }

		public string NomeModulo { get; }

		public string NomeView { get; }

		public string ViewLista { get; }

		public CustomDelegateCommand<ItemMenuPrincipal> AbrirViewCommand { get; set; }

		public ICommand FixarNoInicioCommand { get; set; }

		public KeyGestureCustom Atalho { get; protected set; }

		public NavigationParameters Parameters
		{
			get
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				//IL_003d: Unknown result type (might be due to invalid IL or missing references)
				//IL_004e: Unknown result type (might be due to invalid IL or missing references)
				//IL_005f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0071: Expected O, but got Unknown
				NavigationParameters val = new NavigationParameters();
				val.Add("ID", (object)Id);
				val.Add("Cor", (object)Grupo.Cor);
				val.Add("Icone", (object)Icone);
				val.Add("NomeView", (object)NomeView);
				val.Add("Descricao", (object)Descricao);
				val.Add("ViewLista", (object)ViewLista);
				return val;
			}
		}

		public TilePrincipal Tile { get; protected set; }

		public int AnguloPin
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => AnguloPin));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => AnguloPin), value);
			}
		}

		public bool Pin => AnguloPin != 0;

		public event EventHandler<ItemMenuPrincipal> AbrirViewEvent;

		public ItemMenuPrincipal(IGrupo grupo, IClassificacao classificacao, IIcone icone, string descricao, string nomeModulo, string nomeView, ITilesService tilesService, IAcessoRapidoService acessoRapidoService, string viewLista = "")
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Expected O, but got Unknown
			_tilesService = tilesService;
			_acessoRapidoService = acessoRapidoService;
			FixarNoInicioCommand = (ICommand)new DelegateCommand((Action)FixarNaTelaInicial);
			Id = Guid.NewGuid().ToString();
			Grupo = grupo;
			Classificacao = classificacao;
			Icone = icone;
			Descricao = descricao;
			NomeModulo = nomeModulo;
			NomeView = nomeView;
			ViewLista = viewLista;
			AdicionarItemAcessoRapido();
			Tile = TilePrincipal.Criar().ComCor(Grupo.Cor.Codigo).ComTamanho("Small")
				.ComActionAbrirView((Action)delegate
				{
					OnAbrirViewEvent(this);
				})
				.ComConteudoTile(ConteudoTile.Criar().ComDescricao(Descricao).ComIcone(Icone)
					.ComMostrarPin(Visibility.Visible)
					.ComDesfixarCommand((ICommand)new DelegateCommand((Action)delegate
					{
						Tile.OnDespinouTileEvent();
					})));
			ObservableExtensions.Subscribe<EventArgs>(Tile.WhenDespinouTileEvent, (Action<EventArgs>)delegate
			{
				AnguloPin = 0;
				_tilesService.RemoverTile(this);
			});
			ObservableExtensions.Subscribe<EventArgs>(Tile.WhenPinouTileEvent, (Action<EventArgs>)delegate
			{
				Pinar();
				_tilesService.AdicionarTile(this);
			});
			_tilesService.AdicionouTileEvent += TilesServiceOnAdicionouTileEvent;
		}

		protected void AdicionarItemAcessoRapido()
		{
			_acessoRapidoService.AdicionarModulo(ItemAcessoRapido.Criar().ComNomeModulo(NomeModulo).ComViewPrincipal(NomeView)
				.ComDescricao(Descricao)
				.ComOpcoesBusca(OpcoesBusca)
				.ComParametros(Parameters));
		}

		private void TilesServiceOnAdicionouTileEvent(object sender, Tuple<string, string> s)
		{
			if (!(s.Item1 != NomeModulo) && !(s.Item2 != NomeView))
			{
				Pinar();
				_tilesService.AdicionarTileSemSalvar(this);
			}
		}

		public ItemMenuPrincipal SetarSequencia(int sequencia)
		{
			Atalho = Classificacao.ObterTecla(sequencia);
			AbrirViewCommand = new CustomDelegateCommand<ItemMenuPrincipal>(Atalho, (Action<ItemMenuPrincipal>)OnAbrirViewEvent);
			return this;
		}

		protected virtual void OnAbrirViewEvent(ItemMenuPrincipal e)
		{
			if (!DadosSistema.TurnoAberto && (e is RecebimentoItemMenuPrincipal || e is PagamentoItemMenuPrincipal || e is MovimentoContaItemMenuPrincipal || e is MovimentacaoSaidaItemMenuPrincipal || e is MovimentacaoEntradaItemMenuPrincipal || e is PessoaItemMenuPrincipal))
			{
				DialogoFactory.MostrarDialogoConfirma("É necessário realizar a abertura do turno! Acesse 'Auxiliares > Turnos' ou, 'Menu administrativo' no Ponto de vendas", false, HorizontalAlignment.Center);
			}
			else
			{
				this.AbrirViewEvent?.Invoke(this, e);
			}
		}

		private void FixarNaTelaInicial()
		{
			if (Pin)
			{
				Tile.OnDespinouTileEvent();
			}
			else
			{
				Tile.OnPinouTileEvent();
			}
		}

		public void Pinar()
		{
			AnguloPin = -45;
		}

		public void Dispose()
		{
			_tilesService.AdicionouTileEvent -= TilesServiceOnAdicionouTileEvent;
		}
	}
	public class ItemMenuPrincipalFactory
	{
		private Modulo _modulo;

		private IGrupo _grupo;

		private ITilesService _tileService;

		private IAcessoRapidoService _acessoRapidoService;

		public ItemMenuPrincipalFactory SetarAcessoRapidoService(IAcessoRapidoService acessoRapidoService)
		{
			_acessoRapidoService = acessoRapidoService;
			return this;
		}

		public ItemMenuPrincipalFactory SetarTileService(ITilesService tilesService)
		{
			_tileService = tilesService;
			return this;
		}

		public ItemMenuPrincipalFactory SetarGrupo(IGrupo grupo)
		{
			_grupo = grupo;
			return this;
		}

		public ItemMenuPrincipalFactory SetarModulo(Modulo modulo)
		{
			_modulo = modulo;
			return this;
		}

		public ItemMenuPrincipal Criar()
		{
			if (((object)_grupo).GetType() == typeof(Cadastro))
			{
				if (HelperObject.Is<Grupo>((object)_modulo))
				{
					return new GrupoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ProdutoServico>((object)_modulo))
				{
					return new ProdutoServicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Usuario>((object)_modulo))
				{
					return new UsuarioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Perfil>((object)_modulo))
				{
					return new PerfilItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Grade>((object)_modulo))
				{
					return new GradeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Pessoa>((object)_modulo))
				{
					return new PessoaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TabelaPreco>((object)_modulo))
				{
					return new TabelaPrecoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoProdutoFornecedor>((object)_modulo))
				{
					return new RelacaoProdutoFornecedorItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoVendedorCliente>((object)_modulo))
				{
					return new RelacaoVendedorClienteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Promocao>((object)_modulo))
				{
					return new PromocaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PromocaoScanntech>((object)_modulo))
				{
					return new PromocaoScanntechItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Kit>((object)_modulo))
				{
					return new KitItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TributacaoMunicipal>((object)_modulo))
				{
					return new TributacaoMunicipalItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TributacaoEstadual>((object)_modulo))
				{
					return new TributacaoEstadualItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TributacaoFederal>((object)_modulo))
				{
					return new TributacaoFederalItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TributacaoIbsCbs>((object)_modulo))
				{
					return new TributacaoIbsCbsItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OperacaoFiscal>((object)_modulo))
				{
					return new OperacoesFiscaisItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Tanque>((object)_modulo))
				{
					return new TanqueBombaBicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Cardapio>((object)_modulo))
				{
					return new CardapioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MenuRapido>((object)_modulo))
				{
					return new MenuRapidoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GrupoMenu>((object)_modulo))
				{
					return new GrupoMenuRapidoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AtualizacaoGrupoComplemento>((object)_modulo))
				{
					return new AtualizacaoGrupoComplementoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Marca>((object)_modulo))
				{
					return new MarcaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AnuncioMercadoLivre>((object)_modulo))
				{
					return new AnuncioMercadoLivreItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AnuncioShopee>((object)_modulo))
				{
					return new AnuncioShopeeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ConsultaProduto>((object)_modulo))
				{
					return new ConsultaProdutoParametrizaSoftAdvIntegraItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ConsultaProdutoIMendes>((object)_modulo))
				{
					return new ConsultaProdutoIMendesItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Moeda>((object)_modulo))
				{
					return new MoedaMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacionamentoCliente>((object)_modulo))
				{
					return new RelacionamentoClientelItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoEspecieConta>((object)_modulo))
				{
					return new RelacaoEspecieContaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoCfopDevolucao>((object)_modulo))
				{
					return new RelacaoCfopDevolucaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Animal>((object)_modulo))
				{
					return new AnimalItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Hospedagem>((object)_modulo))
				{
					return new HospedagemItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Locacao>((object)_modulo))
				{
					return new LocacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AgrupamentoQuarto>((object)_modulo))
				{
					return new AgrupamentoQuartoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PropriedadeRural>((object)_modulo) && DadosSistema.EmpresaLogada.IsProdutorRural)
				{
					return new PropriedadeRuralItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TipoSegmentacao>((object)_modulo))
				{
					return new TipoSegmentacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Meta>((object)_modulo))
				{
					return new MetaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(Entrada))
			{
				if (HelperObject.Is<FatorConversao>((object)_modulo))
				{
					return new FatorConversaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MontagemComposicao>((object)_modulo))
				{
					return new MontagemComposicaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MovimentacaoEntrada>((object)_modulo))
				{
					return new MovimentacaoEntradaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MovimentacaoCtrc>((object)_modulo))
				{
					return new MovimentacaoCtrcItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ManifestacaoDestinatario>((object)_modulo))
				{
					return new ManifestacaoDestinatarioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ManifestacaoDestinatarioCte>((object)_modulo))
				{
					return new ManifestacaoDestinatarioCteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoDadosFornecedorEntrada>((object)_modulo))
				{
					return new RelacaoDadosFornecedorEntradaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RelacaoPisCofinsEntrada>((object)_modulo))
				{
					return new RelacaoPisCofinsEntradaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<SugestaoCompra>((object)_modulo))
				{
					return new SugestaoCompraItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Cotacao>((object)_modulo))
				{
					return new CotacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(Saida))
			{
				if (HelperObject.Is<Objeto>((object)_modulo))
				{
					return new ObjetoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MovimentacaoSaida>((object)_modulo))
				{
					return new MovimentacaoSaidaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Restaurante>((object)_modulo))
				{
					return new RestauranteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MapaMesa>((object)_modulo))
				{
					return new MapaMesaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Delivery>((object)_modulo))
				{
					return new DeliveryItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ControleEntrega>((object)_modulo))
				{
					return new ControleEntregaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PontoDeVenda>((object)_modulo) && DadosSistema.PodeAbrirPontoDeVenda())
				{
					return new PontoDeVendaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<FarmaciaPopular>((object)_modulo))
				{
					return new FarmaciaPopularItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ConhecimentoTransporteEletronico>((object)_modulo))
				{
					return new ConhecimentoTransporteEletronicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ManifestoEletronicoDocumentoFiscal>((object)_modulo))
				{
					return new ManifestoEletronicoDocumentoFiscalItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OrdemServico>((object)_modulo))
				{
					return new OrdemServicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RomaneioCarga>((object)_modulo))
				{
					return new RomaneioCargaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RomaneioConhecimentoTransporteEletronico>((object)_modulo))
				{
					return new RomaneioConhecimentoTransporteEletronicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Devolucao>((object)_modulo))
				{
					return new DevolucaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PainelPedidosRestaurante>((object)_modulo))
				{
					return new PainelPedidosItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AutoatendimentoBalanca>((object)_modulo))
				{
					return new AutoatendimentoBalancaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PedidoIfood>((object)_modulo) && DadosSistema.IfoodDisponivel)
				{
					return new PedidoIfoodItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PedidoMercadoLivre>((object)_modulo))
				{
					return new PedidoMercadoLivreItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PedidoShopee>((object)_modulo))
				{
					return new PedidoShopeeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<PedidoEcommerce>((object)_modulo))
				{
					return new PedidoEcommerceItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ValePresente>((object)_modulo))
				{
					return new ValePresenteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(Financeiro))
			{
				if (HelperObject.Is<PlanoConta>((object)_modulo))
				{
					return new PlanoContaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<CentroCusto>((object)_modulo))
				{
					return new CentroCustoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Conta>((object)_modulo))
				{
					return new ContaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MovimentoConta>((object)_modulo))
				{
					return new MovimentoContaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Pagamento>((object)_modulo))
				{
					return new PagamentoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Recebimento>((object)_modulo))
				{
					return new RecebimentoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoBoleto>((object)_modulo))
				{
					return new GestaoBoletoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<FormaPagamento>((object)_modulo))
				{
					return new FormaPagamentoMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Comissao>((object)_modulo))
				{
					return new ComissaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RenegociacaoECobranca>((object)_modulo))
				{
					return new RenegociacaoECobrancaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<RenegociacaoPagamento>((object)_modulo))
				{
					return new RenegociacaoPagamentoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Cheque>((object)_modulo))
				{
					return new ChequeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<FluxoCaixa>((object)_modulo))
				{
					return new FluxoCaixaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Balancete>((object)_modulo))
				{
					return new BalanceteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Transferencia>((object)_modulo))
				{
					return new TransferenciaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TransacaoPbm>((object)_modulo))
				{
					return new TransacaoPbmItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoContrato>((object)_modulo))
				{
					return new GestaoContratoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(Auxiliar))
			{
				if (HelperObject.Is<EmailModelo>((object)_modulo))
				{
					return new EmailModeloItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MensagemWhatsappModelo>((object)_modulo))
				{
					return new MensagemWhatsappModeloItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Configuracao>((object)_modulo))
				{
					return new ConfiguracaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ConfiguracaoDFe>((object)_modulo))
				{
					return new ConfiguracaoDFeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Etiqueta>((object)_modulo))
				{
					return new EtiquetaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Inutilizacao>((object)_modulo))
				{
					return new InutilizacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<DeclaracaoExportacao>((object)_modulo))
				{
					return new DeclaracaoExportacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<TipoPersonalizado>((object)_modulo))
				{
					return new TipoPersonalizadoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoBalanca>((object)_modulo))
				{
					return new GeradorArquivoBalancaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoTerminalConsulta>((object)_modulo))
				{
					return new GeradorArquivoTerminalConsultaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoEtiquetas>((object)_modulo))
				{
					return new GeradorArquivoEtiquetasItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoEmpresarial>((object)_modulo))
				{
					return new GestaoEmpresarialItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<DemonstrativoResultado>((object)_modulo))
				{
					return new DemonstrativoResultadoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Turno>((object)_modulo))
				{
					return new TurnoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Relatorio>((object)_modulo))
				{
					return new RelatorioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ImportacaoAbcFarma>((object)_modulo))
				{
					return new ImportacaoAbcFarmaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ImportacaoGuiaDaFarmacia>((object)_modulo))
				{
					return new ImportacaoGuiaDaFarmaciaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Agendamento>((object)_modulo))
				{
					return new AgendamentoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ConferenciaEstoque>((object)_modulo))
				{
					return new ConferenciaEstoqueItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Mensagem>((object)_modulo))
				{
					return new MensagemItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OpcaoNotificacao>((object)_modulo))
				{
					return new OpcaoNotificacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Shopee>((object)_modulo))
				{
					return new ShopeeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MercadoLivre>((object)_modulo))
				{
					return new MercadoLivreItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<SincronizacaoEcommerce>((object)_modulo))
				{
					return new SincronizacaoEcommerceItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Internacao>((object)_modulo))
				{
					return new InternacaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoSincronizacaoScanntech>((object)_modulo))
				{
					return new GestaoSincronizacaoScanntechItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GerenciamentoRotinaDiaria>((object)_modulo))
				{
					return new GerenciamentoRotinaDiariaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoCashback>((object)_modulo))
				{
					return new GestaoCashbackItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GerenciadorSenha>((object)_modulo))
				{
					return new GerenciadorSenhaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GerenciadorTicket>((object)_modulo))
				{
					return new GerenciadorTicketItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoSincronizacaoHospedagemFNRH>((object)_modulo))
				{
					return new GestaoSincronizacaoHospedagemFNRHItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoSincronizacaoRodoilPro>((object)_modulo))
				{
					return new GestaoSincronizacaoRodoilProItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoSincronizacaoClubeConstrudata>((object)_modulo))
				{
					return new GestaoSincronizacaoClubeConstrudataItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AlteracoesPrecoAgendadas>((object)_modulo))
				{
					return new AlteracoesPrecoAgendadasItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoCodigoIdentificadorOperacaoTransporte>((object)_modulo))
				{
					return new GestaoCodigoIdentificadorOperacaoTransporteItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(Industria))
			{
				if (HelperObject.Is<JornadaTrabalho>((object)_modulo))
				{
					return new JornadaTrabalhoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Feriado>((object)_modulo))
				{
					return new FeriadoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Custo>((object)_modulo))
				{
					return new CustoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<EncargoSocial>((object)_modulo))
				{
					return new EncargoSocialItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<MaoObra>((object)_modulo))
				{
					return new MaoObraItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<CustoIndireto>((object)_modulo))
				{
					return new CustoIndiretoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<SimuladorCusto>((object)_modulo))
				{
					return new SimuladorCustoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<FaseProducao>((object)_modulo))
				{
					return new FaseProducaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OrcamentoIndustria>((object)_modulo))
				{
					return new OrcamentoIndustriaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<EstoqueFisico>((object)_modulo))
				{
					return new EstoqueFisicoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OrdemProducao>((object)_modulo))
				{
					return new OrdemProducaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<OrdemDesmontagem>((object)_modulo))
				{
					return new OrdemDesmontagemItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ProgramacaoIndustria>((object)_modulo))
				{
					return new ProgramacaoIndustriaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<AtualizacaoCustoMateriaPrima>((object)_modulo))
				{
					return new AtualizacaoCustoMateriaPrimaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<SubstituicaoMateriaPrima>((object)_modulo))
				{
					return new SubstituicaoMateriaPrimaItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			if (((object)_grupo).GetType() == typeof(ArquivosFiscaisEletronicos))
			{
				if (HelperObject.Is<GeradorArquivoSpedFiscal>((object)_modulo))
				{
					return new GeradorArquivoSpedFiscalItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoEfdContribuicao>((object)_modulo))
				{
					return new GeradorArquivoEfdContribuicaoItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoSintegra>((object)_modulo))
				{
					return new GeradorArquivoSintegraItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoLivroCaixaDigitalProdutorRural>((object)_modulo) && DadosSistema.EmpresaLogada.IsProdutorRural)
				{
					return new GeradorArquivoLivroCaixaDigitalProdutorRuralItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoContabilidade>((object)_modulo))
				{
					return new GeradorArquivoContabilidadeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<ExportarArquivoContabilidade>((object)_modulo))
				{
					return new ExportarArquivoContabilidadeItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GeradorArquivoInventario>((object)_modulo))
				{
					return new GeradorArquivoInventarioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<Sngpc>((object)_modulo))
				{
					return new SngpcItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<LivroMovimentacaoCombustivel>((object)_modulo))
				{
					return new LivroMovimentacaoCombustivelItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoPainelContabil>((object)_modulo))
				{
					return new GestaoPainelContabilItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
				if (HelperObject.Is<GestaoSincronizacaoDominio>((object)_modulo))
				{
					return new GestaoSincronizacaoDominioItemMenuPrincipal(_grupo, _tileService, _acessoRapidoService);
				}
			}
			return null;
		}
	}
	public class JornadaTrabalhoItemMenuPrincipal : ItemMenuPrincipal
	{
		public JornadaTrabalhoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeJornadaTrabalho(), "Jornadas de trabalho", "JornadaTrabalhoModulo", "JornadasTrabalhoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class KitItemMenuPrincipal : ItemMenuPrincipal
	{
		public KitItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeKit(), "Montagem de Kits", "KitModulo", "KitsView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class LivroMovimentacaoCombustivelItemMenuPrincipal : ItemMenuPrincipal
	{
		public LivroMovimentacaoCombustivelItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeLivroMovimentacaoCombustivel(), "LMC", "LivroMovimentacaoCombustivelModulo", "LivrosMovimentacaoCombustivelView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ManifestacaoDestinatarioItemMenuPrincipal : ItemMenuPrincipal
	{
		public ManifestacaoDestinatarioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeManifestacaoDestinatario(), "Manifestação do destinatário", "ManifestacaoDestinatarioModulo", "ManifestacoesDestinatarioView", tilesService, acessoRapidoService)
		{
			base.OpcoesBusca = new List<string> { "compras", "importação", "importar nota fiscal", "importar xml" };
			AdicionarItemAcessoRapido();
		}
	}
	public class ManifestoEletronicoDocumentoFiscalItemMenuPrincipal : ItemMenuPrincipal
	{
		public ManifestoEletronicoDocumentoFiscalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeMDFe(), "MDF-e", "ManifestoEletronicoDocumentoFiscalModulo", "ManifestosEletronicoDocumentoFiscalView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MaoObraItemMenuPrincipal : ItemMenuPrincipal
	{
		public MaoObraItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMaoObra(), "Mão de obra", "MaoObraModulo", "MaosObraView", tilesService, acessoRapidoService)
		{
		}
	}
	public class MapaMesaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MapaMesaItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMapaMesa(), "Mapa de mesas", "MapaMesaModulo", "MapaMesasView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class MarcaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MarcaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeMarca(), "Marcas", "MarcaModulo", "MarcasView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MensagemItemMenuPrincipal : ItemMenuPrincipal
	{
		public MensagemItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeMensagem(), "Notificações", "MensagemModulo", "MensagensView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MercadoLivreItemMenuPrincipal : ItemMenuPrincipal
	{
		public MercadoLivreItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMercadoLivre(), "Autenticação Mercado Livre", "MercadoLivreModulo", "MercadosLivresView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class MoedaMenuPrincipal : ItemMenuPrincipal
	{
		public MoedaMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMoeda(), "Moedas", "MoedasModulo", "MoedasView", tilesService, acessoRapidoService)
		{
		}
	}
	public class MontagemComposicaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public MontagemComposicaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeComposicao(), "Composições", "ComposicaoModulo", "ComposicoesView", tilesService, acessoRapidoService)
		{
		}
	}
	public class MovimentacaoCtrcItemMenuPrincipal : ItemMenuPrincipal
	{
		public MovimentacaoCtrcItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeCtrc(), "CTRC", "MovimentacaoCtrcModulo", "MovimentacoesCtrcView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class MovimentacaoEntradaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MovimentacaoEntradaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Compras\nPedido compra\nE mais..."), (IIcone)new IconeMovimentacaoEntrada(), "Movimentações de entrada", "MovimentacaoModulo", "MovimentacoesEntradaView", tilesService, acessoRapidoService)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			base.OpcoesBusca = new List<string> { "nf-e", "pedido de compra", "nota fiscal eletrônica", "compras", "importação", "importar xml" };
			AdicionarItemAcessoRapido();
		}
	}
	public class OpcaoNotificacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public OpcaoNotificacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeOpcaoNotificacao(), "Opções de notificação ao solicitar garçom", "OpcaoNotificacaoModulo", "OpcoesNotificacaoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class OrcamentoIndustriaItemMenuPrincipal : ItemMenuPrincipal
	{
		public OrcamentoIndustriaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Produtos com|ficha técnica|personalizada|E mais..."), (IIcone)(object)new IconeOrcamentoIndustria(), "Orçamentos", "OrcamentoIndustriaModulo", "OrcamentosIndustriaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class OrdemProducaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public OrdemProducaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Produção|Controle de etapas|Desmontar|E mais..."), (IIcone)(object)new IconeOrdemProducao(), "Ordens de Produção", "OrdemProducaoModulo", "OrdensProducaoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class OrdemServicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public OrdemServicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeOrdemServico(), "Ordens de serviço", "OrdemServicoModulo", "OrdensServicoView", tilesServicer, acessoRapidoService)
		{
		}
	}
	public class PainelPedidosItemMenuPrincipal : ItemMenuPrincipal
	{
		public PainelPedidosItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePainelPedidosRestaurante(), "Painel de pedidos", "PainelPedidoModulo", "PainelPedidoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class PedidoIfoodItemMenuPrincipal : ItemMenuPrincipal
	{
		public PedidoIfoodItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePedidoIfood(), "Pedidos do iFood", "PedidoIfoodModulo", "PedidosIfoodView", tilesServicer, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class PedidoMercadoLivreItemMenuPrincipal : ItemMenuPrincipal
	{
		public PedidoMercadoLivreItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeMercadoLivre(), "Pedidos do Mercado Livre", "PedidoMercadoLivreModulo", "PedidosMercadoLivreView", tilesService, acessoRapidoService)
		{
		}
	}
	public class PedidoEcommerceItemMenuPrincipal : ItemMenuPrincipal
	{
		public PedidoEcommerceItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconePedidoEcommerce(), "Pedidos e-commerce", "PedidoEcommerceModulo", "PedidosEcommerceView", tilesService, acessoRapidoService, "PedidoEcommerceListaView")
		{
		}
	}
	public class PontoDeVendaItemMenuPrincipal : ItemMenuPrincipal
	{
		public PontoDeVendaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario(), (IIcone)(object)new IconePontoDeVenda(), "Ponto de venda", "PontoDeVendaModulo", "PontoDeVendasView", tilesService, acessoRapidoService)
		{
			base.OpcoesBusca = new List<string> { "Cupom fiscal", "PAF", "ecf", "sat", "nota manual", "contingência", "NFC-e", "pdv" };
			AdicionarItemAcessoRapido();
		}
	}
	public class ProgramacaoIndustriaItemMenuPrincipal : ItemMenuPrincipal
	{
		public ProgramacaoIndustriaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Timeline|Interrupções|E mais..."), (IIcone)(object)new IconeProgramacaoIndustria(), "Programação de produção", "ProgramacaoIndustriaModulo", "ProgramacoesIndustriaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class RelatorioItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelatorioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Movimentações|Financeiro|E mais..."), (IIcone)(object)new IconeRelatorio(), "Relatórios", "RelatorioModulo", "RelatoriosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class RenegociacaoECobrancaItemMenuPrincipal : ItemMenuPrincipal
	{
		public RenegociacaoECobrancaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRenegociacaoECobranca(), "Renegociação de parcelas e cobranças", "RenegociacaoECobrancaModulo", "RenegociacaoECobrancasView", tilesService, acessoRapidoService, "RenegociacaoECobrancaFichaView")
		{
		}
	}
	public class RenegociacaoPagamentoItemMenuPrincipal : ItemMenuPrincipal
	{
		public RenegociacaoPagamentoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRenegociacaoPagamento(), "Renegociação de pagamentos", "RenegociacaoPagamentoModulo", "RenegociacaoPagamentosView", tilesService, acessoRapidoService, "RenegociacaoPagamentoFichaView")
		{
		}
	}
	public class RestauranteItemMenuPrincipal : ItemMenuPrincipal
	{
		public RestauranteItemMenuPrincipal(IGrupo grupo, ITilesService tilesServicer, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeRestaurante(), "Restaurante", "RestauranteModulo", "RestaurantesView", tilesServicer, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	internal class MovimentacaoSaidaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MovimentacaoSaidaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario(DadosSistema.Sistema.IsComercial ? "Fatura" : "Fatura\nDAV\nDAV-OS\nE mais..."), (IIcone)(object)new IconeSaida(), "Movimentações de saída", "MovimentacaoModulo", "MovimentacoesSaidaView", tilesService, acessoRapidoService)
		{
			base.OpcoesBusca = new List<string> { "nf-e", "dav", "nota manual", "dav-os", "pré-venda", "pedido de venda", "orçamento", "nota fiscal eletrônica", "nfs-e" };
			AdicionarItemAcessoRapido();
		}
	}
	public class MovimentoContaItemMenuPrincipal : ItemMenuPrincipal
	{
		public MovimentoContaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Movimentações|Saldos|E mais..."), (IIcone)new IconeMovimentoConta(), "Movimentações da conta", "MovimentoContaModulo", "MovimentosContaView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class ObjetoItemMenuPrincipal : ItemMenuPrincipal
	{
		public ObjetoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeObjeto(), "Objetos", "ObjetoModulo", "ObjetosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class PagamentoItemMenuPrincipal : ItemMenuPrincipal
	{
		public PagamentoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Incluir|Editar|Quitar"), (IIcone)new IconePagamento(), "Contas a pagar", "PagamentoModulo", "PagamentosView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class PerfilItemMenuPrincipal : ItemMenuPrincipal
	{
		public PerfilItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeAuxiliar(), "Perfis do usuário", "PerfilModulo", "PerfisView", tilesService, acessoRapidoService, "PerfilListaView")
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class PessoaItemMenuPrincipal : ItemMenuPrincipal
	{
		public PessoaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Clientes|Fornecedores|Transportadoras|E mais..."), (IIcone)new IconePessoa(), "Pessoas", "PessoaModulo", "PessoasView", tilesService, acessoRapidoService)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			base.OpcoesBusca = new List<string> { "Cliente", "Clientes", "Fornecedor", "Fornecedores", "Transportadoras", "Contabilista", "Entregador", "Frentista", "Vendedor" };
			AdicionarItemAcessoRapido();
		}
	}
	public class PlanoContaItemMenuPrincipal : ItemMenuPrincipal
	{
		public PlanoContaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconePlanoConta(), "Plano de contas", "PlanoContaModulo", "PlanosContaView", tilesService, acessoRapidoService)
		{
		}
	}
	public class ProdutoServicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public ProdutoServicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Produtos|Serviços|E mais..."), (IIcone)new IconeProdutoServico(), "Produtos ou Serviços", "ProdutoServicoModulo", "ProdutosServicosView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class PromocaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public PromocaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePromocao(), "Promoções", "PromocaoModulo", "PromocoesView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class RecebimentoItemMenuPrincipal : ItemMenuPrincipal
	{
		public RecebimentoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Primario("Quitar|Boletos|Carnês|E mais..."), (IIcone)new IconeRecebimento(), "Contas a receber", "RecebimentoModulo", "RecebimentosView", tilesService, acessoRapidoService)
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown

	}
	public class RelacaoVendedorClienteItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoVendedorClienteItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeRelacaoVendedorCliente(), "Relação Vendedor Clientes", "RelacaoVendedorClienteModulo", "RelacoesVendedorClienteView", tilesService, acessoRapidoService, "RelacaoVendedorClienteListaView")
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class RelacaoProdutoFornecedorItemMenuPrincipal : ItemMenuPrincipal
	{
		public RelacaoProdutoFornecedorItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeRelacaoProdutoPessoa(), "Relação Produtos Fornecedor", "RelacaoProdutoFornecedorModulo", "RelacoesProdutoFornecedorView", tilesService, acessoRapidoService)
		{
		}
	}
	public class RomaneioCargaItemMenuPrincipal : ItemMenuPrincipal
	{
		public RomaneioCargaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeRomaneioCarga(), "Romaneio de cargas", "RomaneioCargaModulo", "RomaneiosCargaView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class SimuladorCustoItemMenuPrincipal : ItemMenuPrincipal
	{
		public SimuladorCustoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeSimuladorCusto(), "Simulador de Custos", "SimuladorCustoModulo", "SimuladorCustosView", tilesService, acessoRapidoService)
		{
		}
	}
	public class SincronizacaoEcommerceItemMenuPrincipal : ItemMenuPrincipal
	{
		public SincronizacaoEcommerceItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeSincronizacaoEcommerce(), "Sincronização e-commerce", "SincronizacaoEcommerceModulo", "SincronizacoesEcommerceView", tilesService, acessoRapidoService)
		{
		}
	}
	public class SngpcItemMenuPrincipal : ItemMenuPrincipal
	{
		public SngpcItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeSngpc(), "SNGPC", "SngpcModulo", "SngpcsView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TabelaPrecoItemMenuPrincipal : ItemMenuPrincipal
	{
		public TabelaPrecoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTabelaPreco(), "Tabelas de preço", "TabelaPrecoModulo", "TabelasPrecoView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TanqueBombaBicoItemMenuPrincipal : ItemMenuPrincipal
	{
		public TanqueBombaBicoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeTanqueBombaBico(), "Tanques/Bombas/Bicos", "TanqueBombaBicoModulo", "TanquesBombasBicosView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TipoPersonalizadoItemMenuPrincipal : ItemMenuPrincipal
	{
		public TipoPersonalizadoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeCamposPersonalizados(), "Campos personalizados", "TipoPersonalizadoModulo", "TiposPersonalizadoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TipoSegmentacaoItemMenuPrincipal : ItemMenuPrincipal
	{
		public TipoSegmentacaoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeTipoSegmentacao(), "Tipos de segmentação", "TipoSegmentacaoModulo", "TiposSegmentacaoView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TransacaoPbmItemMenuPrincipal : ItemMenuPrincipal
	{
		public TransacaoPbmItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTransacaoPbm(), "Transações PBM", "TransacaoPbmModulo", "TransacoesPbmView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TransferenciaItemMenuPrincipal : ItemMenuPrincipal
	{
		public TransferenciaItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeTransferencia(), "Transferências entre contas", "TransferenciaModulo", "TransferenciasView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TributacaoIsItemMenuPrincipal : ItemMenuPrincipal
	{
		public TributacaoIsItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeReformaTributaria(), "Tributações IS", "TributacaoIsModulo", "TributacoesIsView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TributacaoIbsCbsItemMenuPrincipal : ItemMenuPrincipal
	{
		public TributacaoIbsCbsItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeReformaTributaria(), "Tributações IBS/CBS", "TributacaoIbsCbsModulo", "TributacoesIbsCbsView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TributacaoEstadualItemMenuPrincipal : ItemMenuPrincipal
	{
		public TributacaoEstadualItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTributacaoEstadual(), "Tributações estaduais", "TributacaoEstadualModulo", "TributacoesEstaduaisView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TributacaoFederalItemMenuPrincipal : ItemMenuPrincipal
	{
		public TributacaoFederalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTributacaoFederal(), "Tributações federais", "TributacaoFederalModulo", "TributacoesFederaisView", tilesService, acessoRapidoService)
		{
		}
	}
	public class OperacoesFiscaisItemMenuPrincipal : ItemMenuPrincipal
	{
		public OperacoesFiscaisItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeCfop(), "Operações fiscais", "OperacaoFiscalModulo", "OperacoesFiscaisView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class TributacaoMunicipalItemMenuPrincipal : ItemMenuPrincipal
	{
		public TributacaoMunicipalItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeTributacaoMunicipal(), "Tributações municipais", "TributacaoMunicipalModulo", "TributacoesMunicipaisView", tilesService, acessoRapidoService)
		{
		}
	}
	public class TurnoItemMenuPrincipal : ItemMenuPrincipal
	{
		public TurnoItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconeTurno(), "Turnos", "TurnoModulo", "TurnosView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class UsuarioItemMenuPrincipal : ItemMenuPrincipal
	{
		public UsuarioItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)new IconePessoa(), "Usuários", "UsuarioModulo", "UsuariosView", tilesService, acessoRapidoService)
		{
		}//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown

	}
	public class ValePresenteItemMenuPrincipal : ItemMenuPrincipal
	{
		public ValePresenteItemMenuPrincipal(IGrupo grupo, ITilesService tilesService, IAcessoRapidoService acessoRapidoService)
			: base(grupo, (IClassificacao)(object)new Secundario(), (IIcone)(object)new IconeValePresente(), "Vales presente", "ValePresenteModulo", "ValesPresenteView", tilesService, acessoRapidoService, "ValePresenteListaView")
		{
		}
	}
}
namespace Otus.Modulo.Menus.Models.Icones
{
	public class IconeCotacao : IIcone
	{
		public string Data => "M 376 0 L 624 0 C 656.03 0 682 25.97 682 58 L 682 264 C 682 296.03 656.03 322 624 322 L 538 322 L 498 408 L 462 322 L 376 322 C 343.97 322 318 296.03 318 264 L 318 58 C 318 25.97 343.97 0 376 0 Z M 490.78 52 L 508.73 52 L 508.73 73.1 C 526.02 73.87 538.93 76.5 547.46 80.98 L 547.46 112.27 C 535.98 105.71 523.07 101.69 508.73 100.22 L 490.78 100.68 C 479.39 102.61 473.69 108.22 473.69 117.49 C 473.69 125.45 479.39 132.09 490.78 137.43 L 508.73 144.15 C 526.68 150.25 539.55 157.23 547.34 165.07 C 555.12 172.91 559.02 182.4 559.02 193.53 C 559.02 206.35 554.67 216.69 545.98 224.53 C 537.3 232.38 524.88 237.15 508.73 238.85 L 508.73 264 L 490.78 264 L 490.78 239.54 C 474.14 239.47 458.65 235.91 444.3 228.88 L 444.3 196.77 C 449.06 200.4 456.15 203.77 465.57 206.86 C 475 209.95 483.4 211.72 490.78 212.19 L 509.81 211.55 C 520.81 209.68 526.31 204.21 526.31 195.15 C 526.31 187.58 520.45 181.39 508.73 176.6 L 508.73 176.72 L 490.82 169.92 L 490.78 170 C 471.6 163.27 458.5 155.99 451.5 148.15 C 444.49 140.3 440.98 130.74 440.98 119.46 C 440.98 107.33 445.53 96.99 454.63 88.45 C 463.73 79.92 475.78 74.95 490.78 73.56 Z M 50 72 L 266 72 C 292.51 72 314 93.49 314 120 L 314 292 C 314 318.51 292.51 340 266 340 L 162 340 L 130 424 L 98 340 L 50 340 C 23.49 340 2 318.51 2 292 L 2 120 C 2 93.49 23.49 72 50 72 Z M 150.43 116 L 165.16 116 L 165.16 133.31 C 179.36 133.95 189.96 136.1 196.95 139.78 L 196.95 165.47 C 187.53 160.08 176.94 156.78 165.16 155.58 L 150.43 155.96 C 141.08 157.54 136.4 162.14 136.4 169.75 C 136.4 176.28 141.08 181.74 150.43 186.11 L 165.16 191.63 C 179.9 196.64 190.46 202.37 196.85 208.8 C 203.24 215.24 206.44 223.03 206.44 232.16 C 206.44 242.69 202.87 251.17 195.74 257.61 C 188.61 264.04 178.42 267.96 165.16 269.36 L 165.16 290 L 150.43 290 L 150.43 269.93 C 136.77 269.86 124.06 266.95 112.29 261.17 L 112.29 234.82 C 116.19 237.8 122.01 240.56 129.74 243.1 C 137.48 245.64 144.38 247.09 150.43 247.48 L 166.05 246.95 C 175.08 245.41 179.6 240.92 179.6 233.49 C 179.6 227.28 174.79 222.2 165.16 218.27 L 165.16 218.37 L 150.47 212.78 L 150.43 212.85 C 134.69 207.33 123.94 201.35 118.19 194.91 C 112.44 188.48 109.56 180.63 109.56 171.37 C 109.56 161.41 113.3 152.93 120.76 145.92 C 128.23 138.91 138.12 134.84 150.43 133.69 Z M 734 72 L 950 72 C 976.51 72 998 93.49 998 120 L 998 292 C 998 318.51 976.51 340 950 340 L 846 340 L 870 424 L 782 340 L 734 340 C 707.49 340 686 318.51 686 292 L 686 120 C 686 93.49 707.49 72 734 72 Z M 834.43 116 L 849.16 116 L 849.16 133.31 C 863.36 133.95 873.96 136.1 880.95 139.78 L 880.95 165.47 C 871.53 160.08 860.94 156.78 849.16 155.58 L 834.43 155.96 C 825.08 157.54 820.4 162.14 820.4 169.75 C 820.4 176.28 825.08 181.74 834.43 186.11 L 849.16 191.63 C 863.9 196.64 874.46 202.37 880.85 208.8 C 887.24 215.24 890.44 223.03 890.44 232.16 C 890.44 242.69 886.87 251.17 879.74 257.61 C 872.61 264.04 862.42 267.96 849.16 269.36 L 849.16 290 L 834.43 290 L 834.43 269.93 C 820.77 269.86 808.06 266.95 796.29 261.17 L 796.29 234.82 C 800.19 237.8 806.01 240.56 813.74 243.1 C 821.48 245.64 828.38 247.09 834.43 247.48 L 850.05 246.95 C 859.08 245.41 863.6 240.92 863.6 233.49 C 863.6 227.28 858.79 222.2 849.16 218.27 L 849.16 218.37 L 834.47 212.78 L 834.43 212.85 C 818.69 207.33 807.94 201.35 802.19 194.91 C 796.44 188.48 793.56 180.63 793.56 171.37 C 793.56 161.41 797.3 152.93 804.76 145.92 C 812.23 138.91 822.12 134.84 834.43 133.69 Z M 148 548 C 180.94 548 207.64 574.7 207.64 607.64 C 207.64 640.58 180.94 667.28 148 667.28 C 115.06 667.28 88.36 640.58 88.36 607.64 C 88.36 574.7 115.06 548 148 548 Z M 125.28 698.92 L 170.72 698.92 C 236.6 698.92 290 752.32 290 818.2 L 290 1000 L 6 1000 L 6 818.2 C 6 752.32 59.4 698.92 125.28 698.92 Z M 500 548 C 532.94 548 559.64 574.7 559.64 607.64 C 559.64 640.58 532.94 667.28 500 667.28 C 467.06 667.28 440.36 640.58 440.36 607.64 C 440.36 574.7 467.06 548 500 548 Z M 477.28 698.92 L 522.72 698.92 C 588.6 698.92 642 752.32 642 818.2 L 642 1000 L 358 1000 L 358 818.2 C 358 752.32 411.4 698.92 477.28 698.92 Z M 852 548 C 884.94 548 911.64 574.7 911.64 607.64 C 911.64 640.58 884.94 667.28 852 667.28 C 819.06 667.28 792.36 640.58 792.36 607.64 C 792.36 574.7 819.06 548 852 548 Z M 829.28 698.92 L 874.72 698.92 C 940.6 698.92 994 752.32 994 818.2 L 994 1000 L 710 1000 L 710 818.2 C 710 752.32 763.4 698.92 829.28 698.92 Z";
	}
	public class IconeAtualizacaoGrupoComplemento : IIcone
	{
		public string Data => "M1086818 809741c195416,0 353829,158413 353829,353829 0,195406 -158413,353818 -353829,353818 -195406,0 -353818,-158413 -353818,-353818 0,-195416 158413,-353829 353818,-353829zm-948394 455446c183361,0 366660,145 550022,311 7001,27465 16798,53823 29019,78761l-579040 0 101442 110918 -51130 55904 -188736 -206363 188736 -206363 51140 55904 -101452 110928zm1159458 -454483l0 -397492 78398 -85720 10 544150c-23520,-23323 -49835,-43808 -78409,-60937zm-1103554 -469697l150107 0 0 94192 47091 0 0 -94192 150096 0 0 320802 -347294 0 0 -320802zm406740 374272l150107 0 0 94182 47091 0 0 -94182 150096 0 0 61082c-119296,42669 -213105,138901 -252480,259719l-94813 0 0 -320802zm-406740 0l150107 0 0 94182 47091 0 0 -94182 150096 0 0 320802 -347294 0 0 -320802zm-115930 -469987l0 834327 -78398 85710 0 -998436c412653,0 825255,-62 1237867,-62l-101452 -110918 51140 -55915 188736 206373 -188736 206353 -51130 -55904 101442 -110918c-386389,0 -772561,-611 -1159469,-611zm1146243 980095c1812,17482 311,33514 -4464,48209 -16135,50115 -63796,75209 -112875,84654l0 58017 -57302 0 0 -52818c-17730,-1170 -86508,-15814 -86456,-28149l197 -64252c0,-994 54403,24969 73355,27952 37843,6442 111094,-2651 105626,-55335 -4277,-41198 -63019,-47557 -94617,-56422 -54806,-15141 -92825,-38982 -99153,-100230 -7084,-68021 39261,-106692 101048,-120570l0 -55583 57302 0 0 50488c14758,673 82738,12666 83152,27900l1025 58576c-186,-10553 -57137,-27724 -108070,-25042 -28335,1491 -58265,14240 -54775,47650 4205,40887 63951,47857 95984,56505 53439,14530 93643,37024 100023,98449z";
	}
	public class IconeShopee : IIcone
	{
		public string Data => "M-0 13201.8l12817.7 0c0,-6687.2 4778.3,-13201.8 11841.7,-13201.8 7063.4,0 11841.8,6514.6 11841.8,13201.8l12818 0 -2011.6 37653.2c-143.5,2694.7 -2209.2,4907.6 -4907.6,4907.6l-35480.8 0c-2697.9,0 -4764.1,-2212.4 -4907.6,-4907.6l-2011.6 -37653.2zm24402.4 32320.6c4764.7,-161.5 7436.1,-4792.6 4098.9,-7823.9 -3205.6,-2912 -9367.1,-1972 -11248.6,-7516.7 -2626.2,-7739 8610.5,-12607.2 14985.7,-7605.9l-1372.3 2526.8c-3365.9,-2092.8 -10444.8,-2563.8 -10582.6,2554.1 -173.9,6432.7 12287.8,3484.4 12904.3,12348.9 323.4,4649.4 -2658.1,7528.1 -7035.9,8362.7 -3280.3,625 -6997.3,188.3 -10044.9,-2293.5l1047.1 -2637c2871.8,1405.9 3864.2,2199.1 7248.4,2084.6zm-9573.5 -32320.6l19661 0c0,-5568.7 -3890.4,-11190.5 -9830.5,-11190.5 -5940.1,0 -9830.5,5621.8 -9830.5,11190.5z";
	}
	public class IconeTributacaoMunicipal : IIcone
	{
		public string Data => "M1367.8 2728.6c0,7.6 -0.8,17.9 -2.2,30.7 -1.5,12.8 -3.2,26.3 -5.4,39.9 -2.2,13.8 -4.5,27.3 -6.9,40.3 -2.2,13.1 -4.4,24.1 -6.4,33.5l-90.1 454.5 -228.6 0 90 -448.5c2.9,-16.3 6,-33.3 8.6,-50.6 3,-17.6 4.5,-30.9 4.5,-40.1 0,-25.4 -5.6,-45.3 -17.3,-59.5 -11.4,-13.9 -29.3,-21 -53.3,-21 -18.3,0 -37.6,6.5 -58.4,19.9 -20.6,13.3 -40.3,30.7 -58.7,52.2 -18.4,21.5 -34.9,46.1 -49,73.6 -14.3,27.8 -24.1,56.1 -30,85.5l-75.8 388.5 -229.8 0 90.1 -448.5c2.9,-16.3 5.9,-33.3 8.6,-50.6 3,-17.6 4.5,-30.9 4.5,-40.1 0,-25.4 -5.9,-45.3 -17.7,-59.5 -11.8,-13.9 -29.5,-21 -52.9,-21 -18.7,0 -38.6,6.5 -59.1,19.9 -20.6,13.3 -40.4,30.9 -58.6,52.7 -18.2,21.7 -34.2,46.4 -48.2,73.9 -14,27.7 -23.8,55.8 -29.5,84.6l-78 388.5 -230.5 0 109.5 -539.2c4.4,-23.6 8.6,-47.2 13,-71 4.4,-23.6 8.1,-46.4 11.6,-68.2 3.1,-21.7 6.3,-42.2 8.9,-60.9 2.6,-18.6 4.6,-34.3 6.1,-47.1l224.8 0c-1.2,14.4 -2.9,29.4 -4.7,44.9 -1.7,15.6 -3.1,30.6 -4.9,45 -1.7,14.2 -3.2,27 -4.7,38 -1.5,11 -2.7,18.9 -3.7,23.3l3 0c17.8,-24.6 37.1,-47.1 58,-67.9 21,-20.7 42.9,-38.8 65.9,-53.7 23.2,-15.2 46.8,-27 71.4,-35.7 24.3,-8.9 49,-13.1 73.5,-13.1 57.6,0 101.1,15.7 130.4,47.5 29.3,31.9 46.2,75.8 51.2,132.1 18.2,-26.4 38.1,-50.9 59.7,-72.9 21.7,-22 44.8,-40.9 69.2,-56.9 24.6,-15.8 49.9,-28.1 76.6,-36.7 26.2,-8.9 53.3,-13.1 80.6,-13.1 61,0 107,17.8 138,53.2 31,35.7 46.5,86.8 46.5,153.3zm1971.7 -1308.4c17.7,-17.5 29,-27.1 49,-48.4 19.4,-20.7 24.9,-32.6 48.2,-56 21.2,-21.3 26.7,-27.7 35.4,-68.2 15.4,-74 21.5,-96.7 -0.5,-173.9 -16.6,-58.3 -54.2,-80.1 -110.9,-96.2 -95.2,-27.1 -147.7,-101.7 -242.3,-167 -48.5,-33.2 -67.9,-63.8 -144.2,-58 -70.5,5.3 -109.4,-4.9 -173.5,-25.9 -27.9,-9 -54,-23 -83.3,-16.4 -75.7,17.8 -54.2,55.7 -106.2,-92.6l-44.7 7.6c-6.1,-1.2 -84.7,-69.6 -177.3,-91.5 -42.1,-9.9 -75.4,-0.3 -95.5,37.3 -18,33.2 -43.2,42.5 -88.5,37.6l61 -118.6 -166.8 -5.5 -29.6 76.7 -41.8 0.9 35.7 -123.8c31.9,-30.3 55.7,-55.3 75.2,-110.2 -35.1,-28.5 -68.8,-48.4 -79.8,-104.3 -6.9,-34.4 9.3,-85.7 -56.6,-129.4l-67 80.5c-11.1,46.3 -34.2,52.2 -54.2,85.7l-92 -0.6c-38.9,6.4 -29,17.2 -67,-4.7 -74.8,-42.8 -24.4,-16 -124.7,-1.7l11.3 52.2 -182.2 13.7c-65,32.6 -112.9,36.1 -163.7,-7 -36.6,-31.2 -44.1,-116.8 -22.7,-168.1 32.8,-77.2 -12.5,-78.4 -26.7,-103.4 -15.1,-26.2 0.9,-16.6 -8.1,-41.1l-117.5 70.8c-83.6,26.6 -71.4,15.2 -112.3,74.3l-154.9 -37.3c49.6,107.2 94.3,133.7 107.6,174.8 -108.8,79.8 -302.6,222 -361,21l-55.1 41.1 -19.1 -25.4 -139.6 37.9c25.3,28.3 51.1,48.7 54.6,78.1l-73.4 16 -12.2 56.5 56 182.9c-16.5,70.5 -33.4,147.1 -51.6,213.8 -143.6,-8.5 -77.2,-17.8 -181.9,40.2 -95.2,52.7 -73.1,52.2 -64.8,169.3 -52,29.7 -115.7,49.2 -56.6,111.6 65.6,69.4 79.2,81.6 65.3,128.8l60.6 5.5 14.5 44 142.2 -52.2c-3.8,112.2 -12.8,95.3 24.9,137.8l65.6 -1.7c24.1,4.3 44.4,9.3 70.5,11.4l117.5 -50.1c22.9,-17.5 31.9,-62 72.8,-74.8 28.2,-8.8 86.5,-24.2 113.1,-26.2 5.5,146.6 -11.6,126.8 62.7,209.8 53.1,59.1 54,18 98.7,26.5 15.7,2.9 57.4,48.7 76,58.6 67.6,35.9 147.1,12.6 202,70.3 19.4,20.4 25.3,83.6 16.3,109.5 -9.9,28.5 -13,9.3 -2.1,51.5 4.7,18.1 8.7,32.1 16.3,49.6l158.4 0.6 3.8 76 70 87.1c10.4,34.1 -12.5,79.3 -24.1,114 -22.3,67.3 -23.5,52.5 0,118.3 18.3,51.5 5.2,75.1 6.1,128.5l178.4 17.8 31.3 143.3 65.3 -16 4.3 161.4 6.4 124.4c-6.9,42 38.9,30.9 10.2,49.9 -63.2,41.6 -229,189.7 -237.7,241.5 80.4,37.6 51.4,16.4 99.3,74.6l46.1 -21.6 37.7 57.4c11.1,11.1 128.9,68.4 143.3,123.5l-35.1 37.3 8.7 44.3 47 -50.4c34,-48.4 40,-85.9 87,-108.4 33.1,-15.7 36.6,-12.8 55.1,-41.3 24.1,-37 54.6,-78.7 76,-119.2 30.8,-58.3 30.5,-79 72,-116.9 25.8,-23.6 30.2,-18.3 39.5,-58 6.9,-30.3 13.9,-46.3 7.8,-77.2 -7.8,-41.1 -7.8,-121.8 28.1,-146.8 31,-21.8 29.9,-17.2 54.2,-43.1 22,-23.3 26.4,-25.9 52.5,-44.9 46.2,-33.5 59.2,-59.7 130.1,-60.9l55.7 -45.8c27.6,-12.3 42.4,-9.3 72.2,-25 43.8,-23.3 97.8,11.4 170.3,0l42.4 -46.3c20.6,-11.4 42.4,-20.1 57.5,-31.2 14.5,-22.7 -10.5,-59.4 61.8,-148 105.3,-128.8 39.2,-63.5 58.9,-141.6 3.1,-12.8 30.7,-47.2 40.6,-64.4 14.2,-24.8 18.3,-46.1 28.2,-75.8 15.9,-47.8 29.3,-116.2 31.3,-167.2 4.7,-125.3 -39.5,-184.1 73.7,-229.3 57.2,-22.8 37.4,-62.9 137,-168.1 19.4,-20.7 26.7,-35 48.2,-56l-0.7 -0.3zm-1612.3 -419.4c53.6,-75.2 128.8,-110.7 220.7,-110.7 141.3,0 216.3,99.2 216.3,234.4 0,110.1 -48.9,248.8 -146.1,309.4 -46.3,28.9 -97.4,42.5 -152,42.5 -59.9,0 -114,-18 -156.3,-61.2 -103.2,-105.3 -59.7,-306.4 17.4,-414.5zm162.6 1038.2l-209.1 0 904.9 -1148.9 209.4 0 -905.2 1148.9zm716.1 -587.8c60.6,0 115.2,18.2 157.3,62.6 115.9,122.5 49.2,397.5 -87.2,481.7 -46.3,28.6 -97.5,42 -151.9,42 -59.9,0 -114.1,-18 -156.4,-61.2 -147.4,-150.4 -22.8,-525.2 238.1,-525.2zm-728.8 -130.7c35.8,0 58.4,-39.2 71.1,-67.6 17.6,-39.3 72.7,-206.8 -9.2,-206.8 -51.3,0 -71.6,69.8 -83.3,109.2 -10.9,36.8 -51.8,165.2 21.3,165.2zm658.5 561.1c35.7,0 58.1,-39.1 70.9,-67.3 22.4,-49.7 33.3,-102.2 33.3,-156.8 0,-28 -11.8,-50.3 -42.4,-50.3 -50.9,0 -71.6,70.2 -83.3,109.3 -11.9,39.8 -51.7,165.1 21.6,165.1z";
	}
	public class IconeTributacaoFederal : IIcone
	{
		public string Data => "M6426.5 11975.3c-184,-90.1 -390.3,-134.9 -616.1,-134.9 -255.8,0 -451.1,77.2 -588.7,229.6 -137.4,152.9 -241.5,388.9 -307.3,705.1l-147.3 690.8 1177.8 0 -203.3 930.1 -1193.7 0 -940.7 4418.2 -1198.6 0 945.6 -4418.2 -870.3 0 203.3 -930.1 865.7 0 182.6 -838.6c132.9,-604 372.8,-1042.7 720,-1320.6 347.2,-277.8 803.3,-417.7 1363.2,-417.7 297.6,0 563.4,38.4 795.5,119l-188.9 965.5 1.1 1.9zm11989.3 -4143.8c97.7,-96.7 160.2,-149.4 270.3,-266.9 107.1,-114 137.5,-179.8 265.9,-308.7 116.9,-117.4 147.1,-152.7 195.3,-375.9 84.8,-408.3 118.5,-533.3 -3,-959.1 -91.3,-321.2 -299.1,-441.7 -611.5,-530.3 -524.7,-149.4 -814.2,-560.7 -1335.9,-920.6 -267.3,-183.3 -374.4,-352.1 -795.5,-319.8 -388.8,29 -603,-27.2 -956.9,-142.8 -153.7,-49.9 -297.6,-127.1 -459.3,-90.2 -417.6,98.1 -299,306.9 -585.7,-510.8l-246.4 41.7c-33.7,-6.5 -467.2,-383.9 -977.6,-504.3 -232,-54.8 -416,-1.6 -526.6,205.6 -99.1,183.3 -238.5,234.5 -488,207.2l336.3 -653.9 -919.9 -30.4 -163.2 422.8 -230.6 4.9 196.9 -682.7c176,-167.2 307.3,-305.2 414.6,-607.5 -193.5,-157.3 -379.4,-266.7 -440,-575 -38.3,-189.7 51.1,-472.5 -311.9,-713.6l-369.5 443.7c-60.9,255.4 -188.9,287.7 -299.1,472.5l-507.2 -3.2c-214.2,35.4 -160.2,94.6 -369.5,-25.8 -412.6,-236 -134.4,-88.2 -687.8,-9.5l62.4 287.8 -1004.7 75.6c-358.5,179.8 -622.4,199.2 -902.6,-38.4 -201.8,-171.8 -243.1,-644.3 -124.9,-927.1 181,-425.8 -68.8,-432.2 -147.3,-570.2 -83.3,-144.4 4.9,-91.6 -44.6,-226.6l-648.2 390.3c-460.8,146.4 -393.8,83.7 -619.4,409.8l-854.4 -205.6c273.6,591.1 520.1,737.4 593.5,964 -600,440.2 -1668.8,1224.4 -1990.6,115.6l-304 226.6 -105.5 -140 -769.6 208.8c139.2,155.9 281.6,268.3 301,430.6l-404.7 88.2 -67.4 311.8 308.9 1008.8c-91.2,388.9 -184,811.2 -284.7,1179.2 -792,-46.7 -425.6,-98.1 -1003.2,221.6 -524.7,290.8 -403.2,287.8 -357.1,933.6 -286.5,163.9 -638.3,271.5 -311.9,615.5 361.5,382.5 436.9,449.7 360.1,710.1l334.3 30.4 79.9 242.5 784 -287.8c-20.8,618.5 -70.4,525.4 137.4,759.9l361.5 -9.5c132.9,23.9 245.1,51.3 388.9,62.7l648 -276.3c126.5,-96.5 176.1,-342 401.7,-412.7 155.3,-48.3 477.1,-133.5 623.9,-144.6 30.3,808.2 -64,699.1 345.6,1156.8 292.7,326.1 297.6,99.5 544.1,146.3 86.3,16.1 316.8,268.5 419,323.1 372.8,197.8 811.2,69.3 1113.8,387.5 107.1,112.4 139.2,461 89.6,604 -54.5,157.3 -71.8,51.3 -11.3,284.2 25.8,99.7 48.2,176.8 89.8,273.3l873.8 3.2 20.8 419.2 385.8 480.5c57.5,188.1 -68.8,437.1 -132.8,628.4 -123,371 -129.5,289.2 0,652.3 100.7,284.2 28.7,414.3 33.7,708.6l983.9 98.1 172.7 790.3 359.9 -88.2 23.8 890.2 35.3 686c-38.3,231.6 214.2,170.4 56.1,274.9 -348.8,229.6 -1262.7,1046.1 -1310.7,1331.9 443.4,207.2 283.2,90.2 547.5,411.3l254.4 -118.9 207.8 316.6c60.9,61.2 710.6,377.4 790.4,681.2l-193.5 205.6 48 244.1 259.4 -277.9c187.4,-266.9 220.7,-473.9 480,-597.6 182.6,-86.6 201.8,-70.7 304,-228 132.9,-204.2 301,-433.8 419,-657.2 169.6,-321.2 168.2,-435.4 396.8,-644.5 142.4,-129.9 166.6,-101.1 217.7,-319.6 38.3,-167.2 76.9,-255.4 43.2,-425.8 -43.2,-226.6 -43.2,-671.7 155.2,-809.7 171.2,-120.5 164.8,-94.6 299.1,-237.9 121.5,-128.5 145.7,-143 289.5,-247.5 254.5,-184.7 326.4,-329.3 717.2,-335.7l307.3 -252.4c152.2,-67.7 233.6,-51.3 398.2,-138 241.6,-128.5 539.2,62.7 939.3,0l233.6 -255.4c113.6,-62.8 233.6,-111 316.9,-171.9 79.9,-125.3 -57.6,-327.7 340.6,-816.1 580.8,-710 216.3,-350 325,-780.8 17.4,-70.7 169.5,-260.3 224,-354.9 78.4,-136.5 100.8,-254 155.3,-417.9 87.8,-263.4 161.6,-640.9 172.7,-922.1 25.7,-690.8 -217.7,-1015.4 406.5,-1264.4 315.3,-125.5 206.4,-347 755.3,-927.1 107.1,-114.1 147.1,-192.7 265.7,-308.7l-3.9 -1.4zm-8890.8 -2312.9c295.7,-414.6 710.1,-610.5 1217.1,-610.5 779.1,0 1192.8,546.8 1192.8,1292.8 0,607.2 -269.7,1372.1 -805.5,1706.4 -255.5,159.3 -537.3,234.2 -838.2,234.2 -330.3,0 -628.8,-99.2 -862.1,-337.3 -569.3,-580.8 -329.1,-1689.9 95.9,-2285.6zm896.8 5725.1l-1153.1 0 4990.3 -6335.5 1154.5 0 -4991.7 6335.5zm3948.9 -3241.5c334.1,0 635.4,100.1 867.4,345.5 638.9,675.7 271.5,2192.2 -480.7,2656.5 -255.1,157.7 -537.9,231.5 -837.6,231.5 -330.4,0 -629,-99.2 -862.3,-337.4 -812.7,-829.2 -125.6,-2896 1313.1,-2896zm-4018.9 -720.7c197.3,0 322.1,-216.3 392.4,-372.6 97.1,-216.5 401.1,-1140.5 -50.6,-1140.5 -283.1,0 -394.9,385.2 -459.5,602.3 -60.2,202.9 -285.7,910.8 117.7,910.8zm3631.4 3094.1c196.9,0 320.5,-215.4 390.9,-371.3 123.8,-274.3 183.4,-563.6 183.4,-864.6 0,-154.3 -65,-277.2 -234,-277.2 -280.8,0 -394.9,387.3 -459.5,602.6 -65.5,219.4 -285.1,910.5 119.2,910.5z";
	}
	public class IconeTributacaoEstadual : IIcone
	{
		public string Data => "M1437.7 4338.1c-153.5,78.8 -387,119.5 -699.7,123.1 -6.6,98 4.5,188.2 80.8,259.2 166.4,154.9 492,46.7 661.6,-54.9l-53 267.3c-135.1,73.5 -283.3,110.3 -445.8,110.3 -359.1,0 -565.5,-209.5 -565.5,-569.7 -1.2,-452.9 308.2,-874.1 784.5,-874.1 521.8,0 623.5,540.3 237.2,738.9zm3525.1 -2227.6c26.3,-26 43.2,-40.3 72.8,-71.9 28.9,-30.7 37.1,-48.5 71.6,-83.2 31.5,-31.7 39.6,-41.2 52.6,-101.3 22.8,-110 31.9,-143.7 -0.8,-258.5 -24.6,-86.6 -80.6,-119 -164.8,-142.9 -141.4,-40.3 -219.4,-151.1 -360,-248.1 -72,-49.4 -100.9,-94.9 -214.4,-86.2 -104.8,7.8 -162.5,-7.3 -257.9,-38.5 -41.4,-13.4 -80.2,-34.2 -123.8,-24.3 -112.5,26.4 -80.6,82.7 -157.8,-137.7l-66.4 11.2c-9.1,-1.7 -125.9,-103.5 -263.4,-135.9 -62.5,-14.8 -112.1,-0.4 -141.9,55.4 -26.7,49.4 -64.3,63.2 -131.5,55.8l90.6 -176.2 -247.9 -8.2 -44 113.9 -62.1 1.3 53.1 -184c47.4,-45.1 82.8,-82.2 111.7,-163.7 -52.1,-42.4 -102.3,-71.9 -118.6,-155 -10.3,-51.1 13.8,-127.3 -84.1,-192.3l-99.6 119.6c-16.4,68.8 -50.9,77.5 -80.6,127.3l-136.7 -0.9c-57.7,9.5 -43.2,25.5 -99.6,-7 -111.2,-63.6 -36.2,-23.8 -185.3,-2.6l16.8 77.6 -270.7 20.4c-96.6,48.5 -167.7,53.7 -243.2,-10.4 -54.4,-46.3 -65.5,-173.6 -33.7,-249.8 48.8,-114.7 -18.5,-116.5 -39.7,-153.7 -22.5,-38.9 1.3,-24.7 -12,-61.1l-174.7 105.2c-124.2,39.5 -106.1,22.6 -166.9,110.4l-230.2 -55.4c73.7,159.3 140.2,198.7 160,259.8 -161.7,118.6 -449.7,330 -536.4,31.1l-81.9 61.1 -28.4 -37.7 -207.4 56.3c37.5,42 75.9,72.3 81.1,116.1l-109.1 23.8 -18.2 84 83.2 271.9c-24.6,104.8 -49.6,218.6 -76.7,317.8 -213.4,-12.6 -114.7,-26.4 -270.4,59.7 -141.4,78.4 -108.7,77.6 -96.2,251.6 -77.2,44.2 -172,73.2 -84.1,165.9 97.4,103.1 117.8,121.2 97,191.4l90.1 8.2 21.5 65.3 211.3 -77.6c-5.6,166.7 -19,141.6 37,204.8l97.4 -2.6c35.8,6.5 66,13.8 104.8,16.9l174.6 -74.5c34.1,-26 47.5,-92.2 108.2,-111.2 41.9,-13 128.6,-36 168.1,-39 8.2,217.8 -17.2,188.4 93.1,311.8 78.9,87.9 80.2,26.8 146.6,39.4 23.3,4.3 85.4,72.3 112.9,87.1 100.5,53.3 218.6,18.7 300.2,104.4 28.9,30.3 37.5,124.2 24.2,162.8 -14.7,42.4 -19.4,13.8 -3.1,76.6 7,26.9 13,47.6 24.2,73.7l235.5 0.9 5.6 113 104 129.5c15.5,50.7 -18.5,117.8 -35.8,169.3 -33.2,100 -34.9,77.9 0,175.8 27.1,76.6 7.7,111.6 9.1,190.9l265.1 26.4 46.5 213 97 -23.8 6.4 239.9 9.5 184.9c-10.3,62.4 57.7,45.9 15.1,74.1 -94,61.9 -340.3,281.9 -353.2,358.9 119.5,55.8 76.3,24.3 147.5,110.8l68.6 -32 56 85.3c16.4,16.5 191.5,101.7 213,183.6l-52.1 55.4 12.9 65.8 69.9 -74.9c50.5,-71.9 59.5,-127.7 129.3,-161 49.2,-23.3 54.4,-19.1 81.9,-61.4 35.8,-55 81.1,-116.9 112.9,-177.1 45.7,-86.6 45.3,-117.3 106.9,-173.7 38.4,-35 44.9,-27.2 58.7,-86.1 10.3,-45.1 20.7,-68.8 11.6,-114.7 -11.6,-61.1 -11.6,-181 41.8,-218.2 46.1,-32.5 44.4,-25.5 80.6,-64.1 32.7,-34.6 39.3,-38.5 78,-66.7 68.6,-49.8 88,-88.7 193.3,-90.5l82.8 -68c41,-18.2 63,-13.8 107.3,-37.2 65.1,-34.6 145.3,16.9 253.1,0l63 -68.8c30.6,-16.9 63,-29.9 85.4,-46.3 21.5,-33.8 -15.5,-88.3 91.8,-219.9 156.5,-191.3 58.3,-94.3 87.6,-210.4 4.7,-19.1 45.7,-70.1 60.4,-95.6 21.1,-36.8 27.2,-68.4 41.9,-112.6 23.7,-71 43.6,-172.7 46.5,-248.5 6.9,-186.2 -58.7,-273.6 109.6,-340.7 85,-33.8 55.6,-93.5 203.5,-249.8 28.9,-30.8 39.6,-51.9 71.6,-83.2l-1 -0.4zm-2396 -623.3c79.7,-111.7 191.4,-164.5 328,-164.5 210,0 321.5,147.4 321.5,348.4 0,163.6 -72.7,369.8 -217.1,459.8 -68.9,42.9 -144.8,63.1 -225.9,63.1 -89,0 -169.5,-26.7 -232.3,-90.9 -153.4,-156.5 -88.7,-455.4 25.9,-615.9zm241.7 1542.8l-310.7 0 1344.8 -1707.3 311.1 0 -1345.2 1707.3zm1064.2 -873.5c90,0 171.2,27 233.8,93.1 172.2,182.1 73.2,590.8 -129.5,715.9 -68.7,42.5 -145,62.4 -225.7,62.4 -89,0 -169.5,-26.7 -232.4,-90.9 -219,-223.4 -33.9,-780.4 353.9,-780.4zm-1083 -194.2c53.2,0 86.8,-58.3 105.7,-100.4 26.2,-58.4 108.1,-307.3 -13.6,-307.3 -76.3,0 -106.4,103.8 -123.8,162.3 -16.2,54.7 -77,245.4 31.7,245.4zm978.6 833.8c53.1,0 86.4,-58 105.3,-100.1 33.4,-73.9 49.4,-151.9 49.4,-233 0,-41.6 -17.5,-74.7 -63.1,-74.7 -75.7,0 -106.4,104.4 -123.8,162.4 -17.7,59.1 -76.8,245.4 32.1,245.4zm-2413.9 1183.2c0,-105.7 -90.7,-146.5 -183.7,-146.5 -216.5,0 -367.1,210.3 -408.3,405 394.7,0 592,-86.4 592,-258.8l0 0.3z";
	}
	public class IconeReformaTributaria : IIcone
	{
		public string Data => "M1112.4 208.8c-18.7,-14.8 -31.3,-42.9 -31.3,-74.9 0,-45 49.4,-124.3 60.5,-133.9 11.1,9.6 60.5,88.9 60.5,133.9 0,32.1 -12.6,60.1 -31.3,74.9 20.4,11.4 35.5,33.8 40,60.8 320.9,14.7 537.9,217.6 809.5,321.5 81.4,-2.1 113.2,-51.3 114.5,-129.3 46.2,-8.1 156.9,313.1 -298.4,157.4 -208.2,-71.2 -418.9,-175.5 -612.9,-55.9l0 1113.9 41.5 252.1 -246.6 0 41.4 -252.1 0 -1113.9c-189,-116.5 -393.8,-20.5 -596.7,50.3 -477,166.6 -360,-159.7 -314.6,-151.8 1.4,77.9 33.1,127.1 114.5,129.3 271.6,-103.9 488.6,-306.8 809.5,-321.5 4.5,-27 19.6,-49.4 40,-60.8zm811.4 1085.9l94.5 30.7c61.6,22.2 92.5,51 92.5,86 0,43.3 -30.9,69.1 -92.5,77 -93.5,2.2 -134.6,4.1 -227.2,-22.6 -49.5,-14.3 -86.8,-29.9 -112,-46.8l0 149.1c75.6,32.5 157,49 244.7,49.4l0 113.5 94.5 0 0 -116.6c84.8,-7.9 150.2,-30.1 195.9,-66.5 45.8,-36.6 68.7,-84.5 68.7,-143.9 0,-51.7 -20.5,-95.8 -61.4,-132.1 -41.1,-36.3 -108.9,-68.8 -203.2,-97.2l-94.5 -31.1c-60.1,-24.8 -89.9,-55.6 -89.9,-92.6 0,-42.9 29.8,-69 89.9,-78l94.5 -2c75.3,6.8 143.3,25.4 203.6,55.8l0 -145.2c-44.8,-20.8 -112.6,-32.9 -203.6,-36.7l0 -97.8 -94.5 0 0 100.1c-79,6.3 -142.5,29.4 -190.1,69 -48,39.7 -72,87.5 -72,143.9 0,52.3 18.5,96.8 55.5,133.2 36.8,36.3 105.7,70.1 206.6,101.4zm-1923.8 -236.6c0,-64.5 17.5,-121.1 52.4,-170.1 34.8,-48.8 83.6,-73.2 146.2,-73.2 42.7,0 76.8,13.5 102.4,40.5 25.6,27.2 38.4,64.7 38.4,113.2 0,39.8 -8.7,79.9 -26.1,120.1 -17.5,40.3 -41.3,69.7 -71.5,88.4 -30.1,18.9 -63.8,28.2 -101,28.2 -42,0 -76.1,-13.1 -101.9,-39.6 -26,-26.3 -38.9,-62.1 -38.9,-107.4zm106.9 11.2c0,17.3 3.8,29.7 11.2,37.7 7.4,8 17.6,11.9 30.4,11.9 24.4,0 44.8,-18.3 61.2,-54.8 16.5,-36.7 24.7,-75.3 24.7,-116.1 0,-14.9 -4,-26.5 -11.7,-34.6 -7.8,-8.3 -18,-12.5 -30.5,-12.5 -14.2,0 -27,5.9 -38.4,17.5 -11.4,11.6 -22,34.4 -32,68.3 -10,33.7 -15.1,61.4 -15.1,82.5zm15.9 533.3l-117.1 0 620.6 -787.8 117.3 0 -620.7 787.8zm298.8 -147.6c0,-64.5 17.5,-121.2 52.4,-170.2 34.8,-49 83.6,-73.5 146.2,-73.5 42.7,0 76.8,13.5 102.4,40.5 25.6,27.2 38.4,64.7 38.4,113.1 0,39.8 -8.7,79.9 -26.1,120.4 -17.5,40.3 -41.3,69.7 -71.5,88.4 -30.1,18.5 -63.8,27.9 -101,27.9 -42,0 -76.1,-13.1 -101.9,-39.6 -25.9,-26.3 -38.9,-61.9 -38.9,-106.9zm106.9 10.7c0,17.3 3.6,29.8 11.1,37.7 7.4,8 17.8,11.9 31,11.9 24.4,0 44.6,-18.2 61.1,-54.7 16.4,-36.3 24.6,-74.9 24.6,-115.7 0,-15.2 -3.8,-27 -11.6,-35.1 -7.8,-8.3 -18,-12.5 -30.4,-12.5 -14.2,0 -27,5.9 -38.2,17.5 -11.4,11.6 -22.2,34.4 -32.2,68.3 -10.2,33.7 -15.2,61.4 -15.2,82.5zm613 -1142.4c99.5,0 99.5,151.2 0,151.2 -99.5,0 -99.5,-151.2 0,-151.2zm189.8 1652.7l89.4 80.5 -558.4 0 89.4 -80.5 379.6 0zm180.9 121.4l195.3 24.9 83.7 132.3 -649.7 0 -649.7 0 83.8 -132.3 195.3 -24.9 741.3 0z";
	}
	public class IconeSubstituicaoMateriaPrima : IIcone
	{
		public string Data => "M11.83 112.73c14.04,0 75.22,0.01 99.1,0.03l0 -75.94 6.7 -7.64c0,30.2 0,60.4 0,90.6l-105.81 0 8.67 9.88 -4.37 4.98 -16.13 -18.39 16.13 -18.39 4.37 4.98 -8.67 9.88zm14.76 -78.91l12.83 0 0 8.39 4.02 0 0 -8.39 12.83 0 0 28.58 -29.69 0 0 -28.58zm0 33.35l12.83 0 0 8.39 4.02 0 0 -8.39 12.83 0 0 28.58 -29.69 0 0 -28.58zm34.76 0l12.83 0 0 8.39 4.02 0 0 -8.39 12.83 0 0 28.58 -29.69 0 0 -28.58zm-54.66 -45.31l0 74.34 -6.7 7.64 0 -88.96c35.27,0 70.54,-0.01 105.81,-0.01l-8.67 -9.88 4.37 -4.98 16.13 18.39 -16.13 18.39 -4.37 -4.98 8.67 -9.88c-33.03,0 -66.03,-0.05 -99.11,-0.05z";
	}
	public class IconeAutoatendimentoBalanca : IIcone
	{
		public string Data => "M74.9499969474971,74.9499967575073L0.05,74.9499967575073z M27.2170543663204,62.593420791626L73.7116012565792,62.593420791626C74.3955459587276,62.593420791626 74.9499969474971,63.120960521698 74.9499969474971,63.7717092514038 74.9499969474971,64.4224627494812 74.3955459587276,64.9499972343445 73.7116012565792,64.9499972343445L27.2170543663204,64.9499972343445C26.5331058494747,64.9499972343445 25.9786548607051,64.4224627494812 25.9786548607051,63.7717092514038 25.9786548607051,63.120960521698 26.5331058494747,62.593420791626 27.2170543663204,62.593420791626z M49.0188026420772,49.6581790924072L49.8638343803585,49.6581790924072 49.4413223259151,50.9569480895996 48.5962905876338,50.9569480895996z M21.9879531852901,45.5756595611572L25.476842879504,45.5756595611572 24.98142433092,53.0700061798096 9.53712367936969,53.0700061798096 9.53712367936969,51.2821109771728 13.851577758044,51.2821109771728 13.8586807243526,50.7040927886963 17.112096785754,50.5007942199707C19.4349479667842,50.30461769104,21.1184825889766,49.1938846588135,21.7720069877803,47.8288887023926z M55.1462173454463,44.4631107330322L58.7375984184444,44.4631107330322 58.7375984184444,45.3289554595947 55.9912528984249,45.3289554595947 55.9912528984249,47.0606430053711 57.2587928764522,47.0606430053711C58.2446670524776,47.0606430053711,58.7375984184444,47.5657203674316,58.7375984184444,48.5758750915527L58.7375984184444,49.0087936401367C58.7375984184444,50.0189483642578,58.2446670524776,50.5240257263184,57.2587928764522,50.5240257263184L55.1462173454463,50.5240257263184 55.1462173454463,49.6581790924072 57.2587928764522,49.6581790924072C57.6813125602901,49.6581790924072,57.8925704948604,49.4417179107666,57.8925704948604,49.0087936401367L57.8925704948604,48.5758750915527C57.8925704948604,48.1429489135742,57.6813125602901,47.9264896392822,57.2587928764522,47.9264896392822L55.1462173454463,47.9264896392822z M50.7090911857784,44.4631107330322L54.3004760734737,44.4631107330322 54.3004760734737,45.3289554595947 51.5541229240596,45.3289554595947 51.5541229240596,47.0606430053711 52.8216705314815,47.0606430053711C53.8075370781124,47.0606430053711,54.3004760734737,47.5657203674316,54.3004760734737,48.5758750915527L54.3004760734737,49.0087936401367C54.3004760734737,50.0189483642578,53.8075370781124,50.5240257263184,52.8216705314815,50.5240257263184L50.7090911857784,50.5240257263184 50.7090911857784,49.6581790924072 52.8216705314815,49.6581790924072C53.2441864006221,49.6581790924072,53.4554443351924,49.4417179107666,53.4554443351924,49.0087936401367L53.4554443351924,48.5758750915527C53.4554443351924,48.1429489135742,53.2441864006221,47.9264896392822,52.8216705314815,47.9264896392822L50.7090911857784,47.9264896392822z M44.3706855766475,44.4631107330322L46.2720031730831,44.4631107330322C47.2578735344112,44.4631107330322,47.7508087150753,44.9681880950928,47.7508087150753,45.9783390045166L47.7508087150753,46.4112613677978C47.7508087150753,47.0462139129639 47.5113830558956,47.4069831848145 47.0325317375362,47.493567276001 47.5113830558956,47.5801494598389 47.7508087150753,47.9409206390381 47.7508087150753,48.5758750915527L47.7508087150753,49.0087936401367C47.7508087150753,50.0189483642578,47.2578735344112,50.5240257263184,46.2720031730831,50.5240257263184L44.3706855766475,50.5240257263184 44.3706855766475,49.6581790924072 46.2720031730831,49.6581790924072C46.694522856921,49.6581790924072,46.9057807914913,49.4417179107666,46.9057807914913,49.0087936401367L46.9057807914913,48.5758750915527C46.9057807914913,48.1429489135742,46.694522856921,47.9264896392822,46.2720031730831,47.9264896392822L44.3706855766475,47.9264896392822 44.3706855766475,47.0606430053711 46.2720031730831,47.0606430053711C46.694522856921,47.0606430053711,46.9057807914913,46.8441837310791,46.9057807914913,46.4112613677978L46.9057807914913,45.9783390045166C46.9057807914913,45.5454166412353,46.694522856921,45.3289554595947,46.2720031730831,45.3289554595947L44.3706855766475,45.3289554595947z M42.257884978503,44.4631107330322L43.5254287712276,44.4631107330322 43.5254287712276,50.5240257263184 42.6803970329463,50.5240257263184 42.6803970329463,45.3289554595947 42.257884978503,45.3289554595947z M39.0132179252803,43.5511558532715C38.009895323962,43.5511558532715,37.1965370170772,44.3827312469482,37.1965370170772,45.408528137207L37.1965370170772,49.642004776001C37.1965370170772,50.6678035736084,38.009895323962,51.4993770599365,39.0132179252803,51.4993770599365L61.9836273185909,51.4993770599365C62.9869499199092,51.4993770599365,63.8003120414913,50.6678035736084,63.8003120414913,49.642004776001L63.8003120414913,45.408528137207C63.8003120414913,44.3827312469482,62.9869499199092,43.5511558532715,61.9836273185909,43.5511558532715z M24.2799491874874,41.2513530731201C23.7364807121456,41.2513530731201 23.2959117881954,41.7016647338867 23.2959117881954,42.2571514129639 23.2959117881954,42.812638092041 23.7364807121456,43.2629497528076 24.2799491874874,43.2629497528076 24.8234176628292,43.2629497528076 25.2639865867794,42.812638092041 25.2639865867794,42.2571514129639 25.2639865867794,41.7016647338867 24.8234176628292,41.2513530731201 24.2799491874874,41.2513530731201z M21.4389953605831,41.2513530731201C20.8955268852413,41.2513530731201 20.4549579612911,41.7016647338867 20.4549579612911,42.2571514129639 20.4549579612911,42.812638092041 20.8955268852413,43.2629497528076 21.4389953605831,43.2629497528076 21.9824638359249,43.2629497528076 22.4230327598751,42.812638092041 22.4230327598751,42.2571514129639 22.4230327598751,41.7016647338867 21.9824638359249,41.2513530731201 21.4389953605831,41.2513530731201z M30.580373763293,39.4004199981689L41.8896369926631,39.4004199981689 59.1658554069698,39.4004199981689 70.4751205436885,39.4004199981689C71.8264465324581,39.4004199981689,72.9698867790401,40.3685234069824,73.3511734001338,41.6218441009521L73.4204635612667,41.8973682403564 73.4851913444698,41.8973682403564 74.6785888664424,61.245011138916 26.3182582847774,61.245011138916 27.5968933098018,41.8973682403564 27.6350288383663,41.8973682403564 27.7043228141963,41.6218441009521C28.0856056205928,40.3685234069824,29.2290458671749,39.4004199981689,30.580373763293,39.4004199981689z M30.7019252769649,33.0655639648437L70.3661727897823,33.0655639648437 70.3466186515987,33.1830108642578C69.8219833366573,35.5703475952148,65.5845489494503,37.5102527618408,60.0807991020381,37.9815395355225L59.6682662956417,38.0123718261719 59.6269874565303,38.0291278839111C59.5103454582393,38.0693843841553,59.3864936821163,38.0910709381103,59.258258818835,38.0910709381103L41.7327995292842,38.0910709381103C41.6045570366085,38.0910709381103,41.4810409538448,38.0693843841553,41.3650207512081,38.0291278839111L41.3239936821163,38.0123718261719 40.9119338981807,37.9815395355225C35.4154205314815,37.5102527618408,31.2077178947628,35.5703475952148,30.7196826927364,33.1830108642578z M1.68392455503345,10.0499961853027L47.737937926501,10.0499961853027C48.6403274528682,10.0499961853027,49.3718643181026,10.7977073669434,49.3718643181026,11.7200553894043L49.3718643181026,31.3899276733398 47.1992073051631,31.3899276733398 47.1992073051631,13.6284912109375C47.1992073051631,12.7061431884766,46.467674254626,11.9584358215332,45.5652847282588,11.9584358215332L3.85794305726886,11.9584358215332C2.95555186197162,11.9584358215332,2.22401928827167,12.7061431884766,2.22401952669024,13.6284912109375L2.22401952669024,38.4061229705811C2.22401928827167,39.3284690856934,2.95555186197162,40.076180267334,3.85794305726886,40.076180267334L25.8942947380245,40.076180267334 25.8729038231075,40.1612174987793 25.834768294543,40.1612174987793 25.5242080681026,44.8591239929199 1.68392455503345,44.8591239929199C0.781532704085112,44.8591239929199,0.05,44.1114128112793,0.05,43.189066696167L0.05,11.7200553894043C0.05,10.7977073669434,0.781532704085112,10.0499961853027,1.68392455503345,10.0499961853027z M0.05,0.0499999999999972L74.9499969474971,0.0499999999999972z";
	}
	public class IconeAtualizacaoCustoMateriaPrima : IIcone
	{
		public string Data => "M1086818 809741c195416,0 353829,158413 353829,353829 0,195406 -158413,353818 -353829,353818 -195406,0 -353818,-158413 -353818,-353818 0,-195416 158413,-353829 353818,-353829zm-948394 455446c183361,0 366660,145 550022,311 7001,27465 16798,53823 29019,78761l-579040 0 101442 110918 -51130 55904 -188736 -206363 188736 -206363 51140 55904 -101452 110928zm1159458 -454483l0 -397492 78398 -85720 10 544150c-23520,-23323 -49835,-43808 -78409,-60937zm-1103554 -469697l150107 0 0 94192 47091 0 0 -94192 150096 0 0 320802 -347294 0 0 -320802zm406740 374272l150107 0 0 94182 47091 0 0 -94182 150096 0 0 61082c-119296,42669 -213105,138901 -252480,259719l-94813 0 0 -320802zm-406740 0l150107 0 0 94182 47091 0 0 -94182 150096 0 0 320802 -347294 0 0 -320802zm-115930 -469987l0 834327 -78398 85710 0 -998436c412653,0 825255,-62 1237867,-62l-101452 -110918 51140 -55915 188736 206373 -188736 206353 -51130 -55904 101442 -110918c-386389,0 -772561,-611 -1159469,-611zm1146243 980095c1812,17482 311,33514 -4464,48209 -16135,50115 -63796,75209 -112875,84654l0 58017 -57302 0 0 -52818c-17730,-1170 -86508,-15814 -86456,-28149l197 -64252c0,-994 54403,24969 73355,27952 37843,6442 111094,-2651 105626,-55335 -4277,-41198 -63019,-47557 -94617,-56422 -54806,-15141 -92825,-38982 -99153,-100230 -7084,-68021 39261,-106692 101048,-120570l0 -55583 57302 0 0 50488c14758,673 82738,12666 83152,27900l1025 58576c-186,-10553 -57137,-27724 -108070,-25042 -28335,1491 -58265,14240 -54775,47650 4205,40887 63951,47857 95984,56505 53439,14530 93643,37024 100023,98449z";
	}
	public class IconeConsultaProduto : IIcone
	{
		public string Data => "M124688377 173349992c1657230,-310590 3274545,823950 3584899,2518971 507219,2771105 -2727528,4732667 -4953623,3009893 -1338846,-1036168 -1639752,-3010130 -569455,-4362674 505684,-639131 1070888,-1003573 1938179,-1166190zm-4524110 7988907l0 -487970 -365032 -712705 -18895 -36964 0 -38499 0 -2555345 -992472 0 0 1119307 0 45230 -24682 40743 -852884 1403798 0 1213429c148564,32358 281303,106876 384990,210564 141124,141124 228396,336335 228396,551623 0,215524 -87272,410499 -228396,551623 -141124,141124 -336099,228396 -551623,228396 -215406,0 -410617,-87272 -551741,-228396 -141124,-141124 -228396,-336099 -228396,-551623 0,-215524 87272,-410499 228396,-551623 103688,-103688 236545,-178324 385109,-210564l0 -1258777 0 -45349 24682 -40625 852884 -1403798 0 -1073958 -1641996 0 0 950785 0 61528 -41688 47829 -835996 957989 0 493284c148564,32240 281303,106876 384990,210564 141124,141124 228396,336335 228396,551741 0,215524 -87272,410499 -228396,551623 -141124,141124 -336099,228396 -551623,228396 -215524,0 -410499,-87272 -551623,-228396 -141124,-141124 -228396,-336099 -228396,-551623 0,-215406 87272,-410617 228396,-551741 103688,-103688 236427,-178324 384990,-210564l0 -554930 0 -61646 41688 -47710 835996 -957989 0 -892800c-1214137,-68850 -2177440,-1075139 -2177440,-2306518 0,-1266335 1018099,-2294354 2280773,-2310061 89634,-772697 746362,-1372503 1543032,-1372503 111718,0 220956,12046 326179,34366 -2126,-51490 -3307,-103570 -3307,-155531 0,-2028286 1644004,-3672290 3672408,-3672290 1702343,0 3133893,1158160 3550179,2729653 422781,-538042 1079509,-883470 1817131,-883470 1276019,0 2310415,1034396 2310415,2310297 0,52552 -1653,104278 -5196,155768 110301,-46293 231230,-71920 358301,-71920 511116,0 925276,414042 925276,925158l144194 0c1276019,0 2310297,1034396 2310297,2310533 0,923269 -541585,1719939 -1324320,2089931l0 1324320 835878 957870 41688 47710 0 61646 0 554930c148564,32240 281303,106876 384990,210564 141124,141124 228396,336335 228396,551741 0,215524 -87272,410499 -228396,551623 -141124,141124 -336099,228396 -551623,228396 -215288,0 -410499,-87272 -551623,-228396 -141124,-141124 -228396,-336099 -228396,-551623 0,-215406 87272,-410617 228396,-551741 103688,-103688 236427,-178324 384990,-210564l0 -493402 -835878 -957870 -41688 -47829 0 -61528 0 -1259013c-206903,60937 -425970,93650 -652712,93650l-8503 -236 -500606 0 0 1014438c168994,25154 320274,104514 435535,219775 141124,141124 228396,336335 228396,551623 0,215406 -87272,410617 -228396,551741 -141124,141124 -336099,228396 -551623,228396 -215524,0 -410499,-87272 -551623,-228396 -141124,-141124 -228396,-336335 -228396,-551741 0,-215288 87272,-410499 228396,-551623 91760,-91760 206312,-160728 334446,-197810l0 -1036404 -715303 0c232766,-994362 146792,-2101387 -268903,-2995958 -332910,-715539 -806827,-1339318 -1448084,-1804968 -1383958,-1005227 -3220457,-1159341 -4759947,-421246 -895516,429276 -1669630,1203390 -2098789,2098907 -488560,1019280 -578194,2116857 -329722,3123265l-644564 0 0 2516728 365032 712705 18895 36846 0 38617 0 510526c163326,27398 309646,105577 421718,217650 141124,141124 228396,336335 228396,551623 0,215524 -87272,410499 -228396,551623 -141124,141124 -336099,228396 -551623,228396 -215524,0 -410499,-87272 -551623,-228396 -141124,-141124 -228396,-336099 -228396,-551623 0,-215288 87272,-410499 228396,-551623 95067,-95067 214461,-165688 348263,-201589zm203360 306457c-123291,0 -235128,50072 -315905,130850 -80777,80777 -130850,192613 -130850,315905 0,123291 50072,235128 130850,315905 80777,80777 192613,130850 315905,130850 123291,0 235128,-50072 315905,-130850 80777,-80777 130850,-192613 130850,-315905 0,-123291 -50072,-235128 -130850,-315905 -80777,-80777 -192613,-130850 -315905,-130850zm-4283432 -1180244c-80777,-80777 -192613,-130850 -315905,-130850 -123291,0 -235128,50072 -315905,130850 -80777,80777 -130850,192613 -130850,316023 0,123291 50072,235128 130850,315905 80777,80777 192613,130850 315905,130850 123291,0 235128,-50072 315905,-130850 80777,-80777 130850,-192613 130850,-315905 0,-123410 -50072,-235246 -130850,-316023zm1659474 1180244c-123410,0 -235246,50072 -316023,130850 -80777,80777 -130850,192613 -130850,315905 0,123291 50072,235128 130850,315905 80777,80777 192613,130850 316023,130850 123291,0 235128,-50072 315905,-130850 80777,-80777 130850,-192613 130850,-315905 0,-123291 -50072,-235128 -130850,-315905 -80777,-80777 -192613,-130850 -315905,-130850zm15174530 -1096278c-123291,0 -235128,50072 -315905,130850 -80777,80777 -130850,192613 -130850,316023 0,123291 50072,235128 130850,315905 80777,80777 192613,130850 315905,130850 123291,0 235128,-50072 315905,-130850 80777,-80777 130850,-192613 130850,-315905 0,-123410 -50072,-235246 -130850,-316023 -80777,-80777 -192613,-130850 -315905,-130850zm-2006202 -1571730c-80777,-80777 -192613,-130850 -315905,-130850 -123291,0 -235128,50072 -315905,130850 -80777,80777 -130850,192613 -130850,315905 0,123410 50072,235246 130850,316023 80777,80777 192613,130850 315905,130850 123291,0 235128,-50072 315905,-130850 80777,-80777 130850,-192613 130850,-316023 0,-123291 -50072,-235128 -130850,-315905zm-6743238 -2471379c-248236,0 -448408,-78888 -599451,-235836 -151989,-157657 -227570,-369402 -227570,-635116 0,-290632 80541,-520682 241623,-690857 161909,-170175 376960,-255676 645981,-255676 258274,0 459154,77234 604530,231585 145257,153642 217531,370347 217531,649288 0,286499 -79714,514777 -240796,683417 -160255,168522 -374480,253196 -641848,253196zm45703 -1389036c-236663,0 -354640,164389 -354640,493992 0,311299 112190,466594 337989,466594 229931,0 345429,-161909 345429,-486552 0,-316377 -109592,-474034 -328777,-474034zm2500785 -379558l-2271680 3488179 -593665 0 2266720 -3488179 598625 0zm-374362 3537189c-248354,0 -448408,-78888 -599570,-236663 -151870,-156949 -227451,-368693 -227451,-634408 0,-290514 80541,-520564 241623,-690739 161909,-170175 376960,-255794 645981,-255794 259810,0 461634,76408 606065,228396 144549,152697 216705,368575 216705,647634 0,286381 -80541,515604 -241623,685779 -160255,170293 -374480,255794 -641730,255794zm38971 -1389154c-235010,0 -352869,164389 -352869,493284 0,311299 114552,467421 342949,467421 229931,0 345310,-161909 345310,-486552 0,-151162 -30705,-267368 -92114,-350389 -61410,-82194 -142777,-123764 -243276,-123764zm-2707924 2936556c936850,435653 2253730,429394 3224000,-69204 343303,-176434 535562,-351451 812850,-538750l282838 257211c-69204,568983 207375,658735 828792,1280861l2060762 2166575c47002,33539 121048,62590 181867,102861l238434 0c117150,-79006 132739,-45349 247882,-160610 115261,-115261 81604,-130731 160610,-247882l0 -238434c-40270,-60819 -69204,-134865 -102861,-181867l-1525790 -1648373c-184937,-184937 -340823,-340587 -526823,-526350 -623661,-622834 -711288,-897524 -1282514,-828320l-255558 -283310c187299,-277288 362552,-469547 538632,-812850 498952,-972160 506038,-2284789 69322,-3224000 -327006,-703021 -749669,-1165482 -1176701,-1475836 -1135958,-825013 -2638838,-943935 -3894072,-342476 -720971,345547 -1354552,979128 -1699981,1699981 -904491,1887280 -152933,4154000 1818312,5070773z";
	}
	public class IconeDemonstrativoResultado : IIcone
	{
		public string Data => "M946217 0l0 17548134c80439,0 158393,10173 233153,29100l2650685 -3469384 0 -132487 101140 0 1838493 -2406294c-97709,-148693 -154371,-326604 -154371,-517764 0,-522377 423603,-945981 946099,-945981 522377,0 945981,423603 945981,945981 0,110721 -18927,217066 -54059,315721l3230552 2608337 84934 0 0 68491 1720201 1388865c144316,-89192 314538,-140649 496708,-140649 200860,0 386933,62576 540239,169276l1658453 -1195576 0 -290407 402902 0 3009820 -2169829c-24841,-84815 -38208,-174244 -38208,-266985 0,-522495 423485,-945981 945981,-945981 58081,0 115098,5323 170222,15378l555499 -969521 0 -273728 156973 0 2285400 -3987502c-145854,-166437 -234336,-384567 -234336,-623280 0,-522495 423485,-946099 945981,-946099 522495,0 945981,423603 945981,946099 0,522377 -423485,945981 -945981,945981 -55242,0 -109420,-4968 -162178,-13958l-3000593 5235364c142660,165609 229013,381373 229013,617247 0,522495 -423485,945981 -945981,945981 -200741,0 -386933,-62576 -540121,-169276l-2302907 1660109 414968 0 0 336541 -630851 0 0 -180987 -2552503 1840267c24841,84697 38327,174126 38327,266867 0,522495 -423603,946099 -946099,946099 -522377,0 -945981,-423603 -945981,-946099 0,-110603 18927,-217066 53941,-315721l-5035805 -4065693c-144316,89192 -314302,140767 -496471,140767 -101731,0 -199795,-16324 -291708,-46016l-4565950 5976108c118410,157920 188439,354048 188439,566618 0,31111 -1420,61867 -4377,92149l49801 0 0 336422 -147865 0c-156264,307086 -475415,517527 -843540,517527l0 3451995 22797456 0 0 946217 -22797456 0 -946217 0 0 -946217 0 -22892208 946217 0zm11195976 937818c2153978,0 3900085,1746107 3900085,3900085 0,2153859 -1746107,3899966 -3900085,3899966 -2100865,0 -3813732,-1661528 -3896536,-3742283l0 94752 -630851 0 0 -336422 628485 0c44714,-2115178 1773196,-3816097 3898902,-3816097zm-265210 4249164l517172 192461c337132,139466 506289,320453 506289,540358 0,272190 -169157,433422 -506289,482986 -511613,13722 -736722,25669 -1243248,-141595 -270534,-90020 -474705,-187966 -612516,-293955l0 935571c413194,204054 859154,307796 1338591,310043l0 712236 517172 0 0 -731636c463941,-49564 821537,-189030 1071725,-417334 250069,-229368 375813,-530184 375813,-903514 0,-323765 -112259,-600213 -335831,-828517 -224755,-228185 -595718,-432002 -1111707,-609677l-517172 -195773c-328378,-155554 -491621,-349079 -491621,-580932 0,-269469 163243,-433303 491621,-489610l517172 -12894c412129,42940 784157,159221 1114073,350499l0 -911676c-245337,-130239 -616183,-205946 -1114073,-229013l0 -614408 -517172 0 0 627894c-432475,40456 -778952,184535 -1040023,433422 -262490,248886 -394030,549703 -394030,903514 0,328497 101376,607192 303182,835496 202043,228185 578921,440046 1130871,636056zm10876824 13735703l0 -336422 630851 0 0 336422 -630851 0zm-20185334 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261702 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630614 0 0 336422 -630614 0zm1261465 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 -4610783l0 -336541 630851 0 0 336541 -630851 0zm-21446917 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm2523167 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261702 0l0 -336541 630733 0 0 336541 -630733 0zm2523167 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm3784750 0l0 -336541 630614 0 0 336541 -630614 0zm1261465 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 -4610664l0 -336541 630851 0 0 336541 -630851 0zm-21446917 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261702 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630614 0 0 336541 -630614 0zm1261465 0l0 -336541 630851 0 0 336541 -630851 0zm2523167 0l0 -336541 630851 0 0 336541 -630851 0zm-20185334 -4610901l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630733 0 0 336422 -630733 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm10092785 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630614 0 0 336422 -630614 0zm1261465 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 0l0 -336422 630851 0 0 336422 -630851 0zm1261583 -4610783l0 -336541 630851 0 0 336541 -630851 0zm-21446917 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261702 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630733 0 0 336541 -630733 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630614 0 0 336541 -630614 0zm1261465 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0zm1261583 0l0 -336541 630851 0 0 336541 -630851 0z";
	}
	public class IconeGeradorArquivoDevolucaoIcmsStSimplesNacional : IIcone
	{
		public string Data => "M15.641001,32.237003 L15.641001,34.237003 24.008999,34.237003 24.008999,32.237003 z M4.7179985,30.030002 L11.012,30.030002 11.012,36.324001 4.7179985,36.324001 z M15.641001,28.678001 L15.641001,30.678001 24.008999,30.678001 24.008999,28.678001 z M3.3649998,28.677002 L3.3649998,37.677002 12.365001,37.677002 12.365001,28.677002 z M30.437969,23.535 L40.606973,23.535057 40.607059,38.330634 44.966,38.330658 35.740746,47.028999 26.514999,38.330559 30.438057,38.330578 z M15.641001,23.216002 L15.641001,25.216001 24.008999,25.216001 24.008999,23.216002 z M15.641001,19.775 L15.641001,21.775001 24.008999,21.775001 24.008999,19.775 z M4.7179985,17.569 L11.012,17.569 11.012,23.863001 4.7179985,23.863001 z M15.641001,16.216 L15.641001,18.215999 33.310002,18.215999 33.310002,16.216 z M3.3649998,16.216 L3.3649998,25.216001 12.365001,25.216001 12.365001,16.216 z M15.641001,10.569 L15.641001,12.569 33.310002,12.569 33.310002,10.569 z M15.641001,7.1280003 L15.641001,9.1280003 30.613,9.1280003 30.613,7.1280003 z M10.518972,4.2890053 L12.070004,5.4874191 6.9952679,11.761005 3.6339989,7.8767719 5.0213223,6.3334217 7.0792599,8.7000599 z M15.641001,3.5690002 L15.641001,5.5690002 27.079001,5.5690002 27.079001,3.5690002 z M3.3649998,3.5690002 L3.3649998,12.569 12.365001,12.569 12.365001,3.5690002 z M0,0 L27.911788,0.00075531006 38.217587,9.7175083 38.217946,21.340893 38.217999,21.403343 28.359046,21.405283 28.359105,36.316132 20.925017,36.401093 28.183274,43.341 0.001329422,43.338665 z";
	}
	public class IconeLocacao : IIcone
	{
		public string Data => "M10091090 14661880l142510 54290 0 208240 65410 24810 0 -208120 161160 61390 0 450550 -369080 -140600 0 -450560zm1097350 480780c-7970,-6070 -156590,-34750 -169620,-37340 -135810,278210 -326260,344450 -525560,347460 -141610,4570 -343460,-69560 -434480,-186880 7130,-7790 70190,-43440 87430,-54270 -4300,-9110 -289320,-104760 -319450,-115200 -5470,12710 -20310,250040 -22690,289510 5580,-450 78210,-41050 96480,-48970 282170,360500 987850,436960 1287890,-194310zm-931620 -490440l-165730 -61370 401790 -125530 151660 45410 -387720 141490zm236060 87410l-164300 -59700 390020 -145770 176050 53670 -401770 151800zm-688810 -194910c7970,6070 156590,34750 169620,37340 135810,-278210 326260,-344450 525560,-347460 141610,-4570 343460,69560 434480,186880 -7130,7790 -70190,43440 -87430,54270 4300,9110 289320,104760 319450,115200 5470,-12710 20310,-250040 22690,-289510 -5580,450 -78210,41050 -96480,48970 -313840,-359410 -987850,-436960 -1287890,194310zm1090580 117160c-340580,129750 -90070,34310 -369070,140610l0 450550 369070 -140600 0 -450560z";
	}
	public class IconeMensagemWhatsappModelo : IIcone
	{
		public string Data => "M1082904 931559l39424 0 -176067 -179982 -89285 91275 -38 28 -86744 88678 66155 0c-4996,43415 -10484,72336 -40855,110290 -27499,34409 -67492,60610 -114243,76507 -46059,15679 -98868,21432 -152805,15224 -42211,-4863 -85199,-17082 -126263,-37632 35585,40761 81170,70516 131884,89275 51576,19072 108499,26845 165706,23328 57273,-3517 114841,-18390 167659,-44533 87029,-43092 161146,-116964 199793,-221482l4057 -10977 11621 0zm-770792 14266c-93237,28409 -170398,51908 -312112,95076l92631 -313951c-35822,-70137 -54477,-147743 -54477,-227112 0,-276015 223823,-499838 499838,-499838 276015,0 499838,223823 499838,499838 0,83739 -20608,162673 -57027,232013l-34542 -35310 -267560 273503 29111 0c-53027,19157 -110196,29632 -169820,29632 -78905,0 -156066,-18437 -225880,-53851zm404970 -401198c-21148,26750 -39955,52183 -68895,75426 -41614,14541 -248270,-147942 -238288,-204315 18333,-32296 33651,-43652 56676,-68800 6218,-4275 -62515,-164568 -73113,-164075 -11508,540 -52600,-4815 -61700,2029 -27556,20693 -69909,77388 -77729,112404 -14295,64241 29679,139354 58885,186171 30561,49045 63653,88659 98982,121770 49775,49026 110404,92470 189044,125438 50894,21357 124272,47131 185404,22741 33319,-13309 63312,-32172 79388,-62724 5308,-10114 12550,-61226 11261,-72687 -1204,-10569 -35784,-28513 -67862,-43813 -43983,-20968 -85957,-42429 -92053,-29566z";
	}
	public class IconeEstoqueFisico : IIcone
	{
		public string Data => "M137.05086,140.894L140.46378,140.894 140.46378,147.02612 143.38046,147.02612 143.38046,140.894 151.64439,140.894 151.64439,147.02612 154.56108,147.02612 154.56108,140.894 157.97299,140.894 157.97299,160.79401 137.05086,160.79401z M111.73243,140.894L115.14535,140.894 115.14535,147.02612 118.06203,147.02612 118.06203,140.894 126.32597,140.894 126.32597,147.02612 129.24265,147.02612 129.24265,140.894 132.65456,140.894 132.65456,160.79401 111.73243,160.79401z M86.414001,140.894L89.82692,140.894 89.82692,147.02612 92.743607,147.02612 92.743607,140.894 101.00754,140.894 101.00754,147.02612 103.92422,147.02612 103.92422,140.894 107.33614,140.894 107.33614,160.79401 86.414001,160.79401z M35.601002,140.894L39.117813,140.894 39.117813,147.02612 42.123276,147.02612 42.123276,140.894 50.638763,140.894 50.638763,147.02612 53.64423,147.02612 53.64423,140.894 57.16,140.894 57.16,160.79401 35.601002,160.79401z M137.05099,118.705L140.4639,118.705 140.4639,124.83712 143.38055,124.83712 143.38055,118.705 151.64444,118.705 151.64444,124.83712 154.56111,124.83712 154.56111,118.705 157.97299,118.705 157.97299,138.605 137.05099,138.605z M111.73199,118.705L115.14507,118.705 115.14507,124.83712 118.06187,124.83712 118.06187,118.705 126.32614,118.705 126.32614,124.83712 129.24295,124.83712 129.24295,118.705 132.655,118.705 132.655,138.605 111.73199,138.605z M35.601002,118.705L39.117813,118.705 39.117813,124.83712 42.123276,124.83712 42.123276,118.705 50.638763,118.705 50.638763,124.83712 53.64423,124.83712 53.64423,118.705 57.16,118.705 57.16,138.605 35.601002,138.605z M137.05099,96.739998L140.4639,96.739998 140.4639,102.87212 143.38055,102.87212 143.38055,96.739998 151.64444,96.739998 151.64444,102.87212 154.56111,102.87212 154.56111,96.739998 157.97299,96.739998 157.97299,116.64 137.05099,116.64z M35.601002,96.739998L39.117813,96.739998 39.117813,102.87212 42.123276,102.87212 42.123276,96.739998 50.638763,96.739998 50.638763,102.87212 53.64423,102.87212 53.64423,96.739998 57.16,96.739998 57.16,116.64 35.601002,116.64z M22.148001,71.739998L32.047993,71.739998 161.52698,71.739998 171.42697,71.739998 171.42697,81.639992 171.42697,160.79399 161.52698,160.79399 161.52698,81.639992 32.047993,81.639992 32.047993,160.79399 22.148001,160.79399 22.148001,81.639992z M0.05,54.874996L17.047997,54.874996 176.52698,54.875 193.52498,54.874996 193.52498,67.397995 193.52498,160.79399 176.52698,160.79399 176.52698,67.397995 17.047997,67.397995 17.047997,160.79399 0.05,160.79399 0.05,67.397995z M0.05,37.774758L193.52498,37.774758 193.52498,50.740758 0.05,50.740758z M102.48347,0.05L193.52499,34.140999 0.05,34.140999z";
	}
	public class IconeAgendamento : IIcone
	{
		public string Data => "M12.5509040355682,103.171501159668L24.2118816375732,103.171501159668C25.3445653915405,103.171501159668,26.2627878189087,104.0908203125,26.2627897262573,105.22486114502L26.2627897262573,105.869079589844C26.2627878189087,107.003128051758,25.3445653915405,107.92244720459,24.2118816375732,107.92244720459L12.5509040355682,107.92244720459C11.4182212352753,107.92244720459,10.4999997795073,107.003128051758,10.5000016093254,105.869079589844L10.5000016093254,105.22486114502C10.4999997795073,104.0908203125,11.4182212352753,103.171501159668,12.5509040355682,103.171501159668z M12.5509040355682,87.0946731567383L24.2118816375732,87.0946731567383C25.3445653915405,87.0946731567383,26.2627878189087,88.0139923095703,26.2627897262573,89.1480331420898L26.2627897262573,89.7922668457031C26.2627878189087,90.9263000488281,25.3445653915405,91.8456192016602,24.2118816375732,91.8456192016602L12.5509040355682,91.8456192016602C11.4182212352753,91.8456192016602,10.4999997795073,90.9263000488281,10.5000016093254,89.7922668457031L10.5000016093254,89.1480331420898C10.4999997795073,88.0139923095703,11.4182212352753,87.0946731567383,12.5509040355682,87.0946731567383z M12.5509040355682,71.0228424072266L24.2118816375732,71.0228424072266C25.3445653915405,71.0228424072266,26.2627868652344,71.9421615600586,26.2627830505371,73.0761947631836L26.2627830505371,73.7204208374023C26.2627868652344,74.8544692993164,25.3445653915405,75.7737884521484,24.2118816375732,75.7737884521484L12.5509040355682,75.7737884521484C11.4182212352753,75.7737884521484,10.4999997795073,74.8544692993164,10.5000016093254,73.7204208374023L10.5000016093254,73.0761947631836C10.4999997795073,71.9421615600586,11.4182212352753,71.0228424072266,12.5509040355682,71.0228424072266z M12.5509040355682,55.0846366882324L24.2118816375732,55.0846366882324C25.3445653915405,55.0846366882324,26.2627868652344,56.0039520263672,26.2627830505371,57.1379928588867L26.2627830505371,57.7822227478027C26.2627868652344,58.916259765625,25.3445653915405,59.835578918457,24.2118816375732,59.835578918457L12.5509040355682,59.835578918457C11.4182212352753,59.835578918457,10.4999997795073,58.916259765625,10.5000016093254,57.7822227478027L10.5000016093254,57.1379928588867C10.4999997795073,56.0039520263672,11.4182212352753,55.0846366882324,12.5509040355682,55.0846366882324z M12.5509040355682,39.0127983093262L24.2118816375732,39.0127983093262C25.3445653915405,39.0127983093262,26.2627868652344,39.9321174621582,26.2627830505371,41.0661544799805L26.2627830505371,41.7103843688965C26.2627868652344,42.844425201416,25.3445653915405,43.763744354248,24.2118816375732,43.763744354248L12.5509040355682,43.763744354248C11.4182212352753,43.763744354248,10.4999997795073,42.844425201416,10.5000016093254,41.7103843688965L10.5000016093254,41.0661544799805C10.4999997795073,39.9321174621582,11.4182212352753,39.0127983093262,12.5509040355682,39.0127983093262z M58.1032752990723,34.982738494873C60.7046356201172,34.9827423095703 62.005313873291,38.372932434082 62.005313873291,45.1533164978027 62.005313873291,51.9336967468262 60.6775360107422,55.3238906860352 58.0219802856445,55.3238906860352 55.2986831665039,55.3238906860352 53.9370346069336,52.0354042053223 53.9370346069336,45.4584312438965 53.9370346069336,38.4746360778809 55.3257827758789,34.9827423095703 58.1032752990723,34.982738494873z M58.3877983093262,30.0805187225342C54.8244781494141,30.0805187225342 52.104564666748,31.4060840606689 50.2280654907227,34.0572128295898 48.3515625,36.7083511352539 47.4133110046387,40.5629959106445 47.4133110046387,45.6211624145508 47.4133110046387,55.3713531494141 50.8817901611328,60.2464485168457 57.8187484741211,60.2464485168457 61.273681640625,60.2464485168457 63.9258499145508,58.9310569763184 65.7752494812012,56.3002662658691 67.6246528625488,53.6694793701172 68.5493545532227,49.8860206604004 68.5493545532227,44.9499015808105 68.5493545532227,35.0369834899902 65.1621704101563,30.0805187225342 58.3877983093262,30.0805187225342z M79.4548187255859,29.9177894592285C78.2625274658203,30.8263607025146 76.9279708862305,31.6603488922119 75.4511642456055,32.4197540283203 73.9743499755859,33.1791572570801 72.4026908874512,33.8029556274414 70.736198425293,34.2911415100098L70.736198425293,39.7018852233887C71.3052444458008,39.6340827941895 71.8810653686523,39.5222053527832 72.4636650085449,39.3662567138672 73.046257019043,39.2103118896484 73.6085319519043,39.0204582214355 74.1504821777344,38.796703338623 74.6924362182617,38.5729560852051 75.2038955688477,38.322078704834 75.6848754882813,38.0440826416016 76.1658630371094,37.7660903930664 76.5824813842773,37.4711418151855 76.9347534179688,37.1592407226563L76.9347534179688,59.737922668457 83.3365325927734,59.737922668457 83.3365325927734,29.9177894592285z M40.4408321380615,27.0925540924072L91.0759887695313,27.0925540924072C92.1239547729492,27.0925540924072,92.973503112793,27.9431133270264,92.973503112793,28.9923343658447L92.973503112793,60.8597679138184C92.973503112793,61.9089851379395,92.1239547729492,62.7595443725586,91.0759887695313,62.7595443725586L40.4408321380615,62.7595443725586C39.3928642272949,62.7595443725586,38.5433197021484,61.9089851379395,38.5433197021484,60.8597679138184L38.5433197021484,28.9923343658447C38.5433197021484,27.9431133270264,39.3928642272949,27.0925540924072,40.4408321380615,27.0925540924072z M12.5509040355682,22.9359722137451L24.2118816375732,22.9359722137451C25.3445653915405,22.9359722137451,26.2627868652344,23.8552932739258,26.2627830505371,24.9893321990967L26.2627830505371,25.6335620880127C26.2627868652344,26.767599105835,25.3445653915405,27.6869163513184,24.2118816375732,27.6869163513184L12.5509040355682,27.6869163513184C11.4182212352753,27.6869163513184,10.4999997795073,26.767599105835,10.5000016093254,25.6335620880127L10.5000016093254,24.9893321990967C10.4999997795073,23.8552932739258,11.4182212352753,22.9359722137451,12.5509040355682,22.9359722137451z M70.4516868591309,13.6096019744873L72.9417152404785,13.6096019744873 77.0130462646484,19.862585067749C77.199089050293,20.1443138122559,77.3708114624023,20.423656463623,77.5282287597656,20.700611114502L77.5568466186523,20.700611114502C77.5186920166016,20.461856842041,77.4996109008789,19.9915103912354,77.4996109008789,19.289571762085L77.4996109008789,13.6096019744873 79.6819610595703,13.6096019744873 79.6819610595703,23.8808212280273 77.3493423461914,23.8808212280273 73.1205940246582,17.4272880554199C72.8725433349609,17.0500564575195,72.7008209228516,16.7659378051758,72.6054191589355,16.5749340057373L72.5767936706543,16.5749340057373C72.6149559020996,16.9378395080566,72.6340370178223,17.4917507171631,72.6340370178223,18.236665725708L72.6340370178223,23.8808212280273 70.4516868591309,23.8808212280273z M59.7344284057617,13.6096019744873L62.0527305603027,13.6096019744873 62.0527305603027,19.5689163208008C62.0527305603027,21.2306480407715 62.7014770507813,22.0615119934082 63.9989585876465,22.0615119934082 65.272590637207,22.0615119934082 65.9094123840332,21.2592964172363 65.9094123840332,19.6548671722412L65.9094123840332,13.6096019744873 68.2205505371094,13.6096019744873 68.2205505371094,19.432825088501C68.2205505371094,22.5175342559814 66.7847328186035,24.0598888397217 63.9130973815918,24.0598888397217 61.1273193359375,24.0598888397217 59.7344284057617,22.5533466339111 59.7344284057617,19.5402641296387z M55.3058433532715,13.6096019744873L57.6098327636719,13.6096019744873 57.6098327636719,19.8052825927734C57.6098327636719,21.1805076599121 57.2914237976074,22.2334156036377 56.6546058654785,22.9640045166016 56.0177917480469,23.6945934295654 55.0959548950195,24.0598888397217 53.889102935791,24.0598888397217 53.3500747680664,24.0598888397217 52.8515930175781,23.9667739868164 52.3936576843262,23.7805442810059L52.3936576843262,21.6102676391602C52.7895812988281,21.911096572876 53.2332077026367,22.0615119934082 53.7245330810547,22.0615119934082 54.7787399291992,22.0615119934082 55.3058433532715,21.2783985137939 55.3058433532715,19.7121677398682z M40.4408321380615,10.4712266921997C37.9257125854492,10.4712266921997,35.8868045806885,12.5125713348389,35.8868045806885,15.0306978225708L35.8868045806885,61.5239410400391C35.8868045806885,64.0420684814453,37.9257125854492,66.0834121704102,40.4408321380615,66.0834121704102L91.0759887695313,66.0834121704102C93.5911026000977,66.0834121704102,95.6300201416016,64.0420684814453,95.6300201416016,61.5239410400391L95.6300201416016,15.0306978225708C95.6300201416016,12.5125713348389,93.5911026000977,10.4712266921997,91.0759887695313,10.4712266921997z M12.5509040355682,6.86413908004761L24.2118816375732,6.86413908004761C25.3445653915405,6.86413812637329,26.2627868652344,7.78345680236816,26.2627830505371,8.91749572753906L26.2627830505371,9.56172370910645C26.2627868652344,10.695761680603,25.3445653915405,11.6150798797607,24.2118816375732,11.6150798797607L12.5509040355682,11.6150798797607C11.4182212352753,11.6150798797607,10.4999997795073,10.695761680603,10.5000016093254,9.56172370910645L10.5000016093254,8.91749572753906C10.4999997795073,7.78345680236816,11.4182212352753,6.86413812637329,12.5509040355682,6.86413908004761z M115,0L115,115 M17.2951822280884,0L104.5,0 104.5,115 17.2951822280884,115 17.2951822280884,110.974060058594 23.6361503601074,110.974060058594C26.4912424087524,110.974060058594,28.8057498931885,108.656784057617,28.8057518005371,105.798278808594L28.8057518005371,104.915710449219C28.8057498931885,102.057197570801,26.4912424087524,99.7399291992188,23.6361503601074,99.7399291992188L17.2951822280884,99.7399291992188 17.2951822280884,95.0737533569336 23.6361503601074,95.0737533569336C26.4912424087524,95.0737533569336,28.8057498931885,92.756477355957,28.8057518005371,89.8979721069336L28.8057518005371,89.0154037475586C28.8057498931885,86.1568908691406,26.4912424087524,83.8396224975586,23.6361503601074,83.8396224975586L17.2951822280884,83.8396224975586 17.2951822280884,79.1824188232422 23.6361503601074,79.1824188232422C26.4912424087524,79.1824188232422,28.8057498931885,76.8651428222656,28.8057518005371,74.0066375732422L28.8057518005371,73.1240615844727C28.8057498931885,70.2655639648438,26.4912424087524,67.9482879638672,23.6361503601074,67.9482879638672L17.2951822280884,67.9482879638672 17.2951822280884,62.9619903564453 23.6361503601074,62.9619903564453C26.4912424087524,62.9619903564453,28.8057498931885,60.644718170166,28.8057518005371,57.7862129211426L28.8057518005371,56.903636932373C28.8057498931885,54.0451316833496,26.4912424087524,51.7278594970703,23.6361503601074,51.7278594970703L17.2951822280884,51.7278594970703 17.2951822280884,47.0706558227539 23.6361503601074,47.0706558227539C26.4912424087524,47.0706558227539,28.8057498931885,44.7533836364746,28.8057518005371,41.8948822021484L28.8057518005371,41.0123023986816C28.8057498931885,38.1538047790527,26.4912424087524,35.8365287780762,23.6361503601074,35.8365287780762L17.2951822280884,35.8365287780762 17.2951822280884,30.8502292633057 23.6361503601074,30.8502292633057C26.4912424087524,30.8502292633057,28.8057498931885,28.5329551696777,28.8057518005371,25.6744518280029L28.8057518005371,24.7918758392334C28.8057498931885,21.9333724975586,26.4912424087524,19.6160984039307,23.6361503601074,19.6160984039307L17.2951822280884,19.6160984039307 17.2951822280884,14.9588947296143 23.6361503601074,14.9588947296143C26.4912424087524,14.9588956832886,28.8057498931885,12.641619682312,28.8057518005371,9.78311634063721L28.8057518005371,8.90054225921631C28.8057498931885,6.04203987121582,26.4912424087524,3.72476482391357,23.6361503601074,3.72476410865784L17.2951822280884,3.72476410865784z M0,0L0,115z";
	}
	public class IconeBalancete : IIcone
	{
		public string Data => "M3.5719823,32.931614 L3.5719823,43.951 9.4580618,43.951 9.4580618,33.081436 9.1958395,33.092117 C5.9088799,33.214787 4.137993,33.012733 3.6250521,32.939411 z M16.503158,31.740688 L16.353431,31.796181 C14.674122,32.403439 12.912424,32.840698 10.955207,32.998581 L10.617077,33.021927 10.617077,43.791996 16.503158,43.791996 z M23.886009,27.821775 C23.886009,27.821775 23.089122,28.556166 22.073484,29.149929 20.574905,30.026028 20.13928,30.173573 18.637104,30.85747 L17.980178,31.147789 17.980178,43.951 23.866258,43.951 z M29.745462,21.056002 C28.698991,22.674637 27.622102,24.590118 25.446218,26.561506 L25.292278,26.696751 25.289278,43.951 31.175357,43.951 31.182983,22.727913 z M31.649307,10.418 L24.551357,11.689747 27.305526,15.29122 C27.305526,15.29122 27.305404,15.290651 25.875217,18.680889 24.445032,22.071127 22.06147,24.454624 17.718055,27.73904 13.374518,31.023462 3.4659997,31.603296 3.4659997,31.603296 3.4659997,31.603296 5.4820053,31.976969 10.090315,31.712118 14.698624,31.447266 18.277507,29.778376 22.008369,27.527508 25.74094,25.275625 29.794801,18.734537 29.794801,18.734537 L31.808,21.383022 z M8.1265398,9.3059998 L8.1265398,11.102646 8.0234171,11.114459 C7.6020342,11.164896 7.2449914,11.229168 7.0052258,11.287398 6.2885152,11.461462 4.4700695,12.000871 3.9363348,13.76904 3.4026004,15.53721 4.0697686,16.747009 4.8370115,17.584564 5.6042549,18.422117 8.2729279,19.352732 9.5739056,19.755999 10.874882,20.159266 11.3419,20.903757 11.208466,21.803352 11.075034,22.70295 9.4071137,22.858051 7.6057602,22.733969 5.8044054,22.609886 3.936417,21.679272 3.9364168,21.679272 L4.0363661,24.222954 C4.0363656,24.222954 5.6678483,24.807608 7.1722895,24.998732 7.4157716,25.029663 7.6581789,25.052275 7.8982803,25.066189 L8.1265398,25.076677 8.1265398,27.18 9.8932044,27.18 9.8932044,24.958679 9.9636694,24.946201 C10.250575,24.890577 10.526809,24.817455 10.789457,24.725933 11.800747,24.373535 12.743117,24.037449 13.543712,22.982697 14.344309,21.927946 14.07715,20.03586 13.276584,19.074144 12.47602,18.112429 10.641392,17.491829 8.8734776,16.902405 7.1055638,16.312981 6.5652243,15.933531 6.5214502,14.997274 6.4686948,13.868944 7.3722159,13.365882 9.4404141,13.365882 11.508611,13.365882 13.07659,14.203713 13.07659,14.203713 L13.164675,11.896773 C13.164675,11.896773 12.609504,11.535325 11.175073,11.194093 10.816468,11.108785 10.424502,11.060317 10.026282,11.038511 L9.8932044,11.03233 9.8932044,9.3059998 z M1.5789995,4.5930004 L9.1339995,4.5930004 9.1339995,6.8369999 25.531004,6.8369999 25.531004,4.5930004 33.087001,4.5930004 C33.931442,4.5929999 34.616001,5.2775564 34.616001,6.1219997 L34.616001,45.596001 C34.616001,46.440441 33.931442,47.125 33.087001,47.125 L1.5789995,47.125 C0.73455521,47.125 0.049998532,46.440441 0.05,45.596001 L0.05,6.1219997 C0.049998532,5.2775564 0.73455521,4.5929999 1.5789995,4.5930004 z M17.333501,0.05 C18.278089,0.05 19.049419,0.79202062 19.096704,1.7251219 L19.098673,1.803 22.559002,1.803 C23.500095,1.8029999 24.263004,2.5659068 24.263004,3.507 L24.263004,3.8380003 24.263004,4.0069995 24.263004,5.7109995 22.559002,5.7109995 12.107001,5.7109995 10.403,5.7109995 10.403,4.0069995 10.403,3.8380003 10.403,3.507 C10.403,2.5659068 11.165908,1.8029999 12.107001,1.803 L15.56833,1.803 15.570299,1.7251219 C15.617585,0.79202062 16.388914,0.05 17.333501,0.05 z";
	}
	public class IconeCadastro : IIcone
	{
		public string Data => "M1.5965591,18.128744 L1.5965591,18.987999 8.8585139,18.987999 8.8585139,18.128744 z M12.413445,15.623424 L12.413445,16.482679 16.571678,16.482679 16.571678,15.623424 z M6.9472061,15.623424 L6.9472061,16.482679 11.10544,16.482679 11.10544,15.623424 z M1.5965591,15.623424 L1.5965591,16.482679 5.7547932,16.482679 5.7547932,15.623424 z M9.8420506,13.07182 L9.8420506,13.931074 16.595001,13.931074 16.595001,13.07182 z M1.5499171,13.07182 L1.5499171,13.931074 8.974105,13.931074 8.974105,13.07182 z M1.5499171,10.704343 L1.5499171,11.563597 16.595001,11.563597 16.595001,10.704343 z M1.5499171,8.1939907 L1.5499171,9.0532455 16.595001,9.0532455 16.595001,8.1939907 z M7.6813102,3.9872658 L7.6813102,4.8465199 12.012932,4.8465199 12.012932,3.9872658 z M7.6813102,2.0645075 L7.6813102,2.9237621 12.012932,2.9237621 12.012932,2.0645075 z M3.5969685,1.8181255 C3.4290739,1.8144089 3.1567257,1.8928829 3.0365611,2.1108816 2.8992289,2.360023 2.8667033,2.5236392 2.8667033,2.8880556 2.8667033,3.2524719 2.8667033,3.2524719 2.8667033,3.2524717 2.8667033,3.2524719 2.805265,3.2004242 2.7618972,3.226454 2.7185293,3.2524834 2.6968451,3.3305724 2.7149153,3.4384098 2.7329853,3.5462468 2.7871948,3.6503656 2.823335,3.7582026 2.8594754,3.8660402 2.8558613,3.9143808 2.8594757,3.951566 2.8630894,3.9887514 2.8775447,3.9961882 2.9136849,3.9961882 2.9498251,3.9961882 2.9678944,3.9924695 2.9678944,3.9924695 L3.1522077,4.6432114 3.3292936,4.8254189 3.3329073,5.279079 1.3380002,6.3287106 1.3717446,6.9332814 6.3963195,6.9332814 6.4300881,6.3704009 4.4108172,5.2863731 4.4108172,4.7919345 4.5797344,4.6506352 4.7568188,4.0036311 C4.7568188,4.0036311 4.7821179,4.0370884 4.8363276,4.0110593 4.8905373,3.9850292 4.8724676,3.9180958 4.9049931,3.8028214 4.9375187,3.6875472 5.0134121,3.4941602 5.0206404,3.3826036 5.0278683,3.2710471 5.0134121,3.2264485 4.9664307,3.2264488 4.9194494,3.2264485 4.8688532,3.2561982 4.8688532,3.2561982 4.8688532,3.2561982 4.9194494,2.7393229 4.8688532,2.5385222 4.8182584,2.3377216 4.7676621,1.8989273 4.297844,1.869179 3.828024,1.8394308 3.8135677,1.8543314 3.6617804,1.8245831 3.6428071,1.8208647 3.6209536,1.8186563 3.5969685,1.8181255 z M14.389002,0.41389328 L17.859236,4.382009 14.389002,4.382009 z M13.463002,0 L13.463002,5.2420082 13.586002,5.2420082 13.586002,5.2750082 18.061001,5.2750082 18.061001,21.848 0,21.848 0,0.047611929 z";
	}
	public class IconeCardapio : IIcone
	{
		public string Data => "M17.7779996782117,25.3575267791748L17.7779996782117,26.5830001831055 24.4559991746717,26.5830001831055 24.4559991746717,25.3575267791748z M17.7779996782117,22.0191287994385L17.7779996782117,23.2446022033691 24.4559991746717,23.2446022033691 24.4559991746717,22.0191287994385z M17.7779996782117,18.8183975219727L17.7779996782117,20.0438690185547 24.4559991746717,20.0438690185547 24.4559991746717,18.8183975219727z M17.7779996782117,15.4799995422363L17.7779996782117,16.7054710388184 24.4559991746717,16.7054710388184 24.4559991746717,15.4799995422363z M17.7779996782117,12.3235273361206L17.7779996782117,13.548999786377 24.4559991746717,13.548999786377 24.4559991746717,12.3235273361206z M17.7779996782117,8.98513031005859L17.7779996782117,10.2106018066406 24.4559991746717,10.2106018066406 24.4559991746717,8.98513031005859z M17.7779996782117,5.7843976020813L17.7779996782117,7.00987005233765 24.4559991746717,7.00987005233765 24.4559991746717,5.7843976020813z M17.7779996782117,2.44600009918213L17.7779996782117,3.67147278785706 24.4559991746717,3.67147278785706 24.4559991746717,2.44600009918213z M4.50251368578006,1.93499982357025L4.25875500734378,8.03503036499023C4.19781569536258,9.14108657836914,4.88642481859256,10.1588935852051,5.75184945161868,10.6039619445801L5.76546839769412,10.6105327606201 5.41236523683597,25.6847705841064C5.51935748155643,26.1698875427246,5.967830458241,26.5668277740479,6.5391605287366,26.5910015106201L6.64748505647708,26.5907955169678 6.75483922060062,26.5909996032715C7.32616881425906,26.5668258666992,7.77464179094363,26.169885635376,7.88163355882693,25.6847705841064L7.52853039796878,10.6105327606201 7.54215029771854,10.6039619445801C8.4075744539075,10.1588926315308,9.09618357713748,9.14108657836914,9.03524474199344,8.03503036499023L8.79148558672,1.93499982357025 8.09272555406619,1.93499982357025 8.09272555406619,7.6110577583313C8.09272555406619,7.70643854141235,8.01264933641482,7.78375911712646,7.9138696580701,7.78375911712646L7.76724890764285,7.78375911712646C7.66846922929813,7.78375911712646,7.58839301164676,7.70643854141235,7.5883925348096,7.6110577583313L7.5883925348096,1.93499982357025 6.88999966676761,1.93499982357025 6.88999966676761,7.63800001144409C6.88999966676761,7.73299312591553,6.81299284990359,7.80999994277954,6.71800021226932,7.80999994277954L6.57600001390506,7.80999994277954C6.48100689943362,7.80999994277954,6.40399960573245,7.73299312591553,6.40399960573245,7.63800001144409L6.40399960573245,1.93499982357025 5.70560673769046,1.93499982357025 5.70560673769046,7.6110577583313C5.70560673769046,7.70643854141235,5.62553004320193,7.78375959396362,5.52675036485721,7.78375959396362L5.3801291375928,7.78375959396362C5.28135041292239,7.78375959396362,5.20127371843387,7.70643854141235,5.20127324159671,7.6110577583313L5.20127324159671,1.93499982357025z M13.6840083986096,1.92000019550323C10.5277183442884,4.45087480545044 10.9340331941419,15.0289993286133 10.9340331941419,15.0289993286133 10.9340331941419,15.0289993286133 12.3409746080213,15.0130004882813 12.3409746080213,15.0130004882813L12.0910241990857,25.6847763061523C12.1980035692029,26.1698875427246,12.6464240938001,26.5668258666992,13.2176854997449,26.5909996032715L13.3259981065564,26.5907936096191 13.4333389192395,26.5909976959229C14.0046012788587,26.5668239593506 14.4530208497815,26.169885635376 14.5600002198987,25.6847763061523 14.5600002198987,25.6847763061523 13.6840083986096,1.91999995708466 13.6840083986096,1.92000019550323z M3.41799930627872,0.0560002215206623L24.7569997697644,0.0560002215206623C25.407039442616,0.0559999980032444,25.9339998155408,0.582960844039917,25.9339998155408,1.23300004005432L25.9339998155408,28.6909999847412C25.9339998155408,29.3410396575928,25.407039442616,29.8680000305176,24.7569997697644,29.8680000305176L3.41799930627872,29.8680000305176C2.76796034868289,29.8680000305176,2.24099949892093,29.3410396575928,2.24099902208377,28.6909999847412L2.24099902208377,1.23300004005432C2.24099949892093,0.582960844039917,2.76796034868289,0.0559999980032444,3.41799930627872,0.0560002215206623z M2.46599916513492,4.49018830398285E-30C1.81583789880801,-1.77635683940025E-15,1.28877786691714,0.526962876319885,1.28877822454501,1.1770042181015L1.28877822454501,28.6351089477539C1.28877786691714,29.2851505279541,1.81583789880801,29.8121128082275,2.46599916513492,29.8121128082275L1.17722062166263,29.8679981231689C0.527059474545013,29.8679981231689,-5.26996558392057E-07,29.3410358428955,3.97903932025656E-13,28.6909942626953L3.97903932025656E-13,1.2328896522522C-5.26996558392057E-07,0.582847893238068,0.527059474545013,0.0558850206434727,1.17722062166263,0.055884949862957z";
	}
	public class IconeCentroCusto : IIcone
	{
		public string Data => "M23.882446,18.254 L23.882446,19.502726 23.807232,19.510937 C23.499893,19.545992 23.239481,19.590664 23.064604,19.631136 22.541866,19.752115 21.215567,20.127022 20.826283,21.355955 20.436998,22.58489 20.923603,23.425739 21.483198,24.007866 22.042795,24.589993 23.989216,25.236799 24.938095,25.517082 25.886974,25.797367 26.227598,26.314812 26.130278,26.94006 26.032957,27.565308 24.816444,27.673107 23.502609,27.586866 22.188776,27.500626 20.826342,26.853819 20.826342,26.853819 L20.899241,28.621758 C20.899241,28.621758 22.089176,29.028111 23.186455,29.16095 23.36404,29.182447 23.540842,29.198164 23.715963,29.207832 L23.882446,29.215124 23.882446,30.677 25.170979,30.677 25.170979,29.133112 25.222374,29.124439 C25.431629,29.085779 25.633104,29.034958 25.824669,28.971346 26.562262,28.726418 27.249588,28.492826 27.83351,27.759741 28.417431,27.026655 28.222576,25.711596 27.638678,25.043173 27.054779,24.374748 25.716677,23.943411 24.427233,23.533743 23.137789,23.124075 22.743687,22.860344 22.71176,22.209618 22.673283,21.425392 23.332274,21.075748 24.840733,21.075748 26.349192,21.075748 27.492809,21.658068 27.492809,21.658068 L27.557055,20.05467 C27.557055,20.05467 27.152136,19.803452 26.105923,19.566284 25.84437,19.506992 25.558485,19.473307 25.26804,19.458151 L25.170979,19.453854 25.170979,18.254 z M24.452,0.1 C26.329769,0.1 27.851999,1.6222311 27.851999,3.4999995 27.851999,5.260407 26.514103,6.7083316 24.799629,6.8824463 L24.778009,6.8840904 24.778009,16.630129 25.172131,16.660099 C26.738752,16.819197 28.167736,17.443882 29.318531,18.393604 L29.579739,18.619867 36.717304,11.482686 36.666668,11.414971 C36.300064,10.872328 36.085999,10.218162 36.085999,9.513998 36.085999,7.636231 37.608231,6.1139989 39.486,6.1139979 41.363766,6.1139989 42.885998,7.636231 42.885998,9.513998 42.885998,11.391768 41.363766,12.913999 39.486,12.914 38.664475,12.914 37.911003,12.622634 37.323284,12.137604 L37.255428,12.075933 30.145405,19.185574 30.371393,19.446465 C31.321116,20.597261 31.945799,22.026245 32.1049,23.592867 L32.134869,23.987009 42.154907,23.987009 42.165173,23.906212 C42.414543,22.274191 43.824272,21.023998 45.525997,21.023998 47.403767,21.023998 48.925999,22.54623 48.925999,24.423998 48.925999,26.301767 47.403767,27.823999 45.525997,27.823999 43.824272,27.823999 42.414543,26.573807 42.165173,24.941786 L42.145504,24.786987 32.134869,24.786987 32.1049,25.181129 C31.945799,26.747751 31.321116,28.176735 30.371393,29.32753 L30.145403,29.588423 37.39056,36.833191 37.648026,36.640663 C38.19067,36.274059 38.844837,36.059998 39.549,36.059998 41.426766,36.059998 42.948998,37.58223 42.948998,39.459995 42.948998,41.337765 41.426766,42.859997 39.549,42.859997 37.67123,42.859997 36.148998,41.337765 36.148998,39.459995 36.148998,38.755833 36.363064,38.101669 36.729664,37.559025 L36.838921,37.412918 29.579739,30.154129 29.318531,30.380392 C28.167736,31.330114 26.738752,31.954798 25.172131,32.113899 L24.778009,32.143867 24.778009,42.290211 24.79563,42.29155 C26.510101,42.465664 27.848,43.913589 27.848,45.673996 27.848,47.551765 26.325768,49.073998 24.448,49.073998 22.570232,49.073998 21.048,47.551765 21.048,45.673996 21.048,43.972271 22.298193,42.562542 23.930214,42.313171 L23.977989,42.307102 23.977989,32.143867 23.583868,32.113899 C22.017246,31.954798 20.588263,31.330114 19.437466,30.380392 L19.17626,30.154129 12.071295,37.258713 12.163608,37.360279 C12.648637,37.948002 12.940002,38.701473 12.940002,39.522999 12.940002,41.400764 11.417769,42.922997 9.5400019,42.922997 7.6622329,42.922997 6.1400009,41.400764 6.1400009,39.522999 6.1400009,37.645229 7.6622329,36.122997 9.5400019,36.122997 10.244165,36.122997 10.898331,36.337059 11.440974,36.703663 L11.47187,36.726765 18.610596,29.588423 18.384605,29.32753 C17.434883,28.176735 16.810198,26.747751 16.6511,25.181129 L16.621128,24.786987 6.8956032,24.786987 6.8955799,24.787962 C6.8045164,26.584419 5.3190904,28.012999 3.5000017,28.012999 1.6222321,28.012999 0.10000005,26.490767 0.1,24.612999 0.10000005,22.735229 1.6222321,21.212999 3.5000017,21.212999 5.1430498,21.212999 6.5138898,22.378458 6.8309274,23.92778 L6.8415051,23.987009 16.621128,23.987009 16.6511,23.592867 C16.810198,22.026245 17.434883,20.597261 18.384605,19.446465 L18.610594,19.185574 11.63144,12.206794 11.377975,12.396333 C10.835331,12.762935 10.181166,12.976998 9.4770032,12.976998 7.5992332,12.976999 6.0770012,11.454767 6.0770012,9.5769987 6.0770012,7.6992307 7.5992332,6.1769996 9.4770032,6.1769986 11.35477,6.1769996 12.877002,7.6992307 12.877002,9.5769987 12.877002,10.281162 12.662938,10.935328 12.296335,11.477971 L12.184091,11.628074 19.17626,18.619867 19.437466,18.393604 C20.588263,17.443882 22.017246,16.819197 23.583868,16.660099 L23.977989,16.630129 23.977989,6.8663874 23.934214,6.8608241 C22.302193,6.6114554 21.052,5.2017264 21.052,3.4999995 21.052,1.6222311 22.574232,0.1 24.452,0.1 z";
	}
	public class IconeComissao : IIcone
	{
		public string Data => "M44.571884,96.263 L36.509003,105.0959 49.542862,125.05 59.198006,112.65949 z M76.898895,95.441002 L63.881996,113.31992 71.749964,125.37201 83.658003,104.61351 z M118.75554,56.338005 L126.78374,56.338005 126.78374,64.705727 C126.78374,64.705728 131.29005,65.188504 135.31314,66.475899 139.33624,67.763294 140.46292,69.050171 140.46292,69.05017 L140.46292,85.625399 C140.46292,85.625399 134.02563,79.832222 124.69213,79.510387 115.35863,79.188536 114.71493,83.694355 114.71493,85.786333 114.71493,87.878307 115.19816,91.901264 125.17516,95.763382 135.15218,99.6255 145.451,108.47614 145.451,116.03941 145.451,123.60266 143.35914,133.57897 136.43959,137.11922 129.52004,140.65947 127.10623,140.01596 127.10623,140.01596 L127.10623,150.95801 118.41656,150.95801 118.41656,140.82042 C118.41656,140.82042 109.56597,139.8549 106.83031,138.24571 104.09468,136.63652 102.1641,135.51007 102.1641,135.51007 L102.1641,117.96984 C102.1641,117.96984 110.37072,126.33766 121.31323,125.21122 132.25574,124.0848 132.41665,119.2572 130.00286,116.52157 127.58908,113.78594 118.57761,110.40664 113.10635,107.02732 107.6351,103.64801 103.4512,100.42962 101.03741,90.935364 98.623628,81.441111 101.92578,75.38303 106.27056,71.360042 110.61535,67.337051 115.60385,65.727859 115.60385,65.727861 L118.75554,65.205275 z M77.472658,19.475529 C80.587198,19.523006 79.522577,22.762596 80.491327,26.314678 81.491327,29.981342 83.657997,32.981346 85.991327,37.648014 88.324662,42.314676 84.157998,52.481344 84.157997,52.481343 84.157998,52.481344 86.824662,50.481227 89.824662,46.814561 92.824662,43.147892 93.491326,42.148228 95.15799,43.314893 96.824661,44.481556 95.491326,51.481191 93.824663,55.481195 92.15799,59.481202 91.991326,61.647874 91.158006,64.147875 90.324662,66.647882 88.158006,64.814878 88.158007,64.814878 L82.324663,83.648163 76.657999,88.981148 76.657999,92.147827 86.324661,104.48087 84.991327,106.64754 96.65799,110.14758 96.65799,139.98121 C96.65799,139.98121 96.65799,139.98121 101.65799,143.14791 106.65799,146.3146 112.491,147.48128 112.491,147.48128 L112.491,151.31466 71.324785,151.31466 64.491425,132.14781 68.158106,124.48106 61.203094,114.68462 54.460724,122.96565 58.888023,131.67479 51.925636,151.402 1.159996,151.402 0.5,123.4722 34.991333,107.98134 33.824661,104.14799 44.324673,92.314659 44.324673,88.981331 38.824669,84.147995 C38.824669,84.147995 33.824661,63.981332 32.491325,65.147998 31.157997,66.31466 29.657997,65.314668 29.657997,65.31467 29.657997,65.314668 30.491333,61.647996 28.657997,57.147992 26.824669,52.648 26.991325,52.814664 25.324669,45.981335 23.657997,39.148003 29.657997,44.314665 29.657997,44.314665 L37.324669,49.98133 C37.324669,49.98133 36.324669,46.981334 34.65799,41.314667 32.991325,35.648001 35.65799,33.148001 35.991333,30.981337 36.324669,28.814663 36.157993,22.981335 36.157993,22.981331 36.157993,22.981335 36.657993,23.647999 41.824673,23.981335 46.991333,24.314669 50.324657,24.314669 59.657997,22.981331 68.991335,21.647999 73.657999,19.647659 77.157999,19.480995 77.267374,19.475788 77.372194,19.473999 77.472658,19.475529 z M51.548042,0.50684738 C54.780571,0.40585327 57.345814,1.4604225 59.324963,1.1479225 62.4916,0.6479187 70.491091,1.9812546 75.491148,2.6479225 80.491207,3.3145905 80.491253,1.8141594 86.657859,11.480942 92.824465,21.147724 91.157808,41.647969 91.157807,41.64797 L88.324856,44.315001 C88.324856,44.315 88.824531,43.148302 88.491199,40.648229 88.157867,38.148155 87.157241,35.480911 84.990891,31.980974 82.824541,28.481035 82.657898,24.81477 82.6579,24.81477 82.657898,24.81477 82.285257,21.540722 80.951923,18.874054 79.618591,16.20739 73.991333,17.648003 73.991333,17.648003 73.991333,17.648003 65.158001,19.814667 51.658001,21.648003 38.158001,23.481335 33.824669,19.981335 33.824669,19.981335 33.824669,19.981335 33.824669,20.481335 34.324669,23.481335 34.824669,26.481335 34.324669,27.481335 32.824669,33.648001 31.324669,39.814669 33.491333,44.314668 33.491333,44.314668 L29.824997,41.648002 C29.824997,41.648002 30.491333,28.981335 30.491333,22.814667 30.491333,16.648003 33.65834,14.148003 35.324997,10.314667 36.991657,6.4813347 38.491272,6.3148537 44.99128,2.4815369 47.428783,1.0440445 49.608524,0.56744385 51.548042,0.50684738 z";
	}
	internal class IconeComposicao : IIcone
	{
		public string Data => "M39.427559,46.319977 L39.191563,46.31998 39.191578,55.402847 39.140526,55.658619 C38.841877,57.075401 38.310017,58.60268 36.781441,57.962864 34.976944,56.431713 33.965183,55.720814 32.734592,58.345665 32.351254,59.164665 32.178627,60.065552 32.194618,60.932033 32.210609,61.798519 32.415218,62.630604 32.786343,63.312008 34.454235,66.374336 35.449577,63.538273 36.880199,63.538277 37.982716,63.538277 38.702473,64.634415 39.175465,65.705925 L39.191593,65.744209 39.191605,73.892967 66.763512,73.893005 66.763474,46.320019 66.65097,46.320019 66.65097,46.505516 58.502239,46.505504 58.463955,46.521629 C57.392448,46.994621 56.296318,47.714378 56.296318,48.816891 56.296322,50.24752 59.132378,51.242874 56.070061,52.910763 55.388664,53.281883 54.55658,53.486492 53.690098,53.502483 52.82362,53.518467 51.922737,53.34584 51.103737,52.962498 48.478889,51.731895 49.189781,50.720135 50.720924,48.915642 51.360733,47.387062 49.833458,46.855194 48.416676,46.556541 L48.160904,46.505489 39.427559,46.505474 z M23.549026,39.340473 C22.682545,39.356464 21.850464,39.561069 21.169062,39.932194 18.106747,41.600082 20.942802,42.595432 20.942806,44.026066 20.942806,45.128574 19.846678,45.848331 18.775175,46.321323 L18.736889,46.337452 10.588151,46.33744 10.588193,73.909424 38.161102,73.909462 38.161102,73.892967 37.811607,73.892959 37.811596,65.744201 37.795471,65.705917 C37.558975,65.170166 37.260784,64.628258 36.883907,64.220283 L36.877888,64.214203 36.853855,64.264732 C36.218327,65.537323 35.256538,66.348465 34.180103,66.348465 32.266445,66.348457 30.715115,63.784839 30.715111,60.622459 30.715105,57.460075 32.26643,54.896465 34.180088,54.896469 35.376129,54.896469 36.43063,55.897884 37.053314,57.421009 L37.089806,57.51506 37.108189,57.489219 C37.429302,57.011127 37.620537,56.322731 37.760525,55.658615 L37.811581,55.402847 37.811569,46.337478 29.078218,46.337467 28.822451,46.286411 C27.405672,45.987758 25.878393,45.455894 26.518204,43.927311 28.049343,42.122822 28.760241,41.111057 26.135393,39.880459 25.316391,39.497112 24.415508,39.324486 23.549026,39.340473 z M10.571611,17.90595 L10.571653,45.478939 10.588151,45.478935 10.588148,45.12944 18.736887,45.129452 18.775173,45.113323 C18.909105,45.054203 19.043432,44.991222 19.175951,44.924122 19.308466,44.857021 19.439184,44.785797 19.565905,44.71019 19.629267,44.672386 19.691626,44.633484 19.752716,44.593452 19.813807,44.553421 19.873627,44.512256 19.931898,44.469936 19.990173,44.427612 20.0469,44.384125 20.101807,44.339439 20.156717,44.294754 20.209808,44.248878 20.260801,44.201767 L20.298601,44.164326 20.184162,44.115082 C18.787897,43.483192 17.884123,42.474689 17.884123,41.338447 17.884119,39.424785 20.447727,37.873455 23.610098,37.873459 26.772469,37.873466 29.336084,39.424801 29.336086,41.338463 29.336088,42.534508 28.334681,43.589008 26.811558,44.211693 L26.75194,44.234821 26.812384,44.290504 C27.063089,44.504795 27.400761,44.668556 27.774733,44.798767 27.940943,44.85664 28.114323,44.907883 28.290426,44.953995 28.466528,45.000107 28.645353,45.04108 28.822449,45.078411 L29.078217,45.129467 37.811565,45.129478 38.143559,45.129482 38.143547,36.396107 38.194599,36.140339 C38.493248,34.723553 39.025108,33.196277 40.553684,33.83609 42.358177,35.367241 43.369942,36.078144 44.600529,33.453293 44.983871,32.634289 45.156498,31.733402 45.140507,30.86692 45.124516,30.000437 44.919907,29.168352 44.548779,28.48695 42.88089,25.42462 41.885548,28.260681 40.454922,28.260679 39.352409,28.260677 38.632652,27.164543 38.159657,26.093033 L38.143532,26.054748 38.14352,17.905992 z M70.905579,3.9844065 L45.620346,14.980839 45.626926,14.995968 45.947426,14.856584 49.197258,22.329264 49.227314,22.357939 C49.334949,22.457186 49.446274,22.555248 49.560658,22.650011 49.675041,22.744776 49.792488,22.836243 49.912361,22.922296 49.972298,22.965322 50.032841,23.006998 50.093914,23.047054 50.154991,23.08711 50.216595,23.125551 50.278645,23.162109 50.340698,23.198669 50.403202,23.233347 50.466076,23.26588 50.528954,23.298412 50.592197,23.3288 50.655735,23.356777 50.719273,23.384754 50.783108,23.410322 50.847153,23.433216 50.911201,23.45611 50.97546,23.476332 51.039856,23.493612 51.104252,23.510898 51.168785,23.525242 51.233372,23.536384 51.297962,23.547527 51.362606,23.555469 51.427231,23.559944 51.491852,23.564421 51.556454,23.565428 51.620956,23.562704 51.685455,23.559986 51.749855,23.553531 51.814079,23.543085 51.8783,23.53264 51.942341,23.518198 52.006126,23.4995 52.069908,23.480799 52.133434,23.457842 52.196625,23.430361 52.237621,23.412531 52.277126,23.392332 52.315269,23.369993 52.353416,23.347652 52.390205,23.323173 52.425777,23.296787 52.461346,23.270399 52.495697,23.242103 52.528961,23.212131 52.562233,23.182159 52.594414,23.150507 52.625656,23.117412 52.656895,23.084314 52.687191,23.049772 52.716679,23.014013 52.746166,22.978254 52.774841,22.94128 52.802853,22.90332 52.858871,22.827404 52.912205,22.747549 52.963955,22.665602 53.015709,22.583656 53.065876,22.49962 53.115559,22.41534 53.165241,22.331062 53.214439,22.246538 53.264248,22.16362 53.314064,22.080704 53.364487,21.999391 53.416622,21.92153 53.442688,21.882601 53.469185,21.844532 53.49625,21.80756 53.523312,21.770586 53.550934,21.734707 53.579266,21.700153 53.607594,21.6656 53.636627,21.63237 53.666496,21.600696 53.696369,21.569025 53.727081,21.538908 53.75877,21.510578 53.790459,21.482248 53.823124,21.455704 53.856903,21.431179 53.890686,21.406652 53.925579,21.384146 53.961727,21.363888 53.997875,21.34363 54.035275,21.325619 54.074062,21.310091 54.151638,21.27903 54.234768,21.257889 54.324554,21.248514 54.36945,21.243828 54.416004,21.242082 54.464363,21.243509 54.512722,21.244938 54.562881,21.249538 54.614979,21.257541 54.667076,21.265545 54.721111,21.276951 54.777225,21.291994 54.833336,21.307035 54.891521,21.325712 54.95192,21.348253 55.012321,21.370796 55.074936,21.397203 55.1399,21.427709 55.204861,21.458212 55.272175,21.492813 55.341976,21.53174 55.411777,21.570667 55.484066,21.613924 55.558983,21.661737 55.596439,21.685646 55.634552,21.710691 55.67334,21.736906 55.712124,21.763122 55.751583,21.790504 55.791737,21.819086 55.831886,21.847666 55.872726,21.877445 55.914276,21.908447 55.955822,21.939453 55.998081,21.971681 56.041061,22.005169 56.117573,22.064775 56.192635,22.12758 56.266121,22.19339 56.339607,22.259201 56.411518,22.328016 56.48172,22.399647 56.551922,22.471275 56.620415,22.545721 56.687073,22.622789 56.753731,22.699856 56.81855,22.77955 56.881405,22.861673 56.944256,22.943796 57.005138,23.028354 57.063927,23.115152 57.122711,23.201952 57.179401,23.290989 57.233864,23.38208 57.288322,23.473169 57.340557,23.566309 57.39043,23.66131 57.440308,23.756309 57.487823,23.853168 57.532852,23.951694 57.577881,24.050222 57.620422,24.150417 57.660343,24.25209 57.700264,24.353762 57.737568,24.456911 57.772121,24.561348 57.806679,24.665785 57.838486,24.771507 57.867413,24.878322 57.896339,24.985142 57.92239,25.093052 57.945427,25.201868 57.968468,25.310686 57.988499,25.420404 58.005394,25.530838 58.022282,25.641271 58.036037,25.752418 58.04652,25.864086 58.057007,25.975754 58.06422,26.087944 58.068035,26.200466 58.07185,26.312988 58.072269,26.425838 58.069153,26.53883 58.064049,26.719946 58.054535,26.888773 58.040623,27.045971 58.033669,27.124569 58.025612,27.200258 58.01646,27.273126 57.998161,27.418861 57.975475,27.553299 57.948421,27.677097 57.81316,28.296102 57.568729,28.649208 57.217442,28.819126 56.69051,29.074003 55.923145,28.916704 54.923134,28.626375 54.789799,28.587665 54.652328,28.546589 54.510738,28.503811 54.303623,28.50667 54.124294,28.534372 53.969608,28.583429 L53.858166,28.624172 53.755672,28.672598 C53.166954,28.985536 53.045467,29.69565 53.104942,30.485403 53.11816,30.660902 53.140312,30.840334 53.168259,31.020218 53.196205,31.200098 53.22995,31.380428 53.266346,31.55772 L53.321529,31.81263 56.804516,39.821419 56.93692,40.125866 64.945679,36.642895 65.200584,36.587711 C66.618927,36.296551 68.231598,36.175194 68.254494,37.832115 67.570038,40.097538 67.321632,41.308872 70.219475,41.390549 71.123405,41.415459 72.018387,41.214477 72.806602,40.854252 73.594818,40.494022 74.27626,39.974548 74.75312,39.362461 76.896187,36.611656 73.898476,36.829948 73.327919,35.518013 72.888222,34.506973 73.606361,33.409778 74.40033,32.548698 L74.429008,32.518638 81.901657,29.26882 z";
	}
	public class IconeConferenciaEstoque : IIcone
	{
		public string Data => "M22.3330009460449,57.4329975128174L22.3330009460449,60.2549999237061 31.3669984817505,60.2549999237061 31.3669984817505,57.4329975128174z M6.47668533325195,53.5810077667236L16.34931640625,53.5810077667236 16.34931640625,63.3509929656982 6.47668533325195,63.3509929656982z M22.3330009460449,52.4869983673096L22.3330009460449,55.3090007781982 33.6170008659363,55.3090007781982 33.6170008659363,52.4869983673096z M4.05200271606445,51.1809986114502L4.05200271606445,65.7510021209717 18.7740009307861,65.7510021209717 18.7740009307861,51.1809986114502z M22.3330009460449,44.4989998817444L22.3330009460449,47.3209975242615 35.7879977703094,47.3209975242615 35.7879977703094,44.4989998817444z M66.1363132476807,40.0189955785871L71.1299961090088,43.6361525535583 50.8615243911743,71.6299942016602 39.4459979377687,54.1624563217163 44.7322910308838,50.7239330291748 51.1677711486816,60.64739112854z M22.3330009460449,39.5530009746551L22.3330009460449,42.3749986171722 39.1279975652695,42.3749986171722 39.1279975652695,39.5530009746551z M6.47668533325195,35.6310070037842L16.34931640625,35.6310070037842 16.34931640625,45.4009883880615 6.47668533325195,45.4009883880615z M22.3330009460449,34.5169990539551L22.3330009460449,37.3389978885651 48.1439970016479,37.3389978885651 48.1439970016479,34.5169990539551z M4.05200271606445,33.2309983253479L4.05200271606445,47.8009980201721 18.7740009307861,47.8009980201721 18.7740009307861,33.2309983253479z M22.3330009460449,26.5479991912842L22.3330009460449,29.3699977874756 48.1439970016479,29.3699977874756 48.1439970016479,26.5479991912842z M22.3330009460449,21.6020000457764L22.3330009460449,24.4239986419678 48.1439970016479,24.4239986419678 48.1439970016479,21.6020000457764z M15.557022857666,17.0169971466064L17.3699996948242,18.3301551818848 10.0114524841309,28.4929931640625 5.86699752807617,22.1516082763672 7.7862060546875,20.9032981872559 10.1226318359375,24.5058944702148z M22.3330009460449,16.565998840332L22.3330009460449,19.3879993438721 41.9739983558655,19.3879993438721 41.9739983558655,16.565998840332z M4.05200271606445,15.3369987487793L4.05200271606445,29.9069984436035 18.7740009307861,29.9069984436035 18.7740009307861,15.3369987487793z M2.77200012207031,6.96699981689453L10.9700000762939,6.96699981689453 10.9700000762939,12.126997756958 40.5499985694885,12.126997756958 40.5499985694885,6.96699981689453 48.7479999542236,6.96699981689453C50.2513187408447,6.96699981689453,51.4700000762939,8.18567924499511,51.4700000762939,9.68899803161621L51.4700000762939,56.2703922271728 45.4904053688049,47.0949967384338 35.8000026226044,53.3786790847778 47.0353229522705,70.4309986114502 2.77200012207031,70.4309986114502C1.26868133544922,70.4309986114502,0.0499999999999972,69.212317276001,0.0499999999999972,67.7089984893799L0.0499999999999972,9.68899803161621C0.0499999999999972,8.18567924499511,1.26868133544922,6.96699981689453,2.77200012207031,6.96699981689453z M25.6210000991821,0.0499999999999972C27.2496879577637,0.0499999999999972,28.5700004577637,1.3705337524414,28.5700004577637,2.99949722290039L28.5683448791504,3.06499938964843 34.8090003013611,3.06499938964843C36.4498386859894,3.06499938964843,37.7800006151199,4.39516143798828,37.7800010919571,6.03600006103515L37.7800010919571,6.25399856567383 37.7800010919571,6.52200012207031 37.7800010919571,9.4929988861084 34.8090003013611,9.4929988861084 16.4330013275146,9.4929988861084 13.4620006561279,9.4929988861084 13.4620006561279,6.52200012207031 13.4620006561279,6.25399856567383 13.4620006561279,6.03600006103515C13.4620006561279,4.39516143798828,14.7921627044678,3.06499938964843,16.4330013275146,3.06499938964843L22.6736572265625,3.06499938964843 22.6720016479492,2.99949722290039C22.6720016479492,1.3705337524414,23.9923131942749,0.0499999999999972,25.6210000991821,0.0499999999999972z";
	}
	public class IconeConfiguracao : IIcone
	{
		public string Data => "M19.448251,39.105125 C20.984827,39.142254 22.273876,40.337875 22.394596,41.903183 22.523365,43.572842 21.274068,45.030865 19.604217,45.159763 17.934366,45.288658 16.476297,44.039623 16.347528,42.369961 16.238882,40.961182 17.111297,39.703075 18.389619,39.266888 18.626346,39.186111 18.876995,39.133522 19.137908,39.11338 19.242275,39.105324 19.345813,39.10265 19.448251,39.105125 z M19.535076,35.684792 C19.317064,35.679523 19.096714,35.685211 18.874601,35.702358 18.430376,35.736649 18.000137,35.815136 17.587862,35.933178 L17.511001,35.956226 17.639108,37.617317 17.580566,37.639763 C17.322534,37.74239 17.075956,37.866386 16.842834,38.009422 L16.744979,38.07111 15.481227,36.988533 15.361631,37.078896 C14.712642,37.594238 14.167352,38.230633 13.757601,38.950916 L13.686645,39.082943 14.950397,40.165524 14.904442,40.271667 C14.798844,40.523945 14.714123,40.786598 14.652281,41.057289 L14.639079,41.118576 12.977802,41.246811 12.966814,41.326286 C12.913398,41.75174 12.901828,42.188877 12.936084,42.633049 12.97034,43.077225 13.0488,43.507412 13.166817,43.919632 L13.189863,43.996479 14.851283,43.868233 14.873545,43.9263 C14.976162,44.184296 15.100151,44.430836 15.243183,44.663921 L15.305059,44.762062 14.222104,46.025951 14.312465,46.145527 C14.827807,46.794403 15.464223,47.339581 16.184549,47.749222 L16.316585,47.820164 17.399677,46.55611 17.505502,46.60191 C17.757799,46.707478 18.020471,46.792164 18.291185,46.853977 L18.352476,46.867172 18.480621,48.528744 18.560104,48.539722 C18.985599,48.593098 20.74147,48.457561 21.153745,48.33952 L21.230604,48.316467 21.102449,46.654755 C21.102449,46.654755 21.66513,46.405865 21.898254,46.262829 L21.996107,46.201141 23.260377,47.284164 23.379974,47.193802 C24.028965,46.678459 24.574255,46.042065 24.984005,45.321781 L25.054962,45.189754 23.790691,44.106728 23.836643,44.000584 C23.942245,43.748306 24.026962,43.485653 24.088806,43.214966 L24.101902,43.154171 25.763803,43.025887 25.774793,42.946411 C25.828208,42.520958 25.839777,42.08382 25.805521,41.639648 25.771265,41.195473 25.692806,40.765285 25.574789,40.353062 L25.551742,40.276215 23.889986,40.404488 23.86754,40.345955 C23.764926,40.087955 23.640935,39.841415 23.497903,39.60833 L23.436409,39.510796 24.519502,38.246746 24.429143,38.127171 C23.913799,37.478294 23.277383,36.933117 22.557058,36.523472 L22.425022,36.452534 21.342065,37.716423 21.235584,37.670341 C20.983287,37.564777 20.720617,37.480087 20.449901,37.418274 L20.389102,37.405186 20.260986,35.743954 20.181503,35.732971 C19.968755,35.706284 19.753086,35.69006 19.535076,35.684792 z M36.523914,31.021652 C37.391296,31.015211 38.260261,31.237768 39.032321,31.689707 39.380905,31.893759 39.709732,32.144569 40.009742,32.442173 41.929805,34.346848 41.942318,37.446983 40.037693,39.366528 38.133071,41.286068 35.03255,41.298119 33.112487,39.393448 31.192426,37.488773 31.179912,34.388634 33.084537,32.469093 33.636951,31.912352 34.289974,31.516073 34.985607,31.280483 35.483623,31.111822 36.003482,31.025517 36.523914,31.021652 z M36.519054,25.496988 C35.852275,25.49958 35.185756,25.565756 34.529369,25.695477 L34.293117,25.748106 34.303962,28.435087 34.125736,28.490955 C33.705959,28.628159 33.295543,28.801826 32.899452,29.01194 L32.810619,29.060486 30.900417,27.165596 30.791687,27.236046 C30.215349,27.619795 29.668899,28.067013 29.162221,28.577662 28.655542,29.08831 28.212612,29.638218 27.833393,30.217508 L27.763799,30.326778 29.674163,32.221832 29.626701,32.310322 C29.419708,32.708019 29.249275,33.119747 29.115385,33.540546 L29.060756,33.719742 26.373062,33.73019 26.322301,33.96682 C26.073236,35.281448 26.078701,36.63459 26.338369,37.947235 L26.391037,38.183464 29.079074,38.173016 29.134801,38.350677 C29.272083,38.770424 29.445835,39.180813 29.656034,39.576889 L29.704597,39.665722 27.809206,41.575958 27.87968,41.684685 C28.263563,42.261009 30.282533,44.263798 30.861949,44.643055 L30.971244,44.712654 32.866802,42.802254 C32.866802,42.802254 33.764888,43.227222 34.18576,43.361156 L34.364433,43.415638 34.375282,46.103725 34.611949,46.154514 C35.926776,46.403744 37.280086,46.398479 38.592857,46.139038 L38.829109,46.086414 38.81826,43.398323 38.996486,43.342453 C39.416263,43.205254 39.826675,43.031586 40.22277,42.821472 L40.31089,42.773315 42.221809,44.668919 42.330539,44.598473 C42.906879,44.214722 43.453323,43.767502 43.960002,43.256855 44.466686,42.746204 44.909614,42.196297 45.288837,41.617008 L45.358425,41.507736 43.447673,39.612293 43.495521,39.523087 C43.702518,39.125393 43.872947,38.713661 44.006835,38.292866 L44.061126,38.114773 46.749164,38.104324 46.799922,37.867695 C47.048988,36.55307 47.043529,35.199924 46.783859,33.88728 L46.731193,33.651051 44.043495,33.661499 43.987419,33.482735 C43.850135,33.062988 43.676387,32.652596 43.46619,32.256523 L43.418014,32.168404 45.313022,30.258556 45.242549,30.149832 C44.858665,29.573507 44.411308,29.027061 43.90052,28.520369 43.389736,28.013674 42.839695,27.570719 42.260276,27.191462 L42.150981,27.121859 40.256137,29.031544 40.166915,28.983688 C39.769138,28.776663 39.357334,28.606192 38.936462,28.472254 L38.757785,28.417772 38.74694,25.730793 38.510273,25.680004 C37.852859,25.555389 37.185829,25.494396 36.519054,25.496988 z M18.911085,16.111578 C20.289524,16.122637 21.640935,16.653624 22.662008,17.63316 22.987978,17.945869 23.280279,18.304291 23.529047,18.706106 25.121158,21.277721 24.327169,24.652843 21.755628,26.244659 19.184083,27.836472 15.808775,27.042187 14.216663,24.470572 12.624551,21.898958 13.418538,18.523832 15.990082,16.932018 16.329105,16.72216 16.682096,16.553774 17.04313,16.425467 17.652376,16.208944 18.284523,16.10655 18.911085,16.111578 z M19.055395,9.936655 L18.367221,12.861976 18.158824,12.876331 C17.666528,12.916248 17.174913,12.998263 16.689384,13.123648 L16.580116,13.153316 14.996182,10.59491 14.859574,10.643218 C14.132772,10.910522 13.421947,11.254642 12.737847,11.678105 12.053751,12.101571 11.42876,12.584336 10.86541,13.11565 L10.761254,13.216376 12.345325,15.775003 12.270651,15.85889 C11.941922,16.237541 11.649301,16.640974 11.394061,17.063791 L11.287963,17.244469 8.3620677,16.555941 8.2452169,16.800095 C7.6318655,18.165089 7.2853084,19.638258 7.2257786,21.133577 L7.2215233,21.404226 10.147791,22.092842 10.162121,22.300589 C10.202086,22.792875 10.284153,23.284483 10.409589,23.770008 L10.439267,23.879274 7.8801866,25.463373 7.9285131,25.599981 C8.1958947,26.326784 9.8700195,29.03087 10.401406,29.594254 L10.502143,29.698416 13.061446,28.114183 C13.061446,28.114183 13.927512,28.810266 14.350367,29.06554 L14.530503,29.171324 13.842047,32.097847 14.086218,32.214718 C15.451316,32.828178 16.924564,33.174866 18.419939,33.234539 L18.690599,33.238823 19.379055,30.312296 19.587451,30.297941 C20.07975,30.258026 20.571363,30.17601 21.056894,30.050627 L21.165281,30.021196 22.749811,32.580566 22.886421,32.532257 C23.613222,32.264954 24.324047,31.920834 25.008144,31.49737 25.692245,31.073904 26.317232,30.591139 26.880584,30.059826 L26.984739,29.959101 25.400344,27.39995 25.475622,27.315382 C25.804357,26.936733 26.096973,26.533297 26.352214,26.110479 L26.457658,25.930918 29.383926,26.619534 29.500776,26.375383 C30.114126,25.010387 30.460687,23.537218 30.520216,22.041901 L30.524471,21.771252 27.598573,21.08272 27.584156,20.873684 C27.544189,20.381397 27.462125,19.889791 27.33669,19.404263 L27.307249,19.295877 29.865806,17.712103 29.817482,17.575497 C29.5501,16.848692 29.205902,16.137854 28.782358,15.453737 28.358814,14.76962 27.875974,14.144606 27.344589,13.581221 L27.243848,13.477057 24.685514,15.060697 24.600936,14.985413 C24.22224,14.656657 23.818763,14.364007 23.39591,14.108734 L23.215772,14.00295 23.903945,11.077629 23.659774,10.960758 C22.294677,10.347296 20.821428,10.000607 19.326053,9.940937 z";
	}
	public class IconeAlteracaoRapida : IIcone
	{
		public string Data => "M30.6801509849727,43.176776650548L36.3787994377315,48.8770864039659 31.6689224235713,50.6745374232531 28.9033756248653,48.3158948451281z M4.70301151201129,39.9074933558702L4.70316553041339,52.5324285060167 17.139102935046,52.5325810939074 17.1390056602657,44.6121213465929 15.499038695544,46.6733396083117 15.4990901939571,50.8685681849718 6.34313726350665,50.8684575587511 6.34302329942584,41.5715062648058 13.7883853904903,41.5715978175402 15.1122961036861,39.90761924088z M17.3231010429561,38.5696523219347L11.819914817065,45.8978421717882 9.4539012901485,42.8967587977648 7.52505206987262,44.6362683802843 11.7664880745113,50.0325238734484 19.1489391319454,40.1265981227159z M21.8358058921993,30.359752419591L21.8358268730342,32.0527417689562 34.5297622673213,32.0528981715441 34.529743193835,30.3599069148302z M21.8357563011348,26.3277738124132L21.8357772819698,28.0207650691271 34.5297126762569,28.0209195643663 34.5296897880733,26.3279283076525z M46.4586029045284,25.5647370845079L30.0777950279415,41.9379308253527 26.0456027977169,53.7658956080675 37.5595016472042,49.8811643153429 54.1672630302608,33.273399117589z M4.70280933305621,23.4845769435167L4.70296478196979,36.1095120936632 17.1389026634395,36.1096646815538 17.1388053886592,28.189206841588 15.4988374702632,30.2504231959581 15.4988889686763,34.4456517726183 6.34293603822589,34.4455411463976 6.34282159730792,25.1485879451036 13.7881841652095,25.148679497838 15.112093924731,23.4847047358751z M17.322898864001,22.1467378169298L11.8197145454586,29.4749276667833 9.45370006486774,26.4738442927599 7.52485084459186,28.2133500605822 11.7662878029048,33.6096074610949 19.1487369529903,23.7036798030138z M21.8356056205928,14.0818336039782L21.8356285087764,15.7748267680407 34.5295600883663,15.7749831706285 34.5295410148799,14.0819919139147z M21.8355560295284,10.0498559504747L21.8355770103633,11.7428481608629 34.5295104973018,11.7430036097765 34.5294914238155,10.0500113993883z M4.70260810777545,7.06066155731678L4.70276355668902,19.6855962306261 17.1387004844844,19.6857488185167 17.1386032097042,11.7652919322252 15.4986362449825,13.8265063792467 15.4986867897213,18.0217359095812 6.34273433610797,18.0216233760118 6.34262132570148,8.72467208206654 13.7879829399288,8.72476458847523 15.1118946067989,7.06078982651234z M17.322696685046,5.72282290756702L11.8195123665035,13.0510108500719 9.45349979326129,10.0499284297228 7.52465009614825,11.7894361048937 11.7660865776241,17.1856935054064 19.1485366813838,7.27976632416248z";
	}
	public class IconeConta : IIcone
	{
		public string Data => "M30.016001,31.798001 L40.096001,31.798001 40.096001,34.146001 30.016001,34.146001 z M30.016001,27.594001 L43.139999,27.594001 43.139999,29.942 30.016001,29.942 z M30.016001,23.708001 L46.005001,23.708001 46.005001,26.056 30.016001,26.056 z M30.016001,19.544001 L46.005001,19.544001 46.005001,21.892001 30.016001,21.892001 z M30.016001,15.460001 L46.005001,15.460001 46.005001,17.808001 30.016001,17.808001 z M16.653215,14.15 L18.794241,14.15 18.794241,16.013575 18.955517,16.020247 C19.438122,16.043788 19.913147,16.096108 20.347744,16.188199 22.086132,16.556559 22.758945,16.946742 22.758945,16.946742 L22.652195,19.437087 C22.652195,19.437087 20.751959,18.532647 18.245502,18.532647 15.739047,18.532647 14.644068,19.075703 14.708001,20.293736 14.761051,21.304425 15.415891,21.714043 17.558432,22.350325 19.700972,22.986611 21.924362,23.656548 22.894569,24.694722 23.864777,25.732896 24.188547,27.775403 23.218302,28.914008 22.248058,30.052613 21.105997,30.415419 19.880411,30.795834 19.562109,30.894633 19.227339,30.973569 18.879639,31.033614 L18.794241,31.047085 18.794241,33.445 16.653215,33.445 16.653215,31.174464 16.376585,31.163138 C16.085606,31.148122 15.791832,31.123711 15.496756,31.090319 13.673519,30.884 11.69632,30.252866 11.69632,30.252866 L11.575192,27.506961 C11.575192,27.506961 13.839013,28.511559 16.022079,28.645507 18.205145,28.779452 20.226503,28.612021 20.388212,27.640906 20.549919,26.669793 19.98394,25.866112 18.407282,25.430785 16.830622,24.995459 13.59645,23.990859 12.666625,23.086718 11.736801,22.182578 10.928258,20.876597 11.575092,18.967856 12.221927,17.059117 14.425707,16.476824 15.29429,16.288922 15.584863,16.226062 16.017565,16.15668 16.52824,16.102235 L16.653215,16.089482 z M9.7959995,8.68 L9.7959995,11.358 13.662,11.358 13.662,8.68 z M8.6859999,7.7560007 L47.574001,7.7560007 47.574001,12.554001 8.6859999,12.554001 z M7.875,6.384001 C7.1266542,6.3840005 6.52,6.9906547 6.52,7.739001 L6.52,34.443001 C6.52,35.191346 7.1266542,35.798001 7.875,35.798001 L48.815002,35.798001 C49.563351,35.798001 50.170002,35.191346 50.170002,34.443001 L50.170002,7.739001 C50.170002,6.9906547 49.563351,6.3840005 48.815002,6.384001 z M7.671,4.8170001 L48.679005,4.8170001 C50.292778,4.8170006 51.601002,6.1252248 51.601002,7.739001 L51.601002,37.099 C51.601002,38.712777 50.292778,40.021001 48.679005,40.021001 L7.671,40.021001 C6.0572243,40.021001 4.7490001,38.712777 4.7490006,37.099 L4.7490006,7.739001 C4.7490001,6.1252248 6.0572243,4.8170006 7.671,4.8170001 z M2.997,0.075 L44.005001,0.075 C45.618774,0.075000253 46.926998,1.3832243 46.926998,2.9970006 L46.926998,3.9759998 45.495998,3.9759998 45.495998,2.9970006 C45.495998,2.2486543 44.889347,1.6420003 44.140999,1.6420005 L3.2009997,1.6420005 C2.4526541,1.6420003 1.846,2.2486543 1.8460004,2.9970006 L1.8460004,29.701001 C1.846,30.449347 2.4526541,31.056 3.2009997,31.056 L3.8060002,31.056 3.8060002,35.279 2.997,35.279 C1.383224,35.279 0.075,33.970776 0.075,32.356999 L0.075,2.9970006 C0.075,1.3832243 1.383224,0.075000253 2.997,0.075 z";
	}
	public class IconeCusto : IIcone
	{
		public string Data => "M8.0954903,30.616367 C7.7002304,30.616367 7.3798092,30.934742 7.3798092,31.327477 L7.3798092,34.73261 C7.3798092,35.125352 7.7002304,35.443723 8.0954903,35.443723 L12.19598,35.443723 C12.245387,35.443723 12.293626,35.438746 12.340215,35.429272 L12.344115,35.428277 12.344312,35.427785 12.408799,35.411307 12.508712,35.371489 12.517717,35.367181 12.604027,35.3158 12.69763,35.239058 12.721614,35.211877 12.778703,35.143124 12.828615,35.064698 12.830847,35.060098 12.870605,34.968182 12.87516,34.955819 12.900006,34.857152 12.9029,34.838303 12.911657,34.732121 12.911657,33.367997 12.911661,31.327477 C12.911661,30.934742 12.591241,30.616367 12.19598,30.616367 z M2.015807,30.616367 C1.6205471,30.616367 1.3001259,30.934742 1.3001259,31.327477 L1.3001259,34.73261 C1.3001259,35.125352 1.6205471,35.443723 2.015807,35.443723 L6.1162985,35.443723 C6.5115574,35.443723 6.8319786,35.125352 6.8319786,34.73261 L6.8319786,31.327477 C6.8319786,30.934742 6.5115574,30.616367 6.1162985,30.616367 z M8.0954903,24.967297 C7.7002304,24.967297 7.3798092,25.285739 7.3798092,25.678556 L7.3798092,29.084417 C7.3798092,29.477237 7.7002304,29.795677 8.0954903,29.795677 L12.19598,29.795677 C12.467722,29.795677 12.704089,29.645164 12.825282,29.423447 L12.839055,29.395034 12.838352,29.391822 12.855416,29.360582 C12.891631,29.275503 12.911657,29.181957 12.911657,29.083767 L12.911657,25.678459 12.907965,25.605833 C12.871317,25.247175 12.566537,24.967297 12.19598,24.967297 z M2.015807,24.967297 C1.6205471,24.967297 1.3001259,25.285739 1.3001259,25.678556 L1.3001259,29.084417 C1.3001259,29.477237 1.6205471,29.795677 2.015807,29.795677 L6.1162985,29.795677 C6.5115574,29.795677 6.8319786,29.477237 6.8319786,29.084417 L6.8319786,25.678556 C6.8319786,25.285739 6.5115574,24.967297 6.1162985,24.967297 z M27.803447,19.970623 L27.803447,21.705412 C26.686651,21.817334 25.789553,22.216833 25.112155,22.903909 24.434753,23.590986 24.096054,24.422627 24.096054,25.398832 24.096054,26.306644 24.356943,27.076106 24.878724,27.707222 25.400508,28.338336 26.375414,28.924371 27.803447,29.465326 L27.806841,29.459019 29.139938,30.006424 29.139938,29.996955 C30.012625,30.382467 30.448968,30.879897 30.448968,31.489248 30.448968,32.218097 30.039469,32.658102 29.220473,32.809253 L27.803447,32.860286 C27.254205,32.822979 26.628675,32.679969 25.926865,32.431254 25.225053,32.18254 24.697171,31.912061 24.343214,31.619822 L24.343214,34.203349 C25.411186,34.769175 26.564598,35.055198 27.803447,35.061415 L27.803447,37.029376 29.139938,37.029376 29.139938,35.00546 C30.342171,34.868663 31.266732,34.48471 31.913617,33.853596 32.560504,33.22248 32.883946,32.390838 32.883948,31.35867 32.883946,30.463297 32.594069,29.70005 32.014313,29.068935 31.434556,28.437821 30.47643,27.876657 29.139938,27.385446 L27.803447,26.844489 C26.95517,26.415458 26.531032,25.88072 26.531032,25.240278 26.531032,24.494132 26.95517,24.043337 27.803447,23.88789 L29.139938,23.850581 C30.207912,23.968722 31.169087,24.292051 32.023467,24.82057 L32.023467,22.302329 C31.388785,21.941692 30.427608,21.730285 29.139938,21.668106 L29.139938,19.970623 z M8.0954893,19.257475 C8.0460814,19.257475 7.9978436,19.262449 7.9512537,19.271923 L7.8826674,19.289446 7.8169144,19.313364 C7.560046,19.421339 7.3798092,19.674116 7.3798092,19.96873 L7.3798092,23.37459 C7.3798092,23.76741 7.7002304,24.085851 8.0954903,24.085851 L10.001415,24.085851 10.000769,24.085179 12.195977,24.085179 C12.591235,24.085178 12.911657,23.766781 12.911657,23.374016 L12.911657,19.968634 12.907965,19.896002 12.897121,19.825386 C12.849454,19.593878 12.688614,19.403343 12.474557,19.313364 L12.44766,19.30358 12.408799,19.289446 C12.341569,19.268669 12.270088,19.257475 12.195977,19.257475 z M2.015806,19.257475 C1.941695,19.257475 1.8702152,19.268669 1.802985,19.289446 L1.7920254,19.293432 1.737232,19.313364 C1.4803627,19.421339 1.3001259,19.674116 1.3001259,19.96873 L1.3001259,23.37459 C1.3001259,23.76741 1.6205471,24.085851 2.015807,24.085851 L6.1162985,24.085851 C6.5115574,24.085851 6.8319786,23.76741 6.8319786,23.37459 L6.8319786,21.837873 6.8319786,19.96873 C6.8319786,19.674116 6.6517418,19.421339 6.3948725,19.313364 L6.34321,19.294571 6.3291195,19.289446 C6.2618893,19.268669 6.1904095,19.257475 6.1162975,19.257475 z M28.490001,17.050001 C34.813662,17.050001 39.940001,22.17634 39.940001,28.500002 39.940001,34.823661 34.813662,39.950002 28.490001,39.950002 22.16634,39.950002 17.040001,34.823661 17.04,28.500002 17.040001,22.17634 22.16634,17.050001 28.490001,17.050001 z M14.170023,13.499244 C13.774764,13.499244 13.454343,13.817617 13.454343,14.210351 L13.454343,17.615489 C13.454343,18.008224 13.774764,18.326595 14.170023,18.326596 L18.27051,18.326596 C18.66577,18.326595 18.986193,18.008224 18.986193,17.615489 L18.986193,14.210351 C18.986193,13.817617 18.66577,13.499244 18.27051,13.499244 z M8.0954903,13.499244 C7.7002304,13.499244 7.3798092,13.817617 7.3798092,14.210351 L7.3798092,17.615489 C7.3798092,18.008224 7.7002304,18.326595 8.0954903,18.326596 L12.195977,18.326596 C12.591235,18.326595 12.911657,18.008224 12.911657,17.615489 L12.911657,14.210351 C12.911657,13.817617 12.591235,13.499244 12.195977,13.499244 z M2.015807,13.499244 C1.6205471,13.499244 1.3001259,13.817617 1.3001259,14.210351 L1.3001259,17.615489 C1.3001259,18.008224 1.6205471,18.326595 2.015807,18.326596 L6.1162947,18.326596 C6.5115564,18.326595 6.8319767,18.008224 6.8319767,17.615489 L6.8319767,14.210351 C6.8319767,13.817617 6.5115564,13.499244 6.1162947,13.499244 z M17.871848,2.5584114 L21.734482,2.5584114 C22.269311,2.5584114 22.62091,2.7132051 22.789282,3.0227917 22.838801,3.1661194 22.863561,3.2807806 22.863561,3.3667763 L22.863561,5.8434704 C23.111167,6.0154627 23.23497,6.244787 23.23497,6.5314405 L23.23497,9.214526 C23.23497,9.6731728 22.918038,9.9426277 22.284167,10.02289 L17.871848,10.02289 17.842136,9.9884918 17.842136,8.8705404 17.871848,8.8361423 21.34822,8.8361423 C21.853335,8.8361423 22.105892,8.7329453 22.105893,8.5265549 L22.105893,7.2194117 C22.105892,7.0015533 21.902856,6.8926256 21.496783,6.8926247 L17.871848,6.8926247 17.842136,6.8582266 17.842136,5.7230747 17.871848,5.6886766 21.036238,5.6886766 C21.501737,5.6886766 21.734483,5.5854815 21.734482,5.3790901 L21.734482,4.0547474 C21.734483,3.848357 21.496783,3.7451609 21.02138,3.7451609 L17.871848,3.7451609 17.842136,3.7107618 17.842136,2.5928104 z M11.558408,2.5584114 L15.822166,2.5584114 C16.438702,2.5584114 16.800204,2.7762688 16.906676,3.2119835 L16.921531,3.3667763 16.921531,6.0498617 C16.921532,6.6117046 16.485747,6.8926256 15.614177,6.8926247 L12.657775,6.8926247 12.657775,8.8361423 16.891824,8.8361423 16.921531,8.8705404 16.921531,9.9884918 16.891824,10.02289 11.558408,10.02289 11.528698,9.9884918 11.528698,5.7230747 11.558408,5.6886766 15.12392,5.6886766 C15.569608,5.6886766 15.792454,5.579748 15.792453,5.3618906 L15.792453,4.0547474 C15.792454,3.848357 15.554754,3.7451609 15.079351,3.7451609 L11.558408,3.7451609 11.528698,3.7107618 11.528698,2.5928104 z M8.8027761,2.5584114 L10.600014,2.5584114 10.62972,2.5928104 10.62972,9.9884933 10.600014,10.02289 9.5157292,10.02289 9.4860223,9.9884933 9.4860223,3.7451609 8.8027761,3.7451609 8.7730692,3.7107618 8.7730692,2.5928104 z M2.3587168,1.4298323 C1.735401,1.4298332 1.2301033,1.931958 1.2301033,2.5513599 L1.2301033,10.136004 C1.2301033,10.755406 1.735401,11.25753 2.3587168,11.25753 L24.007289,11.25753 C24.630603,11.25753 25.135898,10.755406 25.135898,10.136004 L25.135898,2.5513599 C25.135898,1.931958 24.630603,1.4298332 24.007289,1.4298323 z M1.1786144,0.05 L25.187385,0.05 C25.810703,0.05 26.316001,0.55210571 26.316001,1.1714857 L26.316001,15.796492 26.20861,15.814281 C25.891263,15.870964 25.577937,15.93926 25.26909,16.018718 L25.065875,16.073705 25.065875,14.210351 C25.065875,13.817617 24.745454,13.499244 24.350193,13.499244 L20.249704,13.499244 C19.854445,13.499244 19.534025,13.817617 19.534026,14.210351 L19.534026,17.615489 C19.534025,18.008224 19.854445,18.326595 20.249704,18.326596 L20.583679,18.326596 20.292037,18.555763 C19.973779,18.818394 19.668343,19.096004 19.376808,19.387514 L18.967767,19.816511 18.954017,19.757222 C18.891286,19.556776 18.741738,19.394344 18.549085,19.313362 L18.495286,19.293793 18.483333,19.289446 C18.416102,19.268669 18.344622,19.257475 18.270511,19.257475 L14.170023,19.257475 C14.095912,19.257475 14.024432,19.268669 13.957202,19.289446 L13.902201,19.30945 13.891443,19.313362 C13.655981,19.41234 13.484911,19.63299 13.458034,19.896006 L13.454343,19.968662 13.454343,23.374018 C13.454343,23.766784 13.774764,24.08518 14.170023,24.08518 L16.365231,24.08518 16.36459,24.085848 16.378113,24.085848 16.279161,24.366764 C16.245387,24.466566 16.2128,24.566917 16.181419,24.667802 L16.092321,24.967295 14.17002,24.967295 C13.799464,24.967295 13.494684,25.247173 13.458034,25.605831 L13.454343,25.67849 13.454343,29.08377 C13.454343,29.181959 13.474369,29.275505 13.510583,29.360582 L13.527647,29.391824 13.526943,29.395031 13.540717,29.423446 C13.661911,29.645163 13.898279,29.795675 14.17002,29.795675 L15.66659,29.795675 15.668541,29.817621 C15.69054,30.034231 15.717904,30.249258 15.7505,30.462563 L15.775984,30.616365 14.17002,30.616365 C13.77476,30.616365 13.454339,30.934738 13.454339,31.327473 L13.454339,33.367982 13.454343,34.732123 13.463096,34.838273 13.466001,34.857178 13.490831,34.95579 13.495419,34.968245 13.535116,35.060014 13.537403,35.064732 13.587275,35.143094 13.644496,35.212003 13.668344,35.239039 13.762008,35.315823 13.848224,35.367148 13.857378,35.371531 13.957201,35.411313 14.021689,35.427789 14.021882,35.428273 14.025785,35.429269 C14.072373,35.438744 14.120611,35.443719 14.17002,35.443719 L17.630992,35.443719 17.632392,35.445962 C17.911778,35.881711 18.216681,36.299538 18.544992,36.697327 L18.667721,36.838998 14.689174,36.838998 1.1786144,36.839001 C0.55530052,36.839001 0.050001907,36.336896 0.050001907,35.717519 L0.050001907,21.04227 0.05,12.535151 0.050001907,1.1714857 C0.050001907,0.55210571 0.55530052,0.05 1.1786144,0.05 z";
	}
	public class IconeCustoIndireto : IIcone
	{
		public string Data => "M19.245613,25.694645 L16.200483,28.739774 17.156605,29.695896 18.596874,28.255626 18.596676,32.061767 19.949753,32.061695 19.949947,28.312273 21.389641,29.751966 22.345669,28.795938 20.201115,26.651384 20.201735,26.650766 z M28.127985,14.700287 L25.98354,16.84473 25.982922,16.844112 25.02685,17.800184 28.071857,20.845192 29.027929,19.88912 27.587717,18.448906 33.574512,18.449142 33.574432,17.09613 27.644354,17.095897 29.083973,15.656276 z M10.517814,14.690002 L9.5617418,15.646074 11.001954,17.086286 5.0151563,17.08605 5.0152349,18.439064 10.945317,18.439296 9.5056963,19.878917 10.461685,20.834905 12.60613,18.690462 12.606748,18.69108 13.562819,17.735008 z M19.852272,11.512999 L18.702219,11.513 18.702219,12.831068 C17.741215,12.916105 16.969259,13.219639 16.386356,13.741668 15.803452,14.263701 15.512,14.895569 15.512001,15.637276 15.512,16.327018 15.736496,16.911646 16.18549,17.391159 16.634485,17.87067 17.473394,18.315931 18.702219,18.72694 L18.705139,18.722149 19.852272,19.138059 19.852272,19.130865 C20.603222,19.423771 20.978695,19.801712 20.978695,20.264688 20.978695,20.818456 20.62632,21.152767 19.921572,21.267608 L18.702219,21.306383 C18.229595,21.278038 17.691326,21.169382 17.087416,20.980411 16.483505,20.791441 16.029263,20.585935 15.724683,20.363897 L15.724682,22.326824 C16.643675,22.756731 17.636187,22.974046 18.702219,22.978769 L18.702219,24.473999 19.852272,24.473999 19.852272,22.936256 C20.886795,22.832319 21.682383,22.540598 22.239029,22.061086 22.795675,21.581573 23.073997,20.949703 23.073999,20.165478 23.073997,19.485185 22.824558,18.905281 22.325678,18.42577 21.826796,17.946256 21.002327,17.519893 19.852272,17.146677 L18.702219,16.735666 C17.972275,16.409694 17.607304,16.003408 17.607304,15.516809 17.607304,14.949898 17.972275,14.60739 18.702219,14.489283 L19.852272,14.460937 C20.771265,14.550699 21.598358,14.79636 22.333553,15.197922 L22.333553,13.284597 C21.787409,13.01059 20.960314,12.849966 19.852272,12.802724 z M19.950129,3.2769129 L18.597052,3.2769842 18.596859,7.026407 17.157164,5.5867116 16.201138,6.5427377 18.345694,8.6872938 18.345074,8.6879127 19.301193,9.6440322 22.346321,6.5989044 21.390202,5.6427848 19.949932,7.0830538 z";
	}
	public class IconeDeclaracaoExportacao : IIcone
	{
		public string Data => "M7.8389804,43.078037 L7.8389804,45.221935 24.005846,45.221935 24.005846,43.078037 z M7.8389804,38.143761 L7.8389804,40.287125 24.005846,40.287125 24.005846,38.143761 z M7.8389804,33.208954 L7.8389804,35.352848 24.005846,35.352848 24.005846,33.208954 z M37.990169,31.990906 L31.528007,41.0369 28.555748,36.871616 26.643816,38.655453 31.399072,45.201595 39.993622,33.748085 z M7.8389804,28.274677 L7.8389804,30.418037 39.967052,30.418037 39.967052,28.274677 z M7.8389804,23.339869 L7.8389804,25.483227 39.967052,25.483227 39.967052,23.339869 z M7.8389804,18.405594 L7.8389804,20.548952 39.967052,20.548952 39.967052,18.405594 z M7.8389804,13.470784 L7.8389804,15.614143 34.539127,15.614143 34.539127,13.470784 z M23.931646,1.4738611 L23.20523,3.0812047 21.689386,2.183568 21.865534,4.021987 20.168108,4.1187468 21.192968,5.5540028 19.863957,6.5860977 21.449268,7.1989036 20.680845,8.8760576 22.426164,8.5212755 22.746532,10.150049 23.915457,9.0050697 25.132419,10.198215 25.452673,8.5372286 27.117992,8.8274612 26.50951,7.2631688 27.982464,6.6021886 26.653469,5.5218654 27.710188,4.0545278 26.109123,3.9899786 26.109123,2.2481492 24.684067,3.1191339 z M4.026677,0 L34.952923,0 44,9.8782043 44,47.900002 48,47.900002 48,48 44,48 4.0000001,48.000004 0,48 0,47.900002 4.0000557,47.900002 z";
	}
	public class IconeEcf : IIcone
	{
		public string Data => "M0.3610003,32.539997 L44.392002,32.539997 44.392002,44.398999 41.007999,44.398999 41.007999,43.357998 3.707,43.357998 3.707,44.398999 0.3610003,44.398999 z M41.063999,29.379999 L41.063999,30.530001 43.528,30.530001 43.528,29.379999 z M0,19.094998 L12.580001,19.094998 12.580001,25.281998 32.173004,25.281998 32.173004,19.094998 44.752998,19.094998 44.752998,31.675999 33.616001,31.675999 32.173004,31.675999 12.580001,31.675999 9.177001,31.675999 0,31.675999 z M17.217001,12.399 L17.217001,14.286 27.527,14.286 27.527,12.399 z M17.217001,8.6249994 L17.217001,10.511999 27.527,10.511999 27.527,8.6249994 z M17.217001,4.919 L17.217001,6.8060001 27.527,6.8060001 27.527,4.919 z M14.291,0 L30.462999,0 30.462999,23.651999 14.291,23.651999 z";
	}
	public class IconeEmailModelo : IIcone
	{
		public string Data => "M41.720483,29.690989 L50.043999,37.835449 47.729008,37.860859 47.583504,38.201431 C41.44302,52.092548 27.518238,54.015625 17.716007,46.557373 27.74834,49.5037 35.243915,46.604076 36.65076,38.097424 L36.668312,37.982243 33.399316,38.016125 z M16.770708,27.133997 L32.202698,27.133997 37.571983,31.864647 30.268879,39.167759 35.431373,39.195808 34.668384,41.012989 1.2260295,41.012989 z M48.986019,12.903009 L48.986019,35.055969 41.720672,27.790615 38.585319,30.925974 33.616001,26.557852 z M0.1,12.716002 L15.668994,26.595129 0.1,40.01199 z M43.941551,11.502003 L48.647976,11.502003 43.879009,15.718966 z M0.57504006,11.425982 L5.318981,11.425982 5.318981,15.718977 z M24.647964,9.4447174 C26.083046,9.4447174 26.800587,10.264573 26.800588,11.904284 26.800587,13.467373 26.513174,14.722058 25.938349,15.66834 25.363523,16.614622 24.584535,17.087763 23.601385,17.087763 22.919523,17.087763 22.37443,16.827248 21.966104,16.306217 21.557781,15.785189 21.353618,15.072604 21.353618,14.168464 21.353618,12.827579 21.656889,11.705067 22.263429,10.800927 22.869968,9.8967876 23.664814,9.4447174 24.647964,9.4447174 z M24.850145,3.8130002 C21.972051,3.813 19.607338,4.7362952 17.756002,6.5828857 15.904667,8.4294758 14.979,10.754953 14.979,13.55932 14.979,16.2794 15.86106,18.480368 17.625179,20.16222 19.389299,21.844074 21.714371,22.684999 24.600393,22.684999 26.653908,22.684999 28.366491,22.401499 29.738146,21.834496 L29.738146,20.409327 C28.350633,21.037626 26.677692,21.351778 24.719322,21.351778 22.237661,21.351778 20.265413,20.629614 18.802582,19.185289 17.33975,17.740965 16.608333,15.842655 16.608333,13.490359 16.608333,11.099752 17.377411,9.1075802 18.915565,7.5138431 20.453718,5.9201059 22.400198,5.1232371 24.755001,5.1232362 27.197018,5.1232371 29.107818,5.7898488 30.4874,7.1230707 31.866983,8.4562931 32.556774,10.176457 32.556774,12.283563 32.556774,13.708732 32.273323,14.869555 31.706428,15.766033 31.13953,16.66251 30.427936,17.110748 29.571644,17.110748 28.628136,17.110748 28.156383,16.317711 28.156383,14.731636 28.156383,14.655014 28.342705,12.505767 28.715351,8.2838936 L27.228733,8.2838936 C27.165304,8.9505053 27.12566,9.3910818 27.109804,9.6056233 L27.062233,9.6056233 C26.935373,9.1765404 26.647961,8.8202477 26.199993,8.536747 25.752025,8.2532463 25.242611,8.111495 24.671751,8.1114941 23.181167,8.111495 21.977999,8.6842451 21.06224,9.8297434 20.146484,10.975242 19.688606,12.41382 19.688606,14.145477 19.688606,15.455714 20.019626,16.497772 20.681667,17.271656 21.343708,18.045538 22.21784,18.432478 23.304062,18.432478 25.000788,18.432478 26.138546,17.520678 26.717336,15.697073 L26.776802,15.697073 C26.784729,17.520678 27.656879,18.432478 29.393249,18.432478 30.733188,18.432478 31.868965,17.848236 32.800579,16.67975 33.732193,15.511265 34.198002,14.01522 34.198002,12.191616 34.198002,9.7014017 33.313957,7.6824122 31.545873,6.1346474 29.777788,4.5868821 27.545877,3.813 24.850145,3.8130002 z M6.2950147,0.1 L42.790981,0.1 42.790981,16.545048 32.165287,25.820005 16.808246,25.820005 6.3950971,16.569584 z";
	}
	public class IconeEncargoSocial : IIcone
	{
		public string Data => "M8.0954912,30.616367 C7.7002304,30.616367 7.3798092,30.934742 7.3798092,31.327476 L7.3798092,34.73261 C7.3798092,35.125352 7.7002304,35.443723 8.0954912,35.443723 L12.19598,35.443723 C12.245387,35.443723 12.293625,35.438748 12.340215,35.429272 L12.344115,35.428277 12.344312,35.427785 12.408799,35.411309 12.508711,35.371491 12.517717,35.367181 12.604026,35.3158 12.697629,35.23906 12.721613,35.211877 12.778703,35.143124 12.828615,35.064698 12.830846,35.060098 12.870605,34.968182 12.87516,34.955819 12.900006,34.857152 12.902899,34.838303 12.911657,34.732121 12.911657,33.367997 12.911661,31.327476 C12.911661,30.934742 12.59124,30.616367 12.19598,30.616367 z M2.015807,30.616367 C1.6205471,30.616367 1.3001259,30.934742 1.3001259,31.327476 L1.3001259,34.73261 C1.3001259,35.125352 1.6205471,35.443723 2.015807,35.443723 L6.1162985,35.443723 C6.5115574,35.443723 6.8319786,35.125352 6.8319786,34.73261 L6.8319786,31.327476 C6.8319786,30.934742 6.5115574,30.616367 6.1162985,30.616367 z M32.749504,30.169368 C33.144761,30.169369 33.463517,30.331954 33.705769,30.657136 33.948024,30.982317 34.06915,31.441393 34.06915,32.034364 34.06915,33.309577 33.616519,33.947184 32.711254,33.947184 31.812364,33.947184 31.36292,33.335084 31.36292,32.11088 31.36292,30.816538 31.825113,30.169369 32.749504,30.169368 z M32.835942,28.485817 C31.777652,28.485817 30.93134,28.820528 30.297005,29.48995 29.662671,30.159369 29.345504,31.064683 29.345504,32.205891 29.345504,33.251462 29.643544,34.083461 30.239629,34.701878 30.835713,35.320294 31.621458,35.6295 32.596869,35.6295 33.648782,35.6295 34.490313,35.294795 35.121459,34.625373 35.752608,33.955951 36.068181,33.057011 36.068181,31.928556 36.068181,30.831981 35.784484,29.984045 35.217088,29.384755 34.649693,28.785462 33.855977,28.485817 32.835942,28.485817 z M8.0954912,24.967297 C7.7002304,24.967297 7.3798092,25.285738 7.3798092,25.678556 L7.3798092,29.084417 C7.3798092,29.477236 7.7002304,29.795677 8.0954912,29.795677 L12.19598,29.795677 C12.467721,29.795677 12.704089,29.645164 12.825282,29.423446 L12.839055,29.395033 12.838352,29.391821 12.855416,29.360582 C12.891631,29.275503 12.911657,29.181957 12.911657,29.083767 L12.911657,25.678459 12.907966,25.605833 C12.871316,25.247175 12.566536,24.967297 12.19598,24.967297 z M2.015807,24.967297 C1.6205471,24.967297 1.3001259,25.285738 1.3001259,25.678556 L1.3001259,29.084417 C1.3001259,29.477236 1.6205471,29.795677 2.015807,29.795677 L6.1162985,29.795677 C6.5115574,29.795677 6.8319786,29.477236 6.8319786,29.084417 L6.8319786,25.678556 C6.8319786,25.285738 6.5115574,24.967297 6.1162985,24.967297 z M24.238749,23.216855 C25.099624,23.216855 25.530062,23.838522 25.530062,25.081855 25.530062,26.357065 25.077307,26.994672 24.171792,26.994672 23.285409,26.994672 22.842217,26.382569 22.842217,25.158366 22.842217,23.864027 23.307727,23.216855 24.238749,23.216855 z M31.717682,21.724477 L22.804221,35.438329 25.13779,35.438329 34.070374,21.724477 z M24.295628,21.533305 C23.23734,21.533305 22.39103,21.868015 21.756695,22.537435 21.122358,23.206859 20.80519,24.112172 20.80519,25.253375 20.80519,26.298949 21.103234,27.130944 21.699318,27.749361 22.295399,28.367781 23.081147,28.676989 24.056556,28.676989 25.10847,28.676989 25.95,28.345465 26.581146,27.682421 27.212294,27.019374 27.527868,26.123624 27.527868,24.99517 27.527868,23.898595 27.242576,23.047473 26.671993,22.441805 26.101411,21.836138 25.309288,21.533305 24.295628,21.533305 z M8.0954893,19.257475 C8.0460814,19.257475 7.9978436,19.26245 7.9512537,19.271923 L7.8826674,19.289446 7.8169153,19.313364 C7.560046,19.421339 7.3798092,19.674116 7.3798092,19.96873 L7.3798092,23.37459 C7.3798092,23.767411 7.7002304,24.08585 8.0954912,24.085851 L10.001415,24.085851 10.00077,24.085179 12.195977,24.085179 C12.591235,24.085179 12.911657,23.766781 12.911657,23.374016 L12.911657,19.968634 12.907965,19.896002 12.897121,19.825386 C12.849453,19.593878 12.688613,19.403343 12.474556,19.313364 L12.44766,19.30358 12.408799,19.289446 C12.341568,19.268669 12.270088,19.257475 12.195977,19.257475 z M2.015806,19.257475 C1.941695,19.257475 1.8702152,19.268669 1.802985,19.289446 L1.7920263,19.293432 1.737232,19.313364 C1.4803627,19.421339 1.3001259,19.674116 1.3001259,19.96873 L1.3001259,23.37459 C1.3001259,23.767411 1.6205471,24.08585 2.015807,24.085851 L6.1162985,24.085851 C6.5115574,24.08585 6.8319786,23.767411 6.8319786,23.37459 L6.8319786,21.837874 6.8319786,19.96873 C6.8319786,19.674116 6.6517418,19.421339 6.3948734,19.313364 L6.34321,19.294571 6.3291195,19.289446 C6.2618893,19.268669 6.1904095,19.257475 6.1162975,19.257475 z M28.490001,17.050001 C34.813662,17.050001 39.940001,22.17634 39.940001,28.500002 39.940001,34.823661 34.813662,39.950002 28.490001,39.950002 22.16634,39.950002 17.040001,34.823661 17.04,28.500002 17.040001,22.17634 22.16634,17.050001 28.490001,17.050001 z M14.170022,13.499244 C13.774763,13.499243 13.454342,13.817617 13.454342,14.210351 L13.454342,17.615489 C13.454342,18.008225 13.774763,18.326596 14.170022,18.326596 L18.27051,18.326596 C18.66577,18.326596 18.986193,18.008225 18.986193,17.615489 L18.986193,14.210351 C18.986193,13.817617 18.66577,13.499243 18.27051,13.499244 z M8.0954903,13.499244 C7.7002304,13.499243 7.3798092,13.817617 7.3798092,14.210351 L7.3798092,17.615489 C7.3798092,18.008225 7.7002304,18.326596 8.0954903,18.326596 L12.195977,18.326596 C12.591235,18.326596 12.911657,18.008225 12.911657,17.615489 L12.911657,14.210351 C12.911657,13.817617 12.591235,13.499243 12.195977,13.499244 z M2.015807,13.499244 C1.6205471,13.499243 1.3001259,13.817617 1.3001259,14.210351 L1.3001259,17.615489 C1.3001259,18.008225 1.6205471,18.326596 2.015807,18.326596 L6.1162947,18.326596 C6.5115564,18.326596 6.8319767,18.008225 6.8319767,17.615489 L6.8319767,14.210351 C6.8319767,13.817617 6.5115564,13.499243 6.1162947,13.499244 z M17.871848,2.5584114 L21.734482,2.5584114 C22.26931,2.5584114 22.620909,2.7132051 22.789282,3.0227917 22.838801,3.1661194 22.863562,3.2807806 22.863562,3.3667763 L22.863562,5.8434704 C23.111166,6.0154627 23.23497,6.244787 23.23497,6.5314405 L23.23497,9.214526 C23.23497,9.6731728 22.918038,9.9426277 22.284167,10.022891 L17.871848,10.022891 17.842136,9.9884918 17.842136,8.8705404 17.871848,8.8361423 21.348219,8.8361423 C21.853335,8.8361423 22.105892,8.7329453 22.105893,8.5265549 L22.105893,7.2194117 C22.105892,7.0015533 21.902855,6.8926256 21.496783,6.8926247 L17.871848,6.8926247 17.842136,6.8582266 17.842136,5.7230747 17.871848,5.6886766 21.036238,5.6886766 C21.501738,5.6886766 21.734482,5.5854815 21.734482,5.3790901 L21.734482,4.0547474 C21.734482,3.848357 21.496783,3.7451609 21.02138,3.7451609 L17.871848,3.7451609 17.842136,3.7107618 17.842136,2.5928104 z M11.558408,2.5584114 L15.822166,2.5584114 C16.438702,2.5584114 16.800204,2.7762688 16.906676,3.2119835 L16.921532,3.3667763 16.921532,6.0498617 C16.921532,6.6117046 16.485747,6.8926256 15.614178,6.8926247 L12.657775,6.8926247 12.657775,8.8361423 16.891823,8.8361423 16.921532,8.8705404 16.921532,9.9884918 16.891823,10.022891 11.558408,10.022891 11.528697,9.9884918 11.528697,5.7230747 11.558408,5.6886766 15.12392,5.6886766 C15.569608,5.6886766 15.792453,5.579748 15.792453,5.3618906 L15.792453,4.0547474 C15.792453,3.848357 15.554754,3.7451609 15.079351,3.7451609 L11.558408,3.7451609 11.528697,3.7107618 11.528697,2.5928104 z M8.8027761,2.5584114 L10.600013,2.5584114 10.629719,2.5928104 10.629719,9.9884933 10.600013,10.022891 9.5157292,10.022891 9.4860223,9.9884933 9.4860223,3.7451609 8.8027761,3.7451609 8.7730692,3.7107618 8.7730692,2.5928104 z M2.3587168,1.4298323 C1.735401,1.4298332 1.2301033,1.931958 1.2301033,2.5513599 L1.2301033,10.136004 C1.2301033,10.755406 1.735401,11.257531 2.3587168,11.257531 L24.007288,11.257531 C24.630602,11.257531 25.135899,10.755406 25.135899,10.136004 L25.135899,2.5513599 C25.135899,1.931958 24.630602,1.4298332 24.007288,1.4298323 z M1.1786144,0.05 L25.187386,0.05 C25.810704,0.05 26.316001,0.55210571 26.316001,1.1714857 L26.316001,15.796491 26.208611,15.814281 C25.891263,15.870964 25.577937,15.939261 25.26909,16.018717 L25.065875,16.073704 25.065875,14.210351 C25.065875,13.817617 24.745453,13.499243 24.350193,13.499244 L20.249704,13.499244 C19.854445,13.499243 19.534025,13.817617 19.534026,14.210351 L19.534026,17.615489 C19.534025,18.008225 19.854445,18.326596 20.249704,18.326596 L20.583679,18.326596 20.292036,18.555764 C19.973778,18.818394 19.668343,19.096005 19.376808,19.387514 L18.967766,19.816511 18.954018,19.757222 C18.891285,19.556776 18.741738,19.394345 18.549086,19.313362 L18.495285,19.293793 18.483334,19.289446 C18.416102,19.268669 18.344622,19.257475 18.270512,19.257475 L14.170022,19.257475 C14.095911,19.257475 14.024432,19.268669 13.957201,19.289446 L13.9022,19.30945 13.891443,19.313362 C13.65598,19.41234 13.48491,19.63299 13.458034,19.896006 L13.454342,19.968662 13.454342,23.374018 C13.454342,23.766783 13.774763,24.085181 14.170022,24.085181 L16.365231,24.085181 16.36459,24.085848 16.378113,24.085848 16.279162,24.366764 C16.245386,24.466566 16.212799,24.566918 16.18142,24.667802 L16.092322,24.967295 14.17002,24.967295 C13.799464,24.967295 13.494684,25.247173 13.458034,25.605831 L13.454342,25.678489 13.454342,29.083769 C13.454342,29.181959 13.474368,29.275505 13.510583,29.360582 L13.527647,29.391823 13.526943,29.395031 13.540717,29.423445 C13.66191,29.645162 13.898279,29.795675 14.17002,29.795675 L15.666591,29.795675 15.66854,29.817621 C15.69054,30.034231 15.717904,30.249258 15.750499,30.462562 L15.775983,30.616365 14.17002,30.616365 C13.77476,30.616365 13.454338,30.934738 13.454339,31.327472 L13.454339,33.367982 13.454342,34.732125 13.463096,34.838273 13.466001,34.857178 13.490831,34.955792 13.495419,34.968247 13.535116,35.060014 13.537403,35.064732 13.587275,35.143094 13.644496,35.212003 13.668345,35.239041 13.762007,35.315823 13.848224,35.36715 13.857378,35.371533 13.957201,35.411313 14.021689,35.427789 14.021882,35.428273 14.025785,35.429269 C14.072374,35.438744 14.120611,35.443719 14.17002,35.443719 L17.630991,35.443719 17.632391,35.445962 C17.911778,35.881711 18.216681,36.299538 18.544992,36.697327 L18.667721,36.838998 14.689174,36.838998 1.1786144,36.839001 C0.55530052,36.839001 0.050001907,36.336896 0.050001907,35.717519 L0.050001907,21.042269 0.05,12.535151 0.050001907,1.1714857 C0.050001907,0.55210571 0.55530052,0.05 1.1786144,0.05 z";
	}
	public class IconeEntrada : IIcone
	{
		public string Data => "M15.890037,20.097 C16.93975,20.097 17.79071,20.947884 17.79071,21.997499 17.79071,23.047117 16.93975,23.897999 15.890037,23.897999 14.840324,23.897999 13.989364,23.047117 13.989364,21.997499 13.989364,20.947884 14.840324,20.097 15.890037,20.097 z M9.285434,20.097 C10.335147,20.097 11.186108,20.947884 11.186108,21.997499 11.186108,23.047117 10.335147,23.897999 9.285434,23.897999 8.2357209,23.897999 7.3847603,23.047117 7.3847605,21.997499 7.3847603,20.947884 8.2357209,20.097 9.285434,20.097 z M0.71296787,4.5609999 L4.106236,4.5609999 8.2400851,17.436592 16.460268,17.436592 19.310849,7.7919288 21.117001,7.7919288 17.695662,18.482002 7.0520443,18.482002 3.1563334,6.508945 0.71296787,6.508945 C-0.24539137,5.9235206 -0.22988844,5.2731719 0.71296787,4.5609999 z M9.3340042,1.4494378 L15.035004,1.4494378 15.035004,7.9850026 17.934,7.9850026 12.137167,14.158001 6.4360003,7.9850026 9.3340042,7.9850026 z";
	}
	public class IconeEtiqueta : IIcone
	{
		public string Data => "M25.200001,41.43 L25.200001,43.23 27,43.23 27,41.43 z M19.800001,41.43 L19.82,41.43 19.82,41.443996 19.800001,41.443996 z M3.5999993,36 L8.9999989,36 8.9999989,37.23 8.9999989,40.23 8.9999989,41.43 3.5999993,41.43 3.5999993,40.23 3.5999993,37.23 z M3.5999993,34.200001 L3.5999993,34.23 1.7999977,34.23 1.7999977,37.23 1.7999977,40.23 1.7999977,43.23 3.5999993,43.23 8.9999989,43.23 10.799998,43.23 10.799998,40.23 10.799998,37.23 10.799998,34.23 8.9999989,34.23 8.9999989,34.200001 z M32.400001,32.400005 L34.200001,32.400005 34.200001,34.200005 32.400001,34.200005 z M0,32.400005 L1.800001,32.400005 3.6000007,32.400005 10.8,32.400005 12.599999,32.400005 12.599999,33.600002 12.599999,34.800003 12.599999,36.000004 12.601002,36.000004 12.601002,36.029999 12.599998,36.029999 12.599998,37.23 12.599998,38.43 12.599998,39.629997 12.599998,40.829994 12.599998,42.029999 12.599998,43.23 12.617001,43.23 12.617001,43.243999 12.599998,43.243999 12.599998,44.443996 12.599998,45.043999 8.9999989,45.043999 0,45.043999 0,42.039333 0,37.234665 0,32.43 z M30.600002,30.600004 L30.600002,32.400005 30.600002,36.000004 32.400001,36.000004 34.200001,36.000004 36,36.000004 36,32.400005 36,30.600004 34.200001,30.600004 32.400001,30.600004 z M14.400004,30.600004 L16.200003,30.600004 18.000002,30.600004 19.800003,30.600004 19.800003,31.200005 19.800003,32.400005 18.000002,32.400005 18.000002,33.600002 18.000002,34.800003 18.000002,36.000004 18.000002,37.799999 19.800003,37.799999 19.800003,36.000004 19.800003,34.800003 19.800003,33.600002 19.800003,32.400005 21.600002,32.400005 23.400003,32.400005 23.400003,33.030003 23.400003,34.230003 21.600002,34.23 21.600002,35.43 21.600002,36.629997 21.600002,37.830002 25.200003,37.830006 27.000002,37.830006 27.000002,39.629997 28.800001,39.629997 28.800001,41.43 32.414001,41.43 32.414001,41.443996 32.400001,41.443996 32.400001,42.643997 32.400001,43.243999 27,43.243999 27,45.043999 25.200001,45.043999 25.200001,43.243999 23.399999,43.243999 23.399999,45.043999 21.6,45.043999 21.6,43.843994 21.6,42.643997 21.6,41.443996 21.586,41.443996 21.586,41.43 21.6,41.43 23.399999,41.43 23.399999,39.629997 21.6,39.629997 21.6,39.029999 21.6,37.829998 19.800001,37.829998 19.800001,39.029999 19.800001,40.23 19.800001,41.43 18,41.43 18,39.629997 16.200001,39.629997 16.200001,41.43 18,41.43 18,43.23 19.800001,43.23 19.800001,43.843994 19.800001,45.043999 18.000002,45.043999 14.400002,45.043999 14.400002,44.443996 14.400002,43.243999 14.398002,43.243999 14.398002,43.23 14.400002,43.23 14.400002,42.029999 14.400002,40.829994 14.400002,39.629997 14.400002,38.43 14.400002,37.23 14.400002,36.029999 14.382001,36.029999 14.382001,36.000004 14.400004,36.000004 16.200003,36.000004 16.200003,34.200005 14.400004,34.200005 14.400004,33.600002 14.400004,32.400005 z M37.799999,27.030001 L37.799999,28.829998 41.400001,28.829998 41.400001,27.030001 z M34.200001,27.030001 L34.200001,28.829998 36,28.829998 36,27.030001 z M30.600002,27.000002 L30.600002,28.799999 32.400001,28.799999 32.400001,27.000002 z M36,23.400002 L36,25.200001 36,27.000002 37.799999,27.000002 37.799999,25.200001 39.600002,25.200001 39.600002,23.400002 z M32.400001,23.400002 L32.400001,25.200001 34.200001,25.200001 34.200001,23.400002 z M18.000002,23.400002 L19.800003,23.400002 19.800003,25.200001 19.800003,26.400002 19.800003,27.6 19.800003,28.799999 18.000002,28.799999 18.000002,27.000002 18.000002,25.200001 z M0,23.384001 L1.800001,23.384001 1.800001,23.400002 3.6000007,23.400002 3.6000007,24.000002 3.6000007,25.200001 1.800001,25.200001 1.800001,26.400002 1.800001,27.6 1.800001,28.799999 1.800001,30.000002 1.800001,30.600004 0,30.600004 6.7120789E-07,30.118999 0,28.336998 0,27.6 0,26.400002 0,25.200001 0,23.400002 z M7.2000001,21.600002 L7.2000001,23.400002 8.9999998,23.400002 8.9999998,21.600002 z M12.599999,19.799999 L14.400004,19.799999 14.400004,21.600002 12.599999,21.600002 z M5.3999995,18.000002 L7.2000001,18.000002 7.2000001,18.600002 7.2000001,19.799999 8.9999998,19.799999 8.9999998,18.600002 8.9999998,18.030001 10.8,18.030001 12.599999,18.030001 12.599999,19.799999 10.8,19.799999 10.8,21.600002 12.599999,21.600002 12.599999,22.799999 12.599999,23.400002 10.8,23.400002 10.8,25.200001 12.599999,25.200001 14.400004,25.200001 16.200003,25.200001 16.200003,27.000002 14.400004,27.000002 12.599999,27.000002 10.8,27.000002 10.8,28.799999 12.599999,28.799999 14.400004,28.799999 14.400004,29.399998 14.400004,30.600004 12.599999,30.600004 10.8,30.600004 8.9999998,30.600004 8.9999998,28.799999 7.2000001,28.799999 5.3999995,28.799999 3.6000007,28.799999 3.6000007,27.6 3.6000007,27.000002 5.3999995,27.000002 5.3999995,25.200001 5.3999995,24.000002 5.3999995,22.799999 5.3999995,21.600002 5.3999995,20.400002 5.3999995,19.200001 z M37.799999,14.429999 L39.600002,14.429999 39.600002,16.200001 37.799999,16.200001 z M34.200001,14.429999 L36,14.429999 36,16.230001 36,18.030001 37.799999,18.030001 37.799999,18.600002 37.799999,19.799999 34.200001,19.799999 34.200001,21.600002 37.799999,21.600002 37.799999,19.799999 39.600002,19.799999 39.600002,21.600002 41.400001,21.600002 41.400001,19.799999 39.600002,19.799999 39.600002,18.600002 39.600002,18.030001 41.400001,18.030001 41.400001,16.230001 41.400001,14.429999 43.200001,14.429999 43.200001,16.230001 43.200001,18.030001 45,18.030001 45,19.799999 43.200001,19.799999 43.200001,21.600002 45,21.600002 45,21.630001 45,23.400002 43.200001,23.400002 43.200001,25.200001 45,25.200001 45,25.23 45,27.030001 43.200001,27.030001 43.200001,28.829998 45,28.829998 45,30.6 43.200001,30.6 43.200001,32.400002 45,32.400002 45,34.200001 41.400001,34.200001 41.400001,32.399998 41.400001,30.6 39.599998,30.6 37.799999,30.6 37.799999,32.400002 39.599998,32.400002 39.599998,34.200001 37.799999,34.200001 37.799999,36 39.599998,36 41.400001,36 41.429001,36.00024 41.429001,36.029999 41.400001,36.029999 41.400001,37.830002 43.200001,37.830002 43.200001,36.029999 43.195,36.029999 43.195,36.014957 45,36.029999 45,39.629997 45,43.23 45,43.243999 45,45.043999 37.799999,45.043999 37.799999,43.243999 36,43.243999 36,45.043999 34.200001,45.043999 34.200001,43.843994 34.200001,42.643997 34.200001,41.443996 34.179001,41.443996 34.179001,41.43 34.200001,41.43 34.200001,39.629997 36,39.629997 36,41.43 37.799999,41.43 37.799999,43.23 43.200001,43.23 43.200001,39.629997 37.799999,39.629997 36,39.629997 36,37.830002 34.200001,37.830002 34.200001,39.629997 32.399998,39.629997 32.399998,37.830002 30.599998,37.830002 30.599998,39.629997 28.800001,39.629997 28.800001,37.830002 27.000002,37.830002 27.000002,36.030003 25.200003,36.030003 25.200003,35.43 25.200003,34.230003 27.000002,34.230003 27.000002,36.000004 28.800001,36.000004 28.800001,32.400005 27.000002,32.400005 27.000002,30.630003 28.800001,30.630003 28.800001,27.030001 27.000002,27.030001 27.000002,27.000002 25.200003,27.000002 25.200003,28.799999 27.000002,28.799999 27.000002,30.630003 25.200003,30.630003 25.200003,28.829998 23.400003,28.829998 23.400003,30.600004 21.600002,30.600004 21.600002,30.000002 21.600002,28.799999 21.600002,27.6 21.600002,27.000002 23.400003,27.000002 23.400003,25.200001 21.600002,25.200001 21.600002,23.400002 23.400003,23.400002 25.200003,23.400002 25.200003,23.384001 26.992,23.384001 26.992,23.586998 27.000002,23.586998 27.000002,25.200001 28.800001,25.200001 28.800001,23.400002 28.789001,23.400002 28.789001,23.384001 30.600002,23.384001 30.600002,21.584002 25.200003,21.584002 25.200003,20.41 25.200003,19.799999 27.000002,19.799999 27.000002,18.600002 27.000002,18.030001 28.800001,18.030001 30.600002,18.030001 30.600002,18.600002 30.600002,19.799999 32.400001,19.799999 32.400001,18.600002 32.400001,18.030001 34.200001,18.030001 34.200001,16.230001 z M30.600002,14.429999 L32.400001,14.429999 32.400001,16.200001 30.600002,16.200001 30.600002,15.630001 z M25.200003,14.429999 L27.000002,14.429999 27.000002,16.200001 25.200003,16.200001 z M10.802002,14.384002 L12.600001,14.384002 12.600001,14.398004 14.400004,14.398004 16.200003,14.398004 18.000004,14.398004 19.800003,14.398004 19.800003,14.429999 19.800003,16.200001 18.000002,16.200001 18.000002,17.400003 18.000002,18.600002 18.000002,19.799999 19.800003,19.799999 19.800003,18.600002 19.800003,18.030001 21.600002,18.030001 23.400003,18.030001 23.400003,19.799999 21.600002,19.799999 21.600002,21.000002 21.600002,22.200001 21.600002,23.400002 19.800003,23.400002 19.800003,21.600002 18.000002,21.600002 18.000002,23.400002 16.200003,23.400002 16.200003,21.600002 16.200003,19.799999 14.400004,19.799999 14.400004,18.030001 16.200003,18.030001 16.200003,16.230001 14.400004,16.230001 14.400004,18.000002 12.599999,18.000002 12.599999,16.230001 10.8,16.230001 10.8,15.970999 10.802002,15.970999 z M6.7120789E-07,14.384002 L1.800001,14.384002 3.6000007,14.384002 7.2000006,14.384002 9.0000018,14.384002 9.0020016,14.384002 9.0020016,14.429997 8.9999998,14.429997 8.9999998,15.629999 8.9999998,16.200001 7.2000001,16.200001 5.3999995,16.200001 5.3999995,18.000002 3.6000007,18.000002 3.6000007,19.200001 3.6000007,20.400002 3.6000007,21.600002 1.800001,21.600002 1.800001,20.983999 1.800001,19.799999 1.800001,19.784002 1.800001,18.600002 1.800001,17.400003 1.800001,16.200001 0,16.200001 0,16.184002 z M19.800003,12.600001 L19.804001,12.600001 19.804001,12.630001 19.800003,12.630001 z M12.600001,12.584002 L12.601002,12.584002 12.601002,12.598001 12.600001,12.598001 z M28.800001,10.799999 L30.600002,10.799999 30.600002,10.814 30.600002,12.614 30.601002,12.614 30.601002,12.630001 30.600002,12.630001 30.600002,14.429999 28.800001,14.429999 28.800001,12.630001 28.789001,12.630001 28.789001,12.6 28.800001,12.6 z M14.400004,9.000001 L16.200003,9.000001 16.200003,9.0140009 16.200003,10.814002 16.200003,12.598001 14.400004,12.598001 14.398002,12.598001 14.398002,12.584002 14.400004,12.584002 14.400004,10.800002 14.400004,10.784001 z M28.800001,7.1999998 L30.600002,7.1999998 30.616001,7.1999998 30.616001,7.2140007 30.600002,7.2140007 30.600002,8.999999 28.800001,8.999999 z M21.600002,7.1999998 L25.200003,7.1999998 25.200003,9 28.800001,9 28.800001,10.799999 27.000002,10.799999 27.000002,12.087 26.991001,12.087 26.991001,12.630001 25.200003,12.630001 25.200003,14.429999 23.400003,14.429999 23.400003,16.230001 21.600002,16.230001 21.600002,14.429999 21.600002,12.630001 21.586,12.630001 21.586,12.600001 21.600002,12.600001 21.600002,10.799999 23.400003,10.799999 23.400003,12.600001 25.200003,12.600001 25.200003,10.799999 23.400003,10.799999 23.400003,9 21.600002,9 z M27.000002,5.4000006 L28.800001,5.4000006 28.800001,7.1999998 27.000002,7.1999998 z M36,3.6000013 L37.799999,3.6000013 39.600002,3.6000013 41.400001,3.6000013 41.400001,5.4000006 41.400001,7.1999998 41.400001,8.9999981 39.600002,8.9999981 37.799999,8.9999981 36,8.9999981 36,7.1999998 36,5.4000006 z M3.6000014,3.6000013 L5.4000009,3.6000013 7.2000006,3.6000013 9.0000018,3.6000013 9.0000018,5.4000006 9.0000018,7.2000008 9.0000018,9.000001 7.2000006,9.000001 5.4000009,9.000001 3.6000014,9.000001 3.6000014,7.2000008 3.6000014,5.4000006 z M1.800001,1.800001 L1.800001,3.6000013 1.800001,5.4000006 1.800001,7.2000008 1.800001,9.000001 1.800001,10.800002 3.6000007,10.800002 5.4000009,10.800002 7.2000006,10.800002 9.0000018,10.800002 10.8,10.800002 10.800001,9.000001 10.800001,7.2000008 10.800001,5.4000006 10.8,3.6000013 10.8,1.800001 9.0000018,1.800001 7.2000006,1.800001 5.4000009,1.800001 3.6000007,1.800001 z M34.200001,1.7999997 L34.200001,3.6000013 34.200001,5.4000006 34.200001,7.1999998 34.200001,8.9999981 34.200001,10.799997 36,10.799997 37.799999,10.799997 39.600002,10.799997 41.400001,10.799997 43.200001,10.799997 43.200001,8.9999981 43.200001,7.1999998 43.200001,5.4000006 43.200001,3.599998 43.200001,1.7999997 41.400001,1.7999997 39.600002,1.7999997 37.799999,1.7999997 36,1.7999997 z M27.000002,1.7999997 L28.800001,1.7999997 28.800001,3.599998 27.000002,3.599998 27.000002,3.0000005 27.000002,2.400001 z M32.400001,0 L34.200001,0 36,0 37.799999,0 39.600002,0 41.400001,0 43.200001,0 44.952,0 44.952,4.1946659 44.952,8.3893251 44.952,12.583994 44.959999,12.583994 44.959999,12.629998 43.200001,12.629998 41.400001,12.629998 39.600002,12.629998 37.799999,12.629998 36,12.629998 34.200001,12.629998 32.400001,12.629998 32.398002,12.629998 32.398002,12.614 32.400001,12.614 32.400001,10.814 32.400001,9.013999 32.416,6.6499996 32.400001,6.6499996 32.400001,5.4000006 32.400001,3.6000013 32.400001,1.7999997 z M28.800001,0 L30.600002,0 30.600002,1.7999997 28.800001,1.7999997 z M25.200003,0 L27.000002,0 27.000002,1.7999997 25.200003,1.7999997 z M16.200003,0 L18.000002,0 18.000002,1.2000002 18.000002,2.400001 18.000002,3.6000013 19.800003,3.6000013 19.800003,2.400001 19.800003,1.800001 21.600002,1.800001 23.400003,1.800001 25.200003,1.800001 25.200003,2.400001 25.200003,3.0000005 25.200003,3.599998 21.600002,3.599998 21.600002,5.4000006 21.600002,7.1999998 19.800003,7.1999998 19.800003,5.400001 18.000002,5.400001 18.000002,7.1999998 19.800003,7.1999998 19.800003,9 19.800003,10.79999919.800003,12.598001 18.000002,12.598001 18.000002,10.814002 18.000002,9.0140009 18.000002,7.2140007 16.200003,7.2140007 16.200003,7.1999998 14.400004,7.1999998 14.400004,5.400001 16.200003,5.400001 16.200003,3.6000013 14.400004,3.6000013 14.400004,1.800001 16.200003,1.800001 z M6.7120789E-07,0 L1.800001,0 3.6000007,0 5.3999995,0 7.2000006,0 9.0000018,0 10.800001,0 12.600001,0 12.600001,1.800001 12.600001,3.6000013 12.600001,5.4000006 12.600001,7.2000008 12.600001,9.000001 12.600001,10.784001 12.600001,10.800002 12.600001,12.584002 10.800001,12.584002 9.0000018,12.584002 7.2000006,12.584002 3.6000007,12.584002 1.800001,12.584002 6.7120789E-07,12.584002 6.7120789E-07,10.800002 6.7120789E-07,10.7840016.7120789E-07,9.000001 6.7120789E-07,7.2000008 6.7120789E-07,5.400001 6.7120789E-07,3.6000013 6.7120789E-07,1.800001 z";
	}
	public class IconeExportarArquivoContabilidade : IIcone
	{
		public string Data => "M47.2919998142238,52.711634333393L47.2919998142238,56.5169982910156 43.5326911822401,56.5169982910156z M41.4720001193996,29.8780002367209L55.4811208454161,42.4249673017351 41.4720001193996,55.1809997785379 41.4720001193996,48.8050003051758 28.9890003177638,48.8050003051758 28.9890003177638,36.2550010681152 41.4720001193996,36.2550010681152z M10.4479379627223,27.1100006103516L10.4479379627223,29.7210178375244C8.68452930183366,29.8894691467285 7.26802062721208,30.4907531738281 6.19841193885759,31.5248641967773 5.12880420417741,32.5589790344238 4.59399986000017,33.810676574707 4.59400033683733,35.279956817627 4.59399986000017,36.6462936401367 5.00594377250627,37.8044052124023 5.82983064384416,38.7542915344238 6.65371846885637,39.7041778564453 8.19308757514909,40.5862159729004 10.4479379627223,41.4004058837891L12.5582466098781,42.200553894043C13.9362096759792,42.7807807922363 14.6251916858669,43.5294570922852 14.6251916858669,44.446590423584 14.6251916858669,45.5789680480957 13.9362096759792,46.2480964660645 12.5582466098781,46.4539833068848 11.8766183826442,46.554931640625 11.1714057895656,46.566951751709 10.4479379627223,46.5101356506348 9.58068752021745,46.4539833068848 8.59298610420183,46.2387390136719 7.4848332378383,45.8643989562988 6.37668037147478,45.490062713623 5.54315709800676,45.0829658508301 4.98426198692277,44.6431198120117L4.98426198692277,48.5315704345703C6.67058133811906,49.3831901550293,8.49180698127702,49.8136825561523,10.4479379627223,49.8230400085449L10.4479379627223,52.7849998474121 12.5582466098781,52.7849998474121 12.5582466098781,49.7388153076172C14.456560132217,49.5329284667969 15.9164314243312,48.9550399780273 16.9378585788722,48.0051536560059 17.9592857334133,47.0552673339844 18.4699993106838,45.8035697937012 18.4699993106838,44.2500610351563 18.4699993106838,42.9024391174316 18.0122833225246,41.7536849975586 17.0968532535549,40.8038024902344 16.1814231845851,39.8539161682129 14.6685543033595,39.0093116760254 12.5582466098781,38.2699928283691 11.580297467422,37.9465255737305 10.9448451969142,37.6880264282227 10.4479379627223,37.4558029174805 9.10851859779314,36.8100662231445 8.43880843849138,36.0052375793457 8.43880843849138,35.0413131713867 8.43880843849138,33.9182968139648 9.10851859779314,33.2398071289063 10.4479379627223,33.0058441162109 11.1115312549587,32.930793762207 11.8087835285182,32.9033355712891 12.5582466098781,32.9496955871582 14.2445659610744,33.1275062561035 15.7622528049465,33.6141471862793 17.1113090488429,34.4096183776855L17.1113090488429,30.6194305419922C16.1091518375392,30.076639175415,14.5914659473415,29.758451461792,12.5582466098781,29.6648654937744L12.5582466098781,27.1100006103516z M5.44700002403215,20.9570007324219L5.44700002403215,23.3640003204346 39.2799987766261,23.3640003204346 39.2799987766261,20.9570007324219z M5.44700002403215,14.121000289917L5.44700002403215,16.5279998779297 39.2799987766261,16.5279998779297 39.2799987766261,14.121000289917z M5.44700002403215,7.24600124359131L5.44700002403215,9.65299987792969 33.4560012790676,9.65299987792969 33.4560012790676,7.24600124359131z M36.6876182529445,0L47.2919998142238,12.5428562164307 47.2919998142238,32.4309921264648 39.3680000278468,24.4300003051758 39.3680000278468,34.2029991149902 26.8869991275783,34.2029991149902 26.8869991275783,50.7760009765625 39.3680000278468,50.7760009765625 39.3680000278468,56.5169982910156 0,56.5169982910156 0,0.124737001955509z";
	}
	public class IconeFarmaciaPopular : IIcone
	{
		public string Data => "M14.200754,37.032055 L20.76404,37.032055 C21.36397,37.054562 21.314228,37.675678 21.314228,37.675678 20.901514,39.42355 19.473576,40.656219 17.560303,40.656219 15.64703,40.656219 14.059119,39.360748 13.646404,37.612877 13.646404,37.612877 13.727143,37.022282 14.200754,37.032055 z M17.560303,32.392845 C19.473576,32.392845 20.901514,33.642773 21.314228,35.415115 21.314228,35.415115 21.36397,36.04493 20.76404,36.067753 L14.200754,36.067753 C13.727143,36.077663 13.646404,35.478802 13.646404,35.478802 14.059119,33.706459 15.647031,32.392845 17.560303,32.392845 z M12.430034,26.914457 C11.875067,26.930719 11.241406,27.210482 10.742365,28.153791 L8.5243711,31.962872 9.1132555,32.312492 9.1274624,32.320045 11.734097,33.812214 11.747796,33.820641 12.373075,34.179253 14.59107,30.370173 C15.456758,28.974747 14.859911,27.956753 13.727009,27.308222 13.727009,27.308222 13.143563,26.893551 12.430034,26.914457 z M12.476899,25.965199 C12.858714,25.975031 13.243579,26.076778 13.597611,26.279446 L14.762547,26.946314 C15.89545,27.594845 16.283611,29.031189 15.629529,30.154476 L9.99827,39.825336 C9.3441887,40.948627 7.8955526,41.333492 6.7626505,40.684963 L5.5977144,40.018093 C4.4648128,39.36956 4.0766516,37.93322 4.7307324,36.809929 L10.361992,27.139071 C10.811673,26.36681 11.636905,25.943567 12.476899,25.965199 z M25.604475,15.896126 L28.397127,15.896126 28.397127,18.669222 31.193958,18.669222 31.193958,21.438179 28.397127,21.438179 28.397127,24.211277 25.604475,24.211277 25.604475,21.438179 22.807644,21.438179 22.807644,18.669222 25.604475,18.669222 z M18.590466,14.738423 L18.590466,25.423859 35.4832,25.423859 35.4832,14.738423 z M19.937708,8.4539089 L34.13596,8.4539089 C35.592358,8.4539089 36.772999,9.6245346 36.772999,11.068575 L36.772999,29.09371 C36.772999,30.537748 35.592358,31.708374 34.13596,31.708374 L19.937708,31.708374 C18.48131,31.708374 17.300665,30.537748 17.300665,29.09371 L17.300665,11.068575 C17.300665,9.6245346 18.48131,8.4539089 19.937708,8.4539089 z M41,0 L41,41 z M22.517307,0 L31.556356,0 C32.238125,-4.4408921E-16 32.790806,0.54799187 32.790806,1.2239748 L32.790806,5.7905769 C32.790806,6.466558 32.238125,7.0145497 31.556356,7.0145497 L22.517307,7.0145497 C21.835541,7.0145497 21.28286,6.466558 21.28286,5.7905769 L21.28286,1.2239748 C21.28286,0.54799187 21.835541,-4.4408921E-16 22.517307,0 z M0,0 L0,41 z";
	}
	public class IconeFaseProducao : IIcone
	{
		public string Data => "M61.141499,56.763999 C62.49432,56.763999 63.591,57.860677 63.591,59.213499 63.591,60.56632 62.49432,61.662999 61.141499,61.662999 59.788677,61.662999 58.691998,60.56632 58.691998,59.213499 58.691998,57.860677 59.788677,56.763999 61.141499,56.763999 z M47.428497,56.763999 C48.781319,56.763999 49.877998,57.860677 49.877998,59.213499 49.877998,60.56632 48.781319,61.662999 47.428497,61.662999 46.075676,61.662999 44.978996,60.56632 44.978996,59.213499 44.978996,57.860677 46.075676,56.763999 47.428497,56.763999 z M33.312496,56.763999 C34.665318,56.763999 35.761997,57.860677 35.761997,59.213499 35.761997,60.56632 34.665318,61.662999 33.312496,61.662999 31.959679,61.662999 30.862997,60.56632 30.862997,59.213499 30.862997,57.860677 31.959679,56.763999 33.312496,56.763999 z M19.255499,56.763999 C20.60832,56.763999 21.704996,57.860677 21.704996,59.213499 21.704996,60.56632 20.60832,61.662999 19.255499,61.662999 17.902678,61.662999 16.805998,60.56632 16.805998,59.213499 16.805998,57.860677 17.902678,56.763999 19.255499,56.763999 z M5.5275002,56.763999 C6.880321,56.763999 7.9770007,57.860677 7.9770007,59.213499 7.9770007,60.56632 6.880321,61.662999 5.5275002,61.662999 4.1746778,61.662999 3.0779986,60.56632 3.0779982,59.213499 3.0779986,57.860677 4.1746778,56.763999 5.5275002,56.763999 z M61.141499,55.609 C59.150787,55.608999 57.536999,57.222788 57.536999,59.213499 57.536999,61.204209 59.150787,62.817999 61.141499,62.817999 63.13221,62.817999 64.745995,61.204209 64.745995,59.213499 64.745995,57.222788 63.13221,55.608999 61.141499,55.609 z M47.428497,55.609 C45.437786,55.608999 43.823997,57.222788 43.823997,59.213499 43.823997,61.204209 45.437786,62.817999 47.428497,62.817999 49.419209,62.817999 51.032997,61.204209 51.032997,59.213499 51.032997,57.222788 49.419209,55.608999 47.428497,55.609 z M33.312496,55.609 C31.321787,55.608999 29.707998,57.222788 29.707998,59.213499 29.707998,61.204209 31.321787,62.817999 33.312496,62.817999 35.303207,62.817999 36.916996,61.204209 36.916996,59.213499 36.916996,57.222788 35.303207,55.608999 33.312496,55.609 z M19.255499,55.609 C17.264788,55.608999 15.651,57.222788 15.651,59.213499 15.651,61.204209 17.264788,62.817999 19.255499,62.817999 21.246206,62.817999 22.859999,61.204209 22.859999,59.213499 22.859999,57.222788 21.246206,55.608999 19.255499,55.609 z M5.5275002,55.609 C3.5367885,55.608999 1.9229999,57.222788 1.9230001,59.213499 1.9229999,61.204209 3.5367885,62.817999 5.5275002,62.817999 7.5182109,62.817999 9.131999,61.204209 9.131999,59.213499 9.131999,57.222788 7.5182109,55.608999 5.5275002,55.609 z M5.3010001,53.83 L61.367996,53.83 C64.268036,53.829999 66.618996,56.180952 66.618996,59.080999 L66.618996,59.345999 C66.618996,62.246046 64.268036,64.596999 61.367996,64.597 L5.3010001,64.597 C2.4009526,64.596999 0.05,62.246046 0.05,59.345999 L0.05,59.080999 C0.05,56.180952 2.4009526,53.829999 5.3010001,53.83 z M25.705,43.564999 L40.828999,43.564999 40.828999,51.593 25.705,51.593 z M5.8659997,43.564999 L20.99,43.564999 20.99,51.593 5.8659997,51.593 z M25.084999,37.227 L41.449001,37.227 41.449001,42.637 25.084999,42.637 z M5.2459998,37.227 L21.610001,37.227 21.610001,42.637 5.2459998,42.637 z M46.233002,31.871999 L61.357002,31.871999 61.357002,39.899999 46.233002,39.899999 z M45.612999,25.533999 L61.977001,25.533999 61.977001,30.943999 45.612999,30.943999 z M53.709,11.065999 C56.59058,11.065999 59.028858,13.136708 59.841908,15.988629 L59.863293,16.067056 64.299004,19.257848 63.658306,29.602996 63.118736,29.599917 63.118736,20.053757 57.374928,16.132001 50.004044,16.132001 44.292606,20.053765 44.292606,29.599925 43.756069,29.602999 43.118999,19.257851 47.560852,16.044518 47.576092,15.988629 C48.389141,13.136708 50.827423,11.065999 53.709,11.065999 z M52.618,4.8249977 L54.799,4.8249977 54.799,10.383 52.618,10.383 z M49.473,0.05 L57.945,0.05 57.945,3.9819992 49.473,3.9819992 z";
	}
	public class IconeFatorConversao : IIcone
	{
		public string Data => "M12.388,23.009001 L17.887998,23.009001 17.887998,29.009001 12.388,29.009001 z M6.1939995,23.009001 L11.693999,23.009001 11.693999,29.009001 6.1939995,29.009001 z M0,23.009001 L5.5000001,23.009001 5.5000001,29.009001 0,29.009001 z M12.388001,16.299999 L17.888,16.299999 17.888,22.299999 12.388001,22.299999 z M6.1939995,16.299999 L11.694,16.299999 11.694,22.299999 6.1939995,22.299999 z M0,16.299999 L5.5000001,16.299999 5.5000001,22.299999 0,22.299999 z M30.175317,9.2061369 C32.099855,17.906603 29.362867,22.826119 22.829822,24.66666 L22.683012,24.706557 23.192789,26.625914 18.785326,23.865465 21.301691,19.505755 21.856836,21.595932 22.067697,21.560045 C27.142099,20.641916 29.14578,16.811239 27.735696,9.8601591 z M21.838001,1.4830005 L27.338001,1.4830005 27.338001,7.483 21.838001,7.483 z M16.915175,0 L20.458,3.8071229 16.899003,7.366996 16.903751,5.2043588 16.690787,5.184448 C11.551663,4.75794 8.6248033,7.9394991 8.1877666,15.018684 L5.662,15.019 C6.054886,6.1168873 9.9718776,2.0733864 16.758682,1.9864366 L16.910816,1.9858967 z";
	}
	public class IconeFeriado : IIcone
	{
		public string Data => "M40.057186,41.435002 L40.057186,48.223 48.16,48.223 48.16,41.435002 z M31.052389,41.435002 L31.052389,48.223 39.155205,48.223 39.155205,41.435002 z M22.123594,41.435002 L22.123594,48.223 30.226408,48.223 30.226408,41.435002 z M13.193796,41.435002 L13.193796,48.223 21.29661,48.223 21.29661,41.435002 z M4.2119999,41.435002 L4.2119999,48.223 12.314816,48.223 12.314816,41.435002 z M40.057186,33.887001 L40.057186,40.675 48.16,40.675 48.16,33.887001 z M31.052389,33.887001 L31.052389,40.675 39.155205,40.675 39.155205,33.887001 z M22.123594,33.887001 L22.123594,40.675 30.226408,40.675 30.226408,33.887001 z M13.193796,33.887001 L13.193796,40.675 21.29661,40.675 21.29661,33.887001 z M4.2119999,33.887001 L4.2119999,40.675 12.314816,40.675 12.314816,33.887001 z M40.057186,26.439001 L40.057186,33.227002 48.16,33.227002 48.16,26.439001 z M22.123594,26.439001 L22.123594,33.227002 30.226408,33.227002 30.226408,26.439001 z M13.193796,26.439001 L13.193796,33.227002 21.29661,33.227002 21.29661,26.439001 z M4.2119999,26.439001 L4.2119999,33.227002 12.314816,33.227002 12.314816,26.439001 z M40.057186,18.891001 L40.057186,25.678999 48.16,25.678999 48.16,18.891001 z M31.052389,18.891001 L31.052389,25.678999 39.155205,25.678999 39.155205,18.891001 z M22.123594,18.891001 L22.123594,25.678999 30.226408,25.678999 30.226408,18.891001 z M13.193796,18.891001 L13.193796,25.678999 21.29661,25.678999 21.29661,18.891001 z M4.2119999,18.891001 L4.2119999,25.678999 12.314816,25.678999 12.314816,18.891001 z M3.4070001,18.134001 L48.965,18.134001 48.965,48.980999 3.4070001,48.980999 z M3.5650003,16.643999 C2.7128246,16.643999 2.0220001,17.334824 2.0220001,18.186999 L2.0220001,49.089997 C2.0220001,49.94217 2.7128246,50.632996 3.5650003,50.632996 L48.808998,50.632996 C49.661175,50.632996 50.352001,49.94217 50.352001,49.089997 L50.352001,18.186999 C50.352001,17.334824 49.661175,16.643999 48.808998,16.643999 z M2.8529999,5.3289998 L8.9490004,5.3289998 8.9490004,9.5659984 C8.9490004,11.508937 10.524062,13.084 12.467,13.084 L13.438,13.084 C15.380938,13.084 16.956001,11.508937 16.956001,9.5659984 L16.956001,5.3289998 35.463001,5.3289998 35.463001,9.5659984 C35.463001,11.508937 37.038063,13.084 38.981003,13.084 L39.952,13.084 C41.894939,13.084 43.470001,11.508937 43.470001,9.5659984 L43.470001,5.3289998 49.521,5.3289998 C51.096668,5.3290003 52.374001,6.6063321 52.374001,8.1819995 L52.374001,49.475994 C52.374001,51.051667 51.096668,52.328999 49.521,52.328999 L2.8529999,52.328999 C1.2773319,52.328999 3.1584369E-07,51.051667 0,49.475994 L0,8.1819995 C3.1584369E-07,6.6063321 1.2773319,5.3290003 2.8529999,5.3289998 z M39.087093,0 L39.800072,0 C41.05151,7.9259516E-07 42.066002,1.0145237 42.066002,2.2660008 L42.066002,9.4839996 C42.066002,10.735477 41.05151,11.75 39.800072,11.75 L39.087093,11.75 C37.835655,11.75 36.821167,10.735477 36.821167,9.4839996 L36.821167,2.2660008 C36.821167,1.0145237 37.835655,7.9259516E-07 39.087093,0 z M12.57393,0 L13.286906,0 C14.538345,7.9259516E-07 15.552835,1.0145237 15.552835,2.2660008 L15.552835,9.4839996 C15.552835,10.735477 14.538345,11.75 13.286906,11.75 L12.57393,11.75 C11.322491,11.75 10.308,10.735477 10.308,9.4839996 L10.308,2.2660008 C10.308,1.0145237 11.322491,7.9259516E-07 12.57393,0 z";
	}
	public class IconeFinanceiro : IIcone
	{
		public string Data => "M0.36499789,21.801 L25.065998,21.801 25.065998,23.487 0.36499789,23.487 z M15.587999,13.478001 L15.605998,20.981 9.5897112,20.981 9.5679979,13.938164 12.328239,16.69825 z M7.1069956,11.546 L8.0269976,12.420321 8.0157723,20.981 1.9997103,20.981 1.977998,16.491348 z M22.64057,6.4960003 L23.336998,7.2037601 23.296484,20.981 17.280998,20.981 17.287125,11.844648 z M18.44664,0 L24.943995,0 24.943995,6.5205927 22.540545,4.1171246 12.30496,14.3524 7.1187482,9.166173 1.8289453,14.456004 0,12.627064 7.2106915,5.4855442 12.351398,10.626249 20.719219,2.2788448 z";
	}
	public class IconeFormaPagamento : IIcone
	{
		public string Data => "M35.723064,22.652535 L37.527039,23.795323 C37.527035,23.795323 36.124378,24.666933 35.158131,26.330115 34.191883,27.993296 34.172123,28.953636 35.099224,29.435505 35.868507,29.835347 36.424438,29.577139 37.721828,28.42932 39.019215,27.281498 40.372704,26.094519 41.515919,25.897602 42.65913,25.700684 44.297264,26.365027 44.766838,27.498944 45.236412,28.632862 45.064949,29.546856 44.874336,30.52385 44.824833,30.77759 44.754261,31.033707 44.664707,31.290274 L44.641766,31.35274 46.418411,32.384907 45.59304,33.805603 43.91077,32.828266 43.795734,33.006951 C43.67244,33.19357 43.541103,33.377999 43.402611,33.559426 42.546879,34.680447 41.317047,35.720764 41.317047,35.720764 L39.235878,34.619187 C39.235878,34.619187 40.852909,33.549431 41.793732,32.15849 42.734554,30.767556 43.38974,29.354193 42.732571,28.828881 42.075401,28.303568 41.261757,28.333191 40.331409,29.192015 39.401062,30.050841 37.409958,31.764483 36.381618,31.992297 35.353279,32.220112 34.073967,32.194477 32.909115,30.943659 31.744265,29.692845 32.162403,27.979864 32.358028,27.322625 32.42347,27.102755 32.538872,26.785769 32.695396,26.423466 L32.734127,26.335051 31.297142,25.500216 32.122517,24.07952 33.503262,24.881682 33.570377,24.777535 C33.773865,24.467429 33.995754,24.174744 34.231522,23.926003 35.174603,22.931034 35.723064,22.652535 35.723064,22.652535 z M39.847446,7.3672579 L20.488905,40.688732 37.7519,50.717892 57.110443,17.39642 z M14.35621,6.1695527 L6.9489093,43.987854 21.596151,46.837635 9.6600485,43.031525 21.00503,7.4527124 z M31.087498,6.066274 L15.582386,41.346085 29.807116,47.597714 18.771986,41.186756 37.530689,8.8979925 z M22.737877,5.6322936 L11.030441,42.347546 26.714254,47.348671 14.050465,41.783104 29.053415,7.6461399 z";
	}
	public class IconeGeradorArquivoBalanca : IIcone
	{
		public string Data => "M5.9840598,25.52767 L19.125254,25.52767 19.125254,26.443504 5.9840598,26.443504 z M30.437969,23.535 L40.606972,23.535057 40.607059,38.330635 44.966,38.330658 35.740746,47.028999 26.514999,38.330559 30.438057,38.330578 z M5.9840598,22.607466 L19.125254,22.607466 19.125254,23.523298 5.9840598,23.523298 z M5.9840598,19.627052 L19.125254,19.627052 19.125254,20.542885 5.9840598,20.542885 z M5.9840598,16.70764 L19.125254,16.70764 19.125254,17.623472 5.9840598,17.623472 z M5.9840598,13.969652 L19.125254,13.969652 19.125254,14.885484 5.9840598,14.885484 z M5.9840598,11.05024 L19.125254,11.05024 19.125254,11.966072 5.9840598,11.966072 z M25.259214,10.434079 L25.889824,11.463033 25.888102,11.466548 C25.842306,11.562983 25.816639,11.671112 25.816639,11.785324 25.816639,12.19141 26.141096,12.520607 26.541332,12.520607 26.941566,12.520607 27.266026,12.19141 27.266026,11.785324 27.266026,11.379237 26.941566,11.050039 26.541332,11.050039 26.447527,11.050039 26.357882,11.068123 26.275606,11.101041 L26.265989,11.105027 z M26.541126,9.3815966 C27.849249,9.3815966 28.909685,10.457863 28.909685,11.785503 28.909685,13.113143 27.849249,14.189407 26.541126,14.189407 25.233007,14.189407 24.172565,13.113143 24.172565,11.785503 24.172565,10.457863 25.233007,9.3815966 26.541126,9.3815966 z M5.9840598,8.2520409 L16.548567,8.2520409 16.548567,9.1678724 5.9840598,9.1678724 z M26.54137,8.1800385 C24.579554,8.1800385 22.989193,9.7942028 22.989193,11.785376 22.989193,13.465425 24.121395,14.877079 25.653625,15.277205 L25.716776,15.291996 25.716776,30.841927 3.8984637,30.841927 3.8984637,33.464897 25.716776,33.464897 27.281578,33.464897 27.281578,30.841927 27.281578,15.311764 27.429117,15.277205 C28.961342,14.877079 30.093548,13.465425 30.093548,11.785376 30.093548,9.7942028 28.503181,8.1800385 26.54137,8.1800385 z M3.9195139,5.2129993 L3.8580003,28.87648 21.79838,28.87648 21.79838,9.6006889 17.336342,5.2129993 z M0,0 L27.911787,0.00075544807 38.217587,9.7175083 38.217945,21.340893 38.217999,21.403343 28.359045,21.405283 28.359104,36.316132 20.925016,36.401093 28.183273,43.341 0.0013286916,43.338665 z";
	}
	public class IconeGeradorArquivoContabilidade : IIcone
	{
		public string Data => "M67.64534,46.049 L93.890999,69.90522 67.64534,93.842003 67.64534,80.417816 41.937,80.417816 41.937,59.310925 67.64534,59.310925 z M18.823772,31.416061 L20.777464,32.345779 C20.777464,32.345779 17.705145,37.586964 16.921959,41.713272 16.138695,45.839584 16.380186,49.091549 16.801878,50.055321 17.223566,51.0191 17.585064,51.921913 18.368256,52.494144 19.151362,53.066364 19.573053,53.306896 19.573053,53.306896 L17.434452,54.270756 C17.434452,54.270756 15.567013,53.427013 14.452548,49.813149 13.338079,46.19928 14.663043,41.13987 15.325757,38.820927 15.988396,36.502049 18.277838,32.1353 18.277838,32.1353 z M14.878914,30.002012 L16.892788,30.99085 C16.892788,30.99085 12.434821,38.761822 11.802286,43.219326 11.169749,47.676834 10.673169,51.747986 12.796824,54.482773 14.725021,56.965862 18.361307,56.398834 19.716654,56.188019 21.071997,55.977188 24.031559,54.603188 26.802647,52.826267 29.573734,51.049351 30.386623,50.296429 30.386623,50.296429 L30.320301,52.756531 C30.320301,52.756531 24.11532,57.243992 20.952786,57.846348 17.790251,58.448689 14.124133,57.888607 12.618163,56.804382 11.112199,55.720169 9.2079363,53.022266 9.5392552,48.053082 9.8705597,43.083904 10.417345,40.357391 12.043774,36.412163 13.670198,32.466934 14.878914,30.002012 14.878914,30.002012 z M40.470978,17.790575 C42.536076,17.790575 44.044193,18.418394 44.995331,19.674051 45.946472,20.929701 46.241489,22.686026 45.880383,24.94302 L31.605425,24.94302 C32.289402,22.813177 33.438923,21.088642 35.053986,19.769415 36.66906,18.450184 38.474724,17.790575 40.470978,17.790575 z M41.099796,14.501399 C37.640869,14.501399 34.545063,15.69733 31.812347,18.089201 29.079641,20.481068 27.394926,23.576197 26.758198,27.37458 26.084183,31.395466 26.628277,34.514439 28.390493,36.73148 30.152708,38.948524 32.900951,40.057045 36.635197,40.057045 40.455498,40.057045 43.657764,39.278309 46.242001,37.72081 L46.857414,34.049564 C44.058464,35.861343 41.170448,36.767239 38.193371,36.767239 35.543255,36.767239 33.5849,36.028214 32.318317,34.55019 31.051724,33.072155 30.611792,30.974308 30.998524,28.256634 L49.661205,28.256634 50.004879,26.206463 C50.620277,22.535213 50.15469,19.666567 48.608089,17.600498 47.061474,15.534432 44.558712,14.501399 41.099796,14.501399 z M56.591549,2.3336358 C56.591549,2.3336339 59.746037,2.7749419 61.715252,6.311111 63.01268,8.6409054 62.297649,14.245817 62.297649,14.245817 L60.248413,13.060318 C60.248413,13.060317 60.448818,12.337468 60.308205,9.5452585 60.167584,6.7530389 57.91769,5.0662923 57.91769,5.0662923 z M48.681236,1.5019163 C49.133534,1.4975346 49.597893,1.5201321 50.069706,1.5774459 52.586052,1.8831354 54.749657,3.1528368 56.066223,5.1046047 57.415607,7.1050077 57.241829,11.711824 57.241829,11.711828 L54.937733,10.936234 C54.937733,10.936234 55.08408,10.110761 55.008534,9.2663565 54.867615,7.6911569 54.302685,6.2566676 53.738285,5.6453242 53.173866,5.0339837 52.42144,3.9523332 49.387714,3.7172468 46.492687,3.4929092 42.285645,5.6685953 42.285645,5.6685939 L41.298069,3.5060341 C41.298069,3.5060346 42.473633,2.9413545 43.861149,2.4475548 44.98851,2.0463436 46.721359,1.5208933 48.681236,1.5019163 z M37.26511,0.95425845 L38.383987,1.6999693 C38.383987,1.6999713 38.446205,2.279773 38.446205,2.6524663 38.446205,3.0251684 38.446678,3.6671248 38.612392,3.9570277 38.778027,4.2469306 38.90263,4.5161128 39.130405,4.7024193 39.358185,4.8888102 39.751442,5.1372733 39.751442,5.1372733 39.751442,5.1372733 39.916996,5.2822266 39.792786,5.5928474 39.668503,5.9034653 39.772148,6.1104937 39.523663,6.4832711 39.275169,6.8559675 39.295883,6.8766856 39.047394,7.1458669 38.798897,7.4150496 38.819618,7.1665864 38.653904,7.6427951 38.48827,8.1190863 38.343262,8.4089871 38.281208,8.6988888 38.219059,8.9887104 38.053421,9.4235668 38.053421,9.4857216 38.053421,9.5478001 38.736759,9.5892372 38.736759,9.5892353 38.736759,9.5892372 39.109535,9.1336594 39.192318,8.9059153 39.275169,8.6781702 39.275169,8.4710636 39.316597,8.4089871 39.358021,8.3468332 39.378738,8.1811638 39.627232,8.2018833 39.875725,8.2226009 40.331276,8.2847567 40.84898,8.3675509 41.366669,8.450347 41.615715,8.4296284 41.781353,8.4089871 41.947067,8.3882704 42.154205,8.3675528 42.154205,8.3675509 42.154205,8.3675528 41.864212,9.2993355 41.760632,9.5685148 41.657143,9.8376999 41.512138,10.065523 41.491425,10.231113 41.470722,10.396784 41.739922,10.707407 41.822784,10.665964 41.90564,10.624611 42.402615,10.458937 42.506191,10.27255 42.60968,10.086242 43.002777,9.8584185 43.12706,9.6306706 43.251267,9.4028473 43.499531,9.1751003 43.561676,9.1130295 43.623814,9.0508661 43.99651,8.8231201 44.369289,8.9473534 44.741985,9.0715828 46.129524,9.5271607 46.336586,9.6306706 46.543648,9.7341843 46.99905,10.044803 47.330402,10.210473 47.661758,10.376064 47.910011,10.583176 48.199921,10.583168 48.489845,10.583176 48.635006,10.479662 48.717861,10.62461 48.800716,10.769561 49.235252,11.618467 49.235252,11.784133 49.235252,11.949809 49.339214,12.301865 49.422066,12.343223 49.504917,12.384661 49.836582,12.52961 49.960869,12.571047 50.085072,12.612406 50.623482,12.508891 50.830631,12.446738 51.037701,12.384661 51.183086,12.260429 51.597286,12.467454 52.011421,12.674562 52.881413,13.026538 53.067841,13.026538 53.254185,13.026538 54.144028,13.171488 54.392536,13.337159 54.641022,13.502828 55.262444,13.440676 55.490215,13.419955 55.718086,13.399316 56.297367,13.212926 56.628727,13.337159 56.960064,13.461394 57.995934,14.165426 58.410065,14.434608 58.824261,14.703795  59.362995,15.283595 59.797829,15.573498 60.232754,15.863398 60.460754,16.236101 61.019886,16.629513 61.579029,17.022928 62.262825,17.395704 62.532024,17.499218 62.801239,17.602734 64.354164,17.582016 64.66481,18.658825 64.975441,19.735556 65.078384,21.019394 64.995522,21.702711 64.912682,22.38603 64.870689,22.841604 64.76712,23.152145 64.663628,23.462765 64.601494,23.897932 64.311569,24.084242 64.02166,24.270626 62.944809,25.367924 61.930138,26.27907 60.915371,27.190125 60.480541,28.328859 60.19062,28.598042 59.9007,28.867224 59.962833,28.992088 59.590057,29.344149 59.217361,29.696123 59.445145,29.69644 58.989586,29.924273 58.533951,30.15201 58.036957,30.235046 57.892048,30.44207 57.747044,30.649181 57.519264,30.710861 57.581406,31.042196 57.643559,31.373543 57.415707,33.423092 57.5607,34.313515 57.705627,35.203934 57.436409,36.094452 57.31221,36.322186 57.18792,36.549931 57.291504,36.674328 57.001572,37.295563 56.711655,37.916809 56.628799,38.144558 56.463165,38.537975 56.297451,38.931385 56.23539,39.014496 56.069675,39.221596 55.904038,39.428623 55.779755,39.718052 55.779755,39.966595 55.779755,40.215061 56.090389,40.526161 55.821182,40.857494 55.551968,41.188759 55.096428,42.286049 54.827221,42.493076 54.55801,42.70018 54.454433,42.61731 54.123097,43.134964 53.791813,43.652691 53.688252,43.901379 53.605389,44.2948 53.522614,44.68821 53.708965,44.978115 53.439743,45.226666 53.170551,45.475124 52.694279,45.764637 52.507855,45.868153 52.321499,45.971668 52.21801,46.012936 52.031582,46.261414 51.845234,46.50988 52.01086,46.654667 51.555309,46.696014 51.099766,46.737461 51.079044,46.7789 50.51992,46.737461 49.960796,46.696014 49.753735,46.612671 49.360237,46.633389 48.966824,46.654114 48.697628,46.633389 48.304131,46.819698 47.910633,47.006081 47.889908,47.04697 47.600071,47.150562 47.31015,47.254082 47.31015,47.047359 46.792461,47.420139 46.274685,47.792835 46.088341,47.999474 45.79842,48.061546 45.508492,48.123699 45.446434,47.916439 45.073647,48.123463 44.700874,48.330566 44.307461,48.662064 44.245319,48.765575 44.183178,48.869095 43.541275,49.200912 43.25135,49.49081 42.96143,49.780708 42.94072,50.008453 42.630085,50.298359 42.319447,50.588268 42.050243,50.608982 41.760323,50.81609 41.470406,51.023117 41.325478,51.022636 41.242622,51.416054 41.159763,51.809551 40.973415,52.285202 40.973415,52.616554 40.973415,52.947811 41.118343,53.506416 41.118343,53.817039 41.118343,54.127579 41.201195,55.162483 40.86985,55.43166 40.538582,55.700844 40.497143,55.638382 40.310726,55.845402 40.124371,56.052509 39.95874,56.01075 39.730957,56.507694 39.503101,57.004616 39.233902,57.439541 39.130405,57.708729 39.02684,57.977909 39.006123,58.37149 38.654141,58.702751 38.302067,59.034008 38.15707,59.344704 37.929302,59.7174 37.701511,60.090172 37.701511,60.25592 37.432312,60.463032 37.163105,60.670059 37.121681,61.001244 36.852463,61.104763 36.583267,61.208271 36.376205,61.166992 36.127716,61.353382 35.879219,61.539688 35.506443,61.871498 35.382229,62.202755 35.257946,62.534103 35.154377,62.864796 34.968025,63.113262 34.781681,63.36174 34.533188,63.486206 34.367474,63.672504 34.201839,63.858898 34.036202,64.127998 34.036202,64.127998 34.036202,64.127998 33.601284,63.589966 33.663425,63.362213 33.725575,63.134392 34.036202,63.093021 34.098274,62.637447 34.160412,62.181953 34.243267,62.202675 34.077553,62.057732 33.911915,61.912777 33.663425,61.622868 33.373505,61.457203 33.083591,61.291615 33.042164,61.270897 32.814377,61.125938 32.586605,60.980999 32.565887,60.898197 32.068901,60.670376 31.57192,60.442635 31.40621,59.759304 31.219858,59.573002 31.033512,59.386608 30.909226,58.972477 30.66073,59.22102 30.412239,59.469482 30.495094,59.67651 30.184462,59.593716 29.873825,59.510841 29.873825,59.531559 29.583906,59.117432 29.293985,58.703377 29.563194,58.724018 29.169781,58.620506 28.776291,58.516987 28.444943,58.372036 28.444943,58.372036 28.444943,58.372036 28.879862,57.440258 29.128351,56.964043 29.376843,56.487831 30.018747,55.949471 30.080891,55.866596 30.14303,55.783794 30.867798,55.121201 31.157719,54.852024 31.447638,54.58284 31.903193,54.39653 32.089619,54.210133 32.27597,54.023815 32.565887,53.878868 32.628037,53.692482 32.690174,53.50618 32.814377,53.257717 32.752243,53.216278 32.690174,53.174839 32.462318,53.112766 32.462318,53.112766 L32.42096,47.770618 30.992083,47.770618 C30.992083,47.770618 30.992083,46.300472 30.826368,46.093445 30.66073,45.886341 30.72287,46.134884 30.640017,45.803539 30.557159,45.472271 30.515812,44.892475 30.412239,44.892475 30.308666,44.892475 27.2232,45.058147 27.2232,45.058147 27.2232,45.058147 27.078194,42.946121 27.098915,42.594135 27.119625,42.242161 27.326687,42.387024 27.119625,42.159287 26.912558,41.931541 26.726213,41.765873 26.726213,41.53812 26.726213,41.310379 26.788273,40.979042 26.850418,40.751297 26.912558,40.523556 26.953989,39.943748 27.078194,39.736725 27.202482,39.529697 27.347399,39.239788 27.388834,39.07412 27.430258,38.908447 27.678747,37.976742 27.05748,37.459103 26.436295,36.941463 26.477642,37.003532 26.353437,36.713631 26.229149,36.423798 26.125654,36.527325 26.104944,36.092468 26.08423,35.657608 26.187801,35.222839 25.918594,35.1814 25.649385,35.139969 25.297325,35.036457 24.821054,35.119324 24.344706,35.202122 24.158358,35.222839 23.827011,35.222839 23.495657,35.222839 23.226452,35.222839 23.226452,35.222839 23.226452,35.222839 23.019394,34.91222 22.977963,34.663761 22.936611,34.415291 22.750181,34.311779 22.791609,33.939087 22.83304,33.566383 22.915897,33.400719 22.936611,33.110813 22.957247,32.820911 22.729469,31.827057 22.64669,31.516426 22.563839,31.205889 22.563839,31.060944 22.27392,30.874552 21.983995,30.688244 22.066853,30.564013 21.507727,30.460499 20.948603,30.356909 20.327337,30.274115 20.099556,30.232674 19.871779,30.191313 19.540432,30.108442 19.374792,30.067083 19.209078,30.025648 18.67067,30.108442 18.401465,29.715023 18.132258,29.321613 18.028761,29.114586 17.883764,29.031784 17.738842,28.948914 17.904478,28.783325 17.221144,28.907555 16.537735,29.031784 16.330671,29.2181 16.061464,29.031784 15.79226,28.845406 15.253843,28.01713 14.839641,27.520205 14.425509,27.02327 14.446229,27.126783 14.384086,26.712732 14.321941,26.298601 14.342654,25.884468 14.280514,25.532482 14.21845,25.180428 14.280515,24.372957 14.073448,24.372957 13.866386,24.372957 13.680042,24.600706 13.12091,24.6835 12.561783,24.766371 12.106233,24.621422 11.795595,25.076916 11.484962,25.532482 11.298532,26.029417 10.863689,26.236443 10.428852,26.443546 10.428852,26.298601 9.78687,26.609137 9.1448861,26.919758 9.0828219,27.147501 8.4408398,27.126783 7.7988558,27.106066 7.9024229,27.147501 7.1776681,27.064705 6.4528251,26.981834 6.1836214,26.878325 5.9765578,26.919758 5.769495,26.961195 5.7073522,26.443546 5.6038556,26.153646 5.5002833,25.863745 5.4174304,25.635996 5.4795733,25.284023 5.5417142,24.931967 5.5417142,24.290083 5.5417152,24.290083 5.5417142,24.290083 5.2517929,24.6835 4.5891009,24.890606 3.926481,25.097631 3.3880687,25.242582 3.1809245,25.36681 2.9738584,25.491049 3.0152862,25.739515 2.8082238,25.36681 2.6011627,24.994116 2.8289411,24.538546 2.3319583,24.579981 1.8348943,24.621422 1.6071158,24.579981 1.5656894,24.228012 1.5243389,23.875946 1.5036251,23.296227 1.2551289,23.068483 1.0065612,22.840651 0.7586181,22.757856 0.46869768,22.281645 0.17878004,21.805361 -0.15193952,21.805361 0.075834633,21.287706 0.30361495,20.770062 0.24084266,20.625191 0.61354075,20.376648 0.98631863,20.128181 1.5040968,19.776756 1.5040975,19.776756 L5.0642705,19.839787 C5.0642701,19.839787 6.3278356,19.487251 6.659103,18.720818 6.9903717,17.954475 7.0116387,17.933914 7.0116387,17.933914 7.0116387,17.933914 7.7366352,19.777231 9.2482968,19.81867 10.759967,19.860106 14.550267,19.860106 14.550267,19.860106 14.550267,19.860106 14.964238,19.549246 15.088527,19.300705 15.21281,19.05224 15.274325,18.783058 15.274324,18.783058 15.274325,18.783058 15.273696,19.901228 16.267582,19.901228 17.261553,19.901228 21.175905,19.901228 21.175905,19.901228 21.175905,19.901228 22.87392,19.445652 23.350185,18.306847 23.82654,17.167961 24.592491,13.088777 24.592491,13.088777 L29.997326,13.088777 C29.997326,13.088777 31.944069,12.985103 32.544544,11.28713 33.145103,9.5892372 33.496929,7.6218414 33.496929,7.621841 L33.703522,6.1924186 C33.703522,6.1924181 33.703133,5.3025494 33.806622,5.157598 33.910194,5.0126486 33.972488,4.9508081 34.117497,4.8265743 34.262409,4.702342 34.469158,4.0184741 34.572739,3.9977551 34.676224,3.9770379 34.945511,3.9568684 35.007572,3.9155154 35.069721,3.8740742 35.918919,3.0251684 36.105343,2.631753 36.291695,2.2382569 36.415588,2.1135495 36.643372,1.7614913 36.871067,1.409516 37.26511,0.9542609 37.26511,0.95425845 z M20.338316,0 L20.339684,0 23.188042,0 31.366545,0 30.88316,2.443012 22.704653,2.443012 21.890793,6.5562573 30.048456,6.5562573 29.565075,8.9992657 21.407408,8.9992657 20.055458,15.831969 17.207098,15.831969 18.559048,8.9992657 18.536846,8.9992657 19.020226,6.5562573 19.042433,6.5562573 19.8563,2.443012 19.854927,2.443012 z M4.1190391,0 L4.1569963,0 6.9674931,0 7.0054489,0 11.56121,11.444044 13.825659,0 16.674114,0 13.541426,15.831969 13.307995,15.831969 10.692966,15.831969 10.459535,15.831969 6.0343514,4.7159181 3.8348051,15.831969 0.98634372,15.831969 z";
	}
	public class IconeGeradorArquivoEfdContribuicao : IIcone
	{
		public string Data => "M26.889,29.163216 L26.067881,29.298328 C24.222742,29.588215 22.242762,29.817505 20.177772,29.96981 17.586414,30.160938 15.106509,30.212734 12.840831,30.142742 L12.232928,30.119627 12.352666,30.179192 12.356385,30.181091 12.36007,30.182877 12.533785,30.269293 C14.521168,31.229053 16.773359,31.77054 19.159691,31.77054 20.41173,31.77054 21.626841,31.621481 22.786158,31.341156 L23.00217,31.28656 23.057724,31.272659 23.067802,31.269852 23.100935,31.261463 C24.276066,30.950508 25.391035,30.503946 26.425655,29.940815 L26.889,29.676533 z M6.6908802,25.128628 L6.9696974,25.548349 C7.0736374,25.698215 7.1806039,25.846048 7.2905125,25.991774 L7.3456744,26.06308 7.4798869,26.23782 7.8884286,26.73164 7.8282112,26.695333 C7.1375994,26.263407 6.7409653,25.774799 6.6948976,25.239412 6.6921362,25.207306 6.6906466,25.17518 6.6904191,25.14304 z M30.979969,23.535 L41.148971,23.535057 41.149059,38.330635 45.507999,38.330658 36.282745,47.028999 27.056999,38.330559 30.980057,38.330578 z M5.6948423,23.102596 L5.4267577,23.200266 C2.6123859,24.24486 0.95686577,25.463577 1.0595088,26.656479 1.2209626,28.532877 5.6885509,29.8347 12.026197,30.135519 L12.257042,30.144764 12.246339,30.139334 12.143052,30.083818 12.101061,30.061956 12.03501,30.025747 11.939897,29.974628 11.889606,29.946051 11.824948,29.910608 11.662269,29.816845 11.638059,29.803089 11.629847,29.798164 11.552018,29.753307 C10.302903,29.015421 9.1586665,28.089272 8.1702251,26.988016 L8.0865382,26.892471 8.1112231,26.906 C11.004616,28.430349 17.895357,29.151781 25.831392,28.566454 L26.889,28.480936 26.889,23.992916 26.56485,24.036335 C25.261175,24.20475 23.903171,24.343006 22.506266,24.446035 16.51375,24.888021 11.117239,24.584915 7.5877213,23.753649 L7.5134448,23.735596 7.4648351,23.777201 C7.0649876,24.133018 6.8061408,24.494215 6.7055492,24.855122 L6.6754307,24.994682 6.4151544,24.559441 C6.2321543,24.239029 6.0618595,23.911001 5.9049248,23.575983 z M33.670124,16.94532 L33.476795,16.985334 C30.942032,17.49999 28.053572,17.897913 24.976332,18.124876 19.469696,18.531021 14.466332,18.308008 10.952461,17.624155 L10.728803,17.579205 10.563345,17.681566 C9.4973191,18.359978 8.9307069,19.070442 8.9909906,19.771046 9.0532178,20.494249 9.7768106,21.129913 11.01543,21.653069 L11.207907,21.731585 11.279223,21.71726 11.821908,21.615391 11.568607,21.700279 11.336466,21.781071 11.447178,21.823252 C14.543823,22.964144 20.259869,23.49958 26.834116,23.128973 L26.889,23.125401 26.889,21.389999 26.874,21.389999 26.874,19.913 33.57933,19.913 33.597141,19.795675 33.60733,19.709208 33.614159,19.660173 33.641075,19.422886 33.647964,19.364397 33.648708,19.355616 33.651375,19.33206 C33.723003,18.620005 33.735939,17.900906 33.688263,17.181492 z M26.31686,11.830008 L26.31686,14.768461 27.019463,14.768461 C27.282276,14.768461 27.471971,14.754336 27.588539,14.726093 27.741142,14.689778 27.867781,14.628246 27.968456,14.541494 28.06913,14.454742 28.15126,14.312006 28.214844,14.113284 28.278427,13.914565 28.310219,13.643719 28.310219,13.300747 28.310219,12.957776 28.278427,12.694495 28.214844,12.510904 28.15126,12.327315 28.062242,12.184074 27.94779,12.081182 27.83334,11.978292 27.688156,11.908689 27.512241,11.872375 27.380835,11.844131 27.123318,11.830009 26.739696,11.830008 z M25.375818,11.079508 L27.095764,11.079508 C27.483629,11.079508 27.779291,11.107752 27.982761,11.164241 28.256172,11.240905 28.49037,11.377084 28.685364,11.572779 28.880356,11.768475 29.028717,12.008051 29.130453,12.291505 29.232189,12.574961 29.283052,12.924489 29.283052,13.340089 29.283052,13.705251 29.235367,14.019978 29.139992,14.284266 29.02342,14.607062 28.857042,14.868325 28.640856,15.068054 28.477655,15.219367 28.257233,15.337389 27.979582,15.422121 27.771875,15.484663 27.494224,15.515937 27.146631,15.515937 L25.375818,15.515937 z M21.408222,11.079508 L24.603317,11.079508 24.603317,11.830008 22.349264,11.830008 22.349264,12.880103 24.294933,12.880103 24.294933,13.630604 22.349264,13.630604 22.349264,15.515937 21.408222,15.515937 z M17.059389,11.079508 L20.515175,11.079508 20.515175,11.830008 18.000431,11.830008 18.000431,12.813526 20.340321,12.813526 20.340321,13.561002 18.000431,13.561002 18.000431,14.768461 20.604196,14.768461 20.604196,15.515937 17.059389,15.515937 z M19.120603,4.3230209 C18.065931,4.3247647 17.005356,4.4347324 15.957851,4.6577177 L15.882396,4.6747098 15.872771,4.6766858 15.833329,4.68576 15.609193,4.7362375 15.554794,4.7498455 15.522324,4.7573166 15.392037,4.7905569 15.261657,4.8231702 15.229595,4.8320012 15.175251,4.8458672 14.954328,4.9078302 14.915402,4.9185514 14.906029,4.9213753 14.831663,4.9422331 C13.46444,5.3431811 12.183645,5.9290833 11.022196,6.6689005 L10.943496,6.7203379 10.933927,6.7263384 10.903757,6.7463121 10.73434,6.8570428 10.678674,6.8953218 10.631336,6.9266605 10.516414,7.0069008 10.451569,7.0514908 10.423801,7.0715652 10.335618,7.1331353 C8.8742723,8.1806488 7.6721481,9.4534197 6.7527808,10.868635 L6.7164449,10.926117 6.7123927,10.932236 6.68969,10.968443 6.5726828,11.153552 6.5534872,11.185659 6.5335936,11.217384 6.4221095,11.405416 6.4001688,11.442121 6.3965668,11.448501 6.3619422,11.506898 C5.5217265,12.965199 4.9537272,14.583161 4.7222394,16.300156 L4.7097749,16.402966 4.7052249,16.435656 4.6966943,16.510853 4.6804494,16.644855 4.6753291,16.699203 4.6680054,16.763771 4.6510171,16.957363 4.6477451,16.99209 4.6470155,17.002939 4.6391234,17.092888 C4.5897164,17.75202 4.5905732,18.416281 4.6431984,19.080362 L4.6481785,19.130772 4.6017474,19.163584 C3.7577805,19.780508 3.3183943,20.419943 3.372791,21.052128 3.4394776,21.82716 4.2408132,22.504169 5.6051311,23.053102 L5.6870135,23.084953 5.6796211,23.068298 C5.5362086,22.72645 5.4064812,22.377926 5.2910933,22.023354 L5.2689175,21.952127 5.2659229,21.943171 5.2545513,21.905985 5.1886701,21.694359 5.1739215,21.642309 5.1645388,21.611626 5.1298646,21.486814 5.09451,21.362038 5.0865587,21.330946 5.0720882,21.278862 5.0183777,21.064272 5.0087203,21.026497 5.0066151,21.017279 4.9885325,20.945038 C4.8698496,20.443577 4.781043,19.939028 4.7214822,19.433662 L4.7007646,19.223475 4.7475833,19.190948 C5.7086662,18.544743 7.1245579,17.925394 8.8866347,17.368616 L9.3069123,17.239325 9.2367515,17.219988 C7.1491369,16.626158 5.8904065,15.817507 5.8074364,14.853238 5.6484112,13.005055 9.8519487,11.088223 16.077784,9.8723297 L16.620466,9.7704573 16.367166,9.8553495 C13.228014,10.927676 11.353886,12.20437 11.461055,13.449883 11.682309,16.021269 20.264517,17.486031 30.629951,16.721523 31.601711,16.649847 32.554649,16.561127 33.483559,16.457066 L33.57806,16.445864 33.583477,16.491501 C33.593349,16.582767 33.602276,16.674057 33.610248,16.765352 L33.612877,16.799479 33.81617,16.756901 C35.388279,16.421255 36.816177,16.039307 38.058464,15.624864 L38.112694,15.606423 37.832916,15.659532 C36.723816,15.861691 35.556763,16.041225 34.344017,16.194038 L33.596184,16.283278 33.56068,16.035673 C33.507893,15.699183 33.441566,15.363413 33.3615,15.029053 L33.343479,14.957925 33.341381,14.948838 33.331745,14.911596 33.278221,14.700319 33.263794,14.649043 33.255863,14.618417 33.220592,14.495519 33.18602,14.372646 33.176659,14.342437 33.161945,14.291179 33.096195,14.082776 33.084854,14.046181 33.081867,14.03737 33.059738,13.967226 C32.634491,12.678139 32.013073,11.470538 31.228418,10.375466 L31.173853,10.301249 31.167496,10.292238 31.146345,10.263839 31.02887,10.10406 30.988256,10.051554 30.955032,10.006943 30.869966,9.898634 30.822639,9.837451 30.801334,9.8112535 30.736044,9.7281227 C29.625038,8.3502932 28.275122,7.2168674 26.774124,6.350039 L26.713163,6.3157835 26.706671,6.3119621 26.668251,6.2905464 26.471937,6.1802349 26.437889,6.162138 26.404238,6.1433816 26.2047,6.0382123 26.165878,6.0175796 26.159134,6.014194 26.097176,5.9815383 C24.550482,5.1893406 22.834452,4.6538 21.013388,4.4355431 L20.904346,4.4237909 20.869671,4.4194994 20.789907,4.4114556 20.647793,4.3961396 20.59017,4.391315 20.521669,4.384407 20.316269,4.3683825 20.27951,4.3653064 20.268028,4.3646178 20.172605,4.3571744 C19.823063,4.3338828 19.472162,4.3224392 19.120603,4.3230209 z M0,0 L27.911787,0.00075544807 38.217586,9.7175083 38.217945,21.340893 38.217998,21.403343 33.233726,21.405275 33.252865,21.389999 28.365999,21.389999 28.365999,30.695 28.359083,30.695 28.359104,36.316132 20.925016,36.401093 28.183273,43.341 0.0013286915,43.338665 z";
	}
	public class IconeGeradorArquivoInventario : IIcone
	{
		public string Data => "M15.641001,32.237003 L15.641001,34.237003 24.008999,34.237003 24.008999,32.237003 z M4.7179985,30.030002 L11.012,30.030002 11.012,36.324001 4.7179985,36.324001 z M15.641001,28.678001 L15.641001,30.678001 24.008999,30.678001 24.008999,28.678001 z M3.3649998,28.677002 L3.3649998,37.677002 12.365001,37.677002 12.365001,28.677002 z M30.437969,23.535 L40.606973,23.535057 40.607059,38.330634 44.966,38.330658 35.740746,47.028999 26.514999,38.330559 30.438057,38.330578 z M15.641001,23.216002 L15.641001,25.216001 24.008999,25.216001 24.008999,23.216002 z M15.641001,19.775 L15.641001,21.775001 24.008999,21.775001 24.008999,19.775 z M4.7179985,17.569 L11.012,17.569 11.012,23.863001 4.7179985,23.863001 z M15.641001,16.216 L15.641001,18.215999 33.310002,18.215999 33.310002,16.216 z M3.3649998,16.216 L3.3649998,25.216001 12.365001,25.216001 12.365001,16.216 z M15.641001,10.569 L15.641001,12.569 33.310002,12.569 33.310002,10.569 z M15.641001,7.1280003 L15.641001,9.1280003 30.613,9.1280003 30.613,7.1280003 z M10.518972,4.2890053 L12.070004,5.4874191 6.9952679,11.761005 3.6339989,7.8767719 5.0213223,6.3334217 7.0792599,8.7000599 z M15.641001,3.5690002 L15.641001,5.5690002 27.079001,5.5690002 27.079001,3.5690002 z M3.3649998,3.5690002 L3.3649998,12.569 12.365001,12.569 12.365001,3.5690002 z M0,0 L27.911788,0.00075531006 38.217587,9.7175083 38.217946,21.340893 38.217999,21.403343 28.359046,21.405283 28.359105,36.316132 20.925017,36.401093 28.183274,43.341 0.001329422,43.338665 z";
	}
	public class IconeGeradorArquivoLivroCaixaDigitalProdutorRural : IIcone
	{
		public string Data => "M23060 18896l7480 0 0 11640 3131 0 -6872 6871 -6872 -6871 3132 0 0 -11640zm-16471 -2507l-2965 0 0 -4379 866 0 0 3557 2099 0 0 822zm-6485 13426l-20 0 0 -25947c0,-1061 435,-2028 1136,-2730l3 -3c702,-701 1668,-1136 2730,-1136l20872 0c392,0 749,160 1006,418l0 3c258,258 418,613 418,1004l0 16365 -1493 0 0 -16296 -20803 0c-654,0 -1248,266 -1677,695l-3 3c-429,429 -695,1022 -695,1677l0 23338c451,-267 977,-421 1536,-421l1677 0 17162 0 0 949 -18038 0c-775,0 -1482,307 -1996,800 -519,499 -842,1188 -842,1948 0,759 323,1449 842,1947 514,493 1221,800 1996,800l17142 0 809 809 -18367 0c-951,0 -1820,-378 -2454,-986 -644,-618 -1044,-1474 -1044,-2417 0,-283 36,-558 104,-821zm3810 2295l16022 0 -2681 -2681 4698 0 0 -574 -18038 0c-478,0 -911,186 -1222,485 -306,294 -497,699 -497,1143 0,444 190,848 497,1142 312,299 744,485 1222,485zm11717 -24957c901,-6 1636,719 1643,1620 7,901 -718,1636 -1619,1643 -901,6 -1636,-719 -1643,-1620 -7,-901 718,-1636 1619,-1643zm-4784 -2572l163 -1 0 -20 -624 -383 131 -215 744 457 1 159 225 -1 11 1473 -225 1 2 285c172,-1 534,-9 2039,-36l-15 -1958c-351,-259 -528,-70 -390,-690l4199 2c119,428 39,460 -313,587l34 2173 789 362 -36 1729c-231,-131 -169,-488 -371,-804 -119,-187 -373,-437 -548,-542 -1335,-807 -3079,82 -2903,1843l-1970 -24c-494,-1708 -2376,-1362 -3077,-266l66 -1428 702 -16c-264,-141 -208,58 -225,-955 1163,5 1571,13 1766,18l-2 -279 -205 2 -11 -1473 42 -1zm2718 -139l1053 -14 21 2289 -1056 525 -18 -2800zm1342 38l1483 -9 6 2116 -1474 11 -16 -2117zm-4586 3559c667,-5 1212,532 1217,1198 5,668 -532,1212 -1199,1217 -667,5 -1212,-532 -1216,-1199 -5,-667 532,-1212 1198,-1217zm5 562c356,-3 647,284 650,640 3,357 -285,648 -641,650 -356,3 -648,-284 -650,-640 -3,-357 284,-648 641,-650zm5311 -689c482,-3 875,384 878,866 4,481 -384,875 -865,878 -482,3 -875,-384 -879,-865 -3,-482 384,-874 866,-879zm7071 8477l-935 0 -311 -997c-33,-110 -74,-210 -123,-302 -49,-91 -109,-170 -177,-236 -69,-66 -153,-117 -249,-154 -97,-36 -210,-55 -342,-55l-317 0 0 1743 -839 0 0 -4379 1729 -6c185,0 359,32 520,96 160,64 299,153 416,266 117,113 207,247 273,400 66,154 99,320 99,499 0,128 -20,252 -60,373 -39,121 -96,232 -168,334 -73,102 -159,193 -259,271 -100,79 -210,138 -330,178 138,51 250,125 338,222 87,97 156,213 207,348 26,62 49,124 71,186 21,62 44,127 67,194 24,68 49,141 77,219 26,79 56,165 90,260 40,121 78,224 112,310 35,86 72,162 112,227zm-1107 -3064c0,-102 -19,-195 -58,-280 -39,-84 -91,-157 -156,-219 -66,-62 -143,-110 -233,-145 -90,-35 -186,-52 -288,-52l-614 0 0 1365 614 0c102,0 198,-15 288,-47 90,-31 167,-76 233,-134 66,-59 118,-129 156,-211 38,-82 58,-174 58,-277zm-2729 154c0,208 -39,403 -118,584 -80,181 -186,338 -322,471 -136,133 -295,237 -478,312 -183,75 -377,112 -583,112l-725 0 0 1430 -850 0 0 -4379 1576 0c205,0 399,37 582,112 184,75 343,178 478,310 135,132 243,287 322,466 79,179 118,373 118,581zm-850 -11c0,-113 -21,-219 -63,-318 -42,-99 -100,-185 -173,-258 -73,-73 -159,-131 -258,-175 -99,-44 -205,-66 -318,-66l-565 0 0 1655 565 0c113,0 219,-23 318,-69 99,-46 185,-107 258,-184 73,-77 131,-165 173,-266 42,-101 63,-207 63,-321zm-2883 729c0,303 -51,587 -154,852 -102,265 -245,497 -427,696 -183,199 -401,356 -655,471 -254,115 -531,173 -830,173l-1831 0 0 -4384 1831 0c300,0 576,58 830,175 254,117 472,274 655,471 183,197 325,429 427,696 102,267 154,550 154,850zm-861 -22c4,-205 -27,-398 -93,-581 -66,-183 -161,-342 -285,-479 -124,-137 -277,-245 -458,-323 -181,-79 -384,-118 -611,-118l-740 0 0 2992 740 0c223,0 423,-37 600,-112 177,-75 328,-179 452,-312 124,-134 220,-291 288,-471 68,-181 103,-379 107,-595zm-3480 1425c-219,267 -485,479 -797,636 -312,157 -657,236 -1033,236 -325,0 -630,-59 -915,-178 -285,-119 -532,-281 -744,-485 -211,-204 -377,-445 -499,-720 -121,-276 -183,-573 -183,-891 0,-318 61,-615 183,-891 122,-276 288,-517 499,-723 211,-207 459,-368 744,-485 285,-117 590,-175 915,-175 376,0 721,79 1033,236 312,157 573,362 781,614l-586 603c-135,-194 -310,-350 -523,-468 -214,-119 -449,-178 -704,-178 -205,0 -397,39 -578,118 -181,79 -339,185 -474,318 -135,134 -242,289 -321,466 -79,177 -118,366 -118,567 0,197 39,384 118,562 79,177 185,333 321,466 135,133 293,239 474,318 181,79 374,118 578,118 270,0 510,-61 718,-184 208,-122 382,-284 521,-485l592 608z";
	}
	public class IconeGeradorArquivoSintegra : IIcone
	{
		public string Data => "M30.549969,23.535 L40.718975,23.535057 40.719063,38.330635 45.077999,38.330658 35.852745,47.028999 26.627001,38.330559 30.550057,38.330578 z M7.6442414,14.271585 C7.6442419,14.271585 6.8754412,16.806414 4.8020688,19.413048 2.7288085,22.019711 1.5760001,23.250641 1.5759999,23.250641 1.5760001,23.250641 4.4942795,21.584787 4.9550639,21.295172 5.4158522,21.005558 5.4158522,21.005558 5.4158522,21.005558 5.4158522,21.005558 2.3413213,27.257364 5.8388134,30.517473 7.0889126,31.682724 8.563367,32.158253 10.117462,32.170834 14.779755,32.20858 20.158835,28.07984 22.347544,25.907831 25.265711,23.011818 25.2656,23.011818 25.2656,23.011818 25.2656,23.011818 19.545447,27.448965 15.936155,27.955776 12.326978,28.462566 9.173874,27.776905 8.4827491,25.098093 7.7916256,22.419302 8.4105722,20.136555 8.4105722,20.136555 8.4105722,20.136555 9.9463779,20.570951 11.559075,20.932957 13.171661,21.294962 13.862447,21.367907 13.862447,21.367907 13.862447,21.367907 10.254282,19.485016 8.9489242,16.877718 7.6434561,14.270446 7.6442419,14.271585 7.6442414,14.271585 z M23.01615,3.9972572 C18.353869,3.959518 12.974787,8.0882597 10.78613,10.260262 7.8679172,13.156265 7.868045,13.156265 7.868045,13.156265 7.868045,13.156265 13.588225,8.71912 17.197453,8.2123203 20.806681,7.7055178 23.95978,8.3911886 24.650911,11.069991 25.342039,13.748794 24.723082,16.031527 24.723082,16.031527 24.723082,16.031527 23.187239,15.597126 21.574602,15.235125 19.961971,14.873127 19.271145,14.80019 19.271145,14.80019 19.271145,14.80019 22.879379,16.683084 24.184765,19.290373 25.490152,21.897663 25.489435,21.896503 25.489435,21.896503 25.489435,21.896503 26.258242,19.361687 28.331564,16.755033 30.404886,14.148376 31.557636,12.917456 31.557636,12.917456 31.557636,12.917456 28.639366,14.58331 28.178595,14.872917 27.717825,15.162527 27.717825,15.162527 27.717825,15.162527 27.717825,15.162527 30.792286,8.9107351 27.294788,5.6506085 26.044695,4.4853573 24.570246,4.0098362 23.01615,3.9972572 z M0,0 L27.911787,0.00075544807 38.217587,9.7175083 38.217945,21.340893 38.217999,21.403343 28.359045,21.405283 28.359104,36.316132 20.925017,36.401093 28.183273,43.341 0.0013286916,43.338665 z";
	}
	public class IconeGeradorArquivoSpedFiscal : IIcone
	{
		public string Data => "M26.884001,29.162186 L26.054079,29.298742 C24.208927,29.588623 22.228932,29.817907 20.163925,29.970209 17.572548,30.161333 15.092621,30.213127 12.826925,30.143137 L12.219018,30.120022 12.338756,30.179586 12.342476,30.181484 12.346161,30.18327 12.519877,30.269684 C14.507277,31.229424 16.759487,31.770897 19.145836,31.770897 20.397883,31.770897 21.613005,31.62184 22.77233,31.341524 L22.988346,31.286928 23.043902,31.273026 23.053978,31.270222 23.087113,31.261833 C24.262251,30.950884 25.377228,30.504333 26.411858,29.941217 L26.884001,29.671923 z M6.6769298,25.129141 L6.9557451,25.548851 C7.0596879,25.698713 7.1666544,25.846542 7.2765649,25.992262 L7.3317259,26.063568 7.4659389,26.238304 7.8744891,26.732115 7.8142684,26.69581 C7.123649,26.263893 6.7270129,25.775297 6.6809467,25.239923 6.6781824,25.207817 6.6766942,25.175692 6.6764673,25.143551 z M30.546969,23.535 L40.715973,23.535057 40.716061,38.330635 45.075001,38.330658 35.849743,47.028999 26.624001,38.330559 30.547057,38.330578 z M5.6808818,23.103155 L5.4127944,23.200823 C2.5983997,24.245392 0.94286657,25.464081 1.04551,26.656958 1.2069658,28.533312 5.6745899,29.835102 12.012286,30.135914 L12.243133,30.145159 12.232429,30.139729 12.129141,30.084215 12.087149,30.062351 12.021098,30.026146 11.925984,29.975027 11.875695,29.946451 11.811035,29.911009 11.648353,29.817247 11.624145,29.803493 11.615932,29.798569 11.538102,29.753712 C10.288979,29.015842 9.1447318,28.089714 8.1562827,26.988487 L8.072593,26.892942 8.0972799,26.906469 C10.990696,28.430786 17.881491,29.152199 25.817593,28.566885 L26.884001,28.480658 26.884001,23.992275 26.551052,24.036873 C25.247368,24.205284 23.889355,24.343538 22.49244,24.446566 16.499874,24.888538 11.103319,24.58544 7.5737742,23.754194 L7.4994968,23.736141 7.4508889,23.777744 C7.0510376,24.133553 6.792188,24.494741 6.6915968,24.855639 L6.6614788,24.995197 6.4011987,24.559965 C6.2181972,24.239561 6.0479015,23.911541 5.8909643,23.576532 z M33.656384,16.946024 L33.463051,16.986038 C30.928271,17.500679 28.039786,17.898596 24.962525,18.125553 19.455841,18.531689 14.452441,18.30868 10.938541,17.624844 L10.71488,17.579893 10.549422,17.682253 C9.4833873,18.360647 8.9167712,19.071096 8.9770549,19.771683 9.0392831,20.494869 9.7628797,21.13052 11.00151,21.653662 L11.193988,21.732178 11.265306,21.717854 11.807994,21.615986 11.554692,21.700872 11.322548,21.781662 11.43326,21.823841 C14.529932,22.964708 20.246023,23.500132 26.820322,23.129534 L26.884001,23.125389 26.884001,21.389999 26.869,21.389999 26.869,19.913 33.565686,19.913 33.583401,19.796312 33.593594,19.709846 33.600415,19.660816 33.627335,19.423532 33.634228,19.365046 33.634964,19.356264 33.637638,19.332706 C33.709263,18.62067 33.722195,17.901587 33.674523,17.18219 z M28.303391,11.88444 L28.303391,14.856869 28.998982,14.856869 C29.259173,14.856869 29.44697,14.842584 29.562378,14.814013 29.713459,14.777278 29.838833,14.715034 29.938504,14.62728 30.038172,14.539524 30.11948,14.395138 30.182429,14.194119 30.245381,13.9931 30.276854,13.719122 30.276854,13.372186 30.276854,13.025249 30.245381,12.758924 30.182429,12.573212 30.11948,12.387499 30.031351,12.242601 29.918043,12.138521 29.804734,12.034439 29.661,11.964032 29.48684,11.927298 29.356745,11.898726 29.101797,11.884441 28.722004,11.88444 z M19.708262,11.88444 L19.708262,13.157902 20.218153,13.157902 C20.585356,13.157902 20.830859,13.134432 20.954661,13.087494 21.078461,13.040556 21.175507,12.967087 21.2458,12.867086 21.316094,12.767088 21.351242,12.650762 21.351242,12.518109 21.351242,12.354846 21.301932,12.220153 21.203312,12.114031 21.104691,12.007909 20.97984,11.941584 20.828762,11.915052 20.717551,11.894645 20.49408,11.884441 20.158348,11.88444 z M27.371739,11.125262 L29.074522,11.125262 C29.458512,11.125262 29.751229,11.153833 29.952664,11.210976 30.223349,11.288527 30.455212,11.426281 30.648259,11.624238 30.841299,11.822197 30.988182,12.064542 31.088902,12.351274 31.189623,12.638006 31.239983,12.991575 31.239983,13.411981 31.239983,13.781367 31.19277,14.099732 31.098345,14.367077 30.982939,14.693604 30.81822,14.95789 30.604191,15.159928 30.442622,15.312988 30.224398,15.432375 29.949519,15.51809 29.743882,15.581354 29.469004,15.612987 29.124882,15.612987 L27.371739,15.612987 z M23.075749,11.125262 L26.49705,11.125262 26.49705,11.88444 24.007399,11.88444 24.007399,12.879332 26.323939,12.879332 26.323939,13.635449 24.007399,13.635449 24.007399,14.856869 26.585179,14.856869 26.585179,15.612987 23.075749,15.612987 z M18.776612,11.125262 L20.27166,11.125262 C20.838204,11.125262 21.207508,11.147712 21.37957,11.192609 21.643956,11.259955 21.865328,11.406383 22.043686,11.631891 22.222042,11.8574 22.311219,12.148725 22.31122,12.505864 22.311219,12.781373 22.259812,13.013004 22.156994,13.200759 22.054177,13.388513 21.923558,13.53596 21.765135,13.643103 21.606713,13.750245 21.445666,13.821162 21.281996,13.855857 21.059576,13.898713 20.737484,13.92014 20.315724,13.92014 L19.708262,13.92014 19.708262,15.612987 18.776612,15.612987 z M16.092127,11.048733 C16.671261,11.048733 17.107186,11.1722 17.399901,11.419138 17.692616,11.666076 17.846316,11.995665 17.861004,12.407906 L16.929354,12.447701 C16.889487,12.217092 16.803978,12.051276 16.672835,11.950256 16.541689,11.849236 16.344973,11.798727 16.082684,11.798727 15.812002,11.798727 15.600073,11.852808 15.446896,11.96097 15.348275,12.030358 15.298965,12.123215 15.298965,12.23954 15.298965,12.345662 15.345128,12.436478 15.437454,12.511988 15.554959,12.607905 15.840329,12.707905 16.293564,12.811985 16.7468,12.916067 17.082005,13.023719 17.299181,13.134943 17.516358,13.246167 17.686322,13.398206 17.809073,13.591062 17.931824,13.783917 17.993199,14.022182 17.993199,14.305853 17.993199,14.562994 17.919758,14.803808 17.772875,15.028297 17.625992,15.252786 17.418261,15.419621 17.149676,15.528803 16.881092,15.637986 16.546412,15.692578 16.145632,15.692578 15.562303,15.692578 15.114312,15.561457 14.801664,15.299212 14.489015,15.036971 14.302265,14.65483 14.241414,14.152792 L15.147887,14.067079 C15.202442,14.362996 15.313129,14.58034 15.479945,14.719115 15.646759,14.857891 15.871805,14.927277 16.155075,14.927277 16.455136,14.927277 16.681227,14.865542 16.833355,14.742074 16.985483,14.618607 17.061549,14.474219 17.061549,14.308913 17.061549,14.202793 17.029549,14.112487 16.96555,14.037998 16.901551,13.963509 16.789816,13.898713 16.630343,13.843611 16.521231,13.806876 16.272581,13.741571 15.884394,13.647694 15.384995,13.527287 15.034578,13.379328 14.833139,13.20382 14.549867,12.956883 14.408229,12.655864 14.408229,12.300764 14.408229,12.072194 14.474853,11.85842 14.608095,11.659442 14.741338,11.460464 14.933333,11.308934 15.184082,11.204854 15.434829,11.100772 15.737511,11.048733 16.092127,11.048733 z M19.106749,4.3240209 C18.052067,4.3257647 16.991486,4.4357295 15.94397,4.6587095 L15.868515,4.675703 15.85889,4.6776772 15.819448,4.6867523 15.59531,4.7372279 15.540912,4.7508349 15.50844,4.7583051 15.378152,4.7915459 15.247773,4.8241572 15.215708,4.8329911 15.161363,4.8468552 14.94044,4.9088159 14.901514,4.9195375 14.89214,4.9223614 14.817773,4.9432192 C13.450539,5.3441572 12.169733,5.9300461 11.008276,6.6698451 L10.929575,6.7212825 10.920006,6.7272825 10.889838,6.7472548 10.720417,6.8579836 10.664751,6.8962603 10.617414,6.9275994 10.50249,7.0078378 10.437645,7.0524259 10.409877,7.0724998 10.321693,7.134069 C8.8603357,8.1815577 7.658203,9.454299 6.7388266,10.869481 L6.7024921,10.926962 6.6984409,10.933081 6.6757382,10.969287 6.5587285,11.154392 6.5395339,11.186498 6.5196403,11.218223 6.4081538,11.40625 6.3862127,11.442953 6.3826102,11.449333 6.3479861,11.50773 C5.5077646,12.965997 4.9397596,14.58392 4.7082713,16.300875 L4.6958068,16.403681 4.6912559,16.436373 4.6827253,16.511566 4.6664799,16.645567 4.6613596,16.699915 4.6540373,16.764479 4.6370466,16.958067 4.633776,16.992794 4.6330465,17.003643 4.6251529,17.093588 C4.5757468,17.752707 4.5766032,18.41695 4.6292289,19.081017 L4.6342095,19.131428 4.5877784,19.164238 C3.7438033,19.781147 3.3044136,20.420567 3.3588098,21.052736 3.4254983,21.827751 4.2268403,22.504744 5.5911706,23.053665 L5.6730526,23.085512 5.6656607,23.068859 C5.5222462,22.72702 5.3925179,22.378504 5.2771276,22.023939 L5.2549537,21.952715 5.2519592,21.943758 5.2405875,21.906572 5.1747063,21.694954 5.1599568,21.642904 5.150574,21.612219 5.1158984,21.487413 5.0805429,21.362638 5.072594,21.331549 5.058122,21.279467 5.0044101,21.064882 4.9947537,21.027107 4.9926475,21.017889 4.9745672,20.945648 C4.855882,20.444199 4.7670749,19.939663 4.7075141,19.434307 L4.6867951,19.224123 4.7336128,19.191599 C5.6947053,18.545412 7.1106084,17.926075 8.8726981,17.36931 L9.2929786,17.240023 9.2228177,17.220686 C7.1351865,16.626869 5.8764475,15.818237 5.7934764,14.853992 5.6344493,13.005853 9.8380206,11.089063 16.063902,9.8731995 L16.606592,9.7713289 16.353289,9.8562193 C13.214112,10.928521 11.339969,12.205184 11.447138,13.45067 11.668394,16.021994 20.25067,17.486721 30.616188,16.722233 31.587958,16.650558 32.540894,16.56184 33.469818,16.457781 L33.56432,16.446581 33.569737,16.492214 C33.579609,16.583483 33.588536,16.674767 33.596505,16.76606 L33.599137,16.800186 33.80243,16.757608 C35.374554,16.421972 36.802464,16.040031 38.044758,15.625598 L38.098992,15.607159 37.81921,15.660266 C36.710102,15.86242 35.543038,16.04195 34.330284,16.194759 L33.58244,16.283997 33.546936,16.036398 C33.494149,15.699916 33.427826,15.364154 33.34776,15.029802 L33.329739,14.958674 33.327641,14.949589 33.318005,14.912349 33.264477,14.701077 33.250046,14.6498 33.242123,14.619175 33.206848,14.496281 33.172276,14.373411 33.162911,14.343203 33.148205,14.291945 33.082451,14.083547 33.07111,14.046952 33.068127,14.038141 33.045998,13.967999 C32.620739,12.678944 31.999325,11.471371 31.214657,10.376324 L31.160092,10.302109 31.153738,10.293099 31.132588,10.2647 31.01511,10.104924 30.974495,10.05242 30.941271,10.007809 30.856207,9.8995028 30.808874,9.8383217 30.787569,9.8121243 30.722281,9.7289963 C29.611267,8.3511982 28.261338,7.2177992 26.760329,6.3509917 L26.699368,6.3167362 26.692875,6.3129158 26.654455,6.2915006 26.45814,6.181191 26.424092,6.1630955 26.39044,6.1443381 26.190901,6.0391707 26.152079,6.018539 26.145336,6.0151534 26.083374,5.9824991 C24.536669,5.19032 22.820626,4.6547923 20.999546,4.4365396 L20.890505,4.4247885 20.855831,4.4204974 20.776064,4.4124546 20.63395,4.3971372 20.576327,4.392314 20.507826,4.3854055 20.302424,4.3693814 20.265663,4.3663039 20.254183,4.3656168 20.158758,4.3581719 C19.809212,4.3348832 19.458308,4.3234396 19.106749,4.3240209 z M0,0 L27.911787,0.00075544807 38.217587,9.7175083 38.217945,21.340893 38.217999,21.403343 36.508675,21.389999 28.361,21.389999 28.361,30.695 28.359083,30.695 28.359104,36.316132 20.925017,36.401093 28.183274,43.341 0.0013286916,43.338665 z";
	}
	public class IconeGeradorArquivoTerminalConsulta : IIcone
	{
		public string Data => "M33.879,27.013999 L44.755002,27.013999 44.755002,43.939996 49.006,43.939996 39.434185,53.724998 29.628,43.939996 33.879,43.939996 z M22.739654,11.099 L22.739654,12.931531 22.636411,12.943579 C22.214536,12.995024 21.857076,13.06058 21.617031,13.119973 20.899484,13.297514 19.078916,13.847696 18.544559,15.65118 18.010202,17.454666 18.67815,18.688631 19.446287,19.542913 20.214426,20.397195 22.886211,21.346397 24.188706,21.757719 25.491203,22.16904 25.958767,22.928402 25.825178,23.845966 25.691589,24.763528 24.021721,24.92173 22.218265,24.795168 20.41481,24.668607 18.544641,23.719406 18.544641,23.719406 L18.644708,26.313892 C18.644706,26.313892 20.278095,26.910221 21.78429,27.105166 22.028057,27.136715 22.270746,27.159778 22.511127,27.173967 L22.739654,27.184667 22.739654,29.33 24.508381,29.33 24.508381,27.064314 24.578928,27.051586 C24.866167,26.994852 25.142725,26.920269 25.405677,26.826917 26.418148,26.467481 27.361618,26.124681 28.163149,25.048864 28.964678,23.973047 28.697206,22.043171 27.895708,21.062246 27.09421,20.081321 25.25744,19.448326 23.487463,18.847129 21.717487,18.245931 21.176517,17.858904 21.132692,16.903947 21.079876,15.75308 21.984451,15.23997 24.05506,15.23997 26.125673,15.23997 27.69548,16.094535 27.69548,16.094535 L27.783668,13.741519 C27.783668,13.741519 27.227848,13.372851 25.791746,13.024804 25.43272,12.937792 25.040296,12.888356 24.641613,12.866114 L24.508381,12.85981 24.508381,11.099 z M23.326284,9.1509834 C29.43161,9.1509834 34.380954,14.100228 34.380954,20.205428 34.380954,21.540941 34.144119,22.82114 33.710159,24.006317 L33.539161,24.439998 30.915001,24.439998 30.915001,28.239586 30.759195,28.388128 C28.796028,30.172392 26.188156,31.259872 23.326284,31.259873 17.22096,31.259872 12.271616,26.310631 12.271616,20.205428 12.271616,14.100228 17.22096,9.1509834 23.326284,9.1509834 z M0.1,0.1 L33.128697,0.1 42.875,11.34303 42.829883,24.439998 36.693348,24.439998 36.714748,24.37408 C37.124347,23.057205 37.345001,21.657085 37.345001,20.205428 37.345001,12.463264 31.068609,6.1869999 23.326284,6.1869999 15.583962,6.1869999 9.3075691,12.463264 9.3075691,20.205428 9.3075691,24.076509 10.876667,27.581117 13.413557,30.117954 L13.43894,30.142155 6.1809994,42.713021 11.74963,45.928001 18.916263,33.515282 19.157549,33.593617 C20.474451,34.003208 21.874599,34.223858 23.326284,34.223858 25.987709,34.223858 28.475912,33.482227 30.595499,32.194363 L30.915001,31.989539 30.915001,41.878471 24.024,41.878471 32.791073,50.626999 0.1,50.626999 z";
	}
	public class IconeGeradorArquivoEtiquetas : IIcone
	{
		public string Data => "M0 0l3106608 0 908800 1068286 0 1306331 -1042501 0 0 1622095 -756313 0 810459 810549 -3026134 0 -919 -4807262zm3199153 2600876l1042514 0 0 1622095 436293 0 -957602 957602 -957602 -957602 436397 0 0 -1622095zm-1664583 844059c564146,-564133 1128305,-1128292 1692451,-1692439 34687,-34674 46357,-83618 35049,-127930l0 -26 -83282 -804404c-3260,-80319 -7698,-162462 -68325,-223102 -57639,-57626 -125576,-67730 -211432,-70292l-811520 -80073 -13 0c-45464,-13326 -96621,-2070 -132497,33794 -564146,564159 -1128292,1128305 -1692451,1692451l165955 165955c318209,-318209 338081,-338069 503532,-503532l222494 13 81613 -81625 0 -63668c-135577,-30145 -108963,-230981 25527,-228277 121073,2432 156769,157532 58208,215262l-13 111383 -130647 130647 -222494 0c-159667,159667 -319334,319334 -479014,479002l126688 126688 388151 -388125 177845 0 482896 -482909c-50833,-115911 97306,-218548 187536,-128331 89634,89647 -11955,238783 -128344,187561l-507387 507400 -177729 -155 -363763 363763 202738 202738c185362,-185375 370724,-370737 556099,-556099 -51157,-116273 97876,-218017 187561,-128344 90229,90242 -12459,238382 -128331,187536l-556112 556112 200564 200577 367011 -363543 -1216 -175775 507387 -507387c-51208,-116416 97927,-217991 187548,-128344 90242,90229 -12420,238369 -128319,187523l-482896 482909 0 177845 -388151 388138 126701 126701c159693,-159667 319334,-319347 479014,-479014l0 -222507 130634 -130647 111383 0c57703,-98497 212829,-62995 215262,58337 2678,134244 -198106,161026 -228277,25384l-63681 0 -81613 81625 13 222481 -503532 503545 151180 151180zm35553 -1260932c26,-41997 -63732,-42010 -63732,0 0,41984 63758,42022 63732,0zm-276833 -606699c0,-41997 -63732,-42010 -63732,13 0,42022 63732,41984 63732,-13zm507737 29680c0,-41673 -63875,-41828 -63875,155 0,41078 62865,43174 63875,-155zm343929 821986c-42010,26 -42022,63745 -13,63719 42048,0 41997,-63719 13,-63719zm-29511 -507737c-62400,1643 -20326,97009 22202,54482 20326,-20326 5162,-54469 -22202,-54482z";
	}
	public class IconeGerenciadorBoleto : IIcone
	{
		public string Data => "M35.951999,39.587 L35.951999,45.731999 37.751999,45.731999 37.751999,39.587 z M34.031997,39.587 L34.031997,45.731999 34.831997,45.731999 34.831997,39.587 z M32.514,39.587 L32.514,45.731999 33.313999,45.731999 33.313999,39.587 z M30.747999,39.587 L30.747999,45.731999 32.001999,45.731999 32.001999,39.587 z M29.045,39.587 L29.045,45.731999 30.299,45.731999 30.299,39.587 z M26.472999,39.587 L26.472999,45.731999 28.272999,45.731999 28.272999,39.587 z M24.583999,39.587 L24.583999,45.731999 25.384001,45.731999 25.384001,39.587 z M23.004999,39.587 L23.004999,45.731999 23.805,45.731999 23.805,39.587 z M20.804998,39.587 L20.804998,45.731999 22.059,45.731999 22.059,39.587 z M18.140999,39.587 L18.140999,45.731999 19.394998,45.731999 19.394998,39.587 z M16.314001,39.587 L16.314001,45.731999 17.114,45.731999 17.114,39.587 z M14.115,39.587 L14.115,45.731999 15.369,45.731999 15.369,39.587 z M11.73,39.587 L11.73,45.731999 13.03,45.731999 13.03,39.587 z M9.871,39.587 L9.871,45.731999 11.171,45.731999 11.171,39.587 z M8.2739999,28.487 L39.432998,28.487 39.432998,47.384999 8.2739999,47.384999 z M41.297001,16.204 C39.971515,16.204 38.896999,17.278516 38.896999,18.604 38.896999,19.929484 39.971515,21.004 41.297001,21.004 42.622482,21.004 43.696998,19.929484 43.696998,18.604 43.696998,17.278516 42.622482,16.204 41.297001,16.204 z M0.1,11.767001 L47.383999,11.767001 47.383999,33.415 41.142002,33.415 41.142002,29.996001 43.297001,29.996001 43.297001,26.406001 4.1129997,26.406001 4.1129997,29.996001 6.2679994,29.996001 6.2679994,33.415 0.1,33.415 z M8.2739999,0.1 L39.209999,0.1 39.209999,9.7840006 8.2739999,9.7840006 z";
	}
	public class IconeGestaoContrato : IIcone
	{
		public string Data => "M8657043 118l31787701 0 12165487 12165487 0 1728880c3226676,-1591482 6311229,-3097430 6689281,-3224904l2000487 -998176 -21974 1644528c-1027357,483907 -3145277,1519770 -5506804,2682754l-145432 71712 -205920 101247 -80572 39459c-915477,449646 -1900067,934971 -2879105,1418878l-54227 27173 -95340 47020 -62260 30953 -87779 42767 -69467 34852 -79745 39341 -298307 147559 -92032 45721 -56826 27999 -99121 49147 -49147 24219 -106445 52927 -41113 19966 -267826 133027 -25873 12759 -127593 63087 -18548 8979 -283539 140706 -2599 1181 -3426 1772 -143660 71121 -3781 2127 -139052 68758 -10987 5435 -131019 64860 -140706 69940 -24928 12759 -114833 56353 -31898 16067 -106800 52573 -38514 19493 -98766 48792 -45366 22801 -91087 45012 -51746 25755 -83053 41113 -58007 28826 -75492 37333 -64033 32253 -68168 33434 -69940 34734 -60607 30126 -75847 37687 -53400 26346 -81399 40641 -46193 22919 -86952 43240 -38987 19021 -91914 45839 -32607 16067 -96640 47847 -25873 12759 -101365 50446 -19021 9688 -105973 52573 -13114 6380 -109753 54699 -6852 3308 -114006 56826 -827 0 -112707 55999 -4726 2481 -105855 52573 -10278 5553 -98294 48320 -15713 8034 -90614 44894 -20320 10160 -83998 41586 -24928 12641 -76319 37805 -29653 14768 -69940 34379 -33552 16894 -63087 31426 -37805 19021 -56353 27999 -41468 20320 -50446 25046 -44539 22447 -91914 45721 -38987 19139 -50092 25046 -33434 16422 -52573 26346 -28354 13941 -42413 21265 -35206 17367 -66514 33434 -8506 3781 -44421 22092 -27645 13586 -68995 34261 -7679 4253 -37333 18312 -20675 10160 -62733 31307 -20438 10160 -24101 11932 -15240 7561 -55999 27999 -29181 14413 -13586 6734 -10160 4726 -49619 24573 -33907 16894 -5907 3426 -5907 2599 -42413 21147 -35206 17367 -1300 827 -2481 1300 -35206 17367 -31780 15713c-190326,95340 -701405,325952 -826517,409005 -666672,439958 133500,150866 -366120,546758 -219507,173668 -1232096,626385 -1505475,743345l233093 737084c-1220636,-12759 -5906004,124994 -6855388,-276805 -1605423,-677659 -2250947,-627212 -3549557,-1055357 -656511,-215726 -1114309,-417039 -1859308,-486152 -1486809,-138580 -2604899,1207168 -3212736,1273209 -334340,497966 -1738095,900710 -2135996,2489593 -158191,630284 -177685,1399502 -319218,1939881 -170832,654385 -555619,1113837 -15240,1707969 906617,999830 2625573,-74075 3126965,-645879 350171,-398373 551838,-952810 883343,-1367250 205920,-257666 492413,-233920 795091,-332686 328906,-108099 634419,-329378 853571,-371318 709084,-133500 2711226,866685 3422437,1111237 1906329,655684 4056501,2082714 6014930,3194896 1394068,790838 1822921,1058665 2256027,2309427 17839,-47020 -37215,-357732 682857,-486506 372972,-66632 2868826,-22565 3360531,-21265l1742349 4444241 1759715 -763666 464177 1313023 9237708 -4544779 1495315 -735784 0 735784 0 926937 -835377 410659 -7822257 3848454 0 22182692 -22449809 0 -1026058 0 -11054722 0 -2522200 0 -6900400 0 0 -17485037 0 -26700 0 -4849820 -344973 -167879 -7465352 -3630484 -846719 -411604 0 -932017 0 -709911 8657043 4193427 0 -473 303505 147559 480245 -1205396 1802128 777252 1319757 -3107472 736611 3426c1707615,7207 4340395,826399 5344951,2330574 295353,276805 604293,595078 906144,837977 405579,326306 1136283,689591 1591482,951510 1023104,589053 2111068,1103204 3181192,1600697 1098124,510725 2210661,986717 3312211,1489408 837977,381833 2687479,990851 3496629,1547770 675532,309058 959071,872238 1734315,756105 856525,-128420 649305,-738265 1101550,-1238358l97467 -107272 3341037 -2168248 271253 -25519c359386,-33907 997704,11460 1379536,-170360l-306459 -3137125 1207168 -13586c627685,-7207 1226071,184300 1629524,-353951 247152,-330087 -27054,-1546470 -127947,-1905502 -281058,-1001484 -853571,-1144317 -1736914,-1645709 -1818550,-1033264 -3864403,-2434893 -5828031,-3110071 -587399,-202612 -2253901,-1004910 -2856185,-1060437 -235692,99593 -448346,237819 -715818,325125 -120859,39341 -244198,60607 -362340,88960 -286965,403453 -462051,872238 -827816,1289277 -1106630,1261395 -3460124,2114966 -4779881,659465 -923511,-1017551 -512025,-1673708 -234392,-2732846 158073,-603112 155947,-1288804 315319,-1927476 108099,-431098 275506,-820610 499738,-1174089 -1767395,1246982 -3155910,3216044 -5590330,2204399 -856997,-356432 -1748728,-898937 -2616358,-1174916l-962498 -305159 183592 -839158c-539552,-248333 -1083356,-545931 -1347402,-1011290 -1624089,-776425 -3235891,-1614283 -4844386,-2424260l-50446 -25400 -50446 -25519 -50446 -25400 -50446 -25400 -50328 -25400 -50446 -25046 -50446 -25400 -50446 -25400 -50446 -25519 -50446 -25400 -50446 -25400 -50446 -25400 -50446 -25519 -50446 -24928 -50446 -25519 -50328 -25400 -50446 -25400 -50446 -25400 -50446 -25519 -50446 -24928 -50446 -25519 -39459 -19848c-710738,-356905 -1422304,-711565 -2133042,-1060909l-8152 -3781 -1654 -827 -13114 -6380 -6380 -2954 -14768 -7679 -13586 -6261 -14413 -6852 -23746 -11460 -9806 -5080 -36860 -17367 -2481 -1654 -45012 -21620 -7561 -3426c-921384,-446692 -4736287,-2289461 -4831272,-2337427l0 9806 -1495197 -720898 0 -1669573 2143675 1037163 3115978 1507602 1912708 925165 6025 5907c474219,232738 972658,480717 1478657,733303l0 -16210174zm4728725 44270634l34496328 0 0 1495197 -34496328 0 0 -1495197zm0 9046201l13144642 0 0 1495669 -13144642 0 0 -1495669zm0 -3047338l12970384 0 0 1495669 -12970384 0 0 -1495669zm0 -3077464l34496328 0 0 1495669 -34496328 0 0 -1495669zm17197245 -6590161c-281767,-255894 -1789841,-847192 -2759546,-1240484l-1114664 1508783c358559,449292 842584,718418 1632595,764138 873892,50446 1729235,-700933 2241614,-1032437zm-3566924 -1585457c-818365,-371791 -1606368,-714164 -2404767,-1074496l-967696 1110056c237346,1244737 1112183,1566082 2492546,1155304l879917 -1190865zm-3214272 -1444042c-850263,-397073 -1772947,-837504 -2600291,-1281243l-820019 1098596c30953,1641928 1479130,1987728 2236534,1539736l1183776 -1357090zm-3354269 -1702535c-1932674,-1112891 -1294357,-713692 -2663260,-1955121 -566251,579011 -488751,1953467 19848,2570519 624731,517932 1249935,644697 1875021,414085l768391 -1029484zm30746285 -23704352l-10749090 0 0 -10749563 10749090 10749563zm-16133500 -1470623l-2231454 0 -317446 2228028 -1911881 -1538909 -1911881 1538909 -317918 -2228028 -2231808 0 1166764 -1963627 -1743648 -1403755 2207353 -854398 -331032 -2320414 2103861 814585 1122343 -1888135 1072723 1857536 2025415 -783986 -331032 2320414 2207353 854398 -1721556 1385916 1143845 1981467zm-1555449 43009357c119559,-423064 -50446,-693844 -338593,-833723 -191625,-93214 -434051,-141179 -680730,-144959l-827 0c-253059,-3426 -511552,37333 -731059,117432 -384432,142360 -639145,426727 -454372,816711 9333,20320 87779,47020 216199,75847 397546,88133 1084538,82226 1864389,-15240l124994 -16067zm-75374 -1259269c306813,149685 542032,373799 634065,677304 239000,-740037 681912,-1440143 1190510,-2008521 495839,-554792 1059137,-989670 1556276,-1215556l0 -473c592951,-270307 1123170,-264046 1428683,123340 243371,308585 297952,880744 41586,1778027 -223405,783632 -1135456,1381190 -2247521,1791968 -507417,186900 -1057483,337294 -1602587,448346 -39341,542978 74665,1082884 409478,1574588 52927,77146 57653,154293 47847,218680 164453,-576412 518404,-1785588 2566266,-2319233 297125,-77146 776543,12287 1276163,104673 217853,39932 441258,81872 639617,109399 -112352,-189499 -139052,-401326 -113179,-581137 30480,-213127 147086,-400499 294999,-481899l354 0c208165,-114006 434406,-86834 615044,169533 284366,404398 394947,703650 374626,921502 442439,-19493 1121516,-213600 2134814,-727751 346272,-175085 480599,41940 662064,344618l-827 0c44421,72893 94041,155474 151693,234747l473 -473c217734,293345 621305,556091 1621136,-23628 393293,-227658 610673,-188199 896338,-136571l1654 0c242426,45012 562944,103492 1295656,-158428l16185 -4253 119441 -35206 171305 458979 -103019 30126c-887478,316973 -1298610,241599 -1611448,183946 -158900,-28826 -280940,-50919 -485207,67341 -1402101,812931 -2024706,363285 -2381139,-116133l0 -1181c-68640,-94159 -126648,-189499 -176739,-272198l-827 -827c-27054,-44894 235692,-220806 95813,-150040 -1362170,690064 -2256500,845538 -2825232,747717 -376753,106800 -955291,-1300 -1505475,-103492 -431925,-79627 -846010,-157246 -1009518,-114361 -1753454,456380 -2051406,1488817 -2190813,1977568 -59307,209819 -102547,354306 -247034,431807 -251405,133618 -453072,-13114 -800644,-523839 -354778,-520413 -500092,-1082884 -493831,-1647835 -32134,59780 -67341,121213 -106327,183592l-391639 -189971c-814585,97112 -1546588,96640 -1996589,-3781 -298425,-66159 -507417,-186900 -589171,-360213 -333159,-706131 93214,-1207050 750552,-1449949 288265,-105500 624377,-158900 951156,-154293l827 0c331859,5553 664545,72066 934144,203439zm1082884 1078276c447992,-99593 895984,-227186 1310424,-379824 966396,-356432 1751682,-850972 1925940,-1462590 212773,-746771 197887,-1183776 41468,-1382136 -119086,-150866 -392820,-122513 -728578,30126l-1181 0c-431570,195760 -929891,583618 -1377056,1084183 -530218,593306 -985417,1340077 -1171017,2110241zm367892 2348414l0 0zm5132060 -2217040c-7561,-74665 -72066,-207692 -214899,-423419 -8034,91560 8979,195406 66632,284012 34734,52927 82581,100420 148267,139407z";
	}
	public class IconeAgrupamentoQuarto : IIcone
	{
		public string Data => "M129679 95780l2776 0 0 -6754 -2776 0 0 6754zm-87569 -88356l20724 0c-706,8707 -2771,16541 -6714,23169l16681 0 0 -23169 2609 0 0 23169 16684 0c-3946,-6629 -6016,-14462 -6720,-23169l20732 0 0 48799 -6430 0c-4474,-2580 -4506,-8262 -411,-16854 -2127,-1932 -3977,-4038 -5574,-6312l-18281 0 0 23166 -2609 0 0 -23166 -18282 0c-1597,2272 -3445,4380 -5574,6312 4095,8591 4064,14273 -411,16854l-6424 0 0 -48799zm53438 96494c4251,0 7732,4363 7732,6759l0 12988 -103279 0 0 -12988c0,-2405 3484,-6759 7736,-6759 29270,0 58541,0 87811,0zm-84542 -3579l0 -3698 0 -16968c0,-3185 2758,-5790 6124,-5790l69503 0c3366,0 6119,2605 6119,5790l0 16968 0 3698 -9035 0c1501,-3734 1200,-8420 -395,-12089 -9375,-1474 -18907,-1342 -28580,255 -1032,3484 -882,8283 37,11835l-6504 0c1505,-3737 1189,-8443 -397,-12089 -9373,-1474 -18905,-1342 -28580,255 -1026,3480 -884,8285 40,11835l-8332 0zm85174 47866l7099 0 0 -6156 0 -14805 -7099 0 -89080 0 -5267 0 -1832 0 0 20961 7099 0 3045 -13821 82993 0 3042 13821zm-58324 -89445l72498 0 0 4888 -72498 0 0 -4888zm0 -58760l72498 0 0 4887 -72498 0 0 -4887zm76057 148204l34296 0 0 -12839 -34296 0 0 12839zm12868 -40429l8559 0c684,0 1242,527 1242,1172l0 0c0,645 -558,1174 -1242,1174l-8559 0c-684,0 -1242,-529 -1242,-1174l0 0c0,-645 558,-1172 1242,-1172zm0 16418l8559 0c684,0 1242,529 1242,1172l0 0c0,647 -558,1172 -1242,1172l-8559 0c-684,0 -1242,-526 -1242,-1172l0 0c0,-643 558,-1172 1242,-1172zm0 16420l8559 0c684,0 1242,527 1242,1172l0 0c0,643 -558,1171 -1242,1171l-8559 0c-684,0 -1242,-527 -1242,-1171l0 0c0,-645 558,-1172 1242,-1172zm-12868 -25243l34296 0 0 -12843 -34296 0 0 12843zm0 16416l34296 0 0 -12839 -34296 0 0 12839zm11256 -31478l11796 0 0 -1477c0,-811 -703,-1476 -1563,-1476l-8670 0c-858,0 -1563,664 -1563,1476l0 1477zm-5967 -13384l10478 0 2776 0 10477 0 -4025 -14430c-389,-1395 -1250,-2624 -2776,-2624l-3676 0 -2776 0 -3677 0c-1526,0 -2385,1229 -2776,2624l-4025 14430z";
	}
	public class IconeImportacaoGuiaDaFarmacia : IIcone
	{
		public string Data => "M10.7652549549937,21.6160068541765C10.7652549549937,21.6160068541765 10.2496804997325,22.4915008574724 9.59349439591169,22.8977508574724 8.93730733841658,23.3040008574724 8.65608594864607,23.4446258574724 8.65608594864607,23.4446258574724L8.78321931809187,23.5436897307634 8.84669111222029,23.604000094533 14.4681539341807,23.604000094533 14.5461425587535,23.4130001097918C14.5461425587535,23.4130001097918 13.2493905827403,23.1789951354265 12.077629069984,22.5227355986834 10.9058675572276,21.8664760619402 10.7652549549937,21.6160068541765 10.7652549549937,21.6160068541765z M10.6427717015147,16.3070011168718C10.6427717015147,16.3070011168718 9.43652914017439,16.8040008574724 9.53028963059187,18.2102508574724 9.62404916733503,19.6165008574724 10.1839141651988,20.220455172658 11.2921170994639,21.1367435485125 12.1047143742442,21.8086185485125 14.108632068336,22.6009998351336 14.108632068336,22.6009998351336L14.1401347920299,22.5071277648211C14.1401347920299,22.5071277648211 12.5463561818004,21.5068187743425 11.5929384037852,20.5849990874529 10.6395206257701,19.6631774932146 10.6427717015147,18.631868365407 10.6427717015147,18.631868365407z M22.249124507606,15.729000094533L26.4489993855357,19.8630008727312 24.4650001332164,19.8630008727312 24.4650001332164,27.1368553684848 19.9080009266734,27.1368553684848 19.9080009266734,19.8630008727312 17.9549999043345,19.8630008727312z M11.3621110722423,13.9300012618303L11.3621110722423,19.2326450377703 11.3720941349864,19.2529068022966C11.5124807164073,19.5275650054216,11.7169990345836,19.8263473540545,12.0149421498179,20.1144351989031L12.0271167561412,20.1259994536638 12.0271167561412,14.408253672719 11.9952812001109,14.3820390731096C11.848941783607,14.2640514403582,11.6735219761729,14.135042193532,11.4632901951671,13.9957942992449z M7.26407096832991,6.58700561821461L7.27638385742903,7.12167311012745C7.27638385742903,7.12167263329029 7.27638385742903,7.12167263329029 7.45895812958479,7.28600979149342 7.64153240174055,7.45033121407032 7.9701599881053,7.92503166496754 9.12037179917097,9.05401230156422 10.2705821797252,10.1829776793718 12.4432019993663,11.5040054351091 12.4432019993663,11.5040054351091 12.4432019993663,11.5040054351091 12.8628654286265,11.4488601714373 14.6154823109508,10.1342716246843 16.3681049153209,8.81968403160572 16.1857452198863,6.58700561821461 16.1857452198863,6.58700561821461z M8.19929979294539,2.39958358108997C8.15680692642927,2.39876938164234 8.11351774185896,2.39879799187183 8.06941030472517,2.39972877800465 7.90655324906111,2.40316391289234 7.73255966156721,2.4188942939043 7.54643390625715,2.44991541206837 6.55376289337873,2.61536121666431 5.4076976582408,3.77885175049305 5.21861122101545,5.05514622032642 5.02952478379011,6.33144045174122 5.2092146679759,7.6619386702776 6.38774154633284,9.05398369133473 7.63731620758772,10.5299482375383 9.2135476872325,11.701126101613 10.9389629170299,12.6937990218401 12.6643781468272,13.6864728957415 12.7762117192149,14.2855310469866 12.7762117192149,14.2855310469866L12.7762117192149,16.9330005675554C12.7762117192149,16.9330005675554 13.3680686756969,16.4283657103777 13.5504264637828,15.8463058501482 13.7958831593394,15.0628423720598 13.7961330220103,15.1724453002214 13.7961330220103,14.2793798476458 13.7961330220103,13.0668773680925 12.1358871266246,12.0611276656389 10.8830032154918,11.25745010674 9.63011835068464,10.4537725478411 8.96342275589705,10.0231180220842 7.86159370392561,8.88840675652027 6.75976512879133,7.75369501411915 6.5685429379344,7.28087473213673 6.33213232010603,6.14628553688526 6.09572170227766,5.0116963416338 6.57783840149641,4.0555253058672 7.56210658997297,3.6180765658617 9.07327936142683,2.9464514285326 10.7936878010631,5.07040834724903 10.7936878010631,5.07040834724903 10.7936878010631,5.07040834724903 10.7214584156871,4.12093544304371 10.5773534581065,3.819626095891 10.2335996434093,3.10087633430958 9.63984392136335,2.80397081673145 9.63984487503767,2.80397057831287 9.29468820542097,2.61352873146534 8.83668897598982,2.41179085075855 8.19929979294539,2.39958358108997z M0.05,0.05L17.6056079670787,0.05 22.5797271534801,5.78099966347218 22.5860004231334,14.4198093444109 22.3136081501842,14.1520004302263 15.1701459690928,21.021101000905 18.6747054859996,21.021101000905 18.6747054859996,27.1099987059832 0.05,27.1099987059832z";
	}
	public class IconeInternacao : IIcone
	{
		public string Data => "M201443 660417l257530 0 0 -121123c-5141,-23 -10350,-780 -15582,-2180 -7504,-2019 -13976,-6862 -19483,-12186 -8399,-8147 -17326,-11199 -27493,-4567 -4659,3029 -8583,7894 -12920,10488 -19116,11543 -38141,11474 -56775,-941 -4177,-2777 -9983,-8399 -12874,-12461 -8055,-11245 -10327,-19667 -10327,-33459 0,-6104 2800,-14733 5531,-20195 4383,-8743 7252,-11405 13563,-18451l36810 -41009c1079,-1079 2180,-2157 3259,-3259 24555,-24876 65725,-23935 90096,711 7504,7596 13884,16844 21342,24142l21526 23867c2042,2433 4268,4200 6104,6885 528,757 1033,1538 1515,2295l157153 0 0 -257530 -201443 0 0 -201443 -257530 0 0 201443 -201443 0 0 257530 201443 0 0 201443zm329979 54824c0,11038 3695,22031 9386,30017 849,1147 1859,2708 2731,3832l4865 5026c1790,1423 2157,2387 5209,4636 18129,13425 40826,14022 59827,1905 6747,-4314 11704,-13517 20723,-13517 14917,0 15031,6816 25037,13402 3718,2478 6632,4521 11291,6104 14182,4682 27676,3236 40780,-3396 32633,-16569 39724,-57601 14940,-84222 -5232,-5645 -12461,-14687 -18061,-20424l-6311 -6793c-1216,-1193 -1951,-1882 -2937,-3167 -1079,-1400 -1974,-2042 -3098,-3488 -3970,-5118 -13999,-16500 -18818,-20103 -14113,-10465 -24899,-13930 -43304,-13930 -15972,0 -31830,8239 -41147,16523 -4200,3741 -3075,3121 -6563,6563 -1377,1377 -1813,2203 -3052,3534l-15008 16890c-1400,1400 -1813,2065 -3052,3488 -1193,1354 -1882,2295 -2937,3649l-15353 17005c-4131,4108 -7963,9776 -10442,15376 -2203,4934 -4704,13976 -4704,21090zm174502 -123349c0,10074 1308,20287 6770,27470 13838,18336 37980,9088 51726,-6976 9730,-11360 16753,-35685 10602,-50212 -1606,-3809 -3213,-5921 -5554,-8950 -4016,-5232 -12668,-9179 -19919,-9179 -6976,0 -12828,2524 -17257,5232 -9157,5554 -17326,14618 -21480,24509 -1813,4360 -4888,12415 -4888,18107zm-166975 37521c12989,0 23316,-8904 26552,-21755 2065,-8239 1446,-17303 -872,-25542 -1354,-4865 -3167,-9593 -5531,-13654 -6678,-11543 -21044,-24417 -34698,-24417 -38783,0 -34813,53929 -9524,75180 6472,5439 14825,10189 24073,10189zm102259 -97578c0,14205 2754,28915 12278,39311 872,918 1423,1446 2433,2249 8147,6357 16339,9157 26735,5531 2203,-734 5508,-2410 7252,-3557 5806,-3787 11360,-11383 14320,-17579 1010,-2134 2456,-5622 3029,-7734 4246,-15720 4750,-21985 459,-37360 -6288,-22467 -33092,-38347 -55582,-10717 -6288,7711 -10924,19300 -10924,29856zm-79747 -459c0,11819 4911,24486 9845,31899 2501,3764 5554,8147 9179,10969 8284,6472 10602,5921 20837,5921 4911,0 10533,-2226 13425,-4406 2914,-2203 6517,-6196 8422,-9386 7367,-12323 8170,-20264 8170,-34997 0,-19851 -16179,-43625 -35180,-43625 -7160,0 -11704,229 -17900,5095 -3878,3052 -5921,5186 -8629,9638 -2570,4200 -3993,7550 -5531,12759 -1239,4200 -2639,11061 -2639,16133zm-294064 -187169c0,8904 643,14802 3626,23201 1010,2800 2157,5209 3695,7757 1446,2387 2800,4796 4567,6953 6724,8239 18290,16959 29351,16959l3741 0c6908,0 14618,-4704 18336,-10419 7711,-11864 7527,-29512 1882,-42180 -5622,-12645 -21182,-30361 -36144,-30361 -9432,0 -15467,1698 -21916,8927 -4406,4957 -7137,12576 -7137,19162zm210141 22513c0,14595 8078,32358 24808,32358 8124,0 13494,-1262 20310,-5760 4842,-3190 8376,-6586 12140,-10969 1583,-1859 3580,-4521 4682,-6678 4796,-9432 7894,-15835 7894,-26781 0,-14618 -8514,-33230 -25312,-33230 -8101,0 -12828,1721 -19552,6013 -4498,2869 -8996,6953 -12186,11245 -4039,5485 -5990,8101 -8560,14549 -2410,6013 -4223,12737 -4223,19254zm-33344 -15972c21526,0 37016,-26689 37016,-45920 0,-9409 -1285,-18772 -6173,-26988 -7596,-12874 -21641,-23545 -37016,-16684 -6380,2846 -11543,8101 -15444,13815 -9179,13448 -11727,31187 -7825,46884 3442,13907 13632,28892 29443,28892zm-110177 -52048c0,15330 1652,24073 10166,36993 2547,3901 6242,7367 10029,10052 5324,3787 11589,5714 18129,4888 10809,-1354 19277,-9524 24004,-18910 6517,-12966 6517,-28181 2616,-41904 -4544,-16018 -17716,-31968 -35915,-30407 -8468,734 -15444,5416 -20333,12209 -5118,7045 -8698,18359 -8698,27079z";
	}
	public class IconeHospedagem : IIcone
	{
		public string Data => "M0 547750l105869 0 0 -290284 -105869 0 0 290284zm248965 -547750l11474 34571 37355 -47 -30243 21303 11589 34544 -30175 -21431 -30155 21431 11589 -34544 -30256 -21303 37348 47 11474 -34571zm-114370 39768l8764 26427 28537 -67 -23117 16288 8852 26400 -23036 -16355 -23049 16355 8852 -26400 -23123 -16288 28537 67 8784 -26427zm226138 0l8784 26427 28537 -67 -23123 16288 8852 26400 -23049 -16355 -23036 16355 8852 -26400 -23117 -16288 28537 67 8764 -26427zm97584 71959l7679 23123 24950 -74 -20204 14252 7726 23070 -20150 -14306 -20137 14306 7733 -23070 -20204 -14252 24950 74 7658 -23123zm-410971 0l7658 23123 24950 -74 -20204 14252 7733 23070 -20137 -14306 -20137 14306 7733 -23070 -20204 -14252 24930 74 7679 -23123zm85779 436023l60768 0 0 -81620 8798 0 13874 0 63391 0 4362 0 18303 0 0 81620 60768 0 0 -413573 -230264 0 0 413573zm99606 -359506l31072 0 0 31058 -31072 0 0 -31058zm52415 0l55105 0 0 31058 -55105 0 0 -31058zm-128844 0l55085 0 0 31058 -55085 0 0 -31058zm128844 52415l55105 0 0 31058 -55105 0 0 -31058zm-128844 0l55085 0 0 31058 -55085 0 0 -31058zm128844 52415l55105 0 0 31058 -55105 0 0 -31058zm-128844 0l55085 0 0 31058 -55085 0 0 -31058zm128844 52415l55105 0 0 31079 -55105 0 0 -31079zm-128844 0l55085 0 0 31079 -55085 0 0 -31079zm76429 -104831l31072 0 0 31058 -31072 0 0 -31058zm0 52415l31072 0 0 31058 -31072 0 0 -31058zm0 52415l31072 0 0 31079 -31072 0 0 -31079zm-16166 202260l63391 0 0 -58968 -63391 0 0 58968zm174087 0l105862 0 0 -290284 -105862 0 0 290284zm25395 -254675l55092 0 0 31058 -55092 0 0 -31058zm0 164494l55092 0 0 31072 -55092 0 0 -31072zm0 -52415l55092 0 0 31072 -55092 0 0 -31072zm0 -59663l55092 0 0 31079 -55092 0 0 -31079zm-390666 112078l55092 0 0 31072 -55092 0 0 -31072zm0 -52415l55092 0 0 31072 -55092 0 0 -31072zm0 -59663l55092 0 0 31079 -55092 0 0 -31079zm0 -52415l55092 0 0 31058 -55092 0 0 -31058z";
	}
	public class IconeManifestacaoDestinatarioCte : IIcone
	{
		public string Data => "M19.570341,19.85401 L19.331903,20.168139 C19.331903,20.168139 18.331982,22.075325 18.042572,23.088095 17.75313,24.100899 17.174448,26.310604 17.661196,27.888966 18.147942,29.467327 18.963552,29.835831 18.963552,29.835831 L19.897593,29.414867 C19.897593,29.414867 19.713417,29.309811 19.371393,29.059893 19.029331,28.809973 18.871447,28.415667 18.687273,27.994736 18.503098,27.573807 18.397625,26.153505 18.739719,24.351332 19.081778,22.549162 20.423622,20.260067 20.423622,20.260067 z M17.847412,19.236422 C17.847412,19.236422 17.319502,20.312981 16.609156,22.036066 15.898808,23.759148 15.659998,24.949959 15.515299,27.120253 15.370595,29.290552 16.202288,30.468866 16.860023,30.942398 17.517759,31.415932 19.118948,31.660551 20.500195,31.397478 21.881441,31.134399 24.591486,29.174492 24.591486,29.174492 L24.620453,28.100037 C24.620453,28.100037 24.265421,28.428879 23.05514,29.20495 21.844858,29.981022 20.552261,30.581121 19.96031,30.673199 19.368359,30.765274 17.780199,31.012924 16.938053,29.928432 16.01054,28.734009 16.227423,26.955927 16.503685,25.009104 16.779947,23.062281 18.726978,19.668299 18.726978,19.668299 z M29.024826,13.903059 C28.152956,13.903059 27.364326,14.191145 26.658939,14.767321 25.953554,15.343496 25.451498,16.096689 25.152769,17.026901 L31.3874,17.026901 C31.545116,16.041155 31.416265,15.274078 31.000851,14.725671 30.585438,14.177261 29.926764,13.903059 29.024826,13.903059 z M29.299465,12.466507 C30.810156,12.466507 31.903246,12.917686 32.578735,13.820043 33.254219,14.722402 33.457565,15.975288 33.188786,17.578711 L33.038685,18.474125 24.887703,18.474125 C24.718796,19.661074 24.910938,20.577312 25.464125,21.222847 26.017311,21.868376 26.872629,22.191145 28.030075,22.191145 29.330322,22.191145 30.591673,21.795496 31.814123,21.004198 L31.545338,22.607618 C30.416666,23.287859 29.018066,23.627974 27.349537,23.627974 25.718592,23.627974 24.51829,23.143824 23.748636,22.175528 22.978983,21.20723 22.741348,19.845016 23.035727,18.088888 23.31382,16.429939 24.049625,15.078137 25.243143,14.033485 26.436663,12.988832 27.788767,12.466507 29.299465,12.466507 z M36.065536,7.1522179 L36.644733,8.3457098 C36.644733,8.3457098 37.62738,9.0823994 37.688797,10.301905 37.75021,11.521407 37.662682,11.837112 37.662682,11.837112 L38.557693,12.354881 C38.557693,12.354881 38.869984,9.9069281 38.303329,8.8893862 37.443268,7.3449593 36.065536,7.1522174 36.065536,7.1522179 z M32.610683,6.7889633 C31.754698,6.7972517 30.997873,7.0267429 30.505495,7.201973 29.899492,7.4176402 29.386059,7.6642652 29.386059,7.6642647 L29.817387,8.6087666 C29.817387,8.6087666 31.654827,7.6585326 32.919239,7.7565126 34.244228,7.8591866 34.572853,8.3315992 34.819363,8.5986032 35.065868,8.8656082 35.312603,9.4921236 35.374149,10.180094 35.407143,10.548889 35.343227,10.909416 35.343227,10.909416 L36.349548,11.248158 C36.349548,11.248156 36.425446,9.2361221 35.836098,8.362443 35.261082,7.5100055 34.316124,6.9554615 33.217102,6.8219509 33.011036,6.7969193 32.808224,6.7870498 32.610683,6.7889633 z M27.624653,6.5497732 C27.624653,6.5497742 27.452553,6.7486076 27.353106,6.9023337 27.253622,7.0560951 27.199512,7.1105614 27.118122,7.2824211 27.036701,7.454246 26.66581,7.825007 26.638668,7.8431063 26.611561,7.8611674 26.49395,7.8699765 26.44875,7.8790245 26.403513,7.8880739 26.313215,8.1867542 26.249922,8.2410126 26.186592,8.2952719 26.159382,8.3222809 26.114149,8.3855877 26.068947,8.4488955 26.069118,8.8375473 26.069118,8.8375473 L25.978888,9.4618502 C25.978888,9.4618502 25.825226,10.321113 25.562933,11.06267 25.300671,11.804262 24.450426,11.849542 24.450426,11.849542 L22.089848,11.849542 C22.089848,11.849542 21.755316,13.631131 21.547266,14.128541 21.339256,14.625917 20.597643,14.82489 20.597643,14.82489 20.597643,14.82489 18.888037,14.82489 18.453918,14.82489 18.019835,14.82489 18.020109,14.336528 18.020109,14.336528 18.020109,14.336528 17.993242,14.454094 17.938961,14.562611 17.884678,14.671162 17.703875,14.80693 17.703875,14.80693 17.703875,14.80693 16.048449,14.80693 15.388223,14.788833 14.728,14.770735 14.411355,13.965663 14.411355,13.965663 14.411355,13.965663 14.402067,13.974643 14.257384,14.309345 14.112702,14.644085 13.560836,14.798056 13.560836,14.798056 L12.005919,14.770527 C12.005919,14.770527 11.779778,14.924012 11.616966,15.032531 11.454189,15.141082 11.481605,15.204355 11.382121,15.430437 11.28264,15.656523 11.427083,15.656523 11.553705,15.864541 11.680329,16.072527 11.788619,16.108688 11.897182,16.208193 12.005713,16.307661 12.01476,16.560854 12.03282,16.714619 12.050913,16.868343 12.150396,16.886442 12.36749,16.868343 12.584549,16.850246 12.485066,17.049217 12.575501,17.211992 12.665936,17.374771 12.647842,17.266253 12.738279,17.211992 12.82875,17.157734 13.063903,17.094427 13.353304,17.004009 13.642737,16.913555 13.769361,16.741728 13.769361,16.741728 13.769361,16.741728 13.769361,17.022072 13.742221,17.175833 13.71508,17.329559 13.751266,17.429029 13.796502,17.555643 13.841704,17.682257 13.868845,17.908342 13.95928,17.890244 14.049716,17.872147 14.167292,17.917356 14.483869,17.95355 14.800408,17.98971 14.755175,17.971613 15.035563,17.980663 15.315951,17.98971 15.343058,17.890244 15.623446,17.75458 15.903834,17.618952 15.903834,17.682257 16.09375,17.591805 16.283669,17.501385 16.365093,17.284349 16.500763,17.085379 16.636435,16.886442 16.835398,16.949749 17.079599,16.913555 17.323801,16.877394 17.405188,16.777925 17.495623,16.777925 17.58606,16.777925 17.558952,17.130589 17.58606,17.284349 17.613199,17.438078 17.604153,17.618952 17.631294,17.799824 17.658436,17.980663 17.649387,17.935453 17.830259,18.152489 18.011164,18.369522 18.246319,18.731272 18.363895,18.812675 18.481472,18.894047 18.571907,18.812675 18.870388,18.758417 19.168837,18.704159 19.096495,18.77648 19.15979,18.812675 19.223118,18.848837 19.26832,18.939257 19.385897,19.11108 19.503473,19.282906 19.738625,19.246746 19.811001,19.264843 19.883345,19.282906 20.028061,19.319101 20.127544,19.337164 20.227028,19.355263 20.498367,19.391424 20.742567,19.436668 20.986767,19.481878 20.950579,19.536135 21.077204,19.617506 21.203827,19.698912 21.203827,19.762218 21.240013,19.897846 21.276167,20.033514 21.37565,20.467581 21.366636,20.594196 21.35759,20.720812 21.321402,20.793167 21.303307,20.955946 21.285213,21.118721 21.366636,21.163929 21.384697,21.272448 21.402792,21.380964 21.493225,21.516628 21.493225,21.516628 21.493225,21.516628 21.610802,21.516628 21.755522,21.516628 21.900238,21.516628 21.981628,21.507578 22.189674,21.471416 22.397686,21.435223 22.551449,21.480433 22.669027,21.498529 22.786604,21.516628 22.741369,21.706514 22.750416,21.896437 22.759462,22.086363 22.804663,22.041147 22.858946,22.167732 22.913193,22.294348 22.895134,22.267237 23.166439,22.493319 23.43778,22.7194 23.32925,23.126322 23.311159,23.198679 23.293062,23.271034 23.229769,23.397654 23.175486,23.488073 23.121239,23.578491 23.103144,23.831722 23.076004,23.931189 23.048862,24.030657 23.021757,24.175369 23.021757,24.274837 23.021757,24.374306 23.103144,24.446663 23.193581,24.546131 23.284016,24.645597 23.193581,24.582325 23.184536,24.73605 23.175486,24.889782 23.238817,25.812212 23.238817,25.812212 23.238817,25.812212 24.586405,25.739855 24.631641,25.739855 24.676876,25.739855 24.694935,25.993082 24.731123,26.137764 24.767309,26.282478 24.74017,26.173927 24.812513,26.264381 24.884888,26.354799 24.884888,26.996887 24.884888,26.996887 L25.508957,26.996887 25.52702,29.330078 C25.52702,29.330078 25.626537,29.357189 25.653643,29.375288 25.680782,29.393387 25.626537,29.501904 25.599396,29.583271 25.572254,29.664675 25.445631,29.727983 25.364241,29.809355 25.282818,29.890764 25.083853,29.972136 24.95723,30.089701 24.830608,30.207266 24.514063,30.496655 24.486923,30.532818 24.459782,30.569012 24.179428,30.804144 24.070898,31.012129 23.96237,31.220119 23.772417,31.627073 23.772417,31.627073 23.772417,31.627073 23.917135,31.69038 24.088993,31.735592 24.260818,31.780802 24.14324,31.771786 24.269863,31.952625 24.396486,32.133495 24.396486,32.124447 24.532158,32.160641 24.667828,32.196804 24.631641,32.106384 24.74017,31.997868 24.848701,31.889318 24.902983,32.07019 24.984371,32.151596 25.06576,32.232964 25.138136,32.53141 25.355194,32.630878 25.572254,32.730377 25.581301,32.766541 25.680782,32.829845 25.780268,32.893154 25.798361,32.902203 25.924984,32.974525 26.051607,33.046879 26.160135,33.173496 26.232479,33.236805 26.304855,33.30011 26.268667,33.291061 26.241528,33.489998 26.214417,33.688972 26.078749,33.707039 26.051607,33.806541 26.024467,33.90601 26.214417,34.140999 26.214417,34.140999 26.214417,34.140999 26.28676,34.023468 26.359102,33.942062 26.431477,33.860695 26.540007,33.806332 26.621395,33.697811 26.702785,33.589294 26.748018,33.444862 26.802299,33.300144 26.85655,33.155468 27.01936,33.010548 27.127892,32.92918 27.23642,32.847775 27.326855,32.865803 27.444429,32.820595 27.56201,32.775383 27.580103,32.630737 27.697678,32.540318 27.815254,32.449863 27.815254,32.377472 27.91474,32.214661 28.014221,32.051888 28.077549,31.916189 28.231316,31.771513 28.385046,31.626835 28.394093,31.454937 28.439327,31.337372 28.484528,31.219805 28.602104,31.02985 28.70162,30.812817 28.801104,30.595778 28.873444,30.614016 28.954836,30.523563 29.036255,30.433144 29.054352,30.460426 29.199034,30.342859 29.343752,30.225296 29.307564,29.7733 29.307564,29.637671 29.307564,29.502007 29.244268,29.258034 29.244268,29.113356 29.244268,28.968639 29.325657,28.760899 29.361845,28.589037 29.398033,28.417212 29.461329,28.417421 29.58795,28.327003 29.714575,28.236549 29.83215,28.227501 29.967823,28.100882 30.103493,27.974266 30.112539,27.874798 30.239161,27.748184 30.365788,27.621571 30.646141,27.476648 30.673281,27.431438 30.700422,27.386227 30.872246,27.241447 31.035057,27.150993 31.197872,27.060577 31.224977,27.151096 31.351603,27.123951 31.478226,27.09684 31.559614,27.006592 31.785755,26.843815 32.011856,26.681005 32.011856,26.77129 32.138481,26.726078 32.265068,26.680834 32.27412,26.662975 32.44598,26.581572 32.61784,26.5002 32.735413,26.509251 32.907238,26.5002 33.079098,26.491152 33.169533,26.527555 33.413734,26.545654 33.657932,26.563753 33.666981,26.545654 33.865944,26.527555 34.064907,26.509495 33.992569,26.446257 34.073956,26.33774 34.15538,26.229218 34.200577,26.211193 34.281971,26.165983 34.363392,26.120773 34.571404,25.994329 34.688976,25.885813 34.806557,25.777258 34.72517,25.650642 34.761322,25.478819 34.797512,25.306993 34.842743,25.198376 34.987431,24.972258 35.132145,24.746172 35.17738,24.782366 35.29496,24.691912 35.412537,24.601494 35.611496,24.122252 35.729076,23.977573 35.846653,23.832861 35.710983,23.696987 35.710983,23.58847 35.710983,23.479919 35.765263,23.35351 35.837605,23.26309 35.909981,23.172638 35.937088,23.136339 36.009464,22.964516 36.081806,22.792692 36.117992,22.693222 36.244614,22.421892 36.371243,22.150566 36.326004,22.096237 36.380287,21.996769 36.434532,21.897303 36.552113,21.508371 36.488815,21.119478 36.425488,20.730585 36.525005,19.83544 36.49786,19.690725 36.470718,19.546013 36.570202,19.519075 36.633533,19.428619 36.696823,19.3382 36.913887,19.301935 37.112885,19.202469 37.311852,19.102964 37.212368,19.102825 37.375145,18.949099 37.537956,18.795336 37.510818,18.740801 37.63744,18.623236 37.764065,18.505671 37.953979,18.008327 38.397182,17.610422 38.840343,17.212477 39.310658,16.733232 39.437279,16.651827 39.563904,16.570457 39.591042,16.380396 39.636242,16.244732 39.681477,16.109104 39.699814,15.910132 39.735996,15.611691 39.77219,15.313251 39.727226,14.752533 39.591557,14.282269 39.455883,13.811971 38.777637,13.821019 38.660057,13.775809 38.542484,13.730599 38.243832,13.567788 37.999626,13.395964 37.755424,13.22414 37.655842,13.061362 37.465889,12.934748 37.275974,12.808132 37.04068,12.554904 36.859779,12.437336 36.678905,12.31977 36.226486,12.012283 36.081772,11.958024 35.93705,11.903764 35.684048,11.98517 35.584526,11.994185 35.485046,12.003234 35.213638,12.030379 35.10511,11.958024 34.996574,11.885666 34.607933,11.822359 34.526546,11.822359 34.445122,11.822359 34.065151,11.668633 33.884277,11.578178 33.703373,11.48776 33.639874,11.542018 33.549438,11.56913 33.458965,11.596276 33.223812,11.641486 33.169567,11.623423 33.115284,11.605325 32.970428,11.542018 32.934242,11.52392 32.898056,11.505857 32.85265,11.352096 32.85265,11.279737 32.85265,11.207382 32.662865,10.836621 32.626678,10.773314 32.590492,10.710007 32.527092,10.755217 32.400467,10.755214 32.273846,10.755217 32.165421,10.664761 32.020702,10.592439 31.875982,10.520082 31.677084,10.384419 31.586649,10.33921 31.496214,10.294001 30.890203,10.095027 30.727425,10.040769 30.564613,9.9865103 30.401838,10.085979 30.374697,10.113128 30.347557,10.140238 30.239126,10.239707 30.184879,10.33921 30.130598,10.438678 29.958912,10.53818 29.913713,10.619551 29.868475,10.700955 29.651419,10.773314 29.615231,10.791375 29.579042,10.809475 29.461466,10.67381 29.47051,10.601453 29.479557,10.529131 29.542887,10.429629 29.588087,10.312063 29.633324,10.194498 29.759981,9.7875404 29.759981,9.7875395 29.759981,9.7875404 29.669512,9.7965889 29.597137,9.8056374 29.524792,9.8146524 29.416023,9.8237009 29.189919,9.7875395 28.963812,9.751379 28.764847,9.7242327 28.656317,9.7151842 28.547787,9.7061348 28.53874,9.778491 28.520647,9.8056374 28.502554,9.8327494 28.502554,9.9232035 28.466366,10.022672 28.430212,10.122139 28.267399,10.321113 28.267399,10.321112 28.267399,10.321113 27.968952,10.303015 27.968952,10.275902 27.968952,10.248756 28.041294,10.058832 28.068436,9.9322519 28.095541,9.8056374 28.158871,9.6790228 28.231213,9.4710016 28.303589,9.2630167 28.29454,9.3715334 28.403072,9.2539682 28.511599,9.1364021 28.502554,9.1273537 28.611084,8.9645777 28.71961,8.8017664 28.674343,8.7113466 28.728624,8.5756836 28.782873,8.4400196 28.710566,8.3767109 28.710566,8.3767109 28.710566,8.3767109 28.538809,8.2681942 28.439327,8.1867886 28.339844,8.1054182 28.285425,7.9878526 28.213083,7.8612366 28.140707,7.7346215 28.140501,7.454246 28.140501,7.2914677 28.140501,7.1286936 28.113327,6.8754644 28.113327,6.8754635 z M20.231823,6.1329994 L20.020702,7.1999884 20.021301,7.1999884 19.665842,8.9964542 19.656143,8.9964542 19.445026,10.063442 19.454723,10.063442 18.864254,13.047635 20.108284,13.047635 20.698753,10.063442 24.261639,10.063442 24.472757,8.9964542 20.909872,8.9964542 21.265329,7.1999884 24.837318,7.1999884 25.048437,6.1329994 21.47645,6.1329994 20.23242,6.1329994 z M13.148003,6.1329994 L11.779789,13.047635 13.023863,13.047635 13.984522,8.1926832 15.917235,13.047635 16.019186,13.047635 17.161308,13.047635 17.26326,13.047635 18.631472,6.1329994 17.3874,6.1329994 16.398394,11.131203 14.408652,6.1329994 14.392074,6.1329994 13.164581,6.1329994 z M16.519157,1.0677059 C19.716774,1.0655137 23.116938,2.0037482 28.217184,3.6171548 35.635727,5.9639359 42.941868,2.4947336 42.941868,2.4947333 42.941868,2.4947336 43.503883,6.0658188 44.515499,14.228559 45.527111,22.391298 48,36.778122 48,36.778122 48,36.778122 47.662846,37.39035 45.527203,39.124943 43.391556,40.859543 41.255997,41.369759 32.038975,38.104572 22.821959,34.839394 22.035215,34.737423 16.752264,35.04353 11.46931,35.349632 7.5352616,39.022934 7.5352621,39.022934 7.5352616,39.022934 6.8607316,25.554411 6.6359253,21.371023 6.4111228,17.187639 4.5003376,4.6374679 4.5003376,4.6374679 4.5003376,4.6374679 7.3103886,3.0049505 12.143682,1.678515 13.654084,1.2640015 15.065696,1.0687021 16.519157,1.0677059 z M2.6672781,0 C4.1403747,0 5.3345561,1.0840253 5.3345556,2.4212387 5.3345561,3.4241478 4.6628294,4.2846398 3.7055032,4.6522026 L3.6687105,4.6644273 6.3859963,40.255939 6.9772267,48 4.941246,47.876789 1.8349936,4.7206259 1.6290536,4.6522026 C0.67172724,4.2846398 5.52291E-07,3.4241478 0,2.4212387 5.52291E-07,1.0840253 1.1941813,0 2.6672781,0 z";
	}
	public class IconeOrdemDesmontagem : IIcone
	{
		public string Data => "M131460 917439l973252 0c72295,0 131450,59165 131450,131469 0,72315 -59155,131460 -131450,131460l-973252 0c-72295,0 -131460,-59145 -131460,-131460 0,-72305 59165,-131469 131460,-131469zm79532 -574592l142385 -125962 -125972 -142385 -142385 125962 125972 142385zm369661 -169199l-173638 77387 -77397 -173638 173648 -77397 77387 173648zm-362068 207749l173638 -77387 77397 173638 -173648 77397 -77387 -173648zm536091 292650l190110 0 0 190110 -190110 0 0 -190110zm-17441 0l-190110 0 0 190110 190110 0 0 -190110zm-397661 0l190110 0 0 190110 -190110 0 0 -190110zm650258 -48328l117469 -149484 -149484 -117469 -117469 149484 149484 117469zm-252597 30887l-190110 0 0 -190110 190110 0 0 190110zm-55803 -503207l188340 25855 -25855 188340 -188340 -25855 25855 -188340zm-550151 843127c28920,0 52372,23452 52372,52382 0,28920 -23452,52372 -52372,52372 -28930,0 -52382,-23452 -52382,-52372 0,-28930 23452,-52382 52382,-52382zm973588 0c28930,0 52382,23452 52382,52382 0,28920 -23452,52372 -52382,52372 -28920,0 -52372,-23452 -52372,-52372 0,-28930 23452,-52382 52372,-52382zm-192266 0c28930,0 52382,23452 52382,52382 0,28920 -23452,52372 -52382,52372 -28930,0 -52372,-23452 -52372,-52372 0,-28930 23442,-52382 52372,-52382zm-196883 0c28930,0 52382,23452 52382,52382 0,28920 -23452,52372 -52382,52372 -28920,0 -52372,-23452 -52372,-52372 0,-28930 23452,-52382 52372,-52382zm-195271 0c28920,0 52372,23452 52372,52382 0,28920 -23452,52372 -52372,52372 -28930,0 -52382,-23452 -52382,-52372 0,-28930 23452,-52382 52382,-52382zm-192295 0c28950,0 52382,23452 52382,52382 0,28920 -23433,52372 -52382,52372 -28920,0 -52372,-23452 -52372,-52372 0,-28930 23452,-52382 52372,-52382z";
	}
	public class IconeRelacaoCfopDevolucao : IIcone
	{
		public string Data => "M90510143 95933572l-27277687 25990 -8649795 -27653472 4113791 3780 7451676 24894563 21450475 95216c3023293,-9902727 6046469,-19806872 9069762,-29708182 1294043,-4186680 860490,-3657557 6549721,-3736234 3239007,-44773 6562952,177201 6480494,2445026 -90491,2463809 -2454004,2502912 -9060784,2502912 -1572132,2774857 -6312625,19236874 -7528582,23462302 -698410,2425652 -1861915,5465839 -2599073,7668099zm-85913301 -95933572l33252093 0 0 47196668 -33252093 0 0 -47196668zm9511230 13530868c0,1963510 -1555829,3410892 -3518513,3410892 -1473371,0 -2645500,-511758 -3590338,-1651636l1268644 -1280458c1311645,1668293 3904694,1402018 3924304,-478680 19020,-1796114 -2476686,-2022578 -3732808,-1089080l-933497 -957479 466749 -4296427 5373458 12050 0 1627536 -3925367 0 -215477 1807101c413588,-198348 967520,-275253 1424227,-275253 1846085,0 3458618,1204025 3458618,3171434zm-3329025 17344244l0 5834536 -4584557 0 9169231 5834890 9169231 -5834890 -4584675 0 0 -5834536 -9169231 0zm11718452 3732572l0 -5834536 -4584557 0 9169231 -5834772 9169231 5834772 -4584557 0 0 5834536 -9169349 0zm-4272564 -17845488l-1854945 0 0 -7503774 -1555829 598350 0 -1567761 3410774 -1268526 0 9741710zm7461954 -245365c-506678,283286 -1079039,424929 -1717319,424929 -638280,0 -1208750,-141643 -1711412,-424929 -3172379,-1787136 -3109413,-7296212 -59894,-9083466 510695,-299234 1101130,-448792 1771306,-448792 670176,0 1258603,151566 1765163,454817 3041959,1819977 3137057,7297511 -47844,9077441zm-694157 -7509680c-275253,-231425 -616307,-347079 -1023161,-347079 -391025,0 -722037,117662 -993391,352985 -1468173,1274433 -1588198,4730571 12050,5953970 271236,207444 598350,311166 981342,311166 382873,0 710105,-103722 981342,-311166 1569296,-1200245 1515781,-4721238 41820,-5959876zm10453824 7755045l-7037025 0 3686027 -4368253c489076,-597760 1232732,-1475498 1232732,-2285781 0,-1968000 -2487082,-1630962 -3470668,-155583l-1292508 -1280576c1610053,-2035100 5158099,-2464636 6390830,0 183463,367043 275253,789846 275253,1268644 0,1777213 -1945200,3722530 -2991988,5026379l3207347 0 0 1795169zm-27877573 35914132l-7568984 17210752 5452489 -1477624c8106495,23487465 25041168,36802030 52649394,27268354 -9874966,923574 -31517882,71471 -39867379,-30681136l5442802 -1477742 -16108323 -10842604zm85798592 14742927l-7612575 23902943 -18269354 0 -7396153 -23902943 2305509 0 5014329 9714303 10283592 -5297615 12389337 1297942 598587 -5714629 2686729 0zm-21041730 -5648947l-8662435 4462051 4462051 8662317 8662317 -4461933 -4461933 -8662435zm17291911 -153456l-9691031 -1015719 -1015601 9691031 9690912 1015601 1015719 -9690912zm-7280854 -6771695l-11052765 -1194692 -751689 7173706 2509527 -1292626 2232857 4334230 2609941 273835 561729 -5364479 3437000 396341 453400 -4326315zm13523899 -2707993l7568984 -17210871 -5452489 1477742c-8106495,-23487583 -25041168,-36802030 -52649394,-27268472 9874966,-923574 31517882,-71353 39867379,30681136l-5442802 1477742 16108323 10842722zm-32064725 54562697c-3147216,-7322674 7085106,-10305329 8773718,-4191524 2026004,7330707 -6441392,9620150 -8773718,4191524zm16709863 0c-3147098,-7322674 7084279,-10305329 8773718,-4191524 2026004,7330707 -6441274,9620150 -8773718,4191524z";
	}
	public class IconeRelacaoDadosFornecedorEntrada : IIcone
	{
		public string Data => "M1750450 1598574c-29892,-197 -42322,-20323 -56282,-25946 -23973,-9767 -51596,-2516 -73940,4242 -34134,10408 -37044,19681 -44739,51793 -15587,62595 -888,91057 1578,149805 1135,26488 2960,17906 7202,33493 8188,29793 10852,64174 21950,94263l32753 90859c31224,63286 52927,180979 72362,256942l23923 79317c493,2022 2318,6807 2960,8632 11493,33641 41385,79070 61461,82918l86815 -4291c-937,19681 56725,214028 61707,227839 19681,53322 93326,333349 116805,416513 36206,127460 -4489,54259 153307,204113 23726,22345 40004,38425 62102,58748 35268,32112 39560,69600 25551,121393 -17659,64568 -7695,366595 -7892,436885 -24861,17560 -46811,24614 -72806,37242l-153652 78429c-8632,3847 -15390,6560 -21506,9767l-249740 129482c-25995,11789 -52187,22542 -73003,37044 -21260,14206 -43161,23282 -68515,34825 -97864,45232 -192768,101514 -287870,146944 -25551,12134 -49968,21408 -73053,36995 -45430,31224 -99886,45676 -148966,73003 -11937,6610 -16919,12233 -28412,18547 -45430,25058 -95397,44542 -138361,70488l-432988 221723c-444,0 -14946,7251 -16278,7744 -38228,17116 -77344,32556 -110985,55098l-155724 76407c-48833,30533 -105559,49475 -153011,80698 -33887,22148 -73694,38623 -110097,57367 0,95644 -11049,249790 8386,332757 10852,45627 4094,375819 4489,445665l2909328 -937 2909328 937c444,-69846 -6314,-400038 4538,-445665 19435,-82967 8386,-237113 8386,-332757 -36403,-18744 -76209,-35219 -110097,-57367 -47452,-31224 -104178,-50165 -153011,-80698l-155724 -76407c-33641,-22542 -72757,-37981 -110985,-55098 -1332,-493 -15834,-7744 -16278,-7744l-432988 -221723c-42963,-25946 -92931,-45430 -138361,-70488 -11493,-6314 -16475,-11937 -28412,-18547 -49080,-27327 -103536,-41780 -148966,-73003 -23085,-15587 -47501,-24861 -73053,-36995 -95102,-45430 -190006,-101711 -287870,-146944 -25354,-11542 -47255,-20618 -68515,-34825 -20816,-14502 -47008,-25255 -73003,-37044l-249740 -129482c-6116,-3206 -12874,-5919 -21506,-9767l-153652 -78429c-25995,-12628 -47945,-19681 -72806,-37242l-8238 7646 -9569 -5179c-1825,-35762 2466,-78922 -641,-112760 -3206,-33739 -8188,-10013 -10852,-47058 -1381,-17412 197,-41138 197,-59438 0,-78873 0,-157549 0,-236373 0,-35022 -9027,-79366 14897,-103290 15834,-15637 31174,-23973 49968,-35959 19879,-12874 122478,-110097 141715,-131998 12677,-14502 46762,-150249 56725,-188723l37242 -130173c4143,-12184 8386,-31421 12480,-46318 23923,-85434 42914,-173136 72757,-257435 12233,-33690 20816,-83658 29152,-120455 58057,7202 111873,32753 144872,-63533l33690 -107335c444,-1578 1085,-2960 1578,-4785 3404,-11049 3650,-11049 7695,-27130l36798 -150298c444,-1776 1184,-3108 1628,-5130 444,-2072 937,-3404 1332,-5229 444,-1776 691,-3404 1135,-5179 1332,-6610 1332,-8632 2960,-15193 6363,-23233 19681,-49475 31865,-73842l43654 -176096c3798,-27130 12628,-38425 10309,-65506l-20964 -130913c-11789,-14453 -58995,-21950 -82326,-20569 -43309,2960 -13072,5229 -83115,30977 0,-64420 9717,-529274 -6116,-581066 -10655,-35268 691,-85877 -4735,-125437 -4341,-28708 -4982,-6067 -9520,-31421 -7448,-41336 -7448,-93375 -19188,-136536 -197,-2022 -937,-3354 -1578,-5130l-22148 -71918c-21457,-60524 -48833,-118630 -78183,-174912l-34825 -55591c-11986,-22394 -18054,-30336 -32309,-51596 -691,-1085 -1578,-2466 -2516,-3847 -8533,-12874 1578,2022 -5229,-7448l-83806 -109160c-18991,-25748 -45232,-43358 -68021,-65506 -22148,-21704 -44345,-38228 -71474,-55838 -24121,-15784 -62793,-37488 -88541,-45874l-46564 -14897c-52237,-15834 -108962,-28264 -160213,-35959 -14255,-2022 -27623,197 -41582,-1825 -66295,-7892 0,-10408 -131603,-9964 -58501,0 -108913,-19879 -163172,-4686 -21901,6067 -7448,6511 -29793,641 -46811,-12677 -166181,-14009 -217678,-5229 -65752,11592 -134908,4341 -168401,11345 -16919,3650 -13515,6758 -37932,8583 -42076,2960 -70784,4538 -112810,14946l-104868 25995c-7892,2220 -12184,3798 -18300,5623l-9027 2960c-94707,30040 -144181,79070 -213535,145760 -32556,31372 -59685,63483 -86617,101021 -17856,24861 -34331,40004 -50165,65802 -5870,9471 -10359,20766 -17166,30484 -9964,13762 -8583,7892 -17412,24170 -6511,11098 -8139,16524 -14699,28708l-80452 175158c-19879,50412 -43851,159325 -45627,209983 -1381,43605 2022,42224 -7942,78824 -12184,44542 4045,115079 -3650,162531 -4242,24910 -10359,41385 -10112,71918 1085,129482 14699,309376 -1578,429141zm-212104 5401205l-890590 2025002 641541 -173876c953827,2763568 2946322,4330129 6194720,3208394 -1161886,108666 -3708368,8435 -4690804,-3609912l640406 -173876 -1895273 -1275732zm9961639 5089609l-3209479 3058 -1017754 -3253676 484041 444 876779 2929107 7103 49 7103 0 7054 49 7103 0 7103 49 7103 0 7103 49 7054 0 7103 49 7103 0 7054 49 7103 0 7103 49 7103 0 7054 99 7103 49 7103 0 7103 49 7054 0 7054 49 7103 0 7103 49 14157 49 7103 49 7054 0 7103 49 7054 0 7103 49 14157 99 7054 49 7103 0 7103 49 7054 0 7103 49 7103 49 7054 0 7054 49 7103 0 7054 49 7103 0 7103 49 7054 99 7103 0 7054 49 7054 0 7103 49 7054 0 7103 49 7103 49 7004 0 7103 49 7103 0 7054 49 7103 0 7103 49 7054 99 7054 0 7054 49 7103 0 7103 49 7054 0 7103 49 236126 1036 0 49 28412 148 7103 49 7103 0 7103 49 7152 0 7054 49 7103 0 7103 49 7103 0 7103 49 7152 0 7103 49 7103 99 7103 0 7103 49 7152 0 7054 49 7103 0 7103 49 7152 49 7103 0 7103 49 7152 0 7103 49 7103 0 7152 49 7103 99 7103 0 7152 49 7103 0 7054 49 7152 49 7103 0 7103 49 7152 0 7103 49 7152 0 7103 49 7103 49 7152 0 7103 99 7152 0 7103 49 14255 49 7103 49 7152 0 7054 49 92635 444 0 -49 229960 987 0 49 42865 197 7152 49 7152 0 7152 49 7152 0 7152 49 7054 0 7152 49 7152 0 7202 49 7152 0 7152 49 7152 99 7152 0 7152 49 7152 0 7152 49 7152 0 7152 49 7152 0 7152 49 7152 0 7152 49 7152 49 7152 0 7152 49 7152 0 7152 99 7152 0 7152 49 7152 49 7152 0 7152 49 7152 0 7202 49 7152 0 7152 49 7152 49 7152 0 7152 49 7152 0 7152 99 7152 0 7152 49 7152 49 7152 0 7152 49 7152 0 7152 49 7152 49 7202 0 7152 49 7152 0 7152 49 7152 99 7152 0 7152 49 7152 0 7152 49 231489 987 0 99 215409 888 7202 49 7202 0 7152 49 28807 148 7152 49 7202 0 7152 49 28757 99 7152 49 7202 0 7202 49 7152 0 7251 99 14354 49 7202 49 7202 0 7152 49 7202 0 7202 49 7152 0 7202 49 7251 0 7152 49 7202 0 7202 49 7152 0 7202 99 945244 -3096226c15143,-35416 27968,-78084 39363,-128989l82523 -270260c152271,-492624 101267,-430325 770628,-439598 381146,-5278 772207,20865 762489,287672 -10605,289892 -288708,294479 -1066045,294479 -184974,326492 -742759,2263446 -885806,2760608 -82178,285354 -219108,643070 -305824,902182zm336210 -3354993l-89577 161248 -806143 2651202 -2149551 0 -870218 -2812450 271247 0 589995 1142994 1209980 -623290 1457697 152715 70438 -672419 316134 0zm-2475797 -664626l-1019185 524982 524982 1019234 1019234 -524982 -525031 -1019234zm2034571 -18054l-1140232 -119518 -119518 1140232 1140232 119518 119518 -1140232zm-856654 -796771l-1300444 -140531 -88442 844026 295269 -152074 262713 509938 307058 32260 66098 -631231 404428 46663 53322 -509050zm1388394 -318600l890541 -2025051 -641491 173876c-953827,-2763518 -2946372,-4330129 -6194720,-3208394 1161886,-108666 3708368,-8386 4690755,3609912l-640406 173876 1895322 1275781zm-1961370 1880820l-42569 -82770 51300 0 -8731 82770zm-1608538 4538977c-370294,-861586 833618,-1212495 1032305,-493167 238395,862524 -757853,1131896 -1032305,493167zm1966106 0c-370294,-861586 833520,-1212495 1032305,-493167 238395,862524 -757902,1131896 -1032305,493167z";
	}
	public class IconeAnimal : IIcone
	{
		public string Data => "M8.35,3C9.53,2.83 10.78,4.12 11.14,5.9C11.5,7.67 10.85,9.25 9.67,9.43C8.5,9.61 7.24,8.32 6.87,6.54C6.5,4.77 7.17,3.19 8.35,3M15.5,3C16.69,3.19 17.35,4.77 17,6.54C16.62,8.32 15.37,9.61 14.19,9.43C13,9.25 12.35,7.67 12.72,5.9C13.08,4.12 14.33,2.83 15.5,3M3,7.6C4.14,7.11 5.69,8 6.5,9.55C7.26,11.13 7,12.79 5.87,13.28C4.74,13.77 3.2,12.89 2.41,11.32C1.62,9.75 1.9,8.08 3,7.6M21,7.6C22.1,8.08 22.38,9.75 21.59,11.32C20.8,12.89 19.26,13.77 18.13,13.28C17,12.79 16.74,11.13 17.5,9.55C18.31,8 19.86,7.11 21,7.6M19.33,18.38C19.37,19.32 18.65,20.36 17.79,20.75C16,21.57 13.88,19.87 11.89,19.87C9.9,19.87 7.76,21.64 6,20.75C5,20.26 4.31,18.96 4.44,17.88C4.62,16.39 6.41,15.59 7.47,14.5C8.88,13.09 9.88,10.44 11.89,10.44C13.89,10.44 14.95,13.05 16.3,14.5C17.41,15.72 19.26,16.75 19.33,18.38Z";
	}
	public class IconeRelacaoEspecieConta : IIcone
	{
		public string Data => "M12845284 43507246l-5797605 13182447 4176331 -1131906c6209266,17990398 19180103,28188467 40326671,20886150 -7563699,707401 -24140901,54909 -30536409,-23499969l4168945 -1131906 -12337933 -8304817zm67626633 -412945l5797284 -13182768 -4176010 1131906c-6209266,-17990077 -19180424,-28188467 -40326671,-20886150 7563699,-707401 24140901,-54588 30536088,23499969l-4168945 1131906 12338254 8305138zm-24183608 16056685l32441864 0 0 2130552 -32441864 0 0 -2130552zm-56288309 -36430347l3397644 -1806554 459184 863459 -2266380 1205118 -267804 142251 142572 267804 6981530 13130428 142251 267804 267804 -142572 27607904 -14679453 267804 -142251 -142251 -267804 -1259386 -2368172 863781 -459184 1860821 3499756 -29870752 15882645 -8184722 -15393276zm4141330 -407486l-1998255 1062546 6696708 12594820 27072296 -14394951 -1116814 -2100046 -25073719 13332084 -5580215 -10494452zm-2136332 -6430509l29870752 -15882645 8184722 15393276 -29870752 15882645 -8184722 -15393276zm18857389 695520l1212825 -175967c804376,-89589 1321681,78029 1542282,493543 272942,515057 103718,999609 -510241,1446913 -994150,562902 -1425077,820431 -2591984,1033647 -623592,113351 -1125162,140966 -1503749,84130l944058 1775406c1019839,-46240 2002751,-315970 2950340,-814330l718640 1350901 1018234 -541388 -737906 -1388150c864423,-580242 1427967,-1219247 1690313,-1914446 261703,-697447 204867,-1400994 -170187,-2107432 -325282,-613316 -827495,-1021445 -1498290,-1220532 -672400,-197481 -1609714,-195555 -2805200,7707l-1215715 170508c-804376,48808 -1322965,-148994 -1555447,-587307 -270052,-509598 -114957,-992545 475240,-1444023l1005710 -565471c855111,-350971 1705405,-520195 2547671,-502534l-919011 -1728203c-614280,8670 -1421866,253354 -2426292,730521l-619417 -1164659 -1018234 541388 633225 1191311c-811761,529507 -1349938,1166585 -1611962,1912519 -266199,746255 -220601,1455904 134544,2125735 330099,621986 812724,1047133 1441133,1267734 627445,221886 1583384,229271 2869421,22478zm-17266941 -433496l27607904 -14679774 267804 -142251 142251 267804 6981852 13130428 142251 267804 -267804 142251 -27607904 14679453 -267804 142572 -142251 -267804 -6981530 -13130428 -142572 -267804 267804 -142251zm27624923 -14001594l-27072296 14394630 6696708 12594820 27072296 -14394951 -6696708 -12594499zm40554016 69101803l1432463 435101c934103,315649 1402921,724420 1402921,1222779 0,615243 -468818,979701 -1402921,1092088 -1418013,30826 -2041284,58442 -3444847,-320466 -750429,-202940 -1316222,-424826 -1698340,-664372l0 2116423c1145713,461111 2381016,695841 3710724,701300l0 1610999 1432463 0 0 -1655312c1286038,-112067 2276977,-427716 2970249,-944058 693272,-518589 1041353,-1199017 1041353,-2043531 0,-732768 -311475,-1358287 -930892,-1874629 -622950,-516021 -1651137,-977132 -3080710,-1378838l-1432463 -443129c-910983,-351934 -1363425,-789926 -1363425,-1314295 0,-609463 452441,-979701 1363425,-1107502l1432463 -28900c1142181,96654 2172938,359641 3087132,792816l0 -2061835c-679465,-295098 -1707331,-466570 -3087132,-518911l0 -1389434 -1432463 0 0 1420903c-1199017,90874 -2160094,416798 -2882908,980343 -726989,562260 -1091767,1242688 -1091767,2042889 0,743686 280970,1374342 840661,1890363 559049,516342 1602971,995756 3134014,1439206zm-16925604 11914713l35320918 0 0 1173329 -35320918 0 0 -1173329zm732447 -1850224l33856024 0 0 1173329 -33856024 0 0 -1173329zm707080 -1761920l32441864 0 0 1173008 -32441864 0 0 -1173008zm24541322 -16135357l0 15280246 -2417943 0 0 -15280246 -666299 0 0 -1471638 666299 0 2417943 0 666299 0 0 1471638 -666299 0zm-14222837 0l0 15280246 -2417622 0 0 -15280246 -666620 0 0 -1471638 666620 0 2417622 0 666620 0 0 1471638 -666620 0zm21262810 0l0 15280246 -2417622 0 0 -15280246 -666620 0 0 -1471638 666620 0 2417622 0 666620 0 0 1471638 -666620 0zm-28303103 0l0 15280246 -2417622 0 0 -15280246 -666620 0 0 -1471638 666620 0 2417622 0 666620 0 0 1471638 -666620 0zm-433817 -6737167l27034726 0 -13517363 -5648290 -13517363 5648290zm30893160 0l0 1612283 -34751594 0 0 -1612283 17375797 -7260573 17375797 7260573z";
	}
	public class IconeGerenciamentoRotinaDiaria : IIcone
	{
		public string Data => "M60.652,75.816V15.163C60.652,6.781,67.433,0,75.817,0c8.38,0,15.161,6.781,15.161,15.163v60.653c0,8.38-6.781,15.161-15.161,15.161C67.433,90.978,60.652,84.196,60.652,75.816z M318.424,90.978c8.378,0,15.163-6.781,15.163-15.161V15.163C333.587,6.781,326.802,0,318.424,0c-8.382,0-15.168,6.781-15.168,15.163v60.653C303.256,84.196,310.042,90.978,318.424,90.978z M485.212,363.906c0,66.996-54.312,121.307-121.303,121.307c-66.986,0-121.302-54.311-121.302-121.307c0-66.986,54.315-121.3,121.302-121.3C430.9,242.606,485.212,296.919,485.212,363.906zM454.89,363.906c0-50.161-40.81-90.976-90.98-90.976c-50.166,0-90.976,40.814-90.976,90.976c0,50.171,40.81,90.98,90.976,90.98C414.08,454.886,454.89,414.077,454.89,363.906z M121.305,181.955H60.652v60.651h60.653V181.955z M60.652,333.584h60.653V272.93H60.652V333.584z M151.629,242.606h60.654v-60.651h-60.654V242.606z M151.629,333.584h60.654V272.93h-60.654V333.584zM30.328,360.891V151.628h333.582v60.653h30.327V94c0-18.421-14.692-33.349-32.843-33.349h-12.647v15.166c0,16.701-13.596,30.325-30.322,30.325c-16.731,0-30.326-13.624-30.326-30.325V60.651H106.14v15.166c0,16.701-13.593,30.325-30.322,30.325c-16.733,0-30.327-13.624-30.327-30.325V60.651H32.859C14.707,60.651,0.001,75.579,0.001,94v266.892c0,18.36,14.706,33.346,32.858,33.346h179.424v-30.331H32.859C31.485,363.906,30.328,362.487,30.328,360.891zM303.256,242.606v-60.651h-60.648v60.651H303.256z M409.399,363.906h-45.49v-45.49c0-8.377-6.781-15.158-15.163-15.158s-15.159,6.781-15.159,15.158v60.658c0,8.378,6.777,15.163,15.159,15.163h60.653c8.382,0,15.163-6.785,15.163-15.163C424.562,370.692,417.781,363.906,409.399,363.906z";
	}
	public class IconeRelacaoPisCofinsEntrada : IIcone
	{
		public string Data => "M99907 690225l-99907 227181 71976 -19508c107004,310045 330543,485798 694975,359949 -130352,12194 -416039,940 -526260,-404993l71847 -19508 -212632 -143120zm706566 237891c0,-214235 279659,-186076 190748,18538 -44579,102599 -190748,93899 -190748,-18538zm65097 6849c0,55566 77726,37443 77726,-73837 0,-55635 -77726,-31633 -77726,73837zm27278 324713l-71303 0 377804 -479651 71412 0 -377913 479651zm190748 -89841c0,-214305 279510,-186858 190738,18439 -44243,102322 -190738,93573 -190738,-18439zm65087 6532c0,55318 77726,38076 77726,-73520 0,-56813 -77726,-30901 -77726,73520zm-378141 -856803c45856,-44738 42669,-87990 4751,-98779 -33019,-9393 -55575,-52557 -86664,-50201 -32029,2435 -45183,-12362 -57585,-9462 -16994,3969 -12174,12392 -23784,-20706 -24457,4137 -11600,-9710 -49805,-18717 -26377,-6216 -14124,19696 -41244,16687l13679 -26476 -37393 -1217 -6602 17083 -9373 188c15668,-54279 13500,-20340 24804,-52240 -34662,-27941 -5473,-35602 -30623,-52181l-27189 37037 -63593 -1623 2524 11689c-47548,3573 -62593,14183 -77519,1504 -8185,-6948 -9917,-26130 -5078,-37512 6750,-15866 1782,-6899 -7839,-32266 -37037,22250 -39838,15609 -51517,32405l-34761 -8334c11105,23952 21112,29891 24121,39036 -24348,17806 -67839,49548 -80894,4682l-16697 3533 -31257 8472c5632,6315 11422,10878 12184,17430l-16470 3593c-9809,45133 23022,11066 -1772,101194 -32187,-1861 -17311,-3949 -40759,8997 -21349,11798 -16400,11610 -14500,37779 -45460,25882 10501,24526 1930,53636l13570 1247 3276 9848 31821 -11640c-861,25041 -2870,21290 5602,30752 26773,-742 29178,7037 56823,-8997 5454,-3157 4692,-19894 41679,-22567 2375,63692 23339,50260 36196,52725 3533,673 12847,10868 17064,13085 11184,5879 61098,4464 48944,40105l3147 22616 35533 119c2435,48033 27199,13421 11135,61831 -8552,25774 475,2039 1405,55100l40006 3959 7017 31999 14668 -3583c-2267,69135 5038,74638 4711,74856 -14144,9304 -51270,42352 -53240,53913 18014,8413 11481,3642 22230,16668l10333 -4830c19399,29366 35978,22963 40610,40412l-7859 8334 1930 9908c27169,-27377 69363,-91722 75589,-97354 31623,-28584 -20617,-38393 40877,-82636 83952,-51122 81349,-21527 95988,-29376 41263,-32059 10472,-18964 36275,-50379 30782,-37483 -2593,-3256 22309,-45955 27872,-47796 -5652,-91375 29792,-105410l41491 -49983zm488827 289121l99917 -227191 -71976 19508c-107004,-310035 -330543,-485788 -694975,-359949 130352,-12194 416039,-940 526250,404993l-71847 19508 212632 143130z";
	}
	public class IconeSugestaoCompra : IIcone
	{
		public string Data => "M75179 267144l243412 0 0 -183203c0,-46163 37811,-83941 83941,-83941l294181 0c46155,0 83941,37815 83941,83941l0 105672c7806,-2430 16095,-3742 24674,-3742l319123 0c45965,0 83571,37606 83571,83571l0 319123c0,45965 -37606,83571 -83571,83571l-148368 0 -214902 91028 75937 -91028 -31790 0c-45965,0 -83571,-37606 -83571,-83571l0 -178011c-18041,5684 -44521,3840 -64947,3840l-77638 107207 -2485 -107207 -139261 0 0 214997c0,41347 -33831,75179 -75179,75179l-48076 0 64995 106639 -174335 -106639 -129653 0c-41351,0 -75179,-33831 -75179,-75179l0 -287069c0,-41351 33828,-75179 75179,-75179zm764889 865566c-157372,0 -314743,0 -472115,0l15285 -98145 167152 -84076 810 -46716 -17945 -17176 -18121 -62393c-15860,5519 -8850,-3331 -14962,-19551 -9773,-26073 -20610,-63496 4522,-50062 -2312,-55170 4511,-116065 35697,-127817 24685,-9334 39515,-8256 62833,-8553 23314,297 27199,-5009 51861,4328 6457,2426 14838,12320 19649,16095 22629,17773 29749,50820 27045,115947 25139,-13434 14303,23988 4507,50062 -6102,16219 894,25069 -14966,19551l-18107 62393 -17956 17176 806 46716 168329 86118 15677 96104zm-507673 -865566l29852 0c41347,0 75179,33828 75179,75179l0 58267 152699 0 1847 79837 57820 -79837c22207,0 53565,2441 71965,-4658l0 -126490c0,-32109 18356,-60143 45089,-74120l0 -111381c0,-38595 -31537,-70133 -70133,-70133l-294181 0c-38595,0 -70136,31537 -70136,70133l0 183203zm170443 12841l154513 -2147 36591 -156690 -176559 161c-35037,110 -42264,5006 -46357,-16183 -12317,-36034 -4189,-32050 -41245,-32571 -18623,-249 -37734,1026 -37258,14065 520,14164 14109,14391 52092,14391 9114,16055 48728,150592 58223,178974zm153594 61902c18096,-42099 -40732,-59249 -50443,-24102 -11653,42146 37031,55313 50443,24102zm-96067 0c18092,-42099 -40735,-59249 -50443,-24102 -11653,42146 37027,55313 50443,24102zm-18499 -197242c10210,0 20434,0 30629,0l2485 55137c-8769,26 -17557,59 -26337,99l-6776 -55236zm84054 64812c8568,0 17132,0 25703,0l-8714 54840c-7399,33 -14772,66 -22149,99l5160 -54939zm-120898 0c8718,0 20727,0 29445,0l7582 55295c-7483,29 -14970,62 -22464,92l-14563 -55386zm81654 0c8615,0 17246,0 25843,0l-3305 54994c-7399,26 -14820,66 -22240,99l-297 -55093zm-39068 0c8667,0 17352,0 26011,0l2114 55141c-7454,29 -14915,62 -22376,95l-5750 -55236zm86462 -64812c10089,0 20166,0 30273,0l-10268 54833c-8714,33 -17399,62 -26077,99l6072 -54932zm-148929 0c10264,0 30991,0 41252,0l8931 55288c-8810,33 -24798,70 -33626,92l-16557 -55379zm102725 0c10136,0 20298,0 30423,0l-3881 54991c-8718,22 -17458,62 -26202,95l-341 -55086zm357659 92655c-92860,13995 -134263,114786 -81911,198298 16293,25986 31966,35092 42586,81738l0 -836 109007 0c5233,-23695 12929,-37148 39108,-60249 83868,-108336 3650,-235900 -108790,-218951zm-37371 290429c520,3390 971,6933 1422,10825l101571 0c634,-3840 1235,-7435 1843,-10825l-104836 0zm2664 22053c366,3357 766,6966 1220,10920l95418 0c663,-3833 1275,-7465 1843,-10920l-98482 0zm2576 22149l1491 9381 88800 0c634,-3243 1220,-6369 1774,-9381l-92065 0zm7362 20610c16410,23974 61440,15831 76370,0l-76370 0zm49967 -235094l10448 -60040 -59407 90226 33131 0 -10345 60737 63456 -91035 -37283 114zm-845265 233349l253091 0 0 23457 -253091 0 0 -23457zm0 -69195l253091 0 0 23453 -253091 0 0 -23453zm0 -147334l256667 0 0 23450 -256667 0 0 -23450zm0 72845l260218 0 0 23446 -260218 0 0 -23446zm-47042 -14292c35085,0 35081,62650 0,62650 -35092,0 -35092,-62650 0,-62650zm0 96661c35085,0 35085,62646 0,62646 -35092,0 -35092,-62646 0,-62646z";
	}
	public class IconeRelacionamentoCliente : IIcone
	{
		public string Data => "M261841 412283c5806,2807 29748,44626 72408,19398 8213,-4855 15499,-14300 19637,-16785 32,-138 31971,-50837 27938,-75339 -4819,-29256 23142,-77842 -70322,-96708 -37162,-7501 -73028,24318 -72771,62288 115,16812 3863,23050 -262,30832 -8783,16560 23331,76175 23372,76312zm-78876 -299402l8415 3693 -3698 8415 -8415 -3693 3698 -8415zm-7396 16831l8415 3693 -3693 8415 -8415 -3693 3693 -8415zm-7386 16826l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm-7386 16822l8415 3693 -3693 8411 -8415 -3693 3693 -8411zm306669 -144159l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7120 16941l8470 -3555 3560 8470 -8470 3555 -3560 -8470zm7115 16941l8470 -3555 3555 8470 -8470 3555 -3555 -8470zm7111 16941l8470 -3555 3555 8466 -8470 3555 -3555 -8466zm7111 16932l8470 -3555 3555 8466 -8470 3555 -3555 -8466zm-183411 -51539l-9187 0 0 -9187 9187 0 0 9187zm0 -18374l-9187 0 0 -9187 9187 0 0 9187zm0 -18370l-9187 0 0 -9187 9187 0 0 9187zm0 -18374l-9187 0 0 -9187 9187 0 0 9187zm0 -18374l-9187 0 0 -9187 9187 0 0 9187zm0 -18374l-9187 0 0 -9187 9187 0 0 9187zm184724 -32242l-22632 0 0 -59031 -40363 57520 -40359 -57520 0 59031 -22632 0 0 -96942 23762 0 39229 56014 39422 -56014 23574 0 0 96942zm-143148 0l-30928 0 -48471 -54319 58278 0c4148,0 6224,-3330 6224,-9996 0,-6665 -2076,-9996 -6224,-9996l-70162 0 0 74310 -22632 0 0 -96942 94112 0c8677,0 15526,3395 20556,10184 4653,6160 6978,13643 6978,22444 0,8801 -2324,16284 -6978,22444 -5030,6789 -11879,10184 -20556,10184l-8677 0 28480 31686zm-123346 0l-75063 0c-8801,0 -16284,-2921 -22444,-8737 -6160,-5848 -9242,-13174 -9242,-21943l0 -35582c0,-8769 3082,-16096 9242,-21943 6160,-5811 13643,-8737 22444,-8737l75063 0 0 22632 -75063 0c-6036,0 -9054,2706 -9054,8080l0 35517c0,5374 3018,8080 9054,8080l75063 0 0 22632zm-150002 316945c14534,7014 19908,29013 47488,12697 5742,-3399 14566,-10818 17465,-12559 3280,-13813 21764,-34217 18999,-51025 -1883,-11461 6890,-46753 -18714,-65554 -8190,-6018 -9913,-9573 -21796,-11971 -15375,2246 -63538,1493 -57415,23822 -7860,11599 2141,42706 -2375,51200 -6445,12127 12159,38783 16348,53391zm70883 19614l18976 9219 27938 14405c-29027,17442 -35439,73997 -38921,104985 -1034,175 -31925,4318 -23992,3064 -6936,-45701 -7754,-94939 11107,-135234l4892 3560zm-141894 88494c-1824,16583 -2356,23105 9040,27589 17901,7056 78600,16642 71576,15590 6932,-45715 7754,-94925 -11112,-135234 -39582,36078 -58398,-8921 -69505,92054zm128959 -85761c-16927,13036 -34479,13036 -51411,0 15021,38168 14258,88949 9385,129630 0,0 33441,684 33106,-119 -1663,-4001 -3849,-49197 -3450,-47378 -749,-27648 2361,-56693 12370,-82132zm330316 -2733l-42945 19077c41011,19242 41236,66138 50373,119836 25793,-2444 118959,-17180 131109,-25131 12347,-8080 -3064,-79822 -14989,-91581 -10845,-10694 -47607,-18016 -50653,-26160 -1093,441 -2012,-643 -1355,4534 781,6247 -14980,18755 -28544,23464 -10115,138 -35499,-18641 -36119,-29141 -2485,1553 -3183,3312 -6877,5103zm6403 -15301c4051,1966 20822,31227 50662,13574 5742,-3399 10841,-10009 13739,-11746 5,-28 22122,-37088 19545,-52715 -1874,-11369 6872,-46781 -18709,-65559 -8195,-6013 -12076,-1447 -23964,-3845 -15375,2251 -25434,-5728 -42247,12448 -26564,25669 -8990,42412 -15379,54452 -6151,11585 16325,53290 16353,53391zm-213994 21424c-42008,33129 -77415,-9504 -92348,126474 -2605,23730 -3372,33023 12926,39435 40460,15921 97167,24167 141228,24699 27938,340 115394,-14373 136194,-27988 17644,-11553 -4378,-114094 -21420,-130893 -15503,-15283 -68044,-25742 -72403,-37387 -6794,2774 5935,44328 3509,53161l-50299 128463 -47561 -125041c-2435,-9431 776,-45072 0,-58218 -3546,2223 -4548,4736 -9826,7295zm55775 154784l1980 8475 17056 -103240c-17221,-11171 -15264,-15338 -34029,-32l14993 94797zm1190 -106855l22504 -33050c-18746,3009 -15678,8016 -43427,78l20924 32972z";
	}
	public class IconeGestaoEmpresarial : IIcone
	{
		public string Data => "M9.3710003,24.254 L13.346,24.254 13.346,26.78 9.3710003,26.78 z M2.6573474,22.771001 L4.0620008,22.771001 4.0241804,26.78 0.05,26.78 0.12919509,25.175419 z M13.971,21.594001 L17.945999,21.594001 17.945999,26.78 13.971,26.78 z M4.6959996,20.663 L8.6710005,20.663 8.6710005,26.78 4.6959996,26.78 z M18.690998,16.389 L22.665998,16.389 22.665998,26.78 18.690998,26.78 z M23.410999,15.179 L27.386,15.179 27.386,26.78 23.410999,26.78 z M11.499001,6.1319912 L11.499001,10.691982 16.058001,10.691982 16.058001,6.1319912 z M0.76699984,6.1319912 L0.76699984,10.691982 5.3260002,10.691982 5.3260002,6.1319912 z M18.720573,4.9210003 L26.833466,4.9258831 26.851,13.091347 23.706558,9.9468954 13.639712,20.094445 9.0475473,15.300168 0.81049418,23.459001 0.59900016,19.633668 9.0474052,11.265547 13.63947,16.059703 21.70904,7.9095328 z M11.499001,0.80599957 L11.499001,5.3659924 16.058001,5.3659924 16.058001,0.80599957 z M6.052,0.80599957 L6.052,5.3659924 10.611,5.3659924 10.611,0.80599957 z M0.76699984,0.80599957 L0.76699984,5.3659914 5.3260002,5.3659914 5.3260002,0.80599957 z M0.05,0.05 L27.386,0.05 27.386,4.1648586 26.669001,4.1631468 26.669001,0.80599957 22.110001,0.80599957 22.110001,4.1522625 21.343,4.1504314 21.343,0.80599957 16.784,0.80599957 16.784,5.3659924 18.196293,5.3659924 18.962271,6.1319912 16.784,6.1319912 16.784,10.691982 17.791861,10.691982 17.073809,11.409981 16.784,11.409981 16.784,11.699767 16.058001,12.425712 16.058001,11.409981 11.499001,11.409981 11.499001,12.668217 10.611,11.78024610.611,11.409981 10.240725,11.409981 9.5227022,10.691982 10.611,10.691982 10.611,6.1319912 6.052,6.1319912 6.052,10.691982 8.5121355,10.691982 7.7941265,11.409981 6.052,11.409981 6.052,13.152084 5.3260002,13.878074 5.3260002,11.409981 0.76699984,11.409981 0.76699984,15.969973 3.2340722,15.969973 2.4280617,16.775972 0.76699984,16.775972 0.76699984,18.43701 0.05,19.154001 z";
	}
	public class IconeGrade : IIcone
	{
		public string Data => "M28.385,37.331001 L49.954,37.331001 C51.145279,37.331001 52.111,38.155469 52.111,39.172501 52.111,40.189533 51.145279,41.014001 49.954,41.014001 L28.385,41.014001 C27.193722,41.014001 26.228001,40.189533 26.228,39.172501 26.228001,38.155469 27.193722,37.331001 28.385,37.331001 z M28.385,32.055 L49.954,32.055 C51.145279,32.055001 52.111,32.879468 52.111,33.896502 52.111,34.913533 51.145279,35.738001 49.954,35.738001 L28.385,35.738001 C27.193722,35.738001 26.228001,34.913533 26.228,33.896502 26.228001,32.879468 27.193722,32.055001 28.385,32.055 z M28.385,26.679001 L49.954,26.679001 C51.145279,26.679001 52.111,27.503468 52.111,28.520501 52.111,29.537533 51.145279,30.362001 49.954,30.362 L28.385,30.362 C27.193722,30.362001 26.228001,29.537533 26.228,28.520501 26.228001,27.503468 27.193722,26.679001 28.385,26.679001 z M10.533995,0 L10.777001,0 19.092131,0 19.089001,0.083499908 C19.089001,1.9245415 21.301161,3.4170017 24.030002,3.4170017 26.758839,3.4170017 28.971001,1.9245415 28.971001,0.083499908 L28.967872,0 37.461152,0 37.525998,0 37.525998,0.13037682 37.542741,0.13111305 47.875,8.6610374 41.365598,16.847009 37.525998,13.795088 37.525998,25.285 27.721002,25.285 C26.034875,25.285 24.668,26.651652 24.668,28.3375 24.668,29.496521 25.314063,30.504671 26.265759,31.02158 L26.370078,31.075001 26.265759,31.12842 C25.314063,31.645329 24.668,32.653481 24.668,33.8125 24.668,34.339329 24.801484,34.834984 25.03648,35.267502 L25.071417,35.325001 10.777001,35.325001 10.533995,35.325001 10.533995,13.743546 6.5845051,16.847011 0,8.6610374 10.451465,0.13111305 10.533995,0.12752342 z";
	}
	public class IconeImportacaoAbcFarma : IIcone
	{
		public string Data => "M10.7652549549937,21.6160068541765C10.7652549549937,21.6160068541765 10.2496804997325,22.4915008574724 9.59349439591169,22.8977508574724 8.93730733841658,23.3040008574724 8.65608594864607,23.4446258574724 8.65608594864607,23.4446258574724L8.78321931809187,23.5436897307634 8.84669111222029,23.604000094533 14.4681539341807,23.604000094533 14.5461425587535,23.4130001097918C14.5461425587535,23.4130001097918 13.2493905827403,23.1789951354265 12.077629069984,22.5227355986834 10.9058675572276,21.8664760619402 10.7652549549937,21.6160068541765 10.7652549549937,21.6160068541765z M10.6427717015147,16.3070011168718C10.6427717015147,16.3070011168718 9.43652914017439,16.8040008574724 9.53028963059187,18.2102508574724 9.62404916733503,19.6165008574724 10.1839141651988,20.220455172658 11.2921170994639,21.1367435485125 12.1047143742442,21.8086185485125 14.108632068336,22.6009998351336 14.108632068336,22.6009998351336L14.1401347920299,22.5071277648211C14.1401347920299,22.5071277648211 12.5463561818004,21.5068187743425 11.5929384037852,20.5849990874529 10.6395206257701,19.6631774932146 10.6427717015147,18.631868365407 10.6427717015147,18.631868365407z M22.249124507606,15.729000094533L26.4489993855357,19.8630008727312 24.4650001332164,19.8630008727312 24.4650001332164,27.1368553684848 19.9080009266734,27.1368553684848 19.9080009266734,19.8630008727312 17.9549999043345,19.8630008727312z M11.3621110722423,13.9300012618303L11.3621110722423,19.2326450377703 11.3720941349864,19.2529068022966C11.5124807164073,19.5275650054216,11.7169990345836,19.8263473540545,12.0149421498179,20.1144351989031L12.0271167561412,20.1259994536638 12.0271167561412,14.408253672719 11.9952812001109,14.3820390731096C11.848941783607,14.2640514403582,11.6735219761729,14.135042193532,11.4632901951671,13.9957942992449z M7.26407096832991,6.58700561821461L7.27638385742903,7.12167311012745C7.27638385742903,7.12167263329029 7.27638385742903,7.12167263329029 7.45895812958479,7.28600979149342 7.64153240174055,7.45033121407032 7.9701599881053,7.92503166496754 9.12037179917097,9.05401230156422 10.2705821797252,10.1829776793718 12.4432019993663,11.5040054351091 12.4432019993663,11.5040054351091 12.4432019993663,11.5040054351091 12.8628654286265,11.4488601714373 14.6154823109508,10.1342716246843 16.3681049153209,8.81968403160572 16.1857452198863,6.58700561821461 16.1857452198863,6.58700561821461z M8.19929979294539,2.39958358108997C8.15680692642927,2.39876938164234 8.11351774185896,2.39879799187183 8.06941030472517,2.39972877800465 7.90655324906111,2.40316391289234 7.73255966156721,2.4188942939043 7.54643390625715,2.44991541206837 6.55376289337873,2.61536121666431 5.4076976582408,3.77885175049305 5.21861122101545,5.05514622032642 5.02952478379011,6.33144045174122 5.2092146679759,7.6619386702776 6.38774154633284,9.05398369133473 7.63731620758772,10.5299482375383 9.2135476872325,11.701126101613 10.9389629170299,12.6937990218401 12.6643781468272,13.6864728957415 12.7762117192149,14.2855310469866 12.7762117192149,14.2855310469866L12.7762117192149,16.9330005675554C12.7762117192149,16.9330005675554 13.3680686756969,16.4283657103777 13.5504264637828,15.8463058501482 13.7958831593394,15.0628423720598 13.7961330220103,15.1724453002214 13.7961330220103,14.2793798476458 13.7961330220103,13.0668773680925 12.1358871266246,12.0611276656389 10.8830032154918,11.25745010674 9.63011835068464,10.4537725478411 8.96342275589705,10.0231180220842 7.86159370392561,8.88840675652027 6.75976512879133,7.75369501411915 6.5685429379344,7.28087473213673 6.33213232010603,6.14628553688526 6.09572170227766,5.0116963416338 6.57783840149641,4.0555253058672 7.56210658997297,3.6180765658617 9.07327936142683,2.9464514285326 10.7936878010631,5.07040834724903 10.7936878010631,5.07040834724903 10.7936878010631,5.07040834724903 10.7214584156871,4.12093544304371 10.5773534581065,3.819626095891 10.2335996434093,3.10087633430958 9.63984392136335,2.80397081673145 9.63984487503767,2.80397057831287 9.29468820542097,2.61352873146534 8.83668897598982,2.41179085075855 8.19929979294539,2.39958358108997z M0.05,0.05L17.6056079670787,0.05 22.5797271534801,5.78099966347218 22.5860004231334,14.4198093444109 22.3136081501842,14.1520004302263 15.1701459690928,21.021101000905 18.6747054859996,21.021101000905 18.6747054859996,27.1099987059832 0.05,27.1099987059832z";
	}
	public class IconeImposto : IIcone
	{
		public string Data => "M67.718521,151.91135 C70.386353,151.91135 71.94561,155.19171 71.068275,158.80354 L70.641518,161.02353 C69.764183,164.63536 66.890259,167.56334 64.222412,167.56334 61.554573,167.56334 60.054806,164.37105 60.932137,160.75922 L61.299412,158.45114 C62.176746,154.83931 65.050674,151.91135 67.718521,151.91135 z M69.000038,145.50034 C62.418674,145.54375 55.808411,150.82689 54.212368,157.39751 54.212368,157.39751 49.55685,173.17732 62.836891,173.97433 69.302139,174.36234 76.028519,168.6478 77.624557,162.07716 77.624557,162.07716 82.15757,145.41357 69.000038,145.50034 z M34.092632,122.43734 C36.760468,122.43734 38.319733,125.71771 37.442394,129.32954 L37.01564,131.54953 C36.138306,135.16136 33.264374,138.08934 30.596533,138.08934 27.928698,138.08934 26.428915,134.89705 27.306253,131.28522 L27.673527,128.97714 C28.550859,125.36531 31.424791,122.43734 34.092632,122.43734 z M35.374153,116.02634 C28.792791,116.06975 22.182528,121.35288 20.586487,127.9235 20.586483,127.92351 15.930965,143.70332 29.211014,144.50034 35.676258,144.88835 42.402637,139.1738 43.99868,132.60316 43.99868,132.60316 48.531685,115.93958 35.374153,116.02634 z M68.130821,115.50001 L23.136023,174.5 30.646004,174.5 75.6408,115.50001 z M78.067711,13.721559 C77.565666,13.784236 75.211349,15.546641 71.559525,16.329187 68.837219,16.91254 69.537498,19.69659 67.514778,20.000004 64.814178,20.405102 64.248909,18.166729 61.500584,18.166729 65.3666,24.610187 65.200439,21.03632 63.500549,26.833416 L63.477528,26.864664 63.319302,26.964548 C62.686565,27.408733 62.183861,28.037739 61.882938,28.777012 L61.868225,28.8188 61.730385,28.944851 C61.184368,29.435596 59.995476,28.85844 58.54287,29.826856 54.336105,32.631389 50.93874,28.472553 50.542931,26.493517 46.665638,26.493519 45.595413,28.833416 42.167385,28.833416 45.750088,31.392521 45.93568,31.925587 44.062996,32.095837 38.003727,32.646686 41.063015,32.704311 41.063015,36.429172 41.063007,39.026241 42.726677,40.68095 42.50069,43.166782 42.280445,45.58942 41.500694,48.173748 41.500687,50.833462 34.384487,50.833462 29.834105,52.647941 29.834101,57.166801 29.834095,58.572925 30.809319,60.768722 29.834091,61.500145 29.071003,62.072468 27.380774,61.123199 26.834112,62.166813 25.885998,63.97686 29.566401,65.694511 29.834084,67.166824 30.544415,71.073707 29.931051,68.93045 32.500729,71.500168 35.687405,74.686882 35.866928,71.328323 38.167355,70.500168 39.659428,69.96302 39.285309,74.529671 41.833977,75.166839 44.852905,75.921577 45.091122,76.629578 47.833946,75.500175 50.344425,74.466438 51.3573,73.900764 52.833912,71.833504 53.620163,70.732712 55.411469,71.599174 56.500542,71.500168 58.763161,71.294472 56.86599,70.096016 57.833881,69.166824 60.537872,66.570969 58.348385,78.670677 61.500496,79.500183 65.303627,80.501015 69.699097,82.166847 73.833733,82.166847 77.500458,82.166847 77.663261,84.951149 77.833694,87.16687 78.015366,89.528549 78.074593,91.371399 78.500336,93.500214 81.055862,93.500206 83.61142,93.500206 86.166939,93.500214 86.166931,97.085747 88.476021,98.766861 88.166908,102.16689 88.010994,103.882 86.652054,104.72477 86.500267,106.1669 86.289688,108.16734 88.661522,107.89798 88.500237,109.83357 87.675919,119.72533 86.765381,115.83359 93.166847,115.83359 97.932869,115.83359 95.119194,119.60738 97.166809,122.16694 100.58085,126.43452 100.16679,123.99649 100.16677,130.16696 100.16676,135.88748 102.61649,134.25766 98.833466,137.50031 96.63179,139.38745 95.017578,140.42903 92.776047,141.97011 87.779648,145.40517 93.096603,143.96761 93.096596,148.54158 94.954399,148.54158 96.19577,147.93594 97.744713,147.41963 98.354408,151.07785 99.974854,150.40535 102.23254,151.90747 106.88065,155 103.535,153.99225 103.16672,155.83366 102.95532,156.89064 102.8334,157.92091 102.8334,159.16701 103.0556,159.16701 103.27782,159.16701 103.50005,159.16701 104.93565,154.86017 108.99614,153.9635 111.5,150.83365 113.26107,148.63232 113.66137,147.68283 114.49998,145.16699 115.38509,142.51164 116.81293,143.59447 118.49996,142.16698 121.31949,139.7812 118.0918,132.90846 121.49995,129.50029 122.95245,128.04776 123.41975,125.88173 125.1666,124.8336 127.42722,123.47723 129.83679,124.33183 132.16653,123.16695 134.87071,121.81484 143.68089,121.56197 144.83313,119.8336 147.46931,115.87927 147.69608,110.56255 151.49976,108.83357 159.41605,105.23524 155.00215,88.993065 157.16641,82.500191 160.00974,82.026298 161.02483,82.379173 162.49971,80.166847 164.00684,77.90612 163.18086,75.030609 165.83302,74.500175 165.83302,74.277954 165.83302,74.055733 165.83302,73.833511 172.62648,71.569 174.34976,65.777168 172.83301,61.833485 171.36353,58.012844 173.29819,56.798942 170.00015,55.000004 167.95125,53.882408 166.05811,53.574882 164.08348,52.916672 164.0835,52.803833 160.56645,49.275555 158.6628,47.851299 156.81737,46.470615 154.42871,45.161846 154.42871,45.052463 149.50877,44.349613 147.3284,42.142094 145.3454,41.635799 143.98061,41.287342 143.90567,41.937801 140.00021,43.500004 135.77689,45.18935 138.68422,42.386234 138.16661,40.833439 137.62413,39.205929 135.06297,39.351192 133.83334,38.833431 131.42201,37.818142 127.76989,34.538395 125.00025,35.000004 122.46161,35.423115 124.18271,38.500103 121.50008,38.500103 121.50009,32.330433 123.36559,34.255459 118.83346,33.500095 112.88969,32.509457 115.47957,33.55151 114.1668,36.833431 112.26637,41.584583 112.63624,33.610561 112.8335,33.166759 113.86322,30.84985 114.465,30.128216 116.50014,28.50008 118.23673,27.11079 117.93519,27.242504 117.1668,26.166744 116.61749,25.397686 116.06577,24.587543 115.50015,23.833401 113.93788,21.750355 114.21752,18.166729 112.16685,18.166729 106.47612,18.166729 109.32634,26.623279 102.16692,24.833408 99.163582,24.082569 98.430099,20.40884 95.500313,22.166735 94.170731,22.964496 95.994263,25.100819 94.166977,26.166744 93.41832,26.603466 91.670502,26.166744 90.833679,26.166744 80.44696,26.166744 81.410843,30.052196 79.833778,22.166735 78.493813,15.466838 78.569748,13.658893 78.067711,13.721559 z";
	}
	public class IconeInutilizacao : IIcone
	{
		public string Data => "M56.813923,49.9244 L70.174004,63.284477 83.534073,49.9244 89.814598,56.204925 76.454521,69.565002 89.814598,82.925071 83.534073,89.205597 70.174004,75.84552 56.813927,89.205597 50.533402,82.925071 63.893478,69.565002 50.533402,56.204925 z M18.823773,31.416061 L20.777464,32.345779 C20.777464,32.345779 17.705145,37.586963 16.921959,41.713272 16.138695,45.839584 16.380186,49.091549 16.801878,50.05532 17.223566,51.0191 17.585064,51.921913 18.368256,52.494144 19.151362,53.066364 19.573053,53.306896 19.573053,53.306896 L17.434452,54.270755 C17.434452,54.270755 15.567013,53.427013 14.452548,49.813148 13.33808,46.19928 14.663043,41.139869 15.325756,38.820926 15.988396,36.502048 18.277838,32.135299 18.277838,32.135299 z M14.878913,30.002012 L16.892788,30.99085 C16.892788,30.99085 12.434821,38.761821 11.802286,43.219326 11.169749,47.676834 10.673169,51.747986 12.796825,54.482773 14.725021,56.965862 18.361307,56.398834 19.716654,56.188019 21.071997,55.977188 24.031559,54.603187 26.802647,52.826267 29.573734,51.04935 30.386623,50.296428 30.386623,50.296428 L30.320301,52.75653 C30.320301,52.75653 24.11532,57.243992 20.952786,57.846348 17.790251,58.448688 14.124133,57.888607 12.618163,56.804382 11.112199,55.720169 9.2079364,53.022266 9.5392552,48.053081 9.8705598,43.083904 10.417345,40.357391 12.043774,36.412162 13.670199,32.466934 14.878914,30.002012 14.878913,30.002012 z M40.470978,17.790575 C42.536076,17.790575 44.044193,18.418394 44.995331,19.674051 45.946472,20.929701 46.24149,22.686025 45.880383,24.94302 L31.605425,24.94302 C32.289402,22.813177 33.438923,21.088642 35.053986,19.769415 36.66906,18.450184 38.474724,17.790575 40.470978,17.790575 z M41.099796,14.501399 C37.640869,14.501399 34.545063,15.69733 31.812348,18.089201 29.079641,20.481067 27.394926,23.576196 26.758198,27.37458 26.084183,31.395466 26.628277,34.514438 28.390493,36.731479 30.152708,38.948524 32.900951,40.057045 36.635197,40.057045 40.455498,40.057045 43.657765,39.278309 46.242001,37.72081 L46.857414,34.049564 C44.058464,35.861343 41.170448,36.767238 38.193371,36.767238 35.543255,36.767238 33.5849,36.028213 32.318318,34.55019 31.051724,33.072155 30.611792,30.974308 30.998524,28.256633 L49.661205,28.256633 50.004879,26.206463 C50.620277,22.535213 50.15469,19.666567 48.60809,17.600498 47.061474,15.534432 44.558712,14.501399 41.099796,14.501399 z M56.591549,2.3336358 C56.591549,2.3336336 59.746037,2.7749416 61.715252,6.3111102 63.01268,8.6409051 62.297649,14.245817 62.297649,14.245818 L60.248413,13.060317 C60.248413,13.060317 60.448818,12.337468 60.308205,9.5452573 60.167584,6.7530386 57.91769,5.066292 57.91769,5.0662915 z M48.681236,1.5019153 C49.133534,1.4975343 49.597893,1.5201318 50.069706,1.5774446 52.586052,1.8831352 54.749657,3.1528365 56.066223,5.1046044 57.415608,7.1050074 57.241829,11.711824 57.241829,11.711828 L54.937733,10.936233 C54.937733,10.936234 55.08408,10.11076 55.008534,9.2663552 54.867615,7.6911566 54.302685,6.2566673 53.738285,5.6453239 53.173866,5.0339834 52.42144,3.9523329 49.387714,3.717247 46.492687,3.4929089 42.285645,5.668595 42.285645,5.6685936 L41.298069,3.5060339 C41.298069,3.5060343 42.473633,2.9413542 43.861149,2.4475555 44.98851,2.0463433 46.721359,1.5208931 48.681236,1.5019153 z M37.26511,0.9542578 L38.383988,1.6999684 C38.383988,1.699971 38.446205,2.2797727 38.446205,2.6524651 38.446205,3.0251681 38.446678,3.6671245 38.612393,3.9570269 38.778027,4.2469303 38.90263,4.5161125 39.130406,4.702419 39.358185,4.8888099 39.751442,5.137273 39.751442,5.137273 39.751442,5.137273 39.916996,5.2822263 39.792786,5.5928475 39.668503,5.903465 39.772148,6.1104934 39.523663,6.4832704 39.275169,6.8559672 39.295883,6.8766853 39.047394,7.1458671 38.798897,7.4150493 38.819618,7.1665861 38.653904,7.6427948 38.48827,8.119086 38.343262,8.4089868 38.281208,8.6988885 38.219059,8.9887101 38.053421,9.4235665 38.053421,9.4857213 38.053421,9.5477998 38.736759,9.5892369 38.736759,9.589235 38.736759,9.5892369 39.109535,9.1336591 39.192318,8.9059169 39.275169,8.6781699 39.275169,8.4710633 39.316597,8.4089858 39.358021,8.3468329 39.378738,8.1811635 39.627232,8.201883 39.875725,8.2226007 40.331276,8.2847564 40.84898,8.3675496 41.366669,8.4503467 41.615715,8.4296281 41.781353,8.4089858 41.947067,8.3882701 42.154205,8.3675525 42.154205,8.3675496 42.154205,8.3675525 41.864212,9.2993352 41.760632,9.5685145 41.657143,9.8376996 41.512138,10.065523 41.491425,10.231113 41.470722,10.396784 41.739922,10.707407 41.822785,10.665963 41.90564,10.624611 42.402615,10.458936 42.506191,10.272551 42.60968,10.086241 43.002777,9.8584182 43.12706,9.6306712 43.251267,9.402847 43.499531,9.1751 43.561676,9.1130273 43.623814,9.0508658 43.99651,8.8231198 44.369289,8.9473531 44.741985,9.0715825 46.129524,9.5271604 46.336586,9.6306712 46.543648,9.734184 46.99905,10.044802 47.330402,10.210474 47.661759,10.376064 47.910011,10.583175 48.199921,10.583168 48.489845,10.583175 48.635006,10.479662 48.717861,10.624612 48.800716,10.769561 49.235252,11.618467 49.235252,11.784132 49.235252,11.949809 49.339214,12.301864 49.422066,12.343222 49.504917,12.38466 49.836582,12.529609 49.960869,12.571046 50.085072,12.612405 50.623482,12.508891 50.830631,12.446737 51.037701,12.38466 51.183086,12.260429 51.597286,12.467455 52.011421,12.674561 52.881413,13.026538 53.067841,13.026538 53.254185,13.026538 54.144028,13.171488 54.392536,13.337158 54.641022,13.502827 55.262444,13.440675 55.490215,13.419955 55.718086,13.399316 56.297367,13.212926 56.628727,13.337158 56.960064,13.461394 57.995934,14.165426 58.410065,14.434608 58.824261,14.703795 59.362995,15.283595 59.797829,15.573497 60.232754,15.863397 60.460754,16.236101 61.019886,16.629513 61.579029,17.022928 62.262825,17.395704 62.532024,17.499218 62.801239,17.602733 64.354164,17.582016 64.66481,18.658825 64.975441,19.735555 65.078384,21.019394 64.995522,21.702711 64.912682,22.38603 64.870689,22.841604 64.76712,23.152145 64.663628,23.462764 64.601494,23.897932 64.311569,24.084242 64.02166,24.270626 62.944809,25.367923 61.930138,26.27907 60.915371,27.190124 60.480541,28.328859 60.190621,28.598041 59.9007,28.867223 59.962833,28.992088 59.590057,29.344148 59.217362,29.696123 59.445145,29.696439 58.989586,29.924272 58.533951,30.15201 58.036957,30.235046 57.892048,30.44207 57.747044,30.649181 57.519264,30.710861 57.581406,31.042196 57.643559,31.373543 57.415707,33.423092 57.5607,34.313514 57.705628,35.203933 57.436409,36.094452 57.31221,36.322185 57.18792,36.54993 57.291504,36.674328 57.001572,37.295562 56.711655,37.916809 56.6288,38.144558 56.463165,38.537975 56.297451,38.931385 56.23539,39.014496 56.069676,39.221595 55.904038,39.428623 55.779755,39.718052 55.779755,39.966594 55.779755,40.215061 56.090389,40.526161 55.821182,40.857494 55.551968,41.188759 55.096428,42.286049 54.827221,42.493076 54.55801,42.70018 54.454434,42.617309 54.123097,43.134964 53.791813,43.652691 53.688252,43.901378 53.605389,44.2948 53.522614,44.688209 53.708965,44.978115 53.439743,45.226665 53.170551,45.475124 52.694279,45.764637 52.507855,45.868152 52.321499,45.971668 52.21801,46.012935 52.031582,46.261413 51.845234,46.50988 52.010861,46.654667 51.555309,46.696014 51.099766,46.737461 51.079044,46.7789 50.51992,46.737461 49.960796,46.696014 49.753735,46.612671 49.360237,46.633388 48.966824,46.654113 48.697628,46.633388 48.304131,46.819698 47.910633,47.00608 47.889908,47.04697 47.600071,47.150562 47.31015,47.254081 47.31015,47.047359 46.792461,47.420139 46.274685,47.792835 46.088341,47.999473 45.79842,48.061546 45.508492,48.123699 45.446434,47.916439 45.073647,48.123462 44.700874,48.330566 44.307461,48.662063 44.245319,48.765575 44.183178,48.869095 43.541275,49.200912 43.25135,49.49081 42.96143,49.780708 42.94072,50.008453 42.630085,50.298359 42.319447,50.588268 42.050243,50.608982 41.760323,50.816089 41.470406,51.023117 41.325478,51.022636 41.242622,51.416053 41.159763,51.809551 40.973415,52.285202 40.973415,52.616554 40.973415,52.947811 41.118343,53.506416 41.118343,53.817039 41.118343,54.127578 41.201195,55.162483 40.86985,55.431659 40.538582,55.700844 40.497143,55.638382 40.310726,55.845401 40.124371,56.052509 39.95874,56.01075 39.730957,56.507694 39.503101,57.004615 39.233902,57.439541 39.130406,57.708729 39.02684,57.977909 39.006123,58.37149 38.654141,58.702751 38.302067,59.034008 38.15707,59.344703 37.929302,59.717399 37.701511,60.090172 37.701511,60.25592 37.432312,60.463031 37.163105,60.670059 37.121681,61.001243 36.852463,61.104763 36.583267,61.208271 36.376206,61.166992 36.127716,61.353382 35.879219,61.539688 35.506443,61.871498 35.382229,62.202755 35.257946,62.534103 35.154377,62.864795 34.968025,63.113262 34.781681,63.36174 34.533188,63.486206 34.367474,63.672504 34.20184,63.858898 34.036202,64.127998 34.036202,64.127998 34.036202,64.127998 33.601284,63.589966 33.663426,63.362213 33.725575,63.134391 34.036202,63.093021 34.098274,62.637447 34.160412,62.181953 34.243267,62.202675 34.077553,62.057731 33.911915,61.912777 33.663426,61.622867 33.373505,61.457203 33.083592,61.291614 33.042164,61.270897 32.814377,61.125938 32.586605,60.980999 32.565888,60.898197 32.068901,60.670376 31.57192,60.442634 31.40621,59.759304 31.219858,59.573002 31.033512,59.386608 30.909226,58.972477 30.66073,59.221019 30.412239,59.469482 30.495094,59.67651 30.184462,59.593715 29.873825,59.510841 29.873825,59.531559 29.583906,59.117431 29.293985,58.703376 29.563194,58.724018 29.169781,58.620506 28.776291,58.516987 28.444943,58.372036 28.444943,58.372036 28.444943,58.372036 28.879862,57.440258 29.128351,56.964042 29.376843,56.487831 30.018747,55.94947 30.080891,55.866596 30.14303,55.783794 30.867798,55.1212 31.157719,54.852024 31.447638,54.58284 31.903193,54.39653 32.089619,54.210132 32.275971,54.023815 32.565888,53.878868 32.628037,53.692482 32.690174,53.50618 32.814377,53.257717 32.752243,53.216278 32.690174,53.174839 32.462319,53.112766 32.462319,53.112766 L32.42096,47.770618 30.992083,47.770618 C30.992083,47.770618 30.992083,46.300472 30.826368,46.093445 30.66073,45.886341 30.72287,46.134884 30.640017,45.803539 30.55716,45.472271 30.515812,44.892475 30.412239,44.892475 30.308666,44.892475 27.2232,45.058147 27.2232,45.058147 27.2232,45.058147 27.078194,42.946121 27.098915,42.594135 27.119625,42.242161 27.326687,42.387024 27.119625,42.159286 26.912558,41.931541 26.726214,41.765873 26.726214,41.53812 26.726214,41.310379 26.788273,40.979042 26.850418,40.751297 26.912558,40.523555 26.953989,39.943748 27.078194,39.736725 27.202482,39.529697 27.347399,39.239788 27.388834,39.074119 27.430258,38.908447 27.678747,37.976742 27.05748,37.459102 26.436295,36.941463 26.477642,37.003532 26.353438,36.71363 26.229149,36.423797 26.125654,36.527324 26.104944,36.092468 26.084231,35.657608 26.187801,35.222839 25.918594,35.1814 25.649386,35.139969 25.297325,35.036457 24.821055,35.119323 24.344706,35.202121 24.158358,35.222839 23.827011,35.222839 23.495657,35.222839 23.226452,35.222839 23.226452,35.222839 23.226452,35.222839 23.019394,34.91222 22.977963,34.663761 22.936611,34.415291 22.750181,34.311779 22.791609,33.939087 22.83304,33.566383 22.915897,33.400718 22.936611,33.110813 22.957247,32.820911 22.729469,31.827057 22.64669,31.516426 22.563839,31.205888 22.563839,31.060943 22.27392,30.874551 21.983996,30.688244 22.066853,30.564012 21.507727,30.460499 20.948603,30.356909 20.327337,30.274114 20.099556,30.232673 19.871779,30.191313 19.540432,30.108442 19.374792,30.067083 19.209078,30.025648 18.67067,30.108442 18.401465,29.715023 18.132258,29.321613 18.028761,29.114586 17.883764,29.031784 17.738842,28.948913 17.904478,28.783325 17.221144,28.907554 16.537735,29.031784 16.330671,29.218099 16.061464,29.031784 15.79226,28.845405 15.253843,28.01713 14.839641,27.520204 14.42551,27.023269 14.446229,27.126783 14.384086,26.712732 14.321941,26.298601 14.342654,25.884468 14.280516,25.532482 14.21845,25.180427 14.280515,24.372957 14.073448,24.372957 13.866387,24.372957 13.680042,24.600706 13.120911,24.6835 12.561783,24.76637 12.106233,24.621422 11.795594,25.076915 11.484962,25.532482 11.298532,26.029417 10.863689,26.236442 10.428852,26.443546 10.428852,26.298601 9.7868691,26.609136 9.1448861,26.919758 9.0828219,27.147501 8.4408399,27.126783 7.7988559,27.106065 7.902423,27.147501 7.1776677,27.064705 6.4528252,26.981834 6.1836215,26.878324 5.9765583,26.919758 5.7694951,26.961195 5.7073523,26.443546 5.6038567,26.153646 5.5002833,25.863744 5.4174305,25.635996 5.4795743,25.284023 5.5417143,24.931966 5.5417143,24.290083 5.5417148,24.290083 5.5417143,24.290083 5.251793,24.6835 4.5891019,24.890606 3.9264811,25.097631 3.3880688,25.242582 3.180924,25.36681 2.9738585,25.491049 3.0152863,25.739515 2.8082241,25.36681 2.6011628,24.994116 2.8289412,24.538545 2.3319577,24.579981 1.8348944,24.621422 1.6071158,24.579981 1.56569,24.228012 1.5243389,23.875946 1.5036251,23.296226 1.2551292,23.068483 1.0065613,22.84065 0.75861815,22.757856 0.46869809,22.281645 0.1787801,21.805361 -0.15193946,21.805361 0.075834555,21.287706 0.30361501,20.770061 0.24084272,20.62519 0.61354146,20.376648 0.98631868,20.128181 1.5040968,19.776756 1.5040968,19.776756 L5.0642706,19.839786 C5.0642701,19.839786 6.3278357,19.487251 6.659103,18.720817 6.9903718,17.954475 7.0116387,17.933914 7.0116383,17.933914 7.0116387,17.933914 7.7366353,19.777231 9.2482968,19.81867 10.759967,19.860105 14.550267,19.860105 14.550267,19.860105 14.550267,19.860105 14.964238,19.549246 15.088527,19.300705 15.21281,19.05224 15.274325,18.783058 15.274325,18.783058 15.274325,18.783058 15.273696,19.901228 16.267582,19.901228 17.261553,19.901228 21.175905,19.901228 21.175905,19.901228 21.175905,19.901228 22.873921,19.445652 23.350185,18.306846 23.82654,17.167961 24.592491,13.088776 24.592491,13.088776 L29.997326,13.088776 C29.997326,13.088776 31.944069,12.985102 32.544544,11.28713 33.145104,9.5892369 33.496929,7.6218411 33.496929,7.6218407 L33.703522,6.1924178 C33.703522,6.1924178 33.703133,5.3025491 33.806622,5.1575973 33.910194,5.0126483 33.972488,4.9508078 34.117497,4.8265731 34.262409,4.7023417 34.469158,4.0184738 34.572739,3.9977541 34.676224,3.9770376 34.945511,3.9568681 35.007572,3.9155156 35.069721,3.8740739 35.918919,3.0251681 36.105343,2.6317527 36.291695,2.2382566 36.415588,2.1135492 36.643372,1.761492 36.871067,1.4095157 37.26511,0.9542606 37.26511,0.9542578 z M20.338316,0 L20.339684,0 23.188042,0 31.366545,0 30.88316,2.4430117 22.704653,2.4430117 21.890793,6.556257 30.048456,6.556257 29.565075,8.9992663 21.407408,8.9992663 20.055458,15.831969 17.207098,15.831969 18.559048,8.9992663 18.536846,8.9992663 19.020226,6.556257 19.042433,6.556257 19.8563,2.4430117 19.854927,2.4430117 z M4.1190387,0 L4.1569968,0 6.9674932,0 7.0054499,0 11.561208,11.444045 13.825659,0 16.674114,0 13.541426,15.831969 13.307995,15.831969 10.692966,15.831969 10.459536,15.831969 6.0343519,4.7159178 3.8348058,15.831969 0.98634461,15.831969 z ";
	}
	public class IconeJornadaTrabalho : IIcone
	{
		public string Data => "M52.323998,46.593136 L52.323998,49.431446 C52.323998,51.004109 51.049107,52.279 49.476444,52.279 L46.615219,52.279 47.033714,52.038368 C47.655956,51.660286 48.248688,51.238465 48.80764,50.777176 L49.06163,50.557167 49.07618,50.554947 C49.627575,50.442116 50.071629,50.034607 50.236622,49.504151 L50.26329,49.400425 50.378891,49.279175 C51.028393,48.564564 51.610256,47.787373 52.114368,46.957714 z M13.218604,41.405819 L13.218604,48.180847 21.305946,48.180847 21.305946,41.405819 z M4.2539582,41.405819 L4.2539582,48.180847 12.341303,48.180847 12.341303,41.405819 z M13.218604,33.872242 L13.218604,40.64727 21.305946,40.64727 21.305946,33.872242 z M4.2539582,33.872242 L4.2539582,40.64727 12.341303,40.64727 12.341303,33.872242 z M38.669998,27.129 C37.950375,27.129 37.367001,27.712374 37.367001,28.431999 L37.367001,40.195 C37.367001,40.419884 37.423969,40.631462 37.524265,40.81609 L37.527393,40.82153 37.531265,40.829087 C37.751915,41.235268 38.182255,41.511002 38.676998,41.511002 L48.372002,41.511002 C49.091629,41.511002 49.675003,40.927628 49.675003,40.208 L49.675003,40.141003 C49.675003,39.421375 49.091629,38.838001 48.372002,38.838001 L40.040001,38.838001 40.040001,28.431999 C40.040001,27.712374 39.456627,27.129 38.737,27.129 z M13.218604,26.438477 L13.218604,33.213505 21.305946,33.213505 21.305946,26.438477 z M4.2539582,26.438477 L4.2539582,33.213505 12.341303,33.213505 12.341303,26.438477 z M39.209,25.987 C46.469337,25.987 52.355,31.872666 52.355,39.133 52.355,46.393337 46.469337,52.279 39.209,52.279 31.948666,52.279 26.063,46.393337 26.063,39.133 26.063,31.872666 31.948666,25.987 39.209,25.987 z M22.131351,18.9049 L22.131351,25.679926 30.218695,25.679926 30.218695,18.9049 z M13.218604,18.9049 L13.218604,25.679926 21.305946,25.679926 21.305946,18.9049 z M4.2539582,18.9049 L4.2539582,25.679926 12.341303,25.679926 12.341303,18.9049 z M3.4504952,18.149348 L48.921509,18.149348 48.921509,27.587456 48.80764,27.488821 C48.621323,27.335058 48.431252,27.185683 48.237587,27.040848 L48.118046,26.955839 48.118046,26.438477 47.362606,26.438477 47.033714,26.227629 C46.826298,26.101601 46.615604,25.980433 46.401791,25.864281 L46.041843,25.679926 48.118046,25.679926 48.118046,18.9049 40.030701,18.9049 40.030701,24.065497 39.985531,24.062634 C39.728333,24.049597 39.469437,24.042999 39.209,24.042999 L39.130444,24.043993 39.130444,18.9049 31.043098,18.9049 31.043098,25.679926 32.376156,25.679926 32.016209,25.864281 C31.802395,25.980433 31.5917,26.101601 31.384285,26.227629 L31.055395,26.438477 31.043098,26.438477 31.043098,26.44636 30.772038,26.620135 30.218695,27.013624 30.218695,26.438477 22.131351,26.438477 22.131351,33.213505 25.325544,33.213505 25.304846,33.259289 C25.257122,33.372124 25.21073,33.485661 25.165691,33.599877 L25.062147,33.872242 22.131351,33.872242 22.131351,40.64727 24.194368,40.64727 24.196907,40.675865 C24.209787,40.802685 24.224237,40.929039 24.240236,41.054913 L24.289337,41.405819 22.131351,41.405819 22.131351,48.180847 27.131983,48.180847 27.564821,48.73164 27.743052,48.937397 3.4504952,48.937397 z M2.8975523,5.3688171 L8.9819136,5.3688171 8.9819136,9.5977175 C8.9819136,11.536944 10.553967,13.108997 12.493196,13.108997 L13.462341,13.108997 C15.401569,13.108997 16.973625,11.536944 16.973625,9.5977175 L16.973625,5.3688171 35.44529,5.3688171 35.44529,9.5977175 C35.44529,11.536944 37.017342,13.108997 38.956573,13.108997 L39.925716,13.108997 C41.864944,13.108997 43.437,11.536944 43.437,9.5977175 L43.437,5.3688171 49.476444,5.3688171 C51.049107,5.3688171 52.323998,6.643708 52.323998,8.2163641 L52.323998,31.672863 52.114368,31.308285 C51.610256,30.478626 51.028393,29.701435 50.378891,28.986824 L50.305859,28.910221 50.305859,18.202244 C50.305859,17.351698 49.616352,16.662194 48.765804,16.662194 L3.6081929,16.662194 C2.7576449,16.662194 2.0681393,17.351698 2.0681388,18.202244 L2.0681388,49.046185 C2.0681393,49.896733 2.7576449,50.586235 3.6081929,50.586235 L29.389927,50.586235 29.610359,50.777176 C30.16931,51.238465 30.762041,51.660286 31.384285,52.038368 L31.80278,52.279 2.8975523,52.279 C1.324893,52.279 0.050000317,51.004109 0.05,49.431446 L0.05,8.2163641 C0.050000317,6.643708 1.324893,5.3688171 2.8975523,5.3688171 z M39.062462,0.05 L39.774078,0.05 C41.023129,0.050001099 42.035683,1.0625853 42.035683,2.3116703 L42.035683,9.515876 C42.035683,10.764962 41.023129,11.777546 39.774078,11.777546 L39.062462,11.777546 C37.813412,11.777546 36.800861,10.764962 36.800861,9.515876 L36.800861,2.3116703 C36.800861,1.0625853 37.813412,0.050001099 39.062462,0.05 z M12.599921,0.05 L13.311537,0.05 C14.560586,0.050001099 15.573139,1.0625853 15.573139,2.3116703 L15.573139,9.515876 C15.573139,10.764962 14.560586,11.777546 13.311537,11.777546 L12.599921,11.777546 C11.350872,11.777546 10.338318,10.764962 10.338318,9.515876 L10.338318,2.3116703 C10.338318,1.0625853 11.350872,0.050001099 12.599921,0.05 z";
	}
	public class IconeLivroMovimentacaoCombustivel : IIcone
	{
		public string Data => "M6.9385546,54.338409 L14.051046,54.338409 53.554462,54.338409 56.440357,54.338409 56.440357,56.707867 56.440357,59.077324 53.554462,59.077324 14.051046,59.077324 6.9385546,59.077324 C5.3447206,59.077324 4.0526627,58.016483 4.0526622,56.707867 4.0526627,55.399254 5.3447206,54.338409 6.9385546,54.338409 z M23.691999,15.429999 L23.691999,38.140999 26.657999,38.140999 26.657999,24.106777 28.895229,38.140999 31.861231,38.140999 34.147999,23.615963 34.147999,38.140999 37.113998,38.140999 37.113998,15.429999 35.436767,15.429999 34.147999,15.429999 32.470768,15.429999 30.368961,28.780191 28.240883,15.429999 26.657999,15.429999 25.274883,15.429999 z M12.555,15.429999 L12.555,33.716 12.555,38.140999 15.521,38.140999 22.038,38.140999 22.038,33.716 15.521,33.716 15.521,15.429999 z M44.967853,15.169 C41.409012,15.169 38.523998,20.326231 38.523998,26.688 38.523998,33.049767 41.409012,38.207001 44.967853,38.207001 47.636989,38.207001 49.927093,35.306057 50.905323,31.171713 L50.946998,30.989372 47.727623,29.599178 47.700821,29.743139 C47.198078,32.276676 46.13055,34.025002 44.894855,34.025002 43.175632,34.025002 41.781925,30.64072 41.781925,26.466 41.781925,22.291279 43.175632,18.907 44.894855,18.907 46.076824,18.907 47.104931,20.506599 47.632072,22.86293 L47.653648,22.965216 50.790642,21.750828 50.776279,21.694036 C49.735592,17.833233 47.525776,15.169 44.967853,15.169 z M5.9951804,0.32460052 L6.1444853,53.114971 C6.1444853,53.114971 4.6084502,53.38895 3.6209958,54.212086 2.6335423,55.035221 2.5239418,56.625896 2.9079383,58.107822 3.2919352,59.589748 5.9250181,60.413227 5.9250181,60.413227 L5.9256466,60.415489 5.9256466,60.357971 56.569999,60.357971 56.569999,61.411999 5.9256466,61.411999 5.9256466,61.374626 5.9002183,61.371292 C5.7348168,61.348572 5.4932783,61.308121 5.157224,61.236103 4.3890994,61.071491 3.4564612,60.797138 1.7008038,59.370499 -0.054854223,57.943863 0.0001918593,52.511665 0.00019162051,52.511665 L0.00019162051,7.0067344 C0.019272504,3.040915 1.9750241,0.7050423 5.9951804,0.32460052 z M45.247551,1.885527E-14 L53.987545,1.885527E-14 C55.738537,-1.8890791E-07 57.157997,1.4194783 57.157997,3.1704931 L57.157997,49.454643 C57.157997,51.205658 55.738537,52.625141 53.987545,52.625141 L18.545481,52.625141 18.545481,52.720001 7.9390004,52.720001 7.9390004,0.10712736 z";
	}
	public class IconeManifestacaoDestinatario : IIcone
	{
		public string Data => "M19.570341,19.85401 L19.331903,20.168139 C19.331903,20.168139 18.331982,22.075325 18.042572,23.088095 17.75313,24.100899 17.174448,26.310604 17.661196,27.888966 18.147942,29.467327 18.963552,29.835831 18.963552,29.835831 L19.897593,29.414867 C19.897593,29.414867 19.713417,29.309811 19.371393,29.059893 19.029331,28.809973 18.871447,28.415667 18.687273,27.994736 18.503098,27.573807 18.397625,26.153505 18.739719,24.351332 19.081778,22.549162 20.423622,20.260067 20.423622,20.260067 z M17.847412,19.236422 C17.847412,19.236422 17.319502,20.312981 16.609156,22.036066 15.898808,23.759148 15.659998,24.949959 15.515299,27.120253 15.370595,29.290552 16.202288,30.468866 16.860023,30.942398 17.517759,31.415932 19.118948,31.660551 20.500195,31.397478 21.881441,31.134399 24.591486,29.174492 24.591486,29.174492 L24.620453,28.100037 C24.620453,28.100037 24.265421,28.428879 23.05514,29.20495 21.844858,29.981022 20.552261,30.581121 19.96031,30.673199 19.368359,30.765274 17.780199,31.012924 16.938053,29.928432 16.01054,28.734009 16.227423,26.955927 16.503685,25.009104 16.779947,23.062281 18.726978,19.668299 18.726978,19.668299 z M29.024826,13.903059 C28.152956,13.903059 27.364326,14.191145 26.658939,14.767321 25.953554,15.343496 25.451498,16.096689 25.152769,17.026901 L31.3874,17.026901 C31.545116,16.041155 31.416265,15.274078 31.000851,14.725671 30.585438,14.177261 29.926764,13.903059 29.024826,13.903059 z M29.299465,12.466507 C30.810156,12.466507 31.903246,12.917686 32.578735,13.820043 33.254219,14.722402 33.457565,15.975288 33.188786,17.578711 L33.038685,18.474125 24.887703,18.474125 C24.718796,19.661074 24.910938,20.577312 25.464125,21.222847 26.017311,21.868376 26.872629,22.191145 28.030075,22.191145 29.330322,22.191145 30.591673,21.795496 31.814123,21.004198 L31.545338,22.607618 C30.416666,23.287859 29.018066,23.627974 27.349537,23.627974 25.718592,23.627974 24.51829,23.143824 23.748636,22.175528 22.978983,21.20723 22.741348,19.845016 23.035727,18.088888 23.31382,16.429939 24.049625,15.078137 25.243143,14.033485 26.436663,12.988832 27.788767,12.466507 29.299465,12.466507 z M36.065536,7.1522179 L36.644733,8.3457098 C36.644733,8.3457098 37.62738,9.0823994 37.688797,10.301905 37.75021,11.521407 37.662682,11.837112 37.662682,11.837112 L38.557693,12.354881 C38.557693,12.354881 38.869984,9.9069281 38.303329,8.8893862 37.443268,7.3449593 36.065536,7.1522174 36.065536,7.1522179 z M32.610683,6.7889633 C31.754698,6.7972517 30.997873,7.0267429 30.505495,7.201973 29.899492,7.4176402 29.386059,7.6642652 29.386059,7.6642647 L29.817387,8.6087666 C29.817387,8.6087666 31.654827,7.6585326 32.919239,7.7565126 34.244228,7.8591866 34.572853,8.3315992 34.819363,8.5986032 35.065868,8.8656082 35.312603,9.4921236 35.374149,10.180094 35.407143,10.548889 35.343227,10.909416 35.343227,10.909416 L36.349548,11.248158 C36.349548,11.248156 36.425446,9.2361221 35.836098,8.362443 35.261082,7.5100055 34.316124,6.9554615 33.217102,6.8219509 33.011036,6.7969193 32.808224,6.7870498 32.610683,6.7889633 z M27.624653,6.5497732 C27.624653,6.5497742 27.452553,6.7486076 27.353106,6.9023337 27.253622,7.0560951 27.199512,7.1105614 27.118122,7.2824211 27.036701,7.454246 26.66581,7.825007 26.638668,7.8431063 26.611561,7.8611674 26.49395,7.8699765 26.44875,7.8790245 26.403513,7.8880739 26.313215,8.1867542 26.249922,8.2410126 26.186592,8.2952719 26.159382,8.3222809 26.114149,8.3855877 26.068947,8.4488955 26.069118,8.8375473 26.069118,8.8375473 L25.978888,9.4618502 C25.978888,9.4618502 25.825226,10.321113 25.562933,11.06267 25.300671,11.804262 24.450426,11.849542 24.450426,11.849542 L22.089848,11.849542 C22.089848,11.849542 21.755316,13.631131 21.547266,14.128541 21.339256,14.625917 20.597643,14.82489 20.597643,14.82489 20.597643,14.82489 18.888037,14.82489 18.453918,14.82489 18.019835,14.82489 18.020109,14.336528 18.020109,14.336528 18.020109,14.336528 17.993242,14.454094 17.938961,14.562611 17.884678,14.671162 17.703875,14.80693 17.703875,14.80693 17.703875,14.80693 16.048449,14.80693 15.388223,14.788833 14.728,14.770735 14.411355,13.965663 14.411355,13.965663 14.411355,13.965663 14.402067,13.974643 14.257384,14.309345 14.112702,14.644085 13.560836,14.798056 13.560836,14.798056 L12.005919,14.770527 C12.005919,14.770527 11.779778,14.924012 11.616966,15.032531 11.454189,15.141082 11.481605,15.204355 11.382121,15.430437 11.28264,15.656523 11.427083,15.656523 11.553705,15.864541 11.680329,16.072527 11.788619,16.108688 11.897182,16.208193 12.005713,16.307661 12.01476,16.560854 12.03282,16.714619 12.050913,16.868343 12.150396,16.886442 12.36749,16.868343 12.584549,16.850246 12.485066,17.049217 12.575501,17.211992 12.665936,17.374771 12.647842,17.266253 12.738279,17.211992 12.82875,17.157734 13.063903,17.094427 13.353304,17.004009 13.642737,16.913555 13.769361,16.741728 13.769361,16.741728 13.769361,16.741728 13.769361,17.022072 13.742221,17.175833 13.71508,17.329559 13.751266,17.429029 13.796502,17.555643 13.841704,17.682257 13.868845,17.908342 13.95928,17.890244 14.049716,17.872147 14.167292,17.917356 14.483869,17.95355 14.800408,17.98971 14.755175,17.971613 15.035563,17.980663 15.315951,17.98971 15.343058,17.890244 15.623446,17.75458 15.903834,17.618952 15.903834,17.682257 16.09375,17.591805 16.283669,17.501385 16.365093,17.284349 16.500763,17.085379 16.636435,16.886442 16.835398,16.949749 17.079599,16.913555 17.323801,16.877394 17.405188,16.777925 17.495623,16.777925 17.58606,16.777925 17.558952,17.130589 17.58606,17.284349 17.613199,17.438078 17.604153,17.618952 17.631294,17.799824 17.658436,17.980663 17.649387,17.935453 17.830259,18.152489 18.011164,18.369522 18.246319,18.731272 18.363895,18.812675 18.481472,18.894047 18.571907,18.812675 18.870388,18.758417 19.168837,18.704159 19.096495,18.77648 19.15979,18.812675 19.223118,18.848837 19.26832,18.939257 19.385897,19.11108 19.503473,19.282906 19.738625,19.246746 19.811001,19.264843 19.883345,19.282906 20.028061,19.319101 20.127544,19.337164 20.227028,19.355263 20.498367,19.391424 20.742567,19.436668 20.986767,19.481878 20.950579,19.536135 21.077204,19.617506 21.203827,19.698912 21.203827,19.762218 21.240013,19.897846 21.276167,20.033514 21.37565,20.467581 21.366636,20.594196 21.35759,20.720812 21.321402,20.793167 21.303307,20.955946 21.285213,21.118721 21.366636,21.163929 21.384697,21.272448 21.402792,21.380964 21.493225,21.516628 21.493225,21.516628 21.493225,21.516628 21.610802,21.516628 21.755522,21.516628 21.900238,21.516628 21.981628,21.507578 22.189674,21.471416 22.397686,21.435223 22.551449,21.480433 22.669027,21.498529 22.786604,21.516628 22.741369,21.706514 22.750416,21.896437 22.759462,22.086363 22.804663,22.041147 22.858946,22.167732 22.913193,22.294348 22.895134,22.267237 23.166439,22.493319 23.43778,22.7194 23.32925,23.126322 23.311159,23.198679 23.293062,23.271034 23.229769,23.397654 23.175486,23.488073 23.121239,23.578491 23.103144,23.831722 23.076004,23.931189 23.048862,24.030657 23.021757,24.175369 23.021757,24.274837 23.021757,24.374306 23.103144,24.446663 23.193581,24.546131 23.284016,24.645597 23.193581,24.582325 23.184536,24.73605 23.175486,24.889782 23.238817,25.812212 23.238817,25.812212 23.238817,25.812212 24.586405,25.739855 24.631641,25.739855 24.676876,25.739855 24.694935,25.993082 24.731123,26.137764 24.767309,26.282478 24.74017,26.173927 24.812513,26.264381 24.884888,26.354799 24.884888,26.996887 24.884888,26.996887 L25.508957,26.996887 25.52702,29.330078 C25.52702,29.330078 25.626537,29.357189 25.653643,29.375288 25.680782,29.393387 25.626537,29.501904 25.599396,29.583271 25.572254,29.664675 25.445631,29.727983 25.364241,29.809355 25.282818,29.890764 25.083853,29.972136 24.95723,30.089701 24.830608,30.207266 24.514063,30.496655 24.486923,30.532818 24.459782,30.569012 24.179428,30.804144 24.070898,31.012129 23.96237,31.220119 23.772417,31.627073 23.772417,31.627073 23.772417,31.627073 23.917135,31.69038 24.088993,31.735592 24.260818,31.780802 24.14324,31.771786 24.269863,31.952625 24.396486,32.133495 24.396486,32.124447 24.532158,32.160641 24.667828,32.196804 24.631641,32.106384 24.74017,31.997868 24.848701,31.889318 24.902983,32.07019 24.984371,32.151596 25.06576,32.232964 25.138136,32.53141 25.355194,32.630878 25.572254,32.730377 25.581301,32.766541 25.680782,32.829845 25.780268,32.893154 25.798361,32.902203 25.924984,32.974525 26.051607,33.046879 26.160135,33.173496 26.232479,33.236805 26.304855,33.30011 26.268667,33.291061 26.241528,33.489998 26.214417,33.688972 26.078749,33.707039 26.051607,33.806541 26.024467,33.90601 26.214417,34.140999 26.214417,34.140999 26.214417,34.140999 26.28676,34.023468 26.359102,33.942062 26.431477,33.860695 26.540007,33.806332 26.621395,33.697811 26.702785,33.589294 26.748018,33.444862 26.802299,33.300144 26.85655,33.155468 27.01936,33.010548 27.127892,32.92918 27.23642,32.847775 27.326855,32.865803 27.444429,32.820595 27.56201,32.775383 27.580103,32.630737 27.697678,32.540318 27.815254,32.449863 27.815254,32.377472 27.91474,32.214661 28.014221,32.051888 28.077549,31.916189 28.231316,31.771513 28.385046,31.626835 28.394093,31.454937 28.439327,31.337372 28.484528,31.219805 28.602104,31.02985 28.70162,30.812817 28.801104,30.595778 28.873444,30.614016 28.954836,30.523563 29.036255,30.433144 29.054352,30.460426 29.199034,30.342859 29.343752,30.225296 29.307564,29.7733 29.307564,29.637671 29.307564,29.502007 29.244268,29.258034 29.244268,29.113356 29.244268,28.968639 29.325657,28.760899 29.361845,28.589037 29.398033,28.417212 29.461329,28.417421 29.58795,28.327003 29.714575,28.236549 29.83215,28.227501 29.967823,28.100882 30.103493,27.974266 30.112539,27.874798 30.239161,27.748184 30.365788,27.621571 30.646141,27.476648 30.673281,27.431438 30.700422,27.386227 30.872246,27.241447 31.035057,27.150993 31.197872,27.060577 31.224977,27.151096 31.351603,27.123951 31.478226,27.09684 31.559614,27.006592 31.785755,26.843815 32.011856,26.681005 32.011856,26.77129 32.138481,26.726078 32.265068,26.680834 32.27412,26.662975 32.44598,26.581572 32.61784,26.5002 32.735413,26.509251 32.907238,26.5002 33.079098,26.491152 33.169533,26.527555 33.413734,26.545654 33.657932,26.563753 33.666981,26.545654 33.865944,26.527555 34.064907,26.509495 33.992569,26.446257 34.073956,26.33774 34.15538,26.229218 34.200577,26.211193 34.281971,26.165983 34.363392,26.120773 34.571404,25.994329 34.688976,25.885813 34.806557,25.777258 34.72517,25.650642 34.761322,25.478819 34.797512,25.306993 34.842743,25.198376 34.987431,24.972258 35.132145,24.746172 35.17738,24.782366 35.29496,24.691912 35.412537,24.601494 35.611496,24.122252 35.729076,23.977573 35.846653,23.832861 35.710983,23.696987 35.710983,23.58847 35.710983,23.479919 35.765263,23.35351 35.837605,23.26309 35.909981,23.172638 35.937088,23.136339 36.009464,22.964516 36.081806,22.792692 36.117992,22.693222 36.244614,22.421892 36.371243,22.150566 36.326004,22.096237 36.380287,21.996769 36.434532,21.897303 36.552113,21.508371 36.488815,21.119478 36.425488,20.730585 36.525005,19.83544 36.49786,19.690725 36.470718,19.546013 36.570202,19.519075 36.633533,19.428619 36.696823,19.3382 36.913887,19.301935 37.112885,19.202469 37.311852,19.102964 37.212368,19.102825 37.375145,18.949099 37.537956,18.795336 37.510818,18.740801 37.63744,18.623236 37.764065,18.505671 37.953979,18.008327 38.397182,17.610422 38.840343,17.212477 39.310658,16.733232 39.437279,16.651827 39.563904,16.570457 39.591042,16.380396 39.636242,16.244732 39.681477,16.109104 39.699814,15.910132 39.735996,15.611691 39.77219,15.313251 39.727226,14.752533 39.591557,14.282269 39.455883,13.811971 38.777637,13.821019 38.660057,13.775809 38.542484,13.730599 38.243832,13.567788 37.999626,13.395964 37.755424,13.22414 37.655842,13.061362 37.465889,12.934748 37.275974,12.808132 37.04068,12.554904 36.859779,12.437336 36.678905,12.31977 36.226486,12.012283 36.081772,11.958024 35.93705,11.903764 35.684048,11.98517 35.584526,11.994185 35.485046,12.003234 35.213638,12.030379 35.10511,11.958024 34.996574,11.885666 34.607933,11.822359 34.526546,11.822359 34.445122,11.822359 34.065151,11.668633 33.884277,11.578178 33.703373,11.48776 33.639874,11.542018 33.549438,11.56913 33.458965,11.596276 33.223812,11.641486 33.169567,11.623423 33.115284,11.605325 32.970428,11.542018 32.934242,11.52392 32.898056,11.505857 32.85265,11.352096 32.85265,11.279737 32.85265,11.207382 32.662865,10.836621 32.626678,10.773314 32.590492,10.710007 32.527092,10.755217 32.400467,10.755214 32.273846,10.755217 32.165421,10.664761 32.020702,10.592439 31.875982,10.520082 31.677084,10.384419 31.586649,10.33921 31.496214,10.294001 30.890203,10.095027 30.727425,10.040769 30.564613,9.9865103 30.401838,10.085979 30.374697,10.113128 30.347557,10.140238 30.239126,10.239707 30.184879,10.33921 30.130598,10.438678 29.958912,10.53818 29.913713,10.619551 29.868475,10.700955 29.651419,10.773314 29.615231,10.791375 29.579042,10.809475 29.461466,10.67381 29.47051,10.601453 29.479557,10.529131 29.542887,10.429629 29.588087,10.312063 29.633324,10.194498 29.759981,9.7875404 29.759981,9.7875395 29.759981,9.7875404 29.669512,9.7965889 29.597137,9.8056374 29.524792,9.8146524 29.416023,9.8237009 29.189919,9.7875395 28.963812,9.751379 28.764847,9.7242327 28.656317,9.7151842 28.547787,9.7061348 28.53874,9.778491 28.520647,9.8056374 28.502554,9.8327494 28.502554,9.9232035 28.466366,10.022672 28.430212,10.122139 28.267399,10.321113 28.267399,10.321112 28.267399,10.321113 27.968952,10.303015 27.968952,10.275902 27.968952,10.248756 28.041294,10.058832 28.068436,9.9322519 28.095541,9.8056374 28.158871,9.6790228 28.231213,9.4710016 28.303589,9.2630167 28.29454,9.3715334 28.403072,9.2539682 28.511599,9.1364021 28.502554,9.1273537 28.611084,8.9645777 28.71961,8.8017664 28.674343,8.7113466 28.728624,8.5756836 28.782873,8.4400196 28.710566,8.3767109 28.710566,8.3767109 28.710566,8.3767109 28.538809,8.2681942 28.439327,8.1867886 28.339844,8.1054182 28.285425,7.9878526 28.213083,7.8612366 28.140707,7.7346215 28.140501,7.454246 28.140501,7.2914677 28.140501,7.1286936 28.113327,6.8754644 28.113327,6.8754635 z M20.231823,6.1329994 L20.020702,7.1999884 20.021301,7.1999884 19.665842,8.9964542 19.656143,8.9964542 19.445026,10.063442 19.454723,10.063442 18.864254,13.047635 20.108284,13.047635 20.698753,10.063442 24.261639,10.063442 24.472757,8.9964542 20.909872,8.9964542 21.265329,7.1999884 24.837318,7.1999884 25.048437,6.1329994 21.47645,6.1329994 20.23242,6.1329994 z M13.148003,6.1329994 L11.779789,13.047635 13.023863,13.047635 13.984522,8.1926832 15.917235,13.047635 16.019186,13.047635 17.161308,13.047635 17.26326,13.047635 18.631472,6.1329994 17.3874,6.1329994 16.398394,11.131203 14.408652,6.1329994 14.392074,6.1329994 13.164581,6.1329994 z M16.519157,1.0677059 C19.716774,1.0655137 23.116938,2.0037482 28.217184,3.6171548 35.635727,5.9639359 42.941868,2.4947336 42.941868,2.4947333 42.941868,2.4947336 43.503883,6.0658188 44.515499,14.228559 45.527111,22.391298 48,36.778122 48,36.778122 48,36.778122 47.662846,37.39035 45.527203,39.124943 43.391556,40.859543 41.255997,41.369759 32.038975,38.104572 22.821959,34.839394 22.035215,34.737423 16.752264,35.04353 11.46931,35.349632 7.5352616,39.022934 7.5352621,39.022934 7.5352616,39.022934 6.8607316,25.554411 6.6359253,21.371023 6.4111228,17.187639 4.5003376,4.6374679 4.5003376,4.6374679 4.5003376,4.6374679 7.3103886,3.0049505 12.143682,1.678515 13.654084,1.2640015 15.065696,1.0687021 16.519157,1.0677059 z M2.6672781,0 C4.1403747,0 5.3345561,1.0840253 5.3345556,2.4212387 5.3345561,3.4241478 4.6628294,4.2846398 3.7055032,4.6522026 L3.6687105,4.6644273 6.3859963,40.255939 6.9772267,48 4.941246,47.876789 1.8349936,4.7206259 1.6290536,4.6522026 C0.67172724,4.2846398 5.52291E-07,3.4241478 0,2.4212387 5.52291E-07,1.0840253 1.1941813,0 2.6672781,0 z";
	}
	public class IconeMaoObra : IIcone
	{
		public string Data => "M19.733311,15.694674 C19.444138,15.70604 19.152966,15.861162 18.965618,16.134909 L18.31951,17.078985 C18.019756,17.516979 18.099306,18.092789 18.497192,18.365095 18.627749,18.454445 18.776358,18.500015 18.927704,18.505444 18.949324,18.50622 18.971001,18.506176 18.992689,18.505323 19.281862,18.493958 19.573036,18.338837 19.760382,18.06509 L20.40649,17.121014 C20.706245,16.68302 20.626694,16.10721 20.228808,15.834904 20.079599,15.732789 19.906815,15.687856 19.733311,15.694674 z M11.510443,13.896999 L11.116248,14.627784 C11.116248,14.627784 11.022999,14.955924 11.022999,15.330941 11.022999,15.705958 11.226101,15.877842 11.679174,16.127853 12.132247,16.377865 12.366595,16.627876 12.694682,16.924767 13.02277,17.221655 13.600829,18.174824 13.600829,18.174824 13.600829,18.174824 14.506887,18.471493 15.069375,18.612125 15.826974,18.801537 16.663002,19.252998 16.663002,19.252998 z M11.924296,12.745999 L11.76,13.255471 18.609123,20.551184 18.556101,20.668472 C18.355131,21.143627 18.244,21.666035 18.244,22.214399 18.244,24.407854 20.022101,26.185999 22.2155,26.185999 22.558218,26.185999 22.890799,26.142588 23.20804,26.060963 L23.227902,26.055588 20.87413,23.701757 23.599321,20.976498 26.010683,23.387922 26.036339,23.301674 C26.134472,22.956131 26.187,22.591398 26.187,22.214399 26.187,20.020943 24.408899,18.242798 22.2155,18.242798 21.701422,18.242798 21.210157,18.340473 20.759251,18.51828 L20.694294,18.544821 20.350624,18.236856 20.295492,18.186834 19.886599,18.745203 C19.541365,19.216642 18.879335,19.318949 18.407917,18.973711 L17.944822,18.634569 C17.561794,18.354063 17.422445,17.864414 17.568832,17.437788 L17.576391,17.417425 17.51648,17.433159 C17.160023,17.517727 16.770559,17.45743 16.444418,17.234221 L15.942676,16.890829 C15.443872,16.549448 15.246656,15.932028 15.415598,15.383194 L15.427037,15.349072 15.40128,15.354537 C15.06245,15.418032 14.699402,15.352263 14.392446,15.142183 L13.890704,14.798792 C13.391899,14.457411 13.194682,13.839991 13.363626,13.291158 L13.382361,13.235274 13.348308,13.2425 C13.009479,13.305994 12.64643,13.240226 12.339473,13.030146 z M18.417164,12.476685 C18.108673,12.474829 17.8048,12.621259 17.617453,12.895004 L16.047365,15.189177 C15.747611,15.62717 15.859675,16.225233 16.297669,16.524988 L16.613737,16.741299 C16.777985,16.853706 16.964741,16.908201 17.149836,16.909315 17.458326,16.911169 17.762201,16.76474 17.949547,16.490993 L19.519634,14.196822 C19.819389,13.758829 19.707325,13.160766 19.269331,12.861012 L18.953264,12.644701 C18.789017,12.532293 18.602259,12.477798 18.417164,12.476685 z M10.563287,10.499848 C10.310464,10.488708 9.9600001,10.581723 9.6787501,11.065125 9.1787501,11.924506 7.9600001,14.783606 7.9600001,14.783606 7.9600001,14.783606 7.6269694,15.501296 7.604713,17.081511 7.5824567,18.661724 7.270865,19.284908 7.5156871,20.375479 7.7605091,21.466049 8.0053311,21.822153 8.0053311,21.822153 L6.6799065,24.648306 13.912054,28.088579 16.280701,23.973585 17.644478,23.208569 C17.644478,23.208569 17.545668,22.798668 17.602791,22.05616 17.659916,21.313652 17.697998,21.257084 17.697998,21.257084 17.697998,21.257084 16.821653,19.790941 15.412649,19.372108 14.003646,18.953278 13.222982,18.876869 12.823129,18.172451 12.423276,17.468033 12.480398,17.46825 11.699734,16.916149 10.919069,16.364048 10.462095,16.344503 10.500176,15.506798 10.538258,14.669092 10.652501,14.573379 10.652501,14.573379 L11.718775,11.888246 C11.718775,11.888246 11.947262,11.060484 11.452206,10.859873 L10.8975,10.580181 C10.8975,10.580181 10.759927,10.508512 10.563287,10.499848 z M16.394163,10.319684 C16.085672,10.317829 15.781799,10.464259 15.594453,10.738005 L14.024365,13.032177 C13.724611,13.47017 13.836676,14.068233 14.274669,14.367987 L14.590736,14.584298 C14.754984,14.696706 14.941742,14.751202 15.126837,14.752315 15.435328,14.754169 15.739201,14.607739 15.926547,14.333994 L17.496635,12.039823 C17.796389,11.601829 17.684324,11.003766 17.24633,10.704012 L16.930264,10.4877 C16.766016,10.375293 16.579258,10.320797 16.394163,10.319684 z M13.870498,9.1126146 C13.562007,9.1107597 13.258133,9.2571897 13.070787,9.5309362 L12.131071,10.904024 12.162074,10.960716 C12.340284,11.306007 12.367547,11.724058 12.205814,12.104925 L12.193781,12.131378 12.209339,12.148288 C12.253736,12.194183 12.303392,12.236314 12.358141,12.273784 L12.569403,12.418366 C12.73365,12.530775 12.920408,12.585271 13.105502,12.586384 13.413994,12.588239 13.717867,12.441809 13.905213,12.168063 L14.868163,10.761026 C15.167917,10.323032 15.055853,9.7249699 14.617859,9.4252157 L14.406597,9.280632 C14.24235,9.1682243 14.055593,9.1137285 13.870498,9.1126146 z M8.5314999,3.7679996 C8.1887818,3.7679996 7.8562017,3.8114104 7.53896,3.8930333 L7.5190979,3.8984077 9.8728686,6.2521787 7.1476789,8.9773684 4.7363172,6.5660062 4.7106615,6.6522527 C4.612527,6.997787 4.56,7.3625092 4.56,7.7394996 4.56,9.9328985 6.338101,11.710999 8.5314999,11.710999 8.6343156,11.710999 8.7362185,11.707092 8.8370686,11.699419 L8.8859454,11.695079 9.3284445,10.784191 C9.6535102,10.115042 10.45948,9.8361073 11.12863,10.161173 L11.522257,10.352393 11.534539,10.338551 C12.137917,9.6419973 12.503,8.7333832 12.503,7.7394996 12.503,5.5461006 10.724899,3.7679996 8.5314999,3.7679996 z";
	}
	public class IconeMapaMesa : IIcone
	{
		public string Data => "M100.10141,78.928802 L97.6558,79.675415 97.643723,79.927566 C97.587433,81.129272 97.559441,82.015823 97.559441,82.015823 L100.0666,82.015823 C100.0666,82.015823 99.999413,81.323433 100.09174,79.147346 z M53.320621,51.066021 L49.735172,79.400955 50.691029,80.117813 50.810558,79.221031 C50.810558,79.221031 52.364521,79.101478 53.798946,79.87857 55.233364,80.65567 56.907558,81.612106 57.863941,81.074112 58.820309,80.536125 58.940144,80.416748 58.940144,80.416748 58.940144,80.416748 55.473324,78.324547 54.815777,76.949676 54.158237,75.574798 53.381138,73.123947 53.022472,69.537323 52.663815,65.950691 52.364933,59.972992 52.663815,58.538341 52.962696,57.103687 53.381138,55.669037 53.739796,54.712604 54.098461,53.756172 54.457119,52.79974\r\n                54.098461,52.142193 53.739796,51.484642 53.320621,51.066021 53.320621,51.066021 z M86.123909,49.480011 C86.123909,49.480011 85.311554,50.104515 84.874054,51.042004 84.436554,51.979488 84.374039,53.792026 84.374039,53.979523 84.374039,54.167019 84.124039,57.979526 83.999054,59.604504 83.874046,61.229538 79.311554,75.604515 79.311554,75.604515 L79.124046,76.042015 C79.124046,76.042015 78.436554,76.792 76.874062,78.229538 75.311577,79.667023 71.749092,81.1045 71.749092,81.1045 L72.1241,82.229545 C72.1241,82.229545 72.186592,82.292107 73.8741,82.292107 75.5616,82.292107 75.686005,82.417107 77.936073,81.917114 80.186188,81.417114 81.748741,81.104622 81.748741,81.104622 L82.123741,81.542114 86.123909,81.167122 C86.123909,81.167122 86.74894,79.916595 87.248962,77.604126 87.748978,75.291656 88.81102,70.666725 88.81102,70.666725 z M121.43705,47.31506 L123.76662,49.012997 C123.76662,49.012997 121.9743,54.407436 121.55644,56.400318 121.13858,58.3932 121.07844,58.862053 121.07844,58.862053 L118.69089,56.635059 C118.69089,56.635059 119.07413,53.647343 119.99156,50.944748 120.79799,48.56916 121.43705,47.31506 121.43705,47.31506 z M39.393612,41.442016 L41.904224,45.98542 C41.904224,45.98542 41.067234,46.224701 39.991421,46.045357 38.915607,45.866009 37.899555,45.387398 37.66048,45.387398 37.421406,45.387398 36.823246,45.208065 36.823246,45.208065 36.823246,45.208065 37.360935,43.414753 38.078259,42.637653 38.795589,41.860554 39.393612,41.442016 39.393612,41.442016 z M89.89994,38.087006 L84.112045,38.087017 84.112045,38.248257 84.393639,38.302948 C85.514282,38.548408 86.27504,39.027134 86.27504,39.577503 86.27504,40.37804 84.665512,41.027004 82.680054,41.027004 80.694595,41.027004 79.085068,40.37804 79.085068,39.577503 79.085068,38.9771 79.990425,38.46196 81.280724,38.241913 L81.392059,38.225483 81.392059,38.087021 73.658325,38.08704 73.658325,38.806812 73.663475,38.815918 C73.707397,38.8992 73.658226,38.984943 73.658226,39.072502 73.658226,39.873039 71.633873,40.70224 69.270103,40.70224 66.906334,40.70224 64.990112,39.873039 64.990112,39.072502 64.990112,38.984943 64.882881,38.805046 64.882881,38.805046 L64.882881,38.087059 58.194149,38.08707 58.194149,38.220169 58.341488,38.241913 C59.631779,38.46196 60.537144,38.977104 60.537144,39.577503 60.537144,40.37804 58.927608,41.027004 56.94215,41.027004 54.956692,41.027004 53.347164,40.37804 53.347164,39.577503 53.347164,39.027134 54.107922,38.548408 55.228565,38.302948 L55.474155,38.255249 55.474155,38.087078 49.775326,38.087089 45.044582,43.608467 46.073925,46.449173 47.0051,46.966492 47.665501,47.231136 48.395084,51.34042 43.562199,85.204994 47.605587,85.204979 52.005505,50.733364 53.263485,47.231121 86.706451,47.231056 87.648376,50.473747 92.221275,85.119064 96.197998,85.204834 91.452301,51.51223 92.0121,45.983585 93.926186,45.414539 93.046745,44.276436 89.942558,45.15588 83.062241,44.483364 83.631294,42.931408\r\n                85.597099,41.482918 90.729424,39.718494 z M123.10828,34.889004 C123.10829,34.889004 123.10829,34.889004 121.019,40.574703 119.7785,43.950588 118.55904,48.731624 117.77295,52.059067 L117.74142,52.193294 117.86444,59.682938 106.24517,59.640526 C106.24517,59.640526 103.33018,58.797069 101.25994,58.501861 99.189697,58.206653 98.386841,58.03796 98.386841,58.03796 98.386841,58.03796 99.570122,62.002197 99.908119,65.08081 100.1088,66.908737 100.44353,69.539543 100.68239,71.375397 L100.70421,71.542801 100.72401,71.357719 C101.17369,67.19503 101.738,62.789932 101.738,62.789932 L117.07935,63.72781 C117.07935,63.72781 117.19836,64.48983 117.37745,67.479164 117.55653,70.468574 118.45221,75.685371 118.45221,75.685371 L120.4221,75.743965 C120.4221,75.743965 120.4221,75.743965 120.12363,74.044144 119.82516,72.344322 119.22788,63.72781 119.22788,63.72781 L120.60136,63.84507 C120.60136,63.84507 120.36242,68.475632 120.72058,73.516579 121.07875,78.557518 122.51183,82.073997 122.51183,82.073997 L124.78004,82.073997 C124.78004,82.073997 123.88426,79.026276 123.64549,76.740288 123.40672,74.454307 122.81007,65.017326 124.12328,58.217972 125.4365,51.418537 130.51031,38.522678 130.51031,38.522674 L124.36186,34.889004 z M20.316624,34.687004 L14.499318,38.325992 C14.499317,38.325989 19.299862,51.240711 20.542355,58.050083 21.784832,64.859383 21.220325,74.310165 20.994417,76.599487 20.768505,78.888817 19.920963,81.940994 19.920963,81.940994 L22.067028,81.940994 C22.067028,81.940994 23.422909,78.419365 23.761791,73.371063 24.100666,68.322746 23.874584,63.685413 23.874584,63.685413 L25.174091,63.567982 C25.174091,63.567982 24.60899,72.19709 24.326595,73.899399 24.044201,75.601707 24.044201,75.601707 24.044201,75.601707 L25.907993,75.543022 C25.907993,75.543022 26.755436,70.318603 26.924877,67.324821 27.094311,64.331116 27.206913,63.567982 27.206913,63.567982 L41.722004,62.628734 C41.722004,62.628734 42.851612,71.962234 43.133999,76.306152 43.195774,77.256378 43.238628,78.057724 43.267879,78.729858 L43.278996,78.995537 44.741238,69.586212 C44.741238,69.586212 44.533871,68.424934 44.492405,65.729164 44.450932,63.033398 44.783199,58.886066 44.783199,58.886066 L43.455669,58.720173 C43.455669,58.720173 43.455311,58.720173 41.962307,58.92754 40.469303,59.134907 32.921673,59.218155 32.09219,59.218155 31.262707,59.218155 29.853046,59.218529 27.364407,57.72541 26.586712,57.258812 25.987255,56.249451 25.53059,55.082123 L25.421171,54.78989 25.42844,54.830124 C25.602497,55.807419 25.682171,56.464859 25.682171,56.464859 L23.423206,58.695114 C23.423206,58.695114 23.366306,58.225567 22.970951,56.229774 22.575596,54.233978 20.87981,48.83165 20.87981,48.83165 L23.083912,47.131229 C23.083912,47.131229 23.615337,48.235077 24.310452,50.333347 L24.402264,50.615021 24.395641,50.574009 C24.264843,49.739033 24.21236,49.181286 24.21236,49.18129 L25.368198,46.903873 25.25922,46.471184 C24.7094,44.329586 24.097172,42.1604 23.479435,40.381019 21.502682,34.687004 21.502682,34.687004 21.502682,34.687004 z M79.137199,28.179924 C79.137199,28.179924 79.246964,30.734245 79.920097,32.208977 80.593239,33.683708 82.023643,34.105053 82.023643,34.105053 82.023643,34.105053 82.191925,34.737099 82.276085,35.832615 82.351013,36.808311 82.39257,38.686745 82.400764,39.085979 L82.401024,39.098575 82.382225,39.099094 C81.259155,39.136532 80.398842,39.348156 80.398842,39.603611 80.398842,39.885494 81.446358,40.114006 82.738541,40.114006 84.030716,40.114006 85.078232,39.885494 85.078232,39.603611 85.078232,39.348156 84.217918,39.136532 83.094849,39.099094 L82.996346,39.096367 82.997055,39.087307 C83.014008,38.871887 83.069489,38.186905 83.179131,37.008022 83.312012,35.578987 83.476456,34.112308 83.476456,34.112308 83.476456,34.112308 84.305939,33.904499 85.291847,32.501785 86.287407,31.085415 86.188263,28.179924 86.188263,28.179924 z M53.23,28.179924 C53.23,28.179924 53.339764,30.734245 54.012905,32.20898 54.686039,33.683708 56.116444,34.105053 56.116444,34.105053 56.116444,34.105053 56.284733,34.737099 56.368885,35.832615 56.443813,36.808311 56.485371,38.686745 56.493565,39.085979 L56.493824,39.098575 56.475025,39.099094 C55.351963,39.136532 54.49165,39.348152 54.49165,39.603611 54.49165,39.885494 55.539165,40.114006 56.831341,40.114006 58.123524,40.114006 59.17104,39.885494 59.17104,39.603611 59.17104,39.348152 58.310726,39.136532 57.187656,39.099094 L57.089153,39.096367 57.089863,39.087307 C57.106808,38.871887 57.162289,38.186905 57.271931,37.008022 57.404812,35.578987 57.569263,34.112308 57.569263,34.112308 57.569263,34.112308 58.398739,33.904499 59.384655,32.501785 60.380207,31.085415 60.281063,28.179924 60.281063,28.179924 z M42.494404,24.824047 C42.07761,24.825706 41.566311,24.919367 40.985821,25.024523 39.658993,25.26482 33.386303,33.976528 33.386303,33.976528 L32.90366,33.495384 C32.90366,33.495384 32.904049,33.495693 32.783413,33.976345 32.662807,34.457001 31.45647,36.920147 29.948793,39.503654 28.441135,42.087227 25.405758,49.39777 25.405758,49.39777 25.405758,49.39777 25.305141,49.597454 25.727322,51.399891 26.149509,53.202328 26.270138,55.064888 27.898548,56.687088 29.526951,58.309292 32.120358,58.249233 35.497807,58.18911 38.875256,58.129051 45.44923,57.528221 45.44923,57.528221 45.44923,57.528221 45.871395,57.94881\r\n                45.992023,58.849998 46.112652,59.751255 45.690456,64.257004 45.690456,64.257004 L47.318867,50.979416 46.776081,48.035435 C46.776081,48.035435 46.474506,47.855137 45.630138,47.254364 44.785809,46.653534 44.242985,44.490559 44.242985,44.490559 L42.192394,38.902943 C42.192394,38.902943 42.795506,38.121872 44.242985,37.340805 45.690456,36.55973 47.017109,35.718601 47.017109,35.718601 48.344181,34.096401 47.801228,33.014915 47.801228,33.014915 47.801228,33.014915 47.017017,31.873308 45.629864,29.890633 44.242702,27.907896 43.8811,26.286125 43.579556,25.444996 43.409924,24.971861 43.03027,24.821911 42.494404,24.824047 z M109.99827,15.917006 C109.99827,15.917006 109.18632,16.854515 108.06133,17.792013 106.93634,18.729513 105.37384,22.229999 105.37384,22.230003 L101.43687,36.355408 93.686714,39.792862 94.186729,42.355312 93.186707,42.605305 92.374191,40.479858 C92.374191,40.479858 89.874138,41.29285 87.6241,41.980331 85.374054,42.66782 84.874039,43.480301 84.874039,43.480301 L89.81115,43.855289 93.623894,42.917812 94.373947,43.855289 100.31184,41.980831 99.936813,44.730224 93.186371,46.792686 92.498817,51.792564 96.374084,77.854538 96.749107,78.667045 100.56234,77.605072 99.937729,74.542564 C99.937729,74.542564 99.937714,74.542549 99.249985,68.417519 98.562256,62.292492 96.56208,56.480461 96.56208,56.480461 96.56208,56.480461 97.498993,56.604519 99.686485,56.917019 101.87398,57.229519 104.43646,57.917019 107.43645,58.479519 110.43644,59.042019 116.68604,58.354519 116.68604,58.354519 L116.68604,52.167015 C116.68604,52.167015 117.87302,41.104511 117.81053,37.917007 117.74803,34.729507 116.93555,27.104504 116.12308,24.292004 115.3106,21.479502 114.06065,19.917002 112.99818,19.354502 111.93571,18.792 111.18575,17.542002 110.99825,17.167 110.81075,16.792 109.99827,15.917006 109.99827,15.917006 z M69.422234,13.90901 C68.942947,13.909484 68.451935,14.038343 67.94931,14.294162 L67.94931,15.690605 68.195808,15.690605 68.195808,20.51886 68.142593,20.554924 C67.053215,21.346565 66.272331,23.204185 66.209785,25.398874 L66.153412,39.194992 66.261276,39.194992 66.260109,39.197845 C66.257408,39.207748 66.256035,39.217712 66.256035,39.227741 66.256035,39.54858 67.657829,39.808674 69.387032,39.808674 71.116241,39.808674 72.518036,39.54858 72.518036,39.227741 72.518036,39.217712 72.516663,39.207748 72.513962,39.197845 L72.512787,39.194992 72.620659,39.194992 72.429466,25.398874 C72.369522,23.295629 71.649864,21.501951 70.631195,20.659378 L70.578262,20.618282 70.578262,15.690605 70.82476,15.690605 70.82476,14.294162 C70.369072,14.036444 69.90152,13.908534 69.422234,13.90901 z M44.133106,11.284082 C43.262135,11.287891 42.232853,11.442764 41.093426,11.893211 38.489048,12.922806 37.51997,15.860181 36.76289,18.222195 36.005795,20.584207 34.340199,24.611744 34.188778,26.216701 34.037365,27.821659 33.21925,32.485016 33.21925,32.485016 33.21925,32.485016 36.03532,29.062942 37.27702,27.458015 38.518726,25.85309 39.487514,24.611544 40.698986,24.005915 41.910442,23.400278 43.697063,24.096935 43.697063,24.096935 43.697063,24.096935 43.817951,23.097538\r\n                43.908825,22.734156 43.999691,22.370771 44.575832,22.522262 45.544956,22.613109 46.514088,22.703956 47.361584,22.40119 47.361584,22.401188 47.361584,22.40119 47.51318,21.674623 47.51318,21.311224 47.51318,20.947824 47.81641,19.554392 47.81641,19.55439 47.81641,19.554392 47.81641,19.554296 47.998104,19.524013 48.179798,19.493736 48.482693,19.372911 48.482693,19.372909 48.482693,19.372911 48.391506,19.039612 48.300716,18.676241 48.209934,18.312872\r\n                47.997684,17.52618 47.997684,16.890156 47.997684,16.254131 48.119602,14.437313 48.119602,14.437313 48.119602,14.437313 48.391422,14.103538 48.633686,13.800711 48.875942,13.497885 48.785458,13.316189 48.664326,13.073929 48.543194,12.831667 47.483265,12.044472 46.605045,11.711367 46.111042,11.523992 45.252926,11.27918 44.133106,11.284082 z M104.39215,6.1704553 C103.84941,6.1825789 103.36435,6.2684964 103.15831,6.3543457 102.82864,6.4917015 102.05942,6.601642 101.26272,7.2334837 100.46602,7.8653244 99.669319,8.9366244 99.31218,9.5684718 98.95504,10.200316 98.186005,11.2713 98.186005,11.2713 L99.285179,11.985904 99.61483,13.607194 99.807129,14.101584 99.58744,16.87606 100.35648,16.931002 100.57633,17.315584 C100.57633,17.315582 101.09868,19.293321 101.37337,19.485609 101.64808,19.677894 101.81309,19.980093 102.66462,19.677929 103.51616,19.375763 104.1756,19.293354 104.1756,19.293354 L105.32957,19.540932 C105.32957,19.540933 105.98891,17.617771 106.62061,17.068367 107.25229,16.518961 109.36691,14.953438 109.36691,14.953438 109.36691,14.953438 109.33966,14.59558 109.50449,13.881296 109.66933,13.167008 109.724,12.20547 109.724,11.903272 109.724,11.601075 109.7242,11.738438 109.69672,11.243934 109.66925,10.749427 109.39453,10.117558 109.2297,9.540635 109.06487,8.9637126 108.73519,8.3318442 108.57036,7.9747003 108.40553,7.6175588 107.99344,7.3703064\r\n                107.74619,7.1505264 107.49894,6.9307455 106.23521,6.4091147 105.38357,6.244285 105.0642,6.1824726 104.71779,6.1631812 104.39215,6.1704553 z";
	}
	public class IconeMercadoLivre : IIcone
	{
		public string Data => "M64.9559978835285,64.9499976962805L0.0560013234615326,64.9499976962805z M15.7679533354938,34.0688369601965C16.1165794722736,34.0688827365637 16.4587406508625,34.1418350070715 16.7483372084796,34.2930038303137 17.3275341384113,34.5953338474035 17.7176899306476,35.2887275546789 17.8085865370929,35.996765884757L17.8220962874591,36.1546829074621 17.8456997267902,36.1132858127356C18.4159244887531,35.1891067355871 19.7749218337238,34.0394943088293 21.2213196150959,34.6393401950598 22.3904365889728,35.124192032218 22.8199772231281,35.9148376315832 22.8956474654377,36.7023132175207L22.9053921096027,36.9312179416418 23.002766072005,36.845196518302C23.7361740462482,36.2353599399328 24.6631111495197,35.8111694186926 25.5269855849445,36.1800506442785 27.9216083876789,37.202576431632 27.2222465865314,39.2271774142981 26.6799206130207,40.3499610751867L26.5803188674152,40.5499084323645 26.7887062422931,40.3836295932531C27.2728657118976,40.050591263175 27.8921360366046,39.8690307468176 28.4878277175128,39.8587272495031 28.9210533492267,39.8512351840734 29.3418144576252,39.9343116611242 29.6890625350177,40.1155746310949 30.8002495162189,40.695610794425 31.3042282454669,42.5276496738195 30.6877197615802,43.7092521518469 30.0712093703449,44.8908431857824 28.2640098921955,45.4201362460852 26.8668884627521,44.5934341281652 25.9903873793781,44.0747917026281 25.6232265822589,42.9007957309484 25.8470425002277,41.8449981540441L25.908361851424,41.6071212619543 25.7169041030109,41.8287360996008C24.9739632003009,42.6394660800695 23.8452991835773,43.3232200473547 22.5901855818927,42.5395362704992 21.7039760939777,41.986195358634 21.3741154067218,41.1671989291906 21.3273949019611,40.3685043185949L21.3241237990558,40.1647613376379 21.2549805037677,40.2262771457434C20.5392593733966,40.8155792087316 19.596817433089,41.1566856235266 18.5696300856769,40.5153396457434 17.4615806929767,39.8235023349524 17.2229732863605,38.7165992587805 17.320133625716,37.753155502677L17.3428615920246,37.5862014621496 17.251961170882,37.6759689182043C16.5865082137287,38.2620590060949 15.427739559859,38.4221542209387 14.4799036376178,37.8641479343176 13.5791506163776,37.3338592380285 13.3713898055255,36.0148132175207 13.864021717757,35.0706413120031 14.2027077071369,34.4215209811926 15.0009731642902,34.0687454074621 15.7679533354938,34.0688369601965z M64.7609753005207,30.1694438785315L64.8037990920246,30.4258411258459C64.9114803664386,31.1538760989904 64.9650692336261,31.8923328250647 64.961864887923,32.639385971427 64.9105572097003,44.5922324985266 50.3379825942218,54.2195251315832 32.4131016127765,54.1425407260656 14.4882215850055,54.0655639499426 -0.00117039196193218,44.313458237052 0.0501358352601528,32.3606155246496 0.0517401017248631,31.9870899051428 0.0675244323909283,31.615834030509 0.0971477769315243,31.2470729678869L0.147690235823393,30.7384326785803 1.89494365379214,31.0243633121252 1.89034920856357,31.2131431430578C1.84452980682254,41.8876464694738 15.5168423049152,50.5999114841223 32.4283184401691,50.6725395053625 49.3397907607257,50.7451675266027 63.0863994948566,42.1506545871496 63.132217823714,31.4761512607336 63.1336483351886,31.1425731509924 63.1216854445636,30.8109100192785 63.0966419570148,30.4813678592443L63.0910915724933,30.4236324161291z M31.9677547805011,18.3555877536535C32.1342434279621,18.3519046634436 32.2994961135089,18.3526733249426 32.4634785048664,18.3575466006994 34.9231914870441,18.4306476444006 37.0965656630695,19.4276492923498 38.8629116408527,20.1891010135412 41.3750385634601,21.2720516055822 43.4099697463214,22.0087306827307 49.4740032546222,21.7054889529943 52.5060123793781,21.5538661807776 55.2517856948078,20.2172553867102 57.2395443312824,18.918547424674L57.3004383437335,18.8779056400061 57.5995182387531,19.1075866550207C61.088631092757,21.8537166446447,63.4303898207843,25.1839092105627,64.1780514113605,28.821852478385L64.2286037795246,29.0953510135412 63.8747906081378,29.122462067008C58.6476253859699,29.6317127078772 48.5216183058917,33.7057159274816 48.5216183058917,33.7057159274816 48.5216183058917,33.7057159274816 36.6898121230304,23.855372223258 34.3942455641925,22.5124061435461 32.0986790053546,21.1694381564856 29.3774608962238,22.8320186465979 29.3774608962238,22.8320186465979 29.3774608962238,22.8320186465979 26.7787079207599,24.6515167087317 26.1290211074054,25.1280544131994 25.4793304793537,25.6045959323645 25.1005787245929,26.4707286685705 22.8483414046466,26.6440189212561 20.5961117140949,26.8173129886389 20.4558109633625,25.388539108634 20.4558128707111,25.388539108634 20.4558109633625,25.388539108634 22.4044803969562,23.4820049136877 23.4872879378498,22.3556926578283 24.5700916640461,21.2293784946203 25.392227589339,20.1459262698889 29.2037853591144,18.8463352054358 30.1566738478839,18.5214374393225 31.079805790633,18.3752277225256 31.9677547805011,18.3555877536535z M8.43287700340152,18.1755454868078L8.67192119285464,18.3393485873938C11.2951073043048,20.1193836063147 14.7515777938068,22.1406562656164 16.8477768294513,22.0809829562902 16.8477768294513,22.0809829562902 18.5124706618488,22.4637744754553 20.166210591048,22.0074317783117 21.8199505202472,21.5510928958654 22.8467468611896,21.3573959201574 22.8467468611896,21.3573959201574L19.823229252547,24.2888038486242C19.823229252547,24.2888038486242 18.9815010420978,24.9940345615149 18.9120449416339,25.8277023166418 18.8906578414142,26.0843704074621 19.7825130812824,27.1160800784826 20.7938579909503,27.453272613883 22.9108986251056,28.1591098636389 24.7862610213459,27.6355941623449 25.6986183516681,26.8940685123205 26.6109718672931,26.1525466769934 29.4605373732746,24.1174590915442 29.4605373732746,24.1174609988928 29.4605373732746,24.1174590915442 32.1631626479328,21.8421542972326 35.1854671828449,24.351860794425 38.2077793471515,26.8615672916174 48.1650890700519,35.0123260349035 48.1650890700519,35.0123260349035 48.1650890700519,35.0123260349035 49.0774883620441,36.7810714572668 47.994018971175,37.351486954093 46.9105495803058,37.9218986362219 46.2268490187824,38.4347923129797 44.5731129042804,37.0658271640539 42.9193691603839,35.6968582004309 40.3532261244953,33.4149329990149 40.3532261244953,33.4149329990149 40.3532261244953,33.4149329990149 38.8135341994464,32.8445403903723 40.0110744826496,34.2134826511145 41.2086071364582,35.5824249118566 44.1739277236164,38.2632682651281 44.1739277236164,38.2632682651281 44.1739277236164,38.2632682651281 44.8582386367023,39.7467735141516 43.6607059828937,40.4882972568274 42.4631656996906,41.2298248142004 41.7218403212726,41.4575431674719 40.4672779433429,40.4882972568274 39.2127193801105,39.5190551608801 37.4449200026691,37.6648223727941 37.4449200026691,37.6648223727941 37.4449200026691,37.6648223727941 37.4429058425128,37.6636932224035 37.4390491835773,37.6615569919348 37.3308643691242,37.6017120212316 35.7737964980304,36.753285202384 36.5895351760089,37.9500243991613 37.2965511672199,38.9872482150793 39.8471264235675,41.4218681186438 39.8471264235675,41.4218681186438 39.8471264235675,41.4218681186438 40.2364696852863,42.4334724277258 39.4835895888507,43.0905349582434 37.9652790419757,44.4156005710363 36.5111317031085,43.0039222568274 36.5111317031085,43.0039222568274L33.9449848525226,40.6077812045813C33.9449848525226,40.6077812045813 32.7902149550617,40.2441718906164 33.2392925612628,40.8644454807043 33.6883663527667,41.4847152560949 35.9765171401203,43.7733009189367 35.9765171401203,43.7733009189367 35.9765171401203,43.7733009189367 35.9978985182941,44.6935470432043 34.6078991286457,44.7791030734777 33.2179073683917,44.8646629184484 31.8065266005695,44.0729301303625 31.8065266005695,44.0729301303625 31.8065266005695,44.0729301303625 32.5324329726398,41.1917808383703 31.1649860732257,39.9231880038977 29.6894535414875,38.5543220371008 28.1283802382648,38.9607055515051 28.1283802382648,38.9607055515051 28.1283802382648,38.9607055515051 28.3516544692218,36.8313110202551 26.8955025069416,35.7807357639074 25.1562485091388,34.5259101718664 23.5306972853839,35.3460471957922 23.5306972853839,35.3460471957922 23.5306972853839,35.3460471957922 22.8891624800861,34.1269004672766 21.8840870253742,33.8060691684484 20.8790134780109,33.4852454990149 20.237478672713,33.4638564914465 19.4676346175373,33.7205169528723 18.6977924697101,33.9771774142981 18.2700981490314,34.3835609287024 18.2700981490314,34.3835609287024 18.2700981490314,34.3835609287024 17.243639408797,32.971920761466 15.8964104048908,32.9933059543371 14.5491842620075,33.0146949619055 13.565494953841,33.9557884067297 13.565494953841,33.9557884067297 13.565494953841,33.9557884067297 9.50242751762271,31.7527702182531 6.89351171180606,31.0469520419836 4.61070913001895,30.4293563693762 4.03065198585391,30.155649933219 0.483069746941328,29.5523860782385L0.465888231247663,29.5494945377111 0.498864202946424,29.3198154300451C1.18275350257754,25.1546485751867,3.95135635063052,21.3706634372473,8.14912837669253,18.3733318179846z M32.598900257796,10.857457908988C41.8414158217609,10.8971526950598,50.1705402724445,13.5091607898474,56.0638813368976,17.668833526969L56.4693722121417,17.9619396060705 55.4185146681964,18.5793483585119 55.233234822005,18.4463508456945C49.6485103957355,14.5322559207678 41.5784687392414,12.0553367465734 32.594246327132,12.016752037406 23.8742679946125,11.9793031543493 15.9957298628986,14.2461803287268 10.4029898039997,17.9143588870764L10.2001299254596,18.0506408542395 9.17281001731753,17.4017474979162 9.62193053886294,17.0978534549475C15.5120968215168,13.2065475314856,23.6364635817707,10.8189695209265,32.598900257796,10.857457908988z M0.0560003586113453,0.05L64.9559978835285,0.05z";
	}
	public class IconeMoeda : IIcone
	{
		public string Data => "M8626570 10704280l-254200 0 -152700 -252790c-11460,-19180 -22450,-36250 -32970,-51450 -10530,-14960 -21060,-27820 -31810,-38580 -10760,-10760 -21980,-18950 -33670,-24790 -11700,-5610 -24320,-8420 -37890,-8420l-59630 0 0 376030 -221460 0 0 -982400 350780 0c238530,0 357790,89090 357790,267050 0,34380 -5380,65950 -15900,94950 -10520,28990 -25260,55180 -44440,78570 -19170,23150 -42320,43260 -69450,60100 -27360,17070 -57530,30160 -90970,39750l0 2810c14740,4680 28770,11920 42570,22220 13800,10290 26890,22210 39750,36010 12860,13800 25020,28530 36710,44200 11700,15670 22220,31100 31810,46300l185680 290440zm-137690 2731050c-493990,-188910 -940790,-484700 -1182090,-971200 -188250,-379550 -214890,-797820 -187580,-1213680l3220 -49000c26150,159420 -136950,1479900 1438240,1824910l33020 -188180 525430 508190 -667540 301480 37300 -212520zm1726730 -4263340l-33020 188180 -525430 -508200 667540 -301480 -37300 212520c493990,188920 940790,484710 1182090,971210 188250,379540 214890,797820 187580,1213680l-3220 49000c-26640,-155210 136950,-1479920 -1438240,-1824910zm40300 3176480l0 148490 -100080 0 0 -144520c-92840,-470 -179140,-21510 -258880,-62900l0 -189890c26430,21520 65950,41390 118330,59630 52620,18240 99380,28760 140550,31570l0 -249520c-106880,-39750 -179830,-82780 -218890,-129080 -39050,-46300 -58700,-102890 -58700,-169550 0,-71790 25500,-132820 76240,-183330 50510,-50520 117630,-79740 201350,-87930l0 -127440 100080 0 0 124640c96350,4670 168140,20110 215610,46530l0 184980c-63840,-38820 -135870,-62440 -215610,-71100l0 259580c99850,36010 171640,77400 215140,123700 43270,46310 65020,102430 65020,168140 0,75770 -24330,137040 -72730,183340 -48410,46300 -117630,74600 -207430,84660zm-100080 -599590l0 -217250c-63610,11460 -95180,44670 -95180,99390 0,47000 31570,86280 95180,117860zm100080 231510l0 207650c65250,-10050 97980,-42790 97980,-97980 0,-44670 -32730,-81380 -97980,-109670zm-2232210 -2092710l0 273360 95880 0c47470,0 85590,-13790 114350,-41150 29230,-27830 43960,-62440 43960,-103360 0,-86050 -51440,-128850 -154340,-128850l-99850 0zm959560 811220l0 148500 -100080 0 0 -144520c-92840,-470 -179130,-21520 -258880,-62910l0 -189890c26430,21520 65950,41390 118330,59640 52620,18240 99390,28760 140550,31570l0 -249520c-106870,-39750 -179830,-82790 -218880,-129090 -39060,-46290 -58700,-102890 -58700,-169540 0,-71790 25490,-132820 76230,-183330 50510,-50520 117630,-79750 201350,-87930l0 -127450 100080 0 0 124640c96350,4680 168140,20110 215610,46540l0 184970c-63840,-38820 -135870,-62440 -215610,-71090l0 259570c99850,36020 171650,77410 215150,123710 43260,46310 65010,102430 65010,168140 0,75760 -24330,137030 -72730,183340 -48410,46300 -117630,74600 -207430,84650zm-100080 -599590l0 -217240c-63610,11450 -95180,44660 -95180,99380 0,47010 31570,86290 95180,117860zm100080 231520l0 207650c65250,-10060 97980,-42800 97980,-97980 0,-44670 -32730,-81380 -97980,-109670zm-450420 -1579640c811700,0 1469730,658020 1469730,1469730 0,811700 -658030,1469730 -1469730,1469730 -811710,0 -1469730,-658030 -1469730,-1469730 0,-811710 658020,-1469730 1469730,-1469730zm0 237290c340330,0 648450,137950 871470,360970 223010,223020 360970,531140 360970,871470 0,340320 -137960,648450 -360970,871460 -223020,223020 -531140,360980 -871470,360980 -340320,0 -648450,-137960 -871470,-360980 -223020,-223010 -360970,-531140 -360970,-871460 0,-340330 137950,-648450 360970,-871470 223020,-223020 531150,-360970 871470,-360970zm845210 387230c-216310,-216310 -515140,-350100 -845210,-350100 -330070,0 -628910,133790 -845210,350100 -216310,216300 -350100,515130 -350100,845210 0,330060 133790,628900 350100,845210 216300,216300 515140,350100 845210,350100 330070,0 628900,-133800 845210,-350100 216300,-216310 350100,-515150 350100,-845210 0,-330080 -133800,-628910 -350100,-845210zm829100 1025040c811710,0 1469730,658030 1469730,1469730 0,811710 -658020,1469730 -1469730,1469730 -811700,0 -1469730,-658020 -1469730,-1469730 0,-32610 1090,-64970 3200,-97050 83270,-11040 164450,-28640 243060,-52240 -5920,48950 -8960,98760 -8960,149290 0,340320 137960,648450 360970,871470 223020,223020 531150,360970 871470,360970 340330,0 648450,-137950 871470,-360970 223010,-223020 360970,-531150 360970,-871470 0,-340330 -137960,-648450 -360970,-871470 -223020,-223010 -531140,-360970 -871470,-360970 -56770,0 -112650,3860 -167380,11290 22380,-78890 38640,-160360 48430,-243790 39230,-3170 78900,-4790 118940,-4790zm845220 624520c-216300,-216300 -515140,-350100 -845210,-350100 -60860,0 -120660,4570 -179080,13340 -150440,481460 -527030,863040 -1005400,1020420 -7130,52840 -10830,106760 -10830,161550 0,330070 133800,628910 350100,845210 216300,216310 515140,350100 845210,350100 330070,0 628910,-133790 845210,-350100 216300,-216300 350100,-515140 350100,-845210 0,-330070 -133800,-628910 -350100,-845210z";
	}
	public class IconeOpcaoNotificacao : IIcone
	{
		public string Data => "M30.6801509849727,43.176776650548L36.3787994377315,48.8770864039659 31.6689224235713,50.6745374232531 28.9033756248653,48.3158948451281z M4.70301151201129,39.9074933558702L4.70316553041339,52.5324285060167 17.139102935046,52.5325810939074 17.1390056602657,44.6121213465929 15.499038695544,46.6733396083117 15.4990901939571,50.8685681849718 6.34313726350665,50.8684575587511 6.34302329942584,41.5715062648058 13.7883853904903,41.5715978175402 15.1122961036861,39.90761924088z M17.3231010429561,38.5696523219347L11.819914817065,45.8978421717882 9.4539012901485,42.8967587977648 7.52505206987262,44.6362683802843 11.7664880745113,50.0325238734484 19.1489391319454,40.1265981227159z M21.8358058921993,30.359752419591L21.8358268730342,32.0527417689562 34.5297622673213,32.0528981715441 34.529743193835,30.3599069148302z M21.8357563011348,26.3277738124132L21.8357772819698,28.0207650691271 34.5297126762569,28.0209195643663 34.5296897880733,26.3279283076525z M46.4586029045284,25.5647370845079L30.0777950279415,41.9379308253527 26.0456027977169,53.7658956080675 37.5595016472042,49.8811643153429 54.1672630302608,33.273399117589z M4.70280933305621,23.4845769435167L4.70296478196979,36.1095120936632 17.1389026634395,36.1096646815538 17.1388053886592,28.189206841588 15.4988374702632,30.2504231959581 15.4988889686763,34.4456517726183 6.34293603822589,34.4455411463976 6.34282159730792,25.1485879451036 13.7881841652095,25.148679497838 15.112093924731,23.4847047358751z M17.322898864001,22.1467378169298L11.8197145454586,29.4749276667833 9.45370006486774,26.4738442927599 7.52485084459186,28.2133500605822 11.7662878029048,33.6096074610949 19.1487369529903,23.7036798030138z M21.8356056205928,14.0818336039782L21.8356285087764,15.7748267680407 34.5295600883663,15.7749831706285 34.5295410148799,14.0819919139147z M21.8355560295284,10.0498559504747L21.8355770103633,11.7428481608629 34.5295104973018,11.7430036097765 34.5294914238155,10.0500113993883z M4.70260810777545,7.06066155731678L4.70276355668902,19.6855962306261 17.1387004844844,19.6857488185167 17.1386032097042,11.7652919322252 15.4986362449825,13.8265063792467 15.4986867897213,18.0217359095812 6.34273433610797,18.0216233760118 6.34262132570148,8.72467208206654 13.7879829399288,8.72476458847523 15.1118946067989,7.06078982651234z M17.322696685046,5.72282290756702L11.8195123665035,13.0510108500719 9.45349979326129,10.0499284297228 7.52465009614825,11.7894361048937 11.7660865776241,17.1856935054064 19.1485366813838,7.27976632416248z";
	}
	public class IconeOrcamentoIndustria : IIcone
	{
		public string Data => "M33.707788,46.85912 L33.707788,54.862999 48.318211,54.862999 48.318211,46.85912 z M15.260789,46.85912 L15.260789,54.862999 29.871212,54.862999 29.871212,46.85912 z M33.108999,40.540001 L33.108999,45.933659 48.917,45.933659 48.917,40.540001 z M14.662,40.540001 L14.662,45.933659 30.469999,45.933659 30.469999,40.540001 z M33.707787,31.18212 L33.707787,39.186001 48.318211,39.186001 48.318211,31.18212 z M33.108999,24.863001 L33.108999,30.256659 48.917,30.256659 48.917,24.863001 z M21.006156,7.8299997 L21.006156,10.615017 C18.948677,10.794696 17.295947,11.436048 16.047969,12.539075 14.799989,13.642103 14.176,14.977213 14.176001,16.544409 14.176,18.001801 14.65664,19.237089 15.617921,20.250279 16.579202,21.263464 18.37528,22.204281 21.006156,23.072727 L21.012412,23.062603 23.468384,23.941397 23.468384,23.926199 C25.076141,24.545092 25.88002,25.343662 25.88002,26.321915 25.88002,27.492007 25.125597,28.198384 23.616756,28.441046 L21.006156,28.522972 C19.994281,28.463082 18.841867,28.23349 17.548917,27.834208 16.255965,27.434923 15.283441,27.000696 14.631343,26.531538 L14.631343,30.679121 C16.598878,31.58749 18.723816,32.046669 21.006156,32.056648 L21.006156,35.216 23.468384,35.216 23.468384,31.966814 C25.683268,31.747206 27.386589,31.130808 28.578352,30.117623 29.770115,29.104431 30.365997,27.769325 30.366001,26.112288 30.365997,24.674858 29.831953,23.449553 28.763863,22.436363 27.695772,21.423178 25.930614,20.522291 23.468384,19.733702 L21.006156,18.865256 C19.443371,18.176487 18.661979,17.318024 18.661979,16.289864 18.661979,15.092009 19.443371,14.368303 21.006156,14.11875 L23.468384,14.058856 C25.435921,14.248517 27.206699,14.76759 28.780727,15.616069 L28.780727,11.573302 C27.61145,10.994338 25.840668,10.654947 23.468384,10.555125 L23.468384,7.8299997 z M62.644001,0 L62.644001,62.477001 z M7.1740002,0 L44.464436,0 55.470001,11.865147 55.470001,14.863 55.470001,27.797613 55.470001,62.477001 28.911743,62.477001 7.1740002,62.476998 z M0,0 L0,62.476998 z";
	}
	public class IconeOrdemProducao : IIcone
	{
		public string Data => "M114.59045,99.393997C111.62824,99.393997 109.2269,101.79532 109.2269,104.7575 109.2269,107.71968 111.62824,110.12099 114.59045,110.12099 117.55266,110.12099 119.954,107.71968 119.954,104.7575 119.954,101.79532 117.55266,99.393997 114.59045,99.393997z M94.590277,99.393997C91.628074,99.393997 89.226729,101.79532 89.226729,104.7575 89.226729,107.71968 91.628074,110.121 94.590277,110.121 97.552481,110.121 99.953818,107.71968 99.953818,104.7575 99.953818,101.79532 97.552481,99.393997 94.590277,99.393997z M73.979094,99.393997C71.016883,99.393997 68.615546,101.79532 68.615546,104.7575 68.615546,107.71967 71.016883,110.12099 73.979094,110.12099 76.941291,110.12099 79.342635,107.71967 79.342635,104.7575 79.342635,101.79532 76.941291,99.393997 73.979094,99.393997z M53.674907,99.393997C50.712703,99.393997 48.311362,101.79532 48.311362,104.7575 48.311362,107.71967 50.712703,110.12099 53.674907,110.12099 56.637114,110.12099 59.038459,107.71967 59.038459,104.7575 59.038459,101.79532 56.637114,99.393997 53.674907,99.393997z M33.648731,99.393997C30.686523,99.393997 28.285183,101.79532 28.285182,104.7575 28.285183,107.71967 30.686523,110.12099 33.648731,110.12099 36.610938,110.12099 39.012278,107.71967 39.012278,104.7575 39.012278,101.79532 36.610938,99.393997 33.648731,99.393997z M13.153551,99.393997C10.191342,99.393997 7.7899998,101.79532 7.7899998,104.7575 7.7899998,107.71968 10.191342,110.12099 13.153551,110.12099 16.115757,110.12099 18.517099,107.71968 18.517099,104.7575 18.517099,101.79532 16.115757,99.393997 13.153551,99.393997z M12.071999,91.744003L115.672,91.744003C122.31157,91.743995,127.694,97.126434,127.694,103.766L127.694,105.75C127.694,112.38956,122.31157,117.772,115.672,117.772L12.071999,117.772C5.432432,117.772,0.05,112.38956,0.05,105.75L0.05,103.766C0.05,97.126434,5.432432,91.743995,12.071999,91.744003z M90.925994,79.952003L122.791,79.952003 122.791,86.139999 90.925994,86.139999z M55.233287,37.431C64.097621,37.431,71.545825,40.113952,73.656977,43.744835L73.684825,43.798225 86.279982,56.290001 100.37926,56.290001 100.26414,55.762127 99.816466,55.739521C97.034735,55.45702 94.863997,53.107761 94.863997,50.251499 94.863997,47.204822 97.333815,44.735 100.38049,44.735 103.42718,44.735 105.89699,47.204822 105.89699,50.251499 105.89699,52.346092 104.72962,54.16803 103.00999,55.102188L102.96516,55.123787 103.2195,56.290001 105.73598,56.290001C108.46979,56.290001,110.686,58.506191,110.686,61.239998L110.686,62.318001C110.686,65.051811,108.46979,67.267998,105.73598,67.267998L105.61369,67.267998 107.83629,77.459175 105.12501,78.050484 102.77346,67.267998 83.982894,67.267998C83.128577,67.267998,82.324805,67.051575,81.623423,66.670563L81.305182,66.477226 81.254393,66.453911C80.82239,66.229149,80.416499,65.936325,80.05255,65.575363L74.255858,59.826252 74.255858,86.139999 38.985724,86.139999 38.985724,62.420379 28.505251,81.498421C27.188976,83.894485,24.179519,84.769836,21.783443,83.453567L20.838614,82.934532C18.442536,81.618263,17.567181,78.608818,18.88346,76.212745L36.440152,44.253582C36.553269,44.047672,36.678891,43.852989,36.815553,43.669967L37.026316,43.407127 37.065932,43.345394C39.474408,39.918896,46.697263,37.431,55.233287,37.431z M55.341998,0.05C64.375167,0.050001535 71.697997,7.3730562 71.697997,16.406502 71.697997,25.439947 64.375167,32.763 55.341998,32.763 46.308829,32.763 38.985999,25.439947 38.985999,16.406502 38.985999,7.3730562 46.308829,0.050001535 55.341998,0.05z";
	}
	public class IconeOrdemServico : IIcone
	{
		public string Data => "M82.875226,75.72791 C80.662762,75.72791 78.86921,77.521245 78.86921,79.733437 78.86921,81.945626 80.662762,83.738961 82.875226,83.738961 85.087693,83.738961 86.881245,81.945626 86.881245,79.733437 86.881245,77.521245 85.087693,75.72791 82.875226,75.72791 z M52.532414,64.201821 L56.038098,67.829881 48.927331,74.696545 C48.927331,74.696545 47.210463,76.841798 46.903944,78.129185 46.597425,79.416577 46.658259,80.151898 46.229111,80.458417 45.799964,80.764928 36.151018,87.269006 36.151018,87.269006 36.151018,87.269006 34.642377,86.283032 33.96801,85.547368 33.293643,84.811703 33.171017,84.566483 33.171018,84.566483 L39.546838,74.696324 C39.546838,74.696324 40.405124,73.899358 41.202105,73.776746 41.999084,73.654134 44.022189,73.347611 44.696557,72.611943 45.370926,71.876283 52.532414,64.201821 52.532414,64.201821 z M46.474577,29.681595 C53.967191,29.681594 60.041156,35.755556 60.041156,43.248168 60.041156,43.716457 60.01743,44.179204 59.971114,44.635271 L59.887521,45.293118 88.523038,73.94463 C91.711579,77.134949 91.756684,82.259511 88.623788,85.390656 85.490887,88.521806 80.366353,88.473836 77.177815,85.283517 L48.564451,56.654174 48.540634,56.658427 C47.866974,56.761359 47.17701,56.814744 46.474577,56.814744 38.981962,56.814744 32.907999,50.740783 32.907998,43.248168 32.907999,41.60916 33.198647,40.038035 33.731215,38.583525 L33.839021,38.299951 42.342758,46.609998 C43.265476,47.5117 44.744461,47.494662 45.646162,46.571942 L49.74602,42.37653 C50.647719,41.453811 50.630683,39.974828 49.707965,39.073126 L41.190162,30.749337 41.193851,30.747723 C42.816934,30.061216 44.601423,29.681594 46.474577,29.681595 z M84.253606,29.400617 C85.632626,29.421431 87.003705,29.968324 88.039971,31.036365 L89.659692,32.705748 C91.732228,34.841831 91.680695,38.253609 89.544595,40.326169 L73.933133,55.473271 71.843587,53.319659 86.347653,39.24702 C87.259705,38.362097 87.719876,37.356969 87.375466,37.002003 87.031064,36.647038 86.012505,37.076653 85.100453,37.961575 L70.596387,52.034215 68.063111,49.423264 82.252545,35.655895 C83.164597,34.770972 83.624768,33.765845 83.280358,33.410879 82.935956,33.055914 81.917397,33.485528 81.005345,34.37045 L66.815907,48.13782 64.808101,46.06845 80.419564,30.92135 C81.487611,29.885071 82.874581,29.379804 84.253606,29.400617 z M35.116821,26.473083 L9.5872028,32.660198 10.06627,34.636951 35.595887,28.449836 z M33.893833,20.876147 L8.3642166,27.063261 8.8432835,29.040014 34.372903,22.852901 z M47.487781,11.820294 L6.9061001,21.655284 7.3851669,23.632037 47.966846,13.79705 z M46.029799,6.1293591 L5.4481171,15.96435 5.9271839,17.941104 46.508865,8.1061142 z M52.470881,0.05 L60.495997,32.238508 C60.495997,32.238508 54.288928,25.564727 48.150829,26.04056 42.01273,26.516393 40.157024,27.515637 38.253774,28.752798 36.35046,29.989958 36.588371,30.180083 36.588372,30.180083 L47.151627,40.410346 44.534572,43.122398 34.209294,33.130206 C34.209293,33.130205 27.500212,39.363525 30.593036,47.357384 33.685856,55.35127 38.111012,57.4924 40.394939,58.777135 42.678864,60.061845 47.199173,59.871199 47.199172,59.871199 L48.769417,61.298701 13.891682,70.196 0.05,12.799138 z";
	}
	public class IconePedidoEcommerce : IIcone
	{
		public string Data => "M46.3080916404724,45.9248818010092L70.0678381919861,45.9248818010092 70.0678381919861,64.4431503862143 79.1990027427673,64.4431503862143 58.2764105796814,84.1890076249838 36.7329840660095,64.4431503862143 46.3080916404724,64.4431503862143z M9.33656358718872,43.771401271224L9.33656358718872,58.2503441423178 23.1108374595642,58.2503441423178 23.1108374595642,49.1579941362143 21.4269328117371,51.4258278459311 21.4269328117371,56.480198726058 17.6739821434021,56.480198726058 17.101137638092,57.2516945451498 16.5550684928894,56.480198726058 11.020468711853,56.480198726058 11.020468711853,45.5415466874838 19.5772204399109,45.5415466874838 20.8801608085632,43.771401271224z M23.417977809906,42.1372002214193L17.2326426506042,50.8521889299154 14.4251847267151,46.857941493392 12.5391421318054,48.5373428910971 17.1011347770691,54.9825218766928 25.3920340538025,43.8166969865561z M28.8754515647888,33.1602066606283L28.8754515647888,35.6468666642904 53.0894484519959,35.6468666642904 53.0894484519959,33.1602066606283z M28.8754515647888,28.0002049058676L28.8754515647888,30.4868668168783 53.0894484519959,30.4868668168783 53.0894484519959,28.0002049058676z M9.33656358718872,24.9595364183187L9.33656358718872,39.4384811967611 23.1108355522156,39.4384811967611 23.1108355522156,30.346129283309 21.4269328117371,32.6139649003744 21.4269328117371,37.6683357805014 17.6739821434021,37.6683357805014 17.101137638092,38.4398239701986 16.5550684928894,37.6683357805014 11.020468711853,37.6683357805014 11.020468711853,26.7296818345785 19.5772204399109,26.7296818345785 20.8801608085632,24.9595364183187z M23.417977809906,23.3253334611654L17.2326397895813,32.0403221696615 14.4251847267151,28.0460785478354 12.5391430854797,29.7254761308432 17.1011347770691,36.1706589311361 25.3920340538025,25.0048302263021z M28.8754515647888,17.1503685563803L28.8754515647888,19.6370266526937 53.0894484519959,19.6370266526937 53.0894484519959,17.1503685563803z M28.8754515647888,11.9903658479452L28.8754515647888,14.4770258516073 53.0894484519959,14.4770258516073 53.0894484519959,11.9903658479452z M28.8754515647888,6.64368282258511L28.8754515647888,9.13034425675869 53.0894484519959,9.13034425675869 53.0894484519959,6.64368282258511z M9.33656358718872,6.2739399522543L9.33656358718872,20.7528847306967 23.1108374595642,20.7528847306967 23.1108374595642,11.6605318635702 21.4269328117371,13.9283693879843 21.4269328117371,18.9827393144369 17.6739821434021,18.9827393144369 17.101137638092,19.7542275041342 16.5550684928894,18.9827393144369 11.020468711853,18.9827393144369 11.020468711853,8.04408393800259 19.5772204399109,8.04408393800259 20.8801608085632,6.2739399522543z M23.417977809906,4.63973461091518L17.2326397895813,13.3547257035971 14.4251847267151,9.36048112809658 12.5391430854797,11.0398796647787 17.1011347770691,17.485060557723 25.3920340538025,6.31923280656338z M83.9999976158142,0.189303649560316L83.9999976158142,84.1889999955893z M7.03608465194702,0.189000057172024L56.0035758018494,0.189000057172024C57.2379775047302,0.188999995589261,58.2386603355408,1.22441612184048,58.2386603355408,2.50166641175747L58.2386603355408,42.7784927934408 43.0525832176209,42.7784927934408 43.0525832176209,62.2732132524252 29.4964022636414,62.2732132524252 36.6212553977966,69.1249512284994 7.03608465194702,69.1249512284994C5.80168092250824,69.1249512284994,4.80099977762177,68.0895355790854,4.80099992786447,66.8122833818197L4.80099992786447,2.50166641175747C4.80099977762177,1.22441612184048,5.80168092250824,0.188999995589261,7.03608465194702,0.189000057172024z M0,84.1889999955893L0,0.189303649560316z";
	}
	public class IconePontoDeVenda : IIcone
	{
		public string Data => "M20.391727,19.297834 L20.675929,19.297834 C20.735139,19.297834 20.783137,19.345925 20.783137,19.405249 L20.783137,21.464393 C20.783137,21.523717 20.735139,21.571809 20.675929,21.571809 L20.391727,21.571809 C20.332517,21.571809 20.284519,21.523717 20.284519,21.464393 L20.284519,19.405249 C20.284519,19.345925 20.332517,19.297834 20.391727,19.297834 z M19.000046,19.297834 L19.28526,19.297834 C19.344469,19.297834 19.392468,19.345925 19.392468,19.405249 L19.392468,21.464393 C19.392468,21.523717 19.344469,21.571809 19.28526,21.571809 L19.000046,21.571809 C18.940837,21.571809 18.892838,21.523717 18.892838,21.464393 L18.892838,19.405249 C18.892838,19.345925 18.940837,19.297834 19.000046,19.297834 z M17.578023,19.297834 L17.863237,19.297834 C17.922447,19.297834 17.970445,19.345925 17.970445,19.405249 L17.970445,21.464393 C17.970445,21.523717 17.922447,21.571809 17.863237,21.571809 L17.578023,21.571809 C17.518814,21.571809 17.470815,21.523717 17.470815,21.464393 L17.470815,19.405249 C17.470815,19.345925 17.518814,19.297834 17.578023,19.297834 z M16.282425,19.297834 L16.567638,19.297834 C16.626848,19.297834 16.674846,19.345925 16.674846,19.405249 L16.674846,21.464393 C16.674846,21.523717 16.626848,21.571809 16.567638,21.571809 L16.282425,21.571809 C16.223216,21.571809 16.175216,21.523717 16.175216,21.464393 L16.175216,19.405249 C16.175216,19.345925 16.223216,19.297834 16.282425,19.297834 z M14.579234,18.522457 L15.445242,22.268999 21.402996,22.268999 22.221343,18.522457 z M14.30818,17.62053 C14.152337,17.62053 14.026001,17.747116 14.026001,17.903269 L14.026001,18.233638 C14.026001,18.38979 14.152337,18.516377 14.30818,18.516376 L22.618822,18.516376 C22.774664,18.516377 22.901001,18.38979 22.901001,18.233638 L22.901001,17.903269 C22.901001,17.747116 22.774664,17.62053 22.618822,17.62053 L21.603264,17.62053 21.603305,17.621084 C21.604026,17.635333 21.604391,17.649678 21.604391,17.664107 21.604391,18.125847 21.230817,18.500162 20.76999,18.500162 20.309162,18.500162 19.935588,18.125847 19.935588,17.664107 19.935588,17.649678 19.935952,17.635333 19.936673,17.621084 L19.936715,17.62053 17.020628,17.62053 17.02067,17.621084 C17.021391,17.635333 17.021755,17.649678 17.021755,17.664107 17.021755,18.125847 16.648181,18.500162 16.187354,18.500162 15.726526,18.500162 15.352952,18.125847 15.352952,17.664107 15.352952,17.649678 15.353317,17.635333 15.354038,17.621084 L15.35408,17.62053 z M17.691436,14.686167 C17.591366,14.683563 17.493055,14.734516 17.439484,14.827488 L15.796471,17.678911 C15.71855,17.814143 15.764791,17.987063 15.899755,18.065139 L16.061796,18.158879 C16.196761,18.236955 16.369339,18.190621 16.447261,18.055389 L18.090274,15.203966 C18.168195,15.068734 18.121953,14.895814 17.986989,14.817739 L17.824948,14.723998 C17.782772,14.699601 17.736922,14.687351 17.691436,14.686167 z M19.268246,14.683095 C19.22276,14.684279 19.176911,14.696529 19.134734,14.720928 L18.972694,14.814667 C18.837729,14.892742 18.791487,15.065664 18.869408,15.200896 L20.515961,18.058461 C20.593883,18.193692 20.766462,18.240027 20.901426,18.16195 L21.063466,18.068211 C21.198431,17.990135 21.244673,17.817214 21.166751,17.681982 L19.520198,14.824416 C19.466627,14.731445 19.368316,14.680491 19.268246,14.683095 z M13.041392,14.447 L25.880252,14.447 30.14761,14.447 C30.205817,14.447 30.253001,14.49414 30.253001,14.552289 L30.253001,15.707493 C30.253001,15.765642 30.205817,15.812783 30.14761,15.812783 L25.985643,15.812783 25.985643,29.158711 C25.985643,29.21686 25.938458,29.264 25.880252,29.264 L13.041392,29.264 C12.983186,29.264 12.936001,29.21686 12.936001,29.158711 L12.936001,15.707493 12.936001,14.552289 C12.936001,14.54502 12.936738,14.537924 12.938142,14.53107 12.939545,14.524216 12.941616,14.517604 12.944282,14.511306 12.946949,14.505008 12.950212,14.499023 12.954,14.493422 12.957787,14.48782 12.962101,14.482602 12.966869,14.477839 12.971637,14.473075 12.97686,14.468766 12.982467,14.464982 12.988073,14.461197 12.994064,14.457938 13.000369,14.455274 13.006674,14.45261 13.013291,14.450542 13.020152,14.449139 13.027013,14.447737 13.034117,14.447 13.041392,14.447 z M25.412833,13.103914 L25.494646,13.154219 28.622562,13.546879 28.670001,14.021005 25.697429,14.021005 25.618375,13.783448 25.412833,13.767644 z M31.910307,6.387056 C32.05767,6.38622 32.211489,6.3948251 32.369703,6.4094722 33.213508,6.4875893 33.666663,7.690411 33.666663,7.690411 L34.992612,14.085005 35.005,14.085005 35.005,28.346001 35.005,29.264 31.567008,29.264 30.486011,29.264 30.486011,28.346001 31.567008,28.346001 31.567008,14.094298 31.044725,12.490041 30.885535,12.143397 C30.885535,12.143397 31.901105,10.002741 31.947983,9.8465069 31.994861,9.6902724 32.057365,9.4558597 31.932357,9.3933661 31.80741,9.3308723 31.666776,9.7058957 31.666776,9.7058959 L30.385502,12.533982 C30.385502,12.533982 26.854264,12.61216 26.416796,12.533982 25.979267,12.455865 25.463364,12.20589 25.416486,11.34654 25.369608,10.487129 26.447743,10.440258 26.447743,10.440258 L29.32281,10.440258 C29.32281,10.440258 30.244562,7.9875005 30.525831,7.206329 30.754362,6.5715767 31.27173,6.3906773 31.910307,6.387056 z M4.2778309,5.6450027 L10.913508,5.6450027 10.913508,5.7039109 10.945085,5.6934222 C11.489166,5.5279007 12.073318,5.8165665 12.268343,6.3576415 L13.79489,10.592892 17.478916,12.12775 C19.082318,12.850297 18.038681,14.629284 16.5882,13.924625 L11.88903,12.108595 10.913508,9.4021094 10.913775,17.827002 10.913775,28.346003 12.382747,28.346003 12.382747,29.264002 10.913775,29.264002 7.7778404,29.264002 7.7778404,17.946001 7.3488491,17.946001 7.3488491,29.264002 4.2139137,29.264002 2.7759445,29.264002 2.7759445,28.346003 4.2190445,28.346003 4.2778309,17.827002 4.2778309,10.20686 2.2930325,16.162571 C2.1053246,16.72582 1.4965752,17.030248 0.93335419,16.842531 L0.78536301,16.793207 C0.22214203,16.605489 -0.082270813,15.996711 0.10543709,15.433462 L3.1127861,6.4094316 C3.2770298,5.9165898 3.7636345,5.6219063 4.2600315,5.6816922 L4.2778309,5.6842919 z M21.159981,5.0150004 L24.653946,12.282437 24.132329,12.614198 23.61059,11.524124 23.231144,11.47673 21.997943,9.4229672 C21.997943,9.422967 21.571066,9.7547286 21.697548,10.307664 21.824031,10.860601 22.456441,12.551006 22.456441,12.551006 22.456441,12.551006 22.883318,13.11974 23.199523,13.246126 23.515729,13.372511 24.306241,13.64108 24.306241,13.64108 L24.304347,13.644866 24.417166,13.644866 C24.563581,13.644866 24.682273,13.749438 24.682273,13.878433 24.682273,14.007428 24.563581,14.112 24.417166,14.112 L20.401107,14.112 C20.254693,14.112 20.136,14.007428 20.136001,13.878433 20.136,13.749438 20.254693,13.644866 20.401107,13.644866 L20.733471,13.644866 20.742091,13.643761 C21.064542,13.599233 21.697583,13.431641 21.602688,12.803354 21.476161,11.965637 20.890964,10.497104 20.890964,10.497104 20.890964,10.497104 20.66955,9.8495055 20.843519,9.2491725 21.017486,8.64884 21.428684,8.411568 21.428684,8.4115682 L20.606289,6.8311785 20.796073,5.8990961 20.511398,5.3463821 z M29.471,2.1330002 C30.744017,2.1330002 31.776,3.1647599 31.776,4.4375003 31.776,5.7102404 30.744017,6.7420002 29.471,6.7420004 28.197984,6.7420002 27.166001,5.7102404 27.166,4.4375003 27.166001,3.1647599 28.197984,2.1330002 29.471,2.1330002 z M8.2210014,0.05 C9.4940165,0.05 10.526001,1.0817597 10.526001,2.3544996 10.526001,3.6272405 9.4940165,4.6590002 8.2210014,4.6590002 6.9479836,4.6590002 5.9160011,3.6272405 5.9160011,2.3544996 5.9160011,1.0817597 6.9479836,0.05 8.2210014,0.05 z";
	}
	public class IconeProgramacaoIndustria : IIcone
	{
		public string Data => "M47.561489,54.00919C48.22168,54.00919 48.756866,54.544613 48.756866,55.20509 48.756866,55.86557 48.22168,56.40099 47.561489,56.40099 46.901302,56.40099 46.366116,55.86557 46.366116,55.20509 46.366116,54.544613 46.901302,54.00919 47.561489,54.00919z M43.10405,54.00919C43.764236,54.00919 44.299423,54.544613 44.299423,55.20509 44.299423,55.86557 43.764236,56.40099 43.10405,56.40099 42.443863,56.40099 41.908676,55.86557 41.908676,55.20509 41.908676,54.544613 42.443863,54.00919 43.10405,54.00919z M38.510433,54.00919C39.170616,54.00919 39.705807,54.544613 39.705807,55.20509 39.705807,55.865566 39.170616,56.40099 38.510433,56.40099 37.850243,56.40099 37.315056,55.865566 37.315056,55.20509 37.315056,54.544613 37.850243,54.00919 38.510433,54.00919z M33.985233,54.00919C34.64542,54.00919 35.180611,54.544613 35.180611,55.20509 35.180611,55.865566 34.64542,56.40099 33.985233,56.40099 33.325047,56.40099 32.78986,55.865566 32.78986,55.20509 32.78986,54.544613 33.325047,54.00919 33.985233,54.00919z M29.521996,54.00919C30.182182,54.00919 30.717369,54.544613 30.717369,55.20509 30.717369,55.865566 30.182182,56.40099 29.521996,56.40099 28.861807,56.40099 28.32662,55.865566 28.326618,55.20509 28.32662,54.544613 28.861807,54.00919 29.521996,54.00919z M24.954229,54.00919C25.614416,54.00919 26.149605,54.544613 26.149605,55.20509 26.149605,55.86557 25.614416,56.40099 24.954229,56.40099 24.294043,56.40099 23.758854,55.86557 23.758854,55.20509 23.758854,54.544613 24.294043,54.00919 24.954229,54.00919z M24.713184,52.303471C23.233423,52.303467,22.033838,53.503593,22.033838,54.984013L22.033838,55.426388C22.033838,56.906811,23.233423,58.106934,24.713184,58.106934L47.802536,58.106934C49.282299,58.106934,50.481884,56.906811,50.481884,55.426388L50.481884,54.984013C50.481884,53.503593,49.282299,52.303467,47.802536,52.303471z M42.287392,49.674206L42.287392,51.053947 49.389153,51.053947 49.389153,49.674206z M34.33255,40.193298C32.430126,40.193298,30.820366,40.748028,30.283587,41.512035L30.274757,41.525795 30.227785,41.584404C30.197329,41.62521,30.169329,41.668617,30.144119,41.714531L26.231256,48.840466C25.937897,49.374718,26.132986,50.045734,26.667002,50.339222L26.877575,50.454952C27.411589,50.74844,28.082308,50.553265,28.375666,50.019012L30.711452,45.765182 30.711452,51.053947 38.572113,51.053947 38.572113,45.186768 39.864025,46.468651C39.945137,46.549133,40.035599,46.614422,40.131878,46.664539L40.143196,46.669735 40.214127,46.712845C40.370441,46.797802,40.549576,46.846054,40.739979,46.846054L44.927837,46.846054 45.451931,49.250225 46.05619,49.118381 45.560841,46.846054 45.588093,46.846054C46.19738,46.846054,46.691303,46.35191,46.691303,45.742355L46.691303,45.501991C46.691303,44.892437,46.19738,44.398289,45.588093,44.398289L45.027245,44.398289 44.970558,44.13826 44.980553,44.133446C45.363808,43.925156 45.623978,43.518917 45.623978,43.051888 45.623978,42.37257 45.073528,41.821873 44.394516,41.821873 43.715504,41.821873 43.165054,42.37257 43.165054,43.051888 43.165054,43.688747 43.648846,44.212563 44.26881,44.275551L44.368587,44.280594 44.394241,44.398289 41.251934,44.398289 38.444847,41.612999 38.438641,41.601093C37.968128,40.791519,36.308144,40.193298,34.33255,40.193298z M12.673066,36.25832L12.673066,45.622841 22.033394,45.622841 22.033394,39.600666 20.905447,41.11903 20.905447,44.494389 13.801013,44.494389 13.801013,37.386772 19.63706,37.386772 20.497553,36.25832z M22.183622,35.317612L18.027079,40.789402 16.199392,38.341927 14.896868,39.374954 18.027138,43.777313 23.598835,36.418873z M34.356777,31.858456C32.343555,31.858459 30.711514,33.49128 30.711514,35.505466 30.711514,37.519653 32.343555,39.152477 34.356777,39.152477 36.370003,39.152477 38.002045,37.519653 38.002045,35.505466 38.002045,33.49128 36.370003,31.858459 34.356777,31.858456z M24.648775,27.35L24.648775,28.668642 49.493454,28.668642 49.493454,27.35z M24.648775,24.385836L24.648775,25.704481 49.493454,25.704481 49.493454,24.385836z M12.673066,23.906452L12.673066,33.270969 22.033394,33.270969 22.033394,27.248796 20.905447,28.767162 20.905447,32.142517 13.801013,32.142517 13.801013,25.034903 19.63706,25.034903 20.497553,23.906452z M22.183622,22.965742L18.027079,28.437534 16.199392,25.990059 14.896868,27.023085 18.027138,31.425447 23.598833,24.067003z M24.648775,18.191284L24.648775,19.50993 49.493454,19.50993 49.493454,18.191284z M24.648775,15.22712L24.648775,16.545765 49.493454,16.545765 49.493454,15.22712z M24.648775,12.081905L24.648775,13.400551 49.493454,13.400551 49.493454,12.081905z M12.673066,11.63329L12.673066,20.997808 22.033394,20.997808 22.033394,14.975633 20.905447,16.493999 20.905447,19.869356 13.801013,19.869356 13.801013,12.761743 19.63706,12.761743 20.497553,11.63329z M22.183622,10.692581L18.027079,16.164371 16.199392,13.716899 14.896868,14.749923 18.027138,19.152287 23.598835,11.793842z M10.117637,5.3446455L19.664505,5.3446455 19.664505,8.4383554 40.335495,8.4383554 40.335495,5.3446455 49.882362,5.3446455C51.143028,5.344646,52.165001,6.367075,52.165001,7.6283059L52.165001,57.66634C52.165001,58.927574,51.143028,59.950001,49.882362,59.950001L10.117637,59.950001C8.8569717,59.950001,7.8350005,58.927574,7.8350005,57.66634L7.8350005,7.6283059C7.8350005,6.367075,8.8569717,5.344646,10.117637,5.3446455z M59.950001,0.05L59.950001,59.950001z M30.137177,0.05C31.3619,0.05,32.354736,1.0432795,32.354736,2.2685511L32.354588,2.2743485 36.905968,2.2743485C38.127239,2.274349,39.124496,3.2338748,39.185635,4.4404898L39.187241,4.5040488 39.188606,4.5040488 39.188606,4.5580072 39.188606,4.7080665 39.188606,6.991725 36.905968,6.991725 23.368385,6.991725 21.085749,6.991725 21.085749,4.7080665 21.085749,4.5580072 21.085749,4.5040488 21.087112,4.5040488 21.088718,4.4404898C21.149855,3.2338748,22.147116,2.274349,23.368385,2.2743485L27.919764,2.2743485 27.919619,2.2685511C27.919619,1.0432795,28.912453,0.05,30.137177,0.05z M0.05,0.05L0.05,59.950001z";
	}
	public class IconeRelatorio : IIcone
	{
		public string Data => "M5.9550009,63.117001 L5.9550009,65.117004 27.984001,65.117004 27.984001,63.117001 z M5.9550009,55.896 L5.9550009,57.896 27.984001,57.896 27.984001,55.896 z M5.9550009,48.798 L5.9550009,50.798 27.984001,50.798 27.984001,48.798 z M36.817411,47.298 L36.817411,49.229137 36.714338,49.241837 C36.293161,49.296047 35.936293,49.365131 35.696642,49.427723 34.98028,49.614815 33.162722,50.194603 32.629244,52.095131 32.09577,53.995663 32.762613,55.296024 33.529482,56.196278 34.296354,57.096527 36.963728,58.096806 38.264074,58.530258 39.564421,58.963715 40.03121,59.763939 39.89784,60.730873 39.764475,61.697811 38.097364,61.864525 36.296885,61.731152 34.496408,61.597782 32.629328,60.597504 32.629328,60.597504 L32.729227,63.3316 C32.729227,63.3316 34.359919,63.960018 35.863626,64.165451 36.106989,64.1987 36.349279,64.223007 36.589262,64.237953 L36.817411,64.249229 36.817411,66.510002 38.58322,66.510002 38.58322,64.122406 38.65365,64.108986 C38.940416,64.049202 39.216516,63.970604 39.479036,63.872231 40.489832,63.493454 41.431746,63.132206 42.231951,61.998501 43.032156,60.864792 42.765127,58.83107 41.964952,57.797363 41.164778,56.763657 39.331041,56.0966 37.563986,55.463051 35.796934,54.829506 35.256857,54.42165 35.213102,53.41531 35.160376,52.202515 36.063456,51.661797 38.130648,51.661797 40.19784,51.661797 41.765055,52.562344 41.765055,52.562344 L41.853098,50.08271 C41.853098,50.08271 41.298197,49.694206 39.864466,49.327431 39.506033,49.235737 39.114256,49.18364 38.71623,49.160202 L38.58322,49.153561 38.58322,47.298 z M28.107002,42.067001 L28.107002,44.067001 40.468004,44.067001 40.468004,42.067001 z M28.107002,35.458 L28.107002,37.458 40.468004,37.458 40.468004,35.458 z M55.83441,33.160999 L55.83441,35.092136 55.731337,35.104836 C55.31016,35.159046 54.953291,35.22813 54.71364,35.290722 53.997278,35.477814 52.17972,36.057602 51.646242,37.95813 51.112768,39.858662 51.779612,41.159023 52.54648,42.059277 53.313353,42.959526 55.980726,43.959805 57.281073,44.393257 58.581419,44.826714 59.048208,45.626938 58.914839,46.593872 58.781473,47.56081 57.114363,47.727524 55.313883,47.594151 53.513407,47.460781 51.646326,46.460503 51.646326,46.460503 L51.746225,49.194599 C51.746225,49.194599 53.376917,49.823017 54.880625,50.028446 55.123987,50.061695 55.366278,50.086002 55.60626,50.100952 L55.83441,50.112228 55.83441,52.373001 57.600218,52.373001 57.600218,49.985401 57.670649,49.971985 C57.957415,49.912197 58.233515,49.833603 58.496035,49.735229 59.50683,49.356453 60.448744,48.995205 61.248949,47.8615 62.049154,46.727791 61.782125,44.694069 60.981951,43.660362 60.181776,42.626656 58.34804,41.959599 56.580984,41.32605 54.813932,40.692505 54.273855,40.284649 54.230101,39.278309 54.177374,38.065514 55.080454,37.524796 57.147646,37.524796 59.214838,37.524796 60.782053,38.425343 60.782053,38.425343 L60.870096,35.945709 C60.870096,35.945709 60.315195,35.557205 58.881464,35.19043 58.523031,35.098736 58.131254,35.046638 57.733229,35.023201 L57.600218,35.01656 57.600218,33.160999 z M22.508957,31.778107 L17.419765,36.180973 23.576458,38.30006 23.592046,38.242447 C23.757831,37.598095 23.846002,36.922596 23.846003,36.226501 23.846002,34.625484 23.379575,33.133404 22.575194,31.87874 z M9.9729671,30.62776 L9.9404144,30.661097 C8.5603266,32.10862 7.7130003,34.068607 7.7130013,36.226501 7.7130003,40.681507 11.324496,44.292999 15.779501,44.292999 18.772708,44.292999 21.385141,42.662716 22.777284,40.241657 L22.780439,40.236012 14.913254,37.229362 z M28.107002,28.482 L28.107002,30.482 40.468004,30.482 40.468004,28.482 z M15.779501,28.16 C14.178483,28.16 12.686406,28.626427 11.43174,29.430807 L11.418043,29.439821 15.77914,35.041431 21.266969,30.314863 21.203235,30.255531 C19.770728,28.953543 17.867786,28.16 15.779501,28.16 z M5.9550009,21.629 L5.9550009,23.629 34.593005,23.629 34.593005,21.629 z M44.05431,14.345 L44.05431,16.344999 59.485003,16.344999 59.485003,14.345 z M0,14.137 L37.118044,14.137 48.406,26.81039 48.406,71.786003 0,71.786003 z M24.972,7.4920001 L24.972,9.4919996 53.610003,9.4919996 53.610003,7.4920001 z M19.017,0 L56.135042,0 67.422998,12.67339 67.422998,57.648998 50.18898,57.648998 50.18898,29.93 59.485003,29.93 59.485003,27.93 50.18898,27.93 50.18898,25.775894 48.130505,23.321001 59.485003,23.321001 59.485003,21.321001 47.124001,21.321001 47.124001,22.120667 38.433033,11.756004 19.017,11.756004 z";
	}
	public class IconeRenegociacaoECobranca : IIcone
	{
		public string Data => "M11.067063,30.071614 C11.112997,30.070746 11.157601,30.07078 11.200821,30.071747 13.966891,30.133631 15.90965,30.009913 18.440421,32.237777 20.18032,33.769432 20.362405,34.043321 20.306296,34.064925 L20.303016,34.064989 20.251731,34.047762 C20.22194,34.037759 20.206075,34.03244 20.206075,34.03244 20.206075,34.03244 20.253894,34.059515 20.28608,34.065318 L20.303016,34.064989 20.306621,34.0662 C20.933476,34.276857 24.284215,35.405623 24.444226,35.517787 24.620801,35.641569 27.327655,35.456165 28.681327,35.456165 30.034999,35.456165 32.742162,34.836923 32.447888,36.63157 32.153615,38.426234 30.859065,38.98256 28.74027,39.230093 26.621474,39.47761 21.795327,39.291615 21.795327,39.291615 21.795327,39.291615 19.441108,39.043738 18.264003,38.363104 17.086888,37.682487 17.086888,37.682487 17.086888,37.682487 17.086888,37.682487 20.1139,39.762047 21.820712,40.07143 23.527518,40.380797 23.189568,40.544911 26.426612,40.359295 29.663663,40.173677 31.107772,39.958062 32.184297,39.150677 33.796357,37.941616 34.340791,35.707045 32.241868,34.602331 31.147967,34.026601 30.461776,34.402756 28.331548,34.402755 26.433507,34.402756 25.101632,34.487208 24.866208,34.487208 24.630784,34.487208 22.501841,33.660337 22.501841,33.660337 22.501841,33.660337 25.797782,32.856019 26.916045,32.361035 28.034316,31.866067 29.799994,32.299168 29.799994,32.299168 29.799994,32.299168 31.56568,31.309232 32.91937,31.309233 34.273057,31.309232 35.450429,32.361035 35.450429,32.361035 35.450429,32.361035 38.510781,30.690514 39.746758,31.371099 40.982747,32.051684 40.982449,32.794135 40.982449,32.794135 40.982449,32.794135 42.453603,32.051684 43.277577,31.989817 44.10156,31.92795 45.455396,31.8042 45.808526,32.732269 46.16166,33.660337 45.572923,35.330873 45.278649,35.454607 44.984376,35.578357 40.805711,37.496361 39.334325,38.23883 37.862935,38.981281 34.802453,41.208652 33.684197,41.951103 32.565942,42.693555 31.094544,42.940957 29.799722,43.621542 28.504901,44.30211 26.856946,44.797569 24.973567,44.178852 23.090188,43.560118 18.911448,43.127131 15.203549,42.137147 11.495649,41.147179 11.319076,40.466561 9.4357042,40.899678 7.5523257,41.332796 5.0803904,42.632132 4.9626822,42.817764 4.8449659,43.003382 0,33.765357 0,33.765357 0,33.765357 8.1732177,30.126306 11.067063,30.071614 z M21.356002,0 L24.350001,0 24.350001,3.1452141 24.740331,3.1537247 C25.534932,3.1771736 26.257598,3.2357941 26.908326,3.3295879 27.775964,3.454649 28.575894,3.6468697 29.30812,3.9062519 L29.30812,8.0610008 C28.947022,7.8294106 28.553327,7.6256104 28.12703,7.4496002 27.700734,7.273592 27.261901,7.1276894 26.810529,7.0118923 26.359157,6.8960972 25.910293,6.8104095 25.463935,6.7548256 25.017579,6.6992455 24.593791,6.6714535 24.192571,6.6714535 23.640895,6.6714535 23.139369,6.720089 22.687999,6.8173561 22.236625,6.9146252 21.855468,7.0512638 21.544522,7.227272 21.233577,7.4032822 20.992846,7.6140308 20.822328,7.8595161 20.651809,8.1050053 20.56655,8.3805981 20.56655,8.6862984 20.56655,9.0197887 20.661839,9.3185425 20.852418,9.5825558 21.042998,9.846571 21.313821,10.096689 21.664887,10.332912 22.015954,10.569136 22.44225,10.800728 22.943776,11.027687 23.4453,11.254646 24.012023,11.488554 24.643943,11.729408 25.506566,12.062901 26.281421,12.417234 26.968509,12.792414 27.655597,13.167591 28.244889,13.591404 28.736382,14.06385 29.227876,14.536296 29.60402,15.075904 29.864812,15.682673 30.125604,16.289443 30.255999,16.995796 30.256001,17.801734 30.255999,18.913373 30.027806,19.846686 29.57142,20.601674 29.115033,21.356662 28.49565,21.968062 27.713273,22.435878 26.930894,22.903694 26.020627,23.2395 24.982472,23.443302 24.852702,23.468776 24.722032,23.49266 24.590459,23.514952 L24.350001,23.552521 24.350001,26.771999 21.356002,26.771999 21.356002,23.745666 21.253835,23.744659 C20.230802,23.724395 19.251929,23.633205 18.317212,23.471092 17.248965,23.28582 16.323653,23.007909 15.541275,22.637363 L15.541275,18.190807 C16.413928,18.867055 17.36181,19.37424 18.384919,19.712363 19.408029,20.050487 20.441169,20.219548 21.48434,20.219548 22.096198,20.219548 22.630322,20.168597 23.086709,20.066697 23.543097,19.964797 23.924254,19.823527 24.230185,19.642886 24.536117,19.462245 24.76431,19.249181 24.914767,19.003694 25.065225,18.758206 25.140452,18.491877 25.140452,18.204703 25.140452,17.81563 25.020087,17.468243 24.779355,17.162542 24.538623,16.856841 24.210124,16.5743 23.79386,16.314918 23.377593,16.055535 22.883592,15.805416 22.311854,15.564562 21.740117,15.323707 21.123242,15.078219 20.46123,14.828101 18.776107,14.179646 17.519788,13.387604 16.692273,12.451973 15.864758,11.516344 15.451001,10.386179 15.451001,9.0614758 15.451001,8.0239468 15.676687,7.1323204 16.128058,6.3865948 16.579431,5.640871 17.193799,5.0271549 17.97116,4.5454445 18.748523,4.0637341 19.648761,3.7093983 20.67187,3.482439 20.799758,3.4540691 20.928588,3.4274731 21.058356,3.4026489 L21.356002,3.3501587 z";
	}
	public class IconeNaturezaOperacao : IIcone
	{
		public string Data => "M42.602001,38.620838 L36.021744,45.111999 42.602001,45.111999 z M37.258991,24.070999 L37.258991,28.458015 27.864004,28.458015 27.864004,37.023018 37.258991,37.023018 37.258991,41.477001 46.442001,32.773788 z M13.823009,13.762998 L4.6399999,22.466213 13.823009,31.169002 13.823009,26.781984 23.218,26.781984 23.218,18.216983 13.823009,18.216983 z M8.776,4.6980001 L8.776,16.497785 15.278837,10.083 15.278837,16.681257 24.427,16.681257 24.427,28.286283 15.334953,28.286283 15.334953,34.942001 8.776,28.353752 8.776,45.111999 35.979164,45.111999 35.979164,38.555744 26.831003,38.555744 26.831003,26.950716 35.92305,26.950716 35.92305,20.295 42.602001,27.003786 42.602001,13.473724 35.341045,4.6980001 z";
	}
	public class IconeObjeto : IIcone
	{
		public string Data => "M23.38219,27.452339 C22.516757,27.452339 21.815185,28.154963 21.815185,29.02169 21.815185,29.88842 22.516757,30.591043 23.38219,30.591043 24.247622,30.591043 24.949193,29.88842 24.949193,29.02169 24.949193,28.154963 24.247622,27.452339 23.38219,27.452339 z M6.6668072,27.452339 C5.8013744,27.452339 5.099802,28.154963 5.099802,29.02169 5.099802,29.88842 5.8013744,30.591043 6.6668072,30.591043 7.5322399,30.591043 8.2338114,29.88842 8.2338114,29.02169 8.2338114,28.154963 7.5322399,27.452339 6.6668072,27.452339 z M8.2899742,20.562811 L6.5167408,25.436384 23.485689,25.410801 21.548263,20.562811 z M7.3793774,18.603001 L22.531707,18.603001 26.051243,26.638382 27.071,27.521105 27.071,34.656384 25.198297,34.656384 25.199433,34.671341 C25.200632,34.695015 25.201239,34.718845 25.201239,34.742813 L25.201239,37.189053 C25.201239,37.95615 24.579918,38.577999 23.813479,38.577999 L22.987049,38.577999 C22.220609,38.577999 21.599288,37.95615 21.599289,37.189053 L21.599289,34.742813 C21.599288,34.718845 21.599895,34.695015 21.601094,34.671341 L21.60223,34.656384 8.446764,34.656384 8.4479008,34.671341 C8.4491005,34.695015 8.449707,34.718845 8.449707,34.742813 L8.449707,37.189053 C8.449707,37.95615 7.8283863,38.577999 7.0619469,38.577999 L6.2355165,38.577999 C5.4690781,38.577999 4.8477564,37.95615 4.8477564,37.189053 L4.8477564,34.742813 C4.8477564,34.718845 4.8483629,34.695015 4.8495617,34.671341 L4.8506985,34.656384 2.9790001,34.656384 2.9790001,27.688726 4.0169907,26.669382 3.9841709,26.669382 z M8.385829,14.579269 L10.173495,14.579269 10.173044,14.588327 C10.173044,15.71553 11.972296,16.629309 14.191783,16.629309 14.261143,16.629309 14.330091,16.628416 14.398587,16.626652 L14.475107,16.623697 14.475107,16.683506 14.480847,16.684383 C14.632507,16.715445 14.746592,16.849754 14.746592,17.010735 L14.746592,17.033882 C14.746592,17.217857 14.597584,17.367001 14.413773,17.367001 L10.258061,17.367001 8.385829,17.367001 4.0571728,17.367001 C3.8733616,17.367001 3.7243538,17.217857 3.7243538,17.033882 L3.7243538,17.010735 C3.7243538,16.826757 3.8733616,16.677616 4.0571728,16.677616 L4.2914524,16.677616 4.2914524,16.62915 4.4859657,16.626652 C6.5408497,16.573751 8.188509,15.736962 8.2926712,14.693357 L8.2946434,14.653744 8.385829,14.653744 z M16.233719,12.782845 C15.983826,12.782845 15.781246,12.985606 15.781246,13.235724 15.781246,13.485844 15.983826,13.688605 16.233719,13.688605 16.483612,13.688605 16.686193,13.485844 16.686192,13.235724 16.686193,12.985606 16.483612,12.782845 16.233719,12.782845 z M14.851163,12.782845 C14.60127,12.782845 14.398691,12.985606 14.398691,13.235724 14.398691,13.485844 14.60127,13.688605 14.851163,13.688605 15.101058,13.688605 15.303637,13.485844 15.303637,13.235724 15.303637,12.985606 15.101058,12.782845 14.851163,12.782845 z M13.452519,12.782845 C13.202626,12.782845 13.000047,12.985606 13.000047,13.235724 13.000047,13.485844 13.202626,13.688605 13.452519,13.688605 13.702414,13.688605 13.904992,13.485844 13.904992,13.235724 13.904992,12.985606 13.702414,12.782845 13.452519,12.782845 z M25.657011,11.703883 C25.576302,11.703883 25.510874,11.76898 25.510874,11.849279 L25.510874,15.966525 C25.510874,16.046825 25.576302,16.111923 25.657011,16.111923 L25.790234,16.111923 C25.870943,16.111923 25.93637,16.046825 25.93637,15.966525 L25.93637,11.849279 C25.93637,11.76898 25.870943,11.703883 25.790234,11.703883 z M24.683401,11.703883 C24.602582,11.703883 24.537066,11.76898 24.537066,11.849279 L24.537066,15.966525 C24.537066,16.046825 24.602582,16.111923 24.683401,16.111923 L24.816807,16.111923 C24.897627,16.111923 24.963143,16.046825 24.963143,15.966525 L24.963143,11.849279 C24.963143,11.76898 24.897627,11.703883 24.816807,11.703883 z M23.668648,11.703883 C23.58794,11.703883 23.522512,11.76898 23.522512,11.849279 L23.522512,15.966525 C23.522512,16.046825 23.58794,16.111923 23.668648,16.111923 L23.801872,16.111923 C23.882581,16.111923 23.948008,16.046825 23.948008,15.966525 L23.948008,11.849279 C23.948008,11.76898 23.882581,11.703883 23.801872,11.703883 z M22.694839,11.703883 C22.614131,11.703883 22.548704,11.76898 22.548704,11.849279 L22.548704,15.966525 C22.548704,16.046825 22.614131,16.111923 22.694839,16.111923 L22.828063,16.111923 C22.908772,16.111923 22.974199,16.046825 22.974199,15.966525 L22.974199,11.849279 C22.974199,11.76898 22.908772,11.703883 22.828063,11.703883 z M21.751881,11.703883 C21.671172,11.703883 21.605745,11.76898 21.605745,11.849279 L21.605745,15.966525 C21.605745,16.046825 21.671172,16.111923 21.751881,16.111923 L21.885105,16.111923 C21.965814,16.111923 22.031241,16.046825 22.031241,15.966525 L22.031241,11.849279 C22.031241,11.76898 21.965814,11.703883 21.885105,11.703883 z M26.772226,8.5560303 C26.450114,8.5560303 26.18899,8.8156538 26.18899,9.1359129 26.18899,9.4561739 26.450114,9.7157965 26.772226,9.7157965 27.09434,9.7157965 27.355464,9.4561739 27.355464,9.1359129 27.355464,8.8156538 27.09434,8.5560303 26.772226,8.5560303 z M21.448914,5.5765119 C21.280925,5.5765119 21.144743,5.7120166 21.144743,5.8791699 L21.144743,6.0731521 C21.144743,6.2403049 21.280925,6.3758101 21.448914,6.3758101 L28.037326,6.3758101 C28.205315,6.3758101 28.341496,6.2403049 28.341496,6.0731521 L28.341496,5.8791699 C28.341496,5.7120166 28.205315,5.5765119 28.037326,5.5765119 z M21.448914,3.6290572 C21.280925,3.6290572 21.144743,3.7645624 21.144743,3.9317152 L21.144743,4.1256976 C21.144743,4.2928505 21.280925,4.4283552 21.448914,4.4283552 L28.037326,4.4283552 C28.205315,4.4283552 28.341496,4.2928505 28.341496,4.1256976 L28.341496,3.9317152 C28.341496,3.7645624 28.205315,3.6290572 28.037326,3.6290572 z M1.5544949,3.125422 L1.5544949,12.306817 16.793781,12.306817 16.793781,3.125422 z M21.448914,1.7855052 C21.280925,1.7855055 21.144743,1.9209117 21.144743,2.0879438 L21.144743,2.281785 C21.144743,2.4488168 21.280925,2.5842233 21.448914,2.584223 L28.037326,2.584223 C28.205315,2.5842233 28.341496,2.4488168 28.341496,2.281785 L28.341496,2.0879438 C28.341496,1.9209117 28.205315,1.7855055 28.037326,1.7855052 z M0.96125412,1.6440002 L17.336748,1.6440002 C17.867633,1.644001 18.298,2.0747564 18.298,2.6061203 L18.298,13.244782 C18.298,13.776146 17.867633,14.206902 17.336748,14.206902 L0.96125412,14.206902 C0.43036842,14.206902 0,13.776146 0,13.244782 L0,2.6061203 C0,2.0747564 0.43036842,1.644001 0.96125412,1.6440002 z M20.937164,0 L28.582836,0 C29.107597,-2.0642617E-09 29.533,0.42419794 29.533,0.94747239 L29.533,11.35967 29.533,11.402918 29.533,16.316246 C29.533,16.86508 29.086847,17.309999 28.53649,17.309999 L20.983509,17.309999 20.938804,17.308872 20.937164,17.309999 C20.412402,17.309999 19.986999,16.885801 19.987,16.362528 L19.987,16.316246 19.987,11.35967 19.987,0.94747239 C19.986999,0.42419794 20.412402,-2.0642617E-09 20.937164,0 z";
	}
	public class IconePlanoConta : IIcone
	{
		public string Data => "M8.87951092049479,2.46771025657654L9.6909008435905,2.46771025657654 9.6909008435905,3.19159030914307C9.6909008435905,3.19159030914307 10.1463413648307,3.23335456848145 10.5529442243278,3.34472584724426 10.9595489911735,3.45609617233276 11.073419611901,3.56742238998413 11.073419611901,3.56742191314697L11.073419611901,5.00132274627686C11.073419611901,5.00132274627686 10.4228191785514,4.50016260147095 9.47950748726726,4.47232103347778 8.53619579598308,4.4444785118103 8.47113804146647,4.83427047729492 8.47113804146647,5.01524496078491 8.47113804146647,5.19621849060059 8.51997761055827,5.54423904418945 9.52832607552409,5.87834548950195 10.5366764478385,6.21245193481445 11.5775509290397,6.97810935974121 11.5775509290397,7.63239717483521 11.5775509290397,8.28668403625488 11.3661327771842,9.14972114562988 10.6667919568717,9.45598220825195 9.96745113655925,9.76224517822266 9.723493617028,9.70657634735107 9.723493617028,9.70657634735107L9.723493617028,10.6531572341919 8.84525303170085,10.6531572341919 8.84525303170085,9.77616882324219C8.84525303170085,9.77616882324219 7.95074657723308,9.69264316558838 7.67426113411784,9.55343437194824 7.39777759835124,9.41422557830811 7.20265964791179,9.31677722930908 7.20265964791179,9.31677722930908L7.20265964791179,7.79939603805542C7.20265964791179,7.79939651489258 8.03207973763347,8.52328491210938 9.13801006600261,8.42583847045898 10.2439403943717,8.32839298248291 10.2602043561637,7.91076374053955 10.0162487439811,7.67410802841187 9.77229503914714,7.4374532699585 8.86152843758464,7.14511442184448 8.30856327340007,6.85277366638184 7.7555981092155,6.56043434143066 7.33274273201823,6.28201580047607 7.08878711983562,5.46068048477173 6.84483341500163,4.63934564590454 7.17857364937663,4.11526918411255 7.61768726631999,3.76724600791931 8.05680469796062,3.41922235488892 8.56097797676921,3.28001356124878 8.56097797676921,3.28001379966736L8.87951092049479,3.23480558395386z M9.29299930855632,1.72000110149384C6.59453586861491,1.72000110149384 4.40699962899089,3.90753746032715 4.40699915215373,6.60600090026855 4.40699962899089,9.13580989837646 6.32963947579265,11.2165632247925 8.7934341840446,11.4667749404907L8.80399898812175,11.4675779342651 8.80399898812175,25.0210018157959 8.81299785897136,25.0210018157959 8.81299785897136,25.2220020294189 13.3789978437126,25.2220020294189 13.3789978437126,32.6620025634766 13.3799982480705,32.6620025634766 13.3799982480705,32.6669998168945 23.2656307630241,32.6669998168945 23.2707119397819,32.6954536437988C23.4805145673454,33.7207412719727 24.3876896314323,34.492000579834 25.4750004224479,34.492000579834 26.7176399640739,34.492000579834 27.7250004224479,33.4846420288086 27.7249985150993,32.242000579834 27.7250004224479,30.999361038208 26.7176399640739,29.9920024871826 25.4750004224479,29.9920024871826 24.3876896314323,29.9920024871826 23.4805145673454,30.7632617950439 23.2707119397819,31.7885494232178L23.2567005567253,31.8670024871826 14.1789980344474,31.8670024871826 14.1789980344474,25.2220020294189 18.6306286267936,25.2220020294189 18.6407108716667,25.2784557342529C18.8505154065788,26.3037433624268 19.7576885633171,27.0750026702881 20.8449993543327,27.0750026702881 22.0876408033073,27.0750026702881 23.0949993543327,26.0676422119141 23.0949993543327,24.8250026702881 23.0949993543327,23.5823612213135 22.0876408033073,22.5750026702881 20.8449993543327,22.5750026702881 19.7576885633171,22.5750026702881 18.8505154065788,23.3462619781494 18.6407108716667,24.3715476989746L18.6317005567253,24.4220027923584 9.60399822518229,24.4220027923584 9.60399822518229,17.101001739502 18.6243801526725,17.101001739502 18.6407108716667,17.192455291748C18.8505154065788,18.2177410125732 19.7576885633171,18.9890003204346 20.8449993543327,18.9890003204346 22.0876408033073,18.9890003204346 23.0949993543327,17.9816417694092 23.0949993543327,16.7390003204346 23.0949993543327,15.4963607788086 22.0876408033073,14.4890012741089 20.8449993543327,14.4890012741089 19.7576885633171,14.4890012741089 18.8505154065788,15.2602605819702 18.6407108716667,16.2855472564697L18.637950938195,16.3010025024414 9.60399822518229,16.3010025024414 9.60399822518229,11.4811134338379 9.79256443306804,11.4667749404907C12.2563600949943,11.2165632247925 14.1789989881217,9.13580989837646 14.1789989881217,6.60600090026855 14.1789989881217,3.90753746032715 11.9914627484977,1.72000110149384 9.29299930855632,1.72000110149384z";
	}
	public class IconeRelacaoProdutoPessoa : IIcone
	{
		public string Data => "M21.894844,51.326004 L24.469947,52.693836 C24.469946,52.693836 23.717819,53.422604 22.533103,53.285927 21.348389,53.149254 21.074993,52.69384 21.074993,52.69384 21.074993,52.69384 21.075172,52.443241 21.325773,52.033169 21.576372,51.623096 21.894844,51.326004 21.894844,51.326004 z M18.591959,49.640995 L18.774093,49.822922 20.801958,50.826088 19.958906,52.101807 C19.958906,52.101807 19.161582,52.534161 18.273136,51.896301 17.384689,51.258442 17.772013,50.734558 17.772013,50.734558 z M15.298197,48.166996 L17.517969,49.386055 16.629954,50.433433 C16.629954,50.433433 15.60911,50.711456 15.098688,50.369396 14.588265,50.027336 14.411057,49.321842 14.566387,49.043915 14.721717,48.765987 15.298197,48.166996 15.298197,48.166996 z M11.929955,45.709 L12.104092,45.797142 12.386466,46.392475 14.126991,47.472572 C14.126991,47.472572 13.757052,48.707367 12.973937,48.575085 12.190822,48.442806 11.734004,48.090073 11.625237,47.538898 11.516472,46.987724 11.494951,46.260174 11.494951,46.260174 z M0.00030708313,28.250997 L9.7759209,33.332382 9.5449352,34.179192 13.239748,35.872498 C13.239748,35.872498 14.548272,36.642071 15.933823,35.872498 17.319372,35.102924 18.320253,33.717232 18.320253,33.717232 L18.935577,33.486282 C18.935577,33.486282 18.39712,34.795002 18.39712,35.487854 18.39712,36.18071 17.627367,37.412445 17.858294,38.028316 18.089219,38.644184 18.166195,39.952492 19.397802,40.029472 20.629407,40.106453 22.400097,40.106544 23.092884,38.95182 23.785671,37.7971 24.478632,36.565655 26.326076,37.48946 28.17352,38.413269 34.947869,42.031509 34.947869,42.031509 34.947869,42.031509 36.179361,43.18576 35.794475,44.032555 35.409588,44.879349 34.255045,45.495167 34.255046,45.495167 34.255045,45.495167 33.100381,46.110561 33.023403,46.803375 32.946425,47.496189 32.638515,47.958275 31.791762,48.574116 30.945007,49.189953 30.252208,49.805466 30.252209,49.805466 30.252208,49.805466 29.636387,51.576366 29.020566,51.653347 28.404745,51.730324 27.788924,51.96109 27.557992,52.115051 27.327058,52.269009 26.403327,52.499641 26.403326,52.499641 L26.028476,52.602997 12.980812,45.618286 C12.980812,45.618286 11.546644,43.41592 10.007092,43.108013 8.4675388,42.800102 6.4661198,42.800102 6.4661198,42.800102 L5.1575012,45.494308 3.3870144,45.032444 2.7711945,45.879196 0,44.570579 z M48.85327,26.063007 L48.943951,44.463345 44.548847,46.757996 44.095722,45.723343 42.237912,46.353195 40.697289,42.214176 C40.697289,42.214176 38.205104,42.079208 37.38948,42.079208 36.573856,42.079208 36.528543,42.304153 36.528543,42.304153 36.528543,42.304153 35.984794,41.269402 34.625421,40.639549 33.266047,40.009701 26.333242,36.320576 26.333242,36.320576 26.333242,36.320576 24.430119,35.645733 23.659807,36.455543 22.889496,37.26535 22.617621,38.120148 21.801997,38.615028 20.986372,39.109913 20.578561,38.974945 20.306686,38.929954 20.034811,38.884968 19.627292,38.569626 19.627292,38.569626 19.627292,38.569626 19.038539,38.119827 19.400733,36.904793 19.762928,35.689758 19.445799,35.105545 20.171022,34.16074 20.896245,33.215931 23.252207,31.910805 23.252206,31.910805 23.252207,31.910805 24.249056,31.055843 26.15225,31.685759 28.055444,32.315674 30.638061,33.080894 30.638061,33.080894 30.638061,33.080894 34.263556,33.26086 34.897957,33.26086 35.532358,33.26086 36.845984,33.170876 36.845984,33.170876 L36.709895,32.496006 38.567717,31.731153 38.386186,31.056282 z M26.864972,13.394 L37.795971,13.394 37.795971,24.325001 26.864972,24.325001 z M13.624971,13.394 L24.555971,13.394 24.555971,24.325001 13.624971,24.325001 z M26.864972,0 L37.795971,0 37.795971,10.931 26.864972,10.931 z M13.624971,0 L24.555971,0 24.555971,10.931 13.624971,10.931 z";
	}
	public class IconeRenegociacaoPagamento : IIcone
	{
		public string Data => "M10349810 13953930l322010 0 0 121300c38850,61020 58360,150360 58360,227030l0 133240 0 130720 0 92910c0,18280 -14960,33230 -33250,33230l-19640 0c-18280,0 -33230,-14950 -33230,-33230l0 -64160 -14960 0 0 116870c0,20390 -16680,37050 -37060,37050l-21900 0c-20390,0 -37070,-16660 -37070,-37050l0 -79230 -14320 0 0 113710c0,23200 -18990,42180 -42200,42180l-24930 0c-23200,0 -42190,-18980 -42190,-42180l0 -95790 -14330 0c0,111960 0,223930 0,335900 0,23210 -18980,42180 -42190,42180l-24930 0c-23200,0 -42190,-18970 -42190,-42180 0,-171310 0,-316410 0,-487700 0,-29630 -44800,-28020 -44800,30840l0 36650 0 60110 0 68990c0,21690 -17730,39420 -39410,39420l-23300 0c-21690,0 -39420,-17730 -39420,-39420l0 -129100 0 -94710 3460 -41390c18360,-117520 107590,-274160 207490,-351210l0 -124980zm-578750 1187950l1326900 900 0 589140 -1326900 0 0 -590040zm636900 320580l51770 14090c33750,10190 50700,23420 50700,39520 0,19910 -16950,31710 -50700,35340 -51230,1030 -73750,1890 -124470,-10360 -27110,-6570 -47560,-13750 -61370,-21510l0 68490c41390,14910 86020,22510 134070,22680l0 52120 51770 0 0 -53550c46480,-3630 82260,-13830 107310,-30530 25060,-16800 37640,-38800 37640,-66110 0,-23710 -11260,-43960 -33630,-60660 -22500,-16690 -59670,-31620 -111320,-44620l-51770 -14320c-32900,-11390 -49250,-25550 -49250,-42540 0,-19700 16350,-31680 49250,-35810l51770 -930c41270,3120 78520,11630 111530,25640l0 -66690c-24530,-9560 -61690,-15110 -111530,-16820l0 -44930 -51770 0 0 45960c-43310,2920 -78050,13480 -104160,31720 -26280,18200 -39450,40210 -39450,66100 0,24040 10140,44470 30390,61160 20200,16690 57900,32210 113220,46560z";
	}
	public class IconeSaida : IIcone
	{
		public string Data => "M19.291937,22.701217 C20.566603,22.701217 21.599926,23.734271 21.599926,25.008607 21.599926,26.282945 20.566603,27.316001 19.291937,27.316001 18.017269,27.316001 16.983946,26.282945 16.983946,25.008607 16.983946,23.734271 18.017269,22.701217 19.291937,22.701217 z M11.273459,22.701217 C12.547793,22.701217 13.580842,23.734271 13.580842,25.008607 13.580842,26.282945 12.547793,27.316001 11.273459,27.316001 9.9991273,27.316001 8.9660759,26.282945 8.9660759,25.008607 8.9660759,23.734271 9.9991273,22.701217 11.273459,22.701217 z M11.945,12.489 L17.889999,12.489 17.889999,18.338 11.945,18.338 z M13.525807,5.7860003 L14.284975,7.7147937 13.463064,11.160479 14.38,11.413465 11.028453,11.413465 11.028453,12.393559 9.5107612,12.867999 7.9299998,7.2091131 z M15.770844,4.995 L21.493999,6.5760639 19.976479,12.268 18.838181,11.888293 18.838181,11.414014 17.067501,11.414014 14.285,10.59193 z M0.86560907,3.8389999 L4.985351,3.8389999 10.004228,19.47122 19.984289,19.47122 23.445162,7.7616615 25.637999,7.7616615 21.484177,20.740451 8.5618362,20.740451 3.8320813,6.2039943 0.86560907,6.2039943 C-0.29792816,5.4932323 -0.27910618,4.7036456 0.86560907,3.8389999 z M12.608634,0 L18.268999,1.5811186 17.510229,4.3949996 15.107061,3.7306987 14.601131,5.9440002 14.063581,4.8057283 11.028,5.532956 z";
	}
	public class IconeSimuladorCusto : IIcone
	{
		public string Data => "M4.5991322,59.268375 L4.5819518,59.285557 4.5991322,59.302738 z M63.36937,14.864185 L63.39402,60.957783 63.390526,60.957783 63.390526,60.967281 6.2636968,60.967281 11.14826,65.851761 8.8529874,68.147003 0.0079998684,59.302151 0.016535905,59.293613 0,59.277081 8.8452093,50.432011 11.140425,52.727184 6.2982289,57.569309 59.996002,57.569309 59.996002,18.52561 z M30.119078,14.479 L33.403927,14.479 33.403927,18.339312 C36.568779,18.480715 38.931168,18.961485 40.491097,19.781624 L40.491097,25.508459 C38.391194,24.306534 36.028805,23.571236 33.403927,23.302567 L30.119078,23.387409 C28.034174,23.740919 26.991722,24.766094 26.991722,26.462931 26.991722,27.919386 28.034174,29.135454 30.119078,30.111137 L33.403927,31.341345 C36.688774,32.458431 39.043663,33.734596 40.468598,35.169838 41.893532,36.605083 42.605999,38.340809 42.605999,40.377018 42.605999,42.724312 41.811035,44.615578 40.221111,46.050823 38.631184,47.486065 36.358791,48.359234 33.403927,48.670322 L33.403927,53.272999 30.119078,53.272999 30.119078,48.797577 C27.074219,48.78344 24.239351,48.132984 21.614472,46.846222 L21.614472,40.970909 C22.484432,41.635506 23.781872,42.25061 25.506792,42.816227 27.231712,43.381836 28.76914,43.707062 30.119078,43.791901 L33.601868,43.675846 C35.614807,43.332104 36.621277,42.331478 36.621277,40.673965 36.621277,39.288212 35.548828,38.156986 33.403927,37.280281 L33.403927,37.301811 30.127424,36.056942 30.119078,36.071285 C26.609242,34.841076 24.213102,33.50835 22.93066,32.073109 21.64822,30.637865 21.007,28.887997 21.007,26.823509 21.007,24.603479 21.839462,22.71221 23.504385,21.149702 25.169308,19.587194 27.374205,18.678679 30.119078,18.424152 z M59.255867,8.8442554 L59.255867,8.8786182 59.273049,8.8614368 z M55.002014,0 L63.847,8.8448429 63.838463,8.8533802 63.855003,8.8699131 55.009789,17.714987 52.714577,15.419806 57.55677,10.577686 3.8590003,10.577686 3.8590003,49.621384 0.48563202,53.28281 0.46097745,7.1892161 0.46447675,7.1892161 0.46447675,7.1797147 57.591301,7.1797147 52.706741,2.2952342 z";
	}
	public class IconeSincronizacaoEcommerce : IIcone
	{
		public string Data => "M54.5753116607666,65.3555834293365L62.9074859619141,65.3555834293365 62.9074859619141,70.9179689884186 65.6678247451782,70.9179689884186 65.6678247451782,65.3555834293365 73.9999997615814,65.3555834293365 73.9999997615814,84.0000002384186 54.5753116607666,84.0000002384186z M32.2879257202148,65.3555834293365L40.6201057434082,65.3555834293365 40.6201057434082,70.9179689884186 43.3804359436035,70.9179689884186 43.3804359436035,65.3555834293365 51.7126197814941,65.3555834293365 51.7126197814941,84.0000002384186 32.2879257202148,84.0000002384186z M10,65.3555834293365L18.3321800231934,65.3555834293365 18.3321800231934,70.9179689884186 21.0925140380859,70.9179689884186 21.0925140380859,65.3555834293365 29.424690246582,65.3555834293365 29.424690246582,84.0000002384186 10,84.0000002384186z M54.5753116607666,44.4445192813873L62.9074859619141,44.4445192813873 62.9074859619141,50.0069010257721 65.6678247451782,50.0069010257721 65.6678247451782,44.4445192813873 73.9999997615814,44.4445192813873 73.9999997615814,63.0889360904694 54.5753116607666,63.0889360904694z M32.2879257202148,44.4445192813873L40.6201057434082,44.4445192813873 40.6201057434082,50.0069010257721 43.3804397583008,50.0069010257721 43.3804397583008,44.4445192813873 51.7126197814941,44.4445192813873 51.7126197814941,63.0889360904694 32.2879257202148,63.0889360904694z M10,44.4445192813873L18.3321800231934,44.4445192813873 18.3321800231934,50.0069010257721 21.0925140380859,50.0069010257721 21.0925140380859,44.4445192813873 29.4246940612793,44.4445192813873 29.4246940612793,63.0889360904694 10,63.0889360904694z M84.0000004768372,0L84.0000004768372,84.0000002384186 M41.7915496826172,0L64.1741380691528,20.2803328037262 54.2260646820068,20.2803328037262 54.2260646820068,39.299854516983 29.5408210754395,39.299854516983 29.5408210754395,20.2803328037262 20.0539894104004,20.2803328037262z M0,0L0,84.0000002384186z";
	}
	public class IconeTabelaPreco : IIcone
	{
		public string Data => "M13.32,48.249667 L25.017998,48.249667 25.017998,51.840666 13.32,51.840666 z M0.3395609,48.249667 L12.037561,48.249667 12.037561,51.840666 0.3395609,51.840666 z M13.32,42.249667 L25.017998,42.249667 25.017998,45.840666 13.32,45.840666 z M0.3395609,42.249667 L12.037561,42.249667 12.037561,45.840666 0.3395609,45.840666 z M13.32,36.097483 L25.017998,36.097483 25.017998,39.688483 13.32,39.688483 z M0.3395609,36.097483 L12.037561,36.097483 12.037561,39.688483 0.3395609,39.688483 z M13.32,30.082666 L25.017998,30.082666 25.017998,33.673667 13.32,33.673667 z M0.3395609,30.082666 L12.037561,30.082666 12.037561,33.673667 0.3395609,33.673667 z M13.32,24.15185 L25.017998,24.15185 25.017998,27.74285 13.32,27.74285 z M0.3395609,24.15185 L12.037561,24.15185 12.037561,27.74285 0.3395609,27.74285 z M41.445372,23.726999 L41.485838,26.347944 C41.485838,26.347944 42.955077,26.306879 44.484902,26.803568 45.952441,27.280055 46.666464,27.74892 46.666464,27.74892 L46.275208,32.319201 C46.275208,32.319201 41.190886,29.829745 37.84521,31.195062 32.311177,33.453411 36.447165,35.657778 39.479133,35.689025 43.088903,35.726217 47.866058,37.224192 48.153986,42.202801 48.488913,47.993483 42.247272,49.515083 42.247272,49.515083 L42.327954,52.18759 37.600763,52.212997 37.583231,49.890339 C37.583231,49.890339 31.956568,48.444077 31.956568,48.444077 L32.277194,44.149733 C32.277194,44.149733 39.950196,47.125689 42.240966,44.740242 43.849964,43.064745 42.395329,40.4877 39.90046,39.962555 36.771189,39.303889 35.427385,39.799503 32.211274,37.220591 29.799434,35.286645 30.272627,31.287969 32.072342,29.46221 34.485352,27.014283 37.353566,26.450058 37.353565,26.450058 L37.351108,23.801144 z M39.180999,18.395 L50.879,18.395 50.879,21.986 39.180999,21.986 z M26.317277,18.395 L38.015281,18.395 38.015281,21.986 26.317277,21.986 z M13.351999,18.395 L25.049998,18.395 25.049998,21.986 13.351999,21.986 z M0.37156049,18.395 L12.06956,18.395 12.06956,21.986 0.37156049,21.986 z M39.231997,12.060999 L50.929998,12.060999 50.93,15.651999 39.231997,15.651999 z M26.368278,12.060999 L38.066279,12.060999 38.06628,15.651999 26.368278,15.651999 z M13.403,12.060999 L25.100997,12.060999 25.100999,15.651999 13.403,15.651999 z M0.42255918,12.060999 L12.120561,12.060999 12.120561,15.651999 0.42256108,15.651999 z M39.231997,6.0299988 L50.929998,6.0299988 50.929998,9.6209993 50.846998,9.6209993 50.846998,9.6349993 39.148997,9.6349993 39.148997,6.0440006 39.231997,6.0440006 z M26.368278,6.0299988 L38.066279,6.0299988 38.066279,9.6209993 37.983279,9.6209993 37.983279,9.6349993 26.285277,9.6349993 26.285277,6.0440006 26.368278,6.0440006 z M13.403,6.0299988 L25.100999,6.0299988 25.100999,9.6209993 25.017998,9.6209993 25.017998,9.6349993 13.32,9.6349993 13.32,6.0440006 13.403,6.0440006 z M0.42256108,6.0299988 L12.120561,6.0299988 12.120561,9.6209993 12.037561,9.6209993 12.037561,9.6349993 0.3395609,9.6349993 0.3395609,6.0440006 0.42256108,6.0440006 z M39.231997,0 L50.929998,0 50.929998,3.5909996 39.231997,3.5909996 z M26.368278,0 L38.066279,0 38.06628,3.5909996 26.368278,3.5909996 z M13.403,0 L25.100999,0 25.101,3.5909996 13.403,3.5909996 z M0.42255918,0 L12.120561,0 12.120561,3.5909996 0.42256108,3.5909996 z";
	}
	public class IconeTransacaoPbm : IIcone
	{
		public string Data => "M29.416817,57.878456 L27.710137,62.918259 C28.375393,62.896012 28.829968,62.792176 29.073853,62.606751 29.317738,62.421326 29.562718,61.965187 29.8088,61.238327 L30.377632,59.558396 C30.638844,58.787033 30.715775,58.319763 30.608441,58.156594 30.501106,57.993424 30.103897,57.900711 29.416817,57.878456 z M31.722469,51.069718 L30.366203,55.07486 C30.568169,55.06744 30.724583,55.063732 30.835442,55.063732 31.293648,55.063732 31.623882,54.950627 31.826138,54.724407 32.028397,54.498192 32.310356,53.851067 32.672028,52.783028 32.862911,52.219342 32.944927,51.824391 32.918079,51.598171 32.891232,51.371956 32.804711,51.22918 32.65852,51.169846 32.512325,51.110512 32.200306,51.077133 31.722469,51.069718 z M20.326557,51.069718 L18.774386,55.653378 C18.904873,55.660797 19.018154,55.664505 19.114229,55.664505 19.542877,55.664505 19.876143,55.558815 20.114023,55.347431 20.351904,55.136048 20.583838,54.696594 20.809824,54.029072 L21.310932,52.549393 C21.519367,51.933789 21.558918,51.533276 21.429585,51.347851 21.300251,51.16243 20.932575,51.069718 20.326557,51.069718 z M40.636223,47.98798 L46.675163,47.98798 C46.487633,49.070854 46.240051,50.34726 45.932419,51.817204 L45.043659,56.398781 48.964359,47.98798 55.036553,47.98798 48.937164,66 44.857624,66 48.969902,53.839939 43.228027,66 40.334656,66 42.645508,54.118073 38.616371,66 34.536831,66 z M28.098827,47.98798 L32.754826,47.98798 C34.225529,47.98798 35.300755,48.102856 35.980507,48.332607 36.660259,48.562359 37.114338,49.027455 37.342751,49.72789 37.571163,50.428326 37.421795,51.556801 36.894646,53.113312 36.538288,54.165821 36.125401,54.899605 35.655991,55.314663 35.186581,55.729721 34.430096,56.048447 33.38654,56.270836 34.382633,56.515717 34.9767,56.92202 35.168739,57.489762 35.360775,58.057511 35.258289,58.927666 34.861286,60.100235 L34.295914,61.769913 C33.883778,62.986984 33.439858,63.888664 32.964157,64.474953 32.488449,65.061234 31.91116,65.461983 31.232273,65.677185 30.553387,65.892395 29.275356,66 27.398178,66 L21.999436,66 z M16.702915,47.98798 L21.403257,47.98798 C22.674418,47.98798 23.617914,48.088108 24.23375,48.288368 24.849585,48.488625 25.265268,48.777886 25.480806,49.156147 25.696341,49.534413 25.774058,49.992409 25.713953,50.530136 25.653847,51.067864 25.432911,51.900414 25.051148,53.027786 L24.519899,54.596466 C24.130629,55.74609 23.728569,56.584206 23.313723,57.11081 22.898874,57.637406 22.327786,58.041634 21.60046,58.323471 20.873135,58.605316 19.999529,58.746239 18.979643,58.746239 L17.726957,58.746239 15.270607,66 10.603523,66 z M23.957142,17.231053 L24.266373,17.314734 C29.13332,18.696995 33.257008,21.845946 35.900395,26.024494 L36.062142,26.289814 25.662514,36.689507 C25.272062,37.07996 25.258696,37.69965 25.63266,38.073616 26.006624,38.447582 26.626307,38.434216 27.016758,38.043762 L37.018505,28.04195 37.191006,28.401383 C37.758476,29.63646 38.205876,30.938135 38.51825,32.291477 L38.519772,32.298481 28.060352,42.757973 C24.109261,46.709088 17.703364,46.709172 13.752379,42.75816 L13.245186,42.250958 C9.2941991,38.299954 9.2942821,31.894007 13.245371,27.942894 z M45.391174,1.9926377 C43.467659,1.9926261 41.544151,2.7264135 40.076573,4.1940048 L30.099621,14.171034 C34.751686,16.147959 38.843697,19.548147 41.835716,25.743786 L51.739758,15.85729 C54.674923,12.922105 54.674889,8.1631939 51.739685,5.2279642 L50.705814,4.1940854 C49.238213,2.7264669 47.31469,1.9926518 45.391174,1.9926377 z M66,2.7842617E-09 L66,66 z M45.378677,2.7842617E-09 C47.967934,-6.0552747E-05 50.557175,0.98765652 52.532662,2.96316 L53.039856,3.4703605 C56.990841,7.4213726 56.990692,13.82738 53.039517,17.778582 L41.116062,29.702118 40.99733,29.336373 C38.715199,22.591742 33.459313,17.218186 26.790241,14.776333 L26.510365,14.677757 38.224541,2.9634993 C40.200131,0.98789971 42.789414,6.1040721E-05 45.378677,2.7842617E-09 z M0,2.7842617E-09 L0,66 z";
	}
	public class IconeValePresente : IIcone
	{
		public string Data => "M3922170 7628639l6517693 0 0 2901909 -6517693 0 0 -2901909zm10077598 -2127774c-298691,1010199 -889325,1209089 -1503400,1415675 -944257,317633 -1085848,251691 -1565908,348414l-10892 -75649c-3315,-346638 -2605,-735658 2249,-1098515 4380,-350426 12786,-671374 24033,-904715 34569,-709021 131291,-1288053 319409,-1702527 229316,-504922 572994,-778278 1068208,-770583 157810,2486 308517,24861 450582,64758 143959,321421 355517,770109 369131,1129533 1184,31491 2960,61680 4972,91395 -108206,-145616 -252402,-260334 -422998,-332076 -125254,-52564 -263293,-82516 -410212,-84647 -188946,-2841 -323552,108561 -416842,314081 -134251,295613 -205402,753890 -233815,1337776 -11839,242931 -20007,549672 -24151,868963l0 1657 -2486 237722c213452,-116256 434126,-190603 651604,-263767 427733,-143959 839247,-282472 1012093,-867068 154495,-524219 145735,-1435919 -44395,-1925806 -62153,-160296 -101576,-266608 -144196,-370907 279039,150352 512735,377892 683331,660838 200548,332550 312069,738381 307215,1182215 -2723,241392 -39186,491662 -113533,743235zm2949500 5404615l-3931996 0c300467,285905 724057,778396 1132256,1412005 510841,793550 1048201,1978014 865293,1847078 -117085,-83700 -1351391,-442532 -1356363,-320829 -21073,509184 -241036,855940 -536886,1248394 -9826,13023 -918922,-2608663 -2261434,-3954845l0 7242933 6089131 0 0 -7474735zm428562 -3276841l-6517693 0 0 2901909 6517693 0 0 -2901909zm-8720880 -3445780c1010317,298691 1209208,889207 1415793,1503281 317633,944257 251573,1085966 348414,1565908l-75649 10892c-346638,3315 -735658,2723 -1098515,-2249 -350545,-4380 -671493,-12667 -904834,-24033 -709021,-34569 -1287935,-131291 -1702409,-319409 -504922,-229198 -778278,-572994 -770583,-1068208 2486,-157692 24861,-308517 64758,-450582 321303,-143841 770109,-355517 1129533,-369131 31373,-1065 61680,-2960 91395,-4972 -145735,108206 -260452,252520 -332076,422998 -52564,125254 -82516,263293 -84765,410212 -2841,189064 108561,323552 314200,416960 295613,134133 753772,205284 1337776,233815 242931,11839 549672,20007 868844,24033l1776 0 237722 2486c-116375,-213452 -190603,-434126 -263885,-651604 -143841,-427733 -282472,-839247 -866950,-1012093 -147747,-43567 -289457,-67244 -422051,-72571 -73637,-21073 -133422,-33859 -166808,-33859 -373749,0 -997649,19179 -1336947,150825 -160296,62153 -266727,101576 -370907,144314 150352,-279157 377774,-512735 660838,-683450 332431,-200548 738381,-312069 1182215,-307215 241273,2723 491544,39186 743117,113652zm-4306218 6722621l3931996 0c-300467,285905 -724057,778396 -1132256,1412005 -510841,793550 -1048201,1978014 -865293,1847078 117085,-83700 1351391,-442532 1356363,-320829 21073,509184 241036,855940 536886,1248394 9826,13023 918922,-2608663 2261434,-3954845l0 7242933 -6089131 0 0 -7474735z";
	}
}

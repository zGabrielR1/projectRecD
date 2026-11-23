using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Events;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Icones;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.EnderecosServidor.Services;
using Otus.Modulo.EnderecosServidor.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.EnderecosServidor")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.EnderecosServidor")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.EnderecosServidor
{
	[Module(ModuleName = "EnderecoServidorModulo")]
	public class EnderecoServidorModulo : IModule
	{
		private readonly IUnityContainer _container;

		public EnderecoServidorModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<EnderecosServidorView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EnderecoServidorView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<INavegacaoConfiguracaoService, ConfiguracaoNavegacaoService>(_container, typeof(ConfiguracaoNavegacaoService).Name, Array.Empty<InjectionMember>());
		}
	}
}
namespace Otus.Modulo.EnderecosServidor.Views
{
	public class EnderecoServidorView : UserControl, IComponentConnector
	{
		internal Path Loader;

		internal GradientStop GradientStop1;

		internal GradientStop GradientStop2;

		internal GradientStop GradientStop3;

		internal BeginStoryboard AnimacaoLoader;

		private bool _contentLoaded;

		public EnderecoServidorView(IEventPublisher eventPublisher)
		{
			InitializeComponent();
			ObservableExtensions.Subscribe<LoaderIpEvent>(eventPublisher.GetEvent<LoaderIpEvent>(), (Action<LoaderIpEvent>)delegate(LoaderIpEvent @event)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					if (@event.Mostrar)
					{
						AnimacaoLoader.Storyboard.Begin();
						Loader.ToolTip = "Conectando...";
					}
					else
					{
						AnimacaoLoader.Storyboard.Stop();
						Loader.ToolTip = null;
					}
				});
			});
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.EnderecosServidor;component/views/enderecoservidorview.xaml", UriKind.Relative);
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
				Loader = (Path)target;
				break;
			case 2:
				GradientStop1 = (GradientStop)target;
				break;
			case 3:
				GradientStop2 = (GradientStop)target;
				break;
			case 4:
				GradientStop3 = (GradientStop)target;
				break;
			case 5:
				AnimacaoLoader = (BeginStoryboard)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class EnderecosServidorView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EnderecosServidorView()
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
				Uri resourceLocator = new Uri("/Modulo.EnderecosServidor;component/views/enderecosservidorview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.EnderecosServidor.ViewModels
{
	public class EnderecoIpViewModel : ViewModelDtoBase<string>
	{
		[CustomValidation(typeof(EnderecoIpViewModel), "ValidarValor", ErrorMessage = "O Endereço IP informado não é válido.")]
		public string Ip
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Ip));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Ip), value);
			}
		}

		public EnderecoIpViewModel(string ip)
			: base(ip)
		{
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarValor(object value, ValidationContext context)
		{
			string text = (value as string) ?? string.Empty;
			if (!(text == string.Empty) && !(text == "127.0.0.1"))
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
		}

		public override void ProjectedAsViewModel()
		{
			Ip = base.Dto;
		}
	}
	public class EnderecoServidorViewModel : NavigationBase
	{
		private readonly IConfiguracaoClientService _configuracaoClientService;

		private readonly IAutenticacaoService _autenticacaoService;

		private readonly IWService _wService;

		private readonly IExecutarProcessoService _executarProcessoService;

		private bool _configurando;

		private IDisposable _subscription;

		public Lista<string> Ips
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => Ips));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => Ips), value);
			}
		}

		public EnderecoIpViewModel ViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<EnderecoIpViewModel>((Expression<Func<EnderecoIpViewModel>>)(() => ViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<EnderecoIpViewModel>((Expression<Func<EnderecoIpViewModel>>)(() => ViewModel), value);
			}
		}

		public IIcone Logomarca => (IIcone)new Logomarca();

		public AsyncCommand ConfirmarCommand { get; set; }

		public EnderecoServidorViewModel(IConfiguracaoClientService configuracaoClientService, IAutenticacaoService autenticacaoService, IWService wService, IExecutarProcessoService executarProcessoService, IEventPublisher eventPublisher)
			: base("Endereço do servidor")
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			_configuracaoClientService = configuracaoClientService;
			_autenticacaoService = autenticacaoService;
			_wService = wService;
			_executarProcessoService = executarProcessoService;
			ConfirmarCommand = new AsyncCommand((Func<Task>)Confirmar, (Func<bool>)PodeConfirmar, false, (bool?)null);
			ViewModel = new EnderecoIpViewModel(string.Empty);
			_subscription = ObservableExtensions.Subscribe<MongoDbConfiguradoEvent>(eventPublisher.GetEvent<MongoDbConfiguradoEvent>(), (Action<MongoDbConfiguradoEvent>)delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					((CommandBase<object>)(object)ConfirmarCommand).RaiseCanExecuteChanged();
				});
			});
			Ips = new Lista<string>();
			ConfiguracaoClient val = _configuracaoClientService.ObterConfiguracao();
			if (val.IpValido)
			{
				Ips.Adicionar(val.Ip);
			}
			try
			{
				string nome = Environment.MachineName;
				if (!Ips.Existe((Expression<Func<string, bool>>)((string x) => x.ToLower().Equals(nome.ToLower()))))
				{
					Ips.Adicionar(nome);
				}
				foreach (string ip in HelperIp.GetLocalIpsAddress())
				{
					if (!Ips.Existe((Expression<Func<string, bool>>)((string x) => x.ToLower().Equals(ip.ToLower()))))
					{
						Ips.Adicionar(ip);
					}
				}
				Ips.PosicionarNoPrimeiro();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeConfirmar()
		{
			if (!_configurando && ViewModel != null && ((ViewModelValidationBase)ViewModel).Valido)
			{
				return DadosSistema.BancoDadosConfigurado;
			}
			return false;
		}

		private async Task Confirmar()
		{
			try
			{
				string ip = ViewModel?.Ip ?? string.Empty;
				TaskScheduler contexto = TaskScheduler.FromCurrentSynchronizationContext();
				_configurando = true;
				await Task.Factory.StartNew(delegate
				{
					_wService.TestarConfiguracao(ip, 12221);
					Task.Factory.StartNew(delegate
					{
						_configuracaoClientService.SalvarIp(ip);
						ISessaoService instancia = _autenticacaoService.Instancia;
						_executarProcessoService.AdicionarProcesso((Action)delegate
						{
							((UpdateViewBindableBase)this).DormirSync(5000, (Action)delegate
							{
								RestartService("SincronizadorDigisat");
							});
						}, (Action<object>)delegate
						{
							((NavigationBase)this).FecharView();
							instancia.Inicializar();
							_configurando = false;
						});
					}, CancellationToken.None, TaskCreationOptions.None, contexto);
				}).ContinueWith(delegate(Task x)
				{
					if (x.Exception != null)
					{
						throw x.Exception.InnerException;
					}
				});
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		public static void RestartService(string serviceName, int timeoutMilliseconds = 10000)
		{
			ServiceController serviceController = new ServiceController(serviceName);
			int tickCount = Environment.TickCount;
			TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
			serviceController.Refresh();
			if (serviceController.Status != ServiceControllerStatus.Stopped)
			{
				serviceController.Stop();
				serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				int tickCount2 = Environment.TickCount;
				timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (tickCount2 - tickCount));
				serviceController.Start();
				serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
			}
			else
			{
				serviceController.Start();
			}
		}

		public override void Dispose()
		{
			_subscription?.Dispose();
			_subscription = null;
			((NavigationBase)this).Dispose();
		}
	}
	public class EnderecosServidorViewModel : ModuloBase
	{
		public EnderecosServidorViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloEnderecoServidorRegion, "", (IResolucao)new ResolucaoBase())
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			((ModuloBase)this).KeepAlive = false;
		}
	}
}
namespace Otus.Modulo.EnderecosServidor.Services
{
	public class ConfiguracaoNavegacaoService : INavegacaoConfiguracaoService
	{
		private readonly IRegionManagerService _regionManagerService;

		public ConfiguracaoNavegacaoService(IRegionManagerService regionManagerService)
		{
			_regionManagerService = regionManagerService;
		}

		public void Configurar()
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			IRegionManagerService regionManagerService = _regionManagerService;
			string trabalhoRegion = Regioes.TrabalhoRegion;
			string name = typeof(EnderecosServidorView).Name;
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("Cor", (object)new Cor("#1F4C60"));
			val.Add("ViewLista", (object)typeof(EnderecoServidorView).Name);
			regionManagerService.AbrirVisao(trabalhoRegion, name, true, val);
		}
	}
	public class LoaderIpEvent
	{
		public bool Mostrar { get; set; }
	}
}
namespace Otus.Modulo.EnderecosServidor.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.EnderecosServidor.Properties.Resources", typeof(Resources).Assembly);
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

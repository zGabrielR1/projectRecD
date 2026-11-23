using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DigisatServer.Application.Main.Dto.Auxiliares.Autorizacoes;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Domain.Core;
using DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Configuracoes;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Enderecos;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Juridicas;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Juridicas.Emitentes;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Pessoas.Telefones;
using DigisatServer.Domain.Main.Aggregates.CadastrosAgg.Usuarios;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Criptografia;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.File;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.Distribuicao;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.File;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.Session;
using DigisatServer.Infrastructure.Crosscutting.Projection;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiWdConnect;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiWdConnect.Model;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.CadastrosVO.Municipios;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Data.Core.Interfaces;
using Microsoft.Practices.Unity;
using MongoDB.Bson;
using Otus.Infraestrutura.Events;
using Otus.Infraestrutura.Exceptions;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Icones;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.Autenticacoes.Projections;
using Otus.Modulo.Autenticacoes.Services;
using Otus.Modulo.Autenticacoes.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Autenticacoes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Autenticacoes")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Autenticacoes
{
	[Module(ModuleName = "AutenticacaoModulo")]
	[ModuleDependency("EnderecoServidorModulo")]
	[ModuleDependency("MenuModulo")]
	[ModuleDependency("InicialModulo")]
	public class AutenticacaoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AutenticacaoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			UnityExtensions.RegisterTypeForNavigation<AutenticacoesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PreAutenticacaoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PrimeiroAcessoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AutenticacaoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<BloqueioView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AlterarSenhaView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<IAutenticacaoService, AutenticacaoService>(_container, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<ISessaoService, SessaoService>(_container, (LifetimeManager)new ContainerControlledLifetimeManager(), Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<ISessao>(_container, (InjectionMember[])(object)new InjectionMember[1] { (InjectionMember)new InjectionFactory((Func<IUnityContainer, object>)((IUnityContainer container) => UnityContainerExtensions.Resolve<IAutenticacaoService>(container, Array.Empty<ResolverOverride>()).Instancia.Sessao)) });
			UnityContainerExtensions.RegisterType<INavegacaoBloqueioService, AutenticacaoNavegacaoService>(_container, typeof(AutenticacaoNavegacaoService).Name, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoAlterarSenhaService, AutenticacaoNavegacaoService>(_container, typeof(AutenticacaoNavegacaoService).Name, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<INavegacaoPreAutenticacaoService, AutenticacaoNavegacaoService>(_container, $"p{typeof(AutenticacaoNavegacaoService).Name}", Array.Empty<InjectionMember>());
			UnityContainerExtensions.Resolve<IAutenticacaoService>(_container, Array.Empty<ResolverOverride>()).Inicializar();
			DadosSistema.SetarAutenticacaoService(UnityContainerExtensions.Resolve<IAutenticacaoService>(_container, Array.Empty<ResolverOverride>()));
		}
	}
}
namespace Otus.Modulo.Autenticacoes.Views
{
	public class AlterarIpServidor : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AlterarIpServidor()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/alteraripservidor.xaml", UriKind.Relative);
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
	public class AutenticacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AutenticacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/autenticacaoview.xaml", UriKind.Relative);
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
			_contentLoaded = true;
		}
	}
	public class AutenticacoesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AutenticacoesView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/autenticacoesview.xaml", UriKind.Relative);
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
	public class AlterarSenhaView : UserControl, IComponentConnector
	{
		internal Grid grdPrincipal;

		private bool _contentLoaded;

		public AlterarSenhaView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/alterarsenhaview.xaml", UriKind.Relative);
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
			if (connectionId == 1)
			{
				grdPrincipal = (Grid)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class BloqueioView : UserControl, IComponentConnector
	{
		internal Grid grdPrincipal;

		private bool _contentLoaded;

		public BloqueioView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/bloqueioview.xaml", UriKind.Relative);
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
			if (connectionId == 1)
			{
				grdPrincipal = (Grid)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class Emitente : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public Emitente()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/emitente.xaml", UriKind.Relative);
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
			_contentLoaded = true;
		}
	}
	public class LogomarcaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LogomarcaView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/logomarcaview.xaml", UriKind.Relative);
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
	public class PreAutenticacaoView : UserControl, IComponentConnector
	{
		internal Grid grdPrincipal;

		private bool _contentLoaded;

		public PreAutenticacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/preautenticacaoview.xaml", UriKind.Relative);
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
			if (connectionId == 1)
			{
				grdPrincipal = (Grid)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class PrimeiroAcessoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PrimeiroAcessoView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/primeiroacessoview.xaml", UriKind.Relative);
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
			_contentLoaded = true;
		}
	}
	public class TituloView : UserControl, IComponentConnector
	{
		internal TextBlock txbSaudacao;

		private bool _contentLoaded;

		public TituloView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/tituloview.xaml", UriKind.Relative);
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
				txbSaudacao = (TextBlock)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class UsuarioSenhaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public UsuarioSenhaView()
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
				Uri resourceLocator = new Uri("/Modulo.Autenticacoes;component/views/usuariosenhaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Autenticacoes.ViewModels
{
	public abstract class AcessoViewModel : NavigationBase
	{
		public string NomeUsuario
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeUsuario));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeUsuario), value);
			}
		}

		public string Senha
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Senha));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Senha), value);
			}
		}

		public IIcone Cadeado => (IIcone)new Cadeado();

		protected AcessoViewModel()
			: base("")
		{
		}
	}
	public class AlterarSenhaViewModel : AutenticacaoBaseViewModel
	{
		private readonly IAutenticacaoService _autenticacaoService;

		public string NovaSenha
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NovaSenha));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NovaSenha), value);
			}
		}

		public string SenhaConfirmada
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada), value);
			}
		}

		public AlterarSenhaViewModel(IAutenticacaoService autenticacaoService, IEventPublisher eventPublisher, IImpressaoDigitalService impressaoDigitalService)
			: base(eventPublisher, impressaoDigitalService)
		{
			_autenticacaoService = autenticacaoService;
			base.NomeUsuario = autenticacaoService.Instancia.Sessao.NomeUsuario;
		}

		public override void AutenticarImpressaoDigital()
		{
			Autenticar("AlterarSenhaView");
		}

		public override void Autenticar(string view)
		{
			try
			{
				UsuarioDto usuarioLogado = DadosSistema.GetService().Canal<IUsuarioModuleService, UsuarioDto>((Func<IUsuarioModuleService, UsuarioDto>)((IUsuarioModuleService service) => service.ObterUsuarioLogado()));
				if (base.Senha != null && base.Senha != usuarioLogado.Senha)
				{
					throw new Exception("Senha atual inválida.");
				}
				if (NovaSenha != null && SenhaConfirmada != NovaSenha)
				{
					throw new Exception("A senha não é igual a confirmação.");
				}
				usuarioLogado.Senha = NovaSenha;
				DadosSistema.GetService().Canal<IUsuarioModuleService>((Action<IUsuarioModuleService>)delegate(IUsuarioModuleService service)
				{
					service.AtualizarSenha(usuarioLogado);
				});
				_autenticacaoService.Instancia.ExecutarLogOut();
				DialogoFactory.MostrarDialogoConfirma("O sistema será fechado. Um novo login deve ser realizado para assumir as alterações", false, HorizontalAlignment.Center);
				Process.GetCurrentProcess().Kill();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		public override void FecharView()
		{
			try
			{
				if (HelperString.IsNullOrEmpty(base.Senha))
				{
					DialogoFactory.MostrarDialogoConfirma("Informe a senha atual para retornar", false, HorizontalAlignment.Center);
				}
				else
				{
					_autenticacaoService.Instancia.Desbloquear(base.Senha);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}
	}
	public abstract class AutenticacaoBaseViewModel : AcessoViewModel
	{
		private IDisposable _subscription;

		private readonly IImpressaoDigitalService _impressaoDigitalService;

		public bool FocoNaSenha
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaSenha));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaSenha), value);
			}
		}

		public DelegateCommand<string> AutenticarCommand { get; set; }

		public ICommand SetarFocoNaSenhaCommand { get; set; }

		public bool UtilizaLeitorBiometrico { get; protected set; }

		protected AutenticacaoBaseViewModel(IEventPublisher eventPublisher, IImpressaoDigitalService impressaoDigitalService)
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			AutenticarCommand = new DelegateCommand<string>((Action<string>)Autenticar, (Func<string, bool>)PodeAutenticar, (bool?)null);
			SetarFocoNaSenhaCommand = (ICommand)new DelegateCommand((Action)SetarFoco);
			_subscription = ObservableExtensions.Subscribe<MongoDbConfiguradoEvent>(eventPublisher.GetEvent<MongoDbConfiguradoEvent>(), (Action<MongoDbConfiguradoEvent>)delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					((CommandBase<string>)(object)AutenticarCommand).RaiseCanExecuteChanged();
				});
			});
			_impressaoDigitalService = impressaoDigitalService;
			UtilizaLeitorBiometrico = _impressaoDigitalService.UtilizaLeitorBiometrico;
			ObterImpressaoDigital();
		}

		private void SetarFoco()
		{
			FocoNaSenha = !FocoNaSenha;
		}

		private void ObterImpressaoDigital()
		{
			_impressaoDigitalService.ObterImpressaoDigital((Action<UsuarioBase>)delegate(UsuarioBase usuario)
			{
				if (usuario != null)
				{
					if (string.IsNullOrWhiteSpace(base.NomeUsuario))
					{
						base.NomeUsuario = usuario.Nome;
					}
					base.Senha = usuario.Senha;
					ThreadPrincipal.Executar((Action)AutenticarImpressaoDigital);
				}
			}, (Action<Exception>)delegate(Exception exception)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					if (string.IsNullOrEmpty(DadosSistema.NomeUsuario) || DadosSistema.Bloqueado)
					{
						if (_impressaoDigitalService.IsOcupado())
						{
							HelperException.ShowLastException(exception, "", false, false);
						}
						else if (HelperException.ShowLastException(exception, "Tentar novamente?", true, false))
						{
							ObterImpressaoDigital();
						}
					}
				});
			});
		}

		public virtual void Autenticar(string view)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			//IL_0051: Expected O, but got Unknown
			Action<NavigationResult> obj = delegate
			{
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("Token", (object)new Token(string.Empty, base.NomeUsuario, base.Senha, string.Empty));
			((NavigationBase)this).Navegar(view, obj, val);
			base.Senha = string.Empty;
		}

		protected virtual bool PodeAutenticar(string arg)
		{
			if (!string.IsNullOrEmpty(base.NomeUsuario) && !string.IsNullOrEmpty(base.Senha))
			{
				return DadosSistema.BancoDadosConfigurado;
			}
			return false;
		}

		public override void Dispose()
		{
			_subscription?.Dispose();
			_subscription = null;
			((NavigationBase)this).Dispose();
		}

		public abstract void AutenticarImpressaoDigital();
	}
	public class AutenticacaoViewModel : AcessoViewModel
	{
		private readonly IAutenticacaoService _autenticacaoService;

		private readonly IConfiguracaoClientService _configuracaoClientService;

		public ISessao Sessao
		{
			get
			{
				return ((BindableBase)this).GetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao));
			}
			set
			{
				((BindableBase)this).SetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao), value, (Action)ProcessarEmitentes);
			}
		}

		public ICommand AutenticarCommand { get; set; }

		public Visibility MostrarView
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarView));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarView), value);
			}
		}

		public AutenticacaoViewModel(ISessao sessao, IAutenticacaoService autenticacaoService, IConfiguracaoClientService configuracaoClientService)
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			_autenticacaoService = autenticacaoService;
			_configuracaoClientService = configuracaoClientService;
			AutenticarCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Autenticar(autenticacaoPelaListaDeEmpresas: true);
			}, (Func<bool>)PodeAutenticar, (bool?)true);
			MostrarView = Visibility.Collapsed;
			Sessao = sessao;
		}

		private void Autenticar(bool autenticacaoPelaListaDeEmpresas)
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			try
			{
				ConfiguracaoClient val = _configuracaoClientService.ObterConfiguracao();
				val.SetarIdEmpresaLocal(Sessao.Perfis.ItemSelecionado.Empresa.Id);
				((IConfiguracaoService)_configuracaoClientService).Salvar<ConfiguracaoClient>(val);
				_autenticacaoService.Instancia.Autenticar(Sessao.Perfis.ItemSelecionado.Empresa);
				if (autenticacaoPelaListaDeEmpresas)
				{
					DadosSistema.GetEventPublisher().Publish<TrocouEmpresaEvent>(new TrocouEmpresaEvent());
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private bool PodeAutenticar()
		{
			if (!HelperObject.IsNull((object)Sessao) && !HelperObject.IsNull((object)Sessao.Perfis))
			{
				return ((ViewModelValidationBase)Sessao.Perfis).Valido;
			}
			return false;
		}

		private void ProcessarEmitentes()
		{
			if (PodeAutenticar())
			{
				if (Sessao.Perfis.Numero == 1)
				{
					Autenticar(autenticacaoPelaListaDeEmpresas: false);
				}
				else
				{
					MostrarView = Visibility.Visible;
				}
			}
		}
	}
	public class AutenticacoesViewModel : ModuloBase
	{
		public AutenticacoesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAutenticacaoRegion, "", (IResolucao)new ResolucaoBase())
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			((ModuloBase)this).KeepAlive = false;
		}
	}
	public class BloqueioViewModel : AutenticacaoBaseViewModel
	{
		private readonly IAutenticacaoService _autenticacaoService;

		public BloqueioViewModel(IAutenticacaoService autenticacaoService, IEventPublisher eventPublisher, IImpressaoDigitalService impressaoDigitalService)
			: base(eventPublisher, impressaoDigitalService)
		{
			_autenticacaoService = autenticacaoService;
			base.NomeUsuario = autenticacaoService.Instancia.Sessao.NomeUsuario;
		}

		public override void AutenticarImpressaoDigital()
		{
			Autenticar("BloqueioView");
		}

		public override void Autenticar(string view)
		{
			try
			{
				_autenticacaoService.Instancia.Desbloquear(base.Senha);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}
	}
	public class PreAutenticacaoViewModel : AutenticacaoBaseViewModel
	{
		private readonly IUnityContainer _container;

		public ICommand AlterarIpServidorCommand { get; set; }

		public PreAutenticacaoViewModel(IUnityContainer container, IEventPublisher eventPublisher, IImpressaoDigitalService impressaoDigitalService)
			: base(eventPublisher, impressaoDigitalService)
		{
			_container = container;
			AlterarIpServidorCommand = (ICommand)new DelegateCommand<string>((Action<string>)AlterarIpServidor, (Func<string, bool>)PodeAlterarIpServidor, (bool?)null);
		}

		private bool PodeAlterarIpServidor(string arg)
		{
			return !Environment.GetCommandLineArgs().Any((string x) => x.Equals("guacamole"));
		}

		private void AlterarIpServidor(string obj)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Atenção! Essa alteração poderá acarretar no mau funcionamento do sistema caso o endereço informado seja incorreto.\nProsseguir com a alteração do endereço do servidor?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				UnityContainerExtensions.Resolve<IAutenticacaoService>(_container, Array.Empty<ResolverOverride>()).Configurar();
			}
		}

		public override void AutenticarImpressaoDigital()
		{
			Autenticar("PrimeiroAcessoView");
		}
	}
	public class PrimeiroAcessoViewModel : NavigationBase
	{
		private readonly IAutenticacaoService _autenticacaoService;

		private readonly IConfiguracaoClientService _configuracaoClientService;

		private readonly IEventPublisher _eventPublisher;

		private readonly IWService _service;

		private IDisposable _sessaoAtualizadaEvent;

		private readonly ConfiguracaoClient _configuracao;

		public ISessao Sessao
		{
			get
			{
				return ((BindableBase)this).GetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao));
			}
			set
			{
				((BindableBase)this).SetProperty<ISessao>((Expression<Func<ISessao>>)(() => Sessao), value, (Action)ProcessarEmitentes);
			}
		}

		public ICommand AutenticarCommand { get; set; }

		public Visibility MostrarView
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarView));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => MostrarView), value);
			}
		}

		public PrimeiroAcessoViewModel(IAutenticacaoService autenticacaoService, IConfiguracaoClientService configuracaoClientService, IEventPublisher eventPublisher, IWService service)
			: base("")
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			_autenticacaoService = autenticacaoService;
			_configuracaoClientService = configuracaoClientService;
			_eventPublisher = eventPublisher;
			_service = service;
			_configuracao = configuracaoClientService.ObterConfiguracao();
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			AutenticarCommand = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)PodeAutenticar, (bool?)true);
			MostrarView = Visibility.Collapsed;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			AssinarEventos();
			object obj = navigationParameters["Token"];
			PreAutenticar((Token)((obj is Token) ? obj : null));
		}

		private void PreAutenticar(Token token)
		{
			try
			{
				_autenticacaoService.Instancia.PreAutenticar(token);
			}
			catch (AtualizacaoIniciadaException)
			{
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
				((NavigationBase)this).FecharView();
			}
		}

		private void AssinarEventos()
		{
			if (HelperObject.IsNull((object)_sessaoAtualizadaEvent))
			{
				_sessaoAtualizadaEvent = ObservableExtensions.Subscribe<SessaoAtualizadaPrimeiroAcessoEvent>(_eventPublisher.GetEvent<SessaoAtualizadaPrimeiroAcessoEvent>(), (Action<SessaoAtualizadaPrimeiroAcessoEvent>)delegate(SessaoAtualizadaPrimeiroAcessoEvent @event)
				{
					Sessao = @event.Sessao;
				});
			}
		}

		private void ProcessarEmitentes()
		{
			if ((!PodeAutenticar() || !_service.OnlineMsg(false)) && _configuracao.Ecf == null)
			{
				_autenticacaoService.Instancia.LimparSessao();
				((NavigationBase)this).FecharView();
			}
			else if (Sessao.Perfis.Numero == 1)
			{
				Gravar();
			}
			else if (string.IsNullOrWhiteSpace(_configuracao.IdEmpresaLocal))
			{
				MostrarView = Visibility.Visible;
			}
			else
			{
				Gravar();
			}
		}

		private bool PodeAutenticar()
		{
			if (!HelperObject.IsNull((object)Sessao) && !HelperObject.IsNull((object)Sessao.Perfis))
			{
				return ((ViewModelValidationBase)Sessao.Perfis).Valido;
			}
			return false;
		}

		private void Gravar()
		{
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			try
			{
				if (string.IsNullOrWhiteSpace(_configuracao.IdEmpresaLocal))
				{
					_configuracao.SetarIdEmpresaLocal(Sessao.Perfis.ItemSelecionado.Empresa.Id);
					((IConfiguracaoService)_configuracaoClientService).Salvar<ConfiguracaoClient>(_configuracao);
					if (Sessao.Perfis.Numero > 1)
					{
						Autenticar();
						return;
					}
				}
				Sessao.Perfis.PosicionarNoPrimeiro();
				((NavigationBase)this).Navegar("AutenticacaoView", (Action<NavigationResult>)delegate
				{
				}, new NavigationParameters());
				((NavigationBase)this).Dispose();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void Autenticar()
		{
			try
			{
				_autenticacaoService.Instancia.Autenticar(Sessao.Perfis.ItemSelecionado.Empresa);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
			finally
			{
				((NavigationBase)this).FecharView();
			}
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= OnParametroInformado;
			_sessaoAtualizadaEvent?.Dispose();
			((NavigationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.Autenticacoes.Services
{
	public class AutenticacaoNavegacaoService : INavegacaoPreAutenticacaoService, INavegacaoBloqueioService, INavegacaoAlterarSenhaService
	{
		private readonly IRegionManagerService _regionManagerService;

		public AutenticacaoNavegacaoService(IRegionManagerService regionManagerService)
		{
			_regionManagerService = regionManagerService;
		}

		public void PreAutenticar()
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			IRegionManagerService regionManagerService = _regionManagerService;
			string trabalhoRegion = Regioes.TrabalhoRegion;
			string name = typeof(AutenticacoesView).Name;
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("Cor", (object)new Cor("#1F4C60"));
			val.Add("Icone", (object)new Usuario());
			val.Add("Descricao", (object)"Bem-vindo");
			val.Add("ViewLista", (object)typeof(PreAutenticacaoView).Name);
			regionManagerService.AbrirVisao(trabalhoRegion, name, true, val);
		}

		public void TrocarEmpresa()
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			IRegionManagerService regionManagerService = _regionManagerService;
			string trabalhoRegion = Regioes.TrabalhoRegion;
			string name = typeof(AutenticacoesView).Name;
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("Cor", (object)new Cor("#1F4C60"));
			val.Add("Icone", (object)new Usuario());
			val.Add("Descricao", (object)"Bem-vindo");
			val.Add("ViewLista", (object)typeof(PrimeiroAcessoView).Name);
			regionManagerService.AbrirVisao(trabalhoRegion, name, true, val);
		}

		public void Bloquear()
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			IRegionManagerService regionManagerService = _regionManagerService;
			string trabalhoRegion = Regioes.TrabalhoRegion;
			string name = typeof(AutenticacoesView).Name;
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("Cor", (object)new Cor("#1F4C60"));
			val.Add("Icone", (object)new Cadeado());
			val.Add("ViewLista", (object)typeof(BloqueioView).Name);
			regionManagerService.AbrirVisao(trabalhoRegion, name, false, val);
		}

		public void AlterarSenha()
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			IRegionManagerService regionManagerService = _regionManagerService;
			string trabalhoRegion = Regioes.TrabalhoRegion;
			string name = typeof(AutenticacoesView).Name;
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("Cor", (object)new Cor("#1F4C60"));
			val.Add("Icone", (object)new Cadeado());
			val.Add("ViewLista", (object)typeof(AlterarSenhaView).Name);
			regionManagerService.AbrirVisao(trabalhoRegion, name, false, val);
		}

		public void Desbloquear()
		{
		}
	}
	public class AutenticacaoService : IAutenticacaoService
	{
		private readonly IUnityContainer _container;

		public ISessaoService Instancia => UnityContainerExtensions.Resolve<ISessaoService>(_container, Array.Empty<ResolverOverride>());

		public AutenticacaoService(IUnityContainer container)
		{
			_container = container;
		}

		public void Inicializar()
		{
			IniciarSincronizador();
			ConfiguracaoClient val = UnityContainerExtensions.Resolve<IConfiguracaoClientService>(_container, Array.Empty<ResolverOverride>()).ObterConfiguracao();
			if (!HelperObject.IsNull((object)val) && val.IpValido)
			{
				VerificarAtualizacaoEInicializar(val.Ip, val.Porta);
			}
			else
			{
				Configurar();
			}
		}

		private void VerificarAtualizacaoEInicializar(string ip, int porta)
		{
			string exe;
			try
			{
				exe = Process.GetCurrentProcess().MainModule.FileName;
			}
			catch (Exception)
			{
				ISessaoService instancia = Instancia;
				if (instancia != null)
				{
					instancia.Inicializar();
				}
				return;
			}
			MostrarLoader("Verificando atualizações...");
			Dispatcher dispatcher = Application.Current.Dispatcher;
			Task.Run(() => new LancadorAtualizador().VerificarEIniciar(ip, porta, exe, (Action<string>)delegate(string mensagem)
			{
				dispatcher.Invoke(delegate
				{
					MostrarLoader(mensagem);
				});
			}, (Action)delegate
			{
				dispatcher.Invoke(EsconderLoader);
			})).ContinueWith(delegate(Task<bool> tarefa)
			{
				if (!tarefa.IsFaulted && tarefa.Result)
				{
					Application.Current.Shutdown();
				}
				else
				{
					EsconderLoader();
					ISessaoService instancia2 = Instancia;
					if (instancia2 != null)
					{
						instancia2.Inicializar();
					}
				}
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private static void MostrarLoader(string mensagem)
		{
			try
			{
				LoaderProcessoService.MostrarLoader(mensagem);
			}
			catch (Exception)
			{
			}
		}

		private static void EsconderLoader()
		{
			try
			{
				LoaderProcessoService.EsconderLoader();
			}
			catch (Exception)
			{
			}
		}

		public void Configurar()
		{
			foreach (INavegacaoConfiguracaoService item in UnityContainerExtensions.ResolveAll<INavegacaoConfiguracaoService>(_container, Array.Empty<ResolverOverride>()))
			{
				item.Configurar();
			}
		}

		private void IniciarSincronizador()
		{
			try
			{
				using ServiceController serviceController = new ServiceController();
				serviceController.MachineName = ".";
				serviceController.ServiceName = "SincronizadorDigisat";
				if (serviceController.Status != ServiceControllerStatus.Running)
				{
					serviceController.Start();
					serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30.0));
				}
			}
			catch (Exception)
			{
				DialogoFactory.MostrarDialogoConfirma("Erro ao inicializar serviço SincronizadorDigisat. Verifique!", false, HorizontalAlignment.Center);
			}
		}
	}
	public class SessaoService : ISessaoService
	{
		private readonly IUnityContainer _container;

		private readonly IWService _service;

		private readonly IUsuarioRepositoryRead _usuarioRepositoryRead;

		private readonly IConfiguracaoRepositoryRead _configuracaoRepositoryRead;

		private readonly IAcessoRapidoService _acessoRapidoService;

		private readonly IImpressaoDigitalService _impressaoDigitalService;

		protected readonly IEventPublisher EventPublisher;

		private readonly IXmlFile _xmlTokenFile;

		private const int MinutosAguardarAprovacaoWhatsApp = 5;

		private const int TimeoutAprovacaoWhatsApp = 45;

		private const int IntervaloConsultaAprovacaoWhatsApp = 2000;

		private Token _tokenTrocaEmpresa;

		public ISessao Sessao { get; private set; }

		public bool PodeBloquear => ((IEnumerable<object>)UnityContainerExtensions.Resolve<IRegionManager>(_container, Array.Empty<ResolverOverride>()).Regions[Regioes.TrabalhoRegion].ActiveViews).All((object x) => x.GetType().Name != typeof(AutenticacoesView).Name);

		public bool PodeAlterarSenha
		{
			get
			{
				if (((IEnumerable<object>)UnityContainerExtensions.Resolve<IRegionManager>(_container, Array.Empty<ResolverOverride>()).Regions[Regioes.TrabalhoRegion].ActiveViews).All((object x) => x.GetType().Name != typeof(AutenticacoesView).Name))
				{
					ISessao sessao = Sessao;
					if (sessao == null || !sessao.UsuarioRevenda)
					{
						ISessao sessao2 = Sessao;
						if (sessao2 == null || !sessao2.UsuarioSuporte)
						{
							ISessao sessao3 = Sessao;
							return sessao3 == null || !sessao3.UsuarioPedirMenu;
						}
					}
				}
				return false;
			}
		}

		public bool PodeAutenticar => ((IEnumerable<object>)UnityContainerExtensions.Resolve<IRegionManager>(_container, Array.Empty<ResolverOverride>()).Regions[Regioes.TrabalhoRegion].ActiveViews).All((object x) => x.GetType().Name != typeof(AutenticacoesView).Name);

		public SessaoService(IUnityContainer container, IWService service, IUsuarioRepositoryRead usuarioRepositoryRead, IConfiguracaoRepositoryRead configuracaoRepositoryRead, IAcessoRapidoService acessoRapidoService, IImpressaoDigitalService impressaoDigitalService)
		{
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			SessaoService sessaoService = this;
			_container = container;
			_service = service;
			_usuarioRepositoryRead = usuarioRepositoryRead;
			_configuracaoRepositoryRead = configuracaoRepositoryRead;
			_acessoRapidoService = acessoRapidoService;
			_impressaoDigitalService = impressaoDigitalService;
			EventPublisher = UnityContainerExtensions.Resolve<IEventPublisher>(container, Array.Empty<ResolverOverride>());
			ObservableExtensions.Subscribe<Configuracao>(EventPublisher.GetEvent<Configuracao>(), (Action<Configuracao>)delegate(Configuracao x)
			{
				ISessao sessao = Sessao;
				if (sessao != null)
				{
					sessao.AtualizarConfiguracao(x);
				}
			});
			ObservableExtensions.Subscribe<ConfiguracaoDto>(EventPublisher.GetEvent<ConfiguracaoDto>(), (Action<ConfiguracaoDto>)delegate
			{
				if (!HelperObject.IsNull((object)sessaoService.Sessao))
				{
					Configuracao val = configuracaoRepositoryRead.Get(sessaoService.Sessao.Perfis.ItemSelecionado.Empresa.Id);
					if (!HelperObject.IsNull((object)val))
					{
						sessaoService.Sessao.AtualizarConfiguracao(ConfiguracaoProjection.Morfar(val));
					}
				}
			});
			XmlFileFactory.SetCurrent((IXmlFileFactory)new XmlTokenFileFactory());
			_xmlTokenFile = XmlFileFactory.Create();
		}

		public bool Inicializar()
		{
			try
			{
				Token val = _xmlTokenFile.Read<Token>();
				if (val != null && val.TimeOut)
				{
					val = null;
				}
				PreAutenticar(val);
				Autenticar(Sessao.Perfis.ItemSelecionado.Empresa);
				return true;
			}
			catch (AtualizacaoIniciadaException)
			{
				return false;
			}
			catch (Exception)
			{
				NavegarPreAutenticacao();
				return false;
			}
		}

		public void PreAutenticar(Token token)
		{
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			if (HelperObject.IsNull((object)token) && HelperObject.IsNotNull((object)_tokenTrocaEmpresa))
			{
				token = _tokenTrocaEmpresa;
				_tokenTrocaEmpresa = null;
			}
			PreValidarNomeUsuarioESenha((token != null) ? token.NomeUsuario : null, (token != null) ? token.Senha : null);
			string releaseServidor = InicializarServidor(token);
			CompararReleaseSistema(releaseServidor);
			ISessao val = CriarSessao(token);
			if (val == null || !val.Valida)
			{
				throw new Exception("Credenciais inválidas");
			}
			SetarSessao(val);
			EventPublisher.Publish<SessaoAtualizadaPrimeiroAcessoEvent>(new SessaoAtualizadaPrimeiroAcessoEvent(Sessao));
		}

		private void PreValidarNomeUsuarioESenha(string nomeUsuario, string senha)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			if (HelperObject.IsNull((object)Sessao) || (Sessao.NomeUsuarioValido(nomeUsuario) && Sessao.SenhaValida(senha)))
			{
				return;
			}
			throw new CredenciaisInvalidasException("Os dados informados não correspondem a sessão anterior.");
		}

		private ISessao CriarSessao(Token token)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			UsuarioBase val = ((IRepositoryRead<UsuarioBase>)(object)_usuarioRepositoryRead).Find(EntitySpecification<UsuarioBase>.Ativo()).ToList().FirstOrDefault((UsuarioBase x) => x.Acessos.Any((string y) => string.Equals(y.ToLowerInvariant(), token.NomeUsuario.ToLowerInvariant(), StringComparison.Ordinal)));
			if (HelperObject.IsNull((object)val))
			{
				throw new UsuarioNaoEncontradoException();
			}
			if (HelperObject.IsNot<UsuarioSuporte>((object)val) && !val.TestarSenha(token.Senha))
			{
				throw new CredenciaisInvalidasException("Credenciais inválidas!");
			}
			return (ISessao)new Sessao(HelperObject.ToStringOrEmpty((object)((EntityBase)val).Id), val.UsuarioNaoPrecisaAbrirTurno, val.Nome, token.NomeUsuario, token.Senha, val.PerfisAtivos.MorfarCollection(), val.ModulosPinados, val.PinAtualizado, token.IdEmpresa, val.LimiteDesconto, val is UsuarioSuporte, val is UsuarioRevenda, val is UsuarioPedirMenu);
		}

		private string InicializarServidor(Token token)
		{
			try
			{
				return _service.Criar().SetarNomeUsuario(token.NomeUsuario).SetarSenha(token.Senha)
					.SetarIdEmpresa(token.IdEmpresa)
					.SetarToken(token.Id)
					.Reiniciar();
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		private void CompararReleaseSistema(string releaseServidor)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrWhiteSpace(releaseServidor))
			{
				return;
			}
			string text = DadosSistema.ObterReleaseSistema();
			if (text.Equals(releaseServidor))
			{
				return;
			}
			if (TentarAtualizarEstacao(text, releaseServidor))
			{
				throw new AtualizacaoIniciadaException();
			}
			throw new Exception($"Release do sistema '{text}' difere da release do servidor '{releaseServidor}'. Verifique!");
		}

		private bool TentarAtualizarEstacao(string releaseSistema, string releaseServidor)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (!ReleaseLocal.ServidorMaisNovo(releaseSistema, releaseServidor))
				{
					return false;
				}
				ConfiguracaoClient val = UnityContainerExtensions.Resolve<IConfiguracaoClientService>(_container, Array.Empty<ResolverOverride>()).ObterConfiguracao();
				if (HelperObject.IsNull((object)val) || !val.IpValido)
				{
					return false;
				}
				string fileName = Process.GetCurrentProcess().MainModule.FileName;
				if (!new LancadorAtualizador().VerificarEIniciar(val.Ip, val.Porta, fileName, (Action<string>)null, (Action)null))
				{
					return false;
				}
				Application.Current.Dispatcher.Invoke(delegate
				{
					Application.Current.Shutdown();
				});
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void SetarSessao(ISessao sessao)
		{
			if (!HelperObject.IsNull((object)Sessao))
			{
				((IDisposable)Sessao).Dispose();
			}
			Sessao = sessao;
		}

		private void NavegarPreAutenticacao()
		{
			foreach (INavegacaoPreAutenticacaoService item in UnityContainerExtensions.ResolveAll<INavegacaoPreAutenticacaoService>(_container, Array.Empty<ResolverOverride>()))
			{
				item.PreAutenticar();
			}
		}

		public void Autenticar(Pessoa empresa)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			try
			{
				ProcessarAutenticacao(empresa);
				Sessao.SetarEmpresa(empresa);
				EventPublisher.Publish<SessaoAtualizadaEvent>(new SessaoAtualizadaEvent(Sessao));
				UnityContainerExtensions.Resolve<INavegacaoInicialService>(_container, "MenuNavegacaoService", Array.Empty<ResolverOverride>()).Navegar();
				UnityContainerExtensions.Resolve<INavegacaoInicialService>(_container, "InicialNavegacaoService", Array.Empty<ResolverOverride>()).Navegar();
				_acessoRapidoService.SistemaBloqueado = false;
			}
			catch (Exception)
			{
				LimparSessao();
				throw;
			}
		}

		private void ProcessarAutenticacao(Pessoa empresa)
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			_service.SetarIdEmpresa(empresa.Id);
			Configuracao val = _configuracaoRepositoryRead.Get(empresa.Id);
			Sessao.AtualizarConfiguracao(ConfiguracaoProjection.Morfar(val));
			EventPublisher.Publish<AutenticouEvent>(new AutenticouEvent());
		}

		public virtual async Task Bloquear()
		{
			await ExecutarLogOff();
			foreach (INavegacaoBloqueioService item in UnityContainerExtensions.ResolveAll<INavegacaoBloqueioService>(_container, Array.Empty<ResolverOverride>()))
			{
				item.Bloquear();
			}
			DadosSistema.SetarBloqueado(true);
		}

		public virtual async Task AlterarSenha()
		{
			await ExecutarLogOff();
			foreach (INavegacaoAlterarSenhaService item in UnityContainerExtensions.ResolveAll<INavegacaoAlterarSenhaService>(_container, Array.Empty<ResolverOverride>()))
			{
				item.AlterarSenha();
			}
			DadosSistema.SetarBloqueado(true);
		}

		public async Task ExecutarLogOff()
		{
			_xmlTokenFile.Delete();
			EventPublisher.Publish<OcultarBuscaRapida>(new OcultarBuscaRapida());
			try
			{
				await _service.Canal<IAutenticacaoModuleService, Task>((Func<IAutenticacaoModuleService, Task>)((IAutenticacaoModuleService service) => service.ExecutarLogOffAsync()));
			}
			catch (Exception)
			{
			}
			finally
			{
				EventPublisher.Publish<ExecutouLogOffEvent>(new ExecutouLogOffEvent());
				_service.SetarToken(string.Empty);
			}
		}

		public async Task ExecutarLogOut()
		{
			await ExecutarLogOff();
			LimparSessao();
			EventPublisher.Publish<SessaoAtualizadaEvent>(new SessaoAtualizadaEvent(Sessao));
			NavegarPreAutenticacao();
		}

		public async Task TrocarEmpresa()
		{
			try
			{
				SessaoService sessaoService = this;
				ISessao sessao = Sessao;
				sessaoService._tokenTrocaEmpresa = ((sessao != null) ? sessao.GerarToken(string.Empty) : null);
				await ExecutarLogOff();
				LimparSessao();
				EventPublisher.Publish<SessaoAtualizadaEvent>(new SessaoAtualizadaEvent(Sessao));
				foreach (INavegacaoPreAutenticacaoService item in UnityContainerExtensions.ResolveAll<INavegacaoPreAutenticacaoService>(_container, Array.Empty<ResolverOverride>()))
				{
					item.TrocarEmpresa();
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				await ExecutarLogOut();
			}
		}

		public void LimparSessao()
		{
			_service.SetarNomeUsuario(string.Empty).SetarSenha(string.Empty).SetarToken(string.Empty)
				.SetarIdEmpresa(string.Empty);
			SetarSessao(null);
		}

		public void Desbloquear(string senha)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			PreValidarNomeUsuarioESenha(Sessao.Acesso, senha);
			ProcessarAutenticacao(Sessao.Perfis.ItemSelecionado.Empresa);
			EventPublisher.Publish<SessaoAtualizadaEvent>(new SessaoAtualizadaEvent(Sessao));
			foreach (INavegacaoBloqueioService item in UnityContainerExtensions.ResolveAll<INavegacaoBloqueioService>(_container, Array.Empty<ResolverOverride>()))
			{
				item.Desbloquear();
			}
			_acessoRapidoService.SistemaBloqueado = false;
			DadosSistema.SetarBloqueado(false);
		}

		public bool DeveAutorizar(IDireito direito, Modulo modulo)
		{
			return Sessao.TestarDireito(direito, modulo);
		}

		public AutorizacaoDto VerificarPermissao<TModulo, TDireito>(decimal descontoPercentual = 0m, string detalheSolicitacao = null) where TModulo : Modulo, new() where TDireito : class, IDireito, new()
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			TDireito val = new TDireito();
			bool flag = ExigeAutorizacaoTerceiro();
			if (Sessao.VerificarPermissao<TModulo, TDireito>() && !flag)
			{
				return new AutorizacaoDto
				{
					Data = DateTime.Now,
					Direito = DireitoProjection.ProjectedAsDto((IDireito)(object)val),
					NomeUsuario = Sessao.NomeUsuario,
					LimiteDesconto = Sessao.LimiteDesconto
				};
			}
			AutorizacaoDto autorizacao = null;
			Func<IDialogo, Task<bool>> func = async delegate(IDialogo dialogo)
			{
				PermissaoAcessoDialogoDataContext permissaoAcessoContext2 = (PermissaoAcessoDialogoDataContext)(object)((dialogo is PermissaoAcessoDialogoDataContext) ? dialogo : null);
				_ = autorizacao;
				string usuario = permissaoAcessoContext2.Usuario;
				AutorizacaoDto val2;
				if (usuario != null && usuario.Length == 15 && HelperString.IsNullOrEmpty(permissaoAcessoContext2.Senha))
				{
					_ = autorizacao;
					val2 = (autorizacao = await AutorizarHash<TModulo, TDireito>(dialogo));
				}
				else
				{
					_ = autorizacao;
					val2 = (autorizacao = await Autorizar<TModulo, TDireito>(dialogo));
				}
				autorizacao = val2;
				if (autorizacao != null && !HelperObject.Is<AutorizacaoDtoNull>((object)autorizacao) && ExigeAutorizacaoTerceiro() && autorizacao.NomeUsuario == Sessao.NomeUsuario)
				{
					((DialogoDataContext)permissaoAcessoContext2).Aviso = "Usuário logado não pode se auto-autorizar. Solicite autorização de outro usuário.";
					autorizacao = null;
					return false;
				}
				return autorizacao != null && !HelperObject.Is<AutorizacaoDtoNull>((object)autorizacao);
			};
			string text = ((object)new TDireito()).ToString() + (_impressaoDigitalService.UtilizaLeitorBiometrico ? "\n\nEncoste o dedo no leitor biométrico." : string.Empty);
			Func<IDialogo, Task<bool>> func2 = async delegate(IDialogo dialogo)
			{
				PermissaoAcessoDialogoDataContext permissaoAcessoContext = (PermissaoAcessoDialogoDataContext)(object)((dialogo is PermissaoAcessoDialogoDataContext) ? dialogo : null);
				_ = autorizacao;
				autorizacao = await SolicitarAutorizacaoWhatsApp<TModulo, TDireito>(permissaoAcessoContext, descontoPercentual, detalheSolicitacao);
				if (autorizacao != null && ExigeAutorizacaoTerceiro() && autorizacao.NomeUsuario == Sessao.NomeUsuario)
				{
					((DialogoDataContext)permissaoAcessoContext).Aviso = "Usuário logado não pode se auto-autorizar. Solicite autorização de outro usuário.";
					autorizacao = null;
				}
				return autorizacao != null;
			};
			if (DialogoFactory.MostrarDialogoPermissaoAcesso(text, func, (Action<IDialogo>)ObterImpressaoDigital, DadosSistema.WhatsAppConfigurado ? func2 : null, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return autorizacao;
		}

		private async Task<AutorizacaoDto> SolicitarAutorizacaoWhatsApp<TModulo, TDireito>(PermissaoAcessoDialogoDataContext contexto, decimal descontoPercentual, string detalheSolicitacao) where TModulo : Modulo, new() where TDireito : class, IDireito, new()
		{
			TModulo val = new TModulo();
			TDireito direito = new TDireito();
			IApiWdConnectService servico = ApiWdConnectServiceFactory.Criar(DadosSistema.EnderecoServidorWhatsApp, DadosSistema.ObterProxy(), 45);
			((DialogoDataContext)contexto).Aviso = "Enviando o pedido pelo WhatsApp...";
			AprovacaoResponse pedido;
			try
			{
				pedido = await servico.CriarAprovacaoAsync(MontarPedidoAprovacao((Modulo)(object)val, (IDireito)(object)direito, descontoPercentual, detalheSolicitacao));
			}
			catch (TaskCanceledException)
			{
				((DialogoDataContext)contexto).Aviso = "O WD Connect demorou para responder ao envio. Solicite de novo para voltar a aguardar o mesmo pedido.";
				return null;
			}
			catch (HttpRequestException excecao)
			{
				((DialogoDataContext)contexto).Aviso = $"Não foi possível enviar o pedido ao WD Connect: {MensagemDeErro(excecao)}";
				return null;
			}
			if (pedido == null)
			{
				((DialogoDataContext)contexto).Aviso = "O WD Connect não respondeu ao pedido.";
				return null;
			}
			((DialogoDataContext)contexto).Aviso = $"Aguardando a resposta no WhatsApp. Pedido {pedido.Codigo}.";
			DateTime limite = DateTime.Now.AddMinutes(5.0);
			while (DateTime.Now < limite && !contexto.Encerrado)
			{
				AprovacaoConsultaResponse val2 = await ConsultarAprovacao(servico, pedido.Id);
				if (val2 != null && !val2.Pendente)
				{
					return TratarDecisaoWhatsApp(val2, (IDireito)(object)direito, contexto);
				}
				await Task.Delay(2000);
			}
			if (contexto.Encerrado)
			{
				await CancelarPedidoAprovacao(servico, pedido.Id);
			}
			else
			{
				((DialogoDataContext)contexto).Aviso = "Ninguém respondeu o pedido no WhatsApp.";
			}
			return null;
		}

		private AprovacaoRequest MontarPedidoAprovacao(Modulo modulo, IDireito direito, decimal descontoPercentual, string detalheSolicitacao)
		{
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0149: Expected O, but got Unknown
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Expected O, but got Unknown
			bool num = PermissaoWdConnect.EhDireitoDesconto(direito);
			bool flag = num && descontoPercentual > 0m;
			string text = (flag ? string.Empty : PermissaoWdConnect.ObterNome(modulo, direito));
			string text2 = $"digisat-{modulo.Codigo}-{direito.Codigo}-{Environment.MachineName}";
			string text3 = $"*{direito.Descricao}*";
			if (!HelperString.IsNullOrEmpty(detalheSolicitacao))
			{
				text3 += $"\n\n{detalheSolicitacao}";
			}
			else if (flag)
			{
				text3 += string.Format("\n\nDesconto pedido: {0}%.", descontoPercentual.ToString("N2"));
			}
			text3 += $"\n\n{modulo.DescricaoDetalhada} · {Sessao.NomeUsuario} · {Environment.MachineName}";
			if (flag)
			{
				text2 += $"-{(int)Math.Round(descontoPercentual * 100m)}";
			}
			AprovacaoRequest val = new AprovacaoRequest
			{
				Mensagem = text3,
				Referencia = text2.ToLowerInvariant(),
				Permissao = (HelperString.IsNullOrEmpty(text) ? null : text),
				DescontoPercentual = (flag ? new decimal?(descontoPercentual) : null),
				ExpiraMin = 5
			};
			if (num)
			{
				val.PedirValor = new AprovacaoPedirValor
				{
					Rotulo = "percentual aprovado",
					Minimo = default(decimal),
					Maximo = (flag ? descontoPercentual : 100m)
				};
			}
			return val;
		}

		private AutorizacaoDto TratarDecisaoWhatsApp(AprovacaoConsultaResponse consulta, IDireito direito, PermissaoAcessoDialogoDataContext contexto)
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			AprovacaoDecisor decididoPor = consulta.DecididoPor;
			string text = ((decididoPor == null) ? string.Empty : (HelperString.IsNullOrEmpty(decididoPor.UsuarioDigisat) ? decididoPor.Nome : decididoPor.UsuarioDigisat));
			if (consulta.Aprovada)
			{
				return new AutorizacaoDto
				{
					Direito = DireitoProjection.ProjectedAsDto(direito),
					NomeUsuario = (text ?? string.Empty),
					Data = DateTime.Now,
					LimiteDesconto = (consulta.Valor ?? 0m)
				};
			}
			if (consulta.Status == "negada")
			{
				((DialogoDataContext)contexto).Aviso = (HelperString.IsNullOrEmpty(consulta.Motivo) ? $"Pedido negado por {text}." : $"Pedido negado por {text}: {consulta.Motivo}");
			}
			else if (consulta.Status == "expirada")
			{
				((DialogoDataContext)contexto).Aviso = "O pedido expirou sem resposta no WhatsApp.";
			}
			else
			{
				((DialogoDataContext)contexto).Aviso = "O pedido foi cancelado.";
			}
			return null;
		}

		private static async Task<AprovacaoConsultaResponse> ConsultarAprovacao(IApiWdConnectService servico, int id)
		{
			try
			{
				return await servico.ConsultarAprovacaoAsync(id, 0);
			}
			catch (TaskCanceledException)
			{
				return null;
			}
			catch (HttpRequestException)
			{
				return null;
			}
		}

		private static string MensagemDeErro(Exception excecao)
		{
			Exception ex = excecao;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			return ex.Message;
		}

		private static async Task CancelarPedidoAprovacao(IApiWdConnectService servico, int id)
		{
			try
			{
				await servico.CancelarAprovacaoAsync(id);
			}
			catch (Exception)
			{
			}
		}

		private bool ExigeAutorizacaoTerceiro()
		{
			Lista<PerfilEmpresaSessao> perfis = Sessao.Perfis;
			bool? obj;
			if (perfis == null)
			{
				obj = null;
			}
			else
			{
				PerfilEmpresaSessao itemSelecionado = perfis.ItemSelecionado;
				if (itemSelecionado == null)
				{
					obj = null;
				}
				else
				{
					Configuracao configuracao = itemSelecionado.Configuracao;
					obj = ((configuracao != null) ? new bool?(configuracao.ExigirAutorizacaoTerceiro) : null);
				}
			}
			bool? flag = obj;
			if (flag.HasValue && flag.GetValueOrDefault() && !Sessao.UsuarioSuporte && !Sessao.UsuarioRevenda && !Sessao.UsuarioPedirMenu)
			{
				return !(Sessao.NomeUsuario?.Equals("Usuário Administrador") ?? false);
			}
			return false;
		}

		private async Task<AutorizacaoDto> Autorizar<TModulo, TDireito>(IDialogo dataContext) where TModulo : Modulo, new() where TDireito : class, IDireito, new()
		{
			PermissaoAcessoDialogoDataContext permissaoAcessoContext = (PermissaoAcessoDialogoDataContext)(object)((dataContext is PermissaoAcessoDialogoDataContext) ? dataContext : null);
			return await UnityContainerExtensions.Resolve<IWService>(_container, Array.Empty<ResolverOverride>()).Canal<IAutorizacaoModuleService, Task<AutorizacaoDto>>((Func<IAutorizacaoModuleService, Task<AutorizacaoDto>>)delegate(IAutorizacaoModuleService s)
			{
				PermissaoAcessoDialogoDataContext obj = permissaoAcessoContext;
				string obj2 = ((obj != null) ? obj.Usuario : null);
				PermissaoAcessoDialogoDataContext obj3 = permissaoAcessoContext;
				return s.AutorizarAsync(obj2, (obj3 != null) ? obj3.Senha : null, ((IDireito)new TDireito()).Codigo, ((Modulo)new TModulo()).Codigo);
			});
		}

		private async Task<AutorizacaoDto> AutorizarHash<TModulo, TDireito>(IDialogo dataContext) where TModulo : Modulo, new() where TDireito : class, IDireito, new()
		{
			PermissaoAcessoDialogoDataContext val = (PermissaoAcessoDialogoDataContext)(object)((dataContext is PermissaoAcessoDialogoDataContext) ? dataContext : null);
			string token = TripleDes.Cifrar(val.Usuario, Hash.RetornarChavePrivadaInformacaoDecifrada(), CipherMode.ECB, PaddingMode.PKCS7);
			return await UnityContainerExtensions.Resolve<IWService>(_container, Array.Empty<ResolverOverride>()).Canal<IAutorizacaoModuleService, Task<AutorizacaoDto>>((Func<IAutorizacaoModuleService, Task<AutorizacaoDto>>)((IAutorizacaoModuleService s) => s.AutorizarHashAsync(token, ((IDireito)new TDireito()).Codigo, ((Modulo)new TModulo()).Codigo)));
		}

		private void ObterImpressaoDigital(IDialogo dataContext)
		{
			_impressaoDigitalService.ObterImpressaoDigital((Action<UsuarioBase>)delegate(UsuarioBase usuario)
			{
				if (usuario == null)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum usuário localizado por impressão digital!", false, HorizontalAlignment.Center);
				}
				else
				{
					PermissaoAcessoDialogoDataContext permissaoAcessoContext = default(PermissaoAcessoDialogoDataContext);
					ref PermissaoAcessoDialogoDataContext reference = ref permissaoAcessoContext;
					IDialogo obj = dataContext;
					reference = (PermissaoAcessoDialogoDataContext)(object)((obj is PermissaoAcessoDialogoDataContext) ? obj : null);
					permissaoAcessoContext.Usuario = usuario.Nome;
					permissaoAcessoContext.Senha = usuario.Senha;
					ThreadPrincipal.Executar((Action)async delegate
					{
						await ((DialogoDataContext)permissaoAcessoContext).Confirmar();
					});
				}
			}, (Action<Exception>)delegate(Exception excecao)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					if (HelperException.ShowLastException(excecao, "Tentar novamente?", true, false))
					{
						ObterImpressaoDigital(dataContext);
					}
				});
			});
		}

		public async Task<AutorizacaoDto> Autorizar(Token token, IDireito direito, Modulo modulo)
		{
			return await _service.Canal<IAutorizacaoModuleService, Task<AutorizacaoDto>>((Func<IAutorizacaoModuleService, Task<AutorizacaoDto>>)((IAutorizacaoModuleService service) => service.AutorizarAsync(token.NomeUsuario, token.Senha, direito.Codigo, modulo.Codigo)));
		}

		public async Task<AutorizacaoDto> AutorizarHash(Token token, IDireito direito, Modulo modulo)
		{
			return await _service.Canal<IAutorizacaoModuleService, Task<AutorizacaoDto>>((Func<IAutorizacaoModuleService, Task<AutorizacaoDto>>)((IAutorizacaoModuleService service) => service.AutorizarHashAsync(token.NomeUsuario, direito.Codigo, modulo.Codigo)));
		}
	}
}
namespace Otus.Modulo.Autenticacoes.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Autenticacoes.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.Autenticacoes.Projections
{
	public static class PerfilEmpresaProjection
	{
		public static PerfilEmpresaSessao Morfar(this IPerfilEmpresa perfilEmpresa)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			return new PerfilEmpresaSessao(HelperObject.To<Emitente>((object)perfilEmpresa.Empresa).Morfar(), (IEnumerable<Restricao>)perfilEmpresa.Perfil.Cliente);
		}

		public static IEnumerable<PerfilEmpresaSessao> MorfarCollection(this IEnumerable<IPerfilEmpresa> empresasDto)
		{
			List<IPerfilEmpresa> source = HelperIEnumerable.ToListEmptyWhenNull<IPerfilEmpresa>(empresasDto);
			if (source.Any())
			{
				return source.Select((IPerfilEmpresa x) => x.Morfar());
			}
			return new List<PerfilEmpresaSessao>();
		}
	}
	public static class PessoaProjection
	{
		public static Pessoa Morfar(this Emitente emitente)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)emitente))
			{
				ObjectId id = ((EntityBase)emitente).Id;
				return new Pessoa(((object)(ObjectId)(ref id)).ToString(), ((Pessoa)emitente).Nome, HelperObject.Is<Matriz>((object)emitente), MunicipioValueObjectService.ObterMunicipioPorCodigoIbge(((Pessoa)emitente).Carteira.EnderecoPrincipalNacional.Municipio.CodigoIbge), ((Pessoa)emitente).CpfCnpj, ((Pessoa)emitente).Carteira.Ie.Numero, ((Juridica)emitente).RegistroMunicipal, ((Endereco)((Pessoa)emitente).Carteira.EnderecoPrincipalNacional).Logradouro, ((Endereco)((Pessoa)emitente).Carteira.EnderecoPrincipalNacional).Complemento, ((Endereco)((Pessoa)emitente).Carteira.EnderecoPrincipalNacional).Bairro, ((Endereco)((Pessoa)emitente).Carteira.EnderecoPrincipalNacional).Numero, ((Telefone)((Pessoa)emitente).Carteira.TelefonePrincipal).Numero, emitente.RamoAtividade, emitente.PontoDeVenda, emitente.Sistema, ((Pessoa)emitente).Imagem, emitente.ImagemMarcaAgua, emitente.TransparenciaMarcaAgua, ((Juridica)emitente).NomeDoResponsavel, emitente.RegimeTributario, (IEnumerable<int>)HelperIEnumerable.ToListEmptyWhenNull<int>(emitente.ModulosAdicionais), emitente.ObterHabilitadoMEI(), ((Pessoa)emitente).Carteira.EnderecoPrincipalNacional.Cep, ((Pessoa)emitente).CpfCnpjFormatado, ((Juridica)emitente).NomeFantasia, emitente.IsProdutorRural);
			}
			return null;
		}
	}
}

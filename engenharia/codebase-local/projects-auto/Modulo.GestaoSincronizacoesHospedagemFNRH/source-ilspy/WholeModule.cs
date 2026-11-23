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
using DigisatServer.Application.Main.Dto.Auxiliares.GestaoSincronizacaoFNRH;
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
using Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoSincronizacoesHospedagemFNRH")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoSincronizacoesHospedagemFNRH")]
[assembly: AssemblyCopyright("Copyright ©  2026")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoSincronizacoesHospedagemFNRH
{
	[Module(ModuleName = "GestaoSincronizacaoHospedagemFNRHModulo", OnDemand = true)]
	public class GestaoSincronizacaoHospedagemFNRHModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoSincronizacaoHospedagemFNRHModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacoesHospedagemFNRHView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoSincronizacaoHospedagemFNRHFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.Properties.Resources", typeof(Resources).Assembly);
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
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;
	}
}
namespace Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.Views
{
	public class GestaoSincronizacaoHospedagemFNRHFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacaoHospedagemFNRHFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesHospedagemFNRH;component/views/gestaosincronizacaohospedagemfnrhfichaview.xaml", UriKind.Relative);
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
	public class GestaoSincronizacoesHospedagemFNRHView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoSincronizacoesHospedagemFNRHView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesHospedagemFNRH;component/views/gestaosincronizacoeshospedagemfnrhview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.Views.GestaoSincronizacaoHospedagemFNRHFichaViewDetalhes
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
				Uri resourceLocator = new Uri("/Modulo.GestaoSincronizacoesHospedagemFNRH;component/views/gestaosincronizacaohospedagemfnrhfichaviewdetalhes/sincronizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoSincronizacoesHospedagemFNRH.ViewModels
{
	public class GestaoSincronizacaoHospedagemFNRHFichaViewModel : FichaBase<ObjetoDigisatFNRHDto, SincronizacaoViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<ObjetoDigisatFNRHDto>> _objetos;

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

		public GestaoSincronizacaoHospedagemFNRHFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
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
				GestaoSincronizacaoHospedagemFNRHFormulario gestaoSincronizacaoHospedagemFNRHFormulario = new GestaoSincronizacaoHospedagemFNRHFormulario(ObterObjetos, SetarSincronizarConsultarNovamente);
				ForcarSincronismoCommand = (ICommand)new DelegateCommand<object>((Action<object>)gestaoSincronizacaoHospedagemFNRHFormulario.SincronizarConsultarNovamente, (Func<object, bool>)gestaoSincronizacaoHospedagemFNRHFormulario.PodeSincronizarConsultarNovamente, (bool?)null);
				base.Formulario = (IFormulario)(object)gestaoSincronizacaoHospedagemFNRHFormulario;
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
				_service.Canal<IGestaoSincronizacaoFNRHModuleService, Task<IEnumerable<ObjetoDigisatFNRHDto>>>((Func<IGestaoSincronizacaoFNRHModuleService, Task<IEnumerable<ObjetoDigisatFNRHDto>>>)((IGestaoSincronizacaoFNRHModuleService service) => _objetos = service.PesquisarPorDataInclusao(dataInicial, dataFinal)));
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
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoSincronizacaoHospedagemFNRHFormulario>((object)base.Formulario)).Abas)
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

		public void SetarSincronizarConsultarNovamente(IEnumerable<ObjetoDigisatFNRHDto> objetos)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoSincronizacaoFNRHModuleService>((Action<IGestaoSincronizacaoFNRHModuleService>)delegate(IGestaoSincronizacaoFNRHModuleService service)
				{
					service.SetarSincronizarConsultarNovamente(objetos);
				});
			}, (Action<object>)delegate
			{
				DialogoFactory.MostrarDialogoConfirma("Os registros serão sincronizados em breve!", false, HorizontalAlignment.Center);
			});
		}
	}
	public class GestaoSincronizacaoHospedagemFNRHFormulario : Formulario
	{
		private readonly Action<IEnumerable<ObjetoDigisatFNRHDto>> _setarSincronizarConsultarNovamente;

		private SincronizacaoViewModel _sincronizacaoViewModel;

		private SincronizacaoAbaViewModel _sincronizacaoAbaViewModel;

		public GestaoSincronizacaoHospedagemFNRHFormulario(Action<DateTime, DateTime> obterObjetos, Action<IEnumerable<ObjetoDigisatFNRHDto>> setarNaoSincronizado)
		{
			GestaoSincronizacaoHospedagemFNRHFormulario gestaoSincronizacaoHospedagemFNRHFormulario = this;
			_setarSincronizarConsultarNovamente = setarNaoSincronizado;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoSincronizacaoHospedagemFNRHFormulario.ProjetarAbas(obterObjetos);
			});
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		public bool PodeSincronizarConsultarNovamente(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public void SincronizarConsultarNovamente(object obj)
		{
			List<ObjetoDigisatFNRHDto> list = HelperIEnumerable.ToListEmptyWhenNull<ObjetoDigisatFNRHDto>(_sincronizacaoViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum registro selecionado para sincronizar", false, HorizontalAlignment.Center);
			}
			else
			{
				_setarSincronizarConsultarNovamente(list);
			}
		}

		private void ProjetarAbas(Action<DateTime, DateTime> obterObjetos)
		{
			_sincronizacaoViewModel = new SincronizacaoViewModel(obterObjetos);
			_sincronizacaoAbaViewModel = new SincronizacaoAbaViewModel((IViewModelDtoBase)(object)_sincronizacaoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)_sincronizacaoAbaViewModel });
		}
	}
	public class GestaoSincronizacoesHospedagemFNRHViewModel : ModuloBase
	{
		public GestaoSincronizacoesHospedagemFNRHViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoSincronizacoesHospedagemFNRHRegion, "GestaoSincronizacaoHospedagemFNRHFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class SincronizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SincronizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SINCRONIZAÇÃO", "Resumo das sincronizações", viewModel)
		{
		}
	}
	public class SincronizacaoViewModel : ViewModelDtoBase<IEnumerable<ObjetoDigisatFNRHDto>>
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

		public IEnumerable<ObjetoDigisatFNRHDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ObjetoDigisatFNRHDto>>((Expression<Func<IEnumerable<ObjetoDigisatFNRHDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ObjetoDigisatFNRHDto>>((Expression<Func<IEnumerable<ObjetoDigisatFNRHDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoObjetos";

		public ICommand PesquisarCommand { get; set; }

		public SincronizacaoViewModel()
		{
		}

		public SincronizacaoViewModel(Action<DateTime, DateTime> obterObjetos)
			: base((IEnumerable<ObjetoDigisatFNRHDto>)null)
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

		public IEnumerable<ObjetoDigisatFNRHDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ObjetoDigisatFNRHDto>();
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

		public void SetarResultado(IEnumerable<ObjetoDigisatFNRHDto> objetos)
		{
			Itens = objetos;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.MenusRapido;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.AtualizacoesGrupoComplementos.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.AtualizacoesGrupoComplementos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.AtualizacoesGrupoComplementos")]
[assembly: AssemblyCopyright("Copyright ©  2025")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AtualizacoesGrupoComplementos
{
	[Module(ModuleName = "AtualizacaoGrupoComplementoModulo", OnDemand = true)]
	[ModuleDependency("GrupoMenuRapidoModulo")]
	public class AtualizacaoGrupoComplementoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AtualizacaoGrupoComplementoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AtualizacoesGrupoComplementoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AtualizacaoGrupoComplementoFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AtualizacoesGrupoComplementos.Views
{
	public class AtualizacaoGrupoComplementoFichaView : UserControl, IComponentConnector
	{
		internal ButtonInfo PesquisarGrupoComplemento;

		internal ListViewFocusable listaItens;

		private bool _contentLoaded;

		public AtualizacaoGrupoComplementoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AtualizacoesGrupoComplementos;component/views/atualizacaogrupocomplementofichaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				PesquisarGrupoComplemento = (ButtonInfo)target;
				break;
			case 2:
				listaItens = (ListViewFocusable)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class AtualizacoesGrupoComplementoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AtualizacoesGrupoComplementoView()
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
				Uri resourceLocator = new Uri("/Modulo.AtualizacoesGrupoComplementos;component/views/atualizacoesgrupocomplementoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AtualizacoesGrupoComplementos.ViewModels
{
	public class AtualizacaoGrupoComplementoFichaViewModel : NavigationBase
	{
		public Lista<GrupoComplementoMenuRapidoViewModel> GruposComplementoMenuRapido
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<GrupoComplementoMenuRapidoViewModel>>((Expression<Func<Lista<GrupoComplementoMenuRapidoViewModel>>>)(() => GruposComplementoMenuRapido));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<GrupoComplementoMenuRapidoViewModel>>((Expression<Func<Lista<GrupoComplementoMenuRapidoViewModel>>>)(() => GruposComplementoMenuRapido), value);
			}
		}

		public GrupoMenuRapidoDto GrupoMenuRapido
		{
			get
			{
				return ((BindableBase)this).GetProperty<GrupoMenuRapidoDto>((Expression<Func<GrupoMenuRapidoDto>>)(() => GrupoMenuRapido));
			}
			set
			{
				((BindableBase)this).SetProperty<GrupoMenuRapidoDto>((Expression<Func<GrupoMenuRapidoDto>>)(() => GrupoMenuRapido), value);
			}
		}

		public ICommand PesquisarGrupoMenuRapidoCommand { get; set; }

		public ICommand AtualizarGrupoComplementoTodosItensCommand { get; set; }

		public AtualizacaoGrupoComplementoFichaViewModel()
			: base(string.Empty)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			PesquisarGrupoMenuRapidoCommand = (ICommand)new DelegateCommand((Action)PesquisarGrupoMenuRapido);
			AtualizarGrupoComplementoTodosItensCommand = (ICommand)new DelegateCommand((Action)AtualizarGrupoComplementoTodosItens, (Func<bool>)PodeAtualizarGrupoComplementoTodosItens, (bool?)null);
		}

		private void PesquisarGrupoMenuRapido()
		{
			((NavigationBase)this).Navegar((object)new SelecaoCommandParameter<GrupoMenuRapidoDto>("GrupoMenuListaView", (Action<GrupoMenuRapidoDto>)PreencherGrupoMenuRapido, true));
		}

		private void PreencherGrupoMenuRapido(GrupoMenuRapidoDto dto)
		{
			GrupoMenuRapido = dto;
			IEnumerable<MenuRapidoDto> enumerable = DadosSistema.GetService().Canal<IMenuRapidoModuleService, IEnumerable<MenuRapidoDto>>((Func<IMenuRapidoModuleService, IEnumerable<MenuRapidoDto>>)((IMenuRapidoModuleService service) => service.ObterMenuRapidoPorGrupoMenuReferencia(((EntityBaseDto)dto).Id.ToString()))).Reverse();
			GruposComplementoMenuRapido = new Lista<GrupoComplementoMenuRapidoViewModel>();
			foreach (MenuRapidoDto item in enumerable)
			{
				foreach (ItemMenuRapidoDto item2 in HelperIEnumerable.ToListEmptyWhenNull<ItemMenuRapidoDto>(item.ItensMenuRapido.Where((ItemMenuRapidoDto x) => x.GruposMenuRapido.Any((GrupoComplementoMenuRapidoDto y) => y.GrupoReferencia.Equals(((EntityBaseDto)dto).Id)))))
				{
					GruposComplementoMenuRapido.Adicionar(GrupoComplementoMenuRapidoViewModel.Init().ComMenuRapido(((EntityBaseDto)item).Id).ComItemMenuRapido(item2)
						.ComGrupoComplementoReferencia(((EntityBaseDto)GrupoMenuRapido).Id)
						.Criar());
				}
			}
			GruposComplementoMenuRapido.PosicionarNoPrimeiro();
		}

		private void AtualizarGrupoComplementoTodosItens()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Deseja atualizar o grupo de complemento de todos os itens?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			foreach (GrupoComplementoMenuRapidoViewModel iten in GruposComplementoMenuRapido.Itens)
			{
				iten.ExecutarAtualizacao(atualizacaoTodos: true);
			}
			PreencherGrupoMenuRapido(GrupoMenuRapido);
			DialogoFactory.MostrarDialogoConfirma("Itens atualizados com sucesso!", false, HorizontalAlignment.Center);
		}

		private bool PodeAtualizarGrupoComplementoTodosItens()
		{
			return GruposComplementoMenuRapido?.Itens?.Any() ?? false;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			object obj = navigationParameters["GrupoMenuRapidoDto"];
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			if (HelperObject.IsNotNull((object)val))
			{
				PreencherGrupoMenuRapido(val);
			}
		}
	}
	public class AtualizacoesGrupoComplementoViewModel : ModuloBase
	{
		public AtualizacoesGrupoComplementoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAtualizacoesGrupoComplementoRegion, typeof(AtualizacaoGrupoComplementoFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class GrupoComplementoMenuRapidoViewModel : BindableBase
	{
		public string MenuRapidoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MenuRapidoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MenuRapidoReferencia), value);
			}
		}

		public ItemMenuRapidoDto ItemMenuRapido
		{
			get
			{
				return ((BindableBase)this).GetProperty<ItemMenuRapidoDto>((Expression<Func<ItemMenuRapidoDto>>)(() => ItemMenuRapido));
			}
			set
			{
				((BindableBase)this).SetProperty<ItemMenuRapidoDto>((Expression<Func<ItemMenuRapidoDto>>)(() => ItemMenuRapido), value);
			}
		}

		public string GrupoMenuRapidoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => GrupoMenuRapidoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => GrupoMenuRapidoReferencia), value);
			}
		}

		public Visibility LoaderVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => LoaderVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => LoaderVisivel), value);
			}
		}

		public ICommand ExecutarAtualizacaoCommand { get; set; }

		public GrupoComplementoMenuRapidoViewModel()
		{
		}

		private GrupoComplementoMenuRapidoViewModel(string menuRapidoReferencia, ItemMenuRapidoDto itemMenuRapido, string grupoMenuRapidoReferencia)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			MenuRapidoReferencia = menuRapidoReferencia;
			ItemMenuRapido = itemMenuRapido;
			GrupoMenuRapidoReferencia = grupoMenuRapidoReferencia;
			LoaderVisivel = Visibility.Collapsed;
			ExecutarAtualizacaoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				ExecutarAtualizacao();
			});
		}

		public void ExecutarAtualizacao(bool atualizacaoTodos = false)
		{
			LoaderVisivel = Visibility.Visible;
			try
			{
				DadosSistema.GetService().Canal<IMenuRapidoModuleService>((Action<IMenuRapidoModuleService>)delegate(IMenuRapidoModuleService service)
				{
					service.AtualizarGrupoComplemento(MenuRapidoReferencia, ItemMenuRapido.Id, GrupoMenuRapidoReferencia);
				});
				if (!atualizacaoTodos)
				{
					DialogoFactory.MostrarDialogoConfirma($"Item {ItemMenuRapido.DescricaoTela} atualizado com sucesso!", false, HorizontalAlignment.Center);
				}
			}
			finally
			{
				LoaderVisivel = Visibility.Collapsed;
			}
		}

		public static GrupoComplementoMenuRapidoViewModel Init()
		{
			return new GrupoComplementoMenuRapidoViewModel();
		}

		public GrupoComplementoMenuRapidoViewModel ComMenuRapido(string menuRapidoReferencia)
		{
			MenuRapidoReferencia = menuRapidoReferencia;
			return this;
		}

		public GrupoComplementoMenuRapidoViewModel ComItemMenuRapido(ItemMenuRapidoDto itemMenuRapido)
		{
			ItemMenuRapido = itemMenuRapido;
			return this;
		}

		public GrupoComplementoMenuRapidoViewModel ComGrupoComplementoReferencia(string grupoMenuRapidoReferencia)
		{
			GrupoMenuRapidoReferencia = grupoMenuRapidoReferencia;
			return this;
		}

		public GrupoComplementoMenuRapidoViewModel Criar()
		{
			return new GrupoComplementoMenuRapidoViewModel(MenuRapidoReferencia, ItemMenuRapido, GrupoMenuRapidoReferencia);
		}
	}
}
namespace Otus.Modulo.AtualizacoesGrupoComplementos.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.AtualizacoesGrupoComplementos.Properties.Resources", typeof(Resources).Assembly);
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

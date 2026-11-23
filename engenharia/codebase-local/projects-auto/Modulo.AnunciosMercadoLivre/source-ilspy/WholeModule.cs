using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Kits;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.MercadosLivres;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.MercadosLivresModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.MercadoLivre;
using DigisatServer.Infrastructure.Crosscutting.Service.MercadoLivre.Classes.Anuncios;
using DigisatServer.Infrastructure.Crosscutting.Service.MercadoLivre.Classes.Categorias;
using DigisatServer.Infrastructure.Crosscutting.Service.MercadoLivre.Classes.Usuarios;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.AnunciosMercadoLivre.ViewModels.Atributos;
using Otus.Modulo.AnunciosMercadoLivre.ViewModels.Variacoes;
using Otus.Modulo.AnunciosMercadoLivre.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.AnunciosMercadoLivre")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.AnunciosMercadoLivre")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AnunciosMercadoLivre
{
	[Module(ModuleName = "AnuncioMercadoLivreModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("TabelaPrecoModulo")]
	[ModuleDependency("MercadoLivreModulo")]
	public class AnuncioMercadoLivreModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AnuncioMercadoLivreModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AnunciosMercadoLivreView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioMercadoLivreListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioMercadoLivreFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioMercadoLivreFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioMercadoLivreImportarFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AnunciosMercadoLivre.Views
{
	public class AnuncioMercadoLivreFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public AnuncioMercadoLivreFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaadicionarview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			if (connectionId == 1)
			{
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class AnuncioMercadoLivreFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public AnuncioMercadoLivreFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaatualizarview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			if (connectionId == 1)
			{
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class AnuncioMercadoLivreImportarFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioMercadoLivreImportarFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivreimportarfichaview.xaml", UriKind.Relative);
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
	public class AnuncioMercadoLivreListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioMercadoLivreListaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrelistaview.xaml", UriKind.Relative);
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
	public class AnunciosMercadoLivreView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnunciosMercadoLivreView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciosmercadolivreview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AnunciosMercadoLivre.Views.AnuncioMercadoLivreFichaViewDetalhes
{
	public class AnuncioMercadoLivreCategoriaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioMercadoLivreCategoriaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivrecategoriaview.xaml", UriKind.Relative);
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
	public class AnuncioMercadoLivreGarantiaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioMercadoLivreGarantiaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivregarantiaview.xaml", UriKind.Relative);
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
	public class AnuncioMercadoLivreImagensView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioMercadoLivreImagensView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivreimagensview.xaml", UriKind.Relative);
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
	public class AnuncioMercadoLivreTabelaMedidasView : UserControl, IComponentConnector
	{
		internal ItemsControl ListaTamanhos;

		private bool _contentLoaded;

		public AnuncioMercadoLivreTabelaMedidasView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivretabelamedidasview.xaml", UriKind.Relative);
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
				ListaTamanhos = (ItemsControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class AnuncioMercadoLivreVariacaoView : UserControl, IComponentConnector
	{
		internal ItemsControl ListaVariacoes;

		private bool _contentLoaded;

		public AnuncioMercadoLivreVariacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivrevariacaoview.xaml", UriKind.Relative);
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
				ListaVariacoes = (ItemsControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class AnuncioMercadoLivreView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterProduto;

		internal ButtonInfo ParameterKit;

		private bool _contentLoaded;

		public AnuncioMercadoLivreView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosMercadoLivre;component/views/anunciomercadolivrefichaviewdetalhes/anunciomercadolivreview.xaml", UriKind.Relative);
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
				ParameterProduto = (ButtonInfo)target;
				break;
			case 2:
				ParameterKit = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.AnunciosMercadoLivre.ViewModels
{
	public class AnuncioMercadoLivreCategoriaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioMercadoLivreCategoriaAbaViewModel(IViewModelDtoBase viewModel)
			: base("Categoria", "Atributos específicos", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreCategoriaViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly IList<IDisposable> _disposables = new List<IDisposable>();

		private readonly MercadoLivreApi _api;

		private readonly bool _projetando;

		public Lista<Category> CategoriasNivel0 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel1 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel2 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel3 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel4 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel5 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel6 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel7 { get; set; } = new Lista<Category>();


		public Lista<AtributoViewModel> Atributos { get; set; } = new Lista<AtributoViewModel>();


		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Categoria' é obrigatório.")]
		public string CategoriaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CategoriaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CategoriaDescricao), value);
			}
		}

		public string CategoriaId
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CategoriaId));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CategoriaId), value);
			}
		}

		public ICommand AlterarCategoriaCommand { get; set; }

		public event EventHandler<string> AlterouCategoriaEvent;

		public AnuncioMercadoLivreCategoriaViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_0223: Expected O, but got Unknown
			_api = new MercadoLivreApi(DadosSistema.ObterProxy());
			((MercadoLivreBaseApi)_api).SetarToken(DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).TokenAcesso);
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel0.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel0AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel1.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel1AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel2.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel2AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel3.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel3AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel4.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel4AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel5.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel5AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel6.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel6AlterouItemSelecionado(itemSelecionado);
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel7.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				await CategoriasNivel7AlterouItemSelecionado(itemSelecionado);
			}));
			AlterarCategoriaCommand = (ICommand)new AsyncCommand((Func<Task>)AlterarCategoria, (Func<bool>)PodeAlterarCategoria, false, (bool?)null);
		}

		private bool PodeAlterarCategoria()
		{
			return !string.IsNullOrEmpty(base.Dto.CategoriaDescricao);
		}

		private async Task AlterarCategoria()
		{
			OnAlterouCategoriaEvent(string.Empty);
			await ListarCategoriasNivel0().ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		public override async void ProjectedAsViewModel()
		{
			CategoriaDescricao = ((ViewModelDtoBase<AnuncioDto>)this).Dto.CategoriaDescricao;
			CategoriaId = ((ViewModelDtoBase<AnuncioDto>)this).Dto.CategoriaId;
			ProjectAtributos();
			if (!string.IsNullOrEmpty(CategoriaDescricao))
			{
				return;
			}
			await ListarCategoriasNivel0().ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private void ProjectAtributos()
		{
			foreach (AtributoDto atributo in base.Dto.Atributos)
			{
				try
				{
					AtributoFactory factory = AtributoFactory.Init().SetarTipo(ObterTipoAtributo(atributo)).SetarTitulo(atributo.Titulo)
						.SetarValueId(atributo.Id);
					HelperObject.ToDo<AtributoTextoDto>((object)atributo, (Action<AtributoTextoDto>)delegate(AtributoTextoDto texto)
					{
						factory = factory.SetarValor(texto.Valor);
					});
					HelperObject.ToDo<AtributoInteiroDto>((object)atributo, (Action<AtributoInteiroDto>)delegate(AtributoInteiroDto numerico)
					{
						factory = factory.SetarValor(numerico.Valor);
					});
					HelperObject.ToDo<AtributoBoleanoDto>((object)atributo, (Action<AtributoBoleanoDto>)delegate(AtributoBoleanoDto boleano)
					{
						factory = factory.SetarValor(boleano.Valor);
					});
					HelperObject.ToDo<AtributoUnidadeDto>((object)atributo, (Action<AtributoUnidadeDto>)delegate(AtributoUnidadeDto unidade)
					{
						factory = factory.SetarUnidades(((AtributoListaDto)unidade).Opcoes).SetarValor(((AtributoListaDto)unidade).OpcaoSelecionada).SetarValor(unidade.Valor);
					});
					if (HelperObject.IsExactlyObject<AtributoListaDto>((object)atributo))
					{
						HelperObject.ToDo<AtributoListaDto>((object)atributo, (Action<AtributoListaDto>)delegate(AtributoListaDto lista)
						{
							factory = factory.SetarOpcoes(lista.Opcoes).SetarValor(lista.OpcaoSelecionada);
						});
					}
					Atributos.Adicionar(factory.Criar());
				}
				catch (Exception ex)
				{
					HelperException.ShowLastException(ex, "", false, false);
				}
			}
		}

		private string ObterTipoAtributo(AtributoDto atributo)
		{
			if (HelperObject.Is<AtributoTextoDto>((object)atributo))
			{
				return Attribute.type_string;
			}
			if (HelperObject.Is<AtributoBoleanoDto>((object)atributo))
			{
				return Attribute.type_boolean;
			}
			if (HelperObject.Is<AtributoInteiroDto>((object)atributo))
			{
				return Attribute.type_number;
			}
			if (HelperObject.Is<AtributoUnidadeDto>((object)atributo))
			{
				return Attribute.type_number_unit;
			}
			if (HelperObject.Is<AtributoListaDto>((object)atributo))
			{
				return Attribute.type_list;
			}
			throw new NotImplementedException();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.CategoriaDescricao = CategoriaDescricao;
			val.CategoriaId = CategoriaId;
			val.Atributos = HelperIEnumerable.ToListEmptyWhenNull<AtributoDto>(Atributos.Itens.Select((AtributoViewModel atributo) => atributo.ObterDto()));
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		private async Task ListarCategoriasNivel0()
		{
			IEnumerable<Category> enumerable = await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<Category>>("/sites/MLB/categories", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
			CategoriasNivel0.AdicionarSemPosicionar(enumerable);
		}

		private async Task CategoriasNivel0AlterouItemSelecionado(Category category)
		{
			CategoriasNivel1.Limpar();
			CategoriasNivel2.Limpar();
			CategoriasNivel3.Limpar();
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel1.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel1AlterouItemSelecionado(Category category)
		{
			CategoriasNivel2.Limpar();
			CategoriasNivel3.Limpar();
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel2.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel2AlterouItemSelecionado(Category category)
		{
			CategoriasNivel3.Limpar();
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel3.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel3AlterouItemSelecionado(Category category)
		{
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel4.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel4AlterouItemSelecionado(Category category)
		{
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel5.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel5AlterouItemSelecionado(Category category)
		{
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel6.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel6AlterouItemSelecionado(Category category)
		{
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			try
			{
				ListCategories val = await ((MercadoLivreBaseApi)_api).GetAsync<ListCategories>($"/categories/{category.id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				CategoriasNivel7.AdicionarSemPosicionar(val.children_categories);
			}
			catch (Exception)
			{
			}
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task CategoriasNivel7AlterouItemSelecionado(Category category)
		{
			CategoriaDescricao = category.name;
			CategoriaId = category.id;
			await ObterAtributosCategoria(category).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private async Task ObterAtributosCategoria(Category itemSeleciondo)
		{
			try
			{
				OnAlterouCategoriaEvent(itemSeleciondo.id);
				IEnumerable<Attribute> source = await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<Attribute>>($"/categories/{itemSeleciondo.id}/attributes", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
				if (!source.Any())
				{
					return;
				}
				Atributos.Limpar();
				foreach (Attribute item in source.Where((Attribute attr) => attr.Required))
				{
					try
					{
						Atributos.Adicionar(AtributoFactory.Init().SetarValueId(item.id).SetarTipo(item.value_type)
							.SetarTitulo(item.name)
							.SetarOpcoes(((IEnumerable<AttributeValue>)HelperIEnumerable.ToListEmptyWhenNull<AttributeValue>(item.values)).Select((Func<AttributeValue, AtributoOpcaoDto>)((AttributeValue value) => new AtributoOpcaoDto
							{
								Id = value.id,
								Valor = value.name
							})))
							.SetarUnidades(((IEnumerable<AttributeValue>)HelperIEnumerable.ToListEmptyWhenNull<AttributeValue>(item.allowed_units)).Select((Func<AttributeValue, AtributoOpcaoDto>)((AttributeValue value) => new AtributoOpcaoDto
							{
								Id = value.id,
								Valor = value.name
							})))
							.SetarRequired(item.Required)
							.Criar());
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)CategoriasNivel0).Dispose();
			((ViewModelValidationBase)CategoriasNivel1).Dispose();
			((ViewModelValidationBase)CategoriasNivel2).Dispose();
			((ViewModelValidationBase)CategoriasNivel3).Dispose();
			((ViewModelValidationBase)CategoriasNivel4).Dispose();
			((ViewModelValidationBase)CategoriasNivel5).Dispose();
			((ViewModelValidationBase)CategoriasNivel6).Dispose();
			((ViewModelValidationBase)CategoriasNivel7).Dispose();
			((ViewModelValidationBase)Atributos).Dispose();
			DesassinarEventos();
			((ViewModelValidationBase)this).Dispose();
		}

		private void DesassinarEventos()
		{
			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			_disposables.Clear();
		}

		protected virtual void OnAlterouCategoriaEvent(string e)
		{
			this.AlterouCategoriaEvent?.Invoke(this, e);
		}
	}
	public class AnuncioMercadoLivreGarantiaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioMercadoLivreGarantiaAbaViewModel(IViewModelDtoBase viewModel)
			: base("Garantia", "Informações sobre a garantia", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreGarantiaViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly IList<IDisposable> _disposables = new List<IDisposable>();

		private readonly MercadoLivreApi _api;

		public Lista<WarrantyOption> TiposGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<WarrantyOption>>((Expression<Func<Lista<WarrantyOption>>>)(() => TiposGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<WarrantyOption>>((Expression<Func<Lista<WarrantyOption>>>)(() => TiposGarantia), value);
			}
		}

		public Lista<WarrantyOption> TiposTempoGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<WarrantyOption>>((Expression<Func<Lista<WarrantyOption>>>)(() => TiposTempoGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<WarrantyOption>>((Expression<Func<Lista<WarrantyOption>>>)(() => TiposTempoGarantia), value);
			}
		}

		public Visibility TempoGarantiaVisibility
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => TempoGarantiaVisibility));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => TempoGarantiaVisibility), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Tipo da garantia' é obrigatório.")]
		public string TipoGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TipoGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TipoGarantia), value);
			}
		}

		public int TempoGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => TempoGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => TempoGarantia), value);
			}
		}

		public AnuncioMercadoLivreGarantiaViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			TiposGarantia = new Lista<WarrantyOption>();
			TiposTempoGarantia = new Lista<WarrantyOption>();
			_api = new MercadoLivreApi(DadosSistema.ObterProxy());
			((MercadoLivreBaseApi)_api).SetarToken(DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).TokenAcesso);
			_disposables.Add(ObservableExtensions.Subscribe<WarrantyOption>(TiposGarantia.WhenAlterouItemSelecionado, (Action<WarrantyOption>)delegate(WarrantyOption x)
			{
				AlterouTipoGarantia(x.name);
			}));
		}

		private void AlterouTipoGarantia(string tipoGarantia)
		{
			TipoGarantia = tipoGarantia;
			TempoGarantiaVisibility = ((tipoGarantia.StartsWith("Sem") || string.IsNullOrEmpty(TipoGarantia)) ? Visibility.Collapsed : Visibility.Visible);
		}

		private async Task ObterOpcoesGarantia(string categoryId)
		{
			try
			{
				if (string.IsNullOrEmpty(categoryId))
				{
					TipoGarantia = string.Empty;
					TempoGarantiaVisibility = Visibility.Collapsed;
					TempoGarantia = 0;
					TiposGarantia.Limpar();
					TiposTempoGarantia.Limpar();
				}
				IEnumerable<Warranty> source = (await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<Warranty>>($"/categories/{categoryId}/sale_terms", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null)).Where((Warranty option) => option.id.Equals("WARRANTY_TYPE") || option.id.Equals("WARRANTY_TIME"));
				TiposGarantia.Limpar();
				Lista<WarrantyOption> tiposGarantia = TiposGarantia;
				Warranty obj = source.FirstOrDefault((Warranty option) => option.id.Equals("WARRANTY_TYPE"));
				tiposGarantia.AdicionarSemPosicionar(((obj != null) ? obj.values : null) ?? Enumerable.Empty<WarrantyOption>());
				TiposGarantia.PosicionarNoPrimeiro();
				TiposTempoGarantia.Limpar();
				Lista<WarrantyOption> tiposTempoGarantia = TiposTempoGarantia;
				Warranty obj2 = source.FirstOrDefault((Warranty option) => option.id.Equals("WARRANTY_TIME"));
				tiposTempoGarantia.AdicionarSemPosicionar(((obj2 != null) ? obj2.allowed_units : null) ?? Enumerable.Empty<WarrantyOption>());
				TiposTempoGarantia.PosicionarNoPrimeiro();
				TempoGarantia = 0;
			}
			catch (Exception)
			{
			}
		}

		public override async void ProjectedAsViewModel()
		{
			await ObterOpcoesGarantia(((ViewModelDtoBase<AnuncioDto>)this).Dto.CategoriaId).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
			TiposGarantia.Posicionar((Expression<Func<WarrantyOption, bool>>)((WarrantyOption x) => x.name.Equals(((ViewModelDtoBase<AnuncioDto>)this).Dto.Garantia.TipoGarantia)));
			TempoGarantia = ((ViewModelDtoBase<AnuncioDto>)this).Dto.Garantia.TempoGarantia;
			TiposTempoGarantia.Posicionar((Expression<Func<WarrantyOption, bool>>)((WarrantyOption x) => x.name.Equals(((ViewModelDtoBase<AnuncioDto>)this).Dto.Garantia.TipoTempoGarantia)));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.Garantia = new GarantiaAnuncioDto
			{
				TempoGarantia = TempoGarantia,
				TipoGarantia = TiposGarantia.ItemSelecionado.name,
				TipoTempoGarantia = TiposTempoGarantia.ItemSelecionado.name
			};
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		public async void AlterouCategoria(string categoryId)
		{
			await ObterOpcoesGarantia(categoryId).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)TiposGarantia).Dispose();
			((ViewModelValidationBase)TiposTempoGarantia).Dispose();
			DesassinarEventos();
			((ViewModelValidationBase)this).Dispose();
		}

		private void DesassinarEventos()
		{
			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			_disposables.Clear();
		}
	}
	public class AnuncioMercadoLivreImagensAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioMercadoLivreImagensAbaViewModel(IViewModelDtoBase viewModel)
			: base("Imagens", "Imagens", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreImagensViewModel : ViewModelDtoBase<AnuncioDto>
	{
		[Required(ErrorMessage = "É obrigatório cadastrar pelo menos uma imagem.")]
		public Lista<ImagemAnuncioDto> Imagens { get; set; } = new Lista<ImagemAnuncioDto>();


		public string UrlImagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => UrlImagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => UrlImagem), value);
			}
		}

		public override bool Valid
		{
			get
			{
				if (HelperObject.IsNotNull((object)Imagens))
				{
					return Imagens.Numero > 0;
				}
				return false;
			}
		}

		public override string Error
		{
			get
			{
				if (!((Validation)this).Valid)
				{
					return ((Validation)this).Error;
				}
				return string.Empty;
			}
		}

		public ICommand<string> AdicionarImagemCommand { get; set; }

		public ICommand<ImagemAnuncioDto> RemoverImagemCommand { get; set; }

		public AnuncioMercadoLivreImagensViewModel(AnuncioDto dto)
			: base(dto)
		{
			AdicionarImagemCommand = (ICommand<string>)(object)new DelegateCommand<string>((Action<string>)AdicionarImagem, (Func<string, bool>)PodeAdicionarImagem, (bool?)null);
			RemoverImagemCommand = (ICommand<ImagemAnuncioDto>)(object)new DelegateCommand<ImagemAnuncioDto>((Action<ImagemAnuncioDto>)RemoverImagem, (Func<ImagemAnuncioDto, bool>)PodeRemoverImagem, (bool?)null);
		}

		private bool PodeRemoverImagem(ImagemAnuncioDto arg)
		{
			if (HelperObject.IsNotNull((object)arg))
			{
				return Imagens.Existe((Expression<Func<ImagemAnuncioDto, bool>>)((ImagemAnuncioDto x) => x.Url.Equals(arg.Url)));
			}
			return false;
		}

		private void RemoverImagem(ImagemAnuncioDto obj)
		{
			Imagens.Remover(obj);
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreImagensViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private bool PodeAdicionarImagem(string arg)
		{
			if (HelperObject.IsNotNull((object)arg) && !string.IsNullOrEmpty(UrlImagem))
			{
				return !Imagens.Existe((Expression<Func<ImagemAnuncioDto, bool>>)((ImagemAnuncioDto x) => x.Url.Equals(arg.Trim())));
			}
			return false;
		}

		private void AdicionarImagem(string obj)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			Imagens.Adicionar(new ImagemAnuncioDto
			{
				Url = obj.Trim()
			});
			UrlImagem = string.Empty;
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreImagensViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsViewModel()
		{
			Imagens.Adicionar(base.Dto.Imagens);
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreImagensViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Imagens = Imagens.Itens;
				base.AtualizarDto<AnuncioDto>(ref val);
			}
		}
	}
	public class AnuncioMercadoLivreTabelaMedidasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioMercadoLivreTabelaMedidasAbaViewModel(IViewModelDtoBase viewModel)
			: base("Tabela de medidas", "Criar no Mercado Livre", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreTabelaMedidasViewModel : ViewModelDtoBase<AnuncioDto>
	{
		public AnuncioMercadoLivreVariacaoViewModel Variacao { get; private set; }

		public AnuncioMercadoLivreTabelaMedidasViewModel(AnuncioDto dto, AnuncioMercadoLivreVariacaoViewModel variacaoViewModel)
			: base(dto)
		{
			Variacao = variacaoViewModel;
			((ViewModelValidationBase)this).Valido = ((Validation)this).Valid;
		}

		public override void ProjectedAsViewModel()
		{
			((ViewModelValidationBase)this).Valido = ((Validation)this).Valid;
		}
	}
	public class AnuncioMercadoLivreVariacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioMercadoLivreVariacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Variações", "Grade", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreVariacaoViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly AnuncioMercadoLivreViewModel _anuncioViewModel;

		private readonly AnuncioMercadoLivreCategoriaViewModel _categoriaViewModel;

		private readonly MercadoLivreApi _api;

		private string _categoriaId = string.Empty;

		private string _chaveTabelasMedida = string.Empty;

		private IEnumerable<Attribute> _atributosCategoria = Enumerable.Empty<Attribute>();

		private bool _projetando;

		private bool _exigeTabelaMedidas;

		private string _dominio = string.Empty;

		private FichaTabelaMedidas _ficha = new FichaTabelaMedidas();

		private int _cargaCategoria;

		private decimal _precoCadastro;

		private const string PendenciaTabelaMedidas = "Esta categoria exige tabela de medidas. Selecione a tabela no topo desta aba.";

		private static readonly string[] FiltrosObrigatorios = new string[2] { "GENDER", "BRAND" };

		public Lista<VariacaoMercadoLivreViewModel> Variacoes { get; protected set; } = new Lista<VariacaoMercadoLivreViewModel>();


		public Lista<ImagemAnuncioDto> ImagensAnuncio { get; private set; }

		public Lista<TamanhoTabelaMedidasViewModel> TamanhosTabela { get; set; } = new Lista<TamanhoTabelaMedidasViewModel>();


		public Lista<Attribute> AtributosLinha { get; set; } = new Lista<Attribute>();


		public Lista<Attribute> AtributosColuna { get; set; } = new Lista<Attribute>();


		public List<ValorVariacaoViewModel> ValoresLinha
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<ValorVariacaoViewModel>>((Expression<Func<List<ValorVariacaoViewModel>>>)(() => ValoresLinha));
			}
			set
			{
				((BindableBase)this).SetProperty<List<ValorVariacaoViewModel>>((Expression<Func<List<ValorVariacaoViewModel>>>)(() => ValoresLinha), value);
			}
		}

		public List<ValorVariacaoViewModel> ValoresColuna
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<ValorVariacaoViewModel>>((Expression<Func<List<ValorVariacaoViewModel>>>)(() => ValoresColuna));
			}
			set
			{
				((BindableBase)this).SetProperty<List<ValorVariacaoViewModel>>((Expression<Func<List<ValorVariacaoViewModel>>>)(() => ValoresColuna), value);
			}
		}

		public bool GradeSimples
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GradeSimples));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GradeSimples), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertiesChanged<Visibility, int, GridLength, GridLength>((Expression<Func<Visibility>>)(() => ColunaVisibility), System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<GridLength>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<GridLength>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
				});
			}
		}

		public Visibility ColunaVisibility
		{
			get
			{
				if (!GradeSimples)
				{
					return Visibility.Visible;
				}
				return Visibility.Collapsed;
			}
		}

		public GridLength ColunaWidth
		{
			get
			{
				if (!GradeSimples)
				{
					return new GridLength(0.7, GridUnitType.Star);
				}
				return new GridLength(0.0);
			}
		}

		public GridLength LinhaMapeamentoWidth
		{
			get
			{
				if (!ExigeMapeamento(ValoresLinha))
				{
					return new GridLength(0.0);
				}
				return new GridLength(1.5, GridUnitType.Star);
			}
		}

		public GridLength ColunaMapeamentoWidth
		{
			get
			{
				if (!GradeSimples && ExigeMapeamento(ValoresColuna))
				{
					return new GridLength(1.3, GridUnitType.Star);
				}
				return new GridLength(0.0);
			}
		}

		public int LinhaColumnSpan
		{
			get
			{
				if (!GradeSimples)
				{
					return 1;
				}
				return 3;
			}
		}

		public string MensagemCategoria
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MensagemCategoria));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MensagemCategoria), value);
			}
		}

		public ICommand CriarTabelaMedidasCommand { get; set; }

		public bool PrecisaCriarTabelaMedidas
		{
			get
			{
				if (_exigeTabelaMedidas && !HelperString.IsNullOrEmpty(_dominio) && !TabelasMedida.Itens.Any())
				{
					return TamanhosTabela.Itens.Any();
				}
				return false;
			}
		}

		public string RotuloEquivalenciaTabela
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => RotuloEquivalenciaTabela));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => RotuloEquivalenciaTabela), value);
			}
		}

		public string RotuloMedidaTabela
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => RotuloMedidaTabela));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => RotuloMedidaTabela), value);
			}
		}

		public GridLength TabelaMedidasColunaWidth
		{
			get
			{
				if (!_exigeTabelaMedidas)
				{
					return new GridLength(0.0);
				}
				return new GridLength(1.3, GridUnitType.Star);
			}
		}

		public GridLength EquivalenciaTabelaWidth
		{
			get
			{
				if (!HelperString.IsNullOrEmpty(RotuloEquivalenciaTabela))
				{
					return new GridLength(2.4, GridUnitType.Star);
				}
				return new GridLength(0.0);
			}
		}

		public GridLength MedidaTabelaWidth
		{
			get
			{
				if (!HelperString.IsNullOrEmpty(RotuloMedidaTabela))
				{
					return new GridLength(2.4, GridUnitType.Star);
				}
				return new GridLength(0.0);
			}
		}

		public Lista<Chart> TabelasMedida { get; set; } = new Lista<Chart>();


		public List<AtributoOpcaoDto> LinhasTabela
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<AtributoOpcaoDto>>((Expression<Func<List<AtributoOpcaoDto>>>)(() => LinhasTabela));
			}
			set
			{
				((BindableBase)this).SetProperty<List<AtributoOpcaoDto>>((Expression<Func<List<AtributoOpcaoDto>>>)(() => LinhasTabela), value);
			}
		}

		public string DiagnosticoTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DiagnosticoTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DiagnosticoTabelaMedidas), value);
			}
		}

		public Visibility TabelaMedidasVisibility
		{
			get
			{
				if (!TabelasMedida.Itens.Any())
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public string PendenciasTexto => string.Join(Environment.NewLine, ((Validation)this).Pendencies.Where((string x) => x != "Esta categoria exige tabela de medidas. Selecione a tabela no topo desta aba."));

		public string NomeAtributoLinha
		{
			get
			{
				if (AtributosLinha.ItemSelecionado != null)
				{
					return $"{AtributosLinha.ItemSelecionado.name} no Mercado Livre";
				}
				return "Valor no Mercado Livre";
			}
		}

		public string NomeAtributoColuna
		{
			get
			{
				if (AtributosColuna.ItemSelecionado != null)
				{
					return $"{AtributosColuna.ItemSelecionado.name} no Mercado Livre";
				}
				return "Valor no Mercado Livre";
			}
		}

		public override bool Valid
		{
			get
			{
				if (!Variacoes.Itens.Any())
				{
					return true;
				}
				if (AtributosLinha.ItemSelecionado == null)
				{
					return false;
				}
				if (!GradeSimples && AtributosColuna.ItemSelecionado == null)
				{
					return false;
				}
				if (!GradeSimples && AtributosLinha.ItemSelecionado.id == AtributosColuna.ItemSelecionado.id)
				{
					return false;
				}
				if (Variacoes.Itens.Count > 100)
				{
					return false;
				}
				if (Variacoes.Itens.Any((VariacaoMercadoLivreViewModel x) => !x.QuantidadeValida))
				{
					return false;
				}
				if (Variacoes.Itens.Any((VariacaoMercadoLivreViewModel x) => !x.PrecoValido))
				{
					return false;
				}
				if (_exigeTabelaMedidas && TabelasMedida.ItemSelecionado == null)
				{
					return false;
				}
				if (!HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresLinha).All((ValorVariacaoViewModel x) => x.Resolvido))
				{
					return false;
				}
				if (!GradeSimples)
				{
					return HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresColuna).All((ValorVariacaoViewModel x) => x.Resolvido);
				}
				return true;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				List<string> list = new List<string>();
				if (!Variacoes.Itens.Any())
				{
					return list.ToArray();
				}
				if (!AtributosLinha.Itens.Any())
				{
					list.Add("A categoria selecionada não aceita variações no Mercado Livre. Escolha outra categoria na aba Categoria ou publique este produto sem grade.");
					return list.ToArray();
				}
				if (AtributosLinha.ItemSelecionado == null)
				{
					list.Add("Selecione o atributo do Mercado Livre que corresponde à linha da grade.");
				}
				if (!GradeSimples && AtributosColuna.ItemSelecionado == null)
				{
					list.Add("Selecione o atributo do Mercado Livre que corresponde à coluna da grade.");
				}
				if (!GradeSimples && AtributosLinha.ItemSelecionado != null && AtributosColuna.ItemSelecionado != null && AtributosLinha.ItemSelecionado.id == AtributosColuna.ItemSelecionado.id)
				{
					list.Add("Linha e coluna não podem usar o mesmo atributo do Mercado Livre.");
				}
				if (Variacoes.Itens.Count > 100)
				{
					list.Add($"O Mercado Livre permite no máximo {100} variações por anúncio.");
				}
				List<string> list2 = (from x in Variacoes.Itens
					where !x.QuantidadeValida
					select x.Nome).ToList();
				if (list2.Any())
				{
					list.Add(string.Format("O Mercado Livre não aceita variação com estoque zerado. Informe a quantidade de: {0}.", string.Join(", ", list2)));
				}
				List<string> list3 = (from x in Variacoes.Itens
					where !x.PrecoValido
					select x.Nome).ToList();
				if (list3.Any())
				{
					list.Add(string.Format("Informe o preço de: {0}.", string.Join(", ", list3)));
				}
				if (_exigeTabelaMedidas && TabelasMedida.ItemSelecionado == null)
				{
					list.Add("Esta categoria exige tabela de medidas. Selecione a tabela no topo desta aba.");
				}
				if (TabelasMedida.Itens.Any() && TabelasMedida.ItemSelecionado != null && Variacoes.Itens.Any((VariacaoMercadoLivreViewModel x) => HelperString.IsNullOrEmpty(x.LinhaTabelaMedidasId)))
				{
					list.Add("Relacione todas as variações com uma linha da tabela de medidas.");
				}
				if (!HelperString.IsNullOrEmpty(DiagnosticoTabelaMedidas))
				{
					list.Add(DiagnosticoTabelaMedidas);
				}
				List<string> list4 = (from x in HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresLinha)
					where !x.Resolvido
					select x.ValorGrade).ToList();
				if (!GradeSimples)
				{
					list4.AddRange(from x in HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresColuna)
						where !x.Resolvido
						select x.ValorGrade);
				}
				if (list4.Any())
				{
					list.Add(string.Format("Relacione com o Mercado Livre: {0}.", string.Join(", ", list4.Distinct())));
				}
				return list.ToArray();
			}
		}

		public string ErrorMessage
		{
			get
			{
				if (HelperList.DistinctBy<VariacaoMercadoLivreViewModel, decimal>((IEnumerable<VariacaoMercadoLivreViewModel>)Variacoes.Itens, (Func<VariacaoMercadoLivreViewModel, decimal>)((VariacaoMercadoLivreViewModel x) => x.Preco)).Count() > 1)
				{
					return "O Mercado Livre exibe e cobra o maior preço quando as variações têm preços diferentes, a não ser que o vendedor esteja habilitado em Preço por Variação.";
				}
				return string.Empty;
			}
		}

		public event EventHandler<bool> AlterouNecessidadeTabelaMedidasEvent;

		public AnuncioMercadoLivreVariacaoViewModel(AnuncioDto dto, AnuncioMercadoLivreViewModel anuncioViewModel, AnuncioMercadoLivreImagensViewModel imagensViewModel, AnuncioMercadoLivreCategoriaViewModel categoriaViewModel)
			: base(dto)
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			ImagensAnuncio = imagensViewModel.Imagens;
			_anuncioViewModel = anuncioViewModel;
			_categoriaViewModel = categoriaViewModel;
			_anuncioViewModel.AlterouProdutoEvent += AlterouProdutoEvent;
			((BindableBase)_anuncioViewModel).PropertyChanged += AnuncioAlterado;
			_api = new MercadoLivreApi(DadosSistema.ObterProxy());
			((MercadoLivreBaseApi)_api).SetarToken(DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).TokenAcesso);
			CriarTabelaMedidasCommand = (ICommand)new AsyncCommand((Func<Task>)CriarTabelaMedidas, (Func<bool>)PodeCriarTabelaMedidas, false, (bool?)null);
			ObservableExtensions.Subscribe<Attribute>(AtributosLinha.WhenAlterouItemSelecionado, (Action<Attribute>)delegate(Attribute item)
			{
				AtributoLinhaSelecionado(item);
			});
			ObservableExtensions.Subscribe<Attribute>(AtributosColuna.WhenAlterouItemSelecionado, (Action<Attribute>)delegate(Attribute item)
			{
				AtributoColunaSelecionado(item);
			});
			ObservableExtensions.Subscribe<Chart>(TabelasMedida.WhenAlterouItemSelecionado, (Action<Chart>)TabelaMedidaSelecionada);
			CriarLinhasColunas(_anuncioViewModel.Estoque);
		}

		private static bool ExigeMapeamento(IEnumerable<ValorVariacaoViewModel> valores)
		{
			return HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>(valores).Any((ValorVariacaoViewModel x) => !x.ListaLivre);
		}

		private bool PodeCriarTabelaMedidas()
		{
			if (_exigeTabelaMedidas && !HelperString.IsNullOrEmpty(_dominio))
			{
				return Variacoes.Itens.Any();
			}
			return false;
		}

		public async void AlterouCategoria(string categoriaId)
		{
			_categoriaId = HelperString.ToStringEmptyWhenNull(categoriaId);
			await CarregarAtributosCategoria(categoriaId, ++_cargaCategoria);
		}

		public async void RevisarTabelasMedida()
		{
			try
			{
				if (_exigeTabelaMedidas && !(ChaveTabelasMedida() == _chaveTabelasMedida))
				{
					await CarregarTabelasMedida(_cargaCategoria);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private string ChaveTabelasMedida()
		{
			IEnumerable<string> second = from x in ObterFiltrosTabelaMedidas()
				select $"{x.id}={x.value_id}/{x.value_name}";
			return string.Join("|", new string[2]
			{
				_categoriaId,
				HelperString.ToStringEmptyWhenNull(_anuncioViewModel.Titulo)
			}.Concat(second));
		}

		private bool CargaSuperada(int carga)
		{
			return carga != _cargaCategoria;
		}

		private void AlterouProdutoEvent(object sender, EstoqueDto estoque)
		{
			CriarLinhasColunas(estoque);
			((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private void CriarLinhasColunas(EstoqueDto estoque)
		{
			List<VariacaoMercadoLivreViewModel> list = Variacoes.Itens.ToList();
			list.ForEach(delegate(VariacaoMercadoLivreViewModel x)
			{
				x.Alterou -= VariacaoAlterada;
			});
			Variacoes.Limpar();
			ProdutoServicoEmpresaDto val = ObterProdutoAtualizado();
			_precoCadastro = ((val == null) ? _anuncioViewModel.Preco : val.Preco.PrecoVendaVigente);
			EstoqueGradeDto val2 = (EstoqueGradeDto)(((object)((val == null) ? null : /*isinst with value type is only supported in some contexts*/)) ?? ((object)((estoque is EstoqueGradeDto) ? estoque : null)));
			if (val2 == null || !HelperIEnumerable.ToListEmptyWhenNull<GradeDto>(val2.Grades).Any())
			{
				GradeSimples = false;
				AtualizarValoresDePara();
				return;
			}
			int num = val2.Grades.Select((GradeDto g) => g.Coluna.Descricao).Distinct().Count();
			bool flag = num > 1 && val2.Grades.Select((GradeDto g) => g.Linha.Descricao).Distinct().Count() == 1;
			GradeSimples = num == 1 || flag;
			foreach (GradeDto grade in val2.Grades)
			{
				string linha = (flag ? grade.Coluna.Descricao : grade.Linha.Descricao);
				string coluna = (flag ? string.Empty : grade.Coluna.Descricao);
				VariacaoMercadoLivreViewModel variacaoMercadoLivreViewModel = ObterVariacaoAnterior(list, ((EntityBaseDto)grade.Estoque).Id, linha, coluna);
				Variacoes.Adicionar(new VariacaoMercadoLivreViewModel
				{
					Id = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.Id),
					Sku = ((EntityBaseDto)grade.Estoque).Id,
					Nome = (HelperString.IsNullOrEmpty(grade.Coluna.Descricao) ? grade.Linha.Descricao : $"{grade.Linha.Descricao} / {grade.Coluna.Descricao}"),
					CodigoBarras = grade.CodigoBarras,
					Preco = ((variacaoMercadoLivreViewModel != null && variacaoMercadoLivreViewModel.Preco > 0m) ? variacaoMercadoLivreViewModel.Preco : PrecoInicialVariacao()),
					QuantidadeDisponivel = grade.Estoque.Quantidade,
					Linha = linha,
					Coluna = coluna,
					IdImagem = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.IdImagem),
					UrlImagem = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.UrlImagem),
					IdVariacaoMercadoLivre = (variacaoMercadoLivreViewModel?.IdVariacaoMercadoLivre ?? 0),
					IdItemMercadoLivre = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.IdItemMercadoLivre),
					Permalink = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.Permalink),
					LinhaTabelaMedidasId = ((variacaoMercadoLivreViewModel == null) ? string.Empty : variacaoMercadoLivreViewModel.LinhaTabelaMedidasId)
				});
			}
			LinqExtensions.ForEach<VariacaoMercadoLivreViewModel>((IEnumerable<VariacaoMercadoLivreViewModel>)Variacoes.Itens, (Action<VariacaoMercadoLivreViewModel>)delegate(VariacaoMercadoLivreViewModel x)
			{
				x.Alterou += VariacaoAlterada;
			});
			AtualizarValoresDePara();
			((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies), (Expression<Func<string>>)(() => PendenciasTexto));
		}

		private ProdutoServicoEmpresaDto ObterProdutoAtualizado()
		{
			string referencia = HelperString.ToStringEmptyWhenNull(_anuncioViewModel.ProdutoServicoEmpresaReferencia);
			if (HelperString.IsNullOrEmpty(referencia))
			{
				return null;
			}
			try
			{
				return HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { referencia })))).FirstOrDefault();
			}
			catch (Exception)
			{
				return null;
			}
		}

		private static VariacaoMercadoLivreViewModel ObterVariacaoAnterior(List<VariacaoMercadoLivreViewModel> anteriores, string sku, string linha, string coluna)
		{
			VariacaoMercadoLivreViewModel variacaoMercadoLivreViewModel = anteriores.FirstOrDefault((VariacaoMercadoLivreViewModel x) => !HelperString.IsNullOrEmpty(x.Sku) && x.Sku.Equals(sku));
			if (variacaoMercadoLivreViewModel != null)
			{
				return variacaoMercadoLivreViewModel;
			}
			return anteriores.FirstOrDefault((VariacaoMercadoLivreViewModel x) => Normalizar(x.Linha).Equals(Normalizar(linha)) && Normalizar(x.Coluna).Equals(Normalizar(coluna)));
		}

		private decimal PrecoInicialVariacao()
		{
			if (!_anuncioViewModel.UtilizarInformacaoCadastroSincronismo)
			{
				return 0m;
			}
			return _precoCadastro;
		}

		private void AnuncioAlterado(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "UtilizarInformacaoCadastroSincronismo" || !_anuncioViewModel.UtilizarInformacaoCadastroSincronismo)
			{
				return;
			}
			decimal preco = ((_precoCadastro > 0m) ? _precoCadastro : _anuncioViewModel.Preco);
			foreach (VariacaoMercadoLivreViewModel iten in Variacoes.Itens)
			{
				iten.Preco = preco;
			}
		}

		private static string DescricaoFiltro(string id)
		{
			if (!(id == "GENDER"))
			{
				return "Marca";
			}
			return "Gênero";
		}

		private List<ChartAttribute> ObterFiltrosTabelaMedidas()
		{
			return (from atributo in HelperIEnumerable.ToListEmptyWhenNull<AtributoViewModel>((IEnumerable<AtributoViewModel>)_categoriaViewModel.Atributos.Itens)
				where FiltrosObrigatorios.Contains(HelperString.ToStringEmptyWhenNull(atributo.ValueId).ToUpperInvariant())
				select new
				{
					ValueId = atributo.ValueId,
					Titulo = atributo.Titulo,
					Valor = ValorFiltro(atributo)
				} into atributo
				where !HelperString.IsNullOrEmpty(atributo.Valor.Valor) || !HelperString.IsNullOrEmpty(atributo.Valor.Id)
				select atributo).Select(atributo =>
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Expected O, but got Unknown
				//IL_005d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0062: Unknown result type (might be due to invalid IL or missing references)
				//IL_0078: Unknown result type (might be due to invalid IL or missing references)
				//IL_008f: Expected O, but got Unknown
				ChartAttribute val = new ChartAttribute();
				val.id = atributo.ValueId.ToUpperInvariant();
				val.name = HelperString.IsEmptyUseNull(atributo.Titulo);
				val.value_id = HelperString.IsEmptyUseNull(atributo.Valor.Id);
				val.value_name = HelperString.IsEmptyUseNull(atributo.Valor.Valor);
				val.values = (IEnumerable<ChartAttributeValue>)(object)new ChartAttributeValue[1]
				{
					new ChartAttributeValue
					{
						id = HelperString.IsEmptyUseNull(atributo.Valor.Id),
						name = HelperString.IsEmptyUseNull(atributo.Valor.Valor)
					}
				};
				return val;
			}).ToList();
		}

		private static AtributoOpcaoDto ValorFiltro(AtributoViewModel atributo)
		{
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			AtributoListaViewModel atributoListaViewModel = atributo as AtributoListaViewModel;
			AtributoTextoViewModel atributoTextoViewModel = atributo as AtributoTextoViewModel;
			string id = ((atributoListaViewModel != null && atributoListaViewModel.Selecionado != null) ? HelperString.ToStringEmptyWhenNull(atributoListaViewModel.Selecionado.Id) : string.Empty);
			string valor = ((atributoListaViewModel != null && atributoListaViewModel.Selecionado != null) ? HelperString.ToStringEmptyWhenNull(atributoListaViewModel.Selecionado.Valor) : ((atributoTextoViewModel != null) ? HelperString.ToStringEmptyWhenNull(atributoTextoViewModel.Valor) : string.Empty));
			return new AtributoOpcaoDto
			{
				Id = id,
				Valor = valor
			};
		}

		private async Task CriarTabelaMedidas()
		{
			_ = 1;
			try
			{
				List<ChartAttribute> filtros = ObterFiltrosTabelaMedidas();
				List<string> source = FiltrosObrigatorios.Where((string x) => filtros.All((ChartAttribute f) => f.id != x)).ToList();
				if (source.Any())
				{
					DialogoFactory.MostrarDialogoConfirma(string.Format("Informe {0} na aba Categoria antes de criar a tabela de medidas.", string.Join(" e ", source.Select(DescricaoFiltro))), false, HorizontalAlignment.Center);
					return;
				}
				string principal = _ficha.AtributoPrincipal;
				if (_ficha.Vazia)
				{
					DialogoFactory.MostrarDialogoConfirma("Não foi possível identificar o atributo de tamanho na ficha técnica do Mercado Livre para este domínio." + Environment.NewLine + Environment.NewLine + "Retorno do Mercado Livre:" + Environment.NewLine + HelperString.CopiarTexto(_api.UltimaFichaTabelaMedidas, 1500), false, HorizontalAlignment.Left);
					return;
				}
				List<AtributoFichaTabela> source2 = MedidasObrigatorias().Concat(EquivalenciasObrigatorias()).ToList();
				if (MedidasObrigatorias().Count > 1 || EquivalenciasObrigatorias().Count > 1)
				{
					DialogoFactory.MostrarDialogoConfirma("O Mercado Livre pede mais de uma informação por tamanho para criar a tabela deste domínio:" + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, source2.Select((AtributoFichaTabela x) => $"- {x.Nome}")) + Environment.NewLine + Environment.NewLine + "Crie a tabela pelo site do Mercado Livre e depois selecione ela aqui na lista.", false, HorizontalAlignment.Left);
					return;
				}
				if (!TamanhosTabela.Itens.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum tamanho da grade para montar a tabela de medidas.", false, HorizontalAlignment.Center);
					return;
				}
				List<string> list = (from x in TamanhosTabela.Itens
					where !x.Valido
					select x.Tamanho).ToList();
				if (list.Any())
				{
					DialogoFactory.MostrarDialogoConfirma(string.Format("Informe os dados da tabela de medidas de: {0}.", string.Join(", ", list)), false, HorizontalAlignment.Center);
					return;
				}
				List<string> source3 = MedidasRepetidas();
				if (source3.Any())
				{
					string arg = (HelperString.IsNullOrEmpty(RotuloMedidaTabela) ? "a medida" : $"'{RotuloMedidaTabela}'");
					DialogoFactory.MostrarDialogoConfirma($"O Mercado Livre não aceita {arg} repetida em mais de um tamanho da tabela:" + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, source3.Select((string x) => $"- {x}")) + Environment.NewLine + Environment.NewLine + "Informe um valor diferente para cada tamanho.", false, HorizontalAlignment.Left);
					return;
				}
				string text = MontarNomeTabela(filtros);
				if (DialogoFactory.MostrarDialogoSimNao($"Será criada no Mercado Livre a tabela de medidas:{Environment.NewLine}{Environment.NewLine}" + $"Nome: {text}{Environment.NewLine}" + $"Domínio: {_dominio}{Environment.NewLine}" + $"{string.Join(Environment.NewLine, filtros.Select((ChartAttribute x) => $"{DescricaoFiltro(x.id)}: {x.values.First().name}"))}{Environment.NewLine}" + $"Tamanho principal: {principal}{Environment.NewLine}" + $"Linhas:{Environment.NewLine}{string.Join(Environment.NewLine, TamanhosTabela.Itens.Select(DescricaoLinhaTabela))}{Environment.NewLine}{Environment.NewLine}" + "O Mercado Livre não permite excluir tabela de medidas, e depois de criada só o nome pode ser alterado. Confirma?", false, 0, HorizontalAlignment.Left) != MessageBoxResult.Yes)
				{
					return;
				}
				ChartCreateRequest val = new ChartCreateRequest
				{
					names = new Dictionary<string, string> { { "MLB", text } },
					domain_id = _dominio,
					site_id = "MLB",
					measure_type = HelperString.IsEmptyUseNull(_ficha.TipoMedida),
					main_attribute = new ChartMainAttribute
					{
						attribute = new ChartMainAttributeItem
						{
							id = principal
						},
						site_id = "MLB"
					},
					attributes = filtros,
					rows = ((IEnumerable<TamanhoTabelaMedidasViewModel>)TamanhosTabela.Itens).Select((Func<TamanhoTabelaMedidasViewModel, ChartRowRequest>)((TamanhoTabelaMedidasViewModel tamanho) => new ChartRowRequest
					{
						attributes = MontarAtributosLinha(principal, tamanho)
					})).ToList()
				};
				Chart criada = default(Chart);
				_ = criada;
				criada = await _api.CriarTabelaMedidaAsync(val);
				if (criada == null || HelperString.IsNullOrEmpty(criada.id))
				{
					DialogoFactory.MostrarDialogoConfirma("O Mercado Livre não retornou o identificador da tabela criada.", false, HorizontalAlignment.Center);
					return;
				}
				((ViewModelDtoBase<AnuncioDto>)this).Dto.TabelaMedidasId = criada.id;
				await CarregarTabelasMedida(_cargaCategoria);
				if (TabelasMedida.Itens.All((Chart x) => x.id != criada.id))
				{
					TabelasMedida.AdicionarSemPosicionar((IEnumerable<Chart>)(object)new Chart[1] { criada });
					DiagnosticoTabelaMedidas = string.Empty;
					NotificarTabelaMedidas();
				}
				TabelasMedida.Posicionar((Expression<Func<Chart, bool>>)((Chart x) => x.id == criada.id));
			}
			catch (TabelaMedidasInvalidaException val2)
			{
				DialogoFactory.MostrarDialogoConfirma(((Exception)val2).Message, false, HorizontalAlignment.Left);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private List<string> MedidasRepetidas()
		{
			return (from x in TamanhosTabela.Itens
				where x.ExigeMedida
				group x by x.Medida into x
				where x.Count() > 1
				select string.Format("{0}: {1}", x.Key.ToString(CultureInfo.InvariantCulture), string.Join(", ", x.Select((TamanhoTabelaMedidasViewModel tamanho) => tamanho.Tamanho)))).ToList();
		}

		private List<ChartAttribute> MontarAtributosLinha(string principal, TamanhoTabelaMedidasViewModel tamanho)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Expected O, but got Unknown
			List<ChartAttribute> list = new List<ChartAttribute>();
			ChartAttribute val = new ChartAttribute();
			val.id = principal;
			val.values = (IEnumerable<ChartAttributeValue>)(object)new ChartAttributeValue[1]
			{
				new ChartAttributeValue
				{
					name = tamanho.Tamanho
				}
			};
			list.Add(val);
			List<ChartAttribute> list2 = list;
			foreach (AtributoFichaTabela item in EquivalenciasObrigatorias())
			{
				val = new ChartAttribute();
				val.id = item.Id;
				val.values = (IEnumerable<ChartAttributeValue>)(object)new ChartAttributeValue[1]
				{
					new ChartAttributeValue
					{
						id = HelperString.IsEmptyUseNull(tamanho.Equivalencia.Id),
						name = tamanho.Equivalencia.Valor
					}
				};
				list2.Add(val);
			}
			foreach (AtributoFichaTabela item2 in MedidasObrigatorias())
			{
				val = new ChartAttribute();
				val.id = item2.Id;
				val.values = (IEnumerable<ChartAttributeValue>)(object)new ChartAttributeValue[1] { ValorMedida(tamanho.Medida, item2.UnidadePadrao) };
				list2.Add(val);
			}
			return list2;
		}

		private static ChartAttributeValue ValorMedida(decimal medida, string unidade)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			//IL_0046: Expected O, but got Unknown
			return new ChartAttributeValue
			{
				name = $"{medida.ToString(CultureInfo.InvariantCulture)} {unidade}".Trim(),
				Estrutura = new ChartAttributeStruct
				{
					number = medida,
					unit = HelperString.IsEmptyUseNull(unidade)
				}
			};
		}

		private List<AtributoFichaTabela> MedidasObrigatorias()
		{
			return (from x in _ficha.AtributosLinha
				where x.Obrigatorio && x.Numerico
				where HelperString.IsNullOrEmpty(x.Familia) || x.Familia.Equals(_ficha.TipoMedida)
				select x).ToList();
		}

		private List<AtributoFichaTabela> EquivalenciasObrigatorias()
		{
			return _ficha.AtributosLinha.Where((AtributoFichaTabela x) => x.Obrigatorio && x.Lista).ToList();
		}

		private string DescricaoLinhaTabela(TamanhoTabelaMedidasViewModel tamanho)
		{
			List<string> list = new List<string> { tamanho.Tamanho };
			if (tamanho.Equivalencia != null && !HelperString.IsNullOrEmpty(tamanho.Equivalencia.Valor))
			{
				list.Add(tamanho.Equivalencia.Valor);
			}
			AtributoFichaTabela val = MedidasObrigatorias().FirstOrDefault();
			if (val != null)
			{
				list.Add($"{tamanho.Medida.ToString(CultureInfo.InvariantCulture)} {val.UnidadePadrao}".Trim());
			}
			return string.Format("- {0}", string.Join(" / ", list));
		}

		private List<string> ObterTamanhosGrade()
		{
			if (GradeSimples)
			{
				return (from x in Variacoes.Itens
					select x.Linha into x
					where !HelperString.IsNullOrEmpty(x)
					select x).Distinct().ToList();
			}
			return (from x in Variacoes.Itens.Select((VariacaoMercadoLivreViewModel x) => x.Linha).Concat(Variacoes.Itens.Select((VariacaoMercadoLivreViewModel x) => x.Coluna))
				where !HelperString.IsNullOrEmpty(x)
				select x).Distinct().ToList();
		}

		private void MontarTamanhosTabela()
		{
			List<TamanhoTabelaMedidasViewModel> anteriores = TamanhosTabela.Itens.ToList();
			TamanhosTabela.Limpar();
			AtributoFichaTabela equivalencia = EquivalenciasObrigatorias().FirstOrDefault();
			AtributoFichaTabela medida = MedidasObrigatorias().FirstOrDefault();
			RotuloEquivalenciaTabela = ((equivalencia == null) ? string.Empty : equivalencia.Nome);
			RotuloMedidaTabela = ((medida == null) ? string.Empty : (HelperString.IsNullOrEmpty(medida.UnidadePadrao) ? medida.Nome : $"{medida.Nome} ({medida.UnidadePadrao})"));
			((BindableBase)this).RaisePropertiesChanged<GridLength, GridLength>((Expression<Func<GridLength>>)(() => EquivalenciaTabelaWidth), (Expression<Func<GridLength>>)(() => MedidaTabelaWidth));
			List<AtributoOpcaoDto> opcoes = ((equivalencia == null) ? new List<AtributoOpcaoDto>() : ((IEnumerable<ChartAttributeValue>)equivalencia.Valores).Select((Func<ChartAttributeValue, AtributoOpcaoDto>)((ChartAttributeValue x) => new AtributoOpcaoDto
			{
				Id = HelperString.ToStringEmptyWhenNull(x.id),
				Valor = x.name
			})).ToList());
			List<TamanhoTabelaMedidasViewModel> list = ObterTamanhosGrade().Select(delegate(string tamanho)
			{
				TamanhoTabelaMedidasViewModel tamanhoTabelaMedidasViewModel = anteriores.FirstOrDefault((TamanhoTabelaMedidasViewModel x) => x.Tamanho == tamanho);
				return new TamanhoTabelaMedidasViewModel
				{
					Tamanho = tamanho,
					Equivalencias = opcoes,
					ExigeEquivalencia = (equivalencia != null),
					ExigeMedida = (medida != null),
					Equivalencia = ((tamanhoTabelaMedidasViewModel == null) ? opcoes.FirstOrDefault((AtributoOpcaoDto x) => Normalizar(x.Valor) == Normalizar(tamanho)) : tamanhoTabelaMedidasViewModel.Equivalencia),
					Medida = (tamanhoTabelaMedidasViewModel?.Medida ?? 0m)
				};
			}).ToList();
			TamanhosTabela.AdicionarSemPosicionar((IEnumerable<TamanhoTabelaMedidasViewModel>)list);
		}

		private string MontarNomeTabela(IEnumerable<ChartAttribute> filtros)
		{
			List<string> list = (from x in filtros
				select x.values.First().name into x
				where !HelperString.IsNullOrEmpty(x)
				select x).ToList();
			list.Add(_dominio.Replace("_", " "));
			return HelperString.CopiarTexto(string.Join(" ", list), 60).Trim();
		}

		private void LimparTabelasMedida()
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			TabelasMedida.Limpar();
			TamanhosTabela.Limpar();
			LinhasTabela = new List<AtributoOpcaoDto>();
			_ficha = new FichaTabelaMedidas();
			_chaveTabelasMedida = string.Empty;
			DiagnosticoTabelaMedidas = string.Empty;
			NotificarTabelaMedidas();
		}

		private void NotificarTabelaMedidas()
		{
			((BindableBase)this).RaisePropertiesChanged<Visibility, bool, GridLength>((Expression<Func<Visibility>>)(() => TabelaMedidasVisibility), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<GridLength>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			this.AlterouNecessidadeTabelaMedidasEvent?.Invoke(this, PrecisaCriarTabelaMedidas);
		}

		private async Task CarregarTabelasMedida(int carga)
		{
			try
			{
				TabelasMedida.Limpar();
				LinhasTabela = new List<AtributoOpcaoDto>();
				_chaveTabelasMedida = ChaveTabelasMedida();
				DiagnosticoTabelaMedidas = string.Empty;
				long sellerId = DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).IdUsuario;
				if (sellerId <= 0)
				{
					DiagnosticoTabelaMedidas = "Não foi possível identificar o vendedor no Mercado Livre para buscar tabelas de medidas.";
					return;
				}
				string dominio2 = await _api.ObterDominioDaCategoriaAsync("MLB", _categoriaId, HelperString.ToStringEmptyWhenNull(_anuncioViewModel.Titulo));
				if (CargaSuperada(carga))
				{
					return;
				}
				_dominio = dominio2;
				string dominio = _dominio;
				if (HelperString.IsNullOrEmpty(dominio))
				{
					DiagnosticoTabelaMedidas = "Não foi possível identificar o domínio do anúncio no Mercado Livre. Confira a categoria na aba Categoria e o título na aba Anúncio.";
					return;
				}
				List<ChartAttribute> filtros = ObterFiltrosTabelaMedidas();
				List<string> source = FiltrosObrigatorios.Where((string x) => filtros.All((ChartAttribute f) => f.id != x)).ToList();
				if (source.Any())
				{
					DiagnosticoTabelaMedidas = string.Format("Informe {0} na aba Categoria para o Mercado Livre listar as tabelas de medidas.", string.Join(" e ", source.Select(DescricaoFiltro)));
					return;
				}
				FichaTabelaMedidas ficha = await _api.ObterFichaTabelaMedidasAsync(dominio, (IEnumerable<ChartAttribute>)filtros);
				if (CargaSuperada(carga))
				{
					return;
				}
				_ficha = ficha;
				MontarTamanhosTabela();
				List<Chart> list = HelperIEnumerable.ToListEmptyWhenNull<Chart>(await _api.BuscarTabelasMedidaAsync(dominio, "MLB", sellerId, (IEnumerable<ChartAttribute>)filtros));
				if (CargaSuperada(carga))
				{
					return;
				}
				TabelasMedida.AdicionarSemPosicionar((IEnumerable<Chart>)list);
				DiagnosticoTabelaMedidas = (list.Any() ? string.Empty : $"Nenhuma tabela de medidas encontrada para o domínio '{dominio}'. Sem ela o Mercado Livre não aceita publicar nesta categoria.");
				if (!HelperString.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)this).Dto.TabelaMedidasId))
				{
					TabelasMedida.Posicionar((Expression<Func<Chart, bool>>)((Chart x) => x.id == ((ViewModelDtoBase<AnuncioDto>)this).Dto.TabelaMedidasId));
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				NotificarTabelaMedidas();
			}
		}

		private async void TabelaMedidaSelecionada(Chart tabela)
		{
			if (_projetando || tabela == null)
			{
				return;
			}
			try
			{
				Chart val = await _api.ObterTabelaMedidaAsync(tabela.id);
				List<ChartRow> source = ((val == null) ? new List<ChartRow>() : HelperIEnumerable.ToListEmptyWhenNull<ChartRow>(val.rows));
				string principal = ((val == null) ? tabela.main_attribute_id : val.main_attribute_id);
				LinhasTabela = (from x in ((IEnumerable<ChartRow>)source).Select((Func<ChartRow, AtributoOpcaoDto>)((ChartRow x) => new AtributoOpcaoDto
					{
						Id = x.id,
						Valor = x.ValorPrincipal(principal)
					}))
					where !HelperString.IsNullOrEmpty(x.Id)
					select x).ToList();
				foreach (VariacaoMercadoLivreViewModel variacao in Variacoes.Itens.Where((VariacaoMercadoLivreViewModel x) => HelperString.IsNullOrEmpty(x.LinhaTabelaMedidasId)))
				{
					AtributoOpcaoDto val2 = LinhasTabela.FirstOrDefault((AtributoOpcaoDto x) => Normalizar(x.Valor) == Normalizar(variacao.Linha)) ?? LinhasTabela.FirstOrDefault((AtributoOpcaoDto x) => Normalizar(x.Valor) == Normalizar(variacao.Coluna));
					if (val2 != null)
					{
						variacao.LinhaTabelaMedidasId = val2.Id;
					}
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}
		}

		private void VariacaoAlterada(object sender, EventArgs e)
		{
			((BindableBase)this).RaisePropertiesChanged<bool, string[], string, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private async Task CarregarAtributosCategoria(string categoriaId, int carga)
		{
			_ = 1;
			try
			{
				if (HelperString.IsNullOrEmpty(categoriaId))
				{
					_atributosCategoria = Enumerable.Empty<Attribute>();
					MensagemCategoria = "Selecione a categoria do anúncio para listar os atributos de variação.";
					return;
				}
				List<Attribute> source = HelperIEnumerable.ToListEmptyWhenNull<Attribute>(await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<Attribute>>($"/categories/{categoriaId}/attributes", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null));
				if (CargaSuperada(carga))
				{
					return;
				}
				_atributosCategoria = source.Where((Attribute x) => x.PermiteVariacao).ToList();
				_exigeTabelaMedidas = source.Any((Attribute x) => x.TabelaMedidas);
				MensagemCategoria = (_atributosCategoria.Any() ? string.Empty : "Esta categoria do Mercado Livre não aceita variações.");
				string linhaSelecionada = ((ViewModelDtoBase<AnuncioDto>)this).Dto.AtributoVariacaoLinhaId;
				string colunaSelecionada = ((ViewModelDtoBase<AnuncioDto>)this).Dto.AtributoVariacaoColunaId;
				AtributosLinha.Limpar();
				AtributosColuna.Limpar();
				AtributosLinha.AdicionarSemPosicionar(_atributosCategoria);
				AtributosColuna.AdicionarSemPosicionar(_atributosCategoria);
				if (!HelperString.IsNullOrEmpty(linhaSelecionada))
				{
					AtributosLinha.Posicionar((Expression<Func<Attribute, bool>>)((Attribute x) => x.id == linhaSelecionada));
				}
				if (!HelperString.IsNullOrEmpty(colunaSelecionada))
				{
					AtributosColuna.Posicionar((Expression<Func<Attribute, bool>>)((Attribute x) => x.id == colunaSelecionada));
				}
				AtualizarValoresDePara();
				if (_exigeTabelaMedidas)
				{
					await CarregarTabelasMedida(carga);
				}
				else
				{
					LimparTabelasMedida();
				}
				((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies), (Expression<Func<string>>)(() => PendenciasTexto));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void AtributoLinhaSelecionado(Attribute atributo)
		{
			if (!_projetando)
			{
				ValoresLinha = CriarValoresDePara(atributo, Variacoes.Itens.Select((VariacaoMercadoLivreViewModel x) => x.Linha), ObterCombinacoesSalvas(atributo));
				VincularMapeamentos();
				((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
				((BindableBase)this).RaisePropertiesChanged<string, string, GridLength>((Expression<Func<string>>)(() => NomeAtributoLinha), (Expression<Func<string>>)(() => NomeAtributoColuna), System.Linq.Expressions.Expression.Lambda<Func<GridLength>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}
		}

		private void VincularMapeamentos()
		{
			foreach (VariacaoMercadoLivreViewModel variacao in Variacoes.Itens)
			{
				variacao.MapeamentoLinha = HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresLinha).FirstOrDefault((ValorVariacaoViewModel x) => x.ValorGrade == variacao.Linha);
				variacao.MapeamentoColuna = HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresColuna).FirstOrDefault((ValorVariacaoViewModel x) => x.ValorGrade == variacao.Coluna);
			}
		}

		private void AtributoColunaSelecionado(Attribute atributo)
		{
			if (!_projetando)
			{
				ValoresColuna = CriarValoresDePara(atributo, Variacoes.Itens.Select((VariacaoMercadoLivreViewModel x) => x.Coluna), ObterCombinacoesSalvas(atributo));
				VincularMapeamentos();
				((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
				((BindableBase)this).RaisePropertiesChanged<string, string, GridLength>((Expression<Func<string>>)(() => NomeAtributoLinha), (Expression<Func<string>>)(() => NomeAtributoColuna), System.Linq.Expressions.Expression.Lambda<Func<GridLength>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}
		}

		private void AtualizarValoresDePara()
		{
			AtributoLinhaSelecionado(AtributosLinha.ItemSelecionado);
			AtributoColunaSelecionado(AtributosColuna.ItemSelecionado);
		}

		private Dictionary<string, CombinacaoAtributoDto> ObterCombinacoesSalvas(Attribute atributo)
		{
			Dictionary<string, CombinacaoAtributoDto> dictionary = new Dictionary<string, CombinacaoAtributoDto>();
			if (atributo == null)
			{
				return dictionary;
			}
			foreach (VariacaoDto item in HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(base.Dto.Variacoes))
			{
				foreach (CombinacaoAtributoDto item2 in from x in HelperIEnumerable.ToListEmptyWhenNull<CombinacaoAtributoDto>(item.Combinacoes)
					where x.AtributoId == atributo.id
					select x)
				{
					string text = ((atributo.id == base.Dto.AtributoVariacaoLinhaId) ? item.Linha : item.Coluna);
					if (!HelperString.IsNullOrEmpty(text) && !dictionary.ContainsKey(text))
					{
						dictionary.Add(text, item2);
					}
				}
			}
			return dictionary;
		}

		private static List<ValorVariacaoViewModel> CriarValoresDePara(Attribute atributo, IEnumerable<string> valoresGrade, Dictionary<string, CombinacaoAtributoDto> salvas)
		{
			if (atributo == null)
			{
				return new List<ValorVariacaoViewModel>();
			}
			List<string> source = (from x in HelperIEnumerable.ToListEmptyWhenNull<string>(valoresGrade)
				where !HelperString.IsNullOrEmpty(x)
				select x).Distinct().ToList();
			bool listaFechada = atributo.ListaFechada;
			List<AtributoOpcaoDto> opcoes = (listaFechada ? atributo.values.Select((Func<AttributeValue, AtributoOpcaoDto>)((AttributeValue x) => new AtributoOpcaoDto
			{
				Id = x.id,
				Valor = x.name
			})).ToList() : new List<AtributoOpcaoDto>());
			return source.Select(delegate(string valor)
			{
				AtributoOpcaoDto val = null;
				if (salvas.TryGetValue(valor, out var salva) && !HelperString.IsNullOrEmpty(salva.ValueId))
				{
					val = opcoes.FirstOrDefault((AtributoOpcaoDto x) => x.Id == salva.ValueId);
				}
				if (val == null && listaFechada)
				{
					val = opcoes.FirstOrDefault((AtributoOpcaoDto x) => Normalizar(x.Valor) == Normalizar(valor));
				}
				return new ValorVariacaoViewModel(valor, opcoes, val)
				{
					ListaLivre = !listaFechada
				};
			}).ToList();
		}

		private static string Normalizar(string valor)
		{
			return HelperString.RemoverAcentos(HelperString.ToStringEmptyWhenNull(valor).Trim(), false).ToLowerInvariant();
		}

		public override void ProjectedAsViewModel()
		{
			_projetando = true;
			try
			{
				List<VariacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(base.Dto.Variacoes);
				if (list.Any())
				{
					ProjetarVariacoesSalvas(list);
				}
				GradeSimples = Variacoes.Itens.Select((VariacaoMercadoLivreViewModel x) => x.Coluna).Distinct().Count() <= 1;
			}
			finally
			{
				_projetando = false;
			}
			AlterouCategoria(base.Dto.CategoriaId);
		}

		private void ProjetarVariacoesSalvas(IEnumerable<VariacaoDto> salvas)
		{
			Variacoes.Limpar();
			foreach (VariacaoDto salva in salvas)
			{
				Variacoes.Adicionar(new VariacaoMercadoLivreViewModel
				{
					Id = salva.Id,
					Sku = salva.Sku,
					Nome = salva.Nome,
					CodigoBarras = salva.CodigoBarras,
					Linha = salva.Linha,
					Coluna = salva.Coluna,
					Preco = salva.Preco,
					QuantidadeDisponivel = salva.QuantidadeDisponivel,
					IdImagem = salva.IdImagem,
					UrlImagem = salva.UrlImagem,
					IdVariacaoMercadoLivre = salva.IdVariacaoMercadoLivre,
					IdItemMercadoLivre = salva.IdItemMercadoLivre,
					Permalink = salva.Permalink,
					LinhaTabelaMedidasId = salva.LinhaTabelaMedidasId
				});
			}
			LinqExtensions.ForEach<VariacaoMercadoLivreViewModel>((IEnumerable<VariacaoMercadoLivreViewModel>)Variacoes.Itens, (Action<VariacaoMercadoLivreViewModel>)delegate(VariacaoMercadoLivreViewModel x)
			{
				x.Alterou += VariacaoAlterada;
			});
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			Attribute atributoLinha = AtributosLinha.ItemSelecionado;
			Attribute atributoColuna = AtributosColuna.ItemSelecionado;
			val.AtributoVariacaoLinhaId = ((atributoLinha == null) ? string.Empty : atributoLinha.id);
			val.AtributoVariacaoColunaId = ((atributoColuna == null) ? string.Empty : atributoColuna.id);
			val.TabelaMedidasId = ((TabelasMedida.ItemSelecionado == null) ? string.Empty : TabelasMedida.ItemSelecionado.id);
			val.ExigeTabelaMedidas = _exigeTabelaMedidas;
			val.Variacoes = ((IEnumerable<VariacaoMercadoLivreViewModel>)Variacoes.Itens).Select((Func<VariacaoMercadoLivreViewModel, VariacaoDto>)((VariacaoMercadoLivreViewModel variacao) => new VariacaoDto
			{
				Id = variacao.Id,
				Sku = variacao.Sku,
				Nome = variacao.Nome,
				CodigoBarras = variacao.CodigoBarras,
				Linha = variacao.Linha,
				Coluna = variacao.Coluna,
				Preco = variacao.Preco,
				QuantidadeDisponivel = variacao.QuantidadeDisponivel,
				IdImagem = HelperString.ToStringEmptyWhenNull(variacao.IdImagem),
				UrlImagem = HelperString.ToStringEmptyWhenNull(variacao.UrlImagem),
				IdVariacaoMercadoLivre = variacao.IdVariacaoMercadoLivre,
				IdItemMercadoLivre = HelperString.ToStringEmptyWhenNull(variacao.IdItemMercadoLivre),
				Permalink = HelperString.ToStringEmptyWhenNull(variacao.Permalink),
				LinhaTabelaMedidasId = HelperString.ToStringEmptyWhenNull(variacao.LinhaTabelaMedidasId),
				Combinacoes = MontarCombinacoes(variacao, atributoLinha, atributoColuna)
			})).ToList();
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		private IEnumerable<CombinacaoAtributoDto> MontarCombinacoes(VariacaoMercadoLivreViewModel variacao, Attribute atributoLinha, Attribute atributoColuna)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected O, but got Unknown
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			List<CombinacaoAtributoDto> list = new List<CombinacaoAtributoDto>();
			ValorVariacaoViewModel valorVariacaoViewModel = HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresLinha).FirstOrDefault((ValorVariacaoViewModel x) => x.ValorGrade == variacao.Linha);
			if (atributoLinha != null && valorVariacaoViewModel != null)
			{
				list.Add(new CombinacaoAtributoDto
				{
					AtributoId = atributoLinha.id,
					AtributoNome = atributoLinha.name,
					ValueId = valorVariacaoViewModel.ValueIdEnviado,
					ValueName = valorVariacaoViewModel.ValorEnviado
				});
			}
			if (GradeSimples)
			{
				return list;
			}
			ValorVariacaoViewModel valorVariacaoViewModel2 = HelperIEnumerable.ToListEmptyWhenNull<ValorVariacaoViewModel>((IEnumerable<ValorVariacaoViewModel>)ValoresColuna).FirstOrDefault((ValorVariacaoViewModel x) => x.ValorGrade == variacao.Coluna);
			if (atributoColuna != null && valorVariacaoViewModel2 != null)
			{
				list.Add(new CombinacaoAtributoDto
				{
					AtributoId = atributoColuna.id,
					AtributoNome = atributoColuna.name,
					ValueId = valorVariacaoViewModel2.ValueIdEnviado,
					ValueName = valorVariacaoViewModel2.ValorEnviado
				});
			}
			return list;
		}

		public override void Dispose()
		{
			_anuncioViewModel.AlterouProdutoEvent -= AlterouProdutoEvent;
			((BindableBase)_anuncioViewModel).PropertyChanged -= AnuncioAlterado;
			LinqExtensions.ForEach<VariacaoMercadoLivreViewModel>((IEnumerable<VariacaoMercadoLivreViewModel>)Variacoes.Itens, (Action<VariacaoMercadoLivreViewModel>)delegate(VariacaoMercadoLivreViewModel x)
			{
				x.Alterou -= VariacaoAlterada;
			});
			((ViewModelValidationBase)Variacoes).Dispose();
			((ViewModelValidationBase)TamanhosTabela).Dispose();
			((ViewModelValidationBase)AtributosLinha).Dispose();
			((ViewModelValidationBase)AtributosColuna).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnuncioMercadoLivreImportarAbaViewModel : AbaPrincipalViewModelBase
	{
		public AnuncioMercadoLivreImportarAbaViewModel(IViewModelPrincipal viewModel)
			: base("Importar", "Importar", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreImportarFichaViewModel : FichaBase<AnuncioDto, AnuncioMercadoLivreImportarViewModel>
	{
		private AnuncioDto _parametro;

		public ICommand AdicionarCommand { get; set; }

		public AnuncioMercadoLivreImportarFichaViewModel()
			: base("Importar anúncio")
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			((NavigationBase)this).ParametroRetornado += OnParametroRetornado;
			base.DisableConfirmOnClose();
		}

		private void OnParametroRetornado(object sender, NavigationContext navigationContext)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			if (!HelperObject.IsNull((object)_parametro))
			{
				navigationContext.Parameters.Add("DtoIncluir", (object)((IFormularioEntity)base.Formulario).ToDto<AnuncioDto>());
			}
		}

		private void Adicionar()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				if (base.Formulario.Valido)
				{
					_parametro = ((IFormularioEntity)base.Formulario).ToDto<AnuncioDto>();
				}
				((NavigationBase)this).FecharView();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAdicionar()
		{
			if (base.Formulario != null)
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroRetornado -= OnParametroRetornado;
			base.Dispose();
		}
	}
	public class AnuncioMercadoLivreImportarFormulario : FormularioEntity<AnuncioDto>
	{
		public AnuncioMercadoLivreImportarFormulario(AnuncioDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnuncioMercadoLivreImportarAbaViewModel((IViewModelPrincipal)(object)new AnuncioMercadoLivreImportarViewModel(base.Dto)) });
		}
	}
	public class AnuncioMercadoLivreImportarViewModel : ViewModelDtoBase<AnuncioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly MercadoLivreApi _api;

		private IEnumerable<ListingType> _listingTypes;

		private Category _categoria;

		private List<string> _atributosVariacaoCategoria = new List<string>();

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'ID Mercado Livre' é obrigatório.")]
		public string IdMercadoLivre
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => IdMercadoLivre));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => IdMercadoLivre), value);
			}
		}

		[Required]
		public Item Item
		{
			get
			{
				return ((BindableBase)this).GetProperty<Item>((Expression<Func<Item>>)(() => Item));
			}
			set
			{
				((BindableBase)this).SetProperty<Item>((Expression<Func<Item>>)(() => Item), value, (Action)ObterCategoria);
			}
		}

		public string ItemStatus
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ItemStatus));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ItemStatus), value);
			}
		}

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value);
			}
		}

		public StatusAnuncioMercadoLivre Status { get; set; }

		public List<Item> ItensFamilia { get; set; } = new List<Item>();


		public ICommand ConsultarCommand { get; set; }

		public AnuncioMercadoLivreImportarViewModel()
		{
		}

		public AnuncioMercadoLivreImportarViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			_api = new MercadoLivreApi(DadosSistema.ObterProxy());
			((MercadoLivreBaseApi)_api).SetarToken(DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).TokenAcesso);
			ConsultarCommand = (ICommand)new AsyncCommand((Func<Task>)Consultar, (Func<bool>)PodeConsultar, false, (bool?)null);
			ObterListingTypes();
		}

		private async void ObterListingTypes()
		{
			AnuncioMercadoLivreImportarViewModel anuncioMercadoLivreImportarViewModel = this;
			_ = anuncioMercadoLivreImportarViewModel._listingTypes;
			anuncioMercadoLivreImportarViewModel._listingTypes = await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<ListingType>>("/sites/MLB/listing_types", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
		}

		private bool PodeConsultar()
		{
			return !string.IsNullOrEmpty(IdMercadoLivre);
		}

		private async Task Consultar()
		{
			_ = 1;
			try
			{
				Item = await ObterItem(HelperString.ApenasNumeros(IdMercadoLivre));
				List<Item> itensFamilia = (await _api.ObterItensDaFamiliaAsync(Item)).ToList();
				ItensFamilia = itensFamilia;
				if (Item.status.Equals(Status.active))
				{
					Status = (StatusAnuncioMercadoLivre)1;
				}
				else if (Item.status.Equals(Status.closed))
				{
					Status = (StatusAnuncioMercadoLivre)3;
				}
				else
				{
					Status = (StatusAnuncioMercadoLivre)2;
				}
				ItemStatus = HelperEnum.RetornarDescricao((Enum)(object)Status);
				decimal num = Item.initial_quantity ?? 0m;
				decimal num2 = Item.sold_quantity ?? 0m;
				Quantidade = num - num2;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async Task<Item> ObterItem(string numero)
		{
			Exception erro;
			try
			{
				return await ((MercadoLivreBaseApi)_api).GetAsync<Item>($"/items/MLB{numero}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
			}
			catch (Exception ex)
			{
				erro = ex;
			}
			long idUsuario = DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).IdUsuario;
			string text = ((idUsuario <= 0) ? string.Empty : (await _api.ObterPrimeiroItemDaFamiliaAsync(idUsuario, numero)));
			string text2 = text;
			if (HelperString.IsNullOrEmpty(text2))
			{
				throw erro;
			}
			return await ((MercadoLivreBaseApi)_api).GetAsync<Item>($"/items/{text2}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
		}

		private async void ObterCategoria()
		{
			AnuncioMercadoLivreImportarViewModel anuncioMercadoLivreImportarViewModel = this;
			_ = anuncioMercadoLivreImportarViewModel._categoria;
			anuncioMercadoLivreImportarViewModel._categoria = await ((MercadoLivreBaseApi)_api).GetAsync<Category>($"/categories/{Item.category_id}", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e8: Expected O, but got Unknown
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			AnuncioDto obj2 = val;
			Description description = Item.description;
			obj2.Descricao = ((description != null) ? description.plain_text : null) ?? Item.title;
			val.Titulo = HelperString.ToStringEmptyWhenNull(Item.title);
			val.NomeFamilia = HelperString.ToStringEmptyWhenNull(Item.family_name);
			val.Status = Status;
			val.Preco = Item.price;
			((EntityDto)val).Ativo = true;
			val.CategoriaId = HelperString.ToStringEmptyWhenNull(Item.category_id);
			val.CategoriaDescricao = HelperString.ToStringEmptyWhenNull(_categoria.name);
			val.Deletado = false;
			val.Imagens = HelperIEnumerable.ToListEmptyWhenNull<ImagemAnuncioDto>(Item.pictures.Select((Func<Picture, ImagemAnuncioDto>)((Picture pic) => new ImagemAnuncioDto
			{
				Url = pic.url
			})));
			val.IdMercadoLivre = HelperString.ToStringEmptyWhenNull(Item.id);
			val.Permalink = HelperString.ToStringEmptyWhenNull(Item.permalink);
			val.Novo = HelperString.ToStringEmptyWhenNull(Item.condition).Equals("new");
			val.QuantidadeDisponivel = Quantidade;
			val.TipoAnuncioId = HelperString.ToStringEmptyWhenNull(Item.listing_type_id);
			AnuncioDto obj3 = val;
			ListingType obj4 = _listingTypes.FirstOrDefault((ListingType x) => x.id.Equals(Item.listing_type_id));
			obj3.TipoAnuncioDescricao = ((obj4 != null) ? HelperString.ToStringEmptyWhenNull(obj4.name) : null);
			ImportarVariacoes(val);
			if (Item.sale_terms.Any())
			{
				SaleTerm obj5 = Item.sale_terms.FirstOrDefault((SaleTerm x) => x.id.Equals("WARRANTY_TIME"));
				string text = ((obj5 != null) ? obj5.value_name.Split(' ')[0] : null) ?? "0";
				SaleTerm obj6 = Item.sale_terms.FirstOrDefault((SaleTerm x) => x.id.Equals("WARRANTY_TIME"));
				string text2 = ((obj6 != null) ? obj6.value_name.Split(' ')[1] : null) ?? string.Empty;
				SaleTerm obj7 = Item.sale_terms.FirstOrDefault((SaleTerm x) => x.id.Equals("WARRANTY_TYPE"));
				string text3 = ((obj7 != null) ? obj7.value_name : null) ?? string.Empty;
				val.Garantia = new GarantiaAnuncioDto
				{
					TempoGarantia = Convert.ToInt32(Convert.ToDecimal(text.Replace(".", ","))),
					TipoTempoGarantia = HelperString.ToStringEmptyWhenNull(text2),
					TipoGarantia = HelperString.ToStringEmptyWhenNull(text3)
				};
			}
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		private void ImportarVariacoes(AnuncioDto anuncio)
		{
			if (!Item.PossuiVariacoes && ItensFamilia.Count > 1)
			{
				ImportarVariacoesUserProducts(anuncio);
				return;
			}
			anuncio.PossuiGrade = Item.PossuiVariacoes;
			if (!anuncio.PossuiGrade)
			{
				anuncio.Variacoes = Enumerable.Empty<VariacaoDto>();
				anuncio.AtributoVariacaoLinhaId = string.Empty;
				anuncio.AtributoVariacaoColunaId = string.Empty;
				return;
			}
			List<string> source = (from id in Item.variations.SelectMany((Variation variacao) => from combinacao in HelperIEnumerable.ToListEmptyWhenNull<AttributeCombination>(variacao.attribute_combinations)
					select combinacao.id)
				where !HelperString.IsNullOrEmpty(id)
				select id).Distinct().ToList();
			anuncio.AtributoVariacaoLinhaId = HelperString.ToStringEmptyWhenNull(source.ElementAtOrDefault(0));
			anuncio.AtributoVariacaoColunaId = HelperString.ToStringEmptyWhenNull(source.ElementAtOrDefault(1));
			anuncio.Variacoes = Item.variations.Select((Func<Variation, VariacaoDto>)delegate(Variation variacao)
			{
				//IL_0058: Unknown result type (might be due to invalid IL or missing references)
				//IL_005d: Unknown result type (might be due to invalid IL or missing references)
				//IL_007f: Unknown result type (might be due to invalid IL or missing references)
				//IL_009a: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
				//IL_010e: Unknown result type (might be due to invalid IL or missing references)
				//IL_011a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0126: Unknown result type (might be due to invalid IL or missing references)
				//IL_0137: Unknown result type (might be due to invalid IL or missing references)
				//IL_0152: Unknown result type (might be due to invalid IL or missing references)
				//IL_0183: Expected O, but got Unknown
				List<AttributeCombination> source2 = HelperIEnumerable.ToListEmptyWhenNull<AttributeCombination>(variacao.attribute_combinations);
				AttributeCombination val = source2.FirstOrDefault((AttributeCombination x) => x.id == anuncio.AtributoVariacaoLinhaId);
				AttributeCombination val2 = source2.FirstOrDefault((AttributeCombination x) => x.id == anuncio.AtributoVariacaoColunaId);
				return new VariacaoDto
				{
					IdVariacaoMercadoLivre = (variacao.id ?? 0),
					Linha = ((val == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(val.value_name)),
					Coluna = ((val2 == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(val2.value_name)),
					Nome = string.Join(" ", from x in source2
						select x.value_name into x
						where !HelperString.IsNullOrEmpty(x)
						select x),
					Sku = ObterSkuVariacao(variacao),
					Preco = variacao.price,
					QuantidadeDisponivel = variacao.available_quantity,
					IdImagem = HelperString.ToStringEmptyWhenNull(HelperIEnumerable.ToListEmptyWhenNull<string>(variacao.picture_ids).FirstOrDefault()),
					Combinacoes = ((IEnumerable<AttributeCombination>)source2).Select((Func<AttributeCombination, CombinacaoAtributoDto>)((AttributeCombination combinacao) => new CombinacaoAtributoDto
					{
						AtributoId = HelperString.ToStringEmptyWhenNull(combinacao.id),
						AtributoNome = HelperString.ToStringEmptyWhenNull(combinacao.name),
						ValueId = HelperString.ToStringEmptyWhenNull(combinacao.value_id),
						ValueName = HelperString.ToStringEmptyWhenNull(combinacao.value_name)
					})).ToList()
				};
			}).ToList();
		}

		private void ImportarVariacoesUserProducts(AnuncioDto anuncio)
		{
			List<string> source = (from x in ItensFamilia.SelectMany((Item x) => from a in AtributosDeVariacao(x)
					select a.id)
				group x by x into g
				where ItensFamilia.Select(delegate(Item x)
				{
					ItemAttribute obj = AtributosDeVariacao(x).FirstOrDefault((ItemAttribute a) => a.id == g.Key);
					return (obj == null) ? null : obj.value_name;
				}).Distinct().Count() > 1
				select g.Key).ToList();
			if (!source.Any())
			{
				source = ItensFamilia.SelectMany((Item x) => from a in AtributosDeVariacao(x)
					select a.id).Distinct().ToList();
			}
			anuncio.PossuiGrade = true;
			anuncio.AtributoVariacaoLinhaId = HelperString.ToStringEmptyWhenNull(source.ElementAtOrDefault(0));
			anuncio.AtributoVariacaoColunaId = HelperString.ToStringEmptyWhenNull(source.ElementAtOrDefault(1));
			anuncio.Variacoes = ((IEnumerable<Item>)ItensFamilia).Select((Func<Item, VariacaoDto>)delegate(Item itemFamilia)
			{
				//IL_0053: Unknown result type (might be due to invalid IL or missing references)
				//IL_0058: Unknown result type (might be due to invalid IL or missing references)
				//IL_0069: Unknown result type (might be due to invalid IL or missing references)
				//IL_007a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0095: Unknown result type (might be due to invalid IL or missing references)
				//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
				//IL_0109: Unknown result type (might be due to invalid IL or missing references)
				//IL_0115: Unknown result type (might be due to invalid IL or missing references)
				//IL_0121: Unknown result type (might be due to invalid IL or missing references)
				//IL_012d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0182: Expected O, but got Unknown
				List<ItemAttribute> source2 = AtributosDeVariacao(itemFamilia);
				ItemAttribute val = source2.FirstOrDefault((ItemAttribute x) => x.id == anuncio.AtributoVariacaoLinhaId);
				ItemAttribute val2 = source2.FirstOrDefault((ItemAttribute x) => x.id == anuncio.AtributoVariacaoColunaId);
				return new VariacaoDto
				{
					IdItemMercadoLivre = HelperString.ToStringEmptyWhenNull(itemFamilia.id),
					Permalink = HelperString.ToStringEmptyWhenNull(itemFamilia.permalink),
					Linha = ((val == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(val.value_name)),
					Coluna = ((val2 == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(val2.value_name)),
					Nome = string.Join(" ", from x in source2
						select x.value_name into x
						where !HelperString.IsNullOrEmpty(x)
						select x),
					Sku = ObterSkuItem(itemFamilia),
					Preco = itemFamilia.price,
					QuantidadeDisponivel = itemFamilia.available_quantity,
					Combinacoes = source2.Where((ItemAttribute x) => x.id == anuncio.AtributoVariacaoLinhaId || x.id == anuncio.AtributoVariacaoColunaId).Select((Func<ItemAttribute, CombinacaoAtributoDto>)((ItemAttribute x) => new CombinacaoAtributoDto
					{
						AtributoId = HelperString.ToStringEmptyWhenNull(x.id),
						AtributoNome = HelperString.ToStringEmptyWhenNull(x.id),
						ValueId = HelperString.ToStringEmptyWhenNull(x.value_id),
						ValueName = HelperString.ToStringEmptyWhenNull(x.value_name)
					})).ToList()
				};
			}).ToList();
		}

		private static List<ItemAttribute> AtributosDeVariacao(Item item)
		{
			return (from x in HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(item.attributes)
				where !HelperString.ToStringEmptyWhenNull(x.id).Equals("SELLER_SKU", StringComparison.OrdinalIgnoreCase)
				select x).ToList();
		}

		private static string ObterSkuItem(Item item)
		{
			ItemAttribute val = HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(item.attributes).FirstOrDefault((ItemAttribute x) => HelperString.ToStringEmptyWhenNull(x.id).Equals("SELLER_SKU", StringComparison.OrdinalIgnoreCase));
			if (val == null || HelperString.IsNullOrEmpty(val.value_name))
			{
				return HelperString.ToStringEmptyWhenNull(item.seller_sku);
			}
			return val.value_name;
		}

		private static string ObterSkuVariacao(Variation variacao)
		{
			ItemAttribute val = HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(variacao.attributes).FirstOrDefault((ItemAttribute x) => HelperString.ToStringEmptyWhenNull(x.id).Equals("SELLER_SKU", StringComparison.OrdinalIgnoreCase));
			if (val == null || HelperString.IsNullOrEmpty(val.value_name))
			{
				return HelperString.ToStringEmptyWhenNull(variacao.seller_custom_field);
			}
			return val.value_name;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AnuncioMercadoLivreImportarFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Anúncios do Mercado Livre";
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
	public class AnuncioMercadoLivreListaViewModel : ListaEntity<AnuncioDto, AnuncioMercadoLivreViewModel, IAnuncioMercadoLivreModuleService, AnuncioMercadoLivre>
	{
		private readonly IWService _service;

		private readonly IMercadoLivreApiService _mercadoLivreApiService;

		private const string AtributoTamanho = "SIZE";

		private const int MaximoImagensPorVariacao = 10;

		public ICommand SincronizarCommand { get; set; }

		public ICommand SincronizarTudoCommand { get; set; }

		public ICommand AcessarAnuncioCommand { get; set; }

		public ICommand EnviarAnuncioCommand { get; set; }

		public ICommand EnviarTodosAnunciosCommand { get; set; }

		public ICommand PararAnuncioCommand { get; set; }

		public ICommand AtivarAnuncioCommand { get; set; }

		public ICommand FecharAnuncioCommand { get; set; }

		public ICommand ExcluirAnuncioCommand { get; set; }

		public ICommand ImportarMercadoLivreCommand { get; set; }

		public ICommand ReativarParaNovoAnuncioCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public AnuncioMercadoLivreListaViewModel(IWService service, ISessao sessao, IMercadoLivreApiService mercadoLivreApiService, IEntityAtivos<AnuncioDto, IAnuncioMercadoLivreModuleService> ativos, IEntityInativos<AnuncioDto, IAnuncioMercadoLivreModuleService> inativos)
			: base(sessao, typeof(AnuncioMercadoLivreFichaAdicionarView).Name, typeof(AnuncioMercadoLivreFichaAtualizarView).Name, ativos, inativos)
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Expected O, but got Unknown
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Expected O, but got Unknown
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Expected O, but got Unknown
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Expected O, but got Unknown
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Expected O, but got Unknown
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Expected O, but got Unknown
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Expected O, but got Unknown
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Expected O, but got Unknown
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Expected O, but got Unknown
			//IL_022a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0234: Expected O, but got Unknown
			_service = service;
			_mercadoLivreApiService = mercadoLivreApiService;
			SincronizarCommand = (ICommand)new AsyncCommand((Func<Task>)Sincronizar, (Func<bool>)PodeSincronizar, false, (bool?)null);
			SincronizarTudoCommand = (ICommand)new AsyncCommand((Func<Task>)SincronizarTudo, (Func<bool>)PodeSincronizarTudo, false, (bool?)null);
			AcessarAnuncioCommand = (ICommand)new DelegateCommand((Action)AcessarAnuncio, (Func<bool>)PodeAcessarAnuncio, (bool?)null);
			EnviarAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarAnuncio, (Func<bool>)PodeEnviarAnuncio, (bool?)null);
			EnviarTodosAnunciosCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarTodosAnuncios, (Func<bool>)PodeEnviarTodosAnuncios, (bool?)null);
			PararAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)PararAnuncio, (Func<bool>)PodePararAnuncio, (bool?)null);
			AtivarAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)AtivarAnuncio, (Func<bool>)PodeAtivarAnuncio, (bool?)null);
			FecharAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)FecharAnuncio, (Func<bool>)PodeFecharAnuncio, (bool?)null);
			ExcluirAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)ExcluirAnuncio, (Func<bool>)PodeExcluirAnuncio, (bool?)null);
			ImportarMercadoLivreCommand = (ICommand)new DelegateCommand((Action)ImportarMercadoLivre);
			ReativarParaNovoAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)ReativarParaNovoAnuncio, (Func<bool>)PodeReativarParaNovoAnuncio, (bool?)null);
			DuplicarCommand = (ICommand)new AsyncCommand((Func<Task>)Duplicar, (Func<bool>)PodeDuplicar, (bool?)null);
		}

		private void ImportarMercadoLivre()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			Action<NavigationResult> obj = delegate
			{
			};
			NavigationParameters val = new NavigationParameters();
			val.Add(typeof(AnuncioMercadoLivreImportarViewModel).Name, (object)new AnuncioMercadoLivreImportarViewModel());
			((NavigationBase)this).Navegar("AnuncioMercadoLivreImportarFichaView", obj, val);
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
			object obj = regionNavigationEventArgs.NavigationContext.Parameters["DtoIncluir"];
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!(regionNavigationEventArgs.Uri.OriginalString != typeof(AnuncioMercadoLivreListaView).Name) && val != null)
			{
				((ListaManutencaoBase<AnuncioDto, AnuncioMercadoLivreViewModel, AnuncioMercadoLivre>)(object)this).Adicionar(val);
			}
		}

		private bool PodeSincronizar()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Permalink) && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre))
			{
				return !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado;
			}
			return false;
		}

		private async Task Sincronizar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("O status e a quantidade serão atualizados de acordo com os dados atuais no Mercado Livre. Deseja Continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
			Item val = await _mercadoLivreApiService.GetUnprotectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (IEnumerable<KeyValuePair<string, string>>)null);
			if (val.status.Equals(Status.active))
			{
				dto.Status = (StatusAnuncioMercadoLivre)1;
			}
			else if (val.status.Equals(Status.closed))
			{
				dto.Status = (StatusAnuncioMercadoLivre)3;
			}
			else if (val.status.Equals(Status.paused))
			{
				dto.Status = (StatusAnuncioMercadoLivre)2;
			}
			else if (val.status.Equals(Status.under_review))
			{
				dto.Status = (StatusAnuncioMercadoLivre)2;
			}
			decimal num = val.initial_quantity ?? 0m;
			decimal num2 = val.sold_quantity ?? 0m;
			dto.QuantidadeDisponivel = num - num2;
			await AtualizarAnuncio(dto).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		private bool PodeSincronizarTudo()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens))
			{
				return ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private async Task SincronizarTudo()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Os status e quantidades serão atualizados de acordo com os dados atuais no Mercado Livre. Deseja Continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			Lista<string> idsNaoSincronizados = new Lista<string>();
			Lista<string> idsSincronizados = new Lista<string>();
			IEnumerable<AnuncioDto> enumerable = from x in ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Data
				select ((ViewModelDtoBase<AnuncioDto>)(object)x).Dto into x
				where !string.IsNullOrEmpty(x.Permalink) && !string.IsNullOrEmpty(x.IdMercadoLivre) && !x.Deletado
				select x;
			foreach (AnuncioDto itemMercadoLivreViewModel in enumerable)
			{
				try
				{
					AnuncioDto dto = itemMercadoLivreViewModel;
					Item val = await _mercadoLivreApiService.GetUnprotectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (IEnumerable<KeyValuePair<string, string>>)null);
					if (val.status.Equals(Status.active))
					{
						dto.Status = (StatusAnuncioMercadoLivre)1;
					}
					else if (val.status.Equals(Status.closed))
					{
						dto.Status = (StatusAnuncioMercadoLivre)3;
					}
					else if (val.status.Equals(Status.paused))
					{
						dto.Status = (StatusAnuncioMercadoLivre)2;
					}
					else if (val.status.Equals(Status.under_review))
					{
						dto.Status = (StatusAnuncioMercadoLivre)2;
					}
					decimal num = val.initial_quantity ?? 0m;
					decimal num2 = val.sold_quantity ?? 0m;
					dto.QuantidadeDisponivel = num - num2;
					await _service.Canal<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>((Func<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>)((IAnuncioMercadoLivreModuleService service) => ((IAtualizar<AnuncioDto>)(object)service).AtualizarAsync(dto)));
					idsSincronizados.Adicionar(dto.IdMercadoLivre);
				}
				catch (Exception)
				{
					idsNaoSincronizados.Adicionar(itemMercadoLivreViewModel.IdMercadoLivre);
				}
			}
			ThreadPrincipal.Executar((Action)delegate
			{
				if (idsSincronizados.Itens.Any())
				{
					DialogoFactory.MostrarDialogoConfirma(string.Format("Total de anúncios enviados: {0}.\n {1}", idsSincronizados.Itens.Count, idsNaoSincronizados.Itens.Any() ? string.Format("Anúncios não enviados: {0}", string.Join(", ", idsNaoSincronizados.Itens)) : ""), false, HorizontalAlignment.Center);
				}
				((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
				((UpdateViewBindableBase)this).ProcessMessages();
				((UpdateViewBindableBase)this).RecuperarFoco();
			});
		}

		private bool PodeAcessarAnuncio()
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Permalink) && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado)
			{
				return (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1;
			}
			return false;
		}

		private void AcessarAnuncio()
		{
			Process.Start(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Permalink);
		}

		private bool PodeExcluirAnuncio()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 3)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task ExcluirAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("O anúncio e todos os dados vinculados a ele serão excluídos do Mercado Livre. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				Status val = Status.Criar().Deleted();
				await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (object)val, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
				dto.Status = (StatusAnuncioMercadoLivre)3;
				dto.Deletado = true;
				((EntityDto)dto).Ativo = false;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeReativarParaNovoAnuncio()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 3)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task ReativarParaNovoAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Excluir dados do anúncio anterior e permitir anunciá-lo como um novo item?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				dto.Status = (StatusAnuncioMercadoLivre)1;
				dto.IdMercadoLivre = string.Empty;
				dto.Permalink = string.Empty;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeFecharAnuncio()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status != 3)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task FecharAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ao fechar o anúncio, ele ficará indisponível para compradores no Mercado Livre. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				Status val = Status.Criar().ComStatus(Status.closed);
				await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (object)val, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
				dto.Status = (StatusAnuncioMercadoLivre)3;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAtivarAnuncio()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 2)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task AtivarAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ao ativar o anúncio, ele ficará disponível para compradores no Mercado Livre. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				Status val = Status.Criar().ComStatus(Status.active);
				await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (object)val, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
				dto.Status = (StatusAnuncioMercadoLivre)1;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodePararAnuncio()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.IdMercadoLivre) && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task PararAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ao parar o anúncio, ele ficará indisponível para compradores no Mercado Livre. Você poderá ativá-lo novamente quando quiser. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				Status val = Status.Criar().ComStatus(Status.paused);
				await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (object)val, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
				dto.Status = (StatusAnuncioMercadoLivre)2;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeEnviarTodosAnuncios()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task EnviarTodosAnuncios()
		{
			_ = 1;
			try
			{
				IEnumerable<AnuncioDto> enumerable = from x in ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Data
					select ((ViewModelDtoBase<AnuncioDto>)(object)x).Dto into x
					where (int)x.Status == 1
					select x;
				if (DialogoFactory.MostrarDialogoSimNao("Anúncios já enviados para o Mercado Livre serão atualizados. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				Lista<string> idsNaoSincronizados = new Lista<string>();
				Lista<string> idsSincronizados = new Lista<string>();
				foreach (AnuncioDto dto in enumerable)
				{
					try
					{
						if (!string.IsNullOrEmpty(dto.IdMercadoLivre))
						{
							await AtualizarAnuncioMercadoLivre(dto);
							idsSincronizados.Adicionar(dto.IdMercadoLivre);
						}
						else
						{
							await CadastrarAnuncioMercadoLivre(dto);
							idsSincronizados.Adicionar(dto.IdMercadoLivre);
						}
					}
					catch (Exception)
					{
						idsNaoSincronizados.Adicionar(dto.IdMercadoLivre);
					}
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					if (idsSincronizados.Itens.Any())
					{
						DialogoFactory.MostrarDialogoConfirma(string.Format("Total de anúncios enviados: {0}.\n {1}", idsSincronizados.Itens.Count, idsNaoSincronizados.Itens.Any() ? string.Format("Anúncios não enviados: {0}", string.Join(", ", idsNaoSincronizados.Itens)) : ""), false, HorizontalAlignment.Center);
					}
					((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
					((UpdateViewBindableBase)this).ProcessMessages();
					((UpdateViewBindableBase)this).RecuperarFoco();
				});
			}
			catch (Exception ex2)
			{
				HelperException.ShowLastException(ex2, "", false, false);
			}
		}

		private bool PodeEnviarAnuncio()
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioMercadoLivre, InteragirPlataforma>();
			}
			return false;
		}

		private async Task EnviarAnuncio()
		{
			_ = 1;
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto;
				if (!string.IsNullOrEmpty(dto.IdMercadoLivre))
				{
					if (DialogoFactory.MostrarDialogoSimNao("Este anúncio já foi enviado para o Mercado Livre. Deseja enviar uma atualização?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes && PodeEnviarAnuncio())
					{
						await AtualizarAnuncioMercadoLivre(dto);
					}
				}
				else
				{
					await CadastrarAnuncioMercadoLivre(dto);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async Task CadastrarAnuncioMercadoLivre(AnuncioDto dto)
		{
			if (dto.UtilizarInformacaoCadastroSincronismo && !HelperString.IsNullOrEmpty(dto.ProdutoServicoEmpresaReferencia))
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { dto.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
				dto.QuantidadeDisponivel = val.ProdutoEmpresa.Estoque.QuantidadeSaldo;
				dto.Preco = val.Preco.PrecoVendaVigente;
			}
			List<ItemAttribute> atributos = HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(dto.Atributos.Select((AtributoDto atributo) => ItemAttribute.Criar().ComId(atributo.Id).ComValueName(atributo.ValueName)
				.ComValueId(atributo.ValueId)));
			string sellerId = _mercadoLivreApiService.GetSellerId();
			bool userProducts = UsaUserProducts(await _mercadoLivreApiService.GetProtectedAsync<UserResponse>($"/users/{sellerId}", Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo));
			if (!ValidarGtin(dto) || !ValidarVariacoes(dto) || !ValidarTabelaMedidas(dto) || (userProducts && !ValidarNomeFamilia(dto)))
			{
				return;
			}
			if (userProducts && dto.PossuiGrade)
			{
				await CadastrarFamiliaUserProducts(dto, atributos);
				return;
			}
			Item item2 = CriarItemAnuncio(dto).ComCategoryId(dto.CategoriaId).ComCondition(dto.Novo ? "new" : "used").ComListingTypeId(HelperString.IsEmptyUseNull(dto.TipoAnuncioId))
				.ComAttributes((IEnumerable<ItemAttribute>)atributos);
			if (userProducts)
			{
				item2.SetarBuyingMode(dto.Classificado);
				item2.SetarFamilyName(dto.NomeFamilia);
				item2.LimparTitle();
			}
			Description descricao = Description.Criar().ComPlainText(dto.Descricao.Replace("\r\n", "\n").Replace("\r", "\n").Replace('\u00ad'.ToString(), ""));
			item2 = await _mercadoLivreApiService.PostProtectedAsync<Item>("/items", (object)item2, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
			await _mercadoLivreApiService.PostProtectedAsync<Item>($"/items/{item2.id}/description", (object)descricao, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
			dto.IdMercadoLivre = item2.id;
			dto.Permalink = item2.permalink;
			GravarVariacoesDoRetorno(dto, item2);
			await AtualizarAnuncio(dto);
		}

		private static void GravarVariacoesDoRetorno(AnuncioDto dto, Item item)
		{
			List<Variation> list = HelperIEnumerable.ToListEmptyWhenNull<Variation>(item.variations);
			if (!list.Any())
			{
				return;
			}
			foreach (VariacaoDto item2 in HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(dto.Variacoes))
			{
				Variation val = ObterVariacaoRetornada(list, item2);
				if (val != null && val.id.HasValue)
				{
					item2.IdVariacaoMercadoLivre = val.id.Value;
					item2.Permalink = HelperString.ToStringEmptyWhenNull(item.permalink);
				}
			}
		}

		private static Variation ObterVariacaoRetornada(List<Variation> retornadas, VariacaoDto variacao)
		{
			Variation val = retornadas.FirstOrDefault((Variation x) => HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(x.attributes).Any((ItemAttribute atributo) => "SELLER_SKU".Equals(atributo.id, StringComparison.OrdinalIgnoreCase) && !HelperString.IsNullOrEmpty(variacao.Sku) && variacao.Sku.Equals(atributo.value_name)));
			if (val != null)
			{
				return val;
			}
			return retornadas.FirstOrDefault((Variation x) => HelperIEnumerable.ToListEmptyWhenNull<AttributeCombination>(x.attribute_combinations).Any((AttributeCombination combinacao) => HelperIEnumerable.ToListEmptyWhenNull<CombinacaoAtributoDto>(variacao.Combinacoes).Any((CombinacaoAtributoDto salva) => salva.AtributoId.Equals(combinacao.id) && !HelperString.IsNullOrEmpty(salva.ValueName) && salva.ValueName.Equals(combinacao.value_name, StringComparison.OrdinalIgnoreCase))));
		}

		private static bool UsaUserProducts(UserResponse user)
		{
			return HelperIEnumerable.ToListEmptyWhenNull<string>(user.tags).Contains("user_product_seller");
		}

		private static string DescreverFalhasPublicacao(IEnumerable<KeyValuePair<string, string>> falhas)
		{
			List<string> list = (from falha in HelperIEnumerable.ToListEmptyWhenNull<KeyValuePair<string, string>>(falhas)
				group falha by falha.Value).Select(delegate(IGrouping<string, KeyValuePair<string, string>> grupo)
			{
				string arg = string.Join(", ", grupo.Select((KeyValuePair<string, string> falha) => falha.Key));
				IEnumerable<string> values = from linha in HelperString.ToStringEmptyWhenNull(grupo.Key).Split(new string[2]
					{
						Environment.NewLine,
						"\n"
					}, StringSplitOptions.RemoveEmptyEntries)
					select $"- {linha.Trim()}";
				return $"{arg}:{Environment.NewLine}{string.Join(Environment.NewLine, values)}";
			}).ToList();
			if (!list.Any())
			{
				return string.Empty;
			}
			return Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine + Environment.NewLine, list);
		}

		private async Task<IDictionary<string, string>> ObterTamanhosTabelaMedidas(AnuncioDto dto)
		{
			Dictionary<string, string> tamanhos = new Dictionary<string, string>();
			if (HelperString.IsNullOrEmpty(dto.TabelaMedidasId))
			{
				return tamanhos;
			}
			Chart val = await _mercadoLivreApiService.GetProtectedAsync<Chart>($"/catalog/charts/{dto.TabelaMedidasId}", Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
			if (val == null)
			{
				return tamanhos;
			}
			foreach (ChartRow item in HelperIEnumerable.ToListEmptyWhenNull<ChartRow>(val.rows))
			{
				bool flag = HelperIEnumerable.ToListEmptyWhenNull<ChartAttribute>(item.attributes).Any((ChartAttribute x) => "SIZE".Equals(x.id, StringComparison.OrdinalIgnoreCase));
				string text = item.ValorPrincipal(flag ? "SIZE" : val.main_attribute_id);
				if (!HelperString.IsNullOrEmpty(item.id) && !HelperString.IsNullOrEmpty(text) && !tamanhos.ContainsKey(item.id))
				{
					tamanhos.Add(item.id, text);
				}
			}
			return tamanhos;
		}

		private static string TamanhoDaLinhaTabelaMedidas(VariacaoDto variacao, IDictionary<string, string> tamanhos)
		{
			if (tamanhos == null || HelperString.IsNullOrEmpty(variacao.LinhaTabelaMedidasId))
			{
				return string.Empty;
			}
			if (!tamanhos.TryGetValue(variacao.LinhaTabelaMedidasId, out var value))
			{
				return string.Empty;
			}
			return value;
		}

		private static bool ValidarVariacoes(AnuncioDto dto)
		{
			if (!dto.PossuiGrade || HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(dto.Variacoes).Any())
			{
				return true;
			}
			DialogoFactory.MostrarDialogoConfirma("Este anúncio é de um produto com grade, mas não tem nenhuma variação gravada." + Environment.NewLine + "Abra o anúncio e veja a aba Variações: se o Mercado Livre não aceitar variações na categoria escolhida, troque a categoria na aba Categoria ou publique o produto sem grade." + Environment.NewLine + "Depois de relacionar as variações, grave o anúncio antes de enviar.", false, HorizontalAlignment.Center);
			return false;
		}

		private static bool ValidarTabelaMedidas(AnuncioDto dto)
		{
			if (!dto.PossuiGrade || !dto.ExigeTabelaMedidas || !HelperString.IsNullOrEmpty(dto.TabelaMedidasId))
			{
				return true;
			}
			DialogoFactory.MostrarDialogoConfirma("Nenhuma tabela de medidas selecionada." + Environment.NewLine + "Abra o anúncio, vá até a aba Variações, escolha a tabela de medidas, relacione as linhas e grave antes de enviar." + Environment.NewLine + "Se o campo da tabela estiver vazio, a mensagem na própria aba explica o motivo.", false, HorizontalAlignment.Center);
			return false;
		}

		private static bool ValidarGtin(AnuncioDto dto)
		{
			AtributoDto val = HelperIEnumerable.ToListEmptyWhenNull<AtributoDto>(dto.Atributos).FirstOrDefault((AtributoDto x) => HelperString.ToStringEmptyWhenNull(x.Id).Equals("GTIN", StringComparison.OrdinalIgnoreCase));
			string text = ((val == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(val.ValueName).Trim());
			if (HelperString.IsNullOrEmpty(text) || GtinValido(text))
			{
				return true;
			}
			DialogoFactory.MostrarDialogoConfirma($"O código '{text}' informado em GTIN não é um código de barras válido.\n" + "O Mercado Livre aceita EAN/UPC com 8, 12, 13 ou 14 dígitos numéricos.\nCorrija na aba Categoria ou deixe o campo em branco.", false, HorizontalAlignment.Center);
			return false;
		}

		private static bool GtinValido(string valor)
		{
			if (!valor.All(char.IsDigit))
			{
				return false;
			}
			if (valor.Length != 8 && valor.Length != 12 && valor.Length != 13 && valor.Length != 14)
			{
				return false;
			}
			int num = 0;
			int num2 = 3;
			for (int num3 = valor.Length - 2; num3 >= 0; num3--)
			{
				num += (valor[num3] - 48) * num2;
				num2 = ((num2 == 3) ? 1 : 3);
			}
			return (10 - num % 10) % 10 == valor[valor.Length - 1] - 48;
		}

		private static bool ValidarNomeFamilia(AnuncioDto dto)
		{
			if (dto.Classificado || !HelperString.IsNullOrEmpty(dto.NomeFamilia))
			{
				return true;
			}
			DialogoFactory.MostrarDialogoConfirma("Informe o 'Nome de família' na aba Anúncio. O Mercado Livre exige esse campo para a sua conta.", false, HorizontalAlignment.Center);
			return false;
		}

		private async Task CadastrarFamiliaUserProducts(AnuncioDto dto, IEnumerable<ItemAttribute> atributosAnuncio)
		{
			Description descricao = Description.Criar().ComPlainText(dto.Descricao.Replace("\r\n", "\n").Replace("\r", "\n").Replace('\u00ad'.ToString(), ""));
			List<VariacaoDto> variacoes = HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(dto.Variacoes);
			List<VariacaoDto> gerados = new List<VariacaoDto>();
			List<KeyValuePair<string, string>> falhas = new List<KeyValuePair<string, string>>();
			IDictionary<string, string> tamanhos = await ObterTamanhosTabelaMedidas(dto);
			foreach (VariacaoDto variacao in variacoes)
			{
				try
				{
					Item item2 = CriarItemVariacaoUserProducts(dto, variacao, atributosAnuncio, tamanhos);
					item2 = await _mercadoLivreApiService.PostProtectedAsync<Item>("/items", (object)item2, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
					await _mercadoLivreApiService.PostProtectedAsync<Item>($"/items/{item2.id}/description", (object)descricao, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
					variacao.IdItemMercadoLivre = HelperString.ToStringEmptyWhenNull(item2.id);
					variacao.Permalink = HelperString.ToStringEmptyWhenNull(item2.permalink);
					gerados.Add(variacao);
				}
				catch (Exception ex)
				{
					falhas.Add(new KeyValuePair<string, string>(variacao.Nome, ex.GetBaseException().Message));
				}
			}
			VariacaoDto val = gerados.FirstOrDefault();
			if (val != null)
			{
				dto.IdMercadoLivre = val.IdItemMercadoLivre;
				dto.Permalink = val.Permalink;
				await AtualizarAnuncio(dto);
			}
			DialogoFactory.MostrarDialogoConfirma($"{gerados.Count} de {variacoes.Count} variação(ões) publicada(s) na família '{dto.NomeFamilia}'." + DescreverFalhasPublicacao(falhas), false, HorizontalAlignment.Left);
		}

		private Item CriarItemVariacaoUserProducts(AnuncioDto dto, VariacaoDto variacao, IEnumerable<ItemAttribute> atributosAnuncio, IDictionary<string, string> tamanhosTabelaMedidas)
		{
			List<ItemAttribute> list = (from x in HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(atributosAnuncio)
				where HelperIEnumerable.ToListEmptyWhenNull<CombinacaoAtributoDto>(variacao.Combinacoes).All((CombinacaoAtributoDto c) => c.AtributoId != x.id)
				select x).ToList();
			list.AddRange(from combinacao in HelperIEnumerable.ToListEmptyWhenNull<CombinacaoAtributoDto>(variacao.Combinacoes)
				select ItemAttribute.Criar().ComId(combinacao.AtributoId).ComValueName(combinacao.ValueName)
					.ComValueId(combinacao.ValueId));
			list.Add(ItemAttribute.Criar().ComId("SELLER_SKU").ComValueName(variacao.Sku));
			if (!HelperString.IsNullOrEmpty(dto.TabelaMedidasId))
			{
				list.Add(ItemAttribute.Criar().ComId("SIZE_GRID_ID").ComValueName(dto.TabelaMedidasId));
			}
			if (!HelperString.IsNullOrEmpty(variacao.LinhaTabelaMedidasId))
			{
				list.Add(ItemAttribute.Criar().ComId("SIZE_GRID_ROW_ID").ComValueName(variacao.LinhaTabelaMedidasId));
			}
			string text = TamanhoDaLinhaTabelaMedidas(variacao, tamanhosTabelaMedidas);
			if (!HelperString.IsNullOrEmpty(text))
			{
				list.RemoveAll((ItemAttribute x) => "SIZE".Equals(x.id, StringComparison.OrdinalIgnoreCase));
				list.Add(ItemAttribute.Criar().ComId("SIZE").ComValueName(text));
			}
			List<SaleTerm> list2 = new List<SaleTerm>
			{
				SaleTerm.Criar().ComId("WARRANTY_TYPE").ComValueName(dto.Garantia.TipoGarantia),
				SaleTerm.Criar().ComId("WARRANTY_TIME").ComValueName($"{dto.Garantia.TempoGarantia} {dto.Garantia.TipoTempoGarantia}")
			};
			Item obj = Item.Criar().ComAvailableQuantity(variacao.QuantidadeDisponivel).ComPictures(dto.Imagens.Select((ImagemAnuncioDto img) => Picture.Criar().ComSource(img.Url)))
				.ComPrice(variacao.Preco)
				.ComSaleTerms((IEnumerable<SaleTerm>)list2)
				.ComCurrencyId(Item.currencyBrl)
				.ComStatus(Status.active)
				.ComCategoryId(dto.CategoriaId)
				.ComCondition(dto.Novo ? "new" : "used")
				.ComListingTypeId(HelperString.IsEmptyUseNull(dto.TipoAnuncioId))
				.ComAttributes((IEnumerable<ItemAttribute>)list);
			obj.SetarBuyingMode(dto.Classificado);
			obj.SetarFamilyName(dto.NomeFamilia);
			obj.LimparTitle();
			return obj;
		}

		public static string SanitizarDescricaoPlainText(string textoOriginal)
		{
			if (string.IsNullOrWhiteSpace(textoOriginal))
			{
				return string.Empty;
			}
			textoOriginal = textoOriginal.Replace("\r\n", "\n").Replace("\r", "\n");
			StringBuilder stringBuilder = new StringBuilder();
			string text = textoOriginal;
			foreach (char c in text)
			{
				if (c == '\n' || (!char.IsControl(c) && !char.IsSurrogate(c)))
				{
					stringBuilder.Append(c);
				}
			}
			return new string((from ch in stringBuilder.ToString().Normalize(NormalizationForm.FormD)
				where CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark
				select ch).ToArray()).Trim();
		}

		private async Task AtualizarAnuncioMercadoLivre(AnuncioDto dto)
		{
			if (dto.UtilizarInformacaoCadastroSincronismo && !HelperString.IsNullOrEmpty(dto.ProdutoServicoEmpresaReferencia))
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { dto.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
				dto.QuantidadeDisponivel = val.ProdutoEmpresa.Estoque.Quantidade;
				dto.Preco = val.Preco.PrecoVendaVigente;
			}
			Description descricao = Description.Criar().ComPlainText(dto.Descricao.Replace("\r\n", "\n").Replace("\r", "\n").Replace('\u00ad'.ToString(), ""));
			string sellerId = _mercadoLivreApiService.GetSellerId();
			bool userProducts = UsaUserProducts(await _mercadoLivreApiService.GetProtectedAsync<UserResponse>($"/users/{sellerId}", Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo));
			if (!ValidarGtin(dto) || !ValidarVariacoes(dto) || !ValidarTabelaMedidas(dto) || (userProducts && !ValidarNomeFamilia(dto)))
			{
				return;
			}
			if (userProducts && dto.PossuiGrade)
			{
				await AtualizarFamiliaUserProducts(dto, descricao);
				return;
			}
			Item val2 = CriarItemAnuncio(dto);
			if (userProducts)
			{
				val2.LimparTitle();
			}
			Item atualizado = await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}", (object)val2, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
			await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{dto.IdMercadoLivre}/description?api_version=2", (object)descricao, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
			if (atualizado != null)
			{
				GravarVariacoesDoRetorno(dto, atualizado);
				await AtualizarAnuncio(dto);
			}
		}

		private async Task AtualizarFamiliaUserProducts(AnuncioDto dto, Description descricao)
		{
			List<ItemAttribute> atributos = HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(dto.Atributos.Select((AtributoDto atributo) => ItemAttribute.Criar().ComId(atributo.Id).ComValueName(atributo.ValueName)
				.ComValueId(atributo.ValueId)));
			int atualizadas = 0;
			List<VariacaoDto> novas = new List<VariacaoDto>();
			List<KeyValuePair<string, string>> falhas = new List<KeyValuePair<string, string>>();
			IDictionary<string, string> tamanhos = await ObterTamanhosTabelaMedidas(dto);
			foreach (VariacaoDto variacao in HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(dto.Variacoes))
			{
				try
				{
					Item item2 = CriarItemVariacaoUserProducts(dto, variacao, atributos, tamanhos);
					if (HelperString.IsNullOrEmpty(variacao.IdItemMercadoLivre))
					{
						item2 = await _mercadoLivreApiService.PostProtectedAsync<Item>("/items", (object)item2, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
						await _mercadoLivreApiService.PostProtectedAsync<Item>($"/items/{item2.id}/description", (object)descricao, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
						variacao.IdItemMercadoLivre = HelperString.ToStringEmptyWhenNull(item2.id);
						variacao.Permalink = HelperString.ToStringEmptyWhenNull(item2.permalink);
						novas.Add(variacao);
					}
					else
					{
						await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{variacao.IdItemMercadoLivre}", (object)item2, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
						await _mercadoLivreApiService.PutProtectedAsync<Item>($"/items/{variacao.IdItemMercadoLivre}/description?api_version=2", (object)descricao, Enumerable.Empty<KeyValuePair<string, string>>(), (Action)NavegarMercadoLivreModulo);
						atualizadas++;
					}
				}
				catch (Exception ex)
				{
					falhas.Add(new KeyValuePair<string, string>(variacao.Nome, ex.GetBaseException().Message));
				}
			}
			if (novas.Any())
			{
				await AtualizarAnuncio(dto);
			}
			DialogoFactory.MostrarDialogoConfirma($"{atualizadas} variação(ões) atualizada(s) e {novas.Count} publicada(s) na família '{dto.NomeFamilia}'." + DescreverFalhasPublicacao(falhas), false, HorizontalAlignment.Left);
		}

		private Item CriarItemAnuncio(AnuncioDto dto)
		{
			List<SaleTerm> list = new List<SaleTerm>
			{
				SaleTerm.Criar().ComId("WARRANTY_TYPE").ComValueName(dto.Garantia.TipoGarantia),
				SaleTerm.Criar().ComId("WARRANTY_TIME").ComValueName($"{dto.Garantia.TempoGarantia} {dto.Garantia.TipoTempoGarantia}")
			};
			Item val = Item.Criar().ComAvailableQuantity(dto.QuantidadeDisponivel).ComPictures(dto.Imagens.Select((ImagemAnuncioDto img) => Picture.Criar().ComSource(img.Url)))
				.ComPrice(dto.Preco)
				.ComTitle(dto.Titulo)
				.ComSaleTerms((IEnumerable<SaleTerm>)list)
				.ComCurrencyId(Item.currencyBrl)
				.ComStatus(Status.active)
				.ComAttributes((IEnumerable<ItemAttribute>)(object)new ItemAttribute[1] { ItemAttribute.Criar().ComId("seller_sku").ComValueName((!HelperString.IsObjectIdDefaultOrEmpty(dto.ProdutoServicoEmpresaReferencia)) ? dto.ProdutoServicoEmpresaReferencia : dto.KitReferencia) });
			if (dto.PossuiGrade)
			{
				val = val.ComVariations(CriarVariacoesAnuncio(dto));
				if (!HelperString.IsNullOrEmpty(dto.TabelaMedidasId))
				{
					val = val.ComAttributes(HelperIEnumerable.ToListEmptyWhenNull<ItemAttribute>(val.attributes).Concat((IEnumerable<ItemAttribute>)(object)new ItemAttribute[1] { ItemAttribute.Criar().ComId("SIZE_GRID_ID").ComValueName(dto.TabelaMedidasId) }));
				}
			}
			return val;
		}

		private static IEnumerable<ItemAttribute> AtributosVariacaoLegacy(AnuncioDto dto, VariacaoDto variacao)
		{
			List<ItemAttribute> list = new List<ItemAttribute> { ItemAttribute.Criar().ComId("SELLER_SKU").ComValueName(variacao.Sku) };
			if (!HelperString.IsNullOrEmpty(variacao.LinhaTabelaMedidasId))
			{
				list.Add(ItemAttribute.Criar().ComId("SIZE_GRID_ROW_ID").ComValueName(variacao.LinhaTabelaMedidasId));
			}
			return list;
		}

		private static IEnumerable<Variation> CriarVariacoesAnuncio(AnuncioDto dto)
		{
			List<string> imagensAnuncio = (from imagem in HelperIEnumerable.ToListEmptyWhenNull<ImagemAnuncioDto>(dto.Imagens)
				select HelperString.ToStringEmptyWhenNull(imagem.Url) into url
				where !HelperString.IsNullOrEmpty(url)
				select url).Take(10).ToList();
			return (from variacao in HelperIEnumerable.ToListEmptyWhenNull<VariacaoDto>(dto.Variacoes)
				select Variation.Criar().ComId((long?)variacao.IdVariacaoMercadoLivre).ComAttributeCombinations(from combinacao in HelperIEnumerable.ToListEmptyWhenNull<CombinacaoAtributoDto>(variacao.Combinacoes)
					select AttributeCombination.Criar().ComId(combinacao.AtributoId).ComName(combinacao.AtributoNome)
						.ComValueId(combinacao.ValueId)
						.ComValueName(combinacao.ValueName))
					.ComAttributes(AtributosVariacaoLegacy(dto, variacao))
					.ComPrice(variacao.Preco)
					.ComAvailableQuantity(HelperObject.ToInt((object)variacao.QuantidadeDisponivel))
					.ComPictureIds(ImagensDaVariacao(variacao, imagensAnuncio))).ToList();
		}

		private static IEnumerable<string> ImagensDaVariacao(VariacaoDto variacao, IEnumerable<string> imagensAnuncio)
		{
			if (!HelperString.IsNullOrEmpty(variacao.IdImagem))
			{
				return new string[1] { variacao.IdImagem };
			}
			if (!HelperString.IsNullOrEmpty(variacao.UrlImagem))
			{
				return new string[1] { variacao.UrlImagem };
			}
			return imagensAnuncio;
		}

		private async Task AtualizarAnuncio(AnuncioDto dto)
		{
			AnuncioDto dtoAtualizado = default(AnuncioDto);
			_ = dtoAtualizado;
			dtoAtualizado = await _service.Canal<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>((Func<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>)((IAnuncioMercadoLivreModuleService service) => ((IAtualizar<AnuncioDto>)(object)service).AtualizarAsync(dto)));
			ThreadPrincipal.Executar((Action)delegate
			{
				((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Atualizar(dtoAtualizado);
				((UpdateViewBindableBase)this).ProcessMessages();
				((UpdateViewBindableBase)this).RecuperarFoco();
			});
		}

		private void NavegarMercadoLivreModulo()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Expected O, but got Unknown
				DialogoFactory.MostrarDialogoConfirma("É necessário autenticar o usuário através da API do Mercado Livre.", false, HorizontalAlignment.Center);
				IRegion region = ((NavigationBase)this).NavigationService.Region;
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add("ID", (object)Guid.NewGuid().ToString());
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)region, "MercadoLivreView", obj, val);
			});
		}

		public override bool PodeEditar()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			if (((ListaManutencaoBase<AnuncioDto, AnuncioMercadoLivreViewModel, AnuncioMercadoLivre>)(object)this).PodeEditar() && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Selecionado).Dto.Deletado;
			}
			return false;
		}

		private bool PodeDuplicar()
		{
			if (((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens != null && ((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return ((ListaManutencaoBase<AnuncioDto, AnuncioMercadoLivreViewModel, AnuncioMercadoLivre>)(object)this).PodeAdicionar();
			}
			return false;
		}

		private async Task Duplicar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Deseja duplicar o anúncio selecionado?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				AnuncioDto obj = await _service.Canal<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>((Func<IAnuncioMercadoLivreModuleService, Task<AnuncioDto>>)((IAnuncioMercadoLivreModuleService service) => ((IReferenciaAtualizarService<AnuncioDto>)(object)service).AtualizarDtoAsync(((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.ObterDtoItemSelecionado())));
				obj.Duplicar();
				AnuncioMercadoLivreViewModel anuncioMercadoLivreViewModel = new AnuncioMercadoLivreViewModel(obj);
				((ViewModelBase<AnuncioDto>)(object)anuncioMercadoLivreViewModel).ProjectedAsViewModel();
				AnuncioMercadoLivreListaViewModel anuncioMercadoLivreListaViewModel = this;
				string viewAdicionarFicha = ((ListaManutencaoBase<AnuncioDto, AnuncioMercadoLivreViewModel, AnuncioMercadoLivre>)(object)this).ViewAdicionarFicha;
				Action<NavigationResult> obj2 = delegate(NavigationResult navigationResult)
				{
					if (navigationResult.Result == true)
					{
						((ListaBase<AnuncioDto, AnuncioMercadoLivreViewModel>)(object)this).Itens.Estado.AlterarAdicao();
					}
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(AnuncioMercadoLivreViewModel).Name, (object)anuncioMercadoLivreViewModel);
				((NavigationBase)anuncioMercadoLivreListaViewModel).Navegar(viewAdicionarFicha, obj2, val);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}
	}
	public class AnuncioMercadoLivreAbaViewModel : AbaPrincipalViewModelBase
	{
		public AnuncioMercadoLivreAbaViewModel(IViewModelPrincipal viewModel)
			: base("Anúncio", "Dados principais", viewModel)
		{
		}
	}
	public class AnuncioMercadoLivreFichaAdicionarViewModel : FichaBaseAdicionar<AnuncioDto, AnuncioMercadoLivreViewModel, IAnuncioMercadoLivreModuleService>
	{
		public AnuncioMercadoLivreFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class AnuncioMercadoLivreFichaAtualizarViewModel : FichaBaseAtualizar<AnuncioDto, AnuncioMercadoLivreViewModel, IAnuncioMercadoLivreModuleService>
	{
		public AnuncioMercadoLivreFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class AnuncioMercadoLivreFormulario : FormularioEntity<AnuncioDto>
	{
		private readonly AnuncioMercadoLivreCategoriaViewModel _categoriaViewModel;

		private readonly AnuncioMercadoLivreGarantiaViewModel _garantiaViewModel;

		private readonly AnuncioMercadoLivreViewModel _anuncioViewModel;

		private readonly AnuncioMercadoLivreImagensViewModel _imagensViewModel;

		private AnuncioMercadoLivreVariacaoViewModel _variacaoViewModel;

		private string _categoriaIdAtual;

		public AnuncioMercadoLivreFormulario(AnuncioDto dto)
			: base(dto)
		{
			_anuncioViewModel = new AnuncioMercadoLivreViewModel(base.Dto);
			_garantiaViewModel = new AnuncioMercadoLivreGarantiaViewModel(base.Dto);
			_imagensViewModel = new AnuncioMercadoLivreImagensViewModel(base.Dto);
			_categoriaViewModel = new AnuncioMercadoLivreCategoriaViewModel(base.Dto);
			_categoriaViewModel.AlterouCategoriaEvent += CategoriaViewModelOnAlterouCategoriaEvent;
			_anuncioViewModel.AlterouProdutoEvent += AlterouProdutoEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[4]
			{
				(IAbaViewModelBase)new AnuncioMercadoLivreAbaViewModel((IViewModelPrincipal)(object)_anuncioViewModel),
				(IAbaViewModelBase)new AnuncioMercadoLivreCategoriaAbaViewModel((IViewModelDtoBase)(object)_categoriaViewModel),
				(IAbaViewModelBase)new AnuncioMercadoLivreGarantiaAbaViewModel((IViewModelDtoBase)(object)_garantiaViewModel),
				(IAbaViewModelBase)new AnuncioMercadoLivreImagensAbaViewModel((IViewModelDtoBase)(object)_imagensViewModel)
			});
			if (base.Dto.PossuiGrade)
			{
				AdicionarAbaVariacao();
			}
		}

		private void AdicionarAbaVariacao()
		{
			_variacaoViewModel = new AnuncioMercadoLivreVariacaoViewModel(base.Dto, _anuncioViewModel, _imagensViewModel, _categoriaViewModel);
			_variacaoViewModel.AlterouNecessidadeTabelaMedidasEvent += AlterouNecessidadeTabelaMedidasEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnuncioMercadoLivreVariacaoAbaViewModel((IViewModelDtoBase)(object)_variacaoViewModel) });
			string text = (HelperString.IsNullOrEmpty(_categoriaIdAtual) ? base.Dto.CategoriaId : _categoriaIdAtual);
			if (!HelperString.IsNullOrEmpty(text))
			{
				_variacaoViewModel.AlterouCategoria(text);
			}
		}

		private void AlterouNecessidadeTabelaMedidasEvent(object sender, bool precisaCriar)
		{
			IAbaViewModelBase val = ((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => HelperObject.Is<AnuncioMercadoLivreTabelaMedidasAbaViewModel>((object)x));
			if (!precisaCriar || _variacaoViewModel == null)
			{
				RemoverAbaTabelaMedidas(val);
			}
			else if (val == null)
			{
				((Formulario)this).InserirProjetarAba((IAbaViewModelBase)(object)new AnuncioMercadoLivreTabelaMedidasAbaViewModel((IViewModelDtoBase)(object)new AnuncioMercadoLivreTabelaMedidasViewModel(base.Dto, _variacaoViewModel)), false);
			}
		}

		private void RemoverAbaTabelaMedidas(IAbaViewModelBase aba)
		{
			if (aba != null)
			{
				IAbaViewModelBase val = ((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => HelperObject.Is<AnuncioMercadoLivreVariacaoAbaViewModel>((object)x));
				if (((Formulario)this).AbaSelecionada == ((Formulario)this).Abas.IndexOf(aba) && val != null)
				{
					((Formulario)this).AbaSelecionada = ((Formulario)this).Abas.IndexOf(val);
				}
				((Formulario)this).RemoverAba(aba);
			}
		}

		private void AlterouProdutoEvent(object sender, EstoqueDto estoque)
		{
			if (HelperObject.IsExactlyObject<EstoqueGradeDto>((object)estoque))
			{
				if (!((Formulario)this).Abas.Any((IAbaViewModelBase x) => HelperObject.Is<AnuncioMercadoLivreVariacaoAbaViewModel>((object)x)))
				{
					AdicionarAbaVariacao();
				}
				return;
			}
			((Formulario)this).PosicionarNaPrimeiraAba();
			RemoverAbaTabelaMedidas(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => HelperObject.Is<AnuncioMercadoLivreTabelaMedidasAbaViewModel>((object)x)));
			if (_variacaoViewModel != null)
			{
				_variacaoViewModel.AlterouNecessidadeTabelaMedidasEvent -= AlterouNecessidadeTabelaMedidasEvent;
			}
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => HelperObject.Is<AnuncioMercadoLivreVariacaoAbaViewModel>((object)x)));
			_variacaoViewModel = null;
		}

		protected override void OnAlterouAbaSelecionadaEvent(IAbaViewModelBase e)
		{
			((Formulario)this).OnAlterouAbaSelecionadaEvent(e);
			if (_variacaoViewModel != null && HelperObject.Is<AnuncioMercadoLivreVariacaoAbaViewModel>((object)e))
			{
				_variacaoViewModel.RevisarTabelasMedida();
			}
		}

		private void CategoriaViewModelOnAlterouCategoriaEvent(object sender, string categoryId)
		{
			_categoriaIdAtual = categoryId;
			_garantiaViewModel.AlterouCategoria(categoryId);
			if (_variacaoViewModel != null)
			{
				_variacaoViewModel.AlterouCategoria(categoryId);
			}
		}

		public override void Dispose()
		{
			_categoriaViewModel.AlterouCategoriaEvent -= CategoriaViewModelOnAlterouCategoriaEvent;
			_anuncioViewModel.AlterouProdutoEvent -= AlterouProdutoEvent;
			if (_variacaoViewModel != null)
			{
				_variacaoViewModel.AlterouNecessidadeTabelaMedidasEvent -= AlterouNecessidadeTabelaMedidasEvent;
			}
		}
	}
	public class AnuncioMercadoLivreViewModel : ViewModelAtivoBase<AnuncioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly MercadoLivreApi _api;

		public const int MaximoVariacoes = 100;

		public EstoqueDto Estoque { get; set; } = (EstoqueDto)new EstoqueDtoNull();


		public bool IsGrade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsGrade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsGrade), value);
			}
		}

		public Lista<ListingType> TiposAnuncio { get; set; } = new Lista<ListingType>();


		public ISelecaoCommandParameter ProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto dto)
		{
			EstoqueDto estoque = dto.ProdutoEmpresa.Estoque;
			EstoqueGradeDto val = (EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null);
			if (val != null && val.Grades.Count() > 100)
			{
				throw new Exception($"O Mercado Livre permite no máximo {100} variações por anúncio.\nSelecione outro produto.");
			}
			Estoque = dto.ProdutoEmpresa.Estoque;
			IsGrade = HelperObject.Is<EstoqueGradeDto>((object)dto.ProdutoEmpresa.Estoque);
			ProdutoServicoCodigoInterno = dto.ProdutoServico.CodigoInterno;
			ProdutoServicoEmpresaPrecoPrecoReferencia = ((EntityBaseDto)dto.Preco).Id;
			ProdutoServicoEmpresaReferencia = ((EntityBaseDto)dto).Id;
			ProdutoServicoEmpresaDescricao = dto.ProdutoServico.Descricao;
			KitReferencia = string.Empty;
			if (string.IsNullOrEmpty(Titulo))
			{
				Titulo = $"{dto.ProdutoServico.Produto.Marca.Descricao} {dto.ProdutoServico.Descricao} {dto.ProdutoServico.Caracteristica}".Trim();
				Preco = dto.Preco.PrecoVendaVigente;
			}
			Quantidade = (UtilizarInformacaoCadastroSincronismo ? dto.ProdutoEmpresa.Estoque.Quantidade : 0m);
			OnAlterouProdutoEvent(dto.ProdutoEmpresa.Estoque);
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
		}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa)), "Selecione um cadastro do tipo produto.", HorizontalAlignment.Center));

		public ISelecaoCommandParameter KitCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<KitDto>("KitListaView", (Action<KitDto>)delegate(KitDto dto)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			Estoque = (EstoqueDto)new EstoqueDtoNull();
			IsGrade = false;
			OnAlterouProdutoEvent((EstoqueDto)new EstoqueDtoNull());
			ProdutoServicoCodigoInterno = string.Empty;
			ProdutoServicoEmpresaPrecoPrecoReferencia = string.Empty;
			ProdutoServicoEmpresaReferencia = string.Empty;
			ProdutoServicoEmpresaDescricao = dto.Descricao;
			KitReferencia = ((EntityBaseDto)dto).Id;
			Titulo = dto.Descricao.Trim();
			Preco = dto.Itens.Sum((ItemKitDto x) => x.Quantidade * x.PrecoVenda);
			UtilizarInformacaoCadastroSincronismo = false;
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
			DialogoFactory.MostrarDialogoConfirma("Ao faturar o pedido o sistema sempre usará os dados atuais do Kit, por tanto alterar informações depois de enviar o anúncio podem causar divergências.", false, HorizontalAlignment.Center);
		}, true);

		public string ProdutoServicoEmpresaPrecoPrecoReferencia { get; set; }

		public string ProdutoServicoEmpresaReferencia { get; set; }

		public string ProdutoServicoCodigoInterno { get; set; }

		public string KitReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => KitReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => KitReferencia), value);
			}
		}

		public bool PodeAlterarPreco
		{
			get
			{
				if (HelperString.IsNullOrEmpty(KitReferencia))
				{
					return !UtilizarInformacaoCadastroSincronismo;
				}
				return false;
			}
		}

		public bool PodeAlterarQuantidade => !UtilizarInformacaoCadastroSincronismo;

		public bool PodeUtilizarInformacaoCadastroSincronismo
		{
			get
			{
				if (base.Ativo)
				{
					return HelperString.IsNullOrEmpty(KitReferencia);
				}
				return false;
			}
		}

		[Required(ErrorMessage = "O campo 'Produto' é obrigatório.", AllowEmptyStrings = false)]
		public string ProdutoServicoEmpresaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaDescricao), value);
			}
		}

		[Required(ErrorMessage = "O campo 'Título' é obrigatório.", AllowEmptyStrings = false)]
		public string Titulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Titulo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Titulo), value);
			}
		}

		public bool Classificado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Classificado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Classificado), value);
			}
		}

		public string NomeFamilia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeFamilia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeFamilia), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "O 'Preço de venda' deve ser maior que zero.")]
		public decimal Preco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Preco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value);
			}
		}

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value);
			}
		}

		public bool Novo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Novo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Novo), value);
			}
		}

		public bool UtilizarInformacaoCadastroSincronismo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarInformacaoCadastroSincronismo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarInformacaoCadastroSincronismo), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertiesChanged<bool, bool>((Expression<Func<bool>>)(() => PodeAlterarPreco), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioMercadoLivreViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
				});
			}
		}

		[Required(ErrorMessage = "O campo 'Descrição' é obrigatório.", AllowEmptyStrings = false)]
		public string Descricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Descricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Descricao), value);
			}
		}

		public StatusAnuncioMercadoLivre Status
		{
			get
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				return ((BindableBase)this).GetProperty<StatusAnuncioMercadoLivre>((Expression<Func<StatusAnuncioMercadoLivre>>)(() => Status));
			}
			set
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				((BindableBase)this).SetProperty<StatusAnuncioMercadoLivre>((Expression<Func<StatusAnuncioMercadoLivre>>)(() => Status), value);
			}
		}

		public string IdMercadoLivre { get; set; }

		public bool IsTransient { get; set; }

		public bool IsReadOndy { get; set; }

		public bool Deletado { get; set; }

		public string Permalink { get; set; }

		public event EventHandler<EstoqueDto> AlterouProdutoEvent;

		public AnuncioMercadoLivreViewModel()
		{
		}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown


		public AnuncioMercadoLivreViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			_api = new MercadoLivreApi(DadosSistema.ObterProxy());
			((MercadoLivreBaseApi)_api).SetarToken(DadosSistema.GetService().Canal<IMercadoLivreModuleService, TokenDto>((Func<IMercadoLivreModuleService, TokenDto>)((IMercadoLivreModuleService service) => service.ObterToken())).TokenAcesso);
		}

		protected virtual void OnAlterouProdutoEvent(EstoqueDto e)
		{
			this.AlterouProdutoEvent?.Invoke(this, e);
		}

		public override void ProjectedAsListaViewModel()
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			base.Ativo = ((EntityDto)((ViewModelDtoBase<AnuncioDto>)(object)this).Dto).Ativo;
			Titulo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Titulo;
			NomeFamilia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.NomeFamilia;
			Classificado = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Classificado;
			Preco = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Preco;
			IdMercadoLivre = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.IdMercadoLivre;
			Status = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Status;
			UtilizarInformacaoCadastroSincronismo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.UtilizarInformacaoCadastroSincronismo;
		}

		public override async void ProjectedAsViewModel()
		{
			((ViewModelBase<AnuncioDto>)(object)this).ProjectedAsListaViewModel();
			IsReadOndy = !((EntityBaseDto)((ViewModelDtoBase<AnuncioDto>)(object)this).Dto).IsTransient();
			Quantidade = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.QuantidadeDisponivel;
			Novo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Novo;
			Descricao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Descricao;
			KitReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.KitReferencia;
			ProdutoServicoEmpresaReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaReferencia;
			ProdutoServicoEmpresaPrecoPrecoReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaPrecoReferencia;
			ProdutoServicoEmpresaDescricao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaDescricao;
			ProdutoServicoCodigoInterno = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoCodigoInterno;
			Deletado = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Deletado;
			Permalink = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Permalink;
			IsGrade = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.PossuiGrade;
			UtilizarInformacaoCadastroSincronismo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.UtilizarInformacaoCadastroSincronismo;
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
			await ListarTiposAnuncios().ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.IdMercadoLivre = IdMercadoLivre;
				val.Titulo = Titulo;
				val.NomeFamilia = NomeFamilia;
				val.PossuiGrade = IsGrade;
				val.Classificado = Classificado;
				val.Preco = Preco;
				val.QuantidadeDisponivel = Quantidade;
				AnuncioDto obj2 = val;
				ListingType itemSelecionado = TiposAnuncio.ItemSelecionado;
				obj2.TipoAnuncioDescricao = ((itemSelecionado != null) ? itemSelecionado.name : null) ?? string.Empty;
				AnuncioDto obj3 = val;
				ListingType itemSelecionado2 = TiposAnuncio.ItemSelecionado;
				obj3.TipoAnuncioId = ((itemSelecionado2 != null) ? itemSelecionado2.id : null) ?? string.Empty;
				val.Novo = Novo;
				val.Descricao = Descricao;
				val.Status = Status;
				val.KitReferencia = KitReferencia;
				val.ProdutoServicoEmpresaReferencia = ProdutoServicoEmpresaReferencia;
				val.ProdutoServicoEmpresaPrecoReferencia = ProdutoServicoEmpresaPrecoPrecoReferencia;
				val.ProdutoServicoEmpresaDescricao = ProdutoServicoEmpresaDescricao;
				val.Deletado = Deletado;
				val.Permalink = Permalink;
				val.ProdutoServicoCodigoInterno = ProdutoServicoCodigoInterno;
				val.UtilizarInformacaoCadastroSincronismo = UtilizarInformacaoCadastroSincronismo;
				((ViewModelDtoBase<AnuncioDto>)(object)this).AtualizarDto<AnuncioDto>(ref val);
			}
		}

		private async Task ListarTiposAnuncios()
		{
			IEnumerable<ListingType> enumerable = await ((MercadoLivreBaseApi)_api).GetAsync<IEnumerable<ListingType>>("/sites/MLB/listing_types", (IEnumerable<KeyValuePair<string, string>>)null, (Action<string, string>)null);
			TiposAnuncio.Adicionar(enumerable);
			if (!string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.TipoAnuncioId))
			{
				TiposAnuncio.Posicionar((Expression<Func<ListingType, bool>>)((ListingType x) => x.id.Equals(((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.TipoAnuncioId)));
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AnuncioMercadoLivreFormulario(((ViewModelDtoBase<AnuncioDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Anúncios do Mercado Livre";
		}

		public string ToTituloFicha()
		{
			return "Anúncio do Mercado Livre";
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)TiposAnuncio).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnunciosMercadoLivreViewModel : ModuloBase
	{
		public AnunciosMercadoLivreViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAnuncioMercadoLivreRegion, "AnuncioMercadoLivreListaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.AnunciosMercadoLivre.ViewModels.Atributos
{
	public class AtributoBoleanoViewModel : AtributoViewModel
	{
		public bool Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Valor), value);
			}
		}

		public AtributoBoleanoViewModel(string valueId, string titulo, bool valor)
			: base(valueId, titulo)
		{
			Valor = valor;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			return (AtributoDto)new AtributoBoleanoDto
			{
				Id = base.ValueId,
				Valor = Valor,
				Titulo = base.Titulo
			};
		}
	}
	public class AtributoFactory
	{
		private IEnumerable<AtributoOpcaoDto> _opcoes = Enumerable.Empty<AtributoOpcaoDto>();

		private IEnumerable<AtributoOpcaoDto> _unidades = Enumerable.Empty<AtributoOpcaoDto>();

		private string _tipo;

		private string _titulo;

		private bool _valorBoleano;

		private int _valorInteiro;

		private decimal _valorDecimal;

		private string _valorTexto;

		private string _valueId;

		private bool _required;

		private AtributoOpcaoDto _valorOpcao;

		public static AtributoFactory Init()
		{
			return new AtributoFactory();
		}

		public AtributoFactory SetarTipo(string tipo)
		{
			_tipo = tipo;
			return this;
		}

		public AtributoFactory SetarTitulo(string titulo)
		{
			_titulo = titulo;
			return this;
		}

		public AtributoFactory SetarValor(string valor)
		{
			_valorTexto = valor;
			return this;
		}

		public AtributoFactory SetarValor(int valor)
		{
			_valorInteiro = valor;
			return this;
		}

		public AtributoFactory SetarValor(decimal valor)
		{
			_valorDecimal = valor;
			return this;
		}

		public AtributoFactory SetarValor(AtributoOpcaoDto valor)
		{
			_valorOpcao = valor;
			return this;
		}

		public AtributoFactory SetarOpcoes(IEnumerable<AtributoOpcaoDto> opcoes)
		{
			_opcoes = opcoes;
			return this;
		}

		public AtributoFactory SetarValor(bool valor)
		{
			_valorBoleano = valor;
			return this;
		}

		public AtributoFactory SetarValueId(string valueId)
		{
			_valueId = valueId;
			return this;
		}

		public AtributoFactory SetarUnidades(IEnumerable<AtributoOpcaoDto> unidades)
		{
			_unidades = unidades;
			return this;
		}

		public AtributoFactory SetarRequired(bool required)
		{
			_required = required;
			return this;
		}

		public AtributoViewModel Criar()
		{
			if (_tipo.Equals("list") || _opcoes.Any())
			{
				return new AtributoListaViewModel(_valueId, _titulo, _opcoes, _valorOpcao);
			}
			if (_tipo.Equals("number_unit") || _unidades.Any())
			{
				return new AtributoUnidadeViewModel(_valueId, _titulo, _valorDecimal, _unidades, _valorOpcao);
			}
			if (_tipo.Equals("string"))
			{
				return new AtributoTextoViewModel(_valueId, _titulo, _valorTexto);
			}
			if (_tipo.Equals("number"))
			{
				return new AtributoInteiroViewModel(_valueId, _titulo, _valorInteiro);
			}
			if (_tipo.Equals("boolean"))
			{
				return new AtributoBoleanoViewModel(_valueId, _titulo, _valorBoleano);
			}
			throw new NotImplementedException($"Não implementado ({_tipo}).");
		}
	}
	public class AtributoListaViewModel : AtributoViewModel
	{
		public Lista<AtributoOpcaoDto> Opcoes { get; set; } = new Lista<AtributoOpcaoDto>();


		[Required(ErrorMessage = "Campo obrigatório.")]
		public virtual AtributoOpcaoDto Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Selecionado), value);
			}
		}

		public AtributoListaViewModel(string valueId, string titulo, IEnumerable<AtributoOpcaoDto> opcoes, AtributoOpcaoDto selecionado)
			: base(valueId, titulo)
		{
			Opcoes.AdicionarSemPosicionar(opcoes);
			ObservableExtensions.Subscribe<AtributoOpcaoDto>(Opcoes.WhenAlterouItemSelecionado, (Action<AtributoOpcaoDto>)delegate(AtributoOpcaoDto itemSelecionado)
			{
				Selecionado = itemSelecionado;
			});
			if (HelperObject.IsNotNull((object)selecionado))
			{
				Opcoes.Posicionar((Expression<Func<AtributoOpcaoDto, bool>>)((AtributoOpcaoDto x) => x.Id.Equals(selecionado.Id)));
			}
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			return (AtributoDto)new AtributoListaDto
			{
				Id = base.ValueId,
				Titulo = base.Titulo,
				Opcoes = Opcoes.Itens,
				OpcaoSelecionada = Selecionado
			};
		}
	}
	public class AtributoInteiroViewModel : AtributoViewModel
	{
		public int Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Valor), value);
			}
		}

		public AtributoInteiroViewModel(string valueId, string titulo, int valor)
			: base(valueId, titulo)
		{
			Valor = valor;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			return (AtributoDto)new AtributoInteiroDto
			{
				Id = base.ValueId,
				Valor = Valor,
				Titulo = base.Titulo
			};
		}
	}
	public class AtributoTextoViewModel : AtributoViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório.")]
		public string Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Valor), value);
			}
		}

		public AtributoTextoViewModel(string valueId, string titulo, string valor)
			: base(valueId, titulo)
		{
			Valor = valor;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			return (AtributoDto)new AtributoTextoDto
			{
				Id = base.ValueId,
				Valor = Valor,
				Titulo = base.Titulo
			};
		}
	}
	public class AtributoUnidadeViewModel : AtributoListaViewModel
	{
		public decimal Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Valor), value);
			}
		}

		public AtributoUnidadeViewModel(string valueId, string titulo, decimal valor, IEnumerable<AtributoOpcaoDto> opcoes, AtributoOpcaoDto selecionado)
			: base(valueId, titulo, opcoes, selecionado)
		{
			Valor = valor;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			return (AtributoDto)new AtributoUnidadeDto
			{
				Valor = Valor,
				Titulo = base.Titulo,
				Opcoes = base.Opcoes.Itens,
				Id = base.ValueId,
				OpcaoSelecionada = Selecionado
			};
		}
	}
	public abstract class AtributoViewModel : ViewModelValidationBase
	{
		public string ValueId { get; set; }

		public string Titulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Titulo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Titulo), value);
			}
		}

		protected AtributoViewModel(string valueId, string titulo)
		{
			ValueId = valueId;
			Titulo = titulo;
		}

		public abstract AtributoDto ObterDto();
	}
}
namespace Otus.Modulo.AnunciosMercadoLivre.ViewModels.Variacoes
{
	public class VariacaoMercadoLivreViewModel : BindableBase
	{
		private const string CorNormal = "#FF333333";

		private const string CorAlerta = "#FFC62828";

		public string Id { get; set; } = string.Empty;


		public string Nome { get; set; } = string.Empty;


		public string Sku { get; set; } = string.Empty;


		public string CodigoBarras { get; set; } = string.Empty;


		public string Linha
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Linha));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Linha), value);
			}
		}

		public string Coluna
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Coluna));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Coluna), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "Preço não pode ser igual a zero.")]
		public decimal Preco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Preco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value, (Action)AoAlterar);
			}
		}

		public decimal QuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDisponivel), value, (Action)AoAlterar);
			}
		}

		public string UrlImagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => UrlImagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => UrlImagem), value, (Action)AoAlterar);
			}
		}

		public string IdImagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => IdImagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => IdImagem), value);
			}
		}

		public long IdVariacaoMercadoLivre
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => IdVariacaoMercadoLivre));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => IdVariacaoMercadoLivre), value);
			}
		}

		public string IdItemMercadoLivre
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => IdItemMercadoLivre));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => IdItemMercadoLivre), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => SituacaoEnvio));
				});
			}
		}

		public string Permalink { get; set; } = string.Empty;


		public ValorVariacaoViewModel MapeamentoLinha
		{
			get
			{
				return ((BindableBase)this).GetProperty<ValorVariacaoViewModel>((Expression<Func<ValorVariacaoViewModel>>)(() => MapeamentoLinha));
			}
			set
			{
				((BindableBase)this).SetProperty<ValorVariacaoViewModel>((Expression<Func<ValorVariacaoViewModel>>)(() => MapeamentoLinha), value);
			}
		}

		public ValorVariacaoViewModel MapeamentoColuna
		{
			get
			{
				return ((BindableBase)this).GetProperty<ValorVariacaoViewModel>((Expression<Func<ValorVariacaoViewModel>>)(() => MapeamentoColuna));
			}
			set
			{
				((BindableBase)this).SetProperty<ValorVariacaoViewModel>((Expression<Func<ValorVariacaoViewModel>>)(() => MapeamentoColuna), value);
			}
		}

		public string LinhaTabelaMedidasId
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => LinhaTabelaMedidasId));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => LinhaTabelaMedidasId), value, (Action)AoAlterar);
			}
		}

		public bool PrecoValido => Preco > 0m;

		public bool QuantidadeValida => QuantidadeDisponivel > 0m;

		public string CorPreco
		{
			get
			{
				if (!PrecoValido)
				{
					return "#FFC62828";
				}
				return "#FF333333";
			}
		}

		public string CorQuantidade
		{
			get
			{
				if (!QuantidadeValida)
				{
					return "#FFC62828";
				}
				return "#FF333333";
			}
		}

		public string SituacaoEnvio
		{
			get
			{
				if (!string.IsNullOrEmpty(IdItemMercadoLivre))
				{
					return IdItemMercadoLivre;
				}
				return "Não enviada";
			}
		}

		public event EventHandler Alterou;

		private void AoAlterar()
		{
			((BindableBase)this).RaisePropertiesChanged<string, string, bool, bool>((Expression<Func<string>>)(() => CorPreco), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(VariacaoMercadoLivreViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(VariacaoMercadoLivreViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(VariacaoMercadoLivreViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			this.Alterou?.Invoke(this, EventArgs.Empty);
		}
	}
	public class ValorVariacaoViewModel : BindableBase
	{
		public string ValorGrade { get; private set; }

		public Lista<AtributoOpcaoDto> Opcoes { get; set; } = new Lista<AtributoOpcaoDto>();


		public AtributoOpcaoDto Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Selecionado), value);
			}
		}

		public bool ListaLivre { get; set; }

		public bool PodeSelecionar => !ListaLivre;

		public bool Resolvido
		{
			get
			{
				if (!ListaLivre)
				{
					if (Selecionado != null)
					{
						return !string.IsNullOrEmpty(Selecionado.Id);
					}
					return false;
				}
				return true;
			}
		}

		public string ValorEnviado
		{
			get
			{
				if (Selecionado == null || string.IsNullOrEmpty(Selecionado.Valor))
				{
					return ValorGrade;
				}
				return Selecionado.Valor;
			}
		}

		public string ValueIdEnviado
		{
			get
			{
				if (Selecionado == null)
				{
					return string.Empty;
				}
				return Selecionado.Id;
			}
		}

		public event EventHandler AlterouSelecionado;

		public ValorVariacaoViewModel(string valorGrade, IEnumerable<AtributoOpcaoDto> opcoes, AtributoOpcaoDto selecionado)
		{
			ValorGrade = valorGrade;
			Opcoes.AdicionarSemPosicionar(opcoes);
			ObservableExtensions.Subscribe<AtributoOpcaoDto>(Opcoes.WhenAlterouItemSelecionado, (Action<AtributoOpcaoDto>)delegate(AtributoOpcaoDto item)
			{
				Selecionado = item;
				this.AlterouSelecionado?.Invoke(this, EventArgs.Empty);
			});
			if (selecionado != null)
			{
				Opcoes.Posicionar((Expression<Func<AtributoOpcaoDto, bool>>)((AtributoOpcaoDto x) => x.Id.Equals(selecionado.Id)));
			}
		}
	}
	public class TamanhoTabelaMedidasViewModel : BindableBase
	{
		public string Tamanho { get; set; } = string.Empty;


		public bool ExigeEquivalencia { get; set; }

		public bool ExigeMedida { get; set; }

		public List<AtributoOpcaoDto> Equivalencias { get; set; } = new List<AtributoOpcaoDto>();


		public AtributoOpcaoDto Equivalencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Equivalencia));
			}
			set
			{
				((BindableBase)this).SetProperty<AtributoOpcaoDto>((Expression<Func<AtributoOpcaoDto>>)(() => Equivalencia), value);
			}
		}

		public decimal Medida
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Medida));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Medida), value);
			}
		}

		public bool EquivalenciaValida
		{
			get
			{
				if (ExigeEquivalencia)
				{
					if (Equivalencia != null)
					{
						return !HelperString.IsNullOrEmpty(Equivalencia.Valor);
					}
					return false;
				}
				return true;
			}
		}

		public bool MedidaValida
		{
			get
			{
				if (ExigeMedida)
				{
					return Medida > 0m;
				}
				return true;
			}
		}

		public bool Valido
		{
			get
			{
				if (MedidaValida)
				{
					return EquivalenciaValida;
				}
				return false;
			}
		}
	}
}
namespace Otus.Modulo.AnunciosMercadoLivre.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.AnunciosMercadoLivre.Properties.Resources", typeof(Resources).Assembly);
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

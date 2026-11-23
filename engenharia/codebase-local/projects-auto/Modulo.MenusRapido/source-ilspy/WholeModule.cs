using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Grupos;
using DigisatServer.Application.Main.Dto.Cadastros.MenusRapido;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.MenusRapido.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.MenusRapido")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.MenusRapido")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("e4bd8d71-7249-40ec-b001-b9634bae4476")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.MenusRapido
{
	[Module(ModuleName = "MenuRapidoModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("GrupoMenuRapidoModulo")]
	public class MenuRapidoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public MenuRapidoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<MenusRapidoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MenuRapidoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MenuRapidoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MenuRapidoFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdenarMenuFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.MenusRapido.Views
{
	public class MenuRapidoFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public MenuRapidoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menurapidofichaadicionarview.xaml", UriKind.Relative);
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
	public class MenuRapidoFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public MenuRapidoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menurapidofichaatualizarview.xaml", UriKind.Relative);
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
	public class MenuRapidoListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MenuRapidoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menurapidolistaview.xaml", UriKind.Relative);
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
	public class MenusRapidoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MenusRapidoView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menusrapidoview.xaml", UriKind.Relative);
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
	public class OrdenarMenuFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdenarMenuFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/ordenarmenufichaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.MenusRapido.Views.MenuViewDetalhes
{
	public class MenuView : UserControl, IComponentConnector
	{
		internal ScaleTransform Escala;

		internal ButtonInfo Parameter;

		private bool _contentLoaded;

		public MenuView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menuviewdetalhes/menuview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				Escala = (ScaleTransform)target;
				break;
			case 2:
				Parameter = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class IfoodView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterRemoverTabelaPreco;

		internal ButtonInfo ParameterTabelaPreco;

		private bool _contentLoaded;

		public IfoodView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menuviewdetalhes/ifoodview.xaml", UriKind.Relative);
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
				ParameterRemoverTabelaPreco = (ButtonInfo)target;
				break;
			case 2:
				ParameterTabelaPreco = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class ItensMenuView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ItensMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.MenusRapido;component/views/menuviewdetalhes/itensmenuview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.MenusRapido.ViewModels
{
	public class BonificacoesFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemGrupoMenuViewModel> _itens;

		private readonly NavigationBase _navegar;

		public ViewModelValidationBase FichaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel), value);
			}
		}

		[Valid]
		public Lista<ItemAdicionalMenuRapidoDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens), value);
			}
		}

		public ProdutoServicoEmpresaDto ProdutoEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoEmpresa), value);
			}
		}

		public string DescricaoTela
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoTela));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoTela), value);
			}
		}

		public decimal QuantidadeMinima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima), value);
			}
		}

		public decimal QuantidadeMaxima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima), value);
			}
		}

		public int Minimo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Minimo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Minimo), value);
			}
		}

		public int Maximo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Maximo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Maximo), value);
			}
		}

		public bool FocoNaDescricaoTela
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricaoTela));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricaoTela), value);
			}
		}

		public bool FocoNaBuscaProduto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaProduto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaProduto), value);
			}
		}

		public string InformacaoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa), value);
			}
		}

		public SelecaoCommandParameter<ProdutoServicoEmpresaDto> ProdutoServicoCommandParameter { get; set; }

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand CopiarProdutoEmpresaReferenciaItemCommand { get; set; }

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public BonificacoesFichaViewModel(IEnumerable<ItemGrupoMenuViewModel> itens, ViewModelValidationBase viewmodel, NavigationBase navegar)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			_itens = itens;
			_navegar = navegar;
			FichaViewModel = viewmodel;
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaCima();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima()), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaBaixo();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo()), (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemAdicionalMenuRapidoDto>((Action<ItemAdicionalMenuRapidoDto>)RemoverItem);
			CopiarProdutoEmpresaReferenciaItemCommand = (ICommand)new AsyncCommand<ItemAdicionalMenuRapidoDto>((Func<ItemAdicionalMenuRapidoDto, Task>)CopiarProdutoEmpresaReferenciaItem, (Func<ItemAdicionalMenuRapidoDto, bool>)((ItemAdicionalMenuRapidoDto x) => true), false, (bool?)null);
			PesquisarProdutoServicoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto);
			Itens = new Lista<ItemAdicionalMenuRapidoDto>();
			Itens.Adicionar((IEnumerable<ItemAdicionalMenuRapidoDto>)(from x in itens.SelectMany((ItemGrupoMenuViewModel x) => ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)x).Dto.Bonificacoes)
				orderby x.Ordem
				select x));
			Itens.PosicionarNoPrimeiro();
			Minimo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMinima;
			Maximo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMaxima;
		}

		private void RemoverItem(ItemAdicionalMenuRapidoDto obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
		}

		private Task CopiarProdutoEmpresaReferenciaItem(ItemAdicionalMenuRapidoDto obj)
		{
			return Task.Factory.StartNew(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { obj.ProdutoEmpresaReferencia }))).FirstOrDefault();
					DialogoFactory.MostrarDialogoConfirma($"Código interno: {val.ProdutoServico.CodigoInterno}\n" + $"Descrição no estoque: {val.ProdutoServico.Descricao}\n" + $"Característica: {val.ProdutoServico.Caracteristica}", false, HorizontalAlignment.Left);
				});
			});
		}

		private void Adicionar()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			Itens.Adicionar(new ItemAdicionalMenuRapidoDto
			{
				ProdutoEmpresaReferencia = ((EntityBaseDto)ProdutoEmpresa).Id,
				DescricaoTela = DescricaoTela,
				QuantidadeMinima = QuantidadeMinima,
				QuantidadeMaxima = QuantidadeMaxima
			});
			DescricaoTela = string.Empty;
			InformacaoPesquisa = string.Empty;
			ProdutoEmpresa = null;
			QuantidadeMinima = 0m;
			QuantidadeMaxima = 0m;
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
			FocoNaBuscaProduto = !FocoNaBuscaProduto;
		}

		private bool PodeAdicionar()
		{
			if (!string.IsNullOrWhiteSpace(DescricaoTela))
			{
				return HelperObject.IsNotNull((object)ProdutoEmpresa);
			}
			return false;
		}

		private async void PesquisarProduto()
		{
			try
			{
				string informacao = HelperString.ToStringEmptyWhenNull(InformacaoPesquisa);
				if (informacao.Contains("*"))
				{
					informacao = HelperString.ToStringEmptyWhenNull(informacao.Split('*')[1]);
				}
				string text = ProcessarCodigoDeBarrasService.RetornarCodigoInternoDoCodigoBarras(informacao);
				PesquisaDto objetoPesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaProdutoServicoDto
					{
						Informacao = (HelperObject.IsNotNull((object)text) ? text : informacao)
					},
					Pagina = 1,
					Tamanho = 100
				};
				if (!HelperString.IsNullOrEmpty(informacao))
				{
					ProcessarRetornoPesquisa((await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<PaginaEntityDto>>((Func<IProdutoServicoModuleService, Task<PaginaEntityDto>>)((IProdutoServicoModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisa)))).Lista.OfType<ProdutoServicoEmpresaDto>(), informacao);
				}
				else
				{
					ProcessarRetornoPesquisa(new List<ProdutoServicoEmpresaDto>(), informacao);
				}
			}
			catch (Exception ex)
			{
				InformacaoPesquisa = string.Empty;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void ProcessarRetornoPesquisa(IEnumerable<ProdutoServicoEmpresaDto> resultado, string informacaoPesquisa)
		{
			List<ProdutoServicoEmpresaDto> list = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(resultado);
			if (list.Count == 1)
			{
				SelecionouProdutoServico(list.FirstOrDefault());
				return;
			}
			ProdutoServicoCommandParameter = new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)CallbackSelecaoProdutoServicoCommandParameter, true);
			ProdutoServicoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)informacaoPesquisa);
			_navegar.Navegar("ProdutoServicoListaView", (Action<NavigationResult>)delegate
			{
			}, ProdutoServicoCommandParameter.Parametros);
		}

		protected void CallbackSelecaoProdutoServicoCommandParameter(ProdutoServicoEmpresaDto produtoServico)
		{
			SelecionouProdutoServico(produtoServico);
		}

		protected void SelecionouProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			if ((int)produtoServicoEmpresa.ProdutoServico.Tipo == 5)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto que não seja do tipo 'Combustível'!", false, HorizontalAlignment.Center);
				return;
			}
			if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtoServicoEmpresa.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtoServicoEmpresa.ProdutoEmpresa).Estoque.IsNormal)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto com estoque do tipo 'Normal'!", false, HorizontalAlignment.Center);
				return;
			}
			if (!produtoServicoEmpresa.Vendavel)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto 'Vendável'!", false, HorizontalAlignment.Center);
				return;
			}
			ProdutoEmpresa = produtoServicoEmpresa;
			InformacaoPesquisa = ((BaseDto)produtoServicoEmpresa).ToText;
			DescricaoTela = HelperString.CopiarTexto(ProdutoEmpresa.ProdutoServico.Descricao, 190);
			FocoNaBuscaProduto = false;
			FocoNaDescricaoTela = true;
		}

		public void Gravar()
		{
			foreach (ItemAdicionalMenuRapidoDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			foreach (ItemGrupoMenuViewModel iten2 in _itens)
			{
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.Bonificacoes = HelperIEnumerable.ToListEmptyWhenNull<ItemAdicionalMenuRapidoDto>((IEnumerable<ItemAdicionalMenuRapidoDto>)Itens.Itens);
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.QuantidadeMinima = Minimo;
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.QuantidadeMaxima = Maximo;
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class GrupoComplementoFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemMenuViewModel> _itens;

		private readonly EventHandler<ViewModelValidationBase> _alterouFicha;

		private readonly MenuRapidoDto _dto;

		private readonly NavigationBase _navegar;

		public ViewModelValidationBase FichaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel), value);
			}
		}

		[Valid]
		public Lista<ItemGrupoMenuViewModel> Grupos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemGrupoMenuViewModel>>((Expression<Func<Lista<ItemGrupoMenuViewModel>>>)(() => Grupos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemGrupoMenuViewModel>>((Expression<Func<Lista<ItemGrupoMenuViewModel>>>)(() => Grupos), value, (Action)PreencherGruposItens);
			}
		}

		public IEnumerable<GrupoMenuRapidoDto> GruposMenu
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<GrupoMenuRapidoDto>>((Expression<Func<IEnumerable<GrupoMenuRapidoDto>>>)(() => GruposMenu));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<GrupoMenuRapidoDto>>((Expression<Func<IEnumerable<GrupoMenuRapidoDto>>>)(() => GruposMenu), value);
			}
		}

		public decimal QuantidadeMinima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima), value);
			}
		}

		public decimal QuantidadeMaxima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima), value);
			}
		}

		public string DescricaoGrupo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoGrupo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoGrupo), value, (Action)delegate
				{
					if (HelperIEnumerable.IsEmpty<char>((IEnumerable<char>)DescricaoGrupo))
					{
						GruposMenu = new List<GrupoMenuRapidoDto>();
					}
				});
			}
		}

		public SelecaoCommandParameter<IEnumerable<GrupoMenuRapidoDto>> GrupoCommandParameter { get; set; }

		public bool FocoNaBuscaGrupo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaGrupo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaGrupo), value);
			}
		}

		public bool FocoAdicionarGrupo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoAdicionarGrupo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoAdicionarGrupo), value);
			}
		}

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand PredefinicoesCommand { get; set; }

		public ICommand AdicionaisCommand { get; set; }

		public ICommand BonificacoesCommand { get; set; }

		public ICommand PesquisarGrupoCommand { get; set; }

		public ICommand RemoverGrupoCommand { get; set; }

		public GrupoComplementoFichaViewModel(MenuRapidoDto dto, IEnumerable<ItemMenuViewModel> itens, EventHandler<ViewModelValidationBase> alterouFicha, NavigationBase navegar)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Expected O, but got Unknown
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Expected O, but got Unknown
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Expected O, but got Unknown
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Expected O, but got Unknown
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Expected O, but got Unknown
			_itens = itens;
			_alterouFicha = alterouFicha;
			_dto = dto;
			_navegar = navegar;
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)MoverParaCima, (Func<bool>)(() => HelperObject.IsNotNull((object)Grupos) && Grupos.PodeMoverParaCima() && Grupos.Itens.Count((ItemGrupoMenuViewModel x) => x.Selecionado) == 1), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)MoverParaBaixo, (Func<bool>)(() => HelperObject.IsNotNull((object)Grupos) && Grupos.PodeMoverParaBaixo() && Grupos.Itens.Count((ItemGrupoMenuViewModel x) => x.Selecionado) == 1), (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemGrupoMenuViewModel>((Action<ItemGrupoMenuViewModel>)RemoverItem);
			PredefinicoesCommand = (ICommand)new DelegateCommand((Action)AdicionarObservacoes, (Func<bool>)PodeAdicionarPredefinicao, (bool?)null);
			AdicionaisCommand = (ICommand)new DelegateCommand((Action)AdicionarAdicionais, (Func<bool>)PodeAdicionarAdicional, (bool?)null);
			BonificacoesCommand = (ICommand)new DelegateCommand((Action)AdicionarBonificacoes, (Func<bool>)PodeAdicionarBonificacao, (bool?)null);
			PesquisarGrupoCommand = (ICommand)new DelegateCommand((Action)PesquisarGrupoMenu);
			List<GrupoComplementoMenuRapidoDto> source = HelperIEnumerable.ToListEmptyWhenNull<GrupoComplementoMenuRapidoDto>((IEnumerable<GrupoComplementoMenuRapidoDto>)(from x in _itens.SelectMany((ItemMenuViewModel x) => ((ViewModelDtoBase<ItemMenuRapidoDto>)x).Dto.GruposMenuRapido)
				orderby x.Ordem
				select x));
			Grupos = new Lista<ItemGrupoMenuViewModel>();
			Grupos.Adicionar(source.Select((GrupoComplementoMenuRapidoDto y) => new ItemGrupoMenuViewModel(y, MudouItemSelecionado)));
			((BindableBase)this).RaisePropertyChanged<Lista<ItemGrupoMenuViewModel>>((Expression<Func<Lista<ItemGrupoMenuViewModel>>>)(() => Grupos));
			Grupos.PosicionarNoPrimeiro();
			FichaViewModel = (ViewModelValidationBase)(object)this;
			_alterouFicha(null, FichaViewModel);
		}

		public bool PodeAdicionarAdicional()
		{
			if (HelperObject.IsNotNull((object)Grupos) && Grupos.Itens.Any((ItemGrupoMenuViewModel x) => x.Selecionado))
			{
				return Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado).All((ItemGrupoMenuViewModel t) => (int)((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)t).Dto.Grupo.TipoGrupo == 0);
			}
			return false;
		}

		public bool PodeAdicionarPredefinicao()
		{
			if (HelperObject.IsNotNull((object)Grupos) && Grupos.Itens.Any((ItemGrupoMenuViewModel x) => x.Selecionado))
			{
				return Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado).All((ItemGrupoMenuViewModel t) => (int)((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)t).Dto.Grupo.TipoGrupo == 1);
			}
			return false;
		}

		public bool PodeAdicionarBonificacao()
		{
			if (HelperObject.IsNotNull((object)Grupos) && Grupos.Itens.Any((ItemGrupoMenuViewModel x) => x.Selecionado))
			{
				return Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado).All((ItemGrupoMenuViewModel t) => (int)((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)t).Dto.Grupo.TipoGrupo == 2);
			}
			return false;
		}

		private async void PesquisarGrupoMenu()
		{
			try
			{
				PaginaEntityDto val = new PaginaEntityDto();
				if (HelperString.IsNullOrEmpty(DescricaoGrupo))
				{
					ProcessarRetornoPesquisa(val.Lista.OfType<GrupoMenuRapidoDto>());
					return;
				}
				PesquisaDto objetoPesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaGrupoMenuDto
					{
						Informacao = HelperString.ToStringEmptyWhenNull(DescricaoGrupo)
					},
					Pagina = 1,
					Tamanho = 2
				};
				ProcessarRetornoPesquisa((await DadosSistema.GetService().Canal<IGrupoMenuModuleService, Task<PaginaEntityDto>>((Func<IGrupoMenuModuleService, Task<PaginaEntityDto>>)((IGrupoMenuModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisa)))).Lista.OfType<GrupoMenuRapidoDto>());
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void ProcessarRetornoPesquisa(IEnumerable<GrupoMenuRapidoDto> resultado)
		{
			List<GrupoMenuRapidoDto> list = HelperIEnumerable.ToListEmptyWhenNull<GrupoMenuRapidoDto>(resultado);
			if (list.Count == 1)
			{
				CallbackGrupo(new List<GrupoMenuRapidoDto> { list.First() });
				return;
			}
			GrupoCommandParameter = new SelecaoCommandParameter<IEnumerable<GrupoMenuRapidoDto>>("GrupoMenuListaView", (Action<IEnumerable<GrupoMenuRapidoDto>>)delegate(IEnumerable<GrupoMenuRapidoDto> grupos)
			{
				CallbackGrupo(grupos);
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoGrupo));
			}, true);
			GrupoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)DescricaoGrupo);
			_navegar.Navegar("GrupoMenuListaView", (Action<NavigationResult>)delegate
			{
			}, GrupoCommandParameter.Parametros);
		}

		private void CallbackGrupo(IEnumerable<GrupoMenuRapidoDto> grupos)
		{
			Lista<GrupoMenuRapidoDto> val = new Lista<GrupoMenuRapidoDto>(grupos);
			GruposMenu = grupos;
			object obj;
			if (val.Numero <= 1)
			{
				if (val == null)
				{
					obj = null;
				}
				else
				{
					GrupoMenuRapidoDto itemSelecionado = val.ItemSelecionado;
					obj = ((itemSelecionado != null) ? itemSelecionado.Descricao : null);
				}
				if (obj == null)
				{
					obj = string.Empty;
				}
			}
			else
			{
				obj = $"{val.Numero} Grupos selecionados";
			}
			DescricaoGrupo = (string)obj;
			FocoNaBuscaGrupo = false;
			FocoAdicionarGrupo = true;
		}

		private void MoverParaCima()
		{
			ItemGrupoMenuViewModel itemSelecionado = Grupos.Itens.FirstOrDefault((ItemGrupoMenuViewModel x) => x.Selecionado);
			Grupos.ItemSelecionado = itemSelecionado;
			Grupos.MoverParaCima();
			Grupos.ItemSelecionado = itemSelecionado;
		}

		private void MoverParaBaixo()
		{
			ItemGrupoMenuViewModel itemSelecionado = Grupos.Itens.FirstOrDefault((ItemGrupoMenuViewModel x) => x.Selecionado);
			Grupos.ItemSelecionado = itemSelecionado;
			Grupos.MoverParaBaixo();
			Grupos.ItemSelecionado = itemSelecionado;
		}

		private void AdicionarObservacoes()
		{
			if (Grupos.Itens.Count((ItemGrupoMenuViewModel x) => x.Selecionado) > 1)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione apenas um grupo para acessar as predefinições.", false, HorizontalAlignment.Center);
				return;
			}
			FichaViewModel = (ViewModelValidationBase)(object)new PredefinicoesFichaViewModel(_dto, HelperIEnumerable.ToListEmptyWhenNull<ItemGrupoMenuViewModel>(Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado)), (ViewModelValidationBase)(object)this);
			_alterouFicha(null, FichaViewModel);
		}

		private void AdicionarAdicionais()
		{
			if (Grupos.Itens.Count((ItemGrupoMenuViewModel x) => x.Selecionado) > 1)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione apenas um grupo para acessar os adicionais.", false, HorizontalAlignment.Center);
				return;
			}
			FichaViewModel = (ViewModelValidationBase)(object)new AdicionaisFichaViewModel(HelperIEnumerable.ToListEmptyWhenNull<ItemGrupoMenuViewModel>(Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado)), (ViewModelValidationBase)(object)this, _navegar);
			_alterouFicha(null, FichaViewModel);
		}

		private void AdicionarBonificacoes()
		{
			if (Grupos.Itens.Count((ItemGrupoMenuViewModel x) => x.Selecionado) > 1)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione apenas um grupo para acessar as bonificações.", false, HorizontalAlignment.Center);
				return;
			}
			FichaViewModel = (ViewModelValidationBase)(object)new BonificacoesFichaViewModel(HelperIEnumerable.ToListEmptyWhenNull<ItemGrupoMenuViewModel>(Grupos.Itens.Where((ItemGrupoMenuViewModel x) => x.Selecionado)), (ViewModelValidationBase)(object)this, _navegar);
			_alterouFicha(null, FichaViewModel);
		}

		private void RemoverItem(ItemGrupoMenuViewModel obj)
		{
			Grupos.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemGrupoMenuViewModel>>((Expression<Func<Lista<ItemGrupoMenuViewModel>>>)(() => Grupos));
		}

		private void Adicionar()
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Expected O, but got Unknown
			foreach (GrupoMenuRapidoDto item in GruposMenu)
			{
				Grupos.Adicionar(new ItemGrupoMenuViewModel(new GrupoComplementoMenuRapidoDto
				{
					Grupo = item,
					Referencia = HelperString.RemoverPontoBarraHifen(Guid.NewGuid().ToString()),
					Adicionais = item.Adicionais,
					PredefinicoesSelecionadas = item.PredefinicoesSelecionadas,
					PredefinicoesCadastradas = item.PredefinicoesCadastradas,
					Bonificacoes = item.Bonificacoes,
					ValorBeneficio = item.ValorBonificacaoPorItem,
					QuantidadeMinima = item.QuantidadeMinima,
					QuantidadeMaxima = item.QuantidadeMaxima,
					UtilizarItemMaiorValor = item.UtilizarItemMaiorValor,
					TabelaPreco = item.TabelaPreco
				}, MudouItemSelecionado));
			}
			GruposMenu = null;
			DescricaoGrupo = string.Empty;
			((BindableBase)this).RaisePropertyChanged<Lista<ItemGrupoMenuViewModel>>((Expression<Func<Lista<ItemGrupoMenuViewModel>>>)(() => Grupos));
			FocoNaBuscaGrupo = true;
			FocoAdicionarGrupo = false;
		}

		private void MudouItemSelecionado()
		{
			Grupos.ItemSelecionado = Grupos.Itens.FirstOrDefault((ItemGrupoMenuViewModel x) => x.Selecionado);
		}

		private bool PodeAdicionar()
		{
			if (HelperObject.IsNotNull((object)GruposMenu))
			{
				return !HelperString.IsNullOrEmpty(DescricaoGrupo);
			}
			return false;
		}

		public void Gravar()
		{
			foreach (ItemGrupoMenuViewModel iten in Grupos.Itens)
			{
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.Ordem = Grupos.Indice(iten) + 1;
			}
			PreencherGruposItens();
		}

		public void PreencherGruposItens()
		{
			foreach (ItemMenuViewModel iten in _itens)
			{
				((ViewModelDtoBase<ItemMenuRapidoDto>)iten).Dto.GruposMenuRapido = Grupos.Itens.Select((ItemGrupoMenuViewModel x) => ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)x).Dto);
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Grupos).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AdicionaisFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemGrupoMenuViewModel> _itens;

		private readonly NavigationBase _navegar;

		public ViewModelValidationBase FichaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel), value);
			}
		}

		[Valid]
		public Lista<ItemAdicionalMenuRapidoDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens), value);
			}
		}

		public ProdutoServicoEmpresaDto ProdutoEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoEmpresa), value);
			}
		}

		public string DescricaoTela
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoTela));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoTela), value);
			}
		}

		public decimal QuantidadeMinima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMinima), value);
			}
		}

		public decimal QuantidadeMaxima
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeMaxima), value);
			}
		}

		public int Minimo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Minimo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Minimo), value);
			}
		}

		public int Maximo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Maximo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Maximo), value);
			}
		}

		public bool FocoNaDescricaoTela
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricaoTela));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricaoTela), value);
			}
		}

		public bool FocoNaBuscaProduto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaProduto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBuscaProduto), value);
			}
		}

		public string InformacaoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa), value);
			}
		}

		public bool UtilizarItemMaiorValor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarItemMaiorValor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarItemMaiorValor), value);
			}
		}

		public SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>> ProdutoServicoCommandParameter { get; set; }

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand CopiarProdutoEmpresaReferenciaItemCommand { get; set; }

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public AdicionaisFichaViewModel(IEnumerable<ItemGrupoMenuViewModel> itens, ViewModelValidationBase viewmodel, NavigationBase navegar)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			FichaViewModel = viewmodel;
			_itens = itens;
			_navegar = navegar;
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaCima();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima()), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaBaixo();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo()), (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemAdicionalMenuRapidoDto>((Action<ItemAdicionalMenuRapidoDto>)RemoverItem);
			CopiarProdutoEmpresaReferenciaItemCommand = (ICommand)new AsyncCommand<ItemAdicionalMenuRapidoDto>((Func<ItemAdicionalMenuRapidoDto, Task>)CopiarProdutoEmpresaReferenciaItem, (Func<ItemAdicionalMenuRapidoDto, bool>)((ItemAdicionalMenuRapidoDto x) => true), false, (bool?)null);
			PesquisarProdutoServicoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto);
			Itens = new Lista<ItemAdicionalMenuRapidoDto>();
			Itens.Adicionar((IEnumerable<ItemAdicionalMenuRapidoDto>)(from x in itens.SelectMany((ItemGrupoMenuViewModel x) => ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)x).Dto.Adicionais)
				orderby x.Ordem
				select x));
			Itens.PosicionarNoPrimeiro();
			Minimo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMinima;
			Maximo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMaxima;
			UtilizarItemMaiorValor = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.UtilizarItemMaiorValor;
		}

		private void RemoverItem(ItemAdicionalMenuRapidoDto obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
		}

		private Task CopiarProdutoEmpresaReferenciaItem(ItemAdicionalMenuRapidoDto obj)
		{
			return Task.Factory.StartNew(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { obj.ProdutoEmpresaReferencia }))).FirstOrDefault();
					DialogoFactory.MostrarDialogoConfirma($"Código interno: {val.ProdutoServico.CodigoInterno}\n" + $"Descrição no estoque: {val.ProdutoServico.Descricao}\n" + $"Característica: {val.ProdutoServico.Caracteristica}", false, HorizontalAlignment.Left);
				});
			});
		}

		private void Adicionar()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			Itens.Adicionar(new ItemAdicionalMenuRapidoDto
			{
				ProdutoEmpresaReferencia = ((EntityBaseDto)ProdutoEmpresa).Id,
				DescricaoTela = DescricaoTela,
				QuantidadeMinima = QuantidadeMinima,
				QuantidadeMaxima = QuantidadeMaxima
			});
			DescricaoTela = string.Empty;
			InformacaoPesquisa = string.Empty;
			ProdutoEmpresa = null;
			QuantidadeMinima = 0m;
			QuantidadeMaxima = 0m;
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
			FocoNaBuscaProduto = !FocoNaBuscaProduto;
		}

		private bool PodeAdicionar()
		{
			if (!string.IsNullOrWhiteSpace(DescricaoTela))
			{
				return HelperObject.IsNotNull((object)ProdutoEmpresa);
			}
			return false;
		}

		private async void PesquisarProduto()
		{
			try
			{
				string informacao = HelperString.ToStringEmptyWhenNull(InformacaoPesquisa);
				if (informacao.Contains("*"))
				{
					informacao = HelperString.ToStringEmptyWhenNull(informacao.Split('*')[1]);
				}
				string text = ProcessarCodigoDeBarrasService.RetornarCodigoInternoDoCodigoBarras(informacao);
				PesquisaDto objetoPesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaProdutoServicoDto
					{
						Informacao = (HelperObject.IsNotNull((object)text) ? text : informacao)
					},
					Pagina = 1,
					Tamanho = 100
				};
				if (!HelperString.IsNullOrEmpty(informacao))
				{
					ProcessarRetornoPesquisa((await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<PaginaEntityDto>>((Func<IProdutoServicoModuleService, Task<PaginaEntityDto>>)((IProdutoServicoModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisa)))).Lista.OfType<ProdutoServicoEmpresaDto>(), informacao);
				}
				else
				{
					ProcessarRetornoPesquisa(new List<ProdutoServicoEmpresaDto>(), informacao);
				}
			}
			catch (Exception ex)
			{
				InformacaoPesquisa = string.Empty;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void ProcessarRetornoPesquisa(IEnumerable<ProdutoServicoEmpresaDto> resultado, string informacaoPesquisa)
		{
			List<ProdutoServicoEmpresaDto> list = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(resultado);
			if (list.Count == 1)
			{
				SelecionouProdutoServico(list.FirstOrDefault());
				return;
			}
			ProdutoServicoCommandParameter = new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)CallbackSelecaoProdutoServicoCommandParameter, true);
			ProdutoServicoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)informacaoPesquisa);
			_navegar.Navegar("ProdutoServicoListaView", (Action<NavigationResult>)delegate
			{
			}, ProdutoServicoCommandParameter.Parametros);
		}

		protected void CallbackSelecaoProdutoServicoCommandParameter(IEnumerable<ProdutoServicoEmpresaDto> produtosServicos)
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Invalid comparison between Unknown and I4
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			if (produtosServicos.Count() > 1)
			{
				Lista<ItemAdicionalMenuRapidoDto> val = new Lista<ItemAdicionalMenuRapidoDto>();
				foreach (ProdutoServicoEmpresaDto produtosServico in produtosServicos)
				{
					try
					{
						if ((int)produtosServico.ProdutoServico.Tipo == 5)
						{
							throw new Exception("Selecione um produto que não seja do tipo 'Combustível'!");
						}
						if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtosServico.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtosServico.ProdutoEmpresa).Estoque.IsNormal)
						{
							throw new Exception("Selecione um produto com estoque do tipo 'Normal'!");
						}
						if (!produtosServico.Vendavel)
						{
							throw new Exception("Selecione um produto 'Vendável'!");
						}
						val.Adicionar(new ItemAdicionalMenuRapidoDto
						{
							ProdutoEmpresaReferencia = ((EntityBaseDto)produtosServico).Id,
							DescricaoTela = produtosServico.ProdutoServico.Descricao,
							QuantidadeMinima = QuantidadeMinima,
							QuantidadeMaxima = QuantidadeMaxima
						});
					}
					catch (Exception ex)
					{
						HelperException.ShowLastException(ex, "", false, false);
					}
				}
				Itens.Adicionar((IEnumerable<ItemAdicionalMenuRapidoDto>)val.Itens);
				DescricaoTela = string.Empty;
				InformacaoPesquisa = string.Empty;
				ProdutoEmpresa = null;
				QuantidadeMinima = 0m;
				QuantidadeMaxima = 0m;
				((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
				FocoNaBuscaProduto = !FocoNaBuscaProduto;
			}
			else
			{
				SelecionouProdutoServico(produtosServicos.FirstOrDefault());
			}
		}

		protected void SelecionouProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			if ((int)produtoServicoEmpresa.ProdutoServico.Tipo == 5)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto que não seja do tipo 'Combustível'!", false, HorizontalAlignment.Center);
				return;
			}
			if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtoServicoEmpresa.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtoServicoEmpresa.ProdutoEmpresa).Estoque.IsNormal)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto com estoque do tipo 'Normal'!", false, HorizontalAlignment.Center);
				return;
			}
			if (!produtoServicoEmpresa.Vendavel)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto 'Vendável'!", false, HorizontalAlignment.Center);
				return;
			}
			ProdutoEmpresa = produtoServicoEmpresa;
			InformacaoPesquisa = ((BaseDto)produtoServicoEmpresa).ToText;
			DescricaoTela = HelperString.CopiarTexto(ProdutoEmpresa.ProdutoServico.Descricao, 190);
			FocoNaBuscaProduto = false;
			FocoNaDescricaoTela = true;
		}

		public void Gravar()
		{
			foreach (ItemAdicionalMenuRapidoDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			foreach (ItemGrupoMenuViewModel iten2 in _itens)
			{
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.Adicionais = HelperIEnumerable.ToListEmptyWhenNull<ItemAdicionalMenuRapidoDto>((IEnumerable<ItemAdicionalMenuRapidoDto>)Itens.Itens);
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.QuantidadeMinima = Minimo;
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.QuantidadeMaxima = Maximo;
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten2).Dto.UtilizarItemMaiorValor = UtilizarItemMaiorValor;
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class MenuRapidoAbaViewModel : AbaPrincipalViewModelBase
	{
		public MenuRapidoAbaViewModel(IViewModelPrincipal viewModel)
			: base("Menu", "Dados principais", viewModel)
		{
		}
	}
	public class MenuRapidoFichaAdicionarViewModel : FichaBaseAdicionar<MenuRapidoDto, MenuRapidoViewModel, IMenuRapidoModuleService>
	{
		private MenuRapidoViewModel _viewModel;

		public MenuRapidoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			_viewModel = navigationParameters[typeof(MenuRapidoViewModel).Name] as MenuRapidoViewModel;
			((FichaBasePersistir<MenuRapidoDto, MenuRapidoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Formulario = (IFormulario)(object)new MenuRapidoFormulario(((ViewModelDtoBase<MenuRapidoDto>)(object)_viewModel).Dto, (NavigationBase)(object)this);
		}
	}
	public class MenuRapidoFichaAtualizarViewModel : FichaBaseAtualizar<MenuRapidoDto, MenuRapidoViewModel, IMenuRapidoModuleService>
	{
		private MenuRapidoViewModel _viewModel;

		public MenuRapidoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			_viewModel = navigationParameters[typeof(MenuRapidoViewModel).Name] as MenuRapidoViewModel;
			((FichaBasePersistir<MenuRapidoDto, MenuRapidoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Formulario = (IFormulario)(object)new MenuRapidoFormulario(((ViewModelDtoBase<MenuRapidoDto>)(object)_viewModel).Dto, (NavigationBase)(object)this);
		}
	}
	public class MenuRapidoFormulario : FormularioEntity<MenuRapidoDto>
	{
		private MenuRapidoViewModel _menuViewModel;

		private readonly NavigationBase _navigationBase;

		public override bool Valido
		{
			get
			{
				if (((Formulario)this).Valido)
				{
					ItensMenuViewModel itensMenuViewModel = HelperObject.To<ItensMenuViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
					if (itensMenuViewModel == null)
					{
						return true;
					}
					return HelperObject.IsNull((object)itensMenuViewModel.FichaViewModel);
				}
				return false;
			}
		}

		public MenuRapidoFormulario(MenuRapidoDto dto, NavigationBase navigationBase)
			: base(dto)
		{
			_navigationBase = navigationBase;
			AdicionarAbas();
		}

		private void AdicionarAbas()
		{
			_menuViewModel = new MenuRapidoViewModel(base.Dto, _navigationBase);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new MenuRapidoAbaViewModel((IViewModelPrincipal)(object)_menuViewModel),
				(IAbaViewModelBase)new ItensMenuAbaViewModel((IViewModelDtoBase)(object)new ItensMenuViewModel(base.Dto, _navigationBase))
			});
			if (base.Dto.MostrarDeliveryOnline && DadosSistema.IfoodDisponivel)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new IfoodAbaViewModel((IViewModelDtoBase)(object)new IfoodViewModel(base.Dto)) });
			}
			_menuViewModel.AlterouUsaDeliveryOnline += MenuViewModelOnAlterouUsaDeliveryOnline;
		}

		private void MenuViewModelOnAlterouUsaDeliveryOnline(object sender, bool usaDeliveryOnline)
		{
			if (usaDeliveryOnline)
			{
				AdicionarAbaIfood();
			}
			else
			{
				RemoverAbaIfood();
			}
		}

		private void AdicionarAbaIfood()
		{
			if (!((Formulario)this).Abas.Any((IAbaViewModelBase aba) => aba is IfoodAbaViewModel))
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new IfoodAbaViewModel((IViewModelDtoBase)(object)new IfoodViewModel(base.Dto)) });
			}
		}

		private void RemoverAbaIfood()
		{
			if (((Formulario)this).Abas.Any((IAbaViewModelBase aba) => aba is IfoodAbaViewModel))
			{
				((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase aba) => aba is IfoodAbaViewModel));
			}
		}

		public override void Dispose()
		{
			_menuViewModel.AlterouUsaDeliveryOnline -= MenuViewModelOnAlterouUsaDeliveryOnline;
		}
	}
	public class MenuRapidoListaViewModel : ListaSelecaoReferencia<MenuRapidoDto, MenuRapidoViewModel, IMenuRapidoModuleService, MenuRapido>
	{
		public ICommand OrdenarMenuCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand EnviaIfoodCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public ICommand MostrarSomenteMenusSelecionadosNesseLocalCommand { get; set; }

		public ICommand MostrarTodosOsMenusNesseLocalCommand { get; set; }

		public MenuRapidoListaViewModel(ISessao sessao, IWService service, IEntityAtivos<MenuRapidoDto, IMenuRapidoModuleService> ativos, IEntityInativos<MenuRapidoDto, IMenuRapidoModuleService> inativos)
			: base(sessao, typeof(MenuRapidoFichaAdicionarView).Name, typeof(MenuRapidoFichaAtualizarView).Name, service, new IEntityBase<MenuRapidoDto, IMenuRapidoModuleService>[2]
			{
				(IEntityBase<MenuRapidoDto, IMenuRapidoModuleService>)(object)ativos,
				(IEntityBase<MenuRapidoDto, IMenuRapidoModuleService>)(object)inativos
			})
		{
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Expected O, but got Unknown
			OrdenarMenuCommand = (ICommand)new DelegateCommand((Action)OrdenarMenu, (Func<bool>)PodeOrdenarMenu, (bool?)null);
			EnviaIfoodCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarIfood, (Func<bool>)PodeEnviarIfood, false, (bool?)null);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			HelperObject.To<OpcaoPaginacao>((object)((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Pesquisa.Contexto).SetarRegistrosPorPagina((short)100);
			DuplicarCommand = (ICommand)new DelegateCommand((Action)Duplicar, (Func<bool>)PodeDuplicar, (bool?)null);
			MostrarSomenteMenusSelecionadosNesseLocalCommand = (ICommand)new DelegateCommand((Action)MostrarMenuLocal, (Func<bool>)PodeMostrarCardapioLocal, (bool?)null);
			MostrarTodosOsMenusNesseLocalCommand = (ICommand)new DelegateCommand((Action)MostrarTodosOsMenusNesseLocal, (Func<bool>)PodeMostrarTodosOsMenusNesseLocal, (bool?)null);
		}

		private void Visualizar()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)162);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private bool PodeVisualizar()
		{
			if (((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private bool PodeOrdenarMenu()
		{
			if (((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens != null)
			{
				return ((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, Atualizar>();
			}
			return false;
		}

		private void OrdenarMenu()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			((NavigationBase)this).Navegar("OrdenarMenuFichaView", (Action<NavigationResult>)delegate
			{
			}, new NavigationParameters());
		}

		private Task EnviarIfood()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			return ((ListaReferenciaBase<MenuRapidoDto, MenuRapidoViewModel, IMenuRapidoModuleService, MenuRapido>)(object)this)._service.Canal<IMenuRapidoModuleService, Task>((Func<IMenuRapidoModuleService, Task>)((IMenuRapidoModuleService service) => service.EnviarMenuIfoodAsync(((EntityBaseDto)((ViewModelDtoBase<MenuRapidoDto>)(object)((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task task)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.EsconderLoader();
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						DialogoFactory.MostrarDialogoConfirma("Concluído!", false, HorizontalAlignment.Center);
					}
				});
			});
		}

		private bool PodeEnviarIfood()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens) && ((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<MenuRapidoDto>)(object)((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens.Selecionado).Dto.MostrarDeliveryOnline && DadosSistema.IfoodDisponivel)
			{
				return ((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, Atualizar>();
			}
			return false;
		}

		protected async void Duplicar()
		{
			try
			{
				MenuRapidoDto val = await ((ListaReferenciaBase<MenuRapidoDto, MenuRapidoViewModel, IMenuRapidoModuleService, MenuRapido>)(object)this)._service.Canal<IMenuRapidoModuleService, Task<MenuRapidoDto>>((Func<IMenuRapidoModuleService, Task<MenuRapidoDto>>)((IMenuRapidoModuleService service) => ((IReferenciaAtualizarService<MenuRapidoDto>)(object)service).AtualizarDtoAsync(((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens.ObterDtoItemSelecionado())));
				val.LimparId();
				List<ItemMenuRapidoDto> list = HelperIEnumerable.ToListEmptyWhenNull<ItemMenuRapidoDto>(val.ItensMenuRapido);
				foreach (ItemMenuRapidoDto item in list)
				{
					item.LimparId();
				}
				val.ItensMenuRapido = new List<ItemMenuRapidoDto>(list);
				MenuRapidoViewModel menuRapidoViewModel = new MenuRapidoViewModel(val, (NavigationBase)(object)this);
				((ViewModelBase<MenuRapidoDto>)(object)menuRapidoViewModel).ProjectedAsViewModel();
				MenuRapidoListaViewModel menuRapidoListaViewModel = this;
				string viewAdicionarFicha = ((ListaManutencaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).ViewAdicionarFicha;
				Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
				{
					if (navigationResult.Result == true)
					{
						((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens.Estado.AlterarAdicao();
					}
				};
				NavigationParameters val2 = new NavigationParameters();
				val2.Add(typeof(MenuRapidoViewModel).Name, (object)menuRapidoViewModel);
				((NavigationBase)menuRapidoListaViewModel).Navegar(viewAdicionarFicha, obj, val2);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeDuplicar()
		{
			return ((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, Adicionar>();
		}

		private void MostrarMenuLocal()
		{
			ConfiguracaoMenusRapidosLocal.Redefinir();
			List<MenuRapidoLocal> list = HelperIEnumerable.ToListEmptyWhenNull<MenuRapidoLocal>(base.ItensSelecionados.Select((MenuRapidoDto x) => MenuRapidoLocal.Criar().ComReferencia(((EntityBaseDto)x).Id)));
			ConfiguracaoMenusRapidosLocal.Criar().ComMenusRapidos(list).Salvar();
			DialogoFactory.MostrarDialogoConfirma("O sistema deve ser reiniciado para assumir a configuração.", false, HorizontalAlignment.Center);
		}

		private bool PodeMostrarCardapioLocal()
		{
			if (((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens != null && base.ItensSelecionados.Any())
			{
				return ((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, Atualizar>();
			}
			return false;
		}

		private void MostrarTodosOsMenusNesseLocal()
		{
			ConfiguracaoMenusRapidosLocal.Redefinir();
			DialogoFactory.MostrarDialogoConfirma("O sistema deve ser reiniciado para assumir a configuração.", false, HorizontalAlignment.Center);
		}

		private bool PodeMostrarTodosOsMenusNesseLocal()
		{
			if (((ListaBase<MenuRapidoDto, MenuRapidoViewModel>)(object)this).Itens != null && !base.ItensSelecionados.Any())
			{
				return ((ListaSelecaoBase<MenuRapidoDto, MenuRapidoViewModel, MenuRapido>)(object)this).Sessao.VerificarPermissao<MenuRapido, Atualizar>();
			}
			return false;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			base.OnParametroInformado(sender, navigationParameters);
			base.CheckBoxVisibility = Visibility.Visible;
		}
	}
	public class MenusRapidoViewModel : ModuloBase
	{
		public MenusRapidoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloMenuRegion, "MenuRapidoListaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class MenuRapidoViewModel : ViewModelAtivoBase<MenuRapidoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
		private readonly NavigationBase _navigation;

		[Required(ErrorMessage = "O campo 'Descrição' é obrigatório", AllowEmptyStrings = false)]
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

		public bool MostrarPedirMenu
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarPedirMenu));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarPedirMenu), value);
			}
		}

		public ImpressoraDto Impressora
		{
			get
			{
				return ((BindableBase)this).GetProperty<ImpressoraDto>((Expression<Func<ImpressoraDto>>)(() => Impressora));
			}
			set
			{
				((BindableBase)this).SetProperty<ImpressoraDto>((Expression<Func<ImpressoraDto>>)(() => Impressora), value);
			}
		}

		public BitmapSource Imagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Imagem));
			}
			set
			{
				((BindableBase)this).SetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Imagem), value);
			}
		}

		public bool MostrarDeliveryOnline
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarDeliveryOnline));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarDeliveryOnline), value, (Action)delegate
				{
					OnAlterouUsaDeliveryOnline(value);
				});
			}
		}

		public bool IfoodConfigurado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IfoodConfigurado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IfoodConfigurado), value);
			}
		}

		public SelecaoCommandParameter<ImpressoraDto> ImpressoraCommandParameter => new SelecaoCommandParameter<ImpressoraDto>("ImpressoraListaView", (Action<ImpressoraDto>)delegate(ImpressoraDto impressora)
		{
			Impressora = impressora;
		}, true);

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value);
			}
		}

		public string Caracteristica
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Caracteristica));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Caracteristica), value);
			}
		}

		public ICommand RemoverImpressoraCommand { get; set; }

		public event EventHandler<bool> AlterouUsaDeliveryOnline;

		public MenuRapidoViewModel()
		{
		}

		public MenuRapidoViewModel(MenuRapidoDto dto)
			: base(dto)
		{
		}

		public MenuRapidoViewModel(MenuRapidoDto menuDto, NavigationBase navigation)
			: base(menuDto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			_navigation = navigation;
			RemoverImpressoraCommand = (ICommand)new DelegateCommand((Action)RemoverImpressora, (Func<bool>)PodeRemoverImpressora, (bool?)null);
		}

		private void RemoverImpressora()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Impressora = (ImpressoraDto)new ImpressoraDtoNull();
		}

		private bool PodeRemoverImpressora()
		{
			return HelperObject.IsExactlyObject<ImpressoraDto>((object)Impressora);
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}

		public override void ProjectedAsViewModel()
		{
			Descricao = ((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.Descricao;
			Impressora = ((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.Impressora;
			Imagem = HelperImage.ToBitmapSource(HelperImage.Decompress(((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.Imagem));
			base.Ativo = ((EntityDto)((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto).Ativo;
			MostrarPedirMenu = ((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.MostrarPedirMenu;
			MostrarDeliveryOnline = ((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.MostrarDeliveryOnline;
			IfoodConfigurado = !string.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Saida.Ifood.AccessToken.Valor) && DadosSistema.IfoodDisponivel;
			Caracteristica = HelperString.ToStringEmptyWhenNull(((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto.Caracteristica);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			MenuRapidoDto val = (MenuRapidoDto)((obj is MenuRapidoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Descricao = Descricao;
				((EntityDto)val).Ativo = base.Ativo;
				val.Imagem = HelperImage.Compress(Imagem);
				val.Impressora = Impressora;
				val.MostrarPedirMenu = MostrarPedirMenu;
				val.MostrarDeliveryOnline = MostrarDeliveryOnline;
				val.Caracteristica = Caracteristica;
				((ViewModelDtoBase<MenuRapidoDto>)(object)this).AtualizarDto<MenuRapidoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new MenuRapidoFormulario(((ViewModelDtoBase<MenuRapidoDto>)(object)this).Dto, _navigation);
		}

		public string ToTituloLista()
		{
			return "Menus";
		}

		public string ToTituloFicha()
		{
			return "Menu";
		}

		protected virtual void OnAlterouUsaDeliveryOnline(bool e)
		{
			this.AlterouUsaDeliveryOnline?.Invoke(this, e);
		}
	}
	public class ComponentesFichaViewModel : ViewModelValidationBase
	{
		private readonly ItemMenuViewModel _item;

		public Lista<ComponenteDto> Componentes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ComponenteDto>>((Expression<Func<Lista<ComponenteDto>>>)(() => Componentes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ComponenteDto>>((Expression<Func<Lista<ComponenteDto>>>)(() => Componentes), value);
			}
		}

		public ComponentesFichaViewModel(ItemMenuViewModel item, IEnumerable<ComponenteDto> componentes)
		{
			_item = item;
			Componentes = new Lista<ComponenteDto>();
			Componentes.Adicionar(componentes);
		}

		public void Gravar()
		{
			((ViewModelDtoBase<ItemMenuRapidoDto>)_item).Dto.Componentes = Componentes.Itens;
		}

		public bool PodeGravar()
		{
			return !Componentes.Itens.All((ComponenteDto x) => x.Opcional);
		}
	}
	public class IfoodAbaViewModel : AbaAuxiliarViewModelBase
	{
		public IfoodAbaViewModel(IViewModelDtoBase viewModel)
			: base("iFood", "Catálogos", viewModel)
		{
		}
	}
	public class IfoodViewModel : ViewModelDtoBase<MenuRapidoDto>
	{
		public bool Delivery
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Delivery));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Delivery), value);
			}
		}

		public bool ConsumoInterno
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ConsumoInterno));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ConsumoInterno), value);
			}
		}

		public bool Retirada
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Retirada));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Retirada), value);
			}
		}

		public virtual SelecaoCommandParameter<TabelaPrecoDto> TabelaPrecoCommandParameter => new SelecaoCommandParameter<TabelaPrecoDto>("TabelaPrecoListaView", (Action<TabelaPrecoDto>)delegate(TabelaPrecoDto tabela)
		{
			TabelaPreco = tabela;
		}, true);

		public TabelaPrecoDto TabelaPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value);
			}
		}

		public DelegateCommand RemoverTabelaPrecoCommand { get; set; }

		public IfoodViewModel(MenuRapidoDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			RemoverTabelaPrecoCommand = new DelegateCommand((Action)RemoverTabelaPreco, (Func<bool>)PodeRemoverTabelaPreco, (bool?)null);
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
		}

		private bool PodeRemoverTabelaPreco()
		{
			return !HelperObject.Is<TabelaPrecoDtoNull>((object)TabelaPreco);
		}

		public override void ProjectedAsViewModel()
		{
			Delivery = base.Dto.Ifood.Delivery;
			ConsumoInterno = base.Dto.Ifood.ConsumoInterno;
			Retirada = base.Dto.Ifood.Retirada;
			TabelaPreco = base.Dto.Ifood.TabelaPreco;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			object obj = dto;
			MenuRapidoDto val = (MenuRapidoDto)((obj is MenuRapidoDto) ? obj : null);
			val.Ifood = new IfoodMenuRapidoDto
			{
				ConsumoInterno = ConsumoInterno,
				Retirada = Retirada,
				Delivery = Delivery,
				TabelaPreco = TabelaPreco
			};
			base.AtualizarDto<MenuRapidoDto>(ref val);
		}
	}
	public class ItemGrupoMenuViewModel : ViewModelDtoBase<GrupoComplementoMenuRapidoDto>
	{
		private readonly Action _propertyChanged;

		public bool TabelaPrecoVisivel => (int)base.Dto.Grupo.TipoGrupo != 1;

		public TabelaPrecoDto TabelaPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value);
			}
		}

		public virtual SelecaoCommandParameter<TabelaPrecoDto> TabelaPrecoCommandParameter => new SelecaoCommandParameter<TabelaPrecoDto>("TabelaPrecoListaView", (Action<TabelaPrecoDto>)delegate(TabelaPrecoDto tabela)
		{
			TabelaPreco = tabela;
			base.Dto.TabelaPreco = tabela;
		}, true);

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value, (Action)delegate
				{
					_propertyChanged();
				});
			}
		}

		public DelegateCommand RemoverTabelaPrecoCommand { get; set; }

		public ItemGrupoMenuViewModel(GrupoComplementoMenuRapidoDto dto, Action propertyChanged)
			: base(dto)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			_propertyChanged = propertyChanged;
			TabelaPreco = new TabelaPrecoDto();
			if (HelperObject.IsExactlyObject<TabelaPrecoDto>((object)dto.TabelaPreco))
			{
				TabelaPreco = dto.TabelaPreco;
			}
			RemoverTabelaPrecoCommand = new DelegateCommand((Action)RemoverTabelaPreco, (Func<bool>)PodeRemoverTabelaPreco, (bool?)null);
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
			base.Dto.TabelaPreco = TabelaPreco;
		}

		private bool PodeRemoverTabelaPreco()
		{
			return !HelperObject.Is<TabelaPrecoDtoNull>((object)TabelaPreco);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}
	}
	public class ItemMenuViewModel : ViewModelDtoBase<ItemMenuRapidoDto>
	{
		private readonly Action _propertyChanged;

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value, (Action)delegate
				{
					_propertyChanged();
				});
			}
		}

		public ItemMenuViewModel(ItemMenuRapidoDto dto, Action propertyChanged)
			: base(dto)
		{
			_propertyChanged = propertyChanged;
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}
	}
	public class ItensMenuAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ItensMenuAbaViewModel(IViewModelDtoBase viewModel)
			: base("Itens do menu", "Grupos e observações", viewModel)
		{
		}
	}
	public class ItensMenuViewModel : ViewModelDtoBase<MenuRapidoDto>
	{
		private readonly NavigationBase _navegar;

		[Valid]
		public Lista<ItemMenuViewModel> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemMenuViewModel>>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemMenuViewModel>>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens), value);
			}
		}

		public ViewModelValidationBase FichaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel), value);
			}
		}

		public Lista<ProdutoServicoEmpresaDto> Produtos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ProdutoServicoEmpresaDto>>((Expression<Func<Lista<ProdutoServicoEmpresaDto>>>)(() => Produtos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ProdutoServicoEmpresaDto>>((Expression<Func<Lista<ProdutoServicoEmpresaDto>>>)(() => Produtos), value);
			}
		}

		public string DescricaoTela
		{
			get
			{
				object obj;
				if (Produtos.Numero <= 1)
				{
					Lista<ProdutoServicoEmpresaDto> produtos = Produtos;
					if (produtos == null)
					{
						obj = null;
					}
					else
					{
						ProdutoServicoEmpresaDto itemSelecionado = produtos.ItemSelecionado;
						if (itemSelecionado == null)
						{
							obj = null;
						}
						else
						{
							ProdutoServicoDto produtoServico = itemSelecionado.ProdutoServico;
							obj = ((produtoServico != null) ? produtoServico.Descricao : null);
						}
					}
					if (obj == null)
					{
						return string.Empty;
					}
				}
				else
				{
					obj = $"{Produtos.Numero} Produtos selecionados(as)";
				}
				return (string)obj;
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

		public virtual ISelecaoCommandParameter ProdutoServicoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> produtosServicosEmpresa)
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Invalid comparison between Unknown and I4
			Produtos = new Lista<ProdutoServicoEmpresaDto>();
			foreach (ProdutoServicoEmpresaDto item in produtosServicosEmpresa)
			{
				try
				{
					if ((int)item.ProdutoServico.Tipo == 5)
					{
						throw new Exception("Selecione um produto que não seja do tipo 'Combustível'!");
					}
					if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)item.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)item.ProdutoEmpresa).Estoque.IsNormal)
					{
						throw new Exception("Selecione um produto com estoque do tipo 'Normal'!");
					}
					if (!item.Vendavel)
					{
						throw new Exception("Selecione um produto 'Vendável'!");
					}
					if (Itens.Itens.Select((ItemMenuViewModel y) => ((ViewModelDtoBase<ItemMenuRapidoDto>)y).Dto.ProdutoEmpresaReferencia).Contains(((EntityBaseDto)item).Id) && DialogoFactory.MostrarDialogoSimNao($"Produto '{item.ProdutoServico.Descricao}' já existente no cardápio, deseja adicionar novamente?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
					{
						break;
					}
					Produtos.Adicionar(item);
					((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoTela));
				}
				catch (Exception ex)
				{
					HelperException.ShowLastException(ex, "", false, false);
				}
			}
		}, true);

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand GruposCommand { get; set; }

		public ICommand GravarCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand CopiarProdutoEmpresaReferenciaItemCommand { get; set; }

		public ICommand OrdenarCommand { get; set; }

		public ICommand ComponentesCommand { get; set; }

		public ItensMenuViewModel()
		{
		}

		public ItensMenuViewModel(MenuRapidoDto dto, NavigationBase navegar)
			: base(dto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Expected O, but got Unknown
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Expected O, but got Unknown
			//IL_017a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0184: Expected O, but got Unknown
			_navegar = navegar;
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)MoverParaCima, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima() && Itens.Itens.Count((ItemMenuViewModel x) => x.Selecionado) == 1), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)MoverParaBaixo, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo() && Itens.Itens.Count((ItemMenuViewModel x) => x.Selecionado) == 1), (bool?)null);
			GruposCommand = (ICommand)new DelegateCommand((Action)AdicionarGrupos, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.Itens.Any((ItemMenuViewModel x) => x.Selecionado)), (bool?)null);
			GravarCommand = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemMenuViewModel>((Action<ItemMenuViewModel>)RemoverItem);
			CopiarProdutoEmpresaReferenciaItemCommand = (ICommand)new AsyncCommand<ItemMenuViewModel>((Func<ItemMenuViewModel, Task>)CopiarProdutoEmpresaReferenciaItem, (Func<ItemMenuViewModel, bool>)((ItemMenuViewModel x) => true), false, (bool?)null);
			OrdenarCommand = (ICommand)new DelegateCommand((Action)OrdenarAlfabetico);
			ComponentesCommand = (ICommand)new DelegateCommand((Action)EditarComponentes, (Func<bool>)PodeEditarComponentes, (bool?)null);
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNotNull((object)FichaViewModel))
			{
				return HelperObject.To<ComponentesFichaViewModel>((object)FichaViewModel)?.PodeGravar() ?? true;
			}
			return false;
		}

		private void MoverParaCima()
		{
			ItemMenuViewModel itemSelecionado = Itens.Itens.FirstOrDefault((ItemMenuViewModel x) => x.Selecionado);
			Itens.ItemSelecionado = itemSelecionado;
			Itens.MoverParaCima();
			Itens.ItemSelecionado = itemSelecionado;
		}

		private void MoverParaBaixo()
		{
			ItemMenuViewModel itemSelecionado = Itens.Itens.FirstOrDefault((ItemMenuViewModel x) => x.Selecionado);
			Itens.ItemSelecionado = itemSelecionado;
			Itens.MoverParaBaixo();
			Itens.ItemSelecionado = itemSelecionado;
		}

		private bool PodeEditarComponentes()
		{
			if (HelperObject.IsNotNull((object)Itens) && Itens.PossuiItemSelecionado && Itens.Itens.Count((ItemMenuViewModel item) => item.Selecionado) == 1)
			{
				return ((ViewModelDtoBase<ItemMenuRapidoDto>)Itens.ItemSelecionado).Dto.Componentes.Any();
			}
			return false;
		}

		private void EditarComponentes()
		{
			FichaViewModel = (ViewModelValidationBase)(object)new ComponentesFichaViewModel(Itens.ItemSelecionado, ((ViewModelDtoBase<ItemMenuRapidoDto>)Itens.ItemSelecionado).Dto.Componentes);
		}

		private void RemoverItem(ItemMenuViewModel obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemMenuViewModel>>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens));
		}

		private Task CopiarProdutoEmpresaReferenciaItem(ItemMenuViewModel obj)
		{
			return Task.Factory.StartNew(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { ((ViewModelDtoBase<ItemMenuRapidoDto>)obj).Dto.ProdutoEmpresaReferencia }))).FirstOrDefault();
					DialogoFactory.MostrarDialogoConfirma($"Código interno: {val.ProdutoServico.CodigoInterno}\n" + $"Descrição no estoque: {val.ProdutoServico.Descricao}\n" + $"Característica: {val.ProdutoServico.Caracteristica}", false, HorizontalAlignment.Left);
				});
			});
		}

		private void Gravar()
		{
			if (FichaViewModel is GrupoComplementoFichaViewModel)
			{
				HelperObject.To<GrupoComplementoFichaViewModel>((object)FichaViewModel).Gravar();
				FichaViewModel = (ViewModelValidationBase)(object)this;
			}
			if (FichaViewModel is PredefinicoesFichaViewModel)
			{
				base.Dto.Predefinicoes = HelperIEnumerable.ToListEmptyWhenNull<string>(HelperObject.To<PredefinicoesFichaViewModel>((object)FichaViewModel).Predefinicoes);
				HelperObject.To<PredefinicoesFichaViewModel>((object)FichaViewModel).Gravar();
				FichaViewModel = HelperObject.To<PredefinicoesFichaViewModel>((object)FichaViewModel).FichaViewModel;
				return;
			}
			if (FichaViewModel is AdicionaisFichaViewModel)
			{
				HelperObject.To<AdicionaisFichaViewModel>((object)FichaViewModel).Gravar();
				FichaViewModel = HelperObject.To<AdicionaisFichaViewModel>((object)FichaViewModel).FichaViewModel;
				return;
			}
			if (FichaViewModel is BonificacoesFichaViewModel)
			{
				HelperObject.To<BonificacoesFichaViewModel>((object)FichaViewModel).Gravar();
				FichaViewModel = HelperObject.To<BonificacoesFichaViewModel>((object)FichaViewModel).FichaViewModel;
				return;
			}
			if (FichaViewModel is ComponentesFichaViewModel)
			{
				HelperObject.To<ComponentesFichaViewModel>((object)FichaViewModel).Gravar();
			}
			FichaViewModel.Dispose();
			FichaViewModel = null;
		}

		private void OrdenarAlfabetico()
		{
			Itens.Ordenar((Expression<Func<ItemMenuViewModel, object>>)((ItemMenuViewModel x) => ((ViewModelDtoBase<ItemMenuRapidoDto>)x).Dto.DescricaoTela), (Expression<Func<ItemMenuViewModel, object>>)null);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemMenuViewModel>>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens));
		}

		private void Adicionar()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			foreach (ProdutoServicoEmpresaDto iten in Produtos.Itens)
			{
				Itens.Adicionar(new ItemMenuViewModel(new ItemMenuRapidoDto
				{
					ProdutoEmpresaReferencia = ((EntityBaseDto)iten).Id,
					DescricaoTela = iten.ProdutoServico.Descricao,
					Quantidade = Quantidade,
					Componentes = iten.ProdutoServico.Produto.Componentes,
					Visivel = true,
					ItemBase = false
				}, MudouItemSelecionado));
			}
			Produtos = new Lista<ProdutoServicoEmpresaDto>();
			Quantidade = 0m;
			((BindableBase)this).RaisePropertiesChanged<Lista<ItemMenuViewModel>, string>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens), (Expression<Func<string>>)(() => DescricaoTela));
		}

		private bool PodeAdicionar()
		{
			if (!string.IsNullOrWhiteSpace(DescricaoTela))
			{
				return Quantidade > 0m;
			}
			return false;
		}

		private void AdicionarGrupos()
		{
			if (Itens.Itens.Count((ItemMenuViewModel x) => x.Selecionado) > 1)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione apenas um item para acessar os grupos de complementos.", false, HorizontalAlignment.Center);
				return;
			}
			FichaViewModel = (ViewModelValidationBase)(object)new GrupoComplementoFichaViewModel(base.Dto, HelperIEnumerable.ToListEmptyWhenNull<ItemMenuViewModel>(Itens.Itens.Where((ItemMenuViewModel x) => x.Selecionado)), AlterouFicha, _navegar);
		}

		private void AlterouFicha(object sender, ViewModelValidationBase viewModelValidationBase)
		{
			FichaViewModel = viewModelValidationBase;
		}

		private void MudouItemSelecionado()
		{
			Itens.ItemSelecionado = Itens.Itens.FirstOrDefault((ItemMenuViewModel x) => x.Selecionado);
		}

		public override void ProjectedAsViewModel()
		{
			Produtos = new Lista<ProdutoServicoEmpresaDto>();
			Itens = new Lista<ItemMenuViewModel>();
			Itens.Adicionar(from x in base.Dto.ItensMenuRapido
				orderby x.Ordem
				select x into dto
				select new ItemMenuViewModel(dto, MudouItemSelecionado));
			((BindableBase)this).RaisePropertyChanged<Lista<ItemMenuViewModel>>((Expression<Func<Lista<ItemMenuViewModel>>>)(() => Itens));
			Itens.PosicionarNoPrimeiro();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			MenuRapidoDto val = (MenuRapidoDto)((obj is MenuRapidoDto) ? obj : null);
			if (HelperObject.IsNull((object)val))
			{
				return;
			}
			foreach (ItemMenuViewModel iten in Itens.Itens)
			{
				((ViewModelDtoBase<ItemMenuRapidoDto>)iten).Dto.Ordem = Itens.Indice(iten) + 1;
			}
			val.ItensMenuRapido = HelperIEnumerable.ToListEmptyWhenNull<ItemMenuRapidoDto>(Itens.Itens.Select((ItemMenuViewModel x) => ((ViewModelDtoBase<ItemMenuRapidoDto>)x).Dto));
			val.Predefinicoes = base.Dto.Predefinicoes;
			base.AtualizarDto<MenuRapidoDto>(ref val);
		}
	}
	public class PredefinicoesFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemGrupoMenuViewModel> _itens;

		public ViewModelValidationBase FichaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ViewModelValidationBase>((Expression<Func<ViewModelValidationBase>>)(() => FichaViewModel), value);
			}
		}

		public string Predefinicao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Predefinicao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Predefinicao), value);
			}
		}

		public int Minimo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Minimo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Minimo), value);
			}
		}

		public int Maximo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Maximo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Maximo), value);
			}
		}

		public Lista<string> PredefinicoesCadastradas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => PredefinicoesCadastradas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => PredefinicoesCadastradas), value);
			}
		}

		public Lista<string> PredefinicoesSelecionadas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => PredefinicoesSelecionadas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => PredefinicoesSelecionadas), value);
			}
		}

		public IEnumerable<string> Predefinicoes => PredefinicoesCadastradas.Itens.Union(PredefinicoesSelecionadas.Itens);

		public ICommand AdicionarPredefinicaoCommand { get; set; }

		public ICommand SelecionarPredefinicaoCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand RemoverPredefinicaoCommand { get; set; }

		public PredefinicoesFichaViewModel(MenuRapidoDto dto, IEnumerable<ItemGrupoMenuViewModel> itens, ViewModelValidationBase viewModel)
		{
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Expected O, but got Unknown
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Expected O, but got Unknown
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Expected O, but got Unknown
			//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Expected O, but got Unknown
			_itens = HelperIEnumerable.ToListEmptyWhenNull<ItemGrupoMenuViewModel>(itens);
			FichaViewModel = viewModel;
			PredefinicoesCadastradas = new Lista<string>();
			PredefinicoesSelecionadas = new Lista<string>();
			List<string> list = HelperIEnumerable.ToListEmptyWhenNull<string>(_itens.SelectMany((ItemGrupoMenuViewModel x) => ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)x).Dto.PredefinicoesSelecionadas).Distinct());
			List<string> list2 = HelperIEnumerable.ToListEmptyWhenNull<string>(dto.Predefinicoes.Except(list));
			Minimo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMinima;
			Maximo = ((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)_itens.FirstOrDefault()).Dto.QuantidadeMaxima;
			PredefinicoesCadastradas.Adicionar((IEnumerable<string>)list2);
			PredefinicoesSelecionadas.Adicionar((IEnumerable<string>)list);
			PredefinicoesSelecionadas.AlterouEvent += PredefinicoesSelecionadasOnAlterouEvent;
			AdicionarPredefinicaoCommand = (ICommand)new DelegateCommand((Action)AdicionarPredefinicao, (Func<bool>)PodeAdicionarPredefinicao, (bool?)null);
			SelecionarPredefinicaoCommand = (ICommand)new DelegateCommand((Action)SelecionarPredefinicao, (Func<bool>)PodeSelecionarPredefinicao, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				PredefinicoesSelecionadas.MoverParaCima();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)PredefinicoesSelecionadas) && PredefinicoesSelecionadas.PodeMoverParaCima()), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				PredefinicoesSelecionadas.MoverParaBaixo();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)PredefinicoesSelecionadas) && PredefinicoesSelecionadas.PodeMoverParaBaixo()), (bool?)null);
			RemoverPredefinicaoCommand = (ICommand)new DelegateCommand((Action)RemoverPredefinicao, (Func<bool>)PodeRemoverPredefinicao, (bool?)null);
		}

		private void AdicionarPredefinicao()
		{
			PredefinicoesCadastradas.Adicionar(HelperString.CopiarTexto(Predefinicao, 190));
			Predefinicao = string.Empty;
		}

		private bool PodeAdicionarPredefinicao()
		{
			return !string.IsNullOrWhiteSpace(Predefinicao);
		}

		private void SelecionarPredefinicao()
		{
			PredefinicoesSelecionadas.Adicionar(PredefinicoesCadastradas.ItemSelecionado);
			PredefinicoesCadastradas.Remover(PredefinicoesCadastradas.ItemSelecionado);
		}

		private bool PodeSelecionarPredefinicao()
		{
			return PredefinicoesCadastradas.PossuiItemSelecionado;
		}

		private void RemoverPredefinicao()
		{
			PredefinicoesCadastradas.Adicionar(PredefinicoesSelecionadas.ItemSelecionado);
			PredefinicoesSelecionadas.Remover(PredefinicoesSelecionadas.ItemSelecionado);
		}

		private bool PodeRemoverPredefinicao()
		{
			return PredefinicoesSelecionadas.PossuiItemSelecionado;
		}

		private void PredefinicoesSelecionadasOnAlterouEvent(object sender, IEnumerable<string> enumerable)
		{
			foreach (ItemGrupoMenuViewModel iten in _itens)
			{
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.PredefinicoesSelecionadas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.PredefinicoesCadastradas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
			}
		}

		public void Gravar()
		{
			foreach (ItemGrupoMenuViewModel iten in _itens)
			{
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.QuantidadeMinima = Minimo;
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.QuantidadeMaxima = Maximo;
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.PredefinicoesSelecionadas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
				((ViewModelDtoBase<GrupoComplementoMenuRapidoDto>)iten).Dto.PredefinicoesCadastradas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
			}
		}

		public override void Dispose()
		{
			PredefinicoesSelecionadas.AlterouEvent -= PredefinicoesSelecionadasOnAlterouEvent;
			((ViewModelValidationBase)PredefinicoesSelecionadas).Dispose();
			((ViewModelValidationBase)PredefinicoesCadastradas).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class OrdenarMenuFichaViewModel : NavigationBase
	{
		private readonly IWService _service;

		public Lista<ItemOrdemMenuRapidoDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemOrdemMenuRapidoDto>>((Expression<Func<Lista<ItemOrdemMenuRapidoDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemOrdemMenuRapidoDto>>((Expression<Func<Lista<ItemOrdemMenuRapidoDto>>>)(() => Itens), value);
			}
		}

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand GravarCommand { get; set; }

		public OrdenarMenuFichaViewModel(IWService service)
			: base("Ordenar menus")
		{
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			_service = service;
			Itens = new Lista<ItemOrdemMenuRapidoDto>();
			IEnumerable<ItemOrdemMenuRapidoDto> source = service.Canal<IMenuRapidoModuleService, IEnumerable<ItemOrdemMenuRapidoDto>>((Func<IMenuRapidoModuleService, IEnumerable<ItemOrdemMenuRapidoDto>>)((IMenuRapidoModuleService x) => x.ObterOrdemMenu()));
			Itens.Adicionar((IEnumerable<ItemOrdemMenuRapidoDto>)source.OrderBy((ItemOrdemMenuRapidoDto x) => x.Ordem));
			Itens.PosicionarNoPrimeiro();
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaCima();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima()), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaBaixo();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo()), (bool?)null);
			GravarCommand = (ICommand)new DelegateCommand((Action)Gravar);
		}

		private void Gravar()
		{
			foreach (ItemOrdemMenuRapidoDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			_service.Canal<IMenuRapidoModuleService>((Action<IMenuRapidoModuleService>)delegate(IMenuRapidoModuleService x)
			{
				x.SalvarOrdemMenu((IEnumerable<ItemOrdemMenuRapidoDto>)Itens.Itens);
			});
			((NavigationBase)this).FecharView();
		}
	}
}

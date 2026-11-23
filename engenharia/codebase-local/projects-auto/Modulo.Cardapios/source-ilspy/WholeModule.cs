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
using DigisatServer.Application.Main.Dto.Cadastros.Cardapios;
using DigisatServer.Application.Main.Dto.Cadastros.Grupos;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Cardapios.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Modulo.Cardapios")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Modulo.Cardapios")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Cardapios
{
	[Module(ModuleName = "CardapioModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class CardapioModulo : IModule
	{
		private readonly IUnityContainer _container;

		public CardapioModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<CardapiosView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CardapioListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CardapioFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CardapioFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdenarCardapioFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Cardapios.Views
{
	public class CardapioFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public CardapioFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapiofichaadicionarview.xaml", UriKind.Relative);
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
	public class CardapioFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public CardapioFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapiofichaatualizarview.xaml", UriKind.Relative);
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
	public class CardapioListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CardapioListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapiolistaview.xaml", UriKind.Relative);
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
	public class CardapiosView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CardapiosView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapiosview.xaml", UriKind.Relative);
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
	public class OrdenarCardapioFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdenarCardapioFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/ordenarcardapiofichaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Cardapios.Views.CardapioViewDetalhes
{
	public class CardapioView : UserControl, IComponentConnector
	{
		internal ScaleTransform Escala;

		internal ButtonInfo Parameter;

		private bool _contentLoaded;

		public CardapioView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapioviewdetalhes/cardapioview.xaml", UriKind.Relative);
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapioviewdetalhes/ifoodview.xaml", UriKind.Relative);
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
	public class ItensCardapioView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ItensCardapioView()
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
				Uri resourceLocator = new Uri("/Modulo.Cardapios;component/views/cardapioviewdetalhes/itenscardapioview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Cardapios.ViewModels
{
	public class AdicionaisFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemCardapioViewModel> _itens;

		[Valid]
		public Lista<ItemAdicionalDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemAdicionalDto>>((Expression<Func<Lista<ItemAdicionalDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemAdicionalDto>>((Expression<Func<Lista<ItemAdicionalDto>>>)(() => Itens), value);
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

		public virtual SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>> ProdutoServicoCommandParameter => new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> produtosServicos)
		{
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Invalid comparison between Unknown and I4
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
				Lista<ItemAdicionalDto> val = new Lista<ItemAdicionalDto>();
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
						val.Adicionar(new ItemAdicionalDto
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
				Itens.Adicionar((IEnumerable<ItemAdicionalDto>)val.Itens);
				DescricaoTela = string.Empty;
				ProdutoEmpresa = null;
				QuantidadeMinima = 0m;
				QuantidadeMaxima = 0m;
				((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalDto>>((Expression<Func<Lista<ItemAdicionalDto>>>)(() => Itens));
				FocoNaBuscaProduto = !FocoNaBuscaProduto;
			}
			else if ((int)produtosServicos.FirstOrDefault().ProdutoServico.Tipo == 5)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto que não seja do tipo 'Combustível'!", false, HorizontalAlignment.Center);
			}
			else if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtosServicos.FirstOrDefault().ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtosServicos.FirstOrDefault().ProdutoEmpresa).Estoque.IsNormal)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto com estoque do tipo 'Normal'!", false, HorizontalAlignment.Center);
			}
			else if (!produtosServicos.FirstOrDefault().Vendavel)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto 'Vendável'!", false, HorizontalAlignment.Center);
			}
			else
			{
				ProdutoEmpresa = produtosServicos.FirstOrDefault();
				DescricaoTela = HelperString.CopiarTexto(ProdutoEmpresa.ProdutoServico.Descricao, 190);
				FocoNaDescricaoTela = !FocoNaDescricaoTela;
			}
		}, true);

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

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand CopiarProdutoEmpresaReferenciaItemCommand { get; set; }

		public AdicionaisFichaViewModel(IEnumerable<ItemCardapioViewModel> itens)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			_itens = itens;
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaCima();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima()), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Itens.MoverParaBaixo();
			}, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo()), (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemAdicionalDto>((Action<ItemAdicionalDto>)RemoverItem);
			CopiarProdutoEmpresaReferenciaItemCommand = (ICommand)new AsyncCommand<ItemAdicionalDto>((Func<ItemAdicionalDto, Task>)CopiarProdutoEmpresaReferenciaItem, (Func<ItemAdicionalDto, bool>)((ItemAdicionalDto x) => true), false, (bool?)null);
			Itens = new Lista<ItemAdicionalDto>();
			Itens.Adicionar((IEnumerable<ItemAdicionalDto>)(from x in itens.SelectMany((ItemCardapioViewModel x) => ((ViewModelDtoBase<ItemCardapioDto>)x).Dto.Adicionais)
				group x by x.ProdutoEmpresaReferencia into g
				select g.FirstOrDefault() into x
				orderby x.Ordem
				select x));
			Itens.PosicionarNoPrimeiro();
			Minimo = ((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MinimoAdicional;
			Maximo = (((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MaximoAdicional.Equals(0) ? 1 : ((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MaximoAdicional);
		}

		private void Adicionar()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			Itens.Adicionar(new ItemAdicionalDto
			{
				ProdutoEmpresaReferencia = ((EntityBaseDto)ProdutoEmpresa).Id,
				DescricaoTela = DescricaoTela,
				QuantidadeMinima = QuantidadeMinima,
				QuantidadeMaxima = QuantidadeMaxima
			});
			DescricaoTela = string.Empty;
			ProdutoEmpresa = null;
			QuantidadeMinima = 0m;
			QuantidadeMaxima = 0m;
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalDto>>((Expression<Func<Lista<ItemAdicionalDto>>>)(() => Itens));
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

		private void RemoverItem(ItemAdicionalDto obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalDto>>((Expression<Func<Lista<ItemAdicionalDto>>>)(() => Itens));
		}

		private Task CopiarProdutoEmpresaReferenciaItem(ItemAdicionalDto obj)
		{
			return Task.Factory.StartNew(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { obj.ProdutoEmpresaReferencia }))).FirstOrDefault();
					try
					{
						if (HelperObject.IsNotNull((object)val))
						{
							Clipboard.SetText(val.ProdutoServico.Descricao);
						}
					}
					catch (Exception)
					{
						try
						{
							if (HelperObject.IsNotNull((object)val))
							{
								Clipboard.SetDataObject(val.ProdutoServico.Descricao);
							}
						}
						catch (Exception)
						{
						}
					}
				});
			});
		}

		public void Gravar()
		{
			foreach (ItemAdicionalDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			foreach (ItemCardapioViewModel iten2 in _itens)
			{
				((ViewModelDtoBase<ItemCardapioDto>)iten2).Dto.Adicionais = HelperIEnumerable.ToListEmptyWhenNull<ItemAdicionalDto>((IEnumerable<ItemAdicionalDto>)Itens.Itens);
				((ViewModelDtoBase<ItemCardapioDto>)iten2).Dto.MinimoAdicional = Minimo;
				((ViewModelDtoBase<ItemCardapioDto>)iten2).Dto.MaximoAdicional = Maximo;
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class CardapioAbaViewModel : AbaPrincipalViewModelBase
	{
		public CardapioAbaViewModel(IViewModelPrincipal viewModel)
			: base("Cardápio", "Dados principais", viewModel)
		{
		}
	}
	public class CardapioFichaAdicionarViewModel : FichaBaseAdicionar<CardapioDto, CardapioViewModel, ICardapioModuleService>
	{
		public CardapioFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class CardapioFichaAtualizarViewModel : FichaBaseAtualizar<CardapioDto, CardapioViewModel, ICardapioModuleService>
	{
		public CardapioFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class CardapioFormulario : FormularioEntity<CardapioDto>
	{
		private CardapioViewModel _cardapioViewModel;

		public CardapioFormulario(CardapioDto dto)
			: base(dto)
		{
			AdicionarAbas();
		}

		private void AdicionarAbas()
		{
			_cardapioViewModel = new CardapioViewModel(base.Dto);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new CardapioAbaViewModel((IViewModelPrincipal)(object)_cardapioViewModel),
				(IAbaViewModelBase)new ItensCardapioAbaViewModel((IViewModelDtoBase)(object)new ItensCardapioViewModel(base.Dto))
			});
			if (base.Dto.MostrarDeliveryOnline)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new IfoodAbaViewModel((IViewModelDtoBase)(object)new IfoodViewModel(base.Dto)) });
			}
			_cardapioViewModel.AlterouUsaDeliveryOnline += CardapioViewModelOnAlterouUsaDeliveryOnline;
		}

		private void CardapioViewModelOnAlterouUsaDeliveryOnline(object sender, bool usaDeliveryOnline)
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
			_cardapioViewModel.AlterouUsaDeliveryOnline -= CardapioViewModelOnAlterouUsaDeliveryOnline;
		}
	}
	public class CardapioListaViewModel : ListaSelecaoReferencia<CardapioDto, CardapioViewModel, ICardapioModuleService, Cardapio>
	{
		public ICommand OrdenarCardapioCommand { get; set; }

		public ICommand EnviaIfoodCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public ICommand MostrarSomenteCardapiosSelecionadosNesseLocalCommand { get; set; }

		public ICommand MostrarTodosOsCardapiosNesseLocalCommand { get; set; }

		public CardapioListaViewModel(ISessao sessao, IWService service, IEntityAtivos<CardapioDto, ICardapioModuleService> ativos, IEntityInativos<CardapioDto, ICardapioModuleService> inativos)
			: base(sessao, typeof(CardapioFichaAdicionarView).Name, typeof(CardapioFichaAtualizarView).Name, service, new IEntityBase<CardapioDto, ICardapioModuleService>[2]
			{
				(IEntityBase<CardapioDto, ICardapioModuleService>)(object)ativos,
				(IEntityBase<CardapioDto, ICardapioModuleService>)(object)inativos
			})
		{
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Expected O, but got Unknown
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Expected O, but got Unknown
			OrdenarCardapioCommand = (ICommand)new DelegateCommand((Action)OrdenarCardapio);
			EnviaIfoodCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarIfood, (Func<bool>)PodeEnviarIfood, false, (bool?)null);
			HelperObject.To<OpcaoPaginacao>((object)((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Pesquisa.Contexto).SetarRegistrosPorPagina((short)100);
			DuplicarCommand = (ICommand)new DelegateCommand((Action)Duplicar, (Func<bool>)PodeDuplicar, (bool?)null);
			MostrarSomenteCardapiosSelecionadosNesseLocalCommand = (ICommand)new DelegateCommand((Action)MostrarCardapioLocal, (Func<bool>)PodeMostrarCardapioLocal, (bool?)null);
			MostrarTodosOsCardapiosNesseLocalCommand = (ICommand)new DelegateCommand((Action)MostrarTodosOsCardapiosNesseLocal, (Func<bool>)PodeMostrarTodosOsCardapiosNesseLocal, (bool?)null);
		}

		private void OrdenarCardapio()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			((NavigationBase)this).Navegar("OrdenarCardapioFichaView", (Action<NavigationResult>)delegate
			{
			}, new NavigationParameters());
		}

		private Task EnviarIfood()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			return ((ListaReferenciaBase<CardapioDto, CardapioViewModel, ICardapioModuleService, Cardapio>)(object)this)._service.Canal<ICardapioModuleService, Task>((Func<ICardapioModuleService, Task>)((ICardapioModuleService service) => service.EnviarCardapioIfoodAsync(((EntityBaseDto)((ViewModelDtoBase<CardapioDto>)(object)((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task task)
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
			if (HelperObject.IsNotNull((object)((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens) && ((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return ((ViewModelDtoBase<CardapioDto>)(object)((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens.Selecionado).Dto.MostrarDeliveryOnline;
			}
			return false;
		}

		protected async void Duplicar()
		{
			try
			{
				CardapioDto val = await ((ListaReferenciaBase<CardapioDto, CardapioViewModel, ICardapioModuleService, Cardapio>)(object)this)._service.Canal<ICardapioModuleService, Task<CardapioDto>>((Func<ICardapioModuleService, Task<CardapioDto>>)((ICardapioModuleService service) => ((IReferenciaAtualizarService<CardapioDto>)(object)service).AtualizarDtoAsync(((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens.ObterDtoItemSelecionado())));
				val.LimparId();
				List<ItemCardapioDto> list = HelperIEnumerable.ToListEmptyWhenNull<ItemCardapioDto>(val.ItensCardapio);
				foreach (ItemCardapioDto item in list)
				{
					item.LimparId();
				}
				val.ItensCardapio = new List<ItemCardapioDto>(list);
				CardapioViewModel cardapioViewModel = new CardapioViewModel(val);
				((ViewModelBase<CardapioDto>)(object)cardapioViewModel).ProjectedAsViewModel();
				CardapioListaViewModel cardapioListaViewModel = this;
				string viewAdicionarFicha = ((ListaManutencaoBase<CardapioDto, CardapioViewModel, Cardapio>)(object)this).ViewAdicionarFicha;
				Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
				{
					if (navigationResult.Result == true)
					{
						((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens.Estado.AlterarAdicao();
					}
				};
				NavigationParameters val2 = new NavigationParameters();
				val2.Add(typeof(CardapioViewModel).Name, (object)cardapioViewModel);
				((NavigationBase)cardapioListaViewModel).Navegar(viewAdicionarFicha, obj, val2);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeDuplicar()
		{
			return true;
		}

		private void MostrarCardapioLocal()
		{
			ConfiguracaoCardapiosLocal.Redefinir();
			List<CardapioLocal> list = HelperIEnumerable.ToListEmptyWhenNull<CardapioLocal>(base.ItensSelecionados.Select((CardapioDto x) => CardapioLocal.Criar().ComReferencia(((EntityBaseDto)x).Id)));
			ConfiguracaoCardapiosLocal.Criar().ComCardapios(list).Salvar();
			DialogoFactory.MostrarDialogoConfirma("O sistema deve ser reiniciado para assumir a configuração.", false, HorizontalAlignment.Center);
		}

		private bool PodeMostrarCardapioLocal()
		{
			if (((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens != null)
			{
				return base.ItensSelecionados.Any();
			}
			return false;
		}

		private void MostrarTodosOsCardapiosNesseLocal()
		{
			ConfiguracaoCardapiosLocal.Redefinir();
			DialogoFactory.MostrarDialogoConfirma("O sistema deve ser reiniciado para assumir a configuração.", false, HorizontalAlignment.Center);
		}

		private bool PodeMostrarTodosOsCardapiosNesseLocal()
		{
			if (((ListaBase<CardapioDto, CardapioViewModel>)(object)this).Itens != null)
			{
				return !base.ItensSelecionados.Any();
			}
			return false;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			base.OnParametroInformado(sender, navigationParameters);
			base.CheckBoxVisibility = Visibility.Visible;
		}
	}
	public class CardapiosViewModel : ModuloBase
	{
		public CardapiosViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloCardapioRegion, "CardapioListaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class CardapioViewModel : ViewModelAtivoBase<CardapioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
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

		public bool MostrarOnline
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarOnline));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarOnline), value);
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

		public event EventHandler<bool> AlterouUsaDeliveryOnline;

		public CardapioViewModel()
		{
		}

		public CardapioViewModel(CardapioDto cardapioDto)
			: base(cardapioDto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
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

		public override void ProjectedAsViewModel()
		{
			Descricao = ((ViewModelDtoBase<CardapioDto>)(object)this).Dto.Descricao;
			Impressora = ((ViewModelDtoBase<CardapioDto>)(object)this).Dto.Impressora;
			Imagem = HelperImage.ToBitmapSource(HelperImage.Decompress(((ViewModelDtoBase<CardapioDto>)(object)this).Dto.Imagem));
			base.Ativo = ((EntityDto)((ViewModelDtoBase<CardapioDto>)(object)this).Dto).Ativo;
			MostrarOnline = ((ViewModelDtoBase<CardapioDto>)(object)this).Dto.MostrarOnline;
			MostrarPedirMenu = ((ViewModelDtoBase<CardapioDto>)(object)this).Dto.MostrarPedirMenu;
			MostrarDeliveryOnline = ((ViewModelDtoBase<CardapioDto>)(object)this).Dto.MostrarDeliveryOnline;
			IfoodConfigurado = !string.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Saida.Ifood.AccessToken.Valor);
			Caracteristica = HelperString.ToStringEmptyWhenNull(((ViewModelDtoBase<CardapioDto>)(object)this).Dto.Caracteristica);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CardapioDto val = (CardapioDto)((obj is CardapioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Descricao = Descricao;
				((EntityDto)val).Ativo = base.Ativo;
				val.Imagem = HelperImage.Compress(Imagem);
				val.Impressora = Impressora;
				val.MostrarOnline = MostrarOnline;
				val.MostrarPedirMenu = MostrarPedirMenu;
				val.MostrarDeliveryOnline = MostrarDeliveryOnline;
				val.Caracteristica = Caracteristica;
				((ViewModelDtoBase<CardapioDto>)(object)this).AtualizarDto<CardapioDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new CardapioFormulario(((ViewModelDtoBase<CardapioDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Cardápios";
		}

		public string ToTituloFicha()
		{
			return "Cardápio";
		}

		protected virtual void OnAlterouUsaDeliveryOnline(bool e)
		{
			this.AlterouUsaDeliveryOnline?.Invoke(this, e);
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}
	}
	public class ComponentesFichaViewModel : ViewModelValidationBase
	{
		private readonly ItemCardapioViewModel _item;

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

		public ComponentesFichaViewModel(ItemCardapioViewModel item, IEnumerable<ComponenteDto> componentes)
		{
			_item = item;
			Componentes = new Lista<ComponenteDto>();
			Componentes.Adicionar(componentes);
		}

		public void Gravar()
		{
			((ViewModelDtoBase<ItemCardapioDto>)_item).Dto.Componentes = Componentes.Itens;
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
	public class IfoodViewModel : ViewModelDtoBase<CardapioDto>
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

		public IfoodViewModel(CardapioDto dto)
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
			CardapioDto val = (CardapioDto)((obj is CardapioDto) ? obj : null);
			val.Ifood = new IfoodDto
			{
				ConsumoInterno = ConsumoInterno,
				Retirada = Retirada,
				Delivery = Delivery,
				TabelaPreco = TabelaPreco
			};
			base.AtualizarDto<CardapioDto>(ref val);
		}
	}
	public class ItemCardapioViewModel : ViewModelDtoBase<ItemCardapioDto>
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

		public ItemCardapioViewModel(ItemCardapioDto dto, Action propertyChanged)
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
	public class ItensCardapioAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ItensCardapioAbaViewModel(IViewModelDtoBase viewModel)
			: base("Itens do Cardápio", "Produtos e observações", viewModel)
		{
		}
	}
	public class ItensCardapioViewModel : ViewModelDtoBase<CardapioDto>
	{
		[Valid]
		public Lista<ItemCardapioViewModel> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemCardapioViewModel>>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemCardapioViewModel>>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens), value);
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
					if (Itens.Itens.Select((ItemCardapioViewModel y) => ((ViewModelDtoBase<ItemCardapioDto>)y).Dto.ProdutoEmpresaReferencia).Contains(((EntityBaseDto)item).Id) && DialogoFactory.MostrarDialogoSimNao($"Produto '{item.ProdutoServico.Descricao}' já existente no cardápio, deseja adicionar novamente?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
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

		public override bool Valid
		{
			get
			{
				if (((Validation)this).Valid)
				{
					return FichaViewModel == null;
				}
				return false;
			}
		}

		public ICommand AdicionarCommand { get; set; }

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand ObservacoesCommand { get; set; }

		public ICommand AdicionaisCommand { get; set; }

		public ICommand GravarCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand CopiarProdutoEmpresaReferenciaItemCommand { get; set; }

		public ICommand SelecionarItemUtilizandoBarraDeEspacoCommand { get; set; }

		public ICommand OrdenarCommand { get; set; }

		public ICommand ComponentesCommand { get; set; }

		public ItensCardapioViewModel()
		{
		}

		public ItensCardapioViewModel(CardapioDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Expected O, but got Unknown
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a9: Expected O, but got Unknown
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)null);
			MoverParaCimaCommand = (ICommand)new DelegateCommand((Action)MoverParaCima, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaCima() && Itens.Itens.Any((ItemCardapioViewModel x) => x.Selecionado)), (bool?)null);
			MoverParaBaixoCommand = (ICommand)new DelegateCommand((Action)MoverParaBaixo, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.PodeMoverParaBaixo() && Itens.Itens.Any((ItemCardapioViewModel x) => x.Selecionado)), (bool?)null);
			ObservacoesCommand = (ICommand)new DelegateCommand((Action)AdicionarObservacoes, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.Itens.Any((ItemCardapioViewModel x) => x.Selecionado)), (bool?)null);
			AdicionaisCommand = (ICommand)new DelegateCommand((Action)AdicionarAdicionais, (Func<bool>)(() => HelperObject.IsNotNull((object)Itens) && Itens.Itens.Any((ItemCardapioViewModel x) => x.Selecionado)), (bool?)null);
			GravarCommand = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemCardapioViewModel>((Action<ItemCardapioViewModel>)RemoverItem);
			CopiarProdutoEmpresaReferenciaItemCommand = (ICommand)new AsyncCommand<ItemCardapioViewModel>((Func<ItemCardapioViewModel, Task>)CopiarProdutoEmpresaReferenciaItem, (Func<ItemCardapioViewModel, bool>)((ItemCardapioViewModel x) => true), false, (bool?)null);
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
			ItemCardapioViewModel itemSelecionado = Itens.Itens.FirstOrDefault((ItemCardapioViewModel x) => x.Selecionado);
			Itens.ItemSelecionado = itemSelecionado;
			Itens.MoverParaCima();
			Itens.ItemSelecionado = itemSelecionado;
		}

		private void MoverParaBaixo()
		{
			ItemCardapioViewModel itemSelecionado = Itens.Itens.FirstOrDefault((ItemCardapioViewModel x) => x.Selecionado);
			Itens.ItemSelecionado = itemSelecionado;
			Itens.MoverParaBaixo();
			Itens.ItemSelecionado = itemSelecionado;
		}

		private bool PodeEditarComponentes()
		{
			if (HelperObject.IsNotNull((object)Itens) && Itens.PossuiItemSelecionado && Itens.Itens.Count((ItemCardapioViewModel item) => item.Selecionado) == 1)
			{
				return ((ViewModelDtoBase<ItemCardapioDto>)Itens.ItemSelecionado).Dto.Componentes.Any();
			}
			return false;
		}

		private void EditarComponentes()
		{
			FichaViewModel = (ViewModelValidationBase)(object)new ComponentesFichaViewModel(Itens.ItemSelecionado, ((ViewModelDtoBase<ItemCardapioDto>)Itens.ItemSelecionado).Dto.Componentes);
		}

		private void RemoverItem(ItemCardapioViewModel obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemCardapioViewModel>>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens));
		}

		private Task CopiarProdutoEmpresaReferenciaItem(ItemCardapioViewModel obj)
		{
			return Task.Factory.StartNew(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { ((ViewModelDtoBase<ItemCardapioDto>)obj).Dto.ProdutoEmpresaReferencia }))).FirstOrDefault();
					try
					{
						if (HelperObject.IsNotNull((object)val))
						{
							Clipboard.SetText(val.ProdutoServico.Descricao);
						}
					}
					catch (Exception)
					{
						try
						{
							if (HelperObject.IsNotNull((object)val))
							{
								Clipboard.SetDataObject(val.ProdutoServico.Descricao);
							}
						}
						catch (Exception)
						{
						}
					}
				});
			});
		}

		private void Gravar()
		{
			if (FichaViewModel is ObservacoesFichaViewModel)
			{
				base.Dto.Observacoes = HelperIEnumerable.ToListEmptyWhenNull<string>(HelperObject.To<ObservacoesFichaViewModel>((object)FichaViewModel).Observacoes);
				foreach (ItemCardapioViewModel item in Itens.Itens.Where((ItemCardapioViewModel x) => x.Selecionado))
				{
					((ViewModelDtoBase<ItemCardapioDto>)item).Dto.MinimoObservacao = HelperObject.To<ObservacoesFichaViewModel>((object)FichaViewModel).Minimo;
					((ViewModelDtoBase<ItemCardapioDto>)item).Dto.MaximoObservacao = HelperObject.To<ObservacoesFichaViewModel>((object)FichaViewModel).Maximo;
				}
			}
			if (FichaViewModel is AdicionaisFichaViewModel)
			{
				HelperObject.To<AdicionaisFichaViewModel>((object)FichaViewModel).Gravar();
			}
			if (FichaViewModel is ComponentesFichaViewModel)
			{
				HelperObject.To<ComponentesFichaViewModel>((object)FichaViewModel).Gravar();
			}
			FichaViewModel.Dispose();
			FichaViewModel = null;
		}

		private void AdicionarObservacoes()
		{
			FichaViewModel = (ViewModelValidationBase)(object)new ObservacoesFichaViewModel(base.Dto, HelperIEnumerable.ToListEmptyWhenNull<ItemCardapioViewModel>(Itens.Itens.Where((ItemCardapioViewModel x) => x.Selecionado)));
		}

		private void AdicionarAdicionais()
		{
			FichaViewModel = (ViewModelValidationBase)(object)new AdicionaisFichaViewModel(HelperIEnumerable.ToListEmptyWhenNull<ItemCardapioViewModel>(Itens.Itens.Where((ItemCardapioViewModel x) => x.Selecionado)));
		}

		private void OrdenarAlfabetico()
		{
			Itens.Ordenar((Expression<Func<ItemCardapioViewModel, object>>)((ItemCardapioViewModel x) => ((ViewModelDtoBase<ItemCardapioDto>)x).Dto.DescricaoTela), (Expression<Func<ItemCardapioViewModel, object>>)null);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemCardapioViewModel>>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens));
		}

		private void Adicionar()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			foreach (ProdutoServicoEmpresaDto iten in Produtos.Itens)
			{
				Itens.Adicionar(new ItemCardapioViewModel(new ItemCardapioDto
				{
					ProdutoEmpresaReferencia = ((EntityBaseDto)iten).Id,
					DescricaoTela = iten.ProdutoServico.Descricao,
					Quantidade = Quantidade,
					Componentes = iten.ProdutoServico.Produto.Componentes,
					Visivel = true
				}, MudouItemSelecionado));
			}
			Produtos = new Lista<ProdutoServicoEmpresaDto>();
			Quantidade = 0m;
			((BindableBase)this).RaisePropertiesChanged<Lista<ItemCardapioViewModel>, string>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens), (Expression<Func<string>>)(() => DescricaoTela));
		}

		private bool PodeAdicionar()
		{
			if (!string.IsNullOrWhiteSpace(DescricaoTela))
			{
				return Quantidade > 0m;
			}
			return false;
		}

		public override void ProjectedAsViewModel()
		{
			Produtos = new Lista<ProdutoServicoEmpresaDto>();
			Itens = new Lista<ItemCardapioViewModel>();
			Itens.Adicionar(from x in base.Dto.ItensCardapio
				orderby x.Ordem
				select x into dto
				select new ItemCardapioViewModel(dto, MudouItemSelecionado));
			((BindableBase)this).RaisePropertyChanged<Lista<ItemCardapioViewModel>>((Expression<Func<Lista<ItemCardapioViewModel>>>)(() => Itens));
			Itens.PosicionarNoPrimeiro();
		}

		private void MudouItemSelecionado()
		{
			Itens.ItemSelecionado = Itens.Itens.FirstOrDefault((ItemCardapioViewModel x) => x.Selecionado);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CardapioDto val = (CardapioDto)((obj is CardapioDto) ? obj : null);
			if (HelperObject.IsNull((object)val))
			{
				return;
			}
			foreach (ItemCardapioViewModel iten in Itens.Itens)
			{
				((ViewModelDtoBase<ItemCardapioDto>)iten).Dto.Ordem = Itens.Indice(iten) + 1;
			}
			val.ItensCardapio = HelperIEnumerable.ToListEmptyWhenNull<ItemCardapioDto>(Itens.Itens.Select((ItemCardapioViewModel x) => ((ViewModelDtoBase<ItemCardapioDto>)x).Dto));
			val.Observacoes = base.Dto.Observacoes;
			base.AtualizarDto<CardapioDto>(ref val);
		}
	}
	public class ObservacoesFichaViewModel : ViewModelValidationBase
	{
		private readonly IEnumerable<ItemCardapioViewModel> _itens;

		public string Observacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Observacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Observacao), value);
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

		public Lista<string> ObservacoesCadastradas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => ObservacoesCadastradas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => ObservacoesCadastradas), value);
			}
		}

		public Lista<string> ObservacoesSelecionadas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => ObservacoesSelecionadas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<string>>((Expression<Func<Lista<string>>>)(() => ObservacoesSelecionadas), value);
			}
		}

		public IEnumerable<string> Observacoes => ObservacoesCadastradas.Itens.Union(ObservacoesSelecionadas.Itens);

		public ICommand AdicionarObservacaoCommand { get; set; }

		public ICommand SelecionarObservacaoCommand { get; set; }

		public ICommand RemoverObservacaoCommand { get; set; }

		public ObservacoesFichaViewModel(CardapioDto dto, IEnumerable<ItemCardapioViewModel> itens)
		{
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Expected O, but got Unknown
			_itens = HelperIEnumerable.ToListEmptyWhenNull<ItemCardapioViewModel>(itens);
			ObservacoesCadastradas = new Lista<string>();
			ObservacoesSelecionadas = new Lista<string>();
			List<string> list = HelperIEnumerable.ToListEmptyWhenNull<string>(_itens.SelectMany((ItemCardapioViewModel x) => ((ViewModelDtoBase<ItemCardapioDto>)x).Dto.Observacoes).Distinct());
			List<string> list2 = HelperIEnumerable.ToListEmptyWhenNull<string>(dto.Observacoes.Except(list));
			Minimo = ((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MinimoObservacao;
			Maximo = (((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MaximoObservacao.Equals(0) ? 1 : ((ViewModelDtoBase<ItemCardapioDto>)_itens.FirstOrDefault()).Dto.MaximoObservacao);
			ObservacoesCadastradas.Adicionar((IEnumerable<string>)list2);
			ObservacoesSelecionadas.Adicionar((IEnumerable<string>)list);
			ObservacoesSelecionadas.AlterouEvent += ObservacoesSelecionadasOnAlterouEvent;
			AdicionarObservacaoCommand = (ICommand)new DelegateCommand((Action)AdicionarObservacao, (Func<bool>)PodeAdicionarObservacao, (bool?)null);
			SelecionarObservacaoCommand = (ICommand)new DelegateCommand((Action)SelecionarObservacao, (Func<bool>)PodeSelecionarObservacao, (bool?)null);
			RemoverObservacaoCommand = (ICommand)new DelegateCommand((Action)RemoverObservacao, (Func<bool>)PodeRemoverObservacao, (bool?)null);
		}

		private void AdicionarObservacao()
		{
			ObservacoesCadastradas.Adicionar(HelperString.CopiarTexto(Observacao, 190));
			Observacao = string.Empty;
		}

		private bool PodeAdicionarObservacao()
		{
			return !string.IsNullOrWhiteSpace(Observacao);
		}

		private void SelecionarObservacao()
		{
			ObservacoesSelecionadas.Adicionar(ObservacoesCadastradas.ItemSelecionado);
			ObservacoesCadastradas.Remover(ObservacoesCadastradas.ItemSelecionado);
		}

		private bool PodeSelecionarObservacao()
		{
			return ObservacoesCadastradas.PossuiItemSelecionado;
		}

		private void RemoverObservacao()
		{
			ObservacoesCadastradas.Adicionar(ObservacoesSelecionadas.ItemSelecionado);
			ObservacoesSelecionadas.Remover(ObservacoesSelecionadas.ItemSelecionado);
		}

		private bool PodeRemoverObservacao()
		{
			return ObservacoesSelecionadas.PossuiItemSelecionado;
		}

		private void ObservacoesSelecionadasOnAlterouEvent(object sender, IEnumerable<string> enumerable)
		{
			foreach (ItemCardapioViewModel iten in _itens)
			{
				((ViewModelDtoBase<ItemCardapioDto>)iten).Dto.Observacoes = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)ObservacoesSelecionadas.Itens);
			}
		}

		public override void Dispose()
		{
			ObservacoesSelecionadas.AlterouEvent -= ObservacoesSelecionadasOnAlterouEvent;
			((ViewModelValidationBase)ObservacoesSelecionadas).Dispose();
			((ViewModelValidationBase)ObservacoesCadastradas).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class OrdenarCardapioFichaViewModel : NavigationBase
	{
		private readonly IWService _service;

		public Lista<ItemOrdemCardapioDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemOrdemCardapioDto>>((Expression<Func<Lista<ItemOrdemCardapioDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemOrdemCardapioDto>>((Expression<Func<Lista<ItemOrdemCardapioDto>>>)(() => Itens), value);
			}
		}

		public ICommand MoverParaCimaCommand { get; set; }

		public ICommand MoverParaBaixoCommand { get; set; }

		public ICommand GravarCommand { get; set; }

		public OrdenarCardapioFichaViewModel(IWService service)
			: base("Ordenar cardápios")
		{
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			_service = service;
			Itens = new Lista<ItemOrdemCardapioDto>();
			IEnumerable<ItemOrdemCardapioDto> source = service.Canal<ICardapioModuleService, IEnumerable<ItemOrdemCardapioDto>>((Func<ICardapioModuleService, IEnumerable<ItemOrdemCardapioDto>>)((ICardapioModuleService x) => x.ObterOrdemCardapio()));
			Itens.Adicionar((IEnumerable<ItemOrdemCardapioDto>)source.OrderBy((ItemOrdemCardapioDto x) => x.Ordem));
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
			foreach (ItemOrdemCardapioDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			_service.Canal<ICardapioModuleService>((Action<ICardapioModuleService>)delegate(ICardapioModuleService x)
			{
				x.SalvarOrdemCardapio((IEnumerable<ItemOrdemCardapioDto>)Itens.Itens);
			});
			((NavigationBase)this).FecharView();
		}
	}
}
namespace Otus.Modulo.Cardapios.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Cardapios.Properties.Resources", typeof(Resources).Assembly);
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

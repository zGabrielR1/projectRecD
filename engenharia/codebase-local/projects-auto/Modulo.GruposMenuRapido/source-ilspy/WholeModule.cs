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
using DigisatServer.Application.Main.Dto.Cadastros.MenusRapido;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.GruposMenuRapido.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GruposMenuRapido")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GruposMenuRapido")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("3208a41c-5fb6-420c-ac6e-7bcb2a2a56a4")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GruposMenuRapido
{
	[Module(ModuleName = "GrupoMenuRapidoModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class GrupoMenuRapidoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GrupoMenuRapidoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GrupoMenuListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GruposMenuView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GrupoMenuFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GrupoMenuFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GruposMenuRapido.Views
{
	public class GruposMenuView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GruposMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/gruposmenuview.xaml", UriKind.Relative);
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
	public class GrupoMenuFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GrupoMenuFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaadicionarview.xaml", UriKind.Relative);
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
	public class GrupoMenuFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GrupoMenuFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaatualizarview.xaml", UriKind.Relative);
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
	public class GrupoMenuListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GrupoMenuListaView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenulistaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GruposMenuRapido.Views.GrupoMenuFichaViewDetalhes
{
	public class AdicionalGrupoMenuView : UserControl, IComponentConnector
	{
		internal ButtonInfo ParametroProduto;

		private bool _contentLoaded;

		public AdicionalGrupoMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaviewdetalhes/adicionalgrupomenuview.xaml", UriKind.Relative);
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
				ParametroProduto = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class BonificacaoGrupoMenuView : UserControl, IComponentConnector
	{
		internal ButtonInfo ParametroProduto;

		private bool _contentLoaded;

		public BonificacaoGrupoMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaviewdetalhes/bonificacaogrupomenuview.xaml", UriKind.Relative);
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
				ParametroProduto = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class GrupoMenuView : UserControl, IComponentConnector
	{
		internal ScaleTransform Escala;

		internal ButtonInfo ParameterRemoverTabelaPreco;

		internal ButtonInfo ParameterTabelaPreco;

		private bool _contentLoaded;

		public GrupoMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaviewdetalhes/grupomenuview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				Escala = (ScaleTransform)target;
				break;
			case 2:
				ParameterRemoverTabelaPreco = (ButtonInfo)target;
				break;
			case 3:
				ParameterTabelaPreco = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class PredefinicaoGrupoMenuView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PredefinicaoGrupoMenuView()
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
				Uri resourceLocator = new Uri("/Modulo.GruposMenuRapido;component/views/grupomenufichaviewdetalhes/predefinicaogrupomenuview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GruposMenuRapido.ViewModels
{
	public class AdicionalGrupoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AdicionalGrupoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Adicionais", "Vinculação de adicionais", viewModel)
		{
		}
	}
	public class AdicionalGrupoViewModel : ViewModelDtoBase<GrupoMenuRapidoDto>
	{
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

		public virtual SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>> ProdutoServicoCommandParameter => new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> produtos)
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
			if (produtos.Count() > 1)
			{
				Lista<ItemAdicionalMenuRapidoDto> val = new Lista<ItemAdicionalMenuRapidoDto>();
				foreach (ProdutoServicoEmpresaDto produto in produtos)
				{
					try
					{
						if ((int)produto.ProdutoServico.Tipo == 5)
						{
							throw new Exception("Selecione um produto que não seja do tipo 'Combustível'!");
						}
						if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produto.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produto.ProdutoEmpresa).Estoque.IsNormal)
						{
							throw new Exception("Selecione um produto com estoque do tipo 'Normal'!");
						}
						if (!produto.Vendavel)
						{
							throw new Exception("Selecione um produto 'Vendável'!");
						}
						val.Adicionar(new ItemAdicionalMenuRapidoDto
						{
							ProdutoEmpresaReferencia = ((EntityBaseDto)produto).Id,
							DescricaoTela = produto.ProdutoServico.Descricao,
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
				ProdutoEmpresa = null;
				QuantidadeMinima = 0m;
				QuantidadeMaxima = 0m;
				((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
				FocoNaBuscaProduto = !FocoNaBuscaProduto;
			}
			else if ((int)produtos.FirstOrDefault().ProdutoServico.Tipo == 5)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto que não seja do tipo 'Combustível'!", false, HorizontalAlignment.Center);
			}
			else if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtos.FirstOrDefault().ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtos.FirstOrDefault().ProdutoEmpresa).Estoque.IsNormal)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto com estoque do tipo 'Normal'!", false, HorizontalAlignment.Center);
			}
			else if (!produtos.FirstOrDefault().Vendavel)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto 'Vendável'!", false, HorizontalAlignment.Center);
			}
			else
			{
				ProdutoEmpresa = produtos.FirstOrDefault();
				DescricaoTela = HelperString.CopiarTexto(ProdutoEmpresa.ProdutoServico.Descricao, 30);
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

		public AdicionalGrupoViewModel(GrupoMenuRapidoDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
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
			Minimo = dto.QuantidadeMinima;
			Maximo = dto.QuantidadeMaxima;
			UtilizarItemMaiorValor = dto.UtilizarItemMaiorValor;
		}

		private void RemoverItem(ItemAdicionalMenuRapidoDto obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
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

		public override void ProjectedAsViewModel()
		{
			Itens = new Lista<ItemAdicionalMenuRapidoDto>();
			Itens.Adicionar((IEnumerable<ItemAdicionalMenuRapidoDto>)(from x in base.Dto.Adicionais
				group x by x.ProdutoEmpresaReferencia into g
				select g.FirstOrDefault() into x
				orderby x.Ordem
				select x));
			Itens.PosicionarNoPrimeiro();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			foreach (ItemAdicionalMenuRapidoDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			val.Adicionais = Itens.Itens;
			val.QuantidadeMinima = Minimo;
			val.QuantidadeMaxima = Maximo;
			val.UtilizarItemMaiorValor = UtilizarItemMaiorValor;
			base.AtualizarDto<GrupoMenuRapidoDto>(ref val);
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class BonificacaoGrupoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public BonificacaoGrupoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Bonificações", "Vinculação de bonificações", viewModel)
		{
		}
	}
	public class BonificacaoGrupoViewModel : ViewModelDtoBase<GrupoMenuRapidoDto>
	{
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

		public decimal ValorBonificacaoPorItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorBonificacaoPorItem));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorBonificacaoPorItem), value);
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

		public virtual SelecaoCommandParameter<ProdutoServicoEmpresaDto> ProdutoServicoCommandParameter => new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Invalid comparison between Unknown and I4
			if ((int)produtoServicoEmpresa.ProdutoServico.Tipo == 5)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto que não seja do tipo 'Combustível'!", false, HorizontalAlignment.Center);
			}
			else if (!HelperObject.Is<ProdutoEmpresaDtoNull>((object)produtoServicoEmpresa.ProdutoEmpresa) && !HelperObject.To<ProdutoEmpresaDto>((object)produtoServicoEmpresa.ProdutoEmpresa).Estoque.IsNormal)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto com estoque do tipo 'Normal'!", false, HorizontalAlignment.Center);
			}
			else if (!produtoServicoEmpresa.Vendavel)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um produto 'Vendável'!", false, HorizontalAlignment.Center);
			}
			else
			{
				ProdutoEmpresa = produtoServicoEmpresa;
				DescricaoTela = HelperString.CopiarTexto(ProdutoEmpresa.ProdutoServico.Descricao, 30);
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

		public BonificacaoGrupoViewModel(GrupoMenuRapidoDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
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
			Minimo = dto.QuantidadeMinima;
			Maximo = dto.QuantidadeMaxima;
			ValorBonificacaoPorItem = dto.ValorBonificacaoPorItem;
		}

		private void RemoverItem(ItemAdicionalMenuRapidoDto obj)
		{
			Itens.Remover(obj);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemAdicionalMenuRapidoDto>>((Expression<Func<Lista<ItemAdicionalMenuRapidoDto>>>)(() => Itens));
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

		public override void ProjectedAsViewModel()
		{
			Itens = new Lista<ItemAdicionalMenuRapidoDto>();
			Itens.Adicionar((IEnumerable<ItemAdicionalMenuRapidoDto>)(from x in base.Dto.Bonificacoes
				group x by x.ProdutoEmpresaReferencia into g
				select g.FirstOrDefault() into x
				orderby x.Ordem
				select x));
			Itens.PosicionarNoPrimeiro();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			foreach (ItemAdicionalMenuRapidoDto iten in Itens.Itens)
			{
				iten.Ordem = Itens.Indice(iten) + 1;
			}
			val.Bonificacoes = Itens.Itens;
			val.QuantidadeMinima = Minimo;
			val.QuantidadeMaxima = Maximo;
			val.ValorBonificacaoPorItem = ValorBonificacaoPorItem;
			base.AtualizarDto<GrupoMenuRapidoDto>(ref val);
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class GrupoMenuAbaViewModel : AbaPrincipalViewModelBase
	{
		public GrupoMenuAbaViewModel(IViewModelPrincipal viewModel)
			: base("Grupos complementos", "Dados principais", viewModel)
		{
		}
	}
	public class GrupoMenuFichaAdicionarViewModel : FichaBaseAdicionar<GrupoMenuRapidoDto, GrupoMenuViewModel, IGrupoMenuModuleService>
	{
		public GrupoMenuFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class GrupoMenuFichaAtualizarViewModel : FichaBaseAtualizar<GrupoMenuRapidoDto, GrupoMenuViewModel, IGrupoMenuModuleService>
	{
		public GrupoMenuFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}

		protected override async Task<GrupoMenuRapidoDto> Persistir(IFormularioEntity formulario)
		{
			GrupoMenuRapidoDto val = await DadosSistema.GetService().Canal<IGrupoMenuModuleService, Task<GrupoMenuRapidoDto>>((Func<IGrupoMenuModuleService, Task<GrupoMenuRapidoDto>>)((IGrupoMenuModuleService service) => ((IAtualizar<GrupoMenuRapidoDto>)(object)service).AtualizarAsync(formulario.ToDto<GrupoMenuRapidoDto>())));
			((FichaBasePersistir<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).SelecaoNavigateParameterCallBack?.CallBack(val);
			return val;
		}
	}
	public class GrupoMenuFormulario : FormularioEntity<GrupoMenuRapidoDto>
	{
		private readonly GrupoMenuViewModel _grupoMenuViewModel;

		public GrupoMenuFormulario(GrupoMenuRapidoDto dto)
			: base(dto)
		{
			_grupoMenuViewModel = new GrupoMenuViewModel(base.Dto);
			_grupoMenuViewModel.AlterouTipoGrupoEvent += GrupoMenuViewModelOnAlterouTipoGrupoEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new GrupoMenuAbaViewModel((IViewModelPrincipal)(object)_grupoMenuViewModel) });
		}

		private void GrupoMenuViewModelOnAlterouTipoGrupoEvent(object sender, TipoGrupoMenu tipoGrupoMenu)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Invalid comparison between Unknown and I4
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Invalid comparison between Unknown and I4
			if (HelperObject.IsNull((object)tipoGrupoMenu))
			{
				return;
			}
			foreach (AbaAuxiliarViewModelBase item in HelperIEnumerable.ToListEmptyWhenNull<AbaAuxiliarViewModelBase>(((Formulario)this).Abas.OfType<AbaAuxiliarViewModelBase>()))
			{
				((Formulario)this).RemoverAba((IAbaViewModelBase)(object)item);
			}
			if ((int)tipoGrupoMenu == 0)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AdicionalGrupoAbaViewModel((IViewModelDtoBase)(object)new AdicionalGrupoViewModel(base.Dto)) });
			}
			if ((int)tipoGrupoMenu == 1)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new PredefinicaoGrupoAbaViewModel((IViewModelDtoBase)(object)new PredefinicaoGrupoViewModel(base.Dto)) });
			}
			if ((int)tipoGrupoMenu == 2)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new BonificacaoGrupoAbaViewModel((IViewModelDtoBase)(object)new BonificacaoGrupoViewModel(base.Dto)) });
			}
		}

		public override void Dispose()
		{
			_grupoMenuViewModel.AlterouTipoGrupoEvent -= GrupoMenuViewModelOnAlterouTipoGrupoEvent;
			((Formulario)this).Dispose();
		}
	}
	public class GrupoMenuListaViewModel : ListaSelecaoReferencia<GrupoMenuRapidoDto, GrupoMenuViewModel, IGrupoMenuModuleService, GrupoMenu>
	{
		private readonly IModuleManager _moduleManager;

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public ICommand AtualizarMenuRapidoCommand { get; set; }

		public GrupoMenuListaViewModel(IWService service, ISessao sessao, IModuleManager moduleManager, IEntityAtivosFiltroGrupoMenuRapido<GrupoMenuRapidoDto, IGrupoMenuModuleService> ativos, IEntityInativosFiltroGrupoMenuRapido<GrupoMenuRapidoDto, IGrupoMenuModuleService> inativos)
			: base(sessao, typeof(GrupoMenuFichaAdicionarView).Name, typeof(GrupoMenuFichaAtualizarView).Name, service, new IEntityBase<GrupoMenuRapidoDto, IGrupoMenuModuleService>[2]
			{
				(IEntityBase<GrupoMenuRapidoDto, IGrupoMenuModuleService>)(object)ativos,
				(IEntityBase<GrupoMenuRapidoDto, IGrupoMenuModuleService>)(object)inativos
			})
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			_moduleManager = moduleManager;
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			DuplicarCommand = (ICommand)new DelegateCommand((Action)Duplicar, (Func<bool>)PodeDuplicar, (bool?)null);
			AtualizarMenuRapidoCommand = (ICommand)new DelegateCommand((Action)AtualizarMenuRapido, (Func<bool>)PodeAtualizarMenuRapido, (bool?)null);
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
			if (((ListaSelecaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel, GrupoMenu>)(object)this).Sessao.VerificarPermissao<GrupoMenu, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		protected async void Duplicar()
		{
			GrupoMenuRapidoDto dto = ((ViewModelDtoBase<GrupoMenuRapidoDto>)((ListaBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).Itens.Selecionado).Dto;
			_ = dto;
			dto = await ((ListaReferenciaBase<GrupoMenuRapidoDto, GrupoMenuViewModel, IGrupoMenuModuleService, GrupoMenu>)(object)this)._service.Canal<IGrupoMenuModuleService, Task<GrupoMenuRapidoDto>>((Func<IGrupoMenuModuleService, Task<GrupoMenuRapidoDto>>)((IGrupoMenuModuleService service) => ((IReferenciaAtualizarService<GrupoMenuRapidoDto>)(object)service).AtualizarDtoAsync(dto)));
			dto.Duplicar();
			GrupoMenuListaViewModel grupoMenuListaViewModel = this;
			string viewAdicionarFicha = ((ListaManutencaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel, GrupoMenu>)(object)this).ViewAdicionarFicha;
			Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
			{
				if (navigationResult.Result == true)
				{
					((ListaBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).Itens.Estado.AlterarEdicao();
				}
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("GrupoMenuViewModel", (object)new GrupoMenuViewModel(dto));
			((NavigationBase)grupoMenuListaViewModel).Navegar(viewAdicionarFicha, obj, val);
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
			object obj = regionNavigationEventArgs.NavigationContext.Parameters["GrupoMenuRapidoDto"];
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			if (!(regionNavigationEventArgs.Uri.OriginalString != typeof(GrupoMenuListaView).Name) && val != null)
			{
				((ListaBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).Itens.Limpar();
				((ListaBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).Itens.Adicionar<GrupoMenuRapidoDto>((IEnumerable<GrupoMenuRapidoDto>)new List<GrupoMenuRapidoDto> { val });
			}
		}

		private bool PodeDuplicar()
		{
			return ((ListaSelecaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel, GrupoMenu>)(object)this).Sessao.VerificarPermissao<GrupoMenu, Adicionar>();
		}

		private bool PodeAtualizarMenuRapido()
		{
			if (((ListaSelecaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel, GrupoMenu>)(object)this).Sessao.VerificarPermissao<GrupoMenu, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		protected void AtualizarMenuRapido()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			GrupoMenuRapidoDto dto = ((ViewModelDtoBase<GrupoMenuRapidoDto>)((ListaBase<GrupoMenuRapidoDto, GrupoMenuViewModel>)(object)this).Itens.Selecionado).Dto;
			NavigationParameters val = new NavigationParameters();
			val.Add("GrupoMenuRapidoDto", (object)dto);
			NavigationParameters val2 = val;
			_moduleManager.LoadModule("AtualizacaoGrupoComplementoModulo");
			((NavigationBase)this).Navegar("AtualizacaoGrupoComplementoFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}
	}
	public class GrupoMenuViewModel : ViewModelDtoBase<GrupoMenuRapidoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
		public bool TabelaPrecoVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TabelaPrecoVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TabelaPrecoVisivel), value);
			}
		}

		public bool Ativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Ativo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Ativo), value);
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

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Descrição' é obrigatório.")]
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

		public Lista<KeyValuePair<string, TipoGrupoMenu>> TiposGrupo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, TipoGrupoMenu>>>((Expression<Func<Lista<KeyValuePair<string, TipoGrupoMenu>>>>)(() => TiposGrupo));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, TipoGrupoMenu>>>((Expression<Func<Lista<KeyValuePair<string, TipoGrupoMenu>>>>)(() => TiposGrupo), value);
			}
		}

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

		public IEnumerable<string> ItensToText
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<string>>((Expression<Func<IEnumerable<string>>>)(() => ItensToText));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<string>>((Expression<Func<IEnumerable<string>>>)(() => ItensToText), value);
			}
		}

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

		public DelegateCommand RemoverTabelaPrecoCommand { get; set; }

		public event EventHandler<TipoGrupoMenu> AlterouTipoGrupoEvent;

		public GrupoMenuViewModel()
		{
		}

		public GrupoMenuViewModel(GrupoMenuRapidoDto dto)
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
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Invalid comparison between Unknown and I4
			Ativo = ((EntityDto)base.Dto).Ativo;
			Descricao = base.Dto.Descricao;
			Imagem = HelperImage.ToBitmapSource(HelperImage.Decompress(base.Dto.Imagem));
			TiposGrupo = new Lista<KeyValuePair<string, TipoGrupoMenu>>();
			TiposGrupo.AdicionarSemPosicionar((IEnumerable<KeyValuePair<string, TipoGrupoMenu>>)HelperEnum.ObterParDeValores<TipoGrupoMenu>());
			TiposGrupo.AlterouItemSelecionadoEvent += TiposGrupoOnAlterouItemSelecionadoEvent;
			TiposGrupo.Posicionar((Expression<Func<KeyValuePair<string, TipoGrupoMenu>, bool>>)((KeyValuePair<string, TipoGrupoMenu> x) => ((object)x.Value).Equals((object)((ViewModelDtoBase<GrupoMenuRapidoDto>)this).Dto.TipoGrupo)));
			Observacao = base.Dto.Observacao;
			object itensToText;
			if ((int)base.Dto.TipoGrupo != 0)
			{
				if ((int)base.Dto.TipoGrupo != 1)
				{
					IEnumerable<ItemAdicionalMenuRapidoDto> bonificacoes = base.Dto.Bonificacoes;
					itensToText = ((bonificacoes != null) ? HelperIEnumerable.ToListEmptyWhenNull<string>(bonificacoes.Select((ItemAdicionalMenuRapidoDto x) => x.DescricaoTela)) : null);
				}
				else
				{
					itensToText = HelperIEnumerable.ToListEmptyWhenNull<string>(base.Dto.PredefinicoesSelecionadas);
				}
			}
			else
			{
				IEnumerable<ItemAdicionalMenuRapidoDto> adicionais = base.Dto.Adicionais;
				itensToText = ((adicionais != null) ? HelperIEnumerable.ToListEmptyWhenNull<string>(adicionais.Select((ItemAdicionalMenuRapidoDto x) => x.DescricaoTela)) : null);
			}
			ItensToText = (IEnumerable<string>)itensToText;
			TabelaPreco = base.Dto.TabelaPreco;
		}

		private void TiposGrupoOnAlterouItemSelecionadoEvent(object sender, KeyValuePair<string, TipoGrupoMenu> keyValuePair)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Invalid comparison between Unknown and I4
			this.AlterouTipoGrupoEvent?.Invoke(this, keyValuePair.Value);
			TabelaPrecoVisivel = (int)keyValuePair.Value != 1;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			object obj = dto;
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			if (val != null)
			{
				((EntityDto)val).Ativo = Ativo;
				val.Descricao = Descricao;
				val.Imagem = HelperImage.Compress(Imagem);
				val.TipoGrupo = TiposGrupo.ItemSelecionado.Value;
				val.Observacao = Observacao;
				val.TabelaPreco = TabelaPreco;
				base.AtualizarDto<GrupoMenuRapidoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new GrupoMenuFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Grupos de complementos";
		}

		public string ToTituloFicha()
		{
			return "Grupos de complementos";
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}

		public override void Dispose()
		{
			Lista<KeyValuePair<string, TipoGrupoMenu>> tiposGrupo = TiposGrupo;
			if (tiposGrupo != null)
			{
				((ViewModelValidationBase)tiposGrupo).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class GruposMenuViewModel : ModuloBase
	{
		public GruposMenuViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGrupoMenuRegion, typeof(GrupoMenuListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class PredefinicaoGrupoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public PredefinicaoGrupoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Predefinições", "Vinculação de observações", viewModel)
		{
		}
	}
	public class PredefinicaoGrupoViewModel : ViewModelDtoBase<GrupoMenuRapidoDto>
	{
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

		public PredefinicaoGrupoViewModel(GrupoMenuRapidoDto dto)
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
			Minimo = dto.QuantidadeMinima;
			Maximo = dto.QuantidadeMaxima;
		}

		private void AdicionarPredefinicao()
		{
			PredefinicoesCadastradas.Adicionar(Predefinicao);
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
			base.Dto.PredefinicoesSelecionadas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
		}

		public override void ProjectedAsViewModel()
		{
			PredefinicoesCadastradas = new Lista<string>();
			PredefinicoesSelecionadas = new Lista<string>();
			List<string> list = HelperIEnumerable.ToListEmptyWhenNull<string>(base.Dto.PredefinicoesSelecionadas.Distinct());
			List<string> list2 = HelperIEnumerable.ToListEmptyWhenNull<string>(base.Dto.PredefinicoesCadastradas.Except(list));
			Minimo = base.Dto.QuantidadeMinima;
			Maximo = (base.Dto.QuantidadeMaxima.Equals(0) ? 1 : base.Dto.QuantidadeMaxima);
			PredefinicoesCadastradas.Adicionar((IEnumerable<string>)list2);
			PredefinicoesSelecionadas.Adicionar((IEnumerable<string>)list);
			PredefinicoesSelecionadas.AlterouEvent += PredefinicoesSelecionadasOnAlterouEvent;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GrupoMenuRapidoDto val = (GrupoMenuRapidoDto)((obj is GrupoMenuRapidoDto) ? obj : null);
			val.PredefinicoesCadastradas = Predefinicoes;
			val.PredefinicoesSelecionadas = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)PredefinicoesSelecionadas.Itens);
			val.QuantidadeMinima = Minimo;
			val.QuantidadeMaxima = Maximo;
			base.AtualizarDto<GrupoMenuRapidoDto>(ref val);
		}

		public override void Dispose()
		{
			PredefinicoesSelecionadas.AlterouEvent -= PredefinicoesSelecionadasOnAlterouEvent;
			((ViewModelValidationBase)PredefinicoesSelecionadas).Dispose();
			((ViewModelValidationBase)PredefinicoesCadastradas).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
}

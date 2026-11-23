using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Kits;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Shopees;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.ShopeesModulo;
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
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Anuncios;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Atributos;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Categorias;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Logistica;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Marcas;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.TabelasMedidas;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiShopee.Classes.V2_0.Variacoes;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.AnunciosShopee.ViewModels.Atributos;
using Otus.Modulo.AnunciosShopee.ViewModels.Variacoes;
using Otus.Modulo.AnunciosShopee.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.AnunciosShopee")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.AnunciosShopee")]
[assembly: AssemblyCopyright("Copyright ©  2025")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AnunciosShopee
{
	[Module(ModuleName = "AnuncioShopeeModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("TabelaPrecoModulo")]
	[ModuleDependency("ShopeeModulo")]
	public class AnuncioShopeeModulo : IModule
	{
		private readonly IUnityContainer _container;

		protected const string DescricaoParametroApi = "ShopeeApiService";

		public AnuncioShopeeModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AnunciosShopeeView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioShopeeListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioShopeeFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioShopeeFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AnuncioShopeeImportarFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AnunciosShopee.Views
{
	public class AnuncioShopeeFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public AnuncioShopeeFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaadicionarview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public AnuncioShopeeFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaatualizarview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeImportarFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeImportarFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeeimportarfichaview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeListaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeelistaview.xaml", UriKind.Relative);
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
	public class AnunciosShopeeView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnunciosShopeeView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anunciosshopeeview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AnunciosShopee.Views.AnuncioShopeeFichaViewDetalhes
{
	public class AnuncioShopeeVariacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeVariacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeevariacaoview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeAtacadoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeAtacadoView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeeatacadoview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeEntregaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeeentregaview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeCategoriaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeCategoriaView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeecategoriaview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeImagensView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AnuncioShopeeImagensView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeeimagensview.xaml", UriKind.Relative);
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
	public class AnuncioShopeeView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterProduto;

		internal ButtonInfo ParameterKit;

		private bool _contentLoaded;

		public AnuncioShopeeView()
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
				Uri resourceLocator = new Uri("/Modulo.AnunciosShopee;component/views/anuncioshopeefichaviewdetalhes/anuncioshopeeview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AnunciosShopee.ViewModels
{
	public class AnuncioShopeeAbaViewModel : AbaPrincipalViewModelBase
	{
		public AnuncioShopeeAbaViewModel(IViewModelPrincipal viewModel)
			: base("Anúncio", "Dados principais", viewModel)
		{
		}
	}
	public class AnuncioShopeeVariacaoViewModel : ViewModelDtoBase<AnuncioDto>
	{
		public class ModelShopeeItem
		{
			public long ModelId { get; set; }

			public string Descricao { get; set; }
		}

		private readonly AnuncioShopeeViewModel _anuncioViewModel;

		private bool _descartado;

		public bool UtilizaInformacaoCadastro
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizaInformacaoCadastro));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizaInformacaoCadastro), value);
			}
		}

		public Lista<VariacoesViewModel> Variacoes { get; protected set; } = new Lista<VariacoesViewModel>();


		public List<ImagensVariacoesViewModel> ImagensVariacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<ImagensVariacoesViewModel>>((Expression<Func<List<ImagensVariacoesViewModel>>>)(() => ImagensVariacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<List<ImagensVariacoesViewModel>>((Expression<Func<List<ImagensVariacoesViewModel>>>)(() => ImagensVariacoes), value);
			}
		}

		public override bool Valid
		{
			get
			{
				List<ImagensVariacoesViewModel> imagensVariacoes = ImagensVariacoes;
				if (imagensVariacoes == null || !imagensVariacoes.All((ImagensVariacoesViewModel x) => !HelperString.IsNullOrEmpty(x.IdImagem)))
				{
					return ImagensVariacoes?.All((ImagensVariacoesViewModel x) => HelperString.IsNullOrEmpty(x.IdImagem)) ?? false;
				}
				return true;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				if (!((Validation)this).Valid)
				{
					return new string[1] { "Devem ser selecionadas todas as imagens para as variações ou deixar todas em branco." };
				}
				return new string[0];
			}
		}

		public string ErrorMessage
		{
			get
			{
				if (HelperList.DistinctBy<VariacoesViewModel, decimal>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Func<VariacoesViewModel, decimal>)((VariacoesViewModel x) => x.Preco)).Count() != 1)
				{
					return "Caso os preços não forem iguais as opção de atacado ficarão indisponíveis.";
				}
				return string.Empty;
			}
		}

		public List<ModelShopeeItem> ModelsShopee
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<ModelShopeeItem>>((Expression<Func<List<ModelShopeeItem>>>)(() => ModelsShopee));
			}
			set
			{
				((BindableBase)this).SetProperty<List<ModelShopeeItem>>((Expression<Func<List<ModelShopeeItem>>>)(() => ModelsShopee), value);
			}
		}

		public ICommand InverterLinhaColunaCommand { get; set; }

		public event EventHandler<decimal> AlterouPrecoVariacoesEvent;

		public AnuncioShopeeVariacaoViewModel(AnuncioDto dto, AnuncioShopeeViewModel AnuncioViewModel)
			: base(dto)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			InverterLinhaColunaCommand = (ICommand)new DelegateCommand((Action)InverterLinhaColuna);
			_anuncioViewModel = AnuncioViewModel;
			_anuncioViewModel.AlterouProdutoEvent += AlterouProdutoEvent;
			_anuncioViewModel.AlterouPrecoEvent += AlterouPrecoEvent;
			_anuncioViewModel.AlterouUtilizaInformacaoCadatroEvent += AlterouUtilizaInformacaoCadatroEvent;
			ModelsShopee = new List<ModelShopeeItem>();
			CriarLinhasColunas(_anuncioViewModel.Estoque);
			UtilizaInformacaoCadastro = _anuncioViewModel.UtilizarInformacaoCadastroSincronismo;
		}

		private void AlterouPrecoEvent(object sender, decimal preco)
		{
			Variacoes.Itens.Select((VariacoesViewModel x) => x.Preco = preco);
		}

		private void AlterouProdutoEvent(object sender, EstoqueDto estoque)
		{
			Variacoes.Limpar();
			CriarLinhasColunas(estoque);
			((BindableBase)this).RaisePropertiesChanged<bool, string[]>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private void AlterouUtilizaInformacaoCadatroEvent(object sender, bool utilizaInformacaoCadastro)
		{
			UtilizaInformacaoCadastro = utilizaInformacaoCadastro;
		}

		protected virtual void OnAlterouPrecoVariacoesEvent(decimal e)
		{
			this.AlterouPrecoVariacoesEvent?.Invoke(this, e);
		}

		private void InverterLinhaColuna()
		{
			foreach (VariacoesViewModel iten in Variacoes.Itens)
			{
				string linha = iten.Linha;
				iten.Linha = iten.Coluna;
				iten.Coluna = linha;
				iten.Variacao = iten.Variacao.Reverse();
			}
			ImagensVariacoes.ForEach(delegate(ImagensVariacoesViewModel x)
			{
				x.ImagemAlterada -= ImagensOnPropertyChanged;
			});
			ImagensVariacoes = (from x in HelperList.DistinctBy<VariacoesViewModel, string>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Func<VariacoesViewModel, string>)((VariacoesViewModel x) => x.Coluna))
				select new ImagensVariacoesViewModel
				{
					Coluna = x.Coluna
				}).ToList();
			ImagensVariacoes.ForEach(delegate(ImagensVariacoesViewModel x)
			{
				x.ImagemAlterada += ImagensOnPropertyChanged;
			});
		}

		private void CriarLinhasColunas(EstoqueDto estoque)
		{
			EstoqueGradeDto val = (EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null);
			if (val != null)
			{
				bool flag = val.Grades.Select((GradeDto g) => g.Coluna.Descricao).Distinct().Count() == 1;
				int num = 0;
				int num2 = 0;
				string descricao = val.Grades.First().Linha.Descricao;
				foreach (GradeDto grade in val.Grades)
				{
					if (grade.Linha.Descricao != descricao)
					{
						descricao = grade.Linha.Descricao;
						num++;
						num2 = 0;
					}
					List<int> variacao = (flag ? new List<int> { num2 } : new List<int> { num2, num });
					Variacoes.Adicionar(new VariacoesViewModel
					{
						Sku = ((EntityBaseDto)grade.Estoque).Id,
						Nome = $"{grade.Linha.Descricao} {grade.Coluna.Descricao}",
						CodigoBarras = grade.CodigoBarras,
						Preco = base.Dto.Preco,
						QuantidadeDisponivel = HelperObject.ToInt((object)grade.Estoque.Quantidade),
						Linha = grade.Linha.Descricao,
						Coluna = grade.Coluna.Descricao,
						Variacao = variacao
					});
					num2++;
				}
			}
			ImagensVariacoes = (from x in HelperList.DistinctBy<VariacoesViewModel, string>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Func<VariacoesViewModel, string>)((VariacoesViewModel x) => x.Coluna))
				select new ImagensVariacoesViewModel
				{
					Coluna = x.Coluna
				}).ToList();
			ImagensVariacoes.ForEach(delegate(ImagensVariacoesViewModel x)
			{
				x.ImagemAlterada += ImagensOnPropertyChanged;
			});
			LinqExtensions.ForEach<VariacoesViewModel>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Action<VariacoesViewModel>)delegate(VariacoesViewModel x)
			{
				x.PrecoAlterado += VariacoesPrecoOnPropertyChanged;
			});
			((BindableBase)this).RaisePropertiesChanged<bool, string[]>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies));
		}

		private void VariacoesPrecoOnPropertyChanged(object sender, EventArgs e)
		{
			((BindableBase)this).RaisePropertiesChanged<bool, string[], string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			decimal e2 = ((HelperList.DistinctBy<VariacoesViewModel, decimal>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Func<VariacoesViewModel, decimal>)((VariacoesViewModel x) => x.Preco)).Count() == 1) ? Variacoes.Itens.FirstOrDefault().Preco : 0m);
			OnAlterouPrecoVariacoesEvent(e2);
		}

		private void ImagensOnPropertyChanged(object sender, EventArgs e)
		{
			((BindableBase)this).RaisePropertiesChanged<bool, string[]>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override async void ProjectedAsViewModel()
		{
			foreach (ModelDto variaco in ((ViewModelDtoBase<AnuncioDto>)this).Dto.Variacoes)
			{
				Variacoes.Adicionar(new VariacoesViewModel
				{
					Id = variaco.Id,
					Sku = variaco.Sku,
					Nome = variaco.Nome,
					CodigoBarras = variaco.CodigoBarras,
					Linha = variaco.Linha,
					Coluna = variaco.Coluna,
					Variacao = variaco.Variacao,
					Preco = variaco.Preco,
					QuantidadeDisponivel = variaco.QuantidadeDisponivel,
					IdImagem = variaco.IdImagem,
					UrlImagem = variaco.UrlImagem,
					ModelIdShopee = variaco.ModelIdShopee
				});
			}
			ImagensVariacoes = (from x in HelperList.DistinctBy<VariacoesViewModel, string>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Func<VariacoesViewModel, string>)((VariacoesViewModel x) => x.Coluna))
				select new ImagensVariacoesViewModel
				{
					Coluna = x.Coluna,
					IdImagem = x.IdImagem,
					UrlImagem = x.UrlImagem
				}).ToList();
			ImagensVariacoes.ForEach(delegate(ImagensVariacoesViewModel x)
			{
				x.ImagemAlterada += ImagensOnPropertyChanged;
			});
			LinqExtensions.ForEach<VariacoesViewModel>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Action<VariacoesViewModel>)delegate(VariacoesViewModel x)
			{
				x.PrecoAlterado += VariacoesPrecoOnPropertyChanged;
			});
			await CarregarModelsShopee();
			if (!_descartado)
			{
				((BindableBase)this).RaisePropertiesChanged<bool, string[]>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeVariacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			List<ModelDto> list = new List<ModelDto>();
			foreach (VariacoesViewModel variacao in Variacoes.Itens)
			{
				ModelDto val2 = new ModelDto
				{
					Id = variacao.Id,
					Sku = variacao.Sku,
					Nome = variacao.Nome,
					CodigoBarras = variacao.CodigoBarras,
					Linha = variacao.Linha,
					Coluna = variacao.Coluna,
					Variacao = variacao.Variacao,
					Preco = variacao.Preco,
					QuantidadeDisponivel = variacao.QuantidadeDisponivel
				};
				ImagensVariacoesViewModel imagensVariacoesViewModel = ImagensVariacoes.FirstOrDefault((ImagensVariacoesViewModel x) => x.Coluna == variacao.Coluna);
				val2.IdImagem = ((imagensVariacoesViewModel != null) ? HelperString.ToStringEmptyWhenNull(imagensVariacoesViewModel.IdImagem) : null);
				ImagensVariacoesViewModel imagensVariacoesViewModel2 = ImagensVariacoes.FirstOrDefault((ImagensVariacoesViewModel x) => x.Coluna == variacao.Coluna);
				val2.UrlImagem = ((imagensVariacoesViewModel2 != null) ? HelperString.ToStringEmptyWhenNull(imagensVariacoesViewModel2.UrlImagem) : null);
				val2.ModelIdShopee = variacao.ModelIdShopee;
				list.Add(val2);
			}
			val.Variacoes = list;
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		public override void Dispose()
		{
			_descartado = true;
			_anuncioViewModel.AlterouProdutoEvent -= AlterouProdutoEvent;
			_anuncioViewModel.AlterouPrecoEvent -= AlterouPrecoEvent;
			_anuncioViewModel.AlterouUtilizaInformacaoCadatroEvent -= AlterouUtilizaInformacaoCadatroEvent;
			LinqExtensions.ForEach<VariacoesViewModel>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Action<VariacoesViewModel>)delegate(VariacoesViewModel x)
			{
				x.PrecoAlterado -= VariacoesPrecoOnPropertyChanged;
			});
			ImagensVariacoes?.ForEach(delegate(ImagensVariacoesViewModel x)
			{
				x.ImagemAlterada -= ImagensOnPropertyChanged;
			});
			((ViewModelValidationBase)Variacoes).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}

		private async Task CarregarModelsShopee()
		{
			if (((ViewModelDtoBase<AnuncioDto>)this).Dto.IdShopee == 0L || !((ViewModelDtoBase<AnuncioDto>)this).Dto.PossuiGrade)
			{
				return;
			}
			try
			{
				ListaVariacoes val = await DadosSistema.GetShopeeApiService().ObterVariacoesAsync(((ViewModelDtoBase<AnuncioDto>)this).Dto.IdShopee);
				if (_descartado)
				{
					return;
				}
				Response response = val.response;
				IEnumerable<Model> enumerable = ((response != null) ? response.model : null);
				if (enumerable == null)
				{
					return;
				}
				List<ModelShopeeItem> list = new List<ModelShopeeItem>
				{
					new ModelShopeeItem
					{
						ModelId = 0L,
						Descricao = "Nenhum"
					}
				};
				list.AddRange(enumerable.Select((Model m) => new ModelShopeeItem
				{
					ModelId = m.model_id,
					Descricao = (string.IsNullOrEmpty(m.model_name) ? m.model_id.ToString() : m.model_name)
				}));
				ModelsShopee = list;
				Dictionary<string, long> porSku = (from m in enumerable
					where !string.IsNullOrEmpty(m.model_sku)
					group m by m.model_sku).ToDictionary((IGrouping<string, Model> g) => g.Key, (IGrouping<string, Model> g) => g.First().model_id);
				LinqExtensions.ForEach<VariacoesViewModel>((IEnumerable<VariacoesViewModel>)Variacoes.Itens, (Action<VariacoesViewModel>)delegate(VariacoesViewModel v)
				{
					if (v.ModelIdShopee == 0L && porSku.ContainsKey(v.Sku))
					{
						v.ModelIdShopee = porSku[v.Sku];
					}
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}
	}
	public class AnuncioShopeeVariacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioShopeeVariacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Variações", "Grade", viewModel)
		{
		}
	}
	public class AnuncioShopeeEntregaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioShopeeEntregaAbaViewModel(IViewModelDtoBase viewModel)
			: base("Entrega", "Transportadora e cubagem", viewModel)
		{
		}
	}
	public class AnuncioShopeeCategoriaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioShopeeCategoriaAbaViewModel(IViewModelDtoBase viewModel)
			: base("Categoria", "Atributos e marca", viewModel)
		{
		}
	}
	public class AnuncioShopeeAtacadoViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly AnuncioShopeeViewModel _anuncioViewModel;

		private AnuncioShopeeVariacaoViewModel _anuncioVariacaoViewModel;

		public Lista<Wholesale> QuantidadePrecoAtacado { get; set; } = new Lista<Wholesale>();


		public bool Habilitado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Habilitado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Habilitado), value);
			}
		}

		public int QuantideMinima
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => QuantideMinima));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => QuantideMinima), value);
			}
		}

		public int QuantidadeMaxima
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => QuantidadeMaxima));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => QuantidadeMaxima), value);
			}
		}

		public bool PodeEditarQuantidadeMinima
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeEditarQuantidadeMinima));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeEditarQuantidadeMinima), value);
			}
		}

		public decimal PrecoUnitario
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario), value);
			}
		}

		public string ToolTipError
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ToolTipError));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ToolTipError), value);
			}
		}

		public override string Error => string.Empty;

		public override bool Valid
		{
			get
			{
				IReadOnlyCollection<Wholesale> itens = QuantidadePrecoAtacado.Itens;
				decimal? obj;
				if (itens == null)
				{
					obj = null;
				}
				else
				{
					Wholesale obj2 = itens.FirstOrDefault();
					obj = ((obj2 != null) ? new decimal?(obj2.unit_price) : null);
				}
				decimal? num = obj;
				decimal preco = base.Dto.Preco;
				if (!(num.GetValueOrDefault() < preco) || !num.HasValue)
				{
					return !QuantidadePrecoAtacado.Itens.Any();
				}
				return true;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				if (!((Validation)this).Valid)
				{
					return new string[1] { "O preço do produto foi alterado para um preço menor que o de atacado, ajuste os valores antes de salvar." };
				}
				return new string[0];
			}
		}

		public ICommand AdicionarQuantidadePrecoAtacadoCommand { get; set; }

		public ICommand RemoverCommand { get; set; }

		public AnuncioShopeeAtacadoViewModel(AnuncioDto dto, AnuncioShopeeViewModel AnuncioViewModel)
			: base(dto)
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			_anuncioViewModel = AnuncioViewModel;
			_anuncioViewModel.AlterouPrecoEvent += AlterouPrecoEvent;
			AdicionarQuantidadePrecoAtacadoCommand = (ICommand)new DelegateCommand((Action)AdicionarQuantidadePrecoAtacado, (Func<bool>)PodeAdicionar, (bool?)null);
			RemoverCommand = (ICommand)new DelegateCommand<object>((Action<object>)Remover, (Func<object, bool>)PodeRemover, (bool?)null);
			Habilitado = true;
		}

		protected virtual void Remover(object variacao)
		{
			QuantidadePrecoAtacado.Remover((Wholesale)((variacao is Wholesale) ? variacao : null));
			if (QuantidadePrecoAtacado.Itens.Any())
			{
				QuantideMinima = QuantidadePrecoAtacado.Itens.LastOrDefault().max_count + 1;
				return;
			}
			QuantideMinima = 2;
			PodeEditarQuantidadeMinima = true;
		}

		protected bool PodeRemover(object variacao)
		{
			if (QuantidadePrecoAtacado.Itens.Any())
			{
				return ((object)QuantidadePrecoAtacado.Itens.Last()).Equals((variacao is Wholesale) ? variacao : null);
			}
			return false;
		}

		public void VincularVariacao(AnuncioShopeeVariacaoViewModel anuncioShopeeVariacaoViewModel)
		{
			DesvincularVariacao();
			_anuncioVariacaoViewModel = anuncioShopeeVariacaoViewModel;
			_anuncioVariacaoViewModel.AlterouPrecoVariacoesEvent += AlterouPrecoEvent;
		}

		public void DesvincularVariacao()
		{
			if (HelperObject.IsNotNull((object)_anuncioVariacaoViewModel))
			{
				_anuncioVariacaoViewModel.AlterouPrecoVariacoesEvent -= AlterouPrecoEvent;
			}
			_anuncioVariacaoViewModel = null;
		}

		private void AlterouPrecoEvent(object sender, decimal novoPreco)
		{
			Habilitado = novoPreco > 0m;
			if (Habilitado)
			{
				base.Dto.Preco = novoPreco;
			}
			((BindableBase)this).RaisePropertiesChanged<bool, string[]>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string[]>>)(() => ((Validation)this).Pendencies));
		}

		public void AdicionarQuantidadePrecoAtacado()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			QuantidadePrecoAtacado.Adicionar(new Wholesale
			{
				min_count = QuantideMinima,
				max_count = QuantidadeMaxima,
				unit_price = PrecoUnitario
			});
			QuantideMinima = QuantidadeMaxima + 1;
			PodeEditarQuantidadeMinima = false;
			QuantidadeMaxima = 0;
			PrecoUnitario = 0m;
		}

		public override void ProjectedAsViewModel()
		{
			QuantidadePrecoAtacado.Adicionar(base.Dto.Wholesale.Select((Func<WholesaleDto, Wholesale>)((WholesaleDto x) => new Wholesale
			{
				min_count = x.min_count,
				max_count = x.max_count,
				unit_price = x.unit_price
			})));
			if (QuantidadePrecoAtacado.Itens.Any())
			{
				QuantideMinima = QuantidadePrecoAtacado.Itens.LastOrDefault().max_count + 1;
				PodeEditarQuantidadeMinima = false;
			}
			else
			{
				PodeEditarQuantidadeMinima = true;
			}
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string>>)(() => ((Validation)this).Error));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.Wholesale = ((IEnumerable<Wholesale>)QuantidadePrecoAtacado.Itens).Select((Func<Wholesale, WholesaleDto>)((Wholesale x) => new WholesaleDto
			{
				min_count = x.min_count,
				max_count = x.max_count,
				unit_price = x.unit_price
			}));
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		protected bool PodeAdicionar()
		{
			List<string> list = new List<string>();
			if (QuantidadePrecoAtacado.Itens.Count == 0)
			{
				if (base.Dto.Preco == 0m && Habilitado)
				{
					list.Add("Informe o produto do anúncio antes de prosseguir.");
				}
				if (!Habilitado)
				{
					list.Add("Variações devem possuir todas o mesmo preço para utilizar o preço de atacado.");
				}
				if (QuantideMinima < 2)
				{
					list.Add("Quantidade mínima deve ser maior que 2.");
				}
				if (QuantidadeMaxima < QuantideMinima)
				{
					list.Add("Quantidade máxima deve ser maior que a quantidade mínima.");
				}
				if (PrecoUnitario > base.Dto.Preco)
				{
					list.Add("Preço unitário deve ser menor que o preço do produto.");
				}
				if (PrecoUnitario == 0m)
				{
					list.Add("Preço deve ser maior que zero.");
				}
			}
			else
			{
				if (QuantideMinima <= QuantidadePrecoAtacado.Itens.Last().max_count)
				{
					list.Add("Quantidade mínima deve ser maior que a quantidade máxima já informada.");
				}
				if (QuantidadeMaxima <= QuantideMinima)
				{
					list.Add("Quantidade máxima deve ser maior que a quantidade mínima.");
				}
				decimal precoUnitario = PrecoUnitario;
				Wholesale obj = QuantidadePrecoAtacado.Itens.Last();
				decimal? num = ((obj != null) ? new decimal?(obj.unit_price) : null);
				if (precoUnitario >= num.GetValueOrDefault() && num.HasValue)
				{
					list.Add("Preço unitário deve ser menor que o preço unitário já informado.");
				}
				if (PrecoUnitario == 0m)
				{
					list.Add("Preço deve ser maior que zero.");
				}
				if (QuantidadePrecoAtacado.Itens.Count == 5)
				{
					list.Add("Limite máximo de preços diferenciados é 5.");
				}
			}
			ToolTipError = string.Join("\n", list);
			if (QuantidadePrecoAtacado.Itens.Count == 0)
			{
				if (QuantideMinima >= 2 && QuantidadeMaxima >= QuantideMinima && PrecoUnitario < base.Dto.Preco && PrecoUnitario > 0m && QuantidadePrecoAtacado.Itens.Count < 6)
				{
					return Habilitado;
				}
				return false;
			}
			if (QuantideMinima > QuantidadePrecoAtacado.Itens.Last().max_count && QuantidadeMaxima > QuantideMinima && PrecoUnitario < QuantidadePrecoAtacado.Itens.Last().unit_price && PrecoUnitario > 0m && QuantidadePrecoAtacado.Itens.Count < 6)
			{
				return Habilitado;
			}
			return false;
		}

		public override void Dispose()
		{
			_anuncioViewModel.AlterouPrecoEvent -= AlterouPrecoEvent;
			DesvincularVariacao();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnuncioShopeeEntregaViewModel : ViewModelDtoBase<AnuncioDto>
	{
		public class Transportadora : BindableBase
		{
			public string Nome
			{
				get
				{
					return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Nome));
				}
				set
				{
					((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Nome), value);
				}
			}

			public int IdTamanho
			{
				get
				{
					return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => IdTamanho));
				}
				set
				{
					((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => IdTamanho), value);
				}
			}

			public decimal TaxaEnvio
			{
				get
				{
					return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TaxaEnvio));
				}
				set
				{
					((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TaxaEnvio), value);
				}
			}

			public int IdTransportadora
			{
				get
				{
					return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => IdTransportadora));
				}
				set
				{
					((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => IdTransportadora), value);
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
					((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Ativo), value, (Action)delegate
					{
						((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => FreteGratisHabilitado));
					});
				}
			}

			public bool PermiteFreteGratis
			{
				get
				{
					return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PermiteFreteGratis));
				}
				set
				{
					((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PermiteFreteGratis), value, (Action)delegate
					{
						((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => FreteGratisHabilitado));
					});
				}
			}

			public bool FreteGratisHabilitado
			{
				get
				{
					if (Ativo)
					{
						return PermiteFreteGratis;
					}
					return false;
				}
			}

			public bool FreteGratis
			{
				get
				{
					return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FreteGratis));
				}
				set
				{
					((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FreteGratis), value);
				}
			}

			public bool Selecionada
			{
				get
				{
					return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionada));
				}
				set
				{
					((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionada), value);
				}
			}
		}

		public Lista<Transportadora> Transportadoras { get; set; } = new Lista<Transportadora>();


		private bool PossuiEntregaAtiva => Transportadoras.Itens.Any((Transportadora x) => x.Selecionada && x.Ativo);

		public override bool Valid
		{
			get
			{
				if (((Validation)this).Valid)
				{
					return PossuiEntregaAtiva;
				}
				return false;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				List<string> list = ((Validation)this).Pendencies.ToList();
				if (!PossuiEntregaAtiva)
				{
					list.Add(Transportadoras.Itens.Any((Transportadora x) => x.Selecionada) ? "As opções de entrega marcadas estão desativadas na loja Shopee. Ative-as no Seller Center ou marque outra opção." : "É obrigatório ativar ao menos uma opção de entrega.");
				}
				return list.ToArray();
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "É obrigatório cadastrar Peso.")]
		public double Peso
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => Peso));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => Peso), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "É obrigatório cadastrar Altura.")]
		public int Altura
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Altura));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Altura), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "É obrigatório cadastrar Largura.")]
		public int Largura
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Largura));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Largura), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "É obrigatório cadastrar Comprimento.")]
		public int Comprimento
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Comprimento));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Comprimento), value);
			}
		}

		public bool SobEncomenda
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SobEncomenda));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SobEncomenda), value);
			}
		}

		public int DiasParaEnvio
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => DiasParaEnvio));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => DiasParaEnvio), value);
			}
		}

		public AnuncioShopeeEntregaViewModel(AnuncioDto dto)
			: base(dto)
		{
		}

		public override async void ProjectedAsViewModel()
		{
			LoaderProcessoService.MostrarLoader("Obtendo transportadoras...");
			try
			{
				Logistics val = await DadosSistema.GetShopeeApiService().ObterTransportadorasAsync();
				Transportadoras.Adicionar((IEnumerable<Transportadora>)val.response.logistics_channel_list.Select((LogisticsChannel transpotadora) => new Transportadora
				{
					Nome = transpotadora.logistics_channel_name,
					IdTamanho = HelperString.ToInt(transpotadora.size_list.FirstOrDefault()),
					IdTransportadora = transpotadora.logistics_channel_id,
					Ativo = transpotadora.enabled,
					PermiteFreteGratis = transpotadora.PermiteFreteGratis,
					FreteGratis = transpotadora.freteGratis,
					Selecionada = transpotadora.selecionada
				}).ToList());
				Peso = ((ViewModelDtoBase<AnuncioDto>)this).Dto.Dimencoes.Peso;
				Altura = ((ViewModelDtoBase<AnuncioDto>)this).Dto.Dimencoes.Altura;
				Largura = ((ViewModelDtoBase<AnuncioDto>)this).Dto.Dimencoes.Largura;
				Comprimento = ((ViewModelDtoBase<AnuncioDto>)this).Dto.Dimencoes.Comprimento;
				LinqExtensions.ForEach<Transportadora>((IEnumerable<Transportadora>)Transportadoras.Itens, (Action<Transportadora>)delegate(Transportadora x)
				{
					LogisticaDto val2 = base.Dto.Logistica.FirstOrDefault((LogisticaDto y) => y.IdTransportadora == x.IdTransportadora);
					if (val2 != null)
					{
						x.Nome = val2.Nome;
						x.IdTamanho = val2.IdTamanho;
						x.IdTransportadora = val2.IdTransportadora;
						x.FreteGratis = val2.FreteGratis && x.PermiteFreteGratis;
						x.Selecionada = val2.Selecionada;
					}
					((BindableBase)x).PropertyChanged += TransportadoraOnPropertyChanged;
				});
				((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string>>)(() => ((Validation)this).Error));
				SobEncomenda = ((ViewModelDtoBase<AnuncioDto>)this).Dto.SobEncomenda;
				DiasParaEnvio = ((ViewModelDtoBase<AnuncioDto>)this).Dto.DiasParaEnvio;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			LoaderProcessoService.EsconderLoader();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.SobEncomenda = SobEncomenda;
			val.DiasParaEnvio = (SobEncomenda ? DiasParaEnvio : 0);
			val.Dimencoes = new DimencoesDto
			{
				Peso = Peso,
				Altura = Altura,
				Largura = Largura,
				Comprimento = Comprimento
			};
			val.Logistica = ((IEnumerable<Transportadora>)Transportadoras.Itens).Select((Func<Transportadora, LogisticaDto>)((Transportadora transpotadora) => new LogisticaDto
			{
				Nome = transpotadora.Nome,
				IdTamanho = transpotadora.IdTamanho,
				IdTransportadora = transpotadora.IdTransportadora,
				Ativo = transpotadora.Ativo,
				FreteGratis = transpotadora.FreteGratis,
				Selecionada = transpotadora.Selecionada
			})).ToList();
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		private void TransportadoraOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeEntregaViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void Dispose()
		{
			LinqExtensions.ForEach<Transportadora>((IEnumerable<Transportadora>)Transportadoras.Itens, (Action<Transportadora>)delegate(Transportadora x)
			{
				((BindableBase)x).PropertyChanged -= TransportadoraOnPropertyChanged;
			});
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AnuncioShopeeCategoriaViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly IList<IDisposable> _disposables = new List<IDisposable>();

		private bool _tabelaMedidasCarregada;

		private bool _descartado;

		public List<Category> Categorias { get; set; } = new List<Category>();


		public Lista<Category> CategoriasNivel1 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel2 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel3 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel4 { get; set; } = new Lista<Category>();


		public Lista<Category> CategoriasNivel5 { get; set; } = new Lista<Category>();


		public Lista<Brand> Marcas { get; set; } = new Lista<Brand>();


		public Lista<AtributoViewModel> Atributos { get; set; } = new Lista<AtributoViewModel>();


		public Lista<TabelaMedidasModelo> ModelosTabelaMedidas { get; set; } = new Lista<TabelaMedidasModelo>();


		public ICommand BuscarImagemTabelaMedidasCommand { get; set; }

		public ICommand RemoverImagemTabelaMedidasCommand { get; set; }

		public bool PossuiTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PossuiTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PossuiTabelaMedidas), value);
			}
		}

		public bool TabelaMedidasObrigatoria
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasObrigatoria));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasObrigatoria), value);
			}
		}

		public bool SuportaModeloTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SuportaModeloTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SuportaModeloTabelaMedidas), value);
			}
		}

		public bool SuportaImagemTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SuportaImagemTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SuportaImagemTabelaMedidas), value);
			}
		}

		public bool PossuiModelosTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PossuiModelosTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PossuiModelosTabelaMedidas), value);
			}
		}

		public bool TabelaMedidasPorModelo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasPorModelo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasPorModelo), value, (Action)delegate
				{
					if (TabelaMedidasPorModelo)
					{
						TabelaMedidasPorImagem = false;
					}
					((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string>>)(() => ((Validation)this).Error));
				});
			}
		}

		public bool TabelaMedidasPorImagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasPorImagem));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TabelaMedidasPorImagem), value, (Action)delegate
				{
					if (TabelaMedidasPorImagem)
					{
						TabelaMedidasPorModelo = false;
					}
					((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string>>)(() => ((Validation)this).Error));
				});
			}
		}

		public string IdImagemTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => IdImagemTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => IdImagemTabelaMedidas), value);
			}
		}

		public string UrlImagemTabelaMedidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => UrlImagemTabelaMedidas));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => UrlImagemTabelaMedidas), value);
			}
		}

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

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Marca' é obrigatório.")]
		public string MarcaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MarcaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MarcaDescricao), value);
			}
		}

		public string MarcaId
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MarcaId));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MarcaId), value);
			}
		}

		private bool TabelaMedidasPreenchida
		{
			get
			{
				if (!TabelaMedidasPorModelo)
				{
					if (TabelaMedidasPorImagem)
					{
						return !HelperString.IsNullOrEmpty(IdImagemTabelaMedidas);
					}
					return false;
				}
				return ModelosTabelaMedidas.PossuiItemSelecionado;
			}
		}

		private bool TabelaMedidasValida
		{
			get
			{
				if (PossuiTabelaMedidas && TabelaMedidasObrigatoria)
				{
					return TabelaMedidasPreenchida;
				}
				return true;
			}
		}

		private IEnumerable<AtributoListaSelecionavelViewModel> AtributosAcimaDoLimite
		{
			get
			{
				if (!_descartado)
				{
					return from x in Atributos.Itens.OfType<AtributoListaSelecionavelViewModel>()
						where x.ExcedeLimite
						select x;
				}
				return Enumerable.Empty<AtributoListaSelecionavelViewModel>();
			}
		}

		public override bool Valid
		{
			get
			{
				if (((Validation)this).Valid && TabelaMedidasValida)
				{
					return !AtributosAcimaDoLimite.Any();
				}
				return false;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				List<string> list = ((Validation)this).Pendencies.ToList();
				if (!TabelaMedidasValida)
				{
					list.Add("A categoria exige tabela de medidas. Selecione um modelo ou envie a imagem da tabela.");
				}
				list.AddRange(AtributosAcimaDoLimite.Select((AtributoListaSelecionavelViewModel x) => $"O atributo {x.Titulo} permite no máximo {x.MaximoSelecionados} opções."));
				return list.ToArray();
			}
		}

		public event EventHandler<string> AlterouCategoriaEvent;

		public AnuncioShopeeCategoriaViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Expected O, but got Unknown
			BuscarImagemTabelaMedidasCommand = (ICommand)new AsyncCommand((Func<Task>)BuscarImagemTabelaMedidas);
			RemoverImagemTabelaMedidasCommand = (ICommand)new DelegateCommand((Action)RemoverImagemTabelaMedidas);
			_disposables.Add(ObservableExtensions.Subscribe<TabelaMedidasModelo>(ModelosTabelaMedidas.WhenAlterouItemSelecionado, (Action<TabelaMedidasModelo>)delegate
			{
				((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeCategoriaViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel1.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				try
				{
					await CategoriasNivel1AlterouItemSelecionado(itemSelecionado);
				}
				catch (Exception)
				{
				}
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel2.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				try
				{
					await CategoriasNivel2AlterouItemSelecionado(itemSelecionado);
				}
				catch (Exception)
				{
				}
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel3.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				try
				{
					await CategoriasNivel3AlterouItemSelecionado(itemSelecionado);
				}
				catch (Exception)
				{
				}
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel4.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				try
				{
					await CategoriasNivel4AlterouItemSelecionado(itemSelecionado);
				}
				catch (Exception)
				{
				}
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Category>(CategoriasNivel5.WhenAlterouItemSelecionado, (Action<Category>)async delegate(Category itemSelecionado)
			{
				try
				{
					await CategoriasNivel5AlterouItemSelecionado(itemSelecionado);
				}
				catch (Exception)
				{
				}
			}));
			_disposables.Add(ObservableExtensions.Subscribe<Brand>(Marcas.WhenAlterouItemSelecionado, (Action<Brand>)async delegate(Brand itemSelecionado)
			{
				await MarcasAlterouItemSelecionado(itemSelecionado);
			}));
		}

		public override async void ProjectedAsViewModel()
		{
			await ListarCategoriasNivel1();
			if (_descartado || HelperString.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)this).Dto.CategoriaId))
			{
				return;
			}
			List<int> ArvoreCategoria = new List<int>();
			int categoriaAtual = HelperString.ToInt(((ViewModelDtoBase<AnuncioDto>)this).Dto.CategoriaId);
			while (categoriaAtual > 0)
			{
				ArvoreCategoria.Insert(0, categoriaAtual);
				Category obj = Categorias.FirstOrDefault((Category x) => x.category_id == categoriaAtual);
				categoriaAtual = ((obj != null) ? obj.parent_category_id : 0);
			}
			if (CategoriasNivel1.Itens.Select((Category x) => x.category_id).Contains(ArvoreCategoria[0]))
			{
				CategoriasNivel1.Posicionar((Expression<Func<Category, bool>>)((Category x) => x.category_id == ArvoreCategoria[0]));
				CategoriasNivel2.Posicionar((Expression<Func<Category, bool>>)((Category x) => x.category_id == ArvoreCategoria[1]));
				if (ArvoreCategoria.Count > 2)
				{
					CategoriasNivel3.Posicionar((Expression<Func<Category, bool>>)((Category x) => x.category_id == ArvoreCategoria[2]));
				}
				if (ArvoreCategoria.Count > 3)
				{
					CategoriasNivel4.Posicionar((Expression<Func<Category, bool>>)((Category x) => x.category_id == ArvoreCategoria[3]));
				}
				if (ArvoreCategoria.Count > 5)
				{
					CategoriasNivel5.Posicionar((Expression<Func<Category, bool>>)((Category x) => x.category_id == ArvoreCategoria[4]));
				}
			}
			else
			{
				DialogoFactory.MostrarDialogoConfirma("A árvore de categorias sofreu alteração, será necessário selecionar uma nova!", false, HorizontalAlignment.Center);
			}
		}

		private void ProjectAtributos()
		{
			try
			{
				foreach (AtributoViewModel atributo in Atributos.Itens)
				{
					Atributos.ItemSelecionado = atributo;
					AtributoDto dtoAttr = base.Dto.Atributos.FirstOrDefault((AtributoDto a) => a.Id == atributo.ValueId);
					AtributoListaViewModel atributoListaViewModel = atributo as AtributoListaViewModel;
					AtributoTextoViewModel atributoTextoViewModel = atributo as AtributoTextoViewModel;
					AtributoListaSelecionavelViewModel atributoListaSelecionavelViewModel = atributo as AtributoListaSelecionavelViewModel;
					if (atributoListaViewModel != null)
					{
						if (dtoAttr != null)
						{
							atributoListaViewModel.Opcoes.ItemSelecionado = atributoListaViewModel.Opcoes.Itens.FirstOrDefault((AtributoOpcaoDto x) => x.Id == dtoAttr.ValueId);
						}
					}
					else if (atributoTextoViewModel != null)
					{
						if (dtoAttr != null)
						{
							atributoTextoViewModel.Valor = dtoAttr.ValueName;
						}
					}
					else
					{
						if (atributoListaSelecionavelViewModel == null)
						{
							continue;
						}
						List<AtributoListaSelecionavelDto> selecionaveis = HelperIEnumerable.ToListEmptyWhenNull<AtributoListaSelecionavelDto>(base.Dto.Atributos.OfType<AtributoListaSelecionavelDto>());
						atributoListaSelecionavelViewModel.Opcoes.ToList().ForEach(delegate(AtributoOpcaoSelecionavelViewModel x)
						{
							x.Selecionado = base.Dto.Atributos.Any((AtributoDto a) => a.ValueId == x.Id) || selecionaveis.Any((AtributoListaSelecionavelDto a) => a.OpcoesSelecionadas.Any((AtributoOpcaoDto o) => o.Id == x.Id));
						});
					}
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.CategoriaDescricao = CategoriaDescricao;
			val.CategoriaId = CategoriaId;
			val.IdMarca = HelperString.ToInt(MarcaId);
			val.DescricaoMarca = MarcaDescricao;
			val.Atributos = HelperIEnumerable.ToListEmptyWhenNull<AtributoDto>(Atributos.Itens.Select((AtributoViewModel atributo) => atributo.ObterDto()));
			if (_tabelaMedidasCarregada)
			{
				bool flag = PossuiTabelaMedidas && TabelaMedidasPorModelo && ModelosTabelaMedidas.PossuiItemSelecionado;
				bool flag2 = PossuiTabelaMedidas && TabelaMedidasPorImagem && !HelperString.IsNullOrEmpty(IdImagemTabelaMedidas);
				val.IdModeloTabelaMedidas = (flag ? ModelosTabelaMedidas.ItemSelecionado.Id : 0);
				val.IdImagemTabelaMedidas = (flag2 ? IdImagemTabelaMedidas : string.Empty);
				val.UrlImagemTabelaMedidas = (flag2 ? UrlImagemTabelaMedidas : string.Empty);
			}
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		private async Task ListarCategoriasNivel1()
		{
			try
			{
				IEnumerable<CategoryDto> enumerable = await DadosSistema.GetService().Canal<IShopeeModuleService, Task<IEnumerable<CategoryDto>>>((Func<IShopeeModuleService, Task<IEnumerable<CategoryDto>>>)((IShopeeModuleService service) => service.ObterCategorias()));
				if (!enumerable.Any())
				{
					await DadosSistema.GetShopeeApiService().AtualizarCategoriasAsync();
					enumerable = await DadosSistema.GetService().Canal<IShopeeModuleService, Task<IEnumerable<CategoryDto>>>((Func<IShopeeModuleService, Task<IEnumerable<CategoryDto>>>)((IShopeeModuleService service) => service.ObterCategorias()));
				}
				Categorias = CategoryShopeeService.MorfarCollection(enumerable).ToList();
				CategoriasNivel1.AdicionarSemPosicionar(Categorias.Where((Category x) => x.parent_category_id == 0));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async Task CategoriasNivel1AlterouItemSelecionado(Category category)
		{
			CategoriasNivel2.Limpar();
			CategoriasNivel3.Limpar();
			CategoriasNivel4.Limpar();
			CategoriasNivel5.Limpar();
			LimparAtributos();
			if (category != null)
			{
				CategoriasNivel2.AdicionarSemPosicionar(Categorias.Where((Category x) => x.parent_category_id == category.category_id));
			}
		}

		private async Task CategoriasNivel2AlterouItemSelecionado(Category category)
		{
			CategoriasNivel3.Limpar();
			CategoriasNivel4.Limpar();
			CategoriasNivel5.Limpar();
			LimparAtributos();
			if (category == null)
			{
				return;
			}
			if (!category.has_children)
			{
				CategoriaDescricao = category.display_category_name;
				CategoriaId = category.category_id.ToString();
				await ObterMarcaAtributos(category);
			}
			else
			{
				CategoriasNivel3.AdicionarSemPosicionar(Categorias.Where((Category x) => x.parent_category_id == category.category_id));
			}
		}

		private async Task CategoriasNivel3AlterouItemSelecionado(Category category)
		{
			CategoriasNivel4.Limpar();
			CategoriasNivel5.Limpar();
			Atributos.Limpar();
			Marcas.Limpar();
			LimparTabelaMedidas();
			if (category == null)
			{
				return;
			}
			if (!category.has_children)
			{
				CategoriaDescricao = category.display_category_name;
				CategoriaId = category.category_id.ToString();
				await ObterMarcaAtributos(category);
			}
			else
			{
				CategoriasNivel4.AdicionarSemPosicionar(Categorias.Where((Category x) => x.parent_category_id == category.category_id));
			}
		}

		private async Task CategoriasNivel4AlterouItemSelecionado(Category category)
		{
			CategoriasNivel5.Limpar();
			Atributos.Limpar();
			Marcas.Limpar();
			LimparTabelaMedidas();
			if (category == null)
			{
				return;
			}
			if (!category.has_children)
			{
				CategoriaDescricao = category.display_category_name;
				CategoriaId = category.category_id.ToString();
				await ObterMarcaAtributos(category);
			}
			else
			{
				CategoriasNivel5.AdicionarSemPosicionar(Categorias.Where((Category x) => x.parent_category_id == category.category_id));
			}
		}

		private async Task CategoriasNivel5AlterouItemSelecionado(Category category)
		{
			Atributos.Limpar();
			Marcas.Limpar();
			LimparTabelaMedidas();
			if (category != null)
			{
				CategoriaDescricao = category.display_category_name;
				CategoriaId = category.category_id.ToString();
				await ObterMarcaAtributos(category);
			}
		}

		private async Task ObterMarcaAtributos(Category category)
		{
			await Task.Delay(1000);
			if (_descartado)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Carregando marcas...");
			try
			{
				await ObterMarcaInicial(category, ((ViewModelDtoBase<AnuncioDto>)this).Dto.IdMarca);
				if (_descartado)
				{
					return;
				}
				await ObterAtributosCategoria(category);
				if (!_descartado)
				{
					if (((ViewModelDtoBase<AnuncioDto>)this).Dto.Atributos.Any())
					{
						ProjectAtributos();
					}
					await ObterTabelaMedidas(category);
					if (!_descartado)
					{
						await ObterMarcas(category);
					}
				}
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private async Task MarcasAlterouItemSelecionado(Brand brand)
		{
			if (brand != null)
			{
				MarcaDescricao = brand.display_brand_name;
				MarcaId = brand.brand_id.ToString();
			}
		}

		private async Task ObterMarcaInicial(Category itemSeleciondo, int marcaSelecionada)
		{
			try
			{
				if (HelperObject.IsNull((object)marcaSelecionada))
				{
					marcaSelecionada = 0;
				}
				ListBrands val = await DadosSistema.GetShopeeApiService().ObterMarcaAsync(itemSeleciondo.category_id.ToString(), marcaSelecionada);
				if (!_descartado)
				{
					Marcas.Adicionar((IEnumerable<Brand>)val.response.brand_list.OrderBy((Brand x) => x.display_brand_name));
					Marcas.PosicionarNoPrimeiro();
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		private async Task ObterMarcas(Category itemSeleciondo)
		{
			try
			{
				ListBrands val = await DadosSistema.GetShopeeApiService().ObterMarcasAsync(itemSeleciondo.category_id.ToString());
				if (!_descartado)
				{
					Marcas.Adicionar((IEnumerable<Brand>)val.response.brand_list.OrderBy((Brand x) => x.display_brand_name));
					Marcas.PosicionarNoPrimeiro();
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		private async Task ObterAtributosCategoria(Category itemSeleciondo)
		{
			try
			{
				Attributes val = await DadosSistema.GetShopeeApiService().ObterAtributosCategoriaAsync(itemSeleciondo.category_id.ToString());
				if (_descartado || !HelperObject.IsNotNull((object)val.response.list.FirstOrDefault().attribute_tree))
				{
					return;
				}
				Atributos.Limpar();
				List<AtributoViewModel> list = new List<AtributoViewModel>();
				foreach (AtributesTree item in val.response.list.FirstOrDefault().attribute_tree)
				{
					try
					{
						list.Add(AtributoFactory.Init().SetarValueId(item.attribute_id.ToString()).SetarUnidade(HelperString.ToStringEmptyWhenNull(item.attribute_value_list.FirstOrDefault().value_unit))
							.SetarTipo((TiposAtributoShopee)item.attribute_info.input_type)
							.SetarMaximoSelecionados(item.attribute_info.max_value_count)
							.SetarTitulo(item.multi_lang.FirstOrDefault().value)
							.SetarOpcoes(((IEnumerable<AtributesValues>)HelperIEnumerable.ToListEmptyWhenNull<AtributesValues>((IEnumerable<AtributesValues>)item.attribute_value_list)).Select((Func<AtributesValues, AtributoOpcaoDto>)((AtributesValues value) => new AtributoOpcaoDto
							{
								Id = value.value_id.ToString(),
								Valor = value.multi_lang.FirstOrDefault().value,
								Unidade = HelperString.ToStringEmptyWhenNull(value.value_unit)
							})))
							.Criar());
					}
					catch (Exception)
					{
					}
				}
				foreach (AtributoListaSelecionavelViewModel item2 in list.OfType<AtributoListaSelecionavelViewModel>())
				{
					item2.SelecaoAlterada += AtributoSelecionavelOnSelecaoAlterada;
				}
				Atributos.Adicionar((IEnumerable<AtributoViewModel>)list.OrderBy((AtributoViewModel x) => x.Titulo.Split(' ')[0]));
			}
			catch (Exception value2)
			{
				Console.WriteLine(value2);
				throw;
			}
		}

		private async Task ObterTabelaMedidas(Category category)
		{
			LimparTabelaMedidas();
			try
			{
				TabelaMedidasCategoria val = await DadosSistema.GetShopeeApiService().ObterTabelaMedidasCategoriaAsync(category.category_id.ToString());
				if (!_descartado)
				{
					TabelaMedidasObrigatoria = val.Obrigatoria;
					SuportaModeloTabelaMedidas = val.SuportaModelo;
					SuportaImagemTabelaMedidas = val.SuportaImagem;
					ModelosTabelaMedidas.AdicionarSemPosicionar((IEnumerable<TabelaMedidasModelo>)val.Modelos.OrderBy((TabelaMedidasModelo x) => x.Nome));
					PossuiModelosTabelaMedidas = ModelosTabelaMedidas.Numero > 0;
					PossuiTabelaMedidas = val.Suportada;
					_tabelaMedidasCarregada = true;
					ProjectTabelaMedidas();
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		private void ProjectTabelaMedidas()
		{
			if (!PossuiTabelaMedidas)
			{
				return;
			}
			if (SuportaModeloTabelaMedidas && ModelosTabelaMedidas.Existe((Expression<Func<TabelaMedidasModelo, bool>>)((TabelaMedidasModelo x) => x.Id == ((ViewModelDtoBase<AnuncioDto>)this).Dto.IdModeloTabelaMedidas)))
			{
				ModelosTabelaMedidas.Posicionar((Expression<Func<TabelaMedidasModelo, bool>>)((TabelaMedidasModelo x) => x.Id == ((ViewModelDtoBase<AnuncioDto>)this).Dto.IdModeloTabelaMedidas));
				TabelaMedidasPorModelo = true;
			}
			else if (SuportaImagemTabelaMedidas && !HelperString.IsNullOrEmpty(base.Dto.IdImagemTabelaMedidas))
			{
				IdImagemTabelaMedidas = base.Dto.IdImagemTabelaMedidas;
				UrlImagemTabelaMedidas = base.Dto.UrlImagemTabelaMedidas;
				TabelaMedidasPorImagem = true;
			}
			else if (SuportaModeloTabelaMedidas && (PossuiModelosTabelaMedidas || !SuportaImagemTabelaMedidas))
			{
				TabelaMedidasPorModelo = true;
			}
			else
			{
				TabelaMedidasPorImagem = SuportaImagemTabelaMedidas;
			}
		}

		private void LimparTabelaMedidas()
		{
			_tabelaMedidasCarregada = false;
			PossuiTabelaMedidas = false;
			TabelaMedidasObrigatoria = false;
			SuportaModeloTabelaMedidas = false;
			SuportaImagemTabelaMedidas = false;
			PossuiModelosTabelaMedidas = false;
			TabelaMedidasPorModelo = false;
			TabelaMedidasPorImagem = false;
			ModelosTabelaMedidas.Limpar();
			ModelosTabelaMedidas.ItemSelecionado = null;
			IdImagemTabelaMedidas = string.Empty;
			UrlImagemTabelaMedidas = string.Empty;
		}

		private async Task BuscarImagemTabelaMedidas()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;",
				Title = "Selecione a imagem da tabela de medidas"
			};
			if (openFileDialog.ShowDialog() == true)
			{
				try
				{
					LoaderProcessoService.MostrarLoader("Enviando imagem da tabela de medidas...");
					string text = await DadosSistema.GetShopeeApiService().CarregarImagemAsync(openFileDialog.FileName);
					IdImagemTabelaMedidas = text.Replace("https://cf.shopee.com.br/file/", "");
					UrlImagemTabelaMedidas = text;
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
				}
				((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeCategoriaViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
			}
		}

		private void RemoverImagemTabelaMedidas()
		{
			IdImagemTabelaMedidas = string.Empty;
			UrlImagemTabelaMedidas = string.Empty;
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeCategoriaViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private void AtributoSelecionavelOnSelecaoAlterada(object sender, EventArgs e)
		{
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeCategoriaViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public void LimparAtributos()
		{
			CategoriaDescricao = "";
			CategoriaDescricao = "";
			MarcaDescricao = "";
			MarcaId = "";
			Atributos.Limpar();
			Marcas.Limpar();
			LimparTabelaMedidas();
		}

		public override void Dispose()
		{
			_descartado = true;
			foreach (IDisposable disposable in _disposables)
			{
				disposable.Dispose();
			}
			_disposables.Clear();
			foreach (AtributoListaSelecionavelViewModel item in Atributos.Itens.OfType<AtributoListaSelecionavelViewModel>())
			{
				item.SelecaoAlterada -= AtributoSelecionavelOnSelecaoAlterada;
			}
			((ViewModelValidationBase)CategoriasNivel1).Dispose();
			((ViewModelValidationBase)CategoriasNivel2).Dispose();
			((ViewModelValidationBase)CategoriasNivel3).Dispose();
			((ViewModelValidationBase)CategoriasNivel4).Dispose();
			((ViewModelValidationBase)CategoriasNivel5).Dispose();
			((ViewModelValidationBase)Atributos).Dispose();
			((ViewModelValidationBase)Marcas).Dispose();
			((ViewModelValidationBase)ModelosTabelaMedidas).Dispose();
			LoaderProcessoService.EsconderLoader();
			((ViewModelValidationBase)this).Dispose();
		}

		protected virtual void OnAlterouCategoriaEvent(string e)
		{
			this.AlterouCategoriaEvent?.Invoke(this, e);
		}
	}
	public class AnuncioShopeeFichaAdicionarViewModel : FichaBaseAdicionar<AnuncioDto, AnuncioShopeeViewModel, IAnuncioShopeeModuleService>
	{
		public AnuncioShopeeFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class AnuncioShopeeFichaAtualizarViewModel : FichaBaseAtualizar<AnuncioDto, AnuncioShopeeViewModel, IAnuncioShopeeModuleService>
	{
		public AnuncioShopeeFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class AnuncioShopeeFormulario : FormularioEntity<AnuncioDto>
	{
		private readonly AnuncioShopeeViewModel AnuncioShopeeViewModel;

		private readonly AnuncioShopeeAtacadoViewModel AnuncioShopeeAtacadoViewModel;

		private AnuncioShopeeVariacaoViewModel AnuncioShopeeVariacaoViewModel;

		public AnuncioShopeeFormulario(AnuncioDto dto)
			: base(dto)
		{
			AnuncioShopeeViewModel = new AnuncioShopeeViewModel(base.Dto);
			AnuncioShopeeAtacadoViewModel = new AnuncioShopeeAtacadoViewModel(base.Dto, AnuncioShopeeViewModel);
			AnuncioShopeeViewModel.AlterouProdutoEvent += AlterouProdutoEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[5]
			{
				(IAbaViewModelBase)new AnuncioShopeeAbaViewModel((IViewModelPrincipal)(object)AnuncioShopeeViewModel),
				(IAbaViewModelBase)new AnuncioShopeeCategoriaAbaViewModel((IViewModelDtoBase)(object)new AnuncioShopeeCategoriaViewModel(base.Dto)),
				(IAbaViewModelBase)new AnuncioShopeeEntregaAbaViewModel((IViewModelDtoBase)(object)new AnuncioShopeeEntregaViewModel(base.Dto)),
				(IAbaViewModelBase)new AnuncioShopeeImagensAbaViewModel((IViewModelDtoBase)(object)new AnuncioShopeeImagensViewModel(base.Dto)),
				(IAbaViewModelBase)new AnuncioShopeeAtacadoAbaViewModel((IViewModelDtoBase)(object)AnuncioShopeeAtacadoViewModel)
			});
			if (base.Dto.PossuiGrade)
			{
				AnuncioShopeeVariacaoViewModel = new AnuncioShopeeVariacaoViewModel(base.Dto, AnuncioShopeeViewModel);
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnuncioShopeeVariacaoAbaViewModel((IViewModelDtoBase)(object)AnuncioShopeeVariacaoViewModel) });
				AnuncioShopeeAtacadoViewModel.VincularVariacao(AnuncioShopeeVariacaoViewModel);
			}
		}

		public void AlterouProdutoEvent(object sender, EstoqueDto estoque)
		{
			if (HelperObject.IsExactlyObject<EstoqueGradeDto>((object)estoque))
			{
				if (!((Formulario)this).Abas.Any((IAbaViewModelBase x) => HelperObject.Is<AnuncioShopeeVariacaoAbaViewModel>((object)x)))
				{
					AnuncioShopeeVariacaoViewModel = new AnuncioShopeeVariacaoViewModel(base.Dto, AnuncioShopeeViewModel);
					((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnuncioShopeeVariacaoAbaViewModel((IViewModelDtoBase)(object)AnuncioShopeeVariacaoViewModel) });
					AnuncioShopeeAtacadoViewModel.VincularVariacao(AnuncioShopeeVariacaoViewModel);
				}
				return;
			}
			IAbaViewModelBase val = ((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => HelperObject.Is<AnuncioShopeeVariacaoAbaViewModel>((object)x));
			if (val != null)
			{
				((Formulario)this).RemoverAba(val);
				AnuncioShopeeAtacadoViewModel.DesvincularVariacao();
				((IDisposable)val).Dispose();
				AnuncioShopeeVariacaoViewModel = null;
			}
		}

		public override void Dispose()
		{
			AnuncioShopeeViewModel.AlterouProdutoEvent -= AlterouProdutoEvent;
			((Formulario)this).Dispose();
		}
	}
	public class AnuncioShopeeAtacadoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioShopeeAtacadoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Atacado", "Preço diferenciado", viewModel)
		{
		}
	}
	public class AnuncioShopeeImagensAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AnuncioShopeeImagensAbaViewModel(IViewModelDtoBase viewModel)
			: base("Mídia", "Imagens e vídeo", viewModel)
		{
		}
	}
	public class AnuncioShopeeImagensViewModel : ViewModelDtoBase<AnuncioDto>
	{
		private readonly IDisposable _whenAlterouItemSelecionadoSubscription;

		public Lista<ImagemAnuncioDto> Imagens { get; set; } = new Lista<ImagemAnuncioDto>();


		public string VideoUrl
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => VideoUrl));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => VideoUrl), value);
			}
		}

		public string ThumbnailUrl
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ThumbnailUrl));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ThumbnailUrl), value);
			}
		}

		public string VideoUploadId
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => VideoUploadId));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => VideoUploadId), value);
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

		public override string[] Pendencies
		{
			get
			{
				if (((Validation)this).Valid)
				{
					return new string[0];
				}
				List<string> list = new List<string>();
				if (HelperObject.IsNotNull((object)Imagens) && Imagens.Numero < 1)
				{
					list.Add("É obrigatório cadastrar no mínimo 1 imagem.");
				}
				return list.ToArray();
			}
		}

		public string LinkShopee
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => LinkShopee));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => LinkShopee), value);
			}
		}

		public ICommand BuscarImagensCommand { get; set; }

		public ICommand BuscarVideoCommand { get; set; }

		public ICommand RemoverImagemCommand { get; set; }

		public ICommand RemoverVideoCommand { get; set; }

		public AnuncioShopeeImagensViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			RemoverImagemCommand = (ICommand)new DelegateCommand<object>((Action<object>)RemoverImagem);
			RemoverVideoCommand = (ICommand)new DelegateCommand((Action)RemoverVideo, true);
			BuscarImagensCommand = (ICommand)new AsyncCommand((Func<Task>)BuscarImagens, (Func<bool>)PodeBuscarImagem, (bool?)null);
			BuscarVideoCommand = (ICommand)new AsyncCommand((Func<Task>)BuscarVideo, (Func<bool>)PodeBuscarVideo, (bool?)null);
			_whenAlterouItemSelecionadoSubscription = ObservableExtensions.Subscribe<ImagemAnuncioDto>(Imagens.WhenAlterouItemSelecionado, (Action<ImagemAnuncioDto>)delegate
			{
				ImagemAnuncioDto itemSelecionado = Imagens.ItemSelecionado;
				LinkShopee = HelperString.ToStringEmptyWhenNull((itemSelecionado != null) ? itemSelecionado.Url : null);
			});
		}

		private async Task BuscarVideo()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Filter = "Vídeos|*.mp4",
				Title = "Selecione um vídeo (MP4, até 30MB, entre 10s e 60s, máx. 1280x1280)"
			};
			if (openFileDialog.ShowDialog() == true && ValidarVideo(openFileDialog))
			{
				LoaderProcessoService.MostrarLoader("Enviando vídeo...");
				VideoCreationResponse val = await DadosSistema.GetShopeeApiService().ProcessarVideoAsync(openFileDialog.FileName);
				VideoUrl = val.video_info.video_url_list.FirstOrDefault((VideoUrlItem x) => x.video_url_region == "BR").video_url;
				ThumbnailUrl = val.video_info.thumbnail_url_list.FirstOrDefault((ThumbnailUrlItem x) => x.image_url_region == "BR").image_url;
				VideoUploadId = val.video_upload_id;
				LoaderProcessoService.EsconderLoader();
			}
		}

		private bool ValidarVideo(OpenFileDialog dialog)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			FileInfo fileInfo = new FileInfo(dialog.FileName);
			if (fileInfo.Length > 31457280)
			{
				DialogoFactory.MostrarDialogoConfirma("O vídeo deve ter no máximo 30MB.", false, HorizontalAlignment.Center);
				return false;
			}
			MediaFile val = new MediaFile
			{
				Filename = fileInfo.FullName
			};
			Engine val2 = new Engine();
			try
			{
				val2.GetMetadata(val);
				double totalSeconds = val.Metadata.Duration.TotalSeconds;
				string[] array = val.Metadata.VideoData.FrameSize.Split('x');
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				if (totalSeconds < 10.0 || totalSeconds > 60.0)
				{
					DialogoFactory.MostrarDialogoConfirma($"A duração do vídeo deve estar entre 10 e 60 segundos. ({totalSeconds:F1}s)", false, HorizontalAlignment.Center);
					return false;
				}
				if (num > 1280 || num2 > 1280)
				{
					DialogoFactory.MostrarDialogoConfirma($"A resolução máxima permitida é 1280x1280 (atual: {num}x{num2}).", false, HorizontalAlignment.Center);
					return false;
				}
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
			return true;
		}

		private async Task BuscarImagens()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = true,
				Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;",
				Title = "Selecione até 9 imagens"
			};
			if (openFileDialog.ShowDialog() == true)
			{
				try
				{
					List<ImagemAnuncioDto> listaImagens = new List<ImagemAnuncioDto>();
					LoaderProcessoService.MostrarLoader("Enviando imagens...");
					foreach (string item in openFileDialog.FileNames.Take(ContadorImagens()).ToList())
					{
						string text = await DadosSistema.GetShopeeApiService().CarregarImagemAsync(item);
						listaImagens.Add(new ImagemAnuncioDto
						{
							Url = text,
							Id = text.Replace("https://cf.shopee.com.br/file/", "")
						});
					}
					LoaderProcessoService.EsconderLoader();
					Imagens.Adicionar((IEnumerable<ImagemAnuncioDto>)listaImagens);
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
				}
			}
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), (Expression<Func<string>>)(() => ((Validation)this).Error));
		}

		private void RemoverImagem(object obj)
		{
			Imagens.Remover((ImagemAnuncioDto)((obj is ImagemAnuncioDto) ? obj : null));
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeImagensViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		private void RemoverVideo()
		{
			VideoUrl = "";
			ThumbnailUrl = "";
			VideoUploadId = "";
		}

		public override void ProjectedAsViewModel()
		{
			Imagens.Adicionar(base.Dto.Imagens.Imagens);
			VideoUrl = base.Dto.VideoUrl;
			ThumbnailUrl = base.Dto.ThumbnailUrl;
			VideoUploadId = base.Dto.VideoUploadId;
			((BindableBase)this).RaisePropertiesChanged<bool, string>((Expression<Func<bool>>)(() => ((Validation)this).Valid), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeImagensViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Imagens.Imagens = Imagens.Itens;
				val.VideoUrl = VideoUrl;
				val.ThumbnailUrl = ThumbnailUrl;
				val.VideoUploadId = VideoUploadId;
				base.AtualizarDto<AnuncioDto>(ref val);
			}
		}

		public override void Dispose()
		{
			_whenAlterouItemSelecionadoSubscription?.Dispose();
			((ViewModelValidationBase)this).Dispose();
		}

		private int ContadorImagens()
		{
			return 9 - Imagens.Itens.Count;
		}

		private bool PodeBuscarImagem()
		{
			return Imagens.Itens.Count < 9;
		}

		private bool PodeBuscarVideo()
		{
			return HelperString.IsNullOrEmpty(VideoUrl);
		}
	}
	public class AnuncioShopeeImportarAbaViewModel : AbaPrincipalViewModelBase
	{
		public AnuncioShopeeImportarAbaViewModel(IViewModelPrincipal viewModel)
			: base("Importar", "Importar", viewModel)
		{
		}
	}
	public class AnuncioShopeeImportarFichaViewModel : FichaBase<AnuncioDto, AnuncioShopeeImportarViewModel>
	{
		private AnuncioDto _parametro;

		public ICommand AdicionarCommand { get; set; }

		public AnuncioShopeeImportarFichaViewModel()
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
	public class AnuncioShopeeImportarFormulario : FormularioEntity<AnuncioDto>
	{
		public AnuncioShopeeImportarFormulario(AnuncioDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AnuncioShopeeImportarAbaViewModel((IViewModelPrincipal)(object)new AnuncioShopeeImportarViewModel(base.Dto)) });
		}
	}
	public class AnuncioShopeeImportarViewModel : ViewModelDtoBase<AnuncioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'ID Shopee' é obrigatório.")]
		public long IdShopee
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => IdShopee));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => IdShopee), value);
			}
		}

		[Required]
		public Anuncio Item
		{
			get
			{
				return ((BindableBase)this).GetProperty<Anuncio>((Expression<Func<Anuncio>>)(() => Item));
			}
			set
			{
				((BindableBase)this).SetProperty<Anuncio>((Expression<Func<Anuncio>>)(() => Item), value);
			}
		}

		public ListaVariacoes Variacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<ListaVariacoes>((Expression<Func<ListaVariacoes>>)(() => Variacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<ListaVariacoes>((Expression<Func<ListaVariacoes>>)(() => Variacoes), value);
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

		public int ItemStock
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => ItemStock));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => ItemStock), value);
			}
		}

		public string ItemVideoThumbnail
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ItemVideoThumbnail));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ItemVideoThumbnail), value);
			}
		}

		public ICommand ConsultarCommand { get; set; }

		public AnuncioShopeeImportarViewModel()
		{
		}

		public AnuncioShopeeImportarViewModel(AnuncioDto dto)
			: base(dto)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			ConsultarCommand = (ICommand)new AsyncCommand((Func<Task>)Consultar, (Func<bool>)PodeConsultar, false, (bool?)null);
		}

		private bool PodeConsultar()
		{
			return IdShopee != 0;
		}

		private async Task Consultar()
		{
			LoaderProcessoService.MostrarLoader("Consultando anúncio...");
			string anuncioCadastrado = null;
			try
			{
				anuncioCadastrado = await ObterAnuncioCadastrado(IdShopee);
				if (anuncioCadastrado == null)
				{
					Anuncio val = await DadosSistema.GetShopeeApiService().ConsultarAnuncioShopeeAsync(IdShopee);
					if (val.has_model)
					{
						DialogoFactory.MostrarDialogoConfirma("Este produto possui variações vinculadas. Para garantir a consistência das informações, crie o anúncio por esta tela selecionando um produto com controle de grade.", false, HorizontalAlignment.Center);
					}
					Item = val;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			LoaderProcessoService.EsconderLoader();
			if (anuncioCadastrado != null)
			{
				Item = null;
				ItemStatus = string.Empty;
				ItemStock = 0;
				ItemVideoThumbnail = string.Empty;
				DialogoFactory.MostrarDialogoConfirma(anuncioCadastrado, false, HorizontalAlignment.Center);
			}
			else if (!HelperObject.IsNull((object)Item))
			{
				StatusAnuncioShopee val2 = (StatusAnuncioShopee)((!(Item.item_status?.ToLower() == "normal")) ? 1 : 2);
				ItemStatus = HelperEnum.RetornarDescricao((Enum)(object)val2);
				AnuncioShopeeImportarViewModel anuncioShopeeImportarViewModel = this;
				List<SellerStock> seller_stock = Item.seller_stock;
				int? obj;
				if (seller_stock == null)
				{
					obj = null;
				}
				else
				{
					SellerStock obj2 = seller_stock.FirstOrDefault();
					obj = ((obj2 != null) ? new int?(obj2.stock) : null);
				}
				anuncioShopeeImportarViewModel.ItemStock = obj ?? 0;
				AnuncioShopeeImportarViewModel anuncioShopeeImportarViewModel2 = this;
				List<VideoInfoAnuncio> video_info = Item.video_info;
				object obj3;
				if (video_info == null)
				{
					obj3 = null;
				}
				else
				{
					VideoInfoAnuncio obj4 = video_info.FirstOrDefault();
					obj3 = ((obj4 != null) ? obj4.thumbnail_url : null);
				}
				anuncioShopeeImportarViewModel2.ItemVideoThumbnail = HelperString.ToStringEmptyWhenNull((string)obj3);
			}
		}

		private static async Task<string> ObterAnuncioCadastrado(long idShopee)
		{
			string informacao = idShopee.ToString();
			AnuncioDto val = (await DadosSistema.GetService().Canal<IAnuncioShopeeModuleService, Task<PaginaEntityDto>>((Func<IAnuncioShopeeModuleService, Task<PaginaEntityDto>>)((IAnuncioShopeeModuleService service) => ((IPesquisarAtivos)service).PesquisarAtivosAsync(informacao, (short)1, (short)30)))).Lista.OfType<AnuncioDto>().FirstOrDefault((AnuncioDto x) => x.IdShopee == idShopee && !x.Deletado);
			if (val != null)
			{
				return $"Este anúncio já está cadastrado no sistema ({val.Titulo}). Para alterá-lo, use F6 - Editar na lista de anúncios.";
			}
			AnuncioDto val2 = (await DadosSistema.GetService().Canal<IAnuncioShopeeModuleService, Task<PaginaEntityDto>>((Func<IAnuncioShopeeModuleService, Task<PaginaEntityDto>>)((IAnuncioShopeeModuleService service) => ((IPesquisarInativos)service).PesquisarInativosAsync(informacao, (short)1, (short)30)))).Lista.OfType<AnuncioDto>().FirstOrDefault((AnuncioDto x) => x.IdShopee == idShopee && !x.Deletado);
			if (val2 != null)
			{
				return $"Este anúncio já está cadastrado no sistema, mas está inativo ({val2.Titulo}). Pesquise com o status Inativos na lista de anúncios para ativá-lo.";
			}
			return null;
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Expected O, but got Unknown
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Expected O, but got Unknown
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			val.IdShopee = Item.item_id;
			val.Logistica = Item.logistic_info?.Select((Func<LogisticInfo, LogisticaDto>)((LogisticInfo x) => new LogisticaDto
			{
				Nome = x.logistic_name,
				IdTransportadora = x.logistic_id,
				Ativo = x.enabled,
				FreteGratis = x.is_free,
				TaxaEnvio = x.shipping_fee,
				IdTamanho = x.size_id,
				Selecionada = true
			})).ToList() ?? new List<LogisticaDto>();
			AnuncioDto obj2 = val;
			DimencoesDto val2 = new DimencoesDto();
			Dimension dimension = Item.dimension;
			val2.Altura = ((dimension != null) ? dimension.package_height : 0);
			Dimension dimension2 = Item.dimension;
			val2.Largura = ((dimension2 != null) ? dimension2.package_width : 0);
			Dimension dimension3 = Item.dimension;
			val2.Comprimento = ((dimension3 != null) ? dimension3.package_length : 0);
			val2.Peso = Item.weight;
			obj2.Dimencoes = val2;
			val.Atributos = MontarListaDto(Item.attribute_list);
			AnuncioDto obj3 = val;
			ImagemDto val3 = new ImagemDto();
			Imagens image = Item.image;
			val3.Imagens = ((image == null) ? null : image.image_id_list?.Select((Func<string, ImagemAnuncioDto>)((string x) => new ImagemAnuncioDto
			{
				Url = x,
				Id = x.Replace("https://cf.shopee.com.br/file/", "")
			})).ToList()) ?? new List<ImagemAnuncioDto>();
			obj3.Imagens = val3;
			val.Permalink = $"https://shopee.com.br/product/{DadosSistema.GetShopeeApiService().ObterShopId()}/{Item.item_id}";
			AnuncioDto obj4 = val;
			List<VideoInfoAnuncio> video_info = Item.video_info;
			object obj5;
			if (video_info == null)
			{
				obj5 = null;
			}
			else
			{
				VideoInfoAnuncio obj6 = video_info.FirstOrDefault();
				obj5 = ((obj6 != null) ? obj6.video_url : null);
			}
			obj4.VideoUrl = HelperString.ToStringEmptyWhenNull((string)obj5);
			AnuncioDto obj7 = val;
			List<VideoInfoAnuncio> video_info2 = Item.video_info;
			object obj8;
			if (video_info2 == null)
			{
				obj8 = null;
			}
			else
			{
				VideoInfoAnuncio obj9 = video_info2.FirstOrDefault();
				obj8 = ((obj9 != null) ? obj9.thumbnail_url : null);
			}
			obj7.ThumbnailUrl = HelperString.ToStringEmptyWhenNull((string)obj8);
			AnuncioDto obj10 = val;
			PreOrder pre_order = Item.pre_order;
			obj10.SobEncomenda = pre_order != null && pre_order.is_pre_order;
			AnuncioDto obj11 = val;
			PreOrder pre_order2 = Item.pre_order;
			obj11.DiasParaEnvio = ((pre_order2 != null) ? pre_order2.days_to_ship : 0);
			AnuncioDto obj12 = val;
			Brand brand = Item.brand;
			obj12.IdMarca = ((brand != null) ? brand.brand_id : 0);
			AnuncioDto obj13 = val;
			Brand brand2 = Item.brand;
			obj13.DescricaoMarca = HelperString.ToStringEmptyWhenNull((brand2 != null) ? brand2.original_brand_name : null);
			AnuncioDto obj14 = val;
			List<SellerStock> seller_stock = Item.seller_stock;
			int? obj15;
			if (seller_stock == null)
			{
				obj15 = null;
			}
			else
			{
				SellerStock obj16 = seller_stock.FirstOrDefault();
				obj15 = ((obj16 != null) ? new int?(obj16.stock) : null);
			}
			obj14.QuantidadeDisponivel = obj15 ?? 0;
			val.ProdutoServicoEmpresaCodigoBarras = HelperString.ToStringEmptyWhenNull(Item.gtin_code);
			val.Preco = Item.original_price;
			val.Titulo = HelperString.ToStringEmptyWhenNull(Item.item_name);
			val.Descricao = HelperString.ToStringEmptyWhenNull(Item.description);
			val.Status = (StatusAnuncioShopee)((!(Item.item_status?.ToLower() == "normal")) ? 1 : 2);
			val.PossuiGrade = Item.has_model;
			val.CategoriaId = Item.category_id.ToString();
			val.Sku = HelperString.ToStringEmptyWhenNull(Item.item_sku);
			val.Condicao = HelperString.ToStringEmptyWhenNull(Item.condition);
			val.Wholesale = Item.wholesale?.Select((Func<Wholesale, WholesaleDto>)((Wholesale x) => new WholesaleDto
			{
				min_count = x.min_count,
				max_count = x.max_count,
				unit_price = x.unit_price
			})).ToList() ?? new List<WholesaleDto>();
			base.AtualizarDto<AnuncioDto>(ref val);
		}

		public List<AtributoDto> MontarListaDto(List<AttributeList> atributosLista)
		{
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0195: Expected O, but got Unknown
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Expected O, but got Unknown
			//IL_00d7: Expected O, but got Unknown
			List<AtributoDto> list = new List<AtributoDto>();
			if (HelperObject.IsNull((object)atributosLista))
			{
				return list;
			}
			foreach (AttributeList atributosListum in atributosLista)
			{
				if (atributosListum.attribute_value_list == null || atributosListum.attribute_value_list.Count == 0)
				{
					continue;
				}
				if (atributosListum.attribute_value_list.Count == 1)
				{
					AttributeValue val = atributosListum.attribute_value_list.First();
					if (val.value_id > 0 && HelperObject.IsNotNull((object)val.original_value_name))
					{
						AtributoListaDto item = new AtributoListaDto
						{
							Id = atributosListum.attribute_id.ToString(),
							Titulo = HelperString.ToStringEmptyWhenNull(atributosListum.original_attribute_name),
							OpcaoSelecionada = new AtributoOpcaoDto
							{
								Id = val.value_id.ToString(),
								Valor = val.original_value_name,
								Unidade = val.value_unit
							}
						};
						list.Add((AtributoDto)(object)item);
					}
					else
					{
						AtributoTextoDto item2 = new AtributoTextoDto
						{
							Id = atributosListum.attribute_id.ToString(),
							Titulo = HelperString.ToStringEmptyWhenNull(atributosListum.original_attribute_name),
							Valor = val.original_value_name,
							Unidade = val.value_unit
						};
						list.Add((AtributoDto)(object)item2);
					}
				}
				else
				{
					AtributoListaSelecionavelDto item3 = new AtributoListaSelecionavelDto
					{
						Id = atributosListum.attribute_id.ToString(),
						Titulo = HelperString.ToStringEmptyWhenNull(atributosListum.original_attribute_name),
						OpcoesSelecionadas = ((IEnumerable<AttributeValue>)atributosListum.attribute_value_list).Select((Func<AttributeValue, AtributoOpcaoDto>)((AttributeValue v) => new AtributoOpcaoDto
						{
							Id = v.value_id.ToString(),
							Valor = v.original_value_name,
							Unidade = v.value_unit
						})).ToList()
					};
					list.Add((AtributoDto)(object)item3);
				}
			}
			return list;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AnuncioShopeeImportarFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Anúncios da Shopee";
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
	public class AnuncioShopeeListaViewModel : ListaEntity<AnuncioDto, AnuncioShopeeViewModel, IAnuncioShopeeModuleService, AnuncioShopee>
	{
		private readonly IWService _service;

		public string EnviarAtualizar
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => (((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0) ? "F8 - Enviar anúncio" : "F8 - Atualizar anúncio"));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => EnviarAtualizar), value);
			}
		}

		public ICommand AtualizarCategoriasCommand { get; set; }

		public ICommand SincronizarCommand { get; set; }

		public ICommand SincronizarTudoCommand { get; set; }

		public ICommand AcessarAnuncioCommand { get; set; }

		public ICommand ConsultarStatusAnuncioCommand { get; set; }

		public ICommand EnviarAnuncioCommand { get; set; }

		public ICommand EnviarTodosAnunciosCommand { get; set; }

		public ICommand PausarAnuncioCommand { get; set; }

		public ICommand AtivarAnuncioCommand { get; set; }

		public ICommand ExcluirAnuncioCommand { get; set; }

		public ICommand ImportarShopeeCommand { get; set; }

		public ICommand ReativarParaNovoAnuncioCommand { get; set; }

		public AnuncioShopeeListaViewModel(IWService service, ISessao sessao, IEntityAtivos<AnuncioDto, IAnuncioShopeeModuleService> ativos, IEntityInativos<AnuncioDto, IAnuncioShopeeModuleService> inativos)
			: base(sessao, typeof(AnuncioShopeeFichaAdicionarView).Name, typeof(AnuncioShopeeFichaAtualizarView).Name, ativos, inativos)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01eb: Expected O, but got Unknown
			//IL_020d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Expected O, but got Unknown
			_service = service;
			AtualizarCategoriasCommand = (ICommand)new AsyncCommand((Func<Task>)AtualizarCategorias);
			SincronizarCommand = (ICommand)new AsyncCommand((Func<Task>)Sincronizar, (Func<bool>)PodeSincronizar, false, (bool?)null);
			SincronizarTudoCommand = (ICommand)new AsyncCommand((Func<Task>)SincronizarTudo, (Func<bool>)PodeSincronizarTudo, false, (bool?)null);
			AcessarAnuncioCommand = (ICommand)new DelegateCommand((Action)AcessarAnuncio, (Func<bool>)PodeAcessarAnuncio, (bool?)null);
			ConsultarStatusAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)ConsultarStatusAnuncio, (Func<bool>)PodeAcessarAnuncio, (bool?)null);
			EnviarAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarAnuncio, (Func<bool>)PodeEnviarAnuncio, (bool?)null);
			EnviarTodosAnunciosCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarTodosAnuncios, (Func<bool>)PodeEnviarTodosAnuncios, (bool?)null);
			AtivarAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)AtivarAnuncio, (Func<bool>)PodeAtivarAnuncio, (bool?)null);
			PausarAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)PausarAnuncio, (Func<bool>)PodePausarAnuncio, (bool?)null);
			ExcluirAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)ExcluirAnuncio, (Func<bool>)PodeExcluirAnuncio, (bool?)null);
			ImportarShopeeCommand = (ICommand)new DelegateCommand((Action)ImportarShopee);
			ReativarParaNovoAnuncioCommand = (ICommand)new AsyncCommand((Func<Task>)ReativarParaNovoAnuncio, (Func<bool>)PodeReativarParaNovoAnuncio, (bool?)null);
		}

		private async Task AtualizarCategorias()
		{
			await ExecutarComTratamento(async delegate
			{
				await DadosSistema.GetShopeeApiService().AtualizarCategoriasAsync();
			}, "Atualizando categorias...");
		}

		private async void ImportarShopee()
		{
			if (await ValidarAutenticacaoShopee())
			{
				AnuncioShopeeListaViewModel anuncioShopeeListaViewModel = this;
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(AnuncioShopeeImportarViewModel).Name, (object)new AnuncioShopeeImportarViewModel());
				((NavigationBase)anuncioShopeeListaViewModel).Navegar("AnuncioShopeeImportarFichaView", obj, val);
				((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
			}
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
			object obj = regionNavigationEventArgs.NavigationContext.Parameters["DtoIncluir"];
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!(regionNavigationEventArgs.Uri.OriginalString != typeof(AnuncioShopeeListaView).Name) && val != null)
			{
				((ListaManutencaoBase<AnuncioDto, AnuncioShopeeViewModel, AnuncioShopee>)(object)this).Adicionar(val);
			}
		}

		private bool PodeSincronizar()
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Permalink) && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 2 && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task Sincronizar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("O preço e o estoque do anúncio na Shopee serão atualizados com os dados do sistema. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				await ExecutarComTratamento(() => SincronizarPrecoEstoque(dto), "Atualizando preço e estoque do anúncio...");
			}
		}

		private bool PodeSincronizarTudo()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task SincronizarTudo()
		{
			if (DialogoFactory.MostrarDialogoSimNao("O preço e o estoque dos anúncios listados serão atualizados na Shopee com os dados do sistema. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			List<AnuncioDto> list = (from x in ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Data
				select ((ViewModelDtoBase<AnuncioDto>)(object)x).Dto into x
				where x.IdShopee != 0L && !x.Deletado && (int)x.Status == 2
				select x).ToList();
			int sincronizados = 0;
			List<string> falhas = new List<string>();
			LoaderProcessoService.MostrarLoader("Atualizando preço e estoque dos anúncios...");
			try
			{
				foreach (AnuncioDto dto in list)
				{
					try
					{
						await SincronizarPrecoEstoque(dto);
						sincronizados++;
					}
					catch (AuthenticationException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						falhas.Add($"{dto.Titulo}: {ex2.Message}");
					}
				}
			}
			catch (AuthenticationException)
			{
				NavegarShopeeModulo();
				return;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
			string text = $"Anúncios sincronizados: {sincronizados}.";
			if (falhas.Any())
			{
				text += $"{Environment.NewLine}{Environment.NewLine}Não sincronizados:{Environment.NewLine}{string.Join(Environment.NewLine, falhas)}";
			}
			DialogoFactory.MostrarDialogoConfirma(text, false, HorizontalAlignment.Center);
		}

		private async Task SincronizarPrecoEstoque(AnuncioDto dto)
		{
			if (dto.UtilizarInformacaoCadastroSincronismo && !HelperString.IsNullOrEmpty(dto.ProdutoServicoEmpresaReferencia))
			{
				ProdutoServicoEmpresaDto produtoCadastro = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { dto.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
				EstoqueGradeDto estoqueGrade = default(EstoqueGradeDto);
				ref EstoqueGradeDto reference = ref estoqueGrade;
				EstoqueDto estoque = produtoCadastro.ProdutoEmpresa.Estoque;
				reference = (EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null);
				if (estoqueGrade != null)
				{
					LinqExtensions.ForEach<ModelDto>(dto.Variacoes, (Action<ModelDto>)delegate(ModelDto x)
					{
						GradeDto val = estoqueGrade.Grades.FirstOrDefault((GradeDto g) => ((EntityBaseDto)g.Estoque).Id == x.Sku);
						if (val != null)
						{
							x.QuantidadeDisponivel = HelperObject.ToInt((object)val.Estoque.QuantidadeSaldo);
						}
					});
					LinqExtensions.ForEach<ModelDto>(dto.Variacoes, (Action<ModelDto>)delegate(ModelDto x)
					{
						x.Preco = produtoCadastro.Preco.PrecoVendaVigente;
					});
				}
				dto.QuantidadeDisponivel = Convert.ToInt32(produtoCadastro.ProdutoEmpresa.Estoque.QuantidadeSaldo);
				dto.Preco = produtoCadastro.Preco.PrecoVendaVigente;
			}
			if (!dto.PossuiGrade)
			{
				await DadosSistema.GetShopeeApiService().AtualizarPrecoAnuncioShopeeAsync(dto.IdShopee, dto.Preco);
				await DadosSistema.GetShopeeApiService().AtualizarEstoqueAnuncioShopeeAsync(dto.IdShopee, dto.QuantidadeDisponivel);
				return;
			}
			if (dto.Variacoes.Any((ModelDto x) => x.ModelIdShopee == 0))
			{
				Response response = (await DadosSistema.GetShopeeApiService().ObterVariacoesAsync(dto.IdShopee)).response;
				Dictionary<string, long> modelIdPorSku = (from m in ((response != null) ? response.model : null) ?? new List<Model>()
					where !string.IsNullOrEmpty(m.model_sku)
					group m by m.model_sku).ToDictionary((IGrouping<string, Model> g) => g.Key, (IGrouping<string, Model> g) => g.First().model_id);
				LinqExtensions.ForEach<ModelDto>(dto.Variacoes, (Action<ModelDto>)delegate(ModelDto v)
				{
					if (v.ModelIdShopee == 0L && modelIdPorSku.ContainsKey(v.Sku))
					{
						v.ModelIdShopee = modelIdPorSku[v.Sku];
					}
				});
			}
			List<IGrouping<long, ModelDto>> source = (from x in dto.Variacoes
				where x.ModelIdShopee != 0
				group x by x.ModelIdShopee).ToList();
			if (!source.Any())
			{
				throw new Exception("Nenhuma variação deste anúncio está vinculada às variações da Shopee. Edite o anúncio, acesse a aba \"Variações\" e informe o \"Model Shopee\" de cada variação antes de sincronizar.");
			}
			IEnumerable<Tuple<decimal, long>> enumerable = source.Select((IGrouping<long, ModelDto> g) => new Tuple<decimal, long>(g.First().Preco, g.Key));
			IEnumerable<Tuple<int, long>> estoques = source.Select((IGrouping<long, ModelDto> g) => new Tuple<int, long>(g.Sum((ModelDto x) => x.QuantidadeDisponivel), g.Key));
			await DadosSistema.GetShopeeApiService().AtualizarPrecoAnuncioGradeShopeeAsync(dto.IdShopee, enumerable);
			await DadosSistema.GetShopeeApiService().AtualizarEstoqueAnuncioGradeShopeeAsync(dto.IdShopee, estoques);
		}

		private bool PodeAcessarAnuncio()
		{
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && !string.IsNullOrEmpty(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Permalink) && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado)
			{
				return (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 2;
			}
			return false;
		}

		private void AcessarAnuncio()
		{
			Process.Start(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Permalink);
		}

		private async Task ConsultarStatusAnuncio()
		{
			DialogoFactory.MostrarDialogoConfirma($"Status: {(await DadosSistema.GetShopeeApiService().ConsultarAnuncioShopeeAsync(((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee)).item_status}", false, HorizontalAlignment.Center);
		}

		private bool PodeExcluirAnuncio()
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task ExcluirAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("O anúncio e todos os dados vinculados a ele serão excluídos da Shopee. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				await ExecutarComTratamento(async delegate
				{
					await DadosSistema.GetShopeeApiService().DeletarAnuncioShopeeAsync(dto.IdShopee);
				}, "Excluindo anúncio...");
				dto.Status = (StatusAnuncioShopee)1;
				dto.Deletado = true;
				((EntityDto)dto).Ativo = false;
				await AtualizarAnuncio(dto);
			}
		}

		private bool PodeReativarParaNovoAnuncio()
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
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
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				dto.Status = (StatusAnuncioShopee)2;
				dto.AlertasShopee = string.Empty;
				dto.IdShopee = 0L;
				dto.Permalink = string.Empty;
				await AtualizarAnuncio(dto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodePausarAnuncio()
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status != 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task PausarAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ao pausar o anúncio, ele ficará indisponível para compradores na Shopee. Você poderá ativá-lo novamente quando quiser. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				await ExecutarComTratamento(async delegate
				{
					await DadosSistema.GetShopeeApiService().DesativarAnuncioShopeeAsync(dto.IdShopee);
				}, "Pausando anúncio...");
				dto.Status = (StatusAnuncioShopee)1;
				await AtualizarAnuncio(dto);
			}
		}

		private bool PodeAtivarAnuncio()
		{
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Invalid comparison between Unknown and I4
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.IdShopee != 0L && !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 1)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task AtivarAnuncio()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ao ativar o anúncio, ele ficará disponível para compradores na Shopee. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				await ExecutarComTratamento(async delegate
				{
					await DadosSistema.GetShopeeApiService().AtivarAnuncioShopeeAsync(dto.IdShopee);
				}, "Ativando anúncio...");
				dto.Status = (StatusAnuncioShopee)2;
				await AtualizarAnuncio(dto);
			}
		}

		private bool PodeEnviarTodosAnuncios()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task EnviarTodosAnuncios()
		{
			_ = 1;
			try
			{
				IEnumerable<AnuncioDto> enumerable = from x in ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Data
					select ((ViewModelDtoBase<AnuncioDto>)(object)x).Dto into x
					where (int)x.Status == 2
					select x;
				if (DialogoFactory.MostrarDialogoSimNao("Anúncios já enviados para a Shopee serão atualizados. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				Lista<long> idsNaoSincronizados = new Lista<long>();
				Lista<long> idsSincronizados = new Lista<long>();
				foreach (AnuncioDto dto in enumerable)
				{
					try
					{
						if (dto.IdShopee != 0L)
						{
							await AtualizarAnuncioShopee(dto);
							idsSincronizados.Adicionar(dto.IdShopee);
						}
						else
						{
							await CadastrarAnuncioShopee(dto);
							idsSincronizados.Adicionar(dto.IdShopee);
						}
					}
					catch (Exception)
					{
						idsNaoSincronizados.Adicionar(dto.IdShopee);
					}
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					if (idsSincronizados.Itens.Any())
					{
						DialogoFactory.MostrarDialogoConfirma(string.Format("Total de anúncios enviados: {0}.\n {1}", idsSincronizados.Itens.Count, idsNaoSincronizados.Itens.Any() ? string.Format("Anúncios não enviados: {0}", string.Join(", ", idsNaoSincronizados.Itens)) : ""), false, HorizontalAlignment.Center);
					}
					((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
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
			if (HelperObject.IsNotNull((object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens) && ((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.SelecionadoValido && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 2)
			{
				return DadosSistema.VerificarPermissao<AnuncioShopee, InteragirPlataforma>();
			}
			return false;
		}

		private async Task EnviarAnuncio()
		{
			_ = 1;
			try
			{
				AnuncioDto dto = ((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto;
				if (dto.IdShopee != 0L)
				{
					if (DialogoFactory.MostrarDialogoSimNao("Este anúncio já foi enviado para a Shopee. Deseja enviar uma atualização?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes && PodeEnviarAnuncio())
					{
						await AtualizarAnuncioShopee(dto);
					}
				}
				else
				{
					await CadastrarAnuncioShopee(dto);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async Task CadastrarAnuncioShopee(AnuncioDto dto)
		{
			Anuncio body = MontarAnuncio(dto);
			await ExecutarComTratamento(async delegate
			{
				AnuncioCreationResponse val = await DadosSistema.GetShopeeApiService().EnviarAnuncioShopeeAsync(body);
				dto.IdShopee = val.response.item_id;
				dto.Permalink = $"https://shopee.com.br/product/{DadosSistema.GetShopeeApiService().ObterShopId()}/{val.response.item_id}";
				if (dto.PossuiGrade)
				{
					Thread.Sleep(5000);
					CriarVariacoes val2 = MontarVariacoes(dto);
					Response response = (await DadosSistema.GetShopeeApiService().CriarVariacoesAsync(val2)).response;
					IEnumerable<Model> enumerable = ((response != null) ? response.model : null);
					foreach (ModelDto x in dto.Variacoes)
					{
						ModelDto obj = x;
						long? obj2;
						if (enumerable == null)
						{
							obj2 = null;
						}
						else
						{
							Model obj3 = enumerable.FirstOrDefault((Model v) => v.model_sku == x.Sku);
							obj2 = ((obj3 != null) ? new long?(obj3.model_id) : null);
						}
						obj.ModelIdShopee = obj2 ?? 0;
					}
				}
				await AtualizarAnuncio(dto);
			}, "Enviando anúncio...");
		}

		private async Task AtualizarAnuncioShopee(AnuncioDto dto)
		{
			Anuncio body = MontarAnuncio(dto);
			await ExecutarComTratamento(async delegate
			{
				await DadosSistema.GetShopeeApiService().AtualizarAnuncioShopeeAsync(body);
				await AtualizarAnuncio(dto);
			}, "Atualizando anúncio...");
		}

		public Anuncio MontarAnuncio(AnuncioDto dto)
		{
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0239: Expected O, but got Unknown
			//IL_0239: Unknown result type (might be due to invalid IL or missing references)
			//IL_023e: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0265: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Unknown result type (might be due to invalid IL or missing references)
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b1: Expected O, but got Unknown
			//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e3: Expected O, but got Unknown
			//IL_039a: Unknown result type (might be due to invalid IL or missing references)
			//IL_039f: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ba: Expected O, but got Unknown
			//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ec: Expected O, but got Unknown
			if (dto.UtilizarInformacaoCadastroSincronismo && !HelperString.IsNullOrEmpty(dto.ProdutoServicoEmpresaReferencia))
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { dto.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
				dto.QuantidadeDisponivel = Convert.ToInt32(val.ProdutoEmpresa.Estoque.Quantidade);
				dto.Preco = val.Preco.PrecoVendaVigente;
			}
			List<LogisticInfo> list = (from x in dto.Logistica
				where x.Selecionada
				select LogisticInfo.Criar().ComNome(x.Nome).ComTamanhoId(x.IdTamanho)
					.ComTaxaEnvio(x.TaxaEnvio)
					.ComHabilitado(x.Ativo)
					.ComLogisticaId(x.IdTransportadora)
					.ComFreteGratis(x.FreteGratis && x.IdTransportadora != 90023)).ToList();
			Dimension val2 = Dimension.Criar().ComAltura(dto.Dimencoes.Altura).ComLargura(dto.Dimencoes.Largura)
				.ComComprimento(dto.Dimencoes.Comprimento);
			List<Wholesale> list2 = dto.Wholesale.Select((Func<WholesaleDto, Wholesale>)((WholesaleDto x) => new Wholesale
			{
				min_count = x.min_count,
				max_count = x.max_count,
				unit_price = x.unit_price
			})).ToList();
			Anuncio obj = Anuncio.Criar().ComOriginalPrice(dto.Preco).ComDescription(dto.Descricao)
				.ComPeso(dto.Dimencoes.Peso)
				.ComItemName(dto.Titulo);
			StatusAnuncioShopee status = dto.Status;
			Anuncio val3 = obj.ComItemStatus(((object)(StatusAnuncioShopee)(ref status)).ToString()).ComDimension(val2).ComLogisticInfo(list)
				.ComAttributeList(MontarListaAtributos(dto))
				.ComCategoryId(HelperString.ToInt(dto.CategoriaId))
				.ComImage(new Imagens
				{
					image_id_list = dto.Imagens.Imagens.Select((ImagemAnuncioDto x) => x.Id).ToList()
				})
				.ComPreOrder(new PreOrder
				{
					is_pre_order = dto.SobEncomenda,
					days_to_ship = dto.DiasParaEnvio
				})
				.ComItemSku(dto.ProdutoServicoCodigoInterno)
				.ComCondition(dto.Condicao)
				.ComBrand(new Brand
				{
					brand_id = dto.IdMarca,
					original_brand_name = dto.DescricaoMarca
				})
				.ComItemDangerous(0)
				.ComSellerStock(new List<SellerStock>
				{
					new SellerStock
					{
						location_id = "BRZ",
						stock = dto.QuantidadeDisponivel
					}
				})
				.ComGtin((dto.ProdutoServicoEmpresaCodigoBarras == "") ? "00" : dto.ProdutoServicoEmpresaCodigoBarras)
				.ComWholesale(list2);
			if (!HelperString.IsNullOrEmpty(dto.VideoUploadId))
			{
				val3.ComVideo(new List<string> { dto.VideoUploadId });
			}
			else if (dto.IdShopee != 0L)
			{
				val3.ComVideo(new List<string>());
			}
			if (dto.IdShopee != 0L)
			{
				val3.item_id = dto.IdShopee;
			}
			if (dto.IdModeloTabelaMedidas > 0)
			{
				val3.ComSizeChartInfo(new SizeChartInfo
				{
					size_chart_id = dto.IdModeloTabelaMedidas
				});
			}
			else if (!HelperString.IsNullOrEmpty(dto.IdImagemTabelaMedidas))
			{
				val3.ComSizeChartInfo(new SizeChartInfo
				{
					size_chart = dto.IdImagemTabelaMedidas
				});
			}
			return val3;
		}

		public CriarVariacoes MontarVariacoes(AnuncioDto dto)
		{
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Expected O, but got Unknown
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			List<string> linhas = dto.Variacoes.Select((ModelDto x) => x.Linha).Distinct().ToList();
			List<string> colunas = dto.Variacoes.Select((ModelDto x) => x.Coluna).Distinct().ToList();
			bool somenteColunas = linhas.Count == 1 && colunas.Count > 1;
			bool tier2 = !somenteColunas && colunas.Count > 1;
			List<InfoTierVariation> list = new List<InfoTierVariation> { somenteColunas ? new InfoTierVariation
			{
				variation_id = 0L,
				variation_name = linhas.First(),
				variation_option_list = ((IEnumerable<string>)colunas).Select((Func<string, VariationOption>)delegate(string x)
				{
					//IL_000d: Unknown result type (might be due to invalid IL or missing references)
					//IL_0012: Unknown result type (might be due to invalid IL or missing references)
					//IL_001a: Unknown result type (might be due to invalid IL or missing references)
					//IL_0026: Unknown result type (might be due to invalid IL or missing references)
					//IL_0055: Expected O, but got Unknown
					VariationOption val2 = new VariationOption
					{
						variation_option_id = 0L,
						variation_option_name = x
					};
					ModelDto obj2 = dto.Variacoes.FirstOrDefault((ModelDto v) => v.Coluna == x);
					val2.image_id = ((obj2 != null) ? obj2.IdImagem : null);
					return val2;
				}).ToList()
			} : new InfoTierVariation
			{
				variation_id = 0L,
				variation_name = (tier2 ? "Linhas" : colunas.First()),
				variation_option_list = ((IEnumerable<string>)linhas).Select((Func<string, VariationOption>)delegate(string x)
				{
					//IL_000d: Unknown result type (might be due to invalid IL or missing references)
					//IL_0012: Unknown result type (might be due to invalid IL or missing references)
					//IL_001a: Unknown result type (might be due to invalid IL or missing references)
					//IL_0026: Unknown result type (might be due to invalid IL or missing references)
					//IL_0055: Expected O, but got Unknown
					VariationOption val = new VariationOption
					{
						variation_option_id = 0L,
						variation_option_name = x
					};
					ModelDto obj = dto.Variacoes.FirstOrDefault((ModelDto v) => v.Linha == x);
					val.image_id = ((obj != null) ? obj.IdImagem : null);
					return val;
				}).ToList()
			} };
			if (tier2)
			{
				list.Add(new InfoTierVariation
				{
					variation_id = 0L,
					variation_name = "Colunas",
					variation_option_list = ((IEnumerable<string>)colunas).Select((Func<string, VariationOption>)((string x) => new VariationOption
					{
						variation_option_id = 0L,
						variation_option_name = x
					})).ToList()
				});
			}
			Dimension dimension = Dimension.Criar().ComAltura(dto.Dimencoes.Altura).ComLargura(dto.Dimencoes.Largura)
				.ComComprimento(dto.Dimencoes.Comprimento);
			List<Model> list2 = dto.Variacoes.Select((Func<ModelDto, Model>)((ModelDto x) => new Model
			{
				model_id = 0L,
				model_name = (somenteColunas ? x.Coluna : (tier2 ? $"{x.Linha}, {x.Coluna}" : x.Linha)),
				promotion_id = 0L,
				original_price = dto.Preco,
				has_promotion = false,
				tier_index = (somenteColunas ? new List<int> { colunas.IndexOf(x.Coluna) } : (tier2 ? new List<int>
				{
					linhas.IndexOf(x.Linha),
					colunas.IndexOf(x.Coluna)
				} : new List<int> { linhas.IndexOf(x.Linha) })),
				price_info = new List<PriceInfo>
				{
					new PriceInfo
					{
						original_price = HelperDecimal.ArredondarParaDuasCasasDecimais(dto.Preco),
						current_price = HelperDecimal.ArredondarParaDuasCasasDecimais(x.Preco)
					}
				},
				model_sku = x.Sku,
				pre_order = new PreOrder
				{
					is_pre_order = dto.SobEncomenda,
					days_to_ship = dto.DiasParaEnvio
				},
				seller_stock = new List<SellerStock>
				{
					new SellerStock
					{
						location_id = "BRZ",
						stock = x.QuantidadeDisponivel
					}
				},
				gtin_code = "00",
				model_status = "MODEL_NORMAL",
				weight = ObjectToDoubleConverter.TryConvertToDouble((object)dto.Dimencoes.Peso),
				dimension = dimension,
				is_fulfillment_by_shopee = false
			})).ToList();
			return CriarVariacoes.Criar().ComItemId(dto.IdShopee).ComTierVariacao((IEnumerable<InfoTierVariation>)list)
				.ComModel((IEnumerable<Model>)list2);
		}

		public List<AttributeList> MontarListaAtributos(AnuncioDto dto)
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Expected O, but got Unknown
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Expected O, but got Unknown
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Expected O, but got Unknown
			List<AttributeList> list = new List<AttributeList>();
			foreach (AtributoDto atributo in dto.Atributos)
			{
				List<AttributeValue> list2 = new List<AttributeValue>();
				AtributoTextoDto val = (AtributoTextoDto)(object)((atributo is AtributoTextoDto) ? atributo : null);
				AtributoListaDto val2 = (AtributoListaDto)(object)((atributo is AtributoListaDto) ? atributo : null);
				AtributoListaSelecionavelDto val3 = (AtributoListaSelecionavelDto)(object)((atributo is AtributoListaSelecionavelDto) ? atributo : null);
				if (!string.IsNullOrWhiteSpace((val != null) ? val.Valor : null))
				{
					list2.Add(new AttributeValue
					{
						value_id = ((!HelperString.IsNullOrEmpty(((AtributoDto)val).ValueId)) ? HelperString.ToInt(((AtributoDto)val).ValueId) : 0),
						original_value_name = val.Valor,
						value_unit = val.Unidade
					});
				}
				else if (val2 != null && HelperObject.IsNotNull((object)val2.OpcaoSelecionada) && !string.IsNullOrWhiteSpace(val2.OpcaoSelecionada.Valor))
				{
					list2.Add(new AttributeValue
					{
						value_id = HelperString.ToInt(val2.OpcaoSelecionada.Id),
						original_value_name = val2.OpcaoSelecionada.Valor,
						value_unit = val2.OpcaoSelecionada.Unidade
					});
				}
				else if (((val3 != null) ? val3.OpcoesSelecionadas : null) != null && val3.OpcoesSelecionadas.Any())
				{
					foreach (AtributoOpcaoDto item2 in val3.OpcoesSelecionadas.Where((AtributoOpcaoDto p) => !string.IsNullOrWhiteSpace(p.Valor)))
					{
						list2.Add(new AttributeValue
						{
							value_id = HelperString.ToInt(item2.Id),
							original_value_name = item2.Valor,
							value_unit = item2.Unidade
						});
					}
				}
				if (list2.Any())
				{
					AttributeList item = new AttributeList
					{
						attribute_id = HelperString.ToInt(atributo.Id),
						attribute_value_list = list2,
						original_attribute_name = atributo.Titulo
					};
					list.Add(item);
				}
			}
			return list;
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

		private async Task AtualizarAnuncio(AnuncioDto dto)
		{
			AnuncioDto dtoAtualizado = default(AnuncioDto);
			_ = dtoAtualizado;
			dtoAtualizado = await _service.Canal<IAnuncioShopeeModuleService, Task<AnuncioDto>>((Func<IAnuncioShopeeModuleService, Task<AnuncioDto>>)((IAnuncioShopeeModuleService service) => ((IAtualizar<AnuncioDto>)(object)service).AtualizarAsync(dto)));
			ThreadPrincipal.Executar((Action)delegate
			{
				((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Atualizar(dtoAtualizado);
				((UpdateViewBindableBase)this).ProcessMessages();
				((UpdateViewBindableBase)this).RecuperarFoco();
			});
		}

		private void NavegarShopeeModulo()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0041: Unknown result type (might be due to invalid IL or missing references)
				//IL_0064: Expected O, but got Unknown
				DialogoFactory.MostrarDialogoConfirma("Você não está autenticado na Shopee. O módulo Shopee será aberto para você entrar.", false, HorizontalAlignment.Center);
				IRegion region = ((NavigationBase)this).NavigationService.Region;
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add("ID", (object)Guid.NewGuid().ToString());
				NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)region, "ShopeeView", obj, val);
			});
		}

		private async Task ExecutarComTratamento(Func<Task> acao, string mensagemLoader)
		{
			try
			{
				LoaderProcessoService.MostrarLoader(mensagemLoader);
				await acao();
				DialogoFactory.MostrarDialogoConfirma("Concluído!", false, HorizontalAlignment.Center);
			}
			catch (AuthenticationException)
			{
				NavegarShopeeModulo();
			}
			catch (Exception ex2)
			{
				HelperException.ShowLastException(ex2, "", false, false);
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private async Task<bool> ValidarAutenticacaoShopee()
		{
			IShopeeApiService shopeeApiService = DadosSistema.GetShopeeApiService();
			if (shopeeApiService.PossuiToken() && shopeeApiService.IsTokenValido())
			{
				return true;
			}
			bool autenticado;
			try
			{
				LoaderProcessoService.MostrarLoader("Validando autenticação com a Shopee...");
				autenticado = await shopeeApiService.ValidarAutenticacaoAsync();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				return false;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
			if (!autenticado)
			{
				NavegarShopeeModulo();
			}
			return autenticado;
		}

		protected override async void Adicionar()
		{
			if (await ValidarAutenticacaoShopee())
			{
				((ListaManutencaoBase<AnuncioDto, AnuncioShopeeViewModel, AnuncioShopee>)(object)this).Adicionar();
			}
		}

		protected override async Task Editar()
		{
			if (await ValidarAutenticacaoShopee())
			{
				await ((ListaManutencaoBase<AnuncioDto, AnuncioShopeeViewModel, AnuncioShopee>)(object)this).Editar();
			}
		}

		public override bool PodeEditar()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Invalid comparison between Unknown and I4
			if (((ListaManutencaoBase<AnuncioDto, AnuncioShopeeViewModel, AnuncioShopee>)(object)this).PodeEditar() && (int)((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Status == 2)
			{
				return !((ViewModelDtoBase<AnuncioDto>)(object)((ListaBase<AnuncioDto, AnuncioShopeeViewModel>)(object)this).Itens.Selecionado).Dto.Deletado;
			}
			return false;
		}
	}
	public class AnuncioShopeeViewModel : ViewModelAtivoBase<AnuncioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public ISelecaoCommandParameter ProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto dto)
		{
			EstoqueDto estoque = dto.ProdutoEmpresa.Estoque;
			EstoqueGradeDto val = (EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null);
			if (val != null && val.Grades.Count() > 50)
			{
				throw new Exception("Shopee permite no máximo 50 variações por produto.\nSelecione outro produto.");
			}
			Estoque = dto.ProdutoEmpresa.Estoque;
			Preco = dto.Preco.PrecoVendaVigente;
			OnAlterouProdutoEvent(dto.ProdutoEmpresa.Estoque);
			IsGrade = HelperObject.Is<EstoqueGradeDto>((object)dto.ProdutoEmpresa.Estoque);
			ProdutoServicoEmpresaCodigoInterno = dto.ProdutoServico.CodigoInterno;
			ProdutoServicoEmpresaCodigoBarras = dto.ProdutoServico.Produto.CodigoBarras;
			ProdutoServicoEmpresaPrecoReferencia = ((EntityBaseDto)dto.Preco).Id;
			ProdutoServicoEmpresaReferencia = ((EntityBaseDto)dto).Id;
			ProdutoServicoEmpresaDescricao = dto.ProdutoServico.Descricao;
			KitReferencia = string.Empty;
			if (string.IsNullOrEmpty(Titulo))
			{
				Titulo = $"{dto.ProdutoServico.Produto.Marca.Descricao} {dto.ProdutoServico.Descricao} {dto.ProdutoServico.Caracteristica}".Trim();
			}
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
		}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa)), "Selecione um cadastro do tipo produto.", HorizontalAlignment.Center));

		public ISelecaoCommandParameter KitCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<KitDto>("KitListaView", (Action<KitDto>)delegate(KitDto dto)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			OnAlterouProdutoEvent((EstoqueDto)new EstoqueDtoNull());
			ProdutoServicoEmpresaCodigoInterno = string.Empty;
			ProdutoServicoEmpresaPrecoReferencia = string.Empty;
			ProdutoServicoEmpresaReferencia = string.Empty;
			ProdutoServicoEmpresaDescricao = dto.Descricao;
			KitReferencia = ((EntityBaseDto)dto).Id;
			Titulo = dto.Descricao.Trim();
			Preco = dto.Itens.Sum((ItemKitDto x) => x.Quantidade * x.PrecoVenda);
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
			DialogoFactory.MostrarDialogoConfirma("Ao faturar o pedido o sistema sempre usará os dados atuais do Kit, por tanto alterar informações depois de enviar o anúncio podem causar divergências.", false, HorizontalAlignment.Center);
		}, true);

		public EstoqueDto Estoque { get; set; }

		public string ProdutoServicoEmpresaPrecoReferencia { get; set; }

		public string ProdutoServicoEmpresaReferencia { get; set; }

		public string ProdutoServicoEmpresaCodigoInterno { get; set; }

		public string ProdutoServicoEmpresaCodigoBarras
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaCodigoBarras));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaCodigoBarras), value);
			}
		}

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

		public bool IsAtivo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ((ViewModelAtivoBase<AnuncioDto>)this).Ativo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ((ViewModelAtivoBase<AnuncioDto>)this).Ativo), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
				});
			}
		}

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
				if (IsAtivo)
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
				if (((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value))
				{
					OnAlterouPrecoEvent(value);
				}
			}
		}

		public int Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Quantidade), value);
			}
		}

		public string Condicao { get; set; }

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
					((BindableBase)this).RaisePropertiesChanged<bool, bool>((Expression<Func<bool>>)(() => PodeAlterarPreco), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AnuncioShopeeViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
				});
				OnAlterouUtilizaInformacaoCadatroEvent(value);
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

		public StatusAnuncioShopee Status
		{
			get
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				return ((BindableBase)this).GetProperty<StatusAnuncioShopee>((Expression<Func<StatusAnuncioShopee>>)(() => Status));
			}
			set
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				((BindableBase)this).SetProperty<StatusAnuncioShopee>((Expression<Func<StatusAnuncioShopee>>)(() => Status), value);
			}
		}

		public long IdShopee { get; set; }

		public bool IsTransient { get; set; }

		public bool IsReadOndy { get; set; }

		public bool Deletado { get; set; }

		public string Permalink { get; set; }

		public override bool Valid
		{
			get
			{
				string titulo = Titulo;
				if (titulo != null && titulo.Length >= 25)
				{
					string descricao = Descricao;
					if (descricao == null)
					{
						return false;
					}
					return descricao.Length >= 10;
				}
				return false;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				if (((Validation)this).Valid)
				{
					return new string[0];
				}
				List<string> list = new List<string>();
				string titulo = Titulo;
				if (titulo != null && titulo.Length < 25)
				{
					list.Add("Título deve ter entre 25 e 100 caracteres.");
				}
				string descricao = Descricao;
				if (descricao != null && descricao.Length < 10)
				{
					list.Add("A descrição deve ter no mínimo 10 caracteres.");
				}
				return list.ToArray();
			}
		}

		public event EventHandler<decimal> AlterouPrecoEvent;

		public event EventHandler<EstoqueDto> AlterouProdutoEvent;

		public event EventHandler<bool> AlterouUtilizaInformacaoCadatroEvent;

		public AnuncioShopeeViewModel()
		{
		}

		public AnuncioShopeeViewModel(AnuncioDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsListaViewModel()
		{
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			ProdutoServicoEmpresaDto val = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
			base.Ativo = ((EntityDto)((ViewModelDtoBase<AnuncioDto>)(object)this).Dto).Ativo;
			Titulo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Titulo;
			Condicao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Condicao;
			Preco = (((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.UtilizarInformacaoCadastroSincronismo ? val.Preco.PrecoVendaVigente : ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Preco);
			IdShopee = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.IdShopee;
			Status = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Status;
			UtilizarInformacaoCadastroSincronismo = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.UtilizarInformacaoCadastroSincronismo;
		}

		public override void ProjectedAsViewModel()
		{
			((ViewModelBase<AnuncioDto>)(object)this).ProjectedAsListaViewModel();
			IsReadOndy = !((EntityBaseDto)((ViewModelDtoBase<AnuncioDto>)(object)this).Dto).IsTransient();
			IsGrade = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.PossuiGrade;
			Quantidade = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.QuantidadeDisponivel;
			Condicao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Condicao;
			Descricao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Descricao;
			KitReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.KitReferencia;
			ProdutoServicoEmpresaReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaReferencia;
			ProdutoServicoEmpresaPrecoReferencia = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaPrecoReferencia;
			ProdutoServicoEmpresaDescricao = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaDescricao;
			ProdutoServicoEmpresaCodigoInterno = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoCodigoInterno;
			ProdutoServicoEmpresaCodigoBarras = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.ProdutoServicoEmpresaCodigoBarras;
			Deletado = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Deletado;
			Permalink = ((ViewModelDtoBase<AnuncioDto>)(object)this).Dto.Permalink;
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeUtilizarInformacaoCadastroSincronismo));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarQuantidade));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			object obj = dto;
			AnuncioDto val = (AnuncioDto)((obj is AnuncioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.PossuiGrade = IsGrade;
				val.IdShopee = IdShopee;
				val.Titulo = Titulo;
				val.Preco = Preco;
				val.QuantidadeDisponivel = Quantidade;
				val.Condicao = Condicao;
				val.Descricao = Descricao;
				val.Status = Status;
				val.KitReferencia = KitReferencia;
				val.ProdutoServicoEmpresaReferencia = ProdutoServicoEmpresaReferencia;
				val.ProdutoServicoEmpresaPrecoReferencia = ProdutoServicoEmpresaPrecoReferencia;
				val.ProdutoServicoEmpresaDescricao = ProdutoServicoEmpresaDescricao;
				val.ProdutoServicoEmpresaCodigoBarras = ProdutoServicoEmpresaCodigoBarras;
				val.Deletado = Deletado;
				val.Permalink = Permalink;
				val.ProdutoServicoCodigoInterno = ProdutoServicoEmpresaCodigoInterno;
				val.UtilizarInformacaoCadastroSincronismo = UtilizarInformacaoCadastroSincronismo;
				((ViewModelDtoBase<AnuncioDto>)(object)this).AtualizarDto<AnuncioDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AnuncioShopeeFormulario(((ViewModelDtoBase<AnuncioDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Anúncios da Shopee";
		}

		public string ToTituloFicha()
		{
			return "Anúncio da Shopee";
		}

		protected virtual void OnAlterouPrecoEvent(decimal e)
		{
			this.AlterouPrecoEvent?.Invoke(this, e);
		}

		protected virtual void OnAlterouProdutoEvent(EstoqueDto e)
		{
			this.AlterouProdutoEvent?.Invoke(this, e);
		}

		protected virtual void OnAlterouUtilizaInformacaoCadatroEvent(bool e)
		{
			this.AlterouUtilizaInformacaoCadatroEvent?.Invoke(this, e);
		}
	}
	public class AnunciosShopeeViewModel : ModuloBase
	{
		public AnunciosShopeeViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAnuncioShopeeRegion, "AnuncioShopeeListaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.AnunciosShopee.ViewModels.Variacoes
{
	public class ImagensVariacoesViewModel : BindableBase
	{
		public string Coluna { get; set; } = string.Empty;


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

		public string IdImagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => IdImagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => IdImagem), value);
				this.ImagemAlterada?.Invoke(this, EventArgs.Empty);
			}
		}

		public ICommand SelecionarImagemCommand { get; }

		public ICommand RemoverImagemCommand { get; }

		public event EventHandler ImagemAlterada;

		public ImagensVariacoesViewModel()
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			SelecionarImagemCommand = (ICommand)new DelegateCommand((Action)ExecutarSelecionarImagem);
			RemoverImagemCommand = (ICommand)new DelegateCommand((Action)ExecutarRemoverImagem);
		}

		private async void ExecutarSelecionarImagem()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = false,
				Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;",
				Title = "Selecione uma imagem"
			};
			if (openFileDialog.ShowDialog() == true)
			{
				LoaderProcessoService.MostrarLoader("Enviando imagens...");
				try
				{
					string text2 = (UrlImagem = await DadosSistema.GetShopeeApiService().CarregarImagemAsync(openFileDialog.FileName));
					IdImagem = text2.Replace("https://cf.shopee.com.br/file/", "");
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
				}
			}
		}

		private void ExecutarRemoverImagem()
		{
			UrlImagem = "";
			IdImagem = "";
		}
	}
	public class VariacoesViewModel : BindableBase
	{
		public string Id { get; set; } = string.Empty;


		public string Nome { get; set; } = string.Empty;


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

		public IEnumerable<int> Variacao { get; set; } = Enumerable.Empty<int>();


		public string UrlImagem { get; set; } = string.Empty;


		public string IdImagem { get; set; } = string.Empty;


		[NaoPermitirValorMenorIgualZero(ErrorMessage = "Preço não pode ser igual a zero.")]
		public decimal Preco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Preco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value);
				this.PrecoAlterado?.Invoke(this, EventArgs.Empty);
			}
		}

		public int QuantidadeDisponivel { get; set; }

		public string Sku { get; set; } = string.Empty;


		public string CodigoBarras { get; set; } = string.Empty;


		public long ModelIdShopee
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => ModelIdShopee));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => ModelIdShopee), value);
			}
		}

		public event EventHandler PrecoAlterado;
	}
}
namespace Otus.Modulo.AnunciosShopee.ViewModels.Atributos
{
	public class AtributoFactory
	{
		private IEnumerable<AtributoOpcaoDto> _opcoes = Enumerable.Empty<AtributoOpcaoDto>();

		private TiposAtributoShopee _tipo;

		private string _titulo;

		private string _valorTexto;

		private string _valueId;

		private string _unidade;

		private AtributoOpcaoDto _valorOpcao;

		private IEnumerable<AtributoOpcaoDto> _valoresSelecionados = Enumerable.Empty<AtributoOpcaoDto>();

		private int _maximoSelecionados;

		public static AtributoFactory Init()
		{
			return new AtributoFactory();
		}

		public AtributoFactory SetarTipo(TiposAtributoShopee tipo)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
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

		public AtributoFactory SetarValueId(string valueId)
		{
			_valueId = valueId;
			return this;
		}

		public AtributoFactory SetarUnidade(string unidade)
		{
			_unidade = unidade;
			return this;
		}

		public AtributoFactory SetarValoresSelecionados(IEnumerable<AtributoOpcaoDto> valores)
		{
			_valoresSelecionados = valores;
			return this;
		}

		public AtributoFactory SetarMaximoSelecionados(int? maximo)
		{
			_maximoSelecionados = maximo ?? 0;
			return this;
		}

		public AtributoViewModel Criar()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Invalid comparison between Unknown and I4
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Invalid comparison between Unknown and I4
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Invalid comparison between Unknown and I4
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Invalid comparison between Unknown and I4
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			if (((int)_tipo == 1 || (int)_tipo == 2) && _opcoes.Count() >= 1)
			{
				return new AtributoListaViewModel(_valueId, _titulo, _opcoes, _valorOpcao);
			}
			if ((int)_tipo == 3)
			{
				return new AtributoTextoViewModel(_valueId, _unidade, _titulo, _valorTexto);
			}
			if (((int)_tipo == 4 || (int)_tipo == 5) && _opcoes.Count() >= 1)
			{
				return new AtributoListaSelecionavelViewModel(_valueId, _titulo, _opcoes, _valoresSelecionados, _maximoSelecionados);
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
			//IL_0044: Expected O, but got Unknown
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			return (AtributoDto)new AtributoListaDto
			{
				Id = base.ValueId,
				Titulo = base.Titulo,
				Opcoes = Opcoes.Itens,
				OpcaoSelecionada = (AtributoOpcaoDto)(((object)Selecionado) ?? ((object)new AtributoOpcaoDto()))
			};
		}
	}
	public class AtributoListaSelecionavelViewModel : AtributoViewModel
	{
		public IEnumerable<AtributoOpcaoSelecionavelViewModel> Opcoes { get; }

		public int MaximoSelecionados { get; }

		public string Cabecalho
		{
			get
			{
				if (MaximoSelecionados <= 0)
				{
					return base.Titulo;
				}
				return $"{base.Titulo} (até {MaximoSelecionados})";
			}
		}

		public bool ExcedeLimite
		{
			get
			{
				if (MaximoSelecionados > 0)
				{
					return Opcoes.Count((AtributoOpcaoSelecionavelViewModel o) => o.Selecionado) > MaximoSelecionados;
				}
				return false;
			}
		}

		public string Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Valor)) ?? string.Empty;
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Valor), value);
			}
		}

		public event EventHandler SelecaoAlterada;

		public AtributoListaSelecionavelViewModel(string valueId, string titulo, IEnumerable<AtributoOpcaoDto> opcoes, IEnumerable<AtributoOpcaoDto> selecionados, int maximoSelecionados)
			: base(valueId, titulo)
		{
			if (HelperObject.IsNull((object)selecionados))
			{
				selecionados = Enumerable.Empty<AtributoOpcaoDto>();
			}
			MaximoSelecionados = maximoSelecionados;
			Opcoes = opcoes.Select((Func<AtributoOpcaoDto, AtributoOpcaoSelecionavelViewModel>)((AtributoOpcaoDto o) => new AtributoOpcaoSelecionavelViewModel(o.Id, o.Valor, selecionados.Any((AtributoOpcaoDto x) => x.Id == o.Id)))).ToList();
			foreach (AtributoOpcaoSelecionavelViewModel opco in Opcoes)
			{
				opco.PropertyChanged += OpcaoOnPropertyChanged;
			}
			AtualizarHabilitados();
		}

		public IEnumerable<string> ObterSelecionados()
		{
			return from o in Opcoes
				where o.Selecionado
				select o.Id;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Expected O, but got Unknown
			return (AtributoDto)new AtributoListaSelecionavelDto
			{
				Id = base.ValueId,
				Titulo = base.Titulo,
				Opcoes = Opcoes.Select((Func<AtributoOpcaoSelecionavelViewModel, AtributoOpcaoDto>)((AtributoOpcaoSelecionavelViewModel o) => new AtributoOpcaoDto
				{
					Id = o.Id,
					Valor = o.Valor
				})),
				OpcoesSelecionadas = Opcoes.Where((AtributoOpcaoSelecionavelViewModel o) => o.Selecionado).Select((Func<AtributoOpcaoSelecionavelViewModel, AtributoOpcaoDto>)((AtributoOpcaoSelecionavelViewModel o) => new AtributoOpcaoDto
				{
					Id = o.Id,
					Valor = o.Valor
				})).ToList()
			};
		}

		private void OpcaoOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!(e.PropertyName != "Selecionado"))
			{
				AtualizarHabilitados();
				this.SelecaoAlterada?.Invoke(this, EventArgs.Empty);
			}
		}

		private void AtualizarHabilitados()
		{
			bool flag = MaximoSelecionados > 0 && Opcoes.Count((AtributoOpcaoSelecionavelViewModel o) => o.Selecionado) >= MaximoSelecionados;
			foreach (AtributoOpcaoSelecionavelViewModel opco in Opcoes)
			{
				opco.Habilitado = opco.Selecionado || !flag;
			}
		}
	}
	public class AtributoTextoViewModel : AtributoViewModel
	{
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

		public string Unidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Unidade));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Unidade), value);
			}
		}

		public AtributoTextoViewModel(string valueId, string unidade, string titulo, string valor)
			: base(valueId, titulo)
		{
			Valor = valor;
			Unidade = unidade;
		}

		public override AtributoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			return (AtributoDto)new AtributoTextoDto
			{
				Id = base.ValueId,
				Valor = Valor,
				Titulo = base.Titulo,
				Unidade = Unidade
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
namespace Otus.Modulo.AnunciosShopee.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.AnunciosShopee.Properties.Resources", typeof(Resources).Assembly);
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

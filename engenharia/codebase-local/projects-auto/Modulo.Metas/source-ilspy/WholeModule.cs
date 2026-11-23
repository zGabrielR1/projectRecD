using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Relatorios;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.OrigensPesquisaMovimentacao;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesMovimentacao;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Style.GridControls;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.Metas.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Metas")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Metas")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("3b77d8d9-956f-4595-916e-a2237562e84f")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Metas
{
	[Module(ModuleName = "MetaModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	public class MetaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public MetaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<MetasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<MetaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Metas.Views
{
	public class MetaFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MetaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Metas;component/views/metafichaview.xaml", UriKind.Relative);
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
	public class MetasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MetasView()
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
				Uri resourceLocator = new Uri("/Modulo.Metas;component/views/metasview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Metas.Views.MetaFichaViewDetalhes
{
	public class MetaPorVendedorView : UserControl, IComponentConnector
	{
		internal GridControlBestFit MetaGridControl;

		private bool _contentLoaded;

		public MetaPorVendedorView()
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
				Uri resourceLocator = new Uri("/Modulo.Metas;component/views/metafichaviewdetalhes/metaporvendedorview.xaml", UriKind.Relative);
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
				MetaGridControl = (GridControlBestFit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class MetaGeralView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MetaGeralView()
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
				Uri resourceLocator = new Uri("/Modulo.Metas;component/views/metafichaviewdetalhes/metageralview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Metas.ViewModels
{
	public class MetaPorVendedorAbaViewModel : AbaAuxiliarViewModelBase
	{
		public MetaPorVendedorAbaViewModel(IViewModelDtoBase viewModel)
			: base("META POR VENDEDOR", "Valores alcançados", viewModel)
		{
		}
	}
	public class MetaGeralViewModel : ViewModelDtoBase<IEnumerable<PessoaDto>>
	{
		private readonly Action<DateTime, int, int, BaseCalculoComissao> _pesquisar;

		public DateTime MesAnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => MesAnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => MesAnoBase), value);
			}
		}

		public Lista<CodigoDescricaoDto> Origens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Origens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Origens), value);
			}
		}

		public Lista<CodigoDescricaoDto> Situacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Situacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Situacoes), value);
			}
		}

		public MetaGeralRelatorioDto MetaGeral
		{
			get
			{
				return ((BindableBase)this).GetProperty<MetaGeralRelatorioDto>((Expression<Func<MetaGeralRelatorioDto>>)(() => MetaGeral));
			}
			set
			{
				((BindableBase)this).SetProperty<MetaGeralRelatorioDto>((Expression<Func<MetaGeralRelatorioDto>>)(() => MetaGeral), value);
			}
		}

		public Lista<KeyValuePair<string, BaseCalculoComissao>> TipoBaseCalculo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, BaseCalculoComissao>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoComissao>>>>)(() => TipoBaseCalculo));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, BaseCalculoComissao>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoComissao>>>>)(() => TipoBaseCalculo), value);
			}
		}

		public string TagGrafico => "TagGrafico";

		public string TituloGrafico => "META GERAL";

		public bool ImprimirGraficoVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGraficoVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGraficoVisivel), value);
			}
		}

		public ICommand ImprimirGraficoCommand { get; set; }

		public ICommand ImprimirGridCommand { get; set; }

		public ICommand PesquisarMetasCommand { get; set; }

		public MetaGeralViewModel()
		{
		}

		public MetaGeralViewModel(Action<DateTime, int, int, BaseCalculoComissao> pesquisar)
			: base((IEnumerable<PessoaDto>)null)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			_pesquisar = pesquisar;
			PesquisarMetasCommand = (ICommand)new DelegateCommand((Action)PesquisarMetasVendedor);
			ImprimirGraficoCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirGrafico);
		}

		private void PesquisarMetasVendedor()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				//IL_003a: Unknown result type (might be due to invalid IL or missing references)
				try
				{
					_pesquisar(MesAnoBase, Origens.ItemSelecionado.Codigo, Situacoes.ItemSelecionado.Codigo, TipoBaseCalculo.ItemSelecionado.Value);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					Exception e = ex2;
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(e, "", false, false);
					});
				}
				finally
				{
					ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				}
			}).Start();
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			MesAnoBase = DateTime.Now;
			IEnumerable<OrigemPesquisaMovimentacao> source = OrigensPesquisaMovimentacao.ObterPorSistema(OrigensPesquisaMovimentacao.Obter<IOrigemSaida>(), DadosSistema.Sistema.Tipo, DadosSistema.HabilitadoMEI);
			Origens = new Lista<CodigoDescricaoDto>();
			Origens.Adicionar(source.Select((Func<OrigemPesquisaMovimentacao, CodigoDescricaoDto>)((OrigemPesquisaMovimentacao item) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)item).Codigo,
				Descricao = ((CodigoDescricao)item).Descricao
			})));
			Origens.Adicionar(new CodigoDescricaoDto
			{
				Codigo = 101,
				Descricao = "Todos os documentos fiscais de saída"
			});
			Origens.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo.Equals(101)));
			IReadOnlyCollection<SituacaoMovimentacao> dados = SituacoesMovimentacao.Dados;
			Situacoes = new Lista<CodigoDescricaoDto>();
			Situacoes.Adicionar(((IEnumerable<SituacaoMovimentacao>)dados).Select((Func<SituacaoMovimentacao, CodigoDescricaoDto>)((SituacaoMovimentacao item) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)item).Codigo,
				Descricao = ((CodigoDescricao)item).Descricao
			})));
			Situacoes.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo.Equals(2)));
			TipoBaseCalculo = new Lista<KeyValuePair<string, BaseCalculoComissao>>();
			TipoBaseCalculo.Adicionar((IEnumerable<KeyValuePair<string, BaseCalculoComissao>>)HelperEnum.ObterParDeValores<BaseCalculoComissao>());
			TipoBaseCalculo.Posicionar((Expression<Func<KeyValuePair<string, BaseCalculoComissao>, bool>>)((KeyValuePair<string, BaseCalculoComissao> x) => (int)x.Value == 1));
		}

		public void SetarMetas(MetaGeralRelatorioDto metaGeral)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Expected O, but got Unknown
				MetaGeral = new MetaGeralRelatorioDto();
				MetaGeral = metaGeral;
				ImprimirGraficoVisivel = metaGeral.Itens?.Any() ?? false;
			});
		}

		private void ImprimirGrafico(object obj)
		{
			DadosSistema.GetImpressaoGraficoService().ImprimirGrafico(obj, TagGrafico, TituloGrafico);
		}
	}
	public class MetaPorVendedorViewModel : ViewModelDtoBase<IEnumerable<PessoaDto>>, IViewModelImprimirGrid, IViewModelImprimir
	{
		private readonly Action<DateTime, int, int, BaseCalculoComissao> _pesquisar;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public DateTime MesAnoBase
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => MesAnoBase));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => MesAnoBase), value);
			}
		}

		public Lista<CodigoDescricaoDto> Origens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Origens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Origens), value);
			}
		}

		public Lista<CodigoDescricaoDto> Situacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Situacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Situacoes), value);
			}
		}

		public Lista<ItensMovimentadosPorVendedorMetaDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItensMovimentadosPorVendedorMetaDto>>((Expression<Func<Lista<ItensMovimentadosPorVendedorMetaDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItensMovimentadosPorVendedorMetaDto>>((Expression<Func<Lista<ItensMovimentadosPorVendedorMetaDto>>>)(() => Itens), value);
			}
		}

		public Lista<KeyValuePair<string, BaseCalculoComissao>> TipoBaseCalculo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, BaseCalculoComissao>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoComissao>>>>)(() => TipoBaseCalculo));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, BaseCalculoComissao>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoComissao>>>>)(() => TipoBaseCalculo), value);
			}
		}

		public string Titulo => "Metas por vendedor";

		public string SubTitulo => string.Empty;

		public string TagProperty => "MetaGridControl";

		public string TituloRelatorio => "Metas por vendedor";

		public string SubTituloRelatorio => $"{HelperDateTime.ObterDescricaoMes(MesAnoBase.Month)} de {MesAnoBase.Year}";

		public bool Retrato => true;

		public bool ImprimirGridVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGridVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGridVisivel), value);
			}
		}

		public string TagGrafico => "TagGrafico";

		public string TituloGrafico => "";

		public bool ImprimirGraficoVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGraficoVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ImprimirGraficoVisivel), value);
			}
		}

		public ICommand ImprimirGraficoCommand { get; set; }

		public ICommand ImprimirGridCommand { get; set; }

		public ICommand PesquisarMetasCommand { get; set; }

		public MetaPorVendedorViewModel()
		{
		}

		public MetaPorVendedorViewModel(Action<DateTime, int, int, BaseCalculoComissao> pesquisar, IImpressaoRelatorioService impressaoRelatorioService)
			: base((IEnumerable<PessoaDto>)null)
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			_pesquisar = pesquisar;
			_impressaoRelatorioService = impressaoRelatorioService;
			Itens = new Lista<ItensMovimentadosPorVendedorMetaDto>();
			PesquisarMetasCommand = (ICommand)new DelegateCommand((Action)PesquisarMetasVendedor);
			ImprimirGraficoCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirGrafico);
			ImprimirGridCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirGrid);
		}

		private void PesquisarMetasVendedor()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				//IL_003a: Unknown result type (might be due to invalid IL or missing references)
				try
				{
					_pesquisar(MesAnoBase, Origens.ItemSelecionado.Codigo, Situacoes.ItemSelecionado.Codigo, TipoBaseCalculo.ItemSelecionado.Value);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					Exception e = ex2;
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(e, "", false, false);
					});
				}
				finally
				{
					ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				}
			}).Start();
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			MesAnoBase = DateTime.Now;
			IEnumerable<OrigemPesquisaMovimentacao> source = OrigensPesquisaMovimentacao.ObterPorSistema(OrigensPesquisaMovimentacao.Obter<IOrigemSaida>(), DadosSistema.Sistema.Tipo, DadosSistema.HabilitadoMEI);
			Origens = new Lista<CodigoDescricaoDto>();
			Origens.Adicionar(source.Select((Func<OrigemPesquisaMovimentacao, CodigoDescricaoDto>)((OrigemPesquisaMovimentacao item) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)item).Codigo,
				Descricao = ((CodigoDescricao)item).Descricao
			})));
			Origens.Adicionar(new CodigoDescricaoDto
			{
				Codigo = 101,
				Descricao = "Todos os documentos fiscais de saída"
			});
			Origens.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo.Equals(101)));
			IReadOnlyCollection<SituacaoMovimentacao> dados = SituacoesMovimentacao.Dados;
			Situacoes = new Lista<CodigoDescricaoDto>();
			Situacoes.Adicionar(((IEnumerable<SituacaoMovimentacao>)dados).Select((Func<SituacaoMovimentacao, CodigoDescricaoDto>)((SituacaoMovimentacao item) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)item).Codigo,
				Descricao = ((CodigoDescricao)item).Descricao
			})));
			Situacoes.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo.Equals(2)));
			TipoBaseCalculo = new Lista<KeyValuePair<string, BaseCalculoComissao>>();
			TipoBaseCalculo.Adicionar((IEnumerable<KeyValuePair<string, BaseCalculoComissao>>)HelperEnum.ObterParDeValores<BaseCalculoComissao>());
			TipoBaseCalculo.Posicionar((Expression<Func<KeyValuePair<string, BaseCalculoComissao>, bool>>)((KeyValuePair<string, BaseCalculoComissao> x) => (int)x.Value == 1));
		}

		public void SetarMetas(IEnumerable<ItensMovimentadosPorVendedorMetaDto> movimentacoes)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Itens.Limpar();
				Itens.Adicionar(movimentacoes);
				ImprimirGridVisivel = Itens.Itens.Any();
				ImprimirGraficoVisivel = Itens.Itens.Any();
			});
		}

		public void ImprimirGrid(object obj)
		{
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		private void ImprimirGrafico(object obj)
		{
			DadosSistema.GetImpressaoGraficoService().ImprimirGrafico(obj, TagGrafico, TituloGrafico);
		}
	}
	public class MetaGeralAbaViewModel : AbaAuxiliarViewModelBase
	{
		public MetaGeralAbaViewModel(IViewModelDtoBase viewModel)
			: base("Meta geral", "Total alcançado", viewModel)
		{
		}
	}
	public class MetaFichaViewModel : FichaBase<PessoaDto, MetaPorVendedorViewModel>
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ICommand ImprimirCommand { get; set; }

		public MetaFichaViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base("Metas")
		{
			_impressaoRelatorioService = impressaoRelatorioService;
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)Imprimir, (Func<object, bool>)PodeImprimir, (bool?)null);
		}

		private bool PodeImprimir(object arg)
		{
			IFormulario formulario = base.Formulario;
			if (((formulario != null) ? HelperObject.To<Formulario>((object)formulario).ViewModelAbaSelecionada : null) is IViewModelImprimir)
			{
				IFormulario formulario2 = base.Formulario;
				object obj;
				if (formulario2 == null)
				{
					obj = null;
				}
				else
				{
					Formulario obj2 = HelperObject.To<Formulario>((object)formulario2);
					obj = ((obj2 != null) ? obj2.ViewModelAbaSelecionada : null);
				}
				return !(obj is MetaPorVendedorViewModel);
			}
			return false;
		}

		private void Imprimir(object obj)
		{
			IViewModelBase viewModelAbaSelecionada = HelperObject.To<Formulario>((object)base.Formulario).ViewModelAbaSelecionada;
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)((viewModelAbaSelecionada is IViewModelItemImprimirRelatorio) ? viewModelAbaSelecionada : null));
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)((viewModelAbaSelecionada is IViewModelImprimirGrid) ? viewModelAbaSelecionada : null), obj as FrameworkElement, (string)null);
		}

		protected override void CriarFormulario()
		{
			base.Formulario = (IFormulario)(object)new MetaFormulario(_impressaoRelatorioService);
		}
	}
	public class MetaFormulario : Formulario
	{
		private readonly MetaPorVendedorViewModel _metaPorVendedorViewModel;

		private readonly MetaGeralViewModel _metaGeralViewModel;

		private readonly IWService _service;

		public MetaFormulario(IImpressaoRelatorioService impressaoRelatorioService)
		{
			_service = DadosSistema.GetService();
			_metaPorVendedorViewModel = new MetaPorVendedorViewModel(ObterMetaPorVendedor, impressaoRelatorioService);
			_metaGeralViewModel = new MetaGeralViewModel(ObterMetaGeral);
			CarregarDados();
		}

		private void CarregarDados()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					ObterMetaPorVendedor(DateTime.Now, 101, 2, (BaseCalculoComissao)1);
					ObterMetaGeral(DateTime.Now, 101, 2, (BaseCalculoComissao)1);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					Exception e = ex2;
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(e, "", false, false);
					});
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
					CriarAbas();
				}
			}).Start();
		}

		private void CriarAbas()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new MetaPorVendedorAbaViewModel((IViewModelDtoBase)(object)_metaPorVendedorViewModel) });
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new MetaGeralAbaViewModel((IViewModelDtoBase)(object)_metaGeralViewModel) });
			});
		}

		private void ObterMetaPorVendedor(DateTime mesAnoBase, int origem, int situacao, BaseCalculoComissao tipoBaseCalculo)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			IEnumerable<ItensMovimentadosPorVendedorMetaDto> result = _service.Canal<IMetaModuleService, Task<IEnumerable<ItensMovimentadosPorVendedorMetaDto>>>((Func<IMetaModuleService, Task<IEnumerable<ItensMovimentadosPorVendedorMetaDto>>>)((IMetaModuleService x) => x.ObterMetas(mesAnoBase, origem, situacao, tipoBaseCalculo))).Result;
			_metaPorVendedorViewModel.SetarMetas(result);
		}

		private void ObterMetaGeral(DateTime mesAnoBase, int origem, int situacao, BaseCalculoComissao tipoBaseCalculo)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			MetaGeralRelatorioDto result = _service.Canal<IMetaModuleService, Task<MetaGeralRelatorioDto>>((Func<IMetaModuleService, Task<MetaGeralRelatorioDto>>)((IMetaModuleService x) => x.ObterMetaGeral(mesAnoBase, origem, situacao, tipoBaseCalculo))).Result;
			_metaGeralViewModel.SetarMetas(result);
		}
	}
	public class MetasViewModel : ModuloBase
	{
		public MetasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloMetaRegion, "MetaFichaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}

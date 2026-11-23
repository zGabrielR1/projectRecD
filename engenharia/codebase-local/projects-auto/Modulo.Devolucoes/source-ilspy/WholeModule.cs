using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.EntradasModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using NClone;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Devolucoes.Interfaces;
using Otus.Modulo.Devolucoes.Reports;
using Otus.Modulo.Devolucoes.ViewModels.Tipos;
using Otus.Modulo.Devolucoes.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Devolucoes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Devolucoes")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Devolucoes
{
	[Module(ModuleName = "DevolucaoModulo", OnDemand = true)]
	[ModuleDependency("MovimentacaoModulo")]
	public class DevolucaoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public DevolucaoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<DevolucoesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DevolucaoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DevolucaoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DevolucaoFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Devolucoes.Views
{
	public class DevolucaoFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public DevolucaoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/devolucaofichaadicionarview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class DevolucaoFichaAtualizarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public DevolucaoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/devolucaofichaatualizarview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class DevolucaoListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DevolucaoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/devolucaolistaview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class DevolucoesView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DevolucoesView()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/devolucoesview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkCancelar : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkCancelar()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkcancelar%20.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkAjustarEstoqueMei : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkAjustarEstoqueMei()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkajustarestoquemei.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkAjustarEstoqueDevolucaoDav : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkAjustarEstoqueDevolucaoDav()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkajustarestoquedevolucaodav.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkGerarNotaCompraDevolucao : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkGerarNotaCompraDevolucao()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkgerarnotacompradevolucao.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkImprimir40Colunas : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkImprimir40Colunas()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkimprimir40colunas.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkImprimir : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkImprimir()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkimprimir.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkGerarCredito : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkGerarCredito()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkgerarcredito.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
	public class LinkGerarNotaDevolucao : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkGerarNotaDevolucao()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/linkgerarnotadevolucao.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
namespace Otus.Modulo.Devolucoes.Views.DevolucaoViewDetalhes
{
	public class DevolucaoView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DevolucaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Devolucoes;component/views/devolucaoviewdetalhes/devolucaoview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
namespace Otus.Modulo.Devolucoes.ViewModels
{
	public class DevolucaoViewModel : ViewModelAtivoBase<DevolucaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IDevolucaoViewModel
	{
		public TiposDevolucaoComboboxViewModel TiposDevolucao
		{
			get
			{
				return ((BindableBase)this).GetProperty<TiposDevolucaoComboboxViewModel>((Expression<Func<TiposDevolucaoComboboxViewModel>>)(() => TiposDevolucao));
			}
			set
			{
				((BindableBase)this).SetProperty<TiposDevolucaoComboboxViewModel>((Expression<Func<TiposDevolucaoComboboxViewModel>>)(() => TiposDevolucao), value);
			}
		}

		public bool PodeAlterarTipoDevolucao => ((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).IsTransient();

		public bool DavDevolvido => HelperObject.IsNot<HistoricoDavDevolvidoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.HistoricoDavDevolvido);

		public bool NFeDevolucaoGerada => HelperObject.IsNot<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.NotaDevolucao);

		public bool CreditoGerado => HelperObject.IsNot<CreditoDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Credito);

		public bool AjustouEstoqueMei => HelperObject.IsNot<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.AjusteEstoqueMei);

		public bool Cancelado => ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Cancelado;

		public override bool Valido
		{
			get
			{
				TiposDevolucaoComboboxViewModel tiposDevolucao = TiposDevolucao;
				bool? obj;
				if (tiposDevolucao == null)
				{
					obj = null;
				}
				else
				{
					IDevolucaoViewModel viewModel = tiposDevolucao.ViewModel;
					obj = ((viewModel != null) ? new bool?(((IViewModelBase)viewModel).Valido) : null);
				}
				return obj ?? true;
			}
		}

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

		public DevolucaoViewModel()
		{
		}

		public DevolucaoViewModel(DevolucaoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsListaViewModel()
		{
		}

		public override void ProjectedAsViewModel()
		{
			base.Ativo = ((EntityDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).Ativo;
			TiposDevolucao = new TiposDevolucaoComboboxViewModel(((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto);
			((Lista<TipoDevolucao>)TiposDevolucao).Posicionar((Expression<Func<TipoDevolucao, bool>>)((TipoDevolucao x) => x.Codigo.Equals(((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.TipoDevolucao)));
			((Lista<TipoDevolucao>)TiposDevolucao).AlterouItemSelecionadoEvent += TiposDevolucaoOnAlterouItemSelecionadoEvent;
			TiposDevolucao.CriarViewModel(((Lista<TipoDevolucao>)TiposDevolucao).ItemSelecionado);
			((IViewModelBase)TiposDevolucao.ViewModel).ProjectedAsViewModel();
		}

		private void TiposDevolucaoOnAlterouItemSelecionadoEvent(object sender, TipoDevolucao tipoDevolucao)
		{
			TiposDevolucao.CriarViewModel(tipoDevolucao);
			((IViewModelBase)TiposDevolucao.ViewModel).ProjectedAsViewModel();
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new DevolucaoFormulario(((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Devolução";
		}

		public string ToTituloFicha()
		{
			return "Devolução";
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			DevolucaoDto dto2 = (DevolucaoDto)((obj is DevolucaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)dto2))
			{
				((EntityDto)dto2).Ativo = base.Ativo;
				dto2.TipoDevolucao = ((Lista<TipoDevolucao>)TiposDevolucao)?.ItemSelecionado?.Codigo ?? 0;
				TiposDevolucao.AtualizarDto(ref dto2);
				((ViewModelDtoBase<DevolucaoDto>)(object)this).AtualizarDto<DevolucaoDto>(ref dto2);
			}
		}

		public override void Dispose()
		{
			TiposDevolucaoComboboxViewModel tiposDevolucao = TiposDevolucao;
			if (tiposDevolucao != null)
			{
				((ViewModelValidationBase)tiposDevolucao).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}
	}
	public class DevolucaoAbaViewModel : AbaPrincipalViewModelBase
	{
		public DevolucaoAbaViewModel(IViewModelPrincipal viewModel)
			: base("Devolução", "Dados principais", viewModel)
		{
		}
	}
	public class DevolucaoFichaAdicionarViewModel : FichaBaseAdicionar<DevolucaoDto, DevolucaoViewModel, IDevolucaoModuleService>
	{
		public DevolucaoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class DevolucaoFichaAtualizarViewModel : FichaBaseAtualizar<DevolucaoDto, DevolucaoViewModel, IDevolucaoModuleService>
	{
		public DevolucaoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class DevolucaoFormulario : FormularioEntity<DevolucaoDto>
	{
		public DevolucaoFormulario(DevolucaoDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new DevolucaoAbaViewModel((IViewModelPrincipal)(object)new DevolucaoViewModel(base.Dto)) });
		}
	}
	public class DevolucaoListaViewModel : ListaEntity<DevolucaoDto, DevolucaoViewModel, IDevolucaoModuleService, Devolucao>
	{
		private readonly IWService _service;

		public ICommand GerarNotaDevolucaoCommand { get; set; }

		public ICommand GerarNotaCompraDevolucaoCommand { get; set; }

		public ICommand AjusteEstoqueMeiCommand { get; set; }

		public ICommand DevolverQuantidadeDavCommand { get; set; }

		public ICommand GerarCreditoCommand { get; set; }

		public ICommand CancelarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand Imprimir40ColunasCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public DevolucaoListaViewModel(ISessao sessao, IWService service, IEntityAtivosFiltroDevolucao<DevolucaoDto, IDevolucaoModuleService> ativos, IEntityInativosFiltroDevolucao<DevolucaoDto, IDevolucaoModuleService> inativos)
			: base(sessao, typeof(DevolucaoFichaAdicionarView).Name, typeof(DevolucaoFichaAtualizarView).Name, (IEntityAtivos<DevolucaoDto, IDevolucaoModuleService>)(object)ativos, (IEntityInativos<DevolucaoDto, IDevolucaoModuleService>)(object)inativos)
		{
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Expected O, but got Unknown
			_service = service;
			GerarNotaDevolucaoCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)GerarNotaDevolucao, (Func<DevolucaoViewModel, bool>)PodeGerarNotaDevolucao, false, (bool?)null);
			GerarNotaCompraDevolucaoCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)GerarNotaCompraDevolucao, (Func<DevolucaoViewModel, bool>)PodeGerarNotaCompraDevolucao, false, (bool?)null);
			AjusteEstoqueMeiCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)AdicionarAjusteEstoqueMei, (Func<DevolucaoViewModel, bool>)PodeAjustarEstoqueMei, false, (bool?)null);
			DevolverQuantidadeDavCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)DevolverQuantidadeDav, (Func<DevolucaoViewModel, bool>)PodeDevolverQuantidadeDav, false, (bool?)null);
			GerarCreditoCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)GerarCredito, (Func<DevolucaoViewModel, bool>)PodeGerarCredito, false, (bool?)null);
			CancelarCommand = (ICommand)new AsyncCommand<DevolucaoViewModel>((Func<DevolucaoViewModel, Task>)Cancelar, (Func<DevolucaoViewModel, bool>)PodeCancelar, false, (bool?)null);
			ImprimirCommand = (ICommand)new DelegateCommand<DevolucaoViewModel>((Action<DevolucaoViewModel>)Imprimir, (Func<DevolucaoViewModel, bool>)PodeImprimir, (bool?)null);
			Imprimir40ColunasCommand = (ICommand)new DelegateCommand<DevolucaoViewModel>((Action<DevolucaoViewModel>)Imprimir40Colunas, (Func<DevolucaoViewModel, bool>)PodeImprimir40Colunas, (bool?)null);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
		}

		private bool PodeCancelar(DevolucaoViewModel arg)
		{
			if (((ListaManutencaoBase<DevolucaoDto, DevolucaoViewModel, Devolucao>)(object)this).PodeEditar() && HelperObject.IsNotNull((object)arg) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.NotaDevolucao) && HelperObject.Is<HistoricoDavDevolvidoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.HistoricoDavDevolvido) && !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado)
			{
				return DadosSistema.VerificarPermissao<Devolucao, PermitirCancelarRegistroDevolucao>();
			}
			return false;
		}

		private async Task Cancelar(DevolucaoViewModel arg)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Tem certeza que deseja cancelar o registro de devolução?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			if (!HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelBase<DevolucaoDto>)(object)arg).Model.AjusteEstoqueMei))
			{
				await _service.Canal<IDevolucaoModuleService, Task<DevolucaoDto>>((Func<IDevolucaoModuleService, Task<DevolucaoDto>>)((IDevolucaoModuleService service) => service.AdicionarAjusteEstoqueMei(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto, true))).ContinueWith(delegate(Task<DevolucaoDto> task)
				{
					LoaderProcessoService.EsconderLoader();
					if (task.IsFaulted)
					{
						ThreadPrincipal.Executar((Action)delegate
						{
							HelperException.ShowLastException(task.Exception, "", false, false);
						});
					}
					else
					{
						ThreadPrincipal.Executar((Action)delegate
						{
							((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Atualizar(task.Result);
							((BindableBase)this).RaisePropertyChanged<Itens<DevolucaoDto, DevolucaoViewModel>>((Expression<Func<Itens<DevolucaoDto, DevolucaoViewModel>>>)(() => ((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens));
						});
					}
				});
				return;
			}
			await _service.Canal<IDevolucaoModuleService, Task<DevolucaoDto>>((Func<IDevolucaoModuleService, Task<DevolucaoDto>>)((IDevolucaoModuleService service) => service.Cancelar(((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto).Id))).ContinueWith(delegate(Task<DevolucaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Atualizar(task.Result);
						((BindableBase)this).RaisePropertyChanged<Itens<DevolucaoDto, DevolucaoViewModel>>((Expression<Func<Itens<DevolucaoDto, DevolucaoViewModel>>>)(() => ((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens));
					});
				}
			});
		}

		private bool PodeImprimir40Colunas(DevolucaoViewModel arg)
		{
			if (arg != null)
			{
				return !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado;
			}
			return false;
		}

		private void Imprimir40Colunas(DevolucaoViewModel arg)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Invalid comparison between Unknown and I4
			try
			{
				if ((int)DadosSistema.TamanhoBobina == 1)
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoCredito56Mm57Mm58Mm(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto)).Processar());
				}
				else
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoCredito79Mm80Mm(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto)).Processar());
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeImprimir(DevolucaoViewModel arg)
		{
			if (arg != null)
			{
				return !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado;
			}
			return false;
		}

		private void Imprimir(DevolucaoViewModel arg)
		{
			try
			{
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoCredito(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto)).Processar());
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeVisualizar()
		{
			if (DadosSistema.VerificarPermissao<Devolucao, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<DevolucaoDto, DevolucaoViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private void Visualizar()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)106);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Invalid comparison between Unknown and I4
			object obj = navigationParameters["MovimentacaoDto"];
			MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			if (HelperObject.IsNotNull((object)val) && (int)((MovimentacaoBaseDto)val).Tipo == 5)
			{
				((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = HelperString.RemoverPontoBarraHifen(((EntityBaseDto)val).Id);
				((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
				((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
			}
			((ListaSelecaoBase<DevolucaoDto, DevolucaoViewModel, Devolucao>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		public override bool PodeEditar()
		{
			if (((ListaManutencaoBase<DevolucaoDto, DevolucaoViewModel, Devolucao>)(object)this).PodeEditar() && HelperObject.Is<CreditoDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Selecionado).Dto.Credito) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Selecionado).Dto.NotaDevolucao) && HelperObject.Is<HistoricoDavDevolvidoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Selecionado).Dto.HistoricoDavDevolvido) && !((ViewModelDtoBase<DevolucaoDto>)(object)((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Selecionado).Dto.Cancelado)
			{
				return HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Selecionado).Dto.AjusteEstoqueMei);
			}
			return false;
		}

		private bool PodeGerarCredito(DevolucaoViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg) && HelperObject.Is<CreditoDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Credito))
			{
				return !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado;
			}
			return false;
		}

		private async Task GerarCredito(DevolucaoViewModel arg)
		{
			if (DialogoFactory.MostrarDialogoSimNao($"Gerar {((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.TotalGerarCredito:c2} em crédito para novas compras para {((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.PessoaNome}?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IDevolucaoModuleService, Task<DevolucaoDto>>((Func<IDevolucaoModuleService, Task<DevolucaoDto>>)((IDevolucaoModuleService service) => service.GerarCredito(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto))).ContinueWith(delegate(Task<DevolucaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Atualizar(task.Result);
						((BindableBase)this).RaisePropertyChanged<Itens<DevolucaoDto, DevolucaoViewModel>>((Expression<Func<Itens<DevolucaoDto, DevolucaoViewModel>>>)(() => ((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens));
					});
				}
			});
		}

		private bool PodeGerarNotaDevolucao(DevolucaoViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.NotaDevolucao) && HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.AjusteEstoqueMei) && ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.TipoDevolucao.Equals(0))
			{
				return !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado;
			}
			return false;
		}

		private async Task GerarNotaDevolucao(DevolucaoViewModel arg)
		{
			MovimentacaoDto movimentacaoDto = new MovimentacaoDto
			{
				Tipo = (TipoMovimentacao)5,
				DevolucaoId = ((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto).Id
			};
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoEntradaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).ImportarMovimentacoesParaDto(movimentacaoDto, new string[1] { ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.MovimentacaoReferencia }))).ContinueWith(delegate(Task<MovimentacaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_002a: Unknown result type (might be due to invalid IL or missing references)
						//IL_002f: Unknown result type (might be due to invalid IL or missing references)
						//IL_004a: Expected O, but got Unknown
						DevolucaoListaViewModel devolucaoListaViewModel = this;
						Action<NavigationResult> obj = delegate
						{
						};
						NavigationParameters val = new NavigationParameters();
						val.Add("MovimentacaoDto", (object)task.Result);
						((NavigationBase)devolucaoListaViewModel).Navegar("NotaFiscalEletronicaEntradaFichaAdicionarView", obj, val);
					});
				}
			});
		}

		private bool PodeGerarNotaCompraDevolucao(DevolucaoViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.NotaDevolucao) && HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.AjusteEstoqueMei) && ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.TipoDevolucao.Equals(0))
			{
				return !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado;
			}
			return false;
		}

		private async Task GerarNotaCompraDevolucao(DevolucaoViewModel arg)
		{
			MovimentacaoDto movimentacaoDto = new MovimentacaoDto
			{
				Tipo = (TipoMovimentacao)4,
				DevolucaoId = ((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto).Id
			};
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoEntradaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).ImportarMovimentacoesParaDto(movimentacaoDto, new string[1] { ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.MovimentacaoReferencia }))).ContinueWith(delegate(Task<MovimentacaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_002a: Unknown result type (might be due to invalid IL or missing references)
						//IL_002f: Unknown result type (might be due to invalid IL or missing references)
						//IL_004a: Expected O, but got Unknown
						DevolucaoListaViewModel devolucaoListaViewModel = this;
						Action<NavigationResult> obj = delegate
						{
						};
						NavigationParameters val = new NavigationParameters();
						val.Add("MovimentacaoDto", (object)task.Result);
						((NavigationBase)devolucaoListaViewModel).Navegar("NotaFiscalCompraFichaAdicionarView", obj, val);
					});
				}
			});
		}

		private bool PodeDevolverQuantidadeDav(DevolucaoViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.NotaDevolucao) && HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.AjusteEstoqueMei) && HelperObject.Is<HistoricoDavDevolvidoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.HistoricoDavDevolvido) && !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado)
			{
				return ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.TipoDevolucao.Equals(1);
			}
			return false;
		}

		private async Task DevolverQuantidadeDav(DevolucaoViewModel arg)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Deseja realmente efetuar a devolução?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await _service.Canal<IDevolucaoModuleService, Task<DevolucaoDto>>((Func<IDevolucaoModuleService, Task<DevolucaoDto>>)((IDevolucaoModuleService service) => service.AjustarEstoqueDevolucaoDav(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto))).ContinueWith(delegate(Task<DevolucaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Atualizar(task.Result);
						((BindableBase)this).RaisePropertyChanged<Itens<DevolucaoDto, DevolucaoViewModel>>((Expression<Func<Itens<DevolucaoDto, DevolucaoViewModel>>>)(() => ((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens));
						string text = string.Empty;
						foreach (ItemDevolucaoDto item in task.Result.Itens.Where((ItemDevolucaoDto x) => !x.Cancelado))
						{
							text += $"{item.Descricao} - {item.Estoque.Quantidade:n2} \n";
						}
						DialogoFactory.MostrarDialogoConfirma($"Produto atualizado e quantidade devolvida: \n {text}", false, System.Windows.HorizontalAlignment.Center);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = HelperString.RemoverPontoBarraHifen(((EntityBaseDto)task.Result).Id);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
					});
				}
			});
		}

		private bool PodeAjustarEstoqueMei(DevolucaoViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg) && HelperObject.Is<NotaDevolucaoDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.NotaDevolucao) && HelperObject.Is<AjusteEstoqueMeiDtoNull>((object)((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.AjusteEstoqueMei) && !((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.Cancelado && ((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto.TipoDevolucao.Equals(0))
			{
				return DadosSistema.IsMEI;
			}
			return false;
		}

		private async Task AdicionarAjusteEstoqueMei(DevolucaoViewModel arg)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Deseja realmente efetuar a devolução?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await _service.Canal<IDevolucaoModuleService, Task<DevolucaoDto>>((Func<IDevolucaoModuleService, Task<DevolucaoDto>>)((IDevolucaoModuleService service) => service.AdicionarAjusteEstoqueMei(((ViewModelDtoBase<DevolucaoDto>)(object)arg).Dto, false))).ContinueWith(delegate(Task<DevolucaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens.Atualizar(task.Result);
						((BindableBase)this).RaisePropertyChanged<Itens<DevolucaoDto, DevolucaoViewModel>>((Expression<Func<Itens<DevolucaoDto, DevolucaoViewModel>>>)(() => ((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Itens));
						string text = string.Empty;
						foreach (ItemDevolucaoDto iten in task.Result.Itens)
						{
							text += $"{iten.Descricao} - {iten.Estoque.Quantidade:n2} \n";
						}
						DialogoFactory.MostrarDialogoConfirma($"Produto atualizado e quantidade devolvida: \n {text}", false, System.Windows.HorizontalAlignment.Center);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = HelperString.RemoverPontoBarraHifen(((EntityBaseDto)task.Result).Id);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
						((ListaBase<DevolucaoDto, DevolucaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
					});
				}
			});
		}
	}
	public class DevolucaoDavViewModel : DevolucaoViewModel
	{
		public Lista<ItemDevolvidoDavViewModel> _itens;

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

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => HelperObject.IsNot<ClienteDtoNull>((object)x.Cliente) && !x.ConsumidorReservadoSistema), "Selecione uma pessoa do tipo 'Cliente'!", (TipoPessoaCadastro)0, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Cliente' é obrigatório.")]
		public string PessoaNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PessoaNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PessoaNome), value, (Action)RemoverPessoaQuandoNomeApagado);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o campo 'Pessoa' pela pesquisa: o texto digitado nao corresponde a um cadastro.")]
		public string PessoaSelecionada
		{
			get
			{
				if (!HelperString.IsNullOrEmpty(PessoaReferencia))
				{
					return PessoaNome;
				}
				return string.Empty;
			}
		}

		public string PessoaReferencia { get; protected set; }

		public string PessoaDocumento { get; protected set; }

		public ISelecaoCommandParameter ProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> produtoServico)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					if (produtoServico.Any((ProdutoServicoEmpresaDto x) => !HelperObject.IsExactlyObject<ProdutoEmpresaDto>((object)x.ProdutoEmpresa)))
					{
						DialogoFactory.MostrarDialogoConfirma("Apenas produtos vendáveis serão adicionados.", false, System.Windows.HorizontalAlignment.Center);
					}
					ProdutosEmpresa = produtoServico.Where((ProdutoServicoEmpresaDto x) => HelperObject.IsExactlyObject<ProdutoEmpresaDto>((object)x.ProdutoEmpresa));
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
					DevolucaoDavViewModel devolucaoDavViewModel = this;
					IEnumerable<ProdutoServicoEmpresaDto> produtosEmpresa = ProdutosEmpresa;
					object obj;
					if (produtosEmpresa == null || produtosEmpresa.Count() <= 1)
					{
						IEnumerable<ProdutoServicoEmpresaDto> produtosEmpresa2 = ProdutosEmpresa;
						if (produtosEmpresa2 == null)
						{
							obj = null;
						}
						else
						{
							ProdutoServicoEmpresaDto obj2 = produtosEmpresa2.FirstOrDefault();
							if (obj2 == null)
							{
								obj = null;
							}
							else
							{
								ProdutoServicoDto produtoServico2 = obj2.ProdutoServico;
								obj = ((produtoServico2 != null) ? produtoServico2.Descricao : null);
							}
						}
						if (obj == null)
						{
							obj = string.Empty;
						}
					}
					else
					{
						obj = $"{ProdutosEmpresa?.Count()} Produtos selecionados";
					}
					devolucaoDavViewModel.ProdutoDescricao = (string)obj;
					((UpdateViewBindableBase)this).RecuperarFoco();
				}
			}).Start();
		}, true);

		public string ProdutoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoDescricao), value);
			}
		}

		public bool PossuiItemNaoEncontrado => Itens.Itens.Any((ItemDevolvidoDavViewModel x) => x.Cancelado);

		public IEnumerable<ProdutoServicoEmpresaDto> ProdutosEmpresa { get; protected set; }

		public ISelecaoCommandParameter MovimentacaoCommandParameter
		{
			get
			{
				SelecaoCommandParameter<IEnumerable<MovimentacaoBaseDto>> obj = new SelecaoCommandParameter<IEnumerable<MovimentacaoBaseDto>>("MovimentacaoSaidaListaView", (Action<IEnumerable<MovimentacaoBaseDto>>)delegate(IEnumerable<MovimentacaoBaseDto> dtos)
				{
					LoaderProcessoService.MostrarLoader("Processando...");
					new Thread((ThreadStart)async delegate
					{
						try
						{
							if (dtos.Any((MovimentacaoBaseDto x) => ((EntityBaseDto)x.Pessoa).Id != PessoaReferencia))
							{
								DialogoFactory.MostrarDialogoConfirma("Apenas movimentações do cliente informado na devolução serão adicionadas.", false, System.Windows.HorizontalAlignment.Center);
							}
							if (!dtos.Any((MovimentacaoBaseDto x) => (int)x.Tipo == 1 || (int)x.Tipo == 2))
							{
								DialogoFactory.MostrarDialogoConfirma("Apenas movimentações do tipo DAV/DAV-OS serão adicionadas.", false, System.Windows.HorizontalAlignment.Center);
							}
							Movimentacoes = new Lista<MovimentacaoDto>();
							foreach (MovimentacaoBaseDto dto in dtos)
							{
								MovimentacaoBaseDto obj2 = await DadosSistema.GetService().Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IReferenciaAtualizarService<MovimentacaoBaseDto>)(object)service).AtualizarDtoAsync(dto)));
								MovimentacaoDto movimentacao = (MovimentacaoDto)(object)((obj2 is MovimentacaoDto) ? obj2 : null);
								AdicionarMovimentacao(movimentacao);
							}
						}
						finally
						{
							LoaderProcessoService.EsconderLoader();
							DevolucaoDavViewModel devolucaoDavViewModel = this;
							Lista<MovimentacaoDto> movimentacoes = Movimentacoes;
							object obj3;
							if (movimentacoes == null || !(movimentacoes.Itens?.Count > 1))
							{
								Lista<MovimentacaoDto> movimentacoes2 = Movimentacoes;
								if (movimentacoes2 == null)
								{
									obj3 = null;
								}
								else
								{
									IReadOnlyCollection<MovimentacaoDto> itens = movimentacoes2.Itens;
									if (itens == null)
									{
										obj3 = null;
									}
									else
									{
										MovimentacaoDto obj4 = itens.FirstOrDefault();
										obj3 = ((obj4 != null) ? ((BaseDto)obj4).ToText : null);
									}
								}
								if (obj3 == null)
								{
									obj3 = string.Empty;
								}
							}
							else
							{
								obj3 = $"{Movimentacoes?.Itens?.Count} Movimentações selecionada(s)";
							}
							devolucaoDavViewModel.MovimentacaoDescricao = (string)obj3;
							((UpdateViewBindableBase)this).RecuperarFoco();
						}
					}).Start();
				}, true);
				obj.AdicionarParametro("PessoaDocumento", (object)PessoaDocumento);
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public Lista<MovimentacaoDto> Movimentacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<MovimentacaoDto>>((Expression<Func<Lista<MovimentacaoDto>>>)(() => Movimentacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<MovimentacaoDto>>((Expression<Func<Lista<MovimentacaoDto>>>)(() => Movimentacoes), value);
			}
		}

		[Valid(ErrorMessage = "A lista deve conter itens.")]
		public Lista<ItemDevolvidoDavViewModel> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemDevolvidoDavViewModel>>((Expression<Func<Lista<ItemDevolvidoDavViewModel>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemDevolvidoDavViewModel>>((Expression<Func<Lista<ItemDevolvidoDavViewModel>>>)(() => Itens), value);
			}
		}

		public string MovimentacaoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MovimentacaoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MovimentacaoDescricao), value);
			}
		}

		public override bool Valido
		{
			get
			{
				if (Itens.Itens.Any())
				{
					return Itens.Itens.Any((ItemDevolvidoDavViewModel x) => x.QuantidadeDevolver > 0m);
				}
				return false;
			}
		}

		public decimal Total => Itens.Itens.Sum((ItemDevolvidoDavViewModel x) => x.Total);

		public bool TemItens => Itens.Itens.Any();

		public ICommand<ItemDevolvidoDavViewModel> RemoverItemCommand { get; set; }

		public ICommand AdicionarProdutosCommand { get; set; }

		public ICommand AdicionarMovimentacoesCommand { get; set; }

		public ICommand RemoverProdutoCommand { get; set; }

		public ICommand RemoverMovimentacaoCommand { get; set; }

		public ICommand PesquisarCommand { get; }

		public ICommand LimparCommand { get; }

		public DevolucaoDavViewModel()
		{
		}

		public DevolucaoDavViewModel(DevolucaoDto dto)
			: base(dto)
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Expected O, but got Unknown
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Expected O, but got Unknown
			RemoverItemCommand = (ICommand<ItemDevolvidoDavViewModel>)(object)new DelegateCommand<ItemDevolvidoDavViewModel>((Action<ItemDevolvidoDavViewModel>)RemoverItem, (Func<ItemDevolvidoDavViewModel, bool>)PodeRemoverItem, (bool?)null);
			AdicionarProdutosCommand = (ICommand)new AsyncCommand((Func<Task>)AdicionarPorProduto, (Func<bool>)PodeAdicionarPorProduto, (bool?)null);
			AdicionarMovimentacoesCommand = (ICommand)new AsyncCommand((Func<Task>)AdicionarPorMovimentacao, (Func<bool>)PodeAdicionarPorMovimentacao, (bool?)null);
			RemoverProdutoCommand = (ICommand)new DelegateCommand((Action)RemoverProduto);
			RemoverMovimentacaoCommand = (ICommand)new DelegateCommand((Action)RemoverMovimentacao);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)PesquisarItens, (Func<bool>)PodePesquisarItens, (bool?)null);
			LimparCommand = (ICommand)new DelegateCommand((Action)LimparPesquisa);
		}

		private void PesquisarItens()
		{
			string informacao = HelperString.RemoveDiacritics(HelperString.ToStringEmptyWhenNull(InformacaoPesquisa)).ToLower();
			foreach (ItemDevolvidoDavViewModel item in _itens.Itens)
			{
				ItemDevolvidoDavViewModel itemDevolvidoDavViewModel = Itens.Itens.FirstOrDefault((ItemDevolvidoDavViewModel x) => x.NumeroItem == item.NumeroItem && x.NumeroDocumento == item.NumeroDocumento);
				if (itemDevolvidoDavViewModel != null)
				{
					item.QuantidadeDevolver = itemDevolvidoDavViewModel.QuantidadeDevolver;
					item.EstoqueDevolvido.Quantidade = itemDevolvidoDavViewModel.QuantidadeDevolver;
				}
			}
			Itens.Limpar();
			Itens.Adicionar(from x in _itens.Itens
				where x.ToText.StartsWith(informacao) || x.ToText.Contains(informacao)
				select x into itemDevolvido
				select new ItemDevolvidoDavViewModel(itemDevolvido.ProdutoServicoEmpresaReferencia, itemDevolvido.Codigo, itemDevolvido.Descricao, itemDevolvido.Unidade, itemDevolvido.PrecoUnitario, itemDevolvido.NumeroItem, itemDevolvido.EstoqueVenda, itemDevolvido.EstoqueDevolvido, itemDevolvido.EstoqueDevolvido.Quantidade, itemDevolvido.PrecoUnitarioGerarCredito, itemDevolvido.NumeroDocumento, itemDevolvido.MovimentacaoReferencia, itemDevolvido.Cancelado));
			if (string.IsNullOrWhiteSpace(informacao))
			{
				Itens.PosicionarNoUltimo();
			}
			else
			{
				Itens.PosicionarNoPrimeiro();
			}
		}

		private bool PodePesquisarItens()
		{
			return ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens.Any();
		}

		private void LimparPesquisa()
		{
			InformacaoPesquisa = string.Empty;
			PesquisarItens();
		}

		private void RemoverProduto()
		{
			ProdutosEmpresa = (IEnumerable<ProdutoServicoEmpresaDto>)new List<ProdutoServicoEmpresaDtoNull>();
			ProdutoDescricao = string.Empty;
			((UpdateViewBindableBase)this).RecuperarFoco();
		}

		private void RemoverMovimentacao()
		{
			Movimentacoes = new Lista<MovimentacaoDto>();
			MovimentacaoDescricao = string.Empty;
			((UpdateViewBindableBase)this).RecuperarFoco();
		}

		private void SelecionouPessoa(PessoaDto pessoa)
		{
			PessoaNome = pessoa.Nome;
			PessoaReferencia = ((EntityBaseDto)pessoa).Id;
			PessoaDocumento = pessoa.CpfCnpjFormatado;
			((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => MovimentacaoCommandParameter));
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(PessoaNome, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			if (HelperString.IsNullOrEmpty(PessoaNome))
			{
				PessoaReferencia = string.Empty;
				PessoaDocumento = string.Empty;
				((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => MovimentacaoCommandParameter));
			}
		}

		private async Task AdicionarPorProduto()
		{
			await DadosSistema.GetService().Canal<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoMovimentacaoDto>>>((Func<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoMovimentacaoDto>>>)((IDevolucaoModuleService service) => service.ObterMovimentacoesPorProdutoDevolucao(PessoaReferencia, ProdutosEmpresa))).ContinueWith(delegate(Task<IEnumerable<ItemDevolucaoMovimentacaoDto>> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						if (!task.Result.Any())
						{
							DialogoFactory.MostrarDialogoConfirma("Nenhuma movimentação pendente foi encontrada para o(s) produto(s) selecionado(s).", false, System.Windows.HorizontalAlignment.Center);
						}
						else
						{
							foreach (ItemDevolucaoMovimentacaoDto item2 in task.Result)
							{
								if (!Itens.Existe((Expression<Func<ItemDevolvidoDavViewModel, bool>>)((ItemDevolvidoDavViewModel x) => x.MovimentacaoReferencia == item2.MovimentacaoReferencia && x.ProdutoServicoEmpresaReferencia == item2.Item.ProdutoServicoEmpresaReferencia)))
								{
									Itens.Adicionar(new ItemDevolvidoDavViewModel(item2.Item.ProdutoServicoEmpresaReferencia, HelperString.IsNullOrEmpty(item2.Item.ProdutoServico.Produto.CodigoBarras) ? item2.Item.ProdutoServico.CodigoInterno : item2.Item.ProdutoServico.Produto.CodigoBarras, item2.Item.ProdutoServico.Descricao, item2.Item.UnidadeMedida.Sigla, item2.Item.TotalLiquido / ((item2.Item.Quantidade == 0m) ? 1m : item2.Item.Quantidade), item2.Item.Numero, item2.Item.Estoque, item2.Item.Estoque, 0m, item2.Item.Total / ((item2.Item.Quantidade == 0m) ? 1m : item2.Item.Quantidade), item2.MovimentacaoNumero, item2.MovimentacaoReferencia, cancelado: false));
								}
							}
							Itens.OrdenarDescending((Expression<Func<ItemDevolvidoDavViewModel, object>>)((ItemDevolvidoDavViewModel x) => x.NumeroDocumento));
							((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(Itens.Itens.Select((ItemDevolvidoDavViewModel item) => item.ObterDto()));
							_itens = new Lista<ItemDevolvidoDavViewModel>();
							_itens.Adicionar((IEnumerable<ItemDevolvidoDavViewModel>)Itens.Itens);
							((BindableBase)this).RaisePropertyChanged<Lista<ItemDevolvidoDavViewModel>>((Expression<Func<Lista<ItemDevolvidoDavViewModel>>>)(() => Itens));
						}
					});
				}
			});
			ProdutoDescricao = string.Empty;
			ProdutosEmpresa = new List<ProdutoServicoEmpresaDto>();
			((UpdateViewBindableBase)this).RecuperarFoco();
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => TemItens));
		}

		public bool PodeAdicionarPorProduto()
		{
			if (!HelperString.IsNullOrEmpty(ProdutoDescricao))
			{
				return HelperString.IsNullOrEmpty(MovimentacaoDescricao);
			}
			return false;
		}

		private bool PodeRemoverItem(ItemDevolvidoDavViewModel arg)
		{
			return HelperObject.IsNotNull((object)arg);
		}

		private void RemoverItem(ItemDevolvidoDavViewModel obj)
		{
			Itens.Remover(obj);
			_itens.Remover(obj);
		}

		public bool PodeAdicionarPorMovimentacao()
		{
			if (!HelperString.IsNullOrEmpty(MovimentacaoDescricao))
			{
				return HelperString.IsNullOrEmpty(ProdutoDescricao);
			}
			return false;
		}

		private void AdicionarMovimentacao(MovimentacaoDto movimentacao)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				//IL_0029: Unknown result type (might be due to invalid IL or missing references)
				//IL_002f: Invalid comparison between Unknown and I4
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				//IL_003d: Invalid comparison between Unknown and I4
				//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
				if (!(PessoaReferencia != ((EntityBaseDto)((MovimentacaoBaseDto)movimentacao).Pessoa).Id) && ((int)((MovimentacaoBaseDto)movimentacao).Tipo == 1 || (int)((MovimentacaoBaseDto)movimentacao).Tipo == 2))
				{
					if (Itens.Existe((Expression<Func<ItemDevolvidoDavViewModel, bool>>)((ItemDevolvidoDavViewModel x) => x.NumeroDocumento == ((MovimentacaoBaseDto)movimentacao).Numero)))
					{
						DialogoFactory.MostrarDialogoConfirma($"Movimentação do tipo {HelperEnum.RetornarDescricao((Enum)(object)((MovimentacaoBaseDto)movimentacao).Tipo)}, número {((MovimentacaoBaseDto)movimentacao).Numero}, já foi adicionada.", false, System.Windows.HorizontalAlignment.Center);
					}
					else
					{
						Movimentacoes.Adicionar(movimentacao);
						((BindableBase)this).RaisePropertyChanged<Lista<MovimentacaoDto>>((Expression<Func<Lista<MovimentacaoDto>>>)(() => Movimentacoes));
						((BindableBase)this).RaisePropertyChanged(MovimentacaoDescricao);
						((UpdateViewBindableBase)this).RecuperarFoco();
					}
				}
			});
		}

		private async Task AdicionarPorMovimentacao()
		{
			IEnumerable<ItemDevolucaoDto> saldos = default(IEnumerable<ItemDevolucaoDto>);
			try
			{
				_ = saldos;
				saldos = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(await DadosSistema.GetService().Canal<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoDto>>>((Func<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoDto>>>)((IDevolucaoModuleService service) => service.ObterSaldoItensDevolucao((IEnumerable<string>)Movimentacoes.Itens.Select((MovimentacaoDto x) => ((EntityBaseDto)x).Id).ToList(), ((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).Id))));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				return;
			}
			if (!saldos.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("As movimentações selecionadas não possuem produtos com saldo disponível para devolução.", false, System.Windows.HorizontalAlignment.Center);
			}
			foreach (MovimentacaoDto movimentacao in Movimentacoes.Itens)
			{
				try
				{
					Itens.Adicionar(from x in movimentacao.Itens.Where((ItemDto y) => !y.ProdutoServico.IsServico && !y.Cancelado).Select(delegate(ItemDto item)
						{
							ItemDto item2 = item;
							ItemDevolucaoDto obj = saldos.FirstOrDefault((ItemDevolucaoDto x) => x.MovimentacaoReferencia == ((EntityBaseDto)movimentacao).Id && x.NumeroItem == item.Numero);
							return new
							{
								Item = item2,
								Saldo = ((obj != null) ? obj.Estoque : null)
							};
						})
						where HelperObject.IsNotNull((object)x.Saldo)
						select new ItemDevolvidoDavViewModel(x.Item.ProdutoServicoEmpresaReferencia, HelperString.IsNullOrEmpty(x.Item.ProdutoServico.Produto.CodigoBarras) ? x.Item.ProdutoServico.CodigoInterno : x.Item.ProdutoServico.Produto.CodigoBarras, x.Item.ProdutoServico.Descricao, x.Item.UnidadeMedida.Sigla, x.Item.TotalLiquido / ((x.Item.Quantidade == 0m) ? 1m : x.Item.Quantidade), x.Item.Numero, x.Saldo, x.Saldo, 0m, x.Item.Total / ((x.Item.Quantidade == 0m) ? 1m : x.Item.Quantidade), ((MovimentacaoBaseDto)movimentacao).Numero, ((EntityBaseDto)movimentacao).Id, cancelado: false));
					Itens.OrdenarDescending((Expression<Func<ItemDevolvidoDavViewModel, object>>)((ItemDevolvidoDavViewModel x) => x.NumeroDocumento));
					_itens = new Lista<ItemDevolvidoDavViewModel>();
					_itens.Adicionar((IEnumerable<ItemDevolvidoDavViewModel>)Itens.Itens);
					((BindableBase)this).RaisePropertyChanged<Lista<ItemDevolvidoDavViewModel>>((Expression<Func<Lista<ItemDevolvidoDavViewModel>>>)(() => Itens));
				}
				catch (Exception ex2)
				{
					HelperException.ShowLastException(ex2, "", false, false);
				}
			}
			((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(Itens.Itens.Select((ItemDevolvidoDavViewModel item) => item.ObterDto()));
			Movimentacoes = new Lista<MovimentacaoDto>();
			MovimentacaoDescricao = string.Empty;
			((UpdateViewBindableBase)this).RecuperarFoco();
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => TemItens));
		}

		public override void ProjectedAsViewModel()
		{
			Movimentacoes = new Lista<MovimentacaoDto>();
			((ViewModelAtivoBase<DevolucaoDto>)this).Ativo = ((EntityDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).Ativo;
			PessoaNome = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.PessoaNome;
			PessoaDocumento = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.PessoaDocumento;
			PessoaReferencia = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.PessoaReferencia;
			Itens = new Lista<ItemDevolvidoDavViewModel>();
			Itens.Adicionar(((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens.Select((ItemDevolucaoDto itemDevolvido) => new ItemDevolvidoDavViewModel(itemDevolvido.ProdutoServicoEmpresaReferencia, itemDevolvido.Codigo, itemDevolvido.Descricao, itemDevolvido.Unidade, itemDevolvido.PrecoUnitario, itemDevolvido.NumeroItem, itemDevolvido.EstoqueVenda, itemDevolvido.Estoque, itemDevolvido.Estoque.Quantidade, itemDevolvido.PrecoUnitarioGerarCredito, itemDevolvido.NumeroDocumento, itemDevolvido.MovimentacaoReferencia, itemDevolvido.Cancelado)));
			Itens.OrdenarDescending((Expression<Func<ItemDevolvidoDavViewModel, object>>)((ItemDevolvidoDavViewModel x) => x.NumeroDocumento));
			_itens = new Lista<ItemDevolvidoDavViewModel>();
			_itens.Adicionar((IEnumerable<ItemDevolvidoDavViewModel>)Clone.ObjectGraph<IReadOnlyCollection<ItemDevolvidoDavViewModel>>(Itens.Itens));
			((BindableBase)this).RaisePropertiesChanged<Lista<ItemDevolvidoDavViewModel>, string, bool, bool>((Expression<Func<Lista<ItemDevolvidoDavViewModel>>>)(() => Itens), (Expression<Func<string>>)(() => MovimentacaoDescricao), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DevolucaoDavViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DevolucaoDavViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsListaViewModel()
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			DevolucaoDto val = (DevolucaoDto)((obj is DevolucaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				InformacaoPesquisa = string.Empty;
				PesquisarItens();
				val.PessoaNome = PessoaNome;
				val.PessoaReferencia = PessoaReferencia;
				val.MovimentacoesReferencia = from x in Itens.Itens
					where x.QuantidadeDevolver > 0m
					select x.MovimentacaoReferencia;
				val.MovimentacaoDescricao = MovimentacaoDescricao;
				val.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(from x in Itens.Itens
					where x.QuantidadeDevolver > 0m
					select x into item
					select item.ObterDto());
			}
		}
	}
	public class DevolucaoPadraoViewModel : DevolucaoViewModel
	{
		public Lista<ItemDevolvidoViewModel> _itens;

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

		public bool TemItens => Itens.Itens.Any();

		public ISelecaoCommandParameter MovimentacaoCommandParameter
		{
			get
			{
				SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)async delegate(MovimentacaoDto movimentacao)
				{
					try
					{
						if (!movimentacao.IsMovimentacaoEfetivacaoSaida)
						{
							throw new Exception("Selecione uma venda.");
						}
						List<ItemDevolucaoDto> saldos = default(List<ItemDevolucaoDto>);
						_ = saldos;
						saldos = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(await DadosSistema.GetService().Canal<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoDto>>>((Func<IDevolucaoModuleService, Task<IEnumerable<ItemDevolucaoDto>>>)((IDevolucaoModuleService service) => service.ObterSaldoItensDevolucao((IEnumerable<string>)new string[1] { ((EntityBaseDto)movimentacao).Id }, ((EntityBaseDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).Id))));
						var source = (from x in movimentacao.Itens.Select(delegate(ItemDto item)
							{
								ItemDto item2 = item;
								ItemDevolucaoDto obj2 = saldos.FirstOrDefault((ItemDevolucaoDto x) => x.NumeroItem == item.Numero);
								return new
								{
									Item = item2,
									Saldo = ((obj2 != null) ? obj2.Estoque : null)
								};
							})
							where HelperObject.IsNotNull((object)x.Saldo)
							select x).ToList();
						if (!source.Any())
						{
							throw new Exception("Esta venda não possui produtos com saldo disponível para devolução.");
						}
						PessoaDto val = (PessoaDto)((!((MovimentacaoBaseDto)movimentacao).Pessoa.ConsumidorReservadoSistema) ? ((object)((MovimentacaoBaseDto)movimentacao).Pessoa) : ((object)new PessoaDtoNull()));
						PessoaNome = val.Nome;
						PessoaReferencia = ((EntityBaseDto)val).Id;
						MovimentacaoReferencia = ((EntityBaseDto)movimentacao).Id;
						MovimentacaoDescricao = ((BaseDto)movimentacao).ToText;
						Itens.Limpar();
						Itens.Adicionar(source.Select(x => new ItemDevolvidoViewModel(x.Item.ProdutoServicoEmpresaReferencia, x.Item.ProdutoServico.CodigoInterno, x.Item.ProdutoServico.Descricao, x.Item.UnidadeMedida.Sigla, x.Item.TotalLiquido / ((x.Item.Quantidade == 0m) ? 1m : x.Item.Quantidade), x.Item.Numero, x.Saldo, x.Saldo, x.Item.Total / ((x.Item.Quantidade == 0m) ? 1m : x.Item.Quantidade), ((MovimentacaoBaseDto)movimentacao).Numero, ((EntityBaseDto)movimentacao).Id)));
						((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(from x in Itens.Itens
							where x.QuantidadeDevolver > 0m
							select x into item
							select item.ObterDto());
						_itens = new Lista<ItemDevolvidoViewModel>();
						_itens.Adicionar((IEnumerable<ItemDevolvidoViewModel>)Itens.Itens);
						FocoNaPessoa = !FocoNaPessoa;
						((BindableBase)this).RaisePropertiesChanged<Lista<ItemDevolvidoViewModel>, bool>((Expression<Func<Lista<ItemDevolvidoViewModel>>>)(() => Itens), (Expression<Func<bool>>)(() => TemItens));
					}
					catch (Exception ex)
					{
						HelperException.ShowLastException(ex, "", false, false);
					}
				}, true);
				obj.AdicionarParametro("OcultarCheckbox", (object)true);
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => TemItens));
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => HelperObject.IsNot<ClienteDtoNull>((object)x.Cliente) && !x.ConsumidorReservadoSistema), "Selecione uma pessoa do tipo 'Cliente'!", (TipoPessoaCadastro)0, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o campo 'Pessoa' pela pesquisa: o texto digitado nao corresponde a um cadastro.")]
		public string PessoaSelecionada
		{
			get
			{
				if (!HelperString.IsNullOrEmpty(PessoaReferencia))
				{
					return PessoaNome;
				}
				return string.Empty;
			}
		}

		[Valid(ErrorMessage = "A lista deve conter itens.")]
		public Lista<ItemDevolvidoViewModel> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemDevolvidoViewModel>>((Expression<Func<Lista<ItemDevolvidoViewModel>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemDevolvidoViewModel>>((Expression<Func<Lista<ItemDevolvidoViewModel>>>)(() => Itens), value);
			}
		}

		[Required(ErrorMessage = "O campo 'Movimentação' é obrigatório.", AllowEmptyStrings = false)]
		public string MovimentacaoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MovimentacaoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MovimentacaoDescricao), value);
			}
		}

		public string MovimentacaoReferencia { get; protected set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Cliente' é obrigatório.")]
		public string PessoaNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PessoaNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PessoaNome), value, (Action)RemoverPessoaQuandoNomeApagado);
			}
		}

		public string PessoaReferencia { get; protected set; }

		public bool FocoNaPessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa), value);
			}
		}

		public string NotaFiscalEletronicaReferencia { get; set; }

		public override bool Valido
		{
			get
			{
				if (Itens.Itens.Any())
				{
					return Itens.Itens.Any((ItemDevolvidoViewModel x) => x.QuantidadeDevolver > 0m);
				}
				return false;
			}
		}

		public decimal Total => Itens.Itens.Sum((ItemDevolvidoViewModel x) => x.Total);

		public ICommand<ItemDevolvidoViewModel> RemoverItemCommand { get; set; }

		public ICommand PesquisarCommand { get; }

		public ICommand LimparCommand { get; }

		public DevolucaoPadraoViewModel()
		{
		}

		public DevolucaoPadraoViewModel(DevolucaoDto dto)
			: base(dto)
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			RemoverItemCommand = (ICommand<ItemDevolvidoViewModel>)(object)new DelegateCommand<ItemDevolvidoViewModel>((Action<ItemDevolvidoViewModel>)RemoverItem, (Func<ItemDevolvidoViewModel, bool>)PodeRemoverItem, (bool?)null);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)PesquisarItens, (Func<bool>)PodePesquisarItens, (bool?)null);
			LimparCommand = (ICommand)new DelegateCommand((Action)LimparPesquisa);
		}

		private void PesquisarItens()
		{
			string informacao = HelperString.RemoveDiacritics(HelperString.ToStringEmptyWhenNull(InformacaoPesquisa)).ToLower();
			foreach (ItemDevolvidoViewModel item in _itens.Itens)
			{
				ItemDevolvidoViewModel itemDevolvidoViewModel = Itens.Itens.FirstOrDefault((ItemDevolvidoViewModel x) => x.NumeroItem == item.NumeroItem);
				if (itemDevolvidoViewModel != null)
				{
					item.QuantidadeDevolver = itemDevolvidoViewModel.QuantidadeDevolver;
					item.EstoqueDevolvido.Quantidade = itemDevolvidoViewModel.QuantidadeDevolver;
				}
			}
			Itens.Limpar();
			Itens.Adicionar(from x in _itens.Itens
				where x.ToText.StartsWith(informacao) || x.ToText.Contains(informacao)
				select x into itemDevolvido
				select new ItemDevolvidoViewModel(itemDevolvido.ProdutoServicoEmpresaReferencia, itemDevolvido.Codigo, itemDevolvido.Descricao, itemDevolvido.Unidade, itemDevolvido.PrecoUnitario, itemDevolvido.NumeroItem, itemDevolvido.EstoqueVenda, itemDevolvido.EstoqueDevolvido, itemDevolvido.PrecoUnitarioGerarCredito, itemDevolvido.NumeroDocumento, itemDevolvido.MovimentacaoReferencia));
			if (string.IsNullOrWhiteSpace(informacao))
			{
				Itens.PosicionarNoUltimo();
			}
			else
			{
				Itens.PosicionarNoPrimeiro();
			}
		}

		private bool PodePesquisarItens()
		{
			return ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens.Any();
		}

		private void LimparPesquisa()
		{
			InformacaoPesquisa = string.Empty;
			PesquisarItens();
		}

		private bool PodeRemoverItem(ItemDevolvidoViewModel arg)
		{
			return HelperObject.IsNotNull((object)arg);
		}

		private void RemoverItem(ItemDevolvidoViewModel obj)
		{
			Itens.Remover(obj);
			_itens.Remover(obj);
		}

		private void SelecionouPessoa(PessoaDto pessoa)
		{
			PessoaNome = pessoa.Nome;
			PessoaReferencia = ((EntityBaseDto)pessoa).Id;
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(PessoaNome, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			if (HelperString.IsNullOrEmpty(PessoaNome))
			{
				PessoaReferencia = string.Empty;
			}
		}

		public override void ProjectedAsViewModel()
		{
			((ViewModelAtivoBase<DevolucaoDto>)this).Ativo = ((EntityDto)((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto).Ativo;
			MovimentacaoReferencia = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.MovimentacaoReferencia;
			MovimentacaoDescricao = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.MovimentacaoDescricao;
			PessoaNome = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.PessoaNome;
			PessoaReferencia = ((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.PessoaReferencia;
			Itens = new Lista<ItemDevolvidoViewModel>();
			Itens.Adicionar(((ViewModelDtoBase<DevolucaoDto>)(object)this).Dto.Itens.Select((ItemDevolucaoDto itemDevolvido) => new ItemDevolvidoViewModel(itemDevolvido.ProdutoServicoEmpresaReferencia, itemDevolvido.Codigo, itemDevolvido.Descricao, itemDevolvido.Unidade, itemDevolvido.PrecoUnitario, itemDevolvido.NumeroItem, itemDevolvido.EstoqueVenda, itemDevolvido.Estoque, itemDevolvido.PrecoUnitarioGerarCredito, itemDevolvido.NumeroDocumento, itemDevolvido.MovimentacaoReferencia)));
			_itens = new Lista<ItemDevolvidoViewModel>();
			_itens.Adicionar((IEnumerable<ItemDevolvidoViewModel>)Clone.ObjectGraph<IReadOnlyCollection<ItemDevolvidoViewModel>>(Itens.Itens));
			((BindableBase)this).RaisePropertiesChanged<Lista<ItemDevolvidoViewModel>, string, bool, bool>((Expression<Func<Lista<ItemDevolvidoViewModel>>>)(() => Itens), (Expression<Func<string>>)(() => MovimentacaoDescricao), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DevolucaoPadraoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(DevolucaoPadraoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsListaViewModel()
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			DevolucaoDto val = (DevolucaoDto)((obj is DevolucaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				InformacaoPesquisa = string.Empty;
				PesquisarItens();
				val.PessoaNome = PessoaNome;
				val.PessoaReferencia = PessoaReferencia;
				val.MovimentacaoReferencia = MovimentacaoReferencia;
				val.MovimentacaoDescricao = MovimentacaoDescricao;
				val.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDevolucaoDto>(from x in Itens.Itens
					where x.QuantidadeDevolver > 0m
					select x into item
					select item.ObterDto());
			}
		}
	}
	public class DevolucoesViewModel : ModuloBase
	{
		public DevolucoesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloDevolucaoRegion, typeof(DevolucaoListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ItemDevolvidoDavViewModel : BindableBase
	{
		public EstoqueDto EstoqueVenda { get; set; }

		public EstoqueDto EstoqueDevolvido { get; set; }

		public TipoEstoqueBaseViewModel TipoEstoque { get; set; }

		public int NumeroItem { get; set; }

		public decimal Total => PrecoUnitario * EstoqueDevolvido.Quantidade;

		public string ToText => HelperString.RemoveDiacritics(string.Join(" ", Descricao, Codigo)).ToLower();

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

		public string ProdutoServicoEmpresaReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaReferencia), value);
			}
		}

		public string Codigo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Codigo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Codigo), value);
			}
		}

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

		public decimal QuantidadeDevolver
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolver));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolver), value);
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

		public decimal PrecoUnitarioGerarCredito
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitarioGerarCredito));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitarioGerarCredito), value);
			}
		}

		public long NumeroDocumento
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => NumeroDocumento));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => NumeroDocumento), value);
			}
		}

		public string MovimentacaoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MovimentacaoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MovimentacaoReferencia), value);
			}
		}

		public bool Cancelado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Cancelado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Cancelado), value);
			}
		}

		public bool PossuiPrecoBruto => PrecoUnitarioGerarCredito > 0m;

		public bool PodeAlterarItem => !Cancelado;

		public double Opacidade
		{
			get
			{
				if (!Cancelado)
				{
					return 1.0;
				}
				return 0.35;
			}
		}

		public ItemDevolvidoDavViewModel()
		{
		}

		public ItemDevolvidoDavViewModel(string produtoServicoEmpresaReferencia, string codigo, string descricao, string unidade, decimal precoUnitario, int numeroItem, EstoqueDto estoque, EstoqueDto estoqueDevolvido, decimal quantidadeDevolver, decimal precoUnitarioGerarCredito, long numeroDocumento, string movimentacaoReferencia, bool cancelado)
		{
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Expected O, but got Unknown
			NumeroDocumento = numeroDocumento;
			ProdutoServicoEmpresaReferencia = produtoServicoEmpresaReferencia;
			Codigo = codigo;
			Descricao = descricao;
			Unidade = unidade;
			QuantidadeDevolver = quantidadeDevolver;
			Quantidade = estoque.Quantidade;
			PrecoUnitario = precoUnitario;
			EstoqueVenda = Clone.ObjectGraph<EstoqueDto>(estoque);
			NumeroItem = numeroItem;
			EstoqueDevolvido = estoqueDevolvido;
			QuantidadeMaxima = EstoqueVenda.Quantidade;
			TipoEstoque = ((HelperObject.IsNull((object)EstoqueVenda) || HelperObject.Is<EstoqueDtoNull>((object)EstoqueVenda)) ? null : ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel(estoqueDevolvido, EstoqueVenda)).ObterTipoEstoque().ViewModelBase);
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				tipoEstoque.ActionSetarQuantidade((Action)delegate
				{
					EstoqueDevolvido = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto;
					QuantidadeDevolver = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade;
				});
			}
			((ViewModelBase<EstoqueDto>)(object)TipoEstoque)?.ProjectedAsViewModel();
			PrecoUnitarioGerarCredito = precoUnitarioGerarCredito;
			MovimentacaoReferencia = movimentacaoReferencia;
			Cancelado = cancelado;
		}

		public ItemDevolucaoDto ObterDto()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			EstoqueDevolvido.Quantidade = QuantidadeDevolver;
			return new ItemDevolucaoDto
			{
				Estoque = EstoqueDevolvido,
				NumeroItem = NumeroItem,
				Descricao = Descricao,
				Codigo = Codigo,
				ProdutoServicoEmpresaReferencia = ProdutoServicoEmpresaReferencia,
				MovimentacaoReferencia = MovimentacaoReferencia,
				NumeroDocumento = NumeroDocumento,
				PrecoUnitario = PrecoUnitario,
				PrecoUnitarioGerarCredito = PrecoUnitarioGerarCredito
			};
		}
	}
	public class ItemDevolvidoViewModel : BindableBase
	{
		public EstoqueDto EstoqueVenda { get; set; }

		public EstoqueDto EstoqueDevolvido { get; set; }

		public TipoEstoqueBaseViewModel TipoEstoque { get; set; }

		public int NumeroItem { get; set; }

		public decimal Total => PrecoUnitario * EstoqueDevolvido.Quantidade;

		public string ToText => HelperString.RemoveDiacritics(string.Join(" ", Descricao, Codigo)).ToLower();

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

		public string ProdutoServicoEmpresaReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoEmpresaReferencia), value);
			}
		}

		public string Codigo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Codigo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Codigo), value);
			}
		}

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

		public decimal QuantidadeDevolver
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolver));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolver), value);
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

		public decimal PrecoUnitarioGerarCredito
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitarioGerarCredito));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitarioGerarCredito), value);
			}
		}

		public long NumeroDocumento
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => NumeroDocumento));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => NumeroDocumento), value);
			}
		}

		public string MovimentacaoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MovimentacaoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MovimentacaoReferencia), value);
			}
		}

		public bool PossuiPrecoBruto => PrecoUnitarioGerarCredito > 0m;

		public ItemDevolvidoViewModel()
		{
		}

		public ItemDevolvidoViewModel(string produtoServicoEmpresaReferencia, string codigo, string descricao, string unidade, decimal precoUnitario, int numeroItem, EstoqueDto estoque, EstoqueDto estoqueDevolvido, decimal precoUnitarioGerarCredito, long numeroDocumento, string movimentacaoReferencia)
		{
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			ProdutoServicoEmpresaReferencia = produtoServicoEmpresaReferencia;
			Codigo = codigo;
			Descricao = descricao;
			Unidade = unidade;
			QuantidadeDevolver = estoqueDevolvido.Quantidade;
			Quantidade = estoque.Quantidade;
			PrecoUnitario = precoUnitario;
			EstoqueVenda = Clone.ObjectGraph<EstoqueDto>(estoque);
			NumeroItem = numeroItem;
			EstoqueDevolvido = estoqueDevolvido;
			QuantidadeMaxima = EstoqueVenda.Quantidade;
			TipoEstoque = ((HelperObject.IsNull((object)EstoqueVenda) || HelperObject.Is<EstoqueDtoNull>((object)EstoqueVenda)) ? null : ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel(estoqueDevolvido, EstoqueVenda)).ObterTipoEstoque().ViewModelBase);
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				tipoEstoque.ActionSetarQuantidade((Action)delegate
				{
					EstoqueDevolvido = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto;
					QuantidadeDevolver = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade;
				});
			}
			((ViewModelBase<EstoqueDto>)(object)TipoEstoque)?.ProjectedAsViewModel();
			PrecoUnitarioGerarCredito = precoUnitarioGerarCredito;
			MovimentacaoReferencia = movimentacaoReferencia;
			NumeroDocumento = numeroDocumento;
		}

		public ItemDevolucaoDto ObterDto()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			EstoqueDevolvido.Quantidade = QuantidadeDevolver;
			return new ItemDevolucaoDto
			{
				Estoque = EstoqueDevolvido,
				NumeroItem = NumeroItem,
				Descricao = Descricao,
				Codigo = Codigo,
				ProdutoServicoEmpresaReferencia = ProdutoServicoEmpresaReferencia,
				MovimentacaoReferencia = MovimentacaoReferencia,
				NumeroDocumento = NumeroDocumento,
				PrecoUnitario = PrecoUnitario,
				PrecoUnitarioGerarCredito = PrecoUnitarioGerarCredito
			};
		}
	}
}
namespace Otus.Modulo.Devolucoes.ViewModels.Tipos
{
	public class DevolucaoVenda : TipoDevolucao
	{
		public override Type ViewModel => typeof(DevolucaoPadraoViewModel);

		public DevolucaoVenda()
			: base(0, "Devolução de venda")
		{
		}
	}
	public class DevolucaoDav : TipoDevolucao
	{
		public override Type ViewModel => typeof(DevolucaoDavViewModel);

		public DevolucaoDav()
			: base(1, "Devolução DAV/DAV-OS")
		{
		}
	}
	public abstract class TipoDevolucao
	{
		public int Codigo { get; protected set; }

		public string Descricao { get; protected set; }

		public abstract Type ViewModel { get; }

		protected TipoDevolucao(int codigo, string descricao)
		{
			Codigo = codigo;
			Descricao = descricao;
		}
	}
	public class TiposDevolucaoComboboxViewModel : Lista<TipoDevolucao>
	{
		private readonly DevolucaoDto _dto;

		public IDevolucaoViewModel ViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<IDevolucaoViewModel>((Expression<Func<IDevolucaoViewModel>>)(() => ViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<IDevolucaoViewModel>((Expression<Func<IDevolucaoViewModel>>)(() => ViewModel), value);
			}
		}

		public override bool Valido
		{
			get
			{
				IDevolucaoViewModel viewModel = ViewModel;
				if (viewModel == null)
				{
					return true;
				}
				return ((IViewModelBase)viewModel).Valido;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				ViewModelValidationBase val = (ViewModelValidationBase)ViewModel;
				return (((int)val != 0) ? ((Validation)val).Pendencies : null) ?? new string[0];
			}
		}

		public TiposDevolucaoComboboxViewModel(DevolucaoDto dto)
		{
			_dto = dto;
			base.AdicionarSemPosicionar((IEnumerable<TipoDevolucao>)new List<TipoDevolucao>
			{
				new DevolucaoVenda(),
				new DevolucaoDav()
			});
			if (dto.IsDevolucaoDav)
			{
				base.Posicionar((Expression<Func<TipoDevolucao, bool>>)((TipoDevolucao x) => x is DevolucaoDav));
			}
			else
			{
				base.Posicionar((Expression<Func<TipoDevolucao, bool>>)((TipoDevolucao x) => x is DevolucaoVenda));
			}
		}

		public void CriarViewModel(TipoDevolucao itemSelecionado)
		{
			DisposeViewModel();
			ViewModel = (IDevolucaoViewModel)Activator.CreateInstance(itemSelecionado.ViewModel, _dto);
			((IViewModelBase)ViewModel).ProjectedAsViewModel();
		}

		public void ProjectedAsViewModel()
		{
			((IViewModelBase)ViewModel).ProjectedAsViewModel();
		}

		public void AtualizarDto(ref DevolucaoDto dto)
		{
			IDevolucaoViewModel viewModel = ViewModel;
			if (viewModel != null)
			{
				((IViewModelDtoBase)viewModel).AtualizarDto<DevolucaoDto>(ref dto);
			}
		}

		private void DisposeViewModel()
		{
			if (ViewModel != null)
			{
				((IViewModelBase)ViewModel).Dispose();
			}
		}

		public override void Dispose()
		{
			DisposeViewModel();
			base.Dispose();
		}
	}
}
namespace Otus.Modulo.Devolucoes.Reports
{
	public class DevolucaoRelatorio
	{
		public Pessoa Empresa { get; set; }

		public DevolucaoDto Devolucao { get; set; }

		public DevolucaoRelatorio(Pessoa empresa, DevolucaoDto devolucao)
		{
			Empresa = empresa;
			Devolucao = devolucao;
		}

		public DevolucaoRelatorio()
		{
		}

		public static DevolucaoRelatorio Criar()
		{
			return new DevolucaoRelatorio();
		}

		public DevolucaoRelatorio ComEmpresa(Pessoa empresa)
		{
			Empresa = empresa;
			return Build();
		}

		public DevolucaoRelatorio ComDevolucao(DevolucaoDto devolucao)
		{
			Devolucao = devolucao;
			return Build();
		}

		protected DevolucaoRelatorio Build()
		{
			return new DevolucaoRelatorio(Empresa, Devolucao);
		}
	}
	public class ImpressoCredito56Mm57Mm58Mm : Relatorio
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRPanel xrPanel2;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel xrLabel5;

		private XRPanel xrPanel1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel6;

		private XRLabel xrLabel1;

		private FormattingRule formattingRule1;

		private XRPictureBox logotipoEmitente;

		private XRRichText dadosEmitente;

		private DetailBand Detail1;

		private ReportFooterBand ReportFooter;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel19;

		private XRLabel xrLabel15;

		private XRLabel xrLabel9;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private DetailReportBand itensDevolucao;

		private FormattingRule formattingRule2;

		public DevolucaoRelatorio DevolucaoRelatorio { get; set; }

		public ImpressoCredito56Mm57Mm58Mm(DevolucaoDto dto)
		{
			DevolucaoRelatorio = DevolucaoRelatorio.Criar().ComDevolucao(dto).ComEmpresa(DadosSistema.EmpresaLogada);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			ExibirLogotipo();
			((XtraReportBase)this).DataSource = new List<DevolucaoRelatorio> { DevolucaoRelatorio };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			if (!HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem))
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
			}
			((XRControl)dadosEmitente).Visible = HelperObject.IsNull((object)logotipoEmitente.Image);
			((XRControl)logotipoEmitente).Visible = !HelperObject.IsNull((object)logotipoEmitente.Image);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((XtraReport)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			//IL_0230: Unknown result type (might be due to invalid IL or missing references)
			//IL_0283: Unknown result type (might be due to invalid IL or missing references)
			//IL_0327: Unknown result type (might be due to invalid IL or missing references)
			//IL_0354: Unknown result type (might be due to invalid IL or missing references)
			//IL_0456: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_050a: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0806: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_095c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0962: Expected O, but got Unknown
			//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a62: Expected O, but got Unknown
			//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b84: Expected O, but got Unknown
			//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c94: Expected O, but got Unknown
			//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eea: Expected O, but got Unknown
			//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe6: Expected O, but got Unknown
			//IL_1025: Unknown result type (might be due to invalid IL or missing references)
			//IL_104e: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1121: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1229: Unknown result type (might be due to invalid IL or missing references)
			//IL_135d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1363: Expected O, but got Unknown
			//IL_13a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1478: Unknown result type (might be due to invalid IL or missing references)
			//IL_147e: Expected O, but got Unknown
			//IL_14bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_14e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1626: Unknown result type (might be due to invalid IL or missing references)
			//IL_1650: Unknown result type (might be due to invalid IL or missing references)
			//IL_1732: Unknown result type (might be due to invalid IL or missing references)
			//IL_175c: Unknown result type (might be due to invalid IL or missing references)
			//IL_183d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1867: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoCredito56Mm57Mm58Mm));
			Detail = new DetailBand();
			logotipoEmitente = new XRPictureBox();
			dadosEmitente = new XRRichText();
			xrPanel2 = new XRPanel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel12 = new XRLabel();
			formattingRule1 = new FormattingRule();
			xrLabel11 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrPanel1 = new XRPanel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel1 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			itensDevolucao = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel15 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel19 = new XRLabel();
			formattingRule2 = new FormattingRule();
			ReportFooter = new ReportFooterBand();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)dadosEmitente).BeginInit();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)logotipoEmitente,
				(XRControl)dadosEmitente,
				(XRControl)xrPanel2,
				(XRControl)xrPanel1
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail).HeightF = 1142.375f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)logotipoEmitente).Borders = (BorderSide)0;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(35.99963f, 35.19189f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(500.0002f, 209.3289f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).Borders = (BorderSide)0;
			((XRControl)dadosEmitente).CanGrow = false;
			((XRControl)dadosEmitente).Dpi = 254f;
			((XRControl)dadosEmitente).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)dadosEmitente).LocationFloat = new PointFloat(35.99979f, 35.19189f);
			((XRControl)dadosEmitente).Name = "dadosEmitente";
			((XRControl)dadosEmitente).Padding = new PaddingInfo(20, 20, 20, 20, 254f);
			((XRRichTextBase)dadosEmitente).SerializableRtfString = componentResourceManager.GetString("dadosEmitente.SerializableRtfString");
			((XRControl)dadosEmitente).SizeF = new SizeF(500.0002f, 209.3289f);
			((XRControl)dadosEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).StylePriority.UseFont = false;
			((XRControl)dadosEmitente).StylePriority.UsePadding = false;
			((XRControl)xrPanel2).Borders = (BorderSide)15;
			((XRControl)xrPanel2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel14,
				(XRControl)xrLabel13,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)xrLabel5
			});
			((XRControl)xrPanel2).Dpi = 254f;
			((XRControl)xrPanel2).LocationFloat = new PointFloat(35.99963f, 552.6458f);
			((XRControl)xrPanel2).Name = "xrPanel2";
			((XRControl)xrPanel2).SizeF = new SizeF(500.0004f, 555.3333f);
			((XRControl)xrPanel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).Borders = (BorderSide)0;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(3.000397f, 450.8734f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(490.0002f, 49.18909f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Valor";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel13).Borders = (BorderSide)0;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(3.000397f, 326.4775f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(490.0002f, 49.18909f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "Data devolução";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Borders = (BorderSide)0;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel12).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(3.000305f, 199.6026f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(490.0003f, 49.18903f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Data emissão";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Visible = false;
			formattingRule1.Condition = "[Devolucao.TipoDevolucao] == 0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)0;
			formattingRule1.Name = "formattingRule1";
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(3.000305f, 74.2068f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(490.0002f, 49.18906f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Documento(s) devolvido(s)";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel10).Borders = (BorderSide)0;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).LocationFloat = new PointFloat(3.000313f, 14f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(490.0001f, 49.18909f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "DEVOLUÇÃO";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDescricao")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(3.000305f, 123.3959f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(490.0002f, 58.42004f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel3).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(3.000397f, 248.7916f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(490.0002f, 58.42004f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).Visible = false;
			((XRControl)xrLabel4).Borders = (BorderSide)0;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.DataInclusao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(3.000397f, 375.6667f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(490.0002f, 58.41992f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.TotalGerarCredito", "{0:n2}")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 14f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(3.000397f, 500.0624f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(490.0002f, 58.41992f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)2;
			((XRControl)xrPanel1).Borders = (BorderSide)15;
			((XRControl)xrPanel1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel6
			});
			((XRControl)xrPanel1).Dpi = 254f;
			((XRControl)xrPanel1).LocationFloat = new PointFloat(35.99979f, 272f);
			((XRControl)xrPanel1).Name = "xrPanel1";
			((XRControl)xrPanel1).SizeF = new SizeF(500.0002f, 259.2917f);
			((XRControl)xrPanel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(3.000144f, 25.00001f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6875f, 49.1891f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "CLIENTE";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaDocumento")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(3.000144f, 162.8542f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(490.0002f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaNome")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(3.000144f, 88.55923f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(490f, 58.41997f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel1).Borders = (BorderSide)2;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(35.99963f, 60.29186f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(500f, 59.77246f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Autenticação";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 0f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XtraReportBase)itensDevolucao).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)itensDevolucao).DataMember = "Devolucao.Itens";
			((XRControl)itensDevolucao).Dpi = 254f;
			((XtraReportBase)itensDevolucao).FilterString = "[Cancelado] = False";
			((Collection<FormattingRule>)(object)((XRControl)itensDevolucao).FormattingRules).Add(formattingRule2);
			itensDevolucao.Level = 0;
			((XRControl)itensDevolucao).Name = "itensDevolucao";
			((XRControl)itensDevolucao).Visible = false;
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel17,
				(XRControl)xrLabel16
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 52.42004f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel17).Borders = (BorderSide)12;
			((XRControl)xrLabel17).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Estoque.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(377.25f, 0f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(158.75f, 52.42004f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel16).Borders = (BorderSide)13;
			((XRControl)xrLabel16).CanGrow = false;
			((XRControl)xrLabel16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Descricao")
			});
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 9f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(35.99963f, 0f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(341.2503f, 52.42004f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UsePadding = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel16).WordWrap = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel15,
				(XRControl)xrLabel9,
				(XRControl)xrLabel19
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 80.37797f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel15).Borders = (BorderSide)14;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel15).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(377.2499f, 40.18899f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(158.7496f, 40.18898f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "Qtd. Dev.";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel9).Borders = (BorderSide)15;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel9).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(36.00015f, 40.18892f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(341.2498f, 40.18899f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UsePadding = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Descrição";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel19).Borders = (BorderSide)7;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 9f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel19).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(35.99979f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(499.9998f, 40.18899f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UsePadding = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).Text = "ITENS";
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)32;
			formattingRule2.Condition = "[Devolucao.IsDevolucaoDav] == True";
			formattingRule2.Formatting.Visible = (DefaultBoolean)0;
			formattingRule2.Name = "formattingRule2";
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel1 });
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 120.0643f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			bindingSource1.DataSource = typeof(DevolucaoRelatorio);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)itensDevolucao,
				(Band)ReportFooter
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { formattingRule1, formattingRule2 });
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 570;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)dadosEmitente).EndInit();
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoCredito79Mm80Mm : Relatorio
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private FormattingRule formattingRule1;

		private XRLabel xrLabel1;

		private XRRichText dadosEmitente;

		private XRPictureBox logotipoEmitente;

		private XRLabel xrLabel9;

		private ReportFooterBand ReportFooter1;

		private XRLabel lblMensagemRodape;

		private XRCrossBandLine xrCrossBandLine4;

		private XRPanel xrPanel2;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel xrLabel5;

		private XRPanel xrPanel1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel6;

		private DetailReportBand itensDevolucao;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel19;

		private XRLabel xrLabel15;

		private XRLabel xrLabel16;

		private XRLabel xrLabel17;

		private XRLabel xrLabel18;

		private FormattingRule formattingRule2;

		public DevolucaoRelatorio DevolucaoRelatorio { get; set; }

		public ImpressoCredito79Mm80Mm(DevolucaoDto dto)
		{
			DevolucaoRelatorio = DevolucaoRelatorio.Criar().ComDevolucao(dto).ComEmpresa(DadosSistema.EmpresaLogada);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			ExibirLogotipo();
			((XtraReportBase)this).DataSource = new List<DevolucaoRelatorio> { DevolucaoRelatorio };
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			if (!HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem))
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
			}
			((XRControl)dadosEmitente).Visible = HelperObject.IsNull((object)logotipoEmitente.Image);
			((XRControl)logotipoEmitente).Visible = !HelperObject.IsNull((object)logotipoEmitente.Image);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((XtraReport)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Expected O, but got Unknown
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Expected O, but got Unknown
			//IL_025a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0316: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0567: Unknown result type (might be due to invalid IL or missing references)
			//IL_0590: Unknown result type (might be due to invalid IL or missing references)
			//IL_066c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0695: Unknown result type (might be due to invalid IL or missing references)
			//IL_0781: Unknown result type (might be due to invalid IL or missing references)
			//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0848: Unknown result type (might be due to invalid IL or missing references)
			//IL_084e: Expected O, but got Unknown
			//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_095e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0964: Expected O, but got Unknown
			//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a70: Expected O, but got Unknown
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b80: Expected O, but got Unknown
			//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e02: Expected O, but got Unknown
			//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0efe: Expected O, but got Unknown
			//IL_0f3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_101d: Unknown result type (might be due to invalid IL or missing references)
			//IL_104a: Unknown result type (might be due to invalid IL or missing references)
			//IL_10e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_117c: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_125f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1288: Unknown result type (might be due to invalid IL or missing references)
			//IL_133b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1390: Unknown result type (might be due to invalid IL or missing references)
			//IL_145c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1491: Unknown result type (might be due to invalid IL or missing references)
			//IL_1525: Unknown result type (might be due to invalid IL or missing references)
			//IL_153f: Unknown result type (might be due to invalid IL or missing references)
			//IL_157a: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16bf: Expected O, but got Unknown
			//IL_1701: Unknown result type (might be due to invalid IL or missing references)
			//IL_172b: Unknown result type (might be due to invalid IL or missing references)
			//IL_17dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_17e2: Expected O, but got Unknown
			//IL_1824: Unknown result type (might be due to invalid IL or missing references)
			//IL_184e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1980: Unknown result type (might be due to invalid IL or missing references)
			//IL_19aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b98: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bc2: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoCredito79Mm80Mm));
			Detail = new DetailBand();
			xrPanel2 = new XRPanel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel11 = new XRLabel();
			formattingRule1 = new FormattingRule();
			xrLabel10 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrPanel1 = new XRPanel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel6 = new XRLabel();
			dadosEmitente = new XRRichText();
			logotipoEmitente = new XRPictureBox();
			xrLabel9 = new XRLabel();
			xrLabel1 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			ReportFooter1 = new ReportFooterBand();
			lblMensagemRodape = new XRLabel();
			xrCrossBandLine4 = new XRCrossBandLine();
			itensDevolucao = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel17 = new XRLabel();
			xrLabel18 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel19 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel16 = new XRLabel();
			formattingRule2 = new FormattingRule();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)dadosEmitente).BeginInit();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrPanel2,
				(XRControl)xrPanel1,
				(XRControl)dadosEmitente,
				(XRControl)logotipoEmitente,
				(XRControl)xrLabel9
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail).HeightF = 1200.948f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrPanel2).Borders = (BorderSide)15;
			((XRControl)xrPanel2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel14,
				(XRControl)xrLabel13,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)xrLabel5
			});
			((XRControl)xrPanel2).Dpi = 254f;
			((XRControl)xrPanel2).LocationFloat = new PointFloat(49.99999f, 597.083f);
			((XRControl)xrPanel2).Name = "xrPanel2";
			((XRControl)xrPanel2).SizeF = new SizeF(594.8334f, 571.1849f);
			((XRControl)xrPanel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).Borders = (BorderSide)0;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(25.00002f, 454.1305f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(544.8337f, 49.18909f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Valor";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel13).Borders = (BorderSide)0;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(24f, 327.9592f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(544.8334f, 49.18909f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "Data devolução";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Borders = (BorderSide)0;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(24.00007f, 226.2297f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(545.8336f, 49.18903f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Data emissão";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Visible = false;
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel11).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(24f, 85.62799f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(545.8336f, 49.18915f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Documento(s) devolvido(s)";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)32;
			formattingRule1.Condition = "[Devolucao.TipoDevolucao] == 0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)0;
			formattingRule1.Name = "formattingRule1";
			((XRControl)xrLabel10).Borders = (BorderSide)0;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(25.00002f, 25.00012f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(544.8337f, 49.18903f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "DEVOLUÇÃO";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDescricao")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 12f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel2).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(24f, 134.8171f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(545.8336f, 91.41272f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(24.00049f, 275.4187f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(545.8331f, 52.54053f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).Visible = false;
			((XRControl)xrLabel4).Borders = (BorderSide)0;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.DataInclusao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(24.00006f, 377.1483f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(544.8336f, 52.54028f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.TotalGerarCredito", "{0:n2}")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(24.99994f, 503.3196f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(544.8337f, 52.54028f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)2;
			((XRControl)xrPanel1).Borders = (BorderSide)15;
			((XRControl)xrPanel1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel6
			});
			((XRControl)xrPanel1).Dpi = 254f;
			((XRControl)xrPanel1).LocationFloat = new PointFloat(49.99999f, 369f);
			((XRControl)xrPanel1).Name = "xrPanel1";
			((XRControl)xrPanel1).SizeF = new SizeF(594.8334f, 198.083f);
			((XRControl)xrPanel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(25.00002f, 14.71065f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6875f, 49.1891f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "CLIENTE";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaDocumento")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(24.99984f, 122.3197f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(544.8336f, 58.42004f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaNome")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(24.99984f, 63.89972f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(544.8337f, 58.41998f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)32;
			((XRControl)dadosEmitente).Borders = (BorderSide)0;
			((XRControl)dadosEmitente).CanGrow = false;
			((XRControl)dadosEmitente).Dpi = 254f;
			((XRControl)dadosEmitente).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)dadosEmitente).LocationFloat = new PointFloat(71.43744f, 26.17426f);
			((XRControl)dadosEmitente).Name = "dadosEmitente";
			((XRControl)dadosEmitente).Padding = new PaddingInfo(20, 20, 20, 20, 254f);
			((XRRichTextBase)dadosEmitente).SerializableRtfString = componentResourceManager.GetString("dadosEmitente.SerializableRtfString");
			((XRControl)dadosEmitente).SizeF = new SizeF(544.834f, 210.8005f);
			((XRControl)dadosEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).StylePriority.UseFont = false;
			((XRControl)dadosEmitente).StylePriority.UsePadding = false;
			((XRControl)logotipoEmitente).Borders = (BorderSide)0;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(71.43746f, 25.00001f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(544.834f, 211.9748f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).Borders = (BorderSide)15;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(49.99999f, 270.1964f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(594.8334f, 69.03284f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "COMPROVANTE DE DEVOLUÇÃO";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel1).Borders = (BorderSide)2;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(46.43747f, 83.13575f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(598.3959f, 59.77246f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Autenticação";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 28f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 50f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)ReportFooter1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel1
			});
			((XRControl)ReportFooter1).Dpi = 254f;
			((XRControl)ReportFooter1).HeightF = 245.8754f;
			((XRControl)ReportFooter1).Name = "ReportFooter1";
			ReportFooter1.PrintAtBottom = true;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)0;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(4.083306f, 203.7624f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(684.9167f, 42.113f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrCrossBandLine4).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine4).EndBand = (Band)(object)TopMargin;
			((XRCrossBandControl)xrCrossBandLine4).EndPointFloat = new PointFloat(337.5f, 0f);
			((XRControl)xrCrossBandLine4).LocationFloat = new PointFloat(337.5f, 312.5f);
			((XRControl)xrCrossBandLine4).Name = "xrCrossBandLine4";
			((XRCrossBandControl)xrCrossBandLine4).StartBand = (Band)(object)Detail;
			((XRCrossBandControl)xrCrossBandLine4).StartPointFloat = new PointFloat(337.5f, 312.5f);
			((XRControl)xrCrossBandLine4).WidthF = 4.227071f;
			((XtraReportBase)itensDevolucao).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)itensDevolucao).DataMember = "Devolucao.Itens";
			((XRControl)itensDevolucao).Dpi = 254f;
			((XtraReportBase)itensDevolucao).FilterString = "[Cancelado] = False";
			((Collection<FormattingRule>)(object)((XRControl)itensDevolucao).FormattingRules).Add(formattingRule2);
			itensDevolucao.Level = 0;
			((XRControl)itensDevolucao).Name = "itensDevolucao";
			((XRControl)itensDevolucao).Visible = false;
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel17,
				(XRControl)xrLabel18
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 60.42004f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel17).Borders = (BorderSide)13;
			((XRControl)xrLabel17).CanGrow = false;
			((XRControl)xrLabel17).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Descricao")
			});
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(50.0004f, 0f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(376.9584f, 60.42004f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel17).WordWrap = false;
			((XRControl)xrLabel18).Borders = (BorderSide)12;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Estoque.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(426.9588f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(217.8745f, 60.42004f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UsePadding = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)32;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel19,
				(XRControl)xrLabel15,
				(XRControl)xrLabel16
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 80.37791f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel19).Borders = (BorderSide)7;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel19).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(50.00036f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(594.833f, 40.18899f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UsePadding = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).Text = "ITENS";
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel15).Borders = (BorderSide)15;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel15).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(50.00035f, 40.18892f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(376.9584f, 40.18899f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "Descrição";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel16).Borders = (BorderSide)14;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel16).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(426.9588f, 40.18892f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(217.8745f, 40.18899f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UsePadding = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).Text = "Qtd. Dev.";
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)32;
			formattingRule2.Condition = "[Devolucao.IsDevolucaoDav] == True";
			formattingRule2.Formatting.Visible = (DefaultBoolean)0;
			formattingRule2.Name = "formattingRule2";
			bindingSource1.DataSource = typeof(DevolucaoRelatorio);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)ReportFooter1,
				(Band)itensDevolucao
			});
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[1] { (XRCrossBandControl)xrCrossBandLine4 });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { formattingRule1, formattingRule2 });
			((XtraReport)this).Margins = new Margins(49, 57, 28, 50);
			((XtraReport)this).PageHeight = 2972;
			((XtraReport)this).PageWidth = 795;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)dadosEmitente).EndInit();
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoCredito : Relatorio
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRPanel xrPanel2;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel xrLabel5;

		private XRPanel xrPanel1;

		private XRLabel xrLabel9;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel6;

		private XRLabel xrLabel1;

		private FormattingRule formattingRule1;

		private XRPictureBox logotipoEmitente;

		private XRRichText dadosEmitente;

		private XRLabel lblMensagemRodape;

		private XRPanel xrPanel3;

		private DetailReportBand itensDevolucao;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel16;

		private XRLabel xrLabel17;

		private XRLabel xrLabel18;

		private XRLabel xrLabel15;

		private XRLabel xrLabel19;

		private FormattingRule formattingRule2;

		public DevolucaoRelatorio DevolucaoRelatorio { get; set; }

		public ImpressoCredito(DevolucaoDto dto)
		{
			DevolucaoRelatorio = DevolucaoRelatorio.Criar().ComDevolucao(dto).ComEmpresa(DadosSistema.EmpresaLogada);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			ExibirLogotipo();
			((XtraReportBase)this).DataSource = new List<DevolucaoRelatorio> { DevolucaoRelatorio };
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			if (!HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem))
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
			}
			((XRControl)dadosEmitente).Visible = HelperObject.IsNull((object)logotipoEmitente.Image);
			((XRControl)logotipoEmitente).Visible = !HelperObject.IsNull((object)logotipoEmitente.Image);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((XtraReport)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Expected O, but got Unknown
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Expected O, but got Unknown
			//IL_025a: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0346: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Unknown result type (might be due to invalid IL or missing references)
			//IL_0475: Unknown result type (might be due to invalid IL or missing references)
			//IL_0500: Unknown result type (might be due to invalid IL or missing references)
			//IL_0529: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_060c: Unknown result type (might be due to invalid IL or missing references)
			//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0705: Unknown result type (might be due to invalid IL or missing references)
			//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0825: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_090b: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09af: Expected O, but got Unknown
			//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a92: Expected O, but got Unknown
			//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb4: Expected O, but got Unknown
			//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb4: Expected O, but got Unknown
			//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff6: Expected O, but got Unknown
			//IL_1035: Unknown result type (might be due to invalid IL or missing references)
			//IL_105e: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f2: Expected O, but got Unknown
			//IL_1131: Unknown result type (might be due to invalid IL or missing references)
			//IL_115a: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_128d: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_136b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1394: Unknown result type (might be due to invalid IL or missing references)
			//IL_1447: Unknown result type (might be due to invalid IL or missing references)
			//IL_149c: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d6: Expected O, but got Unknown
			//IL_1615: Unknown result type (might be due to invalid IL or missing references)
			//IL_163e: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_16df: Expected O, but got Unknown
			//IL_171e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1748: Unknown result type (might be due to invalid IL or missing references)
			//IL_1886: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1988: Unknown result type (might be due to invalid IL or missing references)
			//IL_19b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a96: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoCredito));
			Detail = new DetailBand();
			xrPanel3 = new XRPanel();
			dadosEmitente = new XRRichText();
			xrPanel2 = new XRPanel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel12 = new XRLabel();
			formattingRule1 = new FormattingRule();
			xrLabel11 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrPanel1 = new XRPanel();
			xrLabel9 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel6 = new XRLabel();
			logotipoEmitente = new XRPictureBox();
			lblMensagemRodape = new XRLabel();
			xrLabel1 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			itensDevolucao = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel18 = new XRLabel();
			xrLabel15 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel19 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel17 = new XRLabel();
			formattingRule2 = new FormattingRule();
			ReportFooter = new ReportFooterBand();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)dadosEmitente).BeginInit();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrPanel3,
				(XRControl)dadosEmitente,
				(XRControl)xrPanel2,
				(XRControl)xrPanel1,
				(XRControl)logotipoEmitente
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail).HeightF = 645.3975f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrPanel3).Borders = (BorderSide)15;
			((XRControl)xrPanel3).Dpi = 254f;
			((XRControl)xrPanel3).LocationFloat = new PointFloat(43.72852f, 54.65917f);
			((XRControl)xrPanel3).Name = "xrPanel3";
			((XRControl)xrPanel3).SizeF = new SizeF(589.8132f, 257.8333f);
			((XRControl)xrPanel3).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).Borders = (BorderSide)0;
			((XRControl)dadosEmitente).CanGrow = false;
			((XRControl)dadosEmitente).Dpi = 254f;
			((XRControl)dadosEmitente).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)dadosEmitente).LocationFloat = new PointFloat(43.7285f, 54.65917f);
			((XRControl)dadosEmitente).Name = "dadosEmitente";
			((XRControl)dadosEmitente).Padding = new PaddingInfo(20, 20, 20, 20, 254f);
			((XRRichTextBase)dadosEmitente).SerializableRtfString = componentResourceManager.GetString("dadosEmitente.SerializableRtfString");
			((XRControl)dadosEmitente).SizeF = new SizeF(589.8132f, 261.9374f);
			((XRControl)dadosEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).StylePriority.UseFont = false;
			((XRControl)dadosEmitente).StylePriority.UsePadding = false;
			((XRControl)xrPanel2).Borders = (BorderSide)15;
			((XRControl)xrPanel2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel14,
				(XRControl)xrLabel13,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)xrLabel5
			});
			((XRControl)xrPanel2).Dpi = 254f;
			((XRControl)xrPanel2).LocationFloat = new PointFloat(43.7285f, 349.8333f);
			((XRControl)xrPanel2).Name = "xrPanel2";
			((XRControl)xrPanel2).SizeF = new SizeF(1824.271f, 253.7084f);
			((XRControl)xrPanel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).Borders = (BorderSide)0;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1485.168f, 105.2068f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(314.1033f, 49.18906f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Valor";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel13).Borders = (BorderSide)0;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(1178.251f, 105.207f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(306.9167f, 49.189f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "Data devolução";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Borders = (BorderSide)0;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 12f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel12).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(871.3342f, 105.2069f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(306.9167f, 49.189f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Data emissão";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel12).Visible = false;
			formattingRule1.Condition = "[Devolucao.TipoDevolucao] == 0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)0;
			formattingRule1.Name = "formattingRule1";
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(25.00001f, 105.2068f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(534.4583f, 49.18909f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Documento(s) devolvido(s)";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel10).Borders = (BorderSide)0;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(25.00001f, 25.00006f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(592.6667f, 49.18912f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "COMPROVANTE DEVOLUÇÃO";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDescricao")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(25.00001f, 154.3958f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(762f, 58.42004f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.MovimentacaoDataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 12f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel3).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(871.3342f, 154.3959f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(306.9167f, 58.42004f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).Visible = false;
			((XRControl)xrLabel4).Borders = (BorderSide)0;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.DataInclusao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1178.251f, 154.396f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(306.9167f, 58.41995f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.TotalGerarCredito", "{0:n2}")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1485.168f, 154.3959f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(314.1033f, 58.41998f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)2;
			((XRControl)xrPanel1).Borders = (BorderSide)15;
			((XRControl)xrPanel1).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel9,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel6
			});
			((XRControl)xrPanel1).Dpi = 254f;
			((XRControl)xrPanel1).LocationFloat = new PointFloat(663.7086f, 54.65917f);
			((XRControl)xrPanel1).Name = "xrPanel1";
			((XRControl)xrPanel1).SizeF = new SizeF(1204.291f, 257.8333f);
			((XRControl)xrPanel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).Borders = (BorderSide)0;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(25.00001f, 146.9792f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(251.3542f, 58.42001f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Documento:";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(25.00001f, 25.00001f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6875f, 49.1891f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "CLIENTE";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaDocumento")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(276.3542f, 146.9792f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(893.7708f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.PessoaNome")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(25.00001f, 88.55922f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(1145.125f, 58.41998f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)16;
			((XRControl)logotipoEmitente).Borders = (BorderSide)0;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(43.7285f, 54.65917f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(589.8132f, 261.9374f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)0;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(43.7285f, 198.9929f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1824.271f, 42.11298f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel1).Borders = (BorderSide)2;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 14f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(229.565f, 112.8543f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1447.563f, 64.75159f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Autenticação";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 0f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XtraReportBase)itensDevolucao).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)itensDevolucao).DataMember = "Devolucao.Itens";
			((XRControl)itensDevolucao).Dpi = 254f;
			((XtraReportBase)itensDevolucao).FilterString = "[Cancelado] = False";
			((Collection<FormattingRule>)(object)((XRControl)itensDevolucao).FormattingRules).Add(formattingRule2);
			itensDevolucao.Level = 0;
			((XRControl)itensDevolucao).Name = "itensDevolucao";
			((XRControl)itensDevolucao).Visible = false;
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel18,
				(XRControl)xrLabel15
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 62.29498f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel18).Borders = (BorderSide)12;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Estoque.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(1383.979f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(484.021f, 62.29498f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel15).Borders = (BorderSide)13;
			((XRControl)xrLabel15).CanGrow = false;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Devolucao.Itens.Descricao")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(43.72852f, 0f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(1340.25f, 62.29498f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel15).WordWrap = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel19,
				(XRControl)xrLabel16,
				(XRControl)xrLabel17
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 89.37801f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel19).Borders = (BorderSide)7;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 12f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel19).FormattingRules).Add(formattingRule1);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(43.72857f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(1824.271f, 40.189f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UsePadding = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).Text = "ITENS";
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel19).Visible = false;
			((XRControl)xrLabel16).Borders = (BorderSide)14;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1383.979f, 40.189f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(484.0209f, 49.18901f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).Text = "Quantidade devolvida";
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel17).Borders = (BorderSide)15;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(43.72856f, 40.189f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(1340.25f, 49.189f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Descrição";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)16;
			formattingRule2.Condition = "[Devolucao.IsDevolucaoDav] == True";
			formattingRule2.Formatting.Visible = (DefaultBoolean)0;
			formattingRule2.Name = "formattingRule2";
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel1
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 241.1059f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			bindingSource1.DataSource = typeof(DevolucaoRelatorio);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)itensDevolucao,
				(Band)ReportFooter
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[2] { formattingRule1, formattingRule2 });
			((XtraReport)this).Margins = new Margins(0, 8, 0, 0);
			((XtraReport)this).PageHeight = 2667;
			((XtraReport)this).PageWidth = 1918;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)dadosEmitente).EndInit();
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.Devolucoes.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Devolucoes.Properties.Resources", typeof(Resources).Assembly);
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
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;
	}
}
namespace Otus.Modulo.Devolucoes.Interfaces
{
	public interface IDevolucaoViewModel : IViewModelDtoBase, IViewModelBase
	{
	}
}

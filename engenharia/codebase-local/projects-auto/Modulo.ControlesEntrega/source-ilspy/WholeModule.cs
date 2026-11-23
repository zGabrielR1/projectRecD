using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Marcas;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Paises;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
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
using DigisatServer.Infrastructure.Crosscutting.Service.ConsultaCep;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.ControlesEntrega.Reports;
using Otus.Modulo.ControlesEntrega.Resources;
using Otus.Modulo.ControlesEntrega.ViewModels.Pessoa;
using Otus.Modulo.ControlesEntrega.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.ControlesEntrega")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.ControlesEntrega")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.ControlesEntrega
{
	[Module(ModuleName = "ControleEntregaModulo", OnDemand = true)]
	[ModuleDependency("MovimentacaoModulo")]
	public class ControleEntregaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ControleEntregaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ControlesEntregaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ControleEntregaListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ControleEntregaFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ControleEntregaFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<RegistroEntregaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.ControlesEntrega.Views
{
	public class ControleEntregaFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public ControleEntregaFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaadicionarview.xaml", UriKind.Relative);
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
	public class ControleEntregaFichaAtualizarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public ControleEntregaFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaatualizarview.xaml", UriKind.Relative);
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
	public class ControleEntregaListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal ListViewFocusable lstResultadosPesquisa;

		internal Grid DetalheLateral;

		internal System.Windows.Controls.Button btnDetalhes;

		internal KeyBinding kbdExibir;

		private bool _contentLoaded;

		public ICommand ExibirCommand { get; set; }

		public ControleEntregaListaView()
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			InitializeComponent();
			ExibirCommand = (ICommand)new DelegateCommand((Action)Exibir);
			kbdExibir.Command = ExibirCommand;
			btnDetalhes.DataContext = this;
		}

		public void Exibir()
		{
			if (btnDetalhes.RenderTransform is RotateTransform rotateTransform)
			{
				if (Math.Abs(rotateTransform.Angle) <= 0.0)
				{
					DetalheLateral.Width = 500.0;
					rotateTransform.Angle += 180.0;
				}
				else
				{
					DetalheLateral.Width = 35.0;
					rotateTransform.Angle = 0.0;
				}
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregalistaview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				lstResultadosPesquisa = (ListViewFocusable)target;
				break;
			case 2:
				DetalheLateral = (Grid)target;
				break;
			case 3:
				btnDetalhes = (System.Windows.Controls.Button)target;
				break;
			case 4:
				kbdExibir = (KeyBinding)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class ControlesEntregaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ControlesEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controlesentregaview.xaml", UriKind.Relative);
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
	public class LinkImprimirGarantiaEntregaPendente : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkImprimirGarantiaEntregaPendente()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/linkimprimirgarantiaentregapendente.xaml", UriKind.Relative);
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/linkimprimir.xaml", UriKind.Relative);
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
	public class LinkRegistrarEntrega : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkRegistrarEntrega()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/linkregistrarentrega.xaml", UriKind.Relative);
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
	public class RegistroEntregaFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public RegistroEntregaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/registroentregafichaview.xaml", UriKind.Relative);
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
}
namespace Otus.Modulo.ControlesEntrega.Views.ControleEntregaFichaViewDetalhes
{
	public class ControleEntregaView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterMovimentacao;

		private bool _contentLoaded;

		public ControleEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaviewdetalhes/controleentregaview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			if (connectionId == 1)
			{
				ParameterMovimentacao = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class ItensControleEntregaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ItensControleEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaviewdetalhes/itenscontroleentregaview.xaml", UriKind.Relative);
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
	public class ObservacaoControleEntregaView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ObservacaoControleEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaviewdetalhes/observacaocontroleentregaview.xaml", UriKind.Relative);
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
	public class PessoaDetalheView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterPessoa;

		private bool _contentLoaded;

		public PessoaDetalheView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaviewdetalhes/pessoadetalheview.xaml", UriKind.Relative);
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
				ParameterPessoa = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class RegistroEntregaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public RegistroEntregaView()
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
				Uri resourceLocator = new Uri("/Modulo.ControlesEntrega;component/views/controleentregafichaviewdetalhes/registroentregaview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
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
}
namespace Otus.Modulo.ControlesEntrega.ViewModels
{
	public class ControleEntregaAbaViewModel : AbaPrincipalViewModelBase
	{
		public ControleEntregaAbaViewModel(IViewModelPrincipal viewModel)
			: base("CONTROLE DE ENTREGA", "Dados sobre a entrega", viewModel)
		{
		}
	}
	public class ControleEntregaFichaAdicionarViewModel : FichaBaseAdicionar<ControleEntregaDto, ControleEntregaViewModel, IControleEntregaModuleService>
	{
		private MovimentacaoDto _movimentacaoDto;

		public ControleEntregaFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			ref MovimentacaoDto movimentacaoDto = ref _movimentacaoDto;
			object obj = navigationParameters["MovimentacaoDto"];
			movimentacaoDto = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			((FichaBasePersistir<ControleEntregaDto, ControleEntregaViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			if (HelperObject.IsNotNull((object)_movimentacaoDto))
			{
				((Base)this).Titulo = "Inclusão: Controle de entrega";
				((FichaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Formulario = (IFormulario)(object)new ControleEntregaFormulario(new ControleEntregaDto(), _movimentacaoDto);
			}
			else
			{
				((FichaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).CriarFormulario();
			}
		}

		protected override async Task<ControleEntregaDto> Persistir(IFormularioEntity formulario)
		{
			ControleEntregaDto val = await base.Persistir(formulario);
			if (HelperObject.IsNotNull((object)_movimentacaoDto) && DialogoFactory.MostrarDialogoSimNao("Deseja imprimir o extrato do controle de entrega?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				Relatorio val2 = null;
				if (DadosSistema.ConfiguracaoDto.Saida.UsarImpresso40ColunasControleEntrega.Valor)
				{
					if (val.Situacao.Codigo == 1)
					{
						val2 = (Relatorio)(object)new ImpressoControleEntregaPendente40Colunas(val);
					}
					if (val.Situacao.Codigo == 3)
					{
						val2 = (Relatorio)(object)new ImpressoControleEntregaConcluido40Colunas(val);
					}
					if (val2 != null)
					{
						XtraReportExtensions.ShowPreviewDialog((IReport)(object)val2.Processar());
					}
				}
				else
				{
					if (val.Situacao.Codigo == 1)
					{
						val2 = (Relatorio)(object)new ImpressoControleEntregaPendente(val);
					}
					if (val.Situacao.Codigo == 3)
					{
						val2 = (Relatorio)(object)new ImpressoControleEntregaConcluido(val);
					}
					if (val2 != null)
					{
						XtraReportExtensions.ShowPreviewDialog((IReport)(object)val2.Processar());
					}
				}
			}
			return val;
		}
	}
	public class ControleEntregaFichaAtualizarViewModel : FichaBaseAtualizar<ControleEntregaDto, ControleEntregaViewModel, IControleEntregaModuleService>
	{
		public ControleEntregaFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class ControleEntregaFormulario : FormularioEntity<ControleEntregaDto>
	{
		private ControleEntregaViewModel _viewModel;

		private ObservacaoControleEntregaAbaViewModel _viewModelObservacao;

		public ControleEntregaFormulario(ControleEntregaDto dto, MovimentacaoDto movimentacaoDto = null)
			: base(dto)
		{
			CriarAbas();
			if (HelperObject.IsNotNull((object)movimentacaoDto))
			{
				_viewModel.SetarMovimentacao(movimentacaoDto);
				_viewModel.AdicionarMovimentacaoEMostrarFicha();
			}
		}

		private void CriarAbas()
		{
			_viewModel = new ControleEntregaViewModel(base.Dto);
			((BindableBase)_viewModel).PropertyChanged += ViewModelOnPropertyChanged;
			_viewModelObservacao = new ObservacaoControleEntregaAbaViewModel((IViewModelDtoBase)(object)new ObservacaoControleEntregaViewModel(base.Dto));
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new ControleEntregaAbaViewModel((IViewModelPrincipal)(object)_viewModel),
				(IAbaViewModelBase)_viewModelObservacao
			});
		}

		private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName.Equals("Observacao"))
			{
				HelperObject.ToDo<ObservacaoControleEntregaViewModel>((object)((AbaViewModelBase)_viewModelObservacao).ViewModel, (Action<ObservacaoControleEntregaViewModel>)delegate(ObservacaoControleEntregaViewModel x)
				{
					x.Observacao += _viewModel?.Observacao;
				});
			}
		}
	}
	public class ControleEntregaListaViewModel : ListaManutencaoBase<ControleEntregaDto, ControleEntregaViewModel, ControleEntrega>
	{
		private readonly IWService _service;

		public bool ImprimirVisivel => PodeImprimir();

		public bool ImprimirGarantiaEntregaPendenteVisivel => PodeImprimirGarantiaEntregaPendente();

		public bool RegistrarEntregaVisivel => PodeRegistrarEntrega();

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand<object> AdicionarSituacaoCommand { get; set; }

		public ICommand RegistrarEntregaCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand ImprimirGarantiaEntregaPendenteCommand { get; set; }

		public ControleEntregaListaViewModel(IWService service, ISessao sessao, IEntityFiltroControleEntrega<ControleEntregaDto, IControleEntregaModuleService> filtro)
			: base(sessao, typeof(ControleEntregaFichaAdicionarView).Name, typeof(ControleEntregaFichaAtualizarView).Name)
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Expected O, but got Unknown
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Expected O, but got Unknown
			_service = service;
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			ImprimirGarantiaEntregaPendenteCommand = (ICommand)new DelegateCommand((Action)ImprimirGarantiaEntregaPendente, (Func<bool>)PodeImprimirGarantiaEntregaPendente, (bool?)null);
			AdicionarSituacaoCommand = (ICommand<object>)(object)new DelegateCommand<object>((Action<object>)AdicionarSituacao);
			RegistrarEntregaCommand = (ICommand)new DelegateCommand((Action)RegistrarEntrega, (Func<bool>)PodeRegistrarEntrega, (bool?)null);
			((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)(object)(filtro as EntityFiltroControleEntrega<ControleEntregaDto, IControleEntregaModuleService>));
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
		}

		protected override void SelecionadoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			base.SelecionadoOnPropertyChanged(sender, propertyChangedEventArgs);
			if (propertyChangedEventArgs.PropertyName.Equals("Selecionado"))
			{
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => RegistrarEntregaVisivel));
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ImprimirVisivel));
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ImprimirGarantiaEntregaPendenteVisivel));
			}
		}

		private bool PodeImprimir()
		{
			if (((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens != null && ((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.SelecionadoValido)
			{
				if (((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo != 1)
				{
					return ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 3;
				}
				return true;
			}
			return false;
		}

		private void Imprimir()
		{
			Relatorio val = null;
			if (DadosSistema.ConfiguracaoDto.Saida.UsarImpresso40ColunasControleEntrega.Valor)
			{
				if (((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1)
				{
					val = (Relatorio)(object)new ImpressoControleEntregaPendente40Colunas(((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto);
				}
				if (((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 3)
				{
					val = (Relatorio)(object)new ImpressoControleEntregaConcluido40Colunas(((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto);
				}
				if (val != null)
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)val.Processar());
				}
			}
			else
			{
				if (((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1)
				{
					val = (Relatorio)(object)new ImpressoControleEntregaPendente(((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto);
				}
				if (((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 3)
				{
					val = (Relatorio)(object)new ImpressoControleEntregaConcluido(((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto);
				}
				if (val != null)
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)val.Processar());
				}
			}
		}

		private bool PodeImprimirGarantiaEntregaPendente()
		{
			if (((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens != null && ((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1;
			}
			return false;
		}

		private void ImprimirGarantiaEntregaPendente()
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			PessoaDto empresa = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
			ControleEntregaGarantiaEntregaPendenteDto controleEntrega = new ControleEntregaGarantiaEntregaPendenteDto
			{
				Cliente = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Cliente,
				Empresa = empresa,
				MovimentacoesReferenciadas = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.MovimentacoesReferenciadas,
				Numero = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Numero,
				Itens = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Itens.Where((ControleEntregaItemDto x) => x.QuantidadePendente > 0m),
				Observacao = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Observacao
			};
			if (DadosSistema.ConfiguracaoDto.Saida.UsarImpresso40ColunasControleEntrega.Valor)
			{
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new GarantiaEntregaPendente40Colunas(controleEntrega)).Processar());
			}
			else
			{
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new GarantiaEntregaPendente(controleEntrega)).Processar());
			}
		}

		private bool PodeRegistrarEntrega()
		{
			if (((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1;
			}
			return false;
		}

		private void RegistrarEntrega()
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			ControleEntregaDto dto = ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto;
			string name = typeof(RegistroEntregaFichaView).Name;
			Action<NavigationResult> obj = delegate
			{
			};
			NavigationParameters val = new NavigationParameters();
			val.Add(typeof(RegistroEntregaViewModel).Name, (object)new RegistroEntregaViewModel(dto));
			((NavigationBase)this).Navegar(name, obj, val);
		}

		private void AdicionarSituacao(object obj)
		{
			SituacaoControleEntregaDto val = (SituacaoControleEntregaDto)((obj is SituacaoControleEntregaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val) && val.Codigo != 1)
			{
				AdicionarSituacao(val);
			}
		}

		private void AdicionarSituacao(SituacaoControleEntregaDto controleEntregaDto)
		{
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Expected O, but got Unknown
			try
			{
				if (controleEntregaDto.Codigo == 3 && ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Itens.Any((ControleEntregaItemDto x) => x.QuantidadePendente > 0m) && DialogoFactory.MostrarDialogoSimNao("Existem itens com quantidade pendente neste controle. Deseja concluir mesmo assim?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				string text = null;
				if (DialogoFactory.MostrarDialogoEntradaString(ref text, "Observação", controleEntregaDto.Observacao, (IIcone)null, false, false, true, (string)null, false, true) != 0)
				{
					ControleEntregaHistoricoDto historico = new ControleEntregaHistoricoDto
					{
						Situacao = controleEntregaDto,
						Data = DateTime.Now,
						Observacao = HelperString.ToStringEmptyWhenNull(text),
						ControleEntregaReferencia = ((EntityBaseDto)((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto).Id
					};
					ControleEntregaDto val = _service.Canal<IControleEntregaModuleService, ControleEntregaDto>((Func<IControleEntregaModuleService, ControleEntregaDto>)((IControleEntregaModuleService service) => service.AdicionarHistorico(historico)));
					((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Atualizar(val);
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private bool PodeVisualizar()
		{
			if (DadosSistema.VerificarPermissao<ControleEntrega, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).PodeSelecionar();
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
			val.Add("ItemModuloCreator", (object)66);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		public override bool PodeEditar()
		{
			if (base.PodeEditar())
			{
				return ((ViewModelDtoBase<ControleEntregaDto>)((ListaBase<ControleEntregaDto, ControleEntregaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1;
			}
			return false;
		}
	}
	public class ControleEntregaViewModel : ViewModelDtoBase<ControleEntregaDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public SelecaoCommandParameter<MovimentacaoDto> MovimentacaoCommandParameter
		{
			get
			{
				SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)CallbackSelecaoMovimentacaoCommandParameter, true);
				obj.AdicionarParametro("OcultarCheckbox", (object)true);
				return obj;
			}
		}

		public string InfoMovimentacaoSelecionada
		{
			get
			{
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				if (!HelperObject.IsNull((object)Movimentacao) && !HelperObject.Is<MovimentacaoDtoNull>((object)Movimentacao))
				{
					return $"{HelperEnum.RetornarDescricao((Enum)(object)((MovimentacaoBaseDto)Movimentacao).Tipo)} número: {((MovimentacaoBaseDto)Movimentacao).Numero}";
				}
				return string.Empty;
			}
		}

		public MovimentacaoDto Movimentacao { get; set; }

		public int Numero { get; set; }

		[Valid]
		public PessoaDetalheViewModel PessoaDetalheViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDetalheViewModel>((Expression<Func<PessoaDetalheViewModel>>)(() => PessoaDetalheViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDetalheViewModel>((Expression<Func<PessoaDetalheViewModel>>)(() => PessoaDetalheViewModel), value);
			}
		}

		[CustomValidation(typeof(ControleEntregaViewModel), "ValidarItensControleEntrega", ErrorMessage = "Verifique a lista de itens.")]
		public Lista<ItemEntregaViewModel> ItensEntrega
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemEntregaViewModel>>((Expression<Func<Lista<ItemEntregaViewModel>>>)(() => ItensEntrega));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemEntregaViewModel>>((Expression<Func<Lista<ItemEntregaViewModel>>>)(() => ItensEntrega), value);
			}
		}

		public string MovimentacoesReferenciadas { get; set; }

		public string ReferenciaMovimentacoesReferenciadas { get; set; }

		public string ResumoFinanceiroMovimentacoesReferenciadas { get; set; }

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

		public Visibility MostrarFicha
		{
			get
			{
				if (!HelperObject.IsNotNull((object)ItensEntrega) || !ItensEntrega.PossuiItemSelecionado)
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public bool FocoNaPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPesquisa), value);
			}
		}

		public bool FocoNoAdicionar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoAdicionar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoAdicionar), value);
			}
		}

		public override string[] Pendencies
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(((Validation)this).Pendencies);
				if (HelperObject.IsNull((object)ItensEntrega))
				{
					return list.ToArray();
				}
				foreach (ItemEntregaViewModel iten in ItensEntrega.Itens)
				{
					list.AddRange(((Validation)iten).Pendencies);
				}
				return list.ToArray();
			}
		}

		public ICommand AdicionarMovimentacaoCommand { get; set; }

		public ControleEntregaViewModel()
		{
		}

		public ControleEntregaViewModel(ControleEntregaDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			AdicionarMovimentacaoCommand = (ICommand)new DelegateCommand((Action)AdicionarMovimentacao, (Func<bool>)PodeAdicionarMovimentacao, (bool?)null);
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarItensControleEntrega(object value, ValidationContext context)
		{
			Lista<ItemEntregaViewModel> val = value as Lista<ItemEntregaViewModel>;
			if (!HelperObject.IsNotNull((object)val) || !val.Itens.Any() || !val.Itens.All((ItemEntregaViewModel x) => ((Validation)x).Valid))
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		private void CallbackSelecaoMovimentacaoCommandParameter(MovimentacaoDto movimentacao)
		{
			if (((MovimentacaoBaseDto)movimentacao).Situacao.Codigo == 4 || ((MovimentacaoBaseDto)movimentacao).Situacao.Codigo == 6 || ((MovimentacaoBaseDto)movimentacao).Situacao.Codigo == 12 || ((MovimentacaoBaseDto)movimentacao).Situacao.Codigo == 7)
			{
				DialogoFactory.MostrarDialogoConfirma("Esta movimentação não pode ser adicionada a um controle de entrega.", false, System.Windows.HorizontalAlignment.Center);
			}
			else
			{
				SetarMovimentacao(movimentacao);
			}
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacao)
		{
			Movimentacao = movimentacao;
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => InfoMovimentacaoSelecionada));
			UpdateView();
			((UpdateViewBindableBase)this).DormirAsync(200, (Action<object>)delegate
			{
				FocoNoAdicionar = !FocoNoAdicionar;
			});
		}

		private bool PodeAdicionarMovimentacao()
		{
			if (HelperObject.IsNotNull((object)Movimentacao))
			{
				return !HelperObject.Is<MovimentacaoDtoNull>((object)Movimentacao);
			}
			return false;
		}

		private void AdicionarMovimentacao()
		{
			AdicionarMovimentacaoEMostrarFicha();
		}

		public void AdicionarMovimentacaoEMostrarFicha()
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			string result = DadosSistema.GetService().Canal<IControleEntregaModuleService, Task<string>>((Func<IControleEntregaModuleService, Task<string>>)((IControleEntregaModuleService service) => service.VerificarMovimentacaoEmOutroControleDeEntrega(((EntityBaseDto)Movimentacao).Id))).Result;
			if (!string.IsNullOrEmpty(result) && DialogoFactory.MostrarDialogoSimNao($"Essa movimentação já está vinculada com o controle de entrega número {result}. Continuar?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				SetarMovimentacao((MovimentacaoDto)new MovimentacaoDtoNull());
				FocoNaPesquisa = !FocoNaPesquisa;
				return;
			}
			if (!HelperString.IsNullOrEmpty(Movimentacao.Observacao) && DialogoFactory.MostrarDialogoSimNao("Adicionar a observação da movimentação na observação do controle de entrega?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				Observacao = (string.IsNullOrEmpty(base.Dto.Observacao) ? Movimentacao.Observacao : (Environment.NewLine + Movimentacao.Observacao));
			}
			MovimentacoesReferenciadas += $"{Movimentacao.ToTextCurta}; ";
			ReferenciaMovimentacoesReferenciadas += $"{((EntityBaseDto)Movimentacao).Id}; ";
			ResumoFinanceiroMovimentacoesReferenciadas += $"{((MovimentacaoBaseDto)Movimentacao).ResumoPagamentosRecebimentosToText}{Environment.NewLine}{Environment.NewLine}";
			SetarCliente();
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => MostrarFicha));
			((UpdateViewBindableBase)this).DormirAsync(500, (Action<object>)delegate
			{
				FocoNaPesquisa = !FocoNaPesquisa;
			});
		}

		private void SetarCliente()
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			MessageBoxResult messageBoxResult = MessageBoxResult.Yes;
			if (!HelperObject.Is<PessoaDtoNull>((object)PessoaDetalheViewModel.Pessoa) && !((EntityBaseDto)((MovimentacaoBaseDto)Movimentacao).Pessoa).Id.Equals(((EntityBaseDto)PessoaDetalheViewModel.Pessoa).Id))
			{
				messageBoxResult = DialogoFactory.MostrarDialogoSimNao("O cliente da movimentação selecionada difere do cliente atual deste controle de entrega. Deseja continuar e substituir o cliente?", false, 0, System.Windows.HorizontalAlignment.Center);
			}
			if (messageBoxResult == MessageBoxResult.Yes)
			{
				PessoaDetalheViewModel = new PessoaDetalheViewModel(((MovimentacaoBaseDto)Movimentacao).Pessoa);
				((ViewModelBase<PessoaDto>)(object)PessoaDetalheViewModel).ProjectedAsViewModel();
				CriarItensEntrega();
				SetarMovimentacao((MovimentacaoDto)new MovimentacaoDtoNull());
			}
		}

		private void CriarItensEntrega()
		{
			if (Movimentacao.Itens.Any((ItemDto x) => !HelperObject.Is<ServicoDtoNull>((object)x.ProdutoServico.Servico)))
			{
				DialogoFactory.MostrarDialogoConfirma("Somente itens do tipo 'Produto' serão adicionados ao controle de entrega.", false, System.Windows.HorizontalAlignment.Center);
			}
			foreach (ItemDto item in Movimentacao.Itens.Where((ItemDto x) => HelperObject.Is<ServicoDtoNull>((object)x.ProdutoServico.Servico) && !x.Cancelado))
			{
				ItemEntregaViewModel itemAdicionar = new ItemEntregaViewModel(CriarItemEntrega(item, ((EntityBaseDto)Movimentacao).Id), !((EntityBaseDto)base.Dto).IsTransient());
				((ViewModelBase<ControleEntregaItemDto>)(object)itemAdicionar).ProjectedAsViewModel();
				((BindableBase)itemAdicionar).PropertyChanged += ItemControleEntregaOnPropertyChanged;
				if (ItensEntrega.Itens.FirstOrDefault((ItemEntregaViewModel x) => x.ProdutoEmpresaReferencia.Equals(itemAdicionar.ProdutoEmpresaReferencia)) != null)
				{
					ItensEntrega.Itens.FirstOrDefault((ItemEntregaViewModel x) => x.ProdutoEmpresaReferencia.Equals(itemAdicionar.ProdutoEmpresaReferencia)).Quantidade += itemAdicionar.Quantidade;
				}
				else
				{
					ItensEntrega.Adicionar(itemAdicionar);
				}
			}
		}

		private void ItemControleEntregaOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<string[]>((Expression<Func<string[]>>)(() => ((Validation)this).Pendencies));
		}

		private ControleEntregaItemDto CriarItemEntrega(ItemDto itemMovimentacao, string movimentacaoReferencia)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Expected O, but got Unknown
			ControleEntregaItemDto val = new ControleEntregaItemDto
			{
				Quantidade = itemMovimentacao.Quantidade,
				QuantidadePendente = itemMovimentacao.Quantidade,
				MovimentacaoReferencia = movimentacaoReferencia,
				ProdutoEmpresaReferencia = itemMovimentacao.ProdutoServicoEmpresaReferencia,
				ProdutoDescricao = itemMovimentacao.ProdutoServico.Descricao,
				CodigoInterno = itemMovimentacao.ProdutoServico.CodigoInterno,
				UnidadeMedida = itemMovimentacao.UnidadeMedida.Sigla,
				Localizacao = itemMovimentacao.Localizacao.Descricao
			};
			ProdutoDto produto = itemMovimentacao.ProdutoServico.Produto;
			object marca;
			if (produto == null)
			{
				marca = null;
			}
			else
			{
				MarcaDto marca2 = produto.Marca;
				marca = ((marca2 != null) ? HelperString.ToStringEmptyWhenNull(marca2.Descricao) : null);
			}
			val.Marca = (string)marca;
			val.PrecoUnitario = itemMovimentacao.PrecoUnitario;
			return val;
		}

		public override void ProjectedAsViewModel()
		{
			ItensEntrega = new Lista<ItemEntregaViewModel>();
			PessoaDetalheViewModel = new PessoaDetalheViewModel(base.Dto.Cliente);
			((ViewModelBase<PessoaDto>)(object)PessoaDetalheViewModel)?.ProjectedAsViewModel();
			Numero = base.Dto.Numero;
			MovimentacoesReferenciadas = base.Dto.MovimentacoesReferenciadas;
			ReferenciaMovimentacoesReferenciadas = base.Dto.ReferenciaMovimentacoesReferenciadas;
			ResumoFinanceiroMovimentacoesReferenciadas = base.Dto.ResumoFinanceiroMovimentacoesReferenciadas;
			foreach (ControleEntregaItemDto iten in base.Dto.Itens)
			{
				ItemEntregaViewModel itemEntregaViewModel = new ItemEntregaViewModel(iten, !((EntityBaseDto)base.Dto).IsTransient());
				((ViewModelBase<ControleEntregaItemDto>)(object)itemEntregaViewModel).ProjectedAsViewModel();
				ItensEntrega.Adicionar(itemEntregaViewModel);
			}
			UpdateView();
		}

		private void UpdateView()
		{
			((UpdateViewBindableBase)this).RecuperarFoco();
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((Validation)this).Valid));
			((BindableBase)this).RaisePropertyChanged<string[]>((Expression<Func<string[]>>)(() => ((Validation)this).Pendencies));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			ControleEntregaDto val = (ControleEntregaDto)((obj is ControleEntregaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				Lista<ControleEntregaItemDto> val2 = AtualizarDtoItensEntrega();
				val.Cliente = PessoaDetalheViewModel.ObterDto();
				val.Numero = Numero;
				val.Itens = val2.Itens;
				val.MovimentacoesReferenciadas = MovimentacoesReferenciadas;
				val.ReferenciaMovimentacoesReferenciadas = ReferenciaMovimentacoesReferenciadas;
				val.ResumoFinanceiroMovimentacoesReferenciadas = ResumoFinanceiroMovimentacoesReferenciadas;
				base.AtualizarDto<ControleEntregaDto>(ref val);
			}
		}

		private Lista<ControleEntregaItemDto> AtualizarDtoItensEntrega()
		{
			Lista<ControleEntregaItemDto> val = new Lista<ControleEntregaItemDto>();
			foreach (ItemEntregaViewModel iten in ItensEntrega.Itens)
			{
				ControleEntregaItemDto dto = ((ViewModelDtoBase<ControleEntregaItemDto>)iten).Dto;
				((ViewModelDtoBase<ControleEntregaItemDto>)iten).AtualizarDto<ControleEntregaItemDto>(ref dto);
				val.Adicionar(dto);
			}
			return val;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ControleEntregaFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Controles de entrega";
		}

		public string ToTituloFicha()
		{
			return "Controle de entrega";
		}

		public override void Dispose()
		{
			foreach (ItemEntregaViewModel iten in ItensEntrega.Itens)
			{
				((BindableBase)iten).PropertyChanged -= ItemControleEntregaOnPropertyChanged;
			}
			((ViewModelValidationBase)ItensEntrega).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class ControlesEntregaViewModel : ModuloBase
	{
		public ControlesEntregaViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloControleEntregaRegion, typeof(ControleEntregaListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ObservacaoControleEntregaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ObservacaoControleEntregaAbaViewModel(IViewModelDtoBase viewModel)
			: base("DADOS ADICIONAIS", "Informações complementares", viewModel)
		{
		}
	}
	public class ObservacaoControleEntregaViewModel : ViewModelDtoBase<ControleEntregaDto>
	{
		public bool PodePesquisarObservacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodePesquisarObservacao));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodePesquisarObservacao), value);
			}
		}

		public SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>> ObservacaoCommandParameter => new SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>>("ObservacaoDadoAdicionalListaView", (Action<IEnumerable<ObservacaoDadoAdicionalDto>>)delegate(IEnumerable<ObservacaoDadoAdicionalDto> observacao)
		{
			if (!string.IsNullOrEmpty(Observacao))
			{
				Observacao = Observacao.TrimEnd() + " ";
			}
			Observacao += string.Join(" ", observacao.Select((ObservacaoDadoAdicionalDto x) => x.Conteudo));
		}, true);

		public string Observacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Observacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Observacao), value, (Action)delegate
				{
					base.Dto.Observacao = Observacao;
				});
			}
		}

		public ObservacaoControleEntregaViewModel(ControleEntregaDto dto)
			: base(dto)
		{
			PodePesquisarObservacao = DadosSistema.PodeExecutarFuncao(new Type[3]
			{
				typeof(Gerencial),
				typeof(Administrador),
				typeof(AdministradorIndustria)
			});
		}

		public override void ProjectedAsViewModel()
		{
			Observacao = base.Dto.Observacao;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			ControleEntregaDto val = (ControleEntregaDto)((obj is ControleEntregaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Observacao = Observacao;
				base.AtualizarDto<ControleEntregaDto>(ref val);
			}
		}
	}
	public class ItemEntregaViewModel : ViewModelDtoBase<ControleEntregaItemDto>
	{
		private decimal _quantidadePendente;

		private static decimal _quantidadePodeDevolver;

		public string ProdutoEmpresaReferencia { get; set; }

		public string CodigoInterno { get; set; }

		public string UnidadeMedida { get; set; }

		public string Localizacao { get; set; }

		public string Marca { get; set; }

		public bool ReadOnly
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ReadOnly));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ReadOnly), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Quantidade' deve ser maior que zero.")]
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

		[NaoPermitirValorMenorZero(ErrorMessage = "A 'Quantidade entregue' não pode ser negativa.")]
		public decimal QuantidadeEntregue
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeEntregue));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeEntregue), value, (Action)AtualizarQuantidadePendente);
			}
		}

		[NaoPermitirValorMenorZero(ErrorMessage = "A 'Quantidade pendente' não pode ser negativa.")]
		public decimal QuantidadePendente
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadePendente));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadePendente), value);
			}
		}

		[NaoPermitirValorMenorZero(ErrorMessage = "A 'Quantidade devolvida' não pode ser negativa.")]
		[CustomValidation(typeof(ItemEntregaViewModel), "ValidarQuantidadeDevolvida", ErrorMessage = "'Quantidade devolvida' ultrapassa a quantidade entregue anteriormente.")]
		public decimal QuantidadeDevolvida
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolvida));
			}
			set
			{
				_quantidadePodeDevolver = Quantidade - QuantidadePendente;
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeDevolvida), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Produto' é obrigatório.")]
		public string ProdutoServicoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoServicoDescricao), value);
			}
		}

		public bool IsRegistroEntrega
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsRegistroEntrega));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsRegistroEntrega), value);
			}
		}

		public ItemEntregaViewModel(ControleEntregaItemDto dto, bool readOnly)
			: base(dto)
		{
			ReadOnly = readOnly;
		}

		private void AtualizarQuantidadePendente()
		{
			if (IsRegistroEntrega)
			{
				QuantidadePendente = _quantidadePendente - QuantidadeEntregue;
			}
			else
			{
				QuantidadePendente = Quantidade - QuantidadeEntregue;
			}
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarQuantidadeDevolvida(object value, ValidationContext context)
		{
			if (!((decimal)value > _quantidadePodeDevolver))
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
		}

		public override void ProjectedAsViewModel()
		{
			_quantidadePendente = base.Dto.QuantidadePendente;
			ProdutoEmpresaReferencia = base.Dto.ProdutoEmpresaReferencia;
			ProdutoServicoDescricao = base.Dto.ProdutoDescricao;
			Quantidade = base.Dto.Quantidade;
			QuantidadeDevolvida = base.Dto.QuantidadeDevolvida;
			QuantidadePendente = base.Dto.QuantidadePendente;
			QuantidadeEntregue = base.Dto.QuantidadeEntregue;
			CodigoInterno = base.Dto.CodigoInterno;
			UnidadeMedida = base.Dto.UnidadeMedida;
			Localizacao = base.Dto.Localizacao;
			Marca = base.Dto.Marca;
			IsRegistroEntrega = false;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			ControleEntregaItemDto val = (ControleEntregaItemDto)((obj is ControleEntregaItemDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.ProdutoEmpresaReferencia = ProdutoEmpresaReferencia;
				val.ProdutoDescricao = ProdutoServicoDescricao;
				val.Quantidade = Quantidade;
				val.QuantidadeDevolvida = QuantidadeDevolvida;
				val.QuantidadeEntregue = QuantidadeEntregue;
				val.QuantidadePendente = QuantidadePendente;
				val.CodigoInterno = CodigoInterno;
				val.UnidadeMedida = UnidadeMedida;
				val.Localizacao = Localizacao;
				val.Marca = Marca;
				base.AtualizarDto<ControleEntregaItemDto>(ref val);
			}
		}
	}
	public class RegistroEntregaAbaViewModel : AbaPrincipalViewModelBase
	{
		public RegistroEntregaAbaViewModel(IViewModelPrincipal viewModel)
			: base("REGISTRO DE ENTREGA", "Dados sobre entrega efetuada", viewModel)
		{
		}
	}
	public class RegistroEntregaFichaViewModel : FichaBasePersistir<ControleEntregaDto, RegistroEntregaViewModel>
	{
		private readonly IWService _service;

		public RegistroEntregaFichaViewModel(IWService service)
			: base("Inclusão: Registro de entrega")
		{
			_service = service;
		}

		protected override Task<ControleEntregaDto> Persistir(IFormularioEntity formulario)
		{
			RegistroEntregaDto registroEntrega = (((Formulario)(RegistroEntregaFormulario)(object)formulario).Abas.FirstOrDefault((IAbaViewModelBase x) => x is RegistroEntregaAbaViewModel).ViewModel as RegistroEntregaViewModel)?.ObterDto();
			ControleEntregaDto dto = _service.Canal<IControleEntregaModuleService, ControleEntregaDto>((Func<IControleEntregaModuleService, ControleEntregaDto>)((IControleEntregaModuleService service) => service.AdicionarRegistroEntrega(registroEntrega)));
			ImprimirComprovanteEntrega(registroEntrega);
			base.SelecaoNavigateParameterCallBack?.CallBack(dto);
			return Task.Factory.StartNew(() => dto);
		}

		private static void ImprimirComprovanteEntrega(RegistroEntregaDto registroEntrega)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Registro de entrega adicionado! Deseja imprimir o comprovante de entrega?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				if (DadosSistema.ConfiguracaoDto.Saida.UsarImpresso40ColunasControleEntrega.Valor)
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ComprovanteEntrega40Colunas(registroEntrega)).Processar());
				}
				else
				{
					XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ComprovanteEntrega(registroEntrega)).Processar());
				}
			}
		}
	}
	public class RegistroEntregaFormulario : FormularioEntity<ControleEntregaDto>
	{
		public RegistroEntregaFormulario(ControleEntregaDto dto)
			: base(dto)
		{
			CriarAbas();
		}

		private void CriarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new RegistroEntregaAbaViewModel((IViewModelPrincipal)(object)new RegistroEntregaViewModel(base.Dto)) });
		}
	}
	public class RegistroEntregaViewModel : ViewModelDtoBase<ControleEntregaDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public DateTime Data
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => Data));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => Data), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Quem recebeu' é obrigatório.")]
		public string Responsavel
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Responsavel));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Responsavel), value);
			}
		}

		public PessoaDto Cliente
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Cliente));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Cliente), value);
			}
		}

		[Valid]
		public Lista<ItemEntregaViewModel> ItensEntrega
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ItemEntregaViewModel>>((Expression<Func<Lista<ItemEntregaViewModel>>>)(() => ItensEntrega));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ItemEntregaViewModel>>((Expression<Func<Lista<ItemEntregaViewModel>>>)(() => ItensEntrega), value);
			}
		}

		public int Numero { get; set; }

		public override string[] Pendencies
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(((Validation)this).Pendencies);
				if (HelperObject.IsNull((object)ItensEntrega))
				{
					return list.ToArray();
				}
				foreach (ItemEntregaViewModel iten in ItensEntrega.Itens)
				{
					list.AddRange(((Validation)iten).Pendencies);
				}
				return list.ToArray();
			}
		}

		public RegistroEntregaViewModel()
		{
		}

		public RegistroEntregaViewModel(ControleEntregaDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Numero = base.Dto.Numero;
			Data = DateTime.Now;
			Cliente = base.Dto.Cliente;
			ItensEntrega = new Lista<ItemEntregaViewModel>();
			foreach (ControleEntregaItemDto iten in base.Dto.Itens)
			{
				ItemEntregaViewModel itemEntregaViewModel = new ItemEntregaViewModel(iten, readOnly: false);
				iten.QuantidadeEntregue = 0m;
				iten.QuantidadeDevolvida = 0m;
				((ViewModelBase<ControleEntregaItemDto>)(object)itemEntregaViewModel).ProjectedAsViewModel();
				itemEntregaViewModel.IsRegistroEntrega = true;
				ItensEntrega.Adicionar(itemEntregaViewModel);
			}
		}

		public RegistroEntregaDto ObterDto()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			Lista<ControleEntregaItemDto> val = ObterDtoItens();
			return new RegistroEntregaDto
			{
				ControleEntregaReferencia = ((EntityBaseDto)base.Dto).Id,
				Itens = val.Itens,
				Data = Data,
				Responsavel = Responsavel
			};
		}

		private Lista<ControleEntregaItemDto> ObterDtoItens()
		{
			Lista<ControleEntregaItemDto> val = new Lista<ControleEntregaItemDto>();
			foreach (ItemEntregaViewModel iten in ItensEntrega.Itens)
			{
				ControleEntregaItemDto dto = ((ViewModelDtoBase<ControleEntregaItemDto>)iten).Dto;
				((ViewModelDtoBase<ControleEntregaItemDto>)iten).AtualizarDto<ControleEntregaItemDto>(ref dto);
				val.Adicionar(dto);
			}
			return val;
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new RegistroEntregaFormulario(base.Dto);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
}
namespace Otus.Modulo.ControlesEntrega.ViewModels.Pessoa
{
	public class EnderecoEntregaEstrangeiroViewModel : EnderecoEstrangeiroViewModel
	{
		public override string Tipo => "Endereço de entrega (Exterior)";

		public EnderecoEntregaEstrangeiroViewModel(EnderecoDto dto)
			: base(dto)
		{
		}
	}
	public class EnderecoCobrancaEstrangeiroViewModel : EnderecoEstrangeiroViewModel
	{
		public override string Tipo => "Endereço de cobrança (Exterior)";

		public EnderecoCobrancaEstrangeiroViewModel(EnderecoDto dto)
			: base(dto)
		{
		}
	}
	public class EnderecoEstrangeiroViewModel : EnderecoViewModel
	{
		public ISelecaoCommandParameter PaisCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PaisDto>("PaisListaView", (Action<PaisDto>)delegate(PaisDto pais)
		{
			Pais = pais;
		}, true);

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Código postal' é obrigatório.")]
		public string CodigoPostal
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CodigoPostal));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CodigoPostal), value);
			}
		}

		public PaisDto Pais
		{
			get
			{
				return ((BindableBase)this).GetProperty<PaisDto>((Expression<Func<PaisDto>>)(() => Pais));
			}
			set
			{
				((BindableBase)this).SetProperty<PaisDto>((Expression<Func<PaisDto>>)(() => Pais), value, (Action)AlterouPais);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'País' é obrigatório.")]
		public string NomePais
		{
			get
			{
				if (!HelperObject.IsNull((object)Pais) && !HelperObject.IsExactlyObject<PaisDtoNull>((object)Pais))
				{
					return Pais.Nome;
				}
				return string.Empty;
			}
		}

		public override string Tipo => "Endereço principal (Exterior)";

		public ICommand RemoverPaisCommand { get; set; }

		public EnderecoEstrangeiroViewModel(EnderecoDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			RemoverPaisCommand = (ICommand)new DelegateCommand((Action)RemoverPais, (Func<bool>)PodeRemoverPais, (bool?)null);
		}

		private void RemoverPais()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Pais = (PaisDto)new PaisDtoNull();
		}

		private bool PodeRemoverPais()
		{
			return !HelperObject.Is<PaisDtoNull>((object)Pais);
		}

		private void AlterouPais()
		{
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomePais));
		}

		public override void ProjectedAsViewModel()
		{
			Pais = ((ViewModelDtoBase<EnderecoDto>)this).Dto.Pais;
			CodigoPostal = ((ViewModelDtoBase<EnderecoDto>)this).Dto.CodigoPostal;
			base.ProjectedAsViewModel();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			EnderecoDto dto2 = (EnderecoDto)((obj is EnderecoDto) ? obj : null);
			if (!HelperObject.IsNull((object)dto2))
			{
				dto2.Pais = Pais;
				dto2.CodigoPostal = CodigoPostal;
				dto2.IsExterior = true;
				base.AtualizarDto(ref dto2);
			}
		}
	}
	public class EnderecoEntregaNacionalViewModel : EnderecoNacionalViewModel
	{
		public override string Tipo => "Endereço de entrega";

		public EnderecoEntregaNacionalViewModel(EnderecoDto dto)
			: base(dto)
		{
		}
	}
	public class EnderecoCobrancaNacionalViewModel : EnderecoNacionalViewModel
	{
		public override string Tipo => "Endereço de cobrança";

		public EnderecoCobrancaNacionalViewModel(EnderecoDto dto)
			: base(dto)
		{
		}
	}
	public class EnderecoNacionalViewModel : EnderecoViewModel
	{
		public ISelecaoCommandParameter MunicipioCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MunicipioDto>("MunicipioListaView", (Action<MunicipioDto>)delegate(MunicipioDto municipio)
		{
			Municipio = municipio;
		}, true);

		public bool InformouCepComLogradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro), value);
			}
		}

		public string Cep
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cep));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cep), value);
			}
		}

		public MunicipioDto Municipio
		{
			get
			{
				return ((BindableBase)this).GetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio));
			}
			set
			{
				((BindableBase)this).SetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio), value, (Action)AlterouMunicipio);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Município' é obrigatório.")]
		public string NomeMunicipio
		{
			get
			{
				if (!HelperObject.IsNull((object)Municipio) && !HelperObject.Is<MunicipioDtoNull>((object)Municipio))
				{
					return Municipio.Nome;
				}
				return string.Empty;
			}
		}

		public UFDto Uf
		{
			get
			{
				return ((BindableBase)this).GetProperty<UFDto>((Expression<Func<UFDto>>)(() => Uf));
			}
			set
			{
				((BindableBase)this).SetProperty<UFDto>((Expression<Func<UFDto>>)(() => Uf), value, (Action)AlterouUf);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'UF' é obrigatório.")]
		public string SiglaUf
		{
			get
			{
				if (!HelperObject.IsNull((object)Uf))
				{
					return Uf.Sigla;
				}
				return string.Empty;
			}
		}

		public override string Tipo => "Endereço principal";

		public ICommand<string> PesquisarCepCommand { get; set; }

		public ICommand RemoverMunicipioCommand { get; set; }

		public EnderecoNacionalViewModel(EnderecoDto dto)
			: base(dto)
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			PesquisarCepCommand = (ICommand<string>)(object)new DelegateCommand<string>((Action<string>)PesquisarCep, (Func<string, bool>)PodePesquisarCep, (bool?)null);
			RemoverMunicipioCommand = (ICommand)new DelegateCommand((Action)RemoverMunicipio, (Func<bool>)PodeRemoverMunicipio, (bool?)null);
		}

		private void RemoverMunicipio()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Municipio = (MunicipioDto)new MunicipioDtoNull();
		}

		private bool PodeRemoverMunicipio()
		{
			return !HelperObject.Is<MunicipioDtoNull>((object)Municipio);
		}

		private bool PodePesquisarCep(string cep)
		{
			if (!string.IsNullOrWhiteSpace(cep))
			{
				return cep.Length.Equals(8);
			}
			return false;
		}

		private void AlterouMunicipio()
		{
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeMunicipio));
			Uf = Municipio.UF;
		}

		private void AlterouUf()
		{
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => SiglaUf));
		}

		private void PesquisarCep(string cep)
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			try
			{
				LoaderProcessoService.MostrarLoader("Pesquisando endereço pelo CEP...");
				ConsultaCepServiceFactory.SetCurrent((IConsultaCepServiceFactory)new ConsultaViaCepServiceFactory());
				ConsultaCepServiceFactory.SetWebProxy(DadosSistema.ObterProxy());
				IConsultaCepService val = ConsultaCepServiceFactory.Create();
				OnConsultouCepEvent(val.ObterCep(HelperString.RemoveDiacritics(cep)));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				Cep = string.Empty;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private void OnConsultouCepEvent(CepInfo cepInfo)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Expected O, but got Unknown
			//IL_00eb: Expected O, but got Unknown
			//IL_00f0: Expected O, but got Unknown
			if (HelperObject.Is<CepInvalido>((object)cepInfo))
			{
				throw new Exception("CEP inválido ou não encontrado.");
			}
			base.Logradouro = cepInfo.Logradouro;
			base.Bairro = cepInfo.Bairro;
			Municipio = new MunicipioDto
			{
				Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Nome),
				CodigoIbge = cepInfo.Municipio.CodigoIbge,
				UF = new UFDto
				{
					Nome = cepInfo.Municipio.Uf.Nome,
					Sigla = cepInfo.Municipio.Uf.Sigla,
					CodigoIbge = cepInfo.Municipio.Uf.CodigoIbge,
					Pais = new PaisDto
					{
						Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Uf.Pais.Nome),
						CodigoBacen = cepInfo.Municipio.Uf.Pais.CodigoBacen
					}
				}
			};
			if (!string.IsNullOrWhiteSpace(base.Logradouro))
			{
				InformouCepComLogradouro = !InformouCepComLogradouro;
			}
		}

		public override void ProjectedAsViewModel()
		{
			Cep = ((ViewModelDtoBase<EnderecoDto>)this).Dto.Cep;
			Municipio = ((ViewModelDtoBase<EnderecoDto>)this).Dto.Municipio;
			Uf = ((ViewModelDtoBase<EnderecoDto>)this).Dto.Municipio.UF;
			base.ProjectedAsViewModel();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			EnderecoDto dto2 = (EnderecoDto)((obj is EnderecoDto) ? obj : null);
			if (!HelperObject.IsNull((object)dto2))
			{
				dto2.Cep = Cep;
				dto2.Municipio = Municipio;
				base.AtualizarDto(ref dto2);
			}
		}
	}
	public abstract class EnderecoViewModel : ViewModelDtoBase<EnderecoDto>
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Logradouro' é obrigatório.")]
		public string Logradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Logradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Logradouro), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Número' é obrigatório.")]
		public string Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Numero), value);
			}
		}

		public string Complemento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Complemento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Complemento), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Bairro' é obrigatório.")]
		public string Bairro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Bairro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Bairro), value);
			}
		}

		public abstract string Tipo { get; }

		public string TipoToText
		{
			get
			{
				string text = (HelperString.IsNullOrEmpty(base.Dto.Descricao) ? $"{base.Dto.Logradouro}, {base.Dto.Numero}".Trim(' ', ',') : base.Dto.Descricao);
				if (!HelperString.IsNullOrEmpty(text))
				{
					return $"{Tipo} - {text}";
				}
				return Tipo;
			}
		}

		protected EnderecoViewModel(EnderecoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Logradouro = base.Dto.Logradouro;
			Numero = base.Dto.Numero;
			Complemento = base.Dto.Complemento;
			Bairro = base.Dto.Bairro;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			EnderecoDto val = (EnderecoDto)((obj is EnderecoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Logradouro = Logradouro;
				val.Numero = Numero;
				val.Complemento = Complemento;
				val.Bairro = Bairro;
				base.AtualizarDto<EnderecoDto>(ref val);
			}
		}
	}
	public class EnderecoViewModelFactory
	{
		private bool _principal;

		private bool _entrega;

		public EnderecoViewModelFactory SetarPrincipal(bool principal)
		{
			_principal = principal;
			return this;
		}

		public EnderecoViewModelFactory SetarEntrega(bool entrega)
		{
			_entrega = entrega;
			return this;
		}

		public EnderecoViewModel Criar(EnderecoDto dto)
		{
			if (_principal)
			{
				if (dto.IsExterior)
				{
					return new EnderecoEstrangeiroViewModel(dto);
				}
				return new EnderecoNacionalViewModel(dto);
			}
			if (_entrega)
			{
				if (dto.IsExterior)
				{
					return new EnderecoEntregaEstrangeiroViewModel(dto);
				}
				return new EnderecoEntregaNacionalViewModel(dto);
			}
			if (dto.IsExterior)
			{
				return new EnderecoCobrancaEstrangeiroViewModel(dto);
			}
			return new EnderecoCobrancaNacionalViewModel(dto);
		}
	}
	public class PessoaDetalheViewModel : ViewModelDtoBase<PessoaDto>
	{
		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => HelperObject.IsExactlyObject<ClienteDto>((object)pessoaDto.Cliente) || pessoaDto.ConsumidorReservadoSistema), Messages.CadastroTipoCliente, (TipoPessoaCadastro)0, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		public PessoaDto Pessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value, (Action)CallBackSetPropriedadePessoa);
			}
		}

		public string NomePessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoa), value, (Action)RemoverPessoaQuandoNomeApagado);
			}
		}

		[Valid]
		public PessoaHistoricoViewModel PessoaHistoricoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaHistoricoViewModel>((Expression<Func<PessoaHistoricoViewModel>>)(() => PessoaHistoricoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaHistoricoViewModel>((Expression<Func<PessoaHistoricoViewModel>>)(() => PessoaHistoricoViewModel), value);
			}
		}

		public ISelecaoCommandParameter PessoaCommandParameter { get; set; }

		public PessoaDetalheViewModel(PessoaDto dto)
			: base(dto)
		{
			PessoaCommandParameter = (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoaDto)
			{
				Pessoa = pessoaDto;
			}, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)0, (Action<PessoaDto>)delegate(PessoaDto pessoaDto)
			{
				Pessoa = pessoaDto;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private string ObterNomePessoa()
		{
			if (!HelperObject.IsNull((object)Pessoa) && !HelperObject.Is<PessoaDtoNull>((object)Pessoa))
			{
				return Pessoa.Nome;
			}
			return string.Empty;
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomePessoa) && HelperObject.IsNotNull((object)Pessoa) && HelperObject.IsNot<PessoaDtoNull>((object)Pessoa))
			{
				Pessoa = (PessoaDto)new PessoaDtoNull();
			}
		}

		protected virtual void CallBackSetPropriedadePessoa()
		{
			NomePessoa = ObterNomePessoa();
			DisposePessoaHistorico();
			PessoaHistoricoViewModel = new PessoaHistoricoViewModel(Pessoa);
			((ViewModelBase<PessoaDto>)(object)PessoaHistoricoViewModel).ProjectedAsViewModel();
			((BindableBase)PessoaHistoricoViewModel).PropertyChanged += PessoaHistoricoViewModelOnPropertyChanged;
			((BindableBase)this).RaisePropertyChanged<PessoaHistoricoViewModel>((Expression<Func<PessoaHistoricoViewModel>>)(() => PessoaHistoricoViewModel));
		}

		protected void PessoaHistoricoViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<PessoaHistoricoViewModel>((Expression<Func<PessoaHistoricoViewModel>>)(() => PessoaHistoricoViewModel));
		}

		public override void ProjectedAsViewModel()
		{
			Pessoa = base.Dto;
		}

		public PessoaDto ObterDto()
		{
			PessoaDto pessoa = Pessoa;
			((ViewModelDtoBase<PessoaDto>)PessoaHistoricoViewModel).AtualizarDto<PessoaDto>(ref pessoa);
			return pessoa;
		}

		protected void DisposePessoaHistorico()
		{
			if (!HelperObject.IsNull((object)PessoaHistoricoViewModel))
			{
				((BindableBase)PessoaHistoricoViewModel).PropertyChanged -= PessoaHistoricoViewModelOnPropertyChanged;
				((ViewModelValidationBase)PessoaHistoricoViewModel).Dispose();
			}
		}

		public override void Dispose()
		{
			DisposePessoaHistorico();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class PessoaHistoricoViewModel : ViewModelDtoBase<PessoaDto>
	{
		private PessoaDto _cadastro;

		[Valid]
		public EnderecoViewModel EnderecoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<EnderecoViewModel>((Expression<Func<EnderecoViewModel>>)(() => EnderecoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<EnderecoViewModel>((Expression<Func<EnderecoViewModel>>)(() => EnderecoViewModel), value);
			}
		}

		public Lista<EnderecoViewModel> Enderecos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<EnderecoViewModel>>((Expression<Func<Lista<EnderecoViewModel>>>)(() => Enderecos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<EnderecoViewModel>>((Expression<Func<Lista<EnderecoViewModel>>>)(() => Enderecos), value);
			}
		}

		public ICommand RemoverMunicipioCommand { get; set; }

		public ICommand RemoverPaisCommand { get; set; }

		public ICommand PesquisarCepCommand { get; set; }

		private EnderecoDto EnderecoSelecionadoGravado
		{
			get
			{
				//IL_001e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0024: Expected O, but got Unknown
				if (!HelperObject.IsNull((object)base.Dto.EnderecoSelecionado))
				{
					return base.Dto.EnderecoSelecionado;
				}
				return (EnderecoDto)new EnderecoDtoNull();
			}
		}

		public Visibility MostrarEnderecos
		{
			get
			{
				if (!HelperObject.IsNotNull((object)Enderecos) || Enderecos.Numero <= 1)
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public PessoaHistoricoViewModel(PessoaDto pessoaDto)
			: base(pessoaDto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			_cadastro = ObterCadastro();
			Enderecos = new Lista<EnderecoViewModel>();
			((BindableBase)Enderecos).PropertyChanged += EnderecosOnPropertyChanged;
			if (!HelperObject.Is<EnderecoDtoNull>((object)_cadastro.EnderecoPrincipal))
			{
				Enderecos.Adicionar(FluentFactory<EnderecoViewModelFactory>.Init().SetarPrincipal(principal: true).Criar(_cadastro.EnderecoPrincipal));
			}
			foreach (EnderecoDto item in from x in HelperIEnumerable.ToListEmptyWhenNull<EnderecoDto>(_cadastro.EnderecosEntrega)
				where !HelperObject.Is<EnderecoDtoNull>((object)x)
				select x)
			{
				Enderecos.Adicionar(FluentFactory<EnderecoViewModelFactory>.Init().SetarEntrega(entrega: true).Criar(item));
			}
			if (!HelperObject.Is<EnderecoDtoNull>((object)_cadastro.EnderecoCobranca))
			{
				Enderecos.Adicionar(FluentFactory<EnderecoViewModelFactory>.Init().Criar(_cadastro.EnderecoCobrancaPrincipal));
			}
			PosicionarNoEnderecoGravado();
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => MostrarEnderecos));
		}

		private PessoaDto ObterCadastro()
		{
			if (HelperObject.Is<PessoaDtoNull>((object)base.Dto) || HelperIEnumerable.ToListEmptyWhenNull<EnderecoDto>(base.Dto.EnderecosEntrega).Any())
			{
				return base.Dto;
			}
			if (string.IsNullOrWhiteSpace(((EntityBaseDto)base.Dto).Id))
			{
				return base.Dto;
			}
			try
			{
				PessoaDto val = DadosSistema.GetService().Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService servico) => servico.ObterPessoaPorId(((EntityBaseDto)base.Dto).Id)));
				return (HelperObject.IsNull((object)val) || HelperObject.Is<PessoaDtoNull>((object)val)) ? base.Dto : val;
			}
			catch (Exception)
			{
				return base.Dto;
			}
		}

		private void PosicionarNoEnderecoGravado()
		{
			EnderecoDto gravado = EnderecoSelecionadoGravado;
			if (HelperObject.Is<EnderecoDtoNull>((object)gravado))
			{
				Enderecos.PosicionarNoPrimeiro();
			}
			else if (Enderecos.Itens.Any((EnderecoViewModel x) => MesmoEndereco(((ViewModelDtoBase<EnderecoDto>)x).Dto, gravado)))
			{
				Enderecos.Posicionar((Expression<Func<EnderecoViewModel, bool>>)((EnderecoViewModel x) => MesmoEndereco(((ViewModelDtoBase<EnderecoDto>)x).Dto, gravado)));
			}
			else
			{
				Enderecos.Adicionar(FluentFactory<EnderecoViewModelFactory>.Init().SetarEntrega(entrega: true).Criar(gravado));
				Enderecos.PosicionarNoUltimo();
			}
		}

		private static bool MesmoEndereco(EnderecoDto um, EnderecoDto outro)
		{
			if (HelperString.ApenasNumeros(um.Cep).Equals(HelperString.ApenasNumeros(outro.Cep)) && IgualSemCaixa(um.Logradouro, outro.Logradouro) && IgualSemCaixa(um.Numero, outro.Numero) && IgualSemCaixa(um.Bairro, outro.Bairro))
			{
				return IgualSemCaixa(um.Complemento, outro.Complemento);
			}
			return false;
		}

		private static bool IgualSemCaixa(string um, string outro)
		{
			return string.Equals(HelperString.ToStringEmptyWhenNull(um).Trim(), HelperString.ToStringEmptyWhenNull(outro).Trim(), StringComparison.OrdinalIgnoreCase);
		}

		private void EnderecosOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName.Equals("ItemSelecionado"))
			{
				EnderecoViewModel = Enderecos.ItemSelecionado;
				((ViewModelBase<EnderecoDto>)(object)EnderecoViewModel)?.ProjectedAsViewModel();
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			PessoaDto val = (PessoaDto)((obj is PessoaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val) && !HelperObject.IsNull((object)EnderecoViewModel) && !HelperObject.IsNull((object)_cadastro))
			{
				val.EnderecoPrincipal = _cadastro.EnderecoPrincipal;
				val.EnderecoCobranca = _cadastro.EnderecoCobranca;
				val.EnderecosEntrega = (from x in HelperIEnumerable.ToListEmptyWhenNull<EnderecoDto>(_cadastro.EnderecosEntrega)
					where !HelperObject.Is<EnderecoDtoNull>((object)x)
					select x).ToList();
				val.EnderecoSelecionado = ObterEnderecoSelecionado();
			}
		}

		private EnderecoDto ObterEnderecoSelecionado()
		{
			EnderecoDto result = ClonarEndereco(((ViewModelDtoBase<EnderecoDto>)EnderecoViewModel).Dto);
			((ViewModelDtoBase<EnderecoDto>)EnderecoViewModel).AtualizarDto<EnderecoDto>(ref result);
			return result;
		}

		private static EnderecoDto ClonarEndereco(EnderecoDto endereco)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)endereco) && !HelperObject.Is<EnderecoDtoNull>((object)endereco))
			{
				return new EnderecoDto
				{
					Cep = endereco.Cep,
					Logradouro = endereco.Logradouro,
					Complemento = endereco.Complemento,
					Numero = endereco.Numero,
					Bairro = endereco.Bairro,
					Municipio = endereco.Municipio,
					Pais = endereco.Pais,
					CodigoPostal = endereco.CodigoPostal,
					IsExterior = endereco.IsExterior,
					CnpjRecebedor = endereco.CnpjRecebedor,
					IeRecebedor = endereco.IeRecebedor,
					Descricao = endereco.Descricao
				};
			}
			return new EnderecoDto();
		}
	}
}
namespace Otus.Modulo.ControlesEntrega.Resources
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Messages
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
					resourceMan = new ResourceManager("Otus.Modulo.ControlesEntrega.Resources.Messages", typeof(Messages).Assembly);
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

		internal static string CadastroTipoCliente => ResourceManager.GetString("CadastroTipoCliente", resourceCulture);

		internal Messages()
		{
		}
	}
}
namespace Otus.Modulo.ControlesEntrega.Reports
{
	public class ComprovanteEntrega40Colunas : Relatorio
	{
		private readonly RegistroEntregaDto _registroEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel39;

		private BindingSource bindingSource1;

		private BindingSource bindingSource2;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel49;

		private XRLabel lblProduto;

		private XRLabel xrLabel51;

		private XRLabel xrLabel24;

		private XRLabel xrLabel33;

		private XRLabel xrLabel53;

		private XRLabel xrLabel25;

		private XRLabel xrLabel8;

		private XRLabel produto;

		private XRLabel xrLabel52;

		private XRLabel xrLabel7;

		private XRLabel xrLabel32;

		private XRLabel xrLabel27;

		private XRLabel xrLabel28;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel31;

		private XRLine xrLine1;

		private XRLabel xrLabel1;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel xrLabel35;

		private XRLabel xrLabel2;

		private XRLabel lblMensagemRodape;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ComprovanteEntrega40Colunas(RegistroEntregaDto registroEntrega)
		{
			_registroEntrega = registroEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReportBase)this).DataSource = new List<RegistroEntregaDto> { _registroEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Expected O, but got Unknown
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_035f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0394: Unknown result type (might be due to invalid IL or missing references)
			//IL_0591: Unknown result type (might be due to invalid IL or missing references)
			//IL_05be: Unknown result type (might be due to invalid IL or missing references)
			//IL_0679: Unknown result type (might be due to invalid IL or missing references)
			//IL_067f: Expected O, but got Unknown
			//IL_06be: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0774: Unknown result type (might be due to invalid IL or missing references)
			//IL_077a: Expected O, but got Unknown
			//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0874: Unknown result type (might be due to invalid IL or missing references)
			//IL_087a: Expected O, but got Unknown
			//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0991: Unknown result type (might be due to invalid IL or missing references)
			//IL_0997: Expected O, but got Unknown
			//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab4: Expected O, but got Unknown
			//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0c10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cee: Expected O, but got Unknown
			//IL_0d2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe1: Unknown result type (might be due to invalid IL or missing references)
			//IL_109f: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_11cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_13d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_14af: Unknown result type (might be due to invalid IL or missing references)
			//IL_14d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1621: Unknown result type (might be due to invalid IL or missing references)
			//IL_1656: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fa: Expected O, but got Unknown
			//IL_1739: Unknown result type (might be due to invalid IL or missing references)
			//IL_1762: Unknown result type (might be due to invalid IL or missing references)
			//IL_1800: Unknown result type (might be due to invalid IL or missing references)
			//IL_1806: Expected O, but got Unknown
			//IL_1845: Unknown result type (might be due to invalid IL or missing references)
			//IL_186e: Unknown result type (might be due to invalid IL or missing references)
			//IL_18fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_195e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1987: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a02: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a57: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1abc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1af7: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel39 = new XRLabel();
			bindingSource2 = new BindingSource(components);
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel35 = new XRLabel();
			xrLabel8 = new XRLabel();
			produto = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel28 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			xrLabel49 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel33 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel25 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel31 = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel1 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			((ISupportInitialize)bindingSource2).BeginInit();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 37f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 53f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel39 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupHeader1).HeightF = 121.4375f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel39).Borders = (BorderSide)15;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(0f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(697f, 121.4375f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader2,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel35,
				(XRControl)xrLabel8,
				(XRControl)produto,
				(XRControl)xrLabel52,
				(XRControl)xrLabel7,
				(XRControl)xrLabel32,
				(XRControl)xrLabel27,
				(XRControl)xrLabel28
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail1).HeightF = 100.9649f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3.000001f, 95.67325f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(690.3855f, 5.291664f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Arial", 8f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(3.000001f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(157.583f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Arial", 8f);
			((XRControl)produto).LocationFloat = new PointFloat(160.583f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(532.8024f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Arial", 8f);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(3.000001f, 47.83663f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(93.55975f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 8f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(96.55975f, 47.83663f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(166.771f, 47.83667f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Arial", 8f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(263.3307f, 47.83667f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(150.8959f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Arial", 8f);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(414.2266f, 47.83663f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(140.3112f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel28).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Arial", 8f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(554.5378f, 47.83663f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(138.8477f, 47.83667f);
			((XRControl)xrLabel28).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[7]
			{
				(XRControl)xrLabel49,
				(XRControl)lblProduto,
				(XRControl)xrLabel51,
				(XRControl)xrLabel24,
				(XRControl)xrLabel33,
				(XRControl)xrLabel53,
				(XRControl)xrLabel25
			});
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupHeader2).HeightF = 90.38159f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)GroupHeader2).StylePriority.UseFont = false;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Arial", 8f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(158.894f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)13;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Arial", 8f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(158.894f, 0f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(538.1061f, 50.48248f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Arial", 8f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(0f, 50.48247f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(96.55978f, 39.89911f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Arial", 8f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(96.55975f, 50.48246f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(166.771f, 39.89911f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Qtd";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Arial", 8f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(263.3307f, 50.48246f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(150.8959f, 39.89913f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Arial", 8f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(414.2266f, 50.48248f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(140.3112f, 39.65242f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)13;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Arial", 8f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(554.5378f, 50.48245f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(142.4623f, 39.89912f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvido";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel2,
				(XRControl)xrLabel31,
				(XRControl)xrLine1,
				(XRControl)xrLabel1
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 355.2957f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)lblMensagemRodape).Borders = (BorderSide)0;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(2.999996f, 325.2957f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(693.3854f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel2).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataToText", "{0:n2}")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 100.7523f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(696.3853f, 47.83668f);
			((XRControl)xrLabel2).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Responsavel")
			});
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Arial", 8f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(6.602795f, 262.1491f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(689.0014f, 45.88705f);
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(96.55976f, 256.86f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(500.993f, 5f);
			((XRControl)xrLabel1).Borders = (BorderSide)15;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(697.0001f, 355.2957f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			bindingSource1.DataSource = typeof(RegistroEntregaDto);
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 5f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.000001f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(693.3854f, 5f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(693.3854f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(693.3854f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)DetailReport
			});
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(45, 48, 37, 53);
			((XtraReport)this).PageHeight = 2500;
			((XtraReport)this).PageWidth = 790;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource2).EndInit();
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ComprovanteEntrega : Relatorio
	{
		private readonly RegistroEntregaDto _registroEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel39;

		private BindingSource bindingSource1;

		private BindingSource bindingSource2;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel49;

		private XRLabel lblProduto;

		private XRLabel xrLabel51;

		private XRLabel xrLabel24;

		private XRLabel xrLabel33;

		private XRLabel xrLabel53;

		private XRLabel xrLabel25;

		private XRLabel xrLabel8;

		private XRLabel produto;

		private XRLabel xrLabel52;

		private XRLabel xrLabel7;

		private XRLabel xrLabel32;

		private XRLabel xrLabel27;

		private XRLabel xrLabel28;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel29;

		private XRLabel xrLabel30;

		private XRLabel xrLabel31;

		private XRLine xrLine1;

		private XRLabel xrLabel1;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel localizacao;

		private XRLabel lblLocalizacao;

		private XRLabel xrLabel35;

		private XRLabel lblMarca;

		private XRLabel marca;

		private XRLabel lblMensagemRodape;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ComprovanteEntrega(RegistroEntregaDto registroEntrega)
		{
			_registroEntrega = registroEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			ProcessarMarca();
			ProcessarLocalizacao();
			((XtraReportBase)this).DataSource = new List<RegistroEntregaDto> { _registroEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ProcessarMarca()
		{
			if (_registroEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Marca)))
			{
				((XRControl)lblMarca).Visible = false;
				((XRControl)marca).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
		}

		private void ProcessarLocalizacao()
		{
			if (_registroEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Localizacao)))
			{
				((XRControl)lblMarca).Location = new System.Drawing.Point(((XRControl)lblLocalizacao).Location.X, ((XRControl)lblLocalizacao).Location.Y);
				((XRControl)marca).Location = new System.Drawing.Point(((XRControl)localizacao).Location.X, ((XRControl)localizacao).Location.Y);
				((XRControl)lblLocalizacao).Visible = false;
				((XRControl)localizacao).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Expected O, but got Unknown
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Expected O, but got Unknown
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Expected O, but got Unknown
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Expected O, but got Unknown
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Expected O, but got Unknown
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Expected O, but got Unknown
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Expected O, but got Unknown
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Expected O, but got Unknown
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Expected O, but got Unknown
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Expected O, but got Unknown
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected O, but got Unknown
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Expected O, but got Unknown
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Expected O, but got Unknown
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Expected O, but got Unknown
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Expected O, but got Unknown
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Expected O, but got Unknown
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Expected O, but got Unknown
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Expected O, but got Unknown
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Expected O, but got Unknown
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Expected O, but got Unknown
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Expected O, but got Unknown
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0269: Unknown result type (might be due to invalid IL or missing references)
			//IL_02be: Unknown result type (might be due to invalid IL or missing references)
			//IL_0396: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05db: Expected O, but got Unknown
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0644: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_071a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07db: Expected O, but got Unknown
			//IL_081b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d7: Expected O, but got Unknown
			//IL_0917: Unknown result type (might be due to invalid IL or missing references)
			//IL_0940: Unknown result type (might be due to invalid IL or missing references)
			//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d3: Expected O, but got Unknown
			//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad4: Expected O, but got Unknown
			//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf2: Expected O, but got Unknown
			//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d10: Expected O, but got Unknown
			//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e2e: Expected O, but got Unknown
			//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f46: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4c: Expected O, but got Unknown
			//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1143: Unknown result type (might be due to invalid IL or missing references)
			//IL_116c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1247: Unknown result type (might be due to invalid IL or missing references)
			//IL_1270: Unknown result type (might be due to invalid IL or missing references)
			//IL_134b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1374: Unknown result type (might be due to invalid IL or missing references)
			//IL_1432: Unknown result type (might be due to invalid IL or missing references)
			//IL_145b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1519: Unknown result type (might be due to invalid IL or missing references)
			//IL_1542: Unknown result type (might be due to invalid IL or missing references)
			//IL_161d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1646: Unknown result type (might be due to invalid IL or missing references)
			//IL_1721: Unknown result type (might be due to invalid IL or missing references)
			//IL_174a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1825: Unknown result type (might be due to invalid IL or missing references)
			//IL_184e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1929: Unknown result type (might be due to invalid IL or missing references)
			//IL_1952: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ada: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b77: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ba0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c26: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c2c: Expected O, but got Unknown
			//IL_1c6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c94: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d17: Expected O, but got Unknown
			//IL_1d56: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e98: Unknown result type (might be due to invalid IL or missing references)
			//IL_1efe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f53: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ff3: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel39 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			bindingSource2 = new BindingSource(components);
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			marca = new XRLabel();
			xrLabel35 = new XRLabel();
			localizacao = new XRLabel();
			xrLabel8 = new XRLabel();
			produto = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel28 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			lblMarca = new XRLabel();
			lblLocalizacao = new XRLabel();
			xrLabel49 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel33 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel25 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLabel29 = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel31 = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel1 = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)bindingSource2).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 50f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 50f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel39 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupHeader1).HeightF = 121.4375f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel39).Borders = (BorderSide)15;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(2.621933f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(1997.378f, 121.4375f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			bindingSource1.DataSource = typeof(RegistroEntregaDto);
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader2,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[10]
			{
				(XRControl)marca,
				(XRControl)xrLabel35,
				(XRControl)localizacao,
				(XRControl)xrLabel8,
				(XRControl)produto,
				(XRControl)xrLabel52,
				(XRControl)xrLabel7,
				(XRControl)xrLabel32,
				(XRControl)xrLabel27,
				(XRControl)xrLabel28
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail1).HeightF = 53.40685f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)marca).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)marca).Borders = (BorderSide)0;
			((XRControl)marca).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Marca")
			});
			((XRControl)marca).Dpi = 254f;
			((XRControl)marca).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)marca).LocationFloat = new PointFloat(787.5203f, 0f);
			((XRControl)marca).Name = "marca";
			((XRControl)marca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)marca).SizeF = new SizeF(192f, 47.83667f);
			((XRControl)marca).StylePriority.UseBorderDashStyle = false;
			((XRControl)marca).StylePriority.UseBorders = false;
			((XRControl)marca).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3.000001f, 47.83667f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(1994f, 5.570175f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)localizacao).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)localizacao).Borders = (BorderSide)0;
			((XRControl)localizacao).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Localizacao")
			});
			((XRControl)localizacao).Dpi = 254f;
			((XRControl)localizacao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)localizacao).LocationFloat = new PointFloat(979.5203f, 0f);
			((XRControl)localizacao).Name = "localizacao";
			((XRControl)localizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)localizacao).SizeF = new SizeF(191.9996f, 47.83667f);
			((XRControl)localizacao).StylePriority.UseBorderDashStyle = false;
			((XRControl)localizacao).StylePriority.UseBorders = false;
			((XRControl)localizacao).StylePriority.UseFont = false;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(3.000001f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(157.583f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)produto).LocationFloat = new PointFloat(160.583f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(626.9373f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(1171.52f, 0f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(108.4792f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1279.999f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1459.999f, 0f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1639.999f, 0f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel28).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(1819.999f, 0f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(179.2799f, 47.83667f);
			((XRControl)xrLabel28).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)lblMarca,
				(XRControl)lblLocalizacao,
				(XRControl)xrLabel49,
				(XRControl)lblProduto,
				(XRControl)xrLabel51,
				(XRControl)xrLabel24,
				(XRControl)xrLabel33,
				(XRControl)xrLabel53,
				(XRControl)xrLabel25
			});
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupHeader2).HeightF = 50.48248f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)GroupHeader2).StylePriority.UseFont = false;
			((XRControl)lblMarca).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblMarca).Borders = (BorderSide)9;
			((XRControl)lblMarca).Dpi = 254f;
			((XRControl)lblMarca).Font = new Font("Segoe UI", 9f);
			((XRControl)lblMarca).LocationFloat = new PointFloat(787.5214f, 0f);
			((XRControl)lblMarca).Name = "lblMarca";
			((XRControl)lblMarca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMarca).SizeF = new SizeF(192f, 50.48244f);
			((XRControl)lblMarca).StylePriority.UseBackColor = false;
			((XRControl)lblMarca).StylePriority.UseBorders = false;
			((XRControl)lblMarca).StylePriority.UseFont = false;
			((XRControl)lblMarca).StylePriority.UseTextAlignment = false;
			((XRControl)lblMarca).Text = "Marca";
			((XRControl)lblMarca).TextAlignment = (TextAlignment)2;
			((XRControl)lblLocalizacao).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblLocalizacao).Borders = (BorderSide)9;
			((XRControl)lblLocalizacao).Dpi = 254f;
			((XRControl)lblLocalizacao).Font = new Font("Segoe UI", 9f);
			((XRControl)lblLocalizacao).LocationFloat = new PointFloat(979.5214f, 0f);
			((XRControl)lblLocalizacao).Name = "lblLocalizacao";
			((XRControl)lblLocalizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblLocalizacao).SizeF = new SizeF(192f, 50.48245f);
			((XRControl)lblLocalizacao).StylePriority.UseBackColor = false;
			((XRControl)lblLocalizacao).StylePriority.UseBorders = false;
			((XRControl)lblLocalizacao).StylePriority.UseFont = false;
			((XRControl)lblLocalizacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblLocalizacao).Text = "Localização";
			((XRControl)lblLocalizacao).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(1.311088f, 0f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(157.5829f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)9;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Segoe UI", 9f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(158.894f, 0f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(628.6274f, 50.48247f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(1171.521f, 0f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(108.4789f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(1280f, 0f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Quantidade";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(1460f, 0f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(1640f, 0f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(179.9987f, 50.48245f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)13;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(1819.999f, 0f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(180.0009f, 50.48245f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvida";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel29,
				(XRControl)xrLabel30,
				(XRControl)xrLabel31,
				(XRControl)xrLine1,
				(XRControl)xrLabel1
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 351.9799f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)lblMensagemRodape).Borders = (BorderSide)13;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Segoe UI", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(1.310011f, 320.5691f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1998.69f, 31.4108f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(1359.638f, 56.75001f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(280.361f, 58.42001f);
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel29).Text = "Data da entrega:";
			((XRControl)xrLabel29).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel30).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Data", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(1640f, 56.75001f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(321.7455f, 58.42001f);
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Responsavel")
			});
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(6.602789f, 262.1491f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(1990.397f, 45.88708f);
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(576f, 256.86f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(850f, 5.29f);
			((XRControl)xrLabel1).Borders = (BorderSide)15;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1.310011f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1998.69f, 320.5691f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 5f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.000001f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(1997f, 5f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(1997f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(1997f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)DetailReport
			});
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(50, 50, 50, 50);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)bindingSource2).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class GarantiaEntregaPendente40Colunas : Relatorio
	{
		private readonly ControleEntregaGarantiaEntregaPendenteDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private PageHeaderBand PageHeader;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel27;

		private XRLabel produto;

		private XRLabel xrLabel24;

		private XRLabel lblProduto;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		private XRLabel xrLabel2;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel51;

		private XRLabel xrLabel53;

		private XRLabel xrLabel7;

		private XRLabel xrLabel52;

		private XRLabel xrLabel32;

		private XRLabel xrLabel33;

		private XRCrossBandLine xrCrossBandLine1;

		private XRLabel xrLabel1;

		private XRRichText dadosEmitente;

		private XRLabel lblMensagemFixaRodape;

		private XRRichText xrRichText3;

		private XRPageInfo xrPageInfo1;

		private XRLabel total;

		private XRLabel xrLabel35;

		private XRRichText xrRichText1;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell3;

		private XRTableCell xrTableCell4;

		private XRTable xrTable2;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private XRTableCell xrTableCell7;

		private XRTableCell xrTableCell8;

		private XRLabel xrLabel49;

		private XRTable xrTable3;

		private XRTableRow xrTableRow3;

		private XRTableCell xrTableCell9;

		private XRTableCell xrTableCell10;

		private XRTableCell xrTableCell11;

		private XRTableCell xrTableCell12;

		private XRTable xrTable4;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell13;

		private XRTableCell xrTableCell14;

		private XRTable xrTable5;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell15;

		private XRTableCell xrTableCell16;

		private XRLabel lblMensagemRodape;

		private XRCrossBandLine xrCrossBandLine2;

		public override string Descricao => "Garantia de entrega pendente";

		public override bool ExibirNoModuloRelatorios => false;

		public GarantiaEntregaPendente40Colunas(ControleEntregaGarantiaEntregaPendenteDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XRControl)total).Text = $"Total dos itens pendentes: {HelperDecimal.FormatarCampoValor(_controleEntrega.Itens.Sum((ControleEntregaItemDto x) => x.QuantidadePendente * x.PrecoUnitario), 2, 0, false)}";
			((XtraReportBase)this).DataSource = new List<ControleEntregaGarantiaEntregaPendenteDto> { _controleEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
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
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Expected O, but got Unknown
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Expected O, but got Unknown
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Expected O, but got Unknown
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Expected O, but got Unknown
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Expected O, but got Unknown
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Expected O, but got Unknown
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Expected O, but got Unknown
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Expected O, but got Unknown
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Expected O, but got Unknown
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Expected O, but got Unknown
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Expected O, but got Unknown
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Expected O, but got Unknown
			//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Expected O, but got Unknown
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Expected O, but got Unknown
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Expected O, but got Unknown
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Expected O, but got Unknown
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Expected O, but got Unknown
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Expected O, but got Unknown
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Expected O, but got Unknown
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Expected O, but got Unknown
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_0210: Expected O, but got Unknown
			//IL_0211: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Expected O, but got Unknown
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0226: Expected O, but got Unknown
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_0231: Expected O, but got Unknown
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_023c: Expected O, but got Unknown
			//IL_023d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Expected O, but got Unknown
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Expected O, but got Unknown
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Expected O, but got Unknown
			//IL_0269: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Expected O, but got Unknown
			//IL_0274: Unknown result type (might be due to invalid IL or missing references)
			//IL_027e: Expected O, but got Unknown
			//IL_027f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0289: Expected O, but got Unknown
			//IL_028a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0294: Expected O, but got Unknown
			//IL_0295: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Expected O, but got Unknown
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02aa: Expected O, but got Unknown
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b5: Expected O, but got Unknown
			//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c0: Expected O, but got Unknown
			//IL_0368: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0412: Unknown result type (might be due to invalid IL or missing references)
			//IL_0510: Unknown result type (might be due to invalid IL or missing references)
			//IL_053b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0624: Unknown result type (might be due to invalid IL or missing references)
			//IL_06df: Unknown result type (might be due to invalid IL or missing references)
			//IL_070a: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0924: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ea: Expected O, but got Unknown
			//IL_0a29: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b07: Expected O, but got Unknown
			//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c24: Expected O, but got Unknown
			//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3c: Expected O, but got Unknown
			//IL_0d7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3c: Expected O, but got Unknown
			//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f54: Expected O, but got Unknown
			//IL_0f93: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_114e: Unknown result type (might be due to invalid IL or missing references)
			//IL_125b: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f9: Expected O, but got Unknown
			//IL_1348: Unknown result type (might be due to invalid IL or missing references)
			//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1509: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a7: Expected O, but got Unknown
			//IL_15f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_16aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1861: Unknown result type (might be due to invalid IL or missing references)
			//IL_1867: Expected O, but got Unknown
			//IL_18b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1960: Unknown result type (might be due to invalid IL or missing references)
			//IL_19f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_19fe: Expected O, but got Unknown
			//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b01: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c20: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cbe: Expected O, but got Unknown
			//IL_1d0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e55: Expected O, but got Unknown
			//IL_1ea4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f58: Unknown result type (might be due to invalid IL or missing references)
			//IL_2077: Unknown result type (might be due to invalid IL or missing references)
			//IL_210f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2115: Expected O, but got Unknown
			//IL_2164: Unknown result type (might be due to invalid IL or missing references)
			//IL_220e: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_22ac: Expected O, but got Unknown
			//IL_22fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_23c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_23e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_24a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_25ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_25d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_26af: Unknown result type (might be due to invalid IL or missing references)
			//IL_26d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_27b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_27dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_286e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2874: Expected O, but got Unknown
			//IL_28b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_28dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_296a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2993: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a40: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a75: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b51: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ce3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d18: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dea: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ec0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f77: Unknown result type (might be due to invalid IL or missing references)
			//IL_300d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3013: Expected O, but got Unknown
			//IL_3052: Unknown result type (might be due to invalid IL or missing references)
			//IL_3087: Unknown result type (might be due to invalid IL or missing references)
			//IL_311b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3135: Unknown result type (might be due to invalid IL or missing references)
			//IL_3170: Unknown result type (might be due to invalid IL or missing references)
			//IL_31bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_31d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3210: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GarantiaEntregaPendente40Colunas));
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			PageHeader = new PageHeaderBand();
			xrRichText1 = new XRRichText();
			xrLabel1 = new XRLabel();
			dadosEmitente = new XRRichText();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel35 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel27 = new XRLabel();
			produto = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrTable5 = new XRTable();
			xrTableRow5 = new XRTableRow();
			xrTableCell15 = new XRTableCell();
			xrTableCell16 = new XRTableCell();
			xrTable4 = new XRTable();
			xrTableRow4 = new XRTableRow();
			xrTableCell13 = new XRTableCell();
			xrTableCell14 = new XRTableCell();
			xrTable3 = new XRTable();
			xrTableRow3 = new XRTableRow();
			xrTableCell9 = new XRTableCell();
			xrTableCell10 = new XRTableCell();
			xrTableCell11 = new XRTableCell();
			xrTableCell12 = new XRTableCell();
			xrTable2 = new XRTable();
			xrTableRow2 = new XRTableRow();
			xrTableCell5 = new XRTableCell();
			xrTableCell6 = new XRTableCell();
			xrTableCell7 = new XRTableCell();
			xrTableCell8 = new XRTableCell();
			xrTable1 = new XRTable();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			xrTableCell3 = new XRTableCell();
			xrTableCell4 = new XRTableCell();
			xrLabel49 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel24 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel33 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			total = new XRLabel();
			xrPageInfo1 = new XRPageInfo();
			xrRichText3 = new XRRichText();
			lblMensagemFixaRodape = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)xrRichText1).BeginInit();
			((ISupportInitialize)dadosEmitente).BeginInit();
			((ISupportInitialize)xrTable5).BeginInit();
			((ISupportInitialize)xrTable4).BeginInit();
			((ISupportInitialize)xrTable3).BeginInit();
			((ISupportInitialize)xrTable2).BeginInit();
			((ISupportInitialize)xrTable1).BeginInit();
			((ISupportInitialize)xrRichText3).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 40f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 45f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(ControleEntregaGarantiaEntregaPendenteDto);
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrRichText1,
				(XRControl)xrLabel1,
				(XRControl)dadosEmitente
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 395.8205f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrRichText1).Borders = (BorderSide)7;
			((XRControl)xrRichText1).Dpi = 254f;
			((XRControl)xrRichText1).Font = new Font("Arial", 8.25f, System.Drawing.FontStyle.Bold);
			((XRControl)xrRichText1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrRichText1).Name = "xrRichText1";
			((XRControl)xrRichText1).Padding = new PaddingInfo(21, 21, 0, 0, 254f);
			((XRRichTextBase)xrRichText1).SerializableRtfString = componentResourceManager.GetString("xrRichText1.SerializableRtfString");
			((XRControl)xrRichText1).SizeF = new SizeF(707.0001f, 103.3535f);
			((XRControl)xrRichText1).StylePriority.UseBorders = false;
			((XRControl)xrRichText1).StylePriority.UseFont = false;
			((XRControl)xrRichText1).StylePriority.UsePadding = false;
			((XRControl)xrLabel1).Borders = (BorderSide)7;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 266.6115f);
			xrLabel1.Multiline = true;
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(706.9999f, 129.209f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "CONTROLE DE ENTREGA Nº:\r\n [Numero!0000000000000]\r\nGERAÇÃO DE CRÉDITO";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)dadosEmitente).Borders = (BorderSide)13;
			((XRControl)dadosEmitente).Dpi = 254f;
			((XRControl)dadosEmitente).Font = new Font("Arial", 8f);
			((XRControl)dadosEmitente).LocationFloat = new PointFloat(0f, 103.3535f);
			((XRControl)dadosEmitente).Name = "dadosEmitente";
			((XRControl)dadosEmitente).Padding = new PaddingInfo(21, 21, 0, 0, 254f);
			((XRRichTextBase)dadosEmitente).SerializableRtfString = componentResourceManager.GetString("dadosEmitente.SerializableRtfString");
			((XRControl)dadosEmitente).SizeF = new SizeF(706.9999f, 163.258f);
			((XRControl)dadosEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).StylePriority.UseFont = false;
			((XRControl)dadosEmitente).StylePriority.UsePadding = false;
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[7]
			{
				(XRControl)xrLabel35,
				(XRControl)xrLabel32,
				(XRControl)xrLabel52,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel27,
				(XRControl)produto
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail1).HeightF = 101.2436f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3f, 95.67338f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(701f, 5.570175f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Arial", 8f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(285.1663f, 47.83671f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(212.7289f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Arial", 8f);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(2.999999f, 47.83671f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(69.43732f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 8f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(72.43732f, 47.83671f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(212.729f, 47.83667f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Arial", 8f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(2.999999f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(130.2426f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Arial", 8f);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(497.8952f, 47.8368f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(206.1048f, 47.83669f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Arial", 8f);
			((XRControl)produto).LocationFloat = new PointFloat(133.2426f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(570.7574f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)xrTable5,
				(XRControl)xrTable4,
				(XRControl)xrTable3,
				(XRControl)xrTable2,
				(XRControl)xrTable1,
				(XRControl)xrLabel49,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel24,
				(XRControl)lblProduto,
				(XRControl)xrLabel6,
				(XRControl)xrLabel5,
				(XRControl)xrLabel2,
				(XRControl)xrLabel33
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 445.0759f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrTable5).Borders = (BorderSide)0;
			((XRControl)xrTable5).Dpi = 254f;
			((XRControl)xrTable5).Font = new Font("Arial", 8f);
			((XRControl)xrTable5).LocationFloat = new PointFloat(0f, 201.2359f);
			((XRControl)xrTable5).Name = "xrTable5";
			xrTable5.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow5 });
			((XRControl)xrTable5).SizeF = new SizeF(706.9996f, 47.625f);
			((XRControl)xrTable5).StylePriority.UseBorders = false;
			((XRControl)xrTable5).StylePriority.UseFont = false;
			xrTableRow5.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell15, xrTableCell16 });
			((XRControl)xrTableRow5).Dpi = 254f;
			((XRControl)xrTableRow5).Name = "xrTableRow5";
			xrTableRow5.Weight = 1.0;
			((XRControl)xrTableCell15).Borders = (BorderSide)9;
			((XRControl)xrTableCell15).Dpi = 254f;
			((XRControl)xrTableCell15).Name = "xrTableCell15";
			((XRControl)xrTableCell15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell15).StylePriority.UseBorders = false;
			((XRControl)xrTableCell15).StylePriority.UsePadding = false;
			((XRControl)xrTableCell15).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell15).Text = "Complemento:";
			((XRControl)xrTableCell15).TextAlignment = (TextAlignment)1;
			xrTableCell15.Weight = 0.9749772373970206;
			((XRControl)xrTableCell16).Borders = (BorderSide)13;
			((XRControl)xrTableCell16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrTableCell16).Dpi = 254f;
			((XRControl)xrTableCell16).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell16).Name = "xrTableCell16";
			((XRControl)xrTableCell16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell16).StylePriority.UseBorders = false;
			((XRControl)xrTableCell16).StylePriority.UseFont = false;
			((XRControl)xrTableCell16).StylePriority.UsePadding = false;
			((XRControl)xrTableCell16).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell16).TextAlignment = (TextAlignment)1;
			xrTableCell16.Weight = 2.469995129870674;
			((XRControl)xrTable4).Borders = (BorderSide)0;
			((XRControl)xrTable4).Dpi = 254f;
			((XRControl)xrTable4).Font = new Font("Arial", 8f);
			((XRControl)xrTable4).LocationFloat = new PointFloat(0f, 57.93753f);
			((XRControl)xrTable4).Name = "xrTable4";
			xrTable4.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow4 });
			((XRControl)xrTable4).SizeF = new SizeF(707.0002f, 47.83665f);
			((XRControl)xrTable4).StylePriority.UseBorders = false;
			((XRControl)xrTable4).StylePriority.UseFont = false;
			xrTableRow4.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell13, xrTableCell14 });
			((XRControl)xrTableRow4).Dpi = 254f;
			((XRControl)xrTableRow4).Name = "xrTableRow4";
			xrTableRow4.Weight = 1.0;
			((XRControl)xrTableCell13).Borders = (BorderSide)9;
			((XRControl)xrTableCell13).Dpi = 254f;
			((XRControl)xrTableCell13).Name = "xrTableCell13";
			((XRControl)xrTableCell13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell13).StylePriority.UseBorders = false;
			((XRControl)xrTableCell13).StylePriority.UsePadding = false;
			((XRControl)xrTableCell13).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell13).Text = "Nome:";
			((XRControl)xrTableCell13).TextAlignment = (TextAlignment)1;
			xrTableCell13.Weight = 0.46696517557598394;
			((XRControl)xrTableCell14).Borders = (BorderSide)13;
			((XRControl)xrTableCell14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrTableCell14).Dpi = 254f;
			((XRControl)xrTableCell14).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell14).Name = "xrTableCell14";
			((XRControl)xrTableCell14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell14).StylePriority.UseBorders = false;
			((XRControl)xrTableCell14).StylePriority.UseFont = false;
			((XRControl)xrTableCell14).StylePriority.UsePadding = false;
			((XRControl)xrTableCell14).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell14).TextAlignment = (TextAlignment)1;
			xrTableCell14.Weight = 2.978010449048186;
			((XRControl)xrTable3).Borders = (BorderSide)0;
			((XRControl)xrTable3).Dpi = 254f;
			((XRControl)xrTable3).Font = new Font("Arial", 8f);
			((XRControl)xrTable3).LocationFloat = new PointFloat(0f, 296.4859f);
			((XRControl)xrTable3).Name = "xrTable3";
			xrTable3.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow3 });
			((XRControl)xrTable3).SizeF = new SizeF(706.9995f, 47.625f);
			((XRControl)xrTable3).StylePriority.UseBorders = false;
			((XRControl)xrTable3).StylePriority.UseFont = false;
			xrTableRow3.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell9, xrTableCell10, xrTableCell11, xrTableCell12 });
			((XRControl)xrTableRow3).Dpi = 254f;
			((XRControl)xrTableRow3).Name = "xrTableRow3";
			xrTableRow3.Weight = 1.0;
			((XRControl)xrTableCell9).Borders = (BorderSide)9;
			((XRControl)xrTableCell9).Dpi = 254f;
			((XRControl)xrTableCell9).Name = "xrTableCell9";
			((XRControl)xrTableCell9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell9).StylePriority.UseBorders = false;
			((XRControl)xrTableCell9).StylePriority.UsePadding = false;
			((XRControl)xrTableCell9).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell9).Text = "Município:";
			((XRControl)xrTableCell9).TextAlignment = (TextAlignment)1;
			xrTableCell9.Weight = 0.6895838425088469;
			((XRControl)xrTableCell10).Borders = (BorderSide)13;
			((XRControl)xrTableCell10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrTableCell10).Dpi = 254f;
			((XRControl)xrTableCell10).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell10).Name = "xrTableCell10";
			((XRControl)xrTableCell10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell10).StylePriority.UseBorders = false;
			((XRControl)xrTableCell10).StylePriority.UseFont = false;
			((XRControl)xrTableCell10).StylePriority.UsePadding = false;
			((XRControl)xrTableCell10).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell10).TextAlignment = (TextAlignment)1;
			xrTableCell10.Weight = 2.109457429847338;
			((XRControl)xrTableCell11).Borders = (BorderSide)12;
			((XRControl)xrTableCell11).Dpi = 254f;
			((XRControl)xrTableCell11).Name = "xrTableCell11";
			((XRControl)xrTableCell11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell11).StylePriority.UseBorders = false;
			((XRControl)xrTableCell11).StylePriority.UsePadding = false;
			((XRControl)xrTableCell11).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell11).Text = "UF:";
			((XRControl)xrTableCell11).TextAlignment = (TextAlignment)1;
			xrTableCell11.Weight = 0.2871187648427955;
			((XRControl)xrTableCell12).Borders = (BorderSide)12;
			((XRControl)xrTableCell12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrTableCell12).Dpi = 254f;
			((XRControl)xrTableCell12).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell12).Name = "xrTableCell12";
			((XRControl)xrTableCell12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell12).StylePriority.UseBorders = false;
			((XRControl)xrTableCell12).StylePriority.UseFont = false;
			((XRControl)xrTableCell12).StylePriority.UsePadding = false;
			((XRControl)xrTableCell12).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell12).TextAlignment = (TextAlignment)1;
			xrTableCell12.Weight = 0.3588119170919998;
			((XRControl)xrTable2).Borders = (BorderSide)0;
			((XRControl)xrTable2).Dpi = 254f;
			((XRControl)xrTable2).Font = new Font("Arial", 8f);
			((XRControl)xrTable2).LocationFloat = new PointFloat(0f, 248.8609f);
			((XRControl)xrTable2).Name = "xrTable2";
			xrTable2.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow2 });
			((XRControl)xrTable2).SizeF = new SizeF(706.9995f, 47.625f);
			((XRControl)xrTable2).StylePriority.UseBorders = false;
			((XRControl)xrTable2).StylePriority.UseFont = false;
			xrTableRow2.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell5, xrTableCell6, xrTableCell7, xrTableCell8 });
			((XRControl)xrTableRow2).Dpi = 254f;
			((XRControl)xrTableRow2).Name = "xrTableRow2";
			xrTableRow2.Weight = 1.0;
			((XRControl)xrTableCell5).Borders = (BorderSide)9;
			((XRControl)xrTableCell5).Dpi = 254f;
			((XRControl)xrTableCell5).Name = "xrTableCell5";
			((XRControl)xrTableCell5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell5).StylePriority.UseBorders = false;
			((XRControl)xrTableCell5).StylePriority.UsePadding = false;
			((XRControl)xrTableCell5).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell5).Text = "Bairro:";
			((XRControl)xrTableCell5).TextAlignment = (TextAlignment)1;
			xrTableCell5.Weight = 0.4669651384004984;
			((XRControl)xrTableCell6).Borders = (BorderSide)13;
			((XRControl)xrTableCell6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrTableCell6).Dpi = 254f;
			((XRControl)xrTableCell6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell6).Name = "xrTableCell6";
			((XRControl)xrTableCell6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell6).StylePriority.UseBorders = false;
			((XRControl)xrTableCell6).StylePriority.UseFont = false;
			((XRControl)xrTableCell6).StylePriority.UsePadding = false;
			((XRControl)xrTableCell6).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell6).TextAlignment = (TextAlignment)1;
			xrTableCell6.Weight = 1.693365602364628;
			((XRControl)xrTableCell7).Borders = (BorderSide)12;
			((XRControl)xrTableCell7).Dpi = 254f;
			((XRControl)xrTableCell7).Name = "xrTableCell7";
			((XRControl)xrTableCell7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell7).StylePriority.UseBorders = false;
			((XRControl)xrTableCell7).StylePriority.UsePadding = false;
			((XRControl)xrTableCell7).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell7).Text = "CEP:";
			((XRControl)xrTableCell7).TextAlignment = (TextAlignment)1;
			xrTableCell7.Weight = 0.385346063503743;
			((XRControl)xrTableCell8).Borders = (BorderSide)12;
			((XRControl)xrTableCell8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep")
			});
			((XRControl)xrTableCell8).Dpi = 254f;
			((XRControl)xrTableCell8).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell8).Name = "xrTableCell8";
			((XRControl)xrTableCell8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell8).StylePriority.UseBorders = false;
			((XRControl)xrTableCell8).StylePriority.UseFont = false;
			((XRControl)xrTableCell8).StylePriority.UsePadding = false;
			((XRControl)xrTableCell8).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell8).TextAlignment = (TextAlignment)1;
			xrTableCell8.Weight = 0.8992951500221109;
			((XRControl)xrTable1).Borders = (BorderSide)0;
			((XRControl)xrTable1).Dpi = 254f;
			((XRControl)xrTable1).Font = new Font("Arial", 8f);
			((XRControl)xrTable1).LocationFloat = new PointFloat(0f, 153.6109f);
			((XRControl)xrTable1).Name = "xrTable1";
			xrTable1.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow1 });
			((XRControl)xrTable1).SizeF = new SizeF(706.9995f, 47.625f);
			((XRControl)xrTable1).StylePriority.UseBorders = false;
			((XRControl)xrTable1).StylePriority.UseFont = false;
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell1, xrTableCell2, xrTableCell3, xrTableCell4 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 1.0;
			((XRControl)xrTableCell1).Borders = (BorderSide)9;
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell1).StylePriority.UseBorders = false;
			((XRControl)xrTableCell1).StylePriority.UsePadding = false;
			((XRControl)xrTableCell1).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell1).Text = "Endereço:";
			((XRControl)xrTableCell1).TextAlignment = (TextAlignment)1;
			xrTableCell1.Weight = 0.709074540694358;
			((XRControl)xrTableCell2).Borders = (BorderSide)13;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell2).StylePriority.UseBorders = false;
			((XRControl)xrTableCell2).StylePriority.UseFont = false;
			((XRControl)xrTableCell2).StylePriority.UsePadding = false;
			((XRControl)xrTableCell2).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell2).TextAlignment = (TextAlignment)1;
			xrTableCell2.Weight = 1.7511108418261232;
			((XRControl)xrTableCell3).Borders = (BorderSide)12;
			((XRControl)xrTableCell3).Dpi = 254f;
			((XRControl)xrTableCell3).Name = "xrTableCell3";
			((XRControl)xrTableCell3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell3).StylePriority.UseBorders = false;
			((XRControl)xrTableCell3).StylePriority.UsePadding = false;
			((XRControl)xrTableCell3).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell3).Text = "Nº:";
			((XRControl)xrTableCell3).TextAlignment = (TextAlignment)1;
			xrTableCell3.Weight = 0.25402038907273355;
			((XRControl)xrTableCell4).Borders = (BorderSide)12;
			((XRControl)xrTableCell4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrTableCell4).Dpi = 254f;
			((XRControl)xrTableCell4).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell4).Name = "xrTableCell4";
			((XRControl)xrTableCell4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell4).StylePriority.UseBorders = false;
			((XRControl)xrTableCell4).StylePriority.UseFont = false;
			((XRControl)xrTableCell4).StylePriority.UsePadding = false;
			((XRControl)xrTableCell4).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell4).TextAlignment = (TextAlignment)1;
			xrTableCell4.Weight = 0.7307661826977656;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Arial", 8f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(0f, 344.1109f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(124.7693f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)13;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Arial", 8f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(492.4226f, 394.5934f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(214.5776f, 50.48245f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Arial", 8f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(0.0001513958f, 394.5934f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(66.96468f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Arial", 8f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(66.96483f, 394.5934f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(212.729f, 50.48242f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Quantidade";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)13;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Arial", 8f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(124.7693f, 344.1109f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(582.2302f, 50.48248f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel6).Borders = (BorderSide)12;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(130.2428f, 105.7742f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(576.7574f, 47.83657f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Borders = (BorderSide)9;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Arial", 8f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 105.7742f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(130.2426f, 47.83665f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)15;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(707.0001f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Arial", 8f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(279.6938f, 394.5934f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(212.7289f, 50.48245f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)total,
				(XRControl)xrPageInfo1,
				(XRControl)xrRichText3,
				(XRControl)lblMensagemFixaRodape
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).Font = new Font("Segoe UI", 11f);
			((XRControl)GroupFooter1).HeightF = 271.4039f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)GroupFooter1).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)13;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(0f, 241.4039f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(706.9995f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)total).Borders = (BorderSide)5;
			((XRControl)total).Dpi = 254f;
			((XRControl)total).Font = new Font("Arial", 8f);
			((XRControl)total).LocationFloat = new PointFloat(0.0001513958f, 0f);
			((XRControl)total).Name = "total";
			((XRControl)total).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)total).SizeF = new SizeF(706.9994f, 52.91668f);
			((XRControl)total).StylePriority.UseBorders = false;
			((XRControl)total).StylePriority.UseFont = false;
			((XRControl)total).Text = "total";
			((XRControl)xrPageInfo1).Borders = (BorderSide)14;
			((XRControl)xrPageInfo1).Dpi = 254f;
			((XRControl)xrPageInfo1).Font = new Font("Arial", 8f);
			xrPageInfo1.Format = "{0:d' de 'MMMM' de 'yyyy}";
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(372.0458f, 192.3544f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			xrPageInfo1.PageInfo = (PageInfo)16;
			((XRControl)xrPageInfo1).SizeF = new SizeF(334.9542f, 49.04948f);
			((XRControl)xrPageInfo1).StylePriority.UseBorders = false;
			((XRControl)xrPageInfo1).StylePriority.UseFont = false;
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)4;
			((XRControl)xrRichText3).Borders = (BorderSide)11;
			((XRControl)xrRichText3).Dpi = 254f;
			((XRControl)xrRichText3).Font = new Font("Arial", 8f);
			((XRControl)xrRichText3).LocationFloat = new PointFloat(0f, 192.3544f);
			((XRControl)xrRichText3).Name = "xrRichText3";
			((XRRichTextBase)xrRichText3).SerializableRtfString = componentResourceManager.GetString("xrRichText3.SerializableRtfString");
			((XRControl)xrRichText3).SizeF = new SizeF(372.0458f, 49.04948f);
			((XRControl)xrRichText3).StylePriority.UseBorders = false;
			((XRControl)xrRichText3).StylePriority.UseFont = false;
			((XRControl)lblMensagemFixaRodape).Borders = (BorderSide)7;
			((XRControl)lblMensagemFixaRodape).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Observacao")
			});
			((XRControl)lblMensagemFixaRodape).Dpi = 254f;
			((XRControl)lblMensagemFixaRodape).Font = new Font("Arial", 8f);
			((XRControl)lblMensagemFixaRodape).LocationFloat = new PointFloat(0f, 52.91666f);
			lblMensagemFixaRodape.Multiline = true;
			((XRControl)lblMensagemFixaRodape).Name = "lblMensagemFixaRodape";
			((XRControl)lblMensagemFixaRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemFixaRodape).SizeF = new SizeF(707f, 139.4376f);
			((XRControl)lblMensagemFixaRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemFixaRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemFixaRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemFixaRodape).TextAlignment = (TextAlignment)1;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)GroupFooter1;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(704f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(704f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(704f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport
			});
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(42, 42, 40, 45);
			((XtraReport)this).PageHeight = 2500;
			((XtraReport)this).PageWidth = 791;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)xrRichText1).EndInit();
			((ISupportInitialize)dadosEmitente).EndInit();
			((ISupportInitialize)xrTable5).EndInit();
			((ISupportInitialize)xrTable4).EndInit();
			((ISupportInitialize)xrTable3).EndInit();
			((ISupportInitialize)xrTable2).EndInit();
			((ISupportInitialize)xrTable1).EndInit();
			((ISupportInitialize)xrRichText3).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class GarantiaEntregaPendente : Relatorio
	{
		private readonly ControleEntregaGarantiaEntregaPendenteDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private PageHeaderBand PageHeader;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel27;

		private XRLabel produto;

		private XRLabel xrLabel24;

		private XRLabel lblProduto;

		private XRLabel xrLabel22;

		private XRLabel xrLabel21;

		private XRLabel xrLabel20;

		private XRLabel xrLabel19;

		private XRLabel xrLabel18;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel51;

		private XRLabel xrLabel53;

		private XRLabel xrLabel49;

		private XRLabel xrLabel7;

		private XRLabel xrLabel52;

		private XRLabel xrLabel32;

		private XRLabel xrLabel33;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel localizacao;

		private XRLabel lblLocalizacao;

		private XRLabel xrLabel1;

		private XRRichText dadosEmitente;

		private XRLabel lblMensagemFixaRodape;

		private XRRichText xrRichText3;

		private XRPageInfo xrPageInfo1;

		private XRLabel total;

		private XRLabel xrLabel35;

		private XRLabel marca;

		private XRLabel lblMarca;

		private XRLabel lblMensagemRodape;

		private XRLabel xrLabel9;

		private XRLabel xrLabel10;

		private XRLabel xrLabel11;

		private XRLabel xrLabel12;

		private XRLabel telefone;

		private XRLabel xrLabel5;

		private PageFooterBand PageFooter;

		public override string Descricao => "Garantia de entrega pendente";

		public override bool ExibirNoModuloRelatorios => false;

		public GarantiaEntregaPendente(ControleEntregaGarantiaEntregaPendenteDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XRControl)total).Text = $"Total dos itens pendentes: {HelperDecimal.FormatarCampoValor(_controleEntrega.Itens.Sum((ControleEntregaItemDto x) => x.QuantidadePendente * x.PrecoUnitario), 2, 0, false)}";
			ProcessarMarca();
			ProcessarLocalizacao();
			((XtraReportBase)this).DataSource = new List<ControleEntregaGarantiaEntregaPendenteDto> { _controleEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ProcessarMarca()
		{
			if (_controleEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Marca)))
			{
				((XRControl)lblMarca).Visible = false;
				((XRControl)marca).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
		}

		private void ProcessarLocalizacao()
		{
			if (_controleEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Localizacao)))
			{
				((XRControl)lblMarca).Location = new System.Drawing.Point(((XRControl)lblLocalizacao).Location.X, ((XRControl)lblLocalizacao).Location.Y);
				((XRControl)marca).Location = new System.Drawing.Point(((XRControl)localizacao).Location.X, ((XRControl)localizacao).Location.Y);
				((XRControl)lblLocalizacao).Visible = false;
				((XRControl)localizacao).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
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
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Expected O, but got Unknown
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Expected O, but got Unknown
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Expected O, but got Unknown
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Expected O, but got Unknown
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Expected O, but got Unknown
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Expected O, but got Unknown
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Expected O, but got Unknown
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Expected O, but got Unknown
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Expected O, but got Unknown
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Expected O, but got Unknown
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Expected O, but got Unknown
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Expected O, but got Unknown
			//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Expected O, but got Unknown
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Expected O, but got Unknown
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Expected O, but got Unknown
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Expected O, but got Unknown
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Expected O, but got Unknown
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Expected O, but got Unknown
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Expected O, but got Unknown
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Expected O, but got Unknown
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_0210: Expected O, but got Unknown
			//IL_0211: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Expected O, but got Unknown
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0226: Expected O, but got Unknown
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_0231: Expected O, but got Unknown
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_023c: Expected O, but got Unknown
			//IL_023d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Expected O, but got Unknown
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Expected O, but got Unknown
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Expected O, but got Unknown
			//IL_0269: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Expected O, but got Unknown
			//IL_0274: Unknown result type (might be due to invalid IL or missing references)
			//IL_027e: Expected O, but got Unknown
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0339: Unknown result type (might be due to invalid IL or missing references)
			//IL_038e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0483: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0573: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0888: Unknown result type (might be due to invalid IL or missing references)
			//IL_088e: Expected O, but got Unknown
			//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0989: Unknown result type (might be due to invalid IL or missing references)
			//IL_098f: Expected O, but got Unknown
			//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aad: Expected O, but got Unknown
			//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bcb: Expected O, but got Unknown
			//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce4: Expected O, but got Unknown
			//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de5: Expected O, but got Unknown
			//IL_0e25: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0efe: Expected O, but got Unknown
			//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f67: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ffa: Expected O, but got Unknown
			//IL_103a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1063: Unknown result type (might be due to invalid IL or missing references)
			//IL_1288: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_138c: Unknown result type (might be due to invalid IL or missing references)
			//IL_13b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1490: Unknown result type (might be due to invalid IL or missing references)
			//IL_14b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1577: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_167b: Unknown result type (might be due to invalid IL or missing references)
			//IL_16a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_177f: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1883: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_193e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1944: Expected O, but got Unknown
			//IL_1984: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a39: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a62: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ae3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ae9: Expected O, but got Unknown
			//IL_1b29: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b52: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bde: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c07: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c93: Expected O, but got Unknown
			//IL_1cd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d89: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e33: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e39: Expected O, but got Unknown
			//IL_1e79: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ea2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f57: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fde: Expected O, but got Unknown
			//IL_201e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2047: Unknown result type (might be due to invalid IL or missing references)
			//IL_20d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_20fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_217e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2184: Expected O, but got Unknown
			//IL_21c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_21ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_227a: Unknown result type (might be due to invalid IL or missing references)
			//IL_22a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2350: Unknown result type (might be due to invalid IL or missing references)
			//IL_2385: Unknown result type (might be due to invalid IL or missing references)
			//IL_2461: Unknown result type (might be due to invalid IL or missing references)
			//IL_248a: Unknown result type (might be due to invalid IL or missing references)
			//IL_25e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2619: Unknown result type (might be due to invalid IL or missing references)
			//IL_269c: Unknown result type (might be due to invalid IL or missing references)
			//IL_26c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_274f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2779: Unknown result type (might be due to invalid IL or missing references)
			//IL_2843: Unknown result type (might be due to invalid IL or missing references)
			//IL_286d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2905: Unknown result type (might be due to invalid IL or missing references)
			//IL_290b: Expected O, but got Unknown
			//IL_294a: Unknown result type (might be due to invalid IL or missing references)
			//IL_297f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a13: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ab3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2acd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b08: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b69: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b92: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c13: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c19: Expected O, but got Unknown
			//IL_2c59: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c82: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d37: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ddb: Expected O, but got Unknown
			//IL_2e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e44: Unknown result type (might be due to invalid IL or missing references)
			//IL_2eb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2eba: Expected O, but got Unknown
			//IL_2efa: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f23: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fd9: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(GarantiaEntregaPendente));
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			PageHeader = new PageHeaderBand();
			xrLabel1 = new XRLabel();
			dadosEmitente = new XRRichText();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel35 = new XRLabel();
			localizacao = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel27 = new XRLabel();
			produto = new XRLabel();
			marca = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			lblMarca = new XRLabel();
			lblLocalizacao = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel24 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel33 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			total = new XRLabel();
			xrPageInfo1 = new XRPageInfo();
			xrRichText3 = new XRRichText();
			lblMensagemFixaRodape = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			xrLabel9 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel12 = new XRLabel();
			telefone = new XRLabel();
			xrLabel5 = new XRLabel();
			PageFooter = new PageFooterBand();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)dadosEmitente).BeginInit();
			((ISupportInitialize)xrRichText3).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 100f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 100f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(ControleEntregaGarantiaEntregaPendenteDto);
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)dadosEmitente
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 489.3743f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrLabel1).Borders = (BorderSide)7;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 352.0618f);
			xrLabel1.Multiline = true;
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1900f, 137.3125f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "CONTROLE DE ENTREGA Nº: [Numero!0000000000000]\r\nGERAÇÃO DE CRÉDITO";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)dadosEmitente).Borders = (BorderSide)15;
			((XRControl)dadosEmitente).Dpi = 254f;
			((XRControl)dadosEmitente).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)dadosEmitente).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)dadosEmitente).Name = "dadosEmitente";
			((XRControl)dadosEmitente).Padding = new PaddingInfo(20, 20, 20, 20, 254f);
			((XRRichTextBase)dadosEmitente).SerializableRtfString = componentResourceManager.GetString("dadosEmitente.SerializableRtfString");
			((XRControl)dadosEmitente).SizeF = new SizeF(1900f, 352.0618f);
			((XRControl)dadosEmitente).StylePriority.UseBorders = false;
			((XRControl)dadosEmitente).StylePriority.UseFont = false;
			((XRControl)dadosEmitente).StylePriority.UsePadding = false;
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel35,
				(XRControl)localizacao,
				(XRControl)xrLabel32,
				(XRControl)xrLabel52,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel27,
				(XRControl)produto,
				(XRControl)marca
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail1).HeightF = 53.40685f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3.000488f, 47.83667f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(1893.33f, 5.570175f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)localizacao).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)localizacao).Borders = (BorderSide)0;
			((XRControl)localizacao).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Localizacao")
			});
			((XRControl)localizacao).Dpi = 254f;
			((XRControl)localizacao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)localizacao).LocationFloat = new PointFloat(1078.805f, 0f);
			((XRControl)localizacao).Name = "localizacao";
			((XRControl)localizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)localizacao).SizeF = new SizeF(192f, 47.83667f);
			((XRControl)localizacao).StylePriority.UseBorderDashStyle = false;
			((XRControl)localizacao).StylePriority.UseBorders = false;
			((XRControl)localizacao).StylePriority.UseFont = false;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1539.118f, 0f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(1270.805f, 0f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(88.31201f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1359.117f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(130.2426f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1719.118f, 0f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(177.2124f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)produto).LocationFloat = new PointFloat(130.2426f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(756.5627f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)marca).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)marca).Borders = (BorderSide)0;
			((XRControl)marca).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Marca")
			});
			((XRControl)marca).Dpi = 254f;
			((XRControl)marca).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)marca).LocationFloat = new PointFloat(886.8054f, 0f);
			((XRControl)marca).Name = "marca";
			((XRControl)marca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)marca).SizeF = new SizeF(192f, 47.83667f);
			((XRControl)marca).StylePriority.UseBorderDashStyle = false;
			((XRControl)marca).StylePriority.UseBorders = false;
			((XRControl)marca).StylePriority.UseFont = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[27]
			{
				(XRControl)xrLabel9,
				(XRControl)xrLabel10,
				(XRControl)xrLabel11,
				(XRControl)xrLabel12,
				(XRControl)telefone,
				(XRControl)xrLabel5,
				(XRControl)lblMarca,
				(XRControl)lblLocalizacao,
				(XRControl)xrLabel49,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel24,
				(XRControl)lblProduto,
				(XRControl)xrLabel22,
				(XRControl)xrLabel21,
				(XRControl)xrLabel20,
				(XRControl)xrLabel19,
				(XRControl)xrLabel18,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16,
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)xrLabel13,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)xrLabel33
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 307.0585f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)lblMarca).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblMarca).Borders = (BorderSide)9;
			((XRControl)lblMarca).Dpi = 254f;
			((XRControl)lblMarca).Font = new Font("Segoe UI", 9f);
			((XRControl)lblMarca).LocationFloat = new PointFloat(886.8054f, 256.576f);
			((XRControl)lblMarca).Name = "lblMarca";
			((XRControl)lblMarca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMarca).SizeF = new SizeF(192f, 50.48244f);
			((XRControl)lblMarca).StylePriority.UseBackColor = false;
			((XRControl)lblMarca).StylePriority.UseBorders = false;
			((XRControl)lblMarca).StylePriority.UseFont = false;
			((XRControl)lblMarca).StylePriority.UseTextAlignment = false;
			((XRControl)lblMarca).Text = "Marca";
			((XRControl)lblMarca).TextAlignment = (TextAlignment)2;
			((XRControl)lblLocalizacao).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblLocalizacao).Borders = (BorderSide)9;
			((XRControl)lblLocalizacao).Dpi = 254f;
			((XRControl)lblLocalizacao).Font = new Font("Segoe UI", 9f);
			((XRControl)lblLocalizacao).LocationFloat = new PointFloat(1078.805f, 256.576f);
			((XRControl)lblLocalizacao).Name = "lblLocalizacao";
			((XRControl)lblLocalizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblLocalizacao).SizeF = new SizeF(192f, 50.48244f);
			((XRControl)lblLocalizacao).StylePriority.UseBackColor = false;
			((XRControl)lblLocalizacao).StylePriority.UseBorders = false;
			((XRControl)lblLocalizacao).StylePriority.UseFont = false;
			((XRControl)lblLocalizacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblLocalizacao).Text = "Localização";
			((XRControl)lblLocalizacao).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(0f, 256.576f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(130.2426f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)13;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(1719.118f, 256.576f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(180.8821f, 50.48245f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(1270.805f, 256.576f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(88.31201f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(1359.117f, 256.576f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Quantidade";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)9;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Segoe UI", 9f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(130.2426f, 256.576f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(756.5627f, 50.48247f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel22).Borders = (BorderSide)12;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1728.021f, 206.0935f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(171.979f, 50.48248f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1664.521f, 206.0935f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(63.49976f, 50.48251f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Text = "UF:";
			((XRControl)xrLabel20).Borders = (BorderSide)12;
			((XRControl)xrLabel20).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(571.5f, 206.0935f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(1093.021f, 50.4825f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Borders = (BorderSide)8;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(359.8333f, 206.0935f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(211.6667f, 50.48248f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Text = "Município:";
			((XRControl)xrLabel18).Borders = (BorderSide)12;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep", "{0:#####-###}")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(105.8333f, 206.0935f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(254f, 50.48248f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Borders = (BorderSide)9;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(0f, 206.0935f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(105.8333f, 50.48251f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Text = "CEP:";
			((XRControl)xrLabel16).Borders = (BorderSide)12;
			((XRControl)xrLabel16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1418.458f, 155.611f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(481.5416f, 50.48248f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Borders = (BorderSide)8;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1282.018f, 155.611f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(136.4404f, 50.48245f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Text = "Bairro:";
			((XRControl)xrLabel14).Borders = (BorderSide)12;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(291.0417f, 155.611f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(990.9764f, 50.48248f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Borders = (BorderSide)9;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 155.611f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(291.0417f, 50.48248f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Complemento:";
			((XRControl)xrLabel4).Borders = (BorderSide)12;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(145.5208f, 57.93752f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(1754.479f, 47.83665f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Borders = (BorderSide)9;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(0f, 57.93752f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(145.5208f, 47.83667f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Text = "Nome:";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)15;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1900f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(1539.117f, 256.576f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)total,
				(XRControl)lblMensagemFixaRodape
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).Font = new Font("Segoe UI", 11f);
			((XRControl)GroupFooter1).HeightF = 192.3543f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)GroupFooter1).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Segoe UI", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(0f, 50.04953f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1899.33f, 32.41081f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)total).Dpi = 254f;
			((XRControl)total).LocationFloat = new PointFloat(3f, 0f);
			((XRControl)total).Name = "total";
			((XRControl)total).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)total).SizeF = new SizeF(1893.33f, 52.91667f);
			((XRControl)total).Text = "total";
			((XRControl)xrPageInfo1).Borders = (BorderSide)0;
			((XRControl)xrPageInfo1).Dpi = 254f;
			((XRControl)xrPageInfo1).Font = new Font("Segoe UI", 11.25f);
			xrPageInfo1.Format = "{0:d' de 'MMMM' de 'yyyy}";
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(1203.446f, 0f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			xrPageInfo1.PageInfo = (PageInfo)16;
			((XRControl)xrPageInfo1).SizeF = new SizeF(695.884f, 50.04947f);
			((XRControl)xrPageInfo1).StylePriority.UseBorders = false;
			((XRControl)xrPageInfo1).StylePriority.UseFont = false;
			((XRControl)xrPageInfo1).StylePriority.UsePadding = false;
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)4;
			((XRControl)xrRichText3).Borders = (BorderSide)0;
			((XRControl)xrRichText3).Dpi = 254f;
			((XRControl)xrRichText3).Font = new Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrRichText3).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrRichText3).Name = "xrRichText3";
			((XRControl)xrRichText3).Padding = new PaddingInfo(15, 0, 0, 0, 254f);
			((XRRichTextBase)xrRichText3).SerializableRtfString = componentResourceManager.GetString("xrRichText3.SerializableRtfString");
			((XRControl)xrRichText3).SizeF = new SizeF(1203.446f, 50.04953f);
			((XRControl)xrRichText3).StylePriority.UseBorders = false;
			((XRControl)xrRichText3).StylePriority.UseFont = false;
			((XRControl)xrRichText3).StylePriority.UsePadding = false;
			((XRControl)lblMensagemFixaRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemFixaRodape).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Observacao")
			});
			((XRControl)lblMensagemFixaRodape).Dpi = 254f;
			((XRControl)lblMensagemFixaRodape).Font = new Font("Segoe UI", 8f);
			((XRControl)lblMensagemFixaRodape).LocationFloat = new PointFloat(0f, 52.91667f);
			lblMensagemFixaRodape.Multiline = true;
			((XRControl)lblMensagemFixaRodape).Name = "lblMensagemFixaRodape";
			((XRControl)lblMensagemFixaRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemFixaRodape).SizeF = new SizeF(1899.33f, 139.4376f);
			((XRControl)lblMensagemFixaRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemFixaRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemFixaRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemFixaRodape).TextAlignment = (TextAlignment)1;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 1.653646f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.307292f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)PageFooter;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(1896.33f, 82.20115f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(1896.33f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(1896.33f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3.307373f;
			((XRControl)xrLabel9).Borders = (BorderSide)9;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(476.2492f, 105.7741f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(151.6336f, 49.83669f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Text = "Endereço:";
			((XRControl)xrLabel10).Borders = (BorderSide)12;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(627.8829f, 105.7742f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(935.0704f, 49.83671f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel11).Borders = (BorderSide)8;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1562.953f, 105.7743f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(133.0469f, 49.83667f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Número:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel12).Borders = (BorderSide)12;
			((XRControl)xrLabel12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(1696f, 105.7743f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(204f, 49.83667f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)telefone).Borders = (BorderSide)8;
			((XRControl)telefone).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)telefone).Dpi = 254f;
			((XRControl)telefone).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)telefone).LocationFloat = new PointFloat(142.8743f, 105.7744f);
			((XRControl)telefone).Name = "telefone";
			((XRControl)telefone).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)telefone).SizeF = new SizeF(333.3749f, 50.83669f);
			((XRControl)telefone).StylePriority.UseBorders = false;
			((XRControl)telefone).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Borders = (BorderSide)9;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 105.7742f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(142.8743f, 49.8367f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrRichText3,
				(XRControl)xrPageInfo1
			});
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 82.46034f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport,
				(Band)PageFooter
			});
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(99, 99, 100, 100);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)dadosEmitente).EndInit();
			((ISupportInitialize)xrRichText3).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoControleEntregaConcluido40Colunas : Relatorio
	{
		private readonly ControleEntregaDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private PageHeaderBand PageHeader;

		private XRLabel xrLabel39;

		private XRLabel xrLabel1;

		private BindingSource bindingSource1;

		private CalculatedField calculatedField1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel5;

		private XRLabel xrLabel6;

		private XRLabel xrLabel23;

		private XRLabel xrLabel24;

		private XRLabel xrLabel25;

		private XRLabel xrLabel26;

		private XRLabel xrLabel27;

		private XRLabel xrLabel28;

		private DetailReportBand detailEntregas;

		private DetailBand Detail2;

		private XRLabel xrLabel7;

		private XRLabel xrLabel8;

		private XRLabel xrLabel29;

		private XRLabel xrLabel30;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private XRLabel xrLabel33;

		private XRLabel xrLabel32;

		private XRLabel xrLabel31;

		private XRLabel xrLabel35;

		private XRLabel xrLabel36;

		private XRLabel xrLabel37;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel34;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel46;

		private XRLabel xrLabel45;

		private XRLabel xrLabel44;

		private XRLabel xrLabel43;

		private XRLabel xrLabel42;

		private XRLabel xrLabel41;

		private XRLabel xrLabel40;

		private XRLabel xrLabel38;

		private XRLabel xrLabel54;

		private XRLabel xrLabel52;

		private XRLabel xrLabel50;

		private XRLabel xrLabel53;

		private XRLabel xrLabel51;

		private XRLabel xrLabel49;

		private XRLabel xrLabel48;

		private XRLabel xrLabel47;

		private XRLabel xrLabel55;

		private XRLabel xrLabel56;

		private XRLabel xrLabel58;

		private XRLabel xrLabel57;

		private XRLabel xrLabel60;

		private XRLabel xrLabel59;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel xrLabel61;

		private XRLabel xrLabel62;

		private XRTable xrTable4;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell13;

		private XRTableCell xrTableCell14;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell3;

		private XRTableCell xrTableCell4;

		private XRTable xrTable2;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private XRTableCell xrTableCell7;

		private XRTableCell xrTableCell8;

		private XRTable xrTable3;

		private XRTableRow xrTableRow3;

		private XRTableCell xrTableCell9;

		private XRTableCell xrTableCell10;

		private XRTableCell xrTableCell11;

		private XRTableCell xrTableCell12;

		private XRTable xrTable5;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell15;

		private XRTableCell xrTableCell16;

		private XRLabel lblMensagemRodape;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoControleEntregaConcluido40Colunas(ControleEntregaDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReportBase)this).DataSource = new List<ControleEntregaDto> { _controleEntrega };
			((XRControl)detailEntregas).Visible = _controleEntrega.RegistrosEntrega.Any();
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void xrLabel37_BeforePrint(object sender, PrintEventArgs e)
		{
			((XRControl)detailEntregas).Visible = _controleEntrega.RegistrosEntrega.Any();
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Expected O, but got Unknown
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Expected O, but got Unknown
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Expected O, but got Unknown
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Expected O, but got Unknown
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Expected O, but got Unknown
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Expected O, but got Unknown
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Expected O, but got Unknown
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Expected O, but got Unknown
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Expected O, but got Unknown
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected O, but got Unknown
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Expected O, but got Unknown
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Expected O, but got Unknown
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Expected O, but got Unknown
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0284: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Expected O, but got Unknown
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Expected O, but got Unknown
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bb: Expected O, but got Unknown
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Expected O, but got Unknown
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Expected O, but got Unknown
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dc: Expected O, but got Unknown
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Expected O, but got Unknown
			//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Expected O, but got Unknown
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Expected O, but got Unknown
			//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0308: Expected O, but got Unknown
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Expected O, but got Unknown
			//IL_0314: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Expected O, but got Unknown
			//IL_031f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Expected O, but got Unknown
			//IL_032a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0334: Expected O, but got Unknown
			//IL_0335: Unknown result type (might be due to invalid IL or missing references)
			//IL_033f: Expected O, but got Unknown
			//IL_0340: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_034b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0355: Expected O, but got Unknown
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_0360: Expected O, but got Unknown
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_036b: Expected O, but got Unknown
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0376: Expected O, but got Unknown
			//IL_0377: Unknown result type (might be due to invalid IL or missing references)
			//IL_0381: Expected O, but got Unknown
			//IL_0382: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Expected O, but got Unknown
			//IL_038d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Expected O, but got Unknown
			//IL_0398: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a2: Expected O, but got Unknown
			//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ad: Expected O, but got Unknown
			//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b8: Expected O, but got Unknown
			//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c3: Expected O, but got Unknown
			//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ce: Expected O, but got Unknown
			//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d9: Expected O, but got Unknown
			//IL_03da: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e4: Expected O, but got Unknown
			//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ef: Expected O, but got Unknown
			//IL_0476: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0520: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e9: Expected O, but got Unknown
			//IL_0628: Unknown result type (might be due to invalid IL or missing references)
			//IL_0651: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0734: Unknown result type (might be due to invalid IL or missing references)
			//IL_0942: Unknown result type (might be due to invalid IL or missing references)
			//IL_096f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a29: Expected O, but got Unknown
			//IL_0a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b29: Expected O, but got Unknown
			//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c29: Expected O, but got Unknown
			//IL_0c68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d24: Expected O, but got Unknown
			//IL_0d63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e02: Expected O, but got Unknown
			//IL_0e41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee5: Expected O, but got Unknown
			//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe5: Expected O, but got Unknown
			//IL_1024: Unknown result type (might be due to invalid IL or missing references)
			//IL_104d: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_136e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1374: Expected O, but got Unknown
			//IL_13c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1477: Unknown result type (might be due to invalid IL or missing references)
			//IL_1595: Unknown result type (might be due to invalid IL or missing references)
			//IL_162d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1633: Expected O, but got Unknown
			//IL_1682: Unknown result type (might be due to invalid IL or missing references)
			//IL_172c: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c9: Expected O, but got Unknown
			//IL_1818: Unknown result type (might be due to invalid IL or missing references)
			//IL_18cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a82: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a88: Expected O, but got Unknown
			//IL_1ad7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b81: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c18: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c1e: Expected O, but got Unknown
			//IL_1c6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d21: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ed7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1edd: Expected O, but got Unknown
			//IL_1f2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd6: Unknown result type (might be due to invalid IL or missing references)
			//IL_206d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2073: Expected O, but got Unknown
			//IL_20c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2176: Unknown result type (might be due to invalid IL or missing references)
			//IL_2282: Unknown result type (might be due to invalid IL or missing references)
			//IL_231a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2320: Expected O, but got Unknown
			//IL_236f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2434: Unknown result type (might be due to invalid IL or missing references)
			//IL_245d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2538: Unknown result type (might be due to invalid IL or missing references)
			//IL_2561: Unknown result type (might be due to invalid IL or missing references)
			//IL_263b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2664: Unknown result type (might be due to invalid IL or missing references)
			//IL_273e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2767: Unknown result type (might be due to invalid IL or missing references)
			//IL_2825: Unknown result type (might be due to invalid IL or missing references)
			//IL_285a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2925: Unknown result type (might be due to invalid IL or missing references)
			//IL_294e: Unknown result type (might be due to invalid IL or missing references)
			//IL_29ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_29d4: Expected O, but got Unknown
			//IL_2a14: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ada: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b03: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bea: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cee: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f09: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f32: Unknown result type (might be due to invalid IL or missing references)
			//IL_300c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3035: Unknown result type (might be due to invalid IL or missing references)
			//IL_30f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_311c: Unknown result type (might be due to invalid IL or missing references)
			//IL_31da: Unknown result type (might be due to invalid IL or missing references)
			//IL_3203: Unknown result type (might be due to invalid IL or missing references)
			//IL_32de: Unknown result type (might be due to invalid IL or missing references)
			//IL_3307: Unknown result type (might be due to invalid IL or missing references)
			//IL_33f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3422: Unknown result type (might be due to invalid IL or missing references)
			//IL_34c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_34cf: Expected O, but got Unknown
			//IL_350e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3537: Unknown result type (might be due to invalid IL or missing references)
			//IL_35e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_360d: Unknown result type (might be due to invalid IL or missing references)
			//IL_36ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_36b4: Expected O, but got Unknown
			//IL_36f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_371d: Unknown result type (might be due to invalid IL or missing references)
			//IL_38d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3906: Unknown result type (might be due to invalid IL or missing references)
			//IL_39c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_39cc: Expected O, but got Unknown
			//IL_3a0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a34: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ade: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ae4: Expected O, but got Unknown
			//IL_3b23: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bde: Unknown result type (might be due to invalid IL or missing references)
			//IL_3be4: Expected O, but got Unknown
			//IL_3c23: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cfb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d01: Expected O, but got Unknown
			//IL_3d40: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d69: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e13: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e19: Expected O, but got Unknown
			//IL_3e58: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e81: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_4160: Unknown result type (might be due to invalid IL or missing references)
			//IL_4195: Unknown result type (might be due to invalid IL or missing references)
			//IL_423e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4267: Unknown result type (might be due to invalid IL or missing references)
			//IL_4303: Unknown result type (might be due to invalid IL or missing references)
			//IL_432c: Unknown result type (might be due to invalid IL or missing references)
			//IL_43c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_43f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_448d: Unknown result type (might be due to invalid IL or missing references)
			//IL_44b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_4536: Unknown result type (might be due to invalid IL or missing references)
			//IL_453c: Expected O, but got Unknown
			//IL_457c: Unknown result type (might be due to invalid IL or missing references)
			//IL_45a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_4615: Unknown result type (might be due to invalid IL or missing references)
			//IL_461b: Expected O, but got Unknown
			//IL_465b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4684: Unknown result type (might be due to invalid IL or missing references)
			//IL_46f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_46ff: Expected O, but got Unknown
			//IL_473f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4768: Unknown result type (might be due to invalid IL or missing references)
			//IL_47d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_47de: Expected O, but got Unknown
			//IL_481e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4847: Unknown result type (might be due to invalid IL or missing references)
			//IL_48d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_48fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_498e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4994: Expected O, but got Unknown
			//IL_49d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a09: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a80: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ad5: Unknown result type (might be due to invalid IL or missing references)
			//IL_4b20: Unknown result type (might be due to invalid IL or missing references)
			//IL_4b3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4b75: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			PageHeader = new PageHeaderBand();
			xrLabel1 = new XRLabel();
			xrLabel39 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			calculatedField1 = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel62 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel54 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel50 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel28 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrTable5 = new XRTable();
			xrTableRow5 = new XRTableRow();
			xrTableCell15 = new XRTableCell();
			xrTableCell16 = new XRTableCell();
			xrTable3 = new XRTable();
			xrTableRow3 = new XRTableRow();
			xrTableCell9 = new XRTableCell();
			xrTableCell10 = new XRTableCell();
			xrTableCell11 = new XRTableCell();
			xrTableCell12 = new XRTableCell();
			xrTable2 = new XRTable();
			xrTableRow2 = new XRTableRow();
			xrTableCell5 = new XRTableCell();
			xrTableCell6 = new XRTableCell();
			xrTableCell7 = new XRTableCell();
			xrTableCell8 = new XRTableCell();
			xrTable1 = new XRTable();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			xrTableCell3 = new XRTableCell();
			xrTableCell4 = new XRTableCell();
			xrTable4 = new XRTable();
			xrTableRow4 = new XRTableRow();
			xrTableCell13 = new XRTableCell();
			xrTableCell14 = new XRTableCell();
			xrLabel57 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel23 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel25 = new XRLabel();
			detailEntregas = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel55 = new XRLabel();
			xrLabel48 = new XRLabel();
			xrLabel35 = new XRLabel();
			xrLabel36 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel29 = new XRLabel();
			xrLabel30 = new XRLabel();
			DetailReport2 = new DetailReportBand();
			Detail3 = new DetailBand();
			xrLabel61 = new XRLabel();
			xrLabel56 = new XRLabel();
			xrLabel47 = new XRLabel();
			xrLabel33 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel31 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			xrLabel34 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLabel59 = new XRLabel();
			xrLabel46 = new XRLabel();
			xrLabel45 = new XRLabel();
			xrLabel44 = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel42 = new XRLabel();
			xrLabel41 = new XRLabel();
			xrLabel40 = new XRLabel();
			xrLabel38 = new XRLabel();
			xrLabel60 = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)xrTable5).BeginInit();
			((ISupportInitialize)xrTable3).BeginInit();
			((ISupportInitialize)xrTable2).BeginInit();
			((ISupportInitialize)xrTable1).BeginInit();
			((ISupportInitialize)xrTable4).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 46f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 50f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel39
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 121.4375f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "calculatedField1")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(3.307297f, 70.95509f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(702.1927f, 50.48243f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel39).Borders = (BorderSide)2;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(3.307297f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(702.4979f, 70.9551f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			bindingSource1.DataSource = typeof(ControleEntregaDto);
			calculatedField1.DisplayName = "numeroDocumento";
			calculatedField1.Expression = "Concat('Nº do documento: ', [Numero])";
			calculatedField1.Name = "calculatedField1";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel62,
				(XRControl)xrLabel58,
				(XRControl)xrLabel54,
				(XRControl)xrLabel52,
				(XRControl)xrLabel50,
				(XRControl)xrLabel26,
				(XRControl)xrLabel27,
				(XRControl)xrLabel28
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 100.965f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel62).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel62).Borders = (BorderSide)8;
			((XRControl)xrLabel62).Dpi = 254f;
			((XRControl)xrLabel62).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel62).LocationFloat = new PointFloat(3.307292f, 95.6733f);
			((XRControl)xrLabel62).Name = "xrLabel62";
			((XRControl)xrLabel62).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel62).SizeF = new SizeF(699.2952f, 5.291672f);
			((XRControl)xrLabel62).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel62).StylePriority.UseBorders = false;
			((XRControl)xrLabel62).StylePriority.UseFont = false;
			((XRControl)xrLabel62).StylePriority.UsePadding = false;
			((XRControl)xrLabel62).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel62).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel58).Borders = (BorderSide)0;
			((XRControl)xrLabel58).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel58).Dpi = 254f;
			((XRControl)xrLabel58).Font = new Font("Arial", 8f);
			((XRControl)xrLabel58).LocationFloat = new PointFloat(233.2258f, 47.83663f);
			((XRControl)xrLabel58).Name = "xrLabel58";
			((XRControl)xrLabel58).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel58).SizeF = new SizeF(157.7708f, 47.83667f);
			((XRControl)xrLabel58).StylePriority.UseBorders = false;
			((XRControl)xrLabel58).StylePriority.UseFont = false;
			((XRControl)xrLabel58).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel58).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel54).Borders = (BorderSide)0;
			((XRControl)xrLabel54).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel54).Dpi = 254f;
			((XRControl)xrLabel54).Font = new Font("Arial", 8f);
			((XRControl)xrLabel54).LocationFloat = new PointFloat(390.9966f, 47.83663f);
			((XRControl)xrLabel54).Name = "xrLabel54";
			((XRControl)xrLabel54).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel54).SizeF = new SizeF(159.1426f, 47.83667f);
			((XRControl)xrLabel54).StylePriority.UseBorders = false;
			((XRControl)xrLabel54).StylePriority.UseFont = false;
			((XRControl)xrLabel54).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel54).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Arial", 8f);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(3.307292f, 47.83663f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(70.77597f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel50).Borders = (BorderSide)0;
			((XRControl)xrLabel50).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Arial", 8f);
			((XRControl)xrLabel50).LocationFloat = new PointFloat(3.307292f, 0f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(191.9787f, 47.83667f);
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel26).Borders = (BorderSide)0;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Arial", 8f);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(195.286f, 0f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(510.5193f, 47.83667f);
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Arial", 8f);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(74.08325f, 47.83663f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(159.1426f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Arial", 8f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(550.1392f, 47.83663f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(155.6661f, 47.83667f);
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[15]
			{
				(XRControl)xrTable5,
				(XRControl)xrTable3,
				(XRControl)xrTable2,
				(XRControl)xrTable1,
				(XRControl)xrTable4,
				(XRControl)xrLabel57,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel49,
				(XRControl)xrLabel2,
				(XRControl)xrLabel5,
				(XRControl)xrLabel6,
				(XRControl)xrLabel23,
				(XRControl)xrLabel24,
				(XRControl)xrLabel25
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 434.7044f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrTable5).Borders = (BorderSide)0;
			((XRControl)xrTable5).Dpi = 254f;
			((XRControl)xrTable5).Font = new Font("Arial", 8f);
			((XRControl)xrTable5).LocationFloat = new PointFloat(3.30728f, 201.4476f);
			((XRControl)xrTable5).Name = "xrTable5";
			xrTable5.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow5 });
			((XRControl)xrTable5).SizeF = new SizeF(702.498f, 47.62498f);
			((XRControl)xrTable5).StylePriority.UseBorders = false;
			((XRControl)xrTable5).StylePriority.UseFont = false;
			xrTableRow5.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell15, xrTableCell16 });
			((XRControl)xrTableRow5).Dpi = 254f;
			((XRControl)xrTableRow5).Name = "xrTableRow5";
			xrTableRow5.Weight = 1.0;
			((XRControl)xrTableCell15).Borders = (BorderSide)8;
			((XRControl)xrTableCell15).Dpi = 254f;
			((XRControl)xrTableCell15).Name = "xrTableCell15";
			((XRControl)xrTableCell15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell15).StylePriority.UseBorders = false;
			((XRControl)xrTableCell15).StylePriority.UsePadding = false;
			((XRControl)xrTableCell15).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell15).Text = "Complemento:";
			((XRControl)xrTableCell15).TextAlignment = (TextAlignment)1;
			xrTableCell15.Weight = 1.085917950720286;
			((XRControl)xrTableCell16).Borders = (BorderSide)9;
			((XRControl)xrTableCell16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrTableCell16).Dpi = 254f;
			((XRControl)xrTableCell16).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell16).Name = "xrTableCell16";
			((XRControl)xrTableCell16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell16).StylePriority.UseBorders = false;
			((XRControl)xrTableCell16).StylePriority.UseFont = false;
			((XRControl)xrTableCell16).StylePriority.UsePadding = false;
			((XRControl)xrTableCell16).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell16).TextAlignment = (TextAlignment)1;
			xrTableCell16.Weight = 2.5067914890999363;
			((XRControl)xrTable3).Borders = (BorderSide)0;
			((XRControl)xrTable3).Dpi = 254f;
			((XRControl)xrTable3).Font = new Font("Arial", 8f);
			((XRControl)xrTable3).LocationFloat = new PointFloat(3.307298f, 296.6976f);
			((XRControl)xrTable3).Name = "xrTable3";
			xrTable3.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow3 });
			((XRControl)xrTable3).SizeF = new SizeF(702.498f, 47.625f);
			((XRControl)xrTable3).StylePriority.UseBorders = false;
			((XRControl)xrTable3).StylePriority.UseFont = false;
			xrTableRow3.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell9, xrTableCell10, xrTableCell11, xrTableCell12 });
			((XRControl)xrTableRow3).Dpi = 254f;
			((XRControl)xrTableRow3).Name = "xrTableRow3";
			xrTableRow3.Weight = 1.0;
			((XRControl)xrTableCell9).Borders = (BorderSide)8;
			((XRControl)xrTableCell9).Dpi = 254f;
			((XRControl)xrTableCell9).Name = "xrTableCell9";
			((XRControl)xrTableCell9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell9).StylePriority.UseBorders = false;
			((XRControl)xrTableCell9).StylePriority.UsePadding = false;
			((XRControl)xrTableCell9).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell9).Text = "Município:";
			((XRControl)xrTableCell9).TextAlignment = (TextAlignment)1;
			xrTableCell9.Weight = 0.6895838425088469;
			((XRControl)xrTableCell10).Borders = (BorderSide)13;
			((XRControl)xrTableCell10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrTableCell10).Dpi = 254f;
			((XRControl)xrTableCell10).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell10).Name = "xrTableCell10";
			((XRControl)xrTableCell10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell10).StylePriority.UseBorders = false;
			((XRControl)xrTableCell10).StylePriority.UseFont = false;
			((XRControl)xrTableCell10).StylePriority.UsePadding = false;
			((XRControl)xrTableCell10).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell10).TextAlignment = (TextAlignment)1;
			xrTableCell10.Weight = 2.109457429847338;
			((XRControl)xrTableCell11).Borders = (BorderSide)12;
			((XRControl)xrTableCell11).Dpi = 254f;
			((XRControl)xrTableCell11).Name = "xrTableCell11";
			((XRControl)xrTableCell11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell11).StylePriority.UseBorders = false;
			((XRControl)xrTableCell11).StylePriority.UsePadding = false;
			((XRControl)xrTableCell11).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell11).Text = "UF:";
			((XRControl)xrTableCell11).TextAlignment = (TextAlignment)1;
			xrTableCell11.Weight = 0.2871187648427955;
			((XRControl)xrTableCell12).Borders = (BorderSide)8;
			((XRControl)xrTableCell12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrTableCell12).Dpi = 254f;
			((XRControl)xrTableCell12).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell12).Name = "xrTableCell12";
			((XRControl)xrTableCell12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell12).StylePriority.UseBorders = false;
			((XRControl)xrTableCell12).StylePriority.UseFont = false;
			((XRControl)xrTableCell12).StylePriority.UsePadding = false;
			((XRControl)xrTableCell12).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell12).TextAlignment = (TextAlignment)1;
			xrTableCell12.Weight = 0.3588119170919998;
			((XRControl)xrTable2).Borders = (BorderSide)0;
			((XRControl)xrTable2).Dpi = 254f;
			((XRControl)xrTable2).Font = new Font("Arial", 8f);
			((XRControl)xrTable2).LocationFloat = new PointFloat(3.307306f, 249.0726f);
			((XRControl)xrTable2).Name = "xrTable2";
			xrTable2.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow2 });
			((XRControl)xrTable2).SizeF = new SizeF(702.4979f, 47.625f);
			((XRControl)xrTable2).StylePriority.UseBorders = false;
			((XRControl)xrTable2).StylePriority.UseFont = false;
			xrTableRow2.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell5, xrTableCell6, xrTableCell7, xrTableCell8 });
			((XRControl)xrTableRow2).Dpi = 254f;
			((XRControl)xrTableRow2).Name = "xrTableRow2";
			xrTableRow2.Weight = 1.0;
			((XRControl)xrTableCell5).Borders = (BorderSide)8;
			((XRControl)xrTableCell5).Dpi = 254f;
			((XRControl)xrTableCell5).Name = "xrTableCell5";
			((XRControl)xrTableCell5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell5).StylePriority.UseBorders = false;
			((XRControl)xrTableCell5).StylePriority.UsePadding = false;
			((XRControl)xrTableCell5).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell5).Text = "Bairro:";
			((XRControl)xrTableCell5).TextAlignment = (TextAlignment)1;
			xrTableCell5.Weight = 0.4669650599967169;
			((XRControl)xrTableCell6).Borders = (BorderSide)13;
			((XRControl)xrTableCell6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrTableCell6).Dpi = 254f;
			((XRControl)xrTableCell6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell6).Name = "xrTableCell6";
			((XRControl)xrTableCell6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell6).StylePriority.UseBorders = false;
			((XRControl)xrTableCell6).StylePriority.UseFont = false;
			((XRControl)xrTableCell6).StylePriority.UsePadding = false;
			((XRControl)xrTableCell6).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell6).TextAlignment = (TextAlignment)1;
			xrTableCell6.Weight = 1.693365602364628;
			((XRControl)xrTableCell7).Borders = (BorderSide)12;
			((XRControl)xrTableCell7).Dpi = 254f;
			((XRControl)xrTableCell7).Name = "xrTableCell7";
			((XRControl)xrTableCell7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell7).StylePriority.UseBorders = false;
			((XRControl)xrTableCell7).StylePriority.UsePadding = false;
			((XRControl)xrTableCell7).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell7).Text = "CEP:";
			((XRControl)xrTableCell7).TextAlignment = (TextAlignment)1;
			xrTableCell7.Weight = 0.385346063503743;
			((XRControl)xrTableCell8).Borders = (BorderSide)8;
			((XRControl)xrTableCell8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep")
			});
			((XRControl)xrTableCell8).Dpi = 254f;
			((XRControl)xrTableCell8).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell8).Name = "xrTableCell8";
			((XRControl)xrTableCell8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell8).StylePriority.UseBorders = false;
			((XRControl)xrTableCell8).StylePriority.UseFont = false;
			((XRControl)xrTableCell8).StylePriority.UsePadding = false;
			((XRControl)xrTableCell8).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell8).TextAlignment = (TextAlignment)1;
			xrTableCell8.Weight = 0.8992951500221109;
			((XRControl)xrTable1).Borders = (BorderSide)0;
			((XRControl)xrTable1).Dpi = 254f;
			((XRControl)xrTable1).Font = new Font("Arial", 8f);
			((XRControl)xrTable1).LocationFloat = new PointFloat(3.306501f, 153.8226f);
			((XRControl)xrTable1).Name = "xrTable1";
			xrTable1.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow1 });
			((XRControl)xrTable1).SizeF = new SizeF(702.4987f, 47.625f);
			((XRControl)xrTable1).StylePriority.UseBorders = false;
			((XRControl)xrTable1).StylePriority.UseFont = false;
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell1, xrTableCell2, xrTableCell3, xrTableCell4 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 1.0;
			((XRControl)xrTableCell1).Borders = (BorderSide)8;
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell1).StylePriority.UseBorders = false;
			((XRControl)xrTableCell1).StylePriority.UsePadding = false;
			((XRControl)xrTableCell1).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell1).Text = "Endereço:";
			((XRControl)xrTableCell1).TextAlignment = (TextAlignment)1;
			xrTableCell1.Weight = 0.709074540694358;
			((XRControl)xrTableCell2).Borders = (BorderSide)13;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell2).StylePriority.UseBorders = false;
			((XRControl)xrTableCell2).StylePriority.UseFont = false;
			((XRControl)xrTableCell2).StylePriority.UsePadding = false;
			((XRControl)xrTableCell2).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell2).TextAlignment = (TextAlignment)1;
			xrTableCell2.Weight = 1.7511108418261232;
			((XRControl)xrTableCell3).Borders = (BorderSide)12;
			((XRControl)xrTableCell3).Dpi = 254f;
			((XRControl)xrTableCell3).Name = "xrTableCell3";
			((XRControl)xrTableCell3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell3).StylePriority.UseBorders = false;
			((XRControl)xrTableCell3).StylePriority.UsePadding = false;
			((XRControl)xrTableCell3).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell3).Text = "Nº:";
			((XRControl)xrTableCell3).TextAlignment = (TextAlignment)1;
			xrTableCell3.Weight = 0.25402038907273355;
			((XRControl)xrTableCell4).Borders = (BorderSide)8;
			((XRControl)xrTableCell4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrTableCell4).Dpi = 254f;
			((XRControl)xrTableCell4).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell4).Name = "xrTableCell4";
			((XRControl)xrTableCell4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell4).StylePriority.UseBorders = false;
			((XRControl)xrTableCell4).StylePriority.UseFont = false;
			((XRControl)xrTableCell4).StylePriority.UsePadding = false;
			((XRControl)xrTableCell4).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell4).TextAlignment = (TextAlignment)1;
			xrTableCell4.Weight = 0.7307661826977656;
			((XRControl)xrTable4).Borders = (BorderSide)0;
			((XRControl)xrTable4).Dpi = 254f;
			((XRControl)xrTable4).Font = new Font("Arial", 8f);
			((XRControl)xrTable4).LocationFloat = new PointFloat(3.306501f, 57.93755f);
			((XRControl)xrTable4).Name = "xrTable4";
			xrTable4.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow4 });
			((XRControl)xrTable4).SizeF = new SizeF(702.4987f, 47.83665f);
			((XRControl)xrTable4).StylePriority.UseBorders = false;
			((XRControl)xrTable4).StylePriority.UseFont = false;
			xrTableRow4.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell13, xrTableCell14 });
			((XRControl)xrTableRow4).Dpi = 254f;
			((XRControl)xrTableRow4).Name = "xrTableRow4";
			xrTableRow4.Weight = 1.0;
			((XRControl)xrTableCell13).Borders = (BorderSide)8;
			((XRControl)xrTableCell13).Dpi = 254f;
			((XRControl)xrTableCell13).Name = "xrTableCell13";
			((XRControl)xrTableCell13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell13).StylePriority.UseBorders = false;
			((XRControl)xrTableCell13).StylePriority.UsePadding = false;
			((XRControl)xrTableCell13).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell13).Text = "Nome:";
			((XRControl)xrTableCell13).TextAlignment = (TextAlignment)1;
			xrTableCell13.Weight = 0.46696517557598394;
			((XRControl)xrTableCell14).Borders = (BorderSide)9;
			((XRControl)xrTableCell14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrTableCell14).Dpi = 254f;
			((XRControl)xrTableCell14).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell14).Name = "xrTableCell14";
			((XRControl)xrTableCell14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell14).StylePriority.UseBorders = false;
			((XRControl)xrTableCell14).StylePriority.UseFont = false;
			((XRControl)xrTableCell14).StylePriority.UsePadding = false;
			((XRControl)xrTableCell14).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell14).TextAlignment = (TextAlignment)1;
			xrTableCell14.Weight = 2.978010449048186;
			((XRControl)xrLabel57).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel57).Borders = (BorderSide)9;
			((XRControl)xrLabel57).Dpi = 254f;
			((XRControl)xrLabel57).Font = new Font("Arial", 8f);
			((XRControl)xrLabel57).LocationFloat = new PointFloat(230.2259f, 394.8052f);
			((XRControl)xrLabel57).Name = "xrLabel57";
			((XRControl)xrLabel57).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel57).SizeF = new SizeF(155.7708f, 39.89917f);
			((XRControl)xrLabel57).StylePriority.UseBackColor = false;
			((XRControl)xrLabel57).StylePriority.UseBorders = false;
			((XRControl)xrLabel57).StylePriority.UseFont = false;
			((XRControl)xrLabel57).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel57).Text = "Pendente";
			((XRControl)xrLabel57).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Arial", 8f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(385.9966f, 394.8054f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(156.1426f, 39.89905f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)8;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Arial", 8f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(3.307292f, 394.8051f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(70.77597f, 39.89914f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)8;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Arial", 8f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(3.307292f, 344.3226f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(191.9787f, 50.48254f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)10;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(3.30729f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(702.4975f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel5).Borders = (BorderSide)8;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Arial", 8f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(3.306504f, 105.7742f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(145.5206f, 47.83665f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(148.8271f, 105.7742f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(556.9781f, 47.8366f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel23).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel23).Borders = (BorderSide)9;
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Arial", 8f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(195.286f, 344.3226f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(510.5188f, 50.48251f);
			((XRControl)xrLabel23).StylePriority.UseBackColor = false;
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).Text = "Produto";
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Arial", 8f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(74.08325f, 394.8052f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(156.1426f, 39.89905f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Qtd";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)9;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Arial", 8f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(542.1392f, 394.8054f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(163.6656f, 39.89905f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvida";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XtraReportBase)detailEntregas).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail2,
				(Band)DetailReport2,
				(Band)GroupHeader2
			});
			((XtraReportBase)detailEntregas).DataMember = "RegistrosEntrega";
			((XtraReportBase)detailEntregas).DataSource = bindingSource1;
			((XRControl)detailEntregas).Dpi = 254f;
			detailEntregas.Level = 1;
			((XRControl)detailEntregas).Name = "detailEntregas";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel55,
				(XRControl)xrLabel48,
				(XRControl)xrLabel35,
				(XRControl)xrLabel36,
				(XRControl)xrLabel37,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel29,
				(XRControl)xrLabel30
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).Font = new Font("Segoe UI", 9f);
			((XRControl)Detail2).HeightF = 201.9299f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)Detail2).StylePriority.UseFont = false;
			((XRControl)xrLabel55).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel55).Borders = (BorderSide)8;
			((XRControl)xrLabel55).Dpi = 254f;
			((XRControl)xrLabel55).Font = new Font("Arial", 8f);
			((XRControl)xrLabel55).LocationFloat = new PointFloat(3.307291f, 151.4475f);
			((XRControl)xrLabel55).Name = "xrLabel55";
			((XRControl)xrLabel55).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel55).SizeF = new SizeF(110.5084f, 50.48245f);
			((XRControl)xrLabel55).StylePriority.UseBackColor = false;
			((XRControl)xrLabel55).StylePriority.UseBorders = false;
			((XRControl)xrLabel55).StylePriority.UseFont = false;
			((XRControl)xrLabel55).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel55).Text = "UN";
			((XRControl)xrLabel55).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel48).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel48).Borders = (BorderSide)8;
			((XRControl)xrLabel48).Dpi = 254f;
			((XRControl)xrLabel48).Font = new Font("Arial", 8f);
			((XRControl)xrLabel48).LocationFloat = new PointFloat(3.307291f, 100.965f);
			((XRControl)xrLabel48).Name = "xrLabel48";
			((XRControl)xrLabel48).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel48).SizeF = new SizeF(195.1738f, 50.48244f);
			((XRControl)xrLabel48).StylePriority.UseBackColor = false;
			((XRControl)xrLabel48).StylePriority.UseBorders = false;
			((XRControl)xrLabel48).StylePriority.UseFont = false;
			((XRControl)xrLabel48).Text = "Código";
			((XRControl)xrLabel35).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel35).Borders = (BorderSide)9;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Arial", 8f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(198.4811f, 100.965f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(507.324f, 50.48247f);
			((XRControl)xrLabel35).StylePriority.UseBackColor = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).Text = "Produto";
			((XRControl)xrLabel36).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel36).Borders = (BorderSide)9;
			((XRControl)xrLabel36).Dpi = 254f;
			((XRControl)xrLabel36).Font = new Font("Arial", 8f);
			((XRControl)xrLabel36).LocationFloat = new PointFloat(113.8157f, 151.4475f);
			((XRControl)xrLabel36).Name = "xrLabel36";
			((XRControl)xrLabel36).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel36).SizeF = new SizeF(295.6486f, 50.48245f);
			((XRControl)xrLabel36).StylePriority.UseBackColor = false;
			((XRControl)xrLabel36).StylePriority.UseBorders = false;
			((XRControl)xrLabel36).StylePriority.UseFont = false;
			((XRControl)xrLabel36).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel36).Text = "Entregue";
			((XRControl)xrLabel36).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel37).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel37).Borders = (BorderSide)9;
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Arial", 8f);
			((XRControl)xrLabel37).LocationFloat = new PointFloat(409.4643f, 151.4474f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(296.3409f, 50.48244f);
			((XRControl)xrLabel37).StylePriority.UseBackColor = false;
			((XRControl)xrLabel37).StylePriority.UseBorders = false;
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "Devolvido";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel37).BeforePrint += xrLabel37_BeforePrint;
			((XRControl)xrLabel7).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel7).Borders = (BorderSide)10;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 8f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(3.306504f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(124.2768f, 50.48251f);
			((XRControl)xrLabel7).StylePriority.UseBackColor = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).Text = "Entrega:";
			((XRControl)xrLabel8).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel8).Borders = (BorderSide)10;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Data", "{0:dd/MM/yy HH:mm}")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Arial", 8f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(127.5833f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(578.2217f, 50.48251f);
			((XRControl)xrLabel8).StylePriority.UseBackColor = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel29).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel29).Borders = (BorderSide)8;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Arial", 8f);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(3.307292f, 50.48251f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(210.7212f, 50.48249f);
			((XRControl)xrLabel29).StylePriority.UseBackColor = false;
			((XRControl)xrLabel29).StylePriority.UseBorders = false;
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).Text = "Quem recebeu:";
			((XRControl)xrLabel30).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel30).Borders = (BorderSide)8;
			((XRControl)xrLabel30).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Responsavel")
			});
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(214.0285f, 50.48252f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(491.7764f, 50.48249f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XtraReportBase)DetailReport2).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail3 });
			((XtraReportBase)DetailReport2).DataMember = "RegistrosEntrega.Itens";
			((XtraReportBase)DetailReport2).DataSource = bindingSource1;
			((XRControl)DetailReport2).Dpi = 254f;
			DetailReport2.Level = 0;
			((XRControl)DetailReport2).Name = "DetailReport2";
			((XRControl)Detail3).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel61,
				(XRControl)xrLabel56,
				(XRControl)xrLabel47,
				(XRControl)xrLabel33,
				(XRControl)xrLabel32,
				(XRControl)xrLabel31
			});
			((XRControl)Detail3).Dpi = 254f;
			((XRControl)Detail3).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail3).HeightF = 100.9651f;
			((XRControl)Detail3).Name = "Detail3";
			((XRControl)Detail3).StylePriority.UseFont = false;
			((XRControl)xrLabel61).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel61).Borders = (BorderSide)8;
			((XRControl)xrLabel61).Dpi = 254f;
			((XRControl)xrLabel61).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel61).LocationFloat = new PointFloat(4.786015f, 95.67342f);
			((XRControl)xrLabel61).Name = "xrLabel61";
			((XRControl)xrLabel61).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel61).SizeF = new SizeF(697.8163f, 5.291664f);
			((XRControl)xrLabel61).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel61).StylePriority.UseBorders = false;
			((XRControl)xrLabel61).StylePriority.UseFont = false;
			((XRControl)xrLabel61).StylePriority.UsePadding = false;
			((XRControl)xrLabel61).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel61).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel56).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel56).Borders = (BorderSide)0;
			((XRControl)xrLabel56).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel56).Dpi = 254f;
			((XRControl)xrLabel56).Font = new Font("Arial", 8f);
			((XRControl)xrLabel56).LocationFloat = new PointFloat(3.306504f, 47.83667f);
			((XRControl)xrLabel56).Name = "xrLabel56";
			((XRControl)xrLabel56).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel56).SizeF = new SizeF(104.6907f, 47.83667f);
			((XRControl)xrLabel56).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel56).StylePriority.UseBorders = false;
			((XRControl)xrLabel56).StylePriority.UseFont = false;
			((XRControl)xrLabel56).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel56).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel47).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel47).Borders = (BorderSide)0;
			((XRControl)xrLabel47).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.CodigoInterno")
			});
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Arial", 8.25f);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(3.306501f, 0f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(191.9795f, 47.83667f);
			((XRControl)xrLabel47).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel33).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel33).Borders = (BorderSide)0;
			((XRControl)xrLabel33).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Arial", 8f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(406.2692f, 47.83671f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(296.3333f, 47.83667f);
			((XRControl)xrLabel33).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Arial", 8f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(107.9972f, 47.83671f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(298.272f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel31).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Arial", 8f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(195.286f, 0f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(507.3165f, 47.83667f);
			((XRControl)xrLabel31).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel34 });
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 57.93752f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)xrLabel34).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel34).Borders = (BorderSide)2;
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Arial", 8f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(3.30729f, 0f);
			xrLabel34.Multiline = true;
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(702.4975f, 57.9375f);
			((XRControl)xrLabel34).StylePriority.UseBackColor = false;
			((XRControl)xrLabel34).StylePriority.UseBorders = false;
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel34).Text = "Registros de entrega";
			((XRControl)xrLabel34).TextAlignment = (TextAlignment)32;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[11]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel59,
				(XRControl)xrLabel46,
				(XRControl)xrLabel45,
				(XRControl)xrLabel44,
				(XRControl)xrLabel43,
				(XRControl)xrLabel42,
				(XRControl)xrLabel41,
				(XRControl)xrLabel40,
				(XRControl)xrLabel38,
				(XRControl)xrLabel60
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).Font = new Font("Segoe UI", 9.75f);
			((XRControl)ReportFooter).HeightF = 329.9998f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)ReportFooter).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)8;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(3.805298f, 299.9998f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(702f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel59).Borders = (BorderSide)2;
			((XRControl)xrLabel59).Dpi = 254f;
			((XRControl)xrLabel59).Font = new Font("Arial", 8f);
			((XRControl)xrLabel59).LocationFloat = new PointFloat(3.307291f, 200f);
			((XRControl)xrLabel59).Name = "xrLabel59";
			((XRControl)xrLabel59).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel59).SizeF = new SizeF(702.4977f, 49.99997f);
			((XRControl)xrLabel59).StylePriority.UseBorders = false;
			((XRControl)xrLabel59).StylePriority.UseFont = false;
			((XRControl)xrLabel59).Text = "Movimentações referenciadas:";
			((XRControl)xrLabel46).Borders = (BorderSide)2;
			((XRControl)xrLabel46).Dpi = 254f;
			((XRControl)xrLabel46).Font = new Font("Arial", 8f);
			((XRControl)xrLabel46).LocationFloat = new PointFloat(3.307292f, 150f);
			((XRControl)xrLabel46).Name = "xrLabel46";
			((XRControl)xrLabel46).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel46).SizeF = new SizeF(190.5f, 49.99998f);
			((XRControl)xrLabel46).StylePriority.UseBorders = false;
			((XRControl)xrLabel46).StylePriority.UseFont = false;
			((XRControl)xrLabel46).Text = "Observação:";
			((XRControl)xrLabel45).Borders = (BorderSide)2;
			((XRControl)xrLabel45).Dpi = 254f;
			((XRControl)xrLabel45).Font = new Font("Arial", 8f);
			((XRControl)xrLabel45).LocationFloat = new PointFloat(3.307292f, 99.99998f);
			((XRControl)xrLabel45).Name = "xrLabel45";
			((XRControl)xrLabel45).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel45).SizeF = new SizeF(137.5833f, 50.00002f);
			((XRControl)xrLabel45).StylePriority.UseBorders = false;
			((XRControl)xrLabel45).StylePriority.UseFont = false;
			((XRControl)xrLabel45).Text = "Usuário:";
			((XRControl)xrLabel44).Borders = (BorderSide)2;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Arial", 8f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(3.307292f, 50f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(92.6041f, 49.99999f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).Text = "Data:";
			((XRControl)xrLabel43).Borders = (BorderSide)2;
			((XRControl)xrLabel43).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Observacao")
			});
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel43).LocationFloat = new PointFloat(193.8073f, 150f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(511.9977f, 49.99997f);
			((XRControl)xrLabel43).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel42).Borders = (BorderSide)2;
			((XRControl)xrLabel42).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.NomeUsuario")
			});
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel42).LocationFloat = new PointFloat(140.8906f, 99.99994f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(564.9144f, 50f);
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseFont = false;
			((XRControl)xrLabel41).Borders = (BorderSide)2;
			((XRControl)xrLabel41).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Data", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel41).LocationFloat = new PointFloat(95.91138f, 50.00004f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(609.8936f, 49.99998f);
			((XRControl)xrLabel41).StylePriority.UseBorders = false;
			((XRControl)xrLabel41).StylePriority.UseFont = false;
			((XRControl)xrLabel40).Borders = (BorderSide)2;
			((XRControl)xrLabel40).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Situacao.Descricao")
			});
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel40).LocationFloat = new PointFloat(148.8271f, 0f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(556.9778f, 50f);
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseFont = false;
			((XRControl)xrLabel38).Borders = (BorderSide)2;
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).Font = new Font("Arial", 8f);
			((XRControl)xrLabel38).LocationFloat = new PointFloat(3.306501f, 0f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(145.5206f, 50f);
			((XRControl)xrLabel38).StylePriority.UseBorders = false;
			((XRControl)xrLabel38).StylePriority.UseFont = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel38).Text = "Situação:";
			((XRControl)xrLabel60).Borders = (BorderSide)10;
			((XRControl)xrLabel60).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "MovimentacoesReferenciadas")
			});
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(3.307291f, 249.9999f);
			xrLabel60.Multiline = true;
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(702.4977f, 49.99995f);
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)PageHeader;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.307291f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(705.8048f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(705.8048f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)PageHeader;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(705.8048f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport,
				(Band)detailEntregas,
				(Band)ReportFooter
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { calculatedField1 });
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(45, 36, 46, 50);
			((XtraReport)this).PageHeight = 2500;
			((XtraReport)this).PageWidth = 790;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)xrTable5).EndInit();
			((ISupportInitialize)xrTable3).EndInit();
			((ISupportInitialize)xrTable2).EndInit();
			((ISupportInitialize)xrTable1).EndInit();
			((ISupportInitialize)xrTable4).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoControleEntregaPendente40Colunas : Relatorio
	{
		private readonly ControleEntregaDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private PageHeaderBand PageHeader;

		private XRLabel xrLabel1;

		private XRLabel xrLabel39;

		private CalculatedField calculatedField1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel28;

		private XRLabel xrLabel27;

		private XRLabel produto;

		private XRLabel xrLabel25;

		private XRLabel xrLabel24;

		private XRLabel lblProduto;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		private XRLabel xrLabel2;

		private GroupFooterBand GroupFooter1;

		private XRLine xrLine1;

		private XRLabel xrLabel31;

		private XRLabel xrLabel30;

		private XRLabel xrLabel29;

		private XRLabel xrLabel8;

		private XRLabel xrLabel51;

		private XRLabel xrLabel53;

		private XRLabel xrLabel49;

		private XRLabel xrLabel7;

		private XRLabel xrLabel52;

		private XRLabel xrLabel32;

		private XRLabel xrLabel33;

		private XRLabel xrLabel34;

		private XRLabel xrLabel60;

		private XRLabel xrLabel35;

		private XRLabel xrLabel37;

		private XRLabel xrLabel36;

		private XRCrossBandLine xrCrossBandLine2;

		private XRCrossBandLine xrCrossBandLine1;

		private XRTable xrTable4;

		private XRTableRow xrTableRow4;

		private XRTableCell xrTableCell13;

		private XRTableCell xrTableCell14;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell3;

		private XRTableCell xrTableCell4;

		private XRTable xrTable2;

		private XRTableRow xrTableRow2;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private XRTableCell xrTableCell7;

		private XRTableCell xrTableCell8;

		private XRTable xrTable3;

		private XRTableRow xrTableRow3;

		private XRTableCell xrTableCell9;

		private XRTableCell xrTableCell10;

		private XRTableCell xrTableCell11;

		private XRTableCell xrTableCell12;

		private XRTable xrTable5;

		private XRTableRow xrTableRow5;

		private XRTableCell xrTableCell15;

		private XRTableCell xrTableCell16;

		private XRLabel lblMensagemRodape;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoControleEntregaPendente40Colunas(ControleEntregaDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReportBase)this).DataSource = new List<ControleEntregaDto> { _controleEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Expected O, but got Unknown
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Expected O, but got Unknown
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Expected O, but got Unknown
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Expected O, but got Unknown
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Expected O, but got Unknown
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Expected O, but got Unknown
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Expected O, but got Unknown
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Expected O, but got Unknown
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Expected O, but got Unknown
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected O, but got Unknown
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Expected O, but got Unknown
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Expected O, but got Unknown
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Expected O, but got Unknown
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0284: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Expected O, but got Unknown
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Expected O, but got Unknown
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bb: Expected O, but got Unknown
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Expected O, but got Unknown
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Expected O, but got Unknown
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dc: Expected O, but got Unknown
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Expected O, but got Unknown
			//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Expected O, but got Unknown
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0423: Unknown result type (might be due to invalid IL or missing references)
			//IL_051c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0522: Expected O, but got Unknown
			//IL_0561: Unknown result type (might be due to invalid IL or missing references)
			//IL_058a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0617: Unknown result type (might be due to invalid IL or missing references)
			//IL_064c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0879: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0961: Unknown result type (might be due to invalid IL or missing references)
			//IL_0967: Expected O, but got Unknown
			//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a67: Expected O, but got Unknown
			//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b84: Expected O, but got Unknown
			//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca1: Expected O, but got Unknown
			//IL_0ce0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dbe: Expected O, but got Unknown
			//IL_0dfd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ed5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0edb: Expected O, but got Unknown
			//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff3: Expected O, but got Unknown
			//IL_1032: Unknown result type (might be due to invalid IL or missing references)
			//IL_105b: Unknown result type (might be due to invalid IL or missing references)
			//IL_11f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1304: Unknown result type (might be due to invalid IL or missing references)
			//IL_139c: Unknown result type (might be due to invalid IL or missing references)
			//IL_13a2: Expected O, but got Unknown
			//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_14a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_165c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1662: Expected O, but got Unknown
			//IL_16b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_175b: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f9: Expected O, but got Unknown
			//IL_1848: Unknown result type (might be due to invalid IL or missing references)
			//IL_18fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab9: Expected O, but got Unknown
			//IL_1b08: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c50: Expected O, but got Unknown
			//IL_1c9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d53: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e72: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f10: Expected O, but got Unknown
			//IL_1f5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2009: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a7: Expected O, but got Unknown
			//IL_20f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_21aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_22b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_234f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2355: Expected O, but got Unknown
			//IL_23a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2469: Unknown result type (might be due to invalid IL or missing references)
			//IL_2492: Unknown result type (might be due to invalid IL or missing references)
			//IL_2550: Unknown result type (might be due to invalid IL or missing references)
			//IL_2579: Unknown result type (might be due to invalid IL or missing references)
			//IL_2654: Unknown result type (might be due to invalid IL or missing references)
			//IL_267d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2758: Unknown result type (might be due to invalid IL or missing references)
			//IL_2781: Unknown result type (might be due to invalid IL or missing references)
			//IL_285c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2885: Unknown result type (might be due to invalid IL or missing references)
			//IL_2960: Unknown result type (might be due to invalid IL or missing references)
			//IL_2989: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a21: Expected O, but got Unknown
			//IL_2a61: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b17: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b40: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bed: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c22: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cfe: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d27: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ec0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ef5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f82: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f88: Expected O, but got Unknown
			//IL_2fc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ffd: Unknown result type (might be due to invalid IL or missing references)
			//IL_307d: Unknown result type (might be due to invalid IL or missing references)
			//IL_30a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3115: Unknown result type (might be due to invalid IL or missing references)
			//IL_311b: Expected O, but got Unknown
			//IL_315b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3190: Unknown result type (might be due to invalid IL or missing references)
			//IL_3210: Unknown result type (might be due to invalid IL or missing references)
			//IL_3239: Unknown result type (might be due to invalid IL or missing references)
			//IL_32aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_3324: Unknown result type (might be due to invalid IL or missing references)
			//IL_334d: Unknown result type (might be due to invalid IL or missing references)
			//IL_33fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_3423: Unknown result type (might be due to invalid IL or missing references)
			//IL_34c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_34e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_357d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3597: Unknown result type (might be due to invalid IL or missing references)
			//IL_35d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_361d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3637: Unknown result type (might be due to invalid IL or missing references)
			//IL_3672: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			PageHeader = new PageHeaderBand();
			xrLabel1 = new XRLabel();
			xrLabel39 = new XRLabel();
			calculatedField1 = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel35 = new XRLabel();
			produto = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel28 = new XRLabel();
			xrLabel8 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrTable5 = new XRTable();
			xrTableRow5 = new XRTableRow();
			xrTableCell15 = new XRTableCell();
			xrTableCell16 = new XRTableCell();
			xrTable3 = new XRTable();
			xrTableRow3 = new XRTableRow();
			xrTableCell9 = new XRTableCell();
			xrTableCell10 = new XRTableCell();
			xrTableCell11 = new XRTableCell();
			xrTableCell12 = new XRTableCell();
			xrTable2 = new XRTable();
			xrTableRow2 = new XRTableRow();
			xrTableCell5 = new XRTableCell();
			xrTableCell6 = new XRTableCell();
			xrTableCell7 = new XRTableCell();
			xrTableCell8 = new XRTableCell();
			xrTable1 = new XRTable();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			xrTableCell3 = new XRTableCell();
			xrTableCell4 = new XRTableCell();
			xrTable4 = new XRTable();
			xrTableRow4 = new XRTableRow();
			xrTableCell13 = new XRTableCell();
			xrTableCell14 = new XRTableCell();
			xrLabel49 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel24 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel33 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel36 = new XRLabel();
			xrLabel60 = new XRLabel();
			xrLabel34 = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel31 = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel29 = new XRLabel();
			xrCrossBandLine2 = new XRCrossBandLine();
			xrCrossBandLine1 = new XRCrossBandLine();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)xrTable5).BeginInit();
			((ISupportInitialize)xrTable3).BeginInit();
			((ISupportInitialize)xrTable2).BeginInit();
			((ISupportInitialize)xrTable1).BeginInit();
			((ISupportInitialize)xrTable4).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 50f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 52f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(ControleEntregaDto);
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel39
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 121.4375f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)PageHeader).StylePriority.UseTextAlignment = false;
			((XRControl)PageHeader).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel1).Borders = (BorderSide)5;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "calculatedField1")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 8f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 70.95506f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(709.0001f, 50.48243f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel39).Borders = (BorderSide)7;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(0f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(709f, 70.95506f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			calculatedField1.DisplayName = "numeroDocumento";
			calculatedField1.Expression = "Concat('Nº do documento: ', [Numero])";
			calculatedField1.Name = "calculatedField1";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel35,
				(XRControl)produto,
				(XRControl)xrLabel27,
				(XRControl)xrLabel32,
				(XRControl)xrLabel7,
				(XRControl)xrLabel52,
				(XRControl)xrLabel28,
				(XRControl)xrLabel8
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail1).HeightF = 100.6733f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3.000004f, 95.67333f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(702.9999f, 5f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Arial", 8f);
			((XRControl)produto).LocationFloat = new PointFloat(130.2426f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(575.7574f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Arial", 8f);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(399.5975f, 47.83674f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(156.1426f, 47.83664f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Arial", 8f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(243.4548f, 47.83674f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(156.1427f, 47.83664f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:#.##}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 8f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(87.31221f, 47.83674f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(156.1426f, 47.83664f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Arial", 8f);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(3.000004f, 47.83667f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(84.31219f, 47.83666f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel28).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Arial", 8f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(555.7402f, 47.83672f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(150.2598f, 47.83664f);
			((XRControl)xrLabel28).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Arial", 8f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(3.000004f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(127.2426f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[15]
			{
				(XRControl)xrTable5,
				(XRControl)xrTable3,
				(XRControl)xrTable2,
				(XRControl)xrTable1,
				(XRControl)xrTable4,
				(XRControl)xrLabel49,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel25,
				(XRControl)xrLabel24,
				(XRControl)lblProduto,
				(XRControl)xrLabel6,
				(XRControl)xrLabel5,
				(XRControl)xrLabel2,
				(XRControl)xrLabel33
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 445.0759f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrTable5).Borders = (BorderSide)0;
			((XRControl)xrTable5).Dpi = 254f;
			((XRControl)xrTable5).Font = new Font("Arial", 8f);
			((XRControl)xrTable5).LocationFloat = new PointFloat(0f, 201.2359f);
			((XRControl)xrTable5).Name = "xrTable5";
			xrTable5.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow5 });
			((XRControl)xrTable5).SizeF = new SizeF(709.0002f, 47.62498f);
			((XRControl)xrTable5).StylePriority.UseBorders = false;
			((XRControl)xrTable5).StylePriority.UseFont = false;
			xrTableRow5.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell15, xrTableCell16 });
			((XRControl)xrTableRow5).Dpi = 254f;
			((XRControl)xrTableRow5).Name = "xrTableRow5";
			xrTableRow5.Weight = 1.0;
			((XRControl)xrTableCell15).Borders = (BorderSide)9;
			((XRControl)xrTableCell15).Dpi = 254f;
			((XRControl)xrTableCell15).Name = "xrTableCell15";
			((XRControl)xrTableCell15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell15).StylePriority.UseBorders = false;
			((XRControl)xrTableCell15).StylePriority.UsePadding = false;
			((XRControl)xrTableCell15).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell15).Text = "Complemento:";
			((XRControl)xrTableCell15).TextAlignment = (TextAlignment)1;
			xrTableCell15.Weight = 1.1069702913451696;
			((XRControl)xrTableCell16).Borders = (BorderSide)13;
			((XRControl)xrTableCell16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrTableCell16).Dpi = 254f;
			((XRControl)xrTableCell16).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell16).Name = "xrTableCell16";
			((XRControl)xrTableCell16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell16).StylePriority.UseBorders = false;
			((XRControl)xrTableCell16).StylePriority.UseFont = false;
			((XRControl)xrTableCell16).StylePriority.UsePadding = false;
			((XRControl)xrTableCell16).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell16).TextAlignment = (TextAlignment)1;
			xrTableCell16.Weight = 2.5189924749666037;
			((XRControl)xrTable3).Borders = (BorderSide)0;
			((XRControl)xrTable3).Dpi = 254f;
			((XRControl)xrTable3).Font = new Font("Arial", 8f);
			((XRControl)xrTable3).LocationFloat = new PointFloat(0f, 296.4859f);
			((XRControl)xrTable3).Name = "xrTable3";
			xrTable3.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow3 });
			((XRControl)xrTable3).SizeF = new SizeF(709.0001f, 47.62498f);
			((XRControl)xrTable3).StylePriority.UseBorders = false;
			((XRControl)xrTable3).StylePriority.UseFont = false;
			xrTableRow3.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell9, xrTableCell10, xrTableCell11, xrTableCell12 });
			((XRControl)xrTableRow3).Dpi = 254f;
			((XRControl)xrTableRow3).Name = "xrTableRow3";
			xrTableRow3.Weight = 1.0;
			((XRControl)xrTableCell9).Borders = (BorderSide)9;
			((XRControl)xrTableCell9).Dpi = 254f;
			((XRControl)xrTableCell9).Name = "xrTableCell9";
			((XRControl)xrTableCell9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell9).StylePriority.UseBorders = false;
			((XRControl)xrTableCell9).StylePriority.UsePadding = false;
			((XRControl)xrTableCell9).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell9).Text = "Município:";
			((XRControl)xrTableCell9).TextAlignment = (TextAlignment)1;
			xrTableCell9.Weight = 0.6895838425088469;
			((XRControl)xrTableCell10).Borders = (BorderSide)13;
			((XRControl)xrTableCell10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrTableCell10).Dpi = 254f;
			((XRControl)xrTableCell10).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell10).Name = "xrTableCell10";
			((XRControl)xrTableCell10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell10).StylePriority.UseBorders = false;
			((XRControl)xrTableCell10).StylePriority.UseFont = false;
			((XRControl)xrTableCell10).StylePriority.UsePadding = false;
			((XRControl)xrTableCell10).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell10).TextAlignment = (TextAlignment)1;
			xrTableCell10.Weight = 2.109457429847338;
			((XRControl)xrTableCell11).Borders = (BorderSide)12;
			((XRControl)xrTableCell11).Dpi = 254f;
			((XRControl)xrTableCell11).Name = "xrTableCell11";
			((XRControl)xrTableCell11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell11).StylePriority.UseBorders = false;
			((XRControl)xrTableCell11).StylePriority.UsePadding = false;
			((XRControl)xrTableCell11).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell11).Text = "UF:";
			((XRControl)xrTableCell11).TextAlignment = (TextAlignment)1;
			xrTableCell11.Weight = 0.2871187648427955;
			((XRControl)xrTableCell12).Borders = (BorderSide)12;
			((XRControl)xrTableCell12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrTableCell12).Dpi = 254f;
			((XRControl)xrTableCell12).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell12).Name = "xrTableCell12";
			((XRControl)xrTableCell12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell12).StylePriority.UseBorders = false;
			((XRControl)xrTableCell12).StylePriority.UseFont = false;
			((XRControl)xrTableCell12).StylePriority.UsePadding = false;
			((XRControl)xrTableCell12).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell12).TextAlignment = (TextAlignment)1;
			xrTableCell12.Weight = 0.3588119170919998;
			((XRControl)xrTable2).Borders = (BorderSide)0;
			((XRControl)xrTable2).Dpi = 254f;
			((XRControl)xrTable2).Font = new Font("Arial", 8f);
			((XRControl)xrTable2).LocationFloat = new PointFloat(0f, 248.8609f);
			((XRControl)xrTable2).Name = "xrTable2";
			xrTable2.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow2 });
			((XRControl)xrTable2).SizeF = new SizeF(709.0001f, 47.62502f);
			((XRControl)xrTable2).StylePriority.UseBorders = false;
			((XRControl)xrTable2).StylePriority.UseFont = false;
			xrTableRow2.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell5, xrTableCell6, xrTableCell7, xrTableCell8 });
			((XRControl)xrTableRow2).Dpi = 254f;
			((XRControl)xrTableRow2).Name = "xrTableRow2";
			xrTableRow2.Weight = 1.0;
			((XRControl)xrTableCell5).Borders = (BorderSide)9;
			((XRControl)xrTableCell5).Dpi = 254f;
			((XRControl)xrTableCell5).Name = "xrTableCell5";
			((XRControl)xrTableCell5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell5).StylePriority.UseBorders = false;
			((XRControl)xrTableCell5).StylePriority.UsePadding = false;
			((XRControl)xrTableCell5).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell5).Text = "Bairro:";
			((XRControl)xrTableCell5).TextAlignment = (TextAlignment)1;
			xrTableCell5.Weight = 0.4669651384004984;
			((XRControl)xrTableCell6).Borders = (BorderSide)13;
			((XRControl)xrTableCell6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrTableCell6).Dpi = 254f;
			((XRControl)xrTableCell6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell6).Name = "xrTableCell6";
			((XRControl)xrTableCell6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell6).StylePriority.UseBorders = false;
			((XRControl)xrTableCell6).StylePriority.UseFont = false;
			((XRControl)xrTableCell6).StylePriority.UsePadding = false;
			((XRControl)xrTableCell6).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell6).TextAlignment = (TextAlignment)1;
			xrTableCell6.Weight = 1.693365602364628;
			((XRControl)xrTableCell7).Borders = (BorderSide)12;
			((XRControl)xrTableCell7).Dpi = 254f;
			((XRControl)xrTableCell7).Name = "xrTableCell7";
			((XRControl)xrTableCell7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell7).StylePriority.UseBorders = false;
			((XRControl)xrTableCell7).StylePriority.UsePadding = false;
			((XRControl)xrTableCell7).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell7).Text = "CEP:";
			((XRControl)xrTableCell7).TextAlignment = (TextAlignment)1;
			xrTableCell7.Weight = 0.385346063503743;
			((XRControl)xrTableCell8).Borders = (BorderSide)12;
			((XRControl)xrTableCell8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep")
			});
			((XRControl)xrTableCell8).Dpi = 254f;
			((XRControl)xrTableCell8).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell8).Name = "xrTableCell8";
			((XRControl)xrTableCell8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell8).StylePriority.UseBorders = false;
			((XRControl)xrTableCell8).StylePriority.UseFont = false;
			((XRControl)xrTableCell8).StylePriority.UsePadding = false;
			((XRControl)xrTableCell8).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell8).TextAlignment = (TextAlignment)1;
			xrTableCell8.Weight = 0.8992951500221109;
			((XRControl)xrTable1).Borders = (BorderSide)0;
			((XRControl)xrTable1).Dpi = 254f;
			((XRControl)xrTable1).Font = new Font("Arial", 8f);
			((XRControl)xrTable1).LocationFloat = new PointFloat(0f, 153.6109f);
			((XRControl)xrTable1).Name = "xrTable1";
			xrTable1.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow1 });
			((XRControl)xrTable1).SizeF = new SizeF(709.0001f, 47.62498f);
			((XRControl)xrTable1).StylePriority.UseBorders = false;
			((XRControl)xrTable1).StylePriority.UseFont = false;
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[4] { xrTableCell1, xrTableCell2, xrTableCell3, xrTableCell4 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 1.0;
			((XRControl)xrTableCell1).Borders = (BorderSide)9;
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell1).StylePriority.UseBorders = false;
			((XRControl)xrTableCell1).StylePriority.UsePadding = false;
			((XRControl)xrTableCell1).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell1).Text = "Endereço:";
			((XRControl)xrTableCell1).TextAlignment = (TextAlignment)1;
			xrTableCell1.Weight = 0.709074540694358;
			((XRControl)xrTableCell2).Borders = (BorderSide)13;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell2).StylePriority.UseBorders = false;
			((XRControl)xrTableCell2).StylePriority.UseFont = false;
			((XRControl)xrTableCell2).StylePriority.UsePadding = false;
			((XRControl)xrTableCell2).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell2).TextAlignment = (TextAlignment)1;
			xrTableCell2.Weight = 1.7511108418261232;
			((XRControl)xrTableCell3).Borders = (BorderSide)12;
			((XRControl)xrTableCell3).Dpi = 254f;
			((XRControl)xrTableCell3).Name = "xrTableCell3";
			((XRControl)xrTableCell3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell3).StylePriority.UseBorders = false;
			((XRControl)xrTableCell3).StylePriority.UsePadding = false;
			((XRControl)xrTableCell3).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell3).Text = "Nº:";
			((XRControl)xrTableCell3).TextAlignment = (TextAlignment)1;
			xrTableCell3.Weight = 0.25402038907273355;
			((XRControl)xrTableCell4).Borders = (BorderSide)12;
			((XRControl)xrTableCell4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrTableCell4).Dpi = 254f;
			((XRControl)xrTableCell4).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell4).Name = "xrTableCell4";
			((XRControl)xrTableCell4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell4).StylePriority.UseBorders = false;
			((XRControl)xrTableCell4).StylePriority.UseFont = false;
			((XRControl)xrTableCell4).StylePriority.UsePadding = false;
			((XRControl)xrTableCell4).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell4).TextAlignment = (TextAlignment)1;
			xrTableCell4.Weight = 0.7307661826977656;
			((XRControl)xrTable4).Borders = (BorderSide)0;
			((XRControl)xrTable4).Dpi = 254f;
			((XRControl)xrTable4).Font = new Font("Arial", 8f);
			((XRControl)xrTable4).LocationFloat = new PointFloat(0f, 57.93765f);
			((XRControl)xrTable4).Name = "xrTable4";
			xrTable4.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow4 });
			((XRControl)xrTable4).SizeF = new SizeF(709.0002f, 47.83665f);
			((XRControl)xrTable4).StylePriority.UseBorders = false;
			((XRControl)xrTable4).StylePriority.UseFont = false;
			xrTableRow4.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell13, xrTableCell14 });
			((XRControl)xrTableRow4).Dpi = 254f;
			((XRControl)xrTableRow4).Name = "xrTableRow4";
			xrTableRow4.Weight = 1.0;
			((XRControl)xrTableCell13).Borders = (BorderSide)9;
			((XRControl)xrTableCell13).Dpi = 254f;
			((XRControl)xrTableCell13).Name = "xrTableCell13";
			((XRControl)xrTableCell13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell13).StylePriority.UseBorders = false;
			((XRControl)xrTableCell13).StylePriority.UsePadding = false;
			((XRControl)xrTableCell13).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell13).Text = "Nome:";
			((XRControl)xrTableCell13).TextAlignment = (TextAlignment)1;
			xrTableCell13.Weight = 0.46696517557598394;
			((XRControl)xrTableCell14).Borders = (BorderSide)13;
			((XRControl)xrTableCell14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrTableCell14).Dpi = 254f;
			((XRControl)xrTableCell14).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrTableCell14).Name = "xrTableCell14";
			((XRControl)xrTableCell14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell14).StylePriority.UseBorders = false;
			((XRControl)xrTableCell14).StylePriority.UseFont = false;
			((XRControl)xrTableCell14).StylePriority.UsePadding = false;
			((XRControl)xrTableCell14).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell14).TextAlignment = (TextAlignment)1;
			xrTableCell14.Weight = 2.9877554824363957;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Arial", 8f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(0f, 344.1109f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(130.2426f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Arial", 8f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(399.5976f, 394.5934f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(156.1426f, 50.48245f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Arial", 8f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(0f, 394.5934f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(87.31232f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)13;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Arial", 8f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(555.7402f, 394.5935f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(153.2599f, 50.48239f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvida";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Arial", 8f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(87.31232f, 394.5933f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(156.1426f, 50.48248f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Qtd";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)13;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Arial", 8f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(130.2426f, 344.1109f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(578.7574f, 50.48254f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel6).Borders = (BorderSide)12;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(142.8744f, 105.7743f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(566.1256f, 47.83657f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Borders = (BorderSide)9;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Arial", 8f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 105.7742f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(142.8743f, 47.83665f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)15;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(709f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Arial", 8f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(243.4549f, 394.5934f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(156.1426f, 50.48245f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)lblMensagemRodape,
				(XRControl)xrLabel37,
				(XRControl)xrLabel36,
				(XRControl)xrLabel60,
				(XRControl)xrLabel34,
				(XRControl)xrLine1,
				(XRControl)xrLabel31,
				(XRControl)xrLabel30,
				(XRControl)xrLabel29
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).Font = new Font("Segoe UI", 11f);
			((XRControl)GroupFooter1).HeightF = 430.7103f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)GroupFooter1).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)8;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(3.000035f, 400.7103f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(702.9999f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel37).Borders = (BorderSide)0;
			((XRControl)xrLabel37).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResumoFinanceiroMovimentacoesReferenciadas")
			});
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel37).LocationFloat = new PointFloat(3.000008f, 355.1176f);
			xrLabel37.Multiline = true;
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(702.9999f, 45.59271f);
			((XRControl)xrLabel37).StylePriority.UseBorders = false;
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel36).Dpi = 254f;
			((XRControl)xrLabel36).Font = new Font("Arial", 8f);
			((XRControl)xrLabel36).LocationFloat = new PointFloat(3.000032f, 296.6975f);
			((XRControl)xrLabel36).Name = "xrLabel36";
			((XRControl)xrLabel36).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel36).SizeF = new SizeF(702.9999f, 58.42004f);
			((XRControl)xrLabel36).StylePriority.UseFont = false;
			((XRControl)xrLabel36).Text = "Resumo financeiro das movimentações referenciadas:";
			((XRControl)xrLabel60).Borders = (BorderSide)0;
			((XRControl)xrLabel60).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "MovimentacoesReferenciadas")
			});
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(3.000005f, 246.6976f);
			xrLabel60.Multiline = true;
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(702.9999f, 49.99991f);
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Arial", 8f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(2.999985f, 205.9165f);
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(703f, 40.78108f);
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).Text = "Movimentações referenciadas:";
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(99.8333f, 142.4965f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(499.2046f, 5f);
			((XRControl)xrLabel31).Borders = (BorderSide)8;
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Arial", 8f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(3.000031f, 147.4966f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(702.9999f, 58.41998f);
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).Text = "Assinatura recebedor";
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(358.5945f, 24.99996f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(270.864f, 58.41999f);
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).Text = "____/____/________";
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Arial", 8f);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(94.83328f, 24.99992f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(263.7611f, 58.41998f);
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel29).Text = "Data da entrega:";
			((XRControl)xrLabel29).TextAlignment = (TextAlignment)16;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(706f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(706f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(706f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3.000061f;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.000006f;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { calculatedField1 });
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(39, 46, 50, 52);
			((XtraReport)this).PageHeight = 2500;
			((XtraReport)this).PageWidth = 794;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)xrTable5).EndInit();
			((ISupportInitialize)xrTable3).EndInit();
			((ISupportInitialize)xrTable2).EndInit();
			((ISupportInitialize)xrTable1).EndInit();
			((ISupportInitialize)xrTable4).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoControleEntregaPendente : Relatorio
	{
		private readonly ControleEntregaDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private PageHeaderBand PageHeader;

		private XRLabel xrLabel1;

		private XRLabel xrLabel39;

		private CalculatedField calculatedField1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel28;

		private XRLabel xrLabel27;

		private XRLabel produto;

		private XRLabel xrLabel25;

		private XRLabel xrLabel24;

		private XRLabel lblProduto;

		private XRLabel xrLabel22;

		private XRLabel xrLabel21;

		private XRLabel xrLabel20;

		private XRLabel xrLabel19;

		private XRLabel xrLabel18;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel9;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private GroupFooterBand GroupFooter1;

		private XRLine xrLine1;

		private XRLabel xrLabel31;

		private XRLabel xrLabel30;

		private XRLabel xrLabel29;

		private XRLabel xrLabel8;

		private XRLabel xrLabel51;

		private XRLabel xrLabel53;

		private XRLabel xrLabel49;

		private XRLabel xrLabel7;

		private XRLabel xrLabel52;

		private XRLabel xrLabel32;

		private XRLabel xrLabel33;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel localizacao;

		private XRLabel lblLocalizacao;

		private XRLabel xrLabel26;

		private XRLabel xrLabel23;

		private XRLabel xrLabel35;

		private XRLabel marca;

		private XRLabel lblMarca;

		private XRLabel lblMensagemRodape;

		private XRLabel xrLabel5;

		private XRLabel xrLabel6;

		private PageFooterBand PageFooter;

		private XRTable xrTable3;

		private XRTableRow xrTableRow3;

		private XRTableCell xrTableCell12;

		private XRTableCell xrTableCell14;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoControleEntregaPendente(ControleEntregaDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			ProcessarMarca();
			ProcessarLocalizacao();
			((XtraReportBase)this).DataSource = new List<ControleEntregaDto> { _controleEntrega };
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ProcessarMarca()
		{
			if (_controleEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Marca)))
			{
				((XRControl)lblMarca).Visible = false;
				((XRControl)marca).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
		}

		private void ProcessarLocalizacao()
		{
			if (_controleEntrega.Itens.All((ControleEntregaItemDto x) => string.IsNullOrEmpty(x.Localizacao)))
			{
				((XRControl)lblMarca).Location = new System.Drawing.Point(((XRControl)lblLocalizacao).Location.X, ((XRControl)lblLocalizacao).Location.Y);
				((XRControl)marca).Location = new System.Drawing.Point(((XRControl)localizacao).Location.X, ((XRControl)localizacao).Location.Y);
				((XRControl)lblLocalizacao).Visible = false;
				((XRControl)localizacao).Visible = false;
				XRLabel obj = lblProduto;
				((XRControl)obj).Width = ((XRControl)obj).Width + 192;
				XRLabel obj2 = produto;
				((XRControl)obj2).Width = ((XRControl)obj2).Width + 192;
			}
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Expected O, but got Unknown
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Expected O, but got Unknown
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Expected O, but got Unknown
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Expected O, but got Unknown
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Expected O, but got Unknown
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Expected O, but got Unknown
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Expected O, but got Unknown
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Expected O, but got Unknown
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Expected O, but got Unknown
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected O, but got Unknown
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Expected O, but got Unknown
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Expected O, but got Unknown
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Expected O, but got Unknown
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0284: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Expected O, but got Unknown
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Expected O, but got Unknown
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bb: Expected O, but got Unknown
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Expected O, but got Unknown
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Expected O, but got Unknown
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dc: Expected O, but got Unknown
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Expected O, but got Unknown
			//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Expected O, but got Unknown
			//IL_034d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d6: Expected O, but got Unknown
			//IL_0515: Unknown result type (might be due to invalid IL or missing references)
			//IL_053e: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0600: Unknown result type (might be due to invalid IL or missing references)
			//IL_082f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0835: Expected O, but got Unknown
			//IL_0875: Unknown result type (might be due to invalid IL or missing references)
			//IL_089e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0947: Unknown result type (might be due to invalid IL or missing references)
			//IL_0974: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b25: Expected O, but got Unknown
			//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c26: Expected O, but got Unknown
			//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d44: Expected O, but got Unknown
			//IL_0d84: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e62: Expected O, but got Unknown
			//IL_0ea2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f7b: Expected O, but got Unknown
			//IL_0fbb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1076: Unknown result type (might be due to invalid IL or missing references)
			//IL_107c: Expected O, but got Unknown
			//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_10e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1194: Unknown result type (might be due to invalid IL or missing references)
			//IL_119a: Expected O, but got Unknown
			//IL_11da: Unknown result type (might be due to invalid IL or missing references)
			//IL_1203: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_12b3: Expected O, but got Unknown
			//IL_12f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_131c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1545: Unknown result type (might be due to invalid IL or missing references)
			//IL_156e: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_15f4: Expected O, but got Unknown
			//IL_1634: Unknown result type (might be due to invalid IL or missing references)
			//IL_165d: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1723: Unknown result type (might be due to invalid IL or missing references)
			//IL_17fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1827: Unknown result type (might be due to invalid IL or missing references)
			//IL_1902: Unknown result type (might be due to invalid IL or missing references)
			//IL_192b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a06: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aed: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b16: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cf5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1df9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e22: Unknown result type (might be due to invalid IL or missing references)
			//IL_1efd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fbe: Expected O, but got Unknown
			//IL_1ffe: Unknown result type (might be due to invalid IL or missing references)
			//IL_2027: Unknown result type (might be due to invalid IL or missing references)
			//IL_20b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_20dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_215d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2163: Expected O, but got Unknown
			//IL_21a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_2258: Unknown result type (might be due to invalid IL or missing references)
			//IL_2281: Unknown result type (might be due to invalid IL or missing references)
			//IL_2307: Unknown result type (might be due to invalid IL or missing references)
			//IL_230d: Expected O, but got Unknown
			//IL_234d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2376: Unknown result type (might be due to invalid IL or missing references)
			//IL_2403: Unknown result type (might be due to invalid IL or missing references)
			//IL_242c: Unknown result type (might be due to invalid IL or missing references)
			//IL_24ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_24b3: Expected O, but got Unknown
			//IL_24f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_251c: Unknown result type (might be due to invalid IL or missing references)
			//IL_25a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_25d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2652: Unknown result type (might be due to invalid IL or missing references)
			//IL_2658: Expected O, but got Unknown
			//IL_2698: Unknown result type (might be due to invalid IL or missing references)
			//IL_26c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_274e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2777: Unknown result type (might be due to invalid IL or missing references)
			//IL_27f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_27fe: Expected O, but got Unknown
			//IL_283e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2867: Unknown result type (might be due to invalid IL or missing references)
			//IL_28f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_291c: Unknown result type (might be due to invalid IL or missing references)
			//IL_29ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_29c0: Expected O, but got Unknown
			//IL_2a00: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a29: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ab6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2adf: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b60: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b66: Expected O, but got Unknown
			//IL_2ba6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c85: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d32: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d67: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e43: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fe1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3016: Unknown result type (might be due to invalid IL or missing references)
			//IL_30a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_311f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3148: Unknown result type (might be due to invalid IL or missing references)
			//IL_31db: Unknown result type (might be due to invalid IL or missing references)
			//IL_3204: Unknown result type (might be due to invalid IL or missing references)
			//IL_3272: Unknown result type (might be due to invalid IL or missing references)
			//IL_329b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3311: Unknown result type (might be due to invalid IL or missing references)
			//IL_332b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3366: Unknown result type (might be due to invalid IL or missing references)
			//IL_33b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_33cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3406: Unknown result type (might be due to invalid IL or missing references)
			//IL_34a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_35c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_3671: Unknown result type (might be due to invalid IL or missing references)
			//IL_3677: Expected O, but got Unknown
			//IL_36c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_37e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_388f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3895: Expected O, but got Unknown
			//IL_38e3: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			PageHeader = new PageHeaderBand();
			xrLabel1 = new XRLabel();
			xrLabel39 = new XRLabel();
			calculatedField1 = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			marca = new XRLabel();
			xrLabel35 = new XRLabel();
			xrLabel26 = new XRLabel();
			localizacao = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel28 = new XRLabel();
			xrLabel27 = new XRLabel();
			produto = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel5 = new XRLabel();
			xrLabel6 = new XRLabel();
			lblMarca = new XRLabel();
			xrLabel23 = new XRLabel();
			lblLocalizacao = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel24 = new XRLabel();
			lblProduto = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel33 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel31 = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel29 = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			PageFooter = new PageFooterBand();
			xrTable3 = new XRTable();
			xrTableRow3 = new XRTableRow();
			xrTableCell12 = new XRTableCell();
			xrTableCell14 = new XRTableCell();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)xrTable3).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 50f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 50f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(ControleEntregaDto);
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel39
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 121.4375f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrLabel1).Borders = (BorderSide)15;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "calculatedField1")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 9.75f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1487.167f, 32.00001f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(486.8333f, 60.48243f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel39).Borders = (BorderSide)7;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(0f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(2000f, 121.4375f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			calculatedField1.DisplayName = "numeroDocumento";
			calculatedField1.Expression = "Concat('Nº do documento: ', [Numero])";
			calculatedField1.Name = "calculatedField1";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[11]
			{
				(XRControl)marca,
				(XRControl)xrLabel35,
				(XRControl)xrLabel26,
				(XRControl)localizacao,
				(XRControl)xrLabel32,
				(XRControl)xrLabel52,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel28,
				(XRControl)xrLabel27,
				(XRControl)produto
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail1).HeightF = 53.40685f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)marca).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)marca).Borders = (BorderSide)0;
			((XRControl)marca).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Marca")
			});
			((XRControl)marca).Dpi = 254f;
			((XRControl)marca).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)marca).LocationFloat = new PointFloat(635.8252f, 0f);
			((XRControl)marca).Name = "marca";
			((XRControl)marca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)marca).SizeF = new SizeF(192f, 47.83667f);
			((XRControl)marca).StylePriority.UseBorderDashStyle = false;
			((XRControl)marca).StylePriority.UseBorders = false;
			((XRControl)marca).StylePriority.UseFont = false;
			((XRControl)xrLabel35).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel35).Borders = (BorderSide)8;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(3f, 47.83667f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(1994f, 5.570175f);
			((XRControl)xrLabel35).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UsePadding = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel26).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel26).Borders = (BorderSide)0;
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(1828.021f, 0f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(168.979f, 47.83667f);
			((XRControl)xrLabel26).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)4;
			((XRControl)localizacao).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)localizacao).Borders = (BorderSide)0;
			((XRControl)localizacao).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Localizacao")
			});
			((XRControl)localizacao).Dpi = 254f;
			((XRControl)localizacao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)localizacao).LocationFloat = new PointFloat(827.8253f, 0f);
			((XRControl)localizacao).Name = "localizacao";
			((XRControl)localizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)localizacao).SizeF = new SizeF(192f, 47.83667f);
			((XRControl)localizacao).StylePriority.UseBorderDashStyle = false;
			((XRControl)localizacao).StylePriority.UseBorders = false;
			((XRControl)localizacao).StylePriority.UseFont = false;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1287.138f, 0f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(1019.825f, 0f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(87.31226f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:#.##}")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1107.138f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(130.2426f, 47.83667f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel28).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(1648.02f, 0f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(180.0006f, 47.83667f);
			((XRControl)xrLabel28).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1467.138f, 0f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(180.8821f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)produto).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)produto).Borders = (BorderSide)0;
			((XRControl)produto).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)produto).Dpi = 254f;
			((XRControl)produto).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)produto).LocationFloat = new PointFloat(130.2426f, 0f);
			((XRControl)produto).Name = "produto";
			((XRControl)produto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)produto).SizeF = new SizeF(505.5826f, 47.83667f);
			((XRControl)produto).StylePriority.UseBorderDashStyle = false;
			((XRControl)produto).StylePriority.UseBorders = false;
			((XRControl)produto).StylePriority.UseFont = false;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[29]
			{
				(XRControl)xrLabel5,
				(XRControl)xrLabel6,
				(XRControl)lblMarca,
				(XRControl)xrLabel23,
				(XRControl)lblLocalizacao,
				(XRControl)xrLabel49,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel25,
				(XRControl)xrLabel24,
				(XRControl)lblProduto,
				(XRControl)xrLabel22,
				(XRControl)xrLabel21,
				(XRControl)xrLabel20,
				(XRControl)xrLabel19,
				(XRControl)xrLabel18,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16,
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)xrLabel13,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel9,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)xrLabel33
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 307.0585f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Borders = (BorderSide)9;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 105.7741f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(142.8743f, 49.8367f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(142.8743f, 105.7743f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(333.3749f, 49.83669f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)lblMarca).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblMarca).Borders = (BorderSide)9;
			((XRControl)lblMarca).Dpi = 254f;
			((XRControl)lblMarca).Font = new Font("Segoe UI", 9f);
			((XRControl)lblMarca).LocationFloat = new PointFloat(635.8253f, 256.576f);
			((XRControl)lblMarca).Name = "lblMarca";
			((XRControl)lblMarca).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMarca).SizeF = new SizeF(192f, 50.48244f);
			((XRControl)lblMarca).StylePriority.UseBackColor = false;
			((XRControl)lblMarca).StylePriority.UseBorders = false;
			((XRControl)lblMarca).StylePriority.UseFont = false;
			((XRControl)lblMarca).StylePriority.UseTextAlignment = false;
			((XRControl)lblMarca).Text = "Marca";
			((XRControl)lblMarca).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel23).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel23).Borders = (BorderSide)12;
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(1828.021f, 256.576f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(171.9792f, 50.48244f);
			((XRControl)xrLabel23).StylePriority.UseBackColor = false;
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).Text = "Assinatura";
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)2;
			((XRControl)lblLocalizacao).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblLocalizacao).Borders = (BorderSide)9;
			((XRControl)lblLocalizacao).Dpi = 254f;
			((XRControl)lblLocalizacao).Font = new Font("Segoe UI", 9f);
			((XRControl)lblLocalizacao).LocationFloat = new PointFloat(827.8253f, 256.576f);
			((XRControl)lblLocalizacao).Name = "lblLocalizacao";
			((XRControl)lblLocalizacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblLocalizacao).SizeF = new SizeF(192f, 50.48244f);
			((XRControl)lblLocalizacao).StylePriority.UseBackColor = false;
			((XRControl)lblLocalizacao).StylePriority.UseBorders = false;
			((XRControl)lblLocalizacao).StylePriority.UseFont = false;
			((XRControl)lblLocalizacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblLocalizacao).Text = "Localização";
			((XRControl)lblLocalizacao).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)9;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(0f, 256.576f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(130.2426f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(1467.138f, 256.576f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(180.8821f, 50.48245f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(1019.825f, 256.576f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(87.31232f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)13;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(1648.02f, 256.576f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(180.0009f, 50.48245f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvida";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(1107.138f, 256.576f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Quantidade";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)lblProduto).BackColor = System.Drawing.Color.Silver;
			((XRControl)lblProduto).Borders = (BorderSide)9;
			((XRControl)lblProduto).Dpi = 254f;
			((XRControl)lblProduto).Font = new Font("Segoe UI", 9f);
			((XRControl)lblProduto).LocationFloat = new PointFloat(130.2426f, 256.576f);
			((XRControl)lblProduto).Name = "lblProduto";
			((XRControl)lblProduto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProduto).SizeF = new SizeF(505.5826f, 50.4825f);
			((XRControl)lblProduto).StylePriority.UseBackColor = false;
			((XRControl)lblProduto).StylePriority.UseBorders = false;
			((XRControl)lblProduto).StylePriority.UseFont = false;
			((XRControl)lblProduto).Text = "Produto";
			((XRControl)xrLabel22).Borders = (BorderSide)12;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1828.021f, 206.0935f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(171.979f, 50.48248f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1764.521f, 206.0935f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(63.49976f, 50.48251f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Text = "UF:";
			((XRControl)xrLabel20).Borders = (BorderSide)12;
			((XRControl)xrLabel20).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(571.5f, 206.0935f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(1193.021f, 50.4825f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Borders = (BorderSide)8;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(359.8333f, 206.0935f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(211.6667f, 50.48248f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Text = "Município:";
			((XRControl)xrLabel18).Borders = (BorderSide)12;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep", "{0:#####-###}")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(105.8333f, 206.0935f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(254f, 50.48248f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Borders = (BorderSide)9;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(0f, 206.0935f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(105.8333f, 50.48251f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Text = "CEP:";
			((XRControl)xrLabel16).Borders = (BorderSide)12;
			((XRControl)xrLabel16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1418.458f, 155.611f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(581.542f, 50.48248f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Borders = (BorderSide)8;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1282.018f, 155.611f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(136.4404f, 50.48245f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Text = "Bairro:";
			((XRControl)xrLabel14).Borders = (BorderSide)12;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(291.0417f, 155.611f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(990.9764f, 50.48248f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Borders = (BorderSide)9;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 155.611f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(291.0417f, 50.48248f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Complemento:";
			((XRControl)xrLabel12).Borders = (BorderSide)12;
			((XRControl)xrLabel12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(1746f, 105.7743f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(254f, 49.83669f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel11).Borders = (BorderSide)8;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1571.375f, 105.7743f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(174.6254f, 49.83669f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Número:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel10).Borders = (BorderSide)12;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(648.2283f, 105.7742f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(923.1464f, 49.83669f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Borders = (BorderSide)9;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(476.2491f, 105.7741f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(171.9792f, 49.83669f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Text = "Endereço:";
			((XRControl)xrLabel4).Borders = (BorderSide)12;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(145.5208f, 57.93752f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(1854.479f, 47.83665f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Borders = (BorderSide)9;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(0f, 57.93752f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(145.5208f, 47.83667f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Text = "Nome:";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)15;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(2000f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel33).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel33).Borders = (BorderSide)9;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(1287.138f, 256.576f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(180.0001f, 50.48245f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Pendente";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)2;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrTable3,
				(XRControl)xrLine1,
				(XRControl)xrLabel31,
				(XRControl)xrLabel30,
				(XRControl)xrLabel29
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).Font = new Font("Segoe UI", 11f);
			((XRControl)GroupFooter1).HeightF = 488.5274f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)GroupFooter1).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Segoe UI", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(3f, 0f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1994f, 31.4108f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(574.5f, 256.48f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(850f, 5f);
			((XRControl)xrLabel31).Borders = (BorderSide)8;
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(2.999999f, 262.1492f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(1994f, 99.37817f);
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).Text = "Assinatura recebedor";
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).LocationFloat = new PointFloat(1648.02f, 89.95834f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(298.9792f, 58.42f);
			((XRControl)xrLabel30).Text = "____/____/________";
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(1344.892f, 89.95834f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(303.1278f, 58.42f);
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).Text = "Data da entrega:";
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(1997f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(1997f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail1;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(1997f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblMensagemRodape });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 31.4108f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)xrTable3).Dpi = 254f;
			((XRControl)xrTable3).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTable3).LocationFloat = new PointFloat(0f, 361.5274f);
			((XRControl)xrTable3).Name = "xrTable3";
			xrTable3.Rows.AddRange((XRTableRow[])(object)new XRTableRow[2] { xrTableRow3, xrTableRow1 });
			((XRControl)xrTable3).SizeF = new SizeF(1998f, 127f);
			((XRControl)xrTable3).StylePriority.UseFont = false;
			xrTableRow3.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell12, xrTableCell14 });
			((XRControl)xrTableRow3).Dpi = 254f;
			((XRControl)xrTableRow3).Name = "xrTableRow3";
			xrTableRow3.Weight = 1.0;
			((XRControl)xrTableCell12).Borders = (BorderSide)8;
			((XRControl)xrTableCell12).Dpi = 254f;
			((XRControl)xrTableCell12).Font = new Font("Segoe UI", 9f);
			((XRControl)xrTableCell12).Name = "xrTableCell12";
			((XRControl)xrTableCell12).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrTableCell12).StylePriority.UseBorders = false;
			((XRControl)xrTableCell12).StylePriority.UseFont = false;
			((XRControl)xrTableCell12).StylePriority.UsePadding = false;
			((XRControl)xrTableCell12).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell12).Text = "Movimentações referenciadas:";
			((XRControl)xrTableCell12).TextAlignment = (TextAlignment)16;
			xrTableCell12.Weight = 0.6320039323970417;
			((XRControl)xrTableCell14).Borders = (BorderSide)8;
			((XRControl)xrTableCell14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "MovimentacoesReferenciadas")
			});
			((XRControl)xrTableCell14).Dpi = 254f;
			((XRControl)xrTableCell14).Font = new Font("Segoe UI Semibold", 9f);
			((XRControl)xrTableCell14).Name = "xrTableCell14";
			((XRControl)xrTableCell14).Padding = new PaddingInfo(0, 5, 0, 0, 254f);
			((XRControl)xrTableCell14).StylePriority.UseBorders = false;
			((XRControl)xrTableCell14).StylePriority.UseFont = false;
			((XRControl)xrTableCell14).StylePriority.UsePadding = false;
			((XRControl)xrTableCell14).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell14).TextAlignment = (TextAlignment)16;
			xrTableCell14.Weight = 2.1452587967933407;
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[2] { xrTableCell1, xrTableCell2 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 1.0;
			((XRControl)xrTableCell1).Borders = (BorderSide)8;
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Font = new Font("Segoe UI", 9f);
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrTableCell1).StylePriority.UseBorders = false;
			((XRControl)xrTableCell1).StylePriority.UseFont = false;
			((XRControl)xrTableCell1).StylePriority.UsePadding = false;
			((XRControl)xrTableCell1).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell1).Text = "Resumo financeiro das movimentações referenciadas:";
			((XRControl)xrTableCell1).TextAlignment = (TextAlignment)16;
			xrTableCell1.Weight = 1.0716801060919707;
			((XRControl)xrTableCell2).Borders = (BorderSide)8;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResumoFinanceiroMovimentacoesReferenciadas")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Font = new Font("Segoe UI Semibold", 9f);
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Padding = new PaddingInfo(0, 5, 0, 0, 254f);
			((XRControl)xrTableCell2).StylePriority.UseBorders = false;
			((XRControl)xrTableCell2).StylePriority.UseFont = false;
			((XRControl)xrTableCell2).StylePriority.UsePadding = false;
			((XRControl)xrTableCell2).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell2).TextAlignment = (TextAlignment)16;
			xrTableCell2.Weight = 1.7055826230984117;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport,
				(Band)PageFooter
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { calculatedField1 });
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(50, 50, 50, 50);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)xrTable3).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoControleEntregaConcluido : Relatorio
	{
		private readonly ControleEntregaDto _controleEntrega;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private PageHeaderBand PageHeader;

		private XRLabel xrLabel39;

		private XRLabel xrLabel1;

		private BindingSource bindingSource1;

		private CalculatedField calculatedField1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel xrLabel5;

		private XRLabel xrLabel6;

		private XRLabel xrLabel9;

		private XRLabel xrLabel10;

		private XRLabel xrLabel11;

		private XRLabel xrLabel12;

		private XRLabel xrLabel13;

		private XRLabel xrLabel14;

		private XRLabel xrLabel15;

		private XRLabel xrLabel16;

		private XRLabel xrLabel17;

		private XRLabel xrLabel18;

		private XRLabel xrLabel19;

		private XRLabel xrLabel20;

		private XRLabel xrLabel21;

		private XRLabel xrLabel22;

		private XRLabel xrLabel23;

		private XRLabel xrLabel24;

		private XRLabel xrLabel25;

		private XRLabel xrLabel26;

		private XRLabel xrLabel27;

		private XRLabel xrLabel28;

		private DetailReportBand detailEntregas;

		private DetailBand Detail2;

		private XRLabel xrLabel7;

		private XRLabel xrLabel8;

		private XRLabel xrLabel29;

		private XRLabel xrLabel30;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private XRLabel xrLabel33;

		private XRLabel xrLabel32;

		private XRLabel xrLabel31;

		private XRLabel xrLabel35;

		private XRLabel xrLabel36;

		private XRLabel xrLabel37;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel34;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel46;

		private XRLabel xrLabel45;

		private XRLabel xrLabel44;

		private XRLabel xrLabel43;

		private XRLabel xrLabel42;

		private XRLabel xrLabel41;

		private XRLabel xrLabel40;

		private XRLabel xrLabel38;

		private XRLabel xrLabel54;

		private XRLabel xrLabel52;

		private XRLabel xrLabel50;

		private XRLabel xrLabel53;

		private XRLabel xrLabel51;

		private XRLabel xrLabel49;

		private XRLabel xrLabel48;

		private XRLabel xrLabel47;

		private XRLabel xrLabel55;

		private XRLabel xrLabel56;

		private XRLabel xrLabel58;

		private XRLabel xrLabel57;

		private XRLabel xrLabel60;

		private XRLabel xrLabel59;

		private XRLine xrLine1;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRLabel xrLabel61;

		private XRLabel xrLabel62;

		private XRLabel lblMensagemRodape;

		private PageFooterBand PageFooter;

		public override string Descricao => "Controle de entrega";

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoControleEntregaConcluido(ControleEntregaDto controleEntrega)
		{
			_controleEntrega = controleEntrega;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XtraReportBase)this).DataSource = new List<ControleEntregaDto> { _controleEntrega };
			((XRControl)detailEntregas).Visible = _controleEntrega.RegistrosEntrega.Any();
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void xrLabel37_BeforePrint(object sender, PrintEventArgs e)
		{
			((XRControl)detailEntregas).Visible = _controleEntrega.RegistrosEntrega.Any();
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
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Expected O, but got Unknown
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Expected O, but got Unknown
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Expected O, but got Unknown
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Expected O, but got Unknown
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Expected O, but got Unknown
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b3: Expected O, but got Unknown
			//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Expected O, but got Unknown
			//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f5: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Expected O, but got Unknown
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_020b: Expected O, but got Unknown
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Expected O, but got Unknown
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Expected O, but got Unknown
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_024d: Expected O, but got Unknown
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected O, but got Unknown
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Expected O, but got Unknown
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Expected O, but got Unknown
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Expected O, but got Unknown
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0284: Expected O, but got Unknown
			//IL_0285: Unknown result type (might be due to invalid IL or missing references)
			//IL_028f: Expected O, but got Unknown
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_029a: Expected O, but got Unknown
			//IL_029b: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Expected O, but got Unknown
			//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b0: Expected O, but got Unknown
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bb: Expected O, but got Unknown
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c6: Expected O, but got Unknown
			//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Expected O, but got Unknown
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dc: Expected O, but got Unknown
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e7: Expected O, but got Unknown
			//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f2: Expected O, but got Unknown
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Expected O, but got Unknown
			//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0308: Expected O, but got Unknown
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Expected O, but got Unknown
			//IL_0314: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Expected O, but got Unknown
			//IL_031f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Expected O, but got Unknown
			//IL_032a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0334: Expected O, but got Unknown
			//IL_0335: Unknown result type (might be due to invalid IL or missing references)
			//IL_033f: Expected O, but got Unknown
			//IL_0340: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_034b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0355: Expected O, but got Unknown
			//IL_0356: Unknown result type (might be due to invalid IL or missing references)
			//IL_0360: Expected O, but got Unknown
			//IL_0361: Unknown result type (might be due to invalid IL or missing references)
			//IL_036b: Expected O, but got Unknown
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0376: Expected O, but got Unknown
			//IL_0377: Unknown result type (might be due to invalid IL or missing references)
			//IL_0381: Expected O, but got Unknown
			//IL_0382: Unknown result type (might be due to invalid IL or missing references)
			//IL_038c: Expected O, but got Unknown
			//IL_038d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Expected O, but got Unknown
			//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_043c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0491: Unknown result type (might be due to invalid IL or missing references)
			//IL_0555: Unknown result type (might be due to invalid IL or missing references)
			//IL_055b: Expected O, but got Unknown
			//IL_059a: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0650: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Unknown result type (might be due to invalid IL or missing references)
			//IL_0893: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0974: Unknown result type (might be due to invalid IL or missing references)
			//IL_097a: Expected O, but got Unknown
			//IL_09ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a7b: Expected O, but got Unknown
			//IL_0abb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b76: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7c: Expected O, but got Unknown
			//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c78: Expected O, but got Unknown
			//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ce1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d51: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d57: Expected O, but got Unknown
			//IL_0d97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e35: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3b: Expected O, but got Unknown
			//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3c: Expected O, but got Unknown
			//IL_0f7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_13a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_13d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1593: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1693: Unknown result type (might be due to invalid IL or missing references)
			//IL_16bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_173c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1742: Expected O, but got Unknown
			//IL_1782: Unknown result type (might be due to invalid IL or missing references)
			//IL_17ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_1843: Unknown result type (might be due to invalid IL or missing references)
			//IL_186c: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_18f3: Expected O, but got Unknown
			//IL_1933: Unknown result type (might be due to invalid IL or missing references)
			//IL_195c: Unknown result type (might be due to invalid IL or missing references)
			//IL_19e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a11: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a92: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a98: Expected O, but got Unknown
			//IL_1ad8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b01: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b8d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bb6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c53: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c59: Expected O, but got Unknown
			//IL_1c99: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d77: Unknown result type (might be due to invalid IL or missing references)
			//IL_1df8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dfe: Expected O, but got Unknown
			//IL_1e3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e67: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ef3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa2: Expected O, but got Unknown
			//IL_1fe2: Unknown result type (might be due to invalid IL or missing references)
			//IL_200b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2097: Unknown result type (might be due to invalid IL or missing references)
			//IL_20c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2146: Unknown result type (might be due to invalid IL or missing references)
			//IL_214c: Expected O, but got Unknown
			//IL_218c: Unknown result type (might be due to invalid IL or missing references)
			//IL_21b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2241: Unknown result type (might be due to invalid IL or missing references)
			//IL_226a: Unknown result type (might be due to invalid IL or missing references)
			//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_22f1: Expected O, but got Unknown
			//IL_2331: Unknown result type (might be due to invalid IL or missing references)
			//IL_235a: Unknown result type (might be due to invalid IL or missing references)
			//IL_23e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_240f: Unknown result type (might be due to invalid IL or missing references)
			//IL_248f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2495: Expected O, but got Unknown
			//IL_24d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_24fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_259b: Unknown result type (might be due to invalid IL or missing references)
			//IL_25c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2682: Unknown result type (might be due to invalid IL or missing references)
			//IL_26ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_2786: Unknown result type (might be due to invalid IL or missing references)
			//IL_27af: Unknown result type (might be due to invalid IL or missing references)
			//IL_29cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_29f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ace: Unknown result type (might be due to invalid IL or missing references)
			//IL_2af7: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bde: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cc5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2da0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dc9: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ea1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2eca: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f61: Expected O, but got Unknown
			//IL_2fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fca: Unknown result type (might be due to invalid IL or missing references)
			//IL_305e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3087: Unknown result type (might be due to invalid IL or missing references)
			//IL_3118: Unknown result type (might be due to invalid IL or missing references)
			//IL_311e: Expected O, but got Unknown
			//IL_315e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3187: Unknown result type (might be due to invalid IL or missing references)
			//IL_3343: Unknown result type (might be due to invalid IL or missing references)
			//IL_3370: Unknown result type (might be due to invalid IL or missing references)
			//IL_3430: Unknown result type (might be due to invalid IL or missing references)
			//IL_3436: Expected O, but got Unknown
			//IL_3476: Unknown result type (might be due to invalid IL or missing references)
			//IL_349f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3549: Unknown result type (might be due to invalid IL or missing references)
			//IL_354f: Expected O, but got Unknown
			//IL_358f: Unknown result type (might be due to invalid IL or missing references)
			//IL_35b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_364a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3650: Expected O, but got Unknown
			//IL_3690: Unknown result type (might be due to invalid IL or missing references)
			//IL_36b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3768: Unknown result type (might be due to invalid IL or missing references)
			//IL_376e: Expected O, but got Unknown
			//IL_37ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_37d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3881: Unknown result type (might be due to invalid IL or missing references)
			//IL_3887: Expected O, but got Unknown
			//IL_38c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_38f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_39ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a21: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c05: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c94: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d37: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e98: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ec1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f86: Unknown result type (might be due to invalid IL or missing references)
			//IL_4006: Unknown result type (might be due to invalid IL or missing references)
			//IL_400c: Expected O, but got Unknown
			//IL_404c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4075: Unknown result type (might be due to invalid IL or missing references)
			//IL_40e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_40eb: Expected O, but got Unknown
			//IL_412b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4154: Unknown result type (might be due to invalid IL or missing references)
			//IL_41c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_41cf: Expected O, but got Unknown
			//IL_420f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4238: Unknown result type (might be due to invalid IL or missing references)
			//IL_42a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_42ae: Expected O, but got Unknown
			//IL_42ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_4317: Unknown result type (might be due to invalid IL or missing references)
			//IL_43a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_43cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_445e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4464: Expected O, but got Unknown
			//IL_44a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_44d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_4550: Unknown result type (might be due to invalid IL or missing references)
			//IL_456a: Unknown result type (might be due to invalid IL or missing references)
			//IL_45a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_45f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_460a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4645: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			PageHeader = new PageHeaderBand();
			xrLabel1 = new XRLabel();
			xrLabel39 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			calculatedField1 = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel62 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel54 = new XRLabel();
			xrLabel52 = new XRLabel();
			xrLabel50 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel28 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel57 = new XRLabel();
			xrLabel53 = new XRLabel();
			xrLabel51 = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel23 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel25 = new XRLabel();
			detailEntregas = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel55 = new XRLabel();
			xrLabel48 = new XRLabel();
			xrLabel35 = new XRLabel();
			xrLabel36 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel29 = new XRLabel();
			xrLabel30 = new XRLabel();
			DetailReport2 = new DetailReportBand();
			Detail3 = new DetailBand();
			xrLabel61 = new XRLabel();
			xrLabel56 = new XRLabel();
			xrLabel47 = new XRLabel();
			xrLabel33 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel31 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			xrLabel34 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			lblMensagemRodape = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel59 = new XRLabel();
			xrLabel46 = new XRLabel();
			xrLabel45 = new XRLabel();
			xrLabel44 = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel42 = new XRLabel();
			xrLabel41 = new XRLabel();
			xrLabel40 = new XRLabel();
			xrLabel38 = new XRLabel();
			xrLabel60 = new XRLabel();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			PageFooter = new PageFooterBand();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 101f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 101f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel39
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI", 9.75f);
			((XRControl)PageHeader).HeightF = 121.4375f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrLabel1).Borders = (BorderSide)15;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "calculatedField1")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 9.75f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1300.875f, 31.95507f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(486.8333f, 59.48243f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel39).Borders = (BorderSide)2;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel39).LocationFloat = new PointFloat(0.6220939f, 0f);
			xrLabel39.Multiline = true;
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(1797.316f, 121.4375f);
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseFont = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "CONTROLE DE ENTREGA";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)32;
			bindingSource1.DataSource = typeof(ControleEntregaDto);
			calculatedField1.DisplayName = "numeroDocumento";
			calculatedField1.Expression = "Concat('Nº do documento: ', [Numero])";
			calculatedField1.Name = "calculatedField1";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel62,
				(XRControl)xrLabel58,
				(XRControl)xrLabel54,
				(XRControl)xrLabel52,
				(XRControl)xrLabel50,
				(XRControl)xrLabel26,
				(XRControl)xrLabel27,
				(XRControl)xrLabel28
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 53.40685f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel62).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel62).Borders = (BorderSide)8;
			((XRControl)xrLabel62).Dpi = 254f;
			((XRControl)xrLabel62).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel62).LocationFloat = new PointFloat(0f, 47.83667f);
			((XRControl)xrLabel62).Name = "xrLabel62";
			((XRControl)xrLabel62).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel62).SizeF = new SizeF(1793.969f, 5.570175f);
			((XRControl)xrLabel62).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel62).StylePriority.UseBorders = false;
			((XRControl)xrLabel62).StylePriority.UseFont = false;
			((XRControl)xrLabel62).StylePriority.UsePadding = false;
			((XRControl)xrLabel62).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel62).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel58).Borders = (BorderSide)0;
			((XRControl)xrLabel58).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadePendente", "{0:n2}")
			});
			((XRControl)xrLabel58).Dpi = 254f;
			((XRControl)xrLabel58).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel58).LocationFloat = new PointFloat(1258.001f, 0f);
			((XRControl)xrLabel58).Name = "xrLabel58";
			((XRControl)xrLabel58).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel58).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel58).StylePriority.UseBorders = false;
			((XRControl)xrLabel58).StylePriority.UseFont = false;
			((XRControl)xrLabel58).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel58).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel54).Borders = (BorderSide)0;
			((XRControl)xrLabel54).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel54).Dpi = 254f;
			((XRControl)xrLabel54).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel54).LocationFloat = new PointFloat(1438.001f, 0f);
			((XRControl)xrLabel54).Name = "xrLabel54";
			((XRControl)xrLabel54).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel54).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel54).StylePriority.UseBorders = false;
			((XRControl)xrLabel54).StylePriority.UseFont = false;
			((XRControl)xrLabel54).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel54).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel52).Borders = (BorderSide)0;
			((XRControl)xrLabel52).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel52).Dpi = 254f;
			((XRControl)xrLabel52).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel52).LocationFloat = new PointFloat(968.2113f, 0f);
			((XRControl)xrLabel52).Name = "xrLabel52";
			((XRControl)xrLabel52).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel52).SizeF = new SizeF(109.7894f, 47.83667f);
			((XRControl)xrLabel52).StylePriority.UseBorders = false;
			((XRControl)xrLabel52).StylePriority.UseFont = false;
			((XRControl)xrLabel52).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel52).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel50).Borders = (BorderSide)0;
			((XRControl)xrLabel50).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel50).LocationFloat = new PointFloat(2.622094f, 0f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(239.6037f, 47.83667f);
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel26).Borders = (BorderSide)0;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(242.2259f, 0f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(725.9854f, 47.83667f);
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1078.001f, 0f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(1618.001f, 0f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(180f, 47.83667f);
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)4;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[26]
			{
				(XRControl)xrLabel57,
				(XRControl)xrLabel53,
				(XRControl)xrLabel51,
				(XRControl)xrLabel49,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)xrLabel5,
				(XRControl)xrLabel6,
				(XRControl)xrLabel9,
				(XRControl)xrLabel10,
				(XRControl)xrLabel11,
				(XRControl)xrLabel12,
				(XRControl)xrLabel13,
				(XRControl)xrLabel14,
				(XRControl)xrLabel15,
				(XRControl)xrLabel16,
				(XRControl)xrLabel17,
				(XRControl)xrLabel18,
				(XRControl)xrLabel19,
				(XRControl)xrLabel20,
				(XRControl)xrLabel21,
				(XRControl)xrLabel22,
				(XRControl)xrLabel23,
				(XRControl)xrLabel24,
				(XRControl)xrLabel25
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 305.0584f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel57).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel57).Borders = (BorderSide)9;
			((XRControl)xrLabel57).Dpi = 254f;
			((XRControl)xrLabel57).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel57).LocationFloat = new PointFloat(1258.001f, 254.5759f);
			((XRControl)xrLabel57).Name = "xrLabel57";
			((XRControl)xrLabel57).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel57).SizeF = new SizeF(180f, 50.48245f);
			((XRControl)xrLabel57).StylePriority.UseBackColor = false;
			((XRControl)xrLabel57).StylePriority.UseBorders = false;
			((XRControl)xrLabel57).StylePriority.UseFont = false;
			((XRControl)xrLabel57).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel57).Text = "Pendente";
			((XRControl)xrLabel57).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel53).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel53).Borders = (BorderSide)9;
			((XRControl)xrLabel53).Dpi = 254f;
			((XRControl)xrLabel53).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel53).LocationFloat = new PointFloat(1438.001f, 254.5757f);
			((XRControl)xrLabel53).Name = "xrLabel53";
			((XRControl)xrLabel53).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel53).SizeF = new SizeF(180f, 50.48242f);
			((XRControl)xrLabel53).StylePriority.UseBackColor = false;
			((XRControl)xrLabel53).StylePriority.UseBorders = false;
			((XRControl)xrLabel53).StylePriority.UseFont = false;
			((XRControl)xrLabel53).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel53).Text = "Entregue";
			((XRControl)xrLabel53).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel51).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel51).Borders = (BorderSide)9;
			((XRControl)xrLabel51).Dpi = 254f;
			((XRControl)xrLabel51).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel51).LocationFloat = new PointFloat(969.5219f, 254.5757f);
			((XRControl)xrLabel51).Name = "xrLabel51";
			((XRControl)xrLabel51).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel51).SizeF = new SizeF(108.4789f, 50.48245f);
			((XRControl)xrLabel51).StylePriority.UseBackColor = false;
			((XRControl)xrLabel51).StylePriority.UseBorders = false;
			((XRControl)xrLabel51).StylePriority.UseFont = false;
			((XRControl)xrLabel51).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel51).Text = "UN";
			((XRControl)xrLabel51).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel49).Borders = (BorderSide)8;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(2.622094f, 254.5759f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(239.6037f, 50.48248f);
			((XRControl)xrLabel49).StylePriority.UseBackColor = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).Text = "Código";
			((XRControl)xrLabel2).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel2).Borders = (BorderSide)10;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(2.621933f, 0f);
			xrLabel2.Multiline = true;
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1795.378f, 57.9375f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Identificação do cliente";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(2.621933f, 57.93752f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(145.5208f, 47.83667f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Text = "Nome:";
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(148.1428f, 57.93752f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(1649.858f, 47.83665f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).WordWrap = false;
			((XRControl)xrLabel5).Borders = (BorderSide)8;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(2.622094f, 105.7742f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(145.5206f, 47.83665f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Text = "Telefone:";
			((XRControl)xrLabel6).Borders = (BorderSide)12;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.TelefonePrincipal.Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(148.1427f, 105.7743f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(333.399f, 47.83656f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Borders = (BorderSide)8;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(481.5417f, 105.774f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(161.372f, 47.8367f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).Text = "Endereço:";
			((XRControl)xrLabel10).Borders = (BorderSide)12;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Logradouro")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(642.9136f, 105.774f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(726.4615f, 47.8367f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel11).Borders = (BorderSide)8;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1369.375f, 105.7742f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(174.6254f, 47.83669f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Número:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel12).Borders = (BorderSide)8;
			((XRControl)xrLabel12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Numero")
			});
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(1544f, 105.7741f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(254f, 47.83669f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Borders = (BorderSide)8;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(2.621933f, 153.6109f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(291.0417f, 50.48248f);
			((XRControl)xrLabel13).StylePriority.UseBorders = false;
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Complemento:";
			((XRControl)xrLabel14).Borders = (BorderSide)12;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Complemento")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(293.6637f, 153.6109f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(886.3542f, 50.48248f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Borders = (BorderSide)8;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1180.018f, 153.6109f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(136.4404f, 50.48245f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Text = "Bairro:";
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Bairro")
			});
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1316.458f, 153.6109f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(481.5416f, 50.48248f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(2.621933f, 204.0933f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(105.8333f, 50.48251f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).Text = "CEP:";
			((XRControl)xrLabel18).Borders = (BorderSide)12;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Cep", "{0:#####-###}")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(108.4553f, 204.0933f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(254f, 50.48248f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Borders = (BorderSide)8;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(362.4553f, 204.0933f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(211.6667f, 50.48248f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Text = "Município:";
			((XRControl)xrLabel20).Borders = (BorderSide)12;
			((XRControl)xrLabel20).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.Nome")
			});
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(574.1219f, 204.0933f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(988.3989f, 50.48248f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1562.521f, 204.0933f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(76.72925f, 50.48253f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Text = "UF:";
			((XRControl)xrLabel22).Borders = (BorderSide)8;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.EnderecoSelecionado.Municipio.UF.Sigla")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1639.25f, 204.0933f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(158.75f, 50.48248f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel23).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel23).Borders = (BorderSide)9;
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(242.2258f, 254.5757f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(727.2961f, 50.48245f);
			((XRControl)xrLabel23).StylePriority.UseBackColor = false;
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).Text = "Produto";
			((XRControl)xrLabel24).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel24).Borders = (BorderSide)9;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(1078.001f, 254.5759f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(180f, 50.48245f);
			((XRControl)xrLabel24).StylePriority.UseBackColor = false;
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Quantidade";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel25).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel25).Borders = (BorderSide)9;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(1618.001f, 254.5759f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(179.9995f, 50.48245f);
			((XRControl)xrLabel25).StylePriority.UseBackColor = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Devolvida";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)2;
			((XtraReportBase)detailEntregas).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail2,
				(Band)DetailReport2,
				(Band)GroupHeader2
			});
			((XtraReportBase)detailEntregas).DataMember = "RegistrosEntrega";
			((XtraReportBase)detailEntregas).DataSource = bindingSource1;
			((XRControl)detailEntregas).Dpi = 254f;
			detailEntregas.Level = 1;
			((XRControl)detailEntregas).Name = "detailEntregas";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel55,
				(XRControl)xrLabel48,
				(XRControl)xrLabel35,
				(XRControl)xrLabel36,
				(XRControl)xrLabel37,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel29,
				(XRControl)xrLabel30
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).Font = new Font("Segoe UI", 9f);
			((XRControl)Detail2).HeightF = 100.965f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)Detail2).StylePriority.UseFont = false;
			((XRControl)xrLabel55).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel55).Borders = (BorderSide)9;
			((XRControl)xrLabel55).Dpi = 254f;
			((XRControl)xrLabel55).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel55).LocationFloat = new PointFloat(1222.35f, 50.48254f);
			((XRControl)xrLabel55).Name = "xrLabel55";
			((XRControl)xrLabel55).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel55).SizeF = new SizeF(103.1875f, 50.48245f);
			((XRControl)xrLabel55).StylePriority.UseBackColor = false;
			((XRControl)xrLabel55).StylePriority.UseBorders = false;
			((XRControl)xrLabel55).StylePriority.UseFont = false;
			((XRControl)xrLabel55).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel55).Text = "UN";
			((XRControl)xrLabel55).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel48).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel48).Borders = (BorderSide)8;
			((XRControl)xrLabel48).Dpi = 254f;
			((XRControl)xrLabel48).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel48).LocationFloat = new PointFloat(0f, 50.48254f);
			((XRControl)xrLabel48).Name = "xrLabel48";
			((XRControl)xrLabel48).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel48).SizeF = new SizeF(255.455f, 50.48247f);
			((XRControl)xrLabel48).StylePriority.UseBackColor = false;
			((XRControl)xrLabel48).StylePriority.UseBorders = false;
			((XRControl)xrLabel48).StylePriority.UseFont = false;
			((XRControl)xrLabel48).Text = "Código";
			((XRControl)xrLabel35).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel35).Borders = (BorderSide)9;
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(255.455f, 50.48254f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(966.8954f, 50.48247f);
			((XRControl)xrLabel35).StylePriority.UseBackColor = false;
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).Text = "Produto";
			((XRControl)xrLabel36).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel36).Borders = (BorderSide)9;
			((XRControl)xrLabel36).Dpi = 254f;
			((XRControl)xrLabel36).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel36).LocationFloat = new PointFloat(1325.538f, 50.4825f);
			((XRControl)xrLabel36).Name = "xrLabel36";
			((XRControl)xrLabel36).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel36).SizeF = new SizeF(235.4791f, 50.48245f);
			((XRControl)xrLabel36).StylePriority.UseBackColor = false;
			((XRControl)xrLabel36).StylePriority.UseBorders = false;
			((XRControl)xrLabel36).StylePriority.UseFont = false;
			((XRControl)xrLabel36).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel36).Text = "Entregue";
			((XRControl)xrLabel36).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel37).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel37).Borders = (BorderSide)9;
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel37).LocationFloat = new PointFloat(1561.018f, 50.48257f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(236.9824f, 50.48244f);
			((XRControl)xrLabel37).StylePriority.UseBackColor = false;
			((XRControl)xrLabel37).StylePriority.UseBorders = false;
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "Devolvido";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel37).BeforePrint += xrLabel37_BeforePrint;
			((XRControl)xrLabel7).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel7).Borders = (BorderSide)10;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(206.3512f, 50.48251f);
			((XRControl)xrLabel7).StylePriority.UseBackColor = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).Text = "Data entrega:";
			((XRControl)xrLabel8).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel8).Borders = (BorderSide)10;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Data")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(206.3512f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(504.2354f, 50.48251f);
			((XRControl)xrLabel8).StylePriority.UseBackColor = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel29).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel29).Borders = (BorderSide)11;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).LocationFloat = new PointFloat(710.5866f, 1.144409E-05f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(238.125f, 50.4825f);
			((XRControl)xrLabel29).StylePriority.UseBackColor = false;
			((XRControl)xrLabel29).StylePriority.UseBorders = false;
			((XRControl)xrLabel29).Text = "Quem recebeu:";
			((XRControl)xrLabel30).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel30).Borders = (BorderSide)10;
			((XRControl)xrLabel30).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Responsavel")
			});
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(948.7116f, 0f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(849.2888f, 50.4825f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XtraReportBase)DetailReport2).Bands.AddRange((Band[])(object)new Band[1] { (Band)Detail3 });
			((XtraReportBase)DetailReport2).DataMember = "RegistrosEntrega.Itens";
			((XtraReportBase)DetailReport2).DataSource = bindingSource1;
			((XRControl)DetailReport2).Dpi = 254f;
			DetailReport2.Level = 0;
			((XRControl)DetailReport2).Name = "DetailReport2";
			((XRControl)Detail3).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel61,
				(XRControl)xrLabel56,
				(XRControl)xrLabel47,
				(XRControl)xrLabel33,
				(XRControl)xrLabel32,
				(XRControl)xrLabel31
			});
			((XRControl)Detail3).Dpi = 254f;
			((XRControl)Detail3).Font = new Font("Segoe UI", 9.75f);
			((XRControl)Detail3).HeightF = 53.40685f;
			((XRControl)Detail3).Name = "Detail3";
			((XRControl)Detail3).StylePriority.UseFont = false;
			((XRControl)xrLabel61).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel61).Borders = (BorderSide)8;
			((XRControl)xrLabel61).Dpi = 254f;
			((XRControl)xrLabel61).Font = new Font("Segoe UI", 8.25f);
			((XRControl)xrLabel61).LocationFloat = new PointFloat(3.307291f, 47.83667f);
			((XRControl)xrLabel61).Name = "xrLabel61";
			((XRControl)xrLabel61).Padding = new PaddingInfo(257, 5, 0, 0, 254f);
			((XRControl)xrLabel61).SizeF = new SizeF(1790.662f, 5.570175f);
			((XRControl)xrLabel61).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel61).StylePriority.UseBorders = false;
			((XRControl)xrLabel61).StylePriority.UseFont = false;
			((XRControl)xrLabel61).StylePriority.UsePadding = false;
			((XRControl)xrLabel61).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel61).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel56).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel56).Borders = (BorderSide)0;
			((XRControl)xrLabel56).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.UnidadeMedida", "{0:n2}")
			});
			((XRControl)xrLabel56).Dpi = 254f;
			((XRControl)xrLabel56).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel56).LocationFloat = new PointFloat(1222.35f, 0f);
			((XRControl)xrLabel56).Name = "xrLabel56";
			((XRControl)xrLabel56).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel56).SizeF = new SizeF(104.6907f, 47.83667f);
			((XRControl)xrLabel56).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel56).StylePriority.UseBorders = false;
			((XRControl)xrLabel56).StylePriority.UseFont = false;
			((XRControl)xrLabel56).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel56).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel47).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel47).Borders = (BorderSide)0;
			((XRControl)xrLabel47).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.CodigoInterno")
			});
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(0.6220939f, 0f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(252.8329f, 47.83667f);
			((XRControl)xrLabel47).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel33).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel33).Borders = (BorderSide)0;
			((XRControl)xrLabel33).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.QuantidadeDevolvida", "{0:n2}")
			});
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(1561.018f, 0f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(236.9823f, 47.83667f);
			((XRControl)xrLabel33).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.QuantidadeEntregue", "{0:n2}")
			});
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1327.041f, 0f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(233.9766f, 47.83667f);
			((XRControl)xrLabel32).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel31).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "RegistrosEntrega.Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(255.455f, 0f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(966.8954f, 47.83667f);
			((XRControl)xrLabel31).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel34 });
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 94.97919f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)xrLabel34).BackColor = System.Drawing.Color.Silver;
			((XRControl)xrLabel34).Borders = (BorderSide)2;
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(0f, 37.04167f);
			xrLabel34.Multiline = true;
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(1798f, 57.9375f);
			((XRControl)xrLabel34).StylePriority.UseBackColor = false;
			((XRControl)xrLabel34).StylePriority.UseBorders = false;
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel34).Text = "Registros de entrega";
			((XRControl)xrLabel34).TextAlignment = (TextAlignment)32;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[11]
			{
				(XRControl)xrLine1,
				(XRControl)xrLabel59,
				(XRControl)xrLabel46,
				(XRControl)xrLabel45,
				(XRControl)xrLabel44,
				(XRControl)xrLabel43,
				(XRControl)xrLabel42,
				(XRControl)xrLabel41,
				(XRControl)xrLabel40,
				(XRControl)xrLabel38,
				(XRControl)xrLabel60
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).Font = new Font("Segoe UI", 9.75f);
			((XRControl)ReportFooter).HeightF = 338.7117f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)ReportFooter).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Segoe UI", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(2.621933f, 0f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1791.347f, 31.4108f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(1798f, 5.291667f);
			((XRControl)xrLabel59).Borders = (BorderSide)2;
			((XRControl)xrLabel59).Dpi = 254f;
			((XRControl)xrLabel59).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel59).LocationFloat = new PointFloat(0f, 238.7117f);
			((XRControl)xrLabel59).Name = "xrLabel59";
			((XRControl)xrLabel59).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel59).SizeF = new SizeF(1793.969f, 49.99998f);
			((XRControl)xrLabel59).StylePriority.UseBorders = false;
			((XRControl)xrLabel59).StylePriority.UseFont = false;
			((XRControl)xrLabel59).Text = "Movimentações referenciadas:";
			((XRControl)xrLabel46).Borders = (BorderSide)2;
			((XRControl)xrLabel46).Dpi = 254f;
			((XRControl)xrLabel46).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel46).LocationFloat = new PointFloat(0f, 188.7117f);
			((XRControl)xrLabel46).Name = "xrLabel46";
			((XRControl)xrLabel46).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel46).SizeF = new SizeF(190.5f, 49.99998f);
			((XRControl)xrLabel46).StylePriority.UseBorders = false;
			((XRControl)xrLabel46).StylePriority.UseFont = false;
			((XRControl)xrLabel46).Text = "Observação:";
			((XRControl)xrLabel45).Borders = (BorderSide)2;
			((XRControl)xrLabel45).Dpi = 254f;
			((XRControl)xrLabel45).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel45).LocationFloat = new PointFloat(0f, 138.7117f);
			((XRControl)xrLabel45).Name = "xrLabel45";
			((XRControl)xrLabel45).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel45).SizeF = new SizeF(137.5833f, 50.00002f);
			((XRControl)xrLabel45).StylePriority.UseBorders = false;
			((XRControl)xrLabel45).StylePriority.UseFont = false;
			((XRControl)xrLabel45).Text = "Usuário:";
			((XRControl)xrLabel44).Borders = (BorderSide)2;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(0f, 88.71164f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(92.6041f, 49.99999f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).Text = "Data:";
			((XRControl)xrLabel43).Borders = (BorderSide)2;
			((XRControl)xrLabel43).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Observacao")
			});
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel43).LocationFloat = new PointFloat(190.4998f, 188.7117f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(1607.501f, 49.99998f);
			((XRControl)xrLabel43).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel42).Borders = (BorderSide)2;
			((XRControl)xrLabel42).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.NomeUsuario")
			});
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel42).LocationFloat = new PointFloat(137.5832f, 138.7117f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(1660.417f, 50.00002f);
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseFont = false;
			((XRControl)xrLabel41).Borders = (BorderSide)2;
			((XRControl)xrLabel41).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Data", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel41).LocationFloat = new PointFloat(92.60393f, 88.71164f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(1705.396f, 49.99999f);
			((XRControl)xrLabel41).StylePriority.UseBorders = false;
			((XRControl)xrLabel41).StylePriority.UseFont = false;
			((XRControl)xrLabel40).Borders = (BorderSide)2;
			((XRControl)xrLabel40).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Historico.Situacao.Descricao")
			});
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel40).LocationFloat = new PointFloat(481.5415f, 38.2291f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(1316.459f, 50f);
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseFont = false;
			((XRControl)xrLabel38).Borders = (BorderSide)2;
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel38).LocationFloat = new PointFloat(0f, 38.2291f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(481.5417f, 50f);
			((XRControl)xrLabel38).StylePriority.UseBorders = false;
			((XRControl)xrLabel38).StylePriority.UseFont = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel38).Text = "Situação do controle de entrega:";
			((XRControl)xrLabel60).Borders = (BorderSide)10;
			((XRControl)xrLabel60).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "MovimentacoesReferenciadas")
			});
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(2.621933f, 288.7117f);
			xrLabel60.Multiline = true;
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(1791.347f, 49.99998f);
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)PageHeader;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3.307291f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(1793.969f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(1793.969f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)PageHeader;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(1793.969f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3.96875f;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblMensagemRodape });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 31.4108f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[8]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)DetailReport,
				(Band)detailEntregas,
				(Band)ReportFooter,
				(Band)PageFooter
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { calculatedField1 });
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[2]
			{
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(201, 101, 101, 101);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.ControlesEntrega.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.ControlesEntrega.Properties.Resources", typeof(Resources).Assembly);
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

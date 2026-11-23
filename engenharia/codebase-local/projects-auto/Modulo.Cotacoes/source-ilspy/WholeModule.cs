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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Auxiliares.MensagensWhatsappModelo;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Compras.Cotacoes;
using DigisatServer.Application.Main.Dto.Financeiros.Formas;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.ComprasModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.EntradasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Function;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Interfaces.EnvioEmail;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesCotacao;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp.Classes;
using DigisatServer.Infrastructure.Crosscutting.Service.EnvioEmail;
using DigisatServer.Infrastructure.Crosscutting.Util;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Newtonsoft.Json;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Infraestrutura.ViewModels.ListasItemNaFicha;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Modulo.Cotacoes.Reports;
using Otus.Modulo.Cotacoes.Services;
using Otus.Modulo.Cotacoes.ViewModels.Abas;
using Otus.Modulo.Cotacoes.ViewModels.Apuracao;
using Otus.Modulo.Cotacoes.ViewModels.Itens;
using Otus.Modulo.Cotacoes.ViewModels.Participantes;
using Otus.Modulo.Cotacoes.ViewModels.Propostas;
using Otus.Modulo.Cotacoes.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Cotacoes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Cotacoes")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Cotacoes
{
	[Module(ModuleName = "CotacaoModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ProdutoServicoModulo")]
	public class CotacaoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public CotacaoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<CotacoesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CotacaoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CotacaoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CotacaoFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Cotacoes.Views
{
	public class CotacaoFichaAdicionarView : CustomUserControl, IComponentConnector
	{
		internal DXTabControl TabControlBase;

		private bool _contentLoaded;

		public CotacaoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaadicionarview.xaml", UriKind.Relative);
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
				TabControlBase = (DXTabControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class CotacaoFichaAtualizarView : CustomUserControl, IComponentConnector
	{
		internal DXTabControl TabControlBase;

		private bool _contentLoaded;

		public CotacaoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaatualizarview.xaml", UriKind.Relative);
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
				TabControlBase = (DXTabControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class CotacaoListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CotacaoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaolistaview.xaml", UriKind.Relative);
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
	public class CotacoesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CotacoesView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacoesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Cotacoes.Views.CotacaoFichaViewDetalhes
{
	public class CotacaoApuracaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CotacaoApuracaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaviewdetalhes/cotacaoapuracaoview.xaml", UriKind.Relative);
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
	public class CotacaoFornecedoresView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterPessoa;

		private bool _contentLoaded;

		public CotacaoFornecedoresView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaviewdetalhes/cotacaofornecedoresview.xaml", UriKind.Relative);
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
				ParameterPessoa = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class CotacaoItensView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterProduto;

		private bool _contentLoaded;

		public CotacaoItensView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaviewdetalhes/cotacaoitensview.xaml", UriKind.Relative);
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
				ParameterProduto = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class CotacaoPropostasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CotacaoPropostasView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaviewdetalhes/cotacaopropostasview.xaml", UriKind.Relative);
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
	public class CotacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CotacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Cotacoes;component/views/cotacaofichaviewdetalhes/cotacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Cotacoes.ViewModels
{
	public class CotacaoFichaAdicionarViewModel : FichaBaseAdicionar<CotacaoDto, CotacaoViewModel, ICotacaoModuleService>
	{
		private readonly NavegacaoGradeItem _navegacaoGrade;

		public CotacaoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
			_navegacaoGrade = new NavegacaoGradeItem((NavigationBase)(object)this);
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			object obj = navigationParameters["CotacaoDto"];
			CotacaoDto val = (CotacaoDto)((obj is CotacaoDto) ? obj : null);
			if (val != null && navigationParameters[typeof(CotacaoViewModel).Name] == null)
			{
				navigationParameters.Add(typeof(CotacaoViewModel).Name, (object)new CotacaoViewModel(val));
			}
			((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).CriarFormulario();
			if (((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario is CotacaoFormulario cotacaoFormulario)
			{
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario = (IFormulario)(object)new CotacaoFormulario(((FormularioEntity<CotacaoDto>)cotacaoFormulario).Dto, null, null, null, null, _navegacaoGrade.Abrir);
			}
		}

		protected override async Task Gravar()
		{
			if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<Cotacao, ManterCotacao>(0m, (string)null) == null)
			{
				return;
			}
			try
			{
				if (DialogoFactory.MostrarDialogoSimNao("Abrir a cotação para receber propostas? Depois disso os itens e os fornecedores não poderão mais ser alterados.", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					await ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).Gravar();
					return;
				}
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DisableConfirmOnClose();
				LoaderProcessoService.MostrarLoader("Gravando...");
				CotacaoFichaAdicionarViewModel cotacaoFichaAdicionarViewModel = this;
				CotacaoFichaAdicionarViewModel cotacaoFichaAdicionarViewModel2 = this;
				IFormulario formulario = ((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario;
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)cotacaoFichaAdicionarViewModel).DtoAtualizado = await ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)cotacaoFichaAdicionarViewModel2).Persistir((IFormularioEntity)(object)((formulario is IFormularioEntity) ? formulario : null));
				try
				{
					LoaderProcessoService.MostrarLoader("Abrindo cotação...");
					((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DtoAtualizado = await base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.AbrirAsync(((EntityBaseDto)((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DtoAtualizado).Id, string.Empty)));
				}
				finally
				{
					((NavigationBase)this).ParametroRetornado += ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).OnParametroRetornado;
					((NavigationBase)this).FecharView();
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}
	}
	public class CotacaoFichaAtualizarViewModel : FichaBaseAtualizar<CotacaoDto, CotacaoViewModel, ICotacaoModuleService>
	{
		private const int DiasValidadeLinkFormulario = 7;

		private CotacaoDto _cotacaoDto;

		private NavegacaoGradeItem _navegacaoGrade;

		public ICommand AbrirCommand { get; set; }

		public ICommand ReabrirCommand { get; set; }

		public ICommand FecharCotacaoCommand { get; set; }

		public ICommand CancelarCotacaoCommand { get; set; }

		public ICommand GerarPedidosCompraCommand { get; set; }

		public ICommand EnviarAosFornecedoresCommand { get; set; }

		public ICommand EnviarAosFornecedoresWhatsAppCommand { get; set; }

		public ICommand ImportarRespostaArquivoCommand { get; set; }

		public ICommand ImportarRespostaTextoCommand { get; set; }

		public CotacaoFormulario FormularioCotacao => ((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario as CotacaoFormulario;

		public string DescricaoSituacao
		{
			get
			{
				if (_cotacaoDto != null && _cotacaoDto.Situacao != null)
				{
					return _cotacaoDto.Situacao.Descricao;
				}
				return string.Empty;
			}
		}

		private bool CotacaoPodeSerAlterada
		{
			get
			{
				if (_cotacaoDto != null)
				{
					return _cotacaoDto.EmAndamento;
				}
				return true;
			}
		}

		public CotacaoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Expected O, but got Unknown
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected O, but got Unknown
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Expected O, but got Unknown
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Expected O, but got Unknown
			//IL_0169: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Expected O, but got Unknown
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Expected O, but got Unknown
			_navegacaoGrade = new NavegacaoGradeItem((NavigationBase)(object)this);
			AbrirCommand = (ICommand)new AsyncCommand((Func<Task>)Abrir, (Func<bool>)PodeAbrir, (bool?)null);
			ReabrirCommand = (ICommand)new AsyncCommand((Func<Task>)Reabrir, (Func<bool>)PodeReabrir, (bool?)null);
			FecharCotacaoCommand = (ICommand)new AsyncCommand((Func<Task>)FecharCotacao, (Func<bool>)PodeFecharCotacao, (bool?)null);
			CancelarCotacaoCommand = (ICommand)new AsyncCommand((Func<Task>)CancelarCotacao, (Func<bool>)PodeCancelarCotacao, (bool?)null);
			GerarPedidosCompraCommand = (ICommand)new AsyncCommand((Func<Task>)GerarPedidosCompra, (Func<bool>)PodeGerarPedidosCompra, (bool?)null);
			EnviarAosFornecedoresCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarAosFornecedores, (Func<bool>)PodeEnviarAosFornecedores, (bool?)null);
			EnviarAosFornecedoresWhatsAppCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarAosFornecedoresWhatsApp, (Func<bool>)PodeEnviarAosFornecedoresWhatsApp, (bool?)null);
			ImportarRespostaArquivoCommand = (ICommand)new AsyncCommand((Func<Task>)ImportarRespostaArquivo, (Func<bool>)PodeImportarResposta, (bool?)null);
			ImportarRespostaTextoCommand = (ICommand)new AsyncCommand((Func<Task>)ImportarRespostaTexto, (Func<bool>)PodeImportarResposta, (bool?)null);
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (navigationParameters[typeof(CotacaoViewModel).Name] is CotacaoViewModel cotacaoViewModel)
			{
				_cotacaoDto = ((ViewModelDtoBase<CotacaoDto>)(object)cotacaoViewModel).Dto;
			}
			((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override bool PodeGravar()
		{
			if (((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).PodeGravar())
			{
				return CotacaoPodeSerAlterada;
			}
			return false;
		}

		public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
		{
			if (!CotacaoPodeSerAlterada)
			{
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DisableConfirmOnClose();
			}
			((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).ConfirmNavigationRequest(navigationContext, continuationCallback);
		}

		protected override async Task Gravar()
		{
			if (!Autorizado<ManterCotacao>())
			{
				return;
			}
			try
			{
				LoaderProcessoService.MostrarLoader("Gravando...");
				CotacaoFichaAtualizarViewModel cotacaoFichaAtualizarViewModel = this;
				CotacaoFichaAtualizarViewModel cotacaoFichaAtualizarViewModel2 = this;
				IFormulario formulario = ((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario;
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)cotacaoFichaAtualizarViewModel).DtoAtualizado = await ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)cotacaoFichaAtualizarViewModel2).Persistir((IFormularioEntity)(object)((formulario is IFormularioEntity) ? formulario : null));
				LoaderProcessoService.EsconderLoader();
				MarcarRetornoParaLista();
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DisableConfirmOnClose();
				((NavigationBase)this).FecharView();
			}
			catch (Exception ex)
			{
				LoaderProcessoService.EsconderLoader();
				HelperException.ShowLastException(ex, "", false, false);
				if (CotacaoFoiConcluidaPorOutroUsuario(ex))
				{
					((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DisableConfirmOnClose();
					((NavigationBase)this).FecharView();
				}
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private static bool CotacaoFoiConcluidaPorOutroUsuario(Exception excecao)
		{
			return ((excecao == null) ? string.Empty : HelperString.ToStringEmptyWhenNull(excecao.Message)).IndexOf("não pode ser alterada", StringComparison.CurrentCultureIgnoreCase) >= 0;
		}

		protected override void CriarFormulario()
		{
			if (_cotacaoDto == null)
			{
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).CriarFormulario();
			}
			else
			{
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario = (IFormulario)(object)new CotacaoFormulario(_cotacaoDto, RegistrarResposta, SimularApuracao, Apurar, VincularProdutoItem, _navegacaoGrade.Abrir);
			}
		}

		private async void RegistrarResposta(string participanteReferencia, RespostaCotacaoDto respostaDto)
		{
			if (!Autorizado<ManterCotacao>())
			{
				return;
			}
			await ExecutarAcao("Gravando resposta...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.RegistrarRespostaAsync(((EntityBaseDto)_cotacaoDto).Id, participanteReferencia, respostaDto))));
		}

		private async void SimularApuracao()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Apurando...");
				IEnumerable<ApuracaoItemDto> apuracoesDto = await base.Service.Canal<ICotacaoModuleService, Task<IEnumerable<ApuracaoItemDto>>>((Func<ICotacaoModuleService, Task<IEnumerable<ApuracaoItemDto>>>)((ICotacaoModuleService s) => s.SimularApuracaoAsync(((EntityBaseDto)_cotacaoDto).Id)));
				LoaderProcessoService.EsconderLoader();
				if (FormularioCotacao != null)
				{
					FormularioCotacao.ApuracaoViewModel.CarregarApuracoes(apuracoesDto);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private async void Apurar(IEnumerable<ApuracaoItemDto> apuracoesDto)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Confirmar a apuração desta cotação? Após confirmar, novas propostas não serão aceitas.", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await ExecutarAcao("Apurando...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.ApurarAsync(((EntityBaseDto)_cotacaoDto).Id, apuracoesDto, string.Empty))));
		}

		private async void VincularProdutoItem(string itemReferencia, ProdutoServicoEmpresaDto produtoDto, IEnumerable<GradeItemCotacaoDto> grades)
		{
			if (!Autorizado<ManterCotacao>() || HelperObject.IsNull((object)produtoDto) || string.IsNullOrWhiteSpace(itemReferencia))
			{
				return;
			}
			ProdutoServicoDto produtoServico = produtoDto.ProdutoServico;
			ProdutoDto val = ((produtoServico == null) ? null : produtoServico.Produto);
			string codigoInterno = ((produtoServico == null) ? string.Empty : produtoServico.CodigoInterno);
			string codigoBarras = ((val == null) ? string.Empty : val.CodigoBarras);
			string unidadeMedida = ((produtoServico == null || produtoServico.UnidadeMedida == null) ? string.Empty : produtoServico.UnidadeMedida.Descricao);
			string marca = ((val == null || val.Marca == null) ? string.Empty : val.Marca.Descricao);
			await ExecutarAcao("Vinculando produto...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.VincularProdutoItemAsync(((EntityBaseDto)_cotacaoDto).Id, itemReferencia, ((EntityBaseDto)produtoDto).Id, codigoInterno, codigoBarras, unidadeMedida, marca, grades))));
		}

		private async Task GerarPedidosCompra()
		{
			if (Autorizado<GerarPedidoCompraDaCotacao>())
			{
				CotacaoDto val = await GeradorPedidoCompraCotacao.Gerar(base.Service, _cotacaoDto);
				if (val != null)
				{
					((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DtoAtualizado = val;
					MarcarRetornoParaLista();
					((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DisableConfirmOnClose();
					((NavigationBase)this).FecharView();
				}
			}
		}

		private async Task EnviarAosFornecedores()
		{
			if (!Autorizado<ManterCotacao>())
			{
				return;
			}
			List<ParticipanteCotacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(_cotacaoDto.Participantes);
			List<ParticipanteCotacaoDto> comEmail = list.Where((ParticipanteCotacaoDto x) => x.Fornecedor != null && !string.IsNullOrWhiteSpace(x.Fornecedor.Email)).ToList();
			int semEmail = list.Count - comEmail.Count;
			if (!comEmail.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum fornecedor desta cotação possui e-mail cadastrado.", false, HorizontalAlignment.Center);
				return;
			}
			string arg = ((semEmail > 0) ? $"\n{semEmail} fornecedor(es) sem e-mail cadastrado ficarão de fora." : string.Empty) + AvisoValidadeLink(comAnexo: true);
			if (DialogoFactory.MostrarDialogoSimNao($"Enviar o formulário da cotação por e-mail para {comEmail.Count} fornecedor(es)?{arg}", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			List<string> enviados = new List<string>();
			List<string> falhas = new List<string>();
			try
			{
				LoaderProcessoService.MostrarLoader("Enviando cotação aos fornecedores...");
				ConfiguracaoEmailDto configuracaoEmail = await base.Service.Canal<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>((Func<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>)((IConfiguracaoModuleService s) => s.ObterConfiguracaoEmailCotacaoAsync()));
				string pasta = Path.Combine(Path.GetTempPath(), "DigisatCotacoes");
				Directory.CreateDirectory(pasta);
				Dictionary<string, IDictionary<string, string>> codigosFornecedor = await ObterCodigosFornecedorPorParticipante();
				foreach (ParticipanteCotacaoDto participante in comEmail)
				{
					try
					{
						string text = GeradorFormularioCotacaoHtml.Gerar(_cotacaoDto, participante, DadosSistema.EmpresaLogada.Nome, ObterCodigosFornecedor(codigosFornecedor, participante.Id));
						string caminho = Path.Combine(pasta, $"Cotacao_{_cotacaoDto.Numero}_{LimparNomeArquivo(participante.Fornecedor.Nome)}.html");
						File.WriteAllText(caminho, text, Encoding.UTF8);
						await EnviarEmailFormulario(configuracaoEmail, participante, caminho, await TentarPublicarLink(caminho, text));
						enviados.Add(participante.Id);
					}
					catch (Exception ex)
					{
						falhas.Add($"{participante.Fornecedor.Nome}: {ex.Message}");
					}
				}
				if (enviados.Any())
				{
					RecarregarFormulario(await base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.RegistrarEnvioAsync(((EntityBaseDto)_cotacaoDto).Id, (IEnumerable<string>)enviados))));
				}
				LoaderProcessoService.EsconderLoader();
				string text2 = $"{enviados.Count} de {comEmail.Count} convite(s) enviado(s).";
				if (semEmail > 0)
				{
					text2 += $"\n{semEmail} fornecedor(es) sem e-mail cadastrado.";
				}
				if (falhas.Any())
				{
					text2 = text2 + "\n\nFalhas:\n" + string.Join(Environment.NewLine, falhas);
				}
				DialogoFactory.MostrarDialogoConfirma(text2, false, HorizontalAlignment.Left);
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

		private async Task<Dictionary<string, IDictionary<string, string>>> ObterCodigosFornecedorPorParticipante()
		{
			try
			{
				return (from x in HelperIEnumerable.ToListEmptyWhenNull<CodigoFornecedorCotacaoDto>(await base.Service.Canal<ICotacaoModuleService, Task<IEnumerable<CodigoFornecedorCotacaoDto>>>((Func<ICotacaoModuleService, Task<IEnumerable<CodigoFornecedorCotacaoDto>>>)((ICotacaoModuleService s) => s.ObterCodigosFornecedorAsync(((EntityBaseDto)_cotacaoDto).Id))))
					group x by x.ParticipanteReferencia).ToDictionary((Func<IGrouping<string, CodigoFornecedorCotacaoDto>, string>)((IGrouping<string, CodigoFornecedorCotacaoDto> x) => x.Key), (Func<IGrouping<string, CodigoFornecedorCotacaoDto>, IDictionary<string, string>>)((IGrouping<string, CodigoFornecedorCotacaoDto> x) => (from c in x
					group c by c.ItemCotacaoReferencia).ToDictionary((IGrouping<string, CodigoFornecedorCotacaoDto> c) => c.Key, (IGrouping<string, CodigoFornecedorCotacaoDto> c) => c.First().CodigoFornecedor)));
			}
			catch (Exception)
			{
				return new Dictionary<string, IDictionary<string, string>>();
			}
		}

		private static IDictionary<string, string> ObterCodigosFornecedor(IDictionary<string, IDictionary<string, string>> codigos, string participanteReferencia)
		{
			if (participanteReferencia == null || !codigos.TryGetValue(participanteReferencia, out var value))
			{
				return new Dictionary<string, string>();
			}
			return value;
		}

		private static async Task<string> TentarPublicarLink(string caminho, string html)
		{
			try
			{
				ApiDigisatFtpService api = new ApiDigisatFtpService((IWebProxy)null);
				try
				{
					DadosArquivoFtp val = DadosArquivoFtp.Criar().ComArquivoEnvio(new ArquivoEnvio(Path.GetFileName(caminho), Convert.ToBase64String(Encoding.UTF8.GetBytes(html))));
					DadosArquivoFtp val2 = await api.Post<DadosArquivoFtp>(val);
					if (val2 == null || val2.ArquivoRetorno == null)
					{
						return null;
					}
					return string.IsNullOrWhiteSpace(val2.ArquivoRetorno.shorten_url) ? val2.ArquivoRetorno.full_url : val2.ArquivoRetorno.shorten_url;
				}
				finally
				{
					((IDisposable)api)?.Dispose();
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		private async Task EnviarAosFornecedoresWhatsApp()
		{
			if (!Autorizado<ManterCotacao>())
			{
				return;
			}
			if (!DadosSistema.WhatsAppConfigurado)
			{
				DialogoFactory.MostrarDialogoConfirma("Informe o IP ou nome do servidor WhatsApp (WD Connect) em Configurações > Auxiliares para enviar mensagens pelo WhatsApp.", false, HorizontalAlignment.Center);
				return;
			}
			List<ParticipanteCotacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(_cotacaoDto.Participantes);
			List<ParticipanteCotacaoDto> comTelefone = list.Where((ParticipanteCotacaoDto x) => x.Fornecedor != null && !string.IsNullOrWhiteSpace(x.Fornecedor.Telefone)).ToList();
			int semTelefone = list.Count - comTelefone.Count;
			if (!comTelefone.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum fornecedor desta cotação possui telefone cadastrado.", false, HorizontalAlignment.Center);
				return;
			}
			string arg = ((semTelefone > 0) ? $"\n{semTelefone} fornecedor(es) sem telefone cadastrado ficarão de fora." : string.Empty) + AvisoValidadeLink(comAnexo: false);
			if (DialogoFactory.MostrarDialogoSimNao($"Enviar o formulário da cotação por WhatsApp para {comTelefone.Count} fornecedor(es)?{arg}", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			List<string> enviados = new List<string>();
			List<string> falhas = new List<string>();
			try
			{
				LoaderProcessoService.MostrarLoader("Enviando cotação aos fornecedores...");
				string modelo = ObterModeloMensagemWhatsApp();
				string pasta = Path.Combine(Path.GetTempPath(), "DigisatCotacoes");
				Directory.CreateDirectory(pasta);
				Dictionary<string, IDictionary<string, string>> codigosFornecedor = await ObterCodigosFornecedorPorParticipante();
				foreach (ParticipanteCotacaoDto participante in comTelefone)
				{
					try
					{
						string text = GeradorFormularioCotacaoHtml.Gerar(_cotacaoDto, participante, DadosSistema.EmpresaLogada.Nome, ObterCodigosFornecedor(codigosFornecedor, participante.Id));
						string text2 = Path.Combine(pasta, $"Cotacao_{_cotacaoDto.Numero}_{LimparNomeArquivo(participante.Fornecedor.Nome)}.html");
						File.WriteAllText(text2, text, Encoding.UTF8);
						string text3 = await TentarPublicarLink(text2, text);
						if (string.IsNullOrWhiteSpace(text3))
						{
							throw new Exception("Não foi possível publicar o link do formulário.");
						}
						string text4 = MontarMensagemWhatsApp(modelo, participante, text3);
						string text5 = await WhatsAppService.Criar().ComTelefone(participante.Fornecedor.Telefone).ComMensagemWhatsApp(text4)
							.ComEnderecoServidor(DadosSistema.EnderecoServidorWhatsApp)
							.ComCpfCnpjEmitente(DadosSistema.EmpresaLogada.CpfCnpj)
							.ComAutoEnviar(true)
							.Compartilhar();
						if (!string.IsNullOrWhiteSpace(text5))
						{
							falhas.Add($"{participante.Fornecedor.Nome}: {text5}");
						}
						else
						{
							enviados.Add(participante.Id);
						}
					}
					catch (Exception ex)
					{
						falhas.Add($"{participante.Fornecedor.Nome}: {ex.Message}");
					}
				}
				if (enviados.Any())
				{
					RecarregarFormulario(await base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.RegistrarEnvioAsync(((EntityBaseDto)_cotacaoDto).Id, (IEnumerable<string>)enviados))));
				}
				LoaderProcessoService.EsconderLoader();
				string text6 = $"{enviados.Count} de {comTelefone.Count} convite(s) enviado(s) por WhatsApp.";
				if (semTelefone > 0)
				{
					text6 += $"\n{semTelefone} fornecedor(es) sem telefone cadastrado.";
				}
				if (falhas.Any())
				{
					text6 = text6 + "\n\nFalhas:\n" + string.Join(Environment.NewLine, falhas);
				}
				DialogoFactory.MostrarDialogoConfirma(text6, false, HorizontalAlignment.Left);
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

		private string ObterModeloMensagemWhatsApp()
		{
			ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
			object obj;
			if (configuracaoDto == null)
			{
				obj = null;
			}
			else
			{
				EntradaDto entrada = configuracaoDto.Entrada;
				if (entrada == null)
				{
					obj = null;
				}
				else
				{
					SelecaoDto modeloMensagemCotacao = entrada.ModeloMensagemCotacao;
					obj = ((modeloMensagemCotacao != null) ? modeloMensagemCotacao.Referencia : null);
				}
			}
			string referencia = (string)obj;
			if (string.IsNullOrWhiteSpace(referencia))
			{
				ConfiguracaoDto configuracaoDto2 = DadosSistema.ConfiguracaoDto;
				if (configuracaoDto2 == null)
				{
					return null;
				}
				AuxiliarDto auxiliar = configuracaoDto2.Auxiliar;
				if (auxiliar == null)
				{
					return null;
				}
				TextoDto mensagemWhatsApp = auxiliar.MensagemWhatsApp;
				if (mensagemWhatsApp == null)
				{
					return null;
				}
				return mensagemWhatsApp.Valor;
			}
			MensagemWhatsappModeloDto result = base.Service.Canal<IMensagemWhatsappModeloModuleService, Task<MensagemWhatsappModeloDto>>((Func<IMensagemWhatsappModeloModuleService, Task<MensagemWhatsappModeloDto>>)((IMensagemWhatsappModeloModuleService s) => s.ObterPorReferencia(referencia))).Result;
			if (result != null)
			{
				return result.Mensagem;
			}
			return null;
		}

		private string MontarMensagemWhatsApp(string modelo, ParticipanteCotacaoDto participante, string link)
		{
			string text = HelperString.ToStringEmptyWhenNull(participante.Fornecedor.Nome);
			return (string.IsNullOrWhiteSpace(modelo) ? $"Olá {text}, segue a cotação de compra {_cotacaoDto.Numero} - {_cotacaoDto.Descricao}." : ResolverModeloMensagemWhatsApp(modelo, text, link)).TrimEnd() + Environment.NewLine + Environment.NewLine + MontarInstrucoesWhatsApp(link);
		}

		private string ResolverModeloMensagemWhatsApp(string modelo, string nome, string link)
		{
			IDictionary<string, string> dictionary = MensagemWhatsAppPlaceholder.NovosValores();
			dictionary["Nome"] = nome;
			dictionary["PrimeiroNome"] = MensagemWhatsAppPlaceholder.ObterPrimeiroNome(nome);
			dictionary["Empresa"] = HelperString.ToStringEmptyWhenNull(DadosSistema.EmpresaLogada.Nome);
			dictionary["NumeroCotacao"] = _cotacaoDto.Numero.ToString();
			dictionary["DescricaoCotacao"] = HelperString.ToStringEmptyWhenNull(_cotacaoDto.Descricao);
			dictionary["PrazoResposta"] = _cotacaoDto.DataLimiteResposta.ToString("dd/MM/yyyy");
			dictionary["Link"] = link;
			return HelperString.ToStringEmptyWhenNull(MensagemWhatsAppPlaceholder.Resolver(modelo, dictionary));
		}

		private string MontarInstrucoesWhatsApp(string link)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("*Como responder esta cotação:*");
			stringBuilder.AppendLine($"1. Abra o formulário pelo link {link} (disponível por {7} dias);");
			stringBuilder.AppendLine("2. Preencha os preços e as condições;");
			stringBuilder.AppendLine("3. Clique em *Gerar arquivo de resposta* e devolva o arquivo por aqui — ou use *Copiar resposta* e cole o conteúdo na conversa.");
			stringBuilder.Append($"{Environment.NewLine}Prazo para resposta: {_cotacaoDto.DataLimiteResposta:dd/MM/yyyy}.");
			return stringBuilder.ToString();
		}

		private string AvisoValidadeLink(bool comAnexo)
		{
			DateTime dateTime = DateTime.Now.Date.AddDays(7.0);
			if (_cotacaoDto.DataLimiteResposta.Date <= dateTime)
			{
				return string.Empty;
			}
			string text = (comAnexo ? " Depois disso o fornecedor só conseguirá responder pelo arquivo anexo." : " Depois disso o fornecedor ficará sem acesso ao formulário.");
			return $"\nO prazo de resposta é {_cotacaoDto.DataLimiteResposta:dd/MM/yyyy}, mas o link do formulário expira em {dateTime:dd/MM/yyyy}." + text;
		}

		private bool PodeEnviarAosFornecedoresWhatsApp()
		{
			if (EstaNaSituacao(2))
			{
				return DadosSistema.WhatsAppConfigurado;
			}
			return false;
		}

		private async Task EnviarEmailFormulario(ConfiguracaoEmailDto configuracaoEmail, ParticipanteCotacaoDto participante, string caminhoAnexo, string link)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(configuracaoEmail.ModeloEmail.Corpo);
			stringBuilder.Append("<br/><br/><b>Como responder esta cotação:</b><br/>");
			if (!string.IsNullOrWhiteSpace(link))
			{
				stringBuilder.Append($"1. Abra o formulário pelo link <a href=\"{link}\">{link}</a> (disponível por {7} dias) — ou pelo arquivo anexo;<br/>");
			}
			else
			{
				stringBuilder.Append("1. Abra o arquivo anexo no seu navegador;<br/>");
			}
			stringBuilder.Append("2. Preencha os preços e as condições;<br/>");
			stringBuilder.Append("3. Clique em <b>Gerar arquivo de resposta</b> e devolva o arquivo respondendo este e-mail — ou use <b>Copiar resposta</b> e cole o conteúdo na resposta.");
			EnvioEmailServiceFactory val = new EnvioEmailServiceFactory().SetarServidorSmtp(configuracaoEmail.ServidorSmtp).SetarPorta(configuracaoEmail.PortaEmail).SetarSenha(configuracaoEmail.SenhaEmail)
				.SetarUsaSsl(configuracaoEmail.UsaSsl)
				.SetarIgnorarCertificadoInvalido(configuracaoEmail.IgnorarCertificadoInvalido)
				.SetarEmailRemetente(configuracaoEmail.UsuarioEmail, DadosSistema.EmpresaLogada.Nome)
				.SetarAssunto($"{configuracaoEmail.ModeloEmail.Assunto} - Cotação {_cotacaoDto.Numero}")
				.SetarMensagem(stringBuilder.ToString())
				.AdicionarDestinatarios(participante.Fornecedor.Email, participante.Fornecedor.Nome)
				.AdicionarAnexo(caminhoAnexo, Path.GetFileName(caminhoAnexo));
			if (!string.IsNullOrWhiteSpace(configuracaoEmail.ModeloEmail.EmailParaResposta))
			{
				val.SetarEmailRemetenteParaResposta(configuracaoEmail.ModeloEmail.EmailParaResposta, (string)null);
			}
			if (configuracaoEmail.EnviarCopiaRemetente)
			{
				val.SetarEmailCopiaOculta(configuracaoEmail.UsuarioEmail, (string)null);
			}
			EmailServiceFactory.SetCurrent((IEnvioEmailServiceFactory)(object)val);
			IEnvioEmailService servico = EmailServiceFactory.Create();
			try
			{
				await servico.Enviar();
			}
			finally
			{
				servico.Dispose();
			}
		}

		private static string LimparNomeArquivo(string nome)
		{
			char[] invalidos = Path.GetInvalidFileNameChars();
			string text = new string(nome.Where((char c) => !invalidos.Contains(c) && c != ' ').ToArray());
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			return "Fornecedor";
		}

		private async Task ImportarRespostaArquivo()
		{
			if (Autorizado<ManterCotacao>())
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					DefaultExt = ".json",
					Filter = "Resposta de cotação (*.json;*.txt)|*.json;*.txt|Todos os arquivos (*.*)|*.*"
				};
				if (openFileDialog.ShowDialog() == true)
				{
					await ImportarResposta(File.ReadAllText(openFileDialog.FileName, Encoding.UTF8));
				}
			}
		}

		private async Task ImportarRespostaTexto()
		{
			if (Autorizado<ManterCotacao>())
			{
				string empty = string.Empty;
				if (DialogoFactory.MostrarDialogoEntradaString(ref empty, "Resposta do fornecedor", "Cole aqui o conteúdo que o fornecedor copiou no formulário da cotação.", (IIcone)null, false, false, false, (string)null, false, false) == MessageBoxResult.OK && !string.IsNullOrWhiteSpace(empty))
				{
					await ImportarResposta(empty);
				}
			}
		}

		private async Task ImportarResposta(string conteudo)
		{
			try
			{
				RespostaImportada importada = RespostaCotacaoCodec.Interpretar(conteudo);
				ParticipanteCotacaoDto participante = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(_cotacaoDto.Participantes).FirstOrDefault((ParticipanteCotacaoDto x) => x.TokenAcesso != null && x.TokenAcesso.Valor == importada.Token);
				if (participante == null)
				{
					DialogoFactory.MostrarDialogoConfirma($"Esta resposta não pertence a esta cotação (veio da cotação {importada.NumeroCotacao}, fornecedor {importada.NomeFornecedor}).", false, HorizontalAlignment.Center);
					return;
				}
				string arg = (participante.Respondeu ? "\nEste fornecedor já respondeu — a resposta atual será substituída e a anterior fica no histórico." : string.Empty);
				if (DialogoFactory.MostrarDialogoSimNao($"Importar a resposta de {participante.Fornecedor.Nome}?{arg}", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes && await ExecutarAcao("Importando resposta...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.RegistrarRespostaPorTokenAsync(importada.Token, importada.Resposta)))))
				{
					DialogoFactory.MostrarDialogoConfirma($"Resposta de {participante.Fornecedor.Nome} importada com sucesso.", false, HorizontalAlignment.Center);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeEnviarAosFornecedores()
		{
			return EstaNaSituacao(2);
		}

		private bool PodeImportarResposta()
		{
			if (_cotacaoDto != null)
			{
				return _cotacaoDto.RecebeProposta;
			}
			return false;
		}

		private async Task Abrir()
		{
			if (!Autorizado<ManterCotacao>() || DialogoFactory.MostrarDialogoSimNao("Abrir a cotação? Depois disso os itens e os fornecedores não poderão mais ser alterados.", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes || !(await GravarAlteracoesPendentes()))
			{
				return;
			}
			await ExecutarAcao("Abrindo cotação...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.AbrirAsync(((EntityBaseDto)_cotacaoDto).Id, string.Empty))));
		}

		private async Task<bool> GravarAlteracoesPendentes()
		{
			IFormularioEntity formulario = default(IFormularioEntity);
			ref IFormularioEntity reference = ref formulario;
			IFormulario formulario2 = ((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).Formulario;
			reference = (IFormularioEntity)(object)((formulario2 is IFormularioEntity) ? formulario2 : null);
			if (formulario == null)
			{
				return true;
			}
			return await ExecutarAcao("Gravando a cotação...", () => ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).Persistir(formulario));
		}

		private async Task Reabrir()
		{
			if (!Autorizado<ApurarCotacao>() || DialogoFactory.MostrarDialogoSimNao("Reabrir a cotação? A apuração atual será descartada.", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await ExecutarAcao("Reabrindo cotação...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.ReabrirAsync(((EntityBaseDto)_cotacaoDto).Id, string.Empty))));
		}

		private async Task FecharCotacao()
		{
			if (!Autorizado<GerarPedidoCompraDaCotacao>() || DialogoFactory.MostrarDialogoSimNao("Fechar a cotação com os vencedores apurados, sem gerar pedidos de compra?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await ExecutarAcao("Fechando cotação...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.FecharAsync(((EntityBaseDto)_cotacaoDto).Id, (IEnumerable<PedidoCompraGeradoDto>)null, string.Empty))));
		}

		private async Task CancelarCotacao()
		{
			if (!Autorizado<ManterCotacao>() || DialogoFactory.MostrarDialogoSimNao("Cancelar esta cotação?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			await ExecutarAcao("Cancelando cotação...", () => base.Service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.CancelarAsync(((EntityBaseDto)_cotacaoDto).Id, string.Empty))));
		}

		private async Task<bool> ExecutarAcao(string mensagem, Func<Task<CotacaoDto>> acao)
		{
			try
			{
				LoaderProcessoService.MostrarLoader(mensagem);
				CotacaoDto dto = await acao();
				LoaderProcessoService.EsconderLoader();
				RecarregarFormulario(dto);
				return true;
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
		}

		private void RecarregarFormulario(CotacaoDto dto)
		{
			if (!HelperObject.IsNull((object)dto))
			{
				string titulo = ObterTituloAbaSelecionada();
				_cotacaoDto = dto;
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).DtoAtualizado = dto;
				MarcarRetornoParaLista();
				((FichaBase<CotacaoDto, CotacaoViewModel>)(object)this).CriarFormulario();
				RestaurarAbaSelecionada(titulo);
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoSituacao));
				AtualizarComandos();
			}
		}

		private void MarcarRetornoParaLista()
		{
			((NavigationBase)this).ParametroRetornado -= ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).OnParametroRetornado;
			((NavigationBase)this).ParametroRetornado += ((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).OnParametroRetornado;
		}

		private string ObterTituloAbaSelecionada()
		{
			if (FormularioCotacao == null || !((Formulario)FormularioCotacao).TemAbas)
			{
				return string.Empty;
			}
			int abaSelecionada = ((Formulario)FormularioCotacao).AbaSelecionada;
			if (abaSelecionada < 0 || abaSelecionada >= ((Formulario)FormularioCotacao).Abas.Count)
			{
				return string.Empty;
			}
			return ((Formulario)FormularioCotacao).Abas[abaSelecionada].Titulo;
		}

		private void RestaurarAbaSelecionada(string titulo)
		{
			if (string.IsNullOrEmpty(titulo) || FormularioCotacao == null || !((Formulario)FormularioCotacao).TemAbas)
			{
				return;
			}
			for (int i = 0; i < ((Formulario)FormularioCotacao).Abas.Count; i++)
			{
				if (!(((Formulario)FormularioCotacao).Abas[i].Titulo != titulo))
				{
					((Formulario)FormularioCotacao).AbaSelecionada = i;
					break;
				}
			}
		}

		private void AtualizarComandos()
		{
			ICommand abrirCommand = AbrirCommand;
			((CommandBase<object>)((abrirCommand is AsyncCommand) ? abrirCommand : null))?.RaiseCanExecuteChanged();
			ICommand reabrirCommand = ReabrirCommand;
			((CommandBase<object>)((reabrirCommand is AsyncCommand) ? reabrirCommand : null))?.RaiseCanExecuteChanged();
			ICommand fecharCotacaoCommand = FecharCotacaoCommand;
			((CommandBase<object>)((fecharCotacaoCommand is AsyncCommand) ? fecharCotacaoCommand : null))?.RaiseCanExecuteChanged();
			ICommand cancelarCotacaoCommand = CancelarCotacaoCommand;
			((CommandBase<object>)((cancelarCotacaoCommand is AsyncCommand) ? cancelarCotacaoCommand : null))?.RaiseCanExecuteChanged();
			ICommand gerarPedidosCompraCommand = GerarPedidosCompraCommand;
			((CommandBase<object>)((gerarPedidosCompraCommand is AsyncCommand) ? gerarPedidosCompraCommand : null))?.RaiseCanExecuteChanged();
			ICommand enviarAosFornecedoresCommand = EnviarAosFornecedoresCommand;
			((CommandBase<object>)((enviarAosFornecedoresCommand is AsyncCommand) ? enviarAosFornecedoresCommand : null))?.RaiseCanExecuteChanged();
			ICommand importarRespostaArquivoCommand = ImportarRespostaArquivoCommand;
			((CommandBase<object>)((importarRespostaArquivoCommand is AsyncCommand) ? importarRespostaArquivoCommand : null))?.RaiseCanExecuteChanged();
			ICommand importarRespostaTextoCommand = ImportarRespostaTextoCommand;
			((CommandBase<object>)((importarRespostaTextoCommand is AsyncCommand) ? importarRespostaTextoCommand : null))?.RaiseCanExecuteChanged();
		}

		private static bool Autorizado<TDireito>() where TDireito : class, IDireito, new()
		{
			return DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<Cotacao, TDireito>(0m, (string)null) != null;
		}

		private bool EstaNaSituacao(int codigo)
		{
			if (_cotacaoDto != null && _cotacaoDto.Situacao != null)
			{
				return _cotacaoDto.Situacao.Codigo == codigo;
			}
			return false;
		}

		private bool PodeAbrir()
		{
			return EstaNaSituacao(1);
		}

		private bool PodeReabrir()
		{
			return EstaNaSituacao(3);
		}

		private bool PodeFecharCotacao()
		{
			return EstaNaSituacao(3);
		}

		private bool PodeGerarPedidosCompra()
		{
			if (!EstaNaSituacao(3))
			{
				return EstaNaSituacao(4);
			}
			return true;
		}

		private bool PodeCancelarCotacao()
		{
			if (_cotacaoDto != null)
			{
				return _cotacaoDto.EmAndamento;
			}
			return false;
		}

		public override void Dispose()
		{
			AbrirCommand = null;
			ReabrirCommand = null;
			FecharCotacaoCommand = null;
			CancelarCotacaoCommand = null;
			GerarPedidosCompraCommand = null;
			EnviarAosFornecedoresCommand = null;
			ImportarRespostaArquivoCommand = null;
			ImportarRespostaTextoCommand = null;
			((FichaBasePersistir<CotacaoDto, CotacaoViewModel>)(object)this).Dispose();
		}
	}
	public class CotacaoFormulario : FormularioEntity<CotacaoDto>
	{
		public CotacaoViewModel PrincipalViewModel { get; private set; }

		public ListaItemCotacaoViewModel ItensViewModel { get; private set; }

		public ListaParticipanteCotacaoViewModel ParticipantesViewModel { get; private set; }

		public LancamentoPropostaViewModel PropostasViewModel { get; private set; }

		public PainelApuracaoViewModel ApuracaoViewModel { get; private set; }

		public CotacaoFormulario(CotacaoDto dto)
			: this(dto, null, null, null, null, null)
		{
		}

		public CotacaoFormulario(CotacaoDto dto, Action<string, RespostaCotacaoDto> registrarResposta, Action simularApuracao, Action<IEnumerable<ApuracaoItemDto>> apurar, Action<string, ProdutoServicoEmpresaDto, IEnumerable<GradeItemCotacaoDto>> vincularProduto, Action<ISelecaoCommandParameter> abrirGrade)
			: base(dto)
		{
			PrincipalViewModel = new CotacaoViewModel(base.Dto);
			ItensViewModel = new ListaItemCotacaoViewModel(base.Dto, vincularProduto, abrirGrade);
			ParticipantesViewModel = new ListaParticipanteCotacaoViewModel(base.Dto);
			PropostasViewModel = new LancamentoPropostaViewModel(base.Dto, registrarResposta);
			ApuracaoViewModel = new PainelApuracaoViewModel(base.Dto, simularApuracao, apurar);
			List<IAbaViewModelBase> list = new List<IAbaViewModelBase>
			{
				(IAbaViewModelBase)(object)new CotacaoPrincipalAbaViewModel((IViewModelPrincipal)(object)PrincipalViewModel),
				(IAbaViewModelBase)(object)new CotacaoItensAbaViewModel((IViewModelDtoBase)(object)ItensViewModel),
				(IAbaViewModelBase)(object)new CotacaoFornecedoresAbaViewModel((IViewModelDtoBase)(object)ParticipantesViewModel)
			};
			if (!EmElaboracao(base.Dto))
			{
				list.Add((IAbaViewModelBase)(object)new CotacaoPropostasAbaViewModel((IViewModelDtoBase)(object)PropostasViewModel));
				list.Add((IAbaViewModelBase)(object)new CotacaoApuracaoAbaViewModel((IViewModelDtoBase)(object)ApuracaoViewModel));
			}
			((Formulario)this).AdicionarProjetarAbas(list.ToArray());
		}

		private static bool EmElaboracao(CotacaoDto dto)
		{
			if (dto != null && dto.Situacao != null)
			{
				return dto.Situacao.Codigo == 1;
			}
			return true;
		}
	}
	public class CotacaoListaViewModel : ListaManutencaoBase<CotacaoDto, CotacaoViewModel, Cotacao>
	{
		private readonly IWService _service;

		private static IEnumerable<SituacaoCotacao> SituacoesFiltro => new List<SituacaoCotacao>
		{
			(SituacaoCotacao)new Todas(),
			(SituacaoCotacao)new EmElaboracao(),
			(SituacaoCotacao)new Aberta(),
			(SituacaoCotacao)new Apurada(),
			(SituacaoCotacao)new Fechada(),
			(SituacaoCotacao)new Cancelada()
		};

		public ICommand DuplicarCommand { get; set; }

		public ICommand CancelarCotacaoCommand { get; set; }

		public ICommand GerarPedidosCompraCommand { get; set; }

		public CotacaoListaViewModel(ISessao sessao, IWService service)
			: base(sessao, typeof(CotacaoFichaAdicionarView).Name, typeof(CotacaoFichaAtualizarView).Name)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Expected O, but got Unknown
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Expected O, but got Unknown
			_service = service;
			foreach (SituacaoCotacao item in SituacoesFiltro)
			{
				((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoSituacaoCotacao(service, item));
			}
			DuplicarCommand = (ICommand)new DelegateCommand((Action)Duplicar, (Func<bool>)PodeDuplicar, (bool?)true);
			CancelarCotacaoCommand = (ICommand)new AsyncCommand((Func<Task>)CancelarCotacao, (Func<bool>)PodeCancelarCotacao, false, (bool?)null);
			GerarPedidosCompraCommand = (ICommand)new AsyncCommand((Func<Task>)GerarPedidosCompra, (Func<bool>)PodeGerarPedidosCompra, false, (bool?)null);
		}

		protected override bool PodeAdicionar()
		{
			return ((ListaSelecaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)(object)this).Sessao.VerificarPermissao<Cotacao, ManterCotacao>();
		}

		public override bool PodeEditar()
		{
			if (((ListaSelecaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)(object)this).Sessao.VerificarPermissao<Cotacao, ManterCotacao>() && ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens != null)
			{
				return ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private bool PodeDuplicar()
		{
			if (((ListaSelecaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)(object)this).Sessao.VerificarPermissao<Cotacao, ManterCotacao>() && ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens != null)
			{
				return ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private void Duplicar()
		{
			((ListaManutencaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)this).Adicionar(CopiarItensEFornecedores(((ViewModelDtoBase<CotacaoDto>)(object)((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.Selecionado).Dto));
		}

		private bool PodeGerarPedidosCompra()
		{
			if (((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens == null || !((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return false;
			}
			if (!((ListaSelecaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)(object)this).Sessao.VerificarPermissao<Cotacao, GerarPedidoCompraDaCotacao>())
			{
				return false;
			}
			return GeradorPedidoCompraCotacao.PodeGerar(((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.ObterDtoItemSelecionado());
		}

		private async Task GerarPedidosCompra()
		{
			CotacaoDto val = ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.ObterDtoItemSelecionado();
			if (val == null)
			{
				return;
			}
			CotacaoDto atualizado = default(CotacaoDto);
			_ = atualizado;
			atualizado = await GeradorPedidoCompraCotacao.Gerar(_service, val);
			if (atualizado != null)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.Atualizar(atualizado);
				});
			}
		}

		private bool PodeCancelarCotacao()
		{
			if (((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens == null || !((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return false;
			}
			if (!((ListaSelecaoBase<CotacaoDto, CotacaoViewModel, Cotacao>)(object)this).Sessao.VerificarPermissao<Cotacao, ManterCotacao>())
			{
				return false;
			}
			CotacaoDto val = ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.ObterDtoItemSelecionado();
			if (val != null)
			{
				return val.EmAndamento;
			}
			return false;
		}

		private async Task CancelarCotacao()
		{
			CotacaoDto dto = ((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.ObterDtoItemSelecionado();
			if (dto == null || DialogoFactory.MostrarDialogoSimNao($"Cancelar a cotação nº {dto.Numero}?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				LoaderProcessoService.MostrarLoader("Cancelando cotação...");
				CotacaoDto atualizado = default(CotacaoDto);
				_ = atualizado;
				atualizado = await _service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService x) => x.CancelarAsync(((EntityBaseDto)dto).Id, string.Empty)));
				if (atualizado != null)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<CotacaoDto, CotacaoViewModel>)(object)this).Itens.Atualizar(atualizado);
					});
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			}
		}

		private static CotacaoDto CopiarItensEFornecedores(CotacaoDto origem)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			return new CotacaoDto
			{
				Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(origem.Itens).Select(CopiarItem).ToList(),
				Participantes = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(origem.Participantes).Select(CopiarFornecedor).ToList()
			};
		}

		private static ItemCotacaoDto CopiarItem(ItemCotacaoDto item)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			return new ItemCotacaoDto
			{
				Descricao = item.Descricao,
				Quantidade = item.Quantidade,
				ProdutoServicoEmpresaReferencia = item.ProdutoServicoEmpresaReferencia,
				CodigoInterno = item.CodigoInterno,
				CodigoBarras = item.CodigoBarras,
				Marca = item.Marca,
				UnidadeMedida = item.UnidadeMedida,
				Observacao = item.Observacao,
				IsItemLivre = item.IsItemLivre,
				Grades = HelperIEnumerable.ToListEmptyWhenNull<GradeItemCotacaoDto>(item.Grades).Select(CopiarGrade).ToList()
			};
		}

		private static ParticipanteCotacaoDto CopiarFornecedor(ParticipanteCotacaoDto participante)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			return new ParticipanteCotacaoDto
			{
				Fornecedor = participante.Fornecedor
			};
		}

		private static GradeItemCotacaoDto CopiarGrade(GradeItemCotacaoDto grade)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			return new GradeItemCotacaoDto
			{
				EstoqueReferencia = grade.EstoqueReferencia,
				Linha = grade.Linha,
				Coluna = grade.Coluna,
				Quantidade = grade.Quantidade,
				CodigoBarras = grade.CodigoBarras
			};
		}
	}
	public class CotacaoViewModel : ViewModelAtivoBase<CotacaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
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

		public long Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => Numero), value);
			}
		}

		public DateTime DataHoraEmissao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEmissao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEmissao), value);
			}
		}

		public DateTime DataLimiteResposta
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataLimiteResposta));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataLimiteResposta), value);
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

		public string DescricaoSituacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoSituacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoSituacao), value);
			}
		}

		public string DescricaoSituacaoUpperCase => DescricaoSituacao?.ToUpper();

		public string CorSituacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CorSituacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CorSituacao), value);
			}
		}

		public int QuantidadeRespondidos
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => QuantidadeRespondidos));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => QuantidadeRespondidos), value);
			}
		}

		public bool PrazoEncerrado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PrazoEncerrado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PrazoEncerrado), value);
			}
		}

		public bool EmAndamento => ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.EmAndamento;

		public bool RecebeProposta => ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.RecebeProposta;

		public bool Concluida => ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Concluida;

		public bool PossuiItemLivre => ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.PossuiItemLivre;

		public int QuantidadeItens
		{
			get
			{
				if (((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Itens != null)
				{
					return ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Itens.Count();
				}
				return 0;
			}
		}

		public int QuantidadeFornecedores
		{
			get
			{
				if (((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Participantes != null)
				{
					return ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Participantes.Count();
				}
				return 0;
			}
		}

		public string ResumoRespostas => $"{QuantidadeRespondidos} de {QuantidadeFornecedores} responderam";

		public IEnumerable<PedidoCompraGeradoDto> PedidosCompraGerados => ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.PedidosCompraGerados;

		public bool PossuiPedidosGerados
		{
			get
			{
				if (((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.PedidosCompraGerados != null)
				{
					return ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.PedidosCompraGerados.Any();
				}
				return false;
			}
		}

		public CotacaoViewModel()
		{
		}

		public CotacaoViewModel(CotacaoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			base.Ativo = ((EntityDto)((ViewModelDtoBase<CotacaoDto>)(object)this).Dto).Ativo;
			Numero = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Numero;
			Descricao = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Descricao;
			DataHoraEmissao = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.DataHoraEmissao;
			int valor = DadosSistema.ConfiguracaoDto.Entrada.DiasValidadePadraoCotacao.Valor;
			DataLimiteResposta = ((((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.DataLimiteResposta == default(DateTime)) ? DateTime.Now.AddDays((valor > 0) ? valor : 7) : ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.DataLimiteResposta);
			Observacao = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Observacao;
			DescricaoSituacao = ((((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Situacao == null) ? string.Empty : ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Situacao.Descricao);
			CorSituacao = ((((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Situacao == null) ? "#000000" : ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.Situacao.Cor);
			QuantidadeRespondidos = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.QuantidadeRespondidos;
			PrazoEncerrado = ((ViewModelDtoBase<CotacaoDto>)(object)this).Dto.PrazoEncerrado;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CotacaoDto val = (CotacaoDto)((obj is CotacaoDto) ? obj : null);
			if (val != null)
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.Descricao = Descricao;
				val.DataLimiteResposta = DataLimiteResposta;
				val.Observacao = Observacao;
				((ViewModelDtoBase<CotacaoDto>)(object)this).AtualizarDto<CotacaoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new CotacaoFormulario(((ViewModelDtoBase<CotacaoDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Cotações de compra";
		}

		public string ToTituloFicha()
		{
			return "Cotação de compra";
		}
	}
	public class CotacoesViewModel : ModuloBase
	{
		public CotacoesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloCotacaoRegion, "CotacaoListaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.Cotacoes.ViewModels.Propostas
{
	public class LancamentoPropostaViewModel : ViewModelDtoBase<CotacaoDto>
	{
		private readonly Action<string, RespostaCotacaoDto> _registrarResposta;

		public Lista<ParticipanteCotacaoViewModel> Participantes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ParticipanteCotacaoViewModel>>((Expression<Func<Lista<ParticipanteCotacaoViewModel>>>)(() => Participantes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ParticipanteCotacaoViewModel>>((Expression<Func<Lista<ParticipanteCotacaoViewModel>>>)(() => Participantes), value);
			}
		}

		public Lista<PropostaItemViewModel> Propostas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PropostaItemViewModel>>((Expression<Func<Lista<PropostaItemViewModel>>>)(() => Propostas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PropostaItemViewModel>>((Expression<Func<Lista<PropostaItemViewModel>>>)(() => Propostas), value);
			}
		}

		public ICommand GravarRespostaCommand { get; private set; }

		public int PrazoEntregaDias
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => PrazoEntregaDias));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => PrazoEntregaDias), value);
			}
		}

		public int CondicaoPagamentoDias
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => CondicaoPagamentoDias));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => CondicaoPagamentoDias), value);
			}
		}

		public decimal ValorFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorFrete), value, (Action)AtualizarTotais);
			}
		}

		public decimal ValorOutrosCustos
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorOutrosCustos));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorOutrosCustos), value, (Action)AtualizarTotais);
			}
		}

		public TipoFreteCotacao TipoFrete
		{
			get
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				return ((BindableBase)this).GetProperty<TipoFreteCotacao>((Expression<Func<TipoFreteCotacao>>)(() => TipoFrete));
			}
			set
			{
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				((BindableBase)this).SetProperty<TipoFreteCotacao>((Expression<Func<TipoFreteCotacao>>)(() => TipoFrete), value, (Action)AtualizarTotais);
			}
		}

		public bool PodeInformarValorFrete => (int)TipoFrete > 0;

		public string ObservacaoResposta
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ObservacaoResposta));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ObservacaoResposta), value);
			}
		}

		public Lista<KeyValuePair<string, TipoFreteCotacao>> OpcoesTipoFrete { get; set; }

		public bool RecebeProposta => base.Dto.RecebeProposta;

		public decimal TotalProposta => Propostas.Itens.Where((PropostaItemViewModel x) => x.Disponivel).Sum((PropostaItemViewModel x) => x.Total);

		public decimal TotalComFrete => TotalProposta + (((int)TipoFrete == 1) ? ValorFrete : 0m) + ValorOutrosCustos;

		public int QuantidadeOfertada => Propostas.Itens.Count((PropostaItemViewModel x) => x.Disponivel);

		public string ResumoLancamento
		{
			get
			{
				if (!Participantes.PossuiItemSelecionado)
				{
					return "Selecione um fornecedor para lançar a proposta";
				}
				return $"{QuantidadeOfertada} de {Propostas.Numero} itens ofertados";
			}
		}

		public LancamentoPropostaViewModel()
		{
		}

		public LancamentoPropostaViewModel(CotacaoDto dto, Action<string, RespostaCotacaoDto> registrarResposta)
			: base(dto)
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			_registrarResposta = registrarResposta;
			Participantes = new Lista<ParticipanteCotacaoViewModel>();
			Propostas = new Lista<PropostaItemViewModel>();
			GravarRespostaCommand = (ICommand)new DelegateCommand((Action)GravarResposta, (Func<bool>)PodeGravar, (bool?)null);
			Participantes.AlterouItemSelecionadoEvent += delegate
			{
				CarregarPropostasDoParticipante();
			};
			((ViewModelBase<CotacaoDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Participantes.Limpar();
			Participantes.Adicionar(from x in HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(base.Dto.Participantes)
				select new ParticipanteCotacaoViewModel(x));
			Participantes.PosicionarNoPrimeiro();
			CarregarPropostasDoParticipante();
			OpcoesTipoFrete = new Lista<KeyValuePair<string, TipoFreteCotacao>>();
			OpcoesTipoFrete.Adicionar((IEnumerable<KeyValuePair<string, TipoFreteCotacao>>)HelperEnum.ObterParDeValores<TipoFreteCotacao>());
			OpcoesTipoFrete.AlterouItemSelecionadoEvent += delegate(object s, KeyValuePair<string, TipoFreteCotacao> e)
			{
				//IL_0003: Unknown result type (might be due to invalid IL or missing references)
				TipoFrete = e.Value;
			};
			PosicionarTipoFrete();
		}

		private void CarregarPropostasDoParticipante()
		{
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected O, but got Unknown
			foreach (PropostaItemViewModel iten in Propostas.Itens)
			{
				((BindableBase)iten).PropertyChanged -= PropostaOnPropertyChanged;
			}
			Propostas.Limpar();
			if (!Participantes.PossuiItemSelecionado)
			{
				return;
			}
			RespostaCotacaoDto respostaCorrente = ((ViewModelDtoBase<ParticipanteCotacaoDto>)Participantes.ItemSelecionado).Dto.RespostaCorrente;
			PrazoEntregaDias = respostaCorrente.PrazoEntregaDias;
			CondicaoPagamentoDias = respostaCorrente.CondicaoPagamentoDias;
			ValorFrete = respostaCorrente.ValorFrete;
			ValorOutrosCustos = respostaCorrente.ValorOutrosCustos;
			TipoFrete = respostaCorrente.TipoFrete;
			ObservacaoResposta = respostaCorrente.Observacao;
			PosicionarTipoFrete();
			foreach (ItemCotacaoDto item in HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.Dto.Itens))
			{
				PropostaItemDto val = HelperIEnumerable.ToListEmptyWhenNull<PropostaItemDto>(respostaCorrente.Propostas).FirstOrDefault((PropostaItemDto x) => x.ItemCotacaoReferencia == item.Id);
				if (val == null)
				{
					val = new PropostaItemDto
					{
						ItemCotacaoReferencia = item.Id,
						Disponivel = true
					};
				}
				PropostaItemViewModel propostaItemViewModel = new PropostaItemViewModel(val, item);
				((BindableBase)propostaItemViewModel).PropertyChanged += PropostaOnPropertyChanged;
				Propostas.Adicionar(propostaItemViewModel);
			}
			AtualizarTotais();
		}

		private void AtualizarTotais()
		{
			((BindableBase)this).RaisePropertyChanged<decimal>((Expression<Func<decimal>>)(() => TotalProposta));
			((BindableBase)this).RaisePropertyChanged<decimal>((Expression<Func<decimal>>)(() => TotalComFrete));
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => QuantidadeOfertada));
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ResumoLancamento));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeInformarValorFrete));
			ICommand gravarRespostaCommand = GravarRespostaCommand;
			((CommandBase<object>)((gravarRespostaCommand is DelegateCommand) ? gravarRespostaCommand : null))?.RaiseCanExecuteChanged();
		}

		private void PosicionarTipoFrete()
		{
			if (OpcoesTipoFrete != null)
			{
				OpcoesTipoFrete.Posicionar((Expression<Func<KeyValuePair<string, TipoFreteCotacao>, bool>>)((KeyValuePair<string, TipoFreteCotacao> x) => (int)x.Value == (int)TipoFrete));
			}
		}

		private void PropostaOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!(e.PropertyName != "PrecoUnitario") || !(e.PropertyName != "Disponivel") || !(e.PropertyName != "Total"))
			{
				AtualizarTotais();
			}
		}

		private void GravarResposta()
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			if (Participantes.PossuiItemSelecionado)
			{
				RespostaCotacaoDto arg = new RespostaCotacaoDto
				{
					DataHoraResposta = DateTime.Now,
					PrazoEntregaDias = PrazoEntregaDias,
					CondicaoPagamentoDias = CondicaoPagamentoDias,
					ValorFrete = ValorFrete,
					ValorOutrosCustos = ValorOutrosCustos,
					TipoFrete = TipoFrete,
					Observacao = ObservacaoResposta,
					Propostas = Propostas.Itens.Select((PropostaItemViewModel x) => x.AtualizarDto()).ToList()
				};
				_registrarResposta?.Invoke(Participantes.ItemSelecionado.Referencia, arg);
			}
		}

		private bool PodeGravar()
		{
			if (RecebeProposta && Participantes.PossuiItemSelecionado)
			{
				return Propostas.Itens.Any((PropostaItemViewModel x) => x.Disponivel && x.PrecoUnitario > 0m);
			}
			return false;
		}
	}
	public class PropostaItemViewModel : ViewModelDtoBase<PropostaItemDto>
	{
		public string DescricaoItem { get; private set; }

		public decimal QuantidadeSolicitada { get; private set; }

		public string UnidadeMedida { get; private set; }

		public string MarcaSolicitada { get; private set; }

		public bool Disponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Disponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Disponivel), value, (Action)AoAlterarDisponibilidade);
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
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario), value, (Action)AtualizarTotal);
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

		public decimal MultiploVenda
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => MultiploVenda));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => MultiploVenda), value, (Action)AtualizarTotal);
			}
		}

		public string MarcaOfertada
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MarcaOfertada));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MarcaOfertada), value);
			}
		}

		public int PrazoEntregaDias
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => PrazoEntregaDias));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => PrazoEntregaDias), value);
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

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			private set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value);
			}
		}

		public bool AtendeQuantidade
		{
			get
			{
				if (!(QuantidadeMinima <= 0m))
				{
					return QuantidadeSolicitada >= QuantidadeMinima;
				}
				return true;
			}
		}

		public decimal QuantidadeAjustada
		{
			get
			{
				if (!(MultiploVenda <= 0m))
				{
					return decimal.Ceiling(QuantidadeSolicitada / MultiploVenda) * MultiploVenda;
				}
				return QuantidadeSolicitada;
			}
		}

		public bool AjustaPorMultiplo
		{
			get
			{
				if (Disponivel && MultiploVenda > 0m)
				{
					return QuantidadeAjustada != QuantidadeSolicitada;
				}
				return false;
			}
		}

		public string Alerta
		{
			get
			{
				if (!Disponivel)
				{
					return string.Empty;
				}
				if (!AtendeQuantidade)
				{
					return $"Quantidade mínima de {QuantidadeMinima:N2} acima do solicitado";
				}
				if (AjustaPorMultiplo)
				{
					return $"Múltiplo de {MultiploVenda:N2} — o pedido sairá com {QuantidadeAjustada:N2}";
				}
				return string.Empty;
			}
		}

		public PropostaItemViewModel()
		{
		}

		public PropostaItemViewModel(PropostaItemDto dto, ItemCotacaoDto item)
			: base(dto)
		{
			DescricaoItem = ((item == null) ? string.Empty : item.Descricao);
			QuantidadeSolicitada = ((item == null) ? 0m : item.Quantidade);
			UnidadeMedida = ((item == null) ? string.Empty : item.UnidadeMedida);
			MarcaSolicitada = ((item == null) ? string.Empty : item.Marca);
			((ViewModelBase<PropostaItemDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Disponivel = base.Dto.Disponivel;
			PrecoUnitario = base.Dto.PrecoUnitario;
			QuantidadeMinima = base.Dto.QuantidadeMinima;
			MultiploVenda = base.Dto.MultiploVenda;
			MarcaOfertada = base.Dto.MarcaOfertada;
			PrazoEntregaDias = base.Dto.PrazoEntregaDias;
			Observacao = base.Dto.Observacao;
			AtualizarTotal();
		}

		public PropostaItemDto AtualizarDto()
		{
			base.Dto.Disponivel = Disponivel;
			base.Dto.PrecoUnitario = (Disponivel ? PrecoUnitario : 0m);
			base.Dto.QuantidadeMinima = QuantidadeMinima;
			base.Dto.MultiploVenda = MultiploVenda;
			base.Dto.MarcaOfertada = MarcaOfertada;
			base.Dto.PrazoEntregaDias = PrazoEntregaDias;
			base.Dto.Observacao = Observacao;
			return base.Dto;
		}

		private void AoAlterarDisponibilidade()
		{
			if (!Disponivel)
			{
				PrecoUnitario = 0m;
			}
			AtualizarTotal();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Alerta));
		}

		private void AtualizarTotal()
		{
			Total = (Disponivel ? (PrecoUnitario * QuantidadeSolicitada) : 0m);
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Alerta));
		}
	}
}
namespace Otus.Modulo.Cotacoes.ViewModels.Participantes
{
	public class ListaParticipanteCotacaoViewModel : ViewModelDtoBase<CotacaoDto>
	{
		public Lista<ParticipanteCotacaoViewModel> Participantes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ParticipanteCotacaoViewModel>>((Expression<Func<Lista<ParticipanteCotacaoViewModel>>>)(() => Participantes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ParticipanteCotacaoViewModel>>((Expression<Func<Lista<ParticipanteCotacaoViewModel>>>)(() => Participantes), value);
			}
		}

		public ICommand RemoverParticipanteCommand { get; private set; }

		public string NomePessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoa), value);
			}
		}

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => HelperObject.IsExactlyObject<FornecedorDto>((object)pessoaDto.Fornecedor)), "Selecione um cadastro do tipo 'Fornecedor'!", (TipoPessoaCadastro)1, HorizontalAlignment.Center);

		public bool PodeAlterarParticipantes
		{
			get
			{
				if (base.Dto.EmAndamento && base.Dto.Situacao != null)
				{
					return base.Dto.Situacao.Codigo == 1;
				}
				return false;
			}
		}

		public int Quantidade => Participantes.Numero;

		public int QuantidadeSemEmail => Participantes.Itens.Count((ParticipanteCotacaoViewModel x) => !x.PossuiEmail);

		public string AvisoSemEmail
		{
			get
			{
				if (QuantidadeSemEmail > 0)
				{
					if (QuantidadeSemEmail != 1)
					{
						return $"{QuantidadeSemEmail} fornecedores não possuem e-mail cadastrado";
					}
					return "1 fornecedor não possui e-mail cadastrado";
				}
				return string.Empty;
			}
		}

		public ListaParticipanteCotacaoViewModel()
		{
		}

		public ListaParticipanteCotacaoViewModel(CotacaoDto dto)
			: base(dto)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			Participantes = new Lista<ParticipanteCotacaoViewModel>();
			RemoverParticipanteCommand = (ICommand)new DelegateCommand((Action)RemoverParticipante, (Func<bool>)PodeRemover, (bool?)null);
			((ViewModelBase<CotacaoDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Participantes.Limpar();
			Participantes.Adicionar(from x in HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(base.Dto.Participantes)
				select new ParticipanteCotacaoViewModel(x));
		}

		public IEnumerable<ParticipanteCotacaoDto> ObterParticipantes()
		{
			return Participantes.Itens.Select((ParticipanteCotacaoViewModel x) => ((ViewModelDtoBase<ParticipanteCotacaoDto>)x).Dto).ToList();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CotacaoDto val = (CotacaoDto)((obj is CotacaoDto) ? obj : null);
			if (val != null)
			{
				val.Participantes = ObterParticipantes();
			}
		}

		public void AdicionarFornecedor(PessoaDto pessoaDto)
		{
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Expected O, but got Unknown
			//IL_00db: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)pessoaDto) && !HelperObject.Is<PessoaDtoNull>((object)pessoaDto))
			{
				if (Participantes.Itens.Any((ParticipanteCotacaoViewModel x) => ((ViewModelDtoBase<ParticipanteCotacaoDto>)x).Dto.Fornecedor.PessoaReferencia == ((EntityBaseDto)pessoaDto).Id))
				{
					DialogoFactory.MostrarDialogoConfirma($"O fornecedor {pessoaDto.Nome} já participa desta cotação.", false, HorizontalAlignment.Center);
					return;
				}
				ParticipanteCotacaoDto dto = new ParticipanteCotacaoDto
				{
					Fornecedor = new FornecedorCotacaoDto
					{
						PessoaReferencia = ((EntityBaseDto)pessoaDto).Id,
						Nome = pessoaDto.Nome,
						Documento = pessoaDto.CpfCnpj,
						Email = ObterEmail(pessoaDto),
						Telefone = ObterTelefone(pessoaDto),
						SiglaUf = ObterSiglaUf(pessoaDto)
					}
				};
				Participantes.Adicionar(new ParticipanteCotacaoViewModel(dto));
				NotificarAlteracao();
			}
		}

		private void SelecionouPessoa(PessoaDto pessoaDto)
		{
			AdicionarFornecedor(pessoaDto);
			NomePessoa = string.Empty;
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)1, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void RemoverParticipante()
		{
			if (Participantes.PossuiItemSelecionado)
			{
				Participantes.Remover(Participantes.ItemSelecionado);
				NotificarAlteracao();
			}
		}

		private void NotificarAlteracao()
		{
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => Quantidade));
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => QuantidadeSemEmail));
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => AvisoSemEmail));
		}

		private bool PodeRemover()
		{
			if (PodeAlterarParticipantes)
			{
				return Participantes.PossuiItemSelecionado;
			}
			return false;
		}

		private static string ObterTelefone(PessoaDto pessoaDto)
		{
			string text = ObterNumero(pessoaDto.TelefoneCotacao);
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			string text2 = ObterNumero(pessoaDto.TelefoneWhatsApp);
			if (!string.IsNullOrWhiteSpace(text2))
			{
				return text2;
			}
			return ObterNumero(pessoaDto.TelefonePrincipal);
		}

		private static string ObterNumero(TelefoneDto telefone)
		{
			if (telefone != null && !HelperObject.Is<TelefoneDtoNull>((object)telefone))
			{
				return HelperString.ToStringEmptyWhenNull(telefone.Numero);
			}
			return string.Empty;
		}

		private static string ObterEmail(PessoaDto pessoaDto)
		{
			List<string> list = ObterEnderecos(pessoaDto.EmailsCotacao);
			if (list.Any())
			{
				return string.Join(";", list);
			}
			List<string> list2 = ObterEnderecos(pessoaDto.EmailsPedidoCompra);
			if (list2.Any())
			{
				return string.Join(";", list2);
			}
			if (pessoaDto.EmailPrincipal != null)
			{
				return pessoaDto.EmailPrincipal.Endereco;
			}
			return string.Empty;
		}

		private static List<string> ObterEnderecos(IEnumerable<EnderecoEletronicoDto> enderecos)
		{
			return (from x in HelperIEnumerable.ToListEmptyWhenNull<EnderecoEletronicoDto>(enderecos)
				where !string.IsNullOrWhiteSpace(x.Endereco)
				select x.Endereco).ToList();
		}

		private static string ObterSiglaUf(PessoaDto pessoaDto)
		{
			if (pessoaDto.EnderecoPrincipal == null || pessoaDto.EnderecoPrincipal.Municipio == null)
			{
				return string.Empty;
			}
			UFDto uF = pessoaDto.EnderecoPrincipal.Municipio.UF;
			if (uF != null)
			{
				return uF.Sigla;
			}
			return string.Empty;
		}
	}
	public class ParticipanteCotacaoViewModel : ViewModelDtoBase<ParticipanteCotacaoDto>
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

		public string Documento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Documento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Documento), value);
			}
		}

		public string Email
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Email));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Email), value);
			}
		}

		public string SiglaUf
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => SiglaUf));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => SiglaUf), value);
			}
		}

		public string DescricaoSituacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoSituacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoSituacao), value);
			}
		}

		public DateTime DataHoraEnvio
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEnvio));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEnvio), value);
			}
		}

		public DateTime DataHoraResposta
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraResposta));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraResposta), value);
			}
		}

		public bool Respondeu
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Respondeu));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Respondeu), value);
			}
		}

		public int QuantidadeRespostasSubstituidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => QuantidadeRespostasSubstituidas));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => QuantidadeRespostasSubstituidas), value);
			}
		}

		public bool PossuiEmail => !string.IsNullOrWhiteSpace(Email);

		public string Telefone
		{
			get
			{
				if (base.Dto.Fornecedor != null)
				{
					return base.Dto.Fornecedor.Telefone;
				}
				return string.Empty;
			}
		}

		public bool PossuiTelefone => !string.IsNullOrWhiteSpace(Telefone);

		public bool Enviado => DataHoraEnvio != default(DateTime);

		public bool Recusou => (int)base.Dto.Situacao == 2;

		public string Referencia => base.Dto.Id;

		public string TokenAcesso
		{
			get
			{
				if (base.Dto.TokenAcesso != null)
				{
					return base.Dto.TokenAcesso.Valor;
				}
				return string.Empty;
			}
		}

		public string DescricaoRespostasSubstituidas
		{
			get
			{
				if (QuantidadeRespostasSubstituidas > 0)
				{
					if (QuantidadeRespostasSubstituidas != 1)
					{
						return $"{QuantidadeRespostasSubstituidas} respostas anteriores substituídas";
					}
					return "1 resposta anterior substituída";
				}
				return string.Empty;
			}
		}

		public ParticipanteCotacaoViewModel()
		{
		}

		public ParticipanteCotacaoViewModel(ParticipanteCotacaoDto dto)
			: base(dto)
		{
			((ViewModelBase<ParticipanteCotacaoDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Nome = base.Dto.Fornecedor.Nome;
			Documento = base.Dto.Fornecedor.Documento;
			Email = base.Dto.Fornecedor.Email;
			SiglaUf = base.Dto.Fornecedor.SiglaUf;
			DescricaoSituacao = base.Dto.DescricaoSituacao;
			DataHoraEnvio = base.Dto.DataHoraEnvio;
			DataHoraResposta = base.Dto.DataHoraResposta;
			Respondeu = base.Dto.Respondeu;
			QuantidadeRespostasSubstituidas = base.Dto.QuantidadeRespostasSubstituidas;
		}
	}
}
namespace Otus.Modulo.Cotacoes.ViewModels.Itens
{
	public class ItemCotacaoViewModel : ItemPesquisaComRemover<ItemCotacaoDto>
	{
		public string DescricaoOrigem
		{
			get
			{
				if (!((ItemPesquisa<ItemCotacaoDto>)(object)this).Item.IsItemLivre)
				{
					return "Cadastrado";
				}
				return "Informado";
			}
		}

		public bool PossuiGrade => HelperIEnumerable.ToListEmptyWhenNull<GradeItemCotacaoDto>(((ItemPesquisa<ItemCotacaoDto>)(object)this).Item.Grades).Any();

		public ItemCotacaoViewModel(ItemCotacaoDto item, Action<object> removerCommand)
			: base(item, removerCommand)
		{
		}
	}
	public class NavegacaoGradeItem
	{
		private const string ProdutoServicoListaView = "ProdutoServicoListaView";

		private readonly NavigationBase _ficha;

		private ISelecaoCommandParameter _pendente;

		public NavegacaoGradeItem(NavigationBase ficha)
		{
			_ficha = ficha;
		}

		public void Abrir(ISelecaoCommandParameter commandParameter)
		{
			if (commandParameter != null && _ficha != null && _ficha.NavigationService != null)
			{
				_pendente = commandParameter;
				if (NavegandoNaListaDeProdutos())
				{
					_ficha.NavigationService.Navigated += AoTerminarNavegacao;
				}
				else
				{
					NavegarParaGrade();
				}
			}
		}

		private bool NavegandoNaListaDeProdutos()
		{
			IRegionNavigationJournalEntry currentEntry = _ficha.NavigationService.Journal.CurrentEntry;
			if (currentEntry != null)
			{
				return currentEntry.Uri.ToString() == "ProdutoServicoListaView";
			}
			return false;
		}

		private void AoTerminarNavegacao(object sender, RegionNavigationEventArgs e)
		{
			if (!(e.Uri.ToString() == "ProdutoServicoListaView"))
			{
				_ficha.NavigationService.Navigated -= AoTerminarNavegacao;
				NavegarParaGrade();
			}
		}

		private void NavegarParaGrade()
		{
			ISelecaoCommandParameter pendente = _pendente;
			_pendente = null;
			if (pendente != null)
			{
				_ficha.Navegar(pendente.View, (Action<NavigationResult>)delegate
				{
				}, pendente.Parametros);
			}
		}
	}
	public class ListaItemCotacaoNaFichaViewModel : ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>
	{
		public ListaItemCotacaoNaFichaViewModel(IEnumerable<ItemCotacaoDto> itens)
			: base(itens)
		{
		}

		protected override ItemCotacaoViewModel CriarItem(ItemCotacaoDto item)
		{
			return new ItemCotacaoViewModel(item, (Action<object>)base.RemoverItemListaPesquisa);
		}

		public override void Pesquisar(string informacao)
		{
			if (string.IsNullOrEmpty(informacao))
			{
				base.AdicionarListaPesquisa(((IEnumerable<ItemCotacaoDto>)((Lista<ItemCotacaoDto>)(object)this).Itens).Select((Func<ItemCotacaoDto, ItemCotacaoViewModel>)((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)this).CriarItem));
				return;
			}
			string termo = HelperString.RemoveDiacritics(informacao).ToLower();
			IEnumerable<ItemCotacaoViewModel> enumerable = ((Lista<ItemCotacaoDto>)(object)this).Itens.Where((ItemCotacaoDto i) => Normalizar(i.CodigoInterno).Equals(termo)).Select((Func<ItemCotacaoDto, ItemCotacaoViewModel>)((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)this).CriarItem);
			if (enumerable.Any())
			{
				base.AdicionarListaPesquisa(enumerable);
				return;
			}
			IEnumerable<ItemCotacaoViewModel> enumerable2 = ((Lista<ItemCotacaoDto>)(object)this).Itens.Where((ItemCotacaoDto i) => Normalizar(i.CodigoBarras).Equals(termo)).Select((Func<ItemCotacaoDto, ItemCotacaoViewModel>)((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)this).CriarItem);
			if (enumerable2.Any())
			{
				base.AdicionarListaPesquisa(enumerable2);
				return;
			}
			base.AdicionarListaPesquisa(((Lista<ItemCotacaoDto>)(object)this).Itens.Where((ItemCotacaoDto i) => Normalizar(i.Descricao).Contains(termo) || Normalizar(i.Marca).Contains(termo)).Select((Func<ItemCotacaoDto, ItemCotacaoViewModel>)((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)this).CriarItem));
		}

		public void RecarregarPesquisa()
		{
			base.ListaPesquisa.Limpar();
			((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)this).Pesquisar(string.Empty);
		}

		private static string Normalizar(string valor)
		{
			if (!string.IsNullOrEmpty(valor))
			{
				return HelperString.RemoveDiacritics(valor).ToLower();
			}
			return string.Empty;
		}
	}
	public class ListaItemCotacaoViewModel : ViewModelDtoBase<CotacaoDto>
	{
		private Action<string, ProdutoServicoEmpresaDto, IEnumerable<GradeItemCotacaoDto>> _vincularProduto;

		private TipoEstoqueBaseViewModel _tipoEstoqueVinculo;

		private Action<ISelecaoCommandParameter> _abrirGrade;

		private EstoqueGradeDto _estoqueGradeProduto;

		private string _produtoReferencia;

		private string _codigoInternoProduto;

		private string _descricaoProduto;

		public ListaItemCotacaoNaFichaViewModel Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<ListaItemCotacaoNaFichaViewModel>((Expression<Func<ListaItemCotacaoNaFichaViewModel>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<ListaItemCotacaoNaFichaViewModel>((Expression<Func<ListaItemCotacaoNaFichaViewModel>>)(() => Itens), value);
			}
		}

		public ICommand AdicionarItemCommand { get; private set; }

		public TipoEstoqueBaseViewModel TipoEstoque
		{
			get
			{
				return ((BindableBase)this).GetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque));
			}
			set
			{
				((BindableBase)this).SetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque), value);
			}
		}

		public bool PossuiGrade => TipoEstoque != null;

		public ISelecaoCommandParameter VincularProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)VincularProdutoSelecionado, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa)), "Selecione um cadastro do tipo 'Produto'.", HorizontalAlignment.Center));

		public bool PodeVincularProduto
		{
			get
			{
				if (_vincularProduto != null && base.Dto.EmAndamento && ItemSelecionado != null)
				{
					return ((ItemPesquisa<ItemCotacaoDto>)(object)ItemSelecionado).Item.IsItemLivre;
				}
				return false;
			}
		}

		public ISelecaoCommandParameter ProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)SelecionouProduto, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa)), "Selecione um cadastro do tipo 'Produto'.", HorizontalAlignment.Center));

		public string NovaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NovaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NovaDescricao), value);
				DesvincularQuandoDescricaoAlterada(value);
			}
		}

		public decimal NovaQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => NovaQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => NovaQuantidade), value);
			}
		}

		public string NovaMarca
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NovaMarca));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NovaMarca), value);
			}
		}

		public string NovaUnidadeMedida
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NovaUnidadeMedida));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NovaUnidadeMedida), value);
			}
		}

		public string NovoCodigoBarras
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NovoCodigoBarras));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NovoCodigoBarras), value);
			}
		}

		public bool FocoNaDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaDescricao), value);
			}
		}

		public bool ItemVinculadoAoCadastro => !string.IsNullOrEmpty(_produtoReferencia);

		public string DescricaoVinculo
		{
			get
			{
				if (!ItemVinculadoAoCadastro)
				{
					return string.Empty;
				}
				if (!string.IsNullOrWhiteSpace(_codigoInternoProduto))
				{
					return $"Vinculado ao produto {_codigoInternoProduto} do cadastro.";
				}
				return "Vinculado a um produto do cadastro.";
			}
		}

		public bool PodeAlterarItens
		{
			get
			{
				if (base.Dto.EmAndamento && base.Dto.Situacao != null)
				{
					return base.Dto.Situacao.Codigo == 1;
				}
				return false;
			}
		}

		public int Quantidade
		{
			get
			{
				if (Itens != null)
				{
					return ((Lista<ItemCotacaoDto>)(object)Itens).Numero;
				}
				return 0;
			}
		}

		private ItemCotacaoViewModel ItemSelecionado
		{
			get
			{
				if (Itens != null && ((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)Itens).ListaPesquisa.PossuiItemSelecionado)
				{
					return ((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)Itens).ListaPesquisa.ItemSelecionado;
				}
				return null;
			}
		}

		public ListaItemCotacaoViewModel()
		{
		}

		public ListaItemCotacaoViewModel(CotacaoDto dto)
			: this(dto, null)
		{
		}

		public ListaItemCotacaoViewModel(CotacaoDto dto, Action<string, ProdutoServicoEmpresaDto, IEnumerable<GradeItemCotacaoDto>> vincularProduto)
			: this(dto, vincularProduto, null)
		{
		}

		public ListaItemCotacaoViewModel(CotacaoDto dto, Action<string, ProdutoServicoEmpresaDto, IEnumerable<GradeItemCotacaoDto>> vincularProduto, Action<ISelecaoCommandParameter> abrirGrade)
			: base(dto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			_abrirGrade = abrirGrade;
			AdicionarItemCommand = (ICommand)new DelegateCommand((Action)AdicionarItem, (Func<bool>)PodeAlterar, (bool?)null);
			_vincularProduto = vincularProduto;
			((ViewModelBase<CotacaoDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Itens = new ListaItemCotacaoNaFichaViewModel(HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.Dto.Itens));
			((Lista<ItemCotacaoDto>)(object)Itens).RemoveuEvent += delegate
			{
				((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => Quantidade));
			};
			((ListaItensNaFichaBase<ItemCotacaoViewModel, ItemCotacaoDto>)Itens).ListaPesquisa.AlterouItemSelecionadoEvent += delegate
			{
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeVincularProduto));
			};
			LimparCamposDeInclusao();
		}

		public IEnumerable<ItemCotacaoDto> ObterItens()
		{
			return ((Lista<ItemCotacaoDto>)(object)Itens).Itens.ToList();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CotacaoDto val = (CotacaoDto)((obj is CotacaoDto) ? obj : null);
			if (val != null)
			{
				val.Itens = ObterItens();
			}
		}

		private void VincularProdutoSelecionado(ProdutoServicoEmpresaDto produtoDto)
		{
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			if (HelperObject.IsNull((object)produtoDto) || ItemSelecionado == null)
			{
				return;
			}
			string itemReferencia = ((ItemPesquisa<ItemCotacaoDto>)(object)ItemSelecionado).Item.Id;
			ProdutoEmpresaDto produtoEmpresa = produtoDto.ProdutoEmpresa;
			if (produtoEmpresa == null || HelperObject.IsNot<EstoqueGradeDto>((object)produtoEmpresa.Estoque))
			{
				_vincularProduto?.Invoke(itemReferencia, produtoDto, new List<GradeItemCotacaoDto>());
				return;
			}
			LimparTipoEstoqueVinculo();
			_tipoEstoqueVinculo = ((TipoEstoqueViewModel<SerialEntradaViewModel, GradePrincipalEntradaViewModel, LoteEntradaViewModel>)new TipoEstoqueEntradaViewModel((EstoqueDto)null, produtoEmpresa.Estoque)).ObterTipoEstoque().ViewModelBase;
			((ViewModelBase<EstoqueDto>)(object)_tipoEstoqueVinculo).ProjectedAsViewModel();
			_tipoEstoqueVinculo.ActionSetarQuantidade((Action)delegate
			{
				ConcluirVinculoComGrade(itemReferencia, produtoDto);
			});
			_abrirGrade?.Invoke(_tipoEstoqueVinculo.EstoqueCommandParameter);
		}

		private void ConcluirVinculoComGrade(string itemReferencia, ProdutoServicoEmpresaDto produtoDto)
		{
			List<GradeItemCotacaoDto> list = ObterGrades((EstoqueGradeDto)((_tipoEstoqueVinculo == null) ? null : /*isinst with value type is only supported in some contexts*/));
			LimparTipoEstoqueVinculo();
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Informe a quantidade de ao menos uma grade do produto para vincular.", false, HorizontalAlignment.Center);
			}
			else
			{
				_vincularProduto?.Invoke(itemReferencia, produtoDto, list);
			}
		}

		private void LimparTipoEstoqueVinculo()
		{
			TipoEstoqueBaseViewModel tipoEstoqueVinculo = _tipoEstoqueVinculo;
			if (tipoEstoqueVinculo != null)
			{
				((ViewModelValidationBase)tipoEstoqueVinculo).Dispose();
			}
			_tipoEstoqueVinculo = null;
		}

		private void SelecionouProduto(ProdutoServicoEmpresaDto produtoDto)
		{
			if (!HelperObject.IsNull((object)produtoDto))
			{
				ProdutoServicoDto produtoServico = produtoDto.ProdutoServico;
				ProdutoDto val = ((produtoServico == null) ? null : produtoServico.Produto);
				_produtoReferencia = ((EntityBaseDto)produtoDto).Id;
				_codigoInternoProduto = ((produtoServico == null) ? string.Empty : produtoServico.CodigoInterno);
				_descricaoProduto = ((produtoServico == null) ? string.Empty : produtoServico.Descricao);
				NovaDescricao = _descricaoProduto;
				NovaMarca = ((val == null || val.Marca == null) ? string.Empty : val.Marca.Descricao);
				NovaUnidadeMedida = ((produtoServico == null || produtoServico.UnidadeMedida == null) ? string.Empty : produtoServico.UnidadeMedida.Sigla);
				NovoCodigoBarras = ((val == null) ? string.Empty : val.CodigoBarras);
				CriarTipoEstoqueGrade(produtoDto);
				if (!PossuiGrade && NovaQuantidade <= 0m)
				{
					NovaQuantidade = 1m;
				}
				NotificarVinculo();
			}
		}

		private void DesvincularQuandoDescricaoAlterada(string descricao)
		{
			if (ItemVinculadoAoCadastro && !string.Equals(descricao, _descricaoProduto, StringComparison.CurrentCultureIgnoreCase))
			{
				LimparVinculoProduto();
			}
		}

		private void LimparVinculoProduto()
		{
			_produtoReferencia = string.Empty;
			_codigoInternoProduto = string.Empty;
			_descricaoProduto = string.Empty;
			LimparTipoEstoque();
			NotificarVinculo();
		}

		private void NotificarVinculo()
		{
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ItemVinculadoAoCadastro));
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoVinculo));
		}

		private void AdicionarItem()
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			if (string.IsNullOrWhiteSpace(NovaDescricao))
			{
				return;
			}
			if (PossuiGrade && !ObterGradesNovoItem().Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Informe a quantidade de ao menos uma grade do produto.", false, HorizontalAlignment.Center);
				return;
			}
			bool itemVinculadoAoCadastro = ItemVinculadoAoCadastro;
			if (itemVinculadoAoCadastro && ((Lista<ItemCotacaoDto>)(object)Itens).Itens.Any((ItemCotacaoDto x) => x.ProdutoServicoEmpresaReferencia == _produtoReferencia))
			{
				DialogoFactory.MostrarDialogoConfirma($"O produto {NovaDescricao} já está nesta cotação.", false, HorizontalAlignment.Center);
				return;
			}
			ItemCotacaoDto val = new ItemCotacaoDto
			{
				ProdutoServicoEmpresaReferencia = (itemVinculadoAoCadastro ? _produtoReferencia : string.Empty),
				CodigoInterno = (itemVinculadoAoCadastro ? _codigoInternoProduto : string.Empty),
				Descricao = NovaDescricao,
				Quantidade = ((NovaQuantidade <= 0m) ? 1m : NovaQuantidade),
				Marca = NovaMarca,
				UnidadeMedida = NovaUnidadeMedida,
				CodigoBarras = NovoCodigoBarras,
				IsItemLivre = !itemVinculadoAoCadastro,
				Grades = ObterGradesNovoItem()
			};
			((Lista<ItemCotacaoDto>)(object)Itens).Adicionar(val);
			LimparCamposDeInclusao();
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => Quantidade));
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoNaDescricao = !FocoNaDescricao;
			});
		}

		private void CriarTipoEstoqueGrade(ProdutoServicoEmpresaDto produtoDto)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			LimparTipoEstoque();
			ProdutoEmpresaDto val = ((produtoDto == null) ? null : produtoDto.ProdutoEmpresa);
			if (val != null && !HelperObject.IsNot<EstoqueGradeDto>((object)val.Estoque))
			{
				ref EstoqueGradeDto estoqueGradeProduto = ref _estoqueGradeProduto;
				EstoqueDto estoque = val.Estoque;
				estoqueGradeProduto = (EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null);
				TipoEstoque = ((TipoEstoqueViewModel<SerialEntradaViewModel, GradePrincipalEntradaViewModel, LoteEntradaViewModel>)new TipoEstoqueEntradaViewModel((EstoqueDto)null, val.Estoque)).ObterTipoEstoque().ViewModelBase;
				((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
				TipoEstoque.ActionSetarQuantidade((Action)AtualizarQuantidadePelaGrade);
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PossuiGrade));
				_abrirGrade?.Invoke(TipoEstoque.EstoqueCommandParameter);
			}
		}

		private void AtualizarQuantidadePelaGrade()
		{
			if (TipoEstoque != null)
			{
				NovaQuantidade = TipoEstoque.Quantidade;
				RestaurarCelulasZeradas();
			}
		}

		private void LimparTipoEstoque()
		{
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				((ViewModelValidationBase)tipoEstoque).Dispose();
			}
			TipoEstoque = null;
			_estoqueGradeProduto = null;
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PossuiGrade));
		}

		public override void Dispose()
		{
			LimparTipoEstoque();
			LimparTipoEstoqueVinculo();
			((ViewModelValidationBase)this).Dispose();
		}

		private void RestaurarCelulasZeradas()
		{
			EstoqueGradeDto val = (EstoqueGradeDto)((TipoEstoque == null) ? null : /*isinst with value type is only supported in some contexts*/);
			if (val == null || _estoqueGradeProduto == null)
			{
				return;
			}
			List<GradeDto> informadas = HelperIEnumerable.ToListEmptyWhenNull<GradeDto>(val.Grades);
			List<GradeDto> list = (from celula in HelperIEnumerable.ToListEmptyWhenNull<GradeDto>(_estoqueGradeProduto.Grades)
				where celula.Estoque != null && informadas.All((GradeDto x) => x.Estoque == null || ((EntityBaseDto)x.Estoque).Id != ((EntityBaseDto)celula.Estoque).Id)
				select celula).Select((Func<GradeDto, GradeDto>)((GradeDto celula) => new GradeDto
			{
				CodigoBarras = celula.CodigoBarras,
				Linha = celula.Linha,
				Coluna = celula.Coluna,
				Estoque = new EstoqueDto
				{
					Id = ((EntityBaseDto)celula.Estoque).Id,
					Quantidade = 0m
				}
			})).ToList();
			if (list.Any())
			{
				val.Grades = informadas.Concat(list).ToList();
			}
		}

		private List<GradeItemCotacaoDto> ObterGradesNovoItem()
		{
			return ObterGrades((EstoqueGradeDto)((TipoEstoque == null) ? null : /*isinst with value type is only supported in some contexts*/));
		}

		private static List<GradeItemCotacaoDto> ObterGrades(EstoqueGradeDto estoque)
		{
			if (estoque == null)
			{
				return new List<GradeItemCotacaoDto>();
			}
			return (from x in HelperIEnumerable.ToListEmptyWhenNull<GradeDto>(estoque.Grades)
				where x.Estoque != null && x.Estoque.Quantidade > 0m
				select x).Select((Func<GradeDto, GradeItemCotacaoDto>)((GradeDto x) => new GradeItemCotacaoDto
			{
				EstoqueReferencia = ((EntityBaseDto)x.Estoque).Id,
				Linha = ((x.Linha == null) ? string.Empty : x.Linha.Descricao),
				Coluna = ((x.Coluna == null) ? string.Empty : x.Coluna.Descricao),
				CodigoBarras = x.CodigoBarras,
				Quantidade = x.Estoque.Quantidade
			})).ToList();
		}

		private void LimparCamposDeInclusao()
		{
			LimparVinculoProduto();
			NovaDescricao = string.Empty;
			NovaMarca = string.Empty;
			NovaUnidadeMedida = string.Empty;
			NovoCodigoBarras = string.Empty;
			NovaQuantidade = 1m;
		}

		private bool PodeAlterar()
		{
			return PodeAlterarItens;
		}
	}
}
namespace Otus.Modulo.Cotacoes.ViewModels.Apuracao
{
	public class ApuracaoItemViewModel : ViewModelDtoBase<ApuracaoItemDto>
	{
		public string DescricaoItem { get; private set; }

		public decimal Quantidade { get; private set; }

		public string UnidadeMedida { get; private set; }

		public Lista<ConcorrenteApuracaoViewModel> Concorrentes { get; private set; }

		public string NomeVencedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeVencedor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeVencedor), value);
			}
		}

		public decimal CustoComparavel
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => CustoComparavel));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => CustoComparavel), value);
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

		public bool DecisaoManual
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => DecisaoManual));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => DecisaoManual), value);
			}
		}

		public string JustificativaDecisao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => JustificativaDecisao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => JustificativaDecisao), value);
			}
		}

		public bool PossuiVencedor => base.Dto.PossuiVencedor;

		public decimal TotalDoItem => CustoComparavel * Quantidade;

		public string MemoriaCalculo
		{
			get
			{
				if (!HelperObject.IsNull((object)base.Dto.Parcelas))
				{
					return string.Join("  +  ", from x in base.Dto.Parcelas
						where x.Valor != 0m
						select $"{x.Descricao}: {x.Valor:N4}");
				}
				return string.Empty;
			}
		}

		public string MotivoEscolha
		{
			get
			{
				if (!DecisaoManual)
				{
					return HelperString.ToStringEmptyWhenNull(base.Dto.MotivoEscolha);
				}
				return $"Escolha manual do comprador. {JustificativaDecisao}".Trim();
			}
		}

		public ApuracaoItemViewModel()
		{
		}

		public ApuracaoItemViewModel(ApuracaoItemDto dto, ItemCotacaoDto item, IEnumerable<ParticipanteCotacaoDto> participantes)
			: base(dto)
		{
			DescricaoItem = ((item == null) ? string.Empty : item.Descricao);
			Quantidade = ((item == null) ? 0m : item.Quantidade);
			UnidadeMedida = ((item == null) ? string.Empty : item.UnidadeMedida);
			Concorrentes = new Lista<ConcorrenteApuracaoViewModel>();
			MontarConcorrentes(item, HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(participantes));
			((ViewModelBase<ApuracaoItemDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			CustoComparavel = base.Dto.CustoComparavel;
			PrecoUnitario = base.Dto.PrecoUnitario;
			DecisaoManual = base.Dto.DecisaoManual;
			JustificativaDecisao = base.Dto.JustificativaDecisao;
			NomeVencedor = ObterNomeConcorrente(base.Dto.ParticipanteVencedorReferencia);
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => MotivoEscolha));
		}

		public void DefinirVencedor(ConcorrenteApuracaoViewModel concorrente, string justificativa)
		{
			if (concorrente != null)
			{
				base.Dto.ParticipanteVencedorReferencia = concorrente.ParticipanteReferencia;
				base.Dto.PrecoUnitario = concorrente.PrecoUnitario;
				base.Dto.CustoComparavel = concorrente.PrecoUnitario;
				base.Dto.DecisaoManual = true;
				base.Dto.JustificativaDecisao = justificativa;
				base.Dto.PossuiVencedor = true;
				((ViewModelBase<ApuracaoItemDto>)(object)this).ProjectedAsViewModel();
			}
		}

		public ApuracaoItemDto AtualizarDto()
		{
			base.Dto.DecisaoManual = DecisaoManual;
			base.Dto.JustificativaDecisao = JustificativaDecisao;
			return base.Dto;
		}

		private void MontarConcorrentes(ItemCotacaoDto item, IEnumerable<ParticipanteCotacaoDto> participantes)
		{
			if (item == null)
			{
				return;
			}
			foreach (ParticipanteCotacaoDto item2 in participantes.Where((ParticipanteCotacaoDto x) => x.Respondeu && x.PossuiResposta))
			{
				PropostaItemDto val = item2.RespostaCorrente.Propostas.FirstOrDefault((PropostaItemDto x) => x.ItemCotacaoReferencia == item.Id);
				if (val != null && val.Disponivel)
				{
					Concorrentes.Adicionar(new ConcorrenteApuracaoViewModel(item2, val));
				}
			}
		}

		private string ObterNomeConcorrente(string participanteReferencia)
		{
			ConcorrenteApuracaoViewModel concorrenteApuracaoViewModel = Concorrentes.Itens.FirstOrDefault((ConcorrenteApuracaoViewModel x) => x.ParticipanteReferencia == participanteReferencia);
			if (concorrenteApuracaoViewModel != null)
			{
				return concorrenteApuracaoViewModel.Nome;
			}
			return string.Empty;
		}
	}
	public class ConcorrenteApuracaoViewModel
	{
		public string ParticipanteReferencia { get; private set; }

		public string Nome { get; private set; }

		public decimal PrecoUnitario { get; private set; }

		public string MarcaOfertada { get; private set; }

		public int PrazoEntregaDias { get; private set; }

		public string ToText => $"{Nome} - {PrecoUnitario:C}";

		public ConcorrenteApuracaoViewModel(ParticipanteCotacaoDto participante, PropostaItemDto proposta)
		{
			ParticipanteReferencia = participante.Id;
			Nome = participante.Fornecedor.Nome;
			PrecoUnitario = proposta.PrecoUnitario;
			MarcaOfertada = proposta.MarcaOfertada;
			PrazoEntregaDias = ((proposta.PrazoEntregaDias > 0) ? proposta.PrazoEntregaDias : participante.RespostaCorrente.PrazoEntregaDias);
		}
	}
	public class PainelApuracaoViewModel : ViewModelDtoBase<CotacaoDto>
	{
		private readonly Action _simular;

		private readonly Action<IEnumerable<ApuracaoItemDto>> _apurar;

		public Lista<ApuracaoItemViewModel> Apuracoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ApuracaoItemViewModel>>((Expression<Func<Lista<ApuracaoItemViewModel>>>)(() => Apuracoes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ApuracaoItemViewModel>>((Expression<Func<Lista<ApuracaoItemViewModel>>>)(() => Apuracoes), value);
			}
		}

		public ICommand SimularCommand { get; private set; }

		public ICommand ApurarCommand { get; private set; }

		public ICommand DefinirVencedorCommand { get; private set; }

		public ICommand ImprimirMapaCommand { get; private set; }

		public ICommand ImprimirPorFornecedorCommand { get; private set; }

		public ConcorrenteApuracaoViewModel ConcorrenteSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<ConcorrenteApuracaoViewModel>((Expression<Func<ConcorrenteApuracaoViewModel>>)(() => ConcorrenteSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<ConcorrenteApuracaoViewModel>((Expression<Func<ConcorrenteApuracaoViewModel>>)(() => ConcorrenteSelecionado), value);
			}
		}

		public string JustificativaDecisao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => JustificativaDecisao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => JustificativaDecisao), value);
			}
		}

		public int QuantidadeComVencedor => Apuracoes.Itens.Count((ApuracaoItemViewModel x) => x.PossuiVencedor);

		public int QuantidadeSemVencedor => Apuracoes.Itens.Count((ApuracaoItemViewModel x) => !x.PossuiVencedor);

		public decimal TotalApurado => Apuracoes.Itens.Where((ApuracaoItemViewModel x) => x.PossuiVencedor).Sum((ApuracaoItemViewModel x) => x.TotalDoItem);

		public string ResumoApuracao
		{
			get
			{
				if (Apuracoes.Itens.Any())
				{
					return $"{QuantidadeComVencedor} de {Apuracoes.Numero} itens com fornecedor definido";
				}
				return "Nenhum item apurado. Use 'Simular apuração' para calcular.";
			}
		}

		public string AvisoSemVencedor
		{
			get
			{
				if (QuantidadeSemVencedor > 0)
				{
					if (QuantidadeSemVencedor != 1)
					{
						return $"{QuantidadeSemVencedor} itens não receberam nenhuma proposta válida";
					}
					return "1 item não recebeu nenhuma proposta válida";
				}
				return string.Empty;
			}
		}

		public PainelApuracaoViewModel()
		{
		}

		public PainelApuracaoViewModel(CotacaoDto dto, Action simular, Action<IEnumerable<ApuracaoItemDto>> apurar)
			: base(dto)
		{
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Expected O, but got Unknown
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Expected O, but got Unknown
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Expected O, but got Unknown
			_simular = simular;
			_apurar = apurar;
			Apuracoes = new Lista<ApuracaoItemViewModel>();
			SimularCommand = (ICommand)new DelegateCommand((Action)SimularApuracao, (Func<bool>)PodeSimular, (bool?)null);
			ApurarCommand = (ICommand)new DelegateCommand((Action)Apurar, (Func<bool>)PodeApurar, (bool?)null);
			DefinirVencedorCommand = (ICommand)new DelegateCommand((Action)DefinirVencedor, (Func<bool>)PodeDefinirVencedor, (bool?)null);
			ImprimirMapaCommand = (ICommand)new DelegateCommand((Action)ImprimirMapa, (Func<bool>)PodeImprimirMapa, (bool?)null);
			ImprimirPorFornecedorCommand = (ICommand)new DelegateCommand((Action)ImprimirPorFornecedor, (Func<bool>)PodeImprimirPorFornecedor, (bool?)null);
			((ViewModelBase<CotacaoDto>)(object)this).ProjectedAsViewModel();
		}

		public override void ProjectedAsViewModel()
		{
			Apuracoes.Limpar();
			List<ItemCotacaoDto> source = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.Dto.Itens);
			List<ParticipanteCotacaoDto> participantes = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(base.Dto.Participantes);
			foreach (ApuracaoItemDto apuracao in HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(base.Dto.Apuracoes))
			{
				ItemCotacaoDto item = source.FirstOrDefault((ItemCotacaoDto x) => x.Id == apuracao.ItemCotacaoReferencia);
				Apuracoes.Adicionar(new ApuracaoItemViewModel(apuracao, item, participantes));
			}
			NotificarTotais();
		}

		public void CarregarApuracoes(IEnumerable<ApuracaoItemDto> apuracoesDto)
		{
			Apuracoes.Limpar();
			List<ItemCotacaoDto> source = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.Dto.Itens);
			List<ParticipanteCotacaoDto> participantes = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(base.Dto.Participantes);
			foreach (ApuracaoItemDto apuracao in HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(apuracoesDto))
			{
				ItemCotacaoDto item = source.FirstOrDefault((ItemCotacaoDto x) => x.Id == apuracao.ItemCotacaoReferencia);
				Apuracoes.Adicionar(new ApuracaoItemViewModel(apuracao, item, participantes));
			}
			NotificarTotais();
		}

		public IEnumerable<ApuracaoItemDto> ObterApuracoes()
		{
			return Apuracoes.Itens.Select((ApuracaoItemViewModel x) => x.AtualizarDto()).ToList();
		}

		private void SimularApuracao()
		{
			if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<Cotacao, ApurarCotacao>(0m, (string)null) != null)
			{
				_simular?.Invoke();
			}
		}

		private void Apurar()
		{
			if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<Cotacao, ApurarCotacao>(0m, (string)null) != null)
			{
				_apurar?.Invoke(ObterApuracoes());
			}
		}

		private void DefinirVencedor()
		{
			if (Apuracoes.PossuiItemSelecionado && ConcorrenteSelecionado != null)
			{
				Apuracoes.ItemSelecionado.DefinirVencedor(ConcorrenteSelecionado, JustificativaDecisao);
				JustificativaDecisao = string.Empty;
				ConcorrenteSelecionado = null;
				NotificarTotais();
			}
		}

		private void NotificarTotais()
		{
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => QuantidadeComVencedor));
			((BindableBase)this).RaisePropertyChanged<int>((Expression<Func<int>>)(() => QuantidadeSemVencedor));
			((BindableBase)this).RaisePropertyChanged<decimal>((Expression<Func<decimal>>)(() => TotalApurado));
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ResumoApuracao));
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => AvisoSemVencedor));
		}

		private void ImprimirMapa()
		{
			try
			{
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoMapaComparativoCotacao(base.Dto, ObterApuracoes())).Processar());
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeImprimirMapa()
		{
			return base.Dto.QuantidadeRespondidos > 0;
		}

		private void ImprimirPorFornecedor()
		{
			try
			{
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoCotacaoPorFornecedor(base.Dto, ObterApuracoes())).Processar());
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeImprimirPorFornecedor()
		{
			return Apuracoes.Itens.Any((ApuracaoItemViewModel x) => x.PossuiVencedor);
		}

		private bool PodeSimular()
		{
			return base.Dto.QuantidadeRespondidos > 0;
		}

		private bool PodeApurar()
		{
			if (base.Dto.RecebeProposta)
			{
				return Apuracoes.Itens.Any((ApuracaoItemViewModel x) => x.PossuiVencedor);
			}
			return false;
		}

		private bool PodeDefinirVencedor()
		{
			if (Apuracoes.PossuiItemSelecionado && ConcorrenteSelecionado != null)
			{
				return !string.IsNullOrWhiteSpace(JustificativaDecisao);
			}
			return false;
		}
	}
}
namespace Otus.Modulo.Cotacoes.ViewModels.Abas
{
	public class CotacaoApuracaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CotacaoApuracaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("APURAÇÃO", "Apontar vencedor", viewModel)
		{
		}
	}
	public class CotacaoFornecedoresAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CotacaoFornecedoresAbaViewModel(IViewModelDtoBase viewModel)
			: base("FORNECEDORES", "Fornecedores envolvidos", viewModel)
		{
		}
	}
	public class CotacaoItensAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CotacaoItensAbaViewModel(IViewModelDtoBase viewModel)
			: base("ITENS", "Produtos cotados", viewModel)
		{
		}
	}
	public class CotacaoPrincipalAbaViewModel : AbaPrincipalViewModelBase
	{
		public CotacaoPrincipalAbaViewModel(IViewModelPrincipal viewModel)
			: base("DADOS PRINCIPAIS", "Descrição, situação...", viewModel)
		{
		}
	}
	public class CotacaoPropostasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CotacaoPropostasAbaViewModel(IViewModelDtoBase viewModel)
			: base("PROPOSTAS", "Retorno dos fornecedores", viewModel)
		{
		}
	}
}
namespace Otus.Modulo.Cotacoes.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Cotacoes.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.Cotacoes.Reports
{
	public abstract class ImpressoCotacaoBase : Relatorio
	{
		protected static readonly Color CorVencedor = Color.FromArgb(215, 240, 215);

		protected static readonly Color CorCabecalho = Color.FromArgb(230, 230, 230);

		protected CotacaoDto CotacaoDto { get; private set; }

		protected List<ApuracaoItemDto> Apuracoes { get; private set; }

		public override bool ExibirNoModuloRelatorios => false;

		protected abstract string Titulo { get; }

		protected ImpressoCotacaoBase(CotacaoDto cotacaoDto, IEnumerable<ApuracaoItemDto> apuracoesAtuais)
		{
			CotacaoDto = cotacaoDto;
			List<ApuracaoItemDto> list = HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(apuracoesAtuais);
			Apuracoes = (list.Any() ? list : HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(cotacaoDto.Apuracoes));
		}

		protected abstract void MontarConteudo(XRTable tabela, float larguraUtil);

		public override XtraReport Processar()
		{
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Expected O, but got Unknown
			((XtraReport)this).Margins = new Margins(50, 50, 50, 50);
			int num = ((XtraReport)this).PageWidth - ((XtraReport)this).Margins.Left - ((XtraReport)this).Margins.Right;
			ReportHeaderBand val = new ReportHeaderBand
			{
				HeightF = 0f
			};
			((XtraReportBase)this).Bands.Add((Band)(object)val);
			MontarCabecalho(val, num);
			DetailBand val2 = new DetailBand
			{
				HeightF = 0f
			};
			((XtraReportBase)this).Bands.Add((Band)(object)val2);
			XRTable val3 = new XRTable
			{
				WidthF = num,
				LocationF = PointFloat.op_Implicit(new PointFloat(0f, 0f)),
				Font = new Font("Segoe UI", 8.25f),
				Borders = (BorderSide)15,
				BorderColor = Color.Gainsboro,
				BorderWidth = 1f
			};
			val3.BeginInit();
			MontarConteudo(val3, num);
			val3.EndInit();
			((XRControl)val2).Controls.Add((XRControl)(object)val3);
			BottomMarginBand val4 = new BottomMarginBand
			{
				HeightF = 40f
			};
			((XtraReportBase)this).Bands.Add((Band)(object)val4);
			MontarRodape(val4, num);
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void MontarCabecalho(ReportHeaderBand banda, float larguraUtil)
		{
			Pessoa empresaLogada = DadosSistema.EmpresaLogada;
			float num = 0f;
			XRLabel val = CriarLabel(Titulo, 0f, num, larguraUtil, 28f, 14f, negrito: true);
			((XRControl)val).TextAlignment = (TextAlignment)32;
			((XRControl)banda).Controls.Add((XRControl)(object)val);
			num += 30f;
			((XRControl)banda).Controls.Add((XRControl)(object)CriarLabel($"{empresaLogada.Nome} - {empresaLogada.CpfCnpjFormatado}", 0f, num, larguraUtil - 200f, 18f, 9f, negrito: true));
			XRLabel val2 = CriarLabel($"Emitido em {DateTime.Now:dd/MM/yyyy HH:mm}", larguraUtil - 200f, num, 200f, 18f, 8.25f, negrito: false);
			((XRControl)val2).TextAlignment = (TextAlignment)64;
			((XRControl)banda).Controls.Add((XRControl)(object)val2);
			num += 20f;
			((XRControl)banda).Controls.Add((XRControl)(object)CriarLabel($"Cotação {CotacaoDto.Numero} - {CotacaoDto.Descricao}    |    Emissão: {CotacaoDto.DataHoraEmissao:dd/MM/yyyy}    |    Prazo de resposta: {CotacaoDto.DataLimiteResposta:dd/MM/yyyy}    |    Situação: {CotacaoDto.Situacao.Descricao}", 0f, num, larguraUtil, 18f, 9f, negrito: false));
			num += 26f;
			((XRControl)banda).HeightF = num;
		}

		private void MontarRodape(BottomMarginBand banda, float larguraUtil)
		{
			XRLabel val = CriarLabel("Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br", 0f, 4f, larguraUtil, 16f, 6.75f, negrito: false);
			((XRControl)val).TextAlignment = (TextAlignment)32;
			((XRControl)banda).Controls.Add((XRControl)(object)val);
		}

		protected static XRLabel CriarLabel(string texto, float x, float y, float largura, float altura, float tamanhoFonte, bool negrito)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			return new XRLabel
			{
				Text = texto,
				LocationF = PointFloat.op_Implicit(new PointFloat(x, y)),
				SizeF = new SizeF(largura, altura),
				Font = new Font("Segoe UI", tamanhoFonte, negrito ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular),
				WordWrap = false
			};
		}

		protected static XRTableRow CriarLinha(XRTable tabela)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			XRTableRow val = new XRTableRow
			{
				HeightF = 20f
			};
			tabela.Rows.Add(val);
			return val;
		}

		protected static XRTableCell CriarCelula(XRTableRow linha, string texto, float largura, bool negrito = false, TextAlignment alinhamento = 16, Color? fundo = null)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			XRTableCell val = new XRTableCell
			{
				Text = texto,
				WidthF = largura,
				TextAlignment = alinhamento,
				Padding = new PaddingInfo(3, 3, 0, 0)
			};
			if (negrito)
			{
				((XRControl)val).Font = new Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
			}
			if (fundo.HasValue)
			{
				((XRControl)val).BackColor = fundo.Value;
			}
			linha.Cells.Add(val);
			return val;
		}

		protected ParticipanteCotacaoDto ObterParticipante(string referencia)
		{
			return HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(CotacaoDto.Participantes).FirstOrDefault((ParticipanteCotacaoDto x) => x.Id == referencia);
		}

		protected PropostaItemDto ObterProposta(ParticipanteCotacaoDto participante, string itemReferencia)
		{
			if (participante == null || !participante.PossuiResposta)
			{
				return null;
			}
			return HelperIEnumerable.ToListEmptyWhenNull<PropostaItemDto>(participante.RespostaCorrente.Propostas).FirstOrDefault((PropostaItemDto x) => x.ItemCotacaoReferencia == itemReferencia);
		}

		protected ApuracaoItemDto ObterApuracao(string itemReferencia)
		{
			return Apuracoes.FirstOrDefault((ApuracaoItemDto x) => x.ItemCotacaoReferencia == itemReferencia);
		}
	}
	public class ImpressoMapaComparativoCotacao : ImpressoCotacaoBase
	{
		private const float LarguraColunaItem = 220f;

		private const float LarguraColunaQuantidade = 60f;

		private const float LarguraColunaMotivo = 190f;

		public override string Descricao => "Mapa comparativo da cotação";

		protected override string Titulo => "Mapa comparativo de cotação de compra";

		public ImpressoMapaComparativoCotacao(CotacaoDto cotacaoDto, IEnumerable<ApuracaoItemDto> apuracoesAtuais = null)
			: base(cotacaoDto, apuracoesAtuais)
		{
			((XtraReport)this).Landscape = true;
		}

		protected override void MontarConteudo(XRTable tabela, float larguraUtil)
		{
			List<ParticipanteCotacaoDto> list = (from x in HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(base.CotacaoDto.Participantes)
				where x.Respondeu
				select x).ToList();
			List<ItemCotacaoDto> list2 = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.CotacaoDto.Itens);
			if (!list.Any() || !list2.Any())
			{
				ImpressoCotacaoBase.CriarCelula(ImpressoCotacaoBase.CriarLinha(tabela), "Nenhum fornecedor respondeu esta cotação.", larguraUtil, negrito: false, (TextAlignment)16);
				return;
			}
			float num = (larguraUtil - 220f - 60f - 190f) / (float)list.Count;
			XRTableRow linha = ImpressoCotacaoBase.CriarLinha(tabela);
			ImpressoCotacaoBase.CriarCelula(linha, "Item", 220f, negrito: true, (TextAlignment)16, ImpressoCotacaoBase.CorCabecalho);
			ImpressoCotacaoBase.CriarCelula(linha, "Qtde", 60f, negrito: true, (TextAlignment)32, ImpressoCotacaoBase.CorCabecalho);
			foreach (ParticipanteCotacaoDto item in list)
			{
				ImpressoCotacaoBase.CriarCelula(linha, item.Fornecedor.Nome, num, negrito: true, (TextAlignment)32, ImpressoCotacaoBase.CorCabecalho);
			}
			ImpressoCotacaoBase.CriarCelula(linha, "Por que venceu", 190f, negrito: true, (TextAlignment)16, ImpressoCotacaoBase.CorCabecalho);
			foreach (ItemCotacaoDto item2 in list2)
			{
				XRTableRow linha2 = ImpressoCotacaoBase.CriarLinha(tabela);
				ImpressoCotacaoBase.CriarCelula(linha2, item2.Descricao, 220f, negrito: false, (TextAlignment)16);
				ImpressoCotacaoBase.CriarCelula(linha2, $"{item2.Quantidade:N2}", 60f, negrito: false, (TextAlignment)32);
				ApuracaoItemDto val = ObterApuracao(item2.Id);
				foreach (ParticipanteCotacaoDto item3 in list)
				{
					PropostaItemDto val2 = ObterProposta(item3, item2.Id);
					bool flag = val != null && val.PossuiVencedor && val.ParticipanteVencedorReferencia == item3.Id;
					string text;
					if (val2 == null)
					{
						text = "—";
					}
					else if (!val2.Disponivel)
					{
						text = "Indisponível";
					}
					else
					{
						text = $"{val2.PrecoUnitario:C}";
						if (!string.IsNullOrWhiteSpace(val2.MarcaOfertada) && !val2.MarcaOfertada.Equals(item2.Marca, StringComparison.OrdinalIgnoreCase))
						{
							text += $" ({val2.MarcaOfertada})";
						}
					}
					ImpressoCotacaoBase.CriarCelula(linha2, text, num, flag, (TextAlignment)32, flag ? new Color?(ImpressoCotacaoBase.CorVencedor) : null);
				}
				XRTableCell obj = ImpressoCotacaoBase.CriarCelula(linha2, ObterMotivoEscolha(val), 190f, negrito: false, (TextAlignment)16);
				((XRControl)obj).WordWrap = true;
				((XRControl)obj).CanGrow = true;
			}
			MontarLinhaResumo(tabela, list, num, "Prazo de entrega", (ParticipanteCotacaoDto p) => (p.PossuiResposta && p.RespostaCorrente.PrazoEntregaDias > 0) ? $"{p.RespostaCorrente.PrazoEntregaDias} dia(s)" : "—");
			MontarLinhaResumo(tabela, list, num, "Prazo de pagamento", (ParticipanteCotacaoDto p) => (p.PossuiResposta && p.RespostaCorrente.CondicaoPagamentoDias > 0) ? $"{p.RespostaCorrente.CondicaoPagamentoDias} dia(s)" : "À vista");
			MontarLinhaResumo(tabela, list, num, "Frete", (ParticipanteCotacaoDto p) => (p.PossuiResposta && !(p.RespostaCorrente.ValorFrete <= 0m)) ? $"{p.RespostaCorrente.ValorFrete:C} ({HelperEnum.RetornarDescricao((Enum)(object)p.RespostaCorrente.TipoFrete)})" : "Incluso");
			MontarLinhaResumo(tabela, list, num, "Outros custos", (ParticipanteCotacaoDto p) => (p.PossuiResposta && !(p.RespostaCorrente.ValorOutrosCustos <= 0m)) ? $"{p.RespostaCorrente.ValorOutrosCustos:C}" : "-");
			MontarLinhaResumo(tabela, list, num, "Total ofertado", (ParticipanteCotacaoDto p) => $"{ObterTotalOfertado(p):C}", negrito: true);
			MontarLinhaResumo(tabela, list, num, "Itens vencidos", (ParticipanteCotacaoDto p) => base.Apuracoes.Count((ApuracaoItemDto x) => x.PossuiVencedor && x.ParticipanteVencedorReferencia == p.Id).ToString(), negrito: true);
			if (base.Apuracoes.Any((ApuracaoItemDto x) => x.PossuiVencedor))
			{
				decimal num2 = base.Apuracoes.Where((ApuracaoItemDto x) => x.PossuiVencedor).Sum((ApuracaoItemDto x) => x.PrecoUnitario * ObterQuantidadeItem(x.ItemCotacaoReferencia));
				XRTableRow linha3 = ImpressoCotacaoBase.CriarLinha(tabela);
				ImpressoCotacaoBase.CriarCelula(linha3, "Total da compra com os vencedores (sem rateios)", 280f, negrito: true, (TextAlignment)16);
				ImpressoCotacaoBase.CriarCelula(linha3, $"{num2:C}", num * (float)list.Count, negrito: true, (TextAlignment)64, ImpressoCotacaoBase.CorVencedor);
				ImpressoCotacaoBase.CriarCelula(linha3, string.Empty, 190f, negrito: false, (TextAlignment)16);
			}
		}

		private void MontarLinhaResumo(XRTable tabela, List<ParticipanteCotacaoDto> participantes, float larguraFornecedor, string rotulo, Func<ParticipanteCotacaoDto, string> valor, bool negrito = false)
		{
			XRTableRow linha = ImpressoCotacaoBase.CriarLinha(tabela);
			ImpressoCotacaoBase.CriarCelula(linha, rotulo, 280f, negrito: true, (TextAlignment)16, ImpressoCotacaoBase.CorCabecalho);
			foreach (ParticipanteCotacaoDto participante in participantes)
			{
				ImpressoCotacaoBase.CriarCelula(linha, valor(participante), larguraFornecedor, negrito, (TextAlignment)32);
			}
			ImpressoCotacaoBase.CriarCelula(linha, string.Empty, 190f, negrito: false, (TextAlignment)16);
		}

		private static string ObterMotivoEscolha(ApuracaoItemDto apuracao)
		{
			if (apuracao == null || !apuracao.PossuiVencedor)
			{
				return "—";
			}
			if (!apuracao.DecisaoManual)
			{
				return HelperString.ToStringEmptyWhenNull(apuracao.MotivoEscolha);
			}
			return $"Escolha manual do comprador. {apuracao.JustificativaDecisao}".Trim();
		}

		private decimal ObterTotalOfertado(ParticipanteCotacaoDto participante)
		{
			if (!participante.PossuiResposta)
			{
				return 0m;
			}
			return (from x in HelperIEnumerable.ToListEmptyWhenNull<PropostaItemDto>(participante.RespostaCorrente.Propostas)
				where x.Disponivel
				select x).Sum((PropostaItemDto x) => x.PrecoUnitario * ObterQuantidadeItem(x.ItemCotacaoReferencia));
		}

		private decimal ObterQuantidadeItem(string itemReferencia)
		{
			ItemCotacaoDto val = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.CotacaoDto.Itens).FirstOrDefault((ItemCotacaoDto x) => x.Id == itemReferencia);
			if (val != null)
			{
				return val.Quantidade;
			}
			return 0m;
		}
	}
	public class ImpressoCotacaoPorFornecedor : ImpressoCotacaoBase
	{
		public override string Descricao => "Cotação por fornecedor vencedor";

		protected override string Titulo => "Cotação de compra - itens por fornecedor vencedor";

		public ImpressoCotacaoPorFornecedor(CotacaoDto cotacaoDto, IEnumerable<ApuracaoItemDto> apuracoesAtuais = null)
			: base(cotacaoDto, apuracoesAtuais)
		{
		}

		protected override void MontarConteudo(XRTable tabela, float larguraUtil)
		{
			//IL_055a: Unknown result type (might be due to invalid IL or missing references)
			var list = (from x in base.Apuracoes
				where x.PossuiVencedor
				group x by x.ParticipanteVencedorReferencia into g
				select new
				{
					Participante = ObterParticipante(g.Key),
					Apuracoes = g.ToList()
				} into g
				where g.Participante != null
				orderby g.Participante.Fornecedor.Nome
				select g).ToList();
			if (!list.Any())
			{
				ImpressoCotacaoBase.CriarCelula(ImpressoCotacaoBase.CriarLinha(tabela), "Nenhum item da cotação possui fornecedor vencedor. Faça a apuração antes de imprimir.", larguraUtil, negrito: false, (TextAlignment)16);
				return;
			}
			float num = larguraUtil - 90f - 110f - 110f;
			List<PedidoCompraGeradoDto> source = HelperIEnumerable.ToListEmptyWhenNull<PedidoCompraGeradoDto>(base.CotacaoDto.PedidosCompraGerados);
			foreach (var item in list)
			{
				ParticipanteCotacaoDto participante = item.Participante;
				FornecedorCotacaoDto fornecedor = participante.Fornecedor;
				XRTableRow linha = ImpressoCotacaoBase.CriarLinha(tabela);
				string text = fornecedor.Nome;
				if (!string.IsNullOrWhiteSpace(fornecedor.Documento))
				{
					text += $"   |   {Formatacoes.FormatarCpfCnpj(fornecedor.Documento)}";
				}
				if (!string.IsNullOrWhiteSpace(fornecedor.Telefone))
				{
					text += $"   |   {FormatarTelefone(fornecedor.Telefone)}";
				}
				if (!string.IsNullOrWhiteSpace(fornecedor.Email))
				{
					text += $"   |   {fornecedor.Email}";
				}
				ImpressoCotacaoBase.CriarCelula(linha, text, larguraUtil, negrito: true, (TextAlignment)16, ImpressoCotacaoBase.CorCabecalho);
				XRTableRow linha2 = ImpressoCotacaoBase.CriarLinha(tabela);
				ImpressoCotacaoBase.CriarCelula(linha2, "Item", num, negrito: true, (TextAlignment)16);
				ImpressoCotacaoBase.CriarCelula(linha2, "Qtde", 90f, negrito: true, (TextAlignment)32);
				ImpressoCotacaoBase.CriarCelula(linha2, "Preço unitário", 110f, negrito: true, (TextAlignment)64);
				ImpressoCotacaoBase.CriarCelula(linha2, "Total", 110f, negrito: true, (TextAlignment)64);
				decimal num2 = default(decimal);
				foreach (ApuracaoItemDto apuracao in item.Apuracoes)
				{
					ItemCotacaoDto val = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(base.CotacaoDto.Itens).FirstOrDefault((ItemCotacaoDto x) => x.Id == apuracao.ItemCotacaoReferencia);
					if (val != null)
					{
						PropostaItemDto val2 = ObterProposta(participante, val.Id);
						decimal num3 = val.Quantidade;
						if (val2 != null && val2.MultiploVenda > 0m)
						{
							num3 = decimal.Ceiling(num3 / val2.MultiploVenda) * val2.MultiploVenda;
						}
						decimal num4 = apuracao.PrecoUnitario * num3;
						num2 += num4;
						string text2 = val.Descricao;
						if (val2 != null && !string.IsNullOrWhiteSpace(val2.MarcaOfertada) && !val2.MarcaOfertada.Equals(val.Marca, StringComparison.OrdinalIgnoreCase))
						{
							text2 += $" (marca ofertada: {val2.MarcaOfertada})";
						}
						if (num3 != val.Quantidade)
						{
							string arg = $"{val.Quantidade:N2} {val.UnidadeMedida}".Trim();
							text2 += $" (múltiplo de {val2.MultiploVenda:N2} — cotado {arg})";
						}
						if (apuracao.DecisaoManual)
						{
							text2 += "  *decisão manual";
						}
						XRTableRow linha3 = ImpressoCotacaoBase.CriarLinha(tabela);
						XRTableCell obj = ImpressoCotacaoBase.CriarCelula(linha3, text2, num, negrito: false, (TextAlignment)16);
						((XRControl)obj).WordWrap = true;
						((XRControl)obj).CanGrow = true;
						ImpressoCotacaoBase.CriarCelula(linha3, $"{num3:N2} {val.UnidadeMedida}".Trim(), 90f, negrito: false, (TextAlignment)32);
						ImpressoCotacaoBase.CriarCelula(linha3, $"{apuracao.PrecoUnitario:C}", 110f, negrito: false, (TextAlignment)64);
						ImpressoCotacaoBase.CriarCelula(linha3, $"{num4:C}", 110f, negrito: false, (TextAlignment)64);
					}
				}
				RespostaCotacaoDto respostaCorrente = participante.RespostaCorrente;
				List<string> list2 = new List<string>();
				if (participante.PossuiResposta)
				{
					if (respostaCorrente.PrazoEntregaDias > 0)
					{
						list2.Add($"Entrega em {respostaCorrente.PrazoEntregaDias} dia(s)");
					}
					list2.Add((respostaCorrente.CondicaoPagamentoDias > 0) ? $"Pagamento em {respostaCorrente.CondicaoPagamentoDias} dia(s)" : "Pagamento à vista");
					list2.Add((respostaCorrente.ValorFrete > 0m) ? $"Frete {respostaCorrente.ValorFrete:C} ({HelperEnum.RetornarDescricao((Enum)(object)respostaCorrente.TipoFrete)})" : "Frete incluso");
					if (respostaCorrente.ValorOutrosCustos > 0m)
					{
						list2.Add($"Outros custos {respostaCorrente.ValorOutrosCustos:C}");
					}
				}
				PedidoCompraGeradoDto val3 = source.FirstOrDefault((PedidoCompraGeradoDto x) => x.ParticipanteReferencia == participante.Id);
				if (val3 != null)
				{
					list2.Add($"Pedido de compra gerado: {val3.Numero}");
				}
				XRTableRow linha4 = ImpressoCotacaoBase.CriarLinha(tabela);
				ImpressoCotacaoBase.CriarCelula(linha4, string.Join("   |   ", list2), num + 90f, negrito: false, (TextAlignment)16);
				ImpressoCotacaoBase.CriarCelula(linha4, "Total do fornecedor", 110f, negrito: true, (TextAlignment)64);
				ImpressoCotacaoBase.CriarCelula(linha4, $"{num2:C}", 110f, negrito: true, (TextAlignment)64, ImpressoCotacaoBase.CorVencedor);
				XRTableRow obj2 = ImpressoCotacaoBase.CriarLinha(tabela);
				((XRControl)obj2).HeightF = 12f;
				((XRControl)ImpressoCotacaoBase.CriarCelula(obj2, string.Empty, larguraUtil, negrito: false, (TextAlignment)16)).Borders = (BorderSide)0;
			}
		}

		private static string FormatarTelefone(string telefone)
		{
			string text = new string(telefone.Where(char.IsDigit).ToArray());
			if (text.Length == 11)
			{
				return $"({text.Substring(0, 2)}) {text.Substring(2, 5)}-{text.Substring(7)}";
			}
			if (text.Length == 10)
			{
				return $"({text.Substring(0, 2)}) {text.Substring(2, 4)}-{text.Substring(6)}";
			}
			if (text.Length == 9)
			{
				return $"{text.Substring(0, 5)}-{text.Substring(5)}";
			}
			if (text.Length == 8)
			{
				return $"{text.Substring(0, 4)}-{text.Substring(4)}";
			}
			return telefone;
		}
	}
}
namespace Otus.Modulo.Cotacoes.Services
{
	public static class GeradorFormularioCotacaoHtml
	{
		private const string Template = "<!DOCTYPE html>\r\n<html lang=\"pt-BR\">\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n<meta name=\"theme-color\" content=\"#14477d\">\r\n<title>Cotação de compra</title>\r\n<style>\r\n  * { box-sizing: border-box; }\r\n  body { font-family: 'Segoe UI', Arial, sans-serif; margin: 0; background: #f2f4f7; color: #222; -webkit-text-size-adjust: 100%; }\r\n  .topo { background: #14477d; color: #fff; padding: 14px 16px; }\r\n  .topo h1 { margin: 0; font-size: 18px; line-height: 1.3; }\r\n  .topo p { margin: 6px 0 0 0; font-size: 13px; opacity: .92; line-height: 1.5; }\r\n  .topo .prazo { display: inline-block; background: rgba(255,255,255,.16); border-radius: 12px; padding: 1px 9px; margin-top: 2px; }\r\n  .conteudo { max-width: 1140px; margin: 14px auto; padding: 0 12px; }\r\n  .cartao { background: #fff; border: 1px solid #dde3ea; border-radius: 6px; padding: 14px; margin-bottom: 14px; }\r\n\r\n  table { border-collapse: collapse; width: 100%; font-size: 13px; }\r\n  th, td { border: 1px solid #dde3ea; padding: 7px 8px; text-align: left; vertical-align: middle; }\r\n  th { background: #eef2f7; font-weight: 600; position: sticky; top: 0; z-index: 1; }\r\n  tbody tr:nth-child(even) td { background: #fbfcfd; }\r\n  td input[type=text] { width: 100%; border: 1px solid #c6ced8; border-radius: 3px; padding: 6px; font-size: 13px; font-family: inherit; }\r\n  td input[type=text]:focus { outline: 0; border-color: #14477d; box-shadow: 0 0 0 2px rgba(20,71,125,.15); }\r\n  td input.invalido { border-color: #d33; background: #fff3f3; }\r\n  .num { text-align: right; }\r\n  .num input[type=text] { text-align: right; }\r\n  .centro { text-align: center; }\r\n  .centro input[type=checkbox] { width: 20px; height: 20px; cursor: pointer; margin: 0; }\r\n  .titulo { font-weight: 600; }\r\n  .sub { display: block; font-size: 11px; font-weight: 400; color: #6b7683; margin-top: 2px; }\r\n  .sub.alerta { color: #b35c00; font-weight: 600; }\r\n  .grade { display: block; font-size: 11px; font-weight: 400; color: #14477d; margin-top: 3px; line-height: 1.5; }\r\n\r\n  .rodape-campos { display: flex; flex-wrap: wrap; gap: 14px; }\r\n  .campo { display: flex; flex-direction: column; font-size: 13px; flex: 1 1 190px; }\r\n  .campo label { font-weight: 600; margin-bottom: 4px; }\r\n  .campo input, .campo select, .campo textarea { border: 1px solid #c6ced8; border-radius: 3px; padding: 7px; font-size: 13px; font-family: inherit; width: 100%; }\r\n  .campo textarea { resize: vertical; }\r\n\r\n  .acoes { display: flex; gap: 10px; align-items: center; flex-wrap: wrap; }\r\n  button { background: #14477d; color: #fff; border: 0; border-radius: 4px; padding: 11px 18px; font-size: 14px; font-family: inherit; cursor: pointer; }\r\n  button:active { opacity: .85; }\r\n  button.secundario { background: #5a6b7d; }\r\n  .aviso { font-size: 13px; color: #555; margin: 0; line-height: 1.55; }\r\n  #mensagem { font-size: 13px; font-weight: 600; }\r\n  #mensagem.ok { color: #1a7a2e; }\r\n  #mensagem.erro { color: #c22; }\r\n  tr.indisponivel td { background: #f7f8f9; color: #9aa4ae; }\r\n  tr.indisponivel .titulo { text-decoration: line-through; }\r\n\r\n  .ajuda { display: inline-flex; align-items: center; justify-content: center; width: 15px; height: 15px;\r\n           border-radius: 50%; background: #8b98a7; color: #fff; font-size: 10px; font-weight: 700;\r\n           cursor: help; position: relative; margin-left: 4px; vertical-align: middle; }\r\n  .ajuda:focus { outline: 2px solid #14477d; outline-offset: 1px; }\r\n  .ajuda::after { content: attr(data-dica); position: absolute; left: 50%; top: calc(100% + 7px);\r\n                  transform: translateX(-50%); width: 230px; background: #2b3947; color: #fff;\r\n                  font-size: 12px; font-weight: 400; line-height: 1.45; text-align: left; padding: 8px 10px;\r\n                  border-radius: 5px; box-shadow: 0 3px 10px rgba(0,0,0,.22); z-index: 5; display: none; }\r\n  .ajuda:hover::after, .ajuda:focus::after, .ajuda.aberta::after { display: block; }\r\n  .ajuda-celula { display: none; }\r\n\r\n  @media (max-width: 860px) {\r\n    .conteudo { margin: 10px auto; padding: 0 8px; }\r\n    .cartao { padding: 10px; }\r\n    table, tbody, tr, td { display: block; width: 100%; }\r\n    thead { display: none; }\r\n    tr { border: 1px solid #dde3ea; border-radius: 6px; margin-bottom: 12px; overflow: hidden; }\r\n    tbody tr:nth-child(even) td { background: transparent; }\r\n    td { border: 0; border-bottom: 1px solid #f0f3f6; display: flex; justify-content: space-between;\r\n         align-items: center; gap: 12px; padding: 9px 10px; text-align: left; min-height: 42px; }\r\n    td:last-child { border-bottom: 0; }\r\n    td::before { content: attr(data-rotulo); font-weight: 600; color: #55606d; flex: 0 0 44%; font-size: 12px; }\r\n    td.titulo { display: block; background: #eef2f7; border-bottom: 1px solid #dde3ea; font-size: 14px; }\r\n    td.titulo::before { content: none; }\r\n    td.num, td.centro { text-align: left; }\r\n    .ajuda-celula { display: inline-flex; }\r\n    .ajuda::after { width: 200px; left: auto; right: 0; transform: none; }\r\n    td input[type=text] { width: 52%; padding: 8px; font-size: 15px; }\r\n    .num input[type=text] { text-align: right; }\r\n    .acoes { flex-direction: column; align-items: stretch; }\r\n    .acoes button { width: 100%; padding: 13px 18px; font-size: 15px; }\r\n    .acoes #mensagem { text-align: center; }\r\n    .campo { flex: 1 1 100%; }\r\n    .campo input, .campo select, .campo textarea { padding: 9px; font-size: 15px; }\r\n  }\r\n</style>\r\n</head>\r\n<body>\r\n<div class=\"topo\">\r\n  <h1 id=\"cabTitulo\"></h1>\r\n  <p id=\"cabSub\"></p>\r\n</div>\r\n<div class=\"conteudo\">\r\n  <div class=\"cartao\">\r\n    <p class=\"aviso\">Preencha o <b>preço unitário</b> dos itens que a sua empresa fornece e desmarque <b>Forneço</b>\r\n    naqueles com os quais não trabalha. Ao final, use <b>Gerar arquivo de resposta</b> e devolva o arquivo,\r\n    ou <b>Copiar resposta</b> e cole o conteúdo na mesma conversa em que recebeu este formulário.</p>\r\n  </div>\r\n  <div class=\"cartao\">\r\n    <table>\r\n      <colgroup>\r\n        <col style=\"width:21%\"><col style=\"width:8%\"><col style=\"width:5%\"><col style=\"width:7%\">\r\n        <col style=\"width:6%\"><col style=\"width:11%\"><col style=\"width:8%\"><col style=\"width:8%\">\r\n        <col style=\"width:13%\"><col style=\"width:7%\"><col style=\"width:6%\">\r\n      </colgroup>\r\n      <thead>\r\n        <tr>\r\n          <th>Item</th><th>Marca</th><th>Un.</th><th class=\"num\">Qtde</th>\r\n          <th class=\"centro\">Forneço <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Deixe marcado se a sua empresa fornece este item. Desmarque para deixá-lo fora da sua proposta.\">?</span></th><th class=\"num\">Preço unitário (R$) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Informe o preço para o prazo de pagamento que você está oferecendo no fim do formulário. Se você cobra a mais para vender a prazo, o acréscimo já deve estar neste preço.\">?</span></th><th>Marca ofertada</th>\r\n          <th class=\"num\">Qtde mínima <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Menor quantidade que você aceita vender deste item. Se for maior que a quantidade pedida, o item não entra na sua proposta.\">?</span></th><th class=\"num\">Múltiplo <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Preencha SOMENTE se você não fraciona a venda. O valor vai na MESMA unidade do item: se o item está em CX, o múltiplo é em caixas, não no conteúdo da caixa. Em dúvida, deixe em branco.\">?</span></th><th class=\"num\">Entrega (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Prazo deste item em particular, quando for diferente do prazo geral informado no fim do formulário. Deixe em branco para valer o prazo geral.\">?</span></th><th>Obs.</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody id=\"linhas\"></tbody>\r\n    </table>\r\n  </div>\r\n  <div class=\"cartao\">\r\n    <div class=\"rodape-campos\">\r\n      <div class=\"campo\"><label>Prazo de entrega (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Prazo que vale para todos os itens da proposta. Se algum item precisar de um prazo diferente, informe na coluna Entrega (dias) da lista — lá o prazo vale só para aquele item.\">?</span></label><input type=\"text\" id=\"prazoEntrega\" value=\"0\" inputmode=\"numeric\"></div>\r\n      <div class=\"campo\"><label>Prazo de pagamento (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Em quantos dias você espera receber. O preço unitário dos itens deve ser o preço para este prazo — se você cobra a mais para vender a prazo, inclua o acréscimo no preço.\">?</span></label><input type=\"text\" id=\"prazoPagamento\" value=\"0\" inputmode=\"numeric\"></div>\r\n      <div class=\"campo\"><label>Valor do frete (R$)</label><input type=\"text\" id=\"valorFrete\" value=\"0\" inputmode=\"decimal\"></div>\r\n      <div class=\"campo\"><label>Outros custos (R$) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Custos que você vai cobrar além da mercadoria e do frete: seguro, embalagem, taxas. Informe o total da proposta — o sistema rateia entre os itens.\">?</span></label><input type=\"text\" id=\"valorOutrosCustos\" value=\"0\" inputmode=\"decimal\"></div>\r\n      <div class=\"campo\"><label>Frete</label>\r\n        <select id=\"tipoFrete\">\r\n          <option value=\"0\">Incluso no preço</option>\r\n          <option value=\"1\">Por conta do comprador</option>\r\n          <option value=\"2\">Por conta do fornecedor</option>\r\n        </select>\r\n      </div>\r\n      <div class=\"campo\" style=\"flex:1 1 100%\"><label>Observações</label><textarea id=\"observacao\" rows=\"2\"></textarea></div>\r\n    </div>\r\n  </div>\r\n  <div class=\"cartao acoes\">\r\n    <button onclick=\"baixarResposta()\">Gerar arquivo de resposta</button>\r\n    <button class=\"secundario\" onclick=\"copiarResposta()\">Copiar resposta</button>\r\n    <span id=\"mensagem\"></span>\r\n  </div>\r\n</div>\r\n<script>\r\nvar DADOS = __DADOS__;\r\n\r\nvar ICONE_FORNECO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Deixe marcado se a sua empresa fornece este item. Desmarque para deixá-lo fora da sua proposta.\">?</span>';\r\nvar ICONE_MINIMA = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Menor quantidade que você aceita vender deste item. Se for maior que a quantidade pedida, o item não entra na sua proposta.\">?</span>';\r\nvar ICONE_MULTIPLO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Preencha SOMENTE se você não fraciona a venda. O valor vai na MESMA unidade do item: se o item está em CX, o múltiplo é em caixas, não no conteúdo da caixa. Em dúvida, deixe em branco.\">?</span>';\r\nvar ICONE_ENTREGA = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Prazo deste item em particular, quando for diferente do prazo geral informado no fim do formulário. Deixe em branco para valer o prazo geral.\">?</span>';\r\nvar ICONE_PRECO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Informe o preço para o prazo de pagamento que você está oferecendo no fim do formulário. Se você cobra a mais para vender a prazo, o acréscimo já deve estar neste preço.\">?</span>';\r\n\r\nfunction formatarQtde(v) { return Number(v).toLocaleString('pt-BR', {minimumFractionDigits: 2, maximumFractionDigits: 2}); }\r\n\r\nfunction montarTela() {\r\n  document.title = 'Cotação de compra ' + DADOS.numero + ' - ' + DADOS.emitente;\r\n  document.getElementById('cabTitulo').textContent = 'Cotação de compra nº ' + DADOS.numero + ' - ' + DADOS.emitente;\r\n  document.getElementById('cabSub').innerHTML = escapeHtml(DADOS.descricao) + '<br>Fornecedor: ' + escapeHtml(DADOS.fornecedor) +\r\n    ' <span class=\"prazo\">Responder até ' + escapeHtml(DADOS.prazoResposta) + '</span>';\r\n  var corpo = document.getElementById('linhas');\r\n  for (var i = 0; i < DADOS.itens.length; i++) {\r\n    var item = DADOS.itens[i];\r\n    var tr = document.createElement('tr');\r\n    tr.id = 'linha_' + i;\r\n    var codigo = item.codigoBarras || item.codigoInterno || '';\r\n    var codigoFornecedor = item.codigoFornecedor || '';\r\n    tr.innerHTML =\r\n      '<td class=\"titulo\" data-rotulo=\"Item\">' + escapeHtml(item.descricao) +\r\n        (codigo ? '<span class=\"sub\">' + escapeHtml(codigo) + '</span>' : '') +\r\n        (codigoFornecedor ? '<span class=\"sub\">Seu c&#243;digo: ' + escapeHtml(codigoFornecedor) + '</span>' : '') +\r\n        montarGrade(item) + '</td>' +\r\n      '<td data-rotulo=\"Marca\">' + (escapeHtml(item.marca) || '&mdash;') + '</td>' +\r\n      '<td data-rotulo=\"Unidade\">' + (escapeHtml(item.unidade) || '&mdash;') + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Quantidade\">' + formatarQtde(item.quantidade) + '</td>' +\r\n      '<td class=\"centro\" data-rotulo=\"Forneço\"><input type=\"checkbox\" id=\"disp_' + i + '\" checked onchange=\"alternarLinha(' + i + ')\">' + ICONE_FORNECO + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Preço unitário (R$)\"><input type=\"text\" id=\"preco_' + i + '\" inputmode=\"decimal\" onblur=\"validarPreco(' + i + ')\">' + ICONE_PRECO + '</td>' +\r\n      '<td data-rotulo=\"Marca ofertada\"><input type=\"text\" id=\"marca_' + i + '\"></td>' +\r\n      '<td class=\"num\" data-rotulo=\"Qtde mínima\"><input type=\"text\" id=\"qmin_' + i + '\" inputmode=\"decimal\">' + ICONE_MINIMA + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Múltiplo\"><input type=\"text\" id=\"mult_' + i + '\" inputmode=\"decimal\" oninput=\"avisarMultiplo(' + i + ')\">' + ICONE_MULTIPLO + '<span class=\"sub\" id=\"avisoMult_' + i + '\"></span></td>' +\r\n      '<td class=\"num\" data-rotulo=\"Entrega (dias)\"><input type=\"text\" id=\"prazo_' + i + '\" inputmode=\"numeric\">' + ICONE_ENTREGA + '</td>' +\r\n      '<td data-rotulo=\"Obs.\"><input type=\"text\" id=\"obs_' + i + '\"></td>';\r\n    corpo.appendChild(tr);\r\n  }\r\n}\r\n\r\nfunction montarGrade(item) {\r\n  if (!item.grades || !item.grades.length) return '';\r\n  var partes = [];\r\n  for (var i = 0; i < item.grades.length; i++) {\r\n    var g = item.grades[i];\r\n    if (g.quantidade <= 0) continue;\r\n    partes.push(escapeHtml((g.linha + ' ' + g.coluna).trim()) + ': ' + formatarQtde(g.quantidade));\r\n  }\r\n  if (!partes.length) return '';\r\n  return '<span class=\"grade\">Grade &mdash; ' + partes.join(' &middot; ') + '</span>';\r\n}\r\n\r\nfunction escapeHtml(s) {\r\n  return String(s == null ? '' : s).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/\"/g, '&quot;');\r\n}\r\n\r\nfunction alternarLinha(i) {\r\n  var ativo = document.getElementById('disp_' + i).checked;\r\n  document.getElementById('linha_' + i).className = ativo ? '' : 'indisponivel';\r\n  var campos = ['preco_', 'marca_', 'qmin_', 'mult_', 'prazo_', 'obs_'];\r\n  for (var c = 0; c < campos.length; c++) document.getElementById(campos[c] + i).disabled = !ativo;\r\n}\r\n\r\nfunction parseNumero(texto) {\r\n  var s = String(texto == null ? '' : texto).trim();\r\n  if (s === '') return 0;\r\n  if (s.indexOf(',') >= 0) s = s.split('.').join('').split(',').join('.');\r\n  var n = parseFloat(s);\r\n  return isNaN(n) ? NaN : n;\r\n}\r\n\r\nfunction parseInteiro(texto) {\r\n  var n = parseInt(String(texto == null ? '' : texto).trim(), 10);\r\n  return isNaN(n) ? 0 : n;\r\n}\r\n\r\nfunction avisarMultiplo(i) {\r\n  var aviso = document.getElementById('avisoMult_' + i);\r\n  var multiplo = parseNumero(document.getElementById('mult_' + i).value);\r\n  var pedida = Number(DADOS.itens[i].quantidade);\r\n  var unidade = DADOS.itens[i].unidade || '';\r\n  aviso.className = 'sub';\r\n  aviso.textContent = '';\r\n  if (isNaN(multiplo) || multiplo <= 0 || pedida <= 0) return;\r\n  var ajustada = Math.ceil(pedida / multiplo) * multiplo;\r\n  if (ajustada === pedida) return;\r\n  aviso.className = 'sub alerta';\r\n  var un = unidade ? ' ' + unidade : '';\r\n  aviso.textContent = 'a quantidade passa de ' + formatarQtde(pedida) + un + ' para ' + formatarQtde(ajustada) + un;\r\n}\r\n\r\nfunction validarPreco(i) {\r\n  var campo = document.getElementById('preco_' + i);\r\n  var n = parseNumero(campo.value);\r\n  campo.className = (campo.value.trim() !== '' && (isNaN(n) || n < 0)) ? 'invalido' : '';\r\n}\r\n\r\nfunction utf8Bytes(texto) {\r\n  var bytes = [];\r\n  for (var i = 0; i < texto.length; i++) {\r\n    var c = texto.charCodeAt(i);\r\n    if (c >= 0xD800 && c <= 0xDBFF && i + 1 < texto.length) {\r\n      var c2 = texto.charCodeAt(i + 1);\r\n      if (c2 >= 0xDC00 && c2 <= 0xDFFF) { c = 0x10000 + ((c - 0xD800) << 10) + (c2 - 0xDC00); i++; }\r\n    }\r\n    if (c < 0x80) bytes.push(c);\r\n    else if (c < 0x800) { bytes.push(0xC0 | (c >> 6), 0x80 | (c & 0x3F)); }\r\n    else if (c < 0x10000) { bytes.push(0xE0 | (c >> 12), 0x80 | ((c >> 6) & 0x3F), 0x80 | (c & 0x3F)); }\r\n    else { bytes.push(0xF0 | (c >> 18), 0x80 | ((c >> 12) & 0x3F), 0x80 | ((c >> 6) & 0x3F), 0x80 | (c & 0x3F)); }\r\n  }\r\n  return bytes;\r\n}\r\n\r\nfunction fnv1a32(texto) {\r\n  var hash = 0x811C9DC5;\r\n  var bytes = utf8Bytes(texto);\r\n  for (var i = 0; i < bytes.length; i++) {\r\n    hash = hash ^ bytes[i];\r\n    hash = Math.imul(hash, 16777619) >>> 0;\r\n  }\r\n  var hex = hash.toString(16);\r\n  while (hex.length < 8) hex = '0' + hex;\r\n  return hex;\r\n}\r\n\r\nfunction fixado(n) { return Number(n).toFixed(4); }\r\n\r\nfunction montarResposta() {\r\n  var propostas = [];\r\n  var algumPrecoValido = false;\r\n  for (var i = 0; i < DADOS.itens.length; i++) {\r\n    var disponivel = document.getElementById('disp_' + i).checked;\r\n    var preco = disponivel ? parseNumero(document.getElementById('preco_' + i).value) : 0;\r\n    if (disponivel) {\r\n      if (isNaN(preco) || preco < 0) throw new Error('O preço do item \"' + DADOS.itens[i].descricao + '\" é inválido.');\r\n      if (preco === 0) disponivel = false;\r\n      else algumPrecoValido = true;\r\n    }\r\n    propostas.push({\r\n      item: DADOS.itens[i].id,\r\n      disponivel: disponivel,\r\n      preco: disponivel ? parseFloat(fixado(preco)) : 0,\r\n      marca: disponivel ? document.getElementById('marca_' + i).value.trim() : '',\r\n      qtdeMinima: disponivel ? parseFloat(fixado(parseNumero(document.getElementById('qmin_' + i).value) || 0)) : 0,\r\n      multiplo: disponivel ? parseFloat(fixado(parseNumero(document.getElementById('mult_' + i).value) || 0)) : 0,\r\n      prazoDias: disponivel ? parseInteiro(document.getElementById('prazo_' + i).value) : 0,\r\n      obs: disponivel ? document.getElementById('obs_' + i).value.trim() : ''\r\n    });\r\n  }\r\n  if (!algumPrecoValido) throw new Error('Informe o preço de ao menos um item, ou avise o comprador de que não fornecerá nenhum.');\r\n\r\n  var valorFrete = parseNumero(document.getElementById('valorFrete').value);\r\n  if (isNaN(valorFrete) || valorFrete < 0) throw new Error('O valor do frete é inválido.');\r\n\r\n  var valorOutrosCustos = parseNumero(document.getElementById('valorOutrosCustos').value);\r\n  if (isNaN(valorOutrosCustos) || valorOutrosCustos < 0) throw new Error('O valor de outros custos é inválido.');\r\n\r\n  var resposta = {\r\n    versao: DADOS.versao,\r\n    tipo: DADOS.tipo,\r\n    token: DADOS.token,\r\n    cotacao: DADOS.numero,\r\n    fornecedor: DADOS.fornecedor,\r\n    prazoEntregaDias: parseInteiro(document.getElementById('prazoEntrega').value),\r\n    condicaoPagamentoDias: parseInteiro(document.getElementById('prazoPagamento').value),\r\n    valorFrete: parseFloat(fixado(valorFrete)),\r\n    tipoFrete: parseInteiro(document.getElementById('tipoFrete').value),\r\n    valorOutrosCustos: parseFloat(fixado(valorOutrosCustos)),\r\n    observacao: document.getElementById('observacao').value.trim(),\r\n    propostas: propostas\r\n  };\r\n  resposta.hash = calcularHash(resposta);\r\n  return resposta;\r\n}\r\n\r\nfunction calcularHash(r) {\r\n  var texto = r.token + '|' + r.prazoEntregaDias + '|' + r.condicaoPagamentoDias + '|' + fixado(r.valorFrete) + '|' + r.tipoFrete + '|' + fixado(r.valorOutrosCustos) + '|' + (r.observacao || '');\r\n  var ordenadas = r.propostas.slice().sort(function (a, b) { return a.item < b.item ? -1 : (a.item > b.item ? 1 : 0); });\r\n  for (var i = 0; i < ordenadas.length; i++) {\r\n    var p = ordenadas[i];\r\n    texto += '|' + p.item + ';' + (p.disponivel ? '1' : '0') + ';' + fixado(p.preco) + ';' + (p.marca || '') + ';' +\r\n             fixado(p.qtdeMinima) + ';' + fixado(p.multiplo) + ';' + p.prazoDias + ';' + (p.obs || '');\r\n  }\r\n  return fnv1a32(texto);\r\n}\r\n\r\nfunction mostrarMensagem(texto, ok) {\r\n  var m = document.getElementById('mensagem');\r\n  m.textContent = texto;\r\n  m.className = ok ? 'ok' : 'erro';\r\n}\r\n\r\nfunction baixarResposta() {\r\n  try {\r\n    var resposta = montarResposta();\r\n    var json = JSON.stringify(resposta);\r\n    var blob = new Blob([json], { type: 'application/json;charset=utf-8' });\r\n    var link = document.createElement('a');\r\n    link.href = URL.createObjectURL(blob);\r\n    link.download = 'resposta-cotacao-' + DADOS.numero + '.json';\r\n    document.body.appendChild(link);\r\n    link.click();\r\n    document.body.removeChild(link);\r\n    mostrarMensagem('Arquivo gerado. Devolva-o respondendo a mensagem que você recebeu.', true);\r\n  } catch (e) { mostrarMensagem(e.message, false); }\r\n}\r\n\r\nfunction copiarResposta() {\r\n  try {\r\n    var resposta = montarResposta();\r\n    var json = JSON.stringify(resposta);\r\n    var base64 = btoa(unescape(encodeURIComponent(json)));\r\n    if (navigator.clipboard && navigator.clipboard.writeText) {\r\n      navigator.clipboard.writeText(base64).then(function () {\r\n        mostrarMensagem('Resposta copiada. Cole o conteúdo na resposta ao comprador.', true);\r\n      }, function () { copiarPorArea(base64); });\r\n      return;\r\n    }\r\n    copiarPorArea(base64);\r\n  } catch (e) { mostrarMensagem(e.message, false); }\r\n}\r\n\r\nfunction copiarPorArea(texto) {\r\n  var area = document.createElement('textarea');\r\n  area.value = texto;\r\n  area.setAttribute('readonly', '');\r\n  area.style.position = 'fixed';\r\n  area.style.top = '0';\r\n  area.style.left = '0';\r\n  area.style.opacity = '0';\r\n  document.body.appendChild(area);\r\n  area.select();\r\n  area.setSelectionRange(0, texto.length);\r\n  var ok = false;\r\n  try { ok = document.execCommand('copy'); } catch (e) { ok = false; }\r\n  document.body.removeChild(area);\r\n  mostrarMensagem(ok ? 'Resposta copiada. Cole o conteúdo na resposta ao comprador.'\r\n                     : 'Não foi possível copiar automaticamente. Use Gerar arquivo de resposta.', ok);\r\n}\r\n\r\nfunction ligarAjudas() {\r\n  document.addEventListener('click', function (e) {\r\n    var alvo = e.target;\r\n    while (alvo && alvo !== document.body && !(alvo.className && String(alvo.className).indexOf('ajuda') >= 0)) alvo = alvo.parentNode;\r\n    if (alvo === document.body) alvo = null;\r\n    var abertos = document.querySelectorAll('.ajuda.aberta');\r\n    for (var i = 0; i < abertos.length; i++) if (abertos[i] !== alvo) abertos[i].className = abertos[i].className.replace(' aberta', '');\r\n    if (alvo && String(alvo.className).indexOf('aberta') < 0) alvo.className += ' aberta';\r\n  });\r\n}\r\n\r\nmontarTela();\r\nligarAjudas();\r\n</script>\r\n</body>\r\n</html>";

		public static string Gerar(CotacaoDto cotacao, ParticipanteCotacaoDto participante, string nomeEmitente, IDictionary<string, string> codigosFornecedor = null)
		{
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			string newValue = JsonConvert.SerializeObject((object)new
			{
				versao = 2,
				tipo = "resposta-cotacao-digisat",
				numero = cotacao.Numero,
				descricao = cotacao.Descricao,
				emitente = nomeEmitente,
				fornecedor = participante.Fornecedor.Nome,
				prazoResposta = cotacao.DataLimiteResposta.ToString("dd/MM/yyyy"),
				token = ((participante.TokenAcesso == null) ? string.Empty : participante.TokenAcesso.Valor),
				itens = (from x in HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(cotacao.Itens)
					select new
					{
						id = x.Id,
						descricao = x.Descricao,
						marca = x.Marca,
						unidade = x.UnidadeMedida,
						quantidade = x.Quantidade,
						codigoBarras = x.CodigoBarras,
						codigoInterno = x.CodigoInterno,
						codigoFornecedor = ObterCodigoFornecedor(codigosFornecedor, x.Id),
						grades = (from g in HelperIEnumerable.ToListEmptyWhenNull<GradeItemCotacaoDto>(x.Grades)
							select new
							{
								linha = g.Linha,
								coluna = g.Coluna,
								quantidade = g.Quantidade
							}).ToList()
					}).ToList()
			}, new JsonSerializerSettings
			{
				StringEscapeHandling = (StringEscapeHandling)2
			});
			return "<!DOCTYPE html>\r\n<html lang=\"pt-BR\">\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n<meta name=\"theme-color\" content=\"#14477d\">\r\n<title>Cotação de compra</title>\r\n<style>\r\n  * { box-sizing: border-box; }\r\n  body { font-family: 'Segoe UI', Arial, sans-serif; margin: 0; background: #f2f4f7; color: #222; -webkit-text-size-adjust: 100%; }\r\n  .topo { background: #14477d; color: #fff; padding: 14px 16px; }\r\n  .topo h1 { margin: 0; font-size: 18px; line-height: 1.3; }\r\n  .topo p { margin: 6px 0 0 0; font-size: 13px; opacity: .92; line-height: 1.5; }\r\n  .topo .prazo { display: inline-block; background: rgba(255,255,255,.16); border-radius: 12px; padding: 1px 9px; margin-top: 2px; }\r\n  .conteudo { max-width: 1140px; margin: 14px auto; padding: 0 12px; }\r\n  .cartao { background: #fff; border: 1px solid #dde3ea; border-radius: 6px; padding: 14px; margin-bottom: 14px; }\r\n\r\n  table { border-collapse: collapse; width: 100%; font-size: 13px; }\r\n  th, td { border: 1px solid #dde3ea; padding: 7px 8px; text-align: left; vertical-align: middle; }\r\n  th { background: #eef2f7; font-weight: 600; position: sticky; top: 0; z-index: 1; }\r\n  tbody tr:nth-child(even) td { background: #fbfcfd; }\r\n  td input[type=text] { width: 100%; border: 1px solid #c6ced8; border-radius: 3px; padding: 6px; font-size: 13px; font-family: inherit; }\r\n  td input[type=text]:focus { outline: 0; border-color: #14477d; box-shadow: 0 0 0 2px rgba(20,71,125,.15); }\r\n  td input.invalido { border-color: #d33; background: #fff3f3; }\r\n  .num { text-align: right; }\r\n  .num input[type=text] { text-align: right; }\r\n  .centro { text-align: center; }\r\n  .centro input[type=checkbox] { width: 20px; height: 20px; cursor: pointer; margin: 0; }\r\n  .titulo { font-weight: 600; }\r\n  .sub { display: block; font-size: 11px; font-weight: 400; color: #6b7683; margin-top: 2px; }\r\n  .sub.alerta { color: #b35c00; font-weight: 600; }\r\n  .grade { display: block; font-size: 11px; font-weight: 400; color: #14477d; margin-top: 3px; line-height: 1.5; }\r\n\r\n  .rodape-campos { display: flex; flex-wrap: wrap; gap: 14px; }\r\n  .campo { display: flex; flex-direction: column; font-size: 13px; flex: 1 1 190px; }\r\n  .campo label { font-weight: 600; margin-bottom: 4px; }\r\n  .campo input, .campo select, .campo textarea { border: 1px solid #c6ced8; border-radius: 3px; padding: 7px; font-size: 13px; font-family: inherit; width: 100%; }\r\n  .campo textarea { resize: vertical; }\r\n\r\n  .acoes { display: flex; gap: 10px; align-items: center; flex-wrap: wrap; }\r\n  button { background: #14477d; color: #fff; border: 0; border-radius: 4px; padding: 11px 18px; font-size: 14px; font-family: inherit; cursor: pointer; }\r\n  button:active { opacity: .85; }\r\n  button.secundario { background: #5a6b7d; }\r\n  .aviso { font-size: 13px; color: #555; margin: 0; line-height: 1.55; }\r\n  #mensagem { font-size: 13px; font-weight: 600; }\r\n  #mensagem.ok { color: #1a7a2e; }\r\n  #mensagem.erro { color: #c22; }\r\n  tr.indisponivel td { background: #f7f8f9; color: #9aa4ae; }\r\n  tr.indisponivel .titulo { text-decoration: line-through; }\r\n\r\n  .ajuda { display: inline-flex; align-items: center; justify-content: center; width: 15px; height: 15px;\r\n           border-radius: 50%; background: #8b98a7; color: #fff; font-size: 10px; font-weight: 700;\r\n           cursor: help; position: relative; margin-left: 4px; vertical-align: middle; }\r\n  .ajuda:focus { outline: 2px solid #14477d; outline-offset: 1px; }\r\n  .ajuda::after { content: attr(data-dica); position: absolute; left: 50%; top: calc(100% + 7px);\r\n                  transform: translateX(-50%); width: 230px; background: #2b3947; color: #fff;\r\n                  font-size: 12px; font-weight: 400; line-height: 1.45; text-align: left; padding: 8px 10px;\r\n                  border-radius: 5px; box-shadow: 0 3px 10px rgba(0,0,0,.22); z-index: 5; display: none; }\r\n  .ajuda:hover::after, .ajuda:focus::after, .ajuda.aberta::after { display: block; }\r\n  .ajuda-celula { display: none; }\r\n\r\n  @media (max-width: 860px) {\r\n    .conteudo { margin: 10px auto; padding: 0 8px; }\r\n    .cartao { padding: 10px; }\r\n    table, tbody, tr, td { display: block; width: 100%; }\r\n    thead { display: none; }\r\n    tr { border: 1px solid #dde3ea; border-radius: 6px; margin-bottom: 12px; overflow: hidden; }\r\n    tbody tr:nth-child(even) td { background: transparent; }\r\n    td { border: 0; border-bottom: 1px solid #f0f3f6; display: flex; justify-content: space-between;\r\n         align-items: center; gap: 12px; padding: 9px 10px; text-align: left; min-height: 42px; }\r\n    td:last-child { border-bottom: 0; }\r\n    td::before { content: attr(data-rotulo); font-weight: 600; color: #55606d; flex: 0 0 44%; font-size: 12px; }\r\n    td.titulo { display: block; background: #eef2f7; border-bottom: 1px solid #dde3ea; font-size: 14px; }\r\n    td.titulo::before { content: none; }\r\n    td.num, td.centro { text-align: left; }\r\n    .ajuda-celula { display: inline-flex; }\r\n    .ajuda::after { width: 200px; left: auto; right: 0; transform: none; }\r\n    td input[type=text] { width: 52%; padding: 8px; font-size: 15px; }\r\n    .num input[type=text] { text-align: right; }\r\n    .acoes { flex-direction: column; align-items: stretch; }\r\n    .acoes button { width: 100%; padding: 13px 18px; font-size: 15px; }\r\n    .acoes #mensagem { text-align: center; }\r\n    .campo { flex: 1 1 100%; }\r\n    .campo input, .campo select, .campo textarea { padding: 9px; font-size: 15px; }\r\n  }\r\n</style>\r\n</head>\r\n<body>\r\n<div class=\"topo\">\r\n  <h1 id=\"cabTitulo\"></h1>\r\n  <p id=\"cabSub\"></p>\r\n</div>\r\n<div class=\"conteudo\">\r\n  <div class=\"cartao\">\r\n    <p class=\"aviso\">Preencha o <b>preço unitário</b> dos itens que a sua empresa fornece e desmarque <b>Forneço</b>\r\n    naqueles com os quais não trabalha. Ao final, use <b>Gerar arquivo de resposta</b> e devolva o arquivo,\r\n    ou <b>Copiar resposta</b> e cole o conteúdo na mesma conversa em que recebeu este formulário.</p>\r\n  </div>\r\n  <div class=\"cartao\">\r\n    <table>\r\n      <colgroup>\r\n        <col style=\"width:21%\"><col style=\"width:8%\"><col style=\"width:5%\"><col style=\"width:7%\">\r\n        <col style=\"width:6%\"><col style=\"width:11%\"><col style=\"width:8%\"><col style=\"width:8%\">\r\n        <col style=\"width:13%\"><col style=\"width:7%\"><col style=\"width:6%\">\r\n      </colgroup>\r\n      <thead>\r\n        <tr>\r\n          <th>Item</th><th>Marca</th><th>Un.</th><th class=\"num\">Qtde</th>\r\n          <th class=\"centro\">Forneço <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Deixe marcado se a sua empresa fornece este item. Desmarque para deixá-lo fora da sua proposta.\">?</span></th><th class=\"num\">Preço unitário (R$) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Informe o preço para o prazo de pagamento que você está oferecendo no fim do formulário. Se você cobra a mais para vender a prazo, o acréscimo já deve estar neste preço.\">?</span></th><th>Marca ofertada</th>\r\n          <th class=\"num\">Qtde mínima <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Menor quantidade que você aceita vender deste item. Se for maior que a quantidade pedida, o item não entra na sua proposta.\">?</span></th><th class=\"num\">Múltiplo <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Preencha SOMENTE se você não fraciona a venda. O valor vai na MESMA unidade do item: se o item está em CX, o múltiplo é em caixas, não no conteúdo da caixa. Em dúvida, deixe em branco.\">?</span></th><th class=\"num\">Entrega (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Prazo deste item em particular, quando for diferente do prazo geral informado no fim do formulário. Deixe em branco para valer o prazo geral.\">?</span></th><th>Obs.</th>\r\n        </tr>\r\n      </thead>\r\n      <tbody id=\"linhas\"></tbody>\r\n    </table>\r\n  </div>\r\n  <div class=\"cartao\">\r\n    <div class=\"rodape-campos\">\r\n      <div class=\"campo\"><label>Prazo de entrega (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Prazo que vale para todos os itens da proposta. Se algum item precisar de um prazo diferente, informe na coluna Entrega (dias) da lista — lá o prazo vale só para aquele item.\">?</span></label><input type=\"text\" id=\"prazoEntrega\" value=\"0\" inputmode=\"numeric\"></div>\r\n      <div class=\"campo\"><label>Prazo de pagamento (dias) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Em quantos dias você espera receber. O preço unitário dos itens deve ser o preço para este prazo — se você cobra a mais para vender a prazo, inclua o acréscimo no preço.\">?</span></label><input type=\"text\" id=\"prazoPagamento\" value=\"0\" inputmode=\"numeric\"></div>\r\n      <div class=\"campo\"><label>Valor do frete (R$)</label><input type=\"text\" id=\"valorFrete\" value=\"0\" inputmode=\"decimal\"></div>\r\n      <div class=\"campo\"><label>Outros custos (R$) <span class=\"ajuda\" tabindex=\"0\" data-dica=\"Custos que você vai cobrar além da mercadoria e do frete: seguro, embalagem, taxas. Informe o total da proposta — o sistema rateia entre os itens.\">?</span></label><input type=\"text\" id=\"valorOutrosCustos\" value=\"0\" inputmode=\"decimal\"></div>\r\n      <div class=\"campo\"><label>Frete</label>\r\n        <select id=\"tipoFrete\">\r\n          <option value=\"0\">Incluso no preço</option>\r\n          <option value=\"1\">Por conta do comprador</option>\r\n          <option value=\"2\">Por conta do fornecedor</option>\r\n        </select>\r\n      </div>\r\n      <div class=\"campo\" style=\"flex:1 1 100%\"><label>Observações</label><textarea id=\"observacao\" rows=\"2\"></textarea></div>\r\n    </div>\r\n  </div>\r\n  <div class=\"cartao acoes\">\r\n    <button onclick=\"baixarResposta()\">Gerar arquivo de resposta</button>\r\n    <button class=\"secundario\" onclick=\"copiarResposta()\">Copiar resposta</button>\r\n    <span id=\"mensagem\"></span>\r\n  </div>\r\n</div>\r\n<script>\r\nvar DADOS = __DADOS__;\r\n\r\nvar ICONE_FORNECO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Deixe marcado se a sua empresa fornece este item. Desmarque para deixá-lo fora da sua proposta.\">?</span>';\r\nvar ICONE_MINIMA = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Menor quantidade que você aceita vender deste item. Se for maior que a quantidade pedida, o item não entra na sua proposta.\">?</span>';\r\nvar ICONE_MULTIPLO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Preencha SOMENTE se você não fraciona a venda. O valor vai na MESMA unidade do item: se o item está em CX, o múltiplo é em caixas, não no conteúdo da caixa. Em dúvida, deixe em branco.\">?</span>';\r\nvar ICONE_ENTREGA = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Prazo deste item em particular, quando for diferente do prazo geral informado no fim do formulário. Deixe em branco para valer o prazo geral.\">?</span>';\r\nvar ICONE_PRECO = '<span class=\"ajuda ajuda-celula\" tabindex=\"0\" data-dica=\"Informe o preço para o prazo de pagamento que você está oferecendo no fim do formulário. Se você cobra a mais para vender a prazo, o acréscimo já deve estar neste preço.\">?</span>';\r\n\r\nfunction formatarQtde(v) { return Number(v).toLocaleString('pt-BR', {minimumFractionDigits: 2, maximumFractionDigits: 2}); }\r\n\r\nfunction montarTela() {\r\n  document.title = 'Cotação de compra ' + DADOS.numero + ' - ' + DADOS.emitente;\r\n  document.getElementById('cabTitulo').textContent = 'Cotação de compra nº ' + DADOS.numero + ' - ' + DADOS.emitente;\r\n  document.getElementById('cabSub').innerHTML = escapeHtml(DADOS.descricao) + '<br>Fornecedor: ' + escapeHtml(DADOS.fornecedor) +\r\n    ' <span class=\"prazo\">Responder até ' + escapeHtml(DADOS.prazoResposta) + '</span>';\r\n  var corpo = document.getElementById('linhas');\r\n  for (var i = 0; i < DADOS.itens.length; i++) {\r\n    var item = DADOS.itens[i];\r\n    var tr = document.createElement('tr');\r\n    tr.id = 'linha_' + i;\r\n    var codigo = item.codigoBarras || item.codigoInterno || '';\r\n    var codigoFornecedor = item.codigoFornecedor || '';\r\n    tr.innerHTML =\r\n      '<td class=\"titulo\" data-rotulo=\"Item\">' + escapeHtml(item.descricao) +\r\n        (codigo ? '<span class=\"sub\">' + escapeHtml(codigo) + '</span>' : '') +\r\n        (codigoFornecedor ? '<span class=\"sub\">Seu c&#243;digo: ' + escapeHtml(codigoFornecedor) + '</span>' : '') +\r\n        montarGrade(item) + '</td>' +\r\n      '<td data-rotulo=\"Marca\">' + (escapeHtml(item.marca) || '&mdash;') + '</td>' +\r\n      '<td data-rotulo=\"Unidade\">' + (escapeHtml(item.unidade) || '&mdash;') + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Quantidade\">' + formatarQtde(item.quantidade) + '</td>' +\r\n      '<td class=\"centro\" data-rotulo=\"Forneço\"><input type=\"checkbox\" id=\"disp_' + i + '\" checked onchange=\"alternarLinha(' + i + ')\">' + ICONE_FORNECO + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Preço unitário (R$)\"><input type=\"text\" id=\"preco_' + i + '\" inputmode=\"decimal\" onblur=\"validarPreco(' + i + ')\">' + ICONE_PRECO + '</td>' +\r\n      '<td data-rotulo=\"Marca ofertada\"><input type=\"text\" id=\"marca_' + i + '\"></td>' +\r\n      '<td class=\"num\" data-rotulo=\"Qtde mínima\"><input type=\"text\" id=\"qmin_' + i + '\" inputmode=\"decimal\">' + ICONE_MINIMA + '</td>' +\r\n      '<td class=\"num\" data-rotulo=\"Múltiplo\"><input type=\"text\" id=\"mult_' + i + '\" inputmode=\"decimal\" oninput=\"avisarMultiplo(' + i + ')\">' + ICONE_MULTIPLO + '<span class=\"sub\" id=\"avisoMult_' + i + '\"></span></td>' +\r\n      '<td class=\"num\" data-rotulo=\"Entrega (dias)\"><input type=\"text\" id=\"prazo_' + i + '\" inputmode=\"numeric\">' + ICONE_ENTREGA + '</td>' +\r\n      '<td data-rotulo=\"Obs.\"><input type=\"text\" id=\"obs_' + i + '\"></td>';\r\n    corpo.appendChild(tr);\r\n  }\r\n}\r\n\r\nfunction montarGrade(item) {\r\n  if (!item.grades || !item.grades.length) return '';\r\n  var partes = [];\r\n  for (var i = 0; i < item.grades.length; i++) {\r\n    var g = item.grades[i];\r\n    if (g.quantidade <= 0) continue;\r\n    partes.push(escapeHtml((g.linha + ' ' + g.coluna).trim()) + ': ' + formatarQtde(g.quantidade));\r\n  }\r\n  if (!partes.length) return '';\r\n  return '<span class=\"grade\">Grade &mdash; ' + partes.join(' &middot; ') + '</span>';\r\n}\r\n\r\nfunction escapeHtml(s) {\r\n  return String(s == null ? '' : s).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/\"/g, '&quot;');\r\n}\r\n\r\nfunction alternarLinha(i) {\r\n  var ativo = document.getElementById('disp_' + i).checked;\r\n  document.getElementById('linha_' + i).className = ativo ? '' : 'indisponivel';\r\n  var campos = ['preco_', 'marca_', 'qmin_', 'mult_', 'prazo_', 'obs_'];\r\n  for (var c = 0; c < campos.length; c++) document.getElementById(campos[c] + i).disabled = !ativo;\r\n}\r\n\r\nfunction parseNumero(texto) {\r\n  var s = String(texto == null ? '' : texto).trim();\r\n  if (s === '') return 0;\r\n  if (s.indexOf(',') >= 0) s = s.split('.').join('').split(',').join('.');\r\n  var n = parseFloat(s);\r\n  return isNaN(n) ? NaN : n;\r\n}\r\n\r\nfunction parseInteiro(texto) {\r\n  var n = parseInt(String(texto == null ? '' : texto).trim(), 10);\r\n  return isNaN(n) ? 0 : n;\r\n}\r\n\r\nfunction avisarMultiplo(i) {\r\n  var aviso = document.getElementById('avisoMult_' + i);\r\n  var multiplo = parseNumero(document.getElementById('mult_' + i).value);\r\n  var pedida = Number(DADOS.itens[i].quantidade);\r\n  var unidade = DADOS.itens[i].unidade || '';\r\n  aviso.className = 'sub';\r\n  aviso.textContent = '';\r\n  if (isNaN(multiplo) || multiplo <= 0 || pedida <= 0) return;\r\n  var ajustada = Math.ceil(pedida / multiplo) * multiplo;\r\n  if (ajustada === pedida) return;\r\n  aviso.className = 'sub alerta';\r\n  var un = unidade ? ' ' + unidade : '';\r\n  aviso.textContent = 'a quantidade passa de ' + formatarQtde(pedida) + un + ' para ' + formatarQtde(ajustada) + un;\r\n}\r\n\r\nfunction validarPreco(i) {\r\n  var campo = document.getElementById('preco_' + i);\r\n  var n = parseNumero(campo.value);\r\n  campo.className = (campo.value.trim() !== '' && (isNaN(n) || n < 0)) ? 'invalido' : '';\r\n}\r\n\r\nfunction utf8Bytes(texto) {\r\n  var bytes = [];\r\n  for (var i = 0; i < texto.length; i++) {\r\n    var c = texto.charCodeAt(i);\r\n    if (c >= 0xD800 && c <= 0xDBFF && i + 1 < texto.length) {\r\n      var c2 = texto.charCodeAt(i + 1);\r\n      if (c2 >= 0xDC00 && c2 <= 0xDFFF) { c = 0x10000 + ((c - 0xD800) << 10) + (c2 - 0xDC00); i++; }\r\n    }\r\n    if (c < 0x80) bytes.push(c);\r\n    else if (c < 0x800) { bytes.push(0xC0 | (c >> 6), 0x80 | (c & 0x3F)); }\r\n    else if (c < 0x10000) { bytes.push(0xE0 | (c >> 12), 0x80 | ((c >> 6) & 0x3F), 0x80 | (c & 0x3F)); }\r\n    else { bytes.push(0xF0 | (c >> 18), 0x80 | ((c >> 12) & 0x3F), 0x80 | ((c >> 6) & 0x3F), 0x80 | (c & 0x3F)); }\r\n  }\r\n  return bytes;\r\n}\r\n\r\nfunction fnv1a32(texto) {\r\n  var hash = 0x811C9DC5;\r\n  var bytes = utf8Bytes(texto);\r\n  for (var i = 0; i < bytes.length; i++) {\r\n    hash = hash ^ bytes[i];\r\n    hash = Math.imul(hash, 16777619) >>> 0;\r\n  }\r\n  var hex = hash.toString(16);\r\n  while (hex.length < 8) hex = '0' + hex;\r\n  return hex;\r\n}\r\n\r\nfunction fixado(n) { return Number(n).toFixed(4); }\r\n\r\nfunction montarResposta() {\r\n  var propostas = [];\r\n  var algumPrecoValido = false;\r\n  for (var i = 0; i < DADOS.itens.length; i++) {\r\n    var disponivel = document.getElementById('disp_' + i).checked;\r\n    var preco = disponivel ? parseNumero(document.getElementById('preco_' + i).value) : 0;\r\n    if (disponivel) {\r\n      if (isNaN(preco) || preco < 0) throw new Error('O preço do item \"' + DADOS.itens[i].descricao + '\" é inválido.');\r\n      if (preco === 0) disponivel = false;\r\n      else algumPrecoValido = true;\r\n    }\r\n    propostas.push({\r\n      item: DADOS.itens[i].id,\r\n      disponivel: disponivel,\r\n      preco: disponivel ? parseFloat(fixado(preco)) : 0,\r\n      marca: disponivel ? document.getElementById('marca_' + i).value.trim() : '',\r\n      qtdeMinima: disponivel ? parseFloat(fixado(parseNumero(document.getElementById('qmin_' + i).value) || 0)) : 0,\r\n      multiplo: disponivel ? parseFloat(fixado(parseNumero(document.getElementById('mult_' + i).value) || 0)) : 0,\r\n      prazoDias: disponivel ? parseInteiro(document.getElementById('prazo_' + i).value) : 0,\r\n      obs: disponivel ? document.getElementById('obs_' + i).value.trim() : ''\r\n    });\r\n  }\r\n  if (!algumPrecoValido) throw new Error('Informe o preço de ao menos um item, ou avise o comprador de que não fornecerá nenhum.');\r\n\r\n  var valorFrete = parseNumero(document.getElementById('valorFrete').value);\r\n  if (isNaN(valorFrete) || valorFrete < 0) throw new Error('O valor do frete é inválido.');\r\n\r\n  var valorOutrosCustos = parseNumero(document.getElementById('valorOutrosCustos').value);\r\n  if (isNaN(valorOutrosCustos) || valorOutrosCustos < 0) throw new Error('O valor de outros custos é inválido.');\r\n\r\n  var resposta = {\r\n    versao: DADOS.versao,\r\n    tipo: DADOS.tipo,\r\n    token: DADOS.token,\r\n    cotacao: DADOS.numero,\r\n    fornecedor: DADOS.fornecedor,\r\n    prazoEntregaDias: parseInteiro(document.getElementById('prazoEntrega').value),\r\n    condicaoPagamentoDias: parseInteiro(document.getElementById('prazoPagamento').value),\r\n    valorFrete: parseFloat(fixado(valorFrete)),\r\n    tipoFrete: parseInteiro(document.getElementById('tipoFrete').value),\r\n    valorOutrosCustos: parseFloat(fixado(valorOutrosCustos)),\r\n    observacao: document.getElementById('observacao').value.trim(),\r\n    propostas: propostas\r\n  };\r\n  resposta.hash = calcularHash(resposta);\r\n  return resposta;\r\n}\r\n\r\nfunction calcularHash(r) {\r\n  var texto = r.token + '|' + r.prazoEntregaDias + '|' + r.condicaoPagamentoDias + '|' + fixado(r.valorFrete) + '|' + r.tipoFrete + '|' + fixado(r.valorOutrosCustos) + '|' + (r.observacao || '');\r\n  var ordenadas = r.propostas.slice().sort(function (a, b) { return a.item < b.item ? -1 : (a.item > b.item ? 1 : 0); });\r\n  for (var i = 0; i < ordenadas.length; i++) {\r\n    var p = ordenadas[i];\r\n    texto += '|' + p.item + ';' + (p.disponivel ? '1' : '0') + ';' + fixado(p.preco) + ';' + (p.marca || '') + ';' +\r\n             fixado(p.qtdeMinima) + ';' + fixado(p.multiplo) + ';' + p.prazoDias + ';' + (p.obs || '');\r\n  }\r\n  return fnv1a32(texto);\r\n}\r\n\r\nfunction mostrarMensagem(texto, ok) {\r\n  var m = document.getElementById('mensagem');\r\n  m.textContent = texto;\r\n  m.className = ok ? 'ok' : 'erro';\r\n}\r\n\r\nfunction baixarResposta() {\r\n  try {\r\n    var resposta = montarResposta();\r\n    var json = JSON.stringify(resposta);\r\n    var blob = new Blob([json], { type: 'application/json;charset=utf-8' });\r\n    var link = document.createElement('a');\r\n    link.href = URL.createObjectURL(blob);\r\n    link.download = 'resposta-cotacao-' + DADOS.numero + '.json';\r\n    document.body.appendChild(link);\r\n    link.click();\r\n    document.body.removeChild(link);\r\n    mostrarMensagem('Arquivo gerado. Devolva-o respondendo a mensagem que você recebeu.', true);\r\n  } catch (e) { mostrarMensagem(e.message, false); }\r\n}\r\n\r\nfunction copiarResposta() {\r\n  try {\r\n    var resposta = montarResposta();\r\n    var json = JSON.stringify(resposta);\r\n    var base64 = btoa(unescape(encodeURIComponent(json)));\r\n    if (navigator.clipboard && navigator.clipboard.writeText) {\r\n      navigator.clipboard.writeText(base64).then(function () {\r\n        mostrarMensagem('Resposta copiada. Cole o conteúdo na resposta ao comprador.', true);\r\n      }, function () { copiarPorArea(base64); });\r\n      return;\r\n    }\r\n    copiarPorArea(base64);\r\n  } catch (e) { mostrarMensagem(e.message, false); }\r\n}\r\n\r\nfunction copiarPorArea(texto) {\r\n  var area = document.createElement('textarea');\r\n  area.value = texto;\r\n  area.setAttribute('readonly', '');\r\n  area.style.position = 'fixed';\r\n  area.style.top = '0';\r\n  area.style.left = '0';\r\n  area.style.opacity = '0';\r\n  document.body.appendChild(area);\r\n  area.select();\r\n  area.setSelectionRange(0, texto.length);\r\n  var ok = false;\r\n  try { ok = document.execCommand('copy'); } catch (e) { ok = false; }\r\n  document.body.removeChild(area);\r\n  mostrarMensagem(ok ? 'Resposta copiada. Cole o conteúdo na resposta ao comprador.'\r\n                     : 'Não foi possível copiar automaticamente. Use Gerar arquivo de resposta.', ok);\r\n}\r\n\r\nfunction ligarAjudas() {\r\n  document.addEventListener('click', function (e) {\r\n    var alvo = e.target;\r\n    while (alvo && alvo !== document.body && !(alvo.className && String(alvo.className).indexOf('ajuda') >= 0)) alvo = alvo.parentNode;\r\n    if (alvo === document.body) alvo = null;\r\n    var abertos = document.querySelectorAll('.ajuda.aberta');\r\n    for (var i = 0; i < abertos.length; i++) if (abertos[i] !== alvo) abertos[i].className = abertos[i].className.replace(' aberta', '');\r\n    if (alvo && String(alvo.className).indexOf('aberta') < 0) alvo.className += ' aberta';\r\n  });\r\n}\r\n\r\nmontarTela();\r\nligarAjudas();\r\n</script>\r\n</body>\r\n</html>".Replace("__DADOS__", newValue);
		}

		private static string ObterCodigoFornecedor(IDictionary<string, string> codigosFornecedor, string itemReferencia)
		{
			if (codigosFornecedor == null || itemReferencia == null || !codigosFornecedor.TryGetValue(itemReferencia, out var value))
			{
				return string.Empty;
			}
			return value;
		}
	}
	public static class GeradorPedidoCompraCotacao
	{
		private class GrupoPedido
		{
			public ParticipanteCotacaoDto Participante { get; set; }

			public List<ApuracaoItemDto> Apuracoes { get; set; }
		}

		public static bool PodeGerar(CotacaoDto cotacaoDto)
		{
			if (cotacaoDto != null && (cotacaoDto.Situacao.Codigo == 3 || cotacaoDto.Situacao.Codigo == 4))
			{
				return HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(cotacaoDto.Apuracoes).Any((ApuracaoItemDto x) => x.PossuiVencedor);
			}
			return false;
		}

		public static async Task<CotacaoDto> Gerar(IWService service, CotacaoDto cotacaoDto)
		{
			List<ApuracaoItemDto> source = (from x in HelperIEnumerable.ToListEmptyWhenNull<ApuracaoItemDto>(cotacaoDto.Apuracoes)
				where x.PossuiVencedor
				select x).ToList();
			if (!source.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum item da cotação possui fornecedor vencedor.", false, HorizontalAlignment.Center);
				return null;
			}
			List<ItemCotacaoDto> itensCotacao = HelperIEnumerable.ToListEmptyWhenNull<ItemCotacaoDto>(cotacaoDto.Itens);
			List<ItemCotacaoDto> source2 = (from x in source
				select itensCotacao.FirstOrDefault((ItemCotacaoDto item) => item.Id == x.ItemCotacaoReferencia) into item
				where item != null && item.IsItemLivre
				select item).ToList();
			if (source2.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Os itens abaixo foram digitados livremente e precisam ser vinculados a um produto do cadastro antes de gerar os pedidos (aba Itens, botão 'Vincular produto'):\n" + string.Join(Environment.NewLine, source2.Select((ItemCotacaoDto x) => x.Descricao)), false, HorizontalAlignment.Left);
				return null;
			}
			List<ParticipanteCotacaoDto> participantes = HelperIEnumerable.ToListEmptyWhenNull<ParticipanteCotacaoDto>(cotacaoDto.Participantes);
			List<GrupoPedido> list = (from x in source
				group x by x.ParticipanteVencedorReferencia into g
				select new GrupoPedido
				{
					Participante = participantes.FirstOrDefault((ParticipanteCotacaoDto p) => p.Id == g.Key),
					Apuracoes = g.ToList()
				} into g
				where g.Participante != null
				select g).ToList();
			string arg = string.Join(Environment.NewLine, list.Select((GrupoPedido g) => $"{g.Participante.Fornecedor.Nome}: {g.Apuracoes.Count} item(ns)"));
			List<string> list2 = ObterAjustesPorMultiplo(list, itensCotacao);
			string arg2 = (list2.Any() ? ("\n\nOs itens abaixo serão pedidos em quantidade maior, por causa do múltiplo de venda do fornecedor:\n" + string.Join(Environment.NewLine, list2)) : string.Empty);
			List<PedidoCompraGeradoDto> source3 = HelperIEnumerable.ToListEmptyWhenNull<PedidoCompraGeradoDto>(cotacaoDto.PedidosCompraGerados);
			string arg3 = (source3.Any() ? ("\n\nAtenção: esta cotação já gerou os pedidos de compra abaixo. Gerar novamente cria outros pedidos.\n" + string.Join(Environment.NewLine, source3.Select((PedidoCompraGeradoDto x) => $"{x.Numero} - {x.NomeFornecedor}"))) : string.Empty);
			if (DialogoFactory.MostrarDialogoSimNao($"Será gerado um pedido de compra para cada fornecedor vencedor:\n{arg}{arg2}{arg3}\n\nContinuar?", false, 0, HorizontalAlignment.Left) != MessageBoxResult.Yes)
			{
				return null;
			}
			List<PedidoCompraGeradoDto> pedidosGerados = new List<PedidoCompraGeradoDto>();
			try
			{
				LoaderProcessoService.MostrarLoader("Gerando pedidos de compra...");
				PessoaDto emitente = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService s) => s.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				foreach (GrupoPedido grupo in list)
				{
					MovimentacaoDto val = await GerarPedidoDoFornecedor(service, cotacaoDto, grupo.Participante, grupo.Apuracoes, itensCotacao, emitente);
					pedidosGerados.Add(new PedidoCompraGeradoDto
					{
						ParticipanteReferencia = grupo.Participante.Id,
						MovimentacaoReferencia = ((EntityBaseDto)val).Id,
						Numero = ((MovimentacaoBaseDto)val).Numero,
						NomeFornecedor = grupo.Participante.Fornecedor.Nome
					});
				}
				string observacao = "Pedidos de compra gerados: " + string.Join(", ", pedidosGerados.Select((PedidoCompraGeradoDto x) => $"{x.Numero} ({x.NomeFornecedor})"));
				bool estavaFechada = cotacaoDto.Situacao.Codigo == 4;
				CotacaoDto result = ((!estavaFechada) ? (await service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.FecharAsync(((EntityBaseDto)cotacaoDto).Id, (IEnumerable<PedidoCompraGeradoDto>)pedidosGerados, observacao)))) : (await service.Canal<ICotacaoModuleService, Task<CotacaoDto>>((Func<ICotacaoModuleService, Task<CotacaoDto>>)((ICotacaoModuleService s) => s.RegistrarPedidosGeradosAsync(((EntityBaseDto)cotacaoDto).Id, (IEnumerable<PedidoCompraGeradoDto>)pedidosGerados, observacao)))));
				LoaderProcessoService.EsconderLoader();
				string arg4 = string.Join(Environment.NewLine, pedidosGerados.Select((PedidoCompraGeradoDto x) => $"{x.Numero} - {x.NomeFornecedor}"));
				DialogoFactory.MostrarDialogoConfirma(estavaFechada ? $"{pedidosGerados.Count} pedido(s) de compra foram gerados:\n{arg4}" : $"A cotação foi fechada e {pedidosGerados.Count} pedido(s) de compra foram gerados:\n{arg4}", false, HorizontalAlignment.Left);
				return result;
			}
			catch (Exception ex)
			{
				LoaderProcessoService.EsconderLoader();
				string text = (pedidosGerados.Any() ? ("\nPedidos já criados antes da falha: " + string.Join(", ", pedidosGerados.Select((PedidoCompraGeradoDto x) => x.Numero.ToString())) + ". A situação da cotação não foi alterada — verifique-os antes de tentar novamente.") : string.Empty);
				HelperException.ShowLastException(ex, "Falha ao gerar os pedidos de compra." + text, false, false);
				return null;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private static List<string> ObterAjustesPorMultiplo(IEnumerable<GrupoPedido> grupos, List<ItemCotacaoDto> itensCotacao)
		{
			List<string> list = new List<string>();
			foreach (GrupoPedido grupo in grupos)
			{
				RespostaCotacaoDto respostaCorrente = grupo.Participante.RespostaCorrente;
				foreach (ApuracaoItemDto apuracao in grupo.Apuracoes)
				{
					ItemCotacaoDto item = itensCotacao.FirstOrDefault((ItemCotacaoDto x) => x.Id == apuracao.ItemCotacaoReferencia);
					if (item == null)
					{
						continue;
					}
					PropostaItemDto val = HelperIEnumerable.ToListEmptyWhenNull<PropostaItemDto>(respostaCorrente.Propostas).FirstOrDefault((PropostaItemDto x) => x.ItemCotacaoReferencia == item.Id);
					if (val != null && !(val.MultiploVenda <= 0m))
					{
						decimal num = Math.Ceiling(item.Quantidade / val.MultiploVenda) * val.MultiploVenda;
						if (!(num == item.Quantidade))
						{
							list.Add($"{item.Descricao} ({grupo.Participante.Fornecedor.Nome}): {item.Quantidade:N2} passa para {num:N2}");
						}
					}
				}
			}
			return list;
		}

		private static async Task<MovimentacaoDto> GerarPedidoDoFornecedor(IWService service, CotacaoDto cotacaoDto, ParticipanteCotacaoDto participante, List<ApuracaoItemDto> apuracoes, List<ItemCotacaoDto> itensCotacao, PessoaDto emitente)
		{
			PessoaDto fornecedor = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService s) => s.ObterPessoaPorId(participante.Fornecedor.PessoaReferencia)));
			MovimentacaoDto movimentacaoDto = new MovimentacaoDto((TipoMovimentacao)3);
			MovimentacaoBaseDto baseDto = default(MovimentacaoBaseDto);
			_ = baseDto;
			baseDto = await service.Canal<IMovimentacaoEntradaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoEntradaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoEntradaModuleService s) => ((IReferenciaAtualizarService<MovimentacaoBaseDto>)(object)s).AtualizarDtoAsync((MovimentacaoBaseDto)(object)movimentacaoDto)));
			baseDto.Pessoa = fornecedor;
			RespostaCotacaoDto respostaCorrente = participante.RespostaCorrente;
			int num = 0;
			List<ItemDto> itensDto = new List<ItemDto>();
			foreach (ApuracaoItemDto apuracao in apuracoes)
			{
				ItemCotacaoDto itemCotacao = itensCotacao.FirstOrDefault((ItemCotacaoDto x) => x.Id == apuracao.ItemCotacaoReferencia);
				if (itemCotacao != null)
				{
					PropostaItemDto val = HelperIEnumerable.ToListEmptyWhenNull<PropostaItemDto>(respostaCorrente.Propostas).FirstOrDefault((PropostaItemDto x) => x.ItemCotacaoReferencia == itemCotacao.Id);
					decimal num2 = itemCotacao.Quantidade;
					if (val != null && val.MultiploVenda > 0m)
					{
						num2 = Math.Ceiling(num2 / val.MultiploVenda) * val.MultiploVenda;
					}
					int num3 = ((val != null) ? ((val.PrazoEntregaDias > 0) ? val.PrazoEntregaDias : ((respostaCorrente != null) ? respostaCorrente.PrazoEntregaDias : 0)) : 0);
					if (num3 > num)
					{
						num = num3;
					}
					ProdutoServicoEmpresaDto val2 = service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService s) => s.ObterProdutoServicoEmpresaPorId(new string[1] { itemCotacao.ProdutoServicoEmpresaReferencia }))).FirstOrDefault();
					if (HelperObject.IsNull((object)val2))
					{
						throw new Exception($"O produto do item {itemCotacao.Descricao} não foi encontrado no cadastro.");
					}
					ItemDto val3 = CriarItemPedido(service, val2, itemCotacao, num2, apuracao.PrecoUnitario, fornecedor, emitente, baseDto.CalculadoPeloSistema);
					if (HelperObject.Is<ItemDtoNull>((object)val3))
					{
						throw new Exception($"Não foi possível montar o item {itemCotacao.Descricao} do pedido de compra.");
					}
					val3.Numero = itensDto.Count + 1;
					itensDto.Add(val3);
				}
			}
			int prazoEntrega = num;
			HelperObject.ToDo<MovimentacaoDto>((object)baseDto, (Action<MovimentacaoDto>)delegate(MovimentacaoDto x)
			{
				x.Itens = itensDto;
				x.Observacao = $"Pedido de compra gerado a partir da cotação número {cotacaoDto.Numero} - {cotacaoDto.Descricao}";
				if (HelperObject.IsNot<FormaPagamentoDtoNull>((object)fornecedor.Fornecedor.FormaPagamento))
				{
					x.FormaPagamento = fornecedor.Fornecedor.FormaPagamento;
				}
				if (prazoEntrega > 0)
				{
					x.DataHoraPrevisao = DateTime.Now.AddDays(prazoEntrega);
				}
			});
			return await service.Canal<IPedidoCompraModuleService, Task<MovimentacaoDto>>((Func<IPedidoCompraModuleService, Task<MovimentacaoDto>>)((IPedidoCompraModuleService s) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)s).AdicionarAsync((MovimentacaoDto)baseDto)));
		}

		private static ItemDto CriarItemPedido(IWService service, ProdutoServicoEmpresaDto produtoEmpresa, ItemCotacaoDto itemCotacao, decimal quantidade, decimal precoUnitario, PessoaDto fornecedor, PessoaDto emitente, bool calculadoPeloSistema)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected O, but got Unknown
			ItemDto itemDto = new ItemDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)produtoEmpresa).Id,
				PrecoUnitarioInformado = precoUnitario,
				QuantidadeInformada = quantidade,
				Estoque = MontarEstoquePedido(produtoEmpresa, itemCotacao, quantidade),
				CondicaoEspecial = produtoEmpresa.CondicaoEspecial,
				LimiteDesconto = produtoEmpresa.LimiteDesconto,
				CalculadoPeloSistema = calculadoPeloSistema,
				PessoaMovimentacao = fornecedor,
				EmpresaMovimentacao = emitente,
				QuantidadeVendidaMovimentacao = quantidade,
				DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia,
				TipoMovimentacao = (TipoMovimentacao)3
			};
			return service.Canal<IMovimentacaoEntradaModuleService, Task<ItemDto>>((Func<IMovimentacaoEntradaModuleService, Task<ItemDto>>)((IMovimentacaoEntradaModuleService s) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)s).CriarItemMovimentacao(itemDto))).Result;
		}

		private static EstoqueDto MontarEstoquePedido(ProdutoServicoEmpresaDto produtoEmpresa, ItemCotacaoDto itemCotacao, decimal quantidade)
		{
			Dictionary<string, decimal> dictionary = (from x in HelperIEnumerable.ToListEmptyWhenNull<GradeItemCotacaoDto>(itemCotacao.Grades)
				where x.Quantidade > 0m
				select x).ToDictionary((GradeItemCotacaoDto x) => x.EstoqueReferencia, (GradeItemCotacaoDto x) => x.Quantidade);
			return EstoquePedidoCompra.Montar(produtoEmpresa, (IDictionary<string, decimal>)dictionary, quantidade);
		}
	}
	public class ArquivoRespostaCotacao
	{
		public int versao { get; set; }

		public string tipo { get; set; }

		public string token { get; set; }

		public long cotacao { get; set; }

		public string fornecedor { get; set; }

		public int prazoEntregaDias { get; set; }

		public int condicaoPagamentoDias { get; set; }

		public decimal valorFrete { get; set; }

		public int tipoFrete { get; set; }

		public decimal valorOutrosCustos { get; set; }

		public string observacao { get; set; }

		public List<PropostaRespostaCotacao> propostas { get; set; }

		public string hash { get; set; }
	}
	public class PropostaRespostaCotacao
	{
		public string item { get; set; }

		public bool disponivel { get; set; }

		public decimal preco { get; set; }

		public string marca { get; set; }

		public decimal qtdeMinima { get; set; }

		public decimal multiplo { get; set; }

		public int prazoDias { get; set; }

		public string obs { get; set; }
	}
	public class RespostaImportada
	{
		public string Token { get; set; }

		public string NomeFornecedor { get; set; }

		public long NumeroCotacao { get; set; }

		public RespostaCotacaoDto Resposta { get; set; }
	}
	public static class RespostaCotacaoCodec
	{
		public const int VersaoAtual = 2;

		public const string TipoArquivo = "resposta-cotacao-digisat";

		public static RespostaImportada Interpretar(string conteudo)
		{
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Expected O, but got Unknown
			if (string.IsNullOrWhiteSpace(conteudo))
			{
				throw new Exception("Nenhum conteúdo de resposta foi informado.");
			}
			string text = conteudo.Trim();
			if (!text.StartsWith("{"))
			{
				try
				{
					text = Encoding.UTF8.GetString(Convert.FromBase64String(text)).Trim();
				}
				catch (FormatException)
				{
					throw new Exception("O conteúdo colado não é uma resposta de cotação válida.");
				}
			}
			ArquivoRespostaCotacao arquivoRespostaCotacao;
			try
			{
				arquivoRespostaCotacao = JsonConvert.DeserializeObject<ArquivoRespostaCotacao>(text);
			}
			catch (Exception)
			{
				throw new Exception("O conteúdo informado não é uma resposta de cotação válida.");
			}
			if (arquivoRespostaCotacao == null || arquivoRespostaCotacao.tipo != "resposta-cotacao-digisat")
			{
				throw new Exception("O conteúdo informado não é uma resposta de cotação deste sistema.");
			}
			if (arquivoRespostaCotacao.versao != 2)
			{
				throw new Exception($"A versão do arquivo de resposta ({arquivoRespostaCotacao.versao}) não é suportada por esta versão do sistema.");
			}
			if (string.IsNullOrWhiteSpace(arquivoRespostaCotacao.token))
			{
				throw new Exception("A resposta não possui o token de acesso do fornecedor.");
			}
			if (arquivoRespostaCotacao.propostas == null || !arquivoRespostaCotacao.propostas.Any())
			{
				throw new Exception("A resposta não possui nenhum item.");
			}
			if (!string.Equals(CalcularHash(arquivoRespostaCotacao), arquivoRespostaCotacao.hash, StringComparison.OrdinalIgnoreCase))
			{
				throw new Exception("A resposta está corrompida ou foi alterada após ser gerada. Solicite ao fornecedor um novo arquivo.");
			}
			return new RespostaImportada
			{
				Token = arquivoRespostaCotacao.token,
				NomeFornecedor = arquivoRespostaCotacao.fornecedor,
				NumeroCotacao = arquivoRespostaCotacao.cotacao,
				Resposta = new RespostaCotacaoDto
				{
					DataHoraResposta = DateTime.Now,
					PrazoEntregaDias = arquivoRespostaCotacao.prazoEntregaDias,
					CondicaoPagamentoDias = arquivoRespostaCotacao.condicaoPagamentoDias,
					ValorFrete = arquivoRespostaCotacao.valorFrete,
					TipoFrete = (TipoFreteCotacao)arquivoRespostaCotacao.tipoFrete,
					ValorOutrosCustos = arquivoRespostaCotacao.valorOutrosCustos,
					Observacao = arquivoRespostaCotacao.observacao,
					Propostas = ((IEnumerable<PropostaRespostaCotacao>)arquivoRespostaCotacao.propostas).Select((Func<PropostaRespostaCotacao, PropostaItemDto>)((PropostaRespostaCotacao x) => new PropostaItemDto
					{
						ItemCotacaoReferencia = x.item,
						Disponivel = x.disponivel,
						PrecoUnitario = x.preco,
						MarcaOfertada = x.marca,
						QuantidadeMinima = x.qtdeMinima,
						MultiploVenda = x.multiplo,
						PrazoEntregaDias = x.prazoDias,
						Observacao = x.obs
					})).ToList()
				}
			};
		}

		public static string CalcularHash(ArquivoRespostaCotacao arquivo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(arquivo.token).Append('|').Append(arquivo.prazoEntregaDias)
				.Append('|')
				.Append(arquivo.condicaoPagamentoDias)
				.Append('|')
				.Append(FormatarDecimal(arquivo.valorFrete))
				.Append('|')
				.Append(arquivo.tipoFrete)
				.Append('|')
				.Append(FormatarDecimal(arquivo.valorOutrosCustos))
				.Append('|')
				.Append(arquivo.observacao ?? string.Empty);
			foreach (PropostaRespostaCotacao item in arquivo.propostas.OrderBy((PropostaRespostaCotacao x) => x.item, StringComparer.Ordinal))
			{
				stringBuilder.Append('|').Append(item.item).Append(';')
					.Append(item.disponivel ? "1" : "0")
					.Append(';')
					.Append(FormatarDecimal(item.preco))
					.Append(';')
					.Append(item.marca ?? string.Empty)
					.Append(';')
					.Append(FormatarDecimal(item.qtdeMinima))
					.Append(';')
					.Append(FormatarDecimal(item.multiplo))
					.Append(';')
					.Append(item.prazoDias)
					.Append(';')
					.Append(item.obs ?? string.Empty);
			}
			return CalcularFnv1a32(stringBuilder.ToString());
		}

		public static string CalcularFnv1a32(string texto)
		{
			uint num = 2166136261u;
			byte[] bytes = Encoding.UTF8.GetBytes(texto);
			foreach (byte b in bytes)
			{
				num ^= b;
				num *= 16777619;
			}
			return num.ToString("x8");
		}

		private static string FormatarDecimal(decimal valor)
		{
			return valor.ToString("0.0000", CultureInfo.InvariantCulture);
		}
	}
}

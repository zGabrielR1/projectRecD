using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel;
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
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Cadastros.ConsultasProduto;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.File;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ParametrizaSoft;
using DigisatServer.Infrastructure.Crosscutting.Service.ParametrizaSoft.Classes.Base;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.CadastrosVO.SituacoesTributaria;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.ConsultasProduto.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.ConsultasProduto")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.ConsultasProduto")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.ConsultasProduto
{
	[Module(ModuleName = "ConsultaProdutoModulo", OnDemand = true)]
	public class ConsultaProdutoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ConsultaProdutoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ConsultasProdutoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ConsultaProdutoFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.ConsultasProduto.Views
{
	public class ConsultaProdutoFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultaProdutoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultaprodutofichaview.xaml", UriKind.Relative);
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
	public class ConsultasProdutoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultasProdutoView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultasprodutoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ConsultasProduto.Views.ConsultaProdutoFichaViewDetalhes
{
	public class EnviadosParaAnaliseView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EnviadosParaAnaliseView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultaprodutofichaviewdetalhes/enviadosparaanaliseview.xaml", UriKind.Relative);
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
	public class SelecaoProdutoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SelecaoProdutoView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultaprodutofichaviewdetalhes/selecaoprodutoview.xaml", UriKind.Relative);
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
	public class ConsultaProdutosPendentesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultaProdutosPendentesView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultaprodutofichaviewdetalhes/consultaprodutospendentesview.xaml", UriKind.Relative);
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
	public class SelecionadosComCodigoBarrasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SelecionadosComCodigoBarrasView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProduto;component/views/consultaprodutofichaviewdetalhes/selecionadoscomcodigobarrasview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ConsultasProduto.ViewModels
{
	public class ConsultaProduto : BindableBase
	{
		public ConsultaProdutoDto Dto { get; set; }

		public string NcmApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NcmApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NcmApi), value, (Action)delegate
				{
					Dto.NcmApi = value;
				});
			}
		}

		public string CfopApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CfopApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CfopApi), value, (Action)delegate
				{
					Dto.CfopApi = value;
				});
			}
		}

		public string CestApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CestApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CestApi), value, (Action)delegate
				{
					Dto.CestApi = value;
				});
			}
		}

		public string CstIcmsApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CstIcmsApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CstIcmsApi), value, (Action)delegate
				{
					Dto.CstIcmsApi = HelperString.ToInt(value);
				});
			}
		}

		public decimal AliquotaIcmsApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaIcmsApi));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaIcmsApi), value, (Action)delegate
				{
					Dto.AliquotaIcmsApi = value;
				});
			}
		}

		public string CstPisApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CstPisApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CstPisApi), value, (Action)delegate
				{
					Dto.CstPisApi = HelperString.ToInt(value);
				});
			}
		}

		public decimal AliquotaPisApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaPisApi));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaPisApi), value, (Action)delegate
				{
					Dto.AliquotaPisApi = value;
				});
			}
		}

		public string CstCofinsApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CstCofinsApi));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CstCofinsApi), value, (Action)delegate
				{
					Dto.CstCofinsApi = HelperString.ToInt(value);
				});
			}
		}

		public decimal AliquotaCofinsApi
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaCofinsApi));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => AliquotaCofinsApi), value, (Action)delegate
				{
					Dto.AliquotaCofinsApi = value;
				});
			}
		}

		public string Mensagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Mensagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Mensagem), value);
			}
		}

		public bool PossuiDivergencia
		{
			get
			{
				//IL_0095: Unknown result type (might be due to invalid IL or missing references)
				//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
				if (!(Dto.NcmBase != NcmApi) && !(Dto.CfopBase != CfopApi) && !(Dto.CestBase != CestApi) && !(Dto.CstIcmsBase.ToString() != CstIcmsApi) && !(Dto.AliquotaIcmsBase != AliquotaIcmsApi) && Dto.TributacaoEcfBase == SituacaoTributariaValueObjectService.ObterPorCodigo(HelperString.ToInt(CstIcmsApi)).Tipo && !(Dto.AliquotaIcmsEcfBase != AliquotaIcmsApi) && !(Dto.CstPisBase.ToString() != CstPisApi) && !(Dto.AliquotaPisBase != AliquotaPisApi) && !(Dto.CstCofinsBase.ToString() != CstCofinsApi))
				{
					return Dto.AliquotaCofinsBase != AliquotaCofinsApi;
				}
				return true;
			}
		}

		public ConsultaProduto()
		{
		}

		public ConsultaProduto(ConsultaProdutoDto consultaProduto)
		{
			Dto = consultaProduto;
			NcmApi = string.Empty;
			CfopApi = string.Empty;
			CestApi = string.Empty;
			CstIcmsApi = string.Empty;
			AliquotaIcmsApi = 0m;
			CstPisApi = string.Empty;
			AliquotaPisApi = 0m;
			CstCofinsApi = string.Empty;
			AliquotaCofinsApi = 0m;
		}
	}
	public class ConsultaProdutoFichaViewModel : FichaBase<ConsultaProduto, SelecaoProdutoViewModel>
	{
		private readonly IWService _service;

		private readonly IParametrizaSoftClientService _parametrizaSoftClientService;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ICommand AvancarCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => AvancarCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => AvancarCommand), value);
			}
		}

		public ICommand ConsultarProdutosPendentesCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosPendentesCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosPendentesCommand), value);
			}
		}

		public ICommand ConsultarProdutosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosCommand), value);
			}
		}

		public ICommand ProcessarProdutosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ProcessarProdutosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ProcessarProdutosCommand), value);
			}
		}

		public ICommand ImprimirCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ImprimirCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ImprimirCommand), value);
			}
		}

		public ConsultaProdutoFichaViewModel(IWService service, IParametrizaSoftClientService parametrizaSoftClientService, IImpressaoRelatorioService impressaoRelatorioService)
			: base(string.Empty)
		{
			_service = service;
			_parametrizaSoftClientService = parametrizaSoftClientService;
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		protected override async void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)((FichaBase<ConsultaProduto, SelecaoProdutoViewModel>)this).Formulario))
			{
				return;
			}
			ThreadPrincipal.Executar((Action)delegate
			{
				LoaderProcessoService.MostrarLoader("Obtendo produtos...");
			});
			await _service.Canal<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoDto>>>((Func<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoDto>>>)((IProdutoServicoModuleService service) => service.ObterConsultasProduto())).ContinueWith((Func<Task<IEnumerable<ConsultaProdutoDto>>, Task>)async delegate(Task<IEnumerable<ConsultaProdutoDto>> task)
			{
				IEnumerable<ConsultaProdutoDto> produtos = task.Result;
				if (produtos.Any())
				{
					await _parametrizaSoftClientService.CriarApi().ContinueWith(delegate(Task<ApiParametrizaSoftService> taskApi)
					{
						//IL_008a: Unknown result type (might be due to invalid IL or missing references)
						//IL_0094: Expected O, but got Unknown
						//IL_0101: Unknown result type (might be due to invalid IL or missing references)
						//IL_010b: Expected O, but got Unknown
						//IL_013d: Unknown result type (might be due to invalid IL or missing references)
						//IL_0147: Expected O, but got Unknown
						if (taskApi.IsFaulted)
						{
							HelperException.ShowLastException(taskApi.Exception, "", false, false);
							ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
						}
						else
						{
							ApiParametrizaSoftService result = taskApi.Result;
							ConsultaProdutoFormulario formulario = new ConsultaProdutoFormulario(produtos, result);
							ConsultarProdutosPendentesCommand = (ICommand)new DelegateCommand((Action)formulario.ConsultarProdutosPendentes, (Func<bool>)formulario.PodeConsultarProdutosPendentes, (bool?)null);
							AvancarCommand = (ICommand)new DelegateCommand<object>((Action<object>)formulario.Avancar, (Func<object, bool>)formulario.PodeAvancar, (bool?)null);
							ConsultarProdutosCommand = (ICommand)new AsyncCommand((Func<Task>)formulario.ConsultarProdutos, (Func<bool>)formulario.PodeConsultarProdutos, false, (bool?)null);
							ProcessarProdutosCommand = (ICommand)new AsyncCommand((Func<Task>)formulario.ProcessarProdutos, (Func<bool>)formulario.PodeProcessarProdutos, false, (bool?)null);
							ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)delegate(object o)
							{
								formulario.Imprimir(o, _impressaoRelatorioService);
							}, (Func<object, bool>)formulario.PodeImprimir, (bool?)null);
							base.Formulario = (IFormulario)(object)formulario;
						}
					});
				}
				else
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto localizado para consulta", false, HorizontalAlignment.Center);
					ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
				}
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			});
		}
	}
	public class ConsultaProdutoFormulario : Formulario
	{
		private readonly ApiParametrizaSoftService _apiParametrizaSoftService;

		private readonly SelecaoProdutoViewModel _selecaoProdutoViewModel;

		private readonly SelecaoProdutoAbaViewModel _selecaoProdutoAbaViewModel;

		private SelecionadosComCodigoBarrasAbaViewModel _selecionadosComCodigoBarrasAbaViewModel;

		private SelecionadosComCodigoBarrasViewModel _selecionadosComCodigoBarrasViewModel;

		private SelecionadosSemCodigoBarrasViewModel _selecionadosSemCodigoBarrasViewModel;

		private SelecionadosSemCodigoBarrasAbaViewModel _selecionadosSemCodigoBarrasAbaViewModel;

		private IEnumerable<ConsultaProdutoDto> _produtosSelecionados;

		private bool _terminouConsultaComCodigoBarras;

		private bool _terminouConsultaSemCodigoBarras;

		private bool _terminouConsultaProdutosPendentes;

		private EnviadosParaAnaliseViewModel _enviadosParaAnaliseViewModel;

		private EnviadosParaAnaliseAbaViewModel _enviadosParaAnaliseAbaViewModel;

		private bool _processouConsultaComCodigoBarras;

		private bool _processouConsultaSemCodigoBarras;

		private ConsultaProdutoPendenteAbaViewModel _consultaProdutoPendenteAbaViewModel;

		private ConsultaProdutoPendenteViewModel _consultaProdutoPendenteViewModel;

		public ConsultaProdutoFormulario(IEnumerable<ConsultaProdutoDto> produtos, ApiParametrizaSoftService apiApiParametrizaSoftService)
		{
			_apiParametrizaSoftService = apiApiParametrizaSoftService;
			_selecaoProdutoViewModel = new SelecaoProdutoViewModel(produtos);
			_selecaoProdutoAbaViewModel = new SelecaoProdutoAbaViewModel((IViewModelDtoBase)(object)_selecaoProdutoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)_selecaoProdutoAbaViewModel });
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		public bool PodeConsultarProdutosPendentes()
		{
			return HelperObject.Is<SelecaoProdutoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public void ConsultarProdutosPendentes()
		{
			CriarAbaConsultaProdutosPendentes();
		}

		private void CriarAbaConsultaProdutosPendentes()
		{
			if (((Formulario)this).Abas.Any((IAbaViewModelBase x) => HelperObject.Is<ConsultaProdutoPendenteViewModel>((object)x.ViewModel)))
			{
				((Formulario)this).PosicionarNaUltimaAba();
				return;
			}
			_consultaProdutoPendenteViewModel = new ConsultaProdutoPendenteViewModel(_apiParametrizaSoftService);
			_consultaProdutoPendenteAbaViewModel = new ConsultaProdutoPendenteAbaViewModel((IViewModelDtoBase)(object)_consultaProdutoPendenteViewModel);
			((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_consultaProdutoPendenteAbaViewModel);
			((Formulario)this).PosicionarNaUltimaAba();
		}

		public void Avancar(object obj)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Atenção! É recomendado realizar um backup da base de dados antes de prosseguir, uma vez que este processo é irreversível. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			((Formulario)this).RemoverAba((IAbaViewModelBase)(object)_consultaProdutoPendenteAbaViewModel);
			_produtosSelecionados = _selecaoProdutoViewModel.RetornarItensSelecionados(obj);
			if (_produtosSelecionados.Any())
			{
				((AbaAuxiliarViewModelBase)_selecaoProdutoAbaViewModel).Desativar();
				IEnumerable<ConsultaProdutoDto> enumerable = _produtosSelecionados.Where((ConsultaProdutoDto x) => !string.IsNullOrEmpty(x.CodigoBarras));
				if (enumerable.Any())
				{
					_selecionadosComCodigoBarrasViewModel = new SelecionadosComCodigoBarrasViewModel(enumerable, _apiParametrizaSoftService);
					_selecionadosComCodigoBarrasAbaViewModel = new SelecionadosComCodigoBarrasAbaViewModel((IViewModelDtoBase)(object)_selecionadosComCodigoBarrasViewModel);
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_selecionadosComCodigoBarrasAbaViewModel);
					((Formulario)this).PosicionarNaUltimaAba();
				}
				else
				{
					CriarAbaProdutosSemCodigoBarras();
				}
			}
		}

		private void CriarAbaProdutosSemCodigoBarras(IEnumerable<ConsultaProdutoDto> produtosNaoConsultadosViaCodigoBarras = null)
		{
			IEnumerable<ConsultaProdutoDto> enumerable = _produtosSelecionados.Where((ConsultaProdutoDto x) => string.IsNullOrEmpty(x.CodigoBarras));
			if (HelperObject.IsNotNull((object)produtosNaoConsultadosViaCodigoBarras))
			{
				enumerable = enumerable.Union(produtosNaoConsultadosViaCodigoBarras);
			}
			if (enumerable.Any())
			{
				_selecionadosSemCodigoBarrasViewModel = new SelecionadosSemCodigoBarrasViewModel(enumerable, _apiParametrizaSoftService);
				_selecionadosSemCodigoBarrasAbaViewModel = new SelecionadosSemCodigoBarrasAbaViewModel((IViewModelDtoBase)(object)_selecionadosSemCodigoBarrasViewModel);
				((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_selecionadosSemCodigoBarrasAbaViewModel);
				((Formulario)this).PosicionarNaUltimaAba();
			}
		}

		public bool PodeAvancar(object obj)
		{
			if (HelperObject.IsNotNull(obj) && HelperObject.Is<SelecaoProdutoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				return ((ViewModelValidationBase)_selecaoProdutoViewModel).Valido;
			}
			return false;
		}

		public async Task ConsultarProdutos()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				LoaderProcessoService.MostrarLoader("Processando...");
			});
			if (HelperObject.Is<SelecionadosComCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				await _selecionadosComCodigoBarrasViewModel.Consultar().ContinueWith(delegate
				{
					LoaderProcessoService.EsconderLoader();
					_terminouConsultaComCodigoBarras = true;
					DialogoFactory.MostrarDialogoConfirma("Consulte as divergências encontradas e clique em 'F7 - Processar' para atualizar os produtos", false, HorizontalAlignment.Center);
				});
			}
			else if (HelperObject.Is<SelecionadosSemCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Processando...");
				});
				await _selecionadosSemCodigoBarrasViewModel.Consultar().ContinueWith(delegate
				{
					LoaderProcessoService.EsconderLoader();
					_terminouConsultaSemCodigoBarras = true;
					DialogoFactory.MostrarDialogoConfirma("Consulte as divergências encontradas e clique em 'F7 - Processar' para atualizar os produtos", false, HorizontalAlignment.Center);
				});
			}
			else if (HelperObject.Is<ConsultaProdutoPendenteViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				await _consultaProdutoPendenteViewModel.Consultar().ContinueWith(delegate
				{
					LoaderProcessoService.EsconderLoader();
					_terminouConsultaProdutosPendentes = true;
				});
			}
		}

		public bool PodeConsultarProdutos()
		{
			if ((!HelperObject.Is<SelecionadosComCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) || !((AbaViewModelBase)_selecionadosComCodigoBarrasAbaViewModel).Valido || _terminouConsultaComCodigoBarras) && (!HelperObject.Is<SelecionadosSemCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) || !((AbaViewModelBase)_selecionadosSemCodigoBarrasAbaViewModel).Valido || _terminouConsultaSemCodigoBarras))
			{
				if (HelperObject.Is<ConsultaProdutoPendenteViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
				{
					return !_terminouConsultaProdutosPendentes;
				}
				return false;
			}
			return true;
		}

		public bool PodeProcessarProdutos()
		{
			if ((!DadosSistema.VerificarPermissao<ConsultaProduto, Atualizar>() || !HelperObject.Is<SelecionadosComCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) || !_terminouConsultaComCodigoBarras || _processouConsultaComCodigoBarras) && (!HelperObject.Is<SelecionadosSemCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) || !_terminouConsultaSemCodigoBarras || _processouConsultaSemCodigoBarras))
			{
				if (HelperObject.Is<ConsultaProdutoPendenteViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
				{
					return _terminouConsultaProdutosPendentes;
				}
				return false;
			}
			return true;
		}

		public async Task ProcessarProdutos()
		{
			if (HelperObject.Is<SelecionadosComCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				IEnumerable<ConsultaProdutoDto> listaProdutosParaAtualizar3 = _selecionadosComCodigoBarrasViewModel.ObterProdutosPodeAtualizar();
				if (listaProdutosParaAtualizar3.Any())
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.MostrarLoader("Processando...");
					});
					IWService service = DadosSistema.GetService();
					try
					{
						await service.Canal<IParametrizaSoftModuleService, Task<IEnumerable<string>>>((Func<IParametrizaSoftModuleService, Task<IEnumerable<string>>>)((IParametrizaSoftModuleService x) => x.AtualizarProdutosConsultados(listaProdutosParaAtualizar3))).ContinueWith(delegate(Task<IEnumerable<string>> task)
						{
							if (task.IsFaulted)
							{
								HelperException.ShowLastException(task.Exception, "", false, false);
							}
							if (task.Result.Any())
							{
								string arg3 = SalvarArquivoDeErros(task.Result);
								DialogoFactory.MostrarDialogoConfirma($"Ocorreram erros durante o processo. \r\nVerifique o arquivo {arg3} na área de trabalho", false, HorizontalAlignment.Center);
							}
							else
							{
								DialogoFactory.MostrarDialogoConfirma("Os produtos com código de barras foram atualizados", false, HorizontalAlignment.Center);
							}
							_processouConsultaComCodigoBarras = true;
						});
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
				else
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto apresentou divergência na consulta", false, HorizontalAlignment.Center);
				}
				((AbaAuxiliarViewModelBase)_selecionadosComCodigoBarrasAbaViewModel).Desativar();
				IEnumerable<ConsultaProdutoDto> produtosNaoConsultadosViaCodigoBarras = _selecionadosComCodigoBarrasViewModel.ObterProdutosNaoConsultados();
				CriarAbaProdutosSemCodigoBarras(produtosNaoConsultadosViaCodigoBarras);
			}
			else if (HelperObject.Is<SelecionadosSemCodigoBarrasViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				IEnumerable<ConsultaProdutoDto> listaProdutosParaAtualizar2 = _selecionadosSemCodigoBarrasViewModel.ObterProdutosPodeAtualizar();
				string resumo = string.Empty;
				if (listaProdutosParaAtualizar2.Any())
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.MostrarLoader("Processando...");
					});
					IWService service2 = DadosSistema.GetService();
					try
					{
						await service2.Canal<IParametrizaSoftModuleService, Task<IEnumerable<string>>>((Func<IParametrizaSoftModuleService, Task<IEnumerable<string>>>)((IParametrizaSoftModuleService x) => x.AtualizarProdutosConsultados(listaProdutosParaAtualizar2))).ContinueWith(delegate(Task<IEnumerable<string>> task)
						{
							if (task.IsFaulted)
							{
								HelperException.ShowLastException(task.Exception, "", false, false);
							}
							if (task.Result.Any())
							{
								string arg2 = SalvarArquivoDeErros(task.Result);
								DialogoFactory.MostrarDialogoConfirma($"Ocorreram erros durante o processo. \r\nVerifique o arquivo {arg2} na área de trabalho", false, HorizontalAlignment.Center);
							}
							else
							{
								if (!string.IsNullOrEmpty(resumo))
								{
									resumo += Environment.NewLine;
								}
								resumo = ((listaProdutosParaAtualizar2.Count() == 1) ? "1 produto foi atualizado" : $"{listaProdutosParaAtualizar2.Count()} produtos foram atualizados");
							}
							_processouConsultaSemCodigoBarras = true;
						});
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
				IEnumerable<ConsultaProdutoDto> source = _selecionadosSemCodigoBarrasViewModel.ObterProdutosConsultaProibida();
				if (source.Any())
				{
					if (!string.IsNullOrEmpty(resumo))
					{
						resumo += Environment.NewLine;
					}
					resumo += ((source.Count() == 1) ? "1 produto foi aguarda correção" : $"{source.Count()} produtos aguardam correção");
				}
				((AbaAuxiliarViewModelBase)_selecionadosSemCodigoBarrasAbaViewModel).Desativar();
				IEnumerable<ConsultaProdutoDto> enumerable = _selecionadosSemCodigoBarrasViewModel.ObterProdutosEnviadosParaAnalise();
				if (enumerable.Any())
				{
					if (!string.IsNullOrEmpty(resumo))
					{
						resumo += Environment.NewLine;
					}
					resumo += ((enumerable.Count() == 1) ? "1 produto foi enviado para análise" : $"{enumerable.Count()} produtos foram enviados para análise");
					_enviadosParaAnaliseViewModel = new EnviadosParaAnaliseViewModel(enumerable);
					_enviadosParaAnaliseAbaViewModel = new EnviadosParaAnaliseAbaViewModel((IViewModelDtoBase)(object)_enviadosParaAnaliseViewModel);
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_enviadosParaAnaliseAbaViewModel);
					((Formulario)this).PosicionarNaUltimaAba();
				}
				if (!string.IsNullOrEmpty(resumo))
				{
					DialogoFactory.MostrarDialogoConfirma(resumo, false, HorizontalAlignment.Center);
				}
			}
			else
			{
				if (!HelperObject.Is<ConsultaProdutoPendenteViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
				{
					return;
				}
				IEnumerable<ConsultaProdutoDto> listaProdutosParaAtualizar = _consultaProdutoPendenteViewModel.ObterProdutosPodeAtualizar();
				if (listaProdutosParaAtualizar.Any())
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.MostrarLoader("Processando...");
					});
					IWService service3 = DadosSistema.GetService();
					try
					{
						await service3.Canal<IParametrizaSoftModuleService, Task<IEnumerable<string>>>((Func<IParametrizaSoftModuleService, Task<IEnumerable<string>>>)((IParametrizaSoftModuleService x) => x.AtualizarProdutosConsultados(listaProdutosParaAtualizar))).ContinueWith(delegate(Task<IEnumerable<string>> task)
						{
							if (task.IsFaulted)
							{
								HelperException.ShowLastException(task.Exception, "", false, false);
							}
							if (task.Result.Any())
							{
								string arg = SalvarArquivoDeErros(task.Result);
								DialogoFactory.MostrarDialogoConfirma($"Ocorreram erros durante o processo. \r\nVerifique o arquivo {arg} na área de trabalho", false, HorizontalAlignment.Center);
							}
							else
							{
								DialogoFactory.MostrarDialogoConfirma("Os produtos que estavam pendentes foram atualizados", false, HorizontalAlignment.Center);
							}
						});
						return;
					}
					catch (Exception ex3)
					{
						HelperException.ShowLastException(ex3, "", false, false);
						return;
					}
					finally
					{
						LoaderProcessoService.EsconderLoader();
					}
				}
				DialogoFactory.MostrarDialogoConfirma("A correção dos produtos pendentes ainda não foi disponibilizada", false, HorizontalAlignment.Center);
			}
		}

		private string SalvarArquivoDeErros(IEnumerable<string> listaErros)
		{
			string text = $"ListaErrosConsultaProduto_{HelperDate.FormatarData(DateTime.Now, (FormatoData)1)}.txt";
			TxtFile.Criar().ComConteudo(listaErros).ComNomeArquivo(text)
				.ComDiretorioArquivo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
				.Build()
				.Gerar();
			return text;
		}

		public bool PodeImprimir(object obj)
		{
			return HelperObject.Is<EnviadosParaAnaliseViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public void Imprimir(object obj, IImpressaoRelatorioService impressaoRelatorioService)
		{
			impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)_enviadosParaAnaliseViewModel, obj as FrameworkElement, (string)null);
		}
	}
	public class ConsultasProdutoViewModel : ModuloBase
	{
		public ConsultasProdutoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloConsultasProdutoRegion, "ConsultaProdutoFichaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class EnviadosParaAnaliseAbaViewModel : AbaAuxiliarViewModelBase
	{
		public EnviadosParaAnaliseAbaViewModel(IViewModelDtoBase viewModel)
			: base("RESUMO", "Enviados para análise", viewModel)
		{
		}
	}
	public class EnviadosParaAnaliseViewModel : ViewModelDtoBase<IEnumerable<ConsultaProduto>>, IViewModelImprimirGrid, IViewModelImprimir
	{
		public ObservableCollection<ConsultaProduto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista), value);
			}
		}

		public override bool Valido => true;

		public string TituloRelatorio => "Produtos enviados para análise";

		public string SubTituloRelatorio => $"Quantidade de registros: {Lista.Count}";

		public bool Retrato => false;

		public string TagProperty => "ResumoEnviadosParaAnalise";

		public EnviadosParaAnaliseViewModel(IEnumerable<ConsultaProdutoDto> produtos)
			: base(produtos.Select((ConsultaProdutoDto x) => new ConsultaProduto(x)))
		{
			Lista = new ObservableCollection<ConsultaProduto>(base.Dto);
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class ConsultaProdutoPendenteViewModel : ViewModelDtoBase<IEnumerable<ConsultaProduto>>
	{
		private readonly ApiParametrizaSoftService _apiParametrizaSoftService;

		public ObservableCollection<ConsultaProduto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista), value);
			}
		}

		public override bool Valido => true;

		public ConsultaProdutoPendenteViewModel(ApiParametrizaSoftService apiParametrizaSoftService)
			: base((IEnumerable<ConsultaProduto>)null)
		{
			_apiParametrizaSoftService = apiParametrizaSoftService;
			Lista = new ObservableCollection<ConsultaProduto>();
		}

		public override void ProjectedAsViewModel()
		{
		}

		public async Task Consultar()
		{
			try
			{
				foreach (Produto item in await _apiParametrizaSoftService.GetAsync<List<Produto>>((ParametrizaSoftEndPoint)(object)ParametrizaSoftGetProdutoCliente.Criar()))
				{
					ConsultaProduto consultaProduto = new ConsultaProduto(new ConsultaProdutoDto());
					try
					{
						consultaProduto.Dto.RetornoProdutoPendente = true;
						consultaProduto.Dto.IdProdutoServicoEmpresa = ((Imposto)item).Id;
						consultaProduto.Dto.CodigoBarras = item.CodBarras;
						consultaProduto.Dto.Descricao = item.Descricao;
						consultaProduto.NcmApi = ((Imposto)item).NCM;
						consultaProduto.CfopApi = (string.IsNullOrEmpty(((Imposto)item).CFOP) ? "5102" : ((Imposto)item).CFOP);
						consultaProduto.CestApi = ((((Imposto)item).CEST.Equals("0") || HelperObject.IsNull((object)((Imposto)item).CEST)) ? string.Empty : HelperString.PreencherComCaractereEsquerda(((Imposto)item).CEST, 7, '0'));
						consultaProduto.CstIcmsApi = ((Imposto)item).CST_ICMS;
						consultaProduto.AliquotaIcmsApi = ((Imposto)item).Aliq_ICMS;
						consultaProduto.CstPisApi = ((Imposto)item).CST_PIS;
						consultaProduto.AliquotaPisApi = ((Imposto)item).Aliq_PIS;
						consultaProduto.CstCofinsApi = ((Imposto)item).CST_COFINS;
						consultaProduto.AliquotaCofinsApi = ((Imposto)item).Aliq_COFINS;
					}
					catch (BadRequestException ex)
					{
						consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException((Exception)ex);
						consultaProduto.Dto.RetornoProdutoPendente = false;
						HelperException.ShowLastException((Exception)ex, "", false, false);
					}
					catch (Exception ex2)
					{
						consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException(ex2);
						consultaProduto.Dto.RetornoProdutoPendente = false;
					}
					Lista.Add(consultaProduto);
				}
			}
			catch (Exception ex3)
			{
				HelperException.ShowLastException(ex3, "", false, false);
			}
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosPodeAtualizar()
		{
			return from x in Lista
				where x.Dto.RetornoProdutoPendente
				select x into y
				select y.Dto;
		}
	}
	public class SelecaoProdutoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SelecaoProdutoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SELECIONE", "Produtos para consulta", viewModel)
		{
		}
	}
	public class ConsultaProdutoPendenteAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ConsultaProdutoPendenteAbaViewModel(IViewModelDtoBase viewModel)
			: base("PENDÊNCIAS", "Produtos pendentes de análise", viewModel)
		{
		}
	}
	public class SelecaoProdutoViewModel : ViewModelDtoBase<IEnumerable<ConsultaProdutoDto>>
	{
		public override bool Valido => true;

		public string NomeGridControl => "SelecaoProduto";

		public SelecaoProdutoViewModel()
		{
		}

		public SelecaoProdutoViewModel(IEnumerable<ConsultaProdutoDto> produtos)
			: base(produtos)
		{
		}

		public override void ProjectedAsViewModel()
		{
		}

		public IEnumerable<ConsultaProdutoDto> RetornarItensSelecionados(object obj)
		{
			List<ConsultaProdutoDto> list = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoDto>(((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ConsultaProdutoDto>());
			if (!list.Any())
			{
				if (DialogoFactory.MostrarDialogoSimNao("Consultar todos os produtos?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					return base.Dto;
				}
				return list;
			}
			return list;
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(NomeGridControl);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			if (HelperObject.IsNull((object)val.Property))
			{
				throw new Exception($"{NomeGridControl} não encontrado");
			}
			return val.Property;
		}
	}
	public class SelecionadosComCodigoBarrasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SelecionadosComCodigoBarrasAbaViewModel(IViewModelDtoBase viewModel)
			: base("SELECIONADOS", "Com código de barras", viewModel)
		{
		}
	}
	public class SelecionadosComCodigoBarrasViewModel : ViewModelDtoBase<IEnumerable<ConsultaProduto>>
	{
		private readonly ApiParametrizaSoftService _apiParametrizaSoftService;

		public ObservableCollection<ConsultaProduto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista), value);
			}
		}

		public override bool Valido => Lista.All((ConsultaProduto x) => !x.Dto.ConsultadoViaCodigoBarras);

		public ConsultaProduto Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<ConsultaProduto>((Expression<Func<ConsultaProduto>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<ConsultaProduto>((Expression<Func<ConsultaProduto>>)(() => Selecionado), value);
			}
		}

		public SelecionadosComCodigoBarrasViewModel(IEnumerable<ConsultaProdutoDto> produtos, ApiParametrizaSoftService apiParametrizaSoftService)
			: base(produtos.Select((ConsultaProdutoDto x) => new ConsultaProduto(x)))
		{
			_apiParametrizaSoftService = apiParametrizaSoftService;
			Lista = new ObservableCollection<ConsultaProduto>(base.Dto);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public async Task Consultar()
		{
			foreach (ConsultaProduto consultaProduto in Lista)
			{
				try
				{
					Produto val = await _apiParametrizaSoftService.GetAsync<Produto>((ParametrizaSoftEndPoint)(object)ParametrizaSoftGetProdutoCodigoBarras.Criar().ComCodigoBarras(consultaProduto.Dto.CodigoBarras));
					consultaProduto.Dto.ConsultadoViaCodigoBarras = true;
					consultaProduto.CfopApi = (string.IsNullOrEmpty(((Imposto)val).CFOP) ? "5102" : ((Imposto)val).CFOP);
					consultaProduto.NcmApi = ((Imposto)val).NCM;
					consultaProduto.CestApi = ((string.IsNullOrEmpty(((Imposto)val).CEST) || ((Imposto)val).CEST.Equals("0") || ((Imposto)val).CEST.Equals("0000000")) ? string.Empty : HelperString.PreencherComCaractereEsquerda(((Imposto)val).CEST, 7, '0'));
					consultaProduto.CstIcmsApi = ((Imposto)val).CST_ICMS;
					consultaProduto.AliquotaIcmsApi = ((Imposto)val).Aliq_ICMS;
					consultaProduto.CstPisApi = ((Imposto)val).CST_PIS;
					consultaProduto.AliquotaPisApi = ((Imposto)val).Aliq_PIS;
					consultaProduto.CstCofinsApi = ((Imposto)val).CST_COFINS;
					consultaProduto.AliquotaCofinsApi = ((Imposto)val).Aliq_COFINS;
					Selecionado = consultaProduto;
				}
				catch (BadRequestException ex)
				{
					consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException((Exception)ex);
					HelperException.ShowLastException((Exception)ex, "", false, false);
					break;
				}
				catch (Exception ex2)
				{
					consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException(ex2);
					Selecionado = consultaProduto;
				}
			}
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosPodeAtualizar()
		{
			return from x in Lista
				where x.PossuiDivergencia && x.Dto.ConsultadoViaCodigoBarras
				select x into y
				select y.Dto;
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosNaoConsultados()
		{
			return from x in Lista
				where !x.Dto.ConsultadoViaCodigoBarras
				select x into y
				select y.Dto;
		}
	}
	public class SelecionadosSemCodigoBarrasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SelecionadosSemCodigoBarrasAbaViewModel(IViewModelDtoBase viewModel)
			: base("SELECIONADOS", "Sem código de barras", viewModel)
		{
		}
	}
	public class SelecionadosSemCodigoBarrasViewModel : ViewModelDtoBase<IEnumerable<ConsultaProduto>>
	{
		private readonly ApiParametrizaSoftService _apiParametrizaSoftService;

		public ObservableCollection<ConsultaProduto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ConsultaProduto>>((Expression<Func<ObservableCollection<ConsultaProduto>>>)(() => Lista), value);
			}
		}

		public override bool Valido => Lista.All((ConsultaProduto x) => !x.Dto.ConsultadoViaNcmECest);

		public ConsultaProduto Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<ConsultaProduto>((Expression<Func<ConsultaProduto>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<ConsultaProduto>((Expression<Func<ConsultaProduto>>)(() => Selecionado), value);
			}
		}

		public SelecionadosSemCodigoBarrasViewModel(IEnumerable<ConsultaProdutoDto> produtos, ApiParametrizaSoftService apiParametrizaSoftService)
			: base(produtos.Select((ConsultaProdutoDto x) => new ConsultaProduto(x)))
		{
			_apiParametrizaSoftService = apiParametrizaSoftService;
			Lista = new ObservableCollection<ConsultaProduto>(base.Dto);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public async Task Consultar()
		{
			foreach (ConsultaProduto consultaProduto in Lista)
			{
				try
				{
					ProdutoCliente val = ProdutoCliente.Criar().ComIdProdutoCliente(consultaProduto.Dto.IdProdutoServicoEmpresa).ComCest(consultaProduto.Dto.CestBase)
						.ComNcm(consultaProduto.Dto.NcmBase)
						.ComCodigoBarras(consultaProduto.Dto.CodigoBarras)
						.ComDescricao(consultaProduto.Dto.Descricao);
					Produto val2 = await _apiParametrizaSoftService.PostAsync(ParametrizaSoftPostProduto.Criar().ComProdutoCliente(val), "application/json");
					if (!string.IsNullOrEmpty(val2.Mensagem))
					{
						consultaProduto.Mensagem = val2.Mensagem;
						if (val2.Mensagem.Contains("Produto não permitido para consulta NCM e CEST"))
						{
							consultaProduto.Mensagem = $"{val2.Mensagem} Produto já enviado anteriormente, porém, ainda não corrigido.";
							consultaProduto.Dto.ConsultaProibida = true;
						}
						else if (val2.Mensagem.Contains("Produto enviado para análise"))
						{
							consultaProduto.Dto.EnviadoParaAnalise = true;
						}
					}
					else
					{
						consultaProduto.Dto.ConsultadoViaNcmECest = true;
						consultaProduto.NcmApi = ((Imposto)val2).NCM;
						consultaProduto.CestApi = ((string.IsNullOrEmpty(((Imposto)val2).CEST) || ((Imposto)val2).CEST.Equals("0") || ((Imposto)val2).CEST.Equals("0000000")) ? string.Empty : HelperString.PreencherComCaractereEsquerda(((Imposto)val2).CEST, 7, '0'));
						consultaProduto.CfopApi = (string.IsNullOrEmpty(((Imposto)val2).CFOP) ? "5102" : ((Imposto)val2).CFOP);
						consultaProduto.CstIcmsApi = ((Imposto)val2).CST_ICMS;
						consultaProduto.AliquotaIcmsApi = ((Imposto)val2).Aliq_ICMS;
						consultaProduto.CstPisApi = ((Imposto)val2).CST_PIS;
						consultaProduto.AliquotaPisApi = ((Imposto)val2).Aliq_PIS;
						consultaProduto.CstCofinsApi = ((Imposto)val2).CST_COFINS;
						consultaProduto.AliquotaCofinsApi = ((Imposto)val2).Aliq_COFINS;
						Selecionado = consultaProduto;
					}
				}
				catch (BadRequestException ex)
				{
					consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException((Exception)ex);
				}
				catch (Exception ex2)
				{
					consultaProduto.Mensagem = HelperException.RetornarUltimaMensagemDaException(ex2);
				}
			}
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosPodeAtualizar()
		{
			return from x in Lista
				where x.PossuiDivergencia && x.Dto.ConsultadoViaNcmECest
				select x into y
				select y.Dto;
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosEnviadosParaAnalise()
		{
			return from x in Lista
				where x.Dto.EnviadoParaAnalise
				select x into y
				select y.Dto;
		}

		public IEnumerable<ConsultaProdutoDto> ObterProdutosConsultaProibida()
		{
			return from x in Lista
				where x.Dto.ConsultaProibida
				select x into y
				select y.Dto;
		}
	}
}
namespace Otus.Modulo.ConsultasProduto.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.ConsultasProduto.Properties.Resources", typeof(Resources).Assembly);
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

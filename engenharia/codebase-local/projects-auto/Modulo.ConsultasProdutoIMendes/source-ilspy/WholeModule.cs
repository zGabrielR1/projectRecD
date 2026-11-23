using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel;
using System.IO;
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
using DigisatServer.Application.Core.Projections.CadastrosProjection.IMendes.SaneamentoGrades;
using DigisatServer.Application.Main.Dto.Cadastros.ConsultasProduto;
using DigisatServer.Application.Main.Dto.Cadastros.ConsultasProdutoIMendes;
using DigisatServer.Application.Main.Dto.Cadastros.ConsultasProdutoIMendes.SaneamentoGrades;
using DigisatServer.Application.Main.Dto.Cadastros.NsCMNsBS;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Enum.IMendes;
using DigisatServer.Infrastructure.Crosscutting.File;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Helper.IMendes;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Base;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Base.EnviaRecebeDados;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Base.SaneamentoGrades;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Base.Simulador;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.EnviarReceberDados.Alterados;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.EnviarReceberDados.DescricaoProdutos;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.EnviarReceberDados.HistoricoAcesso;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.EnviarReceberDados.RemoverDevolvidos;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.RegrasFiscais;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.SaneamentoGrades;
using DigisatServer.Infrastructure.Crosscutting.Service.IMendes.Classes.Retorno.StatusCliente;
using DigisatServer.Infrastructure.Crosscutting.Style.GridControls;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.Views;
using Otus.Modulo.ConsultasProdutoIMendes.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.ConsultasProdutoIMendes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.ConsultasProdutoIMendes")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.ConsultasProdutoIMendes
{
	[Module(ModuleName = "ConsultaProdutoIMendesModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class ConsultaProdutoIMendesModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ConsultaProdutoIMendesModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ConsultasProdutoIMendesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ConsultaProdutoIMendesFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<CorrigirCadastroFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.ConsultasProdutoIMendes.Views
{
	public class CorrigirCadastroFichaView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public CorrigirCadastroFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/corrigircadastrofichaview.xaml", UriKind.Relative);
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
	public class ConsultaProdutoIMendesFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultaProdutoIMendesFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutoimendesfichaview.xaml", UriKind.Relative);
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
	public class ConsultasProdutoIMendesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultasProdutoIMendesView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultasprodutoimendesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ConsultasProdutoIMendes.Views.ConsultaProdutoFichaViewDetalhes
{
	public class ConsultarProdutosAlteradosView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultarProdutosAlteradosView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/consultarprodutosalteradosview.xaml", UriKind.Relative);
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
	public class CorrigirCadastroView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CorrigirCadastroView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/corrigircadastroview.xaml", UriKind.Relative);
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
	public class ConsultarHistoricoAcessoView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConsultarHistoricoAcessoView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/consultarhistoricoacessoview.xaml", UriKind.Relative);
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
	public class SelecaoProdutoIMendesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SelecaoProdutoIMendesView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/selecaoprodutoimendesview.xaml", UriKind.Relative);
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
	public class SimulacaoView : UserControl, IComponentConnector
	{
		internal GridControlBestFit GridSimulacao;

		private bool _contentLoaded;

		public SimulacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/simulacaoview.xaml", UriKind.Relative);
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
				GridSimulacao = (GridControlBestFit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class SelecionadosView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SelecionadosView()
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
				Uri resourceLocator = new Uri("/Modulo.ConsultasProdutoIMendes;component/views/consultaprodutofichaviewdetalhes/selecionadosview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ConsultasProdutoIMendes.ViewModels
{
	public class ConsultarProdutosAlteradosViewModel : ViewModelDtoBase<Alterado>
	{
		private readonly ApiIMendesService _apiIMendesService;

		public override bool Valido => true;

		public Alterado Alterado
		{
			get
			{
				return ((BindableBase)this).GetProperty<Alterado>((Expression<Func<Alterado>>)(() => Alterado));
			}
			set
			{
				((BindableBase)this).SetProperty<Alterado>((Expression<Func<Alterado>>)(() => Alterado), value);
			}
		}

		public ConsultarProdutosAlteradosViewModel(ApiIMendesService apiIMendesService)
			: base((Alterado)null)
		{
			_apiIMendesService = apiIMendesService;
		}

		public override void ProjectedAsViewModel()
		{
		}

		public async Task Consultar(IWService service)
		{
			_ = 1;
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Consultando produtos alterados...");
				});
				PessoaDto emitente = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				StatusCliente val = await _apiIMendesService.GetAsync<StatusCliente>((IMendesEndPoint)(object)IMendesGetStatusCliente.Criar().ComCnpj(emitente.CpfCnpj));
				if (!val.sucesso)
				{
					throw new Exception(val.mensagem);
				}
				Alterado = await _apiIMendesService.PostAsync<Alterado>(IMendesPostEnviaRecebeDados.Criar().ComDados((EnviaRecebeDados)(object)Alterados.Criar().ComCnpj(emitente.CpfCnpj).ComUf(emitente.EnderecoPrincipal.Municipio.UF.Sigla)), "application/json");
				if (!Alterado.produto.Any())
				{
					DialogoFactory.MostrarDialogoConfirma(Alterado.cabecalho.mensagem, false, HorizontalAlignment.Center);
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
	}
	public class ConsultarProdutosAlteradosAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ConsultarProdutosAlteradosAbaViewModel(IViewModelDtoBase viewModel)
			: base("FINALIZADOS/ALTERADOS", "Produtos com alterações", viewModel)
		{
		}
	}
	public class CorrigirCadastroAbaViewModel : AbaPrincipalViewModelBase
	{
		public CorrigirCadastroAbaViewModel(IViewModelPrincipal viewModel)
			: base("CORRIGIR CADASTRO", "Vincular com produto IMendes", viewModel)
		{
		}
	}
	public class CorrigirCadastroFormulario : FormularioEntity<ProdutoDevolvido>
	{
		public CorrigirCadastroFormulario(ProdutoDevolvido produtoDevolvido, ApiIMendesService apiIMendesService)
			: base(produtoDevolvido)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new CorrigirCadastroAbaViewModel((IViewModelPrincipal)(object)new CorrigirCadastroViewModel(base.Dto, apiIMendesService)) });
		}
	}
	public class CorrigirCadastroFichaViewModel : FichaBase<ProdutoDevolvido, CorrigirCadastroViewModel>
	{
		private SelecaoNavigateParameterCallBack<ProdutoDevolvido> _selecaoNavigateParameterCallBack;

		private ApiIMendesService _apiIMendesService;

		public SelecaoCommandParameter<ProdutoServicoEmpresaDto> ProdutoServicoCommandParameter { get; set; }

		public ProdutoDevolvido Dto { get; set; }

		public ICommand GravarCommand { get; set; }

		public CorrigirCadastroFichaViewModel()
			: base("Corrigir cadastro do produto devolvido")
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			GravarCommand = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)PodeGravar, (bool?)null);
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNotNull((object)base.Formulario))
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		private void Gravar()
		{
			ProdutoDevolvido obj = ((FormularioEntity<ProdutoDevolvido>)(CorrigirCadastroFormulario)(object)base.Formulario).ToDto<ProdutoDevolvido>();
			_selecaoNavigateParameterCallBack?.CallBack(obj);
			((NavigationBase)this).FecharView();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)Dto))
			{
				return;
			}
			_selecaoNavigateParameterCallBack = navigationParameters[typeof(SelecaoNavigateParameterCallBack<ProdutoDevolvido>).Name] as SelecaoNavigateParameterCallBack<ProdutoDevolvido>;
			Dto = navigationParameters["Dto"] as ProdutoDevolvido;
			ref ApiIMendesService apiIMendesService = ref _apiIMendesService;
			object obj = navigationParameters["Api"];
			apiIMendesService = (ApiIMendesService)((obj is ApiIMendesService) ? obj : null);
			IWService service = DadosSistema.GetService();
			IEnumerable<ProdutoServicoEmpresaDto> enumerable = service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => x.ObterProdutosServicosPorCodigoInternoOuCodigoBarrasOuCodigoIMendes(Dto.Codigo)));
			if (HelperObject.IsNull((object)enumerable) || (!enumerable.Any() && Dto.Codigo.Length == 14))
			{
				enumerable = service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => x.ObterProdutosServicosPorCodigoInternoOuCodigoBarrasOuCodigoIMendes(HelperString.CopiarTextoDaDireitaParaEsquerda(Dto.Codigo, 13))));
			}
			ProcessarRetornoPesquisa(enumerable, Dto.Codigo);
		}

		private void ProcessarRetornoPesquisa(IEnumerable<ProdutoServicoEmpresaDto> resultado, string informacaoPesquisa)
		{
			List<ProdutoServicoEmpresaDto> list = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(resultado);
			if (list.Count == 1)
			{
				Dto.SetarProdutoServicoEmpresa(list.FirstOrDefault());
				if (base.Formulario == null)
				{
					((FichaBase<ProdutoDevolvido, CorrigirCadastroViewModel>)this).CriarFormulario();
				}
				base.DisableConfirmOnClose();
			}
			else
			{
				DialogoFactory.MostrarDialogoConfirma("Indique qual produto do seu cadastro condiz com o retornado no histórico", false, HorizontalAlignment.Center);
				ProdutoServicoCommandParameter = new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)CallbackSelecaoProdutoServicoCommandParameter, true);
				ProdutoServicoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)informacaoPesquisa);
				((NavigationBase)this).Navegar((object)ProdutoServicoCommandParameter);
			}
		}

		protected void CallbackSelecaoProdutoServicoCommandParameter(ProdutoServicoEmpresaDto produtoServico)
		{
			Dto.SetarProdutoServicoEmpresa(produtoServico);
			if (base.Formulario == null)
			{
				((FichaBase<ProdutoDevolvido, CorrigirCadastroViewModel>)this).CriarFormulario();
			}
			base.DisableConfirmOnClose();
		}

		protected override void CriarFormulario()
		{
			base.Formulario = (IFormulario)(object)new CorrigirCadastroFormulario(Dto, _apiIMendesService);
		}
	}
	public class ConsultaProdutoIMendes : BindableBase
	{
		public ConsultaProdutoIMendesDto Dto { get; set; }

		public string StatusProduto
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => StatusProduto));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => StatusProduto), value);
			}
		}

		public ConsultaProdutoIMendes()
		{
		}

		public ConsultaProdutoIMendes(ConsultaProdutoIMendesDto consultaProduto)
		{
			Dto = consultaProduto;
			StatusProduto = HelperEnum.RetornarDescricao((Enum)(object)(StatusProduto)0);
		}
	}
	public class ConsultaProdutoIMendesFichaViewModel : FichaBase<ConsultaProdutoIMendes, SelecaoProdutoIMendesViewModel>
	{
		private readonly IWService _service;

		private readonly IIMendesClientService _iMendesClientService;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ICommand GerarSimulacaoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => GerarSimulacaoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => GerarSimulacaoCommand), value);
			}
		}

		public ICommand ProcessarSimulacaoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ProcessarSimulacaoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ProcessarSimulacaoCommand), value);
			}
		}

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

		public ICommand ImprimirProdutosConsultadosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ImprimirProdutosConsultadosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ImprimirProdutosConsultadosCommand), value);
			}
		}

		public ICommand VoltarCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => VoltarCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => VoltarCommand), value);
			}
		}

		public ICommand ConsultarRegrasFiscaisProdutosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarRegrasFiscaisProdutosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarRegrasFiscaisProdutosCommand), value);
			}
		}

		public ICommand ConsultarHistoricoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarHistoricoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarHistoricoCommand), value);
			}
		}

		public ICommand ConsultarProdutosAlteradosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosAlteradosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarProdutosAlteradosCommand), value);
			}
		}

		public ICommand TransferirProdutosAlteradosParaConsultaCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => TransferirProdutosAlteradosParaConsultaCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => TransferirProdutosAlteradosParaConsultaCommand), value);
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

		public ConsultaProdutoIMendesFichaViewModel(IWService service, IIMendesClientService iMendesClientService, IImpressaoRelatorioService impressaoRelatorioService)
			: base(string.Empty)
		{
			_service = service;
			_iMendesClientService = iMendesClientService;
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Expected O, but got Unknown
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Expected O, but got Unknown
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01eb: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return;
			}
			try
			{
				ApiIMendesService apiIMendesService = _iMendesClientService.CriarApi();
				ConsultaProdutoIMendesFormulario consultaProdutoIMendesFormulario = new ConsultaProdutoIMendesFormulario(_service, apiIMendesService, _impressaoRelatorioService);
				GerarSimulacaoCommand = (ICommand)new AsyncCommand((Func<Task>)consultaProdutoIMendesFormulario.GerarSimulacao, (Func<bool>)consultaProdutoIMendesFormulario.PodeGerarSimulacao, (bool?)null);
				ProcessarSimulacaoCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)consultaProdutoIMendesFormulario.ProcessarSimulacao, (Func<object, bool>)consultaProdutoIMendesFormulario.PodeProcessarSimulacao, false, (bool?)null);
				AvancarCommand = (ICommand)new DelegateCommand<object>((Action<object>)consultaProdutoIMendesFormulario.Avancar, (Func<object, bool>)consultaProdutoIMendesFormulario.PodeAvancar, (bool?)null);
				ImprimirProdutosConsultadosCommand = (ICommand)new DelegateCommand<object>((Action<object>)consultaProdutoIMendesFormulario.ImprimirProdutosConsultados, (Func<object, bool>)consultaProdutoIMendesFormulario.PodeImprimirProdutosConsultados, (bool?)null);
				VoltarCommand = (ICommand)new DelegateCommand((Action)consultaProdutoIMendesFormulario.Voltar, (Func<bool>)consultaProdutoIMendesFormulario.PodeVoltar, (bool?)null);
				ConsultarRegrasFiscaisProdutosCommand = (ICommand)new AsyncCommand((Func<Task>)consultaProdutoIMendesFormulario.ConsultarRegrasFiscaisProdutos, (Func<bool>)consultaProdutoIMendesFormulario.PodeConsultarRegrasFiscaisProdutos, false, (bool?)null);
				ConsultarHistoricoCommand = (ICommand)new AsyncCommand((Func<Task>)consultaProdutoIMendesFormulario.ConsultarHistorico, (Func<bool>)consultaProdutoIMendesFormulario.PodeConsultarHistorico, false, (bool?)null);
				ConsultarProdutosAlteradosCommand = (ICommand)new AsyncCommand((Func<Task>)consultaProdutoIMendesFormulario.ConsultarProdutosAlterados, (Func<bool>)consultaProdutoIMendesFormulario.PodeConsultarProdutosAlterados, false, (bool?)null);
				ProcessarProdutosCommand = (ICommand)new AsyncCommand((Func<Task>)consultaProdutoIMendesFormulario.ProcessarProdutos, (Func<bool>)consultaProdutoIMendesFormulario.PodeProcessarProdutos, false, (bool?)null);
				TransferirProdutosAlteradosParaConsultaCommand = (ICommand)new DelegateCommand((Action)consultaProdutoIMendesFormulario.TransferirProdutosAlteradosParaConsulta, (Func<bool>)consultaProdutoIMendesFormulario.PodeTransferirProdutosAlteradosParaConsulta, (bool?)null);
				base.Formulario = (IFormulario)(object)consultaProdutoIMendesFormulario;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
		}
	}
	public class ConsultaProdutoIMendesFormulario : Formulario
	{
		private readonly IWService _service;

		private readonly ApiIMendesService _apiIMendesService;

		private SelecaoProdutoIMendesViewModel _selecaoProdutoIMendesViewModel;

		private SelecaoProdutoIMendesAbaViewModel _selecaoProdutoIMendesAbaViewModel;

		private SimulacaoViewModel _simulacaoViewModel;

		private SimulacaoAbaViewModel _simulacaoAbaViewModel;

		private bool _processouConsultaComCodigoBarras;

		private SelecionadosViewModel _selecionadosViewModel;

		private SelecionadosAbaViewModel _selecionadosAbaViewModel;

		private bool _terminouConsultaComCodigoBarras;

		private ConsultarHistoricoAcessoAbaViewModel _consultarHistoricoAcessoAbaViewModel;

		private ConsultarHistoricoAcessoViewModel _consultarHistoricoAcessoViewModel;

		private ConsultarProdutosAlteradosAbaViewModel _consultarProdutosAlteradosAbaViewModel;

		private ConsultarProdutosAlteradosViewModel _consultarProdutosAlteradosViewModel;

		private List<ConsultaProdutoIMendesDto> _produtos;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ConsultaProdutoIMendesFormulario(IWService service, ApiIMendesService apiIMendesService, IImpressaoRelatorioService impressaoRelatorioService)
		{
			_produtos = new List<ConsultaProdutoIMendesDto>();
			_service = service;
			_apiIMendesService = apiIMendesService;
			_impressaoRelatorioService = impressaoRelatorioService;
			CriarAbaConsultarHistoricoAcesso();
			CriarAbaProdutosAlterados();
			ThreadPrincipal.Executar((Action)ObterProdutos);
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		private void CriarAbaConsultarHistoricoAcesso()
		{
			if (!((Formulario)this).Abas.Any((IAbaViewModelBase x) => HelperObject.Is<ConsultarHistoricoAcessoViewModel>((object)x.ViewModel)))
			{
				_consultarHistoricoAcessoViewModel = new ConsultarHistoricoAcessoViewModel(_apiIMendesService);
				_consultarHistoricoAcessoAbaViewModel = new ConsultarHistoricoAcessoAbaViewModel((IViewModelDtoBase)(object)_consultarHistoricoAcessoViewModel);
				((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_consultarHistoricoAcessoAbaViewModel);
			}
		}

		private void CriarAbaProdutosAlterados()
		{
			if (!((Formulario)this).Abas.Any((IAbaViewModelBase x) => HelperObject.Is<ConsultarProdutosAlteradosViewModel>((object)x.ViewModel)))
			{
				_consultarProdutosAlteradosViewModel = new ConsultarProdutosAlteradosViewModel(_apiIMendesService);
				_consultarProdutosAlteradosAbaViewModel = new ConsultarProdutosAlteradosAbaViewModel((IViewModelDtoBase)(object)_consultarProdutosAlteradosViewModel);
				((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_consultarProdutosAlteradosAbaViewModel);
			}
		}

		private void ObterProdutos()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				LoaderProcessoService.MostrarLoader("Obtendo produtos...");
			});
			_service.Canal<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoIMendesDto>>>((Func<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoIMendesDto>>>)((IProdutoServicoModuleService service) => service.ObterConsultasProdutoIMendes())).ContinueWith(delegate(Task<IEnumerable<ConsultaProdutoIMendesDto>> task)
			{
				_produtos = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesDto>(task.Result);
				if (!_produtos.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto localizado para consulta", false, HorizontalAlignment.Center);
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						ProjetarAbaProdutos(_produtos);
					});
				}
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			});
		}

		private void ProjetarAbaProdutos(IEnumerable<ConsultaProdutoIMendesDto> produtos)
		{
			_selecaoProdutoIMendesViewModel = new SelecaoProdutoIMendesViewModel(produtos);
			_selecaoProdutoIMendesAbaViewModel = new SelecaoProdutoIMendesAbaViewModel((IViewModelDtoBase)(object)_selecaoProdutoIMendesViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)_selecaoProdutoIMendesAbaViewModel });
		}

		public bool PodeGerarSimulacao()
		{
			return HelperObject.Is<ConsultarHistoricoAcessoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public async Task GerarSimulacao()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				LoaderProcessoService.MostrarLoader("Gerando simulação...");
			});
			await _service.Canal<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoIMendesDto>>>((Func<IProdutoServicoModuleService, Task<IEnumerable<ConsultaProdutoIMendesDto>>>)((IProdutoServicoModuleService service) => service.ObterProdutosSimulacaoIMendes())).ContinueWith(delegate(Task<IEnumerable<ConsultaProdutoIMendesDto>> task)
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					List<ConsultaProdutoIMendesDto> produtosParaSimulacao = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesDto>(task.Result);
					if (!produtosParaSimulacao.Any())
					{
						DialogoFactory.MostrarDialogoConfirma("Nenhum produto com código de barras (EAN) localizado para simulação", false, HorizontalAlignment.Center);
					}
					else
					{
						ThreadPrincipal.Executar((Action)delegate
						{
							if (HelperObject.IsNotNull((object)_simulacaoAbaViewModel) && ((Formulario)this).Abas.Contains((IAbaViewModelBase)(object)_simulacaoAbaViewModel))
							{
								((Formulario)this).RemoverAba((IAbaViewModelBase)(object)_simulacaoAbaViewModel);
							}
							_simulacaoViewModel = new SimulacaoViewModel(produtosParaSimulacao);
							_simulacaoAbaViewModel = new SimulacaoAbaViewModel((IViewModelDtoBase)(object)_simulacaoViewModel);
							((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_simulacaoAbaViewModel);
							((Formulario)this).PosicionarNaUltimaAba();
						});
					}
				}
			});
		}

		public bool PodeProcessarSimulacao(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<SimulacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public async Task ProcessarSimulacao(object obj)
		{
			try
			{
				ConsultaIMendesDto consultaIMendes = _simulacaoViewModel.RetornarItensSelecionados(obj);
				List<ConsultaProdutoIMendesDto> selecionados = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesDto>(consultaIMendes.Produtos);
				if (!selecionados.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Selecione ao menos um produto para simular.", false, HorizontalAlignment.Center);
					return;
				}
				if (HelperString.IsNullOrEmpty(consultaIMendes.Email))
				{
					DialogoFactory.MostrarDialogoConfirma("O endereço de e-mail para receber o laudo da simulação deve ser informado.", false, HorizontalAlignment.Center);
					_simulacaoViewModel.FocoNoEmail = !_simulacaoViewModel.FocoNoEmail;
					return;
				}
				PessoaDto emitente = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				string cpfCnpj = emitente.CpfCnpj;
				if (!(await _apiIMendesService.GetAsync<StatusCliente>((IMendesEndPoint)(object)IMendesGetStatusCliente.Criar().ComCnpj(cpfCnpj))).mensagem.Contains("CNPJ não encontrado na base de dados"))
				{
					throw new Exception("Esta funcionalidade é destinada exclusivamente a empresas que ainda não possuem cadastro na base IMendes. Clientes ativos devem utilizar a integração padrão.");
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Gerando simulação...");
				});
				List<ProdutoSimulacaoTributacaoDto> tributacoes = new List<ProdutoSimulacaoTributacaoDto>();
				await _service.Canal<IProdutoServicoModuleService, Task<IEnumerable<ProdutoSimulacaoTributacaoDto>>>((Func<IProdutoServicoModuleService, Task<IEnumerable<ProdutoSimulacaoTributacaoDto>>>)((IProdutoServicoModuleService service) => service.ObterProdutosSimulacaoTributacao(selecionados.Select((ConsultaProdutoIMendesDto x) => x.IdProdutoEmpresa)))).ContinueWith(delegate(Task<IEnumerable<ProdutoSimulacaoTributacaoDto>> task)
				{
					if (task.IsFaulted)
					{
						throw new Exception(HelperException.GetLastException((Exception)task.Exception).Message);
					}
					tributacoes = HelperIEnumerable.ToListEmptyWhenNull<ProdutoSimulacaoTributacaoDto>(task.Result);
				});
				string text = (consultaIMendes.SubstituicaoTributaria ? "S" : "N");
				SimuladorRequest val = new SimuladorRequest
				{
					emit = MontarEmitSimulacao(emitente, cpfCnpj, text)
				};
				PerfilSimulador val2 = new PerfilSimulador();
				List<string> list = new List<string>();
				EnderecoDto enderecoPrincipal = emitente.EnderecoPrincipal;
				object obj2;
				if (enderecoPrincipal == null)
				{
					obj2 = null;
				}
				else
				{
					MunicipioDto municipio = enderecoPrincipal.Municipio;
					if (municipio == null)
					{
						obj2 = null;
					}
					else
					{
						UFDto uF = municipio.UF;
						obj2 = ((uF != null) ? uF.Sigla : null);
					}
				}
				if (obj2 == null)
				{
					obj2 = "SC";
				}
				list.Add((string)obj2);
				val2.uf = list;
				val2.substICMS = text;
				val2.fabricacaoPropria = consultaIMendes.FabricacaoPropria;
				val.perfil = val2;
				SimuladorRequest val3 = val;
				if (val3.emit.crt == 3 && val3.emit.regimeTrib != "LR")
				{
					DialogoFactory.MostrarDialogoConfirma("A empresa está cadastrada no Regime Normal, porém o \"Código da Faixa\" na configuração IMendes não está definido como Lucro Real ou Lucro Presumido. Ajuste o Código da Faixa no cadastro do emitente antes de gerar a simulação.", false, HorizontalAlignment.Center);
					return;
				}
				foreach (ConsultaProdutoIMendesDto selecionado in selecionados)
				{
					ProdutoSimulacaoTributacaoDto val4 = tributacoes.FirstOrDefault((ProdutoSimulacaoTributacaoDto x) => x.IdProdutoEmpresa.Equals(selecionado.IdProdutoEmpresa));
					if (!HelperObject.IsNull((object)val4) && !HelperString.IsNullOrEmpty(val4.CodigoBarras))
					{
						val3.produtos.Add(new ProdutoSimulador
						{
							codigo = val4.CodigoBarras,
							codInterno = "N",
							descricao = val4.Descricao,
							ncm = val4.Ncm,
							simulacao = new Simulacao
							{
								qtdeEstoque = (int)val4.QuantidadeEstoque,
								qtdeVenda = (int)selecionado.QuantidadeVendida,
								vlrUnitCusto = val4.ValorUnitarioCusto,
								vlrUnitVenda = val4.ValorUnitarioVenda,
								tributacao = new Tributacao
								{
									icms = new Icms
									{
										aliqICMS = val4.AliquotaIcms,
										aliqRedBCICMS = val4.AliquotaReducaoBaseCalculoIcms,
										cstCsosn = val4.CstCsosn
									},
									piscofins = new PisCofins
									{
										aliqPIS = val4.AliquotaPis,
										aliqCOFINS = val4.AliquotaCofins,
										cstPISCOFINS = val4.CstPisCofins
									},
									cbsIbs = new CbsIbs
									{
										cstIBSCBS = val4.CstIbsCbs,
										cClassTrib = val4.ClassificacaoTributariaIbsCbs,
										aliqCBS = val4.AliquotaCbs,
										percReducaoAliquotaCBS = val4.PercentualReducaoAliquotaCbs,
										aliqIBSEstadual = val4.AliquotaIbsEstadual,
										aliqIBSMunicipal = val4.AliquotaIbsMunicipal,
										percReducaoAliquotaIBS = val4.PercentualReducaoAliquotaIbs
									}
								}
							}
						});
					}
				}
				if (!val3.produtos.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto com código de barras (EAN) e tributação foi localizado para a simulação.", false, HorizontalAlignment.Center);
					return;
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Enviando simulação...");
				});
				string idSimulacao = await _apiIMendesService.EnviarSimulacao(val3, consultaIMendes.Email);
				if (HelperString.IsNullOrEmpty(idSimulacao))
				{
					DialogoFactory.MostrarDialogoConfirma("Não foi possível obter o identificador da simulação.", false, HorizontalAlignment.Center);
					return;
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Aguardando o relatório...");
				});
				await _apiIMendesService.AguardarSimulacaoFinalizada(idSimulacao, 60, 5000);
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Baixando o relatório...");
				});
				byte[] array = await _apiIMendesService.BaixarSimulacaoPdf(idSimulacao);
				if (HelperObject.IsNull((object)array) || !array.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("O relatório da simulação retornou vazio.", false, HorizontalAlignment.Center);
					return;
				}
				string path = $"Simulacao_IMendes_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), path);
				File.WriteAllBytes(text2, array);
				Process.Start(text2);
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

		private static EmitSimulador MontarEmitSimulacao(PessoaDto emitente, string cpfCnpj, string substIcms)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Expected O, but got Unknown
			EmitSimulador val = new EmitSimulador
			{
				cnpj = HelperString.RemoveCaracteresAlfanumerico(cpfCnpj),
				substICMS = substIcms
			};
			JuridicaDto juridica = emitente.Juridica;
			int? obj;
			if (juridica == null)
			{
				obj = null;
			}
			else
			{
				EmitenteDto emitente2 = juridica.Emitente;
				obj = ((emitente2 != null) ? new int?(emitente2.RegimeTributario.Codigo) : null);
			}
			val.crt = (((obj ?? 1) != 3) ? 1 : 3);
			JuridicaDto juridica2 = emitente.Juridica;
			CodigoFaixa? codigoFaixa;
			if (juridica2 == null)
			{
				codigoFaixa = null;
			}
			else
			{
				EmitenteDto emitente3 = juridica2.Emitente;
				if (emitente3 == null)
				{
					codigoFaixa = null;
				}
				else
				{
					IMendesDto iMendes = emitente3.IMendes;
					codigoFaixa = ((iMendes != null) ? new CodigoFaixa?(iMendes.CodigoFaixa) : null);
				}
			}
			val.regimeTrib = ObterRegimeTributarioSimulacao(codigoFaixa);
			EnderecoDto enderecoPrincipal = emitente.EnderecoPrincipal;
			object obj2;
			if (enderecoPrincipal == null)
			{
				obj2 = null;
			}
			else
			{
				MunicipioDto municipio = enderecoPrincipal.Municipio;
				if (municipio == null)
				{
					obj2 = null;
				}
				else
				{
					UFDto uF = municipio.UF;
					obj2 = ((uF != null) ? uF.Sigla : null);
				}
			}
			if (obj2 == null)
			{
				obj2 = "SC";
			}
			val.uf = (string)obj2;
			JuridicaDto juridica3 = emitente.Juridica;
			object value;
			if (juridica3 == null)
			{
				value = null;
			}
			else
			{
				EmitenteDto emitente4 = juridica3.Emitente;
				if (emitente4 == null)
				{
					value = null;
				}
				else
				{
					IMendesDto iMendes2 = emitente4.IMendes;
					value = ((iMendes2 != null) ? iMendes2.Cnae : null);
				}
			}
			string cnae;
			if (!string.IsNullOrEmpty((string)value))
			{
				JuridicaDto juridica4 = emitente.Juridica;
				object obj3;
				if (juridica4 == null)
				{
					obj3 = null;
				}
				else
				{
					EmitenteDto emitente5 = juridica4.Emitente;
					if (emitente5 == null)
					{
						obj3 = null;
					}
					else
					{
						IMendesDto iMendes3 = emitente5.IMendes;
						obj3 = ((iMendes3 != null) ? iMendes3.Cnae : null);
					}
				}
				cnae = HelperString.ApenasNumeros((string)obj3);
			}
			else
			{
				JuridicaDto juridica5 = emitente.Juridica;
				object obj4;
				if (juridica5 == null)
				{
					obj4 = null;
				}
				else
				{
					EmitenteDto emitente6 = juridica5.Emitente;
					obj4 = ((emitente6 != null) ? emitente6.Cnae : null);
				}
				cnae = HelperString.ApenasNumeros((string)obj4);
			}
			val.cnae = cnae;
			return val;
		}

		private static string ObterRegimeTributarioSimulacao(CodigoFaixa? codigoFaixa)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Invalid comparison between Unknown and I4
			CodigoFaixa? val = codigoFaixa;
			if (val.HasValue)
			{
				CodigoFaixa valueOrDefault = val.GetValueOrDefault();
				if ((int)valueOrDefault == 0 || (int)valueOrDefault == 1)
				{
					return "LR";
				}
			}
			return "SN";
		}

		public bool PodeAvancar(object obj)
		{
			if (HelperObject.IsNotNull(obj) && HelperObject.Is<SelecaoProdutoIMendesViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				return ((ViewModelValidationBase)_selecaoProdutoIMendesViewModel).Valido;
			}
			return false;
		}

		public void Avancar(object obj)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Atenção! É recomendado realizar um backup da base de dados antes de prosseguir, uma vez que este processo é irreversível. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				ConsultaIMendesDto val = _selecaoProdutoIMendesViewModel.RetornarItensSelecionados(obj);
				if (val.Produtos.Any())
				{
					((AbaAuxiliarViewModelBase)_selecaoProdutoIMendesAbaViewModel).Desativar();
					_selecionadosViewModel = new SelecionadosViewModel(val, _apiIMendesService, _impressaoRelatorioService);
					_selecionadosAbaViewModel = new SelecionadosAbaViewModel((IViewModelDtoBase)(object)_selecionadosViewModel);
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_selecionadosAbaViewModel);
					((Formulario)this).PosicionarNaUltimaAba();
				}
			}
		}

		public bool PodeConsultarHistorico()
		{
			return HelperObject.Is<ConsultarHistoricoAcessoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public async Task ConsultarHistorico()
		{
			await _consultarHistoricoAcessoViewModel.Consultar(_service).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		public bool PodeConsultarProdutosAlterados()
		{
			return HelperObject.Is<ConsultarProdutosAlteradosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public async Task ConsultarProdutosAlterados()
		{
			await _consultarProdutosAlteradosViewModel.Consultar(_service).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
			});
		}

		public bool PodeConsultarRegrasFiscaisProdutos()
		{
			if (HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) && ((AbaViewModelBase)_selecionadosAbaViewModel).Valido)
			{
				return !_terminouConsultaComCodigoBarras;
			}
			return false;
		}

		public async Task ConsultarRegrasFiscaisProdutos()
		{
			await _selecionadosViewModel.ConsultarRegrasFiscais(_service).ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					_terminouConsultaComCodigoBarras = true;
					DialogoFactory.MostrarDialogoConfirma("Clique em 'F7 - Processar' para efetivar a correção das tributações dos produtos", false, HorizontalAlignment.Center);
				}
			});
		}

		public bool PodeProcessarProdutos()
		{
			if (DadosSistema.VerificarPermissao<ConsultaProdutoIMendes, Atualizar>() && HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada) && _terminouConsultaComCodigoBarras && !_processouConsultaComCodigoBarras)
			{
				return HelperObject.IsNot<ConsultarHistoricoAcessoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public async Task ProcessarProdutos()
		{
			if (!HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				return;
			}
			List<ConsultaProdutoIMendesDto> list = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesDto>(_selecionadosViewModel.ObterProdutosPodeAtualizar());
			bool atualizarTributacaoFederalEEstadual = _selecionadosViewModel.AtualizarTributacaoFederalEEstadual;
			bool atualizarTributacaoReformaTributaria = _selecionadosViewModel.AtualizarTributacaoReformaTributaria;
			if (!atualizarTributacaoFederalEEstadual && !atualizarTributacaoReformaTributaria)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione pelo menos uma das opções 'Atualizar tributação Federal e Estadual' e/ou 'Atualizar tributação Reforma tributária (IBS/CBS/IS)'", false, HorizontalAlignment.Center);
				return;
			}
			if (list.Any())
			{
				try
				{
					IEnumerable<List<ConsultaProdutoIMendesDto>> enumerable = HelperList.SplitList<ConsultaProdutoIMendesDto>(new List<ConsultaProdutoIMendesDto>(list), 1000);
					List<string> listaErros = new List<string>();
					int total = list.Count;
					int processados = 0;
					foreach (List<ConsultaProdutoIMendesDto> lista in enumerable)
					{
						int percentual = (int)Math.Round((double)processados / (double)total * 100.0);
						ThreadPrincipal.Executar((Action)delegate
						{
							LoaderProcessoService.MostrarLoader($"Processando alterações nos cadastros... {percentual}% concluído");
						});
						processados += lista.Count;
						await _service.Canal<IIMendesModuleService, Task<IEnumerable<string>>>((Func<IIMendesModuleService, Task<IEnumerable<string>>>)((IIMendesModuleService x) => x.AtualizarProdutosConsultados((IEnumerable<ConsultaProdutoIMendesDto>)lista, atualizarTributacaoFederalEEstadual, atualizarTributacaoReformaTributaria))).ContinueWith(delegate(Task<IEnumerable<string>> task)
						{
							if (task.IsFaulted)
							{
								throw new Exception(HelperException.GetLastException((Exception)task.Exception).Message);
							}
							listaErros.AddRange(task.Result);
						});
					}
					if (listaErros.Any())
					{
						string arg = SalvarArquivoDeErros(listaErros);
						DialogoFactory.MostrarDialogoConfirma($"Ocorreram erros durante o processo. \r\nVerifique o arquivo {arg} na área de trabalho", false, HorizontalAlignment.Center);
					}
					else
					{
						DialogoFactory.MostrarDialogoConfirma("Os produtos foram atualizados", false, HorizontalAlignment.Center);
					}
					_processouConsultaComCodigoBarras = true;
					return;
				}
				catch (Exception ex)
				{
					HelperException.ShowLastException(ex, "", false, false);
					return;
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
				}
			}
			DialogoFactory.MostrarDialogoConfirma("Nenhum item com tributação para processar", false, HorizontalAlignment.Center);
		}

		public bool PodeImprimirProdutosConsultados(object obj)
		{
			if (HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				return _terminouConsultaComCodigoBarras;
			}
			return false;
		}

		public void ImprimirProdutosConsultados(object obj)
		{
			if (HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				_selecionadosViewModel.Imprimir(obj);
			}
		}

		public bool PodeVoltar()
		{
			return HelperObject.Is<SelecionadosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public void Voltar()
		{
			((AbaAuxiliarViewModelBase)_selecaoProdutoIMendesAbaViewModel).Ativar();
			if (((Formulario)this).Abas.Contains((IAbaViewModelBase)(object)_selecionadosAbaViewModel))
			{
				((Formulario)this).RemoverAba((IAbaViewModelBase)(object)_selecionadosAbaViewModel);
			}
			_terminouConsultaComCodigoBarras = false;
			_processouConsultaComCodigoBarras = false;
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

		public bool PodeTransferirProdutosAlteradosParaConsulta()
		{
			if (HelperObject.Is<ConsultarProdutosAlteradosViewModel>((object)((Formulario)this).ViewModelAbaSelecionada))
			{
				ConsultarProdutosAlteradosViewModel consultarProdutosAlteradosViewModel = _consultarProdutosAlteradosViewModel;
				bool? obj;
				if (consultarProdutosAlteradosViewModel == null)
				{
					obj = null;
				}
				else
				{
					Alterado alterado = consultarProdutosAlteradosViewModel.Alterado;
					obj = ((alterado == null) ? null : alterado.produto?.Any());
				}
				return obj ?? false;
			}
			return false;
		}

		public void TransferirProdutosAlteradosParaConsulta()
		{
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Expected O, but got Unknown
			List<ConsultaProdutoIMendesDto> list = new List<ConsultaProdutoIMendesDto>();
			foreach (IGrouping<string, Produto> codigo in HelperIEnumerable.ToListEmptyWhenNull<IGrouping<string, Produto>>(from x in _consultarProdutosAlteradosViewModel.Alterado.produto
				group x by x.codigo))
			{
				ConsultaProdutoIMendesDto val = _produtos.FirstOrDefault((ConsultaProdutoIMendesDto x) => HelperString.PreencherComCaractereEsquerda(HelperString.ApenasNumeros(x.CodigoBarras), 14, '0').Equals(codigo.Key));
				if (HelperObject.IsNull((object)val))
				{
					val = _produtos.FirstOrDefault((ConsultaProdutoIMendesDto x) => x.CodigoInterno.ToUpperInvariant().Equals(codigo.Key.ToUpperInvariant()));
				}
				if (!HelperObject.IsNull((object)val))
				{
					list.Add(val);
				}
			}
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum produto localizado para consulta", false, HorizontalAlignment.Center);
				return;
			}
			if (((Formulario)this).Abas.Contains((IAbaViewModelBase)(object)_selecaoProdutoIMendesAbaViewModel))
			{
				((AbaAuxiliarViewModelBase)_selecaoProdutoIMendesAbaViewModel).Desativar();
			}
			if (((Formulario)this).Abas.Contains((IAbaViewModelBase)(object)_selecionadosAbaViewModel))
			{
				((Formulario)this).RemoverAba((IAbaViewModelBase)(object)_selecionadosAbaViewModel);
			}
			PessoaDto val2 = DadosSistema.GetService().Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
			ConsultaIMendesDto consultaIMendesDto = new ConsultaIMendesDto
			{
				FinalidadeMercadoria = val2.Juridica.Emitente.IMendes.FinalidadeMercadoria,
				ProduzidaZonaFrancaManaus = val2.Juridica.Emitente.IMendes.ProduzidaZonaFrancaManaus,
				Cnae = val2.Juridica.Emitente.IMendes.Cnae,
				Produtos = list,
				SanearProdutosSemCodigoBarras = val2.Juridica.Emitente.IMendes.SanearProdutosSemCodigoBarras
			};
			_selecionadosViewModel = new SelecionadosViewModel(consultaIMendesDto, _apiIMendesService, _impressaoRelatorioService);
			_selecionadosAbaViewModel = new SelecionadosAbaViewModel((IViewModelDtoBase)(object)_selecionadosViewModel);
			((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)_selecionadosAbaViewModel);
			((Formulario)this).PosicionarNaUltimaAba();
			_terminouConsultaComCodigoBarras = false;
		}
	}
	public class ConsultarHistoricoAcessoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ConsultarHistoricoAcessoAbaViewModel(IViewModelDtoBase viewModel)
			: base("HISTÓRICO DE ACESSO", "Produtos que você deve corrigir", viewModel)
		{
		}
	}
	public class ConsultarHistoricoAcessoViewModel : ViewModelDtoBase<HistoricoAcesso>
	{
		private readonly ApiIMendesService _apiIMendesService;

		public Lista<ProdutoDevolvidoViewModel> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ProdutoDevolvidoViewModel>>((Expression<Func<Lista<ProdutoDevolvidoViewModel>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ProdutoDevolvidoViewModel>>((Expression<Func<Lista<ProdutoDevolvidoViewModel>>>)(() => Lista), value);
			}
		}

		public override bool Valido => true;

		public HistoricoAcesso HistoricoAcesso
		{
			get
			{
				return ((BindableBase)this).GetProperty<HistoricoAcesso>((Expression<Func<HistoricoAcesso>>)(() => HistoricoAcesso));
			}
			set
			{
				((BindableBase)this).SetProperty<HistoricoAcesso>((Expression<Func<HistoricoAcesso>>)(() => HistoricoAcesso), value);
			}
		}

		public ConsultarHistoricoAcessoViewModel(ApiIMendesService apiIMendesService)
			: base((HistoricoAcesso)null)
		{
			_apiIMendesService = apiIMendesService;
			Lista = new Lista<ProdutoDevolvidoViewModel>();
		}

		public override void ProjectedAsViewModel()
		{
		}

		public async Task Consultar(IWService service)
		{
			_ = 1;
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Consultando histórico de acesso...");
				});
				PessoaDto emitente = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				StatusCliente val = await _apiIMendesService.GetAsync<StatusCliente>((IMendesEndPoint)(object)IMendesGetStatusCliente.Criar().ComCnpj(emitente.CpfCnpj));
				if (!val.sucesso)
				{
					throw new Exception(val.mensagem);
				}
				HistoricoAcesso = await _apiIMendesService.PostAsync<HistoricoAcesso>(IMendesPostEnviaRecebeDados.Criar().ComDados((EnviaRecebeDados)(object)HistoricoAcesso.Criar().ComCnpj(emitente.CpfCnpj)), "application/json");
				Lista.Limpar();
				foreach (ProdutoDevolvido prodDevolvido in HistoricoAcesso.prodDevolvidos)
				{
					ProdutoDevolvido produtoDevolvido = new ProdutoDevolvido(prodDevolvido.id, prodDevolvido.codigo, prodDevolvido.descricao, prodDevolvido.MotivoDevolucaoToText, prodDevolvido.dtinclusao, prodDevolvido.dtdevolucao);
					Lista.Adicionar(new ProdutoDevolvidoViewModel(produtoDevolvido, _apiIMendesService));
				}
				if (!HistoricoAcesso.prodDevolvidos.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum produto devolvido", false, HorizontalAlignment.Center);
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

		public override void Dispose()
		{
			((ViewModelValidationBase)this).Dispose();
			((ViewModelValidationBase)Lista).Dispose();
		}
	}
	public class ConsultasProdutoIMendesViewModel : ModuloBase
	{
		public ConsultasProdutoIMendesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloConsultasProdutoIMendesRegion, "ConsultaProdutoIMendesFichaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class CorrigirCadastroViewModel : ViewModelDtoBase<ProdutoDevolvido>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly ApiIMendesService _apiIMendesService;

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

		public Lista<Produto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<Produto>>((Expression<Func<Lista<Produto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<Produto>>((Expression<Func<Lista<Produto>>>)(() => Lista), value);
			}
		}

		public ICommand PesquisarPorDescricaoCommand { get; set; }

		public CorrigirCadastroViewModel()
		{
		}

		public CorrigirCadastroViewModel(ProdutoDevolvido dto, ApiIMendesService apiIMendesService)
			: base(dto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			_apiIMendesService = apiIMendesService;
			PesquisarPorDescricaoCommand = (ICommand)new DelegateCommand((Action)PesquisarPorDescricao, (Func<bool>)PodePesquisarPorDescricao, (bool?)null);
			Lista = new Lista<Produto>();
		}

		private void PesquisarPorDescricao()
		{
			ThreadPrincipal.Executar((Action)async delegate
			{
				await Consultar();
			});
		}

		public async Task Consultar()
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Consultando produto na base IMendes...");
				});
				PessoaDto val = DadosSistema.GetService().Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				DescricaoProdutos obj = await _apiIMendesService.PostAsync<DescricaoProdutos>(IMendesPostEnviaRecebeDados.Criar().ComDados((EnviaRecebeDados)(object)DescricaoProdutos.Criar().ComCnpj(val.CpfCnpj).ComDescricao(Descricao)), "application/json");
				Lista.Limpar();
				foreach (Produto item in obj.produto)
				{
					Lista.Adicionar(item);
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

		private bool PodePesquisarPorDescricao()
		{
			return !string.IsNullOrEmpty(Descricao);
		}

		public override void ProjectedAsViewModel()
		{
			Descricao = base.Dto.ProdutoServicoEmpresa.ProdutoServico.Descricao;
			PesquisarPorDescricao();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			base.AtualizarDto<T>(ref dto);
			base.Dto.ProdutoRelacionado = Lista.ItemSelecionado;
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new CorrigirCadastroFormulario(base.Dto, _apiIMendesService);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)this).Dispose();
			((ViewModelValidationBase)Lista).Dispose();
		}
	}
	public class ProdutoDevolvido : BindableBase
	{
		public long Id { get; protected set; }

		public string Codigo { get; protected set; }

		public string Descricao { get; protected set; }

		public string MotivoDevolucao { get; protected set; }

		public DateTime? DataInclusao { get; protected set; }

		public DateTime? DataDevolucao { get; protected set; }

		public ProdutoServicoEmpresaDto ProdutoServicoEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa), value);
			}
		}

		public Produto ProdutoRelacionado { get; set; }

		public ProdutoDevolvido(long id, string codigo, string descricao, string motivoDevolucao, DateTime? dataInclusao, DateTime? dataDevolucao)
		{
			Id = id;
			Codigo = codigo;
			Descricao = descricao;
			MotivoDevolucao = motivoDevolucao;
			DataInclusao = dataInclusao;
			DataDevolucao = dataDevolucao;
		}

		public void SetarProdutoServicoEmpresa(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			ProdutoServicoEmpresa = produtoServicoEmpresa;
		}
	}
	public class ProdutoDevolvidoViewModel : BindableBase
	{
		private readonly ApiIMendesService _apiIMendesService;

		public ISelecaoCommandParameter CorrigirCadastro
		{
			get
			{
				SelecaoCommandParameter<ProdutoDevolvido> obj = new SelecaoCommandParameter<ProdutoDevolvido>("CorrigirCadastroFichaView", (Action<ProdutoDevolvido>)CallBackCorrigirCadastro, true);
				obj.AdicionarParametro("Dto", (object)ProdutoDevolvido);
				obj.AdicionarParametro("Api", (object)_apiIMendesService);
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public bool Corrigido
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Corrigido));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Corrigido), value);
			}
		}

		public ProdutoDevolvido ProdutoDevolvido { get; set; }

		public ProdutoDevolvidoViewModel(ProdutoDevolvido produtoDevolvido, ApiIMendesService apiIMendesService)
		{
			_apiIMendesService = apiIMendesService;
			ProdutoDevolvido = produtoDevolvido;
		}

		private async void CallBackCorrigirCadastro(ProdutoDevolvido dto)
		{
			Corrigido = HelperObject.IsNotNull((object)dto.ProdutoRelacionado);
			if (!Corrigido)
			{
				return;
			}
			IWService service = DadosSistema.GetService();
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Consultando produto na base IMendes...");
				});
				ProdutoServicoEmpresaDto val = await service.Canal<IIMendesModuleService, Task<ProdutoServicoEmpresaDto>>((Func<IIMendesModuleService, Task<ProdutoServicoEmpresaDto>>)((IIMendesModuleService x) => x.ObterProdutoPorCodigoBarrasOuCodigoIMendesSaneamentoGrades((string)null, HelperObject.ToStringOrEmpty((object)dto.ProdutoRelacionado.id), (ProdutoServicoEmpresaDto)null)));
				dto.ProdutoServicoEmpresa.ProdutoServico.Descricao = dto.ProdutoRelacionado.descricao;
				if (!string.IsNullOrEmpty(dto.ProdutoRelacionado.ean))
				{
					dto.ProdutoServicoEmpresa.ProdutoServico.Produto.CodigoBarras = dto.ProdutoRelacionado.ean;
				}
				dto.ProdutoServicoEmpresa.ProdutoServico.Produto.CodigoIMendes = dto.ProdutoRelacionado.id.ToString();
				dto.ProdutoServicoEmpresa.TributacaoFederal = val.TributacaoFederal;
				dto.ProdutoServicoEmpresa.ProdutoEmpresa.TributacaoEstadual = val.ProdutoEmpresa.TributacaoEstadual;
				dto.ProdutoServicoEmpresa.ProdutoEmpresa.CodigoEspecificadorSubstituicaoTributaria = val.ProdutoEmpresa.CodigoEspecificadorSubstituicaoTributaria;
				if (HelperObject.IsNot<NCMNBSDtoNull>((object)val.NcmNbs))
				{
					dto.ProdutoServicoEmpresa.NcmNbs = val.NcmNbs;
				}
				await service.Canal<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => ((IAtualizar<ProdutoServicoEmpresaDto>)(object)x).AtualizarAsync(dto.ProdutoServicoEmpresa)));
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				return;
			}
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Removendo produtos devolvidos da base IMendes...");
				});
				PessoaDto val2 = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
				List<Produto> list = new List<Produto> { Produto.Criar().ComId(dto.Id.ToString()) };
				StatusRemoveDevolvidos val3 = await _apiIMendesService.PostAsync<StatusRemoveDevolvidos>(IMendesPostEnviaRecebeDados.Criar().ComDados((EnviaRecebeDados)(object)RemoveDevolvidos.Criar().ComDados(Dados.Criar().ComCnpj(val2.CpfCnpj).ComProdutos((IList<Produto>)list))), "application/json");
				if (!val3.sucesso)
				{
					DialogoFactory.MostrarDialogoConfirma(val3.mensagem, false, HorizontalAlignment.Center);
				}
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			}
			catch (Exception ex2)
			{
				HelperException.ShowLastException(ex2, "", false, false);
			}
		}
	}
	public class SelecaoProdutoIMendesAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SelecaoProdutoIMendesAbaViewModel(IViewModelDtoBase viewModel)
			: base("SANEAMENTO", "Produtos para consulta", viewModel)
		{
		}
	}
	public class SelecaoProdutoIMendesViewModel : ViewModelDtoBase<IEnumerable<ConsultaProdutoIMendesDto>>
	{
		public Lista<KeyValuePair<string, FinalidadeMercadoria>> FinalidadeMercadoria
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, FinalidadeMercadoria>>>((Expression<Func<Lista<KeyValuePair<string, FinalidadeMercadoria>>>>)(() => FinalidadeMercadoria));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, FinalidadeMercadoria>>>((Expression<Func<Lista<KeyValuePair<string, FinalidadeMercadoria>>>>)(() => FinalidadeMercadoria), value);
			}
		}

		public bool ProduzidaZonaFrancaManaus
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ProduzidaZonaFrancaManaus));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ProduzidaZonaFrancaManaus), value);
			}
		}

		public string Cnae
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cnae));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cnae), value);
			}
		}

		public bool SanearProdutosSemCodigoBarras
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SanearProdutosSemCodigoBarras));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SanearProdutosSemCodigoBarras), value);
			}
		}

		public override bool Valido => true;

		public string NomeGridControl => "SelecaoProduto";

		public SelecaoProdutoIMendesViewModel()
		{
		}

		public SelecaoProdutoIMendesViewModel(IEnumerable<ConsultaProdutoIMendesDto> produtos)
			: base(produtos)
		{
		}

		public override void ProjectedAsViewModel()
		{
			PessoaDto emitente = DadosSistema.GetService().Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
			FinalidadeMercadoria = new Lista<KeyValuePair<string, FinalidadeMercadoria>>();
			FinalidadeMercadoria.Adicionar((IEnumerable<KeyValuePair<string, FinalidadeMercadoria>>)HelperEnum.ObterParDeValores<FinalidadeMercadoria>());
			FinalidadeMercadoria.Posicionar((Expression<Func<KeyValuePair<string, FinalidadeMercadoria>, bool>>)((KeyValuePair<string, FinalidadeMercadoria> x) => (int)x.Value == (int)emitente.Juridica.Emitente.IMendes.FinalidadeMercadoria));
			ProduzidaZonaFrancaManaus = emitente.Juridica.Emitente.IMendes.ProduzidaZonaFrancaManaus;
			Cnae = emitente.Juridica.Emitente.IMendes.Cnae;
			SanearProdutosSemCodigoBarras = emitente.Juridica.Emitente.IMendes.SanearProdutosSemCodigoBarras;
		}

		public ConsultaIMendesDto RetornarItensSelecionados(object obj)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Expected O, but got Unknown
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Expected O, but got Unknown
			IEnumerable<ConsultaProdutoIMendesDto> enumerable = ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ConsultaProdutoIMendesDto>();
			ConsultaIMendesDto result = new ConsultaIMendesDto
			{
				FinalidadeMercadoria = FinalidadeMercadoria.ItemSelecionado.Value,
				ProduzidaZonaFrancaManaus = ProduzidaZonaFrancaManaus,
				Cnae = HelperString.ToStringEmptyWhenNull(Cnae),
				Produtos = enumerable,
				SanearProdutosSemCodigoBarras = SanearProdutosSemCodigoBarras
			};
			if (enumerable.Any())
			{
				return result;
			}
			if (DialogoFactory.MostrarDialogoSimNao("Consultar todos os produtos?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return result;
			}
			return new ConsultaIMendesDto
			{
				FinalidadeMercadoria = FinalidadeMercadoria.ItemSelecionado.Value,
				ProduzidaZonaFrancaManaus = ProduzidaZonaFrancaManaus,
				Cnae = Cnae,
				Produtos = base.Dto,
				SanearProdutosSemCodigoBarras = SanearProdutosSemCodigoBarras
			};
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
	public class SimulacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SimulacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SIMULAÇÃO", "Produtos para simulação", viewModel)
		{
		}
	}
	public class SimulacaoViewModel : ViewModelDtoBase<IEnumerable<ConsultaProdutoIMendesDto>>
	{
		private const int LimiteSelecao = 30;

		private bool _ajustandoSelecao;

		public bool SelecionarPrimeirosTrinta
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SelecionarPrimeirosTrinta));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SelecionarPrimeirosTrinta), value);
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

		public bool FocoNoEmail
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoEmail));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoEmail), value);
			}
		}

		public bool SubstituicaoTributaria
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SubstituicaoTributaria));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SubstituicaoTributaria), value);
			}
		}

		public bool FabricacaoPropria
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FabricacaoPropria));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FabricacaoPropria), value);
			}
		}

		public override bool Valido => true;

		public string NomeGridControl => "SimulacaoProduto";

		public ICommand SelecionarPrimeirosTrintaCommand { get; set; }

		public ICommand LimparSelecaoCommand { get; set; }

		public ICommand ValidarLimiteSelecaoCommand { get; set; }

		public SimulacaoViewModel()
		{
		}

		public SimulacaoViewModel(IEnumerable<ConsultaProdutoIMendesDto> produtos)
			: base(produtos)
		{
			SelecionarPrimeirosTrintaCommand = (ICommand)new DelegateCommand<object>((Action<object>)SelecionarPrimeirosTrintaExecute);
			LimparSelecaoCommand = (ICommand)new DelegateCommand<object>((Action<object>)LimparSelecao);
			ValidarLimiteSelecaoCommand = (ICommand)new DelegateCommand<object>((Action<object>)ValidarLimiteSelecao);
		}

		private void SelecionarPrimeirosTrintaExecute(object view)
		{
			GridControl val = ObterGrid(view as FrameworkElement);
			if (HelperObject.IsNull((object)val))
			{
				return;
			}
			((DataControlBase)val).SelectedItems.Clear();
			for (int i = 0; i < ((DataControlBase)val).VisibleRowCount; i++)
			{
				if (((DataControlBase)val).SelectedItems.Count >= 30)
				{
					break;
				}
				object row = ((DataControlBase)val).GetRow(val.GetRowHandleByVisibleIndex(i));
				if (HelperObject.IsNotNull(row))
				{
					((DataControlBase)val).SelectedItems.Add(row);
				}
			}
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			if (HelperObject.IsNull((object)view))
			{
				return null;
			}
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(NomeGridControl);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			return val.Property;
		}

		private void LimparSelecao(object view)
		{
			GridControl val = ObterGrid(view as FrameworkElement);
			if (!HelperObject.IsNull((object)val))
			{
				((DataControlBase)val).SelectedItems.Clear();
			}
		}

		private void ValidarLimiteSelecao(object parametro)
		{
			if (_ajustandoSelecao)
			{
				return;
			}
			IList selecionados = parametro as IList;
			if (HelperObject.IsNull((object)selecionados) || selecionados.Count <= 30)
			{
				return;
			}
			_ajustandoSelecao = true;
			Application.Current.Dispatcher.BeginInvoke((Action)delegate
			{
				try
				{
					while (selecionados.Count > 30)
					{
						selecionados.RemoveAt(selecionados.Count - 1);
					}
					DialogoFactory.MostrarDialogoConfirma($"Só é possível selecionar até {30} produtos para a simulação.", false, HorizontalAlignment.Center);
				}
				finally
				{
					_ajustandoSelecao = false;
				}
			});
		}

		public override void ProjectedAsViewModel()
		{
		}

		public ConsultaIMendesDto RetornarItensSelecionados(object view)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			GridControl obj = ObterGrid(view as FrameworkElement);
			List<ConsultaProdutoIMendesDto> produtos = ((obj != null) ? HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesDto>(((DataControlBase)obj).SelectedItems.OfType<ConsultaProdutoIMendesDto>()) : null);
			return new ConsultaIMendesDto
			{
				Email = HelperString.ToStringEmptyWhenNull(Email),
				Produtos = produtos,
				SubstituicaoTributaria = SubstituicaoTributaria,
				FabricacaoPropria = FabricacaoPropria
			};
		}
	}
	public class SelecionadosAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SelecionadosAbaViewModel(IViewModelDtoBase viewModel)
			: base("SELECIONADOS", "Prontos para saneamento", viewModel)
		{
		}
	}
	public class SelecionadosViewModel : ViewModelDtoBase<IEnumerable<ConsultaProdutoIMendes>>, IViewModelImprimirGrid, IViewModelImprimir
	{
		private readonly ConsultaIMendesDto _consultaIMendesDto;

		private readonly ApiIMendesService _apiIMendesService;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ObservableCollection<ConsultaProdutoIMendes> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<ConsultaProdutoIMendes>>((Expression<Func<ObservableCollection<ConsultaProdutoIMendes>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<ConsultaProdutoIMendes>>((Expression<Func<ObservableCollection<ConsultaProdutoIMendes>>>)(() => Lista), value);
			}
		}

		public bool AtualizarTributacaoFederalEEstadual
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AtualizarTributacaoFederalEEstadual));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AtualizarTributacaoFederalEEstadual), value);
			}
		}

		public bool AtualizarTributacaoReformaTributaria
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AtualizarTributacaoReformaTributaria));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AtualizarTributacaoReformaTributaria), value);
			}
		}

		public override bool Valido => true;

		public ConsultaProdutoIMendes Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<ConsultaProdutoIMendes>((Expression<Func<ConsultaProdutoIMendes>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<ConsultaProdutoIMendes>((Expression<Func<ConsultaProdutoIMendes>>)(() => Selecionado), value);
			}
		}

		public string TituloRelatorio => "Produtos consultados";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => true;

		public string TagProperty => "ProdutosConsultados";

		public SelecionadosViewModel(ConsultaIMendesDto consultaIMendesDto, ApiIMendesService apiIMendesService, IImpressaoRelatorioService impressaoRelatorioService)
			: base(consultaIMendesDto.Produtos.Select((ConsultaProdutoIMendesDto x) => new ConsultaProdutoIMendes(x)))
		{
			_consultaIMendesDto = consultaIMendesDto;
			_apiIMendesService = apiIMendesService;
			_impressaoRelatorioService = impressaoRelatorioService;
			AtualizarTributacaoFederalEEstadual = true;
			AtualizarTributacaoReformaTributaria = true;
			Lista = new ObservableCollection<ConsultaProdutoIMendes>(base.Dto);
		}

		public async Task ConsultarRegrasFiscais(IWService service)
		{
			if (!Lista.Any())
			{
				return;
			}
			PessoaDto emitente = service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
			bool? obj;
			if (emitente == null)
			{
				obj = null;
			}
			else
			{
				JuridicaDto juridica = emitente.Juridica;
				if (juridica == null)
				{
					obj = null;
				}
				else
				{
					EmitenteDto emitente2 = juridica.Emitente;
					if (emitente2 == null)
					{
						obj = null;
					}
					else
					{
						RegimeTributarioDto regimeTributario = emitente2.RegimeTributario;
						obj = ((regimeTributario != null) ? new bool?(regimeTributario.IsRegimeNormal) : null);
					}
				}
			}
			if (obj ?? false)
			{
				JuridicaDto juridica2 = emitente.Juridica;
				int num;
				if (juridica2 == null)
				{
					num = 1;
				}
				else
				{
					EmitenteDto emitente3 = juridica2.Emitente;
					CodigoFaixa? obj2;
					if (emitente3 == null)
					{
						obj2 = null;
					}
					else
					{
						IMendesDto iMendes = emitente3.IMendes;
						obj2 = ((iMendes != null) ? new CodigoFaixa?(iMendes.CodigoFaixa) : null);
					}
					num = ((obj2 != (CodigoFaixa?)1) ? 1 : 0);
				}
				if (num != 0)
				{
					JuridicaDto juridica3 = emitente.Juridica;
					int num2;
					if (juridica3 == null)
					{
						num2 = 1;
					}
					else
					{
						EmitenteDto emitente4 = juridica3.Emitente;
						CodigoFaixa? obj3;
						if (emitente4 == null)
						{
							obj3 = null;
						}
						else
						{
							IMendesDto iMendes2 = emitente4.IMendes;
							obj3 = ((iMendes2 != null) ? new CodigoFaixa?(iMendes2.CodigoFaixa) : null);
						}
						num2 = ((obj3 != (CodigoFaixa?)0) ? 1 : 0);
					}
					if (num2 != 0)
					{
						throw new Exception("O código da faixa informado no cadastro do emitente não é compatível com o regime tributário");
					}
				}
			}
			string cpfCnpjEmitente = emitente.CpfCnpj;
			StatusCliente val = await _apiIMendesService.GetAsync<StatusCliente>((IMendesEndPoint)(object)IMendesGetStatusCliente.Criar().ComCnpj(cpfCnpjEmitente));
			if (!val.sucesso)
			{
				throw new Exception(val.mensagem);
			}
			int num3 = DadosSistema.ConfiguracaoDto.Geral.NumeroRegistrosSaneamentoIMendes.Valor;
			if (num3 > 400)
			{
				num3 = 400;
			}
			IEnumerable<List<ConsultaProdutoIMendes>> enumerable = HelperList.SplitList<ConsultaProdutoIMendes>(new List<ConsultaProdutoIMendes>(Lista), (num3 <= 0) ? 100 : num3);
			int total = Lista.Count;
			int processados = 0;
			foreach (List<ConsultaProdutoIMendes> lista in enumerable)
			{
				int percentual = (int)Math.Round((double)processados / (double)total * 100.0);
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader($"Consultando produto na base IMendes... {percentual}% concluído");
				});
				processados += lista.Count;
				List<Produto> list = lista.Select((ConsultaProdutoIMendes item) => Produto.Criar().ComCodigo((!string.IsNullOrEmpty(HelperString.ApenasNumeros(item.Dto.CodigoBarras))) ? HelperString.ApenasNumeros(item.Dto.CodigoBarras) : item.Dto.CodigoInterno.ToUpperInvariant()).ComCodigoIMendes((!string.IsNullOrEmpty(item.Dto.CodigoIMendes)) ? item.Dto.CodigoIMendes : null)
					.ComDescricao(item.Dto.Descricao)
					.ComNcm(item.Dto.Ncm)).ToList();
				try
				{
					List<Uf> list2 = new List<Uf> { Uf.Criar().ComUf(emitente.EnderecoPrincipal.Municipio.UF.Sigla) };
					JuridicaDto juridica4 = emitente.Juridica;
					bool? obj4;
					if (juridica4 == null)
					{
						obj4 = null;
					}
					else
					{
						EmitenteDto emitente5 = juridica4.Emitente;
						if (emitente5 == null)
						{
							obj4 = null;
						}
						else
						{
							IMendesDto iMendes3 = emitente5.IMendes;
							obj4 = ((iMendes3 != null) ? new bool?(HelperObject.IsNotNull((object)iMendes3.Ufs)) : null);
						}
					}
					if (obj4 ?? false)
					{
						JuridicaDto juridica5 = emitente.Juridica;
						bool? obj5;
						if (juridica5 == null)
						{
							obj5 = null;
						}
						else
						{
							EmitenteDto emitente6 = juridica5.Emitente;
							if (emitente6 == null)
							{
								obj5 = null;
							}
							else
							{
								IMendesDto iMendes4 = emitente6.IMendes;
								obj5 = ((iMendes4 != null) ? new bool?(iMendes4.Ufs.Any()) : null);
							}
						}
						if (obj5 ?? false)
						{
							JuridicaDto juridica6 = emitente.Juridica;
							object collection;
							if (juridica6 == null)
							{
								collection = null;
							}
							else
							{
								EmitenteDto emitente7 = juridica6.Emitente;
								if (emitente7 == null)
								{
									collection = null;
								}
								else
								{
									IMendesDto iMendes5 = emitente7.IMendes;
									collection = ((iMendes5 != null) ? iMendes5.Ufs.Select((UFDto x) => Uf.Criar().ComUf(x.Sigla)) : null);
								}
							}
							list2.AddRange((IEnumerable<Uf>)collection);
						}
					}
					List<CnaeCaracTrib> list3 = new List<CnaeCaracTrib> { (CnaeCaracTrib)8 };
					JuridicaDto juridica7 = emitente.Juridica;
					bool? obj6;
					if (juridica7 == null)
					{
						obj6 = null;
					}
					else
					{
						EmitenteDto emitente8 = juridica7.Emitente;
						if (emitente8 == null)
						{
							obj6 = null;
						}
						else
						{
							IMendesDto iMendes6 = emitente8.IMendes;
							obj6 = ((iMendes6 != null) ? new bool?(HelperObject.IsNotNull((object)iMendes6.CaracteristicasTributarias)) : null);
						}
					}
					if (obj6 ?? false)
					{
						JuridicaDto juridica8 = emitente.Juridica;
						bool? obj7;
						if (juridica8 == null)
						{
							obj7 = null;
						}
						else
						{
							EmitenteDto emitente9 = juridica8.Emitente;
							if (emitente9 == null)
							{
								obj7 = null;
							}
							else
							{
								IMendesDto iMendes7 = emitente9.IMendes;
								obj7 = ((iMendes7 != null) ? new bool?(iMendes7.CaracteristicasTributarias.Any()) : null);
							}
						}
						if (obj7 ?? false)
						{
							JuridicaDto juridica9 = emitente.Juridica;
							object collection2;
							if (juridica9 == null)
							{
								collection2 = null;
							}
							else
							{
								EmitenteDto emitente10 = juridica9.Emitente;
								if (emitente10 == null)
								{
									collection2 = null;
								}
								else
								{
									IMendesDto iMendes8 = emitente10.IMendes;
									collection2 = ((iMendes8 != null) ? iMendes8.CaracteristicasTributarias : null);
								}
							}
							list3.AddRange((IEnumerable<CnaeCaracTrib>)collection2);
						}
					}
					SaneamentoGrades obj8 = SaneamentoGrades.Criar();
					Emit obj9 = Emit.Criar().ComAmbiente(2).ComCnpj(HelperString.RemoveCaracteresAlfanumerico(cpfCnpjEmitente));
					JuridicaDto juridica10 = emitente.Juridica;
					bool? obj10;
					if (juridica10 == null)
					{
						obj10 = null;
					}
					else
					{
						EmitenteDto emitente11 = juridica10.Emitente;
						obj10 = ((emitente11 != null) ? new bool?(emitente11.RegimeTributario.Codigo.Equals(5)) : null);
					}
					int num4;
					if (!(obj10 ?? false))
					{
						JuridicaDto juridica11 = emitente.Juridica;
						int? obj11;
						if (juridica11 == null)
						{
							obj11 = null;
						}
						else
						{
							EmitenteDto emitente12 = juridica11.Emitente;
							obj11 = ((emitente12 != null) ? new int?(emitente12.RegimeTributario.Codigo) : null);
						}
						num4 = obj11 ?? 1;
					}
					else
					{
						num4 = 1;
					}
					Emit obj12 = obj9.ComCrt(num4);
					JuridicaDto juridica12 = emitente.Juridica;
					CodigoFaixa? obj13;
					if (juridica12 == null)
					{
						obj13 = null;
					}
					else
					{
						EmitenteDto emitente13 = juridica12.Emitente;
						if (emitente13 == null)
						{
							obj13 = null;
						}
						else
						{
							IMendesDto iMendes9 = emitente13.IMendes;
							obj13 = ((iMendes9 != null) ? new CodigoFaixa?(iMendes9.CodigoFaixa) : null);
						}
					}
					Emit obj14 = obj12.ComRegimeTributario((CodigoFaixa)(((int?)obj13) ?? 2));
					EnderecoDto enderecoPrincipal = emitente.EnderecoPrincipal;
					object obj15;
					if (enderecoPrincipal == null)
					{
						obj15 = null;
					}
					else
					{
						MunicipioDto municipio = enderecoPrincipal.Municipio;
						if (municipio == null)
						{
							obj15 = null;
						}
						else
						{
							UFDto uF = municipio.UF;
							obj15 = ((uF != null) ? uF.Sigla : null);
						}
					}
					if (obj15 == null)
					{
						obj15 = "SC";
					}
					Emit obj16 = obj14.ComUf((string)obj15);
					object obj17;
					if (!HelperString.IsNullOrEmpty(_consultaIMendesDto.Cnae))
					{
						obj17 = _consultaIMendesDto.Cnae;
					}
					else
					{
						JuridicaDto juridica13 = emitente.Juridica;
						if (juridica13 == null)
						{
							obj17 = null;
						}
						else
						{
							EmitenteDto emitente14 = juridica13.Emitente;
							obj17 = ((emitente14 != null) ? emitente14.Cnae : null);
						}
					}
					Emit obj18 = obj16.ComCnae((string)obj17);
					JuridicaDto juridica14 = emitente.Juridica;
					object obj19;
					if (juridica14 == null)
					{
						obj19 = null;
					}
					else
					{
						EmitenteDto emitente15 = juridica14.Emitente;
						if (emitente15 == null)
						{
							obj19 = null;
						}
						else
						{
							IMendesDto iMendes10 = emitente15.IMendes;
							obj19 = ((iMendes10 != null) ? HelperEnum.RetornarDescricaoCurta((Enum)(object)iMendes10.RegimeEspecial) : null);
						}
					}
					Emit obj20 = obj18.ComRegimeEspecial((string)obj19);
					JuridicaDto juridica15 = emitente.Juridica;
					SubstitutoDoIcms? obj21;
					if (juridica15 == null)
					{
						obj21 = null;
					}
					else
					{
						EmitenteDto emitente16 = juridica15.Emitente;
						if (emitente16 == null)
						{
							obj21 = null;
						}
						else
						{
							IMendesDto iMendes11 = emitente16.IMendes;
							obj21 = ((iMendes11 != null) ? new SubstitutoDoIcms?(iMendes11.SubstitutoDoIcms) : null);
						}
					}
					SaneamentoGrades obj22 = obj8.ComEmit(obj20.ComSubstitutoTributario((SubstitutoDoIcms)(((int?)obj21) ?? 1)));
					Perfil obj23 = Perfil.Criar().ComUfs((IList<Uf>)list2).ComCfop(5102)
						.ComCaracteristicaTributaria((IList<CnaeCaracTrib>)list3)
						.ComFinalidade(_consultaIMendesDto.FinalidadeMercadoria)
						.ComDestinatarioNoSimplesNacional(false)
						.ComOrigem(0);
					JuridicaDto juridica16 = emitente.Juridica;
					SubstitutoDoIcms? obj24;
					if (juridica16 == null)
					{
						obj24 = null;
					}
					else
					{
						EmitenteDto emitente17 = juridica16.Emitente;
						if (emitente17 == null)
						{
							obj24 = null;
						}
						else
						{
							IMendesDto iMendes12 = emitente17.IMendes;
							obj24 = ((iMendes12 != null) ? new SubstitutoDoIcms?(iMendes12.SubstitutoDoIcms) : null);
						}
					}
					SaneamentoGrades val2 = obj22.ComPerfil(obj23.ComSubstitutoTributario((SubstitutoDoIcms)(((int?)obj24) ?? 1)).ComProduzidaZonaFrancaManaus((ProduzidaZonaFrancaManaus)(!_consultaIMendesDto.ProduzidaZonaFrancaManaus))).ComProdutos((IList<Produto>)list);
					RetornoProduto val3 = await _apiIMendesService.PostAsync(IMendesPostSaneamentoGrades.Criar().ComSaneamentoGrades(val2), "application/json");
					if (!val3.cabecalho.mensagem.ToLower().Equals("ok"))
					{
						DialogoFactory.MostrarDialogoConfirma(val3.cabecalho.mensagem, false, HorizontalAlignment.Center);
					}
					foreach (ConsultaProdutoIMendes item in lista)
					{
						item.StatusProduto = HelperEnum.RetornarDescricao((Enum)(object)(StatusProduto)2);
						Selecionado = item;
					}
					foreach (Grupo item2 in val3.grupos.Where((Grupo x) => x.regras.Any()))
					{
						IList<string> prodEan = item2.prodEan;
						if (HelperObject.IsNotNull((object)prodEan) && prodEan.Any())
						{
							foreach (string codigoProduto3 in prodEan)
							{
								IEnumerable<ConsultaProdutoIMendes> enumerable2 = lista.Where((ConsultaProdutoIMendes x) => HelperString.PreencherComCaractereEsquerda(HelperString.ApenasNumeros(x.Dto.CodigoBarras), 14, '0').Equals(codigoProduto3));
								if (!enumerable2.Any())
								{
									enumerable2 = lista.Where((ConsultaProdutoIMendes x) => x.Dto.CodigoInterno.Equals(codigoProduto3));
								}
								foreach (ConsultaProdutoIMendes item3 in enumerable2)
								{
									item3.StatusProduto = HelperEnum.RetornarDescricao((Enum)(object)(StatusProduto)1);
									item3.Dto.Grupo = item2;
									item3.Dto.SaneamentoGradeGrupo = SaneamentoGradeGrupoProjections.ProjectedAsDto(item2);
								}
							}
						}
						prodEan = item2.prodCodImendes;
						if (HelperObject.IsNotNull((object)prodEan) && prodEan.Any())
						{
							foreach (string codigoProduto2 in prodEan)
							{
								IEnumerable<ConsultaProdutoIMendes> enumerable3 = lista.Where((ConsultaProdutoIMendes x) => x.Dto.CodigoIMendes.Equals(codigoProduto2));
								if (!enumerable3.Any())
								{
									continue;
								}
								foreach (ConsultaProdutoIMendes item4 in enumerable3)
								{
									item4.StatusProduto = HelperEnum.RetornarDescricao((Enum)(object)(StatusProduto)1);
									item4.Dto.Grupo = item2;
									item4.Dto.SaneamentoGradeGrupo = SaneamentoGradeGrupoProjections.ProjectedAsDto(item2);
								}
							}
						}
						if (!_consultaIMendesDto.SanearProdutosSemCodigoBarras)
						{
							continue;
						}
						prodEan = item2.prodCodInterno;
						if (!HelperObject.IsNotNull((object)prodEan) || !prodEan.Any())
						{
							continue;
						}
						foreach (string codigoProduto in prodEan)
						{
							IEnumerable<ConsultaProdutoIMendes> enumerable4 = lista.Where((ConsultaProdutoIMendes x) => x.Dto.CodigoInterno.Equals(codigoProduto));
							if (!enumerable4.Any())
							{
								continue;
							}
							foreach (ConsultaProdutoIMendes item5 in enumerable4)
							{
								item5.StatusProduto = HelperEnum.RetornarDescricao((Enum)(object)(StatusProduto)1);
								item5.Dto.Grupo = item2;
								item5.Dto.SaneamentoGradeGrupo = SaneamentoGradeGrupoProjections.ProjectedAsDto(item2);
							}
						}
					}
				}
				catch (BadRequestException ex)
				{
					HelperException.ShowLastException((Exception)ex, "", false, false);
				}
				catch (Exception ex2)
				{
					HelperException.ShowLastException(ex2, "", false, false);
				}
			}
			ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public IEnumerable<ConsultaProdutoIMendesDto> ObterProdutosPodeAtualizar()
		{
			return from x in Lista
				where HelperObject.IsNot<SaneamentoGradeGrupoDtoNull>((object)x.Dto.SaneamentoGradeGrupo)
				select x into y
				select y.Dto;
		}

		public void Imprimir(object obj)
		{
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)this).Dispose();
			Lista = null;
		}
	}
}
namespace Otus.Modulo.ConsultasProdutoIMendes.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.ConsultasProdutoIMendes.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.ConsultasProdutoIMendes.Projections
{
	public static class GrupoProjections
	{
		public static GrupoRegraFiscalDto ProjectedAsDto(this Grupo grupo)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			return new GrupoRegraFiscalDto
			{
				Lista = HelperString.ToStringEmptyWhenNull(grupo.lista),
				Tipo = HelperString.ToStringEmptyWhenNull(grupo.tipo),
				Codigo = grupo.codigo,
				Produtos = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)grupo.produto),
				CodIMendes = HelperIEnumerable.ToListEmptyWhenNull<string>((IEnumerable<string>)grupo.codIMendes),
				Ncm = HelperString.ToStringEmptyWhenNull(grupo.ncm),
				Cest = HelperString.ToStringEmptyWhenNull(grupo.cest),
				CodigoAnp = HelperString.ToStringEmptyWhenNull(grupo.codAnp),
				Regras = new List<RegraDto>(grupo.regra.ProjectedAsCollectionDto()),
				Ipi = grupo.ipi.ProjectedAsDto(),
				PisCofins = grupo.piscofins.ProjectedAsDto()
			};
		}

		public static IEnumerable<GrupoRegraFiscalDto> ProjectedAsCollectionDto(this IEnumerable<Grupo> grupos)
		{
			List<Grupo> source = HelperIEnumerable.ToListEmptyWhenNull<Grupo>(grupos);
			if (!source.Any())
			{
				return Enumerable.Empty<GrupoRegraFiscalDto>();
			}
			return source.Select(ProjectedAsDto);
		}
	}
	public static class IpiRegraFiscalProjections
	{
		public static IpiRegraFiscalDto ProjectedAsDto(this Ipi ipi)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Expected O, but got Unknown
			return new IpiRegraFiscalDto
			{
				Percentual = ipi.aliqIPI,
				CodigoEnquadramento = HelperString.ToStringEmptyWhenNull(ipi.codenq),
				CstEntrada = HelperString.ToStringEmptyWhenNull(ipi.cstEnt),
				CstSaida = HelperString.ToStringEmptyWhenNull(ipi.cstSai),
				Ex = HelperString.ToStringEmptyWhenNull(ipi.ex)
			};
		}
	}
	public static class PisCofinsRegraFiscalProjections
	{
		public static PisCofinsRegraFiscalDto ProjectedAsDto(this PisCofins pisCofins)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			return new PisCofinsRegraFiscalDto
			{
				CstEnt = HelperString.ToStringEmptyWhenNull(pisCofins.cstEnt),
				CstSai = HelperString.ToStringEmptyWhenNull(pisCofins.cstSai),
				DataFimVigencia = (pisCofins.dtVigFin ?? DateTime.MinValue),
				DataInicioVigencia = (pisCofins.dtVigIni ?? DateTime.MinValue),
				Nri = HelperString.ToStringEmptyWhenNull(pisCofins.nri),
				AmparoLegal = HelperString.ToStringEmptyWhenNull(pisCofins.ampLegal),
				PercentualCofins = pisCofins.aliqCOFINS,
				PercentualPis = pisCofins.aliqPIS
			};
		}
	}
	public static class RegraProjections
	{
		public static RegraDto ProjectedAsDto(this Regra regra)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_0171: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Expected O, but got Unknown
			return new RegraDto
			{
				Uf = HelperString.ToStringEmptyWhenNull(regra.uf),
				Codigo = regra.codigo,
				PercentualIcms = regra.aliqicms,
				TributacaoEcf = TributacaoPdvHelper.ObterPorSimboloPdv(regra.simbPDV),
				DataInicioVigencia = (regra.dtVigIni ?? DateTime.MinValue),
				DataFimVigencia = (regra.dtVigFin ?? DateTime.MinValue),
				Antecipado = HelperString.ToStringEmptyWhenNull(regra.antecipado),
				CfopCompra = regra.cfopCompra,
				CfopVenda = regra.cfopVenda,
				CodigoBeneficioFiscal = HelperString.ToStringEmptyWhenNull(regra.codBenef),
				Csosn = HelperString.ToStringEmptyWhenNull(regra.csosn),
				Cst = HelperString.ToStringEmptyWhenNull(regra.cst),
				Desonerado = HelperString.ToStringEmptyWhenNull(regra.desonerado),
				Excecao = HelperString.ToStringEmptyWhenNull(regra.excecao),
				Fcp = regra.fcp,
				IcmsDesonerado = regra.icmsdeson,
				MotivoIcmsDesonarado = regra.motdesicms,
				Iva = regra.iva,
				PercentualDiferimento = regra.pDifer,
				PercentualIcmsPdv = regra.pICMSPDV,
				PercentualIcmsSt = regra.aliqicmsst,
				ReducaoBaseCalculoIcms = regra.reducaobcicms,
				ReducaoBaseCalculoIcmsSt = regra.reducaobcicmsst,
				ReducaoBaseCalculoIcmsEfetivo = regra.pRedBCEfet,
				PercentualIcmsEfetivo = regra.pICMSEfet,
				Cest = HelperString.ToStringEmptyWhenNull(regra.cest),
				PercentualIsencao = regra.pIsencao,
				FinalidadeRegraIMendes = HelperString.ToStringEmptyWhenNull(regra.estd_finalidade),
				AmparoLegal = HelperString.ToStringEmptyWhenNull(regra.AmpLegal)
			};
		}

		public static IEnumerable<RegraDto> ProjectedAsCollectionDto(this IEnumerable<Regra> regras)
		{
			List<Regra> source = HelperIEnumerable.ToListEmptyWhenNull<Regra>(regras);
			if (!source.Any())
			{
				return Enumerable.Empty<RegraDto>();
			}
			return source.Select(ProjectedAsDto);
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Financeiros.Boleto;
using DigisatServer.Application.Main.Dto.Financeiros.CentrosCusto;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Financeiros.PlanosConta;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Financeiros.Bancos;
using DigisatServer.Infrastructure.Crosscutting.Dto.Financeiros.EspeciesPagamento;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.Boletos;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Bancos.LayoutsArquivo;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GestaoBoletos.Interfaces;
using Otus.Modulo.GestaoBoletos.Reports;
using Otus.Modulo.GestaoBoletos.Services;
using Otus.Modulo.GestaoBoletos.ViewModels.BoletoDetalhesViewModel;
using Otus.Modulo.GestaoBoletos.ViewModels.DetalhesViewModel;
using Otus.Modulo.GestaoBoletos.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoBoletos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoBoletos")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoBoletos
{
	[Module(ModuleName = "GestaoBoletoModulo", OnDemand = true)]
	public class GestaoBoletoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoBoletoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoBoletoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoBoletoPrincipalView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ResumoView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<IModoFuncionamentoService, ModoFuncionamentoService>(_container, Array.Empty<InjectionMember>());
		}
	}
}
namespace Otus.Modulo.GestaoBoletos.Views
{
	public class GestaoBoletoPrincipalView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoBoletoPrincipalView()
		{
			InitializeComponent();
			DXGridDataController.DisableThreadingProblemsDetection = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.GestaoBoletos;component/views/gestaoboletoprincipalview.xaml", UriKind.Relative);
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
	public class GestaoBoletoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoBoletoView()
		{
			InitializeComponent();
			DXGridDataController.DisableThreadingProblemsDetection = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.GestaoBoletos;component/views/gestaoboletoview.xaml", UriKind.Relative);
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
	public class ResumoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal ListViewFocusable LstResultadosPesquisa;

		private bool _contentLoaded;

		public ResumoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoBoletos;component/views/resumoview.xaml", UriKind.Relative);
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
				LstResultadosPesquisa = (ListViewFocusable)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.GestaoBoletos.Services
{
	public class ModoFuncionamentoService : IModoFuncionamentoService
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		public ModoFuncionamentoService(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
		}

		public IViewModelGestaoBoleto[] ObterViewModels(NavigationParameters parametros)
		{
			return new IViewModelGestaoBoleto[1] { CriarProjetarViewModel(typeof(BoletoViewModel), _service, _impressaoRelatorioService, _executarProcessoService) };
		}

		private IViewModelGestaoBoleto CriarProjetarViewModel(Type tipoViewModel, params object[] parametros)
		{
			IViewModelGestaoBoleto viewModelGestaoBoleto = CriarViewModel(tipoViewModel, parametros);
			viewModelGestaoBoleto.CriarFormulario();
			return viewModelGestaoBoleto;
		}

		private IViewModelGestaoBoleto CriarViewModel(Type tipoViewModel, params object[] parametros)
		{
			return Activator.CreateInstance(tipoViewModel, parametros) as IViewModelGestaoBoleto;
		}
	}
}
namespace Otus.Modulo.GestaoBoletos.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoBoletos.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.GestaoBoletos.Interfaces
{
	public interface IGridControlPesquisa
	{
		void ExecutarPesquisa();
	}
	public interface IModoFuncionamentoService
	{
		IViewModelGestaoBoleto[] ObterViewModels(NavigationParameters parametros);
	}
	public interface IViewModelGestaoBoleto
	{
		Formulario Formulario { get; }

		void CriarFormulario();
	}
	public interface IViewModelGraficoGestaoBoleto : IViewModelBase
	{
		Formulario Formulario { get; }
	}
}
namespace Otus.Modulo.GestaoBoletos.ViewModels
{
	public class ResumoViewModel : NavigationBase
	{
		public IEnumerable<ItemResumoViewModel> ListaResumo
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ItemResumoViewModel>>((Expression<Func<IEnumerable<ItemResumoViewModel>>>)(() => ListaResumo));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ItemResumoViewModel>>((Expression<Func<IEnumerable<ItemResumoViewModel>>>)(() => ListaResumo), value);
			}
		}

		public ICommand ImprimirCommand { get; set; }

		public ResumoViewModel()
			: base(string.Empty)
		{
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
		}

		private void Imprimir()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new Resumo(ListaResumo)).Processar());
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

		private bool PodeImprimir()
		{
			return ListaResumo?.Any() ?? false;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((Base)this).Titulo = navigationParameters["descricao"] as string;
			IReadOnlyCollection<ItemResumoViewModel> listaResumo = navigationParameters["dto"] as IReadOnlyCollection<ItemResumoViewModel>;
			ListaResumo = listaResumo;
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= OnParametroInformado;
			((NavigationBase)this).Dispose();
		}
	}
	public class GestaoBoletoPrincipalViewModel : BaseMinimizar
	{
		private const int CodigoTransmissaoRemessaRetorno = 1;

		private readonly IModoFuncionamentoService _modoFuncionamentoService;

		private readonly IExecutarProcessoService _executarProcessoService;

		private ContaDto _contaSelecionada;

		public ICommand AtivarCommand { get; set; }

		public ICommand DesativarCommand { get; set; }

		public ICommand AnteciparCommand { get; set; }

		public ICommand ConsultarStatusCommand { get; set; }

		public ICommand RemoverAntecipacaoCommand { get; set; }

		public ICommand GerarArquivoRemessaCommand { get; set; }

		public ICommand ProcessarArquivoRetornoCommand { get; set; }

		public Lista<IViewModelGestaoBoleto> ViewModels
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<IViewModelGestaoBoleto>>((Expression<Func<Lista<IViewModelGestaoBoleto>>>)(() => ViewModels));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<IViewModelGestaoBoleto>>((Expression<Func<Lista<IViewModelGestaoBoleto>>>)(() => ViewModels), value);
			}
		}

		public GestaoBoletoPrincipalViewModel(IModoFuncionamentoService modoFuncionamentoService, IExecutarProcessoService executarProcessoService)
		{
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			_modoFuncionamentoService = modoFuncionamentoService;
			_executarProcessoService = executarProcessoService;
			GerarArquivoRemessaCommand = (ICommand)new DelegateCommand<object>((Action<object>)GerarArquivoRemessa, (Func<object, bool>)PodeGerarArquivoRemessa, (bool?)null);
			ProcessarArquivoRetornoCommand = (ICommand)new DelegateCommand((Action)ProcessarArquivoRetorno, (Func<bool>)PodeProcessarArquivoRetorno, (bool?)null);
			AtivarCommand = (ICommand)new DelegateCommand<object>((Action<object>)Ativar, (Func<object, bool>)PodeAtivar, (bool?)null);
			DesativarCommand = (ICommand)new DelegateCommand<object>((Action<object>)Desativar, (Func<object, bool>)PodeDesativar, (bool?)null);
			AnteciparCommand = (ICommand)new DelegateCommand<object>((Action<object>)Antecipar, (Func<object, bool>)PodeAntecipar, (bool?)null);
			RemoverAntecipacaoCommand = (ICommand)new DelegateCommand<object>((Action<object>)RemoverAntecipacao, (Func<object, bool>)PodeRemoverAntecipacao, (bool?)null);
			ConsultarStatusCommand = (ICommand)new DelegateCommand<object>((Action<object>)ConsultarStatus, (Func<object, bool>)PodeConsultarStatus, (bool?)null);
		}

		private bool PodeConsultarStatus(object arg)
		{
			if (HelperObject.IsNotNull((object)RetornarBoletoPossivelRemessaDetalheViewModel()))
			{
				return ((CodigoDescricaoDto)_contaSelecionada.Agencia.Boleto.TipoTransmissao).Descricao == "DePagar";
			}
			return false;
		}

		private bool ContaSelecionadaTransmitePorRemessaRetorno()
		{
			ContaDto contaSelecionada = _contaSelecionada;
			if (contaSelecionada == null)
			{
				return false;
			}
			AgenciaDto agencia = contaSelecionada.Agencia;
			int? obj;
			if (agencia == null)
			{
				obj = null;
			}
			else
			{
				BoletoDto boleto = agencia.Boleto;
				if (boleto == null)
				{
					obj = null;
				}
				else
				{
					TipoTransmissaoDto tipoTransmissao = boleto.TipoTransmissao;
					obj = ((tipoTransmissao != null) ? new int?(((CodigoDescricaoDto)tipoTransmissao).Codigo) : null);
				}
			}
			return obj == 1;
		}

		private bool BancoDaContaSelecionadaEmitePorRemessaRetorno()
		{
			ContaDto contaSelecionada = _contaSelecionada;
			if (contaSelecionada == null)
			{
				return false;
			}
			AgenciaDto agencia = contaSelecionada.Agencia;
			bool? obj;
			if (agencia == null)
			{
				obj = null;
			}
			else
			{
				BancoDto banco = agencia.Banco;
				obj = ((banco != null) ? new bool?(HelperIEnumerable.ToListEmptyWhenNull<TipoTransmissaoDto>(banco.TiposTransmissao).Any((TipoTransmissaoDto x) => !HelperObject.Is<TipoTransmissaoDtoNull>((object)x) && ((CodigoDescricaoDto)x).Codigo == 1)) : null);
			}
			return obj == true;
		}

		private async void ConsultarStatus(object obj)
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Consultando status boletos...");
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)boletoPossivelRemessaDetalheViewModel) || !boletoPossivelRemessaDetalheViewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				List<BoletoDto> list = HelperIEnumerable.ToListEmptyWhenNull<BoletoDto>(await DadosSistema.GetService().Canal<IDePagarModuleService, Task<IEnumerable<BoletoDto>>>((Func<IDePagarModuleService, Task<IEnumerable<BoletoDto>>>)((IDePagarModuleService service) => service.SincronizarBoletos())));
				if (!list.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Sincronizado, nenhum registro a ser processado.", false, System.Windows.HorizontalAlignment.Center);
				}
				else
				{
					MontarExibirResumos(list, "Resumo da operação de sincronização");
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

		private bool PodeGerarArquivoRemessa(object arg)
		{
			if (HelperObject.IsNotNull((object)RetornarBoletoPossivelRemessaDetalheViewModel()))
			{
				return ContaSelecionadaTransmitePorRemessaRetorno();
			}
			return false;
		}

		private void GerarArquivoRemessa(object obj)
		{
			try
			{
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)boletoPossivelRemessaDetalheViewModel) || !boletoPossivelRemessaDetalheViewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				GeracaoArquivoRemessa.Init().ComConta(boletoPossivelRemessaDetalheViewModel.Contas.ItemSelecionado).ComExecutarProcessoService(_executarProcessoService)
					.ComService(DadosSistema.GetService())
					.ComActioLimparPesquisa(LimparPesquisaEObterContas(boletoPossivelRemessaDetalheViewModel))
					.ComActionAtualizarBoletosProcessados(AtualizarBoletosProcessados)
					.GerarArquivoRemessa(from x in boletoPossivelRemessaDetalheViewModel.RetornarItensSelecionados(obj)
						select new GerenciadorBoletoViewModel(x));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private static Action LimparPesquisaEObterContas(BoletoPossivelRemessaDetalheViewModel viewModel)
		{
			return delegate
			{
				viewModel.Pesquisar();
				viewModel.ObterContas();
			};
		}

		private BoletoPossivelRemessaDetalheViewModel RetornarBoletoPossivelRemessaDetalheViewModel()
		{
			Formulario val = ViewModels?.Itens?.FirstOrDefault((IViewModelGestaoBoleto x) => HelperObject.Is<BoletoViewModel>((object)x))?.Formulario;
			if (!HelperObject.IsNotNull((object)val))
			{
				return null;
			}
			if (HelperObject.Is<BoletoPossivelRemessaDetalheViewModel>((object)val.ViewModelAbaSelecionada))
			{
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = (BoletoPossivelRemessaDetalheViewModel)(object)val.ViewModelAbaSelecionada;
				_contaSelecionada = boletoPossivelRemessaDetalheViewModel.Contas.ItemSelecionado;
				return boletoPossivelRemessaDetalheViewModel;
			}
			return null;
		}

		private bool PodeProcessarArquivoRetorno()
		{
			if (HelperObject.IsNotNull((object)RetornarBoletoPossivelRemessaDetalheViewModel()))
			{
				return BancoDaContaSelecionadaEmitePorRemessaRetorno();
			}
			return false;
		}

		private void ProcessarArquivoRetorno()
		{
			try
			{
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)boletoPossivelRemessaDetalheViewModel) || !boletoPossivelRemessaDetalheViewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				_contaSelecionada = boletoPossivelRemessaDetalheViewModel.Contas.ItemSelecionado;
				LeituraArquivoRetorno.Init().ComExecutarProcessoService(_executarProcessoService).ComService(DadosSistema.GetService())
					.ComActioNavegarResumo(NavegarResumo)
					.ComConta(_contaSelecionada)
					.ComActionAtualizarBoletosProcessados(AtualizarBoletosProcessados)
					.LerArquivoRetorno();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private void MontarExibirResumos(List<BoletoDto> boletos, string descricao)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			List<ItemResumoViewModel> list = new List<ItemResumoViewModel>();
			foreach (BoletoDto boleto in boletos)
			{
				list.Add(ItemResumoViewModel.Init().ComStatusProcessamento(HelperEnum.RetornarDescricao((Enum)(object)boleto.SituacaoBoleto)).ComNumeroDocumento($"{boleto.ParcelaRecebimento.Documento}-{boleto.ParcelaRecebimento.Ordem}")
					.ComNossoNumero(boleto.NossoNumero)
					.ComSacado(boleto.ParcelaRecebimento.Pessoa.Nome)
					.ComValor(boleto.ParcelaRecebimento.ValorQuitacao)
					.ComDataVencimento(boleto.ParcelaRecebimento.DataVencimento)
					.ComDataCredito(boleto.ParcelaRecebimento.DataQuitacao)
					.ComValorCredito(boleto.ParcelaRecebimento.ValorQuitacao)
					.SetarQuitadoNoSistema(quitadoSistema: true)
					.ComAntecipado(boleto.Antecipado)
					.ComDataAntecipacao(boleto.DataAntecipacao)
					.Criar());
			}
			NavegarResumo(list, descricao);
		}

		private void NavegarResumo(IEnumerable<ItemResumoViewModel> itens, string descricao)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("dto", (object)itens);
			val.Add("descricao", (object)descricao);
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar(typeof(ResumoView).Name, (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private void AtualizarBoletosProcessados(IEnumerable<BoletoDto> dtos)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			foreach (BoletoDto item in dtos.Where((BoletoDto x) => !HelperObject.IsExactlyObject<EspeciePagamentoDto>((object)x.ParcelaRecebimento.EspeciePagamento)))
			{
				item.ParcelaRecebimento.EspeciePagamento = (EspeciePagamentoDto)new EspeciePagamentoDtoNull();
			}
			DadosSistema.GetService().Canal<IBoletoModuleService>((Action<IBoletoModuleService>)delegate(IBoletoModuleService service)
			{
				service.AtualizarBoletosProcessados(dtos);
			});
		}

		public void Ativar(object obj)
		{
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			try
			{
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)boletoPossivelRemessaDetalheViewModel) || !boletoPossivelRemessaDetalheViewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				foreach (BoletoDto boleto in boletoPossivelRemessaDetalheViewModel.RetornarItensSelecionados(obj))
				{
					if (HelperObject.IsNotNull((object)boleto) && !((EntityDto)boleto).Ativo && !HelperObject.IsExactlyObject<EspeciePagamentoDto>((object)boleto.ParcelaRecebimento.EspeciePagamento))
					{
						boleto.ParcelaRecebimento.EspeciePagamento = (EspeciePagamentoDto)new EspeciePagamentoDtoNull();
					}
					DadosSistema.GetService().Canal<IBoletoModuleService, Task<BoletoDto>>((Func<IBoletoModuleService, Task<BoletoDto>>)((IBoletoModuleService service) => ((IAtivar<BoletoDto>)(object)service).AtivarAsync(boleto)));
				}
				boletoPossivelRemessaDetalheViewModel.Pesquisar();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAtivar(object obj)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
			if (HelperObject.IsNotNull((object)boletoPossivelRemessaDetalheViewModel))
			{
				return (int)boletoPossivelRemessaDetalheViewModel.FiltrosAtivoInativo.ItemSelecionado.Value == 1;
			}
			return false;
		}

		public async void Desativar(object obj)
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Realizando baixa nos boletos...");
				BoletoPossivelRemessaDetalheViewModel viewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)viewModel) || !viewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				IEnumerable<BoletoDto> enumerable = viewModel.RetornarItensSelecionados(obj);
				foreach (BoletoDto boleto in enumerable)
				{
					if (HelperObject.IsNotNull((object)boleto) && ((EntityDto)boleto).Ativo && !HelperObject.IsExactlyObject<EspeciePagamentoDto>((object)boleto.ParcelaRecebimento.EspeciePagamento))
					{
						boleto.ParcelaRecebimento.EspeciePagamento = (EspeciePagamentoDto)new EspeciePagamentoDtoNull();
					}
					await DadosSistema.GetService().Canal<IBoletoModuleService, Task<BoletoDto>>((Func<IBoletoModuleService, Task<BoletoDto>>)((IBoletoModuleService service) => ((IDesativar<BoletoDto>)(object)service).DesativarAsync(boleto)));
				}
				viewModel.Pesquisar();
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

		private bool PodeDesativar(object obj)
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Invalid comparison between Unknown and I4
			BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
			if (!HelperObject.IsNotNull((object)boletoPossivelRemessaDetalheViewModel))
			{
				return false;
			}
			if (((CodigoDescricaoDto)_contaSelecionada.Agencia.Boleto.TipoTransmissao).Descricao == "DePagar")
			{
				return false;
			}
			return (int)boletoPossivelRemessaDetalheViewModel.FiltrosAtivoInativo.ItemSelecionado.Value == 0;
		}

		public void Antecipar(object obj)
		{
			try
			{
				BoletoPossivelRemessaDetalheViewModel viewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)viewModel) || !viewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				IEnumerable<BoletoDto> enumerable = viewModel.RetornarItensSelecionados(obj);
				MessageBoxResult resultado = DialogoFactory.MostrarDialogoSimNao("Gerar conta a pagar para o banco na data de vencimento do boleto?", false, 0, System.Windows.HorizontalAlignment.Center);
				if (resultado == MessageBoxResult.Yes && (HelperObject.IsExactlyObject<PessoaDtoNull>((object)viewModel.Contas.ItemSelecionado.Agencia.BancoPessoa) || HelperObject.IsExactlyObject<PlanoContaDtoNull>((object)viewModel.Contas.ItemSelecionado.Agencia.PlanoConta) || HelperObject.IsExactlyObject<CentroCustoDtoNull>((object)viewModel.Contas.ItemSelecionado.Agencia.CentroCusto)))
				{
					DialogoFactory.MostrarDialogoConfirma("Dados para geração de contas a pagar de boleto antecipado não indicados no cadastro da conta", false, System.Windows.HorizontalAlignment.Center);
					resultado = MessageBoxResult.No;
				}
				foreach (BoletoDto boleto in enumerable)
				{
					if (HelperObject.IsNotNull((object)boleto) && !boleto.Antecipado)
					{
						DadosSistema.GetService().Canal<IBoletoModuleService, BoletoDto>((Func<IBoletoModuleService, BoletoDto>)((IBoletoModuleService service) => service.Antecipar(boleto, resultado == MessageBoxResult.Yes, viewModel.Contas.ItemSelecionado.Agencia.BancoPessoa, viewModel.Contas.ItemSelecionado.Agencia.PlanoConta, viewModel.Contas.ItemSelecionado.Agencia.CentroCusto)));
					}
				}
				viewModel.Pesquisar();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAntecipar(object obj)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
			if (HelperObject.IsNotNull((object)boletoPossivelRemessaDetalheViewModel))
			{
				return (int)boletoPossivelRemessaDetalheViewModel.FiltrosSituacaoAntecipacaoBoleto.ItemSelecionado.Value == 2;
			}
			return false;
		}

		public void RemoverAntecipacao(object obj)
		{
			try
			{
				BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
				if (HelperObject.IsNull((object)boletoPossivelRemessaDetalheViewModel) || !boletoPossivelRemessaDetalheViewModel.Contas.PossuiItemSelecionado)
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhuma conta que emite boleto foi localizada", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				foreach (BoletoDto boleto in boletoPossivelRemessaDetalheViewModel.RetornarItensSelecionados(obj))
				{
					if (HelperObject.IsNotNull((object)boleto) && boleto.Antecipado)
					{
						DadosSistema.GetService().Canal<IBoletoModuleService, BoletoDto>((Func<IBoletoModuleService, BoletoDto>)((IBoletoModuleService service) => service.RemoverAntecipacao(boleto)));
					}
				}
				boletoPossivelRemessaDetalheViewModel.Pesquisar();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeRemoverAntecipacao(object obj)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			BoletoPossivelRemessaDetalheViewModel boletoPossivelRemessaDetalheViewModel = RetornarBoletoPossivelRemessaDetalheViewModel();
			if (HelperObject.IsNotNull((object)boletoPossivelRemessaDetalheViewModel))
			{
				return (int)boletoPossivelRemessaDetalheViewModel.FiltrosSituacaoAntecipacaoBoleto.ItemSelecionado.Value == 1;
			}
			return false;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
			ViewModels = new Lista<IViewModelGestaoBoleto>();
			ViewModels.Adicionar(_modoFuncionamentoService.ObterViewModels(navigationParameters));
			ViewModels.PosicionarNoPrimeiro();
			try
			{
				HelperObject.To<BoletoPossivelRemessaDetalheViewModel>((object)ViewModels.ItemSelecionado.Formulario.ViewModelAbaSelecionada)?.Contas.Posicionar((Expression<Func<ContaDto, bool>>)((ContaDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)_contaSelecionada).Id)));
			}
			catch (Exception)
			{
			}
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= ((BaseMinimizar)this).OnParametroInformado;
			Lista<IViewModelGestaoBoleto> viewModels = ViewModels;
			if (viewModels != null)
			{
				((ViewModelValidationBase)viewModels).Dispose();
			}
			ViewModels = null;
			((BaseMinimizar)this).Dispose();
		}
	}
	public class GestaoBoletoViewModel : ModuloBase
	{
		public GestaoBoletoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoBoletoRegion, typeof(GestaoBoletoPrincipalView).Name, (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.GestaoBoletos.ViewModels.DetalhesViewModel
{
	public abstract class BaseDetalheViewModel<TDto> : ViewModelValidationBase, IViewModelBase, IViewModelImprimirGrid, IViewModelImprimir, IGridControlPesquisa where TDto : class
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ICommand PesquisarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand CarregarLayoutCommand { get; set; }

		public ICommand SalvarLayoutCommand { get; set; }

		public ICommand CarregarLayoutPadraoCommand { get; set; }

		public ICommand DefinirLayoutPadraoCommand { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public abstract string TituloRelatorio { get; }

		public abstract string SubTituloRelatorio { get; }

		public abstract string TagProperty { get; }

		public abstract string NomeArquivoLayoutPadrao { get; }

		public short Pagina { get; protected set; } = 1;


		public int RegistrosPorPagina
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => RegistrosPorPagina));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => RegistrosPorPagina), value);
			}
		}

		public bool Retrato => false;

		public Lista<TDto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista), value);
			}
		}

		protected BaseDetalheViewModel()
		{
		}

		protected BaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			_impressaoRelatorioService = impressaoRelatorioService;
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)ImprimirExportar);
			CarregarLayoutCommand = (ICommand)new DelegateCommand<object>((Action<object>)CarregarLayout);
			SalvarLayoutCommand = (ICommand)new DelegateCommand<object>((Action<object>)SalvarLayout);
			CarregarLayoutPadraoCommand = (ICommand)new DelegateCommand<object>((Action<object>)CarregarLayoutPadrao);
			DefinirLayoutPadraoCommand = (ICommand)new DelegateCommand<object>((Action<object>)DefinirLayoutPadrao);
			RegistrosPorPagina = 100;
		}

		public virtual void Pesquisar()
		{
			Lista.Limpar();
			Pagina = 1;
			ExecutarPesquisa();
		}

		private void ImprimirExportar(object obj)
		{
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		private void CarregarLayout(object obj)
		{
			string nomeArquivo = SelecionarArquivo();
			CarregarLayout(nomeArquivo, obj);
		}

		private void CarregarLayout(string nomeArquivo, object obj)
		{
			try
			{
				if (!string.IsNullOrEmpty(nomeArquivo))
				{
					FileStream fileStream = new FileStream(nomeArquivo, FileMode.Open);
					FrameworkElement frameworkElement = obj as FrameworkElement;
					GridControl obj2 = ObterGrid(frameworkElement);
					((DataControlBase)obj2).RestoreLayoutFromStream((Stream)fileStream);
					fileStream.Close();
					frameworkElement.UpdateLayout();
					((UIElement)(object)obj2).UpdateLayout();
					((UIElement)(object)obj2.View).UpdateLayout();
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private string SelecionarArquivo()
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
			openFileDialog.Multiselect = false;
			openFileDialog.Filter = "XML | *.xml";
			openFileDialog.ShowDialog();
			return openFileDialog.FileName;
		}

		private void DefinirLayoutPadrao(object obj)
		{
			if (DialogoFactory.MostrarDialogoSimNao("Definir layout atual como padrão?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				SalvarArquivo(ObterNomeArquivoLayoutPadrao(), obj);
			}
		}

		private string ObterPastaLayoutPadrao()
		{
			string text = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "LayoutsGestaoBoleto");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		private string ObterNomeArquivoLayoutPadrao()
		{
			return Path.Combine(ObterPastaLayoutPadrao(), NomeArquivoLayoutPadrao);
		}

		private void CarregarLayoutPadrao(object obj)
		{
			CarregarLayout(ObterArquivoLayoutPadrao(), obj);
		}

		private string ObterArquivoLayoutPadrao()
		{
			string text = ObterNomeArquivoLayoutPadrao();
			if (!File.Exists(text))
			{
				return string.Empty;
			}
			return text;
		}

		private void SalvarLayout(object obj)
		{
			try
			{
				Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
				{
					InitialDirectory = Directory.GetCurrentDirectory(),
					Filter = "XML | *.xml"
				};
				saveFileDialog.ShowDialog();
				SalvarArquivo(saveFileDialog.FileName, obj);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private void SalvarArquivo(string nomeArquivo, object obj)
		{
			if (!string.IsNullOrWhiteSpace(nomeArquivo))
			{
				FileStream fileStream = new FileStream(nomeArquivo, FileMode.Create);
				((DataControlBase)ObterGrid(obj as FrameworkElement)).SaveLayoutToStream((Stream)fileStream);
				fileStream.Close();
			}
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(TagProperty);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			return val.Property;
		}

		public abstract void ExecutarPesquisa();

		public virtual void ProjectedAsViewModel()
		{
		}

		public virtual void ProjectedAsListaViewModel()
		{
			ProjectedAsViewModel();
		}

		public override void Dispose()
		{
			Lista<TDto> lista = Lista;
			if (lista != null)
			{
				((ViewModelValidationBase)lista).Dispose();
			}
			Lista = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.GestaoBoletos.ViewModels.BoletoDetalhesViewModel
{
	public class GeracaoArquivoRemessa
	{
		private ContaDto _conta;

		private Action _limparPesquisa;

		private IExecutarProcessoService _executarProcessoService;

		private IWService _service;

		private Action<IEnumerable<BoletoDto>> _atualizarBoletosProcessados;

		private bool _imprimirResumo;

		public Lista<ItemResumoViewModel> Resumos { get; set; }

		private GeracaoArquivoRemessa()
		{
		}

		public static GeracaoArquivoRemessa Init()
		{
			return new GeracaoArquivoRemessa();
		}

		public GeracaoArquivoRemessa ComConta(ContaDto conta)
		{
			_conta = conta;
			return this;
		}

		public GeracaoArquivoRemessa ComActioLimparPesquisa(Action limparPesquisa)
		{
			_limparPesquisa = limparPesquisa;
			return this;
		}

		public GeracaoArquivoRemessa ComExecutarProcessoService(IExecutarProcessoService executarProcessoService)
		{
			_executarProcessoService = executarProcessoService;
			return this;
		}

		public GeracaoArquivoRemessa ComService(IWService service)
		{
			_service = service;
			return this;
		}

		public GeracaoArquivoRemessa ComActionAtualizarBoletosProcessados(Action<IEnumerable<BoletoDto>> atualizarBoletosProcessados)
		{
			_atualizarBoletosProcessados = atualizarBoletosProcessados;
			return this;
		}

		public void GerarArquivoRemessa(IEnumerable<GerenciadorBoletoViewModel> itensSelecionados)
		{
			string diretorioArquivoRemessa = _conta.Agencia.Boleto.DiretorioArquivoRemessa;
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				GerarArquivo(itensSelecionados, diretorioArquivoRemessa, _conta);
			}, (Action<object>)RetornoGerarArquivo);
		}

		private void GerarArquivo(IEnumerable<GerenciadorBoletoViewModel> itens, string diretorioArquivoRemessa, ContaDto conta)
		{
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(diretorioArquivoRemessa) || !Directory.Exists(diretorioArquivoRemessa))
			{
				diretorioArquivoRemessa = SelecionarDiretorio("Selecione o diretório onde o arquivo de remessa deve ser salvo");
			}
			if (string.IsNullOrEmpty(diretorioArquivoRemessa))
			{
				throw new Exception("Informe o diretório onde o arquivo de remessa deve ser salvo.");
			}
			int num = RetornarSequenciaRemessaDiaria(conta);
			int num2 = conta.Agencia.Boleto.NumeroSequencialArquivo + 1;
			TipoArquivo val = (TipoArquivo)((((CodigoDescricaoDto)conta.Agencia.Boleto.LayoutArquivoRemessaRetorno).Codigo == 1) ? 1 : 2);
			List<IBoletoProxy> list = itens.Select((GerenciadorBoletoViewModel x) => x.BoletoProxy).ToList();
			if (((CodigoDescricaoDto)conta.Agencia.Boleto.TipoCobranca).Codigo == 99)
			{
				TipoCobrancaDto val2 = DialogoFactory.MostrarDialogoComboBox<TipoCobrancaDto>("Selecione o tipo de cobrança", "Tipo de cobrança", conta.Agencia.Banco.TiposCobranca.Where((TipoCobrancaDto x) => ((CodigoDescricaoDto)x).Codigo != 99), "Descricao", true);
				foreach (IBoletoProxy item in list)
				{
					item.ContaBoleto.SetarTipoCobranca(((CodigoDescricaoDto)val2).Codigo);
				}
			}
			string arquivoGerado = BoletoService.GerarArquivoRemessa((IEnumerable<IBoletoProxy>)list, diretorioArquivoRemessa, val, num, num2);
			System.Windows.Application.Current.Dispatcher.Invoke(() => DialogoFactory.MostrarDialogoConfirma($"Arquivo {arquivoGerado} gerado com sucesso!", false, System.Windows.HorizontalAlignment.Center));
			SalvarAtualizacaoSequenciaRemessaDiariaENumeroArquivoRemessa(conta, num, num2);
			AtualizarNumeroSequenciaArquivoRemessaNoBoleto(itens.Select((GerenciadorBoletoViewModel x) => ((ViewModelDtoBase<BoletoDto>)(object)x).Dto), num, num2);
		}

		private static string SelecionarDiretorio(string descricao)
		{
			return System.Windows.Application.Current.Dispatcher.Invoke(delegate
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.Description = descricao;
				folderBrowserDialog.ShowDialog();
				return folderBrowserDialog.SelectedPath;
			});
		}

		private static int RetornarSequenciaRemessaDiaria(ContaDto conta)
		{
			int numeroSequencialArquivoRemessa = conta.Agencia.Boleto.NumeroSequencialArquivoRemessa;
			if (conta.Agencia.Boleto.DataRemessa.Date == DateTime.Now.Date)
			{
				return numeroSequencialArquivoRemessa + 1;
			}
			return 1;
		}

		private void AtualizarNumeroSequenciaArquivoRemessaNoBoleto(IEnumerable<BoletoDto> boletos, int numeroSequencialArquivoRemessaDiaria, int numeroArquivoRemessa)
		{
			foreach (BoletoDto boleto in boletos)
			{
				boleto.NumeroSequencialArquivoRemessaDiaria = numeroSequencialArquivoRemessaDiaria;
				boleto.NumeroSequencialArquivo = numeroArquivoRemessa;
				boleto.SituacaoBoleto = (SituacaoBoleto)((!boleto.SolicitacaoDeBaixa) ? 1 : 4);
				boleto.DataRemessa = DateTime.Now;
			}
			_atualizarBoletosProcessados(boletos);
			_imprimirResumo = false;
			if (DialogoFactory.MostrarDialogoSimNao("Imprimir resumo da remessa?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				_imprimirResumo = true;
				ImprimirResumo(boletos);
			}
		}

		private void ImprimirResumo(IEnumerable<BoletoDto> boletosDto)
		{
			Resumos = new Lista<ItemResumoViewModel>();
			foreach (BoletoDto item in boletosDto)
			{
				AdicionarItemResumoQuitado(item);
			}
		}

		private void AdicionarItemResumoQuitado(BoletoDto boleto)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			Resumos.Adicionar(ItemResumoViewModel.Init().ComStatusProcessamento(HelperEnum.RetornarDescricao((Enum)(object)boleto.SituacaoBoleto)).ComNumeroDocumento($"{boleto.ParcelaRecebimento.Documento}-{boleto.ParcelaRecebimento.Ordem}")
				.ComNossoNumero(boleto.NossoNumeroFormatado)
				.ComSacado(boleto.ParcelaRecebimento.Pessoa.Nome)
				.ComValor(boleto.ParcelaRecebimento.Valor)
				.ComDataVencimento(boleto.ParcelaRecebimento.DataVencimento)
				.ComDescricaoConta(((BaseDto)boleto.ParcelaRecebimento.Conta).ToText)
				.ComUsoEmpresa(((EntityBaseDto)boleto).Id)
				.ComAntecipado(boleto.Antecipado)
				.ComDataAntecipacao(boleto.DataAntecipacao)
				.Criar());
		}

		private void Imprimir()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ResumoRemessa(Resumos.Itens, _conta.Descricao)).Processar());
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

		private async void SalvarAtualizacaoSequenciaRemessaDiariaENumeroArquivoRemessa(ContaDto conta, int sequenciaRemessaDiaria, int numeroArquivoRemessa)
		{
			conta.Agencia.Boleto.NumeroSequencialArquivoRemessa = sequenciaRemessaDiaria;
			conta.Agencia.Boleto.DataRemessa = DateTime.Now;
			conta.Agencia.Boleto.NumeroSequencialArquivo = numeroArquivoRemessa;
			await _service.Canal<IContaModuleService, Task<ContaDto>>((Func<IContaModuleService, Task<ContaDto>>)(async (IContaModuleService service) => await ((IAtualizar<ContaDto>)(object)service).AtualizarAsync(conta)));
		}

		private void RetornoGerarArquivo(object obj)
		{
			try
			{
				if (_imprimirResumo)
				{
					Imprimir();
				}
				if (obj is Exception ex)
				{
					throw ex;
				}
				_limparPesquisa();
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}
	}
	public class GerenciadorBoletoViewModel : ViewModelAtivoBase<BoletoDto>
	{
		public IBoletoProxy BoletoProxy
		{
			get
			{
				//IL_006f: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
				//IL_00e9: Expected O, but got Unknown
				//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
				//IL_05fd: Expected O, but got Unknown
				//IL_06bb: Expected O, but got Unknown
				//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
				//IL_06bc: Expected O, but got Unknown
				PessoaBoletoProxy val = new PessoaBoletoProxy(((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Empresa.CpfCnpj, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Empresa.Nome, CriarEnderecoBoletoProxy(((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Empresa.EnderecoCobranca, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Empresa.EmailCobranca), ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Empresa.NaoProtestar);
				PessoaBoletoProxy val2 = new PessoaBoletoProxy(((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Pessoa.CpfCnpj, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Pessoa.Nome, CriarEnderecoBoletoProxy(((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Pessoa.EnderecoCobranca, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Pessoa.EmailCobranca), ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Pessoa.NaoProtestar);
				ContaBoletoProxy val3 = new ContaBoletoProxy(((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Banco.Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.EspecieTitulo).Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.FormaCadastramento).Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.TipoCobranca).Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.TipoDistribuicao).Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.TipoEmissao).Codigo, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.TipoTransmissao).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.IdContaDePagar, (int?)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Solucao).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.NumeroAgencia, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.DigitoAgencia, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.NumeroConta, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.DigitoConta, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Operacao, ((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Carteira).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Carteira.Variacao, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.CodigoCedente, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DigitoCodigoCedente, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Convenio, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Contrato, ((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.FinalidadeArquivoRemessaRetorno).Codigo, ((CodigoDescricaoValorDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.IdentificacaoAceiteNaoAceite).Valor, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.LocalPagamento, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.PercentualJuroMensal, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.PercentualMulta, ((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.PrimeiraInstrucao).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DiasPrimeiraInstrucao, ((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.SegundaInstrucao).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DiasSegundaInstrucao, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DiasIniciarCobrancaDeJuro, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DiasIniciarCobrancaDaMulta, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.OutrasInstrucoes, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.Beneficiario, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.DiasAntesVencimentoParaAplicarDesconto, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.PercentualDesconto, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.MostrarComprovanteEntrega, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.FormatoCarne, (TipoArquivo)((CodigoDescricaoDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.LayoutArquivoRemessaRetorno).Codigo, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ContaBoleto.Agencia.Boleto.CodigoExclusivoBanese);
				return (IBoletoProxy)new BoletoProxy((IPessoaBoletoProxy)val, (IPessoaBoletoProxy)(object)val2, (IContaBoletoProxy)(object)val3, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.DataEfetivacao, ((EntityBaseDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto).DataInclusao, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.DataVencimento, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.DataEmissao, ((EntityBaseDto)((ViewModelDtoBase<BoletoDto>)(object)this).Dto).Id, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Valor, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ValorJurosMoraBoleto, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.ValorMultaBoleto, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Documento, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.ParcelaRecebimento.Ordem, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.NossoNumero, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.SolicitacaoDeBaixa, ((ViewModelDtoBase<BoletoDto>)(object)this).Dto.AlteracaoVencimento);
			}
		}

		public GerenciadorBoletoViewModel()
		{
		}

		public GerenciadorBoletoViewModel(BoletoDto dto)
			: base(dto)
		{
		}

		private IEnderecoBoletoProxy CriarEnderecoBoletoProxy(EnderecoDto enderecoDto, EnderecoEletronicoDto enderecoEletronicoDto)
		{
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			return (IEnderecoBoletoProxy)new EnderecoBoletoProxy(enderecoDto.Logradouro, enderecoDto.Bairro, enderecoDto.Municipio.Nome, enderecoDto.Cep, enderecoDto.Complemento, enderecoDto.Numero, enderecoDto.Municipio.UF.Sigla, enderecoEletronicoDto.Endereco);
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class ItemResumoViewModel : BindableBase
	{
		public string NumeroDocumento { get; set; }

		public string Sacado { get; set; }

		public decimal Valor { get; set; }

		public DateTime Vencimento { get; set; }

		public string NossoNumero { get; set; }

		public string StatusProcessamentoBoleto { get; set; }

		public DateTime? DataCredito { get; set; }

		public decimal ValorCredito { get; set; }

		public bool QuitadoNoSistema { get; set; }

		public string DescricaoConta { get; set; }

		public string UsoEmpresa { get; set; }

		public bool Antecipado { get; set; }

		public DateTime DataAntecipacao { get; set; }

		public string AntecipacaoToText
		{
			get
			{
				if (!Antecipado)
				{
					return string.Empty;
				}
				return $"Boleto antecipado em {HelperDate.FormatarData(DataAntecipacao, (FormatoData)6)}";
			}
		}

		public ItemResumoViewModel()
		{
		}

		private ItemResumoViewModel(string numeroDocumento, string sacado, decimal valor, DateTime vencimento, string nossoNumero, string statusProcessamentoBoleto, DateTime? dataCredito, decimal valorCredito, bool quitadoNoSistema, string descricaoConta, string usoEmpresa, bool antecipado, DateTime dataAntecipacao)
		{
			NumeroDocumento = numeroDocumento;
			Sacado = sacado;
			Valor = valor;
			Vencimento = vencimento;
			NossoNumero = nossoNumero;
			StatusProcessamentoBoleto = statusProcessamentoBoleto;
			DataCredito = dataCredito;
			ValorCredito = valorCredito;
			QuitadoNoSistema = quitadoNoSistema;
			DescricaoConta = descricaoConta;
			UsoEmpresa = usoEmpresa;
			Antecipado = antecipado;
			DataAntecipacao = dataAntecipacao;
		}

		public static ItemResumoViewModel Init()
		{
			return new ItemResumoViewModel();
		}

		public ItemResumoViewModel ComNumeroDocumento(string numeroDocumento)
		{
			NumeroDocumento = numeroDocumento;
			return this;
		}

		public ItemResumoViewModel ComSacado(string sacado)
		{
			Sacado = sacado;
			return this;
		}

		public ItemResumoViewModel ComValor(decimal valor)
		{
			Valor = valor;
			return this;
		}

		public ItemResumoViewModel ComDataVencimento(DateTime vencimento)
		{
			Vencimento = vencimento;
			return this;
		}

		public ItemResumoViewModel ComNossoNumero(string nossoNumero)
		{
			NossoNumero = nossoNumero;
			return this;
		}

		public ItemResumoViewModel ComStatusProcessamento(string status)
		{
			StatusProcessamentoBoleto = status;
			return this;
		}

		public ItemResumoViewModel ComDataCredito(DateTime? dataCredito)
		{
			DataCredito = dataCredito;
			return this;
		}

		public ItemResumoViewModel ComValorCredito(decimal valorCredito)
		{
			ValorCredito = valorCredito;
			return this;
		}

		public ItemResumoViewModel SetarQuitadoNoSistema(bool quitadoSistema)
		{
			QuitadoNoSistema = quitadoSistema;
			return this;
		}

		public ItemResumoViewModel ComDescricaoConta(string descricaoConta)
		{
			DescricaoConta = descricaoConta;
			return this;
		}

		public ItemResumoViewModel ComUsoEmpresa(string usoEmpresa)
		{
			UsoEmpresa = usoEmpresa;
			return this;
		}

		public ItemResumoViewModel ComAntecipado(bool antecipado)
		{
			Antecipado = antecipado;
			return this;
		}

		public ItemResumoViewModel ComDataAntecipacao(DateTime dataAntecipacao)
		{
			DataAntecipacao = dataAntecipacao;
			return this;
		}

		public ItemResumoViewModel CriarItemLancamentoNaoEncontrado()
		{
			QuitadoNoSistema = false;
			StatusProcessamentoBoleto = "Lançamento não encontrado";
			return new ItemResumoViewModel(NumeroDocumento, Sacado, Valor, Vencimento, NossoNumero, StatusProcessamentoBoleto, DataCredito, ValorCredito, QuitadoNoSistema, DescricaoConta, UsoEmpresa, Antecipado, DataAntecipacao);
		}

		public ItemResumoViewModel Criar()
		{
			return new ItemResumoViewModel(NumeroDocumento, Sacado, Valor, Vencimento, NossoNumero, StatusProcessamentoBoleto, DataCredito, ValorCredito, QuitadoNoSistema, DescricaoConta, UsoEmpresa, Antecipado, DataAntecipacao);
		}
	}
	public class LeituraArquivoRetorno
	{
		private ContaDto _conta;

		private IExecutarProcessoService _executarProcessoService;

		private IWService _service;

		private Action<IEnumerable<ItemResumoViewModel>, string> _navegarResumo;

		private Action<IEnumerable<BoletoDto>> _atualizarBoletosProcessados;

		public Lista<ItemResumoViewModel> Resumos { get; set; }

		private LeituraArquivoRetorno()
		{
			Resumos = new Lista<ItemResumoViewModel>();
		}

		public static LeituraArquivoRetorno Init()
		{
			return new LeituraArquivoRetorno();
		}

		public LeituraArquivoRetorno ComConta(ContaDto conta)
		{
			_conta = conta;
			return this;
		}

		public LeituraArquivoRetorno ComExecutarProcessoService(IExecutarProcessoService executarProcessoService)
		{
			_executarProcessoService = executarProcessoService;
			return this;
		}

		public LeituraArquivoRetorno ComService(IWService service)
		{
			_service = service;
			return this;
		}

		public LeituraArquivoRetorno ComActioNavegarResumo(Action<IEnumerable<ItemResumoViewModel>, string> navegarResumo)
		{
			_navegarResumo = navegarResumo;
			return this;
		}

		public LeituraArquivoRetorno ComActionAtualizarBoletosProcessados(Action<IEnumerable<BoletoDto>> atualizarBoletosProcessados)
		{
			_atualizarBoletosProcessados = atualizarBoletosProcessados;
			return this;
		}

		public void LerArquivoRetorno()
		{
			IEnumerable<Stream> arquivos = ObterArquivos();
			if (HelperObject.IsNull((object)arquivos) || !arquivos.Any())
			{
				throw new Exception("Nenhum arquivo selecionado!");
			}
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				LerArquivo(_conta, arquivos);
			}, (Action<object>)RetornoLerArquivo);
		}

		private void LerArquivo(ContaDto conta, IEnumerable<Stream> arquivosRetorno)
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			int codigo = conta.Agencia.Banco.Codigo;
			LayoutArquivoRemessaRetorno val = LayoutsArquivoRemessaRetorno.Dados.FirstOrDefault((LayoutArquivoRemessaRetorno x) => ((CodigoDescricao)x).Codigo.Equals(((CodigoDescricaoDto)conta.Agencia.Boleto.LayoutArquivoRemessaRetorno).Codigo));
			Banco bancoBoleto = new Banco(codigo);
			try
			{
				IEnumerable<BoletoDto> enumerable = null;
				foreach (Stream item in arquivosRetorno)
				{
					if (HelperObject.Is<CNAB240>((object)val))
					{
						enumerable = LerRetornoCnab240(item, (IBanco)(object)bancoBoleto, conta);
					}
					if (HelperObject.Is<CNAB400>((object)val))
					{
						enumerable = LerRetornoCnab400(item, (IBanco)(object)bancoBoleto, conta);
					}
					if (HelperObject.IsNotNull((object)enumerable) && enumerable.Any())
					{
						_atualizarBoletosProcessados?.Invoke(enumerable);
					}
					item.Close();
				}
			}
			finally
			{
				MoverArquivos(arquivosRetorno);
			}
		}

		private void MoverArquivos(IEnumerable<Stream> arquivosRetorno)
		{
			try
			{
				string text = Path.Combine((!string.IsNullOrEmpty(_conta.Agencia.Boleto.DiretorioArquivoRetorno)) ? _conta.Agencia.Boleto.DiretorioArquivoRetorno : Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "ArquivosRetorno", _conta.Descricao), "Processados", DateTime.Now.ToString("MMyyyy"));
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				foreach (Stream item in arquivosRetorno)
				{
					string[] array = HelperObject.To<FileStream>((object)item).Name.Split('\\');
					File.Move(HelperObject.To<FileStream>((object)item).Name, Path.Combine(text, array[array.Length - 1]));
				}
			}
			catch (Exception)
			{
			}
		}

		private IEnumerable<BoletoDto> LerRetornoCnab240(Stream arquivoRetorno, IBanco bancoBoleto, ContaDto conta)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0203: Invalid comparison between Unknown and I4
			//IL_0220: Unknown result type (might be due to invalid IL or missing references)
			//IL_025b: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Invalid comparison between Unknown and I4
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0354: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			List<BoletoDto> list = new List<BoletoDto>();
			ArquivoRetornoCNAB240 val = new ArquivoRetornoCNAB240
			{
				Solucao = (TipoSolucao)2
			};
			((AbstractArquivoRetorno)val).LerArquivoRetorno(bancoBoleto, arquivoRetorno);
			if (!HelperIEnumerable.ToListEmptyWhenNull<DetalheRetornoCNAB240>((IEnumerable<DetalheRetornoCNAB240>)val.ListaDetalhes).Any())
			{
				throw new Exception($"Nenhum registro a ser processado no arquivo {HelperObject.To<FileStream>((object)arquivoRetorno).Name}.");
			}
			foreach (DetalheRetornoCNAB240 listaDetalhe in val.ListaDetalhes)
			{
				string text = listaDetalhe.SegmentoT.NossoNumero?.Trim();
				string usoEmpresa = listaDetalhe.SegmentoT.UsoEmpresa;
				string text2 = ((usoEmpresa == null) ? null : HelperString.LimparEspacos(usoEmpresa)?.ToLower()) ?? null;
				if (HelperObject.IsNull((object)text))
				{
					AdicionarItemResumoNossoNumeroNulo(listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto, listaDetalhe.SegmentoT.ValorTitulo, listaDetalhe.SegmentoT.DataVencimento, listaDetalhe.SegmentoT.NumeroDocumento?.Trim(), text2, antecipado: false, DateTime.MinValue);
				}
				if (!HelperObject.IsNull((object)text))
				{
					BoletoDto val2 = null;
					if (!string.IsNullOrWhiteSpace(text2) && text2.Length == 24)
					{
						val2 = ObterBoletoPorIdParaQuitar(text2);
					}
					if (HelperObject.IsNull((object)val2) || HelperObject.Is<BoletoDtoNull>((object)val2))
					{
						val2 = ObterBoletoParaQuitar(text, ((EntityBaseDto)conta).Id);
					}
					if (HelperObject.Is<BoletoDtoNull>((object)val2))
					{
						text = HelperString.CopiarTexto(text, 0, text.Length - 1, true);
						val2 = ObterBoletoParaQuitar(text.Trim(), ((EntityBaseDto)conta).Id);
					}
					if (HelperObject.IsNull((object)val2) || HelperObject.Is<BoletoDtoNull>((object)val2))
					{
						val2 = ObterBoletoPorDocumentoParaQuitar(listaDetalhe.SegmentoT.NumeroDocumento, listaDetalhe.SegmentoT.NossoNumero);
					}
					if (HelperObject.Is<BoletoDtoNull>((object)val2))
					{
						AdicionarItemResumoBoletoNulo(text, listaDetalhe.SegmentoT.ValorTitulo, listaDetalhe.SegmentoT.DataVencimento, listaDetalhe.SegmentoT.NumeroDocumento?.Trim(), text2, listaDetalhe.SegmentoT.NomeSacado?.Trim(), antecipado: false, DateTime.MinValue);
					}
					else if ((int)val2.SituacaoBoleto == 4 || !val2.ParcelaRecebimento.Situacao.PodeSerQuitada)
					{
						val2.SituacaoBoleto = listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto;
						AdicionarBoletoAtualizado(list, val2);
						AdicionarItemResumoQuitado(val2.ParcelaRecebimento.DataQuitacao, val2.ParcelaRecebimento.ValorQuitacao, val2.NossoNumero, val2.ParcelaRecebimento, HelperEnum.RetornarDescricao((Enum)(object)val2.SituacaoBoleto), text2, val2.Antecipado, val2.DataAntecipacao);
					}
					else if ((int)listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto == 4)
					{
						BaixarBoleto((listaDetalhe.SegmentoU.DataCredito == DateTime.MinValue) ? listaDetalhe.SegmentoU.DataOcorrencia : listaDetalhe.SegmentoU.DataCredito, listaDetalhe.SegmentoU.ValorPagoPeloSacado, listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto, text, val2, listaDetalhe.SegmentoT.UsoEmpresa.Trim());
					}
					else
					{
						val2.SituacaoBoleto = listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto;
						AdicionarBoletoAtualizado(list, val2);
						AdicionarItemResumoBoletoNaoLiquidado(text, listaDetalhe.SegmentoT.ValorTitulo, listaDetalhe.SegmentoT.DataVencimento, $"{val2.ParcelaRecebimento.Documento}-{val2.ParcelaRecebimento.Ordem}", listaDetalhe.SegmentoT.StatusProcessamentoDoBoleto, text2, listaDetalhe.SegmentoT.NomeSacado?.Trim(), val2.Antecipado, val2.DataAntecipacao);
					}
				}
			}
			return list;
		}

		private static void AdicionarBoletoAtualizado(ICollection<BoletoDto> boletosAtualizados, BoletoDto boleto)
		{
			BoletoDto item = boletosAtualizados.FirstOrDefault((BoletoDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)boleto).Id);
			boletosAtualizados.Remove(item);
			boletosAtualizados.Add(boleto);
		}

		private IEnumerable<BoletoDto> LerRetornoCnab400(Stream arquivoRetorno, IBanco bancoBoleto, ContaDto conta)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f6: Invalid comparison between Unknown and I4
			//IL_020f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Unknown result type (might be due to invalid IL or missing references)
			//IL_026d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0273: Invalid comparison between Unknown and I4
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			List<BoletoDto> list = new List<BoletoDto>();
			ArquivoRetornoCNAB400 val = new ArquivoRetornoCNAB400();
			((AbstractArquivoRetorno)val).LerArquivoRetorno(bancoBoleto, arquivoRetorno);
			if (!HelperIEnumerable.ToListEmptyWhenNull<DetalheRetornoCNAB400>((IEnumerable<DetalheRetornoCNAB400>)val.ListaDetalhes).Any())
			{
				throw new Exception($"Nenhum registro a ser processado no arquivo {HelperObject.To<FileStream>((object)arquivoRetorno).Name}.");
			}
			if (val.ListaDetalhes.FirstOrDefault().Conta != 0 && !val.ListaDetalhes.FirstOrDefault().Conta.Equals(HelperString.ToInt(conta.Agencia.NumeroConta)))
			{
				throw new Exception("Conta do arquivo diferente da conta selecionada.");
			}
			foreach (DetalheRetornoCNAB400 listaDetalhe in val.ListaDetalhes)
			{
				string text = HelperString.RemoverPontoBarraHifen(listaDetalhe.NossoNumero)?.Trim();
				string usoEmpresa = listaDetalhe.UsoEmpresa;
				string text2 = ((usoEmpresa == null) ? null : HelperString.LimparEspacos(usoEmpresa)?.ToLower());
				if (HelperObject.IsNull((object)text))
				{
					AdicionarItemResumoNossoNumeroNulo(listaDetalhe.StatusProcessamentoDoBoleto, listaDetalhe.ValoTitulo, listaDetalhe.DataVencimento, listaDetalhe.NumeroDocumento?.Trim(), text2, antecipado: false, DateTime.MinValue);
				}
				if (HelperObject.IsNull((object)text))
				{
					continue;
				}
				BoletoDto val2 = null;
				if (!string.IsNullOrWhiteSpace(text2) && text2.Length == 24)
				{
					val2 = ObterBoletoPorIdParaQuitar(text2);
				}
				if (HelperObject.IsNull((object)val2) || HelperObject.Is<BoletoDtoNull>((object)val2))
				{
					val2 = ObterBoletoParaQuitar(text, ((EntityBaseDto)conta).Id);
				}
				if (HelperObject.Is<BoletoDtoNull>((object)val2))
				{
					int.TryParse(text, out var result);
					if (result > 0)
					{
						text = result.ToString();
						val2 = ObterBoletoParaQuitar(text, ((EntityBaseDto)conta).Id);
					}
				}
				if (HelperObject.Is<BoletoDtoNull>((object)val2))
				{
					AdicionarItemResumoBoletoNulo(text, listaDetalhe.ValorPago, listaDetalhe.DataVencimento, listaDetalhe.NumeroDocumento?.Trim(), text2, listaDetalhe.NomeSacado?.Trim(), antecipado: false, DateTime.MinValue);
				}
				else if ((int)val2.SituacaoBoleto == 4 || !val2.ParcelaRecebimento.Situacao.PodeSerQuitada)
				{
					val2.SituacaoBoleto = listaDetalhe.StatusProcessamentoDoBoleto;
					AdicionarItemResumoQuitado(val2.ParcelaRecebimento.DataQuitacao, val2.ParcelaRecebimento.ValorQuitacao, val2.NossoNumero, val2.ParcelaRecebimento, HelperEnum.RetornarDescricao((Enum)(object)val2.SituacaoBoleto), text2, val2.Antecipado, val2.DataAntecipacao);
				}
				else if ((int)listaDetalhe.StatusProcessamentoDoBoleto == 4)
				{
					BaixarBoleto(ObterDataQuitacao(listaDetalhe), listaDetalhe.ValorPago, listaDetalhe.StatusProcessamentoDoBoleto, text, val2, listaDetalhe.UsoEmpresa.Trim());
				}
				else
				{
					val2.SituacaoBoleto = listaDetalhe.StatusProcessamentoDoBoleto;
					AdicionarBoletoAtualizado(list, val2);
					AdicionarItemResumoBoletoNaoLiquidado(text, listaDetalhe.ValorTitulo, listaDetalhe.DataVencimento, $"{val2.ParcelaRecebimento.Documento} {val2.ParcelaRecebimento.Ordem}", listaDetalhe.StatusProcessamentoDoBoleto, text2, listaDetalhe.NomeSacado?.Trim(), val2.Antecipado, val2.DataAntecipacao);
				}
			}
			return list;
		}

		private DateTime ObterDataQuitacao(DetalheRetornoCNAB400 detalhe)
		{
			if (detalhe.DataCredito != DateTime.MinValue)
			{
				return detalhe.DataCredito;
			}
			if (detalhe.DataLiquidacao != DateTime.MinValue)
			{
				return detalhe.DataLiquidacao;
			}
			if (detalhe.DataOcorrencia != DateTime.MinValue)
			{
				return detalhe.DataOcorrencia;
			}
			throw new Exception($"Boleto {detalhe.NossoNumero}: não foi possível determinar a data de quitação.");
		}

		private void BaixarBoleto(DateTime dataCredito, decimal valorPago, SituacaoBoleto statusProcessamentoBoleto, string nossoNumero, BoletoDto boleto, string usoEmpresa)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			QuitacaoParcelaRecebimentoBoletoArquivoRetornoDto quitacao = ObterDtoQuitacao(boleto, dataCredito, valorPago);
			RecebimentoBoletoDto recebimentoDto = Quitar(quitacao);
			AdicionarItemResumoQuitado(dataCredito, valorPago, nossoNumero, recebimentoDto, HelperEnum.RetornarDescricao((Enum)(object)statusProcessamentoBoleto), usoEmpresa, boleto.Antecipado, boleto.DataAntecipacao);
		}

		private void AdicionarItemResumoBoletoNaoLiquidado(string nossoNumero, decimal valor, DateTime dataVencimento, string numeroDocumento, SituacaoBoleto statusBoleto, string usoEmpresa, string nomeSacado, bool antecipado, DateTime dataAntecipacao)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(usoEmpresa) || !RemoverResumoExistente(usoEmpresa))
			{
				Resumos.Adicionar(ItemResumoViewModel.Init().ComNossoNumero(nossoNumero).ComValor(valor)
					.ComDataVencimento(dataVencimento)
					.ComNumeroDocumento(numeroDocumento)
					.ComStatusProcessamento(HelperEnum.RetornarDescricao((Enum)(object)statusBoleto))
					.ComUsoEmpresa(usoEmpresa)
					.ComSacado(nomeSacado)
					.ComAntecipado(antecipado)
					.ComDataAntecipacao(dataAntecipacao)
					.Criar());
			}
		}

		private bool RemoverResumoExistente(string usoEmpresa)
		{
			ItemResumoViewModel itemResumoViewModel = Resumos.Itens.FirstOrDefault((ItemResumoViewModel x) => x.UsoEmpresa == usoEmpresa);
			if (HelperObject.IsNotNull((object)itemResumoViewModel))
			{
				if (itemResumoViewModel.StatusProcessamentoBoleto == HelperEnum.RetornarDescricao((Enum)(object)(SituacaoBoleto)4))
				{
					return true;
				}
				Resumos.Remover(itemResumoViewModel);
			}
			return false;
		}

		private void AdicionarItemResumoBoletoNulo(string nossoNumero, decimal valor, DateTime dataVencimento, string numeroDocumento, string usoEmpresa, string nomeSacado, bool antecipado, DateTime dataAntecipacao)
		{
			if (string.IsNullOrEmpty(usoEmpresa) || !RemoverResumoExistente(usoEmpresa))
			{
				Resumos.Adicionar(ItemResumoViewModel.Init().ComNossoNumero(nossoNumero).ComValor(valor)
					.ComDataVencimento(dataVencimento)
					.ComNumeroDocumento(numeroDocumento)
					.ComUsoEmpresa(usoEmpresa)
					.ComSacado(nomeSacado)
					.ComAntecipado(antecipado)
					.ComDataAntecipacao(dataAntecipacao)
					.CriarItemLancamentoNaoEncontrado());
			}
		}

		private void AdicionarItemResumoNossoNumeroNulo(SituacaoBoleto statusProcessamentoBoleto, decimal valor, DateTime dataVencimento, string numeroDocumento, string usoEmpresa, bool antecipado, DateTime dataAntecipacao)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(usoEmpresa) || !RemoverResumoExistente(usoEmpresa))
			{
				Resumos.Adicionar(ItemResumoViewModel.Init().ComStatusProcessamento(HelperEnum.RetornarDescricao((Enum)(object)statusProcessamentoBoleto)).ComValor(valor)
					.ComDataVencimento(dataVencimento)
					.ComNumeroDocumento(numeroDocumento)
					.ComUsoEmpresa(usoEmpresa)
					.ComAntecipado(antecipado)
					.ComDataAntecipacao(dataAntecipacao)
					.Criar());
			}
		}

		private void AdicionarItemResumoQuitado(DateTime dataCredito, decimal valorCredito, string nossoNumero, RecebimentoBoletoDto recebimentoDto, string status, string usoEmpresa, bool antecipado, DateTime dataAntecipacao)
		{
			if (string.IsNullOrEmpty(usoEmpresa) || !RemoverResumoExistente(usoEmpresa))
			{
				Resumos.Adicionar(ItemResumoViewModel.Init().ComStatusProcessamento(status).ComNumeroDocumento($"{recebimentoDto.Documento}-{recebimentoDto.Ordem}")
					.ComNossoNumero(nossoNumero)
					.ComSacado(recebimentoDto.Pessoa.Nome)
					.ComValor(recebimentoDto.Valor)
					.ComDataVencimento(recebimentoDto.DataVencimento)
					.ComDataCredito(dataCredito)
					.ComValorCredito(valorCredito)
					.SetarQuitadoNoSistema(quitadoSistema: true)
					.ComUsoEmpresa(usoEmpresa)
					.ComAntecipado(antecipado)
					.ComDataAntecipacao(dataAntecipacao)
					.Criar());
			}
		}

		private BoletoDto ObterBoletoParaQuitar(string identificacaoTitulo, string contaReferencia)
		{
			return _service.Canal<IBoletoModuleService, Task<BoletoDto>>((Func<IBoletoModuleService, Task<BoletoDto>>)(async (IBoletoModuleService service) => await service.ObterBoletoParaQuitar(identificacaoTitulo, contaReferencia))).Result;
		}

		private BoletoDto ObterBoletoPorIdParaQuitar(string id)
		{
			return _service.Canal<IBoletoModuleService, Task<BoletoDto>>((Func<IBoletoModuleService, Task<BoletoDto>>)(async (IBoletoModuleService service) => await service.ObterBoletoPorIdParaQuitar(id))).Result;
		}

		private BoletoDto ObterBoletoPorDocumentoParaQuitar(string documento, string nossoNumero)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			try
			{
				return _service.Canal<IBoletoModuleService, Task<BoletoDto>>((Func<IBoletoModuleService, Task<BoletoDto>>)(async (IBoletoModuleService service) => await service.ObterBoletoPorDocumentoParaQuitar(HelperString.ToStringEmptyWhenNull(documento.Split(' ')[0]).Trim(), nossoNumero.Trim()))).Result;
			}
			catch (Exception)
			{
				return (BoletoDto)new BoletoDtoNull();
			}
		}

		private RecebimentoBoletoDto Quitar(QuitacaoParcelaRecebimentoBoletoArquivoRetornoDto quitacao)
		{
			return _service.Canal<IRecebimentoModuleService, RecebimentoBoletoDto>((Func<IRecebimentoModuleService, RecebimentoBoletoDto>)((IRecebimentoModuleService service) => service.QuitarParcelaRecebimentoBoleto(quitacao)));
		}

		public QuitacaoParcelaRecebimentoBoletoArquivoRetornoDto ObterDtoQuitacao(BoletoDto boleto, DateTime dataRecebimento, decimal valorQuitado)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			decimal acrescimo = default(decimal);
			decimal desconto = default(decimal);
			decimal valorTotal = boleto.ParcelaRecebimento.ValorTotal;
			if (valorQuitado > valorTotal)
			{
				acrescimo = valorQuitado - valorTotal;
			}
			if (valorQuitado < valorTotal)
			{
				desconto = valorTotal - valorQuitado;
			}
			return new QuitacaoParcelaRecebimentoBoletoArquivoRetornoDto
			{
				ParcelaReferencia = ((EntityBaseDto)boleto.ParcelaRecebimento).Id,
				Conta = boleto.ParcelaRecebimento.Conta,
				Desconto = desconto,
				EspeciePagamento = boleto.ParcelaRecebimento.EspeciePagamento,
				Acrescimo = acrescimo,
				DataHora = dataRecebimento,
				ValorQuitado = valorQuitado
			};
		}

		private IEnumerable<Stream> ObterArquivos()
		{
			System.Windows.Forms.OpenFileDialog openFileDialog = SelecionarArquivoRetorno(_conta.Agencia.Boleto.DiretorioArquivoRetorno);
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return null;
			}
			List<Stream> list = new List<Stream>();
			string[] fileNames = openFileDialog.FileNames;
			foreach (string fileName in fileNames)
			{
				openFileDialog.FileName = fileName;
				if (openFileDialog.CheckFileExists)
				{
					list.Add(openFileDialog.OpenFile());
				}
			}
			return list;
		}

		private static System.Windows.Forms.OpenFileDialog SelecionarArquivoRetorno(string caminhoArquivoRetorno)
		{
			return new System.Windows.Forms.OpenFileDialog
			{
				FileName = "",
				Title = "Selecione um arquivo de retorno",
				Filter = "Arquivos de Retorno (*.ret;*.crt)|*.ret;*.crt|Todos Arquivos (*.*)|*.*",
				InitialDirectory = caminhoArquivoRetorno,
				Multiselect = true
			};
		}

		private void RetornoLerArquivo(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (Resumos.Itens.Any())
				{
					_navegarResumo(Resumos.Itens, "Resumo da operação de importação do retorno");
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}
	}
	public abstract class BoletoBaseDetalheViewModel<TDto> : BaseDetalheViewModel<TDto> where TDto : class
	{
		public abstract bool MostrarFiltroPaginacao { get; }

		public Lista<KeyValuePair<string, FiltroAtivoInativo>> FiltrosAtivoInativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, FiltroAtivoInativo>>>((Expression<Func<Lista<KeyValuePair<string, FiltroAtivoInativo>>>>)(() => FiltrosAtivoInativo));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, FiltroAtivoInativo>>>((Expression<Func<Lista<KeyValuePair<string, FiltroAtivoInativo>>>>)(() => FiltrosAtivoInativo), value);
			}
		}

		public Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>> FiltrosSituacaoAntecipacaoBoleto
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>>>>)(() => FiltrosSituacaoAntecipacaoBoleto));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>>>>)(() => FiltrosSituacaoAntecipacaoBoleto), value);
			}
		}

		protected BoletoBaseDetalheViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
		}

		public override void ProjectedAsViewModel()
		{
			FiltrosAtivoInativo = new Lista<KeyValuePair<string, FiltroAtivoInativo>>();
			FiltrosAtivoInativo.Adicionar((IEnumerable<KeyValuePair<string, FiltroAtivoInativo>>)HelperEnum.ObterParDeValores<FiltroAtivoInativo>());
			FiltrosAtivoInativo.PosicionarNoPrimeiro();
			FiltrosSituacaoAntecipacaoBoleto = new Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>>();
			FiltrosSituacaoAntecipacaoBoleto.Adicionar((IEnumerable<KeyValuePair<string, SituacaoAntecipacaoBoleto>>)HelperEnum.ObterParDeValores<SituacaoAntecipacaoBoleto>());
			FiltrosSituacaoAntecipacaoBoleto.PosicionarNoPrimeiro();
		}

		public override void Dispose()
		{
			Lista<KeyValuePair<string, FiltroAtivoInativo>> filtrosAtivoInativo = FiltrosAtivoInativo;
			if (filtrosAtivoInativo != null)
			{
				((ViewModelValidationBase)filtrosAtivoInativo).Dispose();
			}
			FiltrosAtivoInativo = null;
			Lista<KeyValuePair<string, SituacaoAntecipacaoBoleto>> filtrosSituacaoAntecipacaoBoleto = FiltrosSituacaoAntecipacaoBoleto;
			if (filtrosSituacaoAntecipacaoBoleto != null)
			{
				((ViewModelValidationBase)filtrosSituacaoAntecipacaoBoleto).Dispose();
			}
			FiltrosSituacaoAntecipacaoBoleto = null;
			base.Dispose();
		}
	}
	public class BoletoPossivelRemessaDetalheAbaViewModel : AbaViewModelBase
	{
		public BoletoPossivelRemessaDetalheAbaViewModel(IViewModelBase viewModel)
			: base("BOLETOS", "Lista de boletos para remessa", viewModel)
		{
		}
	}
	public class BoletoPossivelRemessaDetalheViewModel : BoletoBaseDetalheViewModel<GerenciadorBoletoViewModel>
	{
		private readonly Action<PesquisaDto, Action<IEnumerable<BoletoDto>>> _pesquisarBoletosPossivelRemessa;

		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		private ContaDto _contaSelecionada;

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public Lista<KeyValuePair<string, SituacaoBoleto>> FiltrosSituacaoBoleto
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, SituacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoBoleto>>>>)(() => FiltrosSituacaoBoleto));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, SituacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoBoleto>>>>)(() => FiltrosSituacaoBoleto), value);
			}
		}

		public override string TituloRelatorio => "Boletos";

		public override string SubTituloRelatorio => "Não quitados ou baixados";

		public override string TagProperty => "BoletosNaoQuitadosBaixadosGridControl";

		public override string NomeArquivoLayoutPadrao => "BoletosNaoQuitadosBaixados.xml";

		public override bool MostrarFiltroPaginacao => false;

		public BoletoPossivelRemessaDetalheViewModel(Action<PesquisaDto, Action<IEnumerable<BoletoDto>>> pesquisarBoletosPossivelRemessa, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarBoletosPossivelRemessa = pesquisarBoletosPossivelRemessa;
			_obterContas = obterContas;
			ObterContas();
		}

		public void ObterContas()
		{
			IEnumerable<ContaDto> enumerable = _obterContas();
			Contas = new Lista<ContaDto>();
			Contas.Adicionar(enumerable);
			Contas.AlterouItemSelecionadoEvent += ContasOnAlterouItemSelecionadoEvent;
			if (HelperObject.IsNotNull((object)_contaSelecionada))
			{
				Contas.Posicionar((Expression<Func<ContaDto, bool>>)((ContaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)_contaSelecionada).Id));
			}
			else
			{
				Contas.PosicionarNoPrimeiro();
			}
			_contaSelecionada = Contas?.ItemSelecionado;
		}

		private void ContasOnAlterouItemSelecionadoEvent(object sender, ContaDto contaDto)
		{
			if (HelperObject.IsNotNull((object)contaDto))
			{
				_contaSelecionada = contaDto;
			}
			PreencherFiltroSituacaoBoleto();
		}

		public override void ExecutarPesquisa()
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Expected O, but got Unknown
			//IL_0098: Expected O, but got Unknown
			if (!Contas.PossuiItemSelecionado)
			{
				return;
			}
			_pesquisarBoletosPossivelRemessa(new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaGestaoBoletoDto
				{
					FiltroAtivoInativo = base.FiltrosAtivoInativo.ItemSelecionado.Value,
					FiltroSituacaoAntecipacaoBoleto = base.FiltrosSituacaoAntecipacaoBoleto.ItemSelecionado.Value,
					FiltroSituacaoBoleto = FiltrosSituacaoBoleto.ItemSelecionado.Value,
					ContaReferencia = ((EntityBaseDto)Contas.ItemSelecionado).Id
				}
			}, delegate(IEnumerable<BoletoDto> boletos)
			{
				base.Lista.Adicionar(boletos.Select((BoletoDto x) => new GerenciadorBoletoViewModel(x)));
			});
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			base.Lista = new Lista<GerenciadorBoletoViewModel>();
			PreencherFiltroSituacaoBoleto();
		}

		public void PreencherFiltroSituacaoBoleto()
		{
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			FiltrosSituacaoBoleto = new Lista<KeyValuePair<string, SituacaoBoleto>>();
			if (HelperObject.IsNull((object)_contaSelecionada))
			{
				throw new Exception("Nenhuma conta para emissão de boletos foi encontrada!");
			}
			if (((CodigoDescricaoDto)_contaSelecionada.Agencia.Boleto.TipoTransmissao).Descricao == "DePagar")
			{
				FiltrosSituacaoBoleto.Adicionar(new KeyValuePair<string, SituacaoBoleto>(HelperEnum.RetornarDescricao((Enum)(object)(SituacaoBoleto)2), (SituacaoBoleto)2));
				FiltrosSituacaoBoleto.PosicionarNoPrimeiro();
				return;
			}
			foreach (KeyValuePair<string, SituacaoBoleto> item in HelperEnum.ObterParDeValores<SituacaoBoleto>())
			{
				SituacaoBoleto value = item.Value;
				if (!((object)(SituacaoBoleto)(ref value)).Equals((object)(SituacaoBoleto)4))
				{
					value = item.Value;
					if (!((object)(SituacaoBoleto)(ref value)).Equals((object)(SituacaoBoleto)8))
					{
						FiltrosSituacaoBoleto.Adicionar(item);
					}
				}
			}
			FiltrosSituacaoBoleto.PosicionarNoPrimeiro();
		}

		public IEnumerable<BoletoDto> RetornarItensSelecionados(object obj)
		{
			IEnumerable<GerenciadorBoletoViewModel> source = ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<GerenciadorBoletoViewModel>();
			if (source.Any())
			{
				return source.Select((GerenciadorBoletoViewModel x) => ((ViewModelDtoBase<BoletoDto>)(object)x).Dto);
			}
			throw new Exception("Nenhum boleto selecionado");
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(TagProperty);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			if (HelperObject.IsNull((object)val.Property))
			{
				throw new Exception($"{TagProperty} não encontrado");
			}
			return val.Property;
		}

		public override void Dispose()
		{
			Contas.AlterouItemSelecionadoEvent -= ContasOnAlterouItemSelecionadoEvent;
			base.Dispose();
		}
	}
	public class BoletoFormulario : Formulario
	{
		public BoletoFormulario(Action<PesquisaDto, Action<IEnumerable<BoletoDto>>> pesquisarBoletosPossivelRemessa, Action<PesquisaDto, Action<IEnumerable<BoletoVisualizacaoDto>>> pesquisarBoletosImprovavelRemessa, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new BoletoPossivelRemessaDetalheAbaViewModel((IViewModelBase)(object)new BoletoPossivelRemessaDetalheViewModel(pesquisarBoletosPossivelRemessa, obterContas, impressaoRelatorioService)),
				(IAbaViewModelBase)new BoletoImprovavelRemessaDetalheAbaViewModel((IViewModelBase)(object)new BoletoImprovavelRemessaDetalheViewModel(pesquisarBoletosImprovavelRemessa, obterContas, impressaoRelatorioService))
			});
		}
	}
	public class BoletoImprovavelRemessaDetalheAbaViewModel : AbaViewModelBase
	{
		public BoletoImprovavelRemessaDetalheAbaViewModel(IViewModelBase viewModel)
			: base("LIQUIDADOS/BAIXADOS", "Lista de boletos para consulta", viewModel)
		{
		}
	}
	public class BoletoImprovavelRemessaDetalheViewModel : BoletoBaseDetalheViewModel<BoletoVisualizacaoDto>
	{
		private readonly Action<PesquisaDto, Action<IEnumerable<BoletoVisualizacaoDto>>> _pesquisarBoletosImprovavelRemessa;

		private readonly Func<IEnumerable<ContaDto>> _obterContas;

		private bool _pesquisando;

		public Lista<ContaDto> Contas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContaDto>>((Expression<Func<Lista<ContaDto>>>)(() => Contas), value);
			}
		}

		public Lista<KeyValuePair<string, SituacaoBoleto>> FiltrosSituacaoBoleto
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, SituacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoBoleto>>>>)(() => FiltrosSituacaoBoleto));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, SituacaoBoleto>>>((Expression<Func<Lista<KeyValuePair<string, SituacaoBoleto>>>>)(() => FiltrosSituacaoBoleto), value);
			}
		}

		public override string TituloRelatorio => "Boletos";

		public override string SubTituloRelatorio => "Quitados ou baixados";

		public override string TagProperty => "BoletosQuitadosBaixadosGridControl";

		public override string NomeArquivoLayoutPadrao => "BoletosQuitadosBaixados.xml";

		public override bool MostrarFiltroPaginacao => true;

		public BoletoImprovavelRemessaDetalheViewModel(Action<PesquisaDto, Action<IEnumerable<BoletoVisualizacaoDto>>> pesquisarBoletosImprovavelRemessa, Func<IEnumerable<ContaDto>> obterContas, IImpressaoRelatorioService impressaoRelatorioService)
			: base(impressaoRelatorioService)
		{
			_pesquisarBoletosImprovavelRemessa = pesquisarBoletosImprovavelRemessa;
			_obterContas = obterContas;
			ObterContas();
		}

		private void ObterContas()
		{
			IEnumerable<ContaDto> enumerable = _obterContas();
			Contas = new Lista<ContaDto>();
			Contas.Adicionar(enumerable);
			Contas.PosicionarNoPrimeiro();
		}

		public override void ExecutarPesquisa()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			//IL_00c1: Expected O, but got Unknown
			if (_pesquisando)
			{
				return;
			}
			_pesquisando = true;
			if (Contas.PossuiItemSelecionado)
			{
				_pesquisarBoletosImprovavelRemessa(new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaGestaoBoletoDto
					{
						FiltroAtivoInativo = base.FiltrosAtivoInativo.ItemSelecionado.Value,
						FiltroSituacaoAntecipacaoBoleto = base.FiltrosSituacaoAntecipacaoBoleto.ItemSelecionado.Value,
						Pagina = base.Pagina,
						RegistrosPorPagina = (short)base.RegistrosPorPagina,
						ContaReferencia = ((EntityBaseDto)Contas.ItemSelecionado).Id,
						FiltroSituacaoBoleto = FiltrosSituacaoBoleto.ItemSelecionado.Value
					}
				}, delegate(IEnumerable<BoletoVisualizacaoDto> boletos)
				{
					base.Lista.AdicionarSemEventoCollectionChanged(boletos, true);
					LoaderProcessoService.EsconderLoader();
					base.Pagina++;
					_pesquisando = false;
				});
			}
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			base.ProjectedAsViewModel();
			base.Lista = new Lista<BoletoVisualizacaoDto>();
			FiltrosSituacaoBoleto = new Lista<KeyValuePair<string, SituacaoBoleto>>();
			foreach (KeyValuePair<string, SituacaoBoleto> item in HelperEnum.ObterParDeValores<SituacaoBoleto>())
			{
				SituacaoBoleto value = item.Value;
				if (!((object)(SituacaoBoleto)(ref value)).Equals((object)(SituacaoBoleto)4))
				{
					value = item.Value;
					if (!((object)(SituacaoBoleto)(ref value)).Equals((object)(SituacaoBoleto)8))
					{
						continue;
					}
				}
				FiltrosSituacaoBoleto.Adicionar(item);
			}
			FiltrosSituacaoBoleto.PosicionarNoPrimeiro();
		}

		public override void Pesquisar()
		{
			base.Lista.LimparSemEventoCollectionChanged();
			base.Pagina = 1;
			ExecutarPesquisa();
		}
	}
	public class BoletoViewModel : ViewModelValidationBase, IViewModelGestaoBoleto
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IExecutarProcessoService _executarProcessoService;

		public Formulario Formulario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario));
			}
			set
			{
				((BindableBase)this).SetProperty<Formulario>((Expression<Func<Formulario>>)(() => Formulario), value);
			}
		}

		public BoletoViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IExecutarProcessoService executarProcessoService)
		{
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_executarProcessoService = executarProcessoService;
		}

		public void CriarFormulario()
		{
			Formulario = (Formulario)(object)new BoletoFormulario(PesquisarBoletosPossivelRemessa, PesquisarBoletosImprovavelRemessa, ObterContas(), _impressaoRelatorioService);
		}

		private void PesquisarBoletosPossivelRemessa(PesquisaDto opcoesPesquisa, Action<IEnumerable<BoletoDto>> atualizarListaAction)
		{
			IEnumerable<BoletoDto> boletos = null;
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				boletos = _service.Canal<IBoletoModuleService, IEnumerable<BoletoDto>>((Func<IBoletoModuleService, IEnumerable<BoletoDto>>)((IBoletoModuleService service) => service.ObterBoletosPossivelRemessa(opcoesPesquisa)));
			}, (Action<object>)delegate(object retorno)
			{
				try
				{
					if (retorno is Exception ex)
					{
						throw ex;
					}
					atualizarListaAction(boletos);
				}
				catch (Exception ex2)
				{
					DialogoFactory.MostrarDialogoConfirma(ex2.Message, false, System.Windows.HorizontalAlignment.Center);
				}
			});
		}

		private void PesquisarBoletosImprovavelRemessa(PesquisaDto opcoesPesquisa, Action<IEnumerable<BoletoVisualizacaoDto>> atualizarListaAction)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			_service.Canal<IBoletoModuleService, Task<IEnumerable<BoletoVisualizacaoDto>>>((Func<IBoletoModuleService, Task<IEnumerable<BoletoVisualizacaoDto>>>)((IBoletoModuleService service) => service.ObterBoletosImprovavelRemessa(opcoesPesquisa))).ContinueWith(delegate(Task<IEnumerable<BoletoVisualizacaoDto>> task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				atualizarListaAction(task.Result);
			});
		}

		private Func<IEnumerable<ContaDto>> ObterContas()
		{
			return () => _service.Canal<IContaModuleService, Task<IEnumerable<ContaDto>>>((Func<IContaModuleService, Task<IEnumerable<ContaDto>>>)((IContaModuleService x) => x.ObterContasBoleto())).Result;
		}

		public override void Dispose()
		{
			Formulario formulario = Formulario;
			if (formulario != null)
			{
				formulario.Dispose();
			}
			Formulario = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.GestaoBoletos.Reports
{
	public class Resumo : Relatorio
	{
		private readonly IEnumerable<ItemResumoViewModel> _itens;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private GroupHeaderBand GroupHeader1;

		private XRLabel lblTituloCodigo;

		private XRLabel xrLabel14;

		private XRLabel lblTituloDescricao;

		private XRLabel lblTituloUnidade;

		private XRLabel lblTituloVenda;

		private BindingSource bindingSource1;

		private FormattingRule ImprimirSomenteSePossuiCampoPersonalizado;

		private XRLabel xrLabel6;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel lblInformacaoCodigo;

		private XRLabel xrLabel5;

		private XRLabel xrLabel1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private FormattingRule ItemDestacado;

		private FormattingRule ItemNaoDestacado;

		private XRLabel xrLabel10;

		private XRLabel xrLabel9;

		private ReportHeaderBand ReportHeader;

		private XRLabel Empresa;

		private XRLabel CNPJ;

		private XRLabel Titulo;

		private PageFooterBand PageFooter;

		private XRLabel xrLabel71;

		private GroupFooterBand GroupFooter1;

		private XRSubreport subReport;

		private XRLabel xrLabel47;

		private XRLabel txtTituloValorTotalizacaoEspecie;

		private XRLabel xrLabel49;

		private XRLabel xrLabel50;

		private XRLabel xrLabel11;

		private XRLine xrLine1;

		public override bool ExibirNoModuloRelatorios => false;

		public Resumo(IEnumerable<ItemResumoViewModel> itens)
		{
			_itens = itens;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)Empresa).Text = DadosSistema.EmpresaLogada.Nome;
			((XRControl)CNPJ).Text = DadosSistema.EmpresaLogada.CpfCnpjFormatado;
			((XtraReportBase)this).DataSource = _itens;
			((XtraReportBase)((SubreportBase)subReport).ReportSource).DataSource = ((XtraReportBase)this).DataSource;
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
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
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
			//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Expected O, but got Unknown
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_0342: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0487: Unknown result type (might be due to invalid IL or missing references)
			//IL_048d: Expected O, but got Unknown
			//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_059f: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a5: Expected O, but got Unknown
			//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b0: Expected O, but got Unknown
			//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bb: Expected O, but got Unknown
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_080a: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d3: Expected O, but got Unknown
			//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0921: Unknown result type (might be due to invalid IL or missing references)
			//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d2: Expected O, but got Unknown
			//IL_09f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae4: Expected O, but got Unknown
			//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bde: Expected O, but got Unknown
			//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ebc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1098: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1186: Unknown result type (might be due to invalid IL or missing references)
			//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1286: Unknown result type (might be due to invalid IL or missing references)
			//IL_12af: Unknown result type (might be due to invalid IL or missing references)
			//IL_1374: Unknown result type (might be due to invalid IL or missing references)
			//IL_139d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1462: Unknown result type (might be due to invalid IL or missing references)
			//IL_148b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1550: Unknown result type (might be due to invalid IL or missing references)
			//IL_157a: Unknown result type (might be due to invalid IL or missing references)
			//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1795: Unknown result type (might be due to invalid IL or missing references)
			//IL_17be: Unknown result type (might be due to invalid IL or missing references)
			//IL_1840: Unknown result type (might be due to invalid IL or missing references)
			//IL_1869: Unknown result type (might be due to invalid IL or missing references)
			//IL_191d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1946: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ade: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ba3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ca2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ccb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1da1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dca: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel11 = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel10 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			lblInformacaoCodigo = new XRLabel();
			ItemDestacado = new FormattingRule();
			ItemNaoDestacado = new FormattingRule();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel9 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel1 = new XRLabel();
			lblTituloCodigo = new XRLabel();
			xrLabel14 = new XRLabel();
			lblTituloDescricao = new XRLabel();
			lblTituloUnidade = new XRLabel();
			lblTituloVenda = new XRLabel();
			ImprimirSomenteSePossuiCampoPersonalizado = new FormattingRule();
			ReportHeader = new ReportHeaderBand();
			Titulo = new XRLabel();
			Empresa = new XRLabel();
			CNPJ = new XRLabel();
			PageFooter = new PageFooterBand();
			xrLabel71 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			subReport = new XRSubreport();
			xrLabel47 = new XRLabel();
			txtTituloValorTotalizacaoEspecie = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel50 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[10]
			{
				(XRControl)xrLabel11,
				(XRControl)xrLine1,
				(XRControl)xrLabel10,
				(XRControl)xrLabel8,
				(XRControl)xrLabel5,
				(XRControl)xrLabel6,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)lblInformacaoCodigo
			});
			((XRControl)Detail).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(ItemDestacado);
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(ItemNaoDestacado);
			((XRControl)Detail).HeightF = 68.41997f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel11).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "AntecipacaoToText")
			});
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).LocationFloat = new PointFloat(0f, 58.41996f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(10, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(2870f, 5f);
			((XRControl)xrLabel11).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UsePadding = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel11).WordWrap = false;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineStyle = DashStyle.Dot;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(0f, 63.41996f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(2870f, 5f);
			((XRControl)xrLine1).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel10).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel10).Borders = (BorderSide)0;
			((XRControl)xrLabel10).CanGrow = false;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorCredito", "{0:n2}")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).LocationFloat = new PointFloat(2601.313f, 0f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(0, 10, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(268.6873f, 58.42f);
			((XRControl)xrLabel10).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UsePadding = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel10).WordWrap = false;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).CanGrow = false;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "StatusProcessamentoBoleto")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1730.063f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(602.562f, 58.42f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).WordWrap = false;
			((XRControl)xrLabel5).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).CanGrow = false;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataCredito", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).LocationFloat = new PointFloat(2332.625f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(268.6873f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel5).WordWrap = false;
			((XRControl)xrLabel6).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).CanGrow = false;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor", "{0:n2}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(656.8536f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(0, 10, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(225.8524f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UsePadding = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel6).WordWrap = false;
			((XRControl)xrLabel4).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel4).Borders = (BorderSide)0;
			((XRControl)xrLabel4).CanGrow = false;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "NossoNumero")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(325.4375f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(331.4161f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).WordWrap = false;
			((XRControl)xrLabel3).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Vencimento", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(882.7061f, 0f);
			xrLabel3.Multiline = true;
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(256.1252f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel3).WordWrap = false;
			((XRControl)xrLabel2).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).CanGrow = false;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "NumeroDocumento")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(325.4375f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).WordWrap = false;
			((XRControl)lblInformacaoCodigo).BorderDashStyle = (BorderDashStyle)0;
			((XRControl)lblInformacaoCodigo).Borders = (BorderSide)0;
			((XRControl)lblInformacaoCodigo).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Sacado")
			});
			((XRControl)lblInformacaoCodigo).Dpi = 254f;
			((XRControl)lblInformacaoCodigo).LocationFloat = new PointFloat(1138.831f, 0f);
			((XRControl)lblInformacaoCodigo).Name = "lblInformacaoCodigo";
			((XRControl)lblInformacaoCodigo).Padding = new PaddingInfo(10, 5, 0, 0, 254f);
			((XRControl)lblInformacaoCodigo).SizeF = new SizeF(591.231f, 58.42f);
			((XRControl)lblInformacaoCodigo).StylePriority.UseBorderDashStyle = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UseBorders = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UsePadding = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UseTextAlignment = false;
			((XRControl)lblInformacaoCodigo).TextAlignment = (TextAlignment)16;
			((XRControl)lblInformacaoCodigo).WordWrap = false;
			ItemDestacado.Condition = "[Destacar] == True";
			((BrickStyle)ItemDestacado.Formatting).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			ItemDestacado.Name = "ItemDestacado";
			ItemNaoDestacado.Condition = "[Destacar] == False";
			((BrickStyle)ItemNaoDestacado.Formatting).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			ItemNaoDestacado.Name = "ItemNaoDestacado";
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 45.43751f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 69.03834f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel9,
				(XRControl)xrLabel7,
				(XRControl)xrLabel1,
				(XRControl)lblTituloCodigo,
				(XRControl)xrLabel14,
				(XRControl)lblTituloDescricao,
				(XRControl)lblTituloUnidade,
				(XRControl)lblTituloVenda
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 56.75f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel9).Borders = (BorderSide)8;
			((XRControl)xrLabel9).CanGrow = false;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(2601.313f, 0f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(268.6873f, 56.75f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Valor crédito";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).CanGrow = false;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(2332.625f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(268.6873f, 56.75f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Data crédito";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel1).Borders = (BorderSide)8;
			((XRControl)xrLabel1).CanGrow = false;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1730.063f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(602.562f, 56.75f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Status";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloCodigo).Borders = (BorderSide)8;
			((XRControl)lblTituloCodigo).CanGrow = false;
			((XRControl)lblTituloCodigo).Dpi = 254f;
			((XRControl)lblTituloCodigo).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloCodigo).LocationFloat = new PointFloat(1138.831f, 0f);
			((XRControl)lblTituloCodigo).Name = "lblTituloCodigo";
			((XRControl)lblTituloCodigo).Padding = new PaddingInfo(10, 5, 0, 0, 254f);
			((XRControl)lblTituloCodigo).SizeF = new SizeF(591.2311f, 56.75f);
			((XRControl)lblTituloCodigo).StylePriority.UseBorders = false;
			((XRControl)lblTituloCodigo).StylePriority.UseFont = false;
			((XRControl)lblTituloCodigo).StylePriority.UsePadding = false;
			((XRControl)lblTituloCodigo).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloCodigo).Text = "Sacado";
			((XRControl)lblTituloCodigo).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel14).Borders = (BorderSide)8;
			((XRControl)xrLabel14).CanGrow = false;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(325.4375f, 56.75f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Número/Ordem";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloDescricao).Borders = (BorderSide)8;
			((XRControl)lblTituloDescricao).CanGrow = false;
			((XRControl)lblTituloDescricao).Dpi = 254f;
			((XRControl)lblTituloDescricao).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloDescricao).LocationFloat = new PointFloat(882.7061f, 0f);
			((XRControl)lblTituloDescricao).Name = "lblTituloDescricao";
			((XRControl)lblTituloDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTituloDescricao).SizeF = new SizeF(256.1251f, 56.75f);
			((XRControl)lblTituloDescricao).StylePriority.UseBorders = false;
			((XRControl)lblTituloDescricao).StylePriority.UseFont = false;
			((XRControl)lblTituloDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloDescricao).Text = "Vencimento";
			((XRControl)lblTituloDescricao).TextAlignment = (TextAlignment)2;
			((XRControl)lblTituloUnidade).Borders = (BorderSide)8;
			((XRControl)lblTituloUnidade).CanGrow = false;
			((XRControl)lblTituloUnidade).Dpi = 254f;
			((XRControl)lblTituloUnidade).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloUnidade).LocationFloat = new PointFloat(325.4375f, 0f);
			((XRControl)lblTituloUnidade).Name = "lblTituloUnidade";
			((XRControl)lblTituloUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTituloUnidade).SizeF = new SizeF(331.4161f, 56.75f);
			((XRControl)lblTituloUnidade).StylePriority.UseBorders = false;
			((XRControl)lblTituloUnidade).StylePriority.UseFont = false;
			((XRControl)lblTituloUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloUnidade).Text = "Nosso número";
			((XRControl)lblTituloUnidade).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloVenda).Borders = (BorderSide)8;
			((XRControl)lblTituloVenda).CanGrow = false;
			((XRControl)lblTituloVenda).Dpi = 254f;
			((XRControl)lblTituloVenda).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloVenda).LocationFloat = new PointFloat(656.8536f, 0f);
			((XRControl)lblTituloVenda).Name = "lblTituloVenda";
			((XRControl)lblTituloVenda).Padding = new PaddingInfo(5, 10, 0, 0, 254f);
			((XRControl)lblTituloVenda).SizeF = new SizeF(225.8524f, 56.75f);
			((XRControl)lblTituloVenda).StylePriority.UseBorders = false;
			((XRControl)lblTituloVenda).StylePriority.UseFont = false;
			((XRControl)lblTituloVenda).StylePriority.UsePadding = false;
			((XRControl)lblTituloVenda).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloVenda).Text = "Valor";
			((XRControl)lblTituloVenda).TextAlignment = (TextAlignment)4;
			ImprimirSomenteSePossuiCampoPersonalizado.Condition = "[CamposPersonalizados].Count()>0";
			ImprimirSomenteSePossuiCampoPersonalizado.Formatting.Visible = (DefaultBoolean)0;
			ImprimirSomenteSePossuiCampoPersonalizado.Name = "ImprimirSomenteSePossuiCampoPersonalizado";
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)Titulo,
				(XRControl)Empresa,
				(XRControl)CNPJ
			});
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 254f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)Titulo).Dpi = 254f;
			((XRControl)Titulo).Font = new Font("Segoe UI", 19f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Titulo).LocationFloat = new PointFloat(0f, 154f);
			((XRControl)Titulo).Name = "Titulo";
			((XRControl)Titulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Titulo).SizeF = new SizeF(2870f, 99.99994f);
			((XRControl)Titulo).StylePriority.UseFont = false;
			((XRControl)Titulo).StylePriority.UseTextAlignment = false;
			((XRControl)Titulo).Text = "Resumo da importação do arquivo de retorno";
			((XRControl)Titulo).TextAlignment = (TextAlignment)2;
			((XRControl)Empresa).Dpi = 254f;
			((XRControl)Empresa).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Empresa).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)Empresa).Name = "Empresa";
			((XRControl)Empresa).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Empresa).SizeF = new SizeF(2870f, 58.41998f);
			((XRControl)Empresa).StylePriority.UseFont = false;
			((XRControl)Empresa).Text = "Empresa";
			((XRControl)CNPJ).Dpi = 254f;
			((XRControl)CNPJ).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)CNPJ).LocationFloat = new PointFloat(0f, 58.41996f);
			((XRControl)CNPJ).Name = "CNPJ";
			((XRControl)CNPJ).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)CNPJ).SizeF = new SizeF(2870f, 58.42f);
			((XRControl)CNPJ).StylePriority.UseFont = false;
			((XRControl)CNPJ).Text = "CNPJ";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel71 });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 58.42f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)xrLabel71).Dpi = 254f;
			((XRControl)xrLabel71).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel71).Name = "xrLabel71";
			((XRControl)xrLabel71).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel71).SizeF = new SizeF(2870f, 58.42f);
			((XRControl)xrLabel71).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel71).Text = "Desenvolvido por DigiSat Tecnologia LTDA®  www.digisat.com.br";
			((XRControl)xrLabel71).TextAlignment = (TextAlignment)32;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)subReport,
				(XRControl)xrLabel47,
				(XRControl)txtTituloValorTotalizacaoEspecie,
				(XRControl)xrLabel49,
				(XRControl)xrLabel50
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 229f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)subReport).Dpi = 254f;
			((XRControl)subReport).LocationFloat = new PointFloat(1.650093f, 170.58f);
			((XRControl)subReport).Name = "subReport";
			((SubreportBase)subReport).ReportSource = (XtraReport)(object)new ResumoStatus();
			((XRControl)subReport).SizeF = new SizeF(1500f, 58.41995f);
			((XRControl)xrLabel47).Borders = (BorderSide)8;
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(1.650093f, 56.12908f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(1500f, 58.42f);
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel47).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel47).Text = "Totalização por status do processamento";
			((XRControl)xrLabel47).TextAlignment = (TextAlignment)2;
			((XRControl)txtTituloValorTotalizacaoEspecie).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)txtTituloValorTotalizacaoEspecie).Borders = (BorderSide)8;
			((XRControl)txtTituloValorTotalizacaoEspecie).Dpi = 254f;
			((XRControl)txtTituloValorTotalizacaoEspecie).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)txtTituloValorTotalizacaoEspecie).LocationFloat = new PointFloat(1201.65f, 114.5491f);
			((XRControl)txtTituloValorTotalizacaoEspecie).Name = "txtTituloValorTotalizacaoEspecie";
			((XRControl)txtTituloValorTotalizacaoEspecie).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)txtTituloValorTotalizacaoEspecie).SizeF = new SizeF(300f, 58.42f);
			((XRControl)txtTituloValorTotalizacaoEspecie).StylePriority.UseBorderDashStyle = false;
			((XRControl)txtTituloValorTotalizacaoEspecie).StylePriority.UseBorders = false;
			((XRControl)txtTituloValorTotalizacaoEspecie).StylePriority.UseFont = false;
			((XRControl)txtTituloValorTotalizacaoEspecie).StylePriority.UseTextAlignment = false;
			((XRControl)txtTituloValorTotalizacaoEspecie).Text = "Valor crédito";
			((XRControl)txtTituloValorTotalizacaoEspecie).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel49).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel49).Borders = (BorderSide)8;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(1.650093f, 114.5491f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(900f, 58.42005f);
			((XRControl)xrLabel49).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel49).Text = "Status";
			((XRControl)xrLabel49).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel50).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel50).Borders = (BorderSide)8;
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel50).LocationFloat = new PointFloat(901.6501f, 114.5491f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(300f, 58.42003f);
			((XRControl)xrLabel50).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel50).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel50).Text = "Valor";
			((XRControl)xrLabel50).TextAlignment = (TextAlignment)4;
			bindingSource1.DataSource = typeof(ItemResumoViewModel);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)ReportHeader,
				(Band)PageFooter,
				(Band)GroupFooter1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[3] { ImprimirSomenteSePossuiCampoPersonalizado, ItemDestacado, ItemNaoDestacado });
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(50, 50, 45, 69);
			((XtraReport)this).PageHeight = 2100;
			((XtraReport)this).PageWidth = 2970;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ResumoRemessa : Relatorio
	{
		private readonly IEnumerable<ItemResumoViewModel> _itens;

		private readonly string _descricaoConta;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private GroupHeaderBand GroupHeader1;

		private XRLabel lblTituloCodigo;

		private XRLabel xrLabel14;

		private XRLabel lblTituloDescricao;

		private XRLabel lblTituloUnidade;

		private XRLabel lblTituloVenda;

		private BindingSource bindingSource1;

		private FormattingRule ImprimirSomenteSePossuiCampoPersonalizado;

		private XRLabel xrLabel6;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel lblInformacaoCodigo;

		private XRLabel xrLabel1;

		private XRLabel xrLabel8;

		private FormattingRule ItemDestacado;

		private FormattingRule ItemNaoDestacado;

		private ReportHeaderBand ReportHeader;

		private XRLabel Empresa;

		private XRLabel CNPJ;

		private XRLabel lblTitulo;

		private PageFooterBand PageFooter;

		private XRLabel xrLabel71;

		private GroupFooterBand GroupFooter1;

		private XRSubreport subReport;

		private XRLabel xrLabel47;

		private XRLabel xrLabel49;

		private XRLabel xrLabel50;

		public override bool ExibirNoModuloRelatorios => false;

		public ResumoRemessa(IEnumerable<ItemResumoViewModel> itens, string conta)
		{
			_itens = itens;
			_descricaoConta = conta;
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)Empresa).Text = DadosSistema.EmpresaLogada.Nome;
			((XRControl)CNPJ).Text = DadosSistema.EmpresaLogada.CpfCnpjFormatado;
			((XRControl)lblTitulo).Text = (HelperObject.IsNotNull((object)_itens.FirstOrDefault()) ? $"Resumo da remessa do banco: {_descricaoConta}" : "Resumo da remessa");
			((XtraReportBase)this).DataSource = _itens;
			((XtraReportBase)((SubreportBase)subReport).ReportSource).DataSource = ((XtraReportBase)this).DataSource;
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
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
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
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0286: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Expected O, but got Unknown
			//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02da: Unknown result type (might be due to invalid IL or missing references)
			//IL_0391: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Expected O, but got Unknown
			//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04af: Expected O, but got Unknown
			//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ae: Expected O, but got Unknown
			//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0608: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c0: Expected O, but got Unknown
			//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_070e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ba: Expected O, but got Unknown
			//IL_07df: Unknown result type (might be due to invalid IL or missing references)
			//IL_0809: Unknown result type (might be due to invalid IL or missing references)
			//IL_094d: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d62: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f68: Unknown result type (might be due to invalid IL or missing references)
			//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_10e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1183: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_122e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1257: Unknown result type (might be due to invalid IL or missing references)
			//IL_130b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1334: Unknown result type (might be due to invalid IL or missing references)
			//IL_1410: Unknown result type (might be due to invalid IL or missing references)
			//IL_149a: Unknown result type (might be due to invalid IL or missing references)
			//IL_14c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1588: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1687: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b0: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel8 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			lblInformacaoCodigo = new XRLabel();
			ItemDestacado = new FormattingRule();
			ItemNaoDestacado = new FormattingRule();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel1 = new XRLabel();
			lblTituloCodigo = new XRLabel();
			xrLabel14 = new XRLabel();
			lblTituloDescricao = new XRLabel();
			lblTituloUnidade = new XRLabel();
			lblTituloVenda = new XRLabel();
			ImprimirSomenteSePossuiCampoPersonalizado = new FormattingRule();
			ReportHeader = new ReportHeaderBand();
			lblTitulo = new XRLabel();
			Empresa = new XRLabel();
			CNPJ = new XRLabel();
			PageFooter = new PageFooterBand();
			xrLabel71 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			subReport = new XRSubreport();
			xrLabel47 = new XRLabel();
			xrLabel49 = new XRLabel();
			xrLabel50 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel8,
				(XRControl)xrLabel6,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)lblInformacaoCodigo
			});
			((XRControl)Detail).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(ItemDestacado);
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(ItemNaoDestacado);
			((XRControl)Detail).HeightF = 63.42002f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)8;
			((XRControl)xrLabel8).CanGrow = false;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "StatusProcessamentoBoleto")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(2267.438f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(602.562f, 58.42f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).WordWrap = false;
			((XRControl)xrLabel6).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).CanGrow = false;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor", "{0:n2}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(656.8536f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(0, 10, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(225.8524f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UsePadding = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel6).WordWrap = false;
			((XRControl)xrLabel4).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).CanGrow = false;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "NossoNumero")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(310.4375f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(346.4161f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).WordWrap = false;
			((XRControl)xrLabel3).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Vencimento", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(882.7061f, 0f);
			xrLabel3.Multiline = true;
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(256.1252f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel3).WordWrap = false;
			((XRControl)xrLabel2).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel2).Borders = (BorderSide)8;
			((XRControl)xrLabel2).CanGrow = false;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "NumeroDocumento")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(310.4375f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).WordWrap = false;
			((XRControl)lblInformacaoCodigo).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)lblInformacaoCodigo).Borders = (BorderSide)8;
			((XRControl)lblInformacaoCodigo).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Sacado")
			});
			((XRControl)lblInformacaoCodigo).Dpi = 254f;
			((XRControl)lblInformacaoCodigo).LocationFloat = new PointFloat(1138.831f, 0f);
			((XRControl)lblInformacaoCodigo).Name = "lblInformacaoCodigo";
			((XRControl)lblInformacaoCodigo).Padding = new PaddingInfo(10, 5, 0, 0, 254f);
			((XRControl)lblInformacaoCodigo).SizeF = new SizeF(1128.607f, 58.42f);
			((XRControl)lblInformacaoCodigo).StylePriority.UseBorderDashStyle = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UseBorders = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UsePadding = false;
			((XRControl)lblInformacaoCodigo).StylePriority.UseTextAlignment = false;
			((XRControl)lblInformacaoCodigo).TextAlignment = (TextAlignment)16;
			((XRControl)lblInformacaoCodigo).WordWrap = false;
			ItemDestacado.Condition = "[Destacar] == True";
			((BrickStyle)ItemDestacado.Formatting).Font = new Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			ItemDestacado.Name = "ItemDestacado";
			ItemNaoDestacado.Condition = "[Destacar] == False";
			((BrickStyle)ItemNaoDestacado.Formatting).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			ItemNaoDestacado.Name = "ItemNaoDestacado";
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 45.43751f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 69.03834f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel1,
				(XRControl)lblTituloCodigo,
				(XRControl)xrLabel14,
				(XRControl)lblTituloDescricao,
				(XRControl)lblTituloUnidade,
				(XRControl)lblTituloVenda
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 56.75f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel1).Borders = (BorderSide)8;
			((XRControl)xrLabel1).CanGrow = false;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(2267.438f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(602.562f, 56.75f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Status";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloCodigo).Borders = (BorderSide)8;
			((XRControl)lblTituloCodigo).CanGrow = false;
			((XRControl)lblTituloCodigo).Dpi = 254f;
			((XRControl)lblTituloCodigo).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloCodigo).LocationFloat = new PointFloat(1138.831f, 0f);
			((XRControl)lblTituloCodigo).Name = "lblTituloCodigo";
			((XRControl)lblTituloCodigo).Padding = new PaddingInfo(10, 5, 0, 0, 254f);
			((XRControl)lblTituloCodigo).SizeF = new SizeF(1128.607f, 56.75f);
			((XRControl)lblTituloCodigo).StylePriority.UseBorders = false;
			((XRControl)lblTituloCodigo).StylePriority.UseFont = false;
			((XRControl)lblTituloCodigo).StylePriority.UsePadding = false;
			((XRControl)lblTituloCodigo).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloCodigo).Text = "Sacado";
			((XRControl)lblTituloCodigo).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel14).Borders = (BorderSide)8;
			((XRControl)xrLabel14).CanGrow = false;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(310.4375f, 56.75f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Número/Ordem";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloDescricao).Borders = (BorderSide)8;
			((XRControl)lblTituloDescricao).CanGrow = false;
			((XRControl)lblTituloDescricao).Dpi = 254f;
			((XRControl)lblTituloDescricao).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloDescricao).LocationFloat = new PointFloat(882.7061f, 0f);
			((XRControl)lblTituloDescricao).Name = "lblTituloDescricao";
			((XRControl)lblTituloDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTituloDescricao).SizeF = new SizeF(256.1251f, 56.75f);
			((XRControl)lblTituloDescricao).StylePriority.UseBorders = false;
			((XRControl)lblTituloDescricao).StylePriority.UseFont = false;
			((XRControl)lblTituloDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloDescricao).Text = "Vencimento";
			((XRControl)lblTituloDescricao).TextAlignment = (TextAlignment)2;
			((XRControl)lblTituloUnidade).Borders = (BorderSide)8;
			((XRControl)lblTituloUnidade).CanGrow = false;
			((XRControl)lblTituloUnidade).Dpi = 254f;
			((XRControl)lblTituloUnidade).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloUnidade).LocationFloat = new PointFloat(310.4375f, 0f);
			((XRControl)lblTituloUnidade).Name = "lblTituloUnidade";
			((XRControl)lblTituloUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTituloUnidade).SizeF = new SizeF(346.4161f, 56.75f);
			((XRControl)lblTituloUnidade).StylePriority.UseBorders = false;
			((XRControl)lblTituloUnidade).StylePriority.UseFont = false;
			((XRControl)lblTituloUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloUnidade).Text = "Nosso número";
			((XRControl)lblTituloUnidade).TextAlignment = (TextAlignment)1;
			((XRControl)lblTituloVenda).Borders = (BorderSide)8;
			((XRControl)lblTituloVenda).CanGrow = false;
			((XRControl)lblTituloVenda).Dpi = 254f;
			((XRControl)lblTituloVenda).Font = new Font("Segoe UI", 12f);
			((XRControl)lblTituloVenda).LocationFloat = new PointFloat(656.8536f, 0f);
			((XRControl)lblTituloVenda).Name = "lblTituloVenda";
			((XRControl)lblTituloVenda).Padding = new PaddingInfo(5, 10, 0, 0, 254f);
			((XRControl)lblTituloVenda).SizeF = new SizeF(225.8524f, 56.75f);
			((XRControl)lblTituloVenda).StylePriority.UseBorders = false;
			((XRControl)lblTituloVenda).StylePriority.UseFont = false;
			((XRControl)lblTituloVenda).StylePriority.UsePadding = false;
			((XRControl)lblTituloVenda).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloVenda).Text = "Valor";
			((XRControl)lblTituloVenda).TextAlignment = (TextAlignment)4;
			ImprimirSomenteSePossuiCampoPersonalizado.Condition = "[CamposPersonalizados].Count()>0";
			ImprimirSomenteSePossuiCampoPersonalizado.Formatting.Visible = (DefaultBoolean)0;
			ImprimirSomenteSePossuiCampoPersonalizado.Name = "ImprimirSomenteSePossuiCampoPersonalizado";
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)lblTitulo,
				(XRControl)Empresa,
				(XRControl)CNPJ
			});
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 254f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)lblTitulo).Dpi = 254f;
			((XRControl)lblTitulo).Font = new Font("Segoe UI", 19f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)lblTitulo).LocationFloat = new PointFloat(0f, 154f);
			((XRControl)lblTitulo).Name = "lblTitulo";
			((XRControl)lblTitulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTitulo).SizeF = new SizeF(2870f, 99.99994f);
			((XRControl)lblTitulo).StylePriority.UseFont = false;
			((XRControl)lblTitulo).StylePriority.UseTextAlignment = false;
			((XRControl)lblTitulo).Text = "Resumo da remessa";
			((XRControl)lblTitulo).TextAlignment = (TextAlignment)2;
			((XRControl)Empresa).Dpi = 254f;
			((XRControl)Empresa).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Empresa).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)Empresa).Name = "Empresa";
			((XRControl)Empresa).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Empresa).SizeF = new SizeF(2870f, 58.41998f);
			((XRControl)Empresa).StylePriority.UseFont = false;
			((XRControl)Empresa).Text = "Empresa";
			((XRControl)CNPJ).Dpi = 254f;
			((XRControl)CNPJ).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)CNPJ).LocationFloat = new PointFloat(0f, 58.41996f);
			((XRControl)CNPJ).Name = "CNPJ";
			((XRControl)CNPJ).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)CNPJ).SizeF = new SizeF(2870f, 58.42f);
			((XRControl)CNPJ).StylePriority.UseFont = false;
			((XRControl)CNPJ).Text = "CNPJ";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel71 });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 58.42f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)xrLabel71).Dpi = 254f;
			((XRControl)xrLabel71).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel71).Name = "xrLabel71";
			((XRControl)xrLabel71).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel71).SizeF = new SizeF(2870f, 58.42f);
			((XRControl)xrLabel71).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel71).Text = "Desenvolvido por DigiSat Tecnologia LTDA®  www.digisat.com.br";
			((XRControl)xrLabel71).TextAlignment = (TextAlignment)32;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)subReport,
				(XRControl)xrLabel47,
				(XRControl)xrLabel49,
				(XRControl)xrLabel50
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 229f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)subReport).Dpi = 254f;
			((XRControl)subReport).LocationFloat = new PointFloat(1.650093f, 170.58f);
			((XRControl)subReport).Name = "subReport";
			((SubreportBase)subReport).ReportSource = (XtraReport)(object)new ResumoStatusRemessa();
			((XRControl)subReport).SizeF = new SizeF(1200f, 58.41995f);
			((XRControl)xrLabel47).Borders = (BorderSide)8;
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel47).LocationFloat = new PointFloat(1.650093f, 56.12908f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(1200f, 58.42f);
			((XRControl)xrLabel47).StylePriority.UseBorders = false;
			((XRControl)xrLabel47).StylePriority.UseFont = false;
			((XRControl)xrLabel47).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel47).Text = "Totalização por status do processamento";
			((XRControl)xrLabel47).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel49).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel49).Borders = (BorderSide)8;
			((XRControl)xrLabel49).Dpi = 254f;
			((XRControl)xrLabel49).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel49).LocationFloat = new PointFloat(1.650093f, 114.5491f);
			((XRControl)xrLabel49).Name = "xrLabel49";
			((XRControl)xrLabel49).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel49).SizeF = new SizeF(900f, 58.42005f);
			((XRControl)xrLabel49).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel49).StylePriority.UseBorders = false;
			((XRControl)xrLabel49).StylePriority.UseFont = false;
			((XRControl)xrLabel49).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel49).Text = "Status";
			((XRControl)xrLabel49).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel50).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel50).Borders = (BorderSide)8;
			((XRControl)xrLabel50).Dpi = 254f;
			((XRControl)xrLabel50).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel50).LocationFloat = new PointFloat(901.6501f, 114.5491f);
			((XRControl)xrLabel50).Name = "xrLabel50";
			((XRControl)xrLabel50).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel50).SizeF = new SizeF(300f, 58.42003f);
			((XRControl)xrLabel50).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel50).StylePriority.UseBorders = false;
			((XRControl)xrLabel50).StylePriority.UseFont = false;
			((XRControl)xrLabel50).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel50).Text = "Valor";
			((XRControl)xrLabel50).TextAlignment = (TextAlignment)4;
			bindingSource1.DataSource = typeof(ItemResumoViewModel);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)ReportHeader,
				(Band)PageFooter,
				(Band)GroupFooter1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[3] { ImprimirSomenteSePossuiCampoPersonalizado, ItemDestacado, ItemNaoDestacado });
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(50, 50, 45, 69);
			((XtraReport)this).PageHeight = 2100;
			((XtraReport)this).PageWidth = 2970;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ResumoStatus : XtraReport
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private GroupHeaderBand GroupHeader1;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel1;

		private XRLabel xrLabel3;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel2;

		private ReportHeaderBand ReportHeader;

		private XRLabel xrLabel5;

		private XRLabel xrLabel6;

		public ResumoStatus()
		{
			InitializeComponent();
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
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Expected O, but got Unknown
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			//IL_0108: Unknown result type (might be due to invalid IL or missing references)
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Expected O, but got Unknown
			//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b6: Expected O, but got Unknown
			//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_031e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b2: Expected O, but got Unknown
			//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0400: Unknown result type (might be due to invalid IL or missing references)
			//IL_0442: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Expected O, but got Unknown
			//IL_0487: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a8: Expected O, but got Unknown
			//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0610: Unknown result type (might be due to invalid IL or missing references)
			//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c1: Expected O, but got Unknown
			//IL_0700: Unknown result type (might be due to invalid IL or missing references)
			//IL_0729: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			XRSummary val3 = new XRSummary();
			XRSummary val4 = new XRSummary();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			GroupHeader1 = new GroupHeaderBand();
			GroupFooter1 = new GroupFooterBand();
			xrLabel5 = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel3 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel6 = new XRLabel();
			xrLabel2 = new XRLabel();
			ReportHeader = new ReportHeaderBand();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
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
			bindingSource1.DataSource = typeof(ItemResumoViewModel);
			((XRControl)GroupHeader1).Dpi = 254f;
			GroupHeader1.GroupFields.AddRange((GroupField[])(object)new GroupField[1]
			{
				new GroupField("StatusProcessamentoBoleto", (XRColumnSortOrder)1)
			});
			((XRControl)GroupHeader1).HeightF = 0f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel5,
				(XRControl)xrLabel1,
				(XRControl)xrLabel3
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorCredito")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1200f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:n2}";
			val.Running = (SummaryRunning)1;
			xrLabel5.Summary = val;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "StatusProcessamentoBoleto")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(900f, 58.42f);
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(900f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:n2}";
			val2.Running = (SummaryRunning)1;
			xrLabel3.Summary = val2;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)4;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel6,
				(XRControl)xrLabel2
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 58.42f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)xrLabel6).Borders = (BorderSide)2;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorCredito")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1200f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			val3.FormatString = "{0:c2}";
			val3.Running = (SummaryRunning)2;
			xrLabel6.Summary = val3;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel2).Borders = (BorderSide)2;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(900f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			val4.FormatString = "{0:c2}";
			val4.Running = (SummaryRunning)2;
			xrLabel2.Summary = val4;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)4;
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 0f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)GroupFooter1,
				(Band)ReportFooter,
				(Band)ReportHeader
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 2159;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ResumoStatusRemessa : XtraReport
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private GroupHeaderBand GroupHeader1;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel1;

		private XRLabel xrLabel3;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel2;

		private ReportHeaderBand ReportHeader;

		public ResumoStatusRemessa()
		{
			InitializeComponent();
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
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Expected O, but got Unknown
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Expected O, but got Unknown
			//IL_029d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a3: Expected O, but got Unknown
			//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0333: Unknown result type (might be due to invalid IL or missing references)
			//IL_0339: Expected O, but got Unknown
			//IL_0378: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0490: Expected O, but got Unknown
			//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			GroupHeader1 = new GroupHeaderBand();
			GroupFooter1 = new GroupFooterBand();
			xrLabel1 = new XRLabel();
			xrLabel3 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel2 = new XRLabel();
			ReportHeader = new ReportHeaderBand();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((Band)Detail).Expanded = false;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
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
			bindingSource1.DataSource = typeof(ItemResumoViewModel);
			((XRControl)GroupHeader1).Dpi = 254f;
			((Band)GroupHeader1).Expanded = false;
			GroupHeader1.GroupFields.AddRange((GroupField[])(object)new GroupField[1]
			{
				new GroupField("StatusProcessamentoBoleto", (XRColumnSortOrder)1)
			});
			((XRControl)GroupHeader1).HeightF = 0f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel3
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "StatusProcessamentoBoleto")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(900f, 58.42f);
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(900f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:n2}";
			val.Running = (SummaryRunning)1;
			xrLabel3.Summary = val;
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)4;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel2 });
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 58.42f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)xrLabel2).Borders = (BorderSide)2;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Valor")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(900f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:c2}";
			val2.Running = (SummaryRunning)2;
			xrLabel2.Summary = val2;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)4;
			((XRControl)ReportHeader).Dpi = 254f;
			((Band)ReportHeader).Expanded = false;
			((XRControl)ReportHeader).HeightF = 0f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1,
				(Band)GroupFooter1,
				(Band)ReportFooter,
				(Band)ReportHeader
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 2159;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}

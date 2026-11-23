using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.AutoatendimentoBalancas;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Auxiliares.Restaurantes;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Exceptions;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.AutoatendimentoBalancas.Reports;
using Otus.Modulo.AutoatendimentoBalancas.ViewModels;
using Otus.Modulo.AutoatendimentoBalancas.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.AutoatendimentoBalancas")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.AutoatendimentoBalancas")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AutoatendimentoBalancas
{
	[Module(ModuleName = "AutoatendimentoBalancaModulo", OnDemand = true)]
	public class AutoatendimentoBalancaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AutoatendimentoBalancaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AutoatendimentoBalancasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AutoatendimentoBalancaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AutoatendimentoBalancas.Views
{
	public class AutoatendimentoBalancasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AutoatendimentoBalancasView()
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
				Uri resourceLocator = new Uri("/Modulo.AutoatendimentoBalancas;component/views/autoatendimentobalancasview.xaml", UriKind.Relative);
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
	public class AutoatendimentoBalancaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AutoatendimentoBalancaView()
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
				Uri resourceLocator = new Uri("/Modulo.AutoatendimentoBalancas;component/views/autoatendimentobalancaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AutoatendimentoBalancas.ViewModels
{
	public class AutoatendimentoBalancasViewModel : ModuloBase
	{
		public AutoatendimentoBalancasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAutoatendimentoBalancaRegion, "AutoatendimentoBalancaView", (IResolucao)new ResolucaoGrande())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class AutoatendimentoBalancaViewModel : BaseMinimizar
	{
		private readonly IBalancaService _balancaService;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IWService _service;

		public bool AtivouIgnorarProdutoSubstituto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AtivouIgnorarProdutoSubstituto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AtivouIgnorarProdutoSubstituto), value);
			}
		}

		public string NomeFantasia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeFantasia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeFantasia), value);
			}
		}

		public ItensLancamentoManualViewModel ItensLancamentoManual
		{
			get
			{
				return ((BindableBase)this).GetProperty<ItensLancamentoManualViewModel>((Expression<Func<ItensLancamentoManualViewModel>>)(() => ItensLancamentoManual));
			}
			set
			{
				((BindableBase)this).SetProperty<ItensLancamentoManualViewModel>((Expression<Func<ItensLancamentoManualViewModel>>)(() => ItensLancamentoManual), value);
			}
		}

		public CapturaPesoViewModel CapturaPeso
		{
			get
			{
				return ((BindableBase)this).GetProperty<CapturaPesoViewModel>((Expression<Func<CapturaPesoViewModel>>)(() => CapturaPeso));
			}
			set
			{
				((BindableBase)this).SetProperty<CapturaPesoViewModel>((Expression<Func<CapturaPesoViewModel>>)(() => CapturaPeso), value);
			}
		}

		public ICommand IgnorarProdutoSubstitutoCommand { get; set; }

		public AutoatendimentoBalancaViewModel(IBalancaService balancaService, IImpressoraNaoFiscalService impressoraNaoFiscalService, IWService service)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			_balancaService = balancaService;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_service = service;
			NomeFantasia = DadosSistema.EmpresaLogada.Fantasia;
			IgnorarProdutoSubstitutoCommand = (ICommand)new DelegateCommand((Action)IgnorarProdutoSubstituto);
		}

		private void IgnorarProdutoSubstituto()
		{
			CapturaPesoViewModel capturaPeso = CapturaPeso;
			if (capturaPeso == null || !capturaPeso.ModalQuantidadeAberta)
			{
				AtivouIgnorarProdutoSubstituto = !AtivouIgnorarProdutoSubstituto;
			}
		}

		protected override async void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			try
			{
				ConfiguracaoAutoatendimento configuracaoClient = DadosSistema.ObterConfiguracaoClient().ObterConfiguracao().ConfiguracaoAutoatendimento;
				await _service.Canal<IAutoatendimentoBalancaModuleService, Task<AutoatendimentoBalancaDto>>((Func<IAutoatendimentoBalancaModuleService, Task<AutoatendimentoBalancaDto>>)delegate(IAutoatendimentoBalancaModuleService x)
				{
					ConfiguracaoAutoatendimento obj = configuracaoClient;
					return x.Obter(((obj != null) ? obj.ProdutoFixoReferencia : null) ?? string.Empty);
				}).ContinueWith(delegate(Task<AutoatendimentoBalancaDto> task)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						if (task.IsFaulted)
						{
							HelperException.ShowLastException(task.Exception, "", false, false);
							((NavigationBase)this).FecharView();
						}
						else
						{
							AutoatendimentoBalancaDto result = task.Result;
							if (DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.TipoFinalizacaoAutoatendimentoBalanca.Valor == 2)
							{
								CapturaPeso = new ImpressaoCapturaPesoViewModel(_balancaService, _impressoraNaoFiscalService, result, () => AtivouIgnorarProdutoSubstituto, DesativarIgnorarProdutoSubstituto);
							}
							else
							{
								if (DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.GerarNumeracaoComandaAutoatendimentoAutomaticamente.Valor && HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.SequenciaComandaAutoatendimento.Referencia))
								{
									throw new Exception("Sequência de numeração para comandas lançados pelo 'Autoatendimento' não encontrada");
								}
								CapturaPeso = new ComandaCapturaPesoViewModel(_balancaService, _service, result, () => AtivouIgnorarProdutoSubstituto, DesativarIgnorarProdutoSubstituto, _impressoraNaoFiscalService);
							}
							ItensLancamentoManual = new ItensLancamentoManualViewModel(result, CapturaPeso.ProcessarItemManual, CapturaPeso.PodeProcessarItemManual);
						}
					});
				});
				<>n__0(sender, navigationParameters);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				((NavigationBase)this).FecharView();
			}
		}

		private void DesativarIgnorarProdutoSubstituto()
		{
			AtivouIgnorarProdutoSubstituto = false;
		}

		public override void Dispose()
		{
			ItensLancamentoManual?.Dispose();
			CapturaPeso?.Dispose();
			((BaseMinimizar)this).Dispose();
		}

		[CompilerGenerated]
		[DebuggerHidden]
		private void <>n__0(object sender, NavigationParameters navigationParameters)
		{
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
		}
	}
	public class CapturaPesoViewModel : UpdateViewBindableBase, IDisposable
	{
		private readonly IBalancaService _balancaService;

		private BackgroundWorker _bwBalanca;

		private bool _processando;

		private volatile bool _aguardandoItemCapturaPeso;

		private volatile bool _pesagemDescartada;

		private decimal _quantidadeLida;

		private readonly Func<bool> _ativouIgnorarProdutoSubstituto;

		private ItemCapturaPeso _itemPrincipal;

		private ItemCapturaPeso _itemEscolhidoPeloCliente;

		private bool _temItemPrincipal;

		private ItemLancamentoManual _itemModal;

		private ItemCapturaPeso ItemAtivo => _itemEscolhidoPeloCliente ?? _itemPrincipal;

		public ImageSource Fundo
		{
			get
			{
				return ((BindableBase)this).GetProperty<ImageSource>((Expression<Func<ImageSource>>)(() => Fundo));
			}
			set
			{
				((BindableBase)this).SetProperty<ImageSource>((Expression<Func<ImageSource>>)(() => Fundo), value);
			}
		}

		private static bool SolicitarQuantidade
		{
			get
			{
				ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
				bool? obj;
				if (configuracaoDto == null)
				{
					obj = null;
				}
				else
				{
					AutoatendimentoRestauranteDto autoatendimentoRestaurante = configuracaoDto.AutoatendimentoRestaurante;
					if (autoatendimentoRestaurante == null)
					{
						obj = null;
					}
					else
					{
						BoleanoDto solicitarQuantidadeItemUnidade = autoatendimentoRestaurante.SolicitarQuantidadeItemUnidade;
						obj = ((solicitarQuantidadeItemUnidade != null) ? new bool?(solicitarQuantidadeItemUnidade.Valor) : null);
					}
				}
				return obj ?? false;
			}
		}

		public Lista<ItemCapturaPeso> Lista { get; protected set; }

		public string MensagemOrientacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MensagemOrientacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MensagemOrientacao), value);
			}
		}

		public string MensagemTecnica
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MensagemTecnica));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MensagemTecnica), value);
			}
		}

		public decimal Peso
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Peso));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Peso), value);
				MostrarPesoTotal = Peso > 0m;
				CalcularTotal();
			}
		}

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value);
			}
		}

		public bool MostrarPesoTotal
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarPesoTotal));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarPesoTotal), value);
			}
		}

		public bool MostrarPrecoItem => PrecoItem > 0m;

		public decimal PrecoItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoItem));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoItem), value);
			}
		}

		public bool MostrarImagemTelaInicialBuffet
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarImagemTelaInicialBuffet));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarImagemTelaInicialBuffet), value);
			}
		}

		public Visibility ListaVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => ListaVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => ListaVisivel), value);
			}
		}

		public Visibility ModalQuantidadeVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => ModalQuantidadeVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => ModalQuantidadeVisivel), value);
			}
		}

		public bool ModalQuantidadeAberta => ModalQuantidadeVisivel == Visibility.Visible;

		public string DescricaoModal
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoModal));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoModal), value);
			}
		}

		public int QuantidadeModal
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => QuantidadeModal));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => QuantidadeModal), value);
			}
		}

		public double LarguraItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => LarguraItem));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => LarguraItem), value);
			}
		}

		public double AlturaItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => AlturaItem));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => AlturaItem), value);
			}
		}

		public double AlturaIconeItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => AlturaIconeItem));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => AlturaIconeItem), value);
			}
		}

		public double AlturaLinhaItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => AlturaLinhaItem));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => AlturaLinhaItem), value);
			}
		}

		public double FonteItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => FonteItem));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => FonteItem), value);
			}
		}

		public double FonteOrientacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => FonteOrientacao));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => FonteOrientacao), value);
			}
		}

		public double FonteTecnica
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => FonteTecnica));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => FonteTecnica), value);
			}
		}

		public double FontePreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => FontePreco));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => FontePreco), value);
			}
		}

		public double FontePeso
		{
			get
			{
				return ((BindableBase)this).GetProperty<double>((Expression<Func<double>>)(() => FontePeso));
			}
			set
			{
				((BindableBase)this).SetProperty<double>((Expression<Func<double>>)(() => FontePeso), value);
			}
		}

		protected decimal QuantidadeItemManual { get; private set; } = 1m;


		public ItemBase Item { get; protected set; }

		public ICommand SelecionarCommand { get; set; }

		public ICommand IncrementarQuantidadeCommand { get; set; }

		public ICommand DecrementarQuantidadeCommand { get; set; }

		public ICommand ConfirmarQuantidadeCommand { get; set; }

		public ICommand CancelarQuantidadeCommand { get; set; }

		public CapturaPesoViewModel(IBalancaService balancaService, AutoatendimentoBalancaDto autoatendimentoBalanca, Func<bool> ativouIgnorarProdutoSubstituto)
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Expected O, but got Unknown
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			_balancaService = balancaService;
			_ativouIgnorarProdutoSubstituto = ativouIgnorarProdutoSubstituto;
			SelecionarCommand = (ICommand)new DelegateCommand<ItemCapturaPeso>((Action<ItemCapturaPeso>)SelecionouItemCapturaPeso);
			IncrementarQuantidadeCommand = (ICommand)new DelegateCommand((Action)IncrementarQuantidade);
			DecrementarQuantidadeCommand = (ICommand)new DelegateCommand((Action)DecrementarQuantidade);
			ConfirmarQuantidadeCommand = (ICommand)new DelegateCommand((Action)ConfirmarQuantidade);
			CancelarQuantidadeCommand = (ICommand)new DelegateCommand((Action)CancelarQuantidade);
			ListaVisivel = Visibility.Collapsed;
			ModalQuantidadeVisivel = Visibility.Collapsed;
			Lista = new Lista<ItemCapturaPeso>();
			IEnumerable<AutoatendimentoBalancaItemDto> source = autoatendimentoBalanca.Itens.Where((AutoatendimentoBalancaItemDto x) => x.Produto.ProdutoServico.Produto.Pesavel);
			Lista.Adicionar(source.Select((AutoatendimentoBalancaItemDto x) => new ItemCapturaPeso(x)));
			ConfigurarEscalaInterface();
			ConfigurarItemPrincipal();
			AtualizarPrecoItemSelecionado();
			InicializarBalanca();
			CarregarImagemFundo();
		}

		private void InicializarBalanca()
		{
			try
			{
				_balancaService.Inicializar(true);
				_balancaService.ObterTimeOut();
				_bwBalanca = new BackgroundWorker
				{
					WorkerSupportsCancellation = true
				};
				_bwBalanca.DoWork += BwBalancaOnDoWork;
				_bwBalanca.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				MensagemOrientacao = "Sem comunicação com a balança,\nverifique as configurações.";
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async void BwBalancaOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			decimal ultimoPeso = default(decimal);
			while (!_bwBalanca.CancellationPending && !Dispatcher.CurrentDispatcher.HasShutdownStarted && !Dispatcher.CurrentDispatcher.HasShutdownFinished)
			{
				Thread.Sleep(100);
				try
				{
					if (_processando)
					{
						continue;
					}
					CapturaPesoViewModel capturaPesoViewModel = this;
					ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
					object mensagemOrientacao;
					if (configuracaoDto == null)
					{
						mensagemOrientacao = null;
					}
					else
					{
						AutoatendimentoRestauranteDto autoatendimentoRestaurante = configuracaoDto.AutoatendimentoRestaurante;
						if (autoatendimentoRestaurante == null)
						{
							mensagemOrientacao = null;
						}
						else
						{
							TextoDto textoTelaInicialAutoatendimento = autoatendimentoRestaurante.TextoTelaInicialAutoatendimento;
							mensagemOrientacao = ((textoTelaInicialAutoatendimento != null) ? HelperString.ToStringEmptyWhenNull(textoTelaInicialAutoatendimento.Valor) : null);
						}
					}
					capturaPesoViewModel.MensagemOrientacao = (string)mensagemOrientacao;
					decimal peso = (await _balancaService.LerPeso()).Peso;
					if (peso >= 0m && peso <= 0.010m)
					{
						ultimoPeso = default(decimal);
						continue;
					}
					if (peso < 0m)
					{
						throw new NaoFoiPossivelObterDadosBalancaException();
					}
					if (ultimoPeso == 0m || ultimoPeso != peso)
					{
						Processar(peso);
						ultimoPeso = peso;
					}
				}
				catch (NaoFoiPossivelObterDadosBalancaException val)
				{
					NaoFoiPossivelObterDadosBalancaException val2 = val;
					NaoFoiPossivelObterDadosBalancaException e = val2;
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException((Exception)(object)e, "", false, false);
					});
				}
				catch (TimeoutException)
				{
					ultimoPeso = default(decimal);
				}
				catch (Exception)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						MensagemOrientacao = "Falha de comunicação com a balança.";
					});
					break;
				}
			}
		}

		private void Processar(decimal quantidade)
		{
			if (Lista.Numero == 0)
			{
				return;
			}
			bool aguardarSelecao = Lista.Numero > 1 && !_temItemPrincipal;
			_processando = true;
			_quantidadeLida = quantidade;
			_pesagemDescartada = false;
			ThreadPrincipal.Executar((Action)delegate
			{
				Item = (_temItemPrincipal ? ItemAtivo : Lista.ItemSelecionado);
				_itemEscolhidoPeloCliente = null;
				if (aguardarSelecao)
				{
					Peso = quantidade;
					MostrarListaItensCapturaPeso();
				}
				else
				{
					_pesagemDescartada = AplicarRegrasTara();
				}
			});
			if (aguardarSelecao)
			{
				AguardarItemCapturaPeso();
			}
			if (!_pesagemDescartada)
			{
				Processar();
				Finalizar();
			}
			_processando = false;
		}

		private bool AplicarRegrasTara()
		{
			decimal tara = Item.Dto.ProdutoEmpresa.Tara;
			AutoatendimentoRestauranteDto autoatendimentoRestaurante = DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante;
			if (_ativouIgnorarProdutoSubstituto() && autoatendimentoRestaurante.NaoDescontarTaraProduto.Valor)
			{
				Peso = _quantidadeLida;
				return false;
			}
			if (_quantidadeLida > tara)
			{
				Peso = _quantidadeLida - tara;
				return false;
			}
			if (autoatendimentoRestaurante.NaoImprimirCasoPesoMenorTara.Valor)
			{
				Peso = 0m;
				return true;
			}
			Peso = _quantidadeLida;
			return false;
		}

		private void MostrarListaItensCapturaPeso()
		{
			ListaVisivel = Visibility.Visible;
			MensagemOrientacao = "SELECIONE O PRODUTO";
			_aguardandoItemCapturaPeso = true;
		}

		private void AguardarItemCapturaPeso()
		{
			while (_aguardandoItemCapturaPeso)
			{
				Thread.Sleep(100);
			}
		}

		private void Finalizar()
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Peso = 0m;
				Item = null;
				QuantidadeItemManual = 1m;
				PosicionarNoItemAtivo();
				MensagemOrientacao = "BOM APETITE!";
			});
			Thread.Sleep(3000);
		}

		private void SelecionouItemCapturaPeso(ItemCapturaPeso itemCapturaPeso)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				if (_aguardandoItemCapturaPeso)
				{
					Item = itemCapturaPeso;
					_pesagemDescartada = AplicarRegrasTara();
					ListaVisivel = Visibility.Collapsed;
					_aguardandoItemCapturaPeso = false;
				}
				else if (_temItemPrincipal)
				{
					_itemEscolhidoPeloCliente = itemCapturaPeso;
					PosicionarNoItemAtivo();
				}
			});
		}

		private void ConfigurarItemPrincipal()
		{
			ConfiguracaoAutoatendimento configuracaoAutoatendimento = DadosSistema.ObterConfiguracaoClient().ObterConfiguracao().ConfiguracaoAutoatendimento;
			string referencia = ((configuracaoAutoatendimento != null) ? configuracaoAutoatendimento.ProdutoPrincipalReferencia : null);
			if (HelperString.IsNullOrEmpty(referencia))
			{
				return;
			}
			_itemPrincipal = Lista.Itens.FirstOrDefault((ItemCapturaPeso x) => referencia.Equals(((EntityBaseDto)x.Dto).Id));
			if (!HelperObject.IsNull((object)_itemPrincipal))
			{
				_temItemPrincipal = true;
				ListaVisivel = Visibility.Visible;
				PosicionarNoItemAtivo();
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					PosicionarNoItemAtivo();
				});
			}
		}

		private void PosicionarNoItemAtivo()
		{
			if (_temItemPrincipal)
			{
				Lista.ItemSelecionado = ItemAtivo;
				AtualizarPrecoItemSelecionado();
			}
		}

		private void AtualizarPrecoItemSelecionado()
		{
			PrecoItem = ((!_temItemPrincipal) ? ((Lista.Itens.Count != 1) ? 0m : (Lista?.Itens?.FirstOrDefault()?.Preco ?? 0m)) : (ItemAtivo?.Preco ?? 0m));
		}

		private void ConfigurarEscalaInterface()
		{
			ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
			bool? obj;
			if (configuracaoDto == null)
			{
				obj = null;
			}
			else
			{
				AutoatendimentoRestauranteDto autoatendimentoRestaurante = configuracaoDto.AutoatendimentoRestaurante;
				if (autoatendimentoRestaurante == null)
				{
					obj = null;
				}
				else
				{
					BoleanoDto interfaceAmpliadaAutoatendimento = autoatendimentoRestaurante.InterfaceAmpliadaAutoatendimento;
					obj = ((interfaceAmpliadaAutoatendimento != null) ? new bool?(interfaceAmpliadaAutoatendimento.Valor) : null);
				}
			}
			double num = ((obj ?? false) ? 1.3 : 1.0);
			LarguraItem = 240.0 * num;
			AlturaItem = 120.0 * num;
			AlturaIconeItem = 70.0 * num;
			AlturaLinhaItem = 12.0 * num;
			FonteItem = 14.0 * num;
			FonteOrientacao = 48.0 * num;
			FonteTecnica = 24.0 * num;
			FontePreco = 40.0 * num;
			FontePeso = 30.0 * num;
		}

		private void CarregarImagemFundo()
		{
			ConfiguracaoClient val = DadosSistema.ObterConfiguracaoClient().ObterConfiguracao();
			try
			{
				MostrarImagemTelaInicialBuffet = false;
				if (!HelperString.IsNullOrEmpty(val.DiretorioImagemTelaInicialBuffet) && File.Exists(val.DiretorioImagemTelaInicialBuffet))
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.UriSource = new Uri(val.DiretorioImagemTelaInicialBuffet, UriKind.Absolute);
					bitmapImage.EndInit();
					Fundo = bitmapImage;
					MostrarImagemTelaInicialBuffet = true;
				}
			}
			catch (Exception)
			{
			}
		}

		protected virtual void Processar()
		{
		}

		public bool PodeProcessarItemManual(ItemLancamentoManual itemLancamentoManual)
		{
			if (itemLancamentoManual != null && !_processando)
			{
				return !ModalQuantidadeAberta;
			}
			return false;
		}

		public void ProcessarItemManual(ItemLancamentoManual itemLancamentoManual)
		{
			if (!HelperObject.IsNull((object)itemLancamentoManual) && !_processando && !ModalQuantidadeAberta)
			{
				if (!SolicitarQuantidade)
				{
					ExecutarItemManual(itemLancamentoManual, 1);
				}
				else
				{
					AbrirModalQuantidade(itemLancamentoManual);
				}
			}
		}

		private async void ExecutarItemManual(ItemLancamentoManual itemLancamentoManual, int quantidade)
		{
			await Task.Factory.StartNew(delegate
			{
				try
				{
					_processando = true;
					Item = itemLancamentoManual;
					QuantidadeItemManual = quantidade;
					Processar();
					Finalizar();
				}
				catch (Exception)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						MensagemTecnica = "Não foi possível adicionar o produto. Por favor, tente novamente.";
						((UpdateViewBindableBase)this).DormirAsync(3000, (Action<object>)delegate
						{
							MensagemTecnica = string.Empty;
						});
					});
				}
				finally
				{
					_processando = false;
				}
			});
		}

		private void AbrirModalQuantidade(ItemLancamentoManual itemLancamentoManual)
		{
			_itemModal = itemLancamentoManual;
			DescricaoModal = itemLancamentoManual.Descricao;
			QuantidadeModal = 1;
			ModalQuantidadeVisivel = Visibility.Visible;
		}

		private void IncrementarQuantidade()
		{
			QuantidadeModal++;
		}

		private void DecrementarQuantidade()
		{
			if (QuantidadeModal > 1)
			{
				QuantidadeModal--;
			}
		}

		private void ConfirmarQuantidade()
		{
			ItemLancamentoManual itemModal = _itemModal;
			int quantidadeModal = QuantidadeModal;
			FecharModalQuantidade();
			if (!HelperObject.IsNull((object)itemModal))
			{
				ExecutarItemManual(itemModal, quantidadeModal);
			}
		}

		private void CancelarQuantidade()
		{
			FecharModalQuantidade();
		}

		private void FecharModalQuantidade()
		{
			ModalQuantidadeVisivel = Visibility.Collapsed;
			_itemModal = null;
		}

		private void CalcularTotal()
		{
			decimal peso = Peso;
			decimal? obj = Item?.Preco;
			Total = HelperDecimal.TruncarDuasCasasDecimais(((decimal?)peso * obj) ?? 0m);
		}

		public void Dispose()
		{
			if (HelperObject.IsNotNull((object)_bwBalanca))
			{
				_bwBalanca.CancelAsync();
				_bwBalanca.DoWork -= BwBalancaOnDoWork;
				_bwBalanca.Dispose();
			}
			((ViewModelValidationBase)Lista).Dispose();
		}
	}
	public class ComandaCapturaPesoViewModel : CapturaPesoViewModel
	{
		private readonly IWService _service;

		private readonly Func<bool> _ativouIgnorarProdutoSubstituto;

		private readonly Action _desativarIgnorarProdutoSubstituto;

		private bool _aguardandoLeituraComanda;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		public bool FocoNoCodigoComanda
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCodigoComanda));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCodigoComanda), value);
			}
		}

		public string CodigoComanda
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CodigoComanda));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CodigoComanda), value, (Action)OnCodigoComandaChanged);
			}
		}

		public ComandaCapturaPesoViewModel(IBalancaService balancaService, IWService service, AutoatendimentoBalancaDto autoatendimentoBalancaDto, Func<bool> ativouIgnorarProdutoSubstituto, Action desativarIgnorarProdutoSubstituto, IImpressoraNaoFiscalService impressoraNaoFiscalService)
			: base(balancaService, autoatendimentoBalancaDto, ativouIgnorarProdutoSubstituto)
		{
			_service = service;
			_ativouIgnorarProdutoSubstituto = ativouIgnorarProdutoSubstituto;
			_desativarIgnorarProdutoSubstituto = desativarIgnorarProdutoSubstituto;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
		}

		private void OnCodigoComandaChanged()
		{
			if (string.IsNullOrWhiteSpace(CodigoComanda))
			{
				return;
			}
			if (HelperString.ToLong(CodigoComanda) < 1)
			{
				base.MensagemOrientacao = "NÃO FOI POSSÍVEL LER COMANDA, ESCANEIE NOVAMENTE!";
				CodigoComanda = string.Empty;
			}
			else if (DadosSistema.ConfiguracaoDto.Saida.NumeroMaximoMesasContasCliente.Valor > 0 && HelperString.ToLong(CodigoComanda) > DadosSistema.ConfiguracaoDto.Saida.NumeroMaximoMesasContasCliente.Valor)
			{
				base.MensagemTecnica = "NÚMERO INFORMADO FORA DO LIMITE ESTABELECIDO!";
				CodigoComanda = string.Empty;
				((UpdateViewBindableBase)this).DormirAsync(3000, (Action<object>)delegate
				{
					base.MensagemTecnica = string.Empty;
				});
			}
			else
			{
				_aguardandoLeituraComanda = false;
			}
		}

		protected override void Processar()
		{
			base.Processar();
			CodigoComanda = string.Empty;
			base.MensagemOrientacao = "ESCANEIE A COMANDA";
			((UpdateViewBindableBase)this).DormirSync(100, (Action)delegate
			{
				FocoNoCodigoComanda = !FocoNoCodigoComanda;
			});
			_aguardandoLeituraComanda = true;
			AguardarLeituraComanda();
			_desativarIgnorarProdutoSubstituto();
		}

		private void AguardarLeituraComanda()
		{
			if (DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.GerarNumeracaoComandaAutoatendimentoAutomaticamente.Valor)
			{
				((UpdateViewBindableBase)this).DormirSync(1000, (Action)delegate
				{
					CodigoComanda = HelperObject.ToStringOrEmpty((object)ObterProximaSequenciaComandaAutoatendimento());
				});
			}
			while (_aguardandoLeituraComanda)
			{
				Thread.Sleep(100);
				FocoNoCodigoComanda = !FocoNoCodigoComanda;
			}
			SalvarComanda();
		}

		private long ObterProximaSequenciaComandaAutoatendimento()
		{
			return DadosSistema.GetService().Canal<IAutoatendimentoBalancaModuleService, long>((Func<IAutoatendimentoBalancaModuleService, long>)((IAutoatendimentoBalancaModuleService service) => service.ObterProximaSequenciaComandaAutoatendimento(DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.SequenciaComandaAutoatendimento.Referencia)));
		}

		private async void SalvarComanda()
		{
			bool flag = !_ativouIgnorarProdutoSubstituto() && HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)Item.ProdutoSubstituto) && Total > Item.ProdutoSubstituto.Preco.PrecoVendaVigente;
			ItemMesaContaDto itemMesaContaDto = (flag ? RetornarItemMesaContaSubstituto() : RetornarItemMesaConta());
			XtraReport relatorio = null;
			if (DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.ImprimirComprovanteComanda.Valor)
			{
				bool num = Item is ItemLancamentoManual;
				decimal quantidade = (num ? QuantidadeItemManual : Peso);
				decimal total = (num ? (Item.Preco * QuantidadeItemManual) : (flag ? Item.ProdutoSubstituto.Preco.PrecoVendaVigente : Total));
				relatorio = (((int)DadosSistema.TamanhoBobina == 1) ? new RelatorioAutoatendimento56Mm57Mm58Mm(Item, total, quantidade, flag, HelperString.ToLong(CodigoComanda)).Processar() : new RelatorioAutoatendimento79Mm80Mm(Item, total, quantidade, flag, HelperString.ToLong(CodigoComanda)).Processar());
			}
			itemMesaContaDto.DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia;
			await _service.Canal<IRestauranteModuleService, Task>((Func<IRestauranteModuleService, Task>)((IRestauranteModuleService x) => x.Salvar((IEnumerable<ItemMesaContaDto>)new List<ItemMesaContaDto> { itemMesaContaDto }, DadosSistema.DeveImprimirAmbienteProducaoPainelPedido)));
			if (DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.ImprimirComprovanteComanda.Valor && HelperObject.IsNotNull((object)relatorio))
			{
				Imprimir(relatorio);
			}
			CodigoComanda = string.Empty;
		}

		private void Imprimir(XtraReport impresso)
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					_impressoraNaoFiscalService.Imprimir((IReport)(object)impresso);
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private ItemMesaContaDto RetornarItemMesaConta()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Expected O, but got Unknown
			return new ItemMesaContaDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)base.Item.Dto).Id,
				NumeroMesaConta = HelperString.ToLong(CodigoComanda),
				Quantidade = ((base.Item is ItemLancamentoManual) ? base.QuantidadeItemManual : base.Peso),
				DataHora = DateTime.Now,
				Descricao = base.Item.Descricao,
				Codigo = base.Item.Dto.ProdutoServico.CodigoInterno,
				Unidade = base.Item.Dto.ProdutoServico.UnidadeMedida.Sigla,
				ValorUnitario = base.Item.Preco,
				Situacao = (SituacaoMesaConta)2
			};
		}

		private ItemMesaContaDto RetornarItemMesaContaSubstituto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			return new ItemMesaContaDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)base.Item.ProdutoSubstituto).Id,
				NumeroMesaConta = HelperString.ToLong(CodigoComanda),
				Quantidade = 1m,
				DataHora = DateTime.Now,
				Descricao = base.Item.ProdutoSubstituto.ProdutoServico.Descricao,
				Codigo = base.Item.ProdutoSubstituto.ProdutoServico.CodigoInterno,
				Unidade = base.Item.ProdutoSubstituto.ProdutoServico.UnidadeMedida.Sigla,
				ValorUnitario = base.Item.ProdutoSubstituto.Preco.PrecoVendaVigente,
				Situacao = (SituacaoMesaConta)2
			};
		}
	}
	public class ImpressaoCapturaPesoViewModel : CapturaPesoViewModel
	{
		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly Func<bool> _ativouIgnorarProdutoSubstituto;

		private readonly Action _desativarIgnorarProdutoSubstituto;

		private bool Substituir
		{
			get
			{
				if (!_ativouIgnorarProdutoSubstituto() && HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)base.Item.ProdutoSubstituto))
				{
					return base.Total > base.Item.ProdutoSubstituto.Preco.PrecoVendaVigente;
				}
				return false;
			}
		}

		public ImpressaoCapturaPesoViewModel(IBalancaService balancaService, IImpressoraNaoFiscalService impressoraNaoFiscalService, AutoatendimentoBalancaDto autoatendimentoBalanca, Func<bool> ativouIgnorarProdutoSubstituto, Action desativarIgnorarProdutoSubstituto)
			: base(balancaService, autoatendimentoBalanca, ativouIgnorarProdutoSubstituto)
		{
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_ativouIgnorarProdutoSubstituto = ativouIgnorarProdutoSubstituto;
			_desativarIgnorarProdutoSubstituto = desativarIgnorarProdutoSubstituto;
		}

		protected override void Processar()
		{
			base.Processar();
			Imprimir();
			ThreadPrincipal.Executar((Action)delegate
			{
				base.MensagemOrientacao = "RETIRE A COMANDA";
			});
			_desativarIgnorarProdutoSubstituto();
			Thread.Sleep(2000);
		}

		private void Imprimir()
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Invalid comparison between Unknown and I4
			try
			{
				bool num = base.Item is ItemLancamentoManual;
				decimal quantidade = (num ? base.QuantidadeItemManual : base.Peso);
				decimal total = (num ? (base.Item.Preco * base.QuantidadeItemManual) : (Substituir ? base.Item.ProdutoSubstituto.Preco.PrecoVendaVigente : base.Total));
				XtraReport relatorio = (((int)DadosSistema.TamanhoBobina == 1) ? new RelatorioAutoatendimento56Mm57Mm58Mm(base.Item, total, quantidade, Substituir, 0L).Processar() : new RelatorioAutoatendimento79Mm80Mm(base.Item, total, quantidade, Substituir, 0L).Processar());
				ThreadPrincipal.Executar((Action)delegate
				{
					_impressoraNaoFiscalService.Imprimir((IReport)(object)relatorio);
				});
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}
	}
	public class ItemBase : BindableBase
	{
		public BitmapSource Foto
		{
			get
			{
				return ((BindableBase)this).GetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Foto));
			}
			set
			{
				((BindableBase)this).SetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Foto), value);
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

		public decimal Preco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Preco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value);
			}
		}

		public ProdutoServicoEmpresaDto Dto { get; }

		public ProdutoServicoEmpresaDto ProdutoSubstituto { get; }

		public ItemBase(AutoatendimentoBalancaItemDto autoatendimentoBalancaItemDto)
		{
			Dto = autoatendimentoBalancaItemDto.Produto;
			byte[] array = HelperImage.Decompress(autoatendimentoBalancaItemDto.Produto.ProdutoServico.Produto.Imagem);
			Foto = ((array != null) ? HelperImage.ToBitmapSource(array) : null);
			Descricao = autoatendimentoBalancaItemDto.Produto.ProdutoServico.Descricao.ToUpper();
			Preco = autoatendimentoBalancaItemDto.Produto.Preco.PrecoVendaVigente;
			ProdutoSubstituto = autoatendimentoBalancaItemDto.ProdutoSubstituto;
		}
	}
	public class ItemCapturaPeso : ItemBase
	{
		public ItemCapturaPeso(AutoatendimentoBalancaItemDto autoatendimentoBalancaItemDto)
			: base(autoatendimentoBalancaItemDto)
		{
		}
	}
	public class ItemLancamentoManual : ItemBase
	{
		public ItemLancamentoManual(AutoatendimentoBalancaItemDto autoatendimentoBalancaItemDto)
			: base(autoatendimentoBalancaItemDto)
		{
		}
	}
	public class ItensLancamentoManualViewModel : BindableBase, IDisposable
	{
		public Lista<ItemLancamentoManual> Lista { get; protected set; }

		public int Width { get; protected set; }

		public int ItemHeight { get; protected set; }

		public int LarguraItem { get; protected set; }

		public double AlturaIconeItem { get; protected set; }

		public double AlturaLinhaItem { get; protected set; }

		public double FonteItem { get; protected set; }

		public ICommand SelecionarCommand { get; set; }

		public ItensLancamentoManualViewModel(AutoatendimentoBalancaDto autoatendimentoBalanca, Action<ItemLancamentoManual> processarItemManual, Func<ItemLancamentoManual, bool> podeProcessarItemManual)
		{
			Lista = new Lista<ItemLancamentoManual>();
			IEnumerable<AutoatendimentoBalancaItemDto> source = autoatendimentoBalanca.Itens.Where((AutoatendimentoBalancaItemDto x) => !x.Produto.ProdutoServico.Produto.Pesavel);
			Lista.Adicionar(source.Select((AutoatendimentoBalancaItemDto x) => new ItemLancamentoManual(x)));
			SelecionarCommand = (ICommand)new DelegateCommand<ItemLancamentoManual>(processarItemManual, podeProcessarItemManual, (bool?)null);
			AjustarTamanhoPainel();
		}

		private void AjustarTamanhoPainel()
		{
			ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
			bool? obj;
			if (configuracaoDto == null)
			{
				obj = null;
			}
			else
			{
				AutoatendimentoRestauranteDto autoatendimentoRestaurante = configuracaoDto.AutoatendimentoRestaurante;
				if (autoatendimentoRestaurante == null)
				{
					obj = null;
				}
				else
				{
					BoleanoDto interfaceAmpliadaAutoatendimento = autoatendimentoRestaurante.InterfaceAmpliadaAutoatendimento;
					obj = ((interfaceAmpliadaAutoatendimento != null) ? new bool?(interfaceAmpliadaAutoatendimento.Valor) : null);
				}
			}
			double num = ((obj ?? false) ? 1.3 : 1.0);
			ItemHeight = (int)(84.0 * num);
			LarguraItem = (int)(102.0 * num);
			AlturaIconeItem = 40.0 * num;
			AlturaLinhaItem = 9.0 * num;
			FonteItem = 10.0 * num;
			int num2 = (int)(Application.Current.MainWindow.ActualHeight - 140.0) / ItemHeight;
			Width = (int)((double)((Lista.Numero > num2) ? 160 : 104) * num);
		}

		public void Dispose()
		{
			((ViewModelValidationBase)Lista).Dispose();
		}
	}
}
namespace Otus.Modulo.AutoatendimentoBalancas.Reports
{
	public class ImpressaoComanda
	{
		public string Linha { get; set; }
	}
	public class RelatorioAutoatendimento56Mm57Mm58Mm : XtraReport, IRelatorio
	{
		private readonly ItemBase _item;

		private readonly decimal _total;

		private readonly decimal _quantidade;

		private readonly bool _substituir;

		private readonly long _comanda;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ReportHeaderBand bandCabecalho;

		private XRLabel lblCnpj;

		private XRLabel lblNomeFantasia;

		private ReportFooterBand ReportFooter;

		private XRPageInfo dataHora;

		private XRLabel xrLabel4;

		private XRBarCode codigoBarras;

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel lblDescricao;

		private XRLabel lblQtd;

		private XRLabel lblTotal;

		private ObjectDataSource objectDataSource1;

		private FormattingRule formattingRule1;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel7;

		private XRPictureBox logotipoEmitente;

		private XRLabel lblEndereco;

		private XRLabel lblTelefone;

		private XRLabel lblTara;

		private XRLabel lblLivre;

		private XRLabel lblMensagemRodape;

		private XRLabel lblComanda;

		private XRBarCode codigoBarrasComanda;

		public int Id { get; set; }

		public string Descricao => "Relatório de autoatendimento";

		public IParametros Parametros { get; set; }

		public bool ExibirNoModuloRelatorios => false;

		public RelatorioAutoatendimento56Mm57Mm58Mm(ItemBase item, decimal total, decimal quantidade, bool substituir, long comanda)
		{
			_item = item;
			_total = total;
			_quantidade = quantidade;
			_substituir = substituir;
			_comanda = comanda;
			InitializeComponent();
		}

		public XtraReport Processar()
		{
			((XRControl)codigoBarras).Visible = false;
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			ExibirLogotipo();
			if (!((XRControl)logotipoEmitente).Visible)
			{
				ExibirDadosEmitente();
			}
			((XRControl)lblDescricao).Text = (_substituir ? _item.ProdutoSubstituto.ProdutoServico.Descricao.ToUpper() : _item.Descricao);
			((XRControl)lblQtd).Text = (_substituir ? "1" : HelperDecimal.FormatarCampoValor(_quantidade, 3, 0, false));
			((XRControl)lblTotal).Text = _total.ToString("c2", new CultureInfo("pt-Br"));
			((XRControl)lblTara).Text = ((!_substituir && !DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.NaoDescontarTaraProduto.Valor) ? string.Format("Peso do recipiente: {0} kg", _item.Dto.ProdutoEmpresa.Tara.ToString("n3", new CultureInfo("pt-Br"))) : "");
			if (_item is ItemCapturaPeso)
			{
				if (_substituir)
				{
					((XRControl)lblLivre).Text = $"LIVRE{Environment.NewLine} Peso: {HelperDecimal.FormatarCampoValor(_quantidade, 3, 0, false)}";
					if (_item.ProdutoSubstituto.ProdutoServico.Produto.CodigoBarras != string.Empty)
					{
						((XRControl)codigoBarras).Text = _item.ProdutoSubstituto.ProdutoServico.Produto.CodigoBarras;
						((XRControl)codigoBarras).Size = new System.Drawing.Size(500, 200);
						((XRControl)codigoBarras).Visible = true;
						((XRControl)lblLivre).Size = new System.Drawing.Size(500, 120);
						((XRControl)lblLivre).Top = 410;
					}
					else
					{
						((XRControl)lblLivre).Size = new System.Drawing.Size(500, 120);
						((XRControl)codigoBarras).Visible = false;
					}
					((XRControl)lblLivre).Visible = true;
				}
				else
				{
					((XRControl)codigoBarras).Text = ConfigurarCodigoBarras();
					((XRControl)codigoBarras).Size = new System.Drawing.Size(500, 200);
					((XRControl)codigoBarras).Visible = true;
					((XRControl)lblLivre).Visible = false;
				}
				ReportHeaderBand obj = bandCabecalho;
				((XRControl)obj).Height = ((XRControl)obj).Height + 150;
			}
			if (_comanda > 0)
			{
				((XRControl)codigoBarrasComanda).Text = _comanda.ToString();
				((XRControl)codigoBarrasComanda).Size = new System.Drawing.Size(500, 180);
				((XRControl)codigoBarrasComanda).Visible = true;
				((XRControl)lblComanda).Text = $"COMANDA: {_comanda}";
				((XRControl)lblComanda).Size = new System.Drawing.Size(500, 60);
				XRLabel obj2 = lblComanda;
				((XRControl)obj2).Top = ((XRControl)obj2).Top + 180;
				((XRControl)lblComanda).Visible = true;
				XRPageInfo obj3 = dataHora;
				((XRControl)obj3).Top = ((XRControl)obj3).Top + 220;
				XRLabel obj4 = lblMensagemRodape;
				((XRControl)obj4).Top = ((XRControl)obj4).Top + 220;
				((XRControl)codigoBarras).Visible = false;
				((XRControl)codigoBarras).Size = new System.Drawing.Size(500, 0);
			}
			else
			{
				((XRControl)codigoBarrasComanda).Visible = false;
				((XRControl)lblComanda).Visible = false;
			}
			List<ImpressaoComanda> dataSource = (from i in DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.ListaProdutosImpressaoComanda.Valor.OfType<string>()
				select new ImpressaoComanda
				{
					Linha = i
				}).ToList();
			((XtraReportBase)this).DataSource = dataSource;
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).ShowPrintStatusDialog = false;
			((XtraReport)this).Margins.Left = DadosSistema.MargemEsquerdaImpressoraNaoFiscal;
			((XtraReport)this).RollPaper = DadosSistema.RollPaper;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			((XRControl)logotipoEmitente).Visible = false;
			if (!HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem) && DadosSistema.EmpresaLogada.Imagem.Length != 0)
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
				if (logotipoEmitente.Image != null)
				{
					((XRControl)logotipoEmitente).Visible = true;
				}
			}
		}

		private void ExibirDadosEmitente()
		{
			((XRControl)lblNomeFantasia).Text = DadosSistema.EmpresaLogada.Fantasia;
			((XRControl)lblCnpj).Text = DadosSistema.EmpresaLogada.CpfCnpjFormatado;
			((XRControl)lblEndereco).Text = DadosSistema.EmpresaLogada.EnderecoToText;
			((XRControl)lblTelefone).Text = DadosSistema.EmpresaLogada.Telefone;
		}

		private string ConfigurarCodigoBarras()
		{
			string text = "2" + HelperString.PreencherComCaractereEsquerda(_item.Dto.ProdutoServico.CodigoInterno, 6, '0');
			switch (DadosSistema.ConfiguracaoDto.Saida.InformacaoCodigoBarras.Valor)
			{
			case 1:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_quantidade, 3, 5, true), 5, '0');
				break;
			case 3:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_total, 3, 5, true), 5, '0');
				break;
			case 2:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_total, 2, 5, true), 5, '0');
				break;
			}
			return text;
		}

		private void lblTara_BeforePrint(object sender, PrintEventArgs e)
		{
			e.Cancel = _item.Dto.ProdutoEmpresa.Tara <= 0m;
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
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
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
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Expected O, but got Unknown
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Expected O, but got Unknown
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Expected O, but got Unknown
			//IL_025b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_0370: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0517: Unknown result type (might be due to invalid IL or missing references)
			//IL_054c: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Unknown result type (might be due to invalid IL or missing references)
			//IL_0705: Unknown result type (might be due to invalid IL or missing references)
			//IL_072e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0855: Unknown result type (might be due to invalid IL or missing references)
			//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0915: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1004: Unknown result type (might be due to invalid IL or missing references)
			//IL_1091: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_11bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_128b: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_137c: Unknown result type (might be due to invalid IL or missing references)
			//IL_13a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_14ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_1503: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_162a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1752: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			EAN13Generator symbology = new EAN13Generator();
			Code128Generator symbology2 = new Code128Generator();
			Detail = new DetailBand();
			xrLabel4 = new XRLabel();
			formattingRule1 = new FormattingRule();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bandCabecalho = new ReportHeaderBand();
			lblLivre = new XRLabel();
			lblTara = new XRLabel();
			lblTelefone = new XRLabel();
			lblEndereco = new XRLabel();
			logotipoEmitente = new XRPictureBox();
			codigoBarras = new XRBarCode();
			xrLabel1 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			lblDescricao = new XRLabel();
			lblQtd = new XRLabel();
			lblTotal = new XRLabel();
			lblCnpj = new XRLabel();
			lblNomeFantasia = new XRLabel();
			ReportFooter = new ReportFooterBand();
			lblComanda = new XRLabel();
			lblMensagemRodape = new XRLabel();
			dataHora = new XRPageInfo();
			GroupFooter1 = new GroupFooterBand();
			xrLabel7 = new XRLabel();
			objectDataSource1 = new ObjectDataSource(components);
			codigoBarrasComanda = new XRBarCode();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel4 });
			((XRControl)Detail).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(formattingRule1);
			((XRControl)Detail).HeightF = 57.81252f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).Borders = (BorderSide)7;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Linha")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Arial", 8f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(0f, 0f);
			xrLabel4.Multiline = true;
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(499.1207f, 57.81252f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).WordWrap = false;
			formattingRule1.Condition = "[DataSource.RowCount] == 0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)1;
			formattingRule1.Name = "formattingRule1";
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 13.95506f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)bandCabecalho).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)lblLivre,
				(XRControl)lblTara,
				(XRControl)lblTelefone,
				(XRControl)lblEndereco,
				(XRControl)logotipoEmitente,
				(XRControl)codigoBarras,
				(XRControl)xrLabel1,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)lblDescricao,
				(XRControl)lblQtd,
				(XRControl)lblTotal,
				(XRControl)lblCnpj,
				(XRControl)lblNomeFantasia
			});
			((XRControl)bandCabecalho).Dpi = 254f;
			((XRControl)bandCabecalho).Font = new Font("Arial", 8f);
			((XRControl)bandCabecalho).HeightF = 348.4245f;
			((XRControl)bandCabecalho).Name = "bandCabecalho";
			((XRControl)bandCabecalho).StylePriority.UseFont = false;
			((XRControl)lblLivre).CanGrow = false;
			((XRControl)lblLivre).Dpi = 254f;
			((XRControl)lblLivre).Font = new Font("Arial", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)lblLivre).LocationFloat = new PointFloat(0.0009285609f, 213f);
			lblLivre.Multiline = true;
			((XRControl)lblLivre).Name = "lblLivre";
			((XRControl)lblLivre).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblLivre).SizeF = new SizeF(500f, 5f);
			((XRControl)lblLivre).StylePriority.UseFont = false;
			((XRControl)lblLivre).StylePriority.UseTextAlignment = false;
			((XRControl)lblLivre).TextAlignment = (TextAlignment)32;
			((XRControl)lblTara).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblTara).Borders = (BorderSide)0;
			((XRControl)lblTara).CanGrow = false;
			((XRControl)lblTara).Dpi = 254f;
			((XRControl)lblTara).Font = new Font("Arial", 8f);
			((XRControl)lblTara).LocationFloat = new PointFloat(0f, 315f);
			lblTara.Multiline = true;
			((XRControl)lblTara).Name = "lblTara";
			((XRControl)lblTara).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblTara).SizeF = new SizeF(499.1208f, 33.42453f);
			((XRControl)lblTara).StylePriority.UseBorders = false;
			((XRControl)lblTara).StylePriority.UseFont = false;
			((XRControl)lblTara).StylePriority.UsePadding = false;
			((XRControl)lblTara).StylePriority.UseTextAlignment = false;
			((XRControl)lblTara).TextAlignment = (TextAlignment)16;
			((XRControl)lblTara).WordWrap = false;
			((XRControl)lblTara).BeforePrint += lblTara_BeforePrint;
			((XRControl)lblTelefone).Dpi = 254f;
			((XRControl)lblTelefone).Font = new Font("Arial", 8f);
			((XRControl)lblTelefone).LocationFloat = new PointFloat(0f, 71.99996f);
			((XRControl)lblTelefone).Name = "lblTelefone";
			((XRControl)lblTelefone).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTelefone).SizeF = new SizeF(500f, 35.99998f);
			((XRControl)lblTelefone).StylePriority.UseFont = false;
			((XRControl)lblTelefone).StylePriority.UseTextAlignment = false;
			((XRControl)lblTelefone).TextAlignment = (TextAlignment)2;
			((XRControl)lblEndereco).Dpi = 254f;
			((XRControl)lblEndereco).Font = new Font("Arial", 8f);
			((XRControl)lblEndereco).LocationFloat = new PointFloat(0f, 107.9999f);
			((XRControl)lblEndereco).Name = "lblEndereco";
			((XRControl)lblEndereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblEndereco).SizeF = new SizeF(500f, 87.00006f);
			((XRControl)lblEndereco).StylePriority.UseFont = false;
			((XRControl)lblEndereco).StylePriority.UseTextAlignment = false;
			((XRControl)lblEndereco).TextAlignment = (TextAlignment)2;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(0.0009285609f, 0f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(499.1199f, 195f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			codigoBarras.Alignment = (TextAlignment)32;
			((XRControl)codigoBarras).Dpi = 254f;
			((XRControl)codigoBarras).Font = new Font("Arial", 8f);
			((XRControl)codigoBarras).LocationFloat = new PointFloat(0.0009255861f, 213f);
			codigoBarras.Module = 4f;
			((XRControl)codigoBarras).Name = "codigoBarras";
			((XRControl)codigoBarras).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)codigoBarras).SizeF = new SizeF(500f, 5.013153f);
			((XRControl)codigoBarras).StylePriority.UseFont = false;
			((XRControl)codigoBarras).StylePriority.UsePadding = false;
			((XRControl)codigoBarras).StylePriority.UseTextAlignment = false;
			codigoBarras.Symbology = (BarCodeGeneratorBase)(object)symbology;
			((XRControl)codigoBarras).Text = "200001001935";
			((XRControl)codigoBarras).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel1).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel1).CanGrow = false;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 234f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(233.6854f, 36.00002f);
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "ITEM";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel2).CanGrow = false;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(233.6854f, 234f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(133.1437f, 36.00002f);
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "QTD";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel3).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel3).CanGrow = false;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(366.8291f, 234f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(132.2917f, 36.00002f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "TOTAL";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)64;
			((XRControl)lblDescricao).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblDescricao).Borders = (BorderSide)0;
			((XRControl)lblDescricao).CanGrow = false;
			((XRControl)lblDescricao).Dpi = 254f;
			((XRControl)lblDescricao).Font = new Font("Arial", 8f);
			((XRControl)lblDescricao).LocationFloat = new PointFloat(0f, 269.9999f);
			lblDescricao.Multiline = true;
			((XRControl)lblDescricao).Name = "lblDescricao";
			((XRControl)lblDescricao).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblDescricao).SizeF = new SizeF(233.6854f, 45.00006f);
			((XRControl)lblDescricao).StylePriority.UseBorders = false;
			((XRControl)lblDescricao).StylePriority.UseFont = false;
			((XRControl)lblDescricao).StylePriority.UsePadding = false;
			((XRControl)lblDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblDescricao).Text = "Buffet KG";
			((XRControl)lblDescricao).TextAlignment = (TextAlignment)16;
			((XRControl)lblDescricao).WordWrap = false;
			((XRControl)lblQtd).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblQtd).Borders = (BorderSide)0;
			((XRControl)lblQtd).CanGrow = false;
			((XRControl)lblQtd).Dpi = 254f;
			((XRControl)lblQtd).Font = new Font("Arial", 8f);
			((XRControl)lblQtd).LocationFloat = new PointFloat(233.6854f, 269.9999f);
			lblQtd.Multiline = true;
			((XRControl)lblQtd).Name = "lblQtd";
			((XRControl)lblQtd).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblQtd).SizeF = new SizeF(133.1437f, 45.00006f);
			((XRControl)lblQtd).StylePriority.UseBorders = false;
			((XRControl)lblQtd).StylePriority.UseFont = false;
			((XRControl)lblQtd).StylePriority.UsePadding = false;
			((XRControl)lblQtd).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtd).Text = "0.369";
			((XRControl)lblQtd).TextAlignment = (TextAlignment)64;
			((XRControl)lblQtd).WordWrap = false;
			((XRControl)lblTotal).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblTotal).Borders = (BorderSide)0;
			((XRControl)lblTotal).CanGrow = false;
			((XRControl)lblTotal).Dpi = 254f;
			((XRControl)lblTotal).Font = new Font("Arial", 8f);
			((XRControl)lblTotal).LocationFloat = new PointFloat(366.8291f, 269.9999f);
			lblTotal.Multiline = true;
			((XRControl)lblTotal).Name = "lblTotal";
			((XRControl)lblTotal).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblTotal).SizeF = new SizeF(132.2917f, 45.00006f);
			((XRControl)lblTotal).StylePriority.UseBorders = false;
			((XRControl)lblTotal).StylePriority.UseFont = false;
			((XRControl)lblTotal).StylePriority.UsePadding = false;
			((XRControl)lblTotal).StylePriority.UseTextAlignment = false;
			((XRControl)lblTotal).Text = "19.35";
			((XRControl)lblTotal).TextAlignment = (TextAlignment)64;
			((XRControl)lblTotal).WordWrap = false;
			((XRControl)lblCnpj).Dpi = 254f;
			((XRControl)lblCnpj).Font = new Font("Arial", 8f);
			((XRControl)lblCnpj).LocationFloat = new PointFloat(0f, 35.99998f);
			((XRControl)lblCnpj).Name = "lblCnpj";
			((XRControl)lblCnpj).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblCnpj).SizeF = new SizeF(500f, 35.99998f);
			((XRControl)lblCnpj).StylePriority.UseFont = false;
			((XRControl)lblCnpj).StylePriority.UseTextAlignment = false;
			((XRControl)lblCnpj).TextAlignment = (TextAlignment)2;
			((XRControl)lblNomeFantasia).Dpi = 254f;
			((XRControl)lblNomeFantasia).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)lblNomeFantasia).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)lblNomeFantasia).Name = "lblNomeFantasia";
			((XRControl)lblNomeFantasia).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblNomeFantasia).SizeF = new SizeF(500f, 35.99998f);
			((XRControl)lblNomeFantasia).StylePriority.UseFont = false;
			((XRControl)lblNomeFantasia).StylePriority.UseTextAlignment = false;
			((XRControl)lblNomeFantasia).TextAlignment = (TextAlignment)2;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)codigoBarrasComanda,
				(XRControl)lblComanda,
				(XRControl)lblMensagemRodape,
				(XRControl)dataHora
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 123.1908f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)lblComanda).CanGrow = false;
			((XRControl)lblComanda).Dpi = 254f;
			((XRControl)lblComanda).Font = new Font("Arial", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)lblComanda).LocationFloat = new PointFloat(0f, 10f);
			lblComanda.Multiline = true;
			((XRControl)lblComanda).Name = "lblComanda";
			((XRControl)lblComanda).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblComanda).SizeF = new SizeF(500.0009f, 5f);
			((XRControl)lblComanda).StylePriority.UseFont = false;
			((XRControl)lblComanda).StylePriority.UseTextAlignment = false;
			((XRControl)lblComanda).TextAlignment = (TextAlignment)32;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)0;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(0.0009285609f, 93.19083f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(500f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)dataHora).Borders = (BorderSide)0;
			((XRControl)dataHora).Dpi = 254f;
			((XRControl)dataHora).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			dataHora.Format = "{0:dd/MM/yyyy HH:mm}";
			((XRControl)dataHora).LocationFloat = new PointFloat(0f, 33f);
			((XRControl)dataHora).Name = "dataHora";
			((XRControl)dataHora).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			dataHora.PageInfo = (PageInfo)16;
			((XRControl)dataHora).SizeF = new SizeF(500f, 45.19083f);
			dataHora.StartPageNumber = 0;
			((XRControl)dataHora).StylePriority.UseBorders = false;
			((XRControl)dataHora).StylePriority.UseFont = false;
			((XRControl)dataHora).StylePriority.UseTextAlignment = false;
			((XRControl)dataHora).TextAlignment = (TextAlignment)32;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel7 });
			((XRControl)GroupFooter1).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)GroupFooter1).FormattingRules).Add(formattingRule1);
			((XRControl)GroupFooter1).HeightF = 79.375f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)xrLabel7).Borders = (BorderSide)15;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(0f, 0f);
			xrLabel7.Multiline = true;
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(499.1207f, 79.375f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "TOTAL";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).WordWrap = false;
			objectDataSource1.DataSource = typeof(ImpressaoComanda);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			codigoBarrasComanda.Alignment = (TextAlignment)32;
			((XRControl)codigoBarrasComanda).Dpi = 254f;
			((XRControl)codigoBarrasComanda).Font = new Font("Arial", 8f);
			((XRControl)codigoBarrasComanda).LocationFloat = new PointFloat(0f, 5f);
			codigoBarrasComanda.Module = 4f;
			((XRControl)codigoBarrasComanda).Name = "codigoBarrasComanda";
			((XRControl)codigoBarrasComanda).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			codigoBarrasComanda.ShowText = false;
			((XRControl)codigoBarrasComanda).SizeF = new SizeF(500f, 5f);
			((XRControl)codigoBarrasComanda).StylePriority.UseFont = false;
			((XRControl)codigoBarrasComanda).StylePriority.UseTextAlignment = false;
			codigoBarrasComanda.Symbology = (BarCodeGeneratorBase)(object)symbology2;
			((XRControl)codigoBarrasComanda).Text = "5000";
			((XRControl)codigoBarrasComanda).TextAlignment = (TextAlignment)32;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)bandCabecalho,
				(Band)ReportFooter,
				(Band)GroupFooter1
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[1] { formattingRule1 });
			((XtraReport)this).Margins = new Margins(35, 0, 0, 14);
			((XRControl)this).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			((XtraReport)this).PageHeight = 2969;
			((XtraReport)this).PageWidth = 570;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class RelatorioAutoatendimento79Mm80Mm : XtraReport, IRelatorio
	{
		private readonly ItemBase _item;

		private readonly decimal _total;

		private readonly decimal _quantidade;

		private readonly bool _substituir;

		private readonly long _comanda;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ReportHeaderBand bandCabecalho;

		private XRLabel lblCnpj;

		private XRLabel lblNomeFantasia;

		private ReportFooterBand ReportFooter;

		private XRPageInfo dataHora;

		private XRLabel xrLabel4;

		private XRBarCode codigoBarras;

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel lblDescricao;

		private XRLabel lblQtd;

		private XRLabel lblTotal;

		private ObjectDataSource objectDataSource1;

		private FormattingRule formattingRule1;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel7;

		private XRPictureBox logotipoEmitente;

		private XRLabel lblEndereco;

		private XRLabel lblTelefone;

		private XRLabel lblTara;

		private XRLabel lblLivre;

		private XRLabel lblMensagemRodape;

		private XRLabel lblComanda;

		private XRBarCode codigoBarrasComanda;

		public int Id { get; set; }

		public string Descricao => "Relatório de autoatendimento";

		public IParametros Parametros { get; set; }

		public bool ExibirNoModuloRelatorios => false;

		public RelatorioAutoatendimento79Mm80Mm(ItemBase item, decimal total, decimal quantidade, bool substituir, long comanda)
		{
			_item = item;
			_total = total;
			_quantidade = quantidade;
			_substituir = substituir;
			_comanda = comanda;
			InitializeComponent();
		}

		public XtraReport Processar()
		{
			((XRControl)codigoBarras).Visible = false;
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			ExibirLogotipo();
			if (!((XRControl)logotipoEmitente).Visible)
			{
				ExibirDadosEmitente();
			}
			((XRControl)lblDescricao).Text = (_substituir ? _item.ProdutoSubstituto.ProdutoServico.Descricao.ToUpper() : _item.Descricao);
			((XRControl)lblQtd).Text = (_substituir ? "1" : HelperDecimal.FormatarCampoValor(_quantidade, 3, 0, false));
			((XRControl)lblTotal).Text = _total.ToString("c2", new CultureInfo("pt-Br"));
			((XRControl)lblTara).Text = ((!_substituir && !DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.NaoDescontarTaraProduto.Valor) ? string.Format("Peso do recipiente: {0} kg", _item.Dto.ProdutoEmpresa.Tara.ToString("n3", new CultureInfo("pt-Br"))) : "");
			if (_item is ItemCapturaPeso)
			{
				if (_substituir)
				{
					((XRControl)lblLivre).Text = $"LIVRE{Environment.NewLine} Peso: {HelperDecimal.FormatarCampoValor(_quantidade, 3, 0, false)}";
					if (_item.ProdutoSubstituto.ProdutoServico.Produto.CodigoBarras != string.Empty)
					{
						((XRControl)codigoBarras).Text = _item.ProdutoSubstituto.ProdutoServico.Produto.CodigoBarras;
						((XRControl)codigoBarras).Size = new System.Drawing.Size(702, 200);
						((XRControl)codigoBarras).Visible = true;
						((XRControl)lblLivre).Size = new System.Drawing.Size(702, 120);
						((XRControl)lblLivre).Top = 410;
					}
					else
					{
						((XRControl)lblLivre).Size = new System.Drawing.Size(702, 120);
						((XRControl)codigoBarras).Visible = false;
					}
					((XRControl)lblLivre).Visible = true;
				}
				else
				{
					((XRControl)codigoBarras).Text = ConfigurarCodigoBarras();
					((XRControl)codigoBarras).Size = new System.Drawing.Size(702, 200);
					((XRControl)codigoBarras).Visible = true;
					((XRControl)lblLivre).Visible = false;
				}
				ReportHeaderBand obj = bandCabecalho;
				((XRControl)obj).Height = ((XRControl)obj).Height + 150;
			}
			if (_comanda > 0)
			{
				((XRControl)codigoBarrasComanda).Text = _comanda.ToString();
				((XRControl)codigoBarrasComanda).Size = new System.Drawing.Size(702, 180);
				((XRControl)codigoBarrasComanda).Visible = true;
				((XRControl)lblComanda).Text = $"COMANDA: {_comanda}";
				((XRControl)lblComanda).Size = new System.Drawing.Size(702, 60);
				XRLabel obj2 = lblComanda;
				((XRControl)obj2).Top = ((XRControl)obj2).Top + 180;
				((XRControl)lblComanda).Visible = true;
				XRPageInfo obj3 = dataHora;
				((XRControl)obj3).Top = ((XRControl)obj3).Top + 220;
				XRLabel obj4 = lblMensagemRodape;
				((XRControl)obj4).Top = ((XRControl)obj4).Top + 220;
				((XRControl)codigoBarras).Visible = false;
				((XRControl)codigoBarras).Size = new System.Drawing.Size(702, 0);
			}
			else
			{
				((XRControl)codigoBarrasComanda).Visible = false;
				((XRControl)lblComanda).Visible = false;
			}
			List<ImpressaoComanda> dataSource = (from i in DadosSistema.ConfiguracaoDto.AutoatendimentoRestaurante.ListaProdutosImpressaoComanda.Valor.OfType<string>()
				select new ImpressaoComanda
				{
					Linha = i
				}).ToList();
			((XtraReportBase)this).DataSource = dataSource;
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).ShowPrintStatusDialog = false;
			((XtraReport)this).Margins.Left = DadosSistema.MargemEsquerdaImpressoraNaoFiscal;
			((XtraReport)this).RollPaper = DadosSistema.RollPaper;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			((XRControl)logotipoEmitente).Visible = false;
			if (!HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem) && DadosSistema.EmpresaLogada.Imagem.Length != 0)
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
				if (logotipoEmitente.Image != null)
				{
					((XRControl)logotipoEmitente).Visible = true;
				}
			}
		}

		private void ExibirDadosEmitente()
		{
			((XRControl)lblNomeFantasia).Text = DadosSistema.EmpresaLogada.Fantasia;
			((XRControl)lblCnpj).Text = DadosSistema.EmpresaLogada.CpfCnpjFormatado;
			((XRControl)lblEndereco).Text = DadosSistema.EmpresaLogada.EnderecoToText;
			((XRControl)lblTelefone).Text = DadosSistema.EmpresaLogada.Telefone;
		}

		private string ConfigurarCodigoBarras()
		{
			string text = "2" + HelperString.PreencherComCaractereEsquerda(_item.Dto.ProdutoServico.CodigoInterno, 6, '0');
			switch (DadosSistema.ConfiguracaoDto.Saida.InformacaoCodigoBarras.Valor)
			{
			case 1:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_quantidade, 3, 5, true), 5, '0');
				break;
			case 3:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_total, 3, 5, true), 5, '0');
				break;
			case 2:
				text += HelperString.PreencherComCaractereEsquerda(HelperDecimal.FormatarCampoValor(_total, 2, 5, true), 5, '0');
				break;
			}
			return text;
		}

		private void lblTara_BeforePrint(object sender, PrintEventArgs e)
		{
			e.Cancel = _item.Dto.ProdutoEmpresa.Tara <= 0m;
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
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
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
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Expected O, but got Unknown
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0216: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Expected O, but got Unknown
			//IL_025b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0290: Unknown result type (might be due to invalid IL or missing references)
			//IL_0370: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0517: Unknown result type (might be due to invalid IL or missing references)
			//IL_054c: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0633: Unknown result type (might be due to invalid IL or missing references)
			//IL_0705: Unknown result type (might be due to invalid IL or missing references)
			//IL_072e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0855: Unknown result type (might be due to invalid IL or missing references)
			//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0917: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ddc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ecb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f01: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1082: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1294: Unknown result type (might be due to invalid IL or missing references)
			//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1391: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_14e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1518: Unknown result type (might be due to invalid IL or missing references)
			//IL_15df: Unknown result type (might be due to invalid IL or missing references)
			//IL_161a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1768: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			EAN13Generator symbology = new EAN13Generator();
			Code128Generator symbology2 = new Code128Generator();
			Detail = new DetailBand();
			xrLabel4 = new XRLabel();
			formattingRule1 = new FormattingRule();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bandCabecalho = new ReportHeaderBand();
			lblLivre = new XRLabel();
			lblTara = new XRLabel();
			lblTelefone = new XRLabel();
			lblEndereco = new XRLabel();
			logotipoEmitente = new XRPictureBox();
			codigoBarras = new XRBarCode();
			xrLabel1 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			lblDescricao = new XRLabel();
			lblQtd = new XRLabel();
			lblTotal = new XRLabel();
			lblCnpj = new XRLabel();
			lblNomeFantasia = new XRLabel();
			ReportFooter = new ReportFooterBand();
			lblComanda = new XRLabel();
			lblMensagemRodape = new XRLabel();
			dataHora = new XRPageInfo();
			GroupFooter1 = new GroupFooterBand();
			xrLabel7 = new XRLabel();
			codigoBarrasComanda = new XRBarCode();
			objectDataSource1 = new ObjectDataSource(components);
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel4 });
			((XRControl)Detail).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)Detail).FormattingRules).Add(formattingRule1);
			((XRControl)Detail).HeightF = 57.81252f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).Borders = (BorderSide)7;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Linha")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Arial", 8f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(0f, 0f);
			xrLabel4.Multiline = true;
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(701.9975f, 57.81252f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).WordWrap = false;
			formattingRule1.Condition = "[DataSource.RowCount] == 0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)1;
			formattingRule1.Name = "formattingRule1";
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 11.70835f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)bandCabecalho).Controls.AddRange((XRControl[])(object)new XRControl[14]
			{
				(XRControl)lblLivre,
				(XRControl)lblTara,
				(XRControl)lblTelefone,
				(XRControl)lblEndereco,
				(XRControl)logotipoEmitente,
				(XRControl)codigoBarras,
				(XRControl)xrLabel1,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)lblDescricao,
				(XRControl)lblQtd,
				(XRControl)lblTotal,
				(XRControl)lblCnpj,
				(XRControl)lblNomeFantasia
			});
			((XRControl)bandCabecalho).Dpi = 254f;
			((XRControl)bandCabecalho).Font = new Font("Arial", 8f);
			((XRControl)bandCabecalho).HeightF = 357.3586f;
			((XRControl)bandCabecalho).Name = "bandCabecalho";
			((XRControl)bandCabecalho).StylePriority.UseFont = false;
			((XRControl)lblLivre).CanGrow = false;
			((XRControl)lblLivre).Dpi = 254f;
			((XRControl)lblLivre).Font = new Font("Arial", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)lblLivre).LocationFloat = new PointFloat(0.0009260131f, 214.2917f);
			lblLivre.Multiline = true;
			((XRControl)lblLivre).Name = "lblLivre";
			((XRControl)lblLivre).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblLivre).SizeF = new SizeF(701.9966f, 5f);
			((XRControl)lblLivre).StylePriority.UseFont = false;
			((XRControl)lblLivre).StylePriority.UseTextAlignment = false;
			((XRControl)lblLivre).TextAlignment = (TextAlignment)32;
			((XRControl)lblTara).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblTara).Borders = (BorderSide)0;
			((XRControl)lblTara).CanGrow = false;
			((XRControl)lblTara).Dpi = 254f;
			((XRControl)lblTara).Font = new Font("Arial", 8f);
			((XRControl)lblTara).LocationFloat = new PointFloat(0.0009209909f, 323.9341f);
			lblTara.Multiline = true;
			((XRControl)lblTara).Name = "lblTara";
			((XRControl)lblTara).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblTara).SizeF = new SizeF(701.9966f, 33.42453f);
			((XRControl)lblTara).StylePriority.UseBorders = false;
			((XRControl)lblTara).StylePriority.UseFont = false;
			((XRControl)lblTara).StylePriority.UsePadding = false;
			((XRControl)lblTara).StylePriority.UseTextAlignment = false;
			((XRControl)lblTara).TextAlignment = (TextAlignment)16;
			((XRControl)lblTara).WordWrap = false;
			((XRControl)lblTara).BeforePrint += lblTara_BeforePrint;
			((XRControl)lblTelefone).Dpi = 254f;
			((XRControl)lblTelefone).Font = new Font("Arial", 8f);
			((XRControl)lblTelefone).LocationFloat = new PointFloat(0f, 71.99996f);
			((XRControl)lblTelefone).Name = "lblTelefone";
			((XRControl)lblTelefone).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTelefone).SizeF = new SizeF(701.9982f, 35.99998f);
			((XRControl)lblTelefone).StylePriority.UseFont = false;
			((XRControl)lblTelefone).StylePriority.UseTextAlignment = false;
			((XRControl)lblTelefone).TextAlignment = (TextAlignment)2;
			((XRControl)lblEndereco).Dpi = 254f;
			((XRControl)lblEndereco).Font = new Font("Arial", 8f);
			((XRControl)lblEndereco).LocationFloat = new PointFloat(0f, 107.9999f);
			((XRControl)lblEndereco).Name = "lblEndereco";
			((XRControl)lblEndereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblEndereco).SizeF = new SizeF(701.9982f, 87.00006f);
			((XRControl)lblEndereco).StylePriority.UseFont = false;
			((XRControl)lblEndereco).StylePriority.UseTextAlignment = false;
			((XRControl)lblEndereco).TextAlignment = (TextAlignment)2;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(50f, 0f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(612.6826f, 195f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			codigoBarras.Alignment = (TextAlignment)32;
			((XRControl)codigoBarras).Dpi = 254f;
			((XRControl)codigoBarras).Font = new Font("Arial", 8f);
			((XRControl)codigoBarras).LocationFloat = new PointFloat(0.0009260131f, 214f);
			codigoBarras.Module = 4f;
			((XRControl)codigoBarras).Name = "codigoBarras";
			((XRControl)codigoBarras).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			((XRControl)codigoBarras).SizeF = new SizeF(701.9973f, 5.291656f);
			((XRControl)codigoBarras).StylePriority.UseFont = false;
			((XRControl)codigoBarras).StylePriority.UseTextAlignment = false;
			codigoBarras.Symbology = (BarCodeGeneratorBase)(object)symbology;
			((XRControl)codigoBarras).Text = "200001001935";
			((XRControl)codigoBarras).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel1).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel1).CanGrow = false;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 242.934f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(297.1854f, 35.99999f);
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "ITEM";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel2).CanGrow = false;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(297.1854f, 242.934f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(201.9354f, 35.99999f);
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "QTD";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel3).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel3).CanGrow = false;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(499.1208f, 242.934f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(202.8774f, 35.99999f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "TOTAL";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)64;
			((XRControl)lblDescricao).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblDescricao).Borders = (BorderSide)0;
			((XRControl)lblDescricao).CanGrow = false;
			((XRControl)lblDescricao).Dpi = 254f;
			((XRControl)lblDescricao).Font = new Font("Arial", 8f);
			((XRControl)lblDescricao).LocationFloat = new PointFloat(0f, 278.934f);
			lblDescricao.Multiline = true;
			((XRControl)lblDescricao).Name = "lblDescricao";
			((XRControl)lblDescricao).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblDescricao).SizeF = new SizeF(297.1854f, 45.00005f);
			((XRControl)lblDescricao).StylePriority.UseBorders = false;
			((XRControl)lblDescricao).StylePriority.UseFont = false;
			((XRControl)lblDescricao).StylePriority.UsePadding = false;
			((XRControl)lblDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblDescricao).Text = "Buffet KG";
			((XRControl)lblDescricao).TextAlignment = (TextAlignment)16;
			((XRControl)lblDescricao).WordWrap = false;
			((XRControl)lblQtd).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblQtd).Borders = (BorderSide)0;
			((XRControl)lblQtd).CanGrow = false;
			((XRControl)lblQtd).Dpi = 254f;
			((XRControl)lblQtd).Font = new Font("Arial", 10f);
			((XRControl)lblQtd).LocationFloat = new PointFloat(297.1854f, 278.934f);
			lblQtd.Multiline = true;
			((XRControl)lblQtd).Name = "lblQtd";
			((XRControl)lblQtd).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblQtd).SizeF = new SizeF(201.9354f, 45.00005f);
			((XRControl)lblQtd).StylePriority.UseBorders = false;
			((XRControl)lblQtd).StylePriority.UseFont = false;
			((XRControl)lblQtd).StylePriority.UsePadding = false;
			((XRControl)lblQtd).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtd).Text = "0.369";
			((XRControl)lblQtd).TextAlignment = (TextAlignment)64;
			((XRControl)lblQtd).WordWrap = false;
			((XRControl)lblTotal).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)lblTotal).Borders = (BorderSide)0;
			((XRControl)lblTotal).CanGrow = false;
			((XRControl)lblTotal).Dpi = 254f;
			((XRControl)lblTotal).Font = new Font("Arial", 10f);
			((XRControl)lblTotal).LocationFloat = new PointFloat(499.1208f, 278.934f);
			lblTotal.Multiline = true;
			((XRControl)lblTotal).Name = "lblTotal";
			((XRControl)lblTotal).Padding = new PaddingInfo(5, 5, 0, 10, 254f);
			((XRControl)lblTotal).SizeF = new SizeF(202.8767f, 45.00005f);
			((XRControl)lblTotal).StylePriority.UseBorders = false;
			((XRControl)lblTotal).StylePriority.UseFont = false;
			((XRControl)lblTotal).StylePriority.UsePadding = false;
			((XRControl)lblTotal).StylePriority.UseTextAlignment = false;
			((XRControl)lblTotal).Text = "19.35";
			((XRControl)lblTotal).TextAlignment = (TextAlignment)64;
			((XRControl)lblTotal).WordWrap = false;
			((XRControl)lblCnpj).Dpi = 254f;
			((XRControl)lblCnpj).Font = new Font("Arial", 8f);
			((XRControl)lblCnpj).LocationFloat = new PointFloat(0f, 35.99998f);
			((XRControl)lblCnpj).Name = "lblCnpj";
			((XRControl)lblCnpj).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblCnpj).SizeF = new SizeF(701.9982f, 35.99998f);
			((XRControl)lblCnpj).StylePriority.UseFont = false;
			((XRControl)lblCnpj).StylePriority.UseTextAlignment = false;
			((XRControl)lblCnpj).TextAlignment = (TextAlignment)2;
			((XRControl)lblNomeFantasia).Dpi = 254f;
			((XRControl)lblNomeFantasia).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)lblNomeFantasia).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)lblNomeFantasia).Name = "lblNomeFantasia";
			((XRControl)lblNomeFantasia).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblNomeFantasia).SizeF = new SizeF(701.9982f, 35.99998f);
			((XRControl)lblNomeFantasia).StylePriority.UseFont = false;
			((XRControl)lblNomeFantasia).StylePriority.UseTextAlignment = false;
			((XRControl)lblNomeFantasia).TextAlignment = (TextAlignment)2;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)codigoBarrasComanda,
				(XRControl)lblComanda,
				(XRControl)lblMensagemRodape,
				(XRControl)dataHora
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 117.1908f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)lblComanda).CanGrow = false;
			((XRControl)lblComanda).Dpi = 254f;
			((XRControl)lblComanda).Font = new Font("Arial", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)lblComanda).LocationFloat = new PointFloat(0f, 11f);
			lblComanda.Multiline = true;
			((XRControl)lblComanda).Name = "lblComanda";
			((XRControl)lblComanda).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblComanda).SizeF = new SizeF(701.9966f, 5f);
			((XRControl)lblComanda).StylePriority.UseFont = false;
			((XRControl)lblComanda).StylePriority.UseTextAlignment = false;
			((XRControl)lblComanda).TextAlignment = (TextAlignment)32;
			((XRControl)lblMensagemRodape).AnchorHorizontal = (HorizontalAnchorStyles)3;
			((XRControl)lblMensagemRodape).Borders = (BorderSide)0;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Arial", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(0f, 87.19083f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(702f, 30f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)dataHora).AnchorHorizontal = (HorizontalAnchorStyles)3;
			((XRControl)dataHora).Borders = (BorderSide)0;
			((XRControl)dataHora).Dpi = 254f;
			((XRControl)dataHora).Font = new Font("Arial", 8f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			dataHora.Format = "{0:dd/MM/yyyy HH:mm}";
			((XRControl)dataHora).LocationFloat = new PointFloat(0f, 30f);
			((XRControl)dataHora).Name = "dataHora";
			((XRControl)dataHora).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			dataHora.PageInfo = (PageInfo)16;
			((XRControl)dataHora).SizeF = new SizeF(701.9976f, 45.19083f);
			dataHora.StartPageNumber = 0;
			((XRControl)dataHora).StylePriority.UseBorders = false;
			((XRControl)dataHora).StylePriority.UseFont = false;
			((XRControl)dataHora).StylePriority.UseTextAlignment = false;
			((XRControl)dataHora).TextAlignment = (TextAlignment)32;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel7 });
			((XRControl)GroupFooter1).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)GroupFooter1).FormattingRules).Add(formattingRule1);
			((XRControl)GroupFooter1).HeightF = 79.375f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)xrLabel7).Borders = (BorderSide)15;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Arial", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(0.0001614888f, 0f);
			xrLabel7.Multiline = true;
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(701.9973f, 79.375f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "TOTAL";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel7).WordWrap = false;
			codigoBarrasComanda.Alignment = (TextAlignment)32;
			((XRControl)codigoBarrasComanda).Dpi = 254f;
			((XRControl)codigoBarrasComanda).Font = new Font("Arial", 8f);
			((XRControl)codigoBarrasComanda).LocationFloat = new PointFloat(0f, 6f);
			codigoBarrasComanda.Module = 4f;
			((XRControl)codigoBarrasComanda).Name = "codigoBarrasComanda";
			((XRControl)codigoBarrasComanda).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			codigoBarrasComanda.ShowText = false;
			((XRControl)codigoBarrasComanda).SizeF = new SizeF(702f, 5f);
			((XRControl)codigoBarrasComanda).StylePriority.UseFont = false;
			((XRControl)codigoBarrasComanda).StylePriority.UseTextAlignment = false;
			codigoBarrasComanda.Symbology = (BarCodeGeneratorBase)(object)symbology2;
			((XRControl)codigoBarrasComanda).Text = "5000";
			((XRControl)codigoBarrasComanda).TextAlignment = (TextAlignment)32;
			objectDataSource1.DataSource = typeof(ImpressaoComanda);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)bandCabecalho,
				(Band)ReportFooter,
				(Band)GroupFooter1
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[1] { formattingRule1 });
			((XtraReport)this).Margins = new Margins(50, 46, 0, 12);
			((XRControl)this).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			((XtraReport)this).PageHeight = 2969;
			((XtraReport)this).PageWidth = 800;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.AutoatendimentoBalancas.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.AutoatendimentoBalancas.Properties.Resources", typeof(Resources).Assembly);
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

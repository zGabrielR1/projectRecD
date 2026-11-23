using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto.Cadastros.ConsultasProduto;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Actors.Interfaces;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.Mensagens.Report;
using Otus.Modulo.Mensagens.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Mensagens")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Mensagens")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Mensagens
{
	[Module(ModuleName = "MensagemModulo", OnDemand = true)]
	[ModuleDependency("PedidoEcommerceModulo")]
	[ModuleDependency("MovimentacaoModulo")]
	[ModuleDependency("AgendamentoModulo")]
	[ModuleDependency("GestaoSincronizacaoScanntechModulo")]
	[ModuleDependency("InutilizacaoModulo")]
	public class MensagemModulo : IModule
	{
		private readonly IUnityContainer _container;

		public MensagemModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<MensagensView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<NotificacaoListaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Mensagens.Views
{
	public class NotificacaoListaView : UserControl, IComponentConnector
	{
		internal ListViewFocusable LstResultadosPesquisa;

		private bool _contentLoaded;

		public NotificacaoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Mensagens;component/views/notificacaolistaview.xaml", UriKind.Relative);
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
				LstResultadosPesquisa = (ListViewFocusable)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class MensagensView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MensagensView()
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
				Uri resourceLocator = new Uri("/Modulo.Mensagens;component/views/mensagensview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Mensagens.ViewModels
{
	public class NotificacaoListaViewModel : NavigationBase
	{
		private const int TamanhoPagina = 100;

		private readonly IMensagemClientActorSystem _mensagemClientActorSystem;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly List<NotificacaoMensagem> _notificacoes;

		private int _quantidadeExibida = 100;

		public Lista<NotificacaoMensagem> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<NotificacaoMensagem>>((Expression<Func<Lista<NotificacaoMensagem>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<NotificacaoMensagem>>((Expression<Func<Lista<NotificacaoMensagem>>>)(() => Lista), value);
			}
		}

		public string Resumo
		{
			get
			{
				if (_notificacoes.Count <= Lista.Numero)
				{
					return string.Empty;
				}
				return $"Exibindo {Lista.Numero} de {_notificacoes.Count} notificações";
			}
		}

		public ICommand AtenderCommand { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public ICommand ExcluirTodasCommand { get; set; }

		public ICommand<NotificacaoMensagem> NavegarViewCommand { get; set; }

		public ICommand<NotificacaoMensagem> GerarRelatorioCommand { get; set; }

		public NotificacaoListaViewModel(IMensagemClientActorSystem mensagemClientActorSystem, IEventPublisher eventPublisher, IImpressaoRelatorioService impressaoRelatorioService)
			: base("Notificações")
		{
			//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Expected O, but got Unknown
			_mensagemClientActorSystem = mensagemClientActorSystem;
			_impressaoRelatorioService = impressaoRelatorioService;
			Lista = new Lista<NotificacaoMensagem>();
			List<NotificacaoMensagem> source = mensagemClientActorSystem.Mensagens?.Values.ToList() ?? new List<NotificacaoMensagem>();
			IEnumerable<NotificacaoMensagem> second = ((DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor == 1) ? source.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 4 && x.IdUsuario == DadosSistema.IdUsuario) : source.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 4 && x.IdEmpresa == DadosSistema.EmpresaLogada.Id));
			_notificacoes = (from x in source.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao != 4).Concat(second)
				orderby x.DataHora descending
				select x).ToList();
			CompletarPagina();
			Lista.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<MensagemResponse>(eventPublisher.GetEvent<MensagemResponse>(), (Action<MensagemResponse>)delegate(MensagemResponse response)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					//IL_000b: Unknown result type (might be due to invalid IL or missing references)
					//IL_0095: Unknown result type (might be due to invalid IL or missing references)
					//IL_009b: Invalid comparison between Unknown and I4
					//IL_0122: Unknown result type (might be due to invalid IL or missing references)
					//IL_0128: Invalid comparison between Unknown and I4
					//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
					//IL_01b8: Invalid comparison between Unknown and I4
					//IL_0232: Unknown result type (might be due to invalid IL or missing references)
					//IL_0239: Invalid comparison between Unknown and I4
					if ((int)response.Mensagem.TipoMensagemNotificacao == 0)
					{
						List<NotificacaoMensagem> list = HelperIEnumerable.ToListEmptyWhenNull<NotificacaoMensagem>(_notificacoes.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 0));
						if (list.Any())
						{
							foreach (NotificacaoMensagem item in list)
							{
								_mensagemClientActorSystem.Atender(item);
							}
						}
					}
					if ((int)response.Mensagem.TipoMensagemNotificacao == 6)
					{
						List<NotificacaoMensagem> list2 = HelperIEnumerable.ToListEmptyWhenNull<NotificacaoMensagem>(_notificacoes.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 6));
						if (list2.Any())
						{
							foreach (NotificacaoMensagem item2 in list2)
							{
								_mensagemClientActorSystem.Atender(item2);
							}
						}
					}
					if ((int)response.Mensagem.TipoMensagemNotificacao == 8)
					{
						List<NotificacaoMensagem> list3 = HelperIEnumerable.ToListEmptyWhenNull<NotificacaoMensagem>(_notificacoes.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 8));
						if (list3.Any())
						{
							foreach (NotificacaoMensagem item3 in list3)
							{
								_mensagemClientActorSystem.Atender(item3);
							}
						}
					}
					if ((int)response.Mensagem.TipoMensagemNotificacao != 4 || ((DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor != 2 || !(response.Mensagem.IdEmpresa != DadosSistema.EmpresaLogada.Id)) && (DadosSistema.ConfiguracaoDto.Geral.TipoAgendamento.Valor != 1 || !(response.Mensagem.IdUsuario != DadosSistema.IdUsuario))))
					{
						if ((int)response.Mensagem.TipoMensagemNotificacao == 10)
						{
							List<NotificacaoMensagem> list4 = HelperIEnumerable.ToListEmptyWhenNull<NotificacaoMensagem>(_notificacoes.Where((NotificacaoMensagem x) => (int)x.TipoMensagemNotificacao == 10 && x.Identificador == response.Mensagem.Identificador));
							if (list4.Any())
							{
								foreach (NotificacaoMensagem item4 in list4)
								{
									_mensagemClientActorSystem.Atender(item4);
								}
							}
						}
						_notificacoes.Insert(0, response.Mensagem);
						Lista.Inserir(0, response.Mensagem);
						Lista.PosicionarNoPrimeiro();
						((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Resumo));
					}
				});
			});
			ObservableExtensions.Subscribe<NotificacaoAtendidaResponse>(eventPublisher.GetEvent<NotificacaoAtendidaResponse>(), (Action<NotificacaoAtendidaResponse>)delegate(NotificacaoAtendidaResponse response)
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					_notificacoes.RemoveAll((NotificacaoMensagem x) => x.Id.Equals(response.IdMensagem));
					NotificacaoMensagem val = Lista.Itens.FirstOrDefault((NotificacaoMensagem x) => x.Id.Equals(response.IdMensagem));
					Lista.Remover(val);
					CompletarPagina();
				});
			});
			AtenderCommand = (ICommand)new DelegateCommand<NotificacaoMensagem>((Action<NotificacaoMensagem>)Atender, (Func<NotificacaoMensagem, bool>)PodeAtender, (bool?)null);
			NavegarViewCommand = (ICommand<NotificacaoMensagem>)(object)new DelegateCommand<NotificacaoMensagem>((Action<NotificacaoMensagem>)NavegarView, (Func<NotificacaoMensagem, bool>)PodeNavegarView, (bool?)null);
			GerarRelatorioCommand = (ICommand<NotificacaoMensagem>)(object)new DelegateCommand<NotificacaoMensagem>((Action<NotificacaoMensagem>)GerarRelatorio, (Func<NotificacaoMensagem, bool>)PodeGerarRelatorio, (bool?)null);
			ExcluirTodasCommand = (ICommand)new DelegateCommand((Action)ExcluirTodas);
			ScrollChangedCommand = (ICommand)new DelegateCommand<object>((Action<object>)CarregarMais);
		}

		private void CompletarPagina()
		{
			int num = Math.Min(_quantidadeExibida, _notificacoes.Count) - Lista.Numero;
			if (num > 0)
			{
				Lista.AdicionarSemPosicionar(_notificacoes.Skip(Lista.Numero).Take(num));
			}
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Resumo));
		}

		private void CarregarMais(object obj)
		{
			if (obj is ScrollChangedEventArgs { VerticalChange: >0.0 } scrollChangedEventArgs && !(Math.Abs(scrollChangedEventArgs.VerticalOffset + scrollChangedEventArgs.ViewportHeight - scrollChangedEventArgs.ExtentHeight) > 1.0) && Lista.Numero < _notificacoes.Count)
			{
				_quantidadeExibida = Lista.Numero + 100;
				CompletarPagina();
			}
		}

		private bool PodeAtender(NotificacaoMensagem arg)
		{
			return arg != null;
		}

		private void Atender(NotificacaoMensagem notificacao)
		{
			_mensagemClientActorSystem.Atender(notificacao);
		}

		private bool PodeNavegarView(NotificacaoMensagem arg)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Invalid comparison between Unknown and I4
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Invalid comparison between Unknown and I4
			TipoMensagemNotificacao tipoMensagemNotificacao = arg.TipoMensagemNotificacao;
			if ((int)tipoMensagemNotificacao == 11 || (int)tipoMensagemNotificacao == 12)
			{
				return true;
			}
			if (HelperObject.IsNotNull((object)arg))
			{
				return !string.IsNullOrEmpty(arg.NavegarView);
			}
			return false;
		}

		private void NavegarView(NotificacaoMensagem obj)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected I4, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			TipoMensagemNotificacao tipoMensagemNotificacao = obj.TipoMensagemNotificacao;
			switch (tipoMensagemNotificacao - 10)
			{
			case 0:
				_mensagemClientActorSystem.Atender(obj);
				break;
			case 1:
			case 2:
				if (!HelperString.IsNullOrEmpty(obj.Mensagem))
				{
					Process.Start("explorer.exe", obj.Mensagem);
				}
				return;
			default:
				if (DialogoFactory.MostrarDialogoSimNao("Remover a notificação?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					_mensagemClientActorSystem.Atender(obj);
				}
				break;
			}
			string navegarView = obj.NavegarView;
			Action<NavigationResult> obj2 = delegate
			{
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("Identificador", (object)obj.Identificador);
			((NavigationBase)this).Navegar(navegarView, obj2, val);
		}

		private bool PodeGerarRelatorio(NotificacaoMensagem arg)
		{
			if (HelperObject.IsNotNull((object)arg))
			{
				return arg.GerarRelatorio;
			}
			return false;
		}

		private void GerarRelatorio(NotificacaoMensagem obj)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Invalid comparison between Unknown and I4
			if (DialogoFactory.MostrarDialogoSimNao("Remover a notificação?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				_mensagemClientActorSystem.Atender(obj);
			}
			if ((int)obj.TipoMensagemNotificacao == 8)
			{
				IReport val = RelatorioConsultaProdutoIMendes.Criar().ComEmpresa(DadosSistema.EmpresaLogada).ComTitulo("Produtos alterados no dia")
					.ComSubTitulo(HelperDate.FormatarData(obj.DataHora, (FormatoData)6))
					.ComData(obj.DataHora)
					.Build();
				_impressaoRelatorioService.PreviewRelatorio(val);
			}
		}

		private void ExcluirTodas()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Tem certeza que deseja remover todas as notificações?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			foreach (NotificacaoMensagem notificaco in _notificacoes)
			{
				_mensagemClientActorSystem.Atender(notificaco);
			}
			_notificacoes.Clear();
			_quantidadeExibida = 100;
			Lista.Limpar();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Resumo));
		}
	}
	public class MensagensViewModel : ModuloBase
	{
		public MensagensViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloMensagemRegion, typeof(NotificacaoListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.Mensagens.Report
{
	public class RelatorioConsultaProdutoIMendes : XtraReport
	{
		private DateTime _data;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private PageHeaderBand PageHeader;

		private PageFooterBand PageFooter;

		private XRPageInfo xrPageInfo1;

		private XRLabel xrLabel1;

		private XRLabel DataEmissao;

		private FormattingRule formattingRule1;

		private XRLabel Titulo;

		private XRLabel CNPJ;

		private XRLabel Empresa;

		private XRLabel SubTitulo;

		private XRLabel Fantasia;

		private XRPictureBox logotipoEmitente;

		private XRLabel xrLabel14;

		private XRLabel xrLabel60;

		private XRLabel lblTituloDescricao;

		private ObjectDataSource objectDataSource1;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		public RelatorioConsultaProdutoIMendes()
		{
			InitializeComponent();
		}

		public static RelatorioConsultaProdutoIMendes Criar()
		{
			return new RelatorioConsultaProdutoIMendes();
		}

		public RelatorioConsultaProdutoIMendes ComEmpresa(Pessoa pessoa)
		{
			((XRControl)Empresa).Text = pessoa.Nome;
			((XRControl)Fantasia).Text = pessoa.Fantasia;
			((XRControl)CNPJ).Text = pessoa.CpfCnpjFormatado;
			if (!HelperObject.IsNotNull((object)pessoa.Imagem) || pessoa.Imagem.Length == 0)
			{
				return this;
			}
			logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(pessoa.Imagem));
			if (!HelperObject.IsNotNull((object)logotipoEmitente.Image))
			{
				return this;
			}
			((XRControl)Empresa).TextAlignment = (TextAlignment)64;
			((XRControl)CNPJ).TextAlignment = (TextAlignment)64;
			((XRControl)Fantasia).TextAlignment = (TextAlignment)64;
			return this;
		}

		public RelatorioConsultaProdutoIMendes ComTitulo(string titulo)
		{
			((XRControl)Titulo).Text = titulo;
			if (!string.IsNullOrEmpty(titulo))
			{
				((XtraReport)this).DisplayName = titulo;
			}
			return this;
		}

		public RelatorioConsultaProdutoIMendes ComSubTitulo(string subtitulo)
		{
			((XRControl)SubTitulo).Text = subtitulo;
			return this;
		}

		public RelatorioConsultaProdutoIMendes ComData(DateTime data)
		{
			_data = data;
			return this;
		}

		public IReport Build()
		{
			try
			{
				IEnumerable<ConsultaProdutoIMendesLogDto> enumerable = from x in DadosSistema.GetService().Canal<IIMendesModuleService, Task<IEnumerable<ConsultaProdutoIMendesLogDto>>>((Func<IIMendesModuleService, Task<IEnumerable<ConsultaProdutoIMendesLogDto>>>)((IIMendesModuleService x) => x.ObterConsultaProdutoLog(_data.Date, HelperDate.ToFinalDia(_data), (string)null, "Todas"))).Result
					orderby x.DataInclusao descending
					group x by x.CodigoInterno into x
					select x.FirstOrDefault();
				((XtraReportBase)this).DataSource = HelperIEnumerable.ToListEmptyWhenNull<ConsultaProdutoIMendesLogDto>(enumerable);
				((XtraReport)this).ShowPrintMarginsWarning = false;
				((XRControl)DataEmissao).Text = $"Gerado em {DateTime.Now:d' de 'MMMM' de 'yyyy}";
				((XtraReport)this).PrintingSystem.Document.AutoFitToPagesWidth = 1;
				((XtraReport)this).CreateDocument(false);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
			return (IReport)(object)this;
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
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f3: Expected O, but got Unknown
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_0241: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a6: Expected O, but got Unknown
			//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0363: Unknown result type (might be due to invalid IL or missing references)
			//IL_0369: Expected O, but got Unknown
			//IL_038e: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0426: Unknown result type (might be due to invalid IL or missing references)
			//IL_042c: Expected O, but got Unknown
			//IL_0451: Unknown result type (might be due to invalid IL or missing references)
			//IL_047a: Unknown result type (might be due to invalid IL or missing references)
			//IL_050a: Unknown result type (might be due to invalid IL or missing references)
			//IL_055f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0697: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0785: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0856: Unknown result type (might be due to invalid IL or missing references)
			//IL_087f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0927: Unknown result type (might be due to invalid IL or missing references)
			//IL_0950: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d72: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e54: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel6 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			PageHeader = new PageHeaderBand();
			xrLabel5 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel60 = new XRLabel();
			lblTituloDescricao = new XRLabel();
			logotipoEmitente = new XRPictureBox();
			Fantasia = new XRLabel();
			SubTitulo = new XRLabel();
			Titulo = new XRLabel();
			CNPJ = new XRLabel();
			Empresa = new XRLabel();
			PageFooter = new PageFooterBand();
			xrPageInfo1 = new XRPageInfo();
			xrLabel1 = new XRLabel();
			DataEmissao = new XRLabel();
			formattingRule1 = new FormattingRule();
			objectDataSource1 = new ObjectDataSource(components);
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel6,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 58.42f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseTextAlignment = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataInclusao", "{0:HH:mm:ss}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1582.501f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(317.4995f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(597.9584f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(984.5424f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CodigoBarras")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(254.0001f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(343.9583f, 58.42f);
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "xrLabel3";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CodigoInterno")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(254.0001f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "xrLabel2";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 50f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 120f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[10]
			{
				(XRControl)xrLabel5,
				(XRControl)xrLabel14,
				(XRControl)xrLabel60,
				(XRControl)lblTituloDescricao,
				(XRControl)logotipoEmitente,
				(XRControl)Fantasia,
				(XRControl)SubTitulo,
				(XRControl)Titulo,
				(XRControl)CNPJ,
				(XRControl)Empresa
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).Font = new Font("Segoe UI Light", 10f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)PageHeader).HeightF = 424.0133f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)PageHeader).StylePriority.UseFont = false;
			((XRControl)xrLabel5).Borders = (BorderSide)2;
			((XRControl)xrLabel5).CanGrow = false;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1582.501f, 366.5133f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(317.4988f, 57.50003f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Hora consulta";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel14).Borders = (BorderSide)2;
			((XRControl)xrLabel14).CanGrow = false;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(0.0001614888f, 366.5133f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(253.9999f, 57.50003f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).Text = "Código";
			((XRControl)xrLabel60).Borders = (BorderSide)2;
			((XRControl)xrLabel60).CanGrow = false;
			((XRControl)xrLabel60).Dpi = 254f;
			((XRControl)xrLabel60).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel60).LocationFloat = new PointFloat(254.0001f, 366.5133f);
			((XRControl)xrLabel60).Name = "xrLabel60";
			((XRControl)xrLabel60).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel60).SizeF = new SizeF(343.9583f, 57.50003f);
			((XRControl)xrLabel60).StylePriority.UseBorders = false;
			((XRControl)xrLabel60).StylePriority.UseFont = false;
			((XRControl)xrLabel60).Text = "Barras";
			((XRControl)lblTituloDescricao).Borders = (BorderSide)2;
			((XRControl)lblTituloDescricao).CanGrow = false;
			((XRControl)lblTituloDescricao).Dpi = 254f;
			((XRControl)lblTituloDescricao).Font = new Font("Segoe UI", 11f);
			((XRControl)lblTituloDescricao).LocationFloat = new PointFloat(597.9583f, 366.5133f);
			((XRControl)lblTituloDescricao).Name = "lblTituloDescricao";
			((XRControl)lblTituloDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTituloDescricao).SizeF = new SizeF(984.5424f, 57.50003f);
			((XRControl)lblTituloDescricao).StylePriority.UseBorders = false;
			((XRControl)lblTituloDescricao).StylePriority.UseFont = false;
			((XRControl)lblTituloDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblTituloDescricao).Text = "Descrição";
			((XRControl)lblTituloDescricao).TextAlignment = (TextAlignment)1;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(394.5209f, 165.2183f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)Fantasia).Dpi = 254f;
			((XRControl)Fantasia).Font = new Font("Segoe UI", 10f);
			((XRControl)Fantasia).LocationFloat = new PointFloat(0.0001614888f, 94.83998f);
			((XRControl)Fantasia).Name = "Fantasia";
			((XRControl)Fantasia).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Fantasia).SizeF = new SizeF(1900f, 47.41998f);
			((XRControl)Fantasia).StylePriority.UseFont = false;
			((XRControl)Fantasia).Text = "Fantasia";
			((XRControl)Fantasia).WordWrap = false;
			((XRControl)SubTitulo).Dpi = 254f;
			((XRControl)SubTitulo).Font = new Font("Segoe UI", 14f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)SubTitulo).LocationFloat = new PointFloat(0.0001615286f, 278.2183f);
			((XRControl)SubTitulo).Name = "SubTitulo";
			((XRControl)SubTitulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)SubTitulo).SizeF = new SizeF(1900f, 58.42001f);
			((XRControl)SubTitulo).StylePriority.UseFont = false;
			((XRControl)SubTitulo).StylePriority.UseTextAlignment = false;
			((XRControl)SubTitulo).Text = "SubTitulo";
			((XRControl)SubTitulo).TextAlignment = (TextAlignment)2;
			((XRControl)Titulo).Dpi = 254f;
			((XRControl)Titulo).Font = new Font("Segoe UI", 19f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Titulo).LocationFloat = new PointFloat(0.0001615286f, 178.2184f);
			((XRControl)Titulo).Name = "Titulo";
			((XRControl)Titulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Titulo).SizeF = new SizeF(1900f, 99.99998f);
			((XRControl)Titulo).StylePriority.UseFont = false;
			((XRControl)Titulo).StylePriority.UseTextAlignment = false;
			((XRControl)Titulo).Text = "Titulo";
			((XRControl)Titulo).TextAlignment = (TextAlignment)2;
			((XRControl)CNPJ).Dpi = 254f;
			((XRControl)CNPJ).Font = new Font("Segoe UI", 10f);
			((XRControl)CNPJ).LocationFloat = new PointFloat(0.0001614888f, 47.41999f);
			((XRControl)CNPJ).Name = "CNPJ";
			((XRControl)CNPJ).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)CNPJ).SizeF = new SizeF(1900f, 47.42f);
			((XRControl)CNPJ).StylePriority.UseFont = false;
			((XRControl)CNPJ).Text = "CNPJ";
			((XRControl)Empresa).Dpi = 254f;
			((XRControl)Empresa).Font = new Font("Segoe UI", 10f);
			((XRControl)Empresa).LocationFloat = new PointFloat(0.0001614888f, 0f);
			((XRControl)Empresa).Name = "Empresa";
			((XRControl)Empresa).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Empresa).SizeF = new SizeF(1900f, 47.42f);
			((XRControl)Empresa).StylePriority.UseFont = false;
			((XRControl)Empresa).Text = "Empresa";
			((XRControl)Empresa).WordWrap = false;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrPageInfo1,
				(XRControl)xrLabel1,
				(XRControl)DataEmissao
			});
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 116.84f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)xrPageInfo1).Dpi = 254f;
			xrPageInfo1.Format = " Página {0} de {1}";
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(1520f, 58.41996f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrPageInfo1).SizeF = new SizeF(380f, 58.42f);
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(380.0004f, 58.41996f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1140f, 58.42f);
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Desenvolvido por DigiSat Tecnologia LTDA®  www.digisat.com.br";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)DataEmissao).Dpi = 254f;
			((XRControl)DataEmissao).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)DataEmissao).Name = "DataEmissao";
			((XRControl)DataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)DataEmissao).SizeF = new SizeF(1900f, 58.42f);
			((XRControl)DataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)DataEmissao).Text = "DataEmissao";
			((XRControl)DataEmissao).TextAlignment = (TextAlignment)32;
			formattingRule1.Name = "formattingRule1";
			objectDataSource1.DataSource = typeof(ConsultaProdutoIMendesLogDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageHeader,
				(Band)PageFooter
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[1] { formattingRule1 });
			((XtraReport)this).Margins = new Margins(95, 103, 50, 120);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.Mensagens.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Mensagens.Properties.Resources", typeof(Resources).Assembly);
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

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
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Core.Projections.MovimentacoesProjection.Ecommerces;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Application.Main.Dto.Movimentacoes.Ecommerce;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.DFe.NotaFiscalEletronica.Classes.NotaFiscal;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Danfe.DanfePaisagem;
using DigisatServer.Infrastructure.Crosscutting.Report.Danfe.DanfeRetrato;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.Ecommerce;
using DigisatServer.Infrastructure.Crosscutting.Service.Ecommerce.Classes;
using DigisatServer.Infrastructure.Crosscutting.Service.Ecommerce.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Modulo.PedidosEcommerce.Reports;
using Otus.Modulo.PedidosEcommerce.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.PedidosEcommerce")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.PedidosEcommerce")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.PedidosEcommerce
{
	[Module(ModuleName = "PedidoEcommerceModulo", OnDemand = true)]
	[ModuleDependency("MovimentacaoModulo")]
	public class PedidoEcommerceModulo : IModule
	{
		private readonly IUnityContainer _container;

		public PedidoEcommerceModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<PedidosEcommerceView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PedidoEcommerceListaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.PedidosEcommerce.Views
{
	public class PedidoEcommerceListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal ListViewFocusable lstResultadosPesquisa;

		private bool _contentLoaded;

		public PedidoEcommerceListaView()
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
				Uri resourceLocator = new Uri("/Modulo.PedidosEcommerce;component/views/pedidoecommercelistaview.xaml", UriKind.Relative);
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
				lstResultadosPesquisa = (ListViewFocusable)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class PedidosEcommerceView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PedidosEcommerceView()
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
				Uri resourceLocator = new Uri("/Modulo.PedidosEcommerce;component/views/pedidosecommerceview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.PedidosEcommerce.ViewModels
{
	public class PedidoEcommerceAbaViewModel : AbaPrincipalViewModelBase
	{
		public PedidoEcommerceAbaViewModel(IViewModelPrincipal viewModel)
			: base("Pedido e-commerce", "Pedido recebido do site", viewModel)
		{
		}
	}
	public class PedidoEcommerceFormulario : FormularioEntity<PedidoEcommerce>
	{
		public PedidoEcommerceFormulario(PedidoEcommerce dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new PedidoEcommerceAbaViewModel((IViewModelPrincipal)(object)new PedidoEcommerceViewModel(base.Dto)) });
		}
	}
	public class PedidoEcommerceListaViewModel : ListaSemPaginacao<IEnumerable<PedidoEcommerce>, PedidoEcommerce, PedidoEcommerceViewModel>
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IEventPublisher _eventPublisher;

		private readonly IApiEcommerce _apiEcommerce;

		public bool ImprimirVisivel => true;

		public ICommand AtualizarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand ExecutarAcaoCommand { get; set; }

		public ICommand CancelarCommand { get; set; }

		public ICommand GerarDavCommand { get; set; }

		public ICommand AdicionarCodigoRastreamentoCommand { get; set; }

		public ICommand<PedidoEcommerceViewModel> AnexarNfeCommand { get; set; }

		public PedidoEcommerceListaViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService, IEventPublisher eventPublisher)
			: base((OpcaoDados<IEnumerable<PedidoEcommerce>>)new OpcaoPedidoEcommerce("Todos"))
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Expected O, but got Unknown
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Expected O, but got Unknown
			//IL_017a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0184: Expected O, but got Unknown
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Expected O, but got Unknown
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			_eventPublisher = eventPublisher;
			AtualizarCommand = (ICommand)new DelegateCommand((Action)Atualizar);
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir);
			ExecutarAcaoCommand = (ICommand)new AsyncCommand<PedidoEcommerceViewModel>((Func<PedidoEcommerceViewModel, Task>)ExecutarAcao, (Func<PedidoEcommerceViewModel, bool>)PodeExecutarAcao, (bool?)null);
			CancelarCommand = (ICommand)new AsyncCommand<PedidoEcommerceViewModel>((Func<PedidoEcommerceViewModel, Task>)Cancelar, (Func<PedidoEcommerceViewModel, bool>)PodeCancelar, (bool?)null);
			GerarDavCommand = (ICommand)new AsyncCommand<PedidoEcommerceViewModel>((Func<PedidoEcommerceViewModel, Task>)GerarDav, (Func<PedidoEcommerceViewModel, bool>)PodeGerarDav, (bool?)null);
			AdicionarCodigoRastreamentoCommand = (ICommand)new AsyncCommand((Func<Task>)AdicionarCodigoRastreamento, (Func<bool>)PodeAdicionarCodigoRastreamento, false, (bool?)null);
			AnexarNfeCommand = (ICommand<PedidoEcommerceViewModel>)(object)new DelegateCommand<PedidoEcommerceViewModel>((Action<PedidoEcommerceViewModel>)AnexarNfe, (Func<PedidoEcommerceViewModel, bool>)PodeAnexarNfe, (bool?)null);
			try
			{
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Aguardando"));
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Processado"));
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Confirmado"));
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Enviado"));
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Entregue"));
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)new OpcaoPedidoEcommerce("Cancelado"));
				EcommerceDto ecommerce = DadosSistema.ConfiguracaoDto.Ecommerce;
				_apiEcommerce = (IApiEcommerce)new ApiEcommerce(ecommerce.Url.Valor, ecommerce.Token.Valor, DadosSistema.ObterProxy());
				foreach (OpcaoPedidoEcommerce item in ((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.Opcoes.OfType<OpcaoPedidoEcommerce>())
				{
					item.SetarEcommerceService(_apiEcommerce);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAnexarNfe(PedidoEcommerceViewModel arg)
		{
			if (HelperObject.IsNotNull((object)arg))
			{
				return ((ViewModelDtoBase<PedidoEcommerce>)arg).Dto.status == "Confirmado";
			}
			return false;
		}

		private void AnexarNfe(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			_service.Canal<IPedidoEcommerceModuleService, Task<string>>((Func<IPedidoEcommerceModuleService, Task<string>>)((IPedidoEcommerceModuleService service) => service.ObterXmlNFe(((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.numero))).ContinueWith(delegate(Task<string> task)
			{
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					ThreadPrincipal.Executar((Action)async delegate
					{
						try
						{
							string text = task.Result;
							if (string.IsNullOrEmpty(text))
							{
								text = CarregarArquivoXmlNfe();
							}
							if (!string.IsNullOrEmpty(text))
							{
								byte[] pdfContent = ObterPdfContent(text);
								await _apiEcommerce.Post((ObjectJsonOrder)(object)CriarObjetoUpdateNfe(pedidoEcommerceViewModel, text, pdfContent));
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
					});
				}
			});
		}

		private static string CarregarArquivoXmlNfe()
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = "C:\\";
			openFileDialog.Filter = "Arquivo procNFe (*.xml)|*.xml";
			openFileDialog.RestoreDirectory = true;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return string.Empty;
			}
			using StreamReader streamReader = new StreamReader(openFileDialog.OpenFile());
			return streamReader.ReadToEnd();
		}

		private byte[] ObterPdfContent(string xmlContent)
		{
			using MemoryStream memoryStream = new MemoryStream();
			ObterImpressoDocumentoFiscalEletronico(xmlContent).ExportToPdf((Stream)memoryStream);
			return memoryStream.ToArray();
		}

		private static PedidoUpdateNfe CriarObjetoUpdateNfe(PedidoEcommerceViewModel obj, string xmlContent, byte[] pdfContent)
		{
			return PedidoUpdateNfe.Criar().Comid_pedido(((ObjectJsonPut<PedidoEcommercePut>)(object)((ViewModelDtoBase<PedidoEcommerce>)obj).Dto).id).Compdf_nfe(Convert.ToBase64String(pdfContent))
				.Comxml_nfe(Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlContent)));
		}

		private XtraReport ObterImpressoDocumentoFiscalEletronico(string xmlContent)
		{
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				NFeProc val = HelperObject.To<NFeProc>(HelperXml.StringXmlToObject(xmlContent, typeof(NFeProc)));
				IRelatorio obj;
				if (val.NFe.InfNFe.Ide.tpImp != 1)
				{
					IRelatorio val2 = (IRelatorio)(object)new RelatorioDanfePaisagem().ComNfeProc(val).ComCpfCnpj(DadosSistema.EmpresaLogada.CpfCnpj).ComImagem(DadosSistema.EmpresaLogada.Imagem)
						.ComImagemMarcaAgua(DadosSistema.EmpresaLogada.ImagemMarcaAgua)
						.ComTransparenciaMarcaAgua(DadosSistema.EmpresaLogada.TransparenciaMarcaAgua)
						.ComMargemEsquerdaImpressoraNaoFiscalDanfeNfe(DadosSistema.MargemEsquerdaImpressoraNaoFiscalDanfeNfe)
						.ComMargemSuperiorImpressoraNaoFiscalDanfeNfe(DadosSistema.MargemSuperiorImpressoraNaoFiscalDanfeNfe)
						.ComImprimirItemResumidoDanfeNfe(DadosSistema.ImprimirItemResumidoDanfeNfe)
						.ComCasasDecimaisQuantidade(DadosSistema.ConfiguracaoDto.Geral.CasasDecimaisQuantidade.Valor)
						.ComImprimirCodigoBarrasItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoBarrasItemDanfeNfe.Valor)
						.ComImprimirFcpItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirFcpItemDanfeNfe.Valor)
						.ComImprimirStItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirStItemDanfeNfe.Valor)
						.ComImprimirUnidadeQuantidadeTributariaItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirUnidadeQuantidadeTributariaItemDanfeNfe.Valor)
						.ComImprimirNumeroEItemPedidoCompraItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirNumeroEItemPedidoCompraItemDanfeNfe.Valor)
						.ComSuprimirTotaisReformaTributariaDanfeNfe(DadosSistema.SuprimirTotaisReformaTributariaDanfeNfe);
					obj = val2;
				}
				else
				{
					IRelatorio val2 = (IRelatorio)(object)new RelatorioDanfeRetrato().ComNfeProc(val).ComCpfCnpj(DadosSistema.EmpresaLogada.CpfCnpj).ComImagem(DadosSistema.EmpresaLogada.Imagem)
						.ComImagemMarcaAgua(DadosSistema.EmpresaLogada.ImagemMarcaAgua)
						.ComTransparenciaMarcaAgua(DadosSistema.EmpresaLogada.TransparenciaMarcaAgua)
						.ComMargemEsquerdaImpressoraNaoFiscalDanfeNfe(DadosSistema.MargemEsquerdaImpressoraNaoFiscalDanfeNfe)
						.ComMargemSuperiorImpressoraNaoFiscalDanfeNfe(DadosSistema.MargemSuperiorImpressoraNaoFiscalDanfeNfe)
						.ComExibirAssinaturaDataRecebimentoTodasPaginase(DadosSistema.ConfiguracaoDto.Saida.ExibirAssinaturaDataRecebimentoTodasPaginas.Valor)
						.ComImprimirItemResumidoDanfeNfe(DadosSistema.ImprimirItemResumidoDanfeNfe)
						.ComCasasDecimaisQuantidade(DadosSistema.ConfiguracaoDto.Geral.CasasDecimaisQuantidade.Valor)
						.ComImprimirCodigoBarrasItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirCodigoBarrasItemDanfeNfe.Valor)
						.ComImprimirFcpItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirFcpItemDanfeNfe.Valor)
						.ComImprimirStItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirStItemDanfeNfe.Valor)
						.ComImprimirUnidadeQuantidadeTributariaItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirUnidadeQuantidadeTributariaItemDanfeNfe.Valor)
						.ComImprimirNumeroEItemPedidoCompraItemDanfeNfe(DadosSistema.ConfiguracaoDto.Saida.ImprimirNumeroEItemPedidoCompraItemDanfeNfe.Valor)
						.ComSuprimirTotaisReformaTributariaDanfeNfe(DadosSistema.SuprimirTotaisReformaTributariaDanfeNfe);
					obj = val2;
				}
				return obj.Processar();
			}
			catch (Exception)
			{
				throw new Exception("O arquivo selecionado não é um procNFe válido.");
			}
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			string text = navigationParameters["Identificador"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.Contexto.Informacao = text;
				((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
			}
			base.OnParametroInformado(sender, navigationParameters);
		}

		private bool PodeAdicionarCodigoRastreamento()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens) && ((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return string.IsNullOrEmpty(((ViewModelDtoBase<PedidoEcommerce>)((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Selecionado).Dto.codigo_rastreamento);
			}
			return false;
		}

		private async Task AdicionarCodigoRastreamento()
		{
			string codigo_rastreamento = "";
			if (DialogoFactory.MostrarDialogoEntradaString(ref codigo_rastreamento, "Código de rastreamento", "Informe o código de rastreamento do pedido", (IIcone)null, false, false, false, (string)null, false, false) != MessageBoxResult.OK)
			{
				return;
			}
			PedidoEcommerce dto = ((ViewModelDtoBase<PedidoEcommerce>)((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Selecionado).Dto;
			dto.codigo_rastreamento = codigo_rastreamento;
			await _apiEcommerce.Put<PedidoEcommercePut>((ObjectJsonPut<PedidoEcommercePut>)(object)dto).ContinueWith(delegate(Task<PedidoEcommercePut> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Atualizar(dto);
					});
				}
			});
		}

		private async void Imprimir()
		{
			_ = 1;
			try
			{
				XtraReport obj = await new ImpressoPedidoEcommerce(await _service.Canal<IPedidoEcommerceModuleService, Task<PedidoEcommerceDto>>((Func<IPedidoEcommerceModuleService, Task<PedidoEcommerceDto>>)((IPedidoEcommerceModuleService service) => service.ObterDto(((ViewModelDtoBase<PedidoEcommerce>)((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Selecionado).Dto)))).ObterRelatorio();
				IViewModelItemImprimirRelatorio val = (IViewModelItemImprimirRelatorio)(object)((obj is IViewModelItemImprimirRelatorio) ? obj : null);
				_impressaoRelatorioService.ImprimirRelatorio(val);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void Atualizar()
		{
			((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
		}

		private async Task ExecutarAcao(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			pedidoEcommerceViewModel.SetarStatusAtualizacao(pedidoEcommerceViewModel.ProximoStatus);
			await ExecutarPut(pedidoEcommerceViewModel.Acao.ToLower(), pedidoEcommerceViewModel);
			_eventPublisher.Publish<NotificacaoAtendidaRequest>(new NotificacaoAtendidaRequest(((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.numero, DadosSistema.NomeUsuario));
		}

		private bool PodeExecutarAcao(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			if (HelperObject.IsNotNull((object)pedidoEcommerceViewModel))
			{
				return !string.IsNullOrEmpty(pedidoEcommerceViewModel.ProximoStatus);
			}
			return false;
		}

		private async Task Cancelar(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			pedidoEcommerceViewModel.SetarStatusAtualizacao("Cancelado");
			await ExecutarPut("cancelar", pedidoEcommerceViewModel);
		}

		private bool PodeCancelar(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			if (HelperObject.IsNotNull((object)pedidoEcommerceViewModel))
			{
				return ((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.status != "Cancelado";
			}
			return false;
		}

		private async Task ExecutarPut(string acao, PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			if (DialogoFactory.MostrarDialogoSimNao($"Deseja {acao} o pedido '{((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.numero}'?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			await _apiEcommerce.Put<PedidoEcommercePut>((ObjectJsonPut<PedidoEcommercePut>)(object)((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto).ContinueWith(delegate(Task<PedidoEcommercePut> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Atualizar(pedidoEcommerceViewModel.AtualizarStatus(task.Result));
						GerarDavPedidoConfirmado();
					});
				}
			});
		}

		private async void GerarDavPedidoConfirmado()
		{
			if (PodeGerarDav(((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Selecionado))
			{
				await GerarDav(((ListaBase<PedidoEcommerce, PedidoEcommerceViewModel>)(object)this).Itens.Selecionado);
			}
		}

		private async Task GerarDav(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			if (DialogoFactory.MostrarDialogoSimNao($"Deseja gerar o DAV do pedido '{((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.numero}'?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IPedidoEcommerceModuleService, Task<RetornoMovimentacaoDto>>((Func<IPedidoEcommerceModuleService, Task<RetornoMovimentacaoDto>>)((IPedidoEcommerceModuleService service) => service.CriarMovimentacaoDto(PedidoEcommerceProjection.ProjectedAsDto(((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto)))).ContinueWith(delegate(Task<RetornoMovimentacaoDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_0059: Unknown result type (might be due to invalid IL or missing references)
						//IL_005e: Unknown result type (might be due to invalid IL or missing references)
						//IL_007e: Expected O, but got Unknown
						if (!string.IsNullOrEmpty(task.Result.Mensagem))
						{
							DialogoFactory.MostrarDialogoConfirma(task.Result.Mensagem, false, System.Windows.HorizontalAlignment.Center);
						}
						PedidoEcommerceListaViewModel pedidoEcommerceListaViewModel = this;
						Action<NavigationResult> obj = delegate
						{
						};
						NavigationParameters val = new NavigationParameters();
						val.Add("PesquisarDav", (object)task.Result.MovimentacaoDto);
						((NavigationBase)pedidoEcommerceListaViewModel).Navegar("MovimentacaoSaidaListaView", obj, val);
					});
				}
			});
		}

		private bool PodeGerarDav(PedidoEcommerceViewModel pedidoEcommerceViewModel)
		{
			if (HelperObject.IsNotNull((object)pedidoEcommerceViewModel) && ((ViewModelDtoBase<PedidoEcommerce>)pedidoEcommerceViewModel).Dto.status == "Confirmado" && DadosSistema.VerificarPermissao<MovimentacaoSaida, ManterDav>())
			{
				if (!DadosSistema.PodeExecutarFuncao(new Type[3]
				{
					typeof(Gerencial),
					typeof(Administrador),
					typeof(AdministradorIndustria)
				}))
				{
					return DadosSistema.ContemModuloAdicional(121);
				}
				return true;
			}
			return false;
		}
	}
	public class PedidoEcommerceViewModel : ViewModelDtoBase<PedidoEcommerce>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public string CorStatus
		{
			get
			{
				string result = "#000000";
				switch (base.Dto.status)
				{
				case "Iniciado":
					result = "#ffa000";
					break;
				case "Aguardando":
					result = "#a04400";
					break;
				case "Processado":
					result = "#8B33A2";
					break;
				case "Confirmado":
					result = "#0038a8";
					break;
				case "Enviado":
					result = "#174e7a";
					break;
				case "Entregue":
					result = "#236026";
					break;
				case "Cancelado":
					result = "#f44336";
					break;
				}
				return result;
			}
		}

		public string Acao { get; set; }

		public string ProximoStatus { get; set; }

		public string Documento { get; set; }

		public PedidoEcommerceViewModel()
		{
		}

		public PedidoEcommerceViewModel(PedidoEcommerce dto)
			: base(dto)
		{
		}

		private string Proximo()
		{
			if (base.Dto.status == "Aguardando")
			{
				return "Processado";
			}
			if (base.Dto.status == "Processado")
			{
				return "Confirmado";
			}
			if (base.Dto.status == "Confirmado")
			{
				return "Enviado";
			}
			if (base.Dto.status == "Enviado")
			{
				return "Entregue";
			}
			return string.Empty;
		}

		private string ObterAcaoStatus(string status)
		{
			return status switch
			{
				"Processado" => "Processar", 
				"Confirmado" => "Confirmar", 
				"Enviado" => "Enviar", 
				"Entregue" => "Entregar", 
				"Cancelado" => "Cancelar", 
				_ => string.Empty, 
			};
		}

		public override void ProjectedAsViewModel()
		{
			ProximoStatus = Proximo();
			Acao = ObterAcaoStatus(ProximoStatus);
			Documento = (string.IsNullOrEmpty(base.Dto.usuario.cnpj) ? base.Dto.usuario.cpf : base.Dto.usuario.cnpj);
		}

		public string ToTituloLista()
		{
			return "Pedidos";
		}

		public string ToTituloFicha()
		{
			return "Pedido";
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new PedidoEcommerceFormulario(base.Dto);
		}

		public void SetarStatusAtualizacao(string status)
		{
			base.Dto.SetarStatusAtualizacao(status);
		}

		public PedidoEcommerce AtualizarStatus(PedidoEcommercePut pedidoEcommercePut)
		{
			base.Dto.status = pedidoEcommercePut.status;
			return base.Dto;
		}
	}
	public class PedidosEcommerceViewModel : ModuloBase
	{
		public PedidosEcommerceViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloPedidoEcommerceRegion, typeof(PedidoEcommerceListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.PedidosEcommerce.Reports
{
	public class ImpressoPedidoEcommerce : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly PedidoEcommerceDto _dto;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ReportHeaderBand ReportHeader;

		private XRLabel tituloComplemento;

		private XRLabel referencia;

		private XRLabel complemento;

		private XRLabel cep;

		private XRLabel uf;

		private XRLabel municipio;

		private XRLabel bairro;

		private XRLabel tituloUf;

		private XRLabel tituloCep;

		private XRLabel tituloMunicipio;

		private XRLabel tituloBairro;

		private XRLabel numero;

		private XRLabel tiluloNumero;

		private XRLabel endereco;

		private XRLabel tituloEndereco;

		private XRLabel retira;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel xrLabel1;

		private BindingSource bindingSource1;

		private CalculatedField documentoCliente;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel37;

		private XRLabel xrLabel36;

		private XRLabel xrLabel35;

		private XRLabel xrLabel34;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel33;

		private XRLabel xrLabel32;

		private XRLabel xrLabel31;

		private XRLabel xrLabel30;

		private XRLabel xrLabel29;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel43;

		private XRLabel xrLabel42;

		private XRLabel xrLabel41;

		private XRLabel xrLabel40;

		private XRLabel xrLabel39;

		private XRLabel xrLabel38;

		private XRLabel xrLabel13;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel22;

		private SubBand pagamento;

		private XRPanel xrPanel1;

		private XRLabel xrLabel19;

		private XRLabel xrLabel18;

		private SubBand observacao;

		private XRPanel xrPanel2;

		private XRLabel xrLabel20;

		private XRLabel xrLabel21;

		private SubBand enderecoEntrega;

		private SubBand retiraLocal;

		private XRLabel xrLabel25;

		private XRLabel xrLabel23;

		private CalculatedField contato;

		private XRLabel xrLabel26;

		private XRLabel xrLabel5;

		private XRLabel xrLabel9;

		private XRLabel xrLabel28;

		private FormattingRule troco;

		private XRLabel xrLabel6;

		private XRLabel lblDesconto;

		private XRLabel lblDescricaoDesconto;

		private CalculatedField cupomDesconto;

		private XRLabel xrLabel24;

		private XRLabel xrLabel44;

		private XRLabel xrLabel27;

		public string TituloRelatorio => "Pedido e-commerce";

		public string SubTituloRelatorio => "";

		public bool Retrato => true;

		public ImpressoPedidoEcommerce(PedidoEcommerceDto dto)
		{
			_dto = dto;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XRControl)retira).Visible = _dto.Retirar;
				((XRControl)tituloBairro).Visible = !_dto.Retirar;
				((XRControl)bairro).Visible = !_dto.Retirar;
				((XRControl)tituloCep).Visible = !_dto.Retirar;
				((XRControl)cep).Visible = !_dto.Retirar;
				((XRControl)tituloComplemento).Visible = !_dto.Retirar;
				((XRControl)complemento).Visible = !_dto.Retirar;
				((XRControl)tituloEndereco).Visible = !_dto.Retirar;
				((XRControl)endereco).Visible = !_dto.Retirar;
				((XRControl)tituloMunicipio).Visible = !_dto.Retirar;
				((XRControl)municipio).Visible = !_dto.Retirar;
				((XRControl)tituloUf).Visible = !_dto.Retirar;
				((XRControl)uf).Visible = !_dto.Retirar;
				((XRControl)referencia).Visible = !_dto.Retirar;
				((XRControl)tiluloNumero).Visible = !_dto.Retirar;
				((XRControl)numero).Visible = !_dto.Retirar;
				((XRControl)pagamento).Visible = !string.IsNullOrEmpty(_dto.FormaPagamento);
				((XRControl)observacao).Visible = !string.IsNullOrEmpty(_dto.Observacao);
				((XRControl)enderecoEntrega).Visible = !_dto.Retirar;
				((XRControl)retiraLocal).Visible = _dto.Retirar;
				if (_dto.Desconto > 0m)
				{
					((XRControl)lblDesconto).Text = $" - {_dto.Desconto:n2}";
					((XRControl)lblDescricaoDesconto).Text = (string.IsNullOrEmpty(_dto.CupomDesconto) ? "Desconto" : $"Cupom de desconto '{_dto.CupomDesconto}'");
					ReportFooterBand reportFooter = ReportFooter;
					((XRControl)reportFooter).Height = ((XRControl)reportFooter).Height + 30;
				}
				else
				{
					((XRControl)lblDesconto).Text = string.Empty;
					((XRControl)lblDescricaoDesconto).Text = string.Empty;
				}
				((XtraReportBase)this).DataSource = new List<PedidoEcommerceDto> { _dto };
				return this;
			}));
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
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
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
			//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0462: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_062e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0657: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ea: Expected O, but got Unknown
			//IL_070f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0738: Unknown result type (might be due to invalid IL or missing references)
			//IL_079c: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a2: Expected O, but got Unknown
			//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_087b: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0925: Unknown result type (might be due to invalid IL or missing references)
			//IL_092b: Expected O, but got Unknown
			//IL_096a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0993: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb6: Expected O, but got Unknown
			//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c85: Expected O, but got Unknown
			//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d54: Expected O, but got Unknown
			//IL_0d79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e06: Expected O, but got Unknown
			//IL_0e2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e54: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0eeb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_104e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1077: Unknown result type (might be due to invalid IL or missing references)
			//IL_1125: Unknown result type (might be due to invalid IL or missing references)
			//IL_114e: Unknown result type (might be due to invalid IL or missing references)
			//IL_12dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_12e2: Expected O, but got Unknown
			//IL_1307: Unknown result type (might be due to invalid IL or missing references)
			//IL_1330: Unknown result type (might be due to invalid IL or missing references)
			//IL_1390: Unknown result type (might be due to invalid IL or missing references)
			//IL_1396: Expected O, but got Unknown
			//IL_13bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1442: Unknown result type (might be due to invalid IL or missing references)
			//IL_146b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1508: Unknown result type (might be due to invalid IL or missing references)
			//IL_1531: Unknown result type (might be due to invalid IL or missing references)
			//IL_15bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c3: Expected O, but got Unknown
			//IL_15e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1611: Unknown result type (might be due to invalid IL or missing references)
			//IL_169d: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1763: Unknown result type (might be due to invalid IL or missing references)
			//IL_178c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1818: Unknown result type (might be due to invalid IL or missing references)
			//IL_181e: Expected O, but got Unknown
			//IL_1843: Unknown result type (might be due to invalid IL or missing references)
			//IL_186c: Unknown result type (might be due to invalid IL or missing references)
			//IL_18f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1921: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_19b4: Expected O, but got Unknown
			//IL_19d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a02: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a84: Expected O, but got Unknown
			//IL_1aa9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ad2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c14: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c1a: Expected O, but got Unknown
			//IL_1c3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c68: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cf4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1daa: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db0: Expected O, but got Unknown
			//IL_1dd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dfe: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e80: Expected O, but got Unknown
			//IL_1ea5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ece: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f83: Unknown result type (might be due to invalid IL or missing references)
			//IL_2010: Unknown result type (might be due to invalid IL or missing references)
			//IL_2016: Expected O, but got Unknown
			//IL_203b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2064: Unknown result type (might be due to invalid IL or missing references)
			//IL_20f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2119: Unknown result type (might be due to invalid IL or missing references)
			//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_2213: Unknown result type (might be due to invalid IL or missing references)
			//IL_23a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_23a7: Expected O, but got Unknown
			//IL_23e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_240f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2498: Unknown result type (might be due to invalid IL or missing references)
			//IL_249e: Expected O, but got Unknown
			//IL_24dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2506: Unknown result type (might be due to invalid IL or missing references)
			//IL_258f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2595: Expected O, but got Unknown
			//IL_25d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_25fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2686: Unknown result type (might be due to invalid IL or missing references)
			//IL_268c: Expected O, but got Unknown
			//IL_26cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_26f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2778: Unknown result type (might be due to invalid IL or missing references)
			//IL_277e: Expected O, but got Unknown
			//IL_27bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_27e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_293e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2967: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a17: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a40: Unknown result type (might be due to invalid IL or missing references)
			//IL_2aef: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b18: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bf0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d77: Unknown result type (might be due to invalid IL or missing references)
			//IL_2da0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f47: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f4d: Expected O, but got Unknown
			//IL_2f72: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_303a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3063: Unknown result type (might be due to invalid IL or missing references)
			//IL_30f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3120: Unknown result type (might be due to invalid IL or missing references)
			//IL_31ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_31d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3291: Unknown result type (might be due to invalid IL or missing references)
			//IL_3297: Expected O, but got Unknown
			//IL_32d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_32ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_33c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_33c8: Expected O, but got Unknown
			//IL_33ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_3416: Unknown result type (might be due to invalid IL or missing references)
			//IL_34b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_34b9: Expected O, but got Unknown
			//IL_34de: Unknown result type (might be due to invalid IL or missing references)
			//IL_3507: Unknown result type (might be due to invalid IL or missing references)
			//IL_35dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_3605: Unknown result type (might be due to invalid IL or missing references)
			//IL_36b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_36de: Unknown result type (might be due to invalid IL or missing references)
			//IL_378e: Unknown result type (might be due to invalid IL or missing references)
			//IL_37b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_38e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3982: Unknown result type (might be due to invalid IL or missing references)
			//IL_39ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a61: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a67: Expected O, but got Unknown
			//IL_3aa2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3acb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b56: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bff: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c05: Expected O, but got Unknown
			//IL_3c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c53: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3db7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3de0: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e60: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e66: Expected O, but got Unknown
			//IL_3e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3eb4: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			ReportHeader = new ReportHeaderBand();
			xrLabel5 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel1 = new XRLabel();
			enderecoEntrega = new SubBand();
			xrLabel24 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel23 = new XRLabel();
			referencia = new XRLabel();
			tituloComplemento = new XRLabel();
			tituloBairro = new XRLabel();
			bairro = new XRLabel();
			tituloMunicipio = new XRLabel();
			municipio = new XRLabel();
			complemento = new XRLabel();
			tituloUf = new XRLabel();
			uf = new XRLabel();
			tituloCep = new XRLabel();
			cep = new XRLabel();
			numero = new XRLabel();
			tiluloNumero = new XRLabel();
			endereco = new XRLabel();
			tituloEndereco = new XRLabel();
			retiraLocal = new SubBand();
			retira = new XRLabel();
			documentoCliente = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel13 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel36 = new XRLabel();
			xrLabel35 = new XRLabel();
			xrLabel34 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel22 = new XRLabel();
			xrLabel33 = new XRLabel();
			xrLabel32 = new XRLabel();
			xrLabel31 = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel29 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel44 = new XRLabel();
			xrLabel27 = new XRLabel();
			lblDescricaoDesconto = new XRLabel();
			lblDesconto = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel42 = new XRLabel();
			xrLabel41 = new XRLabel();
			xrLabel40 = new XRLabel();
			xrLabel39 = new XRLabel();
			xrLabel38 = new XRLabel();
			pagamento = new SubBand();
			xrPanel1 = new XRPanel();
			xrLabel28 = new XRLabel();
			troco = new FormattingRule();
			xrLabel6 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel18 = new XRLabel();
			observacao = new SubBand();
			xrPanel2 = new XRPanel();
			xrLabel20 = new XRLabel();
			xrLabel21 = new XRLabel();
			contato = new CalculatedField();
			cupomDesconto = new CalculatedField();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
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
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[15]
			{
				(XRControl)xrLabel5,
				(XRControl)xrLabel9,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16,
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)xrLabel1
			});
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).Font = new Font("Segoe UI", 12f);
			((XRControl)ReportHeader).HeightF = 375.0376f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)ReportHeader).StylePriority.UseFont = false;
			((Band)ReportHeader).SubBands.AddRange((SubBand[])(object)new SubBand[2] { enderecoEntrega, retiraLocal });
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 212.5134f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(187.8541f, 58.41998f);
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Contato:";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel9).CanGrow = false;
			((XRControl)xrLabel9).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "contato")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).LocationFloat = new PointFloat(187.8541f, 212.5134f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(747.5206f, 58.41998f);
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel17).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CriadoEm", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).LocationFloat = new PointFloat(1403.438f, 95.67334f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(496.5614f, 58.42001f);
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "xrLabel17";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1226.167f, 95.67334f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(177.2708f, 58.42001f);
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).Text = "Data:";
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Status")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(713.5204f, 95.67334f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(455.2919f, 58.42001f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Text = "xrLabel15";
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(536.2496f, 95.67334f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(177.2708f, 58.42001f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Status:";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel12).BackColor = Color.LightGray;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(0.0004037221f, 300.0376f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(1900f, 75f);
			((XRControl)xrLabel12).StylePriority.UseBackColor = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Entrega";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel11).CanGrow = false;
			((XRControl)xrLabel11).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "documentoCliente")
			});
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1469.583f, 154.0934f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(430.4162f, 58.42001f);
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "xrLabel11";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel10).CanGrow = false;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Email")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).LocationFloat = new PointFloat(935.3746f, 212.5134f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(964.6249f, 58.41998f);
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "xrLabel10";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel8).CanGrow = false;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Cliente.Nome")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(179.9167f, 154.0934f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(988.8956f, 58.42001f);
			((XRControl)xrLabel8).Text = "xrLabel8";
			((XRControl)xrLabel7).CanGrow = false;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Numero")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).LocationFloat = new PointFloat(211.6667f, 95.67334f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(288.6873f, 58.42001f);
			((XRControl)xrLabel7).Text = "xrLabel7";
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1202.354f, 154.0934f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(267.2292f, 58.42003f);
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "Documento:";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(0f, 154.0934f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(179.9167f, 58.42001f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "Cliente:";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 95.67334f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(211.6667f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Número:";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).BackColor = Color.LightGray;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1900f, 75f);
			((XRControl)xrLabel1).StylePriority.UseBackColor = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Pedido";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)enderecoEntrega).Controls.AddRange((XRControl[])(object)new XRControl[19]
			{
				(XRControl)xrLabel24,
				(XRControl)xrLabel26,
				(XRControl)xrLabel25,
				(XRControl)xrLabel23,
				(XRControl)referencia,
				(XRControl)tituloComplemento,
				(XRControl)tituloBairro,
				(XRControl)bairro,
				(XRControl)tituloMunicipio,
				(XRControl)municipio,
				(XRControl)complemento,
				(XRControl)tituloUf,
				(XRControl)uf,
				(XRControl)tituloCep,
				(XRControl)cep,
				(XRControl)numero,
				(XRControl)tiluloNumero,
				(XRControl)endereco,
				(XRControl)tituloEndereco
			});
			((XRControl)enderecoEntrega).Dpi = 254f;
			((XRControl)enderecoEntrega).HeightF = 297.1f;
			((XRControl)enderecoEntrega).Name = "enderecoEntrega";
			((XRControl)xrLabel24).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Notas")
			});
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).LocationFloat = new PointFloat(0.0008615514f, 292.1f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(1899.998f, 5f);
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Nome")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).LocationFloat = new PointFloat(283.1042f, 8.074442E-05f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(2, 2, 0, 0, 96f);
			((XRControl)xrLabel26).SizeF = new SizeF(1616.895f, 58.42f);
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(1.249893f, 233.6801f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(252.7501f, 58.41992f);
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Referência:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(0.0004037221f, 0f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(283.1038f, 58.42001f);
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).Text = "Destinatário:";
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)1;
			((XRControl)referencia).CanGrow = false;
			((XRControl)referencia).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Referencia")
			});
			((XRControl)referencia).Dpi = 254f;
			((XRControl)referencia).LocationFloat = new PointFloat(254f, 233.6801f);
			((XRControl)referencia).Name = "referencia";
			((XRControl)referencia).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)referencia).SizeF = new SizeF(1645.999f, 58.41995f);
			((XRControl)referencia).StylePriority.UseTextAlignment = false;
			((XRControl)referencia).Text = "referencia";
			((XRControl)referencia).TextAlignment = (TextAlignment)16;
			((XRControl)tituloComplemento).Dpi = 254f;
			((XRControl)tituloComplemento).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloComplemento).LocationFloat = new PointFloat(1.009305E-05f, 175.2601f);
			((XRControl)tituloComplemento).Name = "tituloComplemento";
			((XRControl)tituloComplemento).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloComplemento).SizeF = new SizeF(316.2501f, 58.41992f);
			((XRControl)tituloComplemento).StylePriority.UseFont = false;
			((XRControl)tituloComplemento).StylePriority.UseTextAlignment = false;
			((XRControl)tituloComplemento).Text = "Complemento:";
			((XRControl)tituloComplemento).TextAlignment = (TextAlignment)16;
			((XRControl)tituloBairro).Dpi = 254f;
			((XRControl)tituloBairro).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloBairro).LocationFloat = new PointFloat(1.009305E-05f, 116.8398f);
			((XRControl)tituloBairro).Name = "tituloBairro";
			((XRControl)tituloBairro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloBairro).SizeF = new SizeF(148.1667f, 58.42004f);
			((XRControl)tituloBairro).StylePriority.UseFont = false;
			((XRControl)tituloBairro).StylePriority.UseTextAlignment = false;
			((XRControl)tituloBairro).Text = "Bairro:";
			((XRControl)tituloBairro).TextAlignment = (TextAlignment)1;
			((XRControl)bairro).CanGrow = false;
			((XRControl)bairro).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Bairro")
			});
			((XRControl)bairro).Dpi = 254f;
			((XRControl)bairro).LocationFloat = new PointFloat(148.1667f, 116.84f);
			((XRControl)bairro).Name = "bairro";
			((XRControl)bairro).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)bairro).SizeF = new SizeF(418.0417f, 58.41998f);
			((XRControl)bairro).StylePriority.UseTextAlignment = false;
			((XRControl)bairro).Text = "bairro";
			((XRControl)bairro).TextAlignment = (TextAlignment)16;
			((XRControl)tituloMunicipio).Dpi = 254f;
			((XRControl)tituloMunicipio).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloMunicipio).LocationFloat = new PointFloat(566.2078f, 116.8398f);
			((XRControl)tituloMunicipio).Name = "tituloMunicipio";
			((XRControl)tituloMunicipio).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloMunicipio).SizeF = new SizeF(230.1875f, 58.42004f);
			((XRControl)tituloMunicipio).StylePriority.UseFont = false;
			((XRControl)tituloMunicipio).StylePriority.UseTextAlignment = false;
			((XRControl)tituloMunicipio).Text = "Município:";
			((XRControl)tituloMunicipio).TextAlignment = (TextAlignment)64;
			((XRControl)municipio).CanGrow = false;
			((XRControl)municipio).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Municipio")
			});
			((XRControl)municipio).Dpi = 254f;
			((XRControl)municipio).LocationFloat = new PointFloat(796.3953f, 116.8398f);
			((XRControl)municipio).Name = "municipio";
			((XRControl)municipio).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)municipio).SizeF = new SizeF(493.2709f, 58.41998f);
			((XRControl)municipio).StylePriority.UseTextAlignment = false;
			((XRControl)municipio).Text = "municipio";
			((XRControl)municipio).TextAlignment = (TextAlignment)16;
			((XRControl)complemento).CanGrow = false;
			((XRControl)complemento).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Complemento")
			});
			((XRControl)complemento).Dpi = 254f;
			((XRControl)complemento).LocationFloat = new PointFloat(316.2496f, 175.2601f);
			((XRControl)complemento).Name = "complemento";
			((XRControl)complemento).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)complemento).SizeF = new SizeF(1583.75f, 58.42004f);
			((XRControl)complemento).StylePriority.UseTextAlignment = false;
			((XRControl)complemento).Text = "complemento";
			((XRControl)complemento).TextAlignment = (TextAlignment)16;
			((XRControl)tituloUf).Dpi = 254f;
			((XRControl)tituloUf).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloUf).LocationFloat = new PointFloat(1289.667f, 116.8398f);
			((XRControl)tituloUf).Name = "tituloUf";
			((XRControl)tituloUf).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloUf).SizeF = new SizeF(113.7709f, 58.41998f);
			((XRControl)tituloUf).StylePriority.UseFont = false;
			((XRControl)tituloUf).StylePriority.UseTextAlignment = false;
			((XRControl)tituloUf).Text = "UF:";
			((XRControl)tituloUf).TextAlignment = (TextAlignment)64;
			((XRControl)uf).CanGrow = false;
			((XRControl)uf).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Uf")
			});
			((XRControl)uf).Dpi = 254f;
			((XRControl)uf).LocationFloat = new PointFloat(1403.438f, 116.84f);
			((XRControl)uf).Name = "uf";
			((XRControl)uf).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)uf).SizeF = new SizeF(136.4362f, 58.41998f);
			((XRControl)uf).StylePriority.UseTextAlignment = false;
			((XRControl)uf).Text = "uf";
			((XRControl)uf).TextAlignment = (TextAlignment)16;
			((XRControl)tituloCep).Dpi = 254f;
			((XRControl)tituloCep).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloCep).LocationFloat = new PointFloat(1539.875f, 116.8398f);
			((XRControl)tituloCep).Name = "tituloCep";
			((XRControl)tituloCep).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloCep).SizeF = new SizeF(113.7709f, 58.41998f);
			((XRControl)tituloCep).StylePriority.UseFont = false;
			((XRControl)tituloCep).StylePriority.UseTextAlignment = false;
			((XRControl)tituloCep).Text = "CEP:";
			((XRControl)tituloCep).TextAlignment = (TextAlignment)64;
			((XRControl)cep).CanGrow = false;
			((XRControl)cep).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Cep")
			});
			((XRControl)cep).Dpi = 254f;
			((XRControl)cep).LocationFloat = new PointFloat(1653.646f, 116.84f);
			((XRControl)cep).Name = "cep";
			((XRControl)cep).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)cep).SizeF = new SizeF(246.354f, 58.41998f);
			((XRControl)cep).StylePriority.UseTextAlignment = false;
			((XRControl)cep).Text = "cep";
			((XRControl)cep).TextAlignment = (TextAlignment)16;
			((XRControl)numero).CanGrow = false;
			((XRControl)numero).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Numero")
			});
			((XRControl)numero).Dpi = 254f;
			((XRControl)numero).LocationFloat = new PointFloat(1698.916f, 58.42004f);
			((XRControl)numero).Name = "numero";
			((XRControl)numero).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)numero).SizeF = new SizeF(201.0834f, 58.42001f);
			((XRControl)numero).StylePriority.UseTextAlignment = false;
			((XRControl)numero).Text = "numero";
			((XRControl)numero).TextAlignment = (TextAlignment)16;
			((XRControl)tiluloNumero).Dpi = 254f;
			((XRControl)tiluloNumero).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tiluloNumero).LocationFloat = new PointFloat(1484.603f, 58.42004f);
			((XRControl)tiluloNumero).Name = "tiluloNumero";
			((XRControl)tiluloNumero).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tiluloNumero).SizeF = new SizeF(214.3126f, 58.41995f);
			((XRControl)tiluloNumero).StylePriority.UseFont = false;
			((XRControl)tiluloNumero).StylePriority.UseTextAlignment = false;
			((XRControl)tiluloNumero).Text = "Número:";
			((XRControl)tiluloNumero).TextAlignment = (TextAlignment)64;
			((XRControl)endereco).CanGrow = false;
			((XRControl)endereco).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnderecoEntrega.Logradouro")
			});
			((XRControl)endereco).Dpi = 254f;
			((XRControl)endereco).LocationFloat = new PointFloat(211.6667f, 58.42004f);
			((XRControl)endereco).Name = "endereco";
			((XRControl)endereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)endereco).SizeF = new SizeF(1272.936f, 58.42001f);
			((XRControl)endereco).StylePriority.UseTextAlignment = false;
			((XRControl)endereco).Text = "endereco";
			((XRControl)endereco).TextAlignment = (TextAlignment)16;
			((XRControl)tituloEndereco).Dpi = 254f;
			((XRControl)tituloEndereco).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)tituloEndereco).LocationFloat = new PointFloat(1.009305E-05f, 58.42004f);
			((XRControl)tituloEndereco).Name = "tituloEndereco";
			((XRControl)tituloEndereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloEndereco).SizeF = new SizeF(211.6667f, 58.42001f);
			((XRControl)tituloEndereco).StylePriority.UseFont = false;
			((XRControl)tituloEndereco).StylePriority.UseTextAlignment = false;
			((XRControl)tituloEndereco).Text = "Endereço:";
			((XRControl)tituloEndereco).TextAlignment = (TextAlignment)1;
			((XRControl)retiraLocal).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)retira });
			((XRControl)retiraLocal).Dpi = 254f;
			((XRControl)retiraLocal).HeightF = 58.41998f;
			((XRControl)retiraLocal).Name = "retiraLocal";
			((XRControl)retira).Dpi = 254f;
			((XRControl)retira).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)retira).Name = "retira";
			((XRControl)retira).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)retira).SizeF = new SizeF(714.375f, 58.41998f);
			((XRControl)retira).Text = "Cliente retira na loja";
			documentoCliente.Expression = "Iif([Cliente.Cpf] == '', [Cliente.Cnpj], [Cliente.Cpf])";
			documentoCliente.Name = "documentoCliente";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)ReportFooter
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel13,
				(XRControl)xrLabel37,
				(XRControl)xrLabel36,
				(XRControl)xrLabel35,
				(XRControl)xrLabel34
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail1).HeightF = 60.09f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrLabel13).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.CodigoInterno")
			});
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(254f, 60.09f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "xrLabel13";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)256;
			((XRControl)xrLabel37).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Total", "{0:n2}")
			});
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel37).LocationFloat = new PointFloat(1646f, 0f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(254f, 60.09f);
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "xrLabel37";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel36).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Preco", "{0:n2}")
			});
			((XRControl)xrLabel36).Dpi = 254f;
			((XRControl)xrLabel36).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel36).LocationFloat = new PointFloat(1392f, 0f);
			((XRControl)xrLabel36).Name = "xrLabel36";
			((XRControl)xrLabel36).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel36).SizeF = new SizeF(254f, 60.09f);
			((XRControl)xrLabel36).StylePriority.UseFont = false;
			((XRControl)xrLabel36).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel36).Text = "xrLabel36";
			((XRControl)xrLabel36).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel35).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Quantidade", "{0:n2}")
			});
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(1148.583f, 0f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(243.4165f, 60.09f);
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).Text = "xrLabel35";
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel34).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Descricao")
			});
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(254f, 0f);
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(894.5834f, 60.09f);
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel34).Text = "xrLabel34";
			((XRControl)xrLabel34).TextAlignment = (TextAlignment)256;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel22,
				(XRControl)xrLabel33,
				(XRControl)xrLabel32,
				(XRControl)xrLabel31,
				(XRControl)xrLabel30,
				(XRControl)xrLabel29
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 158.42f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel22).BackColor = Color.LightGray;
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(0.0004037221f, 25f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(1900f, 75f);
			((XRControl)xrLabel22).StylePriority.UseBackColor = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).Text = "Produtos";
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel33).BackColor = Color.LightGray;
			((XRControl)xrLabel33).Borders = (BorderSide)0;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).LocationFloat = new PointFloat(1646f, 100f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel33).StylePriority.UseBackColor = false;
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Total";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel32).BackColor = Color.LightGray;
			((XRControl)xrLabel32).Borders = (BorderSide)0;
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1392f, 100f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel32).StylePriority.UseBackColor = false;
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).Text = "Preço";
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel31).BackColor = Color.LightGray;
			((XRControl)xrLabel31).Borders = (BorderSide)0;
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).LocationFloat = new PointFloat(1148.583f, 100f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(243.4166f, 58.42f);
			((XRControl)xrLabel31).StylePriority.UseBackColor = false;
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).Text = "Quantidade";
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel30).BackColor = Color.LightGray;
			((XRControl)xrLabel30).Borders = (BorderSide)0;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).LocationFloat = new PointFloat(254f, 100f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(894.5834f, 58.42f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).Text = "Descrição";
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel29).BackColor = Color.LightGray;
			((XRControl)xrLabel29).Borders = (BorderSide)0;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).LocationFloat = new PointFloat(0f, 100f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel29).StylePriority.UseBackColor = false;
			((XRControl)xrLabel29).StylePriority.UseBorders = false;
			((XRControl)xrLabel29).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel29).Text = "Código";
			((XRControl)xrLabel29).TextAlignment = (TextAlignment)1;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[10]
			{
				(XRControl)xrLabel44,
				(XRControl)xrLabel27,
				(XRControl)lblDescricaoDesconto,
				(XRControl)lblDesconto,
				(XRControl)xrLabel43,
				(XRControl)xrLabel42,
				(XRControl)xrLabel41,
				(XRControl)xrLabel40,
				(XRControl)xrLabel39,
				(XRControl)xrLabel38
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).Font = new Font("Segoe UI", 12f);
			((XRControl)ReportFooter).HeightF = 400f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)ReportFooter).StylePriority.UseFont = false;
			((Band)ReportFooter).SubBands.AddRange((SubBand[])(object)new SubBand[2] { pagamento, observacao });
			((XRControl)xrLabel44).BorderColor = Color.Silver;
			((XRControl)xrLabel44).Borders = (BorderSide)0;
			((XRControl)xrLabel44).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamento.Juros", "+ {0:n2}")
			});
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1598.376f, 140f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(301.6241f, 70f);
			((XRControl)xrLabel44).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel27).BorderColor = Color.Silver;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).LocationFloat = new PointFloat(0.0008615514f, 140f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(1598.375f, 70.00002f);
			((XRControl)xrLabel27).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).Text = "Acréscimo";
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)16;
			((XRControl)lblDescricaoDesconto).Dpi = 254f;
			((XRControl)lblDescricaoDesconto).LocationFloat = new PointFloat(0f, 210f);
			((XRControl)lblDescricaoDesconto).Name = "lblDescricaoDesconto";
			((XRControl)lblDescricaoDesconto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDescricaoDesconto).SizeF = new SizeF(1598.375f, 5.291672f);
			((XRControl)lblDescricaoDesconto).StylePriority.UseTextAlignment = false;
			((XRControl)lblDescricaoDesconto).Text = "lblDescricaoDesconto";
			((XRControl)lblDescricaoDesconto).TextAlignment = (TextAlignment)16;
			((XRControl)lblDesconto).BorderColor = Color.Silver;
			((XRControl)lblDesconto).Borders = (BorderSide)0;
			((XRControl)lblDesconto).Dpi = 254f;
			((XRControl)lblDesconto).LocationFloat = new PointFloat(1598.375f, 210f);
			((XRControl)lblDesconto).Name = "lblDesconto";
			((XRControl)lblDesconto).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDesconto).SizeF = new SizeF(301.6254f, 5f);
			((XRControl)lblDesconto).StylePriority.UseBorderColor = false;
			((XRControl)lblDesconto).StylePriority.UseBorders = false;
			((XRControl)lblDesconto).StylePriority.UseTextAlignment = false;
			((XRControl)lblDesconto).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel43).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel43).BorderColor = Color.Black;
			((XRControl)xrLabel43).Borders = (BorderSide)2;
			((XRControl)xrLabel43).CanGrow = false;
			((XRControl)xrLabel43).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "TotalPedido", "{0:c2}")
			});
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel43).LocationFloat = new PointFloat(1403.439f, 215.2917f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(496.5614f, 70f);
			((XRControl)xrLabel43).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel43).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel43).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel43).Text = "xrLabel43";
			((XRControl)xrLabel43).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel42).BorderColor = Color.Silver;
			((XRControl)xrLabel42).Borders = (BorderSide)0;
			((XRControl)xrLabel42).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EnvioValor", "+ {0:n2}")
			});
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).LocationFloat = new PointFloat(1598.375f, 69.99992f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(301.6251f, 70.00002f);
			((XRControl)xrLabel42).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel42).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel41).BorderColor = Color.DimGray;
			((XRControl)xrLabel41).Borders = (BorderSide)2;
			((XRControl)xrLabel41).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Total")
			});
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).LocationFloat = new PointFloat(1598.375f, 0f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(301.625f, 70f);
			((XRControl)xrLabel41).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel41).StylePriority.UseBorders = false;
			((XRControl)xrLabel41).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:n2}";
			val.Running = (SummaryRunning)2;
			xrLabel41.Summary = val;
			((XRControl)xrLabel41).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel40).AnchorVertical = (VerticalAnchorStyles)2;
			((XRControl)xrLabel40).BorderColor = Color.Black;
			((XRControl)xrLabel40).Borders = (BorderSide)2;
			((XRControl)xrLabel40).CanGrow = false;
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).LocationFloat = new PointFloat(0f, 215.2917f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(1403.437f, 70f);
			((XRControl)xrLabel40).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel40).Text = "Total pedido";
			((XRControl)xrLabel40).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel39).BorderColor = Color.Silver;
			((XRControl)xrLabel39).Borders = (BorderSide)0;
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).LocationFloat = new PointFloat(0f, 69.99992f);
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(1598.375f, 70.00002f);
			((XRControl)xrLabel39).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel39).StylePriority.UseBorders = false;
			((XRControl)xrLabel39).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel39).Text = "Frete";
			((XRControl)xrLabel39).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel38).BorderColor = Color.DimGray;
			((XRControl)xrLabel38).Borders = (BorderSide)2;
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(1598.375f, 69.99994f);
			((XRControl)xrLabel38).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel38).StylePriority.UseBorders = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel38).Text = "Total produtos";
			((XRControl)xrLabel38).TextAlignment = (TextAlignment)16;
			((XRControl)pagamento).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrPanel1 });
			((XRControl)pagamento).Dpi = 254f;
			((XRControl)pagamento).HeightF = 202.7708f;
			((XRControl)pagamento).Name = "pagamento";
			((XRControl)xrPanel1).Borders = (BorderSide)15;
			((XRControl)xrPanel1).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel28,
				(XRControl)xrLabel6,
				(XRControl)xrLabel19,
				(XRControl)xrLabel18
			});
			((XRControl)xrPanel1).Dpi = 254f;
			((XRControl)xrPanel1).LocationFloat = new PointFloat(0f, 16.5f);
			((XRControl)xrPanel1).Name = "xrPanel1";
			((XRControl)xrPanel1).SizeF = new SizeF(1900f, 186.2708f);
			((XRControl)xrPanel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).Borders = (BorderSide)0;
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 10f);
			((Collection<FormattingRule>)(object)((XRControl)xrLabel28).FormattingRules).Add(troco);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(1403.438f, 20.7434f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).Text = "Troco para";
			troco.Condition = "[Troco]  <= 0";
			troco.Formatting.Visible = (DefaultBoolean)1;
			troco.Name = "troco";
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Troco", "{0:c2}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)xrLabel6).FormattingRules).Add(troco);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1403.438f, 79.16341f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(2, 2, 0, 0, 96f);
			((XRControl)xrLabel6).SizeF = new SizeF(471.5621f, 61.74993f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).Text = "xrLabel6";
			((XRControl)xrLabel19).Borders = (BorderSide)0;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(10.58313f, 20.7434f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).Text = "Pagamento";
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pagamento.ToText")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).LocationFloat = new PointFloat(10.58313f, 79.16341f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(1392.855f, 61.74995f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)observacao).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrPanel2 });
			((XRControl)observacao).Dpi = 254f;
			((XRControl)observacao).HeightF = 297.2708f;
			((XRControl)observacao).Name = "observacao";
			((XRControl)xrPanel2).Borders = (BorderSide)15;
			((XRControl)xrPanel2).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel20,
				(XRControl)xrLabel21
			});
			((XRControl)xrPanel2).Dpi = 254f;
			((XRControl)xrPanel2).LocationFloat = new PointFloat(0f, 23f);
			((XRControl)xrPanel2).Name = "xrPanel2";
			((XRControl)xrPanel2).SizeF = new SizeF(1900f, 274.2708f);
			((XRControl)xrPanel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).Borders = (BorderSide)0;
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(10.58313f, 20.7434f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).Text = "Observação";
			((XRControl)xrLabel21).Borders = (BorderSide)0;
			((XRControl)xrLabel21).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Observacao")
			});
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).LocationFloat = new PointFloat(10.58313f, 79.16345f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(1864.417f, 167.5833f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			contato.Expression = "Concat([Cliente.Telefone], '/', [Cliente.Celular])";
			contato.Name = "contato";
			cupomDesconto.Expression = "Iif(IsNullOrEmpty([CupomDesconto]), '' ,Concat('\"',[CupomDesconto], '\"  - ', [PercentualCupomDesconto], '%' ) )";
			cupomDesconto.Name = "cupomDesconto";
			bindingSource1.DataSource = typeof(PedidoEcommerceDto);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)ReportHeader,
				(Band)DetailReport
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[3] { documentoCliente, contato, cupomDesconto });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[1] { troco });
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.PedidosEcommerce.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.PedidosEcommerce.Properties.Resources", typeof(Resources).Assembly);
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

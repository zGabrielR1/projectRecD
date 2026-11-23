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
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto.Auxiliares.ArquivosContabilidade;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Relatorios;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Interfaces.EnvioEmail;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service.EnvioEmail;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Icones;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Tipos;
using Otus.Modulo.GeradorArquivosContabilidade.Reports;
using Otus.Modulo.GeradorArquivosContabilidade.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GeradorArquivosContabilidade")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GeradorArquivosContabilidade")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GeradorArquivosContabilidade
{
	[Module(ModuleName = "GeradorArquivoContabilidadeModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	public class GeradorArquivoContabilidadeModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GeradorArquivoContabilidadeModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GeradorArquivosContabilidadeView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GeradorArquivoContabilidadeFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GeradorArquivosContabilidade.Views
{
	public class VerificarXmls : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public VerificarXmls()
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
				Uri resourceLocator = new Uri("/Modulo.GeradorArquivosContabilidade;component/views/verificarxmls.xaml", UriKind.Relative);
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
	public class GeradorArquivoContabilidadeFichaView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GeradorArquivoContabilidadeFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GeradorArquivosContabilidade;component/views/geradorarquivocontabilidadefichaview.xaml", UriKind.Relative);
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
	public class GeradorArquivosContabilidadeView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GeradorArquivosContabilidadeView()
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
				Uri resourceLocator = new Uri("/Modulo.GeradorArquivosContabilidade;component/views/geradorarquivoscontabilidadeview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GeradorArquivosContabilidade.ViewModels
{
	public class GeradorArquivoContabilidadeAbaViewModel : AbaAuxiliarViewModelBase
	{
		public GeradorArquivoContabilidadeAbaViewModel(IViewModelDtoBase viewModel)
			: base("Envio de arquivos", "XMLs e arquivos fiscais", viewModel)
		{
		}
	}
	public class GeradorArquivoContabilidadeFichaViewModel : FichaBase<GeradorArquivoContabilidadeDto, GeradorArquivoContabilidadeViewModel>
	{
		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private IEnumerable<string> _resumoArquivosEnviados;

		private DateTime _dataEnvio;

		private string _emailsEnviados;

		public bool PodeImprimirResumo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeImprimirResumo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeImprimirResumo), value);
			}
		}

		public bool EnviandoEmail
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => EnviandoEmail));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => EnviandoEmail), value);
			}
		}

		public ICommand EnviarEmailCommand { get; set; }

		public ICommand ExportarArquivoCommand { get; set; }

		public ICommand VerificarXmlsCommand { get; set; }

		public ICommand ImprimirResumoCommand { get; set; }

		public GeradorArquivoContabilidadeFichaViewModel(IWService service, IImpressaoRelatorioService impressaoRelatorioService)
			: base(string.Empty)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			EnviarEmailCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarEmail, (Func<bool>)PodeEnviarEmail, false, (bool?)null);
			VerificarXmlsCommand = (ICommand)new AsyncCommand((Func<Task>)ValidarXmls, (Func<bool>)PodeVerificarXml, false, (bool?)null);
			ImprimirResumoCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirResumo, (Func<bool>)(() => PodeImprimirResumo), false, (bool?)null);
		}

		private async Task ImprimirResumo()
		{
			_ = 1;
			try
			{
				IEnumerable<CodigoDescricaoValorDto> resumoMovimentacoes = await _service.Canal<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<CodigoDescricaoValorDto>>>((Func<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<CodigoDescricaoValorDto>>>)((IGeradorArquivoContabilidadeModuleService x) => x.ResumoPorOrigemESituacao(_dataEnvio)));
				XtraReport obj = await new ResumoArquivosEnviados(new ResumoArquivosEnviadosContabilidadeDto
				{
					DataHoraEnvio = _dataEnvio,
					Arquivos = new List<CodigoDescricaoDto>(_resumoArquivosEnviados.Select((Func<string, CodigoDescricaoDto>)((string x) => new CodigoDescricaoDto
					{
						Descricao = x
					}))),
					EmailsEnviados = HelperString.ToStringEmptyWhenNull(_emailsEnviados),
					ResumoMovimentacoes = resumoMovimentacoes
				}).ObterRelatorio();
				IViewModelItemImprimirRelatorio val = (IViewModelItemImprimirRelatorio)(object)((obj is IViewModelItemImprimirRelatorio) ? obj : null);
				_impressaoRelatorioService.ImprimirRelatorio(val);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public void IniciarVariaveis()
		{
			EnviandoEmail = true;
			_emailsEnviados = string.Empty;
			_resumoArquivosEnviados = new List<string>();
			PodeImprimirResumo = false;
		}

		private async Task EnviarEmail()
		{
			_ = 1;
			try
			{
				IniciarVariaveis();
				string text = VerificarDestinatarioEmail();
				LoaderProcessoService.MostrarLoader("Enviando e-mail...");
				GeradorArquivoContabilidadeFormulario formulario = ((FichaBase<GeradorArquivoContabilidadeDto, GeradorArquivoContabilidadeViewModel>)this).Formulario as GeradorArquivoContabilidadeFormulario;
				formulario.GeradorArquivoContabilidadeViewModel.Informacoes?.Limpar();
				formulario.GeradorArquivoContabilidadeViewModel.MovimentacoesSemXml?.Limpar();
				GeradorArquivoContabilidadeDto dto = ((FormularioEntity<GeradorArquivoContabilidadeDto>)formulario).ToDto<GeradorArquivoContabilidadeDto>();
				GeradorArquivoContabilidadeDto obj = dto;
				ParametrosArquivoContabilidadeDto parametro = formulario.GeradorArquivoContabilidadeViewModel.Parametro;
				obj.EmailResposta = ((parametro != null) ? HelperString.ToStringEmptyWhenNull(parametro.EmailResposta) : null);
				GeradorArquivoContabilidadeDto obj2 = dto;
				ParametrosArquivoContabilidadeDto parametro2 = formulario.GeradorArquivoContabilidadeViewModel.Parametro;
				obj2.Corpo = ((parametro2 != null) ? HelperString.ToStringEmptyWhenNull(parametro2.CorpoEmail) : null) + "<br>";
				if (!string.IsNullOrEmpty(text))
				{
					dto.Email = dto.Email + ";" + text;
				}
				_emailsEnviados = dto.Email;
				_dataEnvio = dto.Data;
				List<string> informacoes = new List<string>();
				try
				{
					List<string> list = informacoes;
					list.AddRange(await _service.Canal<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<string>>>((Func<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<string>>>)((IGeradorArquivoContabilidadeModuleService x) => x.EnviarEmailAsync(dto))));
					DialogoFactory.MostrarDialogoConfirma($"E-mail enviado para {dto.Email}!", false, System.Windows.HorizontalAlignment.Center);
				}
				catch (Exception ex)
				{
					HelperException.ShowLastException(ex, "", false, false);
				}
				try
				{
					List<string> list = informacoes;
					list.AddRange(await EnviarEmailArquivosFiscais(dto));
				}
				catch (Exception ex2)
				{
					HelperException.ShowLastException(ex2, "", false, false);
				}
				if (informacoes.Any())
				{
					formulario.GeradorArquivoContabilidadeViewModel.AdicionarInformacoes(informacoes);
				}
				_resumoArquivosEnviados = informacoes;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
				PodeImprimirResumo = true;
				EnviandoEmail = false;
			}
		}

		private async Task ValidarXmls()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Verificando XMLs...");
				GeradorArquivoContabilidadeFormulario formulario = ((FichaBase<GeradorArquivoContabilidadeDto, GeradorArquivoContabilidadeViewModel>)this).Formulario as GeradorArquivoContabilidadeFormulario;
				formulario.GeradorArquivoContabilidadeViewModel.Informacoes.Limpar();
				formulario.GeradorArquivoContabilidadeViewModel.MovimentacoesSemXml.Limpar();
				GeradorArquivoContabilidadeDto dto = ((FormularioEntity<GeradorArquivoContabilidadeDto>)formulario).ToDto<GeradorArquivoContabilidadeDto>();
				IEnumerable<MovimentacaoSemXmlDto> enumerable = await _service.Canal<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<MovimentacaoSemXmlDto>>>((Func<IGeradorArquivoContabilidadeModuleService, Task<IEnumerable<MovimentacaoSemXmlDto>>>)((IGeradorArquivoContabilidadeModuleService service) => service.ValidarXmlDocumentoFiscaisEletronicos(dto.Data)));
				if (enumerable.Any())
				{
					formulario.GeradorArquivoContabilidadeViewModel.AdicionarInformacoesMovimentacoesSemXml(enumerable);
				}
				else
				{
					DialogoFactory.MostrarDialogoConfirma("Não foi detectado nenhum xml faltando no período informado.", false, System.Windows.HorizontalAlignment.Center);
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

		private bool PodeVerificarXml()
		{
			GeradorArquivoContabilidadeFormulario obj = base.Formulario as GeradorArquivoContabilidadeFormulario;
			object obj2;
			if (obj == null)
			{
				obj2 = null;
			}
			else
			{
				IAbaViewModelBase obj3 = ((Formulario)obj).Abas.FirstOrDefault((IAbaViewModelBase x) => x is GeradorArquivoContabilidadeAbaViewModel);
				obj2 = ((obj3 != null) ? obj3.ViewModel : null);
			}
			if (obj2 != null && ((IViewModelBase)obj2).Valido)
			{
				return !EnviandoEmail;
			}
			return false;
		}

		private string VerificarDestinatarioEmail()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			string empty = string.Empty;
			if (DialogoFactory.MostrarDialogoSimNao("Deseja enviar cópia do e-mail para outro endereço eletrônico, além do cadastrado no contabilista?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return string.Empty;
			}
			if (DialogoFactory.MostrarDialogoEntradaString(ref empty, "Informe os e-mails dos destinatários separados por ';'", "Envio de arquivos fiscais para a contabilidade", (IIcone)new EnviarEmail(), false, false, false, (string)null, false, false) == MessageBoxResult.None || empty == null)
			{
				return string.Empty;
			}
			return HelperString.ToStringEmptyWhenNull(empty);
		}

		private bool PodeEnviarEmail()
		{
			GeradorArquivoContabilidadeFormulario obj = base.Formulario as GeradorArquivoContabilidadeFormulario;
			object obj2;
			if (obj == null)
			{
				obj2 = null;
			}
			else
			{
				IAbaViewModelBase obj3 = ((Formulario)obj).Abas.FirstOrDefault((IAbaViewModelBase x) => x is GeradorArquivoContabilidadeAbaViewModel);
				obj2 = ((obj3 != null) ? obj3.ViewModel : null);
			}
			if (obj2 == null)
			{
				return false;
			}
			return ((IViewModelBase)obj2).Valido;
		}

		private async Task<IEnumerable<string>> EnviarEmailArquivosFiscais(GeradorArquivoContabilidadeDto dto)
		{
			string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal", "Relatorios");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			List<string> first = HelperIEnumerable.ToListEmptyWhenNull<string>(from x in Directory.GetFiles(text)
				where (x.Contains("Sintegra") || x.Contains("FortesFiscal") || x.Contains("QuestorContabil") || x.Contains("EfdContribuicao") || x.Contains("SpedFiscal") || x.Contains("EFPH") || x.Contains("DominioSistemas") || x.Contains("AlterdataContabil")) && x.Contains(dto.Data.ToString("MMyyyy"))
				select x);
			string[] relatoriosDoPeriodo = Directory.GetFiles(path);
			List<string> list = HelperIEnumerable.ToListEmptyWhenNull<string>(first.Union(relatoriosDoPeriodo));
			string text2 = Path.Combine(text, $"ArquivosFiscais_{dto.Data:MMyyyy}_{DadosSistema.EmpresaLogada.CpfCnpj}.zip");
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			HelperCompressao.CompactarArquivos(text2, (IEnumerable<string>)list, 10000000);
			List<string> informacoes = new List<string>
			{
				string.Format("EFD Contribuição: {0}", list.Count((string x) => x.Contains("EfdContribuicao"))),
				string.Format("Sintegra: {0}", list.Count((string x) => x.Contains("Sintegra"))),
				string.Format("Sped Fiscal: {0}", list.Count((string x) => x.Contains("SpedFiscal"))),
				string.Format("Fortes Fiscal: {0}", list.Count((string x) => x.Contains("FortesFiscal"))),
				string.Format("Questor Contábil: {0}", list.Count((string x) => x.Contains("QuestorContabil"))),
				string.Format("EFPH: {0}", list.Count((string x) => x.Contains("EFPH"))),
				string.Format("Domínio Sistemas: {0}", list.Count((string x) => x.Contains("DominioSistemas"))),
				string.Format("Alteradata Contábil: {0}", list.Count((string x) => x.Contains("AlterdataContabil"))),
				$"Relatórios: {relatoriosDoPeriodo.Length}"
			};
			if (!list.Any())
			{
				throw new Exception("Nenhum arquivo fiscal localizado no período solicitado.");
			}
			await EnviarEmail(dto, informacoes, text2);
			string text3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal", "Relatorios", "Enviados");
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			string[] array = relatoriosDoPeriodo;
			foreach (string obj in array)
			{
				FileInfo fileInfo = new FileInfo(obj);
				File.Move(obj, Path.Combine(text3, fileInfo.Name));
			}
			return informacoes;
		}

		private async Task EnviarEmail(GeradorArquivoContabilidadeDto dto, IEnumerable<string> informacoes, string arquivoCompactado)
		{
			AuxiliarDto auxiliar = DadosSistema.ConfiguracaoDto.Auxiliar;
			if (string.IsNullOrWhiteSpace(auxiliar.ServidorSmtp.Valor))
			{
				throw new Exception("Verifique as configurações de e-mail!\nAcesse Auxiliares/Configurações/Auxiliar");
			}
			string text = $"Arquivos fiscais referente {dto.Data:MM/yyyy} - {DadosSistema.EmpresaLogada.Nome}";
			List<string> list = new List<string>
			{
				$"Razão Social: {DadosSistema.EmpresaLogada.Nome}",
				$"CNPJ: {DadosSistema.EmpresaLogada.CpfCnpjFormatado}",
				" "
			};
			list.Add(dto.Corpo);
			list.AddRange(informacoes);
			EnvioEmailServiceFactory obj = new EnvioEmailServiceFactory().SetarServidorSmtp(auxiliar.ServidorSmtp.Valor).SetarAssunto(text).SetarEmailCopiaOculta(auxiliar.EnviarCopiaRemetente.Valor ? auxiliar.UsuarioEmail.Valor : string.Empty, DadosSistema.EmpresaLogada.Nome)
				.SetarEmailRemetente(auxiliar.UsuarioEmail.Valor, DadosSistema.EmpresaLogada.Nome)
				.AdicionarDestinatarios(dto.Email, "")
				.SetarEmailRemetenteParaResposta(HelperString.ToStringEmptyWhenNull(dto.EmailResposta), (string)null)
				.SetarMensagem(string.Join("<br />", list))
				.SetarPorta(auxiliar.PortaEmail.Valor)
				.SetarSenha(auxiliar.SenhaEmail.Valor)
				.SetarUsaSsl(auxiliar.UsaSsl.Valor)
				.SetarIgnorarCertificadoInvalido(auxiliar.IgnorarCertificadoInvalido.Valor);
			obj.AdicionarAnexo(arquivoCompactado, (string)null);
			EmailServiceFactory.SetCurrent((IEnvioEmailServiceFactory)(object)obj);
			IEnvioEmailService serviceEmail = EmailServiceFactory.Create();
			try
			{
				await serviceEmail.Enviar();
			}
			finally
			{
				serviceEmail.Dispose();
			}
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			GeradorArquivoContabilidadeViewModel geradorArquivoContabilidadeViewModel = new GeradorArquivoContabilidadeViewModel(_impressaoRelatorioService);
			((Base)this).Titulo = geradorArquivoContabilidadeViewModel.ToTituloFicha();
			if (base.Formulario == null)
			{
				base.Formulario = geradorArquivoContabilidadeViewModel.CriarFormulario();
			}
		}

		public override void Dispose()
		{
			EnviarEmailCommand = null;
			ExportarArquivoCommand = null;
			ImprimirResumoCommand = null;
			VerificarXmlsCommand = null;
			base.Dispose();
		}
	}
	public class GeradorArquivoContabilidadeFormulario : FormularioEntity<GeradorArquivoContabilidadeDto>
	{
		public GeradorArquivoContabilidadeViewModel GeradorArquivoContabilidadeViewModel { get; protected set; }

		public GeradorArquivoContabilidadeFormulario(GeradorArquivoContabilidadeDto dto, IImpressaoRelatorioService impressaoRelatorioService)
			: base(dto)
		{
			GeradorArquivoContabilidadeViewModel = new GeradorArquivoContabilidadeViewModel(impressaoRelatorioService);
			CriarAbas();
		}

		private void CriarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new GeradorArquivoContabilidadeAbaViewModel((IViewModelDtoBase)(object)GeradorArquivoContabilidadeViewModel) });
		}
	}
	public class GeradorArquivoContabilidadeViewModel : ViewModelDtoBase<GeradorArquivoContabilidadeDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public ISelecaoCommandParameter ContabilistaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoa)
		{
			Contabilista = pessoa;
		}, (ValidateParameter<PessoaDto>)(object)ValidacaoContabilista);

		private ValidateParameterPessoa<PessoaDto> ValidacaoContabilista => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => !HelperObject.Is<ContabilistaDtoNull>((object)x.Fisica.Contabilista)), "Selecione uma pessoa do tipo 'Contabilista'!", (TipoPessoaCadastro)7, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarContabilistaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarContabilista);

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Contabilista' é obrigatório.")]
		public string NomeContabilista
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeContabilista));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeContabilista), value, (Action)RemoverContabilistaQuandoNomeApagado);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o campo 'Contabilista' pela pesquisa: o texto digitado nao corresponde a um cadastro.")]
		public string ContabilistaSelecionado
		{
			get
			{
				if (!HelperObject.IsNull((object)Contabilista) && !HelperObject.Is<PessoaDtoNull>((object)Contabilista))
				{
					return NomeContabilista;
				}
				return string.Empty;
			}
		}

		public PessoaDto Contabilista
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Contabilista));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Contabilista), value, (Action)delegate
				{
					NomeContabilista = ObterNomeContabilista();
				});
			}
		}

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

		public Lista<string> Informacoes { get; set; }

		public Lista<MovimentacaoSemXmlDto> MovimentacoesSemXml { get; set; }

		public ParametrosArquivoContabilidadeDto Parametro
		{
			get
			{
				return ((BindableBase)this).GetProperty<ParametrosArquivoContabilidadeDto>((Expression<Func<ParametrosArquivoContabilidadeDto>>)(() => Parametro));
			}
			set
			{
				((BindableBase)this).SetProperty<ParametrosArquivoContabilidadeDto>((Expression<Func<ParametrosArquivoContabilidadeDto>>)(() => Parametro), value);
			}
		}

		public ICommand RemoverContabilistaCommand { get; set; }

		public ICommand ImprimirMovimentacoesSemXmlCommand { get; set; }

		public GeradorArquivoContabilidadeViewModel(IImpressaoRelatorioService impressaoRelatorioService)
			: base(new GeradorArquivoContabilidadeDto())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			_impressaoRelatorioService = impressaoRelatorioService;
			RemoverContabilistaCommand = (ICommand)new DelegateCommand((Action)RemoverContabilista, (Func<bool>)PodeRemoverContabilista, (bool?)null);
			ImprimirMovimentacoesSemXmlCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirMovimentacoesSemXml, (Func<bool>)PodeImprimirMovimentacoesSemXml, false, (bool?)null);
		}

		private bool PodeRemoverContabilista()
		{
			if (HelperObject.IsNotNull((object)Contabilista))
			{
				return !HelperObject.Is<PessoaDtoNull>((object)Contabilista);
			}
			return false;
		}

		private void RemoverContabilista()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Contabilista = (PessoaDto)new PessoaDtoNull();
		}

		private bool PodeImprimirMovimentacoesSemXml()
		{
			if (HelperObject.IsNotNull((object)MovimentacoesSemXml))
			{
				return MovimentacoesSemXml.Itens.Count > 0;
			}
			return false;
		}

		private async Task ImprimirMovimentacoesSemXml()
		{
			try
			{
				XtraReport obj = await new DocumentosFiscaisSemXml(MovimentacoesSemXml.Itens).ObterRelatorio();
				IViewModelItemImprimirRelatorio val = (IViewModelItemImprimirRelatorio)(object)((obj is IViewModelItemImprimirRelatorio) ? obj : null);
				_impressaoRelatorioService.ImprimirRelatorio(val);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async void PesquisarContabilista(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomeContabilista, (TipoPessoaCadastro)7, (Action<PessoaDto>)delegate(PessoaDto pessoa)
			{
				Contabilista = pessoa;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoContabilista, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private string ObterNomeContabilista()
		{
			if (!HelperObject.IsNull((object)Contabilista) && !HelperObject.Is<PessoaDtoNull>((object)Contabilista))
			{
				return Contabilista.Nome;
			}
			return string.Empty;
		}

		private void RemoverContabilistaQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomeContabilista) && HelperObject.IsNotNull((object)Contabilista) && HelperObject.IsNot<PessoaDtoNull>((object)Contabilista))
			{
				Contabilista = (PessoaDto)new PessoaDtoNull();
			}
		}

		public override void ProjectedAsViewModel()
		{
			Data = DateTime.Now.AddMonths(-1);
			Informacoes = new Lista<string>();
			MovimentacoesSemXml = new Lista<MovimentacaoSemXmlDto>();
			CarregarContabilista();
			Parametro = DadosSistema.GetService().Canal<IGeradorArquivoContabilidadeModuleService, ParametrosArquivoContabilidadeDto>((Func<IGeradorArquivoContabilidadeModuleService, ParametrosArquivoContabilidadeDto>)((IGeradorArquivoContabilidadeModuleService x) => x.ObterParametrosExportarArquivoDto()));
			Parametro.DataInicial = DateTime.Now;
			Parametro.DataFinal = DateTime.Now;
		}

		private void CarregarContabilista()
		{
			DadosSistema.GetService().Canal<IPessoaModuleService>((Action<IPessoaModuleService>)delegate(IPessoaModuleService x)
			{
				x.ObterPessoasAtivasPorTipoCadastro((TipoPessoaCadastro)7).ContinueWith(delegate(Task<IEnumerable<PessoaDto>> task)
				{
					if (!task.IsFaulted)
					{
						Contabilista = task.Result.FirstOrDefault();
					}
				});
			});
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GeradorArquivoContabilidadeDto val = (GeradorArquivoContabilidadeDto)((obj is GeradorArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Data = Data;
				val.Email = Contabilista.EmailPrincipal.Endereco;
				base.AtualizarDto<GeradorArquivoContabilidadeDto>(ref val);
			}
		}

		public string ToTituloLista()
		{
			return "Arquivos contabilidade";
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new GeradorArquivoContabilidadeFormulario(base.Dto, _impressaoRelatorioService);
		}

		public string ToTituloFicha()
		{
			return "Arquivos para contabilidade";
		}

		public void AdicionarInformacoes(IEnumerable<string> informacoes)
		{
			Informacoes.Limpar();
			Informacoes.Adicionar(informacoes);
		}

		public void AdicionarInformacoesMovimentacoesSemXml(IEnumerable<MovimentacaoSemXmlDto> lista)
		{
			MovimentacoesSemXml.Limpar();
			MovimentacoesSemXml.Adicionar(lista);
		}
	}
	public class GeradorArquivosContabilidadeViewModel : ModuloBase
	{
		public GeradorArquivosContabilidadeViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGeradorArquivoContabilidadeRegion, typeof(GeradorArquivoContabilidadeFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.GeradorArquivosContabilidade.Reports
{
	public class ResumoArquivosEnviados : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly ResumoArquivosEnviadosContabilidadeDto _dto;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel5;

		private CalculatedField xmlAutorizado;

		private CalculatedField xmlCancelado;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel4;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel3;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLabel xrLabel6;

		private XRLabel xrLabel7;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel9;

		public string TituloRelatorio => "Arquivos enviados para contabilidade";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => true;

		public IList<TipoViewModelBase> Parametros { get; protected set; } = new List<TipoViewModelBase>();


		public string DescricaoDetalhada => "Arquivos enviados para contabilidade";

		public ResumoArquivosEnviados(ResumoArquivosEnviadosContabilidadeDto dto)
		{
			_dto = dto;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XtraReportBase)this).DataSource = new List<ResumoArquivosEnviadosContabilidadeDto> { _dto };
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
			//IL_019c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0209: Unknown result type (might be due to invalid IL or missing references)
			//IL_0233: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0328: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Expected O, but got Unknown
			//IL_0407: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b3: Expected O, but got Unknown
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0501: Unknown result type (might be due to invalid IL or missing references)
			//IL_0593: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_072c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0732: Expected O, but got Unknown
			//IL_0771: Unknown result type (might be due to invalid IL or missing references)
			//IL_079b: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_08df: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a87: Expected O, but got Unknown
			//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cff: Expected O, but got Unknown
			//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d67: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel1 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel5 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			xmlAutorizado = new CalculatedField();
			xmlCancelado = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel4 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel3 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel7 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			xrLabel6 = new XRLabel();
			xrLabel9 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel8,
				(XRControl)xrLabel2,
				(XRControl)xrLabel5
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail).HeightF = 122.84f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 58.42001f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(367.7708f, 64.41999f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UsePadding = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "E-mails do envio:";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).Borders = (BorderSide)0;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(367.7708f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UsePadding = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Data/Hora do envio:";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).Borders = (BorderSide)0;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraEnvio", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(367.7708f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1731.229f, 58.42001f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EmailsEnviados")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).LocationFloat = new PointFloat(367.7705f, 58.42001f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(1731.229f, 64.42001f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)16;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 44.32884f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			xmlAutorizado.Expression = "Iif([Situacao] == 'Concluído', Iif([PossuiXmlAutorizado] == True, 'Ok', 'Não encontrado'), '-')";
			xmlAutorizado.Name = "xmlAutorizado";
			xmlCancelado.Expression = "Iif([Situacao] == 'Cancelado', Iif([PossuiXmlCancelado] == True, 'Ok', 'Não encontrado'), '-')";
			xmlCancelado.Name = "xmlCancelado";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport).DataMember = "Arquivos";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel4 });
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 76.94088f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel4).Borders = (BorderSide)0;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Arquivos.Descricao")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(2.999999f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(2095.999f, 76.94088f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UsePadding = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel3 });
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 88.52415f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel3).BackColor = Color.Gainsboro;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1.009305E-05f, 15f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(2098.999f, 59.42001f);
			((XRControl)xrLabel3).StylePriority.UseBackColor = false;
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UsePadding = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "RESUMO DOS ARQUIVOS ENVIADOS";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)32;
			bindingSource1.DataSource = typeof(ResumoArquivosEnviadosContabilidadeDto);
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail2,
				(Band)GroupHeader2
			});
			((XtraReportBase)DetailReport1).DataMember = "ResumoMovimentacoes";
			((XtraReportBase)DetailReport1).DataSource = bindingSource1;
			((XRControl)DetailReport1).Dpi = 254f;
			DetailReport1.Level = 1;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel9,
				(XRControl)xrLabel7
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).HeightF = 76.94088f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)xrLabel7).Borders = (BorderSide)0;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResumoMovimentacoes.Descricao")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(246.7329f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(983.078f, 76.94088f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UsePadding = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel6 });
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 87.3125f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)xrLabel6).BackColor = Color.Gainsboro;
			((XRControl)xrLabel6).Borders = (BorderSide)0;
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1.000977f, 13.35416f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(2098.999f, 59.42001f);
			((XRControl)xrLabel6).StylePriority.UseBackColor = false;
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UsePadding = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "RESUMO DOS DOCUMENTOS FISCAIS";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel9).Borders = (BorderSide)0;
			((XRControl)xrLabel9).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ResumoMovimentacoes.Valor", "{0:n2}")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(1229.811f, 0f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(619.979f, 76.94088f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)64;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)DetailReport1
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[2] { xmlAutorizado, xmlCancelado });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(0, 0, 0, 44);
			((XtraReport)this).PageHeight = 2101;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class DocumentosFiscaisSemXml : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly IEnumerable<MovimentacaoSemXmlDto> _dtos;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel xrLabel10;

		private XRLabel xrLabel16;

		private XRLabel xrLabel11;

		private XRLabel xrLabel13;

		private XRLabel xrLabel7;

		private XRLabel xrLabel12;

		private XRLabel xrLabel2;

		private XRLabel xrLabel5;

		private XRLabel xrLabel3;

		private XRLabel xrLabel17;

		private XRLabel xrLabel4;

		private XRLabel xrLabel6;

		private CalculatedField xmlAutorizado;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private CalculatedField xmlCancelado;

		public string TituloRelatorio => "Documento fiscais eletrônicos sem XML";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => false;

		public IList<TipoViewModelBase> Parametros { get; protected set; } = new List<TipoViewModelBase>();


		public string DescricaoDetalhada => "Documento fiscais eletrônicos sem XML";

		public DocumentosFiscaisSemXml(IEnumerable<MovimentacaoSemXmlDto> dtos)
		{
			_dtos = dtos;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XtraReportBase)this).DataSource = _dtos;
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
			//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Unknown result type (might be due to invalid IL or missing references)
			//IL_029c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_0378: Expected O, but got Unknown
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Expected O, but got Unknown
			//IL_0407: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0574: Unknown result type (might be due to invalid IL or missing references)
			//IL_059d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0639: Unknown result type (might be due to invalid IL or missing references)
			//IL_0662: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0727: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_086b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0894: Unknown result type (might be due to invalid IL or missing references)
			//IL_0930: Unknown result type (might be due to invalid IL or missing references)
			//IL_0959: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa4: Expected O, but got Unknown
			//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b4a: Expected O, but got Unknown
			//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c12: Expected O, but got Unknown
			//IL_0c37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cd5: Expected O, but got Unknown
			//IL_0cfa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d98: Expected O, but got Unknown
			//IL_0dbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e43: Expected O, but got Unknown
			//IL_0e68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f46: Expected O, but got Unknown
			//IL_0f6b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
			//IL_1003: Unknown result type (might be due to invalid IL or missing references)
			//IL_1009: Expected O, but got Unknown
			//IL_102e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1057: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			GroupHeader1 = new GroupHeaderBand();
			xrLabel1 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel6 = new XRLabel();
			xmlAutorizado = new CalculatedField();
			xmlCancelado = new CalculatedField();
			xrLabel14 = new XRLabel();
			xrLabel15 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)xrLabel2,
				(XRControl)xrLabel5,
				(XRControl)xrLabel3,
				(XRControl)xrLabel17,
				(XRControl)xrLabel4,
				(XRControl)xrLabel6
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail).HeightF = 50f;
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
			bindingSource1.DataSource = typeof(MovimentacaoSemXmlDto);
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[9]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel8,
				(XRControl)xrLabel9,
				(XRControl)xrLabel10,
				(XRControl)xrLabel16,
				(XRControl)xrLabel11,
				(XRControl)xrLabel13,
				(XRControl)xrLabel7,
				(XRControl)xrLabel12
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 11f);
			GroupHeader1.GroupFields.AddRange((GroupField[])(object)new GroupField[1]
			{
				new GroupField("Origem", (XRColumnSortOrder)1)
			});
			((XRControl)GroupHeader1).HeightF = 141.8401f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).BackColor = Color.Gainsboro;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Origem")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(4.037221E-05f, 25.00001f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(2770f, 58.42f);
			((XRControl)xrLabel1).StylePriority.UseBackColor = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "xrLabel1";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(4.037221E-05f, 83.42001f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(214.3124f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).Text = "Número";
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(214.3125f, 83.42002f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(124.3541f, 58.41999f);
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Série";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(338.6666f, 83.42002f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Emissão";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(592.6667f, 83.42003f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).Text = "Situação";
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(846.6667f, 83.42008f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(976.1239f, 58.41994f);
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).Text = "Pessoa";
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(1822.791f, 83.42008f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(346.6042f, 58.42001f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "XML Autorização";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(2169.396f, 83.42008f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(346.6042f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "XML Cancelamento";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(2516f, 83.42009f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Valor total";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Numero")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(214.3125f, 50f);
			((XRControl)xrLabel2).Text = "xrLabel2";
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Serie")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).LocationFloat = new PointFloat(214.3125f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(124.3542f, 50f);
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "xrLabel5";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(338.6667f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(254f, 50f);
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "xrLabel3";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel17).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Situacao")
			});
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).LocationFloat = new PointFloat(592.6667f, 0f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(254f, 50f);
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "xrLabel17";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Pessoa")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(846.6667f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(976.1249f, 50f);
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ValorTotal", "{0:n2}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(2516f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(254f, 50f);
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "xrLabel6";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)4;
			xmlAutorizado.Expression = "Iif([Situacao] == 'Concluído', Iif([PossuiXmlAutorizado] == True, 'Ok', 'Não encontrado'), '-')";
			xmlAutorizado.Name = "xmlAutorizado";
			xmlCancelado.Expression = "Iif([Situacao] == 'Cancelado', Iif([PossuiXmlCancelado] == True, 'Ok', 'Não encontrado'), '-')";
			xmlCancelado.Name = "xmlCancelado";
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "xmlAutorizado")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1822.792f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(2, 2, 0, 0, 96f);
			((XRControl)xrLabel14).SizeF = new SizeF(346.6036f, 50f);
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "xrLabel14";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "xmlCancelado")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).LocationFloat = new PointFloat(2169.396f, 0f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(2, 2, 0, 0, 96f);
			((XRControl)xrLabel15).SizeF = new SizeF(346.6042f, 50f);
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "xrLabel15";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)2;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[2] { xmlAutorizado, xmlCancelado });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Landscape = true;
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2100;
			((XtraReport)this).PageWidth = 2770;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.GeradorArquivosContabilidade.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GeradorArquivosContabilidade.Properties.Resources", typeof(Resources).Assembly);
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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Auxiliares.GestaoPainelContabil;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatContabil;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatContabil.Classes;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatContabil.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GestaoPainelContabeis.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoPainelContabeis")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoPainelContabeis")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoPainelContabeis
{
	[Module(ModuleName = "GestaoPainelContabilModulo", OnDemand = true)]
	public class GestaoPainelContabilModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoPainelContabilModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoPainelContabeisView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoPainelContabilFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoPainelContabeis.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoPainelContabeis.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.GestaoPainelContabeis.Views
{
	public class GestaoPainelContabilFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoPainelContabilFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoPainelContabeis;component/views/gestaopainelcontabilfichaview.xaml", UriKind.Relative);
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
	public class GestaoPainelContabeisView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoPainelContabeisView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoPainelContabeis;component/views/gestaopainelcontabeisview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoPainelContabeis.Views.GestaoPainelContabilFichaViewDetalhes
{
	public class EnviarRelatorioView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal ButtonInfo ParameterArquivo;

		private bool _contentLoaded;

		public EnviarRelatorioView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoPainelContabeis;component/views/gestaopainelcontabilfichaviewdetalhes/enviarrelatorioview.xaml", UriKind.Relative);
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
				ParameterArquivo = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class EnviarArquivoFiscalView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal ButtonInfo ParameterArquivo;

		private bool _contentLoaded;

		public EnviarArquivoFiscalView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoPainelContabeis;component/views/gestaopainelcontabilfichaviewdetalhes/enviararquivofiscalview.xaml", UriKind.Relative);
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
				ParameterArquivo = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class SincronizacaoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public SincronizacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoPainelContabeis;component/views/gestaopainelcontabilfichaviewdetalhes/sincronizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoPainelContabeis.ViewModels
{
	public class EnviarArquivoFiscalAbaViewModel : AbaAuxiliarViewModelBase
	{
		public EnviarArquivoFiscalAbaViewModel(IViewModelDtoBase viewModel)
			: base("ARQUIVOS FISCAIS", "Sintegra/Sped Fiscal/EFD", viewModel)
		{
		}
	}
	public class EnviarArquivoFiscalViewModel : ViewModelDtoBase<string>
	{
		private readonly Action _obterHistoricoArquivos;

		public string NomeArquivo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeArquivo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeArquivo), value);
			}
		}

		public DateTime DataArquivo { get; set; }

		public IEnumerable<ArquivoDigisatContabilDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ArquivoDigisatContabilDto>>((Expression<Func<IEnumerable<ArquivoDigisatContabilDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ArquivoDigisatContabilDto>>((Expression<Func<IEnumerable<ArquivoDigisatContabilDto>>>)(() => Itens), value);
			}
		}

		public override bool Valido => true;

		public ICommand SelecionarArquivoCommand { get; set; }

		public ICommand EnviarArquivoCommand { get; set; }

		public EnviarArquivoFiscalViewModel(Action obterHistoricoArquivos)
			: base((string)null)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			_obterHistoricoArquivos = obterHistoricoArquivos;
			SelecionarArquivoCommand = (ICommand)new DelegateCommand((Action)SelecionarArquivo);
			EnviarArquivoCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarArquivo, (Func<bool>)PodeEnviarArquivo, (bool?)null);
		}

		private void SelecionarArquivo()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione um arquivo para enviar",
				Filter = "Arquivos fiscais (*.txt;*.fs;*.csv)|*.txt;*.fs;*.csv",
				InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal"),
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				NomeArquivo = string.Empty;
				return;
			}
			NomeArquivo = openFileDialog.FileName;
			FileInfo fileInfo = new FileInfo(NomeArquivo);
			DataArquivo = fileInfo.LastWriteTime;
		}

		private async Task EnviarArquivo()
		{
			if (HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Geral.TokenDigisatContabil.Valor) || HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Geral.EmpresaRelacionadaDigisatContabil.Valor))
			{
				DialogoFactory.MostrarDialogoConfirma("Credenciais não informadas!\nPara configurar acesse Auxiliares > Configurações > Geral > Portal do contador", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			if (!NomeArquivo.Contains("Sintegra") && !NomeArquivo.Contains("SpedFiscal") && !NomeArquivo.Contains("EfdContribuicao") && !NomeArquivo.Contains("Fortes") && !NomeArquivo.Contains("Alterdata") && !NomeArquivo.Contains("Dominio") && !NomeArquivo.Contains("EFPH") && !NomeArquivo.Contains("MovimentosContas") && !NomeArquivo.Contains("Questor"))
			{
				DialogoFactory.MostrarDialogoConfirma("Para que possamos identificar o arquivo, o nome do mesmo deve conter uma das seguintes palavras: \n'Sintegra', 'SpedFiscal', 'EfdContribuicao',, 'Fortes', 'Alterdata', 'Dominio', 'EFPH', 'MovimentosContas' ou 'Questor'", false, System.Windows.HorizontalAlignment.Center);
				NomeArquivo = string.Empty;
				return;
			}
			string[] array = File.ReadAllLines(NomeArquivo, Encoding.GetEncoding("iso-8859-1"));
			await EnviarArquivosDigisatContabilService.EnviarArquivosAsync(DataArquivo, NomeArquivo, (IEnumerable<string>)array, default(DateTime), default(DateTime)).ContinueWith(delegate(Task task)
			{
				NomeArquivo = string.Empty;
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)_obterHistoricoArquivos.Invoke);
				}
			});
		}

		private bool PodeEnviarArquivo()
		{
			return !HelperString.IsNullOrEmpty(NomeArquivo);
		}

		public void SetarResultado(IEnumerable<ArquivoDigisatContabilDto> objetos)
		{
			Itens = objetos.Where((ArquivoDigisatContabilDto x) => (int)x.Tipo != 3 && (int)x.Tipo != 10);
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class EnviarRelatorioAbaViewModel : AbaAuxiliarViewModelBase
	{
		public EnviarRelatorioAbaViewModel(IViewModelDtoBase viewModel)
			: base("RELATÓRIOS", "Envio de relatórios", viewModel)
		{
		}
	}
	public class EnviarRelatorioViewModel : ViewModelDtoBase<string>
	{
		private readonly Action<ArquivoDigisatContabilDto> _salvarHistoricoRelatorio;

		private readonly Action _obterHistoricoRelatorios;

		public string NomeArquivo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeArquivo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeArquivo), value);
			}
		}

		public DateTime DataArquivo { get; set; }

		public IEnumerable<ArquivoDigisatContabilDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ArquivoDigisatContabilDto>>((Expression<Func<IEnumerable<ArquivoDigisatContabilDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ArquivoDigisatContabilDto>>((Expression<Func<IEnumerable<ArquivoDigisatContabilDto>>>)(() => Itens), value);
			}
		}

		public override bool Valido => true;

		public ICommand SelecionarArquivoCommand { get; set; }

		public ICommand EnviarArquivoCommand { get; set; }

		public EnviarRelatorioViewModel(Action<ArquivoDigisatContabilDto> salvarHistoricoRelatorio, Action obterHistoricoRelatorios)
			: base((string)null)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			_salvarHistoricoRelatorio = salvarHistoricoRelatorio;
			_obterHistoricoRelatorios = obterHistoricoRelatorios;
			SelecionarArquivoCommand = (ICommand)new DelegateCommand((Action)SelecionarArquivo);
			EnviarArquivoCommand = (ICommand)new AsyncCommand((Func<Task>)EnviarArquivo, (Func<bool>)PodeEnviarArquivo, (bool?)null);
		}

		private void SelecionarArquivo()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione um arquivo para enviar",
				Filter = "Arquivos (*.txt;*.xls;*.xlsx;*.pdf;*.png,*.csv)|*.txt;*.xls;*.xlsx;*.pdf;*.png;*.csv",
				InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal"),
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				NomeArquivo = string.Empty;
				return;
			}
			NomeArquivo = openFileDialog.FileName;
			FileInfo fileInfo = new FileInfo(NomeArquivo);
			DataArquivo = fileInfo.LastWriteTime;
		}

		private async Task EnviarArquivo()
		{
			await EnviarDigisatContabil().ContinueWith(delegate(Task task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)_obterHistoricoRelatorios.Invoke);
				}
			});
		}

		private async Task EnviarDigisatContabil()
		{
			if (HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Geral.TokenDigisatContabil.Valor) || HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Geral.EmpresaRelacionadaDigisatContabil.Valor))
			{
				throw new Exception("Credenciais não informadas!\nPara configurar acesse Auxiliares > Configurações > Geral > Portal do contador");
			}
			string valor = DadosSistema.ConfiguracaoDto.Geral.TokenDigisatContabil.Valor;
			string valor2 = DadosSistema.ConfiguracaoDto.Geral.EmpresaRelacionadaDigisatContabil.Valor;
			Guid guid = Guid.NewGuid();
			string text = Convert.ToBase64String(File.ReadAllBytes(NomeArquivo));
			try
			{
				LoaderProcessoService.MostrarLoader("Enviando relatório...");
				ApiDigisatContabilService val = new ApiDigisatContabilService(valor, DadosSistema.ObterProxy());
				RelatorioDigisatContabil val2 = ((BaseEmpresa<RelatorioDigisatContabil>)(object)((BaseEmpresa<RelatorioDigisatContabil>)(object)RelatorioDigisatContabil.Criar()).ComReferencia(guid.ToString())).ComEmpresaRelacionada(valor2).ComDataEmitido(DataArquivo).ComDescricao(Path.GetFileNameWithoutExtension(NomeArquivo) + Path.GetExtension(NomeArquivo))
					.ComFile(text);
				await val.Post<RetornoRelatorioDigisatContabil>(val2.EndPoint, (IObjetoDigisatContabil)(object)val2).ContinueWith(delegate(Task<RetornoRelatorioDigisatContabil> task)
				{
					//IL_0057: Unknown result type (might be due to invalid IL or missing references)
					//IL_005c: Unknown result type (might be due to invalid IL or missing references)
					//IL_0067: Unknown result type (might be due to invalid IL or missing references)
					//IL_006e: Unknown result type (might be due to invalid IL or missing references)
					//IL_008f: Unknown result type (might be due to invalid IL or missing references)
					//IL_009f: Expected O, but got Unknown
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
						NomeArquivo = string.Empty;
					}
					else
					{
						if (task.Result.status.Equals("205"))
						{
							DialogoFactory.MostrarDialogoConfirma(task.Result.limite_mensal, false, System.Windows.HorizontalAlignment.Center);
						}
						_salvarHistoricoRelatorio(new ArquivoDigisatContabilDto
						{
							Data = DateTime.Now,
							Tipo = (TipoArquivoPainelContabil)3,
							NomeArquivo = Path.GetFileNameWithoutExtension(NomeArquivo) + Path.GetExtension(NomeArquivo),
							NomeUsuario = DadosSistema.NomeUsuario
						});
						DialogoFactory.MostrarDialogoConfirma("Relatório enviado com sucesso!", false, System.Windows.HorizontalAlignment.Center);
						NomeArquivo = string.Empty;
					}
				});
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private bool PodeEnviarArquivo()
		{
			return !HelperString.IsNullOrEmpty(NomeArquivo);
		}

		public void SetarResultado(IEnumerable<ArquivoDigisatContabilDto> objetos)
		{
			Itens = objetos.Where((ArquivoDigisatContabilDto x) => (int)x.Tipo == 3 || (int)x.Tipo == 10);
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class GestaoPainelContabilFormulario : Formulario
	{
		private readonly Action<IEnumerable<ObjetoDigisatContabilDto>> _setarNaoSincronizado;

		private readonly Action<ArquivoDigisatContabilDto> _salvarHistoricoArquivo;

		private SincronizacaoViewModel _sincronizacaoViewModel;

		private SincronizacaoAbaViewModel _sincronizacaoAbaViewModel;

		public GestaoPainelContabilFormulario(Action<DateTime, DateTime> obterObjetos, Action obterHistoricoArquivos, Action<IEnumerable<ObjetoDigisatContabilDto>> setarNaoSincronizado, Action<ArquivoDigisatContabilDto> salvarHistoricoArquivo)
		{
			GestaoPainelContabilFormulario gestaoPainelContabilFormulario = this;
			_setarNaoSincronizado = setarNaoSincronizado;
			_salvarHistoricoArquivo = salvarHistoricoArquivo;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoPainelContabilFormulario.ProjetarAbas(obterObjetos, obterHistoricoArquivos);
			});
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		public bool PodeForcarSincronismo(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public void ForcarSincronismo(object obj)
		{
			List<ObjetoDigisatContabilDto> list = HelperIEnumerable.ToListEmptyWhenNull<ObjetoDigisatContabilDto>(_sincronizacaoViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum registro selecionado para sincronizar", false, System.Windows.HorizontalAlignment.Center);
			}
			else
			{
				_setarNaoSincronizado(list);
			}
		}

		private void ProjetarAbas(Action<DateTime, DateTime> obterObjetos, Action obterHistoricoArquivos)
		{
			_sincronizacaoViewModel = new SincronizacaoViewModel(obterObjetos);
			_sincronizacaoAbaViewModel = new SincronizacaoAbaViewModel((IViewModelDtoBase)(object)_sincronizacaoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[3]
			{
				(IAbaViewModelBase)_sincronizacaoAbaViewModel,
				(IAbaViewModelBase)new EnviarArquivoFiscalAbaViewModel((IViewModelDtoBase)(object)new EnviarArquivoFiscalViewModel(obterHistoricoArquivos)),
				(IAbaViewModelBase)new EnviarRelatorioAbaViewModel((IViewModelDtoBase)(object)new EnviarRelatorioViewModel(_salvarHistoricoArquivo, obterHistoricoArquivos))
			});
		}
	}
	public class GestaoPainelContabilFichaViewModel : FichaBase<ObjetoDigisatContabilDto, SincronizacaoViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<ObjetoDigisatContabilDto>> _objetos;

		private Task<IEnumerable<ArquivoDigisatContabilDto>> _arquivos;

		public ICommand ForcarSincronismoCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ForcarSincronismoCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ForcarSincronismoCommand), value);
			}
		}

		public GestaoPainelContabilFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
			: base(string.Empty)
		{
			_service = service;
			_executarProcessoService = executarProcessoService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return;
			}
			try
			{
				GestaoPainelContabilFormulario gestaoPainelContabilFormulario = new GestaoPainelContabilFormulario(ObterObjetos, ObterHistoricoArquivos, SetarNaoSincronizado, SalvarHistoricoArquivo);
				ForcarSincronismoCommand = (ICommand)new DelegateCommand<object>((Action<object>)gestaoPainelContabilFormulario.ForcarSincronismo, (Func<object, bool>)gestaoPainelContabilFormulario.PodeForcarSincronismo, (bool?)null);
				base.Formulario = (IFormulario)(object)gestaoPainelContabilFormulario;
				ThreadPrincipal.Executar((Action)ObterHistoricoArquivos);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
		}

		public void ObterObjetos(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoPainelContabilModuleService, Task<IEnumerable<ObjetoDigisatContabilDto>>>((Func<IGestaoPainelContabilModuleService, Task<IEnumerable<ObjetoDigisatContabilDto>>>)((IGestaoPainelContabilModuleService service) => _objetos = service.PesquisarPorDataInclusao(dataInicial, dataFinal)));
			}, (Action<object>)RetornoObjetos);
		}

		private void RetornoObjetos(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_objetos))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoPainelContabilFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<SincronizacaoViewModel>((object)aba.ViewModel, (Action<SincronizacaoViewModel>)delegate(SincronizacaoViewModel x)
					{
						x.SetarResultado(_objetos.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public void ObterHistoricoArquivos()
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoPainelContabilModuleService, Task<IEnumerable<ArquivoDigisatContabilDto>>>((Func<IGestaoPainelContabilModuleService, Task<IEnumerable<ArquivoDigisatContabilDto>>>)((IGestaoPainelContabilModuleService service) => _arquivos = service.ObterHistoricoArquivos()));
			}, (Action<object>)RetornoArquivos);
		}

		private void RetornoArquivos(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_arquivos))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoPainelContabilFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<EnviarArquivoFiscalViewModel>((object)aba.ViewModel, (Action<EnviarArquivoFiscalViewModel>)delegate(EnviarArquivoFiscalViewModel x)
					{
						x.SetarResultado(_arquivos.Result);
					});
					HelperObject.ToDo<EnviarRelatorioViewModel>((object)aba.ViewModel, (Action<EnviarRelatorioViewModel>)delegate(EnviarRelatorioViewModel x)
					{
						x.SetarResultado(_arquivos.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public void SetarNaoSincronizado(IEnumerable<ObjetoDigisatContabilDto> objetos)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoPainelContabilModuleService>((Action<IGestaoPainelContabilModuleService>)delegate(IGestaoPainelContabilModuleService service)
				{
					service.SetarNaoSincronizado(objetos.Select((ObjetoDigisatContabilDto x) => x.Referencia));
				});
			}, (Action<object>)delegate
			{
				DialogoFactory.MostrarDialogoConfirma("Os registros serão sincronizados novamente em breve!", false, System.Windows.HorizontalAlignment.Center);
			});
		}

		public void SalvarHistoricoArquivo(ArquivoDigisatContabilDto arquivoDigisatContabilDto)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoPainelContabilModuleService>((Action<IGestaoPainelContabilModuleService>)delegate(IGestaoPainelContabilModuleService service)
				{
					service.SalvarHistoricoArquivo(arquivoDigisatContabilDto);
				});
			}, (Action<object>)delegate
			{
			});
		}
	}
	public class GestaoPainelContabeisViewModel : ModuloBase
	{
		public GestaoPainelContabeisViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoPainelContabeisRegion, "GestaoPainelContabilFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class SincronizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SincronizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("SINCRONIZAÇÃO", "Resumo das sincronizações", viewModel)
		{
		}
	}
	public class SincronizacaoViewModel : ViewModelDtoBase<IEnumerable<ObjetoDigisatContabilDto>>
	{
		private readonly Action<DateTime, DateTime> _obterObjetos;

		public DateTime DataInicial
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial), value);
			}
		}

		public DateTime DataFinal
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinal));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinal), value);
			}
		}

		public IEnumerable<ObjetoDigisatContabilDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ObjetoDigisatContabilDto>>((Expression<Func<IEnumerable<ObjetoDigisatContabilDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ObjetoDigisatContabilDto>>((Expression<Func<IEnumerable<ObjetoDigisatContabilDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoObjetos";

		public ICommand PesquisarCommand { get; set; }

		public SincronizacaoViewModel()
		{
		}

		public SincronizacaoViewModel(Action<DateTime, DateTime> obterObjetos)
			: base((IEnumerable<ObjetoDigisatContabilDto>)null)
		{
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			_obterObjetos = obterObjetos;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
		}

		private void Pesquisar()
		{
			_obterObjetos(DataInicial, DataFinal);
		}

		public IEnumerable<ObjetoDigisatContabilDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ObjetoDigisatContabilDto>();
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

		public void SetarResultado(IEnumerable<ObjetoDigisatContabilDto> objetos)
		{
			Itens = objetos;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}

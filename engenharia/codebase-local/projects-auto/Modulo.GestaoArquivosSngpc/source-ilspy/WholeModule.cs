using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
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
using System.Xml;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Auxiliares.Sngpcs;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Farmacia.Sngpc;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiSngpc;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GestaoArquivosSngpc.Views;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoArquivosSngpc")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoArquivosSngpc")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoArquivosSngpc
{
	[Module(ModuleName = "GestaoArquivoSngpcModulo", OnDemand = true)]
	public class GestaoArquivoSngpcModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoArquivoSngpcModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoArquivosSngpcView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoArquivoSngpcFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoArquivosSngpc.Views
{
	public class GestaoArquivoSngpcFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoArquivoSngpcFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoArquivosSngpc;component/views/gestaoarquivosngpcfichaview.xaml", UriKind.Relative);
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
	public class GestaoArquivosSngpcView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoArquivosSngpcView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoArquivosSngpc;component/views/gestaoarquivossngpcview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoArquivosSngpc.Views.GestaoArquivoSngpcFichaViewDetalhes
{
	public class SincronizacaoView : UserControl, IComponentConnector
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
				Uri resourceLocator = new Uri("/Modulo.GestaoArquivosSngpc;component/views/gestaoarquivosngpcfichaviewdetalhes/sincronizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoArquivosSngpc.ViewModels
{
	public class GestaoArquivoSngpcFichaViewModel : FichaBase<ArquivoSngpcDto, SincronizacaoViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<ArquivoSngpcDto>> _objetos;

		private DateTime _dataInicial;

		private DateTime _dataFinal;

		public ICommand ConsultarArquivosCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarArquivosCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => ConsultarArquivosCommand), value);
			}
		}

		public GestaoArquivoSngpcFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
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
				GestaoArquivoSngpcFormulario gestaoArquivoSngpcFormulario = new GestaoArquivoSngpcFormulario(ObterObjetos, ConsultarArquivos);
				ConsultarArquivosCommand = (ICommand)new DelegateCommand<object>((Action<object>)gestaoArquivoSngpcFormulario.ConsultarArquivos, (Func<object, bool>)gestaoArquivoSngpcFormulario.PodeConsultarArquivos);
				base.Formulario = (IFormulario)(object)gestaoArquivoSngpcFormulario;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
			finally
			{
				base.DisableConfirmOnClose();
			}
		}

		public void ObterObjetos(DateTime dataInicial, DateTime dataFinal)
		{
			_dataInicial = dataInicial;
			_dataFinal = dataFinal;
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<ISngpcModuleService, Task<IEnumerable<ArquivoSngpcDto>>>((Func<ISngpcModuleService, Task<IEnumerable<ArquivoSngpcDto>>>)((ISngpcModuleService service) => _objetos = service.PesquisarPorDataTransmissao(dataInicial, dataFinal)));
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
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoArquivoSngpcFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<SincronizacaoViewModel>((object)aba.ViewModel, (Action<SincronizacaoViewModel>)delegate(SincronizacaoViewModel x)
					{
						x.SetarResultado(_objetos.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void ConsultarArquivos(IEnumerable<ArquivoSngpcDto> objetos)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Expected O, but got Unknown
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Expected O, but got Unknown
			if (!objetos.Any())
			{
				return;
			}
			ApiSngpcService val = new ApiSngpcService();
			string text;
			try
			{
				text = val.ObterToken(DadosSistema.ConfiguracaoDto.Saida.EmailSngpcAnvisa.Valor, DadosSistema.ConfiguracaoDto.Saida.SenhaSngpcAnvisa.Valor);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
				return;
			}
			List<string> list = new List<string>();
			foreach (ArquivoSngpcDto objeto in objetos)
			{
				string text2;
				try
				{
					text2 = val.ConsultaDadosArquivoXml(text, DadosSistema.ConfiguracaoDto.Saida.EmailSngpcAnvisa.Valor, HelperString.RemoveCaracteresAlfanumerico(DadosSistema.EmpresaLogada.CpfCnpj), objeto.CodigoHash);
				}
				catch (Exception ex2)
				{
					list.Add($"Arquivo {objeto.NomeArquivo}: {ex2.Message}");
					continue;
				}
				if (text2.Contains("transmissaoSNGPC"))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text2);
					TransmissaoSNGPC val2 = HelperXml.StringXmlToObject<TransmissaoSNGPC>(xmlDocument.OuterXml);
					if (HelperObject.IsNotNull((object)val2))
					{
						ArquivoSngpcDto dto = new ArquivoSngpcDto
						{
							CodigoHash = HelperString.ToStringEmptyWhenNull(val2.Cabecalho.CodigoHash),
							NomeArquivo = HelperString.ToStringEmptyWhenNull(objeto.NomeArquivo),
							DataTransmissao = HelperDate.TryToDateTime((object)val2.Cabecalho.DataTransmissao),
							DataValidacao = HelperDate.TryToDateTime((object)val2.Cabecalho.DataValidacao),
							InicioReferencia = HelperDate.TryToDateTime((object)val2.Cabecalho.InicioReferencia),
							FimReferencia = HelperDate.TryToDateTime((object)val2.Cabecalho.FimReferencia),
							MensagemValidacao = HelperString.ToStringEmptyWhenNull(val2.Cabecalho.MensagemValidacao)
						};
						DadosSistema.GetService().Canal<ISngpcModuleService>((Action<ISngpcModuleService>)delegate(ISngpcModuleService service)
						{
							service.SalvarArquivoSngpc(dto);
						});
					}
					list.Add($"Arquivo {objeto.NomeArquivo}: consultado com sucesso.");
				}
				else
				{
					list.Add($"Arquivo {objeto.NomeArquivo}: {text2}");
				}
			}
			DialogoFactory.MostrarDialogoConfirma(HelperString.LinhasParaTexto(list.ToArray()), false, HorizontalAlignment.Center);
			ObterObjetos(_dataInicial, _dataFinal);
		}
	}
	public class GestaoArquivoSngpcFormulario : Formulario
	{
		private readonly Action<IEnumerable<ArquivoSngpcDto>> _consultarArquivos;

		private SincronizacaoViewModel _sincronizacaoViewModel;

		private SincronizacaoAbaViewModel _sincronizacaoAbaViewModel;

		public GestaoArquivoSngpcFormulario(Action<DateTime, DateTime> obterObjetos, Action<IEnumerable<ArquivoSngpcDto>> consultarArquivos)
		{
			GestaoArquivoSngpcFormulario gestaoArquivoSngpcFormulario = this;
			_consultarArquivos = consultarArquivos;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoArquivoSngpcFormulario.ProjetarAbas(obterObjetos);
			});
		}

		public bool PodeConsultarArquivos(object obj)
		{
			return HelperObject.Is<SincronizacaoViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
		}

		public void ConsultarArquivos(object obj)
		{
			List<ArquivoSngpcDto> list = HelperIEnumerable.ToListEmptyWhenNull<ArquivoSngpcDto>(_sincronizacaoViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum registro selecionado para consultar o resultado da validação", false, HorizontalAlignment.Center);
			}
			else
			{
				_consultarArquivos(list);
			}
		}

		private void ProjetarAbas(Action<DateTime, DateTime> obterObjetos)
		{
			_sincronizacaoViewModel = new SincronizacaoViewModel(obterObjetos);
			_sincronizacaoAbaViewModel = new SincronizacaoAbaViewModel((IViewModelDtoBase)(object)_sincronizacaoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)_sincronizacaoAbaViewModel });
		}
	}
	public class GestaoArquivosSngpcViewModel : ModuloBase
	{
		public GestaoArquivosSngpcViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoArquivosSngpcRegion, "GestaoArquivoSngpcFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class SincronizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public SincronizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("TRANSMISSÕES", "Resumo das validações", viewModel)
		{
		}
	}
	public class SincronizacaoViewModel : ViewModelDtoBase<IEnumerable<ArquivoSngpcDto>>
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

		public IEnumerable<ArquivoSngpcDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ArquivoSngpcDto>>((Expression<Func<IEnumerable<ArquivoSngpcDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ArquivoSngpcDto>>((Expression<Func<IEnumerable<ArquivoSngpcDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoObjetos";

		public ICommand PesquisarCommand { get; set; }

		public SincronizacaoViewModel()
		{
		}

		public SincronizacaoViewModel(Action<DateTime, DateTime> obterObjetos)
			: base((IEnumerable<ArquivoSngpcDto>)null)
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

		public IEnumerable<ArquivoSngpcDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<ArquivoSngpcDto>();
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

		public void SetarResultado(IEnumerable<ArquivoSngpcDto> objetos)
		{
			Itens = objetos;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.GestaoArquivosSngpc.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoArquivosSngpc.Properties.Resources", typeof(Resources).Assembly);
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
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default => defaultInstance;
	}
}

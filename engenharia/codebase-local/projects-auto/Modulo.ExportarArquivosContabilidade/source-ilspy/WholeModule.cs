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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.ArquivosContabilidade;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.ArquivoContabilidade.ArquivoCsv.Classes;
using DigisatServer.Infrastructure.Crosscutting.ArquivoContabilidade.FortesFiscal.Types;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.Views;
using Otus.Modulo.ExportarArquivosContabilidade.ViewModels.LeiautesArquivo;
using Otus.Modulo.ExportarArquivosContabilidade.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.ExportarArquivosContabilidade")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.ExportarArquivosContabilidade")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.ExportarArquivosContabilidade
{
	[Module(ModuleName = "ExportarArquivoContabilidadeModulo", OnDemand = true)]
	public class ExportarArquivoContabilidadeModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ExportarArquivoContabilidadeModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ExportarArquivosContabilidadeView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ExportarArquivoContabilidadeFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.ExportarArquivosContabilidade.Views
{
	public class ExportarArquivoContabilidadeFichaView : UserControl, IComponentConnector
	{
		internal FichaBaseExecutar FichaBase;

		private bool _contentLoaded;

		public ExportarArquivoContabilidadeFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.ExportarArquivosContabilidade;component/views/exportararquivocontabilidadefichaview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBaseExecutar)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class ExportarArquivosContabilidadeView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ExportarArquivosContabilidadeView()
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
				Uri resourceLocator = new Uri("/Modulo.ExportarArquivosContabilidade;component/views/exportararquivoscontabilidadeview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ExportarArquivosContabilidade.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.ExportarArquivosContabilidade.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.ExportarArquivosContabilidade.ViewModels
{
	public class ExportarArquivoContabilidadeViewModel : ViewModelDtoBase<ParametrosArquivoContabilidadeDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public LeiauteArquivoComboBoxViewModel LeiauteArquivoComboBox
		{
			get
			{
				return ((BindableBase)this).GetProperty<LeiauteArquivoComboBoxViewModel>((Expression<Func<LeiauteArquivoComboBoxViewModel>>)(() => LeiauteArquivoComboBox));
			}
			set
			{
				((BindableBase)this).SetProperty<LeiauteArquivoComboBoxViewModel>((Expression<Func<LeiauteArquivoComboBoxViewModel>>)(() => LeiauteArquivoComboBox), value);
			}
		}

		public ExportarArquivoContabilidadeViewModel()
		{
		}

		public ExportarArquivoContabilidadeViewModel(ParametrosArquivoContabilidadeDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			LeiauteArquivoComboBox = new LeiauteArquivoComboBoxViewModel(base.Dto);
			((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).Posicionar((Expression<Func<LeiauteArquivoViewModel, bool>>)((LeiauteArquivoViewModel x) => x.Id.Equals(((ViewModelDtoBase<ParametrosArquivoContabilidadeDto>)this).Dto.CodigoArquivoContabilidadePosicionado)));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto dto2 = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (dto2 != null)
			{
				((EntityBaseDto)dto2).Id = ((EntityBaseDto)base.Dto).Id;
				dto2.CodigoArquivoContabilidadePosicionado = ((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).ItemSelecionado.Id;
				dto2.DataInicial = ((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).ItemSelecionado.DataInicial;
				dto2.DataFinal = ((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).ItemSelecionado.DataFinal;
				dto2.Fortes = (FortesFiscalDto)new FortesFiscalDtoNull();
				dto2.Questor = (QuestorContabilDto)new QuestorContabilDtoNull();
				dto2.Dominio = (DominioDto)new DominioDtoNull();
				dto2.Efph = (EfphDto)new EfphDtoNull();
				dto2.Siescon = (SiesconDto)new SiesconDtoNull();
				((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).ItemSelecionado.AtualizarDto(ref dto2);
				base.AtualizarDto<ParametrosArquivoContabilidadeDto>(ref dto2);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ExportarArquivoContabilidadeFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Exportar arquivo para contabilidade";
		}

		public string ToTituloFicha()
		{
			return "Exportar arquivo para contabilidade";
		}

		public override void Dispose()
		{
			foreach (LeiauteArquivoViewModel iten in ((Lista<LeiauteArquivoViewModel>)LeiauteArquivoComboBox).Itens)
			{
				iten.Dispose();
			}
			((ViewModelValidationBase)LeiauteArquivoComboBox).Dispose();
		}
	}
	public class ExportarArquivoContabilidadeAbaViewModel : AbaPrincipalViewModelBase
	{
		public ExportarArquivoContabilidadeAbaViewModel(IViewModelPrincipal viewModel)
			: base("Arquivo contabilidade", "Dados gerais", viewModel)
		{
		}
	}
	public class ExportarArquivoContabilidadeFichaViewModel : FichaBaseGeradorArquivo<ParametrosArquivoContabilidadeDto, ExportarArquivoContabilidadeViewModel, IGeradorArquivoContabilidadeModuleService>
	{
		protected override string DiretorioArquivo => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fiscal");

		protected override string NomeArquivo => ((Lista<LeiauteArquivoViewModel>)(((Formulario)(((FichaBase<ParametrosArquivoContabilidadeDto, ExportarArquivoContabilidadeViewModel>)(object)this).Formulario as ExportarArquivoContabilidadeFormulario)).ViewModelAbaSelecionada as ExportarArquivoContabilidadeViewModel).LeiauteArquivoComboBox).ItemSelecionado.NomeArquivo;

		protected override DateTime PeriodoInicio => base.Dto.DataInicial;

		protected override DateTime PeriodoFim => base.Dto.DataFinal;

		protected override string TipoArquivoCompartilhando
		{
			get
			{
				ExportarArquivoContabilidadeViewModel exportarArquivoContabilidadeViewModel = ((Formulario)(((FichaBase<ParametrosArquivoContabilidadeDto, ExportarArquivoContabilidadeViewModel>)(object)this).Formulario as ExportarArquivoContabilidadeFormulario)).ViewModelAbaSelecionada as ExportarArquivoContabilidadeViewModel;
				return $"Arquivo fiscal contabilidade {((Lista<LeiauteArquivoViewModel>)exportarArquivoContabilidadeViewModel.LeiauteArquivoComboBox).ItemSelecionado.TipoArquivoWhatsApp}";
			}
		}

		public ExportarArquivoContabilidadeFichaViewModel(IExecutarProcessoService executarProcessoService, IWService service)
			: base(string.Empty, executarProcessoService, service)
		{
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			ParametrosArquivoContabilidadeDto dto = base.Service.Canal<IGeradorArquivoContabilidadeModuleService, ParametrosArquivoContabilidadeDto>((Func<IGeradorArquivoContabilidadeModuleService, ParametrosArquivoContabilidadeDto>)((IGeradorArquivoContabilidadeModuleService s) => s.ObterParametrosExportarArquivoDto()));
			base.Dto = dto;
			base.OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<ParametrosArquivoContabilidadeDto, ExportarArquivoContabilidadeViewModel>)(object)this).Formulario = (IFormulario)(object)new ExportarArquivoContabilidadeFormulario(base.Dto);
		}

		public override void Dispose()
		{
			base.ExecutarCommand = null;
			((FichaBase<ParametrosArquivoContabilidadeDto, ExportarArquivoContabilidadeViewModel>)(object)this).Dispose();
		}
	}
	public class ExportarArquivoContabilidadeFormulario : FormularioEditavel<ParametrosArquivoContabilidadeDto>
	{
		public ExportarArquivoContabilidadeFormulario(ParametrosArquivoContabilidadeDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new ExportarArquivoContabilidadeAbaViewModel((IViewModelPrincipal)(object)new ExportarArquivoContabilidadeViewModel(dto)) });
		}
	}
	public class ExportarArquivosContabilidadeViewModel : ModuloBase
	{
		public ExportarArquivosContabilidadeViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloExportarArquivoContabilidadeRegion, typeof(ExportarArquivoContabilidadeFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.ExportarArquivosContabilidade.ViewModels.LeiautesArquivo
{
	public class AlterdataContabilViewModel : LeiauteArquivoViewModel
	{
		public override string NomeArquivo => $"AlterdataContabil_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.txt";

		public override string TipoArquivoWhatsApp => "Alterdata Contábil";

		public AlterdataContabilViewModel(AlterdataContabilDto dto)
			: base(6, "Alterdata Contábil")
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.AlterdataContabil = new AlterdataContabilDto();
			}
		}

		public override void Dispose()
		{
		}
	}
	public class ArquivoCsvViewModel : LeiauteArquivoViewModel
	{
		public bool GerarCabecalho
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GerarCabecalho));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GerarCabecalho), value);
			}
		}

		public Lista<string> Separadores { get; set; }

		public Lista<PropriedadeCsv> PropriedadesDisponiveis { get; set; }

		public Lista<PropriedadeCsv> PropriedadesSelecionadas { get; set; }

		public override string NomeArquivo => $"MovimentosContas_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.csv";

		public override string TipoArquivoWhatsApp => "CSV - Movimentações das contas";

		public ICommand AdicionarCommand { get; set; }

		public ICommand RemoverCommand { get; set; }

		public ArquivoCsvViewModel(ArquivoCsvDto arquivoCsvDto)
			: base(1, "CSV - Movimentações das contas")
		{
			GerarCabecalho = arquivoCsvDto.GerarCabecalho;
			Separadores = new Lista<string>();
			Separadores.Adicionar((IEnumerable<string>)new List<string> { "    ;    ", "    ,   ", "    |   " });
			Separadores.Posicionar((Expression<Func<string, bool>>)((string x) => HelperString.LimparEspacos(x).Equals(arquivoCsvDto.Separador.ToString())));
			PropriedadesDisponiveis = new Lista<PropriedadeCsv>();
			PropriedadesDisponiveis.Adicionar(((RegistroCsv<MovimentoContaRegistroCsv>)(object)MovimentoContaRegistroCsv.Criar()).ObterPropriedades());
			PropriedadesDisponiveis.PosicionarNoPrimeiro();
			PropriedadesSelecionadas = new Lista<PropriedadeCsv>();
			PropriedadesSelecionadas.Adicionar(((IEnumerable<PropriedadeCsvDto>)arquivoCsvDto.PropriedadesSelecionadas.OrderBy((PropriedadeCsvDto x) => x.Posicao)).Select((Func<PropriedadeCsvDto, PropriedadeCsv>)((PropriedadeCsvDto x) => new PropriedadeCsv(x.Descricao, x.Identificador, x.Posicao))));
			PropriedadesSelecionadas.PosicionarNoPrimeiro();
			AdicionarCommand = (ICommand)new DelegateCommand<PropriedadeCsv>((Action<PropriedadeCsv>)PropriedadesSelecionadas.Adicionar, (Func<PropriedadeCsv, bool>)((PropriedadeCsv valor) => valor != null && !PropriedadesSelecionadas.Existe((Expression<Func<PropriedadeCsv, bool>>)((PropriedadeCsv x) => x.Identificador.Equals(valor.Identificador)))), (bool?)null);
			RemoverCommand = (ICommand)new DelegateCommand<PropriedadeCsv>((Action<PropriedadeCsv>)PropriedadesSelecionadas.Remover);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.ArquivoCsv = new ArquivoCsvDto
				{
					PropriedadesSelecionadas = ((IEnumerable<PropriedadeCsv>)PropriedadesSelecionadas.Itens).Select((Func<PropriedadeCsv, int, PropriedadeCsvDto>)((PropriedadeCsv propriedade, int i) => new PropriedadeCsvDto
					{
						Posicao = i,
						Descricao = propriedade.Descricao,
						Identificador = propriedade.Identificador
					})),
					Separador = char.Parse(HelperString.LimparEspacos(Separadores.ItemSelecionado)),
					GerarCabecalho = GerarCabecalho
				};
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Separadores).Dispose();
			((ViewModelValidationBase)PropriedadesDisponiveis).Dispose();
			((ViewModelValidationBase)PropriedadesSelecionadas).Dispose();
		}
	}
	public class DominioViewModel : LeiauteArquivoViewModel
	{
		public override string NomeArquivo => $"DominioSistemas_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.txt";

		public override string TipoArquivoWhatsApp => "Domínio Sistemas";

		public Lista<KeyValuePair<string, string>> TipoContribuicaoPisCofins { get; set; }

		public DominioViewModel(DominioDto dto)
			: base(5, "Domínio Sistemas")
		{
			TipoContribuicaoPisCofins = new Lista<KeyValuePair<string, string>>();
			TipoContribuicaoPisCofins.Adicionar(new KeyValuePair<string, string>("N", "Não cumulativo"));
			TipoContribuicaoPisCofins.Adicionar(new KeyValuePair<string, string>("C", "Cumulativo"));
			TipoContribuicaoPisCofins.Adicionar(new KeyValuePair<string, string>("S", "Sem incidência"));
			if (!string.IsNullOrEmpty(dto.TipoContribuicaoPisCofins))
			{
				TipoContribuicaoPisCofins.Posicionar((Expression<Func<KeyValuePair<string, string>, bool>>)((KeyValuePair<string, string> x) => x.Key.Equals(dto.TipoContribuicaoPisCofins)));
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Dominio = new DominioDto
				{
					TipoContribuicaoPisCofins = TipoContribuicaoPisCofins.ItemSelecionado.Key
				};
			}
		}

		public override void Dispose()
		{
		}
	}
	public class EfphViewModel : LeiauteArquivoViewModel
	{
		public override string NomeArquivo => $"EFPH_{HelperEnum.RetornarDescricao((Enum)(object)TiposArquivo.ItemSelecionado.Value)}_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.txt";

		public override string TipoArquivoWhatsApp => "EFPH - PH Softwares";

		public Lista<KeyValuePair<string, MotivoInventarioSpedFiscal>> MotivosInventario { get; protected set; }

		public Lista<KeyValuePair<string, TipoArquivoEfph>> TiposArquivo { get; protected set; }

		public DateTime Periodo
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => Periodo));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => Periodo), value);
			}
		}

		public string HistoricoIdentificadorEntrada
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => HistoricoIdentificadorEntrada));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => HistoricoIdentificadorEntrada), value);
			}
		}

		public string HistoricoIdentificadorSaida
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => HistoricoIdentificadorSaida));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => HistoricoIdentificadorSaida), value);
			}
		}

		public bool GerarInventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GerarInventario));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GerarInventario), value);
			}
		}

		public DateTime DataInventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInventario));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInventario), value);
			}
		}

		public bool CustoMedio
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => CustoMedio));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => CustoMedio), value);
			}
		}

		public bool Custo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Custo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Custo), value);
			}
		}

		public bool Venda
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Venda));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Venda), value);
			}
		}

		public ValorEscriturarInventario ValorEscriturarInventario
		{
			get
			{
				if (!Venda)
				{
					if (CustoMedio)
					{
						return (ValorEscriturarInventario)0;
					}
					return (ValorEscriturarInventario)1;
				}
				return (ValorEscriturarInventario)2;
			}
		}

		public EfphViewModel(EfphDto dto)
			: base(4, "EFPH - PH Softwares")
		{
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Invalid comparison between Unknown and I4
			Periodo = DateTime.Today.AddMonths(-1);
			HistoricoIdentificadorSaida = dto.HistoricoIdentificadorSaida;
			HistoricoIdentificadorEntrada = dto.HistoricoIdentificadorEntrada;
			MotivosInventario = new Lista<KeyValuePair<string, MotivoInventarioSpedFiscal>>();
			MotivosInventario.Adicionar((IEnumerable<KeyValuePair<string, MotivoInventarioSpedFiscal>>)HelperEnum.ObterParDeValores<MotivoInventarioSpedFiscal>());
			MotivosInventario.Posicionar((Expression<Func<KeyValuePair<string, MotivoInventarioSpedFiscal>, bool>>)((KeyValuePair<string, MotivoInventarioSpedFiscal> x) => (int)x.Value == 1));
			TiposArquivo = new Lista<KeyValuePair<string, TipoArquivoEfph>>();
			TiposArquivo.Adicionar((IEnumerable<KeyValuePair<string, TipoArquivoEfph>>)HelperEnum.ObterParDeValores<TipoArquivoEfph>());
			TiposArquivo.Posicionar((Expression<Func<KeyValuePair<string, TipoArquivoEfph>, bool>>)((KeyValuePair<string, TipoArquivoEfph> x) => (int)x.Value == (int)dto.TipoArquivo));
			ObservableExtensions.Subscribe<KeyValuePair<string, TipoArquivoEfph>>(TiposArquivo.WhenAlterouItemSelecionado, (Action<KeyValuePair<string, TipoArquivoEfph>>)delegate(KeyValuePair<string, TipoArquivoEfph> x)
			{
				//IL_0003: Unknown result type (might be due to invalid IL or missing references)
				//IL_0009: Invalid comparison between Unknown and I4
				GerarInventario = (int)x.Value == 2;
			});
			GerarInventario = (int)dto.TipoArquivo == 2;
			DataInventario = DateTime.Today;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				int month = Periodo.Month;
				int year = Periodo.Year;
				DateTime dataInicial = new DateTime(year, month, 1);
				DateTime dataFinal = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
				val.DataInicial = dataInicial;
				val.DataFinal = dataFinal;
				val.Efph = new EfphDto
				{
					HistoricoIdentificadorEntrada = HistoricoIdentificadorEntrada,
					HistoricoIdentificadorSaida = HistoricoIdentificadorSaida,
					GerarInventario = GerarInventario,
					DataInventario = DataInventario,
					MotivoInventario = MotivosInventario.ItemSelecionado.Value,
					ValorEscriturarInventario = ValorEscriturarInventario,
					TipoArquivo = TiposArquivo.ItemSelecionado.Value
				};
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)MotivosInventario).Dispose();
		}
	}
	public class FortesFiscalViewModel : LeiauteArquivoViewModel
	{
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

		public string CodigoEstabelecimento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CodigoEstabelecimento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CodigoEstabelecimento), value);
			}
		}

		public string NomeEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeEmpresa), value);
			}
		}

		public bool EstabelecimentoContribuinteIpi
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => EstabelecimentoContribuinteIpi));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => EstabelecimentoContribuinteIpi), value);
			}
		}

		public bool CalculaFecop
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => CalculaFecop));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => CalculaFecop), value);
			}
		}

		public string NumeroAIDF
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NumeroAIDF));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NumeroAIDF), value);
			}
		}

		public bool RegimeAliquotasEspecificas
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => RegimeAliquotasEspecificas));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => RegimeAliquotasEspecificas), value);
			}
		}

		public Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>> TiposRegimeTributario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>>((Expression<Func<Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>>>)(() => TiposRegimeTributario));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>>((Expression<Func<Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>>>)(() => TiposRegimeTributario), value);
			}
		}

		public bool GerarRegistroIiv
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GerarRegistroIiv));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GerarRegistroIiv), value);
			}
		}

		public DateTime DataInventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInventario));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInventario), value);
			}
		}

		public Lista<KeyValuePair<string, MotivoInventario>> MotivosInventario
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, MotivoInventario>>>((Expression<Func<Lista<KeyValuePair<string, MotivoInventario>>>>)(() => MotivosInventario));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, MotivoInventario>>>((Expression<Func<Lista<KeyValuePair<string, MotivoInventario>>>>)(() => MotivosInventario), value);
			}
		}

		public bool GerarSomenteMovimentacoesCompoeTotalFinanceiro
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => GerarSomenteMovimentacoesCompoeTotalFinanceiro));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => GerarSomenteMovimentacoesCompoeTotalFinanceiro), value);
			}
		}

		public Lista<KeyValuePair<string, TipoArquivoFortes>> TiposArquivo { get; protected set; }

		public Lista<KeyValuePair<string, ValorEscriturarInventario>> ValoresEscriturarInventario { get; protected set; }

		public Lista<KeyValuePair<string, GerarRegistroOutrosValoresEFormasPagamento>> OpcoesGerarRegistroOutrosValoresEFormasPagamento { get; protected set; }

		public override string NomeArquivo => $"FortesFiscal_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.fs";

		public override string TipoArquivoWhatsApp => "Fortes AC Fiscal";

		public FortesFiscalViewModel(FortesFiscalDto dto)
			: base(2, "Fortes AC Fiscal")
		{
			TiposRegimeTributario = new Lista<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>();
			TiposRegimeTributario.Adicionar((IEnumerable<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>>)HelperEnum.ObterParDeValores<RegimeTributacaoArquivoFortesFiscal>());
			TiposRegimeTributario.Posicionar((Expression<Func<KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal>, bool>>)((KeyValuePair<string, RegimeTributacaoArquivoFortesFiscal> x) => (int)x.Value == (int)dto.RegimeTributacao));
			Observacao = dto.Observacao;
			CodigoEstabelecimento = dto.CodigoEstabelecimento;
			NomeEmpresa = dto.NomeEmpresa;
			EstabelecimentoContribuinteIpi = dto.EstabelecimentoContribuinteIpi;
			CalculaFecop = dto.CalculaFecop;
			NumeroAIDF = dto.NumeroAIDF;
			GerarRegistroIiv = dto.GerarRegistroIiv;
			DataInventario = base.DataFinal;
			RegimeAliquotasEspecificas = dto.RegimeAliquotasEspecificas;
			MotivosInventario = new Lista<KeyValuePair<string, MotivoInventario>>();
			MotivosInventario.Adicionar((IEnumerable<KeyValuePair<string, MotivoInventario>>)HelperEnum.ObterParDeValores<MotivoInventario>());
			MotivosInventario.Posicionar((Expression<Func<KeyValuePair<string, MotivoInventario>, bool>>)((KeyValuePair<string, MotivoInventario> x) => ((object)x.Value).Equals((object)dto.MotivoInventario)));
			TiposArquivo = new Lista<KeyValuePair<string, TipoArquivoFortes>>();
			TiposArquivo.Adicionar((IEnumerable<KeyValuePair<string, TipoArquivoFortes>>)HelperEnum.ObterParDeValores<TipoArquivoFortes>());
			TiposArquivo.Posicionar((Expression<Func<KeyValuePair<string, TipoArquivoFortes>, bool>>)((KeyValuePair<string, TipoArquivoFortes> x) => ((object)x.Value).Equals((object)dto.TipoArquivo)));
			ValoresEscriturarInventario = new Lista<KeyValuePair<string, ValorEscriturarInventario>>();
			ValoresEscriturarInventario.Adicionar((IEnumerable<KeyValuePair<string, ValorEscriturarInventario>>)HelperEnum.ObterParDeValores<ValorEscriturarInventario>());
			ValoresEscriturarInventario.Posicionar((Expression<Func<KeyValuePair<string, ValorEscriturarInventario>, bool>>)((KeyValuePair<string, ValorEscriturarInventario> x) => ((object)x.Value).Equals((object)dto.ValorEscriturarInventario)));
			OpcoesGerarRegistroOutrosValoresEFormasPagamento = new Lista<KeyValuePair<string, GerarRegistroOutrosValoresEFormasPagamento>>();
			OpcoesGerarRegistroOutrosValoresEFormasPagamento.Adicionar((IEnumerable<KeyValuePair<string, GerarRegistroOutrosValoresEFormasPagamento>>)HelperEnum.ObterParDeValores<GerarRegistroOutrosValoresEFormasPagamento>());
			OpcoesGerarRegistroOutrosValoresEFormasPagamento.Posicionar((Expression<Func<KeyValuePair<string, GerarRegistroOutrosValoresEFormasPagamento>, bool>>)((KeyValuePair<string, GerarRegistroOutrosValoresEFormasPagamento> x) => ((object)x.Value).Equals((object)dto.GerarRegistroOutrosValoresEFormasPagamento)));
			GerarSomenteMovimentacoesCompoeTotalFinanceiro = dto.GerarSomenteMovimentacoesCompoeTotalFinanceiro;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Expected I4, but got Unknown
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Fortes = new FortesFiscalDto
				{
					CodigoEstabelecimento = HelperString.ToStringEmptyWhenNull(CodigoEstabelecimento),
					NomeEmpresa = HelperString.ToStringEmptyWhenNull(NomeEmpresa),
					Observacao = HelperString.ToStringEmptyWhenNull(Observacao),
					RegimeTributacao = TiposRegimeTributario.ItemSelecionado.Value,
					EstabelecimentoContribuinteIpi = EstabelecimentoContribuinteIpi,
					CalculaFecop = CalculaFecop,
					NumeroAIDF = HelperString.ToStringEmptyWhenNull(NumeroAIDF),
					GerarRegistroIiv = GerarRegistroIiv,
					MotivoInventario = (int)MotivosInventario.ItemSelecionado.Value,
					DataInventario = DataInventario,
					TipoArquivo = TiposArquivo.ItemSelecionado.Value,
					ValorEscriturarInventario = ValoresEscriturarInventario.ItemSelecionado.Value,
					RegimeAliquotasEspecificas = RegimeAliquotasEspecificas,
					GerarRegistroOutrosValoresEFormasPagamento = OpcoesGerarRegistroOutrosValoresEFormasPagamento.ItemSelecionado.Value,
					GerarSomenteMovimentacoesCompoeTotalFinanceiro = GerarSomenteMovimentacoesCompoeTotalFinanceiro
				};
			}
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)TiposRegimeTributario).Dispose();
			((ViewModelValidationBase)MotivosInventario).Dispose();
		}
	}
	public interface ILayoutArquivoViewModel
	{
		int Id { get; }

		string Descricao { get; }

		string NomeArquivo { get; }
	}
	public class LeiauteArquivoComboBoxViewModel : Lista<LeiauteArquivoViewModel>
	{
		public LeiauteArquivoComboBoxViewModel(ParametrosArquivoContabilidadeDto dto)
		{
			base.Adicionar((LeiauteArquivoViewModel)new ArquivoCsvViewModel(dto.ArquivoCsv));
			base.Adicionar((LeiauteArquivoViewModel)new FortesFiscalViewModel(dto.Fortes));
			base.Adicionar((LeiauteArquivoViewModel)new QuestorContabilViewModel(dto.Questor));
			base.Adicionar((LeiauteArquivoViewModel)new EfphViewModel(dto.Efph));
			base.Adicionar((LeiauteArquivoViewModel)new DominioViewModel(dto.Dominio));
			base.Adicionar((LeiauteArquivoViewModel)new AlterdataContabilViewModel(dto.AlterdataContabil));
			base.Adicionar((LeiauteArquivoViewModel)new SiesconViewModel(dto.Siescon));
			base.Ordenar((Expression<Func<LeiauteArquivoViewModel, object>>)((LeiauteArquivoViewModel x) => x.Descricao), (Expression<Func<LeiauteArquivoViewModel, object>>)null);
			base.PosicionarNoPrimeiro();
		}
	}
	public abstract class LeiauteArquivoViewModel : BindableBase, ILayoutArquivoViewModel, IDisposable
	{
		public int Id { get; }

		public string Descricao { get; }

		public abstract string NomeArquivo { get; }

		public abstract string TipoArquivoWhatsApp { get; }

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

		protected LeiauteArquivoViewModel(int id, string descricao)
		{
			Id = id;
			Descricao = descricao;
			int month = DateTime.Now.AddMonths(-1).Month;
			int year = DateTime.Now.AddMonths(-1).Year;
			DataInicial = new DateTime(year, month, 1);
			DataFinal = new DateTime(year, month, DateTime.DaysInMonth(year, month));
		}

		public abstract void AtualizarDto<T>(ref T dto);

		public abstract void Dispose();
	}
	public class QuestorContabilViewModel : LeiauteArquivoViewModel
	{
		public override string NomeArquivo => $"QuestorContabil_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.csv";

		public override string TipoArquivoWhatsApp => "Questor Contábil";

		public QuestorContabilViewModel(QuestorContabilDto dto)
			: base(3, "Questor Contábil")
		{
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Questor = new QuestorContabilDto();
			}
		}

		public override void Dispose()
		{
		}
	}
	public class SiesconViewModel : LeiauteArquivoViewModel
	{
		public string CodigoEstabelecimento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => CodigoEstabelecimento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => CodigoEstabelecimento), value);
			}
		}

		public Lista<KeyValuePair<string, TipoArquivoSiescon>> TiposArquivo { get; protected set; }

		public override string NomeArquivo => $"{TiposArquivo.ItemSelecionado.Value}_Siescon_{DadosSistema.EmpresaLogada.Nome}_{base.DataInicial:ddMMyyyy}_{base.DataFinal:ddMMyyyy}.txt";

		public override string TipoArquivoWhatsApp => "Siescon";

		public SiesconViewModel(SiesconDto dto)
			: base(7, "Siescon")
		{
			CodigoEstabelecimento = dto.CodigoEstabelecimento;
			TiposArquivo = new Lista<KeyValuePair<string, TipoArquivoSiescon>>();
			TiposArquivo.Adicionar((IEnumerable<KeyValuePair<string, TipoArquivoSiescon>>)HelperEnum.ObterParDeValores<TipoArquivoSiescon>());
			TiposArquivo.PosicionarNoUltimo();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Expected O, but got Unknown
			object obj = dto;
			ParametrosArquivoContabilidadeDto val = (ParametrosArquivoContabilidadeDto)((obj is ParametrosArquivoContabilidadeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Siescon = new SiesconDto
				{
					CodigoEstabelecimento = HelperString.ToStringEmptyWhenNull(CodigoEstabelecimento),
					TipoArquivo = TiposArquivo.ItemSelecionado.Value
				};
			}
		}

		public override void Dispose()
		{
		}
	}
}

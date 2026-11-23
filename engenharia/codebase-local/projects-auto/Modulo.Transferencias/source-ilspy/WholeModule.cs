using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
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
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.Transferencias;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesTransferencias;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Transferencias.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Transferencias")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Transferencias")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Transferencias
{
	[Module(ModuleName = "TransferenciaModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ContaModulo")]
	public class TransferenciaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public TransferenciaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<TransferenciasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<TransferenciaListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<TransferenciaFichaAdicionarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Transferencias.Views
{
	public class TransferenciaFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public TransferenciaFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Transferencias;component/views/transferenciafichaadicionarview.xaml", UriKind.Relative);
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
	public class TransferenciaListaView : UserControl, IComponentConnector
	{
		internal KeyBinding kbdExibir;

		private bool _contentLoaded;

		public TransferenciaListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Transferencias;component/views/transferencialistaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				kbdExibir = (KeyBinding)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class TransferenciasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public TransferenciasView()
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
				Uri resourceLocator = new Uri("/Modulo.Transferencias;component/views/transferenciasview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Transferencias.Views.TrasferenciaFichaViewDetalhes
{
	public class TransferenciaView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterEmpresa;

		private bool _contentLoaded;

		public TransferenciaView()
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
				Uri resourceLocator = new Uri("/Modulo.Transferencias;component/views/trasferenciafichaviewdetalhes/transferenciaview.xaml", UriKind.Relative);
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
				ParameterEmpresa = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.Transferencias.ViewModels
{
	public class TransferenciaAbaViewModel : AbaPrincipalViewModelBase
	{
		public TransferenciaAbaViewModel(IViewModelPrincipal viewModel)
			: base("Transferência", "Origem/Destino, Valor", viewModel)
		{
		}
	}
	public class TransferenciaFichaAdicionarViewModel : FichaBaseAdicionar<TransferenciaDto, TransferenciaViewModel, ITransferenciaModuleService>
	{
		public TransferenciaFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class TransferenciaFormulario : FormularioEntity<TransferenciaDto>
	{
		public TransferenciaFormulario(TransferenciaDto dto)
			: base(dto)
		{
			CriarAbas();
		}

		private void CriarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new TransferenciaAbaViewModel((IViewModelPrincipal)(object)new TransferenciaViewModel(base.Dto)) });
		}
	}
	public class TransferenciaListaViewModel : ListaReferencia<TransferenciaDto, TransferenciaViewModel, ITransferenciaModuleService, Transferencia>
	{
		public bool PodeEstornar
		{
			get
			{
				if (!((ListaSelecaoBase<TransferenciaDto, TransferenciaViewModel, Transferencia>)(object)this).Sessao.VerificarPermissao<Transferencia, PermitirEstornar>())
				{
					return false;
				}
				if (HelperObject.IsNull((object)((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens) || !((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens.SelecionadoValido)
				{
					return false;
				}
				return HelperObject.IsNot<Estornado>((object)Situacoes.ObterPorCodigo(((ViewModelDtoBase<TransferenciaDto>)((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo));
			}
		}

		public bool EstornarVisivel => PodeEstornar;

		public ICommand EstornarCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public TransferenciaListaViewModel(ISessao sessao, IWService service, IEntityAtivos<TransferenciaDto, ITransferenciaModuleService> ativos)
			: base(sessao, typeof(TransferenciaFichaAdicionarView).Name, "", service, new IEntityBase<TransferenciaDto, ITransferenciaModuleService>[1] { (IEntityBase<TransferenciaDto, ITransferenciaModuleService>)(object)ativos })
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			EstornarCommand = (ICommand)new DelegateCommand((Action)Estornar, (Func<bool>)(() => PodeEstornar), (bool?)null);
			((BindableBase)((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens).PropertyChanged += ((ListaManutencaoBase<TransferenciaDto, TransferenciaViewModel, Transferencia>)(object)this).SelecionadoOnPropertyChanged;
		}

		private void Visualizar()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)86);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private bool PodeVisualizar()
		{
			if (((ListaSelecaoBase<TransferenciaDto, TransferenciaViewModel, Transferencia>)(object)this).Sessao.VerificarPermissao<Transferencia, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[3]
			{
				typeof(Gerencial),
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<TransferenciaDto, TransferenciaViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private async void Estornar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Confirmar estorno do lançamento selecionado?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				TransferenciaDto dto = default(TransferenciaDto);
				_ = dto;
				dto = await ((ListaReferenciaBase<TransferenciaDto, TransferenciaViewModel, ITransferenciaModuleService, Transferencia>)(object)this)._service.Canal<ITransferenciaModuleService, Task<TransferenciaDto>>((Func<ITransferenciaModuleService, Task<TransferenciaDto>>)((ITransferenciaModuleService x) => x.EstornarAsync(((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens.ObterDtoItemSelecionado())));
				((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens.Atualizar(dto);
				((ListaBase<TransferenciaDto, TransferenciaViewModel>)(object)this).Itens.Posicionar((Expression<Func<TransferenciaViewModel, bool>>)((TransferenciaViewModel x) => ((EntityBaseDto)((ViewModelDtoBase<TransferenciaDto>)x).Dto).Id == ((EntityBaseDto)dto).Id));
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		protected override void SelecionadoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((ListaManutencaoBase<TransferenciaDto, TransferenciaViewModel, Transferencia>)(object)this).SelecionadoOnPropertyChanged(sender, propertyChangedEventArgs);
			if (propertyChangedEventArgs.PropertyName.Equals("Selecionado"))
			{
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => EstornarVisivel));
			}
		}
	}
	public class TransferenciasViewModel : ModuloBase
	{
		public TransferenciasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloTransferenciasRegion, typeof(TransferenciaListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class TransferenciaViewModel : ViewModelDtoBase<TransferenciaDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public SelecaoCommandParameter<PessoaDto> EmpresaCommandParameter => (SelecaoCommandParameter<PessoaDto>)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto dto)
		{
			EmpresaDestino = dto;
		}, (ValidateParameter<PessoaDto>)(object)ValidacaoEmpresaDestino);

		private ValidateParameterPessoa<PessoaDto> ValidacaoEmpresaDestino => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => HelperObject.IsExactlyObject<EmitenteDto>((object)x.Juridica.Emitente)), "Selecione uma pessoa do tipo 'Emitente'!", (TipoPessoaCadastro)14, HorizontalAlignment.Center);

		public ICommand PesquisarEmpresaDestinoCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarEmpresaDestino);

		public PessoaDto Empresa { get; protected set; }

		public PessoaDto EmpresaDestino
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => EmpresaDestino));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => EmpresaDestino), value, (Action)delegate
				{
					NomeEmpresaDestino = ObterNomeEmpresaDestino();
				});
			}
		}

		public string NomeEmpresaDestino
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeEmpresaDestino));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeEmpresaDestino), value, (Action)RemoverEmpresaDestinoQuandoNomeApagado);
			}
		}

		public Lista<ContaDto> ContasOrigem { get; set; }

		public Lista<ContaDto> ContasDestino { get; set; }

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

		public string Documento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Documento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Documento), value);
			}
		}

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

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "O 'Valor' deve ser maior que zero.")]
		public decimal Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Valor), value);
			}
		}

		public DateTime DataHoraCompetencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraCompetencia));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraCompetencia), value);
			}
		}

		public TransferenciaViewModel()
		{
		}

		public TransferenciaViewModel(TransferenciaDto dto)
			: base(dto)
		{
		}

		private async void PesquisarEmpresaDestino(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomeEmpresaDestino, (TipoPessoaCadastro)14, (Action<PessoaDto>)delegate(PessoaDto dto)
			{
				EmpresaDestino = dto;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoEmpresaDestino, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private string ObterNomeEmpresaDestino()
		{
			if (!HelperObject.IsNull((object)EmpresaDestino) && !HelperObject.Is<PessoaDtoNull>((object)EmpresaDestino))
			{
				return EmpresaDestino.Nome;
			}
			return string.Empty;
		}

		private void RemoverEmpresaDestinoQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomeEmpresaDestino) && HelperObject.IsNotNull((object)EmpresaDestino) && HelperObject.IsNot<PessoaDtoNull>((object)EmpresaDestino))
			{
				EmpresaDestino = (PessoaDto)new PessoaDtoNull();
			}
		}

		public override void ProjectedAsViewModel()
		{
			CriarContasOrigem();
			CriarContasDestino();
			Empresa = base.Dto.Empresa;
			EmpresaDestino = base.Dto.EmpresaDestino;
			ContasOrigem.Posicionar((Expression<Func<ContaDto, bool>>)((ContaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)((ViewModelDtoBase<TransferenciaDto>)this).Dto.ContaOrigem).Id));
			ContasDestino.Posicionar((Expression<Func<ContaDto, bool>>)((ContaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)((ViewModelDtoBase<TransferenciaDto>)this).Dto.ContaDestino).Id));
			Descricao = base.Dto.Descricao;
			Documento = base.Dto.Documento;
			Observacao = base.Dto.Observacao;
			Valor = base.Dto.Valor;
			DataHoraCompetencia = base.Dto.DataHoraCompetencia;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			TransferenciaDto val = (TransferenciaDto)((obj is TransferenciaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Empresa = Empresa;
				val.EmpresaDestino = EmpresaDestino;
				val.ContaOrigem = ContasOrigem.ItemSelecionado;
				val.ContaDestino = ContasDestino.ItemSelecionado;
				val.Descricao = $"Transferência {ContasOrigem.ItemSelecionado.Descricao} para {ContasDestino.ItemSelecionado.Descricao}";
				val.Documento = Documento;
				val.Observacao = Observacao;
				val.Valor = Valor;
				val.DataHoraCompetencia = DataHoraCompetencia;
				base.AtualizarDto<TransferenciaDto>(ref val);
			}
		}

		private void CriarContasOrigem()
		{
			ContasOrigem = new Lista<ContaDto>();
			ContasOrigem.Adicionar(base.Dto.Contas);
		}

		private void CriarContasDestino()
		{
			ContasDestino = new Lista<ContaDto>();
			ContasDestino.Adicionar(base.Dto.Contas);
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new TransferenciaFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Transferências entre contas";
		}

		public string ToTituloFicha()
		{
			return "Transferência entre conta";
		}
	}
}
namespace Otus.Modulo.Transferencias.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Transferencias.Properties.Resources", typeof(Resources).Assembly);
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

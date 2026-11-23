using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Agendamentos;
using DigisatServer.Application.Main.Dto.Cadastros.Animais;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Agendamentos;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Resources;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Recorrencia;
using Otus.Infraestrutura.Views.Recorrencia;
using Otus.Modulo.Agendamentos.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Agendamentos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Agendamentos")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Agendamentos
{
	[Module(ModuleName = "AgendamentoModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	public class AgendamentoModulo : IModule
	{
		private readonly IUnityContainer _container;

		private readonly IModuleManager _moduleManager;

		public AgendamentoModulo(IUnityContainer container, IModuleManager moduleManager)
		{
			_container = container;
			_moduleManager = moduleManager;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AgendamentosView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AgendamentoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AgendamentoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AgendamentoFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<RecorrenciaFichaView>(_container, (string)null);
			_moduleManager.LoadModule("AnimalModulo");
		}
	}
}
namespace Otus.Modulo.Agendamentos.Views
{
	public class AgendamentoFichaAdicionarView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AgendamentoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Agendamentos;component/views/agendamentofichaadicionarview.xaml", UriKind.Relative);
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
	public class AgendamentoFichaAtualizarView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AgendamentoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Agendamentos;component/views/agendamentofichaatualizarview.xaml", UriKind.Relative);
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
	public class AgendamentoListaView : UserControl, IComponentConnector
	{
		internal SchedulerControl scheduler;

		internal DateNavigator dateNavigator;

		private bool _contentLoaded;

		public AgendamentoListaView()
		{
			InitializeComponent();
			AppointmentLabelCollection labels = scheduler.Storage.AppointmentStorage.Labels;
			((DXCollectionBase<IAppointmentLabel>)(object)labels).Clear();
			foreach (Rotulo iten in Rotulo.Itens)
			{
				labels.Add(labels.CreateNewLabel((object)iten.Codigo, iten.Descricao, string.Empty, iten.Cor));
			}
			AppointmentStatusCollection statuses = scheduler.Storage.AppointmentStorage.Statuses;
			((DXCollectionBase<IAppointmentStatus>)(object)statuses).Clear();
			foreach (Situacao iten2 in Situacao.Itens)
			{
				((DXCollectionBase<IAppointmentStatus>)(object)statuses).Add((IAppointmentStatus)(object)statuses.CreateNewStatus((object)iten2.Codigo, iten2.Descricao, string.Empty, iten2.Cor));
			}
			((SchedulerViewAppointmentDisplayOptions)scheduler.DayView.AppointmentDisplayOptions).SnapToCellsMode = (AppointmentSnapToCellsMode)3;
		}

		private void SchedulerStorage_OnAppointmentDeleting(object sender, PersistentObjectCancelEventArgs e)
		{
			e.Cancel = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!_contentLoaded)
			{
				_contentLoaded = true;
				Uri resourceLocator = new Uri("/Modulo.Agendamentos;component/views/agendamentolistaview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				scheduler = (SchedulerControl)target;
				break;
			case 2:
				((SchedulerStorage)target).AppointmentDeleting += new PersistentObjectCancelEventHandler(SchedulerStorage_OnAppointmentDeleting);
				break;
			case 3:
				dateNavigator = (DateNavigator)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class AgendamentosView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AgendamentosView()
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
				Uri resourceLocator = new Uri("/Modulo.Agendamentos;component/views/agendamentosview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Agendamentos.Views.AgendamentoFichaViewDetalhes
{
	public class AgendamentoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterAnimal;

		internal ButtonInfo ParameterPessoa;

		internal ButtonInfo ParameterUsuario;

		private bool _contentLoaded;

		public AgendamentoView()
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
				Uri resourceLocator = new Uri("/Modulo.Agendamentos;component/views/agendamentofichaviewdetalhes/agendamentoview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParameterAnimal = (ButtonInfo)target;
				break;
			case 2:
				ParameterPessoa = (ButtonInfo)target;
				break;
			case 3:
				ParameterUsuario = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.Agendamentos.ViewModels
{
	public class AgendamentoAbaViewModel : AbaPrincipalViewModelBase
	{
		public AgendamentoAbaViewModel(IViewModelPrincipal viewModel)
			: base("Agendamento", "Datas e outras informações", viewModel)
		{
		}
	}
	public class AgendamentoFichaAdicionarViewModel : FichaBaseAdicionar<AgendamentoDto, AgendamentoViewModel, IAgendamentoModuleService>
	{
		private PessoaDto _pessoaDto;

		private DateTime _dataInicial;

		private AnimalDto _animalDto;

		private string _observacao;

		private string _assunto;

		public ICommand RecorrenciaCommand { get; set; }

		public AgendamentoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			RecorrenciaCommand = (ICommand)new DelegateCommand((Action)AbrirRecorrencia, (Func<bool>)PodeAbrirRecorrencia, (bool?)null);
		}

		private bool PodeAbrirRecorrencia()
		{
			if (HelperObject.IsNotNull((object)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario))
			{
				return ((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario.Valido;
			}
			return false;
		}

		private void AbrirRecorrencia()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			try
			{
				AgendamentoDto val = ((IFormularioEntity)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario).ToDto<AgendamentoDto>();
				NavigationParameters val2 = new NavigationParameters();
				val2.Add("Dto", (object)val);
				val2.Add("FecharOrigem", (object)new Action(FecharAposRecorrencia));
				NavigationParameters val3 = val2;
				((NavigationBase)this).Navegar("RecorrenciaFichaView", (Action<NavigationResult>)delegate
				{
				}, val3);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void FecharAposRecorrencia()
		{
			((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).DisableConfirmOnClose();
			((NavigationBase)this).FecharView();
		}

		protected override Task Gravar()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			AgendamentoDto dtoAtualizado = ((IFormularioEntity)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario).ToDto<AgendamentoDto>();
			IEnumerable<AgendamentoDto> source = DadosSistema.GetService().Canal<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>((Func<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>)((IAgendamentoModuleService service) => service.PossuiAgendamentoDoUsuarioNoPeriodo(dtoAtualizado.UsuarioReferencia, dtoAtualizado.DataHoraInicial, dtoAtualizado.DataHoraFinal)));
			if (!source.Any())
			{
				return ((FichaBasePersistir<AgendamentoDto, AgendamentoViewModel>)(object)this).Gravar();
			}
			IEnumerable<string> source2 = source.Select((AgendamentoDto x) => $"{x.Assunto} {HelperTime.FormatarHora(x.DataHoraInicial, (FormatoHora)2)} até {HelperTime.FormatarHora(x.DataHoraFinal, (FormatoHora)2)}\n");
			if (DialogoFactory.MostrarDialogoSimNao($"Horário sobrepõe outro agendamento:\n{HelperString.LinhasParaTexto(source2.ToArray())}Continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return Task.Factory.StartNew(delegate
				{
				});
			}
			return ((FichaBasePersistir<AgendamentoDto, AgendamentoViewModel>)(object)this).Gravar();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			ref PessoaDto pessoaDto = ref _pessoaDto;
			object obj = navigationParameters["PessoaDto"];
			pessoaDto = (PessoaDto)((obj is PessoaDto) ? obj : null);
			ref AnimalDto animalDto = ref _animalDto;
			object obj2 = navigationParameters["AnimalDto"];
			animalDto = (AnimalDto)((obj2 is AnimalDto) ? obj2 : null);
			_dataInicial = (navigationParameters["DataHoraInicial"] as DateTime?) ?? DateTime.Now;
			_observacao = navigationParameters["Observacao"] as string;
			_assunto = navigationParameters["Assunto"] as string;
			if (navigationParameters["AgendamentoViewModel"] == null)
			{
				navigationParameters.Add("AgendamentoViewModel", (object)new AgendamentoViewModel());
			}
			((FichaBasePersistir<AgendamentoDto, AgendamentoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}

		protected override void CriarFormulario()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			if (HelperObject.IsNotNull((object)_pessoaDto))
			{
				((Base)this).Titulo = "Agendamento";
				AgendamentoDto val = new AgendamentoDto
				{
					PessoaNome = _pessoaDto.Nome,
					PessoaReferencia = ((EntityBaseDto)_pessoaDto).Id,
					Telefone = _pessoaDto.TelefoneToText
				};
				AnimalDto animalDto = _animalDto;
				val.AnimalNome = ((animalDto != null) ? animalDto.Nome : null) ?? string.Empty;
				AnimalDto animalDto2 = _animalDto;
				val.AnimalReferencia = ((animalDto2 != null) ? ((EntityBaseDto)animalDto2).Id : null) ?? string.Empty;
				val.DataHoraInicial = _dataInicial;
				val.DataHoraFinal = _dataInicial.AddHours(1.0);
				val.Observacao = HelperString.ToStringEmptyWhenNull(_observacao);
				val.Assunto = HelperString.ToStringEmptyWhenNull(_assunto);
				val.Notificar = true;
				val.MinutosParaNotificar = 10;
				((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario = (IFormulario)(object)new AgendamentoFormulario(val);
			}
			else
			{
				((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).CriarFormulario();
			}
		}
	}
	public class AgendamentoFichaAtualizarViewModel : FichaBaseAtualizar<AgendamentoDto, AgendamentoViewModel, IAgendamentoModuleService>
	{
		public ICommand RecorrenciaCommand { get; set; }

		public AgendamentoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			RecorrenciaCommand = (ICommand)new DelegateCommand((Action)AbrirRecorrencia, (Func<bool>)PodeAbrirRecorrencia, (bool?)null);
		}

		protected override Task Gravar()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			AgendamentoDto dtoAtualizado = ((IFormularioEntity)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario).ToDto<AgendamentoDto>();
			IEnumerable<AgendamentoDto> source = DadosSistema.GetService().Canal<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>((Func<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>)((IAgendamentoModuleService service) => service.PossuiAgendamentoDoUsuarioNoPeriodo(dtoAtualizado.UsuarioReferencia, dtoAtualizado.DataHoraInicial, dtoAtualizado.DataHoraFinal)));
			if (!source.All((AgendamentoDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)dtoAtualizado).Id)))
			{
				IEnumerable<string> source2 = from x in source
					where !((EntityBaseDto)x).Id.Equals(((EntityBaseDto)dtoAtualizado).Id)
					select $"{x.Assunto} {HelperTime.FormatarHora(x.DataHoraInicial, (FormatoHora)2)} até {HelperTime.FormatarHora(x.DataHoraFinal, (FormatoHora)2)}\n";
				if (DialogoFactory.MostrarDialogoSimNao($"Horário sobrepõe outro agendamento:\n{HelperString.LinhasParaTexto(source2.ToArray())}Continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return Task.Factory.StartNew(delegate
					{
					});
				}
			}
			AplicarNosProximosDaSerie(dtoAtualizado);
			return ((FichaBasePersistir<AgendamentoDto, AgendamentoViewModel>)(object)this).Gravar();
		}

		private void AplicarNosProximosDaSerie(AgendamentoDto dto)
		{
			if (!dto.PossuiSerie || DialogoFactory.MostrarDialogoSimNao($"Este agendamento é o {dto.PosicaoNaSerieToText} de uma série.\nAplicar as alterações aos próximos agendamentos da série?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			try
			{
				DadosSistema.GetService().Canal<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>((Func<IAgendamentoModuleService, IEnumerable<AgendamentoDto>>)((IAgendamentoModuleService service) => service.AtualizarProximosDaSerieAsync(dto).Result));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeAbrirRecorrencia()
		{
			if (HelperObject.IsNotNull((object)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario))
			{
				return ((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario.Valido;
			}
			return false;
		}

		private void AbrirRecorrencia()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			try
			{
				AgendamentoDto val = ((IFormularioEntity)((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).Formulario).ToDto<AgendamentoDto>();
				NavigationParameters val2 = new NavigationParameters();
				val2.Add("Dto", (object)val);
				val2.Add("FecharOrigem", (object)new Action(FecharAposRecorrencia));
				NavigationParameters val3 = val2;
				((NavigationBase)this).Navegar("RecorrenciaFichaView", (Action<NavigationResult>)delegate
				{
				}, val3);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void FecharAposRecorrencia()
		{
			((FichaBase<AgendamentoDto, AgendamentoViewModel>)(object)this).DisableConfirmOnClose();
			((NavigationBase)this).FecharView();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (navigationParameters["AgendamentoViewModel"] == null)
			{
				object obj = navigationParameters["AgendamentoDto"];
				AgendamentoDto dto = (AgendamentoDto)((obj is AgendamentoDto) ? obj : null);
				string identificador = navigationParameters["Identificador"] as string;
				if (!string.IsNullOrEmpty(identificador))
				{
					dto = DadosSistema.GetService().Canal<IAgendamentoModuleService, AgendamentoDto>((Func<IAgendamentoModuleService, AgendamentoDto>)((IAgendamentoModuleService service) => service.ObterPorId(identificador)));
					navigationParameters.Add("AgendamentoViewModel", (object)new AgendamentoViewModel(dto));
				}
				else
				{
					navigationParameters.Add("AgendamentoViewModel", (object)new AgendamentoViewModel(dto));
				}
			}
			((FichaBasePersistir<AgendamentoDto, AgendamentoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
		}
	}
	public class AgendamentoFormulario : FormularioEntity<AgendamentoDto>
	{
		public AgendamentoFormulario(AgendamentoDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AgendamentoAbaViewModel((IViewModelPrincipal)(object)new AgendamentoViewModel(base.Dto)) });
		}
	}
	public class AgendamentoListaViewModel : BaseMinimizar
	{
		private readonly ISessao _sessao;

		private readonly IWService _service;

		private bool _recarregarAoVoltar;

		private List<AgendamentoViewModel> _todosAgendamentos = new List<AgendamentoViewModel>();

		public ObservableCollection<AgendamentoViewModel> Agendamentos { get; set; }

		public ObservableCollection<UsuarioFiltroItem> UsuariosDisponiveis { get; } = new ObservableCollection<UsuarioFiltroItem>();


		public ObservableCollection<Appointment> SelectedAppointments
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<Appointment>>((Expression<Func<ObservableCollection<Appointment>>>)(() => SelectedAppointments));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<Appointment>>((Expression<Func<ObservableCollection<Appointment>>>)(() => SelectedAppointments), value);
			}
		}

		public IList<DateTime> Datas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IList<DateTime>>((Expression<Func<IList<DateTime>>>)(() => Datas));
			}
			set
			{
				((BindableBase)this).SetProperty<IList<DateTime>>((Expression<Func<IList<DateTime>>>)(() => Datas), value, (Action)DatasChanged);
			}
		}

		public ICommand AdicionarCommand { get; set; }

		public ICommand AdicionarDoubleClickCommand { get; set; }

		public ICommand EditarCommand { get; set; }

		public ICommand CancelarCommand { get; set; }

		public ICommand OutrasInformacoesPessoaCommand { get; set; }

		public ICommand NavegarParaAnimalCommand { get; set; }

		public ICommand AdicionarRecorrenciaCommand { get; set; }

		public ICommand GerarDavOsCommand { get; set; }

		public ICommand NavegarMovimentacaoCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public AgendamentoListaViewModel(ISessao sessao, IWService service)
		{
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			//IL_0209: Unknown result type (might be due to invalid IL or missing references)
			//IL_0213: Expected O, but got Unknown
			_sessao = sessao;
			_service = service;
			Agendamentos = new ObservableCollection<AgendamentoViewModel>();
			Datas = new List<DateTime> { DateTime.Now };
			SelectedAppointments = new ObservableCollection<Appointment>();
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)PodeAdicionar, (bool?)true);
			AdicionarDoubleClickCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)AdicionarDoubleClick, (Func<object, bool>)PodeAdicionarDoubleClick, false, (bool?)null);
			EditarCommand = (ICommand)new AsyncCommand((Func<Task>)Editar, (Func<bool>)PodeEditar, false, (bool?)null);
			OutrasInformacoesPessoaCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)OutrasInformacoesPessoa, (Func<object, bool>)PodeOutrasInformacoesPessoa, false, (bool?)null);
			NavegarParaAnimalCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)NavegarParaAnimal, (Func<object, bool>)PodeNavegarParaAnimal, false, (bool?)null);
			AdicionarRecorrenciaCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)AdicionarRecorrencia, (Func<object, bool>)PodeAdicionarRecorrencia, false, (bool?)null);
			GerarDavOsCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)GerarDavOs, (Func<object, bool>)PodeGerarDavOs, false, (bool?)null);
			NavegarMovimentacaoCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)NavegarMovimentacao, (Func<object, bool>)PodeNavegarMovimentacao, false, (bool?)null);
			CancelarCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)Cancelar, (Func<object, bool>)PodeCancelar, false, (bool?)null);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
		}

		private bool PodeAdicionarDoubleClick(object obj)
		{
			if (PodeAdicionar() && !SelectedAppointments.Any())
			{
				return obj is SchedulerControl;
			}
			return false;
		}

		private Task AdicionarDoubleClick(object obj)
		{
			return Task.Factory.StartNew(delegate
			{
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_0028: Unknown result type (might be due to invalid IL or missing references)
				//IL_003e: Expected O, but got Unknown
				object obj2 = obj;
				SchedulerControl val = (SchedulerControl)((obj2 is SchedulerControl) ? obj2 : null);
				NavegarFichaAdicionar(new AgendamentoViewModel(new AgendamentoDto
				{
					DataHoraInicial = val.SelectedInterval.Start,
					DataHoraFinal = val.SelectedInterval.End
				}));
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool PodeAdicionar()
		{
			return _sessao.VerificarPermissao<Agendamento, Adicionar>();
		}

		private void Adicionar()
		{
			NavegarFichaAdicionar(new AgendamentoViewModel());
		}

		private void NavegarFichaAdicionar(AgendamentoViewModel agendamentoViewModel)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			_recarregarAoVoltar = true;
			Action<NavigationResult> obj = delegate
			{
			};
			NavigationParameters val = new NavigationParameters();
			val.Add(typeof(AgendamentoViewModel).Name, (object)agendamentoViewModel);
			((NavigationBase)this).Navegar("AgendamentoFichaAdicionarView", obj, val);
		}

		private Task Editar()
		{
			object id = ((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id;
			AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(id));
			return Task.Factory.StartNew(delegate
			{
				//IL_0036: Unknown result type (might be due to invalid IL or missing references)
				//IL_003b: Unknown result type (might be due to invalid IL or missing references)
				//IL_005b: Expected O, but got Unknown
				_recarregarAoVoltar = true;
				AgendamentoListaViewModel agendamentoListaViewModel = this;
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(AgendamentoViewModel).Name, (object)viewModel);
				((NavigationBase)agendamentoListaViewModel).Navegar("AgendamentoFichaAtualizarView", obj, val);
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool PodeEditar()
		{
			if (HelperObject.IsNotNull((object)Agendamentos))
			{
				return _sessao.VerificarPermissao<Agendamento, Atualizar>();
			}
			return false;
		}

		private Task OutrasInformacoesPessoa(object obj)
		{
			string location = obj as string;
			AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => location.Contains(x.Id.ToString()));
			return Task.Factory.StartNew(delegate
			{
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				//IL_004a: Expected O, but got Unknown
				AgendamentoListaViewModel agendamentoListaViewModel = this;
				Action<NavigationResult> obj2 = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add("PessoasOutrasInformacoesReferencia", (object)viewModel.PessoaReferencia);
				((NavigationBase)agendamentoListaViewModel).Navegar("PessoasOutrasInformacoesFichaView", obj2, val);
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool PodeOutrasInformacoesPessoa(object obj)
		{
			string text = obj as string;
			if (HelperObject.IsNull((object)text) || !SelectedAppointments.Any() || HelperObject.IsNull((object)Agendamentos) || !Agendamentos.Any())
			{
				return false;
			}
			object id = ((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id;
			AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(id));
			if (HelperObject.IsNull((object)agendamentoViewModel))
			{
				return false;
			}
			if (!string.IsNullOrWhiteSpace(agendamentoViewModel.PessoaReferencia) && text.Contains(((EntityBaseDto)((ViewModelDtoBase<AgendamentoDto>)agendamentoViewModel).Dto).Id))
			{
				return DadosSistema.VerificarPermissao<Pessoa, VisualizarOutrasInformacoes>();
			}
			return false;
		}

		private Task NavegarParaAnimal(object obj)
		{
			string location = obj as string;
			AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => location.Contains(x.Id.ToString()));
			return Task.Factory.StartNew(delegate
			{
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				//IL_004f: Expected O, but got Unknown
				AgendamentoListaViewModel agendamentoListaViewModel = this;
				Action<NavigationResult> obj2 = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add("InformacaoPesquisa", (object)HelperObject.ToStringOrEmpty((object)viewModel.AnimalReferencia));
				((NavigationBase)agendamentoListaViewModel).Navegar("AnimalListaView", obj2, val);
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool PodeNavegarParaAnimal(object obj)
		{
			string text = obj as string;
			if (HelperObject.IsNull((object)text) || !SelectedAppointments.Any() || HelperObject.IsNull((object)Agendamentos) || !Agendamentos.Any())
			{
				return false;
			}
			object id = ((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id;
			AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(id));
			if (HelperObject.IsNull((object)agendamentoViewModel))
			{
				return false;
			}
			if (!string.IsNullOrWhiteSpace(agendamentoViewModel.AnimalReferencia) && text.Contains(((EntityBaseDto)((ViewModelDtoBase<AgendamentoDto>)agendamentoViewModel).Dto).Id))
			{
				return DadosSistema.VerificarPermissao<Animal, Visualizar>();
			}
			return false;
		}

		private Task AdicionarRecorrencia(object obj)
		{
			AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => SelectedAppointments.Select((Appointment y) => y.Location).Contains(x.Id.ToString()));
			return Task.Factory.StartNew(delegate
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0036: Expected O, but got Unknown
				//IL_0037: Expected O, but got Unknown
				NavigationParameters val = new NavigationParameters();
				val.Add("Dto", (object)((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto);
				val.Add("RecorrenciaViewModel", (object)new RecorrenciaViewModel(((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto));
				NavigationParameters val2 = val;
				_recarregarAoVoltar = true;
				((NavigationBase)this).Navegar(typeof(RecorrenciaFichaView).Name, (Action<NavigationResult>)delegate
				{
				}, val2);
				((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool PodeAdicionarRecorrencia(object obj)
		{
			if (HelperObject.IsNull((object)Agendamentos.FirstOrDefault((AgendamentoViewModel x) => SelectedAppointments.Select((Appointment y) => y.Location).Contains(x.Id.ToString()))))
			{
				return false;
			}
			return DadosSistema.VerificarPermissao<Agendamento, Adicionar>();
		}

		private Task Cancelar(object obj)
		{
			object id = ((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id;
			AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(id));
			if (((ViewModelDtoBase<AgendamentoDto>)agendamentoViewModel).Dto.PossuiSerie)
			{
				return CancelarDaSerie(agendamentoViewModel);
			}
			if (DialogoFactory.MostrarDialogoSimNao($"Deseja cancelar o agendamento \"{agendamentoViewModel.ToolTip}?\"", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return Task.Factory.StartNew(delegate
				{
				});
			}
			return CancelarAgendamento(agendamentoViewModel);
		}

		private Task CancelarAgendamento(AgendamentoViewModel viewModel)
		{
			((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.Cancelado = true;
			Agendamentos.Remove(viewModel);
			_todosAgendamentos.Remove(viewModel);
			return _service.Canal<IAgendamentoModuleService, Task<AgendamentoDto>>((Func<IAgendamentoModuleService, Task<AgendamentoDto>>)((IAgendamentoModuleService x) => ((IAtualizar<AgendamentoDto>)(object)x).AtualizarAsync(((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto)));
		}

		private Task CancelarDaSerie(AgendamentoViewModel viewModel)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			List<CodigoDescricaoDto> list = new List<CodigoDescricaoDto>
			{
				new CodigoDescricaoDto
				{
					Codigo = 1,
					Descricao = "Somente este agendamento"
				},
				new CodigoDescricaoDto
				{
					Codigo = 2,
					Descricao = "Este e os próximos da série"
				}
			};
			CodigoDescricaoDto val = DialogoFactory.MostrarDialogoComboBox<CodigoDescricaoDto>($"Este agendamento é o {((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.PosicaoNaSerieToText} de uma série. O que deseja cancelar?", "Cancelar agendamento", (IEnumerable<CodigoDescricaoDto>)list, "Descricao", true);
			if (HelperObject.IsNull((object)val))
			{
				return Task.Factory.StartNew(delegate
				{
				});
			}
			if (val.Codigo == 1)
			{
				return CancelarAgendamento(viewModel);
			}
			long num = _service.Canal<IAgendamentoModuleService, long>((Func<IAgendamentoModuleService, long>)((IAgendamentoModuleService x) => x.CancelarSerie(((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.SerieReferencia, ((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.DataHoraInicial)));
			RemoverProximosDaSerie(viewModel);
			DialogoFactory.MostrarDialogoConfirma($"{num} agendamento(s) da série foram cancelados.", false, HorizontalAlignment.Center);
			return Task.Factory.StartNew(delegate
			{
			});
		}

		private void RemoverProximosDaSerie(AgendamentoViewModel viewModel)
		{
			foreach (AgendamentoViewModel item in _todosAgendamentos.Where((AgendamentoViewModel x) => ((ViewModelDtoBase<AgendamentoDto>)x).Dto.SerieReferencia.Equals(((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.SerieReferencia) && ((ViewModelDtoBase<AgendamentoDto>)x).Dto.DataHoraInicial >= ((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.DataHoraInicial).ToList())
			{
				_todosAgendamentos.Remove(item);
			}
			AtualizarUsuariosDisponiveis();
			AplicarFiltro();
		}

		private bool PodeCancelar(object obj)
		{
			KeyEventArgs obj2 = obj as KeyEventArgs;
			if (obj2 != null && obj2.Key == Key.Delete && HelperObject.IsNotNull((object)SelectedAppointments) && SelectedAppointments.Any() && HelperObject.IsNotNull((object)Agendamentos))
			{
				return _sessao.VerificarPermissao<Agendamento, Atualizar>();
			}
			return false;
		}

		private void AtualizarUsuariosDisponiveis()
		{
			UsuariosDisponiveis.Clear();
			foreach (UsuarioFiltroItem item in from x in _todosAgendamentos
				where !string.IsNullOrEmpty(x.UsuarioReferencia)
				group x by x.UsuarioReferencia into g
				select new UsuarioFiltroItem(g.First().UsuarioNome ?? g.Key, g.Key, AplicarFiltro))
			{
				UsuariosDisponiveis.Add(item);
			}
		}

		private void AplicarFiltro()
		{
			List<string> selecionados = (from x in UsuariosDisponiveis
				where x.Selecionado
				select x.Referencia).ToList();
			List<AgendamentoViewModel> list = (selecionados.Any() ? _todosAgendamentos.Where((AgendamentoViewModel x) => selecionados.Contains(x.UsuarioReferencia)).ToList() : _todosAgendamentos);
			Agendamentos.Clear();
			CollectionExtensions.AddRange<AgendamentoViewModel>((Collection<AgendamentoViewModel>)Agendamentos, (IEnumerable<AgendamentoViewModel>)list);
		}

		private async void DatasChanged()
		{
			_todosAgendamentos = (await _service.Canal<IAgendamentoModuleService, Task<IEnumerable<AgendamentoDto>>>((Func<IAgendamentoModuleService, Task<IEnumerable<AgendamentoDto>>>)((IAgendamentoModuleService service) => service.Pesquisar((IEnumerable<DateTime>)Datas)))).Select(delegate(AgendamentoDto dto)
			{
				AgendamentoViewModel agendamentoViewModel = new AgendamentoViewModel(dto);
				((ViewModelBase<AgendamentoDto>)(object)agendamentoViewModel).ProjectedAsViewModel();
				return agendamentoViewModel;
			}).ToList();
			AtualizarUsuariosDisponiveis();
			AplicarFiltro();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!_sessao.VerificarPermissao<Agendamento, Visualizar>())
			{
				((NavigationBase)this).FecharView();
				DialogoFactory.MostrarDialogoConfirma(Messages.AcessoNegado, false, HorizontalAlignment.Center);
				return;
			}
			object obj = navigationParameters["AgendamentoDto"];
			Atualizar((AgendamentoDto)((obj is AgendamentoDto) ? obj : null));
			if (_recarregarAoVoltar)
			{
				_recarregarAoVoltar = false;
				DatasChanged();
			}
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
		}

		private void Atualizar(AgendamentoDto agendamentoDto)
		{
			if (!HelperObject.IsNull((object)agendamentoDto))
			{
				AgendamentoViewModel agendamentoViewModel = new AgendamentoViewModel(agendamentoDto);
				((ViewModelBase<AgendamentoDto>)(object)agendamentoViewModel).ProjectedAsViewModel();
				Substituir(agendamentoViewModel);
			}
		}

		public void Substituir(AgendamentoViewModel item)
		{
			AgendamentoViewModel item2 = _todosAgendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(item.Id));
			int num = _todosAgendamentos.IndexOf(item2);
			if (num >= 0)
			{
				_todosAgendamentos[num] = item;
			}
			else
			{
				_todosAgendamentos.Insert(0, item);
			}
			AtualizarUsuariosDisponiveis();
			AplicarFiltro();
		}

		private bool PodeGerarDavOs(object obj)
		{
			AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => SelectedAppointments.Select((Appointment y) => y.Location).Contains(x.Id.ToString()));
			if (HelperObject.IsNull((object)agendamentoViewModel))
			{
				return false;
			}
			if (SelectedAppointments.Any() && HelperObject.IsNotNull((object)Agendamentos))
			{
				return !string.IsNullOrWhiteSpace(agendamentoViewModel.PessoaReferencia);
			}
			return false;
		}

		private async Task GerarDavOs(object obj)
		{
			try
			{
				AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id));
				if (viewModel.DavOsGerado <= 0 || DialogoFactory.MostrarDialogoSimNao($"Agendamento já gerou o DAV-OS {viewModel.DavOsGerado}. Deseja continuar?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					MovimentacaoDto movimentacaoDto = new MovimentacaoDto((TipoMovimentacao)2);
					MovimentacaoBaseDto dto = default(MovimentacaoBaseDto);
					_ = dto;
					dto = await _service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IReferenciaAtualizarService<MovimentacaoBaseDto>)(object)service).AtualizarDtoAsync((MovimentacaoBaseDto)(object)movimentacaoDto)));
					dto.Pessoa = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(viewModel.PessoaReferencia)));
					HelperObject.ToDo<MovimentacaoDto>((object)dto, (Action<MovimentacaoDto>)delegate(MovimentacaoDto x)
					{
						x.FormaPagamento = dto.Pessoa.Cliente.FormaPagamento;
						x.Animais = PreencherAnimalDavOs(viewModel);
					});
					HelperObject.To<MovimentacaoDto>((object)dto).OrigemAgendamento = true;
					if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
					{
						AgendamentoListaViewModel agendamentoListaViewModel = this;
						Action<NavigationResult> obj2 = delegate
						{
						};
						NavigationParameters val = new NavigationParameters();
						val.Add("MovimentacaoDto", (object)dto);
						((NavigationBase)agendamentoListaViewModel).Navegar("DavOsFichaAdicionarView", obj2, val);
						((NavigationBase)this).Resolucao.Reset();
						((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
					}
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

		public List<MovimentacaoAnimalDto> PreencherAnimalDavOs(AgendamentoViewModel viewModel)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			List<MovimentacaoAnimalDto> list = new List<MovimentacaoAnimalDto>();
			if (!string.IsNullOrEmpty(viewModel.AnimalReferencia))
			{
				MovimentacaoAnimalDto item = new MovimentacaoAnimalDto
				{
					AnimalReferencia = viewModel.AnimalReferencia,
					AnimalNome = viewModel.AnimalNome
				};
				list.Add(item);
				return list;
			}
			return list;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			try
			{
				object obj = regionNavigationEventArgs.NavigationContext.Parameters["MovimentacaoDto"];
				MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
				if (!HelperObject.IsNull((object)val))
				{
					AgendamentoViewModel viewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id));
					viewModel.MovimentacaoReferencia = ((EntityBaseDto)val).Id;
					((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.MovimentacaoReferencia = ((EntityBaseDto)val).Id;
					viewModel.DavOsGerado = ((MovimentacaoBaseDto)val).Numero;
					((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto.DavOsGerado = ((MovimentacaoBaseDto)val).Numero;
					_service.Canal<IAgendamentoModuleService, Task<AgendamentoDto>>((Func<IAgendamentoModuleService, Task<AgendamentoDto>>)((IAgendamentoModuleService service) => ((IAtualizar<AgendamentoDto>)(object)service).AtualizarAsync(((ViewModelDtoBase<AgendamentoDto>)viewModel).Dto)));
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				if (regionNavigationEventArgs.NavigationContext.Uri.OriginalString.Contains("Agendamento"))
				{
					((NavigationBase)this).Resolucao.Update();
					((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
				}
			}
		}

		private Task NavegarMovimentacao(object obj)
		{
			try
			{
				AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => x.Id.Equals(((IPersistentObject)SelectedAppointments.FirstOrDefault()).Id));
				SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
				{
				}, true);
				val.AdicionarParametro("Identificador", (object)agendamentoViewModel.MovimentacaoReferencia);
				val.AdicionarParametro("OcultarCheckbox", (object)true);
				((NavigationBase)this).Navegar((object)val);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				IResolucao resolucao = ((NavigationBase)this).Resolucao;
				if (resolucao != null)
				{
					resolucao.Reset();
				}
			}
			return Task.Factory.StartNew(delegate
			{
				((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
			});
		}

		private bool PodeNavegarMovimentacao(object obj)
		{
			AgendamentoViewModel agendamentoViewModel = Agendamentos.FirstOrDefault((AgendamentoViewModel x) => SelectedAppointments.Select((Appointment y) => y.Location).Contains(x.Id.ToString()));
			if (HelperObject.IsNull((object)agendamentoViewModel))
			{
				return false;
			}
			if (SelectedAppointments.Any() && HelperObject.IsNotNull((object)Agendamentos))
			{
				return !string.IsNullOrWhiteSpace(agendamentoViewModel.MovimentacaoReferencia);
			}
			return false;
		}

		private void Visualizar()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)82);
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private bool PodeVisualizar()
		{
			if (DadosSistema.PodeVisualizarRelatorioAgendamento)
			{
				return DadosSistema.PodeExecutarFuncao(new Type[2]
				{
					typeof(Administrador),
					typeof(AdministradorIndustria)
				});
			}
			return false;
		}

		public override void Dispose()
		{
			Agendamentos?.Clear();
			SelectedAppointments?.Clear();
			((BaseMinimizar)this).Dispose();
		}
	}
	public class UsuarioFiltroItem : INotifyPropertyChanged
	{
		private readonly Action _onChanged;

		private bool _selecionado;

		public string Nome { get; }

		public string Referencia { get; }

		public bool Selecionado
		{
			get
			{
				return _selecionado;
			}
			set
			{
				_selecionado = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Selecionado"));
				_onChanged?.Invoke();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public UsuarioFiltroItem(string nome, string referencia, Action onChanged)
		{
			Nome = nome;
			Referencia = referencia;
			_onChanged = onChanged;
		}
	}
	public class AgendamentosViewModel : ModuloBase
	{
		public AgendamentosViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAgendamentosRegion, "AgendamentoListaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class AgendamentoViewModel : ViewModelDtoBase<AgendamentoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public DateTime DataInicial
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicial), value, (Action)ValidarDatas);
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
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinal), value, (Action)ValidarDatas);
			}
		}

		public bool IsPet => DadosSistema.IsPet;

		public virtual ISelecaoCommandParameter AnimalCommandParameter
		{
			get
			{
				SelecaoCommandParameter<AnimalDto> obj = new SelecaoCommandParameter<AnimalDto>("AnimalListaView", (Action<AnimalDto>)delegate(AnimalDto animal)
				{
					AnimalNome = animal.Nome;
					AnimalReferencia = ((EntityBaseDto)animal).Id;
					PessoaNome = (HelperString.IsNullOrEmpty(PessoaNome) ? animal.Proprietario.Nome : PessoaNome);
					PessoaReferencia = (HelperString.IsNullOrEmpty(PessoaReferencia) ? ((EntityBaseDto)animal.Proprietario).Id : PessoaReferencia);
					object telefone;
					if (!HelperString.IsNullOrEmpty(Telefone))
					{
						telefone = Telefone;
					}
					else
					{
						TelefoneDto telefoneWhatsApp = animal.Proprietario.TelefoneWhatsApp;
						telefone = ((telefoneWhatsApp != null) ? telefoneWhatsApp.ToText : null);
					}
					Telefone = (string)telefone;
				}, true);
				obj.AdicionarParametro("InformacaoPesquisa", (object)AnimalReferencia);
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public string AnimalReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => AnimalReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => AnimalReferencia), value);
			}
		}

		public string AnimalNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => AnimalNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => AnimalNome), value, (Action)delegate
				{
					AnimalReferencia = string.Empty;
				});
			}
		}

		public virtual ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, true);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		public string PessoaReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PessoaReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PessoaReferencia), value);
			}
		}

		public string PessoaNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PessoaNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PessoaNome), value, (Action)delegate
				{
					PessoaReferencia = string.Empty;
				});
			}
		}

		public virtual ISelecaoCommandParameter UsuarioCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<UsuarioDto>("UsuarioListaView", (Action<UsuarioDto>)delegate(UsuarioDto usuario)
		{
			UsuarioNome = usuario.Nome;
			UsuarioReferencia = ((EntityBaseDto)usuario).Id;
		}, true);

		public string UsuarioReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => UsuarioReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => UsuarioReferencia), value);
			}
		}

		public string UsuarioNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => UsuarioNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => UsuarioNome), value, (Action)delegate
				{
					UsuarioReferencia = string.Empty;
				});
			}
		}

		public string Telefone
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Telefone));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Telefone), value);
			}
		}

		public string Assunto
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Assunto));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Assunto), value);
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

		public string ToolTip
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ToolTip));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ToolTip), value);
			}
		}

		public bool Notificar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Notificar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Notificar), value);
			}
		}

		public int MinutosParaNotificar
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => MinutosParaNotificar));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => MinutosParaNotificar), value);
			}
		}

		public object Id { get; set; }

		public string MovimentacaoReferencia { get; set; }

		public Lista<Rotulo> Label { get; set; }

		public Lista<Situacao> Status { get; set; }

		public string RecurrenceInfo { get; set; }

		public string ReminderInfo { get; set; }

		public bool HasReminder { get; set; }

		public AppointmentType Type { get; set; }

		public bool PodeAlterarUsuario => DadosSistema.PodeAlterarUsuarioAgendamento;

		public long DavOsGerado
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => DavOsGerado));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => DavOsGerado), value);
			}
		}

		public string SerieReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => SerieReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => SerieReferencia), value);
			}
		}

		public int SeriePosicao
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => SeriePosicao));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => SeriePosicao), value);
			}
		}

		public int SerieTotal
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => SerieTotal));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => SerieTotal), value);
			}
		}

		public bool PossuiSerie => SerieTotal > 0;

		public string PosicaoNaSerieToText
		{
			get
			{
				if (!PossuiSerie)
				{
					return string.Empty;
				}
				return $"Agendamento {SeriePosicao} de {SerieTotal} da série";
			}
		}

		public AgendamentoViewModel()
		{
		}

		public AgendamentoViewModel(AgendamentoDto dto)
			: base(dto)
		{
		}

		public void ValidarDatas()
		{
			if (DataInicial.Date > DataFinal.Date)
			{
				((ViewModelValidationBase)this).Valido = false;
			}
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(PessoaNome, (TipoPessoaCadastro)11, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void SelecionouPessoa(PessoaDto pessoa)
		{
			PessoaNome = pessoa.Nome;
			Telefone = (HelperObject.Is<TelefoneDtoNull>((object)pessoa.TelefonePrincipal) ? pessoa.TelefoneWhatsApp.Numero : pessoa.TelefoneToText);
			PessoaReferencia = ((EntityBaseDto)pessoa).Id;
		}

		public override void ProjectedAsViewModel()
		{
			Id = ((EntityBaseDto)base.Dto).Id;
			SerieReferencia = base.Dto.SerieReferencia;
			SeriePosicao = base.Dto.SeriePosicao;
			SerieTotal = base.Dto.SerieTotal;
			DataInicial = base.Dto.DataHoraInicial;
			DataFinal = base.Dto.DataHoraFinal;
			PessoaNome = base.Dto.PessoaNome;
			PessoaReferencia = base.Dto.PessoaReferencia;
			AnimalNome = base.Dto.AnimalNome;
			AnimalReferencia = base.Dto.AnimalReferencia;
			UsuarioNome = ((!((EntityBaseDto)base.Dto).IsTransient()) ? base.Dto.UsuarioNome : DadosSistema.NomeUsuario);
			UsuarioReferencia = ((!((EntityBaseDto)base.Dto).IsTransient()) ? base.Dto.UsuarioReferencia : DadosSistema.IdUsuario);
			Telefone = base.Dto.Telefone;
			Assunto = base.Dto.Assunto;
			Observacao = base.Dto.Observacao;
			Label = new Lista<Rotulo>();
			Label.Adicionar((IEnumerable<Rotulo>)Rotulo.Itens);
			Label.Posicionar((Expression<Func<Rotulo, bool>>)((Rotulo x) => x.Codigo.Equals(((ViewModelDtoBase<AgendamentoDto>)this).Dto.Label)));
			Status = new Lista<Situacao>();
			Status.Adicionar((IEnumerable<Situacao>)Situacao.Itens);
			Status.Posicionar((Expression<Func<Situacao, bool>>)((Situacao x) => x.Codigo.Equals(((ViewModelDtoBase<AgendamentoDto>)this).Dto.Status)));
			HasReminder = false;
			Type = (AppointmentType)0;
			MovimentacaoReferencia = base.Dto.MovimentacaoReferencia;
			string text = (HelperString.IsNullOrEmpty(AnimalNome) ? "" : $"- {AnimalNome}");
			string text2 = (PossuiSerie ? $" ({SeriePosicao} de {SerieTotal})" : string.Empty);
			ToolTip = $"{DataInicial:HH:mm} - {DataFinal:HH:mm} - {Assunto} - {PessoaNome} {text} - {UsuarioNome}{text2}";
			Notificar = base.Dto.Notificar;
			MinutosParaNotificar = base.Dto.MinutosParaNotificar;
			DavOsGerado = base.Dto.DavOsGerado;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			AgendamentoDto val = (AgendamentoDto)((obj is AgendamentoDto) ? obj : null);
			val.DataHoraInicial = DataInicial;
			val.DataHoraFinal = DataFinal;
			val.Assunto = Assunto;
			val.Observacao = Observacao;
			val.PessoaNome = PessoaNome;
			val.PessoaReferencia = PessoaReferencia;
			val.AnimalNome = AnimalNome;
			val.AnimalReferencia = AnimalReferencia;
			val.UsuarioNome = UsuarioNome;
			val.UsuarioReferencia = UsuarioReferencia;
			val.Telefone = Telefone;
			val.Label = Label.ItemSelecionado.Codigo;
			val.Status = Status.ItemSelecionado.Codigo;
			val.MovimentacaoReferencia = MovimentacaoReferencia;
			val.Notificar = Notificar;
			val.MinutosParaNotificar = MinutosParaNotificar;
			val.DavOsGerado = DavOsGerado;
			val.SerieReferencia = SerieReferencia;
			val.SeriePosicao = SeriePosicao;
			val.SerieTotal = SerieTotal;
			val.DataHoraNotificar = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, DataInicial.Hour, DataInicial.Minute, 0).AddMinutes(MinutosParaNotificar * -1);
			base.AtualizarDto<AgendamentoDto>(ref val);
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AgendamentoFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Agendamentos";
		}

		public string ToTituloFicha()
		{
			return "Agendamento";
		}

		public override void Dispose()
		{
			Lista<Rotulo> label = Label;
			if (label != null)
			{
				((ViewModelValidationBase)label).Dispose();
			}
			Lista<Situacao> status = Status;
			if (status != null)
			{
				((ViewModelValidationBase)status).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.Agendamentos.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Agendamentos.Properties.Resources", typeof(Resources).Assembly);
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

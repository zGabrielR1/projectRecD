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
using DevExpress.Mvvm;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Financeiros.Cheques;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Cheques.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Cheques")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Cheques")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Cheques
{
	[Module(ModuleName = "ChequeModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("RecebimentoModulo")]
	[ModuleDependency("PagamentoModulo")]
	public class ChequeModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ChequeModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ChequesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ChequeListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ChequeFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ChequeFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Cheques.Views
{
	public class ChequeFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public ChequeFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cheques;component/views/chequefichaatualizarview.xaml", UriKind.Relative);
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
	public class ChequeFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public ChequeFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Cheques;component/views/chequefichaadicionarview.xaml", UriKind.Relative);
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
	public class ChequeListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ChequeListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Cheques;component/views/chequelistaview.xaml", UriKind.Relative);
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
	public class ChequesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ChequesView()
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
				Uri resourceLocator = new Uri("/Modulo.Cheques;component/views/chequesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Cheques.ViewModels
{
	public class ChequeAbaViewModel : AbaPrincipalViewModelBase
	{
		public ChequeAbaViewModel(IViewModelPrincipal viewModel)
			: base("Cheque", "Dados principais", viewModel)
		{
		}
	}
	public class ChequeFichaAdicionarViewModel : FichaBaseAdicionar<ChequeDto, ChequeViewModel, IChequeModuleService>
	{
		public ChequeFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class ChequeFichaAtualizarViewModel : FichaBaseAtualizar<ChequeDto, ChequeViewModel, IChequeModuleService>
	{
		public ChequeFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class ChequeFormulario : FormularioEntity<ChequeDto>
	{
		public ChequeFormulario(ChequeDto dto, Pessoa empresa)
			: base(dto)
		{
			ChequeViewModel chequeViewModel = new ChequeViewModel(base.Dto, empresa);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new ChequeAbaViewModel((IViewModelPrincipal)(object)chequeViewModel),
				(IAbaViewModelBase)new ChequeParcelasAbaViewModel((IViewModelDtoBase)(object)new ChequeParcelasViewModel(chequeViewModel))
			});
		}
	}
	public class ChequeListaViewModel : ListaEntity<ChequeDto, ChequeViewModel, IChequeModuleService, Cheque>
	{
		private readonly IWService _service;

		private readonly INotificacao _notificacao;

		public bool PodeDesativar
		{
			get
			{
				if (((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).PodeEditar())
				{
					return ((EntityDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Ativo;
				}
				return false;
			}
		}

		public bool PodeCompensar
		{
			get
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0042: Invalid comparison between Unknown and I4
				//IL_0054: Unknown result type (might be due to invalid IL or missing references)
				//IL_005a: Invalid comparison between Unknown and I4
				if (((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens != null && ((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.SelecionadoValido && ((EntityDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Ativo && (int)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Situacao != 4)
				{
					return (int)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Situacao != 6;
				}
				return false;
			}
		}

		public bool PodeDevolver
		{
			get
			{
				//IL_003c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0042: Invalid comparison between Unknown and I4
				if (((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens != null && ((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.SelecionadoValido && ((EntityDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Ativo)
				{
					return (int)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Situacao != 4;
				}
				return false;
			}
		}

		public bool PodeRepassar
		{
			get
			{
				//IL_0053: Unknown result type (might be due to invalid IL or missing references)
				//IL_0059: Invalid comparison between Unknown and I4
				//IL_006b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0071: Invalid comparison between Unknown and I4
				if (((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens != null && ((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.SelecionadoValido && !((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.IsChequeEmitido && ((EntityDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Ativo && (int)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Situacao != 4)
				{
					return (int)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Situacao != 6;
				}
				return false;
			}
		}

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand DesativarCommand { get; set; }

		public ICommand CompensarCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public ICommand DevolverCommand { get; set; }

		public ICommand RepassarCommand { get; set; }

		public ChequeListaViewModel(ISessao sessao, IWService service, INotificacao notificacao, IEntityAtivos<ChequeDto, IChequeModuleService> ativos, IEntityInativos<ChequeDto, IChequeModuleService> inativos)
			: base(sessao, "ChequeFichaAdicionarView", "ChequeFichaAtualizarView", ativos, inativos)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Expected O, but got Unknown
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Expected O, but got Unknown
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Expected O, but got Unknown
			_service = service;
			_notificacao = notificacao;
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			DuplicarCommand = (ICommand)new AsyncCommand((Func<Task>)Duplicar, true);
			DesativarCommand = (ICommand)new DelegateCommand((Action)Desativar, PodeDesativar);
			CompensarCommand = (ICommand)new DelegateCommand((Action)Compensar, PodeCompensar);
			DevolverCommand = (ICommand)new DelegateCommand((Action)Devolver, PodeDevolver);
			RepassarCommand = (ICommand)new DelegateCommand((Action)Repassar, PodeRepassar);
		}

		protected override void SelecionadoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).SelecionadoOnPropertyChanged(sender, propertyChangedEventArgs);
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeCompensar));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeDesativar));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeDevolver));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeRepassar));
		}

		private bool PodeVisualizar()
		{
			if (DadosSistema.VerificarPermissao<Cheque, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<ChequeDto, ChequeViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private void Visualizar()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)63);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		public override bool PodeEditar()
		{
			if (((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).PodeEditar())
			{
				return ((EntityDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Ativo;
			}
			return false;
		}

		private void Desativar()
		{
			string observacao = string.Empty;
			if (DialogoFactory.MostrarDialogoEntradaString(ref observacao, "Observação", $"Informe uma observação para desativar o cheque número {((InclusaoChequeViewModel)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Numero}.", (IIcone)null, false, false, false, (string)null, false, true) == MessageBoxResult.OK)
			{
				ChequeDto val = _service.Canal<IChequeModuleService, ChequeDto>((Func<IChequeModuleService, ChequeDto>)((IChequeModuleService x) => x.DesativarCheque(new AcaoChequeDto
				{
					Observacao = HelperString.ToStringEmptyWhenNull(observacao),
					IdCheque = ((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Id
				})));
				_notificacao.MostrarNotificacao($"Cheque número {val.Numero} foi desativado");
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
			}
		}

		private void Compensar()
		{
			DateTime data = default(DateTime);
			string informacao = default(string);
			if (DialogoFactory.MostrarDialogoEntradaDataString(ref data, ((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Emissao, "Data de compensação", $"Informe a data de compensação do cheque número {((InclusaoChequeViewModel)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Numero}.", ref informacao, string.Empty, true, "Observação (Ex.: Banco onde foi compensado)", (IIcone)null) == MessageBoxResult.OK)
			{
				ChequeDto val = _service.Canal<IChequeModuleService, ChequeDto>((Func<IChequeModuleService, ChequeDto>)((IChequeModuleService x) => x.CompensarCheque(new AcaoChequeDto
				{
					Observacao = informacao,
					Data = data,
					IdCheque = ((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Id
				})));
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Atualizar(val);
				_notificacao.MostrarNotificacao($"Cheque número {val.Numero} foi compensado");
			}
		}

		private void Devolver()
		{
			DateTime data = default(DateTime);
			string informacao = default(string);
			if (DialogoFactory.MostrarDialogoEntradaDataString(ref data, ((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Emissao, "Data de devolução", $"Informe a data de devolução do cheque número {((InclusaoChequeViewModel)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Numero}.", ref informacao, string.Empty, true, "Observação", (IIcone)null) == MessageBoxResult.OK)
			{
				ChequeDto val = _service.Canal<IChequeModuleService, ChequeDto>((Func<IChequeModuleService, ChequeDto>)((IChequeModuleService x) => x.DevolverCheque(new AcaoChequeDto
				{
					Observacao = informacao,
					IdCheque = ((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Id,
					Data = data
				})));
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Atualizar(val);
				_notificacao.MostrarNotificacao($"Cheque número {val.Numero} foi devolvido");
			}
		}

		private void Repassar()
		{
			DateTime data = default(DateTime);
			string informacao = default(string);
			if (DialogoFactory.MostrarDialogoEntradaDataString(ref data, ((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto.Emissao, "Data de repasse", $"Informe a data de repasse do cheque número {((InclusaoChequeViewModel)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Numero}.", ref informacao, string.Empty, true, "Observação (Ex.: Destinatário do cheque)", (IIcone)null) == MessageBoxResult.OK)
			{
				ChequeDto val = _service.Canal<IChequeModuleService, ChequeDto>((Func<IChequeModuleService, ChequeDto>)((IChequeModuleService x) => x.RepassarCheque(new AcaoChequeDto
				{
					Observacao = informacao,
					IdCheque = ((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto).Id,
					Data = data
				})));
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Atualizar(val);
				_notificacao.MostrarNotificacao($"Cheque número {val.Numero} foi repassado");
			}
		}

		protected override void Adicionar()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			//IL_0047: Expected O, but got Unknown
			string viewAdicionarFicha = ((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).ViewAdicionarFicha;
			Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
			{
				if (navigationResult.Result == true)
				{
					((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Estado.AlterarAdicao();
				}
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("ChequeViewModel", (object)new ChequeViewModel(new ChequeDto(), ((ListaSelecaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).Sessao.Perfis.ItemSelecionado.Empresa));
			((NavigationBase)this).Navegar(viewAdicionarFicha, obj, val);
		}

		protected override async Task Editar()
		{
			ChequeDto dto = await _service.Canal<IChequeModuleService, Task<ChequeDto>>((Func<IChequeModuleService, Task<ChequeDto>>)((IChequeModuleService service) => ((IReferenciaAtualizarService<ChequeDto>)(object)service).AtualizarDtoAsync(((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.ObterDtoItemSelecionado())));
			ChequeListaViewModel chequeListaViewModel = this;
			string viewAtualizarFicha = ((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).ViewAtualizarFicha;
			Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
			{
				if (navigationResult.Result == true)
				{
					((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Estado.AlterarEdicao();
				}
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("ChequeViewModel", (object)new ChequeViewModel(dto, ((ListaSelecaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).Sessao.Perfis.ItemSelecionado.Empresa));
			((NavigationBase)chequeListaViewModel).Navegar(viewAtualizarFicha, obj, val);
		}

		private async Task Duplicar()
		{
			ChequeDto dto = ((ViewModelDtoBase<ChequeDto>)(object)((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Selecionado).Dto;
			_ = dto;
			dto = await _service.Canal<IChequeModuleService, Task<ChequeDto>>((Func<IChequeModuleService, Task<ChequeDto>>)((IChequeModuleService service) => ((IReferenciaAtualizarService<ChequeDto>)(object)service).AtualizarDtoAsync(dto)));
			dto.Duplicar();
			ChequeListaViewModel chequeListaViewModel = this;
			string viewAdicionarFicha = ((ListaManutencaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).ViewAdicionarFicha;
			Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
			{
				if (navigationResult.Result == true)
				{
					((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Estado.AlterarEdicao();
				}
			};
			NavigationParameters val = new NavigationParameters();
			val.Add("ChequeViewModel", (object)new ChequeViewModel(dto, ((ListaSelecaoBase<ChequeDto, ChequeViewModel, Cheque>)(object)this).Sessao.Perfis.ItemSelecionado.Empresa));
			((NavigationBase)chequeListaViewModel).Navegar(viewAdicionarFicha, obj, val);
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
			object obj = regionNavigationEventArgs.NavigationContext.Parameters["ChequeDto"];
			ChequeDto val = (ChequeDto)((obj is ChequeDto) ? obj : null);
			if (!(regionNavigationEventArgs.Uri.OriginalString != typeof(ChequeListaView).Name) && val != null)
			{
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Limpar();
				((ListaBase<ChequeDto, ChequeViewModel>)(object)this).Itens.Adicionar<ChequeDto>((IEnumerable<ChequeDto>)new List<ChequeDto> { val });
			}
		}
	}
	public class ChequeParcelasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ChequeParcelasAbaViewModel(IViewModelDtoBase viewModel)
			: base("Parcelas", "Pagar ou receber", viewModel)
		{
		}
	}
	public class ChequeParcelasViewModel : ViewModelDtoBase<ChequeViewModel>
	{
		public Lista<PagamentoRecebimentoDto> Parcelas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PagamentoRecebimentoDto>>((Expression<Func<Lista<PagamentoRecebimentoDto>>>)(() => Parcelas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PagamentoRecebimentoDto>>((Expression<Func<Lista<PagamentoRecebimentoDto>>>)(() => Parcelas), value);
			}
		}

		public ISelecaoCommandParameter ParcelasCommandParameter { get; set; }

		public ICommand ExcluirParcelaCommand { get; set; }

		public ChequeParcelasViewModel(ChequeViewModel chequeViewModel)
			: base(chequeViewModel)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			ExcluirParcelaCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				Parcelas.Remover(Parcelas.ItemSelecionado);
			}, (Func<bool>)(() => Parcelas.PossuiItemSelecionado), (bool?)null);
		}

		public override void ProjectedAsViewModel()
		{
			Parcelas = new Lista<PagamentoRecebimentoDto>();
			Parcelas.Adicionar(((ViewModelBase<ChequeDto>)(object)base.Dto).Model.Parcelas);
			CriarParcelaCommandParameter(((ViewModelBase<ChequeDto>)(object)base.Dto).Model.IsChequeEmitido);
			ObservableExtensions.Subscribe<TipoCheque>(base.Dto.Tipo.WhenAlterouItemSelecionado, (Action<TipoCheque>)delegate(TipoCheque tipo)
			{
				//IL_0003: Unknown result type (might be due to invalid IL or missing references)
				//IL_0009: Invalid comparison between Unknown and I4
				CriarParcelaCommandParameter((int)tipo.Tipo == 1);
			});
		}

		private void CriarParcelaCommandParameter(bool chequeEmitido)
		{
			ISelecaoCommandParameter parcelasCommandParameter;
			if (!chequeEmitido)
			{
				ISelecaoCommandParameter val = (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<RecebimentoDto>>("RecebimentoListaView", (Action<IEnumerable<RecebimentoDto>>)AdicionarParcelas, true);
				parcelasCommandParameter = val;
			}
			else
			{
				ISelecaoCommandParameter val = (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<PagamentoDto>>("PagamentoListaView", (Action<IEnumerable<PagamentoDto>>)AdicionarParcelas, true);
				parcelasCommandParameter = val;
			}
			ParcelasCommandParameter = parcelasCommandParameter;
			((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => ParcelasCommandParameter));
		}

		private void AdicionarParcelas(IEnumerable<PagamentoRecebimentoDto> parcelas)
		{
			foreach (PagamentoRecebimentoDto item in HelperIEnumerable.ToListEmptyWhenNull<PagamentoRecebimentoDto>(Parcelas.Itens.Where((PagamentoRecebimentoDto x) => parcelas.Select((PagamentoRecebimentoDto i) => ((EntityBaseDto)i).Id).Contains(((EntityBaseDto)x).Id))))
			{
				Parcelas.Remover(item);
			}
			Parcelas.Adicionar(parcelas);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			ChequeDto val = (ChequeDto)((obj is ChequeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Parcelas = Parcelas.Itens;
				base.AtualizarDto<ChequeDto>(ref val);
			}
		}
	}
	public class ChequesViewModel : ModuloBase
	{
		public ChequesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloChequesRegion, typeof(ChequeListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ChequeViewModel : InclusaoChequeViewModel
	{
		private readonly Pessoa _empresa;

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, true);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		public string NomePessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoa), value, (Action)RemoverPessoaQuandoNomeApagado);
			}
		}

		public Lista<TipoCheque> Tipo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TipoCheque>>((Expression<Func<Lista<TipoCheque>>>)(() => Tipo));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TipoCheque>>((Expression<Func<Lista<TipoCheque>>>)(() => Tipo), value);
			}
		}

		public string SubTituloPessoa
		{
			get
			{
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Invalid comparison between Unknown and I4
				Lista<TipoCheque> tipo = Tipo;
				if (tipo == null || (int)tipo.ItemSelecionado.Tipo != 1)
				{
					return "Recebido de";
				}
				return "Emitido para";
			}
		}

		public PessoaDto Pessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value);
			}
		}

		public DateTime Data
		{
			get
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Expected I4, but got Unknown
				SituacaoCheque situacao = ((ViewModelDtoBase<ChequeDto>)this).Dto.Situacao;
				switch (situacao - 1)
				{
				case 2:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.PreDatado;
				case 0:
				case 1:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.Emissao;
				case 3:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.Compensacao;
				case 4:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.Devolucao;
				case 5:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.Repassado;
				default:
					return ((ViewModelDtoBase<ChequeDto>)this).Dto.Emissao;
				}
			}
		}

		public string Situacao => HelperEnum.RetornarDescricao((Enum)(object)((ViewModelDtoBase<ChequeDto>)this).Dto.Situacao);

		public ChequeViewModel()
			: base(new ChequeDto())
		{
		}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown


		public ChequeViewModel(ChequeDto dto)
			: base(dto)
		{
		}

		public ChequeViewModel(ChequeDto dto, Pessoa empresa)
			: base(dto)
		{
			_empresa = empresa;
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)11, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void SelecionouPessoa(PessoaDto pessoa)
		{
			NomePessoa = pessoa.Nome;
			Pessoa = pessoa;
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomePessoa))
			{
				Pessoa = (PessoaDto)new PessoaDtoNull();
			}
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_013e: Expected O, but got Unknown
			((InclusaoChequeViewModel)this).Banco = ((ViewModelDtoBase<ChequeDto>)this).Dto.Banco;
			((InclusaoChequeViewModel)this).Agencia = ((ViewModelDtoBase<ChequeDto>)this).Dto.Agencia;
			((InclusaoChequeViewModel)this).Conta = ((ViewModelDtoBase<ChequeDto>)this).Dto.Conta;
			((InclusaoChequeViewModel)this).Numero = ((ViewModelDtoBase<ChequeDto>)this).Dto.Numero;
			((InclusaoChequeViewModel)this).Valor = ((ViewModelDtoBase<ChequeDto>)this).Dto.Valor;
			((InclusaoChequeViewModel)this).Emissao = ((((ViewModelDtoBase<ChequeDto>)this).Dto.Emissao == DateTime.MinValue) ? DateTime.Today : ((ViewModelDtoBase<ChequeDto>)this).Dto.Emissao);
			((InclusaoChequeViewModel)this).PreDatado = ((((ViewModelDtoBase<ChequeDto>)this).Dto.PreDatado == DateTime.MinValue) ? null : new DateTime?(((ViewModelDtoBase<ChequeDto>)this).Dto.PreDatado));
			((InclusaoChequeViewModel)this).NomeEmissor = ((ViewModelDtoBase<ChequeDto>)this).Dto.NomeEmissor;
			((InclusaoChequeViewModel)this).DocumentoEmissor = ((ViewModelDtoBase<ChequeDto>)this).Dto.DocumentoEmissor;
			((InclusaoChequeViewModel)this).Observacao = ((ViewModelDtoBase<ChequeDto>)this).Dto.Observacao;
			NomePessoa = ((ViewModelDtoBase<ChequeDto>)this).Dto.Pessoa;
			Pessoa = new PessoaDto
			{
				Nome = ((ViewModelDtoBase<ChequeDto>)this).Dto.Pessoa,
				Id = ((ViewModelDtoBase<ChequeDto>)this).Dto.PessoaReferencia,
				CpfCnpjFormatado = ((ViewModelDtoBase<ChequeDto>)this).Dto.PessoaCpfCnpj
			};
			CriarTipoCheque();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => SubTituloPessoa));
		}

		private void CriarTipoCheque()
		{
			if (HelperObject.IsNotNull((object)Tipo))
			{
				((ViewModelValidationBase)Tipo).Dispose();
			}
			Tipo = new Lista<TipoCheque>();
			if (((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)this).Dto).IsTransient())
			{
				Tipo.Adicionar(new TipoCheque((SituacaoCheque)2));
				Tipo.Adicionar(new TipoCheque((SituacaoCheque)1));
			}
			else
			{
				Tipo.Adicionar(((ViewModelDtoBase<ChequeDto>)this).Dto.IsChequeEmitido ? new TipoCheque((SituacaoCheque)1) : new TipoCheque((SituacaoCheque)2));
			}
			ObservableExtensions.Subscribe<TipoCheque>(Tipo.WhenAlterouItemSelecionado, (Action<TipoCheque>)delegate(TipoCheque tipoCheque)
			{
				//IL_0036: Unknown result type (might be due to invalid IL or missing references)
				//IL_003c: Invalid comparison between Unknown and I4
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => SubTituloPessoa));
				if ((int)tipoCheque.Tipo == 1 && ((EntityBaseDto)((ViewModelDtoBase<ChequeDto>)this).Dto).IsTransient())
				{
					PreencherDadosEmpresaNoEmissorCheque();
				}
			});
			if (((ViewModelDtoBase<ChequeDto>)this).Dto.IsChequeEmitido)
			{
				Tipo.PosicionarNoUltimo();
			}
			else
			{
				Tipo.PosicionarNoPrimeiro();
			}
		}

		private void PreencherDadosEmpresaNoEmissorCheque()
		{
			if (string.IsNullOrWhiteSpace(((InclusaoChequeViewModel)this).NomeEmissor) && string.IsNullOrWhiteSpace(((InclusaoChequeViewModel)this).DocumentoEmissor))
			{
				((InclusaoChequeViewModel)this).NomeEmissor = _empresa.Nome;
				((InclusaoChequeViewModel)this).DocumentoEmissor = _empresa.CpfCnpjFormatado;
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Invalid comparison between Unknown and I4
			object obj = dto;
			ChequeDto val = (ChequeDto)((obj is ChequeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.IsChequeEmitido = (int)Tipo.ItemSelecionado.Tipo == 1;
				val.Banco = ((InclusaoChequeViewModel)this).Banco;
				val.Agencia = ((InclusaoChequeViewModel)this).Agencia;
				val.Conta = ((InclusaoChequeViewModel)this).Conta;
				val.Numero = ((InclusaoChequeViewModel)this).Numero;
				val.Valor = ((InclusaoChequeViewModel)this).Valor;
				val.Emissao = ((InclusaoChequeViewModel)this).Emissao;
				val.PreDatado = ((InclusaoChequeViewModel)this).PreDatado ?? DateTime.MinValue;
				val.NomeEmissor = ((InclusaoChequeViewModel)this).NomeEmissor;
				val.DocumentoEmissor = ((InclusaoChequeViewModel)this).DocumentoEmissor;
				val.Pessoa = HelperString.ToStringEmptyWhenNull(NomePessoa);
				val.PessoaCpfCnpj = HelperString.ToStringEmptyWhenNull(Pessoa.CpfCnpjFormatado);
				val.PessoaReferencia = HelperString.ToStringEmptyWhenNull(((EntityBaseDto)Pessoa).Id);
				val.Observacao = ((InclusaoChequeViewModel)this).Observacao;
				((InclusaoChequeViewModel)this).AtualizarDto<ChequeDto>(ref val);
			}
		}

		public override IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ChequeFormulario(((ViewModelDtoBase<ChequeDto>)this).Dto, _empresa);
		}

		public override string ToTituloFicha()
		{
			return "Cheque";
		}

		public override string ToTituloLista()
		{
			return "Cheques";
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Tipo).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public struct TipoCheque
	{
		public string Descricao { get; }

		public int Codigo { get; }

		public SituacaoCheque Tipo { get; }

		public TipoCheque(SituacaoCheque situacaoCheque)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected I4, but got Unknown
			Tipo = situacaoCheque;
			Descricao = HelperEnum.RetornarDescricao((Enum)(object)situacaoCheque);
			Codigo = (int)situacaoCheque;
		}
	}
}
namespace Otus.Modulo.Cheques.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Cheques.Properties.Resources", typeof(Resources).Assembly);
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

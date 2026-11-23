using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Agendamentos;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Application.Main.Dto.Industrias.FichasTecnicas;
using DigisatServer.Application.Main.Dto.Industrias.OrdensProducao;
using DigisatServer.Application.Main.Dto.Industrias.Programacoes;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.IndustriasModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Icones;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesMovimentacao;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using NClone;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.ViewModels.Tipos;
using Otus.Infraestrutura.Views;
using Otus.Infraestrutura.Views.Estoques.Grades;
using Otus.Infraestrutura.Views.Estoques.Lotes;
using Otus.Infraestrutura.Views.Estoques.Seriais;
using Otus.Modulo.OrdensProducao.Reports;
using Otus.Modulo.OrdensProducao.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.OrdensProducao")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.OrdensProducao")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.OrdensProducao
{
	[Module(ModuleName = "OrdemProducaoModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("AtualizacaoCustoMateriaPrimaModulo")]
	[ModuleDependency("RelatorioModulo")]
	[ModuleDependency("EstoqueFisicoModulo")]
	public class OrdemProducaoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public OrdemProducaoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<OrdensProducaoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdemProducaoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdemProducaoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdemProducaoFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EstoqueOrdemProducaoFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<SerialListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<LoteFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GradeFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<SerialFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ControleEtapaFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<DesmontarFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.OrdensProducao.Views
{
	public class ControleEtapaFichaView : CustomUserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public ControleEtapaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/controleetapafichaview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class DesmontarFichaView : CustomUserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public DesmontarFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/desmontarfichaview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class EstoqueOrdemProducaoFichaView : CustomUserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public EstoqueOrdemProducaoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/estoqueordemproducaofichaview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class OrdemProducaoFichaAdicionarView : CustomUserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public OrdemProducaoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/ordemproducaofichaadicionarview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class OrdemProducaoFichaAtualizarView : CustomUserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public OrdemProducaoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/ordemproducaofichaatualizarview.xaml", UriKind.Relative);
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
				FichaBase = (FichaBasePersistir)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class OrdemProducaoListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdemProducaoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/ordemproducaolistaview.xaml", UriKind.Relative);
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
	public class OrdensProducaoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdensProducaoView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensProducao;component/views/ordensproducaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.OrdensProducao.ViewModels
{
	public class ColaboradoresViewModel : BindableBase, IDisposable
	{
		public ISelecaoCommandParameter PessoaCommandParameter
		{
			get
			{
				SelecaoCommandParameter<IEnumerable<PessoaDto>> obj = new SelecaoCommandParameter<IEnumerable<PessoaDto>>("PessoaListaView", (Action<IEnumerable<PessoaDto>>)delegate(IEnumerable<PessoaDto> pessoas)
				{
					//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
					//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
					//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
					//IL_00e6: Expected O, but got Unknown
					foreach (PessoaDto pessoaDto in pessoas)
					{
						if (!Colaboradores.Existe((Expression<Func<ColaboradorEtapaOrdemProducaoDto, bool>>)((ColaboradorEtapaOrdemProducaoDto x) => x.PessoaReferencia.Equals(((EntityBaseDto)pessoaDto).Id))))
						{
							Colaboradores.Adicionar(new ColaboradorEtapaOrdemProducaoDto
							{
								PessoaReferencia = ((EntityBaseDto)pessoaDto).Id,
								PessoaNome = pessoaDto.Nome
							});
						}
					}
				}, true);
				obj.AdicionarParametro("InformacaoPesquisa", (object)"ColaboradorIndustria");
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public Lista<ColaboradorEtapaOrdemProducaoDto> Colaboradores
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ColaboradorEtapaOrdemProducaoDto>>((Expression<Func<Lista<ColaboradorEtapaOrdemProducaoDto>>>)(() => Colaboradores));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ColaboradorEtapaOrdemProducaoDto>>((Expression<Func<Lista<ColaboradorEtapaOrdemProducaoDto>>>)(() => Colaboradores), value);
			}
		}

		public ICommand RemoverColaboradorCommand { get; set; }

		public ColaboradoresViewModel(IEnumerable<ColaboradorEtapaOrdemProducaoDto> colaboradores)
		{
			RemoverColaboradorCommand = (ICommand)new DelegateCommand<object>((Action<object>)RemoverColaborador, (Func<object, bool>)PodeRemoverColaborador, (bool?)null);
			Colaboradores = new Lista<ColaboradorEtapaOrdemProducaoDto>();
			Colaboradores.Adicionar(colaboradores);
		}

		private void RemoverColaborador(object colaborador)
		{
			Colaboradores.Remover((ColaboradorEtapaOrdemProducaoDto)((colaborador is ColaboradorEtapaOrdemProducaoDto) ? colaborador : null));
		}

		private bool PodeRemoverColaborador(object colaborador)
		{
			return colaborador is ColaboradorEtapaOrdemProducaoDto;
		}

		public void Dispose()
		{
			((ViewModelValidationBase)Colaboradores).Dispose();
		}
	}
	public class ControleEtapaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ControleEtapaAbaViewModel(EtapaProducaoViewModel viewModel)
			: base(((ViewModelDtoBase<EtapaOrdemProducaoDto>)viewModel).Dto.Descricao, viewModel.Situacao, (IViewModelDtoBase)(object)viewModel)
		{
		}
	}
	public class ControleEtapaFichaViewModel : FichaBase<OrdemProducaoDto, ControleEtapaViewModel>
	{
		private readonly IWService _service;

		private OrdemProducaoDto _ordemProducaoDto;

		public ICommand GravarCommand { get; set; }

		public ControleEtapaFichaViewModel(IWService service)
			: base("Controle de etapas de produção")
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			_service = service;
			GravarCommand = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNotNull((object)base.Formulario))
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		private async Task Gravar()
		{
			if (!((ControleEtapaFormulario)(object)((FichaBase<OrdemProducaoDto, ControleEtapaViewModel>)this).Formulario).PodeGravar())
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			OrdemProducaoDto dto = ((IFormularioEntity)((FichaBase<OrdemProducaoDto, ControleEtapaViewModel>)this).Formulario).ToDto<OrdemProducaoDto>();
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService x) => x.SalvarControleEtapaAsync(dto))).ContinueWith(delegate(Task<OrdemProducaoDto> task)
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
						_ordemProducaoDto = task.Result;
						((NavigationBase)this).ParametroRetornado += OnParametroRetornado;
						base.DisableConfirmOnClose();
						((NavigationBase)this).FecharView();
					});
				}
			});
		}

		private void OnParametroRetornado(object sender, NavigationContext e)
		{
			e.Parameters.Add("OrdemProducaoDto", (object)_ordemProducaoDto);
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroRetornado -= OnParametroRetornado;
			GravarCommand = null;
			base.Dispose();
		}
	}
	public class ControleEtapaFormulario : FormularioEntity<OrdemProducaoDto>
	{
		private readonly IEnumerable<ControleEtapaAbaViewModel> _viewModels;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IWService _service;

		public ControleEtapaFormulario(OrdemProducaoDto dto, IImpressaoRelatorioService impressaoRelatorioService, IWService service)
			: base(dto)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
			_service = service;
			_viewModels = HelperIEnumerable.ToListEmptyWhenNull<ControleEtapaAbaViewModel>(dto.Etapas.Select((EtapaOrdemProducaoDto etapa) => new ControleEtapaAbaViewModel(new EtapaProducaoViewModel(etapa, AlterouDataHoraFim, PodeIniciarEtapa, ImprimirEtapa, CriarAgendamentoProximaEtapa))));
			CriarAbas((IEnumerable<IAbaViewModelBase>)_viewModels);
		}

		private void AlterouDataHoraFim()
		{
			bool num = _viewModels.Select((ControleEtapaAbaViewModel x) => ((AbaViewModelBase)x).ViewModel).OfType<EtapaProducaoViewModel>().All((EtapaProducaoViewModel x) => x.Terminado);
			IAbaViewModelBase val = ((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is EstoqueOrdemProducaoAbaViewModel);
			if (num && HelperObject.IsNull((object)val))
			{
				((Formulario)this).InserirProjetarAba((IAbaViewModelBase)(object)new EstoqueOrdemProducaoAbaViewModel((IViewModelPrincipal)(object)new EstoqueOrdemProducaoViewModel(base.Dto, criarMateriasPrimas: false), "Estoque"), true);
			}
		}

		private Dictionary<long, EtapaProducaoViewModel> CriarAgendamentoProximaEtapa()
		{
			return new Dictionary<long, EtapaProducaoViewModel> { 
			{
				base.Dto.Numero,
				_viewModels.Select((ControleEtapaAbaViewModel x) => ((AbaViewModelBase)x).ViewModel).OfType<EtapaProducaoViewModel>().FirstOrDefault((EtapaProducaoViewModel x) => !x.Iniciado)
			} };
		}

		private bool PodeIniciarEtapa(EtapaOrdemProducaoDto etapaOrdemProducaoDto)
		{
			if (DadosSistema.ConfiguracaoDto.Industria.PermitirProducaoSemMateriaPrima.Valor)
			{
				return true;
			}
			OrdemProducaoDto val = ObterDto();
			if (val.Etapas.FirstOrDefault((EtapaOrdemProducaoDto x) => x.FaseProducaoReferencia.Equals(etapaOrdemProducaoDto.FaseProducaoReferencia)).Itens.Sum((ItemEstoqueOrdemProducaoDto i) => i.QuantidadeFalta) > 0m)
			{
				if (DialogoFactory.MostrarDialogoSimNao("Não foi possível iniciar a etapa de produção. Não há matérias-primas suficientes!\nDeseja imprimir relatório de itens em falta?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return false;
				}
				MateriasPrimasFalta materiasPrimasFalta = new MateriasPrimasFalta(val);
				_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)materiasPrimasFalta);
				return false;
			}
			return true;
		}

		private OrdemProducaoDto ObterDto()
		{
			return _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService service) => service.ObterOrdemProducaoDtoAsync(((EntityBaseDto)base.Dto).Id))).Result;
		}

		private void ImprimirEtapa(EtapaOrdemProducaoDto etapaOrdemProducao)
		{
			_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoControleEtapa(base.Dto, new List<EtapaOrdemProducaoDto> { etapaOrdemProducao }));
		}

		private void CriarAbas(IEnumerable<IAbaViewModelBase> viewModels)
		{
			((Formulario)this).AdicionarProjetarAbas(viewModels.ToArray());
			ControleEtapaAbaViewModel value = _viewModels.FirstOrDefault(delegate(ControleEtapaAbaViewModel x)
			{
				EtapaProducaoViewModel etapaProducaoViewModel = ((AbaViewModelBase)x).ViewModel as EtapaProducaoViewModel;
				return HelperObject.IsNotNull((object)etapaProducaoViewModel) && ((!etapaProducaoViewModel.Terminado && etapaProducaoViewModel.Iniciado) || (!etapaProducaoViewModel.Terminado && !etapaProducaoViewModel.Iniciado));
			});
			((Formulario)this).AbaSelecionada = ((Formulario)this).Abas.IndexOf((IAbaViewModelBase)(object)value);
		}

		public override void AdicionarProjetarAbas(params IAbaViewModelBase[] abas)
		{
			((Formulario)this).AdicionarAbas((IEnumerable<IAbaViewModelBase>)abas);
			for (short num = 0; num < HelperIEnumerable.ToListEmptyWhenNull<IAbaViewModelBase>((IEnumerable<IAbaViewModelBase>)abas).Count; num++)
			{
				int num2 = num + 1;
				abas[num].SetarNumero((short)num2);
				abas[num].ViewModel.ProjectedAsViewModel();
			}
		}

		public bool PodeGravar()
		{
			if (PodeGravarComQuantidadeZero())
			{
				return PodeTerminarOrdemProducao();
			}
			return false;
		}

		private bool PodeGravarComQuantidadeZero()
		{
			IEnumerable<ControleEtapaAbaViewModel> source = _viewModels.Where(delegate(ControleEtapaAbaViewModel x)
			{
				EtapaProducaoViewModel etapaProducaoViewModel = ((AbaViewModelBase)x).ViewModel as EtapaProducaoViewModel;
				return HelperObject.IsNotNull((object)etapaProducaoViewModel) && etapaProducaoViewModel.Terminado && !((ViewModelDtoBase<EtapaOrdemProducaoDto>)etapaProducaoViewModel).Dto.Concluida && etapaProducaoViewModel.MateriaPrimaViewModel.Itens.Itens.Any((IItemEstoqueOrdemProducaoViewModel i) => i.Quantidade == 0m);
			});
			if (!source.Any())
			{
				return true;
			}
			string arg = string.Join(", ", source.Select((ControleEtapaAbaViewModel x) => ((AbaViewModelBase)x).Titulo));
			if (DialogoFactory.MostrarDialogoSimNao($"As seguintes etapas possuem matérias-primas sem quantidade informada:\n'{arg}'\nDeseja continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				return true;
			}
			((Formulario)this).AbaSelecionada = ((Formulario)this).Abas.IndexOf((IAbaViewModelBase)(object)source.FirstOrDefault());
			return false;
		}

		private bool PodeTerminarOrdemProducao()
		{
			if (_viewModels.Select((ControleEtapaAbaViewModel x) => ((AbaViewModelBase)x).ViewModel).OfType<EtapaProducaoViewModel>().All((EtapaProducaoViewModel x) => x.Terminado))
			{
				return DialogoFactory.MostrarDialogoSimNao("Deseja terminar a ordem de produção?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes;
			}
			return true;
		}
	}
	public class ControleEtapaViewModel : ViewModelDtoBase<OrdemProducaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private readonly IWService _service;

		public ControleEtapaViewModel(OrdemProducaoDto dto, IImpressaoRelatorioService impressaoRelatorioService, IWService service)
			: base(dto)
		{
			_impressaoRelatorioService = impressaoRelatorioService;
			_service = service;
		}

		public override void ProjectedAsViewModel()
		{
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ControleEtapaFormulario(base.Dto, _impressaoRelatorioService, _service);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
	public class DesmontarAbaViewModel : AbaPrincipalViewModelBase
	{
		public DesmontarAbaViewModel(IViewModelPrincipal viewModel)
			: base("Desmontar", "Produto e matérias-primas", viewModel)
		{
		}
	}
	public class DesmontarFichaViewModel : FichaBase<OrdemDesmontagemDto, DesmontarViewModel>
	{
		private readonly IWService _service;

		private OrdemProducaoDto _ordemProducaoDto;

		public ICommand GravarCommand { get; set; }

		public DesmontarFichaViewModel(IWService service)
			: base(string.Empty)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			_service = service;
			GravarCommand = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNotNull((object)base.Formulario))
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		private async Task Gravar()
		{
			OrdemDesmontagemDto dto = ((IFormularioEntity)((FichaBase<OrdemDesmontagemDto, DesmontarViewModel>)this).Formulario).ToDto<OrdemDesmontagemDto>();
			if (DialogoFactory.MostrarDialogoSimNao((dto.MateriasPrimas.Any((ItemOrdemDesmontagemDto x) => x.EstoqueRetornado.Quantidade == 0m) ? "Existem quantidades a serem retornadas com informação ZERO.\n" : string.Empty) + "Essa ação não poderá ser desfeita. Continuar?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService x) => x.SalvarDesmontagemAsync(dto))).ContinueWith(delegate(Task<OrdemProducaoDto> task)
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
						_ordemProducaoDto = task.Result;
						((NavigationBase)this).ParametroRetornado += OnParametroRetornado;
						base.DisableConfirmOnClose();
						((NavigationBase)this).FecharView();
					});
				}
			});
		}

		private void OnParametroRetornado(object sender, NavigationContext e)
		{
			e.Parameters.Add("OrdemProducaoDto", (object)_ordemProducaoDto);
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroRetornado -= OnParametroRetornado;
			GravarCommand = null;
			base.Dispose();
		}
	}
	public class DesmontarFormulario : FormularioEntity<OrdemDesmontagemDto>
	{
		public DesmontarFormulario(OrdemDesmontagemDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new DesmontarAbaViewModel((IViewModelPrincipal)(object)new DesmontarViewModel(base.Dto)) });
		}
	}
	public class DesmontarViewModel : ViewModelDtoBase<OrdemDesmontagemDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public DateTime DataHoraInicio
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio), value);
			}
		}

		public DateTime DataHoraFim
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraFim));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraFim), value);
			}
		}

		[Valido]
		public ItemDesmontagemViewModel ProdutoProduzido { get; set; }

		[Valido]
		public ItensDesmontagemViewModel ItensDesmontagem { get; set; }

		public ColaboradoresViewModel ColaboradoresViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel), value);
			}
		}

		public DesmontarViewModel(OrdemDesmontagemDto ordemDesmontagemDto)
			: base(ordemDesmontagemDto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			DataHoraInicio = base.Dto.DataHoraInicio;
			DataHoraFim = base.Dto.DataHoraFim;
			ProdutoProduzido = new ItemDesmontagemViewModel(base.Dto.ProdutoProduzido, delegate
			{
				((BindableBase)this).RaisePropertyChanged<ItemDesmontagemViewModel>((Expression<Func<ItemDesmontagemViewModel>>)(() => ProdutoProduzido));
			}, produtoProduzido: true);
			ItensDesmontagem = new ItensDesmontagemViewModel(base.Dto.MateriasPrimas, delegate
			{
				((BindableBase)this).RaisePropertyChanged<ItensDesmontagemViewModel>((Expression<Func<ItensDesmontagemViewModel>>)(() => ItensDesmontagem));
			});
			ColaboradoresViewModel = new ColaboradoresViewModel(base.Dto.Colaboradores);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			OrdemDesmontagemDto val = (OrdemDesmontagemDto)((obj is OrdemDesmontagemDto) ? obj : null);
			val.DataHoraInicio = DataHoraInicio;
			val.DataHoraFim = DataHoraFim;
			val.ProdutoProduzido = ProdutoProduzido.Dto;
			val.Colaboradores = ColaboradoresViewModel.Colaboradores.Itens;
			val.MateriasPrimas = HelperIEnumerable.ToListEmptyWhenNull<ItemOrdemDesmontagemDto>(ItensDesmontagem.Lista.Select((ItemDesmontagemViewModel x) => x.Dto));
			base.AtualizarDto<OrdemDesmontagemDto>(ref val);
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new DesmontarFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public string ToTituloFicha()
		{
			return "Desmontar produto produzido";
		}

		public override void Dispose()
		{
			ColaboradoresViewModel.Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EstoqueOrdemProducaoAbaViewModel : AbaPrincipalViewModelBase
	{
		public EstoqueOrdemProducaoAbaViewModel(IViewModelPrincipal viewModel, string subtitulo = "Matérias-primas")
			: base("Produto produzido", subtitulo, viewModel)
		{
		}
	}
	public class EstoqueOrdemProducaoFichaViewModel : FichaBase<EstoqueOrdemProducaoDto, EstoqueOrdemProducaoViewModel>
	{
		private readonly IWService _service;

		private OrdemProducaoDto _ordemProducaoDto;

		public ICommand GravarCommand { get; set; }

		public EstoqueOrdemProducaoFichaViewModel(IWService service)
			: base(string.Empty)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			_service = service;
			GravarCommand = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNotNull((object)base.Formulario))
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		private async Task Gravar()
		{
			if (!((EstoqueOrdemProducaoFormulario)(object)((FichaBase<EstoqueOrdemProducaoDto, EstoqueOrdemProducaoViewModel>)this).Formulario).PodeGravar() || DialogoFactory.MostrarDialogoSimNao("Deseja terminar a ordem de produção?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			OrdemProducaoDto dto = ((IFormularioEntity)((FichaBase<EstoqueOrdemProducaoDto, EstoqueOrdemProducaoViewModel>)this).Formulario).ToDto<OrdemProducaoDto>();
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService x) => x.ConcluirAsync(dto))).ContinueWith(delegate(Task<OrdemProducaoDto> task)
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
						_ordemProducaoDto = task.Result;
						((NavigationBase)this).ParametroRetornado += OnParametroRetornado;
						base.DisableConfirmOnClose();
						((NavigationBase)this).FecharView();
					});
				}
			});
		}

		private void OnParametroRetornado(object sender, NavigationContext e)
		{
			e.Parameters.Add("OrdemProducaoDto", (object)_ordemProducaoDto);
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroRetornado -= OnParametroRetornado;
			GravarCommand = null;
			base.Dispose();
		}
	}
	public class EstoqueOrdemProducaoFormulario : FormularioEntity<OrdemProducaoDto>
	{
		private readonly EstoqueOrdemProducaoViewModel _viewModel;

		public EstoqueOrdemProducaoFormulario(OrdemProducaoDto dto)
			: base(dto)
		{
			_viewModel = new EstoqueOrdemProducaoViewModel(base.Dto);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new EstoqueOrdemProducaoAbaViewModel((IViewModelPrincipal)(object)_viewModel) });
		}

		public bool PodeGravar()
		{
			if (_viewModel.MateriaPrimaViewModel.Itens.Itens.All((IItemEstoqueOrdemProducaoViewModel x) => x.Quantidade != 0m))
			{
				return true;
			}
			return DialogoFactory.MostrarDialogoSimNao("Existem matérias-primas sem quantidade informada. Deseja continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes;
		}
	}
	public class EstoqueOrdemProducaoViewModel : ViewModelDtoBase<OrdemProducaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly bool _criarMateriasPrimas;

		private static DateTime _dataHoraInicio;

		public DateTime DataHoraInicio
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio), value, (Action)delegate
				{
					_dataHoraInicio = value;
				});
			}
		}

		[CustomValidation(typeof(EstoqueOrdemProducaoViewModel), "ValidarDataFim", ErrorMessage = "O campo 'Data/Hora fim' não pode ser menor que Data/Hora início.")]
		public DateTime DataHoraFim
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraFim));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraFim), value);
			}
		}

		public MateriaPrimaViewModel MateriaPrimaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<MateriaPrimaViewModel>((Expression<Func<MateriaPrimaViewModel>>)(() => MateriaPrimaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<MateriaPrimaViewModel>((Expression<Func<MateriaPrimaViewModel>>)(() => MateriaPrimaViewModel), value);
			}
		}

		[Valid]
		public EstoqueProdutoProduzidoViewModel EstoqueProdutoProduzidoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<EstoqueProdutoProduzidoViewModel>((Expression<Func<EstoqueProdutoProduzidoViewModel>>)(() => EstoqueProdutoProduzidoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<EstoqueProdutoProduzidoViewModel>((Expression<Func<EstoqueProdutoProduzidoViewModel>>)(() => EstoqueProdutoProduzidoViewModel), value);
			}
		}

		public ColaboradoresViewModel ColaboradoresViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel), value);
			}
		}

		public EstoqueOrdemProducaoViewModel()
		{
		}

		public EstoqueOrdemProducaoViewModel(OrdemProducaoDto dto, bool criarMateriasPrimas = true)
			: base(dto)
		{
			_criarMateriasPrimas = criarMateriasPrimas;
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarDataFim(object value, ValidationContext context)
		{
			if (!(HelperDate.TryToDateTime(value).Date >= _dataHoraInicio.Date))
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		public override void ProjectedAsViewModel()
		{
			DataHoraInicio = base.Dto.Etapas.FirstOrDefault().DataHoraInicio;
			DataHoraFim = (base.Dto.Etapas.FirstOrDefault().DataHoraFim.Equals(DateTime.MinValue) ? DateTime.Now : base.Dto.Etapas.FirstOrDefault().DataHoraFim);
			if (_criarMateriasPrimas)
			{
				MateriaPrimaViewModel = new MateriaPrimaViewModel(base.Dto.Etapas.FirstOrDefault().Itens);
				((ViewModelBase<IEnumerable<ItemEstoqueOrdemProducaoDto>>)MateriaPrimaViewModel).ProjectedAsViewModel();
				ColaboradoresViewModel = new ColaboradoresViewModel(base.Dto.Etapas.FirstOrDefault().Colaboradores);
			}
			EstoqueProdutoProduzidoViewModel = new EstoqueProdutoProduzidoViewModel(base.Dto.ProdutoProduzido);
			((BindableBase)EstoqueProdutoProduzidoViewModel).PropertyChanged += EstoqueProdutoProduzidoViewModelOnPropertyChanged;
			((ViewModelBase<ItemEstoqueOrdemProducaoDto>)(object)EstoqueProdutoProduzidoViewModel).ProjectedAsViewModel();
		}

		private void EstoqueProdutoProduzidoViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			((BindableBase)this).RaisePropertyChanged<EstoqueProdutoProduzidoViewModel>((Expression<Func<EstoqueProdutoProduzidoViewModel>>)(() => EstoqueProdutoProduzidoViewModel));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			OrdemProducaoDto val = (OrdemProducaoDto)((obj is OrdemProducaoDto) ? obj : null);
			if (HelperObject.IsNull((object)val))
			{
				return;
			}
			if (_criarMateriasPrimas)
			{
				val.Etapas.FirstOrDefault().Itens = MateriaPrimaViewModel.Itens.Itens.Select((IItemEstoqueOrdemProducaoViewModel x) => x.ObterDto());
				val.Etapas.FirstOrDefault().Colaboradores = ColaboradoresViewModel.Colaboradores.Itens;
				val.Etapas.FirstOrDefault().DataHoraInicio = DataHoraInicio;
				val.Etapas.FirstOrDefault().DataHoraFim = DataHoraFim;
			}
			val.ProdutoProduzido.EstoqueSaida = ((ViewModelDtoBase<ItemEstoqueOrdemProducaoDto>)EstoqueProdutoProduzidoViewModel).Dto.EstoqueSaida;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new EstoqueOrdemProducaoFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Estoques da ordem de produção";
		}

		public string ToTituloFicha()
		{
			return "Estoque da ordem de produção";
		}

		public override void Dispose()
		{
			MateriaPrimaViewModel materiaPrimaViewModel = MateriaPrimaViewModel;
			if (materiaPrimaViewModel != null)
			{
				((ViewModelValidationBase)materiaPrimaViewModel).Dispose();
			}
			ColaboradoresViewModel?.Dispose();
			((BindableBase)EstoqueProdutoProduzidoViewModel).PropertyChanged -= EstoqueProdutoProduzidoViewModelOnPropertyChanged;
			EstoqueProdutoProduzidoViewModel estoqueProdutoProduzidoViewModel = EstoqueProdutoProduzidoViewModel;
			if (estoqueProdutoProduzidoViewModel != null)
			{
				((ViewModelValidationBase)estoqueProdutoProduzidoViewModel).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EstoqueProdutoProduzidoViewModel : ViewModelDtoBase<ItemEstoqueOrdemProducaoDto>
	{
		public TipoEstoqueNormalComboBoxViewModel TiposEstoque { get; set; }

		public TipoEstoqueBaseViewModel TipoEstoque { get; set; }

		public bool TipoVisivel { get; set; }

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "O campo 'Quantidade' deve ser maior que zero.")]
		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value, (Action)delegate
				{
					base.Dto.EstoqueSaida.Quantidade = Quantidade;
				});
			}
		}

		public EstoqueProdutoProduzidoViewModel(ItemEstoqueOrdemProducaoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			TipoVisivel = base.Dto.Estoque.Quantidade.Equals(0m) && base.Dto.Estoque.PodeAdicionarEstoqueInicial;
			if (TipoVisivel)
			{
				TiposEstoque = (TipoEstoqueNormalComboBoxViewModel)new TipoEstoqueLoteComboBoxViewModel(base.Dto.Estoque);
				((Lista<TipoEstoque>)(object)TiposEstoque).Posicionar((Expression<Func<TipoEstoque, bool>>)((TipoEstoque x) => ((object)x.EstoqueDto).GetType() == ((object)((ViewModelDtoBase<ItemEstoqueOrdemProducaoDto>)this).Dto.Estoque).GetType()));
				((Lista<TipoEstoque>)(object)TiposEstoque).AlterouItemSelecionadoEvent += TiposEstoqueOnAlterouItemSelecionadoEvent;
				{
					foreach (TipoEstoque iten in ((Lista<TipoEstoque>)(object)TiposEstoque).Itens)
					{
						if (HelperObject.IsNot<TipoEstoqueNormalViewModel>((object)TiposEstoque))
						{
							iten.ViewModelBase.QuantidadeVisivel = Visibility.Collapsed;
						}
						((BindableBase)iten.ViewModelBase).PropertyChanged += ViewModelBaseOnPropertyChanged;
						if (((ViewModelDtoBase<EstoqueDto>)(object)iten.ViewModelBase).Dto.IsNormal)
						{
							iten.ViewModelBase.Quantidade = base.Dto.QuantidadeNecessaria;
						}
					}
					return;
				}
			}
			TipoEstoque = ((TipoEstoqueViewModel<SerialEntradaViewModel, GradePrincipalEntradaViewModel, LoteEntradaViewModel>)new TipoEstoqueEntradaViewModel(base.Dto.EstoqueSaida, base.Dto.Estoque)).ObterTipoEstoque().ViewModelBase;
			if (HelperObject.IsNot<TipoEstoqueNormalViewModel>((object)TipoEstoque))
			{
				TipoEstoque.QuantidadeVisivel = Visibility.Collapsed;
			}
			((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade = base.Dto.QuantidadeNecessaria;
			((BindableBase)TipoEstoque).PropertyChanged += TipoEstoqueViewModelBaseOnPropertyChanged;
			((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
		}

		private void TiposEstoqueOnAlterouItemSelecionadoEvent(object sender, TipoEstoque estoque)
		{
			AlterouItemSelecionado(estoque.ViewModelBase);
		}

		private void ViewModelBaseOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName.Equals("Quantidade"))
			{
				AlterouItemSelecionado(((Lista<TipoEstoque>)(object)TiposEstoque).ItemSelecionado.ViewModelBase);
			}
		}

		private void TipoEstoqueViewModelBaseOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName.Equals("Quantidade"))
			{
				AlterouItemSelecionado(TipoEstoque);
			}
		}

		private void AlterouItemSelecionado(TipoEstoqueBaseViewModel item)
		{
			base.Dto.EstoqueSaida = ((ViewModelDtoBase<EstoqueDto>)(object)item).Dto;
			((EntityBaseDto)base.Dto.EstoqueSaida).Id = ((EntityBaseDto)base.Dto.Estoque).Id;
			if (base.Dto.EstoqueSaida.IsNormal)
			{
				item.QuantidadeVisivel = Visibility.Visible;
			}
			Quantidade = item.Quantidade;
		}

		public override void Dispose()
		{
			if (HelperObject.IsNotNull((object)TiposEstoque))
			{
				((Lista<TipoEstoque>)(object)TiposEstoque).AlterouItemSelecionadoEvent -= TiposEstoqueOnAlterouItemSelecionadoEvent;
				foreach (TipoEstoque iten in ((Lista<TipoEstoque>)(object)TiposEstoque).Itens)
				{
					((BindableBase)iten.ViewModelBase).PropertyChanged -= ViewModelBaseOnPropertyChanged;
				}
				((ViewModelValidationBase)TiposEstoque).Dispose();
			}
			if (HelperObject.IsNotNull((object)TipoEstoque))
			{
				((BindableBase)TipoEstoque).PropertyChanged -= TipoEstoqueViewModelBaseOnPropertyChanged;
				((ViewModelValidationBase)TipoEstoque).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EtapaProducaoViewModel : ViewModelDtoBase<EtapaOrdemProducaoDto>
	{
		private readonly Action _alterouDataHoraFim;

		private readonly Func<EtapaOrdemProducaoDto, bool> _podeIniciarEtapa;

		private readonly Func<Dictionary<long, EtapaProducaoViewModel>> _criarAgendamentoProximaEtapa;

		private UsuarioDto _usuario;

		private Dictionary<long, EtapaProducaoViewModel> _proximaEtapa;

		private long _numero;

		public DateTime DataAgendamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataAgendamento));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataAgendamento), value);
			}
		}

		public PessoaDto PessoaAgendamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaAgendamento));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaAgendamento), value, (Action)delegate
				{
					NomePessoaAgendamento = ObterNomePessoaAgendamento();
				});
			}
		}

		public string NomePessoaAgendamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoaAgendamento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoaAgendamento), value, (Action)RemoverPessoaAgendamentoQuandoNomeApagado);
			}
		}

		public Visibility CamposAgendamentoVisivel
		{
			get
			{
				if (!Terminado || !HelperObject.IsNotNull((object)PessoaAgendamento))
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public SelecaoCommandParameter<PessoaDto> PessoaCommandParameter => new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto p)
		{
			PessoaAgendamento = p;
		}, true);

		public ICommand PesquisarPessoaAgendamentoCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoaAgendamento);

		public bool Iniciado
		{
			get
			{
				if (DataHoraInicio.HasValue)
				{
					DateTime? dataHoraInicio = DataHoraInicio;
					DateTime minValue = DateTime.MinValue;
					if (!dataHoraInicio.HasValue)
					{
						return true;
					}
					if (!dataHoraInicio.HasValue)
					{
						return false;
					}
					return dataHoraInicio.GetValueOrDefault() != minValue;
				}
				return false;
			}
		}

		public bool Terminado
		{
			get
			{
				if (DataHoraFim.HasValue)
				{
					DateTime? dataHoraFim = DataHoraFim;
					DateTime minValue = DateTime.MinValue;
					if (!dataHoraFim.HasValue)
					{
						return true;
					}
					if (!dataHoraFim.HasValue)
					{
						return false;
					}
					return dataHoraFim.GetValueOrDefault() != minValue;
				}
				return false;
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

		public DateTime? DataHoraInicio
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime?>((Expression<Func<DateTime?>>)(() => DataHoraInicio));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime?>((Expression<Func<DateTime?>>)(() => DataHoraInicio), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => Iniciado));
				});
			}
		}

		public DateTime? DataHoraFim
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime?>((Expression<Func<DateTime?>>)(() => DataHoraFim));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime?>((Expression<Func<DateTime?>>)(() => DataHoraFim), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => Terminado));
				});
			}
		}

		public ColaboradoresViewModel ColaboradoresViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<ColaboradoresViewModel>((Expression<Func<ColaboradoresViewModel>>)(() => ColaboradoresViewModel), value);
			}
		}

		public string DuracaoPrevista => base.Dto.DuracaoPrevista;

		public string Situacao
		{
			get
			{
				string text = (base.Dto.Concluida ? "terminada" : ((base.Dto.DataHoraInicio == DateTime.MinValue) ? "aguardando início" : "iniciada"));
				return "Etapa " + text;
			}
		}

		public MateriaPrimaViewModel MateriaPrimaViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<MateriaPrimaViewModel>((Expression<Func<MateriaPrimaViewModel>>)(() => MateriaPrimaViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<MateriaPrimaViewModel>((Expression<Func<MateriaPrimaViewModel>>)(() => MateriaPrimaViewModel), value);
			}
		}

		public ICommand IniciarCommand { get; set; }

		public ICommand TerminarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand AgendarCommand { get; set; }

		public EtapaProducaoViewModel(EtapaOrdemProducaoDto etapaOrdemProducao, Action alterouDataHoraFim, Func<EtapaOrdemProducaoDto, bool> podeIniciarEtapa, Action<EtapaOrdemProducaoDto> imprimirEtapa, Func<Dictionary<long, EtapaProducaoViewModel>> criarAgendamentoProximaEtapa)
			: base(etapaOrdemProducao)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			EtapaProducaoViewModel etapaProducaoViewModel = this;
			_alterouDataHoraFim = alterouDataHoraFim;
			_podeIniciarEtapa = podeIniciarEtapa;
			_criarAgendamentoProximaEtapa = criarAgendamentoProximaEtapa;
			IniciarCommand = (ICommand)new DelegateCommand((Action)Iniciar, (Func<bool>)PodeIniciar, (bool?)null);
			TerminarCommand = (ICommand)new DelegateCommand((Action)Terminar, (Func<bool>)PodeTerminar, (bool?)null);
			ImprimirCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				imprimirEtapa(((ViewModelDtoBase<EtapaOrdemProducaoDto>)etapaProducaoViewModel).Dto);
			});
			AgendarCommand = (ICommand)new DelegateCommand((Action)Agendar);
		}

		private void Iniciar()
		{
			if (_podeIniciarEtapa(base.Dto))
			{
				DataHoraInicio = DateTime.Now;
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Situacao));
				MateriaPrimaViewModel.PodeEditar = Iniciado && !base.Dto.Concluida;
			}
		}

		private bool PodeIniciar()
		{
			return !Iniciado;
		}

		private void Terminar()
		{
			DataHoraFim = DateTime.Now;
			_alterouDataHoraFim();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => Situacao));
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => Terminado));
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => CamposAgendamentoVisivel));
			_proximaEtapa = _criarAgendamentoProximaEtapa();
			if (!HelperObject.IsNotNull((object)_proximaEtapa))
			{
				return;
			}
			DataAgendamento = DateTime.Now;
			EtapaProducaoViewModel value = _proximaEtapa.FirstOrDefault().Value;
			object obj;
			if (value == null)
			{
				obj = null;
			}
			else
			{
				ColaboradoresViewModel colaboradoresViewModel = value.ColaboradoresViewModel;
				if (colaboradoresViewModel == null)
				{
					obj = null;
				}
				else
				{
					Lista<ColaboradorEtapaOrdemProducaoDto> colaboradores = colaboradoresViewModel.Colaboradores;
					if (colaboradores == null)
					{
						obj = null;
					}
					else
					{
						IReadOnlyCollection<ColaboradorEtapaOrdemProducaoDto> itens = colaboradores.Itens;
						obj = ((itens != null) ? HelperIEnumerable.ToListEmptyWhenNull<string>(itens.Select((ColaboradorEtapaOrdemProducaoDto x) => x.PessoaReferencia)) : null);
					}
				}
			}
			List<string> colaboradoresReferencia = (List<string>)obj;
			if (HelperObject.IsNotNull((object)colaboradoresReferencia))
			{
				IEnumerable<UsuarioDto> result = DadosSistema.GetService().Canal<IUsuarioModuleService, Task<IEnumerable<UsuarioDto>>>((Func<IUsuarioModuleService, Task<IEnumerable<UsuarioDto>>>)((IUsuarioModuleService service) => service.ObterUsuariosPorPessoaReferencia((IEnumerable<string>)colaboradoresReferencia))).Result;
				_usuario = result.FirstOrDefault((UsuarioDto x) => ((EntityBaseDto)x.Pessoa).Id.Equals(_proximaEtapa.FirstOrDefault().Value.ColaboradoresViewModel.Colaboradores.Itens.FirstOrDefault().PessoaReferencia));
				_numero = _proximaEtapa.FirstOrDefault().Key;
				if (HelperObject.IsNotNull((object)_usuario))
				{
					PessoaAgendamento = _usuario.Pessoa;
				}
			}
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => CamposAgendamentoVisivel));
		}

		private string ObterNomePessoaAgendamento()
		{
			if (!HelperObject.IsNull((object)PessoaAgendamento) && !HelperObject.Is<PessoaDtoNull>((object)PessoaAgendamento))
			{
				return ((BaseDto)PessoaAgendamento).ToText;
			}
			return string.Empty;
		}

		private void RemoverPessoaAgendamentoQuandoNomeApagado()
		{
			if (HelperString.IsNullOrEmpty(NomePessoaAgendamento) && HelperObject.IsNotNull((object)PessoaAgendamento))
			{
				PessoaAgendamento = null;
			}
		}

		private void Agendar()
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Expected O, but got Unknown
			if (!HelperObject.IsNull((object)_proximaEtapa) && !HelperObject.IsNull((object)_usuario) && !HelperObject.IsNull((object)PessoaAgendamento))
			{
				AgendamentoDto agendamentoDto = new AgendamentoDto
				{
					UsuarioNome = _usuario.Nome,
					UsuarioReferencia = ((EntityBaseDto)_usuario).Id,
					Assunto = $"Etapa de produção da ordem {_numero}",
					DataHoraInicial = DataAgendamento,
					DataHoraFinal = DataAgendamento.AddMinutes(30.0),
					PessoaNome = PessoaAgendamento.Nome,
					PessoaReferencia = ((EntityBaseDto)PessoaAgendamento).Id,
					Telefone = PessoaAgendamento.TelefoneToText,
					Notificar = true,
					MinutosParaNotificar = 1
				};
				DadosSistema.GetService().Canal<IAgendamentoModuleService, Task<AgendamentoDto>>((Func<IAgendamentoModuleService, Task<AgendamentoDto>>)((IAgendamentoModuleService service) => ((IAdicionar<AgendamentoDto>)(object)service).AdicionarAsync(agendamentoDto)));
				DialogoFactory.MostrarDialogoConfirma("Agendamento criado com sucesso", false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private async void PesquisarPessoaAgendamento(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoaAgendamento, (TipoPessoaCadastro)11, (Action<PessoaDto>)delegate(PessoaDto p)
			{
				PessoaAgendamento = p;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private bool PodeTerminar()
		{
			if (!Terminado)
			{
				return Iniciado;
			}
			return false;
		}

		public override void ProjectedAsViewModel()
		{
			ColaboradoresViewModel = new ColaboradoresViewModel(base.Dto.Colaboradores);
			Descricao = base.Dto.Descricao;
			DataHoraInicio = ((base.Dto.DataHoraInicio == DateTime.MinValue) ? null : new DateTime?(base.Dto.DataHoraInicio));
			DataHoraFim = ((base.Dto.DataHoraFim == DateTime.MinValue) ? null : new DateTime?(base.Dto.DataHoraFim));
			MateriaPrimaViewModel = new MateriaPrimaViewModel(base.Dto.Itens, Iniciado && !base.Dto.Concluida);
			((ViewModelBase<IEnumerable<ItemEstoqueOrdemProducaoDto>>)MateriaPrimaViewModel).ProjectedAsViewModel();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			OrdemProducaoDto val = (OrdemProducaoDto)((obj is OrdemProducaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				EtapaOrdemProducaoDto obj2 = val.Etapas.FirstOrDefault((EtapaOrdemProducaoDto x) => x.FaseProducaoReferencia.Equals(base.Dto.FaseProducaoReferencia));
				obj2.DataHoraInicio = DataHoraInicio ?? DateTime.MinValue;
				obj2.DataHoraFim = DataHoraFim ?? DateTime.MinValue;
				obj2.Itens = MateriaPrimaViewModel.Itens.Itens.Select((IItemEstoqueOrdemProducaoViewModel x) => x.ObterDto());
				obj2.Colaboradores = ColaboradoresViewModel.Colaboradores.Itens;
				base.AtualizarDto<OrdemProducaoDto>(ref val);
			}
		}

		public override void Dispose()
		{
			ColaboradoresViewModel.Dispose();
			((ViewModelValidationBase)MateriaPrimaViewModel).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public interface IItemEstoqueOrdemProducaoViewModel
	{
		ItemEstoqueOrdemProducaoDto Dto { get; }

		decimal Quantidade { get; }

		bool PodeEditar { get; set; }

		ItemEstoqueOrdemProducaoDto ObterDto();
	}
	public class ItemDesmontagemViewModel : BindableBase, IValidoAttribute, IDataErrorInfo
	{
		private readonly Action _alterouQuantidade;

		private readonly bool _produtoProduzido;

		public ItemOrdemDesmontagemDto Dto { get; }

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<decimal>((Expression<Func<decimal>>)(() => Quantidade));
					_alterouQuantidade();
					Dto.EstoqueRetornado.Quantidade = Quantidade;
				});
			}
		}

		public TipoEstoqueBaseViewModel TipoEstoque { get; set; }

		public string this[string columnName]
		{
			get
			{
				if (!columnName.Equals("Quantidade"))
				{
					return string.Empty;
				}
				if (Quantidade > HelperDecimal.Arredondar(Dto.EstoqueUtilizado.Quantidade, 8))
				{
					return "A quantidade retornada deve ser menor que a quantidade utilizada.";
				}
				if (Quantidade < 0m)
				{
					return "A quantidade retornada deve ser maior ou igual a zero.";
				}
				if (_produtoProduzido && Quantidade == 0m)
				{
					return "A quantidade retornada deve ser maior que zero.";
				}
				return string.Empty;
			}
		}

		public string Error => this["Quantidade"];

		public bool Valido => string.IsNullOrEmpty(Error);

		public ItemDesmontagemViewModel(ItemOrdemDesmontagemDto dto, Action alterouQuantidade, bool produtoProduzido = false)
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Expected O, but got Unknown
			_alterouQuantidade = alterouQuantidade;
			_produtoProduzido = produtoProduzido;
			Dto = dto;
			EstoqueDto val = Clone.ObjectGraph<EstoqueDto>(dto.EstoqueUtilizado);
			TipoEstoque = ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel(dto.EstoqueRetornado, val)).ObterTipoEstoque().ViewModelBase;
			((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
			TipoEstoque.ActionSetarQuantidade((Action)delegate
			{
				Dto.EstoqueRetornado = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto;
				Quantidade = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade;
			});
			if (HelperObject.Is<EstoqueDtoNull>((object)Dto.EstoqueRetornado) && HelperObject.IsNull((object)TipoEstoque.EstoqueCommandParameter))
			{
				Dto.EstoqueRetornado = new EstoqueDto
				{
					Id = ((EntityBaseDto)Dto.EstoqueUtilizado).Id
				};
				Quantidade = Dto.EstoqueUtilizado.Quantidade;
			}
		}
	}
	public class ItemEstoqueOrdemProducaoViewModel : BindableBase, IItemEstoqueOrdemProducaoViewModel
	{
		private readonly Lista<IItemEstoqueOrdemProducaoViewModel> _itens;

		private DateTime _quantidadeEmHora;

		public ISelecaoCommandParameter SubstituirItemCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto produtoServicoEmpresaDto)
		{
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			if (_itens.Existe((Expression<Func<IItemEstoqueOrdemProducaoViewModel, bool>>)((IItemEstoqueOrdemProducaoViewModel x) => x.Dto.ProdutoReferencia.Equals(((EntityBaseDto)produtoServicoEmpresaDto.ProdutoServico).Id))))
			{
				DialogoFactory.MostrarDialogoConfirma("Matéria-prima já existe na lista!", false, System.Windows.HorizontalAlignment.Center);
			}
			else
			{
				string produtoReferencia = Dto.ProdutoReferencia;
				ItemEstoqueOrdemProducaoDto dto = new ItemEstoqueOrdemProducaoDto
				{
					ProdutoReferencia = ((EntityBaseDto)produtoServicoEmpresaDto.ProdutoServico).Id,
					ProdutoDescricao = ((BaseDto)produtoServicoEmpresaDto.ProdutoServico).ToText,
					UnidadeMedida = produtoServicoEmpresaDto.ProdutoServico.UnidadeMedida.Sigla,
					Estoque = produtoServicoEmpresaDto.ProdutoEmpresa.Estoque,
					SubtituidoProdutoReferencia = produtoReferencia
				};
				TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
				if (tipoEstoque != null)
				{
					((ViewModelValidationBase)tipoEstoque).Dispose();
				}
				TipoEstoque = null;
				Criar(dto);
				((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => SubstituirItemCommandParameter));
			}
		}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => (int)x.ProdutoServico.Tipo != 2 && !((EntityBaseDto)x.ProdutoServico).Id.Equals(Dto.ProdutoReferencia)), $"Selecione um cadastro do tipo 'Produto' diferente de {Dto.ProdutoDescricao}!", System.Windows.HorizontalAlignment.Center), PodeSubstituir);

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value, (Action)delegate
				{
					Dto.EstoqueSaida.Quantidade = Quantidade;
				});
			}
		}

		public string QuantidadeEmHorasDisplay
		{
			get
			{
				int num = (int)Quantidade;
				int num2 = (int)Math.Round((Quantidade - (decimal)num) * 60m);
				return $"{HelperInteger.ToStringLeftZero(num, 3)}:{HelperInteger.ToStringLeftZero(num2, 2)}";
			}
			set
			{
				int num = HelperString.ToInt(HelperString.CopiarTexto(value, (Sentido)1, 3));
				int num2 = HelperString.ToInt(HelperString.CopiarTexto(value, (Sentido)0, 2));
				int num3 = num * 60 + num2;
				Quantidade = (decimal)num3 / 60m;
			}
		}

		public DateTime QuantidadeEmHora
		{
			get
			{
				_quantidadeEmHora = Convert.ToDateTime(TimeSpan.FromMinutes((double)Math.Round(Quantidade * 60m)).ToString("hh':'mm"));
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => _quantidadeEmHora));
			}
			set
			{
				Quantidade = (decimal)value.TimeOfDay.TotalMinutes / 60m;
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => QuantidadeEmHora), value);
			}
		}

		public bool MostrarQuantidadeEmHora
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarQuantidadeEmHora));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarQuantidadeEmHora), value);
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

		public ItemEstoqueOrdemProducaoDto Dto
		{
			get
			{
				return ((BindableBase)this).GetProperty<ItemEstoqueOrdemProducaoDto>((Expression<Func<ItemEstoqueOrdemProducaoDto>>)(() => Dto));
			}
			set
			{
				((BindableBase)this).SetProperty<ItemEstoqueOrdemProducaoDto>((Expression<Func<ItemEstoqueOrdemProducaoDto>>)(() => Dto), value);
			}
		}

		public bool PodeEditar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar), value, (Action)delegate
				{
					if (!HelperObject.IsNull((object)Dto))
					{
						PodeSubstituir = string.IsNullOrEmpty(Dto.SubtituidoProdutoReferencia) && PodeEditar;
						((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => SubstituirItemCommandParameter));
					}
				});
			}
		}

		public bool PodeSubstituir
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeSubstituir));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeSubstituir), value);
			}
		}

		public TipoEstoqueBaseViewModel TipoEstoque
		{
			get
			{
				return ((BindableBase)this).GetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque));
			}
			set
			{
				((BindableBase)this).SetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque), value);
			}
		}

		public ItemEstoqueOrdemProducaoViewModel(ItemEstoqueOrdemProducaoDto dto, bool podeEditar, Lista<IItemEstoqueOrdemProducaoViewModel> itens)
		{
			_itens = itens;
			PodeEditar = podeEditar;
			Criar(dto);
			PodeSubstituir = string.IsNullOrEmpty(Dto.SubtituidoProdutoReferencia) && PodeEditar;
		}

		private void Criar(ItemEstoqueOrdemProducaoDto dto)
		{
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			Dto = dto;
			if (HelperObject.IsExactlyObject<EstoqueDtoNull>((object)dto.EstoqueSaida) && HelperObject.IsExactlyObject<EstoqueDtoNull>((object)dto.Estoque))
			{
				Quantidade = dto.QuantidadeNecessaria;
			}
			else
			{
				TipoEstoque = ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel(dto.EstoqueSaida, dto.Estoque)).ObterTipoEstoque().ViewModelBase;
				((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
				TipoEstoque.ActionSetarQuantidade((Action)delegate
				{
					Dto.EstoqueSaida = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto;
					Quantidade = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade;
				});
				if (HelperObject.Is<EstoqueDtoNull>((object)Dto.EstoqueSaida) && HelperObject.IsNull((object)TipoEstoque.EstoqueCommandParameter))
				{
					Dto.EstoqueSaida = dto.Estoque;
					Quantidade = Dto.QuantidadeNecessaria;
				}
				else
				{
					Quantidade = Dto.EstoqueSaida.Quantidade;
				}
			}
			MostrarQuantidadeEmHora = dto.UnidadeMedida.Equals("H");
			ToolTip = (PodeSubstituir ? dto.ProdutoDescricao : "Matéria-prima substituída");
		}

		public ItemEstoqueOrdemProducaoDto ObterDto()
		{
			((EntityBaseDto)Dto.EstoqueSaida).Id = ((EntityBaseDto)Dto.Estoque).Id;
			return Dto;
		}
	}
	public class ItemEstoqueOrdemProducaoVinculadaViewModel : BindableBase, IItemEstoqueOrdemProducaoViewModel
	{
		public ISelecaoCommandParameter OrdemProducaoCommandParameter
		{
			get
			{
				SelecaoValidacaoCommandParameter<OrdemProducaoDto> obj = new SelecaoValidacaoCommandParameter<OrdemProducaoDto>("OrdemProducaoListaView", (Action<OrdemProducaoDto>)delegate(OrdemProducaoDto ordemDto)
				{
					Dto.EstoqueSaida = ordemDto.ProdutoProduzido.EstoqueSaida;
					Quantidade = Dto.EstoqueSaida.Quantidade;
					((BindableBase)this).RaisePropertyChanged<decimal>((Expression<Func<decimal>>)(() => Quantidade));
				}, new ValidateParameter<OrdemProducaoDto>((Func<OrdemProducaoDto, bool>)((OrdemProducaoDto ordemDto) => ((EntityBaseDto)ordemDto).Id.Equals(Dto.OrdemProducaoVinculadaReferencia) && ordemDto.Situacao.Codigo.Equals(2)), $"Selecione a ordem de produção '{Dto.OrdemProducaoVinculadaDescricao}' concluída!", System.Windows.HorizontalAlignment.Center));
				((SelecaoCommandParameter<OrdemProducaoDto>)(object)obj).AdicionarParametro("InformacaoPesquisa", (object)Dto.OrdemProducaoVinculadaReferencia);
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public decimal Quantidade { get; set; }

		public ItemEstoqueOrdemProducaoDto Dto { get; }

		public bool PodeEditar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar), value);
			}
		}

		public ItemEstoqueOrdemProducaoVinculadaViewModel(ItemEstoqueOrdemProducaoDto dto, bool podeEditar)
		{
			Dto = dto;
			PodeEditar = podeEditar;
			Quantidade = Dto.EstoqueSaida.Quantidade;
		}

		public ItemEstoqueOrdemProducaoDto ObterDto()
		{
			return Dto;
		}
	}
	public class ItensDesmontagemViewModel : BindableBase, IValidoAttribute, IDataErrorInfo
	{
		public IEnumerable<ItemDesmontagemViewModel> Lista { get; set; }

		public string this[string columnName]
		{
			get
			{
				if (Lista.All((ItemDesmontagemViewModel x) => x.Valido))
				{
					return string.Empty;
				}
				IEnumerable<string> values = from x in Lista
					where !x.Valido
					select $"{x.Dto.ProdutoDescricao} - {x.Error}";
				return string.Join(Environment.NewLine, values);
			}
		}

		public string Error => this["Lista"];

		public bool Valido => string.IsNullOrEmpty(Error);

		public ItensDesmontagemViewModel(IEnumerable<ItemOrdemDesmontagemDto> itensDesmontagem, Action alterouQuantidade)
		{
			Lista = HelperIEnumerable.ToListEmptyWhenNull<ItemDesmontagemViewModel>(itensDesmontagem.Select((ItemOrdemDesmontagemDto x) => new ItemDesmontagemViewModel(x, alterouQuantidade.Invoke)));
		}
	}
	public interface IValidoAttribute : IDataErrorInfo
	{
		bool Valido { get; }
	}
	public class MateriaPrimaViewModel : ViewModelBase<IEnumerable<ItemEstoqueOrdemProducaoDto>>
	{
		public ISelecaoCommandParameter AdicionarMateriaPrimaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto produtoServicoEmpresaDto)
		{
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Expected O, but got Unknown
			if (Itens.Existe((Expression<Func<IItemEstoqueOrdemProducaoViewModel, bool>>)((IItemEstoqueOrdemProducaoViewModel x) => x.Dto.ProdutoReferencia.Equals(((EntityBaseDto)produtoServicoEmpresaDto.ProdutoServico).Id))))
			{
				DialogoFactory.MostrarDialogoConfirma("Matéria-prima já existe na lista!", false, System.Windows.HorizontalAlignment.Center);
			}
			else
			{
				Itens.Adicionar(CriarItemViewModel(new ItemEstoqueOrdemProducaoDto
				{
					ProdutoReferencia = ((EntityBaseDto)produtoServicoEmpresaDto.ProdutoServico).Id,
					ProdutoDescricao = ((BaseDto)produtoServicoEmpresaDto.ProdutoServico).ToText,
					UnidadeMedida = produtoServicoEmpresaDto.ProdutoServico.UnidadeMedida.Sigla,
					Estoque = produtoServicoEmpresaDto.ProdutoEmpresa.Estoque,
					Incluido = true
				}));
			}
		}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => (int)x.ProdutoServico.Tipo != 2), "Selecione um cadastro do tipo 'Produto'!", System.Windows.HorizontalAlignment.Center), PodeEditar);

		public bool PodeEditar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeEditar), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => AdicionarMateriaPrimaCommandParameter));
					if (HelperObject.IsNull((object)Itens))
					{
						return;
					}
					foreach (IItemEstoqueOrdemProducaoViewModel iten in Itens.Itens)
					{
						if (HelperObject.IsExactlyObject<ItemEstoqueOrdemProducaoViewModel>((object)iten) && HelperObject.IsNull((object)HelperObject.To<ItemEstoqueOrdemProducaoViewModel>((object)iten).TipoEstoque))
						{
							iten.PodeEditar = false;
						}
						else
						{
							iten.PodeEditar = PodeEditar;
						}
					}
				});
			}
		}

		public Lista<IItemEstoqueOrdemProducaoViewModel> Itens { get; set; }

		public MateriaPrimaViewModel(IEnumerable<ItemEstoqueOrdemProducaoDto> itens, bool podeEditar = true)
			: base(itens)
		{
			PodeEditar = podeEditar;
		}

		private IItemEstoqueOrdemProducaoViewModel CriarItemViewModel(ItemEstoqueOrdemProducaoDto dto)
		{
			if (!dto.PossuiOrdemProducaoVinculada)
			{
				return new ItemEstoqueOrdemProducaoViewModel(dto, PodeEditar, Itens);
			}
			return new ItemEstoqueOrdemProducaoVinculadaViewModel(dto, PodeEditar);
		}

		public override void ProjectedAsViewModel()
		{
			Itens = new Lista<IItemEstoqueOrdemProducaoViewModel>();
			Itens.Adicionar(base.Model.Select(CriarItemViewModel));
			Itens.PosicionarNoPrimeiro();
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Itens).Dispose();
			Itens = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class OrdemProducaoAbaViewModel : AbaPrincipalViewModelBase
	{
		public OrdemProducaoAbaViewModel(IViewModelPrincipal viewModel)
			: base("Ordem de produção", "Dados principais", viewModel)
		{
		}
	}
	public class OrdemProducaoFichaAdicionarViewModel : FichaBaseAdicionar<OrdemProducaoDto, OrdemProducaoViewModel, IOrdemProducaoModuleService>
	{
		private bool _custoMateriasPrimasVerificado;

		public OrdemProducaoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}

		protected override async Task<OrdemProducaoDto> Persistir(IFormularioEntity formulario)
		{
			OrdemProducaoDto dtoFormulario = formulario.ToDto<OrdemProducaoDto>();
			bool gerarOrdemProducaoMateriaPrimaProduzida = dtoFormulario.FichaTecnicaDto.PossuiMateriaPrimaProduzida && DialogoFactory.MostrarDialogoSimNao("Gerar ordem de produção para as matérias-primas que precisam ser produzidas?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes;
			dtoFormulario.GerarOrdemProducaoMateriaPrimaProduzida = gerarOrdemProducaoMateriaPrimaProduzida;
			OrdemProducaoDto val = await base.Service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService service) => ((IAdicionar<OrdemProducaoDto>)(object)service).AdicionarAsync(dtoFormulario)));
			((FichaBasePersistir<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).SelecaoNavigateParameterCallBack?.CallBack(val);
			return val;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((FichaBasePersistir<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
			OrdemProducaoViewModel ordemProducaoViewModel = ((Formulario)(OrdemProducaoFormulario)(object)((FichaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Formulario).ViewModelAbaSelecionada as OrdemProducaoViewModel;
			ordemProducaoViewModel?.SetarAcaoNavegacao(delegate(ProdutoServicoEmpresaDto x)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Expected O, but got Unknown
				NavigationParameters val = new NavigationParameters();
				val.Add("ProdutoServicoEmpresaDto", (object)x);
				NavigationParameters val2 = val;
				((NavigationBase)this).Navegar("AtualizacaoCustoMateriaPrimaFichaView", (Action<NavigationResult>)delegate
				{
				}, val2);
			});
			PerguntarAtualizacaoCustoMateriasPrimas(ordemProducaoViewModel);
		}

		private void PerguntarAtualizacaoCustoMateriasPrimas(OrdemProducaoViewModel viewModel)
		{
			if (_custoMateriasPrimasVerificado || string.IsNullOrEmpty(viewModel?.ProdutoProduzidoReferencia))
			{
				return;
			}
			_custoMateriasPrimasVerificado = true;
			((UpdateViewBindableBase)this).DormirAsync(500, (Action<object>)delegate
			{
				//IL_004f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0054: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Expected O, but got Unknown
				if (DialogoFactory.MostrarDialogoSimNao("Deseja revisar e atualizar os custos das matérias-primas antes de continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					ProdutoServicoEmpresaDto val = base.Service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdProdutoServico(new string[1] { viewModel.ProdutoProduzidoReferencia }))).FirstOrDefault();
					if (!HelperObject.IsNull((object)val))
					{
						NavigationParameters val2 = new NavigationParameters();
						val2.Add("ProdutoServicoEmpresaDto", (object)val);
						NavigationParameters val3 = val2;
						((NavigationBase)this).Navegar("AtualizacaoCustoMateriaPrimaFichaView", (Action<NavigationResult>)delegate
						{
						}, val3);
					}
				}
			});
		}
	}
	public class OrdemProducaoFichaAtualizarViewModel : FichaBaseAtualizar<OrdemProducaoDto, OrdemProducaoViewModel, IOrdemProducaoModuleService>
	{
		public OrdemProducaoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}

		protected override async Task<OrdemProducaoDto> Persistir(IFormularioEntity formulario)
		{
			OrdemProducaoDto dtoFormulario = formulario.ToDto<OrdemProducaoDto>();
			bool gerarOrdemProducaoMateriaPrimaProduzida = dtoFormulario.FichaTecnicaDto.PossuiMateriaPrimaProduzida && DialogoFactory.MostrarDialogoSimNao("Gerar ordem de produção para as matérias-primas que precisam ser produzidas?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes;
			dtoFormulario.GerarOrdemProducaoMateriaPrimaProduzida = gerarOrdemProducaoMateriaPrimaProduzida;
			OrdemProducaoDto val = await base.Service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService service) => ((IAtualizar<OrdemProducaoDto>)(object)service).AtualizarAsync(dtoFormulario)));
			((FichaBasePersistir<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).SelecaoNavigateParameterCallBack?.CallBack(val);
			return val;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((FichaBasePersistir<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).OnParametroInformado(sender, navigationParameters);
			(((Formulario)(OrdemProducaoFormulario)(object)((FichaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Formulario).ViewModelAbaSelecionada as OrdemProducaoViewModel)?.SetarAcaoNavegacao(delegate(ProdutoServicoEmpresaDto x)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Expected O, but got Unknown
				NavigationParameters val = new NavigationParameters();
				val.Add("ProdutoServicoEmpresaDto", (object)x);
				NavigationParameters val2 = val;
				((NavigationBase)this).Navegar("AtualizacaoCustoMateriaPrimaFichaView", (Action<NavigationResult>)delegate
				{
				}, val2);
			});
		}
	}
	public class OrdemProducaoFormulario : FormularioEntity<OrdemProducaoDto>
	{
		public OrdemProducaoFormulario(OrdemProducaoDto dto)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new OrdemProducaoAbaViewModel((IViewModelPrincipal)(object)new OrdemProducaoViewModel(base.Dto)) });
		}
	}
	public class OrdemProducaoListaViewModel : ListaSelecaoMultiplaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>
	{
		private readonly INotificacao _notificacao;

		private readonly IWService _service;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		private bool _podeSelecionar;

		private ISelecaoNavigateParameterCallBack _selecaoNavigateParameterCallBackWithService;

		public Visibility ExibirOpcaoMaisNasAcoesItemLista
		{
			get
			{
				if (!PodeImprimirEtiqueta())
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public DateTime DataHoraInicio
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraInicio), value);
			}
		}

		public ICommand ControlarEtapasCommand { get; set; }

		public ICommand VerificarItensFaltaCommand { get; set; }

		public ICommand AdicionarSituacaoCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand ImprimirSemEtapasCommand { get; set; }

		public ICommand ImprimirControleEtapaCommand { get; set; }

		public ICommand ImprimirOrdemDesmontagemCommand { get; set; }

		public ICommand DesmontarCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand GerarEtiquetaCommand { get; set; }

		public ICommand MostrarCadeiaOrdensGeradasCommand { get; set; }

		public ICommand DuplicarCommand { get; set; }

		public OrdemProducaoListaViewModel(ISessao sessao, INotificacao notificacao, IWService service, IEntityFiltroOrdemProducao<OrdemProducaoDto, IOrdemProducaoModuleService> pesquisa, IImpressaoRelatorioService impressaoRelatorioService, IEntityFiltroAvancadoOrdemProducao<OrdemProducaoDto, IOrdemProducaoModuleService> filtroAvancado)
			: base(sessao, "OrdemProducaoFichaAdicionarView", "OrdemProducaoFichaAtualizarView")
		{
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Expected O, but got Unknown
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Expected O, but got Unknown
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Expected O, but got Unknown
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Expected O, but got Unknown
			//IL_0185: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Expected O, but got Unknown
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Expected O, but got Unknown
			//IL_01de: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Expected O, but got Unknown
			//IL_020a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0214: Expected O, but got Unknown
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_0240: Expected O, but got Unknown
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_026d: Expected O, but got Unknown
			_notificacao = notificacao;
			_service = service;
			_impressaoRelatorioService = impressaoRelatorioService;
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).SetarTipoPesquisa((Pesquisa)new PesquisaAvancadaViewModel((IItens)(object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens, (FiltroAvancadoBase)(object)((filtroAvancado is FiltroAvancadoOrdemProducao) ? filtroAvancado : null), Array.Empty<OpcaoBase>()));
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.AdicionarOpcao((OpcaoBase)(object)((pesquisa is OpcaoBase) ? pesquisa : null));
			ControlarEtapasCommand = (ICommand)new AsyncCommand((Func<Task>)ControlarEtapas, (Func<bool>)PodeControlarEtapas, false, (bool?)null);
			VerificarItensFaltaCommand = (ICommand)new AsyncCommand((Func<Task>)VerificarItensFalta, (Func<bool>)PodeVerificarItensFalta, false, (bool?)null);
			AdicionarSituacaoCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)AdicionarSituacao, (Func<object, bool>)PodeAdicionarSituacao, false, (bool?)null);
			ImprimirCommand = (ICommand)new AsyncCommand((Func<Task>)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			ImprimirSemEtapasCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirSemEtapas, (Func<bool>)PodeImprimir, (bool?)null);
			ImprimirControleEtapaCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirControleEtapa, (Func<bool>)PodeControlarEtapas, false, (bool?)null);
			ImprimirOrdemDesmontagemCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirOrdemDesmontagem, (Func<bool>)PodeImprimirOrdemDesmontagem, false, (bool?)null);
			DesmontarCommand = (ICommand)new AsyncCommand((Func<Task>)Desmontar, (Func<bool>)PodeDesmontar, false, (bool?)null);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)VisualizarRelatorio, (Func<bool>)PodeVisualizarRelatorio, (bool?)null);
			GerarEtiquetaCommand = (ICommand)new DelegateCommand((Action)ImprimirEtiqueta, (Func<bool>)PodeImprimirEtiqueta, (bool?)null);
			MostrarCadeiaOrdensGeradasCommand = (ICommand)new DelegateCommand((Action)MostrarCadeiaOrdensGeradas, (Func<bool>)PodeMostrarCadeiaOrdensGeradas, (bool?)null);
			DuplicarCommand = (ICommand)new AsyncCommand((Func<Task>)Duplicar, (Func<bool>)PodeDuplicar, false, (bool?)null);
		}

		private async Task ControlarEtapas()
		{
			await ObterDto(delegate(OrdemProducaoDto ordemProducaoDto)
			{
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0056: Expected O, but got Unknown
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(ControleEtapaViewModel).Name, (object)new ControleEtapaViewModel(ordemProducaoDto, _impressaoRelatorioService, _service));
				((NavigationBase)this).Navegar("ControleEtapaFichaView", obj, val);
			});
		}

		private bool PodeControlarEtapas()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo != 2 && ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo != 12 && ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo != 22)
			{
				return ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado.FichaTecnicaDto.EtapasProducao.Count() > 1;
			}
			return false;
		}

		private async Task VerificarItensFalta()
		{
			if (!HelperString.IsObjectIdDefaultOrEmpty(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.OrdemProducaoReferenciaParent))
			{
				await VerificarItensEmFaltaDaOrdemProducaoSelecionada();
			}
			else if (DialogoFactory.MostrarDialogoSimNao("Imprimir lista de matérias-primas de todas as ordens de produção geradas a partir dessa?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				await ObterDtos(delegate(IEnumerable<OrdemProducaoDto> ordensProducaoDto)
				{
					if (ordensProducaoDto.Sum((OrdemProducaoDto ordemProducaoDto) => ordemProducaoDto.Etapas.Where((EtapaOrdemProducaoDto x) => !x.Concluida).Sum((EtapaOrdemProducaoDto x) => x.Itens.Sum((ItemEstoqueOrdemProducaoDto i) => i.QuantidadeFalta))) > 0m)
					{
						ListaMateriasPrimasFalta listaMateriasPrimasFalta = new ListaMateriasPrimasFalta(from g in ordensProducaoDto.Select((OrdemProducaoDto a) => a.FichaTecnicaDto).SelectMany((FichaTecnicaDto x) => x.EtapasProducao).SelectMany((EtapaProducaoDto z) => z.MateriasPrimas)
							group g by new { ((MateriaPrimaCustoDto)g).ProdutoReferencia } into s
							select new MateriaPrimaDto
							{
								Descricao = HelperString.ToStringEmptyWhenNull(s.FirstOrDefault().Descricao),
								UnidadeMedida = HelperString.ToStringEmptyWhenNull(s.FirstOrDefault().UnidadeMedida),
								QuantidadeDisponivel = s.FirstOrDefault().QuantidadeDisponivel,
								QuantidadeNecessaria = s.Sum((MateriaPrimaDto i) => i.QuantidadeNecessaria),
								QuantidadeFalta = s.Sum((MateriaPrimaDto i) => i.QuantidadeFalta)
							});
						_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)listaMateriasPrimasFalta);
					}
					else
					{
						DialogoFactory.MostrarDialogoConfirma("Há matérias-primas suficientes para produzir!", false, System.Windows.HorizontalAlignment.Center);
					}
				});
			}
			else
			{
				await VerificarItensEmFaltaDaOrdemProducaoSelecionada();
			}
		}

		private async Task VerificarItensEmFaltaDaOrdemProducaoSelecionada()
		{
			await ObterDto(delegate(OrdemProducaoDto ordemProducaoDto)
			{
				if (ordemProducaoDto.Etapas.Where((EtapaOrdemProducaoDto x) => !x.Concluida).Sum((EtapaOrdemProducaoDto x) => x.Itens.Sum((ItemEstoqueOrdemProducaoDto i) => i.QuantidadeFalta)) > 0m)
				{
					MateriasPrimasFalta materiasPrimasFalta = new MateriasPrimasFalta(ordemProducaoDto);
					_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)materiasPrimasFalta);
				}
				else
				{
					DialogoFactory.MostrarDialogoConfirma("Há matérias-primas suficientes para produzir!", false, System.Windows.HorizontalAlignment.Center);
				}
			});
		}

		private bool PodeVerificarItensFalta()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				if (((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado.FichaTecnicaDto.EtapasProducao.Count() <= 1 || ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 2 || ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 12 || ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 22)
				{
					if (((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado.FichaTecnicaDto.EtapasProducao.Count() == 1)
					{
						return ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 1;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		private async Task AdicionarSituacao(object obj)
		{
			try
			{
				SituacaoMovimentacao situacao = SituacoesMovimentacao.ObterPorCodigo(HelperObject.To<SituacaoMovimentacaoDto>(obj).Codigo);
				if (HelperObject.Is<Iniciado>((object)situacao))
				{
					DateTime dataHoraInicio = default(DateTime);
					MessageBoxResult num = DialogoFactory.MostrarDialogoEntradaDataInformacaoCarregada(ref dataHoraInicio, DateTime.MinValue, "Data/hora início", $"Deseja {situacao.DescricaoComando.ToLower()} ordem de produção?", DateTime.Now, (IIcone)null);
					DataHoraInicio = dataHoraInicio;
					if (num != MessageBoxResult.OK)
					{
						return;
					}
				}
				else if (DialogoFactory.MostrarDialogoSimNao($"Deseja {situacao.DescricaoComando.ToLower()} ordem de produção?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				if (HelperObject.Is<Concluido>((object)situacao))
				{
					await VerificarEstoque();
				}
				else if (!HelperObject.Is<Iniciado>((object)situacao) || DadosSistema.ConfiguracaoDto.Industria.PermitirProducaoSemMateriaPrima.Valor)
				{
					await AdicionarSituacaoHistorico(situacao);
				}
				else
				{
					await VerificarMateriasPrimas(situacao);
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private async Task VerificarEstoque()
		{
			await ObterDto(delegate(OrdemProducaoDto ordemProducaoDto)
			{
				//IL_002d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0032: Unknown result type (might be due to invalid IL or missing references)
				//IL_004d: Expected O, but got Unknown
				EstoqueOrdemProducaoViewModel estoqueOrdemProducaoViewModel = new EstoqueOrdemProducaoViewModel(ordemProducaoDto);
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(EstoqueOrdemProducaoViewModel).Name, (object)estoqueOrdemProducaoViewModel);
				((NavigationBase)this).Navegar("EstoqueOrdemProducaoFichaView", obj, val);
			});
		}

		private async Task VerificarMateriasPrimas(SituacaoMovimentacao situacao)
		{
			await ObterDto(async delegate(OrdemProducaoDto ordemProducaoDto)
			{
				if (ordemProducaoDto.FichaTecnicaDto.EtapasProducao.Sum((EtapaProducaoDto x) => x.MateriasPrimas.Sum((MateriaPrimaDto i) => i.QuantidadeFalta)) > 0m)
				{
					if (DialogoFactory.MostrarDialogoSimNao("Não foi possível iniciar a etapa de produção. Não há matérias-primas suficientes!\nDeseja imprimir relatório de itens em falta?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
					{
						MateriasPrimasFalta materiasPrimasFalta = new MateriasPrimasFalta(ordemProducaoDto);
						_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)materiasPrimasFalta);
					}
				}
				else
				{
					await AdicionarSituacaoHistorico(situacao);
				}
			});
		}

		private async Task AdicionarSituacaoHistorico(SituacaoMovimentacao situacao)
		{
			string empty = string.Empty;
			DialogoFactory.MostrarDialogoEntradaString(ref empty, "Observação", situacao.ObterDescricao(), (IIcone)new Pergunta(), false, false, HelperObject.Is<Inutilizado>((object)situacao), (string)null, false, true);
			LoaderProcessoService.MostrarLoader($"Adicionando situação {((CodigoDescricao)situacao).Descricao}...");
			HistoricoIndustriaDto historicoOrdemProducao = new HistoricoIndustriaDto
			{
				OrdemProducaoReferencia = ((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id,
				Situacao = new SituacaoMovimentacaoDto
				{
					Codigo = ((CodigoDescricao)situacao).Codigo
				},
				Observacao = HelperString.ToStringEmptyWhenNull(empty),
				NomeUsuario = DadosSistema.NomeUsuario,
				DataHoraInicio = DataHoraInicio
			};
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService service) => service.AdicionarSituacaoAsync(historicoOrdemProducao))).ContinueWith(delegate(Task<OrdemProducaoDto> task)
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
						Posicionar(task.Result);
					});
				}
			});
		}

		private void Posicionar(OrdemProducaoDto ordemProducaoDto)
		{
			_notificacao.MostrarNotificacao($"Ordem de produção: {ordemProducaoDto.Numero} - {ordemProducaoDto.Historicos.LastOrDefault().Observacao}");
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Posicionar((Expression<Func<OrdemProducaoViewModel, bool>>)((OrdemProducaoViewModel x) => ((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)x).Dto).Id.Equals(((EntityBaseDto)ordemProducaoDto).Id)));
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Atualizar(ordemProducaoDto);
			((ListaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>)(object)this).PodeEditar();
		}

		private bool PodeAdicionarSituacao(object obj)
		{
			return obj is SituacaoMovimentacaoDto;
		}

		private async Task Imprimir()
		{
			await CriarImpresso(exibirEtapas: true);
		}

		private async Task ImprimirSemEtapas()
		{
			await CriarImpresso(exibirEtapas: false);
		}

		public async Task CriarImpresso(bool exibirEtapas)
		{
			if (((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo.Equals(22))
			{
				LoaderProcessoService.MostrarLoader("Processando...");
				await _service.Canal<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<DesmontagemRelatorioDto>>>((Func<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<DesmontagemRelatorioDto>>>)((IOrdemProducaoRelatorioModuleService service) => service.ObterDesmontagensAsync(string.Empty, ((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id, DateTime.MinValue, DateTime.MinValue, false))).ContinueWith(delegate(Task<IEnumerable<DesmontagemRelatorioDto>> task)
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
							_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoOrdemProducao(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto, exibirEtapas, task.Result));
						});
					}
				});
			}
			else if (!HelperString.IsObjectIdDefaultOrEmpty(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.OrdemProducaoReferenciaParent))
			{
				_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoOrdemProducao(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto, exibirEtapas));
			}
			else if (DialogoFactory.MostrarDialogoSimNao("Imprimir todas as ordens de produção geradas a partir dessa?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				LoaderProcessoService.MostrarLoader("Processando...");
				await _service.Canal<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<OrdemProducaoDto>>>((Func<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<OrdemProducaoDto>>>)((IOrdemProducaoRelatorioModuleService service) => service.ObterOrdensProducaoPorOrdemProducaoReferenciaParentAsync(((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task<IEnumerable<OrdemProducaoDto>> task)
				{
					LoaderProcessoService.EsconderLoader();
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						List<OrdemProducaoDto> ordens = new List<OrdemProducaoDto>();
						ordens.AddRange(task.Result);
						if (DialogoFactory.MostrarDialogoSimNao("Imprimir as ordens de produção separadamente?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
						{
							ImprimirTudo(ordens, exibirEtapas);
						}
						else
						{
							ThreadPrincipal.Executar((Action)delegate
							{
								_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoOrdensProducaoCadeia(ordens));
							});
						}
					}
				});
			}
			else
			{
				_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoOrdemProducao(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto, exibirEtapas));
			}
		}

		private void ImprimirTudo(IEnumerable<OrdemProducaoDto> ordensProducao, bool exibirEtapas)
		{
			XtraReport relatorio = null;
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					foreach (OrdemProducaoDto item in ordensProducao)
					{
						relatorio = ProcessarImpressao(relatorio, item, exibirEtapas);
					}
					ThreadPrincipal.Executar((Action)delegate
					{
						XtraReport obj = relatorio;
						if (obj != null)
						{
							XtraReportExtensions.ShowPreviewDialog((IReport)(object)obj);
						}
					});
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					Exception e = ex2;
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(e, "", false, false);
					});
				}
				finally
				{
					LoaderProcessoService.EsconderLoader();
				}
			}).Start();
		}

		private XtraReport ProcessarImpressao(XtraReport relatorio, OrdemProducaoDto dto, bool exibirEtapas)
		{
			if (HelperObject.IsNull((object)relatorio))
			{
				relatorio = (XtraReport)(object)new ImpressoOrdemProducao(dto, exibirEtapas).Processar();
			}
			else
			{
				relatorio.Pages.AddRange((IEnumerable)((XtraReport)new ImpressoOrdemProducao(dto, exibirEtapas).Processar()).Pages);
			}
			return relatorio;
		}

		private bool PodeImprimir()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && DadosSistema.PermitirVisualizarFichaTecnica)
			{
				return ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private async Task ImprimirControleEtapa()
		{
			await ObterDto(delegate(OrdemProducaoDto ordemProducaoDto)
			{
				_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoControleEtapa(ordemProducaoDto, ordemProducaoDto.Etapas));
			});
		}

		private async Task ObterDto(Action<OrdemProducaoDto> callback)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>((Func<IOrdemProducaoModuleService, Task<OrdemProducaoDto>>)((IOrdemProducaoModuleService service) => service.ObterOrdemProducaoDtoAsync(((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task<OrdemProducaoDto> task)
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
						callback(task.Result);
					});
				}
			});
		}

		private async Task ObterDtos(Action<IEnumerable<OrdemProducaoDto>> callback)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<OrdemProducaoDto>>>((Func<IOrdemProducaoRelatorioModuleService, Task<IEnumerable<OrdemProducaoDto>>>)((IOrdemProducaoRelatorioModuleService service) => service.ObterOrdensProducaoPorOrdemProducaoReferenciaParentAsync(((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task<IEnumerable<OrdemProducaoDto>> task)
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
						callback(task.Result);
					});
				}
			});
		}

		private async Task ImprimirOrdemDesmontagem()
		{
			await ObterOrdemDesmontagemDto(delegate(OrdemDesmontagemDto ordemDesmontagemDto)
			{
				_impressaoRelatorioService.ImprimirRelatorio((IViewModelItemImprimirRelatorio)(object)new ImpressoOrdemDesmontagem(ordemDesmontagemDto));
			});
		}

		private bool PodeImprimirOrdemDesmontagem()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 2;
			}
			return false;
		}

		private async Task Desmontar()
		{
			await ObterOrdemDesmontagemDto(delegate(OrdemDesmontagemDto ordemDesmontagemDto)
			{
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_004a: Expected O, but got Unknown
				Action<NavigationResult> obj = delegate
				{
				};
				NavigationParameters val = new NavigationParameters();
				val.Add(typeof(DesmontarViewModel).Name, (object)new DesmontarViewModel(ordemDesmontagemDto));
				((NavigationBase)this).Navegar("DesmontarFichaView", obj, val);
			});
		}

		private bool PodeDesmontar()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.PodeDesmontar && ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo == 2)
			{
				return DadosSistema.VerificarPermissao<OrdemProducao, PermitirDesmontar>();
			}
			return false;
		}

		private async Task ObterOrdemDesmontagemDto(Action<OrdemDesmontagemDto> callback)
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			await _service.Canal<IOrdemProducaoModuleService, Task<OrdemDesmontagemDto>>((Func<IOrdemProducaoModuleService, Task<OrdemDesmontagemDto>>)((IOrdemProducaoModuleService service) => service.ObterOrdemDesmontagemDtoAsync(((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task<OrdemDesmontagemDto> task)
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
						callback(task.Result);
					});
				}
			});
		}

		private void VisualizarRelatorio()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)104);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private bool PodeVisualizarRelatorio()
		{
			return DadosSistema.VerificarPermissao<Industria, VisualizarRelatorio>();
		}

		private bool PodeImprimirEtiqueta()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return !((ListaSelecaoBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private void ImprimirEtiqueta()
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdProdutoServico(new string[1] { ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.ProdutoProduzidoReferencia }))).FirstOrDefault();
			NavigationParameters val2 = new NavigationParameters();
			val2.Add("ID", (object)Guid.NewGuid().ToString());
			val2.Add("dto", (object)val);
			val2.Add("quantidade", (object)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Quantidade.ToString("F2"));
			NavigationParameters val3 = val2;
			NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)((NavigationBase)this).NavigationService.Region, "EtiquetaImpressaoFichaView", (Action<NavigationResult>)delegate
			{
			}, val3);
		}

		private bool PodeMostrarCadeiaOrdensGeradas()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido && !((ListaSelecaoBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).PodeSelecionar())
			{
				return HelperString.IsObjectIdDefaultOrEmpty(((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.OrdemProducaoReferenciaParent);
			}
			return false;
		}

		private void MostrarCadeiaOrdensGeradas()
		{
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = ((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id;
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
			((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
		}

		private bool PodeDuplicar()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens) && ((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.SelecionadoValido && !((ListaSelecaoBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).PodeSelecionar())
			{
				return ((ListaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>)(object)this).PodeAdicionar();
			}
			return false;
		}

		private async Task Duplicar()
		{
			await ObterDto(delegate(OrdemProducaoDto ordemProducaoDto)
			{
				//IL_0040: Unknown result type (might be due to invalid IL or missing references)
				//IL_004a: Expected O, but got Unknown
				//IL_006c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0076: Expected O, but got Unknown
				((EntityBaseDto)ordemProducaoDto).Id = string.Empty;
				((EntityVersaoDto)ordemProducaoDto).Versao = TimeSpan.Zero;
				ordemProducaoDto.Numero = 0L;
				ordemProducaoDto.DataHoraEmissao = DateTime.Now;
				ordemProducaoDto.OrcamentoIndustriaReferencia = string.Empty;
				ordemProducaoDto.OrdemProducaoReferenciaParent = string.Empty;
				ordemProducaoDto.Situacao = new SituacaoMovimentacaoDto();
				ordemProducaoDto.SituacoesDisponiveis = Enumerable.Empty<SituacaoMovimentacaoDto>();
				ordemProducaoDto.Historicos = Enumerable.Empty<HistoricoIndustriaDto>();
				ordemProducaoDto.Etapas = Enumerable.Empty<EtapaOrdemProducaoDto>();
				ordemProducaoDto.ProdutoProduzido = new ItemEstoqueOrdemProducaoDto();
				ordemProducaoDto.PodeDesmontar = false;
				ordemProducaoDto.Tempo = TimeSpan.Zero;
				ordemProducaoDto.TempoString = string.Empty;
				((ListaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>)(object)this).Adicionar(ordemProducaoDto);
			});
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			base.OnParametroInformado(sender, navigationParameters);
			object obj = navigationParameters["PodeSelecionar"];
			_podeSelecionar = HelperObject.IsNull(obj) || (bool)obj;
			string text = navigationParameters["InformacaoPesquisa"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = text;
				((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
				((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
			}
			ref ISelecaoNavigateParameterCallBack selecaoNavigateParameterCallBackWithService = ref _selecaoNavigateParameterCallBackWithService;
			object obj2 = navigationParameters["ISelecaoNavigateParameterCallBack"];
			selecaoNavigateParameterCallBackWithService = (ISelecaoNavigateParameterCallBack)((obj2 is ISelecaoNavigateParameterCallBack) ? obj2 : null);
		}

		protected override void ProcessarParametroRetornadoCustom(OrdemProducaoDto dto)
		{
			base.ProcessarParametroRetornadoCustom(dto);
			ISelecaoNavigateParameterCallBack selecaoNavigateParameterCallBackWithService = _selecaoNavigateParameterCallBackWithService;
			if (selecaoNavigateParameterCallBackWithService != null)
			{
				selecaoNavigateParameterCallBackWithService.Executar<OrdemProducaoDto>(((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.ObterDtoItemSelecionado(), _service);
			}
		}

		protected override Task Editar()
		{
			ProgramacaoIndustriaDto result = _service.Canal<IProgramacaoIndustriaModuleService, Task<ProgramacaoIndustriaDto>>((Func<IProgramacaoIndustriaModuleService, Task<ProgramacaoIndustriaDto>>)((IProgramacaoIndustriaModuleService service) => service.ObterPorOrdemProducaoId(((EntityBaseDto)((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto).Id))).Result;
			if (HelperObject.IsNot<ProgramacaoIndustriaDtoNull>((object)result))
			{
				DialogoFactory.MostrarDialogoConfirma($"Ordem de produção não pode ser editada, pois está vinculada com a programação {result.Descricao}.", false, System.Windows.HorizontalAlignment.Center);
				return Task.CompletedTask;
			}
			if (!((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.PossuiOrdemProducaoVinculada)
			{
				return ((ListaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>)(object)this).Editar();
			}
			DialogoFactory.MostrarDialogoConfirma("Ordem de produção não pode ser editada, pois possui ordens de produção vinculadas.", false, System.Windows.HorizontalAlignment.Center);
			return Task.CompletedTask;
		}

		public override bool PodeEditar()
		{
			if (((ListaManutencaoBase<OrdemProducaoDto, OrdemProducaoViewModel, OrdemProducao>)(object)this).PodeEditar())
			{
				return ((ViewModelDtoBase<OrdemProducaoDto>)((ListaBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).Itens.Selecionado).Dto.Situacao.Codigo.Equals(1);
			}
			return false;
		}

		protected override bool PodeSelecionar()
		{
			if (((ListaSelecaoBase<OrdemProducaoDto, OrdemProducaoViewModel>)(object)this).PodeSelecionar())
			{
				return _podeSelecionar;
			}
			return false;
		}
	}
	public class OrdemProducaoViewModel : ViewModelDtoBase<OrdemProducaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
		private Action<ProdutoServicoEmpresaDto> _action;

		public ISelecaoCommandParameter ProdutoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto dto)
		{
			ProdutoServicoEmpresa = dto;
			ProdutoProduzidoDescricao = ((BaseDto)dto).ToText;
			ProdutoProduzidoReferencia = ((EntityBaseDto)dto.ProdutoServico).Id;
			FichaTecnicaDto = dto.ProdutoEmpresa.FichaTecnica;
			if (Quantidade == 0m)
			{
				Quantidade = FichaTecnicaDto.PrevisaoProducao;
			}
		}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => (int)x.ProdutoServico.Tipo == 9), "Informe um registro do tipo 'Produto Produzido'!", System.Windows.HorizontalAlignment.Center));

		public long Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => Numero), value);
			}
		}

		public DateTime DataHoraEmissao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEmissao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEmissao), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Descrição' é obrigatório.")]
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

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "O campo 'Quantidade' deve ser maior que zero.")]
		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value);
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

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Produto' é obrigatório.")]
		public string ProdutoProduzidoDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ProdutoProduzidoDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ProdutoProduzidoDescricao), value);
			}
		}

		public ProdutoServicoEmpresaDto ProdutoServicoEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa), value, (Action)delegate
				{
					((UpdateViewBindableBase)this).DormirAsync(500, (Action<object>)delegate
					{
						if (DialogoFactory.MostrarDialogoSimNao("Deseja revisar e atualizar os custos das matérias-primas antes de continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
						{
							_action(value);
						}
					});
				});
			}
		}

		public string ProdutoProduzidoReferencia { get; set; }

		public FichaTecnicaDto FichaTecnicaDto { get; set; }

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value);
			}
		}

		public OrdemProducaoViewModel()
		{
		}

		public OrdemProducaoViewModel(OrdemProducaoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Numero = base.Dto.Numero;
			DataHoraEmissao = base.Dto.DataHoraEmissao;
			Quantidade = base.Dto.Quantidade;
			Descricao = base.Dto.Descricao;
			Observacao = base.Dto.Observacao;
			ProdutoProduzidoDescricao = base.Dto.ProdutoProduzidoDescricao;
			ProdutoProduzidoReferencia = base.Dto.ProdutoProduzidoReferencia;
			FichaTecnicaDto = base.Dto.FichaTecnicaDto;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			OrdemProducaoDto val = (OrdemProducaoDto)((obj is OrdemProducaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Numero = Numero;
				val.DataHoraEmissao = DataHoraEmissao;
				val.Quantidade = Quantidade;
				val.Descricao = Descricao;
				val.Observacao = Observacao;
				val.ProdutoProduzidoReferencia = ProdutoProduzidoReferencia;
				val.ProdutoProduzidoDescricao = (HelperObject.IsNull((object)ProdutoServicoEmpresa) ? ProdutoProduzidoDescricao : $"{ProdutoServicoEmpresa.ProdutoServico.CodigoInterno}-{ProdutoProduzidoDescricao}");
				val.FichaTecnicaDto = FichaTecnicaDto;
				base.AtualizarDto<OrdemProducaoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new OrdemProducaoFormulario(base.Dto);
		}

		public string ToTituloFicha()
		{
			return "Ordem de produção";
		}

		public string ToTituloLista()
		{
			return "Ordens de produção";
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}

		public void SetarAcaoNavegacao(Action<ProdutoServicoEmpresaDto> action)
		{
			_action = action;
		}
	}
	public class OrdensProducaoViewModel : ModuloBase
	{
		public OrdensProducaoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloOrdemProducaoRegion, typeof(OrdemProducaoListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ValidoAttribute : ValidationAttribute
	{
		protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value != null)
			{
				HelperObject.ToDo<IValidoAttribute>(value, (Action<IValidoAttribute>)delegate(IValidoAttribute validation)
				{
					base.ErrorMessage = validation.Error;
				});
			}
			if ((IValidoAttribute)value != null && ((IValidoAttribute)value).Valido)
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
		}
	}
}
namespace Otus.Modulo.OrdensProducao.Reports
{
	public class ImpressoOrdensProducaoCadeia : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly IEnumerable<OrdemProducaoDto> _dtos;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private CalculatedField toText;

		private CalculatedField descricaoMateriaPrima;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private ReportHeaderBand ReportHeader1;

		private XRLabel xrLabel22;

		private XRLabel xrLabel21;

		private XRLabel xrLabel18;

		private XRLabel xrLabel15;

		private XRLabel xrLabel11;

		private XRLabel xrLabel1;

		private XRLabel xrLabel5;

		private XRLabel xrLabel23;

		private XRLabel xrLabel16;

		private XRLabel xrLabel6;

		private XRLabel xrLabel17;

		private XRLabel xrLabel44;

		private XRLabel xrLabel13;

		private XRLabel xrLabel14;

		private XRLabel xrLabel10;

		private XRLabel xrLabel43;

		private XRLabel xrLabel7;

		private XRLabel xrLabel8;

		private XRLabel lblQtd;

		private XRLabel xrLabel2;

		private XRLabel lblUnidade;

		private XRLabel lblQtdUtilizada;

		private XRLine xrLine1;

		private GroupFooterBand GroupFooter1;

		private XRLabel lblTotalQtdNecessaria;

		private ReportFooterBand ReportFooter;

		private XRSubreport xrSubreport1;

		private XRLabel xrLabel4;

		private XRPictureBox imagem;

		private CalculatedField QuantidadeProduto;

		public string TituloRelatorio => "Ordens de produção em cadeia resumido";

		public string SubTituloRelatorio => string.Format("Números: {0}", string.Join("/", from x in _dtos
			orderby x.Numero
			select x.Numero));

		public bool Retrato => true;

		public ImpressoOrdensProducaoCadeia(IEnumerable<OrdemProducaoDto> dtos)
		{
			_dtos = dtos;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XtraReportBase)this).DataSource = _dtos.OrderBy((OrdemProducaoDto x) => x.Numero);
				List<ItemEstoqueOrdemProducaoDto> dataSource = HelperIEnumerable.ToListEmptyWhenNull<ItemEstoqueOrdemProducaoDto>(_dtos.SelectMany((OrdemProducaoDto x) => x.Etapas.SelectMany((EtapaOrdemProducaoDto y) => y.Itens)));
				((XtraReportBase)((SubreportBase)xrSubreport1).ReportSource).DataSource = dataSource;
				return this;
			}));
		}

		private void lblQtdUtilizada_BeforePrint(object sender, PrintEventArgs e)
		{
			if (((XRControl)lblUnidade).Text != "H")
			{
				return;
			}
			try
			{
				int num = (int)HelperString.ToDecimal(((XRControl)lblQtdUtilizada).Text, 10, 0);
				int num2 = (int)Math.Round((HelperString.ToDecimal(((XRControl)lblQtdUtilizada).Text, 10, 0) - (decimal)num) * 100m) * 60;
				((XRControl)lblQtdUtilizada).Text = $"{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num, 3), 3)}:{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num2, 2), 2)}";
			}
			catch (Exception)
			{
			}
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
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Expected O, but got Unknown
			//IL_0120: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Expected O, but got Unknown
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Expected O, but got Unknown
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Expected O, but got Unknown
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Expected O, but got Unknown
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Expected O, but got Unknown
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Expected O, but got Unknown
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Expected O, but got Unknown
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Expected O, but got Unknown
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Expected O, but got Unknown
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Expected O, but got Unknown
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Expected O, but got Unknown
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Expected O, but got Unknown
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Expected O, but got Unknown
			//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_035e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0387: Unknown result type (might be due to invalid IL or missing references)
			//IL_0435: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Expected O, but got Unknown
			//IL_0460: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_052f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0535: Expected O, but got Unknown
			//IL_0574: Unknown result type (might be due to invalid IL or missing references)
			//IL_059d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0605: Unknown result type (might be due to invalid IL or missing references)
			//IL_060b: Expected O, but got Unknown
			//IL_064a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e9: Expected O, but got Unknown
			//IL_0728: Unknown result type (might be due to invalid IL or missing references)
			//IL_0751: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_07cc: Expected O, but got Unknown
			//IL_080b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0834: Unknown result type (might be due to invalid IL or missing references)
			//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0905: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a8: Expected O, but got Unknown
			//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7d: Expected O, but got Unknown
			//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d5e: Expected O, but got Unknown
			//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f28: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f2e: Expected O, but got Unknown
			//IL_0f6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f96: Unknown result type (might be due to invalid IL or missing references)
			//IL_104e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1077: Unknown result type (might be due to invalid IL or missing references)
			//IL_1130: Unknown result type (might be due to invalid IL or missing references)
			//IL_1159: Unknown result type (might be due to invalid IL or missing references)
			//IL_122f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1258: Unknown result type (might be due to invalid IL or missing references)
			//IL_132e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1357: Unknown result type (might be due to invalid IL or missing references)
			//IL_141a: Unknown result type (might be due to invalid IL or missing references)
			//IL_146f: Unknown result type (might be due to invalid IL or missing references)
			//IL_16a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_16ae: Expected O, but got Unknown
			//IL_16d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_175f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1765: Expected O, but got Unknown
			//IL_178a: Unknown result type (might be due to invalid IL or missing references)
			//IL_17b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_181b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1821: Expected O, but got Unknown
			//IL_1846: Unknown result type (might be due to invalid IL or missing references)
			//IL_186f: Unknown result type (might be due to invalid IL or missing references)
			//IL_197e: Unknown result type (might be due to invalid IL or missing references)
			//IL_19a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ba7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ca5: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cab: Expected O, but got Unknown
			//IL_1ced: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d16: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			Detail = new DetailBand();
			xrLabel4 = new XRLabel();
			imagem = new XRPictureBox();
			xrLine1 = new XRLine();
			xrLabel13 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel43 = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel23 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel44 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			toText = new CalculatedField();
			descricaoMateriaPrima = new CalculatedField();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel2 = new XRLabel();
			lblUnidade = new XRLabel();
			lblQtdUtilizada = new XRLabel();
			ReportHeader1 = new ReportHeaderBand();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			lblQtd = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblTotalQtdNecessaria = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrSubreport1 = new XRSubreport();
			QuantidadeProduto = new CalculatedField();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[19]
			{
				(XRControl)xrLabel4,
				(XRControl)imagem,
				(XRControl)xrLine1,
				(XRControl)xrLabel13,
				(XRControl)xrLabel14,
				(XRControl)xrLabel10,
				(XRControl)xrLabel43,
				(XRControl)xrLabel22,
				(XRControl)xrLabel21,
				(XRControl)xrLabel18,
				(XRControl)xrLabel15,
				(XRControl)xrLabel11,
				(XRControl)xrLabel1,
				(XRControl)xrLabel5,
				(XRControl)xrLabel23,
				(XRControl)xrLabel16,
				(XRControl)xrLabel6,
				(XRControl)xrLabel17,
				(XRControl)xrLabel44
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 513.9266f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1646f, 227.5416f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(253.9996f, 58.42003f);
			((XRControl)xrLabel4).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "Imagem";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)2;
			((XRControl)imagem).Borders = (BorderSide)0;
			((XRControl)imagem).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Image", (object)null, "Imagem")
			});
			((XRControl)imagem).Dpi = 254f;
			((XRControl)imagem).LocationFloat = new PointFloat(1646f, 285.9616f);
			((XRControl)imagem).Name = "imagem";
			((XRControl)imagem).SizeF = new SizeF(253.9996f, 227.965f);
			imagem.Sizing = (ImageSizeMode)5;
			((XRControl)imagem).StylePriority.UseBorders = false;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(0f, 9f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(1900f, 5.291667f);
			((XRControl)xrLabel13).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzidoDescricao")
			});
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 285.9616f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(1070.522f, 58.41995f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "xrLabel13";
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.EstoqueSaida.Quantidade", "{0:0.####}")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1438.001f, 285.9616f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(207.9993f, 58.41995f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.UnidadeMedida")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1070.522f, 285.9616f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(158.458f, 58.41995f);
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel43).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Quantidade", "{0:0.####}")
			});
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel43).LocationFloat = new PointFloat(1228.98f, 285.9616f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(209.0209f, 58.42f);
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel43).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel43).Text = "xrLabel14";
			((XRControl)xrLabel43).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel22).Borders = (BorderSide)0;
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(0f, 39.28162f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(214.3124f, 58.42f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).Text = "Descrição:";
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel21).Borders = (BorderSide)0;
			((XRControl)xrLabel21).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(214.3122f, 39.28162f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(1685.688f, 58.42f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Text = "xrLabel21";
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(0f, 97.70161f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(336.0208f, 58.42f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Data de emissão:";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel15).Borders = (BorderSide)0;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(336.0206f, 97.70161f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(283.1041f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "xrLabel15";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1175.041f, 97.70161f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(209.0208f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Situação:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Situacao.Descricao")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1384.062f, 97.70161f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(515.9377f, 58.42f);
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "xrLabel4";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 156.1216f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(254f, 58.41999f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Observação:";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel23).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Observacao")
			});
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(253.9998f, 156.1216f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(1646f, 58.41999f);
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).Text = "xrLabel23";
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)4096;
			((XRControl)xrLabel16).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(0f, 227.5416f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(1070.522f, 58.42003f);
			((XRControl)xrLabel16).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).Text = "Produto produzido";
			((XRControl)xrLabel6).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1070.522f, 227.5416f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(158.4583f, 58.42003f);
			((XRControl)xrLabel6).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "Unidade";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel17).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(1228.98f, 227.5416f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(209.0209f, 58.42003f);
			((XRControl)xrLabel17).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Prevista";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel44).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel44).Borders = (BorderSide)8;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1438.001f, 227.5416f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(207.9993f, 58.42003f);
			((XRControl)xrLabel44).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).Text = "Produzida";
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)2;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 0f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 65.29931f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(OrdemProducaoDto);
			((DataContainerComponent)toText).DataMember = "FichaTecnicaDto.EtapasProducao";
			toText.Expression = "Concat(PadLeft([Hora], 2, '0'), ':', PadLeft([Minuto], 2, '0'), ':', PadLeft([Segundo], 2, '0'))";
			toText.Name = "toText";
			((DataContainerComponent)descricaoMateriaPrima).DataMember = "Itens";
			descricaoMateriaPrima.Expression = "Iif([PossuiOrdemProducaoVinculada] == False, [ProdutoDescricao] ,[OrdemProducaoVinculadaDescricao])";
			descricaoMateriaPrima.FieldType = (FieldType)1;
			descricaoMateriaPrima.Name = "descricaoMateriaPrima";
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)DetailReport1
			});
			((XtraReportBase)DetailReport).DataMember = "Etapas";
			((XtraReportBase)DetailReport).DataSource = bindingSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 0f;
			((XRControl)Detail1).Name = "Detail1";
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail2,
				(Band)ReportHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport1).DataMember = "Etapas.Itens";
			((XtraReportBase)DetailReport1).DataSource = bindingSource1;
			((XRControl)DetailReport1).Dpi = 254f;
			DetailReport1.Level = 0;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel2,
				(XRControl)lblUnidade,
				(XRControl)lblQtdUtilizada
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).HeightF = 58.42f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(44.97827f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1388.334f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)256;
			((XRControl)lblUnidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.UnidadeMedida")
			});
			((XRControl)lblUnidade).Dpi = 254f;
			((XRControl)lblUnidade).LocationFloat = new PointFloat(1433.312f, 0f);
			((XRControl)lblUnidade).Name = "lblUnidade";
			((XRControl)lblUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblUnidade).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)lblUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblUnidade).TextAlignment = (TextAlignment)512;
			((XRControl)lblQtdUtilizada).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.QuantidadeNecessaria", "{0:0.########}")
			});
			((XRControl)lblQtdUtilizada).Dpi = 254f;
			((XRControl)lblQtdUtilizada).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblQtdUtilizada).Name = "lblQtdUtilizada";
			((XRControl)lblQtdUtilizada).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtdUtilizada).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblQtdUtilizada).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtdUtilizada).TextAlignment = (TextAlignment)1024;
			((XRControl)lblQtdUtilizada).BeforePrint += lblQtdUtilizada_BeforePrint;
			((XRControl)ReportHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)lblQtd
			});
			((XRControl)ReportHeader1).Dpi = 254f;
			((XRControl)ReportHeader1).HeightF = 58.42001f;
			((XRControl)ReportHeader1).Name = "ReportHeader1";
			((XRControl)xrLabel7).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(44.97884f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(1388.333f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Matéria-prima";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel8).Borders = (BorderSide)8;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1433.312f, 0f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Unidade";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)32;
			((XRControl)lblQtd).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)lblQtd).Borders = (BorderSide)8;
			((XRControl)lblQtd).Dpi = 254f;
			((XRControl)lblQtd).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)lblQtd).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblQtd).Name = "lblQtd";
			((XRControl)lblQtd).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtd).SizeF = new SizeF(299.9999f, 58.42001f);
			((XRControl)lblQtd).StylePriority.UseBorderDashStyle = false;
			((XRControl)lblQtd).StylePriority.UseBorders = false;
			((XRControl)lblQtd).StylePriority.UseFont = false;
			((XRControl)lblQtd).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtd).Text = "Qtd necessária";
			((XRControl)lblQtd).TextAlignment = (TextAlignment)64;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblTotalQtdNecessaria });
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)lblTotalQtdNecessaria).Borders = (BorderSide)2;
			((XRControl)lblTotalQtdNecessaria).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.QuantidadeProduto")
			});
			((XRControl)lblTotalQtdNecessaria).Dpi = 254f;
			((XRControl)lblTotalQtdNecessaria).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)lblTotalQtdNecessaria).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblTotalQtdNecessaria).Name = "lblTotalQtdNecessaria";
			((XRControl)lblTotalQtdNecessaria).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTotalQtdNecessaria).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseBorders = false;
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseFont = false;
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:0.########}";
			val.Running = (SummaryRunning)1;
			lblTotalQtdNecessaria.Summary = val;
			((XRControl)lblTotalQtdNecessaria).TextAlignment = (TextAlignment)1024;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrSubreport1 });
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 113.7708f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)xrSubreport1).Dpi = 254f;
			((XRControl)xrSubreport1).LocationFloat = new PointFloat(0f, 55.35084f);
			((XRControl)xrSubreport1).Name = "xrSubreport1";
			((SubreportBase)xrSubreport1).ReportSource = (XtraReport)(object)new ResumoMateriaPrima();
			((XRControl)xrSubreport1).SizeF = new SizeF(1900f, 58.41999f);
			((DataContainerComponent)QuantidadeProduto).DataMember = "Etapas.Itens";
			QuantidadeProduto.Expression = "Iif([UnidadeMedida] == 'H', 0, [QuantidadeNecessaria])";
			QuantidadeProduto.Name = "QuantidadeProduto";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)ReportFooter
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[3] { toText, descricaoMateriaPrima, QuantidadeProduto });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 65);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoControleEtapa : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly OrdemProducaoDto _dto;

		private readonly IEnumerable<EtapaOrdemProducaoDto> _etapasProducao;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private XRLabel lblDataEmissao;

		private XRLabel lblProdutoProduzidoQuantidade;

		private XRLabel lblProdutoProduzidoDescricao;

		private BindingSource bindingSource1;

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel7;

		private XRLabel xrLabel12;

		private GroupHeaderBand GroupHeader3;

		private XRLabel xrLabel18;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel19;

		private CalculatedField toText;

		private XRLabel xrLabel20;

		private XRLabel xrLabel22;

		private XRLabel lblDescricao;

		private XRLabel lblQuantidade;

		private XRLabel xrLabel10;

		private XRLabel xrLabel9;

		private XRLabel xrLabel5;

		private XRLabel xrLabel11;

		private XRLabel lblSituacao;

		private XRLabel lblObservacao;

		private XRLabel lblProdutoProduzidoObservacao;

		private XRLabel xrLabel14;

		private XRLabel xrLabel15;

		private XRLabel xrLabel13;

		private XRLabel xrLabel4;

		private DetailReportBand DetailReport2;

		private DetailBand Detail3;

		private XRLabel lblUnidade;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel21;

		private XRLabel xrLabel8;

		private XRLine xrLine4;

		private XRLine xrLine3;

		private XRLine xrLine2;

		private XRLabel xrLabel25;

		private XRLine xrLine1;

		private XRLabel xrLabel24;

		private XRLine xrLine5;

		private CalculatedField descricaoMateriaPrima;

		private XRLabel lblProdutoProduzidoUnidade;

		private XRLabel xrLabel23;

		private XRLabel xrLabel28;

		private XRLabel xrLabel27;

		private XRLabel xrLabel26;

		public string TituloRelatorio => "Controle de etapa";

		public string SubTituloRelatorio => $"Número: {_dto.Numero}";

		public bool Retrato => true;

		public ImpressoControleEtapa(OrdemProducaoDto dto, IEnumerable<EtapaOrdemProducaoDto> etapasProducao)
		{
			_dto = dto;
			_etapasProducao = etapasProducao;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XRControl)lblDescricao).Text = _dto.Descricao;
				((XRControl)lblDataEmissao).Text = _dto.DataHoraEmissao.ToString("dd/MM/yyyy");
				((XRControl)lblObservacao).Text = _dto.Observacao;
				((XRControl)lblSituacao).Text = _dto.Situacao.Descricao;
				((XRControl)lblProdutoProduzidoDescricao).Text = _dto.ProdutoProduzidoDescricao;
				((XRControl)lblProdutoProduzidoQuantidade).Text = HelperDecimal.FormatarCampoValor(_dto.Quantidade, 3, 0, false);
				((XRControl)lblProdutoProduzidoObservacao).Text = _dto.FichaTecnicaDto.Observacao;
				((XRControl)lblProdutoProduzidoUnidade).Text = _dto.ProdutoProduzido.UnidadeMedida;
				((XtraReportBase)this).DataSource = _etapasProducao;
				return this;
			}));
		}

		private void lblQuantidade_BeforePrint(object sender, PrintEventArgs e)
		{
			if (((XRControl)lblUnidade).Text != "H")
			{
				return;
			}
			try
			{
				int num = (int)HelperString.ToDecimal(((XRControl)lblQuantidade).Text, 10, 0);
				int num2 = (int)Math.Round((HelperString.ToDecimal(((XRControl)lblQuantidade).Text, 10, 0) - (decimal)num) * 100m) * 60;
				((XRControl)lblQuantidade).Text = $"{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num, 3), 3)}:{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num2, 2), 2)}";
			}
			catch (Exception)
			{
			}
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
			//IL_0336: Unknown result type (might be due to invalid IL or missing references)
			//IL_0378: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0438: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_056c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0595: Unknown result type (might be due to invalid IL or missing references)
			//IL_0632: Unknown result type (might be due to invalid IL or missing references)
			//IL_065b: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0721: Unknown result type (might be due to invalid IL or missing references)
			//IL_07be: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0878: Unknown result type (might be due to invalid IL or missing references)
			//IL_087e: Expected O, but got Unknown
			//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0978: Unknown result type (might be due to invalid IL or missing references)
			//IL_097e: Expected O, but got Unknown
			//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d7e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f94: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_106f: Unknown result type (might be due to invalid IL or missing references)
			//IL_10c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1112: Unknown result type (might be due to invalid IL or missing references)
			//IL_1118: Expected O, but got Unknown
			//IL_113d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1166: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_11e6: Expected O, but got Unknown
			//IL_120b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1234: Unknown result type (might be due to invalid IL or missing references)
			//IL_12cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_12f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_13d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1563: Unknown result type (might be due to invalid IL or missing references)
			//IL_158c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1634: Unknown result type (might be due to invalid IL or missing references)
			//IL_165d: Unknown result type (might be due to invalid IL or missing references)
			//IL_170a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1733: Unknown result type (might be due to invalid IL or missing references)
			//IL_17df: Unknown result type (might be due to invalid IL or missing references)
			//IL_1808: Unknown result type (might be due to invalid IL or missing references)
			//IL_18b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_18de: Unknown result type (might be due to invalid IL or missing references)
			//IL_1986: Unknown result type (might be due to invalid IL or missing references)
			//IL_19af: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a91: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b73: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c38: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dd3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dfc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f9f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fff: Unknown result type (might be due to invalid IL or missing references)
			//IL_206d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2096: Unknown result type (might be due to invalid IL or missing references)
			//IL_211a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2120: Expected O, but got Unknown
			//IL_2145: Unknown result type (might be due to invalid IL or missing references)
			//IL_216e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2263: Unknown result type (might be due to invalid IL or missing references)
			//IL_228c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2346: Unknown result type (might be due to invalid IL or missing references)
			//IL_236f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2459: Unknown result type (might be due to invalid IL or missing references)
			//IL_2482: Unknown result type (might be due to invalid IL or missing references)
			//IL_251f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2548: Unknown result type (might be due to invalid IL or missing references)
			//IL_25e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_260e: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLine4 = new XRLine();
			xrLine3 = new XRLine();
			xrLine2 = new XRLine();
			xrLabel25 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel20 = new XRLabel();
			lblProdutoProduzidoObservacao = new XRLabel();
			xrLabel9 = new XRLabel();
			lblProdutoProduzidoQuantidade = new XRLabel();
			lblProdutoProduzidoDescricao = new XRLabel();
			lblDataEmissao = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			lblQuantidade = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel7 = new XRLabel();
			GroupHeader3 = new GroupHeaderBand();
			lblProdutoProduzidoUnidade = new XRLabel();
			xrLabel23 = new XRLabel();
			lblObservacao = new XRLabel();
			xrLabel11 = new XRLabel();
			lblSituacao = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel22 = new XRLabel();
			lblDescricao = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			toText = new CalculatedField();
			DetailReport2 = new DetailReportBand();
			Detail3 = new DetailBand();
			xrLine5 = new XRLine();
			xrLine1 = new XRLine();
			xrLabel24 = new XRLabel();
			lblUnidade = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			xrLabel21 = new XRLabel();
			xrLabel8 = new XRLabel();
			descricaoMateriaPrima = new CalculatedField();
			xrLabel26 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel28 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[15]
			{
				(XRControl)xrLabel28,
				(XRControl)xrLabel27,
				(XRControl)xrLabel26,
				(XRControl)xrLine4,
				(XRControl)xrLine3,
				(XRControl)xrLine2,
				(XRControl)xrLabel25,
				(XRControl)xrLabel14,
				(XRControl)xrLabel15,
				(XRControl)xrLabel13,
				(XRControl)xrLabel4,
				(XRControl)xrLabel1,
				(XRControl)xrLabel19,
				(XRControl)xrLabel12,
				(XRControl)xrLabel20
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 379.0447f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLine4).Dpi = 254f;
			xrLine4.LineWidth = 3;
			((XRControl)xrLine4).LocationFloat = new PointFloat(927.9166f, 348.0692f);
			((XRControl)xrLine4).Name = "xrLine4";
			((XRControl)xrLine4).SizeF = new SizeF(794.812f, 5.291672f);
			((XRControl)xrLine3).Dpi = 254f;
			xrLine3.LineWidth = 3;
			((XRControl)xrLine3).LocationFloat = new PointFloat(927.9166f, 284.7916f);
			((XRControl)xrLine3).Name = "xrLine3";
			((XRControl)xrLine3).SizeF = new SizeF(794.812f, 5.291672f);
			((XRControl)xrLine2).Dpi = 254f;
			xrLine2.LineWidth = 3;
			((XRControl)xrLine2).LocationFloat = new PointFloat(927.9166f, 218.4234f);
			((XRControl)xrLine2).Name = "xrLine2";
			((XRControl)xrLine2).SizeF = new SizeF(794.812f, 5.291672f);
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(634.2291f, 165.295f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(293.6875f, 58.42f);
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Colaboradores:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(0.0003229777f, 294.9409f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(140.2292f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Fim:";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(140.2295f, 294.9409f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(481.5417f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "____/____/________  ____:____";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(140.2295f, 165.295f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(481.5417f, 58.42f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "____/____/________  ____:____";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(0.0003229777f, 165.295f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(140.2292f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "Início:";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).BackColor = Color.Gainsboro;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 99.29498f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1534.875f, 58.42001f);
			((XRControl)xrLabel1).StylePriority.UseBackColor = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel19).BackColor = Color.Gainsboro;
			((XRControl)xrLabel19).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DuracaoPrevista")
			});
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(1534.875f, 99.29498f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(365.1243f, 58.42f);
			((XRControl)xrLabel19).StylePriority.UseBackColor = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel12).BackColor = Color.Transparent;
			((XRControl)xrLabel12).Borders = (BorderSide)8;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(0f, 40.87501f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(1534.875f, 58.42001f);
			((XRControl)xrLabel12).StylePriority.UseBackColor = false;
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Etapa de produção";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel20).BackColor = Color.Transparent;
			((XRControl)xrLabel20).Borders = (BorderSide)8;
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1534.875f, 40.87501f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(365.1249f, 58.42001f);
			((XRControl)xrLabel20).StylePriority.UseBackColor = false;
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).Text = "Duração prevista";
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)64;
			((XRControl)lblProdutoProduzidoObservacao).Dpi = 254f;
			((XRControl)lblProdutoProduzidoObservacao).Font = new Font("Segoe UI", 11f);
			((XRControl)lblProdutoProduzidoObservacao).LocationFloat = new PointFloat(235.4788f, 325.8608f);
			((XRControl)lblProdutoProduzidoObservacao).Name = "lblProdutoProduzidoObservacao";
			((XRControl)lblProdutoProduzidoObservacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoObservacao).SizeF = new SizeF(1664.521f, 58.42f);
			((XRControl)lblProdutoProduzidoObservacao).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoObservacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblProdutoProduzidoObservacao).Text = "lblProdutoProduzidoObservacao";
			((XRControl)lblProdutoProduzidoObservacao).TextAlignment = (TextAlignment)4096;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(0f, 325.8608f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(235.4792f, 58.42f);
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Observação:";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)lblProdutoProduzidoQuantidade).Dpi = 254f;
			((XRControl)lblProdutoProduzidoQuantidade).Font = new Font("Segoe UI", 11f);
			((XRControl)lblProdutoProduzidoQuantidade).LocationFloat = new PointFloat(1646f, 267.4409f);
			((XRControl)lblProdutoProduzidoQuantidade).Name = "lblProdutoProduzidoQuantidade";
			((XRControl)lblProdutoProduzidoQuantidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoQuantidade).SizeF = new SizeF(254f, 58.42f);
			((XRControl)lblProdutoProduzidoQuantidade).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoQuantidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblProdutoProduzidoQuantidade).Text = "lblProdutoProduzidoQuantidade";
			((XRControl)lblProdutoProduzidoQuantidade).TextAlignment = (TextAlignment)2;
			((XRControl)lblProdutoProduzidoDescricao).Dpi = 254f;
			((XRControl)lblProdutoProduzidoDescricao).Font = new Font("Segoe UI", 11f);
			((XRControl)lblProdutoProduzidoDescricao).LocationFloat = new PointFloat(0f, 267.4408f);
			((XRControl)lblProdutoProduzidoDescricao).Name = "lblProdutoProduzidoDescricao";
			((XRControl)lblProdutoProduzidoDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoDescricao).SizeF = new SizeF(1471.375f, 58.41998f);
			((XRControl)lblProdutoProduzidoDescricao).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoDescricao).Text = "lblProdutoProduzidoDescricao";
			((XRControl)lblDataEmissao).Borders = (BorderSide)0;
			((XRControl)lblDataEmissao).Dpi = 254f;
			((XRControl)lblDataEmissao).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)lblDataEmissao).LocationFloat = new PointFloat(336.0208f, 63.5f);
			((XRControl)lblDataEmissao).Name = "lblDataEmissao";
			((XRControl)lblDataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDataEmissao).SizeF = new SizeF(283.1041f, 58.42f);
			((XRControl)lblDataEmissao).StylePriority.UseBorders = false;
			((XRControl)lblDataEmissao).StylePriority.UseFont = false;
			((XRControl)lblDataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)lblDataEmissao).Text = "lblDataEmissao";
			((XRControl)lblDataEmissao).TextAlignment = (TextAlignment)2;
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
			bindingSource1.DataSource = typeof(EtapaOrdemProducaoDto);
			((XRControl)lblQuantidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeNecessaria", "{0:0.####}")
			});
			((XRControl)lblQuantidade).Dpi = 254f;
			((XRControl)lblQuantidade).LocationFloat = new PointFloat(1339.688f, 0f);
			((XRControl)lblQuantidade).Name = "lblQuantidade";
			((XRControl)lblQuantidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQuantidade).SizeF = new SizeF(295.3129f, 58.42f);
			((XRControl)lblQuantidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblQuantidade).TextAlignment = (TextAlignment)512;
			((XRControl)lblQuantidade).BeforePrint += lblQuantidade_BeforePrint;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.descricaoMateriaPrima")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(44.97919f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1128.022f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)256;
			((XRControl)xrLabel2).WordWrap = false;
			((XRControl)xrLabel10).Borders = (BorderSide)8;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1339.688f, 23.8125f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(295.3129f, 58.42001f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Qtd necessária";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(44.97919f, 23.8125f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(1128.022f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Matéria-prima";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader3).Controls.AddRange((XRControl[])(object)new XRControl[16]
			{
				(XRControl)lblProdutoProduzidoUnidade,
				(XRControl)xrLabel23,
				(XRControl)lblObservacao,
				(XRControl)xrLabel11,
				(XRControl)lblSituacao,
				(XRControl)xrLabel5,
				(XRControl)xrLabel22,
				(XRControl)lblDescricao,
				(XRControl)xrLabel18,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16,
				(XRControl)lblDataEmissao,
				(XRControl)lblProdutoProduzidoObservacao,
				(XRControl)lblProdutoProduzidoQuantidade,
				(XRControl)xrLabel9,
				(XRControl)lblProdutoProduzidoDescricao
			});
			((XRControl)GroupHeader3).Dpi = 254f;
			((XRControl)GroupHeader3).HeightF = 384.2808f;
			((XRControl)GroupHeader3).Name = "GroupHeader3";
			((XRControl)lblProdutoProduzidoUnidade).Dpi = 254f;
			((XRControl)lblProdutoProduzidoUnidade).Font = new Font("Segoe UI", 11f);
			((XRControl)lblProdutoProduzidoUnidade).LocationFloat = new PointFloat(1471.375f, 267.4407f);
			((XRControl)lblProdutoProduzidoUnidade).Name = "lblProdutoProduzidoUnidade";
			((XRControl)lblProdutoProduzidoUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoUnidade).SizeF = new SizeF(174.625f, 58.42004f);
			((XRControl)lblProdutoProduzidoUnidade).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblProdutoProduzidoUnidade).Text = "lblProdutoProduzidoUnidade";
			((XRControl)lblProdutoProduzidoUnidade).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel23).Borders = (BorderSide)8;
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(1471.375f, 209.0208f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(174.625f, 58.41997f);
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).Text = "Unidade";
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)2;
			((XRControl)lblObservacao).Dpi = 254f;
			((XRControl)lblObservacao).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)lblObservacao).LocationFloat = new PointFloat(254f, 121.92f);
			((XRControl)lblObservacao).Name = "lblObservacao";
			((XRControl)lblObservacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblObservacao).SizeF = new SizeF(1646f, 58.41999f);
			((XRControl)lblObservacao).StylePriority.UseFont = false;
			((XRControl)lblObservacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblObservacao).Text = "lblObservacao";
			((XRControl)lblObservacao).TextAlignment = (TextAlignment)4096;
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1354.958f, 63.5f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(209.0208f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Situação:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)lblSituacao).Dpi = 254f;
			((XRControl)lblSituacao).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)lblSituacao).LocationFloat = new PointFloat(1563.979f, 63.5f);
			((XRControl)lblSituacao).Name = "lblSituacao";
			((XRControl)lblSituacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblSituacao).SizeF = new SizeF(336.0208f, 58.42f);
			((XRControl)lblSituacao).StylePriority.UseFont = false;
			((XRControl)lblSituacao).StylePriority.UseTextAlignment = false;
			((XRControl)lblSituacao).Text = "lblSituacao";
			((XRControl)lblSituacao).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 121.92f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(254f, 58.41999f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Observação:";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel22).Borders = (BorderSide)0;
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(214.3124f, 58.42f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).Text = "Descrição:";
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)1;
			((XRControl)lblDescricao).Borders = (BorderSide)0;
			((XRControl)lblDescricao).Dpi = 254f;
			((XRControl)lblDescricao).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)lblDescricao).LocationFloat = new PointFloat(214.3124f, 0f);
			((XRControl)lblDescricao).Name = "lblDescricao";
			((XRControl)lblDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDescricao).SizeF = new SizeF(1685.688f, 58.42f);
			((XRControl)lblDescricao).StylePriority.UseBorders = false;
			((XRControl)lblDescricao).StylePriority.UseFont = false;
			((XRControl)lblDescricao).Text = "lblDescricao";
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(0f, 63.5f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(336.0208f, 58.42f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Data de emissão:";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(1646f, 209.0208f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Quantidade";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(0f, 209.0208f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(1471.375f, 58.42f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).Text = "Produto produzido";
			((DataContainerComponent)toText).DataMember = "FichaTecnicaDto.EtapasProducao";
			toText.Expression = "Concat(PadLeft([Hora], 2, '0'), ':', PadLeft([Minuto], 2, '0'), ':', PadLeft([Segundo], 2, '0'))";
			toText.Name = "toText";
			((XtraReportBase)DetailReport2).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail3,
				(Band)GroupHeader2
			});
			((XtraReportBase)DetailReport2).DataMember = "Itens";
			((XtraReportBase)DetailReport2).DataSource = bindingSource1;
			((XRControl)DetailReport2).Dpi = 254f;
			DetailReport2.Level = 0;
			((XRControl)DetailReport2).Name = "DetailReport2";
			((XRControl)Detail3).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLine5,
				(XRControl)xrLine1,
				(XRControl)xrLabel24,
				(XRControl)xrLabel2,
				(XRControl)lblUnidade,
				(XRControl)lblQuantidade
			});
			((XRControl)Detail3).Dpi = 254f;
			((XRControl)Detail3).HeightF = 130.0693f;
			((XRControl)Detail3).Name = "Detail3";
			((XRControl)xrLine5).Dpi = 254f;
			xrLine5.LineWidth = 3;
			((XRControl)xrLine5).LocationFloat = new PointFloat(1635.001f, 53.12831f);
			((XRControl)xrLine5).Name = "xrLine5";
			((XRControl)xrLine5).SizeF = new SizeF(264.9983f, 5.291668f);
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(140.2295f, 111.5483f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(1759.77f, 5.291664f);
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(44.97919f, 58.42f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(95.25034f, 58.41999f);
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "Obs:";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)256;
			((XRControl)lblUnidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida")
			});
			((XRControl)lblUnidade).Dpi = 254f;
			((XRControl)lblUnidade).LocationFloat = new PointFloat(1173.001f, 0f);
			((XRControl)lblUnidade).Name = "lblUnidade";
			((XRControl)lblUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblUnidade).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)lblUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblUnidade).TextAlignment = (TextAlignment)512;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel21,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel10
			});
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 82.23251f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1635.001f, 23.8125f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(264.9983f, 58.42001f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel21).Text = "Qtd utilizada";
			((XRControl)xrLabel21).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel8).Borders = (BorderSide)8;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1173.001f, 23.8125f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Unidade";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)32;
			((DataContainerComponent)descricaoMateriaPrima).DataMember = "Itens";
			descricaoMateriaPrima.Expression = "Iif([PossuiOrdemProducaoVinculada] == False, [ProdutoDescricao] ,[OrdemProducaoVinculadaDescricao])";
			descricaoMateriaPrima.FieldType = (FieldType)1;
			descricaoMateriaPrima.Name = "descricaoMateriaPrima";
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(1743.373f, 173.295f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(148.6272f, 58.42001f);
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).Text = "____:____";
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1743.373f, 237.715f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(148.6272f, 58.42001f);
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).Text = "____:____";
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(1743.373f, 301.6246f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(148.6272f, 58.42001f);
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).Text = "____:____";
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)16;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader3,
				(Band)DetailReport2
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[2] { toText, descricaoMateriaPrima });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoOrdemDesmontagem : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly OrdemDesmontagemDto _dto;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private GroupHeaderBand GroupHeader1;

		private XRLabel lblProdutoProduzidoDescricao;

		private XRLabel lblProdutoProduzidoQuantidade;

		private XRLabel xrLabel2;

		private XRLabel xrLabel1;

		private XRLabel lblQuantidade;

		private XRLabel xrLabel6;

		private ObjectDataSource objectDataSource1;

		private XRLine xrLine5;

		private XRLabel lblUnidade;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel xrLabel10;

		private XRLabel xrLabel21;

		private XRLine xrLine1;

		private XRLabel xrLabel9;

		private XRLabel xrLabel15;

		private XRLabel xrLabel13;

		private XRLabel xrLabel11;

		private XRLabel xrLabel14;

		private XRLabel xrLabel25;

		private XRLine xrLine2;

		private XRLine xrLine3;

		private XRLine xrLine4;

		private XRRichText xrRichText1;

		private XRRichText lblProdutoProduzidoEstoque;

		private XRLabel lblProdutoProduzidoUnidade;

		private XRLabel xrLabel8;

		public string TituloRelatorio => "Ordem de desmontagem";

		public string SubTituloRelatorio => $"Número da ordem de produção: {_dto.NumeroOrdemProducao}";

		public bool Retrato => true;

		public ImpressoOrdemDesmontagem(OrdemDesmontagemDto dto)
		{
			_dto = dto;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XRControl)lblProdutoProduzidoDescricao).Text = _dto.ProdutoProduzido.ProdutoDescricao;
				((XRControl)lblProdutoProduzidoQuantidade).Text = HelperDecimal.FormatarCampoValor(_dto.ProdutoProduzido.EstoqueUtilizado.Quantidade, 3, 0, false);
				((XRControl)lblProdutoProduzidoEstoque).Text = ((BaseDto)_dto.ProdutoProduzido).ToText;
				((XRControl)lblProdutoProduzidoUnidade).Text = _dto.ProdutoProduzido.UnidadeMedida;
				((XtraReportBase)this).DataSource = _dto.MateriasPrimas;
				return this;
			}));
		}

		private void xrRichText1_BeforePrint(object sender, PrintEventArgs e)
		{
			((XRControl)((sender is XRRichText) ? sender : null)).Font = new Font("Segoe UI", 10f);
		}

		private void lblQuantidade_BeforePrint(object sender, PrintEventArgs e)
		{
			if (((XRControl)lblUnidade).Text != "H")
			{
				return;
			}
			try
			{
				int num = (int)HelperString.ToDecimal(((XRControl)lblQuantidade).Text, 10, 0);
				int num2 = (int)Math.Round((HelperString.ToDecimal(((XRControl)lblQuantidade).Text, 10, 0) - (decimal)num) * 100m) * 60;
				((XRControl)lblQuantidade).Text = $"{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num, 3), 3)}:{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num2, 2), 2)}";
			}
			catch (Exception)
			{
			}
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
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
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
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected O, but got Unknown
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Expected O, but got Unknown
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Expected O, but got Unknown
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Expected O, but got Unknown
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Expected O, but got Unknown
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Expected O, but got Unknown
			//IL_021f: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0265: Expected O, but got Unknown
			//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0323: Unknown result type (might be due to invalid IL or missing references)
			//IL_0329: Expected O, but got Unknown
			//IL_034e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0377: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_044a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0450: Expected O, but got Unknown
			//IL_0475: Unknown result type (might be due to invalid IL or missing references)
			//IL_049e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0518: Unknown result type (might be due to invalid IL or missing references)
			//IL_051e: Expected O, but got Unknown
			//IL_0543: Unknown result type (might be due to invalid IL or missing references)
			//IL_056c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0600: Unknown result type (might be due to invalid IL or missing references)
			//IL_0655: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0809: Unknown result type (might be due to invalid IL or missing references)
			//IL_086e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0897: Unknown result type (might be due to invalid IL or missing references)
			//IL_0905: Unknown result type (might be due to invalid IL or missing references)
			//IL_0989: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
			//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e19: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ecd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ef6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f99: Unknown result type (might be due to invalid IL or missing references)
			//IL_1053: Unknown result type (might be due to invalid IL or missing references)
			//IL_107c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1136: Unknown result type (might be due to invalid IL or missing references)
			//IL_115f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1219: Unknown result type (might be due to invalid IL or missing references)
			//IL_1242: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_12ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1353: Unknown result type (might be due to invalid IL or missing references)
			//IL_137c: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1413: Unknown result type (might be due to invalid IL or missing references)
			//IL_1492: Unknown result type (might be due to invalid IL or missing references)
			//IL_14bb: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoOrdemDesmontagem));
			Detail = new DetailBand();
			xrRichText1 = new XRRichText();
			lblUnidade = new XRLabel();
			xrLine5 = new XRLine();
			lblQuantidade = new XRLabel();
			xrLabel6 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			GroupHeader1 = new GroupHeaderBand();
			lblProdutoProduzidoUnidade = new XRLabel();
			xrLabel8 = new XRLabel();
			lblProdutoProduzidoEstoque = new XRRichText();
			xrLabel15 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLine2 = new XRLine();
			xrLine3 = new XRLine();
			xrLine4 = new XRLine();
			xrLine1 = new XRLine();
			xrLabel9 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel1 = new XRLabel();
			lblProdutoProduzidoQuantidade = new XRLabel();
			lblProdutoProduzidoDescricao = new XRLabel();
			objectDataSource1 = new ObjectDataSource(components);
			((ISupportInitialize)xrRichText1).BeginInit();
			((ISupportInitialize)lblProdutoProduzidoEstoque).BeginInit();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrRichText1,
				(XRControl)lblUnidade,
				(XRControl)xrLine5,
				(XRControl)lblQuantidade,
				(XRControl)xrLabel6
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 116.84f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrRichText1).CanShrink = true;
			((XRControl)xrRichText1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Rtf", (object)null, "ToText")
			});
			((XRControl)xrRichText1).Dpi = 254f;
			((XRControl)xrRichText1).Font = new Font("Segoe UI", 10f);
			((XRControl)xrRichText1).LocationFloat = new PointFloat(52.91667f, 58.41996f);
			((XRControl)xrRichText1).Name = "xrRichText1";
			((XRRichTextBase)xrRichText1).SerializableRtfString = componentResourceManager.GetString("xrRichText1.SerializableRtfString");
			((XRControl)xrRichText1).SizeF = new SizeF(1560.918f, 58.42f);
			((XRControl)xrRichText1).BeforePrint += xrRichText1_BeforePrint;
			((XRControl)lblUnidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "UnidadeMedida")
			});
			((XRControl)lblUnidade).Dpi = 254f;
			((XRControl)lblUnidade).LocationFloat = new PointFloat(1151.835f, 0f);
			((XRControl)lblUnidade).Name = "lblUnidade";
			((XRControl)lblUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblUnidade).SizeF = new SizeF(166.6871f, 58.42f);
			((XRControl)lblUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblUnidade).Text = "lblUnidade";
			((XRControl)lblUnidade).TextAlignment = (TextAlignment)512;
			((XRControl)xrLine5).Dpi = 254f;
			xrLine5.LineWidth = 3;
			((XRControl)xrLine5).LocationFloat = new PointFloat(1613.835f, 53.12822f);
			((XRControl)xrLine5).Name = "xrLine5";
			((XRControl)xrLine5).SizeF = new SizeF(264.9983f, 5.291668f);
			((XRControl)lblQuantidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EstoqueUtilizado.Quantidade")
			});
			((XRControl)lblQuantidade).Dpi = 254f;
			((XRControl)lblQuantidade).LocationFloat = new PointFloat(1318.522f, 0f);
			((XRControl)lblQuantidade).Name = "lblQuantidade";
			((XRControl)lblQuantidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQuantidade).SizeF = new SizeF(295.3124f, 58.42f);
			((XRControl)lblQuantidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblQuantidade).TextAlignment = (TextAlignment)512;
			((XRControl)lblQuantidade).BeforePrint += lblQuantidade_BeforePrint;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoDescricao")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(1151.835f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "xrLabel6";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)256;
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
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[21]
			{
				(XRControl)lblProdutoProduzidoUnidade,
				(XRControl)xrLabel8,
				(XRControl)lblProdutoProduzidoEstoque,
				(XRControl)xrLabel15,
				(XRControl)xrLabel13,
				(XRControl)xrLabel11,
				(XRControl)xrLabel14,
				(XRControl)xrLabel25,
				(XRControl)xrLine2,
				(XRControl)xrLine3,
				(XRControl)xrLine4,
				(XRControl)xrLine1,
				(XRControl)xrLabel9,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)xrLabel10,
				(XRControl)xrLabel21,
				(XRControl)xrLabel2,
				(XRControl)xrLabel1,
				(XRControl)lblProdutoProduzidoQuantidade,
				(XRControl)lblProdutoProduzidoDescricao
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 11f);
			((XRControl)GroupHeader1).HeightF = 567.2292f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoUnidade).Dpi = 254f;
			((XRControl)lblProdutoProduzidoUnidade).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)lblProdutoProduzidoUnidade).LocationFloat = new PointFloat(1714.751f, 58.42001f);
			((XRControl)lblProdutoProduzidoUnidade).Name = "lblProdutoProduzidoUnidade";
			((XRControl)lblProdutoProduzidoUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoUnidade).SizeF = new SizeF(174.1235f, 58.41998f);
			((XRControl)lblProdutoProduzidoUnidade).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoUnidade).Text = "lblProdutoProduzidoUnidade";
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1613.835f, 58.42001f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(100.9165f, 58.41998f);
			((XRControl)xrLabel8).Text = "Und:";
			((XRControl)lblProdutoProduzidoEstoque).Dpi = 254f;
			((XRControl)lblProdutoProduzidoEstoque).Font = new Font("Segoe UI", 10f);
			((XRControl)lblProdutoProduzidoEstoque).LocationFloat = new PointFloat(100.9166f, 116.84f);
			((XRControl)lblProdutoProduzidoEstoque).Name = "lblProdutoProduzidoEstoque";
			((XRRichTextBase)lblProdutoProduzidoEstoque).SerializableRtfString = componentResourceManager.GetString("lblProdutoProduzidoEstoque.SerializableRtfString");
			((XRControl)lblProdutoProduzidoEstoque).SizeF = new SizeF(1560.918f, 58.42f);
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(142.3338f, 410.5088f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(481.5417f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "____/____/________  ____:____";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(142.3338f, 280.8629f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(481.5417f, 58.42f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel13).Text = "____/____/________  ____:____";
			((XRControl)xrLabel13).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(2.104604f, 280.8629f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(140.2292f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Início:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(2.104604f, 410.5088f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(140.2292f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Fim:";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(636.3334f, 280.8629f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(293.6875f, 58.42f);
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Colaboradores:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLine2).Dpi = 254f;
			xrLine2.LineWidth = 3;
			((XRControl)xrLine2).LocationFloat = new PointFloat(930.0209f, 333.9913f);
			((XRControl)xrLine2).Name = "xrLine2";
			((XRControl)xrLine2).SizeF = new SizeF(958.8535f, 5.291656f);
			((XRControl)xrLine3).Dpi = 254f;
			xrLine3.LineWidth = 3;
			((XRControl)xrLine3).LocationFloat = new PointFloat(930.0209f, 400.3594f);
			((XRControl)xrLine3).Name = "xrLine3";
			((XRControl)xrLine3).SizeF = new SizeF(958.8535f, 5.291656f);
			((XRControl)xrLine4).Dpi = 254f;
			xrLine4.LineWidth = 3;
			((XRControl)xrLine4).LocationFloat = new PointFloat(930.0209f, 463.6371f);
			((XRControl)xrLine4).Name = "xrLine4";
			((XRControl)xrLine4).SizeF = new SizeF(958.8535f, 5.291656f);
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(473.6042f, 257.2809f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(349.9167f, 5.291656f);
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).LocationFloat = new PointFloat(0f, 204.1525f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(473.6042f, 58.42f);
			((XRControl)xrLabel9).Text = "Quantidade à desmontar:";
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(0f, 508.8092f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(1151.834f, 58.41998f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "Matéria-prima";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1151.834f, 508.8092f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "Unidade";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel10).Borders = (BorderSide)8;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1318.522f, 508.8092f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(295.3129f, 58.42001f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Qtd utilizada";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1613.835f, 508.8092f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(264.9983f, 58.42001f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel21).Text = "Qtd retornada";
			((XRControl)xrLabel21).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 58.42001f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(425.9791f, 58.41999f);
			((XRControl)xrLabel2).Text = "Quantidade produzida:";
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(362.4792f, 58.42f);
			((XRControl)xrLabel1).Text = "Produto produzido:";
			((XRControl)lblProdutoProduzidoQuantidade).Dpi = 254f;
			((XRControl)lblProdutoProduzidoQuantidade).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)lblProdutoProduzidoQuantidade).LocationFloat = new PointFloat(425.9791f, 58.42001f);
			((XRControl)lblProdutoProduzidoQuantidade).Name = "lblProdutoProduzidoQuantidade";
			((XRControl)lblProdutoProduzidoQuantidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoQuantidade).SizeF = new SizeF(1187.855f, 58.41998f);
			((XRControl)lblProdutoProduzidoQuantidade).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoQuantidade).Text = "lblProdutoProduzidoQuantidade";
			((XRControl)lblProdutoProduzidoDescricao).Dpi = 254f;
			((XRControl)lblProdutoProduzidoDescricao).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)lblProdutoProduzidoDescricao).LocationFloat = new PointFloat(362.4792f, 0f);
			((XRControl)lblProdutoProduzidoDescricao).Name = "lblProdutoProduzidoDescricao";
			((XRControl)lblProdutoProduzidoDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblProdutoProduzidoDescricao).SizeF = new SizeF(1526.395f, 58.42f);
			((XRControl)lblProdutoProduzidoDescricao).StylePriority.UseFont = false;
			((XRControl)lblProdutoProduzidoDescricao).Text = "lblProdutoProduzidoDescricao";
			objectDataSource1.DataSource = typeof(ItemOrdemDesmontagemDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader1
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2700;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)xrRichText1).EndInit();
			((ISupportInitialize)lblProdutoProduzidoEstoque).EndInit();
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoOrdemProducao : XtraReport, IRelatorioParametrosSemCabecalhoERodape, IRelatorioParametros, IViewModelItemImprimirRelatorio, IViewModelImprimir, IDisposable
	{
		private readonly OrdemProducaoDto _dto;

		private readonly IEnumerable<DesmontagemRelatorioDto> _desmontagemRelatorioDto;

		private readonly bool _exibirEtapas;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private BindingSource bindingSource1;

		private DetailReportBand etapas;

		private DetailBand Detail1;

		private XRLabel xrLabel1;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLabel lblUnidade;

		private XRLabel xrLabel2;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel12;

		private GroupHeaderBand GroupHeader3;

		private XRLabel xrLabel18;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel19;

		private CalculatedField toText;

		private XRLabel xrLabel20;

		private XRLabel xrLabel22;

		private XRLabel xrLabel21;

		private XRLabel lblQtdNecessaria;

		private XRLabel lblQtd;

		private XRLabel xrLabel9;

		private XRLabel xrLabel5;

		private XRLabel xrLabel11;

		private XRLabel xrLabel4;

		private XRLabel xrLabel23;

		private XRLabel xrLabel24;

		private XRRichText xrRichText1;

		private XRRichText xrRichText2;

		private CalculatedField descricaoMateriaPrima;

		private XRLabel lblQtdUtilizada;

		private XRLabel xrLabel10;

		private XRLabel xrLabel6;

		private DetailReportBand DesmontagemReport;

		private DetailBand Detail3;

		private DetailReportBand DetailReport3;

		private DetailBand Detail4;

		private ObjectDataSource objectDataSource1;

		private FormattingRule formattingRule1;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel30;

		private XRLabel xrLabel33;

		private XRLabel xrLabel34;

		private XRLabel xrLabel35;

		private XRLabel xrLabel36;

		private XRLabel xrLabel37;

		private XRLabel xrLabel38;

		private XRRichText informacaoEstoqueDesmontada;

		private GroupHeaderBand GroupHeader4;

		private XRLabel xrLabel29;

		private XRLabel xrLabel31;

		private XRLabel xrLabel32;

		private XRLabel xrLabel25;

		private XRLabel xrLabel26;

		private XRLabel xrLabel27;

		private XRRichText informacaoEstoqueMateriaPrimaRetornada;

		private SubBand observacao;

		private XRLabel xrLabel28;

		private DetailReportBand DetailReport2;

		private DetailBand Detail5;

		private XRLabel xrLabel39;

		private GroupHeaderBand GroupHeader5;

		private XRLabel xrLabel40;

		private FormattingRule esconderColaboradores;

		private DetailReportBand DetailReport4;

		private DetailBand Detail6;

		private XRLabel xrLabel41;

		private GroupHeaderBand GroupHeader6;

		private XRLabel xrLabel42;

		private FormattingRule esconderColaboradoresDesmontagem;

		private XRLabel lblTotalQtdNecessaria;

		private XRLabel lblTotalQtdUtilizada;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel43;

		private XRLabel xrLabel44;

		private PageHeaderBand PageHeader;

		private XRLabel Empresa;

		private XRLabel CNPJ;

		private XRLabel Fantasia;

		private XRLabel Titulo;

		private XRLabel SubTitulo;

		private PageFooterBand PageFooter;

		private XRLabel DataEmissao;

		private XRLabel xrLabel45;

		private XRPageInfo xrPageInfo1;

		private XRLabel xrLabel46;

		private XRPictureBox imagem;

		private GroupHeaderBand GroupHeader7;

		private XRLabel xrLabel47;

		private XRLabel xrLabel48;

		private CalculatedField QuantidadeProduto;

		public string TituloRelatorio => "Ordem de produção";

		public string SubTituloRelatorio => $"Número: {_dto.Numero}";

		public bool Retrato => true;

		public IList<TipoViewModelBase> Parametros { get; } = new List<TipoViewModelBase>();


		public string DescricaoDetalhada => string.Empty;

		public bool Favorito { get; set; }

		public ImpressoOrdemProducao(OrdemProducaoDto dto, bool exibirEtapas, IEnumerable<DesmontagemRelatorioDto> desmontagemRelatorioDto = null)
		{
			_dto = dto;
			_desmontagemRelatorioDto = desmontagemRelatorioDto ?? Enumerable.Empty<DesmontagemRelatorioDto>();
			_exibirEtapas = exibirEtapas;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew((Func<ImpressoOrdemProducao>)Processar));
		}

		public ImpressoOrdemProducao Processar()
		{
			((XRControl)Empresa).Text = DadosSistema.EmpresaLogada.Nome;
			((XRControl)Fantasia).Text = DadosSistema.EmpresaLogada.Fantasia;
			((XRControl)CNPJ).Text = DadosSistema.EmpresaLogada.CpfCnpjFormatado;
			((XRControl)Titulo).Text = TituloRelatorio;
			if (!string.IsNullOrEmpty(TituloRelatorio))
			{
				((XtraReport)this).DisplayName = TituloRelatorio;
			}
			((XRControl)SubTitulo).Text = SubTituloRelatorio;
			((XRControl)DataEmissao).Text = $"Gerado em {DateTime.Now:d' de 'MMMM' de 'yyyy}";
			if (_dto.Situacao.Codigo.Equals(2) || _dto.Situacao.Codigo.Equals(22))
			{
				((XRControl)lblQtd).Text = "Qtd utilizada";
				((XRControl)lblQtdUtilizada).Visible = true;
				((XRControl)lblQtdNecessaria).Visible = false;
				((XRControl)lblTotalQtdNecessaria).Visible = false;
				((XRControl)lblTotalQtdUtilizada).Visible = true;
			}
			else
			{
				((XRControl)lblQtd).Text = "Qtd necessária";
				((XRControl)lblQtdUtilizada).Visible = false;
				((XRControl)lblQtdNecessaria).Visible = true;
				((XRControl)lblTotalQtdNecessaria).Visible = true;
				((XRControl)lblTotalQtdUtilizada).Visible = false;
			}
			((XtraReportBase)this).DataSource = new List<OrdemProducaoDto> { _dto };
			objectDataSource1.DataSource = _desmontagemRelatorioDto;
			((XRControl)DesmontagemReport).Visible = _desmontagemRelatorioDto.Any();
			((XRControl)etapas).Visible = _exibirEtapas;
			((XRControl)observacao).Visible = _dto.Situacao.Codigo.Equals(21) || _dto.Situacao.Codigo.Equals(1);
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).PrintingSystem.Document.AutoFitToPagesWidth = 1;
			((XtraReport)this).CreateDocument();
			return this;
		}

		private void xrRichText1_BeforePrint(object sender, PrintEventArgs e)
		{
			FormatarFonte(sender);
		}

		private static void FormatarFonte(object sender)
		{
			((XRControl)((sender is XRRichText) ? sender : null)).Font = new Font("Segoe UI", 10f);
		}

		private void xrRichText2_BeforePrint(object sender, PrintEventArgs e)
		{
			FormatarFonte(sender);
		}

		private void informacaoEstoqueDesmontada_BeforePrint(object sender, PrintEventArgs e)
		{
			FormatarFonte(sender);
		}

		private void informacaoEstoqueMateriaPrimaRetornada_BeforePrint(object sender, PrintEventArgs e)
		{
			FormatarFonte(sender);
		}

		private void lblQtdNecessaria_BeforePrint(object sender, PrintEventArgs e)
		{
			if (((XRControl)lblUnidade).Text != "H")
			{
				return;
			}
			try
			{
				int num = (int)HelperString.ToDecimal(((XRControl)lblQtdNecessaria).Text, 10, 0);
				int num2 = (int)Math.Round((HelperString.ToDecimal(((XRControl)lblQtdNecessaria).Text, 10, 0) - (decimal)num) * 100m) * 60;
				((XRControl)lblQtdNecessaria).Text = $"{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num, 3), 3)}:{HelperString.CopiarTexto(HelperInteger.ToStringLeftZero(num2, 2), 2)}";
			}
			catch (Exception)
			{
			}
		}

		private void lblQtdUtilizada_BeforePrint(object sender, PrintEventArgs e)
		{
			if (((XRControl)lblUnidade).Text != "H")
			{
				return;
			}
			try
			{
				((XRControl)lblQtdUtilizada).Text = string.Empty;
			}
			catch (Exception)
			{
			}
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
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
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
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Expected O, but got Unknown
			//IL_014c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Expected O, but got Unknown
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Expected O, but got Unknown
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Expected O, but got Unknown
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Expected O, but got Unknown
			//IL_018e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Expected O, but got Unknown
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a3: Expected O, but got Unknown
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ae: Expected O, but got Unknown
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Expected O, but got Unknown
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Expected O, but got Unknown
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cf: Expected O, but got Unknown
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01da: Expected O, but got Unknown
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e5: Expected O, but got Unknown
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Expected O, but got Unknown
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Expected O, but got Unknown
			//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Expected O, but got Unknown
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_0211: Expected O, but got Unknown
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Expected O, but got Unknown
			//IL_021d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0227: Expected O, but got Unknown
			//IL_0228: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Expected O, but got Unknown
			//IL_0233: Unknown result type (might be due to invalid IL or missing references)
			//IL_023d: Expected O, but got Unknown
			//IL_023e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Expected O, but got Unknown
			//IL_0249: Unknown result type (might be due to invalid IL or missing references)
			//IL_0253: Expected O, but got Unknown
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_025e: Expected O, but got Unknown
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0269: Expected O, but got Unknown
			//IL_026a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0274: Expected O, but got Unknown
			//IL_0275: Unknown result type (might be due to invalid IL or missing references)
			//IL_027f: Expected O, but got Unknown
			//IL_0280: Unknown result type (might be due to invalid IL or missing references)
			//IL_028a: Expected O, but got Unknown
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0295: Expected O, but got Unknown
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Expected O, but got Unknown
			//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ab: Expected O, but got Unknown
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b6: Expected O, but got Unknown
			//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c1: Expected O, but got Unknown
			//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02cc: Expected O, but got Unknown
			//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d7: Expected O, but got Unknown
			//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Expected O, but got Unknown
			//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ed: Expected O, but got Unknown
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Expected O, but got Unknown
			//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0303: Expected O, but got Unknown
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_030e: Expected O, but got Unknown
			//IL_030f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0319: Expected O, but got Unknown
			//IL_031a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Expected O, but got Unknown
			//IL_0325: Unknown result type (might be due to invalid IL or missing references)
			//IL_032f: Expected O, but got Unknown
			//IL_0330: Unknown result type (might be due to invalid IL or missing references)
			//IL_033a: Expected O, but got Unknown
			//IL_033b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0345: Expected O, but got Unknown
			//IL_0346: Unknown result type (might be due to invalid IL or missing references)
			//IL_0350: Expected O, but got Unknown
			//IL_0351: Unknown result type (might be due to invalid IL or missing references)
			//IL_035b: Expected O, but got Unknown
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0366: Expected O, but got Unknown
			//IL_0367: Unknown result type (might be due to invalid IL or missing references)
			//IL_0371: Expected O, but got Unknown
			//IL_0372: Unknown result type (might be due to invalid IL or missing references)
			//IL_037c: Expected O, but got Unknown
			//IL_037d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0387: Expected O, but got Unknown
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_0392: Expected O, but got Unknown
			//IL_0399: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a3: Expected O, but got Unknown
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ae: Expected O, but got Unknown
			//IL_03af: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b9: Expected O, but got Unknown
			//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c4: Expected O, but got Unknown
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cf: Expected O, but got Unknown
			//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03da: Expected O, but got Unknown
			//IL_03db: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e5: Expected O, but got Unknown
			//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f0: Expected O, but got Unknown
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fb: Expected O, but got Unknown
			//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0406: Expected O, but got Unknown
			//IL_0407: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Expected O, but got Unknown
			//IL_0412: Unknown result type (might be due to invalid IL or missing references)
			//IL_041c: Expected O, but got Unknown
			//IL_041d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0427: Expected O, but got Unknown
			//IL_0428: Unknown result type (might be due to invalid IL or missing references)
			//IL_0432: Expected O, but got Unknown
			//IL_0433: Unknown result type (might be due to invalid IL or missing references)
			//IL_043d: Expected O, but got Unknown
			//IL_043e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0448: Expected O, but got Unknown
			//IL_0449: Unknown result type (might be due to invalid IL or missing references)
			//IL_0453: Expected O, but got Unknown
			//IL_0454: Unknown result type (might be due to invalid IL or missing references)
			//IL_045e: Expected O, but got Unknown
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0469: Expected O, but got Unknown
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0474: Expected O, but got Unknown
			//IL_0559: Unknown result type (might be due to invalid IL or missing references)
			//IL_0599: Unknown result type (might be due to invalid IL or missing references)
			//IL_059f: Expected O, but got Unknown
			//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0638: Unknown result type (might be due to invalid IL or missing references)
			//IL_063e: Expected O, but got Unknown
			//IL_067d: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0726: Unknown result type (might be due to invalid IL or missing references)
			//IL_072c: Expected O, but got Unknown
			//IL_076b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0794: Unknown result type (might be due to invalid IL or missing references)
			//IL_0810: Unknown result type (might be due to invalid IL or missing references)
			//IL_0816: Expected O, but got Unknown
			//IL_0855: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08da: Expected O, but got Unknown
			//IL_0919: Unknown result type (might be due to invalid IL or missing references)
			//IL_0942: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a97: Expected O, but got Unknown
			//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b75: Expected O, but got Unknown
			//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e5f: Expected O, but got Unknown
			//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fce: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_10fb: Expected O, but got Unknown
			//IL_113a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1163: Unknown result type (might be due to invalid IL or missing references)
			//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11fb: Expected O, but got Unknown
			//IL_123a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1263: Unknown result type (might be due to invalid IL or missing references)
			//IL_1414: Unknown result type (might be due to invalid IL or missing references)
			//IL_141a: Expected O, but got Unknown
			//IL_143f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1468: Unknown result type (might be due to invalid IL or missing references)
			//IL_14ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_14f4: Expected O, but got Unknown
			//IL_1533: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b8: Expected O, but got Unknown
			//IL_15dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1606: Unknown result type (might be due to invalid IL or missing references)
			//IL_166e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1674: Expected O, but got Unknown
			//IL_1699: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_173c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1742: Expected O, but got Unknown
			//IL_1767: Unknown result type (might be due to invalid IL or missing references)
			//IL_1790: Unknown result type (might be due to invalid IL or missing references)
			//IL_185e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1887: Unknown result type (might be due to invalid IL or missing references)
			//IL_1994: Unknown result type (might be due to invalid IL or missing references)
			//IL_19bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a77: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aa0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b83: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c7f: Expected O, but got Unknown
			//IL_1cc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d99: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d9f: Expected O, but got Unknown
			//IL_1de1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fa7: Expected O, but got Unknown
			//IL_1fcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ff5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2054: Unknown result type (might be due to invalid IL or missing references)
			//IL_205a: Expected O, but got Unknown
			//IL_207f: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_217a: Unknown result type (might be due to invalid IL or missing references)
			//IL_21a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_225d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2286: Unknown result type (might be due to invalid IL or missing references)
			//IL_24ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_24f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_25ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_25d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2666: Unknown result type (might be due to invalid IL or missing references)
			//IL_266c: Expected O, but got Unknown
			//IL_26ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_26d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2780: Unknown result type (might be due to invalid IL or missing references)
			//IL_27a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_283a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2840: Expected O, but got Unknown
			//IL_287f: Unknown result type (might be due to invalid IL or missing references)
			//IL_28a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2950: Unknown result type (might be due to invalid IL or missing references)
			//IL_2979: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a32: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2af8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2afe: Expected O, but got Unknown
			//IL_2b3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b66: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c02: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ce4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2def: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2eb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_3101: Unknown result type (might be due to invalid IL or missing references)
			//IL_312a: Unknown result type (might be due to invalid IL or missing references)
			//IL_31cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_31d3: Expected O, but got Unknown
			//IL_3212: Unknown result type (might be due to invalid IL or missing references)
			//IL_323b: Unknown result type (might be due to invalid IL or missing references)
			//IL_32de: Unknown result type (might be due to invalid IL or missing references)
			//IL_32e4: Expected O, but got Unknown
			//IL_3323: Unknown result type (might be due to invalid IL or missing references)
			//IL_334c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3406: Unknown result type (might be due to invalid IL or missing references)
			//IL_342f: Unknown result type (might be due to invalid IL or missing references)
			//IL_34dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_3506: Unknown result type (might be due to invalid IL or missing references)
			//IL_358f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3595: Expected O, but got Unknown
			//IL_35d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_35fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_368d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3693: Expected O, but got Unknown
			//IL_36d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3848: Unknown result type (might be due to invalid IL or missing references)
			//IL_384e: Expected O, but got Unknown
			//IL_3873: Unknown result type (might be due to invalid IL or missing references)
			//IL_389c: Unknown result type (might be due to invalid IL or missing references)
			//IL_392d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3933: Expected O, but got Unknown
			//IL_3958: Unknown result type (might be due to invalid IL or missing references)
			//IL_3981: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a25: Expected O, but got Unknown
			//IL_3a4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a73: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b10: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b16: Expected O, but got Unknown
			//IL_3b55: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c73: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d56: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e39: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e62: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fa6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fcf: Unknown result type (might be due to invalid IL or missing references)
			//IL_4150: Unknown result type (might be due to invalid IL or missing references)
			//IL_4156: Expected O, but got Unknown
			//IL_417b: Unknown result type (might be due to invalid IL or missing references)
			//IL_41a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_426d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4296: Unknown result type (might be due to invalid IL or missing references)
			//IL_440c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4435: Unknown result type (might be due to invalid IL or missing references)
			//IL_44c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_44e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_456b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4594: Unknown result type (might be due to invalid IL or missing references)
			//IL_4622: Unknown result type (might be due to invalid IL or missing references)
			//IL_464b: Unknown result type (might be due to invalid IL or missing references)
			//IL_46ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_4713: Unknown result type (might be due to invalid IL or missing references)
			//IL_47f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_481f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4891: Unknown result type (might be due to invalid IL or missing references)
			//IL_48ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_493c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4965: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoOrdemProducao));
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			Detail = new DetailBand();
			imagem = new XRPictureBox();
			xrLabel43 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrRichText1 = new XRRichText();
			xrLabel24 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel15 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			etapas = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel19 = new XRLabel();
			xrLabel1 = new XRLabel();
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			lblQtdUtilizada = new XRLabel();
			xrRichText2 = new XRRichText();
			lblUnidade = new XRLabel();
			lblQtdNecessaria = new XRLabel();
			xrLabel2 = new XRLabel();
			observacao = new SubBand();
			xrLabel28 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			lblQtd = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			lblTotalQtdNecessaria = new XRLabel();
			lblTotalQtdUtilizada = new XRLabel();
			DetailReport2 = new DetailReportBand();
			Detail5 = new DetailBand();
			xrLabel47 = new XRLabel();
			xrLabel39 = new XRLabel();
			GroupHeader5 = new GroupHeaderBand();
			xrLabel48 = new XRLabel();
			xrLabel40 = new XRLabel();
			esconderColaboradores = new FormattingRule();
			GroupHeader7 = new GroupHeaderBand();
			formattingRule1 = new FormattingRule();
			GroupHeader3 = new GroupHeaderBand();
			xrLabel44 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel23 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel21 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			xrLabel46 = new XRLabel();
			toText = new CalculatedField();
			descricaoMateriaPrima = new CalculatedField();
			DesmontagemReport = new DetailReportBand();
			Detail3 = new DetailBand();
			xrLabel33 = new XRLabel();
			xrLabel34 = new XRLabel();
			xrLabel35 = new XRLabel();
			xrLabel36 = new XRLabel();
			xrLabel37 = new XRLabel();
			xrLabel38 = new XRLabel();
			informacaoEstoqueDesmontada = new XRRichText();
			DetailReport3 = new DetailReportBand();
			Detail4 = new DetailBand();
			xrLabel25 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel27 = new XRLabel();
			informacaoEstoqueMateriaPrimaRetornada = new XRRichText();
			GroupHeader4 = new GroupHeaderBand();
			xrLabel29 = new XRLabel();
			xrLabel31 = new XRLabel();
			xrLabel32 = new XRLabel();
			objectDataSource1 = new ObjectDataSource(components);
			GroupHeader2 = new GroupHeaderBand();
			xrLabel30 = new XRLabel();
			DetailReport4 = new DetailReportBand();
			Detail6 = new DetailBand();
			xrLabel41 = new XRLabel();
			GroupHeader6 = new GroupHeaderBand();
			xrLabel42 = new XRLabel();
			esconderColaboradoresDesmontagem = new FormattingRule();
			PageHeader = new PageHeaderBand();
			Empresa = new XRLabel();
			CNPJ = new XRLabel();
			Fantasia = new XRLabel();
			Titulo = new XRLabel();
			SubTitulo = new XRLabel();
			PageFooter = new PageFooterBand();
			DataEmissao = new XRLabel();
			xrLabel45 = new XRLabel();
			xrPageInfo1 = new XRPageInfo();
			QuantidadeProduto = new CalculatedField();
			((ISupportInitialize)xrRichText1).BeginInit();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)xrRichText2).BeginInit();
			((ISupportInitialize)informacaoEstoqueDesmontada).BeginInit();
			((ISupportInitialize)informacaoEstoqueMateriaPrimaRetornada).BeginInit();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[8]
			{
				(XRControl)imagem,
				(XRControl)xrLabel43,
				(XRControl)xrLabel10,
				(XRControl)xrRichText1,
				(XRControl)xrLabel24,
				(XRControl)xrLabel9,
				(XRControl)xrLabel14,
				(XRControl)xrLabel13
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 251.989f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)imagem).Borders = (BorderSide)0;
			((XRControl)imagem).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Image", (object)null, "Imagem")
			});
			((XRControl)imagem).Dpi = 254f;
			((XRControl)imagem).LocationFloat = new PointFloat(1646f, 0f);
			((XRControl)imagem).Name = "imagem";
			((XRControl)imagem).SizeF = new SizeF(253.9996f, 227.965f);
			imagem.Sizing = (ImageSizeMode)5;
			((XRControl)imagem).StylePriority.UseBorders = false;
			((XRControl)xrLabel43).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Quantidade", "{0:0.####}")
			});
			((XRControl)xrLabel43).Dpi = 254f;
			((XRControl)xrLabel43).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel43).LocationFloat = new PointFloat(1217.375f, 0f);
			((XRControl)xrLabel43).Name = "xrLabel43";
			((XRControl)xrLabel43).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel43).SizeF = new SizeF(203.7291f, 58.42f);
			((XRControl)xrLabel43).StylePriority.UseFont = false;
			((XRControl)xrLabel43).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel43).Text = "xrLabel14";
			((XRControl)xrLabel43).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel10).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.UnidadeMedida")
			});
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1057.563f, 0f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(159.8124f, 58.42f);
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)2;
			((XRControl)xrRichText1).CanShrink = true;
			((XRControl)xrRichText1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Rtf", (object)null, "ProdutoProduzido.ToText")
			});
			((XRControl)xrRichText1).Dpi = 254f;
			((XRControl)xrRichText1).Font = new Font("Segoe UI", 10f);
			((XRControl)xrRichText1).LocationFloat = new PointFloat(44.97917f, 58.41996f);
			((XRControl)xrRichText1).Name = "xrRichText1";
			((XRRichTextBase)xrRichText1).SerializableRtfString = componentResourceManager.GetString("xrRichText1.SerializableRtfString");
			((XRControl)xrRichText1).SizeF = new SizeF(1601.021f, 58.41999f);
			((XRControl)xrRichText1).BeforePrint += xrRichText1_BeforePrint;
			((XRControl)xrLabel24).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.Observacao")
			});
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(235.4794f, 169.545f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(1410.521f, 58.42f);
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "xrLabel24";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)4096;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(0f, 169.545f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(235.4792f, 58.42f);
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Observação:";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.EstoqueSaida.Quantidade", "{0:0.####}")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1421.104f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(224.8959f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel13).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzidoDescricao")
			});
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(1057.563f, 58.42f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "xrLabel13";
			((XRControl)xrLabel12).BackColor = Color.Transparent;
			((XRControl)xrLabel12).Borders = (BorderSide)8;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(0.0004440943f, 0f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(1646f, 58.42001f);
			((XRControl)xrLabel12).StylePriority.UseBackColor = false;
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Etapas de produção";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel20).BackColor = Color.Transparent;
			((XRControl)xrLabel20).Borders = (BorderSide)8;
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1646f, 0f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(254f, 58.42001f);
			((XRControl)xrLabel20).StylePriority.UseBackColor = false;
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).Text = "Tempo";
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel15).Borders = (BorderSide)0;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(336.0208f, 63.5f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(283.1041f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "xrLabel15";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)2;
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
			bindingSource1.DataSource = typeof(OrdemProducaoDto);
			((XtraReportBase)etapas).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail1,
				(Band)DetailReport1,
				(Band)DetailReport2,
				(Band)GroupHeader7
			});
			((XtraReportBase)etapas).DataMember = "Etapas";
			((XRControl)etapas).Dpi = 254f;
			etapas.Level = 0;
			((XRControl)etapas).Name = "etapas";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel19,
				(XRControl)xrLabel1
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 75f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel19).BackColor = Color.Gainsboro;
			((XRControl)xrLabel19).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.DuracaoPrevista")
			});
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(1646f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel19).StylePriority.UseBackColor = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel1).BackColor = Color.Gainsboro;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Descricao")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1646f, 58.42001f);
			((XRControl)xrLabel1).StylePriority.UseBackColor = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail2,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport1).DataMember = "Etapas.Itens";
			((XRControl)DetailReport1).Dpi = 254f;
			DetailReport1.Level = 0;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)lblQtdUtilizada,
				(XRControl)xrRichText2,
				(XRControl)lblUnidade,
				(XRControl)lblQtdNecessaria,
				(XRControl)xrLabel2
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail2).HeightF = 116.84f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)Detail2).StylePriority.UseFont = false;
			((Band)Detail2).SubBands.AddRange((SubBand[])(object)new SubBand[1] { observacao });
			((XRControl)lblQtdUtilizada).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.EstoqueSaida.Quantidade", "{0:0.########}")
			});
			((XRControl)lblQtdUtilizada).Dpi = 254f;
			((XRControl)lblQtdUtilizada).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblQtdUtilizada).Name = "lblQtdUtilizada";
			((XRControl)lblQtdUtilizada).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtdUtilizada).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblQtdUtilizada).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtdUtilizada).TextAlignment = (TextAlignment)1024;
			((XRControl)lblQtdUtilizada).BeforePrint += lblQtdUtilizada_BeforePrint;
			((XRControl)xrRichText2).CanShrink = true;
			((XRControl)xrRichText2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Rtf", (object)null, "Etapas.Itens.ToText")
			});
			((XRControl)xrRichText2).Dpi = 254f;
			((XRControl)xrRichText2).Font = new Font("Segoe UI", 10f);
			((XRControl)xrRichText2).LocationFloat = new PointFloat(71.43774f, 58.42004f);
			((XRControl)xrRichText2).Name = "xrRichText2";
			((XRRichTextBase)xrRichText2).SerializableRtfString = componentResourceManager.GetString("xrRichText2.SerializableRtfString");
			((XRControl)xrRichText2).SizeF = new SizeF(1803.563f, 58.42001f);
			((XRControl)xrRichText2).BeforePrint += xrRichText2_BeforePrint;
			((XRControl)lblUnidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.UnidadeMedida")
			});
			((XRControl)lblUnidade).Dpi = 254f;
			((XRControl)lblUnidade).LocationFloat = new PointFloat(1433.313f, 0f);
			((XRControl)lblUnidade).Name = "lblUnidade";
			((XRControl)lblUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblUnidade).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)lblUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblUnidade).TextAlignment = (TextAlignment)512;
			((XRControl)lblQtdNecessaria).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.QuantidadeNecessaria", "{0:0.########}")
			});
			((XRControl)lblQtdNecessaria).Dpi = 254f;
			((XRControl)lblQtdNecessaria).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblQtdNecessaria).Name = "lblQtdNecessaria";
			((XRControl)lblQtdNecessaria).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtdNecessaria).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblQtdNecessaria).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtdNecessaria).TextAlignment = (TextAlignment)1024;
			((XRControl)lblQtdNecessaria).BeforePrint += lblQtdNecessaria_BeforePrint;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.descricaoMateriaPrima")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1388.334f, 58.42f);
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)256;
			((XRControl)observacao).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel28 });
			((XRControl)observacao).Dpi = 254f;
			((XRControl)observacao).HeightF = 58.41999f;
			((XRControl)observacao).Name = "observacao";
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(95.25034f, 58.41999f);
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).Text = "Obs:";
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)256;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)lblQtd,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 58.42001f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)lblQtd).Borders = (BorderSide)8;
			((XRControl)lblQtd).Dpi = 254f;
			((XRControl)lblQtd).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)lblQtd).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblQtd).Name = "lblQtd";
			((XRControl)lblQtd).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtd).SizeF = new SizeF(299.9999f, 58.42001f);
			((XRControl)lblQtd).StylePriority.UseBorders = false;
			((XRControl)lblQtd).StylePriority.UseFont = false;
			((XRControl)lblQtd).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtd).Text = "Qtd necessária";
			((XRControl)lblQtd).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel8).Borders = (BorderSide)8;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1433.313f, 3.814697E-06f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Unidade";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(1388.334f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Matéria-prima";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)lblTotalQtdNecessaria,
				(XRControl)lblTotalQtdUtilizada
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)lblTotalQtdNecessaria).Borders = (BorderSide)2;
			((XRControl)lblTotalQtdNecessaria).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.QuantidadeProduto")
			});
			((XRControl)lblTotalQtdNecessaria).Dpi = 254f;
			((XRControl)lblTotalQtdNecessaria).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)lblTotalQtdNecessaria).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblTotalQtdNecessaria).Name = "lblTotalQtdNecessaria";
			((XRControl)lblTotalQtdNecessaria).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTotalQtdNecessaria).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseBorders = false;
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseFont = false;
			((XRControl)lblTotalQtdNecessaria).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:0.########}";
			val.Running = (SummaryRunning)1;
			lblTotalQtdNecessaria.Summary = val;
			((XRControl)lblTotalQtdNecessaria).TextAlignment = (TextAlignment)1024;
			((XRControl)lblTotalQtdUtilizada).Borders = (BorderSide)2;
			((XRControl)lblTotalQtdUtilizada).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Itens.EstoqueSaida.Quantidade")
			});
			((XRControl)lblTotalQtdUtilizada).Dpi = 254f;
			((XRControl)lblTotalQtdUtilizada).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
			((XRControl)lblTotalQtdUtilizada).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)lblTotalQtdUtilizada).Name = "lblTotalQtdUtilizada";
			((XRControl)lblTotalQtdUtilizada).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblTotalQtdUtilizada).SizeF = new SizeF(300f, 58.42f);
			((XRControl)lblTotalQtdUtilizada).StylePriority.UseBorders = false;
			((XRControl)lblTotalQtdUtilizada).StylePriority.UseFont = false;
			((XRControl)lblTotalQtdUtilizada).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:0.########}";
			val2.Running = (SummaryRunning)1;
			lblTotalQtdUtilizada.Summary = val2;
			((XRControl)lblTotalQtdUtilizada).TextAlignment = (TextAlignment)1024;
			((XtraReportBase)DetailReport2).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail5,
				(Band)GroupHeader5
			});
			((XtraReportBase)DetailReport2).DataMember = "Etapas.Colaboradores";
			((XRControl)DetailReport2).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)DetailReport2).FormattingRules).Add(esconderColaboradores);
			DetailReport2.Level = 1;
			((XRControl)DetailReport2).Name = "DetailReport2";
			((XRControl)Detail5).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel47,
				(XRControl)xrLabel39
			});
			((XRControl)Detail5).Dpi = 254f;
			((XRControl)Detail5).HeightF = 58.42f;
			Detail5.MultiColumn.Layout = (ColumnLayout)1;
			Detail5.MultiColumn.Mode = (MultiColumnMode)1;
			((XRControl)Detail5).Name = "Detail5";
			((XRControl)xrLabel47).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Colaboradores.Tempo")
			});
			((XRControl)xrLabel47).Dpi = 254f;
			((XRControl)xrLabel47).LocationFloat = new PointFloat(1634.396f, 0f);
			((XRControl)xrLabel47).Name = "xrLabel47";
			((XRControl)xrLabel47).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel47).SizeF = new SizeF(265.6033f, 58.42f);
			((XRControl)xrLabel47).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel47).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel39).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Etapas.Colaboradores.PessoaNome")
			});
			((XRControl)xrLabel39).Dpi = 254f;
			((XRControl)xrLabel39).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel39).Name = "xrLabel39";
			((XRControl)xrLabel39).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel39).SizeF = new SizeF(1589.417f, 58.42f);
			((XRControl)xrLabel39).Text = "xrLabel39";
			((XRControl)GroupHeader5).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel48,
				(XRControl)xrLabel40
			});
			((XRControl)GroupHeader5).Dpi = 254f;
			((XRControl)GroupHeader5).HeightF = 107.715f;
			((XRControl)GroupHeader5).Name = "GroupHeader5";
			((XRControl)xrLabel48).Borders = (BorderSide)8;
			((XRControl)xrLabel48).Dpi = 254f;
			((XRControl)xrLabel48).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel48).LocationFloat = new PointFloat(1634.396f, 49.29496f);
			((XRControl)xrLabel48).Name = "xrLabel48";
			((XRControl)xrLabel48).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel48).SizeF = new SizeF(265.6036f, 58.42f);
			((XRControl)xrLabel48).StylePriority.UseBorders = false;
			((XRControl)xrLabel48).StylePriority.UseFont = false;
			((XRControl)xrLabel48).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel48).Text = "Tempo";
			((XRControl)xrLabel48).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel40).Borders = (BorderSide)8;
			((XRControl)xrLabel40).Dpi = 254f;
			((XRControl)xrLabel40).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel40).LocationFloat = new PointFloat(44.97949f, 49.29496f);
			((XRControl)xrLabel40).Name = "xrLabel40";
			((XRControl)xrLabel40).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel40).SizeF = new SizeF(1589.416f, 58.42f);
			((XRControl)xrLabel40).StylePriority.UseBorders = false;
			((XRControl)xrLabel40).StylePriority.UseFont = false;
			((XRControl)xrLabel40).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel40).Text = "Colaboradores";
			((XRControl)xrLabel40).TextAlignment = (TextAlignment)16;
			esconderColaboradores.Condition = "[DataSource.RowCount] <= 0";
			((DataContainerComponent)esconderColaboradores).DataMember = "Etapas.Colaboradores";
			esconderColaboradores.Formatting.Visible = (DefaultBoolean)1;
			esconderColaboradores.Name = "esconderColaboradores";
			((XRControl)GroupHeader7).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel12,
				(XRControl)xrLabel20
			});
			((XRControl)GroupHeader7).Dpi = 254f;
			((XRControl)GroupHeader7).HeightF = 58.42001f;
			((XRControl)GroupHeader7).Name = "GroupHeader7";
			formattingRule1.Condition = "[DataSource.RowCount] ==  0";
			formattingRule1.Formatting.Visible = (DefaultBoolean)1;
			formattingRule1.Name = "formattingRule1";
			((XRControl)GroupHeader3).Controls.AddRange((XRControl[])(object)new XRControl[13]
			{
				(XRControl)xrLabel44,
				(XRControl)xrLabel6,
				(XRControl)xrLabel23,
				(XRControl)xrLabel11,
				(XRControl)xrLabel4,
				(XRControl)xrLabel5,
				(XRControl)xrLabel22,
				(XRControl)xrLabel21,
				(XRControl)xrLabel18,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16,
				(XRControl)xrLabel15,
				(XRControl)xrLabel46
			});
			((XRControl)GroupHeader3).Dpi = 254f;
			((XRControl)GroupHeader3).HeightF = 267.4409f;
			((XRControl)GroupHeader3).Name = "GroupHeader3";
			((XRControl)xrLabel44).Borders = (BorderSide)8;
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1421.104f, 209.0208f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(224.8959f, 58.42f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).Text = "Produzida";
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel6).Borders = (BorderSide)8;
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1057.563f, 209.0208f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(159.8125f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "Unidade";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel23).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Observacao")
			});
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(254f, 121.92f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(1646f, 58.41999f);
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).Text = "xrLabel23";
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)4096;
			((XRControl)xrLabel11).Borders = (BorderSide)0;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1175.041f, 63.5f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(209.0208f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Situação:";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Situacao.Descricao")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1384.062f, 63.5f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(515.9377f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel5).Borders = (BorderSide)0;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 121.92f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(254f, 58.41999f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Observação:";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel22).Borders = (BorderSide)0;
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(214.3124f, 58.42f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).Text = "Descrição:";
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel21).Borders = (BorderSide)0;
			((XRControl)xrLabel21).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(214.3124f, 0f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(1685.688f, 58.42f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).Text = "xrLabel21";
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(0f, 63.5f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(336.0208f, 58.42f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Data de emissão:";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(1217.375f, 209.0208f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(203.7291f, 58.42f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Prevista";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(0f, 209.0208f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(1057.563f, 58.42f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).Text = "Produto produzido";
			((XRControl)xrLabel46).Borders = (BorderSide)8;
			((XRControl)xrLabel46).Dpi = 254f;
			((XRControl)xrLabel46).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel46).LocationFloat = new PointFloat(1646f, 209.0208f);
			((XRControl)xrLabel46).Name = "xrLabel46";
			((XRControl)xrLabel46).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel46).SizeF = new SizeF(253.9996f, 58.42f);
			((XRControl)xrLabel46).StylePriority.UseBorders = false;
			((XRControl)xrLabel46).StylePriority.UseFont = false;
			((XRControl)xrLabel46).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel46).Text = "Imagem";
			((XRControl)xrLabel46).TextAlignment = (TextAlignment)2;
			((DataContainerComponent)toText).DataMember = "FichaTecnicaDto.EtapasProducao";
			toText.Expression = "Concat(PadLeft([Hora], 2, '0'), ':', PadLeft([Minuto], 2, '0'), ':', PadLeft([Segundo], 2, '0'))";
			toText.Name = "toText";
			((DataContainerComponent)descricaoMateriaPrima).DataMember = "Etapas.Itens";
			descricaoMateriaPrima.Expression = "Iif([PossuiOrdemProducaoVinculada] == False, [ProdutoDescricao] ,[OrdemProducaoVinculadaDescricao])";
			descricaoMateriaPrima.Name = "descricaoMateriaPrima";
			((XtraReportBase)DesmontagemReport).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail3,
				(Band)DetailReport3,
				(Band)GroupHeader2,
				(Band)DetailReport4
			});
			((XtraReportBase)DesmontagemReport).DataSource = objectDataSource1;
			((XRControl)DesmontagemReport).Dpi = 254f;
			((Band)DesmontagemReport).Expanded = false;
			((Collection<FormattingRule>)(object)((XRControl)DesmontagemReport).FormattingRules).Add(formattingRule1);
			DesmontagemReport.Level = 1;
			((XRControl)DesmontagemReport).Name = "DesmontagemReport";
			((XRControl)Detail3).Controls.AddRange((XRControl[])(object)new XRControl[7]
			{
				(XRControl)xrLabel33,
				(XRControl)xrLabel34,
				(XRControl)xrLabel35,
				(XRControl)xrLabel36,
				(XRControl)xrLabel37,
				(XRControl)xrLabel38,
				(XRControl)informacaoEstoqueDesmontada
			});
			((XRControl)Detail3).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)Detail3).FormattingRules).Add(formattingRule1);
			((XRControl)Detail3).HeightF = 127f;
			((XRControl)Detail3).Name = "Detail3";
			((XRControl)xrLabel33).Borders = (BorderSide)0;
			((XRControl)xrLabel33).Dpi = 254f;
			((XRControl)xrLabel33).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel33).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel33).Name = "xrLabel33";
			((XRControl)xrLabel33).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel33).SizeF = new SizeF(124.3541f, 58.42f);
			((XRControl)xrLabel33).StylePriority.UseBorders = false;
			((XRControl)xrLabel33).StylePriority.UseFont = false;
			((XRControl)xrLabel33).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel33).Text = "Início:";
			((XRControl)xrLabel33).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel34).Borders = (BorderSide)0;
			((XRControl)xrLabel34).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraInicio", "{0:dd/MM/yyyy HH:mm:ss}")
			});
			((XRControl)xrLabel34).Dpi = 254f;
			((XRControl)xrLabel34).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel34).LocationFloat = new PointFloat(124.354f, 0f);
			((XRControl)xrLabel34).Name = "xrLabel34";
			((XRControl)xrLabel34).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel34).SizeF = new SizeF(425.9791f, 58.42f);
			((XRControl)xrLabel34).StylePriority.UseBorders = false;
			((XRControl)xrLabel34).StylePriority.UseFont = false;
			((XRControl)xrLabel34).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel34).Text = "xrLabel2";
			((XRControl)xrLabel34).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel35).Borders = (BorderSide)0;
			((XRControl)xrLabel35).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraFim", "{0:dd/MM/yyyy HH:mm:ss}")
			});
			((XRControl)xrLabel35).Dpi = 254f;
			((XRControl)xrLabel35).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel35).LocationFloat = new PointFloat(650.8752f, 0f);
			((XRControl)xrLabel35).Name = "xrLabel35";
			((XRControl)xrLabel35).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel35).SizeF = new SizeF(468.6039f, 58.42f);
			((XRControl)xrLabel35).StylePriority.UseBorders = false;
			((XRControl)xrLabel35).StylePriority.UseFont = false;
			((XRControl)xrLabel35).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel35).Text = "xrLabel3";
			((XRControl)xrLabel35).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel36).Borders = (BorderSide)0;
			((XRControl)xrLabel36).Dpi = 254f;
			((XRControl)xrLabel36).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel36).LocationFloat = new PointFloat(550.3332f, 0f);
			((XRControl)xrLabel36).Name = "xrLabel36";
			((XRControl)xrLabel36).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel36).SizeF = new SizeF(100.5417f, 58.41999f);
			((XRControl)xrLabel36).StylePriority.UseBorders = false;
			((XRControl)xrLabel36).StylePriority.UseFont = false;
			((XRControl)xrLabel36).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel36).Text = "Fim:";
			((XRControl)xrLabel36).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel37).Dpi = 254f;
			((XRControl)xrLabel37).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel37).LocationFloat = new PointFloat(1119.479f, 0f);
			((XRControl)xrLabel37).Name = "xrLabel37";
			((XRControl)xrLabel37).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel37).SizeF = new SizeF(383.646f, 58.42f);
			((XRControl)xrLabel37).StylePriority.UseFont = false;
			((XRControl)xrLabel37).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel37).Text = "Qtd desmontada:";
			((XRControl)xrLabel37).TextAlignment = (TextAlignment)1024;
			((XRControl)xrLabel38).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.QuantidadeDesmontada", "{0:n}")
			});
			((XRControl)xrLabel38).Dpi = 254f;
			((XRControl)xrLabel38).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel38).LocationFloat = new PointFloat(1503.125f, 0f);
			((XRControl)xrLabel38).Name = "xrLabel38";
			((XRControl)xrLabel38).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel38).SizeF = new SizeF(396.8754f, 58.42f);
			((XRControl)xrLabel38).StylePriority.UseFont = false;
			((XRControl)xrLabel38).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel38).Text = "xrLabel12";
			((XRControl)xrLabel38).TextAlignment = (TextAlignment)256;
			((XRControl)informacaoEstoqueDesmontada).CanShrink = true;
			((XRControl)informacaoEstoqueDesmontada).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Rtf", (object)null, "ProdutoProduzido.InformacaoEstoqueDesmontada")
			});
			((XRControl)informacaoEstoqueDesmontada).Dpi = 254f;
			((XRControl)informacaoEstoqueDesmontada).Font = new Font("Segoe UI", 10f);
			((XRControl)informacaoEstoqueDesmontada).LocationFloat = new PointFloat(44.97917f, 58.42004f);
			((XRControl)informacaoEstoqueDesmontada).Name = "informacaoEstoqueDesmontada";
			((XRRichTextBase)informacaoEstoqueDesmontada).SerializableRtfString = componentResourceManager.GetString("informacaoEstoqueDesmontada.SerializableRtfString");
			((XRControl)informacaoEstoqueDesmontada).SizeF = new SizeF(939.4167f, 58f);
			((XRControl)informacaoEstoqueDesmontada).BeforePrint += informacaoEstoqueDesmontada_BeforePrint;
			((XtraReportBase)DetailReport3).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail4,
				(Band)GroupHeader4
			});
			((XtraReportBase)DetailReport3).DataMember = "Itens";
			((XtraReportBase)DetailReport3).DataSource = objectDataSource1;
			((XRControl)DetailReport3).Dpi = 254f;
			DetailReport3.Level = 0;
			((XRControl)DetailReport3).Name = "DetailReport3";
			((XRControl)Detail4).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel25,
				(XRControl)xrLabel26,
				(XRControl)xrLabel27,
				(XRControl)informacaoEstoqueMateriaPrimaRetornada
			});
			((XRControl)Detail4).Dpi = 254f;
			((XRControl)Detail4).HeightF = 116.4201f;
			((XRControl)Detail4).Name = "Detail4";
			((XRControl)xrLabel25).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel25).Borders = (BorderSide)0;
			((XRControl)xrLabel25).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.ProdutoDescricao")
			});
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(1388.334f, 58.42f);
			((XRControl)xrLabel25).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).Text = "xrLabel14";
			((XRControl)xrLabel26).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel26).Borders = (BorderSide)0;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.UnidadeMedida", "{0:n}")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).LocationFloat = new PointFloat(1433.313f, 0f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(166.6873f, 58.42f);
			((XRControl)xrLabel26).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel27).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrLabel27).Borders = (BorderSide)0;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.QuantidadeDesmontada", "{0:n}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(299.9996f, 58.42f);
			((XRControl)xrLabel27).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).Text = "xrLabel17";
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)4;
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).CanShrink = true;
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Rtf", (object)null, "Itens.InformacaoEstoqueDesmontada")
			});
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).Dpi = 254f;
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).Font = new Font("Segoe UI", 10f);
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).LocationFloat = new PointFloat(82.31264f, 58.42004f);
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).Name = "informacaoEstoqueMateriaPrimaRetornada";
			((XRRichTextBase)informacaoEstoqueMateriaPrimaRetornada).SerializableRtfString = componentResourceManager.GetString("informacaoEstoqueMateriaPrimaRetornada.SerializableRtfString");
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).SizeF = new SizeF(1351f, 58.00002f);
			((XRControl)informacaoEstoqueMateriaPrimaRetornada).BeforePrint += informacaoEstoqueMateriaPrimaRetornada_BeforePrint;
			((XRControl)GroupHeader4).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel29,
				(XRControl)xrLabel31,
				(XRControl)xrLabel32
			});
			((XRControl)GroupHeader4).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)GroupHeader4).FormattingRules).Add(formattingRule1);
			((XRControl)GroupHeader4).HeightF = 58.42001f;
			((XRControl)GroupHeader4).Name = "GroupHeader4";
			((XRControl)xrLabel29).Borders = (BorderSide)8;
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(1388.334f, 58.42001f);
			((XRControl)xrLabel29).StylePriority.UseBorders = false;
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel29).Text = "Matéria-prima";
			((XRControl)xrLabel29).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel31).Borders = (BorderSide)8;
			((XRControl)xrLabel31).Dpi = 254f;
			((XRControl)xrLabel31).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel31).LocationFloat = new PointFloat(1433.313f, 0f);
			((XRControl)xrLabel31).Name = "xrLabel31";
			((XRControl)xrLabel31).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel31).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel31).StylePriority.UseBorders = false;
			((XRControl)xrLabel31).StylePriority.UseFont = false;
			((XRControl)xrLabel31).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel31).Text = "Unidade";
			((XRControl)xrLabel31).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel32).Borders = (BorderSide)8;
			((XRControl)xrLabel32).Dpi = 254f;
			((XRControl)xrLabel32).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel32).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)xrLabel32).Name = "xrLabel32";
			((XRControl)xrLabel32).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel32).SizeF = new SizeF(299.9999f, 58.42001f);
			((XRControl)xrLabel32).StylePriority.UseBorders = false;
			((XRControl)xrLabel32).StylePriority.UseFont = false;
			((XRControl)xrLabel32).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel32).Text = "Qtd retornada";
			((XRControl)xrLabel32).TextAlignment = (TextAlignment)64;
			objectDataSource1.DataSource = typeof(DesmontagemRelatorioDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel30 });
			((XRControl)GroupHeader2).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)GroupHeader2).FormattingRules).Add(formattingRule1);
			((XRControl)GroupHeader2).HeightF = 97.85426f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)xrLabel30).Borders = (BorderSide)8;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(0f, 39.43425f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(1900f, 58.41999f);
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).Text = "Desmontagem";
			((XtraReportBase)DetailReport4).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail6,
				(Band)GroupHeader6
			});
			((XtraReportBase)DetailReport4).DataMember = "Colaboradores";
			((XtraReportBase)DetailReport4).DataSource = objectDataSource1;
			((XRControl)DetailReport4).Dpi = 254f;
			((Collection<FormattingRule>)(object)((XRControl)DetailReport4).FormattingRules).Add(esconderColaboradoresDesmontagem);
			DetailReport4.Level = 1;
			((XRControl)DetailReport4).Name = "DetailReport4";
			((XRControl)Detail6).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel41 });
			((XRControl)Detail6).Dpi = 254f;
			((XRControl)Detail6).HeightF = 58.42f;
			Detail6.MultiColumn.ColumnCount = 2;
			Detail6.MultiColumn.Layout = (ColumnLayout)1;
			Detail6.MultiColumn.Mode = (MultiColumnMode)1;
			((XRControl)Detail6).Name = "Detail6";
			((XRControl)xrLabel41).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Colaboradores.PessoaNome")
			});
			((XRControl)xrLabel41).Dpi = 254f;
			((XRControl)xrLabel41).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel41).Name = "xrLabel41";
			((XRControl)xrLabel41).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel41).SizeF = new SizeF(873.4167f, 58.42f);
			((XRControl)xrLabel41).Text = "xrLabel41";
			((XRControl)GroupHeader6).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel42 });
			((XRControl)GroupHeader6).Dpi = 254f;
			((XRControl)GroupHeader6).HeightF = 83.41994f;
			((XRControl)GroupHeader6).Name = "GroupHeader6";
			((XRControl)xrLabel42).Borders = (BorderSide)8;
			((XRControl)xrLabel42).Dpi = 254f;
			((XRControl)xrLabel42).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel42).LocationFloat = new PointFloat(44.97937f, 24.99993f);
			((XRControl)xrLabel42).Name = "xrLabel42";
			((XRControl)xrLabel42).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel42).SizeF = new SizeF(1855.021f, 58.42001f);
			((XRControl)xrLabel42).StylePriority.UseBorders = false;
			((XRControl)xrLabel42).StylePriority.UseFont = false;
			((XRControl)xrLabel42).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel42).Text = "Colaboradores";
			((XRControl)xrLabel42).TextAlignment = (TextAlignment)16;
			esconderColaboradoresDesmontagem.Condition = "[DataSource.RowCount] <= 0";
			((DataContainerComponent)esconderColaboradoresDesmontagem).DataMember = "Colaboradores";
			((DataContainerComponent)esconderColaboradoresDesmontagem).DataSource = objectDataSource1;
			esconderColaboradoresDesmontagem.Formatting.Visible = (DefaultBoolean)1;
			esconderColaboradoresDesmontagem.Name = "esconderColaboradoresDesmontagem";
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)Empresa,
				(XRControl)CNPJ,
				(XRControl)Fantasia,
				(XRControl)Titulo,
				(XRControl)SubTitulo
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).HeightF = 365.125f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)Empresa).Dpi = 254f;
			((XRControl)Empresa).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Empresa).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)Empresa).Name = "Empresa";
			((XRControl)Empresa).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Empresa).SizeF = new SizeF(1900f, 58.42f);
			((XRControl)Empresa).StylePriority.UseFont = false;
			((XRControl)Empresa).Text = "Empresa";
			((XRControl)Empresa).WordWrap = false;
			((XRControl)CNPJ).Dpi = 254f;
			((XRControl)CNPJ).Font = new Font("Segoe UI", 12f);
			((XRControl)CNPJ).LocationFloat = new PointFloat(0f, 58.42f);
			((XRControl)CNPJ).Name = "CNPJ";
			((XRControl)CNPJ).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)CNPJ).SizeF = new SizeF(423.625f, 58.41998f);
			((XRControl)CNPJ).StylePriority.UseFont = false;
			((XRControl)CNPJ).Text = "CNPJ";
			((XRControl)Fantasia).Dpi = 254f;
			((XRControl)Fantasia).Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Fantasia).LocationFloat = new PointFloat(0f, 116.84f);
			((XRControl)Fantasia).Name = "Fantasia";
			((XRControl)Fantasia).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Fantasia).SizeF = new SizeF(1900f, 58.42f);
			((XRControl)Fantasia).StylePriority.UseFont = false;
			((XRControl)Fantasia).Text = "Fantasia";
			((XRControl)Fantasia).WordWrap = false;
			((XRControl)Titulo).Dpi = 254f;
			((XRControl)Titulo).Font = new Font("Segoe UI", 19f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Titulo).LocationFloat = new PointFloat(0f, 175.26f);
			((XRControl)Titulo).Name = "Titulo";
			((XRControl)Titulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)Titulo).SizeF = new SizeF(1900f, 99.99998f);
			((XRControl)Titulo).StylePriority.UseFont = false;
			((XRControl)Titulo).StylePriority.UseTextAlignment = false;
			((XRControl)Titulo).Text = "Titulo";
			((XRControl)Titulo).TextAlignment = (TextAlignment)2;
			((XRControl)SubTitulo).Dpi = 254f;
			((XRControl)SubTitulo).Font = new Font("Segoe UI", 14f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)SubTitulo).LocationFloat = new PointFloat(0f, 275.2599f);
			((XRControl)SubTitulo).Name = "SubTitulo";
			((XRControl)SubTitulo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)SubTitulo).SizeF = new SizeF(1900f, 58.42001f);
			((XRControl)SubTitulo).StylePriority.UseFont = false;
			((XRControl)SubTitulo).StylePriority.UseTextAlignment = false;
			((XRControl)SubTitulo).Text = "SubTitulo";
			((XRControl)SubTitulo).TextAlignment = (TextAlignment)2;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)DataEmissao,
				(XRControl)xrLabel45,
				(XRControl)xrPageInfo1
			});
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 116.84f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)DataEmissao).Dpi = 254f;
			((XRControl)DataEmissao).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)DataEmissao).Name = "DataEmissao";
			((XRControl)DataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)DataEmissao).SizeF = new SizeF(1900f, 58.42f);
			((XRControl)DataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)DataEmissao).Text = "DataEmissao";
			((XRControl)DataEmissao).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel45).Dpi = 254f;
			((XRControl)xrLabel45).LocationFloat = new PointFloat(380.0004f, 58.41996f);
			((XRControl)xrLabel45).Name = "xrLabel45";
			((XRControl)xrLabel45).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel45).SizeF = new SizeF(1140f, 58.42f);
			((XRControl)xrLabel45).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel45).Text = "Desenvolvido por DigiSat Tecnologia LTDA®  www.digisat.com.br";
			((XRControl)xrLabel45).TextAlignment = (TextAlignment)32;
			((XRControl)xrPageInfo1).Dpi = 254f;
			xrPageInfo1.Format = " Página {0} de {1}";
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(1520f, 58.41996f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrPageInfo1).SizeF = new SizeF(380f, 58.42f);
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)64;
			((DataContainerComponent)QuantidadeProduto).DataMember = "Etapas.Itens";
			QuantidadeProduto.Expression = "Iif([UnidadeMedida] == 'H', 0, [QuantidadeNecessaria])";
			QuantidadeProduto.Name = "QuantidadeProduto";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[8]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)etapas,
				(Band)GroupHeader3,
				(Band)DesmontagemReport,
				(Band)PageHeader,
				(Band)PageFooter
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[3] { toText, descricaoMateriaPrima, QuantidadeProduto });
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).FormattingRuleSheet.AddRange((FormattingRule[])(object)new FormattingRule[3] { formattingRule1, esconderColaboradores, esconderColaboradoresDesmontagem });
			((XtraReport)this).Margins = new Margins(100, 100, 50, 120);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)xrRichText1).EndInit();
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)xrRichText2).EndInit();
			((ISupportInitialize)informacaoEstoqueDesmontada).EndInit();
			((ISupportInitialize)informacaoEstoqueMateriaPrimaRetornada).EndInit();
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ListaMateriasPrimasFalta : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly IEnumerable<MateriaPrimaDto> _materiasPrimas;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRLabel xrLabel13;

		private XRLabel xrLabel14;

		private XRLabel xrLabel15;

		private XRLabel xrLabel20;

		private XRLabel xrLabel22;

		private GroupHeaderBand GroupHeader3;

		private XRLabel xrLabel16;

		private XRLabel xrLabel17;

		private XRLabel xrLabel18;

		private XRLabel xrLabel19;

		private XRLabel xrLabel21;

		public string TituloRelatorio => "Matérias-primas em falta";

		public string SubTituloRelatorio => string.Empty;

		public bool Retrato => true;

		public ListaMateriasPrimasFalta(IEnumerable<MateriaPrimaDto> materiasPrimas)
		{
			_materiasPrimas = materiasPrimas;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XtraReportBase)this).DataSource = _materiasPrimas;
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
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0183: Expected O, but got Unknown
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_021e: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d6: Expected O, but got Unknown
			//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0388: Unknown result type (might be due to invalid IL or missing references)
			//IL_038e: Expected O, but got Unknown
			//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0441: Expected O, but got Unknown
			//IL_0466: Unknown result type (might be due to invalid IL or missing references)
			//IL_048f: Unknown result type (might be due to invalid IL or missing references)
			//IL_050f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0564: Unknown result type (might be due to invalid IL or missing references)
			//IL_0648: Unknown result type (might be due to invalid IL or missing references)
			//IL_0671: Unknown result type (might be due to invalid IL or missing references)
			//IL_072b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0754: Unknown result type (might be due to invalid IL or missing references)
			//IL_080e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0837: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_091a: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09fd: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel13 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel22 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			GroupHeader3 = new GroupHeaderBand();
			xrLabel16 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel21 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel13,
				(XRControl)xrLabel14,
				(XRControl)xrLabel15,
				(XRControl)xrLabel20,
				(XRControl)xrLabel22
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 70.00001f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel13).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(810.8231f, 58.42f);
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "QuantidadeDisponivel", "{0:0.########}")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).LocationFloat = new PointFloat(977.5104f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "QuantidadeFalta", "{0:0.########}")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1577.51f, 0f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(322.4895f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel20).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "QuantidadeNecessaria", "{0:0.########}")
			});
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1277.51f, 0f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "UnidadeMedida")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).LocationFloat = new PointFloat(810.8231f, 0f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)2;
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
			bindingSource1.DataSource = typeof(MateriaPrimaDto);
			((XRControl)GroupHeader3).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel16,
				(XRControl)xrLabel17,
				(XRControl)xrLabel18,
				(XRControl)xrLabel19,
				(XRControl)xrLabel21
			});
			((XRControl)GroupHeader3).Dpi = 254f;
			((XRControl)GroupHeader3).HeightF = 58.42001f;
			((XRControl)GroupHeader3).Name = "GroupHeader3";
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(810.8231f, 58.42001f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel16).Text = "Matéria-prima";
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(810.8231f, 3.814697E-06f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(166.6873f, 58.42001f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Unidade";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel18).Borders = (BorderSide)8;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(977.5104f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(300f, 58.42001f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Qtd disponível";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel19).Borders = (BorderSide)8;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(1277.51f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(299.9999f, 58.42001f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).Text = "Qtd necessária";
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel21).Borders = (BorderSide)8;
			((XRControl)xrLabel21).Dpi = 254f;
			((XRControl)xrLabel21).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel21).LocationFloat = new PointFloat(1577.51f, 3.814697E-06f);
			((XRControl)xrLabel21).Name = "xrLabel21";
			((XRControl)xrLabel21).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel21).SizeF = new SizeF(322.4895f, 58.42f);
			((XRControl)xrLabel21).StylePriority.UseBorders = false;
			((XRControl)xrLabel21).StylePriority.UseFont = false;
			((XRControl)xrLabel21).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel21).Text = "Qtd falta";
			((XRControl)xrLabel21).TextAlignment = (TextAlignment)64;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader3
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ResumoMateriaPrima : XtraReport
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private XRLabel lblQuantidade;

		private XRLabel xrLabel4;

		private GroupHeaderBand GroupHeader2;

		private XRLabel xrLabel2;

		private XRLabel lblQtdeTotal2;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel1;

		private ReportHeaderBand ReportHeader;

		private XRLabel xrLabel5;

		private CalculatedField QuantidadeProduto;

		private XRLabel lblUnidade;

		private XRLabel xrLabel3;

		public ResumoMateriaPrima()
		{
			InitializeComponent();
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
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Expected O, but got Unknown
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Expected O, but got Unknown
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0263: Unknown result type (might be due to invalid IL or missing references)
			//IL_0269: Expected O, but got Unknown
			//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b0: Expected O, but got Unknown
			//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0418: Unknown result type (might be due to invalid IL or missing references)
			//IL_0502: Unknown result type (might be due to invalid IL or missing references)
			//IL_0508: Expected O, but got Unknown
			//IL_0574: Unknown result type (might be due to invalid IL or missing references)
			//IL_057a: Expected O, but got Unknown
			//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0783: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e6: Expected O, but got Unknown
			//IL_0925: Unknown result type (might be due to invalid IL or missing references)
			//IL_094e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b94: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			lblQuantidade = new XRLabel();
			xrLabel4 = new XRLabel();
			GroupHeader2 = new GroupHeaderBand();
			lblUnidade = new XRLabel();
			xrLabel2 = new XRLabel();
			lblQtdeTotal2 = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel1 = new XRLabel();
			ReportHeader = new ReportHeaderBand();
			xrLabel3 = new XRLabel();
			xrLabel5 = new XRLabel();
			QuantidadeProduto = new CalculatedField();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 11f);
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
			((XRControl)BottomMargin).HeightF = 26f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(ItemEstoqueOrdemProducaoDto);
			((XRControl)lblQuantidade).BorderColor = Color.Black;
			((XRControl)lblQuantidade).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)lblQuantidade).Borders = (BorderSide)8;
			((XRControl)lblQuantidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "QuantidadeNecessaria")
			});
			((XRControl)lblQuantidade).Dpi = 254f;
			((XRControl)lblQuantidade).Font = new Font("Segoe UI", 11f);
			((XRControl)lblQuantidade).LocationFloat = new PointFloat(1476.667f, 0f);
			((XRControl)lblQuantidade).Name = "lblQuantidade";
			((XRControl)lblQuantidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQuantidade).SizeF = new SizeF(423.3331f, 58.42f);
			((XRControl)lblQuantidade).StylePriority.UseBorderColor = false;
			((XRControl)lblQuantidade).StylePriority.UseBorderDashStyle = false;
			((XRControl)lblQuantidade).StylePriority.UseBorders = false;
			((XRControl)lblQuantidade).StylePriority.UseFont = false;
			((XRControl)lblQuantidade).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:0.00######}";
			val.Running = (SummaryRunning)1;
			lblQuantidade.Summary = val;
			((XRControl)lblQuantidade).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel4).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)xrLabel4).Borders = (BorderSide)8;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoDescricao")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(1309.98f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader2).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)lblUnidade,
				(XRControl)xrLabel4,
				(XRControl)lblQuantidade
			});
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).Font = new Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
			GroupHeader2.GroupFields.AddRange((GroupField[])(object)new GroupField[1]
			{
				new GroupField("ProdutoReferencia", (XRColumnSortOrder)1)
			});
			((XRControl)GroupHeader2).HeightF = 58.42f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((XRControl)GroupHeader2).StylePriority.UseFont = false;
			((XRControl)lblUnidade).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)lblUnidade).Borders = (BorderSide)8;
			((XRControl)lblUnidade).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "UnidadeMedida")
			});
			((XRControl)lblUnidade).Dpi = 254f;
			((XRControl)lblUnidade).Font = new Font("Segoe UI", 11f);
			((XRControl)lblUnidade).LocationFloat = new PointFloat(1309.98f, 0f);
			((XRControl)lblUnidade).Name = "lblUnidade";
			((XRControl)lblUnidade).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblUnidade).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)lblUnidade).StylePriority.UseBorderDashStyle = false;
			((XRControl)lblUnidade).StylePriority.UseBorders = false;
			((XRControl)lblUnidade).StylePriority.UseFont = false;
			((XRControl)lblUnidade).StylePriority.UseTextAlignment = false;
			((XRControl)lblUnidade).TextAlignment = (TextAlignment)512;
			((XRControl)xrLabel2).Borders = (BorderSide)8;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(1476.667f, 58.42001f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(423.3331f, 58.42001f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Quantidade necessária";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)64;
			((XRControl)lblQtdeTotal2).Borders = (BorderSide)8;
			((XRControl)lblQtdeTotal2).Dpi = 254f;
			((XRControl)lblQtdeTotal2).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)lblQtdeTotal2).LocationFloat = new PointFloat(0f, 58.42001f);
			((XRControl)lblQtdeTotal2).Name = "lblQtdeTotal2";
			((XRControl)lblQtdeTotal2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblQtdeTotal2).SizeF = new SizeF(1309.98f, 58.42001f);
			((XRControl)lblQtdeTotal2).StylePriority.UseBorders = false;
			((XRControl)lblQtdeTotal2).StylePriority.UseFont = false;
			((XRControl)lblQtdeTotal2).StylePriority.UseTextAlignment = false;
			((XRControl)lblQtdeTotal2).Text = "Matéria-prima";
			((XRControl)lblQtdeTotal2).TextAlignment = (TextAlignment)16;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel1 });
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).Font = new Font("Segoe UI Semibold", 9.75f);
			((XRControl)ReportFooter).HeightF = 70.59081f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)ReportFooter).StylePriority.UseFont = false;
			((XRControl)xrLabel1).BorderColor = Color.Black;
			((XRControl)xrLabel1).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "QuantidadeProduto")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 14f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1476.667f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(423.3331f, 70.59081f);
			((XRControl)xrLabel1).StylePriority.UseBorderColor = false;
			((XRControl)xrLabel1).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:0.00######}";
			val2.Running = (SummaryRunning)2;
			xrLabel1.Summary = val2;
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)4;
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel3,
				(XRControl)xrLabel5,
				(XRControl)xrLabel2,
				(XRControl)lblQtdeTotal2
			});
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 116.84f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)xrLabel3).Borders = (BorderSide)8;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1309.98f, 58.42001f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(166.6873f, 58.42001f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "Unidade";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel5).BackColor = Color.Silver;
			((XRControl)xrLabel5).Borders = (BorderSide)8;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(1900f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseBackColor = false;
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "RESUMO POR MATÉRIA-PRIMA";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)32;
			QuantidadeProduto.Expression = "Iif([UnidadeMedida] == 'H', 0, [QuantidadeNecessaria])";
			QuantidadeProduto.Name = "QuantidadeProduto";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)GroupHeader2,
				(Band)ReportFooter,
				(Band)ReportHeader
			});
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[1] { QuantidadeProduto });
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(0, 0, 0, 26);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class MateriasPrimasFalta : XtraReport, IViewModelItemImprimirRelatorio, IViewModelImprimir
	{
		private readonly OrdemProducaoDto _dto;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel xrLabel13;

		private BindingSource bindingSource1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRLabel xrLabel1;

		private DetailReportBand DetailReport1;

		private DetailBand Detail2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		private XRLabel xrLabel4;

		private XRLabel xrLabel2;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel11;

		private XRLabel xrLabel10;

		private XRLabel xrLabel9;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel12;

		private GroupHeaderBand GroupHeader3;

		private XRLabel xrLabel18;

		private XRLabel xrLabel17;

		private XRLabel xrLabel16;

		private XRLabel xrLabel20;

		private XRLabel xrLabel19;

		public string TituloRelatorio => "Matérias-primas em falta";

		public string SubTituloRelatorio => $"Ordem de produção número: {_dto.Numero}";

		public bool Retrato => true;

		public MateriasPrimasFalta(OrdemProducaoDto dto)
		{
			_dto = dto;
			InitializeComponent();
		}

		public async Task<XtraReport> ObterRelatorio()
		{
			return (XtraReport)(object)(await Task.Factory.StartNew(delegate
			{
				((XtraReportBase)this).DataSource = new List<OrdemProducaoDto> { _dto };
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
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021f: Expected O, but got Unknown
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0287: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0302: Expected O, but got Unknown
			//IL_0341: Unknown result type (might be due to invalid IL or missing references)
			//IL_036a: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f5: Expected O, but got Unknown
			//IL_0434: Unknown result type (might be due to invalid IL or missing references)
			//IL_045d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e3: Expected O, but got Unknown
			//IL_0522: Unknown result type (might be due to invalid IL or missing references)
			//IL_054b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0624: Unknown result type (might be due to invalid IL or missing references)
			//IL_0739: Unknown result type (might be due to invalid IL or missing references)
			//IL_073f: Expected O, but got Unknown
			//IL_077e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0854: Unknown result type (might be due to invalid IL or missing references)
			//IL_087d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a27: Expected O, but got Unknown
			//IL_0a4c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aef: Expected O, but got Unknown
			//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb7: Expected O, but got Unknown
			//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c05: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c7f: Expected O, but got Unknown
			//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d42: Expected O, but got Unknown
			//IL_0d67: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ea6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f60: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f89: Unknown result type (might be due to invalid IL or missing references)
			//IL_1043: Unknown result type (might be due to invalid IL or missing references)
			//IL_106c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1126: Unknown result type (might be due to invalid IL or missing references)
			//IL_114f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1209: Unknown result type (might be due to invalid IL or missing references)
			//IL_1232: Unknown result type (might be due to invalid IL or missing references)
			//IL_1356: Unknown result type (might be due to invalid IL or missing references)
			//IL_137f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1438: Unknown result type (might be due to invalid IL or missing references)
			//IL_1461: Unknown result type (might be due to invalid IL or missing references)
			//IL_151a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1543: Unknown result type (might be due to invalid IL or missing references)
			//IL_15fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1625: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel20 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel13 = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel1 = new XRLabel();
			xrLabel12 = new XRLabel();
			DetailReport1 = new DetailReportBand();
			Detail2 = new DetailBand();
			xrLabel3 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel4 = new XRLabel();
			xrLabel2 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel11 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			GroupHeader3 = new GroupHeaderBand();
			xrLabel19 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel16 = new XRLabel();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel20,
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)xrLabel13
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 99.10417f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel20).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzido.UnidadeMedida")
			});
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1204.146f, 0f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(158.7501f, 58.42f);
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel15).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataHoraEmissao", "{0:dd/MM/yyyy}")
			});
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1616.896f, 0f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(283.1041f, 58.42f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "xrLabel15";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Quantidade", "{0:0.####}")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1362.896f, 0f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "xrLabel14";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel13).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ProdutoProduzidoDescricao")
			});
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 11f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(1204.146f, 58.42f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "xrLabel13";
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
			bindingSource1.DataSource = typeof(OrdemProducaoDto);
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)DetailReport1
			});
			((XtraReportBase)DetailReport).DataMember = "FichaTecnicaDto.EtapasProducao";
			((XRControl)DetailReport).Dpi = 254f;
			((XtraReportBase)DetailReport).FilterString = "[MateriasPrimas][[QuantidadeFalta] > 0.0m]";
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[2]
			{
				(XRControl)xrLabel1,
				(XRControl)xrLabel12
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 75f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.Descricao")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(360.1249f, 0f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(1539.875f, 58.42001f);
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "xrLabel1";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel12).BackColor = Color.Transparent;
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(360.1249f, 58.42001f);
			((XRControl)xrLabel12).StylePriority.UseBackColor = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel12).Text = "Etapa de produção:";
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)16;
			((XtraReportBase)DetailReport1).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail2,
				(Band)GroupHeader1
			});
			((XtraReportBase)DetailReport1).DataMember = "FichaTecnicaDto.EtapasProducao.MateriasPrimas";
			((XRControl)DetailReport1).Dpi = 254f;
			((XtraReportBase)DetailReport1).FilterString = "[QuantidadeFalta] > 0.0m";
			DetailReport1.Level = 0;
			((XRControl)DetailReport1).Name = "DetailReport1";
			((XRControl)Detail2).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel3,
				(XRControl)xrLabel6,
				(XRControl)xrLabel5,
				(XRControl)xrLabel4,
				(XRControl)xrLabel2
			});
			((XRControl)Detail2).Dpi = 254f;
			((XRControl)Detail2).Font = new Font("Segoe UI", 10f);
			((XRControl)Detail2).HeightF = 58.42f;
			((XRControl)Detail2).Name = "Detail2";
			((XRControl)Detail2).StylePriority.UseFont = false;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.MateriasPrimas.UnidadeMedida")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(833.3127f, 0f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(166.6873f, 58.41998f);
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "xrLabel3";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.MateriasPrimas.QuantidadeNecessaria", "{0:0.########}")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1300f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "xrLabel6";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.MateriasPrimas.QuantidadeFalta", "{0:0.########}")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1600f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "xrLabel5";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.MateriasPrimas.QuantidadeDisponivel", "{0:0.########}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1000f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "FichaTecnicaDto.EtapasProducao.MateriasPrimas.Descricao")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(788.3335f, 58.42f);
			((XRControl)xrLabel2).Text = "xrLabel2";
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel11,
				(XRControl)xrLabel10,
				(XRControl)xrLabel9,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 58.42001f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel11).Borders = (BorderSide)8;
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1600f, 3.814697E-06f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(300f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel11).Text = "Qtd falta";
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel10).Borders = (BorderSide)8;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1300f, 0f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(299.9999f, 58.42001f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Qtd necessária";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel9).Borders = (BorderSide)8;
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(1000f, 0f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(300f, 58.42001f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Qtd disponível";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel8).Borders = (BorderSide)8;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(833.3127f, 3.814697E-06f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(166.6874f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Unidade";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel7).Borders = (BorderSide)8;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Semibold", 10f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(44.97917f, 0f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(788.3335f, 58.42001f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Matéria-prima";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)16;
			((XRControl)GroupHeader3).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel19,
				(XRControl)xrLabel18,
				(XRControl)xrLabel17,
				(XRControl)xrLabel16
			});
			((XRControl)GroupHeader3).Dpi = 254f;
			((XRControl)GroupHeader3).HeightF = 58.42f;
			((XRControl)GroupHeader3).Name = "GroupHeader3";
			((XRControl)xrLabel19).Borders = (BorderSide)8;
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(1204.146f, 0f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(158.75f, 58.42f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).Text = "Unidade";
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel18).Borders = (BorderSide)8;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(1616.896f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(283.1041f, 58.42f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Data da ordem";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel17).Borders = (BorderSide)8;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(1362.896f, 0f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(254f, 58.42f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Quantidade";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel16).Borders = (BorderSide)8;
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI Semibold", 11f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(1204.146f, 58.42f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).Text = "Produto produzido";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)GroupHeader3
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 10f);
			((XtraReport)this).Margins = new Margins(0, 0, 0, 0);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 1900;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.OrdensProducao.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.OrdensProducao.Properties.Resources", typeof(Resources).Assembly);
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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.AlteracoesRapidas;
using DigisatServer.Application.Main.Dto.Cadastros.AjustesEInformacoesValoresProvenientesDocumentoFiscal;
using DigisatServer.Application.Main.Dto.Cadastros.AlteracoesPreco;
using DigisatServer.Application.Main.Dto.Cadastros.CalculosPrecoCusto;
using DigisatServer.Application.Main.Dto.Cadastros.Grupos;
using DigisatServer.Application.Main.Dto.Cadastros.Localizacoes;
using DigisatServer.Application.Main.Dto.Cadastros.Marcas;
using DigisatServer.Application.Main.Dto.Cadastros.NsCMNsBS;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.PlanoContasContabeis;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.ReformasTributarias;
using DigisatServer.Application.Main.Dto.Cadastros.TipoSegmentacoes;
using DigisatServer.Application.Main.Dto.Cadastros.TributacoesEstadual;
using DigisatServer.Application.Main.Dto.Cadastros.TributacoesFederal;
using DigisatServer.Application.Main.Dto.Cadastros.TributacoesMunicipal;
using DigisatServer.Application.Main.Dto.Cadastros.UnidadesMedida;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Auxiliares.GestaoEmpresarial;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.GenerosItem;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.IndicadoresOperacaoIbsCbs;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Enum.AlteracoesRapidas.CamposAlteracoesRapidas;
using DigisatServer.Infrastructure.Crosscutting.Enum.AlteracoesRapidas.OrigensAlteracoesRapidas;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.Classificacoes;
using DigisatServer.Infrastructure.Crosscutting.Model.IndicadoresOperacaoConsumidorFinal;
using DigisatServer.Infrastructure.Crosscutting.Model.ReferenciasPrecoEtiqueta;
using DigisatServer.Infrastructure.Crosscutting.Model.TiposCalculoJuro;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.Style.GridControls;
using DigisatServer.Infrastructure.Crosscutting.ValueObject;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.CadastrosVO.TiposItem;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Especies.Pagamentos;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Situacoes;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.AlteracoesRapidas.Events;
using Otus.Modulo.AlteracoesRapidas.ViewModels.Campos;
using Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.Pessoas;
using Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.ProdutosServicos;
using Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.Recebimentos;
using Otus.Modulo.AlteracoesRapidas.ViewModels.Origens;
using Otus.Modulo.AlteracoesRapidas.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.AlteracoesRapidas")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.AlteracoesRapidas")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AlteracoesRapidas
{
	[Module(ModuleName = "AlteracaoRapidaModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class AlteracaoRapidaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AlteracaoRapidaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AlteracoesRapidasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AlteracaoRapidaFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ProdutoServicoSelecionadoView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AlteracoesRapidas.Views
{
	public class AlteracaoRapidaFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AlteracaoRapidaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesRapidas;component/views/alteracaorapidafichaview.xaml", UriKind.Relative);
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
	public class AlteracoesRapidasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AlteracoesRapidasView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesRapidas;component/views/alteracoesrapidasview.xaml", UriKind.Relative);
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
	public class ProdutoServicoSelecionadoView : UserControl, IComponentConnector
	{
		internal GridControlBestFit GridControl;

		private bool _contentLoaded;

		public ProdutoServicoSelecionadoView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesRapidas;component/views/produtoservicoselecionadoview.xaml", UriKind.Relative);
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
				GridControl = (GridControlBestFit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels
{
	public class AlteracaoRapidaAbaViewModel : AbaPrincipalViewModelBase
	{
		public AlteracaoRapidaAbaViewModel(IViewModelPrincipal viewModel)
			: base("Alteração rápida", "Dados principais", viewModel)
		{
		}
	}
	public class AlteracaoRapidaFichaViewModel : FichaBase<AlteracaoRapidaDto, AlteracaoRapidaViewModel>
	{
		private readonly IWService _service;

		private bool _executando;

		private AlteracaoRapidaViewModel _alteracaoRapidaViewModel;

		public ICommand ExecutarCommand { get; set; }

		public AlteracaoRapidaFichaViewModel(IWService service)
			: base("Alteração rápida")
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			_service = service;
			ExecutarCommand = (ICommand)new DelegateCommand((Action)Executar, (Func<bool>)PodeExecutar, (bool?)null);
		}

		private bool PodeExecutar()
		{
			if (base.Formulario != null && base.Formulario.Valido)
			{
				return !_executando;
			}
			return false;
		}

		private async void Executar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("As alterações não poderão ser desfeitas, é recomendado efetuar backup da base de dados antes de continuar. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			_executando = true;
			LoaderProcessoService.MostrarLoader("Processando...");
			AlteracaoRapidaDto dto = ((FormularioEntity<AlteracaoRapidaDto>)(AlteracaoRapidaFormulario)(object)((FichaBase<AlteracaoRapidaDto, AlteracaoRapidaViewModel>)this).Formulario).ToDto<AlteracaoRapidaDto>();
			await _service.Canal<IAlteracaoRapidaModuleService, Task<AlteracaoRapidaResultadoDto>>((Func<IAlteracaoRapidaModuleService, Task<AlteracaoRapidaResultadoDto>>)((IAlteracaoRapidaModuleService service) => service.Executar(dto))).ContinueWith(delegate(Task<AlteracaoRapidaResultadoDto> task)
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					Consultar(task.Result);
				}
			});
		}

		private void Consultar(AlteracaoRapidaResultadoDto dto)
		{
			new Thread((ThreadStart)delegate
			{
				try
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.MostrarLoader("Processando...");
					});
					AlteracaoRapidaResultadoDto val;
					do
					{
						val = _service.Canal<IAlteracaoRapidaModuleService, AlteracaoRapidaResultadoDto>((Func<IAlteracaoRapidaModuleService, AlteracaoRapidaResultadoDto>)((IAlteracaoRapidaModuleService service) => service.Consultar(dto.Id)));
					}
					while (!val.Concluido);
					DialogoFactory.MostrarDialogoConfirma((!string.IsNullOrEmpty(val.Erro)) ? $"Atenção!\nO processo de alteração foi iniciado porém um erro ocorreu durante a execução:\n\nErro: {val.Erro}" : $"Alteração efetuada!\nRegistros afetados: {val.RegistrosAfetados}", false, HorizontalAlignment.Center);
					if (dto.Origem.Equals("Produtos e Serviços selecionados"))
					{
						DadosSistema.GetEventPublisher().Publish<FinalizouAlteracoesEvent>(new FinalizouAlteracoesEvent());
					}
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
					_executando = false;
					ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				}
			}).Start();
		}

		protected override async void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			try
			{
				if (HelperObject.IsNull((object)((FichaBase<AlteracaoRapidaDto, AlteracaoRapidaViewModel>)this).Formulario))
				{
					_alteracaoRapidaViewModel = new AlteracaoRapidaViewModel(await _service.Canal<IAlteracaoRapidaModuleService, Task<AlteracaoRapidaDto>>((Func<IAlteracaoRapidaModuleService, Task<AlteracaoRapidaDto>>)((IAlteracaoRapidaModuleService service) => ((IReferenciaAdicionarService<AlteracaoRapidaDto>)(object)service).ObterDtoAsync())), (Action<string, Action<NavigationResult>, NavigationParameters>)((NavigationBase)this).Navegar);
					((FichaBase<AlteracaoRapidaDto, AlteracaoRapidaViewModel>)this).Formulario = _alteracaoRapidaViewModel.CriarFormulario();
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				((NavigationBase)this).FecharView();
			}
		}
	}
	public sealed class AlteracaoRapidaFormulario : FormularioEntity<AlteracaoRapidaDto>
	{
		public AlteracaoRapidaFormulario(AlteracaoRapidaDto dto, Action<string, Action<NavigationResult>, NavigationParameters> navegarAction)
			: base(dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AlteracaoRapidaAbaViewModel((IViewModelPrincipal)(object)new AlteracaoRapidaViewModel(base.Dto, navegarAction)) });
		}
	}
	public class AlteracaoRapidaViewModel : ViewModelDtoBase<AlteracaoRapidaDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private OrigemProdutoServico _produtoServico;

		private readonly Action<string, Action<NavigationResult>, NavigationParameters> _navegarAction;

		private IDisposable _disposable;

		public Lista<OrigemViewModel> Origens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<OrigemViewModel>>((Expression<Func<Lista<OrigemViewModel>>>)(() => Origens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<OrigemViewModel>>((Expression<Func<Lista<OrigemViewModel>>>)(() => Origens), value);
			}
		}

		public List<string> IdsProdutosServicosSelecionados
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<string>>((Expression<Func<List<string>>>)(() => IdsProdutosServicosSelecionados));
			}
			set
			{
				((BindableBase)this).SetProperty<List<string>>((Expression<Func<List<string>>>)(() => IdsProdutosServicosSelecionados), value);
			}
		}

		public string QuantidadeProdutosServicosSelecionadosToText
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => QuantidadeProdutosServicosSelecionadosToText));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => QuantidadeProdutosServicosSelecionadosToText), value);
			}
		}

		public AlteracaoRapidaViewModel()
		{
		}

		public AlteracaoRapidaViewModel(AlteracaoRapidaDto dto, Action<string, Action<NavigationResult>, NavigationParameters> navegarAction)
			: base(dto)
		{
			_navegarAction = navegarAction;
			_disposable = ObservableExtensions.Subscribe<FinalizouAlteracoesEvent>(DadosSistema.GetEventPublisher().GetEvent<FinalizouAlteracoesEvent>(), (Action<FinalizouAlteracoesEvent>)delegate
			{
				Origens?.PosicionarNoPrimeiro();
			});
		}

		public override void ProjectedAsViewModel()
		{
			_produtoServico = new OrigemProdutoServico(base.Dto.UnidadesMedida);
			((BindableBase)_produtoServico).PropertyChanged += ProdutoServicoOnPropertyChanged;
			Origens = new Lista<OrigemViewModel>();
			Origens.Adicionar((OrigemViewModel)_produtoServico);
			Origens.Adicionar((OrigemViewModel)new OrigemRecebimento());
			Origens.Adicionar((OrigemViewModel)new OrigemPessoa(base.Dto.Contas));
			Origens.Adicionar((OrigemViewModel)new OrigemProdutoServicoSelecionados(base.Dto.UnidadesMedida));
			Origens.PosicionarNoPrimeiro();
			((BindableBase)Origens).PropertyChanged += OrigensOnPropertyChanged;
			Origens.AlterouItemSelecionadoEvent += OrigensOnAlterouItemSelecionadoEvent;
		}

		private void ProdutoServicoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((Validation)this).Valid));
		}

		private void OrigensOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((Validation)this).Valid));
		}

		private void OrigensOnAlterouItemSelecionadoEvent(object sender, OrigemViewModel viewModel)
		{
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			Lista<OrigemViewModel> val = sender as Lista<OrigemViewModel>;
			if (HelperObject.IsNotNull((object)val) && HelperObject.Is<OrigemProdutoServicoSelecionados>((object)val.ItemSelecionado))
			{
				ObservableExtensions.Subscribe<SelecionouProdutosServicosEvent>(DadosSistema.GetEventPublisher().GetEvent<SelecionouProdutosServicosEvent>(), (Action<SelecionouProdutosServicosEvent>)delegate(SelecionouProdutosServicosEvent x)
				{
					if (x.Ids.Any())
					{
						IdsProdutosServicosSelecionados = new List<string>();
						IdsProdutosServicosSelecionados.AddRange(x.Ids);
						string quantidadeProdutosServicosSelecionadosToText = ((IdsProdutosServicosSelecionados.Count == 1) ? $"{IdsProdutosServicosSelecionados.Count} item selecionado" : $"{IdsProdutosServicosSelecionados.Count} itens selecionados");
						QuantidadeProdutosServicosSelecionadosToText = quantidadeProdutosServicosSelecionadosToText;
					}
					else
					{
						Origens.PosicionarNoPrimeiro();
					}
				});
				_navegarAction(typeof(ProdutoServicoSelecionadoView).Name, delegate
				{
				}, new NavigationParameters());
			}
			else
			{
				IdsProdutosServicosSelecionados = new List<string>();
				QuantidadeProdutosServicosSelecionadosToText = "Nenhum item selecionado";
			}
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			object obj = dto;
			AlteracaoRapidaDto val = (AlteracaoRapidaDto)((obj is AlteracaoRapidaDto) ? obj : null);
			if (!HelperObject.IsNull((object)dto))
			{
				val.Origem = Origens.ItemSelecionado.ObterOrigem();
				val.PesquisarPor = (CampoDto)(Origens.ItemSelecionado.CamposFiltro.PossuiItemSelecionado ? ((object)Origens.ItemSelecionado.CamposFiltro.ItemSelecionado.ObterDto()) : ((object)new CampoBooleanDto()));
				val.AlterarPara = Origens.ItemSelecionado.CamposAlterar.ItemSelecionado.ObterDto();
				val.IdsProdutosServicosSelecionados = IdsProdutosServicosSelecionados ?? new List<string>();
				base.AtualizarDto<AlteracaoRapidaDto>(ref val);
			}
		}

		public override void Dispose()
		{
			_disposable?.Dispose();
			_disposable = null;
			if (HelperObject.IsNotNull((object)_produtoServico))
			{
				((BindableBase)_produtoServico).PropertyChanged += ProdutoServicoOnPropertyChanged;
			}
			if (HelperObject.IsNotNull((object)Origens))
			{
				Origens.AlterouItemSelecionadoEvent -= OrigensOnAlterouItemSelecionadoEvent;
				((BindableBase)Origens).PropertyChanged -= OrigensOnPropertyChanged;
				foreach (OrigemViewModel iten in Origens.Itens)
				{
					((ViewModelValidationBase)iten.CamposAlterar).Dispose();
					((ViewModelValidationBase)iten.CamposFiltro).Dispose();
				}
				((ViewModelValidationBase)Origens).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new AlteracaoRapidaFormulario(base.Dto, _navegarAction);
		}

		public string ToTituloLista()
		{
			return "Alterações rápida";
		}

		public string ToTituloFicha()
		{
			return "Alteração rápida";
		}
	}
	public class AlteracoesRapidasViewModel : ModuloBase
	{
		public AlteracoesRapidasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAlteracaoRapidaRegion, typeof(AlteracaoRapidaFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ProdutoServicoSelecionadoViewModel : NavigationBase
	{
		private readonly IExecutarProcessoService _executarProcessoService;

		private IEnumerable<ProdutoServicoResumoDto> _selecionados;

		public Lista<ProdutoServicoResumoDto> ProdutosServicos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ProdutoServicoResumoDto>>((Expression<Func<Lista<ProdutoServicoResumoDto>>>)(() => ProdutosServicos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ProdutoServicoResumoDto>>((Expression<Func<Lista<ProdutoServicoResumoDto>>>)(() => ProdutosServicos), value);
			}
		}

		public ICommand SelecionarCommand { get; set; }

		public ProdutoServicoSelecionadoViewModel(IExecutarProcessoService executarProcessoService)
			: base(string.Empty)
		{
			_executarProcessoService = executarProcessoService;
			_selecionados = Enumerable.Empty<ProdutoServicoResumoDto>();
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			SelecionarCommand = (ICommand)new DelegateCommand<object>((Action<object>)Selecionar, (Func<object, bool>)PodeSelecionar, (bool?)null);
		}

		private void Selecionar(object obj)
		{
			_selecionados = RetornarItensSelecionados(obj);
			((NavigationBase)this).FecharView();
		}

		public override void FecharView()
		{
			DadosSistema.GetEventPublisher().Publish<SelecionouProdutosServicosEvent>(new SelecionouProdutosServicosEvent(_selecionados.Select((ProdutoServicoResumoDto x) => x.IdProdutoServicoEmpresa)));
			((NavigationBase)this).FecharView();
		}

		private IEnumerable<ProdutoServicoResumoDto> RetornarItensSelecionados(object obj)
		{
			object obj2 = ((obj is GridControl) ? obj : null);
			if (obj2 == null)
			{
				return null;
			}
			return ((DataControlBase)obj2).SelectedItems.OfType<ProdutoServicoResumoDto>();
		}

		private bool PodeSelecionar(object arg)
		{
			return RetornarItensSelecionados(arg)?.Any() ?? false;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0038: Expected O, but got Unknown
			IEnumerable<ProdutoServicoResumoDto> produtosServicos = null;
			PesquisaDto opcoesPesquisa = new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaGestaoEmpresarialProdutoServicoDto
				{
					TipoProdutoServico = 0,
					FiltroAtivoInativo = (FiltroAtivoInativo)0
				}
			};
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				produtosServicos = DadosSistema.GetService().Canal<IGestaoEmpresarialModuleService, IEnumerable<ProdutoServicoResumoDto>>((Func<IGestaoEmpresarialModuleService, IEnumerable<ProdutoServicoResumoDto>>)((IGestaoEmpresarialModuleService service) => service.ObterProdutosServicos(opcoesPesquisa)));
			}, (Action<object>)delegate(object retorno)
			{
				try
				{
					if (retorno is Exception ex)
					{
						throw ex;
					}
					ProdutosServicos = new Lista<ProdutoServicoResumoDto>();
					ProdutosServicos.Adicionar(produtosServicos);
				}
				catch (Exception ex2)
				{
					DialogoFactory.MostrarDialogoConfirma(ex2.Message, false, HorizontalAlignment.Center);
				}
			});
		}
	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels.Origens
{
	public class OrigemPessoa : OrigemViewModel
	{
		public override Visibility CamposFiltroVisivel => Visibility.Visible;

		public override Visibility CampoNumeroItensVisivel => Visibility.Collapsed;

		public OrigemPessoa(IEnumerable<ContaDto> contas)
			: base(3, "Pessoas")
		{
			base.CamposFiltro = new Lista<CampoViewModel>();
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoDeveGerarCashback());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoLimiteCredito());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPossuiCashback());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoDiaAcerto());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTipoSegmentacao());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoVendedor());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTipoCadastro(ObterTipoCadastro()));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoClassificacao(ObterClassificacao()));
			base.CamposFiltro.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposFiltro.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<CampoViewModel>(base.CamposFiltro.WhenAlterouItemSelecionado, (Action<CampoViewModel>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposFiltro));
			});
			base.CamposAlterar = new Lista<CampoViewModel>();
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDeveGerarCashback());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoLimiteCredito());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoZerarCashback());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDiaAcerto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTipoSegmentacao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoVendedor());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndicadorOperacao(ObterIndicadoresOperacao()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoConta(contas));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoEspeciePagamento(ObterEspeciesPagamento()));
			base.CamposAlterar.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposAlterar.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<CampoViewModel>(base.CamposAlterar.WhenAlterouItemSelecionado, (Action<CampoViewModel>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposAlterar));
			});
		}

		public override OpcaoOrigemDadosAlteracaoRapida ObterOrigem()
		{
			return (OpcaoOrigemDadosAlteracaoRapida)3;
		}

		private IEnumerable<CodigoDescricaoDto> ObterTipoCadastro()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((IEnumerable<KeyValuePair<string, TipoPessoaCadastro>>)HelperEnum.ObterParDeValores<TipoPessoaCadastro>()).Select((Func<KeyValuePair<string, TipoPessoaCadastro>, CodigoDescricaoDto>)((KeyValuePair<string, TipoPessoaCadastro> tipo) => new CodigoDescricaoDto
			{
				Codigo = (int)tipo.Value,
				Descricao = tipo.Key
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterClassificacao()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((IEnumerable<Classificacao>)Classificacoes.Dados).Select((Func<Classificacao, CodigoDescricaoDto>)((Classificacao classificacao) => new CodigoDescricaoDto
			{
				Codigo = classificacao.Codigo,
				Descricao = classificacao.Descricao
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterIndicadoresOperacao()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((IEnumerable<IndicadorOperacaoConsumidorFinal>)IndicadoresOperacaoConsumidorFinal.Dados).Select((Func<IndicadorOperacaoConsumidorFinal, CodigoDescricaoDto>)((IndicadorOperacaoConsumidorFinal classificacao) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)classificacao).Codigo,
				Descricao = ((CodigoDescricao)classificacao).Descricao
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterEspeciesPagamento()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(new EspeciePagamentoValueObjectRepository().EspeciesPagamentoPadraoCliente.Select((Func<EspeciePagamento, CodigoDescricaoDto>)((EspeciePagamento especie) => new CodigoDescricaoDto
			{
				Codigo = especie.Codigo,
				Descricao = especie.Descricao
			})));
		}
	}
	public class OrigemProdutoServico : OrigemViewModel
	{
		public override Visibility CamposFiltroVisivel => Visibility.Visible;

		public override Visibility CampoNumeroItensVisivel => Visibility.Collapsed;

		public OrigemProdutoServico(IEnumerable<UnidadeMedidaDto> unidadesMedida)
			: base(1, "Produtos/Serviços")
		{
			base.CamposFiltro = new Lista<CampoViewModel>();
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCest());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoMarca());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoNcm());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoNbs());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTributacaoEstadual());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTributacaoIbsCbs());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTributacaoMunicipal());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTributacaoFederal());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoSubgrupo());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoUnidadeMedida(unidadesMedida));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoContabilEntrada());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoContabilSaida());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoTipoItem(ObterTiposItem()));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoGeneroItem());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoCusto());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoVenda());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoReferenciaPrecoEtiqueta(ObterOpcoesReferenciaPrecoEtiqueta()));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoProdutoServico());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPrecoCusto());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPrecoVenda());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoBaseCalculoReferenciaPrecoEtiqueta());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPercentualLimiteDesconto());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoUnidadeMedidaTributavel(unidadesMedida));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoBeneficioFiscalUf());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoSistematicaAtacadista());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoAmbienteProducao());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoComissao());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPercentualCashback());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoDiasContatoPosVenda());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoIMendes());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoQuantidadeMinima());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoQuantidadeMaxima());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoLocalizacao());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPesavel());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoDisponivelApp());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCaracteristica());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoDesdobroNacional());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoAtivo());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoBeneficioCreditoPresumido());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoPercentualCreditoPresumido());
			if (DadosSistema.EmpresaLogada.Municipio.Uf.CodigoIbge == 26)
			{
				base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoInformacaoFiscal());
			}
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoIat(ObterOpcoesIat()));
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoAjusteEInformacaoValorProvenienteDocumentoFiscal());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoIndicadorOperacaoIbsCbs());
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoCodigoTributacaoMunicipio());
			base.CamposFiltro.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposFiltro.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<CampoViewModel>(base.CamposFiltro.WhenAlterouItemSelecionado, (Action<CampoViewModel>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposFiltro));
			});
			base.CamposAlterar = new Lista<CampoViewModel>();
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCest());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoMarca());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoNcm());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoNbs());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoEstadual());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoIbsCbs());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoMunicipal());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoFederal());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoSubgrupo());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoUnidadeMedida(unidadesMedida));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoContabilEntrada());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoContabilSaida());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTipoItem(ObterTiposItem()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoGeneroItem());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoCusto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoReferenciaPrecoEtiqueta(ObterOpcoesReferenciaPrecoEtiqueta()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPrecoCusto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPrecoVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoBaseCalculoReferenciaPrecoEtiqueta());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualLimiteDesconto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoUnidadeMedidaTributavel(unidadesMedida));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoBeneficioFiscalUf());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoSistematicaAtacadista());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoAmbienteProducao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoComissao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualCashback());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDiasContatoPosVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoIMendes());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDescricaoIMendes());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPermiteVendaFracionada());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoQuantidadeMinima());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoQuantidadeMaxima());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoLocalizacao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPesavel());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoVendavel());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDisponivelApp());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCaracteristica());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoBeneficioCreditoPresumido());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualCreditoPresumido());
			if (DadosSistema.EmpresaLogada.Municipio.Uf.CodigoIbge == 26)
			{
				base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoInformacaoFiscal());
			}
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIat(ObterOpcoesIat()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoAjusteEInformacaoValorProvenienteDocumentoFiscal());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDesdobroNacional());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndicadorOperacaoIbsCbs());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoTributacaoMunicipio());
			base.CamposAlterar.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposAlterar.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<CampoViewModel>(base.CamposAlterar.WhenAlterouItemSelecionado, (Action<CampoViewModel>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposAlterar));
			});
		}

		private IEnumerable<CodigoDescricaoDto> ObterOpcoesIat()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((IEnumerable<KeyValuePair<string, Iat>>)HelperEnum.ObterParDeValores<Iat>()).Select((Func<KeyValuePair<string, Iat>, CodigoDescricaoDto>)((KeyValuePair<string, Iat> tipo) => new CodigoDescricaoDto
			{
				Codigo = (int)tipo.Value,
				Descricao = tipo.Key
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterTiposItem()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((ValueObjectRepository<TipoItem>)(object)ValueObjectService<TipoItem, TipoItemValueObjectRepository>.ValueObjectRepository).Dados.Where((TipoItem x) => x.Codigo != 9).Select((Func<TipoItem, CodigoDescricaoDto>)((TipoItem item) => new CodigoDescricaoDto
			{
				Codigo = item.Codigo,
				Descricao = item.Descricao
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterOpcoesReferenciaPrecoEtiqueta()
		{
			return ((IEnumerable<ReferenciaPrecoEtiqueta>)ReferenciasPrecoEtiqueta.Dados).Select((Func<ReferenciaPrecoEtiqueta, CodigoDescricaoDto>)((ReferenciaPrecoEtiqueta x) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)x).Codigo,
				Descricao = ((CodigoDescricao)x).Descricao
			}));
		}

		public override OpcaoOrigemDadosAlteracaoRapida ObterOrigem()
		{
			return (OpcaoOrigemDadosAlteracaoRapida)1;
		}
	}
	public class OrigemProdutoServicoSelecionados : OrigemViewModel
	{
		public override Visibility CamposFiltroVisivel => Visibility.Collapsed;

		public override Visibility CampoNumeroItensVisivel => Visibility.Visible;

		public OrigemProdutoServicoSelecionados(IEnumerable<UnidadeMedidaDto> unidadesMedida)
			: base(4, "Selecionar Produtos/Serviços")
		{
			base.CamposFiltro = new Lista<CampoViewModel>();
			base.CamposAlterar = new Lista<CampoViewModel>();
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCest());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoMarca());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoNcm());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoNbs());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoEstadual());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoIbsCbs());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoMunicipal());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTributacaoFederal());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoSubgrupo());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoUnidadeMedida(unidadesMedida));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoContabilEntrada());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoContabilSaida());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoTipoItem(ObterTiposItem()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoGeneroItem());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoCusto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIndiceCalculoPrecoVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoReferenciaPrecoEtiqueta(ObterOpcoesReferenciaPrecoEtiqueta()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPrecoCusto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPrecoVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoBaseCalculoReferenciaPrecoEtiqueta());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualLimiteDesconto());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoUnidadeMedidaTributavel(unidadesMedida));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoBeneficioFiscalUf());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoAmbienteProducao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoComissao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualCashback());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDiasContatoPosVenda());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoIMendes());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDescricaoIMendes());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPermiteVendaFracionada());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoQuantidadeMinima());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoQuantidadeMaxima());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoLocalizacao());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPesavel());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoVendavel());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoDisponivelApp());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCaracteristica());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoBeneficioCreditoPresumido());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoPercentualCreditoPresumido());
			if (DadosSistema.EmpresaLogada.Municipio.Uf.CodigoIbge == 26)
			{
				base.CamposAlterar.Adicionar((CampoViewModel)new CampoCodigoInformacaoFiscal());
			}
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoIat(ObterOpcoesIat()));
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoAjusteEInformacaoValorProvenienteDocumentoFiscal());
			base.CamposAlterar.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposAlterar.PosicionarNoPrimeiro();
			ObservableExtensions.Subscribe<CampoViewModel>(base.CamposAlterar.WhenAlterouItemSelecionado, (Action<CampoViewModel>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposAlterar));
			});
		}

		private IEnumerable<CodigoDescricaoDto> ObterOpcoesIat()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((IEnumerable<KeyValuePair<string, Iat>>)HelperEnum.ObterParDeValores<Iat>()).Select((Func<KeyValuePair<string, Iat>, CodigoDescricaoDto>)((KeyValuePair<string, Iat> tipo) => new CodigoDescricaoDto
			{
				Codigo = (int)tipo.Value,
				Descricao = tipo.Key
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterTiposItem()
		{
			return HelperIEnumerable.ToListEmptyWhenNull<CodigoDescricaoDto>(((ValueObjectRepository<TipoItem>)(object)ValueObjectService<TipoItem, TipoItemValueObjectRepository>.ValueObjectRepository).Dados.Where((TipoItem x) => x.Codigo != 9).Select((Func<TipoItem, CodigoDescricaoDto>)((TipoItem item) => new CodigoDescricaoDto
			{
				Codigo = item.Codigo,
				Descricao = item.Descricao
			})));
		}

		private IEnumerable<CodigoDescricaoDto> ObterOpcoesReferenciaPrecoEtiqueta()
		{
			return ((IEnumerable<ReferenciaPrecoEtiqueta>)ReferenciasPrecoEtiqueta.Dados).Select((Func<ReferenciaPrecoEtiqueta, CodigoDescricaoDto>)((ReferenciaPrecoEtiqueta x) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)x).Codigo,
				Descricao = ((CodigoDescricao)x).Descricao
			}));
		}

		public override OpcaoOrigemDadosAlteracaoRapida ObterOrigem()
		{
			return (OpcaoOrigemDadosAlteracaoRapida)4;
		}
	}
	public class OrigemRecebimento : OrigemViewModel
	{
		public override Visibility CamposFiltroVisivel => Visibility.Visible;

		public override Visibility CampoNumeroItensVisivel => Visibility.Collapsed;

		public OrigemRecebimento()
			: base(2, "Recebimentos")
		{
			base.CamposFiltro = new Lista<CampoViewModel>();
			base.CamposFiltro.Adicionar((CampoViewModel)new CampoSituacao());
			base.CamposFiltro.PosicionarNoPrimeiro();
			base.CamposAlterar = new Lista<CampoViewModel>();
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoJuro());
			base.CamposAlterar.Adicionar((CampoViewModel)new CampoMulta());
			base.CamposAlterar.Ordenar((Expression<Func<CampoViewModel, object>>)((CampoViewModel x) => x.Descricao), (Expression<Func<CampoViewModel, object>>)null);
			base.CamposAlterar.PosicionarNoPrimeiro();
		}

		public override OpcaoOrigemDadosAlteracaoRapida ObterOrigem()
		{
			return (OpcaoOrigemDadosAlteracaoRapida)2;
		}
	}
	public abstract class OrigemViewModel : BindableBase
	{
		public int Codigo { get; protected set; }

		public string Descricao { get; protected set; }

		public Lista<CampoViewModel> CamposFiltro
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposFiltro));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposFiltro), value);
			}
		}

		public Lista<CampoViewModel> CamposAlterar
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposAlterar));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CampoViewModel>>((Expression<Func<Lista<CampoViewModel>>>)(() => CamposAlterar), value);
			}
		}

		public abstract Visibility CamposFiltroVisivel { get; }

		public abstract Visibility CampoNumeroItensVisivel { get; }

		protected OrigemViewModel(int codigo, string descricao)
		{
			Codigo = codigo;
			Descricao = descricao;
		}

		public abstract OpcaoOrigemDadosAlteracaoRapida ObterOrigem();
	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels.Campos
{
	public class CampoVoid : CampoViewModel
	{
		public bool Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Valor), value);
			}
		}

		public CampoVoid(int codigo, string descricao)
			: base(codigo, descricao)
		{
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return (CampoDto)new CampoBooleanDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Valor = Valor
			};
		}
	}
	public class CampoBoolean : CampoViewModel
	{
		public bool Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Valor), value);
			}
		}

		public CampoBoolean(int codigo, string descricao)
			: base(codigo, descricao)
		{
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return (CampoDto)new CampoBooleanDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Valor = Valor
			};
		}
	}
	public class CampoDecimal : CampoViewModel
	{
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

		public CampoDecimal(int codigo, string descricao)
			: base(codigo, descricao)
		{
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return (CampoDto)new CampoDecimalDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Valor = Valor
			};
		}
	}
	public class CampoInteiro : CampoViewModel
	{
		public int Valor
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Valor));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Valor), value);
			}
		}

		public CampoInteiro(int codigo, string descricao)
			: base(codigo, descricao)
		{
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return (CampoDto)new CampoInteiroDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Valor = Valor
			};
		}
	}
	public class CampoJuroMulta : CampoViewModel
	{
		public CampoInteiro DiasCarencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<CampoInteiro>((Expression<Func<CampoInteiro>>)(() => DiasCarencia));
			}
			set
			{
				((BindableBase)this).SetProperty<CampoInteiro>((Expression<Func<CampoInteiro>>)(() => DiasCarencia), value);
			}
		}

		public CampoDecimal Percentual
		{
			get
			{
				return ((BindableBase)this).GetProperty<CampoDecimal>((Expression<Func<CampoDecimal>>)(() => Percentual));
			}
			set
			{
				((BindableBase)this).SetProperty<CampoDecimal>((Expression<Func<CampoDecimal>>)(() => Percentual), value);
			}
		}

		public CampoListaCodigoDescricao TipoCalculo
		{
			get
			{
				return ((BindableBase)this).GetProperty<CampoListaCodigoDescricao>((Expression<Func<CampoListaCodigoDescricao>>)(() => TipoCalculo));
			}
			set
			{
				((BindableBase)this).SetProperty<CampoListaCodigoDescricao>((Expression<Func<CampoListaCodigoDescricao>>)(() => TipoCalculo), value);
			}
		}

		public CampoJuroMulta(int codigo, string descricao, IEnumerable<CodigoDescricaoDto> tipos = null)
			: base(codigo, descricao)
		{
			DiasCarencia = new CampoInteiro(1, "Dias de carência para cobrança de " + descricao);
			Percentual = new CampoDecimal(2, "Percentual de " + descricao);
			if (HelperObject.IsNotNull((object)tipos) && tipos.Any())
			{
				TipoCalculo = new CampoListaCodigoDescricao(3, "Tipo do cálculo de " + descricao, tipos);
			}
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Expected O, but got Unknown
			CampoJuroMultaDto val = new CampoJuroMultaDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Percentual = Percentual.Valor,
				DiasCarencia = DiasCarencia.Valor
			};
			CampoListaCodigoDescricao tipoCalculo = TipoCalculo;
			val.TipoCalculo = ((tipoCalculo != null) ? tipoCalculo.Lista.ItemSelecionado.Codigo : (-1));
			return (CampoDto)val;
		}
	}
	public abstract class CampoListaEntity : CampoEntity
	{
		public ObservableCollection<string> Referencias
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObservableCollection<string>>((Expression<Func<ObservableCollection<string>>>)(() => Referencias));
			}
			set
			{
				((BindableBase)this).SetProperty<ObservableCollection<string>>((Expression<Func<ObservableCollection<string>>>)(() => Referencias), value);
			}
		}

		protected CampoListaEntity(int codigo, string descricao, bool obrigatorio = false)
			: base(codigo, descricao, obrigatorio)
		{
		}

		protected override void Remover()
		{
			Referencias = new ObservableCollection<string>();
			base.Remover();
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return (CampoDto)new CampoListaReferenciaDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Referencias = Referencias
			};
		}
	}
	public abstract class CampoEntityComboBox : CampoEntityLista<EntityBaseDto>
	{
		protected CampoEntityComboBox(int codigo, string descricao, IEnumerable<EntityBaseDto> itens)
			: base(codigo, descricao, itens)
		{
		}
	}
	public abstract class CampoEntityLista<TDto> : CampoViewModel where TDto : EntityBaseDto
	{
		public Lista<TDto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TDto>>((Expression<Func<Lista<TDto>>>)(() => Lista), value);
			}
		}

		protected CampoEntityLista(int codigo, string descricao, IEnumerable<TDto> itens)
			: base(codigo, descricao)
		{
			Lista = new Lista<TDto>();
			Lista.Adicionar(itens);
			Lista.PosicionarNoPrimeiro();
		}

		public override CampoDto ObterDto()
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			if (Lista != null && Lista.PossuiItemSelecionado)
			{
				return (CampoDto)new CampoReferenciaDto
				{
					Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
					Referencia = HelperString.ToStringEmptyWhenNull(((EntityBaseDto)Lista.ItemSelecionado).Id)
				};
			}
			return (CampoDto)new CampoReferenciaDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo
			};
		}
	}
	public class CampoListaCodigoDescricao : CampoViewModel
	{
		public Lista<CodigoDescricaoDto> Lista
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Lista));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Lista), value);
			}
		}

		public CampoListaCodigoDescricao(int codigo, string descricao, IEnumerable<CodigoDescricaoDto> itens)
			: base(codigo, descricao)
		{
			Lista = new Lista<CodigoDescricaoDto>();
			Lista.Adicionar(itens);
			Lista.PosicionarNoPrimeiro();
		}

		public override CampoDto ObterDto()
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			if (Lista != null && Lista.PossuiItemSelecionado)
			{
				return (CampoDto)new CampoCodigoDescricaoDto
				{
					Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
					Codigo = Lista.ItemSelecionado.Codigo,
					Descricao = Lista.ItemSelecionado.Descricao
				};
			}
			return (CampoDto)new CampoCodigoDescricaoDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo
			};
		}
	}
	public abstract class CampoEntity : CampoViewModel
	{
		public abstract ISelecaoCommandParameter CommandParameter { get; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório.")]
		public string Informacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Informacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Informacao), value);
			}
		}

		public string Referencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Referencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Referencia), value);
			}
		}

		public ICommand RemoverCommand { get; set; }

		protected CampoEntity(int codigo, string descricao, bool obrigatorio = false)
			: base(codigo, descricao)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			RemoverCommand = (ICommand)new DelegateCommand((Action)Remover, (Func<bool>)PodeRemover, (bool?)null);
			base.Obrigatorio = obrigatorio;
		}

		protected virtual bool PodeRemover()
		{
			if (!string.IsNullOrEmpty(Informacao))
			{
				return !string.IsNullOrEmpty(Referencia);
			}
			return false;
		}

		protected virtual void Remover()
		{
			Informacao = string.Empty;
			Referencia = string.Empty;
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			return (CampoDto)new CampoReferenciaDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Referencia = HelperString.ToStringEmptyWhenNull(Referencia)
			};
		}
	}
	public abstract class CampoSelecaoCodigoDescricao : CampoViewModel
	{
		public abstract ISelecaoCommandParameter CommandParameter { get; }

		public CodigoDescricaoDto ObjetoSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<CodigoDescricaoDto>((Expression<Func<CodigoDescricaoDto>>)(() => ObjetoSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<CodigoDescricaoDto>((Expression<Func<CodigoDescricaoDto>>)(() => ObjetoSelecionado), value);
			}
		}

		public ICommand RemoverCommand { get; set; }

		protected CampoSelecaoCodigoDescricao(int codigo, string descricao)
			: base(codigo, descricao)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			RemoverCommand = (ICommand)new DelegateCommand((Action)Remover, (Func<bool>)PodeRemover, (bool?)null);
		}

		private bool PodeRemover()
		{
			return HelperObject.IsNotNull((object)ObjetoSelecionado);
		}

		private void Remover()
		{
			ObjetoSelecionado = null;
		}

		public override CampoDto ObterDto()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			if (HelperObject.IsNotNull((object)ObjetoSelecionado))
			{
				return (CampoDto)new CampoCodigoDescricaoDto
				{
					Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
					Codigo = ObjetoSelecionado.Codigo,
					Descricao = HelperString.ToStringEmptyWhenNull(ObjetoSelecionado.Descricao)
				};
			}
			return (CampoDto)new CampoCodigoDescricaoDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Codigo = -1
			};
		}
	}
	public class CampoTexto : CampoViewModel
	{
		public string Informacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Informacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Informacao), value);
			}
		}

		protected CampoTexto(int codigo, string descricao)
			: base(codigo, descricao)
		{
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			return (CampoDto)new CampoTextDto
			{
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo,
				Informacao = HelperString.ToStringEmptyWhenNull(Informacao)
			};
		}
	}
	public abstract class CampoTextoMascara : CampoTexto
	{
		public string Mascara
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Mascara));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Mascara), value);
			}
		}

		protected CampoTextoMascara(int codigo, string descricao, string mascara)
			: base(codigo, descricao)
		{
			Mascara = mascara;
		}
	}
	public abstract class CampoValueObject : CampoViewModel
	{
		public abstract ISelecaoCommandParameter CommandParameter { get; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo obrigatório.")]
		public string ToText
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ToText));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ToText), value);
			}
		}

		public string Informacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Informacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Informacao), value);
			}
		}

		public ICommand RemoverCommand { get; set; }

		protected CampoValueObject(int codigo, string descricao, bool obrigatorio = false)
			: base(codigo, descricao)
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			RemoverCommand = (ICommand)new DelegateCommand((Action)Remover, (Func<bool>)PodeRemover, (bool?)null);
			base.Obrigatorio = obrigatorio;
		}

		protected virtual bool PodeRemover()
		{
			if (!string.IsNullOrEmpty(Informacao))
			{
				return !string.IsNullOrEmpty(ToText);
			}
			return false;
		}

		private void Remover()
		{
			Informacao = string.Empty;
			ToText = string.Empty;
		}

		public override CampoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			return (CampoDto)new CampoTextDto
			{
				Informacao = HelperString.ToStringEmptyWhenNull(Informacao),
				Campo = (OpcaoCampoAlteracaoRapida)base.Codigo
			};
		}
	}
	public abstract class CampoViewModel : ViewModelValidationBase
	{
		public int Codigo { get; protected set; }

		public string Descricao { get; protected set; }

		public bool Obrigatorio
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Obrigatorio));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Obrigatorio), value);
			}
		}

		protected CampoViewModel(int codigo, string descricao)
		{
			Codigo = codigo;
			Descricao = descricao;
		}

		public abstract CampoDto ObterDto();
	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.Recebimentos
{
	public class CampoJuro : CampoJuroMulta
	{
		public CampoJuro()
			: base(20, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)20), ((IEnumerable<TipoCalculoJuro>)TiposCalculoJuro.Dados).Select((Func<TipoCalculoJuro, CodigoDescricaoDto>)((TipoCalculoJuro x) => new CodigoDescricaoDto
			{
				Codigo = ((CodigoDescricao)x).Codigo,
				Descricao = ((CodigoDescricao)x).Descricao
			})))
		{
		}
	}
	public class CampoMulta : CampoJuroMulta
	{
		public CampoMulta()
			: base(21, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)21))
		{
		}
	}
	public class CampoSituacao : CampoListaCodigoDescricao
	{
		public CampoSituacao()
			: base(22, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)22), new List<CodigoDescricaoDto>
			{
				new CodigoDescricaoDto
				{
					Codigo = 1,
					Descricao = ((Situacao)new Pendente()).Descricao
				}
			})
		{
		}//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.Pessoas
{
	public class CampoClassificacao : CampoListaCodigoDescricao
	{
		public CampoClassificacao(IEnumerable<CodigoDescricaoDto> itens)
			: base(54, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)54), itens)
		{
		}
	}
	public class CampoConta : CampoEntityComboBox
	{
		public CampoConta(IEnumerable<ContaDto> itens)
			: base(64, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)64), (IEnumerable<EntityBaseDto>)itens)
		{
		}
	}
	public class CampoEspeciePagamento : CampoListaCodigoDescricao
	{
		public CampoEspeciePagamento(IEnumerable<CodigoDescricaoDto> itens)
			: base(65, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)65), itens)
		{
		}
	}
	public class CampoDiaAcerto : CampoInteiro
	{
		public CampoDiaAcerto()
			: base(42, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)42))
		{
		}
	}
	public class CampoIndicadorOperacao : CampoListaCodigoDescricao
	{
		public CampoIndicadorOperacao(IEnumerable<CodigoDescricaoDto> itens)
			: base(55, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)55), itens)
		{
		}
	}
	public class CampoPossuiCashback : CampoVoid
	{
		public CampoPossuiCashback()
			: base(39, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)39))
		{
		}
	}
	public class CampoTipoCadastro : CampoListaCodigoDescricao
	{
		public CampoTipoCadastro(IEnumerable<CodigoDescricaoDto> itens)
			: base(47, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)47), itens)
		{
		}
	}
	public class CampoTipoSegmentacao : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TipoSegmentacaoDto>("TipoSegmentacaoListaView", (Action<TipoSegmentacaoDto>)delegate(TipoSegmentacaoDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTipoSegmentacao()
			: base(44, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)44))
		{
		}
	}
	public class CampoVendedor : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, (ValidateParameter<PessoaDto>)(object)new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => HelperObject.IsNot<VendedorDtoNull>((object)x.Fisica.Vendedor)), "Selecione uma pessoa do tipo 'Vendedor'!", (TipoPessoaCadastro)6, HorizontalAlignment.Center));

		public CampoVendedor()
			: base(45, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)45))
		{
		}
	}
	public class CampoZerarCashback : CampoVoid
	{
		public CampoZerarCashback()
			: base(38, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)38))
		{
		}
	}
	public class CampoLimiteCredito : CampoDecimal
	{
		public CampoLimiteCredito()
			: base(34, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)34))
		{
		}
	}
	public class CampoDeveGerarCashback : CampoBoolean
	{
		public CampoDeveGerarCashback()
			: base(30, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)30))
		{
		}
	}
}
namespace Otus.Modulo.AlteracoesRapidas.ViewModels.Campos.ProdutosServicos
{
	public class CampoBaseCalculoReferenciaPrecoEtiqueta : CampoDecimal
	{
		public CampoBaseCalculoReferenciaPrecoEtiqueta()
			: base(63, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)63))
		{
		}
	}
	public class CampoCodigoTributacaoMunicipio : CampoTexto
	{
		public CampoCodigoTributacaoMunicipio()
			: base(61, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)61))
		{
		}
	}
	public class CampoDesdobroNacional : CampoInteiro
	{
		public CampoDesdobroNacional()
			: base(59, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)59))
		{
		}
	}
	public class CampoSistematicaAtacadista : CampoBoolean
	{
		public CampoSistematicaAtacadista()
			: base(58, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)58))
		{
		}
	}
	public class CampoIndicadorOperacaoIbsCbs : CampoValueObject
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IndicadorOperacaoIbsCbsDto>("IndicadorOperacaoIbsCbsListaView", (Action<IndicadorOperacaoIbsCbsDto>)delegate(IndicadorOperacaoIbsCbsDto dto)
		{
			base.Informacao = dto.Codigo.ToString();
			base.ToText = ((BaseDto)dto).ToText;
		}, true);

		public CampoIndicadorOperacaoIbsCbs()
			: base(60, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)60), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoVendavel : CampoBoolean
	{
		public CampoVendavel()
			: base(62, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)62))
		{
		}
	}
	public class CampoAjusteEInformacaoValorProvenienteDocumentoFiscal : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<AjusteEInformacaoValorProvenienteDocumentoFiscalDto>("AjusteEInformacaoValorProvenienteDocumentoFiscalListaView", (Action<AjusteEInformacaoValorProvenienteDocumentoFiscalDto>)delegate(AjusteEInformacaoValorProvenienteDocumentoFiscalDto dto)
		{
			base.Informacao = dto.Codigo;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoAjusteEInformacaoValorProvenienteDocumentoFiscal()
			: base(53, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)53))
		{
		}
	}
	public class CampoAmbienteProducao : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<ImpressoraDto>("ImpressoraListaView", (Action<ImpressoraDto>)delegate(ImpressoraDto dto)
		{
			base.Informacao = dto.Descricao;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoAmbienteProducao()
			: base(26, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)26))
		{
		}
	}
	public class CampoCest : CampoTextoMascara
	{
		public CampoCest()
			: base(1, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)1), "00.000.00")
		{
		}
	}
	public class CampoCodigoBeneficioCreditoPresumido : CampoTexto
	{
		public CampoCodigoBeneficioCreditoPresumido()
			: base(50, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)50))
		{
		}
	}
	public class CampoCodigoBeneficioFiscalUf : CampoTexto
	{
		public CampoCodigoBeneficioFiscalUf()
			: base(25, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)25))
		{
		}
	}
	public class CampoCodigoContabilEntrada : CampoValueObject
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PlanoContaContabilDto>("PlanoContaContabilListaView", (Action<PlanoContaContabilDto>)delegate(PlanoContaContabilDto dto)
		{
			base.Informacao = dto.Codigo;
			base.ToText = ((BaseDto)dto).ToText;
		}, true);

		public CampoCodigoContabilEntrada()
			: base(10, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)10))
		{
		}
	}
	public class CampoCodigoContabilSaida : CampoValueObject
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PlanoContaContabilDto>("PlanoContaContabilListaView", (Action<PlanoContaContabilDto>)delegate(PlanoContaContabilDto dto)
		{
			base.Informacao = dto.Codigo;
			base.ToText = ((BaseDto)dto).ToText;
		}, true);

		public CampoCodigoContabilSaida()
			: base(11, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)11))
		{
		}
	}
	public class CampoCaracteristica : CampoTexto
	{
		public CampoCaracteristica()
			: base(43, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)43))
		{
		}
	}
	public class CampoCodigoIMendes : CampoTexto
	{
		public CampoCodigoIMendes()
			: base(32, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)32))
		{
		}
	}
	public class CampoCodigoInformacaoFiscal : CampoTexto
	{
		public CampoCodigoInformacaoFiscal()
			: base(48, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)48))
		{
		}
	}
	public class CampoDescricaoIMendes : CampoTexto
	{
		public CampoDescricaoIMendes()
			: base(33, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)33))
		{
		}
	}
	public class CampoAtivo : CampoBoolean
	{
		public CampoAtivo()
			: base(49, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)49))
		{
		}
	}
	public class CampoDisponivelApp : CampoBoolean
	{
		public CampoDisponivelApp()
			: base(46, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)46))
		{
		}
	}
	public class CampoIat : CampoListaCodigoDescricao
	{
		public CampoIat(IEnumerable<CodigoDescricaoDto> itens)
			: base(52, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)52), itens)
		{
		}
	}
	public class CampoPercentualCreditoPresumido : CampoDecimal
	{
		public CampoPercentualCreditoPresumido()
			: base(51, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)51))
		{
		}
	}
	public class CampoPesavel : CampoBoolean
	{
		public CampoPesavel()
			: base(41, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)41))
		{
		}
	}
	public class CampoDiasContatoPosVenda : CampoInteiro
	{
		public CampoDiasContatoPosVenda()
			: base(31, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)31))
		{
		}
	}
	public class CampoLocalizacao : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<LocalizacaoDto>("LocalizacaoListaView", (Action<LocalizacaoDto>)delegate(LocalizacaoDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoLocalizacao()
			: base(40, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)40))
		{
		}
	}
	public class CampoQuantidadeMaxima : CampoDecimal
	{
		public CampoQuantidadeMaxima()
			: base(37, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)37))
		{
		}
	}
	public class CampoQuantidadeMinima : CampoDecimal
	{
		public CampoQuantidadeMinima()
			: base(36, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)36))
		{
		}
	}
	public class CampoPercentualCashback : CampoDecimal
	{
		public CampoPercentualCashback()
			: base(28, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)28))
		{
		}
	}
	public class CampoComissao : CampoDecimal
	{
		public CampoComissao()
			: base(27, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)27))
		{
		}
	}
	public class CampoGeneroItem : CampoSelecaoCodigoDescricao
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<GeneroItemDto>("GeneroItemListaView", (Action<GeneroItemDto>)delegate(GeneroItemDto dto)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			base.ObjetoSelecionado = new CodigoDescricaoDto
			{
				Codigo = dto.Codigo,
				Descricao = dto.Descricao
			};
		}, true);

		public CampoGeneroItem()
			: base(13, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)13))
		{
		}
	}
	public class CampoIndiceCalculoPrecoCusto : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<CalculoPrecoCustoDto>("CalculoPrecoCustoListaView", (Action<CalculoPrecoCustoDto>)delegate(CalculoPrecoCustoDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoIndiceCalculoPrecoCusto()
			: base(14, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)14))
		{
		}
	}
	public class CampoIndiceCalculoPrecoVenda : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<AlteracaoPrecoDto>("CalculoPrecoVendaListaView", (Action<AlteracaoPrecoDto>)delegate(AlteracaoPrecoDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoIndiceCalculoPrecoVenda()
			: base(15, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)15))
		{
		}
	}
	public class CampoMarca : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MarcaDto>("MarcaListaView", (Action<MarcaDto>)delegate(MarcaDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoMarca()
			: base(2, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)2))
		{
		}
	}
	public class CampoNbs : CampoValueObject
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<NCMNBSDto>("NBSListaView", (Action<NCMNBSDto>)delegate(NCMNBSDto dto)
		{
			base.Informacao = dto.Codigo;
			base.ToText = ((BaseDto)dto).ToText;
		}, true);

		public CampoNbs()
			: base(4, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)4), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoNcm : CampoValueObject
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<NCMNBSDto>("NCMListaView", (Action<NCMNBSDto>)delegate(NCMNBSDto dto)
		{
			base.Informacao = dto.Codigo;
			base.ToText = ((BaseDto)dto).ToText;
		}, true);

		public CampoNcm()
			: base(3, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)3), obrigatorio: true)
		{
		}
	}
	public class CampoPercentualLimiteDesconto : CampoDecimal
	{
		public CampoPercentualLimiteDesconto()
			: base(23, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)23))
		{
		}
	}
	public class CampoPermiteVendaFracionada : CampoBoolean
	{
		public CampoPermiteVendaFracionada()
			: base(35, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)35))
		{
		}
	}
	public class CampoPrecoCusto : CampoDecimal
	{
		public CampoPrecoCusto()
			: base(18, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)18))
		{
		}
	}
	public class CampoPrecoVenda : CampoDecimal
	{
		public CampoPrecoVenda()
			: base(19, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)19))
		{
		}
	}
	public class CampoProdutoServico : CampoListaEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>("ProdutoServicoListaView", (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> dto)
		{
			base.Informacao = ((!dto.Any()) ? "Nenhum produto/serviço selecionado" : ((dto.Count() == 1) ? "1 produto/serviço selecionado" : $"{dto.Count()} produtos/serviços selecionados"));
			base.Referencias = new ObservableCollection<string>(dto.Select((ProdutoServicoEmpresaDto produtoServico) => ((EntityBaseDto)produtoServico).Id));
		}, true);

		public CampoProdutoServico()
			: base(17, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)17), obrigatorio: true)
		{
		}
	}
	public class CampoReferenciaPrecoEtiqueta : CampoListaCodigoDescricao
	{
		public CampoReferenciaPrecoEtiqueta(IEnumerable<CodigoDescricaoDto> itens)
			: base(16, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)16), itens)
		{
		}
	}
	public class CampoSubgrupo : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<SubGrupoDto>("ProdutoServicoSubGrupoListaView", (Action<SubGrupoDto>)delegate(SubGrupoDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoSubgrupo()
			: base(8, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)8))
		{
		}
	}
	public class CampoTipoItem : CampoListaCodigoDescricao
	{
		public CampoTipoItem(IEnumerable<CodigoDescricaoDto> itens)
			: base(12, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)12), itens)
		{
		}
	}
	public class CampoTributacaoIs : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TributacaoIsDto>("TributacaoIsListaView", (Action<TributacaoIsDto>)delegate(TributacaoIsDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTributacaoIs()
			: base(57, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)57), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoTributacaoIbsCbs : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TributacaoIbsCbsDto>("TributacaoIbsCbsListaView", (Action<TributacaoIbsCbsDto>)delegate(TributacaoIbsCbsDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTributacaoIbsCbs()
			: base(56, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)56), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoTributacaoEstadual : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TributacaoEstadualDto>("TributacaoEstadualListaView", (Action<TributacaoEstadualDto>)delegate(TributacaoEstadualDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTributacaoEstadual()
			: base(5, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)5), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoTributacaoFederal : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TributacaoFederalDto>("TributacaoFederalListaView", (Action<TributacaoFederalDto>)delegate(TributacaoFederalDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTributacaoFederal()
			: base(7, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)7), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoTributacaoMunicipal : CampoEntity
	{
		public override ISelecaoCommandParameter CommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<TributacaoMunicipalDto>("TributacaoMunicipalListaView", (Action<TributacaoMunicipalDto>)delegate(TributacaoMunicipalDto dto)
		{
			base.Informacao = ((BaseDto)dto).ToText;
			base.Referencia = ((EntityBaseDto)dto).Id;
		}, true);

		public CampoTributacaoMunicipal()
			: base(6, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)6), obrigatorio: true)
		{
		}

		protected override bool PodeRemover()
		{
			return false;
		}
	}
	public class CampoUnidadeMedida : CampoEntityComboBox
	{
		public CampoUnidadeMedida(IEnumerable<UnidadeMedidaDto> itens)
			: base(9, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)9), (IEnumerable<EntityBaseDto>)itens)
		{
		}
	}
	public class CampoUnidadeMedidaTributavel : CampoEntityComboBox
	{
		public CampoUnidadeMedidaTributavel(IEnumerable<UnidadeMedidaDto> itens)
			: base(24, HelperEnum.RetornarDescricao((Enum)(object)(OpcaoCampoAlteracaoRapida)24), (IEnumerable<EntityBaseDto>)itens)
		{
		}
	}
}
namespace Otus.Modulo.AlteracoesRapidas.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.AlteracoesRapidas.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.AlteracoesRapidas.Events
{
	public class SelecionouProdutosServicosEvent
	{
		public IEnumerable<string> Ids { get; set; }

		public SelecionouProdutosServicosEvent(IEnumerable<string> ids)
		{
			Ids = ids;
		}
	}
	public class FinalizouAlteracoesEvent
	{
	}
}

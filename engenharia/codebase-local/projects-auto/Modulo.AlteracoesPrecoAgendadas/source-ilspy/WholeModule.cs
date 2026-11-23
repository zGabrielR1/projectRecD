using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
using DevExpress.Xpf.Grid;
using DigisatServer.Application.Main.Dto.Auxiliares.AlteracoesPrecoAgendadas;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using DigisatServer.Infrastructure.Crosscutting.Visitor;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Filtros;
using Otus.Infraestrutura.Views;
using Otus.Modulo.AlteracoesPrecoAgendadas.Models;
using Otus.Modulo.AlteracoesPrecoAgendadas.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Modulo.AlteracoesPrecoAgendadas")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Modulo.AlteracoesPrecoAgendadas")]
[assembly: AssemblyCopyright("Copyright Digisat")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AlteracoesPrecoAgendadas
{
	[Module(ModuleName = "AlteracaoPrecoAgendadaModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class AlteracaoPrecoAgendadaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AlteracaoPrecoAgendadaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AlteracoesPrecoAgendadasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AlteracaoPrecoAgendadaFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AgendamentoFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AlteracoesPrecoAgendadas.Views
{
	public class AgendamentoFichaView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public AgendamentoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/agendamentofichaview.xaml", UriKind.Relative);
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
	public class AlteracaoPrecoAgendadaFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AlteracaoPrecoAgendadaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/alteracaoprecoagendadafichaview.xaml", UriKind.Relative);
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
	public class AlteracoesPrecoAgendadasView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AlteracoesPrecoAgendadasView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/alteracoesprecoagendadasview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AlteracoesPrecoAgendadas.Views.AlteracaoPrecoAgendadaFichaViewDetalhes
{
	public class HistoricoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public HistoricoView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/alteracaoprecoagendadafichaviewdetalhes/historicoview.xaml", UriKind.Relative);
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
	public class PendentesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PendentesView()
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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/alteracaoprecoagendadafichaviewdetalhes/pendentesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AlteracoesPrecoAgendadas.Views.AgendamentoFichaViewDetalhes
{
	public class AgendamentoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParametroFiltro;

		internal TextEdit InformacaoAPesquisar;

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
				Uri resourceLocator = new Uri("/Modulo.AlteracoesPrecoAgendadas;component/views/agendamentofichaviewdetalhes/agendamentoview.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParametroFiltro = (ButtonInfo)target;
				break;
			case 2:
				InformacaoAPesquisar = (TextEdit)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.AlteracoesPrecoAgendadas.ViewModels
{
	public class AgendamentoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public AgendamentoAbaViewModel(IViewModelDtoBase viewModel)
			: base("ITENS", "Produtos e novos preços", viewModel)
		{
		}
	}
	public class AgendamentoFichaViewModel : FichaBasePersistir<AlteracaoPrecoAgendadaDto, AgendamentoViewModel>
	{
		private const string Origem = "Agendamento manual";

		private readonly IWService _service;

		public AgendamentoFichaViewModel(IWService service)
			: base("Agendar alteração de preço")
		{
			_service = service;
		}

		protected override void CriarFormulario()
		{
			((FichaBase<AlteracaoPrecoAgendadaDto, AgendamentoViewModel>)(object)this).Formulario = (IFormulario)(object)new AgendamentoFormulario(_service);
		}

		protected override async Task Gravar()
		{
			try
			{
				List<AlteracaoPrecoAgendadaDto> source = HelperObject.To<AgendamentoFormulario>((object)((FichaBase<AlteracaoPrecoAgendadaDto, AgendamentoViewModel>)(object)this).Formulario).ViewModel.ObterAlteracoes().ToList();
				List<string> precosReferencia = source.Select((AlteracaoPrecoAgendadaDto x) => x.PrecoReferencia).ToList();
				List<AlteracaoPrecoAgendadaDto> list = HelperIEnumerable.ToListEmptyWhenNull<AlteracaoPrecoAgendadaDto>(await _service.Canal<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>((Func<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>)((IAlteracaoPrecoAgendadaModuleService x) => x.ObterPendentesPorPrecos((IEnumerable<string>)precosReferencia))));
				if (list.Any() && DialogoFactory.MostrarDialogoSimNao(MensagemSubstituicao(list), false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				return;
			}
			await base.Gravar();
		}

		private static string MensagemSubstituicao(IList<AlteracaoPrecoAgendadaDto> pendentes)
		{
			List<string> list = (from x in pendentes.Take(10)
				select x.ToTextPendente).ToList();
			if (pendentes.Count > 10)
			{
				list.Add($"... e mais {pendentes.Count - 10} produto(s)");
			}
			return "Os produtos abaixo já possuem alteração de preço agendada, que será substituída:" + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, list) + Environment.NewLine + Environment.NewLine + "Deseja continuar?";
		}

		protected override async Task<AlteracaoPrecoAgendadaDto> Persistir(IFormularioEntity formulario)
		{
			List<AlteracaoPrecoAgendadaDto> alteracoes = HelperObject.To<AgendamentoFormulario>((object)((FichaBase<AlteracaoPrecoAgendadaDto, AgendamentoViewModel>)(object)this).Formulario).ViewModel.ObterAlteracoes().ToList();
			await Task.Run(delegate
			{
				_service.Canal<IAlteracaoPrecoAgendadaModuleService>((Action<IAlteracaoPrecoAgendadaModuleService>)delegate(IAlteracaoPrecoAgendadaModuleService x)
				{
					x.Agendar((IEnumerable<AlteracaoPrecoAgendadaDto>)alteracoes, "Agendamento manual");
				});
			});
			return new AlteracaoPrecoAgendadaDto();
		}
	}
	public class AgendamentoFormulario : Formulario
	{
		public readonly AgendamentoViewModel ViewModel;

		public AgendamentoFormulario(IWService service)
		{
			ViewModel = new AgendamentoViewModel(service);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new AgendamentoAbaViewModel((IViewModelDtoBase)(object)ViewModel) });
		}
	}
	public class AgendamentoViewModel : ViewModelDtoBase<List<ItemAgendamento>>
	{
		private readonly IWService _service;

		private static bool _desconto;

		public FiltroComboBoxViewModel Filtro
		{
			get
			{
				return ((BindableBase)this).GetProperty<FiltroComboBoxViewModel>((Expression<Func<FiltroComboBoxViewModel>>)(() => Filtro));
			}
			set
			{
				((BindableBase)this).SetProperty<FiltroComboBoxViewModel>((Expression<Func<FiltroComboBoxViewModel>>)(() => Filtro), value);
			}
		}

		public Lista<KeyValuePair<string, bool>> TiposAlteracao
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, bool>>>((Expression<Func<Lista<KeyValuePair<string, bool>>>>)(() => TiposAlteracao));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, bool>>>((Expression<Func<Lista<KeyValuePair<string, bool>>>>)(() => TiposAlteracao), value);
			}
		}

		public Lista<KeyValuePair<string, BaseCalculoTabelaPreco>> BasesCalculoTabelaPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<KeyValuePair<string, BaseCalculoTabelaPreco>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoTabelaPreco>>>>)(() => BasesCalculoTabelaPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<KeyValuePair<string, BaseCalculoTabelaPreco>>>((Expression<Func<Lista<KeyValuePair<string, BaseCalculoTabelaPreco>>>>)(() => BasesCalculoTabelaPreco), value);
			}
		}

		[CustomValidation(typeof(AgendamentoViewModel), "ValidarPercentual", ErrorMessage = "O campo 'Percentual' é inválido.")]
		public decimal Percentual
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Percentual));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Percentual), value);
			}
		}

		public DateTime InicioVigencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => InicioVigencia));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => InicioVigencia), value, (Action)AplicarInicioVigencia);
			}
		}

		public string InformacaoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa), value, (Action)OnInformacaoPesquisa);
			}
		}

		public ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>((Expression<Func<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>((Expression<Func<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>>)(() => Itens), value);
			}
		}

		public override bool Valido
		{
			get
			{
				if (base.Dto.Any())
				{
					return base.Dto.All((ItemAgendamento x) => x.PrecoNovo > 0m && x.InicioVigencia > DateTime.Now);
				}
				return false;
			}
		}

		public override string[] Pendencies
		{
			get
			{
				if (!base.Dto.Any())
				{
					return new string[1] { "Nenhum item adicionado." };
				}
				List<string> list = new List<string>();
				if (base.Dto.Any((ItemAgendamento x) => x.PrecoNovo <= 0m))
				{
					list.Add("Há itens com o 'Preço novo' zerado.");
				}
				if (base.Dto.Any((ItemAgendamento x) => x.InicioVigencia <= DateTime.Now))
				{
					list.Add("Há itens com o 'Início vigência' anterior à data e hora atual.");
				}
				return list.ToArray();
			}
		}

		public ICommand LimparCommand { get; set; }

		public ICommand PesquisarCommand { get; set; }

		public ICommand AdicionarItensCommand { get; set; }

		public ICommand RemoverItensCommand { get; set; }

		public AgendamentoViewModel(IWService service)
			: base(new List<ItemAgendamento>())
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Expected O, but got Unknown
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Expected O, but got Unknown
			_service = service;
			AdicionarItensCommand = (ICommand)new AsyncCommand((Func<Task>)AdicionarItens, (Func<bool>)PodeAdicionarItens, (bool?)null);
			RemoverItensCommand = (ICommand)new AsyncCommand((Func<Task>)RemoverItens, (Func<bool>)PodeRemoverItens, (bool?)null);
			LimparCommand = (ICommand)new DelegateCommand((Action)Limpar);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar, (Func<bool>)PodePesquisar, (bool?)null);
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarPercentual(object value, ValidationContext context)
		{
			if (_desconto)
			{
				if (!((decimal)value >= 0m) || !((decimal)value < 100m))
				{
					return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
				}
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			if (!((decimal)value >= 0m))
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		private void AplicarInicioVigencia()
		{
			foreach (ItemAgendamento item in base.Dto)
			{
				item.InicioVigencia = InicioVigencia;
			}
			foreach (ItemAgendamentoViewModel datum in ((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Data)
			{
				datum.InicioVigencia = InicioVigencia;
			}
		}

		private bool PodeAdicionarItens()
		{
			return Filtro.Filtros.Itens.Select((Retorno f) => ((Retorno)(ref f)).Referencia).Any();
		}

		private async Task AdicionarItens()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Calculando...");
				List<ItemTabelaPrecoPesquisaDto> resultado = default(List<ItemTabelaPrecoPesquisaDto>);
				_ = resultado;
				resultado = HelperIEnumerable.ToListEmptyWhenNull<ItemTabelaPrecoPesquisaDto>(await PesquisarItensFiltro());
				LoaderProcessoService.EsconderLoader();
				bool flag = true;
				if (((ViewModelDtoBase<List<ItemAgendamento>>)this).Dto.Any((ItemAgendamento x) => resultado.Any((ItemTabelaPrecoPesquisaDto y) => y.PrecoReferencia == x.PrecoReferencia)))
				{
					flag = DialogoFactory.MostrarDialogoSimNao("Substituir o item caso ele já exista na lista?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes;
				}
				foreach (ItemTabelaPrecoPesquisaDto item in resultado)
				{
					ItemAgendamento itemAgendamento = ((ViewModelDtoBase<List<ItemAgendamento>>)this).Dto.FirstOrDefault((ItemAgendamento x) => x.PrecoReferencia == item.PrecoReferencia);
					if (!HelperObject.IsNotNull((object)itemAgendamento) || flag)
					{
						if (HelperObject.IsNotNull((object)itemAgendamento))
						{
							((ViewModelDtoBase<List<ItemAgendamento>>)this).Dto.Remove(itemAgendamento);
						}
						((ViewModelDtoBase<List<ItemAgendamento>>)this).Dto.Add(new ItemAgendamento
						{
							PrecoReferencia = item.PrecoReferencia,
							ProdutoServicoCodigoInterno = item.ProdutoServicoCodigoInterno,
							ProdutoServicoDescricao = item.ProdutoServicoDescricao,
							PrecoCusto = item.PrecoCusto,
							CustoMedio = item.CustoMedio,
							PrecoAtual = item.PrecoVenda,
							PrecoNovo = item.Preco,
							InicioVigencia = InicioVigencia
						});
					}
				}
				Limpar();
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

		private bool PodeRemoverItens()
		{
			if (Filtro.Filtros.Itens.Select((Retorno f) => ((Retorno)(ref f)).Referencia).Any())
			{
				return base.Dto.Any();
			}
			return false;
		}

		private async Task RemoverItens()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Calculando...");
				List<ItemTabelaPrecoPesquisaDto> resultado = default(List<ItemTabelaPrecoPesquisaDto>);
				_ = resultado;
				resultado = HelperIEnumerable.ToListEmptyWhenNull<ItemTabelaPrecoPesquisaDto>(await PesquisarItensFiltro());
				((ViewModelDtoBase<List<ItemAgendamento>>)this).Dto.RemoveAll((ItemAgendamento x) => resultado.Any((ItemTabelaPrecoPesquisaDto y) => y.PrecoReferencia == x.PrecoReferencia));
				Limpar();
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

		private async Task<IEnumerable<ItemTabelaPrecoPesquisaDto>> PesquisarItensFiltro()
		{
			TipoOperacaoDto tipoOperacao = new TipoOperacaoDto
			{
				Desconto = TiposAlteracao.ItemSelecionado.Value,
				Percentual = Percentual,
				BaseCalculoTabelaPreco = BasesCalculoTabelaPreco.ItemSelecionado.Value
			};
			return await _service.Canal<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<ItemTabelaPrecoPesquisaDto>>>((Func<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<ItemTabelaPrecoPesquisaDto>>>)((IAlteracaoPrecoAgendadaModuleService x) => x.PesquisarItens(new FiltroTabelaPrecoDto
			{
				CodigoFiltro = ((Lista<IFiltroViewModel>)(object)Filtro).ItemSelecionado.Codigo,
				ReferenciaFiltro = Filtro.Filtros.Itens.Select((Retorno f) => ((Retorno)(ref f)).Referencia),
				Percentual = Percentual,
				TabelaPrecoDto = new TabelaPrecoDto
				{
					TipoOperacao = tipoOperacao
				}
			})));
		}

		public virtual void Limpar()
		{
			InformacaoPesquisa = string.Empty;
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Limpar();
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Adicionar<ItemAgendamento>((IEnumerable<ItemAgendamento>)base.Dto);
			((BindableBase)this).RaisePropertyChanged<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>((Expression<Func<ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>>>)(() => Itens));
		}

		private bool PodePesquisar()
		{
			return base.Dto.Any();
		}

		private void Pesquisar()
		{
			if (string.IsNullOrEmpty(InformacaoPesquisa))
			{
				return;
			}
			string informacao = HelperString.RemoveDiacritics(InformacaoPesquisa).ToLower();
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Limpar();
			List<ItemAgendamento> list = base.Dto.Where((ItemAgendamento i) => HelperString.RemoveDiacritics(i.ProdutoServicoCodigoInterno).ToLower().Equals(informacao)).ToList();
			if (list.Any())
			{
				((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Adicionar<ItemAgendamento>((IEnumerable<ItemAgendamento>)list);
				return;
			}
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Adicionar<ItemAgendamento>((IEnumerable<ItemAgendamento>)base.Dto.Where((ItemAgendamento i) => HelperString.RemoveDiacritics(i.ProdutoServicoDescricao).ToLower().Contains(informacao)).ToList());
		}

		private void OnInformacaoPesquisa()
		{
			if (string.IsNullOrEmpty(InformacaoPesquisa))
			{
				Limpar();
			}
		}

		public IEnumerable<AlteracaoPrecoAgendadaDto> ObterAlteracoes()
		{
			return ((IEnumerable<ItemAgendamento>)base.Dto).Select((Func<ItemAgendamento, AlteracaoPrecoAgendadaDto>)((ItemAgendamento x) => new AlteracaoPrecoAgendadaDto
			{
				PrecoReferencia = x.PrecoReferencia,
				ProdutoServicoCodigoInterno = x.ProdutoServicoCodigoInterno,
				ProdutoServicoDescricao = x.ProdutoServicoDescricao,
				PrecoAtual = x.PrecoAtual,
				PrecoNovo = x.PrecoNovo,
				InicioVigencia = x.InicioVigencia
			})).ToList();
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			Filtro = new FiltroComboBoxViewModel(false, false);
			TiposAlteracao = new Lista<KeyValuePair<string, bool>>();
			TiposAlteracao.Adicionar((IEnumerable<KeyValuePair<string, bool>>)new List<KeyValuePair<string, bool>>
			{
				new KeyValuePair<string, bool>("Acréscimo", value: false),
				new KeyValuePair<string, bool>("Desconto", value: true)
			});
			TiposAlteracao.PosicionarNoPrimeiro();
			_desconto = TiposAlteracao.ItemSelecionado.Value;
			TiposAlteracao.AlterouItemSelecionadoEvent += TiposAlteracaoOnAlterouItemSelecionadoEvent;
			BasesCalculoTabelaPreco = new Lista<KeyValuePair<string, BaseCalculoTabelaPreco>>();
			BasesCalculoTabelaPreco.Adicionar((IEnumerable<KeyValuePair<string, BaseCalculoTabelaPreco>>)HelperEnum.ObterParDeValores<BaseCalculoTabelaPreco>());
			BasesCalculoTabelaPreco.Posicionar((Expression<Func<KeyValuePair<string, BaseCalculoTabelaPreco>, bool>>)((KeyValuePair<string, BaseCalculoTabelaPreco> x) => (int)x.Value == 0));
			Itens = new ItensPaginacao<ItemAgendamento, ItemAgendamentoViewModel>(short.MaxValue);
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).AdicionouItemEvent += ItensOnAdicionouItemEvent;
			InicioVigencia = DateTime.Today.AddDays(1.0);
		}

		private void TiposAlteracaoOnAlterouItemSelecionadoEvent(object sender, KeyValuePair<string, bool> e)
		{
			_desconto = e.Value;
		}

		private void ItensOnAdicionouItemEvent(object sender, ItemAgendamentoViewModel itemAgendamentoViewModel)
		{
			itemAgendamentoViewModel.RemoverEvent += delegate(object o, ItemAgendamento item)
			{
				base.Dto.Remove(item);
				Limpar();
			};
		}

		public override void Dispose()
		{
			TiposAlteracao.AlterouItemSelecionadoEvent -= TiposAlteracaoOnAlterouItemSelecionadoEvent;
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).AdicionouItemEvent -= ItensOnAdicionouItemEvent;
			((Itens<ItemAgendamento, ItemAgendamentoViewModel>)(object)Itens).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class AlteracaoPrecoAgendadaFichaViewModel : FichaBase<AlteracaoPrecoAgendadaDto, PendentesViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<AlteracaoPrecoAgendadaDto>> _pendentes;

		private Task<IEnumerable<AlteracaoPrecoAgendadaDto>> _historico;

		public ICommand CancelarCommand
		{
			get
			{
				return ((BindableBase)this).GetProperty<ICommand>((Expression<Func<ICommand>>)(() => CancelarCommand));
			}
			set
			{
				((BindableBase)this).SetProperty<ICommand>((Expression<Func<ICommand>>)(() => CancelarCommand), value);
			}
		}

		public AlteracaoPrecoAgendadaFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService)
			: base(string.Empty)
		{
			_service = service;
			_executarProcessoService = executarProcessoService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				ObterPendentes();
				return;
			}
			try
			{
				AlteracaoPrecoAgendadaFormulario alteracaoPrecoAgendadaFormulario = new AlteracaoPrecoAgendadaFormulario(ObterPendentes, ObterHistorico, Agendar, Cancelar);
				CancelarCommand = (ICommand)new DelegateCommand<object>((Action<object>)alteracaoPrecoAgendadaFormulario.Cancelar, (Func<object, bool>)alteracaoPrecoAgendadaFormulario.PodeCancelar, (bool?)null);
				base.Formulario = (IFormulario)(object)alteracaoPrecoAgendadaFormulario;
				ObterPendentes();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
		}

		public void ObterPendentes()
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>((Func<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>)((IAlteracaoPrecoAgendadaModuleService service) => _pendentes = service.PesquisarPendentes()));
			}, (Action<object>)RetornoPendentes);
		}

		private void RetornoPendentes(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (HelperObject.IsNotNull((object)_pendentes))
				{
					HelperObject.To<AlteracaoPrecoAgendadaFormulario>((object)base.Formulario).SetarPendentes(_pendentes.Result);
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void ObterHistorico(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>((Func<IAlteracaoPrecoAgendadaModuleService, Task<IEnumerable<AlteracaoPrecoAgendadaDto>>>)((IAlteracaoPrecoAgendadaModuleService service) => _historico = service.PesquisarHistorico(dataInicial, dataFinal)));
			}, (Action<object>)RetornoHistorico);
		}

		private void RetornoHistorico(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (HelperObject.IsNotNull((object)_historico))
				{
					HelperObject.To<AlteracaoPrecoAgendadaFormulario>((object)base.Formulario).SetarHistorico(_historico.Result);
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void Agendar()
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			((NavigationBase)this).Navegar("AgendamentoFichaView", (Action<NavigationResult>)delegate
			{
			}, new NavigationParameters());
		}

		public void Cancelar(IEnumerable<AlteracaoPrecoAgendadaDto> alteracoes)
		{
			List<string> ids = alteracoes.Select((AlteracaoPrecoAgendadaDto x) => x.Id).ToList();
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IAlteracaoPrecoAgendadaModuleService>((Action<IAlteracaoPrecoAgendadaModuleService>)delegate(IAlteracaoPrecoAgendadaModuleService service)
				{
					service.Cancelar((IEnumerable<string>)ids);
				});
			}, (Action<object>)RetornoCancelar);
		}

		private void RetornoCancelar(object obj)
		{
			if (obj is Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.InnerException?.Message ?? ex.Message, false, HorizontalAlignment.Center);
			}
			else
			{
				ObterPendentes();
			}
		}
	}
	public class AlteracaoPrecoAgendadaFormulario : Formulario
	{
		private readonly Action<IEnumerable<AlteracaoPrecoAgendadaDto>> _cancelar;

		private PendentesViewModel _pendentesViewModel;

		private PendentesAbaViewModel _pendentesAbaViewModel;

		private HistoricoViewModel _historicoViewModel;

		private HistoricoAbaViewModel _historicoAbaViewModel;

		public AlteracaoPrecoAgendadaFormulario(Action obterPendentes, Action<DateTime, DateTime> obterHistorico, Action agendar, Action<IEnumerable<AlteracaoPrecoAgendadaDto>> cancelar)
		{
			AlteracaoPrecoAgendadaFormulario alteracaoPrecoAgendadaFormulario = this;
			_cancelar = cancelar;
			ThreadPrincipal.Executar((Action)delegate
			{
				alteracaoPrecoAgendadaFormulario.ProjetarAbas(obterPendentes, obterHistorico, agendar);
			});
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		public bool PodeCancelar(object obj)
		{
			if (HelperObject.IsNotNull(obj))
			{
				return HelperObject.Is<PendentesViewModel>((object)((Formulario)this).ViewModelAbaSelecionada);
			}
			return false;
		}

		public void Cancelar(object obj)
		{
			List<AlteracaoPrecoAgendadaDto> list = HelperIEnumerable.ToListEmptyWhenNull<AlteracaoPrecoAgendadaDto>(_pendentesViewModel.RetornarItensSelecionados(obj));
			if (!list.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhuma alteração de preço selecionada para cancelar", false, HorizontalAlignment.Center);
			}
			else if (DialogoFactory.MostrarDialogoSimNao($"Cancelar {list.Count} alteração(ões) de preço agendada(s)?", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				_cancelar(list);
			}
		}

		public void SetarPendentes(IEnumerable<AlteracaoPrecoAgendadaDto> itens)
		{
			_pendentesViewModel?.SetarResultado(itens);
		}

		public void SetarHistorico(IEnumerable<AlteracaoPrecoAgendadaDto> itens)
		{
			_historicoViewModel?.SetarResultado(itens);
		}

		private void ProjetarAbas(Action obterPendentes, Action<DateTime, DateTime> obterHistorico, Action agendar)
		{
			_pendentesViewModel = new PendentesViewModel(obterPendentes, agendar);
			_pendentesAbaViewModel = new PendentesAbaViewModel((IViewModelDtoBase)(object)_pendentesViewModel);
			_historicoViewModel = new HistoricoViewModel(obterHistorico);
			_historicoAbaViewModel = new HistoricoAbaViewModel((IViewModelDtoBase)(object)_historicoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)_pendentesAbaViewModel,
				(IAbaViewModelBase)_historicoAbaViewModel
			});
		}
	}
	public class AlteracoesPrecoAgendadasViewModel : ModuloBase
	{
		public AlteracoesPrecoAgendadasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAlteracoesPrecoAgendadasRegion, "AlteracaoPrecoAgendadaFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class HistoricoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public HistoricoAbaViewModel(IViewModelDtoBase viewModel)
			: base("HISTÓRICO", "Alterações aplicadas e canceladas", viewModel)
		{
		}
	}
	public class HistoricoViewModel : ViewModelDtoBase<IEnumerable<AlteracaoPrecoAgendadaDto>>
	{
		private readonly Action<DateTime, DateTime> _obterHistorico;

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

		public IEnumerable<AlteracaoPrecoAgendadaDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<AlteracaoPrecoAgendadaDto>>((Expression<Func<IEnumerable<AlteracaoPrecoAgendadaDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<AlteracaoPrecoAgendadaDto>>((Expression<Func<IEnumerable<AlteracaoPrecoAgendadaDto>>>)(() => Itens), value);
			}
		}

		public ICommand PesquisarCommand { get; set; }

		public HistoricoViewModel()
		{
		}

		public HistoricoViewModel(Action<DateTime, DateTime> obterHistorico)
			: base((IEnumerable<AlteracaoPrecoAgendadaDto>)null)
		{
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Expected O, but got Unknown
			_obterHistorico = obterHistorico;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
		}

		private void Pesquisar()
		{
			_obterHistorico(DataInicial, DataFinal);
		}

		public void SetarResultado(IEnumerable<AlteracaoPrecoAgendadaDto> itens)
		{
			Itens = itens;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class ItemAgendamentoViewModel : ViewModelDtoBase<ItemAgendamento>
	{
		[NaoPermitirValorMenorIgualZero(ErrorMessage = "O 'Preço novo' deve ser maior que zero.")]
		public decimal PrecoNovo
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoNovo));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoNovo), value, (Action)delegate
				{
					base.Dto.PrecoNovo = PrecoNovo;
				});
			}
		}

		[CustomValidation(typeof(ItemAgendamentoViewModel), "ValidarInicioVigencia", ErrorMessage = "O 'Início vigência' deve ser posterior à data e hora atual.")]
		public DateTime InicioVigencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => InicioVigencia));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => InicioVigencia), value, (Action)delegate
				{
					base.Dto.InicioVigencia = InicioVigencia;
				});
			}
		}

		public string PrecoReferencia => base.Dto.PrecoReferencia;

		public string ProdutoServicoCodigoInterno => base.Dto.ProdutoServicoCodigoInterno;

		public string ProdutoServicoDescricao => base.Dto.ProdutoServicoDescricao;

		public decimal PrecoCusto => base.Dto.PrecoCusto;

		public decimal CustoMedio => base.Dto.CustoMedio;

		public decimal PrecoAtual => base.Dto.PrecoAtual;

		public ICommand RemoverCommand { get; set; }

		public event EventHandler<ItemAgendamento> RemoverEvent;

		public ItemAgendamentoViewModel()
			: base(new ItemAgendamento())
		{
		}

		public ItemAgendamentoViewModel(ItemAgendamento item)
			: base(item)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			RemoverCommand = (ICommand)new DelegateCommand((Action)OnRemoverEvent);
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarInicioVigencia(object value, ValidationContext context)
		{
			if (!((DateTime)value > DateTime.Now))
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		public override void ProjectedAsViewModel()
		{
			PrecoNovo = base.Dto.PrecoNovo;
			InicioVigencia = base.Dto.InicioVigencia;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
		}

		protected virtual void OnRemoverEvent()
		{
			this.RemoverEvent?.Invoke(this, base.Dto);
		}
	}
	public class PendentesAbaViewModel : AbaAuxiliarViewModelBase
	{
		public PendentesAbaViewModel(IViewModelDtoBase viewModel)
			: base("PENDENTES", "Alterações que ainda serão aplicadas", viewModel)
		{
		}
	}
	public class PendentesViewModel : ViewModelDtoBase<IEnumerable<AlteracaoPrecoAgendadaDto>>
	{
		private readonly Action _obterPendentes;

		private readonly Action _agendar;

		public IEnumerable<AlteracaoPrecoAgendadaDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<AlteracaoPrecoAgendadaDto>>((Expression<Func<IEnumerable<AlteracaoPrecoAgendadaDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<AlteracaoPrecoAgendadaDto>>((Expression<Func<IEnumerable<AlteracaoPrecoAgendadaDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoPendentes";

		public ICommand AtualizarCommand { get; set; }

		public ICommand AgendarCommand { get; set; }

		public PendentesViewModel()
		{
		}

		public PendentesViewModel(Action obterPendentes, Action agendar)
			: base((IEnumerable<AlteracaoPrecoAgendadaDto>)null)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			_obterPendentes = obterPendentes;
			_agendar = agendar;
			AtualizarCommand = (ICommand)new DelegateCommand((Action)Atualizar);
			AgendarCommand = (ICommand)new DelegateCommand((Action)Agendar);
		}

		private void Atualizar()
		{
			_obterPendentes();
		}

		private void Agendar()
		{
			_agendar();
		}

		public IEnumerable<AlteracaoPrecoAgendadaDto> RetornarItensSelecionados(object obj)
		{
			return ((DataControlBase)ObterGrid(obj as FrameworkElement)).SelectedItems.OfType<AlteracaoPrecoAgendadaDto>();
		}

		private GridControl ObterGrid(FrameworkElement view)
		{
			FrameworkElementVisitor<GridControl> val = new FrameworkElementVisitor<GridControl>(NomeGridControl);
			Visitor.Visit((object)view, (IVisitor)(object)val);
			if (HelperObject.IsNull((object)val.Property))
			{
				throw new Exception(NomeGridControl + " nao encontrado");
			}
			return val.Property;
		}

		public void SetarResultado(IEnumerable<AlteracaoPrecoAgendadaDto> itens)
		{
			Itens = itens;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.AlteracoesPrecoAgendadas.Models
{
	public class ItemAgendamento
	{
		public string PrecoReferencia { get; set; } = string.Empty;


		public string ProdutoServicoCodigoInterno { get; set; } = string.Empty;


		public string ProdutoServicoDescricao { get; set; } = string.Empty;


		public decimal PrecoCusto { get; set; }

		public decimal CustoMedio { get; set; }

		public decimal PrecoAtual { get; set; }

		public decimal PrecoNovo { get; set; }

		public DateTime InicioVigencia { get; set; }
	}
}

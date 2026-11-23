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
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Industrias.FichasTecnicas;
using DigisatServer.Application.Main.Dto.Industrias.SimuladoresCusto;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.IndustriasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Style.ListViews;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.AtualizacoesCustoMateriaPrima.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.CalculosCustoMateriaPrima")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.CalculosCustoMateriaPrima")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.AtualizacoesCustoMateriaPrima
{
	[Module(ModuleName = "AtualizacaoCustoMateriaPrimaModulo", OnDemand = true)]
	[ModuleDependency("ProdutoServicoModulo")]
	public class AtualizacaoCustoMateriaPrimaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public AtualizacaoCustoMateriaPrimaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<AtualizacoesCustoMateriaPrimaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<AtualizacaoCustoMateriaPrimaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.AtualizacoesCustoMateriaPrima.Views
{
	public class AtualizacaoCustoMateriaPrimaFichaView : UserControl, IComponentConnector
	{
		internal ButtonInfo PesquisarProduto;

		internal ListViewFocusable listaItens;

		internal ListView listaMateriasPrimas;

		private bool _contentLoaded;

		public AtualizacaoCustoMateriaPrimaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.AtualizacoesCustoMateriaPrima;component/views/atualizacaocustomateriaprimafichaview.xaml", UriKind.Relative);
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
			switch (connectionId)
			{
			case 1:
				PesquisarProduto = (ButtonInfo)target;
				break;
			case 2:
				listaItens = (ListViewFocusable)target;
				break;
			case 3:
				listaMateriasPrimas = (ListView)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class AtualizacoesCustoMateriaPrimaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AtualizacoesCustoMateriaPrimaView()
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
				Uri resourceLocator = new Uri("/Modulo.AtualizacoesCustoMateriaPrima;component/views/atualizacoescustomateriaprimaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.AtualizacoesCustoMateriaPrima.ViewModels
{
	public class AtualizacaoCustoMateriaPrimaFichaViewModel : NavigationBase
	{
		public Lista<ProdutoProduzidoViewModel> ProdutosProduzidos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ProdutoProduzidoViewModel>>((Expression<Func<Lista<ProdutoProduzidoViewModel>>>)(() => ProdutosProduzidos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ProdutoProduzidoViewModel>>((Expression<Func<Lista<ProdutoProduzidoViewModel>>>)(() => ProdutosProduzidos), value);
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
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa), value);
			}
		}

		public ICommand PesquisarProdutoCommand { get; set; }

		public ICommand AtualizarPrecoCustoTodaCadeiaCommand { get; set; }

		public AtualizacaoCustoMateriaPrimaFichaViewModel()
			: base(string.Empty)
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			((NavigationBase)this).ParametroInformado += OnParametroInformado;
			PesquisarProdutoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto);
			AtualizarPrecoCustoTodaCadeiaCommand = (ICommand)new AsyncCommand((Func<Task>)AtualizarPrecoCustoTodaCadeia, (Func<bool>)PodeAtualizarPrecoCustoTodaCadeia, (bool?)null);
		}

		private void PesquisarProduto()
		{
			((NavigationBase)this).Navegar((object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)PreencherProdutoProduzido, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => (int)x.ProdutoServico.Tipo == 9), "Selecione um registro do tipo 'Produto produzido'!", HorizontalAlignment.Center)));
		}

		private void PreencherProdutoProduzido(ProdutoServicoEmpresaDto dto)
		{
			ProdutoServicoEmpresa = dto;
			IEnumerable<ProdutoServicoEmpresaDto> enumerable = DadosSistema.GetService().Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutosProduzidos(((EntityBaseDto)dto).Id.ToString()))).Reverse();
			ProdutosProduzidos = new Lista<ProdutoProduzidoViewModel>();
			foreach (ProdutoServicoEmpresaDto item in enumerable)
			{
				ProdutosProduzidos.Adicionar(ProdutoProduzidoViewModel.Init().ComProdutoServicoEmpresa(item).Criar());
			}
			ProdutosProduzidos.PosicionarNoPrimeiro();
		}

		private async Task AtualizarPrecoCustoTodaCadeia()
		{
			foreach (ProdutoProduzidoViewModel iten in ProdutosProduzidos.Itens)
			{
				await iten.AtualizarCusto();
			}
		}

		private bool PodeAtualizarPrecoCustoTodaCadeia()
		{
			return ProdutosProduzidos?.Itens?.Any() ?? false;
		}

		private void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			object obj = navigationParameters["ProdutoServicoEmpresaDto"];
			ProdutoServicoEmpresaDto val = (ProdutoServicoEmpresaDto)((obj is ProdutoServicoEmpresaDto) ? obj : null);
			if (HelperObject.IsNotNull((object)val))
			{
				PreencherProdutoProduzido(val);
			}
		}
	}
	public class AtualizacoesCustoMateriaPrimaViewModel : ModuloBase
	{
		public AtualizacoesCustoMateriaPrimaViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloAtualizacoesCustoMateriaPrimaRegion, typeof(AtualizacaoCustoMateriaPrimaFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class MateriaPrimaViewModel : BindableBase
	{
		public string Descricao { get; set; }

		public string UnidadeMedida { get; set; }

		public decimal CustoUnitario { get; set; }

		public decimal Quantidade { get; set; }

		public decimal CustoTotal { get; set; }

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

		public bool MostrarQuantidadeEmHora => UnidadeMedida?.Equals("H") ?? false;

		public MateriaPrimaViewModel()
		{
		}

		private MateriaPrimaViewModel(string descricao, string unidadeMedida, decimal custoUnitario, decimal quantidade, decimal custoTotal)
		{
			Descricao = descricao;
			UnidadeMedida = unidadeMedida;
			CustoUnitario = custoUnitario;
			Quantidade = quantidade;
			CustoTotal = custoTotal;
		}

		public static MateriaPrimaViewModel Init()
		{
			return new MateriaPrimaViewModel();
		}

		public MateriaPrimaViewModel ComDescricao(string descricao)
		{
			Descricao = descricao;
			return this;
		}

		public MateriaPrimaViewModel ComUnidadeMedida(string unidadeMedida)
		{
			UnidadeMedida = unidadeMedida;
			return this;
		}

		public MateriaPrimaViewModel ComQuantidade(decimal quantidade)
		{
			Quantidade = quantidade;
			return this;
		}

		public MateriaPrimaViewModel ComCustoUnitario(decimal custoUnitario)
		{
			CustoUnitario = custoUnitario;
			return this;
		}

		public MateriaPrimaViewModel ComCustoTotal(decimal custoTotal)
		{
			CustoTotal = custoTotal;
			return this;
		}

		public MateriaPrimaViewModel Criar()
		{
			return new MateriaPrimaViewModel(Descricao, UnidadeMedida, CustoUnitario, Quantidade, CustoTotal);
		}
	}
	public class ProdutoProduzidoViewModel : BindableBase
	{
		private SimuladorCustoDto _simuladorCustoDto;

		public ProdutoServicoEmpresaDto ProdutoServicoEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServicoEmpresa), value);
			}
		}

		public IEnumerable<MateriaPrimaViewModel> MateriasPrimas
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<MateriaPrimaViewModel>>((Expression<Func<IEnumerable<MateriaPrimaViewModel>>>)(() => MateriasPrimas));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<MateriaPrimaViewModel>>((Expression<Func<IEnumerable<MateriaPrimaViewModel>>>)(() => MateriasPrimas), value);
			}
		}

		public Visibility LoaderVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Visibility>((Expression<Func<Visibility>>)(() => LoaderVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<Visibility>((Expression<Func<Visibility>>)(() => LoaderVisivel), value);
			}
		}

		public ICommand AtualizarCustoCommand { get; set; }

		public ProdutoProduzidoViewModel()
		{
		}

		private ProdutoProduzidoViewModel(ProdutoServicoEmpresaDto produtoServicoEmpresa, IEnumerable<MateriaPrimaViewModel> materiasPrimas)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			ProdutoServicoEmpresa = produtoServicoEmpresa;
			MateriasPrimas = materiasPrimas;
			LoaderVisivel = Visibility.Collapsed;
			AtualizarCustoCommand = (ICommand)new AsyncCommand((Func<Task>)AtualizarCusto);
		}

		public async Task AtualizarCusto()
		{
			LoaderVisivel = Visibility.Visible;
			ProdutoServicoEmpresaDto dto = default(ProdutoServicoEmpresaDto);
			_ = dto;
			dto = await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => ((IReferenciaAtualizarService<ProdutoServicoEmpresaDto>)(object)x).AtualizarDtoAsync(ProdutoServicoEmpresa)));
			try
			{
				await ObterParametrosSimuladorCusto();
				ProdutoServicoEmpresaDto val = await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => x.AtualizarCustosMateriasPrimas(((EntityBaseDto)dto).Id)));
				if (!HelperObject.IsNull((object)_simuladorCustoDto))
				{
					SimuladorCustoDto simuladorCustoDto = _simuladorCustoDto;
					simuladorCustoDto.OutrosCustos = val.ProdutoEmpresa.FichaTecnica.OutrosCustos;
					simuladorCustoDto.PrevisaoProducao = val.ProdutoEmpresa.FichaTecnica.PrevisaoProducao;
					simuladorCustoDto.ValorCustoMateriaPrima = val.ProdutoEmpresa.FichaTecnica.ValorCustoMateriaPrima;
					simuladorCustoDto.TempoProducao = val.ProdutoEmpresa.FichaTecnica.TempoProducao;
					simuladorCustoDto.CustoTotalIncidencias = (dto.ProdutoEmpresa.FichaTecnica.Custos.Any() ? dto.ProdutoEmpresa.FichaTecnica.Custos.FirstOrDefault().TotalIncidencias : 0m);
					simuladorCustoDto.PercentualLucroLiquidoDesejado = (dto.ProdutoEmpresa.FichaTecnica.Custos.Any() ? dto.ProdutoEmpresa.FichaTecnica.Custos.FirstOrDefault().PercentualLucro : 0m);
					CalculosIndustriaService val2 = CalculosIndustriaService.Criar().ComParametros(simuladorCustoDto);
					decimal precoCusto = val2.CalculoSimuladorCusto.PrecoCusto;
					dto.Preco.Custo.Valor = precoCusto;
					dto.ProdutoEmpresa.FichaTecnica = val.ProdutoEmpresa.FichaTecnica;
					if (!simuladorCustoDto.Inconsistencia && !(dto.Preco.Venda.Valor == val2.CalculoSimuladorCusto.PrecoVenda))
					{
						if (DialogoFactory.MostrarDialogoSimNao($"Deseja atualizar o preço de venda de {HelperDecimal.FormatarCampoValor(dto.Preco.Venda.Valor, 2, 0, false)} para {HelperDecimal.FormatarCampoValor(val2.CalculoSimuladorCusto.PrecoVenda, 2, 0, false)}", false, 0, HorizontalAlignment.Center) == MessageBoxResult.Yes)
						{
							dto.Preco.Venda.Valor = val2.CalculoSimuladorCusto.PrecoVenda;
						}
						return;
					}
					return;
				}
			}
			finally
			{
				await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, Task<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService a) => ((IAtualizar<ProdutoServicoEmpresaDto>)(object)a).AtualizarAsync(dto))).ContinueWith(delegate(Task<ProdutoServicoEmpresaDto> task)
				{
					LoaderVisivel = Visibility.Collapsed;
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						ComProdutoServicoEmpresa(task.Result);
					}
				});
			}
		}

		private async Task ObterParametrosSimuladorCusto()
		{
			if (HelperObject.IsNull((object)DadosSistema.GetService()))
			{
				return;
			}
			await DadosSistema.GetService().Canal<ISimuladorCustoModuleService, Task<SimuladorCustoDto>>((Func<ISimuladorCustoModuleService, Task<SimuladorCustoDto>>)((ISimuladorCustoModuleService x) => x.ObterParaCadastro())).ContinueWith(delegate(Task<SimuladorCustoDto> task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					_simuladorCustoDto = task.Result;
				}
			});
		}

		public static ProdutoProduzidoViewModel Init()
		{
			return new ProdutoProduzidoViewModel();
		}

		public ProdutoProduzidoViewModel ComProdutoServicoEmpresa(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			ProdutoServicoEmpresa = produtoServicoEmpresa;
			MateriasPrimas = produtoServicoEmpresa.ProdutoEmpresa.FichaTecnica.EtapasProducao.SelectMany((EtapaProducaoDto x) => x.MateriasPrimas.Select((MateriaPrimaDto y) => MateriaPrimaViewModel.Init().ComDescricao(y.Descricao).ComUnidadeMedida(y.UnidadeMedida)
				.ComCustoUnitario(((MateriaPrimaCustoDto)y).PrecoCusto)
				.ComQuantidade(y.Quantidade)
				.ComCustoTotal(y.Total)
				.Criar()));
			return this;
		}

		public ProdutoProduzidoViewModel Criar()
		{
			return new ProdutoProduzidoViewModel(ProdutoServicoEmpresa, MateriasPrimas);
		}
	}
}
namespace Otus.Modulo.AtualizacoesCustoMateriaPrima.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
					resourceMan = new ResourceManager("Otus.Modulo.AtualizacoesCustoMateriaPrima.Properties.Resources", typeof(Resources).Assembly);
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

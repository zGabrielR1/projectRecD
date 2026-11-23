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
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GestaoCashback.Resources;
using Otus.Modulo.GestaoCashback.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoCashback")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoCashback")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoCashback
{
	[Module(ModuleName = "GestaoCashbackModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	public class GestaoCashbackModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoCashbackModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoCashbacksView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoCashbackFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoCashback.Views
{
	public class GestaoCashbacksView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoCashbacksView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoCashback;component/views/gestaocashbacksview.xaml", UriKind.Relative);
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
	public class GestaoCashbackFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoCashbackFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoCashback;component/views/gestaocashbackfichaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoCashback.Views.GestaoCashbackFichaViewDetalhes
{
	public class TransferenciaCashbackView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterPessoaOrigem;

		internal ButtonInfo ParameterPessoaDestino;

		private bool _contentLoaded;

		public TransferenciaCashbackView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoCashback;component/views/gestaocashbackfichaviewdetalhes/transferenciacashbackview.xaml", UriKind.Relative);
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
				ParameterPessoaOrigem = (ButtonInfo)target;
				break;
			case 2:
				ParameterPessoaDestino = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class DebitoCashbackView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DebitoCashbackView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoCashback;component/views/gestaocashbackfichaviewdetalhes/debitocashbackview.xaml", UriKind.Relative);
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
	public class CreditoCashbackView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CreditoCashbackView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoCashback;component/views/gestaocashbackfichaviewdetalhes/creditocashbackview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoCashback.ViewModels
{
	public class TransferenciaCashbackViewModel : ViewModelDtoBase<TransferenciaCashbackDto>
	{
		private readonly Action<TransferenciaCashbackDto> _transferirCashback;

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => HelperObject.IsExactlyObject<ClienteDto>((object)pessoaDto.Cliente) || pessoaDto.ConsumidorReservadoSistema), Messages.CadastroTipoCliente, (TipoPessoaCadastro)0, HorizontalAlignment.Center);

		public ICommand PesquisarPessoaOrigemCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoaOrigem);

		public ICommand PesquisarPessoaDestinoCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoaDestino);

		public PessoaDto PessoaOrigem
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaOrigem));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaOrigem), value, (Action)delegate
				{
					NomePessoaOrigem = ObterNome(PessoaOrigem);
				});
			}
		}

		public string NomePessoaOrigem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoaOrigem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoaOrigem), value, (Action)delegate
				{
					//IL_0028: Unknown result type (might be due to invalid IL or missing references)
					//IL_0032: Expected O, but got Unknown
					if (HelperString.IsNullOrEmpty(NomePessoaOrigem) && HelperObject.IsNotNull((object)PessoaOrigem) && HelperObject.IsNot<PessoaDtoNull>((object)PessoaOrigem))
					{
						PessoaOrigem = (PessoaDto)new PessoaDtoNull();
					}
				});
			}
		}

		public PessoaDto PessoaDestino
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaDestino));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => PessoaDestino), value, (Action)delegate
				{
					NomePessoaDestino = ObterNome(PessoaDestino);
				});
			}
		}

		public string NomePessoaDestino
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoaDestino));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoaDestino), value, (Action)delegate
				{
					//IL_0028: Unknown result type (might be due to invalid IL or missing references)
					//IL_0032: Expected O, but got Unknown
					if (HelperString.IsNullOrEmpty(NomePessoaDestino) && HelperObject.IsNotNull((object)PessoaDestino) && HelperObject.IsNot<PessoaDtoNull>((object)PessoaDestino))
					{
						PessoaDestino = (PessoaDto)new PessoaDtoNull();
					}
				});
			}
		}

		public string TotalCashbackPessoaOrigem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TotalCashbackPessoaOrigem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TotalCashbackPessoaOrigem), value);
			}
		}

		[NaoPermitirValorMenorIgualZero]
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

		public bool FocoNaPessoaOrigem
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoaOrigem));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoaOrigem), value);
			}
		}

		public ISelecaoCommandParameter PessoaOrigemCommandParameter { get; set; }

		public ISelecaoCommandParameter PessoaDestinoCommandParameter { get; set; }

		public ICommand ExecutarCommand { get; set; }

		public override bool Valido => true;

		public override string[] Pendencies => new string[0];

		public TransferenciaCashbackViewModel()
		{
		}

		public TransferenciaCashbackViewModel(Action<TransferenciaCashbackDto> transferirCashback)
			: base((TransferenciaCashbackDto)null)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			_transferirCashback = transferirCashback;
			ExecutarCommand = (ICommand)new DelegateCommand((Action)TransferirCashback);
			PessoaOrigemCommandParameter = (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoaOrigem, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);
			PessoaDestinoCommandParameter = (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoaDto)
			{
				PessoaDestino = pessoaDto;
			}, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);
		}

		private void SelecionouPessoaOrigem(PessoaDto pessoaDto)
		{
			PessoaOrigem = pessoaDto;
			TotalCashbackPessoaOrigem = $"Total de cashback: {HelperDecimal.FormatarCampoValor(PessoaOrigem.Cliente.TotalCashback, 2, 0, false)}";
			Valor = PessoaOrigem.Cliente.TotalCashback;
		}

		private async void PesquisarPessoaOrigem(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoaOrigem, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouPessoaOrigem, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private async void PesquisarPessoaDestino(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoaDestino, (TipoPessoaCadastro)0, (Action<PessoaDto>)delegate(PessoaDto pessoaDto)
			{
				PessoaDestino = pessoaDto;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void TransferirCashback()
		{
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Expected O, but got Unknown
			if (HelperObject.IsNull((object)PessoaOrigem) || HelperObject.IsExactlyObject<PessoaDtoNull>((object)PessoaOrigem) || PessoaOrigem.ConsumidorReservadoSistema)
			{
				DialogoFactory.MostrarDialogoConfirma("Pessoa de origem deve ser indicada", false, HorizontalAlignment.Center);
			}
			else if (HelperObject.IsNull((object)PessoaDestino) || HelperObject.IsExactlyObject<PessoaDtoNull>((object)PessoaDestino) || PessoaDestino.ConsumidorReservadoSistema)
			{
				DialogoFactory.MostrarDialogoConfirma("Pessoa de destino deve ser indicada", false, HorizontalAlignment.Center);
			}
			else if (Valor <= 0m || Valor > PessoaOrigem.Cliente.TotalCashback)
			{
				DialogoFactory.MostrarDialogoConfirma("Valor inválido", false, HorizontalAlignment.Center);
			}
			else if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<GestaoCashback, PermitirTransferirCashback>(0m, (string)null) != null)
			{
				_transferirCashback(new TransferenciaCashbackDto
				{
					PessoaOrigemReferencia = PessoaOrigem,
					PessoaDestinoReferencia = PessoaDestino,
					Valor = Valor
				});
				LimparPropriedades();
			}
		}

		private void LimparPropriedades()
		{
			PessoaOrigem = null;
			PessoaDestino = null;
			TotalCashbackPessoaOrigem = "Total de cashback: R$ 0,00";
			Valor = 0m;
			FocoNaPessoaOrigem = !FocoNaPessoaOrigem;
		}

		private static string ObterNome(PessoaDto pessoa)
		{
			if (!HelperObject.IsNull((object)pessoa) && !HelperObject.Is<PessoaDtoNull>((object)pessoa))
			{
				return pessoa.Nome;
			}
			return string.Empty;
		}

		public override void ProjectedAsViewModel()
		{
			TotalCashbackPessoaOrigem = "Total de cashback: R$ 0,00";
		}
	}
	public class TransferenciaCashbackAbaViewModel : AbaAuxiliarViewModelBase
	{
		public TransferenciaCashbackAbaViewModel(IViewModelDtoBase viewModel)
			: base("TRANSFERÊNCIA", "Transferir cashback", viewModel)
		{
		}
	}
	public class DebitoCashbackAbaViewModel : AbaAuxiliarViewModelBase
	{
		public DebitoCashbackAbaViewModel(IViewModelDtoBase viewModel)
			: base("DÉBITOS GERADOS", "Resumo dos débitos", viewModel)
		{
		}
	}
	public class DebitoCashbackViewModel : ViewModelDtoBase<IEnumerable<ItemCreditoDebitoCashbackDto>>, IViewModelImprimirGrid, IViewModelImprimir
	{
		private readonly Action<DateTime, DateTime> _obterDebitosCashback;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

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

		public IEnumerable<ItemCreditoDebitoCashbackDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ItemCreditoDebitoCashbackDto>>((Expression<Func<IEnumerable<ItemCreditoDebitoCashbackDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ItemCreditoDebitoCashbackDto>>((Expression<Func<IEnumerable<ItemCreditoDebitoCashbackDto>>>)(() => Itens), value);
			}
		}

		public string NomeGridControl => "SelecaoDebitosCashback";

		public string TituloRelatorio => "Débitos de cashback";

		public string SubTituloRelatorio => $"Data inicial: {DataInicial:dd/MM/yyyy}  -  Data final: {DataFinal:dd/MM/yyyy}";

		public bool Retrato => false;

		public string TagProperty => "SelecaoDebitosCashback";

		public string NomeArquivoLayoutPadrao => "Débitos de cashback.xml";

		public ICommand PesquisarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public DebitoCashbackViewModel()
		{
		}

		public DebitoCashbackViewModel(Action<DateTime, DateTime> obterDebitosCashback, IImpressaoRelatorioService impressaoRelatorioService)
			: base((IEnumerable<ItemCreditoDebitoCashbackDto>)null)
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			_obterDebitosCashback = obterDebitosCashback;
			_impressaoRelatorioService = impressaoRelatorioService;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)Imprimir);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
		}

		private void Imprimir(object obj)
		{
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		private void Pesquisar()
		{
			_obterDebitosCashback(DataInicial, DataFinal);
		}

		public void SetarResultado(IEnumerable<ItemCreditoDebitoCashbackDto> debitosCashback)
		{
			Itens = debitosCashback;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class GestaoCashbacksViewModel : ModuloBase
	{
		public GestaoCashbacksViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoCashbackRegion, "GestaoCashbackFichaView", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class GestaoCashbackFichaViewModel : FichaBase<ItemCreditoDebitoCashbackDto, CreditoCashbackViewModel>
	{
		private readonly IWService _service;

		private readonly IExecutarProcessoService _executarProcessoService;

		private Task<IEnumerable<ItemCreditoDebitoCashbackDto>> _creditosCashback;

		private Task<IEnumerable<ItemCreditoDebitoCashbackDto>> _debitosCashback;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

		public GestaoCashbackFichaViewModel(IWService service, IExecutarProcessoService executarProcessoService, IImpressaoRelatorioService impressaoRelatorioService)
			: base(string.Empty)
		{
			_service = service;
			_executarProcessoService = executarProcessoService;
			_impressaoRelatorioService = impressaoRelatorioService;
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return;
			}
			try
			{
				GestaoCashbackFormulario formulario = new GestaoCashbackFormulario(_impressaoRelatorioService, ObterCreditosCashback, ObterDebitosCashback, TransferirCashback);
				base.Formulario = (IFormulario)(object)formulario;
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				ThreadPrincipal.Executar((Action)((NavigationBase)this).FecharView);
			}
		}

		public void ObterCreditosCashback(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoCashbackModuleService, Task<IEnumerable<ItemCreditoDebitoCashbackDto>>>((Func<IGestaoCashbackModuleService, Task<IEnumerable<ItemCreditoDebitoCashbackDto>>>)((IGestaoCashbackModuleService service) => _creditosCashback = service.PesquisarCreditosCashbackPorPeriodo(dataInicial, dataFinal)));
			}, (Action<object>)RetornoCreditosCashback);
		}

		private void RetornoCreditosCashback(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_creditosCashback))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoCashbackFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<CreditoCashbackViewModel>((object)aba.ViewModel, (Action<CreditoCashbackViewModel>)delegate(CreditoCashbackViewModel x)
					{
						x.SetarResultado(_creditosCashback.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void ObterDebitosCashback(DateTime dataInicial, DateTime dataFinal)
		{
			_executarProcessoService.AdicionarProcesso((Action)delegate
			{
				_service.Canal<IGestaoCashbackModuleService, Task<IEnumerable<ItemCreditoDebitoCashbackDto>>>((Func<IGestaoCashbackModuleService, Task<IEnumerable<ItemCreditoDebitoCashbackDto>>>)((IGestaoCashbackModuleService service) => _debitosCashback = service.PesquisarDebitosCashbackPorPeriodo(dataInicial, dataFinal)));
			}, (Action<object>)RetornoDebitosCashback);
		}

		private void RetornoDebitosCashback(object obj)
		{
			try
			{
				if (obj is Exception ex)
				{
					throw ex;
				}
				if (!HelperObject.IsNotNull((object)_debitosCashback))
				{
					return;
				}
				foreach (IAbaViewModelBase aba in ((Formulario)HelperObject.To<GestaoCashbackFormulario>((object)base.Formulario)).Abas)
				{
					HelperObject.ToDo<DebitoCashbackViewModel>((object)aba.ViewModel, (Action<DebitoCashbackViewModel>)delegate(DebitoCashbackViewModel x)
					{
						x.SetarResultado(_debitosCashback.Result);
					});
				}
			}
			catch (Exception ex2)
			{
				DialogoFactory.MostrarDialogoConfirma(ex2.InnerException?.Message ?? ex2.Message, false, HorizontalAlignment.Center);
			}
		}

		public void TransferirCashback(TransferenciaCashbackDto transferenciaCashbackDto)
		{
			try
			{
				_service.Canal<IGestaoCashbackModuleService>((Action<IGestaoCashbackModuleService>)delegate(IGestaoCashbackModuleService service)
				{
					service.TransferirCashback(transferenciaCashbackDto);
				});
				DialogoFactory.MostrarDialogoConfirma("Transferência de cashback realizada com sucesso", false, HorizontalAlignment.Center);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.InnerException?.Message ?? ex.Message, false, HorizontalAlignment.Center);
			}
		}
	}
	public class GestaoCashbackFormulario : Formulario
	{
		private CreditoCashbackViewModel _creditoCashbackViewModel;

		private CreditoCashbackAbaViewModel _creditoCashbackAbaViewModel;

		private DebitoCashbackViewModel _debitoCashbackViewModel;

		private DebitoCashbackAbaViewModel _debitoCashbackAbaViewModel;

		public GestaoCashbackFormulario(IImpressaoRelatorioService impressaoRelatorioService, Action<DateTime, DateTime> obterCreditosCashback, Action<DateTime, DateTime> obterDebitosCashback, Action<TransferenciaCashbackDto> transferirCashback)
		{
			GestaoCashbackFormulario gestaoCashbackFormulario = this;
			ThreadPrincipal.Executar((Action)delegate
			{
				gestaoCashbackFormulario.ProjetarAbas(impressaoRelatorioService, obterCreditosCashback, obterDebitosCashback, transferirCashback);
			});
			((Formulario)this).PosicionarNaPrimeiraAba();
		}

		private void ProjetarAbas(IImpressaoRelatorioService impressaoRelatorioService, Action<DateTime, DateTime> obterCreditosCashback, Action<DateTime, DateTime> obterDebitosCashback, Action<TransferenciaCashbackDto> transferirCashback)
		{
			_creditoCashbackViewModel = new CreditoCashbackViewModel(obterCreditosCashback, impressaoRelatorioService);
			_creditoCashbackAbaViewModel = new CreditoCashbackAbaViewModel((IViewModelDtoBase)(object)_creditoCashbackViewModel);
			_debitoCashbackViewModel = new DebitoCashbackViewModel(obterDebitosCashback, impressaoRelatorioService);
			_debitoCashbackAbaViewModel = new DebitoCashbackAbaViewModel((IViewModelDtoBase)(object)_debitoCashbackViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[3]
			{
				(IAbaViewModelBase)_creditoCashbackAbaViewModel,
				(IAbaViewModelBase)_debitoCashbackAbaViewModel,
				(IAbaViewModelBase)new TransferenciaCashbackAbaViewModel((IViewModelDtoBase)(object)new TransferenciaCashbackViewModel(transferirCashback))
			});
		}
	}
	public class CreditoCashbackAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CreditoCashbackAbaViewModel(IViewModelDtoBase viewModel)
			: base("CRÉDITOS GERADOS", "Resumo dos créditos", viewModel)
		{
		}
	}
	public class CreditoCashbackViewModel : ViewModelDtoBase<IEnumerable<ItemCreditoDebitoCashbackDto>>, IViewModelImprimirGrid, IViewModelImprimir
	{
		private readonly Action<DateTime, DateTime> _obterCreditosCashback;

		private readonly IImpressaoRelatorioService _impressaoRelatorioService;

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

		public IEnumerable<ItemCreditoDebitoCashbackDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ItemCreditoDebitoCashbackDto>>((Expression<Func<IEnumerable<ItemCreditoDebitoCashbackDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ItemCreditoDebitoCashbackDto>>((Expression<Func<IEnumerable<ItemCreditoDebitoCashbackDto>>>)(() => Itens), value);
			}
		}

		public string TituloRelatorio => "Créditos de cashback";

		public string SubTituloRelatorio => $"Data inicial: {DataInicial:dd/MM/yyyy}  -  Data final: {DataFinal:dd/MM/yyyy}";

		public bool Retrato => false;

		public string TagProperty => "SelecaoCreditosCashback";

		public string NomeArquivoLayoutPadrao => "Créditos de cashback.xml";

		public ICommand PesquisarCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public CreditoCashbackViewModel()
		{
		}

		public CreditoCashbackViewModel(Action<DateTime, DateTime> obterCreditosCashback, IImpressaoRelatorioService impressaoRelatorioService)
			: base((IEnumerable<ItemCreditoDebitoCashbackDto>)null)
		{
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			_obterCreditosCashback = obterCreditosCashback;
			_impressaoRelatorioService = impressaoRelatorioService;
			DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DataFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
			ImprimirCommand = (ICommand)new DelegateCommand<object>((Action<object>)Imprimir);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)Pesquisar);
		}

		private void Imprimir(object obj)
		{
			_impressaoRelatorioService.ImprimirRelatorioComGridControl((IViewModelImprimirGrid)(object)this, obj as FrameworkElement, (string)null);
		}

		private void Pesquisar()
		{
			_obterCreditosCashback(DataInicial, DataFinal);
		}

		public void SetarResultado(IEnumerable<ItemCreditoDebitoCashbackDto> creditosCashback)
		{
			Itens = creditosCashback;
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.GestaoCashback.Resources
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Messages
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoCashback.Resources.Messages", typeof(Messages).Assembly);
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

		internal static string CadastroTipoCliente => ResourceManager.GetString("CadastroTipoCliente", resourceCulture);

		internal Messages()
		{
		}
	}
}
namespace Otus.Modulo.GestaoCashback.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoCashback.Properties.Resources", typeof(Resources).Assembly);
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

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.ConferenciasEstoque;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.EntradasModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using NClone;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views.Estoques.Grades;
using Otus.Infraestrutura.Views.Estoques.Lotes;
using Otus.Infraestrutura.Views.Estoques.Seriais;
using Otus.Modulo.ConferenciasEstoque.Reports;
using Otus.Modulo.ConferenciasEstoque.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.ConferenciasEstoque")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.ConferenciasEstoque")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.ConferenciasEstoque
{
	[Module(ModuleName = "ConferenciaEstoqueModulo", OnDemand = true)]
	[ModuleDependency("MovimentacaoModulo")]
	public class ConferenciaEstoqueModulo : IModule
	{
		private readonly IUnityContainer _container;

		public ConferenciaEstoqueModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<ConferenciasEstoqueView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ConferenciaEstoqueListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ConferenciaEstoqueFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<ConferenciaEstoqueFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<SerialListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<LoteFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GradeFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<SerialFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.ConferenciasEstoque.Views
{
	public class ConferenciaEstoqueFichaAdicionarView : CustomUserControl, IComponentConnector
	{
		internal DXTabControl TabControlBase;

		private bool _contentLoaded;

		public ConferenciaEstoqueFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciaestoquefichaadicionarview.xaml", UriKind.Relative);
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
				TabControlBase = (DXTabControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class ConferenciaEstoqueFichaAtualizarView : CustomUserControl, IComponentConnector
	{
		internal DXTabControl TabControlBase;

		private bool _contentLoaded;

		public ConferenciaEstoqueFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciaestoquefichaatualizarview.xaml", UriKind.Relative);
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
				TabControlBase = (DXTabControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class ConferenciaEstoqueListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConferenciaEstoqueListaView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciaestoquelistaview.xaml", UriKind.Relative);
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
	public class ConferenciasEstoqueView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConferenciasEstoqueView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciasestoqueview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.ConferenciasEstoque.Views.ConferenciaEstoqueFichaViewDetalhes
{
	public class ConferenciaEstoqueDiferencaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public ConferenciaEstoqueDiferencaView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciaestoquefichaviewdetalhes/conferenciaestoquediferencaview.xaml", UriKind.Relative);
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
	public class ConferenciaEstoqueView : CustomUserControl, IComponentConnector
	{
		internal ContentControl ContentControl;

		private bool _contentLoaded;

		public ConferenciaEstoqueView()
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
				Uri resourceLocator = new Uri("/Modulo.ConferenciasEstoque;component/views/conferenciaestoquefichaviewdetalhes/conferenciaestoqueview.xaml", UriKind.Relative);
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
				ContentControl = (ContentControl)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.ConferenciasEstoque.ViewModels
{
	public struct AbrirMovimentacaoEvent
	{
		public TipoMovimentacao TipoMovimentacao { get; }

		public bool Entrada { get; }

		public Lista<ItemConferenciaEstoqueDto> Itens { get; }

		public AbrirMovimentacaoEvent(bool entrada, Lista<ItemConferenciaEstoqueDto> itens, TipoMovimentacao tipoMovimentacao)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			TipoMovimentacao = tipoMovimentacao;
			Entrada = entrada;
			Itens = itens;
		}
	}
	public class ComboBoxBusca : ComboBoxEdit
	{
		public ComboBoxBusca()
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			((LookUpEditBase)this).SelectedIndexChanged += OnSelectedIndexChanged;
			((BaseEdit)this).EditValueChanged += new EditValueChangedEventHandler(OnEditValueChanged);
		}

		private void OnEditValueChanged(object sender, EditValueChangedEventArgs editValueChangedEventArgs)
		{
			if (((BaseEdit)this).EditValue?.ToString() == "[, ]")
			{
				((BaseEdit)this).EditValue = "";
			}
		}

		private void OnSelectedIndexChanged(object sender, RoutedEventArgs routedEventArgs)
		{
			((BaseEdit)this).EditValue = ((((LookUpEditBase)this).SelectedItem == null) ? "" : ((KeyValuePair<string, ItemConferenciaEstoqueViewModel>)((LookUpEditBase)this).SelectedItem).Key);
		}
	}
	public class ConferenciaEstoqueAbaViewModel : AbaPrincipalViewModelBase
	{
		public ConferenciaEstoqueAbaViewModel(IViewModelPrincipal viewModel)
			: base("CONFERÊNCIA DE ESTOQUE", "Dados principais", viewModel)
		{
		}
	}
	public class ConferenciaEstoqueDiferencaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ConferenciaEstoqueDiferencaAbaViewModel(IViewModelDtoBase viewModel)
			: base("DIFERENÇAS", "Diferenças de estoque", viewModel)
		{
		}
	}
	public class ConferenciaEstoqueDiferencaViewModel : ViewModelDtoBase<ConferenciaEstoqueDto>, IAbaAuxiliarViewModelBase
	{
		public class EstoqueSobraFalta
		{
			public EstoqueDto EstoqueSobra { get; set; }

			public EstoqueDto EstoqueFalta { get; set; }

			public EstoqueSobraFalta()
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Expected O, but got Unknown
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_001c: Expected O, but got Unknown
				EstoqueFalta = (EstoqueDto)new EstoqueDtoNull();
				EstoqueSobra = (EstoqueDto)new EstoqueDtoNull();
			}
		}

		public Lista<ItemConferenciaEstoqueDto> ItensFaltando { get; set; }

		public Lista<ItemConferenciaEstoqueDto> ItensSobrando { get; set; }

		public ICommand GerarNotaFiscalEletronicaEntradaCommand { get; set; }

		public ICommand GerarNotaDeEntradaCommand { get; set; }

		public ICommand GerarNotaSaidaCommand { get; set; }

		public ICommand GerarNotaManualCommand { get; set; }

		public ICommand AjustarEstoqueItensSobrandoCommand { get; set; }

		public ICommand AjustarEstoqueItensFaltandoCommand { get; set; }

		public override bool Valido => true;

		public event EventHandler<AbrirMovimentacaoEvent> AbrirMovimentacao;

		public ConferenciaEstoqueDiferencaViewModel()
		{
		}

		public ConferenciaEstoqueDiferencaViewModel(ConferenciaEstoqueDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			GerarNotaDeEntradaCommand = (ICommand)new DelegateCommand((Action)GerarNotaDeEntrada, (Func<bool>)PodeGerarNotaDeEntrada, (bool?)null);
			GerarNotaFiscalEletronicaEntradaCommand = (ICommand)new DelegateCommand((Action)GerarNotaFiscalEletronicaEntrada, (Func<bool>)PodeGerarNotaDeEntrada, (bool?)null);
			GerarNotaSaidaCommand = (ICommand)new DelegateCommand((Action)GerarNotaSaida, (Func<bool>)PodeGerarNotaDeSaida, (bool?)null);
			GerarNotaManualCommand = (ICommand)new DelegateCommand((Action)GerarNotaManual, (Func<bool>)PodeGerarNotaManual, (bool?)null);
			AjustarEstoqueItensSobrandoCommand = (ICommand)new DelegateCommand((Action)AjustarEstoqueItensSobrando, (Func<bool>)PodeAjustarEstoqueItensSobrando, (bool?)null);
			AjustarEstoqueItensFaltandoCommand = (ICommand)new DelegateCommand((Action)AjustarEstoqueItensFaltando, (Func<bool>)PodeAjustarEstoqueItensFaltando, (bool?)null);
			ItensSobrando = new Lista<ItemConferenciaEstoqueDto>();
			ItensFaltando = new Lista<ItemConferenciaEstoqueDto>();
		}

		private void GerarNotaFiscalEletronicaEntrada()
		{
			OnAbrirMovimentacao(new AbrirMovimentacaoEvent(entrada: true, ItensFaltando, (TipoMovimentacao)5));
		}

		public void SetarService(IWService service)
		{
		}

		private bool PodeGerarNotaDeEntrada()
		{
			if (ItensFaltando != null)
			{
				return ((ViewModelValidationBase)ItensFaltando).Valido;
			}
			return false;
		}

		private void GerarNotaDeEntrada()
		{
			OnAbrirMovimentacao(new AbrirMovimentacaoEvent(entrada: true, ItensFaltando, (TipoMovimentacao)4));
		}

		private bool PodeGerarNotaDeSaida()
		{
			if (ItensSobrando != null)
			{
				return ((ViewModelValidationBase)ItensSobrando).Valido;
			}
			return false;
		}

		private void GerarNotaSaida()
		{
			OnAbrirMovimentacao(new AbrirMovimentacaoEvent(entrada: false, ItensSobrando, (TipoMovimentacao)10));
		}

		private bool PodeGerarNotaManual()
		{
			if (DadosSistema.PodeEmitirNotaManualRetaguarda && !DadosSistema.IsPostoCombustivel && ItensSobrando != null)
			{
				return ((ViewModelValidationBase)ItensSobrando).Valido;
			}
			return false;
		}

		private void GerarNotaManual()
		{
			OnAbrirMovimentacao(new AbrirMovimentacaoEvent(entrada: false, ItensSobrando, (TipoMovimentacao)9));
		}

		private void AjustarEstoqueItensSobrando()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ajustar a quantidade do estoque conforme o estoque informado?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			Parallel.ForEach(ItensSobrando.Itens, delegate(ItemConferenciaEstoqueDto itemSobrando)
			{
				DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task>((Func<IProdutoServicoModuleService, Task>)((IProdutoServicoModuleService service) => service.AjustarEstoque(itemSobrando.ProdutoServicoEmpresaReferencia, itemSobrando.EstoqueInformado.Quantidade))).ContinueWith(delegate(Task task)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.EsconderLoader();
						if (task.IsFaulted)
						{
							HelperException.ShowLastException(task.Exception, "", false, false);
						}
					});
				});
			});
		}

		private bool PodeAjustarEstoqueItensSobrando()
		{
			if (ItensSobrando != null && DadosSistema.IsMEI && DadosSistema.VerificarPermissao<ProdutoServico, PermitirAjustarEstoque>())
			{
				return ((ViewModelValidationBase)ItensSobrando).Valido;
			}
			return false;
		}

		private void AjustarEstoqueItensFaltando()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Ajustar a quantidade do estoque conforme o estoque informado?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			Parallel.ForEach(ItensFaltando.Itens, delegate(ItemConferenciaEstoqueDto itemFaltando)
			{
				DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task>((Func<IProdutoServicoModuleService, Task>)((IProdutoServicoModuleService service) => service.AjustarEstoque(itemFaltando.ProdutoServicoEmpresaReferencia, itemFaltando.EstoqueInformado.Quantidade))).ContinueWith(delegate(Task task)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.EsconderLoader();
						if (task.IsFaulted)
						{
							HelperException.ShowLastException(task.Exception, "", false, false);
						}
					});
				});
			});
		}

		private bool PodeAjustarEstoqueItensFaltando()
		{
			if (ItensFaltando != null && DadosSistema.IsMEI && DadosSistema.VerificarPermissao<ProdutoServico, PermitirAjustarEstoque>())
			{
				return ((ViewModelValidationBase)ItensFaltando).Valido;
			}
			return false;
		}

		protected virtual void OnAbrirMovimentacao(AbrirMovimentacaoEvent e)
		{
			this.AbrirMovimentacao?.Invoke(this, e);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void Desativar()
		{
		}

		public void Ativar()
		{
		}

		public void SetarItens(IList<ItemConferenciaEstoqueDto> itens)
		{
			ItensSobrando.Limpar();
			ItensFaltando.Limpar();
			foreach (ItemConferenciaEstoqueDto iten in itens)
			{
				EstoqueSobraFalta estoqueSobraFalta = new EstoqueSobraFalta();
				CalcularEstoqueNormal(iten.EstoqueSistema, iten.EstoqueInformado, estoqueSobraFalta);
				CalcularEstoqueSerial(iten.EstoqueSistema, iten.EstoqueInformado, estoqueSobraFalta);
				CalcularEstoqueGrade(iten.EstoqueSistema, iten.EstoqueInformado, estoqueSobraFalta);
				CalcularEstoqueLote(iten.EstoqueSistema, iten.EstoqueInformado, estoqueSobraFalta);
				iten.EstoqueFalta = estoqueSobraFalta.EstoqueFalta;
				iten.EstoqueSobra = estoqueSobraFalta.EstoqueSobra;
				if (iten.EstoqueFalta.Quantidade > 0m)
				{
					ItensFaltando.Adicionar(iten);
				}
				if (iten.EstoqueSobra.Quantidade > 0m)
				{
					ItensSobrando.Adicionar(iten);
				}
			}
		}

		private void CalcularEstoqueNormal(EstoqueDto estoqueSistema, EstoqueDto estoqueInformado, EstoqueSobraFalta estoqueSobraFalta)
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			if (estoqueInformado.IsNormal)
			{
				if (estoqueInformado.Quantidade > estoqueSistema.Quantidade)
				{
					estoqueSobraFalta.EstoqueFalta = new EstoqueDto
					{
						Id = ((EntityBaseDto)estoqueInformado).Id,
						Quantidade = estoqueInformado.Quantidade - estoqueSistema.Quantidade
					};
				}
				else if (estoqueSistema.Quantidade > estoqueInformado.Quantidade)
				{
					estoqueSobraFalta.EstoqueSobra = new EstoqueDto
					{
						Id = ((EntityBaseDto)estoqueInformado).Id,
						Quantidade = estoqueSistema.Quantidade - estoqueInformado.Quantidade
					};
				}
			}
		}

		private void CalcularEstoqueSerial(EstoqueDto estoqueSistema, EstoqueDto estoqueInformado, EstoqueSobraFalta estoqueSobraFalta)
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			if (!HelperObject.Is<EstoqueSerialDto>((object)estoqueInformado))
			{
				return;
			}
			List<SerialDto> list = HelperIEnumerable.ToListEmptyWhenNull<SerialDto>(HelperObject.To<EstoqueSerialDto>((object)estoqueInformado).Seriais.Where(delegate(SerialDto serial)
			{
				EstoqueSerialDto obj2 = HelperObject.To<EstoqueSerialDto>((object)estoqueSistema);
				return obj2 == null || !obj2.Seriais.Any((SerialDto x) => x.Id.Equals(serial.Id));
			}));
			if (list.Any())
			{
				estoqueSobraFalta.EstoqueFalta = (EstoqueDto)new EstoqueSerialDto
				{
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Quantidade = list.Count,
					Seriais = list
				};
			}
			EstoqueSerialDto obj = HelperObject.To<EstoqueSerialDto>((object)estoqueSistema);
			IEnumerable<SerialDto> enumerable = ((obj != null) ? HelperIEnumerable.ToListEmptyWhenNull<SerialDto>(obj.Seriais.Where((SerialDto serial) => !HelperObject.To<EstoqueSerialDto>((object)estoqueInformado).Seriais.Any((SerialDto x) => x.Id.Equals(serial.Id)))) : null);
			IEnumerable<SerialDto> enumerable2 = enumerable ?? Enumerable.Empty<SerialDto>();
			if (enumerable2.Any())
			{
				estoqueSobraFalta.EstoqueSobra = (EstoqueDto)new EstoqueSerialDto
				{
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Quantidade = enumerable2.Count(),
					Seriais = enumerable2
				};
			}
		}

		private void CalcularEstoqueGrade(EstoqueDto estoqueSistema, EstoqueDto estoqueInformado, EstoqueSobraFalta estoqueSobraFalta)
		{
			//IL_013e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Expected O, but got Unknown
			if (!HelperObject.Is<EstoqueGradeDto>((object)estoqueInformado))
			{
				return;
			}
			EstoqueDto obj = ((estoqueInformado is EstoqueGradeDto) ? estoqueInformado : null);
			List<GradeDto> list = new List<GradeDto>();
			List<GradeDto> list2 = new List<GradeDto>();
			foreach (GradeDto grade in ((EstoqueGradeDto)obj).Grades)
			{
				EstoqueSobraFalta estoqueSobraFalta2 = new EstoqueSobraFalta();
				EstoqueGradeDto obj2 = HelperObject.To<EstoqueGradeDto>((object)estoqueSistema);
				object obj3;
				if (obj2 == null)
				{
					obj3 = null;
				}
				else
				{
					GradeDto obj4 = obj2.Grades.FirstOrDefault((GradeDto x) => ((EntityBaseDto)x.Estoque).Id.Equals(((EntityBaseDto)grade.Estoque).Id));
					obj3 = ((obj4 != null) ? obj4.Estoque : null);
				}
				if (obj3 == null)
				{
					obj3 = (object)new EstoqueDtoNull();
				}
				EstoqueDto estoqueSistema2 = (EstoqueDto)obj3;
				CalcularEstoqueNormal(estoqueSistema2, grade.Estoque, estoqueSobraFalta2);
				CalcularEstoqueSerial(estoqueSistema2, grade.Estoque, estoqueSobraFalta2);
				if (estoqueSobraFalta2.EstoqueFalta.Quantidade > 0m)
				{
					GradeDto val = Clone.ObjectGraph<GradeDto>(grade);
					val.Estoque = estoqueSobraFalta2.EstoqueFalta;
					list.Add(val);
				}
				if (estoqueSobraFalta2.EstoqueSobra.Quantidade > 0m)
				{
					GradeDto val2 = Clone.ObjectGraph<GradeDto>(grade);
					val2.Estoque = estoqueSobraFalta2.EstoqueSobra;
					list2.Add(val2);
				}
			}
			if (list.Any())
			{
				estoqueSobraFalta.EstoqueFalta = (EstoqueDto)new EstoqueGradeDto
				{
					Quantidade = list.Sum((GradeDto x) => x.Estoque.Quantidade),
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Grades = list
				};
			}
			if (list2.Any())
			{
				estoqueSobraFalta.EstoqueSobra = (EstoqueDto)new EstoqueGradeDto
				{
					Quantidade = list2.Sum((GradeDto x) => x.Estoque.Quantidade),
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Grades = list2
				};
			}
		}

		private void CalcularEstoqueLote(EstoqueDto estoqueSistema, EstoqueDto estoqueInformado, EstoqueSobraFalta estoqueSobraFalta)
		{
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f3: Expected O, but got Unknown
			if (!HelperObject.Is<EstoqueLoteDto>((object)estoqueInformado))
			{
				return;
			}
			EstoqueDto obj = ((estoqueInformado is EstoqueLoteDto) ? estoqueInformado : null);
			List<LoteDto> list = new List<LoteDto>();
			List<LoteDto> list2 = new List<LoteDto>();
			foreach (LoteDto lote in ((EstoqueLoteDto)obj).Lotes)
			{
				EstoqueSobraFalta estoqueSobraFalta2 = new EstoqueSobraFalta();
				EstoqueLoteDto obj2 = HelperObject.To<EstoqueLoteDto>((object)estoqueSistema);
				object obj3;
				if (obj2 == null)
				{
					obj3 = null;
				}
				else
				{
					IEnumerable<LoteDto> lotes = obj2.Lotes;
					if (lotes == null)
					{
						obj3 = null;
					}
					else
					{
						LoteDto obj4 = lotes.FirstOrDefault((LoteDto x) => ((EntityBaseDto)x.Estoque).Id.Equals(((EntityBaseDto)lote.Estoque).Id));
						obj3 = ((obj4 != null) ? obj4.Estoque : null);
					}
				}
				if (obj3 == null)
				{
					obj3 = (object)new EstoqueDtoNull();
				}
				EstoqueDto estoqueSistema2 = (EstoqueDto)obj3;
				CalcularEstoqueNormal(estoqueSistema2, lote.Estoque, estoqueSobraFalta2);
				CalcularEstoqueSerial(estoqueSistema2, lote.Estoque, estoqueSobraFalta2);
				CalcularEstoqueGrade(estoqueSistema2, lote.Estoque, estoqueSobraFalta2);
				if (estoqueSobraFalta2.EstoqueFalta.Quantidade > 0m)
				{
					LoteDto val = Clone.ObjectGraph<LoteDto>(lote);
					val.Estoque = estoqueSobraFalta2.EstoqueFalta;
					list.Add(val);
				}
				if (estoqueSobraFalta2.EstoqueSobra.Quantidade > 0m)
				{
					LoteDto val2 = Clone.ObjectGraph<LoteDto>(lote);
					val2.Estoque = estoqueSobraFalta2.EstoqueSobra;
					list2.Add(val2);
				}
			}
			if (list.Any())
			{
				estoqueSobraFalta.EstoqueFalta = (EstoqueDto)new EstoqueLoteDto
				{
					Quantidade = list.Sum((LoteDto x) => x.Estoque.Quantidade),
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Lotes = list
				};
			}
			if (list2.Any())
			{
				estoqueSobraFalta.EstoqueSobra = (EstoqueDto)new EstoqueLoteDto
				{
					Quantidade = list2.Sum((LoteDto x) => x.Estoque.Quantidade),
					Id = ((EntityBaseDto)estoqueInformado).Id,
					Lotes = list2
				};
			}
		}
	}
	public class ConferenciaEstoqueFichaAdicionarViewModel : FichaBaseAdicionar<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel, IConferenciaEstoqueModuleService>
	{
		private Action _a;

		private ConferenciaEstoqueFormulario _formulario => HelperObject.To<ConferenciaEstoqueFormulario>((object)((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Formulario);

		public ICommand ImportarArquivoColetorDadosCommand { get; set; }

		public ConferenciaEstoqueFichaAdicionarViewModel(IWService service)
			: base(service)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			ImportarArquivoColetorDadosCommand = (ICommand)new DelegateCommand((Action)ImportarArquivoColetorDados, (Func<bool>)PodeImportarArquivoColetorDados, (bool?)null);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).CriarFormulario();
			_formulario.SetarService(base.Service);
			_formulario.SetarNavigation((Action<string, Action<NavigationResult>, NavigationParameters>)((NavigationBase)this).Navegar);
			_formulario.SetarNavigationProduto((Action<object>)((NavigationBase)this).Navegar);
			_a = _formulario.SetarImportarArquivoColetorDadosCommand();
		}

		private void ImportarArquivoColetorDados()
		{
			_a?.Invoke();
		}

		private bool PodeImportarArquivoColetorDados()
		{
			if (HelperObject.IsNotNull((object)_formulario))
			{
				return !_formulario.ConferenciaEstoqueViewModel.MostrarItensNaoRelacionados;
			}
			return false;
		}

		public override void FecharView()
		{
			((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).DisableConfirmOnClose();
			((NavigationBase)this).FecharView();
		}
	}
	public class ConferenciaEstoqueFichaAtualizarViewModel : FichaBaseAtualizar<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel, IConferenciaEstoqueModuleService>
	{
		private Action _a;

		private ConferenciaEstoqueFormulario _formulario => HelperObject.To<ConferenciaEstoqueFormulario>((object)((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Formulario);

		public ICommand ImportarArquivoColetorDadosCommand { get; set; }

		public ConferenciaEstoqueFichaAtualizarViewModel(IWService service)
			: base(service)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			ImportarArquivoColetorDadosCommand = (ICommand)new DelegateCommand((Action)ImportarArquivoColetorDados, (Func<bool>)PodeImportarArquivoColetorDados, (bool?)null);
		}

		protected override void CriarFormulario()
		{
			((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).CriarFormulario();
			_formulario.SetarService(base.Service);
			_formulario.SetarNavigation((Action<string, Action<NavigationResult>, NavigationParameters>)((NavigationBase)this).Navegar);
			_formulario.SetarNavigationProduto((Action<object>)((NavigationBase)this).Navegar);
			_a = _formulario.SetarImportarArquivoColetorDadosCommand();
		}

		private void ImportarArquivoColetorDados()
		{
			_a?.Invoke();
		}

		private bool PodeImportarArquivoColetorDados()
		{
			if (HelperObject.IsNotNull((object)_formulario))
			{
				return !_formulario.ConferenciaEstoqueViewModel.MostrarItensNaoRelacionados;
			}
			return false;
		}

		public override void FecharView()
		{
			((FichaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).DisableConfirmOnClose();
			((NavigationBase)this).FecharView();
		}
	}
	public sealed class ConferenciaEstoqueFormulario : FormularioEntity<ConferenciaEstoqueDto>
	{
		private IWService _service;

		private readonly ConferenciaEstoqueDiferencaViewModel _conferenciaEstoqueDiferencaViewModel;

		private Action<string, Action<NavigationResult>, NavigationParameters> _navegar;

		public ConferenciaEstoqueViewModel ConferenciaEstoqueViewModel { get; }

		public ConferenciaEstoqueFormulario(ConferenciaEstoqueDto dto)
			: base(dto)
		{
			_conferenciaEstoqueDiferencaViewModel = new ConferenciaEstoqueDiferencaViewModel(base.Dto);
			ConferenciaEstoqueViewModel = new ConferenciaEstoqueViewModel(base.Dto);
			_conferenciaEstoqueDiferencaViewModel.AbrirMovimentacao += OnAbrirMovimentacao;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new ConferenciaEstoqueAbaViewModel((IViewModelPrincipal)(object)ConferenciaEstoqueViewModel) });
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new ConferenciaEstoqueDiferencaAbaViewModel((IViewModelDtoBase)(object)_conferenciaEstoqueDiferencaViewModel) });
		}

		public void SetarService(IWService service)
		{
			_service = service;
			ConferenciaEstoqueViewModel.SetarService(service);
		}

		internal void SetarNavigation(Action<string, Action<NavigationResult>, NavigationParameters> navegar)
		{
			_navegar = navegar;
		}

		internal void SetarNavigationProduto(Action<object> navegar)
		{
			ConferenciaEstoqueViewModel.SetarNavegar(navegar);
		}

		public Action SetarImportarArquivoColetorDadosCommand()
		{
			return ImportarArquivoColetorDados();
		}

		protected override void OnAlterouAbaSelecionadaEvent(IAbaViewModelBase e)
		{
			((Formulario)this).OnAlterouAbaSelecionadaEvent(e);
			_conferenciaEstoqueDiferencaViewModel.SetarItens(ConferenciaEstoqueViewModel.Itens.Itens.ToList());
		}

		private async void OnAbrirMovimentacao(object sender, AbrirMovimentacaoEvent abrirMovimentacaoEvent)
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Processando...");
				});
				string cfopReferencia = (abrirMovimentacaoEvent.Entrada ? DadosSistema.ConfiguracaoDto.Entrada.CfopPadraoEntradaAjusteMercadoria.Referencia : DadosSistema.ConfiguracaoDto.Saida.CfopPadraoSaidaAjusteMercadoria.Referencia);
				if (string.IsNullOrEmpty(cfopReferencia))
				{
					string arg = (abrirMovimentacaoEvent.Entrada ? "Entradas" : "Saídas");
					DialogoFactory.MostrarDialogoConfirma($"CFOP padrão para nota de correção de estoque não indicada nas Configurações > {arg} > CFOP padrão para nota fiscal de ajuste de estoque.", false, HorizontalAlignment.Center);
					return;
				}
				List<Tuple<string, EstoqueDto>> produtos = abrirMovimentacaoEvent.Itens.Itens.Select((ItemConferenciaEstoqueDto x) => new Tuple<string, EstoqueDto>(x.ProdutoServicoEmpresaReferencia, abrirMovimentacaoEvent.Entrada ? x.EstoqueFalta : x.EstoqueSobra)).ToList();
				List<string> idsMovimentacoesAdicionadas = new List<string>();
				string text;
				string text2;
				if (abrirMovimentacaoEvent.Entrada && (int)abrirMovimentacaoEvent.TipoMovimentacao == 4)
				{
					if (produtos.Count > 990)
					{
						List<string> list = idsMovimentacoesAdicionadas;
						list.AddRange(await _service.Canal<IMovimentacaoEntradaModuleService, Task<IEnumerable<string>>>((Func<IMovimentacaoEntradaModuleService, Task<IEnumerable<string>>>)((IMovimentacaoEntradaModuleService service) => service.ObterMovimentacoesConferenciaEstoque((IEnumerable<Tuple<string, EstoqueDto>>)produtos, cfopReferencia, (TipoPrecoTransferencia)2, abrirMovimentacaoEvent.TipoMovimentacao))));
						text = string.Join("|", idsMovimentacoesAdicionadas.Select((string x) => x));
						text2 = "MovimentacaoEntradaListaView";
						DialogoFactory.MostrarDialogoConfirma("Edite a(s) nota(s) de compra e informe a numeração!", false, HorizontalAlignment.Center);
						Action<string, Action<NavigationResult>, NavigationParameters> navegar = _navegar;
						string arg2 = text2;
						Action<NavigationResult> arg3 = delegate
						{
						};
						NavigationParameters val = new NavigationParameters();
						val.Add("PesquisarNotaCompraConferencia", (object)text);
						navegar(arg2, arg3, val);
					}
					else
					{
						MovimentacaoDto val2 = await _service.Canal<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoEntradaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoEntradaModuleService service) => service.ObterMovimentacaoConferenciaEstoque((IEnumerable<Tuple<string, EstoqueDto>>)produtos, cfopReferencia, (TipoPrecoTransferencia)2, abrirMovimentacaoEvent.TipoMovimentacao)));
						Action<string, Action<NavigationResult>, NavigationParameters> navegar2 = _navegar;
						Action<NavigationResult> arg4 = delegate
						{
						};
						NavigationParameters val3 = new NavigationParameters();
						val3.Add("MovimentacaoDto", (object)val2);
						navegar2("NotaFiscalCompraFichaAdicionarView", arg4, val3);
					}
					return;
				}
				if (abrirMovimentacaoEvent.Entrada)
				{
					List<string> list = idsMovimentacoesAdicionadas;
					list.AddRange(await _service.Canal<IMovimentacaoEntradaModuleService, Task<IEnumerable<string>>>((Func<IMovimentacaoEntradaModuleService, Task<IEnumerable<string>>>)((IMovimentacaoEntradaModuleService service) => service.ObterMovimentacoesConferenciaEstoque((IEnumerable<Tuple<string, EstoqueDto>>)produtos, cfopReferencia, (TipoPrecoTransferencia)2, abrirMovimentacaoEvent.TipoMovimentacao))));
					text = string.Join("|", idsMovimentacoesAdicionadas.Select((string x) => x));
					text2 = "MovimentacaoEntradaListaView";
				}
				else
				{
					List<KeyValuePair<string, TipoPrecoTransferencia>> list2 = HelperEnum.ObterParDeValores<TipoPrecoTransferencia>();
					KeyValuePair<string, TipoPrecoTransferencia> keyValuePair = DialogoFactory.MostrarDialogoComboBox<KeyValuePair<string, TipoPrecoTransferencia>>("Qual preço utilizar nos itens lançados manualmente?", "Preço dos itens", (IEnumerable<KeyValuePair<string, TipoPrecoTransferencia>>)list2, "Key", true);
					TipoPrecoTransferencia tipoPrecoNotaTransferencia = (TipoPrecoTransferencia)(((int)keyValuePair.Value == 0) ? 2 : ((int)keyValuePair.Value));
					List<string> list = idsMovimentacoesAdicionadas;
					list.AddRange(await _service.Canal<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>((Func<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>)((IMovimentacaoSaidaModuleService service) => service.ObterMovimentacoesConferenciaEstoque((IEnumerable<Tuple<string, EstoqueDto>>)produtos, cfopReferencia, tipoPrecoNotaTransferencia, DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia, abrirMovimentacaoEvent.TipoMovimentacao))));
					text = string.Join("|", idsMovimentacoesAdicionadas.Select((string x) => x));
					text2 = "MovimentacaoSaidaListaView";
				}
				Action<string, Action<NavigationResult>, NavigationParameters> navegar3 = _navegar;
				string arg5 = text2;
				Action<NavigationResult> arg6 = delegate
				{
				};
				NavigationParameters val4 = new NavigationParameters();
				val4.Add("PesquisarNfesConferencia", (object)text);
				navegar3(arg5, arg6, val4);
			}
			catch (Exception ex)
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			}
		}

		private Action ImportarArquivoColetorDados()
		{
			return delegate
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Multiselect = false,
					Filter = "Arquivos de texto (*.txt)|*.txt;"
				};
				try
				{
					openFileDialog.ShowDialog();
					if (!string.IsNullOrEmpty(openFileDialog.FileName))
					{
						string[] itens = File.ReadAllLines(openFileDialog.FileName, Encoding.Default);
						IEnumerable<string> naoImportados = ConferenciaEstoqueViewModel.AdicionarItens(itens);
						ProcessarNaoImportados(openFileDialog, naoImportados);
						((Formulario)this).PosicionarNaPrimeiraAba();
					}
				}
				finally
				{
					openFileDialog.Reset();
				}
			};
		}

		private static void ProcessarNaoImportados(FileDialog abrir, IEnumerable<string> naoImportados)
		{
			try
			{
				if (!naoImportados.Any())
				{
					return;
				}
				string text = $"{Path.GetDirectoryName(abrir.FileName)}\\{abrir.SafeFileName.Split('.')[0]}_NaoImportados.txt";
				File.WriteAllText(text, string.Empty);
				using (StreamWriter streamWriter = new StreamWriter(text, append: true))
				{
					streamWriter.WriteLine("Códigos não importados para a conferência de estoque");
					streamWriter.WriteLine($"Arquivo original: {abrir.SafeFileName}");
					foreach (string naoImportado in naoImportados)
					{
						streamWriter.WriteLine($"{naoImportado}");
					}
				}
				DialogoFactory.MostrarDialogoConfirma($"O arquivo {abrir.FileName} foi importado parcialmente.\n\nVerifique o arquivo {text}", false, HorizontalAlignment.Center);
			}
			catch (Exception)
			{
			}
		}

		public override void Dispose()
		{
			_conferenciaEstoqueDiferencaViewModel.AbrirMovimentacao -= OnAbrirMovimentacao;
			_navegar = null;
			((Formulario)this).Dispose();
		}
	}
	public class ConferenciaEstoqueListaViewModel : ListaEntity<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel, IConferenciaEstoqueModuleService, ConferenciaEstoque>
	{
		public string InformacaoItensSelecionados
		{
			get
			{
				if (ItensSelecionados.Count() <= 1)
				{
					return string.Empty;
				}
				return $"{ItensSelecionados.Count()} itens selecionados";
			}
		}

		public IEnumerable<ConferenciaEstoqueDto> ItensSelecionados => ((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens?.ObterDtoItensSelecionados();

		public ICommand SelecionarItemUtilizandoBarraDeEspacoCommand { get; set; }

		public ICommand SelecionarItemCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand UnificarCommand { get; set; }

		public ConferenciaEstoqueListaViewModel(ISessao sessao, IEntityAtivos<ConferenciaEstoqueDto, IConferenciaEstoqueModuleService> ativos, IEntityInativos<ConferenciaEstoqueDto, IConferenciaEstoqueModuleService> inativos)
			: base(sessao, typeof(ConferenciaEstoqueFichaAdicionarView).Name, typeof(ConferenciaEstoqueFichaAtualizarView).Name, ativos, inativos)
		{
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Expected O, but got Unknown
			SelecionarItemUtilizandoBarraDeEspacoCommand = (ICommand)new DelegateCommand((Action)SelecionarItem, (Func<bool>)PodeSelecionarItem, (bool?)null);
			SelecionarItemCommand = (ICommand)new DelegateCommand<ConferenciaEstoqueViewModel>((Action<ConferenciaEstoqueViewModel>)SelecionarItem);
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			UnificarCommand = (ICommand)new AsyncCommand((Func<Task>)Unificar, (Func<bool>)PodeUnificar, (bool?)null);
		}

		private bool PodeSelecionarItem()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens))
			{
				return ((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private void SelecionarItem()
		{
			ConferenciaEstoqueViewModel selecionado = ((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.Selecionado;
			if (!HelperObject.IsNull((object)selecionado))
			{
				selecionado.Selecionar();
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => InformacaoItensSelecionados));
			}
		}

		private void SelecionarItem(ConferenciaEstoqueViewModel item)
		{
			if (!HelperObject.IsNull((object)item))
			{
				((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.Selecionado = item;
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => InformacaoItensSelecionados));
			}
		}

		private void Imprimir()
		{
			XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoConferenciaEstoque(((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.ObterDtoItemSelecionado())).Processar());
		}

		private bool PodeImprimir()
		{
			if (((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens != null)
			{
				return ((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private async Task Unificar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("As conferências de estoque selecionadas serão desativadas. Deseja continuar?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Unificando conferências de estoque...");
			IEnumerable<string> conferenciasSelecionadas = ItensSelecionados.Select((ConferenciaEstoqueDto x) => ((EntityBaseDto)x).Id);
			await DadosSistema.GetService().Canal<IConferenciaEstoqueModuleService, Task<IEnumerable<ConferenciaEstoqueDto>>>((Func<IConferenciaEstoqueModuleService, Task<IEnumerable<ConferenciaEstoqueDto>>>)((IConferenciaEstoqueModuleService x) => x.Unificar(conferenciasSelecionadas))).ContinueWith(delegate(Task<IEnumerable<ConferenciaEstoqueDto>> task)
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
						((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.Limpar();
						((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.Adicionar<ConferenciaEstoqueDto>(task.Result);
						((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens.PosicionarPrimeiroRegistro();
					});
				}
			});
		}

		private bool PodeUnificar()
		{
			if (((ListaBase<ConferenciaEstoqueDto, ConferenciaEstoqueViewModel>)(object)this).Itens != null)
			{
				return ItensSelecionados.Count() > 1;
			}
			return false;
		}
	}
	public class ConferenciaEstoqueViewModel : ViewModelAtivoBase<ConferenciaEstoqueDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
		private IWService _service;

		private Action<object> _navegar;

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

		public string InformacaoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisa), value);
			}
		}

		public Lista<ItemConferenciaEstoqueViewModel> ItensNaoRelacionados { get; set; }

		public bool MostrarItensNaoRelacionados
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarItensNaoRelacionados));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarItensNaoRelacionados), value);
			}
		}

		public Lista<KeyValuePair<string, ItemConferenciaEstoqueViewModel>> ResultadoBuscaProdutos { get; set; }

		public string DescricaoItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoItem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoItem), value);
			}
		}

		public bool FocoQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoQuantidade), value);
			}
		}

		public bool FocoDescricaoItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoDescricaoItem));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoDescricaoItem), value);
			}
		}

		public ItemConferenciaEstoqueViewModel ItemSelecionadoBusca
		{
			get
			{
				return ((BindableBase)this).GetProperty<ItemConferenciaEstoqueViewModel>((Expression<Func<ItemConferenciaEstoqueViewModel>>)(() => ItemSelecionadoBusca));
			}
			set
			{
				((BindableBase)this).SetProperty<ItemConferenciaEstoqueViewModel>((Expression<Func<ItemConferenciaEstoqueViewModel>>)(() => ItemSelecionadoBusca), value);
			}
		}

		public DateTime DataConferencia { get; set; }

		public string Usuario { get; set; }

		public string UsuarioReferencia { get; set; }

		public string EmpresaReferencia { get; set; }

		public string ToText => $"{DataConferencia:dd/MM/yyyy} Usuário: {Usuario}";

		public Lista<ItemConferenciaEstoqueDto> Itens { get; set; }

		public ICommand AdicionarItemCommand { get; set; }

		public ICommand RemoverItemCommand { get; set; }

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public ICommand LimparBuscaCommand { get; set; }

		public ICommand ObterProdutosNaoRelacionadosCommand { get; set; }

		public ICommand AdicionarProdutosNaoRelacionadosCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public override bool Valido
		{
			get
			{
				if (!MostrarItensNaoRelacionados)
				{
					return ((ViewModelValidationBase)this).Valido;
				}
				return false;
			}
		}

		public ICommand PesquisarCommand { get; }

		public ICommand LimparCommand { get; }

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

		public event EventHandler<AbrirMovimentacaoEvent> AbirMovimentacao;

		public ConferenciaEstoqueViewModel()
		{
		}

		public ConferenciaEstoqueViewModel(ConferenciaEstoqueDto dto)
			: base(dto)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Expected O, but got Unknown
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Expected O, but got Unknown
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_0178: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Expected O, but got Unknown
			PesquisarProdutoServicoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto, (Func<bool>)PodePesquisarProduto, (bool?)null);
			AdicionarItemCommand = (ICommand)new DelegateCommand((Action)AdicionarItem, (Func<bool>)PodeAdicionarItem, (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<ItemConferenciaEstoqueDto>((Action<ItemConferenciaEstoqueDto>)RemoverItem);
			Itens = new Lista<ItemConferenciaEstoqueDto>();
			ResultadoBuscaProdutos = new Lista<KeyValuePair<string, ItemConferenciaEstoqueViewModel>>();
			ResultadoBuscaProdutos.AlterouItemSelecionadoEvent += ResultadoBuscaProdutosOnAlterouItemSelecionadoEvent;
			LimparBuscaCommand = (ICommand)new DelegateCommand((Action)Limpar);
			ObterProdutosNaoRelacionadosCommand = (ICommand)new AsyncCommand((Func<Task>)ObterProdutosNaoRelacionados, (Func<bool>)PodeObterProdutosNaoRelacionados, false, (bool?)null);
			AdicionarProdutosNaoRelacionadosCommand = (ICommand)new DelegateCommand((Action)AdicionarProdutosNaoRelacionados, (Func<bool>)PodeAdicionarProdutosNaoRelacionados, (bool?)null);
			ImprimirCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirItensNaoRelacionados, (Func<bool>)PodeObterProdutosNaoRelacionados, (bool?)null);
			PesquisarCommand = (ICommand)new DelegateCommand((Action)PesquisarItens, (Func<bool>)PodePesquisarItens, (bool?)null);
			LimparCommand = (ICommand)new DelegateCommand((Action)LimparPesquisa);
		}

		private void PesquisarItens()
		{
			string informacao = HelperString.RemoveDiacritics(HelperString.ToStringEmptyWhenNull(InformacaoPesquisa)).ToLower();
			Itens.Limpar();
			Itens.Adicionar(((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens.Where((ItemConferenciaEstoqueDto x) => x.ToText.StartsWith(informacao) || x.ToText.Contains(informacao)));
			if (string.IsNullOrWhiteSpace(informacao))
			{
				Itens.PosicionarNoUltimo();
			}
			else
			{
				Itens.PosicionarNoPrimeiro();
			}
		}

		private bool PodePesquisarItens()
		{
			return ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens.Any();
		}

		private void LimparPesquisa()
		{
			InformacaoPesquisa = string.Empty;
			PesquisarItens();
		}

		private void ResultadoBuscaProdutosOnAlterouItemSelecionadoEvent(object sender, KeyValuePair<string, ItemConferenciaEstoqueViewModel> conferenciaEstoqueViewModel)
		{
			if (!HelperObject.IsNull((object)conferenciaEstoqueViewModel.Value))
			{
				ItemSelecionadoBusca = conferenciaEstoqueViewModel.Value;
			}
		}

		public void SetarService(IWService service)
		{
			_service = service;
		}

		public void SetarNavegar(Action<object> navegar)
		{
			_navegar = navegar;
		}

		private bool PodePesquisarProduto()
		{
			return DescricaoItem != null;
		}

		private void PesquisarProduto()
		{
			try
			{
				if (string.IsNullOrEmpty(DescricaoItem))
				{
					NavegarProdutoServicoListaView();
					return;
				}
				ObterProduto(DescricaoItem);
				if (((ViewModelValidationBase)ResultadoBuscaProdutos).Valido)
				{
					FocoQuantidade = !FocoQuantidade;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void NavegarProdutoServicoListaView()
		{
			SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto> val = new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto dto)
			{
				AdicionarProdutos(new List<ProdutoServicoEmpresaDto> { dto });
			}, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa)), "Selecione um produto", HorizontalAlignment.Center));
			((SelecaoCommandParameter<ProdutoServicoEmpresaDto>)(object)val).Parametros.Add("InformacaoPesquisa", (object)InformacaoPesquisa);
			_navegar(val);
		}

		private void ObterProduto(string descricao)
		{
			Limpar();
			List<ProdutoServicoEmpresaDto> produtosServicos = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdentificadorParaConferenciaEstoque(descricao))).ToList();
			AdicionarProdutos(produtosServicos);
		}

		public void AdicionarProdutos(IEnumerable<ProdutoServicoEmpresaDto> produtosServicos)
		{
			if (produtosServicos.Any())
			{
				List<KeyValuePair<string, ItemConferenciaEstoqueViewModel>> list = HelperIEnumerable.ToListEmptyWhenNull<KeyValuePair<string, ItemConferenciaEstoqueViewModel>>(from item in produtosServicos.Where(ValidarProdutoServicoSelecionado)
					select new KeyValuePair<string, ItemConferenciaEstoqueViewModel>(((BaseDto)item).ToText, ItemConferenciaEstoqueViewModel.Criar(item)));
				if (list.Any())
				{
					ResultadoBuscaProdutos.AdicionarSemPosicionar((IEnumerable<KeyValuePair<string, ItemConferenciaEstoqueViewModel>>)list);
					ResultadoBuscaProdutos.PosicionarNoPrimeiro();
					DescricaoItem = ResultadoBuscaProdutos.ItemSelecionado.Key;
				}
			}
		}

		private void Limpar()
		{
			foreach (KeyValuePair<string, ItemConferenciaEstoqueViewModel> iten in ResultadoBuscaProdutos.Itens)
			{
				ItemConferenciaEstoqueViewModel value = iten.Value;
				if (value != null)
				{
					((ViewModelValidationBase)value).Dispose();
				}
			}
			ResultadoBuscaProdutos.Limpar();
			ResultadoBuscaProdutos.ItemSelecionado = default(KeyValuePair<string, ItemConferenciaEstoqueViewModel>);
		}

		public virtual bool ValidarProdutoServicoSelecionado(ProdutoServicoEmpresaDto resultado)
		{
			return new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsNot<ProdutoEmpresaDtoNull>((object)x.ProdutoEmpresa) && (int)x.ProdutoServico.Tipo != 5), "Selecione um produto", HorizontalAlignment.Center).Execute(resultado);
		}

		private void AdicionarItem()
		{
			AdicionarProduto(ResultadoBuscaProdutos.ItemSelecionado.Value);
			DescricaoItem = string.Empty;
			FocoDescricaoItem = !FocoDescricaoItem;
			ItemSelecionadoBusca = null;
		}

		public IEnumerable<string> AdicionarItens(string[] itens)
		{
			List<string> list = new List<string>();
			string[] array = new string[2];
			foreach (string text in itens)
			{
				try
				{
					array = text.Split(',');
					ObterProduto(array[0]);
					ItemConferenciaEstoqueViewModel value = ResultadoBuscaProdutos.ItemSelecionado.Value;
					decimal quantidadeInformada = Convert.ToDecimal(array[1].Replace(".", ","));
					value.QuantidadeInformada = quantidadeInformada;
					AdicionarProduto(ResultadoBuscaProdutos.ItemSelecionado.Value);
					ThreadPrincipal.Executar((Action)Limpar);
				}
				catch (Exception)
				{
					list.Add(array[0]);
				}
			}
			return list;
		}

		private void AdicionarProduto(ItemConferenciaEstoqueViewModel itemViewModel)
		{
			Itens.Limpar();
			Itens.Adicionar(((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens);
			ItemConferenciaEstoqueDto val = Itens.Itens.FirstOrDefault((ItemConferenciaEstoqueDto x) => x.ProdutoServicoEmpresaReferencia.Equals(((ViewModelDtoBase<ItemConferenciaEstoqueDto>)itemViewModel).Dto.ProdutoServicoEmpresaReferencia));
			if (val != null)
			{
				if (!((ViewModelDtoBase<ItemConferenciaEstoqueDto>)itemViewModel).Dto.EstoqueSistema.IsNormal)
				{
					DialogoFactory.MostrarDialogoConfirma("Item já foi adicionado!", false, HorizontalAlignment.Center);
				}
				else
				{
					itemViewModel.Somar(val);
					Itens.Substituir((Expression<Func<ItemConferenciaEstoqueDto, bool>>)((ItemConferenciaEstoqueDto x) => x.ProdutoServicoEmpresaReferencia.Equals(((ViewModelDtoBase<ItemConferenciaEstoqueDto>)itemViewModel).Dto.ProdutoServicoEmpresaReferencia)), itemViewModel.ObterDto());
					AlterarItensDto(Itens.Itens);
				}
			}
			else
			{
				Itens.Adicionar(itemViewModel.ObterDto());
				AlterarItensDto(Itens.Itens);
			}
			Limpar();
		}

		private bool PodeAdicionarItem()
		{
			return ((ViewModelValidationBase)ResultadoBuscaProdutos).Valido;
		}

		private void RemoverItem(ItemConferenciaEstoqueDto obj)
		{
			Itens.Remover(obj);
			AlterarItensDto(new List<ItemConferenciaEstoqueDto>(((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens.Where((ItemConferenciaEstoqueDto x) => x.ProdutoServicoEmpresaReferencia != obj.ProdutoServicoEmpresaReferencia)));
			LimparPesquisa();
		}

		protected void AlterarItensDto(IEnumerable<ItemConferenciaEstoqueDto> itensDto)
		{
			((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens = Clone.ObjectGraph<IEnumerable<ItemConferenciaEstoqueDto>>(itensDto);
			((BindableBase)this).RaisePropertyChanged<Lista<ItemConferenciaEstoqueDto>>((Expression<Func<Lista<ItemConferenciaEstoqueDto>>>)(() => Itens));
		}

		private async Task ObterProdutosNaoRelacionados()
		{
			LoaderProcessoService.MostrarLoader("Pesquisando produtos não relacionados...");
			IEnumerable<string> itensRelacionados = Itens.Itens.Select((ItemConferenciaEstoqueDto x) => x.ProdutoServicoEmpresaReferencia);
			await _service.Canal<IConferenciaEstoqueModuleService, Task<IEnumerable<ItemConferenciaEstoqueDto>>>((Func<IConferenciaEstoqueModuleService, Task<IEnumerable<ItemConferenciaEstoqueDto>>>)((IConferenciaEstoqueModuleService x) => x.ObterItensConferenciaEstoqueNaoRelacionados(itensRelacionados))).ContinueWith(delegate(Task<IEnumerable<ItemConferenciaEstoqueDto>> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					IEnumerable<ItemConferenciaEstoqueDto> itensNaoRelacionados = task.Result;
					if (!itensNaoRelacionados.Any())
					{
						DialogoFactory.MostrarDialogoConfirma("Todos os produtos já foram relacionados!", false, HorizontalAlignment.Center);
					}
					else
					{
						ThreadPrincipal.Executar((Action)delegate
						{
							AbrirListaItensNaoRelacionados(itensNaoRelacionados);
						});
					}
				}
			});
		}

		private async Task ImprimirItensNaoRelacionados()
		{
			LoaderProcessoService.MostrarLoader("Pesquisando produtos não relacionados...");
			IEnumerable<string> itensRelacionados = Itens.Itens.Select((ItemConferenciaEstoqueDto x) => x.ProdutoServicoEmpresaReferencia);
			ImpressoItensNaoRelacionados impressoItensNaoRelacionados = new ImpressoItensNaoRelacionados(await _service.Canal<IConferenciaEstoqueModuleService, Task<IEnumerable<ItemConferenciaEstoqueDto>>>((Func<IConferenciaEstoqueModuleService, Task<IEnumerable<ItemConferenciaEstoqueDto>>>)((IConferenciaEstoqueModuleService x) => x.ObterItensConferenciaEstoqueNaoRelacionados(itensRelacionados))));
			LoaderProcessoService.EsconderLoader();
			XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)impressoItensNaoRelacionados).Processar());
		}

		private void AbrirListaItensNaoRelacionados(IEnumerable<ItemConferenciaEstoqueDto> itensNaoRelacionados)
		{
			ItensNaoRelacionados = new Lista<ItemConferenciaEstoqueViewModel>();
			ItensNaoRelacionados.Adicionar(itensNaoRelacionados.Select(ItemConferenciaEstoqueViewModel.Criar));
			ItensNaoRelacionados.PosicionarNoPrimeiro();
			MostrarItensNaoRelacionados = true;
		}

		private bool PodeObterProdutosNaoRelacionados()
		{
			if (HelperObject.IsNotNull((object)Itens) && Itens.Numero > 0)
			{
				if (!DadosSistema.PodeExecutarFuncao(new Type[2]
				{
					typeof(Administrador),
					typeof(AdministradorIndustria)
				}))
				{
					return DadosSistema.EmpresaLogada.ModulosAdicionais.Contains("Conferência de estoque");
				}
				return true;
			}
			return false;
		}

		private void AdicionarProdutosNaoRelacionados()
		{
			IEnumerable<ItemConferenciaEstoqueDto> enumerable = ItensNaoRelacionados.Itens.Select((ItemConferenciaEstoqueViewModel x) => x.ObterDto());
			if (enumerable.Any())
			{
				Itens.Adicionar(enumerable);
				AlterarItensDto(Itens.Itens);
			}
			else
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum produto com quantidade informada para ser adicionado!", false, HorizontalAlignment.Center);
			}
			FecharListaItensNaoRelacionados();
		}

		private void FecharListaItensNaoRelacionados()
		{
			((ViewModelValidationBase)ItensNaoRelacionados).Dispose();
			ItensNaoRelacionados = null;
			MostrarItensNaoRelacionados = false;
		}

		private bool PodeAdicionarProdutosNaoRelacionados()
		{
			if (MostrarItensNaoRelacionados && HelperObject.IsNotNull((object)ItensNaoRelacionados))
			{
				return ItensNaoRelacionados.Numero > 0;
			}
			return false;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ConferenciaEstoqueFormulario(((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Conferências de estoque";
		}

		public string ToTituloFicha()
		{
			return "Conferência de estoque";
		}

		public override void ProjectedAsViewModel()
		{
			Descricao = ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Descricao;
			DataConferencia = ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.DataConferencia;
			Itens.Adicionar(((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Itens);
			Usuario = ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.Usuario;
			UsuarioReferencia = ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.UsuarioReferencia;
			base.Ativo = ((EntityDto)((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto).Ativo;
			EmpresaReferencia = ((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).Dto.EmpresaReferencia;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			LimparPesquisa();
			object obj = dto;
			object obj2 = ((obj is ConferenciaEstoqueDto) ? obj : null);
			((ConferenciaEstoqueDto)obj2).Descricao = Descricao;
			((EntityDto)obj2).Ativo = base.Ativo;
			((ConferenciaEstoqueDto)obj2).Itens = Itens.Itens;
			((ConferenciaEstoqueDto)obj2).DataConferencia = DataConferencia;
			((ConferenciaEstoqueDto)obj2).Usuario = Usuario;
			((ConferenciaEstoqueDto)obj2).UsuarioReferencia = UsuarioReferencia;
			((ConferenciaEstoqueDto)obj2).EmpresaReferencia = EmpresaReferencia;
			((ViewModelDtoBase<ConferenciaEstoqueDto>)(object)this).AtualizarDto<T>(ref dto);
		}

		public override void Dispose()
		{
			Lista<ItemConferenciaEstoqueDto> itens = Itens;
			if (itens != null)
			{
				((ViewModelValidationBase)itens).Dispose();
			}
			Itens = null;
			Lista<ItemConferenciaEstoqueViewModel> itensNaoRelacionados = ItensNaoRelacionados;
			if (itensNaoRelacionados != null)
			{
				((ViewModelValidationBase)itensNaoRelacionados).Dispose();
			}
			ItensNaoRelacionados = null;
			Limpar();
			Lista<KeyValuePair<string, ItemConferenciaEstoqueViewModel>> resultadoBuscaProdutos = ResultadoBuscaProdutos;
			if (resultadoBuscaProdutos != null)
			{
				((ViewModelValidationBase)resultadoBuscaProdutos).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}

		protected virtual void OnAbirMovimentacao(AbrirMovimentacaoEvent e)
		{
			this.AbirMovimentacao?.Invoke(this, e);
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}
	}
	public class ConferenciasEstoqueViewModel : ModuloBase
	{
		public ConferenciasEstoqueViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloConferenciasEstoqueRegion, typeof(ConferenciaEstoqueListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class ItemConferenciaEstoqueViewModel : ViewModelDtoBase<ItemConferenciaEstoqueDto>
	{
		public List<LoteDto> _estoqueLote;

		public List<SerialDto> _estoqueSerial;

		public List<GradeDto> _estoqueGrade;

		public string Descricao { get; set; }

		public decimal QuantidadeInformada
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeInformada));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => QuantidadeInformada), value);
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

		public ItemConferenciaEstoqueViewModel(ItemConferenciaEstoqueDto dto)
			: base(dto)
		{
			EstoqueDto val = Clone.ObjectGraph<EstoqueDto>(dto.EstoqueSistema);
			TipoEstoque = ObterTipoEstoque(val);
			QuantidadeInformada = (val.IsNormal ? 0m : TipoEstoque.Quantidade);
			Descricao = dto.Descricao;
		}

		private TipoEstoqueBaseViewModel ObterTipoEstoque(EstoqueDto estoqueDto)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_0168: Expected O, but got Unknown
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			estoqueDto.PodeAdicionarEstoqueInicial = true;
			TipoEstoque val;
			if (HelperObject.Is<EstoqueLoteDto>((object)estoqueDto))
			{
				IEnumerable<LoteDto> collection = Clone.ObjectGraph<IEnumerable<LoteDto>>(HelperObject.To<EstoqueLoteDto>((object)estoqueDto).Lotes);
				_estoqueLote = new List<LoteDto>(collection);
				foreach (LoteDto lote in ((EstoqueLoteDto)estoqueDto).Lotes)
				{
					lote.Estoque.PodeAdicionarEstoqueInicial = true;
					if (!HelperObject.Is<EstoqueGradeDto>((object)lote.Estoque))
					{
						continue;
					}
					foreach (GradeDto grade in ((EstoqueGradeDto)lote.Estoque).Grades)
					{
						grade.Estoque.PodeAdicionarEstoqueInicial = true;
					}
				}
				val = (TipoEstoque)(object)new Lote<LoteCadastroViewModel>(estoqueDto);
			}
			else if (HelperObject.Is<EstoqueGradeDto>((object)estoqueDto))
			{
				IEnumerable<GradeDto> collection2 = Clone.ObjectGraph<IEnumerable<GradeDto>>(HelperObject.To<EstoqueGradeDto>((object)estoqueDto).Grades);
				_estoqueGrade = new List<GradeDto>(collection2);
				foreach (GradeDto grade2 in ((EstoqueGradeDto)estoqueDto).Grades)
				{
					grade2.Estoque.PodeAdicionarEstoqueInicial = true;
				}
				val = (TipoEstoque)(object)new Grade<GradePrincipalCadastroViewModel>(estoqueDto);
			}
			else if (HelperObject.Is<EstoqueSerialDto>((object)estoqueDto))
			{
				IEnumerable<SerialDto> collection3 = Clone.ObjectGraph<IEnumerable<SerialDto>>(HelperObject.To<EstoqueSerialDto>((object)estoqueDto).Seriais);
				_estoqueSerial = new List<SerialDto>(collection3);
				val = (TipoEstoque)(object)new Serial<SerialCadastroViewModel>(estoqueDto);
			}
			else
			{
				val = (TipoEstoque)new Normal(estoqueDto);
			}
			((ViewModelBase<EstoqueDto>)(object)val.ViewModelBase).ProjectedAsViewModel();
			val.ViewModelBase.ActionSetarQuantidade((Action)delegate
			{
				QuantidadeInformada = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto.Quantidade;
			});
			return val.ViewModelBase;
		}

		public override void ProjectedAsViewModel()
		{
		}

		public static ItemConferenciaEstoqueViewModel Criar(ItemConferenciaEstoqueDto dto)
		{
			return new ItemConferenciaEstoqueViewModel(dto);
		}

		public static ItemConferenciaEstoqueViewModel Criar(ProdutoServicoEmpresaDto produto)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			return ItemConferenciaEstoqueViewModel.Criar(new ItemConferenciaEstoqueDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)produto).Id,
				Codigo = (string.IsNullOrEmpty(produto.ProdutoServico.Produto.CodigoBarras) ? produto.ProdutoServico.CodigoInterno : produto.ProdutoServico.Produto.CodigoBarras),
				Descricao = produto.ProdutoServico.Descricao,
				EstoqueSistema = produto.ProdutoEmpresa.Estoque
			});
		}

		public ItemConferenciaEstoqueDto ObterDto()
		{
			base.Dto.EstoqueInformado = ((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto;
			base.Dto.EstoqueInformado.Quantidade = QuantidadeInformada;
			if (HelperObject.Is<EstoqueLoteDto>((object)base.Dto.EstoqueSistema))
			{
				HelperObject.To<EstoqueLoteDto>((object)base.Dto.EstoqueSistema).Lotes = _estoqueLote;
			}
			if (HelperObject.Is<EstoqueSerialDto>((object)base.Dto.EstoqueSistema))
			{
				HelperObject.To<EstoqueSerialDto>((object)base.Dto.EstoqueSistema).Seriais = _estoqueSerial;
			}
			if (HelperObject.Is<EstoqueGradeDto>((object)base.Dto.EstoqueSistema))
			{
				HelperObject.To<EstoqueGradeDto>((object)base.Dto.EstoqueSistema).Grades = _estoqueGrade;
			}
			return base.Dto;
		}

		public override void Dispose()
		{
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				((ViewModelValidationBase)tipoEstoque).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}

		public void Somar(ItemConferenciaEstoqueDto itemExistente)
		{
			QuantidadeInformada += itemExistente.EstoqueInformado.Quantidade;
		}
	}
}
namespace Otus.Modulo.ConferenciasEstoque.Reports
{
	public class ImpressoItensNaoRelacionados : Relatorio
	{
		private readonly List<ItemConferenciaEstoqueDto> _conferencia;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ObjectDataSource objectDataSource1;

		private XRPageInfo xrPageInfo1;

		private XRLabel xrLabel13;

		private XRLabel lblDataAtual;

		private XRLabel xrLabel14;

		private XRLabel xrLabel15;

		private PageFooterBand PageFooter;

		private XRLabel xrLabel18;

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel tituloDestinatario;

		private XRLabel tituloSituacao;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private PageHeaderBand PageHeader;

		private XRLabel usuario;

		private XRTableCell xrTableCell3;

		private XRTableCell xrTableCell4;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		private XRLabel xrLabel12;

		public override int Id { get; set; }

		public override string Descricao => "Relatório de itens não relacionados na conferência de estoque";

		public override IParametros Parametros { get; set; }

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoItensNaoRelacionados(IEnumerable<ItemConferenciaEstoqueDto> conferencia)
		{
			_conferencia = HelperIEnumerable.ToListEmptyWhenNull<ItemConferenciaEstoqueDto>(conferencia);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XtraReportBase)this).DataSource = _conferencia;
			((XRControl)lblDataAtual).Text = DateTime.Today.ToString("dd/MM/yyyy");
			((XRControl)usuario).Text = string.Format($"Usuário: {HelperString.ToStringEmptyWhenNull(DadosSistema.NomeUsuario)}");
			return (XtraReport)(object)this;
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
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
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
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Expected O, but got Unknown
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Expected O, but got Unknown
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Expected O, but got Unknown
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Expected O, but got Unknown
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Expected O, but got Unknown
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Expected O, but got Unknown
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Expected O, but got Unknown
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Expected O, but got Unknown
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Expected O, but got Unknown
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Expected O, but got Unknown
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_0120: Expected O, but got Unknown
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_012b: Expected O, but got Unknown
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Expected O, but got Unknown
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Expected O, but got Unknown
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Expected O, but got Unknown
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Expected O, but got Unknown
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Expected O, but got Unknown
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Expected O, but got Unknown
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Expected O, but got Unknown
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0228: Unknown result type (might be due to invalid IL or missing references)
			//IL_0332: Unknown result type (might be due to invalid IL or missing references)
			//IL_0338: Expected O, but got Unknown
			//IL_0386: Unknown result type (might be due to invalid IL or missing references)
			//IL_041e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0424: Expected O, but got Unknown
			//IL_0472: Unknown result type (might be due to invalid IL or missing references)
			//IL_050f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0515: Expected O, but got Unknown
			//IL_0563: Unknown result type (might be due to invalid IL or missing references)
			//IL_061d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0623: Expected O, but got Unknown
			//IL_0671: Unknown result type (might be due to invalid IL or missing references)
			//IL_072b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0731: Expected O, but got Unknown
			//IL_077f: Unknown result type (might be due to invalid IL or missing references)
			//IL_083a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0840: Expected O, but got Unknown
			//IL_088e: Unknown result type (might be due to invalid IL or missing references)
			//IL_094c: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b73: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b9c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c61: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f46: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_104a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1074: Unknown result type (might be due to invalid IL or missing references)
			//IL_1142: Unknown result type (might be due to invalid IL or missing references)
			//IL_116b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1245: Unknown result type (might be due to invalid IL or missing references)
			//IL_126e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1348: Unknown result type (might be due to invalid IL or missing references)
			//IL_1371: Unknown result type (might be due to invalid IL or missing references)
			//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_14f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_15fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_170b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1711: Expected O, but got Unknown
			//IL_1750: Unknown result type (might be due to invalid IL or missing references)
			//IL_1779: Unknown result type (might be due to invalid IL or missing references)
			//IL_1824: Unknown result type (might be due to invalid IL or missing references)
			//IL_182a: Expected O, but got Unknown
			//IL_1869: Unknown result type (might be due to invalid IL or missing references)
			//IL_1892: Unknown result type (might be due to invalid IL or missing references)
			//IL_193d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1943: Expected O, but got Unknown
			//IL_1982: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ab: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			XRSummary val3 = new XRSummary();
			Detail = new DetailBand();
			xrTable1 = new XRTable();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			xrTableCell3 = new XRTableCell();
			xrTableCell4 = new XRTableCell();
			xrTableCell5 = new XRTableCell();
			xrTableCell6 = new XRTableCell();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			objectDataSource1 = new ObjectDataSource(components);
			xrPageInfo1 = new XRPageInfo();
			xrLabel13 = new XRLabel();
			lblDataAtual = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel15 = new XRLabel();
			PageFooter = new PageFooterBand();
			usuario = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel2 = new XRLabel();
			tituloDestinatario = new XRLabel();
			tituloSituacao = new XRLabel();
			PageHeader = new PageHeaderBand();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel12 = new XRLabel();
			((ISupportInitialize)xrTable1).BeginInit();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrTable1 });
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 52.91667f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrTable1).Dpi = 254f;
			((XRControl)xrTable1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrTable1).Name = "xrTable1";
			xrTable1.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow1 });
			((XRControl)xrTable1).SizeF = new SizeF(2000f, 52.91667f);
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[6] { xrTableCell1, xrTableCell2, xrTableCell3, xrTableCell4, xrTableCell5, xrTableCell6 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 11.5;
			((XRControl)xrTableCell1).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell1).Borders = (BorderSide)9;
			((XRControl)xrTableCell1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Codigo")
			});
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			((XRControl)xrTableCell1).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell1).StylePriority.UseBorders = false;
			((XRControl)xrTableCell1).StylePriority.UseFont = false;
			((XRControl)xrTableCell1).StylePriority.UsePadding = false;
			xrTableCell1.Weight = 0.09961187523391501;
			((XRControl)xrTableCell2).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell2).Borders = (BorderSide)8;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Descricao")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			((XRControl)xrTableCell2).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell2).StylePriority.UseBorders = false;
			((XRControl)xrTableCell2).StylePriority.UseFont = false;
			((XRControl)xrTableCell2).StylePriority.UsePadding = false;
			xrTableCell2.Weight = 0.26371577117567924;
			((XRControl)xrTableCell3).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell3).Borders = (BorderSide)8;
			((XRControl)xrTableCell3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Custo", "{0:n2}")
			});
			((XRControl)xrTableCell3).Dpi = 254f;
			((XRControl)xrTableCell3).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell3).Name = "xrTableCell3";
			((XRControl)xrTableCell3).Padding = new PaddingInfo(5, 0, 0, 0, 254f);
			((XRControl)xrTableCell3).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell3).StylePriority.UseBorders = false;
			((XRControl)xrTableCell3).StylePriority.UseFont = false;
			((XRControl)xrTableCell3).StylePriority.UsePadding = false;
			((XRControl)xrTableCell3).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell3).TextAlignment = (TextAlignment)4;
			xrTableCell3.Weight = 0.047733794327646795;
			((XRControl)xrTableCell4).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell4).Borders = (BorderSide)8;
			((XRControl)xrTableCell4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoTotal", "{0:n2}")
			});
			((XRControl)xrTableCell4).Dpi = 254f;
			((XRControl)xrTableCell4).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell4).Name = "xrTableCell4";
			((XRControl)xrTableCell4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell4).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell4).StylePriority.UseBorders = false;
			((XRControl)xrTableCell4).StylePriority.UseFont = false;
			((XRControl)xrTableCell4).StylePriority.UsePadding = false;
			((XRControl)xrTableCell4).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell4).TextAlignment = (TextAlignment)4;
			xrTableCell4.Weight = 0.07572879762506331;
			((XRControl)xrTableCell5).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell5).Borders = (BorderSide)8;
			((XRControl)xrTableCell5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EstoqueSistema.Quantidade", "{0:#.###}")
			});
			((XRControl)xrTableCell5).Dpi = 254f;
			((XRControl)xrTableCell5).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell5).Name = "xrTableCell5";
			((XRControl)xrTableCell5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell5).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell5).StylePriority.UseBorders = false;
			((XRControl)xrTableCell5).StylePriority.UseFont = false;
			((XRControl)xrTableCell5).StylePriority.UsePadding = false;
			((XRControl)xrTableCell5).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell5).TextAlignment = (TextAlignment)4;
			xrTableCell5.Weight = 0.10394951804155698;
			((XRControl)xrTableCell6).BorderDashStyle = (BorderDashStyle)2;
			((XRControl)xrTableCell6).Borders = (BorderSide)12;
			((XRControl)xrTableCell6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EstoqueInformado.Quantidade", "{0:#.###}")
			});
			((XRControl)xrTableCell6).Dpi = 254f;
			((XRControl)xrTableCell6).Font = new Font("Segoe UI", 10f);
			((XRControl)xrTableCell6).Name = "xrTableCell6";
			((XRControl)xrTableCell6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell6).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrTableCell6).StylePriority.UseBorders = false;
			((XRControl)xrTableCell6).StylePriority.UseFont = false;
			((XRControl)xrTableCell6).StylePriority.UsePadding = false;
			((XRControl)xrTableCell6).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell6).TextAlignment = (TextAlignment)4;
			xrTableCell6.Weight = 0.11008168188062714;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 87f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 101f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			objectDataSource1.DataSource = typeof(ItemConferenciaEstoqueDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XRControl)xrPageInfo1).Dpi = 254f;
			((XRControl)xrPageInfo1).Font = new Font("Segoe UI", 10f);
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(1767.166f, 0f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrPageInfo1).SizeF = new SizeF(232.834f, 52.81081f);
			((XRControl)xrPageInfo1).StylePriority.UseFont = false;
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(1645.458f, 0f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(121.708f, 50.48249f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Página:";
			((XRControl)lblDataAtual).Dpi = 254f;
			((XRControl)lblDataAtual).Font = new Font("Segoe UI", 10f);
			((XRControl)lblDataAtual).LocationFloat = new PointFloat(1767.166f, 100.6474f);
			((XRControl)lblDataAtual).Name = "lblDataAtual";
			((XRControl)lblDataAtual).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDataAtual).SizeF = new SizeF(232.8337f, 60.85425f);
			((XRControl)lblDataAtual).StylePriority.UseFont = false;
			((XRControl)lblDataAtual).StylePriority.UseTextAlignment = false;
			((XRControl)lblDataAtual).Text = "lblDataAtual";
			((XRControl)lblDataAtual).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1326.479f, 100.6474f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(440.6869f, 60.85423f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Gerado em:";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1326.479f, 161.5017f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(673.5212f, 50.48251f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "DigiSat Tecnologia - www.digisat.com.br";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)4;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)usuario,
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)lblDataAtual,
				(XRControl)xrLabel13,
				(XRControl)xrPageInfo1
			});
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).Font = new Font("Segoe UI Light", 10f);
			((XRControl)PageFooter).HeightF = 211.9842f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)PageFooter).StylePriority.UseFont = false;
			((XRControl)usuario).Dpi = 254f;
			((XRControl)usuario).Font = new Font("Segoe UI", 10f);
			((XRControl)usuario).LocationFloat = new PointFloat(1326.479f, 52.81081f);
			((XRControl)usuario).Name = "usuario";
			((XRControl)usuario).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)usuario).SizeF = new SizeF(673.5206f, 47.83666f);
			((XRControl)usuario).StylePriority.UseFont = false;
			((XRControl)usuario).StylePriority.UseTextAlignment = false;
			((XRControl)usuario).Text = "usuario";
			((XRControl)usuario).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel18).BorderDashStyle = (BorderDashStyle)1;
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 19f);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(0.0006459554f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(1999.999f, 129.8575f);
			((XRControl)xrLabel18).StylePriority.UseBorderDashStyle = false;
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Itens não relacionados na conferência de estoque";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel1).BackColor = Color.LightGray;
			((XRControl)xrLabel1).Borders = (BorderSide)2;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 242.3576f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(13, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(291.041f, 52.99998f);
			((XRControl)xrLabel1).StylePriority.UseBackColor = false;
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UsePadding = false;
			((XRControl)xrLabel1).Text = "Código";
			((XRControl)xrLabel2).BackColor = Color.LightGray;
			((XRControl)xrLabel2).Borders = (BorderSide)2;
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(291.041f, 242.3576f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(745.8212f, 52.99995f);
			((XRControl)xrLabel2).StylePriority.UseBackColor = false;
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Descrição";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)1;
			((XRControl)tituloDestinatario).BackColor = Color.LightGray;
			((XRControl)tituloDestinatario).Borders = (BorderSide)2;
			((XRControl)tituloDestinatario).Dpi = 254f;
			((XRControl)tituloDestinatario).Font = new Font("Segoe UI", 12f);
			((XRControl)tituloDestinatario).LocationFloat = new PointFloat(1389.199f, 242.3576f);
			((XRControl)tituloDestinatario).Name = "tituloDestinatario";
			((XRControl)tituloDestinatario).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloDestinatario).SizeF = new SizeF(296.6504f, 53.00981f);
			((XRControl)tituloDestinatario).StylePriority.UseBackColor = false;
			((XRControl)tituloDestinatario).StylePriority.UseBorders = false;
			((XRControl)tituloDestinatario).StylePriority.UseFont = false;
			((XRControl)tituloDestinatario).StylePriority.UseTextAlignment = false;
			((XRControl)tituloDestinatario).Text = "Qtd. sistema";
			((XRControl)tituloDestinatario).TextAlignment = (TextAlignment)2;
			((XRControl)tituloSituacao).BackColor = Color.LightGray;
			((XRControl)tituloSituacao).Borders = (BorderSide)2;
			((XRControl)tituloSituacao).Dpi = 254f;
			((XRControl)tituloSituacao).Font = new Font("Segoe UI", 12f);
			((XRControl)tituloSituacao).LocationFloat = new PointFloat(1685.849f, 242.3674f);
			((XRControl)tituloSituacao).Name = "tituloSituacao";
			((XRControl)tituloSituacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)tituloSituacao).SizeF = new SizeF(314.1501f, 53.00003f);
			((XRControl)tituloSituacao).StylePriority.UseBackColor = false;
			((XRControl)tituloSituacao).StylePriority.UseBorders = false;
			((XRControl)tituloSituacao).StylePriority.UseFont = false;
			((XRControl)tituloSituacao).StylePriority.UseTextAlignment = false;
			((XRControl)tituloSituacao).Text = "Qtd. física/real";
			((XRControl)tituloSituacao).TextAlignment = (TextAlignment)4;
			((XRControl)PageHeader).Controls.AddRange((XRControl[])(object)new XRControl[7]
			{
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel18,
				(XRControl)tituloSituacao,
				(XRControl)tituloDestinatario,
				(XRControl)xrLabel2,
				(XRControl)xrLabel1
			});
			((XRControl)PageHeader).Dpi = 254f;
			((XRControl)PageHeader).HeightF = 295.3675f;
			((XRControl)PageHeader).Name = "PageHeader";
			((XRControl)xrLabel4).BackColor = Color.LightGray;
			((XRControl)xrLabel4).Borders = (BorderSide)2;
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1173.085f, 242.3675f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(216.1144f, 52.99998f);
			((XRControl)xrLabel4).StylePriority.UseBackColor = false;
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).Text = "Custo total";
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel3).BackColor = Color.LightGray;
			((XRControl)xrLabel3).Borders = (BorderSide)2;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1036.862f, 242.3576f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(136.2223f, 52.99995f);
			((XRControl)xrLabel3).StylePriority.UseBackColor = false;
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "Custo";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)4;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel6,
				(XRControl)xrLabel5,
				(XRControl)xrLabel12
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)xrLabel6).Borders = (BorderSide)2;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EstoqueInformado.Quantidade")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1685.849f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(314.1501f, 58.42f);
			((XRControl)xrLabel6).StylePriority.UseBorders = false;
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:#.###}";
			val.Running = (SummaryRunning)2;
			xrLabel6.Summary = val;
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel5).Borders = (BorderSide)2;
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "EstoqueSistema.Quantidade")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(1389.199f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(296.6506f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseBorders = false;
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:#.###}";
			val2.Running = (SummaryRunning)2;
			xrLabel5.Summary = val2;
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel12).Borders = (BorderSide)2;
			((XRControl)xrLabel12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "CustoTotal")
			});
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(1029.982f, 0f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(359.2172f, 58.42f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			val3.FormatString = "{0:n2}";
			val3.Running = (SummaryRunning)2;
			xrLabel12.Summary = val3;
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)4;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageFooter,
				(Band)PageHeader,
				(Band)GroupFooter1
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(50, 50, 87, 101);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)xrTable1).EndInit();
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class ImpressoConferenciaEstoque : Relatorio
	{
		private readonly List<ConferenciaEstoqueDto> _conferencia;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ObjectDataSource objectDataSource1;

		private DetailReportBand DetailReport;

		private DetailBand Detail1;

		private XRTable xrTable1;

		private XRTableRow xrTableRow1;

		private XRTableCell xrTableCell1;

		private XRTableCell xrTableCell2;

		private XRTableCell xrTableCell5;

		private XRTableCell xrTableCell6;

		private ReportHeaderBand ReportHeader;

		private XRLabel xrLabel4;

		private XRLabel xrLabel3;

		private XRLabel xrLabel2;

		private XRLabel xrLabel1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel9;

		private XRLabel xrLabel8;

		private XRLabel xrLabel7;

		private XRLabel xrLabel6;

		private XRLabel xrLabel5;

		private XRLine xrLine1;

		private XRLabel xrLabel18;

		private PageFooterBand PageFooter;

		private XRLabel xrLabel15;

		private XRLabel xrLabel14;

		private XRLabel lblDataAtual;

		private XRLabel xrLabel13;

		private XRPageInfo xrPageInfo1;

		private XRLine xrLine2;

		private XRTableCell xrTableCell3;

		private XRLabel xrLabel10;

		private GroupFooterBand GroupFooter1;

		private XRLabel xrLabel16;

		private XRLabel xrLabel12;

		private XRLabel xrLabel11;

		public override int Id { get; set; }

		public override string Descricao => "Relatório de conferência de estoque";

		public override IParametros Parametros { get; set; }

		public override bool ExibirNoModuloRelatorios => false;

		public ImpressoConferenciaEstoque(ConferenciaEstoqueDto conferencia)
		{
			_conferencia = new List<ConferenciaEstoqueDto> { conferencia };
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XtraReportBase)this).DataSource = _conferencia;
			((XRControl)lblDataAtual).Text = DateTime.Today.ToString("dd/MM/yyyy");
			return (XtraReport)(object)this;
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
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
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
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Expected O, but got Unknown
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ee: Expected O, but got Unknown
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Expected O, but got Unknown
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Expected O, but got Unknown
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Expected O, but got Unknown
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Expected O, but got Unknown
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Expected O, but got Unknown
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Expected O, but got Unknown
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Expected O, but got Unknown
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_014c: Expected O, but got Unknown
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Expected O, but got Unknown
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Expected O, but got Unknown
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Expected O, but got Unknown
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Expected O, but got Unknown
			//IL_0179: Unknown result type (might be due to invalid IL or missing references)
			//IL_0183: Expected O, but got Unknown
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Expected O, but got Unknown
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0199: Expected O, but got Unknown
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a4: Expected O, but got Unknown
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01af: Expected O, but got Unknown
			//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Expected O, but got Unknown
			//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Expected O, but got Unknown
			//IL_0220: Unknown result type (might be due to invalid IL or missing references)
			//IL_0275: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e6: Expected O, but got Unknown
			//IL_054d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0553: Expected O, but got Unknown
			//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c5: Expected O, but got Unknown
			//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0668: Unknown result type (might be due to invalid IL or missing references)
			//IL_066e: Expected O, but got Unknown
			//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0711: Unknown result type (might be due to invalid IL or missing references)
			//IL_0717: Expected O, but got Unknown
			//IL_074b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0867: Unknown result type (might be due to invalid IL or missing references)
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_090d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0961: Unknown result type (might be due to invalid IL or missing references)
			//IL_098a: Unknown result type (might be due to invalid IL or missing references)
			//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cff: Expected O, but got Unknown
			//IL_0d3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d67: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e18: Expected O, but got Unknown
			//IL_0e57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f31: Expected O, but got Unknown
			//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f99: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_111f: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_1204: Unknown result type (might be due to invalid IL or missing references)
			//IL_120a: Expected O, but got Unknown
			//IL_1249: Unknown result type (might be due to invalid IL or missing references)
			//IL_1272: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_12db: Expected O, but got Unknown
			//IL_131a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1343: Unknown result type (might be due to invalid IL or missing references)
			//IL_13c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1487: Unknown result type (might be due to invalid IL or missing references)
			//IL_14b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1613: Unknown result type (might be due to invalid IL or missing references)
			//IL_16af: Unknown result type (might be due to invalid IL or missing references)
			//IL_16d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1774: Unknown result type (might be due to invalid IL or missing references)
			//IL_179d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1839: Unknown result type (might be due to invalid IL or missing references)
			//IL_1862: Unknown result type (might be due to invalid IL or missing references)
			//IL_18e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_190a: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			XRSummary val = new XRSummary();
			XRSummary val2 = new XRSummary();
			XRSummary val3 = new XRSummary();
			Detail = new DetailBand();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			DetailReport = new DetailReportBand();
			Detail1 = new DetailBand();
			xrTable1 = new XRTable();
			xrTableRow1 = new XRTableRow();
			xrTableCell1 = new XRTableCell();
			xrTableCell2 = new XRTableCell();
			xrTableCell5 = new XRTableCell();
			xrTableCell6 = new XRTableCell();
			xrTableCell3 = new XRTableCell();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel10 = new XRLabel();
			xrLine2 = new XRLine();
			xrLabel9 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel5 = new XRLabel();
			GroupFooter1 = new GroupFooterBand();
			xrLabel16 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel11 = new XRLabel();
			objectDataSource1 = new ObjectDataSource(components);
			ReportHeader = new ReportHeaderBand();
			xrLabel18 = new XRLabel();
			xrLine1 = new XRLine();
			xrLabel4 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel1 = new XRLabel();
			PageFooter = new PageFooterBand();
			xrLabel15 = new XRLabel();
			xrLabel14 = new XRLabel();
			lblDataAtual = new XRLabel();
			xrLabel13 = new XRLabel();
			xrPageInfo1 = new XRPageInfo();
			((ISupportInitialize)xrTable1).BeginInit();
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 0f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 101f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 101f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XtraReportBase)DetailReport).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail1,
				(Band)GroupHeader1,
				(Band)GroupFooter1
			});
			((XtraReportBase)DetailReport).DataMember = "Itens";
			((XtraReportBase)DetailReport).DataSource = objectDataSource1;
			((XRControl)DetailReport).Dpi = 254f;
			DetailReport.Level = 0;
			((XRControl)DetailReport).Name = "DetailReport";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrTable1 });
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).Font = new Font("Segoe UI", 12f);
			((XRControl)Detail1).HeightF = 63.99997f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)Detail1).StylePriority.UseFont = false;
			((XRControl)xrTable1).Dpi = 254f;
			((XRControl)xrTable1).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrTable1).Name = "xrTable1";
			xrTable1.Rows.AddRange((XRTableRow[])(object)new XRTableRow[1] { xrTableRow1 });
			((XRControl)xrTable1).SizeF = new SizeF(2000f, 63.5f);
			xrTableRow1.Cells.AddRange((XRTableCell[])(object)new XRTableCell[5] { xrTableCell1, xrTableCell2, xrTableCell5, xrTableCell6, xrTableCell3 });
			((XRControl)xrTableRow1).Dpi = 254f;
			((XRControl)xrTableRow1).Name = "xrTableRow1";
			xrTableRow1.Weight = 11.5;
			((XRControl)xrTableCell1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Codigo")
			});
			((XRControl)xrTableCell1).Dpi = 254f;
			((XRControl)xrTableCell1).Name = "xrTableCell1";
			((XRControl)xrTableCell1).Text = "xrTableCell1";
			xrTableCell1.Weight = 0.11138443769648027;
			((XRControl)xrTableCell2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.Descricao")
			});
			((XRControl)xrTableCell2).Dpi = 254f;
			((XRControl)xrTableCell2).Name = "xrTableCell2";
			((XRControl)xrTableCell2).Text = "xrTableCell2";
			xrTableCell2.Weight = 0.29753148446180877;
			((XRControl)xrTableCell5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.EstoqueInformado.Quantidade", "{0:#.###}")
			});
			((XRControl)xrTableCell5).Dpi = 254f;
			((XRControl)xrTableCell5).Name = "xrTableCell5";
			((XRControl)xrTableCell5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell5).StylePriority.UsePadding = false;
			((XRControl)xrTableCell5).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell5).TextAlignment = (TextAlignment)2;
			xrTableCell5.Weight = 0.09598981583953191;
			((XRControl)xrTableCell6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.EstoqueSistema.Quantidade", "{0:#.###}")
			});
			((XRControl)xrTableCell6).Dpi = 254f;
			((XRControl)xrTableCell6).Name = "xrTableCell6";
			((XRControl)xrTableCell6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell6).StylePriority.UsePadding = false;
			((XRControl)xrTableCell6).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell6).TextAlignment = (TextAlignment)2;
			xrTableCell6.Weight = 0.1041399008014654;
			((XRControl)xrTableCell3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.DiferencaQuantidadeEstoqueInformadoMenosEstoqueSistema", "{0:#.###}")
			});
			((XRControl)xrTableCell3).Dpi = 254f;
			((XRControl)xrTableCell3).Name = "xrTableCell3";
			((XRControl)xrTableCell3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrTableCell3).StylePriority.UsePadding = false;
			((XRControl)xrTableCell3).StylePriority.UseTextAlignment = false;
			((XRControl)xrTableCell3).TextAlignment = (TextAlignment)2;
			xrTableCell3.Weight = 0.07547563512565296;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[7]
			{
				(XRControl)xrLabel10,
				(XRControl)xrLine2,
				(XRControl)xrLabel9,
				(XRControl)xrLabel8,
				(XRControl)xrLabel7,
				(XRControl)xrLabel6,
				(XRControl)xrLabel5
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).Font = new Font("Segoe UI", 12f);
			((XRControl)GroupHeader1).HeightF = 121.92f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)GroupHeader1).StylePriority.UseFont = false;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1779.479f, 58.42004f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(220.5204f, 58.42f);
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Diferença";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine2).Dpi = 254f;
			xrLine2.LineWidth = 3;
			((XRControl)xrLine2).LocationFloat = new PointFloat(0f, 116.84f);
			((XRControl)xrLine2).Name = "xrLine2";
			((XRControl)xrLine2).SizeF = new SizeF(2000f, 5.080017f);
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).LocationFloat = new PointFloat(1194.75f, 0f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(805.2499f, 58.42f);
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).Text = "Quantidade";
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1475.208f, 58.42004f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(304.2708f, 58.42001f);
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Sistema";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).LocationFloat = new PointFloat(1194.75f, 58.41996f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(280.4583f, 58.42f);
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Informada";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).LocationFloat = new PointFloat(325.4375f, 58.42004f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(869.3126f, 58.42001f);
			((XRControl)xrLabel6).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel6).Text = "Descrição";
			((XRControl)xrLabel6).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).LocationFloat = new PointFloat(0f, 58.42f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(325.4375f, 58.42f);
			((XRControl)xrLabel5).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel5).Text = "Código";
			((XRControl)xrLabel5).TextAlignment = (TextAlignment)1;
			((XRControl)GroupFooter1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel16,
				(XRControl)xrLabel12,
				(XRControl)xrLabel11
			});
			((XRControl)GroupFooter1).Dpi = 254f;
			((XRControl)GroupFooter1).Font = new Font("Segoe UI", 9.75f);
			((XRControl)GroupFooter1).HeightF = 58.42f;
			((XRControl)GroupFooter1).Name = "GroupFooter1";
			((XRControl)GroupFooter1).StylePriority.UseFont = false;
			((XRControl)xrLabel16).Borders = (BorderSide)2;
			((XRControl)xrLabel16).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.DiferencaQuantidadeEstoqueInformadoMenosEstoqueSistema")
			});
			((XRControl)xrLabel16).Dpi = 254f;
			((XRControl)xrLabel16).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel16).LocationFloat = new PointFloat(1779.479f, 0f);
			((XRControl)xrLabel16).Name = "xrLabel16";
			((XRControl)xrLabel16).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel16).SizeF = new SizeF(220.5206f, 58.42f);
			((XRControl)xrLabel16).StylePriority.UseBorders = false;
			((XRControl)xrLabel16).StylePriority.UseFont = false;
			((XRControl)xrLabel16).StylePriority.UseTextAlignment = false;
			val.FormatString = "{0:#.###}";
			val.Running = (SummaryRunning)2;
			xrLabel16.Summary = val;
			((XRControl)xrLabel16).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel12).Borders = (BorderSide)2;
			((XRControl)xrLabel12).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.EstoqueSistema.Quantidade")
			});
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(1475.208f, 0f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(304.2708f, 58.42f);
			((XRControl)xrLabel12).StylePriority.UseBorders = false;
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).StylePriority.UseTextAlignment = false;
			val2.FormatString = "{0:#.###}";
			val2.Running = (SummaryRunning)2;
			xrLabel12.Summary = val2;
			((XRControl)xrLabel12).TextAlignment = (TextAlignment)2;
			((XRControl)xrLabel11).Borders = (BorderSide)2;
			((XRControl)xrLabel11).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Itens.EstoqueInformado.Quantidade")
			});
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(1194.75f, 0f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(280.4583f, 58.42f);
			((XRControl)xrLabel11).StylePriority.UseBorders = false;
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).StylePriority.UseTextAlignment = false;
			val3.FormatString = "{0:#.###}";
			val3.Running = (SummaryRunning)2;
			xrLabel11.Summary = val3;
			((XRControl)xrLabel11).TextAlignment = (TextAlignment)2;
			objectDataSource1.DataSource = typeof(ConferenciaEstoqueDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[6]
			{
				(XRControl)xrLabel18,
				(XRControl)xrLine1,
				(XRControl)xrLabel4,
				(XRControl)xrLabel3,
				(XRControl)xrLabel2,
				(XRControl)xrLabel1
			});
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 196.215f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(2000f, 74.295f);
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).Text = "Conferências de estoque";
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)2;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(0f, 191.135f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(2000f, 5.080017f);
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "DataConferencia", "{0:dddd, d' de 'MMMM' de 'yyyy HH:mm}")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(493.7275f, 132.715f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(1506.272f, 58.42f);
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).Text = "xrLabel4";
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Usuario")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 12f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(181.5193f, 74.29501f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(1818.481f, 58.41999f);
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).Text = "xrLabel3";
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 74.29501f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(181.5192f, 58.41999f);
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Usuário:";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI Semibold", 12f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(0f, 132.715f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(493.7276f, 58.42f);
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Data e hora conferência:";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)1;
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel15,
				(XRControl)xrLabel14,
				(XRControl)lblDataAtual,
				(XRControl)xrLabel13,
				(XRControl)xrPageInfo1
			});
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).Font = new Font("Segoe UI Light", 10f);
			((XRControl)PageFooter).HeightF = 161.7134f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)PageFooter).StylePriority.UseFont = false;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(1326.479f, 111.2309f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(673.5212f, 50.48251f);
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "DigiSat Tecnologia - www.digisat.com.br";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(1576.666f, 52.81081f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(211.6666f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).Text = "Gerado em";
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)4;
			((XRControl)lblDataAtual).Dpi = 254f;
			((XRControl)lblDataAtual).Font = new Font("Segoe UI", 10f);
			((XRControl)lblDataAtual).LocationFloat = new PointFloat(1788.333f, 52.81081f);
			((XRControl)lblDataAtual).Name = "lblDataAtual";
			((XRControl)lblDataAtual).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDataAtual).SizeF = new SizeF(211.667f, 58.41999f);
			((XRControl)lblDataAtual).StylePriority.UseFont = false;
			((XRControl)lblDataAtual).StylePriority.UseTextAlignment = false;
			((XRControl)lblDataAtual).Text = "lblDataAtual";
			((XRControl)lblDataAtual).TextAlignment = (TextAlignment)4;
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI", 10f);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(1666.625f, 2.328346f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(121.708f, 50.48249f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Página:";
			((XRControl)xrPageInfo1).Dpi = 254f;
			((XRControl)xrPageInfo1).Font = new Font("Segoe UI", 10f);
			((XRControl)xrPageInfo1).LocationFloat = new PointFloat(1788.333f, 2.328346f);
			((XRControl)xrPageInfo1).Name = "xrPageInfo1";
			((XRControl)xrPageInfo1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrPageInfo1).SizeF = new SizeF(211.6675f, 50.48249f);
			((XRControl)xrPageInfo1).StylePriority.UseFont = false;
			((XRControl)xrPageInfo1).StylePriority.UseTextAlignment = false;
			((XRControl)xrPageInfo1).TextAlignment = (TextAlignment)4;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)DetailReport,
				(Band)ReportHeader,
				(Band)PageFooter
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[1] { (IComponent)objectDataSource1 });
			((XtraReportBase)this).DataSource = objectDataSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(50, 50, 101, 101);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)xrTable1).EndInit();
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.ConferenciasEstoque.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.ConferenciasEstoque.Properties.Resources", typeof(Resources).Assembly);
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

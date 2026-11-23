using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Objetos;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Financeiros.CentrosCusto;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.GestaoContratos;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Financeiros.PlanosConta;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Application.Main.Dto.Movimentacoes.Relatorios;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Financeiros.EspeciesPagamento;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Function;
using DigisatServer.Infrastructure.Crosscutting.Function.ModeloContrato;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Model.StatusContrato;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Projection.Financeiros;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.ValueObject;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Especies.Pagamentos;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.Reports;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.GestaoContratos.Function;
using Otus.Modulo.GestaoContratos.Reports;
using Otus.Modulo.GestaoContratos.Views;
using Otus.Modulo.GestaoContratos.Views.TiposContrato;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GestaoContratos")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GestaoContratos")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GestaoContratos
{
	[Module(ModuleName = "GestaoContratoModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("PlanoContaModulo")]
	[ModuleDependency("CentroCustoModulo")]
	[ModuleDependency("MovimentacaoModulo")]
	[ModuleDependency("RecebimentoModulo")]
	[ModuleDependency("ContaModulo")]
	[ModuleDependency("VisualizarAnexosModulo")]
	public class GestaoContratoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GestaoContratoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GestaoContratosView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoContratoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoContratoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GestaoContratoFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<TipoContratoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<TipoContratoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<RenovarContratoFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<RenovarContratosEmMassaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GestaoContratos.Views
{
	public class RenovarContratoFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public RenovarContratoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/renovarcontratofichaview.xaml", UriKind.Relative);
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
	public class RenovarContratosEmMassaFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public RenovarContratosEmMassaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/renovarcontratosemmassafichaview.xaml", UriKind.Relative);
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
	public class GestaoContratoFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GestaoContratoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaadicionarview.xaml", UriKind.Relative);
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
	public class GestaoContratoFichaAtualizarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GestaoContratoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaatualizarview.xaml", UriKind.Relative);
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
	public class GestaoContratoListaView : CustomUserControl, IComponentConnector
	{
		internal TextBlock Aguarde;

		internal TextBlock Processando;

		private bool _contentLoaded;

		public GestaoContratoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratolistaview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				Aguarde = (TextBlock)target;
				break;
			case 2:
				Processando = (TextBlock)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class GestaoContratosView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoContratosView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratosview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoContratos.Views.TiposContrato
{
	public class TipoContratoFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public TipoContratoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/tiposcontrato/tipocontratofichaadicionarview.xaml", UriKind.Relative);
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
	public class TipoContratoListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public TipoContratoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/tiposcontrato/tipocontratolistaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoContratos.Views.GestaoContratoFichaViewDetalhes
{
	public class ImpressaoContratoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterObservacao;

		internal System.Windows.Controls.Button BtnCamposDisponiveis;

		private bool _contentLoaded;

		public ImpressaoContratoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaviewdetalhes/impressaocontratoview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParameterObservacao = (ButtonInfo)target;
				break;
			case 2:
				BtnCamposDisponiveis = (System.Windows.Controls.Button)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class RenovarContratosEmMassaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public RenovarContratosEmMassaView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaviewdetalhes/renovarcontratosemmassaview.xaml", UriKind.Relative);
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
	public class RenovarContratoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public RenovarContratoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaviewdetalhes/renovarcontratoview.xaml", UriKind.Relative);
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
	public class GestaoContratoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterPessoa;

		internal ButtonInfo RemoverTipoContrato;

		internal ButtonInfo ParameterTipoContrato;

		internal ButtonInfo ParameterServico;

		internal ButtonInfo RemoverObjeto;

		internal ButtonInfo ParameterObjeto;

		internal ButtonInfo ParameterConta;

		internal ButtonInfo ParameterCentroCusto;

		internal ButtonInfo ParameterPlanoConta;

		private bool _contentLoaded;

		public GestaoContratoView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaviewdetalhes/gestaocontratoview.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Expected O, but got Unknown
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected O, but got Unknown
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParameterPessoa = (ButtonInfo)target;
				break;
			case 2:
				RemoverTipoContrato = (ButtonInfo)target;
				break;
			case 3:
				ParameterTipoContrato = (ButtonInfo)target;
				break;
			case 4:
				ParameterServico = (ButtonInfo)target;
				break;
			case 5:
				RemoverObjeto = (ButtonInfo)target;
				break;
			case 6:
				ParameterObjeto = (ButtonInfo)target;
				break;
			case 7:
				ParameterConta = (ButtonInfo)target;
				break;
			case 8:
				ParameterCentroCusto = (ButtonInfo)target;
				break;
			case 9:
				ParameterPlanoConta = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
	public class GestaoContratoAnexosView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GestaoContratoAnexosView()
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
				Uri resourceLocator = new Uri("/Modulo.GestaoContratos;component/views/gestaocontratofichaviewdetalhes/gestaocontratoanexosview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GestaoContratos.ViewModels
{
	public class ServicosContratoViewModel : BindableBase
	{
		public ServicoContratoDto ServicoContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<ServicoContratoDto>((Expression<Func<ServicoContratoDto>>)(() => ServicoContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<ServicoContratoDto>((Expression<Func<ServicoContratoDto>>)(() => ServicoContrato), value);
			}
		}

		public decimal ValorOriginal
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorOriginal));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorOriginal), value);
			}
		}

		public ICommand RemoverCommand { get; set; }

		public ServicosContratoViewModel(ServicoContratoDto servicoContrato, Action<object> removerProduto)
		{
			ServicoContrato = servicoContrato;
			ValorOriginal = servicoContrato.Valor;
			RemoverCommand = (ICommand)new DelegateCommand<object>(removerProduto, true);
		}
	}
	public class ImpressaoContratoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ImpressaoContratoAbaViewModel(IViewModelDtoBase viewModel)
			: base("Impressão contrato", "Dados do contrato", viewModel)
		{
		}
	}
	public class ImpressaoContratoViewModel : ViewModelDtoBase<GestaoContratoDto>
	{
		private string _arquivoModeloBase64;

		private bool _removerModelo;

		public bool PodePesquisarObservacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodePesquisarObservacao));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodePesquisarObservacao), value);
			}
		}

		public List<KeyValuePair<int, string>> TiposImpressao { get; } = new List<KeyValuePair<int, string>>
		{
			new KeyValuePair<int, string>(0, "Texto digitado"),
			new KeyValuePair<int, string>(1, "Arquivo de contrato")
		};


		public KeyValuePair<int, string> TipoImpressaoSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<KeyValuePair<int, string>>((Expression<Func<KeyValuePair<int, string>>>)(() => TipoImpressaoSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<KeyValuePair<int, string>>((Expression<Func<KeyValuePair<int, string>>>)(() => TipoImpressaoSelecionado), value, (Action)delegate
				{
					IsModoArquivo = value.Key == 1;
					IsModoTexto = !IsModoArquivo;
					base.Dto.TipoImpressaoContrato = value.Key;
				});
			}
		}

		public bool IsModoTexto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsModoTexto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsModoTexto), value);
			}
		}

		public bool IsModoArquivo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsModoArquivo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsModoArquivo), value);
			}
		}

		public string NomeArquivoModelo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeArquivoModelo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeArquivoModelo), value);
			}
		}

		public Lista<ArquivoViewModel> Modelos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Modelos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Modelos), value);
			}
		}

		public bool CamposAbertos
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => CamposAbertos));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => CamposAbertos), value);
			}
		}

		public IReadOnlyList<CampoContrato> CamposDisponiveis => CamposContrato.CamposParaExibir;

		public CampoContrato CampoSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<CampoContrato>((Expression<Func<CampoContrato>>)(() => CampoSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<CampoContrato>((Expression<Func<CampoContrato>>)(() => CampoSelecionado), value, (Action)delegate
				{
					if (value != null)
					{
						CopiarChave(value.TextoCopia);
					}
				});
			}
		}

		public SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>> ObservacaoCommandParameter => new SelecaoCommandParameter<IEnumerable<ObservacaoDadoAdicionalDto>>("ObservacaoDadoAdicionalListaView", (Action<IEnumerable<ObservacaoDadoAdicionalDto>>)delegate(IEnumerable<ObservacaoDadoAdicionalDto> observacao)
		{
			if (!string.IsNullOrEmpty(DadosContrato))
			{
				DadosContrato = DadosContrato.TrimEnd() + " ";
			}
			DadosContrato += string.Join(" ", observacao.Select((ObservacaoDadoAdicionalDto x) => x.Conteudo));
		}, true);

		public string DadosContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DadosContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DadosContrato), value, (Action)delegate
				{
					base.Dto.DadosContrato = DadosContrato;
				});
			}
		}

		public bool ImprimirPagamentoContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ImprimirPagamentoContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ImprimirPagamentoContrato), value);
			}
		}

		public ICommand SelecionarArquivoModeloCommand { get; set; }

		public ICommand RemoverModeloCommand { get; set; }

		public ICommand VisualizarModeloCommand { get; set; }

		public ICommand AbrirCamposCommand { get; set; }

		public ImpressaoContratoViewModel(GestaoContratoDto dto)
			: base(dto)
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Expected O, but got Unknown
			PodePesquisarObservacao = DadosSistema.PodeExecutarFuncao(new Type[3]
			{
				typeof(Gerencial),
				typeof(Administrador),
				typeof(AdministradorIndustria)
			});
			Modelos = new Lista<ArquivoViewModel>();
			SelecionarArquivoModeloCommand = (ICommand)new DelegateCommand((Action)SelecionarArquivoModelo);
			RemoverModeloCommand = (ICommand)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)RemoverModelo);
			VisualizarModeloCommand = (ICommand)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)VisualizarModelo);
			AbrirCamposCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				CamposAbertos = !CamposAbertos;
			});
		}

		public void SelecionarArquivoModelo()
		{
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Expected O, but got Unknown
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione o modelo de contrato",
				Filter = "Documentos (*.docx;*.doc;*.odt)|*.docx;*.doc;*.odt"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string text = (Path.GetExtension(openFileDialog.FileName) ?? string.Empty).ToLowerInvariant();
				if (text != ".docx" && text != ".doc" && text != ".odt")
				{
					DialogoFactory.MostrarDialogoConfirma("Selecione um arquivo .docx, .doc ou .odt.", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				byte[] inArray = File.ReadAllBytes(openFileDialog.FileName);
				_arquivoModeloBase64 = Convert.ToBase64String(inArray);
				_removerModelo = false;
				NomeArquivoModelo = openFileDialog.SafeFileName;
				Modelos.Limpar();
				Modelos.Adicionar(new ArquivoViewModel(_arquivoModeloBase64, openFileDialog.FileName, openFileDialog.SafeFileName, HelperDate.FormatarData(DateTime.Now, (FormatoData)6)));
			}
		}

		private void RemoverModelo(ArquivoViewModel arquivo)
		{
			Modelos.Remover(arquivo);
			_arquivoModeloBase64 = null;
			NomeArquivoModelo = string.Empty;
			_removerModelo = true;
		}

		private void VisualizarModelo(ArquivoViewModel arquivo)
		{
			if (arquivo == null)
			{
				return;
			}
			try
			{
				string text = ((!HelperString.IsNullOrEmpty(_arquivoModeloBase64)) ? _arquivoModeloBase64 : ObterModeloBase64DoServidor());
				if (HelperString.IsNullOrEmpty(text))
				{
					DialogoFactory.MostrarDialogoConfirma("Modelo de contrato não encontrado.", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				string text2 = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Temp");
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				string text3 = Path.Combine(text2, arquivo.NomeArquivo);
				File.WriteAllBytes(text3, Convert.FromBase64String(text));
				Process.Start(text3);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private string ObterModeloBase64DoServidor()
		{
			if (HelperString.IsNullOrEmpty(((EntityBaseDto)base.Dto).Id))
			{
				return string.Empty;
			}
			byte[] result = DadosSistema.GetService().Canal<IGestaoContratoModuleService, Task<byte[]>>((Func<IGestaoContratoModuleService, Task<byte[]>>)((IGestaoContratoModuleService service) => service.ObterModeloContrato(((EntityBaseDto)base.Dto).Id))).Result;
			if (result != null && result.Length != 0)
			{
				return Convert.ToBase64String(result);
			}
			return string.Empty;
		}

		private void CopiarChave(string chave)
		{
			if (HelperString.IsNullOrEmpty(chave))
			{
				return;
			}
			try
			{
				System.Windows.Clipboard.SetText(chave);
			}
			catch (Exception)
			{
				try
				{
					System.Windows.Clipboard.SetDataObject(chave);
				}
				catch (Exception)
				{
				}
			}
		}

		public override void ProjectedAsViewModel()
		{
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Expected O, but got Unknown
			if (string.IsNullOrEmpty(base.Dto.DadosContrato) && ((EntityBaseDto)base.Dto).IsTransient() && !string.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Financeiro.ObservacaoContratoPadrao.Referencia))
			{
				DadosContrato = ObterObservacaoPadrao(DadosSistema.ConfiguracaoDto.Financeiro.ObservacaoContratoPadrao.Referencia);
			}
			else
			{
				DadosContrato = base.Dto.DadosContrato;
			}
			ImprimirPagamentoContrato = base.Dto.ImprimirPagamentoContrato;
			TipoImpressaoSelecionado = TiposImpressao.FirstOrDefault((KeyValuePair<int, string> x) => x.Key == base.Dto.TipoImpressaoContrato);
			_removerModelo = false;
			_arquivoModeloBase64 = null;
			Modelos.Limpar();
			NomeArquivoModelo = ObterNomeAmigavel(base.Dto.NomeArquivoModeloContrato);
			if (!HelperString.IsNullOrEmpty(NomeArquivoModelo))
			{
				string text = ((base.Dto.DataModeloContrato == DateTime.MinValue) ? string.Empty : HelperDate.FormatarData(base.Dto.DataModeloContrato, (FormatoData)6));
				Modelos.Adicionar(new ArquivoViewModel((string)null, NomeArquivoModelo, NomeArquivoModelo, text));
			}
		}

		private string ObterNomeAmigavel(string nomeArmazenado)
		{
			if (HelperString.IsNullOrEmpty(nomeArmazenado))
			{
				return string.Empty;
			}
			string text = ((EntityBaseDto)base.Dto).Id + "-";
			if (!nomeArmazenado.StartsWith(text))
			{
				return nomeArmazenado;
			}
			return nomeArmazenado.Substring(text.Length);
		}

		public string ObterObservacaoPadrao(string referencia)
		{
			return DadosSistema.GetService().Canal<IObservacaoDadoAdicionalModuleService, ObservacaoDadoAdicionalDto>((Func<IObservacaoDadoAdicionalModuleService, ObservacaoDadoAdicionalDto>)((IObservacaoDadoAdicionalModuleService x) => x.ObterObservacao(referencia))).Conteudo;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GestaoContratoDto val = (GestaoContratoDto)((obj is GestaoContratoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.DadosContrato = DadosContrato;
				val.ImprimirPagamentoContrato = ImprimirPagamentoContrato;
				val.TipoImpressaoContrato = TipoImpressaoSelecionado.Key;
				val.RemoverModeloContrato = _removerModelo;
				if (!HelperString.IsNullOrEmpty(_arquivoModeloBase64))
				{
					val.ArquivoModeloContratoBase64 = _arquivoModeloBase64;
					val.NomeArquivoModeloContrato = NomeArquivoModelo;
					val.DataModeloContrato = DateTime.Now;
				}
				else if (_removerModelo)
				{
					val.NomeArquivoModeloContrato = string.Empty;
					val.CaminhoModeloContrato = string.Empty;
					val.DataModeloContrato = DateTime.MinValue;
				}
				base.AtualizarDto<GestaoContratoDto>(ref val);
			}
		}
	}
	public class RenovarContratoAbaViewModel : AbaPrincipalViewModelBase
	{
		public RenovarContratoAbaViewModel(IViewModelPrincipal viewModel)
			: base("RENOVAR CONTRATO", "Nova data inicial, final e valor", viewModel)
		{
		}
	}
	public class RenovarContratoFichaViewModel : FichaBase<GestaoContratoDto, RenovarContratoViewModel>
	{
		public GestaoContratoDto Dto { get; set; }

		public ICommand GravarCommand { get; set; }

		public RenovarContratoFichaViewModel()
			: base("Renovar contrato")
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			GravarCommand = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			base.DisableConfirmOnClose();
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
			IWService service = DadosSistema.GetService();
			GestaoContratoDto dto = ((FormularioEntity<GestaoContratoDto>)(RenovarContratoFormulario)(object)((FichaBase<GestaoContratoDto, RenovarContratoViewModel>)this).Formulario).ToDto<GestaoContratoDto>();
			await service.Canal<IGestaoContratoModuleService, Task<GestaoContratoDto>>((Func<IGestaoContratoModuleService, Task<GestaoContratoDto>>)((IGestaoContratoModuleService a) => ((IAtualizar<GestaoContratoDto>)(object)a).AtualizarAsync(dto)));
			service.Canal<IGestaoContratoModuleService, GestaoContratoDto>((Func<IGestaoContratoModuleService, GestaoContratoDto>)((IGestaoContratoModuleService x) => x.Renovar(new GestaoContratoHistoricoDto
			{
				IdContrato = ((EntityBaseDto)Dto).Id
			})));
			((NavigationBase)this).FecharView();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			base.OnParametroInformado(sender, navigationParameters);
			object obj = navigationParameters["Dto"];
			Dto = (GestaoContratoDto)((obj is GestaoContratoDto) ? obj : null);
		}
	}
	public class RenovarContratoFormulario : FormularioEntity<GestaoContratoDto>
	{
		public RenovarContratoFormulario(GestaoContratoDto contrato)
			: base(contrato)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new RenovarContratoAbaViewModel((IViewModelPrincipal)(object)new RenovarContratoViewModel(base.Dto)),
				(IAbaViewModelBase)new ImpressaoContratoAbaViewModel((IViewModelDtoBase)(object)new ImpressaoContratoViewModel(base.Dto))
			});
		}
	}
	public class RenovarContratoViewModel : ViewModelDtoBase<GestaoContratoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public decimal ValorPercentual
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorPercentual));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorPercentual), value, (Action)CalcularDescontoPercentual);
			}
		}

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

		public Lista<ServicosContratoViewModel> ServicosContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ServicosContratoViewModel>>((Expression<Func<Lista<ServicosContratoViewModel>>>)(() => ServicosContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ServicosContratoViewModel>>((Expression<Func<Lista<ServicosContratoViewModel>>>)(() => ServicosContrato), value);
			}
		}

		public RenovarContratoViewModel()
		{
		}

		public RenovarContratoViewModel(GestaoContratoDto dto)
			: base(dto)
		{
		}

		private void CalcularDescontoPercentual()
		{
			foreach (ServicosContratoViewModel iten in ServicosContrato.Itens)
			{
				iten.ServicoContrato.Valor = HelperDecimal.ArredondarParaDuasCasasDecimais(Calculos.CalcularAcrescimo(iten.ValorOriginal, ValorPercentual));
			}
			((BindableBase)this).RaisePropertyChanged<Lista<ServicosContratoViewModel>>((Expression<Func<Lista<ServicosContratoViewModel>>>)(() => ServicosContrato));
		}

		private ServicosContratoViewModel ObterServicosViewModel(ServicoContratoDto servico)
		{
			return new ServicosContratoViewModel(servico, RemoverServico);
		}

		private void RemoverServico(object servicoViewModel)
		{
			ServicosContrato.Remover(servicoViewModel as ServicosContratoViewModel);
		}

		public override void ProjectedAsViewModel()
		{
			DataInicial = base.Dto.DataInicial;
			DataFinal = base.Dto.DataFinal.AddYears(1);
			ValorPercentual = ValorPercentual;
			ServicosContrato = new Lista<ServicosContratoViewModel>();
			ServicosContrato.Adicionar(base.Dto.Servicos.Select(ObterServicosViewModel));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			base.Dto.DataInicial = DataInicial;
			base.Dto.DataFinal = DataFinal;
			base.Dto.Servicos = ServicosContrato.Itens.Select((ServicosContratoViewModel x) => x.ServicoContrato);
			base.AtualizarDto<T>(ref dto);
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new RenovarContratoFormulario(base.Dto);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
	public class ContratoRenovacaoEmMassaViewModel : BindableBase
	{
		private readonly Action _alterouSelecao;

		public GestaoContratoDto Dto { get; }

		public decimal ValorOriginal { get; }

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

		public DateTime NovaDataFinal
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => NovaDataFinal));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => NovaDataFinal), value);
			}
		}

		public bool Selecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Selecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Selecionado), value, (Action)delegate
				{
					_alterouSelecao?.Invoke();
				});
			}
		}

		public ContratoRenovacaoEmMassaViewModel(GestaoContratoDto dto, Action alterouSelecao)
		{
			Dto = dto;
			ValorOriginal = dto.Valor;
			Valor = dto.Valor;
			_alterouSelecao = alterouSelecao;
		}

		public void AplicarProrrogacao(int meses)
		{
			NovaDataFinal = Dto.DataFinal.AddMonths(meses);
		}
	}
	public class RenovarContratosEmMassaAbaViewModel : AbaPrincipalViewModelBase
	{
		public RenovarContratosEmMassaAbaViewModel(IViewModelPrincipal viewModel)
			: base("RENOVAÇÃO EM MASSA", "Filtre e renove em massa", viewModel)
		{
		}
	}
	public class RenovarContratosEmMassaFichaViewModel : FichaBase<GestaoContratoDto, RenovarContratosEmMassaViewModel>
	{
		private RenovarContratosEmMassaViewModel _viewModel;

		public ICommand GravarCommand { get; set; }

		public RenovarContratosEmMassaFichaViewModel()
			: base("Renovar contratos em massa")
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			GravarCommand = (ICommand)new AsyncCommand((Func<Task>)Renovar, (Func<bool>)PodeRenovar, (bool?)null);
			base.DisableConfirmOnClose();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			base.OnParametroInformado(sender, navigationParameters);
			_viewModel = navigationParameters["RenovarContratosEmMassaViewModel"] as RenovarContratosEmMassaViewModel;
		}

		private bool PodeRenovar()
		{
			if (HelperObject.IsNotNull((object)base.Formulario) && HelperObject.IsNotNull((object)_viewModel))
			{
				return _viewModel.ContratosSelecionados.Any();
			}
			return false;
		}

		private async Task Renovar()
		{
			if (_viewModel.MesesProrrogacao <= 0)
			{
				DialogoFactory.MostrarDialogoConfirma("Informe a quantidade de meses de prorrogação.", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			List<ContratoRenovacaoEmMassaViewModel> itens = HelperIEnumerable.ToListEmptyWhenNull<ContratoRenovacaoEmMassaViewModel>(_viewModel.ContratosSelecionados);
			if (DialogoFactory.MostrarDialogoSimNao((itens.Count == 1) ? $"Deseja renovar o contrato selecionado por mais {_viewModel.MesesProrrogacao} mês(es) com o valor informado?" : $"Deseja renovar os {itens.Count} contratos selecionados por mais {_viewModel.MesesProrrogacao} mês(es) com os valores informados?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			IWService service = DadosSistema.GetService();
			List<string> erros = new List<string>();
			int renovados = 0;
			int atual = 0;
			try
			{
				foreach (ContratoRenovacaoEmMassaViewModel item in itens)
				{
					atual++;
					ThreadPrincipal.Executar((Action)delegate
					{
						LoaderProcessoService.MostrarLoader($"Renovando contrato {atual} de {itens.Count}...");
					});
					GestaoContratoDto contrato = item.Dto;
					try
					{
						contrato.DataFinal = item.NovaDataFinal;
						AplicarValorNosServicos(item);
						await service.Canal<IGestaoContratoModuleService, Task<GestaoContratoDto>>((Func<IGestaoContratoModuleService, Task<GestaoContratoDto>>)((IGestaoContratoModuleService a) => ((IAtualizar<GestaoContratoDto>)(object)a).AtualizarAsync(contrato)));
						service.Canal<IGestaoContratoModuleService, GestaoContratoDto>((Func<IGestaoContratoModuleService, GestaoContratoDto>)((IGestaoContratoModuleService x) => x.Renovar(new GestaoContratoHistoricoDto
						{
							IdContrato = ((EntityBaseDto)contrato).Id
						})));
						renovados++;
					}
					catch (Exception ex)
					{
						erros.Add($"Contrato {contrato.NumeroContrato}: {ex.Message}");
					}
				}
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
			string text = ((itens.Count != 1) ? $"{renovados} de {itens.Count} contratos renovados!" : ((renovados == 1) ? "Contrato renovado!" : "O contrato não foi renovado."));
			if (erros.Any())
			{
				text += $"\n\nNão foi possível renovar:\n{HelperString.LinhasParaTexto(erros.ToArray())}";
			}
			DialogoFactory.MostrarDialogoConfirma(text, false, System.Windows.HorizontalAlignment.Center);
			if (renovados > 0)
			{
				((NavigationBase)this).FecharView();
			}
		}

		private static void AplicarValorNosServicos(ContratoRenovacaoEmMassaViewModel item)
		{
			GestaoContratoDto dto = item.Dto;
			List<ServicoContratoDto> list = HelperIEnumerable.ToListEmptyWhenNull<ServicoContratoDto>(dto.Servicos);
			if (!list.Any() || item.Valor == item.ValorOriginal)
			{
				return;
			}
			if (item.ValorOriginal == 0m)
			{
				list.First().Valor = HelperDecimal.ArredondarParaDuasCasasDecimais(item.Valor);
			}
			else
			{
				decimal num = item.Valor / item.ValorOriginal;
				foreach (ServicoContratoDto item2 in list)
				{
					item2.Valor = HelperDecimal.ArredondarParaDuasCasasDecimais(item2.Valor * num);
				}
				decimal num2 = item.Valor - list.Sum((ServicoContratoDto x) => x.Valor);
				if (num2 != 0m)
				{
					list[list.Count - 1].Valor = HelperDecimal.ArredondarParaDuasCasasDecimais(list[list.Count - 1].Valor + num2);
				}
			}
			dto.Servicos = list;
		}
	}
	public class RenovarContratosEmMassaFormulario : FormularioEntity<GestaoContratoDto>
	{
		public RenovarContratosEmMassaFormulario(RenovarContratosEmMassaViewModel viewModel)
			: base(((ViewModelDtoBase<GestaoContratoDto>)viewModel).Dto)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new RenovarContratosEmMassaAbaViewModel((IViewModelPrincipal)(object)viewModel) });
		}
	}
	public class RenovarContratosEmMassaViewModel : ViewModelDtoBase<GestaoContratoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		public Lista<TipoStatusContrato> Situacoes { get; protected set; }

		public Lista<KeyValuePair<string, int>> Tipos { get; protected set; }

		public int MesesProrrogacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => MesesProrrogacao));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => MesesProrrogacao), value, (Action)AplicarProrrogacaoDatas);
			}
		}

		public decimal ValorPercentual
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorPercentual));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorPercentual), value, (Action)AplicarReajusteValores);
			}
		}

		public Lista<ContratoRenovacaoEmMassaViewModel> Contratos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ContratoRenovacaoEmMassaViewModel>>((Expression<Func<Lista<ContratoRenovacaoEmMassaViewModel>>>)(() => Contratos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ContratoRenovacaoEmMassaViewModel>>((Expression<Func<Lista<ContratoRenovacaoEmMassaViewModel>>>)(() => Contratos), value);
			}
		}

		public bool SelecionarTodos
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => SelecionarTodos));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SelecionarTodos), value, (Action)AplicarSelecaoTodos);
			}
		}

		public string QuantidadeSelecionadosDescricao => $"{ContratosSelecionados.Count()} de {Contratos?.Itens.Count() ?? 0} contrato(s) selecionado(s)";

		public IEnumerable<ContratoRenovacaoEmMassaViewModel> ContratosSelecionados
		{
			get
			{
				if (!HelperObject.IsNull((object)Contratos))
				{
					return Contratos.Itens.Where((ContratoRenovacaoEmMassaViewModel x) => x.Selecionado);
				}
				return Enumerable.Empty<ContratoRenovacaoEmMassaViewModel>();
			}
		}

		public ICommand PesquisarCommand { get; set; }

		public RenovarContratosEmMassaViewModel()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Expected O, but got Unknown
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Expected O, but got Unknown
			PesquisarCommand = (ICommand)new AsyncCommand((Func<Task>)Pesquisar);
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>
			{
				new KeyValuePair<string, int>("Todos", 0)
			};
			list.Add(new KeyValuePair<string, int>(HelperEnum.RetornarDescricao((Enum)(object)(TipoFaturamentoContrato)1), 1));
			list.Add(new KeyValuePair<string, int>(HelperEnum.RetornarDescricao((Enum)(object)(TipoFaturamentoContrato)2), 2));
			Tipos = new Lista<KeyValuePair<string, int>>();
			Tipos.Adicionar((IEnumerable<KeyValuePair<string, int>>)list);
			Tipos.PosicionarNoPrimeiro();
			Situacoes = new Lista<TipoStatusContrato>();
			Situacoes.Adicionar((TipoStatusContrato)new Valido());
			Situacoes.Adicionar((TipoStatusContrato)new VenceTrintaDias("30"));
			Situacoes.Adicionar((TipoStatusContrato)new Vencido());
			Situacoes.PosicionarNoPrimeiro();
		}

		private void AplicarProrrogacaoDatas()
		{
			Lista<ContratoRenovacaoEmMassaViewModel> contratos = Contratos;
			foreach (ContratoRenovacaoEmMassaViewModel item in ((contratos != null) ? HelperIEnumerable.ToListEmptyWhenNull<ContratoRenovacaoEmMassaViewModel>((IEnumerable<ContratoRenovacaoEmMassaViewModel>)contratos.Itens) : null) ?? new List<ContratoRenovacaoEmMassaViewModel>())
			{
				item.AplicarProrrogacao(MesesProrrogacao);
			}
		}

		private void AplicarReajusteValores()
		{
			Lista<ContratoRenovacaoEmMassaViewModel> contratos = Contratos;
			foreach (ContratoRenovacaoEmMassaViewModel item in ((contratos != null) ? HelperIEnumerable.ToListEmptyWhenNull<ContratoRenovacaoEmMassaViewModel>((IEnumerable<ContratoRenovacaoEmMassaViewModel>)contratos.Itens) : null) ?? new List<ContratoRenovacaoEmMassaViewModel>())
			{
				item.Valor = HelperDecimal.ArredondarParaDuasCasasDecimais(Calculos.CalcularAcrescimo(item.ValorOriginal, ValorPercentual));
			}
		}

		private void AplicarSelecaoTodos()
		{
			Lista<ContratoRenovacaoEmMassaViewModel> contratos = Contratos;
			foreach (ContratoRenovacaoEmMassaViewModel item in ((contratos != null) ? HelperIEnumerable.ToListEmptyWhenNull<ContratoRenovacaoEmMassaViewModel>((IEnumerable<ContratoRenovacaoEmMassaViewModel>)contratos.Itens) : null) ?? new List<ContratoRenovacaoEmMassaViewModel>())
			{
				item.Selecionado = SelecionarTodos;
			}
		}

		private void AtualizarSelecao()
		{
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => QuantidadeSelecionadosDescricao));
		}

		private async Task Pesquisar()
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Pesquisando contratos...");
				});
				IWService service = DadosSistema.GetService();
				List<GestaoContratoDto> contratos = new List<GestaoContratoDto>();
				short pagina = 1;
				while (true)
				{
					PesquisaDto pesquisaDto = ObterObjetoPesquisa(pagina, 100);
					List<GestaoContratoDto> list = HelperIEnumerable.ToListEmptyWhenNull<BaseDto>((await service.Canal<IGestaoContratoModuleService, Task<PaginaEntityDto>>((Func<IGestaoContratoModuleService, Task<PaginaEntityDto>>)((IGestaoContratoModuleService s) => ((IPesquisaAtivosInativosCustom)s).PesquisarAtivosAsync(pesquisaDto)))).Lista).OfType<GestaoContratoDto>().ToList();
					contratos.AddRange(list);
					if (list.Count < 100)
					{
						break;
					}
					pagina++;
				}
				Lista<ContratoRenovacaoEmMassaViewModel> val = new Lista<ContratoRenovacaoEmMassaViewModel>();
				val.Adicionar(from x in contratos
					where !x.Cancelado
					select new ContratoRenovacaoEmMassaViewModel(x, AtualizarSelecao));
				Contratos = val;
				if (ValorPercentual != 0m)
				{
					AplicarReajusteValores();
				}
				AplicarProrrogacaoDatas();
				SelecionarTodos = false;
				AtualizarSelecao();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private PesquisaDto ObterObjetoPesquisa(short pagina, short registrosPorPagina)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Expected O, but got Unknown
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Expected O, but got Unknown
			return new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaGestaoContratoDto
				{
					StatusContrato = new CodigoDescricaoDto
					{
						Codigo = Situacoes.ItemSelecionado.Codigo
					},
					TipoFaturamento = Tipos.ItemSelecionado.Value,
					Ativo = true,
					PesquisaAvancada = false
				},
				Pagina = pagina,
				Tamanho = registrosPorPagina
			};
		}

		public override void ProjectedAsViewModel()
		{
			MesesProrrogacao = 12;
			ValorPercentual = 0m;
			Contratos = new Lista<ContratoRenovacaoEmMassaViewModel>();
			AtualizarSelecao();
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new RenovarContratosEmMassaFormulario(this);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
	internal static class ConfirmacaoNumeroContrato
	{
		public static bool Confirmar(IWService service, IFormularioEntity formulario)
		{
			GestaoContratoFormulario contratoFormulario = formulario as GestaoContratoFormulario;
			if (contratoFormulario == null)
			{
				return true;
			}
			long numeroContrato = contratoFormulario.ViewModel.NumeroContrato;
			if (numeroContrato <= 0)
			{
				return true;
			}
			try
			{
				if (!service.Canal<IGestaoContratoModuleService, bool>((Func<IGestaoContratoModuleService, bool>)((IGestaoContratoModuleService x) => x.ExisteContratoComNumero(numeroContrato, ((EntityBaseDto)((FormularioEntity<GestaoContratoDto>)contratoFormulario).Dto).Id))))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				return false;
			}
			return DialogoFactory.MostrarDialogoSimNao($"Já existe um contrato com o número {numeroContrato}. Deseja continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes;
		}
	}
	public class GestaoContratoAbaViewModel : AbaPrincipalViewModelBase
	{
		public GestaoContratoAbaViewModel(IViewModelPrincipal viewModel)
			: base("CONTRATO", "Inclusão de novo contrato", viewModel)
		{
		}
	}
	public class GestaoContratoFormulario : FormularioEntity<GestaoContratoDto>
	{
		public GestaoContratoViewModel ViewModel { get; }

		public GestaoContratoFormulario(GestaoContratoDto dto)
			: base(dto)
		{
			ViewModel = new GestaoContratoViewModel(base.Dto);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[3]
			{
				(IAbaViewModelBase)new GestaoContratoAbaViewModel((IViewModelPrincipal)(object)ViewModel),
				(IAbaViewModelBase)new GestaoContratoAnexosAbaViewModel((IViewModelDtoBase)(object)new GestaoContratoAnexosViewModel(base.Dto, DadosSistema.GetService())),
				(IAbaViewModelBase)new ImpressaoContratoAbaViewModel((IViewModelDtoBase)(object)new ImpressaoContratoViewModel(base.Dto))
			});
		}
	}
	public class GestaoContratosViewModel : ModuloBase
	{
		public GestaoContratosViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGestaoContratoRegion, typeof(GestaoContratoListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class GestaoContratoFichaAdicionarViewModel : FichaBaseAdicionar<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService>
	{
		public GestaoContratoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}

		protected override async Task Gravar()
		{
			IWService service = base.Service;
			IFormulario formulario = ((FichaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Formulario;
			if (ConfirmacaoNumeroContrato.Confirmar(service, (IFormularioEntity)(object)((formulario is IFormularioEntity) ? formulario : null)))
			{
				await ((FichaBasePersistir<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Gravar();
			}
		}

		protected override async Task<GestaoContratoDto> Persistir(IFormularioEntity formulario)
		{
			try
			{
				GestaoContratoDto val = await base.Service.Canal<IGestaoContratoModuleService, Task<GestaoContratoDto>>((Func<IGestaoContratoModuleService, Task<GestaoContratoDto>>)((IGestaoContratoModuleService service) => ((IAdicionar<GestaoContratoDto>)(object)service).AdicionarAsync(formulario.ToDto<GestaoContratoDto>())));
				((FichaBasePersistir<GestaoContratoDto, GestaoContratoViewModel>)(object)this).SelecaoNavigateParameterCallBack?.CallBack(val);
				return val;
			}
			catch (OutOfMemoryException)
			{
				throw new Exception("Quantidade de anexos ultrapassa o limite máximo. Utilize resoluções menores nas imagens");
			}
			catch (Exception ex2)
			{
				if (HelperException.GetLastException(ex2).Message.ToLower().Contains("memória") || HelperException.GetLastException(ex2).Message.ToLower().Contains("memory"))
				{
					throw new Exception("Quantidade de anexos ultrapassa o limite máximo. Utilize resoluções menores nas imagens");
				}
				throw;
			}
		}
	}
	public class GestaoContratoFichaAtualizarViewModel : FichaBaseAtualizar<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService>
	{
		public GestaoContratoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}

		protected override async Task Gravar()
		{
			IWService service = base.Service;
			IFormulario formulario = ((FichaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Formulario;
			if (ConfirmacaoNumeroContrato.Confirmar(service, (IFormularioEntity)(object)((formulario is IFormularioEntity) ? formulario : null)))
			{
				await ((FichaBasePersistir<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Gravar();
			}
		}

		protected override async Task<GestaoContratoDto> Persistir(IFormularioEntity formulario)
		{
			try
			{
				return await base.Service.Canal<IGestaoContratoModuleService, Task<GestaoContratoDto>>((Func<IGestaoContratoModuleService, Task<GestaoContratoDto>>)((IGestaoContratoModuleService service) => ((IAtualizar<GestaoContratoDto>)(object)service).AtualizarAsync(formulario.ToDto<GestaoContratoDto>())));
			}
			catch (OutOfMemoryException)
			{
				throw new Exception("Quantidade de anexos ultrapassa o limite máximo. Utilize resoluções menores nas imagens");
			}
			catch (Exception ex2)
			{
				if (HelperException.GetLastException(ex2).Message.ToLower().Contains("memória") || HelperException.GetLastException(ex2).Message.ToLower().Contains("memory"))
				{
					throw new Exception("Quantidade de anexos ultrapassa o limite máximo. Utilize resoluções menores nas imagens");
				}
				throw;
			}
		}
	}
	public class GestaoContratoListaViewModel : ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>
	{
		private readonly INotificacao _notificacao;

		private readonly IBoletoProxyService _boletoProxyService;

		private readonly IConfiguracaoClientService _configuracaoClientService;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly SemaphoreSlim _semaforoEnvioEmail = new SemaphoreSlim(1);

		public TotalContratoDto Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<TotalContratoDto>((Expression<Func<TotalContratoDto>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<TotalContratoDto>((Expression<Func<TotalContratoDto>>)(() => Total), value);
			}
		}

		public string ToolTipPesquisa => "Aqui você pode pesquisar por:\nNúmero do contrato, todos os campos disponíveis na busca de pessoas, entre outros...\nVocê ainda pode fazer combinações de pesquisa, por exemplo:\nDigite 'número do contrato + qualquer palavra do nome' para retornar cadastros que combinem essas palavras.\nDigite 'número do contrato'|'documento' para buscar por 2 ou mais documentos simultaneamente.";

		public bool Ativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Ativo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Ativo), value);
			}
		}

		public List<KeyValuePair<string, TipoMovimentacao>> TiposMovimentacoes
		{
			get
			{
				return ((BindableBase)this).GetProperty<List<KeyValuePair<string, TipoMovimentacao>>>((Expression<Func<List<KeyValuePair<string, TipoMovimentacao>>>>)(() => TiposMovimentacoes));
			}
			set
			{
				((BindableBase)this).SetProperty<List<KeyValuePair<string, TipoMovimentacao>>>((Expression<Func<List<KeyValuePair<string, TipoMovimentacao>>>>)(() => TiposMovimentacoes), value);
			}
		}

		public Lista<ImagemViewModel> Imagens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => Imagens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => Imagens), value);
			}
		}

		public Lista<ArquivoViewModel> Arquivos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Arquivos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Arquivos), value);
			}
		}

		public ICommand FaturarContratosSelecionadosCommand { get; set; }

		public ICommand GerarFaturasFiscaisContratosSelecionadosCommand { get; set; }

		public ICommand FaturarTudoCommand { get; set; }

		public ICommand GerarFaturaFiscalTudoCommand { get; set; }

		public ICommand GerarDavOsCommand { get; set; }

		public ICommand GerarNotaManualCommand { get; set; }

		public ICommand CancelarContratoCommand { get; set; }

		public ICommand RenovarContratoCommand { get; set; }

		public ICommand RenovarContratosEmMassaCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public ICommand ImprimirContratoCommand { get; set; }

		public ICommand ImprimirFaturaLocacaoCommand { get; set; }

		public ICommand ExibirAnexosCommand { get; set; }

		public GestaoContratoListaViewModel(ISessao sessao, IWService service, INotificacao notificacao, IBoletoProxyService boletoProxyService, IConfiguracaoClientService configuracaoClientService, IEntityAtivosGestaoContrato<GestaoContratoDto, IGestaoContratoModuleService> ativos, IEntityInativoGestaoContrato<GestaoContratoDto, IGestaoContratoModuleService> inativos, IEntityFiltroAvancadoGestaoContrato<GestaoContratoDto, IGestaoContratoModuleService> filtroAvancado, IImpressoraNaoFiscalService impressoraNaoFiscalService)
			: base(sessao, typeof(GestaoContratoFichaAdicionarView).Name, typeof(GestaoContratoFichaAtualizarView).Name, service, new IEntityBase<GestaoContratoDto, IGestaoContratoModuleService>[2]
			{
				(IEntityBase<GestaoContratoDto, IGestaoContratoModuleService>)(object)ativos,
				(IEntityBase<GestaoContratoDto, IGestaoContratoModuleService>)(object)inativos
			})
		{
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Expected O, but got Unknown
			//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Expected O, but got Unknown
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Expected O, but got Unknown
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Expected O, but got Unknown
			//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Expected O, but got Unknown
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Expected O, but got Unknown
			//IL_024e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Expected O, but got Unknown
			//IL_028e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0298: Expected O, but got Unknown
			//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c5: Expected O, but got Unknown
			//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
			((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).SetarTipoPesquisa((Pesquisa)new PesquisaAvancadaViewModel((IItens)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens, (FiltroAvancadoBase)(object)((filtroAvancado is FiltroAvancadoGestaoContrato) ? filtroAvancado : null), (OpcaoBase[])(object)new OpcaoBase[2]
			{
				(OpcaoBase)((ativos is OpcaoBase) ? ativos : null),
				(OpcaoBase)((inativos is OpcaoBase) ? inativos : null)
			}));
			FaturarContratosSelecionadosCommand = (ICommand)new AsyncCommand((Func<Task>)FaturarContratosSelecionados, (Func<bool>)PodeFaturarSelecionados, (bool?)null);
			GerarFaturasFiscaisContratosSelecionadosCommand = (ICommand)new AsyncCommand((Func<Task>)GerarFaturasFiscaisContratosSelecionados, (Func<bool>)PodeGerarFaturaFiscalSelecionados, (bool?)null);
			GerarDavOsCommand = (ICommand)new AsyncCommand((Func<Task>)GerarDavOs, (Func<bool>)PodeGerarDavOs, (bool?)null);
			GerarNotaManualCommand = (ICommand)new AsyncCommand((Func<Task>)GerarNotaManual, (Func<bool>)PodeGerarNotaManual, (bool?)null);
			FaturarTudoCommand = (ICommand)new AsyncCommand((Func<Task>)FaturarTodosContratos, (Func<bool>)PodeFaturarTudo, (bool?)null);
			GerarFaturaFiscalTudoCommand = (ICommand)new AsyncCommand((Func<Task>)GerarFaturaFiscalTodosContratos, (Func<bool>)PodeGerarTodasFaturaFiscal, (bool?)null);
			CancelarContratoCommand = (ICommand)new DelegateCommand((Action)Cancelar, (Func<bool>)PodeCancelar, (bool?)null);
			RenovarContratoCommand = (ICommand)new DelegateCommand((Action)Renovar, (Func<bool>)PodeRenovar, (bool?)null);
			RenovarContratosEmMassaCommand = (ICommand)new DelegateCommand((Action)RenovarContratosEmMassa, (Func<bool>)PodeRenovarContratosEmMassa, (bool?)null);
			ImprimirContratoCommand = (ICommand)new DelegateCommand((Action)ImprimirContrato, (Func<bool>)PodeImprimir, (bool?)null);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			ImprimirFaturaLocacaoCommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirFaturaLocacao, (Func<bool>)(() => true), false, (bool?)null);
			ExibirAnexosCommand = (ICommand)new AsyncCommand((Func<Task>)Anexos, (Func<bool>)PodeExibirAnexos, false, (bool?)null);
			_notificacao = notificacao;
			_boletoProxyService = boletoProxyService;
			_configuracaoClientService = configuracaoClientService;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			((IItensPaginacao)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens).PreencheuPrimeiraPaginaEvent += OnPreencheuPrimeiraPaginaEvent;
			Ativo = true;
			TiposMovimentacoes = new List<KeyValuePair<string, TipoMovimentacao>>
			{
				new KeyValuePair<string, TipoMovimentacao>(HelperEnum.RetornarDescricao((Enum)(object)(TipoMovimentacao)8), (TipoMovimentacao)8),
				new KeyValuePair<string, TipoMovimentacao>(HelperEnum.RetornarDescricaoCurta((Enum)(object)(TipoMovimentacao)9), (TipoMovimentacao)9)
			};
		}

		private void OnPreencheuPrimeiraPaginaEvent(object sender, EventArgs eventArgs)
		{
			ObterTotais();
		}

		private void ObterTotais()
		{
			TotalContratoDto result = ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<TotalContratoDto>>((Func<IGestaoContratoModuleService, Task<TotalContratoDto>>)((IGestaoContratoModuleService service) => service.ObterTotais(ObterObjetoPesquisaDto()))).Result;
			Total = result;
		}

		private PesquisaDto ObterObjetoPesquisaDto()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Expected O, but got Unknown
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Expected O, but got Unknown
			if (((PesquisaAvancadaViewModel)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa).FocoNaBuscaAvancada)
			{
				return ((PesquisaAvancadaViewModel)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa).ContextoFiltroAvancado.ObterObjetoPesquisa((short)1, (short)30);
			}
			Lista<KeyValuePair<string, int>> val = ((!HelperObject.Is<IEntityAtivos<GestaoContratoDto, IGestaoContratoModuleService>>((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto)) ? HelperObject.To<EntityInativoFiltroGestaoContrato<GestaoContratoDto, IGestaoContratoModuleService>>((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto)?.Tipos : HelperObject.To<EntityAtivoFiltroGestaoContrato<GestaoContratoDto, IGestaoContratoModuleService>>((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto)?.Tipos);
			return new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaGestaoContratoDto
				{
					TipoFaturamento = val.ItemSelecionado.Value,
					Texto = ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto.Informacao,
					Ativo = HelperObject.Is<IEntityAtivos<GestaoContratoDto, IGestaoContratoModuleService>>((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto),
					PesquisaAvancada = false
				},
				Pagina = 1,
				Tamanho = 30
			};
		}

		public decimal TotalItensSelecionados()
		{
			return base.ItensSelecionados.Count();
		}

		public async void ProcessarParametroInformadoDto(MovimentacaoBaseDto dto)
		{
			if (HelperObject.IsNotNull((object)dto) && dto.Situacao.Codigo == 2 && (int)dto.Tipo == 8 && DadosSistema.ConfiguracaoDto.Saida.TransmitirNotaFiscalServico.Valor)
			{
				await EnviarEmail(dto, envioAutomatico: false);
			}
		}

		private async Task EnviarEmail(MovimentacaoBaseDto movimentacaoDto, bool envioAutomatico = true)
		{
			EnvioEmailMovimentacaoService envioEmailService = EnvioEmailMovimentacaoService.Criar(((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service, movimentacaoDto, _boletoProxyService, _configuracaoClientService);
			if (envioAutomatico)
			{
				envioEmailService.ComEnvioAutomatico();
			}
			TipoMovimentacao tipo = movimentacaoDto.Tipo;
			if ((int)tipo != 8)
			{
				if ((int)tipo == 9)
				{
					await envioEmailService.EnviarEmailNotaManual((Action<string>)delegate(string email)
					{
						AdicionarHistoricoEmailEnviado(email, movimentacaoDto);
					});
				}
			}
			else
			{
				await envioEmailService.EnviarEmailDocumentoFiscal((Action<string>)delegate(string email)
				{
					AdicionarHistoricoEmailEnviado(email, movimentacaoDto);
				});
			}
		}

		private async void AdicionarHistoricoEmailEnviado(string email, MovimentacaoBaseDto movimentacaoDto)
		{
			await AdicionarHistorico($"E-mail enviado com sucesso para {email}", movimentacaoDto);
		}

		protected async Task AdicionarHistorico(string observacao, MovimentacaoBaseDto movimentacaoDto)
		{
			LoaderProcessoService.MostrarLoader("Adicionando situação...");
			await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).AdicionarHistoricoAsync(new HistoricoDto
			{
				DataHora = DateTime.Now,
				MovimentacaoReferencia = ((EntityBaseDto)movimentacaoDto).Id,
				Observacao = observacao,
				SituacaoMovimentacao = movimentacaoDto.Situacao
			}))).ContinueWith(delegate(Task<MovimentacaoBaseDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				ThreadPrincipal.Executar((Action)delegate
				{
					if (!task.IsFaulted)
					{
						_notificacao.MostrarNotificacao(observacao);
					}
				});
			});
		}

		public override bool PodeEditar()
		{
			if (((ListaManutencaoBase<GestaoContratoDto, GestaoContratoViewModel, GestaoContrato>)(object)this).PodeEditar())
			{
				return !((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Cancelado;
			}
			return false;
		}

		public bool PodeFaturarTudo()
		{
			Pesquisa pesquisa = ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa;
			object obj;
			if (pesquisa == null)
			{
				obj = null;
			}
			else
			{
				OpcaoBase contexto = pesquisa.Contexto;
				obj = ((contexto != null) ? contexto.Descricao : null);
			}
			string text = (string)obj;
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && text.Equals("ativos") && !base.ItensSelecionados.Any())
			{
				return DadosSistema.VerificarPermissao<GestaoContrato, PermitirFaturarContrato>();
			}
			return false;
		}

		public bool PodeGerarTodasFaturaFiscal()
		{
			Pesquisa pesquisa = ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa;
			object obj;
			if (pesquisa == null)
			{
				obj = null;
			}
			else
			{
				OpcaoBase contexto = pesquisa.Contexto;
				obj = ((contexto != null) ? contexto.Descricao : null);
			}
			string text = (string)obj;
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && text.Equals("ativos") && !base.ItensSelecionados.Any())
			{
				return DadosSistema.VerificarPermissao<GestaoContrato, PermitirGerarFaturaFiscal>();
			}
			return false;
		}

		public bool PodeGerarFaturaFiscalSelecionados()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido && !base.ItensSelecionados.All((GestaoContratoDto x) => x.Cancelado) && base.ItensSelecionados.All((GestaoContratoDto x) => x.DataFinal.Date >= DateTime.Now.Date) && base.ItensSelecionados.Any((GestaoContratoDto x) => ((EntityDto)x).Ativo) && base.ItensSelecionados.Any((GestaoContratoDto x) => x.FaturaFiscal) && ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Servicos.Any() && DadosSistema.VerificarPermissao<GestaoContrato, PermitirGerarFaturaFiscal>())
			{
				return base.ItensSelecionados.Any();
			}
			return false;
		}

		public async Task GerarFaturasFiscaisContratosSelecionados()
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Gerando faturas fiscais...");
				});
				IEnumerable<GestaoContratoDto> source = ((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.Where((GestaoContratoDto x) => x.FaturaFiscal);
				MessageBoxResult messageBoxResult;
				if (source.Count() == 1)
				{
					messageBoxResult = ((source.FirstOrDefault().DataUltimoFaturamento == DateTime.MinValue) ? DialogoFactory.MostrarDialogoSimNao("Deseja gerar fatura do contrato selecionado?", false, 0, System.Windows.HorizontalAlignment.Center) : DialogoFactory.MostrarDialogoSimNao($"Último faturamento em {HelperDate.FormatarData(source.FirstOrDefault().DataUltimoFaturamento, (FormatoData)6)}, tem certeza que deseja gerar fatura do contrato selecionado?", false, 0, System.Windows.HorizontalAlignment.Center));
				}
				else if (source.Count() > 1)
				{
					IEnumerable<GestaoContratoDto> source2 = source.Where((GestaoContratoDto x) => x.DataUltimoFaturamento.Month.Equals(DateTime.Now.Month) && x.DataUltimoFaturamento.Year.Equals(DateTime.Now.Year));
					if (source2.Any())
					{
						string arg = HelperString.LinhasParaTexto(source2.Select((GestaoContratoDto contrato) => $"Número: {contrato.NumeroContrato} Último faturamento: {HelperDate.FormatarData(contrato.DataUltimoFaturamento, (FormatoData)6)}\n").ToArray());
						messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Contratos selecionados:\n" + $"{arg}" + "Tem certeza que deseja gerar fatura do(s) contrato(s) selecionado(s)?", false, 0, System.Windows.HorizontalAlignment.Center);
					}
					else
					{
						messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Deseja gerar fatura dos contratos selecionados?", false, 0, System.Windows.HorizontalAlignment.Center);
					}
				}
				else
				{
					messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Deseja gerar fatura dos contratos selecionados?", false, 0, System.Windows.HorizontalAlignment.Center);
				}
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					Lista<string> recebimentosReferencia = new Lista<string>();
					Lista<string> val = recebimentosReferencia;
					val.Adicionar(await CriarFaturasFiscais(source.Select((Func<GestaoContratoDto, ContratoValidoDto>)((GestaoContratoDto x) => new ContratoValidoDto
					{
						Pessoa = x.Pessoa,
						Tecnico = x.Tecnico,
						Servicos = x.Servicos,
						ContratoReferencia = ((EntityBaseDto)x).Id,
						EspeciePagamento = x.EspeciePagamento,
						PlanoConta = x.PlanoConta,
						CentroCusto = x.CentroCusto,
						DiaVencimentoFinanceiro = x.DiaVencimentoFinanceiro,
						NumeroContrato = x.NumeroContrato,
						Conta = x.Conta
					}))));
					if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
					{
						SelecaoCommandParameter<RecebimentoDto> val2 = new SelecaoCommandParameter<RecebimentoDto>("RecebimentoListaView", (Action<RecebimentoDto>)delegate
						{
						}, true);
						val2.AdicionarParametro("Identificador", (object)string.Join("|", recebimentosReferencia.Itens));
						((NavigationBase)this).NavegarCommand.Execute(val2);
						((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
						DialogoFactory.MostrarDialogoConfirma((recebimentosReferencia.Itens.Count <= 1) ? "Fatura gerada!" : $"Foram geradas {recebimentosReferencia.Itens.Count} faturas!", false, System.Windows.HorizontalAlignment.Center);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private async Task GerarFaturaFiscalTodosContratos()
		{
			_ = 1;
			try
			{
				LoaderProcessoService.MostrarLoader("Gerando faturas...");
				if (DialogoFactory.MostrarDialogoSimNao("Deseja gerar fatura de todos os contratos válidos?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				IEnumerable<ContratoValidoDto> source = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<IEnumerable<ContratoValidoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<ContratoValidoDto>>>)((IGestaoContratoModuleService service) => service.ObterContratosValidos(true)));
				if (!source.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Não existem contratos mensais para faturar!", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				Ativo = false;
				Lista<string> recebimentosReferencia = new Lista<string>();
				IEnumerable<List<ContratoValidoDto>> enumerable = HelperList.SplitList<ContratoValidoDto>(HelperIEnumerable.ToListEmptyWhenNull<ContratoValidoDto>(source.Where((ContratoValidoDto x) => x.Servicos.Any())), 100);
				foreach (List<ContratoValidoDto> item in enumerable)
				{
					Lista<string> val = recebimentosReferencia;
					val.Adicionar(await CriarFaturasFiscais(item));
				}
				if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
				{
					SelecaoCommandParameter<RecebimentoDto> val2 = new SelecaoCommandParameter<RecebimentoDto>("RecebimentoListaView", (Action<RecebimentoDto>)delegate
					{
					}, true);
					val2.AdicionarParametro("Identificador", (object)string.Join("|", recebimentosReferencia.Itens));
					((NavigationBase)this).NavegarCommand.Execute(val2);
					((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
					DialogoFactory.MostrarDialogoConfirma((recebimentosReferencia.Itens.Count <= 1) ? "Fatura gerada!" : $"Foram geradas {recebimentosReferencia.Itens.Count} faturas!", false, System.Windows.HorizontalAlignment.Center);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
				Ativo = true;
			}
		}

		public async Task<IEnumerable<string>> CriarFaturasFiscais(IEnumerable<ContratoValidoDto> contratos)
		{
			try
			{
				Lista<RecebimentoDto> parcelasGeradas = new Lista<RecebimentoDto>();
				RecebimentoDto recebimentoAdicionado = default(RecebimentoDto);
				foreach (ContratoValidoDto contratoValido in contratos)
				{
					if (contratoValido.Valor <= 0m)
					{
						DialogoFactory.MostrarDialogoConfirma("Contrato com valor zerado! Parcela não será adicionada.", false, System.Windows.HorizontalAlignment.Center);
						continue;
					}
					int num = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
					DateTime dateTime = ((contratoValido.DiaVencimentoFinanceiro < DateTime.Now.Day) ? DateTime.Now.AddMonths(1) : DateTime.Now);
					RecebimentoDto recebimentoDto = new RecebimentoDto
					{
						Descricao = $"Fatura contrato número {contratoValido.NumeroContrato}",
						DataEmissao = DateTime.Now,
						Documento = contratoValido.NumeroContrato.ToString(),
						Pessoa = contratoValido.Pessoa,
						Conta = contratoValido.Conta,
						EspeciePagamento = contratoValido.EspeciePagamento,
						CentroCusto = contratoValido.CentroCusto,
						PlanoConta = contratoValido.PlanoConta,
						Valor = contratoValido.Valor,
						DataVencimento = ((contratoValido.DiaVencimentoFinanceiro != 0) ? new DateTime(DateTime.Today.Year, HelperDate.TryToDateTime((object)dateTime).Month, (num < contratoValido.DiaVencimentoFinanceiro) ? num : contratoValido.DiaVencimentoFinanceiro) : DateTime.Now),
						Ordem = 1,
						ContratoReferenciaFaturaFiscal = contratoValido.ContratoReferencia,
						MovimentacaoReferenciaFaturaFiscal = string.Empty,
						Juro = DadosSistema.ConfiguracaoDto.Financeiro.PercentualJuro.Valor,
						Multa = DadosSistema.ConfiguracaoDto.Financeiro.PercentualMulta.Valor
					};
					if (((PagamentoRecebimentoDto)recebimentoDto).EspeciePagamento.Codigo == 8 && ((PagamentoRecebimentoDto)recebimentoDto).DataVencimento < DateTime.Today)
					{
						DialogoFactory.MostrarDialogoConfirma("Contrato com espécie Boleto e vencimento anterior à data atual! Parcela não será adicionada.", false, System.Windows.HorizontalAlignment.Center);
						continue;
					}
					_ = recebimentoAdicionado;
					recebimentoAdicionado = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IRecebimentoModuleService, Task<RecebimentoDto>>((Func<IRecebimentoModuleService, Task<RecebimentoDto>>)((IRecebimentoModuleService x) => ((IAdicionar<RecebimentoDto>)(object)x).AdicionarAsync(recebimentoDto)));
					parcelasGeradas.Adicionar(recebimentoAdicionado);
					if (parcelasGeradas.Itens.Any((RecebimentoDto x) => ((PagamentoRecebimentoDto)x).AVista || ((PagamentoRecebimentoDto)x).DataVencimento < DateTime.Today))
					{
						((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IRecebimentoModuleService>((Action<IRecebimentoModuleService>)delegate(IRecebimentoModuleService x)
						{
							x.QuitarParcelasAvista(from y in parcelasGeradas.Itens
								where (((PagamentoRecebimentoDto)y).AVista && ((PagamentoRecebimentoDto)y).EspeciePagamento.Codigo != 8) || ((PagamentoRecebimentoDto)y).DataVencimento < DateTime.Today
								select y into t
								select ((EntityBaseDto)t).Id);
						});
					}
					((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, GestaoContratoDto>((Func<IGestaoContratoModuleService, GestaoContratoDto>)((IGestaoContratoModuleService service) => service.GerouFaturaFiscal(recebimentoAdicionado, contratoValido.ContratoReferencia)));
				}
				return parcelasGeradas.Itens.Select((RecebimentoDto x) => ((EntityBaseDto)x).Id);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		public async Task FaturarTodosContratos()
		{
			try
			{
				if (DialogoFactory.MostrarDialogoSimNao("Este processo pode durar vários minutos. Deseja faturar todos os contratos válidos?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				Ativo = false;
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Processando...");
				});
				IEnumerable<ContratoValidoDto> contratosValidos = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<IEnumerable<ContratoValidoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<ContratoValidoDto>>>)((IGestaoContratoModuleService service) => service.ObterContratosValidos(false)));
				if (!contratosValidos.Any())
				{
					DialogoFactory.MostrarDialogoConfirma("Não existem contratos mensais para faturar!", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				if (!DadosSistema.PodeEmitirNotaManualRetaguarda || !DadosSistema.SolicitarOndeFaturarContrato)
				{
					await FaturarTodosContratosNotaServico(contratosValidos);
					return;
				}
				KeyValuePair<string, TipoMovimentacao> keyValuePair = DialogoFactory.MostrarDialogoComboBox<KeyValuePair<string, TipoMovimentacao>>("Faturar em:", "Informação", (IEnumerable<KeyValuePair<string, TipoMovimentacao>>)TiposMovimentacoes, "Key", true);
				if (!HelperObject.IsNull((object)keyValuePair.Key))
				{
					if ((int)keyValuePair.Value == 9)
					{
						await FaturarTodosContratosNotaManual(contratosValidos);
					}
					else
					{
						await FaturarTodosContratosNotaServico(contratosValidos);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				Ativo = true;
				LoaderProcessoService.EsconderLoader();
			}
		}

		public async Task FaturarTodosContratosNotaServico(IEnumerable<ContratoValidoDto> contratosValidos)
		{
			try
			{
				Queue<string> movimentacoesReferencias = new Queue<string>(30);
				List<string> referenciasGeradas = new List<string>();
				await Task.Run(async delegate
				{
					foreach (List<ContratoValidoDto> contratosParte in HelperList.SplitList<ContratoValidoDto>(contratosValidos.Where((ContratoValidoDto x) => x.Servicos.Any()), 200))
					{
						List<ItemDto> servicos = new List<ItemDto>();
						ItemDto[] collection = await Task.WhenAll(from item in contratosParte.SelectMany((ContratoValidoDto c) => ObterDtoCriarItem(c, (TipoMovimentacao)8))
							select ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService s) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)s).CriarItemMovimentacao(item))));
						servicos.AddRange(collection);
						RetornoEnvioNFSeDto obj = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<INotaFiscalServicoModuleService, Task<RetornoEnvioNFSeDto>>((Func<INotaFiscalServicoModuleService, Task<RetornoEnvioNFSeDto>>)((INotaFiscalServicoModuleService s) => s.GerarNotasFiscaisServicoContratos(contratosParte.Select((ContratoValidoDto x) => x.ContratoReferencia), (IEnumerable<ItemDto>)servicos)));
						servicos.Clear();
						foreach (string movimentacoesReferencium in obj.MovimentacoesReferencia)
						{
							referenciasGeradas.Add(movimentacoesReferencium);
							if (movimentacoesReferencias.Count == 30)
							{
								movimentacoesReferencias.Dequeue();
							}
							movimentacoesReferencias.Enqueue(movimentacoesReferencium);
						}
					}
				});
				await TransmitirNotasFiscaisServicoGeradas(referenciasGeradas);
				if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
				{
					SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
					{
					}, true);
					val.AdicionarParametro("Identificador", (object)string.Join("|", movimentacoesReferencias));
					((NavigationBase)this).NavegarCommand.Execute(val);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		public async Task FaturarTodosContratosNotaManual(IEnumerable<ContratoValidoDto> contratosValidos)
		{
			try
			{
				Queue<string> movimentacoesReferencias = new Queue<string>(30);
				List<string> referenciasGeradas = new List<string>();
				await Task.Run(async delegate
				{
					IEnumerable<List<ContratoValidoDto>> enumerable = HelperList.SplitList<ContratoValidoDto>(contratosValidos.Where((ContratoValidoDto x) => x.Servicos.Any()), 200);
					foreach (List<ContratoValidoDto> contratosParte in enumerable)
					{
						List<ItemDto> servicos = new List<ItemDto>();
						ItemDto[] collection = await Task.WhenAll(from item in contratosParte.SelectMany((ContratoValidoDto c) => ObterDtoCriarItem(c, (TipoMovimentacao)8))
							select ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService s) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)s).CriarItemMovimentacao(item))));
						servicos.AddRange(collection);
						IEnumerable<string> obj = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>((Func<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>)((IMovimentacaoSaidaModuleService s) => s.GerarNotasManuaisContratos(contratosParte.Select((ContratoValidoDto x) => x.ContratoReferencia), (IEnumerable<ItemDto>)servicos)));
						servicos.Clear();
						foreach (string item in obj)
						{
							referenciasGeradas.Add(item);
							if (movimentacoesReferencias.Count == 30)
							{
								movimentacoesReferencias.Dequeue();
							}
							movimentacoesReferencias.Enqueue(item);
						}
					}
				});
				await ConcluirNotasManuaisGeradas(referenciasGeradas);
				if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
				{
					SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
					{
					}, true);
					val.AdicionarParametro("Identificador", (object)string.Join("|", movimentacoesReferencias));
					((NavigationBase)this).NavegarCommand.Execute(val);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		public bool PodeFaturarSelecionados()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido && !base.ItensSelecionados.All((GestaoContratoDto x) => x.Cancelado) && base.ItensSelecionados.All((GestaoContratoDto x) => x.DataFinal.Date >= DateTime.Now.Date) && base.ItensSelecionados.Any((GestaoContratoDto x) => ((EntityDto)x).Ativo) && ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Servicos.Any() && (base.ItensSelecionados.All((GestaoContratoDto x) => x.Servicos.All((ServicoContratoDto y) => y.ProdutoServicoEmpresa.ProdutoServico.IsServico)) || DadosSistema.SolicitarOndeFaturarContrato) && DadosSistema.VerificarPermissao<GestaoContrato, PermitirFaturarContrato>())
			{
				return base.ItensSelecionados.Any();
			}
			return false;
		}

		public async Task FaturarContratosSelecionados()
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Gerando notas fiscais de serviço...");
				});
				MessageBoxResult messageBoxResult;
				if (((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.Count() == 1)
				{
					messageBoxResult = ((((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.FirstOrDefault().DataUltimoFaturamento == DateTime.MinValue) ? DialogoFactory.MostrarDialogoSimNao("Deseja faturar o contrato selecionado?", false, 0, System.Windows.HorizontalAlignment.Center) : DialogoFactory.MostrarDialogoSimNao($"Último faturamento em {HelperDate.FormatarData(((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.FirstOrDefault().DataUltimoFaturamento, (FormatoData)6)}, tem certeza que deseja faturar o contrato selecionado?", false, 0, System.Windows.HorizontalAlignment.Center));
				}
				else if (((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.Count() > 1)
				{
					IEnumerable<GestaoContratoDto> source = ((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.Where((GestaoContratoDto x) => x.DataUltimoFaturamento.Month.Equals(DateTime.Now.Month) && x.DataUltimoFaturamento.Year.Equals(DateTime.Now.Year));
					if (source.Any())
					{
						string arg = HelperString.LinhasParaTexto(source.Select((GestaoContratoDto contrato) => $"Número: {contrato.NumeroContrato} Último faturamento: {HelperDate.FormatarData(contrato.DataUltimoFaturamento, (FormatoData)6)}\n").ToArray());
						messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Contratos selecionados:\n" + $"{arg}" + "Tem certeza que deseja faturar o(s) contrato(s) selecionado(s)?", false, 0, System.Windows.HorizontalAlignment.Center);
					}
					else
					{
						messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Deseja faturar os contratos selecionados?", false, 0, System.Windows.HorizontalAlignment.Center);
					}
				}
				else
				{
					messageBoxResult = DialogoFactory.MostrarDialogoSimNao("Deseja faturar os contratos selecionados?", false, 0, System.Windows.HorizontalAlignment.Center);
				}
				if (messageBoxResult != MessageBoxResult.Yes)
				{
					return;
				}
				if (!DadosSistema.PodeEmitirNotaManualRetaguarda || !DadosSistema.SolicitarOndeFaturarContrato)
				{
					await FaturarContratosSelecionados((TipoMovimentacao)8);
					return;
				}
				KeyValuePair<string, TipoMovimentacao> keyValuePair = DialogoFactory.MostrarDialogoComboBox<KeyValuePair<string, TipoMovimentacao>>("Faturar em:", "Informação", (IEnumerable<KeyValuePair<string, TipoMovimentacao>>)TiposMovimentacoes, "Key", true);
				if (!HelperObject.IsNull((object)keyValuePair.Key))
				{
					if ((int)keyValuePair.Value == 9)
					{
						await FaturarContratosSelecionados((TipoMovimentacao)9);
					}
					else
					{
						await FaturarContratosSelecionados((TipoMovimentacao)8);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		public async Task FaturarContratosSelecionados(TipoMovimentacao tipoMovimentacao)
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			Lista<ItemDto> servicos = new Lista<ItemDto>();
			foreach (GestaoContratoDto itensSelecionado in ((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados)
			{
				IEnumerable<ItemDto> enumerable = from x in ObterDtoCriarItem(CriarContratoFaturar(itensSelecionado), tipoMovimentacao)
					select ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(x))).Result;
				servicos.Adicionar(enumerable);
			}
			IEnumerable<string> enumerable2 = ((!((object)(TipoMovimentacao)(ref tipoMovimentacao)).Equals((object)(TipoMovimentacao)9)) ? (await TaskExtensions.Select<RetornoEnvioNFSeDto, IEnumerable<string>>(((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<INotaFiscalServicoModuleService, Task<RetornoEnvioNFSeDto>>((Func<INotaFiscalServicoModuleService, Task<RetornoEnvioNFSeDto>>)((INotaFiscalServicoModuleService service) => service.GerarNotasFiscaisServicoContratos(base.ItensSelecionados.Select((GestaoContratoDto x) => ((EntityBaseDto)x).Id), (IEnumerable<ItemDto>)servicos.Itens))), (Func<RetornoEnvioNFSeDto, IEnumerable<string>>)((RetornoEnvioNFSeDto x) => x.MovimentacoesReferencia))) : (await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>((Func<IMovimentacaoSaidaModuleService, Task<IEnumerable<string>>>)((IMovimentacaoSaidaModuleService service) => service.GerarNotasManuaisContratos(base.ItensSelecionados.Select((GestaoContratoDto x) => ((EntityBaseDto)x).Id), (IEnumerable<ItemDto>)servicos.Itens)))));
			IEnumerable<string> enumerable3 = enumerable2;
			List<string> referencias = HelperIEnumerable.ToListEmptyWhenNull<string>(enumerable3);
			if (!((object)(TipoMovimentacao)(ref tipoMovimentacao)).Equals((object)(TipoMovimentacao)9))
			{
				await TransmitirNotasFiscaisServicoGeradas(referencias);
			}
			else
			{
				await ConcluirNotasManuaisGeradas(referencias);
			}
			if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
			{
				string text = string.Join("|", referencias);
				SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
				{
				}, true);
				val.AdicionarParametro("Identificador", (object)text);
				((NavigationBase)this).NavegarCommand.Execute(val);
			}
		}

		private async Task TransmitirNotasFiscaisServicoGeradas(IReadOnlyCollection<string> movimentacoesReferencia)
		{
			if (!DadosSistema.ConfiguracaoDto.Saida.TransmitirNotaFiscalServico.Valor || !movimentacoesReferencia.Any())
			{
				return;
			}
			bool emailConfigurado = EmailConfigurado() && DadosSistema.ConfiguracaoDto.Financeiro.EnviarEmailAutomaticoFaturamentoAutomaticoContrato.Valor;
			if (DialogoFactory.MostrarDialogoSimNao((movimentacoesReferencia.Count == 1) ? "Deseja transmitir a nota fiscal de serviço eletrônica gerada?" : $"Deseja transmitir as {movimentacoesReferencia.Count} notas fiscais de serviço eletrônicas geradas?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			List<string> erros = new List<string>();
			List<Task> enviosEmail = new List<Task>();
			int concluidas = 0;
			int transmitidas = 0;
			foreach (string referencia in movimentacoesReferencia)
			{
				transmitidas++;
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader($"Transmitindo nota fiscal de serviço {transmitidas} de {movimentacoesReferencia.Count}...");
				});
				try
				{
					MovimentacaoDto val = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<INotaFiscalServicoModuleService, Task<MovimentacaoDto>>((Func<INotaFiscalServicoModuleService, Task<MovimentacaoDto>>)((INotaFiscalServicoModuleService service) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)service).EfetivarAsync(referencia)));
					if (((MovimentacaoBaseDto)val).Situacao.Codigo == 2)
					{
						concluidas++;
						if (emailConfigurado)
						{
							enviosEmail.Add(EnviarEmailSemBloquearTransmissao(val));
						}
					}
					else
					{
						List<string> list = erros;
						list.Add(await ObterDescricaoErroTransmissao(referencia, ((MovimentacaoBaseDto)val).Situacao.Descricao));
					}
				}
				catch (Exception ex)
				{
					List<string> list = erros;
					list.Add(await ObterDescricaoErroTransmissao(referencia, ex.Message));
				}
			}
			if (enviosEmail.Any())
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Concluindo o envio dos e-mails...");
				});
				await Task.WhenAll(enviosEmail);
			}
			int count = movimentacoesReferencia.Count;
			string text = ((count == 1) ? "nota fiscal" : "notas fiscais");
			string text2 = ((count == 1) ? "transmitida e concluída" : "transmitidas e concluídas");
			string text3 = $"{concluidas} de {count} {text} de serviço {text2}!";
			if (erros.Any())
			{
				int count2 = erros.Count;
				string arg = ((count2 == 1) ? "nota não foi transmitida" : "notas não foram transmitidas");
				text3 += $"\n{count2} {arg}. Será gerado um impresso em PDF com a relação.";
			}
			DialogoFactory.MostrarDialogoConfirma(text3, false, System.Windows.HorizontalAlignment.Center);
			if (erros.Any())
			{
				GerarImpressoPdfNotasNaoTransmitidas(erros);
			}
		}

		private async Task ConcluirNotasManuaisGeradas(IReadOnlyCollection<string> movimentacoesReferencia)
		{
			if (!movimentacoesReferencia.Any() || DadosSistema.ConfiguracaoDto.Saida.ExigirConferenciaPagamentoNotaManual.Valor)
			{
				return;
			}
			bool emailConfigurado = EmailConfigurado() && DadosSistema.ConfiguracaoDto.Financeiro.EnviarEmailAutomaticoFaturamentoAutomaticoContrato.Valor;
			if (DialogoFactory.MostrarDialogoSimNao((movimentacoesReferencia.Count == 1) ? "Deseja concluir a nota manual gerada?" : $"Deseja concluir as {movimentacoesReferencia.Count} notas manuais geradas?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			List<string> erros = new List<string>();
			List<Task> enviosEmail = new List<Task>();
			int concluidas = 0;
			int processadas = 0;
			foreach (string referencia in movimentacoesReferencia)
			{
				processadas++;
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader($"Concluindo nota manual {processadas} de {movimentacoesReferencia.Count}...");
				});
				try
				{
					MovimentacaoDto val = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<INotaManualModuleService, Task<MovimentacaoDto>>((Func<INotaManualModuleService, Task<MovimentacaoDto>>)((INotaManualModuleService service) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)service).EfetivarAsync(referencia)));
					if (((MovimentacaoBaseDto)val).Situacao.Codigo == 2)
					{
						concluidas++;
						if (emailConfigurado)
						{
							enviosEmail.Add(EnviarEmailSemBloquearTransmissao(val));
						}
					}
					else
					{
						List<string> list = erros;
						list.Add(await ObterDescricaoErroTransmissao(referencia, ((MovimentacaoBaseDto)val).Situacao.Descricao, "Nota manual"));
					}
				}
				catch (Exception ex)
				{
					List<string> list = erros;
					list.Add(await ObterDescricaoErroTransmissao(referencia, ex.Message, "Nota manual"));
				}
			}
			if (enviosEmail.Any())
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					LoaderProcessoService.MostrarLoader("Concluindo o envio dos e-mails...");
				});
				await Task.WhenAll(enviosEmail);
			}
			int count = movimentacoesReferencia.Count;
			string text = ((count == 1) ? "nota manual" : "notas manuais");
			string text2 = ((count == 1) ? "concluída" : "concluídas");
			string text3 = $"{concluidas} de {count} {text} {text2}!";
			if (erros.Any())
			{
				int count2 = erros.Count;
				string arg = ((count2 == 1) ? "nota não foi concluída" : "notas não foram concluídas");
				text3 += $"\n{count2} {arg}. Será gerado um impresso em PDF com a relação.";
			}
			DialogoFactory.MostrarDialogoConfirma(text3, false, System.Windows.HorizontalAlignment.Center);
			if (erros.Any())
			{
				GerarImpressoPdfNotasNaoTransmitidas(erros);
			}
		}

		private static bool EmailConfigurado()
		{
			if (!HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Auxiliar.ServidorSmtp.Valor))
			{
				return !HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Auxiliar.UsuarioEmail.Valor);
			}
			return false;
		}

		private async Task EnviarEmailSemBloquearTransmissao(MovimentacaoDto movimentacao)
		{
			await _semaforoEnvioEmail.WaitAsync();
			try
			{
				await EnviarEmail((MovimentacaoBaseDto)(object)movimentacao);
			}
			finally
			{
				_semaforoEnvioEmail.Release();
			}
		}

		private async Task<string> ObterDescricaoErroTransmissao(string referencia, string motivo, string identificacaoTipo = "NFS-e")
		{
			try
			{
				MovimentacaoDto val = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).ObterMovimentacao(referencia)));
				string arg = ((((MovimentacaoBaseDto)val).Numero > 0) ? $"{identificacaoTipo} {((MovimentacaoBaseDto)val).Numero}" : $"{identificacaoTipo} sem número");
				return $"{arg} - {HelperString.CopiarTexto(((MovimentacaoBaseDto)val).Pessoa.Nome, 30)} - {motivo}";
			}
			catch (Exception)
			{
				return $"{identificacaoTipo} - {motivo}";
			}
		}

		private void GerarImpressoPdfNotasNaoTransmitidas(IReadOnlyCollection<string> erros)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Expected O, but got Unknown
			try
			{
				List<string> list = new List<string>();
				list.AddRange(erros);
				List<string> list2 = new List<string>();
				list2.AddRange(list.Select((string linha) => linha));
				ResumoNotasNaoTransmitidasDto val = new ResumoNotasNaoTransmitidasDto
				{
					Empresa = ObterEmpresa(),
					ResumoMovimentacoes = ((IEnumerable<string>)list2).Select((Func<string, CodigoDescricaoDto>)((string x) => new CodigoDescricaoDto
					{
						Descricao = x
					}))
				};
				string text = Path.Combine(Path.GetTempPath(), $"NFSe não transmitidas {DateTime.Now:yyyyMMdd HHmmss}.pdf");
				RelatorioNotasNaoTransmitidas.Criar().ComListaImpresso(val).Build()
					.Processar()
					.ExportToPdf(text);
				Process.Start(text);
			}
			catch (Exception)
			{
				DialogoFactory.MostrarDialogoConfirma(string.Join(Environment.NewLine, erros), false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private async Task GerarNotaManual()
		{
			await FaturarTodosContratosNotaManual(new List<ContratoValidoDto> { CriarContratoFaturar(((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.FirstOrDefault()) });
		}

		public bool PodeGerarNotaManual()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens))
			{
				IEnumerable<GestaoContratoDto> itensSelecionados = base.ItensSelecionados;
				if (itensSelecionados != null && itensSelecionados.Count().Equals(1) && ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Servicos.Any() && !((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado.Cancelado && DadosSistema.PodeEmitirNotaManualRetaguarda)
				{
					return ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado.DataFinal.Date >= DateTime.Now.Date;
				}
			}
			return false;
		}

		private async Task ImprimirFaturaLocacao()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				GestaoContratoDto contratoDto = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto;
				ItemDto[] source = await Task.WhenAll(from dto in ObterDtoCriarItem(CriarContratoFaturar(contratoDto), (TipoMovimentacao)2)
					select ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(dto))));
				FaturaFiscalDto val = new FaturaFiscalDto
				{
					Empresa = ObterEmpresa(),
					Pessoa = contratoDto.Pessoa,
					Itens = ((IEnumerable<ItemDto>)source).Select((Func<ItemDto, ItemFaturaFiscalDto>)((ItemDto x) => new ItemFaturaFiscalDto
					{
						Codigo = x.ProdutoServico.CodigoInterno,
						Descricao = x.ProdutoServico.Descricao,
						TotalLiquido = x.TotalLiquido
					})),
					Numero = contratoDto.NumeroContrato,
					Observacao = contratoDto.Observacao
				};
				_impressoraNaoFiscalService.ImprimirImpressoraNaoFiscalOutrosImpressos((Relatorio)new RelatorioFaturaFiscal(val));
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

		public ContratoValidoDto CriarContratoFaturar(GestaoContratoDto contrato)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			return new ContratoValidoDto
			{
				ContratoReferencia = ((EntityBaseDto)contrato).Id,
				Pessoa = contrato.Pessoa,
				Servicos = contrato.Servicos,
				Tecnico = contrato.Tecnico
			};
		}

		private async Task GerarDavOs()
		{
			_ = 1;
			try
			{
				RecebimentoDto recebimento = ObterDtoCriarRecebimento();
				IEnumerable<ItemDto> source = ObterDtoCriarItem(CriarContratoFaturar(((ListaSelecaoReferencia<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)this).ItensSelecionados.FirstOrDefault()), (TipoMovimentacao)2);
				ItemDto[] itemDtoAtualizado = default(ItemDto[]);
				_ = itemDtoAtualizado;
				itemDtoAtualizado = await Task.WhenAll(source.Select((ItemDto dto) => ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(dto)))));
				if (!itemDtoAtualizado.Any((ItemDto item) => item is ItemDtoNull))
				{
					MovimentacaoDto val = await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoSaidaModuleService service) => service.GerarDavOsGestaoContrato((IEnumerable<ItemDto>)itemDtoAtualizado, ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto, recebimento)));
					if (!DadosSistema.ConfiguracaoDto.Geral.PermitirAcessoMovimentacaoFinanceiroSomenteComTurnoAberto.Valor || DadosSistema.TurnoAberto)
					{
						SelecaoCommandParameter<MovimentacaoDto> val2 = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
						{
						}, true);
						val2.AdicionarParametro("Identificador", (object)((EntityBaseDto)val).Id);
						((NavigationBase)this).Navegar(val2.View, (Action<NavigationResult>)delegate
						{
						}, val2.Parametros);
					}
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public bool PodeGerarDavOs()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens))
			{
				IEnumerable<GestaoContratoDto> itensSelecionados = base.ItensSelecionados;
				if (itensSelecionados != null && itensSelecionados.Count().Equals(1))
				{
					IEnumerable<GestaoContratoDto> itensSelecionados2 = base.ItensSelecionados;
					bool? obj;
					if (itensSelecionados2 == null)
					{
						obj = null;
					}
					else
					{
						GestaoContratoDto obj2 = itensSelecionados2.FirstOrDefault();
						if (obj2 == null)
						{
							obj = null;
						}
						else
						{
							ObjetoDto objeto = obj2.Objeto;
							obj = ((objeto != null) ? new bool?(HelperObject.IsExactlyObject<ObjetoDto>((object)objeto)) : null);
						}
					}
					bool? flag = obj;
					if (flag.HasValue && flag.GetValueOrDefault() && ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido && ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Servicos.Any() && !((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado.Cancelado)
					{
						return ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado.DataFinal.Date >= DateTime.Now.Date;
					}
				}
			}
			return false;
		}

		private RecebimentoDto ObterDtoCriarRecebimento()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Unknown result type (might be due to invalid IL or missing references)
			//IL_0180: Unknown result type (might be due to invalid IL or missing references)
			//IL_019b: Expected O, but got Unknown
			int num = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
			DateTime dateTime = ((((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.DiaVencimentoFinanceiro < DateTime.Now.Day) ? DateTime.Now.AddMonths(1) : DateTime.Now);
			return new RecebimentoDto
			{
				Conta = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Conta,
				EspeciePagamento = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.EspeciePagamento,
				CentroCusto = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.CentroCusto,
				PlanoConta = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.PlanoConta,
				Valor = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Valor,
				DataVencimento = ((((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.DiaVencimentoFinanceiro != 0) ? new DateTime(DateTime.Today.Year, HelperDate.TryToDateTime((object)dateTime).Month, (num < ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.DiaVencimentoFinanceiro) ? num : ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.DiaVencimentoFinanceiro) : DateTime.Now),
				Ordem = 1,
				Juro = DadosSistema.ConfiguracaoDto.Financeiro.PercentualJuro.Valor,
				Multa = DadosSistema.ConfiguracaoDto.Financeiro.PercentualMulta.Valor
			};
		}

		private IEnumerable<ItemDto> ObterDtoCriarItem(ContratoValidoDto contrato, TipoMovimentacao tipo)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Invalid comparison between Unknown and I4
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			if ((int)tipo == 2 || (int)tipo == 9)
			{
				return CriarItemDto(contrato.Servicos, contrato, tipo);
			}
			return CriarItemDto(contrato.Servicos.Where((ServicoContratoDto x) => x.ProdutoServicoEmpresa.ProdutoServico.IsServico), contrato, tipo);
		}

		private IEnumerable<ItemDto> CriarItemDto(IEnumerable<ServicoContratoDto> servicosContratoDto, ContratoValidoDto contrato, TipoMovimentacao tipo)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			return servicosContratoDto.Select((Func<ServicoContratoDto, ItemDto>)((ServicoContratoDto servicoContratoDto) => new ItemDto
			{
				ProdutoServicoEmpresaReferencia = servicoContratoDto.ProdutoServicoEmpresaReferencia,
				QuantidadeInformada = 1m,
				Estoque = new EstoqueDto
				{
					Id = ((EntityBaseDto)servicoContratoDto.ProdutoServicoEmpresa.ProdutoEmpresa.Estoque).Id,
					Quantidade = 1m
				},
				PessoaMovimentacao = contrato.Pessoa,
				EmpresaMovimentacao = ObterEmpresa(),
				PrecoUnitarioInformado = servicoContratoDto.Valor,
				TecnicoResponsavel = (PessoaDto)(((object)contrato.Tecnico) ?? ((object)new PessoaDtoNull())),
				TipoMovimentacao = tipo,
				CalculadoPeloSistema = true,
				DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia,
				Numero = 2
			})).ToList();
		}

		private PessoaDto ObterEmpresa()
		{
			return ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
		}

		public bool PodeCancelar()
		{
			return DadosSistema.VerificarPermissao<GestaoContrato, PermitirCancelarContrato>();
		}

		private void Cancelar()
		{
			string observacao = string.Empty;
			if (DialogoFactory.MostrarDialogoEntradaString(ref observacao, "Observação", $"Informe uma observação para cancelar o contrato número {((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado.NumeroContrato}.", (IIcone)null, false, false, false, (string)null, false, true) == MessageBoxResult.OK)
			{
				GestaoContratoDto val = ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, GestaoContratoDto>((Func<IGestaoContratoModuleService, GestaoContratoDto>)((IGestaoContratoModuleService x) => x.Cancelar(new GestaoContratoHistoricoDto
				{
					Observacao = HelperString.ToStringEmptyWhenNull(observacao),
					IdContrato = ((EntityBaseDto)((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto).Id
				})));
				_notificacao.MostrarNotificacao($"Contrato número {val.NumeroContrato} foi cancelado");
				((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
			}
		}

		public bool PodeRenovar()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido && !((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto.Cancelado)
			{
				return DadosSistema.VerificarPermissao<GestaoContrato, PermitirRenovarContrato>();
			}
			return false;
		}

		private void Renovar()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("Dto", (object)((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto);
			val.Add("RenovarContratoViewModel", (object)new RenovarContratoViewModel(((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto));
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar(typeof(RenovarContratoFichaView).Name, (Action<NavigationResult>)delegate
			{
			}, val2);
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		public bool PodeRenovarContratosEmMassa()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) && !base.ItensSelecionados.Any())
			{
				return DadosSistema.VerificarPermissao<GestaoContrato, PermitirRenovarContrato>();
			}
			return false;
		}

		private void RenovarContratosEmMassa()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("RenovarContratosEmMassaViewModel", (object)new RenovarContratosEmMassaViewModel());
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar(typeof(RenovarContratosEmMassaFichaView).Name, (Action<NavigationResult>)delegate
			{
			}, val2);
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void Visualizar()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Maximizar();
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)125);
			val.Add("VoltarAction", (object)(Action)delegate
			{
				HelperObject.To<ResolucaoBase>((object)((NavigationBase)this).Resolucao).Restaurar();
			});
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		public bool PodeImprimir()
		{
			if (HelperObject.IsNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens) || !((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return false;
			}
			GestaoContratoDto val = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado)?.Dto;
			if (val != null)
			{
				return val.PossuiConteudoParaImpressao;
			}
			return false;
		}

		private void ImprimirContrato()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				GestaoContratoDto dto = ((ViewModelDtoBase<GestaoContratoDto>)(object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.Selecionado).Dto;
				if (dto.TipoImpressaoContrato == 1)
				{
					GerarContratoDeModelo(dto);
					return;
				}
				ImpressoContrato impressoContrato = new ImpressoContrato(dto, dto.DadosContrato);
				_impressoraNaoFiscalService.ImprimirImpressoraNaoFiscalOutrosImpressos((Relatorio)(object)impressoContrato);
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

		private void GerarContratoDeModelo(GestaoContratoDto dto)
		{
			byte[] result = ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<byte[]>>((Func<IGestaoContratoModuleService, Task<byte[]>>)((IGestaoContratoModuleService service) => service.ObterModeloContrato(((EntityBaseDto)dto).Id))).Result;
			if (result == null || result.Length == 0)
			{
				DialogoFactory.MostrarDialogoConfirma("Nenhum modelo de contrato (.docx) foi encontrado para este contrato.", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			byte[] array = MesclagemDocumentoWord.Mesclar(result, (IDictionary<string, string>)CamposContrato.MontarValores(dto), dto.NomeArquivoModeloContrato);
			string text = Path.GetExtension(dto.NomeArquivoModeloContrato);
			if (string.IsNullOrEmpty(text))
			{
				text = ".docx";
			}
			LoaderProcessoService.EsconderLoader();
			if (DialogoFactory.MostrarDialogoSimNao("Deseja gerar o contrato em PDF?\n Caso contrário, o contrato será aberto em seu formato original.", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				LoaderProcessoService.MostrarLoader("Gerando PDF...");
				array = ConversorDocumentoParaPdf.Converter(array, dto.NomeArquivoModeloContrato);
				text = ".pdf";
			}
			string path = $"Contrato {dto.NumeroContrato} - {DateTime.Now:yyyyMMdd HHmmss}{text}";
			string text2 = Path.Combine(Path.GetTempPath(), path);
			File.WriteAllBytes(text2, array);
			Process.Start(text2);
		}

		private bool PodeVisualizar()
		{
			if (((ListaSelecaoBase<GestaoContratoDto, GestaoContratoViewModel, GestaoContrato>)(object)this).Sessao.VerificarPermissao<GestaoContrato, VisualizarRelatorio>() && DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return !((ListaSelecaoBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).PodeSelecionar();
			}
			return false;
		}

		private bool PodeExibirAnexos()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens))
			{
				return ((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private async Task Anexos()
		{
			try
			{
				GestaoContratoDto dto = default(GestaoContratoDto);
				_ = dto;
				dto = await ((ListaSelecaoBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).ObterDtoItemSelecionado();
				Lista<AnexoContratoDto> imagensObjetos = new Lista<AnexoContratoDto>();
				Lista<AnexoContratoDto> arquivosObjetos = new Lista<AnexoContratoDto>();
				imagensObjetos.Adicionar(await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>)((IGestaoContratoModuleService service) => service.ObterImagens(((EntityBaseDto)dto).Id))));
				arquivosObjetos.Adicionar(await ((ListaReferenciaBase<GestaoContratoDto, GestaoContratoViewModel, IGestaoContratoModuleService, GestaoContrato>)(object)this)._service.Canal<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>)((IGestaoContratoModuleService service) => service.ObterArquivos(((EntityBaseDto)dto).Id))));
				Imagens = new Lista<ImagemViewModel>();
				foreach (AnexoContratoDto iten in imagensObjetos.Itens)
				{
					ImagemViewModel val = new ImagemViewModel(HelperImage.ToBitmapSource(iten.Image), iten.FileName, HelperDate.FormatarData(iten.DataCriacao, (FormatoData)6));
					Imagens.Adicionar(val);
				}
				Arquivos = new Lista<ArquivoViewModel>();
				foreach (AnexoContratoDto iten2 in arquivosObjetos.Itens)
				{
					ArquivoViewModel val2 = new ArquivoViewModel(iten2.Arquivo, iten2.FileName, iten2.NomeArquivo, HelperDate.FormatarData(iten2.DataCriacao, (FormatoData)6));
					Arquivos.Adicionar(val2);
				}
				NavigationParameters val3 = new NavigationParameters();
				val3.Add("ItemModuloCreator", (object)42);
				NavigationParameters val4 = val3;
				val4.Add("Imagens", (object)Imagens.Itens);
				val4.Add("Arquivos", (object)Arquivos.Itens);
				((NavigationBase)this).Navegar("VisualizarAnexoFichaView", (Action<NavigationResult>)delegate
				{
				}, val4);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
			((ListaBase<GestaoContratoDto, GestaoContratoViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
		}

		public override void Dispose()
		{
			FaturarContratosSelecionadosCommand = null;
			GerarFaturasFiscaisContratosSelecionadosCommand = null;
			FaturarTudoCommand = null;
			GerarFaturaFiscalTudoCommand = null;
			GerarDavOsCommand = null;
			GerarNotaManualCommand = null;
			RenovarContratosEmMassaCommand = null;
			((ListaManutencaoBase<GestaoContratoDto, GestaoContratoViewModel, GestaoContrato>)(object)this).Dispose();
		}
	}
	public class GestaoContratoViewModel : ViewModelAtivoBase<GestaoContratoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase, IViewModelSelecao
	{
		public PessoaDto Pessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value, (Action)delegate
				{
					((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Pessoa = Pessoa;
					DiaVencimentoFinanceiro = Pessoa.DiaAcerto;
					NomePessoa = ObterNomePessoa();
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ClienteInformado));
				});
			}
		}

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoa)
		{
			Pessoa = pessoa;
		}, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => HelperObject.IsExactlyObject<ClienteDto>((object)pessoaDto.Cliente) && !pessoaDto.ConsumidorReservadoSistema), "Selecione um cadastro do tipo 'Cliente'!", (TipoPessoaCadastro)0, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o campo 'Pessoa' pela pesquisa: o texto digitado nao corresponde a um cadastro.")]
		public string PessoaSelecionada
		{
			get
			{
				if (!HelperObject.IsNull((object)Pessoa) && !HelperObject.Is<PessoaDtoNull>((object)Pessoa))
				{
					return NomePessoa;
				}
				return string.Empty;
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Pessoa' é obrigatório.")]
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

		public bool ClienteInformado => !string.IsNullOrEmpty(NomePessoa);

		public ProdutoServicoEmpresaDto Servico
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => Servico));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => Servico), value, (Action)delegate
				{
					((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Servico = Servico;
					ValorServicoSelecionado = HelperDecimal.TruncarValor(Servico.Preco.Venda.Valor, 4);
				});
			}
		}

		public ISelecaoCommandParameter ServicoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto servico)
		{
			Servico = servico;
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoNoValor = !FocoNoValor;
			});
		}, true);

		public Lista<ServicosContratoViewModel> ServicosViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ServicosContratoViewModel>>((Expression<Func<Lista<ServicosContratoViewModel>>>)(() => ServicosViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ServicosContratoViewModel>>((Expression<Func<Lista<ServicosContratoViewModel>>>)(() => ServicosViewModel), value);
			}
		}

		public ObjetoDto Objeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObjetoDto>((Expression<Func<ObjetoDto>>)(() => Objeto));
			}
			set
			{
				((BindableBase)this).SetProperty<ObjetoDto>((Expression<Func<ObjetoDto>>)(() => Objeto), value);
			}
		}

		public ISelecaoCommandParameter ObjetoCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<ObjetoDto>("ObjetoListaView", (Action<ObjetoDto>)delegate(ObjetoDto objeto)
		{
			Objeto = objeto;
			((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Objeto = objeto;
		}, true);

		public long NumeroContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => NumeroContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => NumeroContrato), value);
			}
		}

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

		public SelecaoCommandParameter<TipoContratoDto> TipoContratoCommandParameter => new SelecaoCommandParameter<TipoContratoDto>("TipoContratoListaView", (Action<TipoContratoDto>)delegate(TipoContratoDto tipoContrato)
		{
			if (!TiposContrato.Existe((Expression<Func<TipoContratoDto, bool>>)((TipoContratoDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)tipoContrato).Id))))
			{
				TiposContrato.Adicionar(tipoContrato);
			}
			TiposContrato.Posicionar((Expression<Func<TipoContratoDto, bool>>)((TipoContratoDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)tipoContrato).Id)));
		}, true);

		public TipoContratoDto TipoContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<TipoContratoDto>((Expression<Func<TipoContratoDto>>)(() => TipoContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<TipoContratoDto>((Expression<Func<TipoContratoDto>>)(() => TipoContrato), value);
			}
		}

		public Lista<TipoContratoDto> TiposContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TipoContratoDto>>((Expression<Func<Lista<TipoContratoDto>>>)(() => TiposContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TipoContratoDto>>((Expression<Func<Lista<TipoContratoDto>>>)(() => TiposContrato), value);
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

		public string ContratoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ContratoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ContratoReferencia), value);
			}
		}

		public decimal ValorServicoSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorServicoSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorServicoSelecionado), value);
			}
		}

		public bool FocoNoValor
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoValor));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoValor), value);
			}
		}

		public bool ContratoMensal
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ContratoMensal));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ContratoMensal), value, (Action)OnSelecionouEvent);
			}
		}

		public bool FaturaFiscal
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FaturaFiscal));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FaturaFiscal), value);
			}
		}

		public Lista<PlanoContaDto> PlanosConta
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PlanoContaDto>>((Expression<Func<Lista<PlanoContaDto>>>)(() => PlanosConta));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PlanoContaDto>>((Expression<Func<Lista<PlanoContaDto>>>)(() => PlanosConta), value);
			}
		}

		public Lista<CentroCustoDto> CentrosCusto
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CentroCustoDto>>((Expression<Func<Lista<CentroCustoDto>>>)(() => CentrosCusto));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CentroCustoDto>>((Expression<Func<Lista<CentroCustoDto>>>)(() => CentrosCusto), value);
			}
		}

		public ObservableCollection<ContaDto> Contas { get; protected set; }

		[Required(ErrorMessage = "O campo 'Conta' é obrigatório.")]
		public ContaDto Conta
		{
			get
			{
				return ((BindableBase)this).GetProperty<ContaDto>((Expression<Func<ContaDto>>)(() => Conta));
			}
			set
			{
				((BindableBase)this).SetProperty<ContaDto>((Expression<Func<ContaDto>>)(() => Conta), value, (Action)OnAlterouConta);
			}
		}

		public Lista<EspeciePagamentoDto> Especies
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<EspeciePagamentoDto>>((Expression<Func<Lista<EspeciePagamentoDto>>>)(() => Especies));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<EspeciePagamentoDto>>((Expression<Func<Lista<EspeciePagamentoDto>>>)(() => Especies), value);
			}
		}

		public Lista<PessoaDto> Tecnicos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Tecnicos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Tecnicos), value);
			}
		}

		public int DiaVencimentoFinanceiro
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => DiaVencimentoFinanceiro));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => DiaVencimentoFinanceiro), value);
			}
		}

		public bool Cancelado { get; set; }

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

		public SelecaoCommandParameter<CentroCustoDto> CentroCustoCommandParameter => new SelecaoCommandParameter<CentroCustoDto>("CentroCustoListaView", (Action<CentroCustoDto>)delegate(CentroCustoDto centroCusto)
		{
			if (!CentrosCusto.Existe((Expression<Func<CentroCustoDto, bool>>)((CentroCustoDto x) => x.CodigoUnico.Equals(centroCusto.CodigoUnico))))
			{
				CentrosCusto.Adicionar(centroCusto);
			}
			CentrosCusto.Posicionar((Expression<Func<CentroCustoDto, bool>>)((CentroCustoDto x) => x.CodigoUnico.Equals(centroCusto.CodigoUnico)));
		}, true);

		public SelecaoCommandParameter<PlanoContaDto> PlanoContaCommandParameter => new SelecaoCommandParameter<PlanoContaDto>("PlanoContaListaView", (Action<PlanoContaDto>)delegate(PlanoContaDto planoConta)
		{
			if (!PlanosConta.Existe((Expression<Func<PlanoContaDto, bool>>)((PlanoContaDto x) => x.CodigoUnico.Equals(planoConta.CodigoUnico))))
			{
				PlanosConta.Adicionar(planoConta);
			}
			PlanosConta.Posicionar((Expression<Func<PlanoContaDto, bool>>)((PlanoContaDto x) => x.CodigoUnico.Equals(planoConta.CodigoUnico)));
		}, true);

		public SelecaoCommandParameter<ContaDto> ContaCommandParameter => CadastrarConta();

		public bool ExibirAnexosVisivel => ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Anexos.Any();

		public bool ImprimirVisivel => ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.PossuiConteudoParaImpressao;

		public bool CancelarVisivel
		{
			get
			{
				if (!Cancelado)
				{
					return DadosSistema.VerificarPermissao<GestaoContrato, PermitirCancelarContrato>();
				}
				return false;
			}
		}

		public bool RenovarVisivel
		{
			get
			{
				if (!Cancelado)
				{
					return DadosSistema.VerificarPermissao<GestaoContrato, PermitirRenovarContrato>();
				}
				return false;
			}
		}

		public bool ImprimirFaturaFiscalVisivel
		{
			get
			{
				if (!Cancelado && DadosSistema.VerificarPermissao<GestaoContrato, PermitirGerarFaturaFiscal>())
				{
					return FaturaFiscal;
				}
				return false;
			}
		}

		public DelegateCommand RemoverTipoContratoCommand { get; set; }

		public DelegateCommand RemoverObjetoCommand { get; set; }

		public DelegateCommand AdicionarServicoCommand { get; set; }

		public event EventHandler SelecionouEvent;

		public GestaoContratoViewModel()
		{
		}

		public GestaoContratoViewModel(GestaoContratoDto dto)
			: base(dto)
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Expected O, but got Unknown
			RemoverTipoContratoCommand = new DelegateCommand((Action)RemoverTipoContrato);
			RemoverObjetoCommand = new DelegateCommand((Action)RemoverObjeto);
			AdicionarServicoCommand = new DelegateCommand((Action)AdicionarServico, (Func<bool>)(() => HelperObject.IsExactlyObject<ProdutoServicoEmpresaDto>((object)Servico)), (bool?)null);
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)0, (Action<PessoaDto>)delegate(PessoaDto pessoa)
			{
				Pessoa = pessoa;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private string ObterNomePessoa()
		{
			if (!HelperObject.Is<PessoaDtoNull>((object)Pessoa) && !HelperObject.IsNull((object)Pessoa))
			{
				return Pessoa.Nome;
			}
			return string.Empty;
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomePessoa) && HelperObject.IsNotNull((object)Pessoa) && HelperObject.IsNot<PessoaDtoNull>((object)Pessoa))
			{
				Pessoa = (PessoaDto)new PessoaDtoNull();
			}
		}

		public void Selecionar()
		{
			Selecionado = !Selecionado;
		}

		public void AdicionarServico()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			ServicoContratoDto servico = new ServicoContratoDto
			{
				ProdutoServicoEmpresa = Servico,
				ProdutoServicoEmpresaReferencia = HelperString.ToStringEmptyWhenNull(((EntityBaseDto)Servico).Id),
				Valor = ValorServicoSelecionado
			};
			ServicosViewModel.Adicionar(ObterServicosViewModel(servico));
			Servico = (ProdutoServicoEmpresaDto)new ProdutoServicoEmpresaDtoNull();
			ValorServicoSelecionado = 0m;
		}

		private ServicosContratoViewModel ObterServicosViewModel(ServicoContratoDto servico)
		{
			return new ServicosContratoViewModel(servico, RemoverServico);
		}

		private void RemoverServico(object servicoViewModel)
		{
			ServicosViewModel.Remover(servicoViewModel as ServicosContratoViewModel);
		}

		private void RemoverTipoContrato()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			TiposContrato.ItemSelecionado = (TipoContratoDto)new TipoContratoDtoNull();
		}

		private void RemoverObjeto()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Objeto = (ObjetoDto)new ObjetoDtoNull();
		}

		protected virtual void OnSelecionouEvent()
		{
			this.SelecionouEvent?.Invoke(this, EventArgs.Empty);
		}

		private SelecaoCommandParameter<ContaDto> CadastrarConta()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			ContaDto val = new ContaDto();
			SelecaoCommandParameter<ContaDto> val2 = new SelecaoCommandParameter<ContaDto>("ContaFichaAdicionarView", (Action<ContaDto>)SetarConta, true);
			Type type = Type.GetType(QualifiedNames.ContaViewModel);
			if (type != null)
			{
				object obj = Activator.CreateInstance(type, val);
				val2.AdicionarParametro("ContaViewModel", (obj is IViewModelPrincipal) ? obj : null);
			}
			return val2;
		}

		private void SetarConta(ContaDto conta)
		{
			if (Contas.All((ContaDto x) => ((EntityBaseDto)x).Id != ((EntityBaseDto)conta).Id))
			{
				Contas.Add(conta);
			}
			PosicionarConta(conta);
		}

		public virtual void OnAlterouConta()
		{
		}

		public override void ProjectedAsViewModel()
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			base.Ativo = ((EntityDto)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto).Ativo;
			Pessoa = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Pessoa;
			Tecnicos = new Lista<PessoaDto>();
			Tecnicos.Adicionar(new PessoaDto
			{
				Nome = "Nenhum",
				Id = "Nenhum"
			});
			Tecnicos.Adicionar(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Tecnicos);
			Tecnicos.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Tecnico).Id)));
			ServicosViewModel = new Lista<ServicosContratoViewModel>();
			ServicosViewModel.Adicionar(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Servicos.Select(ObterServicosViewModel));
			if (((EntityBaseDto)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto).IsTransient())
			{
				int num = DadosSistema.GetService().Canal<IGestaoContratoModuleService, int>((Func<IGestaoContratoModuleService, int>)((IGestaoContratoModuleService a) => a.ObterSequenciaContrato(DadosSistema.ConfiguracaoDto))) + 1;
				NumeroContrato = num;
			}
			else
			{
				NumeroContrato = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.NumeroContrato;
			}
			DataInicial = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.DataInicial;
			DataFinal = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.DataFinal;
			ProjetarTiposContrato();
			Observacao = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Observacao;
			ContratoMensal = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.ContratoMensal;
			FaturaFiscal = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.FaturaFiscal;
			ProjetarEspecies();
			Contas = new ObservableCollection<ContaDto>(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Contas);
			PosicionarConta(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Conta);
			PlanosConta = new Lista<PlanoContaDto>();
			PlanosConta.Adicionar(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.PlanosConta);
			PlanosConta.Posicionar((Expression<Func<PlanoContaDto, bool>>)((PlanoContaDto x) => x.CodigoUnico.Equals(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.PlanoConta.CodigoUnico)));
			CentrosCusto = new Lista<CentroCustoDto>();
			CentrosCusto.Adicionar(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.CentrosCusto);
			CentrosCusto.Posicionar((Expression<Func<CentroCustoDto, bool>>)((CentroCustoDto x) => x.CodigoUnico.Equals(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.CentroCusto.CodigoUnico)));
			Cancelado = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Cancelado;
			DiaVencimentoFinanceiro = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.DiaVencimentoFinanceiro;
			Objeto = ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.Objeto;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			object obj = dto;
			GestaoContratoDto val = (GestaoContratoDto)((obj is GestaoContratoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.Pessoa = Pessoa;
				val.NumeroContrato = NumeroContrato;
				val.DataInicial = DataInicial;
				val.DataFinal = DataFinal;
				val.TipoContrato = HelperObject.IsNullUse<TipoContratoDto>(TiposContrato.ItemSelecionado, (TipoContratoDto)new TipoContratoDtoNull());
				val.Observacao = Observacao;
				val.Servicos = ServicosViewModel.Itens.Select((ServicosContratoViewModel x) => x.ServicoContrato);
				val.Tecnico = (PessoaDto)((!((EntityBaseDto)Tecnicos.ItemSelecionado).Id.Equals("Nenhum")) ? ((object)Tecnicos.ItemSelecionado) : ((object)new PessoaDtoNull()));
				val.EspeciePagamento = Especies.ItemSelecionado;
				val.Conta = Conta;
				val.CentroCusto = CentrosCusto.ItemSelecionado;
				val.PlanoConta = PlanosConta.ItemSelecionado;
				val.ContratoMensal = ContratoMensal;
				val.FaturaFiscal = ContratoMensal && FaturaFiscal;
				val.Cancelado = Cancelado;
				val.DiaVencimentoFinanceiro = DiaVencimentoFinanceiro;
				val.Objeto = Objeto;
				((ViewModelDtoBase<GestaoContratoDto>)(object)this).AtualizarDto<GestaoContratoDto>(ref val);
			}
		}

		private void ProjetarTiposContrato()
		{
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			TiposContrato = new Lista<TipoContratoDto>();
			TiposContrato.Adicionar(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.TiposContrato);
			if (HelperObject.Is<TipoContratoDtoNull>((object)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.TipoContrato))
			{
				TiposContrato.ItemSelecionado = (TipoContratoDto)new TipoContratoDtoNull();
				return;
			}
			TiposContrato.Posicionar((Expression<Func<TipoContratoDto, bool>>)((TipoContratoDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.TipoContrato).Id)));
		}

		private void ProjetarEspecies()
		{
			EspeciePagamentoDto[] especies = EspeciePagamentoProjection.ProjectedAsCollectionDto((IEnumerable<EspeciePagamento>)((ValueObjectRepository<EspeciePagamento>)(object)ValueObjectService<EspeciePagamento, EspeciePagamentoValueObjectRepository>.ValueObjectRepository).Dados).ToArray();
			Especies = new Lista<EspeciePagamentoDto>();
			Especies.Adicionar(OrdenarEspecies(especies));
			if (HelperObject.Is<EspeciePagamentoDtoNull>((object)((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.EspeciePagamento))
			{
				Especies.PosicionarNoPrimeiro();
				return;
			}
			Especies.Posicionar((Expression<Func<EspeciePagamentoDto, bool>>)((EspeciePagamentoDto x) => x.Codigo == ((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto.EspeciePagamento.Codigo));
		}

		private IEnumerable<EspeciePagamentoDto> OrdenarEspecies(IEnumerable<EspeciePagamentoDto> especies)
		{
			List<EspeciePagamentoDto> source = HelperIEnumerable.ToListEmptyWhenNull<EspeciePagamentoDto>((IEnumerable<EspeciePagamentoDto>)especies.OrderBy((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo));
			List<EspeciePagamentoDto> first = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 1).ToList();
			List<EspeciePagamentoDto> second = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 2).ToList();
			List<EspeciePagamentoDto> second2 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 3).ToList();
			List<EspeciePagamentoDto> second3 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 4).ToList();
			List<EspeciePagamentoDto> second4 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 6).ToList();
			List<EspeciePagamentoDto> second5 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 8).ToList();
			List<EspeciePagamentoDto> second6 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 12).ToList();
			List<EspeciePagamentoDto> second7 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 11).ToList();
			List<EspeciePagamentoDto> second8 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 9).ToList();
			List<EspeciePagamentoDto> second9 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 20).ToList();
			List<EspeciePagamentoDto> second10 = source.Where((EspeciePagamentoDto x) => x.EspecieRecebimento.Codigo == 21).ToList();
			return first.Concat(second4).Concat(second5).Concat(second3)
				.Concat(second6)
				.Concat(second7)
				.Concat(second8)
				.Concat(second)
				.Concat(second2)
				.Concat(second9)
				.Concat(second10);
		}

		private void PosicionarConta(ContaDto contaDto)
		{
			Conta = (HelperObject.Is<ContaDtoNull>((object)contaDto) ? Contas.FirstOrDefault() : Contas.FirstOrDefault((ContaDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)contaDto).Id)));
		}

		public IFormulario CriarFormulario()
		{
			PreencherPlanoContaECentroCustoSaidaLocal(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto);
			return (IFormulario)(object)new GestaoContratoFormulario(((ViewModelDtoBase<GestaoContratoDto>)(object)this).Dto);
		}

		private void PreencherPlanoContaECentroCustoSaidaLocal(GestaoContratoDto dto)
		{
			if (((EntityBaseDto)dto).IsTransient())
			{
				dto.PlanoConta = DadosSistema.ObterPlanoContaSaidaLocal();
				dto.CentroCusto = DadosSistema.ObterCentroCustoSaidaLocal();
			}
		}

		public string ToTituloLista()
		{
			return "Gestão de contratos";
		}

		public string ToTituloFicha()
		{
			return "Gestão de contratos";
		}

		public override void Dispose()
		{
			Contas.Clear();
			((ViewModelValidationBase)PlanosConta).Dispose();
			((ViewModelValidationBase)CentrosCusto).Dispose();
			((ViewModelValidationBase)TiposContrato).Dispose();
			((ViewModelValidationBase)ServicosViewModel).Dispose();
			PlanosConta = null;
			CentrosCusto = null;
			TiposContrato = null;
			Conta = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class GestaoContratoAnexosAbaViewModel : AbaAuxiliarViewModelBase
	{
		public GestaoContratoAnexosAbaViewModel(IViewModelDtoBase viewModel)
			: base("Anexos", "Anexos do contrato", viewModel)
		{
		}
	}
	public class GestaoContratoAnexosViewModel : ViewModelDtoBase<GestaoContratoDto>
	{
		private readonly IWService _service;

		public bool IsImage;

		public Lista<ImagemViewModel> Imagens
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => Imagens));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => Imagens), value);
			}
		}

		public Lista<ImagemViewModel> ImagensExcluidas { get; set; } = new Lista<ImagemViewModel>();


		public Lista<ArquivoViewModel> Arquivos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Arquivos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => Arquivos), value);
			}
		}

		public Lista<ArquivoViewModel> ArquivosExcluidos { get; set; } = new Lista<ArquivoViewModel>();


		public ICommand CarregarImagemCommand { get; set; }

		public ICommand CarregarArquivoCommand { get; set; }

		public ICommand<ImagemViewModel> RemoverImagemCommand { get; set; }

		public ICommand<ArquivoViewModel> RemoverArquivoCommand { get; set; }

		public ICommand AbrirAnexoImagemCommand { get; set; }

		public ICommand AbrirAnexoArquivoCommand { get; set; }

		public GestaoContratoAnexosViewModel(GestaoContratoDto dto, IWService service)
			: base(dto)
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			_service = service;
			CarregarImagemCommand = (ICommand)new DelegateCommand((Action)CarregarImagem);
			CarregarArquivoCommand = (ICommand)new DelegateCommand((Action)CarregarArquivo);
			RemoverImagemCommand = (ICommand<ImagemViewModel>)(object)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)RemoverImagem);
			RemoverArquivoCommand = (ICommand<ArquivoViewModel>)(object)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)RemoverArquivo);
			AbrirAnexoImagemCommand = (ICommand)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)AbrirAnexoImagem);
			AbrirAnexoArquivoCommand = (ICommand)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)AbrirAnexoArquivo);
		}

		private void CarregarImagem()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione uma imagem",
				Filter = "Arquivos de imagem (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				BitmapSource bitmapSource = HelperImage.ToBitmapSource(File.ReadAllBytes(openFileDialog.FileName));
				Imagens.Adicionar(new ImagemViewModel(bitmapSource, openFileDialog.FileName, HelperDate.FormatarData(DateTime.Now, (FormatoData)6)));
			}
		}

		private void RemoverImagem(ImagemViewModel obj)
		{
			Imagens.Remover(obj);
			ImagensExcluidas.Adicionar(obj);
		}

		private void CarregarArquivo()
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione um arquivo",
				Filter = "Arquivos (*.pdf;*.txt;*.doc;*.docx)|*.pdf;*.txt;*.doc;*.docx|Pdf Files (*.pdf)|*.pdf|Arquivos de texto (*.txt)|*.txt|Documentos do Word (*.doc;*.docx)|*.doc;*.docx"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string text = Convert.ToBase64String(File.ReadAllBytes(openFileDialog.FileName));
				Arquivos.Adicionar(new ArquivoViewModel(text, openFileDialog.FileName, openFileDialog.SafeFileName.Replace("-", ""), HelperDate.FormatarData(DateTime.Now, (FormatoData)6)));
			}
		}

		private void RemoverArquivo(ArquivoViewModel obj)
		{
			Arquivos.Remover(obj);
			ArquivosExcluidos.Adicionar(obj);
		}

		public override void ProjectedAsViewModel()
		{
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Expected O, but got Unknown
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Expected O, but got Unknown
			ImagensExcluidas.Adicionar(base.Dto.AnexosExluidos.Where((AnexoContratoDto anexo) => anexo.IsImage).Select((Func<AnexoContratoDto, ImagemViewModel>)((AnexoContratoDto anexo) => new ImagemViewModel((BitmapSource)null, anexo.FileName, HelperDate.FormatarData(anexo.DataCriacao, (FormatoData)6)))));
			Imagens = new Lista<ImagemViewModel>();
			foreach (AnexoContratoDto item in base.Dto.Anexos.Where((AnexoContratoDto x) => x.IsImage))
			{
				BitmapSource bitmapSource = HelperImage.ToBitmapSource(HelperImage.Decompress(item.Image));
				Imagens.Adicionar(new ImagemViewModel(bitmapSource, string.Empty, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6)));
			}
			ObterImagens(((EntityBaseDto)base.Dto).Id);
			ArquivosExcluidos.Adicionar(base.Dto.AnexosExluidos.Where((AnexoContratoDto x) => !x.IsImage).Select((Func<AnexoContratoDto, ArquivoViewModel>)((AnexoContratoDto anexo) => new ArquivoViewModel((string)null, anexo.FileName, (string)null, HelperDate.FormatarData(anexo.DataCriacao, (FormatoData)6)))));
			Arquivos = new Lista<ArquivoViewModel>();
			foreach (AnexoContratoDto item2 in base.Dto.Anexos.Where((AnexoContratoDto x) => !x.IsImage))
			{
				Arquivos.Adicionar(new ArquivoViewModel(item2.Arquivo, string.Empty, string.Empty, HelperDate.FormatarData(item2.DataCriacao, (FormatoData)6)));
			}
			ObterArquivos(((EntityBaseDto)base.Dto).Id);
		}

		private void ObterImagens(string contratoEmpresaReferencia)
		{
			LoaderProcessoService.MostrarLoader("Obtendo imagens...");
			new Thread((ThreadStart)async delegate
			{
				try
				{
					IEnumerable<AnexoContratoDto> imagens = default(IEnumerable<AnexoContratoDto>);
					_ = imagens;
					imagens = await _service.Canal<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>)((IGestaoContratoModuleService service) => service.ObterImagens(contratoEmpresaReferencia)));
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_003c: Unknown result type (might be due to invalid IL or missing references)
						//IL_0046: Expected O, but got Unknown
						foreach (AnexoContratoDto item in imagens)
						{
							ImagemViewModel adicionar = new ImagemViewModel(HelperImage.ToBitmapSource(item.Image), item.FileName, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6));
							if (!Imagens.Existe((Expression<Func<ImagemViewModel, bool>>)((ImagemViewModel x) => HelperImage.ToArrayByte(x.Imagem).SequenceEqual(HelperImage.ToArrayByte(adicionar.Imagem)))) && !HelperBool.In<string>(item.FileName, (from x in base.Dto.AnexosExluidos
								where x.IsImage
								select x into anexos
								select anexos.FileName).ToArray()))
							{
								Imagens.Adicionar(adicionar);
							}
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
					ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				}
			}).Start();
		}

		private void ObterArquivos(string contratoReferencia)
		{
			LoaderProcessoService.MostrarLoader("Obtendo arquivos...");
			new Thread((ThreadStart)async delegate
			{
				try
				{
					IEnumerable<AnexoContratoDto> arquivos = default(IEnumerable<AnexoContratoDto>);
					_ = arquivos;
					arquivos = await _service.Canal<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>((Func<IGestaoContratoModuleService, Task<IEnumerable<AnexoContratoDto>>>)((IGestaoContratoModuleService service) => service.ObterArquivos(contratoReferencia)));
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_003d: Unknown result type (might be due to invalid IL or missing references)
						//IL_0047: Expected O, but got Unknown
						foreach (AnexoContratoDto item in arquivos)
						{
							ArquivoViewModel adicionar = new ArquivoViewModel(item.Arquivo, item.FileName, item.NomeArquivo, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6));
							if (!Arquivos.Existe((Expression<Func<ArquivoViewModel, bool>>)((ArquivoViewModel x) => x.Arquivo.SequenceEqual(adicionar.Arquivo))) && !HelperBool.In<string>(item.FileName, (from x in base.Dto.AnexosExluidos
								where !x.IsImage
								select x into anexo
								select anexo.FileName).ToArray()))
							{
								Arquivos.Adicionar(adicionar);
							}
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
					ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
				}
			}).Start();
		}

		private void AbrirAnexoImagem(ImagemViewModel obj)
		{
			string text = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Temp");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text).Attributes = FileAttributes.Directory;
			}
			try
			{
				File.WriteAllBytes(Path.Combine(text, obj.FileName), HelperImage.ToArrayByte(obj.Imagem));
			}
			catch (Exception)
			{
				DialogoFactory.MostrarDialogoConfirma("Ocorreu um erro ao salvar a imagem na pasta " + text, false, System.Windows.HorizontalAlignment.Center);
			}
			Process.Start(Path.Combine(text, obj.FileName));
		}

		private void AbrirAnexoArquivo(ArquivoViewModel obj)
		{
			string text = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Temp");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text).Attributes = FileAttributes.Directory;
			}
			try
			{
				byte[] bytes = Convert.FromBase64String(obj.Arquivo);
				File.WriteAllBytes(Path.Combine(text, obj.FileName), bytes);
			}
			catch (Exception)
			{
				DialogoFactory.MostrarDialogoConfirma("Ocorreu um erro ao salvar arquivo na pasta " + text, false, System.Windows.HorizontalAlignment.Center);
			}
			Process.Start(Path.Combine(text, obj.FileName));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GestaoContratoDto val = (GestaoContratoDto)((obj is GestaoContratoDto) ? obj : null);
			if (val != null)
			{
				List<AnexoContratoDto> first = HelperIEnumerable.ToListEmptyWhenNull<AnexoContratoDto>(((IEnumerable<ImagemViewModel>)Imagens.Itens).Select((Func<ImagemViewModel, AnexoContratoDto>)((ImagemViewModel x) => new AnexoContratoDto
				{
					FileName = x.FileName,
					Image = HelperImage.Compress(x.Imagem),
					IsImage = true
				})));
				List<AnexoContratoDto> first2 = HelperIEnumerable.ToListEmptyWhenNull<AnexoContratoDto>(((IEnumerable<ImagemViewModel>)ImagensExcluidas.Itens).Select((Func<ImagemViewModel, AnexoContratoDto>)((ImagemViewModel x) => new AnexoContratoDto
				{
					FileName = x.FileName,
					IsImage = true
				})));
				List<AnexoContratoDto> second = HelperIEnumerable.ToListEmptyWhenNull<AnexoContratoDto>(((IEnumerable<ArquivoViewModel>)Arquivos.Itens).Select((Func<ArquivoViewModel, AnexoContratoDto>)((ArquivoViewModel x) => new AnexoContratoDto
				{
					FileName = x.FileName,
					Arquivo = x.Arquivo,
					NomeArquivo = x.NomeArquivo
				})));
				List<AnexoContratoDto> second2 = HelperIEnumerable.ToListEmptyWhenNull<AnexoContratoDto>(((IEnumerable<ArquivoViewModel>)ArquivosExcluidos.Itens).Select((Func<ArquivoViewModel, AnexoContratoDto>)((ArquivoViewModel x) => new AnexoContratoDto
				{
					FileName = x.FileName
				})));
				val.Anexos = first.Union(second);
				val.AnexosExluidos = first2.Union(second2);
				base.AtualizarDto<GestaoContratoDto>(ref val);
			}
		}
	}
}
namespace Otus.Modulo.GestaoContratos.ViewModels.TiposContrato
{
	public class TipoContratoAbaViewModel : AbaPrincipalViewModelBase
	{
		public TipoContratoAbaViewModel(IViewModelPrincipal viewModel)
			: base("TIPOS DE CONTRATO", "Dados principais", viewModel)
		{
		}
	}
	public class TipoContratoFichaAdicionarViewModel : FichaAdicionarComConfirmacao<TipoContratoDto, TipoContratoViewModel, ITipoContratoModuleService>
	{
		public TipoContratoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class TipoContratoFormulario : FormularioEntity<TipoContratoDto>
	{
		public TipoContratoFormulario(TipoContratoDto dto)
			: base(dto)
		{
			CriarAbas();
		}

		private void CriarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new TipoContratoAbaViewModel((IViewModelPrincipal)(object)new TipoContratoViewModel(base.Dto)) });
		}
	}
	public class TipoContratoListaViewModel : ListaEntityAdicionarAtivarDesativar<TipoContratoDto, TipoContratoViewModel, ITipoContratoModuleService, TipoContrato>
	{
		public TipoContratoListaViewModel(IWService service, ISessao sessao, IEntityAtivos<TipoContratoDto, ITipoContratoModuleService> ativos, IEntityInativos<TipoContratoDto, ITipoContratoModuleService> inativos)
			: base(service, sessao, typeof(TipoContratoFichaAdicionarView).Name, ativos, inativos)
		{
		}
	}
	public class TipoContratoViewModel : ViewModelDtoBase<TipoContratoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
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

		public TipoContratoViewModel()
		{
		}

		public TipoContratoViewModel(TipoContratoDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Descricao = base.Dto.Descricao;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			TipoContratoDto val = (TipoContratoDto)((obj is TipoContratoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Descricao = Descricao;
				base.AtualizarDto<TipoContratoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new TipoContratoFormulario(base.Dto);
		}

		public string ToTituloLista()
		{
			return "Tipos de contrato";
		}

		public string ToTituloFicha()
		{
			return "Tipo do contrato";
		}
	}
	public class TiposContratoViewModel : ModuloBase
	{
		public TiposContratoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloTipoContratoRegion, typeof(TipoContratoListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.GestaoContratos.Reports
{
	public class ImpressoContrato : Relatorio
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private CalculatedField toDescricao;

		private XRCrossBandLine xrCrossBandLine1;

		private XRCrossBandLine xrCrossBandLine2;

		private XRCrossBandLine xrCrossBandLine3;

		private CalculatedField calculatedField1;

		private CalculatedField calculatedField2;

		private PageFooterBand PageFooter;

		private XRLabel lblMensagemRodape;

		private XRLabel xrLabel15;

		private XRLabel xrLabel18;

		private XRLabel xrLabel20;

		private XRLabel xrLabel22;

		private XRLabel tituloContratado;

		private XRLabel contratado;

		private XRLabel tituloCpfCnpj;

		private XRLabel cpfCnpj;

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel tituloEndereco;

		private XRLabel endereco;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel tituloTelefone;

		private XRLabel telefone;

		private XRLabel dadosContrato;

		private XRLabel xrLabel24;

		private XRLabel xrLabel10;

		private XRLabel dataEmissao;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel7;

		private XRLabel xrLabel58;

		private XRLabel xrLabel19;

		private XRLabel xrLabel23;

		private XRLabel xrLabel27;

		private XRLabel xrLabel26;

		private XRLabel xrLabel25;

		private XRLabel xrLabel17;

		private DetailReportBand pagamento;

		private DetailBand Detail1;

		private GroupHeaderBand GroupHeader1;

		private XRLabel xrLabel81;

		private XRLabel xrLabel85;

		private XRLabel xrLabel84;

		private XRLabel xrLabel88;

		private XRLabel xrLabel91;

		private XRLabel xrLabel92;

		private GroupHeaderBand GroupHeader2;

		private XRLabel numeroContrato;

		private XRPictureBox logotipoEmitente;

		private XRLabel xrLabel14;

		public override string Descricao => "Contrato de locação";

		public override bool ExibirNoModuloRelatorios => false;

		public RelatorioContrato Contrato { get; set; }

		public ImpressoContrato(GestaoContratoDto contratoDto, string conteudoContrato)
		{
			Contrato = RelatorioContrato.Criar().ComContrato(contratoDto).ComEmpresa(DadosSistema.EmpresaLogada)
				.ComConteudoContrato(conteudoContrato);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XRControl)numeroContrato).Text = $"Contrato Nº: {Contrato.Contrato.NumeroContrato}";
			((XtraReportBase)this).DataSource = new List<RelatorioContrato> { Contrato };
			ExibirLogotipo();
			((XRControl)dataEmissao).Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
			((XRControl)pagamento).Visible = Contrato.Contrato.ImprimirPagamentoContrato;
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			if (HelperObject.IsNotNull((object)DadosSistema.EmpresaLogada.Imagem) && DadosSistema.EmpresaLogada.Imagem.Length > 10)
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(Contrato.Empresa.Imagem));
				((XRControl)tituloContratado).Location = new System.Drawing.Point(774, 152);
				((XRControl)contratado).Location = new System.Drawing.Point(999, 152);
				((XRControl)contratado).Size = new System.Drawing.Size(854, 50);
				((XRControl)tituloCpfCnpj).Location = new System.Drawing.Point(774, 202);
				((XRControl)cpfCnpj).Location = new System.Drawing.Point(999, 202);
				((XRControl)cpfCnpj).Size = new System.Drawing.Size(574, 50);
				((XRControl)tituloTelefone).Location = new System.Drawing.Point(774, 252);
				((XRControl)telefone).Location = new System.Drawing.Point(999, 252);
				((XRControl)telefone).Size = new System.Drawing.Size(854, 51);
				((XRControl)tituloEndereco).Location = new System.Drawing.Point(774, 302);
				((XRControl)endereco).Location = new System.Drawing.Point(999, 302);
				((XRControl)endereco).Size = new System.Drawing.Size(854, 51);
			}
			((XRControl)logotipoEmitente).Visible = !HelperObject.IsNull((object)DadosSistema.EmpresaLogada.Imagem);
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
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Expected O, but got Unknown
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Expected O, but got Unknown
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Expected O, but got Unknown
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Expected O, but got Unknown
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Expected O, but got Unknown
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Expected O, but got Unknown
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Expected O, but got Unknown
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Expected O, but got Unknown
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0197: Expected O, but got Unknown
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a2: Expected O, but got Unknown
			//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Expected O, but got Unknown
			//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Expected O, but got Unknown
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c3: Expected O, but got Unknown
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ce: Expected O, but got Unknown
			//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Expected O, but got Unknown
			//IL_01da: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e4: Expected O, but got Unknown
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Expected O, but got Unknown
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fa: Expected O, but got Unknown
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Expected O, but got Unknown
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_0210: Expected O, but got Unknown
			//IL_0211: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Expected O, but got Unknown
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0226: Expected O, but got Unknown
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			//IL_0231: Expected O, but got Unknown
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_023c: Expected O, but got Unknown
			//IL_023d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0247: Expected O, but got Unknown
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Expected O, but got Unknown
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Expected O, but got Unknown
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0268: Expected O, but got Unknown
			//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0456: Unknown result type (might be due to invalid IL or missing references)
			//IL_047f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0513: Unknown result type (might be due to invalid IL or missing references)
			//IL_0519: Expected O, but got Unknown
			//IL_0559: Unknown result type (might be due to invalid IL or missing references)
			//IL_0582: Unknown result type (might be due to invalid IL or missing references)
			//IL_0621: Unknown result type (might be due to invalid IL or missing references)
			//IL_0627: Expected O, but got Unknown
			//IL_0667: Unknown result type (might be due to invalid IL or missing references)
			//IL_0690: Unknown result type (might be due to invalid IL or missing references)
			//IL_0747: Unknown result type (might be due to invalid IL or missing references)
			//IL_0771: Unknown result type (might be due to invalid IL or missing references)
			//IL_083c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0866: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_095b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a10: Expected O, but got Unknown
			//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bfc: Expected O, but got Unknown
			//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c65: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dfa: Expected O, but got Unknown
			//IL_0e3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e63: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fe6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fec: Expected O, but got Unknown
			//IL_102c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1055: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1128: Unknown result type (might be due to invalid IL or missing references)
			//IL_11c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_11cc: Expected O, but got Unknown
			//IL_120c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1235: Unknown result type (might be due to invalid IL or missing references)
			//IL_12eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1314: Unknown result type (might be due to invalid IL or missing references)
			//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13b8: Expected O, but got Unknown
			//IL_13f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1421: Unknown result type (might be due to invalid IL or missing references)
			//IL_14cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_14f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b8: Expected O, but got Unknown
			//IL_15f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1621: Unknown result type (might be due to invalid IL or missing references)
			//IL_16cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_17aa: Expected O, but got Unknown
			//IL_17ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1813: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_19b4: Expected O, but got Unknown
			//IL_19f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1aab: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ab1: Expected O, but got Unknown
			//IL_1af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b25: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cdd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1da9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dd2: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e6f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ea4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f05: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f95: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fea: Unknown result type (might be due to invalid IL or missing references)
			//IL_2061: Unknown result type (might be due to invalid IL or missing references)
			//IL_207b: Unknown result type (might be due to invalid IL or missing references)
			//IL_20b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2101: Unknown result type (might be due to invalid IL or missing references)
			//IL_211b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2156: Unknown result type (might be due to invalid IL or missing references)
			//IL_21b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_21d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_220e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2375: Unknown result type (might be due to invalid IL or missing references)
			//IL_23aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_24ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_24f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_25b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_25da: Unknown result type (might be due to invalid IL or missing references)
			//IL_2684: Unknown result type (might be due to invalid IL or missing references)
			//IL_268a: Expected O, but got Unknown
			//IL_26c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_26f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2780: Unknown result type (might be due to invalid IL or missing references)
			//IL_2786: Expected O, but got Unknown
			//IL_27c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_27ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_2943: Unknown result type (might be due to invalid IL or missing references)
			//IL_2949: Expected O, but got Unknown
			//IL_2988: Unknown result type (might be due to invalid IL or missing references)
			//IL_29b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a52: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a58: Expected O, but got Unknown
			//IL_2a97: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ac0: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b55: Expected O, but got Unknown
			//IL_2b94: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cec: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d16: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e04: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f45: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			numeroContrato = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel22 = new XRLabel();
			tituloContratado = new XRLabel();
			contratado = new XRLabel();
			tituloCpfCnpj = new XRLabel();
			cpfCnpj = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel3 = new XRLabel();
			xrLabel4 = new XRLabel();
			tituloEndereco = new XRLabel();
			endereco = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel9 = new XRLabel();
			tituloTelefone = new XRLabel();
			telefone = new XRLabel();
			dadosContrato = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel10 = new XRLabel();
			dataEmissao = new XRLabel();
			xrLabel14 = new XRLabel();
			logotipoEmitente = new XRPictureBox();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			toDescricao = new CalculatedField();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			xrCrossBandLine3 = new XRCrossBandLine();
			GroupHeader2 = new GroupHeaderBand();
			calculatedField1 = new CalculatedField();
			calculatedField2 = new CalculatedField();
			PageFooter = new PageFooterBand();
			lblMensagemRodape = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel7 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel23 = new XRLabel();
			pagamento = new DetailReportBand();
			Detail1 = new DetailBand();
			xrLabel91 = new XRLabel();
			xrLabel92 = new XRLabel();
			xrLabel88 = new XRLabel();
			GroupHeader1 = new GroupHeaderBand();
			xrLabel81 = new XRLabel();
			xrLabel85 = new XRLabel();
			xrLabel84 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[29]
			{
				(XRControl)numeroContrato,
				(XRControl)xrLabel27,
				(XRControl)xrLabel26,
				(XRControl)xrLabel25,
				(XRControl)xrLabel17,
				(XRControl)xrLabel15,
				(XRControl)xrLabel18,
				(XRControl)xrLabel20,
				(XRControl)xrLabel22,
				(XRControl)tituloContratado,
				(XRControl)contratado,
				(XRControl)tituloCpfCnpj,
				(XRControl)cpfCnpj,
				(XRControl)xrLabel1,
				(XRControl)xrLabel2,
				(XRControl)xrLabel3,
				(XRControl)xrLabel4,
				(XRControl)tituloEndereco,
				(XRControl)endereco,
				(XRControl)xrLabel8,
				(XRControl)xrLabel9,
				(XRControl)tituloTelefone,
				(XRControl)telefone,
				(XRControl)dadosContrato,
				(XRControl)xrLabel24,
				(XRControl)xrLabel10,
				(XRControl)dataEmissao,
				(XRControl)xrLabel14,
				(XRControl)logotipoEmitente
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 755.7194f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)numeroContrato).Borders = (BorderSide)0;
			((XRControl)numeroContrato).Dpi = 254f;
			((XRControl)numeroContrato).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)numeroContrato).LocationFloat = new PointFloat(51.5f, 94.01633f);
			((XRControl)numeroContrato).Name = "numeroContrato";
			((XRControl)numeroContrato).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)numeroContrato).SizeF = new SizeF(1900f, 58.50007f);
			((XRControl)numeroContrato).StylePriority.UseBorders = false;
			((XRControl)numeroContrato).StylePriority.UseFont = false;
			((XRControl)numeroContrato).StylePriority.UseTextAlignment = false;
			((XRControl)numeroContrato).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel27).Borders = (BorderSide)12;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.DataFinal", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(348.668f, 608.2662f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(1505.581f, 50.00006f);
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).WordWrap = false;
			((XRControl)xrLabel26).Borders = (BorderSide)4;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.DataInicial", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(348.6679f, 558.2662f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(1505.58f, 50.00003f);
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel26).WordWrap = false;
			((XRControl)xrLabel25).Borders = (BorderSide)9;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(155.1852f, 608.2664f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(193.4828f, 50.00006f);
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UsePadding = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Data final:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel17).Borders = (BorderSide)1;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(155.1878f, 558.2664f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(193.4802f, 50f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Data inicial:";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel15).Borders = (BorderSide)3;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(155.1875f, 408.2661f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(225.2534f, 50.00003f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "Contratante:";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel18).Borders = (BorderSide)2;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Pessoa.Nome")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(380.4408f, 408.2661f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(674.7686f, 50.00012f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel18).WordWrap = false;
			((XRControl)xrLabel20).Borders = (BorderSide)2;
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1055.21f, 408.2661f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(166.616f, 50f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).Text = "CPF/CNPJ:";
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel22).Borders = (BorderSide)6;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Pessoa.CpfCnpjFormatado")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1221.826f, 408.2659f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(632.4221f, 50.00003f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)16;
			((XRControl)tituloContratado).Borders = (BorderSide)3;
			((XRControl)tituloContratado).Dpi = 254f;
			((XRControl)tituloContratado).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloContratado).LocationFloat = new PointFloat(155.1878f, 152.5164f);
			((XRControl)tituloContratado).Name = "tituloContratado";
			((XRControl)tituloContratado).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloContratado).SizeF = new SizeF(225.2534f, 50.00002f);
			((XRControl)tituloContratado).StylePriority.UseBorders = false;
			((XRControl)tituloContratado).StylePriority.UseFont = false;
			((XRControl)tituloContratado).StylePriority.UsePadding = false;
			((XRControl)tituloContratado).StylePriority.UseTextAlignment = false;
			((XRControl)tituloContratado).Text = "Contratado:";
			((XRControl)tituloContratado).TextAlignment = (TextAlignment)16;
			((XRControl)contratado).Borders = (BorderSide)6;
			((XRControl)contratado).CanGrow = false;
			((XRControl)contratado).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.Nome")
			});
			((XRControl)contratado).Dpi = 254f;
			((XRControl)contratado).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)contratado).LocationFloat = new PointFloat(380.4409f, 152.5164f);
			((XRControl)contratado).Name = "contratado";
			((XRControl)contratado).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)contratado).SizeF = new SizeF(1473.81f, 50.00002f);
			((XRControl)contratado).StylePriority.UseBorders = false;
			((XRControl)contratado).StylePriority.UseFont = false;
			((XRControl)contratado).StylePriority.UseTextAlignment = false;
			((XRControl)contratado).TextAlignment = (TextAlignment)16;
			((XRControl)tituloCpfCnpj).Borders = (BorderSide)1;
			((XRControl)tituloCpfCnpj).Dpi = 254f;
			((XRControl)tituloCpfCnpj).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloCpfCnpj).LocationFloat = new PointFloat(155.1878f, 202.5164f);
			((XRControl)tituloCpfCnpj).Name = "tituloCpfCnpj";
			((XRControl)tituloCpfCnpj).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloCpfCnpj).SizeF = new SizeF(225.2534f, 50f);
			((XRControl)tituloCpfCnpj).StylePriority.UseBorders = false;
			((XRControl)tituloCpfCnpj).StylePriority.UseFont = false;
			((XRControl)tituloCpfCnpj).StylePriority.UsePadding = false;
			((XRControl)tituloCpfCnpj).StylePriority.UseTextAlignment = false;
			((XRControl)tituloCpfCnpj).Text = "CPF/CNPJ:";
			((XRControl)tituloCpfCnpj).TextAlignment = (TextAlignment)16;
			((XRControl)cpfCnpj).Borders = (BorderSide)0;
			((XRControl)cpfCnpj).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.CpfCnpjFormatado")
			});
			((XRControl)cpfCnpj).Dpi = 254f;
			((XRControl)cpfCnpj).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)cpfCnpj).LocationFloat = new PointFloat(380.4408f, 202.5164f);
			((XRControl)cpfCnpj).Name = "cpfCnpj";
			((XRControl)cpfCnpj).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)cpfCnpj).SizeF = new SizeF(1193.656f, 50f);
			((XRControl)cpfCnpj).StylePriority.UseBorders = false;
			((XRControl)cpfCnpj).StylePriority.UseFont = false;
			((XRControl)cpfCnpj).StylePriority.UseTextAlignment = false;
			((XRControl)cpfCnpj).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(675.1743f, 458.2659f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(163.9221f, 50.00006f);
			((XRControl)xrLabel1).StylePriority.UseBorders = false;
			((XRControl)xrLabel1).StylePriority.UseFont = false;
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "Endereço:";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel2).Borders = (BorderSide)4;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Pessoa.EnderecoPrincipal.EncurtadoToText")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(839.0963f, 458.2661f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1015.153f, 50.00006f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).WordWrap = false;
			((XRControl)xrLabel3).Borders = (BorderSide)0;
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1574.096f, 202.5164f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(67.60168f, 50f);
			((XRControl)xrLabel3).StylePriority.UseBorders = false;
			((XRControl)xrLabel3).StylePriority.UseFont = false;
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "IE:";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel4).Borders = (BorderSide)4;
			((XRControl)xrLabel4).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.Ie")
			});
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1641.698f, 202.5164f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(212.5531f, 50f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)tituloEndereco).Borders = (BorderSide)9;
			((XRControl)tituloEndereco).Dpi = 254f;
			((XRControl)tituloEndereco).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloEndereco).LocationFloat = new PointFloat(155.1878f, 302.5164f);
			((XRControl)tituloEndereco).Name = "tituloEndereco";
			((XRControl)tituloEndereco).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloEndereco).SizeF = new SizeF(225.2534f, 51.00003f);
			((XRControl)tituloEndereco).StylePriority.UseBorders = false;
			((XRControl)tituloEndereco).StylePriority.UseFont = false;
			((XRControl)tituloEndereco).StylePriority.UsePadding = false;
			((XRControl)tituloEndereco).StylePriority.UseTextAlignment = false;
			((XRControl)tituloEndereco).Text = "Endereço:";
			((XRControl)tituloEndereco).TextAlignment = (TextAlignment)16;
			((XRControl)endereco).Borders = (BorderSide)12;
			((XRControl)endereco).CanGrow = false;
			((XRControl)endereco).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.EnderecoToText")
			});
			((XRControl)endereco).Dpi = 254f;
			((XRControl)endereco).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)endereco).LocationFloat = new PointFloat(380.4413f, 302.5164f);
			((XRControl)endereco).Name = "endereco";
			((XRControl)endereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)endereco).SizeF = new SizeF(1473.808f, 51.00003f);
			((XRControl)endereco).StylePriority.UseBorders = false;
			((XRControl)endereco).StylePriority.UseFont = false;
			((XRControl)endereco).StylePriority.UseTextAlignment = false;
			((XRControl)endereco).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).Borders = (BorderSide)1;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(155.1875f, 458.2661f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(182.9201f, 50.00006f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UsePadding = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Telefone:";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel9).Borders = (BorderSide)0;
			((XRControl)xrLabel9).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Pessoa.TelefonePrincipal.ToText")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(338.1076f, 458.2659f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(337.0666f, 50.00012f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel9).WordWrap = false;
			((XRControl)tituloTelefone).Borders = (BorderSide)1;
			((XRControl)tituloTelefone).Dpi = 254f;
			((XRControl)tituloTelefone).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloTelefone).LocationFloat = new PointFloat(155.1878f, 252.5164f);
			((XRControl)tituloTelefone).Name = "tituloTelefone";
			((XRControl)tituloTelefone).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloTelefone).SizeF = new SizeF(225.2531f, 50.00002f);
			((XRControl)tituloTelefone).StylePriority.UseBorders = false;
			((XRControl)tituloTelefone).StylePriority.UseFont = false;
			((XRControl)tituloTelefone).StylePriority.UsePadding = false;
			((XRControl)tituloTelefone).StylePriority.UseTextAlignment = false;
			((XRControl)tituloTelefone).Text = "Telefone:";
			((XRControl)tituloTelefone).TextAlignment = (TextAlignment)16;
			((XRControl)telefone).Borders = (BorderSide)4;
			((XRControl)telefone).CanGrow = false;
			((XRControl)telefone).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.Telefone")
			});
			((XRControl)telefone).Dpi = 254f;
			((XRControl)telefone).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)telefone).LocationFloat = new PointFloat(380.4413f, 252.5164f);
			((XRControl)telefone).Name = "telefone";
			((XRControl)telefone).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)telefone).SizeF = new SizeF(1473.808f, 50.00002f);
			((XRControl)telefone).StylePriority.UseBorders = false;
			((XRControl)telefone).StylePriority.UseFont = false;
			((XRControl)telefone).StylePriority.UseTextAlignment = false;
			((XRControl)telefone).TextAlignment = (TextAlignment)16;
			((XRControl)dadosContrato).Borders = (BorderSide)0;
			((XRControl)dadosContrato).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "ConteudoContrato")
			});
			((XRControl)dadosContrato).Dpi = 254f;
			((XRControl)dadosContrato).Font = new Font("Segoe UI", 9f);
			((XRControl)dadosContrato).LocationFloat = new PointFloat(155.1852f, 705.7194f);
			dadosContrato.Multiline = true;
			((XRControl)dadosContrato).Name = "dadosContrato";
			((XRControl)dadosContrato).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)dadosContrato).SizeF = new SizeF(1699.063f, 50f);
			((XRControl)dadosContrato).StylePriority.UseBorders = false;
			((XRControl)dadosContrato).StylePriority.UseFont = false;
			((XRControl)dadosContrato).StylePriority.UseTextAlignment = false;
			((XRControl)dadosContrato).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel24).Borders = (BorderSide)0;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(51.50001f, 30.78716f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(1900f, 63.22917f);
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "TERMOS DO CONTRATO";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel10).Borders = (BorderSide)1;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(155.187f, 508.2662f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(261.1066f, 50.00003f);
			((XRControl)xrLabel10).StylePriority.UseBorders = false;
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).StylePriority.UsePadding = false;
			((XRControl)xrLabel10).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel10).Text = "Data de emissão:";
			((XRControl)xrLabel10).TextAlignment = (TextAlignment)16;
			((XRControl)dataEmissao).Borders = (BorderSide)4;
			((XRControl)dataEmissao).Dpi = 254f;
			((XRControl)dataEmissao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)dataEmissao).LocationFloat = new PointFloat(416.2937f, 508.2662f);
			((XRControl)dataEmissao).Name = "dataEmissao";
			((XRControl)dataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)dataEmissao).SizeF = new SizeF(1437.957f, 50.00009f);
			((XRControl)dataEmissao).StylePriority.UseBorders = false;
			((XRControl)dataEmissao).StylePriority.UseFont = false;
			((XRControl)dataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)dataEmissao).TextAlignment = (TextAlignment)16;
			((XRControl)dataEmissao).WordWrap = false;
			((XRControl)xrLabel14).Borders = (BorderSide)11;
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).LocationFloat = new PointFloat(155.1878f, 152.5164f);
			xrLabel14.Multiline = true;
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(619.1245f, 201.0001f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)logotipoEmitente).Borders = (BorderSide)0;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(175.7807f, 166.0997f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(580.0108f, 171.8959f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 46f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 63f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((DataContainerComponent)toDescricao).DataMember = "Itens";
			toDescricao.Expression = "Iif([Cancelado], Concat('(Cancelado)', [ProdutoServico.Descricao]), [ProdutoServico.Descricao])";
			toDescricao.Name = "toDescricao";
			((XRControl)xrCrossBandLine1).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine1).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine1).EndPointFloat = new PointFloat(2000f, 0f);
			((XRControl)xrCrossBandLine1).LocationFloat = new PointFloat(2000f, 0f);
			((XRControl)xrCrossBandLine1).Name = "xrCrossBandLine1";
			((XRCrossBandControl)xrCrossBandLine1).StartBand = (Band)(object)Detail;
			((XRCrossBandControl)xrCrossBandLine1).StartPointFloat = new PointFloat(2000f, 0f);
			((XRControl)xrCrossBandLine1).WidthF = 3f;
			((XRControl)xrCrossBandLine2).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine2).EndBand = (Band)(object)BottomMargin;
			((XRCrossBandControl)xrCrossBandLine2).EndPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine2).Name = "xrCrossBandLine2";
			((XRCrossBandControl)xrCrossBandLine2).StartBand = (Band)(object)Detail;
			((XRCrossBandControl)xrCrossBandLine2).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine2).WidthF = 3f;
			((XRControl)xrCrossBandLine3).AnchorHorizontal = (HorizontalAnchorStyles)1;
			((XRControl)xrCrossBandLine3).AnchorVertical = (VerticalAnchorStyles)0;
			((XRControl)xrCrossBandLine3).Dpi = 254f;
			((XRCrossBandControl)xrCrossBandLine3).EndBand = (Band)(object)GroupHeader2;
			((XRCrossBandControl)xrCrossBandLine3).EndPointFloat = new PointFloat(0f, 1.999985f);
			((XRControl)xrCrossBandLine3).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine3).Name = "xrCrossBandLine3";
			((XRCrossBandControl)xrCrossBandLine3).StartBand = (Band)(object)GroupHeader2;
			((XRCrossBandControl)xrCrossBandLine3).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine3).WidthF = 2002.968f;
			((XRControl)GroupHeader2).Dpi = 254f;
			((XRControl)GroupHeader2).HeightF = 2f;
			((XRControl)GroupHeader2).Name = "GroupHeader2";
			((GroupBand)GroupHeader2).RepeatEveryPage = true;
			((DataContainerComponent)calculatedField1).DataMember = "Hospedagem.Itens";
			calculatedField1.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField1.Name = "calculatedField1";
			((DataContainerComponent)calculatedField2).DataMember = "Hospedagem.Servicos";
			calculatedField2.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField2.Name = "calculatedField2";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblMensagemRodape });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 5.291667f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)lblMensagemRodape).Borders = (BorderSide)8;
			((XRControl)lblMensagemRodape).BorderWidth = 1f;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Times New Roman", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(2.999999f, 0f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1997.483f, 5.291667f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseBorderWidth = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[4]
			{
				(XRControl)xrLabel7,
				(XRControl)xrLabel58,
				(XRControl)xrLabel19,
				(XRControl)xrLabel23
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 293.2344f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)xrLabel7).Borders = (BorderSide)2;
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(151.9682f, 138.9505f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(645.5833f, 50.00003f);
			((XRControl)xrLabel7).StylePriority.UseBorders = false;
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel7).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel7).Text = "Assinatura";
			((XRControl)xrLabel7).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel58).Borders = (BorderSide)2;
			((XRControl)xrLabel58).Dpi = 254f;
			((XRControl)xrLabel58).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel58).LocationFloat = new PointFloat(1205.446f, 138.9505f);
			((XRControl)xrLabel58).Name = "xrLabel58";
			((XRControl)xrLabel58).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel58).SizeF = new SizeF(645.5833f, 50.00003f);
			((XRControl)xrLabel58).StylePriority.UseBorders = false;
			((XRControl)xrLabel58).StylePriority.UseFont = false;
			((XRControl)xrLabel58).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel58).Text = "Assinatura";
			((XRControl)xrLabel58).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel19).Borders = (BorderSide)0;
			((XRControl)xrLabel19).CanGrow = false;
			((XRControl)xrLabel19).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.Nome")
			});
			((XRControl)xrLabel19).Dpi = 254f;
			((XRControl)xrLabel19).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel19).LocationFloat = new PointFloat(151.969f, 188.9505f);
			((XRControl)xrLabel19).Name = "xrLabel19";
			((XRControl)xrLabel19).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel19).SizeF = new SizeF(645.5825f, 50f);
			((XRControl)xrLabel19).StylePriority.UseBorders = false;
			((XRControl)xrLabel19).StylePriority.UseFont = false;
			((XRControl)xrLabel19).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel19).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel23).Borders = (BorderSide)0;
			((XRControl)xrLabel23).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Pessoa.Nome")
			});
			((XRControl)xrLabel23).Dpi = 254f;
			((XRControl)xrLabel23).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel23).LocationFloat = new PointFloat(1205.446f, 188.9505f);
			((XRControl)xrLabel23).Name = "xrLabel23";
			((XRControl)xrLabel23).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel23).SizeF = new SizeF(645.5856f, 50.00011f);
			((XRControl)xrLabel23).StylePriority.UseBorders = false;
			((XRControl)xrLabel23).StylePriority.UseFont = false;
			((XRControl)xrLabel23).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel23).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel23).WordWrap = false;
			((XtraReportBase)pagamento).Bands.AddRange((Band[])(object)new Band[2]
			{
				(Band)Detail1,
				(Band)GroupHeader1
			});
			((XRControl)pagamento).Dpi = 254f;
			pagamento.Level = 0;
			((XRControl)pagamento).Name = "pagamento";
			((XRControl)Detail1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel91,
				(XRControl)xrLabel92,
				(XRControl)xrLabel88
			});
			((XRControl)Detail1).Dpi = 254f;
			((XRControl)Detail1).HeightF = 58.02429f;
			((XRControl)Detail1).Name = "Detail1";
			((XRControl)xrLabel91).Borders = (BorderSide)9;
			((XRControl)xrLabel91).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.Valor", "{0:n2}")
			});
			((XRControl)xrLabel91).Dpi = 254f;
			((XRControl)xrLabel91).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel91).LocationFloat = new PointFloat(525.9388f, 0f);
			((XRControl)xrLabel91).Name = "xrLabel91";
			((XRControl)xrLabel91).Padding = new PaddingInfo(5, 20, 0, 0, 254f);
			((XRControl)xrLabel91).SizeF = new SizeF(421.7196f, 58.02429f);
			((XRControl)xrLabel91).StylePriority.UseBorders = false;
			((XRControl)xrLabel91).StylePriority.UseFont = false;
			((XRControl)xrLabel91).StylePriority.UsePadding = false;
			((XRControl)xrLabel91).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel91).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel92).Borders = (BorderSide)9;
			((XRControl)xrLabel92).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.DiaVencimentoFinanceiro")
			});
			((XRControl)xrLabel92).Dpi = 254f;
			((XRControl)xrLabel92).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel92).LocationFloat = new PointFloat(155.1852f, 0f);
			((XRControl)xrLabel92).Name = "xrLabel92";
			((XRControl)xrLabel92).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel92).SizeF = new SizeF(370.751f, 58.02429f);
			((XRControl)xrLabel92).StylePriority.UseBorders = false;
			((XRControl)xrLabel92).StylePriority.UseFont = false;
			((XRControl)xrLabel92).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel92).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel88).Borders = (BorderSide)13;
			((XRControl)xrLabel88).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Contrato.EspeciePagamento.Descricao")
			});
			((XRControl)xrLabel88).Dpi = 254f;
			((XRControl)xrLabel88).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel88).LocationFloat = new PointFloat(947.6584f, 0f);
			((XRControl)xrLabel88).Name = "xrLabel88";
			((XRControl)xrLabel88).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel88).SizeF = new SizeF(903.3729f, 58.02429f);
			((XRControl)xrLabel88).StylePriority.UseBorders = false;
			((XRControl)xrLabel88).StylePriority.UseFont = false;
			((XRControl)xrLabel88).StylePriority.UsePadding = false;
			((XRControl)xrLabel88).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel88).TextAlignment = (TextAlignment)32;
			((XRControl)GroupHeader1).Controls.AddRange((XRControl[])(object)new XRControl[3]
			{
				(XRControl)xrLabel81,
				(XRControl)xrLabel85,
				(XRControl)xrLabel84
			});
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 110.8542f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((XRControl)xrLabel81).BackColor = Color.Transparent;
			((XRControl)xrLabel81).Borders = (BorderSide)15;
			((XRControl)xrLabel81).Dpi = 254f;
			((XRControl)xrLabel81).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel81).LocationFloat = new PointFloat(947.6586f, 60.85419f);
			((XRControl)xrLabel81).Name = "xrLabel81";
			((XRControl)xrLabel81).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel81).SizeF = new SizeF(903.3731f, 50f);
			((XRControl)xrLabel81).StylePriority.UseBackColor = false;
			((XRControl)xrLabel81).StylePriority.UseBorders = false;
			((XRControl)xrLabel81).StylePriority.UseFont = false;
			((XRControl)xrLabel81).StylePriority.UsePadding = false;
			((XRControl)xrLabel81).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel81).Text = "Espécie";
			((XRControl)xrLabel81).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel85).BackColor = Color.Transparent;
			((XRControl)xrLabel85).Borders = (BorderSide)11;
			((XRControl)xrLabel85).Dpi = 254f;
			((XRControl)xrLabel85).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel85).LocationFloat = new PointFloat(525.9388f, 60.85419f);
			((XRControl)xrLabel85).Name = "xrLabel85";
			((XRControl)xrLabel85).Padding = new PaddingInfo(5, 20, 0, 0, 254f);
			((XRControl)xrLabel85).SizeF = new SizeF(421.7197f, 50f);
			((XRControl)xrLabel85).StylePriority.UseBackColor = false;
			((XRControl)xrLabel85).StylePriority.UseBorders = false;
			((XRControl)xrLabel85).StylePriority.UseFont = false;
			((XRControl)xrLabel85).StylePriority.UsePadding = false;
			((XRControl)xrLabel85).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel85).Text = "Valor";
			((XRControl)xrLabel85).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel84).BackColor = Color.Transparent;
			((XRControl)xrLabel84).Borders = (BorderSide)11;
			((XRControl)xrLabel84).Dpi = 254f;
			((XRControl)xrLabel84).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel84).LocationFloat = new PointFloat(155.1878f, 60.85419f);
			((XRControl)xrLabel84).Name = "xrLabel84";
			((XRControl)xrLabel84).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel84).SizeF = new SizeF(370.751f, 50f);
			((XRControl)xrLabel84).StylePriority.UseBackColor = false;
			((XRControl)xrLabel84).StylePriority.UseBorders = false;
			((XRControl)xrLabel84).StylePriority.UseFont = false;
			((XRControl)xrLabel84).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel84).Text = "Dia do vencimento";
			((XRControl)xrLabel84).TextAlignment = (TextAlignment)32;
			bindingSource1.DataSource = typeof(RelatorioContrato);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[7]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageFooter,
				(Band)ReportFooter,
				(Band)pagamento,
				(Band)GroupHeader2
			});
			((XRControl)this).Borders = (BorderSide)8;
			((XtraReport)this).CalculatedFields.AddRange((CalculatedField[])(object)new CalculatedField[3] { toDescricao, calculatedField1, calculatedField2 });
			((XtraReport)this).CrossBandControls.AddRange((XRCrossBandControl[])(object)new XRCrossBandControl[3]
			{
				(XRCrossBandControl)xrCrossBandLine3,
				(XRCrossBandControl)xrCrossBandLine2,
				(XRCrossBandControl)xrCrossBandLine1
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(47, 50, 46, 63);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class RelatorioContrato
	{
		public Pessoa Empresa { get; set; }

		public GestaoContratoDto Contrato { get; set; }

		public string ConteudoContrato { get; set; }

		public RelatorioContrato(Pessoa empresa, GestaoContratoDto contrato, string conteudoContrato)
		{
			Empresa = empresa;
			Contrato = contrato;
			ConteudoContrato = conteudoContrato;
		}

		public RelatorioContrato()
		{
		}

		public static RelatorioContrato Criar()
		{
			return new RelatorioContrato();
		}

		public RelatorioContrato ComEmpresa(Pessoa empresa)
		{
			Empresa = empresa;
			return Build();
		}

		public RelatorioContrato ComContrato(GestaoContratoDto contrato)
		{
			Contrato = contrato;
			return Build();
		}

		public RelatorioContrato ComConteudoContrato(string conteudo)
		{
			ConteudoContrato = conteudo;
			return Build();
		}

		protected RelatorioContrato Build()
		{
			return new RelatorioContrato(Empresa, Contrato, ConteudoContrato);
		}
	}
}
namespace Otus.Modulo.GestaoContratos.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GestaoContratos.Properties.Resources", typeof(Resources).Assembly);
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
namespace Otus.Modulo.GestaoContratos.Function
{
	public class CampoContrato
	{
		private readonly Func<GestaoContratoDto, string> _obterValor;

		public string Descricao { get; }

		public string Chave { get; }

		public string TextoCopia { get; }

		public CampoContrato(string descricao, string chave, Func<GestaoContratoDto, string> obterValor, string textoCopia = null)
		{
			Descricao = descricao;
			Chave = chave;
			_obterValor = obterValor;
			TextoCopia = textoCopia ?? chave;
		}

		public string ObterValor(GestaoContratoDto contrato)
		{
			return HelperString.ToStringEmptyWhenNull(_obterValor(contrato));
		}
	}
	public static class CamposContrato
	{
		public static IReadOnlyList<CampoContrato> Campos { get; } = new List<CampoContrato>
		{
			new CampoContrato("Nome do cliente", "[NOME_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.Nome),
			new CampoContrato("CPF/CNPJ do cliente", "[CPF_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.CpfCnpjFormatado),
			new CampoContrato("Endereço do cliente", "[ENDERECO_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.EnderecoPrincipal.ToText),
			new CampoContrato("Telefone do cliente", "[TELEFONE_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.TelefoneToText),
			new CampoContrato("Município do cliente", "[MUNICIPIO_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.MunicipioNome),
			new CampoContrato("UF do cliente", "[UF_CLIENTE]", (GestaoContratoDto c) => c.Pessoa.UfSigla),
			new CampoContrato("Número do contrato", "[NUMERO_CONTRATO]", (GestaoContratoDto c) => c.NumeroContrato.ToString()),
			new CampoContrato("Data de início", "[DATA_INICIO]", (GestaoContratoDto c) => c.DataInicial.ToString("dd/MM/yyyy")),
			new CampoContrato("Data final", "[DATA_FIM]", (GestaoContratoDto c) => c.DataFinal.ToString("dd/MM/yyyy")),
			new CampoContrato("Valor do contrato", "[VALOR_CONTRATO]", (GestaoContratoDto c) => c.Valor.ToString("C")),
			new CampoContrato("Tipo do contrato", "[TIPO_CONTRATO]", (GestaoContratoDto c) => c.TipoContrato.Descricao),
			new CampoContrato("Itens", "[ITENS]", (GestaoContratoDto c) => c.ServicosToText),
			new CampoContrato("Dia de vencimento", "[DIA_VENCIMENTO]", (GestaoContratoDto c) => c.DiaVencimentoFinanceiro.ToString()),
			new CampoContrato("Observação", "[OBSERVACAO]", (GestaoContratoDto c) => c.Observacao),
			new CampoContrato("Nome do emitente", "[EMPRESA_NOME]", delegate
			{
				Pessoa empresaLogada4 = DadosSistema.EmpresaLogada;
				return (empresaLogada4 == null) ? null : empresaLogada4.Nome;
			}),
			new CampoContrato("CPF/CNPJ do emitente", "[EMPRESA_CNPJ]", delegate
			{
				Pessoa empresaLogada3 = DadosSistema.EmpresaLogada;
				return (empresaLogada3 == null) ? null : empresaLogada3.CpfCnpjFormatado;
			}),
			new CampoContrato("Endereço do emitente", "[EMPRESA_ENDERECO]", delegate
			{
				Pessoa empresaLogada2 = DadosSistema.EmpresaLogada;
				return (empresaLogada2 == null) ? null : empresaLogada2.EnderecoToText;
			}),
			new CampoContrato("Telefone do emitente", "[EMPRESA_TELEFONE]", delegate
			{
				Pessoa empresaLogada = DadosSistema.EmpresaLogada;
				return (empresaLogada == null) ? null : empresaLogada.Telefone;
			}),
			new CampoContrato("Data atual", "[DATA_ATUAL]", (GestaoContratoDto c) => DateTime.Now.ToString("dd/MM/yyyy"))
		};


		public static IReadOnlyList<CampoContrato> CamposParaExibir { get; } = MontarCamposParaExibir();


		private static IReadOnlyList<CampoContrato> MontarCamposParaExibir()
		{
			return new List<CampoContrato>(Campos)
			{
				new CampoContrato(textoCopia: string.Join(Environment.NewLine, Campos.Select((CampoContrato campo) => campo.Chave)), descricao: "Todos os campos", chave: "(copiar todos)", obterValor: (GestaoContratoDto c) => string.Empty)
			};
		}

		public static Dictionary<string, string> MontarValores(GestaoContratoDto contrato)
		{
			return Campos.ToDictionary((CampoContrato campo) => campo.Chave, (CampoContrato campo) => campo.ObterValor(contrato));
		}
	}
}

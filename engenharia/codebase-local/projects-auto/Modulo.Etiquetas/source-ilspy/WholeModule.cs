using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraPrinting.Shape;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.AlteracoesPrecoAgendadas;
using DigisatServer.Application.Main.Dto.Auxiliares.Autorizacoes;
using DigisatServer.Application.Main.Dto.Auxiliares.Etiquetas;
using DigisatServer.Application.Main.Dto.Cadastros.Grupos;
using DigisatServer.Application.Main.Dto.Cadastros.Localizacoes;
using DigisatServer.Application.Main.Dto.Cadastros.Marcas;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.Promocoes;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Application.Main.Dto.Cadastros.UnidadesMedida;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Industrias.OrdensProducao;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.EntradasModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Paises;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.File;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.OrigensDadosEtiqueta;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.NetFramework.File;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Etiquetas.ViewModels.EtiquetasImpressao;
using Otus.Modulo.Etiquetas.ViewModels.EtiquetasImpressao.Tipos;
using Otus.Modulo.Etiquetas.ViewModels.Tipos;
using Otus.Modulo.Etiquetas.ViewModels.Tipos.Alinhamentos;
using Otus.Modulo.Etiquetas.ViewModels.Tipos.Campos;
using Otus.Modulo.Etiquetas.Views;
using Otus.Modulo.Etiquetas.Views.EtiquetasImpressao;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Etiquetas")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Etiquetas")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Etiquetas
{
	[Module(ModuleName = "EtiquetaModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ObjetoValorModulo")]
	[ModuleDependency("PromocaoModulo")]
	[ModuleDependency("OrdemProducaoModulo")]
	public class EtiquetaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public EtiquetaModulo(IUnityContainer container, IModuleManager moduleManager)
		{
			_container = container;
			moduleManager.LoadModule("MovimentacaoModulo");
			moduleManager.LoadModule("ProdutoServicoModulo");
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<EtiquetasView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EtiquetaImpressaoFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EtiquetaConfiguracaoListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EtiquetaConfiguracaoFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EtiquetaConfiguracaoFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<BuscaFiltroLancamentoItemRapidoView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Etiquetas.Views
{
	public class EtiquetaConfiguracaoFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public EtiquetaConfiguracaoFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaadicionarview.xaml", UriKind.Relative);
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
	public class EtiquetaConfiguracaoFichaAtualizarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public EtiquetaConfiguracaoFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaatualizarview.xaml", UriKind.Relative);
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
	public class EtiquetaConfiguracaoListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaConfiguracaoListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaolistaview.xaml", UriKind.Relative);
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
	public class EtiquetasView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetasView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasview.xaml", UriKind.Relative);
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
	public class LinkExportar : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkExportar()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/linkexportar.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Etiquetas.Views.EtiquetasImpressao
{
	public class BuscaFiltroLancamentoItemRapidoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public BuscaFiltroLancamentoItemRapidoView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasimpressao/buscafiltrolancamentoitemrapidoview.xaml", UriKind.Relative);
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
	public class EtiquetaImpressaoConfiguracaoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaImpressaoConfiguracaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasimpressao/etiquetaimpressaoconfiguracaoview.xaml", UriKind.Relative);
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
	public class EtiquetaImpressaoFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaImpressaoFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasimpressao/etiquetaimpressaofichaview.xaml", UriKind.Relative);
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
	public class EtiquetaImpressaoItensView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaImpressaoItensView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasimpressao/etiquetaimpressaoitensview.xaml", UriKind.Relative);
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
	public class EtiquetaImpressaoView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterEtiquetaConfiguracao;

		private bool _contentLoaded;

		public EtiquetaImpressaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetasimpressao/etiquetaimpressaoview.xaml", UriKind.Relative);
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
				ParameterEtiquetaConfiguracao = (ButtonInfo)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.Etiquetas.Views.EtiquetaConfiguracaoFichaViewDetalhes
{
	public class CampoFichaManutencaoDetalheView : CustomCrudUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CampoFichaManutencaoDetalheView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/campofichamanutencaodetalheview.xaml", UriKind.Relative);
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
	public class CampoListaManutencaoDetalheView : CustomCrudUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public CampoListaManutencaoDetalheView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/campolistamanutencaodetalheview.xaml", UriKind.Relative);
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
	public class EtiquetaConfiguracaoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaConfiguracaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/etiquetaconfiguracaoview.xaml", UriKind.Relative);
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
	public class EtiquetaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EtiquetaView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/etiquetaview.xaml", UriKind.Relative);
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
	public class PaginaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PaginaView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/paginaview.xaml", UriKind.Relative);
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
	public class PreVisualizacaoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PreVisualizacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Etiquetas;component/views/etiquetaconfiguracaofichaviewdetalhes/previsualizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Etiquetas.ViewModels
{
	public class CampoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public CampoAbaViewModel(IViewModelDtoBase viewModel)
			: base("CAMPOS", "Campos da etiqueta", viewModel)
		{
		}
	}
	public class CampoEtiquetaViewModel : ViewModelDtoBase<CampoEtiquetaDto>
	{
		private readonly IEnumerable<CampoBaseViewModel> _campos;

		public CamposComboBoxViewModel CamposComboBox { get; set; }

		public override bool Valido => ((ViewModelValidationBase)((Lista<CampoBaseViewModel>)CamposComboBox).ItemSelecionado).Valido;

		public CampoEtiquetaViewModel(CampoEtiquetaDto dto, IEnumerable<CampoBaseViewModel> campos = null)
			: base(dto)
		{
			_campos = campos;
		}

		public override void ProjectedAsViewModel()
		{
			CamposComboBox = new CamposComboBoxViewModel(_campos, base.Dto);
			((BindableBase)CamposComboBox).PropertyChanged += CamposComboBoxOnPropertyChanged;
		}

		private void CamposComboBoxOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			CampoEtiquetaDto val = (CampoEtiquetaDto)((obj is CampoEtiquetaDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((Lista<CampoBaseViewModel>)CamposComboBox).ItemSelecionado.AtualizarDto(val);
			}
		}

		public override void Dispose()
		{
			if (!HelperObject.IsNull((object)CamposComboBox))
			{
				((BindableBase)CamposComboBox).PropertyChanged -= CamposComboBoxOnPropertyChanged;
				((ViewModelValidationBase)CamposComboBox).Dispose();
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class CampoFichaManutencaoDetalheViewModel : FichaManutencaoDetalheBase<CampoEtiquetaViewModel>
	{
		public override bool Valido => ((ViewModelValidationBase)((ViewModelDtoBase<CampoEtiquetaViewModel>)(object)this).Dto).Valido;

		public CampoFichaManutencaoDetalheViewModel(CampoEtiquetaViewModel dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			((ViewModelBase<CampoEtiquetaDto>)(object)((ViewModelDtoBase<CampoEtiquetaViewModel>)(object)this).Dto).ProjectedAsViewModel();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			CampoEtiquetaViewModel campoEtiquetaViewModel = dto as CampoEtiquetaViewModel;
			if (!HelperObject.IsNull((object)campoEtiquetaViewModel))
			{
				CampoEtiquetaDto dto2 = ((ViewModelDtoBase<CampoEtiquetaDto>)campoEtiquetaViewModel).Dto;
				((ViewModelDtoBase<CampoEtiquetaDto>)((ViewModelDtoBase<CampoEtiquetaViewModel>)(object)this).Dto).AtualizarDto<CampoEtiquetaDto>(ref dto2);
			}
		}
	}
	public class CampoListaManutencaoDetalheViewModel : ListaDetalheManutencaoBase<CampoEtiquetaViewModel, CampoFichaManutencaoDetalheViewModel>
	{
		public CampoListaManutencaoDetalheViewModel(IEnumerable<CampoEtiquetaViewModel> dtos)
			: base(dtos)
		{
		}
	}
	public class CamposComboBoxViewModel : Lista<CampoBaseViewModel>
	{
		public CamposComboBoxViewModel(IEnumerable<CampoBaseViewModel> itens, CampoEtiquetaDto dto)
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			foreach (CampoBaseViewModel iten in itens)
			{
				CampoBaseViewModel campoBaseViewModel = iten.Criar((CampoEtiquetaDto)((iten.Codigo == dto.CodigoCampo) ? ((object)dto) : ((object)new CampoEtiquetaDto())));
				((BindableBase)campoBaseViewModel).PropertyChanged += NovoItemOnPropertyChanged;
				base.Adicionar(campoBaseViewModel);
			}
			if (dto.CodigoCampo == 0)
			{
				base.PosicionarNoPrimeiro();
				return;
			}
			base.Posicionar((Expression<Func<CampoBaseViewModel, bool>>)((CampoBaseViewModel x) => x.Codigo == dto.CodigoCampo));
		}

		private void NovoItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged();
		}

		public override void Dispose()
		{
			foreach (CampoBaseViewModel iten in base.Itens)
			{
				((BindableBase)iten).PropertyChanged -= NovoItemOnPropertyChanged;
			}
			base.Dispose();
		}
	}
	public class CampoViewModel : ViewModelDetalheBase<CampoEtiquetaViewModel, CampoListaManutencaoDetalheViewModel, CampoFichaManutencaoDetalheViewModel>
	{
		private readonly EtiquetaConfiguracaoViewModel _configuracao;

		public CampoViewModel(List<CampoEtiquetaViewModel> dto, EtiquetaConfiguracaoViewModel configuracao)
			: base(dto)
		{
			_configuracao = configuracao;
			((Lista<OrigemDado>)_configuracao.OrigemComboBox).AlterouItemSelecionadoEvent += OrigemComboBoxOnAlterouItemSelecionadoEvent;
		}

		protected override CampoEtiquetaViewModel CriarDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			CampoEtiquetaViewModel campoEtiquetaViewModel = new CampoEtiquetaViewModel(new CampoEtiquetaDto(), ((Lista<OrigemDado>)_configuracao.OrigemComboBox).ItemSelecionado.Campos.Itens);
			((BindableBase)campoEtiquetaViewModel).PropertyChanged += CampoOnPropertyChanged;
			return campoEtiquetaViewModel;
		}

		private void CampoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged();
		}

		private void OrigemComboBoxOnAlterouItemSelecionadoEvent(object sender, OrigemDado origemDado)
		{
			foreach (CampoFichaManutencaoDetalheViewModel iten in ((Lista<CampoFichaManutencaoDetalheViewModel>)(object)base.ListaDetalheViewModel).Itens)
			{
				((BindableBase)iten).PropertyChanged -= CampoOnPropertyChanged;
			}
			((Lista<CampoFichaManutencaoDetalheViewModel>)(object)base.ListaDetalheViewModel).Limpar();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			EtiquetaConfiguracaoDto val = (EtiquetaConfiguracaoDto)((obj is EtiquetaConfiguracaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Campos = ((ViewModelDtoBase<List<CampoEtiquetaViewModel>>)(object)this).Dto.Select((CampoEtiquetaViewModel x) => ((ViewModelDtoBase<CampoEtiquetaDto>)x).Dto);
				((ViewModelDtoBase<List<CampoEtiquetaViewModel>>)(object)this).AtualizarDto<EtiquetaConfiguracaoDto>(ref val);
			}
		}

		public override void Dispose()
		{
			foreach (CampoFichaManutencaoDetalheViewModel iten in ((Lista<CampoFichaManutencaoDetalheViewModel>)(object)base.ListaDetalheViewModel).Itens)
			{
				((BindableBase)iten).PropertyChanged -= CampoOnPropertyChanged;
			}
			((Lista<OrigemDado>)_configuracao.OrigemComboBox).AlterouItemSelecionadoEvent -= OrigemComboBoxOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EtiquetaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public EtiquetaAbaViewModel(IViewModelDtoBase viewModel)
			: base("MEDIDAS DA ETIQUETA", "Tamanho, posições", viewModel)
		{
		}
	}
	public class EtiquetaConfiguracaoAbaViewModel : AbaPrincipalViewModelBase
	{
		public EtiquetaConfiguracaoAbaViewModel(IViewModelPrincipal viewModel)
			: base("ETIQUETA", "Dados principais", viewModel)
		{
		}
	}
	public class EtiquetaConfiguracaoFichaAdicionarViewModel : FichaBaseAdicionar<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel, IEtiquetaConfiguracaoModuleService>
	{
		public EtiquetaConfiguracaoFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class EtiquetaConfiguracaoFichaAtualizarViewModel : FichaBaseAtualizar<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel, IEtiquetaConfiguracaoModuleService>
	{
		public EtiquetaConfiguracaoFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class EtiquetaConfiguracaoFormulario : FormularioEntity<EtiquetaConfiguracaoDto>
	{
		public EtiquetaConfiguracaoFormulario(EtiquetaConfiguracaoDto dto)
			: base(dto)
		{
			EtiquetaConfiguracaoViewModel configuracao = new EtiquetaConfiguracaoViewModel(base.Dto);
			PaginaViewModel paginaViewModel = new PaginaViewModel(base.Dto.Pagina);
			EtiquetaViewModel etiquetaViewModel = new EtiquetaViewModel(base.Dto.Etiqueta);
			CampoViewModel campoViewModel = new CampoViewModel(base.Dto.Campos.Select((CampoEtiquetaDto x) => new CampoEtiquetaViewModel(x, ((Lista<OrigemDado>)configuracao.OrigemComboBox).ItemSelecionado.Campos.Itens)).ToList(), configuracao);
			PreVisualizacaoViewModel viewModel = new PreVisualizacaoViewModel(new PreVisualizacao(paginaViewModel, etiquetaViewModel, campoViewModel), ((Lista<OrigemDado>)configuracao.OrigemComboBox).ItemSelecionado.DataExemplo.Itens);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[5]
			{
				(IAbaViewModelBase)new EtiquetaConfiguracaoAbaViewModel((IViewModelPrincipal)(object)configuracao),
				(IAbaViewModelBase)new PaginaAbaViewModel((IViewModelDtoBase)(object)paginaViewModel),
				(IAbaViewModelBase)new EtiquetaAbaViewModel((IViewModelDtoBase)(object)etiquetaViewModel),
				(IAbaViewModelBase)new CampoAbaViewModel((IViewModelDtoBase)(object)campoViewModel),
				(IAbaViewModelBase)new PreVisualizacaoAbaViewModel((IViewModelDtoBase)(object)viewModel)
			});
			((Formulario)this).AlterouAbaSelecionadaEvent += OnAlterouAbaSelecionadaEvent;
		}

		private void OnAlterouAbaSelecionadaEvent(object sender, IAbaViewModelBase abaViewModel)
		{
			HelperObject.ToDo<PreVisualizacaoAbaViewModel>((object)abaViewModel, (Action<PreVisualizacaoAbaViewModel>)delegate(PreVisualizacaoAbaViewModel aba)
			{
				((PreVisualizacaoViewModel)(object)((AbaViewModelBase)aba).ViewModel).PreVisualizar();
			});
		}

		public override void Dispose()
		{
			((Formulario)this).AlterouAbaSelecionadaEvent -= OnAlterouAbaSelecionadaEvent;
			((Formulario)this).Dispose();
		}
	}
	public class EtiquetaConfiguracaoListaViewModel : ListaEntity<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel, IEtiquetaConfiguracaoModuleService, Etiqueta>
	{
		public bool ExportarVisivel => PodeExportar();

		public ICommand ImportarCommand { get; set; }

		public ICommand ExportarCommand { get; set; }

		public EtiquetaConfiguracaoListaViewModel(ISessao sessao, IEntityAtivos<EtiquetaConfiguracaoDto, IEtiquetaConfiguracaoModuleService> ativos, IEntityInativos<EtiquetaConfiguracaoDto, IEtiquetaConfiguracaoModuleService> inativos)
			: base(sessao, typeof(EtiquetaConfiguracaoFichaAdicionarView).Name, typeof(EtiquetaConfiguracaoFichaAtualizarView).Name, ativos, inativos)
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			ImportarCommand = (ICommand)new DelegateCommand((Action)Importar, (Func<bool>)PodeImportar, (bool?)true);
			ExportarCommand = (ICommand)new DelegateCommand((Action)Exportar, (Func<bool>)PodeExportar, (bool?)true);
		}

		protected override void SelecionadoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName.Equals("Selecionado"))
			{
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ExportarVisivel));
			}
		}

		private void Importar()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			try
			{
				XmlFileFactory.SetCurrent((IXmlFileFactory)new XmlEtiquetaFileFactory(ObterArquivoImportacao()));
				IXmlFile val = XmlFileFactory.Create();
				((ListaManutencaoBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel, Etiqueta>)(object)this).Adicionar(val.Read<EtiquetaConfiguracaoDto>());
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private string ObterArquivoImportacao()
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog
			{
				DefaultExt = ".xml",
				Filter = "Arquivos xml (*.xml)|*.xml"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				return openFileDialog.FileName;
			}
			throw new ApplicationException("Arquivo não foi informado!");
		}

		private bool PodeImportar()
		{
			return ((ListaManutencaoBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel, Etiqueta>)(object)this).PodeAdicionar();
		}

		private void Exportar()
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			try
			{
				string text = ObterDiretorioExportacao();
				XmlFileFactory.SetCurrent((IXmlFileFactory)new XmlEtiquetaFileFactory(Path.Combine(text, $"{((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado.Descricao}.xml")));
				XmlFileFactory.Create().Save<EtiquetaConfiguracaoDto>(ObterDto());
				Process.Start("explorer.exe", text);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private EtiquetaConfiguracaoDto ObterDto()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			return new EtiquetaConfiguracaoDto
			{
				Ativo = ((EntityDto)((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto).Ativo,
				CodigoOrigemDado = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto.CodigoOrigemDado,
				Descricao = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto.Descricao,
				Pagina = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto.Pagina,
				Etiqueta = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto.Etiqueta,
				Campos = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens.Selecionado).Dto.Campos
			};
		}

		private string ObterDiretorioExportacao()
		{
			using FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				return folderBrowserDialog.SelectedPath;
			}
			throw new ApplicationException("Diretório não foi informado!");
		}

		private bool PodeExportar()
		{
			return ((ListaBase<EtiquetaConfiguracaoDto, EtiquetaConfiguracaoViewModel>)(object)this).Itens?.SelecionadoValido ?? false;
		}
	}
	public class EtiquetaConfiguracaoViewModel : ViewModelAtivoBase<EtiquetaConfiguracaoDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
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

		public OrigemComboBoxViewModel OrigemComboBox { get; set; }

		public EtiquetaConfiguracaoViewModel()
		{
		}

		public EtiquetaConfiguracaoViewModel(EtiquetaConfiguracaoDto dto)
			: base(dto)
		{
			OrigemComboBox = new OrigemComboBoxViewModel(((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)this).Dto.CodigoOrigemDado);
		}

		public override void ProjectedAsViewModel()
		{
			base.Ativo = ((EntityDto)((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)this).Dto).Ativo;
			Descricao = ((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)this).Dto.Descricao;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			EtiquetaConfiguracaoDto val = (EtiquetaConfiguracaoDto)((obj is EtiquetaConfiguracaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.Descricao = Descricao;
				val.CodigoOrigemDado = ((Lista<OrigemDado>)OrigemComboBox).ItemSelecionado.Codigo;
				((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)this).AtualizarDto<EtiquetaConfiguracaoDto>(ref val);
			}
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new EtiquetaConfiguracaoFormulario(((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)this).Dto);
		}

		public string ToTituloLista()
		{
			return "Configurações de etiqueta";
		}

		public string ToTituloFicha()
		{
			return "Configuração de etiqueta";
		}
	}
	public class EtiquetasViewModel : ModuloBase
	{
		public EtiquetasViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloEtiquetaRegion, typeof(EtiquetaImpressaoFichaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class EtiquetaViewModel : ViewModelDtoBase<EtiquetaDto>
	{
		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Altura' deve ser maior que zero.")]
		public decimal Altura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Altura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Altura), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Largura' deve ser maior que zero.")]
		public decimal Largura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Largura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Largura), value);
			}
		}

		public EtiquetaViewModel(EtiquetaDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Altura = base.Dto.Altura;
			Largura = base.Dto.Largura;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			object obj = dto;
			EtiquetaConfiguracaoDto val = (EtiquetaConfiguracaoDto)((obj is EtiquetaConfiguracaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Etiqueta = new EtiquetaDto
				{
					Altura = Altura,
					Largura = Largura
				};
				base.AtualizarDto<EtiquetaConfiguracaoDto>(ref val);
			}
		}
	}
	public sealed class OrigemComboBoxViewModel : Lista<OrigemDado>
	{
		public override bool Valido
		{
			get
			{
				if (((ViewModelValidationBase)this).Valido && base.PossuiItemSelecionado && !HelperObject.IsNull((object)((Lista<OrigemDado>)this).ItemSelecionado.Filtro) && !HelperObject.IsNull((object)((Lista<OrigemDado>)this).ItemSelecionado.Filtro.TipoComboBox))
				{
					return ((ViewModelValidationBase)((Lista<OrigemDado>)this).ItemSelecionado.Filtro.TipoComboBox).Valido;
				}
				return false;
			}
		}

		public OrigemComboBoxViewModel(int codigoOrigemDado)
		{
			if (codigoOrigemDado == 0)
			{
				base.Adicionar((OrigemDado)new Produto());
				base.Adicionar((OrigemDado)new Pessoa());
				base.Adicionar((OrigemDado)new MovimentacaoObjeto());
				base.Adicionar((OrigemDado)new MovimentacaoVolume());
				base.Adicionar((OrigemDado)new MovimentacaoItem());
				base.PosicionarNoPrimeiro();
			}
			else
			{
				base.Adicionar(RetornarOrigemDado(codigoOrigemDado));
			}
		}

		private static OrigemDado RetornarOrigemDado(int codigoOrigemDado)
		{
			return codigoOrigemDado switch
			{
				1 => new Produto(), 
				2 => new Pessoa(), 
				3 => new MovimentacaoObjeto(), 
				4 => new MovimentacaoVolume(), 
				_ => new MovimentacaoItem(), 
			};
		}
	}
	public class PaginaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public PaginaAbaViewModel(IViewModelDtoBase viewModel)
			: base("MEDIDAS DA PÁGINA", "Tamanho, posições", viewModel)
		{
		}
	}
	public class PaginaViewModel : ViewModelDtoBase<PaginaDto>
	{
		[Range(1, int.MaxValue, ErrorMessage = "O campo 'Colunas' deve ser maior que zero.")]
		public int Colunas
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Colunas));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Colunas), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Largura' deve ser maior que zero.")]
		public decimal Largura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Largura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Largura), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Altura' deve ser maior que zero.")]
		public decimal Altura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Altura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Altura), value);
			}
		}

		public decimal MargemEsquerda
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => MargemEsquerda));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => MargemEsquerda), value);
			}
		}

		public decimal MargemDireita
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => MargemDireita));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => MargemDireita), value);
			}
		}

		public decimal MargemSuperior
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => MargemSuperior));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => MargemSuperior), value);
			}
		}

		public decimal MargemInferior
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => MargemInferior));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => MargemInferior), value);
			}
		}

		public PaginaViewModel(PaginaDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			Colunas = base.Dto.Colunas;
			Largura = base.Dto.Largura;
			Altura = base.Dto.Altura;
			MargemEsquerda = base.Dto.MargemEsquerda;
			MargemDireita = base.Dto.MargemDireita;
			MargemSuperior = base.Dto.MargemSuperior;
			MargemInferior = base.Dto.MargemInferior;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Expected O, but got Unknown
			object obj = dto;
			EtiquetaConfiguracaoDto val = (EtiquetaConfiguracaoDto)((obj is EtiquetaConfiguracaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.Pagina = new PaginaDto
				{
					Colunas = Colunas,
					Largura = Largura,
					Altura = Altura,
					MargemEsquerda = MargemEsquerda,
					MargemDireita = MargemDireita,
					MargemSuperior = MargemSuperior,
					MargemInferior = MargemInferior
				};
				base.AtualizarDto<EtiquetaConfiguracaoDto>(ref val);
			}
		}
	}
	public class PreVisualizacao : BindableBase, IDisposable
	{
		public PaginaViewModel Pagina { get; set; }

		public EtiquetaViewModel Etiqueta { get; set; }

		public CampoViewModel Campo { get; set; }

		public PreVisualizacao(PaginaViewModel pagina, EtiquetaViewModel etiqueta, CampoViewModel campo)
		{
			Pagina = pagina;
			Etiqueta = etiqueta;
			Campo = campo;
			((BindableBase)Pagina).PropertyChanged += OnPropertyChanged;
			((BindableBase)Etiqueta).PropertyChanged += OnPropertyChanged;
			((BindableBase)Campo).PropertyChanged += OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged();
		}

		public void Dispose()
		{
			((BindableBase)Pagina).PropertyChanged -= OnPropertyChanged;
			((BindableBase)Etiqueta).PropertyChanged -= OnPropertyChanged;
			((BindableBase)Campo).PropertyChanged -= OnPropertyChanged;
		}
	}
	public class PreVisualizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public PreVisualizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("PRÉ-VISUALIZAÇÃO", "Visualizar alterações", viewModel)
		{
		}
	}
	public class PreVisualizacaoViewModel : ViewModelDtoBase<PreVisualizacao>
	{
		private IEnumerable<IDado> _data;

		private readonly IWService _service;

		public override bool Valido => true;

		public XtraReportPreviewModel EtiquetaPreview
		{
			get
			{
				return ((BindableBase)this).GetProperty<XtraReportPreviewModel>((Expression<Func<XtraReportPreviewModel>>)(() => EtiquetaPreview));
			}
			set
			{
				((BindableBase)this).SetProperty<XtraReportPreviewModel>((Expression<Func<XtraReportPreviewModel>>)(() => EtiquetaPreview), value);
			}
		}

		public PreVisualizacaoViewModel(PreVisualizacao preVisualizacao, IEnumerable<IDado> data)
			: base(preVisualizacao)
		{
			_data = data;
			_service = DadosSistema.GetService();
		}

		public void PreVisualizar()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			EtiquetaPreview = new XtraReportPreviewModel((IReport)(object)GerarRelatorio((Band)(object)AdicionarBordaLimite(ObterBandaDetalhe())));
		}

		private XtraReport GerarRelatorio(Band bandaDetalhe)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Expected O, but got Unknown
			XtraReport val = new XtraReport
			{
				DataSource = _data,
				PaperKind = PaperKind.Custom,
				PageWidth = new Polegada(base.Dto.Pagina.Largura).ToInt(),
				PageHeight = new Polegada(base.Dto.Pagina.Altura).ToInt(),
				PageBreak = (PageBreak)1,
				ShowPrintMarginsWarning = false
			};
			val.Margins.Bottom = new Polegada(base.Dto.Pagina.MargemInferior).ToInt();
			val.Margins.Top = new Polegada(base.Dto.Pagina.MargemSuperior).ToInt();
			val.Margins.Left = new Polegada(base.Dto.Pagina.MargemEsquerda).ToInt();
			val.Margins.Right = new Polegada(base.Dto.Pagina.MargemDireita).ToInt();
			((CollectionBase)(object)((XtraReportBase)val).Bands).Clear();
			((XtraReportBase)val).Bands.Add(bandaDetalhe);
			val.CreateDocument();
			return val;
		}

		private DetailBand AdicionarBordaLimite(DetailBand bandaDetalhe)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			((XRControl)bandaDetalhe).Controls.Add((XRControl)new XRShape
			{
				Shape = (ShapeBase)new ShapeRectangle(),
				Width = new Polegada(base.Dto.Etiqueta.Largura).ToInt(),
				Height = new Polegada(base.Dto.Etiqueta.Altura).ToInt(),
				Stretch = true,
				Borders = (BorderSide)15
			});
			return bandaDetalhe;
		}

		private DetailBand ObterBandaDetalhe()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			DetailBand val = new DetailBand();
			val.MultiColumn.Mode = (MultiColumnMode)1;
			val.MultiColumn.ColumnCount = base.Dto.Pagina.Colunas;
			val.MultiColumn.ColumnWidth = new Polegada(base.Dto.Etiqueta.Largura).ToInt();
			val.MultiColumn.Layout = (ColumnLayout)1;
			((XRControl)val).Height = new Polegada(base.Dto.Etiqueta.Altura).ToInt();
			foreach (CampoBaseViewModel item in ((Lista<CampoFichaManutencaoDetalheViewModel>)(object)((ViewModelDetalheBase<CampoEtiquetaViewModel, CampoListaManutencaoDetalheViewModel, CampoFichaManutencaoDetalheViewModel>)base.Dto.Campo).ListaDetalheViewModel).Itens.Select((CampoFichaManutencaoDetalheViewModel x) => ((Lista<CampoBaseViewModel>)((ViewModelDtoBase<CampoEtiquetaViewModel>)(object)x).Dto.CamposComboBox).ItemSelecionado))
			{
				((XRControl)val).Controls.Add(item.ObterXrControl());
			}
			if (!HelperObject.IsNull((object)((ViewModelDetalheBase<CampoEtiquetaViewModel, CampoListaManutencaoDetalheViewModel, CampoFichaManutencaoDetalheViewModel>)base.Dto.Campo).FichaDetalheViewModel))
			{
				((XRControl)val).Controls.Add(((Lista<CampoBaseViewModel>)((ViewModelDtoBase<CampoEtiquetaViewModel>)(object)((ViewModelDetalheBase<CampoEtiquetaViewModel, CampoListaManutencaoDetalheViewModel, CampoFichaManutencaoDetalheViewModel>)base.Dto.Campo).FichaDetalheViewModel).Dto.CamposComboBox).ItemSelecionado.ObterXrControl());
			}
			return val;
		}

		public void Imprimir(IEnumerable<IDado> dados = null)
		{
			if (dados != null && dados.Any())
			{
				_data = dados;
			}
			XtraReport val = GerarRelatorio((Band)(object)ObterBandaDetalhe());
			if (HelperObject.IsNotNull((object)DadosSistema.ImpressoraNaoFiscalEtiqueta) && DadosSistema.ImpressoraNaoFiscalEtiqueta != "Nenhuma")
			{
				val.PrinterName = DadosSistema.ImpressoraNaoFiscalEtiqueta;
				val.CreateDocument();
				XtraReportExtensions.Print((IReport)(object)val);
			}
			else
			{
				PrintHelper.ShowPrintPreviewDialog(System.Windows.Application.Current.MainWindow, (IReport)(object)val);
			}
		}

		public async Task<long> GerarDav(IEnumerable<IDado> dados = null)
		{
			if (dados != null && dados.Any())
			{
				_data = dados;
			}
			RecebimentoDto recebimento = new RecebimentoDto();
			IEnumerable<ItemDto> source = CriarItemDto(_data.OfType<DadoProduto>());
			if (source.Any((ItemDto item) => item is ItemDtoNull))
			{
				return 0L;
			}
			ItemDto[] itemDtoAtualizado = default(ItemDto[]);
			_ = itemDtoAtualizado;
			itemDtoAtualizado = await Task.WhenAll(source.Select((ItemDto dto) => _service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(dto)))));
			if (itemDtoAtualizado.Any((ItemDto item) => item is ItemDtoNull))
			{
				return 0L;
			}
			MovimentacaoDto val = await _service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoDto>>)((IMovimentacaoSaidaModuleService service) => service.GerarDav((IEnumerable<ItemDto>)itemDtoAtualizado, recebimento)));
			DialogoFactory.MostrarDialogoConfirma($"DAV número {((MovimentacaoBaseDto)val).Numero} foi criado com sucesso!", false, System.Windows.HorizontalAlignment.Center);
			return ((MovimentacaoBaseDto)val).Numero;
		}

		private string MessagemQuantidadeSaldoNegativa(ProdutoServicoEmpresaDto produtoEmpresaDto)
		{
			return $"{produtoEmpresaDto.ProdutoServico.Descricao} atingiu quantidade saldo negativa! " + $"Porém, a quantidade em estoque é de {produtoEmpresaDto.ProdutoEmpresa.Estoque.Quantidade}. " + "Continuar?";
		}

		private IEnumerable<ItemDto> CriarItemDto(IEnumerable<DadoProduto> itens)
		{
			//IL_0253: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Unknown result type (might be due to invalid IL or missing references)
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02de: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_032b: Expected O, but got Unknown
			//IL_032b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0332: Unknown result type (might be due to invalid IL or missing references)
			//IL_0339: Unknown result type (might be due to invalid IL or missing references)
			//IL_034b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0352: Unknown result type (might be due to invalid IL or missing references)
			//IL_0359: Unknown result type (might be due to invalid IL or missing references)
			//IL_0369: Unknown result type (might be due to invalid IL or missing references)
			//IL_0375: Expected O, but got Unknown
			PessoaDto empresaMovimentacao = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.EmpresaLogada.Id)));
			PessoaDto pessoaMovimentacao = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(DadosSistema.IdClienteConsumidor)));
			IEnumerable<ProdutoServicoEmpresaDto> source = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService x) => x.ObterProdutoServicoEmpresaPorId(itens.Select((DadoProduto t) => t.ProdutoServicoEmpresaReferencia).ToArray())));
			List<ItemDto> list = new List<ItemDto>();
			List<string> list2 = new List<string>();
			foreach (IGrouping<string, DadoProduto> itemProduto in from x in itens
				group x by x.ProdutoServicoEmpresaReferencia)
			{
				ProdutoServicoEmpresaDto val = source.FirstOrDefault((ProdutoServicoEmpresaDto t) => ((EntityBaseDto)t).Id.Equals(itemProduto.FirstOrDefault().ProdutoServicoEmpresaReferencia));
				decimal num = itens.Where((DadoProduto x) => x.ProdutoServicoEmpresaReferencia.Equals(itemProduto.FirstOrDefault().ProdutoServicoEmpresaReferencia)).Sum((DadoProduto x) => x.SomaQuantidadeDav);
				if (!DadosSistema.RetornarPermitirEstoqueNegativoPorTipoMovimentacao((TipoMovimentacao)1))
				{
					if (val.ProdutoEmpresa.Estoque.Quantidade < 0m)
					{
						list2.Add($"{val.ProdutoServico.Descricao} atingiu estoque negativo, não será adicionado!");
						continue;
					}
					if (val.ProdutoEmpresa.Estoque.QuantidadeSaldo - num <= val.ProdutoEmpresa.Estoque.Quantidade - num && (!DialogoFactory.MostrarDialogoSimNao(MessagemQuantidadeSaldoNegativa(val), false, 0, System.Windows.HorizontalAlignment.Center).Equals(MessageBoxResult.Yes) || HelperObject.IsNull((object)PermitirVenderEstoqueSaldoNegativo())))
					{
						continue;
					}
				}
				if (val.ProdutoEmpresa.Estoque.QuantidadeMinima != 0m && val.ProdutoEmpresa.Estoque.Quantidade - num <= val.ProdutoEmpresa.Estoque.QuantidadeMinima)
				{
					list2.Add($"{val.ProdutoServico.Descricao} atingiu quantidade mínima em estoque.");
					continue;
				}
				list.Add(new ItemDto
				{
					ProdutoServicoEmpresaReferencia = itemProduto.FirstOrDefault().ProdutoServicoEmpresaReferencia,
					QuantidadeInformada = itemProduto.Sum((DadoProduto x) => HelperDecimal.TruncarValor(x.SomaQuantidadeDav, 4)),
					Quantidade = itemProduto.Sum((DadoProduto x) => HelperDecimal.TruncarValor(x.SomaQuantidadeDav, 4)),
					CasasDecimaisQuantidade = 4,
					Estoque = new EstoqueDto
					{
						Id = ((EntityBaseDto)val.ProdutoEmpresa.Estoque).Id,
						Quantidade = itemProduto.Sum((DadoProduto x) => HelperDecimal.TruncarValor(x.SomaQuantidadeDav, 4))
					},
					PessoaMovimentacao = pessoaMovimentacao,
					EmpresaMovimentacao = empresaMovimentacao,
					PrecoUnitarioInformado = val.Preco.PrecoVendaVigente,
					TipoMovimentacao = (TipoMovimentacao)1,
					CalculadoPeloSistema = true,
					DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia,
					Numero = 2
				});
			}
			if (list2.Any())
			{
				DialogoFactory.MostrarDialogoConfirma(string.Format("Os seguintes itens não foram importados: {0}", string.Join("\n", list2)), false, System.Windows.HorizontalAlignment.Center);
			}
			return list;
		}

		protected AutorizacaoDto PermitirVenderEstoqueSaldoNegativo()
		{
			return DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderEstoqueSaldoNegativo>(0m, (string)null);
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void Dispose()
		{
			base.Dto?.Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.Etiquetas.ViewModels.Tipos
{
	public abstract class BarrasViewModel : TextoViewModel
	{
		private CustomXRBarCode _barCode;

		public PadraoBarrasComboBoxViewModel PadraoBarras { get; set; }

		public OrientacaoCodigoBarrasComboBoxViewModel OrientacaoCodigoBarras { get; set; }

		public bool NaoMostrarTextoBarras
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => NaoMostrarTextoBarras));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => NaoMostrarTextoBarras), value);
			}
		}

		protected BarrasViewModel()
		{
		}

		protected BarrasViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			PadraoBarras = new PadraoBarrasComboBoxViewModel(dto.PadraoCodigoBarra);
			OrientacaoCodigoBarras = new OrientacaoCodigoBarrasComboBoxViewModel(dto.OrientacaoCodigoBarras);
			NaoMostrarTextoBarras = dto.NaoMostrarTextoBarras;
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.PadraoCodigoBarra = ((Lista<BarCodeGeneratorBase>)PadraoBarras).ItemSelecionado.Name;
			campoEtiquetaDto.OrientacaoCodigoBarras = ((Lista<OrientacaoCodigoBarrasBase>)OrientacaoCodigoBarras).ItemSelecionado.OrientacaoTela;
			campoEtiquetaDto.NaoMostrarTextoBarras = NaoMostrarTextoBarras;
		}

		public override XRControl ObterXrControl()
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			_barCode = new CustomXRBarCode(Titulo, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade), ((Lista<BarCodeGeneratorBase>)PadraoBarras).ItemSelecionado, ((Lista<OrientacaoCodigoBarrasBase>)OrientacaoCodigoBarras).ItemSelecionado.Orientacao, NaoMostrarTextoBarras);
			((XRControl)_barCode).BeforePrint += ControleOnBeforePrint;
			return (XRControl)(object)_barCode;
		}

		private void ControleOnBeforePrint(object sender, PrintEventArgs printEventArgs)
		{
			DadoBranco dadoBranco = ((XRControl)(CustomXRBarCode)sender).Report.GetCurrentRow() as DadoBranco;
			((XRControl)(CustomXRBarCode)sender).Visible = dadoBranco == null;
		}

		public override void Dispose()
		{
			((XRControl)_barCode).BeforePrint -= ControleOnBeforePrint;
			base.Dispose();
		}
	}
	public class CabecaParaBaixo : OrientacaoCodigoBarrasBase
	{
		public CabecaParaBaixo()
			: base((BarCodeOrientation)1, "Cabeça para baixo")
		{
		}
	}
	public abstract class CampoBaseViewModel : ViewModelValidationBase
	{
		public abstract string Titulo { get; }

		public abstract string NomePropriedade { get; }

		public abstract int Codigo { get; }

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Largura' deve ser maior que zero.")]
		public decimal Largura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Largura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Largura), value);
			}
		}

		[NaoPermitirValorMenorIgualZero(ErrorMessage = "A 'Altura' deve ser maior que zero.")]
		public decimal Altura
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Altura));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Altura), value);
			}
		}

		public decimal PosicaoX
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PosicaoX));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PosicaoX), value);
			}
		}

		public decimal PosicaoY
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PosicaoY));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PosicaoY), value);
			}
		}

		protected CampoBaseViewModel()
		{
		}

		protected CampoBaseViewModel(CampoEtiquetaDto dto)
		{
			Largura = dto.Largura;
			Altura = dto.Altura;
			PosicaoX = dto.PosicaoX;
			PosicaoY = dto.PosicaoY;
		}

		public abstract CampoBaseViewModel Criar(CampoEtiquetaDto dto);

		public virtual void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			campoEtiquetaDto.Altura = Altura;
			campoEtiquetaDto.Largura = Largura;
			campoEtiquetaDto.CodigoCampo = Codigo;
			campoEtiquetaDto.PosicaoX = PosicaoX;
			campoEtiquetaDto.PosicaoY = PosicaoY;
		}

		public abstract XRControl ObterXrControl();
	}
	public class CustomBaseXRLabel : XRLabel
	{
		public CustomBaseXRLabel(string titulo, decimal posicaoX, decimal posicaoY, bool negrito, string fonte, decimal tamanhoFonte, string cor, bool multiLinha, decimal largura, decimal altura, int angulo, string nomePropriedade, TextAlignment alinhamentoTexto)
		{
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			((XRControl)this).Text = titulo;
			((XRControl)this).Height = new Polegada(altura).ToInt();
			((XRControl)this).Width = new Polegada(largura).ToInt();
			((XRLabel)this).Multiline = multiLinha;
			((XRLabel)this).Angle = angulo;
			((XRControl)this).Name = nomePropriedade;
			((XRControl)this).LocationFloat = new PointFloat(new Polegada(posicaoX).Valor, new Polegada(posicaoY).Valor);
			((XRControl)this).Font = (negrito ? new Font(fonte, (float)tamanhoFonte, System.Drawing.FontStyle.Bold) : new Font(fonte, (float)tamanhoFonte));
			((XRControl)this).ForeColor = ColorTranslator.FromHtml(cor);
			((XRControl)this).WordWrap = multiLinha;
			((XRControl)this).TextAlignment = alinhamentoTexto;
		}
	}
	public class CustomBaseXRPictureBox : XRPictureBox
	{
		public CustomBaseXRPictureBox(decimal posicaoX, decimal posicaoY, decimal largura, decimal altura, float opacidade, System.Drawing.Image imagem)
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			((XRPictureBox)this).Image = SetImageOpacity(imagem, opacidade);
			((XRControl)this).LocationF = PointFloat.op_Implicit(new PointFloat(new Polegada(posicaoX).Valor, new Polegada(posicaoY).Valor));
			((XRControl)this).SizeF = new SizeF(new Polegada(largura).ToInt(), new Polegada(altura).ToInt());
			((XRPictureBox)this).Sizing = (ImageSizeMode)1;
		}

		protected System.Drawing.Image SetImageOpacity(System.Drawing.Image image, float opacity)
		{
			try
			{
				Bitmap bitmap = new Bitmap(image.Width, image.Height);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					ColorMatrix newColorMatrix = new ColorMatrix
					{
						Matrix33 = opacity
					};
					ImageAttributes imageAttributes = new ImageAttributes();
					imageAttributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
					graphics.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
				}
				return bitmap;
			}
			catch (Exception)
			{
				return image;
			}
		}
	}
	public class CustomXRBarCode : XRBarCode
	{
		public CustomXRBarCode(string titulo, decimal posicaoX, decimal posicaoY, bool negrito, string fonte, decimal tamanhoFonte, string cor, bool multiLinha, decimal largura, decimal altura, string nomePropriedade, TextAlignment alinhamentoTexto, XRBinding xrBinding, BarCodeGeneratorBase padraoBarras, BarCodeOrientation orientacaoCodigoBarras, bool naoMostrarTextoBarras)
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
			((XRControl)this).Height = new Polegada(altura).ToInt();
			((XRControl)this).Width = new Polegada(largura).ToInt();
			((XRControl)this).Name = nomePropriedade;
			((XRBarCode)this).Symbology = padraoBarras;
			((XRBarCode)this).AutoModule = true;
			((XRControl)this).Text = titulo;
			((XRControl)this).LocationFloat = new PointFloat(new Polegada(posicaoX).Valor, new Polegada(posicaoY).Valor);
			((XRControl)this).Font = (negrito ? new Font(fonte, (float)tamanhoFonte, System.Drawing.FontStyle.Bold) : new Font(fonte, (float)tamanhoFonte));
			((XRControl)this).ForeColor = ColorTranslator.FromHtml(cor);
			((XRControl)this).WordWrap = multiLinha;
			((XRControl)this).TextAlignment = alinhamentoTexto;
			((XRControl)this).SizeF = new SizeF(new Polegada(largura).Valor, new Polegada(altura).Valor);
			((XRBarCode)this).BarCodeOrientation = orientacaoCodigoBarras;
			((XRBarCode)this).ShowText = !naoMostrarTextoBarras;
			((XRControl)this).DataBindings.Add(xrBinding);
		}
	}
	public class CustomXRLabel : CustomBaseXRLabel
	{
		public CustomXRLabel(string titulo, decimal posicaoX, decimal posicaoY, bool negrito, string fonte, decimal tamanhoFonte, string cor, bool multiLinha, decimal largura, decimal altura, int angulo, string nomePropriedade, TextAlignment alinhamentoTexto, XRBinding xrBinding)
			: base(titulo, posicaoX, posicaoY, negrito, fonte, tamanhoFonte, cor, multiLinha, largura, altura, angulo, nomePropriedade, alinhamentoTexto)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			((XRControl)this).DataBindings.Add(xrBinding);
		}
	}
	public class DadoBase : ViewModelValidationBase, IDado
	{
		public Lista<IDado> DadosRelatorio { get; protected set; }

		public long QuantidadeEtiquetas
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => QuantidadeEtiquetas));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => QuantidadeEtiquetas), value, (Action)OnQuantidadeEtiquetasChanged);
			}
		}

		public DadoBase(long quantidadeEtiquetas)
		{
			DadosRelatorio = new Lista<IDado>();
			QuantidadeEtiquetas = quantidadeEtiquetas;
		}

		private void OnQuantidadeEtiquetasChanged()
		{
			DadosRelatorio.Limpar();
			if (QuantidadeEtiquetas <= 0)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Gerando etiqueta(s)...");
			Task.Factory.StartNew(delegate
			{
				for (int i = 1; i <= QuantidadeEtiquetas; i++)
				{
					DadosRelatorio.Adicionar((IDado)this);
					DispatcherHelper.DoEvents();
				}
			}).ContinueWith(delegate
			{
				LoaderProcessoService.EsconderLoader();
			}, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
		}

		public void SetarQuantidadeEtiquetas(long quantidadeEtiquetas)
		{
			QuantidadeEtiquetas = quantidadeEtiquetas;
		}
	}
	public class DadoBranco : DadoBase
	{
		public DadoBranco(int quantidadeEtiquetas)
			: base(quantidadeEtiquetas)
		{
		}
	}
	public class DadoMovimentacaoItem : DadoBase
	{
		public string Nome { get; protected set; }

		public string Telefone { get; set; }

		public string EnderecoPrincipal { get; set; }

		public string EnderecoPrincipalNumero { get; set; }

		public string EnderecoPrincipalBairro { get; set; }

		public string EnderecoPrincipalCep { get; set; }

		public string EnderecoPrincipalMunicipio { get; set; }

		public string EnderecoPrincipalUf { get; set; }

		public string EnderecoPrincipalPais { get; set; }

		public string EnderecoEntrega { get; set; }

		public string EnderecoEntregaNumero { get; set; }

		public string EnderecoEntregaBairro { get; set; }

		public string EnderecoEntregaCep { get; set; }

		public string EnderecoEntregaMunicipio { get; set; }

		public string EnderecoEntregaUf { get; set; }

		public string EnderecoEntregaPais { get; set; }

		public string NomeFantasia { get; set; }

		public string Responsavel { get; set; }

		public string Documento { get; set; }

		public string Email { get; set; }

		public string Observacao { get; set; }

		public long MovimentacaoNumero { get; set; }

		public DateTime MovimentacaoDataEmissao { get; set; }

		public decimal MovimentacaoTotal { get; set; }

		public string MovimentacaoChaveAcesso { get; set; }

		public string MovimentacaoNomeTecnicoResponsavel { get; set; }

		public string MovimentacaoNomeVendedorResponsavel { get; set; }

		public string MovimentacaoDescricaoPrioridade { get; set; }

		public decimal MovimentacaoTotalPesoBruto { get; set; }

		public decimal MovimentacaoTotalPesoLiquido { get; set; }

		public decimal MovimentacaoItemQuantidade { get; set; }

		public string MovimentacaoItemDescricao { get; set; }

		public string MovimentacaoItemUnidade { get; set; }

		public decimal MovimentacaoItemTotal { get; set; }

		public decimal MovimentacaoItemTotalPesoLiquido { get; set; }

		public decimal MovimentacaoItemTotalPesoBruto { get; set; }

		public string MovimentacaoItemObservacao { get; set; }

		public string MovimentacaoItemCodigoInterno { get; set; }

		public string MovimentacaoItemCodigoBarras { get; set; }

		public string MovimentacaoItemCodigoBarrasTributavel { get; set; }

		public string MovimentacaoItemInformacaoEstoque { get; set; }

		public string ToText => MovimentacaoItemDescricao;

		public DateTime DataImpressao => DateTime.Now;

		public DateTime HoraImpressao => DateTime.Now;

		public DadoMovimentacaoItem(string nome, string telefone, string enderecoPrincipal, string enderecoPrincipalNumero, string enderecoPrincipalBairro, string enderecoPrincipalCep, string enderecoPrincipalMunicipio, string enderecoPrincipalUf, string enderecoPrincipalPais, string enderecoEntrega, string enderecoEntregaNumero, string enderecoEntregaBairro, string enderecoEntregaCep, string enderecoEntregaMunicipio, string enderecoEntregaUf, string enderecoEntregaPais, string nomeFantasia, string responsavel, string documentoCpf, string documentoCnpj, string email, long quantidadeEtiquetas, string observacao, long movimentacaoNumero, DateTime movimentacaoDataEmissao, decimal movimentacaoTotal, string movimentacaoChaveAcesso, string movimentacaoNomeTecnicoResponsavel, string movimentacaoNomeVendedorResponsavel, string movimentacaoDescricaoPrioridade, decimal movimentacaoTotalPesoBruto, decimal movimentacaoTotalPesoLiquido, decimal movimentacaoItemQuantidade, string movimentacaoItemDescricao, string movimentacaoItemUnidade, decimal movimentacaoItemTotal, decimal movimentacaoItemTotalPesoLiquido, decimal movimentacaoItemTotalPesoBruto, string movimentacaoItemObservacao, string movimentacaoItemCodigoInterno, string movimentacaoItemCodigoBarras, string movimentacaoItemCodigoBarrasTributavel, string movimentacaoItemInformacaoEstoque)
			: base(quantidadeEtiquetas)
		{
			base.QuantidadeEtiquetas = quantidadeEtiquetas;
			Nome = nome;
			Telefone = telefone;
			EnderecoPrincipal = enderecoPrincipal;
			EnderecoPrincipalNumero = enderecoPrincipalNumero;
			EnderecoPrincipalBairro = enderecoPrincipalBairro;
			EnderecoPrincipalCep = enderecoPrincipalCep;
			EnderecoPrincipalMunicipio = enderecoPrincipalMunicipio;
			EnderecoPrincipalUf = enderecoPrincipalUf;
			EnderecoPrincipalPais = enderecoPrincipalPais;
			EnderecoEntrega = enderecoEntrega;
			EnderecoEntregaNumero = enderecoEntregaNumero;
			EnderecoEntregaBairro = enderecoEntregaBairro;
			EnderecoEntregaCep = enderecoEntregaCep;
			EnderecoEntregaMunicipio = enderecoEntregaMunicipio;
			EnderecoEntregaUf = enderecoEntregaUf;
			EnderecoEntregaPais = enderecoEntregaPais;
			NomeFantasia = nomeFantasia;
			Responsavel = responsavel;
			Documento = (string.IsNullOrEmpty(documentoCpf) ? documentoCnpj : documentoCpf);
			Email = email;
			Observacao = observacao;
			MovimentacaoNumero = movimentacaoNumero;
			MovimentacaoDataEmissao = movimentacaoDataEmissao;
			MovimentacaoTotal = movimentacaoTotal;
			MovimentacaoChaveAcesso = movimentacaoChaveAcesso;
			MovimentacaoNomeTecnicoResponsavel = movimentacaoNomeTecnicoResponsavel;
			MovimentacaoNomeVendedorResponsavel = movimentacaoNomeVendedorResponsavel;
			MovimentacaoDescricaoPrioridade = movimentacaoDescricaoPrioridade;
			MovimentacaoTotalPesoBruto = movimentacaoTotalPesoBruto;
			MovimentacaoTotalPesoLiquido = movimentacaoTotalPesoLiquido;
			MovimentacaoItemQuantidade = movimentacaoItemQuantidade;
			MovimentacaoItemDescricao = movimentacaoItemDescricao;
			MovimentacaoItemUnidade = movimentacaoItemUnidade;
			MovimentacaoItemTotal = movimentacaoItemTotal;
			MovimentacaoItemTotalPesoLiquido = movimentacaoItemTotalPesoLiquido;
			MovimentacaoItemTotalPesoBruto = movimentacaoItemTotalPesoBruto;
			MovimentacaoItemObservacao = movimentacaoItemObservacao;
			MovimentacaoItemCodigoInterno = movimentacaoItemCodigoInterno;
			MovimentacaoItemCodigoBarras = movimentacaoItemCodigoBarras;
			MovimentacaoItemCodigoBarrasTributavel = movimentacaoItemCodigoBarrasTributavel;
			MovimentacaoItemInformacaoEstoque = movimentacaoItemInformacaoEstoque;
		}
	}
	public class DadoMovimentacaoObjeto : DadoBase
	{
		public string Nome { get; protected set; }

		public string Telefone { get; set; }

		public string EnderecoPrincipal { get; set; }

		public string EnderecoPrincipalNumero { get; set; }

		public string EnderecoPrincipalBairro { get; set; }

		public string EnderecoPrincipalCep { get; set; }

		public string EnderecoPrincipalMunicipio { get; set; }

		public string EnderecoPrincipalUf { get; set; }

		public string EnderecoPrincipalPais { get; set; }

		public string EnderecoEntrega { get; set; }

		public string EnderecoEntregaNumero { get; set; }

		public string EnderecoEntregaBairro { get; set; }

		public string EnderecoEntregaCep { get; set; }

		public string EnderecoEntregaMunicipio { get; set; }

		public string EnderecoEntregaUf { get; set; }

		public string EnderecoEntregaPais { get; set; }

		public string NomeFantasia { get; set; }

		public string Responsavel { get; set; }

		public string Documento { get; set; }

		public string Email { get; set; }

		public string Observacao { get; set; }

		public string ObjetoDescricao { get; set; }

		public string ObjetoSerial { get; set; }

		public string ObjetoPlaca { get; set; }

		public string ObjetoChassi { get; set; }

		public string ObjetoModelo { get; set; }

		public string ObjetoMarca { get; set; }

		public string ObjetoRenavam { get; set; }

		public string ObjetoCor { get; set; }

		public int ObjetoAnoFabricacao { get; set; }

		public string Defeito { get; set; }

		public string Diagnostico { get; set; }

		public DateTime DataGarantia { get; set; }

		public long MovimentacaoNumero { get; set; }

		public DateTime MovimentacaoDataEmissao { get; set; }

		public decimal MovimentacaoTotal { get; set; }

		public string MovimentacaoChaveAcesso { get; set; }

		public string MovimentacaoNomeTecnicoResponsavel { get; set; }

		public string MovimentacaoNomeVendedorResponsavel { get; set; }

		public string MovimentacaoDescricaoPrioridade { get; set; }

		public decimal MovimentacaoTotalPesoBruto { get; set; }

		public decimal MovimentacaoTotalPesoLiquido { get; set; }

		public string ObjetoCilindrada { get; set; }

		public string ObjetoPotencia { get; set; }

		public string ObjetoMotor { get; set; }

		public string ObjetoSegmento { get; set; }

		public string ToText => Nome;

		public DateTime DataImpressao => DateTime.Now;

		public DateTime HoraImpressao => DateTime.Now;

		public DadoMovimentacaoObjeto(string nome, string telefone, string enderecoPrincipal, string enderecoPrincipalNumero, string enderecoPrincipalBairro, string enderecoPrincipalCep, string enderecoPrincipalMunicipio, string enderecoPrincipalUf, string enderecoPrincipalPais, string enderecoEntrega, string enderecoEntregaNumero, string enderecoEntregaBairro, string enderecoEntregaCep, string enderecoEntregaMunicipio, string enderecoEntregaUf, string enderecoEntregaPais, string nomeFantasia, string responsavel, string documentoCpf, string documentoCnpj, string email, long quantidadeEtiquetas, string observacao, string objetoDescricao, string objetoSerial, string objetoPlaca, string objetoChassi, string objetoModelo, string objetoMarca, string objetoRenavam, string objetoCor, int objetoAnoFabricacao, string defeito, string diagnostico, DateTime dataGarantia, long movimentacaoNumero, DateTime movimentacaoDataEmissao, decimal movimentacaoTotal, string movimentacaoChaveAcesso, string movimentacaoNomeTecnicoResponsavel, string movimentacaoNomeVendedorResponsavel, string movimentacaoDescricaoPrioridade, decimal movimentacaoTotalPesoBruto, decimal movimentacaoTotalPesoLiquido, string objetoCilindrada, string objetoPotencia, string objetoMotor, string objetoSegmento)
			: base(quantidadeEtiquetas)
		{
			base.QuantidadeEtiquetas = quantidadeEtiquetas;
			Nome = nome;
			Telefone = telefone;
			EnderecoPrincipal = enderecoPrincipal;
			EnderecoPrincipalNumero = enderecoPrincipalNumero;
			EnderecoPrincipalBairro = enderecoPrincipalBairro;
			EnderecoPrincipalCep = enderecoPrincipalCep;
			EnderecoPrincipalMunicipio = enderecoPrincipalMunicipio;
			EnderecoPrincipalUf = enderecoPrincipalUf;
			EnderecoPrincipalPais = enderecoPrincipalPais;
			EnderecoEntrega = enderecoEntrega;
			EnderecoEntregaNumero = enderecoEntregaNumero;
			EnderecoEntregaBairro = enderecoEntregaBairro;
			EnderecoEntregaCep = enderecoEntregaCep;
			EnderecoEntregaMunicipio = enderecoEntregaMunicipio;
			EnderecoEntregaUf = enderecoEntregaUf;
			EnderecoEntregaPais = enderecoEntregaPais;
			NomeFantasia = nomeFantasia;
			Responsavel = responsavel;
			Documento = (string.IsNullOrEmpty(documentoCpf) ? documentoCnpj : documentoCpf);
			Email = email;
			Observacao = observacao;
			ObjetoDescricao = objetoDescricao;
			ObjetoSerial = objetoSerial;
			ObjetoPlaca = objetoPlaca;
			ObjetoChassi = objetoChassi;
			ObjetoModelo = objetoModelo;
			ObjetoMarca = objetoMarca;
			ObjetoRenavam = objetoRenavam;
			ObjetoCor = objetoCor;
			ObjetoAnoFabricacao = objetoAnoFabricacao;
			Defeito = defeito;
			Diagnostico = diagnostico;
			DataGarantia = dataGarantia;
			MovimentacaoNumero = movimentacaoNumero;
			MovimentacaoDataEmissao = movimentacaoDataEmissao;
			MovimentacaoTotal = movimentacaoTotal;
			MovimentacaoChaveAcesso = movimentacaoChaveAcesso;
			MovimentacaoNomeTecnicoResponsavel = movimentacaoNomeTecnicoResponsavel;
			MovimentacaoNomeVendedorResponsavel = movimentacaoNomeVendedorResponsavel;
			MovimentacaoDescricaoPrioridade = movimentacaoDescricaoPrioridade;
			MovimentacaoTotalPesoBruto = movimentacaoTotalPesoBruto;
			MovimentacaoTotalPesoLiquido = movimentacaoTotalPesoLiquido;
			ObjetoCilindrada = objetoCilindrada;
			ObjetoPotencia = objetoPotencia;
			ObjetoMotor = objetoMotor;
			ObjetoSegmento = objetoSegmento;
		}
	}
	public class DadoMovimentacaoVolume : DadoBase
	{
		public string Nome { get; protected set; }

		public string Telefone { get; set; }

		public string EnderecoPrincipal { get; set; }

		public string EnderecoPrincipalNumero { get; set; }

		public string EnderecoPrincipalBairro { get; set; }

		public string EnderecoPrincipalCep { get; set; }

		public string EnderecoPrincipalMunicipio { get; set; }

		public string EnderecoPrincipalUf { get; set; }

		public string EnderecoPrincipalPais { get; set; }

		public string EnderecoEntrega { get; set; }

		public string EnderecoEntregaNumero { get; set; }

		public string EnderecoEntregaBairro { get; set; }

		public string EnderecoEntregaCep { get; set; }

		public string EnderecoEntregaMunicipio { get; set; }

		public string EnderecoEntregaUf { get; set; }

		public string EnderecoEntregaPais { get; set; }

		public string NomeFantasia { get; set; }

		public string Responsavel { get; set; }

		public string Documento { get; set; }

		public string Email { get; set; }

		public string Observacao { get; set; }

		public int VolumeQuantidade { get; set; }

		public string VolumeEspecie { get; set; }

		public string VolumeMarca { get; set; }

		public string VolumeNumeracao { get; set; }

		public decimal VolumePesoLiquido { get; set; }

		public decimal VolumePesoBruto { get; set; }

		public long MovimentacaoNumero { get; set; }

		public DateTime MovimentacaoDataEmissao { get; set; }

		public decimal MovimentacaoTotal { get; set; }

		public string MovimentacaoChaveAcesso { get; set; }

		public string MovimentacaoNomeTecnicoResponsavel { get; set; }

		public string MovimentacaoNomeVendedorResponsavel { get; set; }

		public string MovimentacaoDescricaoPrioridade { get; set; }

		public decimal MovimentacaoTotalPesoBruto { get; set; }

		public decimal MovimentacaoTotalPesoLiquido { get; set; }

		public string TransportadoraNome { get; set; }

		public string TransportadoraNomeFantasia { get; set; }

		public string TransportadoraDocumento { get; set; }

		public string VolumeContador { get; set; }

		public string ToText => Nome;

		public DateTime DataImpressao => DateTime.Now;

		public DateTime HoraImpressao => DateTime.Now;

		public DadoMovimentacaoVolume(string nome, string telefone, string enderecoPrincipal, string enderecoPrincipalNumero, string enderecoPrincipalBairro, string enderecoPrincipalCep, string enderecoPrincipalMunicipio, string enderecoPrincipalUf, string enderecoPrincipalPais, string enderecoEntrega, string enderecoEntregaNumero, string enderecoEntregaBairro, string enderecoEntregaCep, string enderecoEntregaMunicipio, string enderecoEntregaUf, string enderecoEntregaPais, string nomeFantasia, string responsavel, string documentoCpf, string documentoCnpj, string email, long quantidadeEtiquetas, string observacao, int volumeQuantidade, string volumeEspecie, string volumeMarca, string volumeNumeracao, decimal volumePesoLiquido, decimal volumePesoBruto, long movimentacaoNumero, DateTime movimentacaoDataEmissao, decimal movimentacaoTotal, string movimentacaoChaveAcesso, string movimentacaoNomeTecnicoResponsavel, string movimentacaoNomeVendedorResponsavel, string movimentacaoDescricaoPrioridade, decimal movimentacaoTotalPesoBruto, decimal movimentacaoTotalPesoLiquido, string transportadoraNome, string transportadoraNomeFantasia, string transportadoraDocumento, string volumeContador)
			: base(quantidadeEtiquetas)
		{
			base.QuantidadeEtiquetas = quantidadeEtiquetas;
			Nome = nome;
			Telefone = telefone;
			EnderecoPrincipal = enderecoPrincipal;
			EnderecoPrincipalNumero = enderecoPrincipalNumero;
			EnderecoPrincipalBairro = enderecoPrincipalBairro;
			EnderecoPrincipalCep = enderecoPrincipalCep;
			EnderecoPrincipalMunicipio = enderecoPrincipalMunicipio;
			EnderecoPrincipalUf = enderecoPrincipalUf;
			EnderecoPrincipalPais = enderecoPrincipalPais;
			EnderecoEntrega = enderecoEntrega;
			EnderecoEntregaNumero = enderecoEntregaNumero;
			EnderecoEntregaBairro = enderecoEntregaBairro;
			EnderecoEntregaCep = enderecoEntregaCep;
			EnderecoEntregaMunicipio = enderecoEntregaMunicipio;
			EnderecoEntregaUf = enderecoEntregaUf;
			EnderecoEntregaPais = enderecoEntregaPais;
			NomeFantasia = nomeFantasia;
			Responsavel = responsavel;
			Documento = (string.IsNullOrEmpty(documentoCpf) ? documentoCnpj : documentoCpf);
			Email = email;
			Observacao = observacao;
			VolumeQuantidade = volumeQuantidade;
			VolumeEspecie = volumeEspecie;
			VolumeMarca = volumeMarca;
			VolumeNumeracao = volumeNumeracao;
			VolumePesoLiquido = volumePesoLiquido;
			VolumePesoBruto = volumePesoBruto;
			MovimentacaoNumero = movimentacaoNumero;
			MovimentacaoDataEmissao = movimentacaoDataEmissao;
			MovimentacaoTotal = movimentacaoTotal;
			MovimentacaoChaveAcesso = movimentacaoChaveAcesso;
			MovimentacaoNomeTecnicoResponsavel = movimentacaoNomeTecnicoResponsavel;
			MovimentacaoNomeVendedorResponsavel = movimentacaoNomeVendedorResponsavel;
			MovimentacaoDescricaoPrioridade = movimentacaoDescricaoPrioridade;
			MovimentacaoTotalPesoBruto = movimentacaoTotalPesoBruto;
			MovimentacaoTotalPesoLiquido = movimentacaoTotalPesoLiquido;
			TransportadoraNome = transportadoraNome;
			TransportadoraNomeFantasia = transportadoraNomeFantasia;
			TransportadoraDocumento = transportadoraDocumento;
			VolumeContador = volumeContador;
		}
	}
	public class DadoPessoa : DadoBase
	{
		public string Nome { get; protected set; }

		public string Telefone { get; set; }

		public string EnderecoPrincipal { get; set; }

		public string EnderecoPrincipalNumero { get; set; }

		public string EnderecoPrincipalBairro { get; set; }

		public string EnderecoPrincipalCep { get; set; }

		public string EnderecoPrincipalMunicipio { get; set; }

		public string EnderecoPrincipalUf { get; set; }

		public string EnderecoPrincipalPais { get; set; }

		public string EnderecoEntrega { get; set; }

		public string EnderecoEntregaNumero { get; set; }

		public string EnderecoEntregaBairro { get; set; }

		public string EnderecoEntregaCep { get; set; }

		public string EnderecoEntregaMunicipio { get; set; }

		public string EnderecoEntregaUf { get; set; }

		public string EnderecoEntregaPais { get; set; }

		public string NomeFantasia { get; set; }

		public string Responsavel { get; set; }

		public string Documento { get; set; }

		public string Email { get; set; }

		public string Observacao { get; set; }

		public DateTime DataImpressao => DateTime.Now;

		public DateTime HoraImpressao => DateTime.Now;

		public DadoPessoa(string nome, string telefone, string enderecoPrincipal, string enderecoPrincipalNumero, string enderecoPrincipalBairro, string enderecoPrincipalCep, string enderecoPrincipalMunicipio, string enderecoPrincipalUf, string enderecoPrincipalPais, string enderecoEntrega, string enderecoEntregaNumero, string enderecoEntregaBairro, string enderecoEntregaCep, string enderecoEntregaMunicipio, string enderecoEntregaUf, string enderecoEntregaPais, string nomeFantasia, string responsavel, string documentoCpf, string documentoCnpj, string email, long quantidadeEtiquetas, string observacao)
			: base(quantidadeEtiquetas)
		{
			base.QuantidadeEtiquetas = quantidadeEtiquetas;
			Nome = nome;
			Telefone = telefone;
			EnderecoPrincipal = enderecoPrincipal;
			EnderecoPrincipalNumero = enderecoPrincipalNumero;
			EnderecoPrincipalBairro = enderecoPrincipalBairro;
			EnderecoPrincipalCep = enderecoPrincipalCep;
			EnderecoPrincipalMunicipio = enderecoPrincipalMunicipio;
			EnderecoPrincipalUf = enderecoPrincipalUf;
			EnderecoPrincipalPais = enderecoPrincipalPais;
			EnderecoEntrega = enderecoEntrega;
			EnderecoEntregaNumero = enderecoEntregaNumero;
			EnderecoEntregaBairro = enderecoEntregaBairro;
			EnderecoEntregaCep = enderecoEntregaCep;
			EnderecoEntregaMunicipio = enderecoEntregaMunicipio;
			EnderecoEntregaUf = enderecoEntregaUf;
			EnderecoEntregaPais = enderecoEntregaPais;
			NomeFantasia = nomeFantasia;
			Responsavel = responsavel;
			Documento = (string.IsNullOrEmpty(documentoCpf) ? documentoCnpj : documentoCpf);
			Email = email;
			Observacao = observacao;
		}
	}
	public class DadoProduto : DadoBase
	{
		private readonly decimal _precoVenda;

		private readonly decimal _precoReferencia;

		private readonly decimal _precoVendaCadastro;

		private readonly decimal _precoVendaPromocional;

		public string CodigoInterno { get; protected set; }

		public string CodigoInternoFormatoCodigoBarra { get; protected set; }

		public string CodigoBarra { get; protected set; }

		public string Descricao { get; protected set; }

		public string DescricaoGradeLinhaColuna { get; protected set; }

		public string ProdutoServicoEmpresaReferencia { get; protected set; }

		public decimal PrecoCusto { get; protected set; }

		public decimal PrecoVenda
		{
			get
			{
				if (!AplicarPrecoAgendado)
				{
					return _precoVenda;
				}
				return PrecoVendaAgendado;
			}
		}

		public string UnidadeMedida { get; protected set; }

		public string Caracteristica { get; protected set; }

		public string Marca { get; protected set; }

		public string Observacao { get; protected set; }

		public decimal PesoLiquido { get; protected set; }

		public decimal PesoBruto { get; protected set; }

		public string Localizacao { get; protected set; }

		public string GradeLinha { get; protected set; }

		public string GradeColuna { get; protected set; }

		public decimal QuantidadeEstoque { get; protected set; }

		public string Personalizado { get; protected set; }

		public decimal PrecoReferencia
		{
			get
			{
				if (!AplicarPrecoAgendado)
				{
					return _precoReferencia;
				}
				return PrecoReferenciaAgendado;
			}
		}

		public string DescricaoPrecoReferencia { get; protected set; }

		public DateTime DataUltimaCompra { get; protected set; }

		public string Lote { get; protected set; }

		public string Serial { get; protected set; }

		public DateTime DataFabricacaoLote { get; protected set; }

		public DateTime DataValidadeLote { get; protected set; }

		public DateTime DataConclusaoOrdemProducao { get; protected set; }

		public DateTime DataImpressao => DateTime.Now;

		public DateTime HoraImpressao => DateTime.Now;

		public Dictionary<string, decimal> TabelasPreco { get; protected set; }

		public string IdTabelaPreco { get; protected set; }

		public string CondicaoEspecial { get; protected set; }

		public string Link { get; protected set; }

		public DateTime DataUltimaAlteracaoPrecoVenda { get; protected set; }

		public string CodigoBarrasTributavel { get; protected set; }

		public string CondicaoEspecialX { get; protected set; }

		public decimal CondicaoEspecialN { get; protected set; }

		public bool IsPesavel { get; protected set; }

		public decimal Tara { get; protected set; }

		public string DataUltimaAlteracaoPrecoVendaToText
		{
			get
			{
				if (!(DataUltimaAlteracaoPrecoVenda.Date == DateTime.Now.Date))
				{
					if (!(DataUltimaAlteracaoPrecoVenda == DateTime.MinValue))
					{
						return $"Última alteração de preço em {HelperDate.FormatarData(DataUltimaAlteracaoPrecoVenda, (FormatoData)6)}";
					}
					return null;
				}
				return "Última alteração de preço HOJE";
			}
		}

		public decimal PrecoVendaTabelaPreco => TabelasPreco?.FirstOrDefault((KeyValuePair<string, decimal> x) => x.Key.Equals(IdTabelaPreco)).Value ?? 0m;

		public decimal PrecoVendaAgendado { get; protected set; }

		public decimal PrecoReferenciaAgendado { get; protected set; }

		public bool UtilizarPrecoAgendado { get; protected set; }

		public bool PossuiPrecoAgendado => PrecoVendaAgendado > 0m;

		private bool AplicarPrecoAgendado
		{
			get
			{
				if (UtilizarPrecoAgendado)
				{
					return PossuiPrecoAgendado;
				}
				return false;
			}
		}

		public string ToText => ((!string.IsNullOrEmpty(DescricaoGradeLinhaColuna)) ? DescricaoGradeLinhaColuna : Descricao) + ((!string.IsNullOrEmpty(Lote)) ? (" " + Lote) : string.Empty) + ((!string.IsNullOrEmpty(Serial)) ? (" " + Serial) : string.Empty);

		public string ToTextSegundaLinha => DataUltimaAlteracaoPrecoVendaToText;

		public decimal PrecoVendaCadastro
		{
			get
			{
				if (!AplicarPrecoAgendado)
				{
					return _precoVendaCadastro;
				}
				return PrecoVendaAgendado;
			}
		}

		public decimal PrecoVendaPromocional
		{
			get
			{
				if (!AplicarPrecoAgendado)
				{
					return _precoVendaPromocional;
				}
				return PrecoVendaAgendado;
			}
		}

		public decimal SomaQuantidadeDav
		{
			get
			{
				if (!(PesoLiquido > 0m))
				{
					return 1m;
				}
				return PesoLiquido;
			}
		}

		public decimal SomaQuantidadeBrutaDav
		{
			get
			{
				if (!(PesoBruto > 0m))
				{
					return 1m;
				}
				return PesoBruto;
			}
		}

		public DadoProduto(string codigoInterno, string codigoBarra, string descricao, string descricaoGradeLinhaColuna, decimal precoCusto, decimal precoVenda, string unidadeMedida, string caracteristica, string marca, string observacao, decimal pesoLiquido, decimal pesoBruto, string localizacao, string gradeLinha, string gradeColuna, string personalizado, decimal quantidadeEstoque, long quantidadeEtiquetas, decimal precoReferencia, string descricaoPrecoReferencia, DateTime dataUltimaCompra, string lote, string serial, DateTime dataFabricacaoLote, DateTime dataValidadeLote, DateTime dataConclusaoOrdemProducao, Dictionary<string, decimal> tabelasPreco, string idTabelaPreco, string condicaoEspecial, string link, DateTime dataUltimaAlteracaoPrecoVenda, string codigoBarrasTributavel, decimal nCondicaoEspecial, string xCondicaoEspecial, decimal precoVendaCadastro, decimal precoVendaPromocional, bool isPesavel, string produtoServicoEmpresaReferencia, decimal tara)
			: base(quantidadeEtiquetas)
		{
			CodigoInterno = codigoInterno;
			CodigoInternoFormatoCodigoBarra = CodigoInterno;
			CodigoBarra = codigoBarra;
			Descricao = descricao;
			DescricaoGradeLinhaColuna = descricaoGradeLinhaColuna;
			PrecoCusto = precoCusto;
			_precoVenda = precoVenda;
			UnidadeMedida = unidadeMedida;
			Caracteristica = caracteristica;
			Marca = marca;
			Observacao = observacao;
			PesoLiquido = pesoLiquido;
			PesoBruto = pesoBruto;
			Localizacao = localizacao;
			GradeLinha = gradeLinha;
			GradeColuna = gradeColuna;
			QuantidadeEstoque = quantidadeEstoque;
			Personalizado = personalizado;
			_precoReferencia = precoReferencia;
			DescricaoPrecoReferencia = descricaoPrecoReferencia;
			DataUltimaCompra = dataUltimaCompra;
			Lote = lote;
			Serial = serial;
			DataFabricacaoLote = dataFabricacaoLote;
			DataValidadeLote = dataValidadeLote;
			DataConclusaoOrdemProducao = dataConclusaoOrdemProducao;
			TabelasPreco = tabelasPreco;
			IdTabelaPreco = idTabelaPreco;
			CondicaoEspecial = condicaoEspecial;
			Link = link;
			DataUltimaAlteracaoPrecoVenda = dataUltimaAlteracaoPrecoVenda;
			CodigoBarrasTributavel = codigoBarrasTributavel;
			CondicaoEspecialN = nCondicaoEspecial;
			CondicaoEspecialX = xCondicaoEspecial;
			_precoVendaCadastro = precoVendaCadastro;
			_precoVendaPromocional = precoVendaPromocional;
			IsPesavel = isPesavel;
			ProdutoServicoEmpresaReferencia = produtoServicoEmpresaReferencia;
			Tara = tara;
		}

		public void SetarPrecoAgendado(decimal precoVendaAgendado, decimal precoReferenciaAgendado)
		{
			PrecoVendaAgendado = precoVendaAgendado;
			PrecoReferenciaAgendado = precoReferenciaAgendado;
		}

		public void SetarUtilizarPrecoAgendado(bool utilizarPrecoAgendado)
		{
			UtilizarPrecoAgendado = utilizarPrecoAgendado;
		}

		public void SetarIdTabelaPreco(string idTabelaPreco)
		{
			IdTabelaPreco = idTabelaPreco;
		}

		public void SetarPesoBruto(decimal pesoBruto)
		{
			PesoBruto = pesoBruto;
		}

		public void SetarPesoLiquido(decimal pesoLiquido)
		{
			PesoLiquido = pesoLiquido;
		}

		public void SetarTara(decimal tara)
		{
			Tara = tara;
		}
	}
	public class DadoProdutoFactory
	{
		private decimal _baseCalculoPrecoReferencia;

		private string _caracteristica;

		private string _codigoBarras;

		private string _codigoBarrasTributavel;

		private string _codigoInterno;

		private DateTime _dataUltimaCompra;

		private string _descricao;

		private string _descricaoGradeLinhaColuna;

		private string _descricaoPrecoReferencia;

		private long _etiquetasPorItem;

		private string _gradeColuna;

		private string _gradeLinha;

		private string _localizacao;

		private string _marca;

		private string _observacao;

		private decimal _pesoBruto;

		private decimal _pesoLiquido;

		private decimal _precoCusto;

		private decimal _precoVendaVigente;

		private decimal _precoVendaAgendado;

		private bool _utilizarPrecoAgendado;

		private decimal _quantidade;

		private string _condicaoEspecialX;

		private decimal _condicaoEspecialN;

		private string _siglaUnidadeMedida;

		private string _lote;

		private string _serial;

		private DateTime _dataFabricacaoLote;

		private DateTime _dataValidadeLote;

		private DateTime _dataConclusaoOrdemProducao;

		private string _idTabelaPreco;

		private Dictionary<string, decimal> _tabelasPreco;

		private string _condicaoEspecial;

		private string _link;

		private DateTime _dataUltimaAlteracaoPrecoVenda;

		private decimal _precoVendaCadastro;

		private decimal _precoVendaPromocional;

		private bool _isPesavel;

		private decimal _tara;

		private string _produtoServicoEmpresaReferencia;

		public DadoProdutoFactory SetarDescricaoGradeLinhaColuna(string descricaoGradeLinhaColuna)
		{
			_descricaoGradeLinhaColuna = descricaoGradeLinhaColuna;
			return this;
		}

		public DadoProdutoFactory SetarGradeLinha(string gradeLinha)
		{
			_gradeLinha = gradeLinha;
			return this;
		}

		public DadoProdutoFactory SetarGradeColuna(string gradeColuna)
		{
			_gradeColuna = gradeColuna;
			return this;
		}

		public DadoProdutoFactory SetarEtiquetasPorItem(long etiquetasPorItem)
		{
			_etiquetasPorItem = etiquetasPorItem;
			return this;
		}

		public DadoProdutoFactory SetarCodigoBarras(string codigoBarras)
		{
			_codigoBarras = codigoBarras;
			return this;
		}

		public DadoProdutoFactory SetarCodigoBarrasTributavel(string codigoBarrasTributavel)
		{
			_codigoBarrasTributavel = codigoBarrasTributavel;
			return this;
		}

		public DadoProdutoFactory SetarBaseCalculoPrecoReferencia(decimal precoReferencia)
		{
			_baseCalculoPrecoReferencia = ((precoReferencia > 0m) ? precoReferencia : 1m);
			return this;
		}

		public DadoProdutoFactory SetarDescricaoPrecoReferencia(string descricaoPrecoReferencia)
		{
			_descricaoPrecoReferencia = descricaoPrecoReferencia;
			return this;
		}

		public DadoProdutoFactory SetarPrecoCusto(decimal precoCusto)
		{
			_precoCusto = precoCusto;
			return this;
		}

		public DadoProdutoFactory SetarPrecoVendaVigente(decimal precoVendaVigente)
		{
			_precoVendaVigente = precoVendaVigente;
			return this;
		}

		public DadoProdutoFactory SetarPrecoVendaAgendado(decimal precoVendaAgendado)
		{
			_precoVendaAgendado = precoVendaAgendado;
			return this;
		}

		public DadoProdutoFactory SetarUtilizarPrecoAgendado(bool utilizarPrecoAgendado)
		{
			_utilizarPrecoAgendado = utilizarPrecoAgendado;
			return this;
		}

		public DadoProdutoFactory SetarLocalizacao(string localizacao)
		{
			_localizacao = localizacao;
			return this;
		}

		public DadoProdutoFactory SetarQuantidade(decimal quantidade)
		{
			_quantidade = quantidade;
			return this;
		}

		public DadoProdutoFactory SetarDataUltimaCompra(DateTime dataUltimaCompra)
		{
			_dataUltimaCompra = dataUltimaCompra;
			return this;
		}

		public DadoProdutoFactory SetarCodigoInterno(string codigoInterno)
		{
			_codigoInterno = codigoInterno;
			return this;
		}

		public DadoProdutoFactory SetarDescricao(string descricao)
		{
			_descricao = descricao;
			return this;
		}

		public DadoProdutoFactory SetarSiglaUnidadeMedida(string siglaUnidadeMedida)
		{
			_siglaUnidadeMedida = siglaUnidadeMedida;
			return this;
		}

		public DadoProdutoFactory SetarCaracteristica(string caracteristica)
		{
			_caracteristica = caracteristica;
			return this;
		}

		public DadoProdutoFactory SetarMarca(string marca)
		{
			_marca = marca;
			return this;
		}

		public DadoProdutoFactory SetarObservacao(string observacao)
		{
			_observacao = observacao;
			return this;
		}

		public DadoProdutoFactory SetarPesoLiquido(decimal pesoLiquido)
		{
			_pesoLiquido = pesoLiquido;
			return this;
		}

		public DadoProdutoFactory SetarPesoBruto(decimal pesoBruto)
		{
			_pesoBruto = pesoBruto;
			return this;
		}

		public DadoProdutoFactory SetarLote(string lote)
		{
			_lote = lote;
			return this;
		}

		public DadoProdutoFactory SetarSerial(string serial)
		{
			_serial = serial;
			return this;
		}

		public DadoProdutoFactory SetarDataFabricacaoLote(DateTime dataFabricacao)
		{
			_dataFabricacaoLote = dataFabricacao;
			return this;
		}

		public DadoProdutoFactory SetarDataValidadeLote(DateTime dataValidade)
		{
			_dataValidadeLote = dataValidade;
			return this;
		}

		public DadoProdutoFactory SetarDataConclusaoOrdemProducao(DateTime dataConclusaoOrdemProducao)
		{
			_dataConclusaoOrdemProducao = dataConclusaoOrdemProducao;
			return this;
		}

		public DadoProdutoFactory SetarIdTabelaPreco(string idTabelaPreco)
		{
			_idTabelaPreco = idTabelaPreco;
			return this;
		}

		public DadoProdutoFactory SetarTabelasPreco(Dictionary<string, decimal> tabelasPreco)
		{
			_tabelasPreco = tabelasPreco;
			return this;
		}

		public DadoProdutoFactory SetarCondicaoEspecial(string condicaoEspecial)
		{
			_condicaoEspecial = condicaoEspecial;
			return this;
		}

		public DadoProdutoFactory SetarLink(string linkQrCode)
		{
			_link = linkQrCode;
			return this;
		}

		public DadoProdutoFactory SetarDataUltimaAlteracaoPrecoVenda(DateTime dataUltimaAlteracaoPrecoVenda)
		{
			_dataUltimaAlteracaoPrecoVenda = dataUltimaAlteracaoPrecoVenda;
			return this;
		}

		public DadoProdutoFactory SetarCondicaoEspecialX(string condicaoEspecialX)
		{
			_condicaoEspecialX = condicaoEspecialX;
			return this;
		}

		public DadoProdutoFactory SetarCondicaoEspecialN(decimal condicaoEspecialN)
		{
			_condicaoEspecialN = condicaoEspecialN;
			return this;
		}

		public DadoProdutoFactory SetarPrecoVendaCadastro(decimal precoVendaCadastro)
		{
			_precoVendaCadastro = precoVendaCadastro;
			return this;
		}

		public DadoProdutoFactory SetarPrecoVendaPromocional(decimal precoVendaPromocional)
		{
			_precoVendaPromocional = precoVendaPromocional;
			return this;
		}

		public DadoProdutoFactory SetarIsPesavel(bool isPesavel)
		{
			_isPesavel = isPesavel;
			return this;
		}

		public DadoProdutoFactory SetarTara(decimal tara)
		{
			_tara = tara;
			return this;
		}

		public DadoProdutoFactory SetarProdutoServicoEmpresaReferencia(string produtoServicoEmpresaReferencia)
		{
			_produtoServicoEmpresaReferencia = produtoServicoEmpresaReferencia;
			return this;
		}

		public DadoProduto Criar()
		{
			DadoProduto dadoProduto = new DadoProduto(_codigoInterno, _codigoBarras, _descricao, HelperString.ToStringEmptyWhenNull(_descricaoGradeLinhaColuna), _precoCusto, _precoVendaVigente, _siglaUnidadeMedida, _caracteristica, _marca, _observacao, _pesoLiquido, _pesoBruto, HelperString.ToStringEmptyWhenNull(_localizacao), HelperString.ToStringEmptyWhenNull(_gradeLinha), HelperString.ToStringEmptyWhenNull(_gradeColuna), string.Empty, _quantidade, _etiquetasPorItem, _precoVendaVigente / _baseCalculoPrecoReferencia, HelperString.ToStringEmptyWhenNull(_descricaoPrecoReferencia), _dataUltimaCompra, _lote, _serial, _dataFabricacaoLote, _dataValidadeLote, _dataConclusaoOrdemProducao, _tabelasPreco, _idTabelaPreco, _condicaoEspecial, _link, _dataUltimaAlteracaoPrecoVenda, _codigoBarrasTributavel, _condicaoEspecialN, _condicaoEspecialX, _precoVendaCadastro, _precoVendaPromocional, _isPesavel, _produtoServicoEmpresaReferencia, _tara);
			dadoProduto.SetarPrecoAgendado(_precoVendaAgendado, _precoVendaAgendado / _baseCalculoPrecoReferencia);
			dadoProduto.SetarUtilizarPrecoAgendado(_utilizarPrecoAgendado);
			return dadoProduto;
		}

		public IEnumerable<DadoProduto> Criar(EstoqueDto estoqueDto)
		{
			List<DadoProduto> list = new List<DadoProduto>();
			if (HelperObject.Is<EstoqueDtoNull>((object)estoqueDto))
			{
				list.Add(Criar());
			}
			if (estoqueDto.IsNormal)
			{
				list.Add(SetarQuantidade(estoqueDto.Quantidade).Criar());
			}
			if (HelperObject.Is<EstoqueSerialDto>((object)estoqueDto))
			{
				list.AddRange(ProcessarEstoqueSerial((EstoqueSerialDto)(object)((estoqueDto is EstoqueSerialDto) ? estoqueDto : null)));
			}
			if (HelperObject.Is<EstoqueGradeDto>((object)estoqueDto))
			{
				list.AddRange(ProcessarEstoqueGrade((EstoqueGradeDto)(object)((estoqueDto is EstoqueGradeDto) ? estoqueDto : null)));
			}
			if (HelperObject.Is<EstoqueLoteDto>((object)estoqueDto))
			{
				list.AddRange(ProcessarEstoqueLote((EstoqueLoteDto)(object)((estoqueDto is EstoqueLoteDto) ? estoqueDto : null)));
			}
			return list;
		}

		private IEnumerable<DadoProduto> ProcessarEstoqueSerial(EstoqueSerialDto estoqueSerialDto)
		{
			List<DadoProduto> list = HelperIEnumerable.ToListEmptyWhenNull<DadoProduto>(estoqueSerialDto.Seriais.Select((SerialDto serial) => SetarSerial(serial.Codigo).SetarQuantidade(1m).Criar()));
			if (!list.Any())
			{
				list.Add(SetarSerial(string.Empty).SetarQuantidade(1m).Criar());
			}
			SetarSerial(string.Empty).SetarQuantidade(0m);
			return list;
		}

		private IEnumerable<DadoProduto> ProcessarEstoqueGrade(EstoqueGradeDto estoqueGradeDto)
		{
			List<DadoProduto> list = new List<DadoProduto>();
			foreach (GradeDto grade in estoqueGradeDto.Grades)
			{
				SetarDescricaoGradeLinhaColuna($"{_descricao} {grade.Linha.Descricao} {grade.Coluna.Descricao}").SetarGradeLinha(grade.Linha.Descricao).SetarGradeColuna(grade.Coluna.Descricao).SetarCodigoBarras(grade.CodigoBarras);
				if (grade.Estoque.IsNormal)
				{
					list.Add(SetarQuantidade(grade.Estoque.Quantidade).Criar());
					continue;
				}
				EstoqueDto estoque = grade.Estoque;
				list.AddRange(ProcessarEstoqueSerial((EstoqueSerialDto)(object)((estoque is EstoqueSerialDto) ? estoque : null)));
			}
			if (!list.Any())
			{
				list.Add(SetarDescricaoGradeLinhaColuna(string.Empty).SetarGradeLinha(string.Empty).SetarGradeColuna(string.Empty).SetarCodigoBarras(string.Empty)
					.SetarQuantidade(1m)
					.Criar());
			}
			SetarDescricaoGradeLinhaColuna(string.Empty).SetarGradeLinha(string.Empty).SetarGradeColuna(string.Empty).SetarCodigoBarras(string.Empty)
				.SetarQuantidade(0m);
			return list;
		}

		private IEnumerable<DadoProduto> ProcessarEstoqueLote(EstoqueLoteDto estoqueLoteDto)
		{
			List<DadoProduto> list = new List<DadoProduto>();
			foreach (LoteDto lote in estoqueLoteDto.Lotes)
			{
				SetarLote(lote.Numero).SetarDataFabricacaoLote(lote.DataFabricacao).SetarDataValidadeLote(lote.DataValidade);
				if (lote.Estoque.IsNormal)
				{
					list.Add(SetarQuantidade(lote.Estoque.Quantidade).Criar());
				}
				if (HelperObject.Is<EstoqueSerialDto>((object)lote.Estoque))
				{
					EstoqueDto estoque = lote.Estoque;
					list.AddRange(ProcessarEstoqueSerial((EstoqueSerialDto)(object)((estoque is EstoqueSerialDto) ? estoque : null)));
				}
				if (HelperObject.Is<EstoqueGradeDto>((object)lote.Estoque))
				{
					EstoqueDto estoque2 = lote.Estoque;
					list.AddRange(ProcessarEstoqueGrade((EstoqueGradeDto)(object)((estoque2 is EstoqueGradeDto) ? estoque2 : null)));
				}
			}
			if (!list.Any())
			{
				list.Add(SetarLote(string.Empty).SetarDataFabricacaoLote(DateTime.MinValue).SetarDataValidadeLote(DateTime.MinValue).SetarQuantidade(1m)
					.Criar());
			}
			SetarLote(string.Empty).SetarDataFabricacaoLote(DateTime.MinValue).SetarDataValidadeLote(DateTime.MinValue).SetarQuantidade(0m);
			return list;
		}
	}
	public abstract class DataViewModel : TextoViewModel
	{
		public Lista<FormatoDataHora> Formatos { get; set; }

		protected DataViewModel()
		{
		}

		protected DataViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			Formatos = new Lista<FormatoDataHora>();
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("d"), "d"));
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("D"), "D"));
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("MM/yyyy"), "MM/yyyy"));
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("MMMM/yyyy"), "MMMM/yyyy"));
			Formatos.Posicionar((Expression<Func<FormatoDataHora, bool>>)((FormatoDataHora x) => x.Valor.Equals(dto.FormatoDataHora)));
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.FormatoDataHora = Formatos.ItemSelecionado.Valor;
		}

		public override XRControl ObterXrControl()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			return (XRControl)(object)new CustomXRLabel(Titulo, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, base.Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade, "{0:" + Formatos.ItemSelecionado.Valor + "}"));
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Formatos).Dispose();
			base.Dispose();
		}
	}
	public class FormatoDataHora
	{
		public string Descricao { get; }

		public string Valor { get; }

		public FormatoDataHora(string descricao, string valor)
		{
			Descricao = descricao;
			Valor = valor;
		}
	}
	public abstract class HoraViewModel : TextoViewModel
	{
		public Lista<FormatoDataHora> Formatos { get; set; }

		protected HoraViewModel()
		{
		}

		protected HoraViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			Formatos = new Lista<FormatoDataHora>();
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("HH:mm:ss"), "HH:mm:ss"));
			Formatos.Adicionar(new FormatoDataHora(DateTime.Now.ToString("HH:mm"), "HH:mm"));
			Formatos.Posicionar((Expression<Func<FormatoDataHora, bool>>)((FormatoDataHora x) => x.Valor.Equals(dto.FormatoDataHora)));
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.FormatoDataHora = Formatos.ItemSelecionado.Valor;
		}

		public override XRControl ObterXrControl()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			return (XRControl)(object)new CustomXRLabel(Titulo, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, base.Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade, "{0:" + Formatos.ItemSelecionado.Valor + "}"));
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)Formatos).Dispose();
			base.Dispose();
		}
	}
	public interface IDado
	{
		Lista<IDado> DadosRelatorio { get; }

		void SetarQuantidadeEtiquetas(long quantidadeEtiquetas);
	}
	public class ImagemViewModel : CampoBaseViewModel
	{
		public override string Titulo => "Imagem";

		public override string NomePropriedade => "Imagem";

		public override int Codigo => 38;

		public BitmapSource Imagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Imagem));
			}
			set
			{
				((BindableBase)this).SetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => Imagem), value);
			}
		}

		public float Opacidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<float>((Expression<Func<float>>)(() => Opacidade));
			}
			set
			{
				((BindableBase)this).SetProperty<float>((Expression<Func<float>>)(() => Opacidade), value);
			}
		}

		public ImagemViewModel()
		{
		}

		public ImagemViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			byte[] imagem = dto.Imagem;
			Imagem = ((imagem != null) ? HelperImage.ToBitmapSource(HelperImage.Decompress(imagem)) : null);
			Opacidade = dto.Opacidade;
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ImagemViewModel(dto);
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.Imagem = HelperImage.Compress(Imagem);
			campoEtiquetaDto.Opacidade = Opacidade;
		}

		public override XRControl ObterXrControl()
		{
			return (XRControl)(object)new CustomBaseXRPictureBox(base.PosicaoX, base.PosicaoY, base.Largura, base.Altura, Opacidade, HelperImage.ToImage(Imagem));
		}
	}
	public abstract class MonetarioViewModel : NumericoViewModel
	{
		protected MonetarioViewModel()
		{
		}

		protected MonetarioViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override XRControl ObterXrControl()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected O, but got Unknown
			return (XRControl)(object)new CustomXRLabel(Titulo, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, base.Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade, "{0:C" + base.CasasDecimais + "}"));
		}
	}
	public class MovimentacaoItem : OrigemDado
	{
		public MovimentacaoItem()
			: base((OrigemDadosEtiqueta)new OrigemDadosMovimentacaoItem())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			base.Campos.Adicionar((CampoBaseViewModel)new NomeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TelefoneViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new NomeFantasiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DocumentoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EmailViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDataEmissaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoChaveAcessoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeTecnicoResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeVendedorResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDescricaoPrioridadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new HoraImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemQuantidadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemDescricaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemUnidadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemTotalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemTotalPesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemTotalPesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemCodigoInternoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemCodigoBarrasViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemCodigoBarrasTributavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoItemInformacaoEstoqueViewModel());
			base.Campos.Ordenar((Expression<Func<CampoBaseViewModel, object>>)((CampoBaseViewModel x) => x.Codigo), (Expression<Func<CampoBaseViewModel, object>>)null);
			base.Filtro = new MovimentacaoItemFiltroViewModel();
			base.DataExemplo.Adicionar((IDado)new DadoMovimentacaoItem("Nome da pessoa", "Telefone", "Endereço principal", "Número principal", "Bairro principal", "Cep principal", "Município principal", "Uf principal", "País principal", "Endereço entrega", "Número entrega", "Bairro entrega", "CEP entrega", "Município entrega", "Uf entrega", "País entrega", "Nome fantasia", "Responsável", "Documento CPF", "Documento CNPJ", "E-mail", 1L, "Observação", 1L, DateTime.Now, 1m, "Movimentação chave acesso", "Movimentação técnico responsável", "Movimentação vendedor responsável", "Movimentação prioridade", 0m, 0m, 1m, "Item descrição", "Unidade item", 1m, 1m, 1m, "Item observação", "Item código interno", "7891234567891", "7891234567891", "Lote, Grade, Serial"));
		}

		public override void ExecutarPesquisa(string informacao)
		{
			base.ListaPesquisa.Adicionar(_listaPesquisaTemporaria.Itens.Where((IDado x) => HelperString.RemoveDiacritics(((DadoMovimentacaoItem)x).Nome).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower())));
		}
	}
	public class MovimentacaoObjeto : OrigemDado
	{
		public MovimentacaoObjeto()
			: base((OrigemDadosEtiqueta)new OrigemDadosMovimentacao())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			base.Campos.Adicionar((CampoBaseViewModel)new NomeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TelefoneViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new NomeFantasiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DocumentoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EmailViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoDescricaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoSerialViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoPlacaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoChassiViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoModeloViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoMarcaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoRenavamViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoCorViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoAnoFabricacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DefeitoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DiagnosticoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataGarantiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDataEmissaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoChaveAcessoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeTecnicoResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeVendedorResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDescricaoPrioridadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new HoraImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoCilindradaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoPotenciaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoMotorViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObjetoSegmentoViewModel());
			base.Campos.Ordenar((Expression<Func<CampoBaseViewModel, object>>)((CampoBaseViewModel x) => x.Codigo), (Expression<Func<CampoBaseViewModel, object>>)null);
			base.Filtro = new MovimentacaoObjetoFiltroViewModel();
			base.DataExemplo.Adicionar((IDado)new DadoMovimentacaoObjeto("Nome da pessoa", "Telefone", "Endereço principal", "Número principal", "Bairro principal", "Cep principal", "Município principal", "Uf principal", "País principal", "Endereço entrega", "Número entrega", "Bairro entrega", "CEP entrega", "Município entrega", "Uf entrega", "País entrega", "Nome fantasia", "Responsável", "Documento CPF", "Documento CNPJ", "E-mail", 1L, "Observação", "Objeto descrição", "Objeto serial", "Objeto placa", "Objeto Chassi", "Objeto modelo", "Objeto marca", "Objeto renavam", "Objeto cor", DateTime.Now.Year, "Objeto defeito", "Objeto diagnóstico", DateTime.Now, 1L, DateTime.Now, 1m, "Movimentação chave acesso", "Movimentação técnico responsável", "Movimentação vendedor responsável", "Movimentação prioridade", 0m, 0m, "Objeto Cilindrada", "Objeto Potência", "Objeto Motor", "Objeto Segmento"));
		}

		public override void ExecutarPesquisa(string informacao)
		{
			base.ListaPesquisa.Adicionar(_listaPesquisaTemporaria.Itens.Where((IDado x) => HelperString.RemoveDiacritics(((DadoMovimentacaoObjeto)x).Nome).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower())));
		}
	}
	public class MovimentacaoVolume : OrigemDado
	{
		public MovimentacaoVolume()
			: base((OrigemDadosEtiqueta)new OrigemDadosMovimentacaoVolume())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			base.Campos.Adicionar((CampoBaseViewModel)new NomeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TelefoneViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new NomeFantasiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DocumentoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EmailViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumeQuantidadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumeEspecieViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumeMarcaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumeNumeracaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumePesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumePesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDataEmissaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoChaveAcessoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeTecnicoResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoNomeVendedorResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoDescricaoPrioridadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MovimentacaoTotalPesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new HoraImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TransportadoraNomeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TransportadoraNomeFantasiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TransportadoraDocumentoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new VolumeContadorViewModel());
			base.Campos.Ordenar((Expression<Func<CampoBaseViewModel, object>>)((CampoBaseViewModel x) => x.Codigo), (Expression<Func<CampoBaseViewModel, object>>)null);
			base.Filtro = new MovimentacaoVolumeFiltroViewModel();
			base.DataExemplo.Adicionar((IDado)new DadoMovimentacaoVolume("Nome da pessoa", "Telefone", "Endereço principal", "Número principal", "Bairro principal", "Cep principal", "Município principal", "Uf principal", "País principal", "Endereço entrega", "Número entrega", "Bairro entrega", "CEP entrega", "Município entrega", "Uf entrega", "País entrega", "Nome fantasia", "Responsável", "Documento CPF", "Documento CNPJ", "E-mail", 1L, "Observação", 1, "Volume espécie", "Volume marca", "Volume Numeração", 1m, 1m, 1L, DateTime.Now, 1m, "Movimentação chave acesso", "Movimentação técnico responsável", "Movimentação vendedor responsável", "Movimentação prioridade", 0m, 0m, "Transportadora nome", "Transportadora nome fantasia", "Transportadora documento", "1/1"));
		}

		public override void ExecutarPesquisa(string informacao)
		{
			base.ListaPesquisa.Adicionar(_listaPesquisaTemporaria.Itens.Where((IDado x) => HelperString.RemoveDiacritics(((DadoMovimentacaoVolume)x).Nome).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower())));
		}
	}
	public class Normal : OrientacaoCodigoBarrasBase
	{
		public Normal()
			: base((BarCodeOrientation)0, "Normal")
		{
		}
	}
	public abstract class NumericoViewModel : TextoViewModel
	{
		public int CasasDecimais
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => CasasDecimais));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => CasasDecimais), value);
			}
		}

		protected NumericoViewModel()
		{
		}

		protected NumericoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			CasasDecimais = dto.CasasDecimais;
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.CasasDecimais = CasasDecimais;
		}

		public override XRControl ObterXrControl()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected O, but got Unknown
			return (XRControl)(object)new CustomXRLabel(Titulo, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, base.Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade, "{0:N" + CasasDecimais + "}"));
		}
	}
	public class OrientacaoCodigoBarrasBase
	{
		public BarCodeOrientation Orientacao { get; protected set; }

		public string OrientacaoTela { get; protected set; }

		public OrientacaoCodigoBarrasBase(BarCodeOrientation orientacao, string orientacaoTela)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Orientacao = orientacao;
			OrientacaoTela = orientacaoTela;
		}
	}
	public class OrientacaoCodigoBarrasComboBoxViewModel : Lista<OrientacaoCodigoBarrasBase>
	{
		public OrientacaoCodigoBarrasComboBoxViewModel(string nome)
		{
			base.Adicionar((OrientacaoCodigoBarrasBase)new Normal());
			base.Adicionar((OrientacaoCodigoBarrasBase)new RotacaoEsquerda());
			base.Adicionar((OrientacaoCodigoBarrasBase)new RotacaoDireita());
			base.Adicionar((OrientacaoCodigoBarrasBase)new CabecaParaBaixo());
			if (string.IsNullOrEmpty(nome))
			{
				base.PosicionarNoPrimeiro();
				return;
			}
			base.Posicionar((Expression<Func<OrientacaoCodigoBarrasBase, bool>>)((OrientacaoCodigoBarrasBase x) => x.OrientacaoTela == nome));
		}
	}
	public abstract class OrigemDado : BindableBase
	{
		protected Lista<IDado> _listaPesquisaTemporaria;

		private readonly OrigemDadosEtiqueta _origemDado;

		public int Codigo => ((CodigoDescricao)_origemDado).Codigo;

		public string Descricao => ((CodigoDescricao)_origemDado).Descricao;

		public int NumeroEtiquetasIgnoradas
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => NumeroEtiquetasIgnoradas));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => NumeroEtiquetasIgnoradas), value);
			}
		}

		public Lista<CampoBaseViewModel> Campos { get; protected set; } = new Lista<CampoBaseViewModel>();


		public FiltroViewModel Filtro { get; protected set; }

		public ICommand PesquisarCommand { get; set; }

		public ICommand RemoverCommand { get; set; }

		public Lista<IDado> ListaPesquisa { get; protected set; } = new Lista<IDado>();


		public Lista<IDado> DataExemplo { get; protected set; } = new Lista<IDado>();


		protected OrigemDado(OrigemDadosEtiqueta origemDadosEtiqueta)
		{
			_origemDado = origemDadosEtiqueta;
			Campos.Adicionar((CampoBaseViewModel)new PersonalizadoViewModel());
			Campos.Adicionar((CampoBaseViewModel)new ImagemViewModel());
			RemoverCommand = (ICommand)new DelegateCommand<object>((Action<object>)Remover);
			PesquisarCommand = (ICommand)new DelegateCommand<string>((Action<string>)Pesquisar);
			_listaPesquisaTemporaria = new Lista<IDado>();
		}

		public IEnumerable<IDado> ObterDadosParaImpressao()
		{
			List<IDado> list = HelperIEnumerable.ToListEmptyWhenNull<IDado>(ListaPesquisa.Itens.SelectMany((IDado x) => x.DadosRelatorio.Itens));
			for (int i = 0; i < NumeroEtiquetasIgnoradas; i++)
			{
				list.Insert(i, new DadoBranco(0));
			}
			return list;
		}

		private void Remover(object dado)
		{
			_listaPesquisaTemporaria.Remover(dado as IDado);
			ListaPesquisa.Limpar();
			ListaPesquisa.Adicionar((IEnumerable<IDado>)_listaPesquisaTemporaria.Itens);
			ListaPesquisa.PosicionarNoProximo();
		}

		public void AdicionarData()
		{
			if (!((ViewModelValidationBase)((Lista<Filtro>)Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios).Valido)
			{
				return;
			}
			foreach (IDado iten in ((Lista<Filtro>)Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios.Itens)
			{
				ListaPesquisa.Adicionar(iten);
			}
			((Lista<Filtro>)Filtro.TipoComboBox).ItemSelecionado.ViewModel.Limpar();
			_listaPesquisaTemporaria.Limpar();
			_listaPesquisaTemporaria.Adicionar((IEnumerable<IDado>)ListaPesquisa.Itens);
		}

		public void Pesquisar(string informacao)
		{
			ListaPesquisa.Limpar();
			if (string.IsNullOrEmpty(informacao))
			{
				ListaPesquisa.Adicionar((IEnumerable<IDado>)_listaPesquisaTemporaria.Itens);
			}
			else
			{
				ExecutarPesquisa(informacao);
			}
		}

		public abstract void ExecutarPesquisa(string informacao);

		public void Limpar()
		{
			ListaPesquisa.Limpar();
			NumeroEtiquetasIgnoradas = 0;
		}
	}
	public class PadraoBarrasComboBoxViewModel : Lista<BarCodeGeneratorBase>
	{
		public PadraoBarrasComboBoxViewModel(string nome)
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Expected O, but got Unknown
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Expected O, but got Unknown
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Expected O, but got Unknown
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Expected O, but got Unknown
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Expected O, but got Unknown
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Expected O, but got Unknown
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Expected O, but got Unknown
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Expected O, but got Unknown
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Expected O, but got Unknown
			//IL_0123: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Expected O, but got Unknown
			base.Adicionar((BarCodeGeneratorBase)new EAN13Generator());
			base.Adicionar((BarCodeGeneratorBase)new Code11Generator());
			base.Adicionar((BarCodeGeneratorBase)new Code128Generator());
			base.Adicionar((BarCodeGeneratorBase)new Code39Generator());
			base.Adicionar((BarCodeGeneratorBase)new Code39ExtendedGenerator());
			base.Adicionar((BarCodeGeneratorBase)new Code93Generator());
			base.Adicionar((BarCodeGeneratorBase)new Code93ExtendedGenerator());
			base.Adicionar((BarCodeGeneratorBase)new CodeMSIGenerator());
			base.Adicionar((BarCodeGeneratorBase)new CodabarGenerator());
			base.Adicionar((BarCodeGeneratorBase)new DataMatrixGenerator());
			base.Adicionar((BarCodeGeneratorBase)new DataMatrixGS1Generator());
			base.Adicionar((BarCodeGeneratorBase)new EAN128Generator());
			base.Adicionar((BarCodeGeneratorBase)new EAN8Generator());
			base.Adicionar((BarCodeGeneratorBase)new Industrial2of5Generator());
			base.Adicionar((BarCodeGeneratorBase)new IntelligentMailGenerator());
			base.Adicionar((BarCodeGeneratorBase)new Interleaved2of5Generator());
			base.Adicionar((BarCodeGeneratorBase)new Matrix2of5Generator());
			base.Adicionar((BarCodeGeneratorBase)new PDF417Generator());
			base.Adicionar((BarCodeGeneratorBase)new PostNetGenerator());
			base.Adicionar((BarCodeGeneratorBase)new QRCodeGenerator
			{
				CompactionMode = (QRCodeCompactionMode)2
			});
			base.Adicionar((BarCodeGeneratorBase)new UPCAGenerator());
			base.Adicionar((BarCodeGeneratorBase)new UPCE0Generator());
			base.Adicionar((BarCodeGeneratorBase)new UPCE1Generator());
			base.Adicionar((BarCodeGeneratorBase)new UPCSupplemental2Generator());
			base.Adicionar((BarCodeGeneratorBase)new UPCSupplemental5Generator());
			if (string.IsNullOrEmpty(nome))
			{
				base.PosicionarNoPrimeiro();
				return;
			}
			base.Posicionar((Expression<Func<BarCodeGeneratorBase, bool>>)((BarCodeGeneratorBase x) => x.Name == nome));
		}
	}
	public class PersonalizadoViewModel : TextoViewModel
	{
		private CustomBaseXRLabel _controle;

		public override string Titulo => $"Personalizado {Texto}";

		public override string NomePropriedade => "Personalizado";

		public override int Codigo => 35;

		public string Texto
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Texto));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Texto), value);
			}
		}

		public PersonalizadoViewModel()
		{
		}

		public PersonalizadoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			Texto = dto.Texto;
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PersonalizadoViewModel(dto);
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			base.AtualizarDto(campoEtiquetaDto);
			campoEtiquetaDto.Texto = Texto;
		}

		public override XRControl ObterXrControl()
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			_controle = new CustomBaseXRLabel(Texto, base.PosicaoX, base.PosicaoY, base.Negrito, base.Fonte, base.TamanhoFonte, base.Cor, base.QuebrarLinha, base.Largura, base.Altura, base.Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)base.AlinhamentoTexto).ItemSelecionado.Alinhamento);
			((XRControl)_controle).BeforePrint += ControleOnBeforePrint;
			return (XRControl)(object)_controle;
		}

		private void ControleOnBeforePrint(object sender, PrintEventArgs printEventArgs)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			DadoBranco dadoBranco = ((XRControl)sender).Report.GetCurrentRow() as DadoBranco;
			((XRControl)sender).Text = ((dadoBranco != null) ? string.Empty : Texto);
		}

		public override void Dispose()
		{
			((XRControl)_controle).BeforePrint -= ControleOnBeforePrint;
			base.Dispose();
		}
	}
	public class Pessoa : OrigemDado
	{
		public Pessoa()
			: base((OrigemDadosEtiqueta)new OrigemDadosPessoa())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			base.Campos.Adicionar((CampoBaseViewModel)new NomeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new TelefoneViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoPrincipalPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaNumeroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaBairroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaCepViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaMunicipioViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaUfViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EnderecoEntregaPaisViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new NomeFantasiaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ResponsavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DocumentoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new EmailViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new HoraImpressaoViewModel());
			base.Campos.Ordenar((Expression<Func<CampoBaseViewModel, object>>)((CampoBaseViewModel x) => x.Codigo), (Expression<Func<CampoBaseViewModel, object>>)null);
			base.Filtro = new PessoaFiltroViewModel();
			base.DataExemplo.Adicionar((IDado)new DadoPessoa("Nome", "Telefone", "Endereço principal", "Número principal", "Bairro principal", "Cep principal", "Município principal", "Uf principal", "País principal", "Endereço entrega", "Número entrega", "Bairro entrega", "CEP entrega", "Município entrega", "Uf entrega", "País entrega", "Nome fantasia", "Responsável", "Documento CPF", "Documento CNPJ", "E-mail", 1L, "Observação"));
		}

		public override void ExecutarPesquisa(string informacao)
		{
			base.ListaPesquisa.Adicionar(_listaPesquisaTemporaria.Itens.Where((IDado x) => HelperString.RemoveDiacritics(((DadoPessoa)x).Nome).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower())));
		}
	}
	public class Polegada
	{
		private const decimal valorCentimetroEmPolegadas = 39.3809524m;

		public float Valor { get; protected set; }

		public Polegada(decimal centimetros)
		{
			Valor = (float)(centimetros * 39.3809524m);
		}

		public int ToInt()
		{
			return (int)Valor;
		}
	}
	public class Produto : OrigemDado
	{
		public Produto()
			: base((OrigemDadosEtiqueta)new OrigemDadosProduto())
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			base.Campos.Adicionar((CampoBaseViewModel)new CodigoInternoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CodigoInternoFormatoCodigoBarraViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CodigoBarrasViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DescricaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DescricaoGradeLinhaColunaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new QuantidadeViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PrecoCustoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PrecoVendaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new UnidadeMedidaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CaracteristicaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new MarcaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ObservacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PesoLiquidoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PesoBrutoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new GradeLinhaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new GradeColunaViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new ReferenciaPrecoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DescricaoReferenciaPrecoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataUltimaCompraViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new LocalizacaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new HoraImpressaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new LoteViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new SerialViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataFabricacaoLoteViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataValidadeLoteViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new DataConclusaoOrdemProducaoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PrecoVendaTabelaPrecoViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CondicaoEspecialViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new LinkViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CodigoBarrasTributavelViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CondicaoEspecialNViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new CondicaoEspecialXViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PrecoVendaCadastroViewModel());
			base.Campos.Adicionar((CampoBaseViewModel)new PrecoVendaPromocionalViewModel());
			base.Campos.Ordenar((Expression<Func<CampoBaseViewModel, object>>)((CampoBaseViewModel x) => x.Codigo), (Expression<Func<CampoBaseViewModel, object>>)null);
			base.Filtro = new ProdutoFiltroViewModel();
			base.DataExemplo.Adicionar((IDado)new DadoProduto("1", "7891234567891", "Teste", "Teste completo", 1m, 2m, "UND", "Caracteristica", "Marca", "Observação", 4m, 5m, "Localização", "Grade linha", "Grade coluna", "Personalizado", 1m, 1L, 2m, "Preço por quilograma", DateTime.Now, "Lote 1", "Serial ABC", DateTime.Now, DateTime.Now, DateTime.Now, new Dictionary<string, decimal>(), string.Empty, "Na venda de 'N' ou mais itens, o preço de todos será 'X'", "https://www.digisat.com.br/", DateTime.Now, "7891234567891", 3m, "R$ 3,50", 2m, 1.8m, isPesavel: false, string.Empty, 0m));
		}

		public override void ExecutarPesquisa(string informacao)
		{
			base.ListaPesquisa.Adicionar(_listaPesquisaTemporaria.Itens.Where(delegate(IDado x)
			{
				if (!HelperString.RemoveDiacritics(((DadoProduto)x).ToText).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower()))
				{
					string toTextSegundaLinha = ((DadoProduto)x).ToTextSegundaLinha;
					if (toTextSegundaLinha == null)
					{
						return false;
					}
					return HelperString.RemoveDiacritics(toTextSegundaLinha).ToLower().Contains(HelperString.RemoveDiacritics(informacao).ToLower());
				}
				return true;
			}));
		}
	}
	public class RotacaoDireita : OrientacaoCodigoBarrasBase
	{
		public RotacaoDireita()
			: base((BarCodeOrientation)3, "Rotacionado para a direita")
		{
		}
	}
	public class RotacaoEsquerda : OrientacaoCodigoBarrasBase
	{
		public RotacaoEsquerda()
			: base((BarCodeOrientation)2, "Rotacionado para a esquerda")
		{
		}
	}
	public abstract class TextoViewModel : CampoBaseViewModel
	{
		public string Fonte
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Fonte));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Fonte), value);
			}
		}

		public decimal TamanhoFonte
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TamanhoFonte));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TamanhoFonte), value);
			}
		}

		public string Cor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cor), value);
			}
		}

		public bool Negrito
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Negrito));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Negrito), value);
			}
		}

		public bool QuebrarLinha
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => QuebrarLinha));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => QuebrarLinha), value);
			}
		}

		public int Angulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => Angulo));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => Angulo), value);
			}
		}

		public AlinhamentoTextoComboBoxViewModel AlinhamentoTexto { get; set; }

		protected TextoViewModel()
		{
		}

		protected TextoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
			Fonte = dto.Fonte;
			TamanhoFonte = dto.TamanhoFonte;
			Cor = dto.Cor;
			Negrito = dto.Negrito;
			QuebrarLinha = dto.QuebrarLinha;
			Angulo = dto.Angulo;
			AlinhamentoTexto = new AlinhamentoTextoComboBoxViewModel(dto.CodigoAlinhamento);
		}

		public override void AtualizarDto(CampoEtiquetaDto campoEtiquetaDto)
		{
			campoEtiquetaDto.Fonte = Fonte;
			campoEtiquetaDto.CodigoCampo = Codigo;
			campoEtiquetaDto.Angulo = Angulo;
			campoEtiquetaDto.Cor = Cor;
			campoEtiquetaDto.TamanhoFonte = TamanhoFonte;
			campoEtiquetaDto.CodigoAlinhamento = ((Lista<AlinhamentoTexto>)AlinhamentoTexto).ItemSelecionado.Codigo;
			campoEtiquetaDto.Negrito = Negrito;
			campoEtiquetaDto.QuebrarLinha = QuebrarLinha;
			base.AtualizarDto(campoEtiquetaDto);
		}

		public override XRControl ObterXrControl()
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			return (XRControl)(object)new CustomXRLabel(Titulo, base.PosicaoX, base.PosicaoY, Negrito, Fonte, TamanhoFonte, Cor, QuebrarLinha, base.Largura, base.Altura, Angulo, NomePropriedade, ((Lista<AlinhamentoTexto>)AlinhamentoTexto).ItemSelecionado.Alinhamento, new XRBinding("Text", (object)null, NomePropriedade));
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)AlinhamentoTexto).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public static class TiposTexto
	{
		public const int Descricao = 1;

		public const int CodigoInterno = 2;

		public const int Caracteristica = 3;

		public const int UnidadeMedida = 4;

		public const int Marca = 5;

		public const int Observacao = 6;

		public const int Localizacao = 7;

		public const int GradeLinha = 8;

		public const int GradeColuna = 9;

		public const int DescricaoGradeLinhaColuna = 10;

		public const int Quantidade = 11;

		public const int PrecoCusto = 12;

		public const int PrecoVenda = 13;

		public const int PesoLiquido = 14;

		public const int PesoBruto = 15;

		public const int CodigoBarras = 16;

		public const int Nome = 17;

		public const int Telefone = 18;

		public const int EnderecoPrincipal = 19;

		public const int EnderecoPrincipalBairro = 20;

		public const int EnderecoPrincipalCep = 21;

		public const int EnderecoPrincipalMunicipio = 22;

		public const int EnderecoPrincipalUf = 23;

		public const int EnderecoPrincipalPais = 24;

		public const int EnderecoEntrega = 25;

		public const int EnderecoEntregaBairro = 26;

		public const int EnderecoEntregaCep = 27;

		public const int EnderecoEntregaMunicipio = 28;

		public const int EnderecoEntregaUf = 29;

		public const int EnderecoEntregaPais = 30;

		public const int NomeFantasia = 31;

		public const int Responsavel = 32;

		public const int Documento = 33;

		public const int Email = 34;

		public const int Personalizado = 35;

		public const int PrecoReferencia = 36;

		public const int DescricaoPrecoReferencia = 37;

		public const int Imagem = 38;

		public const int DataUltimaCompra = 39;

		public const int CodigoInternoFormatoCodigoBarra = 40;

		public const int EnderecoPrincipalNumero = 41;

		public const int EnderecoEntregaNumero = 42;

		public const int DataImpressao = 43;

		public const int HoraImpressao = 44;

		public const int Lote = 45;

		public const int Serial = 46;

		public const int DataFabricacaoLote = 47;

		public const int DataValidadeLote = 48;

		public const int DataConclusaoOrdemProducao = 49;

		public const int PrecoVendaTabelaPreco = 50;

		public const int CondicaoEspecial = 51;

		public const int ObjetoDescricao = 52;

		public const int ObjetoSerial = 53;

		public const int ObjetoPlaca = 54;

		public const int ObjetoChassi = 55;

		public const int ObjetoModelo = 56;

		public const int ObjetoMarca = 57;

		public const int ObjetoRenavam = 58;

		public const int ObjetoCor = 59;

		public const int ObjetoAnoFabricacao = 60;

		public const int Defeito = 61;

		public const int Diagnostico = 62;

		public const int DataGarantia = 63;

		public const int MovimentacaoNumero = 64;

		public const int MovimentacaoDataEmissao = 65;

		public const int MovimentacaoTotal = 66;

		public const int MovimentacaoChaveAcesso = 67;

		public const int MovimentacaoNomeTecnicoResponsavel = 68;

		public const int MovimentacaoNomeVendedorResponsavel = 69;

		public const int MovimentacaoDescricaoPrioridade = 70;

		public const int MovimentacaoTotalPesoBruto = 71;

		public const int MovimentacaoTotalPesoLiquido = 72;

		public const int VolumeQuantidade = 73;

		public const int VolumeMarca = 74;

		public const int VolumeEspecie = 75;

		public const int VolumeNumeracao = 76;

		public const int VolumePesoLiquido = 77;

		public const int VolumePesoBruto = 78;

		public const int TransportadoraNome = 79;

		public const int TransportadoraNomeFantasia = 80;

		public const int TransportadoraDocumento = 81;

		public const int MovimentacaoItemQuantidade = 82;

		public const int MovimentacaoItemDescricao = 83;

		public const int MovimentacaoItemTotal = 84;

		public const int MovimentacaoItemUnidade = 85;

		public const int MovimentacaoItemTotalPesoLiquido = 86;

		public const int MovimentacaoItemTotalPesoBruto = 87;

		public const int MovimentacaoItemObservacao = 88;

		public const int MovimentacaoItemCodigoInterno = 89;

		public const int MovimentacaoItemCodigoBarras = 90;

		public const int Link = 91;

		public const int CodigoBarrasTributavel = 92;

		public const int MovimentacaoItemCodigoBarrasTributavel = 93;

		public const int VolumeContador = 94;

		public const int MovimentacaoItemInformacaoEstoque = 95;

		public const int CondicaoEspecialN = 96;

		public const int CondicaoEspecialX = 97;

		public const int PrecoVendaCadastro = 98;

		public const int PrecoVendaPromocional = 99;

		public const int ObjetoCilindrada = 100;

		public const int ObjetoPotencia = 101;

		public const int ObjetoMotor = 102;

		public const int ObjetoSegmento = 103;
	}
}
namespace Otus.Modulo.Etiquetas.ViewModels.Tipos.Campos
{
	public class CaracteristicaViewModel : TextoViewModel
	{
		public override string Titulo => "Característica";

		public override string NomePropriedade => "Caracteristica";

		public override int Codigo => 3;

		public CaracteristicaViewModel()
		{
		}

		public CaracteristicaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CaracteristicaViewModel(dto);
		}
	}
	public class CodigoBarrasTributavelViewModel : BarrasViewModel
	{
		public override string Titulo => "Código de barras tributável";

		public override string NomePropriedade => "CodigoBarrasTributavel";

		public override int Codigo => 92;

		public CodigoBarrasTributavelViewModel()
		{
		}

		public CodigoBarrasTributavelViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CodigoBarrasTributavelViewModel(dto);
		}
	}
	public class CodigoBarrasViewModel : BarrasViewModel
	{
		public override string Titulo => "Código de barras";

		public override string NomePropriedade => "CodigoBarra";

		public override int Codigo => 16;

		public CodigoBarrasViewModel()
		{
		}

		public CodigoBarrasViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CodigoBarrasViewModel(dto);
		}
	}
	public class CodigoInternoFormatoCodigoBarraViewModel : BarrasViewModel
	{
		public override string Titulo => "Código interno (Formato de código de barras)";

		public override string NomePropriedade => "CodigoInternoFormatoCodigoBarra";

		public override int Codigo => 40;

		public CodigoInternoFormatoCodigoBarraViewModel()
		{
		}

		public CodigoInternoFormatoCodigoBarraViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CodigoInternoFormatoCodigoBarraViewModel(dto);
		}
	}
	public class CodigoInternoViewModel : TextoViewModel
	{
		public override string Titulo => "Código interno";

		public override string NomePropriedade => "CodigoInterno";

		public override int Codigo => 2;

		public CodigoInternoViewModel()
		{
		}

		public CodigoInternoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CodigoInternoViewModel(dto);
		}
	}
	public class CondicaoEspecialNViewModel : TextoViewModel
	{
		public override string Titulo => "Condição especial N";

		public override string NomePropriedade => "CondicaoEspecialN";

		public override int Codigo => 96;

		public CondicaoEspecialNViewModel()
		{
		}

		public CondicaoEspecialNViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CondicaoEspecialNViewModel(dto);
		}
	}
	public class CondicaoEspecialXViewModel : TextoViewModel
	{
		public override string Titulo => "Condição especial X";

		public override string NomePropriedade => "CondicaoEspecialX";

		public override int Codigo => 97;

		public CondicaoEspecialXViewModel()
		{
		}

		public CondicaoEspecialXViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CondicaoEspecialXViewModel(dto);
		}
	}
	public class DataConclusaoOrdemProducaoViewModel : DataViewModel
	{
		public override string Titulo => "Data de conclusão da ordem de produção";

		public override string NomePropriedade => "DataConclusaoOrdemProducao";

		public override int Codigo => 49;

		public DataConclusaoOrdemProducaoViewModel()
		{
		}

		public DataConclusaoOrdemProducaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataConclusaoOrdemProducaoViewModel(dto);
		}
	}
	public class DataFabricacaoLoteViewModel : DataViewModel
	{
		public override string Titulo => "Data de fabricação do lote";

		public override string NomePropriedade => "DataFabricacaoLote";

		public override int Codigo => 47;

		public DataFabricacaoLoteViewModel()
		{
		}

		public DataFabricacaoLoteViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataFabricacaoLoteViewModel(dto);
		}
	}
	public class DataGarantiaViewModel : DataViewModel
	{
		public override string Titulo => "Data de garantia do objeto";

		public override string NomePropriedade => "DataGarantia";

		public override int Codigo => 63;

		public DataGarantiaViewModel()
		{
		}

		public DataGarantiaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataGarantiaViewModel(dto);
		}
	}
	public class DataImpressaoViewModel : DataViewModel
	{
		public override string Titulo => "Data de impressão";

		public override string NomePropriedade => "DataImpressao";

		public override int Codigo => 43;

		public DataImpressaoViewModel()
		{
		}

		public DataImpressaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataImpressaoViewModel(dto);
		}
	}
	public class DataUltimaCompraViewModel : DataViewModel
	{
		public override string Titulo => "Data última compra";

		public override string NomePropriedade => "DataUltimaCompra";

		public override int Codigo => 39;

		public DataUltimaCompraViewModel()
		{
		}

		public DataUltimaCompraViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataUltimaCompraViewModel(dto);
		}
	}
	public class DataValidadeLoteViewModel : DataViewModel
	{
		public override string Titulo => "Data de validade do lote";

		public override string NomePropriedade => "DataValidadeLote";

		public override int Codigo => 48;

		public DataValidadeLoteViewModel()
		{
		}

		public DataValidadeLoteViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DataValidadeLoteViewModel(dto);
		}
	}
	public class DefeitoViewModel : TextoViewModel
	{
		public override string Titulo => "Defeito do objeto";

		public override string NomePropriedade => "Defeito";

		public override int Codigo => 61;

		public DefeitoViewModel()
		{
		}

		public DefeitoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DefeitoViewModel(dto);
		}
	}
	public class DescricaoGradeLinhaColunaViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição + grade linha + grade coluna";

		public override string NomePropriedade => "DescricaoGradeLinhaColuna";

		public override int Codigo => 10;

		public DescricaoGradeLinhaColunaViewModel()
		{
		}

		public DescricaoGradeLinhaColunaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DescricaoGradeLinhaColunaViewModel(dto);
		}
	}
	public class DescricaoReferenciaPrecoViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição da referência do preço de venda";

		public override string NomePropriedade => "DescricaoPrecoReferencia";

		public override int Codigo => 37;

		public DescricaoReferenciaPrecoViewModel()
		{
		}

		public DescricaoReferenciaPrecoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DescricaoReferenciaPrecoViewModel(dto);
		}
	}
	public class DiagnosticoViewModel : TextoViewModel
	{
		public override string Titulo => "Diagnóstico do objeto";

		public override string NomePropriedade => "Diagnostico";

		public override int Codigo => 62;

		public DiagnosticoViewModel()
		{
		}

		public DiagnosticoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DiagnosticoViewModel(dto);
		}
	}
	public class DocumentoViewModel : TextoViewModel
	{
		public override string Titulo => "Documento";

		public override string NomePropriedade => "Documento";

		public override int Codigo => 33;

		public DocumentoViewModel()
		{
		}

		public DocumentoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DocumentoViewModel(dto);
		}
	}
	public class EmailViewModel : TextoViewModel
	{
		public override string Titulo => "E-mail";

		public override string NomePropriedade => "Email";

		public override int Codigo => 34;

		public EmailViewModel()
		{
		}

		public EmailViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EmailViewModel(dto);
		}
	}
	public class EnderecoEntregaBairroViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega bairro";

		public override string NomePropriedade => "EnderecoEntregaBairro";

		public override int Codigo => 26;

		public EnderecoEntregaBairroViewModel()
		{
		}

		public EnderecoEntregaBairroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaBairroViewModel(dto);
		}
	}
	public class EnderecoEntregaCepViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega CEP";

		public override string NomePropriedade => "EnderecoEntregaCep";

		public override int Codigo => 27;

		public EnderecoEntregaCepViewModel()
		{
		}

		public EnderecoEntregaCepViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaCepViewModel(dto);
		}
	}
	public class EnderecoEntregaMunicipioViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega município";

		public override string NomePropriedade => "EnderecoEntregaMunicipio";

		public override int Codigo => 28;

		public EnderecoEntregaMunicipioViewModel()
		{
		}

		public EnderecoEntregaMunicipioViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaMunicipioViewModel(dto);
		}
	}
	public class EnderecoEntregaNumeroViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega número";

		public override string NomePropriedade => "EnderecoEntregaNumero";

		public override int Codigo => 42;

		public EnderecoEntregaNumeroViewModel()
		{
		}

		public EnderecoEntregaNumeroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaNumeroViewModel(dto);
		}
	}
	public class EnderecoEntregaPaisViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega país";

		public override string NomePropriedade => "EnderecoEntregaPais";

		public override int Codigo => 30;

		public EnderecoEntregaPaisViewModel()
		{
		}

		public EnderecoEntregaPaisViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaPaisViewModel(dto);
		}
	}
	public class EnderecoEntregaUfViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega UF";

		public override string NomePropriedade => "EnderecoEntregaUf";

		public override int Codigo => 29;

		public EnderecoEntregaUfViewModel()
		{
		}

		public EnderecoEntregaUfViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaUfViewModel(dto);
		}
	}
	public class EnderecoEntregaViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço entrega";

		public override string NomePropriedade => "EnderecoEntrega";

		public override int Codigo => 25;

		public EnderecoEntregaViewModel()
		{
		}

		public EnderecoEntregaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoEntregaViewModel(dto);
		}
	}
	public class EnderecoPrincipalBairroViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal bairro";

		public override string NomePropriedade => "EnderecoPrincipalBairro";

		public override int Codigo => 20;

		public EnderecoPrincipalBairroViewModel()
		{
		}

		public EnderecoPrincipalBairroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalBairroViewModel(dto);
		}
	}
	public class EnderecoPrincipalCepViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal CEP";

		public override string NomePropriedade => "EnderecoPrincipalCep";

		public override int Codigo => 21;

		public EnderecoPrincipalCepViewModel()
		{
		}

		public EnderecoPrincipalCepViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalCepViewModel(dto);
		}
	}
	public class EnderecoPrincipalMunicipioViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal município";

		public override string NomePropriedade => "EnderecoPrincipalMunicipio";

		public override int Codigo => 22;

		public EnderecoPrincipalMunicipioViewModel()
		{
		}

		public EnderecoPrincipalMunicipioViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalMunicipioViewModel(dto);
		}
	}
	public class EnderecoPrincipalNumeroViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal número";

		public override string NomePropriedade => "EnderecoPrincipalNumero";

		public override int Codigo => 41;

		public EnderecoPrincipalNumeroViewModel()
		{
		}

		public EnderecoPrincipalNumeroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalNumeroViewModel(dto);
		}
	}
	public class EnderecoPrincipalPaisViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal país";

		public override string NomePropriedade => "EnderecoPrincipalPais";

		public override int Codigo => 24;

		public EnderecoPrincipalPaisViewModel()
		{
		}

		public EnderecoPrincipalPaisViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalPaisViewModel(dto);
		}
	}
	public class EnderecoPrincipalUfViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal UF";

		public override string NomePropriedade => "EnderecoPrincipalUf";

		public override int Codigo => 23;

		public EnderecoPrincipalUfViewModel()
		{
		}

		public EnderecoPrincipalUfViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalUfViewModel(dto);
		}
	}
	public class EnderecoPrincipalViewModel : TextoViewModel
	{
		public override string Titulo => "Endereço principal";

		public override string NomePropriedade => "EnderecoPrincipal";

		public override int Codigo => 19;

		public EnderecoPrincipalViewModel()
		{
		}

		public EnderecoPrincipalViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new EnderecoPrincipalViewModel(dto);
		}
	}
	public class GradeColunaViewModel : TextoViewModel
	{
		public override string Titulo => "Grade coluna";

		public override string NomePropriedade => "GradeColuna";

		public override int Codigo => 9;

		public GradeColunaViewModel()
		{
		}

		public GradeColunaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new GradeColunaViewModel(dto);
		}
	}
	public class GradeLinhaViewModel : TextoViewModel
	{
		public override string Titulo => "Grade linha";

		public override string NomePropriedade => "GradeLinha";

		public override int Codigo => 8;

		public GradeLinhaViewModel()
		{
		}

		public GradeLinhaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new GradeLinhaViewModel(dto);
		}
	}
	public class HoraImpressaoViewModel : HoraViewModel
	{
		public override string Titulo => "Hora de impressão";

		public override string NomePropriedade => "HoraImpressao";

		public override int Codigo => 44;

		public HoraImpressaoViewModel()
		{
		}

		public HoraImpressaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new HoraImpressaoViewModel(dto);
		}
	}
	public class CondicaoEspecialViewModel : TextoViewModel
	{
		public override string Titulo => "Condição especial";

		public override string NomePropriedade => "CondicaoEspecial";

		public override int Codigo => 51;

		public CondicaoEspecialViewModel()
		{
		}

		public CondicaoEspecialViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new CondicaoEspecialViewModel(dto);
		}
	}
	public class LinkViewModel : BarrasViewModel
	{
		public override string Titulo => "Link";

		public override string NomePropriedade => "Link";

		public override int Codigo => 91;

		public LinkViewModel()
		{
		}

		public LinkViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new LinkViewModel(dto);
		}
	}
	public class MovimentacaoItemCodigoBarrasTributavelViewModel : BarrasViewModel
	{
		public override string Titulo => "Código de barras tributável do item";

		public override string NomePropriedade => "MovimentacaoItemCodigoBarrasTributavel";

		public override int Codigo => 93;

		public MovimentacaoItemCodigoBarrasTributavelViewModel()
		{
		}

		public MovimentacaoItemCodigoBarrasTributavelViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemCodigoBarrasTributavelViewModel(dto);
		}
	}
	public class MovimentacaoItemCodigoBarrasViewModel : BarrasViewModel
	{
		public override string Titulo => "Código de barras do item";

		public override string NomePropriedade => "MovimentacaoItemCodigoBarras";

		public override int Codigo => 90;

		public MovimentacaoItemCodigoBarrasViewModel()
		{
		}

		public MovimentacaoItemCodigoBarrasViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemCodigoBarrasViewModel(dto);
		}
	}
	public class MovimentacaoItemCodigoInternoViewModel : TextoViewModel
	{
		public override string Titulo => "Código interno do item";

		public override string NomePropriedade => "MovimentacaoItemCodigoInterno";

		public override int Codigo => 89;

		public MovimentacaoItemCodigoInternoViewModel()
		{
		}

		public MovimentacaoItemCodigoInternoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemCodigoInternoViewModel(dto);
		}
	}
	public class MovimentacaoItemInformacaoEstoqueViewModel : TextoViewModel
	{
		public override string Titulo => "Informações do estoque";

		public override string NomePropriedade => "MovimentacaoItemInformacaoEstoque";

		public override int Codigo => 95;

		public MovimentacaoItemInformacaoEstoqueViewModel()
		{
		}

		public MovimentacaoItemInformacaoEstoqueViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemInformacaoEstoqueViewModel(dto);
		}
	}
	public class MovimentacaoItemDescricaoViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição do item";

		public override string NomePropriedade => "MovimentacaoItemDescricao";

		public override int Codigo => 83;

		public MovimentacaoItemDescricaoViewModel()
		{
		}

		public MovimentacaoItemDescricaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemDescricaoViewModel(dto);
		}
	}
	public class MovimentacaoItemObservacaoViewModel : TextoViewModel
	{
		public override string Titulo => "Observação do item";

		public override string NomePropriedade => "MovimentacaoItemObservacao";

		public override int Codigo => 88;

		public MovimentacaoItemObservacaoViewModel()
		{
		}

		public MovimentacaoItemObservacaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemObservacaoViewModel(dto);
		}
	}
	public class MovimentacaoItemQuantidadeViewModel : NumericoViewModel
	{
		public override string Titulo => "Quantidade do item";

		public override string NomePropriedade => "MovimentacaoItemQuantidade";

		public override int Codigo => 82;

		public MovimentacaoItemQuantidadeViewModel()
		{
		}

		public MovimentacaoItemQuantidadeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemQuantidadeViewModel(dto);
		}
	}
	public class MovimentacaoItemTotalPesoBrutoViewModel : NumericoViewModel
	{
		public override string Titulo => "Total peso bruto do item";

		public override string NomePropriedade => "MovimentacaoItemTotalPesoBruto";

		public override int Codigo => 87;

		public MovimentacaoItemTotalPesoBrutoViewModel()
		{
		}

		public MovimentacaoItemTotalPesoBrutoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemTotalPesoBrutoViewModel(dto);
		}
	}
	public class MovimentacaoItemTotalPesoLiquidoViewModel : NumericoViewModel
	{
		public override string Titulo => "Total peso líquido do item";

		public override string NomePropriedade => "MovimentacaoItemTotalPesoLiquido";

		public override int Codigo => 86;

		public MovimentacaoItemTotalPesoLiquidoViewModel()
		{
		}

		public MovimentacaoItemTotalPesoLiquidoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemTotalPesoLiquidoViewModel(dto);
		}
	}
	public class MovimentacaoItemTotalViewModel : NumericoViewModel
	{
		public override string Titulo => "Total do item";

		public override string NomePropriedade => "MovimentacaoItemTotal";

		public override int Codigo => 84;

		public MovimentacaoItemTotalViewModel()
		{
		}

		public MovimentacaoItemTotalViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemTotalViewModel(dto);
		}
	}
	public class MovimentacaoItemUnidadeViewModel : TextoViewModel
	{
		public override string Titulo => "Unidade do item";

		public override string NomePropriedade => "MovimentacaoItemUnidade";

		public override int Codigo => 85;

		public MovimentacaoItemUnidadeViewModel()
		{
		}

		public MovimentacaoItemUnidadeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoItemUnidadeViewModel(dto);
		}
	}
	public class LocalizacaoViewModel : TextoViewModel
	{
		public override string Titulo => "Localização";

		public override string NomePropriedade => "Localizacao";

		public override int Codigo => 7;

		public LocalizacaoViewModel()
		{
		}

		public LocalizacaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new LocalizacaoViewModel(dto);
		}
	}
	public class LoteViewModel : TextoViewModel
	{
		public override string Titulo => "Lote";

		public override string NomePropriedade => "Lote";

		public override int Codigo => 45;

		public LoteViewModel()
		{
		}

		public LoteViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new LoteViewModel(dto);
		}
	}
	public class MarcaViewModel : TextoViewModel
	{
		public override string Titulo => "Marca";

		public override string NomePropriedade => "Marca";

		public override int Codigo => 5;

		public MarcaViewModel()
		{
		}

		public MarcaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MarcaViewModel(dto);
		}
	}
	public class MovimentacaoChaveAcessoViewModel : TextoViewModel
	{
		public override string Titulo => "Chave de acesso da movimentação";

		public override string NomePropriedade => "MovimentacaoChaveAcesso";

		public override int Codigo => 67;

		public MovimentacaoChaveAcessoViewModel()
		{
		}

		public MovimentacaoChaveAcessoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoChaveAcessoViewModel(dto);
		}
	}
	public class MovimentacaoDataEmissaoViewModel : DataViewModel
	{
		public override string Titulo => "Data de emissão da movimentação";

		public override string NomePropriedade => "MovimentacaoDataEmissao";

		public override int Codigo => 65;

		public MovimentacaoDataEmissaoViewModel()
		{
		}

		public MovimentacaoDataEmissaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoDataEmissaoViewModel(dto);
		}
	}
	public class MovimentacaoDescricaoPrioridadeViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição da prioridade da movimentação";

		public override string NomePropriedade => "MovimentacaoDescricaoPrioridade";

		public override int Codigo => 70;

		public MovimentacaoDescricaoPrioridadeViewModel()
		{
		}

		public MovimentacaoDescricaoPrioridadeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoDescricaoPrioridadeViewModel(dto);
		}
	}
	public class MovimentacaoNomeTecnicoResponsavelViewModel : TextoViewModel
	{
		public override string Titulo => "Nome do técnico responsável da movimentação";

		public override string NomePropriedade => "MovimentacaoNomeTecnicoResponsavel";

		public override int Codigo => 68;

		public MovimentacaoNomeTecnicoResponsavelViewModel()
		{
		}

		public MovimentacaoNomeTecnicoResponsavelViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoNomeTecnicoResponsavelViewModel(dto);
		}
	}
	public class MovimentacaoNomeVendedorResponsavelViewModel : TextoViewModel
	{
		public override string Titulo => "Nome do vendedor responsável da movimentação";

		public override string NomePropriedade => "MovimentacaoNomeVendedorResponsavel";

		public override int Codigo => 69;

		public MovimentacaoNomeVendedorResponsavelViewModel()
		{
		}

		public MovimentacaoNomeVendedorResponsavelViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoNomeVendedorResponsavelViewModel(dto);
		}
	}
	public class MovimentacaoNumeroViewModel : TextoViewModel
	{
		public override string Titulo => "Número da movimentação";

		public override string NomePropriedade => "MovimentacaoNumero";

		public override int Codigo => 64;

		public MovimentacaoNumeroViewModel()
		{
		}

		public MovimentacaoNumeroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoNumeroViewModel(dto);
		}
	}
	public class MovimentacaoTotalPesoBrutoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso total bruto da movimentação";

		public override string NomePropriedade => "MovimentacaoTotalPesoBruto";

		public override int Codigo => 71;

		public MovimentacaoTotalPesoBrutoViewModel()
		{
		}

		public MovimentacaoTotalPesoBrutoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoTotalPesoBrutoViewModel(dto);
		}
	}
	public class MovimentacaoTotalPesoLiquidoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso total líquido da movimentação";

		public override string NomePropriedade => "MovimentacaoTotalPesoLiquido";

		public override int Codigo => 72;

		public MovimentacaoTotalPesoLiquidoViewModel()
		{
		}

		public MovimentacaoTotalPesoLiquidoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoTotalPesoLiquidoViewModel(dto);
		}
	}
	public class MovimentacaoTotalViewModel : MonetarioViewModel
	{
		public override string Titulo => "Total da movimentação";

		public override string NomePropriedade => "MovimentacaoTotal";

		public override int Codigo => 66;

		public MovimentacaoTotalViewModel()
		{
		}

		public MovimentacaoTotalViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new MovimentacaoTotalViewModel(dto);
		}
	}
	public class NomeFantasiaViewModel : TextoViewModel
	{
		public override string Titulo => "Nome fantasia";

		public override string NomePropriedade => "NomeFantasia";

		public override int Codigo => 31;

		public NomeFantasiaViewModel()
		{
		}

		public NomeFantasiaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new NomeFantasiaViewModel(dto);
		}
	}
	public class ObjetoAnoFabricacaoViewModel : TextoViewModel
	{
		public override string Titulo => "Ano de fabricação do objeto";

		public override string NomePropriedade => "ObjetoAnoFabricacao";

		public override int Codigo => 60;

		public ObjetoAnoFabricacaoViewModel()
		{
		}

		public ObjetoAnoFabricacaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoAnoFabricacaoViewModel(dto);
		}
	}
	public class ObjetoChassiViewModel : TextoViewModel
	{
		public override string Titulo => "Chassi do objeto";

		public override string NomePropriedade => "ObjetoChassi";

		public override int Codigo => 55;

		public ObjetoChassiViewModel()
		{
		}

		public ObjetoChassiViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoChassiViewModel(dto);
		}
	}
	public class ObjetoCilindradaViewModel : TextoViewModel
	{
		public override string Titulo => "Cilindrada do objeto";

		public override string NomePropriedade => "ObjetoCilindrada";

		public override int Codigo => 100;

		public ObjetoCilindradaViewModel()
		{
		}

		public ObjetoCilindradaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoCilindradaViewModel(dto);
		}
	}
	public class ObjetoCorViewModel : TextoViewModel
	{
		public override string Titulo => "Cor do objeto";

		public override string NomePropriedade => "ObjetoCor";

		public override int Codigo => 59;

		public ObjetoCorViewModel()
		{
		}

		public ObjetoCorViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoCorViewModel(dto);
		}
	}
	public class ObjetoDescricaoViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição do objeto";

		public override string NomePropriedade => "ObjetoDescricao";

		public override int Codigo => 52;

		public ObjetoDescricaoViewModel()
		{
		}

		public ObjetoDescricaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoDescricaoViewModel(dto);
		}
	}
	public class ObjetoMarcaViewModel : TextoViewModel
	{
		public override string Titulo => "Marca do objeto";

		public override string NomePropriedade => "ObjetoMarca";

		public override int Codigo => 57;

		public ObjetoMarcaViewModel()
		{
		}

		public ObjetoMarcaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoMarcaViewModel(dto);
		}
	}
	public class ObjetoModeloViewModel : TextoViewModel
	{
		public override string Titulo => "Modelo do objeto";

		public override string NomePropriedade => "ObjetoModelo";

		public override int Codigo => 56;

		public ObjetoModeloViewModel()
		{
		}

		public ObjetoModeloViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoModeloViewModel(dto);
		}
	}
	public class ObjetoMotorViewModel : TextoViewModel
	{
		public override string Titulo => "Motor do objeto";

		public override string NomePropriedade => "ObjetoMotor";

		public override int Codigo => 102;

		public ObjetoMotorViewModel()
		{
		}

		public ObjetoMotorViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoMotorViewModel(dto);
		}
	}
	public class ObjetoPlacaViewModel : TextoViewModel
	{
		public override string Titulo => "Placa do objeto";

		public override string NomePropriedade => "ObjetoPlaca";

		public override int Codigo => 54;

		public ObjetoPlacaViewModel()
		{
		}

		public ObjetoPlacaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoPlacaViewModel(dto);
		}
	}
	public class ObjetoPotenciaViewModel : TextoViewModel
	{
		public override string Titulo => "Potência do objeto";

		public override string NomePropriedade => "ObjetoPotencia";

		public override int Codigo => 101;

		public ObjetoPotenciaViewModel()
		{
		}

		public ObjetoPotenciaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoPotenciaViewModel(dto);
		}
	}
	public class ObjetoRenavamViewModel : TextoViewModel
	{
		public override string Titulo => "Renavam do objeto";

		public override string NomePropriedade => "ObjetoRenavam";

		public override int Codigo => 58;

		public ObjetoRenavamViewModel()
		{
		}

		public ObjetoRenavamViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoRenavamViewModel(dto);
		}
	}
	public class ObjetoSegmentoViewModel : TextoViewModel
	{
		public override string Titulo => "Segmento do objeto";

		public override string NomePropriedade => "ObjetoSegmento";

		public override int Codigo => 103;

		public ObjetoSegmentoViewModel()
		{
		}

		public ObjetoSegmentoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoSegmentoViewModel(dto);
		}
	}
	public class ObjetoSerialViewModel : TextoViewModel
	{
		public override string Titulo => "Serial do objeto";

		public override string NomePropriedade => "ObjetoSerial";

		public override int Codigo => 53;

		public ObjetoSerialViewModel()
		{
		}

		public ObjetoSerialViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObjetoSerialViewModel(dto);
		}
	}
	public class ObservacaoViewModel : TextoViewModel
	{
		public override string Titulo => "Observação";

		public override string NomePropriedade => "Observacao";

		public override int Codigo => 6;

		public ObservacaoViewModel()
		{
		}

		public ObservacaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ObservacaoViewModel(dto);
		}
	}
	public class PesoBrutoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso bruto";

		public override string NomePropriedade => "PesoBruto";

		public override int Codigo => 15;

		public PesoBrutoViewModel()
		{
		}

		public PesoBrutoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PesoBrutoViewModel(dto);
		}
	}
	public class PesoLiquidoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso líquido";

		public override string NomePropriedade => "PesoLiquido";

		public override int Codigo => 14;

		public PesoLiquidoViewModel()
		{
		}

		public PesoLiquidoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PesoLiquidoViewModel(dto);
		}
	}
	public class PrecoCustoViewModel : MonetarioViewModel
	{
		public override string Titulo => "Preço de custo";

		public override string NomePropriedade => "PrecoCusto";

		public override int Codigo => 12;

		public PrecoCustoViewModel()
		{
		}

		public PrecoCustoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PrecoCustoViewModel(dto);
		}
	}
	public class PrecoVendaCadastroViewModel : MonetarioViewModel
	{
		public override string Titulo => "Preço de venda do cadastro";

		public override string NomePropriedade => "PrecoVendaCadastro";

		public override int Codigo => 98;

		public PrecoVendaCadastroViewModel()
		{
		}

		public PrecoVendaCadastroViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PrecoVendaCadastroViewModel(dto);
		}
	}
	public class PrecoVendaPromocionalViewModel : MonetarioViewModel
	{
		public override string Titulo => "Preço de venda promocional";

		public override string NomePropriedade => "PrecoVendaPromocional";

		public override int Codigo => 99;

		public PrecoVendaPromocionalViewModel()
		{
		}

		public PrecoVendaPromocionalViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PrecoVendaPromocionalViewModel(dto);
		}
	}
	public class PrecoVendaTabelaPrecoViewModel : MonetarioViewModel
	{
		public override string Titulo => "Preço de venda da tabela indicada";

		public override string NomePropriedade => "PrecoVendaTabelaPreco";

		public override int Codigo => 50;

		public PrecoVendaTabelaPrecoViewModel()
		{
		}

		public PrecoVendaTabelaPrecoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PrecoVendaTabelaPrecoViewModel(dto);
		}
	}
	public class PrecoVendaViewModel : MonetarioViewModel
	{
		public override string Titulo => "Preço de venda";

		public override string NomePropriedade => "PrecoVenda";

		public override int Codigo => 13;

		public PrecoVendaViewModel()
		{
		}

		public PrecoVendaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new PrecoVendaViewModel(dto);
		}
	}
	public class QuantidadeViewModel : NumericoViewModel
	{
		public override string Titulo => "Quantidade";

		public override string NomePropriedade => "QuantidadeEstoque";

		public override int Codigo => 11;

		public QuantidadeViewModel()
		{
		}

		public QuantidadeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new QuantidadeViewModel(dto);
		}
	}
	public class ReferenciaPrecoViewModel : MonetarioViewModel
	{
		public override string Titulo => "Referência do preço venda";

		public override string NomePropriedade => "PrecoReferencia";

		public override int Codigo => 36;

		public ReferenciaPrecoViewModel()
		{
		}

		public ReferenciaPrecoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ReferenciaPrecoViewModel(dto);
		}
	}
	public class ResponsavelViewModel : TextoViewModel
	{
		public override string Titulo => "Responsável";

		public override string NomePropriedade => "Responsavel";

		public override int Codigo => 32;

		public ResponsavelViewModel()
		{
		}

		public ResponsavelViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new ResponsavelViewModel(dto);
		}
	}
	public class SerialViewModel : TextoViewModel
	{
		public override string Titulo => "Serial";

		public override string NomePropriedade => "Serial";

		public override int Codigo => 46;

		public SerialViewModel()
		{
		}

		public SerialViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new SerialViewModel(dto);
		}
	}
	public class TelefoneViewModel : TextoViewModel
	{
		public override string Titulo => "Telefone";

		public override string NomePropriedade => "Telefone";

		public override int Codigo => 18;

		public TelefoneViewModel()
		{
		}

		public TelefoneViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new TelefoneViewModel(dto);
		}
	}
	public class TransportadoraDocumentoViewModel : TextoViewModel
	{
		public override string Titulo => "Documento da transportadora";

		public override string NomePropriedade => "TransportadoraDocumento";

		public override int Codigo => 81;

		public TransportadoraDocumentoViewModel()
		{
		}

		public TransportadoraDocumentoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new TransportadoraDocumentoViewModel(dto);
		}
	}
	public class TransportadoraNomeFantasiaViewModel : TextoViewModel
	{
		public override string Titulo => "Nome fantasia da transportadora";

		public override string NomePropriedade => "TransportadoraNomeFantasia";

		public override int Codigo => 80;

		public TransportadoraNomeFantasiaViewModel()
		{
		}

		public TransportadoraNomeFantasiaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new TransportadoraNomeFantasiaViewModel(dto);
		}
	}
	public class TransportadoraNomeViewModel : TextoViewModel
	{
		public override string Titulo => "Nome da transportadora";

		public override string NomePropriedade => "TransportadoraNome";

		public override int Codigo => 79;

		public TransportadoraNomeViewModel()
		{
		}

		public TransportadoraNomeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new TransportadoraNomeViewModel(dto);
		}
	}
	public class UnidadeMedidaViewModel : TextoViewModel
	{
		public override string Titulo => "Unidade de medida";

		public override string NomePropriedade => "UnidadeMedida";

		public override int Codigo => 4;

		public UnidadeMedidaViewModel()
		{
		}

		public UnidadeMedidaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new UnidadeMedidaViewModel(dto);
		}
	}
	public class VolumeContadorViewModel : TextoViewModel
	{
		public override string Titulo => "Contador de volumes (volume/volumes)";

		public override string NomePropriedade => "VolumeContador";

		public override int Codigo => 94;

		public VolumeContadorViewModel()
		{
		}

		public VolumeContadorViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumeContadorViewModel(dto);
		}
	}
	public class VolumeEspecieViewModel : TextoViewModel
	{
		public override string Titulo => "Espécie do volume";

		public override string NomePropriedade => "VolumeEspecie";

		public override int Codigo => 75;

		public VolumeEspecieViewModel()
		{
		}

		public VolumeEspecieViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumeEspecieViewModel(dto);
		}
	}
	public class VolumeMarcaViewModel : TextoViewModel
	{
		public override string Titulo => "Marca do volume";

		public override string NomePropriedade => "VolumeMarca";

		public override int Codigo => 74;

		public VolumeMarcaViewModel()
		{
		}

		public VolumeMarcaViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumeMarcaViewModel(dto);
		}
	}
	public class VolumeNumeracaoViewModel : TextoViewModel
	{
		public override string Titulo => "Numeração do volume";

		public override string NomePropriedade => "VolumeNumeracao";

		public override int Codigo => 76;

		public VolumeNumeracaoViewModel()
		{
		}

		public VolumeNumeracaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumeNumeracaoViewModel(dto);
		}
	}
	public class VolumePesoBrutoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso bruto do volume";

		public override string NomePropriedade => "VolumePesoBruto";

		public override int Codigo => 78;

		public VolumePesoBrutoViewModel()
		{
		}

		public VolumePesoBrutoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumePesoBrutoViewModel(dto);
		}
	}
	public class VolumePesoLiquidoViewModel : NumericoViewModel
	{
		public override string Titulo => "Peso líquido do volume";

		public override string NomePropriedade => "VolumePesoLiquido";

		public override int Codigo => 77;

		public VolumePesoLiquidoViewModel()
		{
		}

		public VolumePesoLiquidoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumePesoLiquidoViewModel(dto);
		}
	}
	public class VolumeQuantidadeViewModel : TextoViewModel
	{
		public override string Titulo => "Quantidade do volume";

		public override string NomePropriedade => "VolumeQuantidade";

		public override int Codigo => 73;

		public VolumeQuantidadeViewModel()
		{
		}

		public VolumeQuantidadeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new VolumeQuantidadeViewModel(dto);
		}
	}
	public class DescricaoViewModel : TextoViewModel
	{
		public override string Titulo => "Descrição";

		public override string NomePropriedade => "Descricao";

		public override int Codigo => 1;

		public DescricaoViewModel()
		{
		}

		public DescricaoViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new DescricaoViewModel(dto);
		}
	}
	public class NomeViewModel : TextoViewModel
	{
		public override string Titulo => "Nome";

		public override string NomePropriedade => "Nome";

		public override int Codigo => 17;

		public NomeViewModel()
		{
		}

		public NomeViewModel(CampoEtiquetaDto dto)
			: base(dto)
		{
		}

		public override CampoBaseViewModel Criar(CampoEtiquetaDto dto)
		{
			return new NomeViewModel(dto);
		}
	}
}
namespace Otus.Modulo.Etiquetas.ViewModels.Tipos.Alinhamentos
{
	public abstract class AlinhamentoTexto
	{
		public abstract int Codigo { get; }

		public abstract TextAlignment Alinhamento { get; }

		public abstract string Descricao { get; }
	}
	public class AlinhamentoTextoComboBoxViewModel : Lista<AlinhamentoTexto>
	{
		public AlinhamentoTextoComboBoxViewModel(int codigoAlinhamento)
		{
			base.Adicionar((AlinhamentoTexto)new Esquerda());
			base.Adicionar((AlinhamentoTexto)new Centro());
			base.Adicionar((AlinhamentoTexto)new Direita());
			if (codigoAlinhamento == 0)
			{
				base.PosicionarNoPrimeiro();
			}
			else
			{
				base.Posicionar((Expression<Func<AlinhamentoTexto, bool>>)((AlinhamentoTexto x) => x.Codigo == codigoAlinhamento));
			}
			base.AlterouItemSelecionadoEvent += OnAlterouItemSelecionadoEvent;
		}

		private void OnAlterouItemSelecionadoEvent(object sender, AlinhamentoTexto alinhamentoTexto)
		{
			if (alinhamentoTexto == null)
			{
				base.PosicionarNoPrimeiro();
			}
		}

		public override void Dispose()
		{
			base.AlterouItemSelecionadoEvent -= OnAlterouItemSelecionadoEvent;
			base.Dispose();
		}
	}
	public class Centro : AlinhamentoTexto
	{
		public override int Codigo => 3;

		public override TextAlignment Alinhamento => (TextAlignment)32;

		public override string Descricao => "Centro";
	}
	public class Direita : AlinhamentoTexto
	{
		public override int Codigo => 2;

		public override TextAlignment Alinhamento => (TextAlignment)64;

		public override string Descricao => "Direita";
	}
	public class Esquerda : AlinhamentoTexto
	{
		public override int Codigo => 1;

		public override TextAlignment Alinhamento => (TextAlignment)16;

		public override string Descricao => "Esquerda";
	}
}
namespace Otus.Modulo.Etiquetas.ViewModels.EtiquetasImpressao
{
	public class EtiquetaConfiguracaoComboBoxViewModel : Lista<EtiquetaConfiguracaoViewModel>
	{
		private readonly Action<bool> _origemProduto;

		public PreVisualizacaoViewModel PreVisualizacaoViewModel
		{
			get
			{
				PaginaViewModel paginaViewModel = new PaginaViewModel(((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado).Dto.Pagina);
				((ViewModelBase<PaginaDto>)(object)paginaViewModel).ProjectedAsViewModel();
				EtiquetaViewModel etiquetaViewModel = new EtiquetaViewModel(((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado).Dto.Etiqueta);
				((ViewModelBase<EtiquetaDto>)(object)etiquetaViewModel).ProjectedAsViewModel();
				CampoViewModel campoViewModel = new CampoViewModel(((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado).Dto.Campos.Select((CampoEtiquetaDto x) => new CampoEtiquetaViewModel(x, ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado.OrigemComboBox).ItemSelecionado.Campos.Itens)).ToList(), ((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado);
				((ViewModelBase<List<CampoEtiquetaViewModel>>)(object)campoViewModel).ProjectedAsViewModel();
				return new PreVisualizacaoViewModel(new PreVisualizacao(paginaViewModel, etiquetaViewModel, campoViewModel), ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado.OrigemComboBox).ItemSelecionado.ObterDadosParaImpressao());
			}
		}

		public override bool Valido
		{
			get
			{
				if (((ViewModelValidationBase)this).Valido && base.PossuiItemSelecionado && !HelperObject.IsNull((object)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado.OrigemComboBox))
				{
					return ((ViewModelValidationBase)((Lista<EtiquetaConfiguracaoViewModel>)this).ItemSelecionado.OrigemComboBox).Valido;
				}
				return false;
			}
		}

		public EtiquetaConfiguracaoComboBoxViewModel(IEnumerable<EtiquetaConfiguracaoDto> dto, Action<bool> origemProduto)
		{
			_origemProduto = origemProduto;
			foreach (EtiquetaConfiguracaoDto item in dto)
			{
				EtiquetaConfiguracaoViewModel etiquetaConfiguracaoViewModel = new EtiquetaConfiguracaoViewModel(item);
				((ViewModelBase<EtiquetaConfiguracaoDto>)(object)etiquetaConfiguracaoViewModel).ProjectedAsViewModel();
				base.Adicionar(etiquetaConfiguracaoViewModel);
			}
			base.PosicionarNoPrimeiro();
			base.AlterouItemSelecionadoEvent += OnAlterouItemSelecionadoEvent;
		}

		private void OnAlterouItemSelecionadoEvent(object sender, EtiquetaConfiguracaoViewModel etiquetaConfiguracaoViewModel)
		{
			((Lista<Filtro>)((Lista<OrigemDado>)etiquetaConfiguracaoViewModel?.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).PosicionarNoPrimeiro();
			_origemProduto(((Lista<OrigemDado>)etiquetaConfiguracaoViewModel.OrigemComboBox).ItemSelecionado.Descricao.Equals("Produtos"));
		}

		public override void Dispose()
		{
			base.AlterouItemSelecionadoEvent -= OnAlterouItemSelecionadoEvent;
			foreach (EtiquetaConfiguracaoViewModel iten in base.Itens)
			{
				((ViewModelValidationBase)iten).Dispose();
			}
			base.Dispose();
		}
	}
	public class BuscaFiltroLancamentoItemRapidoAbaViewModel : AbaViewModelBase
	{
		public BuscaFiltroLancamentoItemRapidoAbaViewModel(IViewModelBase viewModel)
			: base("Lançamento Rápido", "Busca de produtos/serviços", viewModel)
		{
		}
	}
	public class EtiquetaImpressaoAbaViewModel : AbaViewModelBase
	{
		public EtiquetaImpressaoAbaViewModel(IViewModelBase viewModel)
			: base("ETIQUETA", "Seleção de filtros", viewModel)
		{
		}
	}
	public class EtiquetaImpressaoConfiguracaoAbaViewModel : AbaViewModelBase
	{
		public EtiquetaImpressaoConfiguracaoAbaViewModel(IViewModelBase viewModel)
			: base("CONFIGURAÇÃO", "Etiquetas ignoradas no início", viewModel)
		{
		}
	}
	public class EtiquetaImpressaoFichaViewModel : FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>
	{
		private readonly IWService _service;

		public ICommand ImprimirCommand { get; set; }

		public ICommand GerarDavCommand { get; set; }

		public EtiquetaImpressaoFichaViewModel(IWService service)
			: base(string.Empty)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Expected O, but got Unknown
			_service = service;
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			GerarDavCommand = (ICommand)new AsyncCommand((Func<Task>)GerarDav, (Func<bool>)PodeImprimir, (bool?)null);
		}

		protected override async void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			if (((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario != null)
			{
				return;
			}
			((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario = new EtiquetaImpressaoViewModel(HelperIEnumerable.ToListEmptyWhenNull<EtiquetaConfiguracaoDto>(await ObterDadosEtiqueta()), _service, (Action<object>)((NavigationBase)this).Navegar, (Action<bool>)delegate
			{
			}).CriarFormulario();
			object obj = navigationParameters["dtoEntrada"];
			MovimentacaoDto val = (MovimentacaoDto)((obj is MovimentacaoDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				((EtiquetaImpressaoFormulario)(object)((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario).SetarMovimentacao(val, isNotaEntrada: true);
			}
			object obj2 = navigationParameters["dtoSaida"];
			MovimentacaoDto val2 = (MovimentacaoDto)((obj2 is MovimentacaoDto) ? obj2 : null);
			if (!HelperObject.IsNull((object)val2))
			{
				((EtiquetaImpressaoFormulario)(object)((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario).SetarMovimentacao(val2, isNotaEntrada: false);
			}
			object obj3 = navigationParameters["dto"];
			ProdutoServicoEmpresaDto val3 = (ProdutoServicoEmpresaDto)((obj3 is ProdutoServicoEmpresaDto) ? obj3 : null);
			if (!HelperObject.IsNull((object)val3))
			{
				decimal num = default(decimal);
				string text = navigationParameters["quantidade"] as string;
				if (!HelperString.IsNullOrEmpty(text))
				{
					num = decimal.Parse(HelperString.ToStringEmptyWhenNull(text));
				}
				if (num > 0m)
				{
					((EtiquetaImpressaoFormulario)(object)((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario).SetarProdutoServico(val3, (int)Math.Round(num, MidpointRounding.ToEven));
				}
				else
				{
					((EtiquetaImpressaoFormulario)(object)((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario).SetarProdutoServico(val3);
				}
			}
		}

		private async Task<IEnumerable<EtiquetaConfiguracaoDto>> ObterDadosEtiqueta()
		{
			try
			{
				return await _service.Canal<IEtiquetaConfiguracaoModuleService, Task<IEnumerable<EtiquetaConfiguracaoDto>>>((Func<IEtiquetaConfiguracaoModuleService, Task<IEnumerable<EtiquetaConfiguracaoDto>>>)((IEtiquetaConfiguracaoModuleService x) => x.ObterDtoAsync()));
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
				throw;
			}
		}

		private void Imprimir()
		{
			((EtiquetaImpressaoFormulario)(object)base.Formulario).Imprimir();
		}

		private async Task GerarDav()
		{
			await ((EtiquetaImpressaoFormulario)(object)((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).Formulario).GerarDav();
		}

		public bool PodeImprimir()
		{
			if (!HelperObject.IsNull((object)base.Formulario))
			{
				return base.Formulario.Valido;
			}
			return false;
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= ((FichaBase<EtiquetaConfiguracaoDto, EtiquetaImpressaoViewModel>)this).OnParametroInformado;
			base.Dispose();
		}
	}
	public class EtiquetaImpressaoFormulario : Formulario
	{
		private readonly EtiquetaImpressaoViewModel _etiquetaImpressaoViewModel;

		private readonly Action<object> _navegar;

		private readonly IWService _service;

		private long _gerouDav;

		public EtiquetaImpressaoFormulario(List<EtiquetaConfiguracaoDto> dto, IWService service, Action<object> navegar)
		{
			_navegar = navegar;
			_service = service;
			_etiquetaImpressaoViewModel = new EtiquetaImpressaoViewModel(dto, service, navegar, OrigemProduto);
			EtiquetaImpressaoItensViewModel viewModel = new EtiquetaImpressaoItensViewModel(_etiquetaImpressaoViewModel);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[3]
			{
				(IAbaViewModelBase)new EtiquetaImpressaoAbaViewModel((IViewModelBase)(object)_etiquetaImpressaoViewModel),
				(IAbaViewModelBase)new EtiquetaImpressaoItensAbaViewModel((IViewModelBase)(object)viewModel),
				(IAbaViewModelBase)new EtiquetaImpressaoConfiguracaoAbaViewModel((IViewModelBase)(object)new EtiquetaImpressaoConfiguracaoViewModel(_etiquetaImpressaoViewModel))
			});
			if (HelperObject.IsNotNull((object)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado) && ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Descricao == "Produtos")
			{
				((Formulario)this).InserirProjetarAba((IAbaViewModelBase)(object)new BuscaFiltroLancamentoItemRapidoAbaViewModel((IViewModelBase)(object)new BuscaFiltroLancamentoItemRapidoViewModel(service, _navegar, _etiquetaImpressaoViewModel)), 1);
			}
		}

		public void OrigemProduto(bool produto)
		{
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase aba) => aba is BuscaFiltroLancamentoItemRapidoAbaViewModel));
			if (produto)
			{
				((Formulario)this).InserirProjetarAba((IAbaViewModelBase)(object)new BuscaFiltroLancamentoItemRapidoAbaViewModel((IViewModelBase)(object)new BuscaFiltroLancamentoItemRapidoViewModel(_service, _navegar, _etiquetaImpressaoViewModel)), 1);
			}
		}

		public void Imprimir()
		{
			_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox.PreVisualizacaoViewModel.Imprimir();
		}

		public async Task GerarDav()
		{
			if (_gerouDav <= 0 || DialogoFactory.MostrarDialogoSimNao($"O DAV nº {_gerouDav} já foi gerado para esta etiqueta que ainda está em andamento. Deseja continuar mesmo assim?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				EtiquetaImpressaoFormulario etiquetaImpressaoFormulario = this;
				_ = etiquetaImpressaoFormulario._gerouDav;
				etiquetaImpressaoFormulario._gerouDav = await _etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox.PreVisualizacaoViewModel.GerarDav();
			}
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto, bool isNotaEntrada)
		{
			_etiquetaImpressaoViewModel.SetarMovimentacao(movimentacaoDto, isNotaEntrada);
		}

		public void SetarProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresaDto, int quantidade = 1)
		{
			_etiquetaImpressaoViewModel.SetarProdutoServico(produtoServicoEmpresaDto, quantidade);
		}
	}
	public class EtiquetaImpressaoItensAbaViewModel : AbaViewModelBase
	{
		public EtiquetaImpressaoItensAbaViewModel(IViewModelBase viewModel)
			: base("ITENS", "Itens da etiqueta", viewModel)
		{
		}
	}
	public class EtiquetaImpressaoItensViewModel : ViewModelBase<EtiquetaImpressaoViewModel>
	{
		public OrigemDado OrigemDado => ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)base.Model?.EtiquetasConfiguracaoComboBox)?.ItemSelecionado?.OrigemComboBox).ItemSelecionado;

		public override bool Valido
		{
			get
			{
				if (OrigemDado != null)
				{
					return ((ViewModelValidationBase)OrigemDado.ListaPesquisa).Valido;
				}
				return false;
			}
		}

		public EtiquetaImpressaoItensViewModel(EtiquetaImpressaoViewModel etiquetaConfiguracaoViewModel)
			: base(etiquetaConfiguracaoViewModel)
		{
		}

		private void ListaPesquisaOnAlterouEvent(object sender, IEnumerable<IDado> enumerable)
		{
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((ViewModelValidationBase)this).Valido));
		}

		public override void ProjectedAsViewModel()
		{
			((Lista<EtiquetaConfiguracaoViewModel>)base.Model.EtiquetasConfiguracaoComboBox).AlterouItemSelecionadoEvent += EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent;
			EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent(null, null);
		}

		private void EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent(object sender, EtiquetaConfiguracaoViewModel etiquetaConfiguracaoViewModel)
		{
			if (!HelperObject.IsNull((object)OrigemDado))
			{
				OrigemDado.Limpar();
				OrigemDado.ListaPesquisa.AlterouEvent -= ListaPesquisaOnAlterouEvent;
				OrigemDado.ListaPesquisa.AlterouEvent += ListaPesquisaOnAlterouEvent;
				((BindableBase)this).RaisePropertyChanged<OrigemDado>((Expression<Func<OrigemDado>>)(() => OrigemDado));
				((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => ((ViewModelValidationBase)this).Valido));
			}
		}

		public override void Dispose()
		{
			if (!HelperObject.IsNull((object)OrigemDado))
			{
				OrigemDado.ListaPesquisa.AlterouEvent -= ListaPesquisaOnAlterouEvent;
			}
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EtiquetaImpressaoViewModel : ViewModelBase<List<EtiquetaConfiguracaoDto>>
	{
		private readonly IWService _service;

		private readonly Action<object> _navegar;

		public SelecaoCommandParameter<EtiquetaConfiguracaoDto> EtiquetaConfiguracaoCommandParameter => new SelecaoCommandParameter<EtiquetaConfiguracaoDto>("EtiquetaConfiguracaoListaView", (Action<EtiquetaConfiguracaoDto>)delegate(EtiquetaConfiguracaoDto etiqueta)
		{
			EtiquetaConfiguracaoViewModel etiquetaConfiguracaoViewModel = new EtiquetaConfiguracaoViewModel(etiqueta);
			((ViewModelBase<EtiquetaConfiguracaoDto>)(object)etiquetaConfiguracaoViewModel).ProjectedAsViewModel();
			((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).Substituir((Expression<Func<EtiquetaConfiguracaoViewModel, bool>>)((EtiquetaConfiguracaoViewModel x) => ((EntityBaseDto)((ViewModelDtoBase<EtiquetaConfiguracaoDto>)(object)x).Dto).Id == ((EntityBaseDto)etiqueta).Id), etiquetaConfiguracaoViewModel);
		}, true);

		public EtiquetaConfiguracaoComboBoxViewModel EtiquetasConfiguracaoComboBox { get; protected set; }

		public ICommand AdicionarCommand { get; set; }

		public override bool Valido => true;

		public EtiquetaImpressaoViewModel()
		{
		}

		public EtiquetaImpressaoViewModel(List<EtiquetaConfiguracaoDto> dto, IWService service, Action<object> navegar, Action<bool> origemProduto)
			: base(dto)
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Expected O, but got Unknown
			_service = service;
			_navegar = navegar;
			EtiquetasConfiguracaoComboBox = new EtiquetaConfiguracaoComboBoxViewModel(base.Model, origemProduto);
			AdicionarCommand = (ICommand)new DelegateCommand((Action)Adicionar, (Func<bool>)(() => true), (bool?)null);
		}

		private void Adicionar()
		{
			if (HelperObject.IsNull((object)EtiquetasConfiguracaoComboBox) || !((ViewModelValidationBase)EtiquetasConfiguracaoComboBox).Valido || !((ViewModelValidationBase)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).Valido || !((ViewModelValidationBase)((Lista<Filtro>)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios).Valido)
			{
				DialogoFactory.MostrarDialogoConfirma("A lista de itens não possui registros para adicionar", false, System.Windows.HorizontalAlignment.Center);
			}
			else
			{
				((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.AdicionarData();
			}
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto, bool isNotaEntrada)
		{
			string view = (isNotaEntrada ? "MovimentacaoEntradaListaView" : "MovimentacaoSaidaListaView");
			TipoComboBoxViewModel tipoComboBox = ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox;
			((Lista<Filtro>)tipoComboBox).Posicionar((Expression<Func<Filtro, bool>>)((Filtro x) => HelperObject.Is<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel>((object)x.ViewModel) && x.ViewModel.View.Equals(view)));
			BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel buscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel = ObterFiltroMovimentacaoProduto(((Lista<Filtro>)tipoComboBox).ItemSelecionado?.ViewModel, view);
			buscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel?.SetarMovimentacao(movimentacaoDto);
			foreach (EtiquetaConfiguracaoViewModel iten in ((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).Itens)
			{
				if (HelperObject.IsNotNull((object)buscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel) && HelperObject.Is<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel))
				{
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios.Adicionar((IEnumerable<IDado>)buscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel.DadosTemporarios.Itens);
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
				}
				if (HelperObject.Is<BuscaFiltroEntityMovimentacaoVolumeMovimentacaoVolumeSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel))
				{
					HelperObject.To<BuscaFiltroEntityMovimentacaoVolumeMovimentacaoVolumeSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel).SetarMovimentacao(movimentacaoDto);
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
				}
				if (HelperObject.Is<BuscaFiltroEntityMovimentacaoObjetoMovimentacaoObjetoSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel))
				{
					HelperObject.To<BuscaFiltroEntityMovimentacaoObjetoMovimentacaoObjetoSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel).SetarMovimentacao(movimentacaoDto);
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
				}
				if (HelperObject.Is<BuscaFiltroEntityMovimentacaoItemMovimentacaoItemSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel))
				{
					HelperObject.To<BuscaFiltroEntityMovimentacaoItemMovimentacaoItemSaidaViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel).SetarMovimentacao(movimentacaoDto);
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
				}
			}
		}

		private BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel ObterFiltroMovimentacaoProduto(BuscaFiltroViewModelBase viewModelSelecionado, string view)
		{
			if (HelperObject.Is<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel>((object)viewModelSelecionado))
			{
				return HelperObject.To<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel>((object)viewModelSelecionado);
			}
			if (!PossuiFiltroProdutoSelecionado())
			{
				return null;
			}
			return new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel(view);
		}

		private bool PossuiFiltroProdutoSelecionado()
		{
			return ((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).Itens.Any((EtiquetaConfiguracaoViewModel x) => HelperObject.Is<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)x.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel));
		}

		public void SetarProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresaDto, int quantidade = 1)
		{
			IEnumerable<ItemPrecoVendaDto> source = DadosSistema.GetService().Canal<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>((Func<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>)((ICompraModuleService service) => service.ObterPrecoVendaHistorico((IEnumerable<string>)new List<string> { ((EntityBaseDto)produtoServicoEmpresaDto).Id })));
			IEnumerable<DadoProduto> enumerable = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel().ObterDadoProduto(produtoServicoEmpresaDto, quantidade, source.FirstOrDefault());
			foreach (EtiquetaConfiguracaoViewModel iten in ((Lista<EtiquetaConfiguracaoViewModel>)EtiquetasConfiguracaoComboBox).Itens)
			{
				if (HelperObject.Is<BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel>((object)((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel))
				{
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios.Adicionar((IEnumerable<IDado>)enumerable);
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = produtoServicoEmpresaDto.ProdutoServico.Descricao;
					((Lista<Filtro>)((Lista<OrigemDado>)iten.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.NumeroEtiquetasPorItem = quantidade;
				}
			}
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new EtiquetaImpressaoFormulario(base.Model, _service, _navegar);
		}

		public string ToTituloFicha()
		{
			return "Etiquetas";
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)EtiquetasConfiguracaoComboBox).Dispose();
			EtiquetasConfiguracaoComboBox = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EtiquetaImpressaoConfiguracaoViewModel : ViewModelBase<EtiquetaImpressaoViewModel>
	{
		public OrigemDado OrigemDado => ((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)base.Model?.EtiquetasConfiguracaoComboBox)?.ItemSelecionado?.OrigemComboBox).ItemSelecionado;

		public override bool Valido => true;

		public EtiquetaImpressaoConfiguracaoViewModel(EtiquetaImpressaoViewModel etiquetaImpressaoViewModel)
			: base(etiquetaImpressaoViewModel)
		{
		}

		public override void ProjectedAsViewModel()
		{
			((Lista<EtiquetaConfiguracaoViewModel>)base.Model.EtiquetasConfiguracaoComboBox).AlterouItemSelecionadoEvent += EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent;
			EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent(null, null);
		}

		private void EtiquetasConfiguracaoComboBoxOnAlterouItemSelecionadoEvent(object sender, EtiquetaConfiguracaoViewModel etiquetaConfiguracaoViewModel)
		{
			if (!HelperObject.IsNull((object)OrigemDado))
			{
				((BindableBase)this).RaisePropertyChanged<OrigemDado>((Expression<Func<OrigemDado>>)(() => OrigemDado));
			}
		}
	}
	public class FiltroViewModel
	{
		public TipoComboBoxViewModel TipoComboBox { get; protected set; }

		public FiltroViewModel()
		{
			TipoComboBox = new TipoComboBoxViewModel();
		}
	}
	public class MovimentacaoItemFiltroViewModel : FiltroViewModel
	{
		public MovimentacaoItemFiltroViewModel()
		{
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroMovimentacaoItemMovimentacaoSaida());
			((Lista<Filtro>)base.TipoComboBox).PosicionarNoPrimeiro();
		}
	}
	public class MovimentacaoObjetoFiltroViewModel : FiltroViewModel
	{
		public MovimentacaoObjetoFiltroViewModel()
		{
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroMovimentacaoObjetoMovimentacaoSaida());
			((Lista<Filtro>)base.TipoComboBox).PosicionarNoPrimeiro();
		}
	}
	public class MovimentacaoVolumeFiltroViewModel : FiltroViewModel
	{
		public MovimentacaoVolumeFiltroViewModel()
		{
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroMovimentacaoVolumeMovimentacaoSaida());
			((Lista<Filtro>)base.TipoComboBox).PosicionarNoPrimeiro();
		}
	}
	public class PessoaFiltroViewModel : FiltroViewModel
	{
		public PessoaFiltroViewModel()
		{
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroPessoa());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroPessoaMovimentacaoEntrada());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroPessoaMovimentacaoSaida());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroMunicipio());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroUf());
			((Lista<Filtro>)base.TipoComboBox).PosicionarNoPrimeiro();
		}
	}
	public class ProdutoFiltroViewModel : FiltroViewModel
	{
		public ProdutoFiltroViewModel()
		{
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroProduto());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroSubGrupo());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroMarca());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroPromocao());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroProdutoMovimentacaoEntrada());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroProdutoMovimentacaoSaida());
			((Lista<Filtro>)base.TipoComboBox).Adicionar((Filtro)new FiltroProdutoOrdemProducao());
			((Lista<Filtro>)base.TipoComboBox).PosicionarNoPrimeiro();
		}
	}
}
namespace Otus.Modulo.Etiquetas.ViewModels.EtiquetasImpressao.Tipos
{
	public class BuscaFiltroEntityMovimentacaoItemMovimentacaoItemSaidaViewModel : BuscaFiltroMovimentacaoItemViewModel<MovimentacaoDto>
	{
		public BuscaFiltroEntityMovimentacaoItemMovimentacaoItemSaidaViewModel()
			: base("MovimentacaoSaidaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate(MovimentacaoDto movimentacao)
			{
				base.DescricaoReferencia = ((BaseDto)movimentacao).ToText;
				base.DadosTemporarios.Limpar();
				List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacao };
				AdicionarItensTemporarios(lista);
			}, true);
			obj.AdicionarParametro("OcultarCheckbox", (object)true);
			return (ISelecaoCommandParameter)(object)obj;
		}

		public override void AdicionarItensTemporarios(IEnumerable<MovimentacaoDto> lista)
		{
			foreach (MovimentacaoDto listum in lista)
			{
				foreach (ItemDto item in listum.Itens.Where((ItemDto x) => !x.Cancelado))
				{
					EstoqueDto estoque = item.Estoque;
					if (estoque != null && !estoque.IsNormal)
					{
						EstoqueDto estoque2 = item.Estoque;
						if (estoque2 != null && HelperObject.Is<EstoqueGradeDto>((object)estoque2))
						{
							AdicionarEstoqueGrade(listum, item);
						}
						EstoqueDto estoque3 = item.Estoque;
						if (estoque3 != null && HelperObject.Is<EstoqueSerialDto>((object)estoque3))
						{
							AdicionarEstoqueSerial(listum, item);
						}
						EstoqueDto estoque4 = item.Estoque;
						if (estoque4 != null && HelperObject.Is<EstoqueLoteDto>((object)estoque4))
						{
							AdicionarEstoqueLote(listum, item);
						}
					}
					else if (UtilizarQuantidadeDisponivel)
					{
						base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacao(listum, item, item.Quantidade));
					}
					else
					{
						base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacao(listum, item, 1m));
					}
				}
			}
		}

		public void AdicionarEstoqueGrade(MovimentacaoDto movimentacao, ItemDto movimentacaoItem)
		{
			foreach (GradeDto grade in HelperObject.To<EstoqueGradeDto>((object)movimentacaoItem.Estoque).Grades)
			{
				if (UtilizarQuantidadeDisponivel)
				{
					base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacaoEstoqueEspecial(movimentacao, movimentacaoItem, grade.Estoque.Quantidade, $"Linha: {grade.Linha.Descricao} Coluna: {grade.Coluna.Descricao}"));
				}
				else
				{
					base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacaoEstoqueEspecial(movimentacao, movimentacaoItem, 1m, $"Linha: {grade.Linha.Descricao} Coluna: {grade.Coluna.Descricao}"));
				}
			}
		}

		public void AdicionarEstoqueSerial(MovimentacaoDto movimentacao, ItemDto movimentacaoItem)
		{
			foreach (SerialDto seriai in HelperObject.To<EstoqueSerialDto>((object)movimentacaoItem.Estoque).Seriais)
			{
				base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacaoEstoqueEspecial(movimentacao, movimentacaoItem, 1m, ((BaseDto)seriai).ToText));
			}
		}

		public void AdicionarEstoqueLote(MovimentacaoDto movimentacao, ItemDto movimentacaoItem)
		{
			foreach (LoteDto lote in HelperObject.To<EstoqueLoteDto>((object)movimentacaoItem.Estoque).Lotes)
			{
				if (UtilizarQuantidadeDisponivel)
				{
					base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacaoEstoqueEspecial(movimentacao, movimentacaoItem, lote.Estoque.Quantidade, ((BaseDto)lote).ToText));
				}
				else
				{
					base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacaoEstoqueEspecial(movimentacao, movimentacaoItem, 1m, ((BaseDto)lote).ToText));
				}
			}
		}

		private DadoMovimentacaoItem PreencherDadoMovimentacao(MovimentacaoDto movimentacao, ItemDto item, decimal quantidade)
		{
			string nome = ((MovimentacaoBaseDto)movimentacao).Pessoa.Nome;
			string numero = ((MovimentacaoBaseDto)movimentacao).Pessoa.TelefonePrincipal.Numero;
			string logradouro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Logradouro;
			string numero2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Numero;
			string bairro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Bairro;
			string cep = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Cep;
			MunicipioDto municipio = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			string enderecoPrincipalMunicipio = ((municipio != null) ? municipio.Nome : null) ?? string.Empty;
			MunicipioDto municipio2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj;
			if (municipio2 == null)
			{
				obj = null;
			}
			else
			{
				UFDto uF = municipio2.UF;
				obj = ((uF != null) ? uF.Sigla : null);
			}
			if (obj == null)
			{
				obj = string.Empty;
			}
			MunicipioDto municipio3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj2;
			if (municipio3 == null)
			{
				obj2 = null;
			}
			else
			{
				UFDto uF2 = municipio3.UF;
				if (uF2 == null)
				{
					obj2 = null;
				}
				else
				{
					PaisDto pais = uF2.Pais;
					obj2 = ((pais != null) ? pais.Nome : null);
				}
			}
			if (obj2 == null)
			{
				obj2 = string.Empty;
			}
			string logradouro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Logradouro;
			string numero3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Numero;
			string bairro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Bairro;
			string cep2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Cep;
			string nome2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.Nome;
			string sigla = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Sigla;
			string nome3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Pais.Nome;
			string nomeFantasia = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeFantasia;
			string nomeDoResponsavel = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeDoResponsavel;
			string cpfCnpjFormatado = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string cpfCnpjFormatado2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string endereco = ((MovimentacaoBaseDto)movimentacao).Pessoa.EmailPrincipal.Endereco;
			long quantidadeEtiquetas = (long)Math.Ceiling(quantidade);
			string observacao = ((MovimentacaoBaseDto)movimentacao).Pessoa.Observacao;
			long numero4 = ((MovimentacaoBaseDto)movimentacao).Numero;
			DateTime dataHoraEmissao = ((MovimentacaoBaseDto)movimentacao).DataHoraEmissao;
			decimal total = ((MovimentacaoBaseDto)movimentacao).Total;
			string chaveAcesso = ((MovimentacaoBaseDto)movimentacao).ChaveAcesso;
			string nome4 = movimentacao.TecnicoResponsavel.Nome;
			string nome5 = movimentacao.VendedorResponsavel.Nome;
			string descricao = movimentacao.Prioridade.Descricao;
			decimal totalPesoBruto = movimentacao.TotalPesoBruto;
			decimal totalPesoLiquido = movimentacao.TotalPesoLiquido;
			decimal quantidade2 = item.Quantidade;
			ProdutoServicoDto produtoServico = item.ProdutoServico;
			string movimentacaoItemDescricao = ((produtoServico != null) ? produtoServico.Descricao : null);
			UnidadeMedidaDto unidadeMedida = item.UnidadeMedida;
			string movimentacaoItemUnidade = ((unidadeMedida != null) ? unidadeMedida.Sigla : null);
			decimal total2 = item.Total;
			decimal totalPesoLiquido2 = item.TotalPesoLiquido;
			decimal totalPesoBruto2 = item.TotalPesoBruto;
			string observacaoDigitada = item.ObservacaoDigitada;
			ProdutoServicoDto produtoServico2 = item.ProdutoServico;
			string movimentacaoItemCodigoInterno = ((produtoServico2 != null) ? produtoServico2.CodigoInterno : null);
			ProdutoServicoDto produtoServico3 = item.ProdutoServico;
			object movimentacaoItemCodigoBarras;
			if (produtoServico3 == null)
			{
				movimentacaoItemCodigoBarras = null;
			}
			else
			{
				ProdutoDto produto = produtoServico3.Produto;
				movimentacaoItemCodigoBarras = ((produto != null) ? produto.CodigoBarras : null);
			}
			ProdutoServicoDto produtoServico4 = item.ProdutoServico;
			object movimentacaoItemCodigoBarrasTributavel;
			if (produtoServico4 == null)
			{
				movimentacaoItemCodigoBarrasTributavel = null;
			}
			else
			{
				ProdutoDto produto2 = produtoServico4.Produto;
				movimentacaoItemCodigoBarrasTributavel = ((produto2 != null) ? produto2.CodigoBarrasTributavel : null);
			}
			return new DadoMovimentacaoItem(nome, numero, logradouro, numero2, bairro, cep, enderecoPrincipalMunicipio, (string)obj, (string)obj2, logradouro2, numero3, bairro2, cep2, nome2, sigla, nome3, nomeFantasia, nomeDoResponsavel, cpfCnpjFormatado, cpfCnpjFormatado2, endereco, quantidadeEtiquetas, observacao, numero4, dataHoraEmissao, total, chaveAcesso, nome4, nome5, descricao, totalPesoBruto, totalPesoLiquido, quantidade2, movimentacaoItemDescricao, movimentacaoItemUnidade, total2, totalPesoLiquido2, totalPesoBruto2, observacaoDigitada, movimentacaoItemCodigoInterno, (string)movimentacaoItemCodigoBarras, (string)movimentacaoItemCodigoBarrasTributavel, string.Empty);
		}

		private DadoMovimentacaoItem PreencherDadoMovimentacaoEstoqueEspecial(MovimentacaoDto movimentacao, ItemDto item, decimal quantidade, string informacaoEstoque)
		{
			string nome = ((MovimentacaoBaseDto)movimentacao).Pessoa.Nome;
			string numero = ((MovimentacaoBaseDto)movimentacao).Pessoa.TelefonePrincipal.Numero;
			string logradouro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Logradouro;
			string numero2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Numero;
			string bairro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Bairro;
			string cep = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Cep;
			MunicipioDto municipio = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			string enderecoPrincipalMunicipio = ((municipio != null) ? municipio.Nome : null) ?? string.Empty;
			MunicipioDto municipio2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj;
			if (municipio2 == null)
			{
				obj = null;
			}
			else
			{
				UFDto uF = municipio2.UF;
				obj = ((uF != null) ? uF.Sigla : null);
			}
			if (obj == null)
			{
				obj = string.Empty;
			}
			MunicipioDto municipio3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj2;
			if (municipio3 == null)
			{
				obj2 = null;
			}
			else
			{
				UFDto uF2 = municipio3.UF;
				if (uF2 == null)
				{
					obj2 = null;
				}
				else
				{
					PaisDto pais = uF2.Pais;
					obj2 = ((pais != null) ? pais.Nome : null);
				}
			}
			if (obj2 == null)
			{
				obj2 = string.Empty;
			}
			string logradouro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Logradouro;
			string numero3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Numero;
			string bairro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Bairro;
			string cep2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Cep;
			string nome2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.Nome;
			string sigla = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Sigla;
			string nome3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Pais.Nome;
			string nomeFantasia = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeFantasia;
			string nomeDoResponsavel = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeDoResponsavel;
			string cpfCnpjFormatado = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string cpfCnpjFormatado2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string endereco = ((MovimentacaoBaseDto)movimentacao).Pessoa.EmailPrincipal.Endereco;
			long quantidadeEtiquetas = (long)Math.Ceiling(quantidade);
			string observacao = ((MovimentacaoBaseDto)movimentacao).Pessoa.Observacao;
			long numero4 = ((MovimentacaoBaseDto)movimentacao).Numero;
			DateTime dataHoraEmissao = ((MovimentacaoBaseDto)movimentacao).DataHoraEmissao;
			decimal total = ((MovimentacaoBaseDto)movimentacao).Total;
			string chaveAcesso = ((MovimentacaoBaseDto)movimentacao).ChaveAcesso;
			string nome4 = movimentacao.TecnicoResponsavel.Nome;
			string nome5 = movimentacao.VendedorResponsavel.Nome;
			string descricao = movimentacao.Prioridade.Descricao;
			decimal totalPesoBruto = movimentacao.TotalPesoBruto;
			decimal totalPesoLiquido = movimentacao.TotalPesoLiquido;
			decimal quantidade2 = item.Quantidade;
			ProdutoServicoDto produtoServico = item.ProdutoServico;
			string movimentacaoItemDescricao = ((produtoServico != null) ? produtoServico.Descricao : null);
			UnidadeMedidaDto unidadeMedida = item.UnidadeMedida;
			string movimentacaoItemUnidade = ((unidadeMedida != null) ? unidadeMedida.Sigla : null);
			decimal total2 = item.Total;
			decimal totalPesoLiquido2 = item.TotalPesoLiquido;
			decimal totalPesoBruto2 = item.TotalPesoBruto;
			string observacaoDigitada = item.ObservacaoDigitada;
			ProdutoServicoDto produtoServico2 = item.ProdutoServico;
			string movimentacaoItemCodigoInterno = ((produtoServico2 != null) ? produtoServico2.CodigoInterno : null);
			ProdutoServicoDto produtoServico3 = item.ProdutoServico;
			object movimentacaoItemCodigoBarras;
			if (produtoServico3 == null)
			{
				movimentacaoItemCodigoBarras = null;
			}
			else
			{
				ProdutoDto produto = produtoServico3.Produto;
				movimentacaoItemCodigoBarras = ((produto != null) ? produto.CodigoBarras : null);
			}
			ProdutoServicoDto produtoServico4 = item.ProdutoServico;
			object movimentacaoItemCodigoBarrasTributavel;
			if (produtoServico4 == null)
			{
				movimentacaoItemCodigoBarrasTributavel = null;
			}
			else
			{
				ProdutoDto produto2 = produtoServico4.Produto;
				movimentacaoItemCodigoBarrasTributavel = ((produto2 != null) ? produto2.CodigoBarrasTributavel : null);
			}
			return new DadoMovimentacaoItem(nome, numero, logradouro, numero2, bairro, cep, enderecoPrincipalMunicipio, (string)obj, (string)obj2, logradouro2, numero3, bairro2, cep2, nome2, sigla, nome3, nomeFantasia, nomeDoResponsavel, cpfCnpjFormatado, cpfCnpjFormatado2, endereco, quantidadeEtiquetas, observacao, numero4, dataHoraEmissao, total, chaveAcesso, nome4, nome5, descricao, totalPesoBruto, totalPesoLiquido, quantidade2, movimentacaoItemDescricao, movimentacaoItemUnidade, total2, totalPesoLiquido2, totalPesoBruto2, observacaoDigitada, movimentacaoItemCodigoInterno, (string)movimentacaoItemCodigoBarras, (string)movimentacaoItemCodigoBarrasTributavel, informacaoEstoque);
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto)
		{
			List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacaoDto };
			AdicionarItensTemporarios(lista);
		}

		protected override void PreencherNumeroEtiquetasPorItem()
		{
			foreach (DadoMovimentacaoItem item in base.DadosTemporarios.Itens.OfType<DadoMovimentacaoItem>())
			{
				item.SetarQuantidadeEtiquetas(UtilizarQuantidadeDisponivel ? (decimal.ToInt32(item.MovimentacaoItemQuantidade) * base.NumeroEtiquetasPorItem) : base.NumeroEtiquetasPorItem);
			}
		}

		public override void Limpar()
		{
			base.Limpar();
			UtilizarQuantidadeDisponivel = false;
		}
	}
	public class BuscaFiltroEntityMovimentacaoObjetoMovimentacaoObjetoSaidaViewModel : BuscaFiltroMovimentacaoObjetoViewModel<MovimentacaoDto>
	{
		public BuscaFiltroEntityMovimentacaoObjetoMovimentacaoObjetoSaidaViewModel()
			: base("MovimentacaoSaidaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate(MovimentacaoDto movimentacao)
			{
				base.DescricaoReferencia = ((BaseDto)movimentacao).ToText;
				base.DadosTemporarios.Limpar();
				List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacao };
				AdicionarItensTemporarios(lista);
			}, true);
			obj.AdicionarParametro("OcultarCheckbox", (object)true);
			return (ISelecaoCommandParameter)(object)obj;
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto)
		{
			List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacaoDto };
			AdicionarItensTemporarios(lista);
		}
	}
	public class BuscaFiltroEntityMovimentacaoVolumeMovimentacaoVolumeSaidaViewModel : BuscaFiltroMovimentacaoVolumeViewModel<MovimentacaoDto>
	{
		public BuscaFiltroEntityMovimentacaoVolumeMovimentacaoVolumeSaidaViewModel()
			: base("MovimentacaoSaidaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate(MovimentacaoDto movimentacao)
			{
				base.DescricaoReferencia = ((BaseDto)movimentacao).ToText;
				base.DadosTemporarios.Limpar();
				List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacao };
				AdicionarItensTemporarios(lista);
			}, true);
			obj.AdicionarParametro("OcultarCheckbox", (object)true);
			return (ISelecaoCommandParameter)(object)obj;
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto)
		{
			List<MovimentacaoDto> lista = new List<MovimentacaoDto> { movimentacaoDto };
			AdicionarItensTemporarios(lista);
		}
	}
	public class BuscaFiltroEntityMunicipioViewModel : BuscaFiltroViewModel<MunicipioDto>
	{
		public BuscaFiltroEntityMunicipioViewModel()
			: base("MunicipioListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MunicipioDto, IPessoaModuleService, Task<IEnumerable<PessoaDto>>>(base.View, (Func<MunicipioDto, Func<IPessoaModuleService, Task<IEnumerable<PessoaDto>>>>)delegate(MunicipioDto dto)
			{
				base.DescricaoReferencia = dto.Nome;
				return (IPessoaModuleService service) => service.PesquisarAtivosPorEnderecoPrincipalAsync(dto.CodigoIbge.ToString());
			}, (Action<Task<IEnumerable<PessoaDto>>>)async delegate(Task<IEnumerable<PessoaDto>> resultado)
			{
				AdicionarItensTemporarios(await resultado);
			}, true);
		}
	}
	public class BuscaFiltroEntityPessoaMovimentacaoEntradaViewModel : BuscaFiltroViewModel<PessoaDto>
	{
		public BuscaFiltroEntityPessoaMovimentacaoEntradaViewModel()
			: base("MovimentacaoEntradaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoEntradaListaView", (Action<MovimentacaoDto>)delegate(MovimentacaoDto movimentacao)
			{
				base.DescricaoReferencia = ((BaseDto)((MovimentacaoBaseDto)movimentacao).Pessoa).ToText;
				base.DadosTemporarios.Limpar();
				List<PessoaDto> lista = new List<PessoaDto> { ((MovimentacaoBaseDto)movimentacao).Pessoa };
				AdicionarItensTemporarios(lista);
			}, true);
			obj.AdicionarParametro("OcultarCheckbox", (object)true);
			return (ISelecaoCommandParameter)(object)obj;
		}
	}
	public class BuscaFiltroEntityPessoaMovimentacaoSaidaViewModel : BuscaFiltroViewModel<PessoaDto>
	{
		public BuscaFiltroEntityPessoaMovimentacaoSaidaViewModel()
			: base("MovimentacaoSaidaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate(MovimentacaoDto movimentacao)
			{
				base.DescricaoReferencia = ((BaseDto)((MovimentacaoBaseDto)movimentacao).Pessoa).ToText;
				base.DadosTemporarios.Limpar();
				List<PessoaDto> lista = new List<PessoaDto> { ((MovimentacaoBaseDto)movimentacao).Pessoa };
				AdicionarItensTemporarios(lista);
			}, true);
			obj.AdicionarParametro("OcultarCheckbox", (object)true);
			return (ISelecaoCommandParameter)(object)obj;
		}
	}
	public class BuscaFiltroEntityPessoaViewModel : BuscaFiltroViewModel<PessoaDto>
	{
		public BuscaFiltroEntityPessoaViewModel()
			: base("PessoaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<PessoaDto>>(base.View, (Action<IEnumerable<PessoaDto>>)delegate(IEnumerable<PessoaDto> pessoa)
			{
				base.DescricaoReferencia = ((pessoa.Count() > 1) ? $"{pessoa.Count()} Pessoas selecionados(as)" : (((BaseDto)pessoa.FirstOrDefault()).ToText ?? string.Empty));
				base.DadosTemporarios.Limpar();
				List<PessoaDto> lista = new List<PessoaDto>(pessoa);
				AdicionarItensTemporarios(lista);
			}, true);
		}
	}
	public class BuscaFiltroEntityUfViewModel : BuscaFiltroViewModel<UFDto>
	{
		public BuscaFiltroEntityUfViewModel()
			: base("UFListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<UFDto, IPessoaModuleService, Task<IEnumerable<PessoaDto>>>(base.View, (Func<UFDto, Func<IPessoaModuleService, Task<IEnumerable<PessoaDto>>>>)delegate(UFDto dto)
			{
				base.DescricaoReferencia = ((BaseDto)dto).ToText;
				return (IPessoaModuleService service) => service.PesquisarAtivosPorEnderecoPrincipalAsync(dto.CodigoIbge.ToString());
			}, (Action<Task<IEnumerable<PessoaDto>>>)async delegate(Task<IEnumerable<PessoaDto>> resultado)
			{
				AdicionarItensTemporarios(await resultado);
			}, true);
		}
	}
	public class BuscaFiltroMovimentacaoItemViewModel<T> : BuscaFiltroQuantidadeDisponivelViewModelBase where T : BaseDto
	{
		public BuscaFiltroMovimentacaoItemViewModel(string view)
			: base(view)
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<T>(base.View, (Action<T>)delegate
			{
			}, true);
		}
	}
	public class BuscaFiltroMovimentacaoObjetoViewModel<T> : BuscaFiltroViewModelBase where T : BaseDto
	{
		public BuscaFiltroMovimentacaoObjetoViewModel(string view)
			: base(view)
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<T>(base.View, (Action<T>)delegate
			{
			}, true);
		}

		public void AdicionarItensTemporarios(IEnumerable<MovimentacaoDto> lista)
		{
			foreach (MovimentacaoDto listum in lista)
			{
				if (listum.Objetos.Count() > 1)
				{
					foreach (MovimentacaoObjetoDto objeto in listum.Objetos)
					{
						base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacao(listum, objeto));
					}
				}
				else
				{
					base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacao(listum, listum.Objetos.FirstOrDefault()));
				}
			}
		}

		private DadoMovimentacaoObjeto PreencherDadoMovimentacao(MovimentacaoDto movimentacao, MovimentacaoObjetoDto movimentacaoObjeto)
		{
			string nome = ((MovimentacaoBaseDto)movimentacao).Pessoa.Nome;
			string numero = ((MovimentacaoBaseDto)movimentacao).Pessoa.TelefonePrincipal.Numero;
			string logradouro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Logradouro;
			string numero2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Numero;
			string bairro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Bairro;
			string cep = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Cep;
			MunicipioDto municipio = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			string enderecoPrincipalMunicipio = ((municipio != null) ? municipio.Nome : null) ?? string.Empty;
			MunicipioDto municipio2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj;
			if (municipio2 == null)
			{
				obj = null;
			}
			else
			{
				UFDto uF = municipio2.UF;
				obj = ((uF != null) ? uF.Sigla : null);
			}
			if (obj == null)
			{
				obj = string.Empty;
			}
			MunicipioDto municipio3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj2;
			if (municipio3 == null)
			{
				obj2 = null;
			}
			else
			{
				UFDto uF2 = municipio3.UF;
				if (uF2 == null)
				{
					obj2 = null;
				}
				else
				{
					PaisDto pais = uF2.Pais;
					obj2 = ((pais != null) ? pais.Nome : null);
				}
			}
			if (obj2 == null)
			{
				obj2 = string.Empty;
			}
			return new DadoMovimentacaoObjeto(nome, numero, logradouro, numero2, bairro, cep, enderecoPrincipalMunicipio, (string)obj, (string)obj2, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Logradouro, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Numero, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Bairro, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Cep, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.Nome, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Sigla, ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Pais.Nome, ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeFantasia, ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeDoResponsavel, ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado, ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado, ((MovimentacaoBaseDto)movimentacao).Pessoa.EmailPrincipal.Endereco, base.NumeroEtiquetasPorItem, ((MovimentacaoBaseDto)movimentacao).Pessoa.Observacao, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Descricao : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Serial : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Placa : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Chassi : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Modelo : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Marca : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Renavam : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Cor : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.AnoFabricacao : 0, (movimentacaoObjeto != null) ? movimentacaoObjeto.Defeito : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Diagnostico : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.DataGarantia : DateTime.Now, ((MovimentacaoBaseDto)movimentacao).Numero, ((MovimentacaoBaseDto)movimentacao).DataHoraEmissao, ((MovimentacaoBaseDto)movimentacao).Total, ((MovimentacaoBaseDto)movimentacao).ChaveAcesso, movimentacao.TecnicoResponsavel.Nome, movimentacao.VendedorResponsavel.Nome, movimentacao.Prioridade.Descricao, movimentacao.TotalPesoBruto, movimentacao.TotalPesoLiquido, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Cilindrada : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Potencia : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Motor : null, (movimentacaoObjeto != null) ? movimentacaoObjeto.Objeto.Segmento : null);
		}
	}
	public class BuscaFiltroMovimentacaoVolumeViewModel<T> : BuscaFiltroViewModelBase where T : BaseDto
	{
		public BuscaFiltroMovimentacaoVolumeViewModel(string view)
			: base(view)
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<T>(base.View, (Action<T>)delegate
			{
			}, true);
		}

		public void AdicionarItensTemporarios(IEnumerable<MovimentacaoDto> lista)
		{
			foreach (MovimentacaoDto listum in lista)
			{
				int num = listum.Volumes.Sum((VolumeDto x) => x.Quantidade);
				int num2 = 1;
				foreach (VolumeDto volume in listum.Volumes)
				{
					for (int i = 0; i < volume.Quantidade; i++)
					{
						base.DadosTemporarios.Adicionar((IDado)PreencherDadoMovimentacao(listum, volume, base.NumeroEtiquetasPorItem, $"{num2}/{num}"));
						num2++;
					}
				}
			}
		}

		private DadoMovimentacaoVolume PreencherDadoMovimentacao(MovimentacaoDto movimentacao, VolumeDto volume, long etiquetasPorItem, string volumeContador)
		{
			string nome = ((MovimentacaoBaseDto)movimentacao).Pessoa.Nome;
			string numero = ((MovimentacaoBaseDto)movimentacao).Pessoa.TelefonePrincipal.Numero;
			string logradouro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Logradouro;
			string numero2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Numero;
			string bairro = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Bairro;
			string cep = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Cep;
			MunicipioDto municipio = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			string enderecoPrincipalMunicipio = ((municipio != null) ? municipio.Nome : null) ?? string.Empty;
			MunicipioDto municipio2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj;
			if (municipio2 == null)
			{
				obj = null;
			}
			else
			{
				UFDto uF = municipio2.UF;
				obj = ((uF != null) ? uF.Sigla : null);
			}
			if (obj == null)
			{
				obj = string.Empty;
			}
			MunicipioDto municipio3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoPrincipal.Municipio;
			object obj2;
			if (municipio3 == null)
			{
				obj2 = null;
			}
			else
			{
				UFDto uF2 = municipio3.UF;
				if (uF2 == null)
				{
					obj2 = null;
				}
				else
				{
					PaisDto pais = uF2.Pais;
					obj2 = ((pais != null) ? pais.Nome : null);
				}
			}
			if (obj2 == null)
			{
				obj2 = string.Empty;
			}
			string logradouro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Logradouro;
			string numero3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Numero;
			string bairro2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Bairro;
			string cep2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Cep;
			string nome2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.Nome;
			string sigla = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Sigla;
			string nome3 = ((MovimentacaoBaseDto)movimentacao).Pessoa.EnderecoEntrega.Municipio.UF.Pais.Nome;
			string nomeFantasia = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeFantasia;
			string nomeDoResponsavel = ((MovimentacaoBaseDto)movimentacao).Pessoa.Juridica.NomeDoResponsavel;
			string cpfCnpjFormatado = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string cpfCnpjFormatado2 = ((MovimentacaoBaseDto)movimentacao).Pessoa.CpfCnpjFormatado;
			string endereco = ((MovimentacaoBaseDto)movimentacao).Pessoa.EmailPrincipal.Endereco;
			string observacao = ((MovimentacaoBaseDto)movimentacao).Pessoa.Observacao;
			int volumeQuantidade = ((volume != null) ? volume.Quantidade : 0);
			string volumeEspecie = ((volume != null) ? volume.Especie : null);
			string volumeMarca = ((volume != null) ? volume.Marca : null);
			string volumeNumeracao = ((volume != null) ? volume.Numeracao : null);
			decimal volumePesoLiquido = ((volume != null) ? volume.PesoLiquido : 0m);
			decimal volumePesoBruto = ((volume != null) ? volume.PesoBruto : 0m);
			long numero4 = ((MovimentacaoBaseDto)movimentacao).Numero;
			DateTime dataHoraEmissao = ((MovimentacaoBaseDto)movimentacao).DataHoraEmissao;
			decimal total = ((MovimentacaoBaseDto)movimentacao).Total;
			string chaveAcesso = ((MovimentacaoBaseDto)movimentacao).ChaveAcesso;
			string nome4 = movimentacao.TecnicoResponsavel.Nome;
			string nome5 = movimentacao.VendedorResponsavel.Nome;
			string descricao = movimentacao.Prioridade.Descricao;
			decimal totalPesoBruto = movimentacao.TotalPesoBruto;
			decimal totalPesoLiquido = movimentacao.TotalPesoLiquido;
			PessoaDto transportadora = movimentacao.Transportadora;
			string transportadoraNome = ((transportadora != null) ? transportadora.Nome : null);
			PessoaDto transportadora2 = movimentacao.Transportadora;
			object transportadoraNomeFantasia;
			if (transportadora2 == null)
			{
				transportadoraNomeFantasia = null;
			}
			else
			{
				JuridicaDto juridica = transportadora2.Juridica;
				transportadoraNomeFantasia = ((juridica != null) ? juridica.NomeFantasia : null);
			}
			PessoaDto transportadora3 = movimentacao.Transportadora;
			return new DadoMovimentacaoVolume(nome, numero, logradouro, numero2, bairro, cep, enderecoPrincipalMunicipio, (string)obj, (string)obj2, logradouro2, numero3, bairro2, cep2, nome2, sigla, nome3, nomeFantasia, nomeDoResponsavel, cpfCnpjFormatado, cpfCnpjFormatado2, endereco, etiquetasPorItem, observacao, volumeQuantidade, volumeEspecie, volumeMarca, volumeNumeracao, volumePesoLiquido, volumePesoBruto, numero4, dataHoraEmissao, total, chaveAcesso, nome4, nome5, descricao, totalPesoBruto, totalPesoLiquido, transportadoraNome, (string)transportadoraNomeFantasia, (transportadora3 != null) ? transportadora3.CpfCnpjFormatado : null, volumeContador);
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMarcaViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<MarcaDto>
	{
		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMarcaViewModel()
			: base("MarcaListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			return (ISelecaoCommandParameter)(object)new SelecaoPersonalizadoCommandParameter<MarcaDto>(base.View, (Action<MarcaDto>)delegate(MarcaDto dto)
			{
				base.DescricaoReferencia = dto.Descricao;
				base.DadosTemporarios.Limpar();
				AdicionarItensTemporarios(dto.ProdutosServicos);
			}, (IParametroPersonalizado)new ParametroAtualizarDtoSelecionadoComProdutoServico());
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<ProdutoServicoEmpresaDto>
	{
		public override bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value, (Action)PreencherNumeroEtiquetasPorItem);
			}
		}

		public MovimentacaoDto MovimentacaoDto
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => MovimentacaoDto));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => MovimentacaoDto), value, (Action)delegate
				{
					base.DescricaoReferencia = ((MovimentacaoBaseDto)value).Pessoa.Nome;
				});
			}
		}

		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel(string listaView)
			: base(listaView)
		{
		}

		protected override void PreencherNumeroEtiquetasPorItem()
		{
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				item.SetarQuantidadeEtiquetas(UtilizarQuantidadeDisponivel ? (decimal.ToInt32(item.QuantidadeEstoque) * base.NumeroEtiquetasPorItem) : base.NumeroEtiquetasPorItem);
			}
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MovimentacaoDto, ICompraModuleService, IEnumerable<ProdutoServicoEmpresaDto>>(base.View, (Func<MovimentacaoDto, Func<ICompraModuleService, IEnumerable<ProdutoServicoEmpresaDto>>>)delegate(MovimentacaoDto dto)
			{
				MovimentacaoDto = dto;
				return (ICompraModuleService service) => service.ObterProdutoServicoEmpresaMovimentacao((MovimentacaoBaseDto)(object)MovimentacaoDto);
			}, (Action<IEnumerable<ProdutoServicoEmpresaDto>>)PreencherDadosMovimentacaoRetornada, true);
		}

		private void PreencherDadosMovimentacaoRetornada(IEnumerable<ProdutoServicoEmpresaDto> resultado)
		{
			base.DadosTemporarios.Limpar();
			IEnumerable<string> produtoServicoEmpresaReferencias = MovimentacaoDto.Itens.Select((ItemDto x) => x.ProdutoServicoEmpresaReferencia).Distinct();
			IEnumerable<ItemPrecoVendaDto> itensPrecoVenda = DadosSistema.GetService().Canal<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>((Func<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>)((ICompraModuleService service) => service.ObterPrecoVendaHistorico(produtoServicoEmpresaReferencias)));
			CarregarPrecosAgendados(resultado.Select((ProdutoServicoEmpresaDto x) => ((EntityBaseDto)x.Preco).Id));
			AdicionarItensTemporarios(MovimentacaoDto.Itens, resultado, itensPrecoVenda);
			AvisarQuandoNenhumItemPossuiPrecoAgendado();
		}

		public void SetarMovimentacao(MovimentacaoDto movimentacaoDto)
		{
			MovimentacaoDto = movimentacaoDto;
			IEnumerable<ProdutoServicoEmpresaDto> resultado = DadosSistema.GetService().Canal<ICompraModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<ICompraModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((ICompraModuleService x) => x.ObterProdutoServicoEmpresaMovimentacao((MovimentacaoBaseDto)(object)MovimentacaoDto)));
			PreencherDadosMovimentacaoRetornada(resultado);
		}

		private void AdicionarItensTemporarios(IEnumerable<ItemDto> lista, IEnumerable<ProdutoServicoEmpresaDto> produtosServicoEmpresa, IEnumerable<ItemPrecoVendaDto> itensPrecoVenda)
		{
			foreach (ItemDto item in lista)
			{
				ProdutoServicoEmpresaDto produtoServicoEmpresaDto = produtosServicoEmpresa.FirstOrDefault((ProdutoServicoEmpresaDto x) => ((EntityBaseDto)x.ProdutoServico).Id == ((EntityBaseDto)item.ProdutoServico).Id);
				ItemPrecoVendaDto itemPrecoVenda = itensPrecoVenda.FirstOrDefault((ItemPrecoVendaDto x) => x.ProdutoServicoEmpresaReferencia == item.ProdutoServicoEmpresaReferencia);
				base.DadosTemporarios.Adicionar((IEnumerable<IDado>)ObterDadoProduto(item, produtoServicoEmpresaDto, base.NumeroEtiquetasPorItem, itemPrecoVenda));
			}
		}

		public IEnumerable<DadoProduto> ObterDadoProduto(ItemDto itemDto, ProdutoServicoEmpresaDto produtoServicoEmpresaDto, long etiquetasPorItem, ItemPrecoVendaDto itemPrecoVenda)
		{
			DadoProdutoFactory dadoProdutoFactory = new DadoProdutoFactory().SetarCodigoInterno(produtoServicoEmpresaDto.ProdutoServico.CodigoInterno).SetarDescricao(produtoServicoEmpresaDto.ProdutoServico.Descricao).SetarSiglaUnidadeMedida(produtoServicoEmpresaDto.ProdutoServico.UnidadeMedida.Sigla)
				.SetarCaracteristica(produtoServicoEmpresaDto.ProdutoServico.Caracteristica)
				.SetarMarca(produtoServicoEmpresaDto.ProdutoServico.Produto.Marca.Descricao)
				.SetarObservacao(produtoServicoEmpresaDto.ProdutoServico.Observacao)
				.SetarPesoLiquido(produtoServicoEmpresaDto.ProdutoServico.Produto.PesoLiquido)
				.SetarPesoBruto(produtoServicoEmpresaDto.ProdutoServico.Produto.PesoBruto)
				.SetarCodigoBarras(itemDto.ProdutoServico.Produto.CodigoBarras)
				.SetarCodigoBarrasTributavel(itemDto.ProdutoServico.Produto.CodigoBarrasTributavel)
				.SetarBaseCalculoPrecoReferencia(produtoServicoEmpresaDto.ProdutoEmpresa.ReferenciaPrecoEtiqueta.BaseCalculo)
				.SetarDescricaoPrecoReferencia(produtoServicoEmpresaDto.ProdutoEmpresa.ReferenciaPrecoEtiqueta.ToText)
				.SetarEtiquetasPorItem(etiquetasPorItem)
				.SetarPrecoCusto(produtoServicoEmpresaDto.Preco.Custo.Valor)
				.SetarPrecoVendaVigente(produtoServicoEmpresaDto.Preco.PrecoVendaVigente)
				.SetarPrecoVendaAgendado(ObterPrecoVendaAgendado(((EntityBaseDto)produtoServicoEmpresaDto.Preco).Id, produtoServicoEmpresaDto.Preco.PrecoVendaVigente, produtoServicoEmpresaDto.Preco.Venda.Valor))
				.SetarUtilizarPrecoAgendado(UtilizarPrecoAgendado);
			LocalizacaoDto localizacao = produtoServicoEmpresaDto.ProdutoEmpresa.Localizacao;
			DadoProdutoFactory dadoProdutoFactory2 = dadoProdutoFactory.SetarLocalizacao((localizacao != null) ? localizacao.Descricao : null).SetarDataUltimaCompra(produtoServicoEmpresaDto.ProdutoEmpresa.DataUltimaCompra);
			TabelaPrecoDto tabelaPreco = base.TabelaPreco;
			return dadoProdutoFactory2.SetarIdTabelaPreco(((tabelaPreco != null) ? ((EntityBaseDto)tabelaPreco).Id : null) ?? string.Empty).SetarTabelasPreco(produtoServicoEmpresaDto.Preco.ItensTabelaPreco.ToDictionary((ItemTabelaPrecoProdutoServicoDto item) => item.TabelaPrecoReferencia, (ItemTabelaPrecoProdutoServicoDto item) => item.Preco)).SetarCondicaoEspecial(produtoServicoEmpresaDto.CondicaoEspecial.PrecoDiferenciadoToText)
				.SetarLink(produtoServicoEmpresaDto.ProdutoServico.Link)
				.SetarDataUltimaAlteracaoPrecoVenda(itemPrecoVenda.Data)
				.SetarCondicaoEspecialN(produtoServicoEmpresaDto.CondicaoEspecial.Quantidade)
				.SetarCondicaoEspecialX(produtoServicoEmpresaDto.CondicaoEspecial.PrecoToText)
				.SetarPrecoVendaCadastro(produtoServicoEmpresaDto.Preco.Venda.Valor)
				.SetarPrecoVendaPromocional(produtoServicoEmpresaDto.Preco.PrecoVendaVigente)
				.SetarIsPesavel(produtoServicoEmpresaDto.ProdutoServico.Produto.Pesavel)
				.SetarProdutoServicoEmpresaReferencia(((EntityBaseDto)produtoServicoEmpresaDto).Id)
				.SetarTara(produtoServicoEmpresaDto.ProdutoEmpresa.Tara)
				.Criar(itemDto.Estoque);
		}

		public override void Limpar()
		{
			base.Limpar();
			UtilizarQuantidadeDisponivel = false;
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityOrdemProducaoViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<ProdutoServicoEmpresaDto>
	{
		public override bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value, (Action)PreencherNumeroEtiquetasPorItem);
			}
		}

		public OrdemProducaoDto OrdemProducaoDto
		{
			get
			{
				return ((BindableBase)this).GetProperty<OrdemProducaoDto>((Expression<Func<OrdemProducaoDto>>)(() => OrdemProducaoDto));
			}
			set
			{
				((BindableBase)this).SetProperty<OrdemProducaoDto>((Expression<Func<OrdemProducaoDto>>)(() => OrdemProducaoDto), value);
			}
		}

		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityOrdemProducaoViewModel()
			: base("OrdemProducaoListaView")
		{
		}

		protected override void PreencherNumeroEtiquetasPorItem()
		{
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				item.SetarQuantidadeEtiquetas(UtilizarQuantidadeDisponivel ? (decimal.ToInt32(item.QuantidadeEstoque) * base.NumeroEtiquetasPorItem) : base.NumeroEtiquetasPorItem);
			}
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<OrdemProducaoDto, IProdutoServicoModuleService, ProdutoServicoEmpresaDto>(base.View, (Func<OrdemProducaoDto, Func<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>>)delegate(OrdemProducaoDto dto)
			{
				OrdemProducaoDto = dto;
				base.DescricaoReferencia = ((BaseDto)dto).ToText;
				return (IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdentificador(dto.ProdutoProduzidoReferencia);
			}, (Action<ProdutoServicoEmpresaDto>)delegate(ProdutoServicoEmpresaDto resultado)
			{
				base.DadosTemporarios.Limpar();
				CarregarPrecosAgendados(new List<string> { ((EntityBaseDto)resultado.Preco).Id });
				base.DadosTemporarios.Adicionar((IEnumerable<IDado>)ObterDadoProduto(resultado, base.NumeroEtiquetasPorItem));
				AvisarQuandoNenhumItemPossuiPrecoAgendado();
			}, true);
		}

		private IEnumerable<DadoProduto> ObterDadoProduto(ProdutoServicoEmpresaDto produtoServicoEmpresaDto, long etiquetasPorItem)
		{
			DadoProdutoFactory dadoProdutoFactory = new DadoProdutoFactory().SetarCodigoInterno(produtoServicoEmpresaDto.ProdutoServico.CodigoInterno).SetarDescricao(produtoServicoEmpresaDto.ProdutoServico.Descricao).SetarSiglaUnidadeMedida(produtoServicoEmpresaDto.ProdutoServico.UnidadeMedida.Sigla)
				.SetarCaracteristica(produtoServicoEmpresaDto.ProdutoServico.Caracteristica)
				.SetarMarca(produtoServicoEmpresaDto.ProdutoServico.Produto.Marca.Descricao)
				.SetarObservacao(produtoServicoEmpresaDto.ProdutoServico.Observacao)
				.SetarPesoLiquido(produtoServicoEmpresaDto.ProdutoServico.Produto.PesoLiquido)
				.SetarPesoBruto(produtoServicoEmpresaDto.ProdutoServico.Produto.PesoBruto)
				.SetarCodigoBarras(produtoServicoEmpresaDto.ProdutoServico.Produto.CodigoBarras)
				.SetarCodigoBarrasTributavel(produtoServicoEmpresaDto.ProdutoServico.Produto.CodigoBarrasTributavel)
				.SetarBaseCalculoPrecoReferencia(produtoServicoEmpresaDto.ProdutoEmpresa.ReferenciaPrecoEtiqueta.BaseCalculo)
				.SetarDescricaoPrecoReferencia(produtoServicoEmpresaDto.ProdutoEmpresa.ReferenciaPrecoEtiqueta.ToText)
				.SetarEtiquetasPorItem(etiquetasPorItem)
				.SetarPrecoCusto(produtoServicoEmpresaDto.Preco.Custo.Valor)
				.SetarPrecoVendaVigente(produtoServicoEmpresaDto.Preco.PrecoVendaVigente)
				.SetarPrecoVendaAgendado(ObterPrecoVendaAgendado(((EntityBaseDto)produtoServicoEmpresaDto.Preco).Id, produtoServicoEmpresaDto.Preco.PrecoVendaVigente, produtoServicoEmpresaDto.Preco.Venda.Valor))
				.SetarUtilizarPrecoAgendado(UtilizarPrecoAgendado);
			LocalizacaoDto localizacao = produtoServicoEmpresaDto.ProdutoEmpresa.Localizacao;
			DadoProdutoFactory dadoProdutoFactory2 = dadoProdutoFactory.SetarLocalizacao((localizacao != null) ? localizacao.Descricao : null).SetarDataUltimaCompra(produtoServicoEmpresaDto.ProdutoEmpresa.DataUltimaCompra).SetarDataConclusaoOrdemProducao(OrdemProducaoDto.Etapas.All((EtapaOrdemProducaoDto x) => x.Concluida) ? OrdemProducaoDto.Etapas.Max((EtapaOrdemProducaoDto x) => x.DataHoraFim) : DateTime.MinValue);
			TabelaPrecoDto tabelaPreco = base.TabelaPreco;
			return dadoProdutoFactory2.SetarIdTabelaPreco(((tabelaPreco != null) ? ((EntityBaseDto)tabelaPreco).Id : null) ?? string.Empty).SetarTabelasPreco(produtoServicoEmpresaDto.Preco.ItensTabelaPreco.ToDictionary((ItemTabelaPrecoProdutoServicoDto item) => item.TabelaPrecoReferencia, (ItemTabelaPrecoProdutoServicoDto item) => item.Preco)).SetarCondicaoEspecial(produtoServicoEmpresaDto.CondicaoEspecial.PrecoDiferenciadoToText)
				.SetarCondicaoEspecialN(produtoServicoEmpresaDto.CondicaoEspecial.Quantidade)
				.SetarCondicaoEspecialX(produtoServicoEmpresaDto.CondicaoEspecial.PrecoToText)
				.SetarLink(produtoServicoEmpresaDto.ProdutoServico.Link)
				.SetarPrecoVendaCadastro(produtoServicoEmpresaDto.Preco.Venda.Valor)
				.SetarPrecoVendaPromocional(produtoServicoEmpresaDto.Preco.PrecoVendaVigente)
				.SetarIsPesavel(produtoServicoEmpresaDto.ProdutoServico.Produto.Pesavel)
				.SetarProdutoServicoEmpresaReferencia(((EntityBaseDto)produtoServicoEmpresaDto).Id)
				.SetarTara(produtoServicoEmpresaDto.ProdutoEmpresa.Tara)
				.Criar(OrdemProducaoDto.ProdutoProduzido.EstoqueSaida);
		}

		public override void Limpar()
		{
			base.Limpar();
			UtilizarQuantidadeDisponivel = false;
		}
	}
	public class BuscaFiltroLancamentoItemRapidoViewModel : UpdateViewBindableBase, IViewModelBase
	{
		private readonly IBalancaService _balancaService;

		private readonly IWService _service;

		private readonly Action<object> _navegar;

		private readonly EtiquetaImpressaoViewModel _etiquetaImpressaoViewModel;

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

		public decimal Tara
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Tara));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Tara), value, (Action)CalcularPesoLiquido);
			}
		}

		public long NumeroEtiquetasPorItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => NumeroEtiquetasPorItem));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => NumeroEtiquetasPorItem), value);
			}
		}

		public bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value);
			}
		}

		public bool UtilizarPrecoAgendado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarPrecoAgendado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarPrecoAgendado), value);
			}
		}

		public bool PreencherDadosManualmente
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PreencherDadosManualmente));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PreencherDadosManualmente), value, (Action)delegate
				{
					FocoNaPesquisa = !FocoNaPesquisa;
				});
			}
		}

		public decimal PesoLiquido
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PesoLiquido));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PesoLiquido), value);
			}
		}

		public decimal PesoBruto
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PesoBruto));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PesoBruto), value);
			}
		}

		public IEnumerable<DadoProduto> DadoProduto { get; set; }

		public bool FocoNoPesoBruto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoPesoBruto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoPesoBruto), value);
			}
		}

		public bool FocoNoPesoLiquido
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoPesoLiquido));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoPesoLiquido), value);
			}
		}

		public bool FocoNaPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPesquisa), value);
			}
		}

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public ICommand AdicionarManualmenteCommand { get; set; }

		public ICommand AdicionarManualmenteEImprimirCommand { get; set; }

		public bool Valido => true;

		public BuscaFiltroLancamentoItemRapidoViewModel(IWService service, Action<object> navegar, EtiquetaImpressaoViewModel etiquetaImpressaoViewModel)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			_balancaService = DadosSistema.GetBalancaService();
			InicializarBalanca();
			NumeroEtiquetasPorItem = 1L;
			PesquisarProdutoServicoCommand = (ICommand)new AsyncCommand((Func<Task>)Pesquisar, (Func<bool>)(() => true), (bool?)null);
			_service = service;
			_navegar = navegar;
			_etiquetaImpressaoViewModel = etiquetaImpressaoViewModel;
			AdicionarManualmenteCommand = (ICommand)new DelegateCommand((Action)AdicionarManualmente, (Func<bool>)PodeAdicionarManualmente, (bool?)null);
			AdicionarManualmenteEImprimirCommand = (ICommand)new DelegateCommand((Action)AdicionarManualmenteEImprimir, (Func<bool>)PodeAdicionarManualmente, (bool?)null);
		}

		private async Task Pesquisar()
		{
			try
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>((Func<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdentificador(InformacaoPesquisa)));
				if (HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)val))
				{
					DadoProduto = ObterDadoProduto(val);
					InformacaoPesquisa = DadoProduto.FirstOrDefault().ToText;
					Tara = DadoProduto.FirstOrDefault().Tara;
					await ProcessarProduto();
				}
				else
				{
					NavegarProdutoServicoListaView();
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private async Task ProcessarProduto()
		{
			try
			{
				if (!PreencherDadosManualmente)
				{
					Adicionar(DadoProduto);
				}
				else if (DadoProduto.First().IsPesavel)
				{
					PesoBruto = await CapturarPeso();
					CalcularPesoLiquido();
					((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
					{
						FocoNoPesoLiquido = !FocoNoPesoLiquido;
					});
				}
				else
				{
					((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
					{
						FocoNoPesoBruto = !FocoNoPesoBruto;
					});
				}
			}
			catch (Exception)
			{
			}
		}

		protected void AdicionarManualmenteEImprimir()
		{
			List<DadoProduto> list = new List<DadoProduto>();
			foreach (DadoProduto item in DadoProduto)
			{
				item.SetarPesoBruto(PesoBruto);
				item.SetarPesoLiquido(PesoLiquido);
				item.SetarTara(Tara);
				list.Add(item);
			}
			_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox.PreVisualizacaoViewModel.Imprimir(list);
			Adicionar(list);
		}

		protected async Task<decimal> CapturarPeso()
		{
			return (await _balancaService.LerPeso()).Peso;
		}

		private void InicializarBalanca()
		{
			try
			{
				_balancaService.ObterTimeOut();
				_balancaService.Inicializar(false);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private IEnumerable<DadoProduto> ObterDadoProduto(ProdutoServicoEmpresaDto resultado)
		{
			IEnumerable<ItemPrecoVendaDto> source = DadosSistema.GetService().Canal<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>((Func<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>)((ICompraModuleService service) => service.ObterPrecoVendaHistorico((IEnumerable<string>)new List<string> { ((EntityBaseDto)resultado).Id })));
			BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel buscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel();
			buscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel.UtilizarPrecoAgendado = UtilizarPrecoAgendado;
			buscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel.CarregarPrecosAgendados(new List<string> { ((EntityBaseDto)resultado.Preco).Id });
			return buscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel.ObterDadoProduto(resultado, 1L, source.FirstOrDefault());
		}

		private void NavegarProdutoServicoListaView()
		{
			SelecaoCommandParameter<ProdutoServicoEmpresaDto> val = new SelecaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)async delegate(ProdutoServicoEmpresaDto resultado)
			{
				DadoProduto = ObterDadoProduto(resultado);
				InformacaoPesquisa = DadoProduto.FirstOrDefault().ToText;
				Tara = DadoProduto.FirstOrDefault().Tara;
				await ProcessarProduto();
			}, true);
			val.Parametros.Add("InformacaoPesquisa", (object)InformacaoPesquisa);
			_navegar(val);
		}

		private void AdicionarManualmente()
		{
			List<DadoProduto> list = new List<DadoProduto>();
			foreach (DadoProduto item in DadoProduto)
			{
				item.SetarPesoBruto(PesoBruto);
				item.SetarPesoLiquido(PesoLiquido);
				list.Add(item);
			}
			Adicionar(list);
		}

		private bool PodeAdicionarManualmente()
		{
			if (PreencherDadosManualmente)
			{
				return HelperObject.IsNotNull((object)DadoProduto);
			}
			return false;
		}

		private void CalcularPesoLiquido()
		{
			PesoLiquido = ((Tara > PesoBruto) ? PesoBruto : (PesoBruto - Tara));
		}

		protected long PreencherNumeroEtiquetasPorItem()
		{
			DadoProduto dadoProduto = ((Lista<Filtro>)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios.Itens.OfType<DadoProduto>().FirstOrDefault();
			if (!UtilizarQuantidadeDisponivel)
			{
				if (decimal.ToInt64(dadoProduto.QuantidadeEstoque) != 0L)
				{
					return decimal.ToInt64(dadoProduto.QuantidadeEstoque);
				}
				return 1L;
			}
			return decimal.ToInt64(dadoProduto.QuantidadeEstoque);
		}

		private void Adicionar(IEnumerable<DadoProduto> produtos)
		{
			List<string> list = new List<string>();
			foreach (DadoProduto produto in produtos)
			{
				InformacaoPesquisa = null;
				((Lista<Filtro>)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DadosTemporarios.Adicionar((IDado)produto);
				((Lista<Filtro>)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.DescricaoReferencia = produto.Descricao;
				long numeroEtiquetasPorItem = (UtilizarQuantidadeDisponivel ? PreencherNumeroEtiquetasPorItem() : NumeroEtiquetasPorItem);
				((Lista<Filtro>)((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.Filtro.TipoComboBox).ItemSelecionado.ViewModel.NumeroEtiquetasPorItem = numeroEtiquetasPorItem;
				((Lista<OrigemDado>)((Lista<EtiquetaConfiguracaoViewModel>)_etiquetaImpressaoViewModel.EtiquetasConfiguracaoComboBox).ItemSelecionado.OrigemComboBox).ItemSelecionado.AdicionarData();
				DadoProduto = null;
				PesoBruto = 0m;
				Tara = 0m;
				PesoLiquido = 0m;
				FocoNaPesquisa = !FocoNaPesquisa;
				list.Add(produto.ToText);
			}
			string text = string.Format("Adicionado item {0} a lista de impressão", string.Join(", ", list));
			if (UtilizarPrecoAgendado && !produtos.Any((DadoProduto x) => x.PossuiPrecoAgendado))
			{
				text = text + Environment.NewLine + Environment.NewLine + "Nenhum dos itens adicionados possui preço agendado a aplicar. As etiquetas serão impressas com o preço atual.";
			}
			DialogoFactory.MostrarDialogoConfirma(text, false, System.Windows.HorizontalAlignment.Center);
		}

		public void ProjectedAsViewModel()
		{
		}

		public void ProjectedAsListaViewModel()
		{
		}

		public void Dispose()
		{
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<ProdutoServicoEmpresaDto>
	{
		public override bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value, (Action)PreencherNumeroEtiquetasPorItem);
			}
		}

		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel()
			: base("ProdutoServicoListaView")
		{
		}

		protected override void PreencherNumeroEtiquetasPorItem()
		{
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				item.SetarQuantidadeEtiquetas(UtilizarQuantidadeDisponivel ? (decimal.ToInt64(item.QuantidadeEstoque) * base.NumeroEtiquetasPorItem) : base.NumeroEtiquetasPorItem);
			}
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<IEnumerable<ProdutoServicoEmpresaDto>>(base.View, (Action<IEnumerable<ProdutoServicoEmpresaDto>>)delegate(IEnumerable<ProdutoServicoEmpresaDto> produtoServico)
			{
				base.DescricaoReferencia = ((produtoServico.Count() > 1) ? $"{produtoServico.Count()} Produtos selecionados(as)" : (produtoServico.FirstOrDefault().ProdutoServico.Descricao ?? string.Empty));
				base.DadosTemporarios.Limpar();
				List<ProdutoServicoEmpresaDto> lista = new List<ProdutoServicoEmpresaDto>(produtoServico);
				AdicionarItensTemporarios(lista);
			}, true);
		}

		public override void Limpar()
		{
			base.Limpar();
			UtilizarQuantidadeDisponivel = false;
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityPromocaoViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<PromocaoDto>
	{
		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityPromocaoViewModel()
			: base("PromocaoListaView")
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			return (ISelecaoCommandParameter)(object)new SelecaoPersonalizadoCommandParameter<PromocaoDto>(base.View, (Action<PromocaoDto>)delegate(PromocaoDto dto)
			{
				base.DescricaoReferencia = dto.Descricao;
				base.DadosTemporarios.Limpar();
				AdicionarItensTemporarios(dto.ProdutosServicos);
			}, (IParametroPersonalizado)new ParametroAtualizarDtoSelecionadoComProdutoServico());
		}

		private void AdicionarItensTemporarios(IEnumerable<ProdutoServicoEtiquetaDto> lista)
		{
			CarregarPrecosAgendados(lista.Select((ProdutoServicoEtiquetaDto x) => ((EntityBaseDto)x.Preco).Id));
			foreach (ProdutoServicoEtiquetaDto listum in lista)
			{
				base.DadosTemporarios.Adicionar((IEnumerable<IDado>)ObterDadoProduto(listum, base.NumeroEtiquetasPorItem));
			}
			AvisarQuandoNenhumItemPossuiPrecoAgendado();
		}

		public IEnumerable<DadoProduto> ObterDadoProduto(ProdutoServicoEtiquetaDto produtoServicoEtiqueta, long etiquetasPorItem)
		{
			DadoProdutoFactory dadoProdutoFactory = new DadoProdutoFactory().SetarCodigoInterno(produtoServicoEtiqueta.CodigoInterno).SetarDescricao(produtoServicoEtiqueta.Descricao).SetarSiglaUnidadeMedida(produtoServicoEtiqueta.SiglaUnidadeMedida)
				.SetarCaracteristica(produtoServicoEtiqueta.Caracteristica)
				.SetarMarca(produtoServicoEtiqueta.Marca)
				.SetarObservacao(produtoServicoEtiqueta.Observacao)
				.SetarPesoLiquido(produtoServicoEtiqueta.PesoLiquido)
				.SetarPesoBruto(produtoServicoEtiqueta.PesoBruto)
				.SetarCodigoBarras(produtoServicoEtiqueta.CodigoBarras)
				.SetarCodigoBarrasTributavel(produtoServicoEtiqueta.CodigoBarrasTributavel)
				.SetarBaseCalculoPrecoReferencia(produtoServicoEtiqueta.ReferenciaPrecoEtiquetaBaseCalculo)
				.SetarDescricaoPrecoReferencia(produtoServicoEtiqueta.ReferenciaPrecoEtiquetaToText)
				.SetarEtiquetasPorItem(etiquetasPorItem)
				.SetarPrecoCusto(produtoServicoEtiqueta.PrecoCusto)
				.SetarPrecoVendaVigente(produtoServicoEtiqueta.PrecoVendaVigente)
				.SetarPrecoVendaAgendado(ObterPrecoVendaAgendado(((EntityBaseDto)produtoServicoEtiqueta.Preco).Id, produtoServicoEtiqueta.PrecoVendaVigente, produtoServicoEtiqueta.Preco.Venda.Valor))
				.SetarUtilizarPrecoAgendado(UtilizarPrecoAgendado)
				.SetarLocalizacao(produtoServicoEtiqueta.Localizacao)
				.SetarDataUltimaCompra(produtoServicoEtiqueta.DataUltimaCompra);
			TabelaPrecoDto tabelaPreco = base.TabelaPreco;
			return dadoProdutoFactory.SetarIdTabelaPreco(((tabelaPreco != null) ? ((EntityBaseDto)tabelaPreco).Id : null) ?? string.Empty).SetarTabelasPreco(produtoServicoEtiqueta.Preco.ItensTabelaPreco.ToDictionary((ItemTabelaPrecoProdutoServicoDto item) => item.TabelaPrecoReferencia, (ItemTabelaPrecoProdutoServicoDto item) => item.Preco)).SetarCondicaoEspecial(produtoServicoEtiqueta.CondicaoEspecial)
				.SetarLink(produtoServicoEtiqueta.Link)
				.SetarPrecoVendaCadastro(produtoServicoEtiqueta.Preco.Venda.Valor)
				.SetarPrecoVendaPromocional(produtoServicoEtiqueta.PrecoVendaVigente)
				.Criar(produtoServicoEtiqueta.Estoque);
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntitySubGrupoViewModel : BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<SubGrupoDto>
	{
		public override bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value, (Action)PreencherNumeroEtiquetasPorItem);
			}
		}

		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntitySubGrupoViewModel()
			: base("ProdutoServicoSubGrupoListaView")
		{
		}

		protected override void PreencherNumeroEtiquetasPorItem()
		{
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				item.SetarQuantidadeEtiquetas(UtilizarQuantidadeDisponivel ? (decimal.ToInt32(item.QuantidadeEstoque) * base.NumeroEtiquetasPorItem) : base.NumeroEtiquetasPorItem);
			}
		}

		public override void Limpar()
		{
			base.Limpar();
			UtilizarQuantidadeDisponivel = false;
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			return (ISelecaoCommandParameter)(object)new SelecaoPersonalizadoCommandParameter<SubGrupoDto>(base.View, (Action<SubGrupoDto>)delegate(SubGrupoDto dto)
			{
				base.DescricaoReferencia = dto.Descricao;
				base.DadosTemporarios.Limpar();
				AdicionarItensTemporarios(dto.ProdutosServicos);
			}, (IParametroPersonalizado)new ParametroAtualizarDtoSelecionadoComProdutoServico());
		}
	}
	public class BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel<T> : BuscaFiltroQuantidadeDisponivelTabelaPrecoViewModelBase where T : EntityBaseDto
	{
		public BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityViewModel(string view)
			: base(view)
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<T>(base.View, (Action<T>)delegate(T p)
			{
				base.Referencia = ((EntityBaseDto)p).Id;
				base.DescricaoReferencia = ((BaseDto)(object)p).ToText;
			}, true);
		}
	}
	public abstract class BuscaFiltroQuantidadeDisponivelTabelaPrecoViewModelBase : BuscaFiltroViewModelBase
	{
		private IList<AlteracaoPrecoAgendadaDto> _precosAgendados = new List<AlteracaoPrecoAgendadaDto>();

		public SelecaoCommandParameter<TabelaPrecoDto> TabelaPrecoCommandParameter => new SelecaoCommandParameter<TabelaPrecoDto>("TabelaPrecoListaView", (Action<TabelaPrecoDto>)delegate(TabelaPrecoDto tabelaPreco)
		{
			TabelaPreco = tabelaPreco;
		}, true);

		public TabelaPrecoDto TabelaPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value, (Action)OnAlterouTabelaPreco);
			}
		}

		public virtual bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value);
			}
		}

		public virtual bool UtilizarPrecoAgendado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarPrecoAgendado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarPrecoAgendado), value, (Action)AplicarPrecoAgendado);
			}
		}

		public ICommand RemoverTabelaPrecoCommand { get; set; }

		protected BuscaFiltroQuantidadeDisponivelTabelaPrecoViewModelBase(string view)
			: base(view)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			RemoverTabelaPrecoCommand = (ICommand)new DelegateCommand((Action)RemoverTabelaPreco);
		}

		public virtual void OnAlterouTabelaPreco()
		{
			if (HelperObject.Is<TabelaPrecoDtoNull>((object)TabelaPreco))
			{
				return;
			}
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				TabelaPrecoDto tabelaPreco = TabelaPreco;
				item.SetarIdTabelaPreco(((tabelaPreco != null) ? ((EntityBaseDto)tabelaPreco).Id : null) ?? string.Empty);
			}
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
		}

		private void AplicarPrecoAgendado()
		{
			foreach (DadoProduto item in base.DadosTemporarios.Itens.OfType<DadoProduto>())
			{
				item.SetarUtilizarPrecoAgendado(UtilizarPrecoAgendado);
			}
			AvisarQuandoNenhumItemPossuiPrecoAgendado();
		}

		protected void AvisarQuandoNenhumItemPossuiPrecoAgendado()
		{
			if (UtilizarPrecoAgendado)
			{
				List<DadoProduto> source = base.DadosTemporarios.Itens.OfType<DadoProduto>().ToList();
				if (source.Any() && !source.Any((DadoProduto x) => x.PossuiPrecoAgendado))
				{
					DialogoFactory.MostrarDialogoConfirma("Nenhum dos itens selecionados possui preço agendado a aplicar. As etiquetas serão impressas com o preço atual.", false, System.Windows.HorizontalAlignment.Center);
				}
			}
		}

		public void CarregarPrecosAgendados(IEnumerable<string> precosReferencia)
		{
			_precosAgendados = HelperIEnumerable.ToListEmptyWhenNull<AlteracaoPrecoAgendadaDto>(DadosSistema.GetService().Canal<IAlteracaoPrecoAgendadaModuleService, IEnumerable<AlteracaoPrecoAgendadaDto>>((Func<IAlteracaoPrecoAgendadaModuleService, IEnumerable<AlteracaoPrecoAgendadaDto>>)((IAlteracaoPrecoAgendadaModuleService service) => service.ObterPrecosAgendados(HelperIEnumerable.ToListEmptyWhenNull<string>(precosReferencia).Distinct()))));
		}

		protected decimal ObterPrecoVendaAgendado(string precoReferencia, decimal precoVendaVigente, decimal precoVendaCadastro)
		{
			if (precoVendaVigente != precoVendaCadastro)
			{
				return 0m;
			}
			AlteracaoPrecoAgendadaDto obj = _precosAgendados.FirstOrDefault((AlteracaoPrecoAgendadaDto x) => x.PrecoReferencia.Equals(precoReferencia));
			if (obj == null)
			{
				return 0m;
			}
			return obj.PrecoNovo;
		}

		public void AdicionarItensTemporarios(IEnumerable<ProdutoServicoEmpresaDto> lista)
		{
			IEnumerable<ItemPrecoVendaDto> source = DadosSistema.GetService().Canal<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>((Func<ICompraModuleService, IEnumerable<ItemPrecoVendaDto>>)((ICompraModuleService service) => service.ObterPrecoVendaHistorico(lista.Select((ProdutoServicoEmpresaDto y) => ((EntityBaseDto)y).Id))));
			CarregarPrecosAgendados(lista.Select((ProdutoServicoEmpresaDto y) => ((EntityBaseDto)y.Preco).Id));
			foreach (ProdutoServicoEmpresaDto produtoServicoEmpresa in lista)
			{
				ItemPrecoVendaDto itemPrecoVenda = source.FirstOrDefault((ItemPrecoVendaDto x) => x.ProdutoServicoEmpresaReferencia == ((EntityBaseDto)produtoServicoEmpresa).Id);
				base.DadosTemporarios.Adicionar((IEnumerable<IDado>)ObterDadoProduto(produtoServicoEmpresa, base.NumeroEtiquetasPorItem, itemPrecoVenda));
			}
			AvisarQuandoNenhumItemPossuiPrecoAgendado();
		}

		public override void Limpar()
		{
			RemoverTabelaPreco();
			base.Limpar();
		}

		public IEnumerable<DadoProduto> ObterDadoProduto(ProdutoServicoEmpresaDto produtoServicoEmpresa, long etiquetasPorItem, ItemPrecoVendaDto itemPrecoVenda)
		{
			DadoProdutoFactory dadoProdutoFactory = new DadoProdutoFactory().SetarCodigoInterno(produtoServicoEmpresa.ProdutoServico.CodigoInterno).SetarDescricao(produtoServicoEmpresa.ProdutoServico.Descricao).SetarSiglaUnidadeMedida(produtoServicoEmpresa.ProdutoServico.UnidadeMedida.Sigla)
				.SetarCaracteristica(produtoServicoEmpresa.ProdutoServico.Caracteristica)
				.SetarMarca(produtoServicoEmpresa.ProdutoServico.Produto.Marca.Descricao)
				.SetarObservacao(produtoServicoEmpresa.ProdutoServico.Observacao)
				.SetarPesoLiquido(produtoServicoEmpresa.ProdutoServico.Produto.PesoLiquido)
				.SetarPesoBruto(produtoServicoEmpresa.ProdutoServico.Produto.PesoBruto)
				.SetarCodigoBarras(produtoServicoEmpresa.ProdutoServico.Produto.CodigoBarras)
				.SetarCodigoBarrasTributavel(produtoServicoEmpresa.ProdutoServico.Produto.CodigoBarrasTributavel)
				.SetarBaseCalculoPrecoReferencia(produtoServicoEmpresa.ProdutoEmpresa.ReferenciaPrecoEtiqueta.BaseCalculo)
				.SetarDescricaoPrecoReferencia(produtoServicoEmpresa.ProdutoEmpresa.ReferenciaPrecoEtiqueta.ToText)
				.SetarEtiquetasPorItem(etiquetasPorItem)
				.SetarPrecoCusto(produtoServicoEmpresa.Preco.Custo.Valor)
				.SetarPrecoVendaVigente(produtoServicoEmpresa.Preco.PrecoVendaVigente)
				.SetarPrecoVendaAgendado(ObterPrecoVendaAgendado(((EntityBaseDto)produtoServicoEmpresa.Preco).Id, produtoServicoEmpresa.Preco.PrecoVendaVigente, produtoServicoEmpresa.Preco.Venda.Valor))
				.SetarUtilizarPrecoAgendado(UtilizarPrecoAgendado);
			LocalizacaoDto localizacao = produtoServicoEmpresa.ProdutoEmpresa.Localizacao;
			DadoProdutoFactory dadoProdutoFactory2 = dadoProdutoFactory.SetarLocalizacao((localizacao != null) ? localizacao.Descricao : null).SetarDataUltimaCompra(produtoServicoEmpresa.ProdutoEmpresa.DataUltimaCompra);
			TabelaPrecoDto tabelaPreco = TabelaPreco;
			return dadoProdutoFactory2.SetarIdTabelaPreco(((tabelaPreco != null) ? ((EntityBaseDto)tabelaPreco).Id : null) ?? string.Empty).SetarTabelasPreco(produtoServicoEmpresa.Preco.ItensTabelaPreco.ToDictionary((ItemTabelaPrecoProdutoServicoDto item) => item.TabelaPrecoReferencia, (ItemTabelaPrecoProdutoServicoDto item) => item.Preco)).SetarCondicaoEspecial(produtoServicoEmpresa.CondicaoEspecial.PrecoDiferenciadoToText)
				.SetarCondicaoEspecialN(produtoServicoEmpresa.CondicaoEspecial.Quantidade)
				.SetarCondicaoEspecialX(produtoServicoEmpresa.CondicaoEspecial.PrecoToText)
				.SetarLink(produtoServicoEmpresa.ProdutoServico.Link)
				.SetarDataUltimaAlteracaoPrecoVenda(itemPrecoVenda.Data)
				.SetarPrecoVendaCadastro(produtoServicoEmpresa.Preco.Venda.Valor)
				.SetarPrecoVendaPromocional(produtoServicoEmpresa.Preco.PrecoVendaVigente)
				.SetarIsPesavel(produtoServicoEmpresa.ProdutoServico.Produto.Pesavel)
				.SetarProdutoServicoEmpresaReferencia(((EntityBaseDto)produtoServicoEmpresa).Id)
				.SetarTara(produtoServicoEmpresa.ProdutoEmpresa.Tara)
				.Criar(produtoServicoEmpresa.ProdutoEmpresa.Estoque);
		}
	}
	public abstract class BuscaFiltroQuantidadeDisponivelViewModelBase : BuscaFiltroViewModelBase
	{
		public virtual bool UtilizarQuantidadeDisponivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UtilizarQuantidadeDisponivel), value, (Action)PreencherNumeroEtiquetasPorItem);
			}
		}

		protected BuscaFiltroQuantidadeDisponivelViewModelBase(string view)
			: base(view)
		{
		}

		public virtual void AdicionarItensTemporarios(IEnumerable<MovimentacaoDto> lista)
		{
		}

		public virtual DadoMovimentacaoVolume PreencherDadoMovimentacao(MovimentacaoDto movimentacao, VolumeDto volume, int etiquetasPorItem, string volumeContador)
		{
			throw new NotImplementedException();
		}
	}
	public class BuscaFiltroViewModel<T> : BuscaFiltroViewModelBase where T : BaseDto
	{
		public BuscaFiltroViewModel(string view)
			: base(view)
		{
		}

		public override ISelecaoCommandParameter SelecaoCommand()
		{
			return (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<T>(base.View, (Action<T>)delegate
			{
			}, true);
		}

		public void AdicionarItensTemporarios(IEnumerable<PessoaDto> lista)
		{
			foreach (PessoaDto listum in lista)
			{
				Lista<IDado> dadosTemporarios = base.DadosTemporarios;
				string nome = listum.Nome;
				string numero = listum.TelefonePrincipal.Numero;
				string logradouro = listum.EnderecoPrincipal.Logradouro;
				string numero2 = listum.EnderecoPrincipal.Numero;
				string bairro = listum.EnderecoPrincipal.Bairro;
				string cep = listum.EnderecoPrincipal.Cep;
				MunicipioDto municipio = listum.EnderecoPrincipal.Municipio;
				string enderecoPrincipalMunicipio = ((municipio != null) ? municipio.Nome : null) ?? string.Empty;
				MunicipioDto municipio2 = listum.EnderecoPrincipal.Municipio;
				object obj;
				if (municipio2 == null)
				{
					obj = null;
				}
				else
				{
					UFDto uF = municipio2.UF;
					obj = ((uF != null) ? uF.Sigla : null);
				}
				if (obj == null)
				{
					obj = string.Empty;
				}
				MunicipioDto municipio3 = listum.EnderecoPrincipal.Municipio;
				object obj2;
				if (municipio3 == null)
				{
					obj2 = null;
				}
				else
				{
					UFDto uF2 = municipio3.UF;
					if (uF2 == null)
					{
						obj2 = null;
					}
					else
					{
						PaisDto pais = uF2.Pais;
						obj2 = ((pais != null) ? pais.Nome : null);
					}
				}
				if (obj2 == null)
				{
					obj2 = string.Empty;
				}
				dadosTemporarios.Adicionar((IDado)new DadoPessoa(nome, numero, logradouro, numero2, bairro, cep, enderecoPrincipalMunicipio, (string)obj, (string)obj2, listum.EnderecoEntrega.Logradouro, listum.EnderecoEntrega.Numero, listum.EnderecoEntrega.Bairro, listum.EnderecoEntrega.Cep, listum.EnderecoEntrega.Municipio.Nome, listum.EnderecoEntrega.Municipio.UF.Sigla, listum.EnderecoEntrega.Municipio.UF.Pais.Nome, listum.Juridica.NomeFantasia, listum.Juridica.NomeDoResponsavel, listum.Juridica.Cnpj, listum.Cpf, listum.EmailPrincipal.Endereco, base.NumeroEtiquetasPorItem, listum.Observacao));
			}
		}
	}
	public abstract class BuscaFiltroViewModelBase : BindableBase
	{
		public string View { get; protected set; }

		public long NumeroEtiquetasPorItem
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => NumeroEtiquetasPorItem));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => NumeroEtiquetasPorItem), value);
			}
		}

		public string Referencia { get; protected set; }

		public string DescricaoReferencia
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoReferencia));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoReferencia), value);
			}
		}

		public ISelecaoCommandParameter FiltroCommandParameter => SelecaoCommand();

		public Lista<IDado> DadosTemporarios { get; protected set; }

		protected BuscaFiltroViewModelBase(string view)
		{
			View = view;
			NumeroEtiquetasPorItem = 1L;
			DadosTemporarios = new Lista<IDado>();
			((BindableBase)this).PropertyChanged += OnPropertyChanged;
		}

		public abstract ISelecaoCommandParameter SelecaoCommand();

		protected virtual void PreencherNumeroEtiquetasPorItem()
		{
			foreach (IDado iten in DadosTemporarios.Itens)
			{
				iten.SetarQuantidadeEtiquetas(NumeroEtiquetasPorItem);
			}
		}

		public virtual void Limpar()
		{
			DadosTemporarios.Limpar();
			Referencia = string.Empty;
			DescricaoReferencia = string.Empty;
			NumeroEtiquetasPorItem = 1L;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "NumeroEtiquetasPorItem")
			{
				PreencherNumeroEtiquetasPorItem();
			}
		}
	}
	public class Filtro : BindableBase
	{
		public virtual string Descricao { get; set; }

		public BuscaFiltroViewModelBase ViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<BuscaFiltroViewModelBase>((Expression<Func<BuscaFiltroViewModelBase>>)(() => ViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<BuscaFiltroViewModelBase>((Expression<Func<BuscaFiltroViewModelBase>>)(() => ViewModel), value);
			}
		}
	}
	public class FiltroMovimentacaoItemMovimentacaoSaida : Filtro
	{
		public override string Descricao => "Movimentação de saída";

		public FiltroMovimentacaoItemMovimentacaoSaida()
		{
			base.ViewModel = new BuscaFiltroEntityMovimentacaoItemMovimentacaoItemSaidaViewModel();
		}
	}
	public class FiltroMovimentacaoObjetoMovimentacaoSaida : Filtro
	{
		public override string Descricao => "Movimentação de saída";

		public FiltroMovimentacaoObjetoMovimentacaoSaida()
		{
			base.ViewModel = new BuscaFiltroEntityMovimentacaoObjetoMovimentacaoObjetoSaidaViewModel();
		}
	}
	public class FiltroMovimentacaoVolumeMovimentacaoSaida : Filtro
	{
		public override string Descricao => "Movimentação de saída";

		public FiltroMovimentacaoVolumeMovimentacaoSaida()
		{
			base.ViewModel = new BuscaFiltroEntityMovimentacaoVolumeMovimentacaoVolumeSaidaViewModel();
		}
	}
	public class FiltroProdutoMovimentacaoSaida : Filtro
	{
		public override string Descricao => "Movimentação de saída";

		public FiltroProdutoMovimentacaoSaida()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel("MovimentacaoSaidaListaView");
		}
	}
	public class FiltroProdutoOrdemProducao : Filtro
	{
		public override string Descricao => "Ordem de produção";

		public FiltroProdutoOrdemProducao()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityOrdemProducaoViewModel();
		}
	}
	public class FiltroSubGrupo : Filtro
	{
		public override string Descricao => "Subgrupo";

		public FiltroSubGrupo()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntitySubGrupoViewModel();
		}
	}
	public class FiltroMarca : Filtro
	{
		public override string Descricao => "Marca";

		public FiltroMarca()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMarcaViewModel();
		}
	}
	public class FiltroMunicipio : Filtro
	{
		public override string Descricao => "Município";

		public FiltroMunicipio()
		{
			base.ViewModel = new BuscaFiltroEntityMunicipioViewModel();
		}
	}
	public class FiltroPessoa : Filtro
	{
		public override string Descricao => "Pessoa";

		public FiltroPessoa()
		{
			base.ViewModel = new BuscaFiltroEntityPessoaViewModel();
		}
	}
	public class FiltroPessoaMovimentacaoEntrada : Filtro
	{
		public override string Descricao => "Movimentação de entrada";

		public FiltroPessoaMovimentacaoEntrada()
		{
			base.ViewModel = new BuscaFiltroEntityPessoaMovimentacaoEntradaViewModel();
		}
	}
	public class FiltroPessoaMovimentacaoSaida : Filtro
	{
		public override string Descricao => "Movimentação de saída";

		public FiltroPessoaMovimentacaoSaida()
		{
			base.ViewModel = new BuscaFiltroEntityPessoaMovimentacaoSaidaViewModel();
		}
	}
	public class FiltroProduto : Filtro
	{
		public override string Descricao => "Produto";

		public FiltroProduto()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityProdutoViewModel();
		}
	}
	public class FiltroProdutoMovimentacaoEntrada : Filtro
	{
		public override string Descricao => "Movimentação de entrada";

		public FiltroProdutoMovimentacaoEntrada()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityMovimentacaoViewModel("MovimentacaoEntradaListaView");
		}
	}
	public class FiltroPromocao : Filtro
	{
		public override string Descricao => "Promoção";

		public FiltroPromocao()
		{
			base.ViewModel = new BuscaFiltroQuantidadeDisponivelTabelaPrecoEntityPromocaoViewModel();
		}
	}
	public class FiltroUf : Filtro
	{
		public override string Descricao => "UF";

		public FiltroUf()
		{
			base.ViewModel = new BuscaFiltroEntityUfViewModel();
		}
	}
	public class TipoComboBoxViewModel : Lista<Filtro>
	{
		public override bool Valido
		{
			get
			{
				if (((ViewModelValidationBase)this).Valido && base.PossuiItemSelecionado)
				{
					return !HelperObject.IsNull((object)((Lista<Filtro>)this).ItemSelecionado.ViewModel);
				}
				return false;
			}
		}
	}
}
namespace Otus.Modulo.Etiquetas.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Etiquetas.Properties.Resources", typeof(Resources).Assembly);
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

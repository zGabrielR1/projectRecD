using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Core.Projections.AuxiliaresProjection;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Autorizacoes;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Auxiliares.MensagensWhatsappModelo;
using DigisatServer.Application.Main.Dto.Cadastros.Animais;
using DigisatServer.Application.Main.Dto.Cadastros.Objetos;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Cadastros.StatusDavOsCadastros;
using DigisatServer.Application.Main.Dto.Cadastros.TabelasPreco;
using DigisatServer.Application.Main.Dto.Cadastros.TiposPersonalizado;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Application.Main.Dto.Financeiros.CentrosCusto;
using DigisatServer.Application.Main.Dto.Financeiros.Contas;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Financeiros.PlanosConta;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Application.Main.Dto.Movimentacoes.Relatorios;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Domain.Main.Aggregates.MovimentacoesAgg.IndicadoresPresencaConsumidor;
using DigisatServer.Infrastructure.Crosscutting.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Dto.Financeiros.EspeciesPagamento;
using DigisatServer.Infrastructure.Crosscutting.Email;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Function;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Icones;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Interfaces.EnvioEmail;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.Atendimentos;
using DigisatServer.Infrastructure.Crosscutting.Model.Boletos;
using DigisatServer.Infrastructure.Crosscutting.Model.IndicadoresOperacaoConsumidorFinal;
using DigisatServer.Infrastructure.Crosscutting.Model.Prioridades;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesMovimentacao;
using DigisatServer.Infrastructure.Crosscutting.Model.StatusDavOs;
using DigisatServer.Infrastructure.Crosscutting.Model.TipoDavOs;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp.Classes;
using DigisatServer.Infrastructure.Crosscutting.Service.EnvioEmail;
using DigisatServer.Infrastructure.Crosscutting.ValueObject;
using DigisatServer.Infrastructure.Crosscutting.ValueObject.FinanceirosVO.Especies.Pagamentos;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Icones;
using Otus.Infraestrutura.Reports;
using Otus.Infraestrutura.Reports.DavOs;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.CamposPersonalizado;
using Otus.Infraestrutura.ViewModels.Estoques.Grades;
using Otus.Infraestrutura.ViewModels.Estoques.Lotes.LotesViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.Seriais.SeriaisViewModel;
using Otus.Infraestrutura.ViewModels.Estoques.TiposEstoque;
using Otus.Infraestrutura.ViewModels.StatusDavOs;
using Otus.Infraestrutura.Views.Estoques.Seriais;
using Otus.Infraestrutura.Views.StatusDavOs;
using Otus.Modulo.OrdensServico.ViewModels.Paineis;
using Otus.Modulo.OrdensServico.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.OrdensServico")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.OrdensServico")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.OrdensServico
{
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ObjetoModulo")]
	[ModuleDependency("ProdutoServicoModulo")]
	[ModuleDependency("MovimentacaoModulo")]
	[ModuleDependency("VisualizarAnexosModulo")]
	[Module(ModuleName = "OrdemServicoModulo", OnDemand = true)]
	public class OrdemServicoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public OrdemServicoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<OrdemServicoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<OrdensServicoView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<PainelEsquerdoOrdemServico>(_container, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<PainelDireitoOrdemServico>(_container, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<PainelDireitoFichaOrdemServico>(_container, Array.Empty<InjectionMember>());
			UnityExtensions.RegisterTypeForNavigation<SerialListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<StatusDavOsCadastroListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<StatusDavOsCadastroFichaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<StatusDavOsCadastroFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.OrdensServico.Views
{
	public class OrdemServicoView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdemServicoView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensServico;component/views/ordemservicoview.xaml", UriKind.Relative);
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
	public class OrdensServicoView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public OrdensServicoView()
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
				Uri resourceLocator = new Uri("/Modulo.OrdensServico;component/views/ordensservicoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.OrdensServico.ViewModels
{
	public class OrdemServicoViewModel : BaseMinimizar
	{
		private readonly IWService _service;

		private Action _callbackNavegar;

		private ISelecaoCommandParameter _navegarCommandParameter;

		public string EstadoDaAnimacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => EstadoDaAnimacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => EstadoDaAnimacao), value);
			}
		}

		public Seletor<IPainel> SeletorEsquerdo
		{
			get
			{
				return ((BindableBase)this).GetProperty<Seletor<IPainel>>((Expression<Func<Seletor<IPainel>>>)(() => SeletorEsquerdo));
			}
			set
			{
				((BindableBase)this).SetProperty<Seletor<IPainel>>((Expression<Func<Seletor<IPainel>>>)(() => SeletorEsquerdo), value);
			}
		}

		public Seletor<IPainel> SeletorDireito
		{
			get
			{
				return ((BindableBase)this).GetProperty<Seletor<IPainel>>((Expression<Func<Seletor<IPainel>>>)(() => SeletorDireito));
			}
			set
			{
				((BindableBase)this).SetProperty<Seletor<IPainel>>((Expression<Func<Seletor<IPainel>>>)(() => SeletorDireito), value);
			}
		}

		public bool FocoAdicionarDavOs
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoAdicionarDavOs));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoAdicionarDavOs), value);
			}
		}

		public ICommand F4Command { get; set; }

		public ICommand F5Command { get; set; }

		public ICommand CtrlFCommand { get; set; }

		public ICommand MostrarPainelDireitoCommand { get; set; }

		public OrdemServicoViewModel(IUnityContainer container, IWService service, IImpressoraNaoFiscalService impressoraNaoFiscalService)
		{
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			//IL_0126: Expected O, but got Unknown
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Expected O, but got Unknown
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0164: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Expected O, but got Unknown
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Expected O, but got Unknown
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a9: Expected O, but got Unknown
			//IL_01aa: Expected O, but got Unknown
			//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d9: Expected O, but got Unknown
			//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Expected O, but got Unknown
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Expected O, but got Unknown
			//IL_0203: Expected O, but got Unknown
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0221: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Expected O, but got Unknown
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Expected O, but got Unknown
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_025b: Expected O, but got Unknown
			//IL_025c: Expected O, but got Unknown
			//IL_0275: Unknown result type (might be due to invalid IL or missing references)
			//IL_027a: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Expected O, but got Unknown
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_029c: Expected O, but got Unknown
			//IL_029c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b4: Expected O, but got Unknown
			//IL_02b5: Expected O, but got Unknown
			//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Expected O, but got Unknown
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Expected O, but got Unknown
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Expected O, but got Unknown
			//IL_0313: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Expected O, but got Unknown
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0330: Expected O, but got Unknown
			//IL_0330: Unknown result type (might be due to invalid IL or missing references)
			//IL_033c: Expected O, but got Unknown
			//IL_033c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0348: Expected O, but got Unknown
			//IL_0348: Unknown result type (might be due to invalid IL or missing references)
			//IL_0355: Expected O, but got Unknown
			//IL_0356: Expected O, but got Unknown
			//IL_036f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0374: Unknown result type (might be due to invalid IL or missing references)
			//IL_0385: Expected O, but got Unknown
			//IL_0385: Unknown result type (might be due to invalid IL or missing references)
			//IL_0396: Expected O, but got Unknown
			//IL_0397: Expected O, but got Unknown
			//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c6: Expected O, but got Unknown
			//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d7: Expected O, but got Unknown
			//IL_03d8: Expected O, but got Unknown
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0407: Expected O, but got Unknown
			//IL_0407: Unknown result type (might be due to invalid IL or missing references)
			//IL_0418: Expected O, but got Unknown
			//IL_0419: Expected O, but got Unknown
			//IL_0474: Unknown result type (might be due to invalid IL or missing references)
			//IL_047e: Expected O, but got Unknown
			//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ab: Expected O, but got Unknown
			//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c2: Expected O, but got Unknown
			//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d9: Expected O, but got Unknown
			_service = service;
			SeletorEsquerdo = UnityContainerExtensions.Resolve<Seletor<IPainel>>(container, Array.Empty<ResolverOverride>());
			SeletorDireito = UnityContainerExtensions.Resolve<Seletor<IPainel>>(container, Array.Empty<ResolverOverride>());
			IEnumerable<ContaDto> enumerable = _service.Canal<IContaModuleService, IEnumerable<ContaDto>>((Func<IContaModuleService, IEnumerable<ContaDto>>)((IContaModuleService x) => x.ObterContas()));
			IEnumerable<CentroCustoDto> enumerable2 = _service.Canal<ICentroCustoModuleService, IEnumerable<CentroCustoDto>>((Func<ICentroCustoModuleService, IEnumerable<CentroCustoDto>>)((ICentroCustoModuleService x) => x.ObterCentrosCusto()));
			IEnumerable<PlanoContaDto> enumerable3 = _service.Canal<IPlanoContaModuleService, IEnumerable<PlanoContaDto>>((Func<IPlanoContaModuleService, IEnumerable<PlanoContaDto>>)((IPlanoContaModuleService x) => x.ObterPlanosConta()));
			EspeciePagamento val = ValueObjectService<EspeciePagamento, EspeciePagamentoValueObjectRepository>.ObterTodos().First((EspeciePagamento x) => x.Codigo == 1);
			EspeciePagamentoDto val2 = new EspeciePagamentoDto
			{
				Codigo = val.Codigo,
				Descricao = val.Descricao,
				EspecieRecebimento = new EspecieRecebimentoDto
				{
					Codigo = val.EspecieRecebimento.Codigo,
					Descricao = val.EspecieRecebimento.Descricao
				}
			};
			SeletorEsquerdo.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelEsquerdoOrdemServico>(container, (ResolverOverride[])(object)new ResolverOverride[1] { (ResolverOverride)new ParameterOverride("seletorDireito", (object)SeletorDireito) }));
			Seletor<IPainel> seletorDireito = SeletorDireito;
			ResolverOverride[] array = new ResolverOverride[1];
			ParameterOverrides val3 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("impressoraNaoFiscalService", (object)impressoraNaoFiscalService);
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("navegarEtiquetaView", (object)new Action<NavigationParameters>(NavegarEtiquetaView));
			array[0] = (ResolverOverride)val3;
			seletorDireito.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoOrdemServico>(container, (ResolverOverride[])(object)array));
			Seletor<IPainel> seletorDireito2 = SeletorDireito;
			ResolverOverride[] array2 = new ResolverOverride[1];
			ParameterOverrides val4 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val4).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val4).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val4).Add("navegar", (object)new Action<object>(((NavigationBase)this).Navegar));
			array2[0] = (ResolverOverride)val4;
			seletorDireito2.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoFichaOrdemServico>(container, (ResolverOverride[])(object)array2));
			Seletor<IPainel> seletorDireito3 = SeletorDireito;
			ResolverOverride[] array3 = new ResolverOverride[1];
			ParameterOverrides val5 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val5).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val5).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val5).Add("navegar", (object)new Action<object>(((NavigationBase)this).Navegar));
			array3[0] = (ResolverOverride)val5;
			seletorDireito3.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoFichaOrdemServicoAnimal>(container, (ResolverOverride[])(object)array3));
			Seletor<IPainel> seletorDireito4 = SeletorDireito;
			ResolverOverride[] array4 = new ResolverOverride[1];
			ParameterOverrides val6 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val6).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val6).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val6).Add("navegar", (object)new Action<object>(((NavigationBase)this).Navegar));
			array4[0] = (ResolverOverride)val6;
			seletorDireito4.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoAnexosOrdemServico>(container, (ResolverOverride[])(object)array4));
			Seletor<IPainel> seletorDireito5 = SeletorDireito;
			ResolverOverride[] array5 = new ResolverOverride[1];
			ParameterOverrides val7 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("navegar", (object)new Action<object>(((NavigationBase)this).Navegar));
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("navegarEstoque", (object)new Action<ISelecaoCommandParameter, Action>(NavegarEstoque));
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("contas", (object)enumerable);
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("centroCustos", (object)enumerable2);
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("planoContas", (object)enumerable3);
			((OverrideCollection<ParameterOverride, string, object>)val7).Add("especie", (object)val2);
			array5[0] = (ResolverOverride)val7;
			seletorDireito5.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoProdutosServicosOrdemServico>(container, (ResolverOverride[])(object)array5));
			Seletor<IPainel> seletorDireito6 = SeletorDireito;
			ResolverOverride[] array6 = new ResolverOverride[1];
			ParameterOverrides val8 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val8).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val8).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			array6[0] = (ResolverOverride)val8;
			seletorDireito6.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoOutrasInformacoesOrdemServico>(container, (ResolverOverride[])(object)array6));
			Seletor<IPainel> seletorDireito7 = SeletorDireito;
			ResolverOverride[] array7 = new ResolverOverride[1];
			ParameterOverrides val9 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val9).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val9).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			array7[0] = (ResolverOverride)val9;
			seletorDireito7.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoDetalhesOrdemServico>(container, (ResolverOverride[])(object)array7));
			Seletor<IPainel> seletorDireito8 = SeletorDireito;
			ResolverOverride[] array8 = new ResolverOverride[1];
			ParameterOverrides val10 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val10).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val10).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			array8[0] = (ResolverOverride)val10;
			seletorDireito8.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoContratoLocacaoOrdemServico>(container, (ResolverOverride[])(object)array8));
			SeletorDireito.PainelChanged += SeletorDireitoOnPainelChanged;
			SeletorEsquerdo.Selecionar<PainelEsquerdoOrdemServico>();
			SeletorDireito.Selecionar<PainelDireitoOrdemServico>();
			F4Command = (ICommand)new AsyncCommand((Func<Task>)AtualizarLista, (Func<bool>)(() => HelperObject.Is<PainelDireitoOrdemServico>((object)SeletorDireito.Painel)), false, (bool?)null);
			F5Command = (ICommand)new DelegateCommand((Action)AdicionarDavOs, (Func<bool>)PodeAdicionarDavOs, (bool?)null);
			CtrlFCommand = (ICommand)new DelegateCommand((Action)AlterarFocoParaOCampoDeBusca);
			MostrarPainelDireitoCommand = (ICommand)new DelegateCommand((Action)MostrarPainelDireito);
			EstadoDaAnimacao = "1";
		}

		private void MostrarPainelDireito()
		{
			EstadoDaAnimacao = ((EstadoDaAnimacao == "0") ? "1" : "0");
		}

		protected override void Navegar(object parametro)
		{
			ISelecaoCommandParameter val = (ISelecaoCommandParameter)((parametro is ISelecaoCommandParameter) ? parametro : null);
			if (val == null)
			{
				throw new Exception("Parâmetros do comando inválidos");
			}
			((NavigationBase)this).Navegar(val.View, (Action<NavigationResult>)delegate(NavigationResult result)
			{
				if (((IEnumerable<KeyValuePair<string, object>>)result.Context.Parameters).Any((KeyValuePair<string, object> x) => x.Value.Equals("Acesso negado!")))
				{
					IResolucao resolucao = ((NavigationBase)this).Resolucao;
					if (resolucao != null)
					{
						resolucao.Update();
					}
				}
				else
				{
					IResolucao resolucao2 = ((NavigationBase)this).Resolucao;
					if (resolucao2 != null)
					{
						resolucao2.Reset();
					}
				}
			}, val.Parametros);
		}

		private void AlterarFocoParaOCampoDeBusca()
		{
			SeletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.SetarFocoNaBusca();
		}

		private void NavegarEstoque(ISelecaoCommandParameter commandParameter, Action callback)
		{
			_callbackNavegar = callback;
			_navegarCommandParameter = commandParameter;
			if (VerificarNavegandoProdutoServicoListaView())
			{
				AssinarEventoFimNavegacaoProdutoServicoListaView();
			}
			else
			{
				NavegarEstoqueView();
			}
		}

		private bool VerificarNavegandoProdutoServicoListaView()
		{
			return ((NavigationBase)this).NavigationService.Journal.CurrentEntry.Uri.ToString() == "ProdutoServicoListaView";
		}

		private void AssinarEventoFimNavegacaoProdutoServicoListaView()
		{
			((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			if (!(regionNavigationEventArgs.Uri.ToString() == "ProdutoServicoListaView") && !HelperObject.IsNull((object)_navegarCommandParameter))
			{
				((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
				NavegarEstoqueView();
			}
		}

		private void NavegarEstoqueView()
		{
			((NavigationBase)this).Navegar(_navegarCommandParameter.View, (Action<NavigationResult>)delegate
			{
				_navegarCommandParameter = null;
			}, _navegarCommandParameter.Parametros);
		}

		private void ProcessarCallbackEstoqueView()
		{
			if (!HelperObject.IsNotNull((object)_navegarCommandParameter))
			{
				_callbackNavegar?.Invoke();
				_callbackNavegar = null;
			}
		}

		private void SeletorDireitoOnPainelChanged(object sender, IPainel e)
		{
			SeletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.Habilitar<PainelDireitoOrdemServico>(SeletorDireito);
			if (HelperObject.Is<PainelDireitoOrdemServico>((object)e) && HelperObject.To<PainelDireitoOrdemServico>((object)e).FocoAdicionarDavOs)
			{
				HelperObject.To<PainelDireitoOrdemServico>((object)e).SetarFocoAdicionarDavOs(valor: false);
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoAdicionarDavOs = !FocoAdicionarDavOs;
				});
			}
		}

		private Task AtualizarLista()
		{
			return SeletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.Pesquisar();
		}

		private bool PodeAdicionarDavOs()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>())
			{
				return HelperObject.Is<PainelDireitoOrdemServico>((object)SeletorDireito.Painel);
			}
			return false;
		}

		private async void AdicionarDavOs()
		{
			try
			{
				MovimentacaoDto movimentacaoDto = new MovimentacaoDto
				{
					Tipo = (TipoMovimentacao)2
				};
				MovimentacaoBaseDto obj = await _service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IReferenciaAtualizarService<MovimentacaoBaseDto>)(object)service).AtualizarDtoAsync((MovimentacaoBaseDto)(object)movimentacaoDto)));
				MovimentacaoDto movimentacaoDto2 = (MovimentacaoDto)(object)((obj is MovimentacaoDto) ? obj : null);
				if (DadosSistema.IsPet)
				{
					SeletorDireito.Get<PainelDireitoFichaOrdemServicoAnimal>().Preencher(movimentacaoDto2);
					SeletorDireito.Selecionar<PainelDireitoFichaOrdemServicoAnimal>();
				}
				else
				{
					SeletorDireito.Get<PainelDireitoFichaOrdemServico>().Preencher(movimentacaoDto2);
					SeletorDireito.Selecionar<PainelDireitoFichaOrdemServico>();
					SeletorDireito.Get<PainelDireitoFichaOrdemServico>().PreencherObjeto(movimentacaoDto2);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public void SetarFocoAdicionarDavOs()
		{
			FocoAdicionarDavOs = !FocoAdicionarDavOs;
		}

		private void NavegarEtiquetaView(NavigationParameters parametros)
		{
			((NavigationBase)this).Navegar("EtiquetaImpressaoFichaView", (Action<NavigationResult>)delegate
			{
			}, parametros);
			((NavigationBase)this).Resolucao.Reset();
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
			ProcessarCallbackEstoqueView();
			if (((NavigationBase)this).NavigationService.Journal.CurrentEntry.Uri.ToString() == "MovimentacaoSaidaListaView")
			{
				AtualizarLista();
			}
			IResolucao resolucao = ((NavigationBase)this).Resolucao;
			if (resolucao != null)
			{
				resolucao.Update();
			}
		}

		public override void Dispose()
		{
			SeletorDireito.PainelChanged -= SeletorDireitoOnPainelChanged;
			SeletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.FecharMethod();
			((BaseMinimizar)this).Dispose();
		}
	}
	public class OrdensServicoViewModel : ModuloBase
	{
		public OrdensServicoViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloOrdemServicoRegion, "OrdemServicoView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown


		public override void Original()
		{
			if (!((ModuloBase)this).ViewAtual().GetType().Name.Contains("OrdemServicoView"))
			{
				((ModuloBase)this).Resolucao.Reset();
			}
		}
	}
}
namespace Otus.Modulo.OrdensServico.ViewModels.Paineis
{
	public class OrdemServicoItemViewModel : BindableBase
	{
		public bool IsPet => DadosSistema.IsPet;

		public long Numero { get; set; }

		public string Objeto { get; set; }

		public string Animal { get; set; }

		public string CampoDestaque { get; set; }

		public MovimentacaoDto MovimentacaoDto { get; }

		public ISelecaoCommandParameter DavOsCommandParameter => ObterCommandParameter();

		public OrdemServicoItemViewModel(MovimentacaoDto movimentacaoDto)
		{
			MovimentacaoDto = movimentacaoDto;
			Numero = ((MovimentacaoBaseDto)movimentacaoDto).Numero;
			if (movimentacaoDto.Objetos.Any())
			{
				Objeto = movimentacaoDto.Objetos.FirstOrDefault().Objeto.ToTextPlacaSerial;
				CampoPersonalizadoDto obj = movimentacaoDto.Objetos.FirstOrDefault().Personalizado.Campos.FirstOrDefault((CampoPersonalizadoDto x) => ((EntityBaseDto)x.TipoPersonalizado).Id.Equals(DadosSistema.ConfiguracaoDto.Saida.OrdemServicoCampoDestaque.Referencia));
				CampoDestaque = ((obj != null) ? obj.ValorParaImpressao : null) ?? ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
			}
			if (movimentacaoDto.Animais.Any())
			{
				Animal = movimentacaoDto.Animais.FirstOrDefault().AnimalNome;
				IEnumerable<MovimentacaoAnimalDto> animais = movimentacaoDto.Animais;
				object obj2;
				if (animais == null)
				{
					obj2 = null;
				}
				else
				{
					CampoPersonalizadoDto obj3 = animais.FirstOrDefault().Personalizado.Campos.FirstOrDefault((CampoPersonalizadoDto x) => ((EntityBaseDto)x.TipoPersonalizado).Id.Equals(DadosSistema.ConfiguracaoDto.Saida.OrdemServicoCampoDestaque.Referencia));
					obj2 = ((obj3 != null) ? obj3.ValorParaImpressao : null);
				}
				if (obj2 == null)
				{
					obj2 = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
				}
				CampoDestaque = (string)obj2;
			}
			else
			{
				CampoDestaque = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa.Nome;
			}
		}

		private ISelecaoCommandParameter ObterCommandParameter()
		{
			SelecaoCommandParameter<MovimentacaoDto> obj = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
			{
			}, true);
			obj.AdicionarParametro("PesquisarDavOs", (object)MovimentacaoDto);
			obj.AdicionarParametro("CampoDestaque", (object)CampoDestaque);
			return (ISelecaoCommandParameter)(object)obj;
		}
	}
	public class PainelDireitoAnexosOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		public Lista<MovimentacaoObjetoDto> Objetos { get; set; } = new Lista<MovimentacaoObjetoDto>();


		public bool MostrarObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto), value);
			}
		}

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
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

		public Lista<ImagemViewModel> ImagensVisiveis
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => ImagensVisiveis));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ImagemViewModel>>((Expression<Func<Lista<ImagemViewModel>>>)(() => ImagensVisiveis), value);
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

		public Lista<ArquivoViewModel> ArquivosVisiveis
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => ArquivosVisiveis));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<ArquivoViewModel>>((Expression<Func<Lista<ArquivoViewModel>>>)(() => ArquivosVisiveis), value);
			}
		}

		public Lista<ArquivoViewModel> ArquivosExcluidos { get; set; } = new Lista<ArquivoViewModel>();


		public string TituloToText => "Anexos do DAV-OS";

		public ICommand F7Command { get; }

		public ICommand CarregarImagemCommand { get; set; }

		public ICommand CarregarArquivoCommand { get; set; }

		public ICommand<ImagemViewModel> RemoverImagemCommand { get; set; }

		public ICommand<ArquivoViewModel> RemoverArquivoCommand { get; set; }

		public ICommand AbrirAnexoImagemCommand { get; set; }

		public ICommand AbrirAnexoArquivoCommand { get; set; }

		public ICommand FecharCommand { get; set; }

		public PainelDireitoAnexosOrdemServico(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo)
		{
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Expected O, but got Unknown
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			F7Command = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)(() => true), (bool?)null);
			CarregarImagemCommand = (ICommand)new DelegateCommand((Action)CarregarImagem);
			CarregarArquivoCommand = (ICommand)new DelegateCommand((Action)CarregarArquivo);
			RemoverImagemCommand = (ICommand<ImagemViewModel>)(object)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)RemoverImagem);
			RemoverArquivoCommand = (ICommand<ArquivoViewModel>)(object)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)RemoverArquivo);
			AbrirAnexoImagemCommand = (ICommand)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)AbrirAnexoImagem);
			AbrirAnexoArquivoCommand = (ICommand)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)AbrirAnexoArquivo);
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
		}

		private void RemoverImagem(ImagemViewModel obj)
		{
			Imagens.Remover(obj);
			ImagensVisiveis.Remover(obj);
			ImagensExcluidas.Adicionar(obj);
		}

		private void CarregarImagem()
		{
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Expected O, but got Unknown
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione as imagens",
				Filter = "Arquivos de imagem (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png",
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string[] fileNames = openFileDialog.FileNames;
				foreach (string text in fileNames)
				{
					byte[] array = RedimensionarImagem.Redimensionar(File.ReadAllBytes(text), 1600, 80L, false);
					ImagemViewModel val = new ImagemViewModel(HelperImage.ToBitmapSource(array, 256), text, HelperDate.FormatarData(DateTime.Now, (FormatoData)6))
					{
						Bytes = array
					};
					val.ObjetoReferencia = ((EntityBaseDto)Objetos.ItemSelecionado.Objeto).Id;
					Imagens.Adicionar(val);
					ImagensVisiveis.Adicionar(val);
				}
			}
		}

		private void CarregarArquivo()
		{
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				FileName = "",
				Title = "Selecione os arquivos",
				Filter = "Pdf Files (*.pdf)|*.pdf|Arquivos de texto (*.txt)|*.txt",
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string[] fileNames = openFileDialog.FileNames;
				foreach (string text in fileNames)
				{
					ArquivoViewModel val = new ArquivoViewModel(Convert.ToBase64String(File.ReadAllBytes(text)), text, Path.GetFileName(text).Replace("-", ""), HelperDate.FormatarData(DateTime.Now, (FormatoData)6));
					val.ObjetoReferencia = ((EntityBaseDto)Objetos.ItemSelecionado.Objeto).Id;
					Arquivos.Adicionar(val);
					ArquivosVisiveis.Adicionar(val);
				}
			}
		}

		private void RemoverArquivo(ArquivoViewModel obj)
		{
			Arquivos.Remover(obj);
			ArquivosVisiveis.Remover(obj);
			ArquivosExcluidos.Adicionar(obj);
		}

		private void AbrirAnexoImagem(ImagemViewModel obj)
		{
			string text = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "Temp");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text).Attributes = FileAttributes.Directory;
			}
			string text2 = Path.Combine(text, Path.GetFileName(obj.FileName));
			try
			{
				File.WriteAllBytes(text2, (HelperObject.IsNotNull((object)obj.Bytes) && obj.Bytes.Length != 0) ? obj.Bytes : HelperImage.ToArrayByte(obj.Imagem));
			}
			catch (Exception)
			{
				DialogoFactory.MostrarDialogoConfirma("Ocorreu um erro ao salvar a imagem na pasta " + text, false, System.Windows.HorizontalAlignment.Center);
			}
			Process.Start(text2);
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

		private static byte[] ObterBytesParaEnvio(ImagemViewModel imagem)
		{
			if (!HelperObject.IsNotNull((object)imagem.Bytes) || imagem.Bytes.Length == 0)
			{
				return HelperImage.Compress(imagem.Imagem);
			}
			return imagem.Bytes;
		}

		private void Gravar()
		{
			if (!((MovimentacaoBaseDto)Movimentacao).Dav)
			{
				return;
			}
			List<AnexoObjetoDto> source = HelperIEnumerable.ToListEmptyWhenNull<AnexoObjetoDto>(((IEnumerable<ImagemViewModel>)ImagensVisiveis.Itens).Select((Func<ImagemViewModel, AnexoObjetoDto>)((ImagemViewModel x) => new AnexoObjetoDto
			{
				FileName = x.FileName,
				Image = ObterBytesParaEnvio(x),
				IsImage = true,
				ObjetoReferencia = HelperString.ToStringEmptyWhenNull(x.ObjetoReferencia)
			})));
			List<AnexoObjetoDto> source2 = HelperIEnumerable.ToListEmptyWhenNull<AnexoObjetoDto>(((IEnumerable<ImagemViewModel>)ImagensExcluidas.Itens).Select((Func<ImagemViewModel, AnexoObjetoDto>)((ImagemViewModel x) => new AnexoObjetoDto
			{
				FileName = x.FileName,
				IsImage = true,
				ObjetoReferencia = HelperString.ToStringEmptyWhenNull(x.ObjetoReferencia)
			})));
			List<AnexoObjetoDto> source3 = HelperIEnumerable.ToListEmptyWhenNull<AnexoObjetoDto>(((IEnumerable<ArquivoViewModel>)ArquivosVisiveis.Itens).Select((Func<ArquivoViewModel, AnexoObjetoDto>)((ArquivoViewModel x) => new AnexoObjetoDto
			{
				FileName = x.FileName,
				Arquivo = x.Arquivo,
				NomeArquivo = x.NomeArquivo,
				ObjetoReferencia = HelperString.ToStringEmptyWhenNull(x.ObjetoReferencia)
			})));
			List<AnexoObjetoDto> source4 = HelperIEnumerable.ToListEmptyWhenNull<AnexoObjetoDto>(((IEnumerable<ArquivoViewModel>)ArquivosExcluidos.Itens).Select((Func<ArquivoViewModel, AnexoObjetoDto>)((ArquivoViewModel x) => new AnexoObjetoDto
			{
				FileName = x.FileName,
				ObjetoReferencia = HelperString.ToStringEmptyWhenNull(x.ObjetoReferencia)
			})));
			foreach (MovimentacaoObjetoDto movimentacaoObjetoDto in Movimentacao.Objetos)
			{
				IEnumerable<AnexoObjetoDto> first = source.Where((AnexoObjetoDto x) => x.FileName.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) || (x.ObjetoReferencia?.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) ?? false));
				IEnumerable<AnexoObjetoDto> first2 = source2.Where((AnexoObjetoDto x) => x.FileName.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) || (x.ObjetoReferencia?.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) ?? false));
				IEnumerable<AnexoObjetoDto> second = source3.Where((AnexoObjetoDto x) => x.FileName.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) || (x.ObjetoReferencia?.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) ?? false));
				IEnumerable<AnexoObjetoDto> second2 = source4.Where((AnexoObjetoDto x) => x.FileName.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) || (x.ObjetoReferencia?.Contains(((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id) ?? false));
				movimentacaoObjetoDto.Anexos = first.Union(second);
				movimentacaoObjetoDto.AnexosExluidos = first2.Union(second2);
			}
			DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService>((Action<IDocumentoAuxiliarOrdemServicoModuleService>)delegate(IDocumentoAuxiliarOrdemServicoModuleService x)
			{
				x.SalvarTipoAnexo(Movimentacao);
			});
			((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
			_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
			_seletorDireito.Voltar();
		}

		public async void Preencher(MovimentacaoDto movimentacaoDto)
		{
			Movimentacao = movimentacaoDto;
			Objetos = new Lista<MovimentacaoObjetoDto>();
			Objetos.Adicionar(Movimentacao.Objetos);
			Objetos.PosicionarNoPrimeiro();
			Objetos.AlterouItemSelecionadoEvent += ObjetosOnAlterouItemSelecionadoEvent;
			MostrarObjeto = Objetos.Numero > 1;
			Lista<AnexoObjetoDto> imagensObjetos = new Lista<AnexoObjetoDto>();
			Lista<AnexoObjetoDto> arquivosObjetos = new Lista<AnexoObjetoDto>();
			foreach (MovimentacaoObjetoDto movimentacaoObjetoDto in Movimentacao.Objetos)
			{
				imagensObjetos.Adicionar(await DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<AnexoObjetoDto>>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<AnexoObjetoDto>>>)((IDocumentoAuxiliarOrdemServicoModuleService service) => service.ObterImagens(((EntityBaseDto)Movimentacao).Id, ((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id))));
				arquivosObjetos.Adicionar(await DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<AnexoObjetoDto>>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<AnexoObjetoDto>>>)((IDocumentoAuxiliarOrdemServicoModuleService service) => service.ObterArquivos(((EntityBaseDto)Movimentacao).Id, ((EntityBaseDto)movimentacaoObjetoDto.Objeto).Id))));
			}
			Imagens = new Lista<ImagemViewModel>();
			foreach (AnexoObjetoDto iten in imagensObjetos.Itens)
			{
				ImagemViewModel val = new ImagemViewModel(HelperImage.ToBitmapSource(iten.Image, 256), iten.FileName, HelperDate.FormatarData(iten.DataCriacao, (FormatoData)6))
				{
					Bytes = iten.Image
				};
				Imagens.Adicionar(val);
			}
			Arquivos = new Lista<ArquivoViewModel>();
			foreach (AnexoObjetoDto iten2 in arquivosObjetos.Itens)
			{
				ArquivoViewModel val2 = new ArquivoViewModel(iten2.Arquivo, iten2.FileName, iten2.NomeArquivo, HelperDate.FormatarData(iten2.DataCriacao, (FormatoData)6));
				Arquivos.Adicionar(val2);
			}
			PreencherAnexoVisivel(Objetos.ItemSelecionado);
		}

		private void ObjetosOnAlterouItemSelecionadoEvent(object sender, MovimentacaoObjetoDto objetoDto)
		{
			PreencherAnexoVisivel(objetoDto);
		}

		public void PreencherAnexoVisivel(MovimentacaoObjetoDto objetoDto)
		{
			ImagensVisiveis = new Lista<ImagemViewModel>();
			ImagensVisiveis.Adicionar(Imagens.Itens.Where((ImagemViewModel x) => x.FileName.Contains(((EntityBaseDto)objetoDto.Objeto).Id) || (x.ObjetoReferencia?.Equals(((EntityBaseDto)objetoDto.Objeto).Id) ?? false)));
			ArquivosVisiveis = new Lista<ArquivoViewModel>();
			ArquivosVisiveis.Adicionar(Arquivos.Itens.Where((ArquivoViewModel arquivoViewModel) => arquivoViewModel.FileName.Contains(((EntityBaseDto)objetoDto.Objeto).Id) || (arquivoViewModel.ObjetoReferencia?.Equals(((EntityBaseDto)objetoDto.Objeto).Id) ?? false)));
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
			_seletorDireito.Get<PainelDireitoOrdemServico>().Preencher(Movimentacao);
			_seletorDireito.Selecionar<PainelDireitoOrdemServico>();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
			((ViewModelValidationBase)Objetos).Dispose();
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoDetalhesOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private IReadOnlyCollection<TipoStatusDocumentoAuxiliarOrdemServico> _opcoesStatusDavOs;

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
			}
		}

		public bool MostrarObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto), value);
			}
		}

		public Lista<CodigoDescricaoDto> Prioridades
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Prioridades));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Prioridades), value);
			}
		}

		public Lista<CodigoDescricaoDto> Atendimentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Atendimentos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => Atendimentos), value);
			}
		}

		public Lista<TipoStatusDocumentoAuxiliarOrdemServico> TiposStatusDavOs
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>((Expression<Func<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>>)(() => TiposStatusDavOs));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>((Expression<Func<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>>)(() => TiposStatusDavOs), value);
			}
		}

		public ISelecaoCommandParameter AdicionarStatusDavOsCommand
		{
			get
			{
				//IL_0026: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Expected O, but got Unknown
				//IL_002b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0035: Expected O, but got Unknown
				SelecaoCommandParameter<StatusDavOsCadastroDto> obj = new SelecaoCommandParameter<StatusDavOsCadastroDto>("StatusDavOsCadastroFichaView", (Action<StatusDavOsCadastroDto>)AtualizarOpcoesStatusDavOs, PermissoesStatusDavOs.Para<OrdemServico>().PodeAdicionar);
				obj.AdicionarParametro("StatusDavOsCadastroViewModel", (object)new StatusDavOsCadastroViewModel(new StatusDavOsCadastroDto()));
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public ISelecaoCommandParameter PesquisarStatusDavOsCommand
		{
			get
			{
				SelecaoCommandParameter<StatusDavOsCadastroDto> obj = new SelecaoCommandParameter<StatusDavOsCadastroDto>("StatusDavOsCadastroListaView", (Action<StatusDavOsCadastroDto>)AtualizarOpcoesStatusDavOs, true);
				obj.AdicionarParametro("PermissoesStatusDavOs", (object)PermissoesStatusDavOs.Para<OrdemServico>());
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public string ObservacaoStatus
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ObservacaoStatus));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ObservacaoStatus), value);
			}
		}

		public bool PodeEditarCampos
		{
			get
			{
				if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>() && Movimentacao != null)
				{
					if (((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo != 1)
					{
						return ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 3;
					}
					return true;
				}
				return false;
			}
		}

		public string TituloToText => "Detalhes do DAV-OS";

		public Lista<MovimentacaoObjetoDto> Objetos { get; set; } = new Lista<MovimentacaoObjetoDto>();


		public ICommand F7Command { get; }

		public PainelDireitoDetalhesOrdemServico(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			F7Command = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)(() => true), (bool?)null);
		}

		private void Gravar()
		{
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_0119: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Expected O, but got Unknown
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Expected O, but got Unknown
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Expected O, but got Unknown
			if (!((MovimentacaoBaseDto)Movimentacao).Dav)
			{
				return;
			}
			int codigoStatusAnterior = ObterCodigoStatusDavOs(Movimentacao);
			if (!PodeEditarCampos)
			{
				StatusDocumentoAuxiliarOrdemServicoDto status = new StatusDocumentoAuxiliarOrdemServicoDto
				{
					Observacao = HelperString.ToStringEmptyWhenNull(ObservacaoStatus),
					DataHora = DateTime.Now,
					TipoStatusDocumentoAuxiliarOrdemServico = new TipoStatusDocumentoAuxiliarOrdemServicoDto
					{
						Codigo = TiposStatusDavOs.ItemSelecionado.Codigo
					},
					Usuario = HelperString.ToStringEmptyWhenNull(DadosSistema.NomeUsuario)
				};
				DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => x.AlterarStatusAsync(((EntityBaseDto)Movimentacao).Id, status))).ContinueWith(delegate(Task<MovimentacaoDto> task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						Movimentacao = task.Result;
						((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
						NotificacaoStatusDavOsWhatsAppService.Notificar(Movimentacao, codigoStatusAnterior);
						_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
						_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
					}
				});
			}
			else
			{
				Movimentacao.Atendimento = Atendimentos.ItemSelecionado;
				((MovimentacaoBaseDto)Movimentacao).StatusDocumentoAuxiliarOrdemServico = new StatusDocumentoAuxiliarOrdemServicoDto
				{
					Observacao = HelperString.ToStringEmptyWhenNull(ObservacaoStatus),
					DataHora = DateTime.Now,
					TipoStatusDocumentoAuxiliarOrdemServico = new TipoStatusDocumentoAuxiliarOrdemServicoDto
					{
						Codigo = TiposStatusDavOs.ItemSelecionado.Codigo
					},
					Usuario = HelperString.ToStringEmptyWhenNull(DadosSistema.NomeUsuario)
				};
				Movimentacao.Prioridade = Prioridades.ItemSelecionado;
				IEnumerable<ItemDto> itens = Movimentacao.Itens;
				DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)x).AtualizarAsync(Movimentacao))).ContinueWith(delegate(Task<MovimentacaoDto> task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						Movimentacao = task.Result;
						Movimentacao.Itens = itens;
						((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
						NotificacaoStatusDavOsWhatsAppService.Notificar(Movimentacao, codigoStatusAnterior);
						_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
						_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
					}
				});
			}
			_seletorDireito.Voltar();
		}

		private static int ObterCodigoStatusDavOs(MovimentacaoDto movimentacaoDto)
		{
			if (!HelperObject.IsNot<StatusDocumentoAuxiliarOrdemServicoDtoNull>((object)((MovimentacaoBaseDto)movimentacaoDto).StatusDocumentoAuxiliarOrdemServico))
			{
				return 0;
			}
			return ((MovimentacaoBaseDto)movimentacaoDto).StatusDocumentoAuxiliarOrdemServico.TipoStatusDocumentoAuxiliarOrdemServico.Codigo;
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			//IL_016a: Expected O, but got Unknown
			Movimentacao = movimentacaoDto;
			Objetos.Limpar();
			Objetos.Adicionar(Movimentacao.Objetos);
			MostrarObjeto = Objetos.Numero > 1;
			if (HelperObject.IsNull((object)_opcoesStatusDavOs))
			{
				_opcoesStatusDavOs = CarregadorStatusDavOs.ObterOpcoesAtivas(DadosSistema.GetService());
			}
			TiposStatusDavOs = new Lista<TipoStatusDocumentoAuxiliarOrdemServico>();
			TiposStatusDavOs.Adicionar((IEnumerable<TipoStatusDocumentoAuxiliarOrdemServico>)_opcoesStatusDavOs);
			if (HelperObject.IsNot<StatusDocumentoAuxiliarOrdemServicoDtoNull>((object)((MovimentacaoBaseDto)movimentacaoDto).StatusDocumentoAuxiliarOrdemServico))
			{
				TipoStatusDocumentoAuxiliarOrdemServicoDto statusAtual = ((MovimentacaoBaseDto)movimentacaoDto).StatusDocumentoAuxiliarOrdemServico.TipoStatusDocumentoAuxiliarOrdemServico;
				if (!TiposStatusDavOs.Existe((Expression<Func<TipoStatusDocumentoAuxiliarOrdemServico, bool>>)((TipoStatusDocumentoAuxiliarOrdemServico x) => x.Codigo == statusAtual.Codigo)))
				{
					TiposStatusDavOs.Adicionar((TipoStatusDocumentoAuxiliarOrdemServico)new StatusDavOsPersonalizado(statusAtual.Codigo, statusAtual.Descricao, statusAtual.Cor));
				}
				TiposStatusDavOs.Posicionar((Expression<Func<TipoStatusDocumentoAuxiliarOrdemServico, bool>>)((TipoStatusDocumentoAuxiliarOrdemServico x) => x.Codigo == statusAtual.Codigo));
			}
			else
			{
				TiposStatusDavOs.PosicionarNoPrimeiro();
			}
			((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => AdicionarStatusDavOsCommand));
			((BindableBase)this).RaisePropertyChanged<ISelecaoCommandParameter>((Expression<Func<ISelecaoCommandParameter>>)(() => PesquisarStatusDavOsCommand));
			Prioridades = new Lista<CodigoDescricaoDto>();
			IEnumerable<CodigoDescricaoDto> enumerable = CodigoDescricaoProjection.ProjectedAsCollectionDto((IEnumerable<CodigoDescricao>)Prioridades.Dados);
			Prioridades.Adicionar(enumerable);
			if (movimentacaoDto.Prioridade.Codigo > 0)
			{
				Prioridades.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == movimentacaoDto.Prioridade.Codigo));
			}
			else
			{
				Prioridades.PosicionarNoPrimeiro();
			}
			IEnumerable<CodigoDescricaoDto> enumerable2 = CodigoDescricaoProjection.ProjectedAsCollectionDto((IEnumerable<CodigoDescricao>)Atendimentos.Dados);
			Atendimentos = new Lista<CodigoDescricaoDto>();
			Atendimentos.Adicionar(enumerable2);
			if (movimentacaoDto.Atendimento.Codigo > 0)
			{
				Atendimentos.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == movimentacaoDto.Atendimento.Codigo));
			}
			else
			{
				Atendimentos.PosicionarNoPrimeiro();
			}
			ObservacaoStatus = ((MovimentacaoBaseDto)movimentacaoDto).StatusDocumentoAuxiliarOrdemServico.Observacao;
		}

		private void AtualizarOpcoesStatusDavOs(StatusDavOsCadastroDto statusDavOsCadastroDto)
		{
			if (HelperObject.IsNull((object)TiposStatusDavOs))
			{
				return;
			}
			int num;
			if (!HelperObject.IsNotNull((object)statusDavOsCadastroDto) || !((EntityDto)statusDavOsCadastroDto).Ativo)
			{
				TipoStatusDocumentoAuxiliarOrdemServico itemSelecionado = TiposStatusDavOs.ItemSelecionado;
				num = ((itemSelecionado != null) ? itemSelecionado.Codigo : 0);
			}
			else
			{
				num = statusDavOsCadastroDto.Codigo;
			}
			int codigoSelecionado = num;
			_opcoesStatusDavOs = CarregadorStatusDavOs.ObterOpcoesAtivas(DadosSistema.GetService());
			TiposStatusDavOs.Limpar();
			TiposStatusDavOs.Adicionar((IEnumerable<TipoStatusDocumentoAuxiliarOrdemServico>)_opcoesStatusDavOs);
			if (TiposStatusDavOs.Existe((Expression<Func<TipoStatusDocumentoAuxiliarOrdemServico, bool>>)((TipoStatusDocumentoAuxiliarOrdemServico x) => x.Codigo == codigoSelecionado)))
			{
				TiposStatusDavOs.Posicionar((Expression<Func<TipoStatusDocumentoAuxiliarOrdemServico, bool>>)((TipoStatusDocumentoAuxiliarOrdemServico x) => x.Codigo == codigoSelecionado));
			}
			else
			{
				TiposStatusDavOs.PosicionarNoPrimeiro();
			}
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoFichaOrdemServicoAnimal : UpdateViewBindableBase, IPainel
	{
		private readonly IWService _service;

		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly Action<object> _navegar;

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
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

		public bool FocoNaPessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa), value);
			}
		}

		public AnimalDto Animal
		{
			get
			{
				return ((BindableBase)this).GetProperty<AnimalDto>((Expression<Func<AnimalDto>>)(() => Animal));
			}
			set
			{
				((BindableBase)this).SetProperty<AnimalDto>((Expression<Func<AnimalDto>>)(() => Animal), value);
			}
		}

		public SelecaoCommandParameter<PessoaDto> PessoaCommandParameter => new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, true);

		public PessoaDto Pessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value, (Action)AoAlterarPessoa);
			}
		}

		public string NomePessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoa), value, (Action)delegate
				{
					RemoverQuandoNomeApagado(NomePessoa, () => Pessoa, delegate(PessoaDto x)
					{
						Pessoa = x;
					});
				});
			}
		}

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		public string Titulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Titulo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Titulo), value);
			}
		}

		public PersonalizadoViewModel PersonalizadoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<PersonalizadoViewModel>((Expression<Func<PersonalizadoViewModel>>)(() => PersonalizadoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<PersonalizadoViewModel>((Expression<Func<PersonalizadoViewModel>>)(() => PersonalizadoViewModel), value);
			}
		}

		public string Atendimento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Atendimento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Atendimento), value);
			}
		}

		public bool FocoNoAtendimento
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoAtendimento));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoAtendimento), value);
			}
		}

		public DateTime DataHoraPrevisao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraPrevisao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraPrevisao), value);
			}
		}

		public DateTime DataHoraAgendamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraAgendamento));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraAgendamento), value);
			}
		}

		public SelecaoCommandParameter<PessoaDto> TecnicoCommandParameter => (SelecaoCommandParameter<PessoaDto>)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SetarTecnico, (ValidateParameter<PessoaDto>)(object)ValidacaoTecnico);

		private ValidateParameterPessoa<PessoaDto> ValidacaoTecnico => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoa) => HelperObject.IsNot<TecnicoDtoNull>((object)pessoa.Fisica.Tecnico)), "Selecione um cadastro do tipo 'Técnico'!", (TipoPessoaCadastro)5, System.Windows.HorizontalAlignment.Center);

		public PessoaDto Tecnico
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Tecnico));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Tecnico), value, (Action)delegate
				{
					NomeTecnico = ((BaseDto)Tecnico).ToText;
				});
			}
		}

		public string NomeTecnico
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeTecnico));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeTecnico), value, (Action)RemoverTecnicoQuandoNomeApagado);
			}
		}

		public ICommand PesquisarTecnicoCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarTecnico);

		public SelecaoCommandParameter<PessoaDto> VendedorCommandParameter => (SelecaoCommandParameter<PessoaDto>)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SetarVendedor, (ValidateParameter<PessoaDto>)(object)ValidacaoVendedor);

		private ValidateParameterPessoa<PessoaDto> ValidacaoVendedor => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoa) => HelperObject.IsNot<VendedorDtoNull>((object)pessoa.Fisica.Vendedor)), "Selecione um cadastro do tipo 'Vendedor'!", (TipoPessoaCadastro)6, System.Windows.HorizontalAlignment.Center);

		public PessoaDto Vendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor), value, (Action)delegate
				{
					NomeVendedor = ((BaseDto)Vendedor).ToText;
				});
			}
		}

		public string NomeVendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeVendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeVendedor), value, (Action)RemoverVendedorQuandoNomeApagado);
			}
		}

		public ICommand PesquisarVendedorCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarVendedor);

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
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value);
			}
		}

		public ICommand F7Command { get; set; }

		public ICommand FecharCommand { get; set; }

		public ICommand PesquisarAnimalCommand { get; set; }

		public ICommand RemoverTecnicoCommand { get; set; }

		public ICommand RemoverVendedorCommand { get; set; }

		public ICommand RemoverTabelaPrecoCommand { get; set; }

		public PainelDireitoFichaOrdemServicoAnimal(IWService service, Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo, Action<object> navegar)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			_service = service;
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			_navegar = navegar;
			Titulo = "DAV - Ordem de serviço";
			F7Command = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
			PesquisarAnimalCommand = (ICommand)new DelegateCommand((Action)PesquisarAnimal);
			RemoverTecnicoCommand = (ICommand)new DelegateCommand((Action)RemoverTecnico, (Func<bool>)PodeRemoverTecnico, (bool?)null);
			RemoverVendedorCommand = (ICommand)new DelegateCommand((Action)RemoverVendedor, (Func<bool>)PodeRemoverVendedor, (bool?)null);
			RemoverTabelaPrecoCommand = (ICommand)new DelegateCommand((Action)RemoverTabelaPreco, (Func<bool>)PodeRemoverTabelaPreco, (bool?)null);
		}

		private bool PodeRemoverTecnico()
		{
			if (HelperObject.IsNotNull((object)Tecnico))
			{
				return HelperObject.IsNot<TecnicoDtoNull>((object)Tecnico);
			}
			return false;
		}

		private void RemoverTecnico()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Tecnico) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Tecnico = (PessoaDto)new PessoaDtoNull();
			}
		}

		private bool PodeRemoverVendedor()
		{
			if (HelperObject.IsNotNull((object)Vendedor))
			{
				return HelperObject.IsNot<VendedorDtoNull>((object)Vendedor);
			}
			return false;
		}

		private void RemoverVendedor()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = (PessoaDto)new PessoaDtoNull();
			}
		}

		private bool PodeRemoverTabelaPreco()
		{
			if (HelperObject.IsNotNull((object)TabelaPreco))
			{
				return HelperObject.IsNot<TabelaPrecoDtoNull>((object)TabelaPreco);
			}
			return false;
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
		}

		private async Task Gravar()
		{
			try
			{
				MovimentacaoAnimalDto item = new MovimentacaoAnimalDto
				{
					AnimalReferencia = ((EntityBaseDto)Animal).Id,
					AnimalNome = Animal.Nome,
					Atendimento = Atendimento,
					Personalizado = PersonalizadoViewModel.ObterDto()
				};
				Movimentacao.Animais = new List<MovimentacaoAnimalDto> { item };
				Movimentacao.DataHoraRecebimento = DataHoraAgendamento;
				Movimentacao.DataHoraPrevisao = DataHoraPrevisao;
				((MovimentacaoBaseDto)Movimentacao).Pessoa = Pessoa;
				Movimentacao.TecnicoResponsavel = Tecnico;
				Movimentacao.VendedorResponsavel = Vendedor;
				Movimentacao.TabelaPreco = TabelaPreco;
				((MovimentacaoBaseDto)Movimentacao).CalculadoPeloSistema = true;
				MovimentacaoDto val = await _service.Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService service) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)service).AdicionarAsync(Movimentacao)));
				((MovimentacaoBaseDto)val).CalculadoPeloSistema = true;
				_seletorDireito.Voltar();
				_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoImprimir();
				_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AdicionarOs(val);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNot<AnimalDtoNull>((object)Animal))
			{
				return HelperObject.IsNot<PessoaDtoNull>((object)Pessoa);
			}
			return false;
		}

		public void FecharMethod()
		{
			_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.OrdensServico.PosicionarNoPrimeiro();
			_seletorDireito.Get<PainelDireitoOrdemServico>()?.SetarFocoAdicionarDavOs(valor: true);
			_seletorDireito.Voltar();
		}

		private void PesquisarAnimal()
		{
			try
			{
				SelecaoCommandParameter<AnimalDto> val = new SelecaoCommandParameter<AnimalDto>("AnimalListaView", (Action<AnimalDto>)CallbackAnimal, true);
				val.AdicionarParametro("InformacaoPesquisa", (object)InformacaoPesquisa);
				if (string.IsNullOrWhiteSpace(InformacaoPesquisa))
				{
					_navegar(val);
					return;
				}
				PaginaEntityDto result = _service.Canal<IAnimalModuleService, Task<PaginaEntityDto>>((Func<IAnimalModuleService, Task<PaginaEntityDto>>)((IAnimalModuleService service) => ((IPesquisarAtivos)service).PesquisarAtivosAsync(InformacaoPesquisa, (short)1, (short)2))).Result;
				if (result.Lista.Count() == 1)
				{
					BaseDto obj = result.Lista.FirstOrDefault();
					CallbackAnimal((AnimalDto)(object)((obj is AnimalDto) ? obj : null));
				}
				else
				{
					_navegar(val);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void CallbackAnimal(AnimalDto animalDto)
		{
			InformacaoPesquisa = animalDto.Nome;
			Animal = animalDto;
			if (!HelperString.IsObjectIdDefaultOrEmpty(((EntityBaseDto)animalDto.Proprietario).Id))
			{
				Pessoa = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(((EntityBaseDto)animalDto.Proprietario).Id)));
				if (!HelperString.IsObjectIdDefaultOrEmpty(Pessoa.Cliente.VendedorReferencia))
				{
					PessoaDto vendedor = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(Pessoa.Cliente.VendedorReferencia)));
					Vendedor = vendedor;
				}
				if (HelperObject.IsNot<TabelaPrecoDtoNull>((object)Pessoa.Cliente.TabelaPreco))
				{
					TabelaPreco = Pessoa.Cliente.TabelaPreco;
				}
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoAtendimento = !FocoNoAtendimento;
				});
			}
			else if (HelperObject.IsExactlyObject<PessoaDtoNull>((object)Pessoa) || Pessoa.ConsumidorReservadoSistema)
			{
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNaPessoa = !FocoNaPessoa;
				});
			}
			else
			{
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoAtendimento = !FocoNoAtendimento;
				});
			}
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			DataHoraAgendamento = DateTime.Now;
			DataHoraPrevisao = DataHoraAgendamento.AddDays(DadosSistema.ConfiguracaoDto.Saida.DiasPrazoPadraoDavDavOs.Valor);
			Atendimento = string.Empty;
			Pessoa = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa;
			Tecnico = movimentacaoDto.TecnicoResponsavel;
			Vendedor = movimentacaoDto.VendedorResponsavel;
			TabelaPreco = movimentacaoDto.TabelaPreco;
			Animal = (AnimalDto)new AnimalDtoNull();
			InformacaoPesquisa = string.Empty;
			Movimentacao = movimentacaoDto;
			ProjectedAsViewModel();
		}

		private void SelecionouPessoa(PessoaDto pessoaDto)
		{
			Pessoa = pessoaDto;
			if (!HelperString.IsObjectIdDefaultOrEmpty(pessoaDto.Cliente.VendedorReferencia))
			{
				PessoaDto vendedor = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(pessoaDto.Cliente.VendedorReferencia)));
				Vendedor = vendedor;
			}
			if (HelperObject.IsNot<TabelaPrecoDtoNull>((object)pessoaDto.Cliente.TabelaPreco))
			{
				TabelaPreco = pessoaDto.Cliente.TabelaPreco;
			}
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoNoAtendimento = !FocoNoAtendimento;
			});
		}

		private void AoAlterarPessoa()
		{
			NomePessoa = ((BaseDto)Pessoa).ToText;
			ValidarFinanceiroPessoa();
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private static void RemoverQuandoNomeApagado(string nome, Func<PessoaDto> obter, Action<PessoaDto> setar)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(nome) && HelperObject.IsNot<PessoaDtoNull>((object)obter()))
			{
				setar((PessoaDto)new PessoaDtoNull());
			}
		}

		private void RemoverTecnicoQuandoNomeApagado()
		{
			if (!HelperString.IsNullOrEmpty(NomeTecnico) || HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				return;
			}
			RemoverTecnico();
			if (HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				return;
			}
			NomeTecnico = Tecnico.Nome;
			((UpdateViewBindableBase)this).DormirAsync(50, (Action<object>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeTecnico));
			});
		}

		private void RemoverVendedorQuandoNomeApagado()
		{
			if (!HelperString.IsNullOrEmpty(NomeVendedor) || HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			RemoverVendedor();
			if (HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			NomeVendedor = Vendedor.Nome;
			((UpdateViewBindableBase)this).DormirAsync(50, (Action<object>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeVendedor));
			});
		}

		private void ValidarFinanceiroPessoa()
		{
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Expected O, but got Unknown
			if (HelperObject.IsNotNull((object)Pessoa.Segmentacao) && HelperObject.IsNot<SegmentacaoDtoNull>((object)Pessoa.Segmentacao) && Pessoa.Segmentacao.AlertarOperadorAoIndicarNaMovimentacao)
			{
				DialogoFactory.MostrarDialogoConfirma($"Você está indicando uma pessoa que possui segmentação:\n{Pessoa.Segmentacao.SegmentacaoToText}", false, System.Windows.HorizontalAlignment.Left);
			}
			if (Pessoa.CreditosServicos.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("O cliente selecionado possui créditos em serviços, para utilizar esses créditos gere o DAV-OS atráves do cadastro do cliente.", false, System.Windows.HorizontalAlignment.Center);
			}
			if (!HelperObject.Is<ClienteDtoNull>((object)Pessoa.Cliente) && !(Pessoa.Cliente.ValorEmAtraso <= 0m))
			{
				if (DadosSistema.ConfiguracaoDto.Saida.AlertarClienteInadimplente.Valor && DialogoFactory.MostrarDialogoConfirmaInformacaoOculta(Pessoa.Cliente.AvisoValorEmAtraso, "Situação 1. Verifique!", false) != MessageBoxResult.OK)
				{
					Pessoa = (PessoaDto)new PessoaDtoNull();
				}
				else if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderParaInadimplente>(0m, (string)null) == null)
				{
					Pessoa = (PessoaDto)new PessoaDtoNull();
				}
			}
		}

		private void SetarTecnico(PessoaDto tecnico)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Tecnico) || !(((EntityBaseDto)Tecnico).Id != ((EntityBaseDto)tecnico).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Tecnico = tecnico;
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoAtendimento = !FocoNoAtendimento;
				});
			}
		}

		private async void PesquisarTecnico(ICommand navegarCommand)
		{
			string nomeTecnico = NomeTecnico;
			Action<PessoaDto> action = SetarTecnico;
			Action<ISelecaoCommandParameter> obj = delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			};
			ValidateParameterPessoa<PessoaDto> validacaoTecnico = ValidacaoTecnico;
			PessoaDto tecnico = Tecnico;
			await PesquisaPessoa.ExecutarAsync(nomeTecnico, (TipoPessoaCadastro)5, action, obj, true, validacaoTecnico, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (tecnico != null) ? ((EntityBaseDto)tecnico).Id : null);
		}

		private void SetarVendedor(PessoaDto vendedor)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || !(((EntityBaseDto)Vendedor).Id != ((EntityBaseDto)vendedor).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = vendedor;
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoAtendimento = !FocoNoAtendimento;
				});
			}
		}

		private async void PesquisarVendedor(ICommand navegarCommand)
		{
			string nomeVendedor = NomeVendedor;
			Action<PessoaDto> action = SetarVendedor;
			Action<ISelecaoCommandParameter> obj = delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			};
			ValidateParameterPessoa<PessoaDto> validacaoVendedor = ValidacaoVendedor;
			PessoaDto vendedor = Vendedor;
			await PesquisaPessoa.ExecutarAsync(nomeVendedor, (TipoPessoaCadastro)6, action, obj, true, validacaoVendedor, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (vendedor != null) ? ((EntityBaseDto)vendedor).Id : null);
		}

		public void ProjectedAsViewModel()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			PersonalizadoViewModel = new PersonalizadoViewModel(((EntityPersonalizadoBaseDto)Movimentacao).Personalizado);
			((ViewModelBase<PersonalizadoDto>)(object)PersonalizadoViewModel).ProjectedAsViewModel();
		}

		public void Load()
		{
			if (HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				UsuarioDto val = _service.Canal<IUsuarioModuleService, UsuarioDto>((Func<IUsuarioModuleService, UsuarioDto>)((IUsuarioModuleService service) => service.ObterUsuarioLogado()));
				if (HelperObject.IsNot<PessoaDtoNull>((object)val.Pessoa) && HelperObject.IsNot<TecnicoDtoNull>((object)val.Pessoa.Fisica.Tecnico))
				{
					Tecnico = val.Pessoa;
				}
			}
			if (HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				UsuarioDto val2 = _service.Canal<IUsuarioModuleService, UsuarioDto>((Func<IUsuarioModuleService, UsuarioDto>)((IUsuarioModuleService service) => service.ObterUsuarioLogado()));
				if (HelperObject.IsNot<PessoaDtoNull>((object)val2.Pessoa) && HelperObject.IsNot<VendedorDtoNull>((object)val2.Pessoa.Fisica.Vendedor))
				{
					Vendedor = val2.Pessoa;
				}
			}
		}

		public void UnLoad()
		{
			((ViewModelValidationBase)PersonalizadoViewModel).Dispose();
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoOutrasInformacoesOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		public bool IsPet => DadosSistema.IsPet;

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
			}
		}

		public bool MostrarObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto), value);
			}
		}

		public bool MostrarAnimal
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarAnimal));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarAnimal), value);
			}
		}

		public string TituloToText
		{
			get
			{
				if (!DadosSistema.IsPet)
				{
					return "Diagnóstico DAV-OS";
				}
				return "Atendimento DAV-OS";
			}
		}

		public Lista<MovimentacaoObjetoDto> Objetos { get; set; } = new Lista<MovimentacaoObjetoDto>();


		public Lista<MovimentacaoAnimalDto> Animais { get; set; } = new Lista<MovimentacaoAnimalDto>();


		public ICommand F7Command { get; }

		public PainelDireitoOutrasInformacoesOrdemServico(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo)
		{
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			F7Command = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)(() => true), (bool?)null);
		}

		private void Gravar()
		{
			if (((MovimentacaoBaseDto)Movimentacao).Dav)
			{
				MovimentacaoDto result = DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)x).AtualizarAsync(Movimentacao))).Result;
				((EntityVersaoDto)Movimentacao).Versao = ((EntityVersaoDto)result).Versao;
				((MovimentacaoBaseDto)Movimentacao).CalculadoPeloSistema = true;
				((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
				_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
				_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
				_seletorDireito.Voltar();
			}
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			Movimentacao = movimentacaoDto;
			Objetos.Limpar();
			Objetos.Adicionar(Movimentacao.Objetos);
			MostrarObjeto = Objetos.Numero > 1;
			Animais.Limpar();
			Animais.Adicionar(Movimentacao.Animais);
			MostrarAnimal = Animais.Numero > 1;
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoFichaOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly IWService _service;

		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly Action<object> _navegar;

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
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

		public bool FocoNoDefeito
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoDefeito));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoDefeito), value);
			}
		}

		public bool FocoNaPessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaPessoa), value);
			}
		}

		public Lista<CodigoDescricaoDto> TiposDavOs
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => TiposDavOs));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => TiposDavOs), value);
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

		public SelecaoCommandParameter<PessoaDto> PessoaCommandParameter => new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouPessoa, true);

		public PessoaDto Pessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value, (Action)AoAlterarPessoa);
			}
		}

		public string NomePessoa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomePessoa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomePessoa), value, (Action)delegate
				{
					RemoverQuandoNomeApagado(NomePessoa, () => Pessoa, delegate(PessoaDto x)
					{
						Pessoa = x;
					});
				});
			}
		}

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		public Lista<CodigoDescricaoDto> IndicadoresPresencaComprador
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => IndicadoresPresencaComprador));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => IndicadoresPresencaComprador), value);
			}
		}

		public Lista<CodigoDescricaoDto> IndicadoresOperacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => IndicadoresOperacao));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<CodigoDescricaoDto>>((Expression<Func<Lista<CodigoDescricaoDto>>>)(() => IndicadoresOperacao), value);
			}
		}

		public string Titulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Titulo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Titulo), value);
			}
		}

		public PersonalizadoViewModel PersonalizadoViewModel
		{
			get
			{
				return ((BindableBase)this).GetProperty<PersonalizadoViewModel>((Expression<Func<PersonalizadoViewModel>>)(() => PersonalizadoViewModel));
			}
			set
			{
				((BindableBase)this).SetProperty<PersonalizadoViewModel>((Expression<Func<PersonalizadoViewModel>>)(() => PersonalizadoViewModel), value);
			}
		}

		public string Defeito
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Defeito));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Defeito), value);
			}
		}

		public bool PossuiGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PossuiGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PossuiGarantia), value);
			}
		}

		public DateTime DataGarantia
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataGarantia));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataGarantia), value);
			}
		}

		public DateTime DataHoraPrevisao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraPrevisao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraPrevisao), value);
			}
		}

		public DateTime DataHoraAgendamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraAgendamento));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraAgendamento), value);
			}
		}

		public SelecaoCommandParameter<PessoaDto> TecnicoCommandParameter => (SelecaoCommandParameter<PessoaDto>)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SetarTecnico, (ValidateParameter<PessoaDto>)(object)ValidacaoTecnico);

		private ValidateParameterPessoa<PessoaDto> ValidacaoTecnico => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoa) => HelperObject.IsNot<TecnicoDtoNull>((object)pessoa.Fisica.Tecnico)), "Selecione um cadastro do tipo 'Técnico'!", (TipoPessoaCadastro)5, System.Windows.HorizontalAlignment.Center);

		public PessoaDto Tecnico
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Tecnico));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Tecnico), value, (Action)delegate
				{
					NomeTecnico = ((BaseDto)Tecnico).ToText;
				});
			}
		}

		public string NomeTecnico
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeTecnico));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeTecnico), value, (Action)RemoverTecnicoQuandoNomeApagado);
			}
		}

		public ICommand PesquisarTecnicoCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarTecnico);

		public SelecaoCommandParameter<PessoaDto> VendedorCommandParameter => (SelecaoCommandParameter<PessoaDto>)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SetarVendedor, (ValidateParameter<PessoaDto>)(object)ValidacaoVendedor);

		private ValidateParameterPessoa<PessoaDto> ValidacaoVendedor => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoa) => HelperObject.IsNot<VendedorDtoNull>((object)pessoa.Fisica.Vendedor)), "Selecione um cadastro do tipo 'Vendedor'!", (TipoPessoaCadastro)6, System.Windows.HorizontalAlignment.Center);

		public PessoaDto Vendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Vendedor), value, (Action)delegate
				{
					NomeVendedor = ((BaseDto)Vendedor).ToText;
				});
			}
		}

		public string NomeVendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeVendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeVendedor), value, (Action)RemoverVendedorQuandoNomeApagado);
			}
		}

		public ICommand PesquisarVendedorCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarVendedor);

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
				((BindableBase)this).SetProperty<TabelaPrecoDto>((Expression<Func<TabelaPrecoDto>>)(() => TabelaPreco), value);
			}
		}

		public ICommand F7Command { get; set; }

		public ICommand FecharCommand { get; set; }

		public ICommand PesquisarObjetoCommand { get; set; }

		public ICommand RemoverTecnicoCommand { get; set; }

		public ICommand RemoverVendedorCommand { get; set; }

		public ICommand RemoverObjetoCommand { get; set; }

		public ICommand RemoverTabelaPrecoCommand { get; set; }

		public PainelDireitoFichaOrdemServico(IWService service, Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo, Action<object> navegar)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Expected O, but got Unknown
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Expected O, but got Unknown
			_service = service;
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			_navegar = navegar;
			Titulo = "DAV - Ordem de serviço";
			F7Command = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
			PesquisarObjetoCommand = (ICommand)new DelegateCommand((Action)PesquisarObjeto);
			RemoverTecnicoCommand = (ICommand)new DelegateCommand((Action)RemoverTecnico, (Func<bool>)PodeRemoverTecnico, (bool?)null);
			RemoverVendedorCommand = (ICommand)new DelegateCommand((Action)RemoverVendedor, (Func<bool>)PodeRemoverVendedor, (bool?)null);
			RemoverObjetoCommand = (ICommand)new DelegateCommand((Action)RemoverObjeto, (Func<bool>)PodeRemoverObjeto, (bool?)null);
			RemoverTabelaPrecoCommand = (ICommand)new DelegateCommand((Action)RemoverTabelaPreco, (Func<bool>)PodeRemoverTabelaPreco, (bool?)null);
		}

		private bool PodeRemoverTecnico()
		{
			if (HelperObject.IsNotNull((object)Tecnico))
			{
				return HelperObject.IsNot<TecnicoDtoNull>((object)Tecnico);
			}
			return false;
		}

		private void RemoverTecnico()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Tecnico) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Tecnico = (PessoaDto)new PessoaDtoNull();
			}
		}

		private bool PodeRemoverVendedor()
		{
			if (HelperObject.IsNotNull((object)Vendedor))
			{
				return HelperObject.IsNot<VendedorDtoNull>((object)Vendedor);
			}
			return false;
		}

		private void RemoverVendedor()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = (PessoaDto)new PessoaDtoNull();
			}
		}

		private bool PodeRemoverObjeto()
		{
			if (HelperObject.IsNotNull((object)Objeto))
			{
				return HelperObject.IsNot<ObjetoDtoNull>((object)Objeto);
			}
			return false;
		}

		private void RemoverObjeto()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Objeto = (ObjetoDto)new ObjetoDtoNull();
			InformacaoPesquisa = string.Empty;
		}

		private bool PodeRemoverTabelaPreco()
		{
			if (HelperObject.IsNotNull((object)TabelaPreco))
			{
				return HelperObject.IsNot<TabelaPrecoDtoNull>((object)TabelaPreco);
			}
			return false;
		}

		private void RemoverTabelaPreco()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			TabelaPreco = (TabelaPrecoDto)new TabelaPrecoDtoNull();
		}

		private async Task Gravar()
		{
			try
			{
				MovimentacaoObjetoDto item = new MovimentacaoObjetoDto
				{
					Defeito = Defeito,
					Objeto = Objeto,
					Personalizado = PersonalizadoViewModel.ObterDto(),
					DataGarantia = DataGarantia,
					PossuiGarantia = PossuiGarantia
				};
				Movimentacao.Objetos = new List<MovimentacaoObjetoDto> { item };
				Movimentacao.DataHoraRecebimento = DataHoraAgendamento;
				Movimentacao.DataHoraPrevisao = DataHoraPrevisao;
				((MovimentacaoBaseDto)Movimentacao).Pessoa = Pessoa;
				Movimentacao.TecnicoResponsavel = Tecnico;
				Movimentacao.VendedorResponsavel = Vendedor;
				Movimentacao.TabelaPreco = TabelaPreco;
				((MovimentacaoBaseDto)Movimentacao).CalculadoPeloSistema = true;
				((MovimentacaoBaseDto)Movimentacao).TipoDavOs = TiposDavOs.ItemSelecionado;
				Movimentacao.IndicadorOperacao = IndicadoresOperacao.ItemSelecionado;
				Movimentacao.IndicadorPresencaComprador = IndicadoresPresencaComprador.ItemSelecionado;
				((MovimentacaoBaseDto)Movimentacao).PagamentosRecebimentos = new List<PagamentoRecebimentoDto>();
				MovimentacaoDto val = await _service.Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService service) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)service).AdicionarAsync(Movimentacao)));
				((MovimentacaoBaseDto)val).CalculadoPeloSistema = true;
				_seletorDireito.Voltar();
				_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoImprimir();
				_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AdicionarOs(val);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeGravar()
		{
			if (HelperObject.IsNot<ObjetoDtoNull>((object)Objeto))
			{
				return HelperObject.IsNot<PessoaDtoNull>((object)Pessoa);
			}
			return false;
		}

		public void FecharMethod()
		{
			_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>()?.OrdensServico.PosicionarNoPrimeiro();
			_seletorDireito.Get<PainelDireitoOrdemServico>()?.SetarFocoAdicionarDavOs(valor: true);
			_seletorDireito.Voltar();
		}

		private void PesquisarObjeto()
		{
			try
			{
				SelecaoCommandParameter<ObjetoDto> val = new SelecaoCommandParameter<ObjetoDto>("ObjetoListaView", (Action<ObjetoDto>)CallbackObjeto, true);
				if (string.IsNullOrWhiteSpace(InformacaoPesquisa))
				{
					val.AdicionarParametro("InformacaoPesquisa", (object)((EntityBaseDto)Pessoa).Id);
					_navegar(val);
					return;
				}
				val.AdicionarParametro("InformacaoPesquisa", (object)InformacaoPesquisa);
				PaginaEntityDto result = _service.Canal<IObjetoModuleService, Task<PaginaEntityDto>>((Func<IObjetoModuleService, Task<PaginaEntityDto>>)((IObjetoModuleService service) => ((IPesquisarAtivos)service).PesquisarAtivosAsync(InformacaoPesquisa, (short)1, (short)100))).Result;
				if (result.Lista.Count() == 1)
				{
					BaseDto obj = result.Lista.FirstOrDefault();
					CallbackObjeto((ObjetoDto)(object)((obj is ObjetoDto) ? obj : null));
				}
				else
				{
					_navegar(val);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void CallbackObjeto(ObjetoDto objetoDto)
		{
			InformacaoPesquisa = ((BaseDto)objetoDto).ToText;
			Objeto = objetoDto;
			if (!HelperString.IsObjectIdDefaultOrEmpty(objetoDto.PessoaReferencia))
			{
				Pessoa = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService x) => x.ObterPessoaPorId(objetoDto.PessoaReferencia)));
				if (!HelperString.IsObjectIdDefaultOrEmpty(Pessoa.Cliente.VendedorReferencia))
				{
					PessoaDto vendedor = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(Pessoa.Cliente.VendedorReferencia)));
					Vendedor = vendedor;
				}
				if (HelperObject.IsNot<TabelaPrecoDtoNull>((object)Pessoa.Cliente.TabelaPreco))
				{
					TabelaPreco = Pessoa.Cliente.TabelaPreco;
				}
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoDefeito = !FocoNoDefeito;
				});
			}
			else if (HelperObject.IsExactlyObject<PessoaDtoNull>((object)Pessoa))
			{
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNaPessoa = !FocoNaPessoa;
				});
			}
			else
			{
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoDefeito = !FocoNoDefeito;
				});
			}
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			DataHoraAgendamento = DateTime.Now;
			DataHoraPrevisao = DataHoraAgendamento.AddDays(DadosSistema.ConfiguracaoDto.Saida.DiasPrazoPadraoDavDavOs.Valor);
			Defeito = string.Empty;
			Pessoa = ((MovimentacaoBaseDto)movimentacaoDto).Pessoa;
			Tecnico = movimentacaoDto.TecnicoResponsavel;
			Vendedor = movimentacaoDto.VendedorResponsavel;
			TabelaPreco = movimentacaoDto.TabelaPreco;
			Objeto = (ObjetoDto)new ObjetoDtoNull();
			DataGarantia = DateTime.Today;
			PossuiGarantia = false;
			InformacaoPesquisa = string.Empty;
			Movimentacao = movimentacaoDto;
			ProjectedAsViewModel();
		}

		public void PreencherObjeto(MovimentacaoDto movimentacaoDto)
		{
			IEnumerable<MovimentacaoObjetoDto> objetos = movimentacaoDto.Objetos;
			bool? obj;
			if (objetos == null)
			{
				obj = null;
			}
			else
			{
				MovimentacaoObjetoDto obj2 = objetos.FirstOrDefault();
				obj = ((obj2 != null) ? new bool?(HelperObject.IsNot<ObjetoDtoNull>((object)obj2.Objeto)) : null);
			}
			if (obj ?? false)
			{
				CallbackObjeto(movimentacaoDto.Objetos.FirstOrDefault().Objeto);
			}
		}

		private void SelecionouPessoa(PessoaDto pessoaDto)
		{
			Pessoa = pessoaDto;
			if (!HelperString.IsObjectIdDefaultOrEmpty(pessoaDto.Cliente.VendedorReferencia))
			{
				PessoaDto vendedor = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(pessoaDto.Cliente.VendedorReferencia)));
				Vendedor = vendedor;
			}
			if (HelperObject.IsNot<TabelaPrecoDtoNull>((object)pessoaDto.Cliente.TabelaPreco))
			{
				TabelaPreco = pessoaDto.Cliente.TabelaPreco;
			}
			AtualizarIndicadorPresencaComprador();
			AtualizarIndicadorOperacao();
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoNoDefeito = !FocoNoDefeito;
			});
		}

		private void AoAlterarPessoa()
		{
			NomePessoa = ((BaseDto)Pessoa).ToText;
			ValidarFinanceiroPessoa();
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomePessoa, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouPessoa, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private static void RemoverQuandoNomeApagado(string nome, Func<PessoaDto> obter, Action<PessoaDto> setar)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(nome) && HelperObject.IsNot<PessoaDtoNull>((object)obter()))
			{
				setar((PessoaDto)new PessoaDtoNull());
			}
		}

		private void RemoverTecnicoQuandoNomeApagado()
		{
			if (!HelperString.IsNullOrEmpty(NomeTecnico) || HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				return;
			}
			RemoverTecnico();
			if (HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				return;
			}
			NomeTecnico = Tecnico.Nome;
			((UpdateViewBindableBase)this).DormirAsync(50, (Action<object>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeTecnico));
			});
		}

		private void RemoverVendedorQuandoNomeApagado()
		{
			if (!HelperString.IsNullOrEmpty(NomeVendedor) || HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			RemoverVendedor();
			if (HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				return;
			}
			NomeVendedor = Vendedor.Nome;
			((UpdateViewBindableBase)this).DormirAsync(50, (Action<object>)delegate
			{
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => NomeVendedor));
			});
		}

		private void ValidarFinanceiroPessoa()
		{
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0108: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Expected O, but got Unknown
			if (HelperObject.IsNotNull((object)Pessoa.Segmentacao) && HelperObject.IsNot<SegmentacaoDtoNull>((object)Pessoa.Segmentacao) && Pessoa.Segmentacao.AlertarOperadorAoIndicarNaMovimentacao)
			{
				DialogoFactory.MostrarDialogoConfirma($"Você está indicando uma pessoa que possui segmentação:\n{Pessoa.Segmentacao.SegmentacaoToText}", false, System.Windows.HorizontalAlignment.Left);
			}
			if (Pessoa.CreditosServicos.Any())
			{
				DialogoFactory.MostrarDialogoConfirma("O cliente selecionado possui créditos em serviços, para utilizar esses créditos gere o DAV-OS atráves do cadastro do cliente.", false, System.Windows.HorizontalAlignment.Center);
			}
			if (!HelperObject.Is<ClienteDtoNull>((object)Pessoa.Cliente) && !(Pessoa.Cliente.ValorEmAtraso <= 0m))
			{
				if (DadosSistema.ConfiguracaoDto.Saida.AlertarClienteInadimplente.Valor && DialogoFactory.MostrarDialogoConfirmaInformacaoOculta(Pessoa.Cliente.AvisoValorEmAtraso, "Situação 1. Verifique!", false) != MessageBoxResult.OK)
				{
					Pessoa = (PessoaDto)new PessoaDtoNull();
				}
				else if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderParaInadimplente>(0m, (string)null) == null)
				{
					Pessoa = (PessoaDto)new PessoaDtoNull();
				}
			}
		}

		public void AtualizarIndicadorPresencaComprador()
		{
			if (HelperObject.IsNull((object)IndicadoresPresencaComprador))
			{
				return;
			}
			if (HelperObject.Is<PessoaDtoNull>((object)Pessoa))
			{
				IndicadoresPresencaComprador.PosicionarNoPrimeiro();
			}
			JuridicaDto juridica = Movimentacao.Empresa.Juridica;
			bool? obj;
			if (juridica == null)
			{
				obj = null;
			}
			else
			{
				EmitenteDto emitente = juridica.Emitente;
				obj = ((emitente != null) ? new bool?(emitente.IsEcommerce) : null);
			}
			if (obj ?? false)
			{
				IndicadoresPresencaComprador.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto a) => a.Codigo == 2));
				return;
			}
			EnderecoDto enderecoPrincipal = Pessoa.EnderecoPrincipal;
			object obj2;
			if (enderecoPrincipal == null)
			{
				obj2 = null;
			}
			else
			{
				MunicipioDto municipio = enderecoPrincipal.Municipio;
				if (municipio == null)
				{
					obj2 = null;
				}
				else
				{
					UFDto uF = municipio.UF;
					obj2 = ((uF != null) ? uF.Sigla : null);
				}
			}
			EnderecoDto enderecoPrincipal2 = Movimentacao.Empresa.EnderecoPrincipal;
			object obj3;
			if (enderecoPrincipal2 == null)
			{
				obj3 = null;
			}
			else
			{
				MunicipioDto municipio2 = enderecoPrincipal2.Municipio;
				if (municipio2 == null)
				{
					obj3 = null;
				}
				else
				{
					UFDto uF2 = municipio2.UF;
					obj3 = ((uF2 != null) ? uF2.Sigla : null);
				}
			}
			if ((string)obj2 != (string)obj3)
			{
				IndicadoresPresencaComprador.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto a) => a.Codigo == 9));
			}
			else
			{
				IndicadoresPresencaComprador.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto a) => a.Codigo == 1));
			}
		}

		public void AtualizarIndicadorOperacao()
		{
			if (HelperObject.IsNull((object)IndicadoresOperacao))
			{
				return;
			}
			if (HelperObject.Is<PessoaDtoNull>((object)Pessoa))
			{
				IndicadoresOperacao.PosicionarNoPrimeiro();
			}
			else if (!HelperString.IsNullOrEmpty(Pessoa.IndicadorOperacao.Descricao))
			{
				IndicadoresOperacao.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == Pessoa.IndicadorOperacao.Codigo));
			}
			else if (!HelperObject.IsExactlyObject<EstrangeiroDto>((object)Pessoa.Estrangeiro) && (Pessoa.Classificacao.Codigo == 6 || Pessoa.Classificacao.Codigo == 7 || Pessoa.Classificacao.Codigo == 8))
			{
				IndicadoresOperacao.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == 1));
			}
			else
			{
				IndicadoresOperacao.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == 0));
			}
		}

		private void SetarTecnico(PessoaDto tecnico)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Tecnico) || !(((EntityBaseDto)Tecnico).Id != ((EntityBaseDto)tecnico).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Tecnico = tecnico;
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoDefeito = !FocoNoDefeito;
				});
			}
		}

		private async void PesquisarTecnico(ICommand navegarCommand)
		{
			string nomeTecnico = NomeTecnico;
			Action<PessoaDto> action = SetarTecnico;
			Action<ISelecaoCommandParameter> obj = delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			};
			ValidateParameterPessoa<PessoaDto> validacaoTecnico = ValidacaoTecnico;
			PessoaDto tecnico = Tecnico;
			await PesquisaPessoa.ExecutarAsync(nomeTecnico, (TipoPessoaCadastro)5, action, obj, true, validacaoTecnico, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (tecnico != null) ? ((EntityBaseDto)tecnico).Id : null);
		}

		private void SetarVendedor(PessoaDto vendedor)
		{
			if (!HelperObject.IsExactlyObject<PessoaDto>((object)Vendedor) || !(((EntityBaseDto)Vendedor).Id != ((EntityBaseDto)vendedor).Id) || DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirRemoverTrocarVendedorTecnico>(0m, (string)null) != null)
			{
				Vendedor = vendedor;
				((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
				{
					FocoNoDefeito = !FocoNoDefeito;
				});
			}
		}

		private async void PesquisarVendedor(ICommand navegarCommand)
		{
			string nomeVendedor = NomeVendedor;
			Action<PessoaDto> action = SetarVendedor;
			Action<ISelecaoCommandParameter> obj = delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			};
			ValidateParameterPessoa<PessoaDto> validacaoVendedor = ValidacaoVendedor;
			PessoaDto vendedor = Vendedor;
			await PesquisaPessoa.ExecutarAsync(nomeVendedor, (TipoPessoaCadastro)6, action, obj, true, validacaoVendedor, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (vendedor != null) ? ((EntityBaseDto)vendedor).Id : null);
		}

		public void ProjectedAsViewModel()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			PersonalizadoViewModel = new PersonalizadoViewModel(((EntityPersonalizadoBaseDto)Movimentacao).Personalizado);
			((ViewModelBase<PersonalizadoDto>)(object)PersonalizadoViewModel).ProjectedAsViewModel();
			TiposDavOs = new Lista<CodigoDescricaoDto>();
			TiposDavOs.Adicionar(CodigoDescricaoProjection.ProjectedAsCollectionDto((IEnumerable<CodigoDescricao>)TiposDavOs.Dados));
			if (((MovimentacaoBaseDto)Movimentacao).TipoDavOs.Codigo > 0)
			{
				TiposDavOs.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == ((MovimentacaoBaseDto)Movimentacao).TipoDavOs.Codigo));
			}
			else
			{
				TiposDavOs.PosicionarNoPrimeiro();
			}
			IEnumerable<CodigoDescricaoDto> enumerable = CodigoDescricaoProjection.ProjectedAsCollectionDto(IndicadoresPresencaComprador.Dados.OfType<INotaFiscalEletronica>().Cast<CodigoDescricao>());
			IndicadoresPresencaComprador = new Lista<CodigoDescricaoDto>();
			IndicadoresPresencaComprador.Adicionar(enumerable);
			IndicadoresPresencaComprador.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == Movimentacao.IndicadorPresencaComprador.Codigo));
			IEnumerable<CodigoDescricaoDto> enumerable2 = CodigoDescricaoProjection.ProjectedAsCollectionDto((IEnumerable<CodigoDescricao>)IndicadoresOperacaoConsumidorFinal.Dados);
			IndicadoresOperacao = new Lista<CodigoDescricaoDto>();
			IndicadoresOperacao.Adicionar(enumerable2);
			IndicadoresOperacao.Posicionar((Expression<Func<CodigoDescricaoDto, bool>>)((CodigoDescricaoDto x) => x.Codigo == Movimentacao.IndicadorOperacao.Codigo));
			ObservableExtensions.Subscribe<CodigoDescricaoDto>(IndicadoresOperacao.WhenAlterouItemSelecionado, (Action<CodigoDescricaoDto>)delegate(CodigoDescricaoDto x)
			{
				Movimentacao.IndicadorOperacao = x;
			});
			if (!((EntityBaseDto)Movimentacao).IsTransient())
			{
				AtualizarIndicadorOperacao();
			}
		}

		public void Load()
		{
			if (HelperObject.Is<PessoaDtoNull>((object)Tecnico))
			{
				UsuarioDto val = _service.Canal<IUsuarioModuleService, UsuarioDto>((Func<IUsuarioModuleService, UsuarioDto>)((IUsuarioModuleService service) => service.ObterUsuarioLogado()));
				if (HelperObject.IsNot<PessoaDtoNull>((object)val.Pessoa) && HelperObject.IsNot<TecnicoDtoNull>((object)val.Pessoa.Fisica.Tecnico))
				{
					Tecnico = val.Pessoa;
				}
			}
			if (HelperObject.Is<PessoaDtoNull>((object)Vendedor))
			{
				UsuarioDto val2 = _service.Canal<IUsuarioModuleService, UsuarioDto>((Func<IUsuarioModuleService, UsuarioDto>)((IUsuarioModuleService service) => service.ObterUsuarioLogado()));
				if (HelperObject.IsNot<PessoaDtoNull>((object)val2.Pessoa) && HelperObject.IsNot<VendedorDtoNull>((object)val2.Pessoa.Fisica.Vendedor))
				{
					Vendedor = val2.Pessoa;
				}
			}
		}

		public void UnLoad()
		{
			((ViewModelValidationBase)PersonalizadoViewModel).Dispose();
			Lista<CodigoDescricaoDto> indicadoresOperacao = IndicadoresOperacao;
			if (indicadoresOperacao != null)
			{
				((ViewModelValidationBase)indicadoresOperacao).Dispose();
			}
			Lista<CodigoDescricaoDto> indicadoresPresencaComprador = IndicadoresPresencaComprador;
			if (indicadoresPresencaComprador != null)
			{
				((ViewModelValidationBase)indicadoresPresencaComprador).Dispose();
			}
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly Action<NavigationParameters> _navegarEtiquetaView;

		private readonly IWService _service;

		private readonly IBoletoProxyService _boletoProxyService;

		public string BotaoTrocarStatus
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => BotaoTrocarStatus));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => BotaoTrocarStatus), value);
			}
		}

		public bool FocoImprimir
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoImprimir));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoImprimir), value);
			}
		}

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
			}
		}

		public IEnumerable<CampoPersonalizadoDto> CamposPersonalizados
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<CampoPersonalizadoDto>>((Expression<Func<IEnumerable<CampoPersonalizadoDto>>>)(() => CamposPersonalizados));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<CampoPersonalizadoDto>>((Expression<Func<IEnumerable<CampoPersonalizadoDto>>>)(() => CamposPersonalizados), value);
			}
		}

		public double AlturaMaximaDefeitoDiagnostico => (System.Windows.Application.Current.MainWindow.ActualHeight - 670.0 > 200.0) ? 200 : 60;

		public bool IsPet => DadosSistema.IsPet;

		public bool FocoAdicionarDavOs { get; set; }

		public ICommand CtrlF5Command { get; set; }

		public ICommand CtrlF6Command { get; set; }

		public ICommand CtrlF7Command { get; set; }

		public ICommand CtrlTCommand { get; set; }

		public ICommand CtrlPCommand { get; set; }

		public ICommand CtrlLCommand { get; set; }

		public ICommand CtrlECommand { get; set; }

		public ICommand CtrlWCommand { get; set; }

		public ICommand CtrlQCommand { get; set; }

		public ICommand ExibirAnexosCommand { get; set; }

		public ICommand CtrlAltECommand { get; set; }

		public PainelDireitoOrdemServico(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo, IImpressoraNaoFiscalService impressoraNaoFiscalService, IWService service, IBoletoProxyService boletoProxyService, Action<NavigationParameters> navegarEtiquetaView)
		{
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
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Expected O, but got Unknown
			//IL_015d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Expected O, but got Unknown
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Expected O, but got Unknown
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Expected O, but got Unknown
			//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ed: Expected O, but got Unknown
			//IL_020f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_navegarEtiquetaView = navegarEtiquetaView;
			_service = service;
			_boletoProxyService = boletoProxyService;
			CtrlF5Command = (ICommand)new DelegateCommand((Action)AdicionarItens, (Func<bool>)PodeAdicionarItens, (bool?)null);
			CtrlF6Command = (ICommand)new DelegateCommand((Action)AdicionarOutrasInformacoes, (Func<bool>)PodeAdicionarOutrasInformacoes, (bool?)null);
			CtrlF7Command = (ICommand)new DelegateCommand((Action)AdicionarDetalhes, (Func<bool>)PodeAdicionarDetalhes, (bool?)null);
			CtrlTCommand = (ICommand)new DelegateCommand((Action)AdicionarStatus, (Func<bool>)PodeAdicionarStatus, (bool?)null);
			CtrlQCommand = (ICommand)new DelegateCommand((Action)AdicionarDadosLocacao, (Func<bool>)PodeAdicionarDadosLocacao, (bool?)null);
			CtrlPCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)(() => Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao)), (bool?)null);
			CtrlLCommand = (ICommand)new DelegateCommand((Action)ImprimirContratoLocacao, (Func<bool>)PodeImprimirContratoLocacao, (bool?)null);
			CtrlECommand = (ICommand)new AsyncCommand((Func<Task>)EnviarEmail, (Func<bool>)(() => Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao)), false, (bool?)null);
			CtrlWCommand = (ICommand)new AsyncCommand((Func<Task>)CompartilharWhatsApp, (Func<bool>)(() => Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao)), false, (bool?)null);
			ExibirAnexosCommand = (ICommand)new DelegateCommand((Action)Anexos, (Func<bool>)(() => Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao) && !DadosSistema.IsPet && DadosSistema.VerificarPermissao<OrdemServico, PermitirVisualizarAnexos>()), (bool?)null);
			CtrlAltECommand = (ICommand)new DelegateCommand((Action)ImprimirEtiqueta, (Func<bool>)PodeImprimirEtiqueta, (bool?)null);
		}

		private void AdicionarItens()
		{
			MovimentacaoBaseDto result = DadosSistema.GetService().Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService x) => ((IReferenciaAtualizarService<MovimentacaoBaseDto>)(object)x).AtualizarDtoAsync((MovimentacaoBaseDto)(object)Movimentacao))).Result;
			MovimentacaoDto movimentacaoDto = (MovimentacaoDto)(object)((result is MovimentacaoDto) ? result : null);
			_seletorDireito.Get<PainelDireitoProdutosServicosOrdemServico>().Preencher(movimentacaoDto);
			_seletorDireito.Selecionar<PainelDireitoProdutosServicosOrdemServico>();
		}

		private bool PodeAdicionarItens()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, PermitirAdicionarItem>() && Movimentacao != null && (((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 1 || ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 19 || ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 3))
			{
				if (!Movimentacao.Objetos.Any())
				{
					return Movimentacao.Animais.Any();
				}
				return true;
			}
			return false;
		}

		private void AdicionarOutrasInformacoes()
		{
			_seletorDireito.Get<PainelDireitoOutrasInformacoesOrdemServico>().Preencher(Movimentacao);
			_seletorDireito.Selecionar<PainelDireitoOutrasInformacoesOrdemServico>();
		}

		private bool PodeAdicionarOutrasInformacoes()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>() && Movimentacao != null && (((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 1 || ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 19 || ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 3))
			{
				if (!Movimentacao.Objetos.Any())
				{
					return Movimentacao.Animais.Any();
				}
				return true;
			}
			return false;
		}

		private bool PodeAdicionarDadosLocacao()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>() && Movimentacao != null)
			{
				return ((MovimentacaoBaseDto)Movimentacao).TipoDavOs.Codigo == 1;
			}
			return false;
		}

		private void AdicionarDetalhes()
		{
			_seletorDireito.Get<PainelDireitoDetalhesOrdemServico>().Preencher(Movimentacao);
			_seletorDireito.Selecionar<PainelDireitoDetalhesOrdemServico>();
		}

		private bool PodeAdicionarDetalhes()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>() && Movimentacao != null)
			{
				if (!Movimentacao.Objetos.Any())
				{
					return Movimentacao.Animais.Any();
				}
				return true;
			}
			return false;
		}

		private async void AdicionarStatus()
		{
			SituacaoMovimentacaoDto val = ((((MovimentacaoBaseDto)Movimentacao).TipoDavOs.Codigo == 1) ? new SituacaoMovimentacaoDto
			{
				Codigo = 23
			} : ((((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo == 18) ? new SituacaoMovimentacaoDto
			{
				Codigo = 5
			} : new SituacaoMovimentacaoDto
			{
				Codigo = 18
			}));
			SituacaoMovimentacao situacao = SituacoesMovimentacao.ObterPorCodigo(val.Codigo);
			string text = string.Empty;
			if (HelperObject.Is<Entregue>((object)situacao) && DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirEntregarDavDavOs>(0m, (string)null) == null)
			{
				return;
			}
			bool flag = HelperObject.Is<Cancelado>((object)situacao) || HelperObject.Is<Inutilizado>((object)situacao) || HelperObject.Is<Rejeitado>((object)situacao) || HelperObject.Is<Entregue>((object)situacao) || HelperObject.Is<Aprovado>((object)situacao) || HelperObject.Is<Recebido>((object)situacao) || HelperObject.Is<Devolvido>((object)situacao);
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "Observação", situacao.ObterDescricao(), (IIcone)new Pergunta(), false, false, flag, (string)null, false, true) == MessageBoxResult.OK && DialogoFactory.MostrarDialogoSimNao($"Tem certeza que deseja alterar a situação da movimentação para {((CodigoDescricao)situacao).Descricao}?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				if (text == null && HelperObject.Is<Pronto>((object)situacao))
				{
					text = ((CodigoDescricao)situacao).Descricao;
				}
				LoaderProcessoService.MostrarLoader($"Adicionando situação {situacao.ObterDescricao()}...");
				HistoricoDto historico = new HistoricoDto
				{
					MovimentacaoReferencia = ((EntityBaseDto)Movimentacao).Id,
					SituacaoMovimentacao = val,
					Observacao = HelperString.ToStringEmptyWhenNull(text),
					MotivoCancelamento = 0
				};
				MovimentacaoBaseDto val2 = await DadosSistema.GetService().Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).AdicionarHistoricoAsync(historico)));
				LoaderProcessoService.EsconderLoader();
				if (((CodigoDescricao)situacao).Codigo == 5 || ((CodigoDescricao)situacao).Codigo == 23)
				{
					_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().PesquisarCommand.Execute(1);
				}
				else
				{
					_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(HelperObject.To<MovimentacaoDto>((object)val2));
				}
			}
		}

		private bool PodeAdicionarStatus()
		{
			if (DadosSistema.VerificarPermissao<OrdemServico, ManterDavOs>() && Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao) && ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo != 2 && ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo != 5)
			{
				return ((MovimentacaoBaseDto)Movimentacao).Situacao.Codigo != 4;
			}
			return false;
		}

		private void AdicionarDadosLocacao()
		{
			_seletorDireito.Get<PainelDireitoContratoLocacaoOrdemServico>().Preencher(Movimentacao, _service, _boletoProxyService, _impressoraNaoFiscalService);
			_seletorDireito.Selecionar<PainelDireitoContratoLocacaoOrdemServico>();
		}

		private void Imprimir()
		{
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Invalid comparison between Unknown and I4
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo comprovante de entrega...");
				if (DadosSistema.ConfiguracaoDto.Saida.UsarImpresso40ColunasComprovanteEntrega.Valor)
				{
					IRelatorio obj;
					if ((int)DadosSistema.TamanhoBobina != 1)
					{
						IRelatorio val = (IRelatorio)new RelatorioComprovanteEntregaOrdemServico79Mm80Mm(Movimentacao, DadosSistema.MargemEsquerdaImpressoraNaoFiscal, DadosSistema.RollPaper);
						obj = val;
					}
					else
					{
						IRelatorio val = (IRelatorio)new RelatorioComprovanteEntregaOrdemServico56Mm57Mm58Mm(Movimentacao, DadosSistema.MargemEsquerdaImpressoraNaoFiscal, DadosSistema.RollPaper);
						obj = val;
					}
					IRelatorio val2 = obj;
					_impressoraNaoFiscalService.Imprimir(val2);
				}
				else
				{
					ComprovanteEntregaOrdemServico val3 = new ComprovanteEntregaOrdemServico(Movimentacao);
					_impressoraNaoFiscalService.ImprimirImpressoraNaoFiscalOutrosImpressos((Relatorio)(object)val3);
				}
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

		public bool PodeImprimirContratoLocacao()
		{
			if (Movimentacao != null && HelperObject.IsNot<MovimentacaoDtoNull>((object)Movimentacao))
			{
				return ((MovimentacaoBaseDto)Movimentacao).TipoDavOs.Codigo.Equals(1);
			}
			return false;
		}

		private void ImprimirContratoLocacao()
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo contrato...");
				_impressoraNaoFiscalService.ImprimirImpressoraNaoFiscalOutrosImpressos((Relatorio)new RelatorioContratoLocacao(Movimentacao));
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

		public async Task EnviarEmail()
		{
			try
			{
				ComprovanteEntregaOrdemServico val = new ComprovanteEntregaOrdemServico(Movimentacao);
				string text = $"Comprovante_Entrega_{Guid.NewGuid()}.pdf";
				string text2 = Path.Combine(Path.GetTempPath(), text);
				if (DadosSistema.ConfiguracaoDto.Geral.ProtegerPdfExportadoComSenha.Valor && HelperString.RemoveCaracteresAlfanumerico(((MovimentacaoBaseDto)Movimentacao).Pessoa.CpfCnpj).Length >= 6)
				{
					XtraReport obj = ((Relatorio)val).Processar();
					PdfExportOptions val2 = new PdfExportOptions();
					val2.PasswordSecurityOptions.OpenPassword = HelperString.CopiarTexto(HelperString.RemoveCaracteresAlfanumerico(((MovimentacaoBaseDto)Movimentacao).Pessoa.CpfCnpj), 6);
					obj.ExportToPdf(text2, val2);
				}
				else
				{
					((Relatorio)val).Processar().ExportToPdf(text2);
				}
				EmailServiceFactory.SetCurrent((IEnvioEmailServiceFactory)(object)ObterEnvioEmailService(text2, text));
				await EmailServiceFactory.Create().Enviar();
				DialogoFactory.MostrarDialogoConfirma("E-mail enviado com sucesso!", false, System.Windows.HorizontalAlignment.Center);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private EnvioEmailServiceFactory ObterEnvioEmailService(string boleto, string nomeArquivo)
		{
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Expected O, but got Unknown
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			ConfiguracaoEmailDto val = ObterConfiguracoesEmail();
			string text = (val.EnviarCopiaRemetente ? val.UsuarioEmail : string.Empty);
			string endereco = ((MovimentacaoBaseDto)Movimentacao).Pessoa.EmailPrincipal.Endereco;
			Lista<AnexoEmail> val2 = new Lista<AnexoEmail>();
			if (!HelperString.IsNullOrEmpty(boleto) && !HelperString.IsNullOrEmpty(nomeArquivo))
			{
				val2.Adicionar(new AnexoEmail(boleto, nomeArquivo));
			}
			PreencherCorpoEmail(val);
			Email val3 = new Email().ComAssunto(val.ModeloEmail.Assunto).ComCorpo(val.ModeloEmail.Corpo).ComEmailResposta(val.ModeloEmail.EmailParaResposta)
				.ComEmailDestinatario(endereco)
				.ComAnexos(val2);
			if (!val.EnviarEmailAutomaticamente || HelperIEnumerable.IsEmpty<char>((IEnumerable<char>)endereco))
			{
				if (DialogoFactory.MostrarDialogoEmail(ref endereco, val3, (IIcone)new EnviarEmail(), false, false, false) != MessageBoxResult.OK)
				{
					throw new Exception("O e-mail não será enviado.");
				}
				if (HelperString.IsNullOrEmpty(val3.EmailDestinatario))
				{
					throw new Exception("O e-mail não será enviado. Destinatário não informado.");
				}
			}
			EnvioEmailServiceFactory val4 = new EnvioEmailServiceFactory().SetarServidorSmtp(val.ServidorSmtp).SetarAssunto(val3.Assunto).SetarEmailCopiaOculta(text, DadosSistema.EmpresaLogada.Nome)
				.SetarEmailRemetenteParaResposta(val3.EmailResposta, DadosSistema.EmpresaLogada.Nome)
				.SetarEmailRemetente(val.UsuarioEmail, DadosSistema.EmpresaLogada.Nome)
				.AdicionarDestinatarios(val3.EmailDestinatario, ((MovimentacaoBaseDto)Movimentacao).Pessoa.Nome)
				.SetarMensagem(val3.Corpo)
				.SetarPorta(val.PortaEmail)
				.SetarSenha(val.SenhaEmail)
				.SetarUsaSsl(val.UsaSsl)
				.SetarIgnorarCertificadoInvalido(val.IgnorarCertificadoInvalido);
			foreach (AnexoEmail item in val3.Anexos.Itens.Where((AnexoEmail x) => HelperObject.IsNotNull((object)x)))
			{
				val4.AdicionarAnexo(item.CaminhoArquivo, item.NomeArquivo);
			}
			return val4;
		}

		private ConfiguracaoEmailDto ObterConfiguracoesEmail()
		{
			return DadosSistema.GetService().Canal<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>((Func<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>)((IConfiguracaoModuleService x) => x.ObterConfiguracaoEmailComprovanteEntregaAsync())).Result;
		}

		private void PreencherCorpoEmail(ConfiguracaoEmailDto configuracaoEmail)
		{
			configuracaoEmail.ModeloEmail.Corpo = configuracaoEmail.ModeloEmail.Corpo.Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)0), ((MovimentacaoBaseDto)Movimentacao).Pessoa.Nome).Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)1), HelperLong.ToStringLeftZero(((MovimentacaoBaseDto)Movimentacao).Numero, 9)).Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)3), (((MovimentacaoBaseDto)Movimentacao).DataHoraEntradaSaida == DateTime.MinValue) ? HelperDate.FormatarData(((MovimentacaoBaseDto)Movimentacao).DataHoraEmissao, (FormatoData)6) : HelperDate.FormatarData(((MovimentacaoBaseDto)Movimentacao).DataHoraEntradaSaida, (FormatoData)6))
				.Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)2), ((MovimentacaoBaseDto)Movimentacao).Serie)
				.Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)4), HelperDecimal.FormatarCampoValor(((MovimentacaoBaseDto)Movimentacao).Total, 2, 0, false))
				.Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)5), string.Join("<br />", Movimentacao.Objetos.Select((MovimentacaoObjetoDto x) => x.Objeto.Descricao)))
				.Replace(HelperEnum.RetornarDescricao((Enum)(object)(TagEmailModeloDocumentoFiscalEletronico)6), string.Join("<br />", Movimentacao.Objetos.Select((MovimentacaoObjetoDto x) => x.DadosCompletosToText.Replace("\n", "<br />"))));
		}

		public async Task CompartilharWhatsApp()
		{
			try
			{
				string text = RetornarNumeroWhatsAppDestinatario();
				if (!HelperObject.IsNull((object)text))
				{
					DadosArquivoFtp val = RetornarArquivoEnvioPdf();
					List<DadosArquivoFtp> list = new List<DadosArquivoFtp>();
					if (HelperObject.IsNotNull((object)val))
					{
						list.Add(val);
					}
					await CompartilharArquivosAsync(list, text);
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private string RetornarNumeroWhatsAppDestinatario()
		{
			PessoaDto val = DadosSistema.GetService().Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(((EntityBaseDto)((MovimentacaoBaseDto)Movimentacao).Pessoa).Id)));
			List<TelefoneDto> list = new List<TelefoneDto>();
			if (!string.IsNullOrEmpty(val.TelefoneWhatsApp.Numero))
			{
				list.Add(val.TelefoneWhatsApp);
			}
			list.AddRange(val.Celulares);
			if (!list.Any())
			{
				list.Add(val.TelefonePrincipal);
			}
			if (list.Count > 1)
			{
				TelefoneDto obj = DialogoFactory.MostrarDialogoComboBox<TelefoneDto>("Informe o celular com DDD e número.", "WhatsApp", (IEnumerable<TelefoneDto>)list, "ToText", true);
				if (obj == null)
				{
					return null;
				}
				return obj.Numero;
			}
			string text = string.Empty;
			if (list.Count == 1)
			{
				text = list.FirstOrDefault().Numero;
			}
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "WhatsApp", "Informe o celular com DDD e número.", (IIcone)null, false, false, true, text, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return text;
		}

		private DadosArquivoFtp RetornarArquivoEnvioPdf()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Expected O, but got Unknown
			ComprovanteEntregaOrdemServico val = new ComprovanteEntregaOrdemServico(Movimentacao);
			string text = $"Comprovante_Entrega_{Guid.NewGuid()}.pdf";
			MemoryStream memoryStream = new MemoryStream();
			if (DadosSistema.ConfiguracaoDto.Geral.ProtegerPdfExportadoComSenha.Valor && HelperString.RemoveCaracteresAlfanumerico(((MovimentacaoBaseDto)Movimentacao).Pessoa.CpfCnpj).Length >= 6)
			{
				XtraReport obj = ((Relatorio)val).Processar();
				PdfExportOptions val2 = new PdfExportOptions();
				val2.PasswordSecurityOptions.OpenPassword = HelperString.CopiarTexto(HelperString.RemoveCaracteresAlfanumerico(((MovimentacaoBaseDto)Movimentacao).Pessoa.CpfCnpj), 6);
				obj.ExportToPdf((Stream)memoryStream, val2);
			}
			else
			{
				((Relatorio)val).Processar().ExportToPdf((Stream)memoryStream);
			}
			return DadosArquivoFtp.Criar().ComArquivoEnvio(Objeto<ArquivoEnvio>.Criar.ComNomeArquivo(text).ComArquivoBase64(Convert.ToBase64String(memoryStream.GetBuffer()))).ComTipoArquivoWhatsApp((TipoArquivoWhatsApp)13)
				.ComTipoArquivoCompartilhando("Comprovante de entrega - Ordem de serviço");
		}

		private async Task CompartilharArquivosAsync(IEnumerable<DadosArquivoFtp> arquivos, string telefone)
		{
			object obj;
			if (!HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Saida.ModeloMensagemComprovanteEntrega.Referencia))
			{
				obj = DadosSistema.GetService().Canal<IMensagemWhatsappModeloModuleService, Task<MensagemWhatsappModeloDto>>((Func<IMensagemWhatsappModeloModuleService, Task<MensagemWhatsappModeloDto>>)delegate(IMensagemWhatsappModeloModuleService service)
				{
					ConfiguracaoDto configuracaoDto2 = DadosSistema.ConfiguracaoDto;
					object obj2;
					if (configuracaoDto2 == null)
					{
						obj2 = null;
					}
					else
					{
						SaidaDto saida = configuracaoDto2.Saida;
						if (saida == null)
						{
							obj2 = null;
						}
						else
						{
							SelecaoDto modeloMensagemComprovanteEntrega = saida.ModeloMensagemComprovanteEntrega;
							obj2 = ((modeloMensagemComprovanteEntrega != null) ? modeloMensagemComprovanteEntrega.Referencia : null);
						}
					}
					return service.ObterPorReferencia((string)obj2);
				}).Result.Mensagem;
			}
			else
			{
				ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
				if (configuracaoDto == null)
				{
					obj = null;
				}
				else
				{
					AuxiliarDto auxiliar = configuracaoDto.Auxiliar;
					obj = ((auxiliar != null) ? auxiliar.MensagemWhatsApp.Valor : null);
				}
			}
			string text = (string)obj;
			await WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(telefone)
				.ComArquivos(arquivos)
				.ComMensagemWhatsApp(text)
				.ComEnderecoServidor(DadosSistema.EnderecoServidorWhatsApp)
				.ComCpfCnpjEmitente(DadosSistema.EmpresaLogada.CpfCnpj)
				.Compartilhar()
				.ContinueWith(delegate(Task<string> task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else if (!string.IsNullOrEmpty(task.Result))
					{
						DialogoFactory.MostrarDialogoConfirma(task.Result, false, System.Windows.HorizontalAlignment.Center);
					}
					else
					{
						DialogoFactory.MostrarDialogoConfirma($"Compartilhado com sucesso com: {telefone}", false, System.Windows.HorizontalAlignment.Center);
					}
				});
		}

		public void Anexos()
		{
			_seletorDireito.Get<PainelDireitoAnexosOrdemServico>().Preencher(Movimentacao);
			_seletorDireito.Selecionar<PainelDireitoAnexosOrdemServico>();
		}

		private bool PodeImprimirEtiqueta()
		{
			return HelperObject.IsNotNull((object)Movimentacao);
		}

		private void ImprimirEtiqueta()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("ID", (object)Guid.NewGuid().ToString());
			val.Add("dtoSaida", (object)Movimentacao);
			NavigationParameters obj = val;
			_navegarEtiquetaView(obj);
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			Movimentacao = movimentacaoDto;
			object camposPersonalizados;
			if (!IsPet)
			{
				MovimentacaoObjetoDto obj = Movimentacao.Objetos.FirstOrDefault();
				camposPersonalizados = ((obj != null) ? obj.Personalizado.Campos : null);
			}
			else
			{
				MovimentacaoAnimalDto obj2 = Movimentacao.Animais.FirstOrDefault();
				camposPersonalizados = ((obj2 != null) ? obj2.Personalizado.Campos : null);
			}
			CamposPersonalizados = (IEnumerable<CampoPersonalizadoDto>)camposPersonalizados;
			MovimentacaoDto movimentacao = Movimentacao;
			object botaoTrocarStatus;
			if (movimentacao != null)
			{
				CodigoDescricaoDto tipoDavOs = ((MovimentacaoBaseDto)movimentacao).TipoDavOs;
				if (((tipoDavOs != null) ? new int?(tipoDavOs.Codigo) : null) == 1)
				{
					botaoTrocarStatus = "Devolver";
					goto IL_00aa;
				}
			}
			botaoTrocarStatus = "Terminar/Entregar";
			goto IL_00aa;
			IL_00aa:
			BotaoTrocarStatus = (string)botaoTrocarStatus;
		}

		public void SetarFocoAdicionarDavOs(bool valor)
		{
			FocoAdicionarDavOs = valor;
		}

		public void SetarFocoImprimir()
		{
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoImprimir = !FocoImprimir;
			});
		}

		public void FecharMethod()
		{
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
			Movimentacao = null;
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoContratoLocacaoOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private IWService _service;

		private IBoletoProxyService _boletoProxyService;

		private IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		public string Titulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Titulo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Titulo), value);
			}
		}

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
			}
		}

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
					((MovimentacaoBaseDto)Movimentacao).DadosContratoDavOs = DadosContrato;
				});
			}
		}

		public ICommand FecharCommand { get; set; }

		public ICommand F7Command { get; }

		public ICommand CtrlICommand { get; }

		public ICommand CtrlGCommand { get; }

		public PainelDireitoContratoLocacaoOrdemServico(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Expected O, but got Unknown
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Expected O, but got Unknown
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
			PodePesquisarObservacao = DadosSistema.PodeExecutarFuncao(new Type[3]
			{
				typeof(Gerencial),
				typeof(Administrador),
				typeof(AdministradorIndustria)
			});
			F7Command = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)(() => true), (bool?)null);
			CtrlICommand = (ICommand)new AsyncCommand((Func<Task>)ImprimirFaturaFiscal, (Func<bool>)PodeGerarFaturaFiscal, (bool?)null);
			CtrlGCommand = (ICommand)new AsyncCommand((Func<Task>)GerarFaturaFiscal, (Func<bool>)PodeGerarFaturaFiscal, (bool?)null);
		}

		private bool PodeGerarFaturaFiscal()
		{
			return DadosSistema.VerificarPermissao<MovimentacaoSaida, PermitirGerarFaturaFiscal>();
		}

		private async Task ImprimirFaturaFiscal()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				MovimentacaoDto dto = Movimentacao;
				FaturaFiscalDto val = new FaturaFiscalDto
				{
					Empresa = dto.Empresa,
					Pessoa = ((MovimentacaoBaseDto)dto).Pessoa,
					Itens = dto.Itens.Select((Func<ItemDto, ItemFaturaFiscalDto>)((ItemDto x) => new ItemFaturaFiscalDto
					{
						Codigo = x.ProdutoServico.CodigoInterno,
						Descricao = x.ProdutoServico.Descricao,
						TotalLiquido = x.TotalLiquido
					})),
					Numero = ((MovimentacaoBaseDto)dto).Numero,
					Observacao = dto.Observacao
				};
				_impressoraNaoFiscalService.ImprimirImpressoraNaoFiscalOutrosImpressos((Relatorio)new RelatorioFaturaFiscal(val));
				await _service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).AdicionarHistoricoAsync(new HistoricoDto
				{
					DataHora = DateTime.Now,
					MovimentacaoReferencia = ((EntityBaseDto)dto).Id,
					Observacao = "Impresso da fatura gerado com sucesso",
					SituacaoMovimentacao = ((MovimentacaoBaseDto)dto).Situacao
				})));
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

		private async Task GerarFaturaFiscal()
		{
			try
			{
				if (DialogoFactory.MostrarDialogoSimNao("Deseja gerar a fatura?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				LoaderProcessoService.MostrarLoader("Gerando fatura...");
				MovimentacaoDto dto = Movimentacao;
				if (((MovimentacaoBaseDto)dto).Historicos.Any((HistoricoDto x) => x.Observacao.Contains("Fatura")) && DialogoFactory.MostrarDialogoSimNao(string.Format("Já existe uma fatura gerada em {0}, deseja continuar?", HelperDate.FormatarData(((MovimentacaoBaseDto)dto).Historicos.Last((HistoricoDto x) => x.Observacao.Contains("Fatura")).DataHora, (FormatoData)6)), false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				int ordem = 0;
				Lista<RecebimentoDto> parcelasGeradas = new Lista<RecebimentoDto>();
				foreach (PagamentoRecebimentoDto pagamentosRecebimento in ((MovimentacaoBaseDto)dto).PagamentosRecebimentos)
				{
					if (pagamentosRecebimento.Valor <= 0m)
					{
						DialogoFactory.MostrarDialogoConfirma("Parcela com valor zerado não será adicionada.", false, System.Windows.HorizontalAlignment.Center);
						continue;
					}
					RecebimentoDto recebimentoDto = new RecebimentoDto
					{
						Descricao = $"Fatura DAV-OS número {((MovimentacaoBaseDto)dto).Numero}",
						Pessoa = pagamentosRecebimento.Pessoa,
						DataEmissao = DateTime.Now,
						Conta = pagamentosRecebimento.Conta,
						Documento = ((MovimentacaoBaseDto)dto).Numero.ToString(),
						EspeciePagamento = pagamentosRecebimento.EspeciePagamento,
						CentroCusto = pagamentosRecebimento.CentroCusto,
						PlanoConta = pagamentosRecebimento.PlanoConta,
						Valor = pagamentosRecebimento.Valor,
						DataVencimento = pagamentosRecebimento.DataVencimento,
						Ordem = ordem++,
						MovimentacaoReferenciaFaturaFiscal = ((EntityBaseDto)dto).Id,
						ContratoReferenciaFaturaFiscal = string.Empty,
						Juro = DadosSistema.ConfiguracaoDto.Financeiro.PercentualJuro.Valor,
						Multa = DadosSistema.ConfiguracaoDto.Financeiro.PercentualMulta.Valor,
						AVista = pagamentosRecebimento.DataVencimento.Date.Equals(DateTime.Today)
					};
					if (((PagamentoRecebimentoDto)recebimentoDto).EspeciePagamento.Codigo == 8 && ((PagamentoRecebimentoDto)recebimentoDto).DataVencimento < DateTime.Today)
					{
						DialogoFactory.MostrarDialogoConfirma("Parcela com espécie Boleto e vencimento anterior à data atual não será adicionada.", false, System.Windows.HorizontalAlignment.Center);
						continue;
					}
					Lista<RecebimentoDto> val = parcelasGeradas;
					val.Adicionar(await _service.Canal<IRecebimentoModuleService, Task<RecebimentoDto>>((Func<IRecebimentoModuleService, Task<RecebimentoDto>>)((IRecebimentoModuleService x) => ((IAdicionar<RecebimentoDto>)(object)x).AdicionarAsync(recebimentoDto))));
				}
				if (parcelasGeradas.Itens.Any((RecebimentoDto x) => ((PagamentoRecebimentoDto)x).AVista || ((PagamentoRecebimentoDto)x).DataVencimento < DateTime.Today))
				{
					_service.Canal<IRecebimentoModuleService>((Action<IRecebimentoModuleService>)delegate(IRecebimentoModuleService x)
					{
						x.QuitarParcelasAvista(from y in parcelasGeradas.Itens
							where (((PagamentoRecebimentoDto)y).AVista && ((PagamentoRecebimentoDto)y).EspeciePagamento.Codigo != 8) || ((PagamentoRecebimentoDto)y).DataVencimento < DateTime.Today
							select y into t
							select ((EntityBaseDto)t).Id);
					});
				}
				if (parcelasGeradas.Itens.Any((RecebimentoDto x) => x.IsBoleto))
				{
					ImpressaoBoletoService.Init().ComBoletoProxyService(_boletoProxyService).VerificarImpressaoBoletosFaturaFiscal((IEnumerable<RecebimentoDto>)parcelasGeradas.Itens);
					ConfiguracaoEmailDto configuracaoEmail = await _service.Canal<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>((Func<IConfiguracaoModuleService, Task<ConfiguracaoEmailDto>>)((IConfiguracaoModuleService x) => x.ObterConfiguracaoEmailBoletoParaFaturaAsync(dto)));
					if (string.IsNullOrEmpty(configuracaoEmail.ServidorSmtp))
					{
						throw new Exception("Acesse Configurações/Auxiliar para configurar os dados do servidor de envio de e-mail.");
					}
					foreach (RecebimentoDto item in parcelasGeradas.Itens.Where((RecebimentoDto x) => x.IsBoleto))
					{
						if (!string.IsNullOrEmpty(((PagamentoRecebimentoDto)item).Pessoa.EmailCobrancaPrincipal.Endereco))
						{
							string text = BoletoService.ObterArquivoPdf(new List<IBoletoProxy> { _boletoProxyService.RetornarBoletoProxy(item) });
							string text2 = string.Format("Boleto_{0} {1}.pdf", string.Join("_", $"{((PagamentoRecebimentoDto)item).Documento}-{((PagamentoRecebimentoDto)item).Ordem}"), Guid.NewGuid());
							try
							{
								await EnvioEmailMovimentacaoService.Criar(_service, (MovimentacaoBaseDto)(object)dto, _boletoProxyService, (IConfiguracaoClientService)null).EnviarEmail(configuracaoEmail, configuracaoEmail.ModeloEmail, ((PagamentoRecebimentoDto)item).Pessoa.EmailCobrancaPrincipal.Endereco, (AnexoEmail[])(object)new AnexoEmail[1]
								{
									new AnexoEmail(text, text2)
								});
							}
							catch (Exception value)
							{
								Console.WriteLine(value);
								throw;
							}
						}
					}
				}
				await _service.Canal<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>((Func<IMovimentacaoSaidaModuleService, Task<MovimentacaoBaseDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).AdicionarHistoricoAsync(new HistoricoDto
				{
					DataHora = DateTime.Now,
					MovimentacaoReferencia = ((EntityBaseDto)dto).Id,
					Observacao = "Fatura gerada com sucesso",
					SituacaoMovimentacao = ((MovimentacaoBaseDto)dto).Situacao
				})));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			finally
			{
				DialogoFactory.MostrarDialogoConfirma("Concluído!", false, System.Windows.HorizontalAlignment.Center);
				LoaderProcessoService.EsconderLoader();
			}
		}

		public void Preencher(MovimentacaoDto movimentacaoDto, IWService service, IBoletoProxyService boletoProxyService, IImpressoraNaoFiscalService impressoraNaoFiscalService)
		{
			if (!HelperObject.IsNull((object)movimentacaoDto))
			{
				Movimentacao = movimentacaoDto;
				DadosContrato = ((movimentacaoDto != null) ? ((MovimentacaoBaseDto)movimentacaoDto).DadosContratoDavOs : null);
				Titulo = $"Contrato locação - Ordem de serviço {((MovimentacaoBaseDto)Movimentacao).Numero}";
				_service = service;
				_boletoProxyService = boletoProxyService;
				_impressoraNaoFiscalService = impressoraNaoFiscalService;
			}
		}

		private void Gravar()
		{
			if (((MovimentacaoBaseDto)Movimentacao).Dav)
			{
				MovimentacaoDto result = DadosSistema.GetService().Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)x).AtualizarAsync(Movimentacao))).Result;
				((EntityVersaoDto)Movimentacao).Versao = ((EntityVersaoDto)result).Versao;
				((MovimentacaoBaseDto)Movimentacao).CalculadoPeloSistema = true;
				((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
				_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
				_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
				_seletorDireito.Voltar();
			}
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoProdutosServicosOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Action<object> _navegar;

		private readonly Action<ISelecaoCommandParameter, Action> _navegarEstoque;

		private readonly IWService _service;

		private readonly IAutenticacaoService _autenticacaoService;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private DateTime _quantidadeEmHora;

		private decimal _diferencaQuantidadeSaldoEQuantidadeMovimentacaoAtual;

		private decimal _diferencaQuantidadeEstoqueQuantidadeMovimentacaoAtual;

		private IEnumerable<PessoaDto> _tecnicos;

		private IEnumerable<PessoaDto> _vendedores;

		private readonly IEnumerable<ContaDto> _contas;

		private readonly IEnumerable<CentroCustoDto> _centroCustos;

		private readonly IEnumerable<PlanoContaDto> _planoContas;

		private readonly EspeciePagamentoDto _especiePagamento;

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

		public TipoEstoqueBaseViewModel TipoEstoque
		{
			get
			{
				return ((BindableBase)this).GetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque));
			}
			set
			{
				((BindableBase)this).SetProperty<TipoEstoqueBaseViewModel>((Expression<Func<TipoEstoqueBaseViewModel>>)(() => TipoEstoque), value, (Action)delegate
				{
					PodeInformarQuantidade = TipoEstoque != null;
				});
			}
		}

		public bool PodeInformarQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeInformarQuantidade), value);
			}
		}

		public bool PodeFocoQuantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeFocoQuantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeFocoQuantidade), value, (Action)MudarFoco);
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

		public string QuantidadeEmHorasDisplay
		{
			get
			{
				int num = (int)Quantidade;
				int num2 = (int)Math.Round((Quantidade - (decimal)(int)Quantidade) * 100m) * 60;
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

		public decimal Quantidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Quantidade), value, (Action)ValidarQuantidade);
			}
		}

		public decimal Preco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Preco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Preco), value);
			}
		}

		public bool PodeAlterarPreco
		{
			get
			{
				if (HelperObject.IsExactlyObject<ProdutoServicoEmpresaDto>((object)ProdutoServicoEmpresa) && DadosSistema.PermitirDesconto && ProdutoServicoEmpresa.LimiteDesconto == 0m)
				{
					if (((MovimentacaoBaseDto)Movimentacao).Dav)
					{
						return !DadosSistema.IsPafEcf;
					}
					return true;
				}
				return false;
			}
		}

		public ProdutoServicoEmpresaDto ProdutoServicoEmpresa { get; protected set; }

		public bool EstoqueNegativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => EstoqueNegativo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => EstoqueNegativo), value);
			}
		}

		private AutorizacaoDto Autorizacao { get; set; }

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

		public MovimentacaoDto Movimentacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
			}
			set
			{
				((BindableBase)this).SetProperty<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao), value);
			}
		}

		public bool MostrarObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarObjeto), value);
			}
		}

		public string TecnicoVendedor
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TecnicoVendedor));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TecnicoVendedor), value);
			}
		}

		public Lista<MovimentacaoObjetoDto> Objetos { get; set; } = new Lista<MovimentacaoObjetoDto>();


		public Lista<PessoaDto> TecnicosVendedores { get; set; } = new Lista<PessoaDto>();


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

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public ICommand RemoverProdutoServicoCommand { get; set; }

		public ICommand AdicionarItemCommand { get; }

		public ICommand RemoverItemCommand { get; }

		public ICommand MudarFocoQuantidadeCommand { get; }

		public ICommand F7Command { get; }

		public PainelDireitoProdutosServicosOrdemServico(Seletor<IPainel> seletorDireito, Action<object> navegar, Action<ISelecaoCommandParameter, Action> navegarEstoque, IWService service, IAutenticacaoService autenticacaoService, Seletor<IPainel> seletorEsquerdo, IEnumerable<ContaDto> contas, IEnumerable<CentroCustoDto> centroCustos, IEnumerable<PlanoContaDto> planoContas, EspeciePagamentoDto especie)
		{
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Expected O, but got Unknown
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Expected O, but got Unknown
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Expected O, but got Unknown
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_navegar = navegar;
			_navegarEstoque = navegarEstoque;
			_service = service;
			_autenticacaoService = autenticacaoService;
			_seletorEsquerdo = seletorEsquerdo;
			_contas = contas;
			_centroCustos = centroCustos;
			_planoContas = planoContas;
			_especiePagamento = especie;
			PesquisarProdutoServicoCommand = (ICommand)new DelegateCommand((Action)Pesquisar, (Func<bool>)(() => true), (bool?)null);
			RemoverProdutoServicoCommand = (ICommand)new DelegateCommand((Action)InicializarPropriedades);
			AdicionarItemCommand = (ICommand)new DelegateCommand((Action)AdicionarItem, (Func<bool>)PodeAdicionarItem, (bool?)null);
			RemoverItemCommand = (ICommand)new DelegateCommand<object>((Action<object>)RemoverItem, (Func<object, bool>)PodeRemoverItem, (bool?)null);
			MudarFocoQuantidadeCommand = (ICommand)new DelegateCommand((Action)MudarFocoQuantidade);
			F7Command = (ICommand)new DelegateCommand((Action)Gravar, (Func<bool>)(() => true), (bool?)null);
		}

		private void Pesquisar()
		{
			try
			{
				ProdutoServicoEmpresaDto val = _service.Canal<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>((Func<IProdutoServicoModuleService, ProdutoServicoEmpresaDto>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorIdentificador(InformacaoPesquisa)));
				if (HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)val))
				{
					if (ValidarProdutoServicoSelecionado(InformacaoPesquisa).Execute(val))
					{
						CriarItem(val);
					}
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

		private void InicializarPropriedades()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			InformacaoPesquisa = string.Empty;
			ProdutoServicoEmpresa = (ProdutoServicoEmpresaDto)new ProdutoServicoEmpresaDtoNull();
			Quantidade = 0m;
			PodeFocoQuantidade = false;
			EstoqueNegativo = false;
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				((ViewModelValidationBase)tipoEstoque).Dispose();
			}
			TipoEstoque = null;
			MostrarQuantidadeEmHora = false;
			IniciarListaTecnicosVendedores();
			TecnicoVendedor = "Técnico";
			Observacao = string.Empty;
			Preco = 0m;
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
		}

		private bool PodeAdicionarItem()
		{
			if (HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)ProdutoServicoEmpresa))
			{
				return Quantidade > 0m;
			}
			return false;
		}

		private void AdicionarItem()
		{
			try
			{
				ItemDto itemDto = CriarItemDto();
				if (DadosSistema.ConfiguracaoDto.Saida.AlertarItemDuplicado.Valor && Movimentacao.Itens.Any((ItemDto x) => x.ProdutoServicoEmpresaReferencia == itemDto.ProdutoServicoEmpresaReferencia) && DialogoFactory.MostrarDialogoSimNao("Este item já foi lançado. Deseja adicioná-lo novamente à lista?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					InicializarPropriedades();
					return;
				}
				ItemDto result = _service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService x) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)x).CriarItemMovimentacao(itemDto))).Result;
				result.Numero = Movimentacao.Itens.Count() + 1;
				Movimentacao.Itens = new List<ItemDto>(Movimentacao.Itens) { result }.OrderBy((ItemDto a) => a.Numero);
				((MovimentacaoBaseDto)Movimentacao).PagamentosRecebimentos = new List<PagamentoRecebimentoDto> { (PagamentoRecebimentoDto)(object)ObterDtoCriarRecebimento() };
				SalvarDav(result);
				((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
				InicializarPropriedades();
				MudarFoco();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private RecebimentoDto ObterDtoCriarRecebimento()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Expected O, but got Unknown
			return new RecebimentoDto
			{
				Conta = (ObterContaPadraoCliente() ?? _contas.FirstOrDefault()),
				EspeciePagamento = (ObterEspeciePagamentoPadraoCliente() ?? _especiePagamento),
				CentroCusto = (Movimentacao.CentroCustoPadrao ?? _centroCustos.FirstOrDefault()),
				PlanoConta = (Movimentacao.PlanoContaPadrao ?? _planoContas.FirstOrDefault()),
				Valor = Movimentacao.Itens.Where((ItemDto x) => !x.Cancelado).Sum((ItemDto x) => x.TotalLiquido),
				DataVencimento = DateTime.Now,
				Ordem = 1,
				Pessoa = ((MovimentacaoBaseDto)Movimentacao).Pessoa,
				Empresa = Movimentacao.Empresa,
				Juro = DadosSistema.ConfiguracaoDto.Financeiro.PercentualJuro.Valor,
				Multa = DadosSistema.ConfiguracaoDto.Financeiro.PercentualMulta.Valor
			};
		}

		private ContaDto ObterContaPadraoCliente()
		{
			string contaReferencia = ((MovimentacaoBaseDto)Movimentacao).Pessoa.Cliente.ContaReferencia;
			if (!string.IsNullOrEmpty(contaReferencia))
			{
				return _contas.FirstOrDefault((ContaDto x) => ((EntityBaseDto)x).Id.Equals(contaReferencia));
			}
			return null;
		}

		private EspeciePagamentoDto ObterEspeciePagamentoPadraoCliente()
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Expected O, but got Unknown
			//IL_0096: Expected O, but got Unknown
			int codigo = ((MovimentacaoBaseDto)Movimentacao).Pessoa.Cliente.CodigoEspeciePagamento;
			if (codigo <= 0)
			{
				return null;
			}
			EspeciePagamento val = ValueObjectService<EspeciePagamento, EspeciePagamentoValueObjectRepository>.ObterTodos().FirstOrDefault((EspeciePagamento x) => x.Codigo == codigo);
			if (!HelperObject.IsNull((object)val))
			{
				return new EspeciePagamentoDto
				{
					Codigo = val.Codigo,
					Descricao = val.Descricao,
					EspecieRecebimento = new EspecieRecebimentoDto
					{
						Codigo = val.EspecieRecebimento.Codigo,
						Descricao = val.EspecieRecebimento.Descricao
					}
				};
			}
			return null;
		}

		private ItemDto CriarItemDto()
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Expected O, but got Unknown
			List<AutorizacaoDto> list = new List<AutorizacaoDto>();
			if (HelperObject.IsNotNull((object)Autorizacao))
			{
				list.Add(Autorizacao);
			}
			return new ItemDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)ProdutoServicoEmpresa).Id,
				PrecoUnitarioInformado = Preco,
				QuantidadeInformada = Quantidade,
				Estoque = (EstoqueDto)(HelperObject.IsNotNull((object)TipoEstoque) ? ((object)((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto) : ((object)new EstoqueDto
				{
					Id = ((EntityBaseDto)ProdutoServicoEmpresa.ProdutoEmpresa.Estoque).Id,
					Quantidade = Quantidade
				})),
				VendedorResponsavel = ObterTecnicoVendedor(),
				TecnicoResponsavel = ObterTecnicoVendedor(),
				MovimentacaoObjeto = (MovimentacaoObjetoDto)(((object)Objetos.ItemSelecionado) ?? ((object)new MovimentacaoObjetoDtoNull())),
				Autorizacoes = list,
				ObservacaoDigitada = Observacao,
				CondicaoEspecial = ProdutoServicoEmpresa.CondicaoEspecial,
				LimiteDesconto = ProdutoServicoEmpresa.LimiteDesconto,
				CalculadoPeloSistema = true,
				TabelaPreco = Movimentacao.TabelaPreco,
				PessoaMovimentacao = ((MovimentacaoBaseDto)Movimentacao).Pessoa,
				EmpresaMovimentacao = Movimentacao.Empresa,
				IndicadorOperacaoMovimentacao = Movimentacao.IndicadorOperacao,
				IndicadorPresencaCompradorMovimentacao = Movimentacao.IndicadorPresencaComprador,
				NaturezaOperacaoMovimentacao = ((MovimentacaoBaseDto)Movimentacao).NaturezaOperacao,
				TipoMovimentacao = ((MovimentacaoBaseDto)Movimentacao).Tipo,
				QuantidadeVendidaMovimentacao = Movimentacao.Itens.Where((ItemDto x) => x.ProdutoServicoEmpresaReferencia.Equals(((EntityBaseDto)ProdutoServicoEmpresa).Id)).Sum((ItemDto x) => x.QuantidadeInformada),
				DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia
			};
		}

		private async void SalvarDav(ItemDto itemDto)
		{
			if (!((MovimentacaoBaseDto)Movimentacao).Dav)
			{
				return;
			}
			MovimentacaoDto movimentacaoDto = (MovimentacaoDto)((((MovimentacaoBaseDto)Movimentacao).Numero == 0L) ? ((object)Movimentacao) : ((object)new MovimentacaoDto
			{
				Numero = ((MovimentacaoBaseDto)Movimentacao).Numero,
				Versao = ((EntityVersaoDto)Movimentacao).Versao,
				Id = ((EntityBaseDto)Movimentacao).Id,
				Itens = new List<ItemDto> { itemDto },
				PagamentosRecebimentos = ((MovimentacaoBaseDto)Movimentacao).PagamentosRecebimentos
			}));
			await _service.Canal<IMovimentacaoSaidaModuleService, Task<InformacaoMovimentacaoDto>>((Func<IMovimentacaoSaidaModuleService, Task<InformacaoMovimentacaoDto>>)((IMovimentacaoSaidaModuleService service) => service.SalvarDavOrdemServico(movimentacaoDto))).ContinueWith(delegate(Task<InformacaoMovimentacaoDto> task)
			{
				if (task.IsFaulted)
				{
					ThreadPrincipal.Executar((Action)delegate
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					});
				}
				else
				{
					InformacaoMovimentacaoDto result = task.Result;
					((MovimentacaoBaseDto)Movimentacao).Numero = result.Numero;
					((EntityVersaoDto)Movimentacao).Versao = result.Versao;
					((EntityBaseDto)Movimentacao).Id = result.Id;
				}
			});
		}

		private PessoaDto ObterTecnicoVendedor()
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			if (!TecnicosVendedores.PossuiItemSelecionado)
			{
				return (PessoaDto)new PessoaDtoNull();
			}
			if (!((EntityBaseDto)TecnicosVendedores.ItemSelecionado).Id.Equals("Nenhum"))
			{
				return TecnicosVendedores.ItemSelecionado;
			}
			return (PessoaDto)new PessoaDtoNull();
		}

		private bool PodeRemoverItem(object item)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			if (item is ItemDto)
			{
				return !((ItemDto)item).Cancelado;
			}
			return false;
		}

		private void RemoverItem(object item)
		{
			LoaderProcessoService.MostrarLoader((DadosSistema.IsPaf || DadosSistema.ManterItemRemovidoDav) ? "Cancelando item" : "Excluindo item");
			try
			{
				if (DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirCancelarItemDocumentoFiscal>(0m, (string)null) == null)
				{
					return;
				}
				ItemDto itemDto = (ItemDto)((item is ItemDto) ? item : null);
				if (DadosSistema.IsPaf || DadosSistema.ManterItemRemovidoDav)
				{
					itemDto.Cancelado = true;
				}
				else
				{
					IEnumerable<ItemDto> enumerable = Movimentacao.Itens.Where((ItemDto x) => x.Numero != itemDto.Numero);
					Movimentacao.Itens = HelperIEnumerable.ToListEmptyWhenNull<ItemDto>(enumerable);
				}
				((MovimentacaoBaseDto)Movimentacao).PagamentosRecebimentos = new List<PagamentoRecebimentoDto> { (PagamentoRecebimentoDto)(object)ObterDtoCriarRecebimento() };
				MovimentacaoDto result = _service.Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<MovimentacaoDto>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => ((ICrudMovimentacaoService<MovimentacaoDto>)(object)x).AtualizarAsync(Movimentacao))).Result;
				((EntityVersaoDto)Movimentacao).Versao = ((EntityVersaoDto)result).Versao;
				((MovimentacaoBaseDto)Movimentacao).CalculadoPeloSistema = true;
				((BindableBase)this).RaisePropertyChanged<MovimentacaoDto>((Expression<Func<MovimentacaoDto>>)(() => Movimentacao));
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

		private void Gravar()
		{
			_seletorEsquerdo.Get<PainelEsquerdoOrdemServico>().AtualizarOs(Movimentacao);
			_seletorDireito.Get<PainelDireitoOrdemServico>().SetarFocoAdicionarDavOs(valor: true);
			_seletorDireito.Voltar();
		}

		public ValidateParameter<ProdutoServicoEmpresaDto> ValidarProdutoServicoSelecionado(string informacaoPesquisa)
		{
			return new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => ((EntityBaseDto)x.ProdutoServico).Id.Equals(informacaoPesquisa) || (x.Vendavel && HelperObject.IsExactlyObject<ProdutoDto>((object)x.ProdutoServico.Produto) && (int)x.ProdutoServico.Tipo != 5) || HelperObject.IsExactlyObject<ServicoDto>((object)x.ProdutoServico.Servico)), "Produtos do tipo 'Combustível' só podem ser vendidos a partir da importação de abastecimentos.", System.Windows.HorizontalAlignment.Center);
		}

		private void NavegarProdutoServicoListaView()
		{
			SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto> val = new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)CriarItem, ValidarProdutoServicoSelecionado(InformacaoPesquisa));
			((SelecaoCommandParameter<ProdutoServicoEmpresaDto>)(object)val).Parametros.Add("InformacaoPesquisa", (object)InformacaoPesquisa);
			_navegar(val);
		}

		private void CriarItem(ProdutoServicoEmpresaDto produtoServicoEmpresaDto)
		{
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Expected O, but got Unknown
			Autorizacao = null;
			decimal num = Movimentacao.Itens.Where((ItemDto x) => x.ProdutoServicoEmpresaReferencia.Equals(((EntityBaseDto)produtoServicoEmpresaDto).Id)).Sum((ItemDto x) => x.Quantidade);
			_diferencaQuantidadeSaldoEQuantidadeMovimentacaoAtual = produtoServicoEmpresaDto.ProdutoEmpresa.Estoque.QuantidadeSaldo - num;
			_diferencaQuantidadeEstoqueQuantidadeMovimentacaoAtual = produtoServicoEmpresaDto.ProdutoEmpresa.Estoque.Quantidade - num;
			ProdutoServicoEmpresa = produtoServicoEmpresaDto;
			MostrarQuantidadeEmHora = produtoServicoEmpresaDto.ProdutoServico.UnidadeMedida.IsHora;
			ThreadPrincipal.Executar((Action)PreencherListaTecnicosVendedores);
			if (!ValidarQuantidadeCallbackProdutoServico())
			{
				InicializarPropriedades();
				return;
			}
			if (HelperObject.Is<ServicoEmpresaDtoNull>((object)ProdutoServicoEmpresa.ServicoEmpresa) && !produtoServicoEmpresaDto.ProdutoEmpresa.Estoque.IsNormal)
			{
				TipoEstoque = ((TipoEstoqueViewModel<SerialSaidaViewModel, GradePrincipalSaidaViewModel, LoteSaidaViewModel>)new TipoEstoqueSaidaViewModel((EstoqueDto)null, ProdutoServicoEmpresa.ProdutoEmpresa.Estoque)).ObterTipoEstoque().ViewModelBase;
				((ViewModelBase<EstoqueDto>)(object)TipoEstoque).ProjectedAsViewModel();
				TipoEstoque.ActionSetarQuantidade((Action)SetarQuantidade);
				if (!PesquisarGrade())
				{
					_navegarEstoque(TipoEstoque.EstoqueCommandParameter, SetarQuantidade);
				}
			}
			else
			{
				TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
				if (tipoEstoque != null)
				{
					((ViewModelValidationBase)tipoEstoque).Dispose();
				}
				TipoEstoque = null;
				PodeInformarQuantidade = false;
			}
			Preco = (HelperObject.Is<TabelaPrecoDtoNull>((object)Movimentacao.TabelaPreco) ? produtoServicoEmpresaDto.Preco.PrecoVendaVigente : produtoServicoEmpresaDto.Preco.ItensTabelaPreco.FirstOrDefault((ItemTabelaPrecoProdutoServicoDto x) => x.TabelaPrecoReferencia == ((EntityBaseDto)Movimentacao.TabelaPreco).Id).Preco);
			((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => PodeAlterarPreco));
			InformacaoPesquisa = ((BaseDto)produtoServicoEmpresaDto).ToText;
			PodeFocoQuantidade = true;
		}

		protected void SetarQuantidade()
		{
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			Quantidade = ((tipoEstoque != null) ? tipoEstoque.Quantidade : 0m);
		}

		private bool PesquisarGrade()
		{
			string informacaoPesquisa = HelperString.RemoveDiacritics(InformacaoPesquisa).ToLower();
			EstoqueDto estoque = ProdutoServicoEmpresa.ProdutoEmpresa.Estoque;
			if (!PesquisarEstoqueGrade((EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null), informacaoPesquisa))
			{
				EstoqueDto estoque2 = ProdutoServicoEmpresa.ProdutoEmpresa.Estoque;
				return PesquisarEstoqueLoteGrade((EstoqueLoteDto)(object)((estoque2 is EstoqueLoteDto) ? estoque2 : null), informacaoPesquisa);
			}
			return true;
		}

		private bool PesquisarEstoqueGrade(EstoqueGradeDto grade, string informacaoPesquisa)
		{
			if (HelperObject.IsNull((object)grade))
			{
				return false;
			}
			GradeDto val = grade.Grades.FirstOrDefault((GradeDto a) => a.CodigoBarras.ToLower().Equals(informacaoPesquisa));
			if (!HelperObject.IsNotNull((object)val) || !val.Estoque.IsNormal)
			{
				return false;
			}
			val.Estoque.Quantidade = 1m;
			HelperObject.To<EstoqueGradeDto>((object)((ViewModelDtoBase<EstoqueDto>)(object)TipoEstoque).Dto).Grades = new List<GradeDto> { val };
			return true;
		}

		private bool PesquisarEstoqueLoteGrade(EstoqueLoteDto lote, string informacaoPesquisa)
		{
			if (HelperObject.IsNull((object)lote))
			{
				return false;
			}
			return lote.Lotes.Where((LoteDto a) => HelperObject.Is<EstoqueGradeDto>((object)a.Estoque)).Select(delegate(LoteDto x)
			{
				PainelDireitoProdutosServicosOrdemServico painelDireitoProdutosServicosOrdemServico = this;
				EstoqueDto estoque = x.Estoque;
				return painelDireitoProdutosServicosOrdemServico.PesquisarEstoqueGrade((EstoqueGradeDto)(object)((estoque is EstoqueGradeDto) ? estoque : null), informacaoPesquisa);
			}).Any((bool a) => a);
		}

		protected bool ValidarQuantidadeCallbackProdutoServico()
		{
			Quantidade = 1m;
			if (EstoqueNegativo)
			{
				return HelperObject.IsNotNull((object)Autorizacao);
			}
			return true;
		}

		private void MudarFoco()
		{
			if (PodeFocoQuantidade)
			{
				MudarFocoQuantidade();
				return;
			}
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoDescricaoItem = !FocoDescricaoItem;
			});
		}

		private void MudarFocoQuantidade()
		{
			((UpdateViewBindableBase)this).DormirAsync(100, (Action<object>)delegate
			{
				FocoQuantidade = !FocoQuantidade;
			});
		}

		protected void ValidarQuantidade()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Invalid comparison between Unknown and I4
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			if ((int)ProdutoServicoEmpresa.ProdutoServico.Tipo == 2)
			{
				return;
			}
			ValidarQuantidadeMinima();
			if (DadosSistema.RetornarPermitirEstoqueNegativoPorTipoMovimentacao(((MovimentacaoBaseDto)Movimentacao).Tipo) || Quantidade == 0m || Quantidade <= _diferencaQuantidadeSaldoEQuantidadeMovimentacaoAtual)
			{
				return;
			}
			if (Quantidade <= _diferencaQuantidadeEstoqueQuantidadeMovimentacaoAtual)
			{
				if (DialogoFactory.MostrarDialogoSimNao(MessagemQuantidadeSaldoNegativa(), false, 0, System.Windows.HorizontalAlignment.Center).Equals(MessageBoxResult.Yes))
				{
					Autorizacao = _autenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderEstoqueSaldoNegativo>(0m, (string)null);
					if (HelperObject.IsNotNull((object)Autorizacao))
					{
						return;
					}
				}
			}
			else
			{
				DialogoFactory.MostrarDialogoConfirma($"{ProdutoServicoEmpresa.ProdutoServico.Descricao} atingiu estoque negativo!", false, System.Windows.HorizontalAlignment.Center);
			}
			Quantidade = 0m;
			EstoqueNegativo = true;
		}

		private void ValidarQuantidadeMinima()
		{
			if (!(ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima == 0m) && DadosSistema.ConfiguracaoDto.Saida.AlertarQuantidadeMinimaMaximaAtingida.Valor && ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.Quantidade - Quantidade <= ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima)
			{
				DialogoFactory.MostrarDialogoConfirma($"Quantidade mínima em estoque atingida. Quantidade mínima: {ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.QuantidadeMinima}.", false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private string MessagemQuantidadeSaldoNegativa()
		{
			return $"{ProdutoServicoEmpresa.ProdutoServico.Descricao} atingiu quantidade saldo negativa! " + $"Porém, a quantidade em estoque é de {ProdutoServicoEmpresa.ProdutoEmpresa.Estoque.Quantidade}. " + "Continuar?";
		}

		public void Preencher(MovimentacaoDto movimentacaoDto)
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Expected O, but got Unknown
			Movimentacao = movimentacaoDto;
			Objetos.Limpar();
			Objetos.Adicionar(Movimentacao.Objetos);
			MostrarObjeto = Objetos.Numero > 1;
			PesquisaDto objetoPesquisaTecnicos = new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
				{
					TipoPessoaCadastro = (TipoPessoaCadastro)5
				},
				Pagina = 1,
				Tamanho = 100
			};
			PesquisaDto objetoPesquisaVendedores = new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
				{
					TipoPessoaCadastro = (TipoPessoaCadastro)6
				},
				Pagina = 1,
				Tamanho = 100
			};
			_tecnicos = _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService x) => ((IPesquisaAtivosInativosCustom)x).PesquisarAtivosAsync(objetoPesquisaTecnicos))).Result.Lista.Cast<PessoaDto>();
			_vendedores = _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService x) => ((IPesquisaAtivosInativosCustom)x).PesquisarAtivosAsync(objetoPesquisaVendedores))).Result.Lista.Cast<PessoaDto>();
			InicializarPropriedades();
		}

		private void IniciarListaTecnicosVendedores()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			TecnicosVendedores.Limpar();
			TecnicosVendedores.Adicionar(new PessoaDto
			{
				Nome = "Nenhum",
				Id = "Nenhum"
			});
		}

		private void PreencherListaTecnicosVendedores()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Invalid comparison between Unknown and I4
			IniciarListaTecnicosVendedores();
			if ((int)ProdutoServicoEmpresa.ProdutoServico.Tipo == 2)
			{
				TecnicosVendedores.Adicionar(_tecnicos);
				if (HelperObject.IsNot<PessoaDtoNull>((object)Movimentacao.TecnicoResponsavel))
				{
					TecnicosVendedores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)Movimentacao.TecnicoResponsavel).Id)));
				}
				else
				{
					TecnicosVendedores.PosicionarNoPrimeiro();
				}
				return;
			}
			TecnicosVendedores.Adicionar(_vendedores);
			if (HelperObject.IsNot<PessoaDtoNull>((object)Movimentacao.VendedorResponsavel))
			{
				TecnicosVendedores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)Movimentacao.VendedorResponsavel).Id)));
			}
			else
			{
				TecnicosVendedores.PosicionarNoPrimeiro();
			}
			TecnicoVendedor = "Vendedor";
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
			TipoEstoqueBaseViewModel tipoEstoque = TipoEstoque;
			if (tipoEstoque != null)
			{
				((ViewModelValidationBase)tipoEstoque).Dispose();
			}
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelEsquerdoOrdemServico : UpdateViewBindableBase, IPainel
	{
		private readonly IWService _service;

		private readonly Seletor<IPainel> _seletorDireito;

		private readonly IDisposable _ordemServicoDisposable;

		public bool FocoNaBusca
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBusca));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNaBusca), value);
			}
		}

		public string TextoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TextoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TextoPesquisa), value);
			}
		}

		public string TituloPesquisa
		{
			get
			{
				if (!DadosSistema.IsPet)
				{
					return "[Ctrl+F] Número, placa/serial ou campo destaque";
				}
				return "[Ctrl+F] Número, nome do pet ou campo destaque";
			}
		}

		public Lista<OrdemServicoItemViewModel> OrdensServico { get; set; } = new Lista<OrdemServicoItemViewModel>();


		public Lista<TipoStatusDocumentoAuxiliarOrdemServico> Status
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>((Expression<Func<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>>)(() => Status));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>((Expression<Func<Lista<TipoStatusDocumentoAuxiliarOrdemServico>>>)(() => Status), value);
			}
		}

		public Lista<SituacaoMovimentacao> OpcoesSituacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<SituacaoMovimentacao>>((Expression<Func<Lista<SituacaoMovimentacao>>>)(() => OpcoesSituacao));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<SituacaoMovimentacao>>((Expression<Func<Lista<SituacaoMovimentacao>>>)(() => OpcoesSituacao), value);
			}
		}

		public IReadOnlyCollection<SituacaoMovimentacao> Situacoes => new List<SituacaoMovimentacao>
		{
			(SituacaoMovimentacao)new Aguardando(),
			(SituacaoMovimentacao)new Aprovado(),
			(SituacaoMovimentacao)new Atrasado(),
			(SituacaoMovimentacao)new Autorizado(),
			(SituacaoMovimentacao)new Cancelado(),
			(SituacaoMovimentacao)new Concluido(),
			(SituacaoMovimentacao)new Contingencia(),
			(SituacaoMovimentacao)new Denegado(),
			(SituacaoMovimentacao)new Entregue(),
			(SituacaoMovimentacao)new Inutilizado(),
			(SituacaoMovimentacao)new Pendente(),
			(SituacaoMovimentacao)new Pronto(),
			(SituacaoMovimentacao)new Rejeitado(),
			(SituacaoMovimentacao)new Transmitido(),
			(SituacaoMovimentacao)new TransformadoEmDavDavOs(),
			(SituacaoMovimentacao)new Todos()
		};

		public short NumeroRegistrosPorPagina => (short)((DadosSistema.ConfiguracaoDto.Saida.RegistrosPorPaginaOrdemServico.Valor == 1) ? 24 : 48);

		public int ItemWidth
		{
			get
			{
				if (NumeroRegistrosPorPagina != 24)
				{
					return 120;
				}
				return 195;
			}
		}

		public int ItemHeight
		{
			get
			{
				if (NumeroRegistrosPorPagina != 24)
				{
					return 140;
				}
				return 200;
			}
		}

		public long TotalRegistros => OrdensServico.Numero;

		public short Pagina { get; protected set; } = 1;


		public bool Habilitado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Habilitado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Habilitado), value);
			}
		}

		public bool FechouModulo
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FechouModulo));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FechouModulo), value);
			}
		}

		public ICommand PesquisarCommand { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public PainelEsquerdoOrdemServico(IWService service, Seletor<IPainel> seletorDireito)
		{
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0164: Expected O, but got Unknown
			_service = service;
			_seletorDireito = seletorDireito;
			_ordemServicoDisposable = ObservableExtensions.Subscribe<OrdemServicoItemViewModel>(OrdensServico.WhenAlterouItemSelecionado, (Action<OrdemServicoItemViewModel>)delegate(OrdemServicoItemViewModel x)
			{
				seletorDireito.Get<PainelDireitoOrdemServico>().Preencher(x.MovimentacaoDto);
			});
			Status = new Lista<TipoStatusDocumentoAuxiliarOrdemServico>();
			Status.Adicionar((IEnumerable<TipoStatusDocumentoAuxiliarOrdemServico>)CarregadorStatusDavOs.ObterOpcoesAtivas(service));
			Status.Adicionar(StatusDocumentosAuxiliaresOrdensServicos.Todos);
			Status.PosicionarNoUltimo();
			OpcoesSituacao = new Lista<SituacaoMovimentacao>();
			OpcoesSituacao.Adicionar((IEnumerable<SituacaoMovimentacao>)Situacoes);
			OpcoesSituacao.Posicionar((Expression<Func<SituacaoMovimentacao, bool>>)((SituacaoMovimentacao x) => ((CodigoDescricao)x).Codigo.Equals(19)));
			PesquisarCommand = (ICommand)new AsyncCommand((Func<Task>)Pesquisar, (Func<bool>)(() => true), false, (bool?)null);
			ScrollChangedCommand = (ICommand)new DelegateCommand<object>((Action<object>)ScrollChanged, true);
		}

		public async Task Pesquisar()
		{
			try
			{
				Pagina = 1;
				OrdensServico.Limpar();
				await Atualizar();
			}
			finally
			{
				TextoPesquisa = string.Empty;
				FocoNaBusca = !FocoNaBusca;
			}
		}

		public async void ScrollChanged(object obj)
		{
			if (obj is ScrollChangedEventArgs { VerticalChange: >0.0 } scrollChangedEventArgs && !(Math.Abs(scrollChangedEventArgs.VerticalOffset + scrollChangedEventArgs.ViewportHeight - scrollChangedEventArgs.ExtentHeight) > 1.0) && TotalRegistros >= NumeroRegistrosPorPagina * Pagina)
			{
				Pagina++;
				await Atualizar();
			}
		}

		public void Habilitar<T>(Seletor<IPainel> seletorDireito)
		{
			Habilitado = HelperObject.Is<T>((object)seletorDireito.Painel);
		}

		public async void Load()
		{
			await Atualizar();
		}

		public async Task Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			try
			{
				PesquisaDto pesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaMovimentacaoDto
					{
						Informacao = HelperString.ToStringEmptyWhenNull(TextoPesquisa),
						StatusDavOs = new CodigoDescricaoDto
						{
							Codigo = Status.ItemSelecionado.Codigo,
							Descricao = Status.ItemSelecionado.Descricao
						},
						Situacao = new CodigoDescricaoDto
						{
							Codigo = ((CodigoDescricao)OpcoesSituacao.ItemSelecionado).Codigo,
							Descricao = ((CodigoDescricao)OpcoesSituacao.ItemSelecionado).Descricao
						}
					},
					Pagina = Pagina,
					Tamanho = NumeroRegistrosPorPagina
				};
				await _service.Canal<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<MovimentacaoDto>>>((Func<IDocumentoAuxiliarOrdemServicoModuleService, Task<IEnumerable<MovimentacaoDto>>>)((IDocumentoAuxiliarOrdemServicoModuleService x) => x.ObterOrdemServico(pesquisa))).ContinueWith(delegate(Task<IEnumerable<MovimentacaoDto>> task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
					else
					{
						ThreadPrincipal.Executar((Action)delegate
						{
							//IL_001e: Unknown result type (might be due to invalid IL or missing references)
							//IL_0028: Expected O, but got Unknown
							if (!FechouModulo)
							{
								_seletorDireito.Get<PainelDireitoOrdemServico>().Preencher((MovimentacaoDto)new MovimentacaoDtoNull());
								OrdensServico.AdicionarSemPosicionar(task.Result.Select((MovimentacaoDto x) => new OrdemServicoItemViewModel(x)));
								OrdensServico.PosicionarNoPrimeiro();
							}
						});
					}
				});
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

		public void AdicionarOs(MovimentacaoDto movimentacaoDto)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				OrdensServico.Inserir(0, new OrdemServicoItemViewModel(movimentacaoDto));
				OrdensServico.Posicionar((Expression<Func<OrdemServicoItemViewModel, bool>>)((OrdemServicoItemViewModel x) => x.Numero.Equals(((MovimentacaoBaseDto)movimentacaoDto).Numero)));
			});
		}

		public void AtualizarOs(MovimentacaoDto movimentacaoDto)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				OrdensServico.Substituir((Expression<Func<OrdemServicoItemViewModel, bool>>)((OrdemServicoItemViewModel x) => x.Numero.Equals(((MovimentacaoBaseDto)movimentacaoDto).Numero)), new OrdemServicoItemViewModel(movimentacaoDto));
				OrdensServico.Posicionar((Expression<Func<OrdemServicoItemViewModel, bool>>)((OrdemServicoItemViewModel x) => x.Numero.Equals(((MovimentacaoBaseDto)movimentacaoDto).Numero)));
			});
		}

		public void UnLoad()
		{
		}

		public void FecharMethod()
		{
			_ordemServicoDisposable.Dispose();
			((ViewModelValidationBase)OrdensServico).Dispose();
			FechouModulo = true;
		}

		public bool PodeVoltar()
		{
			return false;
		}

		public void SetarFocoNaBusca()
		{
			FocoNaBusca = !FocoNaBusca;
		}
	}
}
namespace Otus.Modulo.OrdensServico.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.OrdensServico.Properties.Resources", typeof(Resources).Assembly);
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

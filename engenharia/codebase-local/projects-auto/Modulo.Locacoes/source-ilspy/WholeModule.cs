using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraScheduler;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Cadastros.Hospedagens;
using DigisatServer.Application.Main.Dto.Cadastros.Locacoes;
using DigisatServer.Application.Main.Dto.Cadastros.Objetos;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.ProdutosServicos;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Domain.Core;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Locacoes;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp.Classes;
using DigisatServer.Infrastructure.Crosscutting.Style.GridControls;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.Locacoes.Reports;
using Otus.Modulo.Locacoes.ViewModels.LocacoesFinalizadas;
using Otus.Modulo.Locacoes.ViewModels.MapaLocacoes;
using Otus.Modulo.Locacoes.ViewModels.Paineis;
using Otus.Modulo.Locacoes.Views;
using Otus.Modulo.Locacoes.Views.LocacoesFinalizadas;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Locacoes")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Locacoes")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("7a3dc0cf-c86b-4c58-87b4-69ed1cd2459a")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6.1", FrameworkDisplayName = ".NET Framework 4.6.1")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Locacoes
{
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("ObjetoModulo")]
	[Module(ModuleName = "LocacaoModulo", OnDemand = true)]
	public class LocacaoModulo : IModule
	{
		private readonly IUnityContainer _container;

		public LocacaoModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<LocacaoView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<LocacoesView>(_container, (string)null);
			UnityContainerExtensions.RegisterType<PainelEsquerdoMapaLocacoes>(_container, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<PainelDireitoLocacao>(_container, Array.Empty<InjectionMember>());
			UnityContainerExtensions.RegisterType<PainelFichaLocacao>(_container, Array.Empty<InjectionMember>());
			UnityExtensions.RegisterTypeForNavigation<LocacoesFinalizadasFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Locacoes.Views
{
	public class LocacaoView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LocacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Locacoes;component/views/locacaoview.xaml", UriKind.Relative);
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
	public class LocacoesView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LocacoesView()
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
				Uri resourceLocator = new Uri("/Modulo.Locacoes;component/views/locacoesview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Locacoes.Views.LocacoesFinalizadas
{
	public class LocacoesFinalizadasFichaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LocacoesFinalizadasFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Locacoes;component/views/locacoesfinalizadas/locacoesfinalizadasfichaview.xaml", UriKind.Relative);
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
	public class LocacoesFinalizadasView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal GridControlBestFit GridControl;

		private bool _contentLoaded;

		public LocacoesFinalizadasView()
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
				Uri resourceLocator = new Uri("/Modulo.Locacoes;component/views/locacoesfinalizadas/locacoesfinalizadasview.xaml", UriKind.Relative);
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
				GridControl = (GridControlBestFit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.Locacoes.ViewModels
{
	public class LocacaoViewModel : BaseMinimizar
	{
		public bool PodeAdicionarObjeto => DadosSistema.VerificarPermissao<Objeto, Adicionar>();

		public ISelecaoCommandParameter ObjetoAdicionarCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<ObjetoDto>("ObjetoFichaAdicionarView", (Action<ObjetoDto>)delegate
		{
		}, PodeAdicionarObjeto);

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

		public ICommand F3Command { get; set; }

		public ICommand F4Command { get; set; }

		public ICommand F5Command { get; set; }

		public ICommand F8Command { get; set; }

		public ICommand MostrarPainelDireitoCommand { get; set; }

		public ICommand VisualizarRelatorioCommand { get; set; }

		public LocacaoViewModel(IUnityContainer container)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Expected O, but got Unknown
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_005f: Expected O, but got Unknown
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Expected O, but got Unknown
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			//IL_00b8: Expected O, but got Unknown
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Expected O, but got Unknown
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Expected O, but got Unknown
			//IL_0111: Expected O, but got Unknown
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Expected O, but got Unknown
			//IL_0141: Expected O, but got Unknown
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Expected O, but got Unknown
			//IL_0171: Expected O, but got Unknown
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Expected O, but got Unknown
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Expected O, but got Unknown
			//IL_022a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0234: Expected O, but got Unknown
			//IL_0241: Unknown result type (might be due to invalid IL or missing references)
			//IL_024b: Expected O, but got Unknown
			SeletorEsquerdo = UnityContainerExtensions.Resolve<Seletor<IPainel>>(container, Array.Empty<ResolverOverride>());
			SeletorDireito = UnityContainerExtensions.Resolve<Seletor<IPainel>>(container, Array.Empty<ResolverOverride>());
			Seletor<IPainel> seletorEsquerdo = SeletorEsquerdo;
			ResolverOverride[] array = new ResolverOverride[1];
			ParameterOverrides val = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			array[0] = (ResolverOverride)val;
			seletorEsquerdo.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelEsquerdoMapaLocacoes>(container, (ResolverOverride[])(object)array));
			Seletor<IPainel> seletorDireito = SeletorDireito;
			ResolverOverride[] array2 = new ResolverOverride[1];
			ParameterOverrides val2 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val2).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val2).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val2).Add("navegarMovimentacao", (object)new Action<object>(((NavigationBase)this).Navegar));
			array2[0] = (ResolverOverride)val2;
			seletorDireito.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoLocacao>(container, (ResolverOverride[])(object)array2));
			Seletor<IPainel> seletorDireito2 = SeletorDireito;
			ResolverOverride[] array3 = new ResolverOverride[1];
			ParameterOverrides val3 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("seletorDireito", (object)SeletorDireito);
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("seletorEsquerdo", (object)SeletorEsquerdo);
			((OverrideCollection<ParameterOverride, string, object>)val3).Add("navegar", (object)new Action<string, Action<NavigationResult>, NavigationParameters>(((NavigationBase)this).Navegar));
			array3[0] = (ResolverOverride)val3;
			seletorDireito2.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelFichaLocacao>(container, (ResolverOverride[])(object)array3));
			Seletor<IPainel> seletorDireito3 = SeletorDireito;
			ResolverOverride[] array4 = new ResolverOverride[1];
			ParameterOverrides val4 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val4).Add("seletorDireito", (object)SeletorDireito);
			array4[0] = (ResolverOverride)val4;
			seletorDireito3.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoImprimir>(container, (ResolverOverride[])(object)array4));
			Seletor<IPainel> seletorDireito4 = SeletorDireito;
			ResolverOverride[] array5 = new ResolverOverride[1];
			ParameterOverrides val5 = new ParameterOverrides();
			((OverrideCollection<ParameterOverride, string, object>)val5).Add("seletorDireito", (object)SeletorDireito);
			array5[0] = (ResolverOverride)val5;
			seletorDireito4.Adicionar((IPainel)(object)UnityContainerExtensions.Resolve<PainelDireitoHistorico>(container, (ResolverOverride[])(object)array5));
			SeletorDireito.PainelChanged += SeletorDireitoOnPainelChanged;
			SeletorDireito.Selecionar<PainelDireitoLocacao>();
			SeletorEsquerdo.Selecionar<PainelEsquerdoMapaLocacoes>();
			EstadoDaAnimacao = "1";
			F5Command = (ICommand)new DelegateCommand((Action)AdicionarLocacao, (Func<bool>)PodeAdicionar, (bool?)null);
			F3Command = (ICommand)new DelegateCommand((Action)ObterLocacoesFinalizadas, (Func<bool>)(() => HelperObject.Is<PainelDireitoLocacao>((object)SeletorDireito.Painel)), (bool?)false);
			VisualizarRelatorioCommand = (ICommand)new DelegateCommand((Action)Visualizar, (Func<bool>)PodeVisualizar, (bool?)null);
			MostrarPainelDireitoCommand = (ICommand)new DelegateCommand((Action)MostrarPainelDireito);
		}

		private void ObterLocacoesFinalizadas()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			try
			{
				NavigationParameters val = new NavigationParameters();
				val.Add("LocacoesFinalizadasViewModel", (object)new LocacoesFinalizadasViewModel((IEnumerable<LocacaoDto>)new List<LocacaoDto>(), (Action<string, Action<NavigationResult>, NavigationParameters>)((NavigationBase)this).Navegar));
				NavigationParameters val2 = val;
				((NavigationBase)this).Navegar(typeof(LocacoesFinalizadasFichaView).Name, (Action<NavigationResult>)delegate
				{
				}, val2);
				((NavigationBase)this).NavigationService.Navigated += NavigationServiceOnNavigated;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		private void NavigationServiceOnNavigated(object sender, RegionNavigationEventArgs regionNavigationEventArgs)
		{
			((NavigationBase)this).NavigationService.Navigated -= NavigationServiceOnNavigated;
		}

		private void Visualizar()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			NavigationParameters val = new NavigationParameters();
			val.Add("ItemModuloCreator", (object)159);
			NavigationParameters val2 = val;
			((NavigationBase)this).Navegar("RelatorioFichaView", (Action<NavigationResult>)delegate
			{
			}, val2);
		}

		private bool PodeVisualizar()
		{
			return DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			});
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

		private void MostrarPainelDireito()
		{
			EstadoDaAnimacao = ((EstadoDaAnimacao == "0") ? "1" : "0");
		}

		private bool PodeAdicionar()
		{
			return true;
		}

		private void AdicionarLocacao()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Expected O, but got Unknown
			SeletorDireito.Get<PainelFichaLocacao>().ProjetarLocacao((LocacaoDto)new LocacaoDtoNull());
			SeletorDireito.Selecionar<PainelFichaLocacao>();
		}

		private void SeletorDireitoOnPainelChanged(object sender, IPainel e)
		{
			SeletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>()?.Habilitar<PainelDireitoLocacao>(SeletorDireito);
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((BaseMinimizar)this).OnParametroInformado(sender, navigationParameters);
			IResolucao resolucao = ((NavigationBase)this).Resolucao;
			if (resolucao != null)
			{
				resolucao.Update();
			}
		}

		public override void Dispose()
		{
			SeletorDireito.PainelChanged -= SeletorDireitoOnPainelChanged;
			((BaseMinimizar)this).Dispose();
		}
	}
	public class LocacoesViewModel : ModuloBase
	{
		public LocacoesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloLocacoesRegion, "LocacaoView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
}
namespace Otus.Modulo.Locacoes.ViewModels.Paineis
{
	public class PainelDireitoHistorico : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		public LocacaoDto Locacao { get; set; }

		public string Titulo { get; set; }

		public ICommand FecharCommand { get; set; }

		public PainelDireitoHistorico(Seletor<IPainel> seletorDireito)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
		}

		public void Preencher(LocacaoDto locacaoDto)
		{
			Locacao = locacaoDto;
			Titulo = "Histórico da locação";
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
	public class PainelDireitoImprimir : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly IWService _service;

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

		public DateTime DataVencimentoContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataVencimentoContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataVencimentoContrato), value);
			}
		}

		public string DadosContrato
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DadosContrato));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DadosContrato), value);
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

		public LocacaoDto Locacao { get; set; }

		public string Titulo { get; set; }

		public ICommand FecharCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public ICommand CompartilharImpressosCommand { get; set; }

		public PainelDireitoImprimir(Seletor<IPainel> seletorDireito)
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Expected O, but got Unknown
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			_seletorDireito = seletorDireito;
			_service = DadosSistema.GetService();
			ImprimirCommand = (ICommand)new DelegateCommand((Action)ImprimirContratoLocacao, !HelperString.IsNullOrEmpty(DadosContrato));
			CompartilharImpressosCommand = (ICommand)new AsyncCommand((Func<Task>)CompartilharImpresso, !HelperString.IsNullOrEmpty(DadosContrato));
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
		}

		public string ObterObservacaoPadrao(string referencia)
		{
			return _service.Canal<IObservacaoDadoAdicionalModuleService, ObservacaoDadoAdicionalDto>((Func<IObservacaoDadoAdicionalModuleService, ObservacaoDadoAdicionalDto>)((IObservacaoDadoAdicionalModuleService x) => x.ObterObservacao(referencia))).Conteudo;
		}

		private void ImprimirContratoLocacao()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoContratoLocacao(Locacao, DadosContrato)).Processar());
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

		private async Task CompartilharImpresso()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo...");
				ImpressoContratoLocacao impresso = new ImpressoContratoLocacao(Locacao, DadosContrato);
				List<DadosArquivoFtp> arquivos = RetornarArquivoEnvioPdf((IRelatorio)(object)impresso, (TipoArquivoWhatsApp)22);
				await CompartilharArquivosAsync(arquivos, ObterTelefone());
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

		public string ObterTelefone()
		{
			string text = HelperString.ToStringEmptyWhenNull(Locacao.Pessoa.TelefonePrincipal.Numero);
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "WhatsApp", "Informe o celular com DDD e número.", (IIcone)null, false, false, true, text, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return text;
		}

		private List<DadosArquivoFtp> RetornarArquivoEnvioPdf(IRelatorio impresso, TipoArquivoWhatsApp tipoArquivoWhatsApp)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			string text = $"{impresso.Descricao}.pdf";
			MemoryStream memoryStream = new MemoryStream();
			impresso.Processar().ExportToPdf((Stream)memoryStream);
			DadosArquivoFtp val = DadosArquivoFtp.Criar().ComArquivoEnvio(Objeto<ArquivoEnvio>.Criar.ComNomeArquivo(text).ComArquivoBase64(Convert.ToBase64String(memoryStream.GetBuffer()))).ComTipoArquivoWhatsApp(tipoArquivoWhatsApp)
				.ComTipoArquivoCompartilhando(HelperEnum.RetornarDescricao((Enum)(object)tipoArquivoWhatsApp));
			List<DadosArquivoFtp> list = new List<DadosArquivoFtp>();
			if (HelperObject.IsNotNull((object)val))
			{
				list.Add(val);
			}
			return list;
		}

		private async Task CompartilharArquivosAsync(IEnumerable<DadosArquivoFtp> arquivos, string telefone)
		{
			if (HelperObject.IsNull((object)telefone))
			{
				return;
			}
			WhatsAppService obj = WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(telefone)
				.ComArquivos(arquivos);
			ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
			object obj2;
			if (configuracaoDto == null)
			{
				obj2 = null;
			}
			else
			{
				AuxiliarDto auxiliar = configuracaoDto.Auxiliar;
				if (auxiliar == null)
				{
					obj2 = null;
				}
				else
				{
					TextoDto mensagemWhatsApp = auxiliar.MensagemWhatsApp;
					obj2 = ((mensagemWhatsApp != null) ? mensagemWhatsApp.Valor : null);
				}
			}
			await obj.ComMensagemWhatsApp((string)obj2).ComEnderecoServidor(DadosSistema.EnderecoServidorWhatsApp).ComCpfCnpjEmitente(DadosSistema.EmpresaLogada.CpfCnpj)
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

		public void Preencher(LocacaoDto locacaoDto)
		{
			Locacao = locacaoDto;
			Titulo = "Impresso para locação";
		}

		public void FecharMethod()
		{
			_seletorDireito.Voltar();
		}

		public void Load()
		{
			PodePesquisarObservacao = DadosSistema.PodeExecutarFuncao(new Type[3]
			{
				typeof(Gerencial),
				typeof(Administrador),
				typeof(AdministradorIndustria)
			});
			DadosContrato = ((!string.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Saida.ObservacaoContratoLocacaoPadrao.Referencia)) ? ObterObservacaoPadrao(DadosSistema.ConfiguracaoDto.Saida.ObservacaoContratoLocacaoPadrao.Referencia) : string.Empty);
			DataVencimentoContrato = Locacao.DataFinalLocacao;
		}

		public void UnLoad()
		{
		}

		public bool PodeVoltar()
		{
			return true;
		}
	}
	public class PainelDireitoLocacao : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly IWService _service;

		private readonly Action<object> _navegarMovimentacao;

		private bool _gerouDavOsAgrupado;

		public bool PodeAtualizar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeAtualizar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeAtualizar), value);
			}
		}

		public LocacaoDto Locacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<LocacaoDto>((Expression<Func<LocacaoDto>>)(() => Locacao));
			}
			set
			{
				((BindableBase)this).SetProperty<LocacaoDto>((Expression<Func<LocacaoDto>>)(() => Locacao), value);
			}
		}

		public string TextoLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TextoLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TextoLocacao), value);
			}
		}

		public ICommand CtrlF6Command { get; set; }

		public ICommand CtrlOCommand { get; set; }

		public ICommand CtrlFCommand { get; set; }

		public ICommand CtrlPCommand { get; set; }

		public ICommand AltHCommand { get; set; }

		public ICommand F9Command { get; set; }

		public ICommand NavegarDavOsCommand { get; set; }

		public ICommand CancelarCommand { get; set; }

		public PainelDireitoLocacao(Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo, IWService service, Action<object> navegarMovimentacao)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Expected O, but got Unknown
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Expected O, but got Unknown
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Expected O, but got Unknown
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Expected O, but got Unknown
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_018e: Expected O, but got Unknown
			_navegarMovimentacao = navegarMovimentacao;
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			_service = service;
			PodeAtualizar = DadosSistema.VerificarPermissao<Locacao, Atualizar>();
			CtrlF6Command = (ICommand)new AsyncCommand((Func<Task>)Editar, (Func<bool>)PodeEditar, (bool?)null);
			CtrlPCommand = (ICommand)new AsyncCommand((Func<Task>)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			CtrlOCommand = (ICommand)new AsyncCommand((Func<Task>)EfetivarReservado, (Func<bool>)PodeEfetivarReservado, (bool?)null);
			CtrlFCommand = (ICommand)new AsyncCommand((Func<Task>)Devolver, (Func<bool>)PodeDevolver, (bool?)null);
			F9Command = (ICommand)new AsyncCommand((Func<Task>)GerarDavOs, (Func<bool>)PodeGerarDavOs, (bool?)null);
			AltHCommand = (ICommand)new AsyncCommand((Func<Task>)AbrirHistorico, (Func<bool>)PodeAbrirHistorico, (bool?)null);
			NavegarDavOsCommand = (ICommand)new DelegateCommand((Action)NavegarDavOs, (Func<bool>)PodeNavegarDavOs, (bool?)null);
			CancelarCommand = (ICommand)new AsyncCommand((Func<Task>)Cancelar, (Func<bool>)PodeCancelar, (bool?)null);
		}

		private bool PodeNavegarDavOs()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado)
			{
				return _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.ItemSelecionado.Locacao.DavOsGerado > 0;
			}
			return false;
		}

		public void NavegarDavOs()
		{
			SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
			{
			}, true);
			if (HelperString.IsNullOrEmpty(_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.ItemSelecionado.Locacao.DavOsReferencia))
			{
				val.AdicionarParametro("InformacaoPesquisa", (object)$"{_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.ItemSelecionado.Locacao.DavOsGerado}");
			}
			val.AdicionarParametro("Identificador", (object)$"{HelperString.ToStringEmptyWhenNull(_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.ItemSelecionado.Locacao.DavOsReferencia)}");
			_navegarMovimentacao(val);
		}

		private bool PodeDevolver()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado && Locacao.SituacaoLocacao.Codigo.Equals(2))
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private async Task Devolver()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Tem certeza que deseja finalizar a locação?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LocacaoDto dto = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(Locacao)));
			if (dto.DavOsGerado == 0L)
			{
				if (DialogoFactory.MostrarDialogoSimNao("Essa locação não gerou DAV-OS, deseja continuar?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					await Finalizar(dto);
				}
			}
			else
			{
				await Finalizar(dto);
			}
		}

		public async Task Finalizar(LocacaoDto locacaoAtualizada)
		{
			SituacaoLocacao val = SituacoesLocacoes.ObterPorCodigo(5);
			Lista<HistoricoLocacaoDto> val2 = new Lista<HistoricoLocacaoDto>();
			val2.Adicionar(locacaoAtualizada.HistoricoLocacao);
			val2.Adicionar(new HistoricoLocacaoDto
			{
				DataHora = DateTime.Now,
				Usuario = $"{DadosSistema.NomeUsuario}",
				Titulo = "Alteração de status devolução de locação",
				NovaSituacaoLocacao = new SituacaoLocacaoDto
				{
					Codigo = val.Codigo,
					Descricao = val.Descricao,
					Cor = val.Cor
				},
				SituacaoAntiga = locacaoAtualizada.SituacaoLocacao.Descricao
			});
			locacaoAtualizada.HistoricoLocacao = val2.Itens;
			locacaoAtualizada.SituacaoLocacao.Codigo = val.Codigo;
			locacaoAtualizada.DataFinalLocacao = DateTime.Now;
			_ = locacaoAtualizada;
			locacaoAtualizada = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(locacaoAtualizada)));
			_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Remover(locacaoAtualizada);
		}

		private bool PodeEfetivarReservado()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado && Locacao.SituacaoLocacao.Codigo.Equals(1))
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private async Task EfetivarReservado()
		{
			LocacaoDto locacaoAtualizada = default(LocacaoDto);
			_ = locacaoAtualizada;
			locacaoAtualizada = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(Locacao)));
			SituacaoLocacao val = SituacoesLocacoes.ObterPorCodigo(2);
			Lista<HistoricoLocacaoDto> val2 = new Lista<HistoricoLocacaoDto>();
			val2.Adicionar(locacaoAtualizada.HistoricoLocacao);
			val2.Adicionar(new HistoricoLocacaoDto
			{
				DataHora = DateTime.Now,
				Usuario = $"{DadosSistema.NomeUsuario}",
				Titulo = "Alteração de status efetivação de reserva",
				NovaSituacaoLocacao = new SituacaoLocacaoDto
				{
					Codigo = val.Codigo,
					Descricao = val.Descricao,
					Cor = val.Cor
				},
				SituacaoAntiga = locacaoAtualizada.SituacaoLocacao.Descricao
			});
			locacaoAtualizada.HistoricoLocacao = val2.Itens;
			locacaoAtualizada.SituacaoLocacao.Codigo = val.Codigo;
			_ = locacaoAtualizada;
			locacaoAtualizada = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(locacaoAtualizada)));
			_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Atualizar(locacaoAtualizada);
		}

		private async Task Imprimir()
		{
			LocacaoDto locacaoDto = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(Locacao)));
			_seletorDireito.Get<PainelDireitoImprimir>().Preencher(locacaoDto);
			_seletorDireito.Selecionar<PainelDireitoImprimir>();
		}

		private bool PodeImprimir()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && (Locacao.SituacaoLocacao.Codigo.Equals(1) || Locacao.SituacaoLocacao.Codigo.Equals(2)))
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private async Task AbrirHistorico()
		{
			LocacaoDto locacaoDto = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(Locacao)));
			_seletorDireito.Get<PainelDireitoHistorico>().Preencher(locacaoDto);
			_seletorDireito.Selecionar<PainelDireitoHistorico>();
		}

		private bool PodeAbrirHistorico()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado)
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private async Task Editar()
		{
			LocacaoDto locacaoDto = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(Locacao)));
			_seletorDireito.Get<PainelFichaLocacao>().ProjetarLocacao(locacaoDto);
			_seletorDireito.Selecionar<PainelFichaLocacao>();
		}

		private bool PodeEditar()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado)
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private bool PodeCancelar()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado)
			{
				LocacaoDto locacao = Locacao;
				bool? obj;
				if (locacao == null)
				{
					obj = null;
				}
				else
				{
					SituacaoLocacaoDto situacaoLocacao = locacao.SituacaoLocacao;
					obj = ((situacaoLocacao != null) ? new bool?(situacaoLocacao.Codigo.Equals(1)) : null);
				}
				if (obj ?? false)
				{
					return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
				}
			}
			return false;
		}

		public async Task Cancelar()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Tem certeza que deseja cancelar a locação?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				string observacao = "";
				DialogoFactory.MostrarDialogoEntradaString(ref observacao, "Observação", "Informe uma justificativa para cancelar a locação", (IIcone)null, false, false, true, (string)null, false, true);
				SituacaoLocacao val = SituacoesLocacoes.ObterPorCodigo(4);
				Lista<HistoricoLocacaoDto> val2 = new Lista<HistoricoLocacaoDto>();
				val2.Adicionar(Locacao.HistoricoLocacao);
				val2.Adicionar(new HistoricoLocacaoDto
				{
					DataHora = DateTime.Now,
					Usuario = $"{DadosSistema.NomeUsuario}",
					NovaSituacaoLocacao = new SituacaoLocacaoDto
					{
						Codigo = val.Codigo,
						Descricao = val.Descricao,
						Cor = val.Cor
					},
					SituacaoAntiga = Locacao.SituacaoLocacao.Descricao,
					Observacao = observacao
				});
				Locacao.HistoricoLocacao = val2.Itens;
				Locacao.SituacaoLocacao.Codigo = val.Codigo;
				Locacao = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(Locacao)));
				AtualizarPainelEsquerdo(Locacao);
			}
		}

		public async Task GerarDavOs()
		{
			try
			{
				LocacaoDto locacaoDto = _service.Canal<ILocacaoModuleService, LocacaoDto>((Func<ILocacaoModuleService, LocacaoDto>)((ILocacaoModuleService x) => x.ObterLocacaoReferencia(((EntityBaseDto)Locacao).Id)));
				if (locacaoDto.DavOsGerado > 0 && DialogoFactory.MostrarDialogoSimNao($"Essa locação já gerou o DAV-OS: {Locacao.DavOsGerado:000000000}. Prosseguir?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				if (HelperObject.IsNull((object)locacaoDto.ProdutoServico) || HelperObject.Is<ProdutoServicoEmpresaDtoNull>((object)locacaoDto.ProdutoServico))
				{
					DialogoFactory.MostrarDialogoConfirma("Serviço não informado!", false, System.Windows.HorizontalAlignment.Center);
					return;
				}
				PessoaDto pessoaResponsavelDavOs = ObterPessoa(((EntityBaseDto)Locacao.Pessoa).Id);
				PessoaDto empresa = ObterPessoa(DadosSistema.EmpresaLogada.Id);
				List<ItemDto> itensDto = new List<ItemDto>();
				List<ObjetoDto> objetos = new List<ObjetoDto> { locacaoDto.ObjetoLocacao };
				ItemDto servico = CriarItemDto(locacaoDto, empresa, pessoaResponsavelDavOs);
				if (servico.DescontoDigitado > 0m)
				{
					locacaoDto.TotalDescontoJaAplicado = servico.DescontoDigitado;
				}
				ItemDto result = _service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(servico))).Result;
				result.Numero = itensDto.Count + 1;
				itensDto.Add(result);
				List<LocacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<LocacaoDto>(_service.Canal<ILocacaoModuleService, IEnumerable<LocacaoDto>>((Func<ILocacaoModuleService, IEnumerable<LocacaoDto>>)((ILocacaoModuleService x) => x.ObterLocacoesPorPessoaData(locacaoDto.DataInicialLocacao, locacaoDto.DataFinalLocacao, ((EntityBaseDto)pessoaResponsavelDavOs).Id, locacaoDto.SituacaoLocacao.Codigo))));
				list.Remove(list.FirstOrDefault((LocacaoDto x) => ((EntityBaseDto)x).Id.Equals(((EntityBaseDto)Locacao).Id)));
				List<LocacaoDto> locacoesMesmaPessoaPeriodoAgrupada = HelperIEnumerable.ToListEmptyWhenNull<LocacaoDto>(list.Where((LocacaoDto x) => x.ObjetoLocacao.PlanoContaCodigoUnico.Equals(locacaoDto.ObjetoLocacao.PlanoContaCodigoUnico) && x.ObjetoLocacao.CentroCustoCodigoUnico.Equals(locacaoDto.ObjetoLocacao.CentroCustoCodigoUnico)));
				if (locacoesMesmaPessoaPeriodoAgrupada.Any() && DialogoFactory.MostrarDialogoSimNao("Esse cliente possui outra(s) locações para o mesmo período, deseja agrupar e gerar apenas um DAV-OS?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
				{
					_gerouDavOsAgrupado = true;
					foreach (LocacaoDto item in locacoesMesmaPessoaPeriodoAgrupada)
					{
						ItemDto servicoOutraLocacao = CriarItemDto(item, empresa, pessoaResponsavelDavOs);
						if (servicoOutraLocacao.DescontoDigitado > 0m)
						{
							item.TotalDescontoJaAplicado = servicoOutraLocacao.DescontoDigitado;
						}
						ItemDto result2 = _service.Canal<IMovimentacaoSaidaModuleService, Task<ItemDto>>((Func<IMovimentacaoSaidaModuleService, Task<ItemDto>>)((IMovimentacaoSaidaModuleService service) => ((IMovimentacaoService<MovimentacaoBaseDto>)(object)service).CriarItemMovimentacao(servicoOutraLocacao))).Result;
						result2.Numero = itensDto.Count + 1;
						itensDto.Add(result2);
					}
					objetos.AddRange(locacoesMesmaPessoaPeriodoAgrupada.Select((LocacaoDto x) => x.ObjetoLocacao));
				}
				MovimentacaoDto davOs = await _service.Canal<ILocacaoModuleService, Task<MovimentacaoDto>>((Func<ILocacaoModuleService, Task<MovimentacaoDto>>)((ILocacaoModuleService service) => service.GerarDavOsLocacao(locacaoDto, pessoaResponsavelDavOs, (IEnumerable<ItemDto>)itensDto, (IEnumerable<ObjetoDto>)objetos)));
				locacaoDto.DavOsGerado = ((MovimentacaoBaseDto)davOs).Numero;
				locacaoDto.DavOsReferencia = ((EntityBaseDto)davOs).Id;
				locacaoDto.HistoricoLocacao = CriarHistorico(locacaoDto, ((MovimentacaoBaseDto)davOs).Numero);
				_ = locacaoDto;
				locacaoDto = await DadosSistema.GetService().Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(locacaoDto)));
				if (_gerouDavOsAgrupado)
				{
					foreach (LocacaoDto dto in locacoesMesmaPessoaPeriodoAgrupada)
					{
						dto.HistoricoLocacao = CriarHistorico(dto, ((MovimentacaoBaseDto)davOs).Numero);
						dto.DavOsGerado = ((MovimentacaoBaseDto)davOs).Numero;
						dto.DavOsReferencia = ((EntityBaseDto)davOs).Id;
						AtualizarPainelEsquerdo(await DadosSistema.GetService().Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(dto))));
					}
				}
				SelecaoCommandParameter<MovimentacaoDto> val = new SelecaoCommandParameter<MovimentacaoDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoDto>)delegate
				{
				}, true);
				val.AdicionarParametro("Identificador", (object)((EntityBaseDto)davOs).Id);
				_navegarMovimentacao(val);
				AtualizarPainelEsquerdo(locacaoDto);
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		public ItemDto CriarItemDto(LocacaoDto locacaoDto, PessoaDto empresa, PessoaDto pessoaResponsavelDavOs)
		{
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Expected O, but got Unknown
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Expected O, but got Unknown
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Expected O, but got Unknown
			decimal num = ((locacaoDto.TotalPrePagamento > 0m && locacaoDto.TotalPrePagamento <= locacaoDto.PrecoUnitario) ? locacaoDto.TotalPrePagamento : 0m);
			ItemDto val = new ItemDto
			{
				ProdutoServicoEmpresaReferencia = ((EntityBaseDto)locacaoDto.ProdutoServico).Id,
				QuantidadeInformada = 1m,
				EmpresaMovimentacao = empresa,
				PessoaMovimentacao = pessoaResponsavelDavOs,
				Quantidade = 1m,
				PrecoUnitarioInformado = locacaoDto.PrecoUnitario,
				TipoMovimentacao = (TipoMovimentacao)2,
				DataHora = DateTime.Now,
				CalculadoPeloSistema = true,
				MovimentacaoObjeto = new MovimentacaoObjetoDto
				{
					Objeto = locacaoDto.ObjetoLocacao
				},
				DepartamentoReferencia = DadosSistema.ObterDepartamentoVenda().DepartamentoVendaReferencia,
				DescontoDigitado = ((num - locacaoDto.TotalDescontoJaAplicado < 0m) ? 0m : (num - locacaoDto.TotalDescontoJaAplicado))
			};
			if (!HelperObject.Is<ItemDtoNull>((object)val))
			{
				return val;
			}
			return (ItemDto)new ItemDtoNull();
		}

		public IEnumerable<HistoricoLocacaoDto> CriarHistorico(LocacaoDto locacaoDto, long davOsGerado)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_0090: Expected O, but got Unknown
			Lista<HistoricoLocacaoDto> obj = new Lista<HistoricoLocacaoDto>();
			obj.Adicionar(locacaoDto.HistoricoLocacao);
			obj.Adicionar(new HistoricoLocacaoDto
			{
				DataHora = DateTime.Now,
				Usuario = $"{DadosSistema.NomeUsuario}",
				Titulo = $"Gerado DAV-OS número {davOsGerado}",
				NovaSituacaoLocacao = new SituacaoLocacaoDto
				{
					Codigo = locacaoDto.SituacaoLocacao.Codigo,
					Descricao = locacaoDto.SituacaoLocacao.Descricao,
					Cor = locacaoDto.SituacaoLocacao.Cor
				}
			});
			return obj.Itens;
		}

		private bool PodeGerarDavOs()
		{
			if (Locacao != null && HelperObject.IsNot<LocacaoDtoNull>((object)Locacao) && _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.PossuiItemSelecionado && DadosSistema.VerificarPermissao<MovimentacaoSaida, ManterDavOs>() && (Locacao.SituacaoLocacao.Codigo.Equals(2) || Locacao.SituacaoLocacao.Codigo.Equals(1)))
			{
				return !_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().PesquisandoLocacao;
			}
			return false;
		}

		private PessoaDto ObterPessoa(string pessoaReferencia)
		{
			return _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(pessoaReferencia)));
		}

		public void Alugar()
		{
			LocacaoMapaViewModel itemSelecionado = _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.ItemSelecionado;
			if (DialogoFactory.MostrarDialogoSimNao("Tem certeza que deseja efetivar o aluguel?", false, 0, System.Windows.HorizontalAlignment.Center) == MessageBoxResult.Yes)
			{
				if (itemSelecionado.Locacao.DataInicialLocacao > DateTime.Now)
				{
					DialogoFactory.MostrarDialogoConfirma("A data inicial da locação é superior a data atual. Verifique!", false, System.Windows.HorizontalAlignment.Center);
				}
				LocacaoDto locacaoDto = _service.Canal<ILocacaoModuleService, LocacaoDto>((Func<ILocacaoModuleService, LocacaoDto>)((ILocacaoModuleService x) => x.ObterLocacaoReferencia(((EntityBaseDto)Locacao).Id)));
				_seletorDireito.Get<PainelFichaLocacao>().ProjetarLocacao(locacaoDto);
				_seletorDireito.Selecionar<PainelFichaLocacao>();
			}
		}

		public void AtualizarPainelEsquerdo(LocacaoDto locacao)
		{
			_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Atualizar(locacao);
		}

		public void Preencher(LocacaoMapaViewModel locacao)
		{
			if (!HelperObject.IsNull((object)locacao))
			{
				Locacao = locacao?.Locacao;
				TextoLocacao = ((locacao.Locacao.SituacaoLocacao.Codigo.Equals(2) || locacao.Locacao.SituacaoLocacao.Codigo.Equals(1)) ? "Locação" : string.Empty);
			}
		}

		public void FecharMethod()
		{
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
		}

		public bool PodeVoltar()
		{
			return false;
		}
	}
	public class PainelEsquerdoMapaLocacoes : UpdateViewBindableBase, IPainel
	{
		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly IWService _service;

		private readonly Lista<LocacaoMapaViewModel> _locacoes;

		public Lista<LocacaoReservadoViewModel> Reservado { get; set; } = new Lista<LocacaoReservadoViewModel>();


		public Lista<LocacaoAlugadoViewModel> Alugado { get; set; } = new Lista<LocacaoAlugadoViewModel>();


		public Lista<LocacaoMapaViewModel> Locacoes { get; set; }

		public Lista<LocacoesAgrupadasViewModel> LocacoesAgrupados { get; set; }

		public AppointmentLabelCollection CorLocacoes { get; set; }

		public Lista<SituacaoLocacao> SituacaoLocacoes { get; set; }

		public string CorAlugado => SituacoesLocacoes.ObterPorCodigo(2).Cor;

		public string CorReservado => SituacoesLocacoes.ObterPorCodigo(1).Cor;

		public bool PesquisandoLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PesquisandoLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PesquisandoLocacao), value);
			}
		}

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

		public string ToolTipTotais => $"Total de locações: {Locacoes.Itens.Select((LocacaoMapaViewModel x) => x.ObjetoId).Distinct().Count()}\n" + $"Inicio de locação atrasada: {Locacoes.Itens.Count((LocacaoMapaViewModel x) => x.InicioLocacaoAtrasada)} \n" + $"Devolução de locação atrasada: {Locacoes.Itens.Count((LocacaoMapaViewModel x) => x.DevolucaoAtrasada)} \n" + $"Inicia locação hoje: {Locacoes.Itens.Count((LocacaoMapaViewModel x) => x.IniciaLocacaoHoje)} \n" + $"Devolução hoje: {Locacoes.Itens.Count((LocacaoMapaViewModel x) => x.DevolveHoje)}";

		public ICommand EditarCommand { get; set; }

		public ICommand AlterouItemSelecionadoCommand { get; set; }

		public ICommand AlterarDataCommand { get; set; }

		public ICommand FecharCommand { get; protected set; }

		public PainelEsquerdoMapaLocacoes(IWService service, Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo)
		{
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Expected O, but got Unknown
			Locacoes = new Lista<LocacaoMapaViewModel>();
			LocacoesAgrupados = new Lista<LocacoesAgrupadasViewModel>();
			_locacoes = new Lista<LocacaoMapaViewModel>();
			_service = service;
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			EditarCommand = (ICommand)new DelegateCommand<object>((Action<object>)Editar, (Func<object, bool>)PodeEditar, (bool?)false);
			AlterouItemSelecionadoCommand = (ICommand)new DelegateCommand<object>((Action<object>)AlterouItemSelecionado, (Func<object, bool>)PodeAlterarItemSelecionado, (bool?)false);
			AlterarDataCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)AlterarData, (Func<object, bool>)PodeAlterarItemSelecionado, false, (bool?)null);
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
		}

		private bool PodeAlterarItemSelecionado(object obj)
		{
			return true;
		}

		private async Task AlterarData(object obj)
		{
			if (HelperObject.IsNull(obj) || !DadosSistema.VerificarPermissao<Locacao, Atualizar>())
			{
				return;
			}
			AppointmentResizeEventArgs itemSelecionado = (AppointmentResizeEventArgs)((obj is AppointmentResizeEventArgs) ? obj : null);
			LocacaoMapaViewModel locacaoMapaViewModel = Locacoes.Itens.FirstOrDefault((LocacaoMapaViewModel x) => x.Id.Equals(((IPersistentObject)itemSelecionado.SourceAppointment).Id));
			if ((from x in _service.Canal<ILocacaoModuleService, IEnumerable<LocacaoDto>>((Func<ILocacaoModuleService, IEnumerable<LocacaoDto>>)((ILocacaoModuleService x) => x.ObterLocacoesEmAtividadePorObjeto(locacaoMapaViewModel.ObjetoId)))
				where ((EntityBaseDto)x).Id != locacaoMapaViewModel.Id.ToString()
				select x).Any((LocacaoDto x) => VerificarPeriodos(x.DataInicialLocacao, x.DataFinalLocacao, itemSelecionado.EditedAppointment.Start, itemSelecionado.EditedAppointment.End)))
			{
				DialogoFactory.MostrarDialogoConfirma("O objeto ja possui uma locação para o período.", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			locacaoMapaViewModel.Locacao.DataInicialLocacao = itemSelecionado.EditedAppointment.Start;
			locacaoMapaViewModel.Locacao.DataFinalLocacao = itemSelecionado.EditedAppointment.End;
			LocacaoDto val = await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService x) => x.AtualizarLocacao(locacaoMapaViewModel.Locacao)));
			Atualizar(val);
			_seletorDireito.Get<PainelDireitoLocacao>().Preencher(new LocacaoMapaViewModel(val));
		}

		public static bool VerificarPeriodos(DateTime dataInicialCadastrada, DateTime dataFinalCadastrada, DateTime dataInicialNova, DateTime dataFinalNova)
		{
			if ((!(dataInicialCadastrada < dataInicialNova) || !(dataInicialNova < dataFinalCadastrada)) && (!(dataInicialCadastrada < dataFinalNova) || !(dataFinalNova <= dataFinalCadastrada)) && (!(dataInicialNova < dataInicialCadastrada) || !(dataFinalNova > dataFinalCadastrada)))
			{
				if (dataInicialCadastrada < dataFinalNova)
				{
					return dataInicialNova < dataFinalCadastrada;
				}
				return false;
			}
			return true;
		}

		private void SituacaoLocacoesOnAlterouItemSelecionadoEvent(object sender, SituacaoLocacao situacao)
		{
			SituacaoLocacao obj = situacao;
			if (((obj != null) ? new int?(obj.Codigo) : null) != SituacoesLocacoes.Todos.Codigo)
			{
				Locacoes.Limpar();
				Locacoes.Adicionar(_locacoes.Itens.Where((LocacaoMapaViewModel x) => x.Locacao.SituacaoLocacao.Codigo.Equals(situacao.Codigo)));
			}
			else
			{
				Locacoes.Limpar();
				Locacoes.Adicionar((IEnumerable<LocacaoMapaViewModel>)_locacoes.Itens);
			}
		}

		private void AlterouItemSelecionado(object obj)
		{
			SchedulerControl val = (SchedulerControl)((obj is SchedulerControl) ? obj : null);
			if (((DXCollectionBase<Appointment>)(object)val.SelectedAppointments).Count > 0)
			{
				object id = ((IPersistentObject)((IEnumerable<Appointment>)val.SelectedAppointments).FirstOrDefault()).Id;
				LocacaoMapaViewModel viewModel = Locacoes.Itens.FirstOrDefault((LocacaoMapaViewModel x) => x.Id.Equals(id));
				_seletorDireito.Get<PainelDireitoLocacao>().Preencher(viewModel);
				_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.Posicionar((Expression<Func<LocacaoMapaViewModel, bool>>)((LocacaoMapaViewModel x) => x.Id.Equals(viewModel.Id)));
			}
			else
			{
				_seletorDireito.Get<PainelDireitoLocacao>().Preencher(null);
			}
		}

		private bool PodeEditar(object obj)
		{
			if (((DXCollectionBase<Appointment>)(object)((SchedulerControl)((obj is SchedulerControl) ? obj : null)).SelectedAppointments).Count > 0)
			{
				return HelperObject.IsNotNull((object)Locacoes);
			}
			return false;
		}

		private void Editar(object obj)
		{
			SchedulerControl val = (SchedulerControl)((obj is SchedulerControl) ? obj : null);
			if (((DXCollectionBase<Appointment>)(object)val.SelectedAppointments).Count > 0)
			{
				object id = ((IPersistentObject)((IEnumerable<Appointment>)val.SelectedAppointments).FirstOrDefault()).Id;
				LocacaoMapaViewModel viewModel = Locacoes.Itens.FirstOrDefault((LocacaoMapaViewModel x) => x.Id.Equals(id));
				Task<LocacaoDto> task = _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => ((IReferenciaAtualizarService<LocacaoDto>)(object)service).AtualizarDtoAsync(viewModel.Locacao)));
				_seletorDireito.Get<PainelFichaLocacao>().ProjetarLocacao(task.Result);
				_seletorDireito.Selecionar<PainelFichaLocacao>();
			}
		}

		public void Habilitar<T>(Seletor<IPainel> seletorDireito)
		{
			Habilitado = HelperObject.Is<T>((object)seletorDireito.Painel);
		}

		public void Atualizar(LocacaoDto locacaoDto)
		{
			if (!HelperObject.IsNull((object)locacaoDto))
			{
				LocacaoMapaViewModel viewModel = new LocacaoMapaViewModel(locacaoDto);
				Locacoes.Substituir((Expression<Func<LocacaoMapaViewModel, bool>>)((LocacaoMapaViewModel x) => x.Id.Equals(viewModel.Id)), viewModel);
				_locacoes.Substituir((Expression<Func<LocacaoMapaViewModel, bool>>)((LocacaoMapaViewModel x) => x.Id.Equals(viewModel.Id)), viewModel);
				_seletorDireito.Get<PainelDireitoLocacao>().Preencher(viewModel);
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ToolTipTotais));
			}
		}

		public void Adicionar(LocacaoDto locacaoDto)
		{
			if (!HelperObject.IsNull((object)locacaoDto))
			{
				LocacaoMapaViewModel locacaoMapaViewModel = new LocacaoMapaViewModel(locacaoDto);
				Locacoes.Adicionar(locacaoMapaViewModel);
				_locacoes.Adicionar(locacaoMapaViewModel);
				_seletorDireito.Get<PainelDireitoLocacao>().Preencher(locacaoMapaViewModel);
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ToolTipTotais));
			}
		}

		public void Remover(LocacaoDto locacaoDto)
		{
			if (!HelperObject.IsNull((object)locacaoDto))
			{
				LocacaoMapaViewModel viewModel = new LocacaoMapaViewModel(locacaoDto);
				Locacoes.Remover(Locacoes.Itens.FirstOrDefault((LocacaoMapaViewModel x) => x.Id.Equals(viewModel.Id)));
				_locacoes.Remover(_locacoes.Itens.FirstOrDefault((LocacaoMapaViewModel x) => x.Id.Equals(viewModel.Id)));
				_seletorDireito.Get<PainelDireitoLocacao>().Preencher(viewModel);
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ToolTipTotais));
				Locacoes.ItemSelecionado = null;
			}
		}

		public async Task ProjetarLocacoes()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Processando...");
				PesquisandoLocacao = true;
				PesquisaDto pesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new TextoNumericoDto
					{
						Texto = string.Empty
					},
					Pagina = 1
				};
				Preencher(await _service.Canal<ILocacaoModuleService, Task<IEnumerable<LocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<LocacaoDto>>>)((ILocacaoModuleService x) => x.ObterLocacoesAtivas(pesquisa, 99))));
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
				PesquisandoLocacao = false;
				((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ToolTipTotais));
			}
		}

		private void Preencher(IEnumerable<LocacaoDto> result)
		{
			LimparTodasLocacoes();
			foreach (LocacaoDto item in result)
			{
				int codigo = item.SituacaoLocacao.Codigo;
				if (codigo == 1)
				{
					Reservado.Adicionar(new LocacaoReservadoViewModel(item));
				}
				else
				{
					Alugado.Adicionar(new LocacaoAlugadoViewModel(item));
				}
			}
			Locacoes.AdicionarSemPosicionar((IEnumerable<LocacaoMapaViewModel>)Reservado.Itens);
			Locacoes.AdicionarSemPosicionar((IEnumerable<LocacaoMapaViewModel>)Alugado.Itens);
			_locacoes.Limpar();
			_locacoes.AdicionarSemPosicionar((IEnumerable<LocacaoMapaViewModel>)Locacoes.Itens);
			foreach (ObjetoDto agrupado in HelperIEnumerable.ToListEmptyWhenNull<ObjetoDto>((IEnumerable<ObjetoDto>)(from x in _service.Canal<IObjetoModuleService, IEnumerable<ObjetoDto>>((Func<IObjetoModuleService, IEnumerable<ObjetoDto>>)((IObjetoModuleService x) => x.ObterObjetosAtivosLocacao()))
				orderby x.Descricao
				select x)))
			{
				LocacaoDto val = result.FirstOrDefault((LocacaoDto y) => ((EntityBaseDto)y.ObjetoLocacao).Id.Equals(((EntityBaseDto)agrupado).Id));
				string text = (HelperObject.IsNull((object)val) ? agrupado.Descricao : HelperString.ToStringEmptyWhenNull(val.ObjetoLocacao.Descricao));
				LocacoesAgrupados.Adicionar(LocacoesAgrupadasViewModel.Criar().ComObjetoId(((EntityBaseDto)agrupado).Id).ComDescricaoLocacao($"({text.ToUpper()})"));
			}
		}

		public void LimparTodasLocacoes()
		{
			Reservado?.Limpar();
			Alugado?.Limpar();
			Locacoes?.Limpar();
			LocacoesAgrupados?.Limpar();
		}

		public bool PodeVoltar()
		{
			return true;
		}

		public void FecharMethod()
		{
			_seletorEsquerdo.VoltarTudo();
		}

		public async void Load()
		{
			CorLocacoes = new AppointmentLabelCollection();
			((DXCollectionBase<IAppointmentLabel>)(object)CorLocacoes).Clear();
			foreach (SituacaoLocacao dado in SituacoesLocacoes.Dados)
			{
				CorLocacoes.Add(CorLocacoes.CreateNewLabel((object)dado.Codigo, dado.Descricao, string.Empty, (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(dado.Cor)));
			}
			SituacaoLocacoes = new Lista<SituacaoLocacao>();
			SituacaoLocacoes.Adicionar((IEnumerable<SituacaoLocacao>)SituacoesLocacoes.Dados);
			SituacaoLocacoes.Remover(SituacaoLocacoes.Itens.FirstOrDefault((SituacaoLocacao x) => x.Codigo.Equals(5)));
			SituacaoLocacoes.Remover(SituacaoLocacoes.Itens.FirstOrDefault((SituacaoLocacao x) => x.Codigo.Equals(4)));
			SituacaoLocacoes.PosicionarNoUltimo();
			SituacaoLocacoes.AlterouItemSelecionadoEvent += SituacaoLocacoesOnAlterouItemSelecionadoEvent;
			await ProjetarLocacoes();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => ToolTipTotais));
		}

		public void UnLoad()
		{
			Lista<LocacaoMapaViewModel> locacoes = Locacoes;
			if (locacoes != null)
			{
				((ViewModelValidationBase)locacoes).Dispose();
			}
			Lista<LocacoesAgrupadasViewModel> locacoesAgrupados = LocacoesAgrupados;
			if (locacoesAgrupados != null)
			{
				((ViewModelValidationBase)locacoesAgrupados).Dispose();
			}
			AppointmentLabelCollection corLocacoes = CorLocacoes;
			if (corLocacoes != null)
			{
				corLocacoes.Dispose();
			}
			SituacaoLocacoes.AlterouItemSelecionadoEvent -= SituacaoLocacoesOnAlterouItemSelecionadoEvent;
		}
	}
	public class PainelFichaLocacao : UpdateViewBindableBase, IPainel
	{
		private readonly IWService _service;

		private readonly Seletor<IPainel> _seletorDireito;

		private readonly Seletor<IPainel> _seletorEsquerdo;

		private readonly Action<string, Action<NavigationResult>, NavigationParameters> _navegar;

		private string _situacaoLocacao;

		public bool IsImage;

		public string NumeroLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NumeroLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NumeroLocacao), value);
			}
		}

		public bool SequenciaNumeracaoVisivel => ((EntityBaseDto)LocacaoDto).IsTransient();

		public ObjetoDto Objeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<ObjetoDto>((Expression<Func<ObjetoDto>>)(() => Objeto));
			}
			set
			{
				((BindableBase)this).SetProperty<ObjetoDto>((Expression<Func<ObjetoDto>>)(() => Objeto), value, (Action)delegate
				{
					TemPessoaObjeto = HelperObject.IsNotNull((object)Pessoa) && HelperObject.IsNot<PessoaDtoNull>((object)Pessoa) && HelperObject.IsNotNull((object)Objeto) && HelperObject.IsNot<ObjetoDtoNull>((object)Objeto);
					InformacaoPesquisaObjeto = Objeto.Descricao;
					FocoDisponibilidade = true;
				});
			}
		}

		public string InformacaoPesquisaObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisaObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => InformacaoPesquisaObjeto), value);
			}
		}

		public bool FocoServico
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoServico));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoServico), value);
			}
		}

		public bool FocoDisponibilidade
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoDisponibilidade));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoDisponibilidade), value);
			}
		}

		public bool FocoObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoObjeto), value);
			}
		}

		public LocacaoDto LocacaoDto
		{
			get
			{
				return ((BindableBase)this).GetProperty<LocacaoDto>((Expression<Func<LocacaoDto>>)(() => LocacaoDto));
			}
			set
			{
				((BindableBase)this).SetProperty<LocacaoDto>((Expression<Func<LocacaoDto>>)(() => LocacaoDto), value);
			}
		}

		public string NomeCliente
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeCliente));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeCliente), value);
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
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Pessoa), value, (Action)delegate
				{
					TemPessoaObjeto = HelperObject.IsNotNull((object)Pessoa) && HelperObject.IsNot<PessoaDtoNull>((object)Pessoa) && HelperObject.IsNotNull((object)Objeto) && HelperObject.IsNot<ObjetoDtoNull>((object)Objeto);
					NomeCliente = Pessoa.Nome;
					FocoObjeto = true;
				});
			}
		}

		public DateTime DataInicialLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicialLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicialLocacao), value, (Action)delegate
				{
					ExibirSelecionarLocacao = false;
				});
			}
		}

		public DateTime DataFinalLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinalLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinalLocacao), value, (Action)delegate
				{
					ExibirSelecionarLocacao = false;
				});
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

		public Lista<SituacaoLocacao> SituacaoLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<SituacaoLocacao>>((Expression<Func<Lista<SituacaoLocacao>>>)(() => SituacaoLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<SituacaoLocacao>>((Expression<Func<Lista<SituacaoLocacao>>>)(() => SituacaoLocacao), value);
			}
		}

		public decimal PrecoUnitario
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => PrecoUnitario), value);
			}
		}

		public decimal TotalPrePagamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalPrePagamento));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalPrePagamento), value);
			}
		}

		public bool ExibirSelecionarLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => ExibirSelecionarLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => ExibirSelecionarLocacao), value);
			}
		}

		public string TituloDataHora
		{
			get
			{
				if (!DadosSistema.ConfiguracaoDto.Geral.CalcularPrecoPorHoraHospedagem.Valor)
				{
					return "Data";
				}
				return "Hora";
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

		public ProdutoServicoEmpresaDto ProdutoServico
		{
			get
			{
				return ((BindableBase)this).GetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServico));
			}
			set
			{
				((BindableBase)this).SetProperty<ProdutoServicoEmpresaDto>((Expression<Func<ProdutoServicoEmpresaDto>>)(() => ProdutoServico), value, (Action)delegate
				{
					TemServico = HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)ProdutoServico);
				});
			}
		}

		public bool TemServico
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TemServico));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TemServico), value);
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


		public bool TemPessoaObjeto
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => TemPessoaObjeto));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => TemPessoaObjeto), value);
			}
		}

		public bool FocoPreco
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoPreco));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoPreco), value);
			}
		}

		public SelecaoValidacaoCommandParameter<PessoaDto> PessoaCommandParameter { get; set; }

		public SelecaoCommandParameter<ProdutoServicoEmpresaDto> ProdutoServicoCommandParameter { get; set; }

		public string Titulo { get; set; }

		public ICommand F7Command { get; set; }

		public ICommand F9Command { get; set; }

		public ICommand FecharCommand { get; set; }

		public ICommand CarregarImagemCommand { get; set; }

		public ICommand CarregarArquivoCommand { get; set; }

		public ICommand<ImagemViewModel> RemoverImagemCommand { get; set; }

		public ICommand<ArquivoViewModel> RemoverArquivoCommand { get; set; }

		public ICommand AbrirAnexoImagemCommand { get; set; }

		public ICommand AbrirAnexoArquivoCommand { get; set; }

		public ICommand VerificarDisponibilidadeDatasCommand { get; set; }

		public ICommand PesquisarProdutoServicoCommand { get; set; }

		public ICommand PesquisarPessoaCommand { get; set; }

		public ICommand PesquisarObjetoCommand { get; set; }

		public ICommand RemoverObjetoCommand { get; set; }

		public ICommand RemoverPessoaCommand { get; set; }

		public PainelFichaLocacao()
		{
		}

		public PainelFichaLocacao(IWService service, Seletor<IPainel> seletorDireito, Seletor<IPainel> seletorEsquerdo, Action<string, Action<NavigationResult>, NavigationParameters> navegar)
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Expected O, but got Unknown
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Expected O, but got Unknown
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Expected O, but got Unknown
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Expected O, but got Unknown
			//IL_016e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0178: Expected O, but got Unknown
			//IL_019a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a4: Expected O, but got Unknown
			//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Expected O, but got Unknown
			_navegar = navegar;
			_service = service;
			_seletorDireito = seletorDireito;
			_seletorEsquerdo = seletorEsquerdo;
			F7Command = (ICommand)new AsyncCommand((Func<Task>)Gravar, (Func<bool>)PodeGravar, (bool?)null);
			FecharCommand = (ICommand)new DelegateCommand((Action)FecharMethod);
			VerificarDisponibilidadeDatasCommand = (ICommand)new DelegateCommand((Action)VerificarDisponibilidadeLocacaoObjeto, (Func<bool>)PodeVerificarDisponibilidadeObjeto, (bool?)null);
			CarregarImagemCommand = (ICommand)new DelegateCommand((Action)CarregarImagem);
			CarregarArquivoCommand = (ICommand)new DelegateCommand((Action)CarregarArquivo);
			RemoverImagemCommand = (ICommand<ImagemViewModel>)(object)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)RemoverImagem);
			RemoverArquivoCommand = (ICommand<ArquivoViewModel>)(object)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)RemoverArquivo);
			AbrirAnexoImagemCommand = (ICommand)new DelegateCommand<ImagemViewModel>((Action<ImagemViewModel>)AbrirAnexoImagem);
			AbrirAnexoArquivoCommand = (ICommand)new DelegateCommand<ArquivoViewModel>((Action<ArquivoViewModel>)AbrirAnexoArquivo);
			PesquisarProdutoServicoCommand = (ICommand)new DelegateCommand((Action)PesquisarProduto);
			PesquisarPessoaCommand = (ICommand)new DelegateCommand((Action)PesquisarPessoa);
			PesquisarObjetoCommand = (ICommand)new DelegateCommand((Action)PesquisarObjeto);
			RemoverObjetoCommand = (ICommand)new DelegateCommand((Action)RemoverObjeto, (Func<bool>)PodeRemoverObjeto, (bool?)null);
			RemoverPessoaCommand = (ICommand)new DelegateCommand((Action)RemoverPessoa, (Func<bool>)PodeRemoverPessoa, (bool?)null);
		}

		private async void PesquisarPessoa()
		{
			try
			{
				PesquisaDto objetoPesquisaHospedes = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
					{
						Informacao = HelperString.ToStringEmptyWhenNull(NomeCliente),
						TipoPessoaCadastro = (TipoPessoaCadastro)0
					},
					Pagina = 1,
					Tamanho = 100
				};
				ProcessarRetornoPesquisa((await _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisaHospedes)))).Lista.OfType<PessoaDto>());
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private void ProcessarRetornoPesquisa(IEnumerable<PessoaDto> resultado)
		{
			List<PessoaDto> list = HelperIEnumerable.ToListEmptyWhenNull<PessoaDto>(resultado);
			if (list.Count == 1)
			{
				CallbackPessoa(list.FirstOrDefault());
				return;
			}
			PessoaCommandParameter = new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)CallbackPessoa, (ValidateParameter<PessoaDto>)(object)new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => HelperObject.IsNot<ClienteDtoNull>((object)x.Cliente) && !x.ConsumidorReservadoSistema), "Selecione uma pessoa do tipo 'Cliente'!", (TipoPessoaCadastro)0, System.Windows.HorizontalAlignment.Center));
			((SelecaoCommandParameter<PessoaDto>)(object)PessoaCommandParameter).Parametros.Add("TipoPessoaCadastro", (object)(TipoPessoaCadastro)0);
			((SelecaoCommandParameter<PessoaDto>)(object)PessoaCommandParameter).Parametros.Add("InformacaoPesquisa", (object)NomeCliente);
			_navegar("PessoaListaView", delegate
			{
			}, ((SelecaoCommandParameter<PessoaDto>)(object)PessoaCommandParameter).Parametros);
		}

		private void CallbackPessoa(PessoaDto pessoa)
		{
			Pessoa = pessoa;
			NomeCliente = ((BaseDto)pessoa).ToText;
		}

		private bool PodeRemoverPessoa()
		{
			if (HelperObject.IsNotNull((object)Pessoa))
			{
				return HelperObject.IsNot<PessoaDtoNull>((object)Pessoa);
			}
			return false;
		}

		private void RemoverPessoa()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Pessoa = (PessoaDto)new PessoaDtoNull();
			NomeCliente = string.Empty;
		}

		private void PesquisarObjeto()
		{
			try
			{
				SelecaoValidacaoCommandParameter<ObjetoDto> val = new SelecaoValidacaoCommandParameter<ObjetoDto>("ObjetoListaView", (Action<ObjetoDto>)CallbackObjeto, new ValidateParameter<ObjetoDto>((Func<ObjetoDto, bool>)((ObjetoDto x) => x.Locacao), "Selecione um objeto do tipo 'Locação'.", System.Windows.HorizontalAlignment.Center));
				if (string.IsNullOrWhiteSpace(InformacaoPesquisaObjeto))
				{
					((SelecaoCommandParameter<ObjetoDto>)(object)val).AdicionarParametro("InformacaoPesquisa", (object)((EntityBaseDto)Pessoa).Id);
					_navegar("ObjetoListaView", delegate
					{
					}, ((SelecaoCommandParameter<ObjetoDto>)(object)val).Parametros);
					return;
				}
				((SelecaoCommandParameter<ObjetoDto>)(object)val).AdicionarParametro("InformacaoPesquisa", (object)InformacaoPesquisaObjeto);
				PaginaEntityDto result2 = _service.Canal<IObjetoModuleService, Task<PaginaEntityDto>>((Func<IObjetoModuleService, Task<PaginaEntityDto>>)((IObjetoModuleService service) => ((IPesquisarAtivos)service).PesquisarAtivosAsync(InformacaoPesquisaObjeto, (short)1, (short)100))).Result;
				if (result2.Lista.Count() == 1)
				{
					BaseDto obj = result2.Lista.FirstOrDefault();
					CallbackObjeto((ObjetoDto)(object)((obj is ObjetoDto) ? obj : null));
				}
				else
				{
					_navegar("ObjetoListaView", delegate
					{
					}, ((SelecaoCommandParameter<ObjetoDto>)(object)val).Parametros);
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void CallbackObjeto(ObjetoDto objetoDto)
		{
			if (!objetoDto.Locacao)
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um objeto do tipo 'Locação'.", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			InformacaoPesquisaObjeto = ((BaseDto)objetoDto).ToText;
			Objeto = objetoDto;
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

		private async void PesquisarProduto()
		{
			try
			{
				string informacao = HelperString.ToStringEmptyWhenNull(InformacaoPesquisa);
				if (informacao.Contains("*"))
				{
					informacao = HelperString.ToStringEmptyWhenNull(informacao.Split('*')[1]);
				}
				string text = ProcessarCodigoDeBarrasService.RetornarCodigoInternoDoCodigoBarras(informacao);
				PesquisaDto objetoPesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new PesquisaProdutoServicoDto
					{
						Informacao = (HelperObject.IsNotNull((object)text) ? text : informacao)
					},
					Pagina = 1,
					Tamanho = 100
				};
				if (!HelperString.IsNullOrEmpty(informacao))
				{
					ProcessarRetornoPesquisa((await DadosSistema.GetService().Canal<IProdutoServicoModuleService, Task<PaginaEntityDto>>((Func<IProdutoServicoModuleService, Task<PaginaEntityDto>>)((IProdutoServicoModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisa)))).Lista.OfType<ProdutoServicoEmpresaDto>(), informacao);
				}
				else
				{
					ProcessarRetornoPesquisa(new List<ProdutoServicoEmpresaDto>(), informacao);
				}
			}
			catch (Exception ex)
			{
				InformacaoPesquisa = string.Empty;
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		private void ProcessarRetornoPesquisa(IEnumerable<ProdutoServicoEmpresaDto> resultado, string informacaoPesquisa)
		{
			List<ProdutoServicoEmpresaDto> list = HelperIEnumerable.ToListEmptyWhenNull<ProdutoServicoEmpresaDto>(resultado);
			if (list.Count == 1)
			{
				SelecionouProdutoServico(list.FirstOrDefault());
				return;
			}
			ProdutoServicoCommandParameter = (SelecaoCommandParameter<ProdutoServicoEmpresaDto>)(object)new SelecaoValidacaoCommandParameter<ProdutoServicoEmpresaDto>("ProdutoServicoListaView", (Action<ProdutoServicoEmpresaDto>)CallbackSelecaoProdutoServicoCommandParameter, new ValidateParameter<ProdutoServicoEmpresaDto>((Func<ProdutoServicoEmpresaDto, bool>)((ProdutoServicoEmpresaDto x) => HelperObject.IsExactlyObject<ServicoDto>((object)x.ProdutoServico.Servico)), "Selecione um cadastro do tipo 'Serviço'.", System.Windows.HorizontalAlignment.Center));
			ProdutoServicoCommandParameter.Parametros.Add("InformacaoPesquisa", (object)informacaoPesquisa);
			_navegar("ProdutoServicoListaView", delegate
			{
			}, ProdutoServicoCommandParameter.Parametros);
		}

		protected void CallbackSelecaoProdutoServicoCommandParameter(ProdutoServicoEmpresaDto produtoServico)
		{
			SelecionouProdutoServico(produtoServico);
		}

		protected void SelecionouProdutoServico(ProdutoServicoEmpresaDto produtoServicoEmpresa)
		{
			if (!HelperObject.IsExactlyObject<ServicoDto>((object)produtoServicoEmpresa.ProdutoServico.Servico))
			{
				DialogoFactory.MostrarDialogoConfirma("Selecione um cadastro do tipo 'Serviço'.", false, System.Windows.HorizontalAlignment.Center);
				return;
			}
			ProdutoServico = produtoServicoEmpresa;
			InformacaoPesquisa = ((BaseDto)produtoServicoEmpresa).ToText;
			FocoPreco = true;
			PrecoUnitario = (PrecoUnitario.Equals(0m) ? produtoServicoEmpresa.Preco.PrecoVendaVigente : PrecoUnitario);
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
				Filter = "Pdf Files (*.pdf)|*.pdf|Arquivos de texto (*.txt)|*.txt"
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

		private void ObterImagens(string locacaoReferencia)
		{
			LoaderProcessoService.MostrarLoader("Obtendo imagens...");
			new Thread((ThreadStart)async delegate
			{
				try
				{
					IEnumerable<AnexoLocacaoDto> imagens = default(IEnumerable<AnexoLocacaoDto>);
					_ = imagens;
					imagens = await _service.Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService service) => service.ObterImagens(locacaoReferencia)));
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_003c: Unknown result type (might be due to invalid IL or missing references)
						//IL_0046: Expected O, but got Unknown
						foreach (AnexoLocacaoDto item in imagens)
						{
							ImagemViewModel adicionar = new ImagemViewModel(HelperImage.ToBitmapSource(item.Image), item.FileName, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6));
							if (!Imagens.Existe((Expression<Func<ImagemViewModel, bool>>)((ImagemViewModel x) => HelperImage.ToArrayByte(x.Imagem).SequenceEqual(HelperImage.ToArrayByte(adicionar.Imagem)))) && !HelperBool.In<string>(item.FileName, (from x in LocacaoDto.AnexosExluidos
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

		private void ObterArquivos(string locacaoReferencia)
		{
			LoaderProcessoService.MostrarLoader("Obtendo arquivos...");
			new Thread((ThreadStart)async delegate
			{
				try
				{
					IEnumerable<AnexoLocacaoDto> arquivos = default(IEnumerable<AnexoLocacaoDto>);
					_ = arquivos;
					arquivos = await _service.Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService service) => service.ObterArquivos(locacaoReferencia)));
					ThreadPrincipal.Executar((Action)delegate
					{
						//IL_003d: Unknown result type (might be due to invalid IL or missing references)
						//IL_0047: Expected O, but got Unknown
						foreach (AnexoLocacaoDto item in arquivos)
						{
							ArquivoViewModel adicionar = new ArquivoViewModel(item.Arquivo, item.FileName, item.NomeArquivo, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6));
							if (!Arquivos.Existe((Expression<Func<ArquivoViewModel, bool>>)((ArquivoViewModel x) => x.Arquivo.SequenceEqual(adicionar.Arquivo))) && !HelperBool.In<string>(item.FileName, (from x in LocacaoDto.AnexosExluidos
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

		private bool PodeGravar()
		{
			if (HelperObject.IsNot<ProdutoServicoEmpresaDtoNull>((object)ProdutoServico))
			{
				return ExibirSelecionarLocacao;
			}
			return false;
		}

		public async Task Gravar()
		{
			_ = 1;
			try
			{
				if (HelperObject.Is<LocacaoDtoNull>((object)LocacaoDto))
				{
					LocacaoDto = new LocacaoDto();
				}
				SituacaoLocacao itemSelecionado = SituacaoLocacao.ItemSelecionado;
				Lista<HistoricoLocacaoDto> val = new Lista<HistoricoLocacaoDto>();
				val.Adicionar(LocacaoDto.HistoricoLocacao);
				val.Adicionar(new HistoricoLocacaoDto
				{
					DataHora = DateTime.Now,
					Usuario = $"{DadosSistema.NomeUsuario}",
					Titulo = (((EntityBaseDto)LocacaoDto).IsTransient() ? "Incluído" : "Editado"),
					SituacaoAntiga = _situacaoLocacao,
					NovaSituacaoLocacao = new SituacaoLocacaoDto
					{
						Codigo = itemSelecionado.Codigo,
						Descricao = itemSelecionado.Descricao,
						Cor = itemSelecionado.Cor
					}
				});
				LocacaoDto.HistoricoLocacao = val.Itens;
				LocacaoDto.DataInicialLocacao = DataInicialLocacao;
				LocacaoDto.DataFinalLocacao = DataFinalLocacao;
				LocacaoDto.ObjetoLocacao = Objeto;
				LocacaoDto.Pessoa = Pessoa;
				LocacaoDto.Observacao = Observacao;
				LocacaoDto.ProdutoServico = ProdutoServico;
				LocacaoDto.Usuario = DadosSistema.NomeUsuario;
				LocacaoDto.SituacaoLocacao = new SituacaoLocacaoDto
				{
					Codigo = itemSelecionado.Codigo,
					Descricao = itemSelecionado.Descricao,
					Cor = itemSelecionado.Cor
				};
				LocacaoDto.PrecoUnitario = PrecoUnitario;
				LocacaoDto.TotalPrePagamento = TotalPrePagamento;
				LocacaoDto.NumeroLocacao = HelperString.ToLong(NumeroLocacao);
				SalvarAnexos();
				await AtualizarPainelEsquerdo(await _service.Canal<ILocacaoModuleService, Task<LocacaoDto>>((Func<ILocacaoModuleService, Task<LocacaoDto>>)((ILocacaoModuleService service) => service.AtualizarLocacao(LocacaoDto))));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				throw;
			}
			_seletorDireito.VoltarTudo();
		}

		public async Task AtualizarPainelEsquerdo(LocacaoDto locacaoDto)
		{
			if (((EntityBaseDto)LocacaoDto).IsTransient())
			{
				await _seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().ProjetarLocacoes();
				_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.Posicionar((Expression<Func<LocacaoMapaViewModel, bool>>)((LocacaoMapaViewModel x) => x.Id.Equals(((EntityBaseDto)LocacaoDto).Id)));
			}
			else
			{
				_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Atualizar(LocacaoDto);
				_seletorEsquerdo.Get<PainelEsquerdoMapaLocacoes>().Locacoes.Posicionar((Expression<Func<LocacaoMapaViewModel, bool>>)((LocacaoMapaViewModel x) => x.Id.Equals(((EntityBaseDto)LocacaoDto).Id)));
			}
		}

		public void SalvarAnexos()
		{
			List<AnexoLocacaoDto> first = HelperIEnumerable.ToListEmptyWhenNull<AnexoLocacaoDto>(((IEnumerable<ImagemViewModel>)Imagens.Itens).Select((Func<ImagemViewModel, AnexoLocacaoDto>)((ImagemViewModel x) => new AnexoLocacaoDto
			{
				FileName = x.FileName,
				Image = HelperImage.Compress(x.Imagem),
				IsImage = true
			})));
			List<AnexoLocacaoDto> first2 = HelperIEnumerable.ToListEmptyWhenNull<AnexoLocacaoDto>(((IEnumerable<ImagemViewModel>)ImagensExcluidas.Itens).Select((Func<ImagemViewModel, AnexoLocacaoDto>)((ImagemViewModel x) => new AnexoLocacaoDto
			{
				FileName = x.FileName,
				IsImage = true
			})));
			List<AnexoLocacaoDto> second = HelperIEnumerable.ToListEmptyWhenNull<AnexoLocacaoDto>(((IEnumerable<ArquivoViewModel>)Arquivos.Itens).Select((Func<ArquivoViewModel, AnexoLocacaoDto>)((ArquivoViewModel x) => new AnexoLocacaoDto
			{
				FileName = x.FileName,
				Arquivo = x.Arquivo,
				NomeArquivo = x.NomeArquivo
			})));
			List<AnexoLocacaoDto> second2 = HelperIEnumerable.ToListEmptyWhenNull<AnexoLocacaoDto>(((IEnumerable<ArquivoViewModel>)ArquivosExcluidos.Itens).Select((Func<ArquivoViewModel, AnexoLocacaoDto>)((ArquivoViewModel x) => new AnexoLocacaoDto
			{
				FileName = x.FileName
			})));
			LocacaoDto.Anexos = first.Union(second);
			LocacaoDto.AnexosExluidos = first2.Union(second2);
		}

		public void ProjetarLocacao(LocacaoDto locacaoDto)
		{
			//IL_0556: Unknown result type (might be due to invalid IL or missing references)
			//IL_0560: Expected O, but got Unknown
			//IL_0656: Unknown result type (might be due to invalid IL or missing references)
			//IL_0660: Expected O, but got Unknown
			LocacaoDto = locacaoDto;
			Objeto = locacaoDto.ObjetoLocacao;
			Pessoa = locacaoDto.Pessoa;
			FocoObjeto = false;
			FocoDisponibilidade = false;
			FocoServico = false;
			FocoPreco = false;
			Titulo = ((HelperObject.IsNull((object)locacaoDto) || HelperObject.Is<LocacaoDtoNull>((object)locacaoDto)) ? "Inclusão de locação" : "Edição de locação");
			if (((EntityBaseDto)locacaoDto).IsTransient())
			{
				NumeroLocacao = (DadosSistema.GetService().Canal<ILocacaoModuleService, int>((Func<ILocacaoModuleService, int>)((ILocacaoModuleService a) => a.ObterSequenciaLocacao())) + 1).ToString();
			}
			else
			{
				NumeroLocacao = locacaoDto.NumeroLocacao.ToString();
			}
			DataInicialLocacao = (locacaoDto.DataInicialLocacao.Equals(DateTime.MinValue) ? DateTime.Now : locacaoDto.DataInicialLocacao);
			DataFinalLocacao = (locacaoDto.DataFinalLocacao.Equals(DateTime.MinValue) ? DateTime.Now.AddDays(1.0) : locacaoDto.DataFinalLocacao);
			Observacao = locacaoDto.Observacao;
			TotalPrePagamento = locacaoDto.TotalPrePagamento;
			SituacaoLocacao = new Lista<SituacaoLocacao>();
			SituacaoLocacao.Adicionar((IEnumerable<SituacaoLocacao>)SituacoesLocacoes.Dados);
			SituacaoLocacao.Remover(SituacaoLocacao.Itens.FirstOrDefault((SituacaoLocacao x) => x.Codigo.Equals(4)));
			SituacaoLocacao.Remover(SituacaoLocacao.Itens.FirstOrDefault((SituacaoLocacao x) => x.Codigo.Equals(5)));
			SituacaoLocacao.Remover(SituacaoLocacao.Itens.FirstOrDefault((SituacaoLocacao x) => x.Codigo.Equals(99)));
			SituacaoLocacao.Posicionar((Expression<Func<SituacaoLocacao, bool>>)((SituacaoLocacao x) => x.Codigo.Equals(locacaoDto.SituacaoLocacao.Codigo.Equals(0) ? 2 : locacaoDto.SituacaoLocacao.Codigo)));
			_situacaoLocacao = SituacaoLocacao.ItemSelecionado.Descricao;
			if (((EntityBaseDto)locacaoDto).IsTransient() && !HelperIEnumerable.IsEmpty<char>((IEnumerable<char>)DadosSistema.ConfiguracaoDto.Geral.ServicoPadraoLocacao.Referencia))
			{
				IEnumerable<ProdutoServicoEmpresaDto> source = _service.Canal<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>((Func<IProdutoServicoModuleService, IEnumerable<ProdutoServicoEmpresaDto>>)((IProdutoServicoModuleService service) => service.ObterProdutoServicoEmpresaPorId(new string[1] { DadosSistema.ConfiguracaoDto.Geral.ServicoPadraoLocacao.Referencia })));
				ProdutoServico = source.FirstOrDefault();
				PrecoUnitario = source.FirstOrDefault().Preco.PrecoVendaVigente;
				InformacaoPesquisa = ((BaseDto)ProdutoServico).ToText ?? string.Empty;
			}
			else
			{
				ProdutoServico = locacaoDto.ProdutoServico;
				PrecoUnitario = locacaoDto.PrecoUnitario;
				InformacaoPesquisa = ((BaseDto)ProdutoServico).ToText ?? string.Empty;
			}
			ExibirSelecionarLocacao = !((EntityBaseDto)locacaoDto).IsTransient();
			ImagensExcluidas.Adicionar(locacaoDto.AnexosExluidos.Where((AnexoLocacaoDto anexo) => anexo.IsImage).Select((Func<AnexoLocacaoDto, ImagemViewModel>)((AnexoLocacaoDto anexo) => new ImagemViewModel((BitmapSource)null, anexo.FileName, HelperDate.FormatarData(anexo.DataCriacao, (FormatoData)6)))));
			Imagens = new Lista<ImagemViewModel>();
			foreach (AnexoLocacaoDto item in locacaoDto.Anexos.Where((AnexoLocacaoDto x) => x.IsImage))
			{
				BitmapSource bitmapSource = HelperImage.ToBitmapSource(HelperImage.Decompress(item.Image));
				Imagens.Adicionar(new ImagemViewModel(bitmapSource, string.Empty, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6)));
			}
			ObterImagens(((EntityBaseDto)locacaoDto).Id);
			ArquivosExcluidos.Adicionar(locacaoDto.AnexosExluidos.Where((AnexoLocacaoDto x) => !x.IsImage).Select((Func<AnexoLocacaoDto, ArquivoViewModel>)((AnexoLocacaoDto anexo) => new ArquivoViewModel((string)null, anexo.FileName, (string)null, HelperDate.FormatarData(anexo.DataCriacao, (FormatoData)6)))));
			Arquivos = new Lista<ArquivoViewModel>();
			foreach (AnexoLocacaoDto item2 in locacaoDto.Anexos.Where((AnexoLocacaoDto x) => !x.IsImage))
			{
				Arquivos.Adicionar(new ArquivoViewModel(item2.Arquivo, string.Empty, string.Empty, HelperDate.FormatarData(item2.DataCriacao, (FormatoData)6)));
			}
			ObterArquivos(((EntityBaseDto)locacaoDto).Id);
		}

		public bool PodeVerificarDisponibilidadeObjeto()
		{
			return DataInicialLocacao <= DataFinalLocacao;
		}

		public static bool VerificarPeriodos(DateTime dataInicialCadastrada, DateTime dataFinalCadastrada, DateTime dataInicialNova, DateTime dataFinalNova)
		{
			if ((!(dataInicialCadastrada < dataInicialNova) || !(dataInicialNova < dataFinalCadastrada)) && (!(dataInicialCadastrada < dataFinalNova) || !(dataFinalNova <= dataFinalCadastrada)) && (!(dataInicialNova < dataInicialCadastrada) || !(dataFinalNova > dataFinalCadastrada)))
			{
				if (dataInicialCadastrada < dataFinalNova)
				{
					return dataInicialNova < dataFinalCadastrada;
				}
				return false;
			}
			return true;
		}

		public void VerificarDisponibilidadeLocacaoObjeto()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Verificando disponibilidades...");
				if (HelperIEnumerable.ToListEmptyWhenNull<LocacaoDto>(from x in _service.Canal<ILocacaoModuleService, IEnumerable<LocacaoDto>>((Func<ILocacaoModuleService, IEnumerable<LocacaoDto>>)((ILocacaoModuleService x) => x.ObterLocacoesEmAtividadePorObjeto(((EntityBaseDto)Objeto).Id)))
					where ((EntityBaseDto)x).Id != ((EntityBaseDto)LocacaoDto).Id
					where VerificarPeriodos(x.DataInicialLocacao, x.DataFinalLocacao, DataInicialLocacao, DataFinalLocacao)
					select x).Count >= Objeto.QuantidadeLocacaoMesmoPeriodo)
				{
					DialogoFactory.MostrarDialogoConfirma("O objeto já possui uma locação para o período.", false, System.Windows.HorizontalAlignment.Center);
					DataInicialLocacao = (LocacaoDto.DataInicialLocacao.Equals(DateTime.MinValue) ? DateTime.Now : LocacaoDto.DataInicialLocacao);
					DataFinalLocacao = (LocacaoDto.DataFinalLocacao.Equals(DateTime.MinValue) ? DateTime.Now : LocacaoDto.DataFinalLocacao);
					ExibirSelecionarLocacao = false;
				}
				else
				{
					SituacaoLocacao.Posicionar((Expression<Func<SituacaoLocacao, bool>>)((SituacaoLocacao x) => (DataInicialLocacao <= DateTime.Now) ? x.Codigo.Equals(2) : x.Codigo.Equals(1)));
					ExibirSelecionarLocacao = true;
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
			finally
			{
				ThreadPrincipal.Executar((Action)LoaderProcessoService.EsconderLoader);
			}
		}

		public bool PodeVoltar()
		{
			return true;
		}

		public void FecharMethod()
		{
			_seletorDireito.VoltarTudo();
		}

		public void Load()
		{
		}

		public void UnLoad()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			Pessoa = (PessoaDto)new PessoaDtoNull();
			Objeto = (ObjetoDto)new ObjetoDtoNull();
			NomeCliente = string.Empty;
			FocoObjeto = false;
			FocoDisponibilidade = false;
			FocoServico = false;
			FocoPreco = false;
		}
	}
}
namespace Otus.Modulo.Locacoes.ViewModels.MapaLocacoes
{
	public class LocacaoAlugadoViewModel : LocacaoMapaViewModel
	{
		public string Cor => SituacoesLocacoes.ObterPorCodigo(2).Cor;

		public LocacaoAlugadoViewModel(LocacaoDto locacao)
			: base(locacao)
		{
		}
	}
	public class LocacaoReservadoViewModel : LocacaoMapaViewModel
	{
		public string Cor => SituacoesLocacoes.ObterPorCodigo(1).Cor;

		public LocacaoReservadoViewModel(LocacaoDto locacao)
			: base(locacao)
		{
		}
	}
	public class LocacoesAgrupadasViewModel
	{
		public string DescricaoLocacao { get; set; }

		public string ObjetoId { get; set; }

		private LocacoesAgrupadasViewModel()
		{
		}

		private LocacoesAgrupadasViewModel(string objetoId, string descricaoLocacao)
		{
			DescricaoLocacao = descricaoLocacao;
			ObjetoId = objetoId;
		}

		protected LocacoesAgrupadasViewModel Build()
		{
			return new LocacoesAgrupadasViewModel(ObjetoId, DescricaoLocacao);
		}

		public static LocacoesAgrupadasViewModel Criar()
		{
			return new LocacoesAgrupadasViewModel();
		}

		public LocacoesAgrupadasViewModel ComObjetoId(string objetoId)
		{
			ObjetoId = objetoId;
			return Build();
		}

		public LocacoesAgrupadasViewModel ComDescricaoLocacao(string descricao)
		{
			DescricaoLocacao = descricao;
			return Build();
		}
	}
	public class LocacaoMapaViewModel : BindableBase
	{
		public bool DevolucaoLocacaoAtrasada
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => DevolucaoLocacaoAtrasada));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => DevolucaoLocacaoAtrasada), value);
			}
		}

		public bool AllDay
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AllDay));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AllDay), value);
			}
		}

		public bool IniciaLocacaoHoje
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IniciaLocacaoHoje));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IniciaLocacaoHoje), value);
			}
		}

		public bool DevolveHoje
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => DevolveHoje));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => DevolveHoje), value);
			}
		}

		public bool DevolucaoAtrasada
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => DevolucaoAtrasada));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => DevolucaoAtrasada), value);
			}
		}

		public bool InicioLocacaoAtrasada
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => InicioLocacaoAtrasada));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => InicioLocacaoAtrasada), value);
			}
		}

		public string TextoCompromissoSelecionado
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => TextoCompromissoSelecionado));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => TextoCompromissoSelecionado), value);
			}
		}

		public int CodigoCorLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => CodigoCorLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => CodigoCorLocacao), value);
			}
		}

		public object Id { get; set; }

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

		public string DescricaoLocacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoLocacao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoLocacao), value);
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

		public string ObjetoId
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => ObjetoId));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => ObjetoId), value);
			}
		}

		public LocacaoDto Locacao { get; set; }

		public ICommand<object> NavegarRestauranteCommand { get; set; }

		public ICommand<object> EmitirConferenciaCommand { get; set; }

		public ICommand<object> EmitirDocumentoFiscalCommand { get; set; }

		public ICommand<object> EmitirPreviewConferenciaCommand { get; set; }

		public LocacaoMapaViewModel(LocacaoDto locacao)
		{
			DataInicial = locacao.DataInicialLocacao;
			DataFinal = locacao.DataFinalLocacao;
			CodigoCorLocacao = locacao.SituacaoLocacao.Codigo;
			DescricaoLocacao = $"{locacao.ObjetoLocacao.Descricao}";
			ToolTip = $"Data incial: {locacao.DataInicialLocacao}\n" + $"Data final: {locacao.DataFinalLocacao}";
			TextoCompromissoSelecionado = $"{locacao.Pessoa.Nome.ToUpper()}";
			Id = (Helper.IsEmpty((IEnumerable)((EntityBaseDto)locacao).Id) ? HelperObjectId.GenerateId() : ((EntityBaseDto)locacao).Id);
			ObjetoId = ((EntityBaseDto)locacao.ObjetoLocacao).Id;
			Locacao = locacao;
			DevolucaoLocacaoAtrasada = locacao.SituacaoLocacao.Codigo.Equals(2) && locacao.DataFinalLocacao < DateTime.Now;
			InicioLocacaoAtrasada = locacao.SituacaoLocacao.Codigo.Equals(1) && locacao.DataInicialLocacao < DateTime.Now;
			IniciaLocacaoHoje = (locacao.SituacaoLocacao.Codigo.Equals(1) || locacao.SituacaoLocacao.Codigo.Equals(2)) && locacao.DataInicialLocacao.Date.Equals(DateTime.Now.Date);
			DevolveHoje = (locacao.SituacaoLocacao.Codigo.Equals(1) || locacao.SituacaoLocacao.Codigo.Equals(2)) && locacao.DataFinalLocacao.Date.Equals(DateTime.Now.Date);
			AllDay = false;
		}
	}
}
namespace Otus.Modulo.Locacoes.ViewModels.LocacoesFinalizadas
{
	public class LocacoesFinalizadasAbaViewModel : AbaPrincipalViewModelBase
	{
		public LocacoesFinalizadasAbaViewModel(IViewModelPrincipal viewModel)
			: base("Locações", "Informações gerais", viewModel)
		{
		}
	}
	public class LocacoesFinalizadasFichaViewModel : FichaBase<IEnumerable<HospedagemDto>, LocacoesFinalizadasViewModel>
	{
		public LocacoesFinalizadasFichaViewModel()
			: base("Locações finalizadas")
		{
			base.DisableConfirmOnClose();
			((NavigationBase)this).ParametroRetornado += OnParametroRetornado;
		}

		private void OnParametroRetornado(object sender, NavigationContext navigationContext)
		{
			base.DisableConfirmOnClose();
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroRetornado -= OnParametroRetornado;
			base.Dispose();
		}
	}
	public class LocacoesFinalizadasFormulario : FormularioEntity<IEnumerable<LocacaoDto>>
	{
		public LocacoesFinalizadasFormulario(IEnumerable<LocacaoDto> locacoes, Action<string, Action<NavigationResult>, NavigationParameters> navegar)
			: base(locacoes)
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new LocacoesFinalizadasAbaViewModel((IViewModelPrincipal)(object)new LocacoesFinalizadasViewModel(base.Dto, navegar)) });
		}
	}
	public class LocacoesFinalizadasViewModel : ViewModelDtoBase<IEnumerable<LocacaoDto>>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private ICollection _itemSelecionado;

		private readonly Action<string, Action<NavigationResult>, NavigationParameters> _navegar;

		public DateTime DataInicialPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicialPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataInicialPesquisa), value);
			}
		}

		public DateTime DataFinalPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinalPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataFinalPesquisa), value);
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

		public IEnumerable<LocacaoDto> Itens
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<LocacaoDto>>((Expression<Func<IEnumerable<LocacaoDto>>>)(() => Itens));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<LocacaoDto>>((Expression<Func<IEnumerable<LocacaoDto>>>)(() => Itens), value);
			}
		}

		public ICommand ImprimirCommand { get; set; }

		public ICommand ItensSelecionadosCommand { get; set; }

		public ICommand ExibirAnexosCommand { get; set; }

		public ICommand PesquisarLocacoesCommand { get; set; }

		public LocacoesFinalizadasViewModel()
		{
		}

		public LocacoesFinalizadasViewModel(IEnumerable<LocacaoDto> locacoes, Action<string, Action<NavigationResult>, NavigationParameters> navegar)
			: base(locacoes)
		{
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			_navegar = navegar;
			Itens = locacoes;
			ObterItensSelecionados(new List<LocacaoDto> { Itens.FirstOrDefault() });
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, (Func<bool>)PodeImprimir, (bool?)null);
			ItensSelecionadosCommand = (ICommand)new DelegateCommand<object>((Action<object>)ObterItensSelecionados);
			ExibirAnexosCommand = (ICommand)new AsyncCommand((Func<Task>)Anexos, (Func<bool>)PodeAbrirAnexos, (bool?)null);
			PesquisarLocacoesCommand = (ICommand)new AsyncCommand((Func<Task>)PesquisarLocacoes, true);
		}

		public async Task PesquisarLocacoes()
		{
			_ = 1;
			try
			{
				LoaderProcessoService.MostrarLoader("Pesquisando locações finalizadas");
				IWService service = DadosSistema.GetService();
				List<LocacaoDto> list = HelperIEnumerable.ToListEmptyWhenNull<LocacaoDto>(service.Canal<ILocacaoModuleService, IEnumerable<LocacaoDto>>((Func<ILocacaoModuleService, IEnumerable<LocacaoDto>>)((ILocacaoModuleService x) => x.ObterLocacoesFinalizadas(DataInicialPesquisa.Date, HelperDate.ToFinalDia(DataFinalPesquisa)))));
				List<LocacaoDto> locacoesAnexosPreenchidos = new List<LocacaoDto>();
				foreach (LocacaoDto hospedagemDto in list)
				{
					IEnumerable<AnexoLocacaoDto> arquivos = await service.Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService x) => x.ObterArquivos(((EntityBaseDto)hospedagemDto).Id)));
					IEnumerable<AnexoLocacaoDto> enumerable = await service.Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService x) => x.ObterImagens(((EntityBaseDto)hospedagemDto).Id)));
					if (arquivos.Any() || enumerable.Any())
					{
						hospedagemDto.Anexos = HelperIEnumerable.ToListEmptyWhenNull<AnexoLocacaoDto>(enumerable.Union(arquivos));
					}
					locacoesAnexosPreenchidos.Add(hospedagemDto);
				}
				Itens = locacoesAnexosPreenchidos;
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

		public bool PodeImprimir()
		{
			if (HelperObject.IsNotNull((object)_itemSelecionado) && HelperObject.IsNot<LocacaoDtoNull>((object)_itemSelecionado) && _itemSelecionado.Count > 0)
			{
				return Itens.Any();
			}
			return false;
		}

		public bool PodeAbrirAnexos()
		{
			if (HelperObject.IsNotNull((object)_itemSelecionado) && HelperObject.IsNot<LocacaoDtoNull>((object)_itemSelecionado) && _itemSelecionado.Count > 0)
			{
				return Itens.Any();
			}
			return false;
		}

		private async Task Anexos()
		{
			_ = 1;
			try
			{
				IEnumerable<AnexoLocacaoDto> imagens = await DadosSistema.GetService().Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService service) => service.ObterImagens(((EntityBaseDto)_itemSelecionado.OfType<LocacaoDto>().FirstOrDefault()).Id)));
				IEnumerable<AnexoLocacaoDto> enumerable = await DadosSistema.GetService().Canal<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>((Func<ILocacaoModuleService, Task<IEnumerable<AnexoLocacaoDto>>>)((ILocacaoModuleService service) => service.ObterArquivos(((EntityBaseDto)_itemSelecionado.OfType<LocacaoDto>().FirstOrDefault()).Id)));
				Imagens = new Lista<ImagemViewModel>();
				foreach (AnexoLocacaoDto item in imagens)
				{
					ImagemViewModel val = new ImagemViewModel(HelperImage.ToBitmapSource(item.Image), item.FileName, HelperDate.FormatarData(item.DataCriacao, (FormatoData)6));
					Imagens.Adicionar(val);
				}
				Arquivos = new Lista<ArquivoViewModel>();
				foreach (AnexoLocacaoDto item2 in enumerable)
				{
					ArquivoViewModel val2 = new ArquivoViewModel(item2.Arquivo, item2.FileName, item2.NomeArquivo, HelperDate.FormatarData(item2.DataCriacao, (FormatoData)6));
					Arquivos.Adicionar(val2);
				}
				NavigationParameters val3 = new NavigationParameters();
				val3.Add("ItemModuloCreator", (object)143);
				NavigationParameters val4 = val3;
				val4.Add("Imagens", (object)Imagens.Itens);
				val4.Add("Arquivos", (object)Arquivos.Itens);
				_navegar("VisualizarAnexoFichaView", delegate
				{
				}, val4);
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
		}

		public void ObterItensSelecionados(object obj)
		{
			_itemSelecionado = (ICollection)obj;
		}

		private void Imprimir()
		{
			try
			{
				LoaderProcessoService.MostrarLoader("Imprimindo resumo da locação...");
				XtraReportExtensions.ShowPreviewDialog((IReport)(object)((Relatorio)new ImpressoContratoFinalizado(_itemSelecionado.OfType<LocacaoDto>().FirstOrDefault())).Processar());
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

		public override void ProjectedAsViewModel()
		{
			DataInicialPesquisa = DateTime.Now;
			DataFinalPesquisa = DateTime.Now;
		}

		public string ToTituloLista()
		{
			return string.Empty;
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new LocacoesFinalizadasFormulario(base.Dto, _navegar);
		}

		public string ToTituloFicha()
		{
			return string.Empty;
		}
	}
}
namespace Otus.Modulo.Locacoes.Styles.SchedulerControls
{
	public class SchedulerControlCustomLocacao : SchedulerControl
	{
		public SchedulerControlCustomLocacao()
		{
			((FrameworkElement)this).Loaded += ((SchedulerControl)this).OnLoaded;
		}

		protected override void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			SchedulerControlCustomLocacao obj = sender as SchedulerControlCustomLocacao;
			((SchedulerViewBase)((SchedulerControl)obj).DayView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).DayView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateDayView");
			((SchedulerViewBase)((SchedulerControl)obj).MonthView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).MonthView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateMonthView");
			((SchedulerViewBase)((SchedulerControl)obj).WorkWeekView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).WorkWeekView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateWorkWeekView");
			((SchedulerViewBase)((SchedulerControl)obj).WeekView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).WeekView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateWeekView");
			((SchedulerViewBase)((SchedulerControl)obj).FullWeekView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).FullWeekView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateFullWeekView");
			((SchedulerControl)obj).TimelineView.IntervalCount = DadosSistema.QuantidadeDiasExibirMapaLocacoes;
			((FrameworkContentElement)(object)((SchedulerControl)obj).TimelineView).Style = (Style)((FrameworkElement)this).FindResource((object)"AppointmentTemplateTimelineView");
			((SchedulerViewBase)((SchedulerControl)obj).TimelineView).ResourcesPerPage = DadosSistema.QuantidadeObjetosExibirMapaLocacoes;
			((SchedulerViewBase)((SchedulerControl)obj).TimelineView).ShowMoreButtons = false;
			((SchedulerControl)obj).TimelineView.SelectionBar.Visible = false;
			((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions.AppointmentAutoHeight = false;
			((SchedulerAppointmentDisplayOptions)((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions).EndTimeVisibility = (AppointmentTimeVisibility)2;
			((SchedulerAppointmentDisplayOptions)((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions).ShowRecurrence = false;
			((SchedulerViewAppointmentDisplayOptions)((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions).SnapToCellsMode = (AppointmentSnapToCellsMode)2;
			((SchedulerAppointmentDisplayOptions)((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions).StartTimeVisibility = (AppointmentTimeVisibility)2;
			((SchedulerControl)obj).TimelineView.AppointmentDisplayOptions.AppointmentAutoHeight = true;
		}

		protected override void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
		}
	}
}
namespace Otus.Modulo.Locacoes.Reports
{
	public class ImpressoContratoFinalizado : Relatorio
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

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel xrLabel10;

		private XRLabel dataEmissao;

		private XRLabel lblDescricao;

		private XRLabel xrLabel30;

		private XRLabel xrLabel44;

		private XRLabel xrLabel27;

		private XRLabel xrLabel26;

		private XRLabel xrLabel25;

		private XRLabel xrLabel17;

		private XRLabel xrLabel14;

		private XRLabel xrLabel24;

		private XRLabel xrLabel28;

		private XRLabel xrLabel29;

		public override string Descricao => "Contrato de locação finalizado";

		public override bool ExibirNoModuloRelatorios => false;

		public LocacaoRelatorioContrato ImpressoLocacao { get; set; }

		public ImpressoContratoFinalizado(LocacaoDto impressoLocacao)
		{
			ImpressoLocacao = LocacaoRelatorioContrato.Criar().ComLocacao(impressoLocacao);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XtraReportBase)this).DataSource = new List<LocacaoRelatorioContrato> { ImpressoLocacao };
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XRControl)dataEmissao).Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
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
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0308: Unknown result type (might be due to invalid IL or missing references)
			//IL_0332: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f3: Expected O, but got Unknown
			//IL_0433: Unknown result type (might be due to invalid IL or missing references)
			//IL_045c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0507: Unknown result type (might be due to invalid IL or missing references)
			//IL_0530: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d5: Expected O, but got Unknown
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_063e: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e9: Expected O, but got Unknown
			//IL_0729: Unknown result type (might be due to invalid IL or missing references)
			//IL_0752: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f7: Expected O, but got Unknown
			//IL_0837: Unknown result type (might be due to invalid IL or missing references)
			//IL_0860: Unknown result type (might be due to invalid IL or missing references)
			//IL_0917: Unknown result type (might be due to invalid IL or missing references)
			//IL_0941: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aeb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af1: Expected O, but got Unknown
			//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d2d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f12: Expected O, but got Unknown
			//IL_0f52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1031: Unknown result type (might be due to invalid IL or missing references)
			//IL_105a: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_10fe: Expected O, but got Unknown
			//IL_113e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1167: Unknown result type (might be due to invalid IL or missing references)
			//IL_1211: Unknown result type (might be due to invalid IL or missing references)
			//IL_123a: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_12de: Expected O, but got Unknown
			//IL_131e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1347: Unknown result type (might be due to invalid IL or missing references)
			//IL_13fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1427: Unknown result type (might be due to invalid IL or missing references)
			//IL_14d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_14dc: Expected O, but got Unknown
			//IL_151c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1545: Unknown result type (might be due to invalid IL or missing references)
			//IL_15fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1625: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_171a: Unknown result type (might be due to invalid IL or missing references)
			//IL_17c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_181e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1895: Unknown result type (might be due to invalid IL or missing references)
			//IL_18af: Unknown result type (might be due to invalid IL or missing references)
			//IL_18ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_1935: Unknown result type (might be due to invalid IL or missing references)
			//IL_194f: Unknown result type (might be due to invalid IL or missing references)
			//IL_198a: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a07: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a42: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ba3: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			xrLabel28 = new XRLabel();
			xrLabel29 = new XRLabel();
			xrLabel24 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel44 = new XRLabel();
			lblDescricao = new XRLabel();
			xrLabel30 = new XRLabel();
			xrLabel15 = new XRLabel();
			xrLabel18 = new XRLabel();
			xrLabel20 = new XRLabel();
			xrLabel22 = new XRLabel();
			xrLabel1 = new XRLabel();
			xrLabel2 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel10 = new XRLabel();
			dataEmissao = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			toDescricao = new CalculatedField();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			xrCrossBandLine3 = new XRCrossBandLine();
			calculatedField1 = new CalculatedField();
			calculatedField2 = new CalculatedField();
			PageFooter = new PageFooterBand();
			lblMensagemRodape = new XRLabel();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[21]
			{
				(XRControl)xrLabel28,
				(XRControl)xrLabel29,
				(XRControl)xrLabel24,
				(XRControl)xrLabel14,
				(XRControl)xrLabel27,
				(XRControl)xrLabel26,
				(XRControl)xrLabel25,
				(XRControl)xrLabel17,
				(XRControl)xrLabel44,
				(XRControl)lblDescricao,
				(XRControl)xrLabel30,
				(XRControl)xrLabel15,
				(XRControl)xrLabel18,
				(XRControl)xrLabel20,
				(XRControl)xrLabel22,
				(XRControl)xrLabel1,
				(XRControl)xrLabel2,
				(XRControl)xrLabel8,
				(XRControl)xrLabel9,
				(XRControl)xrLabel10,
				(XRControl)dataEmissao
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 642.0182f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel28).Borders = (BorderSide)3;
			((XRControl)xrLabel28).Dpi = 254f;
			((XRControl)xrLabel28).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel28).LocationFloat = new PointFloat(155.1861f, 138.5f);
			((XRControl)xrLabel28).Name = "xrLabel28";
			((XRControl)xrLabel28).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel28).SizeF = new SizeF(311.8238f, 56f);
			((XRControl)xrLabel28).StylePriority.UseBorders = false;
			((XRControl)xrLabel28).StylePriority.UseFont = false;
			((XRControl)xrLabel28).StylePriority.UsePadding = false;
			((XRControl)xrLabel28).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel28).Text = "Número da locação:";
			((XRControl)xrLabel28).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel29).Borders = (BorderSide)6;
			((XRControl)xrLabel29).CanGrow = false;
			((XRControl)xrLabel29).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.NumeroLocacao")
			});
			((XRControl)xrLabel29).Dpi = 254f;
			((XRControl)xrLabel29).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel29).LocationFloat = new PointFloat(467.0099f, 138.5f);
			((XRControl)xrLabel29).Name = "xrLabel29";
			((XRControl)xrLabel29).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel29).SizeF = new SizeF(1387.238f, 56f);
			((XRControl)xrLabel29).StylePriority.UseBorders = false;
			((XRControl)xrLabel29).StylePriority.UseFont = false;
			((XRControl)xrLabel29).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel29).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel24).Borders = (BorderSide)0;
			((XRControl)xrLabel24).Dpi = 254f;
			((XRControl)xrLabel24).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel24).LocationFloat = new PointFloat(50f, 25f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(1900f, 81.75001f);
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "RESUMO DE LOCAÇÃO FINALIZADA";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel14).Borders = (BorderSide)9;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.ObjetoLocacao.Descricao")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(155.188f, 556.1422f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(1381.024f, 42.99994f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UsePadding = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).Borders = (BorderSide)12;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.DataFinalLocacao", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(500.8362f, 394.5002f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(1353.413f, 50.00006f);
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).WordWrap = false;
			((XRControl)xrLabel26).Borders = (BorderSide)4;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.DataInicialLocacao", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(500.8362f, 344.5002f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(1353.412f, 50.00003f);
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel26).WordWrap = false;
			((XRControl)xrLabel25).Borders = (BorderSide)9;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(155.1853f, 394.5004f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(345.6508f, 50f);
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UsePadding = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Data final locação:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel17).Borders = (BorderSide)1;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(155.1879f, 344.5004f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(345.6482f, 50f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Data de inicio locação:";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel44).Borders = (BorderSide)13;
			((XRControl)xrLabel44).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.PrecoUnitario", "{0:n2}")
			});
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1536.212f, 556.142f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(318.0388f, 43f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UsePadding = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)64;
			((XRControl)lblDescricao).BackColor = System.Drawing.Color.Transparent;
			((XRControl)lblDescricao).Borders = (BorderSide)11;
			((XRControl)lblDescricao).Dpi = 254f;
			((XRControl)lblDescricao).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)lblDescricao).LocationFloat = new PointFloat(155.1856f, 506.1417f);
			((XRControl)lblDescricao).Name = "lblDescricao";
			((XRControl)lblDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDescricao).SizeF = new SizeF(1381.025f, 50.00006f);
			((XRControl)lblDescricao).StylePriority.UseBackColor = false;
			((XRControl)lblDescricao).StylePriority.UseBorders = false;
			((XRControl)lblDescricao).StylePriority.UseFont = false;
			((XRControl)lblDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblDescricao).Text = "Objeto locado";
			((XRControl)lblDescricao).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel30).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel30).Borders = (BorderSide)15;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(1536.212f, 506.1418f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(318.0391f, 50f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).StylePriority.UsePadding = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).Text = "Valor total";
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel15).Borders = (BorderSide)1;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(155.1851f, 194.4998f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(225.2534f, 50.00003f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "Contratante:";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel18).Borders = (BorderSide)0;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.Pessoa.Nome")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(380.4416f, 194.4998f);
			((XRControl)xrLabel18).Name = "xrLabel18";
			((XRControl)xrLabel18).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel18).SizeF = new SizeF(674.7686f, 50.00012f);
			((XRControl)xrLabel18).StylePriority.UseBorders = false;
			((XRControl)xrLabel18).StylePriority.UseFont = false;
			((XRControl)xrLabel18).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel18).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel18).WordWrap = false;
			((XRControl)xrLabel20).Borders = (BorderSide)0;
			((XRControl)xrLabel20).Dpi = 254f;
			((XRControl)xrLabel20).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1055.21f, 194.5f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(166.616f, 50f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).Text = "CPF/CNPJ:";
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel22).Borders = (BorderSide)4;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.Pessoa.CpfCnpjFormatado")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1221.826f, 194.5f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(632.4221f, 50.00003f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(675.1745f, 244.4999f);
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
				new XRBinding("Text", (object)null, "Locacao.Pessoa.EnderecoPrincipal.EncurtadoToText")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(839.0978f, 244.5002f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1015.153f, 50.00006f);
			((XRControl)xrLabel2).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel2).WordWrap = false;
			((XRControl)xrLabel8).Borders = (BorderSide)1;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(155.1876f, 244.5002f);
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
				new XRBinding("Text", (object)null, "Locacao.Pessoa.TelefonePrincipal.ToText")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(338.1078f, 244.4999f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(337.0666f, 50.00012f);
			((XRControl)xrLabel9).StylePriority.UseBorders = false;
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel9).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel9).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel9).WordWrap = false;
			((XRControl)xrLabel10).Borders = (BorderSide)1;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(155.1871f, 294.5003f);
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
			((XRControl)dataEmissao).LocationFloat = new PointFloat(416.2939f, 294.5003f);
			((XRControl)dataEmissao).Name = "dataEmissao";
			((XRControl)dataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)dataEmissao).SizeF = new SizeF(1437.957f, 50.00009f);
			((XRControl)dataEmissao).StylePriority.UseBorders = false;
			((XRControl)dataEmissao).StylePriority.UseFont = false;
			((XRControl)dataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)dataEmissao).TextAlignment = (TextAlignment)16;
			((XRControl)dataEmissao).WordWrap = false;
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
			((XRCrossBandControl)xrCrossBandLine3).EndBand = (Band)(object)Detail;
			((XRCrossBandControl)xrCrossBandLine3).EndPointFloat = new PointFloat(3.032459f, 2f);
			((XRControl)xrCrossBandLine3).LocationFloat = new PointFloat(3.032459f, 0f);
			((XRControl)xrCrossBandLine3).Name = "xrCrossBandLine3";
			((XRCrossBandControl)xrCrossBandLine3).StartBand = (Band)(object)Detail;
			((XRCrossBandControl)xrCrossBandLine3).StartPointFloat = new PointFloat(3.032459f, 0f);
			((XRControl)xrCrossBandLine3).WidthF = 1996.968f;
			((DataContainerComponent)calculatedField1).DataMember = "Hospedagem.Itens";
			calculatedField1.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField1.Name = "calculatedField1";
			((DataContainerComponent)calculatedField2).DataMember = "Hospedagem.Servicos";
			calculatedField2.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField2.Name = "calculatedField2";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblMensagemRodape });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 38.4108f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)lblMensagemRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemRodape).BorderWidth = 1f;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Times New Roman", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(3.000001f, 0f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1997.483f, 38.4108f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseBorderWidth = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			bindingSource1.DataSource = typeof(LocacaoRelatorioContrato);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[4]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageFooter
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
			((XtraReport)this).RollPaper = true;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
	public class LocacaoRelatorioContrato
	{
		public Pessoa Empresa { get; set; }

		public LocacaoDto Locacao { get; set; }

		public string ConteudoContrato { get; set; }

		public LocacaoRelatorioContrato(Pessoa empresa, LocacaoDto locacao, string conteudoContrato)
		{
			Empresa = empresa;
			Locacao = locacao;
			ConteudoContrato = conteudoContrato;
		}

		public LocacaoRelatorioContrato()
		{
		}

		public static LocacaoRelatorioContrato Criar()
		{
			return new LocacaoRelatorioContrato();
		}

		public LocacaoRelatorioContrato ComEmpresa(Pessoa empresa)
		{
			Empresa = empresa;
			return Build();
		}

		public LocacaoRelatorioContrato ComLocacao(LocacaoDto locacao)
		{
			Locacao = locacao;
			return Build();
		}

		public LocacaoRelatorioContrato ComConteudoContrato(string conteudo)
		{
			ConteudoContrato = conteudo;
			return Build();
		}

		protected LocacaoRelatorioContrato Build()
		{
			return new LocacaoRelatorioContrato(Empresa, Locacao, ConteudoContrato);
		}
	}
	public class ImpressoContratoLocacao : Relatorio
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

		private XRLabel xrLabel1;

		private XRLabel xrLabel2;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel dadosContrato;

		private XRLabel xrLabel24;

		private XRLabel dataEmissao;

		private ReportFooterBand ReportFooter;

		private XRLabel xrLabel7;

		private XRLabel xrLabel58;

		private XRLabel xrLabel19;

		private XRLabel xrLabel23;

		private XRLabel lblDescricao;

		private XRLabel xrLabel30;

		private XRLabel xrLabel44;

		private XRLabel xrLabel27;

		private XRLabel xrLabel26;

		private XRLabel xrLabel25;

		private XRLabel xrLabel17;

		private XRLabel xrLabel14;

		private GroupHeaderBand GroupHeader1;

		private XRLabel numeroLocacao;

		private XRLabel tituloNumeroLocacao;

		private XRLabel tituloContratado;

		private XRLabel contratado;

		private XRLabel tituloCpfCnpj;

		private XRLabel cpfCnpj;

		private XRLabel xrLabel3;

		private XRLabel xrLabel4;

		private XRLabel tituloEndereco;

		private XRLabel endereco;

		private XRLabel tituloTelefone;

		private XRLabel telefone;

		private XRPictureBox logotipoEmitente;

		private XRLabel painelLogo;

		public override string Descricao => "Contrato de locação";

		public override bool ExibirNoModuloRelatorios => false;

		public LocacaoRelatorioContrato ImpressoLocacao { get; set; }

		public ImpressoContratoLocacao(LocacaoDto impressoLocacao, string conteudoContrato)
		{
			ImpressoLocacao = LocacaoRelatorioContrato.Criar().ComLocacao(impressoLocacao).ComEmpresa(DadosSistema.EmpresaLogada)
				.ComConteudoContrato(conteudoContrato);
			InitializeComponent();
		}

		public override XtraReport Processar()
		{
			((XtraReportBase)this).DataSource = new List<LocacaoRelatorioContrato> { ImpressoLocacao };
			ExibirLogotipo();
			((XRControl)lblMensagemRodape).Text = "Emitido por DigiSat Tecnologia Ltda®  www.digisat.com.br";
			((XRControl)dataEmissao).Text = string.Format("{0}-{1}, {2}", ImpressoLocacao.Empresa.Municipio.Nome, ImpressoLocacao.Empresa.Municipio.Uf.Sigla, DateTime.Now.ToString("d 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")));
			((XtraReport)this).ShowPrintMarginsWarning = false;
			((XtraReport)this).CreateDocument();
			return (XtraReport)(object)this;
		}

		private void ExibirLogotipo()
		{
			if (HelperObject.IsNotNull((object)DadosSistema.EmpresaLogada.Imagem) && DadosSistema.EmpresaLogada.Imagem.Length > 10)
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(ImpressoLocacao.Empresa.Imagem));
				((XRControl)painelLogo).Visible = true;
				((XRControl)tituloNumeroLocacao).Location = new System.Drawing.Point(797, 161);
				((XRControl)numeroLocacao).Location = new System.Drawing.Point(1109, 161);
				((XRControl)numeroLocacao).Size = new System.Drawing.Size(739, 52);
				((XRControl)tituloContratado).Location = new System.Drawing.Point(797, 213);
				((XRControl)contratado).Location = new System.Drawing.Point(997, 213);
				((XRControl)contratado).Size = new System.Drawing.Size(851, 50);
				((XRControl)tituloCpfCnpj).Location = new System.Drawing.Point(797, 263);
				((XRControl)cpfCnpj).Location = new System.Drawing.Point(997, 263);
				((XRControl)cpfCnpj).Size = new System.Drawing.Size(537, 50);
				((XRControl)tituloTelefone).Location = new System.Drawing.Point(797, 313);
				((XRControl)telefone).Location = new System.Drawing.Point(997, 313);
				((XRControl)telefone).Size = new System.Drawing.Size(851, 50);
				((XRControl)tituloEndereco).Location = new System.Drawing.Point(797, 363);
				((XRControl)endereco).Location = new System.Drawing.Point(997, 363);
				((XRControl)endereco).Size = new System.Drawing.Size(851, 51);
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
			//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0422: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_054c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Expected O, but got Unknown
			//IL_0592: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0665: Unknown result type (might be due to invalid IL or missing references)
			//IL_068f: Unknown result type (might be due to invalid IL or missing references)
			//IL_073f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0745: Expected O, but got Unknown
			//IL_0784: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0853: Unknown result type (might be due to invalid IL or missing references)
			//IL_0859: Expected O, but got Unknown
			//IL_0899: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0961: Unknown result type (might be due to invalid IL or missing references)
			//IL_0967: Expected O, but got Unknown
			//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ba6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c61: Expected O, but got Unknown
			//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_107c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1082: Expected O, but got Unknown
			//IL_10c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_10eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_11a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_1268: Unknown result type (might be due to invalid IL or missing references)
			//IL_126e: Expected O, but got Unknown
			//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_12d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1381: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_1466: Unknown result type (might be due to invalid IL or missing references)
			//IL_146c: Expected O, but got Unknown
			//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_157f: Unknown result type (might be due to invalid IL or missing references)
			//IL_15a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1658: Unknown result type (might be due to invalid IL or missing references)
			//IL_165e: Expected O, but got Unknown
			//IL_169e: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1771: Unknown result type (might be due to invalid IL or missing references)
			//IL_179a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1838: Unknown result type (might be due to invalid IL or missing references)
			//IL_183e: Expected O, but got Unknown
			//IL_187e: Unknown result type (might be due to invalid IL or missing references)
			//IL_18a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_195d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1986: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a24: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2a: Expected O, but got Unknown
			//IL_1a6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a93: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b68: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c24: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c2a: Expected O, but got Unknown
			//IL_1c6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c93: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d67: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e16: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e1c: Expected O, but got Unknown
			//IL_1e5c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e85: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f65: Unknown result type (might be due to invalid IL or missing references)
			//IL_2020: Unknown result type (might be due to invalid IL or missing references)
			//IL_2026: Expected O, but got Unknown
			//IL_2066: Unknown result type (might be due to invalid IL or missing references)
			//IL_208f: Unknown result type (might be due to invalid IL or missing references)
			//IL_211d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2123: Expected O, but got Unknown
			//IL_2162: Unknown result type (might be due to invalid IL or missing references)
			//IL_2197: Unknown result type (might be due to invalid IL or missing references)
			//IL_2242: Unknown result type (might be due to invalid IL or missing references)
			//IL_226b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2328: Unknown result type (might be due to invalid IL or missing references)
			//IL_2351: Unknown result type (might be due to invalid IL or missing references)
			//IL_2400: Unknown result type (might be due to invalid IL or missing references)
			//IL_2455: Unknown result type (might be due to invalid IL or missing references)
			//IL_24cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_24e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_2521: Unknown result type (might be due to invalid IL or missing references)
			//IL_256c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2586: Unknown result type (might be due to invalid IL or missing references)
			//IL_25c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2624: Unknown result type (might be due to invalid IL or missing references)
			//IL_263e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2679: Unknown result type (might be due to invalid IL or missing references)
			//IL_27e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_2816: Unknown result type (might be due to invalid IL or missing references)
			//IL_2943: Unknown result type (might be due to invalid IL or missing references)
			//IL_296c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a26: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a4f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2af9: Unknown result type (might be due to invalid IL or missing references)
			//IL_2aff: Expected O, but got Unknown
			//IL_2b3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b67: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bf5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bfb: Expected O, but got Unknown
			//IL_2c3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c63: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Detail = new DetailBand();
			logotipoEmitente = new XRPictureBox();
			painelLogo = new XRLabel();
			numeroLocacao = new XRLabel();
			tituloNumeroLocacao = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLabel27 = new XRLabel();
			xrLabel26 = new XRLabel();
			xrLabel25 = new XRLabel();
			xrLabel17 = new XRLabel();
			xrLabel44 = new XRLabel();
			lblDescricao = new XRLabel();
			xrLabel30 = new XRLabel();
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
			dataEmissao = new XRLabel();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			toDescricao = new CalculatedField();
			xrCrossBandLine1 = new XRCrossBandLine();
			xrCrossBandLine2 = new XRCrossBandLine();
			xrCrossBandLine3 = new XRCrossBandLine();
			GroupHeader1 = new GroupHeaderBand();
			calculatedField1 = new CalculatedField();
			calculatedField2 = new CalculatedField();
			PageFooter = new PageFooterBand();
			lblMensagemRodape = new XRLabel();
			ReportFooter = new ReportFooterBand();
			xrLabel7 = new XRLabel();
			xrLabel58 = new XRLabel();
			xrLabel19 = new XRLabel();
			xrLabel23 = new XRLabel();
			bindingSource1 = new BindingSource(components);
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[32]
			{
				(XRControl)logotipoEmitente,
				(XRControl)painelLogo,
				(XRControl)numeroLocacao,
				(XRControl)tituloNumeroLocacao,
				(XRControl)xrLabel14,
				(XRControl)xrLabel27,
				(XRControl)xrLabel26,
				(XRControl)xrLabel25,
				(XRControl)xrLabel17,
				(XRControl)xrLabel44,
				(XRControl)lblDescricao,
				(XRControl)xrLabel30,
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
				(XRControl)xrLabel24
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 948.1373f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)logotipoEmitente).Borders = (BorderSide)0;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(171.6305f, 180.3873f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(606.4691f, 214.2292f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			((XRControl)painelLogo).Borders = (BorderSide)11;
			((XRControl)painelLogo).Dpi = 254f;
			((XRControl)painelLogo).LocationFloat = new PointFloat(151.9682f, 159.7916f);
			painelLogo.Multiline = true;
			((XRControl)painelLogo).Name = "painelLogo";
			((XRControl)painelLogo).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)painelLogo).SizeF = new SizeF(644.6527f, 255.0001f);
			((XRControl)painelLogo).StylePriority.UseBorders = false;
			((XRControl)painelLogo).Visible = false;
			((XRControl)numeroLocacao).Borders = (BorderSide)6;
			((XRControl)numeroLocacao).CanGrow = false;
			((XRControl)numeroLocacao).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.NumeroLocacao")
			});
			((XRControl)numeroLocacao).Dpi = 254f;
			((XRControl)numeroLocacao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)numeroLocacao).LocationFloat = new PointFloat(463.7928f, 162.7916f);
			((XRControl)numeroLocacao).Name = "numeroLocacao";
			((XRControl)numeroLocacao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)numeroLocacao).SizeF = new SizeF(1385.236f, 51.00002f);
			((XRControl)numeroLocacao).StylePriority.UseBorders = false;
			((XRControl)numeroLocacao).StylePriority.UseFont = false;
			((XRControl)numeroLocacao).StylePriority.UseTextAlignment = false;
			((XRControl)numeroLocacao).TextAlignment = (TextAlignment)16;
			((XRControl)tituloNumeroLocacao).Borders = (BorderSide)3;
			((XRControl)tituloNumeroLocacao).Dpi = 254f;
			((XRControl)tituloNumeroLocacao).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloNumeroLocacao).LocationFloat = new PointFloat(151.969f, 162.7916f);
			((XRControl)tituloNumeroLocacao).Name = "tituloNumeroLocacao";
			((XRControl)tituloNumeroLocacao).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloNumeroLocacao).SizeF = new SizeF(311.8238f, 51f);
			((XRControl)tituloNumeroLocacao).StylePriority.UseBorders = false;
			((XRControl)tituloNumeroLocacao).StylePriority.UseFont = false;
			((XRControl)tituloNumeroLocacao).StylePriority.UsePadding = false;
			((XRControl)tituloNumeroLocacao).StylePriority.UseTextAlignment = false;
			((XRControl)tituloNumeroLocacao).Text = "Número da locação:";
			((XRControl)tituloNumeroLocacao).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel14).Borders = (BorderSide)9;
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.ObjetoLocacao.Descricao")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(149.8912f, 905.1373f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(1381.024f, 42.99994f);
			((XRControl)xrLabel14).StylePriority.UseBorders = false;
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLabel14).StylePriority.UsePadding = false;
			((XRControl)xrLabel14).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel14).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).Borders = (BorderSide)12;
			((XRControl)xrLabel27).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.DataFinalLocacao", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel27).Dpi = 254f;
			((XRControl)xrLabel27).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel27).LocationFloat = new PointFloat(497.6187f, 638.9174f);
			((XRControl)xrLabel27).Name = "xrLabel27";
			((XRControl)xrLabel27).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel27).SizeF = new SizeF(1353.413f, 50.00006f);
			((XRControl)xrLabel27).StylePriority.UseBorders = false;
			((XRControl)xrLabel27).StylePriority.UseFont = false;
			((XRControl)xrLabel27).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel27).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel27).WordWrap = false;
			((XRControl)xrLabel26).Borders = (BorderSide)4;
			((XRControl)xrLabel26).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.DataInicialLocacao", "{0:dd/MM/yyyy HH:mm}")
			});
			((XRControl)xrLabel26).Dpi = 254f;
			((XRControl)xrLabel26).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel26).LocationFloat = new PointFloat(497.6208f, 588.9174f);
			((XRControl)xrLabel26).Name = "xrLabel26";
			((XRControl)xrLabel26).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel26).SizeF = new SizeF(1353.412f, 50.00003f);
			((XRControl)xrLabel26).StylePriority.UseBorders = false;
			((XRControl)xrLabel26).StylePriority.UseFont = false;
			((XRControl)xrLabel26).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel26).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel26).WordWrap = false;
			((XRControl)xrLabel25).Borders = (BorderSide)9;
			((XRControl)xrLabel25).Dpi = 254f;
			((XRControl)xrLabel25).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel25).LocationFloat = new PointFloat(149.8902f, 638.9174f);
			((XRControl)xrLabel25).Name = "xrLabel25";
			((XRControl)xrLabel25).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel25).SizeF = new SizeF(347.7284f, 50f);
			((XRControl)xrLabel25).StylePriority.UseBorders = false;
			((XRControl)xrLabel25).StylePriority.UseFont = false;
			((XRControl)xrLabel25).StylePriority.UsePadding = false;
			((XRControl)xrLabel25).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel25).Text = "Data final locação:";
			((XRControl)xrLabel25).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel17).Borders = (BorderSide)1;
			((XRControl)xrLabel17).Dpi = 254f;
			((XRControl)xrLabel17).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel17).LocationFloat = new PointFloat(149.8902f, 588.9174f);
			((XRControl)xrLabel17).Name = "xrLabel17";
			((XRControl)xrLabel17).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel17).SizeF = new SizeF(347.7258f, 50.00006f);
			((XRControl)xrLabel17).StylePriority.UseBorders = false;
			((XRControl)xrLabel17).StylePriority.UseFont = false;
			((XRControl)xrLabel17).StylePriority.UsePadding = false;
			((XRControl)xrLabel17).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel17).Text = "Data de inicio locação:";
			((XRControl)xrLabel17).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel44).Borders = (BorderSide)13;
			((XRControl)xrLabel44).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.PrecoUnitario", "{0:n2}")
			});
			((XRControl)xrLabel44).Dpi = 254f;
			((XRControl)xrLabel44).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel44).LocationFloat = new PointFloat(1530.915f, 905.1373f);
			((XRControl)xrLabel44).Name = "xrLabel44";
			((XRControl)xrLabel44).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel44).SizeF = new SizeF(318.0388f, 43f);
			((XRControl)xrLabel44).StylePriority.UseBorders = false;
			((XRControl)xrLabel44).StylePriority.UseFont = false;
			((XRControl)xrLabel44).StylePriority.UsePadding = false;
			((XRControl)xrLabel44).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel44).TextAlignment = (TextAlignment)64;
			((XRControl)lblDescricao).BackColor = System.Drawing.Color.Transparent;
			((XRControl)lblDescricao).Borders = (BorderSide)11;
			((XRControl)lblDescricao).Dpi = 254f;
			((XRControl)lblDescricao).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)lblDescricao).LocationFloat = new PointFloat(149.8902f, 855.1371f);
			((XRControl)lblDescricao).Name = "lblDescricao";
			((XRControl)lblDescricao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblDescricao).SizeF = new SizeF(1381.025f, 50.00006f);
			((XRControl)lblDescricao).StylePriority.UseBackColor = false;
			((XRControl)lblDescricao).StylePriority.UseBorders = false;
			((XRControl)lblDescricao).StylePriority.UseFont = false;
			((XRControl)lblDescricao).StylePriority.UseTextAlignment = false;
			((XRControl)lblDescricao).Text = "Objeto locado";
			((XRControl)lblDescricao).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel30).BackColor = System.Drawing.Color.Transparent;
			((XRControl)xrLabel30).Borders = (BorderSide)15;
			((XRControl)xrLabel30).Dpi = 254f;
			((XRControl)xrLabel30).Font = new Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel30).LocationFloat = new PointFloat(1530.915f, 855.1371f);
			((XRControl)xrLabel30).Name = "xrLabel30";
			((XRControl)xrLabel30).Padding = new PaddingInfo(5, 15, 0, 0, 254f);
			((XRControl)xrLabel30).SizeF = new SizeF(318.0391f, 50f);
			((XRControl)xrLabel30).StylePriority.UseBackColor = false;
			((XRControl)xrLabel30).StylePriority.UseBorders = false;
			((XRControl)xrLabel30).StylePriority.UseFont = false;
			((XRControl)xrLabel30).StylePriority.UsePadding = false;
			((XRControl)xrLabel30).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel30).Text = "Valor total";
			((XRControl)xrLabel30).TextAlignment = (TextAlignment)64;
			((XRControl)xrLabel15).Borders = (BorderSide)3;
			((XRControl)xrLabel15).Dpi = 254f;
			((XRControl)xrLabel15).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel15).LocationFloat = new PointFloat(149.8912f, 488.917f);
			((XRControl)xrLabel15).Name = "xrLabel15";
			((XRControl)xrLabel15).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel15).SizeF = new SizeF(226.1882f, 50.00003f);
			((XRControl)xrLabel15).StylePriority.UseBorders = false;
			((XRControl)xrLabel15).StylePriority.UseFont = false;
			((XRControl)xrLabel15).StylePriority.UsePadding = false;
			((XRControl)xrLabel15).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel15).Text = "Contratante:";
			((XRControl)xrLabel15).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel18).Borders = (BorderSide)2;
			((XRControl)xrLabel18).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.Pessoa.Nome")
			});
			((XRControl)xrLabel18).Dpi = 254f;
			((XRControl)xrLabel18).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel18).LocationFloat = new PointFloat(376.0794f, 488.9169f);
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
			((XRControl)xrLabel20).LocationFloat = new PointFloat(1050.852f, 488.9169f);
			((XRControl)xrLabel20).Name = "xrLabel20";
			((XRControl)xrLabel20).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel20).SizeF = new SizeF(167.7579f, 49.99994f);
			((XRControl)xrLabel20).StylePriority.UseBorders = false;
			((XRControl)xrLabel20).StylePriority.UseFont = false;
			((XRControl)xrLabel20).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel20).Text = "CPF/CNPJ:";
			((XRControl)xrLabel20).TextAlignment = (TextAlignment)32;
			((XRControl)xrLabel22).Borders = (BorderSide)6;
			((XRControl)xrLabel22).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.Pessoa.CpfCnpjFormatado")
			});
			((XRControl)xrLabel22).Dpi = 254f;
			((XRControl)xrLabel22).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel22).LocationFloat = new PointFloat(1218.61f, 488.9169f);
			((XRControl)xrLabel22).Name = "xrLabel22";
			((XRControl)xrLabel22).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel22).SizeF = new SizeF(632.4221f, 50.00003f);
			((XRControl)xrLabel22).StylePriority.UseBorders = false;
			((XRControl)xrLabel22).StylePriority.UseFont = false;
			((XRControl)xrLabel22).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel22).TextAlignment = (TextAlignment)16;
			((XRControl)tituloContratado).Borders = (BorderSide)1;
			((XRControl)tituloContratado).Dpi = 254f;
			((XRControl)tituloContratado).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloContratado).LocationFloat = new PointFloat(151.969f, 213.7916f);
			((XRControl)tituloContratado).Name = "tituloContratado";
			((XRControl)tituloContratado).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloContratado).SizeF = new SizeF(199.6965f, 50.00005f);
			((XRControl)tituloContratado).StylePriority.UseBorders = false;
			((XRControl)tituloContratado).StylePriority.UseFont = false;
			((XRControl)tituloContratado).StylePriority.UsePadding = false;
			((XRControl)tituloContratado).StylePriority.UseTextAlignment = false;
			((XRControl)tituloContratado).Text = "Comodante:";
			((XRControl)tituloContratado).TextAlignment = (TextAlignment)16;
			((XRControl)contratado).Borders = (BorderSide)4;
			((XRControl)contratado).CanGrow = false;
			((XRControl)contratado).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Empresa.Nome")
			});
			((XRControl)contratado).Dpi = 254f;
			((XRControl)contratado).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)contratado).LocationFloat = new PointFloat(351.6655f, 213.7916f);
			((XRControl)contratado).Name = "contratado";
			((XRControl)contratado).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)contratado).SizeF = new SizeF(1497.29f, 50.00005f);
			((XRControl)contratado).StylePriority.UseBorders = false;
			((XRControl)contratado).StylePriority.UseFont = false;
			((XRControl)contratado).StylePriority.UseTextAlignment = false;
			((XRControl)contratado).TextAlignment = (TextAlignment)16;
			((XRControl)tituloCpfCnpj).Borders = (BorderSide)1;
			((XRControl)tituloCpfCnpj).Dpi = 254f;
			((XRControl)tituloCpfCnpj).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloCpfCnpj).LocationFloat = new PointFloat(151.969f, 263.7916f);
			((XRControl)tituloCpfCnpj).Name = "tituloCpfCnpj";
			((XRControl)tituloCpfCnpj).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloCpfCnpj).SizeF = new SizeF(199.6965f, 50f);
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
			((XRControl)cpfCnpj).LocationFloat = new PointFloat(351.6655f, 263.7916f);
			((XRControl)cpfCnpj).Name = "cpfCnpj";
			((XRControl)cpfCnpj).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)cpfCnpj).SizeF = new SizeF(1182.887f, 50f);
			((XRControl)cpfCnpj).StylePriority.UseBorders = false;
			((XRControl)cpfCnpj).StylePriority.UseFont = false;
			((XRControl)cpfCnpj).StylePriority.UseTextAlignment = false;
			((XRControl)cpfCnpj).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel1).Borders = (BorderSide)0;
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel1).LocationFloat = new PointFloat(671.9565f, 538.9171f);
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
				new XRBinding("Text", (object)null, "Locacao.Pessoa.EnderecoPrincipal.EncurtadoToText")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(835.8787f, 538.9171f);
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
			((XRControl)xrLabel3).LocationFloat = new PointFloat(1534.553f, 263.7916f);
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
			((XRControl)xrLabel4).LocationFloat = new PointFloat(1602.154f, 263.7916f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(246.8757f, 50f);
			((XRControl)xrLabel4).StylePriority.UseBorders = false;
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel4).TextAlignment = (TextAlignment)16;
			((XRControl)tituloEndereco).Borders = (BorderSide)9;
			((XRControl)tituloEndereco).Dpi = 254f;
			((XRControl)tituloEndereco).Font = new Font("Segoe UI", 9f);
			((XRControl)tituloEndereco).LocationFloat = new PointFloat(151.969f, 363.7917f);
			((XRControl)tituloEndereco).Name = "tituloEndereco";
			((XRControl)tituloEndereco).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloEndereco).SizeF = new SizeF(199.6965f, 51.00003f);
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
			((XRControl)endereco).LocationFloat = new PointFloat(351.6655f, 363.7917f);
			((XRControl)endereco).Name = "endereco";
			((XRControl)endereco).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)endereco).SizeF = new SizeF(1497.366f, 51.00009f);
			((XRControl)endereco).StylePriority.UseBorders = false;
			((XRControl)endereco).StylePriority.UseFont = false;
			((XRControl)endereco).StylePriority.UseTextAlignment = false;
			((XRControl)endereco).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel8).Borders = (BorderSide)1;
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI", 9f);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(149.8912f, 538.9173f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(184.9988f, 50.00006f);
			((XRControl)xrLabel8).StylePriority.UseBorders = false;
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel8).StylePriority.UsePadding = false;
			((XRControl)xrLabel8).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel8).Text = "Telefone:";
			((XRControl)xrLabel8).TextAlignment = (TextAlignment)16;
			((XRControl)xrLabel9).Borders = (BorderSide)0;
			((XRControl)xrLabel9).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Locacao.Pessoa.TelefonePrincipal.ToText")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(334.89f, 538.9171f);
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
			((XRControl)tituloTelefone).LocationFloat = new PointFloat(151.969f, 313.7916f);
			((XRControl)tituloTelefone).Name = "tituloTelefone";
			((XRControl)tituloTelefone).Padding = new PaddingInfo(15, 5, 0, 0, 254f);
			((XRControl)tituloTelefone).SizeF = new SizeF(199.6965f, 50f);
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
			((XRControl)telefone).LocationFloat = new PointFloat(351.6655f, 313.7917f);
			((XRControl)telefone).Name = "telefone";
			((XRControl)telefone).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)telefone).SizeF = new SizeF(1497.366f, 50f);
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
			((XRControl)dadosContrato).LocationFloat = new PointFloat(149.8912f, 749.3289f);
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
			((XRControl)xrLabel24).LocationFloat = new PointFloat(51.5f, 30.78717f);
			((XRControl)xrLabel24).Name = "xrLabel24";
			((XRControl)xrLabel24).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel24).SizeF = new SizeF(1900f, 81.75001f);
			((XRControl)xrLabel24).StylePriority.UseBorders = false;
			((XRControl)xrLabel24).StylePriority.UseFont = false;
			((XRControl)xrLabel24).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel24).Text = "TERMO DE RESPONSABILIDADE POR EMPRÉSTIMO";
			((XRControl)xrLabel24).TextAlignment = (TextAlignment)32;
			((XRControl)dataEmissao).Borders = (BorderSide)0;
			((XRControl)dataEmissao).Dpi = 254f;
			((XRControl)dataEmissao).Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)dataEmissao).LocationFloat = new PointFloat(149.8902f, 238.9505f);
			((XRControl)dataEmissao).Name = "dataEmissao";
			((XRControl)dataEmissao).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)dataEmissao).SizeF = new SizeF(647.6614f, 50.00009f);
			((XRControl)dataEmissao).StylePriority.UseBorders = false;
			((XRControl)dataEmissao).StylePriority.UseFont = false;
			((XRControl)dataEmissao).StylePriority.UseTextAlignment = false;
			((XRControl)dataEmissao).TextAlignment = (TextAlignment)32;
			((XRControl)dataEmissao).WordWrap = false;
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
			((XRCrossBandControl)xrCrossBandLine3).EndBand = (Band)(object)GroupHeader1;
			((XRCrossBandControl)xrCrossBandLine3).EndPointFloat = new PointFloat(0f, 2.000038f);
			((XRControl)xrCrossBandLine3).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine3).Name = "xrCrossBandLine3";
			((XRCrossBandControl)xrCrossBandLine3).StartBand = (Band)(object)GroupHeader1;
			((XRCrossBandControl)xrCrossBandLine3).StartPointFloat = new PointFloat(0f, 0f);
			((XRControl)xrCrossBandLine3).WidthF = 2001.968f;
			((XRControl)GroupHeader1).Dpi = 254f;
			((XRControl)GroupHeader1).HeightF = 2f;
			((XRControl)GroupHeader1).Name = "GroupHeader1";
			((GroupBand)GroupHeader1).RepeatEveryPage = true;
			((DataContainerComponent)calculatedField1).DataMember = "Hospedagem.Itens";
			calculatedField1.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField1.Name = "calculatedField1";
			((DataContainerComponent)calculatedField2).DataMember = "Hospedagem.Servicos";
			calculatedField2.Expression = "Iif([Cancelado],'Sim' , 'Não' )";
			calculatedField2.Name = "calculatedField2";
			((XRControl)PageFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)lblMensagemRodape });
			((XRControl)PageFooter).Dpi = 254f;
			((XRControl)PageFooter).HeightF = 38.4108f;
			((XRControl)PageFooter).Name = "PageFooter";
			((XRControl)lblMensagemRodape).Borders = (BorderSide)10;
			((XRControl)lblMensagemRodape).BorderWidth = 1f;
			((XRControl)lblMensagemRodape).CanGrow = false;
			((XRControl)lblMensagemRodape).Dpi = 254f;
			((XRControl)lblMensagemRodape).Font = new Font("Times New Roman", 6.75f);
			((XRControl)lblMensagemRodape).LocationFloat = new PointFloat(3.000001f, 0f);
			lblMensagemRodape.Multiline = true;
			((XRControl)lblMensagemRodape).Name = "lblMensagemRodape";
			((XRControl)lblMensagemRodape).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)lblMensagemRodape).SizeF = new SizeF(1997.483f, 38.4108f);
			((XRControl)lblMensagemRodape).StylePriority.UseBorders = false;
			((XRControl)lblMensagemRodape).StylePriority.UseBorderWidth = false;
			((XRControl)lblMensagemRodape).StylePriority.UseFont = false;
			((XRControl)lblMensagemRodape).StylePriority.UseTextAlignment = false;
			((XRControl)lblMensagemRodape).TextAlignment = (TextAlignment)2;
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[5]
			{
				(XRControl)xrLabel7,
				(XRControl)xrLabel58,
				(XRControl)xrLabel19,
				(XRControl)xrLabel23,
				(XRControl)dataEmissao
			});
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 340.7722f;
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
				new XRBinding("Text", (object)null, "Locacao.Pessoa.Nome")
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
			bindingSource1.DataSource = typeof(LocacaoRelatorioContrato);
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[6]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)PageFooter,
				(Band)ReportFooter,
				(Band)GroupHeader1
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
			((XtraReport)this).PageWidth = 2102;
			((XtraReport)this).PaperKind = PaperKind.Custom;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}

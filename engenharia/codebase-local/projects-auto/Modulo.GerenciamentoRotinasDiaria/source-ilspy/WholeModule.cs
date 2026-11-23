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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DigisatServer.Application.Main.Dto.Auxiliares.Agendamentos;
using DigisatServer.Application.Main.Dto.Auxiliares.Configuracoes;
using DigisatServer.Application.Main.Dto.Auxiliares.MensagensWhatsappModelo;
using DigisatServer.Application.Main.Dto.Financeiros.PagamentosRecebimentos;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.FinanceirosModulo;
using DigisatServer.Infrastructure.Crosscutting.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Relatorios;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Agendamentos;
using DigisatServer.Infrastructure.Crosscutting.Model.Boletos;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp.Classes;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using iTextSharp.text.pdf;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.GerenciamentoRotinasDiaria.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.GerenciamentoRotinaDiaria")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.GerenciamentoRotinaDiaria")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.GerenciamentoRotinasDiaria
{
	[Module(ModuleName = "GerenciamentoRotinaDiariaModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("RecebimentoModulo")]
	[ModuleDependency("PagamentoModulo")]
	[ModuleDependency("MovimentacaoModulo")]
	public class GerenciamentoRotinaDiariaModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GerenciamentoRotinaDiariaModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GerenciamentoRotinasDiariaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GerenciamentoRotinaDiariaFichaView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.GerenciamentoRotinasDiaria.Views
{
	public class GerenciamentoRotinaDiariaFichaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GerenciamentoRotinaDiariaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaview.xaml", UriKind.Relative);
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
	public class GerenciamentoRotinasDiariaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GerenciamentoRotinasDiariaView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinasdiariaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GerenciamentoRotinasDiaria.Views.GerenciamentoRotinaDiariaFichaViewDetalhes
{
	public class VencimentoProdutosView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public VencimentoProdutosView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaviewdetalhes/vencimentoprodutosview.xaml", UriKind.Relative);
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
	public class AniversarianteView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public AniversarianteView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaviewdetalhes/aniversarianteview.xaml", UriKind.Relative);
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
	public class AgendamentoView : UserControl, IComponentConnector
	{
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaviewdetalhes/agendamentoview.xaml", UriKind.Relative);
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
	public class MovimentacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public MovimentacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaviewdetalhes/movimentacaoview.xaml", UriKind.Relative);
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
	public class FinanceiroView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public FinanceiroView()
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
				Uri resourceLocator = new Uri("/Modulo.GerenciamentoRotinasDiaria;component/views/gerenciamentorotinadiariafichaviewdetalhes/financeiroview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.GerenciamentoRotinasDiaria.ViewModels
{
	public class AgendamentoAbaViewModel : AbaViewModelBase
	{
		public AgendamentoAbaViewModel(IViewModelBase viewModel)
			: base("AGENDAMENTOS", "Agendamentos de hoje", viewModel)
		{
		}
	}
	public class VencimentoProdutosAbaViewModel : AbaViewModelBase
	{
		public VencimentoProdutosAbaViewModel(IViewModelBase viewModel)
			: base("Vencimento produtos", "Produtos vencidos ou a vencer", viewModel)
		{
		}
	}
	public class AniversarianteAbaViewModel : AbaViewModelBase
	{
		public AniversarianteAbaViewModel(IViewModelBase viewModel)
			: base("ANIVERSARIANTES", "Aniversariantes do dia", viewModel)
		{
		}
	}
	public class VencimentoProdutosViewModel : ViewModelBase<ProdutoLoteValidadeDto>
	{
		private readonly Action<int> _obterProdutosVencimento;

		public IEnumerable<ProdutoLoteValidadeDto> Produtos
		{
			get
			{
				return ((BindableBase)this).GetProperty<IEnumerable<ProdutoLoteValidadeDto>>((Expression<Func<IEnumerable<ProdutoLoteValidadeDto>>>)(() => Produtos));
			}
			set
			{
				((BindableBase)this).SetProperty<IEnumerable<ProdutoLoteValidadeDto>>((Expression<Func<IEnumerable<ProdutoLoteValidadeDto>>>)(() => Produtos), value);
			}
		}

		public int DiasAVencer
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => DiasAVencer));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => DiasAVencer), value);
			}
		}

		public ICommand AtualizarListaCommand { get; set; }

		public ICommand CompartilharWhatsAppCommand { get; set; }

		public VencimentoProdutosViewModel(Action<int> obterProdutosVencimento)
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			_obterProdutosVencimento = obterProdutosVencimento;
			DiasAVencer = 30;
			AtualizarListaCommand = (ICommand)new DelegateCommand((Action)Atualizar);
		}

		private void Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					_obterProdutosVencimento(DiasAVencer);
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
					((UpdateViewBindableBase)this).RecuperarFoco();
				}
			}).Start();
		}

		public void SetarVencimentoProdutos(IEnumerable<ProdutoLoteValidadeDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Produtos = result;
			});
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class AniversarianteViewModel : ViewModelBase<AniversarianteDto>
	{
		private readonly Action _obterAniversariantes;

		public Lista<AniversarianteDto> Aniversariantes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<AniversarianteDto>>((Expression<Func<Lista<AniversarianteDto>>>)(() => Aniversariantes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<AniversarianteDto>>((Expression<Func<Lista<AniversarianteDto>>>)(() => Aniversariantes), value);
			}
		}

		public ICommand AtualizarListaCommand { get; set; }

		public ICommand CompartilharWhatsAppCommand { get; set; }

		public AniversarianteViewModel(Action obterAniversariantes)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			_obterAniversariantes = obterAniversariantes;
			Aniversariantes = new Lista<AniversarianteDto>();
			AtualizarListaCommand = (ICommand)new DelegateCommand((Action)Atualizar);
			CompartilharWhatsAppCommand = (ICommand)new AsyncCommand((Func<Task>)IniciarConversaWhatsApp, (Func<bool>)PodeIniciarConversaWhatsApp, (bool?)null);
		}

		private bool PodeIniciarConversaWhatsApp()
		{
			return Aniversariantes?.PossuiItemSelecionado ?? false;
		}

		private async Task IniciarConversaWhatsApp()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			try
			{
				string text = RetornarNumeroWhatsAppDestinatario(Aniversariantes?.ItemSelecionado);
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				WhatsAppService obj = WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(text);
				ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
				object obj2;
				if (configuracaoDto == null)
				{
					obj2 = null;
				}
				else
				{
					AuxiliarDto auxiliar = configuracaoDto.Auxiliar;
					obj2 = ((auxiliar != null) ? auxiliar.MensagemWhatsApp.Valor : null);
				}
				await obj.ComMensagemWhatsApp((string)obj2).IniciarConversa().ContinueWith(delegate(Task task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
				});
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private string RetornarNumeroWhatsAppDestinatario(AniversarianteDto dto)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(dto.Telefone))
			{
				list.Add(dto.Telefone);
			}
			if (!string.IsNullOrEmpty(dto.WhatsApp))
			{
				list.Add(dto.WhatsApp);
			}
			if (list.Count > 1)
			{
				return DialogoFactory.MostrarDialogoComboBox<string>("Informe o celular com DDD e número.", "WhatsApp", (IEnumerable<string>)list, (string)null, true);
			}
			string text = string.Empty;
			if (list.Count == 1)
			{
				text = list.FirstOrDefault();
			}
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "WhatsApp", "Informe o celular com DDD e número.", (IIcone)null, false, false, true, text, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return text;
		}

		private void Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					_obterAniversariantes();
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
					((UpdateViewBindableBase)this).RecuperarFoco();
				}
			}).Start();
		}

		public bool PodeNavegarWhatsapp()
		{
			return Aniversariantes.PossuiItemSelecionado;
		}

		public void SetarAniversariantes(IEnumerable<AniversarianteDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Aniversariantes = new Lista<AniversarianteDto>();
				Aniversariantes.Adicionar(result);
				Aniversariantes.PosicionarNoPrimeiro();
			});
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class MovimentacaoAbaViewModel : AbaViewModelBase
	{
		public MovimentacaoAbaViewModel(IViewModelBase viewModel)
			: base("MOVIMENTAÇÕES", "Previsão para hoje", viewModel)
		{
		}
	}
	public class AgendamentoViewModel : ViewModelBase<AgendamentoDto>
	{
		private readonly Action _obterAgendamentos;

		public Lista<AgendamentoDto> Agendamentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<AgendamentoDto>>((Expression<Func<Lista<AgendamentoDto>>>)(() => Agendamentos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<AgendamentoDto>>((Expression<Func<Lista<AgendamentoDto>>>)(() => Agendamentos), value);
			}
		}

		public ISelecaoCommandParameter AgendamentoAtualizarParameter
		{
			get
			{
				SelecaoCommandParameter<AgendamentoDto> obj = new SelecaoCommandParameter<AgendamentoDto>("AgendamentoFichaAtualizarView", (Action<AgendamentoDto>)delegate
				{
					Atualizar();
				}, PodeEditarAgendamento);
				obj.AdicionarParametro("AgendamentoDto", (object)Agendamentos.ItemSelecionado);
				((UpdateViewBindableBase)this).RecuperarFoco();
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public bool PodeEditarAgendamento
		{
			get
			{
				if (DadosSistema.VerificarPermissao<Agendamento, Atualizar>())
				{
					return Agendamentos.PossuiItemSelecionado;
				}
				return false;
			}
		}

		public ICommand AtualizarListaCommand { get; set; }

		public ICommand CompartilharWhatsAppCommand { get; set; }

		public AgendamentoViewModel()
		{
		}

		public AgendamentoViewModel(Action obterAgendamentos)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			_obterAgendamentos = obterAgendamentos;
			Agendamentos = new Lista<AgendamentoDto>();
			AtualizarListaCommand = (ICommand)new DelegateCommand((Action)Atualizar);
			CompartilharWhatsAppCommand = (ICommand)new AsyncCommand((Func<Task>)IniciarConversaWhatsApp, (Func<bool>)PodeIniciarConversaWhatsApp, (bool?)null);
		}

		private bool PodeIniciarConversaWhatsApp()
		{
			return Agendamentos?.PossuiItemSelecionado ?? false;
		}

		private async Task IniciarConversaWhatsApp()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			try
			{
				string text = RetornarNumeroWhatsAppDestinatario(Agendamentos?.ItemSelecionado);
				if (string.IsNullOrEmpty(text))
				{
					return;
				}
				WhatsAppService obj = WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(text);
				ConfiguracaoDto configuracaoDto = DadosSistema.ConfiguracaoDto;
				object obj2;
				if (configuracaoDto == null)
				{
					obj2 = null;
				}
				else
				{
					AuxiliarDto auxiliar = configuracaoDto.Auxiliar;
					obj2 = ((auxiliar != null) ? auxiliar.MensagemWhatsApp.Valor : null);
				}
				await obj.ComMensagemWhatsApp((string)obj2).Compartilhar().ContinueWith(delegate(Task<string> task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
				});
			}
			finally
			{
				LoaderProcessoService.EsconderLoader();
			}
		}

		private string RetornarNumeroWhatsAppDestinatario(AgendamentoDto dto)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(dto.Telefone))
			{
				list.Add(dto.Telefone);
			}
			string text = string.Empty;
			if (list.Count == 1)
			{
				text = list.FirstOrDefault();
			}
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "WhatsApp", "Informe o celular com DDD e número.", (IIcone)null, false, false, true, text, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return text;
		}

		private void Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					_obterAgendamentos();
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

		public void SetarAgendamentos(IEnumerable<AgendamentoDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Agendamentos = new Lista<AgendamentoDto>();
				Agendamentos.Adicionar(result);
				Agendamentos.PosicionarNoPrimeiro();
				Agendamentos.AlterouItemSelecionadoEvent += AgendamentosOnAlterouItemSelecionadoEvent;
			});
		}

		private void AgendamentosOnAlterouItemSelecionadoEvent(object sender, AgendamentoDto agendamentoDto)
		{
			AtualizarSelecaoCommandParameter();
		}

		public void AtualizarSelecaoCommandParameter()
		{
			((BindableBase)this).RaisePropertiesChanged<ISelecaoCommandParameter, bool>((Expression<Func<ISelecaoCommandParameter>>)(() => AgendamentoAtualizarParameter), System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(AgendamentoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void Dispose()
		{
			Agendamentos.AlterouItemSelecionadoEvent -= AgendamentosOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class MovimentacaoViewModel : ViewModelBase<DocumentoAuxiliarPeriodoMovimentacaoDto>
	{
		private readonly Action _obterMovimentacoes;

		public bool PodeNavegarMovimentacaoSaida => Saidas.PossuiItemSelecionado;

		public bool PodeNavegarMovimentacaoEntrada => Entradas.PossuiItemSelecionado;

		public ISelecaoCommandParameter MovimentacaoSaidaAtualizarParameter
		{
			get
			{
				SelecaoCommandParameter<MovimentacaoBaseDto> obj = new SelecaoCommandParameter<MovimentacaoBaseDto>("MovimentacaoSaidaListaView", (Action<MovimentacaoBaseDto>)delegate
				{
				}, PodeNavegarMovimentacaoSaida);
				Lista<DocumentoAuxiliarPeriodoMovimentacaoDto> saidas = Saidas;
				object obj2;
				if (saidas == null)
				{
					obj2 = null;
				}
				else
				{
					DocumentoAuxiliarPeriodoMovimentacaoDto itemSelecionado = saidas.ItemSelecionado;
					obj2 = ((itemSelecionado != null) ? itemSelecionado.MovimentacaoReferencia : null);
				}
				obj.AdicionarParametro("InformacaoPesquisa", obj2);
				((UpdateViewBindableBase)this).RecuperarFoco();
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public ISelecaoCommandParameter MovimentacaoEntradaAtualizarParameter
		{
			get
			{
				SelecaoCommandParameter<MovimentacaoBaseDto> obj = new SelecaoCommandParameter<MovimentacaoBaseDto>("MovimentacaoEntradaListaView", (Action<MovimentacaoBaseDto>)delegate
				{
				}, PodeNavegarMovimentacaoEntrada);
				Lista<DocumentoAuxiliarPeriodoMovimentacaoDto> entradas = Entradas;
				object obj2;
				if (entradas == null)
				{
					obj2 = null;
				}
				else
				{
					DocumentoAuxiliarPeriodoMovimentacaoDto itemSelecionado = entradas.ItemSelecionado;
					obj2 = ((itemSelecionado != null) ? itemSelecionado.MovimentacaoReferencia : null);
				}
				obj.AdicionarParametro("InformacaoPesquisaPedidoCompra", obj2);
				((UpdateViewBindableBase)this).RecuperarFoco();
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public Lista<DocumentoAuxiliarPeriodoMovimentacaoDto> Entradas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>((Expression<Func<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>>)(() => Entradas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>((Expression<Func<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>>)(() => Entradas), value);
			}
		}

		public Lista<DocumentoAuxiliarPeriodoMovimentacaoDto> Saidas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>((Expression<Func<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>>)(() => Saidas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>((Expression<Func<Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>>>)(() => Saidas), value);
			}
		}

		public decimal TotalSaidas => Saidas.Itens.Sum((DocumentoAuxiliarPeriodoMovimentacaoDto x) => x.Total);

		public decimal TotalEntradas => Entradas.Itens.Sum((DocumentoAuxiliarPeriodoMovimentacaoDto x) => x.Total);

		public ICommand AtualizarListaCommand { get; set; }

		public MovimentacaoViewModel(Action obterMovimentacoes)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			_obterMovimentacoes = obterMovimentacoes;
			Entradas = new Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>();
			Saidas = new Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>();
			AtualizarListaCommand = (ICommand)new DelegateCommand((Action)Atualizar);
		}

		private void Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					_obterMovimentacoes();
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

		public void SetarMovimentacoesSaida(IEnumerable<DocumentoAuxiliarPeriodoMovimentacaoDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Saidas = new Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>();
				Saidas.Adicionar((IEnumerable<DocumentoAuxiliarPeriodoMovimentacaoDto>)result.OrderBy((DocumentoAuxiliarPeriodoMovimentacaoDto x) => x.DataPrevisao));
				Saidas.PosicionarNoPrimeiro();
				Saidas.AlterouItemSelecionadoEvent += SaidasOnAlterouItemSelecionadoEvent;
			});
		}

		private void SaidasOnAlterouItemSelecionadoEvent(object sender, DocumentoAuxiliarPeriodoMovimentacaoDto documentoAuxiliarPeriodoMovimentacaoDto)
		{
			AtualizarSelecaoCommandParameter();
		}

		public void SetarMovimentacoesEntrada(IEnumerable<DocumentoAuxiliarPeriodoMovimentacaoDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Entradas = new Lista<DocumentoAuxiliarPeriodoMovimentacaoDto>();
				Entradas.Adicionar((IEnumerable<DocumentoAuxiliarPeriodoMovimentacaoDto>)result.OrderBy((DocumentoAuxiliarPeriodoMovimentacaoDto x) => x.DataPrevisao));
				Entradas.PosicionarNoPrimeiro();
				Entradas.AlterouItemSelecionadoEvent += EntradasOnAlterouItemSelecionadoEvent;
			});
		}

		private void EntradasOnAlterouItemSelecionadoEvent(object sender, DocumentoAuxiliarPeriodoMovimentacaoDto documentoAuxiliarPeriodoMovimentacaoDto)
		{
			AtualizarSelecaoCommandParameter();
		}

		public void AtualizarSelecaoCommandParameter()
		{
			((BindableBase)this).RaisePropertiesChanged<ISelecaoCommandParameter, ISelecaoCommandParameter, decimal, decimal>((Expression<Func<ISelecaoCommandParameter>>)(() => MovimentacaoSaidaAtualizarParameter), System.Linq.Expressions.Expression.Lambda<Func<ISelecaoCommandParameter>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MovimentacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<decimal>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MovimentacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<decimal>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(MovimentacaoViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void Dispose()
		{
			Saidas.AlterouItemSelecionadoEvent -= SaidasOnAlterouItemSelecionadoEvent;
			Entradas.AlterouItemSelecionadoEvent -= EntradasOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class GerenciamentoRotinaDiariaFichaViewModel : FichaBase<CodigoDescricaoDto, FinanceiroViewModel>
	{
		public GerenciamentoRotinaDiariaFichaViewModel()
			: base("Gerenciamento de rotina diária (GRD)")
		{
			((NavigationBase)this).ParametroInformado += OnParametroInformadoResolucao;
		}

		private void OnParametroInformadoResolucao(object sender, NavigationParameters navigationParameters)
		{
			IResolucao resolucao = ((NavigationBase)this).Resolucao;
			if (resolucao != null)
			{
				ResolucaoBase obj = HelperObject.To<ResolucaoBase>((object)resolucao);
				if (obj != null)
				{
					obj.Maximizar();
				}
			}
			GerenciamentoRotinaDiariaFormulario gerenciamentoRotinaDiariaFormulario = base.Formulario as GerenciamentoRotinaDiariaFormulario;
			if (HelperObject.IsNull((object)gerenciamentoRotinaDiariaFormulario))
			{
				base.Formulario = (IFormulario)(object)new GerenciamentoRotinaDiariaFormulario();
				return;
			}
			gerenciamentoRotinaDiariaFormulario.ObterFinanceiro();
			gerenciamentoRotinaDiariaFormulario.ObterMovimentacao();
			if (DadosSistema.IsAdministrador)
			{
				gerenciamentoRotinaDiariaFormulario.ObterAgendamentos();
			}
		}

		public override void Dispose()
		{
			((NavigationBase)this).ParametroInformado -= OnParametroInformadoResolucao;
			base.Dispose();
		}
	}
	public class GerenciamentoRotinaDiariaFormulario : Formulario
	{
		private readonly FinanceiroViewModel _financeiroViewModel;

		private readonly MovimentacaoViewModel _movimentacaoViewModel;

		private readonly AgendamentoViewModel _agendamentoViewModel;

		private readonly AniversarianteViewModel _aniversarianteViewModel;

		private readonly VencimentoProdutosViewModel _vencimentoProdutosViewModel;

		public static bool Carregando { get; set; }

		public GerenciamentoRotinaDiariaFormulario()
		{
			_financeiroViewModel = new FinanceiroViewModel(ObterFinanceiro);
			_movimentacaoViewModel = new MovimentacaoViewModel(ObterMovimentacao);
			if (DadosSistema.IsAdministrador)
			{
				_agendamentoViewModel = new AgendamentoViewModel(ObterAgendamentos);
			}
			_aniversarianteViewModel = new AniversarianteViewModel(ObterAniversariantes);
			_vencimentoProdutosViewModel = new VencimentoProdutosViewModel(ObterVencimentoProdutos);
			CarregarDados();
		}

		private void CarregarDados()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					Carregando = true;
					ObterFinanceiro();
					ObterMovimentacao();
					if (DadosSistema.IsAdministrador)
					{
						ObterAgendamentos();
					}
					ObterAniversariantes();
					ObterVencimentoProdutos();
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
					Carregando = false;
					CriarAbas();
				}
			}).Start();
		}

		private void CriarAbas()
		{
			try
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)new FinanceiroAbaViewModel((IViewModelBase)(object)_financeiroViewModel));
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)new MovimentacaoAbaViewModel((IViewModelBase)(object)_movimentacaoViewModel));
					if (DadosSistema.IsAdministrador)
					{
						((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)new AgendamentoAbaViewModel((IViewModelBase)(object)_agendamentoViewModel));
					}
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)new AniversarianteAbaViewModel((IViewModelBase)(object)_aniversarianteViewModel));
					((Formulario)this).AdicionarAba((IAbaViewModelBase)(object)new VencimentoProdutosAbaViewModel((IViewModelBase)(object)_vencimentoProdutosViewModel));
				});
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				throw;
			}
		}

		public void ObterFinanceiro()
		{
			DateTime dataConfiguracao = DateTime.Now.AddDays(-DadosSistema.ConfiguracaoDto.Geral.DiasAtrasoGerenciamentoDiario.Valor);
			DateTime dataFinalConfiguracao = HelperDate.ToFinalDia(DateTime.Now.AddDays(DadosSistema.ConfiguracaoDto.Geral.DiasFuturoGerenciamentoDiario.Valor));
			IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto> result = DadosSistema.GetService().Canal<IPagamentoRecebimentoRelatorioModuleService, Task<IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>>>((Func<IPagamentoRecebimentoRelatorioModuleService, Task<IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>>>)((IPagamentoRecebimentoRelatorioModuleService service) => service.ObterListaDeRecebimentosEmAberto(dataConfiguracao, dataFinalConfiguracao, string.Empty, false, 99))).Result;
			IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto> result2 = DadosSistema.GetService().Canal<IPagamentoRecebimentoRelatorioModuleService, Task<IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>>>((Func<IPagamentoRecebimentoRelatorioModuleService, Task<IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>>>)((IPagamentoRecebimentoRelatorioModuleService service) => service.ObterListaDePagamentosEmAberto(dataConfiguracao, dataFinalConfiguracao, false, 99))).Result;
			_financeiroViewModel.SetarRecebimentos(result.Where((PagamentoRecebimentoSituacaoRelatorioDto x) => x.ValorQuitado.Equals(0m)));
			_financeiroViewModel.SetarPagamentos(result2.Where((PagamentoRecebimentoSituacaoRelatorioDto x) => x.ValorQuitado.Equals(0m)));
			_financeiroViewModel.AtualizarSelecaoCommandParameter();
		}

		public async void ObterMovimentacao()
		{
			DateTime dataConfiguracao = DateTime.Now.AddDays(-DadosSistema.ConfiguracaoDto.Geral.DiasAtrasoGerenciamentoDiario.Valor);
			DocumentosAuxiliaresPorPeriodoDto saidas = await DadosSistema.GetService().Canal<IMovimentacaoSaidaRelatorioModuleService, Task<DocumentosAuxiliaresPorPeriodoDto>>((Func<IMovimentacaoSaidaRelatorioModuleService, Task<DocumentosAuxiliaresPorPeriodoDto>>)((IMovimentacaoSaidaRelatorioModuleService service) => service.DavOuDavOsOuOrcamentoPendentePorVencimento(dataConfiguracao, HelperDate.ToFinalDia(DateTime.Now))));
			DocumentosAuxiliaresPorPeriodoDto val = await DadosSistema.GetService().Canal<IMovimentacaoEntradaRelatorioModuleService, Task<DocumentosAuxiliaresPorPeriodoDto>>((Func<IMovimentacaoEntradaRelatorioModuleService, Task<DocumentosAuxiliaresPorPeriodoDto>>)((IMovimentacaoEntradaRelatorioModuleService service) => service.PedidosCompraPendentePorVencimento(dataConfiguracao, HelperDate.ToFinalDia(DateTime.Now))));
			_movimentacaoViewModel.SetarMovimentacoesSaida(saidas.Movimentacoes);
			_movimentacaoViewModel.SetarMovimentacoesEntrada(val.Movimentacoes);
			_movimentacaoViewModel.AtualizarSelecaoCommandParameter();
		}

		public void ObterAgendamentos()
		{
			DateTime dataConfiguracao = DateTime.Now.AddDays(-DadosSistema.ConfiguracaoDto.Geral.DiasAtrasoGerenciamentoDiario.Valor);
			DateTime dataFinalConfiguracao = HelperDate.ToFinalDia(DateTime.Now.AddDays(DadosSistema.ConfiguracaoDto.Geral.DiasFuturoGerenciamentoDiario.Valor));
			IEnumerable<AgendamentoDto> result = DadosSistema.GetService().Canal<IAgendamentoModuleService, Task<IEnumerable<AgendamentoDto>>>((Func<IAgendamentoModuleService, Task<IEnumerable<AgendamentoDto>>>)((IAgendamentoModuleService service) => service.ObterListaAgendamentosPeriodo(DadosSistema.IdUsuario, string.Empty, dataConfiguracao, dataFinalConfiguracao))).Result;
			foreach (AgendamentoDto agendamentoDto in result)
			{
				agendamentoDto.DescricaoStatus = Situacao.Itens.FirstOrDefault((Situacao x) => x.Codigo.Equals(agendamentoDto.Status)).Descricao;
			}
			_agendamentoViewModel.SetarAgendamentos(result.Where((AgendamentoDto x) => x.Status != 7));
			_agendamentoViewModel.AtualizarSelecaoCommandParameter();
		}

		public void ObterAniversariantes()
		{
			IEnumerable<AniversarianteDto> result = DadosSistema.GetService().Canal<IGerenciamentoRotinaDiariaModuleService, IEnumerable<AniversarianteDto>>((Func<IGerenciamentoRotinaDiariaModuleService, IEnumerable<AniversarianteDto>>)((IGerenciamentoRotinaDiariaModuleService service) => service.ObterAniversariantesDoDia(DateTime.Now, 1)));
			_aniversarianteViewModel.SetarAniversariantes(result);
		}

		public void ObterVencimentoProdutos(int diasAVencer = 30)
		{
			Task<IEnumerable<ProdutoLoteValidadeDto>> task = DadosSistema.GetService().Canal<IProdutoServicoRelatorioModuleService, Task<IEnumerable<ProdutoLoteValidadeDto>>>((Func<IProdutoServicoRelatorioModuleService, Task<IEnumerable<ProdutoLoteValidadeDto>>>)((IProdutoServicoRelatorioModuleService x) => x.ObterProdutosComValidadeEmXDias(diasAVencer, true, true)));
			_vencimentoProdutosViewModel.SetarVencimentoProdutos(task.Result);
		}
	}
	public class GerenciamentoRotinasDiariaViewModel : ModuloBase
	{
		public GerenciamentoRotinasDiariaViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGerenciamentoRotinasDiaria, "GerenciamentoRotinaDiariaFichaView", (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown


		public override void Original()
		{
			if (!((ModuloBase)this).ViewAtual().GetType().Name.Contains("GerenciamentoRotinaDiariaFichaView"))
			{
				((ModuloBase)this).Resolucao.Reset();
			}
		}
	}
	public class FinanceiroAbaViewModel : AbaViewModelBase
	{
		public FinanceiroAbaViewModel(IViewModelBase viewModel)
			: base("FINANCEIRO", "Vencimentos imediatos", viewModel)
		{
		}
	}
	public class FinanceiroViewModel : ViewModelBase<PagamentoRecebimentoSituacaoRelatorioDto>
	{
		private readonly Action _obterPagamentosRecebimentos;

		public Lista<PagamentoRecebimentoSituacaoRelatorioDto> Recebimentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>((Expression<Func<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>>)(() => Recebimentos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>((Expression<Func<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>>)(() => Recebimentos), value);
			}
		}

		public Lista<PagamentoRecebimentoSituacaoRelatorioDto> Pagamentos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>((Expression<Func<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>>)(() => Pagamentos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>((Expression<Func<Lista<PagamentoRecebimentoSituacaoRelatorioDto>>>)(() => Pagamentos), value);
			}
		}

		public bool PodeNavegarRecebimentos => Recebimentos.PossuiItemSelecionado;

		public bool PodeNavegarPagamentos => Pagamentos.PossuiItemSelecionado;

		public ISelecaoCommandParameter RecebimentoAtualizarParameter
		{
			get
			{
				SelecaoCommandParameter<RecebimentoDto> obj = new SelecaoCommandParameter<RecebimentoDto>("RecebimentoListaView", (Action<RecebimentoDto>)delegate
				{
				}, PodeNavegarRecebimentos);
				Lista<PagamentoRecebimentoSituacaoRelatorioDto> recebimentos = Recebimentos;
				object obj2;
				if (recebimentos == null)
				{
					obj2 = null;
				}
				else
				{
					PagamentoRecebimentoSituacaoRelatorioDto itemSelecionado = recebimentos.ItemSelecionado;
					obj2 = ((itemSelecionado != null) ? itemSelecionado.Referencia.ToString() : null);
				}
				obj.AdicionarParametro("InformacaoPesquisa", obj2);
				((UpdateViewBindableBase)this).RecuperarFoco();
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public ISelecaoCommandParameter PagamentoAtualizarParameter
		{
			get
			{
				SelecaoCommandParameter<PagamentoDto> obj = new SelecaoCommandParameter<PagamentoDto>("PagamentoListaView", (Action<PagamentoDto>)delegate
				{
				}, PodeNavegarPagamentos);
				Lista<PagamentoRecebimentoSituacaoRelatorioDto> pagamentos = Pagamentos;
				object obj2;
				if (pagamentos == null)
				{
					obj2 = null;
				}
				else
				{
					PagamentoRecebimentoSituacaoRelatorioDto itemSelecionado = pagamentos.ItemSelecionado;
					obj2 = ((itemSelecionado != null) ? itemSelecionado.Referencia.ToString() : null);
				}
				obj.AdicionarParametro("InformacaoPesquisa", obj2);
				((UpdateViewBindableBase)this).RecuperarFoco();
				return (ISelecaoCommandParameter)(object)obj;
			}
		}

		public decimal TotalPagamentos => Pagamentos.Itens.Sum((PagamentoRecebimentoSituacaoRelatorioDto x) => x.ValorTotal);

		public decimal TotalRecebimentos => Recebimentos.Itens.Sum((PagamentoRecebimentoSituacaoRelatorioDto x) => x.ValorTotal);

		public ICommand AtualizarListaCommand { get; set; }

		public ICommand CompartilharWhatsAppCommand { get; set; }

		public FinanceiroViewModel(Action obterPagamentosRecebimentos)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Expected O, but got Unknown
			_obterPagamentosRecebimentos = obterPagamentosRecebimentos;
			Recebimentos = new Lista<PagamentoRecebimentoSituacaoRelatorioDto>();
			Pagamentos = new Lista<PagamentoRecebimentoSituacaoRelatorioDto>();
			AtualizarListaCommand = (ICommand)new DelegateCommand((Action)Atualizar);
			CompartilharWhatsAppCommand = (ICommand)new AsyncCommand((Func<Task>)IniciarConversaWhatsApp, (Func<bool>)PodeIniciarConversaWhatsApp, (bool?)null);
		}

		private void Atualizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			new Thread((ThreadStart)delegate
			{
				try
				{
					_obterPagamentosRecebimentos();
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

		private bool PodeIniciarConversaWhatsApp()
		{
			return Recebimentos?.PossuiItemSelecionado ?? false;
		}

		private async Task IniciarConversaWhatsApp()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			string telefone = RetornarNumeroWhatsAppDestinatario(Recebimentos?.ItemSelecionado);
			if (string.IsNullOrEmpty(telefone))
			{
				return;
			}
			RecebimentoDto val = DadosSistema.GetService().Canal<IRecebimentoModuleService, RecebimentoDto>((Func<IRecebimentoModuleService, RecebimentoDto>)delegate(IRecebimentoModuleService service)
			{
				Lista<PagamentoRecebimentoSituacaoRelatorioDto> recebimentos = Recebimentos;
				long? obj3;
				if (recebimentos == null)
				{
					obj3 = null;
				}
				else
				{
					PagamentoRecebimentoSituacaoRelatorioDto itemSelecionado = recebimentos.ItemSelecionado;
					obj3 = ((itemSelecionado != null) ? new long?(itemSelecionado.Referencia) : null);
				}
				return service.ObterPorReferencia(obj3 ?? 0);
			});
			List<DadosArquivoFtp> list = new List<DadosArquivoFtp>();
			if (val.IsBoleto)
			{
				string text = string.Format("Boleto_{0} {1}.pdf", string.Join("_", $"{Recebimentos.ItemSelecionado.Documento}-{Recebimentos.ItemSelecionado.Ordem}"), Guid.NewGuid());
				string path = BoletoService.ObterArquivoPdf(new List<IBoletoProxy> { DadosSistema.GetBoletoProxyService().RetornarBoletoProxy(val) });
				DadosArquivoFtp val2;
				if (DadosSistema.ConfiguracaoDto.Geral.ProtegerPdfExportadoComSenha.Valor && HelperString.RemoveCaracteresAlfanumerico(((PagamentoRecebimentoDto)val).Pessoa.CpfCnpj).Length >= 6)
				{
					string directoryName = Path.GetDirectoryName(path);
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
					string path2 = Path.Combine(directoryName, $"{fileNameWithoutExtension}_protegido.pdf");
					using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						using Stream stream2 = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.None);
						PdfEncryptor.Encrypt(new PdfReader(stream), stream2, 2, HelperString.CopiarTexto(HelperString.RemoveCaracteresAlfanumerico(((PagamentoRecebimentoDto)val).Pessoa.CpfCnpj), 6), HelperString.CopiarTexto(HelperString.RemoveCaracteresAlfanumerico(((PagamentoRecebimentoDto)val).Pessoa.CpfCnpj), 6), 2068);
					}
					val2 = DadosArquivoFtp.Criar().ComArquivoEnvio(Objeto<ArquivoEnvio>.Criar.ComNomeArquivo(text).ComArquivoBase64(Convert.ToBase64String(File.ReadAllBytes(path2)))).ComTipoArquivoWhatsApp((TipoArquivoWhatsApp)0)
						.ComTipoArquivoCompartilhando("Boleto");
				}
				else
				{
					val2 = DadosArquivoFtp.Criar().ComArquivoEnvio(Objeto<ArquivoEnvio>.Criar.ComNomeArquivo(text).ComArquivoBase64(Convert.ToBase64String(File.ReadAllBytes(path)))).ComTipoArquivoWhatsApp((TipoArquivoWhatsApp)0)
						.ComTipoArquivoCompartilhando("Boleto");
				}
				if (HelperObject.IsNotNull((object)val2))
				{
					list.Add(val2);
				}
			}
			object obj;
			if (!HelperString.IsNullOrEmpty(DadosSistema.ConfiguracaoDto.Financeiro.ModeloMensagemBoleto.Referencia))
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
						FinanceiroDto financeiro = configuracaoDto2.Financeiro;
						if (financeiro == null)
						{
							obj2 = null;
						}
						else
						{
							SelecaoDto modeloMensagemBoleto = financeiro.ModeloMensagemBoleto;
							obj2 = ((modeloMensagemBoleto != null) ? modeloMensagemBoleto.Referencia : null);
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
			string text2 = (string)obj;
			WhatsAppService conversa = WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(telefone)
				.ComArquivos((IEnumerable<DadosArquivoFtp>)list)
				.ComMensagemWhatsApp(text2);
			if (!list.Any())
			{
				await conversa.IniciarConversa().ContinueWith(delegate(Task task)
				{
					if (task.IsFaulted)
					{
						HelperException.ShowLastException(task.Exception, "", false, false);
					}
				});
			}
			else
			{
				await conversa.ComEnderecoServidor(DadosSistema.EnderecoServidorWhatsApp).ComCpfCnpjEmitente(DadosSistema.EmpresaLogada.CpfCnpj).Compartilhar()
					.ContinueWith(delegate(Task<string> task)
					{
						if (task.IsFaulted)
						{
							HelperException.ShowLastException(task.Exception, "", false, false);
						}
						else if (!string.IsNullOrEmpty(task.Result))
						{
							DialogoFactory.MostrarDialogoConfirma(task.Result, false, HorizontalAlignment.Center);
						}
						else
						{
							DialogoFactory.MostrarDialogoConfirma($"Compartilhado com sucesso com: {telefone}", false, HorizontalAlignment.Center);
						}
					});
			}
			LoaderProcessoService.EsconderLoader();
		}

		private string RetornarNumeroWhatsAppDestinatario(PagamentoRecebimentoSituacaoRelatorioDto dto)
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(dto.PessoaTelefone))
			{
				list.Add(dto.PessoaTelefone);
			}
			string text = string.Empty;
			if (list.Count == 1)
			{
				text = list.FirstOrDefault();
			}
			if (DialogoFactory.MostrarDialogoEntradaString(ref text, "WhatsApp", "Informe o celular com DDD e número.", (IIcone)null, false, false, true, text, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return text;
		}

		public void SetarRecebimentos(IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Recebimentos = new Lista<PagamentoRecebimentoSituacaoRelatorioDto>();
				Recebimentos.Adicionar((IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>)result.OrderBy((PagamentoRecebimentoSituacaoRelatorioDto x) => x.DataVencimento));
				Recebimentos.PosicionarNoPrimeiro();
				Recebimentos.AlterouItemSelecionadoEvent += RecebimentosOnAlterouItemSelecionadoEvent;
			});
		}

		private void RecebimentosOnAlterouItemSelecionadoEvent(object sender, PagamentoRecebimentoSituacaoRelatorioDto pagamentoRecebimentoSituacaoRelatorioDto)
		{
			AtualizarSelecaoCommandParameter();
		}

		public void SetarPagamentos(IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto> result)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Pagamentos = new Lista<PagamentoRecebimentoSituacaoRelatorioDto>();
				Pagamentos.Adicionar((IEnumerable<PagamentoRecebimentoSituacaoRelatorioDto>)result.OrderBy((PagamentoRecebimentoSituacaoRelatorioDto x) => x.DataVencimento));
				Pagamentos.PosicionarNoPrimeiro();
				Pagamentos.AlterouItemSelecionadoEvent += PagamentosOnAlterouItemSelecionadoEvent;
			});
		}

		private void PagamentosOnAlterouItemSelecionadoEvent(object sender, PagamentoRecebimentoSituacaoRelatorioDto pagamentoRecebimentoSituacaoRelatorioDto)
		{
			AtualizarSelecaoCommandParameter();
		}

		public void AtualizarSelecaoCommandParameter()
		{
			((BindableBase)this).RaisePropertiesChanged<ISelecaoCommandParameter, ISelecaoCommandParameter, decimal, decimal>((Expression<Func<ISelecaoCommandParameter>>)(() => RecebimentoAtualizarParameter), System.Linq.Expressions.Expression.Lambda<Func<ISelecaoCommandParameter>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(FinanceiroViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<decimal>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(FinanceiroViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()), System.Linq.Expressions.Expression.Lambda<Func<decimal>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(FinanceiroViewModel)), (MethodInfo)MethodBase.GetMethodFromHandle((RuntimeMethodHandle)/*OpCode not supported: LdMemberToken*/)), Array.Empty<ParameterExpression>()));
		}

		public override void ProjectedAsViewModel()
		{
		}

		public override void Dispose()
		{
			Pagamentos.AlterouItemSelecionadoEvent -= PagamentosOnAlterouItemSelecionadoEvent;
			Recebimentos.AlterouItemSelecionadoEvent -= RecebimentosOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.GerenciamentoRotinasDiaria.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.GerenciamentoRotinasDiaria.Properties.Resources", typeof(Resources).Assembly);
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

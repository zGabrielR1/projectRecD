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
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Auxiliares.Restaurantes;
using DigisatServer.Application.Main.Dto.Cadastros.Fretes;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Movimentacoes;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.AuxiliaresModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Distributed.Wcf.Main.Contracts.SaidasModulo;
using DigisatServer.Infrastructure.Crosscutting.Config;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Classificacoes;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Municipios;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.Paises;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros.UFs;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Event;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model;
using DigisatServer.Infrastructure.Crosscutting.Model.SituacoesEntrega;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Projection;
using DigisatServer.Infrastructure.Crosscutting.Right;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDigisatWhatsApp.Classes;
using DigisatServer.Infrastructure.Crosscutting.Service.ConsultaCep;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Events;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.Models.Cores;
using Otus.Infraestrutura.Models.Sessoes;
using Otus.Infraestrutura.Services;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Modulo.EntregasDelivery.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.EntregasDelivery")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.EntregasDelivery")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.EntregasDelivery
{
	[Module(ModuleName = "EntregaDeliveryModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("FreteModulo")]
	public class EntregaDeliveryModulo : IModule
	{
		private readonly IUnityContainer _container;

		public EntregaDeliveryModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<EntregasDeliveryFichaPrincipalView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<EntregasDeliveryView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.EntregasDelivery.Views
{
	public class EntregasDeliveryFichaPrincipalView : CustomUserControl, IComponentConnector
	{
		internal DXTabControl TabControlBase;

		private bool _contentLoaded;

		public EntregasDeliveryFichaPrincipalView()
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
				Uri resourceLocator = new Uri("/Modulo.EntregasDelivery;component/views/entregasdeliveryfichaprincipalview.xaml", UriKind.Relative);
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
	public class EntregasDeliveryView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EntregasDeliveryView()
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
				Uri resourceLocator = new Uri("/Modulo.EntregasDelivery;component/views/entregasdeliveryview.xaml", UriKind.Relative);
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
	public class EntregasView : CustomUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public EntregasView()
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
				Uri resourceLocator = new Uri("/Modulo.EntregasDelivery;component/views/entregasview.xaml", UriKind.Relative);
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
	public class MapasView : UserControl, IComponentConnector
	{
		internal ButtonInfo ParameterEntregador;

		internal ScrollViewer ScrollViewer;

		private bool _contentLoaded;

		public MapasView()
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
				Uri resourceLocator = new Uri("/Modulo.EntregasDelivery;component/views/mapasview.xaml", UriKind.Relative);
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
			switch (connectionId)
			{
			case 1:
				ParameterEntregador = (ButtonInfo)target;
				break;
			case 2:
				ScrollViewer = (ScrollViewer)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.EntregasDelivery.ViewModels
{
	public class AlterarFocoEvent
	{
	}
	public class CadastroRotaViewModel : NavigationBase
	{
		private readonly IWService _service;

		private readonly ISessao _sessao;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		public Lista<IViewModelDtoBase> OpcoesPagamento
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<IViewModelDtoBase>>((Expression<Func<Lista<IViewModelDtoBase>>>)(() => OpcoesPagamento));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<IViewModelDtoBase>>((Expression<Func<Lista<IViewModelDtoBase>>>)(() => OpcoesPagamento), value);
			}
		}

		public Func<decimal> RetornarTotalComFrete
		{
			get
			{
				AtualizarTotal();
				return () => TotalComFrete;
			}
		}

		public bool AdicionarFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => AdicionarFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => AdicionarFrete), value);
			}
		}

		public decimal TotalComFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TotalComFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TotalComFrete), value);
			}
		}

		public Lista<FreteDto> Fretes
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<FreteDto>>((Expression<Func<Lista<FreteDto>>>)(() => Fretes));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<FreteDto>>((Expression<Func<Lista<FreteDto>>>)(() => Fretes), value);
			}
		}

		public string IdCliente { get; set; }

		public TimeSpan Versao { get; set; }

		[Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
		public string Cliente
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cliente));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cliente), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => Valido));
				});
			}
		}

		public string Telefone
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Telefone));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Telefone), value);
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

		public decimal Total
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => Total));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => Total), value, (Action)OnAlterouTotal);
			}
		}

		public Lista<ItemEntregaDeliveryDto> Itens { get; set; }

		public Lista<CodigoDescricao> Prioridades { get; set; }

		public DateTime DataEmissao
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataEmissao));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataEmissao), value);
			}
		}

		[Range(1.0, 9.223372036854776E+18, ErrorMessage = "O campo 'Número' deve ser maior que zero")]
		public long Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<long>((Expression<Func<long>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<long>((Expression<Func<long>>)(() => Numero), value, (Action)delegate
				{
					((BindableBase)this).RaisePropertyChanged<bool>((Expression<Func<bool>>)(() => Valido));
				});
			}
		}

		public DateTime DataHoraEntregaRetirada
		{
			get
			{
				return ((BindableBase)this).GetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEntregaRetirada));
			}
			set
			{
				((BindableBase)this).SetProperty<DateTime>((Expression<Func<DateTime>>)(() => DataHoraEntregaRetirada), value);
			}
		}

		public DadosEnderecoViewModel Endereco
		{
			get
			{
				return ((BindableBase)this).GetProperty<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => Endereco));
			}
			set
			{
				((BindableBase)this).SetProperty<DadosEnderecoViewModel>((Expression<Func<DadosEnderecoViewModel>>)(() => Endereco), value);
			}
		}

		public bool Valido
		{
			get
			{
				if (Numero > 0 && !string.IsNullOrEmpty(Cliente) && DataEmissao > DateTime.MinValue)
				{
					return ((ViewModelValidationBase)Endereco).Valido;
				}
				return false;
			}
		}

		public bool FocoNoTelefone
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoTelefone));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoTelefone), value);
			}
		}

		public bool FocoNoFrete
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoFrete));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoFrete), value);
			}
		}

		public bool FocoNoCep
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCep));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCep), value);
			}
		}

		public bool FocoNoCliente
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCliente));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoNoCliente), value);
			}
		}

		public SelecaoCommandParameter<PessoaDto> BuscarClienteCommandParameter => new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)SelecionouCliente, true);

		public ICommand PesquisarClienteCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarCliente);

		public ISelecaoCommandParameter FreteCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<FreteDto>("FreteListaView", (Action<FreteDto>)delegate(FreteDto dto)
		{
			if (Fretes.Existe((Expression<Func<FreteDto, bool>>)((FreteDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id)))
			{
				Fretes.Remover(Fretes.Itens.FirstOrDefault((FreteDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id));
			}
			Fretes.Adicionar(dto);
		}, true);

		public CadastroRotaViewModel(IWService service, ISessao sessao, IImpressoraNaoFiscalService impressoraNaoFiscalService, IEventPublisher eventPublisher, EntregaDeliveryDto entregaDelivery = null)
			: base("")
		{
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Expected O, but got Unknown
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Expected O, but got Unknown
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Expected O, but got Unknown
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Expected O, but got Unknown
			_service = service;
			_sessao = sessao;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			DataEmissao = DateTime.Now;
			Prioridades = new Lista<CodigoDescricao>();
			Itens = new Lista<ItemEntregaDeliveryDto>();
			Prioridades.Adicionar(new CodigoDescricao(1, "Baixa"));
			Prioridades.Adicionar(new CodigoDescricao(2, "Normal"));
			Prioridades.Adicionar(new CodigoDescricao(3, "Alta"));
			Prioridades.Posicionar((Expression<Func<CodigoDescricao, bool>>)((CodigoDescricao x) => x.Codigo == 2));
			PaginaEntityDto result = service.Canal<IFreteModuleService, Task<PaginaEntityDto>>((Func<IFreteModuleService, Task<PaginaEntityDto>>)((IFreteModuleService a) => ((IPesquisarAtivos)a).PesquisarAtivosAsync(string.Empty, (short)1, short.MaxValue))).Result;
			Fretes = new Lista<FreteDto>();
			Fretes.Adicionar(result.Lista.Cast<FreteDto>());
			Fretes.PosicionarNoPrimeiro();
			Fretes.AlterouItemSelecionadoEvent += FretesOnAlterouItemSelecionadoEvent;
			eventPublisher.GetEvent<AlterarFocoEvent>((Action<AlterarFocoEvent>)AlterarFoco);
			OpcoesPagamento = new Lista<IViewModelDtoBase>();
			OpcoesPagamento.Adicionar((IViewModelDtoBase[])(object)new IViewModelDtoBase[4]
			{
				(IViewModelDtoBase)new Dinheiro(RetornarTotalComFrete),
				(IViewModelDtoBase)new Cartao(),
				(IViewModelDtoBase)new Pix(),
				(IViewModelDtoBase)new Outros()
			});
			OpcoesPagamento.PosicionarNoPrimeiro();
			if (entregaDelivery == null)
			{
				Endereco = new DadosEnderecoViewModel(new EnderecoDto());
				PreencherCepEMunicipioComDadosEmitente();
				return;
			}
			Versao = entregaDelivery.VersaoCliente;
			IdCliente = entregaDelivery.IdCliente;
			Cliente = entregaDelivery.Nome;
			Endereco = new DadosEnderecoViewModel(entregaDelivery.Endereco);
			PreencherCepEMunicipioComDadosEmitente();
			Telefone = entregaDelivery.Telefone;
			Itens.Adicionar(entregaDelivery.ItensDelivery);
			Total = entregaDelivery.Total + entregaDelivery.TotalAdicionais;
			Numero = entregaDelivery.Numero;
			DataHoraEntregaRetirada = entregaDelivery.DataHoraEntregaRetirada;
			if (entregaDelivery.DescricaoTipoPagamentoDelivery.Equals("Cartão"))
			{
				OpcoesPagamento.PosicionarNoUltimo();
			}
			else
			{
				OpcoesPagamento.PosicionarNoPrimeiro();
			}
			((IViewModelBase)OpcoesPagamento.ItemSelecionado).ProjectedAsViewModel();
			AtualizarTotal();
		}

		private void PreencherCepEMunicipioComDadosEmitente()
		{
			if (HelperObject.Is<MunicipioDtoNull>((object)Endereco.Municipio))
			{
				Endereco.Municipio = MunicipioProjection.ProjectedAsDto(DadosSistema.EmpresaLogada.Municipio);
			}
			if (HelperString.IsNullOrEmpty(Endereco.Cep))
			{
				Endereco.Cep = DadosSistema.EmpresaLogada.Cep;
			}
		}

		public void AtualizarTotal()
		{
			if (AdicionarFrete)
			{
				decimal total = Total;
				Lista<FreteDto> fretes = Fretes;
				decimal? obj;
				if (fretes == null)
				{
					obj = null;
				}
				else
				{
					FreteDto itemSelecionado = fretes.ItemSelecionado;
					obj = ((itemSelecionado != null) ? new decimal?(itemSelecionado.Valor) : null);
				}
				TotalComFrete = total + (obj ?? 0m);
			}
			else
			{
				TotalComFrete = Total;
			}
		}

		private void FretesOnAlterouItemSelecionadoEvent(object sender, FreteDto freteDto)
		{
			AtualizarTotal();
		}

		public void GravarRota()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Expected O, but got Unknown
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Expected O, but got Unknown
			//IL_0246: Unknown result type (might be due to invalid IL or missing references)
			//IL_024c: Expected O, but got Unknown
			try
			{
				EntregaDeliveryDto val = new EntregaDeliveryDto
				{
					Nome = Cliente,
					Telefone = Telefone,
					Prioridade = new CodigoDescricaoDto
					{
						Codigo = Prioridades.ItemSelecionado.Codigo,
						Descricao = Prioridades.ItemSelecionado.Descricao
					},
					Observacao = Observacao,
					DataHoraEmissao = DataEmissao,
					Numero = Numero,
					SituacaoEntrega = (SituacaoEntregaDto)new AguardandoEntregaDto(),
					ItensDelivery = Itens.Itens
				};
				decimal totalFrete;
				if (!AdicionarFrete)
				{
					totalFrete = 0m;
				}
				else
				{
					Lista<FreteDto> fretes = Fretes;
					decimal? obj;
					if (fretes == null)
					{
						obj = null;
					}
					else
					{
						FreteDto itemSelecionado = fretes.ItemSelecionado;
						obj = ((itemSelecionado != null) ? new decimal?(itemSelecionado.Valor) : null);
					}
					totalFrete = obj ?? 0m;
				}
				val.TotalFrete = totalFrete;
				val.Total = Total;
				val.DescricaoTipoPagamentoDelivery = HelperObject.To<IBasePagamento>((object)OpcoesPagamento.ItemSelecionado)?.Descricao ?? "Não informado";
				val.DataHoraEntregaRetirada = DataHoraEntregaRetirada;
				EntregaDeliveryDto dto = val;
				if (dto.DescricaoTipoPagamentoDelivery.Equals("Dinheiro"))
				{
					dto.ValorParaCalculoTroco = HelperObject.To<Dinheiro>((object)OpcoesPagamento.ItemSelecionado)?.ValorParaCalculoTroco ?? 0m;
				}
				EnderecoDto endereco = new EnderecoDto();
				((ViewModelPartialBase<EnderecoDto>)Endereco).AtualizarDto<EnderecoDto>(ref endereco);
				dto.Endereco = endereco;
				_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
				{
					service.AdicionarEntregaAsync(dto);
				});
				if (DialogoFactory.MostrarDialogoSimNao("Entrega cadastrada com sucesso. Deseja imprimir?", false, 0, HorizontalAlignment.Center) != MessageBoxResult.Yes)
				{
					return;
				}
				IWService service2 = _service;
				EntregaDeliveryDto obj2 = dto;
				decimal obj3;
				if (!AdicionarFrete)
				{
					obj3 = 0m;
				}
				else
				{
					Lista<FreteDto> fretes2 = Fretes;
					decimal? obj4;
					if (fretes2 == null)
					{
						obj4 = null;
					}
					else
					{
						FreteDto itemSelecionado2 = fretes2.ItemSelecionado;
						obj4 = ((itemSelecionado2 != null) ? new decimal?(itemSelecionado2.Valor) : null);
					}
					obj3 = obj4 ?? 0m;
				}
				ImpressaoPedidoService val2 = new ImpressaoPedidoService(service2, obj2, obj3, _sessao, _impressoraNaoFiscalService);
				for (int i = 0; i < DadosSistema.ConfiguracaoDto.Saida.NumeroViasDelivery.Valor; i++)
				{
					val2.ImprimirPedido();
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void OnAlterouTotal()
		{
			AdicionarFrete = !DadosSistema.ConfiguracaoDto.Saida.DeliveryEntregaGratis.Valor || Total < DadosSistema.ConfiguracaoDto.Saida.ValorMinimoDeliveryEntregaGratis.Valor;
		}

		private void SelecionouCliente(PessoaDto pessoa)
		{
			if (!(pessoa.Cliente.ValorEmAtraso > 0m) || ((!DadosSistema.ConfiguracaoDto.Saida.AlertarClienteInadimplente.Valor || DialogoFactory.MostrarDialogoConfirmaInformacaoOculta(pessoa.Cliente.AvisoValorEmAtraso, "Situação 1. Verifique!", false) == MessageBoxResult.OK) && DadosSistema.AutenticacaoService.Instancia.VerificarPermissao<MovimentacaoSaida, PermitirVenderParaInadimplente>(0m, (string)null) != null))
			{
				IdCliente = ((EntityBaseDto)pessoa).Id;
				Cliente = pessoa.Nome;
				object telefone;
				if (!pessoa.Celulares.Any())
				{
					telefone = (HelperObject.Is<TelefoneDtoNull>((object)pessoa.TelefoneWhatsApp) ? pessoa.TelefonePrincipal.Numero : pessoa.TelefoneWhatsApp.Numero);
				}
				else
				{
					TelefoneDto obj = pessoa.Celulares.FirstOrDefault();
					telefone = ((obj != null) ? obj.Numero : null);
				}
				Telefone = (string)telefone;
				Versao = ((EntityVersaoDto)pessoa).Versao;
				Endereco = new DadosEnderecoViewModel(pessoa.EnderecoPrincipal);
				ControleDeFoco();
			}
		}

		private async void PesquisarCliente(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(Cliente, (TipoPessoaCadastro)0, (Action<PessoaDto>)SelecionouCliente, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, (ValidateParameterPessoa<PessoaDto>)null, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void ControleDeFoco()
		{
			if (string.IsNullOrEmpty(Cliente))
			{
				FocoNoCliente = !FocoNoCliente;
			}
			else if (string.IsNullOrEmpty(Telefone))
			{
				FocoNoTelefone = !FocoNoTelefone;
			}
			else if (!((ViewModelValidationBase)Endereco).Valido)
			{
				FocoNoCep = !FocoNoCep;
			}
			else
			{
				FocoNoFrete = !FocoNoFrete;
			}
		}

		private void AlterarFoco(AlterarFocoEvent obj)
		{
			ControleDeFoco();
		}

		public override void Dispose()
		{
			Fretes.AlterouItemSelecionadoEvent -= FretesOnAlterouItemSelecionadoEvent;
			((NavigationBase)this).Dispose();
		}
	}
	public class ControleDeEntregaFormulario : Formulario
	{
		private readonly IWService _service;

		private readonly ISessaoService _sessaoService;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IEventPublisher _eventPublisher;

		private readonly INotificacao _notificacao;

		private MapasViewModel _mapas;

		private EntregasViewModel _entregas;

		public ControleDeEntregaFormulario(IWService service, ISessaoService sessaoService, IImpressoraNaoFiscalService impressoraNaoFiscalService, IEventPublisher eventPublisher, INotificacao notificacao)
		{
			_service = service;
			_sessaoService = sessaoService;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_eventPublisher = eventPublisher;
			_notificacao = notificacao;
			CriarViewModels();
			AdicionarAbas();
		}

		private void CriarViewModels()
		{
			_entregas = new EntregasViewModel(_service, _impressoraNaoFiscalService, _sessaoService, _eventPublisher, _notificacao);
			_mapas = new MapasViewModel();
			((Formulario)this).AlterouAbaSelecionadaEvent += OnAlterouAbaSelecionadaEvent;
		}

		private async void OnAlterouAbaSelecionadaEvent(object sender, IAbaViewModelBase abaViewModelBase)
		{
			if (HelperObject.Is<MapasAbaViewModel>((object)abaViewModelBase))
			{
				await _mapas.AtualizarListaEntregas();
			}
		}

		private void AdicionarAbas()
		{
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1]
			{
				new EntregasAbaViewModel((IViewModelBase)(object)_entregas)
			});
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1]
			{
				new MapasAbaViewModel((IViewModelBase)(object)_mapas)
			});
		}

		public override void Dispose()
		{
			((Formulario)this).AlterouAbaSelecionadaEvent -= OnAlterouAbaSelecionadaEvent;
			((Formulario)this).Dispose();
		}
	}
	public class DadosEnderecoViewModel : ViewModelPartialBase<EnderecoDto>
	{
		private readonly IConsultaCepService _consultaCepService;

		public bool InformouCepComLogradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => InformouCepComLogradouro), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Logradouro' é obrigatório")]
		public string Logradouro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Logradouro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Logradouro), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'CEP' é obrigatório")]
		public string Cep
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Cep));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Cep), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Número' é obrigatório")]
		public string Numero
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Numero));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Numero), value);
			}
		}

		public string Complemento
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Complemento));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Complemento), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Campo 'Bairro' é obrigatório")]
		public string Bairro
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Bairro));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Bairro), value);
			}
		}

		public MunicipioDto Municipio
		{
			get
			{
				return ((BindableBase)this).GetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio));
			}
			set
			{
				((BindableBase)this).SetProperty<MunicipioDto>((Expression<Func<MunicipioDto>>)(() => Municipio), value, (Action)delegate
				{
					MunicipioDescricao = Municipio.Nome;
				});
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Município' é obrigatório.")]
		public string MunicipioDescricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => MunicipioDescricao));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => MunicipioDescricao), value);
			}
		}

		public ISelecaoCommandParameter BuscarMunicipioCommandParameter { get; set; }

		public ICommand PesquisarCepCommand { get; set; }

		public DadosEnderecoViewModel(EnderecoDto dto)
			: base(dto)
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			BuscarMunicipioCommandParameter = (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<MunicipioDto>("MunicipioListaView", (Action<MunicipioDto>)delegate(MunicipioDto municipio)
			{
				Municipio = municipio;
			}, true);
			PesquisarCepCommand = (ICommand)new DelegateCommand((Action)PesquisarCep);
			ConsultaCepServiceFactory.SetCurrent((IConsultaCepServiceFactory)new ConsultaViaCepServiceFactory());
			ConsultaCepServiceFactory.SetWebProxy(DadosSistema.ObterProxy());
			_consultaCepService = ConsultaCepServiceFactory.Create();
		}

		private void PesquisarCep()
		{
			try
			{
				OnConsultouCepEvent(_consultaCepService.ObterCep(HelperString.RemoveDiacritics(Cep)));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
				Cep = string.Empty;
			}
		}

		private void OnConsultouCepEvent(CepInfo cepInfo)
		{
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Expected O, but got Unknown
			//IL_00eb: Expected O, but got Unknown
			//IL_00f0: Expected O, but got Unknown
			if (HelperObject.Is<CepInvalido>((object)cepInfo))
			{
				throw new Exception("CEP inválido ou não encontrado.");
			}
			Logradouro = cepInfo.Logradouro;
			Bairro = cepInfo.Bairro;
			Municipio = new MunicipioDto
			{
				Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Nome),
				CodigoIbge = cepInfo.Municipio.CodigoIbge,
				UF = new UFDto
				{
					Nome = cepInfo.Municipio.Uf.Nome,
					Sigla = cepInfo.Municipio.Uf.Sigla,
					CodigoIbge = cepInfo.Municipio.Uf.CodigoIbge,
					Pais = new PaisDto
					{
						Nome = HelperString.ToStringEmptyWhenNull(cepInfo.Municipio.Uf.Pais.Nome),
						CodigoBacen = cepInfo.Municipio.Uf.Pais.CodigoBacen
					}
				}
			};
			if (!string.IsNullOrWhiteSpace(Logradouro))
			{
				InformouCepComLogradouro = !string.IsNullOrWhiteSpace(Logradouro);
			}
		}

		protected override void ProjectedAsPartialViewModel()
		{
			Bairro = base.Dto.Bairro;
			Cep = base.Dto.Cep;
			Numero = base.Dto.Numero;
			Logradouro = base.Dto.Logradouro;
			Complemento = base.Dto.Complemento;
			Municipio = base.Dto.Municipio;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is EnderecoDto) ? obj : null);
			((EnderecoDto)obj2).Bairro = Bairro;
			((EnderecoDto)obj2).Cep = Cep;
			((EnderecoDto)obj2).Numero = Numero;
			((EnderecoDto)obj2).Logradouro = Logradouro;
			((EnderecoDto)obj2).Complemento = Complemento;
			((EnderecoDto)obj2).Municipio = Municipio;
		}

		public override void Dispose()
		{
			((ViewModelValidationBase)this).Dispose();
			BuscarMunicipioCommandParameter = null;
			Municipio = null;
		}
	}
	public interface IBasePagamento
	{
		int Codigo { get; }

		string Descricao { get; }
	}
	public class Dinheiro : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		private readonly Func<decimal> _retornarTotalComFrete;

		public decimal ValorParaCalculoTroco
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => ValorParaCalculoTroco));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => ValorParaCalculoTroco), value, (Action)OnAlterouValorParaCalculoTroco);
			}
		}

		public decimal TrocoCalculado
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => TrocoCalculado));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => TrocoCalculado), value);
			}
		}

		public Visibility TrocoCalculadoVisivel
		{
			get
			{
				if (!(TrocoCalculado > 0m))
				{
					return Visibility.Collapsed;
				}
				return Visibility.Visible;
			}
		}

		public int Codigo { get; }

		public string Descricao { get; }

		public Dinheiro(Func<decimal> retornarTotalComFrete)
		{
			_retornarTotalComFrete = retornarTotalComFrete;
			Codigo = 0;
			Descricao = "Dinheiro";
		}

		private void OnAlterouValorParaCalculoTroco()
		{
			decimal num = _retornarTotalComFrete();
			TrocoCalculado = ValorParaCalculoTroco - num;
			((BindableBase)this).RaisePropertyChanged<Visibility>((Expression<Func<Visibility>>)(() => TrocoCalculadoVisivel));
		}

		public override void ProjectedAsViewModel()
		{
			ValorParaCalculoTroco = base.Dto.ValorParaCalculoTroco;
		}
	}
	public class Cartao : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Cartao()
		{
			Codigo = 1;
			Descricao = "Cartão";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class Outros : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Outros()
		{
			Codigo = 2;
			Descricao = "Outros";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class Pix : ViewModelDtoBase<EntregaDeliveryDto>, IBasePagamento
	{
		public int Codigo { get; }

		public string Descricao { get; }

		public Pix()
		{
			Codigo = 3;
			Descricao = "PIX";
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
	public class EntregasAbaViewModel : IAbaViewModelBase, IDisposable
	{
		public string Titulo => "CONTROLE DE ENTREGAS";

		public short? Numero => null;

		public string SubTitulo => "Situação das entregas";

		public bool Valido => ViewModel.Valido;

		public IViewModelBase ViewModel { get; }

		public EntregasAbaViewModel(IViewModelBase vm)
		{
			ViewModel = vm;
		}

		public void Dispose()
		{
			ViewModel.Dispose();
		}

		public IAbaViewModelBase SetarNumero(short numero)
		{
			return (IAbaViewModelBase)(object)this;
		}
	}
	public class EntregasDeliveryFichaPrincipalViewModel : FichaBase<EntregaDeliveryDto, EntregaViewModel>
	{
		private readonly IWService _service;

		private readonly ISessaoService _sessaoService;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IEventPublisher _eventPublisher;

		private readonly INotificacao _notificacao;

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

		public ICommand MinimizarCommand { get; set; }

		public ICommand MaximizarCommand { get; set; }

		public EntregasDeliveryFichaPrincipalViewModel(IWService service, ISessaoService sessaoService, IImpressoraNaoFiscalService impressoraNaoFiscalService, IEventPublisher eventPublisher, INotificacao notificacao)
			: base("Entregas Delivery")
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Expected O, but got Unknown
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			_service = service;
			_sessaoService = sessaoService;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_eventPublisher = eventPublisher;
			_notificacao = notificacao;
			CriarFormulario(new EntregaDeliveryDto());
			MinimizarCommand = (ICommand)new DelegateCommand((Action)Minimizar);
			MaximizarCommand = (ICommand)new DelegateCommand((Action)null, (Func<bool>)(() => false), (bool?)null);
			ConfiguracaoCores val = ConfiguracaoCores.Carregar();
			Cor = ((Cor)new CorSaida(HelperObject.IsNull((object)val) ? "#015e81" : val.CodigoCorSaidas)).Codigo;
		}

		private void Minimizar()
		{
			NavigationAsyncExtensions.RequestNavigate((INavigateAsync)(object)((NavigationBase)this).NavigationService.Region.RegionManager.Regions[Regioes.TrabalhoRegion], "IniciaisView", (Action<NavigationResult>)delegate
			{
			}, ((Base)this).Parameters);
		}

		public override void Dispose()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			_eventPublisher.Publish<OcultarLogo>(new OcultarLogo(false));
			base.Dispose();
		}

		public override void FecharView()
		{
			base.DisableConfirmOnClose();
			((NavigationBase)this).FecharView();
		}

		private void CriarFormulario(EntregaDeliveryDto dto)
		{
			base.Formulario = new EntregaViewModel(_service, _sessaoService, _impressoraNaoFiscalService, dto, dto.Entregador, new List<PessoaDto>(), _eventPublisher, _notificacao).CriarFormulario();
		}
	}
	public class EntregasDeliveryViewModel : ModuloBase
	{
		public EntregasDeliveryViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloEntregasDeliveryRegion, typeof(EntregasDeliveryFichaPrincipalView).Name, (IResolucao)new ResolucaoTotalTrabalho())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class EntregasViewModel : ViewModelDtoBase<object>
	{
		private readonly IWService _service;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly ISessaoService _sessaoService;

		private readonly IEventPublisher _eventPublisher;

		private readonly INotificacao _notificacao;

		private IDisposable _eventoRestaurante;

		private GerarEntregaRestaurante _entregaRestaurante;

		public short NumeroRegistrosPorPagina => 7;

		public long TotalRegistros => Pedidos.Numero;

		public short Pagina { get; protected set; } = 1;


		public bool FocoInformacaoPesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => FocoInformacaoPesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => FocoInformacaoPesquisa), value);
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

		public ICommand MostrarDetalhesCommand { get; set; }

		public ICommand AtualizarListaCommand { get; set; }

		public ICommand GerarRotaCommand { get; set; }

		public ICommand CancelarEntregaCommand { get; set; }

		public ICommand ConcluirEntregaCommand { get; set; }

		public ICommand ImprimirPedidoCommand { get; set; }

		public ICommand IncluirEntregaCommand { get; set; }

		public ICommand GravarEntregaCommand { get; set; }

		public ICommand CancelarCadastroEntregaCommand { get; set; }

		public ICommand GravarPessoaCommand { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public ICommand CtrlFCommand { get; set; }

		public ICommand VoltarAguardandoEntregaCommand { get; set; }

		public ICommand IniciarConversaWhatsAppCommand { get; set; }

		public Lista<SituacaoEntregaDto> SituacaoEntrega
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<SituacaoEntregaDto>>((Expression<Func<Lista<SituacaoEntregaDto>>>)(() => SituacaoEntrega));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<SituacaoEntregaDto>>((Expression<Func<Lista<SituacaoEntregaDto>>>)(() => SituacaoEntrega), value);
			}
		}

		public Lista<EntregaViewModel> Pedidos
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<EntregaViewModel>>((Expression<Func<Lista<EntregaViewModel>>>)(() => Pedidos));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<EntregaViewModel>>((Expression<Func<Lista<EntregaViewModel>>>)(() => Pedidos), value);
			}
		}

		public int EstadoDaAnimacao
		{
			get
			{
				return ((BindableBase)this).GetProperty<int>((Expression<Func<int>>)(() => EstadoDaAnimacao));
			}
			set
			{
				((BindableBase)this).SetProperty<int>((Expression<Func<int>>)(() => EstadoDaAnimacao), value);
			}
		}

		public bool PodeEntregar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeEntregar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeEntregar), value);
			}
		}

		public bool PodeCancelar
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeCancelar));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeCancelar), value);
			}
		}

		public bool PodeConcluir
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeConcluir));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeConcluir), value);
			}
		}

		public bool PodeVoltarAguardando
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeVoltarAguardando));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeVoltarAguardando), value);
			}
		}

		public bool PodeImprimir
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => PodeImprimir));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => PodeImprimir), value);
			}
		}

		public bool MostrarComboEntregador
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarComboEntregador));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarComboEntregador), value);
			}
		}

		public CadastroRotaViewModel CadastroRota
		{
			get
			{
				return ((BindableBase)this).GetProperty<CadastroRotaViewModel>((Expression<Func<CadastroRotaViewModel>>)(() => CadastroRota));
			}
			set
			{
				((BindableBase)this).SetProperty<CadastroRotaViewModel>((Expression<Func<CadastroRotaViewModel>>)(() => CadastroRota), value);
			}
		}

		public bool IsLista
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsLista));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsLista), value);
			}
		}

		public bool IsCancelado
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsCancelado));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsCancelado), value);
			}
		}

		public EntregasViewModel(IWService service, IImpressoraNaoFiscalService impressoraNaoFiscalService, ISessaoService sessaoService, IEventPublisher eventPublisher, INotificacao notificacao)
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Expected O, but got Unknown
			//IL_015f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0169: Expected O, but got Unknown
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0195: Expected O, but got Unknown
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c1: Expected O, but got Unknown
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_0211: Expected O, but got Unknown
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_0240: Expected O, but got Unknown
			_service = service;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_sessaoService = sessaoService;
			_eventPublisher = eventPublisher;
			_notificacao = notificacao;
			Pedidos = new Lista<EntregaViewModel>();
			SituacaoEntrega = new Lista<SituacaoEntregaDto>();
			AtualizarListaCommand = (ICommand)new AsyncCommand((Func<Task>)ComandoAtualizarLista);
			MostrarDetalhesCommand = (ICommand)new DelegateCommand((Action)MostrarDetalhes);
			GerarRotaCommand = (ICommand)new DelegateCommand<object>((Action<object>)GerarRota, (Func<object, bool>)PodeGerarRota, (bool?)null);
			CancelarEntregaCommand = (ICommand)new DelegateCommand<object>((Action<object>)CancelarEntrega, (Func<object, bool>)PodeCancelarEntrega, (bool?)null);
			ConcluirEntregaCommand = (ICommand)new DelegateCommand<object>((Action<object>)ConcluirEntrega, (Func<object, bool>)PodeConcluirEntrega, (bool?)null);
			ImprimirPedidoCommand = (ICommand)new DelegateCommand<object>((Action<object>)Imprimir, (Func<object, bool>)PodeImprimirEntrega, (bool?)null);
			Pedidos.AlterouItemSelecionadoEvent += PedidosOnAlterouItemSelecionadoEvent;
			IncluirEntregaCommand = (ICommand)new DelegateCommand((Action)AdicionarEntrega, (Func<bool>)PodeAdicionarEntrega, (bool?)null);
			GravarEntregaCommand = (ICommand)new DelegateCommand((Action)GravarEntrega, (Func<bool>)PodeGravarEntrega, (bool?)null);
			CancelarCadastroEntregaCommand = (ICommand)new DelegateCommand((Action)CancelarCadastroEntrega, (Func<bool>)PodeCancelarCadastroEntrega, (bool?)null);
			IsLista = true;
			_eventoRestaurante = _eventPublisher.GetEvent<GerarEntregaRestaurante>((Action<GerarEntregaRestaurante>)AdicionarEntregaRestaurante);
			GravarPessoaCommand = (ICommand)new DelegateCommand((Action)GravarPessoa, (Func<bool>)PodeGravarPessoa, (bool?)null);
			ScrollChangedCommand = (ICommand)new DelegateCommand<object>((Action<object>)ScrollChanged, true);
			CtrlFCommand = (ICommand)new DelegateCommand((Action)delegate
			{
				FocoInformacaoPesquisa = !FocoInformacaoPesquisa;
			});
			VoltarAguardandoEntregaCommand = (ICommand)new DelegateCommand<object>((Action<object>)VoltarAguardandoEntrega, (Func<object, bool>)PodeVoltarAguardandoEntrega, (bool?)null);
			IniciarConversaWhatsAppCommand = (ICommand)new AsyncCommand<object>((Func<object, Task>)IniciarConversaWhatsApp, (Func<object, bool>)PodeIniciarConversaWhatsApp, false, (bool?)null);
		}

		private Task ComandoAtualizarLista()
		{
			InicializarPropriedades();
			return AtualizarLista().ContinueWith((Task a) => InformacaoPesquisa = string.Empty);
		}

		public async void ScrollChanged(object obj)
		{
			if (obj is ScrollChangedEventArgs { VerticalChange: >0.0 } scrollChangedEventArgs && !(Math.Abs(scrollChangedEventArgs.VerticalOffset + scrollChangedEventArgs.ViewportHeight - scrollChangedEventArgs.ExtentHeight) > 1.0) && TotalRegistros >= NumeroRegistrosPorPagina * Pagina)
			{
				Pagina++;
				await AtualizarLista();
			}
		}

		private bool PodeGravarPessoa()
		{
			if (((Validation)this).Valid)
			{
				CadastroRotaViewModel cadastroRota = CadastroRota;
				bool? obj;
				if (cadastroRota == null)
				{
					obj = null;
				}
				else
				{
					DadosEnderecoViewModel endereco = cadastroRota.Endereco;
					obj = ((endereco != null) ? new bool?(((Validation)endereco).Valid) : null);
				}
				if (obj ?? false)
				{
					return !string.IsNullOrEmpty(CadastroRota?.Endereco?.Cep ?? string.Empty);
				}
			}
			return false;
		}

		private async void GravarPessoa()
		{
			_ = 1;
			try
			{
				object obj;
				if (!HelperString.IsNullOrEmpty(CadastroRota.IdCliente))
				{
					obj = _service.Canal<IPessoaModuleService, PessoaDto>((Func<IPessoaModuleService, PessoaDto>)((IPessoaModuleService service) => service.ObterPessoaPorId(CadastroRota.IdCliente)));
				}
				else
				{
					PessoaDto val = new PessoaDto
					{
						Id = HelperObject.ToStringOrEmpty((object)CadastroRota.IdCliente),
						Classificacao = new ClassificacaoDto
						{
							Codigo = 6
						}
					};
					val.Fisica.Genero = new GeneroDto
					{
						Codigo = 0
					};
					val.IndicadorOperacao = new CodigoDescricaoDto
					{
						Codigo = -1,
						Descricao = "Não definido"
					};
					obj = (object)val;
				}
				PessoaDto pessoaDto = (PessoaDto)obj;
				pessoaDto.Nome = CadastroRota.Cliente;
				pessoaDto.EnderecoPrincipal = new EnderecoDto
				{
					Bairro = CadastroRota.Endereco.Bairro,
					Cep = CadastroRota.Endereco.Cep,
					Numero = CadastroRota.Endereco.Numero,
					Logradouro = CadastroRota.Endereco.Logradouro,
					Complemento = CadastroRota.Endereco.Complemento,
					Municipio = CadastroRota.Endereco.Municipio
				};
				pessoaDto.TelefonePrincipal = (TelefoneDto)(string.IsNullOrEmpty(CadastroRota.Telefone) ? new TelefoneDtoNull() : new TelefoneDto
				{
					Numero = CadastroRota.Telefone
				});
				if (((EntityBaseDto)pessoaDto).IsTransient())
				{
					await _service.Canal<IPessoaModuleService, Task<PessoaDto>>((Func<IPessoaModuleService, Task<PessoaDto>>)((IPessoaModuleService service) => ((IAdicionar<PessoaDto>)(object)service).AdicionarAsync(pessoaDto)));
					_notificacao.MostrarNotificacao("Pessoa cadastrada com sucesso!");
				}
				else
				{
					await _service.Canal<IPessoaModuleService, Task<PessoaDto>>((Func<IPessoaModuleService, Task<PessoaDto>>)((IPessoaModuleService service) => ((IAtualizar<PessoaDto>)(object)service).AtualizarAsync(pessoaDto)));
					_notificacao.MostrarNotificacao("Pessoa atualizada com sucesso!");
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private void PedidosOnAlterouItemSelecionadoEvent(object sender, EntregaViewModel entregaViewModel)
		{
			AtualizarSituacoesDeEntrega();
		}

		private void AtualizarSituacoesDeEntrega()
		{
			bool flag = Pedidos.ItemSelecionado?.Entregadores?.PossuiItemSelecionado ?? false;
			int podeEntregar;
			if (flag)
			{
				EntregaViewModel itemSelecionado = Pedidos.ItemSelecionado;
				podeEntregar = ((itemSelecionado != null && itemSelecionado.EntregaDelivery.SituacaoEntrega.Codigo == 1) ? 1 : 0);
			}
			else
			{
				podeEntregar = 0;
			}
			PodeEntregar = (byte)podeEntregar != 0;
			EntregaViewModel itemSelecionado2 = Pedidos.ItemSelecionado;
			int podeCancelar;
			if (itemSelecionado2 == null || itemSelecionado2.EntregaDelivery.SituacaoEntrega.Codigo != 2)
			{
				EntregaViewModel itemSelecionado3 = Pedidos.ItemSelecionado;
				podeCancelar = ((itemSelecionado3 != null && itemSelecionado3.EntregaDelivery.SituacaoEntrega.Codigo == 1) ? 1 : 0);
			}
			else
			{
				podeCancelar = 1;
			}
			PodeCancelar = (byte)podeCancelar != 0;
			EntregaViewModel itemSelecionado4 = Pedidos.ItemSelecionado;
			PodeVoltarAguardando = itemSelecionado4 != null && itemSelecionado4.EntregaDelivery.SituacaoEntrega.Codigo == 2;
			EntregaViewModel itemSelecionado5 = Pedidos.ItemSelecionado;
			int num;
			if (itemSelecionado5 == null || itemSelecionado5.EntregaDelivery.SituacaoEntrega.Codigo != 3)
			{
				EntregaViewModel itemSelecionado6 = Pedidos.ItemSelecionado;
				num = ((itemSelecionado6 == null || itemSelecionado6.EntregaDelivery.SituacaoEntrega.Codigo != 4) ? 1 : 0);
			}
			else
			{
				num = 0;
			}
			PodeConcluir = (byte)((uint)num & (flag ? 1u : 0u)) != 0;
			EntregaViewModel itemSelecionado7 = Pedidos.ItemSelecionado;
			PodeImprimir = itemSelecionado7 == null || itemSelecionado7.EntregaDelivery.SituacaoEntrega.Codigo != 4;
			EntregaViewModel itemSelecionado8 = Pedidos.ItemSelecionado;
			IsCancelado = itemSelecionado8 != null && itemSelecionado8.EntregaDelivery.SituacaoEntrega.Codigo == 4;
		}

		private bool PodeCancelarEntrega(object obj)
		{
			return PodeCancelar;
		}

		private async void CancelarEntrega(object obj)
		{
			string empty = string.Empty;
			if (DialogoFactory.MostrarDialogoEntradaString(ref empty, "Observação", "Informe uma justificativa para cancelar a entrega selecionada.", (IIcone)null, false, false, true, (string)null, false, true) != MessageBoxResult.OK)
			{
				return;
			}
			try
			{
				EntregaDeliveryDto val = (obj as EntregaViewModel)?.EntregaDelivery;
				if (val != null)
				{
					SetarCancelado(((EntityBaseDto)val).Id, empty);
					InicializarPropriedades();
					await AtualizarLista();
					EstadoDaAnimacao = 0;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeVoltarAguardandoEntrega(object obj)
		{
			return PodeVoltarAguardando;
		}

		private async void VoltarAguardandoEntrega(object obj)
		{
			try
			{
				EntregaDeliveryDto val = (obj as EntregaViewModel)?.EntregaDelivery;
				if (val != null)
				{
					SetarAguardando(((EntityBaseDto)val).Id);
					InicializarPropriedades();
					await AtualizarLista();
					EstadoDaAnimacao = 0;
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void InicializarPropriedades()
		{
			Pagina = 1;
			Pedidos.PosicionarNoPrimeiro();
			Pedidos.Limpar();
		}

		private bool PodeGerarRota(object obj)
		{
			return PodeEntregar;
		}

		private async void GerarRota(object obj)
		{
			try
			{
				EntregaViewModel entregaViewModel = obj as EntregaViewModel;
				if (!HelperObject.IsNull((object)entregaViewModel))
				{
					EntregaDeliveryDto entregaDelivery = entregaViewModel.EntregaDelivery;
					if (entregaDelivery != null)
					{
						SetarSaiuParaEntrega(((EntityBaseDto)entregaDelivery).Id, entregaViewModel.Entregadores.ItemSelecionado);
						InicializarPropriedades();
						await AtualizarLista();
						EstadoDaAnimacao = 0;
					}
				}
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private bool PodeConcluirEntrega(object obj)
		{
			return PodeConcluir;
		}

		private async void ConcluirEntrega(object obj)
		{
			try
			{
				EntregaViewModel entregaViewModel = obj as EntregaViewModel;
				if (!HelperObject.IsNull((object)entregaViewModel))
				{
					EntregaDeliveryDto entregaDelivery = entregaViewModel.EntregaDelivery;
					if (entregaDelivery != null)
					{
						SetarConcluido(((EntityBaseDto)entregaDelivery).Id, entregaViewModel.Entregadores.ItemSelecionado);
						InicializarPropriedades();
						await AtualizarLista();
						EstadoDaAnimacao = 0;
					}
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private void Imprimir(object obj)
		{
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			try
			{
				EntregaDeliveryDto val = (obj as EntregaViewModel)?.EntregaDelivery;
				if (val != null)
				{
					ImpressaoPedidoService val2 = new ImpressaoPedidoService(_service, val, val.TotalFrete, _sessaoService.Sessao, _impressoraNaoFiscalService);
					for (int i = 0; i < DadosSistema.ConfiguracaoDto.Saida.NumeroViasDelivery.Valor; i++)
					{
						val2.ImprimirPedido();
					}
				}
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeImprimirEntrega(object obj)
		{
			return PodeImprimir;
		}

		private void SetarCancelado(string id, string motivo)
		{
			_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
			{
				service.SetarCancelado(id, motivo);
			});
		}

		private void SetarAguardando(string id)
		{
			_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
			{
				service.SetarAguardando(id);
			});
		}

		private void SetarSaiuParaEntrega(string id, PessoaDto entregador)
		{
			_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
			{
				service.SetarSaiuEntrega(id, entregador);
			});
		}

		private void SetarConcluido(string id, PessoaDto entregador)
		{
			_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
			{
				service.SetarConcluido(id, entregador);
			});
		}

		private void MostrarDetalhes()
		{
			EstadoDaAnimacao = ((EstadoDaAnimacao != 1) ? 1 : 0);
		}

		private async Task AtualizarLista()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			IEnumerable<PessoaDto> entregadores = default(IEnumerable<PessoaDto>);
			_ = entregadores;
			entregadores = await _service.Canal<IDeliveryModuleService, Task<IEnumerable<PessoaDto>>>((Func<IDeliveryModuleService, Task<IEnumerable<PessoaDto>>>)((IDeliveryModuleService service) => service.ObterEntregadoresAsync()));
			PesquisaDto pesquisa = new PesquisaDto
			{
				Informacao = (InformacaoBaseDto)new TextoNumericoDto
				{
					Texto = HelperString.ToStringEmptyWhenNull(InformacaoPesquisa)
				},
				Pagina = Pagina,
				Tamanho = NumeroRegistrosPorPagina
			};
			await _service.Canal<IDeliveryModuleService, Task<IEnumerable<EntregaDeliveryDto>>>((Func<IDeliveryModuleService, Task<IEnumerable<EntregaDeliveryDto>>>)((IDeliveryModuleService x) => x.ObterEntregasPorSituacaoAsync(SituacaoEntrega.ItemSelecionado.Codigo, pesquisa))).ContinueWith(delegate(Task<IEnumerable<EntregaDeliveryDto>> task)
			{
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				ThreadPrincipal.Executar((Action)delegate
				{
					Pedidos.AdicionarSemPosicionar(task.Result.Select((EntregaDeliveryDto x) => new EntregaViewModel(_service, _sessaoService, _impressoraNaoFiscalService, x, x.Entregador, entregadores.ToList(), _eventPublisher, _notificacao)));
					Pedidos.PosicionarNoPrimeiro();
				});
			});
			LoaderProcessoService.EsconderLoader();
		}

		public override async void ProjectedAsViewModel()
		{
			SituacaoEntrega.Adicionar((IEnumerable<SituacaoEntregaDto>)SituacoesEntrega.Dados);
			SituacaoEntrega.PosicionarNoPrimeiro();
			SituacaoEntrega.AlterouItemSelecionadoEvent += SituacaoEntregaOnAlterouItemSelecionadoEvent;
			MostrarComboEntregador = SituacaoEntrega.ItemSelecionado.Descricao.ToLower().Contains("aguardando");
			await AtualizarLista();
			PreencherDadosRestaurante();
		}

		private async void SituacaoEntregaOnAlterouItemSelecionadoEvent(object sender, SituacaoEntregaDto codigoDescricaoDto)
		{
			InicializarPropriedades();
			await AtualizarLista();
			MostrarComboEntregador = codigoDescricaoDto.Descricao.ToLower().Contains("aguardando");
		}

		private async void GravarEntrega()
		{
			try
			{
				if (Pedidos.Itens.Any((EntregaViewModel x) => x.EntregaDelivery.Numero == CadastroRota.Numero && x.EntregaDelivery.SituacaoEntrega.Codigo == 1))
				{
					throw new Exception($"Já existe uma entrega pendente com o número {CadastroRota.Numero}");
				}
				DadosSistema.GetService().Canal<IRestauranteModuleService>((Action<IRestauranteModuleService>)delegate(IRestauranteModuleService service)
				{
					long numero = CadastroRota.Numero;
					decimal obj;
					if (!CadastroRota.AdicionarFrete)
					{
						obj = 0m;
					}
					else
					{
						FreteDto itemSelecionado = CadastroRota.Fretes.ItemSelecionado;
						obj = ((itemSelecionado != null) ? itemSelecionado.Valor : 0m);
					}
					service.RatearFreteComItensMesaConta(numero, obj);
				});
				CadastroRota.GravarRota();
				IsLista = true;
				CadastroRota = null;
				InicializarPropriedades();
				await AtualizarLista();
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
		}

		private bool PodeGravarEntrega()
		{
			CadastroRotaViewModel cadastroRota = CadastroRota;
			if (cadastroRota != null && cadastroRota.Valido)
			{
				return !IsLista;
			}
			return false;
		}

		private bool PodeCancelarCadastroEntrega()
		{
			return !IsLista;
		}

		private void CancelarCadastroEntrega()
		{
			CadastroRota = null;
			IsLista = true;
		}

		private void AdicionarEntrega()
		{
			IsLista = false;
			CadastroRota = new CadastroRotaViewModel(_service, _sessaoService.Sessao, _impressoraNaoFiscalService, _eventPublisher);
			_eventPublisher.Publish<AlterarFocoEvent>(new AlterarFocoEvent());
		}

		private bool PodeAdicionarEntrega()
		{
			if (IsLista)
			{
				return DadosSistema.IsRestaurante;
			}
			return false;
		}

		private void AdicionarEntregaRestaurante(GerarEntregaRestaurante gerarEntregaRestaurante)
		{
			_entregaRestaurante = gerarEntregaRestaurante;
		}

		private void PreencherDadosRestaurante()
		{
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0371: Unknown result type (might be due to invalid IL or missing references)
			//IL_037d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0393: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_044f: Unknown result type (might be due to invalid IL or missing references)
			//IL_045c: Expected O, but got Unknown
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_0485: Unknown result type (might be due to invalid IL or missing references)
			//IL_049e: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04df: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fb: Expected O, but got Unknown
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Expected O, but got Unknown
			//IL_05db: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0600: Expected O, but got Unknown
			//IL_0600: Unknown result type (might be due to invalid IL or missing references)
			//IL_0607: Unknown result type (might be due to invalid IL or missing references)
			//IL_0614: Expected O, but got Unknown
			//IL_064c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0651: Unknown result type (might be due to invalid IL or missing references)
			//IL_0652: Unknown result type (might be due to invalid IL or missing references)
			//IL_0657: Unknown result type (might be due to invalid IL or missing references)
			//IL_0664: Unknown result type (might be due to invalid IL or missing references)
			//IL_0671: Expected O, but got Unknown
			//IL_0671: Unknown result type (might be due to invalid IL or missing references)
			//IL_0678: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Expected O, but got Unknown
			try
			{
				GerarEntregaRestaurante entregaRestaurante = _entregaRestaurante;
				object obj;
				if (entregaRestaurante == null)
				{
					obj = null;
				}
				else
				{
					ChamarDeliveryDto chamarDelivery = entregaRestaurante.ChamarDelivery;
					obj = ((chamarDelivery != null) ? chamarDelivery.Itens : null);
				}
				if (obj == null)
				{
					return;
				}
				ItemMesaContaDto obj2 = _entregaRestaurante.ChamarDelivery.Itens.FirstOrDefault();
				long numeroMesa = ((obj2 != null) ? obj2.NumeroMesaConta : 0);
				EntregaViewModel entregaCadastrada = Pedidos.Itens.FirstOrDefault((EntregaViewModel x) => x.EntregaDelivery.SituacaoEntrega.Codigo == 1 && x.EntregaDelivery.Numero == numeroMesa);
				if (entregaCadastrada != null)
				{
					entregaCadastrada.EntregaDelivery.ItensDelivery = new List<ItemEntregaDeliveryDto>();
					entregaCadastrada.EntregaDelivery.Total = _entregaRestaurante.ChamarDelivery.Itens.Where((ItemMesaContaDto y) => !y.Cancelado).Sum((ItemMesaContaDto x) => x.Quantidade * x.ValorUnitario);
					entregaCadastrada.EntregaDelivery.TotalAdicionais = _entregaRestaurante.ChamarDelivery.Itens.Where((ItemMesaContaDto y) => !y.Cancelado).Sum((ItemMesaContaDto x) => x.TotalAdicionais);
					entregaCadastrada.EntregaDelivery.DataHoraEntregaRetirada = _entregaRestaurante.ChamarDelivery.DataHoraEntregaRetirada;
					foreach (ItemMesaContaDto iten in _entregaRestaurante.ChamarDelivery.Itens)
					{
						ItemEntregaDeliveryDto val = new ItemEntregaDeliveryDto
						{
							Total = iten.Quantidade * iten.ValorUnitario,
							Preco = iten.ValorUnitario,
							Quantidade = iten.Quantidade,
							Descricao = iten.Descricao,
							CodigoInterno = iten.Codigo,
							Cancelado = iten.Cancelado,
							Adicionais = iten.Adicionais,
							ImpressoraReferencia = iten.ImpressoraReferencia
						};
						foreach (string item in HelperIEnumerable.ToListEmptyWhenNull<string>(iten.Observacoes))
						{
							val.Observacao += $". {item}";
						}
						((List<ItemEntregaDeliveryDto>)entregaCadastrada.EntregaDelivery.ItensDelivery).Add(val);
					}
					Pedidos.Substituir((Expression<Func<EntregaViewModel, bool>>)((EntregaViewModel x) => x.EntregaDelivery.Numero == entregaCadastrada.EntregaDelivery.Numero), entregaCadastrada);
					_service.Canal<IDeliveryModuleService>((Action<IDeliveryModuleService>)delegate(IDeliveryModuleService service)
					{
						service.AtualizarEntregaAsync(entregaCadastrada.EntregaDelivery);
					});
					return;
				}
				EntregaDeliveryDto val2 = new EntregaDeliveryDto
				{
					Numero = numeroMesa,
					DataHoraEntregaRetirada = _entregaRestaurante.ChamarDelivery.DataHoraEntregaRetirada,
					Total = _entregaRestaurante.ChamarDelivery.Itens.Where((ItemMesaContaDto y) => !y.Cancelado).Sum((ItemMesaContaDto x) => x.Quantidade * x.ValorUnitario),
					TotalAdicionais = _entregaRestaurante.ChamarDelivery.Itens.Where((ItemMesaContaDto y) => !y.Cancelado).Sum((ItemMesaContaDto x) => x.TotalAdicionais),
					ItensDelivery = new List<ItemEntregaDeliveryDto>()
				};
				foreach (ItemMesaContaDto iten2 in _entregaRestaurante.ChamarDelivery.Itens)
				{
					ItemEntregaDeliveryDto val3 = new ItemEntregaDeliveryDto
					{
						Total = iten2.Quantidade * iten2.ValorUnitario,
						Preco = iten2.ValorUnitario,
						Quantidade = iten2.Quantidade,
						Descricao = iten2.Descricao,
						CodigoInterno = iten2.Codigo,
						Cancelado = iten2.Cancelado,
						Adicionais = iten2.Adicionais,
						ImpressoraReferencia = iten2.ImpressoraReferencia
					};
					foreach (string item2 in HelperIEnumerable.ToListEmptyWhenNull<string>(iten2.Observacoes))
					{
						val3.Observacao += $". {item2}";
					}
					((List<ItemEntregaDeliveryDto>)val2.ItensDelivery).Add(val3);
				}
				ItemMesaContaDto val4 = _entregaRestaurante.ChamarDelivery.Itens.FirstOrDefault((ItemMesaContaDto x) => !string.IsNullOrEmpty(x.PessoaCpf) || !string.IsNullOrEmpty(x.PessoaNome) || !string.IsNullOrEmpty(x.PessoaCelular));
				if (HelperObject.IsNotNull((object)val4))
				{
					PaginaEntityDto val5 = null;
					if (!string.IsNullOrEmpty(val4.PessoaCpf))
					{
						PesquisaDto objetoPesquisaTodos2 = new PesquisaDto
						{
							Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
							{
								Informacao = val4.PessoaCpf,
								TipoPessoaCadastro = (TipoPessoaCadastro)11
							},
							Pagina = 1,
							Tamanho = 100
						};
						val5 = _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisaTodos2))).Result;
					}
					else if (!string.IsNullOrEmpty(val4.PessoaCelular))
					{
						PesquisaDto objetoPesquisaTodos = new PesquisaDto
						{
							Informacao = (InformacaoBaseDto)new PesquisaPessoaDto
							{
								Informacao = val4.PessoaCelular,
								TipoPessoaCadastro = (TipoPessoaCadastro)11
							},
							Pagina = 1,
							Tamanho = 100
						};
						val5 = _service.Canal<IPessoaModuleService, Task<PaginaEntityDto>>((Func<IPessoaModuleService, Task<PaginaEntityDto>>)((IPessoaModuleService service) => ((IPesquisaAtivosInativosCustom)service).PesquisarAtivosAsync(objetoPesquisaTodos))).Result;
					}
					if (val5 != null && val5.Lista.Count() == 1)
					{
						BaseDto obj3 = val5.Lista.FirstOrDefault();
						PessoaDto val6 = (PessoaDto)(object)((obj3 is PessoaDto) ? obj3 : null);
						val2.VersaoCliente = ((EntityVersaoDto)val6).Versao;
						val2.IdCliente = ((EntityBaseDto)val6).Id;
						val2.Endereco = val6.EnderecoPrincipal;
						val2.Nome = val6.Nome;
						object telefone;
						if (!val6.Celulares.Any())
						{
							telefone = (HelperObject.Is<TelefoneDtoNull>((object)val6.TelefoneWhatsApp) ? val6.TelefonePrincipal.Numero : val6.TelefoneWhatsApp.Numero);
						}
						else
						{
							TelefoneDto obj4 = val6.Celulares.FirstOrDefault();
							telefone = ((obj4 != null) ? obj4.Numero : null);
						}
						val2.Telefone = (string)telefone;
					}
					else
					{
						val2.Nome = val4.PessoaNome;
						val2.Telefone = val4.PessoaCelular;
					}
				}
				CadastroRota = new CadastroRotaViewModel(_service, _sessaoService.Sessao, _impressoraNaoFiscalService, _eventPublisher, val2);
				((UpdateViewBindableBase)this).DormirAsync(1000, (Action<object>)delegate
				{
					_eventPublisher.Publish<AlterarFocoEvent>(new AlterarFocoEvent());
				});
				IsLista = false;
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		private bool PodeIniciarConversaWhatsApp(object arg)
		{
			if (HelperObject.IsNotNull((object)Pedidos))
			{
				return Pedidos.PossuiItemSelecionado;
			}
			return false;
		}

		private async Task IniciarConversaWhatsApp(object obj)
		{
			_ = 1;
			try
			{
				EntregaViewModel entregaViewModel = obj as EntregaViewModel;
				if (HelperObject.IsNull((object)entregaViewModel))
				{
					return;
				}
				EntregaDeliveryDto entregaDelivery = entregaViewModel.EntregaDelivery;
				if (entregaDelivery == null)
				{
					return;
				}
				string telefone = RetornarNumeroWhatsAppDestinatario(entregaDelivery);
				if (string.IsNullOrEmpty(telefone))
				{
					throw new Exception("Não será compartilhado. Telefone não informado.");
				}
				DadosArquivoFtp val = await RetornarArquivoEnvioPdf(entregaDelivery);
				List<DadosArquivoFtp> list = new List<DadosArquivoFtp>();
				if (HelperObject.IsNotNull((object)val))
				{
					list.Add(val);
				}
				string text = string.Empty;
				EntregaViewModel itemSelecionado = Pedidos.ItemSelecionado;
				if (itemSelecionado != null && itemSelecionado.EntregaDelivery.SituacaoEntrega.Codigo == 1)
				{
					text = "Olá,\nA entrega do seu pedido está aguardando para ser iniciada!";
				}
				EntregaViewModel itemSelecionado2 = Pedidos.ItemSelecionado;
				if (itemSelecionado2 != null && itemSelecionado2.EntregaDelivery.SituacaoEntrega.Codigo == 2)
				{
					text = "Olá,\nSeu pedido saiu pra entrega!";
				}
				EntregaViewModel itemSelecionado3 = Pedidos.ItemSelecionado;
				if (itemSelecionado3 != null && itemSelecionado3.EntregaDelivery.SituacaoEntrega.Codigo == 3)
				{
					text = "Olá,\nSeu pedido foi entregue!";
				}
				EntregaViewModel itemSelecionado4 = Pedidos.ItemSelecionado;
				if (itemSelecionado4 != null && itemSelecionado4.EntregaDelivery.SituacaoEntrega.Codigo == 4)
				{
					text = "Olá,\nA entrega do seu pedido foi cancelada!";
				}
				await WhatsAppService.Criar().ComProxy(DadosSistema.ObterProxy()).ComTelefone(telefone)
					.ComMensagemWhatsApp(text)
					.ComArquivos((IEnumerable<DadosArquivoFtp>)list)
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
							DialogoFactory.MostrarDialogoConfirma(task.Result, false, HorizontalAlignment.Center);
						}
						else
						{
							DialogoFactory.MostrarDialogoConfirma($"Compartilhado com sucesso com: {telefone}", false, HorizontalAlignment.Center);
						}
					});
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		private string RetornarNumeroWhatsAppDestinatario(EntregaDeliveryDto dto)
		{
			string telefone = dto.Telefone;
			if (DialogoFactory.MostrarDialogoEntradaString(ref telefone, "WhatsApp", "Informe o número do celular", (IIcone)null, false, false, true, telefone, false, false) != MessageBoxResult.OK)
			{
				return null;
			}
			return telefone;
		}

		private async Task<DadosArquivoFtp> RetornarArquivoEnvioPdf(EntregaDeliveryDto entregaDeliveryDto)
		{
			IRelatorio obj = await new ImpressaoPedidoService(_service, entregaDeliveryDto, entregaDeliveryDto.TotalFrete, _sessaoService.Sessao, _impressoraNaoFiscalService).RetornarImpressoPedido();
			MemoryStream memoryStream = new MemoryStream();
			obj.Processar().ExportToPdf((Stream)memoryStream);
			string text = $"Delivery_{Guid.NewGuid()}.pdf";
			return DadosArquivoFtp.Criar().ComArquivoEnvio(Objeto<ArquivoEnvio>.Criar.ComNomeArquivo(text).ComArquivoBase64(Convert.ToBase64String(memoryStream.GetBuffer()))).ComTipoArquivoWhatsApp((TipoArquivoWhatsApp)14)
				.ComTipoArquivoCompartilhando("Delivery");
		}

		public override void Dispose()
		{
			Pedidos.AlterouItemSelecionadoEvent -= PedidosOnAlterouItemSelecionadoEvent;
			SituacaoEntrega.AlterouItemSelecionadoEvent -= SituacaoEntregaOnAlterouItemSelecionadoEvent;
			((ViewModelValidationBase)SituacaoEntrega).Dispose();
			SituacaoEntrega = null;
			((ViewModelValidationBase)Pedidos).Dispose();
			Pedidos = null;
			MostrarDetalhesCommand = null;
			_eventoRestaurante.Dispose();
			_eventoRestaurante = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class EntregaViewModel : ViewModelBase<EntregaDeliveryDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private readonly IWService _service;

		private readonly ISessaoService _sessaoService;

		private readonly IImpressoraNaoFiscalService _impressoraNaoFiscalService;

		private readonly IEventPublisher _eventPublisher;

		private readonly INotificacao _notificacao;

		public EntregaDeliveryDto EntregaDelivery { get; set; }

		public PessoaDto Entregador
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Entregador));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Entregador), value, (Action)ChangedCallback);
			}
		}

		public Lista<PessoaDto> Entregadores
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Entregadores));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Entregadores), value);
			}
		}

		public ISelecaoCommandParameter EntregadorCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoa)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				if (Entregadores.Existe((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)pessoa).Id)))
				{
					Entregadores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)pessoa).Id));
				}
				else
				{
					Entregadores.Adicionar(pessoa);
					Entregadores.PosicionarNoUltimo();
				}
			});
		}, (ValidateParameter<PessoaDto>)(object)new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => !HelperObject.Is<EntregadorDtoNull>((object)x.Fisica.Entregador)), "Selecione uma pessoa do tipo 'Entregador'!", (TipoPessoaCadastro)3, HorizontalAlignment.Center));

		public EntregaViewModel(IWService service, ISessaoService sessaoService, IImpressoraNaoFiscalService impressoraNaoFiscalService, EntregaDeliveryDto pedido, PessoaDto entregador, IEnumerable<PessoaDto> entregadores, IEventPublisher eventPublisher, INotificacao notificacao)
		{
			_service = service;
			_sessaoService = sessaoService;
			_impressoraNaoFiscalService = impressoraNaoFiscalService;
			_eventPublisher = eventPublisher;
			_notificacao = notificacao;
			EntregaDelivery = pedido;
			Entregadores = new Lista<PessoaDto>();
			Entregadores.Adicionar(entregadores);
			Entregadores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)entregador).Id));
		}

		private void ChangedCallback()
		{
			Entregadores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)Entregador).Id));
		}

		public string ToTituloLista()
		{
			return "Entregas";
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void AtualizarDto<T>(ref T dto)
		{
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new ControleDeEntregaFormulario(_service, _sessaoService, _impressoraNaoFiscalService, _eventPublisher, _notificacao);
		}

		public string ToTituloFicha()
		{
			return "Entregas";
		}
	}
	public class MapasAbaViewModel : IAbaViewModelBase, IDisposable
	{
		public string Titulo => "MAPA";

		public short? Numero => null;

		public string SubTitulo => "Rotas para entregas";

		public bool Valido => ViewModel.Valido;

		public IViewModelBase ViewModel { get; }

		public MapasAbaViewModel(IViewModelBase vm)
		{
			ViewModel = vm;
		}

		public void Dispose()
		{
			ViewModel.Dispose();
		}

		public IAbaViewModelBase SetarNumero(short numero)
		{
			return (IAbaViewModelBase)(object)this;
		}
	}
	public class MapasViewModel : ViewModelDtoBase<object>
	{
		private GeradorDeRotasService _geradorDeRotasService;

		public Lista<EntregaDeliveryDto> ListaDeEntregas
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<EntregaDeliveryDto>>((Expression<Func<Lista<EntregaDeliveryDto>>>)(() => ListaDeEntregas));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<EntregaDeliveryDto>>((Expression<Func<Lista<EntregaDeliveryDto>>>)(() => ListaDeEntregas), value);
			}
		}

		public Lista<PessoaDto> Entregadores
		{
			get
			{
				return ((BindableBase)this).GetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Entregadores));
			}
			set
			{
				((BindableBase)this).SetProperty<Lista<PessoaDto>>((Expression<Func<Lista<PessoaDto>>>)(() => Entregadores), value);
			}
		}

		public ISelecaoCommandParameter EntregadorCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto dto)
		{
			if (Entregadores.Existe((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id)))
			{
				Entregadores.Posicionar((Expression<Func<PessoaDto, bool>>)((PessoaDto x) => ((EntityBaseDto)x).Id == ((EntityBaseDto)dto).Id));
			}
			else
			{
				Entregadores.Adicionar(dto);
			}
		}, true);

		public ICommand AbrirMapaCommand { get; set; }

		public MapasViewModel()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			_geradorDeRotasService = new GeradorDeRotasService();
			ListaDeEntregas = new Lista<EntregaDeliveryDto>();
			PreencherEntregadores();
			AbrirMapaCommand = (ICommand)new DelegateCommand((Action)AbrirMapa);
		}

		private async void PreencherEntregadores()
		{
			Entregadores = new Lista<PessoaDto>();
			IEnumerable<PessoaDto> enumerable = await DadosSistema.GetService().Canal<IDeliveryModuleService, Task<IEnumerable<PessoaDto>>>((Func<IDeliveryModuleService, Task<IEnumerable<PessoaDto>>>)((IDeliveryModuleService service) => service.ObterEntregadoresAsync()));
			Entregadores.Adicionar(enumerable);
			Entregadores.AlterouItemSelecionadoEvent += EntregadoresOnAlterouItemSelecionadoEvent;
			Entregadores.PosicionarNoPrimeiro();
			await AtualizarListaEntregas();
		}

		private async void EntregadoresOnAlterouItemSelecionadoEvent(object sender, PessoaDto pessoaDto)
		{
			await AtualizarListaEntregas();
		}

		public async Task AtualizarListaEntregas()
		{
			try
			{
				ListaDeEntregas.Limpar();
				PesquisaDto pesquisa = new PesquisaDto
				{
					Informacao = (InformacaoBaseDto)new TextoNumericoDto
					{
						Texto = string.Empty
					},
					Pagina = 1,
					Tamanho = 9999
				};
				IEnumerable<EntregaDeliveryDto> source = await DadosSistema.GetService().Canal<IDeliveryModuleService, Task<IEnumerable<EntregaDeliveryDto>>>((Func<IDeliveryModuleService, Task<IEnumerable<EntregaDeliveryDto>>>)((IDeliveryModuleService service) => service.ObterEntregasPorSituacaoAsync(2, pesquisa)));
				ListaDeEntregas.Adicionar(source.Where(delegate(EntregaDeliveryDto x)
				{
					string id = ((EntityBaseDto)x.Entregador).Id;
					PessoaDto itemSelecionado = Entregadores.ItemSelecionado;
					return id == ((itemSelecionado != null) ? ((EntityBaseDto)itemSelecionado).Id : null);
				}));
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, HorizontalAlignment.Center);
			}
		}

		public override void ProjectedAsViewModel()
		{
		}

		public void AbrirMapa()
		{
			if (!ListaDeEntregas.Itens.Any())
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			Task.Run(delegate
			{
				ThreadPrincipal.Executar((Action)delegate
				{
					try
					{
						if (string.IsNullOrWhiteSpace(DadosSistema.ConfiguracaoDto.Geral.GoogleMapsApiKey.Valor))
						{
							throw new Exception("'Api_key' não definida nas configurações do sistema");
						}
						_geradorDeRotasService.LimparEnderecos();
						Pessoa empresaLogada = DadosSistema.EmpresaLogada;
						_geradorDeRotasService.AdicionarEndereco(empresaLogada.Numero, empresaLogada.Logradouro, empresaLogada.Bairro, empresaLogada.Municipio.Nome, empresaLogada.Municipio.Uf.Sigla, DadosSistema.ConfiguracaoDto.Geral.GoogleMapsApiKey.Valor);
						foreach (EntregaDeliveryDto iten in ListaDeEntregas.Itens)
						{
							_geradorDeRotasService.AdicionarEndereco(iten.Endereco, DadosSistema.ConfiguracaoDto.Geral.GoogleMapsApiKey.Valor);
						}
						_geradorDeRotasService.AdicionarEndereco(empresaLogada.Numero, empresaLogada.Logradouro, empresaLogada.Bairro, empresaLogada.Municipio.Nome, empresaLogada.Municipio.Uf.Sigla, DadosSistema.ConfiguracaoDto.Geral.GoogleMapsApiKey.Valor);
						_geradorDeRotasService.CalcularRota(DadosSistema.ConfiguracaoDto.Geral.GoogleMapsApiKey.Valor);
						Process.Start(_geradorDeRotasService.GerarRota());
					}
					catch (Exception ex)
					{
						HelperException.ShowLastException(ex, "", false, false);
					}
					finally
					{
						LoaderProcessoService.EsconderLoader();
					}
				});
			});
		}

		public override void Dispose()
		{
			Entregadores.AlterouItemSelecionadoEvent -= EntregadoresOnAlterouItemSelecionadoEvent;
			_geradorDeRotasService.Dispose();
			_geradorDeRotasService = null;
			((ViewModelValidationBase)ListaDeEntregas).Dispose();
			ListaDeEntregas = null;
			((ViewModelValidationBase)this).Dispose();
		}
	}
}
namespace Otus.Modulo.EntregasDelivery.Reports
{
	public class ImpressoEndereco : XtraReport, IRelatorio
	{
		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private ReportHeaderBand ReportHeader;

		private XRLabel xrLabel2;

		private ObjectDataSource objectDataSource1;

		private XRLabel xrLabel4;

		private XRLabel xrLabel5;

		private XRLabel xrLabel6;

		private XRLabel xrLabel7;

		private XRLabel xrLabel8;

		private XRLabel xrLabel9;

		private XRLabel xrLabel10;

		private XRLabel xrLabel11;

		private XRLabel xrLabel12;

		private XRLabel xrLabel13;

		private XRLabel xrLabel14;

		private XRLine xrLine1;

		private ReportFooterBand ReportFooter;

		private XRPictureBox xrpMapaEndereco;

		private ObjectDataSource objectDataSource2;

		public int Id { get; set; }

		public string Descricao { get; set; }

		public IParametros Parametros { get; set; }

		public bool ExibirNoModuloRelatorios => false;

		public ImpressoEndereco()
		{
			InitializeComponent();
		}

		public XtraReport Processar()
		{
			((XtraReportBase)this).DataSource = Parametros.Objeto;
			IParametro val = Parametros.ListaDeParametros.FirstOrDefault((IParametro x) => x.Titulo == "MapaEndereco");
			if (val != null)
			{
				xrpMapaEndereco.ImageUrl = val.Valor.ToString();
			}
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
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Expected O, but got Unknown
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Expected O, but got Unknown
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Expected O, but got Unknown
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00aa: Expected O, but got Unknown
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Expected O, but got Unknown
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Expected O, but got Unknown
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Expected O, but got Unknown
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Expected O, but got Unknown
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0270: Unknown result type (might be due to invalid IL or missing references)
			//IL_0299: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0302: Expected O, but got Unknown
			//IL_0344: Unknown result type (might be due to invalid IL or missing references)
			//IL_036d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c6: Expected O, but got Unknown
			//IL_0408: Unknown result type (might be due to invalid IL or missing references)
			//IL_0431: Unknown result type (might be due to invalid IL or missing references)
			//IL_0484: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Expected O, but got Unknown
			//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0548: Unknown result type (might be due to invalid IL or missing references)
			//IL_054e: Expected O, but got Unknown
			//IL_0590: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_060c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0612: Expected O, but got Unknown
			//IL_0654: Unknown result type (might be due to invalid IL or missing references)
			//IL_067d: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_0718: Unknown result type (might be due to invalid IL or missing references)
			//IL_079a: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0845: Unknown result type (might be due to invalid IL or missing references)
			//IL_086e: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0919: Unknown result type (might be due to invalid IL or missing references)
			//IL_097c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0982: Expected O, but got Unknown
			//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cef: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ImpressoEndereco));
			Detail = new DetailBand();
			xrLabel4 = new XRLabel();
			xrLabel5 = new XRLabel();
			xrLabel6 = new XRLabel();
			xrLabel7 = new XRLabel();
			xrLabel8 = new XRLabel();
			xrLabel9 = new XRLabel();
			xrLabel10 = new XRLabel();
			xrLabel11 = new XRLabel();
			xrLabel12 = new XRLabel();
			xrLabel13 = new XRLabel();
			xrLabel14 = new XRLabel();
			xrLine1 = new XRLine();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			ReportHeader = new ReportHeaderBand();
			xrLabel2 = new XRLabel();
			objectDataSource1 = new ObjectDataSource(components);
			ReportFooter = new ReportFooterBand();
			xrpMapaEndereco = new XRPictureBox();
			objectDataSource2 = new ObjectDataSource(components);
			((ISupportInitialize)objectDataSource1).BeginInit();
			((ISupportInitialize)objectDataSource2).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[12]
			{
				(XRControl)xrLabel4,
				(XRControl)xrLabel5,
				(XRControl)xrLabel6,
				(XRControl)xrLabel7,
				(XRControl)xrLabel8,
				(XRControl)xrLabel9,
				(XRControl)xrLabel10,
				(XRControl)xrLabel11,
				(XRControl)xrLabel12,
				(XRControl)xrLabel13,
				(XRControl)xrLabel14,
				(XRControl)xrLine1
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).Font = new Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)Detail).HeightF = 180.5516f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).StylePriority.UseFont = false;
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)xrLabel4).Dpi = 254f;
			((XRControl)xrLabel4).Font = new Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel4).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel4).Name = "xrLabel4";
			((XRControl)xrLabel4).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel4).SizeF = new SizeF(198.4375f, 58.42003f);
			((XRControl)xrLabel4).StylePriority.UseFont = false;
			((XRControl)xrLabel4).Text = "Endereço:";
			((XRControl)xrLabel5).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Logradouro")
			});
			((XRControl)xrLabel5).Dpi = 254f;
			((XRControl)xrLabel5).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel5).LocationFloat = new PointFloat(198.4375f, 0f);
			((XRControl)xrLabel5).Name = "xrLabel5";
			((XRControl)xrLabel5).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel5).SizeF = new SizeF(1264.975f, 58.42001f);
			((XRControl)xrLabel5).StylePriority.UseFont = false;
			((XRControl)xrLabel6).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Numero")
			});
			((XRControl)xrLabel6).Dpi = 254f;
			((XRControl)xrLabel6).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel6).LocationFloat = new PointFloat(1641f, 0f);
			((XRControl)xrLabel6).Name = "xrLabel6";
			((XRControl)xrLabel6).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel6).SizeF = new SizeF(210.208f, 58.42001f);
			((XRControl)xrLabel6).StylePriority.UseFont = false;
			((XRControl)xrLabel7).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Bairro")
			});
			((XRControl)xrLabel7).Dpi = 254f;
			((XRControl)xrLabel7).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel7).LocationFloat = new PointFloat(131.5504f, 58.41996f);
			((XRControl)xrLabel7).Name = "xrLabel7";
			((XRControl)xrLabel7).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel7).SizeF = new SizeF(820.4748f, 58.42f);
			((XRControl)xrLabel7).StylePriority.UseFont = false;
			((XRControl)xrLabel8).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Municipio.Nome")
			});
			((XRControl)xrLabel8).Dpi = 254f;
			((XRControl)xrLabel8).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel8).LocationFloat = new PointFloat(1120.617f, 58.42004f);
			((XRControl)xrLabel8).Name = "xrLabel8";
			((XRControl)xrLabel8).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel8).SizeF = new SizeF(520.3824f, 58.42f);
			((XRControl)xrLabel8).StylePriority.UseFont = false;
			((XRControl)xrLabel9).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Municipio.UF.Sigla")
			});
			((XRControl)xrLabel9).Dpi = 254f;
			((XRControl)xrLabel9).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel9).LocationFloat = new PointFloat(1641f, 58.41996f);
			((XRControl)xrLabel9).Name = "xrLabel9";
			((XRControl)xrLabel9).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel9).SizeF = new SizeF(210.2076f, 58.42f);
			((XRControl)xrLabel9).StylePriority.UseFont = false;
			((XRControl)xrLabel10).Dpi = 254f;
			((XRControl)xrLabel10).Font = new Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel10).LocationFloat = new PointFloat(1463.412f, 0f);
			((XRControl)xrLabel10).Name = "xrLabel10";
			((XRControl)xrLabel10).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel10).SizeF = new SizeF(177.5879f, 58.42003f);
			((XRControl)xrLabel10).StylePriority.UseFont = false;
			((XRControl)xrLabel10).Text = "Número:";
			((XRControl)xrLabel11).Dpi = 254f;
			((XRControl)xrLabel11).Font = new Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel11).LocationFloat = new PointFloat(0f, 58.41996f);
			((XRControl)xrLabel11).Name = "xrLabel11";
			((XRControl)xrLabel11).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel11).SizeF = new SizeF(131.5504f, 58.42007f);
			((XRControl)xrLabel11).StylePriority.UseFont = false;
			((XRControl)xrLabel11).Text = "Bairro:";
			((XRControl)xrLabel12).Dpi = 254f;
			((XRControl)xrLabel12).Font = new Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel12).LocationFloat = new PointFloat(952.0252f, 58.42004f);
			((XRControl)xrLabel12).Name = "xrLabel12";
			((XRControl)xrLabel12).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel12).SizeF = new SizeF(168.5922f, 58.42001f);
			((XRControl)xrLabel12).StylePriority.UseFont = false;
			((XRControl)xrLabel12).Text = "Cidade:";
			((XRControl)xrLabel13).Dpi = 254f;
			((XRControl)xrLabel13).Font = new Font("Segoe UI Light", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel13).LocationFloat = new PointFloat(0f, 116.8399f);
			((XRControl)xrLabel13).Name = "xrLabel13";
			((XRControl)xrLabel13).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel13).SizeF = new SizeF(285.0089f, 58.42f);
			((XRControl)xrLabel13).StylePriority.UseFont = false;
			((XRControl)xrLabel13).Text = "Complemento:";
			((XRControl)xrLabel14).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Complemento")
			});
			((XRControl)xrLabel14).Dpi = 254f;
			((XRControl)xrLabel14).Font = new Font("Segoe UI Light", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XRControl)xrLabel14).LocationFloat = new PointFloat(285.0089f, 116.8401f);
			((XRControl)xrLabel14).Name = "xrLabel14";
			((XRControl)xrLabel14).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel14).SizeF = new SizeF(835.6085f, 58.42f);
			((XRControl)xrLabel14).StylePriority.UseFont = false;
			((XRControl)xrLine1).Dpi = 254f;
			xrLine1.LineWidth = 3;
			((XRControl)xrLine1).LocationFloat = new PointFloat(0f, 175.2599f);
			((XRControl)xrLine1).Name = "xrLine1";
			((XRControl)xrLine1).SizeF = new SizeF(1856f, 5.291687f);
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 101f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 201f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			((XRControl)ReportHeader).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrLabel2 });
			((XRControl)ReportHeader).Dpi = 254f;
			((XRControl)ReportHeader).HeightF = 84.87839f;
			((XRControl)ReportHeader).Name = "ReportHeader";
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).Font = new Font("Segoe UI", 16f);
			((XRControl)xrLabel2).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(1830.6f, 84.87839f);
			((XRControl)xrLabel2).StylePriority.UseFont = false;
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "Endereços para entrega";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)2;
			objectDataSource1.DataSource = typeof(MovimentacaoDto);
			((DataComponentBase)objectDataSource1).Name = "objectDataSource1";
			((XRControl)ReportFooter).Controls.AddRange((XRControl[])(object)new XRControl[1] { (XRControl)xrpMapaEndereco });
			((XRControl)ReportFooter).Dpi = 254f;
			((XRControl)ReportFooter).HeightF = 977.9584f;
			((XRControl)ReportFooter).Name = "ReportFooter";
			((XRControl)xrpMapaEndereco).Dpi = 254f;
			xrpMapaEndereco.Image = (System.Drawing.Image)componentResourceManager.GetObject("xrpMapaEndereco.Image");
			((XRControl)xrpMapaEndereco).LocationFloat = new PointFloat(0f, 0f);
			((XRControl)xrpMapaEndereco).Name = "xrpMapaEndereco";
			((XRControl)xrpMapaEndereco).SizeF = new SizeF(1856f, 977.9584f);
			xrpMapaEndereco.Sizing = (ImageSizeMode)4;
			objectDataSource2.DataSource = typeof(EnderecoDto);
			((DataComponentBase)objectDataSource2).Name = "objectDataSource2";
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[5]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin,
				(Band)ReportHeader,
				(Band)ReportFooter
			});
			((XtraReport)this).ComponentStorage.AddRange(new IComponent[2]
			{
				(IComponent)objectDataSource1,
				(IComponent)objectDataSource2
			});
			((XtraReportBase)this).DataSource = objectDataSource2;
			((XRControl)this).Dpi = 254f;
			((XtraReport)this).Margins = new Margins(201, 101, 101, 201);
			((XtraReport)this).PageHeight = 2794;
			((XtraReport)this).PageWidth = 2159;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.1";
			((ISupportInitialize)objectDataSource1).EndInit();
			((ISupportInitialize)objectDataSource2).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.EntregasDelivery.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.EntregasDelivery.Properties.Resources", typeof(Resources).Assembly);
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

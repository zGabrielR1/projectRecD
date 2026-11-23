using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Dto.Cadastros;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Right;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Perfis.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Perfis")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Perfis")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Perfis
{
	[Module(ModuleName = "PerfilModulo", OnDemand = true)]
	public class PerfilModulo : IModule
	{
		private readonly IUnityContainer _container;

		public PerfilModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<PerfisView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PerfilListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PerfilFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<PerfilFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Perfis.Views
{
	public class LinkDuplicar : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public LinkDuplicar()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/linkduplicar.xaml", UriKind.Relative);
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
	public class PerfilFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public PerfilFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfilfichaadicionarview.xaml", UriKind.Relative);
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
	public class PerfilFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public PerfilFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfilfichaatualizarview.xaml", UriKind.Relative);
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
	public class PerfilListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PerfilListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfillistaview.xaml", UriKind.Relative);
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
	public class PerfisView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PerfisView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfisview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Perfis.Views.PerfilFichaViewDetalhes
{
	public class PerfilView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PerfilView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfilfichaviewdetalhes/perfilview.xaml", UriKind.Relative);
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
	public class RestricaoView : UserControl, IComponentConnector
	{
		internal TextEdit InformacaoAPesquisar;

		private bool _contentLoaded;

		public RestricaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Perfis;component/views/perfilfichaviewdetalhes/restricaoview.xaml", UriKind.Relative);
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
				InformacaoAPesquisar = (TextEdit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
}
namespace Otus.Modulo.Perfis.ViewModels
{
	public class DireitoDetalheViewModel : BindableBase, IDisposable
	{
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

		public bool Visivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Visivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Visivel), value);
			}
		}

		public string Descricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Descricao));
			}
			protected set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Descricao), value);
			}
		}

		public DireitoDto Dto { get; protected set; }

		public ICor Cor { get; protected set; }

		public event EventHandler<DireitoDetalheViewModel> AtualizouEvent;

		public DireitoDetalheViewModel(DireitoDto dto)
		{
			Selecionado = true;
			Visivel = true;
			Dto = dto;
			ProjectedAsViewModel();
		}

		public void ProjectedAsViewModel()
		{
			Descricao = Dto.Descricao;
		}

		public void Atualizar(IEnumerable<DireitoDto> dto)
		{
			List<DireitoDto> source = HelperIEnumerable.ToListEmptyWhenNull<DireitoDto>(dto);
			Selecionado = source.Any((DireitoDto x) => x.Codigo == Dto.Codigo);
			((BindableBase)this).PropertyChanged += OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "Selecionado")
			{
				OnAtualizouEvent(this);
			}
		}

		protected virtual void OnAtualizouEvent(DireitoDetalheViewModel direitoDetalheViewModel)
		{
			this.AtualizouEvent?.Invoke(this, direitoDetalheViewModel);
		}

		public void Marcar()
		{
			Selecionado = true;
		}

		public void Desmarcar()
		{
			Selecionado = false;
		}

		public void Dispose()
		{
			((BindableBase)this).PropertyChanged -= OnPropertyChanged;
		}
	}
	public class ListaDireitoDetalheViewModel : ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>
	{
		public event EventHandler<DireitoDetalheViewModel> AtualizouEvent;

		public ListaDireitoDetalheViewModel(IEnumerable<DireitoDto> dto)
			: base(dto)
		{
			((ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>)this).ProjectedAsViewModel();
		}

		public void Atualizar(IEnumerable<DireitoDto> direitosDto)
		{
			List<DireitoDto> dto = HelperIEnumerable.ToListEmptyWhenNull<DireitoDto>(direitosDto);
			foreach (DireitoDetalheViewModel iten in ((Lista<DireitoDetalheViewModel>)(object)this).Itens)
			{
				iten.Atualizar(dto);
				iten.AtualizouEvent += DireitoDetalheViewModelOnAtualizouEvent;
			}
			AtualizarDto();
		}

		private void AtualizarDto()
		{
			base.Dtos = from x in ((Lista<DireitoDetalheViewModel>)(object)this).Itens
				where x.Selecionado
				select x.Dto;
		}

		private void DireitoDetalheViewModelOnAtualizouEvent(object sender, DireitoDetalheViewModel direitoDetalheViewModel)
		{
			AtualizarDto();
			OnAtualizouEvent(direitoDetalheViewModel);
		}

		protected virtual void OnAtualizouEvent(DireitoDetalheViewModel direitoDetalheViewModel)
		{
			this.AtualizouEvent?.Invoke(this, direitoDetalheViewModel);
		}

		public override void Dispose()
		{
			foreach (DireitoDetalheViewModel iten in ((Lista<DireitoDetalheViewModel>)(object)this).Itens)
			{
				iten.AtualizouEvent -= DireitoDetalheViewModelOnAtualizouEvent;
				iten.Dispose();
			}
			((Lista<DireitoDetalheViewModel>)(object)this).Dispose();
		}
	}
	public class ListaRestricaoDetalheViewModel : ListaDetalheBase<RestricaoDto, RestricaoDetalheViewModel>
	{
		public event EventHandler AtualizouEvent;

		public ListaRestricaoDetalheViewModel(IEnumerable<RestricaoDto> dto)
			: base(dto)
		{
			((ListaDetalheBase<RestricaoDto, RestricaoDetalheViewModel>)this).ProjectedAsViewModel();
		}

		public void Atualizar(IEnumerable<RestricaoDto> restricoesDto)
		{
			List<RestricaoDto> restricoes = HelperIEnumerable.ToListEmptyWhenNull<RestricaoDto>(restricoesDto);
			foreach (RestricaoDetalheViewModel iten in ((Lista<RestricaoDetalheViewModel>)(object)this).Itens)
			{
				iten.Atualizar(restricoes);
				iten.AtualizouEvent += RestricaoDetalheViewModelOnAtualizouEvent;
			}
			AtualizarDto();
		}

		private void AtualizarDto()
		{
			base.Dtos = ((Lista<RestricaoDetalheViewModel>)(object)this).Itens.Select((RestricaoDetalheViewModel a) => a.RestricaoDto);
		}

		private void RestricaoDetalheViewModelOnAtualizouEvent(object sender, EventArgs evtArgs)
		{
			AtualizarDto();
			OnAtualizouEvent();
		}

		protected virtual void OnAtualizouEvent()
		{
			this.AtualizouEvent?.Invoke(this, EventArgs.Empty);
		}

		public override void Dispose()
		{
			foreach (RestricaoDetalheViewModel iten in ((Lista<RestricaoDetalheViewModel>)(object)this).Itens)
			{
				iten.AtualizouEvent -= RestricaoDetalheViewModelOnAtualizouEvent;
				iten.Dispose();
			}
			((Lista<RestricaoDetalheViewModel>)(object)this).Dispose();
		}
	}
	public class RestricaoDetalheViewModel : BindableBase, IDisposable
	{
		public string Descricao
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Descricao));
			}
			protected set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Descricao), value);
			}
		}

		public bool Visivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => Visivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => Visivel), value);
			}
		}

		public bool IsExpanded
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => IsExpanded));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => IsExpanded), value);
			}
		}

		public RestricaoDto RestricaoDto { get; protected set; }

		public ICor Cor { get; protected set; }

		public ListaDireitoDetalheViewModel Direitos { get; protected set; }

		public event EventHandler AtualizouEvent;

		public RestricaoDetalheViewModel(RestricaoDto restricaoDto)
		{
			RestricaoDto = restricaoDto;
			Visivel = true;
			ProjectedAsViewModel();
		}

		public void ProjectedAsViewModel()
		{
			Descricao = RestricaoDto.Modulo.DescricaoDetalhada;
			Direitos = new ListaDireitoDetalheViewModel(RestricaoDto.Direitos);
		}

		public void Atualizar(IEnumerable<RestricaoDto> restricoes)
		{
			RestricaoDto val = HelperIEnumerable.ToListEmptyWhenNull<RestricaoDto>(restricoes).FirstOrDefault((RestricaoDto x) => x.Modulo.Codigo == RestricaoDto.Modulo.Codigo);
			Direitos.Atualizar((val != null) ? val.Direitos : null);
			Direitos.AtualizouEvent += DireitosOnAtualizouEvent;
			RestricaoDto restricaoDto = RestricaoDto;
			IEnumerable<DireitoDto> direitos;
			if (((ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>)Direitos).Dtos.Any())
			{
				direitos = ((ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>)Direitos).Dtos;
			}
			else
			{
				IEnumerable<DireitoDto> enumerable = (IEnumerable<DireitoDto>)new List<DireitoDtoNull>();
				direitos = enumerable;
			}
			restricaoDto.Direitos = direitos;
		}

		private void DireitosOnAtualizouEvent(object sender, DireitoDetalheViewModel direitoDetalheViewModel)
		{
			VerificarDireitos(direitoDetalheViewModel);
			RestricaoDto restricaoDto = RestricaoDto;
			IEnumerable<DireitoDto> direitos;
			if (((ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>)Direitos).Dtos.Any())
			{
				direitos = ((ListaDetalheBase<DireitoDto, DireitoDetalheViewModel>)Direitos).Dtos;
			}
			else
			{
				IEnumerable<DireitoDto> enumerable = (IEnumerable<DireitoDto>)new List<DireitoDtoNull>();
				direitos = enumerable;
			}
			restricaoDto.Direitos = direitos;
			OnAtualizouEvent();
		}

		private void VerificarDireitos(DireitoDetalheViewModel direitoDetalheViewModel)
		{
			Direitos.AtualizouEvent -= DireitosOnAtualizouEvent;
			VerificarDireitosServidor(direitoDetalheViewModel);
			VerificarDireitoVisualizar(direitoDetalheViewModel);
			Direitos.AtualizouEvent += DireitosOnAtualizouEvent;
		}

		private void VerificarDireitosServidor(DireitoDetalheViewModel direitoDetalheViewModel)
		{
			IDireito direitoSelecionado = Direitos.Lista.FirstOrDefault((IDireito x) => x.Codigo == direitoDetalheViewModel.Dto.Codigo);
			IDireito obj = direitoSelecionado;
			if (obj == null)
			{
				return;
			}
			HelperObject.ToDo<IDireitoCliente>((object)obj, (Action<IDireitoCliente>)delegate(IDireitoCliente direitoCliente)
			{
				foreach (IDireitoServidor direitoServidor in direitoCliente.DireitosServidor)
				{
					DireitoDetalheViewModel direitoDetalheViewModel2 = ((Lista<DireitoDetalheViewModel>)(object)Direitos).Itens.FirstOrDefault((DireitoDetalheViewModel x) => x.Dto.Codigo.Equals(((IDireito)direitoServidor).Codigo));
					if (!HelperObject.IsNull((object)direitoDetalheViewModel2) && direitoDetalheViewModel2.Dto.Codigo != direitoSelecionado.Codigo && direitoDetalheViewModel.Selecionado)
					{
						direitoDetalheViewModel2.Marcar();
					}
				}
			});
		}

		private void VerificarDireitoVisualizar(DireitoDetalheViewModel direitoDetalheViewModel)
		{
			if (direitoDetalheViewModel.Dto.Codigo == 1 && !direitoDetalheViewModel.Selecionado)
			{
				foreach (DireitoDetalheViewModel item in ((Lista<DireitoDetalheViewModel>)(object)Direitos).Itens.Where((DireitoDetalheViewModel item) => item.Dto.Codigo != 1))
				{
					item.Desmarcar();
				}
				return;
			}
			if (!((Lista<DireitoDetalheViewModel>)(object)Direitos).Itens.Any((DireitoDetalheViewModel x) => x.Selecionado && x.Dto.Codigo == 1) && ((Lista<DireitoDetalheViewModel>)(object)Direitos).Itens.Any((DireitoDetalheViewModel x) => x.Selecionado))
			{
				((Lista<DireitoDetalheViewModel>)(object)Direitos).Itens.FirstOrDefault((DireitoDetalheViewModel x) => x.Dto.Codigo == 1)?.Marcar();
			}
		}

		protected virtual void OnAtualizouEvent()
		{
			this.AtualizouEvent?.Invoke(this, EventArgs.Empty);
		}

		public void Dispose()
		{
			Direitos.AtualizouEvent -= DireitosOnAtualizouEvent;
			((ViewModelValidationBase)Direitos).Dispose();
			Direitos = null;
		}
	}
	public class PerfilAbaViewModel : AbaPrincipalViewModelBase
	{
		public PerfilAbaViewModel(IViewModelPrincipal viewModel)
			: base("PERFIL", "Dados principais", viewModel)
		{
		}
	}
	public class PerfilFichaAdicionarViewModel : FichaBaseAdicionar<PerfilDto, PerfilViewModel, IPerfilModuleService>
	{
		public PerfilFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class PerfilFichaAtualizarViewModel : FichaBaseAtualizar<PerfilDto, PerfilViewModel, IPerfilModuleService>
	{
		public PerfilFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class PerfilFormulario : FormularioEntity<PerfilDto>
	{
		public PerfilFormulario(PerfilDto dto)
			: base(dto)
		{
			PerfilViewModel viewModel = new PerfilViewModel(base.Dto);
			List<RestricaoDto> dto2 = new List<RestricaoDto>(base.Dto.Sistema.Restricoes.Where((RestricaoDto restricao) => restricao.ExibirNoModuloPerfis));
			List<RestricaoDto> restricoesUsuario = new List<RestricaoDto>(base.Dto.Restricoes);
			RestricaoViewModel restricaoViewModel = new RestricaoViewModel(dto2);
			restricaoViewModel.ProjectedAsViewModel(restricoesUsuario);
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new PerfilAbaViewModel((IViewModelPrincipal)(object)viewModel),
				(IAbaViewModelBase)new RestricaoAbaViewModel((IViewModelDtoBase)(object)restricaoViewModel)
			});
		}
	}
	public class PerfilListaViewModel : ListaReferenciaHybrid<PerfilDto, PerfilViewModel, IPerfilModuleService, Perfil>
	{
		public ICommand DuplicarCommand { get; set; }

		public PerfilListaViewModel(ISessao sessao, IWService service, IEntityAtivos<PerfilDto, IPerfilModuleService> ativos, IEntityInativos<PerfilDto, IPerfilModuleService> inativos)
			: base(sessao, typeof(PerfilFichaAdicionarView).Name, typeof(PerfilFichaAtualizarView).Name, service, ativos, inativos)
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			DuplicarCommand = (ICommand)new AsyncCommand((Func<Task>)Duplicar, (Func<bool>)PodeDuplicar, (bool?)null);
		}

		private bool PodeDuplicar()
		{
			if (((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Itens != null)
			{
				return ((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Itens.SelecionadoValido;
			}
			return false;
		}

		private async Task Duplicar()
		{
			PerfilDto dto = ((ViewModelDtoBase<PerfilDto>)(object)((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Itens.Selecionado).Dto;
			_ = dto;
			dto = await ((ListaReferenciaBase<PerfilDto, PerfilViewModel, IPerfilModuleService, Perfil>)(object)this)._service.Canal<IPerfilModuleService, Task<PerfilDto>>((Func<IPerfilModuleService, Task<PerfilDto>>)((IPerfilModuleService service) => ((IAtualizar<PerfilDto>)(object)service).AtualizarAsync(dto)));
			dto.Duplicar();
			PerfilListaViewModel perfilListaViewModel = this;
			Action<NavigationResult> obj = delegate(NavigationResult navigationResult)
			{
				if (navigationResult.Result == true)
				{
					((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Itens.Estado.AlterarAdicao();
				}
			};
			NavigationParameters val = new NavigationParameters();
			? val2 = val;
			string name = typeof(PerfilViewModel).Name;
			object obj2 = Activator.CreateInstance(typeof(PerfilViewModel), dto);
			((NavigationParameters)val2).Add(name, (obj2 is IViewModelPrincipal) ? obj2 : null);
			((NavigationBase)perfilListaViewModel).Navegar("PerfilFichaAdicionarView", obj, val);
		}

		protected override void OnParametroInformado(object sender, NavigationParameters navigationParameters)
		{
			((ListaSelecaoBase<PerfilDto, PerfilViewModel, Perfil>)(object)this).OnParametroInformado(sender, navigationParameters);
			string text = navigationParameters["InformacaoPesquisa"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				if (text != HelperEnum.RetornarDescricao((Enum)(object)(TipoInformacaoPesquisa)0))
				{
					((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Pesquisa.Contexto.Informacao = text;
				}
				((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Pesquisa.Contexto.PesquisarCommand.Execute(1);
				((ListaBase<PerfilDto, PerfilViewModel>)(object)this).Pesquisa.Contexto.Informacao = string.Empty;
			}
		}
	}
	public class PerfilViewModel : ViewModelHybridObject<PerfilDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
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
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Descricao), value, (Action)delegate
				{
					((ViewModelDtoBase<PerfilDto>)(object)this).Dto.Descricao = Descricao;
				});
			}
		}

		public PerfilViewModel()
		{
		}

		public PerfilViewModel(PerfilDto dto)
			: base(dto)
		{
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new PerfilFormulario(((ViewModelDtoBase<PerfilDto>)(object)this).Dto);
		}

		public override void ProjectedAsViewModel()
		{
			((ViewModelAtivoBase<PerfilDto>)(object)this).Ativo = ((EntityDto)((ViewModelDtoBase<PerfilDto>)(object)this).Dto).Ativo;
			Descricao = ((ViewModelDtoBase<PerfilDto>)(object)this).Dto.Descricao;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			PerfilDto val = (PerfilDto)((obj is PerfilDto) ? obj : null);
			if (val != null)
			{
				((EntityDto)val).Ativo = ((ViewModelAtivoBase<PerfilDto>)(object)this).Ativo;
				val.Descricao = Descricao;
				((ViewModelDtoBase<PerfilDto>)(object)this).AtualizarDto<T>(ref dto);
			}
		}

		public string ToTituloLista()
		{
			return "Perfis do usuário";
		}

		public string ToTituloFicha()
		{
			return "Perfil do usuário";
		}
	}
	public class PerfisViewModel : ModuloBase
	{
		public PerfisViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloPerfilRegion, "", (IResolucao)new ResolucaoBase())
		{
		}//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown

	}
	public class RestricaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public RestricaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("RESTRIÇÃO", "Dados principais", viewModel)
		{
		}
	}
	public class RestricaoViewModel : ViewModelDtoBase<List<RestricaoDto>>
	{
		public ListaRestricaoDetalheViewModel Restricoes { get; protected set; }

		public string Pesquisa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Pesquisa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Pesquisa), value, (Action)FiltrarRestricoes);
			}
		}

		public string DescricaoSelecionarTodos
		{
			get
			{
				if (!HelperString.IsNullOrEmpty(Pesquisa))
				{
					return "Marcar/Desmarcar todos os visíveis";
				}
				return "Marcar/Desmarcar todos";
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
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => SelecionarTodos), value, (Action)delegate
				{
					foreach (DireitoDetalheViewModel item in ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens.SelectMany((RestricaoDetalheViewModel item) => ((Lista<DireitoDetalheViewModel>)(object)item.Direitos).Itens.Where((DireitoDetalheViewModel x) => x.Visivel)))
					{
						item.Selecionado = value;
					}
				});
			}
		}

		public RestricaoViewModel(List<RestricaoDto> dto)
			: base(dto)
		{
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoSelecionarTodos));
		}

		public void ProjectedAsViewModel(IEnumerable<RestricaoDto> restricoesUsuario)
		{
			Restricoes = new ListaRestricaoDetalheViewModel(base.Dto);
			Restricoes.Atualizar(restricoesUsuario);
			((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Ordenar((Expression<Func<RestricaoDetalheViewModel, object>>)((RestricaoDetalheViewModel a) => a.Descricao), (Expression<Func<RestricaoDetalheViewModel, object>>)null);
			foreach (DireitoDetalheViewModel item in ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens.SelectMany((RestricaoDetalheViewModel item) => ((Lista<DireitoDetalheViewModel>)(object)item.Direitos).Itens))
			{
				((BindableBase)item).PropertyChanged += DireitoPropertyChanged;
			}
			((ViewModelValidationBase)this).Valido = ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens.Any((RestricaoDetalheViewModel x) => ((Lista<DireitoDetalheViewModel>)(object)x.Direitos).Itens.Any((DireitoDetalheViewModel z) => z.Selecionado));
		}

		private void DireitoPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			((ViewModelValidationBase)this).Valido = ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens.Any((RestricaoDetalheViewModel x) => ((Lista<DireitoDetalheViewModel>)(object)x.Direitos).Itens.Any((DireitoDetalheViewModel z) => z.Selecionado));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is PerfilDto) ? obj : null);
			if (obj2 != null)
			{
				((PerfilDto)obj2).AdicionarRestricoes(((ListaDetalheBase<RestricaoDto, RestricaoDetalheViewModel>)Restricoes).Dtos);
			}
		}

		public override void Dispose()
		{
			foreach (DireitoDetalheViewModel item in ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens.SelectMany((RestricaoDetalheViewModel item) => ((Lista<DireitoDetalheViewModel>)(object)item.Direitos).Itens))
			{
				((BindableBase)item).PropertyChanged -= DireitoPropertyChanged;
			}
			((ViewModelValidationBase)Restricoes).Dispose();
			Restricoes = null;
			((ViewModelValidationBase)this).Dispose();
		}

		private void FiltrarRestricoes()
		{
			string value = Pesquisa?.Trim().ToLowerInvariant() ?? string.Empty;
			bool flag = !string.IsNullOrEmpty(value);
			foreach (RestricaoDetalheViewModel iten in ((Lista<RestricaoDetalheViewModel>)(object)Restricoes).Itens)
			{
				bool flag2 = !flag || iten.Descricao.ToLowerInvariant().Contains(value);
				foreach (DireitoDetalheViewModel iten2 in ((Lista<DireitoDetalheViewModel>)(object)iten.Direitos).Itens)
				{
					iten2.Visivel = flag2 || iten2.Descricao.ToLowerInvariant().Contains(value);
				}
				iten.Visivel = flag2 || ((Lista<DireitoDetalheViewModel>)(object)iten.Direitos).Itens.Any((DireitoDetalheViewModel d) => d.Visivel);
				iten.IsExpanded = flag && iten.Visivel;
			}
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => DescricaoSelecionarTodos));
		}

		public override void ProjectedAsViewModel()
		{
		}
	}
}
namespace Otus.Modulo.Perfis.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Perfis.Properties.Resources", typeof(Resources).Assembly);
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

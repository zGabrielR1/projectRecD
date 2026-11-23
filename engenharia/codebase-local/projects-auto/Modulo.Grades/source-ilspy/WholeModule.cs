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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Grades;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Attributes;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Grades.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Grades")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Grades")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace XamlGeneratedNamespace
{
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
	{
		protected override object CreateInstance(Type type, CultureInfo culture)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, culture);
		}

		protected override object GetPropertyValue(PropertyInfo propertyInfo, object target, CultureInfo culture)
		{
			return propertyInfo.GetValue(target, BindingFlags.Default, null, null, culture);
		}

		protected override void SetPropertyValue(PropertyInfo propertyInfo, object target, object value, CultureInfo culture)
		{
			propertyInfo.SetValue(target, value, BindingFlags.Default, null, null, culture);
		}

		protected override Delegate CreateDelegate(Type delegateType, object target, string handler)
		{
			return (Delegate)target.GetType().InvokeMember("_CreateDelegate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, target, new object[2] { delegateType, handler }, null);
		}

		protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
		{
			eventInfo.AddEventHandler(target, handler);
		}
	}
}
namespace Otus.Modulo.Grades
{
	[Module(ModuleName = "GradeModulo", OnDemand = true)]
	public class GradeModulo : IModule
	{
		private readonly IUnityContainer _container;

		public GradeModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<GradesView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GradeListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GradeFichaAtualizarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<GradeFichaAdicionarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Grades.Views
{
	public class GradeFichaAdicionarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GradeFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradefichaadicionarview.xaml", UriKind.Relative);
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
	public class GradeFichaAtualizarView : UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public GradeFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradefichaatualizarview.xaml", UriKind.Relative);
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
	public class GradesView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GradesView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradesview.xaml", UriKind.Relative);
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
	public class GradeListaView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GradeListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradelistaview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Grades.Views.GradeFichaViewDetalhes
{
	public class GradeView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public GradeView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradefichaviewdetalhes/gradeview.xaml", UriKind.Relative);
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
	public class LinhasColunasView : UserControl, IComponentConnector
	{
		internal TextEdit TxtDescricaoLinhaColuna;

		private bool _contentLoaded;

		public LinhasColunasView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradefichaviewdetalhes/linhascolunasview.xaml", UriKind.Relative);
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
				TxtDescricaoLinhaColuna = (TextEdit)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class VisualizacaoView : UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public VisualizacaoView()
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
				Uri resourceLocator = new Uri("/Modulo.Grades;component/views/gradefichaviewdetalhes/visualizacaoview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Grades.ViewModels
{
	public class ColunasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public ColunasAbaViewModel(IViewModelDtoBase viewModel)
			: base("COLUNAS", "Cadastro de colunas", viewModel)
		{
		}
	}
	public class ColunasViewModel : LinhasColunasViewModel
	{
		[Valid(ErrorMessage = "Selecione as Colunas")]
		public override LinhasColunas LinhasColunas
		{
			get
			{
				return ((BindableBase)this).GetProperty<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas));
			}
			set
			{
				((BindableBase)this).SetProperty<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas), value);
			}
		}

		public ColunasViewModel(GradeDto dto)
			: base(dto, null)
		{
			base.LinhaColuna = "Coluna";
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			LinhasColunas.AdicionarLinhasColunasSelecionadas((from item in ((ViewModelDtoBase<GradeDto>)this).Dto.Itens.Select((ItemDto x) => x.Coluna.Descricao).Distinct()
				select ((ViewModelDtoBase<GradeDto>)this).Dto.LinhasColunas.FirstOrDefault((LinhaColunaDto x) => x.Descricao.Equals(item))).ToList());
		}
	}
	public class GradeFichaAdicionarViewModel : FichaBaseAdicionar<GradeDto, GradeViewModel, IGradeModuleService>
	{
		public GradeFichaAdicionarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class GradeAbaViewModel : AbaPrincipalViewModelBase
	{
		public GradeAbaViewModel(IViewModelPrincipal viewModel)
			: base("GRADE", "Dados principais", viewModel)
		{
		}
	}
	public class GradeFichaAtualizarViewModel : FichaBaseAtualizar<GradeDto, GradeViewModel, IGradeModuleService>
	{
		public GradeFichaAtualizarViewModel(IWService service)
			: base(service)
		{
		}
	}
	public class GradeFormulario : FormularioEntity<GradeDto>
	{
		private readonly LinhasViewModel _linhas;

		private readonly ColunasViewModel _colunas;

		private readonly VisualizacaoViewModel _visualizacao;

		public GradeFormulario(GradeDto dto)
			: base(dto)
		{
			GradeViewModel viewModel = new GradeViewModel(base.Dto);
			_linhas = new LinhasViewModel(base.Dto);
			_colunas = new ColunasViewModel(base.Dto);
			_visualizacao = new VisualizacaoViewModel(base.Dto);
			_linhas.CadastrouLinhaColuna += _colunas.SetarLinhaColunaCadastradaEvent;
			_colunas.CadastrouLinhaColuna += _linhas.SetarLinhaColunaCadastradaEvent;
			_linhas.LinhasColunas.LinhasColunasSelecionadas.AlterouEvent += _visualizacao.SetarLinhasParaVisualizacaoEvent;
			_colunas.LinhasColunas.LinhasColunasSelecionadas.AlterouEvent += _visualizacao.SetarColunasParaVisualizacaoEvent;
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[4]
			{
				(IAbaViewModelBase)new GradeAbaViewModel((IViewModelPrincipal)(object)viewModel),
				(IAbaViewModelBase)new LinhasAbaViewModel((IViewModelDtoBase)(object)_linhas),
				(IAbaViewModelBase)new ColunasAbaViewModel((IViewModelDtoBase)(object)_colunas),
				(IAbaViewModelBase)new VisualizacaoAbaViewModel((IViewModelDtoBase)(object)_visualizacao)
			});
			_linhas.AjustarGrade();
			_colunas.AjustarGrade();
		}

		public override void Dispose()
		{
			_linhas.LinhasColunas.LinhasColunasSelecionadas.AlterouEvent -= _visualizacao.SetarLinhasParaVisualizacaoEvent;
			_colunas.LinhasColunas.LinhasColunasSelecionadas.AlterouEvent -= _visualizacao.SetarColunasParaVisualizacaoEvent;
			((Formulario)this).Dispose();
		}
	}
	public class GradesViewModel : ModuloBase
	{
		public GradesViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloGradeRegion, typeof(GradeListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class GradeViewModel : ViewModelAtivoBase<GradeDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
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
					((ViewModelDtoBase<GradeDto>)(object)this).Dto.Descricao = Descricao;
				});
			}
		}

		public GradeViewModel()
		{
		}

		public GradeViewModel(GradeDto dto)
			: base(dto)
		{
		}

		public override void ProjectedAsViewModel()
		{
			base.Ativo = ((EntityDto)((ViewModelDtoBase<GradeDto>)(object)this).Dto).Ativo;
			Descricao = ((ViewModelDtoBase<GradeDto>)(object)this).Dto.Descricao;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GradeDto val = (GradeDto)((obj is GradeDto) ? obj : null);
			if (val != null)
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.Descricao = Descricao;
				((ViewModelDtoBase<GradeDto>)(object)this).AtualizarDto<GradeDto>(ref val);
			}
		}

		public string ToTituloLista()
		{
			return "Grades";
		}

		public string ToTituloFicha()
		{
			return "Grade";
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new GradeFormulario(((ViewModelDtoBase<GradeDto>)(object)this).Dto);
		}
	}
	public class GradeListaViewModel : ListaReferencia<GradeDto, GradeViewModel, IGradeModuleService, Grade>
	{
		public GradeListaViewModel(ISessao sessao, IWService service, IEntityAtivos<GradeDto, IGradeModuleService> ativos, IEntityInativos<GradeDto, IGradeModuleService> inativos)
			: base(sessao, typeof(GradeFichaAdicionarView).Name, typeof(GradeFichaAtualizarView).Name, service, new IEntityBase<GradeDto, IGradeModuleService>[2]
			{
				(IEntityBase<GradeDto, IGradeModuleService>)(object)ativos,
				(IEntityBase<GradeDto, IGradeModuleService>)(object)inativos
			})
		{
		}
	}
	public class LinhasColunas : ViewModelValidationBase
	{
		public Lista<LinhaColunaDto> LinhasColunasCadastradas { get; set; }

		[Valid]
		public Lista<LinhaColunaViewModel> LinhasColunasSelecionadas { get; set; }

		public ICommand AdicionarLinhaColunaSelecionadaCommand => (ICommand)new DelegateCommand<object>((Action<object>)AdicionarLinhaColunaSelecionada, (Func<object, bool>)PodeAdicionarLinhaColunaSelecionada, (bool?)true);

		public ICommand RemoverLinhaColunaSelecionadaCommand => (ICommand)new DelegateCommand<object>((Action<object>)RemoverLinhaColunaSelecionada);

		public LinhasColunas()
		{
			LinhasColunasCadastradas = new Lista<LinhaColunaDto>();
			LinhasColunasSelecionadas = new Lista<LinhaColunaViewModel>();
			((BindableBase)LinhasColunasSelecionadas).PropertyChanged += LinhasColunasSelecionadasOnPropertyChanged;
			LinhasColunasSelecionadas.AdicionouEvent += LinhasColunasSelecionadasOnAdicionouEvent;
			LinhasColunasSelecionadas.RemoveuEvent += LinhasColunasSelecionadasOnRemoveuEvent;
		}

		private void LinhasColunasSelecionadasOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<Lista<LinhaColunaViewModel>>((Expression<Func<Lista<LinhaColunaViewModel>>>)(() => LinhasColunasSelecionadas));
		}

		public void AdicionarLinhasColunasSelecionadas(IEnumerable<LinhaColunaDto> linhasColunasDtos)
		{
			List<LinhaColunaDto> source = HelperIEnumerable.ToListEmptyWhenNull<LinhaColunaDto>(linhasColunasDtos);
			LinhasColunasSelecionadas.Adicionar(source.Select(ObterLinhaColunaViewModel));
		}

		private void LinhasColunasSelecionadasOnRemoveuEvent(object sender, LinhaColunaViewModel linhaColuna)
		{
			LinhasColunasCadastradas.Adicionar(linhaColuna.Dto);
		}

		private void LinhasColunasSelecionadasOnAdicionouEvent(object sender, LinhaColunaViewModel linhaColuna)
		{
			LinhasColunasCadastradas.Remover(linhaColuna.Dto);
		}

		private bool PodeAdicionarLinhaColunaSelecionada(object obj)
		{
			return obj is LinhaColunaDto;
		}

		private void AdicionarLinhaColunaSelecionada(object obj)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			LinhasColunasSelecionadas.Adicionar(ObterLinhaColunaViewModel((LinhaColunaDto)obj));
		}

		private LinhaColunaViewModel ObterLinhaColunaViewModel(LinhaColunaDto linhaColunaDto)
		{
			return new LinhaColunaViewModel(linhaColunaDto, RemoverLinhaColunaSelecionada);
		}

		private void RemoverLinhaColunaSelecionada(object obj)
		{
			LinhasColunasSelecionadas.Remover(obj as LinhaColunaViewModel);
		}

		public override void Dispose()
		{
			((BindableBase)LinhasColunasSelecionadas).PropertyChanged -= LinhasColunasSelecionadasOnPropertyChanged;
			LinhasColunasSelecionadas.AdicionouEvent -= LinhasColunasSelecionadasOnAdicionouEvent;
			LinhasColunasSelecionadas.RemoveuEvent -= LinhasColunasSelecionadasOnRemoveuEvent;
			((ViewModelValidationBase)LinhasColunasCadastradas).Dispose();
			((ViewModelValidationBase)LinhasColunasSelecionadas).Dispose();
			((ViewModelValidationBase)this).Dispose();
		}
	}
	public class LinhaColunaViewModel : BindableBase
	{
		public LinhaColunaDto Dto { get; protected set; }

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

		public ICommand RemoverLinhaColunaSelecionadaCommand { get; set; }

		public LinhaColunaViewModel(LinhaColunaDto dto, Action<object> removerLinhaColunaSelecionada)
		{
			Dto = dto;
			Descricao = dto.Descricao;
			RemoverLinhaColunaSelecionadaCommand = (ICommand)new DelegateCommand<object>(removerLinhaColunaSelecionada);
		}
	}
	public abstract class LinhasColunasViewModel : ViewModelDtoBase<GradeDto>
	{
		protected readonly Action<string, Action<LinhaColunaDto>> PesquisarLinhaColuna;

		private bool _ordernarInverter;

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

		public string LinhaColuna
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => LinhaColuna));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => LinhaColuna), value);
			}
		}

		public abstract LinhasColunas LinhasColunas { get; set; }

		public string DescricaoLinhaColuna
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => DescricaoLinhaColuna));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => DescricaoLinhaColuna), value, (Action)ChangedCallback);
			}
		}

		public ICommand AdicionarCommand => (ICommand)new DelegateCommand<string>((Action<string>)AdicionarLinhaColuna, (Func<string, bool>)PodeAdicionarLinhaColuna, (bool?)true);

		public ICommand PesquisarCommand { get; }

		public ICommand LimparCommand { get; }

		public ICommand OrdenarItensCommand { get; }

		public event EventHandler<LinhaColunaDto> CadastrouLinhaColuna;

		protected LinhasColunasViewModel(GradeDto dto, Action<string, Action<LinhaColunaDto>> pesquisarLinhaColuna)
			: base(dto)
		{
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_006b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Expected O, but got Unknown
			PesquisarLinhaColuna = pesquisarLinhaColuna;
			LinhasColunas = new LinhasColunas();
			((BindableBase)LinhasColunas).PropertyChanged += LinhasColunasOnPropertyChanged;
			PesquisarCommand = (ICommand)new DelegateCommand((Action)PesquisarItens);
			LimparCommand = (ICommand)new DelegateCommand((Action)LimparPesquisa);
			OrdenarItensCommand = (ICommand)new DelegateCommand((Action)OrdenarItens);
		}

		private void OrdenarItens()
		{
			List<LinhaColunaDto> list = LinhasColunas.LinhasColunasCadastradas.Itens.OrderBy((LinhaColunaDto x) => x.Descricao).ToList();
			if (_ordernarInverter)
			{
				list.Reverse();
			}
			LinhasColunas.LinhasColunasCadastradas.Limpar();
			LinhasColunas.LinhasColunasCadastradas.Adicionar((IEnumerable<LinhaColunaDto>)list);
			((BindableBase)this).RaisePropertyChanged<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas));
			LinhasColunas.LinhasColunasCadastradas.PosicionarNoPrimeiro();
			_ordernarInverter = !_ordernarInverter;
		}

		private void ChangedCallback()
		{
			PesquisarItens();
		}

		private void PesquisarItens()
		{
			string informacao = HelperString.RemoveDiacritics(HelperString.ToStringEmptyWhenNull(InformacaoPesquisa)).ToLowerInvariant();
			LinhasColunas.LinhasColunasCadastradas.Posicionar((Expression<Func<LinhaColunaDto, bool>>)((LinhaColunaDto x) => x.Descricao.ToLower().StartsWith(informacao)));
		}

		private void LimparPesquisa()
		{
			InformacaoPesquisa = string.Empty;
			PesquisarItens();
		}

		public override void ProjectedAsViewModel()
		{
			LinhasColunas.LinhasColunasCadastradas.Adicionar(base.Dto.LinhasColunas);
		}

		private void LinhasColunasOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			((BindableBase)this).RaisePropertyChanged<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas));
		}

		private void AdicionarLinhaColuna(string descricao)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			LinhaColunaDto val = new LinhaColunaDto
			{
				Descricao = descricao
			};
			LinhasColunas.LinhasColunasCadastradas.Adicionar(val);
			DescricaoLinhaColuna = string.Empty;
			OnCadastrouLinhaColuna(val);
		}

		private bool PodeAdicionarLinhaColuna(string descricao)
		{
			if (!string.IsNullOrWhiteSpace(descricao))
			{
				return !ObterLinhasColunas().Any((LinhaColunaDto x) => HelperString.RemoveDiacritics(x.Descricao).ToLower().Equals(HelperString.RemoveDiacritics(descricao).ToLower()));
			}
			return false;
		}

		public void AjustarGrade()
		{
			List<LinhaColunaDto> linhasCadastradas = new List<LinhaColunaDto>(LinhasColunas.LinhasColunasCadastradas.Itens);
			foreach (LinhaColunaDto item in LinhasColunas.LinhasColunasSelecionadas.Itens.SelectMany((LinhaColunaViewModel selecionada) => linhasCadastradas.Where((LinhaColunaDto cadastrada) => cadastrada.Descricao.Equals(selecionada.Dto.Descricao))))
			{
				LinhasColunas.LinhasColunasCadastradas.Remover(item);
			}
			LinhasColunas.LinhasColunasCadastradas.PosicionarNoPrimeiro();
			LinhasColunas.LinhasColunasSelecionadas.OnAlterouEvent((IEnumerable<LinhaColunaViewModel>)LinhasColunas.LinhasColunasSelecionadas.Itens);
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			GradeDto val = (GradeDto)((obj is GradeDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.LinhasColunas = ObterLinhasColunas();
			}
		}

		private IEnumerable<LinhaColunaDto> ObterLinhasColunas()
		{
			return LinhasColunas.LinhasColunasCadastradas.Itens.Union(LinhasColunas.LinhasColunasSelecionadas.Itens.Select((LinhaColunaViewModel x) => x.Dto));
		}

		protected virtual void OnCadastrouLinhaColuna(LinhaColunaDto e)
		{
			this.CadastrouLinhaColuna?.Invoke(this, e);
		}

		public void SetarLinhaColunaCadastradaEvent(object sender, LinhaColunaDto linhaColunaDto)
		{
			LinhasColunas.LinhasColunasCadastradas.Adicionar(linhaColunaDto);
		}

		public override void Dispose()
		{
			((BindableBase)LinhasColunas).PropertyChanged -= LinhasColunasOnPropertyChanged;
			CadastrouLinhaColuna -= SetarLinhaColunaCadastradaEvent;
			((ViewModelValidationBase)this).Dispose();
		}
	}
	internal class LinhasAbaViewModel : AbaAuxiliarViewModelBase
	{
		public LinhasAbaViewModel(IViewModelDtoBase viewModel)
			: base("LINHAS", "Cadastro de linhas", viewModel)
		{
		}
	}
	public class LinhasViewModel : LinhasColunasViewModel
	{
		[Valid(ErrorMessage = "Selecione as Linhas")]
		public override LinhasColunas LinhasColunas
		{
			get
			{
				return ((BindableBase)this).GetProperty<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas));
			}
			set
			{
				((BindableBase)this).SetProperty<LinhasColunas>((Expression<Func<LinhasColunas>>)(() => LinhasColunas), value);
			}
		}

		public LinhasViewModel(GradeDto dto)
			: base(dto, null)
		{
			base.LinhaColuna = "Linha";
		}

		public override void ProjectedAsViewModel()
		{
			base.ProjectedAsViewModel();
			LinhasColunas.AdicionarLinhasColunasSelecionadas((from item in ((ViewModelDtoBase<GradeDto>)this).Dto.Itens.Select((ItemDto x) => x.Linha.Descricao).Distinct()
				select ((ViewModelDtoBase<GradeDto>)this).Dto.LinhasColunas.FirstOrDefault((LinhaColunaDto x) => x.Descricao.Equals(item))).ToList());
		}
	}
	public class VisualizacaoAbaViewModel : AbaAuxiliarViewModelBase
	{
		public VisualizacaoAbaViewModel(IViewModelDtoBase viewModel)
			: base("VISUALIZAÇÃO", "Visualização da grade", viewModel)
		{
		}
	}
	public class VisualizacaoViewModel : ViewModelDtoBase<GradeDto>
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

		public IEnumerable<LinhaColunaViewModel> Linhas { get; protected set; }

		public IEnumerable<LinhaColunaViewModel> Colunas { get; protected set; }

		public override bool Valido => true;

		public VisualizacaoViewModel(GradeDto dto)
			: base(dto)
		{
			Linhas = new List<LinhaColunaViewModel>();
			Colunas = new List<LinhaColunaViewModel>();
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is GradeDto) ? obj : null);
			if (obj2 != null)
			{
				((GradeDto)obj2).AdicionarItens(MontarGrade());
			}
		}

		public void SetarLinhasParaVisualizacaoEvent(object sender, IEnumerable<LinhaColunaViewModel> linhasDeGradeSelecionadas)
		{
			Linhas = linhasDeGradeSelecionadas;
		}

		public void SetarColunasParaVisualizacaoEvent(object sender, IEnumerable<LinhaColunaViewModel> linhasDeGradeSelecionadas)
		{
			Colunas = linhasDeGradeSelecionadas;
		}

		private IEnumerable<ItemDto> MontarGrade()
		{
			List<ItemDto> list = new List<ItemDto>();
			if (Linhas == null || Colunas == null)
			{
				return (IEnumerable<ItemDto>)new List<ItemDtoNull>();
			}
			foreach (LinhaColunaViewModel linha in Linhas)
			{
				list.AddRange(Colunas.Select((Func<LinhaColunaViewModel, ItemDto>)((LinhaColunaViewModel coluna) => new ItemDto
				{
					Linha = linha.Dto,
					Coluna = coluna.Dto
				})));
			}
			return list;
		}

		public override void ProjectedAsViewModel()
		{
			Descricao = base.Dto.Descricao;
		}
	}
}
namespace Otus.Modulo.Grades.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Grades.Properties.Resources", typeof(Resources).Assembly);
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

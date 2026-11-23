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
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DigisatServer.Application.Main.Dto;
using DigisatServer.Application.Main.Dto.Cadastros.Pessoas;
using DigisatServer.Application.Main.Dto.Cadastros.Usuarios;
using DigisatServer.Distributed.Wcf.Main.Contracts;
using DigisatServer.Distributed.Wcf.Main.Contracts.CadastrosModulo;
using DigisatServer.Infrastructure.Crosscutting.Attribute;
using DigisatServer.Infrastructure.Crosscutting.Constant;
using DigisatServer.Infrastructure.Crosscutting.Criptografia;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using DigisatServer.Infrastructure.Crosscutting.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Model.Sistemas;
using DigisatServer.Infrastructure.Crosscutting.Module;
using DigisatServer.Infrastructure.Crosscutting.Mvvm;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Commands;
using DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces;
using DigisatServer.Infrastructure.Crosscutting.Service;
using DigisatServer.Infrastructure.Crosscutting.ViewModel;
using Microsoft.Practices.Unity;
using Otus.Infraestrutura.Commands;
using Otus.Infraestrutura.Interfaces;
using Otus.Infraestrutura.Models;
using Otus.Infraestrutura.UserControls;
using Otus.Infraestrutura.ViewModels;
using Otus.Infraestrutura.ViewModels.Options;
using Otus.Infraestrutura.Views;
using Otus.Modulo.Usuarios.Reports;
using Otus.Modulo.Usuarios.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("Otus.Modulo.Usuarios")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("Otus.Modulo.Usuarios")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Otus.Modulo.Usuarios
{
	[Module(ModuleName = "UsuarioModulo", OnDemand = true)]
	[ModuleDependency("PessoaModulo")]
	[ModuleDependency("PerfilModulo")]
	public class UsuarioModulo : IModule
	{
		private readonly IUnityContainer _container;

		public UsuarioModulo(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			UnityExtensions.RegisterTypeForNavigation<UsuariosView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<UsuarioListaView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<UsuarioFichaAdicionarView>(_container, (string)null);
			UnityExtensions.RegisterTypeForNavigation<UsuarioFichaAtualizarView>(_container, (string)null);
		}
	}
}
namespace Otus.Modulo.Usuarios.Views
{
	public class ImpressaoDigitalPathDefault : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal Path path;

		private bool _contentLoaded;

		public ImpressaoDigitalPathDefault()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/impressaodigitalpathdefault.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				path = (Path)target;
			}
			else
			{
				_contentLoaded = true;
			}
		}
	}
	public class UsuarioFichaAdicionarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public UsuarioFichaAdicionarView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaadicionarview.xaml", UriKind.Relative);
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
	public class UsuarioFichaAtualizarView : System.Windows.Controls.UserControl, IComponentConnector
	{
		internal FichaBasePersistir FichaBase;

		private bool _contentLoaded;

		public UsuarioFichaAtualizarView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaatualizarview.xaml", UriKind.Relative);
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
	public class UsuarioListaView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public UsuarioListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariolistaview.xaml", UriKind.Relative);
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
	public class UsuariosView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public UsuariosView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariosview.xaml", UriKind.Relative);
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
namespace Otus.Modulo.Usuarios.Views.UsuarioFichaViewDetalhes
{
	public class DigiWebView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public DigiWebView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaviewdetalhes/digiwebview.xaml", UriKind.Relative);
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
	public class PerfilEmpresaFichaView : CustomCrudUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PerfilEmpresaFichaView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaviewdetalhes/perfilempresafichaview.xaml", UriKind.Relative);
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
	public class PerfilEmpresaListaView : CustomCrudUserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public PerfilEmpresaListaView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaviewdetalhes/perfilempresalistaview.xaml", UriKind.Relative);
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
	public class UsuarioFarmaciaPopularView : System.Windows.Controls.UserControl, IComponentConnector
	{
		private bool _contentLoaded;

		public UsuarioFarmaciaPopularView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaviewdetalhes/usuariofarmaciapopularview.xaml", UriKind.Relative);
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
	public class UsuarioView : CustomUserControl, IComponentConnector
	{
		internal ButtonInfo ParameterRemoverPessoa;

		internal ButtonInfo ParameterPessoa;

		private bool _contentLoaded;

		public UsuarioView()
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
				Uri resourceLocator = new Uri("/Modulo.Usuarios;component/views/usuariofichaviewdetalhes/usuarioview.xaml", UriKind.Relative);
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
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			switch (connectionId)
			{
			case 1:
				ParameterRemoverPessoa = (ButtonInfo)target;
				break;
			case 2:
				ParameterPessoa = (ButtonInfo)target;
				break;
			default:
				_contentLoaded = true;
				break;
			}
		}
	}
}
namespace Otus.Modulo.Usuarios.ViewModels
{
	public class DigiWebAbaViewModel : AbaAuxiliarViewModelBase
	{
		public DigiWebAbaViewModel(IViewModelDtoBase viewModel)
			: base("Dashboard DigiSat", "E-mail", viewModel)
		{
		}
	}
	public class DigiWebViewModel : ViewModelDtoBase<UsuarioDto>
	{
		[Required(ErrorMessage = "O campo 'E-mail' é obrigatório.")]
		[CustomValidation(typeof(DigiWebViewModel), "ValidarEmail", ErrorMessage = "O e-mail informado não é válido.")]
		public string Email
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Email));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Email), value);
			}
		}

		public DigiWebViewModel(UsuarioDto dto)
			: base(dto)
		{
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult ValidarEmail(object value, ValidationContext context)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			try
			{
				new MailAddress(text);
				return System.ComponentModel.DataAnnotations.ValidationResult.Success;
			}
			catch (Exception)
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
		}

		public override void ProjectedAsViewModel()
		{
			Email = base.Dto.DigiWeb.Email;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			UsuarioDto val = (UsuarioDto)((obj is UsuarioDto) ? obj : null);
			if (!HelperObject.IsNull((object)val))
			{
				val.DigiWeb.Email = Email;
				base.AtualizarDto<UsuarioDto>(ref val);
			}
		}
	}
	public class PerfilEmpresaAbaViewModel : AbaAuxiliarViewModelBase
	{
		public PerfilEmpresaAbaViewModel(IViewModelDtoBase viewModel)
			: base("PERFIS", "Dados principais", viewModel)
		{
		}
	}
	public class PerfilEmpresaFichaManutencaoDetalhe : FichaManutencaoDetalheBase<PerfilEmpresaDto>
	{
		private readonly IEnumerable<PerfilEmpresaDto> _lista;

		public virtual ISelecaoCommandParameter EmpresaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoInformacaoCommandParameter<PessoaDto>("Emitente", "PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto empresa)
		{
			Empresa = empresa;
		}, (ValidateParameter<PessoaDto>)(object)ValidacaoEmpresa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoEmpresa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto pessoaDto) => pessoaDto.Juridica.Emitente.IsEmitente && _lista.All((PerfilEmpresaDto a) => ((EntityBaseDto)a.Empresa).Id != ((EntityBaseDto)pessoaDto).Id)), "Selecione um cadastro do tipo 'Emitente', ou, já existe um perfil definido para o mesmo!", (TipoPessoaCadastro)14, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarEmpresaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarEmpresa);

		public virtual SelecaoCommandParameter<PerfilDto> PerfilCommandParameter => (SelecaoCommandParameter<PerfilDto>)(object)new SelecaoValidacaoInformacaoCommandParameter<PerfilDto>(HelperEnum.RetornarDescricao((Enum)(object)(TipoInformacaoPesquisa)0), "PerfilListaView", (Action<PerfilDto>)delegate(PerfilDto perfil)
		{
			Perfil = perfil;
		}, new ValidateParameter<PerfilDto>((Func<PerfilDto, bool>)((PerfilDto perfilDto) => ((EntityDto)perfilDto).Ativo), "Selecione um perfil ativo.", System.Windows.HorizontalAlignment.Center));

		public PessoaDto Empresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Empresa));
			}
			set
			{
				((BindableBase)this).SetProperty<PessoaDto>((Expression<Func<PessoaDto>>)(() => Empresa), value, (Action)OnEmpresaPerfilChanged);
			}
		}

		public string NomeEmpresa
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => NomeEmpresa));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => NomeEmpresa), value, (Action)RemoverEmpresaQuandoNomeApagado);
			}
		}

		public PerfilDto Perfil
		{
			get
			{
				return ((BindableBase)this).GetProperty<PerfilDto>((Expression<Func<PerfilDto>>)(() => Perfil));
			}
			set
			{
				((BindableBase)this).SetProperty<PerfilDto>((Expression<Func<PerfilDto>>)(() => Perfil), value, (Action)OnEmpresaPerfilChanged);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "Os campos 'Empresa' e 'Perfil' são obrigatórios.")]
		public string EmpresaPerfil
		{
			get
			{
				if ((Empresa == null || !HelperObject.Is<PessoaDtoNull>((object)Empresa)) && (Perfil == null || !HelperObject.Is<PerfilDtoNull>((object)Perfil)))
				{
					PessoaDto empresa = Empresa;
					string arg = ((empresa != null) ? empresa.Nome : null);
					PerfilDto perfil = Perfil;
					return $"{arg} - {((perfil != null) ? perfil.Descricao : null)}";
				}
				return string.Empty;
			}
		}

		public PerfilEmpresaFichaManutencaoDetalhe(PerfilEmpresaDto baseDto)
			: base(baseDto)
		{
		}

		public PerfilEmpresaFichaManutencaoDetalhe(PerfilEmpresaDto baseDto, IEnumerable<PerfilEmpresaDto> lista)
			: base(baseDto)
		{
			_lista = lista;
		}

		private async void PesquisarEmpresa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(NomeEmpresa, (TipoPessoaCadastro)14, (Action<PessoaDto>)delegate(PessoaDto empresa)
			{
				Empresa = empresa;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoEmpresa, (IEnumerable<KeyValuePair<string, object>>)null, "Emitente", (string)null);
		}

		private string ObterNomeEmpresa()
		{
			if (!HelperObject.IsNull((object)Empresa) && !HelperObject.Is<PessoaDtoNull>((object)Empresa))
			{
				return Empresa.Nome;
			}
			return string.Empty;
		}

		private void RemoverEmpresaQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(NomeEmpresa) && HelperObject.IsNotNull((object)Empresa) && HelperObject.IsNot<PessoaDtoNull>((object)Empresa))
			{
				Empresa = (PessoaDto)new PessoaDtoNull();
			}
		}

		private void OnEmpresaPerfilChanged()
		{
			NomeEmpresa = ObterNomeEmpresa();
			((BindableBase)this).RaisePropertyChanged<string>((Expression<Func<string>>)(() => EmpresaPerfil));
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			PerfilEmpresaDto val = (PerfilEmpresaDto)((obj is PerfilEmpresaDto) ? obj : null);
			if (val != null)
			{
				val.Empresa = Empresa;
				val.Perfil = Perfil;
				((ViewModelDtoBase<PerfilEmpresaDto>)(object)this).AtualizarDto<PerfilEmpresaDto>(ref val);
			}
		}

		public override void ProjectedAsViewModel()
		{
			Empresa = ((ViewModelDtoBase<PerfilEmpresaDto>)(object)this).Dto.Empresa;
			Perfil = ((ViewModelDtoBase<PerfilEmpresaDto>)(object)this).Dto.Perfil;
		}
	}
	public class PerfilEmpresaListaManutencaoDetalhe : ListaDetalheManutencaoBase<PerfilEmpresaDto, PerfilEmpresaFichaManutencaoDetalhe>
	{
		public PerfilEmpresaListaManutencaoDetalhe(IEnumerable<PerfilEmpresaDto> dtos)
			: base(dtos)
		{
		}
	}
	public class PerfilEmpresaViewModel : ViewModelDetalheBase<PerfilEmpresaDto, PerfilEmpresaListaManutencaoDetalhe, PerfilEmpresaFichaManutencaoDetalhe>
	{
		public PerfilEmpresaViewModel(List<PerfilEmpresaDto> dto)
			: base(dto)
		{
		}

		protected override PerfilEmpresaFichaManutencaoDetalhe Instanciar(PerfilEmpresaDto dto)
		{
			PerfilEmpresaFichaManutencaoDetalhe perfilEmpresaFichaManutencaoDetalhe = new PerfilEmpresaFichaManutencaoDetalhe(dto, ((ViewModelDtoBase<List<PerfilEmpresaDto>>)(object)this).Dto);
			((ViewModelBase<PerfilEmpresaDto>)(object)perfilEmpresaFichaManutencaoDetalhe).ProjectedAsViewModel();
			return perfilEmpresaFichaManutencaoDetalhe;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			object obj = dto;
			object obj2 = ((obj is UsuarioDto) ? obj : null);
			if (obj2 != null)
			{
				((UsuarioDto)obj2).AdicionarPerfis((IEnumerable<PerfilEmpresaDto>)((ViewModelDtoBase<List<PerfilEmpresaDto>>)(object)this).Dto);
			}
		}
	}
	public class UsuarioAbaViewModel : AbaPrincipalViewModelBase
	{
		public UsuarioAbaViewModel(IViewModelPrincipal viewModel)
			: base("USUÁRIO", "Dados principais", viewModel)
		{
		}
	}
	public class UsuarioFarmaciaPopularAbaViewModel : AbaAuxiliarViewModelBase
	{
		public UsuarioFarmaciaPopularAbaViewModel(IViewModelDtoBase viewModel)
			: base("Farmácia Popular", "Identificação do usuário", viewModel)
		{
		}
	}
	public class UsuarioFarmaciaPopularViewModel : ViewModelDtoBase<UsuarioFarmaciaPopularDto>
	{
		private static string _senha;

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Login' é obrigatório.")]
		public string Login
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Login));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Login), value);
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Senha' é obrigatório.")]
		public string Senha
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Senha));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Senha), value, (Action)delegate
				{
					base.Dto.Senha = Senha;
					_senha = Senha;
				});
			}
		}

		[Required(ErrorMessage = "Confirme a senha.")]
		[CustomValidation(typeof(UsuarioFarmaciaPopularViewModel), "VerificarSenha", ErrorMessage = "A senha não é igual a confirmação.")]
		public string SenhaConfirmada
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada), value);
			}
		}

		public UsuarioFarmaciaPopularViewModel(UsuarioFarmaciaPopularDto dto)
			: base(dto)
		{
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult VerificarSenha(object value, ValidationContext context)
		{
			if (value != null && value.ToString() != _senha)
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		public override void ProjectedAsViewModel()
		{
			Login = base.Dto.Login;
			Senha = base.Dto.Senha;
			SenhaConfirmada = base.Dto.Senha;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			object obj = dto;
			UsuarioDto val = (UsuarioDto)((obj is UsuarioDto) ? obj : null);
			if (val != null)
			{
				val.UsuarioFarmaciaPopular = new UsuarioFarmaciaPopularDto
				{
					Senha = Senha,
					Login = Login
				};
				base.AtualizarDto<UsuarioDto>(ref val);
			}
		}
	}
	public class UsuarioFichaAdicionarViewModel : FichaBaseAdicionar<UsuarioDto, UsuarioViewModel, IUsuarioModuleService>
	{
		private readonly IImpressaoDigitalService _impressaoDigitalService;

		public UsuarioFichaAdicionarViewModel(IWService service, IImpressaoDigitalService impressaoDigitalService)
			: base(service)
		{
			_impressaoDigitalService = impressaoDigitalService;
		}

		protected override void CriarFormulario()
		{
			((FichaBase<UsuarioDto, UsuarioViewModel>)(object)this).CriarFormulario();
			UsuarioFormulario usuarioFormulario = ((FichaBase<UsuarioDto, UsuarioViewModel>)(object)this).Formulario as UsuarioFormulario;
			if (HelperObject.IsNotNull((object)usuarioFormulario))
			{
				usuarioFormulario.SetarService(base.Service);
				usuarioFormulario.SetarImpressaoDigitalService(_impressaoDigitalService);
			}
		}
	}
	public class UsuarioFichaAtualizarViewModel : FichaBaseAtualizar<UsuarioDto, UsuarioViewModel, IUsuarioModuleService>
	{
		private readonly IImpressaoDigitalService _impressaoDigitalService;

		public UsuarioFichaAtualizarViewModel(IWService service, IImpressaoDigitalService impressaoDigitalService)
			: base(service)
		{
			_impressaoDigitalService = impressaoDigitalService;
		}

		protected override void CriarFormulario()
		{
			((FichaBase<UsuarioDto, UsuarioViewModel>)(object)this).CriarFormulario();
			UsuarioFormulario usuarioFormulario = ((FichaBase<UsuarioDto, UsuarioViewModel>)(object)this).Formulario as UsuarioFormulario;
			if (HelperObject.IsNotNull((object)usuarioFormulario))
			{
				usuarioFormulario.SetarService(base.Service);
				usuarioFormulario.SetarImpressaoDigitalService(_impressaoDigitalService);
			}
		}
	}
	public class UsuarioFormulario : FormularioEntity<UsuarioDto>
	{
		private readonly UsuarioViewModel usuarioViewModel;

		private IWService _service;

		private IImpressaoDigitalService _impressaoDigitalService;

		public UsuarioFormulario(UsuarioDto dto)
			: base(dto)
		{
			usuarioViewModel = new UsuarioViewModel(base.Dto, ObterSequenciaImpressaoDigital, ObterImpressaoDigitalService);
			usuarioViewModel.AlterouUsaFarmaciaPopularEvent += UsuarioViewModelOnAlterouUsaFarmaciaPopularEvent;
			usuarioViewModel.AlterouUsaDigiWebEvent += UsuarioViewModelOnAlterouUsaDigiWebEvent;
			if (!HelperObject.Is<UsuarioAdministradorDtoNull>((object)base.Dto.UsuarioAdministrador))
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new UsuarioAbaViewModel((IViewModelPrincipal)(object)usuarioViewModel) });
				return;
			}
			PerfilEmpresaViewModel viewModel = new PerfilEmpresaViewModel(new List<PerfilEmpresaDto>(base.Dto.Perfis));
			((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[2]
			{
				(IAbaViewModelBase)new UsuarioAbaViewModel((IViewModelPrincipal)(object)usuarioViewModel),
				(IAbaViewModelBase)new PerfilEmpresaAbaViewModel((IViewModelDtoBase)(object)viewModel)
			});
		}

		private void UsuarioViewModelOnAlterouUsaDigiWebEvent(object sender, bool usaDigiWeb)
		{
			if (usaDigiWeb)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new DigiWebAbaViewModel((IViewModelDtoBase)(object)new DigiWebViewModel(base.Dto)) });
			}
			else
			{
				((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is DigiWebAbaViewModel));
			}
		}

		private void UsuarioViewModelOnAlterouUsaFarmaciaPopularEvent(object sender, bool usaFarmaciaPopular)
		{
			((Formulario)this).RemoverAba(((Formulario)this).Abas.FirstOrDefault((IAbaViewModelBase x) => x is UsuarioFarmaciaPopularAbaViewModel));
			if (usaFarmaciaPopular)
			{
				((Formulario)this).AdicionarProjetarAbas((IAbaViewModelBase[])(object)new IAbaViewModelBase[1] { (IAbaViewModelBase)new UsuarioFarmaciaPopularAbaViewModel((IViewModelDtoBase)(object)new UsuarioFarmaciaPopularViewModel(base.Dto.UsuarioFarmaciaPopular)) });
			}
		}

		public long ObterSequenciaImpressaoDigital()
		{
			long result = 0L;
			try
			{
				result = _service.Canal<IUsuarioModuleService, long>((Func<IUsuarioModuleService, long>)((IUsuarioModuleService service) => service.ObterSequenciaImpressaoDigital()));
			}
			catch (Exception ex)
			{
				HelperException.ShowLastException(ex, "", false, false);
			}
			return result;
		}

		private IImpressaoDigitalService ObterImpressaoDigitalService()
		{
			return _impressaoDigitalService;
		}

		public override void Dispose()
		{
			usuarioViewModel.AlterouUsaFarmaciaPopularEvent -= UsuarioViewModelOnAlterouUsaFarmaciaPopularEvent;
			usuarioViewModel.AlterouUsaDigiWebEvent -= UsuarioViewModelOnAlterouUsaDigiWebEvent;
			((Formulario)this).Dispose();
		}

		public void SetarService(IWService service)
		{
			_service = service;
		}

		public void SetarImpressaoDigitalService(IImpressaoDigitalService impressaoDigitalService)
		{
			_impressaoDigitalService = impressaoDigitalService;
		}
	}
	public class UsuarioListaViewModel : ListaReferenciaAtualizar<UsuarioDto, UsuarioViewModel, IUsuarioModuleService, Usuario>
	{
		private readonly IWService _service;

		public ICommand SincronizarDigiWebCommand { get; set; }

		public ICommand ImprimirCommand { get; set; }

		public UsuarioListaViewModel(ISessao sessao, IWService service, IEntityAtivos<UsuarioDto, IUsuarioModuleService> ativos, IEntityInativos<UsuarioDto, IUsuarioModuleService> inativos)
			: base(sessao, typeof(UsuarioFichaAdicionarView).Name, typeof(UsuarioFichaAtualizarView).Name, service, new IEntityBase<UsuarioDto, IUsuarioModuleService>[2]
			{
				(IEntityBase<UsuarioDto, IUsuarioModuleService>)(object)ativos,
				(IEntityBase<UsuarioDto, IUsuarioModuleService>)(object)inativos
			})
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Expected O, but got Unknown
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Expected O, but got Unknown
			_service = service;
			ImprimirCommand = (ICommand)new DelegateCommand((Action)Imprimir, true);
			SincronizarDigiWebCommand = (ICommand)new DelegateCommand((Action)Sincronizar, (Func<bool>)PodeSincronizar, (bool?)null);
		}

		private bool PodeSincronizar()
		{
			if (HelperObject.IsNotNull((object)((ListaBase<UsuarioDto, UsuarioViewModel>)(object)this).Itens) && ((ListaBase<UsuarioDto, UsuarioViewModel>)(object)this).Itens.SelecionadoValido)
			{
				return HelperObject.IsNot<UsuarioDigiWebDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)((ListaBase<UsuarioDto, UsuarioViewModel>)(object)this).Itens.Selecionado).Dto.DigiWeb);
			}
			return false;
		}

		private void Sincronizar()
		{
			LoaderProcessoService.MostrarLoader("Processando...");
			_service.Canal<IUsuarioModuleService, Task>((Func<IUsuarioModuleService, Task>)((IUsuarioModuleService service) => service.AutenticarUsuarioDigiWebAsync(((EntityBaseDto)((ViewModelDtoBase<UsuarioDto>)(object)((ListaBase<UsuarioDto, UsuarioViewModel>)(object)this).Itens.Selecionado).Dto).Id))).ContinueWith(delegate(Task task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					DialogoFactory.MostrarDialogoConfirma("Concluído!", false, System.Windows.HorizontalAlignment.Center);
				}
			});
		}

		private void Imprimir()
		{
			if (DialogoFactory.MostrarDialogoSimNao("Esse token servirá para autorizar uma ação que o usuário logado não possui permissão. Certifique-se de compartilhá-lo apenas com os usuários designados. \n\n Para sua segurança um novo código é gerado ao imprimir. Confirmar?", false, 0, System.Windows.HorizontalAlignment.Center) != MessageBoxResult.Yes)
			{
				return;
			}
			LoaderProcessoService.MostrarLoader("Processando...");
			_service.Canal<IUsuarioModuleService, Task<UsuarioDto>>((Func<IUsuarioModuleService, Task<UsuarioDto>>)((IUsuarioModuleService service) => service.GerarToken(((ViewModelDtoBase<UsuarioDto>)(object)((ListaBase<UsuarioDto, UsuarioViewModel>)(object)this).Itens.Selecionado).Dto))).ContinueWith(delegate(Task<UsuarioDto> task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ThreadPrincipal.Executar((Action)async delegate
					{
						XtraReportExtensions.ShowPreviewDialog((IReport)(object)new Cracha(await _service.Canal<IUsuarioModuleService, Task<UsuarioDto>>((Func<IUsuarioModuleService, Task<UsuarioDto>>)((IUsuarioModuleService service) => ((IReferenciaAtualizarService<UsuarioDto>)(object)service).AtualizarDtoAsync(task.Result)))).Processar());
					});
				}
			});
		}
	}
	public class UsuariosViewModel : ModuloBase
	{
		public UsuariosViewModel(IRegionManager regionManager)
			: base(regionManager, Regioes.ModuloUsuarioRegion, typeof(UsuarioListaView).Name, (IResolucao)new ResolucaoBase())
		{
		}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown

	}
	public class UsuarioViewModel : ViewModelAtivoBase<UsuarioDto>, IViewModelPrincipal, IViewModelTitulo, IViewModelDtoBase, IViewModelBase
	{
		private static string _nome;

		private static string _senha;

		private readonly Func<long> _obterSequenciaImpressaoDigital;

		private readonly Func<IImpressaoDigitalService> _obterImpressaoDigitalService;

		public bool UtilizaLeitorBiometrico
		{
			get
			{
				if (DadosSistema.IsAdministrador)
				{
					Func<IImpressaoDigitalService> obterImpressaoDigitalService = _obterImpressaoDigitalService;
					if (obterImpressaoDigitalService == null)
					{
						return false;
					}
					return obterImpressaoDigitalService().UtilizaLeitorBiometrico;
				}
				return false;
			}
		}

		public bool MostrarCheckboxFarmaciaPopular
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarCheckboxFarmaciaPopular));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarCheckboxFarmaciaPopular), value);
			}
		}

		public bool MostrarCheckboxDigiWeb
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MostrarCheckboxDigiWeb));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MostrarCheckboxDigiWeb), value);
			}
		}

		public bool UsaFarmaciaPopular
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UsaFarmaciaPopular));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UsaFarmaciaPopular), value);
				OnAlterouUsaFarmaciaPopularEvent(UsaFarmaciaPopular);
			}
		}

		[NaoPermitirValorMenorZero(ErrorMessage = "O 'Percentual de limite de desconto' não pode ser menor que zero.")]
		public decimal LimiteDesconto
		{
			get
			{
				return ((BindableBase)this).GetProperty<decimal>((Expression<Func<decimal>>)(() => LimiteDesconto));
			}
			set
			{
				((BindableBase)this).SetProperty<decimal>((Expression<Func<decimal>>)(() => LimiteDesconto), value);
			}
		}

		public bool MostrarLimiteDesconto => DadosSistema.PodeExecutarFuncao(new Type[3]
		{
			typeof(Gerencial),
			typeof(Administrador),
			typeof(AdministradorIndustria)
		});

		public ISelecaoCommandParameter PessoaCommandParameter => (ISelecaoCommandParameter)(object)new SelecaoValidacaoCommandParameter<PessoaDto>("PessoaListaView", (Action<PessoaDto>)delegate(PessoaDto pessoa)
		{
			Pessoa = pessoa;
		}, (ValidateParameter<PessoaDto>)(object)ValidacaoPessoa);

		private ValidateParameterPessoa<PessoaDto> ValidacaoPessoa => new ValidateParameterPessoa<PessoaDto>((Func<PessoaDto, bool>)((PessoaDto x) => x.Fisica.Vendedor.IsVendedor || x.Fisica.Tecnico.IsTecnico || x.Fisica.Frentista.IsFrentista || x.Fisica.Entregador.IsEntregador || x.Fisica.ColaboradorIndustria.IsColaboradorIndustria), "Selecione uma pessoa do tipo 'Técnico', 'Vendedor', 'Frentista', 'Entregador' ou 'Colaborador da indústria!", (TipoPessoaCadastro)11, System.Windows.HorizontalAlignment.Center);

		public ICommand PesquisarPessoaCommand => (ICommand)new DelegateCommand<ICommand>((Action<ICommand>)PesquisarPessoa);

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Nome' é obrigatório.")]
		public string Nome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Nome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Nome), value, (Action)delegate
				{
					((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Nome = Nome;
					_nome = Nome;
				});
			}
		}

		[CustomValidation(typeof(UsuarioViewModel), "VerificarAcessoAlternativo", ErrorMessage = "O Nome de usuário deve ser diferente do acesso alternativo.")]
		public string AcessoAlternativo
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => AcessoAlternativo));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => AcessoAlternativo), value, (Action)delegate
				{
					((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.AcessoAlternativo = AcessoAlternativo;
				});
			}
		}

		[Required(AllowEmptyStrings = false, ErrorMessage = "O campo 'Senha' é obrigatório.")]
		public string Senha
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Senha));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Senha), value, (Action)delegate
				{
					((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Senha = Senha;
					_senha = Senha;
				});
			}
		}

		[Required(ErrorMessage = "Confirme a senha.")]
		[CustomValidation(typeof(UsuarioViewModel), "VerificarSenha", ErrorMessage = "A senha não é igual a confirmação.")]
		public string SenhaConfirmada
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => SenhaConfirmada), value);
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
					PessoaNome = Pessoa.Nome;
				});
			}
		}

		public string PessoaNome
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => PessoaNome));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => PessoaNome), value, (Action)RemoverPessoaQuandoNomeApagado);
			}
		}

		public bool UsuarioNaoPrecisaAbrirTurno
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UsuarioNaoPrecisaAbrirTurno));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UsuarioNaoPrecisaAbrirTurno), value);
			}
		}

		public bool UsaDigiWeb
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => UsaDigiWeb));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => UsaDigiWeb), value, (Action)delegate
				{
					OnAlterouUsaDigiWebEvent(UsaDigiWeb);
				});
			}
		}

		public string ImpressaoDigitalTemplate { get; set; }

		public long ImpressaoDigitalId { get; set; }

		public bool PodeAlterarNomeAcessoAlternativo => ((EntityBaseDto)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto).IsTransient();

		public string Mensagem
		{
			get
			{
				return ((BindableBase)this).GetProperty<string>((Expression<Func<string>>)(() => Mensagem));
			}
			set
			{
				((BindableBase)this).SetProperty<string>((Expression<Func<string>>)(() => Mensagem), value);
			}
		}

		public bool MensagemVisivel
		{
			get
			{
				return ((BindableBase)this).GetProperty<bool>((Expression<Func<bool>>)(() => MensagemVisivel));
			}
			set
			{
				((BindableBase)this).SetProperty<bool>((Expression<Func<bool>>)(() => MensagemVisivel), value);
			}
		}

		public BitmapSource ImagemImpressaoDigital
		{
			get
			{
				return ((BindableBase)this).GetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => ImagemImpressaoDigital));
			}
			set
			{
				((BindableBase)this).SetProperty<BitmapSource>((Expression<Func<BitmapSource>>)(() => ImagemImpressaoDigital), value);
			}
		}

		public ICommand RemoverImpressaoDigitalCommand { get; set; }

		public ICommand CadastrarImpressaoDigitalCommand { get; set; }

		public ICommand RemoverPessoaCommand { get; set; }

		public event EventHandler<bool> AlterouUsaFarmaciaPopularEvent;

		public event EventHandler<bool> AlterouUsaDigiWebEvent;

		public UsuarioViewModel()
		{
		}

		public UsuarioViewModel(UsuarioDto dto)
			: base(dto)
		{
		}

		public UsuarioViewModel(UsuarioDto dto, Func<long> obterSequenciaImpressaoDigital, Func<IImpressaoDigitalService> obterImpressaoDigitalService)
			: base(dto)
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Expected O, but got Unknown
			_obterSequenciaImpressaoDigital = obterSequenciaImpressaoDigital;
			_obterImpressaoDigitalService = obterImpressaoDigitalService;
			CadastrarImpressaoDigitalCommand = (ICommand)new AsyncCommand((Func<Task>)CadastrarImpressaoDigital, (Func<bool>)PodeCadastrarImpressaoDigital, false, (bool?)null);
			RemoverImpressaoDigitalCommand = (ICommand)new AsyncCommand((Func<Task>)RemoverImpressaoDigital, (Func<bool>)PodeRemoverImpressaoDigital, false, (bool?)null);
			RemoverPessoaCommand = (ICommand)new DelegateCommand((Action)RemoverPessoa, (Func<bool>)PodeRemoverPessoa, (bool?)null);
		}

		private bool PodeRemoverPessoa()
		{
			return !HelperObject.Is<PessoaDtoNull>((object)Pessoa);
		}

		private void RemoverPessoa()
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			Pessoa = (PessoaDto)new PessoaDtoNull();
			PessoaNome = string.Empty;
		}

		private async void PesquisarPessoa(ICommand navegarCommand)
		{
			await PesquisaPessoa.ExecutarAsync(PessoaNome, (TipoPessoaCadastro)11, (Action<PessoaDto>)delegate(PessoaDto pessoa)
			{
				Pessoa = pessoa;
			}, (Action<ISelecaoCommandParameter>)delegate(ISelecaoCommandParameter parametro)
			{
				navegarCommand.Execute(parametro);
			}, true, ValidacaoPessoa, (IEnumerable<KeyValuePair<string, object>>)null, (string)null, (string)null);
		}

		private void RemoverPessoaQuandoNomeApagado()
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			if (HelperString.IsNullOrEmpty(PessoaNome) && HelperObject.IsNotNull((object)Pessoa) && HelperObject.IsNot<PessoaDtoNull>((object)Pessoa))
			{
				Pessoa = (PessoaDto)new PessoaDtoNull();
			}
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult VerificarAcessoAlternativo(object value, ValidationContext context)
		{
			if (!string.IsNullOrWhiteSpace(value?.ToString()) && value.ToString() == _nome)
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		public static System.ComponentModel.DataAnnotations.ValidationResult VerificarSenha(object value, ValidationContext context)
		{
			if (value != null && value.ToString() != _senha)
			{
				return new System.ComponentModel.DataAnnotations.ValidationResult(string.Empty);
			}
			return System.ComponentModel.DataAnnotations.ValidationResult.Success;
		}

		public override void ProjectedAsViewModel()
		{
			VerificarConfiguracaoDashboard();
			base.Ativo = ((EntityDto)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto).Ativo;
			Nome = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Nome;
			UsuarioNaoPrecisaAbrirTurno = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioNaoPrecisaAbrirTurno;
			AcessoAlternativo = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.AcessoAlternativo;
			Senha = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Senha;
			SenhaConfirmada = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Senha;
			Pessoa = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.Pessoa;
			MostrarCheckboxFarmaciaPopular = DadosSistema.IsFarmacia && HelperObject.IsExactlyObject<UsuarioAdministradorDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioAdministrador);
			MostrarCheckboxDigiWeb = HelperObject.IsExactlyObject<UsuarioAdministradorDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioAdministrador);
			UsaFarmaciaPopular = HelperObject.IsExactlyObject<UsuarioFarmaciaPopularDto>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioFarmaciaPopular);
			if (!string.IsNullOrWhiteSpace(((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.ImpressaoDigitalTemplate))
			{
				ImpressaoDigitalTemplate = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.ImpressaoDigitalTemplate;
				ImpressaoDigitalId = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.ImpressaoDigitalId;
				ImagemImpressaoDigital = HelperImage.ToBitmapSource(HelperImage.Decompress(((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.ImpressaoDigitalBytes));
			}
			MensagemVisivel = false;
			UsaDigiWeb = HelperObject.IsNot<UsuarioDigiWebDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.DigiWeb);
			LimiteDesconto = ((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.LimiteDesconto;
		}

		public override void AtualizarDto<T>(ref T dto)
		{
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Expected O, but got Unknown
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Expected O, but got Unknown
			object obj = dto;
			UsuarioDto val = (UsuarioDto)((obj is UsuarioDto) ? obj : null);
			if (val != null)
			{
				((EntityDto)val).Ativo = base.Ativo;
				val.Nome = Nome;
				val.UsuarioNaoPrecisaAbrirTurno = UsuarioNaoPrecisaAbrirTurno;
				val.AcessoAlternativo = AcessoAlternativo;
				val.Senha = Senha;
				val.Pessoa = Pessoa;
				if (!UsaFarmaciaPopular)
				{
					val.UsuarioFarmaciaPopular = (UsuarioFarmaciaPopularDto)new UsuarioFarmaciaPopularDtoNull();
				}
				val.ImpressaoDigitalId = ImpressaoDigitalId;
				val.ImpressaoDigitalTemplate = HelperString.ToStringEmptyWhenNull(ImpressaoDigitalTemplate);
				val.ImpressaoDigitalBytes = HelperImage.Compress(ImagemImpressaoDigital);
				val.DigiWeb = (UsuarioDigiWebDto)(UsaDigiWeb ? new UsuarioDigiWebDto() : new UsuarioDigiWebDtoNull());
				val.LimiteDesconto = LimiteDesconto;
				((ViewModelDtoBase<UsuarioDto>)(object)this).AtualizarDto<UsuarioDto>(ref val);
			}
		}

		private void VerificarConfiguracaoDashboard()
		{
			new Thread((ThreadStart)delegate
			{
				try
				{
					MostrarCheckboxDigiWeb = false;
					if (HelperObject.IsExactlyObject<UsuarioAdministradorDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioAdministrador) && HelperObject.IsExactlyObject<UsuarioFarmaciaPopularDtoNull>((object)((ViewModelDtoBase<UsuarioDto>)(object)this).Dto.UsuarioFarmaciaPopular))
					{
						MostrarCheckboxDigiWeb = DadosSistema.GetService().Canal<IUsuarioModuleService, bool>((Func<IUsuarioModuleService, bool>)((IUsuarioModuleService service) => service.IsDashboardHabilitado()));
					}
				}
				catch (Exception)
				{
				}
			}).Start();
		}

		protected virtual void OnAlterouUsaFarmaciaPopularEvent(bool e)
		{
			this.AlterouUsaFarmaciaPopularEvent?.Invoke(this, e);
		}

		private bool PodeCadastrarImpressaoDigital()
		{
			if (DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return ImpressaoDigitalId == 0;
			}
			return false;
		}

		private Task CadastrarImpressaoDigital()
		{
			long idImpressaoDigital = _obterSequenciaImpressaoDigital?.Invoke() ?? 0;
			if (idImpressaoDigital == 0L)
			{
				return Task.CompletedTask;
			}
			return Task.Run(delegate
			{
				LoaderProcessoService.MostrarLoader("Cadastrando impressão digital...");
				MensagemVisivel = true;
				_obterImpressaoDigitalService().MensagemEvent += LeitorBiometricoOnMensagemEvent;
				return _obterImpressaoDigitalService().Cadastrar(idImpressaoDigital);
			}).ContinueWith(delegate(Task<TemplateImpressaoDigital> task)
			{
				LoaderProcessoService.EsconderLoader();
				_obterImpressaoDigitalService().MensagemEvent -= LeitorBiometricoOnMensagemEvent;
				MensagemVisivel = false;
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					TemplateImpressaoDigital templateImpressaoDigital = task.Result;
					ImpressaoDigitalTemplate = templateImpressaoDigital.Template;
					ImpressaoDigitalId = templateImpressaoDigital.Id;
					ThreadPrincipal.Executar((Action)delegate
					{
						ImagemImpressaoDigital = HelperImage.ToBitmapSource(templateImpressaoDigital.ImpressaoDigital.Bytes);
					});
				}
			});
		}

		private void LeitorBiometricoOnMensagemEvent(object sender, string mensagem)
		{
			ThreadPrincipal.Executar((Action)delegate
			{
				Mensagem = mensagem;
			});
		}

		private bool PodeRemoverImpressaoDigital()
		{
			if (DadosSistema.PodeExecutarFuncao(new Type[2]
			{
				typeof(Administrador),
				typeof(AdministradorIndustria)
			}))
			{
				return ImpressaoDigitalId > 0;
			}
			return false;
		}

		private Task RemoverImpressaoDigital()
		{
			return Task.Run(delegate
			{
				LoaderProcessoService.MostrarLoader("Processando...");
				_obterImpressaoDigitalService().Remover(ImpressaoDigitalId);
			}).ContinueWith(delegate(Task task)
			{
				LoaderProcessoService.EsconderLoader();
				if (task.IsFaulted)
				{
					HelperException.ShowLastException(task.Exception, "", false, false);
				}
				else
				{
					ImpressaoDigitalTemplate = null;
					ImpressaoDigitalId = 0L;
					ThreadPrincipal.Executar((Action)delegate
					{
						ImagemImpressaoDigital = null;
					});
				}
			});
		}

		public IFormulario CriarFormulario()
		{
			return (IFormulario)(object)new UsuarioFormulario(((ViewModelDtoBase<UsuarioDto>)(object)this).Dto);
		}

		public string ToTituloFicha()
		{
			return "Usuário";
		}

		public string ToTituloLista()
		{
			return "Usuários";
		}

		protected virtual void OnAlterouUsaDigiWebEvent(bool e)
		{
			this.AlterouUsaDigiWebEvent?.Invoke(this, e);
		}
	}
}
namespace Otus.Modulo.Usuarios.Reports
{
	public class Cracha : XtraReport
	{
		private readonly UsuarioDto _usuario;

		private IContainer components;

		private DetailBand Detail;

		private TopMarginBand TopMargin;

		private BottomMarginBand BottomMargin;

		private BindingSource bindingSource1;

		private PrintableComponentContainer printableComponentContainer2;

		private XRLabel xrLabel1;

		private XRBarCode BarCode2;

		private XRLabel xrLabel2;

		private XRBarCode BarCode1;

		private PrintableComponentContainer printableComponentContainer1;

		private XRPictureBox FotoPerfil;

		private ImageCollection imageCollection1;

		private PrintableComponentContainer printableComponentContainer4;

		private XRBarCode BarCode3;

		private XRLabel xrLabel3;

		private XRPictureBox FotoPerfil1;

		private XRPictureBox logotipoEmitente;

		private XRPictureBox FotoPerfil2;

		public Cracha(UsuarioDto usuario)
		{
			_usuario = usuario;
			InitializeComponent();
		}

		public XtraReport Processar()
		{
			try
			{
				((XtraReportBase)this).DataSource = new List<UsuarioDto> { _usuario };
				((XtraReport)this).ShowPrintMarginsWarning = false;
				ExibirLogotipo();
				ExibirFoto();
				ObterToken();
				((XtraReport)this).CreateDocument();
			}
			catch (Exception ex)
			{
				DialogoFactory.MostrarDialogoConfirma(ex.Message, false, System.Windows.HorizontalAlignment.Center);
			}
			return (XtraReport)(object)this;
		}

		private void ExibirFoto()
		{
			if (HelperObject.IsNotNull((object)_usuario.Pessoa.Imagem) && _usuario.Pessoa.Imagem.Length != 0)
			{
				FotoPerfil.Image = HelperImage.ToImage(HelperImage.Decompress(_usuario.Pessoa.Imagem));
				FotoPerfil1.Image = HelperImage.ToImage(HelperImage.Decompress(_usuario.Pessoa.Imagem));
				FotoPerfil2.Image = HelperImage.ToImage(HelperImage.Decompress(_usuario.Pessoa.Imagem));
			}
		}

		private void ObterToken()
		{
			string text = TripleDes.Decifrar(_usuario.Token, Hash.RetornarChavePrivadaInformacaoDecifrada(), CipherMode.ECB, PaddingMode.PKCS7);
			((XRControl)BarCode1).Text = text;
			((XRControl)BarCode2).Text = text;
			((XRControl)BarCode3).Text = text;
		}

		private void ExibirLogotipo()
		{
			if (HelperObject.IsNotNull((object)DadosSistema.EmpresaLogada.Imagem) && DadosSistema.EmpresaLogada.Imagem.Length != 0)
			{
				logotipoEmitente.Image = HelperImage.ToImage(HelperImage.Decompress(DadosSistema.EmpresaLogada.Imagem));
			}
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
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Expected O, but got Unknown
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Expected O, but got Unknown
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Expected O, but got Unknown
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Expected O, but got Unknown
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Expected O, but got Unknown
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Expected O, but got Unknown
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Expected O, but got Unknown
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bc: Expected O, but got Unknown
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Expected O, but got Unknown
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Expected O, but got Unknown
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0328: Unknown result type (might be due to invalid IL or missing references)
			//IL_0363: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Expected O, but got Unknown
			//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0419: Unknown result type (might be due to invalid IL or missing references)
			//IL_0498: Unknown result type (might be due to invalid IL or missing references)
			//IL_0507: Unknown result type (might be due to invalid IL or missing references)
			//IL_050d: Expected O, but got Unknown
			//IL_0532: Unknown result type (might be due to invalid IL or missing references)
			//IL_055b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_0684: Unknown result type (might be due to invalid IL or missing references)
			//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0721: Unknown result type (might be due to invalid IL or missing references)
			//IL_0727: Expected O, but got Unknown
			//IL_074c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0775: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0866: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_095c: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f7: Expected O, but got Unknown
			//IL_0a24: Unknown result type (might be due to invalid IL or missing references)
			components = new Container();
			Code128Generator val = new Code128Generator();
			Code128Generator val2 = new Code128Generator();
			Code128Generator val3 = new Code128Generator();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Cracha));
			Detail = new DetailBand();
			FotoPerfil1 = new XRPictureBox();
			logotipoEmitente = new XRPictureBox();
			BarCode3 = new XRBarCode();
			xrLabel3 = new XRLabel();
			FotoPerfil = new XRPictureBox();
			xrLabel2 = new XRLabel();
			BarCode1 = new XRBarCode();
			BarCode2 = new XRBarCode();
			xrLabel1 = new XRLabel();
			printableComponentContainer2 = new PrintableComponentContainer();
			printableComponentContainer1 = new PrintableComponentContainer();
			printableComponentContainer4 = new PrintableComponentContainer();
			TopMargin = new TopMarginBand();
			BottomMargin = new BottomMarginBand();
			bindingSource1 = new BindingSource(components);
			imageCollection1 = new ImageCollection(components);
			FotoPerfil2 = new XRPictureBox();
			((ISupportInitialize)bindingSource1).BeginInit();
			((ISupportInitialize)imageCollection1).BeginInit();
			((ISupportInitialize)this).BeginInit();
			((XRControl)Detail).Controls.AddRange((XRControl[])(object)new XRControl[13]
			{
				(XRControl)FotoPerfil2,
				(XRControl)FotoPerfil1,
				(XRControl)logotipoEmitente,
				(XRControl)BarCode3,
				(XRControl)xrLabel3,
				(XRControl)FotoPerfil,
				(XRControl)xrLabel2,
				(XRControl)BarCode1,
				(XRControl)BarCode2,
				(XRControl)xrLabel1,
				(XRControl)printableComponentContainer2,
				(XRControl)printableComponentContainer1,
				(XRControl)printableComponentContainer4
			});
			((XRControl)Detail).Dpi = 254f;
			((XRControl)Detail).HeightF = 1732.104f;
			((XRControl)Detail).Name = "Detail";
			((XRControl)Detail).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)Detail).TextAlignment = (TextAlignment)1;
			((XRControl)FotoPerfil1).Borders = (BorderSide)15;
			((XRControl)FotoPerfil1).Dpi = 254f;
			((XRControl)FotoPerfil1).LocationFloat = new PointFloat(208.7478f, 778.2626f);
			((XRControl)FotoPerfil1).Name = "FotoPerfil1";
			((XRControl)FotoPerfil1).SizeF = new SizeF(200f, 200f);
			FotoPerfil1.Sizing = (ImageSizeMode)1;
			((XRControl)FotoPerfil1).StylePriority.UseBorders = false;
			((XRControl)logotipoEmitente).Borders = (BorderSide)15;
			((XRControl)logotipoEmitente).Dpi = 254f;
			((XRControl)logotipoEmitente).LocationFloat = new PointFloat(49.47921f, 477.3468f);
			((XRControl)logotipoEmitente).Name = "logotipoEmitente";
			((XRControl)logotipoEmitente).SizeF = new SizeF(512.9482f, 284.1483f);
			logotipoEmitente.Sizing = (ImageSizeMode)1;
			((XRControl)logotipoEmitente).StylePriority.UseBorders = false;
			BarCode3.AutoModule = true;
			((XRControl)BarCode3).Dpi = 254f;
			((XRControl)BarCode3).LocationFloat = new PointFloat(49.47921f, 1147.833f);
			BarCode3.Module = 5.08f;
			((XRControl)BarCode3).Name = "BarCode3";
			((XRControl)BarCode3).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			BarCode3.ShowText = false;
			((XRControl)BarCode3).SizeF = new SizeF(512.9482f, 147.0554f);
			val.CharacterSet = (Code128Charset)104;
			BarCode3.Symbology = (BarCodeGeneratorBase)(object)val;
			((XRControl)xrLabel3).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Nome")
			});
			((XRControl)xrLabel3).Dpi = 254f;
			((XRControl)xrLabel3).LocationFloat = new PointFloat(49.47921f, 992.8402f);
			((XRControl)xrLabel3).Name = "xrLabel3";
			((XRControl)xrLabel3).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel3).SizeF = new SizeF(512.9482f, 154.993f);
			((XRControl)xrLabel3).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel3).Text = "xrLabel1";
			((XRControl)xrLabel3).TextAlignment = (TextAlignment)32;
			((XRControl)FotoPerfil).Borders = (BorderSide)15;
			((XRControl)FotoPerfil).Dpi = 254f;
			((XRControl)FotoPerfil).LocationFloat = new PointFloat(49.47921f, 24.99589f);
			((XRControl)FotoPerfil).Name = "FotoPerfil";
			((XRControl)FotoPerfil).SizeF = new SizeF(290.94f, 290.07f);
			FotoPerfil.Sizing = (ImageSizeMode)1;
			((XRControl)FotoPerfil).StylePriority.UseBorders = false;
			((XRControl)xrLabel2).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Nome")
			});
			((XRControl)xrLabel2).Dpi = 254f;
			((XRControl)xrLabel2).LocationFloat = new PointFloat(363.6042f, 25.00001f);
			((XRControl)xrLabel2).Name = "xrLabel2";
			((XRControl)xrLabel2).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel2).SizeF = new SizeF(582.6246f, 121.92f);
			((XRControl)xrLabel2).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel2).Text = "xrLabel1";
			((XRControl)xrLabel2).TextAlignment = (TextAlignment)32;
			BarCode1.AutoModule = true;
			((XRControl)BarCode1).Dpi = 254f;
			((XRControl)BarCode1).LocationFloat = new PointFloat(363.6042f, 171.9792f);
			BarCode1.Module = 5.08f;
			((XRControl)BarCode1).Name = "BarCode1";
			((XRControl)BarCode1).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			BarCode1.ShowText = false;
			((XRControl)BarCode1).SizeF = new SizeF(582.6246f, 143.0867f);
			val2.CharacterSet = (Code128Charset)104;
			BarCode1.Symbology = (BarCodeGeneratorBase)(object)val2;
			BarCode2.AutoModule = true;
			((XRControl)BarCode2).Dpi = 254f;
			((XRControl)BarCode2).LocationFloat = new PointFloat(1099.044f, 322.7917f);
			BarCode2.Module = 5.08f;
			((XRControl)BarCode2).Name = "BarCode2";
			((XRControl)BarCode2).Padding = new PaddingInfo(25, 25, 0, 0, 254f);
			BarCode2.ShowText = false;
			((XRControl)BarCode2).SizeF = new SizeF(815.458f, 193.3576f);
			val3.CharacterSet = (Code128Charset)104;
			BarCode2.Symbology = (BarCodeGeneratorBase)(object)val3;
			((XRControl)xrLabel1).DataBindings.AddRange((XRBinding[])(object)new XRBinding[1]
			{
				new XRBinding("Text", (object)null, "Nome")
			});
			((XRControl)xrLabel1).Dpi = 254f;
			((XRControl)xrLabel1).LocationFloat = new PointFloat(1366.273f, 47.62496f);
			((XRControl)xrLabel1).Name = "xrLabel1";
			((XRControl)xrLabel1).Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			((XRControl)xrLabel1).SizeF = new SizeF(548.2288f, 246.2742f);
			((XRControl)xrLabel1).StylePriority.UseTextAlignment = false;
			((XRControl)xrLabel1).Text = "xrLabel1";
			((XRControl)xrLabel1).TextAlignment = (TextAlignment)32;
			((XRControl)printableComponentContainer2).Borders = (BorderSide)15;
			((XRControl)printableComponentContainer2).Dpi = 254f;
			((XRControl)printableComponentContainer2).LocationFloat = new PointFloat(1076f, 13.22917f);
			((XRControl)printableComponentContainer2).Name = "printableComponentContainer2";
			((XRControl)printableComponentContainer2).SizeF = new SizeF(856f, 540f);
			((XRControl)printableComponentContainer2).StylePriority.UseBorders = false;
			((XRControl)printableComponentContainer1).Borders = (BorderSide)15;
			((XRControl)printableComponentContainer1).Dpi = 254f;
			((XRControl)printableComponentContainer1).LocationFloat = new PointFloat(25.00001f, 13.22917f);
			((XRControl)printableComponentContainer1).Name = "printableComponentContainer1";
			((XRControl)printableComponentContainer1).SizeF = new SizeF(952.5f, 322.7917f);
			((XRControl)printableComponentContainer1).StylePriority.UseBorders = false;
			((XRControl)printableComponentContainer4).Borders = (BorderSide)15;
			((XRControl)printableComponentContainer4).Dpi = 254f;
			((XRControl)printableComponentContainer4).LocationFloat = new PointFloat(36.83664f, 455.9478f);
			((XRControl)printableComponentContainer4).Name = "printableComponentContainer4";
			((XRControl)printableComponentContainer4).SizeF = new SizeF(540f, 856f);
			((XRControl)printableComponentContainer4).StylePriority.UseBorders = false;
			((XRControl)TopMargin).Dpi = 254f;
			((XRControl)TopMargin).HeightF = 101f;
			((XRControl)TopMargin).Name = "TopMargin";
			((XRControl)TopMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)TopMargin).TextAlignment = (TextAlignment)1;
			((XRControl)BottomMargin).Dpi = 254f;
			((XRControl)BottomMargin).HeightF = 17f;
			((XRControl)BottomMargin).Name = "BottomMargin";
			((XRControl)BottomMargin).Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			((XRControl)BottomMargin).TextAlignment = (TextAlignment)1;
			bindingSource1.DataSource = typeof(UsuarioDto);
			imageCollection1.ImageStream = (ImageCollectionStreamer)componentResourceManager.GetObject("imageCollection1.ImageStream");
			((XRControl)FotoPerfil2).Borders = (BorderSide)15;
			((XRControl)FotoPerfil2).Dpi = 254f;
			((XRControl)FotoPerfil2).LocationFloat = new PointFloat(1119.999f, 47.62496f);
			((XRControl)FotoPerfil2).Name = "FotoPerfil2";
			((XRControl)FotoPerfil2).SizeF = new SizeF(246.2741f, 246.2741f);
			FotoPerfil2.Sizing = (ImageSizeMode)1;
			((XRControl)FotoPerfil2).StylePriority.UseBorders = false;
			((XtraReportBase)this).Bands.AddRange((Band[])(object)new Band[3]
			{
				(Band)Detail,
				(Band)TopMargin,
				(Band)BottomMargin
			});
			((XtraReportBase)this).DataSource = bindingSource1;
			((XRControl)this).Dpi = 254f;
			((XRControl)this).Font = new Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
			((XtraReport)this).Margins = new Margins(101, 42, 101, 17);
			((XtraReport)this).PageHeight = 2970;
			((XtraReport)this).PageWidth = 2100;
			((XtraReport)this).PaperKind = PaperKind.A4;
			((XtraReport)this).ReportUnit = (ReportUnit)1;
			((XtraReport)this).Version = "15.2";
			((ISupportInitialize)bindingSource1).EndInit();
			((ISupportInitialize)imageCollection1).EndInit();
			((ISupportInitialize)this).EndInit();
		}
	}
}
namespace Otus.Modulo.Usuarios.Properties
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
					resourceMan = new ResourceManager("Otus.Modulo.Usuarios.Properties.Resources", typeof(Resources).Assembly);
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

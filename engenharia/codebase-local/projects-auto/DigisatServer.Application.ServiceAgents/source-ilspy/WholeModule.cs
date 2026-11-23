using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.ServiceModel;
using System.ServiceModel.Channels;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyTitle("DigisatServer.Application.ServiceAgents")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Digisat Tecnologia Ltda")]
[assembly: AssemblyProduct("DigisatServer.Application.ServiceAgents")]
[assembly: AssemblyCopyright("Copyright ©  2015")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("23b368d0-e0ef-4209-b89c-6faf56c0c1d4")]
[assembly: AssemblyFileVersion("2.0.26307.4")]
[assembly: TargetFramework(".NETFramework,Version=v4.6", FrameworkDisplayName = ".NET Framework 4.6")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace DigisatServer.Application.ServiceAgents.ServiceReferenceHabilitacao;

[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ClienteRevendaCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ClienteRevendaCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private EcfCompletoDto[] EcfsField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private ModuloCompletoDto[] ModulosField;

	[OptionalField]
	private PerfilCompletoDto PerfilField;

	[OptionalField]
	private PessoaJuridicaCompletaDto PessoaJuridicaField;

	[OptionalField]
	private RamoDeAtividade RamoAtividadeField;

	[OptionalField]
	private SatCompletoDto[] SatsField;

	[OptionalField]
	private TipoPontoDeVenda TipoAtividadePontoDeVendaField;

	[OptionalField]
	private TipoMesaContaCliente? TipoAtividadeRestauranteField;

	[OptionalField]
	private TipoCadastro TipoCadastroField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public EcfCompletoDto[] Ecfs
	{
		get
		{
			return EcfsField;
		}
		set
		{
			if (EcfsField != value)
			{
				EcfsField = value;
				RaisePropertyChanged("Ecfs");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public ModuloCompletoDto[] Modulos
	{
		get
		{
			return ModulosField;
		}
		set
		{
			if (ModulosField != value)
			{
				ModulosField = value;
				RaisePropertyChanged("Modulos");
			}
		}
	}

	[DataMember]
	public PerfilCompletoDto Perfil
	{
		get
		{
			return PerfilField;
		}
		set
		{
			if (PerfilField != value)
			{
				PerfilField = value;
				RaisePropertyChanged("Perfil");
			}
		}
	}

	[DataMember]
	public PessoaJuridicaCompletaDto PessoaJuridica
	{
		get
		{
			return PessoaJuridicaField;
		}
		set
		{
			if (PessoaJuridicaField != value)
			{
				PessoaJuridicaField = value;
				RaisePropertyChanged("PessoaJuridica");
			}
		}
	}

	[DataMember]
	public RamoDeAtividade RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if (!RamoAtividadeField.Equals(value))
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	[DataMember]
	public SatCompletoDto[] Sats
	{
		get
		{
			return SatsField;
		}
		set
		{
			if (SatsField != value)
			{
				SatsField = value;
				RaisePropertyChanged("Sats");
			}
		}
	}

	[DataMember]
	public TipoPontoDeVenda TipoAtividadePontoDeVenda
	{
		get
		{
			return TipoAtividadePontoDeVendaField;
		}
		set
		{
			if (!TipoAtividadePontoDeVendaField.Equals(value))
			{
				TipoAtividadePontoDeVendaField = value;
				RaisePropertyChanged("TipoAtividadePontoDeVenda");
			}
		}
	}

	[DataMember]
	public TipoMesaContaCliente? TipoAtividadeRestaurante
	{
		get
		{
			return TipoAtividadeRestauranteField;
		}
		set
		{
			if (!TipoAtividadeRestauranteField.Equals(value))
			{
				TipoAtividadeRestauranteField = value;
				RaisePropertyChanged("TipoAtividadeRestaurante");
			}
		}
	}

	[DataMember]
	public TipoCadastro TipoCadastro
	{
		get
		{
			return TipoCadastroField;
		}
		set
		{
			if (!TipoCadastroField.Equals(value))
			{
				TipoCadastroField = value;
				RaisePropertyChanged("TipoCadastro");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "PerfilCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class PerfilCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private char CodigoPerfilField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private UfCompletoDto UfField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public char CodigoPerfil
	{
		get
		{
			return CodigoPerfilField;
		}
		set
		{
			if (!CodigoPerfilField.Equals(value))
			{
				CodigoPerfilField = value;
				RaisePropertyChanged("CodigoPerfil");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public UfCompletoDto Uf
	{
		get
		{
			return UfField;
		}
		set
		{
			if (UfField != value)
			{
				UfField = value;
				RaisePropertyChanged("Uf");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "PessoaJuridicaCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class PessoaJuridicaCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private bool AtivoField;

	[OptionalField]
	private DocumentoCompletoDto CnaeField;

	[OptionalField]
	private DocumentoCompletoDto DocumentoField;

	[OptionalField]
	private EmailCompletoDto EmailField;

	[OptionalField]
	private EnderecoCompletoDto EnderecoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private DocumentoCompletoDto InscricaoEstadualField;

	[OptionalField]
	private string RazaoSocialField;

	[OptionalField]
	private DocumentoCompletoDto RegistroMunicipalField;

	[OptionalField]
	private TelefoneCompletoDto TelefoneField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public bool Ativo
	{
		get
		{
			return AtivoField;
		}
		set
		{
			if (!AtivoField.Equals(value))
			{
				AtivoField = value;
				RaisePropertyChanged("Ativo");
			}
		}
	}

	[DataMember]
	public DocumentoCompletoDto Cnae
	{
		get
		{
			return CnaeField;
		}
		set
		{
			if (CnaeField != value)
			{
				CnaeField = value;
				RaisePropertyChanged("Cnae");
			}
		}
	}

	[DataMember]
	public DocumentoCompletoDto Documento
	{
		get
		{
			return DocumentoField;
		}
		set
		{
			if (DocumentoField != value)
			{
				DocumentoField = value;
				RaisePropertyChanged("Documento");
			}
		}
	}

	[DataMember]
	public EmailCompletoDto Email
	{
		get
		{
			return EmailField;
		}
		set
		{
			if (EmailField != value)
			{
				EmailField = value;
				RaisePropertyChanged("Email");
			}
		}
	}

	[DataMember]
	public EnderecoCompletoDto Endereco
	{
		get
		{
			return EnderecoField;
		}
		set
		{
			if (EnderecoField != value)
			{
				EnderecoField = value;
				RaisePropertyChanged("Endereco");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public DocumentoCompletoDto InscricaoEstadual
	{
		get
		{
			return InscricaoEstadualField;
		}
		set
		{
			if (InscricaoEstadualField != value)
			{
				InscricaoEstadualField = value;
				RaisePropertyChanged("InscricaoEstadual");
			}
		}
	}

	[DataMember]
	public string RazaoSocial
	{
		get
		{
			return RazaoSocialField;
		}
		set
		{
			if ((object)RazaoSocialField != value)
			{
				RazaoSocialField = value;
				RaisePropertyChanged("RazaoSocial");
			}
		}
	}

	[DataMember]
	public DocumentoCompletoDto RegistroMunicipal
	{
		get
		{
			return RegistroMunicipalField;
		}
		set
		{
			if (RegistroMunicipalField != value)
			{
				RegistroMunicipalField = value;
				RaisePropertyChanged("RegistroMunicipal");
			}
		}
	}

	[DataMember]
	public TelefoneCompletoDto Telefone
	{
		get
		{
			return TelefoneField;
		}
		set
		{
			if (TelefoneField != value)
			{
				TelefoneField = value;
				RaisePropertyChanged("Telefone");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EcfCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class EcfCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private bool AtivoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private MarcaEcf MarcaField;

	[OptionalField]
	private EcfTipoModelos ModeloField;

	[OptionalField]
	private string NumeroDeSerieField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public bool Ativo
	{
		get
		{
			return AtivoField;
		}
		set
		{
			if (!AtivoField.Equals(value))
			{
				AtivoField = value;
				RaisePropertyChanged("Ativo");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public MarcaEcf Marca
	{
		get
		{
			return MarcaField;
		}
		set
		{
			if (!MarcaField.Equals(value))
			{
				MarcaField = value;
				RaisePropertyChanged("Marca");
			}
		}
	}

	[DataMember]
	public EcfTipoModelos Modelo
	{
		get
		{
			return ModeloField;
		}
		set
		{
			if (!ModeloField.Equals(value))
			{
				ModeloField = value;
				RaisePropertyChanged("Modelo");
			}
		}
	}

	[DataMember]
	public string NumeroDeSerie
	{
		get
		{
			return NumeroDeSerieField;
		}
		set
		{
			if ((object)NumeroDeSerieField != value)
			{
				NumeroDeSerieField = value;
				RaisePropertyChanged("NumeroDeSerie");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ModuloCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ModuloCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int CodigoField;

	[OptionalField]
	private DateTime DataHoraField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[OptionalField]
	private string NumeroNotaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Codigo
	{
		get
		{
			return CodigoField;
		}
		set
		{
			if (!CodigoField.Equals(value))
			{
				CodigoField = value;
				RaisePropertyChanged("Codigo");
			}
		}
	}

	[DataMember]
	public DateTime DataHora
	{
		get
		{
			return DataHoraField;
		}
		set
		{
			if (!DataHoraField.Equals(value))
			{
				DataHoraField = value;
				RaisePropertyChanged("DataHora");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	[DataMember]
	public string NumeroNota
	{
		get
		{
			return NumeroNotaField;
		}
		set
		{
			if ((object)NumeroNotaField != value)
			{
				NumeroNotaField = value;
				RaisePropertyChanged("NumeroNota");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "RamoDeAtividade", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum RamoDeAtividade
{
	[EnumMember]
	Comercio,
	[EnumMember]
	Farmacia,
	[EnumMember]
	PostoCombustivel,
	[EnumMember]
	Restaurante
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SatCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SatCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private bool AtivoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private MarcaSat MarcaField;

	[OptionalField]
	private ModeloSat ModeloField;

	[OptionalField]
	private string NumeroDeSerieField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public bool Ativo
	{
		get
		{
			return AtivoField;
		}
		set
		{
			if (!AtivoField.Equals(value))
			{
				AtivoField = value;
				RaisePropertyChanged("Ativo");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public MarcaSat Marca
	{
		get
		{
			return MarcaField;
		}
		set
		{
			if (!MarcaField.Equals(value))
			{
				MarcaField = value;
				RaisePropertyChanged("Marca");
			}
		}
	}

	[DataMember]
	public ModeloSat Modelo
	{
		get
		{
			return ModeloField;
		}
		set
		{
			if (!ModeloField.Equals(value))
			{
				ModeloField = value;
				RaisePropertyChanged("Modelo");
			}
		}
	}

	[DataMember]
	public string NumeroDeSerie
	{
		get
		{
			return NumeroDeSerieField;
		}
		set
		{
			if ((object)NumeroDeSerieField != value)
			{
				NumeroDeSerieField = value;
				RaisePropertyChanged("NumeroDeSerie");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "TipoPontoDeVenda", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum TipoPontoDeVenda
{
	[EnumMember]
	Normal,
	[EnumMember]
	Nfce,
	[EnumMember]
	Sat,
	[EnumMember]
	Mfe
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "TipoMesaContaCliente", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum TipoMesaContaCliente
{
	[EnumMember]
	Indefinido,
	[EnumMember]
	Mesa,
	[EnumMember]
	ContaCliente
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "TipoCadastro", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum TipoCadastro
{
	[EnumMember]
	Matriz,
	[EnumMember]
	Filial
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "MarcaEcf", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum MarcaEcf
{
	[EnumMember]
	Indefinida,
	[EnumMember]
	Bematech,
	[EnumMember]
	Daruma,
	[EnumMember]
	Epson,
	[EnumMember]
	Elgin,
	[EnumMember]
	Sweda,
	[EnumMember]
	Zpm
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EcfTipo.Modelos", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkEcfDigisat.Types")]
public enum EcfTipoModelos
{
	[EnumMember]
	Default,
	[EnumMember]
	MP2000,
	[EnumMember]
	MP2100,
	[EnumMember]
	MP3000,
	[EnumMember]
	MP4000,
	[EnumMember]
	MP4200,
	[EnumMember]
	MP6000,
	[EnumMember]
	MP7000,
	[EnumMember]
	FITLOGGERII,
	[EnumMember]
	QUICK,
	[EnumMember]
	MT100,
	[EnumMember]
	TMT81FBII,
	[EnumMember]
	TMT81FBIII,
	[EnumMember]
	TMT88FBII,
	[EnumMember]
	TMT88FBIII,
	[EnumMember]
	TMT800F,
	[EnumMember]
	TMH6000FBII,
	[EnumMember]
	TMH6000FBIII,
	[EnumMember]
	TMT900F,
	[EnumMember]
	FS600,
	[EnumMember]
	FS700,
	[EnumMember]
	MACH,
	[EnumMember]
	MACH2,
	[EnumMember]
	FS800I,
	[EnumMember]
	MFDFIT1E,
	[EnumMember]
	KFISCAL,
	[EnumMember]
	X5,
	[EnumMember]
	ST100,
	[EnumMember]
	ST120,
	[EnumMember]
	ST200
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "UfCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class UfCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[OptionalField]
	private string SiglaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	[DataMember]
	public string Sigla
	{
		get
		{
			return SiglaField;
		}
		set
		{
			if ((object)SiglaField != value)
			{
				SiglaField = value;
				RaisePropertyChanged("Sigla");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "DocumentoCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class DocumentoCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NumeroField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Numero
	{
		get
		{
			return NumeroField;
		}
		set
		{
			if ((object)NumeroField != value)
			{
				NumeroField = value;
				RaisePropertyChanged("Numero");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EmailCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class EmailCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string EnderecoField;

	[OptionalField]
	private int IdField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Endereco
	{
		get
		{
			return EnderecoField;
		}
		set
		{
			if ((object)EnderecoField != value)
			{
				EnderecoField = value;
				RaisePropertyChanged("Endereco");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EnderecoCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class EnderecoCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string BairroField;

	[OptionalField]
	private string CepField;

	[OptionalField]
	private string ComplementoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string LogradouroField;

	[OptionalField]
	private MunicipioCompletoDto MunicipioField;

	[OptionalField]
	private string NumeroField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Bairro
	{
		get
		{
			return BairroField;
		}
		set
		{
			if ((object)BairroField != value)
			{
				BairroField = value;
				RaisePropertyChanged("Bairro");
			}
		}
	}

	[DataMember]
	public string Cep
	{
		get
		{
			return CepField;
		}
		set
		{
			if ((object)CepField != value)
			{
				CepField = value;
				RaisePropertyChanged("Cep");
			}
		}
	}

	[DataMember]
	public string Complemento
	{
		get
		{
			return ComplementoField;
		}
		set
		{
			if ((object)ComplementoField != value)
			{
				ComplementoField = value;
				RaisePropertyChanged("Complemento");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Logradouro
	{
		get
		{
			return LogradouroField;
		}
		set
		{
			if ((object)LogradouroField != value)
			{
				LogradouroField = value;
				RaisePropertyChanged("Logradouro");
			}
		}
	}

	[DataMember]
	public MunicipioCompletoDto Municipio
	{
		get
		{
			return MunicipioField;
		}
		set
		{
			if (MunicipioField != value)
			{
				MunicipioField = value;
				RaisePropertyChanged("Municipio");
			}
		}
	}

	[DataMember]
	public string Numero
	{
		get
		{
			return NumeroField;
		}
		set
		{
			if ((object)NumeroField != value)
			{
				NumeroField = value;
				RaisePropertyChanged("Numero");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "TelefoneCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class TelefoneCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NumeroField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Numero
	{
		get
		{
			return NumeroField;
		}
		set
		{
			if ((object)NumeroField != value)
			{
				NumeroField = value;
				RaisePropertyChanged("Numero");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "MunicipioCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class MunicipioCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int CodigoIbgeField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[OptionalField]
	private UfCompletoDto UfField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int CodigoIbge
	{
		get
		{
			return CodigoIbgeField;
		}
		set
		{
			if (!CodigoIbgeField.Equals(value))
			{
				CodigoIbgeField = value;
				RaisePropertyChanged("CodigoIbge");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	[DataMember]
	public UfCompletoDto Uf
	{
		get
		{
			return UfField;
		}
		set
		{
			if (UfField != value)
			{
				UfField = value;
				RaisePropertyChanged("Uf");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "MarcaSat", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum MarcaSat
{
	[EnumMember]
	Indefinida,
	[EnumMember]
	Emulador,
	[EnumMember]
	Dimep,
	[EnumMember]
	Urano,
	[EnumMember]
	Bematech,
	[EnumMember]
	Tanca,
	[EnumMember]
	Sweda,
	[EnumMember]
	Elgin,
	[EnumMember]
	Gertec,
	[EnumMember]
	Nitere,
	[EnumMember]
	Kryptus,
	[EnumMember]
	Daruma,
	[EnumMember]
	Epson,
	[EnumMember]
	ControlId,
	[EnumMember]
	Care
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ModeloSat", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum ModeloSat
{
	[EnumMember]
	Default
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "DashboardDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class DashboardDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private ProporcaoDesistenciaDto[] ProporcaoDesistenciaPorRamoField;

	[OptionalField]
	private SistemasVendidosPorDiaMesDto[] SistemasVendidosPorDiaUltimos60DiasField;

	[OptionalField]
	private SistemasPorRamoSimplificadoDto[] TotalSistemasDesusoField;

	[OptionalField]
	private SistemasVendidosPorEstadoDto[] TotalSistemasPorEstadoField;

	[OptionalField]
	private SistemasVendidosPorRamoAtividadeEUfDto[] TotalSistemasRamoAtividadeEUfField;

	[OptionalField]
	private SistemasPorRamoSimplificadoDto[] TotalSistemasValidosField;

	[OptionalField]
	private SistemasPorRamoSimplificadoDto[] TotalSistemasVendidosField;

	[OptionalField]
	private SistemasVendidosPorMesAnoDto[] TotalVendidoPorMesAnoField;

	[OptionalField]
	private SistemasVendidosPorMesESistemaDto[] TotalVendidoPorMesESistemaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public ProporcaoDesistenciaDto[] ProporcaoDesistenciaPorRamo
	{
		get
		{
			return ProporcaoDesistenciaPorRamoField;
		}
		set
		{
			if (ProporcaoDesistenciaPorRamoField != value)
			{
				ProporcaoDesistenciaPorRamoField = value;
				RaisePropertyChanged("ProporcaoDesistenciaPorRamo");
			}
		}
	}

	[DataMember]
	public SistemasVendidosPorDiaMesDto[] SistemasVendidosPorDiaUltimos60Dias
	{
		get
		{
			return SistemasVendidosPorDiaUltimos60DiasField;
		}
		set
		{
			if (SistemasVendidosPorDiaUltimos60DiasField != value)
			{
				SistemasVendidosPorDiaUltimos60DiasField = value;
				RaisePropertyChanged("SistemasVendidosPorDiaUltimos60Dias");
			}
		}
	}

	[DataMember]
	public SistemasPorRamoSimplificadoDto[] TotalSistemasDesuso
	{
		get
		{
			return TotalSistemasDesusoField;
		}
		set
		{
			if (TotalSistemasDesusoField != value)
			{
				TotalSistemasDesusoField = value;
				RaisePropertyChanged("TotalSistemasDesuso");
			}
		}
	}

	[DataMember]
	public SistemasVendidosPorEstadoDto[] TotalSistemasPorEstado
	{
		get
		{
			return TotalSistemasPorEstadoField;
		}
		set
		{
			if (TotalSistemasPorEstadoField != value)
			{
				TotalSistemasPorEstadoField = value;
				RaisePropertyChanged("TotalSistemasPorEstado");
			}
		}
	}

	[DataMember]
	public SistemasVendidosPorRamoAtividadeEUfDto[] TotalSistemasRamoAtividadeEUf
	{
		get
		{
			return TotalSistemasRamoAtividadeEUfField;
		}
		set
		{
			if (TotalSistemasRamoAtividadeEUfField != value)
			{
				TotalSistemasRamoAtividadeEUfField = value;
				RaisePropertyChanged("TotalSistemasRamoAtividadeEUf");
			}
		}
	}

	[DataMember]
	public SistemasPorRamoSimplificadoDto[] TotalSistemasValidos
	{
		get
		{
			return TotalSistemasValidosField;
		}
		set
		{
			if (TotalSistemasValidosField != value)
			{
				TotalSistemasValidosField = value;
				RaisePropertyChanged("TotalSistemasValidos");
			}
		}
	}

	[DataMember]
	public SistemasPorRamoSimplificadoDto[] TotalSistemasVendidos
	{
		get
		{
			return TotalSistemasVendidosField;
		}
		set
		{
			if (TotalSistemasVendidosField != value)
			{
				TotalSistemasVendidosField = value;
				RaisePropertyChanged("TotalSistemasVendidos");
			}
		}
	}

	[DataMember]
	public SistemasVendidosPorMesAnoDto[] TotalVendidoPorMesAno
	{
		get
		{
			return TotalVendidoPorMesAnoField;
		}
		set
		{
			if (TotalVendidoPorMesAnoField != value)
			{
				TotalVendidoPorMesAnoField = value;
				RaisePropertyChanged("TotalVendidoPorMesAno");
			}
		}
	}

	[DataMember]
	public SistemasVendidosPorMesESistemaDto[] TotalVendidoPorMesESistema
	{
		get
		{
			return TotalVendidoPorMesESistemaField;
		}
		set
		{
			if (TotalVendidoPorMesESistemaField != value)
			{
				TotalVendidoPorMesESistemaField = value;
				RaisePropertyChanged("TotalVendidoPorMesESistema");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ProporcaoDesistenciaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ProporcaoDesistenciaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private decimal PercentualField;

	[OptionalField]
	private string RamoAtividadeField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public decimal Percentual
	{
		get
		{
			return PercentualField;
		}
		set
		{
			if (!PercentualField.Equals(value))
			{
				PercentualField = value;
				RaisePropertyChanged("Percentual");
			}
		}
	}

	[DataMember]
	public string RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if ((object)RamoAtividadeField != value)
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasVendidosPorDiaMesDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasVendidosPorDiaMesDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string DiaField;

	[OptionalField]
	private string MesField;

	[OptionalField]
	private int TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Dia
	{
		get
		{
			return DiaField;
		}
		set
		{
			if ((object)DiaField != value)
			{
				DiaField = value;
				RaisePropertyChanged("Dia");
			}
		}
	}

	[DataMember]
	public string Mes
	{
		get
		{
			return MesField;
		}
		set
		{
			if ((object)MesField != value)
			{
				MesField = value;
				RaisePropertyChanged("Mes");
			}
		}
	}

	[DataMember]
	public int TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasPorRamoSimplificadoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasPorRamoSimplificadoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private decimal PercentualField;

	[OptionalField]
	private string RamoAtividadeField;

	[OptionalField]
	private decimal TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public decimal Percentual
	{
		get
		{
			return PercentualField;
		}
		set
		{
			if (!PercentualField.Equals(value))
			{
				PercentualField = value;
				RaisePropertyChanged("Percentual");
			}
		}
	}

	[DataMember]
	public string RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if ((object)RamoAtividadeField != value)
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	[DataMember]
	public decimal TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasVendidosPorEstadoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasVendidosPorEstadoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string EstadoField;

	[OptionalField]
	private int TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Estado
	{
		get
		{
			return EstadoField;
		}
		set
		{
			if ((object)EstadoField != value)
			{
				EstadoField = value;
				RaisePropertyChanged("Estado");
			}
		}
	}

	[DataMember]
	public int TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasVendidosPorRamoAtividadeEUfDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasVendidosPorRamoAtividadeEUfDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string EstadoField;

	[OptionalField]
	private string RamoAtividadeField;

	[OptionalField]
	private int TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Estado
	{
		get
		{
			return EstadoField;
		}
		set
		{
			if ((object)EstadoField != value)
			{
				EstadoField = value;
				RaisePropertyChanged("Estado");
			}
		}
	}

	[DataMember]
	public string RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if ((object)RamoAtividadeField != value)
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	[DataMember]
	public int TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasVendidosPorMesAnoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasVendidosPorMesAnoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string AnoField;

	[OptionalField]
	private string MesField;

	[OptionalField]
	private int TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Ano
	{
		get
		{
			return AnoField;
		}
		set
		{
			if ((object)AnoField != value)
			{
				AnoField = value;
				RaisePropertyChanged("Ano");
			}
		}
	}

	[DataMember]
	public string Mes
	{
		get
		{
			return MesField;
		}
		set
		{
			if ((object)MesField != value)
			{
				MesField = value;
				RaisePropertyChanged("Mes");
			}
		}
	}

	[DataMember]
	public int TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasVendidosPorMesESistemaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasVendidosPorMesESistemaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string MesField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private int TotalVendidoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Mes
	{
		get
		{
			return MesField;
		}
		set
		{
			if ((object)MesField != value)
			{
				MesField = value;
				RaisePropertyChanged("Mes");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public int TotalVendido
	{
		get
		{
			return TotalVendidoField;
		}
		set
		{
			if (!TotalVendidoField.Equals(value))
			{
				TotalVendidoField = value;
				RaisePropertyChanged("TotalVendido");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SistemasPorRamoDetalhadoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SistemasPorRamoDetalhadoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CnaeField;

	[OptionalField]
	private decimal QuantidadeField;

	[OptionalField]
	private string RamoAtividadeField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Cnae
	{
		get
		{
			return CnaeField;
		}
		set
		{
			if ((object)CnaeField != value)
			{
				CnaeField = value;
				RaisePropertyChanged("Cnae");
			}
		}
	}

	[DataMember]
	public decimal Quantidade
	{
		get
		{
			return QuantidadeField;
		}
		set
		{
			if (!QuantidadeField.Equals(value))
			{
				QuantidadeField = value;
				RaisePropertyChanged("Quantidade");
			}
		}
	}

	[DataMember]
	public string RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if ((object)RamoAtividadeField != value)
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoResumoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitacaoResumoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string AtualizacaoField;

	[OptionalField]
	private bool CanceladoField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private DateTime DataHoraGeracaoField;

	[OptionalField]
	private DateTime? DataHoraHabilitacaoField;

	[OptionalField]
	private bool HabilitadoMeiField;

	[OptionalField]
	private string MunicipioUfClienteFinalField;

	[OptionalField]
	private int NumeroCreditosField;

	[OptionalField]
	private string RazaoSocialClienteFinalField;

	[OptionalField]
	private string RazaoSocialRevendaField;

	[OptionalField]
	private string SerialField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private string SituacaoDoSerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Atualizacao
	{
		get
		{
			return AtualizacaoField;
		}
		set
		{
			if ((object)AtualizacaoField != value)
			{
				AtualizacaoField = value;
				RaisePropertyChanged("Atualizacao");
			}
		}
	}

	[DataMember]
	public bool Cancelado
	{
		get
		{
			return CanceladoField;
		}
		set
		{
			if (!CanceladoField.Equals(value))
			{
				CanceladoField = value;
				RaisePropertyChanged("Cancelado");
			}
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraGeracao
	{
		get
		{
			return DataHoraGeracaoField;
		}
		set
		{
			if (!DataHoraGeracaoField.Equals(value))
			{
				DataHoraGeracaoField = value;
				RaisePropertyChanged("DataHoraGeracao");
			}
		}
	}

	[DataMember]
	public DateTime? DataHoraHabilitacao
	{
		get
		{
			return DataHoraHabilitacaoField;
		}
		set
		{
			if (!DataHoraHabilitacaoField.Equals(value))
			{
				DataHoraHabilitacaoField = value;
				RaisePropertyChanged("DataHoraHabilitacao");
			}
		}
	}

	[DataMember]
	public bool HabilitadoMei
	{
		get
		{
			return HabilitadoMeiField;
		}
		set
		{
			if (!HabilitadoMeiField.Equals(value))
			{
				HabilitadoMeiField = value;
				RaisePropertyChanged("HabilitadoMei");
			}
		}
	}

	[DataMember]
	public string MunicipioUfClienteFinal
	{
		get
		{
			return MunicipioUfClienteFinalField;
		}
		set
		{
			if ((object)MunicipioUfClienteFinalField != value)
			{
				MunicipioUfClienteFinalField = value;
				RaisePropertyChanged("MunicipioUfClienteFinal");
			}
		}
	}

	[DataMember]
	public int NumeroCreditos
	{
		get
		{
			return NumeroCreditosField;
		}
		set
		{
			if (!NumeroCreditosField.Equals(value))
			{
				NumeroCreditosField = value;
				RaisePropertyChanged("NumeroCreditos");
			}
		}
	}

	[DataMember]
	public string RazaoSocialClienteFinal
	{
		get
		{
			return RazaoSocialClienteFinalField;
		}
		set
		{
			if ((object)RazaoSocialClienteFinalField != value)
			{
				RazaoSocialClienteFinalField = value;
				RaisePropertyChanged("RazaoSocialClienteFinal");
			}
		}
	}

	[DataMember]
	public string RazaoSocialRevenda
	{
		get
		{
			return RazaoSocialRevendaField;
		}
		set
		{
			if ((object)RazaoSocialRevendaField != value)
			{
				RazaoSocialRevendaField = value;
				RaisePropertyChanged("RazaoSocialRevenda");
			}
		}
	}

	[DataMember]
	public string Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if ((object)SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public string SituacaoDoSerial
	{
		get
		{
			return SituacaoDoSerialField;
		}
		set
		{
			if ((object)SituacaoDoSerialField != value)
			{
				SituacaoDoSerialField = value;
				RaisePropertyChanged("SituacaoDoSerial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitacaoCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private ClienteRevendaCompletoDto ClienteRevendaField;

	[OptionalField]
	private DateTime DataHoraHabilitacaoField;

	[OptionalField]
	private DateTime DataLimiteField;

	[OptionalField]
	private DateTime DataValidacaoField;

	[OptionalField]
	private FilialDto[] FiliaisField;

	[OptionalField]
	private bool HabilitadoMeiField;

	[OptionalField]
	private HistoricoLicencasAdicionaisCompletoDto[] HistoricoLicencasAdicionaisField;

	[OptionalField]
	private HistoricoValidacaoPafCompletoDto[] HistoricoValidacoesPafField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private int NumeroCreditosField;

	[OptionalField]
	private string SerialG5Field;

	[OptionalField]
	private SerialRevendaCompletoDto SerialRevendaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public ClienteRevendaCompletoDto ClienteRevenda
	{
		get
		{
			return ClienteRevendaField;
		}
		set
		{
			if (ClienteRevendaField != value)
			{
				ClienteRevendaField = value;
				RaisePropertyChanged("ClienteRevenda");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraHabilitacao
	{
		get
		{
			return DataHoraHabilitacaoField;
		}
		set
		{
			if (!DataHoraHabilitacaoField.Equals(value))
			{
				DataHoraHabilitacaoField = value;
				RaisePropertyChanged("DataHoraHabilitacao");
			}
		}
	}

	[DataMember]
	public DateTime DataLimite
	{
		get
		{
			return DataLimiteField;
		}
		set
		{
			if (!DataLimiteField.Equals(value))
			{
				DataLimiteField = value;
				RaisePropertyChanged("DataLimite");
			}
		}
	}

	[DataMember]
	public DateTime DataValidacao
	{
		get
		{
			return DataValidacaoField;
		}
		set
		{
			if (!DataValidacaoField.Equals(value))
			{
				DataValidacaoField = value;
				RaisePropertyChanged("DataValidacao");
			}
		}
	}

	[DataMember]
	public FilialDto[] Filiais
	{
		get
		{
			return FiliaisField;
		}
		set
		{
			if (FiliaisField != value)
			{
				FiliaisField = value;
				RaisePropertyChanged("Filiais");
			}
		}
	}

	[DataMember]
	public bool HabilitadoMei
	{
		get
		{
			return HabilitadoMeiField;
		}
		set
		{
			if (!HabilitadoMeiField.Equals(value))
			{
				HabilitadoMeiField = value;
				RaisePropertyChanged("HabilitadoMei");
			}
		}
	}

	[DataMember]
	public HistoricoLicencasAdicionaisCompletoDto[] HistoricoLicencasAdicionais
	{
		get
		{
			return HistoricoLicencasAdicionaisField;
		}
		set
		{
			if (HistoricoLicencasAdicionaisField != value)
			{
				HistoricoLicencasAdicionaisField = value;
				RaisePropertyChanged("HistoricoLicencasAdicionais");
			}
		}
	}

	[DataMember]
	public HistoricoValidacaoPafCompletoDto[] HistoricoValidacoesPaf
	{
		get
		{
			return HistoricoValidacoesPafField;
		}
		set
		{
			if (HistoricoValidacoesPafField != value)
			{
				HistoricoValidacoesPafField = value;
				RaisePropertyChanged("HistoricoValidacoesPaf");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public int NumeroCreditos
	{
		get
		{
			return NumeroCreditosField;
		}
		set
		{
			if (!NumeroCreditosField.Equals(value))
			{
				NumeroCreditosField = value;
				RaisePropertyChanged("NumeroCreditos");
			}
		}
	}

	[DataMember]
	public string SerialG5
	{
		get
		{
			return SerialG5Field;
		}
		set
		{
			if ((object)SerialG5Field != value)
			{
				SerialG5Field = value;
				RaisePropertyChanged("SerialG5");
			}
		}
	}

	[DataMember]
	public SerialRevendaCompletoDto SerialRevenda
	{
		get
		{
			return SerialRevendaField;
		}
		set
		{
			if (SerialRevendaField != value)
			{
				SerialRevendaField = value;
				RaisePropertyChanged("SerialRevenda");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SerialRevendaCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SerialRevendaCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private SerialCompletoDto SerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public SerialCompletoDto Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if (SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "FilialDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class FilialDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private HabilitacaoCompletaDto HabilitacaoField;

	[OptionalField]
	private int IdField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public HabilitacaoCompletaDto Habilitacao
	{
		get
		{
			return HabilitacaoField;
		}
		set
		{
			if (HabilitacaoField != value)
			{
				HabilitacaoField = value;
				RaisePropertyChanged("Habilitacao");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HistoricoLicencasAdicionaisCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HistoricoLicencasAdicionaisCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NumeroNotaFiscalField;

	[OptionalField]
	private int QuantidadeField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string NumeroNotaFiscal
	{
		get
		{
			return NumeroNotaFiscalField;
		}
		set
		{
			if ((object)NumeroNotaFiscalField != value)
			{
				NumeroNotaFiscalField = value;
				RaisePropertyChanged("NumeroNotaFiscal");
			}
		}
	}

	[DataMember]
	public int Quantidade
	{
		get
		{
			return QuantidadeField;
		}
		set
		{
			if (!QuantidadeField.Equals(value))
			{
				QuantidadeField = value;
				RaisePropertyChanged("Quantidade");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HistoricoValidacaoPafCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HistoricoValidacaoPafCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataValidacaoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string VersaoErPafField;

	[OptionalField]
	private string VersaoSistemaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataValidacao
	{
		get
		{
			return DataValidacaoField;
		}
		set
		{
			if (!DataValidacaoField.Equals(value))
			{
				DataValidacaoField = value;
				RaisePropertyChanged("DataValidacao");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string VersaoErPaf
	{
		get
		{
			return VersaoErPafField;
		}
		set
		{
			if ((object)VersaoErPafField != value)
			{
				VersaoErPafField = value;
				RaisePropertyChanged("VersaoErPaf");
			}
		}
	}

	[DataMember]
	public string VersaoSistema
	{
		get
		{
			return VersaoSistemaField;
		}
		set
		{
			if ((object)VersaoSistemaField != value)
			{
				VersaoSistemaField = value;
				RaisePropertyChanged("VersaoSistema");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SerialCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SerialCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private bool AtualizacaoField;

	[OptionalField]
	private bool CanceladoField;

	[OptionalField]
	private CancelamentoCompletoDto CancelamentoField;

	[OptionalField]
	private DateTime DataHoraEntraBlackListField;

	[OptionalField]
	private DateTime DataHoraGeracaoField;

	[OptionalField]
	private HistoricoSistemaDto[] HistoricosSistemaField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NumeroSerialField;

	[OptionalField]
	private SerialSistema SistemaField;

	[OptionalField]
	private SerialSistema SistemaOrigemField;

	[OptionalField]
	private SituacaoSerial SituacaoSerialField;

	[OptionalField]
	private string VersaoField;

	[OptionalField]
	private GeracaoSistemaDto[] VersaoHistoricoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public bool Atualizacao
	{
		get
		{
			return AtualizacaoField;
		}
		set
		{
			if (!AtualizacaoField.Equals(value))
			{
				AtualizacaoField = value;
				RaisePropertyChanged("Atualizacao");
			}
		}
	}

	[DataMember]
	public bool Cancelado
	{
		get
		{
			return CanceladoField;
		}
		set
		{
			if (!CanceladoField.Equals(value))
			{
				CanceladoField = value;
				RaisePropertyChanged("Cancelado");
			}
		}
	}

	[DataMember]
	public CancelamentoCompletoDto Cancelamento
	{
		get
		{
			return CancelamentoField;
		}
		set
		{
			if (CancelamentoField != value)
			{
				CancelamentoField = value;
				RaisePropertyChanged("Cancelamento");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraEntraBlackList
	{
		get
		{
			return DataHoraEntraBlackListField;
		}
		set
		{
			if (!DataHoraEntraBlackListField.Equals(value))
			{
				DataHoraEntraBlackListField = value;
				RaisePropertyChanged("DataHoraEntraBlackList");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraGeracao
	{
		get
		{
			return DataHoraGeracaoField;
		}
		set
		{
			if (!DataHoraGeracaoField.Equals(value))
			{
				DataHoraGeracaoField = value;
				RaisePropertyChanged("DataHoraGeracao");
			}
		}
	}

	[DataMember]
	public HistoricoSistemaDto[] HistoricosSistema
	{
		get
		{
			return HistoricosSistemaField;
		}
		set
		{
			if (HistoricosSistemaField != value)
			{
				HistoricosSistemaField = value;
				RaisePropertyChanged("HistoricosSistema");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string NumeroSerial
	{
		get
		{
			return NumeroSerialField;
		}
		set
		{
			if ((object)NumeroSerialField != value)
			{
				NumeroSerialField = value;
				RaisePropertyChanged("NumeroSerial");
			}
		}
	}

	[DataMember]
	public SerialSistema Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if (!SistemaField.Equals(value))
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public SerialSistema SistemaOrigem
	{
		get
		{
			return SistemaOrigemField;
		}
		set
		{
			if (!SistemaOrigemField.Equals(value))
			{
				SistemaOrigemField = value;
				RaisePropertyChanged("SistemaOrigem");
			}
		}
	}

	[DataMember]
	public SituacaoSerial SituacaoSerial
	{
		get
		{
			return SituacaoSerialField;
		}
		set
		{
			if (!SituacaoSerialField.Equals(value))
			{
				SituacaoSerialField = value;
				RaisePropertyChanged("SituacaoSerial");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	[DataMember]
	public GeracaoSistemaDto[] VersaoHistorico
	{
		get
		{
			return VersaoHistoricoField;
		}
		set
		{
			if (VersaoHistoricoField != value)
			{
				VersaoHistoricoField = value;
				RaisePropertyChanged("VersaoHistorico");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "CancelamentoCompletoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class CancelamentoCompletoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string JustificativaField;

	[OptionalField]
	private MotivoCancelamento MotivoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Justificativa
	{
		get
		{
			return JustificativaField;
		}
		set
		{
			if ((object)JustificativaField != value)
			{
				JustificativaField = value;
				RaisePropertyChanged("Justificativa");
			}
		}
	}

	[DataMember]
	public MotivoCancelamento Motivo
	{
		get
		{
			return MotivoField;
		}
		set
		{
			if (!MotivoField.Equals(value))
			{
				MotivoField = value;
				RaisePropertyChanged("Motivo");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HistoricoSistemaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HistoricoSistemaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataHoraField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string JustificativaField;

	[OptionalField]
	private SerialSistema SistemaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataHora
	{
		get
		{
			return DataHoraField;
		}
		set
		{
			if (!DataHoraField.Equals(value))
			{
				DataHoraField = value;
				RaisePropertyChanged("DataHora");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Justificativa
	{
		get
		{
			return JustificativaField;
		}
		set
		{
			if ((object)JustificativaField != value)
			{
				JustificativaField = value;
				RaisePropertyChanged("Justificativa");
			}
		}
	}

	[DataMember]
	public SerialSistema Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if (!SistemaField.Equals(value))
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SerialSistema", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum SerialSistema
{
	[EnumMember]
	Administrador,
	[EnumMember]
	Gerencial,
	[EnumMember]
	Comercial,
	[EnumMember]
	AdministradorIndustria,
	[EnumMember]
	MEI
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SituacaoSerial", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum SituacaoSerial
{
	[EnumMember]
	Gerado,
	[EnumMember]
	Exportado,
	[EnumMember]
	Gravado,
	[EnumMember]
	Vinculado,
	[EnumMember]
	Habilitado,
	[EnumMember]
	Atualizacao,
	[EnumMember]
	Cancelado
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "GeracaoSistemaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class GeracaoSistemaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private bool AtualizacaoField;

	[OptionalField]
	private DateTime DataHoraField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string VersaoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public bool Atualizacao
	{
		get
		{
			return AtualizacaoField;
		}
		set
		{
			if (!AtualizacaoField.Equals(value))
			{
				AtualizacaoField = value;
				RaisePropertyChanged("Atualizacao");
			}
		}
	}

	[DataMember]
	public DateTime DataHora
	{
		get
		{
			return DataHoraField;
		}
		set
		{
			if (!DataHoraField.Equals(value))
			{
				DataHoraField = value;
				RaisePropertyChanged("DataHora");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "MotivoCancelamento", Namespace = "http://schemas.datacontract.org/2004/07/FrameworkUtilDigisat.Types")]
public enum MotivoCancelamento
{
	[EnumMember]
	Devolucao,
	[EnumMember]
	Cancelamento,
	[EnumMember]
	Irregularidade
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SerialComCodigoDeSegurancaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SerialComCodigoDeSegurancaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CodigoDeSegurancaField;

	[OptionalField]
	private string NumeroSerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string CodigoDeSeguranca
	{
		get
		{
			return CodigoDeSegurancaField;
		}
		set
		{
			if ((object)CodigoDeSegurancaField != value)
			{
				CodigoDeSegurancaField = value;
				RaisePropertyChanged("CodigoDeSeguranca");
			}
		}
	}

	[DataMember]
	public string NumeroSerial
	{
		get
		{
			return NumeroSerialField;
		}
		set
		{
			if ((object)NumeroSerialField != value)
			{
				NumeroSerialField = value;
				RaisePropertyChanged("NumeroSerial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ResumoHabilitadoPeriodoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ResumoHabilitadoPeriodoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private HabilitadoPeriodoDto[] HabilitadosField;

	[OptionalField]
	private int TotalField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public HabilitadoPeriodoDto[] Habilitados
	{
		get
		{
			return HabilitadosField;
		}
		set
		{
			if (HabilitadosField != value)
			{
				HabilitadosField = value;
				RaisePropertyChanged("Habilitados");
			}
		}
	}

	[DataMember]
	public int Total
	{
		get
		{
			return TotalField;
		}
		set
		{
			if (!TotalField.Equals(value))
			{
				TotalField = value;
				RaisePropertyChanged("Total");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitadoPeriodoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitadoPeriodoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int QuantidadeField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private string VersaoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Quantidade
	{
		get
		{
			return QuantidadeField;
		}
		set
		{
			if (!QuantidadeField.Equals(value))
			{
				QuantidadeField = value;
				RaisePropertyChanged("Quantidade");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ResumoSerialDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ResumoSerialDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CnpjClienteFinalField;

	[OptionalField]
	private string CnpjRevendaField;

	[OptionalField]
	private DateTime DataHoraGeracaoField;

	[OptionalField]
	private DateTime DataHoraHabilitacaoField;

	[OptionalField]
	private string JustificativaCancelamentoField;

	[OptionalField]
	private string MunicipioUfClienteFinalField;

	[OptionalField]
	private int NumeroCreditosField;

	[OptionalField]
	private string RazaoSocialClienteFinalField;

	[OptionalField]
	private string RazaoSocialRevendaField;

	[OptionalField]
	private string SerialField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private string SituacaoDoSerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string CnpjClienteFinal
	{
		get
		{
			return CnpjClienteFinalField;
		}
		set
		{
			if ((object)CnpjClienteFinalField != value)
			{
				CnpjClienteFinalField = value;
				RaisePropertyChanged("CnpjClienteFinal");
			}
		}
	}

	[DataMember]
	public string CnpjRevenda
	{
		get
		{
			return CnpjRevendaField;
		}
		set
		{
			if ((object)CnpjRevendaField != value)
			{
				CnpjRevendaField = value;
				RaisePropertyChanged("CnpjRevenda");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraGeracao
	{
		get
		{
			return DataHoraGeracaoField;
		}
		set
		{
			if (!DataHoraGeracaoField.Equals(value))
			{
				DataHoraGeracaoField = value;
				RaisePropertyChanged("DataHoraGeracao");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraHabilitacao
	{
		get
		{
			return DataHoraHabilitacaoField;
		}
		set
		{
			if (!DataHoraHabilitacaoField.Equals(value))
			{
				DataHoraHabilitacaoField = value;
				RaisePropertyChanged("DataHoraHabilitacao");
			}
		}
	}

	[DataMember]
	public string JustificativaCancelamento
	{
		get
		{
			return JustificativaCancelamentoField;
		}
		set
		{
			if ((object)JustificativaCancelamentoField != value)
			{
				JustificativaCancelamentoField = value;
				RaisePropertyChanged("JustificativaCancelamento");
			}
		}
	}

	[DataMember]
	public string MunicipioUfClienteFinal
	{
		get
		{
			return MunicipioUfClienteFinalField;
		}
		set
		{
			if ((object)MunicipioUfClienteFinalField != value)
			{
				MunicipioUfClienteFinalField = value;
				RaisePropertyChanged("MunicipioUfClienteFinal");
			}
		}
	}

	[DataMember]
	public int NumeroCreditos
	{
		get
		{
			return NumeroCreditosField;
		}
		set
		{
			if (!NumeroCreditosField.Equals(value))
			{
				NumeroCreditosField = value;
				RaisePropertyChanged("NumeroCreditos");
			}
		}
	}

	[DataMember]
	public string RazaoSocialClienteFinal
	{
		get
		{
			return RazaoSocialClienteFinalField;
		}
		set
		{
			if ((object)RazaoSocialClienteFinalField != value)
			{
				RazaoSocialClienteFinalField = value;
				RaisePropertyChanged("RazaoSocialClienteFinal");
			}
		}
	}

	[DataMember]
	public string RazaoSocialRevenda
	{
		get
		{
			return RazaoSocialRevendaField;
		}
		set
		{
			if ((object)RazaoSocialRevendaField != value)
			{
				RazaoSocialRevendaField = value;
				RaisePropertyChanged("RazaoSocialRevenda");
			}
		}
	}

	[DataMember]
	public string Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if ((object)SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public string SituacaoDoSerial
	{
		get
		{
			return SituacaoDoSerialField;
		}
		set
		{
			if ((object)SituacaoDoSerialField != value)
			{
				SituacaoDoSerialField = value;
				RaisePropertyChanged("SituacaoDoSerial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoListaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitacaoListaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private DateTime DataHoraHabilitacaoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[OptionalField]
	private string SerialField;

	[OptionalField]
	private string VersaoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public DateTime DataHoraHabilitacao
	{
		get
		{
			return DataHoraHabilitacaoField;
		}
		set
		{
			if (!DataHoraHabilitacaoField.Equals(value))
			{
				DataHoraHabilitacaoField = value;
				RaisePropertyChanged("DataHoraHabilitacao");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	[DataMember]
	public string Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if ((object)SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class HabilitacaoXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataValidacaoField;

	[OptionalField]
	private FilialXmlDto[] FiliaisField;

	[OptionalField]
	private bool HabilitadoMeiField;

	[OptionalField]
	private DadosEmpresaXmlDto MatrizField;

	[OptionalField]
	private ModuloHabilitadoXmlDto[] ModuloField;

	[OptionalField]
	private string NumeroCreditosField;

	[OptionalField]
	private string SerialField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private string VersaoField;

	[OptionalField]
	private string VersaoPafField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataValidacao
	{
		get
		{
			return DataValidacaoField;
		}
		set
		{
			if (!DataValidacaoField.Equals(value))
			{
				DataValidacaoField = value;
				RaisePropertyChanged("DataValidacao");
			}
		}
	}

	[DataMember]
	public FilialXmlDto[] Filiais
	{
		get
		{
			return FiliaisField;
		}
		set
		{
			if (FiliaisField != value)
			{
				FiliaisField = value;
				RaisePropertyChanged("Filiais");
			}
		}
	}

	[DataMember]
	public bool HabilitadoMei
	{
		get
		{
			return HabilitadoMeiField;
		}
		set
		{
			if (!HabilitadoMeiField.Equals(value))
			{
				HabilitadoMeiField = value;
				RaisePropertyChanged("HabilitadoMei");
			}
		}
	}

	[DataMember]
	public DadosEmpresaXmlDto Matriz
	{
		get
		{
			return MatrizField;
		}
		set
		{
			if (MatrizField != value)
			{
				MatrizField = value;
				RaisePropertyChanged("Matriz");
			}
		}
	}

	[DataMember]
	public ModuloHabilitadoXmlDto[] Modulo
	{
		get
		{
			return ModuloField;
		}
		set
		{
			if (ModuloField != value)
			{
				ModuloField = value;
				RaisePropertyChanged("Modulo");
			}
		}
	}

	[DataMember]
	public string NumeroCreditos
	{
		get
		{
			return NumeroCreditosField;
		}
		set
		{
			if ((object)NumeroCreditosField != value)
			{
				NumeroCreditosField = value;
				RaisePropertyChanged("NumeroCreditos");
			}
		}
	}

	[DataMember]
	public string Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if ((object)SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	[DataMember]
	public string VersaoPaf
	{
		get
		{
			return VersaoPafField;
		}
		set
		{
			if ((object)VersaoPafField != value)
			{
				VersaoPafField = value;
				RaisePropertyChanged("VersaoPaf");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "DadosEmpresaXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class DadosEmpresaXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string AssinaturaDoAplicativoComercialField;

	[OptionalField]
	private string CnaeField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private EcfXmlDto[] EcfField;

	[OptionalField]
	private string EmailField;

	[OptionalField]
	private EnderecoDto EnderecoField;

	[OptionalField]
	private string InscricaoEstadualField;

	[OptionalField]
	private PerfilXmlDto PerfilField;

	[OptionalField]
	private RamoDeAtividade RamoAtividadeField;

	[OptionalField]
	private string RazaoSocialField;

	[OptionalField]
	private string RegistroMunicipalField;

	[OptionalField]
	private SatXmlDto[] SatField;

	[OptionalField]
	private string TelefoneField;

	[OptionalField]
	private TipoPontoDeVenda TipoAtividadePontoDeVendaField;

	[OptionalField]
	private TipoMesaContaCliente? TipoAtividadeRestauranteField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string AssinaturaDoAplicativoComercial
	{
		get
		{
			return AssinaturaDoAplicativoComercialField;
		}
		set
		{
			if ((object)AssinaturaDoAplicativoComercialField != value)
			{
				AssinaturaDoAplicativoComercialField = value;
				RaisePropertyChanged("AssinaturaDoAplicativoComercial");
			}
		}
	}

	[DataMember]
	public string Cnae
	{
		get
		{
			return CnaeField;
		}
		set
		{
			if ((object)CnaeField != value)
			{
				CnaeField = value;
				RaisePropertyChanged("Cnae");
			}
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public EcfXmlDto[] Ecf
	{
		get
		{
			return EcfField;
		}
		set
		{
			if (EcfField != value)
			{
				EcfField = value;
				RaisePropertyChanged("Ecf");
			}
		}
	}

	[DataMember]
	public string Email
	{
		get
		{
			return EmailField;
		}
		set
		{
			if ((object)EmailField != value)
			{
				EmailField = value;
				RaisePropertyChanged("Email");
			}
		}
	}

	[DataMember]
	public EnderecoDto Endereco
	{
		get
		{
			return EnderecoField;
		}
		set
		{
			if (EnderecoField != value)
			{
				EnderecoField = value;
				RaisePropertyChanged("Endereco");
			}
		}
	}

	[DataMember]
	public string InscricaoEstadual
	{
		get
		{
			return InscricaoEstadualField;
		}
		set
		{
			if ((object)InscricaoEstadualField != value)
			{
				InscricaoEstadualField = value;
				RaisePropertyChanged("InscricaoEstadual");
			}
		}
	}

	[DataMember]
	public PerfilXmlDto Perfil
	{
		get
		{
			return PerfilField;
		}
		set
		{
			if (PerfilField != value)
			{
				PerfilField = value;
				RaisePropertyChanged("Perfil");
			}
		}
	}

	[DataMember]
	public RamoDeAtividade RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if (!RamoAtividadeField.Equals(value))
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	[DataMember]
	public string RazaoSocial
	{
		get
		{
			return RazaoSocialField;
		}
		set
		{
			if ((object)RazaoSocialField != value)
			{
				RazaoSocialField = value;
				RaisePropertyChanged("RazaoSocial");
			}
		}
	}

	[DataMember]
	public string RegistroMunicipal
	{
		get
		{
			return RegistroMunicipalField;
		}
		set
		{
			if ((object)RegistroMunicipalField != value)
			{
				RegistroMunicipalField = value;
				RaisePropertyChanged("RegistroMunicipal");
			}
		}
	}

	[DataMember]
	public SatXmlDto[] Sat
	{
		get
		{
			return SatField;
		}
		set
		{
			if (SatField != value)
			{
				SatField = value;
				RaisePropertyChanged("Sat");
			}
		}
	}

	[DataMember]
	public string Telefone
	{
		get
		{
			return TelefoneField;
		}
		set
		{
			if ((object)TelefoneField != value)
			{
				TelefoneField = value;
				RaisePropertyChanged("Telefone");
			}
		}
	}

	[DataMember]
	public TipoPontoDeVenda TipoAtividadePontoDeVenda
	{
		get
		{
			return TipoAtividadePontoDeVendaField;
		}
		set
		{
			if (!TipoAtividadePontoDeVendaField.Equals(value))
			{
				TipoAtividadePontoDeVendaField = value;
				RaisePropertyChanged("TipoAtividadePontoDeVenda");
			}
		}
	}

	[DataMember]
	public TipoMesaContaCliente? TipoAtividadeRestaurante
	{
		get
		{
			return TipoAtividadeRestauranteField;
		}
		set
		{
			if (!TipoAtividadeRestauranteField.Equals(value))
			{
				TipoAtividadeRestauranteField = value;
				RaisePropertyChanged("TipoAtividadeRestaurante");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "FilialXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class FilialXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataValidacaoField;

	[OptionalField]
	private DadosEmpresaXmlDto MatrizField;

	[OptionalField]
	private string NumeroCreditosField;

	[OptionalField]
	private string SerialField;

	[OptionalField]
	private string SistemaField;

	[OptionalField]
	private string VersaoField;

	[OptionalField]
	private string VersaoPafField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataValidacao
	{
		get
		{
			return DataValidacaoField;
		}
		set
		{
			if (!DataValidacaoField.Equals(value))
			{
				DataValidacaoField = value;
				RaisePropertyChanged("DataValidacao");
			}
		}
	}

	[DataMember]
	public DadosEmpresaXmlDto Matriz
	{
		get
		{
			return MatrizField;
		}
		set
		{
			if (MatrizField != value)
			{
				MatrizField = value;
				RaisePropertyChanged("Matriz");
			}
		}
	}

	[DataMember]
	public string NumeroCreditos
	{
		get
		{
			return NumeroCreditosField;
		}
		set
		{
			if ((object)NumeroCreditosField != value)
			{
				NumeroCreditosField = value;
				RaisePropertyChanged("NumeroCreditos");
			}
		}
	}

	[DataMember]
	public string Serial
	{
		get
		{
			return SerialField;
		}
		set
		{
			if ((object)SerialField != value)
			{
				SerialField = value;
				RaisePropertyChanged("Serial");
			}
		}
	}

	[DataMember]
	public string Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if ((object)SistemaField != value)
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	[DataMember]
	public string Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if ((object)VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	[DataMember]
	public string VersaoPaf
	{
		get
		{
			return VersaoPafField;
		}
		set
		{
			if ((object)VersaoPafField != value)
			{
				VersaoPafField = value;
				RaisePropertyChanged("VersaoPaf");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ModuloHabilitadoXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class ModuloHabilitadoXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int CodigoField;

	[OptionalField]
	private string NomeField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Codigo
	{
		get
		{
			return CodigoField;
		}
		set
		{
			if (!CodigoField.Equals(value))
			{
				CodigoField = value;
				RaisePropertyChanged("Codigo");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EnderecoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class EnderecoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string BairroField;

	[OptionalField]
	private string CepField;

	[OptionalField]
	private int CodigoIbgeMunicipioField;

	[OptionalField]
	private string ComplementoField;

	[OptionalField]
	private string LogradouroField;

	[OptionalField]
	private string NumeroField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Bairro
	{
		get
		{
			return BairroField;
		}
		set
		{
			if ((object)BairroField != value)
			{
				BairroField = value;
				RaisePropertyChanged("Bairro");
			}
		}
	}

	[DataMember]
	public string Cep
	{
		get
		{
			return CepField;
		}
		set
		{
			if ((object)CepField != value)
			{
				CepField = value;
				RaisePropertyChanged("Cep");
			}
		}
	}

	[DataMember]
	public int CodigoIbgeMunicipio
	{
		get
		{
			return CodigoIbgeMunicipioField;
		}
		set
		{
			if (!CodigoIbgeMunicipioField.Equals(value))
			{
				CodigoIbgeMunicipioField = value;
				RaisePropertyChanged("CodigoIbgeMunicipio");
			}
		}
	}

	[DataMember]
	public string Complemento
	{
		get
		{
			return ComplementoField;
		}
		set
		{
			if ((object)ComplementoField != value)
			{
				ComplementoField = value;
				RaisePropertyChanged("Complemento");
			}
		}
	}

	[DataMember]
	public string Logradouro
	{
		get
		{
			return LogradouroField;
		}
		set
		{
			if ((object)LogradouroField != value)
			{
				LogradouroField = value;
				RaisePropertyChanged("Logradouro");
			}
		}
	}

	[DataMember]
	public string Numero
	{
		get
		{
			return NumeroField;
		}
		set
		{
			if ((object)NumeroField != value)
			{
				NumeroField = value;
				RaisePropertyChanged("Numero");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "PerfilXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class PerfilXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private char CodigoPerfilField;

	[OptionalField]
	private string UfField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public char CodigoPerfil
	{
		get
		{
			return CodigoPerfilField;
		}
		set
		{
			if (!CodigoPerfilField.Equals(value))
			{
				CodigoPerfilField = value;
				RaisePropertyChanged("CodigoPerfil");
			}
		}
	}

	[DataMember]
	public string Uf
	{
		get
		{
			return UfField;
		}
		set
		{
			if ((object)UfField != value)
			{
				UfField = value;
				RaisePropertyChanged("Uf");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "EcfXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class EcfXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private MarcaEcf MarcaField;

	[OptionalField]
	private EcfTipoModelos ModeloField;

	[OptionalField]
	private string NumeroDeSerieField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public MarcaEcf Marca
	{
		get
		{
			return MarcaField;
		}
		set
		{
			if (!MarcaField.Equals(value))
			{
				MarcaField = value;
				RaisePropertyChanged("Marca");
			}
		}
	}

	[DataMember]
	public EcfTipoModelos Modelo
	{
		get
		{
			return ModeloField;
		}
		set
		{
			if (!ModeloField.Equals(value))
			{
				ModeloField = value;
				RaisePropertyChanged("Modelo");
			}
		}
	}

	[DataMember]
	public string NumeroDeSerie
	{
		get
		{
			return NumeroDeSerieField;
		}
		set
		{
			if ((object)NumeroDeSerieField != value)
			{
				NumeroDeSerieField = value;
				RaisePropertyChanged("NumeroDeSerie");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SatXmlDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class SatXmlDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private MarcaSat MarcaField;

	[OptionalField]
	private ModeloSat ModeloField;

	[OptionalField]
	private string NumeroDeSerieField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public MarcaSat Marca
	{
		get
		{
			return MarcaField;
		}
		set
		{
			if (!MarcaField.Equals(value))
			{
				MarcaField = value;
				RaisePropertyChanged("Marca");
			}
		}
	}

	[DataMember]
	public ModeloSat Modelo
	{
		get
		{
			return ModeloField;
		}
		set
		{
			if (!ModeloField.Equals(value))
			{
				ModeloField = value;
				RaisePropertyChanged("Modelo");
			}
		}
	}

	[DataMember]
	public string NumeroDeSerie
	{
		get
		{
			return NumeroDeSerieField;
		}
		set
		{
			if ((object)NumeroDeSerieField != value)
			{
				NumeroDeSerieField = value;
				RaisePropertyChanged("NumeroDeSerie");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "RequisicaoCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.HabilitacaoXml")]
public class RequisicaoCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string ChaveField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private RamoDeAtividade RamoAtividadeField;

	[OptionalField]
	private string RazaoSocialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Chave
	{
		get
		{
			return ChaveField;
		}
		set
		{
			if ((object)ChaveField != value)
			{
				ChaveField = value;
				RaisePropertyChanged("Chave");
			}
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public RamoDeAtividade RamoAtividade
	{
		get
		{
			return RamoAtividadeField;
		}
		set
		{
			if (!RamoAtividadeField.Equals(value))
			{
				RamoAtividadeField = value;
				RaisePropertyChanged("RamoAtividade");
			}
		}
	}

	[DataMember]
	public string RazaoSocial
	{
		get
		{
			return RazaoSocialField;
		}
		set
		{
			if ((object)RazaoSocialField != value)
			{
				RazaoSocialField = value;
				RaisePropertyChanged("RazaoSocial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoResumoQuantidadeDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitacaoResumoQuantidadeDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int QuantidadeCopiasAdquiridasField;

	[OptionalField]
	private int QuantidadeCopiasCanceladasField;

	[OptionalField]
	private int QuantidadeCopiasHabilitadasField;

	[OptionalField]
	private int QuantidadeCopiasNaoHabilitadasField;

	[OptionalField]
	private string RazaoSocialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int QuantidadeCopiasAdquiridas
	{
		get
		{
			return QuantidadeCopiasAdquiridasField;
		}
		set
		{
			if (!QuantidadeCopiasAdquiridasField.Equals(value))
			{
				QuantidadeCopiasAdquiridasField = value;
				RaisePropertyChanged("QuantidadeCopiasAdquiridas");
			}
		}
	}

	[DataMember]
	public int QuantidadeCopiasCanceladas
	{
		get
		{
			return QuantidadeCopiasCanceladasField;
		}
		set
		{
			if (!QuantidadeCopiasCanceladasField.Equals(value))
			{
				QuantidadeCopiasCanceladasField = value;
				RaisePropertyChanged("QuantidadeCopiasCanceladas");
			}
		}
	}

	[DataMember]
	public int QuantidadeCopiasHabilitadas
	{
		get
		{
			return QuantidadeCopiasHabilitadasField;
		}
		set
		{
			if (!QuantidadeCopiasHabilitadasField.Equals(value))
			{
				QuantidadeCopiasHabilitadasField = value;
				RaisePropertyChanged("QuantidadeCopiasHabilitadas");
			}
		}
	}

	[DataMember]
	public int QuantidadeCopiasNaoHabilitadas
	{
		get
		{
			return QuantidadeCopiasNaoHabilitadasField;
		}
		set
		{
			if (!QuantidadeCopiasNaoHabilitadasField.Equals(value))
			{
				QuantidadeCopiasNaoHabilitadasField = value;
				RaisePropertyChanged("QuantidadeCopiasNaoHabilitadas");
			}
		}
	}

	[DataMember]
	public string RazaoSocial
	{
		get
		{
			return RazaoSocialField;
		}
		set
		{
			if ((object)RazaoSocialField != value)
			{
				RazaoSocialField = value;
				RaisePropertyChanged("RazaoSocial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "MunicipioListaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Pessoa")]
public class MunicipioListaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "RevendaCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class RevendaCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private PessoaJuridicaCompletaDto PessoaJuridicaField;

	[OptionalField]
	private SerialRevendaCompletoDto[] SeriaisRevendaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public PessoaJuridicaCompletaDto PessoaJuridica
	{
		get
		{
			return PessoaJuridicaField;
		}
		set
		{
			if (PessoaJuridicaField != value)
			{
				PessoaJuridicaField = value;
				RaisePropertyChanged("PessoaJuridica");
			}
		}
	}

	[DataMember]
	public SerialRevendaCompletoDto[] SeriaisRevenda
	{
		get
		{
			return SeriaisRevendaField;
		}
		set
		{
			if (SeriaisRevendaField != value)
			{
				SeriaisRevendaField = value;
				RaisePropertyChanged("SeriaisRevenda");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "RevendaListaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class RevendaListaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NomeField;

	[OptionalField]
	private SerialListaDto[] SeriaisField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string Nome
	{
		get
		{
			return NomeField;
		}
		set
		{
			if ((object)NomeField != value)
			{
				NomeField = value;
				RaisePropertyChanged("Nome");
			}
		}
	}

	[DataMember]
	public SerialListaDto[] Seriais
	{
		get
		{
			return SeriaisField;
		}
		set
		{
			if (SeriaisField != value)
			{
				SeriaisField = value;
				RaisePropertyChanged("Seriais");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "SerialListaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class SerialListaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string NumeroSerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string NumeroSerial
	{
		get
		{
			return NumeroSerialField;
		}
		set
		{
			if ((object)NumeroSerialField != value)
			{
				NumeroSerialField = value;
				RaisePropertyChanged("NumeroSerial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ModulosHabilitacaoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ModulosHabilitacaoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private ModuloCompletoDto[] ModulosField;

	[OptionalField]
	private string NumeroSerialField;

	[OptionalField]
	private SerialSistema SistemaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public ModuloCompletoDto[] Modulos
	{
		get
		{
			return ModulosField;
		}
		set
		{
			if (ModulosField != value)
			{
				ModulosField = value;
				RaisePropertyChanged("Modulos");
			}
		}
	}

	[DataMember]
	public string NumeroSerial
	{
		get
		{
			return NumeroSerialField;
		}
		set
		{
			if ((object)NumeroSerialField != value)
			{
				NumeroSerialField = value;
				RaisePropertyChanged("NumeroSerial");
			}
		}
	}

	[DataMember]
	public SerialSistema Sistema
	{
		get
		{
			return SistemaField;
		}
		set
		{
			if (!SistemaField.Equals(value))
			{
				SistemaField = value;
				RaisePropertyChanged("Sistema");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "ValidacaoPafCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class ValidacaoPafCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataInicialField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private UfCompletoDto UfField;

	[OptionalField]
	private VersaoCompletaDto VersaoField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataInicial
	{
		get
		{
			return DataInicialField;
		}
		set
		{
			if (!DataInicialField.Equals(value))
			{
				DataInicialField = value;
				RaisePropertyChanged("DataInicial");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public UfCompletoDto Uf
	{
		get
		{
			return UfField;
		}
		set
		{
			if (UfField != value)
			{
				UfField = value;
				RaisePropertyChanged("Uf");
			}
		}
	}

	[DataMember]
	public VersaoCompletaDto Versao
	{
		get
		{
			return VersaoField;
		}
		set
		{
			if (VersaoField != value)
			{
				VersaoField = value;
				RaisePropertyChanged("Versao");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "VersaoCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class VersaoCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private int ControleVersaoInternoField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private string VersaoErPafField;

	[OptionalField]
	private string VersaoSistemaField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public int ControleVersaoInterno
	{
		get
		{
			return ControleVersaoInternoField;
		}
		set
		{
			if (!ControleVersaoInternoField.Equals(value))
			{
				ControleVersaoInternoField = value;
				RaisePropertyChanged("ControleVersaoInterno");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public string VersaoErPaf
	{
		get
		{
			return VersaoErPafField;
		}
		set
		{
			if ((object)VersaoErPafField != value)
			{
				VersaoErPafField = value;
				RaisePropertyChanged("VersaoErPaf");
			}
		}
	}

	[DataMember]
	public string VersaoSistema
	{
		get
		{
			return VersaoSistemaField;
		}
		set
		{
			if ((object)VersaoSistemaField != value)
			{
				VersaoSistemaField = value;
				RaisePropertyChanged("VersaoSistema");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "HabilitacaoG4ResumoDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.SerialHabilitacao")]
public class HabilitacaoG4ResumoDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private string CnpjField;

	[OptionalField]
	private DateTime? DataHoraHabilitacaoField;

	[OptionalField]
	private string ProgramaField;

	[OptionalField]
	private string RazaoSocialClienteFinalField;

	[OptionalField]
	private string RazaoSocialRevendaField;

	[OptionalField]
	private string SituacaoDoSerialField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public string Cnpj
	{
		get
		{
			return CnpjField;
		}
		set
		{
			if ((object)CnpjField != value)
			{
				CnpjField = value;
				RaisePropertyChanged("Cnpj");
			}
		}
	}

	[DataMember]
	public DateTime? DataHoraHabilitacao
	{
		get
		{
			return DataHoraHabilitacaoField;
		}
		set
		{
			if (!DataHoraHabilitacaoField.Equals(value))
			{
				DataHoraHabilitacaoField = value;
				RaisePropertyChanged("DataHoraHabilitacao");
			}
		}
	}

	[DataMember]
	public string Programa
	{
		get
		{
			return ProgramaField;
		}
		set
		{
			if ((object)ProgramaField != value)
			{
				ProgramaField = value;
				RaisePropertyChanged("Programa");
			}
		}
	}

	[DataMember]
	public string RazaoSocialClienteFinal
	{
		get
		{
			return RazaoSocialClienteFinalField;
		}
		set
		{
			if ((object)RazaoSocialClienteFinalField != value)
			{
				RazaoSocialClienteFinalField = value;
				RaisePropertyChanged("RazaoSocialClienteFinal");
			}
		}
	}

	[DataMember]
	public string RazaoSocialRevenda
	{
		get
		{
			return RazaoSocialRevendaField;
		}
		set
		{
			if ((object)RazaoSocialRevendaField != value)
			{
				RazaoSocialRevendaField = value;
				RaisePropertyChanged("RazaoSocialRevenda");
			}
		}
	}

	[DataMember]
	public string SituacaoDoSerial
	{
		get
		{
			return SituacaoDoSerialField;
		}
		set
		{
			if ((object)SituacaoDoSerialField != value)
			{
				SituacaoDoSerialField = value;
				RaisePropertyChanged("SituacaoDoSerial");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[Serializable]
[DebuggerStepThrough]
[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
[DataContract(Name = "AuditoriaCompletaDto", Namespace = "http://schemas.datacontract.org/2004/07/Digisat.Habilitacao.Service.Contract.DataContracts.Autenticacao")]
public class AuditoriaCompletaDto : IExtensibleDataObject, INotifyPropertyChanged
{
	[NonSerialized]
	private ExtensionDataObject extensionDataField;

	[OptionalField]
	private DateTime DataHoraField;

	[OptionalField]
	private int IdField;

	[OptionalField]
	private int IdUsuarioField;

	[OptionalField]
	private string InformacaoField;

	[OptionalField]
	private string ModuloField;

	[Browsable(false)]
	public ExtensionDataObject ExtensionData
	{
		get
		{
			return extensionDataField;
		}
		set
		{
			extensionDataField = value;
		}
	}

	[DataMember]
	public DateTime DataHora
	{
		get
		{
			return DataHoraField;
		}
		set
		{
			if (!DataHoraField.Equals(value))
			{
				DataHoraField = value;
				RaisePropertyChanged("DataHora");
			}
		}
	}

	[DataMember]
	public int Id
	{
		get
		{
			return IdField;
		}
		set
		{
			if (!IdField.Equals(value))
			{
				IdField = value;
				RaisePropertyChanged("Id");
			}
		}
	}

	[DataMember]
	public int IdUsuario
	{
		get
		{
			return IdUsuarioField;
		}
		set
		{
			if (!IdUsuarioField.Equals(value))
			{
				IdUsuarioField = value;
				RaisePropertyChanged("IdUsuario");
			}
		}
	}

	[DataMember]
	public string Informacao
	{
		get
		{
			return InformacaoField;
		}
		set
		{
			if ((object)InformacaoField != value)
			{
				InformacaoField = value;
				RaisePropertyChanged("Informacao");
			}
		}
	}

	[DataMember]
	public string Modulo
	{
		get
		{
			return ModuloField;
		}
		set
		{
			if ((object)ModuloField != value)
			{
				ModuloField = value;
				RaisePropertyChanged("Modulo");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void RaisePropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IHabilitacaoServiceWeb")]
public interface IHabilitacaoServiceWeb
{
	[OperationContract(Action = "http://tempuri.org/IHabilitacaoServiceWeb/ObterXmlDaHabilitacao", ReplyAction = "http://tempuri.org/IHabilitacaoServiceWeb/ObterXmlDaHabilitacaoResponse")]
	string ObterXmlDaHabilitacao(string serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoServiceWeb/GerarContraChaveValidacao", ReplyAction = "http://tempuri.org/IHabilitacaoServiceWeb/GerarContraChaveValidacaoResponse")]
	string GerarContraChaveValidacao(string serial, bool revalidacaoSolicitadaPeloCliente);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IHabilitacaoServiceWebChannel : IHabilitacaoServiceWeb, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class HabilitacaoServiceWebClient : ClientBase<IHabilitacaoServiceWeb>, IHabilitacaoServiceWeb
{
	public HabilitacaoServiceWebClient()
	{
	}

	public HabilitacaoServiceWebClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public HabilitacaoServiceWebClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoServiceWebClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoServiceWebClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public string ObterXmlDaHabilitacao(string serial)
	{
		return base.Channel.ObterXmlDaHabilitacao(serial);
	}

	public string GerarContraChaveValidacao(string serial, bool revalidacaoSolicitadaPeloCliente)
	{
		return base.Channel.GerarContraChaveValidacao(serial, revalidacaoSolicitadaPeloCliente);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IClienteRevendaService")]
public interface IClienteRevendaService
{
	[OperationContract(Action = "http://tempuri.org/IClienteRevendaService/SalvarClienteRevenda", ReplyAction = "http://tempuri.org/IClienteRevendaService/SalvarClienteRevendaResponse")]
	int SalvarClienteRevenda(ClienteRevendaCompletoDto clienteRevenda);

	[OperationContract(Action = "http://tempuri.org/IClienteRevendaService/ObterClienteRevendaCompletoPorId", ReplyAction = "http://tempuri.org/IClienteRevendaService/ObterClienteRevendaCompletoPorIdResponse")]
	ClienteRevendaCompletoDto ObterClienteRevendaCompletoPorId(int id);

	[OperationContract(Action = "http://tempuri.org/IClienteRevendaService/ObterClienteRevendaCompletoPorDocumento", ReplyAction = "http://tempuri.org/IClienteRevendaService/ObterClienteRevendaCompletoPorDocumentoResponse")]
	ClienteRevendaCompletoDto ObterClienteRevendaCompletoPorDocumento(string informacao);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IClienteRevendaServiceChannel : IClienteRevendaService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class ClienteRevendaServiceClient : ClientBase<IClienteRevendaService>, IClienteRevendaService
{
	public ClienteRevendaServiceClient()
	{
	}

	public ClienteRevendaServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public ClienteRevendaServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public ClienteRevendaServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public ClienteRevendaServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public int SalvarClienteRevenda(ClienteRevendaCompletoDto clienteRevenda)
	{
		return base.Channel.SalvarClienteRevenda(clienteRevenda);
	}

	public ClienteRevendaCompletoDto ObterClienteRevendaCompletoPorId(int id)
	{
		return base.Channel.ObterClienteRevendaCompletoPorId(id);
	}

	public ClienteRevendaCompletoDto ObterClienteRevendaCompletoPorDocumento(string informacao)
	{
		return base.Channel.ObterClienteRevendaCompletoPorDocumento(informacao);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IHabilitacaoService")]
public interface IHabilitacaoService
{
	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterDadosDashboard", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterDadosDashboardResponse")]
	DashboardDto ObterDadosDashboard();

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterRelatorioSistemasPorRamoDetalhado", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterRelatorioSistemasPorRamoDetalhadoResponse")]
	SistemasPorRamoDetalhadoDto[] ObterRelatorioSistemasPorRamoDetalhado(int nivelDetalhamento);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterSistemasG5NaoAtualizadosParaG6PorCnpjRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterSistemasG5NaoAtualizadosParaG6PorCnpjRevendaResponse")]
	HabilitacaoResumoDto[] ObterSistemasG5NaoAtualizadosParaG6PorCnpjRevenda(string cnpjRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorId", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorIdResponse")]
	HabilitacaoCompletaDto ObterHabilitacaoCompletaPorId(int id);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/SalvarHabilitacao", ReplyAction = "http://tempuri.org/IHabilitacaoService/SalvarHabilitacaoResponse")]
	int SalvarHabilitacao(HabilitacaoCompletaDto habilitacaoCompletaDto);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/AlterarCnpjERazaoSocial", ReplyAction = "http://tempuri.org/IHabilitacaoService/AlterarCnpjERazaoSocialResponse")]
	void AlterarCnpjERazaoSocial(string cnpj, string razao, string serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/SalvarHabilitacaoRefatorado", ReplyAction = "http://tempuri.org/IHabilitacaoService/SalvarHabilitacaoRefatoradoResponse")]
	void SalvarHabilitacaoRefatorado(HabilitacaoCompletaDto habilitacao, bool viaCliente, string nomeMunicipio, string uf, string serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorNumeroSerialECodigoValidacao", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorNumeroSerialECodigoValidacaoResponse")]
	HabilitacaoCompletaDto ObterHabilitacaoCompletaPorNumeroSerialECodigoValidacao(SerialComCodigoDeSegurancaDto serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoMatrizFilial", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoMatrizFilialResponse")]
	HabilitacaoCompletaDto ObterHabilitacaoMatrizFilial(SerialComCodigoDeSegurancaDto serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoCompletaPorCnpjClienteFinal", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoCompletaPorCnpjClienteFinalResponse")]
	HabilitacaoCompletaDto[] ObterListaHabilitacaoCompletaPorCnpjClienteFinal(string cnpjCliente);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoCompletaPorCnpjRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoCompletaPorCnpjRevendaResponse")]
	HabilitacaoCompletaDto[] ObterListaHabilitacaoCompletaPorCnpjRevenda(string cnpjRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjRevendaResponse")]
	HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjRevenda(string cnpjRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjRevendaSimplificado", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjRevendaSimplificadoResponse")]
	HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjRevendaSimplificado(string cnpjRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterSistemasHabilitadosPorPeriodo", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterSistemasHabilitadosPorPeriodoResponse")]
	ResumoHabilitadoPeriodoDto ObterSistemasHabilitadosPorPeriodo(string dataInicial, string dataFinal);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterResumoSerialPorNumeroSerial", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterResumoSerialPorNumeroSerialResponse")]
	ResumoSerialDto ObterResumoSerialPorNumeroSerial(string numeroSerial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjClienteFinal", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterListaHabilitacaoResumoPorCnpjClienteFinalResponse")]
	HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjClienteFinal(string cnpjClienteFinal);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorNumeroSerial", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoCompletaPorNumeroSerialResponse")]
	HabilitacaoCompletaDto ObterHabilitacaoCompletaPorNumeroSerial(string numeroSerial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/PesquisarHabilitacaoPorRazaoSocialDocumentoNumeroSerial", ReplyAction = "http://tempuri.org/IHabilitacaoService/PesquisarHabilitacaoPorRazaoSocialDocumentoNumeroSerialResponse")]
	HabilitacaoListaDto[] PesquisarHabilitacaoPorRazaoSocialDocumentoNumeroSerial(string informacao);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/GerarContraChave", ReplyAction = "http://tempuri.org/IHabilitacaoService/GerarContraChaveResponse")]
	string GerarContraChave(string chave, bool revalidacaoSolicitadaPeloCliente);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/RetornarXml", ReplyAction = "http://tempuri.org/IHabilitacaoService/RetornarXmlResponse")]
	string RetornarXml(HabilitacaoXmlDto habilitarCompletoDto);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/GerarNovosSeriais", ReplyAction = "http://tempuri.org/IHabilitacaoService/GerarNovosSeriaisResponse")]
	string[] GerarNovosSeriais(SerialSistema serialSistema, SerialSistema sistemaOrigem, int quantidade, string versao, bool atualizacao);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterRegistrosBlacklist", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterRegistrosBlacklistResponse")]
	string[] ObterRegistrosBlacklist();

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ExportarBlacklist", ReplyAction = "http://tempuri.org/IHabilitacaoService/ExportarBlacklistResponse")]
	string[] ExportarBlacklist();

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterXmlAtualizado", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterXmlAtualizadoResponse")]
	string ObterXmlAtualizado(string serial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoListaPorIdRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoListaPorIdRevendaResponse")]
	HabilitacaoListaDto[] ObterHabilitacaoListaPorIdRevenda(int id);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/RequisitarHabilitacao", ReplyAction = "http://tempuri.org/IHabilitacaoService/RequisitarHabilitacaoResponse")]
	string RequisitarHabilitacao(RequisicaoCompletaDto requisicaoCompleta);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/RetornarCodigoDeSeguranca", ReplyAction = "http://tempuri.org/IHabilitacaoService/RetornarCodigoDeSegurancaResponse")]
	string RetornarCodigoDeSeguranca(string numeroSerial);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/AdicionarLicencas", ReplyAction = "http://tempuri.org/IHabilitacaoService/AdicionarLicencasResponse")]
	bool AdicionarLicencas(int idHabilitacao, int quantidade, string numeroNota);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/AlterarVersaoSistema", ReplyAction = "http://tempuri.org/IHabilitacaoService/AlterarVersaoSistemaResponse")]
	bool AlterarVersaoSistema(string serial, SerialSistema sistema, string justificativa, string nomeUsuario);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/CancelarSistema", ReplyAction = "http://tempuri.org/IHabilitacaoService/CancelarSistemaResponse")]
	bool CancelarSistema(string numeroSerial, string motivo, string justificativa);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoResumoQuantidadePorCnpjRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterHabilitacaoResumoQuantidadePorCnpjRevendaResponse")]
	HabilitacaoResumoQuantidadeDto ObterHabilitacaoResumoQuantidadePorCnpjRevenda(string cnpjRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/ObterUltimosSeriais", ReplyAction = "http://tempuri.org/IHabilitacaoService/ObterUltimosSeriaisResponse")]
	SerialCompletoDto[] ObterUltimosSeriais(int quantidade);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/GerarSerialFullOnline", ReplyAction = "http://tempuri.org/IHabilitacaoService/GerarSerialFullOnlineResponse")]
	string GerarSerialFullOnline(string sistema, int quantidade, int idRevenda);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/GerarSerialAtualizacaoOnline", ReplyAction = "http://tempuri.org/IHabilitacaoService/GerarSerialAtualizacaoOnlineResponse")]
	string GerarSerialAtualizacaoOnline(string sistemaDestino, int quantidade, int idRevenda, string sistemaOrigem);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/SetarG5ComoNaoAtualizado", ReplyAction = "http://tempuri.org/IHabilitacaoService/SetarG5ComoNaoAtualizadoResponse")]
	void SetarG5ComoNaoAtualizado(string serialG5);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/AdicionarModulosNoSerial", ReplyAction = "http://tempuri.org/IHabilitacaoService/AdicionarModulosNoSerialResponse")]
	void AdicionarModulosNoSerial(string serial, ModuloCompletoDto[] modulos);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoService/AlterarAtualizacaoParaCopiaFull", ReplyAction = "http://tempuri.org/IHabilitacaoService/AlterarAtualizacaoParaCopiaFullResponse")]
	void AlterarAtualizacaoParaCopiaFull(bool isCopiaFull, string numeroSerial);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IHabilitacaoServiceChannel : IHabilitacaoService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class HabilitacaoServiceClient : ClientBase<IHabilitacaoService>, IHabilitacaoService
{
	public HabilitacaoServiceClient()
	{
	}

	public HabilitacaoServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public HabilitacaoServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public DashboardDto ObterDadosDashboard()
	{
		return base.Channel.ObterDadosDashboard();
	}

	public SistemasPorRamoDetalhadoDto[] ObterRelatorioSistemasPorRamoDetalhado(int nivelDetalhamento)
	{
		return base.Channel.ObterRelatorioSistemasPorRamoDetalhado(nivelDetalhamento);
	}

	public HabilitacaoResumoDto[] ObterSistemasG5NaoAtualizadosParaG6PorCnpjRevenda(string cnpjRevenda)
	{
		return base.Channel.ObterSistemasG5NaoAtualizadosParaG6PorCnpjRevenda(cnpjRevenda);
	}

	public HabilitacaoCompletaDto ObterHabilitacaoCompletaPorId(int id)
	{
		return base.Channel.ObterHabilitacaoCompletaPorId(id);
	}

	public int SalvarHabilitacao(HabilitacaoCompletaDto habilitacaoCompletaDto)
	{
		return base.Channel.SalvarHabilitacao(habilitacaoCompletaDto);
	}

	public void AlterarCnpjERazaoSocial(string cnpj, string razao, string serial)
	{
		base.Channel.AlterarCnpjERazaoSocial(cnpj, razao, serial);
	}

	public void SalvarHabilitacaoRefatorado(HabilitacaoCompletaDto habilitacao, bool viaCliente, string nomeMunicipio, string uf, string serial)
	{
		base.Channel.SalvarHabilitacaoRefatorado(habilitacao, viaCliente, nomeMunicipio, uf, serial);
	}

	public HabilitacaoCompletaDto ObterHabilitacaoCompletaPorNumeroSerialECodigoValidacao(SerialComCodigoDeSegurancaDto serial)
	{
		return base.Channel.ObterHabilitacaoCompletaPorNumeroSerialECodigoValidacao(serial);
	}

	public HabilitacaoCompletaDto ObterHabilitacaoMatrizFilial(SerialComCodigoDeSegurancaDto serial)
	{
		return base.Channel.ObterHabilitacaoMatrizFilial(serial);
	}

	public HabilitacaoCompletaDto[] ObterListaHabilitacaoCompletaPorCnpjClienteFinal(string cnpjCliente)
	{
		return base.Channel.ObterListaHabilitacaoCompletaPorCnpjClienteFinal(cnpjCliente);
	}

	public HabilitacaoCompletaDto[] ObterListaHabilitacaoCompletaPorCnpjRevenda(string cnpjRevenda)
	{
		return base.Channel.ObterListaHabilitacaoCompletaPorCnpjRevenda(cnpjRevenda);
	}

	public HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjRevenda(string cnpjRevenda)
	{
		return base.Channel.ObterListaHabilitacaoResumoPorCnpjRevenda(cnpjRevenda);
	}

	public HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjRevendaSimplificado(string cnpjRevenda)
	{
		return base.Channel.ObterListaHabilitacaoResumoPorCnpjRevendaSimplificado(cnpjRevenda);
	}

	public ResumoHabilitadoPeriodoDto ObterSistemasHabilitadosPorPeriodo(string dataInicial, string dataFinal)
	{
		return base.Channel.ObterSistemasHabilitadosPorPeriodo(dataInicial, dataFinal);
	}

	public ResumoSerialDto ObterResumoSerialPorNumeroSerial(string numeroSerial)
	{
		return base.Channel.ObterResumoSerialPorNumeroSerial(numeroSerial);
	}

	public HabilitacaoResumoDto[] ObterListaHabilitacaoResumoPorCnpjClienteFinal(string cnpjClienteFinal)
	{
		return base.Channel.ObterListaHabilitacaoResumoPorCnpjClienteFinal(cnpjClienteFinal);
	}

	public HabilitacaoCompletaDto ObterHabilitacaoCompletaPorNumeroSerial(string numeroSerial)
	{
		return base.Channel.ObterHabilitacaoCompletaPorNumeroSerial(numeroSerial);
	}

	public HabilitacaoListaDto[] PesquisarHabilitacaoPorRazaoSocialDocumentoNumeroSerial(string informacao)
	{
		return base.Channel.PesquisarHabilitacaoPorRazaoSocialDocumentoNumeroSerial(informacao);
	}

	public string GerarContraChave(string chave, bool revalidacaoSolicitadaPeloCliente)
	{
		return base.Channel.GerarContraChave(chave, revalidacaoSolicitadaPeloCliente);
	}

	public string RetornarXml(HabilitacaoXmlDto habilitarCompletoDto)
	{
		return base.Channel.RetornarXml(habilitarCompletoDto);
	}

	public string[] GerarNovosSeriais(SerialSistema serialSistema, SerialSistema sistemaOrigem, int quantidade, string versao, bool atualizacao)
	{
		return base.Channel.GerarNovosSeriais(serialSistema, sistemaOrigem, quantidade, versao, atualizacao);
	}

	public string[] ObterRegistrosBlacklist()
	{
		return base.Channel.ObterRegistrosBlacklist();
	}

	public string[] ExportarBlacklist()
	{
		return base.Channel.ExportarBlacklist();
	}

	public string ObterXmlAtualizado(string serial)
	{
		return base.Channel.ObterXmlAtualizado(serial);
	}

	public HabilitacaoListaDto[] ObterHabilitacaoListaPorIdRevenda(int id)
	{
		return base.Channel.ObterHabilitacaoListaPorIdRevenda(id);
	}

	public string RequisitarHabilitacao(RequisicaoCompletaDto requisicaoCompleta)
	{
		return base.Channel.RequisitarHabilitacao(requisicaoCompleta);
	}

	public string RetornarCodigoDeSeguranca(string numeroSerial)
	{
		return base.Channel.RetornarCodigoDeSeguranca(numeroSerial);
	}

	public bool AdicionarLicencas(int idHabilitacao, int quantidade, string numeroNota)
	{
		return base.Channel.AdicionarLicencas(idHabilitacao, quantidade, numeroNota);
	}

	public bool AlterarVersaoSistema(string serial, SerialSistema sistema, string justificativa, string nomeUsuario)
	{
		return base.Channel.AlterarVersaoSistema(serial, sistema, justificativa, nomeUsuario);
	}

	public bool CancelarSistema(string numeroSerial, string motivo, string justificativa)
	{
		return base.Channel.CancelarSistema(numeroSerial, motivo, justificativa);
	}

	public HabilitacaoResumoQuantidadeDto ObterHabilitacaoResumoQuantidadePorCnpjRevenda(string cnpjRevenda)
	{
		return base.Channel.ObterHabilitacaoResumoQuantidadePorCnpjRevenda(cnpjRevenda);
	}

	public SerialCompletoDto[] ObterUltimosSeriais(int quantidade)
	{
		return base.Channel.ObterUltimosSeriais(quantidade);
	}

	public string GerarSerialFullOnline(string sistema, int quantidade, int idRevenda)
	{
		return base.Channel.GerarSerialFullOnline(sistema, quantidade, idRevenda);
	}

	public string GerarSerialAtualizacaoOnline(string sistemaDestino, int quantidade, int idRevenda, string sistemaOrigem)
	{
		return base.Channel.GerarSerialAtualizacaoOnline(sistemaDestino, quantidade, idRevenda, sistemaOrigem);
	}

	public void SetarG5ComoNaoAtualizado(string serialG5)
	{
		base.Channel.SetarG5ComoNaoAtualizado(serialG5);
	}

	public void AdicionarModulosNoSerial(string serial, ModuloCompletoDto[] modulos)
	{
		base.Channel.AdicionarModulosNoSerial(serial, modulos);
	}

	public void AlterarAtualizacaoParaCopiaFull(bool isCopiaFull, string numeroSerial)
	{
		base.Channel.AlterarAtualizacaoParaCopiaFull(isCopiaFull, numeroSerial);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IMunicipioService")]
public interface IMunicipioService
{
	[OperationContract(Action = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorNome", ReplyAction = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorNomeResponse")]
	MunicipioCompletoDto ObterMunicipioCompletoPorNome(string descricaoMunicipio, string descricaoUf);

	[OperationContract(Action = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorNomeESiglaUf", ReplyAction = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorNomeESiglaUfResponse")]
	MunicipioCompletoDto ObterMunicipioCompletoPorNomeESiglaUf(string nomeMunicipio, string siglaUf);

	[OperationContract(Action = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorId", ReplyAction = "http://tempuri.org/IMunicipioService/ObterMunicipioCompletoPorIdResponse")]
	MunicipioCompletoDto ObterMunicipioCompletoPorId(int id);

	[OperationContract(Action = "http://tempuri.org/IMunicipioService/ObterMunicipioListaPorIdUf", ReplyAction = "http://tempuri.org/IMunicipioService/ObterMunicipioListaPorIdUfResponse")]
	MunicipioListaDto[] ObterMunicipioListaPorIdUf(int idUf);

	[OperationContract(Action = "http://tempuri.org/IMunicipioService/ObterMunicipioListaPorSiglaUf", ReplyAction = "http://tempuri.org/IMunicipioService/ObterMunicipioListaPorSiglaUfResponse")]
	MunicipioListaDto[] ObterMunicipioListaPorSiglaUf(string siglaUf);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IMunicipioServiceChannel : IMunicipioService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class MunicipioServiceClient : ClientBase<IMunicipioService>, IMunicipioService
{
	public MunicipioServiceClient()
	{
	}

	public MunicipioServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public MunicipioServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public MunicipioServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public MunicipioServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public MunicipioCompletoDto ObterMunicipioCompletoPorNome(string descricaoMunicipio, string descricaoUf)
	{
		return base.Channel.ObterMunicipioCompletoPorNome(descricaoMunicipio, descricaoUf);
	}

	public MunicipioCompletoDto ObterMunicipioCompletoPorNomeESiglaUf(string nomeMunicipio, string siglaUf)
	{
		return base.Channel.ObterMunicipioCompletoPorNomeESiglaUf(nomeMunicipio, siglaUf);
	}

	public MunicipioCompletoDto ObterMunicipioCompletoPorId(int id)
	{
		return base.Channel.ObterMunicipioCompletoPorId(id);
	}

	public MunicipioListaDto[] ObterMunicipioListaPorIdUf(int idUf)
	{
		return base.Channel.ObterMunicipioListaPorIdUf(idUf);
	}

	public MunicipioListaDto[] ObterMunicipioListaPorSiglaUf(string siglaUf)
	{
		return base.Channel.ObterMunicipioListaPorSiglaUf(siglaUf);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IPerfilService")]
public interface IPerfilService
{
	[OperationContract(Action = "http://tempuri.org/IPerfilService/ObterPerfilCompletoPorId", ReplyAction = "http://tempuri.org/IPerfilService/ObterPerfilCompletoPorIdResponse")]
	PerfilCompletoDto ObterPerfilCompletoPorId(int id);

	[OperationContract(Action = "http://tempuri.org/IPerfilService/ObterPerfilCompletoPorSiglaDaUf", ReplyAction = "http://tempuri.org/IPerfilService/ObterPerfilCompletoPorSiglaDaUfResponse")]
	PerfilCompletoDto ObterPerfilCompletoPorSiglaDaUf(string siglaUf);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IPerfilServiceChannel : IPerfilService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class PerfilServiceClient : ClientBase<IPerfilService>, IPerfilService
{
	public PerfilServiceClient()
	{
	}

	public PerfilServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public PerfilServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public PerfilServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public PerfilServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public PerfilCompletoDto ObterPerfilCompletoPorId(int id)
	{
		return base.Channel.ObterPerfilCompletoPorId(id);
	}

	public PerfilCompletoDto ObterPerfilCompletoPorSiglaDaUf(string siglaUf)
	{
		return base.Channel.ObterPerfilCompletoPorSiglaDaUf(siglaUf);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IRevendaService")]
public interface IRevendaService
{
	[OperationContract(Action = "http://tempuri.org/IRevendaService/SalvarRevenda", ReplyAction = "http://tempuri.org/IRevendaService/SalvarRevendaResponse")]
	int SalvarRevenda(RevendaCompletaDto revenda);

	[OperationContract(Action = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorId", ReplyAction = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorIdResponse")]
	RevendaCompletaDto ObterRevendaCompletaPorId(int id);

	[OperationContract(Action = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorDocumento", ReplyAction = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorDocumentoResponse")]
	RevendaCompletaDto ObterRevendaCompletaPorDocumento(string informacao);

	[OperationContract(Action = "http://tempuri.org/IRevendaService/VincularSeriaisComARevenda", ReplyAction = "http://tempuri.org/IRevendaService/VincularSeriaisComARevendaResponse")]
	void VincularSeriaisComARevenda(RevendaListaDto revendaLista);

	[OperationContract(Action = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorIdSerialRevenda", ReplyAction = "http://tempuri.org/IRevendaService/ObterRevendaCompletaPorIdSerialRevendaResponse")]
	RevendaCompletaDto ObterRevendaCompletaPorIdSerialRevenda(int id);

	[OperationContract(Action = "http://tempuri.org/IRevendaService/DesvincularSerialComARevenda", ReplyAction = "http://tempuri.org/IRevendaService/DesvincularSerialComARevendaResponse")]
	void DesvincularSerialComARevenda(string numeroSerial);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IRevendaServiceChannel : IRevendaService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class RevendaServiceClient : ClientBase<IRevendaService>, IRevendaService
{
	public RevendaServiceClient()
	{
	}

	public RevendaServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public RevendaServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public RevendaServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public RevendaServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public int SalvarRevenda(RevendaCompletaDto revenda)
	{
		return base.Channel.SalvarRevenda(revenda);
	}

	public RevendaCompletaDto ObterRevendaCompletaPorId(int id)
	{
		return base.Channel.ObterRevendaCompletaPorId(id);
	}

	public RevendaCompletaDto ObterRevendaCompletaPorDocumento(string informacao)
	{
		return base.Channel.ObterRevendaCompletaPorDocumento(informacao);
	}

	public void VincularSeriaisComARevenda(RevendaListaDto revendaLista)
	{
		base.Channel.VincularSeriaisComARevenda(revendaLista);
	}

	public RevendaCompletaDto ObterRevendaCompletaPorIdSerialRevenda(int id)
	{
		return base.Channel.ObterRevendaCompletaPorIdSerialRevenda(id);
	}

	public void DesvincularSerialComARevenda(string numeroSerial)
	{
		base.Channel.DesvincularSerialComARevenda(numeroSerial);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.ISerialService")]
public interface ISerialService
{
	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterSerialCompletoPorNumeroSerial", ReplyAction = "http://tempuri.org/ISerialService/ObterSerialCompletoPorNumeroSerialResponse")]
	SerialCompletoDto ObterSerialCompletoPorNumeroSerial(string numeroSerial);

	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisG6Atualizacao", ReplyAction = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisG6AtualizacaoResponse")]
	void ObterArquivoCsvDosSeriaisG6Atualizacao();

	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisGerados", ReplyAction = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisGeradosResponse")]
	void ObterArquivoCsvDosSeriaisGerados();

	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisAtualizacao", ReplyAction = "http://tempuri.org/ISerialService/ObterArquivoCsvDosSeriaisAtualizacaoResponse")]
	void ObterArquivoCsvDosSeriaisAtualizacao(string versao);

	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterSerialParaVincular", ReplyAction = "http://tempuri.org/ISerialService/ObterSerialParaVincularResponse")]
	SerialListaDto ObterSerialParaVincular(string numeroSerial);

	[OperationContract(Action = "http://tempuri.org/ISerialService/ValidarSerialGravado", ReplyAction = "http://tempuri.org/ISerialService/ValidarSerialGravadoResponse")]
	bool ValidarSerialGravado(string serial);

	[OperationContract(Action = "http://tempuri.org/ISerialService/ObterModulosPorNumeroSerial", ReplyAction = "http://tempuri.org/ISerialService/ObterModulosPorNumeroSerialResponse")]
	ModulosHabilitacaoDto ObterModulosPorNumeroSerial(string numeroSerial);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface ISerialServiceChannel : ISerialService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class SerialServiceClient : ClientBase<ISerialService>, ISerialService
{
	public SerialServiceClient()
	{
	}

	public SerialServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public SerialServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public SerialServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public SerialServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public SerialCompletoDto ObterSerialCompletoPorNumeroSerial(string numeroSerial)
	{
		return base.Channel.ObterSerialCompletoPorNumeroSerial(numeroSerial);
	}

	public void ObterArquivoCsvDosSeriaisG6Atualizacao()
	{
		base.Channel.ObterArquivoCsvDosSeriaisG6Atualizacao();
	}

	public void ObterArquivoCsvDosSeriaisGerados()
	{
		base.Channel.ObterArquivoCsvDosSeriaisGerados();
	}

	public void ObterArquivoCsvDosSeriaisAtualizacao(string versao)
	{
		base.Channel.ObterArquivoCsvDosSeriaisAtualizacao(versao);
	}

	public SerialListaDto ObterSerialParaVincular(string numeroSerial)
	{
		return base.Channel.ObterSerialParaVincular(numeroSerial);
	}

	public bool ValidarSerialGravado(string serial)
	{
		return base.Channel.ValidarSerialGravado(serial);
	}

	public ModulosHabilitacaoDto ObterModulosPorNumeroSerial(string numeroSerial)
	{
		return base.Channel.ObterModulosPorNumeroSerial(numeroSerial);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IValidacaoPafService")]
public interface IValidacaoPafService
{
	[OperationContract(Action = "http://tempuri.org/IValidacaoPafService/ObterValidacaoPafCompletaPorIdUf", ReplyAction = "http://tempuri.org/IValidacaoPafService/ObterValidacaoPafCompletaPorIdUfResponse")]
	ValidacaoPafCompletaDto ObterValidacaoPafCompletaPorIdUf(int idUf);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IValidacaoPafServiceChannel : IValidacaoPafService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class ValidacaoPafServiceClient : ClientBase<IValidacaoPafService>, IValidacaoPafService
{
	public ValidacaoPafServiceClient()
	{
	}

	public ValidacaoPafServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public ValidacaoPafServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public ValidacaoPafServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public ValidacaoPafServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public ValidacaoPafCompletaDto ObterValidacaoPafCompletaPorIdUf(int idUf)
	{
		return base.Channel.ObterValidacaoPafCompletaPorIdUf(idUf);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IUfService")]
public interface IUfService
{
	[OperationContract(Action = "http://tempuri.org/IUfService/ObterTodasAsUfs", ReplyAction = "http://tempuri.org/IUfService/ObterTodasAsUfsResponse")]
	UfCompletoDto[] ObterTodasAsUfs();
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IUfServiceChannel : IUfService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class UfServiceClient : ClientBase<IUfService>, IUfService
{
	public UfServiceClient()
	{
	}

	public UfServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public UfServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public UfServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public UfServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public UfCompletoDto[] ObterTodasAsUfs()
	{
		return base.Channel.ObterTodasAsUfs();
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.ISerialRevendaService")]
public interface ISerialRevendaService
{
	[OperationContract(Action = "http://tempuri.org/ISerialRevendaService/ObterSerialRevendaCompletoPorNumeroSerial", ReplyAction = "http://tempuri.org/ISerialRevendaService/ObterSerialRevendaCompletoPorNumeroSerialResponse")]
	SerialRevendaCompletoDto ObterSerialRevendaCompletoPorNumeroSerial(string numeroSerial);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface ISerialRevendaServiceChannel : ISerialRevendaService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class SerialRevendaServiceClient : ClientBase<ISerialRevendaService>, ISerialRevendaService
{
	public SerialRevendaServiceClient()
	{
	}

	public SerialRevendaServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public SerialRevendaServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public SerialRevendaServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public SerialRevendaServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public SerialRevendaCompletoDto ObterSerialRevendaCompletoPorNumeroSerial(string numeroSerial)
	{
		return base.Channel.ObterSerialRevendaCompletoPorNumeroSerial(numeroSerial);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IHabilitacaoG4Service")]
public interface IHabilitacaoG4Service
{
	[OperationContract(Action = "http://tempuri.org/IHabilitacaoG4Service/ProcessarHabilitacaoPeloDocumentoDoUsuario", ReplyAction = "http://tempuri.org/IHabilitacaoG4Service/ProcessarHabilitacaoPeloDocumentoDoUsuarioResponse")]
	string ProcessarHabilitacaoPeloDocumentoDoUsuario(string documentoRevenda, string documento, string nomeUsuario, SerialSistema sistema, string versao);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoG4Service/RetornarNomeUsuarioHabilitacaoG4PorDocumento", ReplyAction = "http://tempuri.org/IHabilitacaoG4Service/RetornarNomeUsuarioHabilitacaoG4PorDocumentoResponse")]
	string RetornarNomeUsuarioHabilitacaoG4PorDocumento(string documento);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoG4Service/ObterSistemasNaoAtualizadosParaG5EG6PorCnpjRevenda", ReplyAction = "http://tempuri.org/IHabilitacaoG4Service/ObterSistemasNaoAtualizadosParaG5EG6PorCnpjRevendaResponse")]
	HabilitacaoG4ResumoDto[] ObterSistemasNaoAtualizadosParaG5EG6PorCnpjRevenda(string cnpjRevenda);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IHabilitacaoG4ServiceChannel : IHabilitacaoG4Service, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class HabilitacaoG4ServiceClient : ClientBase<IHabilitacaoG4Service>, IHabilitacaoG4Service
{
	public HabilitacaoG4ServiceClient()
	{
	}

	public HabilitacaoG4ServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public HabilitacaoG4ServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoG4ServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoG4ServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public string ProcessarHabilitacaoPeloDocumentoDoUsuario(string documentoRevenda, string documento, string nomeUsuario, SerialSistema sistema, string versao)
	{
		return base.Channel.ProcessarHabilitacaoPeloDocumentoDoUsuario(documentoRevenda, documento, nomeUsuario, sistema, versao);
	}

	public string RetornarNomeUsuarioHabilitacaoG4PorDocumento(string documento)
	{
		return base.Channel.RetornarNomeUsuarioHabilitacaoG4PorDocumento(documento);
	}

	public HabilitacaoG4ResumoDto[] ObterSistemasNaoAtualizadosParaG5EG6PorCnpjRevenda(string cnpjRevenda)
	{
		return base.Channel.ObterSistemasNaoAtualizadosParaG5EG6PorCnpjRevenda(cnpjRevenda);
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IAuditoriaService")]
public interface IAuditoriaService
{
	[OperationContract(Action = "http://tempuri.org/IAuditoriaService/SalvarAuditoria", ReplyAction = "http://tempuri.org/IAuditoriaService/SalvarAuditoriaResponse")]
	int SalvarAuditoria(AuditoriaCompletaDto auditoriaCompletaDto);

	[OperationContract(Action = "http://tempuri.org/IAuditoriaService/ObterSeriaisComRevalidacaoPendente", ReplyAction = "http://tempuri.org/IAuditoriaService/ObterSeriaisComRevalidacaoPendenteResponse")]
	HabilitacaoCompletaDto[] ObterSeriaisComRevalidacaoPendente();

	[OperationContract(Action = "http://tempuri.org/IAuditoriaService/LimparMemoria", ReplyAction = "http://tempuri.org/IAuditoriaService/LimparMemoriaResponse")]
	void LimparMemoria();
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IAuditoriaServiceChannel : IAuditoriaService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class AuditoriaServiceClient : ClientBase<IAuditoriaService>, IAuditoriaService
{
	public AuditoriaServiceClient()
	{
	}

	public AuditoriaServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public AuditoriaServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public AuditoriaServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public AuditoriaServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public int SalvarAuditoria(AuditoriaCompletaDto auditoriaCompletaDto)
	{
		return base.Channel.SalvarAuditoria(auditoriaCompletaDto);
	}

	public HabilitacaoCompletaDto[] ObterSeriaisComRevalidacaoPendente()
	{
		return base.Channel.ObterSeriaisComRevalidacaoPendente();
	}

	public void LimparMemoria()
	{
		base.Channel.LimparMemoria();
	}
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(ConfigurationName = "ServiceReferenceHabilitacao.IHabilitacaoSiteService")]
public interface IHabilitacaoSiteService
{
	[OperationContract(Action = "http://tempuri.org/IHabilitacaoSiteService/GerarSerialAdquiridoPeloSite", ReplyAction = "http://tempuri.org/IHabilitacaoSiteService/GerarSerialAdquiridoPeloSiteResponse")]
	string GerarSerialAdquiridoPeloSite(string documentoRevenda, string sistemaOrigem, string sistemaDestino, string atualizacao);

	[OperationContract(Action = "http://tempuri.org/IHabilitacaoSiteService/ObterSerialExisteESistema", ReplyAction = "http://tempuri.org/IHabilitacaoSiteService/ObterSerialExisteESistemaResponse")]
	string ObterSerialExisteESistema(string serial);
}
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IHabilitacaoSiteServiceChannel : IHabilitacaoSiteService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
{
}
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class HabilitacaoSiteServiceClient : ClientBase<IHabilitacaoSiteService>, IHabilitacaoSiteService
{
	public HabilitacaoSiteServiceClient()
	{
	}

	public HabilitacaoSiteServiceClient(string endpointConfigurationName)
		: base(endpointConfigurationName)
	{
	}

	public HabilitacaoSiteServiceClient(string endpointConfigurationName, string remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoSiteServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(endpointConfigurationName, remoteAddress)
	{
	}

	public HabilitacaoSiteServiceClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	public string GerarSerialAdquiridoPeloSite(string documentoRevenda, string sistemaOrigem, string sistemaDestino, string atualizacao)
	{
		return base.Channel.GerarSerialAdquiridoPeloSite(documentoRevenda, sistemaOrigem, sistemaDestino, atualizacao);
	}

	public string ObterSerialExisteESistema(string serial)
	{
		return base.Channel.ObterSerialExisteESistema(serial);
	}
}

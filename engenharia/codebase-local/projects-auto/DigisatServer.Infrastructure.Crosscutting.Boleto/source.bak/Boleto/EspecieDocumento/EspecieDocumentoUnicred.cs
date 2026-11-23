using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000F6 RID: 246
	public class EspecieDocumentoUnicred : AbstractEspecieDocumento
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoUnicred()
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001534C File Offset: 0x0001354C
		public EspecieDocumentoUnicred(string codigo)
		{
			try
			{
				this.Carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015388 File Offset: 0x00013588
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoUnicred.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoUnicred.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoUnicred.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoUnicred.CobrancaSeriada:
				return "4";
			case EnumEspecieDocumentoUnicred.Recibo:
				return "5";
			case EnumEspecieDocumentoUnicred.LetraCambio:
				return "6";
			case EnumEspecieDocumentoUnicred.NotaDebito:
				return "7";
			case EnumEspecieDocumentoUnicred.DuplicataServico:
				return "8";
			case EnumEspecieDocumentoUnicred.Outros:
				return "9";
			default:
				return "9";
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000153FC File Offset: 0x000135FC
		public EnumEspecieDocumentoUnicred GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 856466825U)
			{
				if (num <= 822911587U)
				{
					if (num != 806133968U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoUnicred.CobrancaSeriada;
							}
						}
					}
					else if (codigo == "5")
					{
						return EnumEspecieDocumentoUnicred.Recibo;
					}
				}
				else if (num != 839689206U)
				{
					if (num == 856466825U)
					{
						if (codigo == "6")
						{
							return EnumEspecieDocumentoUnicred.LetraCambio;
						}
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoUnicred.NotaDebito;
				}
			}
			else if (num <= 906799682U)
			{
				if (num != 873244444U)
				{
					if (num == 906799682U)
					{
						if (codigo == "3")
						{
							return EnumEspecieDocumentoUnicred.NotaSeguro;
						}
					}
				}
				else if (codigo == "1")
				{
					return EnumEspecieDocumentoUnicred.DuplicataMercantil;
				}
			}
			else if (num != 923577301U)
			{
				if (num != 1007465396U)
				{
					if (num == 1024243015U)
					{
						if (codigo == "8")
						{
							return EnumEspecieDocumentoUnicred.DuplicataServico;
						}
					}
				}
				else if (codigo == "9")
				{
					return EnumEspecieDocumentoUnicred.Outros;
				}
			}
			else if (codigo == "2")
			{
				return EnumEspecieDocumentoUnicred.NotaPromissoria;
			}
			return EnumEspecieDocumentoUnicred.Outros;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00015530 File Offset: 0x00013730
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoUnicred();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoUnicred.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoUnicred.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoUnicred.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.NotaSeguro);
					this.Especie = "Nota de seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoUnicred.CobrancaSeriada:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.CobrancaSeriada);
					this.Especie = "Cobrança seriada";
					this.Sigla = "CS";
					break;
				case EnumEspecieDocumentoUnicred.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "REC";
					break;
				case EnumEspecieDocumentoUnicred.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.LetraCambio);
					this.Sigla = "LC";
					this.Especie = "Letra de câmbio";
					break;
				case EnumEspecieDocumentoUnicred.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.NotaDebito);
					this.Sigla = "ND";
					this.Especie = "Nota de débito";
					break;
				case EnumEspecieDocumentoUnicred.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.DuplicataServico);
					this.Sigla = "DS";
					this.Especie = "Duplicata de serviço";
					break;
				case EnumEspecieDocumentoUnicred.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.Outros);
					this.Especie = "Outros";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoUnicred.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

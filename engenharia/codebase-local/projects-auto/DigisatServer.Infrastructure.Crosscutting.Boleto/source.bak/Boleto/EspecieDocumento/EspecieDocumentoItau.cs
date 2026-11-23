using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000E0 RID: 224
	public class EspecieDocumentoItau : AbstractEspecieDocumento
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoItau()
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010E4C File Offset: 0x0000F04C
		public EspecieDocumentoItau(string codigo)
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

		// Token: 0x06000386 RID: 902 RVA: 0x00010E88 File Offset: 0x0000F088
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoItau especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoItau.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoItau.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoItau.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoItau.MensalidadeEscolar:
				return "4";
			case EnumEspecieDocumentoItau.Recibo:
				return "5";
			case EnumEspecieDocumentoItau.Contrato:
				return "6";
			case EnumEspecieDocumentoItau.Cosseguros:
				return "7";
			case EnumEspecieDocumentoItau.DuplicataServico:
				return "8";
			case EnumEspecieDocumentoItau.LetraCambio:
				return "9";
			case EnumEspecieDocumentoItau.NotaDebito:
				return "13";
			case EnumEspecieDocumentoItau.DocumentoDivida:
				return "15";
			case EnumEspecieDocumentoItau.EncargosCondominais:
				return "16";
			case EnumEspecieDocumentoItau.Diversos:
				return "99";
			default:
				return "1";
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00010F24 File Offset: 0x0000F124
		public EnumEspecieDocumentoItau GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 822911587U)
			{
				if (num <= 434841374U)
				{
					if (num != 352233207U)
					{
						if (num != 418063755U)
						{
							if (num == 434841374U)
							{
								if (codigo == "16")
								{
									return EnumEspecieDocumentoItau.EncargosCondominais;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoItau.DocumentoDivida;
						}
					}
					else if (codigo == "99")
					{
						return EnumEspecieDocumentoItau.Diversos;
					}
				}
				else if (num != 518729469U)
				{
					if (num != 806133968U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoItau.MensalidadeEscolar;
							}
						}
					}
					else if (codigo == "5")
					{
						return EnumEspecieDocumentoItau.Recibo;
					}
				}
				else if (codigo == "13")
				{
					return EnumEspecieDocumentoItau.NotaDebito;
				}
			}
			else if (num <= 873244444U)
			{
				if (num != 839689206U)
				{
					if (num != 856466825U)
					{
						if (num == 873244444U)
						{
							if (codigo == "1")
							{
								return EnumEspecieDocumentoItau.DuplicataMercantil;
							}
						}
					}
					else if (codigo == "6")
					{
						return EnumEspecieDocumentoItau.Contrato;
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoItau.Cosseguros;
				}
			}
			else if (num <= 923577301U)
			{
				if (num != 906799682U)
				{
					if (num == 923577301U)
					{
						if (codigo == "2")
						{
							return EnumEspecieDocumentoItau.NotaPromissoria;
						}
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoItau.NotaSeguro;
				}
			}
			else if (num != 1007465396U)
			{
				if (num == 1024243015U)
				{
					if (codigo == "8")
					{
						return EnumEspecieDocumentoItau.DuplicataServico;
					}
				}
			}
			else if (codigo == "9")
			{
				return EnumEspecieDocumentoItau.LetraCambio;
			}
			return EnumEspecieDocumentoItau.DuplicataMercantil;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000110FC File Offset: 0x0000F2FC
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoItau();
				EspecieDocumentoItau especieDocumentoItau = new EspecieDocumentoItau();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoItau.DuplicataMercantil:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoItau.NotaPromissoria:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoItau.NotaSeguro:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.NotaSeguro);
					this.Especie = "Nota de seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoItau.MensalidadeEscolar:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.MensalidadeEscolar);
					this.Especie = "Mensalidade escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoItau.Recibo:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoItau.Contrato:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.Contrato);
					this.Sigla = "C";
					this.Especie = "Contrato";
					break;
				case EnumEspecieDocumentoItau.Cosseguros:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.Cosseguros);
					this.Sigla = "CS";
					this.Especie = "Cosseguros";
					break;
				case EnumEspecieDocumentoItau.DuplicataServico:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.DuplicataServico);
					this.Sigla = "DS";
					this.Especie = "Duplicata de serviço";
					break;
				case EnumEspecieDocumentoItau.LetraCambio:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.LetraCambio);
					this.Sigla = "LC";
					this.Especie = "Letra de câmbio";
					break;
				case EnumEspecieDocumentoItau.NotaDebito:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.NotaDebito);
					this.Sigla = "ND";
					this.Especie = "Nota de débito";
					break;
				case EnumEspecieDocumentoItau.DocumentoDivida:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.DocumentoDivida);
					this.Sigla = "DD";
					this.Especie = "Documento de dívida";
					break;
				case EnumEspecieDocumentoItau.EncargosCondominais:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.EncargosCondominais);
					this.Sigla = "EC";
					this.Especie = "Encargos condominais";
					break;
				case EnumEspecieDocumentoItau.Diversos:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.Diversos);
					this.Especie = "Diversos";
					break;
				default:
					this.Codigo = especieDocumentoItau.GetCodigoEspecieByEnum(EnumEspecieDocumentoItau.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
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

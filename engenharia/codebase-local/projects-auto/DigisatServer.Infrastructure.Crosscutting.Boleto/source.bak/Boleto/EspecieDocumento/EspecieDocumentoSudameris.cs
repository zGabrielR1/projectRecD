using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000F2 RID: 242
	public class EspecieDocumentoSudameris : AbstractEspecieDocumento
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoSudameris()
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014748 File Offset: 0x00012948
		public EspecieDocumentoSudameris(string codigo)
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

		// Token: 0x060003B1 RID: 945 RVA: 0x00014784 File Offset: 0x00012984
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoSudameris.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoSudameris.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoSudameris.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoSudameris.MensalidadeEscolar:
				return "4";
			case EnumEspecieDocumentoSudameris.Recibo:
				return "5";
			case EnumEspecieDocumentoSudameris.Contrato:
				return "6";
			case EnumEspecieDocumentoSudameris.Cosseguros:
				return "7";
			case EnumEspecieDocumentoSudameris.DuplicataServico:
				return "8";
			case EnumEspecieDocumentoSudameris.LetraCambio:
				return "9";
			case EnumEspecieDocumentoSudameris.NotaDebito:
				return "13";
			case EnumEspecieDocumentoSudameris.DocumentoDivida:
				return "15";
			case EnumEspecieDocumentoSudameris.EncargosCondominais:
				return "16";
			case EnumEspecieDocumentoSudameris.Diversos:
				return "99";
			default:
				return "99";
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014820 File Offset: 0x00012A20
		public EnumEspecieDocumentoSudameris GetEnumEspecieByCodigo(string codigo)
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
									return EnumEspecieDocumentoSudameris.EncargosCondominais;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoSudameris.DocumentoDivida;
						}
					}
					else if (codigo == "99")
					{
						return EnumEspecieDocumentoSudameris.Diversos;
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
								return EnumEspecieDocumentoSudameris.MensalidadeEscolar;
							}
						}
					}
					else if (codigo == "5")
					{
						return EnumEspecieDocumentoSudameris.Recibo;
					}
				}
				else if (codigo == "13")
				{
					return EnumEspecieDocumentoSudameris.NotaDebito;
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
								return EnumEspecieDocumentoSudameris.DuplicataMercantil;
							}
						}
					}
					else if (codigo == "6")
					{
						return EnumEspecieDocumentoSudameris.Contrato;
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoSudameris.Cosseguros;
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
							return EnumEspecieDocumentoSudameris.NotaPromissoria;
						}
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoSudameris.NotaSeguro;
				}
			}
			else if (num != 1007465396U)
			{
				if (num == 1024243015U)
				{
					if (codigo == "8")
					{
						return EnumEspecieDocumentoSudameris.DuplicataServico;
					}
				}
			}
			else if (codigo == "9")
			{
				return EnumEspecieDocumentoSudameris.LetraCambio;
			}
			return EnumEspecieDocumentoSudameris.Diversos;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000149F8 File Offset: 0x00012BF8
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoSudameris();
				EspecieDocumentoSudameris especieDocumentoSudameris = new EspecieDocumentoSudameris();
				switch (especieDocumentoSudameris.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoSudameris.DuplicataMercantil:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoSudameris.NotaPromissoria:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoSudameris.NotaSeguro:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.NotaSeguro);
					this.Especie = "Nota de seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoSudameris.MensalidadeEscolar:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.MensalidadeEscolar);
					this.Especie = "Mensalidade escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoSudameris.Recibo:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "R";
					break;
				case EnumEspecieDocumentoSudameris.Contrato:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.Contrato);
					this.Especie = "Contrato";
					this.Sigla = "C";
					break;
				case EnumEspecieDocumentoSudameris.Cosseguros:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.Cosseguros);
					this.Especie = "Cosseguros";
					this.Sigla = "CS";
					break;
				case EnumEspecieDocumentoSudameris.DuplicataServico:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.DuplicataServico);
					this.Especie = "Duplicata de serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoSudameris.LetraCambio:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.LetraCambio);
					this.Especie = "Letra de câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoSudameris.NotaDebito:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.NotaDebito);
					this.Especie = "Nota de débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoSudameris.DocumentoDivida:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.DocumentoDivida);
					this.Especie = "Documento de dívida";
					this.Sigla = "DD";
					break;
				case EnumEspecieDocumentoSudameris.EncargosCondominais:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.EncargosCondominais);
					this.Especie = "Encargos condominais";
					this.Sigla = "EC";
					break;
				case EnumEspecieDocumentoSudameris.Diversos:
					this.Codigo = especieDocumentoSudameris.GetCodigoEspecieByEnum(EnumEspecieDocumentoSudameris.Diversos);
					this.Especie = "Diversos";
					this.Sigla = "D";
					break;
				default:
					this.Codigo = "0";
					this.Especie = "( Selecione )";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00014CA4 File Offset: 0x00012EA4
		public static EspeciesDocumento CarregaTodas()
		{
			EspeciesDocumento especiesDocumento = new EspeciesDocumento();
			EspecieDocumentoSudameris especieDocumentoSudameris = new EspecieDocumentoSudameris();
			foreach (object obj in Enum.GetValues(typeof(EnumEspecieDocumentoSudameris)))
			{
				EnumEspecieDocumentoSudameris enumEspecieDocumentoSudameris = (EnumEspecieDocumentoSudameris)obj;
				especiesDocumento.Add(new EspecieDocumentoSudameris(especieDocumentoSudameris.GetCodigoEspecieByEnum(enumEspecieDocumentoSudameris)));
			}
			return especiesDocumento;
		}
	}
}

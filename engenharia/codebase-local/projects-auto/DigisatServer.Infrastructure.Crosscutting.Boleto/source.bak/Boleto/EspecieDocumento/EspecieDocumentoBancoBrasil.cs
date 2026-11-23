using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000D0 RID: 208
	public class EspecieDocumentoBancoBrasil : AbstractEspecieDocumento
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		public EspecieDocumentoBancoBrasil(string codigo, TipoArquivo tipoArquivo)
		{
			try
			{
				this.Carregar(codigo, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000E728 File Offset: 0x0000C928
		private void Carregar(string idCodigo, TipoArquivo tipoArquivo)
		{
			try
			{
				TipoArquivo tipoArquivo2 = (((idCodigo.Length == 3 && idCodigo.StartsWith("4")) || tipoArquivo == TipoArquivo.CNAB400) ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240);
				this.Banco = new BancoBrasil();
				switch (this.GetEnumEspecieByCodigo(idCodigo, tipoArquivo2))
				{
				case EnumEspecieDocumentoBancoBrasil.Cheque:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.Cheque, tipoArquivo2);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoBancoBrasil.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DuplicataMercantil, tipoArquivo2);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoBancoBrasil.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DuplicataServico, tipoArquivo2);
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoBancoBrasil.DuplicataRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DuplicataRural, tipoArquivo2);
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoBancoBrasil.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.LetraCambio, tipoArquivo2);
					this.Especie = "Letra de câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoBancoBrasil.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.NotaPromissoria, tipoArquivo2);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoBancoBrasil.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.NotaSeguro, tipoArquivo2);
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoBancoBrasil.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.Recibo, tipoArquivo2);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoBancoBrasil.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.NotaDebito, tipoArquivo2);
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoBancoBrasil.ApoliceSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.ApoliceSeguro, tipoArquivo2);
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoBancoBrasil.Warrant:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.Warrant, tipoArquivo2);
					this.Especie = "Warrant";
					break;
				case EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado, tipoArquivo2);
					this.Especie = "Dívida Ativa de Estado";
					break;
				case EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio, tipoArquivo2);
					this.Especie = "Divida Ativa de Município";
					break;
				case EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao, tipoArquivo2);
					this.Especie = "Divida Ativa União";
					break;
				case EnumEspecieDocumentoBancoBrasil.BoletoCartaoCredito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.BoletoCartaoCredito, tipoArquivo2);
					this.Especie = "Boleto Cartão de Crédito";
					break;
				case EnumEspecieDocumentoBancoBrasil.BoletoProposta:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.BoletoProposta, tipoArquivo2);
					this.Especie = "BoletoProposta";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil.DuplicataMercantil, tipoArquivo2);
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

		// Token: 0x0600035D RID: 861 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBancoBrasil especie, TipoArquivo tipoArquivo)
		{
			if (tipoArquivo != TipoArquivo.CNAB240)
			{
				return EspecieDocumentoBancoBrasil.GetCodigoEspecieCnab400(especie);
			}
			return this.GetCodigoEspecieCnab240(especie);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public string GetCodigoEspecieCnab240(EnumEspecieDocumentoBancoBrasil especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBancoBrasil.Cheque:
				return "1";
			case EnumEspecieDocumentoBancoBrasil.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoBancoBrasil.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoBancoBrasil.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoBancoBrasil.LetraCambio:
				return "7";
			case EnumEspecieDocumentoBancoBrasil.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoBancoBrasil.NotaSeguro:
				return "16";
			case EnumEspecieDocumentoBancoBrasil.Recibo:
				return "17";
			case EnumEspecieDocumentoBancoBrasil.NotaDebito:
				return "19";
			case EnumEspecieDocumentoBancoBrasil.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoBancoBrasil.Warrant:
				return "26";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado:
				return "27";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio:
				return "28";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao:
				return "29";
			default:
				return "2";
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000EB08 File Offset: 0x0000CD08
		private static string GetCodigoEspecieCnab400(EnumEspecieDocumentoBancoBrasil especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBancoBrasil.Cheque:
				return "10";
			case EnumEspecieDocumentoBancoBrasil.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoBancoBrasil.DuplicataServico:
				return "12";
			case EnumEspecieDocumentoBancoBrasil.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoBancoBrasil.LetraCambio:
				return "8";
			case EnumEspecieDocumentoBancoBrasil.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoBancoBrasil.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoBancoBrasil.Recibo:
				return "5";
			case EnumEspecieDocumentoBancoBrasil.NotaDebito:
				return "13";
			case EnumEspecieDocumentoBancoBrasil.ApoliceSeguro:
				return "15";
			case EnumEspecieDocumentoBancoBrasil.Warrant:
				return "9";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado:
				return "26";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio:
				return "27";
			case EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao:
				return "25";
			case EnumEspecieDocumentoBancoBrasil.BoletoCartaoCredito:
				return "31";
			case EnumEspecieDocumentoBancoBrasil.BoletoProposta:
				return "32";
			default:
				return "1";
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
		public EnumEspecieDocumentoBancoBrasil GetEnumEspecieByCodigo(string codigo, TipoArquivo tipoArquivo)
		{
			if (tipoArquivo != TipoArquivo.CNAB400)
			{
				return this.GetEnumEspecieCnab240(codigo);
			}
			return EspecieDocumentoBancoBrasil.GetEnumEspecieCnab400(codigo);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		public EnumEspecieDocumentoBancoBrasil GetEnumEspecieCnab240(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 856466825U)
			{
				if (num <= 451618993U)
				{
					if (num != 350953279U)
					{
						if (num != 434841374U)
						{
							if (num == 451618993U)
							{
								if (codigo == "17")
								{
									return EnumEspecieDocumentoBancoBrasil.Recibo;
								}
							}
						}
						else if (codigo == "16")
						{
							return EnumEspecieDocumentoBancoBrasil.NotaSeguro;
						}
					}
					else if (codigo == "19")
					{
						return EnumEspecieDocumentoBancoBrasil.NotaDebito;
					}
				}
				else if (num <= 822911587U)
				{
					if (num != 501951850U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataServico;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoBancoBrasil.NotaPromissoria;
					}
				}
				else if (num != 839689206U)
				{
					if (num == 856466825U)
					{
						if (codigo == "6")
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataRural;
						}
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoBancoBrasil.LetraCambio;
				}
			}
			else if (num <= 2331153606U)
			{
				if (num != 873244444U)
				{
					if (num != 923577301U)
					{
						if (num == 2331153606U)
						{
							if (codigo == "27")
							{
								return EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado;
							}
						}
					}
					else if (codigo == "2")
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
				}
				else if (codigo == "1")
				{
					return EnumEspecieDocumentoBancoBrasil.Cheque;
				}
			}
			else if (num <= 2381486463U)
			{
				if (num != 2347931225U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return EnumEspecieDocumentoBancoBrasil.ApoliceSeguro;
						}
					}
				}
				else if (codigo == "26")
				{
					return EnumEspecieDocumentoBancoBrasil.Warrant;
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio;
					}
				}
			}
			else if (codigo == "29")
			{
				return EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao;
			}
			return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		private static EnumEspecieDocumentoBancoBrasil GetEnumEspecieCnab400(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2297598368U)
			{
				if (num <= 856466825U)
				{
					if (num <= 468396612U)
					{
						if (num <= 422078675U)
						{
							if (num != 418063755U)
							{
								if (num != 422078675U)
								{
									return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
								}
								if (!(codigo == "431"))
								{
									return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
								}
							}
							else
							{
								if (!(codigo == "15"))
								{
									return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
								}
								return EnumEspecieDocumentoBancoBrasil.ApoliceSeguro;
							}
						}
						else if (num != 438856294U)
						{
							if (num != 468396612U)
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							if (!(codigo == "10"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.Cheque;
						}
						else
						{
							if (!(codigo == "432"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.BoletoProposta;
						}
					}
					else if (num <= 518729469U)
					{
						if (num != 501951850U)
						{
							if (num != 518729469U)
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							if (!(codigo == "13"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.NotaDebito;
						}
						else
						{
							if (!(codigo == "12"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.DuplicataServico;
						}
					}
					else if (num != 806133968U)
					{
						if (num != 856466825U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "6"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.DuplicataRural;
					}
					else
					{
						if (!(codigo == "5"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.Recibo;
					}
				}
				else if (num <= 1007465396U)
				{
					if (num <= 906799682U)
					{
						if (num != 873244444U)
						{
							if (num != 906799682U)
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							if (!(codigo == "3"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.NotaSeguro;
						}
						else
						{
							if (!(codigo == "1"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
					}
					else if (num != 923577301U)
					{
						if (num != 1007465396U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "9"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.Warrant;
					}
					else
					{
						if (!(codigo == "2"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.NotaPromissoria;
					}
				}
				else if (num <= 2247118416U)
				{
					if (num != 1024243015U)
					{
						if (num != 2247118416U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "32"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.BoletoProposta;
					}
					else
					{
						if (!(codigo == "8"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.LetraCambio;
					}
				}
				else if (num != 2297451273U)
				{
					if (num != 2297598368U)
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					if (!(codigo == "25"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao;
				}
				else if (!(codigo == "31"))
				{
					return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
				}
				return EnumEspecieDocumentoBancoBrasil.BoletoCartaoCredito;
			}
			if (num <= 2720906668U)
			{
				if (num <= 2687204335U)
				{
					if (num <= 2347931225U)
					{
						if (num != 2331153606U)
						{
							if (num != 2347931225U)
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							if (!(codigo == "26"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
						}
						else
						{
							if (!(codigo == "27"))
							{
								return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
							}
							return EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio;
						}
					}
					else if (num != 2670426716U)
					{
						if (num != 2687204335U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "426"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
					}
					else
					{
						if (!(codigo == "427"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.DividaAtivaMunicipio;
					}
					return EnumEspecieDocumentoBancoBrasil.DividaAtivaEstado;
				}
				if (num <= 2704129049U)
				{
					if (num != 2703981954U)
					{
						if (num != 2704129049U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "415"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.ApoliceSeguro;
					}
					else
					{
						if (!(codigo == "425"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.DividaAtivaUniao;
					}
				}
				else if (num != 2704276144U)
				{
					if (num != 2720906668U)
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					if (!(codigo == "412"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.DuplicataServico;
				}
				else
				{
					if (!(codigo == "409"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.Warrant;
				}
			}
			else if (num <= 2838497096U)
			{
				if (num <= 2737684287U)
				{
					if (num != 2721053763U)
					{
						if (num != 2737684287U)
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						if (!(codigo == "413"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.NotaDebito;
					}
					else
					{
						if (!(codigo == "408"))
						{
							return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
						}
						return EnumEspecieDocumentoBancoBrasil.LetraCambio;
					}
				}
				else if (num != 2754461906U)
				{
					if (num != 2838497096U)
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					if (!(codigo == "401"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
				}
				else
				{
					if (!(codigo == "410"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.Cheque;
				}
			}
			else if (num <= 2888829953U)
			{
				if (num != 2872052334U)
				{
					if (num != 2888829953U)
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					if (!(codigo == "402"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.NotaPromissoria;
				}
				else
				{
					if (!(codigo == "403"))
					{
						return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
					}
					return EnumEspecieDocumentoBancoBrasil.NotaSeguro;
				}
			}
			else if (num != 2905607572U)
			{
				if (num != 2955940429U)
				{
					return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
				}
				if (!(codigo == "406"))
				{
					return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
				}
				return EnumEspecieDocumentoBancoBrasil.DuplicataRural;
			}
			else
			{
				if (!(codigo == "405"))
				{
					return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
				}
				return EnumEspecieDocumentoBancoBrasil.Recibo;
			}
			return EnumEspecieDocumentoBancoBrasil.DuplicataMercantil;
		}
	}
}

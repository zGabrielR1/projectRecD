using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000E2 RID: 226
	public class EspecieDocumentoReal : AbstractEspecieDocumento
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoReal()
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public EspecieDocumentoReal(string codigo)
		{
			try
			{
				this.carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public string getCodigoEspecieByEnum(EnumEspecieDocumentoReal especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoReal.Cheque:
				return "1";
			case EnumEspecieDocumentoReal.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoReal.DuplicataMercantilIndicacao:
				return "3";
			case EnumEspecieDocumentoReal.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoReal.DuplicataServiçoIndicacao:
				return "5";
			case EnumEspecieDocumentoReal.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoReal.LetraCambio:
				return "7";
			case EnumEspecieDocumentoReal.NotaCreditoComercial:
				return "8";
			case EnumEspecieDocumentoReal.NotaCreditoExportacao:
				return "9";
			case EnumEspecieDocumentoReal.NotaCreditoIndustrial:
				return "10";
			case EnumEspecieDocumentoReal.NotaCreditoRural:
				return "11";
			case EnumEspecieDocumentoReal.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoReal.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoReal.TriplicataMercantil:
				return "14";
			case EnumEspecieDocumentoReal.TriplicataServico:
				return "15";
			case EnumEspecieDocumentoReal.NotaSeguro:
				return "16";
			case EnumEspecieDocumentoReal.Recibo:
				return "17";
			case EnumEspecieDocumentoReal.Fatura:
				return "18";
			case EnumEspecieDocumentoReal.NotaDebito:
				return "19";
			case EnumEspecieDocumentoReal.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoReal.MensalidadeEscolar:
				return "21";
			case EnumEspecieDocumentoReal.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoReal.IdentificacaoTituloAceitoNaoAceito:
				return "23";
			default:
				return "2";
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public EnumEspecieDocumentoReal getEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 806133968U)
			{
				if (num <= 434841374U)
				{
					if (num <= 350953279U)
					{
						if (num != 334175660U)
						{
							if (num == 350953279U)
							{
								if (codigo == "19")
								{
									return EnumEspecieDocumentoReal.NotaDebito;
								}
							}
						}
						else if (codigo == "18")
						{
							return EnumEspecieDocumentoReal.Fatura;
						}
					}
					else if (num != 401286136U)
					{
						if (num != 418063755U)
						{
							if (num == 434841374U)
							{
								if (codigo == "16")
								{
									return EnumEspecieDocumentoReal.NotaSeguro;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoReal.TriplicataServico;
						}
					}
					else if (codigo == "14")
					{
						return EnumEspecieDocumentoReal.TriplicataMercantil;
					}
				}
				else if (num <= 485174231U)
				{
					if (num != 451618993U)
					{
						if (num != 468396612U)
						{
							if (num == 485174231U)
							{
								if (codigo == "11")
								{
									return EnumEspecieDocumentoReal.NotaCreditoRural;
								}
							}
						}
						else if (codigo == "10")
						{
							return EnumEspecieDocumentoReal.NotaCreditoIndustrial;
						}
					}
					else if (codigo == "17")
					{
						return EnumEspecieDocumentoReal.Recibo;
					}
				}
				else if (num != 501951850U)
				{
					if (num != 518729469U)
					{
						if (num == 806133968U)
						{
							if (codigo == "5")
							{
								return EnumEspecieDocumentoReal.DuplicataServiçoIndicacao;
							}
						}
					}
					else if (codigo == "13")
					{
						return EnumEspecieDocumentoReal.NotaPromissoriaRural;
					}
				}
				else if (codigo == "12")
				{
					return EnumEspecieDocumentoReal.NotaPromissoria;
				}
			}
			else if (num <= 923577301U)
			{
				if (num <= 856466825U)
				{
					if (num != 822911587U)
					{
						if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (codigo == "6")
								{
									return EnumEspecieDocumentoReal.DuplicataRural;
								}
							}
						}
						else if (codigo == "7")
						{
							return EnumEspecieDocumentoReal.LetraCambio;
						}
					}
					else if (codigo == "4")
					{
						return EnumEspecieDocumentoReal.DuplicataServico;
					}
				}
				else if (num != 873244444U)
				{
					if (num != 906799682U)
					{
						if (num == 923577301U)
						{
							if (codigo == "2")
							{
								return EnumEspecieDocumentoReal.DuplicataMercantil;
							}
						}
					}
					else if (codigo == "3")
					{
						return EnumEspecieDocumentoReal.DuplicataMercantilIndicacao;
					}
				}
				else if (codigo == "1")
				{
					return EnumEspecieDocumentoReal.Cheque;
				}
			}
			else if (num <= 2364708844U)
			{
				if (num != 1007465396U)
				{
					if (num != 1024243015U)
					{
						if (num == 2364708844U)
						{
							if (codigo == "21")
							{
								return EnumEspecieDocumentoReal.MensalidadeEscolar;
							}
						}
					}
					else if (codigo == "8")
					{
						return EnumEspecieDocumentoReal.NotaCreditoComercial;
					}
				}
				else if (codigo == "9")
				{
					return EnumEspecieDocumentoReal.NotaCreditoExportacao;
				}
			}
			else if (num != 2381486463U)
			{
				if (num != 2398264082U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return EnumEspecieDocumentoReal.ParcelaConsorcio;
						}
					}
				}
				else if (codigo == "23")
				{
					return EnumEspecieDocumentoReal.IdentificacaoTituloAceitoNaoAceito;
				}
			}
			else if (codigo == "20")
			{
				return EnumEspecieDocumentoReal.ApoliceSeguro;
			}
			return EnumEspecieDocumentoReal.DuplicataMercantil;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011858 File Offset: 0x0000FA58
		private void carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoReal();
				switch (this.getEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoReal.Cheque:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.Cheque);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoReal.DuplicataMercantil:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoReal.DuplicataMercantilIndicacao:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.DuplicataMercantilIndicacao);
					this.Especie = "Duplicata Mercantil p/ Indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoReal.DuplicataServico:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.DuplicataServico);
					this.Especie = "Duplicata Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoReal.DuplicataServiçoIndicacao:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.DuplicataServiçoIndicacao);
					this.Especie = "Duplicata Serviços p/ Indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoReal.DuplicataRural:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.DuplicataRural);
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoReal.LetraCambio:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.LetraCambio);
					this.Sigla = "LC";
					this.Especie = "Letra de câmbio";
					break;
				case EnumEspecieDocumentoReal.NotaCreditoComercial:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaCreditoComercial);
					this.Especie = "Nota de Crédito Comercial";
					this.Sigla = "NCC";
					break;
				case EnumEspecieDocumentoReal.NotaCreditoExportacao:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaCreditoExportacao);
					this.Especie = "Nota de Crédito a Exportação";
					this.Sigla = "NCE";
					break;
				case EnumEspecieDocumentoReal.NotaCreditoIndustrial:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaCreditoIndustrial);
					this.Especie = "Nota de Crédito Industrial";
					this.Sigla = "NCI";
					break;
				case EnumEspecieDocumentoReal.NotaCreditoRural:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaCreditoRural);
					this.Especie = "Nota de Crédito Rural";
					this.Sigla = "NCR";
					break;
				case EnumEspecieDocumentoReal.NotaPromissoria:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoReal.NotaPromissoriaRural:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NPR";
					break;
				case EnumEspecieDocumentoReal.TriplicataMercantil:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.TriplicataMercantil);
					this.Especie = "Triplicata Mercantil";
					this.Sigla = "TM";
					break;
				case EnumEspecieDocumentoReal.TriplicataServico:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.TriplicataServico);
					this.Especie = "Triplicata de Serviço";
					this.Sigla = "TS";
					break;
				case EnumEspecieDocumentoReal.NotaSeguro:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaSeguro);
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoReal.Recibo:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoReal.Fatura:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.Fatura);
					this.Especie = "Fatura";
					this.Sigla = "FAT";
					break;
				case EnumEspecieDocumentoReal.NotaDebito:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.NotaDebito);
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoReal.ApoliceSeguro:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.ApoliceSeguro);
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoReal.MensalidadeEscolar:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.MensalidadeEscolar);
					this.Especie = "Mensalidade Escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoReal.ParcelaConsorcio:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.ParcelaConsorcio);
					this.Especie = "Parcela de Consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoReal.IdentificacaoTituloAceitoNaoAceito:
					this.Codigo = this.getCodigoEspecieByEnum(EnumEspecieDocumentoReal.IdentificacaoTituloAceitoNaoAceito);
					this.Especie = "Identificação de Título Aceito / Não Aceito";
					this.Sigla = "C016";
					break;
				default:
					this.Codigo = "0";
					this.Especie = "";
					this.Sigla = "";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public static EspeciesDocumento CarregaTodas()
		{
			EspeciesDocumento especiesDocumento = new EspeciesDocumento();
			EspecieDocumentoReal especieDocumentoReal = new EspecieDocumentoReal();
			foreach (object obj in Enum.GetValues(typeof(EnumEspecieDocumentoReal)))
			{
				EnumEspecieDocumentoReal enumEspecieDocumentoReal = (EnumEspecieDocumentoReal)obj;
				especiesDocumento.Add(new EspecieDocumentoReal(especieDocumentoReal.getCodigoEspecieByEnum(enumEspecieDocumentoReal)));
			}
			return especiesDocumento;
		}
	}
}

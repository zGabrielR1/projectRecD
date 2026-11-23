using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000EA RID: 234
	public class EspecieDocumentoBtg : AbstractEspecieDocumento
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoBtg()
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001295C File Offset: 0x00010B5C
		public EspecieDocumentoBtg(string codigo, TipoArquivo tipoArquivo)
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

		// Token: 0x0600039F RID: 927 RVA: 0x00012998 File Offset: 0x00010B98
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBtg.Cheque:
				return "1";
			case EnumEspecieDocumentoBtg.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoBtg.DuplicataMercantilIndicacao:
				return "3";
			case EnumEspecieDocumentoBtg.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoBtg.DuplicataServicoIndicacao:
				return "5";
			case EnumEspecieDocumentoBtg.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoBtg.LetraCambio:
				return "7";
			case EnumEspecieDocumentoBtg.NotaCreditoComercial:
				return "8";
			case EnumEspecieDocumentoBtg.NotaCreditoExportacao:
				return "9";
			case EnumEspecieDocumentoBtg.NotaCreditoIndustrial:
				return "10";
			case EnumEspecieDocumentoBtg.NotaCreditoRural:
				return "11";
			case EnumEspecieDocumentoBtg.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoBtg.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoBtg.TriplicataMercantil:
				return "14";
			case EnumEspecieDocumentoBtg.TriplicataServico:
				return "15";
			case EnumEspecieDocumentoBtg.NotaSeguro:
				return "16";
			case EnumEspecieDocumentoBtg.Recibo:
				return "17";
			case EnumEspecieDocumentoBtg.Fatura:
				return "18";
			case EnumEspecieDocumentoBtg.NotaDebito:
				return "19";
			case EnumEspecieDocumentoBtg.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoBtg.MensalidadeEscolar:
				return "21";
			case EnumEspecieDocumentoBtg.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoBtg.NotaFiscal:
				return "23";
			case EnumEspecieDocumentoBtg.DocumentoDeDivida:
				return "24";
			case EnumEspecieDocumentoBtg.CedulaProdutorRural:
				return "25";
			case EnumEspecieDocumentoBtg.Outros:
				return "99";
			default:
				return "2";
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012ABC File Offset: 0x00010CBC
		public EnumEspecieDocumentoBtg GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 822911587U)
			{
				if (num <= 434841374U)
				{
					if (num <= 352233207U)
					{
						if (num != 334175660U)
						{
							if (num != 350953279U)
							{
								if (num == 352233207U)
								{
									if (codigo == "99")
									{
										return EnumEspecieDocumentoBtg.Outros;
									}
								}
							}
							else if (codigo == "19")
							{
								return EnumEspecieDocumentoBtg.NotaDebito;
							}
						}
						else if (codigo == "18")
						{
							return EnumEspecieDocumentoBtg.Fatura;
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
									return EnumEspecieDocumentoBtg.NotaSeguro;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoBtg.TriplicataServico;
						}
					}
					else if (codigo == "14")
					{
						return EnumEspecieDocumentoBtg.TriplicataMercantil;
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
									return EnumEspecieDocumentoBtg.NotaCreditoRural;
								}
							}
						}
						else if (codigo == "10")
						{
							return EnumEspecieDocumentoBtg.NotaCreditoIndustrial;
						}
					}
					else if (codigo == "17")
					{
						return EnumEspecieDocumentoBtg.Recibo;
					}
				}
				else if (num <= 518729469U)
				{
					if (num != 501951850U)
					{
						if (num == 518729469U)
						{
							if (codigo == "13")
							{
								return EnumEspecieDocumentoBtg.NotaPromissoriaRural;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoBtg.NotaPromissoria;
					}
				}
				else if (num != 806133968U)
				{
					if (num == 822911587U)
					{
						if (codigo == "4")
						{
							return EnumEspecieDocumentoBtg.DuplicataServico;
						}
					}
				}
				else if (codigo == "5")
				{
					return EnumEspecieDocumentoBtg.DuplicataServicoIndicacao;
				}
			}
			else if (num <= 1007465396U)
			{
				if (num <= 873244444U)
				{
					if (num != 839689206U)
					{
						if (num != 856466825U)
						{
							if (num == 873244444U)
							{
								if (codigo == "1")
								{
									return EnumEspecieDocumentoBtg.Cheque;
								}
							}
						}
						else if (codigo == "6")
						{
							return EnumEspecieDocumentoBtg.DuplicataRural;
						}
					}
					else if (codigo == "7")
					{
						return EnumEspecieDocumentoBtg.LetraCambio;
					}
				}
				else if (num != 906799682U)
				{
					if (num != 923577301U)
					{
						if (num == 1007465396U)
						{
							if (codigo == "9")
							{
								return EnumEspecieDocumentoBtg.NotaCreditoExportacao;
							}
						}
					}
					else if (codigo == "2")
					{
						return EnumEspecieDocumentoBtg.DuplicataMercantil;
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoBtg.DuplicataMercantilIndicacao;
				}
			}
			else if (num <= 2314375987U)
			{
				if (num != 1024243015U)
				{
					if (num != 2297598368U)
					{
						if (num == 2314375987U)
						{
							if (codigo == "24")
							{
								return EnumEspecieDocumentoBtg.CedulaProdutorRural;
							}
						}
					}
					else if (codigo == "25")
					{
						return EnumEspecieDocumentoBtg.ParcelaConsorcio;
					}
				}
				else if (codigo == "8")
				{
					return EnumEspecieDocumentoBtg.NotaCreditoComercial;
				}
			}
			else if (num <= 2381486463U)
			{
				if (num != 2364708844U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return EnumEspecieDocumentoBtg.ApoliceSeguro;
						}
					}
				}
				else if (codigo == "21")
				{
					return EnumEspecieDocumentoBtg.MensalidadeEscolar;
				}
			}
			else if (num != 2398264082U)
			{
				if (num == 2415041701U)
				{
					if (codigo == "22")
					{
						return EnumEspecieDocumentoBtg.NotaFiscal;
					}
				}
			}
			else if (codigo == "23")
			{
				return EnumEspecieDocumentoBtg.DocumentoDeDivida;
			}
			return EnumEspecieDocumentoBtg.DuplicataMercantil;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00012EB8 File Offset: 0x000110B8
		private void Carregar(string idCodigo, TipoArquivo tipoArquivo)
		{
			try
			{
				this.Banco = new BancoBtg();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoBtg.Cheque:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "10";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.Cheque);
					}
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoBtg.DuplicataMercantil:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "1";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataMercantil);
					}
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoBtg.DuplicataMercantilIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataMercantilIndicacao);
					this.Especie = "Duplicata Mercantil p/ Indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoBtg.DuplicataServico:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "12";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataServico);
					}
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoBtg.DuplicataServicoIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataServicoIndicacao);
					this.Especie = "Duplicata de Serviço p/ Indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoBtg.DuplicataRural:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "6";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataRural);
					}
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoBtg.LetraCambio:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "8";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.LetraCambio);
					}
					this.Especie = "Letra de Câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoBtg.NotaCreditoComercial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaCreditoComercial);
					this.Especie = "Nota de Crédito Comercial";
					this.Sigla = "NCC";
					break;
				case EnumEspecieDocumentoBtg.NotaCreditoExportacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaCreditoExportacao);
					this.Especie = "Nota de Crédito a Exportação";
					this.Sigla = "NCE";
					break;
				case EnumEspecieDocumentoBtg.NotaCreditoIndustrial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaCreditoIndustrial);
					this.Especie = "Nota de Crédito Industrial";
					this.Sigla = "NCI";
					break;
				case EnumEspecieDocumentoBtg.NotaCreditoRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaCreditoRural);
					this.Especie = "Nota de Crédito Rural";
					this.Sigla = "NCR";
					break;
				case EnumEspecieDocumentoBtg.NotaPromissoria:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "2";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaPromissoria);
					}
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoBtg.NotaPromissoriaRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NPR";
					break;
				case EnumEspecieDocumentoBtg.TriplicataMercantil:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "14";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.TriplicataMercantil);
					}
					this.Especie = "Triplicata Mercantil";
					this.Sigla = "TM";
					break;
				case EnumEspecieDocumentoBtg.TriplicataServico:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "15";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.TriplicataServico);
					}
					this.Especie = "Triplicata de Serviço";
					this.Sigla = "TS";
					break;
				case EnumEspecieDocumentoBtg.NotaSeguro:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "3";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaSeguro);
					}
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoBtg.Recibo:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "5";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.Recibo);
					}
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoBtg.Fatura:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "18";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.Fatura);
					}
					this.Especie = "Fatura";
					this.Sigla = "FAT";
					break;
				case EnumEspecieDocumentoBtg.NotaDebito:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "13";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaDebito);
					}
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoBtg.ApoliceSeguro:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "20";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.ApoliceSeguro);
					}
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoBtg.MensalidadeEscolar:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "21";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.MensalidadeEscolar);
					}
					this.Especie = "Mensalidade Escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoBtg.ParcelaConsorcio:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "22";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.ParcelaConsorcio);
					}
					this.Especie = "Parcela de Consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoBtg.NotaFiscal:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.NotaFiscal);
					this.Especie = "Nota Fiscal";
					this.Sigla = "NF";
					break;
				case EnumEspecieDocumentoBtg.DocumentoDeDivida:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DocumentoDeDivida);
					this.Especie = "Documento de Dívida";
					this.Sigla = "DD";
					break;
				case EnumEspecieDocumentoBtg.CedulaProdutorRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.CedulaProdutorRural);
					this.Especie = "Cédula de Produto Rural";
					this.Sigla = "CPR";
					break;
				case EnumEspecieDocumentoBtg.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.Outros);
					this.Especie = "Outros";
					this.Sigla = "OU";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBtg.DuplicataMercantil);
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

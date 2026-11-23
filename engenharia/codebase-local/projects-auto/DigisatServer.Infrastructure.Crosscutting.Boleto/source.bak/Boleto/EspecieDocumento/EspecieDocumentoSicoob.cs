using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000EC RID: 236
	public class EspecieDocumentoSicoob : AbstractEspecieDocumento
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoSicoob()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000134B0 File Offset: 0x000116B0
		public EspecieDocumentoSicoob(string codigo, TipoArquivo tipoArquivo)
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

		// Token: 0x060003A4 RID: 932 RVA: 0x000134EC File Offset: 0x000116EC
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoSicoob.Cheque:
				return "1";
			case EnumEspecieDocumentoSicoob.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoSicoob.DuplicataMercantilIndicacao:
				return "3";
			case EnumEspecieDocumentoSicoob.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoSicoob.DuplicataServicoIndicacao:
				return "5";
			case EnumEspecieDocumentoSicoob.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoSicoob.LetraCambio:
				return "7";
			case EnumEspecieDocumentoSicoob.NotaCreditoComercial:
				return "8";
			case EnumEspecieDocumentoSicoob.NotaCreditoExportacao:
				return "9";
			case EnumEspecieDocumentoSicoob.NotaCreditoIndustrial:
				return "10";
			case EnumEspecieDocumentoSicoob.NotaCreditoRural:
				return "11";
			case EnumEspecieDocumentoSicoob.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoSicoob.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoSicoob.TriplicataMercantil:
				return "14";
			case EnumEspecieDocumentoSicoob.TriplicataServico:
				return "15";
			case EnumEspecieDocumentoSicoob.NotaSeguro:
				return "16";
			case EnumEspecieDocumentoSicoob.Recibo:
				return "17";
			case EnumEspecieDocumentoSicoob.Fatura:
				return "18";
			case EnumEspecieDocumentoSicoob.NotaDebito:
				return "19";
			case EnumEspecieDocumentoSicoob.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoSicoob.MensalidadeEscolar:
				return "21";
			case EnumEspecieDocumentoSicoob.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoSicoob.NotaFiscal:
				return "23";
			case EnumEspecieDocumentoSicoob.DocumentoDeDivida:
				return "24";
			case EnumEspecieDocumentoSicoob.CedulaProdutorRural:
				return "25";
			case EnumEspecieDocumentoSicoob.Outros:
				return "99";
			default:
				return "2";
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00013610 File Offset: 0x00011810
		public EnumEspecieDocumentoSicoob GetEnumEspecieByCodigo(string codigo)
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
										return EnumEspecieDocumentoSicoob.Outros;
									}
								}
							}
							else if (codigo == "19")
							{
								return EnumEspecieDocumentoSicoob.NotaDebito;
							}
						}
						else if (codigo == "18")
						{
							return EnumEspecieDocumentoSicoob.Fatura;
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
									return EnumEspecieDocumentoSicoob.NotaSeguro;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoSicoob.TriplicataServico;
						}
					}
					else if (codigo == "14")
					{
						return EnumEspecieDocumentoSicoob.TriplicataMercantil;
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
									return EnumEspecieDocumentoSicoob.NotaCreditoRural;
								}
							}
						}
						else if (codigo == "10")
						{
							return EnumEspecieDocumentoSicoob.NotaCreditoIndustrial;
						}
					}
					else if (codigo == "17")
					{
						return EnumEspecieDocumentoSicoob.Recibo;
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
								return EnumEspecieDocumentoSicoob.NotaPromissoriaRural;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoSicoob.NotaPromissoria;
					}
				}
				else if (num != 806133968U)
				{
					if (num == 822911587U)
					{
						if (codigo == "4")
						{
							return EnumEspecieDocumentoSicoob.DuplicataServico;
						}
					}
				}
				else if (codigo == "5")
				{
					return EnumEspecieDocumentoSicoob.DuplicataServicoIndicacao;
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
									return EnumEspecieDocumentoSicoob.Cheque;
								}
							}
						}
						else if (codigo == "6")
						{
							return EnumEspecieDocumentoSicoob.DuplicataRural;
						}
					}
					else if (codigo == "7")
					{
						return EnumEspecieDocumentoSicoob.LetraCambio;
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
								return EnumEspecieDocumentoSicoob.NotaCreditoExportacao;
							}
						}
					}
					else if (codigo == "2")
					{
						return EnumEspecieDocumentoSicoob.DuplicataMercantil;
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoSicoob.DuplicataMercantilIndicacao;
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
								return EnumEspecieDocumentoSicoob.CedulaProdutorRural;
							}
						}
					}
					else if (codigo == "25")
					{
						return EnumEspecieDocumentoSicoob.ParcelaConsorcio;
					}
				}
				else if (codigo == "8")
				{
					return EnumEspecieDocumentoSicoob.NotaCreditoComercial;
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
							return EnumEspecieDocumentoSicoob.ApoliceSeguro;
						}
					}
				}
				else if (codigo == "21")
				{
					return EnumEspecieDocumentoSicoob.MensalidadeEscolar;
				}
			}
			else if (num != 2398264082U)
			{
				if (num == 2415041701U)
				{
					if (codigo == "22")
					{
						return EnumEspecieDocumentoSicoob.NotaFiscal;
					}
				}
			}
			else if (codigo == "23")
			{
				return EnumEspecieDocumentoSicoob.DocumentoDeDivida;
			}
			return EnumEspecieDocumentoSicoob.DuplicataMercantil;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00013A0C File Offset: 0x00011C0C
		private void Carregar(string idCodigo, TipoArquivo tipoArquivo)
		{
			try
			{
				this.Banco = new BancoSicoob();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoSicoob.Cheque:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "10";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.Cheque);
					}
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoSicoob.DuplicataMercantil:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "1";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataMercantil);
					}
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoSicoob.DuplicataMercantilIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataMercantilIndicacao);
					this.Especie = "Duplicata Mercantil p/ Indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoSicoob.DuplicataServico:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "12";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataServico);
					}
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoSicoob.DuplicataServicoIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataServicoIndicacao);
					this.Especie = "Duplicata de Serviço p/ Indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoSicoob.DuplicataRural:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "6";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataRural);
					}
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoSicoob.LetraCambio:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "8";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.LetraCambio);
					}
					this.Especie = "Letra de Câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoSicoob.NotaCreditoComercial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaCreditoComercial);
					this.Especie = "Nota de Crédito Comercial";
					this.Sigla = "NCC";
					break;
				case EnumEspecieDocumentoSicoob.NotaCreditoExportacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaCreditoExportacao);
					this.Especie = "Nota de Crédito a Exportação";
					this.Sigla = "NCE";
					break;
				case EnumEspecieDocumentoSicoob.NotaCreditoIndustrial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaCreditoIndustrial);
					this.Especie = "Nota de Crédito Industrial";
					this.Sigla = "NCI";
					break;
				case EnumEspecieDocumentoSicoob.NotaCreditoRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaCreditoRural);
					this.Especie = "Nota de Crédito Rural";
					this.Sigla = "NCR";
					break;
				case EnumEspecieDocumentoSicoob.NotaPromissoria:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "2";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaPromissoria);
					}
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoSicoob.NotaPromissoriaRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NPR";
					break;
				case EnumEspecieDocumentoSicoob.TriplicataMercantil:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "14";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.TriplicataMercantil);
					}
					this.Especie = "Triplicata Mercantil";
					this.Sigla = "TM";
					break;
				case EnumEspecieDocumentoSicoob.TriplicataServico:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "15";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.TriplicataServico);
					}
					this.Especie = "Triplicata de Serviço";
					this.Sigla = "TS";
					break;
				case EnumEspecieDocumentoSicoob.NotaSeguro:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "3";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaSeguro);
					}
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoSicoob.Recibo:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "5";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.Recibo);
					}
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoSicoob.Fatura:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "18";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.Fatura);
					}
					this.Especie = "Fatura";
					this.Sigla = "FAT";
					break;
				case EnumEspecieDocumentoSicoob.NotaDebito:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "13";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaDebito);
					}
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoSicoob.ApoliceSeguro:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "20";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.ApoliceSeguro);
					}
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoSicoob.MensalidadeEscolar:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "21";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.MensalidadeEscolar);
					}
					this.Especie = "Mensalidade Escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoSicoob.ParcelaConsorcio:
					if (tipoArquivo == TipoArquivo.CNAB400)
					{
						this.Codigo = "22";
					}
					else
					{
						this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.ParcelaConsorcio);
					}
					this.Especie = "Parcela de Consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoSicoob.NotaFiscal:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.NotaFiscal);
					this.Especie = "Nota Fiscal";
					this.Sigla = "NF";
					break;
				case EnumEspecieDocumentoSicoob.DocumentoDeDivida:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DocumentoDeDivida);
					this.Especie = "Documento de Dívida";
					this.Sigla = "DD";
					break;
				case EnumEspecieDocumentoSicoob.CedulaProdutorRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.CedulaProdutorRural);
					this.Especie = "Cédula de Produto Rural";
					this.Sigla = "CPR";
					break;
				case EnumEspecieDocumentoSicoob.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.Outros);
					this.Especie = "Outros";
					this.Sigla = "OU";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicoob.DuplicataMercantil);
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

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000DA RID: 218
	public class EspecieDocumentoCaixa : AbstractEspecieDocumento
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000FF70 File Offset: 0x0000E170
		public EspecieDocumentoCaixa(string codigo)
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

		// Token: 0x06000378 RID: 888 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoCaixa.Cheque:
				return "1";
			case EnumEspecieDocumentoCaixa.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoCaixa.DuplicataMercantilIndicacao:
				return "3";
			case EnumEspecieDocumentoCaixa.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoCaixa.DuplicataServicoIndicacao:
				return "5";
			case EnumEspecieDocumentoCaixa.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoCaixa.LetraCambio:
				return "7";
			case EnumEspecieDocumentoCaixa.NotaCreditoComercial:
				return "8";
			case EnumEspecieDocumentoCaixa.NotaCreditoExportacao:
				return "9";
			case EnumEspecieDocumentoCaixa.NotaCreditoIndustrial:
				return "10";
			case EnumEspecieDocumentoCaixa.NotaCreditoRural:
				return "11";
			case EnumEspecieDocumentoCaixa.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoCaixa.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoCaixa.TriplicataMercantil:
				return "14";
			case EnumEspecieDocumentoCaixa.TriplicataServico:
				return "15";
			case EnumEspecieDocumentoCaixa.NotaSeguro:
				return "16";
			case EnumEspecieDocumentoCaixa.Recibo:
				return "17";
			case EnumEspecieDocumentoCaixa.Fatura:
				return "18";
			case EnumEspecieDocumentoCaixa.NotaDebito:
				return "19";
			case EnumEspecieDocumentoCaixa.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoCaixa.MensalidadeEscolar:
				return "21";
			case EnumEspecieDocumentoCaixa.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoCaixa.NotaFiscal:
				return "23";
			case EnumEspecieDocumentoCaixa.DocumentoDeDivida:
				return "24";
			case EnumEspecieDocumentoCaixa.CedulaProdutorRural:
				return "25";
			case EnumEspecieDocumentoCaixa.Outros:
				return "99";
			default:
				return "2";
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public EnumEspecieDocumentoCaixa GetEnumEspecieByCodigo(string codigo)
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
										return EnumEspecieDocumentoCaixa.Outros;
									}
								}
							}
							else if (codigo == "19")
							{
								return EnumEspecieDocumentoCaixa.NotaDebito;
							}
						}
						else if (codigo == "18")
						{
							return EnumEspecieDocumentoCaixa.Fatura;
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
									return EnumEspecieDocumentoCaixa.NotaSeguro;
								}
							}
						}
						else if (codigo == "15")
						{
							return EnumEspecieDocumentoCaixa.TriplicataServico;
						}
					}
					else if (codigo == "14")
					{
						return EnumEspecieDocumentoCaixa.TriplicataMercantil;
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
									return EnumEspecieDocumentoCaixa.NotaCreditoRural;
								}
							}
						}
						else if (codigo == "10")
						{
							return EnumEspecieDocumentoCaixa.NotaCreditoIndustrial;
						}
					}
					else if (codigo == "17")
					{
						return EnumEspecieDocumentoCaixa.Recibo;
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
								return EnumEspecieDocumentoCaixa.NotaPromissoriaRural;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoCaixa.NotaPromissoria;
					}
				}
				else if (num != 806133968U)
				{
					if (num == 822911587U)
					{
						if (codigo == "4")
						{
							return EnumEspecieDocumentoCaixa.DuplicataServico;
						}
					}
				}
				else if (codigo == "5")
				{
					return EnumEspecieDocumentoCaixa.DuplicataServicoIndicacao;
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
									return EnumEspecieDocumentoCaixa.Cheque;
								}
							}
						}
						else if (codigo == "6")
						{
							return EnumEspecieDocumentoCaixa.DuplicataRural;
						}
					}
					else if (codigo == "7")
					{
						return EnumEspecieDocumentoCaixa.LetraCambio;
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
								return EnumEspecieDocumentoCaixa.NotaCreditoExportacao;
							}
						}
					}
					else if (codigo == "2")
					{
						return EnumEspecieDocumentoCaixa.DuplicataMercantil;
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoCaixa.DuplicataMercantilIndicacao;
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
								return EnumEspecieDocumentoCaixa.CedulaProdutorRural;
							}
						}
					}
					else if (codigo == "25")
					{
						return EnumEspecieDocumentoCaixa.ParcelaConsorcio;
					}
				}
				else if (codigo == "8")
				{
					return EnumEspecieDocumentoCaixa.NotaCreditoComercial;
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
							return EnumEspecieDocumentoCaixa.ApoliceSeguro;
						}
					}
				}
				else if (codigo == "21")
				{
					return EnumEspecieDocumentoCaixa.MensalidadeEscolar;
				}
			}
			else if (num != 2398264082U)
			{
				if (num == 2415041701U)
				{
					if (codigo == "22")
					{
						return EnumEspecieDocumentoCaixa.NotaFiscal;
					}
				}
			}
			else if (codigo == "23")
			{
				return EnumEspecieDocumentoCaixa.DocumentoDeDivida;
			}
			return EnumEspecieDocumentoCaixa.DuplicataMercantil;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000104CC File Offset: 0x0000E6CC
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoCaixa();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoCaixa.Cheque:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.Cheque);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoCaixa.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoCaixa.DuplicataMercantilIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataMercantilIndicacao);
					this.Especie = "Duplicata Mercantil p/ Indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoCaixa.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataServico);
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoCaixa.DuplicataServicoIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataServicoIndicacao);
					this.Especie = "Duplicata de Serviço p/ Indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoCaixa.DuplicataRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataRural);
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoCaixa.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.LetraCambio);
					this.Especie = "Letra de Câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoCaixa.NotaCreditoComercial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaCreditoComercial);
					this.Especie = "Nota de Crédito Comercial";
					this.Sigla = "NCC";
					break;
				case EnumEspecieDocumentoCaixa.NotaCreditoExportacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaCreditoExportacao);
					this.Especie = "Nota de Crédito a Exportação";
					this.Sigla = "NCE";
					break;
				case EnumEspecieDocumentoCaixa.NotaCreditoIndustrial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaCreditoIndustrial);
					this.Especie = "Nota de Crédito Industrial";
					this.Sigla = "NCI";
					break;
				case EnumEspecieDocumentoCaixa.NotaCreditoRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaCreditoRural);
					this.Especie = "Nota de Crédito Rural";
					this.Sigla = "NCR";
					break;
				case EnumEspecieDocumentoCaixa.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoCaixa.NotaPromissoriaRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NPR";
					break;
				case EnumEspecieDocumentoCaixa.TriplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.TriplicataMercantil);
					this.Especie = "Triplicata Mercantil";
					this.Sigla = "TM";
					break;
				case EnumEspecieDocumentoCaixa.TriplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.TriplicataServico);
					this.Especie = "Triplicata de Serviço";
					this.Sigla = "TS";
					break;
				case EnumEspecieDocumentoCaixa.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaSeguro);
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoCaixa.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoCaixa.Fatura:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.Fatura);
					this.Especie = "Fatura";
					this.Sigla = "FAT";
					break;
				case EnumEspecieDocumentoCaixa.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaDebito);
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoCaixa.ApoliceSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.ApoliceSeguro);
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoCaixa.MensalidadeEscolar:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.MensalidadeEscolar);
					this.Especie = "Mensalidade Escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoCaixa.ParcelaConsorcio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.ParcelaConsorcio);
					this.Especie = "Parcela de Consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoCaixa.NotaFiscal:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.NotaFiscal);
					this.Especie = "Nota Fiscal";
					this.Sigla = "NF";
					break;
				case EnumEspecieDocumentoCaixa.DocumentoDeDivida:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DocumentoDeDivida);
					this.Especie = "Documento de Dívida";
					this.Sigla = "DD";
					break;
				case EnumEspecieDocumentoCaixa.CedulaProdutorRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.CedulaProdutorRural);
					this.Especie = "Cédula de Produto Rural";
					this.Sigla = "CPR";
					break;
				case EnumEspecieDocumentoCaixa.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.Outros);
					this.Especie = "Outros";
					this.Sigla = "OU";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCaixa.DuplicataMercantil);
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

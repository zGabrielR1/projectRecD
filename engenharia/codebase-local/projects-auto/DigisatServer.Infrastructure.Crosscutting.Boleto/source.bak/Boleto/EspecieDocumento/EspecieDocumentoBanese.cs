using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000E8 RID: 232
	public class EspecieDocumentoBanese : AbstractEspecieDocumento
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoBanese()
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000123F8 File Offset: 0x000105F8
		public EspecieDocumentoBanese(string codigo)
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

		// Token: 0x0600039A RID: 922 RVA: 0x00012434 File Offset: 0x00010634
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBanese.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoBanese.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoBanese.NotaCreditoComercial:
				return "8";
			case EnumEspecieDocumentoBanese.NotaCreditoExportacao:
				return "9";
			case EnumEspecieDocumentoBanese.NotaCreditoIndustrial:
				return "10";
			case EnumEspecieDocumentoBanese.NotaCreditoRural:
				return "11";
			case EnumEspecieDocumentoBanese.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoBanese.Recibo:
				return "17";
			case EnumEspecieDocumentoBanese.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoBanese.MensalidadeEscolar:
				return "21";
			case EnumEspecieDocumentoBanese.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoBanese.NotaFiscal:
				return "23";
			case EnumEspecieDocumentoBanese.Outros:
				return "99";
			default:
				return "2";
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000124D0 File Offset: 0x000106D0
		public EnumEspecieDocumentoBanese GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 822911587U)
			{
				if (num <= 468396612U)
				{
					if (num != 352233207U)
					{
						if (num != 451618993U)
						{
							if (num == 468396612U)
							{
								if (codigo == "10")
								{
									return EnumEspecieDocumentoBanese.NotaCreditoIndustrial;
								}
							}
						}
						else if (codigo == "17")
						{
							return EnumEspecieDocumentoBanese.Recibo;
						}
					}
					else if (codigo == "99")
					{
						return EnumEspecieDocumentoBanese.Outros;
					}
				}
				else if (num != 485174231U)
				{
					if (num != 501951850U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoBanese.DuplicataServico;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoBanese.NotaPromissoria;
					}
				}
				else if (codigo == "11")
				{
					return EnumEspecieDocumentoBanese.NotaCreditoRural;
				}
			}
			else if (num <= 1024243015U)
			{
				if (num != 923577301U)
				{
					if (num != 1007465396U)
					{
						if (num == 1024243015U)
						{
							if (codigo == "8")
							{
								return EnumEspecieDocumentoBanese.NotaCreditoComercial;
							}
						}
					}
					else if (codigo == "9")
					{
						return EnumEspecieDocumentoBanese.NotaCreditoExportacao;
					}
				}
				else if (codigo == "2")
				{
					return EnumEspecieDocumentoBanese.DuplicataMercantil;
				}
			}
			else if (num <= 2364708844U)
			{
				if (num != 2297598368U)
				{
					if (num == 2364708844U)
					{
						if (codigo == "21")
						{
							return EnumEspecieDocumentoBanese.MensalidadeEscolar;
						}
					}
				}
				else if (codigo == "25")
				{
					return EnumEspecieDocumentoBanese.ParcelaConsorcio;
				}
			}
			else if (num != 2381486463U)
			{
				if (num == 2415041701U)
				{
					if (codigo == "22")
					{
						return EnumEspecieDocumentoBanese.NotaFiscal;
					}
				}
			}
			else if (codigo == "20")
			{
				return EnumEspecieDocumentoBanese.ApoliceSeguro;
			}
			return EnumEspecieDocumentoBanese.DuplicataMercantil;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000126AC File Offset: 0x000108AC
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoBanese();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoBanese.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoBanese.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.DuplicataServico);
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoBanese.NotaCreditoComercial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaCreditoComercial);
					this.Especie = "Nota de Crédito Comercial";
					this.Sigla = "NCC";
					break;
				case EnumEspecieDocumentoBanese.NotaCreditoExportacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaCreditoExportacao);
					this.Especie = "Nota de Crédito a Exportação";
					this.Sigla = "NCE";
					break;
				case EnumEspecieDocumentoBanese.NotaCreditoIndustrial:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaCreditoIndustrial);
					this.Especie = "Nota de Crédito Industrial";
					this.Sigla = "NCI";
					break;
				case EnumEspecieDocumentoBanese.NotaCreditoRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaCreditoRural);
					this.Especie = "Nota de Crédito Rural";
					this.Sigla = "NCR";
					break;
				case EnumEspecieDocumentoBanese.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoBanese.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoBanese.ApoliceSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.ApoliceSeguro);
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoBanese.MensalidadeEscolar:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.MensalidadeEscolar);
					this.Especie = "Mensalidade Escolar";
					this.Sigla = "ME";
					break;
				case EnumEspecieDocumentoBanese.ParcelaConsorcio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.ParcelaConsorcio);
					this.Especie = "Parcela de Consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoBanese.NotaFiscal:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.NotaFiscal);
					this.Especie = "Nota Fiscal";
					this.Sigla = "NF";
					break;
				case EnumEspecieDocumentoBanese.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.Outros);
					this.Especie = "Outros";
					this.Sigla = "OU";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanese.DuplicataMercantil);
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

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000D4 RID: 212
	public class EspecieDocumentoBanrisul : AbstractEspecieDocumento
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000F694 File Offset: 0x0000D894
		public EspecieDocumentoBanrisul(string codigo)
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

		// Token: 0x0600036A RID: 874 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBanrisul.DuplicataMercantil:
				return "02";
			case EnumEspecieDocumentoBanrisul.DuplicataMercantilPorIndicacao:
				return "03";
			case EnumEspecieDocumentoBanrisul.DuplicataServico:
				return "04";
			case EnumEspecieDocumentoBanrisul.DuplicataServicoPorIndicacao:
				return "05";
			case EnumEspecieDocumentoBanrisul.LetraCambio:
				return "07";
			case EnumEspecieDocumentoBanrisul.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoBanrisul.ParcelaConsorcio:
				return "22";
			case EnumEspecieDocumentoBanrisul.Outros:
				return "99";
			default:
				return "02";
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000F73C File Offset: 0x0000D93C
		public EnumEspecieDocumentoBanrisul GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 501951850U)
			{
				if (num <= 451766088U)
				{
					if (num != 352233207U)
					{
						if (num == 451766088U)
						{
							if (codigo == "07")
							{
								return EnumEspecieDocumentoBanrisul.LetraCambio;
							}
						}
					}
					else if (codigo == "99")
					{
						return EnumEspecieDocumentoBanrisul.Outros;
					}
				}
				else if (num != 485321326U)
				{
					if (num == 501951850U)
					{
						if (codigo == "12")
						{
							return EnumEspecieDocumentoBanrisul.NotaPromissoria;
						}
					}
				}
				else if (codigo == "05")
				{
					return EnumEspecieDocumentoBanrisul.DuplicataServicoPorIndicacao;
				}
			}
			else if (num <= 518876564U)
			{
				if (num != 502098945U)
				{
					if (num == 518876564U)
					{
						if (codigo == "03")
						{
							return EnumEspecieDocumentoBanrisul.DuplicataMercantilPorIndicacao;
						}
					}
				}
				else if (codigo == "04")
				{
					return EnumEspecieDocumentoBanrisul.DuplicataServico;
				}
			}
			else if (num != 535654183U)
			{
				if (num == 2415041701U)
				{
					if (codigo == "22")
					{
						return EnumEspecieDocumentoBanrisul.ParcelaConsorcio;
					}
				}
			}
			else if (codigo == "02")
			{
				return EnumEspecieDocumentoBanrisul.DuplicataMercantil;
			}
			return EnumEspecieDocumentoBanrisul.DuplicataMercantil;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000F850 File Offset: 0x0000DA50
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoBanrisul();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoBanrisul.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoBanrisul.DuplicataMercantilPorIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.DuplicataMercantilPorIndicacao);
					this.Especie = "Duplicata mercantil p/ indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoBanrisul.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.DuplicataServico);
					this.Especie = "Duplicata de serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoBanrisul.DuplicataServicoPorIndicacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.DuplicataServicoPorIndicacao);
					this.Especie = "Duplicata de serviço p/ indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoBanrisul.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.LetraCambio);
					this.Especie = "Letra de câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoBanrisul.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoBanrisul.ParcelaConsorcio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.ParcelaConsorcio);
					this.Especie = "Parcela de consórcio";
					this.Sigla = "PC";
					break;
				case EnumEspecieDocumentoBanrisul.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.Outros);
					this.Especie = "Outros";
					this.Sigla = "OUT";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBanrisul.DuplicataMercantil);
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

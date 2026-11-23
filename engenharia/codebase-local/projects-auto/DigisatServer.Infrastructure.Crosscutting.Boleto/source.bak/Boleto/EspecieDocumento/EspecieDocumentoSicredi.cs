using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000F0 RID: 240
	public class EspecieDocumentoSicredi : AbstractEspecieDocumento
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00014320 File Offset: 0x00012520
		public EspecieDocumentoSicredi(string codigo)
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

		// Token: 0x060003AC RID: 940 RVA: 0x0001435C File Offset: 0x0001255C
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoSicredi();
				switch (EspecieDocumentoSicredi.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoSicredi.DuplicataMercantilIndicacao:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.DuplicataMercantilIndicacao);
					this.Especie = "Duplicata Mercantil por Indicação";
					this.Sigla = "DMI";
					break;
				case EnumEspecieDocumentoSicredi.DuplicataServicoIndicacao:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.DuplicataServicoIndicacao);
					this.Especie = "Duplicata de Serviço por Indicação";
					this.Sigla = "DSI";
					break;
				case EnumEspecieDocumentoSicredi.DuplicataRural:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.DuplicataRural);
					this.Especie = "Duplicata Rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoSicredi.LetraCambio:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.LetraCambio);
					this.Especie = "Letra de Câmbio";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoSicredi.NotaPromissoria:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoSicredi.NotaPromissoriaRural:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NPR";
					break;
				case EnumEspecieDocumentoSicredi.NotaSeguros:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.NotaSeguros);
					this.Especie = "Nota de Seguros";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoSicredi.Recibo:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoSicredi.NotaDebito:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.NotaDebito);
					this.Especie = "Nota de Débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoSicredi.Outros:
					this.Codigo = EspecieDocumentoSicredi.GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi.Outros);
					this.Especie = "Outros";
					this.Sigla = "";
					break;
				default:
					this.Codigo = "03";
					this.Especie = "Duplicata Mercantil por Indicação";
					this.Sigla = "DMI";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014578 File Offset: 0x00012778
		public static string GetCodigoEspecieByEnum(EnumEspecieDocumentoSicredi especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoSicredi.DuplicataMercantilIndicacao:
				return "03";
			case EnumEspecieDocumentoSicredi.DuplicataServicoIndicacao:
				return "05";
			case EnumEspecieDocumentoSicredi.DuplicataRural:
				return "06";
			case EnumEspecieDocumentoSicredi.LetraCambio:
				return "07";
			case EnumEspecieDocumentoSicredi.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoSicredi.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoSicredi.NotaSeguros:
				return "16";
			case EnumEspecieDocumentoSicredi.Recibo:
				return "17";
			case EnumEspecieDocumentoSicredi.NotaDebito:
				return "19";
			case EnumEspecieDocumentoSicredi.Outros:
				return "99";
			default:
				return "03";
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000145F8 File Offset: 0x000127F8
		public static EnumEspecieDocumentoSicredi GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 451766088U)
			{
				if (num <= 352233207U)
				{
					if (num != 350953279U)
					{
						if (num == 352233207U)
						{
							if (codigo == "99")
							{
								return EnumEspecieDocumentoSicredi.Outros;
							}
						}
					}
					else if (codigo == "19")
					{
						return EnumEspecieDocumentoSicredi.NotaDebito;
					}
				}
				else if (num != 434841374U)
				{
					if (num != 451618993U)
					{
						if (num == 451766088U)
						{
							if (codigo == "07")
							{
								return EnumEspecieDocumentoSicredi.LetraCambio;
							}
						}
					}
					else if (codigo == "17")
					{
						return EnumEspecieDocumentoSicredi.Recibo;
					}
				}
				else if (codigo == "16")
				{
					return EnumEspecieDocumentoSicredi.NotaSeguros;
				}
			}
			else if (num <= 485321326U)
			{
				if (num != 468543707U)
				{
					if (num == 485321326U)
					{
						if (codigo == "05")
						{
							return EnumEspecieDocumentoSicredi.DuplicataServicoIndicacao;
						}
					}
				}
				else if (codigo == "06")
				{
					return EnumEspecieDocumentoSicredi.DuplicataRural;
				}
			}
			else if (num != 501951850U)
			{
				if (num != 518729469U)
				{
					if (num == 518876564U)
					{
						if (codigo == "03")
						{
							return EnumEspecieDocumentoSicredi.DuplicataMercantilIndicacao;
						}
					}
				}
				else if (codigo == "13")
				{
					return EnumEspecieDocumentoSicredi.NotaPromissoriaRural;
				}
			}
			else if (codigo == "12")
			{
				return EnumEspecieDocumentoSicredi.NotaPromissoria;
			}
			return EnumEspecieDocumentoSicredi.DuplicataMercantilIndicacao;
		}
	}
}

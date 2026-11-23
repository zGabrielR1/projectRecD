using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000EE RID: 238
	public class EspecieDocumentoNordeste : AbstractEspecieDocumento
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00014004 File Offset: 0x00012204
		public EspecieDocumentoNordeste(string codigo)
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

		// Token: 0x060003A8 RID: 936 RVA: 0x00014040 File Offset: 0x00012240
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoNordeste();
				switch (EspecieDocumentoNordeste.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoNordeste.DuplicataMercantil:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoNordeste.NotaPromissoria:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoNordeste.Cheque:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.Cheque);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoNordeste.Carne:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.Carne);
					this.Especie = "Carnê";
					this.Sigla = "";
					break;
				case EnumEspecieDocumentoNordeste.Recibo:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoNordeste.DuplicataPrestacaoServicos:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.DuplicataPrestacaoServicos);
					this.Especie = "Apólice ramos elementares";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoNordeste.Outros:
					this.Codigo = EspecieDocumentoNordeste.GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste.Outros);
					this.Especie = "Outros";
					this.Sigla = "OU";
					break;
				default:
					this.Codigo = "01";
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

		// Token: 0x060003A9 RID: 937 RVA: 0x000141DC File Offset: 0x000123DC
		public static string GetCodigoEspecieByEnum(EnumEspecieDocumentoNordeste especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoNordeste.DuplicataMercantil:
				return "01";
			case EnumEspecieDocumentoNordeste.NotaPromissoria:
				return "02";
			case EnumEspecieDocumentoNordeste.Cheque:
				return "03";
			case EnumEspecieDocumentoNordeste.Carne:
				return "04";
			case EnumEspecieDocumentoNordeste.Recibo:
				return "05";
			case EnumEspecieDocumentoNordeste.DuplicataPrestacaoServicos:
				return "06";
			case EnumEspecieDocumentoNordeste.Outros:
				return "19";
			default:
				return "01";
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001423C File Offset: 0x0001243C
		public static EnumEspecieDocumentoNordeste GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 485321326U)
			{
				if (num != 350953279U)
				{
					if (num != 468543707U)
					{
						if (num == 485321326U)
						{
							if (codigo == "05")
							{
								return EnumEspecieDocumentoNordeste.Recibo;
							}
						}
					}
					else if (codigo == "06")
					{
						return EnumEspecieDocumentoNordeste.DuplicataPrestacaoServicos;
					}
				}
				else if (codigo == "19")
				{
					return EnumEspecieDocumentoNordeste.Outros;
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
							return EnumEspecieDocumentoNordeste.Cheque;
						}
					}
				}
				else if (codigo == "04")
				{
					return EnumEspecieDocumentoNordeste.Carne;
				}
			}
			else if (num != 535654183U)
			{
				if (num == 552431802U)
				{
					if (codigo == "01")
					{
						return EnumEspecieDocumentoNordeste.DuplicataMercantil;
					}
				}
			}
			else if (codigo == "02")
			{
				return EnumEspecieDocumentoNordeste.NotaPromissoria;
			}
			return EnumEspecieDocumentoNordeste.DuplicataMercantil;
		}
	}
}

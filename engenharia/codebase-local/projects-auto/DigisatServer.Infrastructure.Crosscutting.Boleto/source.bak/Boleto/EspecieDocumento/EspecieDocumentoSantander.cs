using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000E6 RID: 230
	public class EspecieDocumentoSantander : AbstractEspecieDocumento
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00011FC0 File Offset: 0x000101C0
		public EspecieDocumentoSantander(string codigo)
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

		// Token: 0x06000395 RID: 917 RVA: 0x00011FFC File Offset: 0x000101FC
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoSantander.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoSantander.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoSantander.LetraCambio353:
				return "7";
			case EnumEspecieDocumentoSantander.LetraCambio008:
				return "30";
			case EnumEspecieDocumentoSantander.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoSantander.NotaPromissoriaRural:
				return "13";
			case EnumEspecieDocumentoSantander.Recibo:
				return "17";
			case EnumEspecieDocumentoSantander.ApoliceSeguro:
				return "20";
			case EnumEspecieDocumentoSantander.Cheque:
				return "97";
			case EnumEspecieDocumentoSantander.NotaPromissoariaDireta:
				return "98";
			default:
				return "2";
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001207C File Offset: 0x0001027C
		public EnumEspecieDocumentoSantander GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 518729469U)
			{
				if (num <= 335455588U)
				{
					if (num != 184457017U)
					{
						if (num == 335455588U)
						{
							if (codigo == "98")
							{
								return EnumEspecieDocumentoSantander.NotaPromissoariaDireta;
							}
						}
					}
					else if (codigo == "97")
					{
						return EnumEspecieDocumentoSantander.Cheque;
					}
				}
				else if (num != 451618993U)
				{
					if (num != 501951850U)
					{
						if (num == 518729469U)
						{
							if (codigo == "13")
							{
								return EnumEspecieDocumentoSantander.NotaPromissoriaRural;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoSantander.NotaPromissoria;
					}
				}
				else if (codigo == "17")
				{
					return EnumEspecieDocumentoSantander.Recibo;
				}
			}
			else if (num <= 839689206U)
			{
				if (num != 822911587U)
				{
					if (num == 839689206U)
					{
						if (codigo == "7")
						{
							return EnumEspecieDocumentoSantander.LetraCambio353;
						}
					}
				}
				else if (codigo == "4")
				{
					return EnumEspecieDocumentoSantander.DuplicataServico;
				}
			}
			else if (num != 923577301U)
			{
				if (num != 2280673654U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return EnumEspecieDocumentoSantander.ApoliceSeguro;
						}
					}
				}
				else if (codigo == "30")
				{
					return EnumEspecieDocumentoSantander.LetraCambio008;
				}
			}
			else if (codigo == "2")
			{
				return EnumEspecieDocumentoSantander.DuplicataMercantil;
			}
			return EnumEspecieDocumentoSantander.DuplicataMercantil;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000121D0 File Offset: 0x000103D0
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoSantander();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoSantander.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoSantander.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.DuplicataServico);
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoSantander.LetraCambio353:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.LetraCambio353);
					this.Especie = "Letra de Câmbio (Somente para o banco 353)";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoSantander.LetraCambio008:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.LetraCambio008);
					this.Especie = "Letra de Câmbio (Somente para o banco 008)";
					this.Sigla = "LC";
					break;
				case EnumEspecieDocumentoSantander.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoSantander.NotaPromissoriaRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.NotaPromissoriaRural);
					this.Especie = "Nota Promissória Rural";
					this.Sigla = "NR";
					break;
				case EnumEspecieDocumentoSantander.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoSantander.ApoliceSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.ApoliceSeguro);
					this.Especie = "Apólice de Seguro";
					this.Sigla = "AP";
					break;
				case EnumEspecieDocumentoSantander.Cheque:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.Cheque);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoSantander.NotaPromissoariaDireta:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSantander.NotaPromissoariaDireta);
					this.Especie = "Nota Promissória Direta";
					this.Sigla = "ND";
					break;
				default:
					this.Codigo = "2";
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

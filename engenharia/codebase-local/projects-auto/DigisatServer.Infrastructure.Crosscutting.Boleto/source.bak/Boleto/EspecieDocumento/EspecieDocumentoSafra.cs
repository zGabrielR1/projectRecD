using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000E4 RID: 228
	public class EspecieDocumentoSafra : AbstractEspecieDocumento
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoSafra()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011D48 File Offset: 0x0000FF48
		public EspecieDocumentoSafra(string codigo)
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

		// Token: 0x06000391 RID: 913 RVA: 0x00011D84 File Offset: 0x0000FF84
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoSafra.DuplicataMercantil:
				return "02";
			case EnumEspecieDocumentoSafra.DuplicataServico:
				return "04";
			case EnumEspecieDocumentoSafra.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoSafra.Recibo:
				return "17";
			case EnumEspecieDocumentoSafra.NotaFiscal:
				return "23";
			case EnumEspecieDocumentoSafra.Outros:
				return "99";
			default:
				return "02";
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011DDC File Offset: 0x0000FFDC
		public EnumEspecieDocumentoSafra GetEnumEspecieByCodigo(string codigo)
		{
			if (codigo == "02")
			{
				return EnumEspecieDocumentoSafra.DuplicataMercantil;
			}
			if (codigo == "04")
			{
				return EnumEspecieDocumentoSafra.DuplicataServico;
			}
			if (codigo == "12")
			{
				return EnumEspecieDocumentoSafra.NotaPromissoria;
			}
			if (codigo == "17")
			{
				return EnumEspecieDocumentoSafra.Recibo;
			}
			if (codigo == "23")
			{
				return EnumEspecieDocumentoSafra.NotaFiscal;
			}
			if (!(codigo == "99"))
			{
				return EnumEspecieDocumentoSafra.DuplicataMercantil;
			}
			return EnumEspecieDocumentoSafra.Outros;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00011E48 File Offset: 0x00010048
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoSafra();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoSafra.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoSafra.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.DuplicataServico);
					this.Especie = "Duplicata de Serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoSafra.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoSafra.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoSafra.NotaFiscal:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.NotaFiscal);
					this.Especie = "Nota fiscal";
					this.Sigla = "NF";
					break;
				case EnumEspecieDocumentoSafra.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoSafra.Outros);
					this.Especie = "Outros";
					this.Sigla = "";
					break;
				default:
					this.Codigo = "02";
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

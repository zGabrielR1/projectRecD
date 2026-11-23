using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000D6 RID: 214
	public class EspecieDocumentoDaycoval : AbstractEspecieDocumento
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoDaycoval()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public EspecieDocumentoDaycoval(string codigo)
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

		// Token: 0x0600036F RID: 879 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoDaycoval especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoDaycoval.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoDaycoval.DuplicataServico:
				return "12";
			case EnumEspecieDocumentoDaycoval.Outros:
				return "99";
			default:
				return "99";
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000FA89 File Offset: 0x0000DC89
		public EnumEspecieDocumentoDaycoval GetEnumEspecieByCodigo(string codigo)
		{
			if (codigo == "1")
			{
				return EnumEspecieDocumentoDaycoval.DuplicataMercantil;
			}
			if (codigo == "12")
			{
				return EnumEspecieDocumentoDaycoval.DuplicataServico;
			}
			if (!(codigo == "99"))
			{
				return EnumEspecieDocumentoDaycoval.Outros;
			}
			return EnumEspecieDocumentoDaycoval.Outros;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000FABC File Offset: 0x0000DCBC
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoDaycoval();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoDaycoval.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoDaycoval.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoDaycoval.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoDaycoval.DuplicataServico);
					this.Sigla = "DS";
					this.Especie = "Duplicata de serviço";
					break;
				case EnumEspecieDocumentoDaycoval.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoDaycoval.Outros);
					this.Especie = "Outros";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoDaycoval.DuplicataMercantil);
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

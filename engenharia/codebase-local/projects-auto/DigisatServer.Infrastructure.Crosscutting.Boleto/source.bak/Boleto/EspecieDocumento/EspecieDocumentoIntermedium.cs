using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000DE RID: 222
	public class EspecieDocumentoIntermedium : AbstractEspecieDocumento
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoIntermedium()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010D50 File Offset: 0x0000EF50
		public EspecieDocumentoIntermedium(string codigo)
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

		// Token: 0x06000381 RID: 897 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoIntermedium especie)
		{
			if (especie == EnumEspecieDocumentoIntermedium.Outros)
			{
				return "99";
			}
			return "1";
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00010D9C File Offset: 0x0000EF9C
		public EnumEspecieDocumentoIntermedium GetEnumEspecieByCodigo(string codigo)
		{
			if (codigo == "99")
			{
				return EnumEspecieDocumentoIntermedium.Outros;
			}
			return EnumEspecieDocumentoIntermedium.DuplicataMercantil;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoIntermedium();
				EspecieDocumentoIntermedium especieDocumentoIntermedium = new EspecieDocumentoIntermedium();
				EnumEspecieDocumentoIntermedium enumEspecieByCodigo = this.GetEnumEspecieByCodigo(idCodigo);
				if (enumEspecieByCodigo != EnumEspecieDocumentoIntermedium.Outros)
				{
					if (enumEspecieByCodigo != EnumEspecieDocumentoIntermedium.DuplicataMercantil)
					{
						this.Codigo = especieDocumentoIntermedium.GetCodigoEspecieByEnum(EnumEspecieDocumentoIntermedium.Outros);
						this.Especie = "Outros";
					}
					else
					{
						this.Codigo = especieDocumentoIntermedium.GetCodigoEspecieByEnum(EnumEspecieDocumentoIntermedium.DuplicataMercantil);
						this.Especie = "Duplicata Mercantil";
					}
				}
				else
				{
					this.Codigo = especieDocumentoIntermedium.GetCodigoEspecieByEnum(EnumEspecieDocumentoIntermedium.Outros);
					this.Especie = "Outros";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000044 RID: 68
	public class TipoEmissaoBanese : AbstractTipoEmissao
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00006934 File Offset: 0x00004B34
		public TipoEmissaoBanese(int codigo)
		{
			try
			{
				this.CarregarTipoEmissao(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006970 File Offset: 0x00004B70
		private void CarregarTipoEmissao(int idCodigo)
		{
			try
			{
				if (idCodigo == 2)
				{
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBanese.ClienteEmite);
				}
				else
				{
					this.Codigo = 2;
					this.Descricao = "Cliente emite";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

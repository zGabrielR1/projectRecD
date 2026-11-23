using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200007C RID: 124
	public class TipoCobrancaSicoob : AbstractTipoCobranca
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000815C File Offset: 0x0000635C
		public TipoCobrancaSicoob(int codigo)
		{
			try
			{
				this.CarregarTipoCobranca(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008198 File Offset: 0x00006398
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSicoob.Simples);
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSicoob.GarantidaVinculada);
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSicoob.GarantidaDescontada);
					break;
				default:
					this.Codigo = 1;
					this.Descricao = "Simples";
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

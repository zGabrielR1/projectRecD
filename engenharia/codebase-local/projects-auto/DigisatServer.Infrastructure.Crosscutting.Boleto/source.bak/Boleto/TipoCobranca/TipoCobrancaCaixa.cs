using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000074 RID: 116
	public class TipoCobrancaCaixa : AbstractTipoCobranca
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00007DB8 File Offset: 0x00005FB8
		public TipoCobrancaCaixa(int codigo)
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

		// Token: 0x0600024D RID: 589 RVA: 0x00007DF4 File Offset: 0x00005FF4
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaCaixa.CobrancaSimples);
					goto IL_007C;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaCaixa.CobrancaCaucionada);
					goto IL_007C;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaCaixa.CobrancaDescontada);
					goto IL_007C;
				}
				this.Codigo = 1;
				this.Descricao = "Cobrança Simples";
				IL_007C:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

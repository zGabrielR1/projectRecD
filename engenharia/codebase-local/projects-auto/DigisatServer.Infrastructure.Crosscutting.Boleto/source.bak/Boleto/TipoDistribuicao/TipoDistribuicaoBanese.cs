using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x0200005B RID: 91
	public class TipoDistribuicaoBanese : AbstractTipoDistribuicao
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00007340 File Offset: 0x00005540
		public TipoDistribuicaoBanese(int codigo)
		{
			try
			{
				this.CarregarTipoDistribuicao(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000737C File Offset: 0x0000557C
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo == 2)
				{
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBanese.ClienteDistribui);
				}
				else
				{
					this.Codigo = 2;
					this.Descricao = "Cliente distribui";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000069 RID: 105
	public class TipoDistribuicaoSicredi : AbstractTipoDistribuicao
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00007844 File Offset: 0x00005A44
		public TipoDistribuicaoSicredi(int codigo)
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

		// Token: 0x0600022E RID: 558 RVA: 0x00007880 File Offset: 0x00005A80
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cedente distribui";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSicredi.CedenteDistribui);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSicredi.SicrediDistribui);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

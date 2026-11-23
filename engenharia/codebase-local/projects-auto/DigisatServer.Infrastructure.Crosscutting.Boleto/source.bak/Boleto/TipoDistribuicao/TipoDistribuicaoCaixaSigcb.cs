using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000059 RID: 89
	public class TipoDistribuicaoCaixaSigcb : AbstractTipoDistribuicao
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00007284 File Offset: 0x00005484
		public TipoDistribuicaoCaixaSigcb(int codigo)
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

		// Token: 0x0600021E RID: 542 RVA: 0x000072C0 File Offset: 0x000054C0
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cliente distribui";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSigcb.ClienteDistribui);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSigcb.BancoDistribui);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

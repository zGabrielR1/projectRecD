using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000061 RID: 97
	public class TipoDistribuicaoSafra : AbstractTipoDistribuicao
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00007554 File Offset: 0x00005754
		public TipoDistribuicaoSafra(int codigo)
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

		// Token: 0x06000226 RID: 550 RVA: 0x00007590 File Offset: 0x00005790
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cliente distribui boleto";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSafra.ClienteDistribuiBoleto);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSafra.BancoDistribuiBoleto);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

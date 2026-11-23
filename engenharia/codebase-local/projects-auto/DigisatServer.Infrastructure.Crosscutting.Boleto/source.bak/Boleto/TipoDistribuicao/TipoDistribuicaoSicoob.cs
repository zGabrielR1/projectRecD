using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000065 RID: 101
	public class TipoDistribuicaoSicoob : AbstractTipoDistribuicao
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000076CC File Offset: 0x000058CC
		public TipoDistribuicaoSicoob(int codigo)
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

		// Token: 0x0600022A RID: 554 RVA: 0x00007708 File Offset: 0x00005908
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Beneficiário distribui";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSicoob.BeneficiarioDistribui);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoSicoob.SicoobDistribui);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

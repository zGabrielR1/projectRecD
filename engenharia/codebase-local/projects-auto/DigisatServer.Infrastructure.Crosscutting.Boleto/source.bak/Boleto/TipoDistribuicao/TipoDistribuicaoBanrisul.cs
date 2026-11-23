using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000055 RID: 85
	public class TipoDistribuicaoBanrisul : AbstractTipoDistribuicao
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000070A8 File Offset: 0x000052A8
		public TipoDistribuicaoBanrisul(int codigo)
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

		// Token: 0x0600021A RID: 538 RVA: 0x000070E4 File Offset: 0x000052E4
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cliente distribui.";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBanrisul.ClienteDistribui);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBanrisul.BancoDistribui);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

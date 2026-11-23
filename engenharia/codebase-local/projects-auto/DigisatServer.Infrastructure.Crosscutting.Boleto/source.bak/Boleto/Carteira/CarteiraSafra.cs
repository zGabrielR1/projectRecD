using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000144 RID: 324
	public class CarteiraSafra : AbstractCarteira
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00025D34 File Offset: 0x00023F34
		public CarteiraSafra()
		{
			try
			{
				this.Banco = new Banco(422);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00025D78 File Offset: 0x00023F78
		public CarteiraSafra(int carteira)
		{
			try
			{
				this.Carregar(carteira);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00025DB4 File Offset: 0x00023FB4
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoSafra();
				if (carteira != 1)
				{
					if (carteira != 2)
					{
						this.NumeroCarteira = 1;
						this.Codigo = "1";
						this.Tipo = "";
						this.Descricao = "Cobrança Simples";
					}
					else
					{
						this.NumeroCarteira = 2;
						this.Codigo = "3";
						this.Tipo = "";
						this.Descricao = "Cobrança Vinculada";
					}
				}
				else
				{
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

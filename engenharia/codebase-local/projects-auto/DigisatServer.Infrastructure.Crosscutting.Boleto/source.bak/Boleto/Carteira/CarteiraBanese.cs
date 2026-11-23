using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000148 RID: 328
	public class CarteiraBanese : AbstractCarteira
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x00025FC4 File Offset: 0x000241C4
		public CarteiraBanese()
		{
			try
			{
				this.Banco = new Banco(47);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00026004 File Offset: 0x00024204
		public CarteiraBanese(int carteira)
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

		// Token: 0x06000484 RID: 1156 RVA: 0x00026040 File Offset: 0x00024240
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoSicoob();
				if (carteira != 1)
				{
					if (carteira != 4)
					{
						this.NumeroCarteira = 1;
						this.Codigo = "1";
						this.Tipo = "";
						this.Descricao = "Cobrança Simples";
					}
					else
					{
						this.NumeroCarteira = 4;
						this.Codigo = "4";
						this.Tipo = "";
						this.Descricao = "Cobrança Descontada";
					}
				}
				else
				{
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "";
					this.Descricao = "Carteira Simples";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

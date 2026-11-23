using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200014C RID: 332
	public class CarteiraSicoob : AbstractCarteira
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00026318 File Offset: 0x00024518
		public CarteiraSicoob()
		{
			try
			{
				this.Banco = new Banco(756);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0002635C File Offset: 0x0002455C
		public CarteiraSicoob(int carteira)
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

		// Token: 0x0600048A RID: 1162 RVA: 0x00026398 File Offset: 0x00024598
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoSicoob();
				if (carteira != 1)
				{
					if (carteira != 3)
					{
						if (carteira != 9)
						{
							this.NumeroCarteira = 1;
							this.Codigo = "1";
							this.Tipo = "";
							this.Descricao = "Cobrança Simples";
						}
						else
						{
							this.NumeroCarteira = 9;
							this.Codigo = "9";
							this.Tipo = "";
							this.Descricao = "Nova Carteira de Cobrança";
						}
					}
					else
					{
						this.NumeroCarteira = 3;
						this.Codigo = "3";
						this.Tipo = "";
						this.Descricao = "Cobrança Garantida";
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

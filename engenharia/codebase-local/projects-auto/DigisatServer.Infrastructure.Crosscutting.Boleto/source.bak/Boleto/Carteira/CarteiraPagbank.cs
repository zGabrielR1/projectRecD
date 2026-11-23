using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200012F RID: 303
	public class CarteiraPagbank : AbstractCarteira
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00024994 File Offset: 0x00022B94
		public CarteiraPagbank()
		{
			try
			{
				this.Banco = new Banco(290);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000249D8 File Offset: 0x00022BD8
		public CarteiraPagbank(int carteira)
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

		// Token: 0x0600045E RID: 1118 RVA: 0x00024A14 File Offset: 0x00022C14
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoPagbank();
				if (carteira == 1)
				{
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples";
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

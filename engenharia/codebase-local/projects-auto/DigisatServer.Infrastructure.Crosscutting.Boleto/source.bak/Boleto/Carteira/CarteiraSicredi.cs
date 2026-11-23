using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200014E RID: 334
	public class CarteiraSicredi : AbstractCarteira
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x00026488 File Offset: 0x00024688
		public CarteiraSicredi()
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

		// Token: 0x0600048C RID: 1164 RVA: 0x000264CC File Offset: 0x000246CC
		public CarteiraSicredi(int carteira)
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

		// Token: 0x0600048D RID: 1165 RVA: 0x00026508 File Offset: 0x00024708
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoSicredi();
				if (carteira == 1)
				{
					this.NumeroCarteira = 1;
					this.Codigo = "CS";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples";
				}
				else
				{
					this.NumeroCarteira = 1;
					this.Codigo = "CS";
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

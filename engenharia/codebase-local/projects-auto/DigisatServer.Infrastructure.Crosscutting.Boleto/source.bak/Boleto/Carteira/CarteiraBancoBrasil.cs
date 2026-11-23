using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200012B RID: 299
	public class CarteiraBancoBrasil : AbstractCarteira
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x000245CC File Offset: 0x000227CC
		public CarteiraBancoBrasil()
		{
			try
			{
				this.Banco = new Banco(1);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0002460C File Offset: 0x0002280C
		public CarteiraBancoBrasil(int carteira)
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

		// Token: 0x06000458 RID: 1112 RVA: 0x00024648 File Offset: 0x00022848
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoBrasil();
				switch (carteira)
				{
				case 1:
					this.NumeroCarteira = 1;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança simples";
					goto IL_0167;
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança vinculada";
					goto IL_0167;
				case 3:
					this.NumeroCarteira = 3;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança caucionada";
					goto IL_0167;
				case 4:
					this.NumeroCarteira = 4;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança descontada";
					goto IL_0167;
				case 7:
					this.NumeroCarteira = 7;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança direta especial - Carteira 17";
					goto IL_0167;
				case 8:
					this.NumeroCarteira = 7;
					this.Codigo = "S";
					this.Tipo = "S";
					this.Descricao = "Cobrança direta especial - Carteira 17";
					goto IL_0167;
				}
				this.NumeroCarteira = 1;
				this.Codigo = "S";
				this.Tipo = "S";
				this.Descricao = "Cobrança simples com registro";
				IL_0167:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

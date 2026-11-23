using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200013B RID: 315
	public class CarteiraBradesco : AbstractCarteira
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x000251E8 File Offset: 0x000233E8
		public CarteiraBradesco()
		{
			try
			{
				this.Banco = new Banco(237);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0002522C File Offset: 0x0002342C
		public CarteiraBradesco(int carteira)
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

		// Token: 0x06000470 RID: 1136 RVA: 0x00025268 File Offset: 0x00023468
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoBradesco();
				switch (carteira)
				{
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = "02";
					this.Tipo = "";
					this.Descricao = "";
					goto IL_010B;
				case 3:
					this.NumeroCarteira = 3;
					this.Codigo = "03";
					this.Tipo = "";
					this.Descricao = "";
					goto IL_010B;
				case 4:
				case 5:
					break;
				case 6:
					this.NumeroCarteira = 6;
					this.Codigo = "06";
					this.Tipo = "";
					this.Descricao = "";
					goto IL_010B;
				default:
					if (carteira == 9)
					{
						this.NumeroCarteira = 9;
						this.Codigo = "09";
						this.Tipo = "";
						this.Descricao = "";
						goto IL_010B;
					}
					break;
				}
				this.NumeroCarteira = 9;
				this.Codigo = "09";
				this.Tipo = "";
				this.Descricao = "";
				IL_010B:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

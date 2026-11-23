using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000131 RID: 305
	public class CarteiraDaycoval : AbstractCarteira
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00024AA4 File Offset: 0x00022CA4
		public CarteiraDaycoval()
		{
			try
			{
				this.Banco = new Banco(707);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00024AE8 File Offset: 0x00022CE8
		public CarteiraDaycoval(int carteira)
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

		// Token: 0x06000461 RID: 1121 RVA: 0x00024B24 File Offset: 0x00022D24
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoDaycoval();
				if (carteira != 19)
				{
					if (carteira != 112)
					{
						this.NumeroCarteira = 19;
						this.Codigo = "19";
						this.Tipo = "";
						this.Descricao = "";
					}
					else
					{
						this.NumeroCarteira = 112;
						this.Codigo = "112";
						this.Tipo = "";
						this.Descricao = "";
					}
				}
				else
				{
					this.NumeroCarteira = 19;
					this.Codigo = "19";
					this.Tipo = "";
					this.Descricao = "";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000135 RID: 309
	public class CarteiraIntermedium : AbstractCarteira
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00024D0C File Offset: 0x00022F0C
		public CarteiraIntermedium()
		{
			try
			{
				this.Banco = new Banco(77);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00024D4C File Offset: 0x00022F4C
		public CarteiraIntermedium(int carteira)
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

		// Token: 0x06000467 RID: 1127 RVA: 0x00024D88 File Offset: 0x00022F88
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoIntermedium();
				if (carteira == 112)
				{
					this.NumeroCarteira = 112;
					this.Codigo = "I";
					this.Tipo = "E";
					this.Descricao = "Escritural eletrônica simples";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

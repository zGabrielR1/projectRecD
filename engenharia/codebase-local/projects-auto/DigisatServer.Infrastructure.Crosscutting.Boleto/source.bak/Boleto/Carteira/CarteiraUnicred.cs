using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000150 RID: 336
	public class CarteiraUnicred : AbstractCarteira
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00026598 File Offset: 0x00024798
		public CarteiraUnicred()
		{
			try
			{
				this.Banco = new Banco(136);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000265DC File Offset: 0x000247DC
		public CarteiraUnicred(int carteira)
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

		// Token: 0x06000490 RID: 1168 RVA: 0x00026618 File Offset: 0x00024818
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoUnicred();
				if (carteira == 21)
				{
					this.NumeroCarteira = 21;
					this.Codigo = "21";
					this.Tipo = "";
					this.Descricao = "";
				}
				else
				{
					this.NumeroCarteira = 21;
					this.Codigo = "21";
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

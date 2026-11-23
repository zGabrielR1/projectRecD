using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000129 RID: 297
	public class CarteiraAilos : AbstractCarteira
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00024500 File Offset: 0x00022700
		public CarteiraAilos(int carteira)
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

		// Token: 0x06000455 RID: 1109 RVA: 0x0002453C File Offset: 0x0002273C
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoAilos();
				if (carteira == 1)
				{
					this.NumeroCarteira = 1;
					this.Codigo = "CS";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples com Registro";
				}
				else
				{
					this.NumeroCarteira = 1;
					this.Codigo = "CS";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples com Registro";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

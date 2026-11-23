using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200013F RID: 319
	internal sealed class CarteiraHsbc : AbstractCarteira
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x000255CC File Offset: 0x000237CC
		public CarteiraHsbc()
		{
			try
			{
				this.Banco = new Banco(399);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00025610 File Offset: 0x00023810
		public CarteiraHsbc(int carteira)
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

		// Token: 0x06000477 RID: 1143 RVA: 0x0002564C File Offset: 0x0002384C
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoHsbc();
				this.NumeroCarteira = 1;
				this.Codigo = "CSB";
				this.Tipo = "";
				this.Descricao = "CSB Cobrança Simples HSBC";
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000127 RID: 295
	public class CarteiraCredisis : AbstractCarteira
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00024430 File Offset: 0x00022630
		public CarteiraCredisis(int carteira)
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

		// Token: 0x06000453 RID: 1107 RVA: 0x0002446C File Offset: 0x0002266C
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoCredisis();
				if (carteira == 18)
				{
					this.NumeroCarteira = 18;
					this.Codigo = "CS";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples com Registro";
				}
				else
				{
					this.NumeroCarteira = 18;
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

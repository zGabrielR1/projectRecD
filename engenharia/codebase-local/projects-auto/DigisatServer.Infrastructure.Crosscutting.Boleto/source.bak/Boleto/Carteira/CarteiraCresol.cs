using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000133 RID: 307
	public class CarteiraCresol : AbstractCarteira
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x00024BE8 File Offset: 0x00022DE8
		public CarteiraCresol()
		{
			try
			{
				this.Banco = new Banco(133);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00024C2C File Offset: 0x00022E2C
		public CarteiraCresol(int carteira)
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

		// Token: 0x06000464 RID: 1124 RVA: 0x00024C68 File Offset: 0x00022E68
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoCresol();
				if (carteira == 9)
				{
					this.NumeroCarteira = 9;
					this.Codigo = EnumCarteirasCresol.CobrancaComRegistro.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasCresol.CobrancaComRegistro);
				}
				else
				{
					this.NumeroCarteira = 9;
					this.Codigo = "9";
					this.Tipo = "S";
					this.Descricao = "Cobrança com registro";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

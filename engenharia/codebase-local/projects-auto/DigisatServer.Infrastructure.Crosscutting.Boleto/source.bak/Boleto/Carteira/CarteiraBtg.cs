using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200014A RID: 330
	public class CarteiraBtg : AbstractCarteira
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00026100 File Offset: 0x00024300
		public CarteiraBtg()
		{
			try
			{
				this.Banco = new Banco(208);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00026144 File Offset: 0x00024344
		public CarteiraBtg(int carteira)
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

		// Token: 0x06000487 RID: 1159 RVA: 0x00026180 File Offset: 0x00024380
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoBtg();
				switch (carteira)
				{
				case 1:
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples";
					break;
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = "2";
					this.Tipo = "";
					this.Descricao = "Cobrança Vinculada";
					break;
				case 3:
					this.NumeroCarteira = 3;
					this.Codigo = "3";
					this.Tipo = "";
					this.Descricao = "Cobrança Caucionada";
					break;
				case 4:
					this.NumeroCarteira = 4;
					this.Codigo = "4";
					this.Tipo = "";
					this.Descricao = "Cobrança Descontada";
					break;
				case 5:
					this.NumeroCarteira = 5;
					this.Codigo = "5";
					this.Tipo = "";
					this.Descricao = "Cobrança Vendor";
					break;
				case 6:
					this.NumeroCarteira = 6;
					this.Codigo = "6";
					this.Tipo = "";
					this.Descricao = "Cobrança Cessão";
					break;
				default:
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

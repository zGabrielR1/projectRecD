using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200012D RID: 301
	public class CarteiraBankBoston : AbstractCarteira
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x000247E8 File Offset: 0x000229E8
		public CarteiraBankBoston()
		{
			try
			{
				this.Banco = new Banco(479);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0002482C File Offset: 0x00022A2C
		public CarteiraBankBoston(int carteira)
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

		// Token: 0x0600045B RID: 1115 RVA: 0x00024868 File Offset: 0x00022A68
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoBankBoston();
				switch (carteira)
				{
				case 1:
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "1";
					this.Descricao = "Simples";
					break;
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = "2";
					this.Tipo = "2";
					this.Descricao = "Direta";
					break;
				case 3:
					this.NumeroCarteira = 3;
					this.Codigo = "3";
					this.Tipo = "3";
					this.Descricao = "Escritural";
					break;
				case 4:
					this.NumeroCarteira = 4;
					this.Codigo = "4";
					this.Tipo = "4";
					this.Descricao = "Caução";
					break;
				default:
					this.NumeroCarteira = 0;
					this.Codigo = " ";
					this.Tipo = " ";
					this.Descricao = "( Selecione )";
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

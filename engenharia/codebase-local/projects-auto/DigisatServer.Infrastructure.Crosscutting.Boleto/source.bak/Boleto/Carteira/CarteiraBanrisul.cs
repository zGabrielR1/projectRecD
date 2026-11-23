using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000139 RID: 313
	public class CarteiraBanrisul : AbstractCarteira
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00024FF4 File Offset: 0x000231F4
		public CarteiraBanrisul()
		{
			try
			{
				this.Banco = new Banco(41);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00025034 File Offset: 0x00023234
		public CarteiraBanrisul(int carteira)
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

		// Token: 0x0600046D RID: 1133 RVA: 0x00025070 File Offset: 0x00023270
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoBanrisul();
				switch (carteira)
				{
				case 1:
					this.NumeroCarteira = 1;
					this.Codigo = EnumCarteirasBanrisul.CobrancaSimples.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasBanrisul.CobrancaSimples);
					break;
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = EnumCarteirasBanrisul.CobrancaVinculada.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasBanrisul.CobrancaVinculada);
					break;
				case 3:
					this.NumeroCarteira = 3;
					this.Codigo = EnumCarteirasBanrisul.CobrancaCaucionada.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasBanrisul.CobrancaCaucionada);
					break;
				case 4:
					this.NumeroCarteira = 4;
					this.Codigo = EnumCarteirasBanrisul.CobrancaDescontada.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasBanrisul.CobrancaDescontada);
					break;
				default:
					this.NumeroCarteira = 1;
					this.Codigo = "1";
					this.Tipo = "S";
					this.Descricao = "Cobrança simples";
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

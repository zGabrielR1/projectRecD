using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000137 RID: 311
	public class CarteiraNordeste : AbstractCarteira
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00024DF0 File Offset: 0x00022FF0
		public CarteiraNordeste()
		{
			try
			{
				this.Banco = new Banco(4);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00024E30 File Offset: 0x00023030
		public CarteiraNordeste(int carteira)
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

		// Token: 0x0600046A RID: 1130 RVA: 0x00024E6C File Offset: 0x0002306C
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoNordeste();
				switch (carteira)
				{
				case 1:
					this.NumeroCarteira = 1;
					this.Codigo = EnumCarteirasNordeste.CobrancaSimplesEscrituralBoletoEmitidoPeloBanco.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasNordeste.CobrancaSimplesEscrituralBoletoEmitidoPeloBanco);
					break;
				case 2:
					this.NumeroCarteira = 2;
					this.Codigo = EnumCarteirasNordeste.CobrancaVinculadaBoletoEmitidoPeloBanco.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasNordeste.CobrancaVinculadaBoletoEmitidoPeloBanco);
					break;
				case 4:
					this.NumeroCarteira = 4;
					this.Codigo = EnumCarteirasNordeste.CobrancaSimplesBoletoEmitidoPeloCliente.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasNordeste.CobrancaSimplesBoletoEmitidoPeloCliente);
					break;
				case 5:
					this.NumeroCarteira = 5;
					this.Codigo = EnumCarteirasNordeste.CobrancaVinculadaBoletoEmitidoPeloCliente.ToString();
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasNordeste.CobrancaVinculadaBoletoEmitidoPeloCliente);
					break;
				case 6:
					this.NumeroCarteira = 6;
					this.Codigo = "I";
					this.Tipo = "S";
					this.Descricao = Utils.RetornarDescricao(EnumCarteirasNordeste.CobrancaSimplificadaSemRegistro);
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

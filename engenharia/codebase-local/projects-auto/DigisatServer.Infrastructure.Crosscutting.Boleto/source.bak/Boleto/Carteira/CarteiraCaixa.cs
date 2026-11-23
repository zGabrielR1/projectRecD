using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x0200013D RID: 317
	public sealed class CarteiraCaixa : AbstractCarteira
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x000253AC File Offset: 0x000235AC
		public CarteiraCaixa()
		{
			try
			{
				this.Banco = new Banco(104);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000253EC File Offset: 0x000235EC
		public CarteiraCaixa(int carteira)
		{
			try
			{
				this.Carregar(carteira, 0, TipoSolucao.Sicob);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00025428 File Offset: 0x00023628
		public CarteiraCaixa(int carteira, int formaCadastramento, TipoSolucao tipoSolucao)
		{
			try
			{
				this.Carregar(carteira, formaCadastramento, tipoSolucao);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00025464 File Offset: 0x00023664
		private void Carregar(int carteira, int formaCadastramento, TipoSolucao tipoSolucao = TipoSolucao.Sicob)
		{
			try
			{
				this.Banco = new BancoCaixa();
				if (tipoSolucao == TipoSolucao.Sicob)
				{
					switch (carteira)
					{
					case 11:
						this.NumeroCarteira = 11;
						this.Codigo = "CS";
						this.Tipo = "CS";
						this.Descricao = "Cobrança simples";
						goto IL_012D;
					case 12:
						this.NumeroCarteira = 12;
						this.Codigo = "CR";
						this.Tipo = "CR";
						this.Descricao = "Cobrança rápida";
						goto IL_012D;
					case 13:
						break;
					case 14:
						this.NumeroCarteira = 14;
						this.Codigo = "SR";
						this.Tipo = "SR";
						this.Descricao = "Cobrança sem registro";
						goto IL_012D;
					default:
						if (carteira == 41)
						{
							this.NumeroCarteira = 41;
							this.Codigo = "DE";
							this.Tipo = "DE";
							this.Descricao = "Cobrança descontada";
							goto IL_012D;
						}
						break;
					}
					this.NumeroCarteira = 11;
					this.Codigo = "CS";
					this.Tipo = "CS";
					this.Descricao = "Cobrança simples";
				}
				else
				{
					this.Codigo = ((formaCadastramento == 1) ? "RG" : "SR");
				}
				IL_012D:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

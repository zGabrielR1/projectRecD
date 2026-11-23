using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200011B RID: 283
	public class CodigoMovimentoSicredi : AbstractCodigoMovimento
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x000235E8 File Offset: 0x000217E8
		public CodigoMovimentoSicredi(int codigo)
		{
			try
			{
				this.Carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00023624 File Offset: 0x00021824
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoSicredi();
				switch (codigo)
				{
				case 2:
					this.Codigo = 2;
					this.Descricao = "Entrada confirmada";
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = "Entrada rejeitada";
					break;
				case 4:
				case 5:
				case 10:
				case 11:
				case 15:
				case 16:
				case 18:
				case 21:
				case 22:
				case 29:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
					break;
				case 6:
					this.Codigo = 6;
					this.Descricao = "Liquidação";
					break;
				case 7:
					this.Codigo = 7;
					this.Descricao = "Confirmação do recebimento da instrução de desconto";
					break;
				case 8:
					this.Codigo = 8;
					this.Descricao = "Confirmação do recebimento do cancelamento do desconto";
					break;
				case 9:
					this.Codigo = 9;
					this.Descricao = "Baixa";
					break;
				case 12:
					this.Codigo = 12;
					this.Descricao = "Confirmação do recebimento instrução de abatimento";
					break;
				case 13:
					this.Codigo = 13;
					this.Descricao = "Confirmação do recebimento instrução de cancelamento abatimento";
					break;
				case 14:
					this.Codigo = 14;
					this.Descricao = "Confirmação do recebimento instrução alteração de vencimento";
					break;
				case 17:
					this.Codigo = 17;
					this.Descricao = "Liquidação após baixa ou liquidação título não registrado";
					break;
				case 19:
					this.Codigo = 19;
					this.Descricao = "Confirmação do recebimento instrução de protesto";
					break;
				case 20:
					this.Codigo = 20;
					this.Descricao = "Confirmação do recebimento instrução de sustação/cancelamento de protesto";
					break;
				case 23:
					this.Codigo = 23;
					this.Descricao = "Remessa a cartório (aponte em cartório)";
					break;
				case 24:
					this.Codigo = 24;
					this.Descricao = "Retirada de cartório e manutenção em carteira";
					break;
				case 25:
					this.Codigo = 25;
					this.Descricao = "Protestado e baixado (baixa por ter sido protestado)";
					break;
				case 26:
					this.Codigo = 26;
					this.Descricao = "Instrução rejeitada";
					break;
				case 27:
					this.Codigo = 27;
					this.Descricao = "Confirmação do pedido de alteração de outros dados";
					break;
				case 28:
					this.Codigo = 28;
					this.Descricao = "Débito de tarifas custas";
					break;
				case 30:
					this.Codigo = 30;
					this.Descricao = "Alteração de dados rejeitada";
					break;
				case 36:
					this.Codigo = 36;
					this.Descricao = "Baixa rejeitada";
					break;
				default:
					if (codigo != 51)
					{
						if (codigo == 52)
						{
							this.Codigo = 52;
							this.Descricao = "Título DDA não reconhecido pelo sacado";
						}
					}
					else
					{
						this.Codigo = 51;
						this.Descricao = "Título DDA reconhecido pelo sacado";
					}
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

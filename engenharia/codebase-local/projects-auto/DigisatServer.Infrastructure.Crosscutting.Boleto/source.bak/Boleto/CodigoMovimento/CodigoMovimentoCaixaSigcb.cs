using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000111 RID: 273
	public class CodigoMovimentoCaixaSigcb : AbstractCodigoMovimento
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x00021ED0 File Offset: 0x000200D0
		public CodigoMovimentoCaixaSigcb(int codigo)
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

		// Token: 0x0600040E RID: 1038 RVA: 0x00021F0C File Offset: 0x0002010C
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoCaixa();
				switch (codigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = "Solicitação de Impressão de Títulos Confirmada";
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = "Entrada Confirmada";
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = "Entrada Rejeitada";
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = "Transferência de Carteira/Entrada";
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = "Transferência de Carteira/Baixa";
					break;
				case 6:
					this.Codigo = 6;
					this.Descricao = "Liquidação";
					break;
				case 7:
					this.Codigo = 7;
					this.Descricao = "Confirmação do Recebimento da Instrução de Desconto";
					break;
				case 8:
					this.Codigo = 8;
					this.Descricao = "Confirmação do Recebimento do Cancelamento do Desconto";
					break;
				case 9:
					this.Codigo = 9;
					this.Descricao = "Baixa";
					break;
				case 12:
					this.Codigo = 12;
					this.Descricao = "Confirmação Recebimento Instrução de Abatimento";
					break;
				case 13:
					this.Codigo = 13;
					this.Descricao = "Confirmação Recebimento Instrução de Cancelamento Abatimento";
					break;
				case 14:
					this.Codigo = 14;
					this.Descricao = "Confirmação Recebimento Instrução Alteração de Vencimento";
					break;
				case 19:
					this.Codigo = 19;
					this.Descricao = "Confirmação Recebimento Instrução de Protesto";
					break;
				case 20:
					this.Codigo = 20;
					this.Descricao = "Confirmação Recebimento Instrução de Sustação/Cancelamento de Protesto";
					break;
				case 23:
					this.Codigo = 23;
					this.Descricao = "Remessa a Cartório";
					break;
				case 24:
					this.Codigo = 24;
					this.Descricao = "Retirada de Cartório";
					break;
				case 25:
					this.Codigo = 25;
					this.Descricao = "Protestado e Baixado (Baixa por Ter Sido Protestado)";
					break;
				case 26:
					this.Codigo = 26;
					this.Descricao = "Instrução Rejeitada";
					break;
				case 27:
					this.Codigo = 27;
					this.Descricao = "Confirmação do Pedido de Alteração de Outros Dados";
					break;
				case 28:
					this.Codigo = 28;
					this.Descricao = "Débito de Tarifas/Custas";
					break;
				case 30:
					this.Codigo = 30;
					this.Descricao = "Alteração de Dados Rejeitada";
					break;
				case 35:
					this.Codigo = 35;
					this.Descricao = "Confirmação de Inclusão Banco de Sacado";
					break;
				case 36:
					this.Codigo = 36;
					this.Descricao = "Confirmação de Alteração Banco de Sacado";
					break;
				case 37:
					this.Codigo = 37;
					this.Descricao = "Confirmação de Exclusão Banco de Sacado";
					break;
				case 38:
					this.Codigo = 38;
					this.Descricao = "Emissão de Bloquetos de Banco de Sacado";
					break;
				case 39:
					this.Codigo = 39;
					this.Descricao = "Manutenção de Sacado Rejeitada";
					break;
				case 40:
					this.Codigo = 40;
					this.Descricao = "Entrada de Título via Banco de Sacado Rejeitada";
					break;
				case 41:
					this.Codigo = 41;
					this.Descricao = "Manutenção de Banco de Sacado Rejeitada";
					break;
				case 44:
					this.Codigo = 44;
					this.Descricao = "Estorno de Baixa / Liquidação";
					break;
				case 45:
					this.Codigo = 45;
					this.Descricao = "Alteração de Dados";
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

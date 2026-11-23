using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200010D RID: 269
	public class CodigoMovimentoBancoBrasil : AbstractCodigoMovimento
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0002180C File Offset: 0x0001FA0C
		public CodigoMovimentoBancoBrasil(int codigo)
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

		// Token: 0x0600040A RID: 1034 RVA: 0x00021848 File Offset: 0x0001FA48
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoBrasil();
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
					this.Codigo = 4;
					this.Descricao = "Transferência de carteira/entrada";
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = "Transferência de carteira/baixa";
					break;
				case 6:
					this.Codigo = 6;
					this.Descricao = "Liquidação normal";
					break;
				case 9:
					this.Codigo = 9;
					this.Descricao = "Baixa";
					break;
				case 11:
					this.Codigo = 11;
					this.Descricao = "Títulos em carteira em ser";
					break;
				case 12:
					this.Codigo = 12;
					this.Descricao = "Confirmação recebimento instrução de abatimento";
					break;
				case 13:
					this.Codigo = 13;
					this.Descricao = "Confirmação recebimento instrução de cancelamento de abatimento";
					break;
				case 14:
					this.Codigo = 14;
					this.Descricao = "Confirmação recebimento instrução alteração de vencimento";
					break;
				case 15:
					this.Codigo = 15;
					this.Descricao = "Franco pagamento";
					break;
				case 17:
					this.Codigo = 17;
					this.Descricao = "Liquidação após baixa";
					break;
				case 19:
					this.Codigo = 19;
					this.Descricao = "Confirmação de recebimento de instrução de protesto";
					break;
				case 20:
					this.Codigo = 20;
					this.Descricao = "Confirmação de recebimento de instrução de sustação de protesto";
					break;
				case 23:
					this.Codigo = 23;
					this.Descricao = "Remessa a cartório/aponte em cartório";
					break;
				case 24:
					this.Codigo = 24;
					this.Descricao = "Retirada de cartório e manutenção em carteira";
					break;
				case 25:
					this.Codigo = 25;
					this.Descricao = "Protestado e baixado/baixa por ter sido protestado";
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
					this.Descricao = "Debito de tarifas/custas";
					break;
				case 29:
					this.Codigo = 29;
					this.Descricao = "Ocorrencias do sacado";
					break;
				case 30:
					this.Codigo = 30;
					this.Descricao = "Alteração de dados rejeitada";
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

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000113 RID: 275
	public class CodigoMovimentoSantander : AbstractCodigoMovimento
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x000222C4 File Offset: 0x000204C4
		public CodigoMovimentoSantander(int codigo)
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

		// Token: 0x06000410 RID: 1040 RVA: 0x00022300 File Offset: 0x00020500
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoSantander();
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
				case 7:
				case 8:
				case 10:
				case 15:
				case 16:
				case 18:
				case 21:
				case 22:
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
				default:
					switch (codigo)
					{
					case 51:
						this.Codigo = 51;
						this.Descricao = "Título DDA reconhecido pelo sacado";
						break;
					case 52:
						this.Codigo = 52;
						this.Descricao = "Título DDA não reconhecido pelo sacado";
						break;
					case 53:
						this.Codigo = 53;
						this.Descricao = "Título DDA recusado pela CIP";
						break;
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

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000119 RID: 281
	public class CodigoMovimentoDaycoval : AbstractCodigoMovimento
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00023278 File Offset: 0x00021478
		public CodigoMovimentoDaycoval(int codigo)
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

		// Token: 0x06000416 RID: 1046 RVA: 0x000232B4 File Offset: 0x000214B4
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoDaycoval();
				if (codigo <= 40)
				{
					switch (codigo)
					{
					case 1:
						this.Codigo = 1;
						this.Descricao = "Entrada confirmada na CIP";
						break;
					case 2:
						this.Codigo = 2;
						this.Descricao = "Entrada confirmada";
						break;
					case 3:
						this.Codigo = 3;
						this.Descricao = "Entrada rejeitada";
						break;
					case 4:
					case 7:
					case 11:
					case 17:
					case 18:
					case 21:
					case 25:
					case 26:
					case 27:
						break;
					case 5:
						this.Codigo = 5;
						this.Descricao = "Campo Livre Alterado";
						break;
					case 6:
						this.Codigo = 6;
						this.Descricao = "Liquidação Normal";
						break;
					case 8:
						this.Codigo = 8;
						this.Descricao = "Liquidação em Cartório";
						break;
					case 9:
						this.Codigo = 9;
						this.Descricao = "Baixa Automática";
						break;
					case 10:
						this.Codigo = 10;
						this.Descricao = "Baixa por ter sido liquidado";
						break;
					case 12:
						this.Codigo = 12;
						this.Descricao = "Confirma Abatimento";
						break;
					case 13:
						this.Codigo = 13;
						this.Descricao = "Abatimento Cancelado";
						break;
					case 14:
						this.Codigo = 14;
						this.Descricao = "Vencimento Alterado";
						break;
					case 15:
						this.Codigo = 15;
						this.Descricao = "Baixa Rejeitada";
						break;
					case 16:
						this.Codigo = 16;
						this.Descricao = "Instrução rejeitada";
						break;
					case 19:
						this.Codigo = 19;
						this.Descricao = "Confirma Recebimento de Ordem de Protesto";
						break;
					case 20:
						this.Codigo = 20;
						this.Descricao = "Confirma Recebimento de Ordem de Sustação";
						break;
					case 22:
						this.Codigo = 22;
						this.Descricao = "Seu Número Alterado";
						break;
					case 23:
						this.Codigo = 23;
						this.Descricao = "Título enviado para Cartório";
						break;
					case 24:
						this.Codigo = 24;
						this.Descricao = "Confirma recebimento de ordem de não protestar";
						break;
					case 28:
						this.Codigo = 28;
						this.Descricao = "Débito de tarifas/custas – Correspondentes";
						break;
					default:
						if (codigo == 40)
						{
							this.Codigo = 40;
							this.Descricao = "Tarifa de entrada (debitada na liquidação)";
						}
						break;
					}
				}
				else if (codigo != 43)
				{
					switch (codigo)
					{
					case 96:
						this.Codigo = 96;
						this.Descricao = "Tarifa sobre instruções – Mês anterior";
						break;
					case 97:
						this.Codigo = 97;
						this.Descricao = "Tarifa sobre baixas – Mês anterior";
						break;
					case 98:
						this.Codigo = 98;
						this.Descricao = "Tarifa sobre entradas – Mês anterior";
						break;
					case 99:
						this.Codigo = 99;
						this.Descricao = "Tarifa sobre instrução de protesto/sustação – mês anterior";
						break;
					}
				}
				else
				{
					this.Codigo = 43;
					this.Descricao = "Baixado por ter sido protestado";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

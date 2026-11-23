using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000115 RID: 277
	public class CodigoMovimentoBtg : AbstractCodigoMovimento
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00022600 File Offset: 0x00020800
		public CodigoMovimentoBtg(int codigo)
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

		// Token: 0x06000412 RID: 1042 RVA: 0x0002263C File Offset: 0x0002083C
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoBtg();
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
					this.Descricao = "Franco de Pagamento";
					break;
				case 17:
					this.Codigo = 17;
					this.Descricao = "Liquidação Após Baixa ou Liquidação Título Não Registrado";
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
					this.Descricao = "Ocorrências do Pagador";
					break;
				case 30:
					this.Codigo = 30;
					this.Descricao = "Alteração de dados rejeitada";
					break;
				case 33:
					this.Codigo = 33;
					this.Descricao = "Confirmação da Alteração dos Dados do Rateio de Crédito";
					break;
				case 34:
					this.Codigo = 34;
					this.Descricao = "Confirmação do Cancelamento dos Dados do Rateio de Crédito";
					break;
				case 35:
					this.Codigo = 35;
					this.Descricao = "Confirmação do Desagendamento do Débito Automático";
					break;
				case 36:
					this.Codigo = 36;
					this.Descricao = "Confirmação de envio de e-mail/SMS";
					break;
				case 37:
					this.Codigo = 37;
					this.Descricao = "Envio de e-mail/SMS rejeitado";
					break;
				case 38:
					this.Codigo = 38;
					this.Descricao = "Confirmação de alteração do Prazo Limite de Recebimento";
					break;
				case 39:
					this.Codigo = 39;
					this.Descricao = "Confirmação de Dispensa de Prazo Limite de Recebimento";
					break;
				case 40:
					this.Codigo = 40;
					this.Descricao = "Confirmação da alteração do número do título dado pelo Beneficiário";
					break;
				case 41:
					this.Codigo = 41;
					this.Descricao = "Confirmação da alteração do número controle do Participante";
					break;
				case 42:
					this.Codigo = 42;
					this.Descricao = "Confirmação da alteração dos dados do Pagador";
					break;
				case 43:
					this.Codigo = 43;
					this.Descricao = "Confirmação da alteração dos dados do Pagador/Avalista";
					break;
				case 44:
					this.Codigo = 44;
					this.Descricao = "Título pago com cheque devolvido";
					break;
				case 45:
					this.Codigo = 45;
					this.Descricao = "Título pago com cheque compensado";
					break;
				case 46:
					this.Codigo = 46;
					this.Descricao = "Instrução para cancelar protesto confirmada";
					break;
				case 47:
					this.Codigo = 47;
					this.Descricao = "Instrução para protesto para fins falimentares confirmada";
					break;
				case 48:
					this.Codigo = 48;
					this.Descricao = "Confirmação de instrução de transferência de carteira/modalidade de cobrança";
					break;
				case 49:
					this.Codigo = 49;
					this.Descricao = "Alteração de contrato de cobrança";
					break;
				case 50:
					this.Codigo = 50;
					this.Descricao = "Título pago com cheque pendente de liquidação";
					break;
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
				case 54:
					this.Codigo = 54;
					this.Descricao = "Confirmação da Instrução de Baixa de Título Negativado sem Protesto";
					break;
				case 55:
					this.Codigo = 55;
					this.Descricao = "Confirmação de Pedido de Dispensa de Multa";
					break;
				case 56:
					this.Codigo = 56;
					this.Descricao = "Confirmação do Pedido de Cobrança de Multa";
					break;
				case 57:
					this.Codigo = 57;
					this.Descricao = "Confirmação do Pedido de Alteração de Cobrança de Juros";
					break;
				case 58:
					this.Codigo = 58;
					this.Descricao = "Confirmação do Pedido de Alteração do Valor/Data de Desconto";
					break;
				case 59:
					this.Codigo = 59;
					this.Descricao = "Confirmação do Pedido de Alteração do Beneficiário do Título";
					break;
				case 60:
					this.Codigo = 60;
					this.Descricao = "Confirmação do Pedido de Dispensa de Juros de Mora";
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

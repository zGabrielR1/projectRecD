using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200010C RID: 268
	public enum EnumCodigoMovimentoBancoBrasil
	{
		// Token: 0x0400044A RID: 1098
		EntradaConfirmada = 2,
		// Token: 0x0400044B RID: 1099
		EntradaRejeitada,
		// Token: 0x0400044C RID: 1100
		TransferenciaCarteiraEntrada,
		// Token: 0x0400044D RID: 1101
		TransferenciaCarteiraBaixa,
		// Token: 0x0400044E RID: 1102
		Liquidacao,
		// Token: 0x0400044F RID: 1103
		Baixa = 9,
		// Token: 0x04000450 RID: 1104
		TitulosCarteiraEmSer = 11,
		// Token: 0x04000451 RID: 1105
		ConfirmacaoRecebimentoInstrucaoAbatimento,
		// Token: 0x04000452 RID: 1106
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x04000453 RID: 1107
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x04000454 RID: 1108
		FrancoPagamento,
		// Token: 0x04000455 RID: 1109
		LiquidacaoAposBaixa = 17,
		// Token: 0x04000456 RID: 1110
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x04000457 RID: 1111
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x04000458 RID: 1112
		RemessaCartorio = 23,
		// Token: 0x04000459 RID: 1113
		RetiradaCartorioManutencaoCarteira,
		// Token: 0x0400045A RID: 1114
		ProtestadoBaixado,
		// Token: 0x0400045B RID: 1115
		InstrucaoRejeitada,
		// Token: 0x0400045C RID: 1116
		ConfirmaçãoPedidoAlteracaoOutrosDados,
		// Token: 0x0400045D RID: 1117
		DebitoTarifas,
		// Token: 0x0400045E RID: 1118
		OcorrenciaSacado,
		// Token: 0x0400045F RID: 1119
		AlteracaoDadosRejeitada,
		// Token: 0x04000460 RID: 1120
		TituloPagoComChequeDevolvido = 44,
		// Token: 0x04000461 RID: 1121
		TituloPagoComChequePendenteDeCompensacao = 50
	}
}

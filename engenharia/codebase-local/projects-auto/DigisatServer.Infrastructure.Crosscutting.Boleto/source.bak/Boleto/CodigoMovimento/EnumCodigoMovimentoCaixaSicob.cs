using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200010E RID: 270
	public enum EnumCodigoMovimentoCaixaSicob
	{
		// Token: 0x04000463 RID: 1123
		EntradaConfirmada = 2,
		// Token: 0x04000464 RID: 1124
		EntradaRejeitada,
		// Token: 0x04000465 RID: 1125
		TransferenciaCarteiraEntrada,
		// Token: 0x04000466 RID: 1126
		TransferenciaCarteiraBaixa,
		// Token: 0x04000467 RID: 1127
		Liquidacao,
		// Token: 0x04000468 RID: 1128
		Baixa = 9,
		// Token: 0x04000469 RID: 1129
		ConfirmacaoRecebimentoInstrucaoAbatimento = 12,
		// Token: 0x0400046A RID: 1130
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x0400046B RID: 1131
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x0400046C RID: 1132
		LiquidacaoAposBaixaLiquidacaoTituloNaoRegistrado = 17,
		// Token: 0x0400046D RID: 1133
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x0400046E RID: 1134
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x0400046F RID: 1135
		RemessaCartorio = 23,
		// Token: 0x04000470 RID: 1136
		RetiradaCartorio,
		// Token: 0x04000471 RID: 1137
		ProtestadoBaixado,
		// Token: 0x04000472 RID: 1138
		InstrucaoRejeitada,
		// Token: 0x04000473 RID: 1139
		ConfirmacaoPedidoAlteracaoOutrosDados,
		// Token: 0x04000474 RID: 1140
		DebitoTarifas,
		// Token: 0x04000475 RID: 1141
		AlteracaoDadosRejeitada = 30,
		// Token: 0x04000476 RID: 1142
		ConfirmacaoEnvioEmailSms = 36,
		// Token: 0x04000477 RID: 1143
		EnvioEmailSmsRejeitado,
		// Token: 0x04000478 RID: 1144
		EstornoProtestoSustacao = 43,
		// Token: 0x04000479 RID: 1145
		EstornoBaixaLiquidacao,
		// Token: 0x0400047A RID: 1146
		AlteracaoDados,
		// Token: 0x0400047B RID: 1147
		TituloDdaReconhecidoPeloSacado = 51,
		// Token: 0x0400047C RID: 1148
		TituloDdaNaoReconhecidoPeloSacado,
		// Token: 0x0400047D RID: 1149
		TituloDdaRecusadoPelaCip
	}
}

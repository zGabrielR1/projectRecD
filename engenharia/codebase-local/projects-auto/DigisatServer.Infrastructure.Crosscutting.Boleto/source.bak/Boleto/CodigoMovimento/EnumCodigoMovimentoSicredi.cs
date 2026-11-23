using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200011A RID: 282
	public enum EnumCodigoMovimentoSicredi
	{
		// Token: 0x0400053B RID: 1339
		EntradaConfirmada = 2,
		// Token: 0x0400053C RID: 1340
		EntradaRejeitada,
		// Token: 0x0400053D RID: 1341
		Liquidacao = 6,
		// Token: 0x0400053E RID: 1342
		ConfirmacaoRecebimentoInstrucaoDesconto,
		// Token: 0x0400053F RID: 1343
		ConfirmacaoRecebimentoCancelamentoDesconto,
		// Token: 0x04000540 RID: 1344
		Baixa,
		// Token: 0x04000541 RID: 1345
		ConfirmacaoRecebimentoInstrucaoAbatimento = 12,
		// Token: 0x04000542 RID: 1346
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x04000543 RID: 1347
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x04000544 RID: 1348
		LiquidacaoAposBaixa = 17,
		// Token: 0x04000545 RID: 1349
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x04000546 RID: 1350
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x04000547 RID: 1351
		RemessaCartorio = 23,
		// Token: 0x04000548 RID: 1352
		RetiradaCartorioManutencaoCarteira,
		// Token: 0x04000549 RID: 1353
		ProtestadoBaixado,
		// Token: 0x0400054A RID: 1354
		InstrucaoRejeitada,
		// Token: 0x0400054B RID: 1355
		ConfirmacaoPedidoAlteracaoOutrosDados,
		// Token: 0x0400054C RID: 1356
		DebitoTarifas,
		// Token: 0x0400054D RID: 1357
		AlteracaoDadosRejeitada = 30,
		// Token: 0x0400054E RID: 1358
		BaixaRejeitada = 36,
		// Token: 0x0400054F RID: 1359
		TituloDdaReconhecidoPeloSacado = 51,
		// Token: 0x04000550 RID: 1360
		TituloDdaNaoReconhecidoPeloSacado
	}
}

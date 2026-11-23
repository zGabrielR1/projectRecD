using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000116 RID: 278
	public enum EnumCodigoMovimentoSicoob
	{
		// Token: 0x040004EC RID: 1260
		EntradaConfirmada = 2,
		// Token: 0x040004ED RID: 1261
		EntradaRejeitada,
		// Token: 0x040004EE RID: 1262
		TransferenciaCarteiraEntrada,
		// Token: 0x040004EF RID: 1263
		TransferenciaCarteiraBaixa,
		// Token: 0x040004F0 RID: 1264
		Liquidacao,
		// Token: 0x040004F1 RID: 1265
		ConfirmacaoDoRecebimentoDaInstrucaoDeDesconto,
		// Token: 0x040004F2 RID: 1266
		ConfirmacaoDoRecebimentoDoCancelamentoDoDesconto,
		// Token: 0x040004F3 RID: 1267
		Baixa,
		// Token: 0x040004F4 RID: 1268
		TitulosCarteiraEmSer = 11,
		// Token: 0x040004F5 RID: 1269
		ConfirmacaoRecebimentoInstrucaoAbatimento,
		// Token: 0x040004F6 RID: 1270
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x040004F7 RID: 1271
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x040004F8 RID: 1272
		FrancoDePagamento,
		// Token: 0x040004F9 RID: 1273
		LiquidacaoAposBaixaOuLiquidacaoTituloNaoRegistrado = 17,
		// Token: 0x040004FA RID: 1274
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x040004FB RID: 1275
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x040004FC RID: 1276
		RemessaCartorio = 23,
		// Token: 0x040004FD RID: 1277
		RetiradaCartorioManutencaoCarteira,
		// Token: 0x040004FE RID: 1278
		ProtestadoBaixado,
		// Token: 0x040004FF RID: 1279
		InstrucaoRejeitada,
		// Token: 0x04000500 RID: 1280
		ConfirmacaoPedidoAlteracaoOutrosDados,
		// Token: 0x04000501 RID: 1281
		DebitoTarifasCustas,
		// Token: 0x04000502 RID: 1282
		OcorrenciaDoPagador,
		// Token: 0x04000503 RID: 1283
		AlteracaoDadosRejeitada,
		// Token: 0x04000504 RID: 1284
		ConfirmacaoDaAlteracaoDosDadosDoRateioDeCredito = 33,
		// Token: 0x04000505 RID: 1285
		ConfirmacaoDoCancelamentoDosDadosDoRateioDeCredito,
		// Token: 0x04000506 RID: 1286
		ConfirmacaoDoDesagendamentoDoDebitoAutomatico,
		// Token: 0x04000507 RID: 1287
		ConfirmacaoDeEnvioDeEmailSms,
		// Token: 0x04000508 RID: 1288
		EnvioDeEmailSmsRejeitado,
		// Token: 0x04000509 RID: 1289
		ConfirmacaoDeAlteracaoDoPrazoLimiteDeRecebimento,
		// Token: 0x0400050A RID: 1290
		ConfirmacaoDeDispensaDePrazoLimiteDeRecebimento,
		// Token: 0x0400050B RID: 1291
		ConfirmacaoDaAlteracaoDoNúmeroDoTituloDadoPeloBeneficiario,
		// Token: 0x0400050C RID: 1292
		ConfirmacaoDaAlteracaoDoNúmeroControleDoParticipante,
		// Token: 0x0400050D RID: 1293
		ConfirmacaoDaAlteracaoDosDadosDoPagador,
		// Token: 0x0400050E RID: 1294
		ConfirmacaoDaAlteracaoDosDadosDoPagadorAvalista,
		// Token: 0x0400050F RID: 1295
		TituloPagoComChequeDevolvido,
		// Token: 0x04000510 RID: 1296
		TituloPagoComChequeCompensado,
		// Token: 0x04000511 RID: 1297
		InstrucaoParaCancelarProtestoConfirmada,
		// Token: 0x04000512 RID: 1298
		InstrucaoParaProtestoParaFinsFalimentaresConfirmada,
		// Token: 0x04000513 RID: 1299
		ConfirmacaoDeInstrucaoDeTransferenciaDeCarteiraModalidadeDeCobranca,
		// Token: 0x04000514 RID: 1300
		AlteracaoDeContratoDeCobranca,
		// Token: 0x04000515 RID: 1301
		TituloPagoComChequePendenteDeLiquidacao,
		// Token: 0x04000516 RID: 1302
		TituloDdaReconhecidoPeloSacado,
		// Token: 0x04000517 RID: 1303
		TituloDdaNaoReconhecidoPeloSacado,
		// Token: 0x04000518 RID: 1304
		TituloDdaRecusadoPelaCip,
		// Token: 0x04000519 RID: 1305
		ConfirmacaoDaInstrucaoDeBaixaDeTituloNegativadoSemProtesto,
		// Token: 0x0400051A RID: 1306
		ConfirmacaoDePedidoDeDispensaDeMulta,
		// Token: 0x0400051B RID: 1307
		ConfirmacaoDoPedidoDeCobrancaDeMulta,
		// Token: 0x0400051C RID: 1308
		ConfirmacaoDoPedidoDeAlteracaoDeCobrancaDeJuros,
		// Token: 0x0400051D RID: 1309
		ConfirmacaoDoPedidoDeAlteracaoDoValorDataDeDesconto,
		// Token: 0x0400051E RID: 1310
		ConfirmacaoDoPedidoDeAlteracaoDoBeneficiarioDoTitulo,
		// Token: 0x0400051F RID: 1311
		ConfirmacaoDoPedidoDeDispensaDeJurosDeMora
	}
}

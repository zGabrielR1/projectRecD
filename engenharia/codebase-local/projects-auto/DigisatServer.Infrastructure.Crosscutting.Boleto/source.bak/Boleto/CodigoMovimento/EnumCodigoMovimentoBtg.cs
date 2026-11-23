using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000114 RID: 276
	public enum EnumCodigoMovimentoBtg
	{
		// Token: 0x040004B7 RID: 1207
		EntradaConfirmada = 2,
		// Token: 0x040004B8 RID: 1208
		EntradaRejeitada,
		// Token: 0x040004B9 RID: 1209
		TransferenciaCarteiraEntrada,
		// Token: 0x040004BA RID: 1210
		TransferenciaCarteiraBaixa,
		// Token: 0x040004BB RID: 1211
		Liquidacao,
		// Token: 0x040004BC RID: 1212
		ConfirmacaoDoRecebimentoDaInstrucaoDeDesconto,
		// Token: 0x040004BD RID: 1213
		ConfirmacaoDoRecebimentoDoCancelamentoDoDesconto,
		// Token: 0x040004BE RID: 1214
		Baixa,
		// Token: 0x040004BF RID: 1215
		TitulosCarteiraEmSer = 11,
		// Token: 0x040004C0 RID: 1216
		ConfirmacaoRecebimentoInstrucaoAbatimento,
		// Token: 0x040004C1 RID: 1217
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x040004C2 RID: 1218
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x040004C3 RID: 1219
		FrancoDePagamento,
		// Token: 0x040004C4 RID: 1220
		LiquidacaoAposBaixaOuLiquidacaoTituloNaoRegistrado = 17,
		// Token: 0x040004C5 RID: 1221
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x040004C6 RID: 1222
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x040004C7 RID: 1223
		RemessaCartorio = 23,
		// Token: 0x040004C8 RID: 1224
		RetiradaCartorioManutencaoCarteira,
		// Token: 0x040004C9 RID: 1225
		ProtestadoBaixado,
		// Token: 0x040004CA RID: 1226
		InstrucaoRejeitada,
		// Token: 0x040004CB RID: 1227
		ConfirmacaoPedidoAlteracaoOutrosDados,
		// Token: 0x040004CC RID: 1228
		DebitoTarifasCustas,
		// Token: 0x040004CD RID: 1229
		OcorrenciaDoPagador,
		// Token: 0x040004CE RID: 1230
		AlteracaoDadosRejeitada,
		// Token: 0x040004CF RID: 1231
		ConfirmacaoDaAlteracaoDosDadosDoRateioDeCredito = 33,
		// Token: 0x040004D0 RID: 1232
		ConfirmacaoDoCancelamentoDosDadosDoRateioDeCredito,
		// Token: 0x040004D1 RID: 1233
		ConfirmacaoDoDesagendamentoDoDebitoAutomatico,
		// Token: 0x040004D2 RID: 1234
		ConfirmacaoDeEnvioDeEmailSms,
		// Token: 0x040004D3 RID: 1235
		EnvioDeEmailSmsRejeitado,
		// Token: 0x040004D4 RID: 1236
		ConfirmacaoDeAlteracaoDoPrazoLimiteDeRecebimento,
		// Token: 0x040004D5 RID: 1237
		ConfirmacaoDeDispensaDePrazoLimiteDeRecebimento,
		// Token: 0x040004D6 RID: 1238
		ConfirmacaoDaAlteracaoDoNúmeroDoTituloDadoPeloBeneficiario,
		// Token: 0x040004D7 RID: 1239
		ConfirmacaoDaAlteracaoDoNúmeroControleDoParticipante,
		// Token: 0x040004D8 RID: 1240
		ConfirmacaoDaAlteracaoDosDadosDoPagador,
		// Token: 0x040004D9 RID: 1241
		ConfirmacaoDaAlteracaoDosDadosDoPagadorAvalista,
		// Token: 0x040004DA RID: 1242
		TituloPagoComChequeDevolvido,
		// Token: 0x040004DB RID: 1243
		TituloPagoComChequeCompensado,
		// Token: 0x040004DC RID: 1244
		InstrucaoParaCancelarProtestoConfirmada,
		// Token: 0x040004DD RID: 1245
		InstrucaoParaProtestoParaFinsFalimentaresConfirmada,
		// Token: 0x040004DE RID: 1246
		ConfirmacaoDeInstrucaoDeTransferenciaDeCarteiraModalidadeDeCobranca,
		// Token: 0x040004DF RID: 1247
		AlteracaoDeContratoDeCobranca,
		// Token: 0x040004E0 RID: 1248
		TituloPagoComChequePendenteDeLiquidacao,
		// Token: 0x040004E1 RID: 1249
		TituloDdaReconhecidoPeloSacado,
		// Token: 0x040004E2 RID: 1250
		TituloDdaNaoReconhecidoPeloSacado,
		// Token: 0x040004E3 RID: 1251
		TituloDdaRecusadoPelaCip,
		// Token: 0x040004E4 RID: 1252
		ConfirmacaoDaInstrucaoDeBaixaDeTituloNegativadoSemProtesto,
		// Token: 0x040004E5 RID: 1253
		ConfirmacaoDePedidoDeDispensaDeMulta,
		// Token: 0x040004E6 RID: 1254
		ConfirmacaoDoPedidoDeCobrancaDeMulta,
		// Token: 0x040004E7 RID: 1255
		ConfirmacaoDoPedidoDeAlteracaoDeCobrancaDeJuros,
		// Token: 0x040004E8 RID: 1256
		ConfirmacaoDoPedidoDeAlteracaoDoValorDataDeDesconto,
		// Token: 0x040004E9 RID: 1257
		ConfirmacaoDoPedidoDeAlteracaoDoBeneficiarioDoTitulo,
		// Token: 0x040004EA RID: 1258
		ConfirmacaoDoPedidoDeDispensaDeJurosDeMora
	}
}

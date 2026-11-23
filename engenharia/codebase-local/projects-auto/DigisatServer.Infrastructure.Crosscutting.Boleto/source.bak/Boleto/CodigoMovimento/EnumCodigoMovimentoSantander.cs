using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000112 RID: 274
	public enum EnumCodigoMovimentoSantander
	{
		// Token: 0x0400049E RID: 1182
		EntradaConfirmada = 2,
		// Token: 0x0400049F RID: 1183
		EntradaRejeitada,
		// Token: 0x040004A0 RID: 1184
		TransferenciaCarteiraEntrada,
		// Token: 0x040004A1 RID: 1185
		TransferenciaCarteiraBaixa,
		// Token: 0x040004A2 RID: 1186
		Liquidacao,
		// Token: 0x040004A3 RID: 1187
		Baixa = 9,
		// Token: 0x040004A4 RID: 1188
		TitulosCarteiraEmSer = 11,
		// Token: 0x040004A5 RID: 1189
		ConfirmacaoRecebimentoInstrucaoAbatimento,
		// Token: 0x040004A6 RID: 1190
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x040004A7 RID: 1191
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x040004A8 RID: 1192
		LiquidacaoAposBaixa = 17,
		// Token: 0x040004A9 RID: 1193
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x040004AA RID: 1194
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x040004AB RID: 1195
		RemessaCartorio = 23,
		// Token: 0x040004AC RID: 1196
		RetiradaCartorioManutencaoCarteira,
		// Token: 0x040004AD RID: 1197
		ProtestadoBaixado,
		// Token: 0x040004AE RID: 1198
		InstrucaoRejeitada,
		// Token: 0x040004AF RID: 1199
		ConfirmaçãoPedidoAlteracaoOutrosDados,
		// Token: 0x040004B0 RID: 1200
		DebitoTarifas,
		// Token: 0x040004B1 RID: 1201
		OcorrenciaSacado,
		// Token: 0x040004B2 RID: 1202
		AlteracaoDadosRejeitada,
		// Token: 0x040004B3 RID: 1203
		TituloDdaReconhecidoPeloSacado = 51,
		// Token: 0x040004B4 RID: 1204
		TituloDdaNaoReconhecidoPeloSacado,
		// Token: 0x040004B5 RID: 1205
		TituloDdaRecusadoPelaCip
	}
}

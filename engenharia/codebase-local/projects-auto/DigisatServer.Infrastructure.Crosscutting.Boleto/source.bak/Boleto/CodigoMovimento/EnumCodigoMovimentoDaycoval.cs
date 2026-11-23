using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000118 RID: 280
	public enum EnumCodigoMovimentoDaycoval
	{
		// Token: 0x04000521 RID: 1313
		EntradaConfirmadaNaCip = 1,
		// Token: 0x04000522 RID: 1314
		EntradaConfirmada,
		// Token: 0x04000523 RID: 1315
		EntradaRejeitada,
		// Token: 0x04000524 RID: 1316
		CampoLivreAlterado = 5,
		// Token: 0x04000525 RID: 1317
		LiquidacaoNormal,
		// Token: 0x04000526 RID: 1318
		LiquidacaoEmCartorio = 8,
		// Token: 0x04000527 RID: 1319
		BaixaAutomatica,
		// Token: 0x04000528 RID: 1320
		BaixaPorTerSidoLoquidado,
		// Token: 0x04000529 RID: 1321
		ConfirmaAbatimento = 12,
		// Token: 0x0400052A RID: 1322
		AbatimentoCancelado,
		// Token: 0x0400052B RID: 1323
		VencimentoAlterado,
		// Token: 0x0400052C RID: 1324
		BaixaRejeitada,
		// Token: 0x0400052D RID: 1325
		InstrucaoRejeitada,
		// Token: 0x0400052E RID: 1326
		ConfirmacaoRecebimentoOrdemProtesto = 19,
		// Token: 0x0400052F RID: 1327
		ConfirmacaoRecebimentoOrdemSustacao,
		// Token: 0x04000530 RID: 1328
		SeuNumeroAlterado = 22,
		// Token: 0x04000531 RID: 1329
		TituloEnviadoCartorio,
		// Token: 0x04000532 RID: 1330
		ConfirmacaoRecebimentoOrdemNaoProtestar,
		// Token: 0x04000533 RID: 1331
		DebitoTarifas = 28,
		// Token: 0x04000534 RID: 1332
		TarifasEntradaDebitadaNaLiquidacao = 40,
		// Token: 0x04000535 RID: 1333
		BaixadoPorTerSidoProtestado = 43,
		// Token: 0x04000536 RID: 1334
		TarifaSobreInstrucoes = 96,
		// Token: 0x04000537 RID: 1335
		TarifaSobreBaixas,
		// Token: 0x04000538 RID: 1336
		TarifaSobreEntradas,
		// Token: 0x04000539 RID: 1337
		TarifaSobreInstrucaoProtestoSustacao
	}
}

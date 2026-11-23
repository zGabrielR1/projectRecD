using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x02000110 RID: 272
	public enum EnumCodigoMovimentoCaixaSigcb
	{
		// Token: 0x0400047F RID: 1151
		SolicitacaoImpressaoTitulosConfirmada = 1,
		// Token: 0x04000480 RID: 1152
		EntradaConfirmada,
		// Token: 0x04000481 RID: 1153
		EntradaRejeitada,
		// Token: 0x04000482 RID: 1154
		TransferenciaCarteiraEntrada,
		// Token: 0x04000483 RID: 1155
		TransferenciaCarteiraBaixa,
		// Token: 0x04000484 RID: 1156
		Liquidacao,
		// Token: 0x04000485 RID: 1157
		ConfirmacaoRecebimentoInstrucaoDesconto,
		// Token: 0x04000486 RID: 1158
		ConfirmacaoRecebimentoCancelamentoDesconto,
		// Token: 0x04000487 RID: 1159
		Baixa,
		// Token: 0x04000488 RID: 1160
		ConfirmacaoRecebimentoInstrucaoAbatimento = 12,
		// Token: 0x04000489 RID: 1161
		ConfirmacaoRecebimentoInstrucaoCancelamentoAbatimento,
		// Token: 0x0400048A RID: 1162
		ConfirmacaoRecebimentoInstrucaoAlteracaoVencimento,
		// Token: 0x0400048B RID: 1163
		ConfirmacaoRecebimentoInstrucaoProtesto = 19,
		// Token: 0x0400048C RID: 1164
		ConfirmacaoRecebimentoInstrucaoSustacaoProtesto,
		// Token: 0x0400048D RID: 1165
		RemessaCartorio = 23,
		// Token: 0x0400048E RID: 1166
		RetiradaCartorio,
		// Token: 0x0400048F RID: 1167
		ProtestadoBaixado,
		// Token: 0x04000490 RID: 1168
		InstrucaoRejeitada,
		// Token: 0x04000491 RID: 1169
		ConfirmacaoPedidoAlteracaoOutrosDados,
		// Token: 0x04000492 RID: 1170
		DebitoTarifas,
		// Token: 0x04000493 RID: 1171
		AlteracaoDadosRejeitada = 30,
		// Token: 0x04000494 RID: 1172
		ConfirmacaoInclusaoBancoSacado = 35,
		// Token: 0x04000495 RID: 1173
		ConfirmacaoAlteracaoBancoSacado,
		// Token: 0x04000496 RID: 1174
		ConfirmacaoExclusaoBancoSacado,
		// Token: 0x04000497 RID: 1175
		EmissaoBloquetosBancoSacado,
		// Token: 0x04000498 RID: 1176
		ManutencaoSacadoRejeitada,
		// Token: 0x04000499 RID: 1177
		EntradaTituloViaBancoSacadoRejeitada,
		// Token: 0x0400049A RID: 1178
		ManutencaoBancoSacadoRejeitada,
		// Token: 0x0400049B RID: 1179
		EstornoBaixaLiquidacao = 44,
		// Token: 0x0400049C RID: 1180
		AlteracaoDados
	}
}

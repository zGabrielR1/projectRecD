using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Enums
{
	// Token: 0x02000023 RID: 35
	internal enum TipoOcorrencia
	{
		// Token: 0x040000BC RID: 188
		RemRegistrar,
		// Token: 0x040000BD RID: 189
		RemBaixar,
		// Token: 0x040000BE RID: 190
		RemDebitarEmConta,
		// Token: 0x040000BF RID: 191
		RemConcederAbatimento,
		// Token: 0x040000C0 RID: 192
		RemCancelarAbatimento,
		// Token: 0x040000C1 RID: 193
		RemConcederDesconto,
		// Token: 0x040000C2 RID: 194
		RemCancelarDesconto,
		// Token: 0x040000C3 RID: 195
		RemAlterarVencimento,
		// Token: 0x040000C4 RID: 196
		RemProtestar,
		// Token: 0x040000C5 RID: 197
		RemCancelarInstrucaoProtesto,
		// Token: 0x040000C6 RID: 198
		RemDispensarJuros,
		// Token: 0x040000C7 RID: 199
		RemAlterarNomeEnderecoSacado,
		// Token: 0x040000C8 RID: 200
		RemAlterarNumeroControle,
		// Token: 0x040000C9 RID: 201
		RemOutrasOcorrencias,
		// Token: 0x040000CA RID: 202
		RetRegistroConfirmado,
		// Token: 0x040000CB RID: 203
		RetRegistroRecusado,
		// Token: 0x040000CC RID: 204
		RetComandoRecusado,
		// Token: 0x040000CD RID: 205
		RetLiquidado,
		// Token: 0x040000CE RID: 206
		RetLiquidadoEmCartorio,
		// Token: 0x040000CF RID: 207
		RetLiquidadoParcialmente,
		// Token: 0x040000D0 RID: 208
		RetLiquidadoSaldoRestante,
		// Token: 0x040000D1 RID: 209
		RetLiquidadoSemRegistro,
		// Token: 0x040000D2 RID: 210
		RetLiquidadoPorConta,
		// Token: 0x040000D3 RID: 211
		RetBaixaSolicitada,
		// Token: 0x040000D4 RID: 212
		RetBaixado,
		// Token: 0x040000D5 RID: 213
		RetBaixadoPorDevolucao,
		// Token: 0x040000D6 RID: 214
		RetBaixadoFrancoPagamento,
		// Token: 0x040000D7 RID: 215
		RetBaixaPorProtesto,
		// Token: 0x040000D8 RID: 216
		RetRecebimentoInstrucaoBaixar,
		// Token: 0x040000D9 RID: 217
		RetBaixaOuLiquidacaoEstornada,
		// Token: 0x040000DA RID: 218
		RetTituloEmSer,
		// Token: 0x040000DB RID: 219
		RetRecebimentoInstrucaoConcederAbatimento,
		// Token: 0x040000DC RID: 220
		RetAbatimentoConcedido,
		// Token: 0x040000DD RID: 221
		RetRecebimentoInstrucaoCancelarAbatimento,
		// Token: 0x040000DE RID: 222
		RetAbatimentoCancelado,
		// Token: 0x040000DF RID: 223
		RetRecebimentoInstrucaoConcederDesconto,
		// Token: 0x040000E0 RID: 224
		RetDescontoConcedido,
		// Token: 0x040000E1 RID: 225
		RetRecebimentoInstrucaoCancelarDesconto,
		// Token: 0x040000E2 RID: 226
		RetDescontoCancelado,
		// Token: 0x040000E3 RID: 227
		RetRecebimentoInstrucaoAlterarDados,
		// Token: 0x040000E4 RID: 228
		RetDadosAlterados,
		// Token: 0x040000E5 RID: 229
		RetRecebimentoInstrucaoAlterarVencimento,
		// Token: 0x040000E6 RID: 230
		RetVencimentoAlterado,
		// Token: 0x040000E7 RID: 231
		RetAlteracaoDadosNovaEntrada,
		// Token: 0x040000E8 RID: 232
		RetAlteracaoDadosBaixa,
		// Token: 0x040000E9 RID: 233
		RetRecebimentoInstrucaoProtestar,
		// Token: 0x040000EA RID: 234
		RetProtestado,
		// Token: 0x040000EB RID: 235
		RetRecebimentoInstrucaoSustarProtesto,
		// Token: 0x040000EC RID: 236
		RetProtestoSustado,
		// Token: 0x040000ED RID: 237
		RetInstrucaoProtestoRejeitadaSustadaOuPendente,
		// Token: 0x040000EE RID: 238
		RetDebitoEmConta,
		// Token: 0x040000EF RID: 239
		RetRecebimentoInstrucaoAlterarNomeSacado,
		// Token: 0x040000F0 RID: 240
		RetNomeSacadoAlterado,
		// Token: 0x040000F1 RID: 241
		RetRecebimentoInstrucaoAlterarEnderecoSacado,
		// Token: 0x040000F2 RID: 242
		RetEnderecoSacadoAlterado,
		// Token: 0x040000F3 RID: 243
		RetEncaminhadoACartorio,
		// Token: 0x040000F4 RID: 244
		RetRetiradoDeCartorio,
		// Token: 0x040000F5 RID: 245
		RetRecebimentoInstrucaoDispensarJuros,
		// Token: 0x040000F6 RID: 246
		RetJurosDispensados,
		// Token: 0x040000F7 RID: 247
		RetManutencaoTituloVencido,
		// Token: 0x040000F8 RID: 248
		RetRecebimentoInstrucaoAlterarTipoCobranca,
		// Token: 0x040000F9 RID: 249
		RetTipoCobrancaAlterado,
		// Token: 0x040000FA RID: 250
		RetDespesasProtesto,
		// Token: 0x040000FB RID: 251
		RetDespesasSustacaoProtesto,
		// Token: 0x040000FC RID: 252
		RetDebitoCustasAntecipadas,
		// Token: 0x040000FD RID: 253
		RetCustasCartorioDistribuidor,
		// Token: 0x040000FE RID: 254
		RetCustasEdital,
		// Token: 0x040000FF RID: 255
		RetProtestoOuSustacaoEstornado,
		// Token: 0x04000100 RID: 256
		RetDebitoTarifas,
		// Token: 0x04000101 RID: 257
		RetAcertoDepositaria,
		// Token: 0x04000102 RID: 258
		RetOutrasOcorrencias
	}
}

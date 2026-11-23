using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200009B RID: 155
	public enum EnumInstrucoesHsbc
	{
		// Token: 0x04000246 RID: 582
		[Descricao("Multa de %% por cento após dia DDMMAA.")]
		MultaDePPPorCentoAposDiaDDMMAA = 15,
		// Token: 0x04000247 RID: 583
		[Descricao("Multa de R$ após DD dias corridos do vencimento.")]
		MultaAposDiasCorridosDoVencimento = 19,
		// Token: 0x04000248 RID: 584
		[Descricao("Cobrar juros só após 07 dias do vencimento.")]
		CobrarJurosSoApos7DiasDoVencimento,
		// Token: 0x04000249 RID: 585
		[Descricao("Multa de R$ após DD dias úteis do vencimento.")]
		MultaAposDiasUteisDoVencimento = 22,
		// Token: 0x0400024A RID: 586
		[Descricao("Não receber após o vencimento.")]
		NaoReceberAposVencimento,
		// Token: 0x0400024B RID: 587
		[Descricao("Multa de R$ após o vencimento.")]
		MultaDeValorAposVencimento,
		// Token: 0x0400024C RID: 588
		[Descricao("Juros só após DDMMAA, cobrar desde o vencimento.")]
		JurosSoAposDataCobrarDesdeOVencimento = 29,
		// Token: 0x0400024D RID: 589
		[Descricao("Conceder abatimento conforme proposto pelo sacado.")]
		ConcederAbatimentoConformePropostoPeloSacado = 34,
		// Token: 0x0400024E RID: 590
		[Descricao("Após vencimento multa de 10 por cento.")]
		AposVencimentoMultaDe10PorCento = 36,
		// Token: 0x0400024F RID: 591
		[Descricao("Conceder desconto mesmo se pago após o vencimento.")]
		ConcederDescontoMesmoSePagoAposVencimento = 40,
		// Token: 0x04000250 RID: 592
		[Descricao("Não receber antes do vencimento - instrução do cedente.")]
		NaoReceberAntesDoVencimentoInstrucaoDoCedente = 42,
		// Token: 0x04000251 RID: 593
		[Descricao("Após vencimento multa de 20% mais mora de 1% ao mês.")]
		AposVencimentoMultaDe20MaisMoraDe1AoMes = 53,
		// Token: 0x04000252 RID: 594
		[Descricao("Não receber antes do vencimento ou 10 dias após.")]
		NaoReceberAntesDoVencimentoOu10DiasApos = 56,
		// Token: 0x04000253 RID: 595
		[Descricao("Abatimento/desconto só com instrução do cedente.")]
		AbatimentoDescontoSoComInstrucaoDoCedente = 65,
		// Token: 0x04000254 RID: 596
		[Descricao("Título sujeito a protesto após o vencimento.")]
		TituloSujeitoAProtestoAposVencimento = 67,
		// Token: 0x04000255 RID: 597
		[Descricao("Após o vencimento multa de 2 por cento.")]
		AposVencimentoMultaDe2PorCento,
		// Token: 0x04000256 RID: 598
		[Descricao("Não receber após DD dias corridos do vencimento.")]
		NaoReceberAposDDDiasCorridosDoVencimento = 71,
		// Token: 0x04000257 RID: 599
		[Descricao("Não receber após DD dias úteis do vencimento.")]
		NaoReceberAposDDDiasUteisDoVencimento,
		// Token: 0x04000258 RID: 600
		[Descricao("Multa de %% por cento após DD dias corridos do vencimento.")]
		MultaDePPPorCentoAposDDDiasCorridosDoVencimento,
		// Token: 0x04000259 RID: 601
		[Descricao("Multa de %% por cento após DD dias úteis do vencimento.")]
		MultaDePPPorCentoAposDDDiasUteisDoVencimento,
		// Token: 0x0400025A RID: 602
		[Descricao("Protestar DD dias corridos após o vencimento se não pago.")]
		ProtestarDDDiasCorridosAposVencimentoSeNaoPago,
		// Token: 0x0400025B RID: 603
		[Descricao("Protestar DD dias úteis após o vencimento se não pago.")]
		ProtestarDDDiasUteisAposVencimentoSeNaoPago = 77,
		// Token: 0x0400025C RID: 604
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400025D RID: 605
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x0400025E RID: 606
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

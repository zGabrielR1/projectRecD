using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000096 RID: 150
	public enum EnumInstrucoesBradesco
	{
		// Token: 0x0400022D RID: 557
		[Descricao("Protesto falimentar após XX dias do vencimento.")]
		ProtestoFalimentarAposXXDiasDoVencimento = 5,
		// Token: 0x0400022E RID: 558
		[Descricao("Protestar após XX dias do vencimento.")]
		ProtestarAposXXDiasDoVencimento,
		// Token: 0x0400022F RID: 559
		[Descricao("Negativar após XX dias do vencimento.")]
		NegativarAposXXDiasDoVencimento,
		// Token: 0x04000230 RID: 560
		[Descricao("Não cobrar juros de mora.")]
		NaoCobrarJurosDeMora,
		// Token: 0x04000231 RID: 561
		[Descricao("Não receber após o vencimento.")]
		NaoReceberAposVencimento,
		// Token: 0x04000232 RID: 562
		[Descricao("Multa de 10% após o 4º dia do Vencimento.")]
		Multa10PorCentoApos4DiasDoVencimento,
		// Token: 0x04000233 RID: 563
		[Descricao("Não receber após o 8º dia do vencimento.")]
		NaoReceberApos8DiasDoVencimento,
		// Token: 0x04000234 RID: 564
		[Descricao("Cobrar encargos após o 5º dia do vencimento.")]
		CobrarEncargosApos5DiasDoVencimento,
		// Token: 0x04000235 RID: 565
		[Descricao("Cobrar encargos após o 10º dia do vencimento.")]
		CobrarEncargosApos10DiasDoVencimento,
		// Token: 0x04000236 RID: 566
		[Descricao("Cobrar encargos após o 15º dia do vencimento.")]
		CobrarEncargosApos15DiasDoVencimento,
		// Token: 0x04000237 RID: 567
		[Descricao("Conceder desconto mesmo se pago após o vencimento.")]
		ConcederDescontoMesmoSePagoAposVencimento,
		// Token: 0x04000238 RID: 568
		[Descricao("Decurso de prazo.")]
		DecursoDePrazo = 18,
		// Token: 0x04000239 RID: 569
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400023A RID: 570
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x0400023B RID: 571
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

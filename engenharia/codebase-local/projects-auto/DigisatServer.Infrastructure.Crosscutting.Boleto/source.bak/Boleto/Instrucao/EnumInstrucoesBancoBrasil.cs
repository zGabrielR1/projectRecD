using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000090 RID: 144
	public enum EnumInstrucoesBancoBrasil
	{
		// Token: 0x04000211 RID: 529
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarAposNDiasCorridos = 1,
		// Token: 0x04000212 RID: 530
		[Descricao("Protestar após XX dias úteis do vencimento.")]
		ProtestarAposNDiasUteis,
		// Token: 0x04000213 RID: 531
		[Descricao("Não protestar.")]
		NaoProtestar,
		// Token: 0x04000214 RID: 532
		[Descricao("Negativação sem protesto.")]
		NegativacaoSemProtesto = 8,
		// Token: 0x04000215 RID: 533
		[Descricao("Protestar no 3º dia útil após vencido.")]
		ProtestarNoTerceiroDiaUtil,
		// Token: 0x04000216 RID: 534
		[Descricao("Protestar no 4º dia útil após vencido.")]
		ProtestarNoQuartoDiaUtil,
		// Token: 0x04000217 RID: 535
		[Descricao("Protestar no 5º dia útil após vencido.")]
		ProtestarNoQuintoDiaUtil,
		// Token: 0x04000218 RID: 536
		[Descricao("Protestar no 10º dia corrido após vencido.")]
		ProtestarNoDecimoDiaCorrido,
		// Token: 0x04000219 RID: 537
		[Descricao("Protestar no 15º dia corrido após vencido.")]
		ProtestarNoDecimoQuintoDiaCorrido,
		// Token: 0x0400021A RID: 538
		[Descricao("Protestar no 20º dia corrido após vencido.")]
		ProtestarNoVigesimoDiaCorrido,
		// Token: 0x0400021B RID: 539
		[Descricao("Protestar no 25º dia corrido após vencido.")]
		ProtestarNoVigesimoQuintoDiaCorrido,
		// Token: 0x0400021C RID: 540
		[Descricao("Protestar no 30º dia corrido após vencido.")]
		ProtestarNoTrigesimoDiaCorrido,
		// Token: 0x0400021D RID: 541
		[Descricao("Protestar no 45º dia corrido após vencido.")]
		ProtestarNoQuadragesimoQuintoDiaCorrido,
		// Token: 0x0400021E RID: 542
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400021F RID: 543
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x04000220 RID: 544
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A1 RID: 161
	public enum EnumInstrucoesSafra
	{
		// Token: 0x040002A3 RID: 675
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarAposNDiasCorridos = 1,
		// Token: 0x040002A4 RID: 676
		[Descricao("Protestar após XX dias úteis do vencimento.")]
		ProtestarAposNDiasUteis,
		// Token: 0x040002A5 RID: 677
		[Descricao("Não protestar.")]
		NaoProtestar,
		// Token: 0x040002A6 RID: 678
		[Descricao("Baixar/Devolver após XX dias corridos do vencimento.")]
		BaixarDevolverAposNDias,
		// Token: 0x040002A7 RID: 679
		[Descricao("Não baixar/devolver.")]
		NaoBaixarDevolver,
		// Token: 0x040002A8 RID: 680
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002A9 RID: 681
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002AA RID: 682
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

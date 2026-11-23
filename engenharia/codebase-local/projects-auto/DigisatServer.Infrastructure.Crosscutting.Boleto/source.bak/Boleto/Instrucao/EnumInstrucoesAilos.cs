using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AD RID: 173
	public enum EnumInstrucoesAilos
	{
		// Token: 0x040002E2 RID: 738
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarDiasCorridos = 1,
		// Token: 0x040002E3 RID: 739
		[Descricao("Negativação de boletos via Serasa após XX dias corridos do vencimento.")]
		Negativacao,
		// Token: 0x040002E4 RID: 740
		[Descricao("Não Protestar/Não Negativar.")]
		NaoProtestar,
		// Token: 0x040002E5 RID: 741
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002E6 RID: 742
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002E7 RID: 743
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

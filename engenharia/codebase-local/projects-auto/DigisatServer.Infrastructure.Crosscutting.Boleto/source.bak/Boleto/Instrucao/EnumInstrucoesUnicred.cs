using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AF RID: 175
	public enum EnumInstrucoesUnicred
	{
		// Token: 0x040002E9 RID: 745
		[Descricao("Protestar após XX dias corridos.")]
		ProtestarAposNDiasCorridos = 1,
		// Token: 0x040002EA RID: 746
		[Descricao("Protestar após XX dias úteis.")]
		ProtestarAposNDiasUteis,
		// Token: 0x040002EB RID: 747
		[Descricao("Não Protestar.")]
		NaoProtestar,
		// Token: 0x040002EC RID: 748
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002ED RID: 749
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002EE RID: 750
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

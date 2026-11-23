using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000088 RID: 136
	public enum EnumInstrucoesCredisis
	{
		// Token: 0x040001FB RID: 507
		[Descricao("Não Protestar.")]
		NaoProtestar = 3,
		// Token: 0x040001FC RID: 508
		[Descricao("Protestar XX dias corridos após vencimento.")]
		ProtestarDiasCorridos = 99,
		// Token: 0x040001FD RID: 509
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040001FE RID: 510
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040001FF RID: 511
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000094 RID: 148
	public enum EnumInstrucoesBanrisul
	{
		// Token: 0x04000227 RID: 551
		[Descricao("Protestar XX dias corridos após vencimento.")]
		ProtestarDiasCorridos = 1,
		// Token: 0x04000228 RID: 552
		[Descricao("Não protestar.")]
		NaoProtestar = 3,
		// Token: 0x04000229 RID: 553
		[Descricao("A partir de DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400022A RID: 554
		[Descricao("A partir de DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x0400022B RID: 555
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

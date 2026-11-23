using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AB RID: 171
	public enum EnumInstrucoesCresol
	{
		// Token: 0x040002DC RID: 732
		[Descricao("Não Protestar.")]
		NaoProtestar = 3,
		// Token: 0x040002DD RID: 733
		[Descricao("Protestar XX dias corridos após vencimento.")]
		ProtestarDiasCorridos = 99,
		// Token: 0x040002DE RID: 734
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002DF RID: 735
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002E0 RID: 736
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

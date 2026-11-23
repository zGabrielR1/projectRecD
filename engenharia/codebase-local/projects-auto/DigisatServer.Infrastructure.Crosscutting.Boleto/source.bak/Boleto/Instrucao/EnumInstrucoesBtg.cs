using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A7 RID: 167
	public enum EnumInstrucoesBtg
	{
		// Token: 0x040002BD RID: 701
		[Descricao("Protestar 3 dias úteis após vencimento.")]
		ProtestarApos3DiasUteis = 3,
		// Token: 0x040002BE RID: 702
		[Descricao("Protestar 4 dias úteis após vencimento.")]
		ProtestarApos4DiasUteis,
		// Token: 0x040002BF RID: 703
		[Descricao("Protestar 5 dias úteis após vencimento.")]
		ProtestarApos5DiasUteis,
		// Token: 0x040002C0 RID: 704
		[Descricao("Não Protestar.")]
		NaoProtestar = 7,
		// Token: 0x040002C1 RID: 705
		[Descricao("Negativação sem Protesto.")]
		NegativacaoSemProtesto,
		// Token: 0x040002C2 RID: 706
		[Descricao("Protestar 10 dias úteis após vencimento.")]
		ProtestarApos10DiasUteis = 10,
		// Token: 0x040002C3 RID: 707
		[Descricao("Protestar 15 dias úteis após vencimento.")]
		ProtestarApos15DiasUteis = 15,
		// Token: 0x040002C4 RID: 708
		[Descricao("Protestar 20 dias úteis após vencimento.")]
		ProtestarApos20DiasUteis = 20,
		// Token: 0x040002C5 RID: 709
		[Descricao("Devolver após 15 dias vencido.")]
		DevolverApos15DiasVencido = 42,
		// Token: 0x040002C6 RID: 710
		[Descricao("Devolver após 30 dias vencido.")]
		DevolverApos30DiasVencido,
		// Token: 0x040002C7 RID: 711
		[Descricao("Protestar XX dias corridos após vencimento.")]
		ProtestarDiasCorridos = 99,
		// Token: 0x040002C8 RID: 712
		[Descricao("A partir de DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002C9 RID: 713
		[Descricao("A partir de DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002CA RID: 714
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

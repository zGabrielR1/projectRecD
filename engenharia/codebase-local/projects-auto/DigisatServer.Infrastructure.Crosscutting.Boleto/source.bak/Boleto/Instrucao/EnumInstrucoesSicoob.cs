using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A9 RID: 169
	public enum EnumInstrucoesSicoob
	{
		// Token: 0x040002CC RID: 716
		[Descricao("Cobrar juros.")]
		CobrarJuros = 1,
		// Token: 0x040002CD RID: 717
		[Descricao("Protestar 3 dias úteis após vencimento.")]
		ProtestarApos3DiasUteis = 3,
		// Token: 0x040002CE RID: 718
		[Descricao("Protestar 4 dias úteis após vencimento.")]
		ProtestarApos4DiasUteis,
		// Token: 0x040002CF RID: 719
		[Descricao("Protestar 5 dias úteis após vencimento.")]
		ProtestarApos5DiasUteis,
		// Token: 0x040002D0 RID: 720
		[Descricao("Não Protestar.")]
		NaoProtestar = 7,
		// Token: 0x040002D1 RID: 721
		[Descricao("Negativação sem Protesto.")]
		NegativacaoSemProtesto,
		// Token: 0x040002D2 RID: 722
		[Descricao("Protestar 10 dias úteis após vencimento.")]
		ProtestarApos10DiasUteis = 10,
		// Token: 0x040002D3 RID: 723
		[Descricao("Protestar 15 dias úteis após vencimento.")]
		ProtestarApos15DiasUteis = 15,
		// Token: 0x040002D4 RID: 724
		[Descricao("Protestar 20 dias úteis após vencimento.")]
		ProtestarApos20DiasUteis = 20,
		// Token: 0x040002D5 RID: 725
		[Descricao("Devolver após 15 dias vencido.")]
		DevolverApos15DiasVencido = 42,
		// Token: 0x040002D6 RID: 726
		[Descricao("Devolver após 30 dias vencido.")]
		DevolverApos30DiasVencido,
		// Token: 0x040002D7 RID: 727
		[Descricao("Protestar XX dias corridos após vencimento.")]
		ProtestarDiasCorridos = 99,
		// Token: 0x040002D8 RID: 728
		[Descricao("A partir de DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002D9 RID: 729
		[Descricao("A partir de DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002DA RID: 730
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

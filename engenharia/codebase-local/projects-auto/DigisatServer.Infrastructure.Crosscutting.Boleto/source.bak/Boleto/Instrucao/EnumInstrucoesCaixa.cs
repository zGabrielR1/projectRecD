using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000099 RID: 153
	public enum EnumInstrucoesCaixa
	{
		// Token: 0x0400023D RID: 573
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarAposNDiasCorridos = 1,
		// Token: 0x0400023E RID: 574
		[Descricao("Protestar após XX dias úteis do vencimento.")]
		ProtestarAposNDiasUteis,
		// Token: 0x0400023F RID: 575
		[Descricao("Não protestar.")]
		NaoProtestar,
		// Token: 0x04000240 RID: 576
		[Descricao("Baixar/Devolver após XX dias corridos do vencimento.")]
		BaixarDevolverAposNDias,
		// Token: 0x04000241 RID: 577
		[Descricao("Não baixar/devolver.")]
		NaoBaixarDevolver,
		// Token: 0x04000242 RID: 578
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x04000243 RID: 579
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x04000244 RID: 580
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

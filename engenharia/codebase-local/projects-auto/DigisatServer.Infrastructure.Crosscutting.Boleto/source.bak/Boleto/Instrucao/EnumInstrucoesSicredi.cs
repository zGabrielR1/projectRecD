using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000B1 RID: 177
	public enum EnumInstrucoesSicredi
	{
		// Token: 0x040002F0 RID: 752
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarDiasCorridos = 1,
		// Token: 0x040002F1 RID: 753
		[Descricao("Não Protestar.")]
		NaoProtestar = 3,
		// Token: 0x040002F2 RID: 754
		[Descricao("Negativação sem Protesto.")]
		NegativacaoSemProtesto = 8,
		// Token: 0x040002F3 RID: 755
		[Descricao("Cancelamento protesto automático.")]
		CancelamentoProtestoAutomatico,
		// Token: 0x040002F4 RID: 756
		[Descricao("Baixar/Devolver.")]
		BaixarDevolver = 11,
		// Token: 0x040002F5 RID: 757
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002F6 RID: 758
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002F7 RID: 759
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

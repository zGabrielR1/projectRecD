using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A5 RID: 165
	public enum EnumInstrucoesBanese
	{
		// Token: 0x040002B7 RID: 695
		[Descricao("Baixar/Devolver após XX dias corridos do vencimento.")]
		BaixarDevolverAposNDias = 1,
		// Token: 0x040002B8 RID: 696
		[Descricao("Não Protestar.")]
		NaoProtestar = 3,
		// Token: 0x040002B9 RID: 697
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002BA RID: 698
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002BB RID: 699
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

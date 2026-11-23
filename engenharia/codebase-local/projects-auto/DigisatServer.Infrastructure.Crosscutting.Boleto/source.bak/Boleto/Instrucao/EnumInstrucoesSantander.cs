using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A3 RID: 163
	public enum EnumInstrucoesSantander
	{
		// Token: 0x040002AC RID: 684
		[Descricao("Não protestar.")]
		NaoProtestar,
		// Token: 0x040002AD RID: 685
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarAposNDiasCorridos,
		// Token: 0x040002AE RID: 686
		[Descricao("Protestar após XX dias úteis do vencimento.")]
		ProtestarAposNDiasUteis,
		// Token: 0x040002AF RID: 687
		[Descricao("Utilizar perfil do cedente.")]
		UtilizarPerfilCedente,
		// Token: 0x040002B0 RID: 688
		[Descricao("Cancelamento de protesto automático.")]
		CancelamentoProtestoAutomatico = 9,
		// Token: 0x040002B1 RID: 689
		[Descricao("Baixar/Devolver.")]
		BaixarDevolver = 11,
		// Token: 0x040002B2 RID: 690
		[Descricao("Não Baixar/Não Devolver.")]
		NaoBaixarNaoDevolver = 21,
		// Token: 0x040002B3 RID: 691
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x040002B4 RID: 692
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002B5 RID: 693
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

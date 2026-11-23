using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008C RID: 140
	public enum EnumInstrucoesIntermedium
	{
		// Token: 0x04000205 RID: 517
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x04000206 RID: 518
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x04000207 RID: 519
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

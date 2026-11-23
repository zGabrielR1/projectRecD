using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008A RID: 138
	public enum EnumInstrucoesDaycoval
	{
		// Token: 0x04000201 RID: 513
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x04000202 RID: 514
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x04000203 RID: 515
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

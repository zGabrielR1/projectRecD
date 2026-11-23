using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000073 RID: 115
	public enum EnumTipoCobrancaCaixa
	{
		// Token: 0x040001D6 RID: 470
		[Descricao("Cobrança Simples")]
		CobrancaSimples = 1,
		// Token: 0x040001D7 RID: 471
		[Descricao("Cobrança Caucionada")]
		CobrancaCaucionada = 3,
		// Token: 0x040001D8 RID: 472
		[Descricao("Cobrança Descontada")]
		CobrancaDescontada
	}
}

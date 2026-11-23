using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000138 RID: 312
	public enum EnumCarteirasBanrisul
	{
		// Token: 0x0400059C RID: 1436
		[Descricao("Cobrança simples")]
		CobrancaSimples = 1,
		// Token: 0x0400059D RID: 1437
		[Descricao("Cobrança vinculada")]
		CobrancaVinculada,
		// Token: 0x0400059E RID: 1438
		[Descricao("Cobrança caucionada")]
		CobrancaCaucionada,
		// Token: 0x0400059F RID: 1439
		[Descricao("Cobrança descontada")]
		CobrancaDescontada
	}
}

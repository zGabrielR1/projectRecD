using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200007B RID: 123
	public enum EnumTipoCobrancaSicoob
	{
		// Token: 0x040001E7 RID: 487
		[Descricao("Simples")]
		Simples = 1,
		// Token: 0x040001E8 RID: 488
		[Descricao("Garantida Vinculada")]
		GarantidaVinculada,
		// Token: 0x040001E9 RID: 489
		[Descricao("Garantida Caucionada")]
		GarantidaDescontada
	}
}

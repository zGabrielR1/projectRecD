using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006F RID: 111
	public enum EnumTipoCobrancaBancoBrasil
	{
		// Token: 0x040001D0 RID: 464
		[Descricao("Simples")]
		Simples = 1,
		// Token: 0x040001D1 RID: 465
		[Descricao("Vinculada/Caucionada")]
		VinculadaCaucionada,
		// Token: 0x040001D2 RID: 466
		[Descricao("Descontada")]
		Descontada = 4
	}
}

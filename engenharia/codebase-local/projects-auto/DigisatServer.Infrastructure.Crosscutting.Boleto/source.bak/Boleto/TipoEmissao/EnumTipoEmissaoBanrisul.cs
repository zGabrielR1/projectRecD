using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000037 RID: 55
	public enum EnumTipoEmissaoBanrisul
	{
		// Token: 0x04000178 RID: 376
		[Descricao("Banco emite.")]
		BancoEmite = 1,
		// Token: 0x04000179 RID: 377
		[Descricao("Cliente emite.")]
		ClienteEmite
	}
}

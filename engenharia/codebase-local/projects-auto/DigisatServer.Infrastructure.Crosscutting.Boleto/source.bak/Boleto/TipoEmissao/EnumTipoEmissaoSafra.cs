using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000045 RID: 69
	public enum EnumTipoEmissaoSafra
	{
		// Token: 0x0400018C RID: 396
		[Descricao("Banco emite boleto")]
		BancoEmiteBoleto = 1,
		// Token: 0x0400018D RID: 397
		[Descricao("Cliente emite boleto")]
		ClienteEmiteBoleto
	}
}

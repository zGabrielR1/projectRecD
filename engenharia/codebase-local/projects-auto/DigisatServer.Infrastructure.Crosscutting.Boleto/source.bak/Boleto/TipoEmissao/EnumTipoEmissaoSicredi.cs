using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200004D RID: 77
	public enum EnumTipoEmissaoSicredi
	{
		// Token: 0x04000198 RID: 408
		[Descricao("SICREDI emite (auto-envelopável)")]
		SicrediEmite = 1,
		// Token: 0x04000199 RID: 409
		[Descricao("Cedente emite")]
		CedenteEmite
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000049 RID: 73
	public enum EnumTipoEmissaoSicoob
	{
		// Token: 0x04000192 RID: 402
		[Descricao("Sicoob emite")]
		SicoobEmite = 1,
		// Token: 0x04000193 RID: 403
		[Descricao("Beneficiário emite")]
		BeneficiarioEmite
	}
}

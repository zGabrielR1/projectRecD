using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000047 RID: 71
	public enum EnumTipoEmissaoBtg
	{
		// Token: 0x0400018F RID: 399
		[Descricao("Banco emite")]
		BancoEmite = 1,
		// Token: 0x04000190 RID: 400
		[Descricao("Beneficiário emite")]
		BeneficiarioEmite
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000064 RID: 100
	public enum EnumTipoDistribuicaoSicoob
	{
		// Token: 0x040001BD RID: 445
		[Descricao("Sicoob distribui")]
		SicoobDistribui = 1,
		// Token: 0x040001BE RID: 446
		[Descricao("Beneficiário distribui")]
		BeneficiarioDistribui
	}
}

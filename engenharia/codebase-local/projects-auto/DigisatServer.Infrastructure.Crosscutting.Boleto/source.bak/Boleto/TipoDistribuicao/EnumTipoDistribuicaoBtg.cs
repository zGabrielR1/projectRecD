using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000062 RID: 98
	public enum EnumTipoDistribuicaoBtg
	{
		// Token: 0x040001BA RID: 442
		[Descricao("Banco distribui")]
		BancoDistribui = 1,
		// Token: 0x040001BB RID: 443
		[Descricao("Beneficiário distribui")]
		BeneficiarioDistribui
	}
}

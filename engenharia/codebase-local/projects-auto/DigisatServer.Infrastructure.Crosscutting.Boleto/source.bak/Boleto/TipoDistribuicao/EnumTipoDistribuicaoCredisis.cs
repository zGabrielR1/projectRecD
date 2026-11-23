using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x0200005C RID: 92
	public enum EnumTipoDistribuicaoCredisis
	{
		// Token: 0x040001B1 RID: 433
		[Descricao("Banco distribui")]
		BancoDistribui = 1,
		// Token: 0x040001B2 RID: 434
		[Descricao("Cedente distribui")]
		CedenteDistribui
	}
}

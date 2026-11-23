using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000068 RID: 104
	public enum EnumTipoDistribuicaoSicredi
	{
		// Token: 0x040001C3 RID: 451
		[Descricao("SICREDI distribui")]
		SicrediDistribui = 1,
		// Token: 0x040001C4 RID: 452
		[Descricao("Cedente distribui")]
		CedenteDistribui
	}
}

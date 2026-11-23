using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000058 RID: 88
	public enum EnumTipoDistribuicaoCaixaSigcb
	{
		// Token: 0x040001AC RID: 428
		[Descricao("Banco distribui")]
		BancoDistribui = 1,
		// Token: 0x040001AD RID: 429
		[Descricao("Cliente distribui")]
		ClienteDistribui
	}
}

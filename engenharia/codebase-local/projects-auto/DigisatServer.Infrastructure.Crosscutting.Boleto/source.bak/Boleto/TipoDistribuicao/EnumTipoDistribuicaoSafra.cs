using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000060 RID: 96
	public enum EnumTipoDistribuicaoSafra
	{
		// Token: 0x040001B7 RID: 439
		[Descricao("Banco distribui boleto")]
		BancoDistribuiBoleto = 1,
		// Token: 0x040001B8 RID: 440
		[Descricao("Cliente distribui boleto")]
		ClienteDistribuiBoleto
	}
}

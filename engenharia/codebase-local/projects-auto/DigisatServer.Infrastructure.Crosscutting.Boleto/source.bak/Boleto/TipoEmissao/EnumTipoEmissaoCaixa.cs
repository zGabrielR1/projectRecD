using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000041 RID: 65
	public enum EnumTipoEmissaoCaixa
	{
		// Token: 0x04000187 RID: 391
		[Descricao("Banco emite")]
		BancoEmite = 1,
		// Token: 0x04000188 RID: 392
		[Descricao("Cliente emite")]
		ClienteEmite
	}
}

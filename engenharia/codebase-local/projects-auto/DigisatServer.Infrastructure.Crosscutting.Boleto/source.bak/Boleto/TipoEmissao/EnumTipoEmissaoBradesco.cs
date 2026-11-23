using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200003F RID: 63
	public enum EnumTipoEmissaoBradesco
	{
		// Token: 0x04000184 RID: 388
		[Descricao("Banco emite e processa registro.")]
		BancoEmiteEProcessa = 1,
		// Token: 0x04000185 RID: 389
		[Descricao("Cliente emite e banco processa registro.")]
		ClienteEmiteEBancoProcessa
	}
}

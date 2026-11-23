using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200003D RID: 61
	public enum EnumTipoEmissaoCresol
	{
		// Token: 0x04000181 RID: 385
		[Descricao("Banco emite e processa registro.")]
		BancoEmiteEProcessa = 1,
		// Token: 0x04000182 RID: 386
		[Descricao("Cliente emite e banco processa registro.")]
		ClienteEmiteEBancoProcessa
	}
}

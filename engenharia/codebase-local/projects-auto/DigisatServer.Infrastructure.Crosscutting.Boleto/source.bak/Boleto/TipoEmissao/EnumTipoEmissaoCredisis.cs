using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000039 RID: 57
	public enum EnumTipoEmissaoCredisis
	{
		// Token: 0x0400017B RID: 379
		[Descricao("Banco emite e processa registro.")]
		BancoEmiteEProcessa = 1,
		// Token: 0x0400017C RID: 380
		[Descricao("Cliente emite e banco processa registro.")]
		ClienteEmiteEBancoProcessa
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200003B RID: 59
	public enum EnumTipoEmissaoDaycoval
	{
		// Token: 0x0400017E RID: 382
		[Descricao("Banco emite e processa registro.")]
		BancoEmiteEProcessa = 1,
		// Token: 0x0400017F RID: 383
		[Descricao("Cliente emite e banco processa registro.")]
		ClienteEmiteEBancoProcessa
	}
}

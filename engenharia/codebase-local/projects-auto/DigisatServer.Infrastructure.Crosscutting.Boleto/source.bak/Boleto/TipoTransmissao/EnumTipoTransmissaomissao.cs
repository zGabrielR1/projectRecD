using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao
{
	// Token: 0x0200002F RID: 47
	public enum EnumTipoTransmissaomissao
	{
		// Token: 0x04000169 RID: 361
		[Descricao("Remessa e retorno")]
		RemessaRetorno = 1,
		// Token: 0x0400016A RID: 362
		[Descricao("DePagar")]
		DePagar
	}
}

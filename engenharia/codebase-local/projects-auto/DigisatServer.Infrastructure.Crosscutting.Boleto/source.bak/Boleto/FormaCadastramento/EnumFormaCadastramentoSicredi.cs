using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C6 RID: 198
	public enum EnumFormaCadastramentoSicredi
	{
		// Token: 0x04000311 RID: 785
		[Descricao("Com cadastramento (cobrança registrada)")]
		ComCadastramento = 1,
		// Token: 0x04000312 RID: 786
		[Descricao("Sem cadastramento (cobrança sem registro)")]
		SemCadastramento
	}
}

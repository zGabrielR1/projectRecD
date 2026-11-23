using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000B8 RID: 184
	public enum EnumFormaCadastramentoBancoBrasil
	{
		// Token: 0x040002FF RID: 767
		[Descricao("Cobrança registrada")]
		CobrancaRegistrada = 1,
		// Token: 0x04000300 RID: 768
		[Descricao("Cobrança sem registro")]
		CobrancaSemRegistro
	}
}

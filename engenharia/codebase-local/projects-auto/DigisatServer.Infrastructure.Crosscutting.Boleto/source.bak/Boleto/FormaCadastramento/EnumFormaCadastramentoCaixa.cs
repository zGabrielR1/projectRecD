using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000BC RID: 188
	public enum EnumFormaCadastramentoCaixa
	{
		// Token: 0x04000305 RID: 773
		[Descricao("Cobrança registrada")]
		CobrancaRegistrada = 1,
		// Token: 0x04000306 RID: 774
		[Descricao("Cobrança sem registro")]
		CobrancaSemRegistro
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000BA RID: 186
	public enum EnumFormaCadastramentoBanrisul
	{
		// Token: 0x04000302 RID: 770
		[Descricao("Com cadastramento (Cobrança registrada)")]
		ComCadastramentoCobrancaRegistrada = 1,
		// Token: 0x04000303 RID: 771
		[Descricao("Sem cadastramento (Cobrança sem registro)")]
		SemCadastramentoCobrancaSemRegistro
	}
}

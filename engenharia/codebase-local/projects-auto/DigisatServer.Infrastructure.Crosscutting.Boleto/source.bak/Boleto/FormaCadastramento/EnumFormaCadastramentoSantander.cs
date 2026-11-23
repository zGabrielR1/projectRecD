using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C0 RID: 192
	public enum EnumFormaCadastramentoSantander
	{
		// Token: 0x0400030A RID: 778
		[Descricao("Cobrança Registrada (Rápida e Eletrônica com Registro)")]
		CobrancaRegistrada = 1,
		// Token: 0x0400030B RID: 779
		[Descricao("Cobrança sem Registro")]
		CobrancaSemRegistro
	}
}

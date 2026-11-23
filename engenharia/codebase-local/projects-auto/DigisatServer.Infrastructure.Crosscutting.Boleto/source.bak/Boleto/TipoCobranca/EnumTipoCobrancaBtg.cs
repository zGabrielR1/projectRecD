using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000079 RID: 121
	public enum EnumTipoCobrancaBtg
	{
		// Token: 0x040001E3 RID: 483
		[Descricao("Com Cadastramento (Cobrança Registrada)")]
		ComCadastramentoCobrancaRegistrada = 1,
		// Token: 0x040001E4 RID: 484
		[Descricao("Sem Cadastramento (Cobrança sem Registro)")]
		SemCadastramento,
		// Token: 0x040001E5 RID: 485
		[Descricao("Com Cadastramento/Recusa do Débito Automático")]
		ComCadastramentoRecusaDoDebitoAutomatico
	}
}

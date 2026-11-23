using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000056 RID: 86
	public enum EnumTipoDistribuicaoCaixaSicob
	{
		// Token: 0x040001A6 RID: 422
		[Descricao("Postagem pelo Cedente")]
		PostagemPeloCedente,
		// Token: 0x040001A7 RID: 423
		[Descricao("Sacado via Correios")]
		SacadoViaCorreios,
		// Token: 0x040001A8 RID: 424
		[Descricao("Cedente via Agência CAIXA")]
		CedenteViaAgenciaCaixa,
		// Token: 0x040001A9 RID: 425
		[Descricao("Sacado via e-mail")]
		SacadoViaEmail,
		// Token: 0x040001AA RID: 426
		[Descricao("Sacado via SMS")]
		SacadoViaSMS
	}
}

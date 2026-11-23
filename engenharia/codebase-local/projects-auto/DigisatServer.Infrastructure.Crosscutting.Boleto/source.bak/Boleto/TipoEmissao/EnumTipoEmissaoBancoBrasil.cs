using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000035 RID: 53
	public enum EnumTipoEmissaoBancoBrasil
	{
		// Token: 0x04000171 RID: 369
		[Descricao("Banco emite")]
		BancoEmite = 1,
		// Token: 0x04000172 RID: 370
		[Descricao("Cliente emite")]
		ClienteEmite,
		// Token: 0x04000173 RID: 371
		[Descricao("Banco pré-emite e cliente complementa")]
		BancoPreEmiteEClienteComplementa,
		// Token: 0x04000174 RID: 372
		[Descricao("Banco reemite")]
		BancoReemite,
		// Token: 0x04000175 RID: 373
		[Descricao("Banco não reemite")]
		BancoNaoReemite,
		// Token: 0x04000176 RID: 374
		[Descricao("Cobrança sem papel")]
		CobrancaSemPapel
	}
}

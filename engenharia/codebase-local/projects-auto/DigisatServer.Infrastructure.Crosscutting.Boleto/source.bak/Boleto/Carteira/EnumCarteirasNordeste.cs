using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000136 RID: 310
	public enum EnumCarteirasNordeste
	{
		// Token: 0x04000596 RID: 1430
		[Descricao("Cobrança Simples Escritural - Boleto Emitido Pelo Banco")]
		CobrancaSimplesEscrituralBoletoEmitidoPeloBanco = 1,
		// Token: 0x04000597 RID: 1431
		[Descricao("Cobrança Vinculada – Boleto Emitido Pelo Banco")]
		CobrancaVinculadaBoletoEmitidoPeloBanco,
		// Token: 0x04000598 RID: 1432
		[Descricao("Cobrança Simples - Boleto Emitido Pelo Cliente")]
		CobrancaSimplesBoletoEmitidoPeloCliente = 4,
		// Token: 0x04000599 RID: 1433
		[Descricao("Cobrança Vinculada - Boleto Emitido Pelo Cliente")]
		CobrancaVinculadaBoletoEmitidoPeloCliente,
		// Token: 0x0400059A RID: 1434
		[Descricao("Cobrança Simplificada (Sem Registro)")]
		CobrancaSimplificadaSemRegistro
	}
}

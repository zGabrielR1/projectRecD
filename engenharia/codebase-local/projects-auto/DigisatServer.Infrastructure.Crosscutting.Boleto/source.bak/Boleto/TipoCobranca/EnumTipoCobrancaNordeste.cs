using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006D RID: 109
	public enum EnumTipoCobrancaNordeste
	{
		// Token: 0x040001CA RID: 458
		[Descricao("Cobrança Simples Escritural - Boleto Emitido Pelo Banco")]
		CobrancaSimplesEscrituralBoletoEmitidoPeloBanco,
		// Token: 0x040001CB RID: 459
		[Descricao("Cobrança Vinculada – Boleto Emitido Pelo Banco")]
		CobrancaVinculadaBoletoEmitidoPeloBanco,
		// Token: 0x040001CC RID: 460
		[Descricao("Cobrança Simples - Boleto Emitido Pelo Cliente")]
		CobrancaSimplesBoletoEmitidoPeloCliente,
		// Token: 0x040001CD RID: 461
		[Descricao("Cobrança Vinculada - Boleto Emitido Pelo Cliente")]
		CobrancaVinculadaBoletoEmitidoPeloCliente,
		// Token: 0x040001CE RID: 462
		[Descricao("Cobrança Simplificada (Sem Registro)")]
		CobrancaSimplificadaSemRegistro
	}
}

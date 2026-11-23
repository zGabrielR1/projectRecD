using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000077 RID: 119
	public enum EnumTipoCobrancaSantander
	{
		// Token: 0x040001DD RID: 477
		[Descricao("Cobrança Simples (Sem Registro e Eletrônica com Registro)")]
		CobrancaSimples = 1,
		// Token: 0x040001DE RID: 478
		[Descricao("Cobrança Caucionada (Eletrônica com Registro e Convencional com Registro)")]
		CobrancaCaucionada = 3,
		// Token: 0x040001DF RID: 479
		[Descricao("Cobrança Descontada (Eletrônica com Registro)")]
		CobrancaDescontada,
		// Token: 0x040001E0 RID: 480
		[Descricao("Cobrança Simples (Rápida com Registro)")]
		CobrancaSimplesComRegistro,
		// Token: 0x040001E1 RID: 481
		[Descricao("Cobrança Caucionada (Rápida com Registro)")]
		CobrancaCaucionadaComRegistro
	}
}

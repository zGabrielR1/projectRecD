using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x0200011F RID: 287
	public enum EnumCodigoLiquidacaoBancoBrasil
	{
		// Token: 0x04000558 RID: 1368
		PorSaldo = 1,
		// Token: 0x04000559 RID: 1369
		PorConta,
		// Token: 0x0400055A RID: 1370
		NoProprioBanco,
		// Token: 0x0400055B RID: 1371
		CompensacaoEletronica,
		// Token: 0x0400055C RID: 1372
		CompensacaoConvencional,
		// Token: 0x0400055D RID: 1373
		PorMeioEletronico,
		// Token: 0x0400055E RID: 1374
		AposFeriadoLocal,
		// Token: 0x0400055F RID: 1375
		EmCartorio,
		// Token: 0x04000560 RID: 1376
		ComandadaBanco,
		// Token: 0x04000561 RID: 1377
		ComandadaClienteArquivo,
		// Token: 0x04000562 RID: 1378
		ComandadaClienteOnline,
		// Token: 0x04000563 RID: 1379
		DecursoDePrazoCliente,
		// Token: 0x04000564 RID: 1380
		DecursoDePrazoBanco
	}
}

using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000188 RID: 392
	public enum EnumTipodeLinha
	{
		// Token: 0x0400070D RID: 1805
		HeaderDeArquivo = 1,
		// Token: 0x0400070E RID: 1806
		HeaderDeLote,
		// Token: 0x0400070F RID: 1807
		DetalheSegmentoP,
		// Token: 0x04000710 RID: 1808
		DetalheSegmentoQ,
		// Token: 0x04000711 RID: 1809
		DetalheSegmentoR,
		// Token: 0x04000712 RID: 1810
		TraillerDeLote,
		// Token: 0x04000713 RID: 1811
		TraillerDeArquivo
	}
}

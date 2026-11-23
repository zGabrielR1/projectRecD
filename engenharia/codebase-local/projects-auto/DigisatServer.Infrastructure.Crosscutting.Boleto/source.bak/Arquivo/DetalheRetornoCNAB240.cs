using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200017F RID: 383
	public class DetalheRetornoCNAB240 : AbstractArquivoRetorno
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x0006F27C File Offset: 0x0006D47C
		public DetalheRetornoCNAB240()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0006F2B0 File Offset: 0x0006D4B0
		public DetalheRetornoCNAB240(DetalheSegmentoTRetornoCNAB240 segmentoT)
		{
			this.SegmentoT = segmentoT;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0006F2EB File Offset: 0x0006D4EB
		public DetalheRetornoCNAB240(DetalheSegmentoURetornoCNAB240 segmentoU)
		{
			this.SegmentoU = segmentoU;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0006F326 File Offset: 0x0006D526
		public DetalheRetornoCNAB240(DetalheSegmentoWRetornoCNAB240 segmentoW)
		{
			this.SegmentoW = segmentoW;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0006F361 File Offset: 0x0006D561
		public DetalheRetornoCNAB240(DetalheSegmentoERetornoCNAB240 segmentoE)
		{
			this.SegmentoE = segmentoE;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0006F39C File Offset: 0x0006D59C
		public DetalheRetornoCNAB240(DetalheSegmentoTRetornoCNAB240 segmentoT, DetalheSegmentoURetornoCNAB240 segmentoU)
		{
			this.SegmentoT = segmentoT;
			this.SegmentoU = segmentoU;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0006F3E9 File Offset: 0x0006D5E9
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0006F3F1 File Offset: 0x0006D5F1
		public DetalheSegmentoTRetornoCNAB240 SegmentoT { get; set; } = new DetalheSegmentoTRetornoCNAB240();

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0006F3FA File Offset: 0x0006D5FA
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0006F402 File Offset: 0x0006D602
		public DetalheSegmentoURetornoCNAB240 SegmentoU { get; set; } = new DetalheSegmentoURetornoCNAB240();

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0006F40B File Offset: 0x0006D60B
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0006F413 File Offset: 0x0006D613
		public DetalheSegmentoWRetornoCNAB240 SegmentoW { get; set; } = new DetalheSegmentoWRetornoCNAB240();

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0006F41C File Offset: 0x0006D61C
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0006F424 File Offset: 0x0006D624
		public DetalheSegmentoERetornoCNAB240 SegmentoE { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0006F42D File Offset: 0x0006D62D
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0006F435 File Offset: 0x0006D635
		public HeaderDeArquivoCNAB240 HeaderArquivo { get; set; } = new HeaderDeArquivoCNAB240();
	}
}

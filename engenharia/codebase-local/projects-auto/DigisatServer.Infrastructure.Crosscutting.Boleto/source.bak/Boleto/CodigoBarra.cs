using System;
using System.Drawing;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x02000029 RID: 41
	public class CodigoBarra
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005DAF File Offset: 0x00003FAF
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public string Codigo { get; set; } = string.Empty;

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005DC0 File Offset: 0x00003FC0
		public Image Imagem { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005DC8 File Offset: 0x00003FC8
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public string LinhaDigitavel { get; set; } = string.Empty;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005DD9 File Offset: 0x00003FD9
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00005DE1 File Offset: 0x00003FE1
		public string Chave { get; set; } = "";
	}
}

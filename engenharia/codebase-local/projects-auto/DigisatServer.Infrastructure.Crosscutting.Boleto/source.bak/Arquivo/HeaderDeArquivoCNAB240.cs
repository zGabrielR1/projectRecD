using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000185 RID: 389
	public class HeaderDeArquivoCNAB240
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00070C5C File Offset: 0x0006EE5C
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00070C64 File Offset: 0x0006EE64
		public string MensagemRemessa { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00070C6D File Offset: 0x0006EE6D
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00070C75 File Offset: 0x0006EE75
		public string NumeroRemessa { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00070C7E File Offset: 0x0006EE7E
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00070C86 File Offset: 0x0006EE86
		public string DataRemessa { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00070C8F File Offset: 0x0006EE8F
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00070C97 File Offset: 0x0006EE97
		public string HoraRemessa { get; set; }

		// Token: 0x060009A2 RID: 2466 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		public void LerHeaderDeArquivoCNAB240(string Registro)
		{
			try
			{
				if (Registro.Substring(7, 1) != "0")
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características de Header de Arquivo.");
				}
				this.MensagemRemessa = Registro.Substring(171, 20).Trim();
				this.NumeroRemessa = Utils.FormatCode(Registro.Substring(157, 6).Trim(), "0", 6);
				this.DataRemessa = Convert.ToDecimal(Registro.Substring(143, 8)).ToString("00/00/0000");
				this.HoraRemessa = Convert.ToDecimal(Registro.Substring(151, 6)).ToString("00:00:00");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - Header de Arquivo.", ex);
			}
		}
	}
}

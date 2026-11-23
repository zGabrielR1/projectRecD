using System;
using System.Collections.Generic;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x02000027 RID: 39
	public class Boletos : List<Boleto>
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000590C File Offset: 0x00003B0C
		private new void Add(Boleto item)
		{
			if (item.Banco == null)
			{
				throw new Exception("Boleto não possui Banco.");
			}
			if (item.Cedente.ContaBancaria == null)
			{
				throw new Exception("Boleto não possui conta bancária.");
			}
			if (item.Cedente == null)
			{
				throw new Exception("Boleto não possui cedente.");
			}
			item.Valida();
			this.Add(item);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005964 File Offset: 0x00003B64
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000596C File Offset: 0x00003B6C
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Banco.Banco Banco { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005975 File Offset: 0x00003B75
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000597D File Offset: 0x00003B7D
		public ContaBancaria ContaBancaria { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005986 File Offset: 0x00003B86
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000598E File Offset: 0x00003B8E
		public Cedente Cedente { get; set; }
	}
}

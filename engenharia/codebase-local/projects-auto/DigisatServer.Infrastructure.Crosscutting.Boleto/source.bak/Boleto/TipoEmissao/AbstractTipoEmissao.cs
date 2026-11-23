using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000032 RID: 50
	public abstract class AbstractTipoEmissao : ITipoEmissao
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000611C File Offset: 0x0000431C
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006124 File Offset: 0x00004324
		public virtual IBanco Banco { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000612D File Offset: 0x0000432D
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006135 File Offset: 0x00004335
		public virtual int Codigo { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000613E File Offset: 0x0000433E
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00006146 File Offset: 0x00004346
		public virtual string Descricao { get; set; }
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006A RID: 106
	public abstract class AbstractTipoCobranca : ITipoCobranca
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007900 File Offset: 0x00005B00
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00007908 File Offset: 0x00005B08
		public virtual IBanco Banco { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007911 File Offset: 0x00005B11
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00007919 File Offset: 0x00005B19
		public virtual int Codigo { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00007922 File Offset: 0x00005B22
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000792A File Offset: 0x00005B2A
		public virtual string Descricao { get; set; }
	}
}

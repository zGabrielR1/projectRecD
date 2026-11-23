using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000124 RID: 292
	public abstract class AbstractCarteira : ICarteira
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00024078 File Offset: 0x00022278
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00024080 File Offset: 0x00022280
		public virtual IBanco Banco { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00024089 File Offset: 0x00022289
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00024091 File Offset: 0x00022291
		public virtual int NumeroCarteira { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0002409A File Offset: 0x0002229A
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000240A2 File Offset: 0x000222A2
		public virtual string Codigo { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000240AB File Offset: 0x000222AB
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x000240B3 File Offset: 0x000222B3
		public virtual string Tipo { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000240BC File Offset: 0x000222BC
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000240C4 File Offset: 0x000222C4
		public virtual string Descricao { get; set; }
	}
}

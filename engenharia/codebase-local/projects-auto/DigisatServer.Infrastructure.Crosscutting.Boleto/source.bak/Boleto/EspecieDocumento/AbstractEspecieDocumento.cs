using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000C9 RID: 201
	public abstract class AbstractEspecieDocumento : IEspecieDocumento
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000DF38 File Offset: 0x0000C138
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000DF40 File Offset: 0x0000C140
		public virtual IBanco Banco { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000DF49 File Offset: 0x0000C149
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000DF51 File Offset: 0x0000C151
		public virtual string Codigo { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000DF5A File Offset: 0x0000C15A
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000DF62 File Offset: 0x0000C162
		public virtual string Sigla { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000DF6B File Offset: 0x0000C16B
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000DF73 File Offset: 0x0000C173
		public virtual string Especie { get; set; }
	}
}

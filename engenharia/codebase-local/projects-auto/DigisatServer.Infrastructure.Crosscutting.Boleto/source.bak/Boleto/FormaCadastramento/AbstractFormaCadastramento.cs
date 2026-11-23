using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000B6 RID: 182
	public abstract class AbstractFormaCadastramento : IFormaCadastramento
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		public virtual IBanco Banco { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000D7ED File Offset: 0x0000B9ED
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000D7F5 File Offset: 0x0000B9F5
		public virtual int Codigo { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000D7FE File Offset: 0x0000B9FE
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000D806 File Offset: 0x0000BA06
		public virtual string Descricao { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000D80F File Offset: 0x0000BA0F
		public virtual bool Valido { get; }
	}
}

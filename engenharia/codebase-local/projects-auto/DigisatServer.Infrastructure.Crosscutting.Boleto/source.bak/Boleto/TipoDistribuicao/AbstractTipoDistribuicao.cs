using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x0200004F RID: 79
	public abstract class AbstractTipoDistribuicao : ITipoDistribuicao
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006D7C File Offset: 0x00004F7C
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00006D84 File Offset: 0x00004F84
		public virtual IBanco Banco { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00006D8D File Offset: 0x00004F8D
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00006D95 File Offset: 0x00004F95
		public virtual int Codigo { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006D9E File Offset: 0x00004F9E
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00006DA6 File Offset: 0x00004FA6
		public virtual string Descricao { get; set; }
	}
}

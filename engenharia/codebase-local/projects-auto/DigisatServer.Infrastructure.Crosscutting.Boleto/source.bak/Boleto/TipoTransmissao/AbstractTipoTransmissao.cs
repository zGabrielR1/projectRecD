using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao
{
	// Token: 0x0200002D RID: 45
	public abstract class AbstractTipoTransmissao : ITipoTransmissao
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005F3B File Offset: 0x0000413B
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005F43 File Offset: 0x00004143
		public virtual IBanco Banco { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005F4C File Offset: 0x0000414C
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005F54 File Offset: 0x00004154
		public virtual int Codigo { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005F5D File Offset: 0x0000415D
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00005F65 File Offset: 0x00004165
		public virtual string Descricao { get; set; }
	}
}

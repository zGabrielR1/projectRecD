using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200010A RID: 266
	public abstract class AbstractCodigoMovimento : ICodigoMovimento
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00021664 File Offset: 0x0001F864
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0002166C File Offset: 0x0001F86C
		public virtual IBanco Banco { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00021675 File Offset: 0x0001F875
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0002167D File Offset: 0x0001F87D
		public virtual int Codigo { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00021686 File Offset: 0x0001F886
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0002168E File Offset: 0x0001F88E
		public virtual string Descricao { get; set; }
	}
}

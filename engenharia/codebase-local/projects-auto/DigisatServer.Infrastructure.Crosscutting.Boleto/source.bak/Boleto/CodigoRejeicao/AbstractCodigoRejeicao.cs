using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x020000FE RID: 254
	public abstract class AbstractCodigoRejeicao : ICodigoRejeicao
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001594C File Offset: 0x00013B4C
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00015954 File Offset: 0x00013B54
		public virtual IBanco Banco { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0001595D File Offset: 0x00013B5D
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00015965 File Offset: 0x00013B65
		public virtual string Codigo { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001596E File Offset: 0x00013B6E
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00015976 File Offset: 0x00013B76
		public virtual string Descricao { get; set; }
	}
}

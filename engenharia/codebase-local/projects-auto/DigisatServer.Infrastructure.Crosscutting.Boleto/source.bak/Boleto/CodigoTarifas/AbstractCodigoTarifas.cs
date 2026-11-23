using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoTarifas
{
	// Token: 0x020000F9 RID: 249
	public abstract class AbstractCodigoTarifas : ICodigoTarifas
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001572C File Offset: 0x0001392C
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00015734 File Offset: 0x00013934
		public virtual IBanco Banco { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001573D File Offset: 0x0001393D
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00015745 File Offset: 0x00013945
		public virtual int Codigo { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001574E File Offset: 0x0001394E
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00015756 File Offset: 0x00013956
		public virtual string Descricao { get; set; }
	}
}

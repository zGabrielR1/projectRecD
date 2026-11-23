using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x0200011D RID: 285
	public abstract class AbstractCodigoLiquidacao : ICodigoLiquidacao
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00023908 File Offset: 0x00021B08
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00023910 File Offset: 0x00021B10
		public virtual IBanco Banco { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00023919 File Offset: 0x00021B19
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00023921 File Offset: 0x00021B21
		public virtual int Enumerado { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0002392A File Offset: 0x00021B2A
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00023932 File Offset: 0x00021B32
		public virtual string Codigo { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0002393B File Offset: 0x00021B3B
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00023943 File Offset: 0x00021B43
		public virtual string Descricao { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0002394C File Offset: 0x00021B4C
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00023954 File Offset: 0x00021B54
		public virtual string Recurso { get; set; }
	}
}

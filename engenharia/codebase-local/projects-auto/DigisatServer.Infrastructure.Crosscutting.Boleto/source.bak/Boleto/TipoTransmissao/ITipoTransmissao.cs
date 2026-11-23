using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao
{
	// Token: 0x0200002E RID: 46
	public interface ITipoTransmissao
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BC RID: 444
		// (set) Token: 0x060001BD RID: 445
		IBanco Banco { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BE RID: 446
		// (set) Token: 0x060001BF RID: 447
		int Codigo { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C0 RID: 448
		// (set) Token: 0x060001C1 RID: 449
		string Descricao { get; set; }
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006B RID: 107
	public interface ITipoCobranca
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000236 RID: 566
		// (set) Token: 0x06000237 RID: 567
		IBanco Banco { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000238 RID: 568
		// (set) Token: 0x06000239 RID: 569
		int Codigo { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600023A RID: 570
		// (set) Token: 0x0600023B RID: 571
		string Descricao { get; set; }
	}
}

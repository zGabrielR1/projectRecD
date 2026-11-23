using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000033 RID: 51
	public interface ITipoEmissao
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001D5 RID: 469
		// (set) Token: 0x060001D6 RID: 470
		IBanco Banco { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D7 RID: 471
		// (set) Token: 0x060001D8 RID: 472
		int Codigo { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D9 RID: 473
		// (set) Token: 0x060001DA RID: 474
		string Descricao { get; set; }
	}
}

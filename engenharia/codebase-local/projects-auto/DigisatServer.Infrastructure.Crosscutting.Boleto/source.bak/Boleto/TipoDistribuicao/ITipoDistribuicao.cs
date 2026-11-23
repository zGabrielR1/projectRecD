using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000050 RID: 80
	public interface ITipoDistribuicao
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000206 RID: 518
		// (set) Token: 0x06000207 RID: 519
		IBanco Banco { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000208 RID: 520
		// (set) Token: 0x06000209 RID: 521
		int Codigo { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600020A RID: 522
		// (set) Token: 0x0600020B RID: 523
		string Descricao { get; set; }
	}
}

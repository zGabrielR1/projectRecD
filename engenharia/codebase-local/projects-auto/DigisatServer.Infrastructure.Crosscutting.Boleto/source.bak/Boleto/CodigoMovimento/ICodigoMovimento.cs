using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200011C RID: 284
	public interface ICodigoMovimento
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000419 RID: 1049
		IBanco Banco { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600041A RID: 1050
		// (set) Token: 0x0600041B RID: 1051
		int Codigo { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600041C RID: 1052
		string Descricao { get; }
	}
}

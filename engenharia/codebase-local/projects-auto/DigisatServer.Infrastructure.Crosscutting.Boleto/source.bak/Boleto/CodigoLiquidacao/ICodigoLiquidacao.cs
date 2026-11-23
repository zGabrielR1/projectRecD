using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x02000123 RID: 291
	public interface ICodigoLiquidacao
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000432 RID: 1074
		IBanco Banco { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000433 RID: 1075
		// (set) Token: 0x06000434 RID: 1076
		int Enumerado { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000435 RID: 1077
		// (set) Token: 0x06000436 RID: 1078
		string Codigo { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000437 RID: 1079
		string Descricao { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000438 RID: 1080
		string Recurso { get; }
	}
}

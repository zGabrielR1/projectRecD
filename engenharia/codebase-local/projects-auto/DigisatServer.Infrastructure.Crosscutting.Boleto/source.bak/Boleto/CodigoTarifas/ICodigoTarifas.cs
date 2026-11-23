using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoTarifas
{
	// Token: 0x020000FD RID: 253
	public interface ICodigoTarifas
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003D6 RID: 982
		IBanco Banco { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003D7 RID: 983
		// (set) Token: 0x060003D8 RID: 984
		int Codigo { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003D9 RID: 985
		string Descricao { get; }
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000109 RID: 265
	public interface ICodigoRejeicao
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F8 RID: 1016
		IBanco Banco { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003F9 RID: 1017
		// (set) Token: 0x060003FA RID: 1018
		string Codigo { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003FB RID: 1019
		string Descricao { get; }
	}
}

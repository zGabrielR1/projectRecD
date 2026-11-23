using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000151 RID: 337
	public interface ICarteira
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000491 RID: 1169
		// (set) Token: 0x06000492 RID: 1170
		IBanco Banco { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000493 RID: 1171
		// (set) Token: 0x06000494 RID: 1172
		int NumeroCarteira { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000495 RID: 1173
		// (set) Token: 0x06000496 RID: 1174
		string Codigo { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000497 RID: 1175
		// (set) Token: 0x06000498 RID: 1176
		string Tipo { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000499 RID: 1177
		// (set) Token: 0x0600049A RID: 1178
		string Descricao { get; set; }
	}
}

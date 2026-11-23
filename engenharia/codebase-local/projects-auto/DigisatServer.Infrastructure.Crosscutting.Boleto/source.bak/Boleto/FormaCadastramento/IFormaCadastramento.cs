using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C8 RID: 200
	public interface IFormaCadastramento
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000333 RID: 819
		// (set) Token: 0x06000334 RID: 820
		IBanco Banco { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000335 RID: 821
		// (set) Token: 0x06000336 RID: 822
		int Codigo { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000337 RID: 823
		// (set) Token: 0x06000338 RID: 824
		string Descricao { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000339 RID: 825
		bool Valido { get; }
	}
}

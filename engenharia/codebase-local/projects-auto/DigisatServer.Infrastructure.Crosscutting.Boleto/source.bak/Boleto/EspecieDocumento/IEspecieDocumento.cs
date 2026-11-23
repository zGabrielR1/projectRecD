using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000F8 RID: 248
	public interface IEspecieDocumento
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003C0 RID: 960
		// (set) Token: 0x060003C1 RID: 961
		IBanco Banco { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003C2 RID: 962
		// (set) Token: 0x060003C3 RID: 963
		string Codigo { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C4 RID: 964
		// (set) Token: 0x060003C5 RID: 965
		string Sigla { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C6 RID: 966
		// (set) Token: 0x060003C7 RID: 967
		string Especie { get; set; }
	}
}

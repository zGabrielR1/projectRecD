using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado
{
	// Token: 0x02000083 RID: 131
	public interface ISacado
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000262 RID: 610
		string CpfCnpjFormatado { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000263 RID: 611
		// (set) Token: 0x06000264 RID: 612
		string Nome { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000265 RID: 613
		string EnderecoToText { get; }
	}
}

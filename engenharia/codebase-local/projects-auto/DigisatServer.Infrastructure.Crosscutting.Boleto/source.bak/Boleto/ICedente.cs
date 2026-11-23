using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x0200002C RID: 44
	public interface ICedente
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B1 RID: 433
		string CpfCnpjFormatado { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B2 RID: 434
		// (set) Token: 0x060001B3 RID: 435
		string Nome { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B4 RID: 436
		string EnderecoToText { get; }
	}
}

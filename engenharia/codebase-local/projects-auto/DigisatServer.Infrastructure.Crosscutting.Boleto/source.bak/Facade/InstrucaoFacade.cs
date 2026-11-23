using System;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000018 RID: 24
	public class InstrucaoFacade : IInstrucaoFacade
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x000052C0 File Offset: 0x000034C0
		public InstrucaoFacade(int codigo, int dias)
		{
			this.Codigo = codigo;
			this.Dias = dias;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000052D6 File Offset: 0x000034D6
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000052DE File Offset: 0x000034DE
		public int Codigo { get; protected set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000052E7 File Offset: 0x000034E7
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000052EF File Offset: 0x000034EF
		public int Dias { get; protected set; }
	}
}

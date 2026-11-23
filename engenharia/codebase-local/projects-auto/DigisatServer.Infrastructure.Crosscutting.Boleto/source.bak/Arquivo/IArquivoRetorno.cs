using System;
using System.IO;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000187 RID: 391
	public interface IArquivoRetorno
	{
		// Token: 0x060009AF RID: 2479
		void LerArquivoRetorno(IBanco banco, Stream arquivo);

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009B0 RID: 2480
		IBanco Banco { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009B1 RID: 2481
		TipoArquivo TipoArquivo { get; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060009B2 RID: 2482
		// (remove) Token: 0x060009B3 RID: 2483
		event EventHandler<LinhaDeArquivoLidaArgs> LinhaDeArquivoLida;
	}
}

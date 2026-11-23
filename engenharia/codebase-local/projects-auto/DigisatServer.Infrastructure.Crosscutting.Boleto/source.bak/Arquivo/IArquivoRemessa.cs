using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000186 RID: 390
	public interface IArquivoRemessa
	{
		// Token: 0x060009A4 RID: 2468
		void GerarArquivoRemessa(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, string diretorioArquivoRemessa, int numeroArquivoRemessa);

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009A5 RID: 2469
		Boletos Boletos { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009A6 RID: 2470
		Cedente Cedente { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009A7 RID: 2471
		IBanco Banco { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009A8 RID: 2472
		// (set) Token: 0x060009A9 RID: 2473
		string NumeroConvenio { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009AA RID: 2474
		// (set) Token: 0x060009AB RID: 2475
		int NumeroArquivoRemessa { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009AC RID: 2476
		TipoArquivo TipoArquivo { get; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060009AD RID: 2477
		// (remove) Token: 0x060009AE RID: 2478
		event EventHandler<LinhaDeArquivoGeradaArgs> LinhaDeArquivoGerada;
	}
}

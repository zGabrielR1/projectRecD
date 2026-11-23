using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000189 RID: 393
	public class LinhaDeArquivoGeradaArgs : EventArgs
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x00070D70 File Offset: 0x0006EF70
		public LinhaDeArquivoGeradaArgs(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, string linha, EnumTipodeLinha tipoLinha)
		{
			try
			{
				this.Boleto = boleto;
				this.Linha = linha;
				this.TipoLinha = tipoLinha;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao instanciar objeto", ex);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00070DB8 File Offset: 0x0006EFB8
		public string Linha { get; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00070DC0 File Offset: 0x0006EFC0
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto Boleto { get; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00070DC8 File Offset: 0x0006EFC8
		public EnumTipodeLinha TipoLinha { get; }
	}
}

using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200018B RID: 395
	public class LinhaDeArquivoLidaArgs : EventArgs
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		public LinhaDeArquivoLidaArgs(object detalhe, string linha)
		{
			try
			{
				this.Linha = linha;
				this.Detalhe = detalhe;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao instanciar objeto", ex);
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00070E10 File Offset: 0x0006F010
		public LinhaDeArquivoLidaArgs(object detalhe, string linha, EnumTipodeLinhaLida tipoLinha)
		{
			try
			{
				this.Linha = linha;
				this.Detalhe = detalhe;
				this.TipoLinha = tipoLinha;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao instanciar objeto", ex);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00070E58 File Offset: 0x0006F058
		public string Linha { get; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00070E60 File Offset: 0x0006F060
		public object Detalhe { get; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00070E68 File Offset: 0x0006F068
		public EnumTipodeLinhaLida TipoLinha { get; }
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000B4 RID: 180
	public class InstrucaoSudameris : AbstractInstrucao
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000D770 File Offset: 0x0000B970
		public InstrucaoSudameris()
		{
			try
			{
				this.Banco = new Banco(347);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		public InstrucaoSudameris(int codigo)
		{
			this.Carregar(codigo, 0);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public InstrucaoSudameris(int codigo, int nrDias)
		{
			this.Carregar(codigo, nrDias);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A4DC File Offset: 0x000086DC
		private void Carregar(int idInstrucao, int nrDias)
		{
			throw new Exception("Não implementado");
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000098 RID: 152
	public class InstrucaoBrb : AbstractInstrucao
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000A47C File Offset: 0x0000867C
		public InstrucaoBrb()
		{
			try
			{
				this.Banco = new Banco(70);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A4BC File Offset: 0x000086BC
		public InstrucaoBrb(int codigo)
		{
			this.Carregar(codigo, 0);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A4CC File Offset: 0x000086CC
		public InstrucaoBrb(int codigo, int nrDias)
		{
			this.Carregar(codigo, nrDias);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A4DC File Offset: 0x000086DC
		private void Carregar(int idInstrucao, int nrDias)
		{
			throw new Exception("Não implementado");
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

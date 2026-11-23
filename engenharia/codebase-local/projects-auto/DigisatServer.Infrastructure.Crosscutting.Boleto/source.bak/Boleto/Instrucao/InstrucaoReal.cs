using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A0 RID: 160
	public class InstrucaoReal : AbstractInstrucao
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public InstrucaoReal()
		{
			try
			{
				this.Banco = new Banco(356);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BB50 File Offset: 0x00009D50
		public InstrucaoReal(int codigo)
		{
			this.Carregar(codigo, 0);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BB60 File Offset: 0x00009D60
		public InstrucaoReal(int codigo, int nrDias)
		{
			this.Carregar(codigo, nrDias);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A4DC File Offset: 0x000086DC
		private void Carregar(int idInstrucao, int nrDias)
		{
			throw new Exception("Não implementado");
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

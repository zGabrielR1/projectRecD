using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000019 RID: 25
	public class InstrucaoFacadeFactory
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000052F8 File Offset: 0x000034F8
		public InstrucaoFacadeFactory SetarCodigo(int codigo)
		{
			this._codigo = codigo;
			return this;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005302 File Offset: 0x00003502
		public InstrucaoFacadeFactory SetarDias(int dias)
		{
			this._dias = dias;
			return this;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000530C File Offset: 0x0000350C
		public InstrucaoFacade Criar()
		{
			return new InstrucaoFacade(this._codigo, this._dias);
		}

		// Token: 0x04000075 RID: 117
		private int _codigo;

		// Token: 0x04000076 RID: 118
		private int _dias;
	}
}

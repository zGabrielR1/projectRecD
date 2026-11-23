using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000093 RID: 147
	public class InstrucaoBankBoston : AbstractInstrucao
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public InstrucaoBankBoston()
		{
			try
			{
				this.Banco = new Banco(479);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00009D24 File Offset: 0x00007F24
		public InstrucaoBankBoston(int codigo)
		{
			this.Carregar(codigo, 0);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009D34 File Offset: 0x00007F34
		public InstrucaoBankBoston(int codigo, int nrDias)
		{
			this.Carregar(codigo, nrDias);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009D44 File Offset: 0x00007F44
		private void Carregar(int idInstrucao, int nrDias)
		{
			try
			{
				this.Banco = new BancoBankBoston();
				this.Valida();
				switch (idInstrucao)
				{
				case 0:
					this.Codigo = 0;
					this.Descricao = "Sem instrução";
					break;
				case 1:
					this.Codigo = 1;
					this.Descricao = "Protestar após 5 dias úteis";
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = "Não protestar";
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = "Devolver após 5 dias";
					break;
				default:
					this.Codigo = 99;
					this.Descricao = "( Selecione )";
					break;
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00009E08 File Offset: 0x00008008
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento
{
	// Token: 0x0200010B RID: 267
	public class CodigoMovimento : AbstractCodigoMovimento
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00021697 File Offset: 0x0001F897
		internal CodigoMovimento()
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0002169F File Offset: 0x0001F89F
		public CodigoMovimento(int codigoBanco, int codigoMovimento, TipoSolucao solucao)
		{
			this.InstanciaCodigoMovimento(codigoBanco, codigoMovimento, solucao);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public override IBanco Banco
		{
			get
			{
				return this._iCodigoMovimento.Banco;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x000216BD File Offset: 0x0001F8BD
		public override int Codigo
		{
			get
			{
				return this._iCodigoMovimento.Codigo;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000216CA File Offset: 0x0001F8CA
		public override string Descricao
		{
			get
			{
				return this._iCodigoMovimento.Descricao;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000216D8 File Offset: 0x0001F8D8
		private void InstanciaCodigoMovimento(int codigoBanco, int codigoMovimento, TipoSolucao solucao)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 8)
					{
						if (codigoBanco == 1)
						{
							this._iCodigoMovimento = new CodigoMovimentoBancoBrasil(codigoMovimento);
							goto IL_00FB;
						}
						if (codigoBanco != 8)
						{
							goto IL_00E5;
						}
					}
					else if (codigoBanco != 33)
					{
						if (codigoBanco != 104)
						{
							goto IL_00E5;
						}
						if (solucao == TipoSolucao.Sigcb)
						{
							this._iCodigoMovimento = new CodigoMovimentoCaixaSigcb(codigoMovimento);
							goto IL_00FB;
						}
						if (solucao != TipoSolucao.Sicob)
						{
							goto IL_00FB;
						}
						this._iCodigoMovimento = new CodigoMovimentoCaixaSicob(codigoMovimento);
						goto IL_00FB;
					}
				}
				else if (codigoBanco <= 353)
				{
					if (codigoBanco == 208)
					{
						this._iCodigoMovimento = new CodigoMovimentoBtg(codigoMovimento);
						goto IL_00FB;
					}
					if (codigoBanco != 353)
					{
						goto IL_00E5;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iCodigoMovimento = new CodigoMovimentoDaycoval(codigoMovimento);
						goto IL_00FB;
					}
					if (codigoBanco == 748)
					{
						this._iCodigoMovimento = new CodigoMovimentoSicredi(codigoMovimento);
						goto IL_00FB;
					}
					if (codigoBanco != 756)
					{
						goto IL_00E5;
					}
					this._iCodigoMovimento = new CodigoMovimentoSicoob(codigoMovimento);
					goto IL_00FB;
				}
				this._iCodigoMovimento = new CodigoMovimentoSantander(codigoMovimento);
				goto IL_00FB;
				IL_00E5:
				throw new Exception("Código do movimento não implementado: " + codigoMovimento);
				IL_00FB:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x04000448 RID: 1096
		private ICodigoMovimento _iCodigoMovimento;
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000051 RID: 81
	public class TipoDistribuicao : AbstractTipoDistribuicao
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00006DAF File Offset: 0x00004FAF
		internal TipoDistribuicao()
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006DB7 File Offset: 0x00004FB7
		public TipoDistribuicao(int codigoBanco, TipoSolucao solucao)
		{
			this.InstanciaTipoDistribuicao(codigoBanco, 0, solucao);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public TipoDistribuicao(int codigoBanco, int codigoTipoDistribuicao, TipoSolucao solucao)
		{
			this.InstanciaTipoDistribuicao(codigoBanco, codigoTipoDistribuicao, solucao);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public TipoDistribuicao(int codigoBanco, int codigoTipoDistribuicao, int solucaoCef)
		{
			this.InstanciaTipoDistribuicao(codigoBanco, codigoTipoDistribuicao, (TipoSolucao)solucaoCef);
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006DD9 File Offset: 0x00004FD9
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00006DE6 File Offset: 0x00004FE6
		public override IBanco Banco
		{
			get
			{
				return this._iTipoDistribuicao.Banco;
			}
			set
			{
				this._iTipoDistribuicao.Banco = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006DF4 File Offset: 0x00004FF4
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00006E01 File Offset: 0x00005001
		public override int Codigo
		{
			get
			{
				return this._iTipoDistribuicao.Codigo;
			}
			set
			{
				this._iTipoDistribuicao.Codigo = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006E0F File Offset: 0x0000500F
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006E1C File Offset: 0x0000501C
		public override string Descricao
		{
			get
			{
				return this._iTipoDistribuicao.Descricao;
			}
			set
			{
				this._iTipoDistribuicao.Descricao = value;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006E2C File Offset: 0x0000502C
		private void InstanciaTipoDistribuicao(int codigoBanco, int codigoTipoDistribuicao, TipoSolucao solucao)
		{
			try
			{
				if (codigoBanco <= 97)
				{
					if (codigoBanco <= 41)
					{
						if (codigoBanco == 1)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoBancoBrasil(codigoTipoDistribuicao);
							goto IL_0157;
						}
						if (codigoBanco == 41)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoBanrisul(codigoTipoDistribuicao);
							goto IL_0157;
						}
					}
					else
					{
						if (codigoBanco == 47)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoBanese(codigoTipoDistribuicao);
							goto IL_0157;
						}
						if (codigoBanco == 85)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoAilos(codigoTipoDistribuicao);
							goto IL_0157;
						}
						if (codigoBanco == 97)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoCredisis(codigoTipoDistribuicao);
							goto IL_0157;
						}
					}
				}
				else if (codigoBanco <= 422)
				{
					if (codigoBanco != 104)
					{
						if (codigoBanco == 208)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoBtg(codigoTipoDistribuicao);
							goto IL_0157;
						}
						if (codigoBanco == 422)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoSafra(codigoTipoDistribuicao);
							goto IL_0157;
						}
					}
					else
					{
						if (solucao == TipoSolucao.Sigcb)
						{
							this._iTipoDistribuicao = new TipoDistribuicaoCaixaSigcb(codigoTipoDistribuicao);
							goto IL_0157;
						}
						if (solucao != TipoSolucao.Sicob)
						{
							goto IL_0157;
						}
						this._iTipoDistribuicao = new TipoDistribuicaoCaixaSicob(codigoTipoDistribuicao);
						goto IL_0157;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iTipoDistribuicao = new TipoDistribuicaoDaycoval(codigoTipoDistribuicao);
						goto IL_0157;
					}
					if (codigoBanco == 748)
					{
						this._iTipoDistribuicao = new TipoDistribuicaoSicredi(codigoTipoDistribuicao);
						goto IL_0157;
					}
					if (codigoBanco == 756)
					{
						this._iTipoDistribuicao = new TipoDistribuicaoSicoob(codigoTipoDistribuicao);
						goto IL_0157;
					}
				}
				throw new Exception("Tipo de distribuição não implementado: " + codigoBanco);
				IL_0157:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x0400019D RID: 413
		private ITipoDistribuicao _iTipoDistribuicao;
	}
}

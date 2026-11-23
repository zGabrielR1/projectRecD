using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x020000FF RID: 255
	public class CodigoRejeicao : AbstractCodigoRejeicao
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0001597F File Offset: 0x00013B7F
		public CodigoRejeicao(int codigoBanco, string codigoRejeicao, TipoSolucao solucao)
		{
			this.InstanciaCodigoRejeicao(codigoBanco, codigoRejeicao, solucao);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00015990 File Offset: 0x00013B90
		public override IBanco Banco
		{
			get
			{
				return this._iCodigoRejeicao.Banco;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001599D File Offset: 0x00013B9D
		public override string Codigo
		{
			get
			{
				return this._iCodigoRejeicao.Codigo;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000159AA File Offset: 0x00013BAA
		public override string Descricao
		{
			get
			{
				return this._iCodigoRejeicao.Descricao;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000159B8 File Offset: 0x00013BB8
		private void InstanciaCodigoRejeicao(int codigoBanco, string codigoRejeicao, TipoSolucao solucao)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 8)
					{
						if (codigoBanco == 1)
						{
							this._iCodigoRejeicao = new CodigoRejeicaoBancoBrasil(codigoRejeicao);
							goto IL_0112;
						}
						if (codigoBanco != 8)
						{
							goto IL_0101;
						}
					}
					else if (codigoBanco != 33)
					{
						if (codigoBanco == 41)
						{
							this._iCodigoRejeicao = new CodigoRejeicaoBanrisul(codigoRejeicao);
							goto IL_0112;
						}
						if (codigoBanco != 104)
						{
							goto IL_0101;
						}
						if (solucao == TipoSolucao.Sigcb)
						{
							this._iCodigoRejeicao = new CodigoRejeicaoCaixaSigcb(codigoRejeicao);
							goto IL_0112;
						}
						if (solucao != TipoSolucao.Sicob)
						{
							goto IL_0112;
						}
						this._iCodigoRejeicao = new CodigoRejeicaoCaixaSicob(codigoRejeicao);
						goto IL_0112;
					}
				}
				else if (codigoBanco <= 353)
				{
					if (codigoBanco == 208)
					{
						this._iCodigoRejeicao = new CodigoRejeicaoBtg(codigoRejeicao);
						goto IL_0112;
					}
					if (codigoBanco != 353)
					{
						goto IL_0101;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iCodigoRejeicao = new CodigoRejeicaoDaycoval(codigoRejeicao);
						goto IL_0112;
					}
					if (codigoBanco == 748)
					{
						this._iCodigoRejeicao = new CodigoRejeicaoSicredi(codigoRejeicao);
						goto IL_0112;
					}
					if (codigoBanco != 756)
					{
						goto IL_0101;
					}
					this._iCodigoRejeicao = new CodigoRejeicaoSicoob(codigoRejeicao);
					goto IL_0112;
				}
				this._iCodigoRejeicao = new CodigoRejeicaoSantander(codigoRejeicao);
				goto IL_0112;
				IL_0101:
				throw new Exception("Código da rejeição não implementada: " + codigoRejeicao);
				IL_0112:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x04000444 RID: 1092
		private ICodigoRejeicao _iCodigoRejeicao;
	}
}

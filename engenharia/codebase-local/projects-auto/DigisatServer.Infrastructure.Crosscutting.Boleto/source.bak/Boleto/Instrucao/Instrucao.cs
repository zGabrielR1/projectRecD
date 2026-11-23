using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000087 RID: 135
	public class Instrucao : AbstractInstrucao
	{
		// Token: 0x0600028A RID: 650 RVA: 0x000087A6 File Offset: 0x000069A6
		public Instrucao(int codigoBanco)
		{
			this.InstanciaInstrucao(codigoBanco);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000087B8 File Offset: 0x000069B8
		public Instrucao(Boleto boleto, int codigoBanco, int codigoInstrucao, decimal valor, DateTime? data, int numeroDias = 0, decimal percentualMulta = 0m, TipoArquivo tipoArquivo = TipoArquivo.CNAB240)
		{
			this.InstanciaInstrucao(boleto, codigoBanco, codigoInstrucao, valor, data, numeroDias, percentualMulta, tipoArquivo);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000087E0 File Offset: 0x000069E0
		private void InstanciaInstrucao(int codigoBanco)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 41)
					{
						if (codigoBanco <= 4)
						{
							if (codigoBanco == 1)
							{
								this._iInstrucao = new InstrucaoBancoBrasil();
								goto IL_0238;
							}
							if (codigoBanco != 4)
							{
								goto IL_0222;
							}
							this._iInstrucao = new InstrucaoNordeste();
							goto IL_0238;
						}
						else if (codigoBanco != 8 && codigoBanco != 33)
						{
							if (codigoBanco != 41)
							{
								goto IL_0222;
							}
							this._iInstrucao = new InstrucaoBanrisul();
							goto IL_0238;
						}
					}
					else if (codigoBanco <= 77)
					{
						if (codigoBanco == 47)
						{
							this._iInstrucao = new InstrucaoBanese();
							goto IL_0238;
						}
						if (codigoBanco != 77)
						{
							goto IL_0222;
						}
						this._iInstrucao = new InstrucaoIntermedium();
						goto IL_0238;
					}
					else
					{
						if (codigoBanco == 85)
						{
							this._iInstrucao = new InstrucaoAilos();
							goto IL_0238;
						}
						if (codigoBanco == 97)
						{
							this._iInstrucao = new InstrucaoCredisis();
							goto IL_0238;
						}
						if (codigoBanco != 104)
						{
							goto IL_0222;
						}
						this._iInstrucao = new InstrucaoCaixa();
						goto IL_0238;
					}
				}
				else if (codigoBanco <= 341)
				{
					if (codigoBanco <= 136)
					{
						if (codigoBanco == 133)
						{
							this._iInstrucao = new InstrucaoCresol();
							goto IL_0238;
						}
						if (codigoBanco != 136)
						{
							goto IL_0222;
						}
						this._iInstrucao = new InstrucaoUnicred();
						goto IL_0238;
					}
					else
					{
						if (codigoBanco == 208)
						{
							this._iInstrucao = new InstrucaoBtg();
							goto IL_0238;
						}
						if (codigoBanco == 237)
						{
							this._iInstrucao = new InstrucaoBradesco();
							goto IL_0238;
						}
						if (codigoBanco != 341)
						{
							goto IL_0222;
						}
						this._iInstrucao = new InstrucaoItau();
						goto IL_0238;
					}
				}
				else if (codigoBanco <= 422)
				{
					if (codigoBanco != 353)
					{
						if (codigoBanco == 399)
						{
							this._iInstrucao = new InstrucaoHsbc();
							goto IL_0238;
						}
						if (codigoBanco != 422)
						{
							goto IL_0222;
						}
						this._iInstrucao = new InstrucaoSafra();
						goto IL_0238;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iInstrucao = new InstrucaoDaycoval();
						goto IL_0238;
					}
					if (codigoBanco == 748)
					{
						this._iInstrucao = new InstrucaoSicredi();
						goto IL_0238;
					}
					if (codigoBanco != 756)
					{
						goto IL_0222;
					}
					this._iInstrucao = new InstrucaoSicoob();
					goto IL_0238;
				}
				this._iInstrucao = new InstrucaoSantander();
				goto IL_0238;
				IL_0222:
				throw new Exception("Instrução de cobrança não implementada para o banco: " + codigoBanco);
				IL_0238:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008A5C File Offset: 0x00006C5C
		private void InstanciaInstrucao(Boleto boleto, int codigoBanco, int codigoInstrucao, decimal valor, DateTime? data, int numeroDias, decimal percentualMulta, TipoArquivo tipoCobranca = TipoArquivo.CNAB240)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 41)
					{
						if (codigoBanco <= 4)
						{
							if (codigoBanco == 1)
							{
								this._iInstrucao = new InstrucaoBancoBrasil(codigoInstrucao, valor, data, tipoCobranca, numeroDias);
								goto IL_02D0;
							}
							if (codigoBanco != 4)
							{
								goto IL_02BA;
							}
							this._iInstrucao = new InstrucaoNordeste(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						else if (codigoBanco != 8 && codigoBanco != 33)
						{
							if (codigoBanco != 41)
							{
								goto IL_02BA;
							}
							this._iInstrucao = new InstrucaoBanrisul(codigoInstrucao, valor, data, numeroDias, percentualMulta);
							goto IL_02D0;
						}
					}
					else if (codigoBanco <= 77)
					{
						if (codigoBanco == 47)
						{
							this._iInstrucao = new InstrucaoBanese(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco != 77)
						{
							goto IL_02BA;
						}
						this._iInstrucao = new InstrucaoIntermedium(codigoInstrucao, valor, data, 0);
						goto IL_02D0;
					}
					else
					{
						if (codigoBanco == 85)
						{
							this._iInstrucao = new InstrucaoAilos(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco == 97)
						{
							this._iInstrucao = new InstrucaoCredisis(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco != 104)
						{
							goto IL_02BA;
						}
						this._iInstrucao = new InstrucaoCaixa(codigoInstrucao, valor, data, numeroDias);
						goto IL_02D0;
					}
				}
				else if (codigoBanco <= 341)
				{
					if (codigoBanco <= 136)
					{
						if (codigoBanco == 133)
						{
							this._iInstrucao = new InstrucaoCresol(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco != 136)
						{
							goto IL_02BA;
						}
						this._iInstrucao = new InstrucaoUnicred(codigoInstrucao, valor, data, numeroDias);
						goto IL_02D0;
					}
					else
					{
						if (codigoBanco == 208)
						{
							this._iInstrucao = new InstrucaoBtg(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco == 237)
						{
							this._iInstrucao = new InstrucaoBradesco(codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco != 341)
						{
							goto IL_02BA;
						}
						this._iInstrucao = new InstrucaoItau(codigoInstrucao, data, valor, numeroDias);
						goto IL_02D0;
					}
				}
				else if (codigoBanco <= 422)
				{
					if (codigoBanco != 353)
					{
						if (codigoBanco == 399)
						{
							this._iInstrucao = new InstrucaoHsbc(boleto, codigoInstrucao, valor, data, numeroDias);
							goto IL_02D0;
						}
						if (codigoBanco != 422)
						{
							goto IL_02BA;
						}
						this._iInstrucao = new InstrucaoSafra(codigoInstrucao, valor, data, numeroDias);
						goto IL_02D0;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iInstrucao = new InstrucaoDaycoval(codigoInstrucao, valor, data, numeroDias);
						goto IL_02D0;
					}
					if (codigoBanco == 748)
					{
						this._iInstrucao = new InstrucaoSicredi(codigoInstrucao, valor, data, numeroDias);
						goto IL_02D0;
					}
					if (codigoBanco != 756)
					{
						goto IL_02BA;
					}
					this._iInstrucao = new InstrucaoSicoob(codigoInstrucao, valor, data, numeroDias);
					goto IL_02D0;
				}
				this._iInstrucao = new InstrucaoSantander(codigoInstrucao, valor, data, numeroDias);
				goto IL_02D0;
				IL_02BA:
				throw new Exception("Instrução de cobrança não implementada para o banco: " + codigoBanco);
				IL_02D0:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00008D70 File Offset: 0x00006F70
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00008D7D File Offset: 0x00006F7D
		public override IBanco Banco
		{
			get
			{
				return this._iInstrucao.Banco;
			}
			set
			{
				this._iInstrucao.Banco = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00008D8B File Offset: 0x00006F8B
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00008D98 File Offset: 0x00006F98
		public override int Codigo
		{
			get
			{
				return this._iInstrucao.Codigo;
			}
			set
			{
				this._iInstrucao.Codigo = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008DA6 File Offset: 0x00006FA6
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00008DB3 File Offset: 0x00006FB3
		public override string Descricao
		{
			get
			{
				return this._iInstrucao.Descricao;
			}
			set
			{
				this._iInstrucao.Descricao = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00008DC1 File Offset: 0x00006FC1
		public override int QuantidadeDias
		{
			get
			{
				return this._iInstrucao.QuantidadeDias;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008DCE File Offset: 0x00006FCE
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this._iInstrucao.IsInstrucaoProtesto;
			}
		}

		// Token: 0x040001F9 RID: 505
		private IInstrucao _iInstrucao;
	}
}

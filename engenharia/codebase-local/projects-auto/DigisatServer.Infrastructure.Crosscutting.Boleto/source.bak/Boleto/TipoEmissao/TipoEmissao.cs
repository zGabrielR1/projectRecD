using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000034 RID: 52
	public class TipoEmissao : AbstractTipoEmissao
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000614F File Offset: 0x0000434F
		internal TipoEmissao()
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006157 File Offset: 0x00004357
		public TipoEmissao(int codigoBanco)
		{
			this.InstanciaTipoEmissao(codigoBanco, 0);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006167 File Offset: 0x00004367
		public TipoEmissao(int codigoBanco, int codigoEspecie)
		{
			this.InstanciaTipoEmissao(codigoBanco, codigoEspecie);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006177 File Offset: 0x00004377
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006184 File Offset: 0x00004384
		public override IBanco Banco
		{
			get
			{
				return this._iTipoEmissao.Banco;
			}
			set
			{
				this._iTipoEmissao.Banco = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006192 File Offset: 0x00004392
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000619F File Offset: 0x0000439F
		public override int Codigo
		{
			get
			{
				return this._iTipoEmissao.Codigo;
			}
			set
			{
				this._iTipoEmissao.Codigo = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000061AD File Offset: 0x000043AD
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000061BA File Offset: 0x000043BA
		public override string Descricao
		{
			get
			{
				return this._iTipoEmissao.Descricao;
			}
			set
			{
				this._iTipoEmissao.Descricao = value;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000061C8 File Offset: 0x000043C8
		private void InstanciaTipoEmissao(int codigoBanco, int codigoTipoEmissao)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 47)
					{
						if (codigoBanco == 1)
						{
							this._iTipoEmissao = new TipoEmissaoBancoBrasil(codigoTipoEmissao);
							goto IL_0181;
						}
						if (codigoBanco == 41)
						{
							this._iTipoEmissao = new TipoEmissaoBanrisul(codigoTipoEmissao);
							goto IL_0181;
						}
						if (codigoBanco == 47)
						{
							this._iTipoEmissao = new TipoEmissaoBanese(codigoTipoEmissao);
							goto IL_0181;
						}
					}
					else
					{
						if (codigoBanco == 85)
						{
							this._iTipoEmissao = new TipoEmissaoAilos(codigoTipoEmissao);
							goto IL_0181;
						}
						if (codigoBanco == 97)
						{
							this._iTipoEmissao = new TipoEmissaoCredisis(codigoTipoEmissao);
							goto IL_0181;
						}
						if (codigoBanco == 104)
						{
							this._iTipoEmissao = new TipoEmissaoCaixa(codigoTipoEmissao);
							goto IL_0181;
						}
					}
				}
				else if (codigoBanco <= 237)
				{
					if (codigoBanco == 133)
					{
						this._iTipoEmissao = new TipoEmissaoCresol(codigoTipoEmissao);
						goto IL_0181;
					}
					if (codigoBanco == 208)
					{
						this._iTipoEmissao = new TipoEmissaoBtg(codigoTipoEmissao);
						goto IL_0181;
					}
					if (codigoBanco == 237)
					{
						this._iTipoEmissao = new TipoEmissaoBradesco(codigoTipoEmissao);
						goto IL_0181;
					}
				}
				else if (codigoBanco <= 707)
				{
					if (codigoBanco == 422)
					{
						this._iTipoEmissao = new TipoEmissaoSafra(codigoTipoEmissao);
						goto IL_0181;
					}
					if (codigoBanco == 707)
					{
						this._iTipoEmissao = new TipoEmissaoDaycoval(codigoTipoEmissao);
						goto IL_0181;
					}
				}
				else
				{
					if (codigoBanco == 748)
					{
						this._iTipoEmissao = new TipoEmissaoSicredi(codigoTipoEmissao);
						goto IL_0181;
					}
					if (codigoBanco == 756)
					{
						this._iTipoEmissao = new TipoEmissaoSicoob(codigoTipoEmissao);
						goto IL_0181;
					}
				}
				throw new Exception("Tipo de emissão não implementado: " + codigoTipoEmissao);
				IL_0181:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x0400016F RID: 367
		private ITipoEmissao _iTipoEmissao;
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006C RID: 108
	public class TipoCobranca : AbstractTipoCobranca
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00007933 File Offset: 0x00005B33
		internal TipoCobranca()
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000793B File Offset: 0x00005B3B
		public TipoCobranca(int codigoBanco)
		{
			this.InstanciaTipoCobranca(codigoBanco, 0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000794B File Offset: 0x00005B4B
		public TipoCobranca(int codigoBanco, int codigoTipoCobranca)
		{
			this.InstanciaTipoCobranca(codigoBanco, codigoTipoCobranca);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000795B File Offset: 0x00005B5B
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007968 File Offset: 0x00005B68
		public override IBanco Banco
		{
			get
			{
				return this._iTipoCobranca.Banco;
			}
			set
			{
				this._iTipoCobranca.Banco = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007976 File Offset: 0x00005B76
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007983 File Offset: 0x00005B83
		public override int Codigo
		{
			get
			{
				return this._iTipoCobranca.Codigo;
			}
			set
			{
				this._iTipoCobranca.Codigo = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007991 File Offset: 0x00005B91
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000799E File Offset: 0x00005B9E
		public override string Descricao
		{
			get
			{
				return this._iTipoCobranca.Descricao;
			}
			set
			{
				this._iTipoCobranca.Descricao = value;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000079AC File Offset: 0x00005BAC
		private void InstanciaTipoCobranca(int codigoBanco, int codigoTipoCobranca)
		{
			try
			{
				if (codigoBanco <= 85)
				{
					if (codigoBanco <= 8)
					{
						if (codigoBanco == 1)
						{
							this._iTipoCobranca = new TipoCobrancaBancoBrasil(codigoTipoCobranca);
							goto IL_0124;
						}
						if (codigoBanco == 4)
						{
							this._iTipoCobranca = new TipoCobrancaNordeste(codigoTipoCobranca);
							goto IL_0124;
						}
						if (codigoBanco != 8)
						{
							goto IL_010E;
						}
					}
					else if (codigoBanco != 33)
					{
						if (codigoBanco == 41)
						{
							this._iTipoCobranca = new TipoCobrancaBanrisul(codigoTipoCobranca);
							goto IL_0124;
						}
						if (codigoBanco != 85)
						{
							goto IL_010E;
						}
						this._iTipoCobranca = new TipoCobrancaAilos(codigoTipoCobranca);
						goto IL_0124;
					}
				}
				else if (codigoBanco <= 353)
				{
					if (codigoBanco == 104)
					{
						this._iTipoCobranca = new TipoCobrancaCaixa(codigoTipoCobranca);
						goto IL_0124;
					}
					if (codigoBanco == 208)
					{
						this._iTipoCobranca = new TipoCobrancaBtg(codigoTipoCobranca);
						goto IL_0124;
					}
					if (codigoBanco != 353)
					{
						goto IL_010E;
					}
				}
				else
				{
					if (codigoBanco == 422)
					{
						this._iTipoCobranca = new TipoCobrancaSafra(codigoTipoCobranca);
						goto IL_0124;
					}
					if (codigoBanco == 748)
					{
						this._iTipoCobranca = new TipoCobrancaSicredi(codigoTipoCobranca);
						goto IL_0124;
					}
					if (codigoBanco != 756)
					{
						goto IL_010E;
					}
					this._iTipoCobranca = new TipoCobrancaSicoob(codigoTipoCobranca);
					goto IL_0124;
				}
				this._iTipoCobranca = new TipoCobrancaSantander(codigoTipoCobranca);
				goto IL_0124;
				IL_010E:
				throw new Exception("Tipo de cobrança não implementado: " + codigoTipoCobranca);
				IL_0124:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x040001C8 RID: 456
		private ITipoCobranca _iTipoCobranca;
	}
}

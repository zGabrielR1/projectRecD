using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000125 RID: 293
	public class Carteira : AbstractCarteira
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x000240CD File Offset: 0x000222CD
		internal Carteira()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000240D5 File Offset: 0x000222D5
		public Carteira(int codigoBanco)
		{
			this.InstanciaCarteira(codigoBanco, 0);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000240E5 File Offset: 0x000222E5
		public Carteira(int codigoBanco, int codigoCarteira)
		{
			this.InstanciaCarteira(codigoBanco, codigoCarteira);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000240F5 File Offset: 0x000222F5
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00024102 File Offset: 0x00022302
		public override IBanco Banco
		{
			get
			{
				return this._iCarteira.Banco;
			}
			set
			{
				this._iCarteira.Banco = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00024110 File Offset: 0x00022310
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0002411D File Offset: 0x0002231D
		public override int NumeroCarteira
		{
			get
			{
				return this._iCarteira.NumeroCarteira;
			}
			set
			{
				this._iCarteira.NumeroCarteira = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0002412B File Offset: 0x0002232B
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00024138 File Offset: 0x00022338
		public override string Codigo
		{
			get
			{
				return this._iCarteira.Codigo;
			}
			set
			{
				this._iCarteira.Codigo = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00024146 File Offset: 0x00022346
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00024153 File Offset: 0x00022353
		public override string Tipo
		{
			get
			{
				return this._iCarteira.Tipo;
			}
			set
			{
				this._iCarteira.Tipo = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00024161 File Offset: 0x00022361
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0002416E File Offset: 0x0002236E
		public override string Descricao
		{
			get
			{
				return this._iCarteira.Descricao;
			}
			set
			{
				this._iCarteira.Descricao = value;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0002417C File Offset: 0x0002237C
		private void InstanciaCarteira(int codigoBanco, int codigoCarteira)
		{
			try
			{
				if (codigoBanco <= 133)
				{
					if (codigoBanco <= 41)
					{
						if (codigoBanco <= 4)
						{
							if (codigoBanco == 1)
							{
								this._iCarteira = new CarteiraBancoBrasil(codigoCarteira);
								goto IL_0270;
							}
							if (codigoBanco != 4)
							{
								goto IL_025A;
							}
							this._iCarteira = new CarteiraNordeste(codigoCarteira);
							goto IL_0270;
						}
						else if (codigoBanco != 8 && codigoBanco != 33)
						{
							if (codigoBanco != 41)
							{
								goto IL_025A;
							}
							this._iCarteira = new CarteiraBanrisul(codigoCarteira);
							goto IL_0270;
						}
					}
					else if (codigoBanco <= 85)
					{
						if (codigoBanco == 47)
						{
							this._iCarteira = new CarteiraBanese(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco == 77)
						{
							this._iCarteira = new CarteiraIntermedium(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco != 85)
						{
							goto IL_025A;
						}
						this._iCarteira = new CarteiraAilos(codigoCarteira);
						goto IL_0270;
					}
					else
					{
						if (codigoBanco == 97)
						{
							this._iCarteira = new CarteiraCredisis(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco == 104)
						{
							this._iCarteira = new CarteiraCaixa(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco != 133)
						{
							goto IL_025A;
						}
						this._iCarteira = new CarteiraCresol(codigoCarteira);
						goto IL_0270;
					}
				}
				else if (codigoBanco <= 341)
				{
					if (codigoBanco <= 208)
					{
						if (codigoBanco == 136)
						{
							this._iCarteira = new CarteiraUnicred(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco != 208)
						{
							goto IL_025A;
						}
						this._iCarteira = new CarteiraBtg(codigoCarteira);
						goto IL_0270;
					}
					else
					{
						if (codigoBanco == 237)
						{
							this._iCarteira = new CarteiraBradesco(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco == 290)
						{
							this._iCarteira = new CarteiraPagbank(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco != 341)
						{
							goto IL_025A;
						}
						this._iCarteira = new CarteiraItau(0);
						goto IL_0270;
					}
				}
				else if (codigoBanco <= 479)
				{
					if (codigoBanco != 353)
					{
						if (codigoBanco == 422)
						{
							this._iCarteira = new CarteiraSafra(codigoCarteira);
							goto IL_0270;
						}
						if (codigoBanco != 479)
						{
							goto IL_025A;
						}
						this._iCarteira = new CarteiraBankBoston(0);
						goto IL_0270;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iCarteira = new CarteiraDaycoval(codigoCarteira);
						goto IL_0270;
					}
					if (codigoBanco == 748)
					{
						this._iCarteira = new CarteiraSicredi(codigoCarteira);
						goto IL_0270;
					}
					if (codigoBanco != 756)
					{
						goto IL_025A;
					}
					this._iCarteira = new CarteiraSicoob(codigoCarteira);
					goto IL_0270;
				}
				this._iCarteira = new CarteiraSantander(codigoCarteira);
				goto IL_0270;
				IL_025A:
				throw new Exception("Carteira não implementada: " + codigoCarteira);
				IL_0270:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x0400057B RID: 1403
		private ICarteira _iCarteira;
	}
}

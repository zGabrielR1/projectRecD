using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000B7 RID: 183
	public class FormaCadastramento : AbstractFormaCadastramento
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000D817 File Offset: 0x0000BA17
		internal FormaCadastramento()
		{
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D81F File Offset: 0x0000BA1F
		public FormaCadastramento(int codigoBanco)
		{
			this.InstanciaFormaCadastramento(codigoBanco, 0);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D82F File Offset: 0x0000BA2F
		public FormaCadastramento(int codigoBanco, int codigoFormaCadastramento)
		{
			this.InstanciaFormaCadastramento(codigoBanco, codigoFormaCadastramento);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D83F File Offset: 0x0000BA3F
		public override bool Valido
		{
			get
			{
				return this._iFormaCadastramento != null;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D84A File Offset: 0x0000BA4A
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000D857 File Offset: 0x0000BA57
		public override IBanco Banco
		{
			get
			{
				return this._iFormaCadastramento.Banco;
			}
			set
			{
				this._iFormaCadastramento.Banco = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D865 File Offset: 0x0000BA65
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000D872 File Offset: 0x0000BA72
		public override int Codigo
		{
			get
			{
				return this._iFormaCadastramento.Codigo;
			}
			set
			{
				this._iFormaCadastramento.Codigo = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000D880 File Offset: 0x0000BA80
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000D88D File Offset: 0x0000BA8D
		public override string Descricao
		{
			get
			{
				return this._iFormaCadastramento.Descricao;
			}
			set
			{
				this._iFormaCadastramento.Descricao = value;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D89C File Offset: 0x0000BA9C
		private void InstanciaFormaCadastramento(int codigoBanco, int codigoFormaCadastramento)
		{
			try
			{
				if (codigoBanco <= 47)
				{
					if (codigoBanco <= 8)
					{
						if (codigoBanco == 1)
						{
							this._iFormaCadastramento = new FormaCadastramentoBancoBrasil(codigoFormaCadastramento);
							goto IL_00E1;
						}
						if (codigoBanco != 8)
						{
							goto IL_00CB;
						}
					}
					else if (codigoBanco != 33)
					{
						if (codigoBanco == 41)
						{
							this._iFormaCadastramento = new FormaCadastramentoBanrisul(codigoFormaCadastramento);
							goto IL_00E1;
						}
						if (codigoBanco != 47)
						{
							goto IL_00CB;
						}
						this._iFormaCadastramento = new FormaCadastramentoBanese(codigoFormaCadastramento);
						goto IL_00E1;
					}
				}
				else if (codigoBanco <= 104)
				{
					if (codigoBanco == 85)
					{
						this._iFormaCadastramento = new FormaCadastramentoAilos(codigoFormaCadastramento);
						goto IL_00E1;
					}
					if (codigoBanco != 104)
					{
						goto IL_00CB;
					}
					this._iFormaCadastramento = new FormaCadastramentoCaixa(codigoFormaCadastramento);
					goto IL_00E1;
				}
				else if (codigoBanco != 353)
				{
					if (codigoBanco == 422)
					{
						this._iFormaCadastramento = new FormaCadastramentoSafra(codigoFormaCadastramento);
						goto IL_00E1;
					}
					if (codigoBanco != 748)
					{
						goto IL_00CB;
					}
					this._iFormaCadastramento = new FormaCadastramentoSicredi(codigoFormaCadastramento);
					goto IL_00E1;
				}
				this._iFormaCadastramento = new FormaCadastramentoSantander(codigoFormaCadastramento);
				goto IL_00E1;
				IL_00CB:
				throw new Exception("Forma de cadastramento não implementada: " + codigoFormaCadastramento);
				IL_00E1:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x040002FD RID: 765
		private IFormaCadastramento _iFormaCadastramento;
	}
}

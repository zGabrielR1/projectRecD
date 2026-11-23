using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao
{
	// Token: 0x02000031 RID: 49
	public class TipoTransmissao : AbstractTipoTransmissao
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000602C File Offset: 0x0000422C
		internal TipoTransmissao()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006034 File Offset: 0x00004234
		public TipoTransmissao(int codigoBanco)
		{
			this.InstanciaTipoTransmissao(codigoBanco, 0);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006044 File Offset: 0x00004244
		public TipoTransmissao(int codigoBanco, int codigoEspecie)
		{
			this.InstanciaTipoTransmissao(codigoBanco, codigoEspecie);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006054 File Offset: 0x00004254
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00006061 File Offset: 0x00004261
		public override IBanco Banco
		{
			get
			{
				return this._iTipoTransmissao.Banco;
			}
			set
			{
				this._iTipoTransmissao.Banco = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000606F File Offset: 0x0000426F
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000607C File Offset: 0x0000427C
		public override int Codigo
		{
			get
			{
				return this._iTipoTransmissao.Codigo;
			}
			set
			{
				this._iTipoTransmissao.Codigo = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000608A File Offset: 0x0000428A
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00006097 File Offset: 0x00004297
		public override string Descricao
		{
			get
			{
				return this._iTipoTransmissao.Descricao;
			}
			set
			{
				this._iTipoTransmissao.Descricao = value;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000060A8 File Offset: 0x000042A8
		private void InstanciaTipoTransmissao(int codigoBanco, int codigoTipoTransmissao)
		{
			try
			{
				if (codigoTipoTransmissao != 1)
				{
					if (codigoTipoTransmissao != 2)
					{
						throw new Exception("Tipo de Transmissao não implementado: " + codigoTipoTransmissao);
					}
					this._iTipoTransmissao = new TiposTransmissao(codigoTipoTransmissao);
				}
				else
				{
					this._iTipoTransmissao = new TiposTransmissao(codigoTipoTransmissao);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da Transmissao. " + ex.Message, ex);
			}
		}

		// Token: 0x0400016B RID: 363
		private ITipoTransmissao _iTipoTransmissao;
	}
}

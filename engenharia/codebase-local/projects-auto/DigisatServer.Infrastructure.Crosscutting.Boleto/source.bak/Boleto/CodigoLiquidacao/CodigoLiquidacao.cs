using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x0200011E RID: 286
	public class CodigoLiquidacao : AbstractCodigoLiquidacao
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0002395D File Offset: 0x00021B5D
		public override IBanco Banco
		{
			get
			{
				return this._iCodigoLiquidacao.Banco;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0002396A File Offset: 0x00021B6A
		public override int Enumerado
		{
			get
			{
				return this._iCodigoLiquidacao.Enumerado;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00023977 File Offset: 0x00021B77
		public override string Codigo
		{
			get
			{
				return this._iCodigoLiquidacao.Codigo;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00023984 File Offset: 0x00021B84
		public override string Descricao
		{
			get
			{
				return this._iCodigoLiquidacao.Descricao;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00023991 File Offset: 0x00021B91
		public override string Recurso
		{
			get
			{
				return this._iCodigoLiquidacao.Recurso;
			}
		}

		// Token: 0x04000556 RID: 1366
		private readonly ICodigoLiquidacao _iCodigoLiquidacao;
	}
}

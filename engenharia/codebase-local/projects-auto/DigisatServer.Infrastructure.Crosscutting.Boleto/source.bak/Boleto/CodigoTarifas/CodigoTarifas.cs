using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoTarifas
{
	// Token: 0x020000FA RID: 250
	public class CodigoTarifas : AbstractCodigoTarifas
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001575F File Offset: 0x0001395F
		public override IBanco Banco
		{
			get
			{
				return this._iCodigoTarifas.Banco;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001576C File Offset: 0x0001396C
		public override int Codigo
		{
			get
			{
				return this._iCodigoTarifas.Codigo;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00015779 File Offset: 0x00013979
		public override string Descricao
		{
			get
			{
				return this._iCodigoTarifas.Descricao;
			}
		}

		// Token: 0x04000434 RID: 1076
		private readonly ICodigoTarifas _iCodigoTarifas;
	}
}

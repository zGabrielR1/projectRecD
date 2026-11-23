using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000086 RID: 134
	public interface IInstrucao
	{
		// Token: 0x06000280 RID: 640
		void Valida();

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000281 RID: 641
		// (set) Token: 0x06000282 RID: 642
		IBanco Banco { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000283 RID: 643
		// (set) Token: 0x06000284 RID: 644
		int Codigo { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000285 RID: 645
		// (set) Token: 0x06000286 RID: 646
		string Descricao { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000287 RID: 647
		// (set) Token: 0x06000288 RID: 648
		int QuantidadeDias { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000289 RID: 649
		bool IsInstrucaoProtesto { get; }
	}
}

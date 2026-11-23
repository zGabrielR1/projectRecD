using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000085 RID: 133
	public abstract class AbstractInstrucao : IInstrucao
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00008756 File Offset: 0x00006956
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000875E File Offset: 0x0000695E
		public virtual IBanco Banco { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008767 File Offset: 0x00006967
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000876F File Offset: 0x0000696F
		public virtual int Codigo { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008778 File Offset: 0x00006978
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00008780 File Offset: 0x00006980
		public virtual string Descricao { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008789 File Offset: 0x00006989
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008791 File Offset: 0x00006991
		public virtual int QuantidadeDias { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600027D RID: 637
		public abstract bool IsInstrucaoProtesto { get; }

		// Token: 0x0600027E RID: 638 RVA: 0x0000879A File Offset: 0x0000699A
		public virtual void Valida()
		{
			throw new NotImplementedException("Função não implementada");
		}
	}
}

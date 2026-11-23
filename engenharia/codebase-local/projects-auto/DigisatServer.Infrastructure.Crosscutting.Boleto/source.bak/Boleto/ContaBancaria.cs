using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x0200002A RID: 42
	public class ContaBancaria
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00005E13 File Offset: 0x00004013
		public ContaBancaria(string agencia, string digitoAgencia, string conta, string digitoConta, string operacaoConta)
		{
			this.Agencia = agencia;
			this.DigitoAgencia = digitoAgencia;
			this.Conta = conta;
			this.DigitoConta = digitoConta;
			this.OperacaoConta = operacaoConta;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public ContaBancaria()
		{
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005E40 File Offset: 0x00004040
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005E48 File Offset: 0x00004048
		public string Agencia { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005E51 File Offset: 0x00004051
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005E59 File Offset: 0x00004059
		public string DigitoAgencia { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005E62 File Offset: 0x00004062
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005E6A File Offset: 0x0000406A
		public string Conta { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005E73 File Offset: 0x00004073
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005E7B File Offset: 0x0000407B
		public string DigitoConta { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005E84 File Offset: 0x00004084
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005E8C File Offset: 0x0000408C
		public string OperacaoConta { get; set; }
	}
}

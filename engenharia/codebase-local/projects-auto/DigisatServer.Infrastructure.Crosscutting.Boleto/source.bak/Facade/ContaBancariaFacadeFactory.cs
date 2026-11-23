using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000015 RID: 21
	public class ContaBancariaFacadeFactory
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00005174 File Offset: 0x00003374
		public ContaBancariaFacadeFactory SetarAgencia(string agencia)
		{
			this._agencia = agencia;
			return this;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000517E File Offset: 0x0000337E
		public ContaBancariaFacadeFactory SetarDigitoAgencia(string digitoAgencia)
		{
			this._digitoAgencia = digitoAgencia;
			return this;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005188 File Offset: 0x00003388
		public ContaBancariaFacadeFactory SetarConta(string conta)
		{
			this._conta = conta;
			return this;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005192 File Offset: 0x00003392
		public ContaBancariaFacadeFactory SetarDigitoConta(string digitoConta)
		{
			this._digitoConta = digitoConta;
			return this;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000519C File Offset: 0x0000339C
		public ContaBancariaFacadeFactory SetarOperacaoConta(string operacaoConta)
		{
			this._operacaoConta = operacaoConta;
			return this;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000051A6 File Offset: 0x000033A6
		public ContaBancariaFacade Criar()
		{
			return new ContaBancariaFacade(this._agencia, this._digitoAgencia, this._conta, this._digitoConta, this._operacaoConta);
		}

		// Token: 0x04000065 RID: 101
		private string _agencia;

		// Token: 0x04000066 RID: 102
		private string _conta;

		// Token: 0x04000067 RID: 103
		private string _digitoAgencia;

		// Token: 0x04000068 RID: 104
		private string _digitoConta;

		// Token: 0x04000069 RID: 105
		private string _operacaoConta;
	}
}

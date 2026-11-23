using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000014 RID: 20
	public class ContaBancariaFacade : IContaBancariaFacade, IBoletoCoreFacade
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00005148 File Offset: 0x00003348
		public ContaBancariaFacade(string agencia, string digitoAgencia, string conta, string digitoConta, string operacaoConta)
		{
			this._contaBancaria = new ContaBancaria(agencia, digitoAgencia, conta, digitoConta, operacaoConta);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005162 File Offset: 0x00003362
		public T Obter<T>() where T : class
		{
			return this._contaBancaria as T;
		}

		// Token: 0x04000064 RID: 100
		private readonly ContaBancaria _contaBancaria;
	}
}

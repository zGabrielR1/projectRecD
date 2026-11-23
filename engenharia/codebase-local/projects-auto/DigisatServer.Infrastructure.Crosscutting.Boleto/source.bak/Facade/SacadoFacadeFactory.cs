using System;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x0200001B RID: 27
	public class SacadoFacadeFactory
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000534E File Offset: 0x0000354E
		public SacadoFacadeFactory SetarCpfCnpj(string cpfCnpj)
		{
			this._cpfCnpj = cpfCnpj;
			return this;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005358 File Offset: 0x00003558
		public SacadoFacadeFactory SetarNome(string nome)
		{
			this._nome = nome;
			return this;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005362 File Offset: 0x00003562
		public SacadoFacadeFactory SetarEnderecoFacade(IEnderecoFacade enderecoFacade)
		{
			this._enderecoFacade = enderecoFacade;
			return this;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000536C File Offset: 0x0000356C
		public SacadoFacadeFactory SetarNaoProtestar(bool naoProtestar)
		{
			this._naoProtestar = naoProtestar;
			return this;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005376 File Offset: 0x00003576
		public SacadoFacade Criar()
		{
			return new SacadoFacade(this._cpfCnpj, this._nome, this._enderecoFacade, this._naoProtestar);
		}

		// Token: 0x04000078 RID: 120
		private string _cpfCnpj;

		// Token: 0x04000079 RID: 121
		private IEnderecoFacade _enderecoFacade;

		// Token: 0x0400007A RID: 122
		private string _nome;

		// Token: 0x0400007B RID: 123
		private bool _naoProtestar;
	}
}

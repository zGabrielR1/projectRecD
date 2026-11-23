using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x0200001A RID: 26
	public class SacadoFacade : ISacadoFacade, IBoletoCoreFacade
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000531F File Offset: 0x0000351F
		public SacadoFacade(string cpfCnpj, string nome, IEnderecoFacade enderecoFacade, bool naoProtestar)
		{
			this._sacado = new Sacado(cpfCnpj, nome, enderecoFacade.Obter<Endereco>(), naoProtestar);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000533C File Offset: 0x0000353C
		public TSacado Obter<TSacado>() where TSacado : class
		{
			return this._sacado as TSacado;
		}

		// Token: 0x04000077 RID: 119
		private readonly Sacado _sacado;
	}
}

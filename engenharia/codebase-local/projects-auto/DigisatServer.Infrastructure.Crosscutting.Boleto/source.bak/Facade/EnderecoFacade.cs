using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000016 RID: 22
	public class EnderecoFacade : IEnderecoFacade, IBoletoCoreFacade
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000051CC File Offset: 0x000033CC
		public EnderecoFacade(string logradouro, string numero, string complemento, string bairro, string cidade, string uf, string cep, string email)
		{
			this._endereco = new Endereco
			{
				Logradouro = logradouro,
				Numero = numero,
				Complemento = complemento,
				Bairro = bairro,
				Cidade = cidade,
				UF = uf,
				Cep = cep,
				Email = email
			};
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005227 File Offset: 0x00003427
		public TEndereco Obter<TEndereco>() where TEndereco : class
		{
			return this._endereco as TEndereco;
		}

		// Token: 0x0400006A RID: 106
		private readonly Endereco _endereco;
	}
}

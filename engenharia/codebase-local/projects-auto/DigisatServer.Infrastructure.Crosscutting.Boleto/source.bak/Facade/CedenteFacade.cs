using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000012 RID: 18
	public class CedenteFacade : ICedenteFacade, IBoletoCoreFacade
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004F8C File Offset: 0x0000318C
		public CedenteFacade(string codigoCedente, int digitoCedente, string cpfcnpj, string nome, string convenio, string contrato, int numeroSequencial, int numeroBordero, string carteira, string variacaoCarteira, int finalidadeArquivoRemessaRetorno, int tipoSolucaoCef, IContaBancariaFacade contaBancariaFacade, IEnderecoFacade enderecoFacade, string beneficiario)
		{
			this._cedente = new Cedente
			{
				Codigo = codigoCedente,
				DigitoCedente = digitoCedente,
				Cpfcnpj = cpfcnpj,
				Nome = nome,
				Convenio = convenio,
				Contrato = contrato,
				NumeroSequencial = numeroSequencial,
				NumeroBordero = numeroBordero,
				Carteira = carteira,
				Variacao = variacaoCarteira,
				FinalidadeArquivoRemessaRetorno = (FinalidadeArquivoRemessaRetorno)finalidadeArquivoRemessaRetorno,
				TipoSolucao = (TipoSolucao)tipoSolucaoCef,
				ContaBancaria = contaBancariaFacade.Obter<ContaBancaria>(),
				Endereco = enderecoFacade.Obter<Endereco>(),
				Beneficiario = beneficiario
			};
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005029 File Offset: 0x00003229
		public TCedente Obter<TCedente>() where TCedente : class
		{
			return this._cedente as TCedente;
		}

		// Token: 0x04000054 RID: 84
		private readonly Cedente _cedente;
	}
}

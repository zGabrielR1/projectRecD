using System;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000013 RID: 19
	public class CedenteFacadeFactory
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000503B File Offset: 0x0000323B
		public CedenteFacadeFactory SetarCodigo(string codigo)
		{
			this._codigo = codigo;
			return this;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005045 File Offset: 0x00003245
		public CedenteFacadeFactory SetarDigito(int digito)
		{
			this._digito = digito;
			return this;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000504F File Offset: 0x0000324F
		public CedenteFacadeFactory SetarCnpj(string cnpj)
		{
			this._cnpj = cnpj;
			return this;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005059 File Offset: 0x00003259
		public CedenteFacadeFactory SetarNome(string nome)
		{
			this._nome = nome;
			return this;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005063 File Offset: 0x00003263
		public CedenteFacadeFactory SetarConvenio(string convenio)
		{
			this._convenio = convenio;
			return this;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000506D File Offset: 0x0000326D
		public CedenteFacadeFactory SetarContrato(string contrato)
		{
			this._contrato = contrato;
			return this;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005077 File Offset: 0x00003277
		public CedenteFacadeFactory SetarNumeroSequencial(int numeroSequencial)
		{
			this._numeroSequencial = numeroSequencial;
			return this;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005081 File Offset: 0x00003281
		public CedenteFacadeFactory SetarNumeroBordero(int numeroBordero)
		{
			this._numeroBordero = numeroBordero;
			return this;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000508B File Offset: 0x0000328B
		public CedenteFacadeFactory SetarCarteira(string carteira)
		{
			this._carteira = carteira;
			return this;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005095 File Offset: 0x00003295
		public CedenteFacadeFactory SetarVariacaoCarteira(string variacaoCarteira)
		{
			this._variacaoCarteira = variacaoCarteira;
			return this;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000509F File Offset: 0x0000329F
		public CedenteFacadeFactory SetarFinalidadeArquivoRemessaRetorno(int finalidadeArquivoRemessaRetorno)
		{
			this._finalidadeArquivoRemessaRetorno = finalidadeArquivoRemessaRetorno;
			return this;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000050A9 File Offset: 0x000032A9
		public CedenteFacadeFactory SetarTipoSolucao(int? tipoSolucao)
		{
			this._tipoSolucao = tipoSolucao.ToInt();
			return this;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000050BD File Offset: 0x000032BD
		public CedenteFacadeFactory SetarContaBancariaFacade(IContaBancariaFacade contaBancariaFacade)
		{
			this._contaBancariaFacade = contaBancariaFacade;
			return this;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000050C7 File Offset: 0x000032C7
		public CedenteFacadeFactory SetarEnderecoFacade(IEnderecoFacade enderecoFacade)
		{
			this._enderecoFacade = enderecoFacade;
			return this;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000050D1 File Offset: 0x000032D1
		public CedenteFacadeFactory SetarBeneficiario(string beneficiario)
		{
			this._beneficiario = beneficiario;
			return this;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000050DC File Offset: 0x000032DC
		public CedenteFacade Criar()
		{
			return new CedenteFacade(this._codigo, this._digito, this._cnpj, this._nome, this._convenio, this._contrato, this._numeroSequencial, this._numeroBordero, this._carteira, this._variacaoCarteira, this._finalidadeArquivoRemessaRetorno, this._tipoSolucao, this._contaBancariaFacade, this._enderecoFacade, this._beneficiario);
		}

		// Token: 0x04000055 RID: 85
		private string _carteira;

		// Token: 0x04000056 RID: 86
		private string _cnpj;

		// Token: 0x04000057 RID: 87
		private string _codigo;

		// Token: 0x04000058 RID: 88
		private IContaBancariaFacade _contaBancariaFacade;

		// Token: 0x04000059 RID: 89
		private string _convenio;

		// Token: 0x0400005A RID: 90
		private string _contrato;

		// Token: 0x0400005B RID: 91
		private int _digito;

		// Token: 0x0400005C RID: 92
		private IEnderecoFacade _enderecoFacade;

		// Token: 0x0400005D RID: 93
		private int _finalidadeArquivoRemessaRetorno;

		// Token: 0x0400005E RID: 94
		private string _nome;

		// Token: 0x0400005F RID: 95
		private int _numeroBordero;

		// Token: 0x04000060 RID: 96
		private int _numeroSequencial;

		// Token: 0x04000061 RID: 97
		private int _tipoSolucao;

		// Token: 0x04000062 RID: 98
		private string _variacaoCarteira;

		// Token: 0x04000063 RID: 99
		private string _beneficiario;
	}
}

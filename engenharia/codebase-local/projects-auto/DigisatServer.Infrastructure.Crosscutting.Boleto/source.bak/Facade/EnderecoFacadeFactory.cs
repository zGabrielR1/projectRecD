using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000017 RID: 23
	public class EnderecoFacadeFactory
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00005239 File Offset: 0x00003439
		public EnderecoFacadeFactory SetarLogradouro(string logradouro)
		{
			this._logradouro = logradouro;
			return this;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005243 File Offset: 0x00003443
		public EnderecoFacadeFactory SetarNumero(string numero)
		{
			this._numero = numero;
			return this;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000524D File Offset: 0x0000344D
		public EnderecoFacadeFactory SetarComplemento(string complemento)
		{
			this._complemento = complemento;
			return this;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005257 File Offset: 0x00003457
		public EnderecoFacadeFactory SetarBairro(string bairro)
		{
			this._bairro = bairro;
			return this;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005261 File Offset: 0x00003461
		public EnderecoFacadeFactory SetarCidade(string cidade)
		{
			this._cidade = cidade;
			return this;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000526B File Offset: 0x0000346B
		public EnderecoFacadeFactory SetarUf(string uf)
		{
			this._uf = uf;
			return this;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005275 File Offset: 0x00003475
		public EnderecoFacadeFactory SetarCep(string cep)
		{
			this._cep = cep;
			return this;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000527F File Offset: 0x0000347F
		public EnderecoFacadeFactory SetarEmail(string email)
		{
			this._email = email;
			return this;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005289 File Offset: 0x00003489
		public EnderecoFacade Criar()
		{
			return new EnderecoFacade(this._logradouro, this._numero, this._complemento, this._bairro, this._cidade, this._uf, this._cep, this._email);
		}

		// Token: 0x0400006B RID: 107
		private string _bairro;

		// Token: 0x0400006C RID: 108
		private string _cep;

		// Token: 0x0400006D RID: 109
		private string _cidade;

		// Token: 0x0400006E RID: 110
		private string _complemento;

		// Token: 0x0400006F RID: 111
		private string _email;

		// Token: 0x04000070 RID: 112
		private string _logradouro;

		// Token: 0x04000071 RID: 113
		private string _numero;

		// Token: 0x04000072 RID: 114
		private string _uf;
	}
}

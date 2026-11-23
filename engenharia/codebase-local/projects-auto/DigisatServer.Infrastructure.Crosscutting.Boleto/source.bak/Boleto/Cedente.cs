using System;
using System.ComponentModel;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x02000028 RID: 40
	[Browsable(false)]
	[Serializable]
	public class Cedente : ICedente
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000599F File Offset: 0x00003B9F
		public Cedente()
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000059BC File Offset: 0x00003BBC
		public Cedente(ContaBancaria contaBancaria)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this._contaBancaria = contaBancaria;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000059E0 File Offset: 0x00003BE0
		public Cedente(string cpfcnpj, string nome)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this.Cpfcnpj = cpfcnpj;
			this._nome = nome;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005A0C File Offset: 0x00003C0C
		public Cedente(string cpfcnpj, string nome, string agencia, string digitoAgencia, string conta, string digitoConta, string operacaoConta)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this.Cpfcnpj = cpfcnpj;
			this._nome = nome;
			this._contaBancaria = new ContaBancaria
			{
				Agencia = agencia,
				DigitoAgencia = digitoAgencia,
				Conta = conta,
				DigitoConta = digitoConta,
				OperacaoConta = operacaoConta
			};
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005A74 File Offset: 0x00003C74
		public Cedente(string cpfcnpj, string nome, string agencia, string digitoAgencia, string conta, string digitoConta)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this.Cpfcnpj = cpfcnpj;
			this._nome = nome;
			this._contaBancaria = new ContaBancaria
			{
				Agencia = agencia,
				DigitoAgencia = digitoAgencia,
				Conta = conta,
				DigitoConta = digitoConta
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public Cedente(string cpfcnpj, string nome, string agencia, string conta, string digitoConta)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this.Cpfcnpj = cpfcnpj;
			this._nome = nome;
			this._contaBancaria = new ContaBancaria
			{
				Agencia = agencia,
				Conta = conta,
				DigitoConta = digitoConta
			};
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005B2C File Offset: 0x00003D2C
		public Cedente(string cpfcnpj, string nome, string agencia, string conta)
		{
			this.TipoSolucao = TipoSolucao.Sigcb;
			this.FinalidadeArquivoRemessaRetorno = FinalidadeArquivoRemessaRetorno.Producao;
			this.Cpfcnpj = cpfcnpj;
			this._nome = nome;
			this._contaBancaria = new ContaBancaria
			{
				Agencia = agencia,
				Conta = conta
			};
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005B7C File Offset: 0x00003D7C
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005B84 File Offset: 0x00003D84
		public string Codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				this._codigo = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005B8D File Offset: 0x00003D8D
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005B95 File Offset: 0x00003D95
		public int DigitoCedente
		{
			get
			{
				return this._digitoCedente;
			}
			set
			{
				this._digitoCedente = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005B9E File Offset: 0x00003D9E
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005BAC File Offset: 0x00003DAC
		public string Cpfcnpj
		{
			get
			{
				return this._cpfcnpj.RemoveCaracteresAlfanumerico();
			}
			set
			{
				if (value == null || (value.Length != 11 && value.Length != 14))
				{
					throw new ArgumentException("O CPF/CNPJ inválido. Utilize 11 dígitos para CPF ou 14 para CNPJ.");
				}
				this._cpfcnpj = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public string CpfCnpjFormatado
		{
			get
			{
				if (this.Cpfcnpj.RemoveCaracteresAlfanumerico().Length != 11)
				{
					return string.Format("CNPJ: {0}", Utils.FormataCnpj(this.Cpfcnpj));
				}
				return string.Format("CPF: {0}", Utils.FormataCpf(this.Cpfcnpj));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005C30 File Offset: 0x00003E30
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005C38 File Offset: 0x00003E38
		public string Nome
		{
			get
			{
				return this._nome;
			}
			set
			{
				this._nome = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005C41 File Offset: 0x00003E41
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005C49 File Offset: 0x00003E49
		public ContaBancaria ContaBancaria
		{
			get
			{
				return this._contaBancaria;
			}
			set
			{
				this._contaBancaria = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005C52 File Offset: 0x00003E52
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005C5A File Offset: 0x00003E5A
		public string Convenio
		{
			get
			{
				return this._convenio;
			}
			set
			{
				this._convenio = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005C63 File Offset: 0x00003E63
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005C6B File Offset: 0x00003E6B
		public string Contrato
		{
			get
			{
				return this._contrato;
			}
			set
			{
				this._contrato = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005C74 File Offset: 0x00003E74
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005C7C File Offset: 0x00003E7C
		public int NumeroSequencial
		{
			get
			{
				return this._numeroSequencial;
			}
			set
			{
				this._numeroSequencial = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005C85 File Offset: 0x00003E85
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005C8D File Offset: 0x00003E8D
		public int NumeroBordero
		{
			get
			{
				return this._numeroBordero;
			}
			set
			{
				this._numeroBordero = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005C96 File Offset: 0x00003E96
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005C9E File Offset: 0x00003E9E
		public string Carteira
		{
			get
			{
				return this._carteira;
			}
			set
			{
				this._carteira = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005CA7 File Offset: 0x00003EA7
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005CAF File Offset: 0x00003EAF
		public string Variacao
		{
			get
			{
				return this._variacao;
			}
			set
			{
				this._variacao = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005CB8 File Offset: 0x00003EB8
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public Endereco Endereco
		{
			get
			{
				return this._endereco;
			}
			set
			{
				this._endereco = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005CCC File Offset: 0x00003ECC
		public string EnderecoToText
		{
			get
			{
				if (!this.Endereco.IsNotNull())
				{
					return string.Empty;
				}
				return string.Format("{0}, {1} - {2} - {3}/{4} - CEP {5}", new object[]
				{
					this.Endereco.Logradouro,
					this.Endereco.Numero,
					this.Endereco.Bairro,
					this.Endereco.Cidade,
					this.Endereco.UF,
					Utils.FormataCep(this.Endereco.Cep)
				});
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005D55 File Offset: 0x00003F55
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005D5D File Offset: 0x00003F5D
		public FinalidadeArquivoRemessaRetorno FinalidadeArquivoRemessaRetorno
		{
			get
			{
				return this._finalidadeArquivoRemessaRetorno;
			}
			set
			{
				this._finalidadeArquivoRemessaRetorno = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005D66 File Offset: 0x00003F66
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00005D6E File Offset: 0x00003F6E
		public TipoSolucao TipoSolucao
		{
			get
			{
				return this._solucao;
			}
			set
			{
				this._solucao = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005D77 File Offset: 0x00003F77
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005D7F File Offset: 0x00003F7F
		public string Beneficiario
		{
			get
			{
				return this._beneficiario;
			}
			set
			{
				this._beneficiario = value;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005D88 File Offset: 0x00003F88
		public override string ToString()
		{
			return string.Format("{0}{1}", this.Codigo.ToString(CultureInfo.InvariantCulture), this.DigitoCedente);
		}

		// Token: 0x04000145 RID: 325
		private string _codigo;

		// Token: 0x04000146 RID: 326
		private string _cpfcnpj;

		// Token: 0x04000147 RID: 327
		private string _nome;

		// Token: 0x04000148 RID: 328
		private ContaBancaria _contaBancaria;

		// Token: 0x04000149 RID: 329
		private string _convenio;

		// Token: 0x0400014A RID: 330
		private string _contrato;

		// Token: 0x0400014B RID: 331
		private int _numeroSequencial;

		// Token: 0x0400014C RID: 332
		private int _numeroBordero;

		// Token: 0x0400014D RID: 333
		private int _digitoCedente = -1;

		// Token: 0x0400014E RID: 334
		private string _carteira;

		// Token: 0x0400014F RID: 335
		private string _variacao;

		// Token: 0x04000150 RID: 336
		private Endereco _endereco;

		// Token: 0x04000151 RID: 337
		private FinalidadeArquivoRemessaRetorno _finalidadeArquivoRemessaRetorno;

		// Token: 0x04000152 RID: 338
		private TipoSolucao _solucao;

		// Token: 0x04000153 RID: 339
		private string _beneficiario;
	}
}

using System;
using System.ComponentModel;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado
{
	// Token: 0x02000084 RID: 132
	[Browsable(false)]
	[Serializable]
	public class Sacado : ISacado
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000084BC File Offset: 0x000066BC
		public Sacado()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000084F0 File Offset: 0x000066F0
		public Sacado(string nome)
		{
			this._nome = nome;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000852C File Offset: 0x0000672C
		public Sacado(string cpfcnpj, string nome)
		{
			this.CpfCnpj = cpfcnpj;
			this._nome = nome;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000857C File Offset: 0x0000677C
		public Sacado(string cpfcnpj, string nome, Endereco endereco, bool naoProtestar)
		{
			this.CpfCnpj = cpfcnpj;
			this._nome = nome;
			this.Endereco = endereco;
			this.NaoProtestar = naoProtestar;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000085D8 File Offset: 0x000067D8
		// (set) Token: 0x0600026B RID: 619 RVA: 0x000085E0 File Offset: 0x000067E0
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000085EC File Offset: 0x000067EC
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008675 File Offset: 0x00006875
		public InformacoesSacado InformacoesSacado { get; } = new InformacoesSacado();

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000867D File Offset: 0x0000687D
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00008688 File Offset: 0x00006888
		public string CpfCnpj
		{
			get
			{
				return this._cpfcnpj;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("Erro. O CPF/CNPJ do cliente não pode ser nulo.");
				}
				if (value == string.Empty)
				{
					this._cpfcnpj = string.Empty;
				}
				else if (value.Length != 11 && value.Length != 14)
				{
					throw new ArgumentException("O CPF/CNPJ inválido. Utilize 11 dígitos para CPF ou 14 para CNPJ.");
				}
				this._cpfcnpj = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000086E8 File Offset: 0x000068E8
		public string CpfCnpjFormatado
		{
			get
			{
				if (this.CpfCnpj.RemoveCaracteresAlfanumerico().Length != 11)
				{
					return string.Format("CNPJ: {0}", Utils.FormataCnpj(this.CpfCnpj));
				}
				return string.Format("CPF: {0}", Utils.FormataCpf(this.CpfCnpj));
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008734 File Offset: 0x00006934
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000873C File Offset: 0x0000693C
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008745 File Offset: 0x00006945
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000874D File Offset: 0x0000694D
		public bool NaoProtestar
		{
			get
			{
				return this._naoProtestar;
			}
			set
			{
				this._naoProtestar = value;
			}
		}

		// Token: 0x040001F0 RID: 496
		private string _cpfcnpj = string.Empty;

		// Token: 0x040001F1 RID: 497
		private string _nome = string.Empty;

		// Token: 0x040001F2 RID: 498
		private bool _naoProtestar;

		// Token: 0x040001F3 RID: 499
		private Endereco _endereco = new Endereco();
	}
}

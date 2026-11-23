using System;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x0200002B RID: 43
	public class Endereco
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005E95 File Offset: 0x00004095
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005E9D File Offset: 0x0000409D
		public string Logradouro { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005EA6 File Offset: 0x000040A6
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005EAE File Offset: 0x000040AE
		public string Numero { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005EB7 File Offset: 0x000040B7
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00005EBF File Offset: 0x000040BF
		public string Complemento { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005EC8 File Offset: 0x000040C8
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005ED0 File Offset: 0x000040D0
		public string Bairro { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005ED9 File Offset: 0x000040D9
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005EE1 File Offset: 0x000040E1
		public string Cidade { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005EEA File Offset: 0x000040EA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005EF2 File Offset: 0x000040F2
		public string UF { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005EFB File Offset: 0x000040FB
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005F03 File Offset: 0x00004103
		public string Cep
		{
			get
			{
				return this._cep;
			}
			set
			{
				this._cep = value.Replace(".", "").Replace("-", "");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005F2A File Offset: 0x0000412A
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005F32 File Offset: 0x00004132
		public string Email { get; set; }

		// Token: 0x0400015D RID: 349
		private string _cep;
	}
}

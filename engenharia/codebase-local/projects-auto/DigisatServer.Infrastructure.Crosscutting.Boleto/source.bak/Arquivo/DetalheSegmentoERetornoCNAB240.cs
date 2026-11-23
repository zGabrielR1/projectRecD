using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000181 RID: 385
	public class DetalheSegmentoERetornoCNAB240
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0006FE2C File Offset: 0x0006E02C
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0006FE34 File Offset: 0x0006E034
		public int CodigoBanco { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0006FE3D File Offset: 0x0006E03D
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0006FE45 File Offset: 0x0006E045
		public int LoteServico { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0006FE4E File Offset: 0x0006E04E
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0006FE56 File Offset: 0x0006E056
		public string TipoRegistro { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0006FE5F File Offset: 0x0006E05F
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0006FE67 File Offset: 0x0006E067
		public int NumeroRegistro { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0006FE70 File Offset: 0x0006E070
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0006FE78 File Offset: 0x0006E078
		public string Segmento { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0006FE81 File Offset: 0x0006E081
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0006FE89 File Offset: 0x0006E089
		public string UsoExclusivoFebrabanCnab { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0006FE92 File Offset: 0x0006E092
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0006FE9A File Offset: 0x0006E09A
		public TipoInscricao TipoInscricaoCliente { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0006FEA3 File Offset: 0x0006E0A3
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0006FEAB File Offset: 0x0006E0AB
		public long NumeroInscricaoCliente { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0006FEBC File Offset: 0x0006E0BC
		public string CodigoConvenioBanco { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0006FEC5 File Offset: 0x0006E0C5
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0006FECD File Offset: 0x0006E0CD
		public int AgenciaMantenedoraConta { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0006FED6 File Offset: 0x0006E0D6
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0006FEDE File Offset: 0x0006E0DE
		public string DigitoVerificadorAgencia { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0006FEE7 File Offset: 0x0006E0E7
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0006FEEF File Offset: 0x0006E0EF
		public long NumeroContaCorrente { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0006FEF8 File Offset: 0x0006E0F8
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0006FF00 File Offset: 0x0006E100
		public string DigitoVerificadorConta { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0006FF09 File Offset: 0x0006E109
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0006FF11 File Offset: 0x0006E111
		public string DigitoVerificadorAgenciaConta { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0006FF1A File Offset: 0x0006E11A
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0006FF22 File Offset: 0x0006E122
		public string NomeEmpresa { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0006FF2B File Offset: 0x0006E12B
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0006FF33 File Offset: 0x0006E133
		public string UsoExclusivoFebrabanCnab2 { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0006FF3C File Offset: 0x0006E13C
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0006FF44 File Offset: 0x0006E144
		public string NaturezaLancamento { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0006FF4D File Offset: 0x0006E14D
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0006FF55 File Offset: 0x0006E155
		public TipoComplementoLancamento TipoComplementoLancamento { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0006FF5E File Offset: 0x0006E15E
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0006FF66 File Offset: 0x0006E166
		public string ComplementoLancamento { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0006FF6F File Offset: 0x0006E16F
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0006FF77 File Offset: 0x0006E177
		public IsencaoCpmf IdentificacaoIsencaoCpmf { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0006FF80 File Offset: 0x0006E180
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0006FF88 File Offset: 0x0006E188
		public DateTime? DataContabil { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0006FF91 File Offset: 0x0006E191
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0006FF99 File Offset: 0x0006E199
		public DateTime DataLancamento { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0006FFA2 File Offset: 0x0006E1A2
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0006FFAA File Offset: 0x0006E1AA
		public decimal ValorLancamento { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0006FFB3 File Offset: 0x0006E1B3
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0006FFBB File Offset: 0x0006E1BB
		public TipoLancamento TipoLancamento { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0006FFC4 File Offset: 0x0006E1C4
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0006FFCC File Offset: 0x0006E1CC
		public CategoriaLancamento CategoriaLancamento { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0006FFD5 File Offset: 0x0006E1D5
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0006FFDD File Offset: 0x0006E1DD
		public string CodigoHistorico { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0006FFE6 File Offset: 0x0006E1E6
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0006FFEE File Offset: 0x0006E1EE
		public string HistoricoLancamento { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0006FFF7 File Offset: 0x0006E1F7
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0006FFFF File Offset: 0x0006E1FF
		public string NumeroDocumentoComplemento { get; private set; }

		// Token: 0x0600093F RID: 2367 RVA: 0x00070008 File Offset: 0x0006E208
		public void LerDetalheSegmentoERetornoCNAB240(string registro)
		{
			try
			{
				if (registro.Substring(13, 1) != "E")
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características do segmento E.");
				}
				this.CodigoBanco = LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 1, 3);
				this.LoteServico = LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 4, 7);
				this.TipoRegistro = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 8, 8);
				this.NumeroRegistro = LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 9, 13);
				this.Segmento = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 14, 14);
				this.UsoExclusivoFebrabanCnab = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 15, 17);
				this.TipoInscricaoCliente = (TipoInscricao)LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 18, 18);
				this.NumeroInscricaoCliente = LeitorLinhaPosicao.ExtrairInt64DaPosicao(registro, 19, 32);
				this.CodigoConvenioBanco = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 33, 52);
				this.AgenciaMantenedoraConta = LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 53, 57);
				this.DigitoVerificadorAgencia = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 58, 58);
				this.NumeroContaCorrente = LeitorLinhaPosicao.ExtrairInt64DaPosicao(registro, 59, 70);
				this.DigitoVerificadorConta = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 71, 71);
				this.DigitoVerificadorAgenciaConta = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 72, 72);
				this.NomeEmpresa = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 73, 102);
				this.UsoExclusivoFebrabanCnab2 = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 103, 108);
				this.NaturezaLancamento = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 109, 111);
				this.TipoComplementoLancamento = (TipoComplementoLancamento)LeitorLinhaPosicao.ExtrairInt32OpcionalDaPosicao(registro, 112, 113).GetValueOrDefault();
				this.ComplementoLancamento = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 114, 133);
				this.IdentificacaoIsencaoCpmf = (IsencaoCpmf)LeitorLinhaPosicao.ExtrairDaPosicao(registro, 134, 134)[0];
				this.DataContabil = LeitorLinhaPosicao.ExtrairDataOpcionalDaPosicao(registro, 135, 142);
				this.DataLancamento = LeitorLinhaPosicao.ExtrairDataDaPosicao(registro, 143, 150);
				this.ValorLancamento = decimal.Parse(LeitorLinhaPosicao.ExtrairDaPosicao(registro, 151, 168)) / 100m;
				this.TipoLancamento = (TipoLancamento)LeitorLinhaPosicao.ExtrairDaPosicao(registro, 169, 169)[0];
				this.CategoriaLancamento = (CategoriaLancamento)LeitorLinhaPosicao.ExtrairInt32DaPosicao(registro, 170, 172);
				this.CodigoHistorico = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 173, 176);
				this.HistoricoLancamento = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 177, 201);
				this.NumeroDocumentoComplemento = LeitorLinhaPosicao.ExtrairDaPosicao(registro, 202, 240);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO E.", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000180 RID: 384
	public class DetalheRetornoCNAB400
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x0006F440 File Offset: 0x0006D640
		public DetalheRetornoCNAB400()
		{
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0006F55C File Offset: 0x0006D75C
		public DetalheRetornoCNAB400(string registro)
		{
			this.Registro = registro;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0006F67F File Offset: 0x0006D87F
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0006F687 File Offset: 0x0006D887
		public int CodigoInscricao { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0006F690 File Offset: 0x0006D890
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0006F698 File Offset: 0x0006D898
		public string NumeroInscricao { get; set; } = string.Empty;

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0006F6A1 File Offset: 0x0006D8A1
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0006F6A9 File Offset: 0x0006D8A9
		public int Agencia { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0006F6B2 File Offset: 0x0006D8B2
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0006F6BA File Offset: 0x0006D8BA
		public int Conta { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0006F6C3 File Offset: 0x0006D8C3
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0006F6CB File Offset: 0x0006D8CB
		public int DacConta { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0006F6D4 File Offset: 0x0006D8D4
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0006F6DC File Offset: 0x0006D8DC
		public string UsoEmpresa { get; set; } = string.Empty;

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0006F6E5 File Offset: 0x0006D8E5
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0006F6ED File Offset: 0x0006D8ED
		public string NossoNumero { get; set; } = string.Empty;

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0006F6F6 File Offset: 0x0006D8F6
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0006F6FE File Offset: 0x0006D8FE
		public int DACNossoNumero { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0006F707 File Offset: 0x0006D907
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0006F70F File Offset: 0x0006D90F
		public string Carteira { get; set; } = string.Empty;

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0006F718 File Offset: 0x0006D918
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0006F720 File Offset: 0x0006D920
		public int CodigoOcorrencia { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0006F729 File Offset: 0x0006D929
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0006F731 File Offset: 0x0006D931
		public string DescricaoOcorrencia { get; set; } = string.Empty;

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0006F73A File Offset: 0x0006D93A
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0006F742 File Offset: 0x0006D942
		public DateTime DataOcorrencia { get; set; } = new DateTime(1, 1, 1);

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0006F74B File Offset: 0x0006D94B
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0006F753 File Offset: 0x0006D953
		public string NumeroDocumento { get; set; } = string.Empty;

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0006F75C File Offset: 0x0006D95C
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0006F764 File Offset: 0x0006D964
		public int ConfirmacaoNossoNumero { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0006F76D File Offset: 0x0006D96D
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0006F775 File Offset: 0x0006D975
		public DateTime DataVencimento { get; set; } = new DateTime(1, 1, 1);

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0006F77E File Offset: 0x0006D97E
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0006F786 File Offset: 0x0006D986
		public decimal ValorTitulo { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0006F78F File Offset: 0x0006D98F
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0006F797 File Offset: 0x0006D997
		public int CodigoBanco { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0006F7A0 File Offset: 0x0006D9A0
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0006F7A8 File Offset: 0x0006D9A8
		public int AgenciaCobradora { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0006F7B1 File Offset: 0x0006D9B1
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0006F7B9 File Offset: 0x0006D9B9
		public int DACAgenciaCobradora { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0006F7C2 File Offset: 0x0006D9C2
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0006F7CA File Offset: 0x0006D9CA
		public int Especie { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0006F7D3 File Offset: 0x0006D9D3
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0006F7DB File Offset: 0x0006D9DB
		public decimal TarifaCobranca { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0006F7E4 File Offset: 0x0006D9E4
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0006F7EC File Offset: 0x0006D9EC
		public decimal IOF { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0006F7F5 File Offset: 0x0006D9F5
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0006F7FD File Offset: 0x0006D9FD
		public decimal ValorAbatimento { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0006F806 File Offset: 0x0006DA06
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0006F80E File Offset: 0x0006DA0E
		public decimal Descontos { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0006F817 File Offset: 0x0006DA17
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0006F81F File Offset: 0x0006DA1F
		public decimal ValorPrincipal { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0006F828 File Offset: 0x0006DA28
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0006F830 File Offset: 0x0006DA30
		public decimal JurosMora { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0006F839 File Offset: 0x0006DA39
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0006F841 File Offset: 0x0006DA41
		public decimal OutrosCreditos { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0006F84A File Offset: 0x0006DA4A
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0006F852 File Offset: 0x0006DA52
		public decimal OutrosDebitos { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0006F85B File Offset: 0x0006DA5B
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0006F863 File Offset: 0x0006DA63
		public DateTime DataCredito { get; set; } = new DateTime(1, 1, 1);

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0006F86C File Offset: 0x0006DA6C
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0006F874 File Offset: 0x0006DA74
		public int InstrucaoCancelada { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0006F87D File Offset: 0x0006DA7D
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0006F885 File Offset: 0x0006DA85
		public string NomeSacado { get; set; } = string.Empty;

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0006F88E File Offset: 0x0006DA8E
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0006F896 File Offset: 0x0006DA96
		public string Erros { get; set; } = string.Empty;

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0006F89F File Offset: 0x0006DA9F
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0006F8A7 File Offset: 0x0006DAA7
		public string CodigoLiquidacao { get; set; } = string.Empty;

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0006F8B0 File Offset: 0x0006DAB0
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
		public int NumeroSequencial { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0006F8C1 File Offset: 0x0006DAC1
		public string Registro { get; } = string.Empty;

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0006F8C9 File Offset: 0x0006DAC9
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0006F8D1 File Offset: 0x0006DAD1
		public decimal ValorDespesa { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0006F8DA File Offset: 0x0006DADA
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0006F8E2 File Offset: 0x0006DAE2
		public decimal ValorOutrasDespesas { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0006F8EB File Offset: 0x0006DAEB
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0006F8F3 File Offset: 0x0006DAF3
		public decimal ValorPago { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0006F8FC File Offset: 0x0006DAFC
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0006F904 File Offset: 0x0006DB04
		public string MotivoCodigoOcorrencia { get; set; } = string.Empty;

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0006F90D File Offset: 0x0006DB0D
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0006F915 File Offset: 0x0006DB15
		public string OrigemPagamento { get; set; } = string.Empty;

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0006F91E File Offset: 0x0006DB1E
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0006F926 File Offset: 0x0006DB26
		public string IdentificacaoTitulo { get; set; } = string.Empty;

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0006F92F File Offset: 0x0006DB2F
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0006F937 File Offset: 0x0006DB37
		public string MotivosRejeicao { get; set; } = string.Empty;

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0006F940 File Offset: 0x0006DB40
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0006F948 File Offset: 0x0006DB48
		public string NumeroProtocolo { get; set; } = string.Empty;

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0006F951 File Offset: 0x0006DB51
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0006F959 File Offset: 0x0006DB59
		public int NumeroCartorio { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0006F962 File Offset: 0x0006DB62
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0006F96A File Offset: 0x0006DB6A
		public string NumeroControle { get; set; } = string.Empty;

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0006F973 File Offset: 0x0006DB73
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0006F97B File Offset: 0x0006DB7B
		public SituacaoBoleto StatusProcessamentoDoBoleto { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0006F984 File Offset: 0x0006DB84
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0006F98C File Offset: 0x0006DB8C
		public int IdentificacaoDoRegistro { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0006F995 File Offset: 0x0006DB95
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0006F99D File Offset: 0x0006DB9D
		public int TipoInscricao { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0006F9A6 File Offset: 0x0006DBA6
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0006F9AE File Offset: 0x0006DBAE
		public string CgcCpf { get; set; } = string.Empty;

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0006F9B7 File Offset: 0x0006DBB7
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0006F9BF File Offset: 0x0006DBBF
		public int ContaCorrente { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0006F9C8 File Offset: 0x0006DBC8
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0006F9D0 File Offset: 0x0006DBD0
		public string SeuNumero { get; set; } = string.Empty;

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0006F9D9 File Offset: 0x0006DBD9
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0006F9E1 File Offset: 0x0006DBE1
		public int Instrucao { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0006F9EA File Offset: 0x0006DBEA
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0006F9F2 File Offset: 0x0006DBF2
		public int CodigoRateio { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0006F9FB File Offset: 0x0006DBFB
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0006FA03 File Offset: 0x0006DC03
		public decimal ValoTitulo { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0006FA0C File Offset: 0x0006DC0C
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0006FA14 File Offset: 0x0006DC14
		public int BancoCobrador { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0006FA1D File Offset: 0x0006DC1D
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0006FA25 File Offset: 0x0006DC25
		public string EspecieTitulo { get; set; } = string.Empty;

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0006FA2E File Offset: 0x0006DC2E
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0006FA36 File Offset: 0x0006DC36
		public decimal DespesasDeCobranca { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0006FA3F File Offset: 0x0006DC3F
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0006FA47 File Offset: 0x0006DC47
		public decimal OutrasDespesas { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0006FA50 File Offset: 0x0006DC50
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0006FA58 File Offset: 0x0006DC58
		public decimal Juros { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0006FA61 File Offset: 0x0006DC61
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0006FA69 File Offset: 0x0006DC69
		public decimal Abatimentos { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0006FA72 File Offset: 0x0006DC72
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0006FA7A File Offset: 0x0006DC7A
		public DateTime DataLiquidacao { get; set; } = new DateTime(1, 1, 1);

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0006FA83 File Offset: 0x0006DC83
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0006FA8B File Offset: 0x0006DC8B
		public int Sequencial { get; set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x0006FA94 File Offset: 0x0006DC94
		public void LerDetalheRetornoCNAB400(string registro)
		{
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(146, 6));
				int num3 = Utils.ToInt32(registro.Substring(295, 6));
				this.CodigoInscricao = Utils.ToInt32(registro.Substring(1, 2));
				this.NumeroInscricao = registro.Substring(3, 14);
				this.Agencia = Utils.ToInt32(registro.Substring(17, 4));
				this.Conta = Utils.ToInt32(registro.Substring(23, 5));
				this.DacConta = Utils.ToInt32(registro.Substring(28, 1));
				this.UsoEmpresa = registro.Substring(37, 25);
				this.NossoNumero = Utils.ToString(registro.Substring(85, 8));
				this.DACNossoNumero = Utils.ToInt32(registro.Substring(93, 1));
				this.Carteira = registro.Substring(107, 1);
				this.CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2));
				this.DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"));
				this.NumeroDocumento = registro.Substring(116, 10);
				this.NossoNumero = Utils.ToString(registro.Substring(126, 9));
				this.DataVencimento = Utils.ToDateTime(num2.ToString("##-##-##"));
				decimal num4 = Convert.ToInt64(registro.Substring(152, 13));
				this.ValorTitulo = num4 / 100m;
				this.CodigoBanco = Utils.ToInt32(registro.Substring(165, 3));
				this.AgenciaCobradora = Utils.ToInt32(registro.Substring(168, 4));
				this.Especie = Utils.ToInt32(registro.Substring(173, 2));
				decimal num5 = Convert.ToUInt64(registro.Substring(175, 13));
				this.TarifaCobranca = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(214, 13));
				this.IOF = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(227, 13));
				this.ValorAbatimento = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(253, 13));
				this.ValorPrincipal = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(266, 13));
				this.JurosMora = num9 / 100m;
				this.DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"));
				this.DataCredito = Utils.ToDateTime(num3.ToString("##-##-##"));
				this.InstrucaoCancelada = Utils.ToInt32(registro.Substring(301, 4));
				this.NomeSacado = registro.Substring(324, 30);
				this.Erros = registro.Substring(377, 8);
				this.CodigoLiquidacao = registro.Substring(392, 2);
				this.NumeroSequencial = Utils.ToInt32(registro.Substring(394, 6));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0006FDF4 File Offset: 0x0006DFF4
		public static string PrimeiroCaracter(string retorno)
		{
			string text;
			try
			{
				text = retorno.Substring(0, 1);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao desmembrar registro.", ex);
			}
			return text;
		}
	}
}

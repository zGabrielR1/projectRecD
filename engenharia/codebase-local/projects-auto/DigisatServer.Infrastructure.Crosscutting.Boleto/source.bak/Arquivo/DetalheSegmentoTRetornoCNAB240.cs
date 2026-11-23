using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000182 RID: 386
	public class DetalheSegmentoTRetornoCNAB240
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00070278 File Offset: 0x0006E478
		public DetalheSegmentoTRetornoCNAB240()
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0007028B File Offset: 0x0006E48B
		public DetalheSegmentoTRetornoCNAB240(string registro)
		{
			this.Registro = registro;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000702A5 File Offset: 0x0006E4A5
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x000702AD File Offset: 0x0006E4AD
		public int IdCodigoMovimento { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000702B6 File Offset: 0x0006E4B6
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x000702BE File Offset: 0x0006E4BE
		public int CodigoBanco { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000702C7 File Offset: 0x0006E4C7
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x000702CF File Offset: 0x0006E4CF
		public string Registro { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000702D8 File Offset: 0x0006E4D8
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x000702FD File Offset: 0x0006E4FD
		public CodigoMovimento CodigoMovimento
		{
			get
			{
				this._codigoMovimento = new CodigoMovimento(this.CodigoBanco, this.IdCodigoMovimento, this._solucao);
				return this._codigoMovimento;
			}
			set
			{
				this._codigoMovimento = value;
				this.IdCodigoMovimento = this._codigoMovimento.Codigo;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00070317 File Offset: 0x0006E517
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0007031F File Offset: 0x0006E51F
		public int Agencia { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00070328 File Offset: 0x0006E528
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00070330 File Offset: 0x0006E530
		public string DigitoAgencia { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00070339 File Offset: 0x0006E539
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00070341 File Offset: 0x0006E541
		public long Conta { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0007034A File Offset: 0x0006E54A
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00070352 File Offset: 0x0006E552
		public string DigitoConta { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0007035B File Offset: 0x0006E55B
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00070363 File Offset: 0x0006E563
		public int DACAgenciaConta { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0007036C File Offset: 0x0006E56C
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00070374 File Offset: 0x0006E574
		public string NossoNumero { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0007037D File Offset: 0x0006E57D
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00070385 File Offset: 0x0006E585
		public int CodigoCarteira { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0007038E File Offset: 0x0006E58E
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00070396 File Offset: 0x0006E596
		public string NumeroDocumento { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0007039F File Offset: 0x0006E59F
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x000703A7 File Offset: 0x0006E5A7
		public DateTime DataVencimento { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000703B0 File Offset: 0x0006E5B0
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x000703B8 File Offset: 0x0006E5B8
		public decimal ValorTitulo { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x000703C1 File Offset: 0x0006E5C1
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x000703C9 File Offset: 0x0006E5C9
		public string IdentificacaoTituloEmpresa { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000703D2 File Offset: 0x0006E5D2
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x000703DA File Offset: 0x0006E5DA
		public int TipoInscricao { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000703E3 File Offset: 0x0006E5E3
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x000703EB File Offset: 0x0006E5EB
		public string NumeroInscricao { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x000703F4 File Offset: 0x0006E5F4
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x000703FC File Offset: 0x0006E5FC
		public string NomeSacado { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00070405 File Offset: 0x0006E605
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0007040D File Offset: 0x0006E60D
		public decimal ValorTarifas { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00070416 File Offset: 0x0006E616
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0007041E File Offset: 0x0006E61E
		public string CodigoRejeicao { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00070427 File Offset: 0x0006E627
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0007042F File Offset: 0x0006E62F
		public List<CodigoRejeicao> MotivosRejeicao { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00070438 File Offset: 0x0006E638
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00070440 File Offset: 0x0006E640
		public string UsoEmpresa { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00070449 File Offset: 0x0006E649
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x00070451 File Offset: 0x0006E651
		public SituacaoBoleto StatusProcessamentoDoBoleto { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0007045A File Offset: 0x0006E65A
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x00070462 File Offset: 0x0006E662
		public List<DetalheSegmentoTRetornoCNAB240> ListaDetalhe { get; set; } = new List<DetalheSegmentoTRetornoCNAB240>();

		// Token: 0x06000973 RID: 2419 RVA: 0x0007046C File Offset: 0x0006E66C
		public void LerDetalheSegmentoTRetornoCNAB240(string registro, TipoSolucao solucao = TipoSolucao.Sigcb)
		{
			try
			{
				this.Registro = registro;
				this._solucao = solucao;
				if (registro.Substring(13, 1) != "T")
				{
					throw new Exception("Registro inválido. O detalhe não possui as características do segmento T.");
				}
				this.CodigoBanco = Convert.ToInt32(registro.Substring(0, 3));
				int num;
				int.TryParse(registro.Substring(15, 2), out num);
				this.IdCodigoMovimento = num;
				this.Agencia = Convert.ToInt32(registro.Substring(17, 5));
				this.DigitoAgencia = registro.Substring(22, 1);
				this.Conta = Convert.ToInt64(registro.Substring(23, 12));
				this.DigitoConta = registro.Substring(35, 1);
				this.NossoNumero = registro.Substring(36, 20);
				this.CodigoCarteira = Convert.ToInt32(registro.Substring(57, 1));
				this.NumeroDocumento = registro.Substring(58, 15);
				if (!string.IsNullOrWhiteSpace(registro.Substring(73, 8)))
				{
					int num2 = Convert.ToInt32(registro.Substring(73, 8));
					if (num2 != 0)
					{
						this.DataVencimento = Convert.ToDateTime(num2.ToString("##-##-####"));
					}
				}
				decimal num3 = Convert.ToInt64(registro.Substring(81, 15));
				this.ValorTitulo = num3 / 100m;
				this.IdentificacaoTituloEmpresa = registro.Substring(105, 25);
				this.UsoEmpresa = registro.Substring(105, 25);
				this.TipoInscricao = Convert.ToInt32(registro.Substring(132, 1));
				this.NumeroInscricao = registro.Substring(133, 15);
				this.NomeSacado = registro.Substring(148, 40);
				decimal num4 = Convert.ToUInt64(registro.Substring(198, 15));
				this.ValorTarifas = num4 / 100m;
				this.CodigoRejeicao = registro.Substring(213, 10);
				int idCodigoMovimento = this.IdCodigoMovimento;
				if (idCodigoMovimento <= 36)
				{
					switch (idCodigoMovimento)
					{
					case 2:
					case 7:
					case 8:
					case 12:
					case 13:
					case 14:
					case 19:
					case 20:
					case 27:
						goto IL_02A5;
					case 3:
					case 26:
					case 30:
						break;
					case 4:
					case 5:
					case 10:
					case 11:
					case 15:
					case 16:
					case 18:
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 28:
					case 29:
						goto IL_02E4;
					case 6:
					case 17:
						goto IL_029C;
					case 9:
						this.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
						goto IL_02EB;
					default:
						if (idCodigoMovimento != 36)
						{
							goto IL_02E4;
						}
						break;
					}
					this.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					this.MotivosRejeicao = this.RetornarMotivosDasRejeicoes(this.CodigoRejeicao);
					goto IL_02EB;
				}
				if (idCodigoMovimento != 61)
				{
					switch (idCodigoMovimento)
					{
					case 78:
					case 79:
					case 81:
					case 83:
						goto IL_02A5;
					case 80:
					case 82:
					case 85:
						this.StatusProcessamentoDoBoleto = SituacaoBoleto.InclusaoNegativacao;
						goto IL_02EB;
					case 84:
						goto IL_02E4;
					case 86:
						this.StatusProcessamentoDoBoleto = SituacaoBoleto.ExclusaoNegativacao;
						goto IL_02EB;
					default:
						goto IL_02E4;
					}
				}
				IL_029C:
				this.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
				goto IL_02EB;
				IL_02A5:
				this.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
				goto IL_02EB;
				IL_02E4:
				this.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoLiquidado;
				IL_02EB:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO T.", ex);
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00070794 File Offset: 0x0006E994
		public List<CodigoRejeicao> RetornarMotivosDasRejeicoes(string codigosDeRejeicoes)
		{
			string[] array = Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace);
			List<CodigoRejeicao> list = new List<CodigoRejeicao>();
			foreach (string text in array)
			{
				if (text.Length > 0)
				{
					CodigoRejeicao codigoRejeicao = new CodigoRejeicao(this.CodigoBanco, text, this._solucao);
					list.Add(codigoRejeicao);
				}
			}
			return list;
		}

		// Token: 0x040006DF RID: 1759
		private CodigoMovimento _codigoMovimento;

		// Token: 0x040006E0 RID: 1760
		private TipoSolucao _solucao;
	}
}

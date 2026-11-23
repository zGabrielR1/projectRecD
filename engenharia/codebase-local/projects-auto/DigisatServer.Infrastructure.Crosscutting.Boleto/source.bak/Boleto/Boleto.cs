using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto
{
	// Token: 0x02000026 RID: 38
	[Browsable(false)]
	[Serializable]
	public class Boleto
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005395 File Offset: 0x00003595
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000539D File Offset: 0x0000359D
		public int Categoria
		{
			get
			{
				return this._categoria;
			}
			set
			{
				this._categoria = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000053A6 File Offset: 0x000035A6
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000053AE File Offset: 0x000035AE
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000053B7 File Offset: 0x000035B7
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000053BF File Offset: 0x000035BF
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000053C8 File Offset: 0x000035C8
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000053D0 File Offset: 0x000035D0
		public string CodigoExclusivoBanese
		{
			get
			{
				return this._codigoExclusivoBanese;
			}
			set
			{
				this._codigoExclusivoBanese = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000053D9 File Offset: 0x000035D9
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000053E1 File Offset: 0x000035E1
		public DateTime DataVencimento
		{
			get
			{
				return this._dataVencimento;
			}
			set
			{
				this._dataVencimento = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000053EA File Offset: 0x000035EA
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000053F2 File Offset: 0x000035F2
		public decimal ValorBoleto
		{
			get
			{
				return this._valorBoleto;
			}
			set
			{
				this._valorBoleto = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000053FB File Offset: 0x000035FB
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005403 File Offset: 0x00003603
		public decimal ValorCobrado
		{
			get
			{
				return this._valorCobrado;
			}
			set
			{
				this._valorCobrado = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000540C File Offset: 0x0000360C
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005414 File Offset: 0x00003614
		public string LocalPagamento
		{
			get
			{
				return this._localPagamento;
			}
			set
			{
				this._localPagamento = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000541D File Offset: 0x0000361D
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005425 File Offset: 0x00003625
		public int QuantidadeMoeda
		{
			get
			{
				return this._quantidadeMoeda;
			}
			set
			{
				this._quantidadeMoeda = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000542E File Offset: 0x0000362E
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005436 File Offset: 0x00003636
		public string ValorMoeda
		{
			get
			{
				return this._valorMoeda;
			}
			set
			{
				this._valorMoeda = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000543F File Offset: 0x0000363F
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005447 File Offset: 0x00003647
		public string Aceite
		{
			get
			{
				return this._aceite;
			}
			set
			{
				this._aceite = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005450 File Offset: 0x00003650
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005458 File Offset: 0x00003658
		public string Especie
		{
			get
			{
				return this._especie;
			}
			set
			{
				this._especie = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005464 File Offset: 0x00003664
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005489 File Offset: 0x00003689
		public IEspecieDocumento EspecieDocumento
		{
			get
			{
				IEspecieDocumento especieDocumento;
				if ((especieDocumento = this._especieDocumento) == null)
				{
					especieDocumento = (this._especieDocumento = new EspecieDocumento());
				}
				return especieDocumento;
			}
			set
			{
				this._especieDocumento = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005492 File Offset: 0x00003692
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000549A File Offset: 0x0000369A
		public DateTime DataDocumento
		{
			get
			{
				return this._dataDocumento;
			}
			set
			{
				this._dataDocumento = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000054A3 File Offset: 0x000036A3
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000054AB File Offset: 0x000036AB
		public DateTime DataProcessamento
		{
			get
			{
				return this._dataProcessamento;
			}
			set
			{
				this._dataProcessamento = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000054B4 File Offset: 0x000036B4
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000054BC File Offset: 0x000036BC
		public int NumeroParcela
		{
			get
			{
				return this._numeroParcela;
			}
			set
			{
				this._numeroParcela = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000054C5 File Offset: 0x000036C5
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000054CD File Offset: 0x000036CD
		public string NumeroDocumento
		{
			get
			{
				return this._numeroDocumento;
			}
			set
			{
				this._numeroDocumento = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000054D6 File Offset: 0x000036D6
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000054DE File Offset: 0x000036DE
		public string IdentificacaoTituloEmpresa
		{
			get
			{
				return this._identificacaoTituloEmpresa;
			}
			set
			{
				this._identificacaoTituloEmpresa = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000054E7 File Offset: 0x000036E7
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000054EF File Offset: 0x000036EF
		public string DigitoNossoNumero
		{
			get
			{
				return this._digitoNossoNumero;
			}
			set
			{
				this._digitoNossoNumero = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000054F8 File Offset: 0x000036F8
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005500 File Offset: 0x00003700
		public string NossoNumero
		{
			get
			{
				return this._nossoNumero;
			}
			set
			{
				this._nossoNumero = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005509 File Offset: 0x00003709
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005511 File Offset: 0x00003711
		public string NossoNumeroSemFormatacao
		{
			get
			{
				return this._nossoNumeroSemFormatacao;
			}
			set
			{
				this._nossoNumeroSemFormatacao = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000551A File Offset: 0x0000371A
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005522 File Offset: 0x00003722
		public string NossoNumeroImpressao
		{
			get
			{
				return this._nossoNumeroImpressao;
			}
			set
			{
				this._nossoNumeroImpressao = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000552B File Offset: 0x0000372B
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005533 File Offset: 0x00003733
		public int Moeda
		{
			get
			{
				return this._moeda;
			}
			set
			{
				this._moeda = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000553C File Offset: 0x0000373C
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005544 File Offset: 0x00003744
		public Cedente Cedente
		{
			get
			{
				return this._cedente;
			}
			set
			{
				this._cedente = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000554D File Offset: 0x0000374D
		public ICedente CedenteImpresso
		{
			get
			{
				return this.Cedente;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005555 File Offset: 0x00003755
		public CodigoBarra CodigoBarra { get; } = new CodigoBarra();

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000555D File Offset: 0x0000375D
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005565 File Offset: 0x00003765
		public IBanco Banco
		{
			get
			{
				return this._banco;
			}
			set
			{
				this._banco = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000556E File Offset: 0x0000376E
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005576 File Offset: 0x00003776
		public decimal ValorDesconto
		{
			get
			{
				return this._valorDesconto;
			}
			set
			{
				this._valorDesconto = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000557F File Offset: 0x0000377F
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005587 File Offset: 0x00003787
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado.Sacado Sacado
		{
			get
			{
				return this._sacado;
			}
			set
			{
				this._sacado = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005590 File Offset: 0x00003790
		public ISacado SacadoImpresso
		{
			get
			{
				return this.Sacado;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005598 File Offset: 0x00003798
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000055A0 File Offset: 0x000037A0
		public string UsoBanco
		{
			get
			{
				return this._usoBanco;
			}
			set
			{
				this._usoBanco = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000055A9 File Offset: 0x000037A9
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000055B1 File Offset: 0x000037B1
		public decimal PercJurosMora
		{
			get
			{
				return this._percJurosMora;
			}
			set
			{
				this._percJurosMora = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000055BA File Offset: 0x000037BA
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000055C2 File Offset: 0x000037C2
		public decimal JurosMora
		{
			get
			{
				return this._jurosMora;
			}
			set
			{
				this._jurosMora = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000055CB File Offset: 0x000037CB
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000055D3 File Offset: 0x000037D3
		public decimal IOF
		{
			get
			{
				return this._iof;
			}
			set
			{
				this._iof = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000055DC File Offset: 0x000037DC
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000055E4 File Offset: 0x000037E4
		public decimal Abatimento
		{
			get
			{
				return this._abatimento;
			}
			set
			{
				this._abatimento = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000055ED File Offset: 0x000037ED
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000055F5 File Offset: 0x000037F5
		public decimal PercMulta
		{
			get
			{
				return this._percMulta;
			}
			set
			{
				this._percMulta = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000055FE File Offset: 0x000037FE
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005606 File Offset: 0x00003806
		public decimal ValorMulta
		{
			get
			{
				return this._valorMulta;
			}
			set
			{
				this._valorMulta = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000560F File Offset: 0x0000380F
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005617 File Offset: 0x00003817
		public decimal OutrosAcrescimos
		{
			get
			{
				return this._outrosAcrescimos;
			}
			set
			{
				this._outrosAcrescimos = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005620 File Offset: 0x00003820
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005628 File Offset: 0x00003828
		public decimal OutrosDescontos
		{
			get
			{
				return this._outrosDescontos;
			}
			set
			{
				this._outrosDescontos = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005631 File Offset: 0x00003831
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005639 File Offset: 0x00003839
		public DateTime DataJurosMora
		{
			get
			{
				return this._dataJurosMora;
			}
			set
			{
				this._dataJurosMora = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005642 File Offset: 0x00003842
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000564A File Offset: 0x0000384A
		public DateTime DataMulta
		{
			get
			{
				return this._dataMulta;
			}
			set
			{
				this._dataMulta = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005653 File Offset: 0x00003853
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000565B File Offset: 0x0000385B
		public DateTime DataDesconto
		{
			get
			{
				return this._dataDesconto;
			}
			set
			{
				this._dataDesconto = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005664 File Offset: 0x00003864
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000566C File Offset: 0x0000386C
		public DateTime DataOutrosAcrescimos
		{
			get
			{
				return this._dataOutrosAcrescimos;
			}
			set
			{
				this._dataOutrosAcrescimos = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005675 File Offset: 0x00003875
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000567D File Offset: 0x0000387D
		public DateTime DataOutrosDescontos
		{
			get
			{
				return this._dataOutrosDescontos;
			}
			set
			{
				this._dataOutrosDescontos = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005686 File Offset: 0x00003886
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000568E File Offset: 0x0000388E
		public string TipoModalidade
		{
			get
			{
				return this._tipoModalidade;
			}
			set
			{
				this._tipoModalidade = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005697 File Offset: 0x00003897
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000569F File Offset: 0x0000389F
		public List<IInstrucao> Instrucoes
		{
			get
			{
				return this._instrucoes;
			}
			set
			{
				this._instrucoes = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000056A8 File Offset: 0x000038A8
		public string InstrucoesToText
		{
			get
			{
				return string.Join("\n", this.Instrucoes.Select<IInstrucao, string>((IInstrucao x) => x.Descricao));
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000056DE File Offset: 0x000038DE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000056E6 File Offset: 0x000038E6
		public ITipoEmissao TipoEmissao
		{
			get
			{
				return this._tipoEmissao;
			}
			set
			{
				this._tipoEmissao = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000056EF File Offset: 0x000038EF
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000056F7 File Offset: 0x000038F7
		public ITipoTransmissao TipoTransmissao
		{
			get
			{
				return this._tipoTransmissao;
			}
			set
			{
				this._tipoTransmissao = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005700 File Offset: 0x00003900
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005708 File Offset: 0x00003908
		public ITipoCobranca TipoCobranca
		{
			get
			{
				return this._tipoCobranca;
			}
			set
			{
				this._tipoCobranca = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005711 File Offset: 0x00003911
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00005719 File Offset: 0x00003919
		public IFormaCadastramento FormaCadastramento
		{
			get
			{
				return this._formaCadastramento;
			}
			set
			{
				this._formaCadastramento = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005722 File Offset: 0x00003922
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000572A File Offset: 0x0000392A
		public ITipoDistribuicao TipoDistribuicao
		{
			get
			{
				return this._tipoDistribuicao;
			}
			set
			{
				this._tipoDistribuicao = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005733 File Offset: 0x00003933
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000573B File Offset: 0x0000393B
		public bool SolicitacaoDeBaixa
		{
			get
			{
				return this._solicitacaoDeBaixa;
			}
			set
			{
				this._solicitacaoDeBaixa = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005744 File Offset: 0x00003944
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000574C File Offset: 0x0000394C
		public bool AlteracaoVencimento
		{
			get
			{
				return this._alteracaoVencimento;
			}
			set
			{
				this._alteracaoVencimento = value;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005758 File Offset: 0x00003958
		public void Valida()
		{
			if (this.Cedente == null)
			{
				throw new Exception("Cedente não informado.");
			}
			if (this.DataProcessamento == DateTime.MinValue)
			{
				this.DataProcessamento = DateTime.Now;
			}
			if (this.DataDocumento == DateTime.MinValue)
			{
				this.DataDocumento = DateTime.Now;
			}
			this.Banco.ValidaBoleto(this);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000057C0 File Offset: 0x000039C0
		public void FormataCampos()
		{
			try
			{
				this.QuantidadeMoeda = 0;
				this.Banco.FormataCodigoBarra(this);
				this.Banco.FormataLinhaDigitavel(this);
				this.Banco.FormataNossoNumero(this);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação dos campos.", ex);
			}
		}

		// Token: 0x0400010E RID: 270
		private string _carteira = "";

		// Token: 0x0400010F RID: 271
		private string _variacao = "";

		// Token: 0x04000110 RID: 272
		private string _codigoExclusivoBanese = "";

		// Token: 0x04000111 RID: 273
		private string _nossoNumero = "";

		// Token: 0x04000112 RID: 274
		private string _nossoNumeroSemFormatacao = "";

		// Token: 0x04000113 RID: 275
		private string _digitoNossoNumero = "";

		// Token: 0x04000114 RID: 276
		private string _nossoNumeroImpressao = "";

		// Token: 0x04000115 RID: 277
		private DateTime _dataVencimento;

		// Token: 0x04000116 RID: 278
		private DateTime _dataDocumento;

		// Token: 0x04000117 RID: 279
		private DateTime _dataProcessamento;

		// Token: 0x04000118 RID: 280
		private int _numeroParcela;

		// Token: 0x04000119 RID: 281
		private decimal _valorBoleto;

		// Token: 0x0400011A RID: 282
		private decimal _valorCobrado;

		// Token: 0x0400011B RID: 283
		private string _localPagamento;

		// Token: 0x0400011C RID: 284
		private int _quantidadeMoeda = 1;

		// Token: 0x0400011D RID: 285
		private string _valorMoeda = "";

		// Token: 0x0400011E RID: 286
		private List<IInstrucao> _instrucoes = new List<IInstrucao>();

		// Token: 0x0400011F RID: 287
		private IEspecieDocumento _especieDocumento = new EspecieDocumento();

		// Token: 0x04000120 RID: 288
		private string _aceite;

		// Token: 0x04000121 RID: 289
		private string _numeroDocumento;

		// Token: 0x04000122 RID: 290
		private string _identificacaoTituloEmpresa;

		// Token: 0x04000123 RID: 291
		private string _especie;

		// Token: 0x04000124 RID: 292
		private int _moeda = 9;

		// Token: 0x04000125 RID: 293
		private string _usoBanco = string.Empty;

		// Token: 0x04000126 RID: 294
		private Cedente _cedente;

		// Token: 0x04000127 RID: 295
		private int _categoria;

		// Token: 0x04000128 RID: 296
		private IBanco _banco;

		// Token: 0x04000129 RID: 297
		private ContaBancaria _contaBancaria = new ContaBancaria();

		// Token: 0x0400012A RID: 298
		private decimal _valorDesconto;

		// Token: 0x0400012B RID: 299
		private global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado.Sacado _sacado;

		// Token: 0x0400012C RID: 300
		private decimal _percJurosMora;

		// Token: 0x0400012D RID: 301
		private decimal _jurosMora;

		// Token: 0x0400012E RID: 302
		private decimal _iof;

		// Token: 0x0400012F RID: 303
		private decimal _abatimento;

		// Token: 0x04000130 RID: 304
		private decimal _percMulta;

		// Token: 0x04000131 RID: 305
		private decimal _valorMulta;

		// Token: 0x04000132 RID: 306
		private decimal _outrosAcrescimos;

		// Token: 0x04000133 RID: 307
		private decimal _outrosDescontos;

		// Token: 0x04000134 RID: 308
		private DateTime _dataJurosMora;

		// Token: 0x04000135 RID: 309
		private DateTime _dataMulta;

		// Token: 0x04000136 RID: 310
		private DateTime _dataDesconto;

		// Token: 0x04000137 RID: 311
		private DateTime _dataOutrosAcrescimos;

		// Token: 0x04000138 RID: 312
		private DateTime _dataOutrosDescontos;

		// Token: 0x04000139 RID: 313
		private string _tipoModalidade = string.Empty;

		// Token: 0x0400013A RID: 314
		private ITipoEmissao _tipoEmissao = new TipoEmissao();

		// Token: 0x0400013B RID: 315
		private ITipoTransmissao _tipoTransmissao = new TipoTransmissao();

		// Token: 0x0400013C RID: 316
		private ITipoCobranca _tipoCobranca = new TipoCobranca();

		// Token: 0x0400013D RID: 317
		private IFormaCadastramento _formaCadastramento = new FormaCadastramento();

		// Token: 0x0400013E RID: 318
		private ITipoDistribuicao _tipoDistribuicao = new TipoDistribuicao();

		// Token: 0x0400013F RID: 319
		private bool _solicitacaoDeBaixa;

		// Token: 0x04000140 RID: 320
		private bool _alteracaoVencimento;
	}
}

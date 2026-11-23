using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000157 RID: 343
	public class Banco : AbstractBanco, IBanco
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0002BFD5 File Offset: 0x0002A1D5
		internal Banco()
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002BFDD File Offset: 0x0002A1DD
		public Banco(int codigoBanco)
		{
			this.InstanciaBanco(codigoBanco);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0002BFEC File Offset: 0x0002A1EC
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0002BFF9 File Offset: 0x0002A1F9
		public override int Codigo
		{
			get
			{
				return this._iBanco.Codigo;
			}
			set
			{
				this._iBanco.Codigo = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0002C007 File Offset: 0x0002A207
		public override string Digito
		{
			get
			{
				return this._iBanco.Digito;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0002C014 File Offset: 0x0002A214
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0002C021 File Offset: 0x0002A221
		public override int SequenciaRemessaDiaria
		{
			get
			{
				return this._iBanco.SequenciaRemessaDiaria;
			}
			set
			{
				this._iBanco.SequenciaRemessaDiaria = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0002C02F File Offset: 0x0002A22F
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0002C03C File Offset: 0x0002A23C
		public override int NumeroSequencialArquivo
		{
			get
			{
				return this._iBanco.NumeroSequencialArquivo;
			}
			set
			{
				this._iBanco.NumeroSequencialArquivo = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0002C04A File Offset: 0x0002A24A
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0002C057 File Offset: 0x0002A257
		public override string CodigoCedenteParaComporNomeDoArquivoRemessa
		{
			get
			{
				return this._iBanco.CodigoCedenteParaComporNomeDoArquivoRemessa;
			}
			set
			{
				this._iBanco.CodigoCedenteParaComporNomeDoArquivoRemessa = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0002C065 File Offset: 0x0002A265
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0002C072 File Offset: 0x0002A272
		public override string ConvenioParaComporNomeDoArquivoRemessa
		{
			get
			{
				return this._iBanco.ConvenioParaComporNomeDoArquivoRemessa;
			}
			set
			{
				this._iBanco.ConvenioParaComporNomeDoArquivoRemessa = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0002C080 File Offset: 0x0002A280
		public override string NomeArquivoRemessa
		{
			get
			{
				return this._iBanco.NomeArquivoRemessa;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0002C08D File Offset: 0x0002A28D
		public override string Nome
		{
			get
			{
				return this._iBanco.Nome;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0002C09A File Offset: 0x0002A29A
		public override string FormatoAgenciaCodigoCedente
		{
			get
			{
				return this._iBanco.FormatoAgenciaCodigoCedente;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0002C0A7 File Offset: 0x0002A2A7
		public override string InstrucaoSacAtendimento
		{
			get
			{
				return this._iBanco.InstrucaoSacAtendimento;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0002C0B4 File Offset: 0x0002A2B4
		public override string DescricaoBeneficiarioSacadorAvalista
		{
			get
			{
				return this._iBanco.DescricaoBeneficiarioSacadorAvalista;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		private void InstanciaBanco(int codigoBanco)
		{
			try
			{
				if (codigoBanco <= 208)
				{
					if (codigoBanco <= 47)
					{
						if (codigoBanco <= 8)
						{
							if (codigoBanco == 1)
							{
								this._iBanco = new BancoBrasil();
								goto IL_0310;
							}
							if (codigoBanco == 4)
							{
								this._iBanco = new BancoNordeste();
								goto IL_0310;
							}
							if (codigoBanco != 8)
							{
								goto IL_02FA;
							}
						}
						else if (codigoBanco != 33)
						{
							if (codigoBanco == 41)
							{
								this._iBanco = new BancoBanrisul();
								goto IL_0310;
							}
							if (codigoBanco != 47)
							{
								goto IL_02FA;
							}
							this._iBanco = new BancoBanese();
							goto IL_0310;
						}
					}
					else if (codigoBanco <= 97)
					{
						if (codigoBanco == 77)
						{
							this._iBanco = new BancoIntermedium();
							goto IL_0310;
						}
						if (codigoBanco == 85)
						{
							this._iBanco = new BancoAilos();
							goto IL_0310;
						}
						if (codigoBanco != 97)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoCredisis();
						goto IL_0310;
					}
					else if (codigoBanco <= 133)
					{
						if (codigoBanco == 104)
						{
							this._iBanco = new BancoCaixa();
							goto IL_0310;
						}
						if (codigoBanco != 133)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoCresol();
						goto IL_0310;
					}
					else
					{
						if (codigoBanco == 136)
						{
							this._iBanco = new BancoUnicred();
							goto IL_0310;
						}
						if (codigoBanco != 208)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoBtg();
						goto IL_0310;
					}
				}
				else if (codigoBanco <= 364)
				{
					if (codigoBanco <= 323)
					{
						if (codigoBanco == 237)
						{
							this._iBanco = new BancoBradesco();
							goto IL_0310;
						}
						if (codigoBanco == 290)
						{
							this._iBanco = new BancoPagbank();
							goto IL_0310;
						}
						if (codigoBanco != 323)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoMercadoPago();
						goto IL_0310;
					}
					else if (codigoBanco <= 341)
					{
						if (codigoBanco == 336)
						{
							this._iBanco = new BancoC6bank();
							goto IL_0310;
						}
						if (codigoBanco != 341)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoItau();
						goto IL_0310;
					}
					else if (codigoBanco != 353)
					{
						if (codigoBanco != 364)
						{
							goto IL_02FA;
						}
						this._iBanco = new BancoEfi();
						goto IL_0310;
					}
				}
				else if (codigoBanco <= 422)
				{
					if (codigoBanco == 399)
					{
						this._iBanco = new BancoHsbc();
						goto IL_0310;
					}
					if (codigoBanco == 403)
					{
						this._iBanco = new BancoCora();
						goto IL_0310;
					}
					if (codigoBanco != 422)
					{
						goto IL_02FA;
					}
					this._iBanco = new BancoSafra();
					goto IL_0310;
				}
				else if (codigoBanco <= 707)
				{
					if (codigoBanco == 461)
					{
						this._iBanco = new BancoAsaas();
						goto IL_0310;
					}
					if (codigoBanco != 707)
					{
						goto IL_02FA;
					}
					this._iBanco = new BancoDaycoval();
					goto IL_0310;
				}
				else
				{
					if (codigoBanco == 748)
					{
						this._iBanco = new BancoSicredi();
						goto IL_0310;
					}
					if (codigoBanco != 756)
					{
						goto IL_02FA;
					}
					this._iBanco = new BancoSicoob();
					goto IL_0310;
				}
				this._iBanco = new BancoSantander();
				goto IL_0310;
				IL_02FA:
				throw new Exception("Código do banco não implementado: " + codigoBanco);
				IL_0310:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002C418 File Offset: 0x0002A618
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this._iBanco.FormataCodigoBarra(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação do código de barra.", ex);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002C450 File Offset: 0x0002A650
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this._iBanco.FormataLinhaDigitavel(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação da linha digitável.", ex);
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002C488 File Offset: 0x0002A688
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this._iBanco.FormataNossoNumero(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação do nosso número.", ex);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this._iBanco.FormataNossoNumeroImpressao(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação do nosso número impressão.", ex);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this._iBanco.FormataNumeroDocumento(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a formatação do número do documento.", ex);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002C530 File Offset: 0x0002A730
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			ITipoTransmissao tipoTransmissao = boleto.TipoTransmissao;
			if (((tipoTransmissao != null) ? tipoTransmissao.Descricao : null) == "DePagar")
			{
				return;
			}
			this._iBanco.ValidaBoleto(boleto);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0002C55D File Offset: 0x0002A75D
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this._iBanco.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0002C570 File Offset: 0x0002A770
		public override string GerarDetalheRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002C5AC File Offset: 0x0002A7AC
		public override string GerarDetalheDadosPagadorAvalistaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheDadosPagadorAvalistaRemessa(boleto, numeroRegistro, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE com dados do pagador/avalista do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
		public override string GerarDetalheDadosMultaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheDadosMultaRemessa(boleto, numeroRegistro, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE com dados da multa do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002C624 File Offset: 0x0002A824
		public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
		{
			string text;
			try
			{
				text = this._iBanco.GerarTrailerRemessa(numeroRegistro, tipoArquivo, cedente, vltitulostotal);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002C664 File Offset: 0x0002A864
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = this._iBanco.GerarHeaderRemessa(cedente, tipoArquivo, numeroArquivoRemessa);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro HEADER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002C6A0 File Offset: 0x0002A8A0
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = this._iBanco.GerarHeaderLoteRemessa(numeroConvenio, cedente, numeroArquivoRemessa);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro HEADER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = this._iBanco.GerarHeaderLoteRemessa(numeroConvenio, cedente, numeroArquivoRemessa, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro HEADER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002C71C File Offset: 0x0002A91C
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheSegmentoPRemessa(boleto, numeroRegistro, numeroConvenio);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002C758 File Offset: 0x0002A958
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio, Cedente cedente)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheSegmentoPRemessa(boleto, numeroRegistro, numeroConvenio, cedente);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002C798 File Offset: 0x0002A998
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheSegmentoQRemessa(boleto, numeroRegistro, tipoArquivo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo cnab240)
		{
			string text;
			try
			{
				text = this._iBanco.GerarDetalheSegmentoRRemessa(boleto, numeroRegistro, cnab240);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração dos registros de DETALHE do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002C810 File Offset: 0x0002AA10
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			string text;
			try
			{
				text = this._iBanco.GerarTrailerArquivoRemessa(numeroRegistro);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002C84C File Offset: 0x0002AA4C
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = this._iBanco.GerarTrailerLoteRemessa(numeroRegistro, numeroRegistroSegmentoPQR, quantidadeBoletos, vltitulostotal);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002C88C File Offset: 0x0002AA8C
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			return this._iBanco.LerDetalheSegmentoTRetornoCNAB240(registro);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002C89A File Offset: 0x0002AA9A
		public override DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro)
		{
			return this._iBanco.LerDetalheSegmentoURetornoCNAB240(registro);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		public override DetalheSegmentoWRetornoCNAB240 LerDetalheSegmentoWRetornoCNAB240(string registro)
		{
			return this._iBanco.LerDetalheSegmentoWRetornoCNAB240(registro);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002C8B6 File Offset: 0x0002AAB6
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			return this._iBanco.LerDetalheRetornoCNAB400(registro);
		}

		// Token: 0x04000631 RID: 1585
		private IBanco _iBanco;
	}
}

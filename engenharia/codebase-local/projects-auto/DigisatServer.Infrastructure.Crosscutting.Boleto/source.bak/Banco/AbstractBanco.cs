using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000156 RID: 342
	public abstract class AbstractBanco
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0002B7DD File Offset: 0x000299DD
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0002B7E5 File Offset: 0x000299E5
		public virtual int Codigo { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0002B7EE File Offset: 0x000299EE
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0002B7F6 File Offset: 0x000299F6
		public virtual string Digito { get; protected set; } = "0";

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0002B7FF File Offset: 0x000299FF
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0002B807 File Offset: 0x00029A07
		public virtual string Nome { get; protected set; } = string.Empty;

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0002B810 File Offset: 0x00029A10
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0002B818 File Offset: 0x00029A18
		public virtual string NomeArquivoRemessa { get; protected set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0002B821 File Offset: 0x00029A21
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0002B829 File Offset: 0x00029A29
		public virtual string FormatoAgenciaCodigoCedente { get; protected set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0002B832 File Offset: 0x00029A32
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0002B83A File Offset: 0x00029A3A
		public virtual string InstrucaoSacAtendimento { get; protected set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0002B843 File Offset: 0x00029A43
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0002B84B File Offset: 0x00029A4B
		public virtual string DescricaoBeneficiarioSacadorAvalista { get; protected set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0002B854 File Offset: 0x00029A54
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0002B85C File Offset: 0x00029A5C
		public virtual int SequenciaRemessaDiaria { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0002B865 File Offset: 0x00029A65
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0002B86D File Offset: 0x00029A6D
		public virtual int NumeroSequencialArquivo { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0002B876 File Offset: 0x00029A76
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0002B87E File Offset: 0x00029A7E
		public virtual string CodigoCedenteParaComporNomeDoArquivoRemessa { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0002B887 File Offset: 0x00029A87
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0002B88F File Offset: 0x00029A8F
		public virtual string ConvenioParaComporNomeDoArquivoRemessa { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0002B898 File Offset: 0x00029A98
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0002B8A0 File Offset: 0x00029AA0
		public virtual Cedente Cedente { get; protected set; }

		// Token: 0x060004EE RID: 1262 RVA: 0x0002B8A9 File Offset: 0x00029AA9
		public virtual string CampoFixo()
		{
			throw new NotImplementedException("Função não implementada");
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return string.Empty;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheDadosPagadorAvalistaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheDadosMultaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
		{
			return string.Empty;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return string.Empty;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			return string.Empty;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio, Cedente cedente)
		{
			return string.Empty;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			return string.Empty;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente)
		{
			return string.Empty;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return string.Empty;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			return string.Empty;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			return string.Empty;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002B8C8 File Offset: 0x00029AC8
		public virtual DetalheSegmentoWRetornoCNAB240 LerDetalheSegmentoWRetornoCNAB240(string registro)
		{
			DetalheSegmentoWRetornoCNAB240 detalheSegmentoWRetornoCNAB = new DetalheSegmentoWRetornoCNAB240();
			detalheSegmentoWRetornoCNAB.LerDetalheSegmentoWRetornoCNAB240(registro);
			return detalheSegmentoWRetornoCNAB;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002B8D6 File Offset: 0x00029AD6
		public virtual DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = new DetalheSegmentoTRetornoCNAB240(registro);
			detalheSegmentoTRetornoCNAB.LerDetalheSegmentoTRetornoCNAB240(registro, TipoSolucao.Sigcb);
			return detalheSegmentoTRetornoCNAB;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002B8E6 File Offset: 0x00029AE6
		public virtual DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro)
		{
			DetalheSegmentoURetornoCNAB240 detalheSegmentoURetornoCNAB = new DetalheSegmentoURetornoCNAB240(registro);
			detalheSegmentoURetornoCNAB.LerDetalheSegmentoURetornoCNAB240(registro);
			return detalheSegmentoURetornoCNAB;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		public virtual DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(146, 6));
				int num3 = Utils.ToInt32(registro.Substring(295, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro);
				detalheRetornoCNAB.CodigoInscricao = Utils.ToInt32(registro.Substring(1, 2));
				detalheRetornoCNAB.NumeroInscricao = registro.Substring(3, 14);
				detalheRetornoCNAB.Agencia = Utils.ToInt32(registro.Substring(17, 4));
				detalheRetornoCNAB.Conta = Utils.ToInt32(registro.Substring(23, 5));
				detalheRetornoCNAB.DacConta = Utils.ToInt32(registro.Substring(28, 1));
				detalheRetornoCNAB.UsoEmpresa = registro.Substring(37, 25);
				detalheRetornoCNAB.NossoNumero = registro.Substring(85, 8);
				detalheRetornoCNAB.DACNossoNumero = Utils.ToInt32(registro.Substring(93, 1));
				detalheRetornoCNAB.Carteira = registro.Substring(107, 1);
				detalheRetornoCNAB.CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2));
				detalheRetornoCNAB.DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"));
				detalheRetornoCNAB.NumeroDocumento = registro.Substring(116, 10);
				detalheRetornoCNAB.NossoNumero = registro.Substring(126, 9);
				detalheRetornoCNAB.DataVencimento = Utils.ToDateTime(num2.ToString("##-##-##"));
				decimal num4 = Convert.ToInt64(registro.Substring(152, 13));
				detalheRetornoCNAB.ValorTitulo = num4 / 100m;
				detalheRetornoCNAB.CodigoBanco = Utils.ToInt32(registro.Substring(165, 3));
				detalheRetornoCNAB.AgenciaCobradora = Utils.ToInt32(registro.Substring(168, 4));
				detalheRetornoCNAB.Especie = Utils.ToInt32(registro.Substring(173, 2));
				decimal num5 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.TarifaCobranca = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(214, 13));
				detalheRetornoCNAB.IOF = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(227, 13));
				detalheRetornoCNAB.ValorAbatimento = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num9 / 100m;
				detalheRetornoCNAB.DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"));
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num3.ToString("##-##-##"));
				detalheRetornoCNAB.InstrucaoCancelada = Utils.ToInt32(registro.Substring(301, 4));
				detalheRetornoCNAB.NomeSacado = registro.Substring(324, 30);
				detalheRetornoCNAB.Erros = registro.Substring(377, 8);
				detalheRetornoCNAB.CodigoLiquidacao = registro.Substring(392, 2);
				detalheRetornoCNAB.NumeroSequencial = Utils.ToInt32(registro.Substring(394, 6));
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002BC58 File Offset: 0x00029E58
		public static long FatorVencimento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			DateTime dateTime = new DateTime(1997, 10, 7, 0, 0, 0);
			DateTime dateTime2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			long num = Utils.DateDiff(DigisatServer.Infrastructure.Crosscutting.Boleto.Util.DateInterval.Day, dateTime2, boleto.DataVencimento);
			if (num > 5500L || num < -3000L)
			{
				throw new Exception("Data do vencimento fora do range de utilização proposto pela CENEGESC. Comunicado FEBRABAN de n° 082/2012 de 14/06/2012");
			}
			while (boleto.DataVencimento > dateTime.AddDays(9999.0))
			{
				dateTime = boleto.DataVencimento.AddDays((double)(-(double)(Utils.DateDiff(DigisatServer.Infrastructure.Crosscutting.Boleto.Util.DateInterval.Day, dateTime, boleto.DataVencimento) - 9999L - 1L + 1000L)));
			}
			return Utils.DateDiff(DigisatServer.Infrastructure.Crosscutting.Boleto.Util.DateInterval.Day, dateTime, boleto.DataVencimento);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002BD2C File Offset: 0x00029F2C
		internal static int Mod10(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length; i > 0; i--)
			{
				int num3 = Convert.ToInt32(Strings.Mid(seq, i, 1)) * num2;
				if (num3 > 9)
				{
					num3 = num3 / 10 + num3 % 10;
				}
				num += num3;
				if (num2 == 2)
				{
					num2 = 1;
				}
				else
				{
					num2++;
				}
			}
			return (10 - num % 10) % 10;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002BD88 File Offset: 0x00029F88
		protected static int Mod11(string seq)
		{
			int num = 0;
			int num2 = 2;
			foreach (char c in seq)
			{
				num += Convert.ToInt32(c) * num2;
				if (num2 < 9)
				{
					num2++;
				}
				else
				{
					num2 = 2;
				}
			}
			int num3 = 11 - num % 11;
			if (num3 > 9)
			{
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		protected static int Mod11(string seq, int b)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length; i > 0; i--)
			{
				num += Convert.ToInt32(Strings.Mid(seq, i, 1)) * num2;
				if (num2 == b)
				{
					num2 = 2;
				}
				else
				{
					num2++;
				}
			}
			int num3 = 11 - num % 11;
			if (num3 > 9 || num3 == 0 || num3 == 1)
			{
				num3 = 1;
			}
			return num3;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002BE3C File Offset: 0x0002A03C
		protected static int Mod11Base9(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				string text = Convert.ToString(seq[i]);
				num += Convert.ToInt32(text) * num2;
				if (num2 >= 9)
				{
					num2 = 2;
				}
				else
				{
					num2++;
				}
			}
			if (num < 11)
			{
				return 11 - num;
			}
			int num3 = 11 - num % 11;
			if (num3 > 9 || num3 == 0)
			{
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		protected static int Mod11(string seq, int lim, int flag)
		{
			int num = 0;
			int i = 1;
			int num2 = 1 + seq.Length % (lim - 1);
			if (num2 == 1)
			{
				num2 = lim;
			}
			while (i <= seq.Length)
			{
				string text = Strings.Mid(seq, i, 1);
				num += Convert.ToInt32(text) * num2;
				num2--;
				if (num2 == 1)
				{
					num2 = lim;
				}
				i++;
			}
			int num3 = num % 11;
			if (flag == 1)
			{
				return num3;
			}
			int num4;
			if (num3 == 0 || num3 == 1 || num3 == 10)
			{
				num4 = 1;
			}
			else
			{
				num4 = 11 - num3;
			}
			return num4;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002BF20 File Offset: 0x0002A120
		protected static int Mult10Mod11(string seq, int lim, int flag)
		{
			int num = 0;
			int i = 1;
			int num2 = 1 + seq.Length % (lim - 1);
			if (num2 == 1)
			{
				num2 = lim;
			}
			while (i <= seq.Length)
			{
				string text = Strings.Mid(seq, i, 1);
				num += Convert.ToInt32(text) * num2;
				num2--;
				if (num2 == 1)
				{
					num2 = lim;
				}
				i++;
			}
			int num3 = num * 10 % 11;
			if (flag == 1)
			{
				return num3;
			}
			int num4;
			if (num3 == 0 || num3 == 1 || num3 == 10)
			{
				num4 = 1;
			}
			else
			{
				num4 = num3;
			}
			return num4;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002BF98 File Offset: 0x0002A198
		protected static int Multiplo10(int soma)
		{
			int i;
			for (i = 0; i < soma; i += 10)
			{
			}
			return i - soma;
		}
	}
}

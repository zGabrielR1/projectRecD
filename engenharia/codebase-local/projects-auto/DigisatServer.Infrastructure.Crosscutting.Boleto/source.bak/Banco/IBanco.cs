using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000176 RID: 374
	public interface IBanco
	{
		// Token: 0x06000819 RID: 2073
		void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081A RID: 2074
		void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081B RID: 2075
		void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081C RID: 2076
		void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081D RID: 2077
		void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081E RID: 2078
		void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto);

		// Token: 0x0600081F RID: 2079
		string GerarHeaderRemessa(string numeroConvenio, Cedente cendente, TipoArquivo tipoArquivo, int numeroArquivoRemessa);

		// Token: 0x06000820 RID: 2080
		string GerarDetalheRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

		// Token: 0x06000821 RID: 2081
		string GerarDetalheDadosPagadorAvalistaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

		// Token: 0x06000822 RID: 2082
		string GerarDetalheDadosMultaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

		// Token: 0x06000823 RID: 2083
		string GerarHeaderRemessa(Cedente cendente, TipoArquivo tipoArquivo, int numeroArquivoRemessa);

		// Token: 0x06000824 RID: 2084
		string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal);

		// Token: 0x06000825 RID: 2085
		string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cendente, int numeroArquivoRemessa);

		// Token: 0x06000826 RID: 2086
		string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cendente, int numeroArquivoRemessa, TipoArquivo tipoArquivo);

		// Token: 0x06000827 RID: 2087
		string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio);

		// Token: 0x06000828 RID: 2088
		string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio, Cedente cedente);

		// Token: 0x06000829 RID: 2089
		string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

		// Token: 0x0600082A RID: 2090
		string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

		// Token: 0x0600082B RID: 2091
		string GerarTrailerArquivoRemessa(int numeroRegistro);

		// Token: 0x0600082C RID: 2092
		string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m);

		// Token: 0x0600082D RID: 2093
		DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro);

		// Token: 0x0600082E RID: 2094
		DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro);

		// Token: 0x0600082F RID: 2095
		DetalheSegmentoWRetornoCNAB240 LerDetalheSegmentoWRetornoCNAB240(string registro);

		// Token: 0x06000830 RID: 2096
		DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro);

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000831 RID: 2097
		Cedente Cedente { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000832 RID: 2098
		// (set) Token: 0x06000833 RID: 2099
		int Codigo { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000834 RID: 2100
		string Nome { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000835 RID: 2101
		string Digito { get; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000836 RID: 2102
		// (set) Token: 0x06000837 RID: 2103
		int SequenciaRemessaDiaria { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000838 RID: 2104
		// (set) Token: 0x06000839 RID: 2105
		int NumeroSequencialArquivo { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600083A RID: 2106
		// (set) Token: 0x0600083B RID: 2107
		string CodigoCedenteParaComporNomeDoArquivoRemessa { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600083C RID: 2108
		// (set) Token: 0x0600083D RID: 2109
		string ConvenioParaComporNomeDoArquivoRemessa { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600083E RID: 2110
		string NomeArquivoRemessa { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600083F RID: 2111
		string FormatoAgenciaCodigoCedente { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000840 RID: 2112
		string InstrucaoSacAtendimento { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000841 RID: 2113
		string DescricaoBeneficiarioSacadorAvalista { get; }
	}
}

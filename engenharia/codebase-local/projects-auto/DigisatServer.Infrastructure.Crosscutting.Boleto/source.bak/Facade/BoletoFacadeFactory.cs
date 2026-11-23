using System;
using System.Collections.Generic;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000011 RID: 17
	public class BoletoFacadeFactory
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00004D30 File Offset: 0x00002F30
		public BoletoFacadeFactory SetarAceite(string aceite)
		{
			this._aceite = aceite;
			return this;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004D3A File Offset: 0x00002F3A
		public BoletoFacadeFactory SetarCarteira(string carteira)
		{
			this._carteira = carteira;
			return this;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004D44 File Offset: 0x00002F44
		public BoletoFacadeFactory SetarVariacao(string variacao)
		{
			this._variacao = variacao;
			return this;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D4E File Offset: 0x00002F4E
		public BoletoFacadeFactory SetarLocalPagamento(string localPagamento)
		{
			this._localPagamento = localPagamento;
			return this;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004D58 File Offset: 0x00002F58
		public BoletoFacadeFactory SetarDataDocumento(DateTime dataDocumento)
		{
			this._dataDocumento = dataDocumento;
			return this;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004D62 File Offset: 0x00002F62
		public BoletoFacadeFactory SetarNumeroDocumento(string numeroDocumento)
		{
			this._numeroDocumento = numeroDocumento;
			return this;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004D6C File Offset: 0x00002F6C
		public BoletoFacadeFactory SetarDataProcessamento(DateTime dataProcessamento)
		{
			this._dataProcessamento = dataProcessamento;
			return this;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004D76 File Offset: 0x00002F76
		public BoletoFacadeFactory SetarDataVencimento(DateTime dataVencimento)
		{
			this._dataVencimento = dataVencimento;
			return this;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004D80 File Offset: 0x00002F80
		public BoletoFacadeFactory SetarNossoNumero(string nossoNumero)
		{
			this._nossoNumero = nossoNumero;
			return this;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004D8A File Offset: 0x00002F8A
		public BoletoFacadeFactory SetarDigitoNossoNumero(string digitoNossoNumero)
		{
			this._digitoNossoNumero = digitoNossoNumero;
			return this;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004D94 File Offset: 0x00002F94
		public BoletoFacadeFactory SetarIdentificacaoTituloEmpresa(string identificacaoTituloEmpresa)
		{
			this._identificacaoTituloEmpresa = identificacaoTituloEmpresa;
			return this;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D9E File Offset: 0x00002F9E
		public BoletoFacadeFactory SetarNumeroParcela(int numeroParcela)
		{
			this._numeroParcela = numeroParcela;
			return this;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public BoletoFacadeFactory SetarValorJurosMora(decimal valorJurosMora)
		{
			this._valorJurosMora = valorJurosMora;
			return this;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004DB2 File Offset: 0x00002FB2
		public BoletoFacadeFactory SetarPercentualJurosMora(decimal percentualJurosMora)
		{
			this._percentualJurosMora = percentualJurosMora;
			return this;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004DBC File Offset: 0x00002FBC
		public BoletoFacadeFactory SetarValorMulta(decimal valorMulta)
		{
			this._valorMulta = valorMulta;
			return this;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public BoletoFacadeFactory SetarPercentualMulta(decimal percentualMulta)
		{
			this._percentualMulta = percentualMulta;
			return this;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public BoletoFacadeFactory SetarValorBoleto(decimal valorBoleto)
		{
			this._valorBoleto = valorBoleto;
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004DDA File Offset: 0x00002FDA
		public BoletoFacadeFactory SetarValorCobrado(decimal valorCobrado)
		{
			this._valorCobrado = valorCobrado;
			return this;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public BoletoFacadeFactory SetarBancoFacade(IBancoFacade bancoFacade)
		{
			this._bancoFacade = bancoFacade;
			return this;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004DEE File Offset: 0x00002FEE
		public BoletoFacadeFactory SetarCedenteFacade(ICedenteFacade cedenteFacade)
		{
			this._cedenteFacade = cedenteFacade;
			return this;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public BoletoFacadeFactory SetarSacadoFacade(ISacadoFacade sacadoFacade)
		{
			this._sacadoFacade = sacadoFacade;
			return this;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004E02 File Offset: 0x00003002
		public BoletoFacadeFactory SetarPrimeiraInstrucaoFacade(IInstrucaoFacade primeiraInstrucaoFacade)
		{
			this._primeiraInstrucaoFacade = primeiraInstrucaoFacade;
			return this;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004E0C File Offset: 0x0000300C
		public BoletoFacadeFactory SetarSegundaInstrucaoFacade(IInstrucaoFacade segundaInstrucaoFacade)
		{
			this._segundaInstrucaoFacade = segundaInstrucaoFacade;
			return this;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004E16 File Offset: 0x00003016
		public BoletoFacadeFactory AdicionarInstrucao(string instrucao)
		{
			this._instrucoes.Add(instrucao);
			return this;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004E25 File Offset: 0x00003025
		public BoletoFacadeFactory AdicionarInstrucoes(IEnumerable<string> instrucoes)
		{
			this._instrucoes.AddRange(instrucoes);
			return this;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004E34 File Offset: 0x00003034
		public BoletoFacadeFactory SetarDiasIniciarCobrancaDeJuros(int diasIniciarCobrancaDeJuros)
		{
			this._diasIniciarCobrancaDeJuros = diasIniciarCobrancaDeJuros;
			return this;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004E3E File Offset: 0x0000303E
		public BoletoFacadeFactory SetarDiasIniciarCobrancaDeMulta(int diasIniciarCobrancaDeMulta)
		{
			this._diasIniciarCobrancaDeMulta = diasIniciarCobrancaDeMulta;
			return this;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E48 File Offset: 0x00003048
		public BoletoFacadeFactory SetarDiasAntesVencimentoParaAplicarDesconto(int diasAntesVencimentoParaAplicarDesconto)
		{
			this._diasAntesVencimentoParaAplicarDesconto = diasAntesVencimentoParaAplicarDesconto;
			return this;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004E52 File Offset: 0x00003052
		public BoletoFacadeFactory SetarPercentualDesconto(decimal percentualDesconto)
		{
			this._percentualDesconto = percentualDesconto;
			return this;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004E5C File Offset: 0x0000305C
		public BoletoFacadeFactory SetarMostrarComprovanteEntrega(bool mostrarComprovanteEntrega)
		{
			this._mostrarComprovanteEntrega = mostrarComprovanteEntrega;
			return this;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004E66 File Offset: 0x00003066
		public BoletoFacadeFactory SetarFormatoCarne(bool formatoCarne)
		{
			this._formatoCarne = formatoCarne;
			return this;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004E70 File Offset: 0x00003070
		public BoletoFacadeFactory SetarSolicitacaoDeBaixa(bool solicitacaoDeBaixa)
		{
			this._solicitacaoDeBaixa = solicitacaoDeBaixa;
			return this;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004E7A File Offset: 0x0000307A
		public BoletoFacadeFactory SetarTipoArquivo(TipoArquivo tipoArquivo)
		{
			this._tipoArquivo = tipoArquivo;
			return this;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E84 File Offset: 0x00003084
		public BoletoFacadeFactory SetarAlteracaoVencimento(bool alteracaoVencimento)
		{
			this._alteracaoVencimento = alteracaoVencimento;
			return this;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004E8E File Offset: 0x0000308E
		public BoletoFacadeFactory SetarCodigoExclusivoBanese(string codigoExclusivoBanese)
		{
			this._codigoExclusivoBanese = codigoExclusivoBanese;
			return this;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004E98 File Offset: 0x00003098
		public BoletoFacade Criar()
		{
			return new BoletoFacade(this._aceite, this._carteira, this._variacao, this._localPagamento, this._dataDocumento, this._numeroDocumento, this._dataProcessamento, this._dataVencimento, this._nossoNumero, this._digitoNossoNumero, this._identificacaoTituloEmpresa, this._numeroParcela, this._percentualJurosMora, this._valorJurosMora, this._percentualMulta, this._valorMulta, this._valorBoleto, this._valorCobrado, this._bancoFacade, this._cedenteFacade, this._sacadoFacade, this._primeiraInstrucaoFacade, this._segundaInstrucaoFacade, this._instrucoes, this._diasIniciarCobrancaDeJuros, this._diasIniciarCobrancaDeMulta, this._diasAntesVencimentoParaAplicarDesconto, this._percentualDesconto, this._mostrarComprovanteEntrega, this._formatoCarne, this._solicitacaoDeBaixa, this._tipoArquivo, this._alteracaoVencimento, this._codigoExclusivoBanese);
		}

		// Token: 0x04000032 RID: 50
		private string _aceite;

		// Token: 0x04000033 RID: 51
		private IBancoFacade _bancoFacade;

		// Token: 0x04000034 RID: 52
		private string _carteira;

		// Token: 0x04000035 RID: 53
		private ICedenteFacade _cedenteFacade;

		// Token: 0x04000036 RID: 54
		private DateTime _dataDocumento;

		// Token: 0x04000037 RID: 55
		private DateTime _dataProcessamento;

		// Token: 0x04000038 RID: 56
		private DateTime _dataVencimento;

		// Token: 0x04000039 RID: 57
		private string _digitoNossoNumero;

		// Token: 0x0400003A RID: 58
		private string _identificacaoTituloEmpresa;

		// Token: 0x0400003B RID: 59
		private readonly List<string> _instrucoes = new List<string>();

		// Token: 0x0400003C RID: 60
		private string _localPagamento;

		// Token: 0x0400003D RID: 61
		private string _nossoNumero;

		// Token: 0x0400003E RID: 62
		private string _numeroDocumento;

		// Token: 0x0400003F RID: 63
		private int _numeroParcela;

		// Token: 0x04000040 RID: 64
		private decimal _percentualJurosMora;

		// Token: 0x04000041 RID: 65
		private decimal _percentualMulta;

		// Token: 0x04000042 RID: 66
		private IInstrucaoFacade _primeiraInstrucaoFacade;

		// Token: 0x04000043 RID: 67
		private ISacadoFacade _sacadoFacade;

		// Token: 0x04000044 RID: 68
		private IInstrucaoFacade _segundaInstrucaoFacade;

		// Token: 0x04000045 RID: 69
		private decimal _valorBoleto;

		// Token: 0x04000046 RID: 70
		private decimal _valorCobrado;

		// Token: 0x04000047 RID: 71
		private decimal _valorJurosMora;

		// Token: 0x04000048 RID: 72
		private decimal _valorMulta;

		// Token: 0x04000049 RID: 73
		private string _variacao;

		// Token: 0x0400004A RID: 74
		private int _diasIniciarCobrancaDeJuros;

		// Token: 0x0400004B RID: 75
		private int _diasIniciarCobrancaDeMulta;

		// Token: 0x0400004C RID: 76
		private int _diasAntesVencimentoParaAplicarDesconto;

		// Token: 0x0400004D RID: 77
		private decimal _percentualDesconto;

		// Token: 0x0400004E RID: 78
		private bool _mostrarComprovanteEntrega;

		// Token: 0x0400004F RID: 79
		private bool _formatoCarne;

		// Token: 0x04000050 RID: 80
		private bool _solicitacaoDeBaixa;

		// Token: 0x04000051 RID: 81
		private TipoArquivo _tipoArquivo;

		// Token: 0x04000052 RID: 82
		private bool _alteracaoVencimento;

		// Token: 0x04000053 RID: 83
		private string _codigoExclusivoBanese;
	}
}

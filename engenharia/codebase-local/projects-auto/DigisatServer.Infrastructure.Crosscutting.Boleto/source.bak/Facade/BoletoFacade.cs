using System;
using System.Collections.Generic;
using System.Linq;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x02000010 RID: 16
	public class BoletoFacade : IBoletoFacade
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000478C File Offset: 0x0000298C
		public BoletoFacade(string aceite, string carteira, string variacao, string localPagamento, DateTime dataDocumento, string numeroDocumento, DateTime dataProcessamento, DateTime dataVencimento, string nossoNumero, string digitoNossoNumero, string identificacaoTituloEmpresa, int numeroParcela, decimal percentualJurosMora, decimal valorJurosMora, decimal percentualMulta, decimal valorMulta, decimal valorBoleto, decimal valorCobrado, IBancoFacade bancoFacade, ICedenteFacade cedenteFacade, ISacadoFacade sacadoFacade, IInstrucaoFacade primeiraInstrucaoFacade, IInstrucaoFacade segundaInstrucaoFacade, List<string> instrucoes, int diasIniciarCobrancaDeJuros, int diasIniciarCobrancaDaMulta, int diasAntesVencimentoParaAplicarDesconto, decimal percentualDesconto, bool mostrarComprovanteEntrega, bool formatoCarne, bool solicitacaoDeBaixa, TipoArquivo tipoArquivo, bool alteracaoVencimento, string codigoExclusivoBanese)
		{
			this.Boleto = new Boleto
			{
				Abatimento = 0m,
				Categoria = 0,
				DataDesconto = dataVencimento.AddDays((double)(-(double)diasAntesVencimentoParaAplicarDesconto)),
				ValorDesconto = valorBoleto * (percentualDesconto / 100m),
				IOF = 0m,
				Moeda = 9,
				Especie = "R$",
				QuantidadeMoeda = 0,
				Aceite = aceite,
				Carteira = carteira,
				DataDocumento = dataDocumento,
				DataProcessamento = dataProcessamento,
				DataVencimento = dataVencimento,
				DigitoNossoNumero = digitoNossoNumero,
				IdentificacaoTituloEmpresa = identificacaoTituloEmpresa,
				LocalPagamento = localPagamento,
				NossoNumero = nossoNumero,
				NossoNumeroSemFormatacao = nossoNumero,
				NumeroDocumento = numeroDocumento,
				NumeroParcela = numeroParcela,
				PercJurosMora = percentualJurosMora,
				JurosMora = valorJurosMora,
				PercMulta = percentualMulta,
				ValorMulta = valorBoleto * (percentualMulta / 100m),
				ValorBoleto = valorBoleto,
				ValorCobrado = valorCobrado,
				Variacao = variacao,
				DataJurosMora = dataVencimento.AddDays((double)diasIniciarCobrancaDeJuros),
				DataMulta = dataVencimento.AddDays((double)diasIniciarCobrancaDaMulta),
				Banco = bancoFacade.Obter<Banco>(),
				EspecieDocumento = bancoFacade.ObterEspecieDocumento<EspecieDocumento>(),
				FormaCadastramento = bancoFacade.ObterFormaCadastramento<FormaCadastramento>(),
				TipoCobranca = bancoFacade.ObterTipoCobranca<TipoCobranca>(),
				TipoDistribuicao = bancoFacade.ObterTipoDistribuicao<TipoDistribuicao>(),
				TipoEmissao = bancoFacade.ObterTipoEmissao<TipoEmissao>(),
				TipoTransmissao = bancoFacade.ObterTipoTransmissao<TipoTransmissao>(),
				Cedente = cedenteFacade.Obter<Cedente>(),
				Sacado = sacadoFacade.Obter<Sacado>(),
				SolicitacaoDeBaixa = solicitacaoDeBaixa,
				AlteracaoVencimento = alteracaoVencimento,
				CodigoExclusivoBanese = codigoExclusivoBanese
			};
			this.PreencherInstrucoes(this.Boleto, valorMulta, primeiraInstrucaoFacade, segundaInstrucaoFacade, instrucoes, tipoArquivo);
			this._boletoBancario = new BoletoBancario
			{
				CodigoBanco = (short)this.Boleto.Banco.Codigo,
				MostrarEnderecoCedente = true,
				OcultarInstrucoes = true,
				MostrarCodigoCarteira = true,
				Boleto = this.Boleto,
				MostrarComprovanteEntregaLivre = mostrarComprovanteEntrega,
				FormatoCarne = formatoCarne
			};
			this._boletoBancario.Boleto.Valida();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000049DB File Offset: 0x00002BDB
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000049E3 File Offset: 0x00002BE3
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto Boleto { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000049EC File Offset: 0x00002BEC
		public string NossoNumeroFormatado
		{
			get
			{
				this.Boleto.Banco.FormataNossoNumeroImpressao(this._boletoBancario.Boleto);
				BoletoBancario boletoBancario = this._boletoBancario;
				if (boletoBancario == null)
				{
					return null;
				}
				Boleto boleto = boletoBancario.Boleto;
				if (boleto == null)
				{
					return null;
				}
				return boleto.NossoNumeroImpressao;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A25 File Offset: 0x00002C25
		public string GerarHtml()
		{
			return this._boletoBancario.MontaHtml();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004A34 File Offset: 0x00002C34
		private void PreencherInstrucoes(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, decimal valorMulta, IInstrucaoFacade primeiraInstrucaoFacade, IInstrucaoFacade segundaInstrucaoFacade, IEnumerable<string> instrucoes, TipoArquivo tipoArquivo)
		{
			if (primeiraInstrucaoFacade.Codigo != -1)
			{
				Instrucao instrucao = new Instrucao(boleto, boleto.Banco.Codigo, primeiraInstrucaoFacade.Codigo, 0m, null, primeiraInstrucaoFacade.Dias, boleto.PercMulta, tipoArquivo);
				if (instrucao.IsInstrucaoProtesto)
				{
					if (!boleto.Sacado.NaoProtestar)
					{
						boleto.Instrucoes.Add(instrucao);
					}
				}
				else
				{
					boleto.Instrucoes.Add(instrucao);
				}
			}
			if (segundaInstrucaoFacade.Codigo != -1)
			{
				Instrucao instrucao2 = new Instrucao(boleto, boleto.Banco.Codigo, segundaInstrucaoFacade.Codigo, 0m, null, segundaInstrucaoFacade.Dias, boleto.PercMulta, tipoArquivo);
				if (instrucao2.IsInstrucaoProtesto)
				{
					if (!boleto.Sacado.NaoProtestar)
					{
						boleto.Instrucoes.Add(instrucao2);
					}
				}
				else
				{
					boleto.Instrucoes.Add(instrucao2);
				}
			}
			if (boleto.JurosMora > 0.01m)
			{
				boleto.Instrucoes.Add(new Instrucao(boleto, boleto.Banco.Codigo, 998, Convert.ToDecimal(boleto.JurosMora.ToString("0.00")), new DateTime?(boleto.DataJurosMora), 0, 0m, tipoArquivo));
			}
			if (boleto.ValorDesconto > 0.01m)
			{
				boleto.Instrucoes.Add(new Instrucao(boleto, boleto.Banco.Codigo, 999, Convert.ToDecimal(boleto.ValorDesconto.ToString("0.00")), new DateTime?(boleto.DataDesconto), 0, 0m, tipoArquivo));
			}
			if (valorMulta > 0.01m)
			{
				if (boleto.Banco.Codigo == 41)
				{
					if (primeiraInstrucaoFacade.Codigo != 18 && segundaInstrucaoFacade.Codigo != 18)
					{
						boleto.Instrucoes.Add(new Instrucao(boleto, boleto.Banco.Codigo, 997, Convert.ToDecimal(valorMulta.ToString("0.00")), new DateTime?(boleto.DataMulta), 0, 0m, tipoArquivo));
					}
				}
				else
				{
					boleto.Instrucoes.Add(new Instrucao(boleto, boleto.Banco.Codigo, 997, Convert.ToDecimal(valorMulta.ToString("0.00")), new DateTime?(boleto.DataMulta), 0, 0m, tipoArquivo));
				}
			}
			foreach (string text in instrucoes.Where<string>((string x) => !string.IsNullOrWhiteSpace(x)))
			{
				boleto.Instrucoes.Add(new Instrucao(boleto.Banco.Codigo)
				{
					Descricao = text
				});
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly BoletoBancario _boletoBancario;
	}
}

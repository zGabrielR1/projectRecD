using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoMovimento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000162 RID: 354
	internal sealed class BancoDaycoval : AbstractBanco, IBanco
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00042FF0 File Offset: 0x000411F0
		internal BancoDaycoval()
		{
			this.Codigo = 707;
			this.Digito = "2";
			this.Nome = "Daycoval";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00043024 File Offset: 0x00041224
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("DAY{0:ddMM}{1}.TXT", DateTime.Now.Date, this.SequenciaRemessaDiaria);
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00043058 File Offset: 0x00041258
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}-{1}/{2}-{3}", new object[]
			{
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4),
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 7),
				boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture)
			});
			if (boleto.Carteira != "19" && boleto.Carteira != "112")
			{
				throw new NotImplementedException("Carteira não implementada.");
			}
			if (boleto.NossoNumero.Length > 10)
			{
				throw new NotImplementedException("Quantidade de dígitos do nosso número ultrapassa 10 posições.");
			}
			if (boleto.NossoNumero.Length < 10)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 10);
			}
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Daycoval";
			}
			boleto.DigitoNossoNumero = this.CalcularDigitoNossoNumero(boleto);
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumeroImpressao(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000431A4 File Offset: 0x000413A4
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return AbstractBanco.Mod10(string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.PreencherComCaractere(Direcao.Esquerda, 4, '0')) + string.Format("{0}", boleto.Carteira.PreencherComCaractere(Direcao.Esquerda, 3, '0')) + string.Format("{0}", boleto.NossoNumero.PreencherComCaractere(Direcao.Esquerda, 10, '0'))).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00043220 File Offset: 0x00041420
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 10);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				this.Codigo,
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto),
				text,
				this.FormataCampoLivre(boleto)
			});
			this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00043314 File Offset: 0x00041514
		public string FormataCampoLivre(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.PreencherComCaractereEsquerda(4, '0')) + string.Format("{0}", boleto.Carteira.PreencherComCaractereEsquerda(3, '0')) + string.Format("{0}", boleto.Cedente.Convenio.CopiarTexto(7).PreencherComCaractereEsquerda(7, '0')) + string.Format("{0}{1}", boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0004339C File Offset: 0x0004159C
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.CodigoBarra.Codigo.Substring(0, 3);
			string text2 = boleto.CodigoBarra.Codigo.Substring(3, 1);
			string text3 = boleto.CodigoBarra.Codigo.Substring(19, 5);
			string text4 = AbstractBanco.Mod10(text + text2 + text3).ToString();
			string text5 = string.Format("{0}{1}{2}.{3}{4} ", new object[]
			{
				text,
				text2,
				text3.Substring(0, 1),
				text3.Substring(1, 4),
				text4
			});
			string text6 = boleto.CodigoBarra.Codigo.Substring(24, 10);
			string text7 = AbstractBanco.Mod10(text6).ToString();
			string text8 = string.Format("{0}.{1}{2} ", text6.Substring(0, 5), text6.Substring(5, 5), text7);
			string text9 = boleto.CodigoBarra.Codigo.Substring(34, 10);
			string text10 = AbstractBanco.Mod10(text9).ToString();
			string text11 = string.Format("{0}.{1}{2} ", text9.Substring(0, 5), text9.Substring(5, 5), text10);
			string text12 = this._dacBoleto.ToString();
			string text13 = string.Format("{0} ", text12);
			string text14 = AbstractBanco.FatorVencimento(boleto).ToString();
			string text15 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text15 = Utils.FormatCode(text15, 10);
			string text16 = string.Format("{0}{1}", text14, text15);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text5, text8, text11, text13, text16 });
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00043568 File Offset: 0x00041768
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.Carteira = string.Format("{0}", boleto.Carteira.PreencherComCaractere(Direcao.Esquerda, 3, '0'));
			string text = string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.PreencherComCaractere(Direcao.Esquerda, 4, '0'));
			boleto.NossoNumeroImpressao = string.Format("{0}/{1}/{2}-{3}", new object[] { text, boleto.Carteira, boleto.NossoNumero, boleto.DigitoNossoNumero });
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000435F0 File Offset: 0x000417F0
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text = " ";
			base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);
			switch (tipoArquivo)
			{
			case TipoArquivo.CNAB240:
				text = this.GerarHeaderRemessaCnab240();
				break;
			case TipoArquivo.CNAB400:
				text = this.GerarHeaderRemessaCnab400(cedente, numeroArquivoRemessa);
				break;
			case TipoArquivo.Outro:
				throw new Exception("Tipo de arquivo inexistente.");
			}
			return text;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarHeaderRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00043648 File Offset: 0x00041848
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 294);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente).PreencherComCaractereDireita(20, ' ') + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "707" + "BANCO DAYCOVAL " + DateTime.Now.ToString("ddMMyy") + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00043710 File Offset: 0x00041910
		public override string GerarDetalheRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarDetalheRemessaCnab240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarDetalheRemessaCnab400(boleto, numeroRegistro, tipoArquivo);
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00043788 File Offset: 0x00041988
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text5;
			try
			{
				string text = new string(' ', 10);
				string text2 = new string(' ', 13);
				string text3 = new string(' ', 24);
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text4 = "1";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text4 += "01";
				}
				else
				{
					text4 += "02";
				}
				text4 += Utils.FitStringLength(boleto.Cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text4 += string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente).PreencherComCaractereDireita(20, ' ');
				text4 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text4 += ((boleto.TipoDistribuicao.Codigo == 1) ? "00000000" : Utils.FitStringLength(boleto.NossoNumero.CopiarTextoDaDireitaParaEsquerda(8), 8, 8, ' ', 0, true, true, false));
				text4 += text2;
				text4 += text3;
				text4 += "6";
				text4 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text4 += Utils.Right(boleto.NumeroDocumento, 10, '0', true);
				text4 += boleto.DataVencimento.ToString("ddMMyy");
				text4 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += "707";
				text4 += "00000";
				text4 += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text4 += "N";
				text4 += boleto.DataProcessamento.ToString("ddMMyy");
				text4 += "0000";
				text4 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text4 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += "0000000000000";
				text4 += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text4 += "01";
				}
				else
				{
					text4 += "02";
				}
				text4 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 30, 30, ' ', 0, true, true, false).ToUpper();
				text4 += text;
				text4 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep.Replace("-", ""), 8, 8, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 30, 30, ' ', 0, true, true, false).ToUpper();
				text4 += text;
				IEnumerable<IInstrucao> enumerable = boleto.Instrucoes.Where<IInstrucao>((IInstrucao x) => x.IsInstrucaoProtesto);
				if (enumerable.Any<IInstrucao>())
				{
					text4 += Utils.FitStringLength(enumerable.FirstOrDefault<IInstrucao>().QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				}
				else
				{
					text4 += "00";
				}
				text4 += "0";
				text4 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text4 = Utils.SubstituiCaracteresEspeciais(text4);
				text5 = text4;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text5;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00043DA4 File Offset: 0x00041FA4
		public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
		{
			string text2;
			try
			{
				string text = " ";
				base.GerarTrailerRemessa(numeroRegistro, tipoArquivo, cedente, vltitulostotal);
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarTrailerRemessaCnab240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarTrailerRemessaCnab400(numeroRegistro);
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00043E1C File Offset: 0x0004201C
		public string GerarTrailerRemessaCnab400(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 393);
				text2 = Utils.SubstituiCaracteresEspeciais("9" + text + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00043E88 File Offset: 0x00042088
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(110, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(37, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					DescricaoOcorrencia = this.RetornarDescricaoDaOcorrencia(registro.Substring(108, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##")),
					Conta = Utils.ToInt32(registro.Substring(29, 7)),
					NossoNumero = Utils.ToString(registro.Substring(62, 10)),
					DACNossoNumero = Utils.ToInt32(registro.Substring(72, 1))
				};
				decimal num3 = Convert.ToUInt64(registro.Substring(279, 13));
				detalheRetornoCNAB.OutrosCreditos = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.DespesasDeCobranca = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(188, 13));
				detalheRetornoCNAB.OutrasDespesas = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(227, 13));
				detalheRetornoCNAB.ValorAbatimento = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(240, 13));
				detalheRetornoCNAB.Descontos = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPago = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num9 / 100m;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num2.ToString("##-##-##"));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				if (codigoOcorrencia <= 17)
				{
					switch (codigoOcorrencia)
					{
					case 2:
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
						goto IL_0324;
					case 3:
						goto IL_027D;
					case 4:
					case 5:
					case 7:
					case 8:
						goto IL_031D;
					case 6:
						break;
					case 9:
					case 10:
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoDaycoval.TipoDeRejeicao.Instrucoes);
						goto IL_0324;
					default:
						if (codigoOcorrencia != 15 && codigoOcorrencia != 17)
						{
							goto IL_031D;
						}
						break;
					}
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_0324;
				}
				if (codigoOcorrencia != 24)
				{
					if (codigoOcorrencia == 27)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoDaycoval.TipoDeRejeicao.Baixas);
						goto IL_0324;
					}
					if (codigoOcorrencia != 32)
					{
						goto IL_031D;
					}
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoDaycoval.TipoDeRejeicao.Instrucoes);
					goto IL_0324;
				}
				IL_027D:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
				detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoDaycoval.TipoDeRejeicao.Entradas);
				goto IL_0324;
				IL_031D:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_0324:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000441EC File Offset: 0x000423EC
		private string RetornarDescricaoDosMotivosDeRejeicoes(string codigosDeRejeicoes, BancoDaycoval.TipoDeRejeicao tipoDeRejeicao)
		{
			string[] array = Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace);
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.Length > 0)
				{
					switch (tipoDeRejeicao)
					{
					case BancoDaycoval.TipoDeRejeicao.Entradas:
						text = text + this.RetornarMotivoDaRejeicao(text2) + "\n";
						break;
					case BancoDaycoval.TipoDeRejeicao.Instrucoes:
						text = text + this.RetornarMotivoDaRejeicao(text2) + "\n";
						break;
					case BancoDaycoval.TipoDeRejeicao.Baixas:
						text = text + this.RetornarMotivoDaRejeicao(text2) + "\n";
						break;
					default:
						return string.Empty;
					}
				}
			}
			return text;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0004427E File Offset: 0x0004247E
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			return new CodigoRejeicaoDaycoval(codigo).Descricao;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0004428B File Offset: 0x0004248B
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			return new CodigoMovimentoDaycoval(codigo.ToInt()).Descricao;
		}

		// Token: 0x04000643 RID: 1603
		private int _dacBoleto;

		// Token: 0x02000198 RID: 408
		private enum TipoDeRejeicao
		{
			// Token: 0x04000743 RID: 1859
			Entradas,
			// Token: 0x04000744 RID: 1860
			Instrucoes,
			// Token: 0x04000745 RID: 1861
			Baixas
		}
	}
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000163 RID: 355
	internal sealed class BancoBradesco : AbstractBanco, IBanco
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x000442A0 File Offset: 0x000424A0
		internal BancoBradesco()
		{
			this.Codigo = 237;
			this.Digito = "2";
			this.Nome = "Bradesco";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000442F0 File Offset: 0x000424F0
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("CB{0:ddMM}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0004432D File Offset: 0x0004252D
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return BancoBradesco.Mod11Bradesco(boleto.Carteira + boleto.NossoNumero, 7);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00044348 File Offset: 0x00042548
		private static string Mod11Bradesco(string seq, int b)
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
			int num3 = num % 11;
			if (num3 == 0)
			{
				return "0";
			}
			if (num3 != 1)
			{
				return (11 - num3).ToString(CultureInfo.InvariantCulture);
			}
			return "P";
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000443B4 File Offset: 0x000425B4
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}-{1}/{2}-{3}", new object[]
			{
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4),
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 7),
				boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture)
			});
			if (boleto.Carteira != "2" && boleto.Carteira != "3" && boleto.Carteira != "6" && boleto.Carteira != "9")
			{
				throw new NotImplementedException("Carteira não implementada.");
			}
			if (boleto.Carteira == "3" && boleto.ValorBoleto == 0m)
			{
				throw new NotImplementedException("Para a carteira 03, o valor do boleto não pode ser igual a zero");
			}
			if (boleto.Carteira == "9" && boleto.ValorBoleto == 0m)
			{
				throw new NotImplementedException("Para a carteira 09, o valor do boleto não pode ser igual a zero");
			}
			if (boleto.NossoNumero.Length > 11)
			{
				throw new NotImplementedException("Quantidade de dígitos do nosso número ultrapassa 11 posições.");
			}
			if (boleto.NossoNumero.Length < 11)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
			}
			if (boleto.Cedente.ContaBancaria.Agencia.Length > 4)
			{
				throw new NotImplementedException("A quantidade de dígitos da Agência " + boleto.Cedente.ContaBancaria.Agencia + ", são de 4 números.");
			}
			if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
			{
				boleto.Cedente.ContaBancaria.Agencia = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length > 7)
			{
				throw new NotImplementedException("A quantidade de dígitos da Conta " + boleto.Cedente.ContaBancaria.Conta + ", são de 07 números.");
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length < 7)
			{
				boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);
			}
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Bradesco";
			}
			boleto.DigitoNossoNumero = this.CalcularDigitoNossoNumero(boleto);
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumeroImpressao(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00044678 File Offset: 0x00042878
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 10);
			string carteira = boleto.Carteira;
			if (!(carteira == "9") && !(carteira == "3") && !(carteira == "2"))
			{
				if (carteira == "6")
				{
					boleto.CodigoBarra.Codigo = ((boleto.ValorBoleto == 0m) ? string.Format("{0}{1}0000{2}{3}", new object[]
					{
						this.Codigo,
						boleto.Moeda,
						text,
						this.FormataCampoLivre(boleto)
					}) : string.Format("{0}{1}{2}{3}{4}", new object[]
					{
						this.Codigo,
						boleto.Moeda,
						AbstractBanco.FatorVencimento(boleto),
						text,
						this.FormataCampoLivre(boleto)
					}));
				}
			}
			else
			{
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[]
				{
					this.Codigo,
					boleto.Moeda,
					AbstractBanco.FatorVencimento(boleto),
					text,
					this.FormataCampoLivre(boleto)
				});
			}
			this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00044850 File Offset: 0x00042A50
		public string FormataCampoLivre(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Format("{0}0{1}{2}{3}0", new object[]
			{
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Carteira,
				boleto.NossoNumero,
				boleto.Cedente.ContaBancaria.Conta
			});
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000448A8 File Offset: 0x00042AA8
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

		// Token: 0x0600063F RID: 1599 RVA: 0x00044A74 File Offset: 0x00042C74
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.Carteira = string.Format("{0}", boleto.Carteira.PreencherComCaractere(Direcao.Esquerda, 2, '0'));
			boleto.NossoNumeroImpressao = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00044AC4 File Offset: 0x00042CC4
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

		// Token: 0x06000642 RID: 1602 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarHeaderRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00044B1C File Offset: 0x00042D1C
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 277);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + Utils.FitStringLength(cedente.Codigo + cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 20, 20, '0', 0, true, true, true) + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "237" + "BRADESCO       " + DateTime.Now.ToString("ddMMyy") + "        " + "MX" + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true) + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00044C24 File Offset: 0x00042E24
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

		// Token: 0x06000645 RID: 1605 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00044C9C File Offset: 0x00042E9C
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text5;
			try
			{
				string text = new string(' ', 10);
				string text2 = new string(' ', 12);
				string text3 = new string(' ', 60);
				string text4 = "1";
				text4 += "00000";
				text4 += " ";
				text4 += "00000";
				text4 += "0000000";
				text4 += " ";
				text4 += "0";
				text4 += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text4 += "000";
				if (boleto.PercMulta > 0m)
				{
					text4 += "2";
					text4 += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
				}
				else
				{
					text4 += "0";
					text4 += "0000";
				}
				text4 += Utils.FitStringLength(boleto.NossoNumero, 11, 11, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
				text4 += "0000000000";
				text4 += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text4 += "N";
				text4 += text;
				text4 += " ";
				text4 += "2";
				text4 += "  ";
				text4 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text4 += Utils.Right(boleto.NumeroDocumento, 10, '0', true);
				text4 += boleto.DataVencimento.ToString("ddMMyy");
				text4 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += "000";
				text4 += "00000";
				text4 += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text4 += "N";
				text4 += boleto.DataProcessamento.ToString("ddMMyy");
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text4 += "0000";
					}
					else
					{
						switch (boleto.Instrucoes[0].Codigo)
						{
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
						case 18:
							text4 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							if (boleto.Instrucoes[0].QuantidadeDias >= 5)
							{
								text4 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_0527;
							}
							text4 += "05";
							goto IL_0527;
						}
						switch (boleto.Instrucoes[1].Codigo)
						{
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
						case 18:
							text4 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							if (boleto.Instrucoes[0].QuantidadeDias >= 5)
							{
								text4 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_0527;
							}
							text4 += "05";
							goto IL_0527;
						}
						text4 += "0000";
					}
					IL_0527:;
				}
				catch (Exception)
				{
					text4 += "0000";
				}
				text4 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text4 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
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
				text4 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text4 += text2;
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep.Replace("-", ""), 8, 8, '0', 0, true, true, true);
				text4 += text3;
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

		// Token: 0x06000647 RID: 1607 RVA: 0x000454CC File Offset: 0x000436CC
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

		// Token: 0x06000648 RID: 1608 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00045544 File Offset: 0x00043744
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

		// Token: 0x0600064A RID: 1610 RVA: 0x000455B0 File Offset: 0x000437B0
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(295, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(37, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					DescricaoOcorrencia = this.RetornarDescricaoDaOcorrencia(registro.Substring(108, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##")),
					Conta = Utils.ToInt32(registro.Substring(29, 7))
				};
				detalheRetornoCNAB.NossoNumero = Utils.ToString(registro.Substring(70, 11));
				detalheRetornoCNAB.DACNossoNumero = Utils.ToInt32(registro.Substring(81, 1));
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
						goto IL_0327;
					case 3:
						goto IL_0280;
					case 4:
					case 5:
					case 7:
					case 8:
						goto IL_0320;
					case 6:
						break;
					case 9:
					case 10:
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoBradesco.TipoDeRejeicao.Instrucoes);
						goto IL_0327;
					default:
						if (codigoOcorrencia != 15 && codigoOcorrencia != 17)
						{
							goto IL_0320;
						}
						break;
					}
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_0327;
				}
				if (codigoOcorrencia != 24)
				{
					if (codigoOcorrencia == 27)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoBradesco.TipoDeRejeicao.Baixas);
						goto IL_0327;
					}
					if (codigoOcorrencia != 32)
					{
						goto IL_0320;
					}
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoBradesco.TipoDeRejeicao.Instrucoes);
					goto IL_0327;
				}
				IL_0280:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
				detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(318, 10).Trim(), BancoBradesco.TipoDeRejeicao.Entradas);
				goto IL_0327;
				IL_0320:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_0327:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00045918 File Offset: 0x00043B18
		private string RetornarDescricaoDosMotivosDeRejeicoes(string codigosDeRejeicoes, BancoBradesco.TipoDeRejeicao tipoDeRejeicao)
		{
			string[] array = Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace);
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.Length > 0)
				{
					switch (tipoDeRejeicao)
					{
					case BancoBradesco.TipoDeRejeicao.Entradas:
						text = text + this.RetornarMotivoDaRejeicaoEntradasRejeitadas(text2) + "\n";
						break;
					case BancoBradesco.TipoDeRejeicao.Instrucoes:
						text = text + this.RetornarMotivoDaRejeicaoInstrucoesRejeitadas(text2) + "\n";
						break;
					case BancoBradesco.TipoDeRejeicao.Baixas:
						text = text + this.RetornarMotivoDaRejeicaoBaixasRejeitadas(text2) + "\n";
						break;
					default:
						return string.Empty;
					}
				}
			}
			return text;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000459AC File Offset: 0x00043BAC
		public string RetornarMotivoDaRejeicaoEntradasRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2212577440U)
			{
				if (num <= 434988469U)
				{
					if (num <= 384361422U)
					{
						if (num <= 233362851U)
						{
							if (num != 216585232U)
							{
								if (num == 233362851U)
								{
									if (codigo == "68")
									{
										return "68-Débito não agendado - erro nos dados de remessa";
									}
								}
							}
							else if (codigo == "69")
							{
								return "69-Débito não agendado - Sacado não consta no cadastro de autorizante";
							}
						}
						else if (num != 334175660U)
						{
							if (num == 384361422U)
							{
								if (codigo == "63")
								{
									return "63-Entrada para Título já cadastrado";
								}
							}
						}
						else if (codigo == "18")
						{
							return "18-Vencimento fora do prazo de operação";
						}
					}
					else if (num <= 418210850U)
					{
						if (num != 417916660U)
						{
							if (num == 418210850U)
							{
								if (codigo == "09")
								{
									return "09-Nosso número duplicado";
								}
							}
						}
						else if (codigo == "65")
						{
							return "65-Limite excedido";
						}
					}
					else if (num != 434841374U)
					{
						if (num == 434988469U)
						{
							if (codigo == "08")
							{
								return "08-Nosso número inválido";
							}
						}
					}
					else if (codigo == "16")
					{
						return "16-Data de vencimento inválida";
					}
				}
				else if (num <= 485321326U)
				{
					if (num <= 468249517U)
					{
						if (num != 451766088U)
						{
							if (num == 468249517U)
							{
								if (codigo == "66")
								{
									return "66-Número autorização inexistente";
								}
							}
						}
						else if (codigo == "07")
						{
							return "07-Agência/conta/Digito - Inválido";
						}
					}
					else if (num != 468396612U)
					{
						if (num == 485321326U)
						{
							if (codigo == "05")
							{
								return "05-Código de ocorrência não numérico";
							}
						}
					}
					else if (codigo == "10")
					{
						return "10-Carteira inválida";
					}
				}
				else if (num <= 518729469U)
				{
					if (num != 502098945U)
					{
						if (num == 518729469U)
						{
							if (codigo == "13")
							{
								return "13-Identificação da emissão do bloqueto inválida";
							}
						}
					}
					else if (codigo == "04")
					{
						return "04-Código de ocorrência não permitida para a carteira";
					}
				}
				else if (num != 518876564U)
				{
					if (num != 535654183U)
					{
						if (num == 2212577440U)
						{
							if (codigo == "50")
							{
								return "50-CEP irregular - Banco Correspondente";
							}
						}
					}
					else if (codigo == "02")
					{
						return "02-Código do registro detalhe inválido";
					}
				}
				else if (codigo == "03")
				{
					return "03-Código da ocorrência inválida";
				}
			}
			else if (num <= 2380500725U)
			{
				if (num <= 2347092582U)
				{
					if (num <= 2330167868U)
					{
						if (num != 2314375987U)
						{
							if (num == 2330167868U)
							{
								if (codigo == "47")
								{
									return "47-Endereço do sacado não informado";
								}
							}
						}
						else if (codigo == "24")
						{
							return "24-Data de emissão inválida";
						}
					}
					else if (num != 2346945487U)
					{
						if (num == 2347092582U)
						{
							if (codigo == "74")
							{
								return "74-Débito não agendado - Conforme seu pedido, Título não registrado";
							}
						}
					}
					else if (codigo == "46")
					{
						return "46-Tipo/número de inscrição do sacado inválidos";
					}
				}
				else if (num <= 2363870201U)
				{
					if (num != 2363723106U)
					{
						if (num == 2363870201U)
						{
							if (codigo == "75")
							{
								return "75-Débito não agendado - Tipo de número de inscrição do debitado inválido";
							}
						}
					}
					else if (codigo == "45")
					{
						return "45-Nome do sacado não informado";
					}
				}
				else if (num != 2364708844U)
				{
					if (num == 2380500725U)
					{
						if (codigo == "44")
						{
							return "44-Agência Cedente não prevista";
						}
					}
				}
				else if (codigo == "21")
				{
					return "21-Espécie do Título inválida";
				}
			}
			else if (num <= 2414203058U)
			{
				if (num <= 2381486463U)
				{
					if (num != 2380647820U)
					{
						if (num == 2381486463U)
						{
							if (codigo == "20")
							{
								return "20-Valor do Título inválido";
							}
						}
					}
					else if (codigo == "72")
					{
						return "72-Débito não agendado - Código de moeda diferente de R$";
					}
				}
				else if (num != 2397425439U)
				{
					if (num == 2414203058U)
					{
						if (codigo == "70")
						{
							return "70-Débito não agendado - Cedente não autorizado pelo Sacado";
						}
					}
				}
				else if (codigo == "73")
				{
					return "73-Débito não agendado - Data de vencimento inválida";
				}
			}
			else if (num <= 2415041701U)
			{
				if (num != 2414894606U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return "22-Espécie não permitida para a carteira";
						}
					}
				}
				else if (codigo == "38")
				{
					return "38-Prazo para protesto inválido";
				}
			}
			else if (num != 2430980677U)
			{
				if (num != 2447611201U)
				{
					if (num == 2515707415U)
					{
						if (codigo == "28")
						{
							return "28-Código do desconto inválido";
						}
					}
				}
				else if (codigo == "48")
				{
					return "48-CEP Inválido";
				}
			}
			else if (codigo == "71")
			{
				return "71-Débito não agendado - Cedente não participa da modalidade de débito automático";
			}
			return "";
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00045FA4 File Offset: 0x000441A4
		public string RetornarMotivoDaRejeicaoInstrucoesRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2280673654U)
			{
				if (num <= 468396612U)
				{
					if (num <= 418063755U)
					{
						if (num <= 334175660U)
						{
							if (num != 134124160U)
							{
								if (num == 334175660U)
								{
									if (codigo == "18")
									{
										return "18-Vencimento fora do prazo de operação";
									}
								}
							}
							else if (codigo == "94")
							{
								return "94-Título Penhorado – Instrução Não Liberada pela Agência";
							}
						}
						else if (num != 367583803U)
						{
							if (num == 418063755U)
							{
								if (codigo == "15")
								{
									return "15-Características da cobrança incompatíveis";
								}
							}
						}
						else if (codigo == "60")
						{
							return "60-Movimento para Título não cadastrado";
						}
					}
					else if (num <= 434988469U)
					{
						if (num != 434841374U)
						{
							if (num == 434988469U)
							{
								if (codigo == "08")
								{
									return "08-Nosso número inválido";
								}
							}
						}
						else if (codigo == "16")
						{
							return "16-Data de vencimento inválida";
						}
					}
					else if (num != 451618993U)
					{
						if (num != 451766088U)
						{
							if (num == 468396612U)
							{
								if (codigo == "10")
								{
									return "10-Carteira inválida";
								}
							}
						}
						else if (codigo == "07")
						{
							return "07-Agência/Conta/dígito inválidos";
						}
					}
					else if (codigo == "17")
					{
						return "17-Data de vencimento anterior a data de emissão";
					}
				}
				else if (num <= 552431802U)
				{
					if (num <= 502098945U)
					{
						if (num != 485321326U)
						{
							if (num == 502098945U)
							{
								if (codigo == "04")
								{
									return "04-Código de ocorrência não permitido para a carteira";
								}
							}
						}
						else if (codigo == "05")
						{
							return "05-Código de ocorrência não numérico";
						}
					}
					else if (num != 535654183U)
					{
						if (num == 552431802U)
						{
							if (codigo == "01")
							{
								return "01-Código do Banco inválido";
							}
						}
					}
					else if (codigo == "02")
					{
						return "02-Código do registro detalhe inválido";
					}
				}
				else if (num <= 2262910297U)
				{
					if (num != 2212577440U)
					{
						if (num == 2262910297U)
						{
							if (codigo == "53")
							{
								return "53-Tipo de inscrição do sacador avalista inválidos";
							}
						}
					}
					else if (codigo == "50")
					{
						return "50-CEP referente a um Banco correspondente";
					}
				}
				else if (num != 2263896035U)
				{
					if (num != 2279835011U)
					{
						if (num == 2280673654U)
						{
							if (codigo == "30")
							{
								return "30-Desconto a conceder não confere";
							}
						}
					}
					else if (codigo == "42")
					{
						return "42-Código para baixa/devolução inválido";
					}
				}
				else if (codigo == "33")
				{
					return "33-Valor do abatimento inválido";
				}
			}
			else if (num <= 2348916963U)
			{
				if (num <= 2314228892U)
				{
					if (num <= 2297451273U)
					{
						if (num != 2296612630U)
						{
							if (num == 2297451273U)
							{
								if (codigo == "31")
								{
									return "31-Concessão de desconto - Já existe desconto anterior";
								}
							}
						}
						else if (codigo == "41")
						{
							return "41-Pedido cancelamento/sustação para Título sem instrução de protesto";
						}
					}
					else if (num != 2313390249U)
					{
						if (num == 2314228892U)
						{
							if (codigo == "36")
							{
								return "36-Concessão abatimento - Já existe abatimento anterior";
							}
						}
					}
					else if (codigo == "40")
					{
						return "40-Título com ordem de protesto emitido";
					}
				}
				else if (num <= 2330167868U)
				{
					if (num != 2314375987U)
					{
						if (num == 2330167868U)
						{
							if (codigo == "47")
							{
								return "47-Endereço do Sacado não informado";
							}
						}
					}
					else if (codigo == "24")
					{
						return "24-Data de emissão inválida";
					}
				}
				else if (num != 2346945487U)
				{
					if (num != 2347784130U)
					{
						if (num == 2348916963U)
						{
							if (codigo == "86")
							{
								return "86-Seu número inválido";
							}
						}
					}
					else if (codigo == "34")
					{
						return "34-Valor do abatimento maior/igual ao valor do Título";
					}
				}
				else if (codigo == "46")
				{
					return "46-Tipo/número de inscrição do Sacado inválidos";
				}
			}
			else if (num <= 2414894606U)
			{
				if (num <= 2364708844U)
				{
					if (num != 2363723106U)
					{
						if (num == 2364708844U)
						{
							if (codigo == "21")
							{
								return "21-Espécie do Título inválida";
							}
						}
					}
					else if (codigo == "45")
					{
						return "45-Nome do Sacado não informado";
					}
				}
				else if (num != 2365694582U)
				{
					if (num != 2381486463U)
					{
						if (num == 2414894606U)
						{
							if (codigo == "38")
							{
								return "38-Prazo para protesto inválido";
							}
						}
					}
					else if (codigo == "20")
					{
						return "20-Valor do título inválido";
					}
				}
				else if (codigo == "85")
				{
					return "85-Título com pagamento vinculado";
				}
			}
			else if (num <= 2431672225U)
			{
				if (num != 2415041701U)
				{
					if (num == 2431672225U)
					{
						if (codigo == "39")
						{
							return "39-Pedido de protesto não permitido para o Título";
						}
					}
				}
				else if (codigo == "22")
				{
					return "22-Espécie não permitida para a carteira";
				}
			}
			else if (num != 2447611201U)
			{
				if (num != 2498929796U)
				{
					if (num == 2515707415U)
					{
						if (codigo == "28")
						{
							return "28-Código de desconto via Telebradesco inválido";
						}
					}
				}
				else if (codigo == "29")
				{
					return "29-Valor do desconto maior/igual ao valor do Título";
				}
			}
			else if (codigo == "48")
			{
				return "48-CEP Inválido";
			}
			return "";
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0004660C File Offset: 0x0004480C
		public string RetornarMotivoDaRejeicaoBaixasRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 468396612U)
			{
				if (num <= 418063755U)
				{
					if (num != 367583803U)
					{
						if (num == 418063755U)
						{
							if (codigo == "15")
							{
								return "15-Carteira/Agência/Conta/nosso número inválidos";
							}
						}
					}
					else if (codigo == "60")
					{
						return "60-Movimento para Título não cadastrado";
					}
				}
				else if (num != 434988469U)
				{
					if (num != 451766088U)
					{
						if (num == 468396612U)
						{
							if (codigo == "10")
							{
								return "10-Carteira inválida";
							}
						}
					}
					else if (codigo == "07")
					{
						return "07-Agência/Conta/dígito inválidos";
					}
				}
				else if (codigo == "08")
				{
					return "08-Nosso número inválido";
				}
			}
			else if (num <= 2279835011U)
			{
				if (num != 502098945U)
				{
					if (num == 2279835011U)
					{
						if (codigo == "42")
						{
							return "42-Código para baixa/devolução via Telebradesco inválido";
						}
					}
				}
				else if (codigo == "04")
				{
					return "04-Código de ocorrência não permitido para a carteira";
				}
			}
			else if (num != 2313390249U)
			{
				if (num != 2330314963U)
				{
					if (num == 2365694582U)
					{
						if (codigo == "85")
						{
							return "85-Título com pagamento vinculado";
						}
					}
				}
				else if (codigo == "77")
				{
					return "77-Transferência para desconto não permitido para a carteira";
				}
			}
			else if (codigo == "40")
			{
				return "40-Título com ordem de protesto emitido";
			}
			return "";
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000467A0 File Offset: 0x000449A0
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 535654183U)
			{
				if (num <= 434841374U)
				{
					if (num <= 350953279U)
					{
						if (num <= 233362851U)
						{
							if (num != 216585232U)
							{
								if (num == 233362851U)
								{
									if (codigo == "68")
									{
										return "68-Acerto dos dados do rateio de Crédito";
									}
								}
							}
							else if (codigo == "69")
							{
								return "69-Cancelamento dos dados do rateio";
							}
						}
						else if (num != 334175660U)
						{
							if (num == 350953279U)
							{
								if (codigo == "19")
								{
									return "19-Confirmação Recebimento Instrução de Protesto";
								}
							}
						}
						else if (codigo == "18")
						{
							return "18-Acerto de Depositária";
						}
					}
					else if (num <= 418063755U)
					{
						if (num != 401286136U)
						{
							if (num == 418063755U)
							{
								if (codigo == "15")
								{
									return "15-Liquidação em Cartório";
								}
							}
						}
						else if (codigo == "14")
						{
							return "14-Vencimento Alterado";
						}
					}
					else if (num != 418210850U)
					{
						if (num == 434841374U)
						{
							if (codigo == "16")
							{
								return "16-Título Pago em Cheque – Vinculado";
							}
						}
					}
					else if (codigo == "09")
					{
						return "09-Baixado Automática via Arquivo";
					}
				}
				else if (num <= 485174231U)
				{
					if (num <= 468396612U)
					{
						if (num != 451618993U)
						{
							if (num == 468396612U)
							{
								if (codigo == "10")
								{
									return "10-Baixado conforme instruções da Agência";
								}
							}
						}
						else if (codigo == "17")
						{
							return "17-Liquidação após baixa ou Título não registrado";
						}
					}
					else if (num != 468543707U)
					{
						if (num == 485174231U)
						{
							if (codigo == "11")
							{
								return "11-Em Ser - Arquivo de Títulos pendentes";
							}
						}
					}
					else if (codigo == "06")
					{
						return "06-Liquidação normal";
					}
				}
				else if (num <= 518729469U)
				{
					if (num != 501951850U)
					{
						if (num == 518729469U)
						{
							if (codigo == "13")
							{
								return "13-Abatimento Cancelado";
							}
						}
					}
					else if (codigo == "12")
					{
						return "12-Abatimento Concedido";
					}
				}
				else if (num != 518876564U)
				{
					if (num == 535654183U)
					{
						if (codigo == "02")
						{
							return "02-Entrada Confirmada";
						}
					}
				}
				else if (codigo == "03")
				{
					return "03-Entrada Rejeitada";
				}
			}
			else if (num <= 2331153606U)
			{
				if (num <= 2296465535U)
				{
					if (num <= 2263896035U)
					{
						if (num != 2247118416U)
						{
							if (num == 2263896035U)
							{
								if (codigo == "33")
								{
									return "33-Confirmação Pedido Alteração Outros Dados";
								}
							}
						}
						else if (codigo == "32")
						{
							return "32-Instrução Rejeitada";
						}
					}
					else if (num != 2280673654U)
					{
						if (num == 2296465535U)
						{
							if (codigo == "55")
							{
								return "55-Sustado judicial";
							}
						}
					}
					else if (codigo == "30")
					{
						return "30-Alteração de Outros Dados Rejeitados";
					}
				}
				else if (num <= 2313390249U)
				{
					if (num != 2297598368U)
					{
						if (num == 2313390249U)
						{
							if (codigo == "40")
							{
								return "40-Estorno de pagamento";
							}
						}
					}
					else if (codigo == "25")
					{
						return "Confirmação Recebimento Instrução de Protesto Falimentar";
					}
				}
				else if (num != 2314375987U)
				{
					if (num == 2331153606U)
					{
						if (codigo == "27")
						{
							return "27-Baixa Rejeitada";
						}
					}
				}
				else if (codigo == "24")
				{
					return "24-Entrada rejeitada por CEP Irregular";
				}
			}
			else if (num <= 2381486463U)
			{
				if (num <= 2364561749U)
				{
					if (num != 2347784130U)
					{
						if (num == 2364561749U)
						{
							if (codigo == "35")
							{
								return "35-Desagendamento do débito automático";
							}
						}
					}
					else if (codigo == "34")
					{
						return "34-Retirado de Cartório e Manutenção Carteira";
					}
				}
				else if (num != 2364708844U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return "20-Confirmação Recebimento Instrução Sustação de Protesto";
						}
					}
				}
				else if (codigo == "21")
				{
					return "21-Acerto do Controle do Participante";
				}
			}
			else if (num <= 2415041701U)
			{
				if (num != 2398264082U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return "22-Título Com Pagamento Cancelado";
						}
					}
				}
				else if (codigo == "23")
				{
					return "23-Entrada do Título em Cartório";
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return "28-Débito de tarifas/custas";
					}
				}
			}
			else if (codigo == "29")
			{
				return "Ocorrências do Sacado";
			}
			return "";
		}

		// Token: 0x04000644 RID: 1604
		private int _dacBoleto;

		// Token: 0x04000645 RID: 1605
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

		// Token: 0x0200019A RID: 410
		private enum TipoDeRejeicao
		{
			// Token: 0x04000749 RID: 1865
			Entradas,
			// Token: 0x0400074A RID: 1866
			Instrucoes,
			// Token: 0x0400074B RID: 1867
			Baixas
		}
	}
}

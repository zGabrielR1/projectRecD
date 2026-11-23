using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200016E RID: 366
	internal sealed class BancoHsbc : AbstractBanco, IBanco
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x0005E308 File Offset: 0x0005C508
		internal BancoHsbc()
		{
			this.Codigo = 399;
			this.Digito = "9";
			this.Nome = "HSBC";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0005E364 File Offset: 0x0005C564
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyy}{1}.TXT", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0005E3A4 File Offset: 0x0005C5A4
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}{1} {2}{3}", new object[]
			{
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 10),
				boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture)
			});
			if (string.IsNullOrEmpty(boleto.Carteira))
			{
				throw new NotImplementedException("Carteira não informada. Utilize a carteira 'CSB'");
			}
			if (!boleto.Carteira.Equals("1"))
			{
				throw new NotImplementedException("Carteira não implementada. Utilize a carteira 'CSB'.");
			}
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (Utils.ToInt64(boleto.NossoNumero) == 0L)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 10)
			{
				throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 10 números.");
			}
			if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 10)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture), 5) + Utils.FormatCode(boleto.NossoNumero, 5);
			}
			this._dacNossoNumero = AbstractBanco.Mod11(boleto.NossoNumero, 7).ToString(CultureInfo.InvariantCulture);
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no HSBC";
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumero(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0005E590 File Offset: 0x0005C790
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}001", new object[]
				{
					this.Codigo,
					boleto.Moeda,
					AbstractBanco.FatorVencimento(boleto),
					text,
					boleto.NossoNumero + this._dacNossoNumero,
					Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4),
					Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7)
				});
				this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9, 0);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
				string text3 = boleto.NossoNumero.Substring(0, 5);
				string text4 = AbstractBanco.Mod10(text + text2 + text3).ToString(CultureInfo.InvariantCulture);
				string text5 = string.Format("{0}{1}{2}.", text, text2, text3.Substring(0, 1));
				text5 += string.Format("{0}{1} ", text3.Substring(1, 4), text4);
				string text6 = boleto.NossoNumero.Substring(5, 5) + this._dacNossoNumero;
				string text7 = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);
				string text8 = AbstractBanco.Mod10(text6 + text7).ToString(CultureInfo.InvariantCulture);
				string text9 = string.Format("{0}.", text6.Substring(0, 5));
				text9 += string.Format("{0}{1}{2} ", text6.Substring(5, 1), text7, text8);
				string text10 = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);
				string text11 = AbstractBanco.Mod10(text10 + "001").ToString(CultureInfo.InvariantCulture);
				string text12 = string.Format("{0}.", text10.Substring(0, 5));
				text12 += string.Format("{0}001{1}", text10.Substring(5, 2), text11);
				string text13 = string.Format(" {0} ", this._dacBoleto);
				string text14 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
				string text15 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text15 = Utils.FormatCode(text15, 10);
				string text16 = text14 + text15;
				boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text5, text9, text12, text13, text16 });
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar linha digitável.", ex);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0005E944 File Offset: 0x0005CB44
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NossoNumero = string.Format("{0}{1}", boleto.NossoNumero, this._dacNossoNumero);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0005E98C File Offset: 0x0005CB8C
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

		// Token: 0x06000766 RID: 1894 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarHeaderRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0005E9E4 File Offset: 0x0005CBE4
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 277);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + "0" + Utils.FitStringLength(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true) + "55" + Utils.FitStringLength(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta + cedente.ContaBancaria.DigitoConta, 7, 7, '0', 0, true, true, true) + "  " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "399" + Utils.FitStringLength("HSBC", 15, 15, ' ', 0, true, true, false).ToUpper() + DateTime.Now.ToString("ddMMyy") + "01600" + "BPI" + "  " + "LANCV08" + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0005EB70 File Offset: 0x0005CD70
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

		// Token: 0x06000769 RID: 1897 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0005EBE8 File Offset: 0x0005CDE8
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = "1";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text += "01";
				}
				else
				{
					text += "02";
				}
				text += Utils.FitStringLength(boleto.Cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text += "0";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true);
				text += "55";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta, 7, 7, '0', 0, true, true, true);
				text += "  ";
				text += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.NossoNumero, 10, 10, '0', 0, true, true, true);
				text += this._dacNossoNumero;
				text += "000000";
				text += Utils.FitStringLength("0", 11, 11, '0', 0, true, true, true);
				text += "000000";
				text += Utils.FitStringLength("0", 11, 11, '0', 0, true, true, true);
				text += "1";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text += boleto.DataVencimento.ToString("ddMMyy");
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += "399";
				text += "00000";
				text += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento), 2, 2, '0', 0, true, true, true);
				text += boleto.Aceite;
				text += boleto.DataDocumento.ToString("ddMMyy");
				int num = boleto.Instrucoes.Count;
				if (num != 0)
				{
					if (num != 1)
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
					else
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text += "  ";
					}
				}
				else
				{
					text += "    ";
				}
				text += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num <= 22)
					{
						if (num == 15)
						{
							text += boleto.DataJurosMora.ToString("ddMMyy");
							text += Utils.FitStringLength(boleto.PercJurosMora.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
							text += "   ";
							goto IL_08D9;
						}
						if (num == 19 || num == 22)
						{
							text += Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 10, 10, '0', 0, true, true, true);
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true).ToUpper();
							goto IL_08D9;
						}
					}
					else if (num <= 29)
					{
						if (num == 24)
						{
							text += Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 10, 10, '0', 0, true, true, true);
							text += "000";
							goto IL_08D9;
						}
						if (num == 29)
						{
							text += boleto.DataJurosMora.ToString("ddMMyy");
							goto IL_08D9;
						}
					}
					else if (num == 73 || num == 74)
					{
						text += "      ";
						text += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
						text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true).ToUpper();
						goto IL_08D9;
					}
					num = boleto.Instrucoes[1].Codigo;
					if (num <= 22)
					{
						if (num == 15)
						{
							text += boleto.DataJurosMora.ToString("ddMMyy");
							text += Utils.FitStringLength(boleto.PercJurosMora.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
							text += "  ";
							goto IL_08D9;
						}
						if (num == 19 || num == 22)
						{
							text += Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 10, 10, '0', 0, true, true, true);
							text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true).ToUpper();
							goto IL_08D9;
						}
					}
					else if (num <= 29)
					{
						if (num == 24)
						{
							text += Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 10, 10, '0', 0, true, true, true);
							text += "000";
							goto IL_08D9;
						}
						if (num == 29)
						{
							text += boleto.DataJurosMora.ToString("ddMMyy");
							goto IL_08D9;
						}
					}
					else if (num == 73 || num == 74)
					{
						text += "      ";
						text += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
						text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true).ToUpper();
						goto IL_08D9;
					}
					text += Utils.FitStringLength("0", 13, 13, '0', 0, true, true, true);
					IL_08D9:;
				}
				catch (Exception)
				{
					text += Utils.FitStringLength("0", 13, 13, '0', 0, true, true, true);
				}
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text += "01";
				}
				else
				{
					text += "02";
				}
				text += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false);
				text += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 38, 38, ' ', 0, true, true, false).ToUpper();
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num == 71 || num == 72)
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					}
					else
					{
						num = boleto.Instrucoes[1].Codigo;
						if (num == 71 || num == 72)
						{
							text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
						}
						else
						{
							text += "  ";
						}
					}
				}
				catch (Exception)
				{
					text += "  ";
				}
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Nome, 39, 39, ' ', 0, true, true, false).ToUpper();
				text += " ";
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num != 75)
					{
						if (num != 77)
						{
							num = boleto.Instrucoes[1].Codigo;
							if (num != 75)
							{
								if (num != 77)
								{
									text += "  ";
								}
								else if (boleto.Instrucoes[1].QuantidadeDias >= 2 && boleto.Instrucoes[1].QuantidadeDias <= 35)
								{
									text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
								}
								else
								{
									text += "02";
								}
							}
							else if (boleto.Instrucoes[1].QuantidadeDias >= 2 && boleto.Instrucoes[1].QuantidadeDias <= 45)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
							}
							else
							{
								text += "02";
							}
						}
						else if (boleto.Instrucoes[0].QuantidadeDias >= 2 && boleto.Instrucoes[0].QuantidadeDias <= 35)
						{
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
						}
						else
						{
							text += "02";
						}
					}
					else if (boleto.Instrucoes[0].QuantidadeDias >= 2 && boleto.Instrucoes[0].QuantidadeDias <= 45)
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					}
					else
					{
						text += "02";
					}
				}
				catch (Exception)
				{
					text += "  ";
				}
				text += "9";
				text += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0005FA78 File Offset: 0x0005DC78
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

		// Token: 0x0600076C RID: 1900 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0005FAF0 File Offset: 0x0005DCF0
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

		// Token: 0x0600076E RID: 1902 RVA: 0x0005FB5C File Offset: 0x0005DD5C
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(37, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					DescricaoOcorrencia = this.RetornarDescricaoDaOcorrencia(registro.Substring(108, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"))
				};
				decimal num2 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.TarifaCobranca = num2 / 100m;
				decimal num3 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num4 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num.ToString("##-##-##"));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				switch (codigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_0229;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarMotivoDaRejeicao(registro.Substring(301, 2)).Replace("DDMMAA", num.ToString("##-##-##"));
					goto IL_0229;
				case 4:
				case 5:
				case 8:
				case 11:
				case 12:
				case 13:
				case 14:
					goto IL_0222;
				case 6:
				case 7:
				case 15:
				case 16:
					break;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarMotivoDaRejeicao(registro.Substring(301, 2)).Replace("DDMMAA", num.ToString("##-##-##"));
					goto IL_0229;
				default:
					switch (codigoOcorrencia)
					{
					case 31:
					case 32:
					case 33:
					case 36:
					case 38:
					case 39:
						break;
					case 34:
					case 35:
					case 37:
						goto IL_0222;
					default:
						switch (codigoOcorrencia)
						{
						case 92:
						case 93:
						case 94:
							break;
						default:
							goto IL_0222;
						}
						break;
					}
					break;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
				goto IL_0229;
				IL_0222:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_0229:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0005FDC4 File Offset: 0x0005DFC4
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2246132678U)
			{
				if (num <= 451766088U)
				{
					if (num <= 418063755U)
					{
						if (num <= 334175660U)
						{
							if (num != 216585232U)
							{
								if (num == 334175660U)
								{
									if (codigo == "18")
									{
										return "18-Instrução automática de protesto";
									}
								}
							}
							else if (codigo == "69")
							{
								return "69-Despesas/custas de cartório";
							}
						}
						else if (num != 401286136U)
						{
							if (num == 418063755U)
							{
								if (codigo == "15")
								{
									return "15-Liquidação em cartório em dinheiro";
								}
							}
						}
						else if (codigo == "14")
						{
							return "14-Vencimento prorrogado";
						}
					}
					else if (num <= 434841374U)
					{
						if (num != 418210850U)
						{
							if (num == 434841374U)
							{
								if (codigo == "16")
								{
									return "16-Liquidação - baixado/devolvido em data anterior dinheiro";
								}
							}
						}
						else if (codigo == "09")
						{
							return "09-Baixa automática";
						}
					}
					else if (num != 451618993U)
					{
						if (num == 451766088U)
						{
							if (codigo == "07")
							{
								return "07-Liquidação por conta em dinheiro";
							}
						}
					}
					else if (codigo == "17")
					{
						return "17-Entregue em cartório em . protocolo .";
					}
				}
				else if (num <= 501951850U)
				{
					if (num <= 468543707U)
					{
						if (num != 468396612U)
						{
							if (num == 468543707U)
							{
								if (codigo == "06")
								{
									return "06-Liquidação normal em dinheiro";
								}
							}
						}
						else if (codigo == "10")
						{
							return "10-Baixado conforme instruções";
						}
					}
					else if (num != 485174231U)
					{
						if (num == 501951850U)
						{
							if (codigo == "12")
							{
								return "12-Abatimento concedido";
							}
						}
					}
					else if (codigo == "11")
					{
						return "11-Títulos em ser (Conciliação Mensal)";
					}
				}
				else if (num <= 518876564U)
				{
					if (num != 518729469U)
					{
						if (num == 518876564U)
						{
							if (codigo == "03")
							{
								return "03-Entrada rejeitada ou Instrução rejeitada";
							}
						}
					}
					else if (codigo == "13")
					{
						return "13-Abatimento cancelado";
					}
				}
				else if (num != 535654183U)
				{
					if (num != 2229355059U)
					{
						if (num == 2246132678U)
						{
							if (codigo == "52")
							{
								return "52-Título DDA não reconhecido pelo sacado";
							}
						}
					}
					else if (codigo == "51")
					{
						return "51-Título DDA aceito pelo sacado";
					}
				}
				else if (codigo == "02")
				{
					return "02-Entrada Confirmada";
				}
			}
			else if (num <= 2364708844U)
			{
				if (num <= 2314228892U)
				{
					if (num <= 2263896035U)
					{
						if (num != 2247118416U)
						{
							if (num == 2263896035U)
							{
								if (codigo == "33")
								{
									return "33-Liquidação por conta em cheque";
								}
							}
						}
						else if (codigo == "32")
						{
							return "32-Liquidação em cartório em cheque";
						}
					}
					else if (num != 2297451273U)
					{
						if (num == 2314228892U)
						{
							if (codigo == "36")
							{
								return "36-Liquidação - baixado/devolvido emdata anterior em cheque";
							}
						}
					}
					else if (codigo == "31")
					{
						return "31-Liquidação normal em cheque/compensação/banco correspondente";
					}
				}
				else if (num <= 2331153606U)
				{
					if (num != 2331006511U)
					{
						if (num == 2331153606U)
						{
							if (codigo == "27")
							{
								return "27-Número do cedente ou controle do participante alterado";
							}
						}
					}
					else if (codigo == "37")
					{
						return "37-Baixa de título protestado";
					}
				}
				else if (num != 2347092582U)
				{
					if (num == 2364708844U)
					{
						if (codigo == "21")
						{
							return "21-Instrução de alteração de mora";
						}
					}
				}
				else if (codigo == "74")
				{
					return "74-Cancelamento de Desconto Diário Aceito";
				}
			}
			else if (num <= 2414203058U)
			{
				if (num <= 2397425439U)
				{
					if (num != 2380647820U)
					{
						if (num == 2397425439U)
						{
							if (codigo == "73")
							{
								return "73-Cancelamento Condição de Desconto Fixo Aceito";
							}
						}
					}
					else if (codigo == "72")
					{
						return "72-Concessão de Desconto Aceito";
					}
				}
				else if (num != 2398264082U)
				{
					if (num == 2414203058U)
					{
						if (codigo == "70")
						{
							return "70-Ressarcimento sobre títulos";
						}
					}
				}
				else if (codigo == "23")
				{
					return "23-Cancelamento de protesto processado";
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
							return "22-Instrução de protesto processada/re-emitida";
						}
					}
				}
				else if (codigo == "38")
				{
					return "38-Liquidação de título não registrado - em dinheiro (Cobrança Expressa ou Cobrança Diretiva)";
				}
			}
			else if (num != 2430833582U)
			{
				if (num != 2430980677U)
				{
					if (num == 2431672225U)
					{
						if (codigo == "39")
						{
							return "39-Liquidação de título não registrado - em cheque (Cobrança Expressa ou Cobrança Diretiva)";
						}
					}
				}
				else if (codigo == "71")
				{
					return "71-Ocorrência/Instrução não permitida para título em garantia de operação";
				}
			}
			else if (codigo == "49")
			{
				return "49-Vencimento alterado para .";
			}
			return "";
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000603BC File Offset: 0x0005E5BC
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2263896035U)
			{
				if (num <= 451766088U)
				{
					if (num <= 401286136U)
					{
						if (num <= 350806184U)
						{
							if (num <= 233362851U)
							{
								if (num != 216585232U)
								{
									if (num == 233362851U)
									{
										if (codigo == "68")
										{
											return "68-Nosso número (número bancário) inválido";
										}
									}
								}
								else if (codigo == "69")
								{
									return "69-Concessão de abatimento não é permitida para moeda diferente de Real";
								}
							}
							else if (num != 334175660U)
							{
								if (num == 350806184U)
								{
									if (codigo == "61")
									{
										return "61-Titulo com valor em moeda variável não permite condição de desconto";
									}
								}
							}
							else if (codigo == "18")
							{
								return "18-Nosso número (número bancário) já existente para outro título";
							}
						}
						else if (num <= 367583803U)
						{
							if (num != 350953279U)
							{
								if (num == 367583803U)
								{
									if (codigo == "60")
									{
										return "60-Valor do desconto igual ou maior que o valor do título";
									}
								}
							}
							else if (codigo == "19")
							{
								return "19-Valor do título inválido";
							}
						}
						else if (num != 384361422U)
						{
							if (num != 401139041U)
							{
								if (num == 401286136U)
								{
									if (codigo == "14")
									{
										return "14-Valor diário da multa não informado";
									}
								}
							}
							else if (codigo == "62")
							{
								return "62-Data do desconto informada não coincide com o registro do título";
							}
						}
						else if (codigo == "63")
						{
							return "63-Titulo não possui condição de desconto diário";
						}
					}
					else if (num <= 434694279U)
					{
						if (num <= 418063755U)
						{
							if (num != 417916660U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										return "15-Quantidade de dias após vencimento para incidência da multa não informada";
									}
								}
							}
							else if (codigo == "65")
							{
								return "65-Título devolvido em DDMMAA";
							}
						}
						else if (num != 418210850U)
						{
							if (num == 434694279U)
							{
								if (codigo == "64")
								{
									return "64-Título baixado em DDMMAA";
								}
							}
						}
						else if (codigo == "09")
						{
							return "09-Taxa mensal de mora acima do permitido (170%)";
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
									return "08-Nosso número(número bancário) utilizado não possui vinculação com a conta cobrança";
								}
							}
						}
						else if (codigo == "16")
						{
							return "16-Outras irregularidades";
						}
					}
					else if (num != 451471898U)
					{
						if (num != 451618993U)
						{
							if (num == 451766088U)
							{
								if (codigo == "07")
								{
									return "07-Data do vencimento inválida";
								}
							}
						}
						else if (codigo == "17")
						{
							return "17-Data de início da multa inválida";
						}
					}
					else if (codigo == "67")
					{
						return "67-Nosso número (número bancário) não informado";
					}
				}
				else if (num <= 518876564U)
				{
					if (num <= 485174231U)
					{
						if (num <= 468396612U)
						{
							if (num != 468249517U)
							{
								if (num == 468396612U)
								{
									if (codigo == "10")
									{
										return "10-Taxa de multa acima do permitido (10% ao mês)";
									}
								}
							}
							else if (codigo == "66")
							{
								return "66-Valor do título não confere com o registrado";
							}
						}
						else if (num != 468543707U)
						{
							if (num == 485174231U)
							{
								if (codigo == "11")
								{
									return "11-Data limite de desconto inválida";
								}
							}
						}
						else if (codigo == "06")
						{
							return "06-Prazo de protesto indefinido (não informado)/inválido ou prazo de protesto inferior aotempo decorrido da data de vencimento em relação ao envio da instrução de alteração de prazo";
						}
					}
					else if (num <= 501951850U)
					{
						if (num != 485321326U)
						{
							if (num == 501951850U)
							{
								if (codigo == "12")
								{
									return "12-CEP Inválido/Inexistência de Ag HSBC";
								}
							}
						}
						else if (codigo == "05")
						{
							return "05-Tipo de moeda inválido";
						}
					}
					else if (num != 502098945U)
					{
						if (num != 518729469U)
						{
							if (num == 518876564U)
							{
								if (codigo == "03")
								{
									return "03-CEP do sacado incorreto ou inválido";
								}
							}
						}
						else if (codigo == "13")
						{
							return "13-Valor/Taxa de multa inválida";
						}
					}
					else if (codigo == "04")
					{
						return "04-Cadastro do cedente não aceita banco correspondente";
					}
				}
				else if (num <= 2229355059U)
				{
					if (num <= 552431802U)
					{
						if (num != 535654183U)
						{
							if (num == 552431802U)
							{
								if (codigo == "01")
								{
									return "01-Valor do desconto não informado/inválido";
								}
							}
						}
						else if (codigo == "02")
						{
							return "02-Inexistência de agência do HSBC na praça do sacado";
						}
					}
					else if (num != 2212577440U)
					{
						if (num == 2229355059U)
						{
							if (codigo == "51")
							{
								return "51-Data de emissão da ocorrência inválida";
							}
						}
					}
					else if (codigo == "50")
					{
						return "50-Título liquidado em DDMMAA";
					}
				}
				else if (num <= 2247118416U)
				{
					if (num != 2246132678U)
					{
						if (num == 2247118416U)
						{
							if (codigo == "32")
							{
								return "32-Tipo de carteira inválido";
							}
						}
					}
					else if (codigo == "52")
					{
						return "52-Nosso número (número bancário) duplicado";
					}
				}
				else if (num != 2262910297U)
				{
					if (num != 2263057392U)
					{
						if (num == 2263896035U)
						{
							if (codigo == "33")
							{
								return "33-Conta corrente do cedente não compatível com o órgão do contratante";
							}
						}
					}
					else if (codigo == "43")
					{
						return "43-Controle do participante inválido";
					}
				}
				else if (codigo == "53")
				{
					return "53-Código de ocorrência comandada inválido";
				}
			}
			else if (num <= 2347092582U)
			{
				if (num <= 2313390249U)
				{
					if (num <= 2296465535U)
					{
						if (num <= 2279835011U)
						{
							if (num != 2279687916U)
							{
								if (num == 2279835011U)
								{
									if (codigo == "42")
									{
										return "42-Quantidade de moeda variável inválida";
									}
								}
							}
							else if (codigo == "54")
							{
								return "54-Valor do desconto concedido inválido";
							}
						}
						else if (num != 2280673654U)
						{
							if (num == 2296465535U)
							{
								if (codigo == "55")
								{
									return "55-Data de prorrogação de vencimento não informada";
								}
							}
						}
						else if (codigo == "30")
						{
							return "30-Número da conta do cedente inválido";
						}
					}
					else if (num <= 2297451273U)
					{
						if (num != 2296612630U)
						{
							if (num == 2297451273U)
							{
								if (codigo == "31")
								{
									return "31-Contrato garantia não cadastrado/inválido";
								}
							}
						}
						else if (codigo == "41")
						{
							return "41-Data de início de multa menor que data de emissão";
						}
					}
					else if (num != 2297598368U)
					{
						if (num != 2313243154U)
						{
							if (num == 2313390249U)
							{
								if (codigo == "40")
								{
									return "40-Ausência no nome do sacador avalista";
								}
							}
						}
						else if (codigo == "56")
						{
							return "56-Outras irregularidades";
						}
					}
					else if (codigo == "25")
					{
						return "25-Utilizada mais de uma instrução de multa";
					}
				}
				else if (num <= 2330020773U)
				{
					if (num <= 2314228892U)
					{
						if (num != 2313537344U)
						{
							if (num == 2314228892U)
							{
								if (codigo == "36")
								{
									return "36-Data de emissão do título inválida";
								}
							}
						}
						else if (codigo == "76")
						{
							return "76-Data de prorrogação de vencimento inválida";
						}
					}
					else if (num != 2314375987U)
					{
						if (num == 2330020773U)
						{
							if (codigo == "57")
							{
								return "57-Ocorrência não permitida para título em garantia de operações";
							}
						}
					}
					else if (codigo == "24")
					{
						return "24-Condição de desconto não permitida para titulo em garantia de Operação";
					}
				}
				else if (num <= 2331006511U)
				{
					if (num != 2330314963U)
					{
						if (num == 2331006511U)
						{
							if (codigo == "37")
							{
								return "37-Valor do título acima de R$ 5.000.000,00 (Cinco milhões de reais)";
							}
						}
					}
					else if (codigo == "77")
					{
						return "77-Data da instrução inválida";
					}
				}
				else if (num != 2331153606U)
				{
					if (num != 2346798392U)
					{
						if (num == 2347092582U)
						{
							if (codigo == "74")
							{
								return "74-Cancelamento comandado sobre título sem desconto";
							}
						}
					}
					else if (codigo == "58")
					{
						return "58-Nosso número (número bancário) comandado na instrução/ocorrência não possui vinculação com a conta cobrança";
					}
				}
				else if (codigo == "27")
				{
					return "27-CEP inválido do sacado";
				}
			}
			else if (num <= 2380647820U)
			{
				if (num <= 2363723106U)
				{
					if (num <= 2347931225U)
					{
						if (num != 2347784130U)
						{
							if (num == 2347931225U)
							{
								if (codigo == "26")
								{
									return "26-Ausência do endereço do sacado";
								}
							}
						}
						else if (codigo == "34")
						{
							return "34-Faixa de aplicação não cadastrada/inválida";
						}
					}
					else if (num != 2363576011U)
					{
						if (num == 2363723106U)
						{
							if (codigo == "45")
							{
								return "45-Título não aceito para compor a carteira de garantias";
							}
						}
					}
					else if (codigo == "59")
					{
						return "59-Nosso número (número bancário) comandado na baixa não possui vinculação com a conta cobrança";
					}
				}
				else if (num <= 2364561749U)
				{
					if (num != 2363870201U)
					{
						if (num == 2364561749U)
						{
							if (codigo == "35")
							{
								return "35-Nosso número (número bancário) inválido";
							}
						}
					}
					else if (codigo == "75")
					{
						return "75-Data de vencimento alterado inválida";
					}
				}
				else if (num != 2364708844U)
				{
					if (num != 2380500725U)
					{
						if (num == 2380647820U)
						{
							if (codigo == "72")
							{
								return "72-Concessão de desconto não é permitida para moeda diferente de real";
							}
						}
					}
					else if (codigo == "44")
					{
						return "44-Nosso número (número bancário) duplicado no mesmo movimento";
					}
				}
				else if (codigo == "21")
				{
					return "21-Título sem borderô";
				}
			}
			else if (num <= 2414894606U)
			{
				if (num <= 2397425439U)
				{
					if (num != 2381486463U)
					{
						if (num == 2397425439U)
						{
							if (codigo == "73")
							{
								return "73-Valor do desconto não informado";
							}
						}
					}
					else if (codigo == "20")
					{
						return "20-Ausência CEP/Endereço/CNPJ ou Sacador Avalista";
					}
				}
				else if (num != 2398264082U)
				{
					if (num != 2414203058U)
					{
						if (num == 2414894606U)
						{
							if (codigo == "38")
							{
								return "38-Data de desconto menor que data da emissão";
							}
						}
					}
					else if (codigo == "70")
					{
						return "70-Valor do abatimento concedido inválido. (Valor do abatimento zerado, maior ou igual ao valor do título)";
					}
				}
				else if (codigo == "23")
				{
					return "23-Instrução não permitida para título em garantia de operação";
				}
			}
			else if (num <= 2430980677U)
			{
				if (num != 2415041701U)
				{
					if (num == 2430980677U)
					{
						if (codigo == "71")
						{
							return "71-Cancelamento comandado sobre título sem abatimento";
						}
					}
				}
				else if (codigo == "22")
				{
					return "22-Número da conta do cedente não cadastrado";
				}
			}
			else if (num != 2431672225U)
			{
				if (num != 2498929796U)
				{
					if (num == 2515707415U)
					{
						if (codigo == "28")
						{
							return "28-Ausência do CPF/CNPJ do sacado em título com instrução de protesto";
						}
					}
				}
				else if (codigo == "29")
				{
					return "29-Agência cedente informada inválida";
				}
			}
			else if (codigo == "39")
			{
				return "39-Espécie inválida";
			}
			return "";
		}

		// Token: 0x0400065A RID: 1626
		private string _dacNossoNumero = string.Empty;

		// Token: 0x0400065B RID: 1627
		private int _dacBoleto;

		// Token: 0x0400065C RID: 1628
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200016D RID: 365
	internal sealed class BancoUnicred : AbstractBanco, IBanco
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		internal BancoUnicred()
		{
			this.Codigo = 136;
			this.Digito = "8";
			this.Nome = "Unicred";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0005BE04 File Offset: 0x0005A004
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0005BE44 File Offset: 0x0005A044
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}-{1}/{2}-{3}", new object[]
			{
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 10),
				boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture)
			});
			if (string.IsNullOrEmpty(boleto.Carteira))
			{
				throw new NotImplementedException("Carteira não informada. Utilize a carteira 'Cobrança com registro'");
			}
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (Utils.ToInt64(boleto.NossoNumero) == 0L)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (Convert.ToInt64(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 10)
			{
				throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 10 números.");
			}
			if (Convert.ToInt64(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 10)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 10);
			}
			this._dacNossoNumero = AbstractBanco.Mod11Base9(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture);
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA/ CORRESPONDENTE BANCÁRIO";
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumero(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0005BFE4 File Offset: 0x0005A1E4
		public int Mod11Base9Unicred(string seq)
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
				num3 = 1;
			}
			return num3;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0005C050 File Offset: 0x0005A250
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", new object[]
				{
					this.Codigo,
					boleto.Moeda,
					AbstractBanco.FatorVencimento(boleto),
					text,
					Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4),
					Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 9),
					Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoConta, 1),
					boleto.NossoNumero + this._dacNossoNumero
				});
				this._dacBoleto = this.Mod11Base9Unicred(boleto.CodigoBarra.Codigo);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0005C1C4 File Offset: 0x0005A3C4
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
				string text3 = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);
				string text4 = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 9).Substring(0, 1);
				string text5 = AbstractBanco.Mod10(text + text2 + text3 + text4).ToString(CultureInfo.InvariantCulture);
				string text6 = string.Format("{0}{1}{2}.{3}{4}{5} ", new object[]
				{
					this.Codigo,
					text2,
					text3.Substring(0, 1),
					text3.Substring(1, 3),
					text4,
					text5
				});
				string text7 = string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4)) + string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 9)) + string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoConta, 1)) + string.Format("{0}", boleto.NossoNumero + this._dacNossoNumero);
				string text8 = text7.Substring(5, 10);
				int num = AbstractBanco.Mod10(text8);
				string text9 = string.Format("{0}.{1}{2} ", text8.Substring(0, 5), text8.Substring(5, 5), num);
				string text10 = text7.Substring(15, 10);
				string text11 = AbstractBanco.Mod10(text10).ToString(CultureInfo.InvariantCulture);
				string text12 = string.Format("{0}.{1}{2}", text10.Substring(0, 5), text10.Substring(5, 5), text11);
				string text13 = string.Format(" {0} ", this._dacBoleto);
				string text14 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
				string text15 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text15 = Utils.FormatCode(text15, 10);
				string text16 = text14 + text15;
				boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text6, text9, text12, text13, text16 });
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar linha digitável.", ex);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0005C460 File Offset: 0x0005A660
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NossoNumero = boleto.NossoNumero;
				boleto.DigitoNossoNumero = this._dacNossoNumero;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
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

		// Token: 0x0600074F RID: 1871 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarHeaderRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0005C4FC File Offset: 0x0005A6FC
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text3;
			try
			{
				string text = new string(' ', 277);
				string text2 = "01REMESSA01COBRANCA       ";
				TipoSolucao tipoSolucao = cedente.TipoSolucao;
				if (tipoSolucao != TipoSolucao.UnicredTrocaEletronicaBeneficiario)
				{
					if (tipoSolucao == TipoSolucao.UnicredCobrancaBeneficiarioWeb)
					{
						text2 += Utils.FitStringLength(cedente.Codigo + cedente.DigitoCedente, 20, 20, '0', 0, true, true, true);
					}
				}
				else
				{
					text2 += Utils.FitStringLength(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, 5, 5, '0', 0, true, true, true);
					text2 += Utils.FitStringLength(cedente.ContaBancaria.Conta + cedente.ContaBancaria.DigitoConta, 10, 10, '0', 0, true, true, true);
					text2 += "     ";
				}
				text2 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
				text2 += "136";
				text2 += Utils.FitStringLength("Unicred", 15, 15, ' ', 0, true, true, false).ToUpper();
				text2 += DateTime.Now.ToString("ddMMyy");
				text2 += "       ";
				text2 += "000";
				text2 += Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true);
				text2 += text;
				text2 += "000001";
				text2 = Utils.SubstituiCaracteresEspeciais(text2);
				text3 = text2;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text3;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0005C6B8 File Offset: 0x0005A8B8
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

		// Token: 0x06000752 RID: 1874 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0005C730 File Offset: 0x0005A930
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = "1";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += "0";
				text += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
				text += "0000000000000";
				text += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text += "136";
				text += "00";
				text += new string(' ', 25);
				text += "0";
				if (boleto.PercMulta > 0m)
				{
					text += "2";
					text += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 10, 10, '0', 0, true, true, true);
				}
				else
				{
					text += "3";
					text += "0000000000";
				}
				if (boleto.PercJurosMora > 0m)
				{
					text += "1";
				}
				else
				{
					text += "5";
				}
				text += "N";
				text += "N";
				text += " ";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text += boleto.DataVencimento.ToString("ddMMyy");
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
				if (tipoSolucao != TipoSolucao.UnicredTrocaEletronicaBeneficiario)
				{
					if (tipoSolucao == TipoSolucao.UnicredCobrancaBeneficiarioWeb)
					{
						text += "0000000000";
					}
				}
				else
				{
					text += "   0000000";
				}
				if (boleto.ValorDesconto > 0m)
				{
					text += "1";
				}
				else
				{
					text += "0";
				}
				text += boleto.DataDocumento.ToString("ddMMyy");
				tipoSolucao = boleto.Cedente.TipoSolucao;
				if (tipoSolucao != TipoSolucao.UnicredTrocaEletronicaBeneficiario)
				{
					if (tipoSolucao == TipoSolucao.UnicredCobrancaBeneficiarioWeb)
					{
						text += "0";
						try
						{
							if (boleto.Instrucoes.Count == 0)
							{
								text += "3";
								text += "00";
							}
							else
							{
								switch (boleto.Instrucoes[0].Codigo)
								{
								case 1:
									text += "1";
									text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									break;
								case 2:
									text += "2";
									text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									break;
								case 3:
									text += "3";
									text += "00";
									break;
								default:
								{
									int codigo = boleto.Instrucoes[1].Codigo;
									if (codigo != 1)
									{
										if (codigo != 2)
										{
											text += "3";
											text += "00";
										}
										else
										{
											text += "2";
											text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
										}
									}
									else
									{
										text += "1";
										text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
									break;
								}
								}
							}
						}
						catch (Exception)
						{
							text += "3";
							text += "00";
						}
					}
				}
				else
				{
					text += "0000";
				}
				text += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NossoNumero, 10, 10, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
				text += "00";
				text += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
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
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 20, 20, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Nome, 38, 38, ' ', 0, true, true, false).ToUpper();
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

		// Token: 0x06000754 RID: 1876 RVA: 0x0005CFCC File Offset: 0x0005B1CC
		public override string GerarDetalheDadosPagadorAvalistaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = string.Empty;
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarDetalheDadosPagadorAvalistaRemessaCnab240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarDetalheDadosPagadorAvalistaRemessaCnab400(boleto, numeroRegistro, tipoArquivo);
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE com dados do pagador/avalista do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheDadosPagadorAvalistaRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0005D044 File Offset: 0x0005B244
		public string GerarDetalheDadosPagadorAvalistaRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text4;
			try
			{
				string text = new string(' ', 120);
				string text2 = new string('0', 60);
				base.GerarDetalheDadosPagadorAvalistaRemessa(boleto, numeroRegistro, tipoArquivo);
				string text3 = "5";
				text3 += text;
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text3 += "01";
				}
				else
				{
					text3 += "02";
				}
				text3 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text3 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text3 += text;
				text3 += text2;
				text3 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text4;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0005D26C File Offset: 0x0005B46C
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

		// Token: 0x06000758 RID: 1880 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0005D2E4 File Offset: 0x0005B4E4
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

		// Token: 0x0600075A RID: 1882 RVA: 0x0005D350 File Offset: 0x0005B550
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(279, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					DescricaoOcorrencia = this.RetornarDescricaoDaOcorrencia(registro.Substring(108, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"))
				};
				decimal num2 = Convert.ToUInt64(registro.Substring(181, 7));
				detalheRetornoCNAB.TarifaCobranca = num2 / 100m;
				decimal num3 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num4 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				detalheRetornoCNAB.DataVencimento = Utils.ToDateTime(registro.Substring(146, 6));
				decimal num5 = Convert.ToUInt64(registro.Substring(152, 13));
				detalheRetornoCNAB.ValorTitulo = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(240, 13));
				detalheRetornoCNAB.Descontos = num6 / 100m;
				detalheRetornoCNAB.NossoNumero = registro.Substring(47, 14);
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(registro.Substring(175, 6));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				switch (codigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_020D;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarMotivoDaRejeicao(registro.Substring(318, 2));
					goto IL_020D;
				case 4:
				case 5:
				case 7:
				case 8:
					goto IL_0206;
				case 6:
					break;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					goto IL_020D;
				default:
					if (codigoOcorrencia != 15)
					{
						goto IL_0206;
					}
					break;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
				goto IL_020D;
				IL_0206:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_020D:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0005D59C File Offset: 0x0005B79C
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

		// Token: 0x0600075C RID: 1884 RVA: 0x0005DB94 File Offset: 0x0005BD94
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2296000396U)
			{
				if (num <= 485321326U)
				{
					if (num <= 434841374U)
					{
						if (num <= 367583803U)
						{
							if (num != 334175660U)
							{
								if (num == 367583803U)
								{
									if (codigo == "60")
									{
										return "60-Movimento para Título não Cadastrado";
									}
								}
							}
							else if (codigo == "18")
							{
								return "18-Vencimento fora do Prazo de Operação";
							}
						}
						else if (num != 384361422U)
						{
							if (num != 418210850U)
							{
								if (num == 434841374U)
								{
									if (codigo == "16")
									{
										return "16-Data de Vencimento Inválida";
									}
								}
							}
							else if (codigo == "09")
							{
								return "09-Nosso Número Duplicado";
							}
						}
						else if (codigo == "63")
						{
							return "63-Entrada para Título já Cadastrado";
						}
					}
					else if (num <= 451766088U)
					{
						if (num != 434988469U)
						{
							if (num != 451618993U)
							{
								if (num == 451766088U)
								{
									if (codigo == "07")
									{
										return "07-Agência - Conta Inválida";
									}
								}
							}
							else if (codigo == "17")
							{
								return "17-Data de Vencimento Anterior à Data de Emissão";
							}
						}
						else if (codigo == "08")
						{
							return "08-Nosso Número Inválido";
						}
					}
					else if (num != 468396612U)
					{
						if (num != 468543707U)
						{
							if (num == 485321326U)
							{
								if (codigo == "05")
								{
									return "05-Código de Movimento Inválido";
								}
							}
						}
						else if (codigo == "06")
						{
							return "06-Número de Inscrição do Beneficiário Inválido";
						}
					}
					else if (codigo == "10")
					{
						return "10-Carteira inválida";
					}
				}
				else if (num <= 2245667539U)
				{
					if (num <= 502098945U)
					{
						if (num != 501951850U)
						{
							if (num == 502098945U)
							{
								if (codigo == "04")
								{
									return "04-Código de Movimento não permitido para a carteira";
								}
							}
						}
						else if (codigo == "12")
						{
							return "12-Tipo de Documento Inválido";
						}
					}
					else if (num != 552431802U)
					{
						if (num != 569209421U)
						{
							if (num == 2245667539U)
							{
								if (codigo == "C3")
								{
									return "C3-Status do Borderô Inválido";
								}
							}
						}
						else if (codigo == "00")
						{
							return "Sem Complemento a informar";
						}
					}
					else if (codigo == "01")
					{
						return "01-Código do Banco Inválido";
					}
				}
				else if (num <= 2263896035U)
				{
					if (num != 2246132678U)
					{
						if (num != 2262445158U)
						{
							if (num == 2263896035U)
							{
								if (codigo == "33")
								{
									return "33-Valor de Abatimento Inválido";
								}
							}
						}
						else if (codigo == "C0")
						{
							return "C0-Sistema Intermitente – Entre em contato com sua Cooperativa";
						}
					}
					else if (codigo == "52")
					{
						return "52-Unidade Federativa Inválida";
					}
				}
				else if (num != 2279222777U)
				{
					if (num != 2280673654U)
					{
						if (num == 2296000396U)
						{
							if (codigo == "C6")
							{
								return "C6-Instrução não Atualiza Cadastro do Título";
							}
						}
					}
					else if (codigo == "30")
					{
						return "30-Alteração de Dados Rejeitada";
					}
				}
				else if (codigo == "C1")
				{
					return "C1-Situação do título Aberto";
				}
			}
			else if (num <= 2347931225U)
			{
				if (num <= 2330167868U)
				{
					if (num <= 2314375987U)
					{
						if (num != 2297598368U)
						{
							if (num == 2314375987U)
							{
								if (codigo == "24")
								{
									return "24-Data de Emissão Inválida";
								}
							}
						}
						else if (codigo == "25")
						{
							return "25-Data de Emissão Posterior à data de Entrega";
						}
					}
					else if (num != 2329555634U)
					{
						if (num != 2330020773U)
						{
							if (num == 2330167868U)
							{
								if (codigo == "47")
								{
									return "47-Endereço do Pagador Não Informado";
								}
							}
						}
						else if (codigo == "57")
						{
							return "57-Código de Multa inválido";
						}
					}
					else if (codigo == "C4")
					{
						return "C4-Nome do Beneficiário Inválido";
					}
				}
				else if (num <= 2346798392U)
				{
					if (num != 2331153606U)
					{
						if (num != 2346333253U)
						{
							if (num == 2346798392U)
							{
								if (codigo == "58")
								{
									return "58-Data de Multa inválido";
								}
							}
						}
						else if (codigo == "C5")
						{
							return "C5-Documento Inválido";
						}
					}
					else if (codigo == "27")
					{
						return "27-Valor de juros inválido";
					}
				}
				else if (num != 2346945487U)
				{
					if (num != 2347784130U)
					{
						if (num == 2347931225U)
						{
							if (codigo == "26")
							{
								return "26-Código de juros inválido";
							}
						}
					}
					else if (codigo == "34")
					{
						return "34-Valor do Abatimento Maior ou Igual ao Valor do título";
					}
				}
				else if (codigo == "46")
				{
					return "46-Número de Inscrição do Pagador Inválido";
				}
			}
			else if (num <= 2447611201U)
			{
				if (num <= 2363576011U)
				{
					if (num != 2348916963U)
					{
						if (num == 2363576011U)
						{
							if (codigo == "59")
							{
								return "59-Valor / percentual de Multa inválido";
							}
						}
					}
					else if (codigo == "86")
					{
						return "86-Seu Número Inválido";
					}
				}
				else if (num != 2363723106U)
				{
					if (num != 2381486463U)
					{
						if (num == 2447611201U)
						{
							if (codigo == "48")
							{
								return "48-CEP Inválido";
							}
						}
					}
					else if (codigo == "20")
					{
						return "20-Valor do Título Inválido";
					}
				}
				else if (codigo == "45")
				{
					return "45-Nome do Pagador não informado";
				}
			}
			else if (num <= 2498929796U)
			{
				if (num != 2449582677U)
				{
					if (num != 2463482396U)
					{
						if (num == 2498929796U)
						{
							if (codigo == "29")
							{
								return "29-Valor de Desconto inválido";
							}
						}
					}
					else if (codigo == "A8")
					{
						return "A8-Valor do Abatimento Inválido para Cancelamento";
					}
				}
				else if (codigo == "80")
				{
					return "80-Data de Desconto inválida";
				}
			}
			else if (num != 2515707415U)
			{
				if (num != 2547370491U)
				{
					if (num == 2565201629U)
					{
						if (codigo == "79")
						{
							return "79-Data de Juros inválida";
						}
					}
				}
				else if (codigo == "A5")
				{
					return "A5-Título Liquidado";
				}
			}
			else if (codigo == "28")
			{
				return "28-Código de Desconto inválido";
			}
			return "";
		}

		// Token: 0x04000657 RID: 1623
		private string _dacNossoNumero = string.Empty;

		// Token: 0x04000658 RID: 1624
		private int _dacBoleto;

		// Token: 0x04000659 RID: 1625
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

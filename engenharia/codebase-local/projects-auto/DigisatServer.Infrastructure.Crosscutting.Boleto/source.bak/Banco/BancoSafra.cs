using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000171 RID: 369
	internal sealed class BancoSafra : AbstractBanco, IBanco
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00064E7C File Offset: 0x0006307C
		internal BancoSafra()
		{
			this.Codigo = 422;
			this.Digito = "7";
			this.Nome = "Banco Safra";
			this.DescricaoBeneficiarioSacadorAvalista = "Beneficiário final";
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00064ECC File Offset: 0x000630CC
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00064F0C File Offset: 0x0006310C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormatoAgenciaCodigoCedente = Utils.FormataAgenciaConta(boleto.Cedente.ContaBancaria.Agencia, boleto.Cedente.ContaBancaria.DigitoAgencia, boleto.Cedente.ContaBancaria.Conta, boleto.Cedente.ContaBancaria.DigitoConta);
				if (boleto.TipoCobranca.Codigo == 1 && boleto.TipoEmissao.Codigo == 1)
				{
					boleto.NossoNumero = Utils.FormatCode("0", 8);
					boleto.DigitoNossoNumero = "0";
				}
				else
				{
					if (string.IsNullOrEmpty(boleto.NossoNumero))
					{
						throw new NotImplementedException("Nosso número inválido");
					}
					if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 8)
					{
						boleto.NossoNumero = Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true);
					}
					boleto.DigitoNossoNumero = AbstractBanco.Mod10(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture);
				}
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no BANCO SAFRA S/A";
				}
				this.FormataCodigoBarra(boleto);
				this.FormataLinhaDigitavel(boleto);
				this.FormataNumeroDocumento(boleto);
				this._codigoCarteira = boleto.Carteira;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0006508C File Offset: 0x0006328C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			string text2 = Utils.FormatCode(boleto.Moeda.ToString(CultureInfo.InvariantCulture), 1);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text3 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text4 = string.Concat(new string[]
			{
				"7",
				Utils.FitStringLength((boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia).CopiarTextoDaDireitaParaEsquerda(5), 5, 5, '0', 0, true, true, true),
				Utils.FitStringLength((boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta).CopiarTextoDaDireitaParaEsquerda(9), 9, 9, '0', 0, true, true, true),
				boleto.NossoNumero,
				boleto.DigitoNossoNumero,
				boleto.TipoCobranca.Codigo.ToString()
			});
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[] { text, text2, num, text3, text4 });
			this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00065254 File Offset: 0x00063454
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.CodigoBarra.Codigo.Substring(0, 4);
			string text2 = boleto.CodigoBarra.Codigo.Substring(19, 5);
			string text3 = AbstractBanco.Mod10(text + text2).ToString(CultureInfo.InvariantCulture);
			string text4 = text + text2 + text3;
			text4 = text4.Substring(0, 5) + "." + text4.Substring(5) + " ";
			text = boleto.CodigoBarra.Codigo.Substring(24, 10);
			text2 = AbstractBanco.Mod10(text).ToString(CultureInfo.InvariantCulture);
			string text5 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			text = boleto.CodigoBarra.Codigo.Substring(34, 10);
			text2 = AbstractBanco.Mod10(text).ToString(CultureInfo.InvariantCulture);
			string text6 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			int dacBoleto = this._dacBoleto;
			string text7 = string.Format("{0} ", dacBoleto);
			text = boleto.CodigoBarra.Codigo.Substring(5, 4);
			text2 = boleto.CodigoBarra.Codigo.Substring(9, 10);
			string text8 = string.Format("{0}{1}", text, text2);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text4, text5, text6, text7, text8 });
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000653D8 File Offset: 0x000635D8
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}-{1}", boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000653F6 File Offset: 0x000635F6
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? string.Format("{0}-{1}", boleto.NossoNumero, boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0006542C File Offset: 0x0006362C
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = " ";
				base.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarHeaderRemessaCnab240(cedente, numeroArquivoRemessa);
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderRemessaCnab400(0, cedente, numeroArquivoRemessa);
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (NotImplementedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000654BC File Offset: 0x000636BC
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + text + Utils.FormatCode(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, "0", 5, true) + " " + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("BANCO SAFRA S/A", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + "000000" + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true) + "103" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00058384 File Offset: 0x00056584
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Funçao não implementada!");
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000656BC File Offset: 0x000638BC
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
				base.GerarHeaderLoteRemessa(numeroConvenio, cedente, numeroArquivoRemessa, tipoArquivo);
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarHeaderLoteRemessaCnab240(cedente, numeroArquivoRemessa);
					break;
				case TipoArquivo.CNAB400:
					text = "";
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (NotImplementedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DO LOTE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00065744 File Offset: 0x00063944
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text5;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 40);
				string text3 = new string(' ', 33);
				string text4 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				text4 += "0001";
				text4 += "1";
				text4 += "R";
				text4 += "01";
				text4 += "  ";
				text4 += "060";
				text4 += " ";
				if (cedente.Cpfcnpj.Length <= 11)
				{
					text4 += "1";
				}
				else
				{
					text4 += "2";
				}
				text4 += Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true);
				text4 += text;
				text4 += Utils.FormatCode(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, "0", 5, true);
				text4 += " ";
				text4 += Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true);
				text4 += Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true);
				text4 += " ";
				text4 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
				text4 += text2;
				text4 += text2;
				text4 += Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true);
				text4 += DateTime.Now.ToString("ddMMyyyy");
				text4 += "00000000";
				text4 += text3;
				text4 = Utils.SubstituiCaracteresEspeciais(text4);
				text5 = text4;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
			}
			return text5;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00065968 File Offset: 0x00063B68
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
					text = this.GerarDetalheRemessaCnab400();
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (NotImplementedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab400()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000659EC File Offset: 0x00063BEC
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text2;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				text += "0001";
				text += "3";
				text += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text += "P";
				text += " ";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 5, true);
				text += " ";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += " ";
				text += Utils.FitStringLength(boleto.NossoNumero + boleto.DigitoNossoNumero, 20, 20, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += "2";
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoDistribuicao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 5, true);
				text += " ";
				text += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text += boleto.Aceite;
				text += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				if (boleto.JurosMora > 0m)
				{
					decimal num = ((boleto.JurosMora < 0.01m) ? 0.01m : boleto.JurosMora);
					text += "1";
					text += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text += Utils.FitStringLength(num.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				}
				else
				{
					text += "3";
					text += "00000000";
					text += "000000000000000";
				}
				if (boleto.ValorDesconto > 0m)
				{
					text += "1";
					text += Utils.FitStringLength(boleto.DataDesconto.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				}
				else
				{
					text += "0";
					text += "00000000";
					text += "000000000000000";
				}
				text += "000000000000000";
				text += "000000000000000";
				text += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
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
						case 2:
							text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							break;
						case 3:
							text += "3";
							text += "00";
							break;
						default:
						{
							int num2 = boleto.Instrucoes[1].Codigo;
							if (num2 == 1 || num2 == 2)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "3";
								text += "00";
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
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text += "2";
						text += "000";
					}
					else
					{
						int num2 = boleto.Instrucoes[0].Codigo;
						if (num2 != 4)
						{
							if (num2 != 5)
							{
								num2 = boleto.Instrucoes[1].Codigo;
								if (num2 == 4)
								{
									text += "1";
									text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
								}
								else
								{
									text += "2";
									text += "000";
								}
							}
							else
							{
								text += "2";
								text += "000";
							}
						}
						else
						{
							text += "1";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
						}
					}
				}
				catch (Exception)
				{
					text += "2";
					text += "000";
				}
				text += "09";
				text += "00000000001";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00066204 File Offset: 0x00064404
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text5;
			try
			{
				string text = new string('0', 16);
				string text2 = new string(' ', 28);
				string text3 = new string(' ', 40);
				string text4 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				text4 += "0001";
				text4 += "3";
				text4 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text4 += "Q";
				text4 += " ";
				text4 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text4 += "1";
				}
				else
				{
					text4 += "2";
				}
				text4 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 15, 15, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper();
				text4 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text4 += text;
				text4 += text3;
				text4 += "000";
				text4 += text2;
				text4 = Utils.SubstituiCaracteresEspeciais(text4);
				text5 = text4;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text5;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000664E8 File Offset: 0x000646E8
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = new string(' ', 151);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "0" + "00000000" + "000000000000000" + "0" + "00000000" + "000000000000000" + "2" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00066668 File Offset: 0x00064868
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
					text = this.GerarTrailerRemessaCnab240(numeroRegistro);
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarTrailerRemessaCnab400(numeroRegistro, cedente);
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (NotImplementedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000666F0 File Offset: 0x000648F0
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais("42299999" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", 6) + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00066784 File Offset: 0x00064984
		public string GerarTrailerRemessaCnab400(int numeroRegistro, Cedente cedente)
		{
			string text2;
			try
			{
				string text = new string(' ', 384);
				string conta = cedente.ContaBancaria.Conta;
				text2 = Utils.SubstituiCaracteresEspeciais("91422" + Utils.FormatCode(conta, 5) + text + Utils.FormatCode(numeroRegistro.ToString(), 6));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000667FC File Offset: 0x000649FC
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text2;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text += "0001";
				text += "5";
				text += Utils.FormatCode("", " ", 9);
				text += Utils.FitStringLength((numeroRegistroSegmentoPQR + 2).ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text2 = this._codigoCarteira;
				if (!(text2 == "1"))
				{
					if (text2 == "3")
					{
						text += Utils.FormatCode("", "0", 6);
						text += Utils.FormatCode("", "0", 17);
						text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
						text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
					}
				}
				else
				{
					text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
					text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
					text += Utils.FormatCode("", "0", 6);
					text += Utils.FormatCode("", "0", 17);
				}
				text += Utils.FormatCode("", "0", 6);
				text += Utils.FormatCode("", "0", 17);
				text += Utils.FormatCode("", "0", 6);
				text += Utils.FormatCode("", "0", 17);
				text += Utils.FormatCode("", " ", 8);
				text += Utils.FormatCode("", " ", 117);
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00066A70 File Offset: 0x00064C70
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "9999" + "9" + "         " + "000001" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + "000000" + text);
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00066B28 File Offset: 0x00064D28
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro);
				int num = Utils.ToInt32(registro.Substring(0, 1));
				detalheRetornoCNAB.IdentificacaoDoRegistro = num;
				detalheRetornoCNAB.NossoNumero = registro.Substring(47, 15);
				int num2 = Utils.ToInt32(registro.Substring(108, 2));
				detalheRetornoCNAB.CodigoOcorrencia = num2;
				detalheRetornoCNAB.DataOcorrencia = Utils.ToDateTime(Utils.ToInt32(registro.Substring(110, 6)).ToString("##-##-##"));
				detalheRetornoCNAB.SeuNumero = registro.Substring(116, 10);
				detalheRetornoCNAB.DataVencimento = Utils.ToDateTime(Utils.ToInt32(registro.Substring(146, 6)).ToString("##-##-##"));
				decimal num3 = Convert.ToUInt64(registro.Substring(152, 13));
				detalheRetornoCNAB.ValorTitulo = num3 / 100m;
				detalheRetornoCNAB.EspecieTitulo = registro.Substring(174, 1);
				decimal num4 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.DespesasDeCobranca = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(188, 13));
				detalheRetornoCNAB.OutrasDespesas = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(227, 13));
				detalheRetornoCNAB.Abatimentos = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(240, 13));
				detalheRetornoCNAB.Descontos = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPago = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num9 / 100m;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(Utils.ToInt32(registro.Substring(328, 8)).ToString("####-##-##"));
				detalheRetornoCNAB.MotivosRejeicao = registro.Substring(318, 10);
				detalheRetornoCNAB.NomeSacado = registro.Substring(19, 5);
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x04000663 RID: 1635
		private int _dacBoleto;

		// Token: 0x04000664 RID: 1636
		private string _codigoCarteira;

		// Token: 0x04000665 RID: 1637
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

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
	// Token: 0x02000159 RID: 345
	internal sealed class BancoBanrisul : AbstractBanco, IBanco
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x0002D210 File Offset: 0x0002B410
		internal BancoBanrisul()
		{
			this.Codigo = 41;
			this.Digito = "8";
			this.Nome = "Banco Banrisul";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002D25C File Offset: 0x0002B45C
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("COBR240_{0}_{1:ddMMyyyyHHmmss}.240", this._alpha[this.SequenciaRemessaDiaria - 1], DateTime.Now.Date);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002D29C File Offset: 0x0002B49C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = string.Format("{0}", Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture) + boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 9));
				string text2 = string.Format("{0}.{1}-{2}", text.CopiarTexto(Sentido.CopiarDaEsquerdaParaDireita, 6), text.CopiarTexto(6, 1, true), text.CopiarTexto(7, 2, true));
				this.FormatoAgenciaCodigoCedente = string.Format("{0}/", boleto.Cedente.ContaBancaria.Agencia) + string.Format("{0}", text2);
				this.InstrucaoSacAtendimento = "SAC BANRISUL - 0800 646 1515 OUVIDORIA BANRISUL - 0800 644 2200";
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (boleto.Cedente.ContaBancaria.Conta.Length < 7)
				{
					boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);
				}
				if (boleto.NossoNumero.Length < 8)
				{
					boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 8);
				}
				boleto.DigitoNossoNumero = this.CalcularDigitosAutoConferencia(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture));
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Banrisul";
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

		// Token: 0x0600054F RID: 1359 RVA: 0x0002D454 File Offset: 0x0002B654
		public string CalcularDigitosAutoConferencia(string informacao)
		{
			int num = this.Mod10Banri(informacao);
			return this.Mod11Banri(informacao, num).ToString("00");
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002D480 File Offset: 0x0002B680
		private int Mod10Banri(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				int num3 = Convert.ToInt32(seq.Substring(i, 1));
				int num4 = ((num3 * num2 > 9) ? (num3 * num2 - 9) : (num3 * num2));
				num += num4;
				if (num2 == 2)
				{
					num2 = 1;
				}
				else
				{
					num2 = 2;
				}
			}
			int num5;
			if (num < 10)
			{
				num5 = num;
			}
			else
			{
				num5 = num % 10;
			}
			if (num5 != 0)
			{
				return 10 - num5;
			}
			return 0;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002D4F4 File Offset: 0x0002B6F4
		private int Mod11Banri(string seq, int dv1)
		{
			int num = 2;
			int num2 = 0;
			int num3 = 7;
			seq += dv1.ToString();
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				int num4 = Convert.ToInt32(seq.Substring(i, 1)) * num;
				num2 += num4;
				if (num < num3)
				{
					num++;
				}
				else
				{
					num = 2;
				}
			}
			seq = seq.Substring(0, seq.Length - 1);
			int num5 = ((num2 < 11) ? num2 : (num2 % 11));
			bool flag = num5 == 1;
			int num7;
			if (flag)
			{
				int num6 = ((dv1 == 9) ? 0 : (dv1 + 1));
				num7 = this.Mod11Banri(seq, num6);
			}
			else
			{
				num7 = ((num5 == 0) ? 0 : (11 - num5));
			}
			if (!flag)
			{
				return Convert.ToInt32(dv1.ToString() + num7);
			}
			return num7;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FitStringLength(this.Codigo.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text2 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text2 = Utils.FormatCode(text2, 10);
			string text3 = Utils.FitStringLength(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true);
			string text4 = string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				"21",
				boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(4).PreencherComCaractereEsquerda(4, '0'),
				text3,
				boleto.NossoNumero,
				"40"
			});
			text4 += this.CalcularNCCodBarras(text4);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[] { text, "9", num, text2, text4 });
			int num2 = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + num2 + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0002D744 File Offset: 0x0002B944
		private string CalcularNCCodBarras(string seq)
		{
			int num = this.Mod10Banri(seq);
			return this.Mod11Banri(seq, num).ToString("00");
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002D770 File Offset: 0x0002B970
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.CodigoBarra.Codigo.Substring(0, 4);
			string text2 = boleto.CodigoBarra.Codigo.Substring(19, 2);
			string text3 = boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(4).PreencherComCaractereEsquerda(4, '0').Substring(0, 3);
			string text4 = AbstractBanco.Mod10(text + text2 + text3).ToString(CultureInfo.InvariantCulture);
			string text5 = text + text2 + text3 + text4;
			text5 = text5.Substring(0, 5) + "." + text5.Substring(5) + " ";
			text = boleto.CodigoBarra.Codigo.Substring(24, 10);
			text2 = AbstractBanco.Mod10(text).ToString(CultureInfo.InvariantCulture);
			string text6 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			text = boleto.CodigoBarra.Codigo.Substring(34, 10);
			text2 = AbstractBanco.Mod10(text).ToString(CultureInfo.InvariantCulture);
			string text7 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			string text8 = boleto.CodigoBarra.Codigo.Substring(4, 1);
			string text9 = string.Format("{0} ", text8);
			text = boleto.CodigoBarra.Codigo.Substring(5, 4);
			text2 = boleto.CodigoBarra.Codigo.Substring(9, 10);
			string text10 = string.Format("{0}{1}", text, text2);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text5, text6, text7, text9, text10 });
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002D928 File Offset: 0x0002BB28
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}-{1}", boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002D946 File Offset: 0x0002BB46
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? string.Format("{0}-{1}", boleto.NossoNumero, boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text = " ";
			base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);
			switch (tipoArquivo)
			{
			case TipoArquivo.CNAB240:
				text = this.GerarHeaderRemessaCnab240(cedente, numeroArquivoRemessa);
				break;
			case TipoArquivo.CNAB400:
				text = this.GerarHeaderRemessaCnab400(cedente, numeroArquivoRemessa);
				break;
			case TipoArquivo.Outro:
				throw new Exception("Tipo de arquivo inexistente.");
			}
			return text;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002DA00 File Offset: 0x0002BC00
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + Utils.FormatCode(cedente.Convenio, " ", 20) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("BANRISUL", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true) + "103" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002DC14 File Offset: 0x0002BE14
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Funçao não implementada!");
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002DC2C File Offset: 0x0002BE2C
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

		// Token: 0x0600055C RID: 1372 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 40);
				string text2 = new string(' ', 33);
				string text3 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				text3 += "0001";
				text3 += "1";
				text3 += "R";
				text3 += "01";
				text3 += "  ";
				text3 += "060";
				text3 += " ";
				if (cedente.Cpfcnpj.Length <= 11)
				{
					text3 += "1";
				}
				else
				{
					text3 += "2";
				}
				text3 += Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true);
				text3 += Utils.FormatCode(cedente.Convenio, " ", 20);
				text3 += Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true);
				text3 += Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true);
				text3 += Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true);
				text3 += Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true);
				text3 += " ";
				text3 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
				text3 += text;
				text3 += text;
				text3 += Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true);
				text3 += DateTime.Now.ToString("ddMMyyyy");
				text3 += "00000000";
				text3 += text2;
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
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

		// Token: 0x0600055E RID: 1374 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002DF58 File Offset: 0x0002C158
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002DF70 File Offset: 0x0002C170
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
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 5, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 1, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += " ";
				text += Utils.FitStringLength(string.Format("{0}{1}", boleto.NossoNumero, boleto.DigitoNossoNumero), 20, 20, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += "2";
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoDistribuicao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 5, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 1, true);
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
				if (boleto.Carteira.Equals("4"))
				{
					text += "0";
					text += "00";
					text += "0";
					text += "000";
					text += "09";
					text += "0000603015";
					text += "1";
				}
				else
				{
					try
					{
						if (boleto.Instrucoes.Count == 0)
						{
							text += "3";
							text += "00";
						}
						else
						{
							int num2 = boleto.Instrucoes[0].Codigo;
							if (num2 != 1)
							{
								if (num2 != 3)
								{
									num2 = boleto.Instrucoes[1].Codigo;
									if (num2 == 1)
									{
										text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
										text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
									else
									{
										text += "3";
										text += "00";
									}
								}
								else
								{
									text += "3";
									text += "00";
								}
							}
							else
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
						}
					}
					catch (Exception)
					{
						text += "3";
						text += "00";
					}
					finally
					{
						text += "0";
						text += "000";
						text += "09";
						text += "0000805076";
						text += "1";
					}
				}
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
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

		// Token: 0x06000562 RID: 1378 RVA: 0x0002E9C8 File Offset: 0x0002CBC8
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				text2 += "0001";
				text2 += "3";
				text2 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text2 += "R";
				text2 += " ";
				text2 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text2 += "0";
				text2 += "00000000";
				text2 += "000000000000000";
				text2 += "0";
				text2 += "00000000";
				text2 += "000000000000000";
				if (boleto.Carteira.Equals("4"))
				{
					text2 += "0";
					text2 += "00000000";
					text2 += "000000000000000";
				}
				else
				{
					text2 += "2";
					text2 += Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text2 += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				}
				text2 += text;
				text2 += "00000000";
				text2 += "000";
				text2 += "00000";
				text2 += " ";
				text2 += "000000000000";
				text2 += " ";
				text2 += " ";
				text2 += "1";
				text2 += "         ";
				text2 = Utils.SubstituiCaracteresEspeciais(text2);
				text3 = text2;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002EC0C File Offset: 0x0002CE0C
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
					text = this.GerarTrailerRemessaCnab400(numeroRegistro, vltitulostotal);
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

		// Token: 0x06000564 RID: 1380 RVA: 0x0002EC84 File Offset: 0x0002CE84
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "9999" + "9" + Utils.FormatCode("", " ", 9) + "000001" + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("0", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002ED40 File Offset: 0x0002CF40
		public string GerarTrailerRemessaCnab400(int numeroRegistro, decimal vltitulostotal)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002ED58 File Offset: 0x0002CF58
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text2;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text += "0001";
				text += "5";
				text += Utils.FormatCode("", " ", 9);
				text += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text2 = this._codigoCarteira;
				if (!(text2 == "1"))
				{
					if (!(text2 == "2"))
					{
						if (!(text2 == "3"))
						{
							if (text2 == "4")
							{
								text += Utils.FormatCode("", "0", 6);
								text += Utils.FormatCode("", "0", 17);
								text += Utils.FormatCode("", "0", 6);
								text += Utils.FormatCode("", "0", 17);
								text += Utils.FormatCode("", "0", 6);
								text += Utils.FormatCode("", "0", 17);
								text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
								text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
							}
						}
						else
						{
							text += Utils.FormatCode("", "0", 6);
							text += Utils.FormatCode("", "0", 17);
							text += Utils.FormatCode("", "0", 6);
							text += Utils.FormatCode("", "0", 17);
							text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
							text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
							text += Utils.FormatCode("", "0", 6);
							text += Utils.FormatCode("", "0", 17);
						}
					}
					else
					{
						text += Utils.FormatCode("", "0", 6);
						text += Utils.FormatCode("", "0", 17);
						text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
						text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
						text += Utils.FormatCode("", "0", 6);
						text += Utils.FormatCode("", "0", 17);
						text += Utils.FormatCode("", "0", 6);
						text += Utils.FormatCode("", "0", 17);
					}
				}
				else
				{
					text += Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
					text += Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true);
					text += Utils.FormatCode("", "0", 6);
					text += Utils.FormatCode("", "0", 17);
					text += Utils.FormatCode("", "0", 6);
					text += Utils.FormatCode("", "0", 17);
					text += Utils.FormatCode("", "0", 6);
					text += Utils.FormatCode("", "0", 17);
				}
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

		// Token: 0x06000567 RID: 1383 RVA: 0x0002F20C File Offset: 0x0002D40C
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

		// Token: 0x06000568 RID: 1384 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
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
				detalheRetornoCNAB.NossoNumero = Utils.ToString(registro.Substring(126, 10));
				detalheRetornoCNAB.DACNossoNumero = Utils.ToInt32(registro.Substring(134, 2));
				decimal num2 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.TarifaCobranca = num2 / 100m;
				decimal num3 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num4 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(Utils.ToInt32(registro.Substring(295, 6)).ToString("##-##-##"));
				switch (detalheRetornoCNAB.CodigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_01DC;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(382, 10)).Replace("DDMMAA", num.ToString("##-##-##"));
					goto IL_01DC;
				case 6:
				case 7:
				case 8:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_01DC;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					goto IL_01DC;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_01DC:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 535654183U)
			{
				if (num <= 434988469U)
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
										return "68-Acerto dos dados do rateio de crédito";
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
									return "19-Confirmação de Instrução Protesto";
								}
							}
						}
						else if (codigo == "18")
						{
							return "18-Alteração de instruções";
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
									return "15-Pagamento em Cartório";
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
						if (num != 434841374U)
						{
							if (num == 434988469U)
							{
								if (codigo == "08")
								{
									return "08-Baixa Por Pagamento, Liquidação Pelo Saldo";
								}
							}
						}
						else if (codigo == "16")
						{
							return "16-Alteração de dados";
						}
					}
					else if (codigo == "09")
					{
						return "09-Devolução Automática";
					}
				}
				else if (num <= 485174231U)
				{
					if (num <= 468396612U)
					{
						if (num != 451766088U)
						{
							if (num == 468396612U)
							{
								if (codigo == "10")
								{
									return "10-Baixado Conforme Instruções";
								}
							}
						}
						else if (codigo == "07")
						{
							return "07-Liquidação Parcial";
						}
					}
					else if (num != 468543707U)
					{
						if (num == 485174231U)
						{
							if (codigo == "11")
							{
								return "11-Arquivo Levantamento";
							}
						}
					}
					else if (codigo == "06")
					{
						return "06-Liquidação Normal";
					}
				}
				else if (num <= 502098945U)
				{
					if (num != 501951850U)
					{
						if (num == 502098945U)
						{
							if (codigo == "04")
							{
								return "04-Baixa de Título Liquidado por Edital";
							}
						}
					}
					else if (codigo == "12")
					{
						return "12-Concessão de Abatimento";
					}
				}
				else if (num != 518729469U)
				{
					if (num != 518876564U)
					{
						if (num == 535654183U)
						{
							if (codigo == "02")
							{
								return "02-Confirmação da Entrada";
							}
						}
					}
					else if (codigo == "03")
					{
						return "03-Entrada Rejeitada";
					}
				}
				else if (codigo == "13")
				{
					return "13-Cancelamento de Abatimento";
				}
			}
			else if (num <= 2313390249U)
			{
				if (num <= 2279835011U)
				{
					if (num <= 2263057392U)
					{
						if (num != 2247118416U)
						{
							if (num == 2263057392U)
							{
								if (codigo == "43")
								{
									return "43-Relação de títulos";
								}
							}
						}
						else if (codigo == "32")
						{
							return "32-Reembolso e Transferência Desconto e Vendor ou Carteira em Garantia";
						}
					}
					else if (num != 2263896035U)
					{
						if (num == 2279835011U)
						{
							if (codigo == "42")
							{
								return "42-Alteração de título";
							}
						}
					}
					else if (codigo == "33")
					{
						return "33-Reembolso e Devolução Desconto e Vendor";
					}
				}
				else if (num <= 2296612630U)
				{
					if (num != 2280673654U)
					{
						if (num == 2296612630U)
						{
							if (codigo == "41")
							{
								return "41-Despesa de aponte";
							}
						}
					}
					else if (codigo == "30")
					{
						return "30-Cobrança a Creditar (Liquidação Em Trânsito)";
					}
				}
				else if (num != 2297451273U)
				{
					if (num != 2297598368U)
					{
						if (num == 2313390249U)
						{
							if (codigo == "40")
							{
								return "40-Baixa de títulos protestados";
							}
						}
					}
					else if (codigo == "25")
					{
						return "25-Devolução, Liquidado Anteriormente";
					}
				}
				else if (codigo == "31")
				{
					return "31-Título em trânsito pago em cartório";
				}
			}
			else if (num <= 2363723106U)
			{
				if (num <= 2346945487U)
				{
					if (num != 2330167868U)
					{
						if (num == 2346945487U)
						{
							if (codigo == "46")
							{
								return "46-Fornecimento de formulário pré-impresso";
							}
						}
					}
					else if (codigo == "47")
					{
						return "47-Confirmação de Entrada – Sacado DDA";
					}
				}
				else if (num != 2347784130U)
				{
					if (num != 2347931225U)
					{
						if (num == 2363723106U)
						{
							if (codigo == "45")
							{
								return "45-Sustação de cartório e envio de título a cartório";
							}
						}
					}
					else if (codigo == "26")
					{
						return "26-Devolvido Pelo Cartório - erro de informação";
					}
				}
				else if (codigo == "34")
				{
					return "34-Reembolso Não Efetuado Por Falta de Saldo";
				}
			}
			else if (num <= 2380500725U)
			{
				if (num != 2364708844U)
				{
					if (num == 2380500725U)
					{
						if (codigo == "44")
						{
							return "44-Manutenção mensal";
						}
					}
				}
				else if (codigo == "21")
				{
					return "21-Aguardando Autorização Para Protesto Por Edital";
				}
			}
			else if (num != 2381486463U)
			{
				if (num != 2398264082U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return "22-Protesto Sustado Por Alteração de vencimento e prazo de cartório";
						}
					}
				}
				else if (codigo == "23")
				{
					return "23-Confirmação da Entrada em Cartório";
				}
			}
			else if (codigo == "20")
			{
				return "20-Confirmação de Instrução Para Sustar Protesto";
			}
			return "";
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002FB48 File Offset: 0x0002DD48
		private string RetornarDescricaoDosMotivosDeRejeicoes(string codigosDeRejeicoes)
		{
			string[] array = Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace);
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.Length > 0)
				{
					text = text + this.RetornarMotivoDaRejeicao(text2) + "\n";
				}
			}
			return text;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002FB94 File Offset: 0x0002DD94
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2296612630U)
			{
				if (num <= 485321326U)
				{
					if (num <= 418210850U)
					{
						if (num <= 384361422U)
						{
							if (num != 334175660U)
							{
								if (num != 367583803U)
								{
									if (num == 384361422U)
									{
										if (codigo == "63")
										{
											return "63-Entrada para título já cadastrado";
										}
									}
								}
								else if (codigo == "60")
								{
									return "60-Movimento para título não cadastrado - alteração ou devolução";
								}
							}
							else if (codigo == "18")
							{
								return "18-Vencimento fora do prazo de operação";
							}
						}
						else if (num != 401139041U)
						{
							if (num != 418063755U)
							{
								if (num == 418210850U)
								{
									if (codigo == "09")
									{
										return "09-Nosso número duplicado";
									}
								}
							}
							else if (codigo == "15")
							{
								return "15-Características da cobrança incompatíveis, consulte manual de retorno do banco para maiores informações";
							}
						}
						else if (codigo == "62")
						{
							return "62-Tipo de impressão inválido - Segmento 3S";
						}
					}
					else if (num <= 451618993U)
					{
						if (num != 434841374U)
						{
							if (num != 434988469U)
							{
								if (num == 451618993U)
								{
									if (codigo == "17")
									{
										return "17-Data de vencimento anterior a data de emissão";
									}
								}
							}
							else if (codigo == "08")
							{
								return "08-Nosso Número inválido";
							}
						}
						else if (codigo == "16")
						{
							return "16-Data de vencimento inválida";
						}
					}
					else if (num != 451766088U)
					{
						if (num != 468396612U)
						{
							if (num == 485321326U)
							{
								if (codigo == "05")
								{
									return "05-Código do movimento inválido";
								}
							}
						}
						else if (codigo == "10")
						{
							return "10-Carteira inválida";
						}
					}
					else if (codigo == "07")
					{
						return "07-Título rejeitado na cobrança";
					}
				}
				else if (num <= 2247118416U)
				{
					if (num <= 552431802U)
					{
						if (num != 502098945U)
						{
							if (num != 535654183U)
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
								return "02-Agência/Conta/Número de Controle – Inválido Cobrança Partilhada";
							}
						}
						else if (codigo == "04")
						{
							return "04-Código do movimento não permitido para a carteira";
						}
					}
					else if (num != 2212577440U)
					{
						if (num != 2246132678U)
						{
							if (num == 2247118416U)
							{
								if (codigo == "32")
								{
									return "32-Valor do IOF inválido: Verifica se o campo é numérico / Quando for moeda AA - CUB e carteira 1 - Cobrança Simples, verifica se é menor ou igual a 99999,99";
								}
							}
						}
						else if (codigo == "52")
						{
							return "52-Unidade de Federação inválida ou alteração de UF não permitida";
						}
					}
					else if (codigo == "50")
					{
						return "50-CEP referente a um Banco Correspondente";
					}
				}
				else if (num <= 2263896035U)
				{
					if (num != 2262910297U)
					{
						if (num != 2263057392U)
						{
							if (num == 2263896035U)
							{
								if (codigo == "33")
								{
									return "33-Valor do abatimento inválido";
								}
							}
						}
						else if (codigo == "43")
						{
							return "43-Prazo para baixa/devolução inválido - se o código é '1' verifica se o campo prazo é numérico";
						}
					}
					else if (codigo == "53")
					{
						return "53-Tipo/Número de inscrição do Sacador/Avalista inválido";
					}
				}
				else if (num <= 2279835011U)
				{
					if (num != 2279687916U)
					{
						if (num == 2279835011U)
						{
							if (codigo == "42")
							{
								return "42-Código para baixa/devolução ou instrução inválido - verifica se o código é branco, 0, 1 ou 2";
							}
						}
					}
					else if (codigo == "54")
					{
						return "54-Sacador/Avalista não informado - para espécie AD o nome do Sacador é obrigatório";
					}
				}
				else if (num != 2280673654U)
				{
					if (num == 2296612630U)
					{
						if (codigo == "41")
						{
							return "41-Pedido de cancelamento/sustação de protesto inválido";
						}
					}
				}
				else if (codigo == "30")
				{
					return "30-Desconto a conceder não confere: Instrução de desconto inválida / Taxa ou valor inválido";
				}
			}
			else if (num <= 2363576011U)
			{
				if (num <= 2331006511U)
				{
					if (num <= 2314375987U)
					{
						if (num != 2297598368U)
						{
							if (num != 2313390249U)
							{
								if (num == 2314375987U)
								{
									if (codigo == "24")
									{
										return "24-Data de emissão inválida - verifica se a data é numérica e se está no formato válido";
									}
								}
							}
							else if (codigo == "40")
							{
								return "40-Título com ordem de protesto emitida (para retorno de alteração)";
							}
						}
						else if (codigo == "25")
						{
							return "25-Data de emissão posterior a data de processamento";
						}
					}
					else if (num != 2330020773U)
					{
						if (num != 2330167868U)
						{
							if (num == 2331006511U)
							{
								if (codigo == "37")
								{
									return "37-Código para protesto inválido - rejeita o título se o campo for diferente de branco, 0, 1 ou 3";
								}
							}
						}
						else if (codigo == "47")
						{
							return "47-Endereço não informado ou alteração de endereço não permitida";
						}
					}
					else if (codigo == "57")
					{
						return "57-Código da multa inválido";
					}
				}
				else if (num <= 2346945487U)
				{
					if (num != 2331153606U)
					{
						if (num != 2346798392U)
						{
							if (num == 2346945487U)
							{
								if (codigo == "46")
								{
									return "46-Tipo/número de inscrição do sacado inválido";
								}
							}
						}
						else if (codigo == "58")
						{
							return "58-Data da multa inválida";
						}
					}
					else if (codigo == "27")
					{
						return "27-Valor/taxa de juros de mora inválido";
					}
				}
				else if (num <= 2347931225U)
				{
					if (num != 2347784130U)
					{
						if (num == 2347931225U)
						{
							if (codigo == "26")
							{
								return "26-Código de juros de mora inválido";
							}
						}
					}
					else if (codigo == "34")
					{
						return "34-Valor do abatimento maior ou igual ao valor do título";
					}
				}
				else if (num != 2348916963U)
				{
					if (num == 2363576011U)
					{
						if (codigo == "59")
						{
							return "59-Valor/percentual da multa inválido";
						}
					}
				}
				else if (codigo == "86")
				{
					return "86-Seu número inválido (para retorno de alteração)";
				}
			}
			else if (num <= 2414894606U)
			{
				if (num <= 2380500725U)
				{
					if (num != 2363723106U)
					{
						if (num != 2364708844U)
						{
							if (num == 2380500725U)
							{
								if (codigo == "44")
								{
									return "44-Código da moeda inválido";
								}
							}
						}
						else if (codigo == "21")
						{
							return "21-Espécie do título inválida (arquivo de registro)";
						}
					}
					else if (codigo == "45")
					{
						return "45-Nome do sacado inválido ou alteração do sacado não permitida";
					}
				}
				else if (num != 2381486463U)
				{
					if (num != 2398264082U)
					{
						if (num == 2414894606U)
						{
							if (codigo == "38")
							{
								return "38-Prazo para protesto inválido - se o código for '1' verifica se o campo é numérico";
							}
						}
					}
					else if (codigo == "23")
					{
						return "23-Aceite inválido - verifica conteúdo válido";
					}
				}
				else if (codigo == "20")
				{
					return "20-Valor do título inválido (não numérico)";
				}
			}
			else if (num <= 2447611201U)
			{
				if (num != 2430833582U)
				{
					if (num != 2431672225U)
					{
						if (num == 2447611201U)
						{
							if (codigo == "48")
							{
								return "48-CEP inválido ou alteração de CEP não permitida";
							}
						}
					}
					else if (codigo == "39")
					{
						return "39-Pedido de protesto não permitido para o título - não permite protesto para as carteiras R, S, N e X";
					}
				}
				else if (codigo == "49")
				{
					return "49-CEP sem praça de cobrança ou alteração de cidade não permitida";
				}
			}
			else if (num <= 2498929796U)
			{
				if (num != 2449582677U)
				{
					if (num == 2498929796U)
					{
						if (codigo == "29")
						{
							return "29-Valor do desconto maior ou igual ao valor do título";
						}
					}
				}
				else if (codigo == "80")
				{
					return "80-Data do desconto inválida - valida data ou prazo da instrução de desconto";
				}
			}
			else if (num != 2515707415U)
			{
				if (num == 2565201629U)
				{
					if (codigo == "79")
					{
						return "79-Data de juros de mora inválido - valida data ou prazo na instrução de juros";
					}
				}
			}
			else if (codigo == "28")
			{
				return "28-Código do desconto inválido";
			}
			return "";
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00030438 File Offset: 0x0002E638
		public override DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro)
		{
			DetalheSegmentoURetornoCNAB240 detalheSegmentoURetornoCNAB = base.LerDetalheSegmentoURetornoCNAB240(registro);
			decimal num = Convert.ToInt64(registro.Substring(92, 15));
			detalheSegmentoURetornoCNAB.ValorPagoPeloSacado = num / 100m;
			return detalheSegmentoURetornoCNAB;
		}

		// Token: 0x04000632 RID: 1586
		private string _codigoCarteira;

		// Token: 0x04000633 RID: 1587
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

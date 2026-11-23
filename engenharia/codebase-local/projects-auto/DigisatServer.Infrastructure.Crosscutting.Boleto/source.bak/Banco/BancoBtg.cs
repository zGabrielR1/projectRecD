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
	// Token: 0x0200016C RID: 364
	internal sealed class BancoBtg : AbstractBanco, IBanco
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00059700 File Offset: 0x00057900
		internal BancoBtg()
		{
			this.Nome = "Banco BTG Pactual S.A.";
			this.Codigo = 208;
			this.Digito = "1";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00059750 File Offset: 0x00057950
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00059790 File Offset: 0x00057990
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormatoAgenciaCodigoCedente = string.Format("{0}/", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(4), 4)) + string.Format("{0}-{1}", Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 6), boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture));
				if (Utils.ToString(boleto.NossoNumero) == string.Empty)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 11)
				{
					throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 11 números.");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 11)
				{
					boleto.NossoNumero = Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 11, 11, '0', 0, true, true, true);
				}
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no " + this.Nome;
				}
				boleto.DigitoNossoNumero = this.CalcularDigitoNossoNumero(boleto);
				this.FormataCodigoBarra(boleto);
				this.FormataLinhaDigitavel(boleto);
				this.FormataNossoNumeroImpressao(boleto);
				this.FormataNumeroDocumento(boleto);
				this._codigoCarteira = boleto.Carteira;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00059958 File Offset: 0x00057B58
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return BancoBtg.Mod11Btg(Utils.FormatCode(boleto.Carteira, "0", 2, true) + boleto.NossoNumero, 7);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00059980 File Offset: 0x00057B80
		private static string Mod11Btg(string seq, int b)
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

		// Token: 0x0600072D RID: 1837 RVA: 0x000599EC File Offset: 0x00057BEC
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = string.Format("{0}/{1}-{2}", boleto.Carteira.PreencherComCaractere(Direcao.Esquerda, 2, '0'), boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00059A1C File Offset: 0x00057C1C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			int num = 2;
			int num2 = 0;
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			int moeda = boleto.Moeda;
			string text2 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text3 = string.Concat(new object[]
			{
				text,
				moeda,
				AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture),
				text2,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(4), "0", 4, true),
				Utils.FormatCode(boleto.Carteira, "0", 2, true),
				boleto.NossoNumero,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.CopiarTextoDaDireitaParaEsquerda(7), "0", 7, true),
				"0"
			});
			for (int i = text3.Length - 1; i >= 0; i--)
			{
				num2 += (int)Convert.ToInt16(text3.Substring(i, 1)) * num;
				num++;
				if (num > 9)
				{
					num = 2;
				}
			}
			int num3 = num2 % 11;
			int num4 = 11 - num3;
			if (num4 == 0 || num4 == 1 || num4 > 9)
			{
				num4 = 1;
			}
			this._dacBoleto = num4;
			boleto.CodigoBarra.Codigo = text3.Substring(0, 4) + this._dacBoleto + text3.Substring(4);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00059BBC File Offset: 0x00057DBC
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

		// Token: 0x06000730 RID: 1840 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00059D40 File Offset: 0x00057F40
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text2;
			try
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

		// Token: 0x06000732 RID: 1842 RVA: 0x00059DCC File Offset: 0x00057FCC
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("BANCO BTG PACTUAL S.A.", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + "103" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0005A00C File Offset: 0x0005820C
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0005A024 File Offset: 0x00058224
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

		// Token: 0x06000735 RID: 1845 RVA: 0x0005A0AC File Offset: 0x000582AC
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 40);
				string text3 = new string(' ', 33);
				text4 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "1" + "R" + "01" + "  " + "060" + " " + ((cedente.Cpfcnpj.Length <= 11) ? "1" : "2") + Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + text2 + text2 + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true) + DateTime.Now.ToString("ddMMyyyy") + "00000000" + text3);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0005A2C8 File Offset: 0x000584C8
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

		// Token: 0x06000737 RID: 1847 RVA: 0x00052824 File Offset: 0x00050A24
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("CNAB240 não implementado.");
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0005A34C File Offset: 0x0005854C
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0005A364 File Offset: 0x00058564
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
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += " ";
				text += Utils.FitStringLength(boleto.NossoNumero, 20, 20, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += "1";
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoDistribuicao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text += "00000 ";
				text += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text += boleto.Aceite;
				text += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				if (boleto.JurosMora > 0m)
				{
					text += "1";
					text += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
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
						int num = boleto.Instrucoes[0].Codigo;
						switch (num)
						{
						case 3:
						case 4:
						case 5:
						case 10:
						case 15:
							break;
						case 6:
						case 9:
						case 11:
						case 12:
						case 13:
						case 14:
							goto IL_05CF;
						case 7:
							text += "3";
							text += "00";
							goto IL_0702;
						case 8:
							text += "8";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							goto IL_0702;
						default:
							if (num != 20)
							{
								if (num != 99)
								{
									goto IL_05CF;
								}
								text += "1";
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_0702;
							}
							break;
						}
						text += "2";
						text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						goto IL_0702;
						IL_05CF:
						num = boleto.Instrucoes[1].Codigo;
						if (num <= 15)
						{
							switch (num)
							{
							case 3:
							case 4:
							case 5:
							case 10:
								break;
							case 6:
							case 7:
							case 9:
								goto IL_06EA;
							case 8:
								text += "8";
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_0702;
							default:
								if (num != 15)
								{
									goto IL_06EA;
								}
								break;
							}
						}
						else if (num != 20)
						{
							if (num != 99)
							{
								goto IL_06EA;
							}
							text += "1";
							text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							goto IL_0702;
						}
						text += "2";
						text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						goto IL_0702;
						IL_06EA:
						text += "3";
						text += "00";
					}
					IL_0702:;
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
						text += "   ";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						if (num == 42 || num == 43)
						{
							text += "1";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
						}
						else
						{
							num = boleto.Instrucoes[1].Codigo;
							if (num == 42 || num == 43)
							{
								text += "1";
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
							}
							else
							{
								text += "2";
								text += "   ";
							}
						}
					}
				}
				catch (Exception)
				{
					text += "2";
					text += "   ";
				}
				text += "09";
				text += "0000000000 ";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0005AC2C File Offset: 0x00058E2C
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text4;
			try
			{
				string text = new string('0', 16);
				string text2 = new string(' ', 28);
				string text3 = new string(' ', 40);
				text4 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "Q" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + ((boleto.Sacado.CpfCnpj.Length <= 11) ? "1" : "2") + Utils.FitStringLength(boleto.Sacado.CpfCnpj, 15, 15, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper() + text + text3 + "000" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0005AEE0 File Offset: 0x000590E0
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = new string(' ', 9);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "000000000000000000000000" + "000000000000000000000000" + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + "0000000000000000" + " " + "000000000000" + " " + " " + "0" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0005B080 File Offset: 0x00059280
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
				if (!(text2 == "2"))
				{
					if (!(text2 == "3"))
					{
						if (!(text2 == "4"))
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
						else
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

		// Token: 0x0600073D RID: 1853 RVA: 0x0005B524 File Offset: 0x00059724
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
					text = this.GerarTrailerRemessaCnab400(numeroRegistro);
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

		// Token: 0x0600073E RID: 1854 RVA: 0x0005B5AC File Offset: 0x000597AC
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "99999" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", 6) + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("0", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0005B660 File Offset: 0x00059860
		public string GerarTrailerRemessaCnab400(int numeroRegistro)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0005B678 File Offset: 0x00059878
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

		// Token: 0x06000741 RID: 1857 RVA: 0x0005370C File Offset: 0x0005190C
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = base.LerDetalheSegmentoTRetornoCNAB240(registro);
			detalheSegmentoTRetornoCNAB.NossoNumero = registro.Substring(37, 20);
			return detalheSegmentoTRetornoCNAB;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0005B730 File Offset: 0x00059930
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
				detalheRetornoCNAB.NossoNumero = Utils.ToString(registro.Substring(66, 7));
				detalheRetornoCNAB.DACNossoNumero = Utils.ToInt32(registro.Substring(73, 1));
				decimal num2 = Convert.ToUInt64(registro.Substring(181, 7));
				detalheRetornoCNAB.TarifaCobranca = num2 / 100m;
				decimal num3 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num4 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(Utils.ToInt32(registro.Substring(175, 6)).ToString("##-##-##"));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				switch (codigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_01C5;
				case 3:
					break;
				case 4:
				case 7:
				case 8:
					goto IL_01BE;
				case 5:
				case 6:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_01C5;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					goto IL_01C5;
				default:
					if (codigoOcorrencia != 26 && codigoOcorrencia != 30)
					{
						goto IL_01BE;
					}
					break;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
				detalheRetornoCNAB.MotivosRejeicao = this.RetornarMotivoDaRejeicao("0");
				goto IL_01C5;
				IL_01BE:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_01C5:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0005B934 File Offset: 0x00059B34
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 502098945U)
			{
				if (num <= 418063755U)
				{
					if (num <= 335455588U)
					{
						if (num != 167679398U)
						{
							if (num != 184457017U)
							{
								if (num == 335455588U)
								{
									if (codigo == "98")
									{
										return "98-Despesas de custas antecipadas";
									}
								}
							}
							else if (codigo == "97")
							{
								return "97-Despesas de sustação de protesto";
							}
						}
						else if (codigo == "96")
						{
							return "96-Despesas de protesto";
						}
					}
					else if (num != 350953279U)
					{
						if (num != 401286136U)
						{
							if (num == 418063755U)
							{
								if (codigo == "15")
								{
									return "15-Liquidação em cartório";
								}
							}
						}
						else if (codigo == "14")
						{
							return "14-Alteração de vencimento";
						}
					}
					else if (codigo == "19")
					{
						return "19-Confirmação instrução protesto";
					}
				}
				else if (num <= 468543707U)
				{
					if (num != 418210850U)
					{
						if (num != 468396612U)
						{
							if (num == 468543707U)
							{
								if (codigo == "06")
								{
									return "06-Liquidação normal";
								}
							}
						}
						else if (codigo == "10")
						{
							return "10-Baixa solicitada";
						}
					}
					else if (codigo == "09")
					{
						return "09-Baixa de título";
					}
				}
				else if (num <= 485321326U)
				{
					if (num != 485174231U)
					{
						if (num == 485321326U)
						{
							if (codigo == "05")
							{
								return "05-Liquidação sem registro";
							}
						}
					}
					else if (codigo == "11")
					{
						return "11-Títulos em ser";
					}
				}
				else if (num != 501951850U)
				{
					if (num == 502098945U)
					{
						if (codigo == "04")
						{
							return "04-Transferência de carteira - entrada";
						}
					}
				}
				else if (codigo == "12")
				{
					return "12-Abatimento concedido";
				}
			}
			else if (num <= 2314375987U)
			{
				if (num <= 535654183U)
				{
					if (num != 518729469U)
					{
						if (num != 518876564U)
						{
							if (num == 535654183U)
							{
								if (codigo == "02")
								{
									return "02-Confirmação entrada título";
								}
							}
						}
						else if (codigo == "03")
						{
							return "03-Comando recusado";
						}
					}
					else if (codigo == "13")
					{
						return "13-Abatimento cancelado";
					}
				}
				else if (num != 2280673654U)
				{
					if (num != 2297598368U)
					{
						if (num == 2314375987U)
						{
							if (codigo == "24")
							{
								return "24-Sustar protesto";
							}
						}
					}
					else if (codigo == "25")
					{
						return "25-Dispensar juros";
					}
				}
				else if (codigo == "30")
				{
					return "30-Alteração dados rejeitada";
				}
			}
			else if (num <= 2364708844U)
			{
				if (num != 2331153606U)
				{
					if (num != 2347931225U)
					{
						if (num == 2364708844U)
						{
							if (codigo == "21")
							{
								return "21-Alteração de nome do sacado";
							}
						}
					}
					else if (codigo == "26")
					{
						return "26-Instrução rejeitada";
					}
				}
				else if (codigo == "27")
				{
					return "27-Confirmação alteração dos dados";
				}
			}
			else if (num <= 2398264082U)
			{
				if (num != 2381486463U)
				{
					if (num == 2398264082U)
					{
						if (codigo == "23")
						{
							return "23-Encaminhado a protesto";
						}
					}
				}
				else if (codigo == "20")
				{
					return "20-Débito em conta";
				}
			}
			else if (num != 2415041701U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return "28-Manutenção título vencido";
					}
				}
			}
			else if (codigo == "22")
			{
				return "22-Alteração de endereço do sacado";
			}
			return "";
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00053BE5 File Offset: 0x00051DE5
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			return "Não informado no arquivo de retorno";
		}

		// Token: 0x04000654 RID: 1620
		private int _dacBoleto;

		// Token: 0x04000655 RID: 1621
		private string _codigoCarteira;

		// Token: 0x04000656 RID: 1622
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

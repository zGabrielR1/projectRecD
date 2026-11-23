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
	// Token: 0x02000172 RID: 370
	internal sealed class BancoSantander : AbstractBanco, IBanco
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00066DA0 File Offset: 0x00064FA0
		internal BancoSantander()
		{
			this.Codigo = 33;
			this.Digito = "7";
			this.Nome = "Santander";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00066DEC File Offset: 0x00064FEC
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00066E2C File Offset: 0x0006502C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}{1}/{2}{3}", new object[]
			{
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 9),
				boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture)
			});
			if (string.IsNullOrEmpty(boleto.Carteira))
			{
				throw new NotImplementedException("Carteira não informada. Utilize a carteira 101, 102 ou 201.");
			}
			if (!(boleto.Carteira == "102") && !(boleto.Carteira == "101") && !(boleto.Carteira == "201"))
			{
				throw new NotImplementedException("Carteira não implementada. Utilize a carteira 101, 102 ou 201.");
			}
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			BancoSantander.FormatarNossoNumero(boleto);
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Grupo Santander - GC";
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00066F70 File Offset: 0x00065170
		private static void FormatarNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 12);
			boleto.DigitoNossoNumero = BancoSantander.Mod11Santander(Utils.FormatCode(boleto.NossoNumero, 12)).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00066FB8 File Offset: 0x000651B8
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
			string text3 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
			string text4 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text5 = Utils.FormatCode(boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente, 7);
			string text6 = Utils.FormatCode(boleto.NossoNumero + boleto.DigitoNossoNumero, 13);
			string text7 = Utils.FormatCode(boleto.Carteira, 3);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", new object[] { text, text2, text3, text4, "9", text5, text6, "0", text7 });
			string text8 = BancoSantander.Mod10Mod11Santander(boleto.CodigoBarra.Codigo).ToString(CultureInfo.InvariantCulture);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", new object[] { text, text2, text8, text3, text4, "9", text5, text6, "0", text7 });
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00067150 File Offset: 0x00065350
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
			string text3 = Strings.Mid(boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente, 1, 4);
			string text4 = AbstractBanco.Mod10(string.Format("{0}{1}{2}{3}", new object[] { text, text2, "9", text3 })).ToString(CultureInfo.InvariantCulture);
			string text5 = string.Format("{0}{1}{2}.{3}{4}", new object[] { text, text2, "9", text3, text4 });
			string text6 = Strings.Mid(boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente, 5, 3);
			string text7 = Strings.Mid(boleto.NossoNumero, 1, 7);
			string text8 = AbstractBanco.Mod10(string.Format("{0}{1}", text6, text7)).ToString(CultureInfo.InvariantCulture);
			string text9 = string.Format("{0}{1}{2}.{3}{4}", new object[]
			{
				" ",
				text6,
				Strings.Mid(text7, 1, 2),
				Strings.Mid(text7, 3),
				text8
			});
			string text10 = Strings.Mid(boleto.NossoNumero, 8, 5);
			string text11 = Utils.FormatCode(boleto.Carteira, 3);
			string text12 = AbstractBanco.Mod10(string.Format("{0}{1}{2}{3}", new object[] { text10, boleto.DigitoNossoNumero, "0", text11 })).ToString(CultureInfo.InvariantCulture);
			string text13 = string.Format("{0}{1}.{2}{3}{4}{5}", new object[] { " ", text10, boleto.DigitoNossoNumero, "0", text11, text12 });
			string text14 = Strings.Mid(boleto.CodigoBarra.Codigo, 5, 1);
			string text15 = string.Format("{0}{1}", " ", text14);
			string text16 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
			string text17 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text18 = string.Format("{0}{1}{2}", " ", text16, text17);
			boleto.CodigoBarra.LinhaDigitavel = string.Format("{0}{1}{2}{3}{4}", new object[] { text5, text9, text13, text15, text18 });
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00067408 File Offset: 0x00065608
		private static int Mod11Santander(string seq)
		{
			int num = 0;
			int num2 = 5;
			while (seq.Length > 0)
			{
				int num3 = Convert.ToInt32(seq.Substring(0, 1));
				num += num3 * num2;
				num2--;
				if (num2 == 1)
				{
					num2 = 9;
				}
				seq = seq.Remove(0, 1);
			}
			int num4 = num - num / 11 * 11;
			int num5;
			if (num4 != 0 && num4 != 1)
			{
				if (num4 != 10)
				{
					num5 = 11 - num4;
				}
				else
				{
					num5 = 1;
				}
			}
			else
			{
				num5 = 0;
			}
			return num5;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00067478 File Offset: 0x00065678
		private static int Mod10Mod11Santander(string seq)
		{
			int num = 0;
			int num2 = 2;
			char[] array = seq.ToCharArray();
			Array.Reverse(array);
			string text = new string(array);
			while (text.Length > 0)
			{
				int num3 = Convert.ToInt32(text.Substring(0, 1));
				num += num3 * num2;
				num2++;
				if (num2 == 10)
				{
					num2 = 2;
				}
				text = text.Remove(0, 1);
			}
			int num4 = num * 10 % 11;
			int num5;
			if (num4 == 0 || num4 == 1 || num4 == 10)
			{
				num5 = 1;
			}
			else
			{
				num5 = num4;
			}
			return num5;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000674F0 File Offset: 0x000656F0
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}{1}{2}", boleto.NossoNumero, " ", boleto.DigitoNossoNumero);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00067514 File Offset: 0x00065714
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
					text = this.GerarHeaderRemessaCnab240(cedente);
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderRemessaCnab400();
					break;
				case TipoArquivo.Outro:
					throw new Exception("Tipo de arquivo inexistente.");
				}
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0006758C File Offset: 0x0006578C
		public string GerarHeaderRemessaCnab240(Cedente cedente)
		{
			string text2;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text += "0000";
				text += "0";
				text += Utils.FormatCode("", " ", 8);
				if (cedente.Cpfcnpj.Length <= 11)
				{
					text += "1";
				}
				else
				{
					text += "2";
				}
				text += Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true);
				text += Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 15, 15, '0', 0, true, true, true);
				text += Utils.FormatCode("", " ", 25);
				text += Utils.FitStringLength(cedente.Nome.ToUpper(), 30, 30, ' ', 0, true, true, false);
				text += Utils.FormatCode("BANCO SANTANDER", " ", 30);
				text += Utils.FormatCode("", " ", 10);
				text += "1";
				text += DateTime.Now.ToString("ddMMyyyy");
				text += Utils.FormatCode("", " ", 6);
				text += "000001";
				text += "040";
				text += Utils.FormatCode("", " ", 74);
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00058384 File Offset: 0x00056584
		public string GerarHeaderRemessaCnab400()
		{
			throw new NotImplementedException("Funçao não implementada!");
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00067760 File Offset: 0x00065960
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
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
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DO LOTE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000677CC File Offset: 0x000659CC
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text += "0001";
				text += "1";
				text += "R";
				text += "01";
				text += "  ";
				text += "030";
				text += " ";
				if (cedente.Cpfcnpj.Length <= 11)
				{
					text += "1";
				}
				else
				{
					text += "2";
				}
				text += Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true);
				text += Utils.FormatCode("", " ", 20);
				text += Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 15, 15, '0', 0, true, true, true);
				text += Utils.FormatCode("", " ", 5);
				text += Utils.FitStringLength(cedente.Nome.ToUpper(), 30, 30, ' ', 0, true, true, false);
				text += Utils.FormatCode("", " ", 40);
				text += Utils.FormatCode("", " ", 40);
				text += Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true);
				text += DateTime.Now.ToString("ddMMyyyy");
				text += Utils.FormatCode("", " ", 41);
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER DO LOTE do arquivo de remessa.", ex);
			}
			return text2;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000679C0 File Offset: 0x00065BC0
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
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab400()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00067A34 File Offset: 0x00065C34
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text3;
			try
			{
				string text = new string(' ', 11);
				string text2 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text2 += "0001";
				text2 += "3";
				text2 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text2 += "P";
				text2 += " ";
				text2 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, 5, 5, '0', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 9, 9, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 9, 9, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text2 += "  ";
				text2 += Utils.FitStringLength(boleto.NossoNumero + boleto.DigitoNossoNumero, 13, 13, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.TipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text2 += "1";
				text2 += "  ";
				text2 += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text2 += "00000 ";
				text2 += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento), 2, 2, '0', 0, true, true, true);
				text2 += boleto.Aceite;
				text2 += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
				if (boleto.JurosMora > 0m)
				{
					if (boleto.DataVencimento.Date.Equals(boleto.DataJurosMora.Date))
					{
						text2 += "1";
					}
					else
					{
						text2 += "5";
					}
					text2 += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text2 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				}
				else
				{
					text2 += "3";
					text2 += "00000000";
					text2 += "000000000000000";
				}
				if (boleto.ValorDesconto > 0m)
				{
					text2 += "1";
					text2 += Utils.FitStringLength(boleto.DataDesconto.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
					text2 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				}
				else
				{
					text2 += "0";
					text2 += "00000000";
					text2 += "000000000000000";
				}
				text2 += "000000000000000";
				text2 += "000000000000000";
				text2 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text2 += "0";
						text2 += "00";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						switch (num)
						{
						case 0:
							text2 += "0";
							text2 += "00";
							goto IL_06C5;
						case 1:
						case 2:
							text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
							text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							goto IL_06C5;
						case 3:
							break;
						default:
							if (num != 9)
							{
								num = boleto.Instrucoes[1].Codigo;
								switch (num)
								{
								case 1:
								case 2:
									text2 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
									text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									goto IL_06C5;
								case 3:
									break;
								default:
									if (num != 9)
									{
										text2 += "0";
										text2 += "00";
										goto IL_06C5;
									}
									break;
								}
								text2 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								text2 += "00";
								goto IL_06C5;
							}
							break;
						}
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
						text2 += "00";
					}
					IL_06C5:;
				}
				catch (Exception)
				{
					text2 += "0";
					text2 += "00";
				}
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text2 += "2";
						text2 += "000";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						if (num != 11)
						{
							if (num != 12)
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num != 0)
								{
									if (num != 11)
									{
										if (num != 12)
										{
											text2 += "2";
											text2 += "000";
										}
										else
										{
											text2 += Utils.FitStringLength(Strings.Mid(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1), 1, 1, '0', 0, true, true, true);
											text2 += "000";
										}
									}
									else
									{
										text2 += Utils.FitStringLength(Strings.Mid(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1), 1, 1, '0', 0, true, true, true);
										text2 += "0";
										text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
								}
								else
								{
									text2 += "2";
									text2 += "000";
								}
							}
							else
							{
								text2 += Utils.FitStringLength(Strings.Mid(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1), 1, 1, '0', 0, true, true, true);
								text2 += "000";
							}
						}
						else
						{
							text2 += Utils.FitStringLength(Strings.Mid(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1), 1, 1, '0', 0, true, true, true);
							text2 += "0";
							text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						}
					}
				}
				catch (Exception)
				{
					text2 += "2";
					text2 += "000";
				}
				text2 += "00";
				text2 += text;
				text2 = Utils.SubstituiCaracteresEspeciais(text2);
				text3 = text2;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000683F0 File Offset: 0x000665F0
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text6;
			try
			{
				string text = new string('0', 16);
				string text2 = new string(' ', 19);
				string text3 = new string(' ', 40);
				string text4 = new string('0', 9);
				string text5 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text5 += "0001";
				text5 += "3";
				text5 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text5 += "Q";
				text5 += " ";
				text5 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text5 += "1";
				}
				else
				{
					text5 += "2";
				}
				text5 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 15, 15, '0', 0, true, true, true);
				text5 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text5 += text;
				text5 += text3;
				text5 += "000";
				text5 += text4;
				text5 += text2;
				text5 = Utils.SubstituiCaracteresEspeciais(text5);
				text6 = text5;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text6;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00068718 File Offset: 0x00066918
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 24);
				string text2 = new string(' ', 151);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "0" + "00000000" + "000000000000000" + text + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00068890 File Offset: 0x00066A90
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
					text = this.GerarTrailerRemessaCnab400();
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

		// Token: 0x060007CD RID: 1997 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab400()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00068904 File Offset: 0x00066B04
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text2;
			try
			{
				string text = new string(' ', 217);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do LOTE de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000689B4 File Offset: 0x00066BB4
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "9999" + "9" + Utils.FormatCode("", " ", 9) + "000001" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FormatCode("", " ", 211));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do ARQUIVO de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00068A74 File Offset: 0x00066C74
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB2;
			try
			{
				if (!registro.Substring(13, 1).Equals("T"))
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características do segmento T.");
				}
				DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = new DetalheSegmentoTRetornoCNAB240(registro)
				{
					CodigoBanco = Convert.ToInt32(registro.Substring(0, 3)),
					IdCodigoMovimento = Convert.ToInt32(registro.Substring(15, 2)),
					Agencia = Convert.ToInt32(registro.Substring(17, 4)),
					DigitoAgencia = registro.Substring(21, 1),
					Conta = Convert.ToInt64(registro.Substring(22, 9)),
					DigitoConta = registro.Substring(31, 1),
					NossoNumero = registro.Substring(40, 12),
					CodigoCarteira = Convert.ToInt32(registro.Substring(53, 1)),
					NumeroDocumento = registro.Substring(54, 15),
					DataVencimento = DateTime.ParseExact(registro.Substring(69, 8), "ddMMyyyy", CultureInfo.InvariantCulture),
					ValorTitulo = Convert.ToDecimal(registro.Substring(77, 15)) / 100m,
					UsoEmpresa = registro.Substring(100, 25),
					TipoInscricao = Convert.ToInt32(registro.Substring(127, 1)),
					NumeroInscricao = registro.Substring(128, 15),
					NomeSacado = registro.Substring(143, 40),
					ValorTarifas = Convert.ToDecimal(registro.Substring(193, 15)) / 100m,
					CodigoRejeicao = registro.Substring(208, 10)
				};
				int idCodigoMovimento = detalheSegmentoTRetornoCNAB.IdCodigoMovimento;
				switch (idCodigoMovimento)
				{
				case 2:
					detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_01F5;
				case 3:
					break;
				case 4:
				case 5:
					goto IL_01EE;
				case 6:
					detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_01F5;
				default:
					if (idCodigoMovimento == 9)
					{
						detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
						goto IL_01F5;
					}
					if (idCodigoMovimento != 26)
					{
						goto IL_01EE;
					}
					break;
				}
				detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
				detalheSegmentoTRetornoCNAB.MotivosRejeicao = detalheSegmentoTRetornoCNAB.RetornarMotivosDasRejeicoes(detalheSegmentoTRetornoCNAB.CodigoRejeicao);
				goto IL_01F5;
				IL_01EE:
				detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_01F5:
				detalheSegmentoTRetornoCNAB2 = detalheSegmentoTRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO T.", ex);
			}
			return detalheSegmentoTRetornoCNAB2;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00068CA4 File Offset: 0x00066EA4
		public override DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro)
		{
			DetalheSegmentoURetornoCNAB240 detalheSegmentoURetornoCNAB2;
			try
			{
				if (!registro.Substring(13, 1).Equals("U"))
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características do segmento U.");
				}
				DetalheSegmentoURetornoCNAB240 detalheSegmentoURetornoCNAB = new DetalheSegmentoURetornoCNAB240(registro);
				detalheSegmentoURetornoCNAB.JurosMultaEncargos = Convert.ToDecimal(registro.Substring(17, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorDescontoConcedido = Convert.ToDecimal(registro.Substring(32, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorAbatimentoConcedido = Convert.ToDecimal(registro.Substring(47, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorIOFRecolhido = Convert.ToDecimal(registro.Substring(62, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorOcorrenciaSacado = (detalheSegmentoURetornoCNAB.ValorPagoPeloSacado = Convert.ToDecimal(registro.Substring(77, 15)) / 100m);
				detalheSegmentoURetornoCNAB.ValorLiquidoASerCreditado = Convert.ToDecimal(registro.Substring(92, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorOutrasDespesas = Convert.ToDecimal(registro.Substring(107, 15)) / 100m;
				detalheSegmentoURetornoCNAB.ValorOutrosCreditos = Convert.ToDecimal(registro.Substring(122, 15)) / 100m;
				detalheSegmentoURetornoCNAB.DataOcorrencia = (detalheSegmentoURetornoCNAB.DataOcorrencia = DateTime.ParseExact(registro.Substring(137, 8), "ddMMyyyy", CultureInfo.InvariantCulture));
				detalheSegmentoURetornoCNAB.DataCredito = DateTime.ParseExact(registro.Substring(145, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
				detalheSegmentoURetornoCNAB.CodigoOcorrenciaSacado = registro.Substring(153, 4);
				detalheSegmentoURetornoCNAB2 = detalheSegmentoURetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO U.", ex);
			}
			return detalheSegmentoURetornoCNAB2;
		}

		// Token: 0x04000666 RID: 1638
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

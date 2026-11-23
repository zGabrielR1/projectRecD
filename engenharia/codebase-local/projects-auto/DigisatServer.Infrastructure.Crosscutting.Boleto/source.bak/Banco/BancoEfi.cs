using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200015C RID: 348
	internal sealed class BancoEfi : AbstractBanco, IBanco
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x000338A8 File Offset: 0x00031AA8
		internal BancoEfi()
		{
			this.Codigo = 364;
			this.Digito = "0";
			this.Nome = "EFÍ S.A";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00033904 File Offset: 0x00031B04
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00009047 File Offset: 0x00007247
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00033941 File Offset: 0x00031B41
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00033954 File Offset: 0x00031B54
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
				{
					TipoSolucao tipoSolucao = cedente.TipoSolucao;
					if (tipoSolucao != TipoSolucao.Sigcb)
					{
						if (tipoSolucao == TipoSolucao.Sicob)
						{
							text = this.GerarHeaderRemessaCnab240Sicob(cedente);
						}
					}
					else
					{
						text = this.GerarHeaderRemessaCnab240Sigcb(cedente, numeroArquivoRemessa);
					}
					break;
				}
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderRemessaCnab400(0, cedente, numeroArquivoRemessa);
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

		// Token: 0x06000596 RID: 1430 RVA: 0x000339F0 File Offset: 0x00031BF0
		public string GerarHeaderRemessaCnab240Sigcb(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 9);
				string text2 = new string(' ', 10);
				string text3 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text3 += "0000";
				text3 += "0";
				text3 += text;
				text3 += ((cedente.Cpfcnpj.Length == 11) ? "1" : "2");
				text3 += Utils.FitStringLength(cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text3 += Utils.FormatCode("0", 20);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
				text3 += (((cedente.Codigo + cedente.DigitoCedente).Length == 6) ? Utils.FormatCode(string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente), "0", 7, false) : Utils.FitStringLength(string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente), 7, 7, '0', 0, true, true, false));
				text3 += Utils.FormatCode("0", 7);
				text3 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength("CAIXA ECONOMICA FEDERAL", 30, 30, ' ', 0, true, true, false);
				text3 += text2;
				text3 += "1";
				text3 += DateTime.Now.ToString("ddMMyyyy");
				text3 += string.Format("{0:hh:mm:ss}", DateTime.Now).Replace(":", "");
				text3 += Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true);
				text3 += (((cedente.Codigo + cedente.DigitoCedente).Length == 6) ? "101" : "107");
				text3 += "00000";
				text3 += Utils.FormatCode(" ", " ", 20);
				if (cedente.FinalidadeArquivoRemessaRetorno == FinalidadeArquivoRemessaRetorno.Teste)
				{
					text3 += "REMESSA-TESTE       ";
				}
				else
				{
					text3 += "REMESSA-PRODUÇÃO    ";
				}
				text3 += Utils.FormatCode(" ", " ", 29);
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text4;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00033CE0 File Offset: 0x00031EE0
		public string GerarHeaderRemessaCnab240Sicob(Cedente cedente)
		{
			string text4;
			try
			{
				string text = new string(' ', 9);
				string text2 = new string(' ', 10);
				string text3 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text3 += "0000";
				text3 += "0";
				text3 += text;
				text3 += ((cedente.Cpfcnpj.Length == 11) ? "1" : "2");
				text3 += Utils.FitStringLength(cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.OperacaoConta, 3, 3, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true), 16, 16, '0', 0, true, true, true);
				text3 += "    ";
				text3 += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
				text3 += Utils.FitStringLength(cedente.Codigo.ToString(CultureInfo.InvariantCulture), 12, 12, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text3 += AbstractBanco.Mod11(Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true));
				text3 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength("CAIXA ECONOMICA FEDERAL", 30, 30, ' ', 0, true, true, false);
				text3 += text2;
				text3 += "1";
				text3 += DateTime.Now.ToString("ddMMyyyy");
				text3 += string.Format("{0:hh:mm:ss}", DateTime.Now).Replace(":", "");
				text3 += "000001";
				text3 += "030";
				text3 += "00000";
				text3 += Utils.FormatCode(" ", " ", 20);
				if (cedente.FinalidadeArquivoRemessaRetorno == FinalidadeArquivoRemessaRetorno.Teste)
				{
					text3 += "REMESSA-TESTE       ";
				}
				else
				{
					text3 += "REMESSA-PRODUÇÃO    ";
				}
				text3 += Utils.FormatCode(" ", " ", 29);
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text4;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0003401C File Offset: 0x0003221C
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00034028 File Offset: 0x00032228
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
				{
					TipoSolucao tipoSolucao = cedente.TipoSolucao;
					if (tipoSolucao != TipoSolucao.Sigcb)
					{
						if (tipoSolucao == TipoSolucao.Sicob)
						{
							text = this.GerarHeaderLoteRemessaCnab240Sicob(cedente, numeroArquivoRemessa);
						}
					}
					else
					{
						text = this.GerarHeaderLoteRemessaCnab240Sigcb(cedente, numeroArquivoRemessa);
					}
					break;
				}
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderLoteRemessaCnab400();
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

		// Token: 0x0600059A RID: 1434 RVA: 0x000340B0 File Offset: 0x000322B0
		private string GerarHeaderLoteRemessaCnab240Sigcb(Cedente cedente, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "1" + "R" + "01" + "00" + (((cedente.Codigo + cedente.DigitoCedente).Length == 6) ? "060" : "067") + " " + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 15, true) + (((cedente.Codigo + cedente.DigitoCedente).Length == 6) ? Utils.FormatCode(string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente), "0", 7, false) : Utils.FitStringLength(string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente), 7, 7, '0', 0, true, true, false)) + Utils.FormatCode("0", 13) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + (((cedente.Codigo + cedente.DigitoCedente).Length == 6) ? Utils.FormatCode(string.Format("{0}{1}", cedente.Codigo, cedente.DigitoCedente), "0", 6, false) : Utils.FormatCode("", "0", 6)) + Utils.FormatCode("", "0", 7) + Utils.FormatCode("0", 1) + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("", " ", 40) + Utils.FormatCode("", " ", 40) + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 8, 8, '0', 0, true, true, true) + DateTime.Now.ToString("ddMMyyyy") + Utils.FormatCode("", "0", 8) + Utils.FormatCode("", " ", 33));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER DO LOTE do arquivo de remessa.", ex);
			}
			return text;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00034398 File Offset: 0x00032598
		private string GerarHeaderLoteRemessaCnab240Sicob(Cedente cedente, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "1" + "R" + "01" + "00" + "020" + " " + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 15, true) + Utils.FitStringLength(Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.OperacaoConta, 3, 3, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true), 16, 16, '0', 0, true, true, true) + "    " + Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false) + Utils.FitStringLength(cedente.Codigo.ToString(CultureInfo.InvariantCulture), 12, 12, '0', 0, true, true, true) + Utils.FitStringLength(cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true) + AbstractBanco.Mod11(Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true)) + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("", " ", 40) + Utils.FormatCode("", " ", 40) + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 8, 8, '0', 0, true, true, true) + DateTime.Now.ToString("ddMMyyyy") + Utils.FormatCode("", "0", 8) + Utils.FormatCode("", " ", 33));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER DO LOTE do arquivo de remessa.", ex);
			}
			return text;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0003401C File Offset: 0x0003221C
		private string GerarHeaderLoteRemessaCnab400()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00034664 File Offset: 0x00032864
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text = " ";
			TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
			if (tipoSolucao != TipoSolucao.Sigcb)
			{
				if (tipoSolucao == TipoSolucao.Sicob)
				{
					text = this.GerarDetalheSegmentoPRemessaCnab240Sicob(boleto, numeroRegistro, numeroConvenio);
				}
			}
			else
			{
				text = this.GerarDetalheSegmentoPRemessaCnab240Sigcb(boleto, numeroRegistro, numeroConvenio);
			}
			return text;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000346A4 File Offset: 0x000328A4
		public string GerarDetalheSegmentoPRemessaCnab240Sigcb(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text3;
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text += "0001";
				text += "3";
				text += Utils.FormatCode(numeroRegistro.ToString(CultureInfo.InvariantCulture), "0", 5, true);
				text += "P";
				text += " ";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 5, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 1, true);
				text += (((boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente).Length == 6) ? Utils.FormatCode(string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente), "0", 7, false) : Utils.FitStringLength(string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente), 7, 7, '0', 0, true, true, false));
				text += Utils.FormatCode("0", 10);
				text += Utils.FormatCode(boleto.NossoNumero, 17);
				string text2 = text;
				ITipoCobranca tipoCobranca = boleto.TipoCobranca;
				text = text2 + Utils.FormatCode(((tipoCobranca != null) ? tipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture) : null) ?? "0", "0", 1, true);
				text += Utils.FormatCode(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), "0", 1, true);
				text += "2";
				text += Utils.FormatCode(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), "0", 1, true);
				int num = boleto.TipoDistribuicao.Codigo;
				if (num != 1)
				{
					if (num == 2)
					{
						text += "0";
					}
				}
				else
				{
					text += "2";
				}
				text += Utils.FitStringLength(boleto.NumeroDocumento, 11, 11, ' ', 0, true, true, false);
				text += "    ";
				text += boleto.DataVencimento.ToString("ddMMyyyy");
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text += "00000";
				text += "0";
				text += Utils.FormatCode(boleto.EspecieDocumento.Codigo.ToString(CultureInfo.InvariantCulture), "0", 2, true);
				text += boleto.Aceite;
				text += ((boleto.DataDocumento.ToString("ddMMyyyy") == "01010001") ? DateTime.Now.ToString("ddMMyyyy") : boleto.DataProcessamento.ToString("ddMMyyyy"));
				if (boleto.JurosMora > 0m)
				{
					if (boleto.JurosMora.ArredondarParaDuasCasasDecimais() > 0m)
					{
						text += "1";
						text += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
						text += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
					}
					else
					{
						text += "2";
						text += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
						text += Utils.FitStringLength(boleto.PercJurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
					}
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
				text += Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
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
							text += "1";
							if (boleto.Instrucoes[0].QuantidadeDias >= 6 && boleto.Instrucoes[0].QuantidadeDias <= 90)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "06";
							}
							break;
						case 2:
							text += "1";
							if (boleto.Instrucoes[0].QuantidadeDias >= 2 && boleto.Instrucoes[0].QuantidadeDias <= 5)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "02";
							}
							break;
						case 3:
							text += "3";
							text += "00";
							break;
						default:
							num = boleto.Instrucoes[1].Codigo;
							if (num != 1)
							{
								if (num != 2)
								{
									text += "3";
									text += "00";
								}
								else
								{
									text += "1";
									if (boleto.Instrucoes[1].QuantidadeDias >= 2 && boleto.Instrucoes[1].QuantidadeDias <= 5)
									{
										text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
									else
									{
										text += "02";
									}
								}
							}
							else
							{
								text += "1";
								if (boleto.Instrucoes[1].QuantidadeDias >= 6 && boleto.Instrucoes[1].QuantidadeDias <= 90)
								{
									text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								}
								else
								{
									text += "06";
								}
							}
							break;
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
						num = boleto.Instrucoes[0].Codigo;
						if (num != 4)
						{
							if (num != 5)
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num == 4)
								{
									text += "1";
									TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
									if (tipoSolucao != TipoSolucao.Sigcb)
									{
										if (tipoSolucao == TipoSolucao.Sicob)
										{
											if (boleto.Instrucoes[1].QuantidadeDias >= 5 && boleto.Instrucoes[1].QuantidadeDias <= 90)
											{
												text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
											}
											else
											{
												text += "90";
											}
										}
									}
									else if (boleto.Instrucoes[1].QuantidadeDias >= 5 && boleto.Instrucoes[1].QuantidadeDias <= 120)
									{
										text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
									}
									else
									{
										text += "120";
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
								text += "2";
								text += "000";
							}
						}
						else
						{
							text += "1";
							TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
							if (tipoSolucao != TipoSolucao.Sigcb)
							{
								if (tipoSolucao == TipoSolucao.Sicob)
								{
									if (boleto.Instrucoes[0].QuantidadeDias >= 5 && boleto.Instrucoes[0].QuantidadeDias <= 90)
									{
										text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
									}
									else
									{
										text += "90";
									}
								}
							}
							else if (boleto.Instrucoes[0].QuantidadeDias >= 5 && boleto.Instrucoes[0].QuantidadeDias <= 120)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
							}
							else
							{
								text += "120";
							}
						}
					}
				}
				catch (Exception)
				{
					text += "2";
					text += "000";
				}
				text += Utils.FormatCode(boleto.Moeda.ToString(CultureInfo.InvariantCulture), "0", 2, true);
				text += Utils.FormatCode("0", 10);
				text += " ";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text3 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar SEGMENTO P do arquivo de remessa.", ex);
			}
			return text3;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000352C0 File Offset: 0x000334C0
		public string GerarDetalheSegmentoPRemessaCnab240Sicob(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text4;
			try
			{
				string text = new string(' ', 9);
				string text2 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true);
				text2 += "0001";
				text2 += "3";
				text2 += Utils.FormatCode(numeroRegistro.ToString(CultureInfo.InvariantCulture), "0", 5, true);
				text2 += "P";
				text2 += " ";
				text2 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 12, 12, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text2 += AbstractBanco.Mod11(Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true));
				text2 += text;
				text2 += Utils.FormatCode(boleto.NossoNumero + boleto.DigitoNossoNumero, 11);
				string text3 = text2;
				ITipoCobranca tipoCobranca = boleto.TipoCobranca;
				text2 = text3 + Utils.FormatCode(((tipoCobranca != null) ? tipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture) : null) ?? "0", "0", 1, true);
				text2 += Utils.FormatCode(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), "0", 1, true);
				text2 += "2";
				text2 += Utils.FormatCode(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), "0", 1, true);
				text2 += Utils.FormatCode(boleto.TipoDistribuicao.Codigo.ToString(CultureInfo.InvariantCulture), "0", 1, true);
				text2 += Utils.FitStringLength(boleto.NumeroDocumento, 11, 11, ' ', 0, true, true, false);
				text2 += "    ";
				text2 += boleto.DataVencimento.ToString("ddMMyyyy");
				text2 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text2 += "00000";
				text2 += "0";
				text2 += Utils.FormatCode(boleto.EspecieDocumento.Codigo.ToString(CultureInfo.InvariantCulture), "0", 2, true);
				text2 += boleto.Aceite;
				text2 += ((boleto.DataDocumento.ToString("ddMMyyyy") == "01010001") ? DateTime.Now.ToString("ddMMyyyy") : boleto.DataProcessamento.ToString("ddMMyyyy"));
				if (boleto.JurosMora > 0m)
				{
					if (boleto.JurosMora.ArredondarParaDuasCasasDecimais() > 0m)
					{
						text2 += "1";
						text2 += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
						text2 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
					}
					else
					{
						text2 += "2";
						text2 += Utils.FitStringLength(boleto.DataJurosMora.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
						text2 += Utils.FitStringLength(boleto.PercJurosMora.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
					}
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
				text2 += Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text2 += "3";
						text2 += "00";
					}
					else
					{
						switch (boleto.Instrucoes[0].Codigo)
						{
						case 1:
							text2 += "1";
							if (boleto.Instrucoes[0].QuantidadeDias >= 6 && boleto.Instrucoes[0].QuantidadeDias <= 90)
							{
								text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text2 += "06";
							}
							break;
						case 2:
							text2 += "1";
							if (boleto.Instrucoes[0].QuantidadeDias >= 2 && boleto.Instrucoes[0].QuantidadeDias <= 5)
							{
								text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text2 += "02";
							}
							break;
						case 3:
							text2 += "3";
							text2 += "00";
							break;
						default:
						{
							int num = boleto.Instrucoes[1].Codigo;
							if (num != 1)
							{
								if (num != 2)
								{
									text2 += "3";
									text2 += "00";
								}
								else
								{
									text2 += "1";
									if (boleto.Instrucoes[1].QuantidadeDias >= 2 && boleto.Instrucoes[1].QuantidadeDias <= 5)
									{
										text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
									else
									{
										text2 += "02";
									}
								}
							}
							else
							{
								text2 += "1";
								if (boleto.Instrucoes[1].QuantidadeDias >= 6 && boleto.Instrucoes[1].QuantidadeDias <= 90)
								{
									text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								}
								else
								{
									text2 += "06";
								}
							}
							break;
						}
						}
					}
				}
				catch (Exception)
				{
					text2 += "3";
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
						if (num != 4)
						{
							if (num != 5)
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num == 4)
								{
									text2 += "1";
									if (boleto.Instrucoes[1].QuantidadeDias >= 5 && boleto.Instrucoes[1].QuantidadeDias <= 120)
									{
										text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
									}
									else
									{
										text2 += "120";
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
								text2 += "2";
								text2 += "000";
							}
						}
						else
						{
							text2 += "1";
							if (boleto.Instrucoes[0].QuantidadeDias >= 5 && boleto.Instrucoes[0].QuantidadeDias <= 120)
							{
								text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
							}
							else
							{
								text2 += "120";
							}
						}
					}
				}
				catch (Exception)
				{
					text2 += "2";
					text2 += "000";
				}
				text2 += Utils.FormatCode(boleto.Moeda.ToString(CultureInfo.InvariantCulture), "0", 2, true);
				text2 += Utils.FormatCode("0", 10);
				text2 += " ";
				text2 = Utils.SubstituiCaracteresEspeciais(text2);
				text4 = text2;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar SEGMENTO P do arquivo de remessa.", ex);
			}
			return text4;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00035DE0 File Offset: 0x00033FE0
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text = " ";
			TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
			if (tipoSolucao != TipoSolucao.Sigcb)
			{
				if (tipoSolucao == TipoSolucao.Sicob)
				{
					text = this.GerarDetalheSegmentoRRemessaCnab240Sicob(boleto, numeroRegistro);
				}
			}
			else
			{
				text = this.GerarDetalheSegmentoRRemessaCnab240Sigcb(boleto, numeroRegistro);
			}
			return text;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00035E20 File Offset: 0x00034020
		public string GerarDetalheSegmentoRRemessaCnab240Sigcb(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro)
		{
			string text3;
			try
			{
				string text = new string(' ', 90);
				string text2 = new string(' ', 11);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "000000000000000000000000" + "000000000000000000000000" + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + Utils.FitStringLength(boleto.Sacado.Endereco.Email.TrimStart(Array.Empty<char>()), 50, 50, ' ', 0, true, true, false).ToUpper() + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00035FB8 File Offset: 0x000341B8
		public string GerarDetalheSegmentoRRemessaCnab240Sicob(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro)
		{
			string text4;
			try
			{
				string text = new string(' ', 90);
				string text2 = new string(' ', 48);
				string text3 = new string(' ', 61);
				text4 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + text2 + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + text3);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0003611C File Offset: 0x0003431C
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return this.GerarDetalheSegmentoQRemessaCnab240(boleto, numeroRegistro, tipoArquivo);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00036128 File Offset: 0x00034328
		public string GerarDetalheSegmentoQRemessaCnab240(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "3" + Utils.FormatCode(numeroRegistro.ToString(CultureInfo.InvariantCulture), "0", 5, true) + "Q" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + ((boleto.Sacado.CpfCnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(boleto.Sacado.CpfCnpj, "0", 15, true) + Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper() + "0" + Utils.FormatCode("", "0", 15) + Utils.FormatCode("", " ", 40) + Utils.FormatCode("", " ", 31));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar SEGMENTO Q do arquivo de remessa.", ex);
			}
			return text;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000363EC File Offset: 0x000345EC
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			return this.GerarTrailerLoteRemessaCnab240(numeroRegistro, quantidadeBoletos, vltitulostotal);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000363F8 File Offset: 0x000345F8
		public string GerarTrailerLoteRemessaCnab240(int numeroRegistro, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", " ", 31) + Utils.FormatCode("", " ", 117));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar Trailer de Lote do arquivo de remessa.", ex);
			}
			return text;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00036570 File Offset: 0x00034770
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			return this.GerarTrailerArquivoRemessaCnab240(numeroRegistro);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0003657C File Offset: 0x0003477C
		public string GerarTrailerArquivoRemessaCnab240(int numeroRegistro)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "9999" + "9" + Utils.FormatCode("", " ", 9) + "000001" + Utils.FormatCode(numeroRegistro.ToString(CultureInfo.InvariantCulture), "0", 6, true) + Utils.FormatCode("", " ", 6) + Utils.FormatCode("", " ", 205));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar Trailer de arquivo de remessa.", ex);
			}
			return text;
		}

		// Token: 0x04000638 RID: 1592
		private string _dacBoleto = string.Empty;

		// Token: 0x04000639 RID: 1593
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

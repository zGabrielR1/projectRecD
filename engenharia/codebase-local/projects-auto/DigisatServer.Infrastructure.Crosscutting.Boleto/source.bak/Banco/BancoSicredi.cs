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
	// Token: 0x02000174 RID: 372
	internal sealed class BancoSicredi : AbstractBanco, IBanco
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0006BF48 File Offset: 0x0006A148
		internal BancoSicredi()
		{
			this.Codigo = 748;
			this.Digito = "X";
			this.Nome = "Banco Sicredi";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0006BF98 File Offset: 0x0006A198
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("{0}{1:MM}{2:dd}.{3}", new object[]
				{
					Utils.FitStringLength(this.CodigoCedenteParaComporNomeDoArquivoRemessa, 5, 5, '0', 0, true, true, true),
					this.RetornarMesParaNomeDoArquivoDeRemessa(),
					DateTime.Now.Date,
					this.RetornarExtensaoParaNomeDoArquivoDeRemessa()
				});
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0006BFF4 File Offset: 0x0006A1F4
		private string RetornarMesParaNomeDoArquivoDeRemessa()
		{
			int month = DateTime.Now.Date.Month;
			switch (month)
			{
			case 10:
				return "O";
			case 11:
				return "N";
			case 12:
				return "D";
			default:
				return month.ToString();
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0006C048 File Offset: 0x0006A248
		private string RetornarExtensaoParaNomeDoArquivoDeRemessa()
		{
			char c = this._alpha[this.SequenciaRemessaDiaria - 1];
			if (c == 'C')
			{
				return "CRM";
			}
			return string.Format("RM{0}", c);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0006C080 File Offset: 0x0006A280
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.CodigoCedenteParaComporNomeDoArquivoRemessa = boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente.ToString();
				if (!string.IsNullOrEmpty(boleto.Cedente.ContaBancaria.OperacaoConta))
				{
					this.FormatoAgenciaCodigoCedente = string.Format("{0}.{1}.{2}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, 4), Utils.FormatCode(boleto.Cedente.ContaBancaria.OperacaoConta, 2), Utils.FormatCode(boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente, 5));
				}
				else
				{
					this.FormatoAgenciaCodigoCedente = Utils.FormataAgenciaConta(boleto.Cedente.ContaBancaria.Agencia, boleto.Cedente.ContaBancaria.DigitoAgencia, boleto.Cedente.ContaBancaria.Conta, boleto.Cedente.ContaBancaria.DigitoConta);
				}
				if (Utils.ToString(boleto.NossoNumero) == string.Empty)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				boleto.NossoNumero = boleto.DataProcessamento.ToString("yy") + "2" + Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				string text = Utils.FitStringLength((boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia).CopiarTextoDaDireitaParaEsquerda(4), 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Cedente.ContaBancaria.OperacaoConta.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true) + Utils.FitStringLength((boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente).CopiarTextoDaDireitaParaEsquerda(5), 5, 5, '0', 0, true, true, true) + boleto.NossoNumero;
				boleto.DigitoNossoNumero = AbstractBanco.Mod11Base9(text).ToString(CultureInfo.InvariantCulture);
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no SICREDI";
				}
				this.FormataCodigoBarra(boleto);
				this.FormataLinhaDigitavel(boleto);
				this.FormataNumeroDocumento(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0006C32C File Offset: 0x0006A52C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			string text2 = Utils.FormatCode(boleto.Moeda.ToString(CultureInfo.InvariantCulture), 1);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text3 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text4 = string.Concat(new object[]
			{
				boleto.TipoCobranca.Codigo,
				boleto.Carteira,
				boleto.NossoNumero,
				boleto.DigitoNossoNumero,
				Utils.FitStringLength((boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia).CopiarTextoDaDireitaParaEsquerda(4), 4, 4, '0', 0, true, true, true),
				Utils.FitStringLength(boleto.Cedente.ContaBancaria.OperacaoConta.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true),
				Utils.FitStringLength((boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente).CopiarTextoDaDireitaParaEsquerda(5), 5, 5, '0', 0, true, true, true),
				"10"
			});
			string text5 = this.Mod11SicrediCampoLivre(text4).ToString(CultureInfo.InvariantCulture);
			text4 += text5;
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[] { text, text2, num, text3, text4 });
			this._dacBoleto = this.Mod11SicrediGeral(boleto.CodigoBarra.Codigo);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0006C53C File Offset: 0x0006A73C
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.CodigoBarra.Codigo.Substring(0, 4);
			string text2 = boleto.CodigoBarra.Codigo.Substring(19, 5);
			string text3 = this.Mod10Sicredi(text + text2).ToString(CultureInfo.InvariantCulture);
			string text4 = text + text2 + text3;
			text4 = text4.Substring(0, 5) + "." + text4.Substring(5) + " ";
			text = boleto.CodigoBarra.Codigo.Substring(24, 10);
			text2 = this.Mod10Sicredi(text).ToString(CultureInfo.InvariantCulture);
			string text5 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			text = boleto.CodigoBarra.Codigo.Substring(34, 10);
			text2 = this.Mod10Sicredi(text).ToString(CultureInfo.InvariantCulture);
			string text6 = string.Format("{0}.{1}{2} ", text.Substring(0, 5), text.Substring(5, 5), text2);
			int dacBoleto = this._dacBoleto;
			string text7 = string.Format("{0} ", dacBoleto);
			text = boleto.CodigoBarra.Codigo.Substring(5, 4);
			text2 = boleto.CodigoBarra.Codigo.Substring(9, 10);
			string text8 = string.Format("{0}{1}", text, text2);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text4, text5, text6, text7, text8 });
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0006C6C4 File Offset: 0x0006A8C4
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string nossoNumero = boleto.NossoNumero;
			boleto.NossoNumero = string.Format("{0}/{1}-{2}", nossoNumero.Substring(0, 2), nossoNumero.Substring(2, 6), nossoNumero.Substring(8));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0006C700 File Offset: 0x0006A900
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? string.Concat(new string[] { string.Format("{0}/{1}-{2}", boleto.NossoNumero.Substring(0, 2), boleto.NossoNumero.Substring(2, 6), boleto.DigitoNossoNumero) }) : boleto.NossoNumero);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0006C760 File Offset: 0x0006A960
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

		// Token: 0x060007FE RID: 2046 RVA: 0x0006C7F0 File Offset: 0x0006A9F0
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia + cedente.ContaBancaria.DigitoAgencia, "0", 5, true) + " " + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("SICREDI", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true) + "081" + "01600" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00058384 File Offset: 0x00056584
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Funçao não implementada!");
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0006CA2C File Offset: 0x0006AC2C
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

		// Token: 0x06000801 RID: 2049 RVA: 0x0006CAB4 File Offset: 0x0006ACB4
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
				text4 += "040";
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
				text4 += ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false));
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

		// Token: 0x06000802 RID: 2050 RVA: 0x0006CD04 File Offset: 0x0006AF04
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

		// Token: 0x06000803 RID: 2051 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab400()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0006CD88 File Offset: 0x0006AF88
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
					decimal num = ((boleto.JurosMora < 0.01m) ? 0.01m : boleto.JurosMora);
					text += "1";
					text += "00000000";
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
						int num2 = boleto.Instrucoes[0].Codigo;
						if (num2 <= 3)
						{
							if (num2 != 1)
							{
								if (num2 == 3)
								{
									text += "3";
									text += "00";
									goto IL_06EF;
								}
							}
							else
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								if (boleto.Instrucoes[0].QuantidadeDias >= 3)
								{
									text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									goto IL_06EF;
								}
								text += "03";
								goto IL_06EF;
							}
						}
						else if (num2 == 8 || num2 == 9)
						{
							text += boleto.Instrucoes[0].Codigo;
							if (boleto.Instrucoes[0].QuantidadeDias >= 0)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_06EF;
							}
							text += "00";
							goto IL_06EF;
						}
						num2 = boleto.Instrucoes[1].Codigo;
						if (num2 != 1)
						{
							if (num2 != 8 && num2 != 9)
							{
								text += "3";
								text += "00";
							}
							else
							{
								text += boleto.Instrucoes[1].Codigo;
								text += "00";
							}
						}
						else
						{
							text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
							if (boleto.Instrucoes[1].QuantidadeDias >= 3)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "03";
							}
						}
					}
					IL_06EF:;
				}
				catch (Exception)
				{
					text += "3";
					text += "00";
				}
				text += "1";
				text += "060";
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

		// Token: 0x06000806 RID: 2054 RVA: 0x0006D524 File Offset: 0x0006B724
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

		// Token: 0x06000807 RID: 2055 RVA: 0x0006D808 File Offset: 0x0006BA08
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = new string(' ', 9);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "0" + "00000000" + "000000000000000" + "0" + "00000000" + "000000000000000" + "2" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + "0000000000000000" + " " + "000000000000" + " " + " " + "0" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0006D9D0 File Offset: 0x0006BBD0
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

		// Token: 0x06000809 RID: 2057 RVA: 0x0006DA58 File Offset: 0x0006BC58
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais("74899999" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", 6) + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		public string GerarTrailerRemessaCnab400(int numeroRegistro, Cedente cedente)
		{
			string text2;
			try
			{
				string text = new string(' ', 384);
				string conta = cedente.ContaBancaria.Conta;
				text2 = Utils.SubstituiCaracteresEspeciais("91748" + Utils.FormatCode(conta, 5) + text + Utils.FormatCode(numeroRegistro.ToString(), 6));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0006DB64 File Offset: 0x0006BD64
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", " ", 8) + Utils.FormatCode("", " ", 117));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
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

		// Token: 0x0600080D RID: 2061 RVA: 0x0006DD9C File Offset: 0x0006BF9C
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

		// Token: 0x0600080E RID: 2062 RVA: 0x0006E014 File Offset: 0x0006C214
		public int Mod10Sicredi(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				int num3 = Convert.ToInt32(seq.Substring(i, 1)) * num2;
				if (num3 > 9)
				{
					num3 = this.SomaDezena(num3);
				}
				num += num3;
				if (num2 < 2)
				{
					num2++;
				}
				else
				{
					num2--;
				}
			}
			return AbstractBanco.Multiplo10(num);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0006E070 File Offset: 0x0006C270
		public int SomaDezena(int dezena)
		{
			string text = dezena.ToString(CultureInfo.InvariantCulture);
			int num = Convert.ToInt32(text.Substring(0, 1));
			int num2 = Convert.ToInt32(text.Substring(1));
			return num + num2;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0006E0A8 File Offset: 0x0006C2A8
		public int Mod11SicrediCampoLivre(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				num += Convert.ToInt32(seq.Substring(i, 1)) * num2;
				if (num2 < 9)
				{
					num2++;
				}
				else
				{
					num2 = 2;
				}
			}
			int num3 = num % 11;
			if (num3 == 0 || num3 == 1)
			{
				return 0;
			}
			return 11 - num3;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0006E100 File Offset: 0x0006C300
		public int Mod11SicrediGeral(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length - 1; i >= 0; i--)
			{
				num += Convert.ToInt32(seq.Substring(i, 1)) * num2;
				if (num2 < 9)
				{
					num2++;
				}
				else
				{
					num2 = 2;
				}
			}
			int num3 = 11 - num % 11;
			if (num3 > 9 || num3 == 0)
			{
				num3 = 1;
			}
			return num3;
		}

		// Token: 0x0400066B RID: 1643
		private int _dacBoleto;

		// Token: 0x0400066C RID: 1644
		private readonly char[] _alpha = "C234567890".ToCharArray();
	}
}

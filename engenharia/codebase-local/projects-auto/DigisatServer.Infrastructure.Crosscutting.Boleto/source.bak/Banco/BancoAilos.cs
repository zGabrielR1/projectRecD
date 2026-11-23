using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200016B RID: 363
	internal sealed class BancoAilos : AbstractBanco, IBanco
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x00057BEC File Offset: 0x00055DEC
		internal BancoAilos()
		{
			this.Codigo = 85;
			this.Digito = "0";
			this.Nome = "Cooperativa Central de Crédito - Ailos";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00057C38 File Offset: 0x00055E38
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00057C78 File Offset: 0x00055E78
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormatoAgenciaCodigoCedente = Utils.FormataAgenciaConta(boleto.Cedente.ContaBancaria.Agencia, boleto.Cedente.ContaBancaria.DigitoAgencia, boleto.Cedente.ContaBancaria.Conta, boleto.Cedente.ContaBancaria.DigitoConta);
				if (Utils.ToString(boleto.NossoNumero) == string.Empty)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				boleto.NossoNumero = Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta, 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 9, 9, '0', 0, true, true, true);
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no AILOS";
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

		// Token: 0x06000713 RID: 1811 RVA: 0x00057DD0 File Offset: 0x00055FD0
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			string text2 = Utils.FormatCode(boleto.Moeda.ToString(CultureInfo.InvariantCulture), 1);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text3 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text4 = Utils.FitStringLength(boleto.Cedente.Convenio.CopiarTextoDaDireitaParaEsquerda(6), 6, 6, '0', 0, true, true, true) + boleto.NossoNumero + Utils.FitStringLength(boleto.Carteira, 2, 2, '0', 0, true, true, true);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[] { text, text2, num, text3, text4 });
			this._dacBoleto = this.Mod11AilosGeral(boleto.CodigoBarra.Codigo);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00057F1C File Offset: 0x0005611C
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

		// Token: 0x06000715 RID: 1813 RVA: 0x000580A0 File Offset: 0x000562A0
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = boleto.NossoNumero;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000580AE File Offset: 0x000562AE
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = boleto.NumeroDocumento + " " + boleto.NumeroParcela;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000580D4 File Offset: 0x000562D4
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

		// Token: 0x06000718 RID: 1816 RVA: 0x00058164 File Offset: 0x00056364
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("CECRED", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true) + "087" + "01600" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00058384 File Offset: 0x00056584
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Funçao não implementada!");
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00058390 File Offset: 0x00056590
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

		// Token: 0x0600071B RID: 1819 RVA: 0x00058418 File Offset: 0x00056618
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
				text3 += "045";
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
				text3 += Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false);
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

		// Token: 0x0600071C RID: 1820 RVA: 0x00058650 File Offset: 0x00056850
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

		// Token: 0x0600071D RID: 1821 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab400()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000586D4 File Offset: 0x000568D4
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
				text += (boleto.SolicitacaoDeBaixa ? "02" : "01");
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 5, true);
				text += Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 1, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += " ";
				text += Utils.FitStringLength(boleto.NossoNumero, 20, 20, ' ', 0, true, true, false);
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
							if ((boleto.Instrucoes[0].QuantidadeDias >= 5 && boleto.Instrucoes[0].QuantidadeDias <= 15) || boleto.Instrucoes[0].QuantidadeDias == 45)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "05";
							}
							break;
						case 3:
							text += "3";
							text += "00";
							break;
						default:
						{
							int codigo = boleto.Instrucoes[1].Codigo;
							if (codigo == 1 || codigo == 2)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								if ((boleto.Instrucoes[1].QuantidadeDias >= 5 && boleto.Instrucoes[1].QuantidadeDias <= 15) || boleto.Instrucoes[1].QuantidadeDias == 45)
								{
									text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								}
								else
								{
									text += "05";
								}
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
				text += "2";
				text += "   ";
				text += "09";
				text += Utils.FitStringLength(boleto.Cedente.Contrato, 10, 10, '0', 0, true, true, true);
				text += " ";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00058E2C File Offset: 0x0005702C
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
				text4 += (boleto.SolicitacaoDeBaixa ? "02" : "01");
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

		// Token: 0x06000721 RID: 1825 RVA: 0x00059100 File Offset: 0x00057300
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = new string(' ', 9);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : "01") + "0" + "00000000" + "000000000000000" + "0" + "00000000" + "000000000000000" + "2" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + "0000000000000000" + " " + "000000000000" + " " + " " + "0" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000592BC File Offset: 0x000574BC
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

		// Token: 0x06000723 RID: 1827 RVA: 0x00059344 File Offset: 0x00057544
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais("08599999" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", 6) + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000593D8 File Offset: 0x000575D8
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

		// Token: 0x06000725 RID: 1829 RVA: 0x00059450 File Offset: 0x00057650
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(quantidadeBoletos.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", " ", 8) + Utils.FormatCode("", " ", 117));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000595F0 File Offset: 0x000577F0
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

		// Token: 0x06000727 RID: 1831 RVA: 0x000596A8 File Offset: 0x000578A8
		public int Mod11AilosGeral(string seq)
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

		// Token: 0x04000652 RID: 1618
		private int _dacBoleto;

		// Token: 0x04000653 RID: 1619
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

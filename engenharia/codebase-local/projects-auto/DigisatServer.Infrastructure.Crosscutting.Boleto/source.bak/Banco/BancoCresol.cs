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
	// Token: 0x0200016A RID: 362
	internal sealed class BancoCresol : AbstractBanco, IBanco
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00053BEC File Offset: 0x00051DEC
		internal BancoCresol()
		{
			this.Codigo = 133;
			this.Digito = "3";
			this.Nome = "Cresol";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00053C3C File Offset: 0x00051E3C
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("{0:ddMMyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00053C7C File Offset: 0x00051E7C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}-", boleto.Cedente.ContaBancaria.Agencia) + string.Format("{0}/", boleto.Cedente.ContaBancaria.DigitoAgencia) + string.Format("{0}-", boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture)) + string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture));
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
			if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 11)
			{
				throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 11 números.");
			}
			if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 11)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
			}
			boleto.DigitoNossoNumero = this.CalcularDigitoNossoNumero(boleto);
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = "PAGÁVEL EM QUALQUER AGÊNCIA BANCÁRIA/CORRESPONDENTE BANCÁRIO";
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNumeroDocumento(boleto);
			this.FormataNossoNumeroImpressao(boleto);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00053E13 File Offset: 0x00052013
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return BancoCresol.Mod11Cresol(string.Format("{0}{1}", Utils.FormatCode(boleto.Carteira, 2), boleto.NossoNumero), 7);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00053E38 File Offset: 0x00052038
		private static string Mod11Cresol(string seq, int b)
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

		// Token: 0x060006F8 RID: 1784 RVA: 0x00053EA4 File Offset: 0x000520A4
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				boleto.CodigoBarra.Codigo = string.Concat(new string[]
				{
					string.Format("{0}", this.Codigo),
					string.Format("{0}", boleto.Moeda),
					string.Format("{0}", AbstractBanco.FatorVencimento(boleto)),
					string.Format("{0}", text),
					string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4)),
					string.Format("{0}", Utils.FormatCode(boleto.Carteira, 2)),
					string.Format("{0}", boleto.NossoNumero),
					string.Format("{0}0", Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7))
				});
				this._dacBoleto = this.Mod11Base9Cresol(boleto.CodigoBarra.Codigo);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0005404C File Offset: 0x0005224C
		public int Mod11Base9Cresol(string seq)
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

		// Token: 0x060006FA RID: 1786 RVA: 0x000540B8 File Offset: 0x000522B8
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
				string text3 = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);
				string text4 = Utils.FormatCode(boleto.Cedente.Carteira, 2).Substring(0, 1);
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
				string text7 = string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4)) + string.Format("{0}", Utils.FormatCode(boleto.Carteira, 2)) + string.Format("{0}", boleto.NossoNumero) + string.Format("{0}0", Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7));
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

		// Token: 0x060006FB RID: 1787 RVA: 0x0004B282 File Offset: 0x00049482
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00054338 File Offset: 0x00052538
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = string.Format("{0}/{1}-{2}", boleto.Carteira.PreencherComCaractere(Direcao.Esquerda, 2, '0'), boleto.NossoNumero, boleto.DigitoNossoNumero);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00054368 File Offset: 0x00052568
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

		// Token: 0x060006FE RID: 1790 RVA: 0x000543C4 File Offset: 0x000525C4
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("CRESOL CONFEDERAÇÃO", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + "000000" + "084" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000545F0 File Offset: 0x000527F0
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 277);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + Utils.FitStringLength(cedente.Convenio, 20, 20, '0', 0, true, true, true) + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + this.Codigo + Utils.FitStringLength(this.Nome, 15, 15, ' ', 0, true, true, false).ToUpper() + DateTime.Now.ToString("ddMMyy") + "          " + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true) + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000546EC File Offset: 0x000528EC
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

		// Token: 0x06000701 RID: 1793 RVA: 0x00054774 File Offset: 0x00052974
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 40);
				string text3 = new string(' ', 33);
				text4 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "1" + "R" + "01" + "  " + "042" + " " + ((cedente.Cpfcnpj.Length <= 11) ? "1" : "2") + Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + text2 + text2 + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true) + DateTime.Now.ToString("ddMMyyyy") + "00000000" + text3);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00054990 File Offset: 0x00052B90
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

		// Token: 0x06000703 RID: 1795 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00054A08 File Offset: 0x00052C08
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = "1";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += "     ";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += "0";
				text += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text += "   ";
				if (boleto.PercMulta > 0m)
				{
					text += "2";
					text += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
				}
				else
				{
					text += "0";
					text += "0000";
				}
				text += Utils.FitStringLength(boleto.NossoNumero, 11, 11, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
				text += new string(' ', 10);
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += " ";
				text += new string(' ', 10);
				text += " ";
				text += " ";
				text += "  ";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text += boleto.DataVencimento.ToString("ddMMyy");
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += "   ";
				text += "     ";
				text += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text += " ";
				text += boleto.DataDocumento.ToString("ddMMyy");
				text += "    ";
				text += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += "0000000000000";
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
				text += new string(' ', 12);
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text += new string(' ', 60);
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

		// Token: 0x06000705 RID: 1797 RVA: 0x00055048 File Offset: 0x00053248
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
				text += "000";
				text += "00000";
				text += Utils.FitStringLength(boleto.NossoNumero, 11, 11, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += "0";
				text += "0";
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += " ";
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
					text += "1";
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
						if (num != 3)
						{
							if (num == 99)
							{
								text += "1";
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num == 99)
								{
									text += "1";
									text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								}
								else
								{
									text += "3";
									text += "00";
								}
							}
						}
						else
						{
							text += "3";
							text += "00";
						}
					}
				}
				catch (Exception)
				{
					text += "3";
					text += "00";
				}
				text += "0";
				text += "000";
				text += "09";
				text += "00000000000";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00055658 File Offset: 0x00053858
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

		// Token: 0x06000707 RID: 1799 RVA: 0x0005590C File Offset: 0x00053B0C
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

		// Token: 0x06000708 RID: 1800 RVA: 0x00055AAC File Offset: 0x00053CAC
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FormatCode("", "0", 92) + Utils.FormatCode("", " ", 125));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00055B74 File Offset: 0x00053D74
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
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00055BEC File Offset: 0x00053DEC
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

		// Token: 0x0600070B RID: 1803 RVA: 0x00055CA0 File Offset: 0x00053EA0
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

		// Token: 0x0600070C RID: 1804 RVA: 0x00055D0C File Offset: 0x00053F0C
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

		// Token: 0x0600070D RID: 1805 RVA: 0x00055DC4 File Offset: 0x00053FC4
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
				decimal num2 = Convert.ToUInt64(registro.Substring(175, 7));
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
				detalheRetornoCNAB.NossoNumero = registro.Substring(70, 11);
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(registro.Substring(295, 6));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				switch (codigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_020A;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarMotivoDaRejeicao(registro.Substring(318, 2));
					goto IL_020A;
				case 4:
				case 5:
				case 7:
				case 8:
					goto IL_0203;
				case 6:
					break;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					goto IL_020A;
				default:
					if (codigoOcorrencia != 15)
					{
						goto IL_0203;
					}
					break;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
				goto IL_020A;
				IL_0203:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_020A:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00056010 File Offset: 0x00054210
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2280673654U)
			{
				if (num <= 502098945U)
				{
					if (num <= 434988469U)
					{
						if (num <= 401286136U)
						{
							if (num != 350953279U)
							{
								if (num != 367583803U)
								{
									if (num == 401286136U)
									{
										if (codigo == "14")
										{
											return "14-Confirmação recebimento instrução alteração de vencimento";
										}
									}
								}
								else if (codigo == "60")
								{
									return "60-Confirmação do Pedido de Dispensa de Juros de Mora";
								}
							}
							else if (codigo == "19")
							{
								return "19-Confirmação recebimento instrução de protesto";
							}
						}
						else if (num != 418063755U)
						{
							if (num != 418210850U)
							{
								if (num == 434988469U)
								{
									if (codigo == "08")
									{
										return "08-Confirmação do Recebimento do Cancelamento do Desconto";
									}
								}
							}
							else if (codigo == "09")
							{
								return "09-Baixa";
							}
						}
						else if (codigo == "15")
						{
							return "15-Franco de pagamento";
						}
					}
					else if (num <= 468543707U)
					{
						if (num != 451618993U)
						{
							if (num != 451766088U)
							{
								if (num == 468543707U)
								{
									if (codigo == "06")
									{
										return "06-Liquidação";
									}
								}
							}
							else if (codigo == "07")
							{
								return "07-Confirmação do Recebimento da Instrução de Desconto";
							}
						}
						else if (codigo == "17")
						{
							return "17-Liquidação após baixa ou liquidação de título não registrado";
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
									return "05-Transferência de carteira/baixa";
								}
							}
						}
						else if (codigo == "11")
						{
							return "11-Títulos em carteira (em ser)";
						}
					}
					else if (num != 501951850U)
					{
						if (num == 502098945U)
						{
							if (codigo == "04")
							{
								return "04-Transferência de carteira/entrada";
							}
						}
					}
					else if (codigo == "12")
					{
						return "12-Confirmação recebimento instrução de abatimento";
					}
				}
				else if (num <= 2229355059U)
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
										return "02-Entrada Confirmada";
									}
								}
							}
							else if (codigo == "03")
							{
								return "03-Entrada rejeitada";
							}
						}
						else if (codigo == "13")
						{
							return "13-Confirmação recebimento instrução de cancelamento de abatimento";
						}
					}
					else if (num != 569209421U)
					{
						if (num != 2212577440U)
						{
							if (num == 2229355059U)
							{
								if (codigo == "51")
								{
									return "51-Título DDA reconhecido pelo sacado";
								}
							}
						}
						else if (codigo == "50")
						{
							return "50-Título pago com cheque pendente de liquidação";
						}
					}
					else if (codigo == "00")
					{
						return "00-Ocorrência Desconhecida";
					}
				}
				else if (num <= 2263057392U)
				{
					if (num != 2246132678U)
					{
						if (num != 2262910297U)
						{
							if (num == 2263057392U)
							{
								if (codigo == "43")
								{
									return "43-Confirmação da alteração dos dados do Sacador/Avalista";
								}
							}
						}
						else if (codigo == "53")
						{
							return "53-Título DDA recusado pela CIP";
						}
					}
					else if (codigo == "52")
					{
						return "52-Título DDA não reconhecido pelo sacado";
					}
				}
				else if (num <= 2279687916U)
				{
					if (num != 2263896035U)
					{
						if (num == 2279687916U)
						{
							if (codigo == "54")
							{
								return "54-Confirmação da Instrução de Baixa de Título Negativado sem Protesto";
							}
						}
					}
					else if (codigo == "33")
					{
						return "33-Confirmação da Alteração dos Dados do Rateio de Crédito";
					}
				}
				else if (num != 2279835011U)
				{
					if (num == 2280673654U)
					{
						if (codigo == "30")
						{
							return "30-Alteração de dados rejeitada";
						}
					}
				}
				else if (codigo == "42")
				{
					return "42-Confirmação da alteração dos dados do Sacado";
				}
			}
			else if (num <= 2346945487U)
			{
				if (num <= 2314228892U)
				{
					if (num <= 2297598368U)
					{
						if (num != 2296465535U)
						{
							if (num != 2296612630U)
							{
								if (num == 2297598368U)
								{
									if (codigo == "25")
									{
										return "25-Protestado e baixado (baixa por ter sido protestado)";
									}
								}
							}
							else if (codigo == "41")
							{
								return "41-Confirmação da alteração do número controle do Participante";
							}
						}
						else if (codigo == "55")
						{
							return "55-Confirmação de Pedido de Dispensa de Multa";
						}
					}
					else if (num != 2313243154U)
					{
						if (num != 2313390249U)
						{
							if (num == 2314228892U)
							{
								if (codigo == "36")
								{
									return "36-Confirmação de envio de e-mail/SMS";
								}
							}
						}
						else if (codigo == "40")
						{
							return "40-Confirmação da alteração do número do título dado pelo cedente";
						}
					}
					else if (codigo == "56")
					{
						return "56-Confirmação do Pedido de Cobrança de Multa";
					}
				}
				else if (num <= 2330167868U)
				{
					if (num != 2314375987U)
					{
						if (num != 2330020773U)
						{
							if (num == 2330167868U)
							{
								if (codigo == "47")
								{
									return "47-Instrução para protesto para fins falimentares confirmada";
								}
							}
						}
						else if (codigo == "57")
						{
							return "57-Confirmação do Pedido de Alteração de Cobrança de Juros";
						}
					}
					else if (codigo == "24")
					{
						return "24-Retirada de cartório e manutenção em carteira";
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
								return "27-Confirmação do pedido de alteração de outros dados";
							}
						}
					}
					else if (codigo == "37")
					{
						return "37-Envio de e-mail/SMS rejeitado";
					}
				}
				else if (num != 2346798392U)
				{
					if (num == 2346945487U)
					{
						if (codigo == "46")
						{
							return "46-Instrução para cancelar protesto confirmada";
						}
					}
				}
				else if (codigo == "58")
				{
					return "58-Confirmação do Pedido de Alteração do Valor/Data de Desconto";
				}
			}
			else if (num <= 2381486463U)
			{
				if (num <= 2363576011U)
				{
					if (num != 2347784130U)
					{
						if (num != 2347931225U)
						{
							if (num == 2363576011U)
							{
								if (codigo == "59")
								{
									return "59-Confirmação do Pedido de Alteração do Cedente do Título";
								}
							}
						}
						else if (codigo == "26")
						{
							return "26-Instrução rejeitada";
						}
					}
					else if (codigo == "34")
					{
						return "34-Confirmação do Cancelamento dos Dados do Rateio de Crédito";
					}
				}
				else if (num <= 2364561749U)
				{
					if (num != 2363723106U)
					{
						if (num == 2364561749U)
						{
							if (codigo == "35")
							{
								return "35-Confirmação do Desagendamento do Débito Automático";
							}
						}
					}
					else if (codigo == "45")
					{
						return "45-Título pago com cheque compensado";
					}
				}
				else if (num != 2380500725U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return "20-Confirmação recebimento instrução de sustação/cancelamento de protesto";
						}
					}
				}
				else if (codigo == "44")
				{
					return "44-Título pago com cheque devolvido";
				}
			}
			else if (num <= 2430833582U)
			{
				if (num != 2398264082U)
				{
					if (num != 2414894606U)
					{
						if (num == 2430833582U)
						{
							if (codigo == "49")
							{
								return "49-Alteração de contrato de cobrança";
							}
						}
					}
					else if (codigo == "38")
					{
						return "38-Confirmação de alteração do Prazo Limite de Recebimento";
					}
				}
				else if (codigo == "23")
				{
					return "23-Remessa a cartório (aponte em cartório)";
				}
			}
			else if (num <= 2447611201U)
			{
				if (num != 2431672225U)
				{
					if (num == 2447611201U)
					{
						if (codigo == "48")
						{
							return "48-Confirmação de instrução de transferência de carteira/modalidade de cobrança";
						}
					}
				}
				else if (codigo == "39")
				{
					return "39-Confirmação de Dispensa de Prazo Limite de Recebimento";
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
				return "29-Ocorrências do pagador";
			}
			return "";
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0005691C File Offset: 0x00054B1C
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2297598368U)
			{
				if (num <= 451471898U)
				{
					if (num <= 335455588U)
					{
						if (num <= 201234636U)
						{
							if (num <= 167679398U)
							{
								if (num != 134124160U)
								{
									if (num != 150901779U)
									{
										if (num == 167679398U)
										{
											if (codigo == "96")
											{
												return "96-Número de contrato inválido";
											}
										}
									}
									else if (codigo == "95")
									{
										return "95-Contrato não permite o envio de e-mail";
									}
								}
								else if (codigo == "94")
								{
									return "94-Código da Carteira inválido para envio e-mail";
								}
							}
							else if (num != 184457017U)
							{
								if (num != 199048284U)
								{
									if (num == 201234636U)
									{
										if (codigo == "90")
										{
											return "90-e-mail devolvido - caixa postal cheia";
										}
									}
								}
								else if (codigo == "B1")
								{
									return "B1-Autoriza recebimento parcial";
								}
							}
							else if (codigo == "97")
							{
								return "97-Rejeição da alteração do prazo limite de recebimento";
							}
						}
						else if (num <= 233362851U)
						{
							if (num != 216585232U)
							{
								if (num != 218012255U)
								{
									if (num == 233362851U)
									{
										if (codigo == "68")
										{
											return "68-Débito Automático Agendado";
										}
									}
								}
								else if (codigo == "91")
								{
									return "91-e-mail/número do celular do sacado não informado";
								}
							}
							else if (codigo == "69")
							{
								return "69-Débito Não Agendado - Erro nos Dados da Remessa";
							}
						}
						else if (num <= 251567493U)
						{
							if (num != 234789874U)
							{
								if (num == 251567493U)
								{
									if (codigo == "93")
									{
										return "93-Código para emissão de bloqueto não permite envio de e-mail";
									}
								}
							}
							else if (codigo == "92")
							{
								return "92-Sacado optante por Bloqueto Eletrônico - e-mail não enviado";
							}
						}
						else if (num != 334175660U)
						{
							if (num == 335455588U)
							{
								if (codigo == "98")
								{
									return "98-Rejeição de dispensa de prazo limite de recebimento";
								}
							}
						}
						else if (codigo == "18")
						{
							return "18-Vencimento fora do Prazo de Operação";
						}
					}
					else if (num <= 401286136U)
					{
						if (num <= 352233207U)
						{
							if (num != 350806184U)
							{
								if (num != 350953279U)
								{
									if (num == 352233207U)
									{
										if (codigo == "99")
										{
											return "99-Rejeição da alteração do número do título dado pelo cedente";
										}
									}
								}
								else if (codigo == "19")
								{
									return "19-Título a Cargo de Bancos Correspondentes com Vencimento Inferior a XX Dias";
								}
							}
							else if (codigo == "61")
							{
								return "61-Alteração da Agência Cobradora/DV Inválida";
							}
						}
						else if (num <= 384361422U)
						{
							if (num != 367583803U)
							{
								if (num == 384361422U)
								{
									if (codigo == "63")
									{
										return "63-Entrada para Título já Cadastrado";
									}
								}
							}
							else if (codigo == "60")
							{
								return "60-Movimento para Título Não Cadastrado";
							}
						}
						else if (num != 401139041U)
						{
							if (num == 401286136U)
							{
								if (codigo == "14")
								{
									return "14-Identificação da Distribuição do Bloqueto Inválida";
								}
							}
						}
						else if (codigo == "62")
						{
							return "62-Tipo de Impressão Inválido";
						}
					}
					else if (num <= 418210850U)
					{
						if (num != 417916660U)
						{
							if (num != 418063755U)
							{
								if (num == 418210850U)
								{
									if (codigo == "09")
									{
										return "09-Nosso Número Duplicado";
									}
								}
							}
							else if (codigo == "15")
							{
								return "15-Características da Cobrança Incompatíveis";
							}
						}
						else if (codigo == "65")
						{
							return "65-Código do Banco para Débito Inválido";
						}
					}
					else if (num <= 434841374U)
					{
						if (num != 434694279U)
						{
							if (num == 434841374U)
							{
								if (codigo == "16")
								{
									return "16-Data de Vencimento Inválida";
								}
							}
						}
						else if (codigo == "64")
						{
							return "64-Número da Linha Inválido";
						}
					}
					else if (num != 434988469U)
					{
						if (num == 451471898U)
						{
							if (codigo == "67")
							{
								return "67-Dados para Débito incompatível com a Identificação da Emissão do Bloqueto";
							}
						}
					}
					else if (codigo == "08")
					{
						return "08-Nosso Número Inválido";
					}
				}
				else if (num <= 569209421U)
				{
					if (num <= 485321326U)
					{
						if (num <= 468249517U)
						{
							if (num != 451618993U)
							{
								if (num != 451766088U)
								{
									if (num == 468249517U)
									{
										if (codigo == "66")
										{
											return "66-Agência/Conta/DV para Débito Inválido";
										}
									}
								}
								else if (codigo == "07")
								{
									return "07-Agência/Conta/DV Inválido";
								}
							}
							else if (codigo == "17")
							{
								return "17-Data de Vencimento Anterior à Data de Emissão";
							}
						}
						else if (num <= 468543707U)
						{
							if (num != 468396612U)
							{
								if (num == 468543707U)
								{
									if (codigo == "06")
									{
										return "06-Tipo/Número de Inscrição do Cedente Inválidos";
									}
								}
							}
							else if (codigo == "10")
							{
								return "10-Carteira inválida";
							}
						}
						else if (num != 485174231U)
						{
							if (num == 485321326U)
							{
								if (codigo == "05")
								{
									return "05-Código de Movimento Inválido";
								}
							}
						}
						else if (codigo == "11")
						{
							return "11-Forma de Cadastramento do Título Inválido";
						}
					}
					else if (num <= 518729469U)
					{
						if (num != 501951850U)
						{
							if (num != 502098945U)
							{
								if (num == 518729469U)
								{
									if (codigo == "13")
									{
										return "13-Identificação de Emissão do Bloqueto Inválida";
									}
								}
							}
							else if (codigo == "04")
							{
								return "04-Código de Movimento Não Permitido para Carteira";
							}
						}
						else if (codigo == "12")
						{
							return "12-Tipo de Documento Inválido";
						}
					}
					else if (num <= 535654183U)
					{
						if (num != 518876564U)
						{
							if (num == 535654183U)
							{
								if (codigo == "02")
								{
									return "02-Código do Registro Detalhe Inválido";
								}
							}
						}
						else if (codigo == "03")
						{
							return "03-Código do Segmento Inválido";
						}
					}
					else if (num != 552431802U)
					{
						if (num == 569209421U)
						{
							if (codigo == "00")
							{
								return "Sem Complemento a informar";
							}
						}
					}
					else if (codigo == "01")
					{
						return "01-Código do Banco Inválido";
					}
				}
				else if (num <= 2263896035U)
				{
					if (num <= 2246132678U)
					{
						if (num != 2212577440U)
						{
							if (num != 2229355059U)
							{
								if (num == 2246132678U)
								{
									if (codigo == "52")
									{
										return "52-Registro de Título já liquidado Cart. 17";
									}
								}
							}
							else if (codigo == "51")
							{
								return "51-CEP imcompatível com a Unidade da Federação";
							}
						}
						else if (codigo == "50")
						{
							return "50-CEP Referente a um Banco Correspondente";
						}
					}
					else if (num <= 2262910297U)
					{
						if (num != 2247118416U)
						{
							if (num == 2262910297U)
							{
								if (codigo == "53")
								{
									return "53-Tipo/Número de Inscrição do Sacador/Avalista Inválidos";
								}
							}
						}
						else if (codigo == "32")
						{
							return "32-Valor do IOF Inválido";
						}
					}
					else if (num != 2263057392U)
					{
						if (num == 2263896035U)
						{
							if (codigo == "33")
							{
								return "33-Valor de Abatimento Inválido";
							}
						}
					}
					else if (codigo == "43")
					{
						return "43-Prazo para Baixa/Devolução Inválida";
					}
				}
				else if (num <= 2280673654U)
				{
					if (num != 2279687916U)
					{
						if (num != 2279835011U)
						{
							if (num == 2280673654U)
							{
								if (codigo == "30")
								{
									return "30-Desconto a Conceder Não Confere";
								}
							}
						}
						else if (codigo == "42")
						{
							return "42-Código para Baixa/Devolução Inválido";
						}
					}
					else if (codigo == "54")
					{
						return "54-Sacador/Avalista Não Informado";
					}
				}
				else if (num <= 2296612630U)
				{
					if (num != 2296465535U)
					{
						if (num == 2296612630U)
						{
							if (codigo == "41")
							{
								return "41-Pedido de Cancelamento/Sustação para Títulos sem Instrução de Protesto";
							}
						}
					}
					else if (codigo == "55")
					{
						return "55-Nosso Número no Banco Correspondente Não Informado";
					}
				}
				else if (num != 2297451273U)
				{
					if (num == 2297598368U)
					{
						if (codigo == "25")
						{
							return "25-Data de Emissão Posterior à data de Entrega";
						}
					}
				}
				else if (codigo == "31")
				{
					return "31-Concessão de Desconto - Já Existe Desconto Anterior";
				}
			}
			else if (num <= 2382472201U)
			{
				if (num <= 2346945487U)
				{
					if (num <= 2330020773U)
					{
						if (num <= 2313537344U)
						{
							if (num != 2313243154U)
							{
								if (num != 2313390249U)
								{
									if (num == 2313537344U)
									{
										if (codigo == "76")
										{
											return "76-Débito Não Agendado, Tipo/Num. Inscrição do Debitado, Inválido";
										}
									}
								}
								else if (codigo == "40")
								{
									return "40-Título com Ordem de Protesto Emitida";
								}
							}
							else if (codigo == "56")
							{
								return "56-Código do Banco Correspondente Não Informado";
							}
						}
						else if (num != 2314228892U)
						{
							if (num != 2314375987U)
							{
								if (num == 2330020773U)
								{
									if (codigo == "57")
									{
										return "57-Código da Multa Inválido";
									}
								}
							}
							else if (codigo == "24")
							{
								return "24-Data de Emissão Inválida";
							}
						}
						else if (codigo == "36")
						{
							return "36-Concessão de Abatimento - Já Existe Abatimento Anterior";
						}
					}
					else if (num <= 2331006511U)
					{
						if (num != 2330167868U)
						{
							if (num != 2330314963U)
							{
								if (num == 2331006511U)
								{
									if (codigo == "37")
									{
										return "37-Código para Protesto Inválido";
									}
								}
							}
							else if (codigo == "77")
							{
								return "77-Transferência para Desconto Não Permitida para a Cateira do Título";
							}
						}
						else if (codigo == "47")
						{
							return "47-Endereço do Sacado Não Informado";
						}
					}
					else if (num <= 2332139344U)
					{
						if (num != 2331153606U)
						{
							if (num == 2332139344U)
							{
								if (codigo == "87")
								{
									return "87-e-mail/SMS enviado";
								}
							}
						}
						else if (codigo == "27")
						{
							return "27-Valor/Taxa de Juros de Mora Inválido";
						}
					}
					else if (num != 2346798392U)
					{
						if (num == 2346945487U)
						{
							if (codigo == "46")
							{
								return "46-Tipo/Número de Inscrição do Sacado Inválido";
							}
						}
					}
					else if (codigo == "58")
					{
						return "58-Data da Multa Inválida";
					}
				}
				else if (num <= 2363870201U)
				{
					if (num <= 2347931225U)
					{
						if (num != 2347092582U)
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
						else if (codigo == "74")
						{
							return "74-Débito Não Agendado - Data Vencimento Inválida";
						}
					}
					else if (num <= 2363576011U)
					{
						if (num != 2348916963U)
						{
							if (num == 2363576011U)
							{
								if (codigo == "59")
								{
									return "59-Valor/Percentual da Multa Inválido";
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
						if (num == 2363870201U)
						{
							if (codigo == "75")
							{
								return "75-Débito Não Agendado, Conforme seu Pedido, Título Não Registrado";
							}
						}
					}
					else if (codigo == "45")
					{
						return "45-Nome do Sacado não informado";
					}
				}
				else if (num <= 2365694582U)
				{
					if (num != 2364561749U)
					{
						if (num != 2364708844U)
						{
							if (num == 2365694582U)
							{
								if (codigo == "85")
								{
									return "85-Título com Pagamento Vinculado";
								}
							}
						}
						else if (codigo == "21")
						{
							return "21-Espécie do Título Inválida";
						}
					}
					else if (codigo == "35")
					{
						return "35-Valor a Conceder Não Confere";
					}
				}
				else if (num <= 2380647820U)
				{
					if (num != 2380500725U)
					{
						if (num == 2380647820U)
						{
							if (codigo == "72")
							{
								return "72-Débito Não Agendado - Cedente Não Participa da Modalidade Débito Automático";
							}
						}
					}
					else if (codigo == "44")
					{
						return "44-Código da Moeda Inválido";
					}
				}
				else if (num != 2381486463U)
				{
					if (num == 2382472201U)
					{
						if (codigo == "84")
						{
							return "84-Número Autorização Inexistente";
						}
					}
				}
				else if (codigo == "20")
				{
					return "20-Valor do Título Inválido";
				}
			}
			else if (num <= 2463482396U)
			{
				if (num <= 2416027439U)
				{
					if (num <= 2399249820U)
					{
						if (num != 2397425439U)
						{
							if (num != 2398264082U)
							{
								if (num == 2399249820U)
								{
									if (codigo == "83")
									{
										return "83-Limite Excedido";
									}
								}
							}
							else if (codigo == "23")
							{
								return "23-Aceite Inválido";
							}
						}
						else if (codigo == "73")
						{
							return "73-Débito Não Agendado - Código de Moeda Diferente de Reao (R$)";
						}
					}
					else if (num <= 2414894606U)
					{
						if (num != 2414203058U)
						{
							if (num == 2414894606U)
							{
								if (codigo == "38")
								{
									return "38-Prazo para Protesto Inválido";
								}
							}
						}
						else if (codigo == "70")
						{
							return "70-Débito Não Agendado - Sacado Não Consta do Cadastro de Autorizante";
						}
					}
					else if (num != 2415041701U)
					{
						if (num == 2416027439U)
						{
							if (codigo == "82")
							{
								return "82-Tentativas de Débito Esgotadas - Pendente";
							}
						}
					}
					else if (codigo == "22")
					{
						return "22-Espécie do Título Não Permitida para a Carteira";
					}
				}
				else if (num <= 2431672225U)
				{
					if (num != 2430833582U)
					{
						if (num != 2430980677U)
						{
							if (num == 2431672225U)
							{
								if (codigo == "39")
								{
									return "39-Pedido de Protesto Não Permitido para o Título";
								}
							}
						}
						else if (codigo == "71")
						{
							return "71-Débito Não Agendado - Cedente Não Autorizado pelo Sacado";
						}
					}
					else if (codigo == "49")
					{
						return "49-CEP Sem Praça de Cobrança (Não Localizado)";
					}
				}
				else if (num <= 2447611201U)
				{
					if (num != 2432805058U)
					{
						if (num == 2447611201U)
						{
							if (codigo == "48")
							{
								return "48-CEP Inválido";
							}
						}
					}
					else if (codigo == "81")
					{
						return "81-Tentativas de Débito Esgotadas - Baixado";
					}
				}
				else if (num != 2449582677U)
				{
					if (num == 2463482396U)
					{
						if (codigo == "A8")
						{
							return "A8-Valor do Abatimento inválido para cancelamento";
						}
					}
				}
				else if (codigo == "80")
				{
					return "80-Data do Desconto Inválida";
				}
			}
			else if (num <= 2564148110U)
			{
				if (num <= 2515707415U)
				{
					if (num != 2480260015U)
					{
						if (num != 2498929796U)
						{
							if (num == 2515707415U)
							{
								if (codigo == "28")
								{
									return "28-Código de Desconto inválido";
								}
							}
						}
						else if (codigo == "29")
						{
							return "29-Valor do Desconto Maior ou Igual ao Valor do Título";
						}
					}
					else if (codigo == "A9")
					{
						return "A9-Não autoriza pagamento parcial";
					}
				}
				else if (num <= 2547370491U)
				{
					if (num != 2530592872U)
					{
						if (num == 2547370491U)
						{
							if (codigo == "A5")
							{
								return "A5-Registro Rejeitado - Título já Liquidado";
							}
						}
					}
					else if (codigo == "A4")
					{
						return "A4-Sacado DDA";
					}
				}
				else if (num != 2548424010U)
				{
					if (num == 2564148110U)
					{
						if (codigo == "A6")
						{
							return "A6-Código do Convenente Inválido ou Encerrado";
						}
					}
				}
				else if (codigo == "78")
				{
					return "78-Data Inferior ou Igual ao Vencimento para Débito Automático";
				}
			}
			else if (num <= 2580925729U)
			{
				if (num != 2565201629U)
				{
					if (num != 2567026010U)
					{
						if (num == 2580925729U)
						{
							if (codigo == "A7")
							{
								return "A7-Título se já encontra na situação Pretendida";
							}
						}
					}
					else if (codigo == "89")
					{
						return "89-e-mail/SMS devolvido - endereço de e-mail ou número do celular incorreto";
					}
				}
				else if (codigo == "79")
				{
					return "79-Data Juros de Mora Inválido";
				}
			}
			else if (num <= 2614480967U)
			{
				if (num != 2583803629U)
				{
					if (num == 2614480967U)
					{
						if (codigo == "A1")
						{
							return "A1-Rejeição da alteração do número controle do participante";
						}
					}
				}
				else if (codigo == "88")
				{
					return "88-e-mail Lido";
				}
			}
			else if (num != 2631258586U)
			{
				if (num == 2648036205U)
				{
					if (codigo == "A3")
					{
						return "A3-Rejeição da alteração dos dados do sacador/avalista";
					}
				}
			}
			else if (codigo == "A2")
			{
				return "A2-Rejeição da alteração dos dados do sacado";
			}
			return "";
		}

		// Token: 0x04000650 RID: 1616
		private int _dacBoleto;

		// Token: 0x04000651 RID: 1617
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

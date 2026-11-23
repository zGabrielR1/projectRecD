using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000164 RID: 356
	internal sealed class BancoBrasil : AbstractBanco, IBanco
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00046D4A File Offset: 0x00044F4A
		internal BancoBrasil()
		{
			this.Codigo = 1;
			this.Digito = "9";
			this.Nome = "Banco do Brasil";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00046D8C File Offset: 0x00044F8C
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00046DCC File Offset: 0x00044FCC
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}-", boleto.Cedente.ContaBancaria.Agencia) + string.Format("{0}/", boleto.Cedente.ContaBancaria.DigitoAgencia) + string.Format("{0}-", Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 12)) + string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture));
			if (string.IsNullOrEmpty(boleto.Carteira))
			{
				throw new NotImplementedException("Carteira não informada. Utilize a carteira 16, 17, 17-019, 17-027, 17-035, 17-043, 17-051, 17-067, 17-140, 17-159, 17-167, 18, 18-019, 18-027, 18-035 ou 18-140.");
			}
			if (!boleto.Carteira.Equals("16") & !boleto.Carteira.Equals("17") & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("019")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("027")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("035")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("043")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("051")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("067")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("140")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("159")) & (!boleto.Carteira.Equals("17") || !boleto.Variacao.Equals("167")) & !boleto.Carteira.Equals("18") & (!boleto.Carteira.Equals("18") || !boleto.Variacao.Equals("019")) & (!boleto.Carteira.Equals("18") || !boleto.Variacao.Equals("027")) & (!boleto.Carteira.Equals("18") || !boleto.Variacao.Equals("035")) & (!boleto.Carteira.Equals("18") || !boleto.Variacao.Equals("140")))
			{
				throw new NotImplementedException("Carteira não informada. Utilize a carteira 16, 17, 17-019, 17-027, 17-035, 17-043, 17-051, 17-067, 17-140, 17-159, 17-167, 18, 18-019, 18-027, 18-035 ou 18-140.");
			}
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (boleto.Carteira.Equals("16") || boleto.Carteira.Equals("18"))
			{
				if (boleto.FormaCadastramento.Valido && boleto.FormaCadastramento.Codigo == 2)
				{
					if (boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture).Length != 6)
					{
						throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio deve ser de 6 posições", boleto.Carteira));
					}
					boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
				}
				else
				{
					BancoBrasil.FormatarNossoNumeroModalidadeDiferenteDe21(boleto);
				}
			}
			if (boleto.Carteira.Equals("17"))
			{
				BancoBrasil.FormatarNossoNumeroModalidadeDiferenteDe21(boleto);
			}
			if (boleto.Cedente.ContaBancaria.Agencia.Length > 4)
			{
				throw new NotImplementedException("A quantidade de dígitos da Agência " + boleto.Cedente.ContaBancaria.Agencia + ", deve ser de 4 números.");
			}
			if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
			{
				boleto.Cedente.ContaBancaria.Agencia = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length > 8)
			{
				throw new NotImplementedException("A quantidade de dígitos da Conta " + boleto.Cedente.ContaBancaria.Conta + ", deve ser de 8 números.");
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length < 8)
			{
				boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 8);
			}
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Banco do Brasil";
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000472D8 File Offset: 0x000454D8
		private static void FormatarNossoNumeroModalidadeDiferenteDe21(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			switch (boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture).Length)
			{
			case 4:
				if (boleto.NossoNumero.Length > 7)
				{
					throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade diferente de 21, a quantidade máxima são de 12 posições para o nosso número", boleto.Carteira));
				}
				BancoBrasil.FormatarNossoNumeroConvenio4Posicoes(boleto);
				return;
			case 6:
				if (boleto.NossoNumero.Length > 5)
				{
					throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo de modalidade diferente de 21, a quantidade máxima são de 12 posições para o nosso número", boleto.Carteira));
				}
				BancoBrasil.FormatarNossoNumeroConvenio6Posicoes(boleto);
				return;
			case 7:
				BancoBrasil.FormatarNossoNumeroConvenio7Posicoes(boleto);
				return;
			}
			throw new NotImplementedException("O número do convênio deve ter 4, 6 ou 7 posições");
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00047381 File Offset: 0x00045581
		private static void FormatarNossoNumeroConvenio4Posicoes(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
			boleto.DigitoNossoNumero = BancoBrasil.Mod11BancoBrasil(Utils.FormatCode(boleto.NossoNumero, 7));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000473C1 File Offset: 0x000455C1
		private static void FormatarNossoNumeroConvenio6Posicoes(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
			boleto.DigitoNossoNumero = BancoBrasil.Mod11BancoBrasil(Utils.FormatCode(boleto.NossoNumero, 5));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00047401 File Offset: 0x00045601
		private static void FormatarNossoNumeroConvenio7Posicoes(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0004742C File Offset: 0x0004562C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 10);
			string text2 = string.Format("{0}{1}{2}{3}", new object[]
			{
				Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3),
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto),
				text
			});
			if (boleto.Carteira.Equals("16") || boleto.Carteira.Equals("18"))
			{
				if (boleto.FormaCadastramento.Valido && boleto.FormaCadastramento.Codigo == 2)
				{
					if (boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture).Length != 6)
					{
						throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio deve ser de 6 posições", boleto.Carteira));
					}
					boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}21", text2, boleto.Cedente.Convenio, boleto.NossoNumero);
				}
				else
				{
					BancoBrasil.FormatarCodigoBarrasModalidadeDiferenteDe21(boleto, text2);
				}
			}
			if (boleto.Carteira.Equals("17"))
			{
				BancoBrasil.FormatarCodigoBarrasModalidadeDiferenteDe21(boleto, text2);
			}
			this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000475D4 File Offset: 0x000457D4
		private static void FormatarCodigoBarrasModalidadeDiferenteDe21(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, string parteComumCodigoBarras)
		{
			switch (boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture).Length)
			{
			case 4:
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[]
				{
					parteComumCodigoBarras,
					Strings.Mid(boleto.NossoNumero, 1, 11),
					boleto.Cedente.ContaBancaria.Agencia,
					boleto.Cedente.ContaBancaria.Conta,
					boleto.Carteira
				});
				return;
			case 5:
				break;
			case 6:
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[]
				{
					parteComumCodigoBarras,
					Strings.Mid(boleto.NossoNumero, 1, 11),
					boleto.Cedente.ContaBancaria.Agencia,
					boleto.Cedente.ContaBancaria.Conta,
					boleto.Carteira
				});
				return;
			case 7:
				boleto.CodigoBarra.Codigo = string.Format("{0}000000{1}{2}", parteComumCodigoBarras, boleto.NossoNumero, boleto.Carteira);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000476F8 File Offset: 0x000458F8
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Strings.Mid(boleto.CodigoBarra.Codigo, 20, 25);
			string text2 = Strings.Left(boleto.CodigoBarra.Codigo, 4) + Strings.Mid(text, 1, 5);
			text2 += AbstractBanco.Mod10(text2).ToString(CultureInfo.InvariantCulture);
			text2 = Strings.Mid(text2, 1, 5) + "." + Strings.Mid(text2, 6, 5);
			string text3 = Strings.Mid(text, 6, 10);
			text3 += AbstractBanco.Mod10(text3).ToString(CultureInfo.InvariantCulture);
			text3 = Strings.Mid(text3, 1, 5) + "." + Strings.Mid(text3, 6, 6);
			string text4 = Strings.Mid(text, 16, 10);
			int num = AbstractBanco.Mod10(text4);
			text4 += num;
			text4 = Strings.Mid(text4, 1, 5) + "." + Strings.Mid(text4, 6, 6);
			string text5 = Strings.Mid(boleto.CodigoBarra.Codigo, 5, 1);
			long num2 = Convert.ToInt64(Strings.Mid(boleto.CodigoBarra.Codigo, 6, 14));
			string text6 = ((num2 == 0L) ? "000" : num2.ToString(CultureInfo.InvariantCulture));
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text2, "  ", text3, "  ", text4, "  ", text5, "  ", text6 });
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00047884 File Offset: 0x00045A84
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			if (boleto.Carteira.Equals("16") || boleto.Carteira.Equals("18"))
			{
				boleto.NossoNumero = string.Format("{0}", boleto.NossoNumero);
				return;
			}
			if (boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture).Length == 6)
			{
				boleto.NossoNumero = string.Format("{0}/{1}", boleto.Carteira, boleto.NossoNumero);
				return;
			}
			boleto.NossoNumero = string.Format("{0}", boleto.NossoNumero);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0004791C File Offset: 0x00045B1C
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
					text = this.GerarHeaderRemessaCnab400(numeroConvenio.ToInt(), cedente, numeroArquivoRemessa);
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

		// Token: 0x0600065E RID: 1630 RVA: 0x0004799C File Offset: 0x00045B9C
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text5;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 10);
				string text3 = new string(' ', 29);
				string text4 = "00100000         ";
				if (cedente.Cpfcnpj.Length <= 11)
				{
					text4 += "1";
				}
				else
				{
					text4 += "2";
				}
				text4 += Utils.FitStringLength(cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 9, 9, '0', 0, true, true, true);
				text4 += "0014";
				text4 += Utils.FitStringLength(cedente.Carteira.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(cedente.Variacao.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
				text4 += "  ";
				text4 += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
				text4 += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text4 += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
				text4 += " ";
				text4 += Utils.FitStringLength(cedente.Nome.ToUpper(), 30, 30, ' ', 0, true, true, false);
				text4 += Utils.FitStringLength("BANCO DO BRASIL S.A.", 30, 30, ' ', 0, true, true, false);
				text4 += text2;
				text4 += "1";
				text4 += DateTime.Now.ToString("ddMMyyyy");
				text4 += DateTime.Now.ToString("HHmmss");
				text4 += Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text4 += "000";
				text4 += "00000";
				text4 += text;
				text4 += text;
				text4 += text3;
				text4 = Utils.SubstituiCaracteresEspeciais(text4);
				text5 = text4;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE ARQUIVO do arquivo de REMESSA.", ex);
			}
			return text5;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00047C30 File Offset: 0x00045E30
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = "TESTE  ";
				if (cedente.FinalidadeArquivoRemessaRetorno == FinalidadeArquivoRemessaRetorno.Producao)
				{
					text = "REMESSA";
				}
				string text2 = new string(' ', 22);
				string text3 = new string(' ', 258);
				text4 = Utils.SubstituiCaracteresEspeciais("01" + text + "01COBRANCA       " + Utils.FitStringLength(cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(4), 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false) + "000000" + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "001BANCODOBRASIL  " + DateTime.Now.ToString("ddMMyy") + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true) + text2 + Utils.FitStringLength(numeroConvenio.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true) + text3 + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text4;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00047DCC File Offset: 0x00045FCC
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
					text = BancoBrasil.GerarHeaderLoteRemessaCnab240(cedente, numeroArquivoRemessa);
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

		// Token: 0x06000661 RID: 1633 RVA: 0x00047E44 File Offset: 0x00046044
		private static string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 40);
				string text2 = new string(' ', 33);
				string text3 = "00100011R01  ";
				text3 += "000";
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
				text3 += Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 9, 9, '0', 0, true, true, true);
				text3 += "0014";
				text3 += Utils.FitStringLength(cedente.Carteira.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.Variacao.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
				FinalidadeArquivoRemessaRetorno finalidadeArquivoRemessaRetorno = cedente.FinalidadeArquivoRemessaRetorno;
				if (finalidadeArquivoRemessaRetorno == FinalidadeArquivoRemessaRetorno.Teste)
				{
					text3 += "TS";
				}
				else
				{
					text3 += "  ";
				}
				text3 += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text3 += " ";
				text3 += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false);
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

		// Token: 0x06000662 RID: 1634 RVA: 0x000480A4 File Offset: 0x000462A4
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
					text = this.GerarDetalheRemessaCnab240(boleto, numeroRegistro, tipoArquivo);
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

		// Token: 0x06000663 RID: 1635 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0004811C File Offset: 0x0004631C
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = new string(' ', 40);
				string text2 = "7";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text2 += "01";
				}
				else
				{
					text2 += "02";
				}
				text2 += Utils.FitStringLength(boleto.Cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(4), 4, 4, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.Cedente.Convenio, 7, 7, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.Cedente.Convenio.PadLeft(7, '0') + (boleto.NossoNumeroSemFormatacao + boleto.DigitoNossoNumero).PadLeft(10, '0'), 17, 17, '0', 0, true, true, true);
				text2 += "0000       ";
				text2 += Utils.FitStringLength(boleto.Variacao, 3, 3, '0', 0, true, true, true);
				text2 += "0000000";
				if (boleto.Carteira.Equals("11") || boleto.Carteira.Equals("17"))
				{
					if (boleto.TipoCobranca.Codigo.Equals(1))
					{
						text2 += "     ";
					}
					else if (boleto.TipoCobranca.Codigo.Equals(4))
					{
						text2 += "04DSC";
					}
					else if (boleto.TipoCobranca.Codigo.Equals(2))
					{
						text2 += "02VIN";
					}
				}
				else
				{
					text2 += "     ";
				}
				text2 += Utils.FitStringLength(boleto.Carteira, 2, 2, '0', 0, true, true, true);
				text2 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text2 += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text2 += boleto.DataVencimento.ToString("ddMMyy");
				text2 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text2 += "001";
				text2 += "0000 ";
				text2 += Utils.FitStringLength(new EspecieDocumento(boleto.Banco.Codigo, boleto.EspecieDocumento.Codigo, TipoArquivo.CNAB400).Codigo, 2, 2, '0', 0, true, true, true);
				text2 += boleto.Aceite;
				text2 += boleto.DataDocumento.ToString("ddMMyy");
				int num = boleto.Instrucoes.Count;
				if (num != 0)
				{
					if (num != 1)
					{
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text2 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
					else
					{
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text2 += (boleto.SolicitacaoDeBaixa ? "44" : "00");
					}
				}
				else
				{
					text2 += (boleto.SolicitacaoDeBaixa ? "4400" : "0000");
				}
				text2 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text2 += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text2 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text2 += "0000000000000";
				text2 += "0000000000000";
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text2 += "01";
				}
				else
				{
					text2 += "02";
				}
				text2 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text2 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 37, 37, ' ', 0, true, true, false);
				text2 += "   ";
				text2 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text2 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text2 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text2 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text2 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text2 += text;
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num <= 25)
					{
						switch (num)
						{
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 10:
						case 15:
							break;
						case 7:
						case 8:
						case 9:
						case 11:
						case 12:
						case 13:
						case 14:
							goto IL_07BB;
						default:
							if (num != 20 && num != 25)
							{
								goto IL_07BB;
							}
							break;
						}
					}
					else if (num != 30 && num != 45 && num != 88)
					{
						goto IL_07BB;
					}
					text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0876;
					IL_07BB:
					num = boleto.Instrucoes[1].Codigo;
					if (num <= 25)
					{
						switch (num)
						{
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 10:
						case 15:
							break;
						case 7:
						case 8:
						case 9:
						case 11:
						case 12:
						case 13:
						case 14:
							goto IL_086A;
						default:
							if (num != 20 && num != 25)
							{
								goto IL_086A;
							}
							break;
						}
					}
					else if (num != 30 && num != 45 && num != 88)
					{
						goto IL_086A;
					}
					text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0876;
					IL_086A:
					text2 += "00";
					IL_0876:;
				}
				catch (Exception)
				{
					text2 += "00";
				}
				text2 += " ";
				text2 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text2 = Utils.SubstituiCaracteresEspeciais(text2);
				text3 = text2;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text3;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00048A30 File Offset: 0x00046C30
		public override string GerarDetalheDadosMultaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = string.Empty;
				if (boleto.SolicitacaoDeBaixa || boleto.AlteracaoVencimento)
				{
					text2 = text;
				}
				else
				{
					base.GerarDetalheDadosMultaRemessa(boleto, numeroRegistro, tipoArquivo);
					switch (tipoArquivo)
					{
					case TipoArquivo.CNAB240:
						text = this.GerarDetalheDadosMultaRemessaCnab240();
						break;
					case TipoArquivo.CNAB400:
						text = this.GerarDetalheDadosMultaRemessaCnab400(boleto, numeroRegistro, tipoArquivo);
						break;
					case TipoArquivo.Outro:
						throw new Exception("Tipo de arquivo inexistente.");
					}
					text2 = text;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE com dados do pagador/avalista do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheDadosMultaRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00048ABC File Offset: 0x00046CBC
		public string GerarDetalheDadosMultaRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = new string(' ', 372);
				base.GerarDetalheDadosPagadorAvalistaRemessa(boleto, numeroRegistro, tipoArquivo);
				text2 = Utils.SubstituiCaracteresEspeciais("5" + "99" + "2" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyy"), 6, 6, '0', 0, true, true, false) + Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 12, 12, '0', 0, true, true, true) + text + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00048BA0 File Offset: 0x00046DA0
		public override string GerarDetalheSegmentoPRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, string numeroConvenio)
		{
			string text2;
			try
			{
				string text = "00100013";
				text += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text += "P ";
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += " ";
				if (boleto.Cedente.Convenio.Length == 7)
				{
					text += Utils.FitStringLength(boleto.NossoNumero, 20, 20, ' ', 0, true, true, false);
				}
				else
				{
					text += Utils.FitStringLength(boleto.NossoNumero + boleto.DigitoNossoNumero, 20, 20, ' ', 0, true, true, false);
				}
				if (boleto.TipoCobranca.Codigo.Equals(1) && boleto.Carteira.Equals("17"))
				{
					text += "7";
				}
				else
				{
					text += Utils.FitStringLength(boleto.TipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				}
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
						case 1:
							text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
							if ((boleto.Instrucoes[0].QuantidadeDias >= 6 && boleto.Instrucoes[0].QuantidadeDias <= 29) || boleto.Instrucoes[0].QuantidadeDias == 35 || boleto.Instrucoes[0].QuantidadeDias == 40)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "06";
							}
							break;
						case 2:
							text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
							if (boleto.Instrucoes[0].QuantidadeDias >= 3 && boleto.Instrucoes[0].QuantidadeDias <= 5)
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							}
							else
							{
								text += "03";
							}
							break;
						case 3:
							text += "3";
							text += "00";
							break;
						default:
							if (num != 8)
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num != 1)
								{
									if (num != 2)
									{
										if (num != 8)
										{
											text += "3";
											text += "00";
										}
										else
										{
											text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
											if (boleto.Instrucoes[1].QuantidadeDias >= 1 && boleto.Instrucoes[0].QuantidadeDias <= 99)
											{
												text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
											}
											else
											{
												text += Utils.FitStringLength(0.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
											}
										}
									}
									else
									{
										text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
										if (boleto.Instrucoes[1].QuantidadeDias >= 3 && boleto.Instrucoes[1].QuantidadeDias <= 5)
										{
											text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
										}
										else
										{
											text += "03";
										}
									}
								}
								else
								{
									text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
									if ((boleto.Instrucoes[1].QuantidadeDias >= 6 && boleto.Instrucoes[1].QuantidadeDias <= 29) || boleto.Instrucoes[1].QuantidadeDias == 35 || boleto.Instrucoes[1].QuantidadeDias == 40)
									{
										text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									}
									else
									{
										text += "06";
									}
								}
							}
							else
							{
								text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
								if (boleto.Instrucoes[0].QuantidadeDias >= 1 && boleto.Instrucoes[0].QuantidadeDias <= 99)
								{
									text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								}
								else
								{
									text += Utils.FitStringLength(0.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
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
				text += "0000090000000000 ";
				text = Utils.SubstituiCaracteresEspeciais(text);
				text2 = text;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000495D0 File Offset: 0x000477D0
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text5;
			try
			{
				string text = new string('0', 16);
				string text2 = new string(' ', 28);
				string text3 = new string(' ', 40);
				string text4 = "00100013";
				text4 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true);
				text4 += "Q ";
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

		// Token: 0x0600066A RID: 1642 RVA: 0x0004987C File Offset: 0x00047A7C
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = new string(' ', 9);
				text3 = Utils.SubstituiCaracteresEspeciais("00100013" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "000000000000000000000000" + "000000000000000000000000" + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + "0000000000000000" + " " + "000000000000" + "  " + "0" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000499E0 File Offset: 0x00047BE0
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
				throw new Exception("Erro durante a geração do TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00049A58 File Offset: 0x00047C58
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

		// Token: 0x0600066E RID: 1646 RVA: 0x00049AC4 File Offset: 0x00047CC4
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text2;
			try
			{
				string text = new string(' ', 217);
				text2 = Utils.SubstituiCaracteresEspeciais("00100015         " + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00049B30 File Offset: 0x00047D30
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais("00199999         000001" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + "000000" + text);
			}
			catch (Exception ex)
			{
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00049BA4 File Offset: 0x00047DA4
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(146, 6));
				int num3 = Utils.ToInt32(registro.Substring(175, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##")),
					DataVencimento = Utils.ToDateTime(num2.ToString("##-##-##")),
					DataCredito = Utils.ToDateTime(num3.ToString("##-##-##")),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					Agencia = Utils.ToInt32(registro.Substring(17, 4)),
					Conta = Utils.ToInt32(registro.Substring(22, 8)),
					DacConta = Utils.ToInt32(registro.Substring(30, 1)),
					Carteira = registro.Substring(106, 2),
					NossoNumero = registro.Substring(63, 17),
					DACNossoNumero = Utils.ToInt32(registro.Substring(79, 1)),
					UsoEmpresa = registro.Substring(38, 25),
					NumeroDocumento = registro.Substring(116, 10)
				};
				decimal num4 = Convert.ToInt64(registro.Substring(152, 13));
				detalheRetornoCNAB.ValorTitulo = num4 / 100m;
				detalheRetornoCNAB.BancoCobrador = Utils.ToInt32(registro.Substring(165, 3));
				detalheRetornoCNAB.AgenciaCobradora = Utils.ToInt32(registro.Substring(168, 4));
				detalheRetornoCNAB.Especie = Utils.ToInt32(registro.Substring(172, 1));
				decimal num5 = Convert.ToUInt64(registro.Substring(188, 13));
				detalheRetornoCNAB.OutrasDespesas = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(214, 13));
				detalheRetornoCNAB.IOF = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(227, 13));
				detalheRetornoCNAB.ValorAbatimento = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(240, 13));
				detalheRetornoCNAB.Descontos = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num9 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				decimal num10 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num10 / 100m;
				decimal num11 = Convert.ToUInt64(registro.Substring(181, 7));
				detalheRetornoCNAB.TarifaCobranca = num11 / 100m;
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				if (codigoOcorrencia <= 15)
				{
					switch (codigoOcorrencia)
					{
					case 2:
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
						goto IL_0357;
					case 3:
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
						goto IL_0357;
					case 4:
						goto IL_0350;
					case 5:
					case 6:
					case 7:
					case 8:
						break;
					default:
						if (codigoOcorrencia != 15)
						{
							goto IL_0350;
						}
						break;
					}
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_0357;
				}
				if (codigoOcorrencia == 85)
				{
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.InclusaoNegativacao;
					goto IL_0357;
				}
				if (codigoOcorrencia == 86)
				{
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.ExclusaoNegativacao;
					goto IL_0357;
				}
				IL_0350:
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_0357:
				detalheRetornoCNAB.NumeroSequencial = Utils.ToInt32(registro.Substring(394, 6));
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00049F54 File Offset: 0x00048154
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = base.LerDetalheSegmentoTRetornoCNAB240(registro);
			if (detalheSegmentoTRetornoCNAB.IdCodigoMovimento == 61)
			{
				detalheSegmentoTRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoLiquidado;
			}
			return detalheSegmentoTRetornoCNAB;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00049F7C File Offset: 0x0004817C
		internal static string Mod11BancoBrasil(string value)
		{
			int num = 0;
			int num2 = 9;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				num += int.Parse(value[i].ToString(CultureInfo.InvariantCulture)) * num2;
				if (num2 == 2)
				{
					num2 = 9;
				}
				else
				{
					num2--;
				}
			}
			int num3 = num % 11;
			string text;
			if (num3 != 0)
			{
				if (num3 == 10)
				{
					text = "X";
				}
				else
				{
					text = num3.ToString(CultureInfo.InvariantCulture);
				}
			}
			else
			{
				text = "0";
			}
			return text;
		}

		// Token: 0x04000646 RID: 1606
		private int _dacBoleto;

		// Token: 0x04000647 RID: 1607
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

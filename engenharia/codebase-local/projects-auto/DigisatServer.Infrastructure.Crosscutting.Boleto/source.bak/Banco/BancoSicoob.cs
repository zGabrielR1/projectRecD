using System;
using System.Globalization;
using System.Text;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000173 RID: 371
	internal sealed class BancoSicoob : AbstractBanco, IBanco
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00068E74 File Offset: 0x00067074
		internal BancoSicoob()
		{
			this.Nome = "Sicoob";
			this.Codigo = 756;
			this.Digito = "0";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00068EC4 File Offset: 0x000670C4
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00068F04 File Offset: 0x00067104
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormatoAgenciaCodigoCedente = string.Format("{0}/{1}-{2}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4), Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 6), boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture));
				if (Utils.ToString(boleto.NossoNumero) == string.Empty)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 7)
				{
					throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 7 números.");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 7)
				{
					boleto.NossoNumero = Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true);
				}
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = "Preferencialmente na COOPERATIVA " + boleto.Cedente.ContaBancaria.Agencia.ToString(CultureInfo.InvariantCulture) + " ou na Rede bancária";
				}
				this.FormataNossoNumero(boleto);
				this.FormataCodigoBarra(boleto);
				this.FormataLinhaDigitavel(boleto);
				this.FormataNumeroDocumento(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000690A8 File Offset: 0x000672A8
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			int num = 0;
			string text = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 4, true);
			string text2 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), "0", 9, true) + Utils.FormatCode(boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), "0", 1);
			string nossoNumero = boleto.NossoNumero;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4 - text.Length; i++)
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(text);
			for (int j = 0; j < 10 - text2.Length; j++)
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(text2);
			for (int k = 0; k < 7 - nossoNumero.Length; k++)
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(nossoNumero);
			for (int l = 0; l < 21; l++)
			{
				num += (int)(Convert.ToInt16(stringBuilder.ToString().Substring(l, 1)) * Convert.ToInt16("319731973197319731973".Substring(l, 1)));
			}
			int num2 = num % 11;
			int num3;
			if (num2 == 1 || num2 == 0)
			{
				num3 = 0;
			}
			else
			{
				num3 = 11 - num2;
			}
			boleto.NossoNumero = boleto.NossoNumero;
			boleto.DigitoNossoNumero = num3.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0006922C File Offset: 0x0006742C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = ((boleto.Cedente.TipoSolucao == TipoSolucao.SicoobNovaCobranca) ? this.MontarCampoLivreNovaCobranca(boleto) : this.MontarCampoLivreCobrancaTradicional(boleto));
			this._dacBoleto = BancoSicoob.CalcularDacCodigoBarra(text);
			boleto.CodigoBarra.Codigo = text.Substring(0, 4) + this._dacBoleto + text.Substring(4);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00069290 File Offset: 0x00067490
		private string MontarCampoLivreCobrancaTradicional(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FitStringLength(boleto.Cedente.Codigo + boleto.Cedente.DigitoCedente, 7, 7, '0', 0, true, true, true);
			return string.Concat(new string[]
			{
				this.MontarCampoObrigatorioCodigoBarra(boleto),
				boleto.Carteira,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 4, true),
				Utils.FitStringLength(boleto.TipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true),
				text,
				boleto.NossoNumero,
				boleto.DigitoNossoNumero,
				boleto.NumeroParcela.ToStringLeftZero(3)
			});
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00069358 File Offset: 0x00067558
		private string MontarCampoLivreNovaCobranca(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Concat(new string[]
			{
				this.MontarCampoObrigatorioCodigoBarra(boleto),
				"9",
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 4, true),
				Utils.FitStringLength(boleto.Cedente.Contrato.ToStringEmptyWhenNull(), 9, 9, '0', 0, true, true, true),
				Utils.FitStringLength(boleto.NossoNumero, 8, 8, '0', 0, true, true, true),
				boleto.DigitoNossoNumero,
				"01"
			});
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000693EC File Offset: 0x000675EC
		private string MontarCampoObrigatorioCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Concat(new object[]
			{
				Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3),
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture),
				Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10)
			});
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00069480 File Offset: 0x00067680
		private static int CalcularDacCodigoBarra(string codigoValidacao)
		{
			int num = 2;
			int num2 = 0;
			for (int i = codigoValidacao.Length - 1; i >= 0; i--)
			{
				num2 += (int)Convert.ToInt16(codigoValidacao.Substring(i, 1)) * num;
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
			return num4;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000694E0 File Offset: 0x000676E0
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

		// Token: 0x060007DE RID: 2014 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00069664 File Offset: 0x00067864
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

		// Token: 0x060007E0 RID: 2016 RVA: 0x000696F0 File Offset: 0x000678F0
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + "0" + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("SICOOB", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + "081" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00069930 File Offset: 0x00067B30
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text3;
			try
			{
				string text = new string(' ', 287);
				string text2 = new string(' ', 7);
				text3 = Utils.SubstituiCaracteresEspeciais("0" + "1" + "REMESSA" + "01" + "COBRANCA" + text2 + Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true) + Utils.FitStringLength(Convert.ToString(cedente.Codigo), 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(Convert.ToString(cedente.DigitoCedente), 1, 1, '0', 0, true, true, true) + Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength("756BANCOOBCED", 18, 18, ' ', 0, true, true, false) + DateTime.Now.ToString("ddMMyy") + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 7, 7, '0', 0, true, true, true) + text + "000001").Replace("COBRANCA", "COBRANÇA");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text3;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00069AD8 File Offset: 0x00067CD8
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

		// Token: 0x060007E3 RID: 2019 RVA: 0x00069B60 File Offset: 0x00067D60
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 40);
				string text3 = new string(' ', 33);
				text4 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "1" + "R" + "01" + "  " + "040" + " " + ((cedente.Cpfcnpj.Length <= 11) ? "1" : "2") + Utils.FitStringLength(cedente.Cpfcnpj, 15, 15, '0', 0, true, true, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + text2 + text2 + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true) + DateTime.Now.ToString("ddMMyyyy") + "00000000" + text3);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
			}
			return text4;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00069D7C File Offset: 0x00067F7C
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

		// Token: 0x060007E5 RID: 2021 RVA: 0x00069E00 File Offset: 0x00068000
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("CNAB240 não implementado.");
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00069E0C File Offset: 0x0006800C
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = "1";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text += "01";
				}
				else
				{
					text += "02";
				}
				text += Utils.FitStringLength(boleto.Cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 8, 8, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.Convenio.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text += Utils.FitStringLength(boleto.NossoNumero, 11, 11, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NumeroParcela.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text += "00";
				text += "   ";
				text += " ";
				text += Utils.FitStringLength(boleto.EspecieDocumento.Sigla.ToUpper(), 3, 3, ' ', 0, true, true, true);
				text += "000";
				text += "0";
				text += "00000";
				text += "0";
				text += "000000";
				text += "    ";
				text += Utils.FitStringLength(boleto.TipoEmissao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoCobranca.Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text += boleto.DataVencimento.ToString("ddMMyy");
				text += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text += "756";
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(new EspecieDocumento(boleto.Banco.Codigo, boleto.EspecieDocumento.Codigo, tipoArquivo).Codigo, 2, 2, '0', 0, true, true, true);
				text += ((boleto.Aceite == "A") ? "1" : "0");
				text += boleto.DataDocumento.ToString("ddMMyy");
				int num = boleto.Instrucoes.Count;
				if (num != 0)
				{
					if (num != 1)
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
					else
					{
						text += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text += "00";
					}
				}
				else
				{
					text += "0000";
				}
				text += Utils.FitStringLength(boleto.PercJurosMora.ToString("0.0000").Replace(",", ""), 6, 6, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.PercMulta.ToString("0.0000").Replace(",", ""), 6, 6, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.TipoDistribuicao.Codigo.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				if (boleto.ValorDesconto > 0m)
				{
					text += ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy"));
					text += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				}
				else
				{
					text += Utils.FormatCode("0", 19);
				}
				text = text + "9" + Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 12, 12, '0', 0, true, true, true);
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
				text += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 37, 37, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num <= 10)
					{
						switch (num)
						{
						case 3:
						case 4:
						case 5:
							break;
						default:
							if (num != 10)
							{
								goto IL_08BA;
							}
							break;
						}
					}
					else if (num != 15 && num != 20 && num != 99)
					{
						goto IL_08BA;
					}
					text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0940;
					IL_08BA:
					num = boleto.Instrucoes[1].Codigo;
					if (num <= 10)
					{
						switch (num)
						{
						case 3:
						case 4:
						case 5:
							break;
						default:
							if (num != 10)
							{
								goto IL_0934;
							}
							break;
						}
					}
					else if (num != 15 && num != 20 && num != 99)
					{
						goto IL_0934;
					}
					text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0940;
					IL_0934:
					text += "00";
					IL_0940:;
				}
				catch (Exception)
				{
					text += "00";
				}
				text += " ";
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

		// Token: 0x060007E7 RID: 2023 RVA: 0x0006A7E8 File Offset: 0x000689E8
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
				text += Utils.FitStringLength(boleto.NossoNumero, 9, 9, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.NumeroParcela.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
				text += "4";
				text += "     ";
				text += Utils.FitStringLength(boleto.Carteira.ToString(CultureInfo.InvariantCulture), 1, 1, '0', 0, true, true, true);
				text += "0";
				text += " ";
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
					text += "0";
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
							goto IL_05E7;
						case 7:
							text += "3";
							text += "00";
							goto IL_06D5;
						case 8:
							text += "8";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
							goto IL_06D5;
						default:
							if (num != 20 && num != 99)
							{
								goto IL_05E7;
							}
							break;
						}
						text += "1";
						text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						goto IL_06D5;
						IL_05E7:
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
								goto IL_06BD;
							case 8:
								text += "8";
								text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								goto IL_06D5;
							default:
								if (num != 15)
								{
									goto IL_06BD;
								}
								break;
							}
						}
						else if (num != 20 && num != 99)
						{
							goto IL_06BD;
						}
						text += "1";
						text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						goto IL_06D5;
						IL_06BD:
						text += "3";
						text += "00";
					}
					IL_06D5:;
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
						text += "0";
						text += "   ";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						if (num == 42 || num == 43)
						{
							text += "0";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
						}
						else
						{
							num = boleto.Instrucoes[1].Codigo;
							if (num == 42 || num == 43)
							{
								text += "0";
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
							}
							else
							{
								text += "0";
								text += "   ";
							}
						}
					}
				}
				catch (Exception)
				{
					text += "0";
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

		// Token: 0x060007E8 RID: 2024 RVA: 0x0006B084 File Offset: 0x00069284
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

		// Token: 0x060007E9 RID: 2025 RVA: 0x0006B338 File Offset: 0x00069538
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

		// Token: 0x060007EA RID: 2026 RVA: 0x0006B4D8 File Offset: 0x000696D8
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

		// Token: 0x060007EB RID: 2027 RVA: 0x0006B560 File Offset: 0x00069760
		public string GerarTrailerRemessaCnab240(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 205);
				text2 = Utils.SubstituiCaracteresEspeciais("75699999" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", 6) + Utils.FormatCode(numeroRegistro.ToString(), 6) + Utils.FormatCode("0", 6) + text);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0006B5F4 File Offset: 0x000697F4
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

		// Token: 0x060007ED RID: 2029 RVA: 0x0006B660 File Offset: 0x00069860
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "0001" + "5" + Utils.FormatCode("", " ", 9) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + Utils.FitStringLength(vltitulostotal.ToString("0.00").Replace(",", ""), 17, 17, '0', 0, true, true, true) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", "0", 6) + Utils.FormatCode("", "0", 17) + Utils.FormatCode("", " ", 8) + Utils.FormatCode("", " ", 117));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do TRAILER de lote do arquivo de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0006B800 File Offset: 0x00069A00
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

		// Token: 0x060007EF RID: 2031 RVA: 0x0006B8B8 File Offset: 0x00069AB8
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = base.LerDetalheSegmentoTRetornoCNAB240(registro);
			detalheSegmentoTRetornoCNAB.NossoNumero = registro.Substring(39, 7);
			return detalheSegmentoTRetornoCNAB;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0006B8D0 File Offset: 0x00069AD0
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

		// Token: 0x060007F1 RID: 2033 RVA: 0x0006BAD4 File Offset: 0x00069CD4
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

		// Token: 0x060007F2 RID: 2034 RVA: 0x00053BE5 File Offset: 0x00051DE5
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			return "Não informado no arquivo de retorno";
		}

		// Token: 0x04000667 RID: 1639
		private int _dacBoleto;

		// Token: 0x04000668 RID: 1640
		private const string CarteiraNovaCobranca = "9";

		// Token: 0x04000669 RID: 1641
		private const string ModalidadeNovaCobranca = "01";

		// Token: 0x0400066A RID: 1642
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

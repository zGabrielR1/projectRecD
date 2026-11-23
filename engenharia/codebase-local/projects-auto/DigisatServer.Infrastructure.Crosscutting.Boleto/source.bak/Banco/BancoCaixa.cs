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
	// Token: 0x02000166 RID: 358
	internal sealed class BancoCaixa : AbstractBanco, IBanco
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0004AA48 File Offset: 0x00048C48
		internal BancoCaixa()
		{
			this.Codigo = 104;
			this.Digito = "0";
			this.Nome = "Caixa Econômica Federal";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0004AAA0 File Offset: 0x00048CA0
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyyyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0004AAE0 File Offset: 0x00048CE0
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			TipoSolucao tipoSolucao = boleto.Cedente.TipoSolucao;
			if (tipoSolucao != TipoSolucao.Sigcb)
			{
				if (tipoSolucao == TipoSolucao.Sicob)
				{
					this.FormatoAgenciaCodigoCedente = string.Format("{0}.{1}.{2}-{3}", new object[]
					{
						Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4),
						boleto.Cedente.ContaBancaria.OperacaoConta,
						Utils.FormatCode(boleto.Cedente.Codigo, 8),
						boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture)
					});
				}
			}
			else
			{
				string text;
				string text2;
				if (string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente).Length == 6)
				{
					text = string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente);
					text2 = AbstractBanco.Mod11Base9(text).ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					text = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 6);
					text2 = Utils.FormatCode(boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 1);
				}
				this.FormatoAgenciaCodigoCedente = string.Format("{0}/{1}-{2}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4), text, text2);
			}
			this.InstrucaoSacAtendimento = "SAC CAIXA: 0800 726 0101 (informações, reclamações, sugestões e elogios) Para pessoas com deficiência auditiva ou de fala: 0800 726 2492 Ouvidoria: 0800 725 7474 caixa.gov.br";
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente na Caixa Econômica Federal";
			}
			tipoSolucao = boleto.Cedente.TipoSolucao;
			if (tipoSolucao != TipoSolucao.Sigcb)
			{
				if (tipoSolucao == TipoSolucao.Sicob)
				{
					if (boleto.Carteira.Equals("11"))
					{
						boleto.NossoNumero = Utils.FormatCode("0", 10);
						boleto.DigitoNossoNumero = "0";
					}
					else
					{
						this.FormatarNossoNumeroSicob(boleto);
					}
					this.FormataCodigoBarraSicob(boleto);
				}
			}
			else
			{
				if (boleto.FormaCadastramento.Codigo == 1 && boleto.TipoEmissao.Codigo == 1)
				{
					boleto.NossoNumero = Utils.FormatCode("0", 17);
					boleto.DigitoNossoNumero = "0";
				}
				else
				{
					this.FormatarNossoNumeroSigcb(boleto);
				}
				this.FormataCodigoBarraSigcb(boleto);
			}
			this.FormataNumeroDocumento(boleto);
			this.FormataLinhaDigitavel(boleto);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0004AD50 File Offset: 0x00048F50
		private void FormatarNossoNumeroSigcb(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			int num = 1;
			int codigo = boleto.TipoEmissao.Codigo;
			if (codigo != 1)
			{
				if (codigo == 2)
				{
					num = 4;
				}
			}
			else
			{
				num = 1;
			}
			boleto.NossoNumero = Utils.FormatCode(boleto.FormaCadastramento.Codigo.ToString(CultureInfo.InvariantCulture), "1", 1) + Utils.FormatCode(num.ToString(CultureInfo.InvariantCulture), "1", 1) + Utils.FormatCode(boleto.NossoNumero, 15);
			boleto.DigitoNossoNumero = AbstractBanco.Mod11Base9(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0004ADEC File Offset: 0x00048FEC
		public void FormataCodigoBarraSigcb(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = this.Codigo.ToString(CultureInfo.InvariantCulture);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text2 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text2 = Utils.FormatCode(text2, 10);
			string text3;
			string text4;
			if (string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente).Length == 6)
			{
				text3 = string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente);
				text4 = AbstractBanco.Mod11Base9(text3).ToString(CultureInfo.InvariantCulture);
			}
			else if (string.Format("{0}{1}", boleto.Cedente.Codigo, boleto.Cedente.DigitoCedente).Length == 8)
			{
				text3 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture).CopiarTexto(6), 6);
				text4 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture).CopiarTexto(6, 7, true), 1);
			}
			else
			{
				text3 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 6);
				text4 = Utils.FormatCode(boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture), 1);
			}
			string text5 = boleto.NossoNumero.Substring(0, 1);
			string text6 = boleto.NossoNumero.Substring(2, 3);
			string text7 = boleto.NossoNumero.Substring(5, 3);
			string text8 = boleto.NossoNumero.Substring(8, 9);
			string text9 = string.Format("{0}{1}{2}{3}{4}{5}{6}", new object[] { text3, text4, text6, text5, text7, "4", text8 });
			string text10 = AbstractBanco.Mod11Base9(text9).ToString(CultureInfo.InvariantCulture);
			string text11 = string.Format("{0}{1}", text9, text10);
			string text12 = AbstractBanco.Mod11(string.Format("{0}{1}{2}{3}{4}", new object[] { text, "9", num, text2, text11 }), 9).ToString(CultureInfo.InvariantCulture);
			this._dacBoleto = text12;
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}", new object[] { text, "9", text12, num, text2, text11 });
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0004B098 File Offset: 0x00049298
		private void FormatarNossoNumeroSicob(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			int codigo = boleto.FormaCadastramento.Codigo;
			if (codigo != 1)
			{
				if (codigo == 2)
				{
					boleto.NossoNumero = "82" + Utils.FormatCode(boleto.NossoNumero, 8);
				}
			}
			else
			{
				boleto.NossoNumero = "9" + Utils.FormatCode(boleto.NossoNumero, 9);
			}
			boleto.DigitoNossoNumero = AbstractBanco.Mod11Base9(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0004B114 File Offset: 0x00049314
		public void FormataCodigoBarraSicob(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = this.Codigo.ToString(CultureInfo.InvariantCulture);
			long num = AbstractBanco.FatorVencimento(boleto);
			string text2 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text2 = Utils.FormatCode(text2, 10);
			string text3 = Utils.FitStringLength(Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Cedente.ContaBancaria.OperacaoConta, 3, 3, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Cedente.Codigo, 8, 8, '0', 0, true, true, true), 15, 15, '0', 0, true, true, true);
			string text4 = string.Format("{0}{1}", boleto.NossoNumero, text3);
			string text5 = AbstractBanco.Mod11(string.Format("{0}{1}{2}{3}{4}", new object[] { text, "9", num, text2, text4 }), 9).ToString(CultureInfo.InvariantCulture);
			this._dacBoleto = text5;
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}", new object[] { text, "9", this._dacBoleto, num, text2, text4 });
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0004B282 File Offset: 0x00049482
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0004B2AC File Offset: 0x000494AC
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
			string dacBoleto = this._dacBoleto;
			string text7 = string.Format("{0} ", dacBoleto);
			text = boleto.CodigoBarra.Codigo.Substring(5, 4);
			text2 = boleto.CodigoBarra.Codigo.Substring(9, 10);
			string text8 = string.Format("{0}{1}", text, text2);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text4, text5, text6, text7, text8 });
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00030B9C File Offset: 0x0002ED9C
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0004B42C File Offset: 0x0004962C
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

		// Token: 0x0600068C RID: 1676 RVA: 0x0004B4C8 File Offset: 0x000496C8
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

		// Token: 0x0600068D RID: 1677 RVA: 0x0004B7B8 File Offset: 0x000499B8
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

		// Token: 0x0600068E RID: 1678 RVA: 0x0003401C File Offset: 0x0003221C
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0004BAF4 File Offset: 0x00049CF4
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

		// Token: 0x06000690 RID: 1680 RVA: 0x0004BB7C File Offset: 0x00049D7C
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

		// Token: 0x06000691 RID: 1681 RVA: 0x0004BE64 File Offset: 0x0004A064
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

		// Token: 0x06000692 RID: 1682 RVA: 0x0003401C File Offset: 0x0003221C
		private string GerarHeaderLoteRemessaCnab400()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0004C130 File Offset: 0x0004A330
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

		// Token: 0x06000694 RID: 1684 RVA: 0x0004C170 File Offset: 0x0004A370
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

		// Token: 0x06000695 RID: 1685 RVA: 0x0004CD8C File Offset: 0x0004AF8C
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

		// Token: 0x06000696 RID: 1686 RVA: 0x0004D8AC File Offset: 0x0004BAAC
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

		// Token: 0x06000697 RID: 1687 RVA: 0x0004D8EC File Offset: 0x0004BAEC
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

		// Token: 0x06000698 RID: 1688 RVA: 0x0004DA84 File Offset: 0x0004BC84
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

		// Token: 0x06000699 RID: 1689 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			return this.GerarDetalheSegmentoQRemessaCnab240(boleto, numeroRegistro, tipoArquivo);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0004DBF4 File Offset: 0x0004BDF4
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

		// Token: 0x0600069B RID: 1691 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			return this.GerarTrailerLoteRemessaCnab240(numeroRegistro, quantidadeBoletos, vltitulostotal);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0004DEC4 File Offset: 0x0004C0C4
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

		// Token: 0x0600069D RID: 1693 RVA: 0x0004E03C File Offset: 0x0004C23C
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			return this.GerarTrailerArquivoRemessaCnab240(numeroRegistro);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0004E048 File Offset: 0x0004C248
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

		// Token: 0x04000649 RID: 1609
		private string _dacBoleto = string.Empty;

		// Token: 0x0400064A RID: 1610
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

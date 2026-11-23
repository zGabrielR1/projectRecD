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
	// Token: 0x02000169 RID: 361
	internal sealed class BancoBanese : AbstractBanco, IBanco
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00051A10 File Offset: 0x0004FC10
		internal BancoBanese()
		{
			this.Nome = "Banese";
			this.Codigo = 47;
			this.Digito = "7";
			this.DescricaoBeneficiarioSacadorAvalista = "Beneficiário final";
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00051A44 File Offset: 0x0004FC44
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Concat(new string[]
				{
					"COB.240.",
					string.Format("{0}.", this.CodigoCedenteParaComporNomeDoArquivoRemessa),
					string.Format("{0:yyyyMMdd}.", DateTime.Now),
					string.Format("{0}.", this.NumeroSequencialArquivo.ToStringLeftZero(5)),
					string.Format("{0}", this.ConvenioParaComporNomeDoArquivoRemessa),
					".REM"
				});
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00051AC4 File Offset: 0x0004FCC4
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.CodigoCedenteParaComporNomeDoArquivoRemessa = boleto.CodigoExclusivoBanese.PreencherComCaractereEsquerda(6, '0');
				this.ConvenioParaComporNomeDoArquivoRemessa = boleto.Cedente.Convenio.PreencherComCaractereEsquerda(5, '0');
				this.FormatoAgenciaCodigoCedente = string.Format("{0}", Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 2)) + string.Format("{0}/", Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia, 1)) + string.Format("{0}-", Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 8)) + string.Format("{0}", boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture));
				if (Utils.ToString(boleto.NossoNumero) == string.Empty)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 8)
				{
					throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 8 números.");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 8)
				{
					boleto.NossoNumero = Utils.FitStringLength(boleto.NossoNumero.ToString(CultureInfo.InvariantCulture), 8, 8, '0', 0, true, true, true);
				}
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = "Pagável preferencialmente na rede Banese";
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

		// Token: 0x060006D7 RID: 1751 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.DigitoAgencia, "0", 3, true);
			string nossoNumero = boleto.NossoNumero;
			int num = BancoBanese.Mod11Banese(string.Format("{0}{1}", text, nossoNumero), 9);
			boleto.NossoNumero = boleto.NossoNumero;
			boleto.DigitoNossoNumero = num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00051D20 File Offset: 0x0004FF20
		private static int Mod11Banese(string seq, int b)
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
			if (num3 == 0 || num3 == 1)
			{
				return 0;
			}
			return 11 - num3;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00051D74 File Offset: 0x0004FF74
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			int num = 2;
			int num2 = 0;
			string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
			int moeda = boleto.Moeda;
			string text2 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text3 = string.Concat(new string[]
			{
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(1).PreencherComCaractereEsquerda(1, '0'), "0", 1, true),
				Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoAgencia.CopiarTextoDaDireitaParaEsquerda(1).PreencherComCaractereEsquerda(1, '0'), "0", 1, true),
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.CopiarTextoDaDireitaParaEsquerda(8).PreencherComCaractereEsquerda(8, '0'), "0", 8, true),
				Utils.FormatCode(boleto.Cedente.ContaBancaria.DigitoConta, "0", 1, true),
				boleto.NossoNumero,
				boleto.DigitoNossoNumero,
				text
			});
			int num3 = this.Mod10CodigoBarras(text3);
			int num4 = this.Mod11CodigoBarras(string.Format("{0}{1}", text3, num3));
			if (num4 == 1)
			{
				if (num3 < 9)
				{
					num3++;
				}
				if (num3 == 9)
				{
					num3 = 0;
				}
				num4 = this.Mod11CodigoBarras(string.Format("{0}{1}", text3, num3));
			}
			string text4 = string.Concat(new object[]
			{
				text,
				moeda,
				AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture),
				text2,
				text3,
				num3,
				num4
			});
			for (int i = text4.Length - 1; i >= 0; i--)
			{
				num2 += (int)Convert.ToInt16(text4.Substring(i, 1)) * num;
				num++;
				if (num > 9)
				{
					num = 2;
				}
			}
			int num5 = num2 % 11;
			int num6 = 11 - num5;
			if (num6 == 0 || num6 == 1 || num6 > 9)
			{
				num6 = 1;
			}
			this._dacBoleto = num6;
			boleto.CodigoBarra.Codigo = text4.Substring(0, 4) + this._dacBoleto + text4.Substring(4);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00051FE0 File Offset: 0x000501E0
		private int Mod10CodigoBarras(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length; i > 0; i--)
			{
				int num3 = Convert.ToInt32(Strings.Mid(seq, i, 1)) * num2;
				if (num3 > 9)
				{
					num3 -= 9;
				}
				num += num3;
				if (num2 == 2)
				{
					num2 = 1;
				}
				else
				{
					num2++;
				}
			}
			int num4 = num % 10;
			if (num4 == 0)
			{
				return num4;
			}
			return 10 - num4;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00052040 File Offset: 0x00050240
		private int Mod11CodigoBarras(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length; i > 0; i--)
			{
				num += Convert.ToInt32(Strings.Mid(seq, i, 1)) * num2;
				if (num2 == 7)
				{
					num2 = 2;
				}
				else
				{
					num2++;
				}
			}
			int num3 = num % 11;
			if (num3 == 0 || num3 == 1)
			{
				return num3;
			}
			return 11 - num3;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00052094 File Offset: 0x00050294
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

		// Token: 0x060006DE RID: 1758 RVA: 0x0002D979 File Offset: 0x0002BB79
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00052218 File Offset: 0x00050418
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

		// Token: 0x060006E0 RID: 1760 RVA: 0x000522A4 File Offset: 0x000504A4
		public string GerarHeaderRemessaCnab240(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 20);
				text2 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0000" + "0" + Utils.FormatCode("", " ", 9) + ((cedente.Cpfcnpj.Length == 11) ? "1" : "2") + Utils.FormatCode(cedente.Cpfcnpj, "0", 14, true) + ((cedente.Convenio.Length == 0) ? text : Utils.FitStringLength(cedente.Convenio.ToString(CultureInfo.InvariantCulture), 20, 20, ' ', 0, true, true, false)) + Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoAgencia, "0", 1, true) + Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true) + Utils.FormatCode(cedente.ContaBancaria.DigitoConta, "0", 1, true) + " " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + Utils.FormatCode("BANESE", " ", 30) + Utils.FormatCode("", " ", 10) + "1" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss") + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + "101" + "00000" + Utils.FormatCode("", " ", 69));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
			}
			return text2;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000524E4 File Offset: 0x000506E4
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000524FC File Offset: 0x000506FC
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x00052584 File Offset: 0x00050784
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x000527A0 File Offset: 0x000509A0
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x00052824 File Offset: 0x00050A24
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("CNAB240 não implementado.");
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00052830 File Offset: 0x00050A30
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00052848 File Offset: 0x00050A48
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
				text += Utils.FitStringLength(boleto.NossoNumero, 19, 19, '0', 0, true, true, true);
				text += Utils.FitStringLength(boleto.DigitoNossoNumero, 1, 1, '0', 0, true, true, true);
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
						if (num == 3)
						{
							text += "3";
							text += "00";
						}
						else
						{
							num = boleto.Instrucoes[1].Codigo;
							if (num == 3)
							{
								text += "3";
								text += "00";
							}
							else
							{
								text += "3";
								text += "00";
							}
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
						text += "1";
						text += "180";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						if (num == 1)
						{
							text += "1";
							text += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
						}
						else
						{
							num = boleto.Instrucoes[1].Codigo;
							if (num == 1)
							{
								text += "1";
								text += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 3, 3, '0', 0, true, true, true);
							}
							else
							{
								text += "1";
								text += "180";
							}
						}
					}
				}
				catch (Exception)
				{
					text += "1";
					text += "180";
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

		// Token: 0x060006E8 RID: 1768 RVA: 0x00052F1C File Offset: 0x0005111C
		public override string GerarDetalheSegmentoQRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text5;
			try
			{
				string text = new string('0', 16);
				string text2 = new string('0', 20);
				string text3 = new string(' ', 8);
				string text4 = new string(' ', 40);
				text5 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "Q" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + ((boleto.Sacado.CpfCnpj.Length <= 11) ? "1" : "2") + Utils.FitStringLength(boleto.Sacado.CpfCnpj, 15, 15, '0', 0, true, true, true) + Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(new char[] { ' ' }), 15, 15, ' ', 0, true, true, false).ToUpper() + Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper() + text + text4 + "000" + text2 + text3);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text5;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000531E0 File Offset: 0x000513E0
		public override string GerarDetalheSegmentoRRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				string text = new string(' ', 110);
				string text2 = new string(' ', 9);
				text3 = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3) + "0001" + "3" + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 5, 5, '0', 0, true, true, true) + "R" + " " + (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01")) + "000000000000000000000000" + "000000000000000000000000" + "1" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.ValorMulta.ToString("0.00").Replace(",", ""), 15, 15, '0', 0, true, true, true) + text + "0000000000000000" + "0" + "000000000000" + " " + " " + "0" + text2);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
			}
			return text3;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00053380 File Offset: 0x00051580
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

		// Token: 0x060006EB RID: 1771 RVA: 0x00053408 File Offset: 0x00051608
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

		// Token: 0x060006EC RID: 1772 RVA: 0x0005349C File Offset: 0x0005169C
		public string GerarTrailerRemessaCnab400(int numeroRegistro)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000534B4 File Offset: 0x000516B4
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

		// Token: 0x060006EE RID: 1774 RVA: 0x00053654 File Offset: 0x00051854
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

		// Token: 0x060006EF RID: 1775 RVA: 0x0005370C File Offset: 0x0005190C
		public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
		{
			DetalheSegmentoTRetornoCNAB240 detalheSegmentoTRetornoCNAB = base.LerDetalheSegmentoTRetornoCNAB240(registro);
			detalheSegmentoTRetornoCNAB.NossoNumero = registro.Substring(37, 20);
			return detalheSegmentoTRetornoCNAB;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00053728 File Offset: 0x00051928
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00053740 File Offset: 0x00051940
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 535654183U)
			{
				if (num <= 451618993U)
				{
					if (num <= 401286136U)
					{
						if (num != 350806184U)
						{
							if (num != 367583803U)
							{
								if (num == 401286136U)
								{
									if (codigo == "14")
									{
										return "14-Confirmação Recebimento Instrução Alteração de Vencimento";
									}
								}
							}
							else if (codigo == "60")
							{
								return "60-Confirmação do Pedido de Dispensa de Juros de Mora";
							}
						}
						else if (codigo == "61")
						{
							return "61-Confirmação de Alteração do Valor Nominal do Título";
						}
					}
					else if (num != 418210850U)
					{
						if (num != 434988469U)
						{
							if (num == 451618993U)
							{
								if (codigo == "17")
								{
									return "17-Liquidação Após Baixa ou Liquidação Título Não Registrado";
								}
							}
						}
						else if (codigo == "08")
						{
							return "08-Confirmação do Recebimento do Cancelamento do Desconto";
						}
					}
					else if (codigo == "09")
					{
						return "09-Baixa";
					}
				}
				else if (num <= 485174231U)
				{
					if (num != 451766088U)
					{
						if (num != 468543707U)
						{
							if (num == 485174231U)
							{
								if (codigo == "11")
								{
									return "11-Títulos em Carteira (Em Ser)";
								}
							}
						}
						else if (codigo == "06")
						{
							return "06-Liquidação";
						}
					}
					else if (codigo == "07")
					{
						return "07-Confirmação do Recebimento da Instrução de Desconto";
					}
				}
				else if (num <= 518729469U)
				{
					if (num != 501951850U)
					{
						if (num == 518729469U)
						{
							if (codigo == "13")
							{
								return "13-Confirmação Recebimento Instrução de Cancelamento Abatimento";
							}
						}
					}
					else if (codigo == "12")
					{
						return "12-Confirmação Recebimento Instrução de Abatimento";
					}
				}
				else if (num != 518876564U)
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
					return "03-Entrada Rejeitada";
				}
			}
			else if (num <= 2330020773U)
			{
				if (num <= 2296465535U)
				{
					if (num != 2279687916U)
					{
						if (num != 2280673654U)
						{
							if (num == 2296465535U)
							{
								if (codigo == "55")
								{
									return "55-Confirmação de Pedido de Dispensa de Multa";
								}
							}
						}
						else if (codigo == "30")
						{
							return "30-Alteração de Dados Rejeitada";
						}
					}
					else if (codigo == "54")
					{
						return "54-Confirmação da Instrução de Baixa de Título Negativado sem Protesto";
					}
				}
				else if (num <= 2313243154U)
				{
					if (num != 2296612630U)
					{
						if (num == 2313243154U)
						{
							if (codigo == "56")
							{
								return "56-Confirmação do Pedido de Cobrança de Multa";
							}
						}
					}
					else if (codigo == "41")
					{
						return "41-Confirmação da alteração do número controle do Participante";
					}
				}
				else if (num != 2313390249U)
				{
					if (num == 2330020773U)
					{
						if (codigo == "57")
						{
							return "57-Confirmação do Pedido de Alteração de Cobrança de Juros";
						}
					}
				}
				else if (codigo == "40")
				{
					return "40-Confirmação da alteração do número do título dado pelo Beneficiário";
				}
			}
			else if (num <= 2347931225U)
			{
				if (num != 2331153606U)
				{
					if (num != 2346798392U)
					{
						if (num == 2347931225U)
						{
							if (codigo == "26")
							{
								return "26-Instrução rejeitada";
							}
						}
					}
					else if (codigo == "58")
					{
						return "58-Confirmação do Pedido de Alteração do Valor/Data de Desconto";
					}
				}
				else if (codigo == "27")
				{
					return "27-Confirmação do Pedido de Alteração de Outros Dados";
				}
			}
			else if (num <= 2414894606U)
			{
				if (num != 2363576011U)
				{
					if (num == 2414894606U)
					{
						if (codigo == "38")
						{
							return "38-Confirmação de alteração do Prazo Limite de Recebimento (a data deve ser informada no campo 28.3.p)";
						}
					}
				}
				else if (codigo == "59")
				{
					return "59-Confirmação do Pedido de Alteração do Beneficiário do Título";
				}
			}
			else if (num != 2431672225U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return "28-Débito de Tarifas/Custas";
					}
				}
			}
			else if (codigo == "39")
			{
				return "39-Confirmação de Dispensa de Prazo Limite de Recebimento";
			}
			return "";
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00053BE5 File Offset: 0x00051DE5
		public string RetornarMotivoDaRejeicao(string codigo)
		{
			return "Não informado no arquivo de retorno";
		}

		// Token: 0x0400064F RID: 1615
		private int _dacBoleto;
	}
}

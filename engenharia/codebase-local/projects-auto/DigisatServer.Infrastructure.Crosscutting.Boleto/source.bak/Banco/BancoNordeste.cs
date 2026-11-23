using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000168 RID: 360
	internal sealed class BancoNordeste : AbstractBanco, IBanco
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0004F398 File Offset: 0x0004D598
		internal BancoNordeste()
		{
			this.Codigo = 4;
			this.Digito = "3";
			this.Nome = "Banco do Nordeste";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0004F3D8 File Offset: 0x0004D5D8
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0004F418 File Offset: 0x0004D618
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormataNossoNumeroImpressao(boleto);
				this.FormatoAgenciaCodigoCedente = string.Format("{0}{1}/", boleto.Cedente.ContaBancaria.Agencia, boleto.Cedente.ContaBancaria.DigitoAgencia) + string.Format("{0}-{1}", boleto.Cedente.Codigo.PreencherComCaractereEsquerda(6, '0'), boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture));
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length >= 7)
				{
					throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 7 números.");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 7)
				{
					boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 7);
				}
				if (Utils.ToInt32(boleto.NumeroDocumento) > 0)
				{
					boleto.NumeroDocumento = Utils.FormatCode(boleto.NumeroDocumento, 7);
				}
				this._dacNossoNumero = this.Mod11Nordeste(boleto.NossoNumero, 8);
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no BANCO DO NORDESTE";
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

		// Token: 0x060006BA RID: 1722 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		private int Mod11Nordeste(string seq, int b)
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
			int num3 = 11 - num % 11;
			if (num3 > 9 || num3 == 0 || num3 == 1)
			{
				num3 = 0;
			}
			return num3;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0004F600 File Offset: 0x0004D800
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NossoNumero = string.Format("{0}-{1}", boleto.NossoNumero, this._dacNossoNumero);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0004F650 File Offset: 0x0004D850
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				boleto.CodigoBarra.Codigo = string.Concat(new string[]
				{
					string.Format("{0}", Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3)),
					string.Format("{0}", boleto.Moeda),
					string.Format("{0}", AbstractBanco.FatorVencimento(boleto)),
					string.Format("{0}", text),
					string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(4).PreencherComCaractereEsquerda(4, '0')),
					string.Format("{0}", boleto.Cedente.ContaBancaria.Conta.PreencherComCaractereEsquerda(7, '0')),
					string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoConta),
					string.Format("{0}", boleto.NossoNumeroSemFormatacao.PreencherComCaractereEsquerda(7, '0')),
					string.Format("{0}", this._dacNossoNumero),
					string.Format("{0}", boleto.TipoCobranca.Codigo),
					"000"
				});
				this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9, 0);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0004F85C File Offset: 0x0004DA5C
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

		// Token: 0x060006BE RID: 1726 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? string.Format(" {0}-{1}", boleto.NossoNumero, boleto.DigitoNossoNumero) : string.Format(" {0}", boleto.NossoNumero));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0004FA20 File Offset: 0x0004DC20
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NumeroDocumento = (boleto.NumeroDocumento + " " + boleto.NumeroParcela).CopiarTextoDaDireitaParaEsquerda(10);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar número do documento.", ex);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00030B9C File Offset: 0x0002ED9C
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0004FA74 File Offset: 0x0004DC74
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text = " ";
			base.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
			switch (tipoArquivo)
			{
			case TipoArquivo.CNAB240:
				text = this.GerarHeaderRemessaCnab240(cedente);
				break;
			case TipoArquivo.CNAB400:
				text = this.GerarHeaderRemessaCnab400(0, cedente, numeroArquivoRemessa);
				break;
			case TipoArquivo.Outro:
				throw new Exception("Tipo de arquivo inexistente.");
			}
			return text;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00031274 File Offset: 0x0002F474
		public string GerarHeaderRemessaCnab240(Cedente cedente)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0004FAD0 File Offset: 0x0004DCD0
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 291);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + Utils.FitStringLength(cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true) + "00" + Utils.FitStringLength(cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false) + "      " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "004" + "B. DO NORDESTE " + DateTime.Now.ToString("ddMMyy") + Utils.FitStringLength(cedente.Convenio, 3, 3, '0', 0, true, true, true) + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0004FC14 File Offset: 0x0004DE14
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarHeaderLoteRemessaCnab240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderLoteRemessaCNAB400();
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x00031274 File Offset: 0x0002F474
		private string GerarHeaderLoteRemessaCnab240()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00031274 File Offset: 0x0002F474
		private string GerarHeaderLoteRemessaCNAB400()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0004FC80 File Offset: 0x0004DE80
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

		// Token: 0x060006C8 RID: 1736 RVA: 0x00031274 File Offset: 0x0002F474
		public string GerarDetalheRemessaCnab240(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text3;
			try
			{
				int num = this.Mod11Nordeste(boleto.NossoNumeroSemFormatacao, 8);
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = new string(' ', 16);
				string text2 = "1";
				text2 += text;
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true);
				text2 += "00";
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 7, 7, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(Convert.ToInt32(Math.Floor(boleto.PercMulta)).ToString(), 2, 2, '0', 0, true, true, true);
				text2 += "    ";
				text2 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text2 += Utils.FitStringLength(boleto.NossoNumeroSemFormatacao, 7, 7, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(num.ToString(), 1, 1, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Cedente.Contrato, 10, 10, '0', 0, true, true, true);
				text2 += "000000";
				text2 += "0000000000000";
				text2 += "        ";
				text2 += Utils.FitStringLength(boleto.Carteira, 1, 1, '0', 0, true, true, true);
				text2 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text2 += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text2 += boleto.DataVencimento.ToString("ddMMyy");
				text2 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text2 += "000";
				text2 += "0000";
				text2 += " ";
				text2 += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento), 2, 2, '0', 0, true, true, true);
				text2 += boleto.Aceite;
				text2 += boleto.DataDocumento.ToString("ddMMyy");
				int num2 = boleto.Instrucoes.Count<IInstrucao>((IInstrucao x) => x.Codigo < 997);
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text2 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
					else
					{
						text2 += "00";
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
				}
				else
				{
					text2 += "0000";
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
				text2 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true);
				text2 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false);
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
				text2 += new string(' ', 40);
				try
				{
					num2 = boleto.Instrucoes[0].Codigo;
					if (num2 == 9)
					{
						text2 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					}
					else
					{
						num2 = boleto.Instrucoes[1].Codigo;
						if (num2 == 9)
						{
							text2 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
						}
						else
						{
							text2 += "99";
						}
					}
				}
				catch (Exception)
				{
					text2 += "99";
				}
				text2 += "0";
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

		// Token: 0x060006CA RID: 1738 RVA: 0x0005047C File Offset: 0x0004E67C
		public override string GerarTrailerLoteRemessa(int numeroRegistro, int numeroRegistroSegmentoPQR, int quantidadeBoletos, decimal vltitulostotal = 0m)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + Utils.FormatCode("", "0", 4, true) + "5" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", "0", 6, true) + Utils.FormatCode("", "0", 18, true) + Utils.FormatCode("", "0", 18, true) + Utils.FormatCode("", " ", 171) + Utils.FormatCode("", " ", 10));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do LOTE de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00050578 File Offset: 0x0004E778
		public override string GerarTrailerArquivoRemessa(int numeroRegistro)
		{
			string text;
			try
			{
				text = Utils.SubstituiCaracteresEspeciais(Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), "0", 3, true) + "9999" + "9" + Utils.FormatCode("", " ", 9) + Utils.FormatCode("", "0", 6, true) + Utils.FormatCode("", "0", 6, true) + Utils.FormatCode("", " ", 211));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do ARQUIVO de REMESSA.", ex);
			}
			return text;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0005063C File Offset: 0x0004E83C
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
					text = this.GerarTrailerRemessa240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarTrailerRemessa400(numeroRegistro);
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

		// Token: 0x060006CD RID: 1741 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		public string GerarTrailerRemessa240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000506B4 File Offset: 0x0004E8B4
		public string GerarTrailerRemessa400(int numeroRegistro)
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

		// Token: 0x060006CF RID: 1743 RVA: 0x00050720 File Offset: 0x0004E920
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
				detalheRetornoCNAB.NossoNumero = registro.Substring(62, 7);
				int num2;
				int.TryParse(registro.Substring(69, 1), out num2);
				detalheRetornoCNAB.DACNossoNumero = num2;
				decimal num3 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.TarifaCobranca = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(188, 13));
				detalheRetornoCNAB.OutrasDespesas = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num5 / 100m;
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				decimal num6 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num6 / 100m;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(Utils.ToInt32(registro.Substring(295, 6)).ToString("##-##-##"));
				int codigoOcorrencia = detalheRetornoCNAB.CodigoOcorrencia;
				if (codigoOcorrencia <= 6)
				{
					if (codigoOcorrencia == 2)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
						goto IL_0203;
					}
					if (codigoOcorrencia == 6)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
						goto IL_0203;
					}
				}
				else
				{
					if (codigoOcorrencia == 9)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(279, 115).Trim());
						goto IL_0203;
					}
					if (codigoOcorrencia == 51)
					{
						detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
						detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(279, 115).Trim());
						goto IL_0203;
					}
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_0203:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00050964 File Offset: 0x0004EB64
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 468543707U)
			{
				if (num <= 418063755U)
				{
					if (num <= 350953279U)
					{
						if (num != 334175660U)
						{
							if (num == 350953279U)
							{
								if (codigo == "19")
								{
									return "19-Confirmação de Protesto";
								}
							}
						}
						else if (codigo == "18")
						{
							return "18-Alteração depositária";
						}
					}
					else if (num != 401286136U)
					{
						if (num == 418063755U)
						{
							if (codigo == "15")
							{
								return "15-Baixa automática";
							}
						}
					}
					else if (codigo == "14")
					{
						return "14-Vencimento Alterado";
					}
				}
				else if (num <= 434988469U)
				{
					if (num != 418210850U)
					{
						if (num == 434988469U)
						{
							if (codigo == "08")
							{
								return "08-Pagamento por cartório";
							}
						}
					}
					else if (codigo == "09")
					{
						return "09-Baixa simples";
					}
				}
				else if (num != 451766088U)
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
						return "10-Devolvido - Protestado";
					}
				}
				else if (codigo == "07")
				{
					return "07-Pagamento por conta";
				}
			}
			else if (num <= 518729469U)
			{
				if (num <= 501951850U)
				{
					if (num != 485174231U)
					{
						if (num == 501951850U)
						{
							if (codigo == "12")
							{
								return "12-Abatimento Concedido";
							}
						}
					}
					else if (codigo == "11")
					{
						return "11-Em Ser (Só no retorno mensal)";
					}
				}
				else if (num != 502098945U)
				{
					if (num == 518729469U)
					{
						if (codigo == "13")
						{
							return "13-Abatimento Cancelado";
						}
					}
				}
				else if (codigo == "04")
				{
					return "04-Alteração";
				}
			}
			else if (num <= 2229355059U)
			{
				if (num != 535654183U)
				{
					if (num == 2229355059U)
					{
						if (codigo == "51")
						{
							return "51-Entrada rejeitada";
						}
					}
				}
				else if (codigo == "02")
				{
					return "02-Entrada Confirmada";
				}
			}
			else if (num != 2364708844U)
			{
				if (num != 2381486463U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return "22-Alteração 'Seu Número'";
						}
					}
				}
				else if (codigo == "20")
				{
					return "20-Confirmação de Sustar-Protesto";
				}
			}
			else if (codigo == "21")
			{
				return "21-Alteração Informações de Controle da Empresa";
			}
			return "";
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00050C78 File Offset: 0x0004EE78
		private string RetornarDescricaoDosMotivosDeRejeicoes(string codigosDeRejeicoes)
		{
			return (from rejeicao in Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace)
				where rejeicao.Length > 0
				select rejeicao).Aggregate(null, (string current, string rejeicao) => current + this.RetornarMotivoDaRejeicaoEntradasRejeitadas(rejeicao) + "\n");
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00050CC8 File Offset: 0x0004EEC8
		public string RetornarMotivoDaRejeicaoEntradasRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2279835011U)
			{
				if (num <= 468249517U)
				{
					if (num <= 401286136U)
					{
						if (num <= 350806184U)
						{
							if (num <= 233362851U)
							{
								if (num != 216585232U)
								{
									if (num == 233362851U)
									{
										if (codigo == "68")
										{
											return "68-Data desconto-2 posterior ao vencimento.";
										}
									}
								}
								else if (codigo == "69")
								{
									return "69-Falta valor desconto-2.";
								}
							}
							else if (num != 334175660U)
							{
								if (num == 350806184U)
								{
									if (codigo == "61")
									{
										return "61-Banco/ Agência Cobrador Inválido.";
									}
								}
							}
							else if (codigo == "18")
							{
								return "18-Vencimento Inválido.";
							}
						}
						else if (num <= 367583803U)
						{
							if (num != 350953279U)
							{
								if (num == 367583803U)
								{
									if (codigo == "60")
									{
										return "60-Não Aceite Inválido para Espécie Documento.";
									}
								}
							}
							else if (codigo == "19")
							{
								return "19-Data Vencimento Anterior a Emissão.";
							}
						}
						else if (num != 384361422U)
						{
							if (num != 401139041U)
							{
								if (num == 401286136U)
								{
									if (codigo == "14")
									{
										return "14-Dígito de Contrato não confere.";
									}
								}
							}
							else if (codigo == "62")
							{
								return "62-Limite Operacional Não Cadastrado.";
							}
						}
						else if (codigo == "63")
						{
							return "63-Título já em situação de protesto.";
						}
					}
					else if (num <= 434841374U)
					{
						if (num <= 418063755U)
						{
							if (num != 417916660U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										return "15-Contrato Inexistente.";
									}
								}
							}
							else if (codigo == "65")
							{
								return "65-Proibido informar nosso número p/ cod. carteira.";
							}
						}
						else if (num != 418210850U)
						{
							if (num != 434694279U)
							{
								if (num == 434841374U)
								{
									if (codigo == "16")
									{
										return "16-Data de Emissão Inválida.";
									}
								}
							}
							else if (codigo == "64")
							{
								return "64-Proibido alterar vencimento título descontado.";
							}
						}
						else if (codigo == "09")
						{
							return "09-Tipo Operação Inexistente.";
						}
					}
					else if (num <= 451471898U)
					{
						if (num != 434988469U)
						{
							if (num == 451471898U)
							{
								if (codigo == "67")
								{
									return "67-Data desconto-2 inválida.";
								}
							}
						}
						else if (codigo == "08")
						{
							return "08-Tipo Operação Inválida.";
						}
					}
					else if (num != 451618993U)
					{
						if (num != 451766088U)
						{
							if (num == 468249517U)
							{
								if (codigo == "66")
								{
									return "66-Falta vencimento desconto-2.";
								}
							}
						}
						else if (codigo == "07")
						{
							return "07-Espécie Documento Inexistente.";
						}
					}
					else if (codigo == "17")
					{
						return "17-Falta Valor do Título.";
					}
				}
				else if (num <= 535654183U)
				{
					if (num <= 485321326U)
					{
						if (num <= 468543707U)
						{
							if (num != 468396612U)
							{
								if (num == 468543707U)
								{
									if (codigo == "06")
									{
										return "06-Espécie Documento Inválida.";
									}
								}
							}
							else if (codigo == "10")
							{
								return "10-Contrato Proibido para esta Carteira.";
							}
						}
						else if (num != 485174231U)
						{
							if (num == 485321326U)
							{
								if (codigo == "05")
								{
									return "05-Novo vencimento igual ao do Título.";
								}
							}
						}
						else if (codigo == "11")
						{
							return "11-Falta Número do Contrato.";
						}
					}
					else if (num <= 502098945U)
					{
						if (num != 501951850U)
						{
							if (num == 502098945U)
							{
								if (codigo == "04")
								{
									return "04-Novo vencimento igual/ menor que o da entrada.";
								}
							}
						}
						else if (codigo == "12")
						{
							return "12-Proibido Informar Tipo de Conta.";
						}
					}
					else if (num != 518729469U)
					{
						if (num != 518876564U)
						{
							if (num == 535654183U)
							{
								if (codigo == "02")
								{
									return "02-Não permite desconto/ abatimento.";
								}
							}
						}
						else if (codigo == "03")
						{
							return "03-Código do serviço inválido.";
						}
					}
					else if (codigo == "13")
					{
						return "13-Tipo de Conta do Contrato Inexistente.";
					}
				}
				else if (num <= 2247118416U)
				{
					if (num <= 2212577440U)
					{
						if (num != 552431802U)
						{
							if (num == 2212577440U)
							{
								if (codigo == "50")
								{
									return "50-Dígito do Título Não Confere.";
								}
							}
						}
						else if (codigo == "01")
						{
							return "01-Falta valor do IOC.";
						}
					}
					else if (num != 2229355059U)
					{
						if (num != 2246132678U)
						{
							if (num == 2247118416U)
							{
								if (codigo == "32")
								{
									return "32-Falta Estado.";
								}
							}
						}
						else if (codigo == "52")
						{
							return "52-Valor Abatimento Inválido.";
						}
					}
					else if (codigo == "51")
					{
						return "51-Título Inexistente ou Liquidado.";
					}
				}
				else if (num <= 2263057392U)
				{
					if (num != 2262910297U)
					{
						if (num == 2263057392U)
						{
							if (codigo == "43")
							{
								return "43-Título Liquidado.";
							}
						}
					}
					else if (codigo == "53")
					{
						return "53-Data Vencimento Inválida.";
					}
				}
				else if (num != 2263896035U)
				{
					if (num != 2279687916U)
					{
						if (num == 2279835011U)
						{
							if (codigo == "42")
							{
								return "42-Título Inexistente.";
							}
						}
					}
					else if (codigo == "54")
					{
						return "54-Estado Inválido.";
					}
				}
				else if (codigo == "33")
				{
					return "33-Estado Inválido.";
				}
			}
			else if (num <= 2347784130U)
			{
				if (num <= 2314228892U)
				{
					if (num <= 2297451273U)
					{
						if (num <= 2296465535U)
						{
							if (num != 2280673654U)
							{
								if (num == 2296465535U)
								{
									if (codigo == "55")
									{
										return "55-Falta Tipo de Pessoa P/ Alteração de CGC/ CPF.";
									}
								}
							}
							else if (codigo == "30")
							{
								return "30-Falta Endereço.";
							}
						}
						else if (num != 2296612630U)
						{
							if (num == 2297451273U)
							{
								if (codigo == "31")
								{
									return "31-Falta Cidade.";
								}
							}
						}
						else if (codigo == "41")
						{
							return "41-Dígito Nosso Número com Erro.";
						}
					}
					else if (num <= 2313243154U)
					{
						if (num != 2297598368U)
						{
							if (num == 2313243154U)
							{
								if (codigo == "56")
								{
									return "56-CPF/ CGC com Erro.";
								}
							}
						}
						else if (codigo == "25")
						{
							return "25-Banco/Agência Cobrador Inexistente.";
						}
					}
					else if (num != 2313390249U)
					{
						if (num != 2313537344U)
						{
							if (num == 2314228892U)
							{
								if (codigo == "36")
								{
									return "36-Título Pré-Numerado já Existente.";
								}
							}
						}
						else if (codigo == "76")
						{
							return "76-Nosso número/ dígito com erro.";
						}
					}
					else if (codigo == "40")
					{
						return "40-Dígito Cliente/ Contrato com Erro.";
					}
				}
				else if (num <= 2331006511U)
				{
					if (num <= 2330020773U)
					{
						if (num != 2314375987U)
						{
							if (num == 2330020773U)
							{
								if (codigo == "57")
								{
									return "57-Data Emissão Inválida.";
								}
							}
						}
						else if (codigo == "24")
						{
							return "24-Falta Mora-1-Dia.";
						}
					}
					else if (num != 2330167868U)
					{
						if (num != 2330314963U)
						{
							if (num == 2331006511U)
							{
								if (codigo == "37")
								{
									return "37-Dígito do Título Não Confere.";
								}
							}
						}
						else if (codigo == "77")
						{
							return "77-Dias vencidos superior ao prazo de devolução.";
						}
					}
					else if (codigo == "47")
					{
						return "47-Falta Tipo de Conta do Contrato.";
					}
				}
				else if (num <= 2346798392U)
				{
					if (num != 2331153606U)
					{
						if (num == 2346798392U)
						{
							if (codigo == "58")
							{
								return "58-Data Vencimento Desconto Inválida.";
							}
						}
					}
					else if (codigo == "27")
					{
						return "27-Código Pessoa Inválido.";
					}
				}
				else if (num != 2346945487U)
				{
					if (num != 2347092582U)
					{
						if (num == 2347784130U)
						{
							if (codigo == "34")
							{
								return "24-Falta CPF/ CGC do Sacado.";
							}
						}
					}
					else if (codigo == "74")
					{
						return "74-Moeda Inválida para o tipo de Operação.";
					}
				}
				else if (codigo == "46")
				{
					return "46-Proibido Taxa – Multa p/ Correspondente.";
				}
			}
			else if (num <= 2397425439U)
			{
				if (num <= 2364561749U)
				{
					if (num <= 2363576011U)
					{
						if (num != 2347931225U)
						{
							if (num == 2363576011U)
							{
								if (codigo == "59")
								{
									return "59-Aceite Inválido para Espécie Documento.";
								}
							}
						}
						else if (codigo == "26")
						{
							return "26-BCO/AGE Cobrador não Cadastrado.";
						}
					}
					else if (num != 2363723106U)
					{
						if (num != 2363870201U)
						{
							if (num == 2364561749U)
							{
								if (codigo == "35")
								{
									return "35-Falta numeração - Bloquete emitido.";
								}
							}
						}
						else if (codigo == "75")
						{
							return "75-Moeda inexistente.";
						}
					}
					else if (codigo == "45")
					{
						return "45-Valor Nominal Incorreto.";
					}
				}
				else if (num <= 2380500725U)
				{
					if (num != 2364708844U)
					{
						if (num == 2380500725U)
						{
							if (codigo == "44")
							{
								return "44-Título Não Pode Ser Baixado.";
							}
						}
					}
					else if (codigo == "21")
					{
						return "21-Data Desconto Inválida.";
					}
				}
				else if (num != 2380647820U)
				{
					if (num != 2381486463U)
					{
						if (num == 2397425439U)
						{
							if (codigo == "73")
							{
								return "73-Seu número já existente.";
							}
						}
					}
					else if (codigo == "20")
					{
						return "20-Falta Vencimento Desconto.";
					}
				}
				else if (codigo == "72")
				{
					return "72-CEP não pertence ao Estado.";
				}
			}
			else if (num <= 2430833582U)
			{
				if (num <= 2414203058U)
				{
					if (num != 2398264082U)
					{
						if (num == 2414203058U)
						{
							if (codigo == "70")
							{
								return "70-Data vencimento desconto-2 inválida.";
							}
						}
					}
					else if (codigo == "23")
					{
						return "23-Falta Valor Desconto.";
					}
				}
				else if (num != 2414894606U)
				{
					if (num != 2415041701U)
					{
						if (num == 2430833582U)
						{
							if (codigo == "49")
							{
								return "49-Dígito Contrato Não Confere.";
							}
						}
					}
					else if (codigo == "22")
					{
						return "22-Data Desconto Posterior ao Vencimento.";
					}
				}
				else if (codigo == "38")
				{
					return "38-Proibido Protestar.";
				}
			}
			else if (num <= 2431672225U)
			{
				if (num != 2430980677U)
				{
					if (num == 2431672225U)
					{
						if (codigo == "39")
						{
							return "39-Proibido título pré-numerado p/ Correspondente.";
						}
					}
				}
				else if (codigo == "71")
				{
					return "71-IOC maior que valor do título.";
				}
			}
			else if (num != 2447611201U)
			{
				if (num != 2498929796U)
				{
					if (num == 2515707415U)
					{
						if (codigo == "28")
						{
							return "28-Falta CEP, Banco e Agência Cobrador.";
						}
					}
				}
				else if (codigo == "29")
				{
					return "29-Falta Nome Sacado.";
				}
			}
			else if (codigo == "48")
			{
				return "48-Tipo de Conta Inexistente.";
			}
			return "";
		}

		// Token: 0x0400064C RID: 1612
		private int _dacBoleto;

		// Token: 0x0400064D RID: 1613
		private int _dacNossoNumero;

		// Token: 0x0400064E RID: 1614
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
	}
}

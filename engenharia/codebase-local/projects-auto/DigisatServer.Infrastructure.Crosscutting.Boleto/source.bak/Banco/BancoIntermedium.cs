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
	// Token: 0x02000167 RID: 359
	internal sealed class BancoIntermedium : AbstractBanco, IBanco
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0004E11C File Offset: 0x0004C31C
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("CI400_001_{0}.REM", this.NumeroSequencialArquivo.ToStringLeftZero(7));
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0004E134 File Offset: 0x0004C334
		internal BancoIntermedium()
		{
			this.Codigo = 77;
			this.Digito = "9";
			this.Nome = "Banco Intermedium S.A.";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0004E168 File Offset: 0x0004C368
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				this.FormatoAgenciaCodigoCedente = string.Format("{0}{1}/{2}-{3}", new object[]
				{
					boleto.Cedente.ContaBancaria.Agencia,
					boleto.Cedente.ContaBancaria.DigitoAgencia,
					boleto.Cedente.Codigo.PreencherComCaractereEsquerda(5, '0'),
					boleto.Cedente.DigitoCedente.ToString(CultureInfo.InvariantCulture)
				});
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no BANCO INTERMEDIUM S.A.";
				}
				boleto.FormataCampos();
				this.FormataNumeroDocumento(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0004E238 File Offset: 0x0004C438
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}00000000000{5}{6}{7}000", new object[]
				{
					this.Codigo,
					boleto.Moeda,
					AbstractBanco.FatorVencimento(boleto),
					text,
					boleto.Carteira,
					boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia,
					Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 5),
					boleto.Cedente.ContaBancaria.DigitoConta
				});
				this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9, 0);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NossoNumero = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero, boleto.DigitoNossoNumero);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0004E400 File Offset: 0x0004C600
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				string text2 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
				string carteira = boleto.Carteira;
				string text3 = "00";
				string text4 = AbstractBanco.Mod10(text + text2 + carteira + text3).ToString(CultureInfo.InvariantCulture);
				string text5 = "000000";
				string text6 = string.Format(" {0} ", this._dacBoleto);
				string text7 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
				string text8 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				string text9 = string.Format("{0}{1}{2}.", text, text2, carteira.Substring(0, 1));
				text9 += string.Format("{0}{1}{2} ", carteira.Substring(1, 2), text3, text4);
				text8 = Utils.FormatCode(text8, 10);
				string text10 = text7 + text8;
				string text11 = boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3);
				string text12 = AbstractBanco.Mod10(text5 + "0" + text11).ToString(CultureInfo.InvariantCulture);
				string text13 = string.Format("{0}.", text5.Substring(0, 5));
				text13 += string.Format("{0}0{1}{2} ", text5.Substring(5, 1), text11, text12);
				string digitoAgencia = boleto.Cedente.ContaBancaria.DigitoAgencia;
				string text14 = Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta, 6, 6, '0', 0, true, true, true);
				string text15 = AbstractBanco.Mod10(digitoAgencia + text14 + "000").ToString(CultureInfo.InvariantCulture);
				string text16 = string.Format("{0}{1}.{2}{3}{4}", new object[]
				{
					digitoAgencia,
					text14.Substring(0, 4),
					text14.Substring(4, 2),
					"000",
					text15
				});
				boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text9, text13, text16, text6, text10 });
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar linha digitável.", ex);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0004E688 File Offset: 0x0004C888
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = "00000000000";
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0004E698 File Offset: 0x0004C898
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

		// Token: 0x060006A7 RID: 1703 RVA: 0x00030B9C File Offset: 0x0002ED9C
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0004E6EC File Offset: 0x0004C8EC
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

		// Token: 0x060006A9 RID: 1705 RVA: 0x00031274 File Offset: 0x0002F474
		public string GerarHeaderRemessaCnab240(Cedente cedente)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0004E748 File Offset: 0x0004C948
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text4;
			try
			{
				string text = new string(' ', 20);
				string text2 = new string(' ', 10);
				string text3 = new string(' ', 277);
				text4 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + text + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "077" + "INTER          " + DateTime.Now.ToString("ddMMyy") + text2 + Utils.FitStringLength(numeroArquivoRemessa.ToString(CultureInfo.InvariantCulture), 7, 7, '0', 0, true, true, true) + text3 + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text4;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0004E82C File Offset: 0x0004CA2C
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

		// Token: 0x060006AC RID: 1708 RVA: 0x00031274 File Offset: 0x0002F474
		private string GerarHeaderLoteRemessaCnab240()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00031274 File Offset: 0x0002F474
		private string GerarHeaderLoteRemessaCNAB400()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0004E898 File Offset: 0x0004CA98
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

		// Token: 0x060006AF RID: 1711 RVA: 0x00031274 File Offset: 0x0002F474
		public string GerarDetalheRemessaCnab240(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0004E910 File Offset: 0x0004CB10
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text6;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = new string(' ', 70);
				string text2 = new string(' ', 19);
				string text3 = new string(' ', 8);
				string text4 = new string(' ', 6);
				string text5 = "1";
				text5 += text2;
				text5 += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
				text5 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true);
				text5 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 9, 9, '0', 0, true, true, true);
				text5 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
				text5 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text5 += "   ";
				if (boleto.ValorMulta > 0m)
				{
					text5 += "2";
					text5 += "0000000000000";
					text5 += Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 4, 4, '0', 0, true, true, true);
					text5 += boleto.DataMulta.ToString("ddMMyy");
				}
				else
				{
					text5 += "0";
					text5 += "0000000000000";
					text5 += "0000";
					text5 += "000000";
				}
				text5 += "00000000000";
				text5 += text3;
				text5 += (boleto.SolicitacaoDeBaixa ? "02" : "01");
				text5 += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text5 += boleto.DataVencimento.ToString("ddMMyy");
				text5 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text5 += "30";
				text5 += text4;
				text5 += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento), 2, 2, '0', 0, true, true, true);
				text5 += "N";
				text5 += "      ";
				text5 += "   ";
				if (boleto.JurosMora > 0m)
				{
					text5 += "1";
					text5 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
					text5 += "0000";
					text5 += boleto.DataJurosMora.ToString("ddMMyy");
				}
				else
				{
					text5 += "0";
					text5 += "0000000000000";
					text5 += "0000";
					text5 += "000000";
				}
				if (boleto.ValorDesconto > 0m)
				{
					text5 += "1";
					text5 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
					text5 += "0000";
					text5 += boleto.DataDesconto.ToString("ddMMyy");
				}
				else
				{
					text5 += "0";
					text5 += "0000000000000";
					text5 += "0000";
					text5 += "000000";
				}
				text5 += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text5 += "01";
				}
				else
				{
					text5 += "02";
				}
				text5 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false);
				text5 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Bairro,
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text5 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text5 += text;
				text5 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text5 = Utils.SubstituiCaracteresEspeciais(text5);
				text6 = text5;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text6;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0004EF7C File Offset: 0x0004D17C
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

		// Token: 0x060006B2 RID: 1714 RVA: 0x0004F078 File Offset: 0x0004D278
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

		// Token: 0x060006B3 RID: 1715 RVA: 0x0004F13C File Offset: 0x0004D33C
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

		// Token: 0x060006B4 RID: 1716 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		public string GerarTrailerRemessa240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0004F1C0 File Offset: 0x0004D3C0
		public string GerarTrailerRemessa400(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 387);
				text2 = Utils.SubstituiCaracteresEspeciais("9" + Utils.FitStringLength((numeroRegistro - 2).ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true) + text + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0004F24C File Offset: 0x0004D44C
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(91, 6));
				int num2 = Utils.ToInt32(registro.Substring(172, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(37, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(89, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"))
				};
				string text = registro.Substring(159, 13).ApenasNumeros();
				if (!string.IsNullOrEmpty(text))
				{
					decimal num3 = Convert.ToUInt64(text);
					detalheRetornoCNAB.ValorPrincipal = num3 / 100m;
				}
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num2.ToString("##-##-##"));
				switch (detalheRetornoCNAB.CodigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_010B;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					goto IL_010B;
				case 4:
				case 6:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_010B;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_010B:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0400064B RID: 1611
		private int _dacBoleto;
	}
}

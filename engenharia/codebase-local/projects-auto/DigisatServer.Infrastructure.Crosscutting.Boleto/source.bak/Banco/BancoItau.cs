using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200016F RID: 367
	internal sealed class BancoItau : AbstractBanco, IBanco
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x00061058 File Offset: 0x0005F258
		internal BancoItau()
		{
			this.Codigo = 341;
			this.Digito = "7";
			this.Nome = "Itaú";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x000610A8 File Offset: 0x0005F2A8
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("C{0:ddMMyy}{1}.REM", DateTime.Now.Date, this._alpha[this.SequenciaRemessaDiaria - 1]);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000610E8 File Offset: 0x0005F2E8
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
				int[] array = new int[]
				{
					112, 115, 104, 147, 188, 108, 109, 150, 121, 180,
					175, 198, 142, 143, 174, 177, 129, 139, 169, 172,
					102, 107, 173, 103, 196
				};
				bool flag = false;
				IEnumerable<int> enumerable = array;
				Func<int, bool> __9__0;
				Func<int, bool> func;
				if ((func = __9__0) == null)
				{
					func = (__9__0 = (int c) => Utils.ToString(boleto.Carteira) == Utils.ToString(c));
				}
				foreach (int num in enumerable.Where<int>(func))
				{
					flag = true;
				}
				if (!flag)
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append(". Carteiras implementadas: ");
					foreach (int num2 in array)
					{
						stringBuilder.AppendFormat(" {0}", num2);
					}
					throw new NotImplementedException("Carteira não implementada: " + boleto.Carteira + stringBuilder);
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length > 8)
				{
					throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 8 números.");
				}
				if (Convert.ToInt32(boleto.NossoNumero).ToString(CultureInfo.InvariantCulture).Length < 8)
				{
					boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 8);
				}
				if (Utils.ToInt32(boleto.NumeroDocumento) > 0)
				{
					boleto.NumeroDocumento = Utils.FormatCode(boleto.NumeroDocumento, 7);
				}
				if (boleto.Carteira != "126" && boleto.Carteira != "131" && boleto.Carteira != "146" && boleto.Carteira != "150" && boleto.Carteira != "168")
				{
					this._dacNossoNumero = AbstractBanco.Mod10(string.Concat(new string[]
					{
						boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3),
						boleto.Cedente.ContaBancaria.DigitoAgencia,
						boleto.Cedente.ContaBancaria.Conta,
						boleto.Carteira,
						boleto.NossoNumero
					}));
				}
				else
				{
					this._dacNossoNumero = AbstractBanco.Mod10(boleto.Carteira + boleto.NossoNumero);
				}
				boleto.Cedente.ContaBancaria.DigitoConta = AbstractBanco.Mod10(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia + boleto.Cedente.ContaBancaria.Conta).ToString(CultureInfo.InvariantCulture);
				if (string.IsNullOrEmpty(boleto.LocalPagamento))
				{
					Boleto boleto2 = boleto;
					boleto2.LocalPagamento = boleto2.LocalPagamento + this.Nome + ". Após o vencimento, somente no ITAÚ";
				}
				if (Utils.ToInt64(boleto.NossoNumero) == 0L)
				{
					throw new NotImplementedException("Nosso número inválido");
				}
				boleto.FormataCampos();
				this.FormataNumeroDocumento(boleto);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao validar boletos.", ex);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00061548 File Offset: 0x0005F748
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text = Utils.FormatCode(text, 10);
				string text2 = Utils.FormatCode(boleto.NumeroDocumento, 7);
				string text3 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 5);
				string carteira = boleto.Carteira;
				if (carteira == "198" || carteira == "107" || carteira == "122" || carteira == "142" || carteira == "143" || carteira == "196")
				{
					boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}0", new object[]
					{
						this.Codigo,
						boleto.Moeda,
						AbstractBanco.FatorVencimento(boleto),
						text,
						boleto.Carteira,
						boleto.NossoNumero,
						text2,
						text3,
						AbstractBanco.Mod10(boleto.Carteira + boleto.NossoNumero + text2 + text3)
					});
				}
				else
				{
					boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}000", new object[]
					{
						this.Codigo,
						boleto.Moeda,
						AbstractBanco.FatorVencimento(boleto),
						text,
						boleto.Carteira,
						boleto.NossoNumero,
						this._dacNossoNumero,
						boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia,
						Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 5),
						boleto.Cedente.ContaBancaria.DigitoConta
					});
				}
				this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9, 0);
				boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000617EC File Offset: 0x0005F9EC
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				string text = Utils.FormatCode(boleto.NumeroDocumento, 7);
				string text2 = Utils.FormatCode(boleto.Cedente.Codigo.ToString(CultureInfo.InvariantCulture), 5);
				string text3 = Utils.FormatCode(this.Codigo.ToString(CultureInfo.InvariantCulture), 3);
				string text4 = boleto.Moeda.ToString(CultureInfo.InvariantCulture);
				string carteira = boleto.Carteira;
				string text5 = boleto.NossoNumero.Substring(0, 2);
				string text6 = AbstractBanco.Mod10(text3 + text4 + carteira + text5).ToString(CultureInfo.InvariantCulture);
				string text7 = boleto.NossoNumero.Substring(2, 6);
				string text8 = string.Format(" {0} ", this._dacBoleto);
				string text9 = AbstractBanco.FatorVencimento(boleto).ToString(CultureInfo.InvariantCulture);
				string text10 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				string text11 = string.Format("{0}{1}{2}.", text3, text4, carteira.Substring(0, 1));
				text11 += string.Format("{0}{1}{2} ", carteira.Substring(1, 2), text5, text6);
				text10 = Utils.FormatCode(text10, 10);
				string text12 = text9 + text10;
				string carteira2 = boleto.Carteira;
				string text15;
				string text20;
				if (carteira2 == "198" || carteira2 == "107" || carteira2 == "122" || carteira2 == "142" || carteira2 == "143" || carteira2 == "196")
				{
					string text13 = text.Substring(0, 4);
					string text14 = AbstractBanco.Mod10(text7 + text13).ToString(CultureInfo.InvariantCulture);
					text15 = string.Format("{0}.", text7.Substring(0, 5));
					text15 += string.Format("{0}{1}{2} ", text7.Substring(5, 1), text13, text14);
					string text16 = text.Substring(4, 3);
					string text17 = text2;
					string text18 = AbstractBanco.Mod10(boleto.Carteira + boleto.NossoNumero + text + text2).ToString(CultureInfo.InvariantCulture);
					string text19 = AbstractBanco.Mod10(text16 + text17 + text18 + "0").ToString(CultureInfo.InvariantCulture);
					text20 = string.Format("{0}{1}.{2}{3}{4}{5}", new object[]
					{
						text16,
						text17.Substring(0, 2),
						text17.Substring(2, 3),
						text18,
						"0",
						text19
					});
				}
				else
				{
					string text21 = this._dacNossoNumero.ToString(CultureInfo.InvariantCulture);
					string text22 = boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3);
					string text23 = AbstractBanco.Mod10(text7 + text21 + text22).ToString(CultureInfo.InvariantCulture);
					text15 = string.Format("{0}.", text7.Substring(0, 5));
					text15 += string.Format("{0}{1}{2}{3} ", new object[]
					{
						text7.Substring(5, 1),
						text21,
						text22,
						text23
					});
					string digitoAgencia = boleto.Cedente.ContaBancaria.DigitoAgencia;
					string text24 = boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta;
					string text25 = AbstractBanco.Mod10(digitoAgencia + text24 + "000").ToString(CultureInfo.InvariantCulture);
					text20 = string.Format("{0}{1}.{2}{3}{4}", new object[]
					{
						digitoAgencia,
						text24.Substring(0, 4),
						text24.Substring(4, 2),
						"000",
						text25
					});
				}
				boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text11, text15, text20, text8, text12 });
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar linha digitável.", ex);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00061C28 File Offset: 0x0005FE28
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				boleto.NossoNumero = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero, this._dacNossoNumero);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00061C7C File Offset: 0x0005FE7C
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

		// Token: 0x06000779 RID: 1913 RVA: 0x00030B9C File Offset: 0x0002ED9C
		public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			return this.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00061CD0 File Offset: 0x0005FED0
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

		// Token: 0x0600077B RID: 1915 RVA: 0x0003401C File Offset: 0x0003221C
		public string GerarHeaderRemessaCnab240(Cedente cedente)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00061D2C File Offset: 0x0005FF2C
		public string GerarHeaderRemessaCnab400(int numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 294);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + Utils.FitStringLength(cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true) + "00" + Utils.FitStringLength(cedente.ContaBancaria.Conta, 5, 5, '0', 0, true, true, true) + Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false) + "        " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "341" + "BANCO ITAU SA  " + DateTime.Now.ToString("ddMMyy") + text + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00061E4C File Offset: 0x0006004C
		public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = " ";
				switch (tipoArquivo)
				{
				case TipoArquivo.CNAB240:
					text = this.GerarHeaderLoteRemessaCnab240(cedente);
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

		// Token: 0x0600077E RID: 1918 RVA: 0x0003401C File Offset: 0x0003221C
		private string GerarHeaderLoteRemessaCnab240(Cedente cedente)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0003401C File Offset: 0x0003221C
		private string GerarHeaderLoteRemessaCNAB400()
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00061EB8 File Offset: 0x000600B8
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

		// Token: 0x06000781 RID: 1921 RVA: 0x0003401C File Offset: 0x0003221C
		public string GerarDetalheRemessaCnab240(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00061F30 File Offset: 0x00060130
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text4;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = new string(' ', 21);
				string text2 = new string(' ', 10);
				string text3 = "1";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text3 += "01";
				}
				else
				{
					text3 += "02";
				}
				text3 += Utils.FitStringLength(boleto.Cedente.Cpfcnpj, 14, 14, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia.CopiarTextoDaDireitaParaEsquerda(3) + boleto.Cedente.ContaBancaria.DigitoAgencia, 4, 4, '0', 0, true, true, true);
				text3 += "00";
				text3 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 5, 5, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
				text3 += "    ";
				text3 += "0000";
				text3 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 25, 25, ' ', 0, true, true, false);
				text3 += Utils.FitStringLength(boleto.NossoNumeroSemFormatacao, 8, 8, '0', 0, true, true, true);
				if (boleto.Moeda == 9)
				{
					text3 += "0000000000000";
				}
				text3 += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(text, 21, 21, ' ', 0, true, true, true);
				if (boleto.Moeda == 9)
				{
					text3 += "I";
				}
				text3 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text3 += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text3 += boleto.DataVencimento.ToString("ddMMyy");
				text3 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += "341";
				text3 += "00000";
				text3 += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento), 2, 2, '0', 0, true, true, true);
				text3 += boleto.Aceite;
				text3 += boleto.DataDocumento.ToString("ddMMyy");
				int num = boleto.Instrucoes.Count;
				if (num != 0)
				{
					if (num != 1)
					{
						text3 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text3 += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
					}
					else
					{
						text3 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
						text3 += "  ";
					}
				}
				else
				{
					text3 += "    ";
				}
				text3 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += ((boleto.ValorDesconto > 0m) ? ((boleto.DataDesconto == DateTime.MinValue) ? "000000" : boleto.DataDesconto.ToString("ddMMyy")) : "000000");
				text3 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += "0000000000000";
				text3 += "0000000000000";
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text3 += "01";
				}
				else
				{
					text3 += "02";
				}
				text3 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 30, 30, ' ', 0, true, true, false);
				text3 += text2;
				text3 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro.Trim(),
					" ",
					boleto.Sacado.Endereco.Numero.Trim(),
					" ",
					boleto.Sacado.Endereco.Complemento
				}), 40, 40, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 12, 12, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text3 += new string(' ', 30);
				text3 += "    ";
				text3 += boleto.DataJurosMora.ToString("ddMMyy");
				try
				{
					num = boleto.Instrucoes[0].Codigo;
					if (num <= 42)
					{
						if (num <= 34)
						{
							if (num != 9 && num != 34)
							{
								goto IL_06E4;
							}
						}
						else if (num != 35 && num != 42)
						{
							goto IL_06E4;
						}
					}
					else if (num <= 82)
					{
						if (num != 81 && num != 82)
						{
							goto IL_06E4;
						}
					}
					else if (num != 91 && num != 92)
					{
						goto IL_06E4;
					}
					text3 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0785;
					IL_06E4:
					num = boleto.Instrucoes[1].Codigo;
					if (num <= 42)
					{
						if (num <= 34)
						{
							if (num != 9 && num != 34)
							{
								goto IL_0779;
							}
						}
						else if (num != 35 && num != 42)
						{
							goto IL_0779;
						}
					}
					else if (num <= 82)
					{
						if (num != 81 && num != 82)
						{
							goto IL_0779;
						}
					}
					else if (num != 91 && num != 92)
					{
						goto IL_0779;
					}
					text3 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true).ToUpper();
					goto IL_0785;
					IL_0779:
					text3 += "00";
					IL_0785:;
				}
				catch (Exception)
				{
					text3 += "00";
				}
				text3 += " ";
				text3 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text4;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00062750 File Offset: 0x00060950
		public override string GerarDetalheDadosMultaRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = string.Empty;
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
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do DETALHE com dados do pagador/avalista do arquivo de REMESSA.", ex);
			}
			return text2;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheDadosMultaRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000627C8 File Offset: 0x000609C8
		public string GerarDetalheDadosMultaRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text2;
			try
			{
				string text = new string(' ', 371);
				base.GerarDetalheDadosPagadorAvalistaRemessa(boleto, numeroRegistro, tipoArquivo);
				text2 = Utils.SubstituiCaracteresEspeciais("2" + "2" + Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) + Utils.FitStringLength(boleto.PercMulta.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true) + text + Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000628A4 File Offset: 0x00060AA4
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

		// Token: 0x06000787 RID: 1927 RVA: 0x000629A0 File Offset: 0x00060BA0
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

		// Token: 0x06000788 RID: 1928 RVA: 0x00062A64 File Offset: 0x00060C64
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

		// Token: 0x06000789 RID: 1929 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessa240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00062ADC File Offset: 0x00060CDC
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

		// Token: 0x0600078B RID: 1931 RVA: 0x00062B48 File Offset: 0x00060D48
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(110, 6));
				int num2 = Utils.ToInt32(registro.Substring(295, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(37, 25),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2)),
					DescricaoOcorrencia = this.RetornarDescricaoDaOcorrencia(registro.Substring(108, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##"))
				};
				detalheRetornoCNAB.NossoNumero = string.Concat(new string[]
				{
					registro.Substring(82, 3),
					"/",
					registro.Substring(85, 8),
					"-",
					registro.Substring(93, 1)
				});
				decimal num3 = Convert.ToUInt64(registro.Substring(281, 13));
				detalheRetornoCNAB.OutrosCreditos = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(175, 13));
				detalheRetornoCNAB.TarifaCobranca = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPrincipal = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num6 / 100m;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num2.ToString("##-##-##"));
				detalheRetornoCNAB.ValorPago = detalheRetornoCNAB.ValorPrincipal;
				switch (detalheRetornoCNAB.CodigoOcorrencia)
				{
				case 2:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_029F;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(377, 8).Trim(), BancoItau.TipoDeRejeicao.Entradas);
					goto IL_029F;
				case 6:
				case 7:
				case 8:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_029F;
				case 9:
				case 10:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(377, 8).Trim(), BancoItau.TipoDeRejeicao.Baixas);
					goto IL_029F;
				case 15:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(377, 8).Trim(), BancoItau.TipoDeRejeicao.Baixas);
					goto IL_029F;
				case 16:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					detalheRetornoCNAB.MotivosRejeicao = this.RetornarDescricaoDosMotivosDeRejeicoes(registro.Substring(377, 8).Trim(), BancoItau.TipoDeRejeicao.Instrucoes);
					goto IL_029F;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_029F:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO/CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00062E28 File Offset: 0x00061028
		private string RetornarDescricaoDosMotivosDeRejeicoes(string codigosDeRejeicoes, BancoItau.TipoDeRejeicao tipoDeRejeicao)
		{
			string[] array = Regex.Split(codigosDeRejeicoes, "(.{2})", RegexOptions.IgnorePatternWhitespace);
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.Length > 0)
				{
					switch (tipoDeRejeicao)
					{
					case BancoItau.TipoDeRejeicao.Entradas:
						text = text + this.RetornarMotivoDaRejeicaoEntradasRejeitadas(text2) + "\n";
						break;
					case BancoItau.TipoDeRejeicao.Instrucoes:
						text = text + this.RetornarMotivoDaRejeicaoInstrucoesRejeitadas(text2) + "\n";
						break;
					case BancoItau.TipoDeRejeicao.Baixas:
						text = text + this.RetornarMotivoDaRejeicaoBaixasRejeitadas(text2) + "\n";
						break;
					default:
						return string.Empty;
					}
				}
			}
			return text;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00062EBC File Offset: 0x000610BC
		public string RetornarDescricaoDaOcorrencia(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2279835011U)
			{
				if (num <= 468396612U)
				{
					if (num <= 401139041U)
					{
						if (num <= 350806184U)
						{
							if (num != 216585232U)
							{
								if (num != 334175660U)
								{
									if (num == 350806184U)
									{
										if (codigo == "61")
										{
											return "61-Tarifa Emissão Aviso de Movimentação de Títulos";
										}
									}
								}
								else if (codigo == "18")
								{
									return "18-Cobrança contratual-Instruções/Alterações rejeitadas/pendentes";
								}
							}
							else if (codigo == "69")
							{
								return "69-Cheque Devolvido";
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
										return "60-Entrada Rejeitada Carnê";
									}
								}
							}
							else if (codigo == "19")
							{
								return "19-Confirma Recebimento Instrução de Protesto";
							}
						}
						else if (num != 384361422U)
						{
							if (num == 401139041U)
							{
								if (codigo == "62")
								{
									return "62-Débito Mensal de Tarifa-Aviso de Movimentação de Títulos";
								}
							}
						}
						else if (codigo == "63")
						{
							return "63-Título Sustado Judicialmente";
						}
					}
					else if (num <= 434694279U)
					{
						if (num <= 418063755U)
						{
							if (num != 401286136U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										return "15-Baixas rejeitadas";
									}
								}
							}
							else if (codigo == "14")
							{
								return "14-Vencimento Alterado";
							}
						}
						else if (num != 418210850U)
						{
							if (num == 434694279U)
							{
								if (codigo == "64")
								{
									return "64-Entrada Confirmada com Rateio de Crédito";
								}
							}
						}
						else if (codigo == "09")
						{
							return "09-Baixa simples";
						}
					}
					else if (num <= 434988469U)
					{
						if (num != 434841374U)
						{
							if (num == 434988469U)
							{
								if (codigo == "08")
								{
									return "08-Liquidação em cartório";
								}
							}
						}
						else if (codigo == "16")
						{
							return "16-Instruções rejeitadas";
						}
					}
					else if (num != 451618993U)
					{
						if (num == 468396612U)
						{
							if (codigo == "10")
							{
								return "10-Baixa por ter sido liquidado";
							}
						}
					}
					else if (codigo == "17")
					{
						return "17-Alteração/Exclusão de dados rejeitados";
					}
				}
				else if (num <= 535654183U)
				{
					if (num <= 501951850U)
					{
						if (num <= 485174231U)
						{
							if (num != 468543707U)
							{
								if (num == 485174231U)
								{
									if (codigo == "11")
									{
										return "11-Em Ser (Só no retorno mensal)";
									}
								}
							}
							else if (codigo == "06")
							{
								return "06-Liquidação normal";
							}
						}
						else if (num != 485321326U)
						{
							if (num == 501951850U)
							{
								if (codigo == "12")
								{
									return "12-Abatimento Concedido";
								}
							}
						}
						else if (codigo == "05")
						{
							return "05-Alteração de dados-Baixa";
						}
					}
					else if (num <= 518729469U)
					{
						if (num != 502098945U)
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
							return "04-Alteração de Dados-Nova entrada ou Alteração/Exclusão de dados acatada";
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
				else if (num <= 2262910297U)
				{
					if (num <= 2246132678U)
					{
						if (num != 2229355059U)
						{
							if (num == 2246132678U)
							{
								if (codigo == "52")
								{
									return "52-Tarifa Mensal Baixas na Carteira";
								}
							}
						}
						else if (codigo == "51")
						{
							return "51-Tarifa Mensal Ref a Entradas Bancos Correspondentes na Carteira";
						}
					}
					else if (num != 2247118416U)
					{
						if (num == 2262910297U)
						{
							if (codigo == "53")
							{
								return "53-Tarifa Mensal Baixas em Bancos Correspondentes na Carteira";
							}
						}
					}
					else if (codigo == "32")
					{
						return "32-Baixa por ter sido Protestado";
					}
				}
				else if (num <= 2263896035U)
				{
					if (num != 2263057392U)
					{
						if (num == 2263896035U)
						{
							if (codigo == "33")
							{
								return "33-Custas de Protesto";
							}
						}
					}
					else if (codigo == "43")
					{
						return "43-Débito Mensal de Tarifas-Manutenção de Títulos Vencidos";
					}
				}
				else if (num != 2279687916U)
				{
					if (num == 2279835011U)
					{
						if (codigo == "42")
						{
							return "42-Débito Mensal de Tarifas-Outras Instruções";
						}
					}
				}
				else if (codigo == "54")
				{
					return "54-Tarifa Mensal de Liquidações na Carteira";
				}
			}
			else if (num <= 2347784130U)
			{
				if (num <= 2313537344U)
				{
					if (num <= 2296612630U)
					{
						if (num != 2280673654U)
						{
							if (num != 2296465535U)
							{
								if (num == 2296612630U)
								{
									if (codigo == "41")
									{
										return "41-Débito Mensal de Tarifas-Extrato de Posição(B4EP/B4OX)";
									}
								}
							}
							else if (codigo == "55")
							{
								return "55-Tarifa Mensal de Liquidações em Bancos Correspondentes na Carteira";
							}
						}
						else if (codigo == "30")
						{
							return "30-Débito Mensal de Tarifas (Para Entradas e Baixas)";
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
									return "56-Custas de Irregularidade";
								}
							}
						}
						else if (codigo == "25")
						{
							return "25-Alegações do Sacado";
						}
					}
					else if (num != 2313390249U)
					{
						if (num == 2313537344U)
						{
							if (codigo == "76")
							{
								return "76-Cheque Compensado";
							}
						}
					}
					else if (codigo == "40")
					{
						return "40-Tarifa Mensal de Emissão de Boleto/Tarifa Mensal de Envio de Duplicata";
					}
				}
				else if (num <= 2330167868U)
				{
					if (num <= 2314375987U)
					{
						if (num != 2314228892U)
						{
							if (num == 2314375987U)
							{
								if (codigo == "24")
								{
									return "24-Instrução de Protesto Rejeitada/Sustada/Pendente";
								}
							}
						}
						else if (codigo == "36")
						{
							return "36-Custas de Edital";
						}
					}
					else if (num != 2330020773U)
					{
						if (num == 2330167868U)
						{
							if (codigo == "47")
							{
								return "47-Baixa com Transferência para Protesto";
							}
						}
					}
					else if (codigo == "57")
					{
						return "57-Instrução Cancelada";
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
								return "27-Tarifa de Extrato Posição";
							}
						}
					}
					else if (codigo == "37")
					{
						return "37-Tarifa de Emissão de Boleto/Tarifa de Envio de Duplicata";
					}
				}
				else if (num != 2346945487U)
				{
					if (num == 2347784130U)
					{
						if (codigo == "34")
						{
							return "34-Custas de Sustação";
						}
					}
				}
				else if (codigo == "46")
				{
					return "56-Débito Mensal de Tarifas-Sustação de Protesto";
				}
			}
			else if (num <= 2381486463U)
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
									return "59-Baixa por Crédito em C/C Através do SISPAG";
								}
							}
						}
						else if (codigo == "26")
						{
							return "26-Tarifa de Aviso de Cobrança";
						}
					}
					else if (num != 2363723106U)
					{
						if (num == 2364561749U)
						{
							if (codigo == "35")
							{
								return "35-Custas de Cartório Distribuidor";
							}
						}
					}
					else if (codigo == "45")
					{
						return "45-Débito Mensal de Tarifas-Protesto";
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
								return "44-Débito Mensal de Tarifas-Outras Ocorrências";
							}
						}
					}
					else if (codigo == "21")
					{
						return "21-Confirma Recebimento Instrução de Não Protestar";
					}
				}
				else if (num != 2380647820U)
				{
					if (num == 2381486463U)
					{
						if (codigo == "20")
						{
							return "20-Confirma Recebimento Instrução Sustação de Protesto/Tarifa";
						}
					}
				}
				else if (codigo == "72")
				{
					return "72-Baixa por Crédito em C/C Através do SISPAG sem Título Correspondente";
				}
			}
			else if (num <= 2430980677U)
			{
				if (num <= 2398264082U)
				{
					if (num != 2397425439U)
					{
						if (num == 2398264082U)
						{
							if (codigo == "23")
							{
								return "23-Título enviado a Cartório/Tarifa";
							}
						}
					}
					else if (codigo == "73")
					{
						return "73-Confirmação de Entrada na Cobrança Simples-Entrada Não Aceita na Cobrança Contratual";
					}
				}
				else if (num != 2414894606U)
				{
					if (num == 2430980677U)
					{
						if (codigo == "71")
						{
							return "71-Entrada Registrada-Aguardando Avaliação";
						}
					}
				}
				else if (codigo == "38")
				{
					return "38-Tarifa de Instrução";
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
							return "48-Custas de Sustação Judicial";
						}
					}
				}
				else if (codigo == "39")
				{
					return "39-Tarifa de Ocorrências";
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return "28-Tarifa de Relação das Liquidações";
					}
				}
			}
			else if (codigo == "29")
			{
				return "29-Tarifa de Manutenção de Títulos Vencidos";
			}
			return "";
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00063998 File Offset: 0x00061B98
		public string RetornarMotivoDaRejeicaoEntradasRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 518876564U)
			{
				if (num <= 417916660U)
				{
					if (num <= 335455588U)
					{
						if (num <= 216585232U)
						{
							if (num != 184457017U)
							{
								if (num != 201234636U)
								{
									if (num == 216585232U)
									{
										if (codigo == "69")
										{
											return "69-CARTEIRA INVÁLIDA PARA TÍTULOS COM RATEIO DE CRÉDITO";
										}
									}
								}
								else if (codigo == "90")
								{
									return "90-COBRANÇA MENSAGEM - NÚMERO DA LINHA DA MENSAGEM INVÁLIDO";
								}
							}
							else if (codigo == "97")
							{
								return "97-COBRANÇA MENSAGEM SEM MENSAGEM (SÓ DE CAMPOS FIXOS), PORÉM COM REGISTRO DO TIPO 7 OU 8";
							}
						}
						else if (num != 233362851U)
						{
							if (num != 334175660U)
							{
								if (num == 335455588U)
								{
									if (codigo == "98")
									{
										return "98-REGISTRO MENSAGEM SEM FLASH CADASTRADO OU FLASH INFORMADO DIFERENTE DO CADASTRADO";
									}
								}
							}
							else if (codigo == "18")
							{
								return "18-DATA DE ENTRADA INVÁLIDA PARA OPERAR COM ESTA CARTEIRA";
							}
						}
						else if (codigo == "68")
						{
							return "68-CARTEIRA INVÁLIDA";
						}
					}
					else if (num <= 352233207U)
					{
						if (num != 350806184U)
						{
							if (num != 350953279U)
							{
								if (num == 352233207U)
								{
									if (codigo == "99")
									{
										return "99-CONTA DE COBRANÇA COM FLASH CADASTRADO E SEM REGISTRO DE MENSAGEM CORRESPONDENTE";
									}
								}
							}
							else if (codigo == "19")
							{
								return "19-OCORRÊNCIA INVÁLIDA";
							}
						}
						else if (codigo == "61")
						{
							return "61-JUROS DE MORA MAIOR QUE O PERMITIDO";
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
									return "63-VALOR DA IMPORTÂNCIA POR DIA DE DESCONTO (IDD) NÃO PERMITIDO";
								}
							}
						}
						else if (codigo == "60")
						{
							return "60-VALOR DO ABATIMENTO INVÁLIDO";
						}
					}
					else if (num != 401286136U)
					{
						if (num == 417916660U)
						{
							if (codigo == "65")
							{
								return "65-TAXA INVÁLIDA (VENDOR)";
							}
						}
					}
					else if (codigo == "14")
					{
						return "14-NOSSO NÚMERO JÁ REGISTRADO NO CADASTRO DO BANCO OU FORA DA FAIXA";
					}
				}
				else if (num <= 468249517U)
				{
					if (num <= 434694279U)
					{
						if (num != 418063755U)
						{
							if (num != 418210850U)
							{
								if (num == 434694279U)
								{
									if (codigo == "64")
									{
										return "64-DATA DE EMISSÃO DO TÍTULO INVÁLIDA";
									}
								}
							}
							else if (codigo == "09")
							{
								return "09-AGÊNCIA ENCERRADA";
							}
						}
						else if (codigo == "15")
						{
							return "15-NOSSO NÚMERO EM DUPLICIDADE NO MESMO MOVIMENTO";
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
									return "67-VALOR DO TÍTULO/QUANTIDADE DE MOEDA INVÁLIDO";
								}
							}
						}
						else if (codigo == "08")
						{
							return "08-NÃO INFORMADO OU DESLOCADO";
						}
					}
					else if (num != 451766088U)
					{
						if (num == 468249517U)
						{
							if (codigo == "66")
							{
								return "66-INVALIDA/FORA DE PRAZO DE OPERAÇÃO (MÍNIMO OU MÁXIMO)";
							}
						}
					}
					else if (codigo == "07")
					{
						return "07-VALOR DO TÍTULO MAIOR QUE 10.000.000,00";
					}
				}
				else if (num <= 485321326U)
				{
					if (num != 468396612U)
					{
						if (num != 485174231U)
						{
							if (num == 485321326U)
							{
								if (codigo == "05")
								{
									return "05-PRAZO DA OPERAÇÃO MENOR QUE PRAZO MÍNIMO OU MAIOR QUE O MÁXIMO";
								}
							}
						}
						else if (codigo == "11")
						{
							return "11-CEP NÃO NUMÉRICO";
						}
					}
					else if (codigo == "10")
					{
						return "10-NÃO INFORMADO OU DESLOCADO";
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
								return "04-SIGLA DO ESTADO INVÁLIDA";
							}
						}
					}
					else if (codigo == "12")
					{
						return "12-NOME NÃO INFORMADO OU DESLOCADO (BANCOS CORRESPONDENTES)";
					}
				}
				else if (num != 518729469U)
				{
					if (num == 518876564U)
					{
						if (codigo == "03")
						{
							return "03-NÃO FOI POSSÍVEL ATRIBUIR A AGÊNCIA PELO CEP OU CEP INVÁLIDO";
						}
					}
				}
				else if (codigo == "13")
				{
					return "13-CEP INCOMPATÍVEL COM A SIGLA DO ESTADO";
				}
			}
			else if (num <= 2332139344U)
			{
				if (num <= 2296465535U)
				{
					if (num <= 2279687916U)
					{
						if (num != 2246132678U)
						{
							if (num != 2262910297U)
							{
								if (num == 2279687916U)
								{
									if (codigo == "54")
									{
										return "54-BANCO CORRESPONDENTE - TÍTULO COM VENCIMENTO INFERIOR A 15 DIAS";
									}
								}
							}
							else if (codigo == "53")
							{
								return "53-EMPRESA NÃO ACEITA BANCO CORRESPONDENTE - COBRANÇA MENSAGEM";
							}
						}
						else if (codigo == "52")
						{
							return "52-EMPRESA NÃO ACEITA BANCO CORRESPONDENTE";
						}
					}
					else if (num != 2279835011U)
					{
						if (num != 2280673654U)
						{
							if (num == 2296465535U)
							{
								if (codigo == "55")
								{
									return "55-CEP NÃO PERTENCE À DEPOSITÁRIA INFORMADA";
								}
							}
						}
						else if (codigo == "30")
						{
							return "30-ENTRADAS BLOQUEADAS, CONTA SUSPENSA EM COBRANÇA";
						}
					}
					else if (codigo == "42")
					{
						return "42-NOSSO NÚMERO FORA DE FAIXA";
					}
				}
				else if (num <= 2314228892U)
				{
					if (num != 2297451273U)
					{
						if (num != 2313243154U)
						{
							if (num == 2314228892U)
							{
								if (codigo == "36")
								{
									return "36-QUANTIDADE DE MOEDA INCOMPATÍVEL COM VALOR DO TÍTULO";
								}
							}
						}
						else if (codigo == "56")
						{
							return "56-VENCTO SUPERIOR A 180 DIAS DA DATA DE ENTRADA";
						}
					}
					else if (codigo == "31")
					{
						return "31-CONTA NÃO TEM PERMISSÃO PARA PROTESTAR (CONTATE SEU GERENTE)";
					}
				}
				else if (num <= 2331006511U)
				{
					if (num != 2330020773U)
					{
						if (num == 2331006511U)
						{
							if (codigo == "37")
							{
								return "37-NÃO NUMÉRICO OU IGUAL A ZEROS";
							}
						}
					}
					else if (codigo == "57")
					{
						return "57-CEP SÓ DEPOSITÁRIA BCO DO BRASIL COM VENCTO INFERIOR A 8 DIAS";
					}
				}
				else if (num != 2331153606U)
				{
					if (num == 2332139344U)
					{
						if (codigo == "87")
						{
							return "87-REGISTRO TIPO 4 SEM INFORMAÇÃO DE AGÊNCIAS/CONTAS BENEFICIÁRIAS DO RATEIO";
						}
					}
				}
				else if (codigo == "27")
				{
					return "27-CNPJ DO CEDENTE INAPTO";
				}
			}
			else if (num <= 2399249820U)
			{
				if (num <= 2364561749U)
				{
					if (num != 2347931225U)
					{
						if (num != 2348916963U)
						{
							if (num == 2364561749U)
							{
								if (codigo == "35")
								{
									return "35-IOF MAIOR QUE 5%";
								}
							}
						}
						else if (codigo == "86")
						{
							return "86-CÓDIGO DO TIPO DE VALOR INVÁLIDO / NÃO PREVISTO PARA TÍTULOS COM RATEIO DE CRÉDITO";
						}
					}
					else if (codigo == "26")
					{
						return "26-AGÊNCIA/CONTA NÃO LIBERADA PARA OPERAR COM COBRANÇA";
					}
				}
				else if (num <= 2365694582U)
				{
					if (num != 2364708844U)
					{
						if (num == 2365694582U)
						{
							if (codigo == "85")
							{
								return "85-AGÊNCIA/CONTA DO CEDENTE É CONTRATUAL / RATEIO DE CRÉDITO NÃO PERMITIDO";
							}
						}
					}
					else if (codigo == "21")
					{
						return "21-CARTEIRA NÃO ACEITA DEPOSITÁRIA CORRESPONDENTE / ESTADO DA AGÊNCIA DIFERENTE DO ESTADO DO SACADO / AG. COBRADORA NÃO CONSTA NO CADASTRO OU ENCERRANDO";
					}
				}
				else if (num != 2382472201U)
				{
					if (num == 2399249820U)
					{
						if (codigo == "83")
						{
							return "83-VALOR DO TÍTULO MENOR QUE A SOMA DOS VALORES ESTIPULADOS PARA RATEIO";
						}
					}
				}
				else if (codigo == "84")
				{
					return "84-AGÊNCIA/CONTA BENEFICIÁRIA DO RATEIO É A CENTRALIZADORA DE CRÉDITO DO CEDENTE";
				}
			}
			else if (num <= 2416027439U)
			{
				if (num != 2414203058U)
				{
					if (num != 2415041701U)
					{
						if (num == 2416027439U)
						{
							if (codigo == "82")
							{
								return "82-DESCONTO/ABATIMENTO NÃO PERMITIDO PARA TÍTULOS COM RATEIO DE CRÉDITO";
							}
						}
					}
					else if (codigo == "22")
					{
						return "22-CARTEIRA NÃO PERMITIDA (NECESSÁRIO CADASTRAR FAIXA LIVRE)";
					}
				}
				else if (codigo == "70")
				{
					return "70-CEDENTE NÃO CADASTRADO PARA FAZER RATEIO DE CRÉDITO";
				}
			}
			else if (num <= 2449582677U)
			{
				if (num != 2432805058U)
				{
					if (num == 2449582677U)
					{
						if (codigo == "80")
						{
							return "80-QUANTIDADE DE CONTAS BENEFICIÁRIAS DO RATEIO MAIOR DO QUE O PERMITIDO (MÁXIMO DE 30 CONTAS POR TÍTULO)";
						}
					}
				}
				else if (codigo == "81")
				{
					return "81-CONTA PARA RATEIO DE CRÉDITO INVÁLIDA / NÃO PERTENCE AO ITAÚ";
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2548424010U)
				{
					if (codigo == "78")
					{
						return "78-DUPLICIDADE DE AGÊNCIA/CONTA BENEFICIÁRIA DO RATEIO DE CRÉDITO";
					}
				}
			}
			else if (codigo == "29")
			{
				return "29-CATEGORIA DA CONTA INVÁLIDA";
			}
			return "";
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000642D4 File Offset: 0x000624D4
		public string RetornarMotivoDaRejeicaoInstrucoesRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 2297451273U)
			{
				if (num <= 468543707U)
				{
					if (num <= 418063755U)
					{
						if (num != 350953279U)
						{
							if (num != 401286136U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										return "15-CNPJ/CPF INFORMADO SEM NOME DO SACADOR/AVALISTA";
									}
								}
							}
							else if (codigo == "14")
							{
								return "14-REGISTRO EM DUPLICIDADE";
							}
						}
						else if (codigo == "19")
						{
							return "19-VALOR DO ABATIMENTO MAIOR QUE 90% DO VALOR DO TÍTULO";
						}
					}
					else if (num != 418210850U)
					{
						if (num != 468396612U)
						{
							if (num == 468543707U)
							{
								if (codigo == "06")
								{
									return "06-NOSSO NÚMERO IGUAL A ZEROS";
								}
							}
						}
						else if (codigo == "10")
						{
							return "10-VALOR DO ABATIMENTO IGUAL OU MAIOR QUE O VALOR DO TÍTULO";
						}
					}
					else if (codigo == "09")
					{
						return "09-CNPJ/CPF DO SACADOR/AVALISTA INVÁLIDO";
					}
				}
				else if (num <= 552431802U)
				{
					if (num != 485174231U)
					{
						if (num != 518876564U)
						{
							if (num == 552431802U)
							{
								if (codigo == "01")
								{
									return "01-INSTRUÇÃO/OCORRÊNCIA NÃO EXISTENTE";
								}
							}
						}
						else if (codigo == "03")
						{
							return "03-CONTA NÃO TEM PERMISSÃO PARA PROTESTAR (CONTATE SEU GERENTE)";
						}
					}
					else if (codigo == "11")
					{
						return "11-SEGUNDA INSTRUÇÃO/OCORRÊNCIA NÃO EXISTENTE";
					}
				}
				else if (num <= 2263896035U)
				{
					if (num != 2247118416U)
					{
						if (num == 2263896035U)
						{
							if (codigo == "33")
							{
								return "33-ALTERAÇÃO DE VENCIMENTO IGUAL À REGISTRADA NO SISTEMA OU QUE TORNA O TÍTULO VENCIDO";
							}
						}
					}
					else if (codigo == "32")
					{
						return "32-DEPOSITÁRIA DO TÍTULO = 9999 OU CARTEIRA NÃO ACEITA PROTESTO";
					}
				}
				else if (num != 2280673654U)
				{
					if (num == 2297451273U)
					{
						if (codigo == "31")
						{
							return "31-EXISTE UMA OCORRÊNCIA DO SACADO QUE BLOQUEIA A INSTRUÇÃO";
						}
					}
				}
				else if (codigo == "30")
				{
					return "30-EXISTE UMA INSTRUÇÃO DE NÃO PROTESTAR ATIVA PARA O TÍTULO";
				}
			}
			else if (num <= 2347931225U)
			{
				if (num <= 2314375987U)
				{
					if (num != 2297598368U)
					{
						if (num != 2314228892U)
						{
							if (num == 2314375987U)
							{
								if (codigo == "24")
								{
									return "24-INSTRUÇÃO INCOMPATÍVEL - EXISTE INSTRUÇÃO DE PROTESTO PARA O TÍTULO";
								}
							}
						}
						else if (codigo == "36")
						{
							return "36-TÍTULO SOFRENDO ALTERAÇÃO DE CONTROLE (AGÊNCIA/CONTA/CARTEIRA/NOSSO NÚMERO)";
						}
					}
					else if (codigo == "25")
					{
						return "25-INSTRUÇÃO INCOMPATÍVEL – NÃO EXISTE INSTRUÇÃO DE PROTESTO PARA O TÍTULO";
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
								return "27-INSTRUÇÃO NÃO ACEITA POR NÃO TER SIDO EMITIDA A ORDEM DE PROTESTO AO CARTÓRIO";
							}
						}
					}
					else if (codigo == "37")
					{
						return "37-INSTRUÇÃO NÃO PERMITIDA PARA A CARTEIRA";
					}
				}
				else if (num != 2347784130U)
				{
					if (num == 2347931225U)
					{
						if (codigo == "26")
						{
							return "26-INSTRUÇÃO NÃO ACEITA POR JÁ TER SIDO EMITIDA A ORDEM DE PROTESTO AO CARTÓRIO";
						}
					}
				}
				else if (codigo == "34")
				{
					return "34-INSTRUÇÃO DE EMISSÃO DE AVISO DE COBRANÇA PARA TÍTULO VENCIDO ANTES DO VENCIMENTO";
				}
			}
			else if (num <= 2398264082U)
			{
				if (num != 2364561749U)
				{
					if (num != 2364708844U)
					{
						if (num == 2398264082U)
						{
							if (codigo == "23")
							{
								return "23-INSTRUÇÃO NÃO ACEITA POR TER SIDO EMITIDO ÚLTIMO AVISO AO SACADO";
							}
						}
					}
					else if (codigo == "21")
					{
						return "21-TÍTULO NÃO REGISTRADO NO SISTEMA";
					}
				}
				else if (codigo == "35")
				{
					return "35-SOLICITAÇÃO DE CANCELAMENTO DE INSTRUÇÃO INEXISTENTE";
				}
			}
			else if (num <= 2415041701U)
			{
				if (num != 2414894606U)
				{
					if (num == 2415041701U)
					{
						if (codigo == "22")
						{
							return "22-TÍTULO BAIXADO OU LIQUIDADO";
						}
					}
				}
				else if (codigo == "38")
				{
					return "38-INSTRUÇÃO NÃO PERMITIDA PARA TÍTULO COM RATEIO DE CRÉDITO";
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return "28-JÁ EXISTE UMA MESMA INSTRUÇÃO CADASTRADA ANTERIORMENTE PARA O TÍTULO";
					}
				}
			}
			else if (codigo == "29")
			{
				return "29-VALOR LÍQUIDO + VALOR DO ABATIMENTO DIFERENTE DO VALOR DO TÍTULO REGISTRADO";
			}
			return "";
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0006477C File Offset: 0x0006297C
		public string RetornarMotivoDaRejeicaoBaixasRejeitadas(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 468543707U)
			{
				if (num <= 451766088U)
				{
					if (num != 434988469U)
					{
						if (num == 451766088U)
						{
							if (codigo == "07")
							{
								return "07-COBRANÇA PRAZO CURTO – SOLICITAÇÃO DE BAIXA P/ TÍTULO NÃO REGISTRADO NO SISTEMA";
							}
						}
					}
					else if (codigo == "08")
					{
						return "08-SOLICITAÇÃO DE BAIXA PARA TÍTULO EM FLOATING";
					}
				}
				else if (num != 468396612U)
				{
					if (num == 468543707U)
					{
						if (codigo == "06")
						{
							return "06-SOLICITAÇÃO DE BAIXA PARA TÍTULO NÃO REGISTRADO NO SISTEMA";
						}
					}
				}
				else if (codigo == "10")
				{
					return "10-VALOR DO TITULO FAZ PARTE DE GARANTIA DE EMPRESTIMO";
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
							return "05-SOLICITAÇÃO DE BAIXA PARA TÍTULO JÁ BAIXADO OU LIQUIDADO";
						}
					}
				}
				else if (codigo == "11")
				{
					return "11-PAGO ATRAVÉS DO SISPAG POR CRÉDITO EM C/C E NÃO BAIXADO";
				}
			}
			else if (num != 502098945U)
			{
				if (num == 552431802U)
				{
					if (codigo == "01")
					{
						return "01-CARTEIRA/Nº NÚMERO NÃO NUMÉRICO";
					}
				}
			}
			else if (codigo == "04")
			{
				return "04-NOSSO NÚMERO EM DUPLICIDADE NUM MESMO MOVIMENTO";
			}
			return "";
		}

		// Token: 0x0400065D RID: 1629
		private int _dacBoleto;

		// Token: 0x0400065E RID: 1630
		private int _dacNossoNumero;

		// Token: 0x0400065F RID: 1631
		private readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

		// Token: 0x0200019C RID: 412
		private enum TipoDeRejeicao
		{
			// Token: 0x04000750 RID: 1872
			Entradas,
			// Token: 0x04000751 RID: 1873
			Instrucoes,
			// Token: 0x04000752 RID: 1874
			Baixas
		}
	}
}

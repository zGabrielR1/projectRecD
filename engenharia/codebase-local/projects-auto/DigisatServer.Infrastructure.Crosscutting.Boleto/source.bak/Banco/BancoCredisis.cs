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
	// Token: 0x0200015E RID: 350
	internal sealed class BancoCredisis : AbstractBanco, IBanco
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x000393DC File Offset: 0x000375DC
		internal BancoCredisis()
		{
			this.Codigo = 97;
			this.Digito = "0";
			this.Nome = "Credisis";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00039410 File Offset: 0x00037610
		public override string NomeArquivoRemessa
		{
			get
			{
				return string.Format("CREDISIS{0:ddMM}{1}.TXT", DateTime.Now.Date, this.SequenciaRemessaDiaria);
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00039444 File Offset: 0x00037644
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			this.FormatoAgenciaCodigoCedente = string.Format("{0}{1}/{2}-{3}", new object[]
			{
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(3), 3),
				boleto.Cedente.ContaBancaria.DigitoAgencia,
				Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta.ToString(CultureInfo.InvariantCulture), 8),
				boleto.Cedente.ContaBancaria.DigitoConta.ToString(CultureInfo.InvariantCulture)
			});
			if (Utils.ToString(boleto.NossoNumero) == string.Empty)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (boleto.NossoNumero.Length > 6)
			{
				throw new NotImplementedException("Quantidade de dígitos do nosso número ultrapassa 6 posições.");
			}
			if (boleto.NossoNumero.Length < 6)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 6);
			}
			if (string.IsNullOrEmpty(boleto.LocalPagamento))
			{
				boleto.LocalPagamento = boleto.LocalPagamento + this.Nome + ". Após o vencimento, somente no Credisis";
			}
			boleto.DigitoNossoNumero = this.CalcularDigitoNossoNumero(boleto);
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumeroImpressao(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00039588 File Offset: 0x00037788
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return AbstractBanco.Mod11(string.Format("{0}", boleto.Cedente.CpfCnpjFormatado)).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000395BC File Offset: 0x000377BC
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 10);
			boleto.CodigoBarra.Codigo = string.Format("{0:000}{1}{2}{3}00000{4}", new object[]
			{
				this.Codigo,
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto),
				text,
				this.FormataCampoLivre(boleto)
			});
			this._dacBoleto = this.Mod11Credisis(boleto.CodigoBarra.Codigo);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000396AC File Offset: 0x000378AC
		public string FormataCampoLivre(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Concat(new string[]
			{
				string.Format("{0:000}", this.Codigo),
				string.Format("{0}", boleto.DigitoNossoNumero),
				string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(3).PreencherComCaractereEsquerda(3, '0')),
				string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoAgencia.PreencherComCaractereEsquerda(1, '0')),
				string.Format("{0}", boleto.Cedente.Convenio.CopiarTexto(6).PreencherComCaractereEsquerda(6, '0')),
				string.Format("{0}", boleto.NossoNumero)
			});
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00039778 File Offset: 0x00037978
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.CodigoBarra.Codigo.Substring(0, 3);
			string text2 = boleto.CodigoBarra.Codigo.Substring(3, 1);
			string text3 = boleto.CodigoBarra.Codigo.Substring(19, 5);
			string text4 = AbstractBanco.Mod10(text + text2 + text3).ToString();
			string text5 = string.Format("{0}{1}{2}.{3}{4} ", new object[]
			{
				text,
				text2,
				text3.Substring(0, 1),
				text3.Substring(1, 4),
				text4
			});
			string text6 = boleto.CodigoBarra.Codigo.Substring(24, 10);
			string text7 = AbstractBanco.Mod10(text6).ToString();
			string text8 = string.Format("{0}.{1}{2} ", text6.Substring(0, 5), text6.Substring(5, 5), text7);
			string text9 = boleto.CodigoBarra.Codigo.Substring(34, 10);
			string text10 = AbstractBanco.Mod10(text9).ToString();
			string text11 = string.Format("{0}.{1}{2} ", text9.Substring(0, 5), text9.Substring(5, 5), text10);
			string text12 = this._dacBoleto.ToString();
			string text13 = string.Format("{0} ", text12);
			string text14 = AbstractBanco.FatorVencimento(boleto).ToString();
			string text15 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text15 = Utils.FormatCode(text15, 10);
			string text16 = string.Format("{0}{1}", text14, text15);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text5, text8, text11, text13, text16 });
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00039944 File Offset: 0x00037B44
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = string.Concat(new string[]
			{
				string.Format("{0:000}", this.Codigo),
				string.Format("{0}", boleto.DigitoNossoNumero),
				string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(3).PreencherComCaractereEsquerda(3, '0')),
				string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoAgencia.PreencherComCaractereEsquerda(1, '0')),
				string.Format("{0}", boleto.Cedente.Convenio.CopiarTexto(6).PreencherComCaractereEsquerda(6, '0')),
				string.Format("{0}", boleto.NossoNumero)
			});
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00039A16 File Offset: 0x00037C16
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = string.Format("{0} {1}", boleto.NumeroDocumento, boleto.NumeroParcela);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00039A3C File Offset: 0x00037C3C
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			string text = " ";
			base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);
			switch (tipoArquivo)
			{
			case TipoArquivo.CNAB240:
				text = this.GerarHeaderRemessaCnab240();
				break;
			case TipoArquivo.CNAB400:
				text = this.GerarHeaderRemessaCnab400(cedente, numeroArquivoRemessa);
				break;
			case TipoArquivo.Outro:
				throw new Exception("Tipo de arquivo inexistente.");
			}
			return text;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarHeaderRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00039A94 File Offset: 0x00037C94
		public string GerarHeaderRemessaCnab400(Cedente cedente, int numeroArquivoRemessa)
		{
			string text2;
			try
			{
				string text = new string(' ', 284);
				text2 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       " + string.Format("{0}{1}", cedente.ContaBancaria.Agencia.CopiarTexto(3).PreencherComCaractereEsquerda(3, '0'), cedente.ContaBancaria.DigitoAgencia) + " " + Utils.FitStringLength(Convert.ToString(cedente.ContaBancaria.Conta), 8, 8, '0', 0, true, true, true) + Utils.FitStringLength(Convert.ToString(cedente.ContaBancaria.DigitoConta), 1, 1, '0', 0, true, true, true) + "      " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "097" + "CENTRALCREDI   " + DateTime.Now.ToString("ddMMyy") + Utils.FitStringLength(numeroArquivoRemessa.ToString(), 7, 7, '0', 0, true, true, true) + text + "001" + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text2;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00039BF0 File Offset: 0x00037DF0
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

		// Token: 0x060005CF RID: 1487 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00039C68 File Offset: 0x00037E68
		public string GerarDetalheRemessaCnab400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text8;
			try
			{
				string text = new string(' ', 43);
				string text2 = new string(' ', 25);
				string text3 = new string(' ', 11);
				string text4 = new string(' ', 9);
				string text5 = new string(' ', 6);
				string text6 = new string(' ', 5);
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text7 = "1";
				if (boleto.Cedente.Cpfcnpj.Length <= 11)
				{
					text7 += "01";
				}
				else
				{
					text7 += "02";
				}
				text7 += boleto.Cedente.Cpfcnpj.PreencherComCaractereDireita(14, ' ');
				text7 += string.Format("{0}{1}", boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(3), boleto.Cedente.ContaBancaria.DigitoAgencia).PreencherComCaractereDireita(4, ' ');
				text7 += Utils.FitStringLength(Convert.ToString(boleto.Cedente.ContaBancaria.Conta), 8, 8, '0', 0, true, true, true);
				text7 += Utils.FitStringLength(Convert.ToString(boleto.Cedente.ContaBancaria.DigitoConta), 1, 1, '0', 0, true, true, true);
				text7 += Utils.FitStringLength(boleto.IdentificacaoTituloEmpresa.ToString(CultureInfo.InvariantCulture), 26, 26, ' ', 0, true, true, false);
				text7 = string.Concat(new string[]
				{
					text7,
					string.Format("{0:000}", this.Codigo),
					string.Format("{0}", boleto.DigitoNossoNumero),
					string.Format("{0}", boleto.Cedente.ContaBancaria.Agencia.CopiarTexto(3).PreencherComCaractereEsquerda(3, '0')),
					string.Format("{0}", boleto.Cedente.ContaBancaria.DigitoAgencia.PreencherComCaractereEsquerda(1, '0')),
					string.Format("{0}", boleto.Cedente.Convenio.CopiarTexto(6).PreencherComCaractereEsquerda(6, '0')),
					string.Format("{0}", boleto.NossoNumero)
				});
				text7 += (boleto.SolicitacaoDeBaixa ? "02" : (boleto.AlteracaoVencimento ? "06" : "01"));
				text7 += DateTime.Now.ToString("ddMMyy");
				text7 += text5;
				text7 += string.Format("{0:00}", boleto.NumeroParcela);
				text7 += "3";
				text7 += "3";
				text7 += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);
				text7 += "2";
				try
				{
					if (boleto.Instrucoes.Count == 0)
					{
						text7 += "00";
						text7 += "03";
					}
					else
					{
						int num = boleto.Instrucoes[0].Codigo;
						if (num != 3)
						{
							if (num == 99)
							{
								text7 += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
								text7 += "01";
							}
							else
							{
								num = boleto.Instrucoes[1].Codigo;
								if (num == 99)
								{
									text7 += Utils.FitStringLength(boleto.Instrucoes[1].QuantidadeDias.ToString(CultureInfo.InvariantCulture), 2, 2, '0', 0, true, true, true);
									text7 += "01";
								}
								else
								{
									text7 += "00";
									text7 += "03";
								}
							}
						}
						else
						{
							text7 += "00";
							text7 += "03";
						}
					}
				}
				catch (Exception)
				{
					text7 += "00";
					text7 += "03";
				}
				text7 += text4;
				text7 += boleto.NumeroDocumento.Split(new char[] { ' ' })[0].PreencherComCaractereEsquerda(10, '0');
				text7 += boleto.DataVencimento.ToString("ddMMyy");
				text7 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text7 += boleto.DataVencimento.AddDays(1.0).ToString("ddMMyy");
				text7 += text6;
				text7 += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyy"), 6, 6, ' ', 0, true, true, false);
				text7 += " ";
				if (boleto.Sacado.CpfCnpj.Length <= 11)
				{
					text7 += "01";
				}
				else
				{
					text7 += "02";
				}
				text7 += boleto.Sacado.CpfCnpj.PreencherComCaractereDireita(14, ' ').ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(new char[] { ' ' }), 40, 40, ' ', 0, true, true, false).ToUpper();
				text7 += text2;
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.Logradouro.Trim(), 35, 35, ' ', 0, true, true, false).ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.Numero.Trim(), 6, 6, ' ', 0, true, true, false).ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(new char[] { ' ' }), 25, 25, ' ', 0, true, true, false).ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 25, 25, ' ', 0, true, true, false).ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text7 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep.Replace("-", ""), 8, 8, '0', 0, true, true, true);
				text7 += text3;
				text7 += text;
				text7 += " ";
				text7 += Utils.FitStringLength(numeroRegistro.ToString(CultureInfo.InvariantCulture), 6, 6, '0', 0, true, true, true);
				text7 = Utils.SubstituiCaracteresEspeciais(text7);
				text8 = text7;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text8;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0003A3FC File Offset: 0x000385FC
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
				throw new Exception("", ex);
			}
			return text2;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessaCnab240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0003A474 File Offset: 0x00038674
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

		// Token: 0x060005D4 RID: 1492 RVA: 0x0003A4E0 File Offset: 0x000386E0
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				int num = Utils.ToInt32(registro.Substring(78, 6));
				int num2 = Utils.ToInt32(registro.Substring(175, 6));
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro)
				{
					UsoEmpresa = registro.Substring(30, 26),
					CodigoOcorrencia = Utils.ToInt32(registro.Substring(76, 2)),
					DataOcorrencia = Utils.ToDateTime(num.ToString("##-##-##")),
					Conta = Utils.ToInt32(registro.Substring(21, 8)),
					DacConta = Utils.ToInt32(registro.Substring(29, 1)),
					NossoNumero = Utils.ToString(registro.Substring(70, 6)),
					DACNossoNumero = Utils.ToInt32(registro.Substring(59, 1))
				};
				decimal num3 = Convert.ToUInt64(registro.Substring(165, 10));
				detalheRetornoCNAB.DespesasDeCobranca = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(240, 13));
				detalheRetornoCNAB.Descontos = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(253, 13));
				detalheRetornoCNAB.ValorPago = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(266, 13));
				detalheRetornoCNAB.JurosMora = num6 / 100m;
				detalheRetornoCNAB.DataCredito = Utils.ToDateTime(num2.ToString("##-##-##"));
				switch (detalheRetornoCNAB.CodigoOcorrencia)
				{
				case 1:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RegistradoNoBanco;
					goto IL_01D2;
				case 2:
				case 5:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Liquidado;
					goto IL_01D2;
				case 3:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.Baixado;
					goto IL_01D2;
				case 6:
					detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.NaoRegistradoNoBanco;
					goto IL_01D2;
				}
				detalheRetornoCNAB.StatusProcessamentoDoBoleto = SituacaoBoleto.RemetidoAoBanco;
				IL_01D2:
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0003A6F4 File Offset: 0x000388F4
		public int Mod11Credisis(string seq)
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

		// Token: 0x0400063C RID: 1596
		private int _dacBoleto;
	}
}

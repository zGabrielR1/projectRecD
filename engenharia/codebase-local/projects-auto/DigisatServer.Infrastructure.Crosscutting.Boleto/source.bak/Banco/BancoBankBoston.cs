using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000158 RID: 344
	internal sealed class BancoBankBoston : AbstractBanco, IBanco
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x0002C8C4 File Offset: 0x0002AAC4
		internal BancoBankBoston()
		{
			this.Codigo = 479;
			this.Digito = "0";
			this.Nome = "BankBoston";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch
			{
				throw new Exception("Erro ao validar boletos.");
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002C92C File Offset: 0x0002AB2C
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar código de barras.", ex);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002C960 File Offset: 0x0002AB60
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar linha digitável.", ex);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002C994 File Offset: 0x0002AB94
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número", ex);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0002C9C8 File Offset: 0x0002ABC8
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar nosso número para impressão", ex);
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			try
			{
				throw new NotImplementedException("Função não implementada!");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar número do documento.", ex);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002CA30 File Offset: 0x0002AC30
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
					text = this.GerarHeaderRemessaCNAB240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarHeaderRemessaCNAB400(numeroConvenio, cedente, numeroArquivoRemessa);
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

		// Token: 0x06000544 RID: 1348 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		public string GerarHeaderRemessaCNAB240()
		{
			throw new NotImplementedException("Função não implementada!");
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
		public string GerarHeaderRemessaCNAB400(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
		{
			string text3;
			try
			{
				string text = new string(' ', 91);
				string text2 = new string(' ', 187);
				text3 = Utils.SubstituiCaracteresEspeciais("01REMESSA01COBRANCA       009" + numeroConvenio.ToString() + "         " + Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper() + "341" + "BANKBOSTON     " + DateTime.Now.ToString("ddMMyy") + "01600BPI" + text + "        " + text2 + "000001");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
			}
			return text3;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002CB90 File Offset: 0x0002AD90
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
					text = this.GerarDetalheRemessaCNAB240();
					break;
				case TipoArquivo.CNAB400:
					text = this.GerarDetalheRemessaCNAB400(boleto, numeroRegistro, tipoArquivo);
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

		// Token: 0x06000547 RID: 1351 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarDetalheRemessaCNAB240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0002CC14 File Offset: 0x0002AE14
		public string GerarDetalheRemessaCNAB400(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			string text4;
			try
			{
				base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);
				string text = new string(' ', 20);
				new string(' ', 21);
				new string(' ', 10);
				string text2 = new string(' ', 40);
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
				text3 += text;
				text3 = text3 + text + "     ";
				text3 += "000000000";
				text3 += "00000000000 ";
				text3 += "R$  ";
				text3 += text;
				CarteiraBankBoston carteiraBankBoston = new CarteiraBankBoston(Convert.ToInt32(boleto.Carteira));
				text3 += carteiraBankBoston.Tipo;
				text3 += "01";
				text3 += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
				text3 += boleto.DataVencimento.ToString("ddMMyy");
				text3 += Utils.FitStringLength(boleto.ValorBoleto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += "479     ";
				text3 += boleto.EspecieDocumento.Sigla;
				text3 += "N";
				text3 += DateTime.Now.ToString("ddMMyy");
				if (boleto.Instrucoes.Count > 1)
				{
					text3 += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(), 2, 2, '0', 0, true, true, true);
					if (boleto.Instrucoes[0].Codigo == 2 || boleto.Instrucoes[0].Codigo == 3)
					{
						text3 += "05";
					}
					else
					{
						text3 += "00";
					}
				}
				text3 += Utils.FitStringLength(boleto.JurosMora.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += boleto.DataVencimento.ToString("ddMMyy");
				text3 += Utils.FitStringLength(boleto.ValorDesconto.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(boleto.IOF.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				text3 += Utils.FitStringLength(boleto.Abatimento.ToString("0.00").Replace(",", ""), 13, 13, '0', 0, true, true, true);
				if (boleto.Sacado.CpfCnpj.Length > 11)
				{
					text3 += "01";
				}
				else
				{
					text3 += "02";
				}
				text3 += Utils.FitStringLength(boleto.Sacado.CpfCnpj, 14, 14, '0', 0, true, true, true).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Nome, 30, 30, ' ', 0, true, true, false);
				text3 += Utils.FitStringLength(string.Concat(new string[]
				{
					boleto.Sacado.Endereco.Logradouro,
					" ",
					boleto.Sacado.Endereco.Numero,
					" - ",
					boleto.Sacado.Endereco.Complemento,
					" ",
					boleto.Sacado.Endereco.Bairro
				}), 37, 37, ' ', 0, true, true, true).ToUpper();
				text3 += "               ";
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cep, 8, 8, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
				text3 += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
				text3 += text2;
				text3 += "   ";
				text3 += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);
				text3 = Utils.SubstituiCaracteresEspeciais(text3);
				text4 = text3;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
			}
			return text4;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0002D134 File Offset: 0x0002B334
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

		// Token: 0x0600054A RID: 1354 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public string GerarTrailerRemessa240()
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public string GerarTrailerRemessa400(int numeroRegistro)
		{
			string text2;
			try
			{
				string text = new string(' ', 393);
				text2 = Utils.SubstituiCaracteresEspeciais("9" + text + Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true));
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
			}
			return text2;
		}
	}
}

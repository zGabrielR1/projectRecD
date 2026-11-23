using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000165 RID: 357
	internal sealed class BancoBrb : AbstractBanco, IBanco
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x00049FFD File Offset: 0x000481FD
		internal BancoBrb()
		{
			this.Codigo = 70;
			this.Digito = "1";
			this.Nome = "Banco_BRB";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0004A030 File Offset: 0x00048230
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(), 3);
			int moeda = boleto.Moeda;
			string text2 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text2 = Utils.FormatCode(text2, 10);
			string text3 = AbstractBanco.FatorVencimento(boleto).ToString();
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				text,
				moeda,
				text3,
				text2,
				boleto.CodigoBarra.Chave
			});
			this._dacBoleto = BancoBrb.Mod11_CodigoBarra(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0004A13C File Offset: 0x0004833C
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(), 3);
			int moeda = boleto.Moeda;
			string text2 = boleto.CodigoBarra.Chave.Substring(0, 5);
			string text3 = boleto.CodigoBarra.Chave.Substring(5, 10);
			string text4 = boleto.CodigoBarra.Chave.Substring(15, 10);
			int num = BancoBrb.Mod10(text + moeda + text2);
			string text5 = string.Format("{0}.{1}{2} ", text + moeda + text2.Substring(0, 1), text2.Substring(1, 4), num);
			int num2 = BancoBrb.Mod10(text3);
			string text6 = string.Format("{0}.{1}{2} ", text3.Substring(0, 5), text3.Substring(5, 5), num2);
			int num3 = BancoBrb.Mod10(text4);
			string text7 = string.Format("{0}.{1}{2} ", text4.Substring(0, 5), text4.Substring(5, 5), num3);
			string text8 = string.Format("{0} {1}{2}", this._dacBoleto, AbstractBanco.FatorVencimento(boleto), Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10));
			boleto.CodigoBarra.LinhaDigitavel = text5 + text6 + text7 + text8;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0004A2BC File Offset: 0x000484BC
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0}{1}{2}", boleto.Categoria, boleto.NossoNumero, Utils.FormatCode(this.Codigo.ToString(), 3) + boleto.CodigoBarra.Chave.Substring(23, 2));
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0004A316 File Offset: 0x00048516
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = string.Format("{0}", boleto.NumeroDocumento);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0004A330 File Offset: 0x00048530
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			if (Utils.ToInt64(boleto.NossoNumero) == 0L)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (Convert.ToInt32(boleto.NossoNumero).ToString().Length > 6)
			{
				throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 6 números.");
			}
			if (Convert.ToInt32(boleto.NossoNumero).ToString().Length < 6)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 6);
			}
			if (boleto.Carteira != "COB")
			{
				throw new NotImplementedException("Carteira não implementada. Utilize a carteira COB.");
			}
			boleto.LocalPagamento += this.Nome;
			this.FormataChave(boleto);
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumero(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0004A410 File Offset: 0x00048610
		public void FormataChave(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string agencia = boleto.Cedente.ContaBancaria.Agencia;
			string text = boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta;
			boleto.Categoria = 1;
			string nossoNumero = boleto.NossoNumero;
			string text2 = Utils.FormatCode(this.Codigo.ToString(), 3);
			int num = BancoBrb.Mod10(string.Concat(new object[] { "000", agencia, text, 1, nossoNumero, text2 }));
			int num2 = BancoBrb.Mod11_NossoNumero(string.Concat(new object[] { "000", agencia, text, 1, nossoNumero, text2, num }), 7);
			if (num2 > 10)
			{
				num++;
				num2 -= 20;
			}
			boleto.CodigoBarra.Chave = string.Concat(new object[] { "000", agencia, text, 1, nossoNumero, text2, num, num2 });
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0004A548 File Offset: 0x00048748
		internal static int Mod11_CodigoBarra(string value, int Base)
		{
			int num = 0;
			int num2 = 2;
			for (int i = value.Length; i > 0; i--)
			{
				num += Convert.ToInt32(Strings.Mid(value, i, 1)) * num2;
				if (num2 == Base)
				{
					num2 = 2;
				}
				else
				{
					num2++;
				}
			}
			int num3;
			if (num % 11 == 0 || num % 11 == 10 || num % 11 == 1)
			{
				num3 = 1;
			}
			else
			{
				num3 = 11 - num % 11;
			}
			return num3;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0004A5AC File Offset: 0x000487AC
		internal static int Mod11_NossoNumero(string value, int Base)
		{
			int num = 0;
			int num2 = 2;
			for (int i = value.Length; i > 0; i--)
			{
				num += Convert.ToInt32(Strings.Mid(value, i, 1)) * num2;
				if (num2 == Base)
				{
					num2 = 2;
				}
				else
				{
					num2++;
				}
			}
			int num3;
			if (num % 11 > 1)
			{
				num3 = 11 - num % 11;
			}
			else if (num % 11 == 1)
			{
				int num4 = Utils.ToInt32(Strings.Mid(value, value.Length, value.Length - 1));
				num4++;
				if (num4 == 10)
				{
					num4 = 0;
				}
				num3 = BancoBrb.Mod11_NossoNumero(Strings.Mid(value, 1, value.Length - 1) + num4, 7);
				num3 += 20;
			}
			else
			{
				num3 = num % 11;
			}
			return num3;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0004A660 File Offset: 0x00048860
		internal new static int Mod10(string seq)
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
			return (10 - num % 10) % 10;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00031274 File Offset: 0x0002F474
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			throw new Exception("Função não implementada.");
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0004A6B8 File Offset: 0x000488B8
		public override DetalheRetornoCNAB400 LerDetalheRetornoCNAB400(string registro)
		{
			DetalheRetornoCNAB400 detalheRetornoCNAB2;
			try
			{
				DetalheRetornoCNAB400 detalheRetornoCNAB = new DetalheRetornoCNAB400(registro);
				detalheRetornoCNAB.IdentificacaoDoRegistro = Utils.ToInt32(registro.Substring(0, 1));
				detalheRetornoCNAB.TipoInscricao = Utils.ToInt32(registro.Substring(1, 2));
				detalheRetornoCNAB.CgcCpf = registro.Substring(3, 14);
				detalheRetornoCNAB.ContaCorrente = Utils.ToInt32(registro.Substring(20, 17));
				detalheRetornoCNAB.NossoNumero = registro.Substring(70, 12);
				detalheRetornoCNAB.SeuNumero = registro.Substring(92, 13);
				detalheRetornoCNAB.Instrucao = Utils.ToInt32(registro.Substring(108, 2));
				detalheRetornoCNAB.NumeroDocumento = registro.Substring(128, 12);
				detalheRetornoCNAB.CodigoRateio = Utils.ToInt32(registro.Substring(140, 4));
				detalheRetornoCNAB.DataOcorrencia = Utils.ToDateTime(Utils.ToInt32(registro.Substring(110, 8)).ToString("##-##-####"));
				detalheRetornoCNAB.DataVencimento = Utils.ToDateTime(Utils.ToInt32(registro.Substring(148, 8)).ToString("##-##-####"));
				decimal num = Convert.ToInt64(registro.Substring(156, 13));
				detalheRetornoCNAB.ValorTitulo = num / 100m;
				detalheRetornoCNAB.BancoCobrador = Utils.ToInt32(registro.Substring(163, 3));
				detalheRetornoCNAB.AgenciaCobradora = Utils.ToInt32(registro.Substring(172, 5));
				detalheRetornoCNAB.EspecieTitulo = registro.Substring(177, 2);
				decimal num2 = Convert.ToUInt64(registro.Substring(179, 13));
				detalheRetornoCNAB.DespesasDeCobranca = num2 / 100m;
				decimal num3 = Convert.ToUInt64(registro.Substring(192, 13));
				detalheRetornoCNAB.OutrasDespesas = num3 / 100m;
				decimal num4 = Convert.ToUInt64(registro.Substring(205, 13));
				detalheRetornoCNAB.Juros = num4 / 100m;
				decimal num5 = Convert.ToUInt64(registro.Substring(218, 13));
				detalheRetornoCNAB.IOF = num5 / 100m;
				decimal num6 = Convert.ToUInt64(registro.Substring(231, 13));
				detalheRetornoCNAB.Abatimentos = num6 / 100m;
				decimal num7 = Convert.ToUInt64(registro.Substring(244, 13));
				detalheRetornoCNAB.Descontos = num7 / 100m;
				decimal num8 = Convert.ToUInt64(registro.Substring(257, 13));
				detalheRetornoCNAB.ValorPago = num8 / 100m;
				decimal num9 = Convert.ToUInt64(registro.Substring(270, 13));
				detalheRetornoCNAB.OutrosDebitos = num9 / 100m;
				decimal num10 = Convert.ToUInt64(registro.Substring(283, 13));
				detalheRetornoCNAB.OutrosCreditos = num10 / 100m;
				detalheRetornoCNAB.DataLiquidacao = Utils.ToDateTime(Utils.ToInt32(registro.Substring(299, 8)).ToString("##-##-####"));
				detalheRetornoCNAB.MotivosRejeicao = registro.Substring(364, 30);
				detalheRetornoCNAB.Sequencial = Utils.ToInt32(registro.Substring(394, 6));
				detalheRetornoCNAB2 = detalheRetornoCNAB;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
			}
			return detalheRetornoCNAB2;
		}

		// Token: 0x04000648 RID: 1608
		private int _dacBoleto;
	}
}

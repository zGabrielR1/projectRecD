using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x0200015A RID: 346
	internal sealed class BancoBasa : AbstractBanco, IBanco
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00030474 File Offset: 0x0002E674
		internal BancoBasa()
		{
			this.Codigo = 3;
			this.Digito = "5";
			this.Nome = "Basa";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000304B0 File Offset: 0x0002E6B0
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.NossoNumero + "000000";
			string text2 = Utils.FormatCode(this.Codigo.ToString(), 3);
			string text3 = boleto.Moeda.ToString("0");
			string text4 = Strings.Right(boleto.Cedente.ContaBancaria.Agencia, 3) + boleto.Cedente.ContaBancaria.DigitoAgencia;
			string text5 = boleto.Cedente.Codigo.ToString().Substring(0, 1);
			string text6 = this.Mod10_LinhaDigitavel(text2 + text3 + text4 + text5).ToString("0");
			string text7 = string.Concat(new string[] { text2, text3, text4, text5, text6 });
			string text8 = boleto.Cedente.Codigo.ToString().Substring(1, 3);
			string text9 = text.Substring(0, 7);
			string text10 = this.Mod10_LinhaDigitavel(text8 + text9).ToString("0");
			string text11 = text8 + text9 + text10;
			string text12 = text.Substring(7, 9);
			string text13 = this.Mod10_LinhaDigitavel(text12 + "8").ToString("0");
			string text14 = text12 + "8" + text13;
			string text15 = boleto.CodigoBarra.Codigo.Substring(4, 1);
			string text16 = AbstractBanco.FatorVencimento(boleto).ToString();
			string text17 = Utils.FormatCode(boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", ""), 10);
			string text18 = text16 + text17;
			string text19 = string.Concat(new string[] { text7, text11, text14, text15, text18 });
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[]
			{
				text19.Substring(0, 5),
				".",
				text19.Substring(5, 5),
				" ",
				text19.Substring(10, 5),
				".",
				text19.Substring(15, 6),
				"  ",
				text19.Substring(21, 5),
				".",
				text19.Substring(26, 6),
				" ",
				text19.Substring(32, 1),
				" ",
				text19.Substring(33, 14)
			});
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00030758 File Offset: 0x0002E958
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = Utils.FormatCode(this.Codigo.ToString(), 3);
			int moeda = boleto.Moeda;
			string text2 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text2 = Utils.FormatCode(text2, 10);
			string text3 = AbstractBanco.FatorVencimento(boleto).ToString();
			string text4 = Strings.Right(boleto.Cedente.ContaBancaria.Agencia, 3) + boleto.Cedente.ContaBancaria.DigitoAgencia;
			string text5 = boleto.Cedente.Codigo.CopiarTexto(Sentido.CopiarDaEsquerdaParaDireita, 4).PreencherComCaractere(Direcao.Esquerda, 4, '0');
			string text6 = boleto.NossoNumero + "000000";
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}", new object[] { text, moeda, text3, text2 });
			CodigoBarra codigoBarra = boleto.CodigoBarra;
			codigoBarra.Codigo += string.Format("{0}{1}{2}{3}", new object[] { text4, text5, text6, "8" });
			this._dacBoleto = this.Mod11_CodigoBarra(boleto.CodigoBarra.Codigo, 9);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000043A9 File Offset: 0x000025A9
		public string FormataCampoLivre(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000308EE File Offset: 0x0002EAEE
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = string.Format("{0}", boleto.NumeroDocumento);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00030906 File Offset: 0x0002EB06
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumero = string.Format("{0:0000}{1}", boleto.Cedente.Codigo, boleto.NumeroDocumento);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000043A9 File Offset: 0x000025A9
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0003092C File Offset: 0x0002EB2C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			if (Utils.ToInt64(boleto.NossoNumero) == 0L)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (boleto.NossoNumero.Length > 10)
			{
				throw new NotImplementedException("A quantidade de dígitos do nosso número para a carteira " + boleto.Carteira + ", são 10 números.");
			}
			if (boleto.NossoNumero.Length < 10)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 10);
			}
			if (boleto.Carteira != "CNR")
			{
				throw new NotImplementedException("Carteira não implementada. Utilize a carteira CNR.");
			}
			boleto.LocalPagamento += this.Nome;
			this.FormataNossoNumero(boleto);
			this.FormataNumeroDocumento(boleto);
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000309F0 File Offset: 0x0002EBF0
		private int Mod11_CodigoBarra(string value, int Base)
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

		// Token: 0x06000576 RID: 1398 RVA: 0x00030A54 File Offset: 0x0002EC54
		private int Mod10_LinhaDigitavel(string seq)
		{
			int num = 0;
			int num2 = 2;
			for (int i = seq.Length; i > 0; i--)
			{
				string text = (Convert.ToInt32(Strings.Mid(seq, i, 1)) * num2).ToString();
				for (int j = 1; j <= text.Length; j++)
				{
					num += Convert.ToInt32(Strings.Mid(text, j, 1));
				}
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

		// Token: 0x04000634 RID: 1588
		private string _dacNossoNumero = string.Empty;

		// Token: 0x04000635 RID: 1589
		private int _dacBoleto;
	}
}

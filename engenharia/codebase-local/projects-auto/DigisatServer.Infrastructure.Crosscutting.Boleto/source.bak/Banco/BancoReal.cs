using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000170 RID: 368
	internal sealed class BancoReal : AbstractBanco, IBanco
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x000648B9 File Offset: 0x00062AB9
		internal BancoReal()
		{
			this.Codigo = 356;
			this.Digito = "5";
			this.Nome = "Banco Real";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000648F8 File Offset: 0x00062AF8
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			if (boleto.Carteira != "57")
			{
				throw new NotImplementedException("Carteira não implementada. Carteiras implementadas 57.");
			}
			if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
			{
				throw new Exception("Número da agência inválido");
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length < 7)
			{
				boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);
			}
			if (boleto.NossoNumero.Length < 13)
			{
				boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 13);
			}
			this._dacNossoNumero = this.CalcularDigitoNossoNumero(boleto);
			this._dacContaCorrente = AbstractBanco.Mod10(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta);
			boleto.Cedente.ContaBancaria.DigitoConta = this._dacContaCorrente.ToString();
			boleto.LocalPagamento += this.Nome;
			if (Utils.ToInt64(boleto.NossoNumero) == 0L)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNossoNumero(boleto);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00009047 File Offset: 0x00007247
		public override void FormataNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00064A41 File Offset: 0x00062C41
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			throw new NotImplementedException("Função do fomata número do documento não implementada.");
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00064A50 File Offset: 0x00062C50
		public string CampoLivre(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return string.Concat(new object[]
			{
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.Conta,
				AbstractBanco.Mod10(boleto.NossoNumero + boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta),
				boleto.NossoNumero
			});
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00064AD0 File Offset: 0x00062CD0
		public string CalcularDigitoNossoNumero(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			return AbstractBanco.Mod10(boleto.NossoNumero.Substring(0, 9) + boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta).ToString();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00064B20 File Offset: 0x00062D20
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = string.Empty;
			string text2 = Utils.FormatCode(boleto.Banco.Codigo.ToString(), 3);
			string text3 = boleto.Moeda.ToString();
			string text4 = this.CampoLivre(boleto).Substring(0, 5);
			string text5 = AbstractBanco.Mod10(text2 + text3 + text4).ToString();
			text = string.Format("{0}{1}{2}.", text2, text3, text4.Substring(0, 1));
			text += string.Format("{0}{1} ", text4.Substring(1, 4), text5);
			string text6 = this.CampoLivre(boleto).Substring(5, 10);
			string text7 = AbstractBanco.Mod10(text6).ToString();
			text += string.Format("{0}.", text6.Substring(0, 5));
			text += string.Format("{0}{1} ", text6.Substring(5, 5), text7);
			string text8 = this.CampoLivre(boleto).Substring(15, 10);
			string text9 = AbstractBanco.Mod10(text8).ToString();
			text += string.Format("{0}.", text8.Substring(0, 5));
			text += string.Format("{0}{1} ", text8.Substring(5, 5), text9);
			string text10 = this._dacBoleto.ToString();
			text += string.Format(" {0} ", text10);
			string text11;
			if (boleto.ValorBoleto != 0m)
			{
				text11 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
				text11 = AbstractBanco.FatorVencimento(boleto) + Utils.FormatCode(text11, 10);
			}
			else
			{
				text11 = "000";
			}
			text += text11;
			boleto.CodigoBarra.LinhaDigitavel = text;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00064D08 File Offset: 0x00062F08
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 14);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", new object[]
			{
				this.Codigo,
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto),
				text.Substring(4, 10),
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.Conta,
				AbstractBanco.Mod10(boleto.NossoNumero + boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta),
				boleto.NossoNumero
			});
			this._dacBoleto = AbstractBanco.Mod11(Strings.Left(boleto.CodigoBarra.Codigo, 4) + Strings.Right(boleto.CodigoBarra.Codigo, 39), 9, 0);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public override string GerarDetalheRemessa(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
		{
			throw new NotImplementedException("Função não implementada.");
		}

		// Token: 0x04000660 RID: 1632
		private string _dacNossoNumero = string.Empty;

		// Token: 0x04000661 RID: 1633
		private int _dacContaCorrente;

		// Token: 0x04000662 RID: 1634
		private int _dacBoleto;
	}
}

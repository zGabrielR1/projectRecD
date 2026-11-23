using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Banco
{
	// Token: 0x02000175 RID: 373
	internal sealed class BancoSudameris : AbstractBanco, IBanco
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0006E156 File Offset: 0x0006C356
		internal BancoSudameris()
		{
			this.Codigo = 347;
			this.Digito = "6";
			this.Nome = "Banco_Sudameris";
			this.DescricaoBeneficiarioSacadorAvalista = "Sacador / Avalista";
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0006E18C File Offset: 0x0006C38C
		public override void ValidaBoleto(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			if (Utils.ToInt32(boleto.NossoNumero) == 0)
			{
				throw new NotImplementedException("Nosso número inválido");
			}
			if (boleto.Cedente.ContaBancaria.Conta.Length != 7)
			{
				throw new Exception("O número da conta corrente do cedente são 7 números.");
			}
			boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 13);
			this._dacDigitaoCobranca = AbstractBanco.Mod10(boleto.NossoNumero + boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta);
			boleto.LocalPagamento += this.Nome;
			this.FormataCodigoBarra(boleto);
			this.FormataLinhaDigitavel(boleto);
			this.FormataNumeroDocumento(boleto);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0006E24C File Offset: 0x0006C44C
		public override void FormataLinhaDigitavel(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			Utils.FormatCode(boleto.NumeroDocumento, 7);
			Utils.FormatCode(boleto.Cedente.Codigo.ToString(), 5);
			string text = Utils.FormatCode(this.Codigo.ToString(), 3);
			string text2 = boleto.Moeda.ToString();
			string agencia = boleto.Cedente.ContaBancaria.Agencia;
			string text3 = boleto.Cedente.ContaBancaria.Conta.Substring(0, 1);
			string text4 = AbstractBanco.Mod10(text + text2 + agencia + text3).ToString();
			string text5 = boleto.Cedente.ContaBancaria.Conta.Substring(1, 6);
			string text6 = string.Format(" {0} ", this._dacDigitaoCobranca);
			string text7 = AbstractBanco.FatorVencimento(boleto).ToString();
			string text8 = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			string text9 = string.Format("{0}{1}{2}.", text, text2, agencia.Substring(0, 1));
			text9 += string.Format("{0}{1}{2} ", agencia.Substring(1, 3), text3, text4);
			text8 = Utils.FormatCode(text8, 10);
			string text10 = text7 + text8;
			string text11 = this._dacDigitaoCobranca.ToString();
			string text12 = boleto.NossoNumero.Substring(0, 3);
			string text13 = AbstractBanco.Mod10(text5 + text11 + text12).ToString();
			string text14 = string.Format("{0}.", text5.Substring(0, 5));
			text14 += string.Format("{0}{1}{2}{3} ", new object[]
			{
				text5.Substring(5, 1),
				text11,
				text12,
				text13
			});
			string text15 = boleto.NossoNumero.Substring(3, 10);
			string text16 = AbstractBanco.Mod10(text15).ToString();
			string text17 = string.Format("{0}.{1}{2}", text15.Substring(0, 5), text15.Substring(5, 5), text16);
			boleto.CodigoBarra.LinhaDigitavel = string.Concat(new string[] { text9, text14, text17, text6, text10 });
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00009047 File Offset: 0x00007247
		public void FormataNossoNumero()
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00030B69 File Offset: 0x0002ED69
		public override void FormataNossoNumeroImpressao(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NossoNumeroImpressao = ((!string.IsNullOrEmpty(boleto.DigitoNossoNumero)) ? (boleto.NossoNumero + "-" + boleto.DigitoNossoNumero) : boleto.NossoNumero);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0006E491 File Offset: 0x0006C691
		public override void FormataNumeroDocumento(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			boleto.NumeroDocumento = Utils.FormatCode(boleto.NossoNumero, 13);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
		public override void FormataCodigoBarra(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto)
		{
			string text = boleto.ValorBoleto.ToString("f2").Replace(",", "").Replace(".", "");
			text = Utils.FormatCode(text, 10);
			boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", new object[]
			{
				this.Codigo,
				boleto.Moeda,
				AbstractBanco.FatorVencimento(boleto),
				text,
				boleto.Cedente.ContaBancaria.Agencia,
				boleto.Cedente.ContaBancaria.Conta,
				this._dacDigitaoCobranca,
				boleto.NossoNumero
			});
			this._dacBoleto = AbstractBanco.Mod11(boleto.CodigoBarra.Codigo, 9, 0);
			boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + this._dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
		}

		// Token: 0x0400066D RID: 1645
		private int _dacBoleto;

		// Token: 0x0400066E RID: 1646
		private int _dacDigitaoCobranca;
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000177 RID: 375
	public abstract class AbstractArquivoRemessa : IArquivoRemessa
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000842 RID: 2114 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		// (remove) Token: 0x06000843 RID: 2115 RVA: 0x0006E608 File Offset: 0x0006C808
		public event EventHandler<LinhaDeArquivoGeradaArgs> LinhaDeArquivoGerada;

		// Token: 0x06000844 RID: 2116 RVA: 0x00002CF8 File Offset: 0x00000EF8
		protected AbstractArquivoRemessa()
		{
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0006E640 File Offset: 0x0006C840
		protected AbstractArquivoRemessa(TipoArquivo tipoArquivo)
		{
			if (tipoArquivo == TipoArquivo.CNAB240)
			{
				this._arquivoRemessa = new ArquivoRemessaCNAB240();
				this._arquivoRemessa.LinhaDeArquivoGerada += this._arquivoRemessa_LinhaDeArquivoGerada;
				return;
			}
			if (tipoArquivo != TipoArquivo.CNAB400)
			{
				throw new NotImplementedException("Arquivo não implementado.");
			}
			this._arquivoRemessa = new ArquivoRemessaCNAB400();
			this._arquivoRemessa.LinhaDeArquivoGerada += this._arquivoRemessa_LinhaDeArquivoGerada;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0006E6AD File Offset: 0x0006C8AD
		private void _arquivoRemessa_LinhaDeArquivoGerada(object sender, LinhaDeArquivoGeradaArgs e)
		{
			this.OnLinhaGerada(e.Boleto, e.Linha, e.TipoLinha);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0006E6C7 File Offset: 0x0006C8C7
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0006E6CF File Offset: 0x0006C8CF
		public virtual string NumeroConvenio
		{
			get
			{
				return this._numeroConvenio;
			}
			set
			{
				this._numeroConvenio = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0006E6E0 File Offset: 0x0006C8E0
		public virtual int NumeroArquivoRemessa
		{
			get
			{
				return this._numeroArquivoRemessa;
			}
			set
			{
				this._numeroArquivoRemessa = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0006E6E9 File Offset: 0x0006C8E9
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0006E6F1 File Offset: 0x0006C8F1
		public virtual Boletos Boletos
		{
			get
			{
				return this._boletos;
			}
			protected set
			{
				this._boletos = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0006E6FA File Offset: 0x0006C8FA
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0006E702 File Offset: 0x0006C902
		public virtual Cedente Cedente
		{
			get
			{
				return this._cedente;
			}
			protected set
			{
				this._cedente = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0006E70B File Offset: 0x0006C90B
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0006E713 File Offset: 0x0006C913
		public virtual IBanco Banco
		{
			get
			{
				return this._banco;
			}
			protected set
			{
				this._banco = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0006E71C File Offset: 0x0006C91C
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x0006E724 File Offset: 0x0006C924
		public virtual TipoArquivo TipoArquivo { get; protected set; }

		// Token: 0x06000853 RID: 2131 RVA: 0x0006E72D File Offset: 0x0006C92D
		public virtual void GerarArquivoRemessa(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, string diretorioArquivoRemessa, int numeroArquivoRemessa)
		{
			this._banco = banco;
			this._cedente = cedente;
			this._boletos = boletos;
			this._numeroConvenio = numeroConvenio;
			this._numeroArquivoRemessa = numeroArquivoRemessa;
			this._arquivoRemessa.GerarArquivoRemessa(numeroConvenio, banco, cedente, boletos, diretorioArquivoRemessa, numeroArquivoRemessa);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0006E768 File Offset: 0x0006C968
		public virtual void OnLinhaGerada(global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto boleto, string linha, EnumTipodeLinha tipoLinha)
		{
			try
			{
				EventHandler<LinhaDeArquivoGeradaArgs> linhaDeArquivoGerada = this.LinhaDeArquivoGerada;
				if (linhaDeArquivoGerada != null)
				{
					linhaDeArquivoGerada(this, new LinhaDeArquivoGeradaArgs(boleto, linha, tipoLinha));
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar evento.", ex);
			}
		}

		// Token: 0x04000670 RID: 1648
		private Boletos _boletos;

		// Token: 0x04000671 RID: 1649
		private Cedente _cedente;

		// Token: 0x04000672 RID: 1650
		private IBanco _banco;

		// Token: 0x04000673 RID: 1651
		private string _numeroConvenio;

		// Token: 0x04000674 RID: 1652
		private int _numeroArquivoRemessa;

		// Token: 0x04000675 RID: 1653
		private readonly IArquivoRemessa _arquivoRemessa;
	}
}

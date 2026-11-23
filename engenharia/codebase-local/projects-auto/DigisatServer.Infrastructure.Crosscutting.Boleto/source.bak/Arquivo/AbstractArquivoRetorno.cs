using System;
using System.IO;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000178 RID: 376
	public abstract class AbstractArquivoRetorno
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000855 RID: 2133 RVA: 0x0006E7B0 File Offset: 0x0006C9B0
		// (remove) Token: 0x06000856 RID: 2134 RVA: 0x0006E7E8 File Offset: 0x0006C9E8
		public event EventHandler<LinhaDeArquivoLidaArgs> LinhaDeArquivoLida;

		// Token: 0x06000857 RID: 2135 RVA: 0x00002CF8 File Offset: 0x00000EF8
		protected AbstractArquivoRetorno()
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0006E820 File Offset: 0x0006CA20
		protected AbstractArquivoRetorno(TipoArquivo tipoArquivo)
		{
			if (tipoArquivo == TipoArquivo.CNAB240)
			{
				this._arquivoRetorno = new ArquivoRetornoCNAB240();
				this._arquivoRetorno.LinhaDeArquivoLida += this.ArquivoRemessa_LinhaDeArquivoLidaCNAB240;
				return;
			}
			if (tipoArquivo != TipoArquivo.CNAB400)
			{
				throw new NotImplementedException("Arquivo não implementado.");
			}
			this._arquivoRetorno = new ArquivoRetornoCNAB400();
			this._arquivoRetorno.LinhaDeArquivoLida += this.ArquivoRemessa_LinhaDeArquivoLidaCNAB400;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0006E88D File Offset: 0x0006CA8D
		private void ArquivoRemessa_LinhaDeArquivoLidaCNAB240(object sender, LinhaDeArquivoLidaArgs e)
		{
			this.OnLinhaLida(e.Detalhe as DetalheRetornoCNAB240, e.Linha, e.TipoLinha);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0006E8AC File Offset: 0x0006CAAC
		private void ArquivoRemessa_LinhaDeArquivoLidaCNAB400(object sender, LinhaDeArquivoLidaArgs e)
		{
			this.OnLinhaLida(e.Detalhe as DetalheRetornoCNAB400, e.Linha);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0006E8C5 File Offset: 0x0006CAC5
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0006E8CD File Offset: 0x0006CACD
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0006E8D6 File Offset: 0x0006CAD6
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0006E8DE File Offset: 0x0006CADE
		public virtual TipoArquivo TipoArquivo { get; protected set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0006E8E7 File Offset: 0x0006CAE7
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0006E8EF File Offset: 0x0006CAEF
		public virtual DetalheRetornoCNAB400 DetalheRetornoCNAB400 { get; protected set; }

		// Token: 0x06000861 RID: 2145 RVA: 0x0006E8F8 File Offset: 0x0006CAF8
		public virtual void LerArquivoRetorno(IBanco banco, Stream arquivo)
		{
			this._banco = banco;
			this._arquivoRetorno.LerArquivoRetorno(banco, arquivo);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0006E910 File Offset: 0x0006CB10
		public virtual void OnLinhaLida(DetalheRetornoCNAB240 detalheRetornoCnab240, string linha, EnumTipodeLinhaLida tipoLinha)
		{
			try
			{
				EventHandler<LinhaDeArquivoLidaArgs> linhaDeArquivoLida = this.LinhaDeArquivoLida;
				if (linhaDeArquivoLida != null)
				{
					linhaDeArquivoLida(this, new LinhaDeArquivoLidaArgs(detalheRetornoCnab240, linha, tipoLinha));
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar evento.", ex);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0006E958 File Offset: 0x0006CB58
		public virtual void OnLinhaLida(DetalheRetornoCNAB400 detalheRetorno, string linha)
		{
			try
			{
				EventHandler<LinhaDeArquivoLidaArgs> linhaDeArquivoLida = this.LinhaDeArquivoLida;
				if (linhaDeArquivoLida != null)
				{
					linhaDeArquivoLida(this, new LinhaDeArquivoLidaArgs(detalheRetorno, linha));
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao gerar evento.", ex);
			}
		}

		// Token: 0x04000678 RID: 1656
		private IBanco _banco;

		// Token: 0x04000679 RID: 1657
		private readonly IArquivoRetorno _arquivoRetorno;
	}
}

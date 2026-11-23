using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x0200000E RID: 14
	public class BancoFacade : IBancoFacade, IBoletoCoreFacade
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004510 File Offset: 0x00002710
		public BancoFacade(int codigo, int codigoEspecieDocumento, int codigoFormaCadastramento, int codigoTipoCobranca, int codigoTipoEmissao, int codigoTipoDistribuicao, int codigoTipoTransmissao, int solucao)
		{
			this._codigo = codigo;
			this._codigoEspecieDocumento = codigoEspecieDocumento;
			this._codigoFormaCadastramento = codigoFormaCadastramento;
			this._codigoTipoCobranca = codigoTipoCobranca;
			this._codigoTipoEmissao = codigoTipoEmissao;
			this._codigoTipoDistribuicao = codigoTipoDistribuicao;
			this._codigoTipoTransmissao = codigoTipoTransmissao;
			this._solucao = solucao;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004560 File Offset: 0x00002760
		public TBanco Obter<TBanco>() where TBanco : class
		{
			return new Banco(this._codigo) as TBanco;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004577 File Offset: 0x00002777
		public TEspecieDocumento ObterEspecieDocumento<TEspecieDocumento>() where TEspecieDocumento : class
		{
			return this.Obter<TEspecieDocumento>(this._codigoEspecieDocumento);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004588 File Offset: 0x00002788
		public TFormaCadastramento ObterFormaCadastramento<TFormaCadastramento>() where TFormaCadastramento : class
		{
			if (this._codigoFormaCadastramento != 0)
			{
				return this.Obter<TFormaCadastramento>(this._codigoFormaCadastramento);
			}
			return default(TFormaCadastramento);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000045B3 File Offset: 0x000027B3
		public TTipoCobranca ObterTipoCobranca<TTipoCobranca>() where TTipoCobranca : class
		{
			return this.Obter<TTipoCobranca>(this._codigoTipoCobranca);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000045C4 File Offset: 0x000027C4
		public TTipoEmissao ObterTipoEmissao<TTipoEmissao>() where TTipoEmissao : class
		{
			if (this._codigoTipoEmissao != 0)
			{
				return this.Obter<TTipoEmissao>(this._codigoTipoEmissao);
			}
			return default(TTipoEmissao);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000045F0 File Offset: 0x000027F0
		public TTipoDistribuicao ObterTipoDistribuicao<TTipoDistribuicao>() where TTipoDistribuicao : class
		{
			if (this._codigoTipoDistribuicao != 0)
			{
				return new TipoDistribuicao(this._codigo, this._codigoTipoDistribuicao, this._solucao) as TTipoDistribuicao;
			}
			return default(TTipoDistribuicao);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004630 File Offset: 0x00002830
		public TTipoTransmissao ObterTipoTransmissao<TTipoTransmissao>() where TTipoTransmissao : class
		{
			if (this._codigoTipoTransmissao != 0)
			{
				return new TipoTransmissao(this._codigo, this._codigoTipoTransmissao) as TTipoTransmissao;
			}
			return default(TTipoTransmissao);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000466C File Offset: 0x0000286C
		private T Obter<T>(int codigo) where T : class
		{
			if (codigo == 0)
			{
				return default(T);
			}
			return Activator.CreateInstance(typeof(T), new object[] { this._codigo, codigo }) as T;
		}

		// Token: 0x04000020 RID: 32
		private readonly int _codigo;

		// Token: 0x04000021 RID: 33
		private readonly int _codigoEspecieDocumento;

		// Token: 0x04000022 RID: 34
		private readonly int _codigoFormaCadastramento;

		// Token: 0x04000023 RID: 35
		private readonly int _codigoTipoCobranca;

		// Token: 0x04000024 RID: 36
		private readonly int _codigoTipoDistribuicao;

		// Token: 0x04000025 RID: 37
		private readonly int _codigoTipoEmissao;

		// Token: 0x04000026 RID: 38
		private readonly int _codigoTipoTransmissao;

		// Token: 0x04000027 RID: 39
		private readonly int _solucao;
	}
}

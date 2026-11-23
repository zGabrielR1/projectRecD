using System;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x0200000F RID: 15
	public class BancoFacadeFactory
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000046BC File Offset: 0x000028BC
		public BancoFacadeFactory SetarCodigo(int codigo)
		{
			this._codigo = codigo;
			return this;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000046C6 File Offset: 0x000028C6
		public BancoFacadeFactory SetarCodigoEspecieDocumento(int? codigoEspecieDocumento)
		{
			this._codigoEspecieDocumento = codigoEspecieDocumento.ToInt();
			return this;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000046DA File Offset: 0x000028DA
		public BancoFacadeFactory SetarCodigoFormaCadastramento(int? codigoFormaCadastramento)
		{
			this._codigoFormaCadastramento = codigoFormaCadastramento.ToInt();
			return this;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000046EE File Offset: 0x000028EE
		public BancoFacadeFactory SetarCodigoTipoCobranca(int? codigoTipoCobranca)
		{
			this._codigoTipoCobranca = codigoTipoCobranca.ToInt();
			return this;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004702 File Offset: 0x00002902
		public BancoFacadeFactory SetarCodigoTipoEmissao(int? codigoTipoEmissao)
		{
			this._codigoTipoEmissao = codigoTipoEmissao.ToInt();
			return this;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004716 File Offset: 0x00002916
		public BancoFacadeFactory SetarCodigoTipoDistribuicao(int? codigoTipoDistribuicao)
		{
			this._codigoTipoDistribuicao = codigoTipoDistribuicao.ToInt();
			return this;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000472A File Offset: 0x0000292A
		public BancoFacadeFactory SetarCodigoTipoTransmissao(int? codigoTipoTransmissao)
		{
			this._codigoTipoTransmissao = codigoTipoTransmissao.ToInt();
			return this;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000473E File Offset: 0x0000293E
		public BancoFacadeFactory SetarSolucao(int? solucao)
		{
			this._solucao = solucao.ToInt();
			return this;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004752 File Offset: 0x00002952
		public BancoFacade Criar()
		{
			return new BancoFacade(this._codigo, this._codigoEspecieDocumento, this._codigoFormaCadastramento, this._codigoTipoCobranca, this._codigoTipoEmissao, this._codigoTipoDistribuicao, this._codigoTipoTransmissao, this._solucao);
		}

		// Token: 0x04000028 RID: 40
		private int _codigo;

		// Token: 0x04000029 RID: 41
		private int _codigoEspecieDocumento;

		// Token: 0x0400002A RID: 42
		private int _codigoFormaCadastramento;

		// Token: 0x0400002B RID: 43
		private int _codigoTipoCobranca;

		// Token: 0x0400002C RID: 44
		private int _codigoTipoDistribuicao;

		// Token: 0x0400002D RID: 45
		private int _codigoTipoEmissao;

		// Token: 0x0400002E RID: 46
		private int _codigoTipoTransmissao;

		// Token: 0x0400002F RID: 47
		private int _solucao;
	}
}

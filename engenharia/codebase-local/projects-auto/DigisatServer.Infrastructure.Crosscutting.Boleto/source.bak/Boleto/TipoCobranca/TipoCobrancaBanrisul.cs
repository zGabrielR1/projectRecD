using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000072 RID: 114
	public class TipoCobrancaBanrisul : AbstractTipoCobranca
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00007D1C File Offset: 0x00005F1C
		public TipoCobrancaBanrisul(int codigo)
		{
			try
			{
				this.CarregarTipoCobranca(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00007D58 File Offset: 0x00005F58
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				if (idCodigo == 8)
				{
					this.Codigo = 8;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBanrisul.CobrancaCredenciadaBanrisulCcbSimples);
				}
				else
				{
					this.Codigo = 8;
					this.Descricao = "Cobrança Credenciada Banrisul - CCB";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

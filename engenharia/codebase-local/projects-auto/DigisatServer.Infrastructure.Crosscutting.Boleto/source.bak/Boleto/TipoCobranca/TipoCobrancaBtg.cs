using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200007A RID: 122
	public class TipoCobrancaBtg : AbstractTipoCobranca
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000807C File Offset: 0x0000627C
		public TipoCobrancaBtg(int codigo)
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

		// Token: 0x06000253 RID: 595 RVA: 0x000080B8 File Offset: 0x000062B8
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBtg.ComCadastramentoCobrancaRegistrada);
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBtg.SemCadastramento);
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBtg.ComCadastramentoRecusaDoDebitoAutomatico);
					break;
				default:
					this.Codigo = 1;
					this.Descricao = "Com Cadastramento (Cobrança Registrada)";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

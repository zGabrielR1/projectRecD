using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000053 RID: 83
	public class TipoDistribuicaoBancoBrasil : AbstractTipoDistribuicao
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00006FC8 File Offset: 0x000051C8
		public TipoDistribuicaoBancoBrasil(int codigo)
		{
			try
			{
				this.CarregarTipoDistribuicao(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007004 File Offset: 0x00005204
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBancoBrasil.BancoDistribui);
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBancoBrasil.ClienteDistribui);
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoBancoBrasil.BancoEnviaEmail);
					break;
				default:
					this.Codigo = 2;
					this.Descricao = "Cliente distribui";
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

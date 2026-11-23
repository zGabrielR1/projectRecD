using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200004C RID: 76
	public class TipoEmissaoAilos : AbstractTipoEmissao
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00006C04 File Offset: 0x00004E04
		public TipoEmissaoAilos(int codigo)
		{
			try
			{
				this.CarregarTipoEmissao(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006C40 File Offset: 0x00004E40
		private void CarregarTipoEmissao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cooperado Emite";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoAilos.CooperadoEmite);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoAilos.CooperativaEmite);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000080 RID: 128
	public class TipoCobrancaSicredi : AbstractTipoCobranca
	{
		// Token: 0x06000258 RID: 600 RVA: 0x000082D8 File Offset: 0x000064D8
		public TipoCobrancaSicredi(int codigo)
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

		// Token: 0x06000259 RID: 601 RVA: 0x00008314 File Offset: 0x00006514
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 3)
					{
						this.Codigo = 1;
						this.Descricao = "Com registro";
					}
					else
					{
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSicredi.SemRegistro);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSicredi.ComRegistro);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoTransmissao
{
	// Token: 0x02000030 RID: 48
	public class TiposTransmissao : AbstractTipoTransmissao
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00005F70 File Offset: 0x00004170
		public TiposTransmissao(int codigo)
		{
			try
			{
				this.CarregarTipoTransmissao(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005FAC File Offset: 0x000041AC
		private void CarregarTipoTransmissao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 1;
						this.Descricao = "Remessa e retorno";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoTransmissaomissao.DePagar);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoTransmissaomissao.RemessaRetorno);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

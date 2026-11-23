using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000046 RID: 70
	public class TipoEmissaoSafra : AbstractTipoEmissao
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000069D0 File Offset: 0x00004BD0
		public TipoEmissaoSafra(int codigo)
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

		// Token: 0x060001F6 RID: 502 RVA: 0x00006A0C File Offset: 0x00004C0C
		private void CarregarTipoEmissao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cliente emite boleto";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoSafra.ClienteEmiteBoleto);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoSafra.BancoEmiteBoleto);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

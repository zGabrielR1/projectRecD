using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200003E RID: 62
	public class TipoEmissaoCresol : AbstractTipoEmissao
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00006700 File Offset: 0x00004900
		public TipoEmissaoCresol(int codigo)
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

		// Token: 0x060001EE RID: 494 RVA: 0x0000673C File Offset: 0x0000493C
		private void CarregarTipoEmissao(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 2;
						this.Descricao = "Cliente emite e banco processa registro.";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoCresol.ClienteEmiteEBancoProcessa);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoCresol.BancoEmiteEProcessa);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

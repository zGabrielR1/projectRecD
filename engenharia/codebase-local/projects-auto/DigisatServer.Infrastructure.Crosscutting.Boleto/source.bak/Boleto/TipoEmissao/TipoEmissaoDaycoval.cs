using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x0200003C RID: 60
	public class TipoEmissaoDaycoval : AbstractTipoEmissao
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00006644 File Offset: 0x00004844
		public TipoEmissaoDaycoval(int codigo)
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

		// Token: 0x060001EC RID: 492 RVA: 0x00006680 File Offset: 0x00004880
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
						this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoDaycoval.ClienteEmiteEBancoProcessa);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoDaycoval.BancoEmiteEProcessa);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

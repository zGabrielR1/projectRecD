using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000070 RID: 112
	public class TipoCobrancaBancoBrasil : AbstractTipoCobranca
	{
		// Token: 0x06000248 RID: 584 RVA: 0x00007C38 File Offset: 0x00005E38
		public TipoCobrancaBancoBrasil(int codigo)
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

		// Token: 0x06000249 RID: 585 RVA: 0x00007C74 File Offset: 0x00005E74
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBancoBrasil.Simples);
					goto IL_007C;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBancoBrasil.VinculadaCaucionada);
					goto IL_007C;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaBancoBrasil.Descontada);
					goto IL_007C;
				}
				this.Codigo = 1;
				this.Descricao = "Simples";
				IL_007C:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

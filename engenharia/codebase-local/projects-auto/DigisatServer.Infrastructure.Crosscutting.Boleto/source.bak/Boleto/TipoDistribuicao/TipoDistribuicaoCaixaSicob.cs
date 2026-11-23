using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoDistribuicao
{
	// Token: 0x02000057 RID: 87
	public class TipoDistribuicaoCaixaSicob : AbstractTipoDistribuicao
	{
		// Token: 0x0600021B RID: 539 RVA: 0x00007164 File Offset: 0x00005364
		public TipoDistribuicaoCaixaSicob(int codigo)
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

		// Token: 0x0600021C RID: 540 RVA: 0x000071A0 File Offset: 0x000053A0
		private void CarregarTipoDistribuicao(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 0:
					this.Codigo = 0;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSicob.PostagemPeloCedente);
					break;
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSicob.SacadoViaCorreios);
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSicob.CedenteViaAgenciaCaixa);
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSicob.SacadoViaEmail);
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumTipoDistribuicaoCaixaSicob.SacadoViaSMS);
					break;
				default:
					this.Codigo = 0;
					this.Descricao = "Postagem pelo Cedente";
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

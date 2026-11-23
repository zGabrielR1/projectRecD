using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200006E RID: 110
	public class TipoCobrancaNordeste : AbstractTipoCobranca
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00007B14 File Offset: 0x00005D14
		public TipoCobrancaNordeste(int codigo)
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

		// Token: 0x06000247 RID: 583 RVA: 0x00007B50 File Offset: 0x00005D50
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 0:
					this.Codigo = 21;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaNordeste.CobrancaSimplesEscrituralBoletoEmitidoPeloBanco);
					break;
				case 1:
					this.Codigo = 41;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaNordeste.CobrancaVinculadaBoletoEmitidoPeloBanco);
					break;
				case 2:
					this.Codigo = 21;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaNordeste.CobrancaSimplesBoletoEmitidoPeloCliente);
					break;
				case 3:
					this.Codigo = 41;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaNordeste.CobrancaVinculadaBoletoEmitidoPeloCliente);
					break;
				case 4:
					this.Codigo = 51;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaNordeste.CobrancaSimplificadaSemRegistro);
					break;
				default:
					this.Codigo = 21;
					this.Descricao = "Cobrança Simples - Boleto Emitido Pelo Cliente";
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

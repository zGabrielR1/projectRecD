using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x0200007E RID: 126
	public class TipoCobrancaAilos : AbstractTipoCobranca
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000823C File Offset: 0x0000643C
		public TipoCobrancaAilos(int codigo)
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

		// Token: 0x06000257 RID: 599 RVA: 0x00008278 File Offset: 0x00006478
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				if (idCodigo == 1)
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaAilos.ComRegistro);
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = "Cobrança Simples com Registro";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoCobranca
{
	// Token: 0x02000078 RID: 120
	public class TipoCobrancaSantander : AbstractTipoCobranca
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00007F58 File Offset: 0x00006158
		public TipoCobrancaSantander(int codigo)
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

		// Token: 0x06000251 RID: 593 RVA: 0x00007F94 File Offset: 0x00006194
		private void CarregarTipoCobranca(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSantander.CobrancaSimples);
					goto IL_00BB;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSantander.CobrancaCaucionada);
					goto IL_00BB;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSantander.CobrancaDescontada);
					goto IL_00BB;
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSantander.CobrancaSimplesComRegistro);
					goto IL_00BB;
				case 6:
					this.Codigo = 6;
					this.Descricao = Utils.RetornarDescricao(EnumTipoCobrancaSantander.CobrancaCaucionadaComRegistro);
					goto IL_00BB;
				}
				this.Codigo = 5;
				this.Descricao = "Cobrança Simples (Rápida com Registro)";
				IL_00BB:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

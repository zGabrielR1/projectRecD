using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.TipoEmissao
{
	// Token: 0x02000036 RID: 54
	public class TipoEmissaoBancoBrasil : AbstractTipoEmissao
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000638C File Offset: 0x0000458C
		public TipoEmissaoBancoBrasil(int codigo)
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

		// Token: 0x060001E6 RID: 486 RVA: 0x000063C8 File Offset: 0x000045C8
		private void CarregarTipoEmissao(int idCodigo)
		{
			try
			{
				switch (idCodigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.BancoEmite);
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.ClienteEmite);
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.BancoPreEmiteEClienteComplementa);
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.BancoReemite);
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.BancoNaoReemite);
					break;
				case 6:
					this.Codigo = 6;
					this.Descricao = Utils.RetornarDescricao(EnumTipoEmissaoBancoBrasil.CobrancaSemPapel);
					break;
				default:
					this.Codigo = 2;
					this.Descricao = "Cliente emite";
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

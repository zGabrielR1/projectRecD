using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000BD RID: 189
	public class FormaCadastramentoCaixa : AbstractFormaCadastramento
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		public FormaCadastramentoCaixa(int codigo)
		{
			try
			{
				this.CarregarFormaCadastramento(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000DB68 File Offset: 0x0000BD68
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 1;
						this.Descricao = "Cobrança registrada";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoCaixa.CobrancaSemRegistro);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoCaixa.CobrancaRegistrada);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C1 RID: 193
	public class FormaCadastramentoSantander : AbstractFormaCadastramento
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public FormaCadastramentoSantander(int codigo)
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

		// Token: 0x0600032C RID: 812 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 1;
						this.Descricao = "Cobrança Registrada (Rápida e Eletrônica com Registro)";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoSantander.CobrancaSemRegistro);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoSantander.CobrancaRegistrada);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

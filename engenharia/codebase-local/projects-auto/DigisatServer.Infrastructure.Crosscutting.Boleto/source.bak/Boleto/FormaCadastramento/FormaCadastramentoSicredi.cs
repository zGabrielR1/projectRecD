using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C7 RID: 199
	public class FormaCadastramentoSicredi : AbstractFormaCadastramento
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public FormaCadastramentoSicredi(int codigo)
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

		// Token: 0x06000332 RID: 818 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 1;
						this.Descricao = "Com cadastramento (cobrança registrada)";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoSicredi.SemCadastramento);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoSicredi.ComCadastramento);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C3 RID: 195
	public class FormaCadastramentoBanese : AbstractFormaCadastramento
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public FormaCadastramentoBanese(int codigo)
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

		// Token: 0x0600032E RID: 814 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo == 1)
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBanese.ComCadastramento);
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBanese.ComCadastramento);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

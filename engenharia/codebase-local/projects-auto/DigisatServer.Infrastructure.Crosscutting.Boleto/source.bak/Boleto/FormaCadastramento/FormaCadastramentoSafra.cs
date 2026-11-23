using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000BF RID: 191
	public class FormaCadastramentoSafra : AbstractFormaCadastramento
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public FormaCadastramentoSafra(int codigo)
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

		// Token: 0x0600032A RID: 810 RVA: 0x0000DC24 File Offset: 0x0000BE24
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo == 1)
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoSafra.CobrancaRegistrada);
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = "Cobrança Registrada";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

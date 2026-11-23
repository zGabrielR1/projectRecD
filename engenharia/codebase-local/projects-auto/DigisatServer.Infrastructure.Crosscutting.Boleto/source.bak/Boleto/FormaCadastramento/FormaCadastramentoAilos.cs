using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000C5 RID: 197
	public class FormaCadastramentoAilos : AbstractFormaCadastramento
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public FormaCadastramentoAilos(int codigo)
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

		// Token: 0x06000330 RID: 816 RVA: 0x0000DE1C File Offset: 0x0000C01C
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo == 1)
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoAilos.ComCadastramento);
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = "Com Cadastramento (Cobrança Registrada)";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

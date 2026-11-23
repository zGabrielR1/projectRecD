using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000B9 RID: 185
	public class FormaCadastramentoBancoBrasil : AbstractFormaCadastramento
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public FormaCadastramentoBancoBrasil(int codigo)
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

		// Token: 0x06000324 RID: 804 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
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
						this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBancoBrasil.CobrancaSemRegistro);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBancoBrasil.CobrancaRegistrada);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

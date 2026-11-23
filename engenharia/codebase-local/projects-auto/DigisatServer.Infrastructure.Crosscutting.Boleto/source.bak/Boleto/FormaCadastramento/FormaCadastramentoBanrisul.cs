using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.FormaCadastramento
{
	// Token: 0x020000BB RID: 187
	public class FormaCadastramentoBanrisul : AbstractFormaCadastramento
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public FormaCadastramentoBanrisul(int codigo)
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

		// Token: 0x06000326 RID: 806 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		private void CarregarFormaCadastramento(int idCodigo)
		{
			try
			{
				if (idCodigo != 1)
				{
					if (idCodigo != 2)
					{
						this.Codigo = 1;
						this.Descricao = "Com cadastramento (Cobrança registrada)";
					}
					else
					{
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBanrisul.SemCadastramentoCobrancaSemRegistro);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumFormaCadastramentoBanrisul.ComCadastramentoCobrancaRegistrada);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

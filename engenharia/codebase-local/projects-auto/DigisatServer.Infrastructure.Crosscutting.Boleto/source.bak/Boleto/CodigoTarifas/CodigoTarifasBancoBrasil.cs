using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoTarifas
{
	// Token: 0x020000FC RID: 252
	public class CodigoTarifasBancoBrasil : AbstractCodigoTarifas
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x00015786 File Offset: 0x00013986
		public CodigoTarifasBancoBrasil()
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015790 File Offset: 0x00013990
		public CodigoTarifasBancoBrasil(int codigo)
		{
			try
			{
				this.Carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000157CC File Offset: 0x000139CC
		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new BancoBrasil();
				switch (codigo)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = "Tarifa de extrato de posição";
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = "Tarifa de manutenção de título vencido";
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = "Tarifa de sustação";
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = "Tarifa de protesto";
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = "Tarifa de outras instruções";
					break;
				case 6:
					this.Codigo = 6;
					this.Descricao = "Tarifa de outras ocorrências";
					break;
				case 7:
					this.Codigo = 7;
					this.Descricao = "Tarifa de envio de duplicata ao sacado";
					break;
				case 8:
					this.Codigo = 8;
					this.Descricao = "Custas de protesto";
					break;
				case 9:
					this.Codigo = 9;
					this.Descricao = "Custas de sustação de protesto";
					break;
				case 10:
					this.Codigo = 10;
					this.Descricao = "Custas do cartório distribuidor";
					break;
				case 11:
					this.Codigo = 11;
					this.Descricao = "Custas de edital";
					break;
				default:
					this.Codigo = 0;
					this.Descricao = "( Selecione )";
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

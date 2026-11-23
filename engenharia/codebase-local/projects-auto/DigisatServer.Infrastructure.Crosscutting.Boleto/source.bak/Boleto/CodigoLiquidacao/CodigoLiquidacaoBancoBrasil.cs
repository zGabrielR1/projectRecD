using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x02000120 RID: 288
	public class CodigoLiquidacaoBancoBrasil : AbstractCodigoLiquidacao
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x000239A8 File Offset: 0x00021BA8
		public CodigoLiquidacaoBancoBrasil(int codigo)
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

		// Token: 0x0600042F RID: 1071 RVA: 0x000239E4 File Offset: 0x00021BE4
		private void Carregar(int idCodigo)
		{
			try
			{
				this.Banco = new BancoBrasil();
				switch (idCodigo)
				{
				case 1:
					this.Enumerado = 1;
					this.Codigo = "";
					this.Descricao = "Por saldo";
					this.Recurso = "";
					break;
				case 2:
					this.Enumerado = 2;
					this.Codigo = "";
					this.Descricao = "Por conta";
					this.Recurso = "";
					break;
				case 3:
					this.Enumerado = 3;
					this.Codigo = "";
					this.Descricao = "No próprio banco";
					this.Recurso = "";
					break;
				case 4:
					this.Enumerado = 4;
					this.Codigo = "";
					this.Descricao = "Compensação eletrônica";
					this.Recurso = "";
					break;
				case 5:
					this.Enumerado = 5;
					this.Codigo = "";
					this.Descricao = "Compensação convencional";
					this.Recurso = "";
					break;
				case 6:
					this.Enumerado = 6;
					this.Codigo = "";
					this.Descricao = "Por meio eletrônico";
					this.Recurso = "";
					break;
				case 7:
					this.Enumerado = 7;
					this.Codigo = "";
					this.Descricao = "Após feriado nacional";
					this.Recurso = "";
					break;
				case 8:
					this.Enumerado = 8;
					this.Codigo = "";
					this.Descricao = "Em cartório";
					this.Recurso = "";
					break;
				case 9:
					this.Enumerado = 9;
					this.Codigo = "";
					this.Descricao = "Comandada banco";
					this.Recurso = "";
					break;
				case 10:
					this.Enumerado = 10;
					this.Codigo = "";
					this.Descricao = "Comandada cliente - arquivo";
					this.Recurso = "";
					break;
				case 11:
					this.Enumerado = 11;
					this.Codigo = "";
					this.Descricao = "Comandada cliente - online";
					this.Recurso = "";
					break;
				case 12:
					this.Enumerado = 12;
					this.Codigo = "";
					this.Descricao = "Decurso de prazo - cliente";
					this.Recurso = "";
					break;
				case 13:
					this.Enumerado = 13;
					this.Codigo = "";
					this.Descricao = "Decurso de prazo - banco";
					this.Recurso = "";
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

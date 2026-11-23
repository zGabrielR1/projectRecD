using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000146 RID: 326
	public class CarteiraSantander : AbstractCarteira
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00025E74 File Offset: 0x00024074
		public CarteiraSantander()
		{
			try
			{
				this.Banco = new Banco(33);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00025EB4 File Offset: 0x000240B4
		public CarteiraSantander(int carteira)
		{
			try
			{
				this.Carregar(carteira);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00025EF0 File Offset: 0x000240F0
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoSantander();
				if (carteira != 101)
				{
					if (carteira != 102)
					{
						if (carteira == 201)
						{
							this.NumeroCarteira = 201;
							this.Codigo = "CSR";
							this.Tipo = "";
							this.Descricao = "Penhor Rápida COM Registro";
						}
					}
					else
					{
						this.NumeroCarteira = 102;
						this.Codigo = "CSR";
						this.Tipo = "";
						this.Descricao = "Cobrança Simples SEM Registro";
					}
				}
				else
				{
					this.NumeroCarteira = 101;
					this.Codigo = "ECR";
					this.Tipo = "";
					this.Descricao = "Cobrança Simples Rápida COM Registro";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

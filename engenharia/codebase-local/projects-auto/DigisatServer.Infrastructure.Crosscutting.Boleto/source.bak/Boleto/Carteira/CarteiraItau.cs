using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira
{
	// Token: 0x02000141 RID: 321
	public class CarteiraItau : AbstractCarteira
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x000256B0 File Offset: 0x000238B0
		public CarteiraItau()
		{
			try
			{
				this.Banco = new Banco(341);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000256F4 File Offset: 0x000238F4
		public CarteiraItau(int carteira)
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

		// Token: 0x0600047A RID: 1146 RVA: 0x00025730 File Offset: 0x00023930
		private void Carregar(int carteira)
		{
			try
			{
				this.Banco = new BancoItau();
				if (carteira <= 147)
				{
					if (carteira <= 121)
					{
						switch (carteira)
						{
						case 102:
							this.NumeroCarteira = 102;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão integral - Carnê";
							break;
						case 103:
							this.NumeroCarteira = 103;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, com emissão e entrega - Carnê";
							break;
						case 104:
							this.NumeroCarteira = 104;
							this.Codigo = "I";
							this.Tipo = "E";
							this.Descricao = "Escritural eletrônica - Carnê";
							break;
						case 105:
						case 106:
						case 110:
						case 111:
						case 113:
						case 114:
							break;
						case 107:
							this.NumeroCarteira = 107;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão integral - 15 poições - Carnê";
							break;
						case 108:
							this.NumeroCarteira = 108;
							this.Codigo = "I";
							this.Tipo = "D";
							this.Descricao = "Direta eletrônica emissão integral - Carnê";
							break;
						case 109:
							this.NumeroCarteira = 109;
							this.Codigo = "I";
							this.Tipo = "D";
							this.Descricao = "Direta eletrônica sem emissão - Simples";
							break;
						case 112:
							this.NumeroCarteira = 112;
							this.Codigo = "I";
							this.Tipo = "E";
							this.Descricao = "Escritural eletrônica simples";
							break;
						case 115:
							this.NumeroCarteira = 115;
							this.Codigo = "I";
							this.Tipo = "E";
							this.Descricao = "Escritural eletrônica simples - Faixa nosso número livre";
							break;
						default:
							if (carteira == 121)
							{
								this.NumeroCarteira = 121;
								this.Codigo = "I";
								this.Tipo = "D";
								this.Descricao = "Direta eletrônica emissão parcial - Simples";
							}
							break;
						}
					}
					else if (carteira != 129)
					{
						switch (carteira)
						{
						case 139:
							this.NumeroCarteira = 139;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão parcial, seguros com IOF 4%";
							break;
						case 140:
						case 141:
							break;
						case 142:
							this.NumeroCarteira = 142;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro e sem emissão - 15 dígitos IOF 4%";
							break;
						case 143:
							this.NumeroCarteira = 143;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro e sem emissão - 15 dígitos IOF 7%";
							break;
						default:
							if (carteira == 147)
							{
								this.NumeroCarteira = 147;
								this.Codigo = "E";
								this.Tipo = "E";
								this.Descricao = "Escritural eletrônica - Dólar";
							}
							break;
						}
					}
					else
					{
						this.NumeroCarteira = 129;
						this.Codigo = "I";
						this.Tipo = "S";
						this.Descricao = "Sem registro, emissão parcial, seguros com IOF 2%";
					}
				}
				else if (carteira <= 180)
				{
					if (carteira != 150)
					{
						switch (carteira)
						{
						case 169:
							this.NumeroCarteira = 169;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão parcial, seguros com IOF 7%";
							break;
						case 172:
							this.NumeroCarteira = 172;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão integral";
							break;
						case 173:
							this.NumeroCarteira = 173;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, com emissão e entrega";
							break;
						case 174:
							this.NumeroCarteira = 174;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão parcial com protesto borderô";
							break;
						case 175:
							this.NumeroCarteira = 175;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, sem emissão e com protesto eletrônico";
							break;
						case 177:
							this.NumeroCarteira = 177;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro, emissão parcial com protesto eletrônico";
							break;
						case 180:
							this.NumeroCarteira = 180;
							this.Codigo = "I";
							this.Tipo = "D";
							this.Descricao = "Direta eletrônica emissão integral - Simples";
							break;
						}
					}
					else
					{
						this.NumeroCarteira = 150;
						this.Codigo = "U";
						this.Tipo = "D";
						this.Descricao = "Direta eletrônica sem emissão - Dólar";
					}
				}
				else if (carteira != 188)
				{
					if (carteira != 196)
					{
						if (carteira == 198)
						{
							this.NumeroCarteira = 198;
							this.Codigo = "I";
							this.Tipo = "S";
							this.Descricao = "Sem registro e sem emissão - 15 dígitos";
						}
					}
					else
					{
						this.NumeroCarteira = 196;
						this.Codigo = "I";
						this.Tipo = "S";
						this.Descricao = "Sem registro, com emissão e entrega - 15 posições";
					}
				}
				else
				{
					this.NumeroCarteira = 188;
					this.Codigo = "I";
					this.Tipo = "E";
					this.Descricao = "Escritural eletrônica - Cobrança inteligente";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

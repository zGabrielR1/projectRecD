using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoLiquidacao
{
	// Token: 0x02000122 RID: 290
	public class CodigoLiquidacaoItau : AbstractCodigoLiquidacao
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00023CAC File Offset: 0x00021EAC
		public CodigoLiquidacaoItau(int codigo)
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

		// Token: 0x06000431 RID: 1073 RVA: 0x00023CE8 File Offset: 0x00021EE8
		private void Carregar(int idCodigo)
		{
			try
			{
				this.Banco = new BancoItau();
				switch (idCodigo)
				{
				case 1:
					this.Enumerado = 1;
					this.Codigo = "AA";
					this.Descricao = "Caixa eletrônico do Banco Itaú.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 2:
					this.Enumerado = 2;
					this.Codigo = "AC";
					this.Descricao = "Pagamento em cartório automatizado.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 4:
					this.Enumerado = 4;
					this.Codigo = "BC";
					this.Descricao = "Bancos correspondentes.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 5:
					this.Enumerado = 5;
					this.Codigo = "BF";
					this.Descricao = "Itaú BankFone.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 6:
					this.Enumerado = 6;
					this.Codigo = "BL";
					this.Descricao = "Itaú BankLine.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 7:
					this.Enumerado = 7;
					this.Codigo = "B0";
					this.Descricao = "Outros bancos - Recebimento offline.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 8:
					this.Enumerado = 8;
					this.Codigo = "B1";
					this.Descricao = "Outros bancos - Pelo código de barras.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 9:
					this.Enumerado = 9;
					this.Codigo = "B2";
					this.Descricao = "Outros bancos - Pelo linha digitável.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 10:
					this.Enumerado = 10;
					this.Codigo = "B3";
					this.Descricao = "Outros bancos - Pelo auto-atendimento.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 11:
					this.Enumerado = 11;
					this.Codigo = "B4";
					this.Descricao = "Outros bancos - Recebimento em casa lotérica.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 12:
					this.Enumerado = 12;
					this.Codigo = "CC";
					this.Descricao = "Agência Itaú - Com cheque de outro banco.";
					this.Recurso = "A compensar";
					goto IL_0356;
				case 13:
					this.Enumerado = 13;
					this.Codigo = "CK";
					this.Descricao = "SISPAG - Sistema de contas a pagar Itaú.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 14:
					this.Enumerado = 14;
					this.Codigo = "CP";
					this.Descricao = "Agência Itaú - Por débito em conta corrente, cheque ou dinheiro.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 15:
					this.Enumerado = 15;
					this.Codigo = "DG";
					this.Descricao = "Agência Itaú - Capturado offline.";
					this.Recurso = "Disponível";
					goto IL_0356;
				case 16:
					this.Enumerado = 16;
					this.Codigo = "LC";
					this.Descricao = "Pagamento em cartório de protesto com cheque.";
					this.Recurso = "A Compensar";
					goto IL_0356;
				case 17:
					this.Enumerado = 17;
					this.Codigo = "Q0";
					this.Descricao = "Agendamento - Pagamento agendado via BankLine ou outro canal eletrônico.";
					this.Recurso = "Disponível";
					goto IL_0356;
				}
				this.Enumerado = 0;
				this.Codigo = " ";
				this.Descricao = "( Selecione )";
				this.Recurso = "Sem Recurso";
				IL_0356:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

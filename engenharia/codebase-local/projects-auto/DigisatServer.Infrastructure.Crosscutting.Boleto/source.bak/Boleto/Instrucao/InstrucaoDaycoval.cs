using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008B RID: 139
	public class InstrucaoDaycoval : AbstractInstrucao
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000904C File Offset: 0x0000724C
		public InstrucaoDaycoval()
		{
			try
			{
				this.Banco = new Banco(707);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009090 File Offset: 0x00007290
		public InstrucaoDaycoval(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000090A4 File Offset: 0x000072A4
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoDaycoval();
				this.Valida();
				switch (idInstrucao)
				{
				case 997:
					this.Codigo = 997;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesDaycoval.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
					break;
				case 998:
					this.Codigo = 998;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesDaycoval.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
					break;
				case 999:
					this.Codigo = 999;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesDaycoval.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
					break;
				default:
					this.Codigo = 0;
					this.Descricao = "";
					this.QuantidadeDias = nrDias;
					break;
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

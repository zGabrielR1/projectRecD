using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008D RID: 141
	public class InstrucaoIntermedium : AbstractInstrucao
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000924C File Offset: 0x0000744C
		public InstrucaoIntermedium()
		{
			try
			{
				this.Banco = new Banco(77);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000928C File Offset: 0x0000748C
		public InstrucaoIntermedium(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000092A0 File Offset: 0x000074A0
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoIntermedium();
				this.Valida();
				switch (idInstrucao)
				{
				case 997:
					this.Codigo = 997;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesIntermedium.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
					break;
				case 998:
					this.Codigo = 998;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesIntermedium.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
					break;
				case 999:
					this.Codigo = 999;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesIntermedium.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
					break;
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

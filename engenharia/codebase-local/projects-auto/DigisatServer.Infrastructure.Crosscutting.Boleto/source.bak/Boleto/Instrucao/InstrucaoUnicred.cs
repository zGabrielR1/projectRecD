using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000B0 RID: 176
	public class InstrucaoUnicred : AbstractInstrucao
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public InstrucaoUnicred()
		{
			try
			{
				this.Banco = new Banco(136);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D23C File Offset: 0x0000B43C
		public InstrucaoUnicred(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D250 File Offset: 0x0000B450
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoUnicred();
				this.Valida();
				switch (idInstrucao)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.ProtestarAposNDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.ProtestarAposNDiasUteis).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.NaoProtestar);
					break;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						break;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					default:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesUnicred.NaoProtestar);
						this.QuantidadeDias = nrDias;
						break;
					}
					break;
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1 || this.Codigo == 2;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

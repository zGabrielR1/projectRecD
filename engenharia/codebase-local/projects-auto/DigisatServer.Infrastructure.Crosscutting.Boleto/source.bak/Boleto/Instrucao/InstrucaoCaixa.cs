using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200009A RID: 154
	public class InstrucaoCaixa : AbstractInstrucao
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000A4E8 File Offset: 0x000086E8
		public InstrucaoCaixa()
		{
			try
			{
				this.Banco = new Banco(104);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A528 File Offset: 0x00008728
		public InstrucaoCaixa(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A53C File Offset: 0x0000873C
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoCaixa();
				this.Valida();
				switch (idInstrucao)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.ProtestarAposNDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.ProtestarAposNDiasUteis).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.NaoProtestar);
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.BaixarDevolverAposNDias).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.NaoBaixarDevolver);
					break;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						break;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					default:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCaixa.NaoProtestar);
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1 || this.Codigo == 2;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

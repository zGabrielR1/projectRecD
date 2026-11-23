using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AE RID: 174
	public class InstrucaoAilos : AbstractInstrucao
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000CF60 File Offset: 0x0000B160
		public InstrucaoAilos()
		{
			try
			{
				this.Banco = new Banco(85);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		public InstrucaoAilos(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoAilos();
				this.Valida();
				switch (idInstrucao)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.Negativacao).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.NaoProtestar);
					break;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						break;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					default:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesAilos.NaoProtestar);
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00009E08 File Offset: 0x00008008
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

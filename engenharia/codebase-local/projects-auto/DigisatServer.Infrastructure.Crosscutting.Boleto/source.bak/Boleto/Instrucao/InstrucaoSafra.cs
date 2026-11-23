using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A2 RID: 162
	public class InstrucaoSafra : AbstractInstrucao
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000BB70 File Offset: 0x00009D70
		public InstrucaoSafra()
		{
			try
			{
				this.Banco = new Banco(422);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public InstrucaoSafra(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoSafra();
				this.Valida();
				switch (idInstrucao)
				{
				case 1:
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.ProtestarAposNDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.ProtestarAposNDiasUteis).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.NaoProtestar);
					break;
				case 4:
					this.Codigo = 4;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.BaixarDevolverAposNDias).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.NaoBaixarDevolver);
					break;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						break;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						break;
					default:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSafra.NaoProtestar);
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1 || this.Codigo == 2;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

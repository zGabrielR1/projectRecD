using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AC RID: 172
	public class InstrucaoCresol : AbstractInstrucao
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public InstrucaoCresol()
		{
			try
			{
				this.Banco = new Banco(133);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public InstrucaoCresol(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000CD64 File Offset: 0x0000AF64
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoCresol();
				this.Valida();
				if (idInstrucao != 3)
				{
					if (idInstrucao != 99)
					{
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCresol.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							break;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCresol.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCresol.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						}
					}
					else
					{
						this.Codigo = 99;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCresol.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						this.QuantidadeDias = nrDias;
					}
				}
				else
				{
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCresol.NaoProtestar);
					this.QuantidadeDias = nrDias;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000CF54 File Offset: 0x0000B154
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 99;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

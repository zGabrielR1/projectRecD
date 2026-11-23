using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A6 RID: 166
	public class InstrucaoBanese : AbstractInstrucao
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000C19C File Offset: 0x0000A39C
		public InstrucaoBanese()
		{
			try
			{
				this.Banco = new Banco(47);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public InstrucaoBanese(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoBanese();
				this.Valida();
				if (idInstrucao != 1)
				{
					if (idInstrucao != 3)
					{
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							break;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						default:
							this.Codigo = 3;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.NaoProtestar);
							this.QuantidadeDias = nrDias;
							break;
						}
					}
					else
					{
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.NaoProtestar);
						this.QuantidadeDias = nrDias;
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanese.BaixarDevolverAposNDias).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					this.QuantidadeDias = nrDias;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

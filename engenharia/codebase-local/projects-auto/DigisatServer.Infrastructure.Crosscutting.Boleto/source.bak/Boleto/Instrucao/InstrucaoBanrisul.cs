using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000095 RID: 149
	public class InstrucaoBanrisul : AbstractInstrucao
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00009E14 File Offset: 0x00008014
		public InstrucaoBanrisul()
		{
			try
			{
				this.Banco = new Banco(41);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009E54 File Offset: 0x00008054
		public InstrucaoBanrisul(int codigo, decimal valor, DateTime? data, int nrDias = 0, decimal percentualMulta = 0m)
		{
			this.Carregar(codigo, valor, data, nrDias, percentualMulta);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009E6C File Offset: 0x0000806C
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias, decimal percentualMulta)
		{
			try
			{
				this.Banco = new BancoBanrisul();
				this.Valida();
				if (idInstrucao != 1)
				{
					if (idInstrucao != 3)
					{
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanrisul.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							break;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanrisul.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanrisul.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						default:
							this.Codigo = 3;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanrisul.NaoProtestar);
							this.QuantidadeDias = nrDias;
							break;
						}
					}
					else
					{
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBanrisul.NaoProtestar);
					}
				}
				else
				{
					this.Codigo = 1;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00009E08 File Offset: 0x00008008
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

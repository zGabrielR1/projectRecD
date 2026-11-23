using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000089 RID: 137
	public class InstrucaoCredisis : AbstractInstrucao
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00008DDC File Offset: 0x00006FDC
		public InstrucaoCredisis()
		{
			try
			{
				this.Banco = new Banco(97);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008E1C File Offset: 0x0000701C
		public InstrucaoCredisis(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00008E30 File Offset: 0x00007030
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoCredisis();
				this.Valida();
				if (idInstrucao != 3)
				{
					if (idInstrucao != 99)
					{
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCredisis.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							break;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCredisis.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCredisis.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							break;
						default:
							this.Codigo = 0;
							this.Descricao = "";
							this.QuantidadeDias = nrDias;
							break;
						}
					}
					else
					{
						this.Codigo = 99;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCredisis.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						this.QuantidadeDias = nrDias;
					}
				}
				else
				{
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesCredisis.NaoProtestar);
					this.QuantidadeDias = nrDias;
				}
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

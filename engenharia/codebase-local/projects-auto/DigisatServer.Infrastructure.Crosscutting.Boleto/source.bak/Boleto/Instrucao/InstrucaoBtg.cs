using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A8 RID: 168
	public class InstrucaoBtg : AbstractInstrucao
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000C400 File Offset: 0x0000A600
		public InstrucaoBtg()
		{
			try
			{
				this.Banco = new Banco(208);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C444 File Offset: 0x0000A644
		public InstrucaoBtg(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C458 File Offset: 0x0000A658
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoBtg();
				this.Valida();
				if (idInstrucao <= 42)
				{
					switch (idInstrucao)
					{
					case 3:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos3DiasUteis);
						this.QuantidadeDias = 3;
						goto IL_0389;
					case 4:
						this.Codigo = 4;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos4DiasUteis);
						this.QuantidadeDias = 4;
						goto IL_0389;
					case 5:
						this.Codigo = 5;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos5DiasUteis);
						this.QuantidadeDias = 5;
						goto IL_0389;
					case 6:
					case 9:
					case 11:
					case 12:
					case 13:
					case 14:
						break;
					case 7:
						this.Codigo = 7;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.NaoProtestar);
						this.QuantidadeDias = nrDias;
						goto IL_0389;
					case 8:
						this.Codigo = 8;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.NegativacaoSemProtesto);
						this.QuantidadeDias = nrDias;
						goto IL_0389;
					case 10:
						this.Codigo = 10;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos10DiasUteis);
						this.QuantidadeDias = 10;
						goto IL_0389;
					case 15:
						this.Codigo = 15;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos15DiasUteis);
						this.QuantidadeDias = 15;
						goto IL_0389;
					default:
						if (idInstrucao == 20)
						{
							this.Codigo = 20;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarApos20DiasUteis);
							this.QuantidadeDias = 20;
							goto IL_0389;
						}
						if (idInstrucao == 42)
						{
							this.Codigo = 42;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.DevolverApos15DiasVencido);
							this.QuantidadeDias = 15;
							goto IL_0389;
						}
						break;
					}
				}
				else
				{
					if (idInstrucao == 43)
					{
						this.Codigo = 43;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.DevolverApos30DiasVencido);
						this.QuantidadeDias = 30;
						goto IL_0389;
					}
					if (idInstrucao == 99)
					{
						this.Codigo = 99;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						this.QuantidadeDias = nrDias;
						goto IL_0389;
					}
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_0389;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0389;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0389;
					}
				}
				this.Codigo = 7;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBtg.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_0389:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000C81C File Offset: 0x0000AA1C
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 99 || this.Codigo == 3 || this.Codigo == 4 || this.Codigo == 5 || this.Codigo == 10 || this.Codigo == 15 || this.Codigo == 20;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

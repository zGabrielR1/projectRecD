using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000AA RID: 170
	public class InstrucaoSicoob : AbstractInstrucao
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000C870 File Offset: 0x0000AA70
		public InstrucaoSicoob()
		{
			try
			{
				this.Banco = new Banco(756);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public InstrucaoSicoob(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoSicoob();
				this.Valida();
				if (idInstrucao <= 42)
				{
					switch (idInstrucao)
					{
					case 1:
						this.Codigo = 1;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.CobrarJuros);
						this.QuantidadeDias = nrDias;
						goto IL_03B6;
					case 2:
					case 6:
					case 9:
					case 11:
					case 12:
					case 13:
					case 14:
						break;
					case 3:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos3DiasUteis);
						this.QuantidadeDias = 3;
						goto IL_03B6;
					case 4:
						this.Codigo = 4;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos4DiasUteis);
						this.QuantidadeDias = 4;
						goto IL_03B6;
					case 5:
						this.Codigo = 5;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos5DiasUteis);
						this.QuantidadeDias = 5;
						goto IL_03B6;
					case 7:
						this.Codigo = 7;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.NaoProtestar);
						this.QuantidadeDias = nrDias;
						goto IL_03B6;
					case 8:
						this.Codigo = 8;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.NegativacaoSemProtesto);
						this.QuantidadeDias = nrDias;
						goto IL_03B6;
					case 10:
						this.Codigo = 10;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos10DiasUteis);
						this.QuantidadeDias = 10;
						goto IL_03B6;
					case 15:
						this.Codigo = 15;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos15DiasUteis);
						this.QuantidadeDias = 15;
						goto IL_03B6;
					default:
						if (idInstrucao == 20)
						{
							this.Codigo = 20;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarApos20DiasUteis);
							this.QuantidadeDias = 20;
							goto IL_03B6;
						}
						if (idInstrucao == 42)
						{
							this.Codigo = 42;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.DevolverApos15DiasVencido);
							this.QuantidadeDias = 15;
							goto IL_03B6;
						}
						break;
					}
				}
				else
				{
					if (idInstrucao == 43)
					{
						this.Codigo = 43;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.DevolverApos30DiasVencido);
						this.QuantidadeDias = 30;
						goto IL_03B6;
					}
					if (idInstrucao == 99)
					{
						this.Codigo = 99;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						this.QuantidadeDias = nrDias;
						goto IL_03B6;
					}
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_03B6;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_03B6;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_03B6;
					}
				}
				this.Codigo = 7;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicoob.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_03B6:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 99 || this.Codigo == 10 || this.Codigo == 15 || this.Codigo == 20 || this.Codigo == 3 || this.Codigo == 4 || this.Codigo == 5;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

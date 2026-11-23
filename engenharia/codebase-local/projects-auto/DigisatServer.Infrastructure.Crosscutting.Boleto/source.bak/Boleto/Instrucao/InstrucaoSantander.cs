using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000A4 RID: 164
	public class InstrucaoSantander : AbstractInstrucao
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000BE64 File Offset: 0x0000A064
		public InstrucaoSantander()
		{
			try
			{
				this.Banco = new Banco(33);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public InstrucaoSantander(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoSantander();
				this.Valida();
				if (idInstrucao <= 9)
				{
					switch (idInstrucao)
					{
					case 0:
						this.Codigo = 0;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.NaoProtestar);
						goto IL_02A2;
					case 1:
						this.Codigo = 1;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.ProtestarAposNDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_02A2;
					case 2:
						this.Codigo = 2;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.ProtestarAposNDiasUteis).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_02A2;
					case 3:
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.UtilizarPerfilCedente);
						goto IL_02A2;
					default:
						if (idInstrucao == 9)
						{
							this.Codigo = 9;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.CancelamentoProtestoAutomatico);
							goto IL_02A2;
						}
						break;
					}
				}
				else
				{
					if (idInstrucao == 11)
					{
						this.Codigo = 11;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.BaixarDevolver);
						goto IL_02A2;
					}
					if (idInstrucao == 21)
					{
						this.Codigo = 21;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.NaoBaixarNaoDevolver);
						goto IL_02A2;
					}
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_02A2;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_02A2;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_02A2;
					}
				}
				this.Codigo = 0;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSantander.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_02A2:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000A7D8 File Offset: 0x000089D8
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1 || this.Codigo == 2;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

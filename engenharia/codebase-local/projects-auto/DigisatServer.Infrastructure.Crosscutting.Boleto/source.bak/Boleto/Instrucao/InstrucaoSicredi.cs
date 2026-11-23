using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x020000B2 RID: 178
	public class InstrucaoSicredi : AbstractInstrucao
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000D494 File Offset: 0x0000B694
		public InstrucaoSicredi()
		{
			try
			{
				this.Banco = new Banco(748);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public InstrucaoSicredi(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoSicredi();
				this.Valida();
				if (idInstrucao <= 3)
				{
					if (idInstrucao == 1)
					{
						this.Codigo = 1;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.ProtestarDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0241;
					}
					if (idInstrucao == 3)
					{
						this.Codigo = 3;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.NaoProtestar);
						goto IL_0241;
					}
				}
				else
				{
					switch (idInstrucao)
					{
					case 8:
						this.Codigo = 8;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.NegativacaoSemProtesto);
						goto IL_0241;
					case 9:
						this.Codigo = 9;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.CancelamentoProtestoAutomatico);
						goto IL_0241;
					case 10:
						break;
					case 11:
						this.Codigo = 11;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.BaixarDevolver);
						goto IL_0241;
					default:
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							goto IL_0241;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							goto IL_0241;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							goto IL_0241;
						}
						break;
					}
				}
				this.Codigo = 3;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesSicredi.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_0241:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00009E08 File Offset: 0x00008008
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 1;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

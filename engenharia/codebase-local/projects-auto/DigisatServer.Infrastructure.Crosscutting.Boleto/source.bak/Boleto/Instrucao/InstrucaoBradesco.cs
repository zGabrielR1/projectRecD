using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000097 RID: 151
	public class InstrucaoBradesco : AbstractInstrucao
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000A074 File Offset: 0x00008274
		public InstrucaoBradesco()
		{
			try
			{
				this.Banco = new Banco(237);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public InstrucaoBradesco(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A0CC File Offset: 0x000082CC
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoBradesco();
				this.Valida();
				switch (idInstrucao)
				{
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.ProtestoFalimentarAposXXDiasDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0355;
				case 6:
					this.Codigo = 6;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.ProtestarAposXXDiasDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0355;
				case 7:
					this.Codigo = 7;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.NegativarAposXXDiasDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0355;
				case 8:
					this.Codigo = 8;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.NaoCobrarJurosDeMora);
					goto IL_0355;
				case 9:
					this.Codigo = 9;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.NaoReceberAposVencimento);
					goto IL_0355;
				case 10:
					this.Codigo = 10;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.Multa10PorCentoApos4DiasDoVencimento);
					goto IL_0355;
				case 11:
					this.Codigo = 11;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.NaoReceberApos8DiasDoVencimento);
					goto IL_0355;
				case 12:
					this.Codigo = 12;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.CobrarEncargosApos5DiasDoVencimento);
					goto IL_0355;
				case 13:
					this.Codigo = 13;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.CobrarEncargosApos10DiasDoVencimento);
					goto IL_0355;
				case 14:
					this.Codigo = 14;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.CobrarEncargosApos15DiasDoVencimento);
					goto IL_0355;
				case 15:
					this.Codigo = 15;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.ConcederDescontoMesmoSePagoAposVencimento);
					goto IL_0355;
				case 16:
				case 17:
					break;
				case 18:
					this.Codigo = 18;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.DecursoDePrazo);
					goto IL_0355;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_0355;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0355;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBradesco.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0355;
					}
					break;
				}
				this.Codigo = 0;
				this.Descricao = "";
				this.QuantidadeDias = nrDias;
				IL_0355:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A464 File Offset: 0x00008664
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 5 || this.Codigo == 6;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

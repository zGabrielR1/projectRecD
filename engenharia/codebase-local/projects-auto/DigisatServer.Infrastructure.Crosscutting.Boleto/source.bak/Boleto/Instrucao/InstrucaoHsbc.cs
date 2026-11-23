using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200009C RID: 156
	internal class InstrucaoHsbc : AbstractInstrucao
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public InstrucaoHsbc()
		{
			try
			{
				this.Banco = new Banco(399);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A834 File Offset: 0x00008A34
		public InstrucaoHsbc(Boleto boleto, int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(boleto, codigo, valor, data, nrDias);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A84C File Offset: 0x00008A4C
		private void Carregar(Boleto boleto, int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoHsbc();
				this.Valida();
				if (idInstrucao <= 40)
				{
					if (idInstrucao <= 29)
					{
						switch (idInstrucao)
						{
						case 15:
							this.Codigo = 15;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaDePPPorCentoAposDiaDDMMAA).Replace("%%", string.Format("{0:0.0}", boleto.PercJurosMora)).Replace("DDMMAA", string.Format("{0:dd/MM/yy}", boleto.DataJurosMora));
							goto IL_0680;
						case 16:
						case 17:
						case 18:
						case 21:
							break;
						case 19:
							this.Codigo = 19;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaAposDiasCorridosDoVencimento).Replace("R$", string.Format("{0:0.00}", boleto.ValorMulta)).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
							goto IL_0680;
						case 20:
							this.Codigo = 20;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.CobrarJurosSoApos7DiasDoVencimento);
							goto IL_0680;
						case 22:
							this.Codigo = 22;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaAposDiasUteisDoVencimento).Replace("R$", string.Format("{0:0.00}", boleto.ValorMulta)).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
							goto IL_0680;
						case 23:
							this.Codigo = 23;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.NaoReceberAposVencimento);
							goto IL_0680;
						case 24:
							this.Codigo = 24;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaDeValorAposVencimento).Replace("R$", string.Format("{0:0.00}", boleto.ValorMulta));
							goto IL_0680;
						default:
							if (idInstrucao == 29)
							{
								this.Codigo = 29;
								this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.JurosSoAposDataCobrarDesdeOVencimento).Replace("DDMMAA", string.Format("{0:dd/MM/yy}", boleto.DataJurosMora));
								goto IL_0680;
							}
							break;
						}
					}
					else
					{
						if (idInstrucao == 34)
						{
							this.Codigo = 34;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.ConcederAbatimentoConformePropostoPeloSacado);
							goto IL_0680;
						}
						if (idInstrucao == 36)
						{
							this.Codigo = 36;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.AposVencimentoMultaDe10PorCento);
							goto IL_0680;
						}
						if (idInstrucao == 40)
						{
							this.Codigo = 40;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.ConcederDescontoMesmoSePagoAposVencimento);
							goto IL_0680;
						}
					}
				}
				else if (idInstrucao <= 53)
				{
					if (idInstrucao == 42)
					{
						this.Codigo = 42;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.NaoReceberAntesDoVencimentoInstrucaoDoCedente);
						goto IL_0680;
					}
					if (idInstrucao == 53)
					{
						this.Codigo = 53;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.AposVencimentoMultaDe20MaisMoraDe1AoMes);
						goto IL_0680;
					}
				}
				else
				{
					if (idInstrucao == 56)
					{
						this.Codigo = 56;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.NaoReceberAntesDoVencimentoOu10DiasApos);
						goto IL_0680;
					}
					switch (idInstrucao)
					{
					case 65:
						this.Codigo = 65;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.AbatimentoDescontoSoComInstrucaoDoCedente);
						goto IL_0680;
					case 66:
					case 69:
					case 70:
					case 76:
						break;
					case 67:
						this.Codigo = 67;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.TituloSujeitoAProtestoAposVencimento);
						goto IL_0680;
					case 68:
						this.Codigo = 68;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.AposVencimentoMultaDe2PorCento);
						goto IL_0680;
					case 71:
						this.Codigo = 71;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.NaoReceberAposDDDiasCorridosDoVencimento).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					case 72:
						this.Codigo = 72;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.NaoReceberAposDDDiasUteisDoVencimento).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					case 73:
						this.Codigo = 73;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaDePPPorCentoAposDDDiasCorridosDoVencimento).Replace("%%", string.Format("{0:0.0}", boleto.PercMulta)).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					case 74:
						this.Codigo = 74;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.MultaDePPPorCentoAposDDDiasUteisDoVencimento).Replace("%%", string.Format("{0:0.0}", boleto.PercMulta)).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					case 75:
						this.Codigo = 75;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.ProtestarDDDiasCorridosAposVencimentoSeNaoPago).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					case 77:
						this.Codigo = 77;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.ProtestarDDDiasUteisAposVencimentoSeNaoPago).Replace("DD", nrDias.ToString(CultureInfo.InvariantCulture));
						goto IL_0680;
					default:
						switch (idInstrucao)
						{
						case 997:
							this.Codigo = 997;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
							goto IL_0680;
						case 998:
							this.Codigo = 998;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							goto IL_0680;
						case 999:
							this.Codigo = 999;
							this.Descricao = Utils.RetornarDescricao(EnumInstrucoesHsbc.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
							goto IL_0680;
						}
						break;
					}
				}
				this.Codigo = 0;
				this.Descricao = "";
				this.QuantidadeDias = nrDias;
				IL_0680:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000AF0C File Offset: 0x0000910C
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 75 || this.Codigo == 77 || this.Codigo == 67;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

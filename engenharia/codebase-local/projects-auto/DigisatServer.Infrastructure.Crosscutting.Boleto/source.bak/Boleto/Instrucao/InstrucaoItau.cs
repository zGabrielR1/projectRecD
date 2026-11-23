using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200009E RID: 158
	public class InstrucaoItau : AbstractInstrucao
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000AF30 File Offset: 0x00009130
		public InstrucaoItau()
		{
			try
			{
				this.Banco = new Banco(341);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000AF74 File Offset: 0x00009174
		public InstrucaoItau(int codigo, int nrDias = 0)
		{
			this.Carregar(codigo, nrDias, null, 0m);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AF9D File Offset: 0x0000919D
		public InstrucaoItau(int codigo, DateTime? data, decimal valor, int nrDias = 0)
		{
			this.Carregar(codigo, nrDias, data, valor);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AFB0 File Offset: 0x000091B0
		private void Carregar(int idInstrucao, int nrDias, DateTime? data, decimal valor)
		{
			try
			{
				this.Banco = new BancoItau();
				this.Valida();
				switch (idInstrucao)
				{
				case 2:
					this.Codigo = 2;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos05DiasDoVencimento);
					goto IL_0AE2;
				case 3:
					this.Codigo = 3;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos30DiasDoVencimento);
					goto IL_0AE2;
				case 4:
				case 36:
				case 41:
				case 44:
				case 46:
				case 48:
				case 49:
				case 50:
				case 53:
				case 55:
				case 56:
				case 60:
				case 63:
				case 64:
				case 65:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 85:
				case 87:
				case 90:
					break;
				case 5:
					this.Codigo = 5;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ReceberConformeInstrucaoDoProprioTitulo);
					goto IL_0AE2;
				case 6:
					this.Codigo = 6;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos10DiasDoVencimento);
					goto IL_0AE2;
				case 7:
					this.Codigo = 7;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos15DiasDoVencimento);
					goto IL_0AE2;
				case 8:
					this.Codigo = 8;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos20DiasDoVencimento);
					goto IL_0AE2;
				case 9:
					this.Codigo = 9;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.Protestar);
					goto IL_0AE2;
				case 10:
					this.Codigo = 10;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoProtestar);
					goto IL_0AE2;
				case 11:
					this.Codigo = 11;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos25DiasDoVencimento);
					goto IL_0AE2;
				case 12:
					this.Codigo = 12;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos35DiasDoVencimento);
					goto IL_0AE2;
				case 13:
					this.Codigo = 13;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos40DiasDoVencimento);
					goto IL_0AE2;
				case 14:
					this.Codigo = 14;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos45DiasDoVencimento);
					goto IL_0AE2;
				case 15:
					this.Codigo = 15;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos50DiasDoVencimento);
					goto IL_0AE2;
				case 16:
					this.Codigo = 16;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos55DiasDoVencimento);
					goto IL_0AE2;
				case 17:
					this.Codigo = 17;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos60DiasDoVencimento);
					goto IL_0AE2;
				case 18:
					this.Codigo = 18;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos90DiasDoVencimento);
					goto IL_0AE2;
				case 19:
					this.Codigo = 19;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos05DiasDoVencimento);
					goto IL_0AE2;
				case 20:
					this.Codigo = 20;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos10DiasDoVencimento);
					goto IL_0AE2;
				case 21:
					this.Codigo = 21;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos15DiasDoVencimento);
					goto IL_0AE2;
				case 22:
					this.Codigo = 22;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos20DiasDoVencimento);
					goto IL_0AE2;
				case 23:
					this.Codigo = 23;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos25DiasDoVencimento);
					goto IL_0AE2;
				case 24:
					this.Codigo = 24;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos30DiasDoVencimento);
					goto IL_0AE2;
				case 25:
					this.Codigo = 25;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos35DiasDoVencimento);
					goto IL_0AE2;
				case 26:
					this.Codigo = 26;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos40DiasDoVencimento);
					goto IL_0AE2;
				case 27:
					this.Codigo = 27;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos45DiasDoVencimento);
					goto IL_0AE2;
				case 28:
					this.Codigo = 28;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos50DiasDoVencimento);
					goto IL_0AE2;
				case 29:
					this.Codigo = 29;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos55DiasDoVencimento);
					goto IL_0AE2;
				case 30:
					this.Codigo = 30;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ImportanciaDeDescontoPorDia);
					goto IL_0AE2;
				case 31:
					this.Codigo = 31;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos60DiasDoVencimento);
					goto IL_0AE2;
				case 32:
					this.Codigo = 32;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberApos90DiasDoVencimento);
					goto IL_0AE2;
				case 33:
					this.Codigo = 33;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ConcederAbatimentoRefaPisPasepCofinCsslMesmoAposVencimento);
					goto IL_0AE2;
				case 34:
					this.Codigo = 34;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ProtestarAposXXDiasCorridosDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				case 35:
					this.Codigo = 35;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ProtestarAposXXDiasUteisDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				case 37:
					this.Codigo = 37;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ReceberAteoUltimoDiaDoMêsDeVencimento);
					goto IL_0AE2;
				case 38:
					this.Codigo = 38;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ConcederDescontoMesmoAposVencimento);
					goto IL_0AE2;
				case 39:
					this.Codigo = 39;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberAposOVencimento);
					goto IL_0AE2;
				case 40:
					this.Codigo = 40;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ConcederDescontoConformeNotaDeCredito);
					goto IL_0AE2;
				case 42:
					this.Codigo = 42;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ProtestoParaFinsFalimentares);
					goto IL_0AE2;
				case 43:
					this.Codigo = 43;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.SujeitoAProtestoSeNaoForPagoNoVencimento);
					goto IL_0AE2;
				case 45:
					this.Codigo = 45;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.TemDiaDaGraca);
					goto IL_0AE2;
				case 47:
					this.Codigo = 47;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DispensarJurosComissaoDePermanencia);
					goto IL_0AE2;
				case 51:
					this.Codigo = 51;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ReceberSomenteComAParcelaAnteriorQuitada);
					goto IL_0AE2;
				case 52:
					this.Codigo = 52;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.EfetuarOPagamentoSomenteAtravesDesteBoletoENaRedeBancaria);
					goto IL_0AE2;
				case 54:
					this.Codigo = 54;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.AposVencimentoPagavelSomenteNaEmpresa);
					goto IL_0AE2;
				case 57:
					this.Codigo = 57;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.SomarValorDoTituloAoValorDoCampoMoraMultaCasoExista);
					goto IL_0AE2;
				case 58:
					this.Codigo = 58;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverApos365DiasDeVencido);
					goto IL_0AE2;
				case 59:
					this.Codigo = 59;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.CobrancaNegociadaPagavelSomentePorEsteBoletoNaRedeBancaria);
					goto IL_0AE2;
				case 61:
					this.Codigo = 61;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.TituloEntregueEmPenhorEmFavorDoCedenteAcima);
					goto IL_0AE2;
				case 62:
					this.Codigo = 62;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.TituloTransferidoAFavorDoCedente);
					goto IL_0AE2;
				case 66:
					this.Codigo = 66;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.SujeitoNegativacaoAposVencimento);
					goto IL_0AE2;
				case 78:
					this.Codigo = 78;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ValorDaIdaEnglobaMultaDe10ProRata);
					goto IL_0AE2;
				case 79:
					this.Codigo = 79;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.CobrarJurosApos15DiasDaEmissao);
					goto IL_0AE2;
				case 80:
					this.Codigo = 80;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.PagamentoEmChequeSomenteReceberComChequeDeEmissaoDoSacado);
					goto IL_0AE2;
				case 81:
					this.Codigo = 81;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ProtestarAposXXDiasCorridosDoVencimento81).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				case 82:
					this.Codigo = 82;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.ProtestarAposXXDiasUteisDoVencimento82).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				case 83:
					this.Codigo = 83;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.OperacaoRedAVendor);
					goto IL_0AE2;
				case 84:
					this.Codigo = 84;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.AposVencimentoConsultarAAgenciaCedente);
					goto IL_0AE2;
				case 86:
					this.Codigo = 86;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.AntesDoVencimentoOuApos15DiasPagavelSomenteEmNossaSede);
					goto IL_0AE2;
				case 88:
					this.Codigo = 88;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberAntesDoVencimento);
					goto IL_0AE2;
				case 89:
					this.Codigo = 89;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NoVencimentoPagavelEmQualquerAgenciaBancaria);
					goto IL_0AE2;
				case 91:
					this.Codigo = 91;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoReceberAposXXDiasDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				case 92:
					this.Codigo = 92;
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.DevolverAposXXDiasDoVencimento).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_0AE2;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.Multa).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0AE2;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0AE2;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.Desconto).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_0AE2;
					}
					break;
				}
				this.Codigo = 10;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesItau.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_0AE2:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 9 || this.Codigo == 34 || this.Codigo == 81 || this.Codigo == 35 || this.Codigo == 82;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

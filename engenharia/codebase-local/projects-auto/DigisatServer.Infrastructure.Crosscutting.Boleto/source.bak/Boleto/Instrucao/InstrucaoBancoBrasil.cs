using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x02000091 RID: 145
	public class InstrucaoBancoBrasil : AbstractInstrucao
	{
		// Token: 0x060002AA RID: 682 RVA: 0x000096B8 File Offset: 0x000078B8
		public InstrucaoBancoBrasil()
		{
			try
			{
				this.Banco = new Banco(1);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000096F8 File Offset: 0x000078F8
		public InstrucaoBancoBrasil(int codigo, decimal valor, DateTime? data, TipoArquivo tipoCobranca, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias, tipoCobranca);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009710 File Offset: 0x00007910
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias, TipoArquivo tipoCobranca)
		{
			try
			{
				this.Banco = new BancoBrasil();
				this.Valida();
				switch (idInstrucao)
				{
				case 1:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarAposNDiasCorridos, tipoCobranca);
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.ProtestarAposNDiasCorridos).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_03F5;
				case 2:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarAposNDiasUteis, tipoCobranca);
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.ProtestarAposNDiasUteis).Replace("XX", nrDias.ToString(CultureInfo.InvariantCulture));
					goto IL_03F5;
				case 3:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.NaoProtestar, tipoCobranca);
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.NaoProtestar);
					goto IL_03F5;
				case 4:
				case 5:
				case 6:
				case 7:
					break;
				case 8:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.NegativacaoSemProtesto, tipoCobranca);
					this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.NegativacaoSemProtesto);
					goto IL_03F5;
				case 9:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoTerceiroDiaUtil, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoTerceiroDiaUtil.RetornarDescricao();
					goto IL_03F5;
				case 10:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoQuartoDiaUtil, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoQuartoDiaUtil.RetornarDescricao();
					goto IL_03F5;
				case 11:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoQuintoDiaUtil, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoQuintoDiaUtil.RetornarDescricao();
					goto IL_03F5;
				case 12:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoDecimoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoDecimoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				case 13:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoDecimoQuintoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoDecimoQuintoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				case 14:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoVigesimoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoVigesimoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				case 15:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoVigesimoQuintoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoVigesimoQuintoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				case 16:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoTrigesimoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoTrigesimoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				case 17:
					this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.ProtestarNoQuadragesimoQuintoDiaCorrido, tipoCobranca);
					this.Descricao = EnumInstrucoesBancoBrasil.ProtestarNoQuadragesimoQuintoDiaCorrido.RetornarDescricao();
					goto IL_03F5;
				default:
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.Multa, tipoCobranca);
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_03F5;
					case 998:
						this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.JurosDeMora, tipoCobranca);
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_03F5;
					case 999:
						this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.Desconto, tipoCobranca);
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.Desconto).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy"));
						goto IL_03F5;
					}
					break;
				}
				this.Codigo = this.GetCodigoByEnum(EnumInstrucoesBancoBrasil.NaoProtestar, tipoCobranca);
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesBancoBrasil.NaoProtestar);
				this.QuantidadeDias = nrDias;
				IL_03F5:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009B48 File Offset: 0x00007D48
		private int GetCodigoByEnum(EnumInstrucoesBancoBrasil enumInstrucoesBancoBrasil, TipoArquivo tipoCobranca)
		{
			switch (enumInstrucoesBancoBrasil)
			{
			case EnumInstrucoesBancoBrasil.ProtestarAposNDiasCorridos:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 6;
				}
				return 1;
			case EnumInstrucoesBancoBrasil.ProtestarAposNDiasUteis:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 0;
				}
				return 2;
			case EnumInstrucoesBancoBrasil.NaoProtestar:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 7;
				}
				return 3;
			case (EnumInstrucoesBancoBrasil)4:
			case (EnumInstrucoesBancoBrasil)5:
			case (EnumInstrucoesBancoBrasil)6:
			case (EnumInstrucoesBancoBrasil)7:
				break;
			case EnumInstrucoesBancoBrasil.NegativacaoSemProtesto:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 88;
				}
				return 8;
			case EnumInstrucoesBancoBrasil.ProtestarNoTerceiroDiaUtil:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 3;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoQuartoDiaUtil:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 4;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoQuintoDiaUtil:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 5;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoDecimoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 10;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoDecimoQuintoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 15;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoVigesimoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 20;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoVigesimoQuintoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 25;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoTrigesimoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 30;
				}
				return -1;
			case EnumInstrucoesBancoBrasil.ProtestarNoQuadragesimoQuintoDiaCorrido:
				if (tipoCobranca != TipoArquivo.CNAB240)
				{
					return 40;
				}
				return -1;
			default:
				switch (enumInstrucoesBancoBrasil)
				{
				case EnumInstrucoesBancoBrasil.Multa:
					if (tipoCobranca != TipoArquivo.CNAB240)
					{
						return 0;
					}
					return 997;
				case EnumInstrucoesBancoBrasil.JurosDeMora:
					if (tipoCobranca != TipoArquivo.CNAB240)
					{
						return 1;
					}
					return 998;
				case EnumInstrucoesBancoBrasil.Desconto:
					if (tipoCobranca != TipoArquivo.CNAB240)
					{
						return 22;
					}
					return 999;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("enumInstrucoesBancoBrasil", enumInstrucoesBancoBrasil, null);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009C64 File Offset: 0x00007E64
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return this.Codigo == 2 || this.Codigo == 1 || this.Codigo == 9 || this.Codigo == 10 || this.Codigo == 11 || this.Codigo == 12 || this.Codigo == 13 || this.Codigo == 14 || this.Codigo == 15 || this.Codigo == 16 || this.Codigo == 17;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

using System;
using System.Globalization;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008F RID: 143
	public class InstrucaoNordeste : AbstractInstrucao
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000942C File Offset: 0x0000762C
		public InstrucaoNordeste()
		{
			try
			{
				this.Banco = new Banco(4);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000946C File Offset: 0x0000766C
		public InstrucaoNordeste(int codigo, decimal valor, DateTime? data, int nrDias = 0)
		{
			this.Carregar(codigo, valor, data, nrDias);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009480 File Offset: 0x00007680
		private void Carregar(int idInstrucao, decimal valor, DateTime? data, int nrDias)
		{
			try
			{
				this.Banco = new BancoNordeste();
				this.Valida();
				if (idInstrucao <= 8)
				{
					if (idInstrucao == 5)
					{
						this.Codigo = 5;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.AcatarInstrucoesContidasNoTitulo);
						goto IL_01F8;
					}
					if (idInstrucao == 8)
					{
						this.Codigo = 8;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.NaoCobrarEncargosMoratorios);
						goto IL_01F8;
					}
				}
				else
				{
					if (idInstrucao == 12)
					{
						this.Codigo = 12;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.NaoReceberAposVencimento);
						goto IL_01F8;
					}
					if (idInstrucao == 15)
					{
						this.Codigo = 15;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.AposVencimentoCobrarComissaoDePermanenciaDoBancoDoNordeste);
						goto IL_01F8;
					}
					switch (idInstrucao)
					{
					case 997:
						this.Codigo = 997;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.Multa).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", string.Format("{0:dd/MM/yyyy}", data));
						goto IL_01F8;
					case 998:
						this.Codigo = 998;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.JurosDeMora).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy")).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR")));
						goto IL_01F8;
					case 999:
						this.Codigo = 999;
						this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.Desconto).Replace("R$", valor.ToString("c2", new CultureInfo("pt-BR"))).Replace("DDMMAA", data.ToDateTimeOrMinValue().ToString("dd/MM/yyyy"));
						goto IL_01F8;
					}
				}
				this.Codigo = 5;
				this.Descricao = Utils.RetornarDescricao(EnumInstrucoesNordeste.AcatarInstrucoesContidasNoTitulo);
				IL_01F8:
				this.QuantidadeDias = nrDias;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00009044 File Offset: 0x00007244
		public override bool IsInstrucaoProtesto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009047 File Offset: 0x00007247
		public override void Valida()
		{
		}
	}
}

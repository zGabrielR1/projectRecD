using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200008E RID: 142
	public enum EnumInstrucoesNordeste
	{
		// Token: 0x04000209 RID: 521
		[Descricao("Acatar instruções contidas no título.")]
		AcatarInstrucoesContidasNoTitulo = 5,
		// Token: 0x0400020A RID: 522
		[Descricao("Não cobrar encargos moratórios.")]
		NaoCobrarEncargosMoratorios = 8,
		// Token: 0x0400020B RID: 523
		[Descricao("Não receber após vencimento.")]
		NaoReceberAposVencimento = 12,
		// Token: 0x0400020C RID: 524
		[Descricao("Após vencimento, cobrar comissão de permanência do BANCO DO NORDESTE.")]
		AposVencimentoCobrarComissaoDePermanenciaDoBancoDoNordeste = 15,
		// Token: 0x0400020D RID: 525
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400020E RID: 526
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x0400020F RID: 527
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

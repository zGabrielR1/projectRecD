using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Enums
{
	// Token: 0x0200001C RID: 28
	public enum CategoriaLancamento
	{
		// Token: 0x0400007D RID: 125
		[Descricao("Cheques")]
		DebitoCheques = 101,
		// Token: 0x0400007E RID: 126
		DebitoEncargos,
		// Token: 0x0400007F RID: 127
		DebitoEstornos,
		// Token: 0x04000080 RID: 128
		DebitoLancamentoAvisado,
		// Token: 0x04000081 RID: 129
		[Descricao("Tarifas")]
		DebitoTarifas,
		// Token: 0x04000082 RID: 130
		DebitoAplicacao,
		// Token: 0x04000083 RID: 131
		DebitoEmprestimoOuFinanciamento,
		// Token: 0x04000084 RID: 132
		DebitoCambio,
		// Token: 0x04000085 RID: 133
		DebitoCpmf,
		// Token: 0x04000086 RID: 134
		DebitoIof,
		// Token: 0x04000087 RID: 135
		DebitoImpostoRenda,
		// Token: 0x04000088 RID: 136
		DebitoPagamentoFornecedores,
		// Token: 0x04000089 RID: 137
		DebitoPagamentosSalario,
		// Token: 0x0400008A RID: 138
		DebitoSaqueEletronico,
		// Token: 0x0400008B RID: 139
		DebitoAcoes,
		// Token: 0x0400008C RID: 140
		DebitoTransferenciaEntreContas = 117,
		// Token: 0x0400008D RID: 141
		DebitoDevolucaoCompensacao,
		// Token: 0x0400008E RID: 142
		DebitoDevolucaoChequeDepositado,
		// Token: 0x0400008F RID: 143
		DebitoTransferenciaInterbancariaDocTed,
		// Token: 0x04000090 RID: 144
		DebitoAntecipacaoAFornecedores,
		// Token: 0x04000091 RID: 145
		DebitoOcAerops,
		// Token: 0x04000092 RID: 146
		CreditoDepositos = 201,
		// Token: 0x04000093 RID: 147
		CreditoLiquidoDeCobranca,
		// Token: 0x04000094 RID: 148
		CreditoDevolucaoDeCheques,
		// Token: 0x04000095 RID: 149
		CreditoEstornos,
		// Token: 0x04000096 RID: 150
		CreditoLancamentoAvisado,
		// Token: 0x04000097 RID: 151
		CreditoResgateDeAplicacao,
		// Token: 0x04000098 RID: 152
		CreditoEmprestimoOuFinanciamento,
		// Token: 0x04000099 RID: 153
		CreditoCambio,
		// Token: 0x0400009A RID: 154
		CreditoTransferenciaInterbancariaDocTed,
		// Token: 0x0400009B RID: 155
		CreditoAcoes,
		// Token: 0x0400009C RID: 156
		CreditoDividendos,
		// Token: 0x0400009D RID: 157
		CreditoSeguro,
		// Token: 0x0400009E RID: 158
		CreditoTransferenciaEntreContas,
		// Token: 0x0400009F RID: 159
		CreditoDepositosEspeciais,
		// Token: 0x040000A0 RID: 160
		CreditoDevolucaoDaCompensacao,
		// Token: 0x040000A1 RID: 161
		CreditoOct,
		// Token: 0x040000A2 RID: 162
		CreditoPagamentosFornecedores,
		// Token: 0x040000A3 RID: 163
		CreditoPagamentosDiversos,
		// Token: 0x040000A4 RID: 164
		CreditoPagamentosSalarios
	}
}

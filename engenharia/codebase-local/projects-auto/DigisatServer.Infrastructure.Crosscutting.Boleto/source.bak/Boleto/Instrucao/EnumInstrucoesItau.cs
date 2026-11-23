using System;
using DigisatServer.Infrastructure.Crosscutting.Attribute;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao
{
	// Token: 0x0200009D RID: 157
	public enum EnumInstrucoesItau
	{
		// Token: 0x04000260 RID: 608
		[Descricao("Devolver após 5 dias do vencimento.")]
		DevolverApos05DiasDoVencimento = 2,
		// Token: 0x04000261 RID: 609
		[Descricao("Devolver após 30 dias do vencimento.")]
		DevolverApos30DiasDoVencimento,
		// Token: 0x04000262 RID: 610
		[Descricao("Receber conforme instrução do próprio título.")]
		ReceberConformeInstrucaoDoProprioTitulo = 5,
		// Token: 0x04000263 RID: 611
		[Descricao("Devolver após 10 dias do vencimento.")]
		DevolverApos10DiasDoVencimento,
		// Token: 0x04000264 RID: 612
		[Descricao("Devolver após 15 dias do vencimento.")]
		DevolverApos15DiasDoVencimento,
		// Token: 0x04000265 RID: 613
		[Descricao("Devolver após 20 dias do vencimento.")]
		DevolverApos20DiasDoVencimento,
		// Token: 0x04000266 RID: 614
		[Descricao("Protestar após 5 dias úteis.")]
		Protestar,
		// Token: 0x04000267 RID: 615
		[Descricao("Não protestar.")]
		NaoProtestar,
		// Token: 0x04000268 RID: 616
		[Descricao("Devolver após 25 dias do vencimento.")]
		DevolverApos25DiasDoVencimento,
		// Token: 0x04000269 RID: 617
		[Descricao("Devolver após 35 dias do vencimento.")]
		DevolverApos35DiasDoVencimento,
		// Token: 0x0400026A RID: 618
		[Descricao("Devolver após 40 dias do vencimento.")]
		DevolverApos40DiasDoVencimento,
		// Token: 0x0400026B RID: 619
		[Descricao("Devolver após 45 dias do vencimento.")]
		DevolverApos45DiasDoVencimento,
		// Token: 0x0400026C RID: 620
		[Descricao("Devolver após 50 dias do vencimento.")]
		DevolverApos50DiasDoVencimento,
		// Token: 0x0400026D RID: 621
		[Descricao("Devolver após 55 dias do vencimento.")]
		DevolverApos55DiasDoVencimento,
		// Token: 0x0400026E RID: 622
		[Descricao("Devolver após 60 dias do vencimento.")]
		DevolverApos60DiasDoVencimento,
		// Token: 0x0400026F RID: 623
		[Descricao("Devolver após 90 dias do vencimento.")]
		DevolverApos90DiasDoVencimento,
		// Token: 0x04000270 RID: 624
		[Descricao("Não receber após 5 dias do vencimento.")]
		NaoReceberApos05DiasDoVencimento,
		// Token: 0x04000271 RID: 625
		[Descricao("Não receber após 10 dias do vencimento.")]
		NaoReceberApos10DiasDoVencimento,
		// Token: 0x04000272 RID: 626
		[Descricao("Não receber após 15 dias do vencimento.")]
		NaoReceberApos15DiasDoVencimento,
		// Token: 0x04000273 RID: 627
		[Descricao("Não receber após 20 dias do vencimento.")]
		NaoReceberApos20DiasDoVencimento,
		// Token: 0x04000274 RID: 628
		[Descricao("Não receber após 25 dias do vencimento.")]
		NaoReceberApos25DiasDoVencimento,
		// Token: 0x04000275 RID: 629
		[Descricao("Não receber após 30 dias do vencimento.")]
		NaoReceberApos30DiasDoVencimento,
		// Token: 0x04000276 RID: 630
		[Descricao("Não receber após 35 dias do vencimento.")]
		NaoReceberApos35DiasDoVencimento,
		// Token: 0x04000277 RID: 631
		[Descricao("Não receber após 40 dias do vencimento.")]
		NaoReceberApos40DiasDoVencimento,
		// Token: 0x04000278 RID: 632
		[Descricao("Não receber após 45 dias do vencimento.")]
		NaoReceberApos45DiasDoVencimento,
		// Token: 0x04000279 RID: 633
		[Descricao("Não receber após 50 dias do vencimento.")]
		NaoReceberApos50DiasDoVencimento,
		// Token: 0x0400027A RID: 634
		[Descricao("Não receber após 55 dias do vencimento.")]
		NaoReceberApos55DiasDoVencimento,
		// Token: 0x0400027B RID: 635
		[Descricao("Importância de desconto por dia")]
		ImportanciaDeDescontoPorDia,
		// Token: 0x0400027C RID: 636
		[Descricao("Não receber após 60 dias do vencimento.")]
		NaoReceberApos60DiasDoVencimento,
		// Token: 0x0400027D RID: 637
		[Descricao("Não receber após 90 dias do vencimento.")]
		NaoReceberApos90DiasDoVencimento,
		// Token: 0x0400027E RID: 638
		[Descricao("Conceder abatimento ref. à PIS-PASEP/COFIN/CSSL, mesmo após vencimento.")]
		ConcederAbatimentoRefaPisPasepCofinCsslMesmoAposVencimento,
		// Token: 0x0400027F RID: 639
		[Descricao("Protestar após XX dias corridos do vencimento.")]
		ProtestarAposXXDiasCorridosDoVencimento,
		// Token: 0x04000280 RID: 640
		[Descricao("Protestar após XX dias úteis do vencimento.")]
		ProtestarAposXXDiasUteisDoVencimento,
		// Token: 0x04000281 RID: 641
		[Descricao("Receber até o último dia do mês de vencimento.")]
		ReceberAteoUltimoDiaDoMêsDeVencimento = 37,
		// Token: 0x04000282 RID: 642
		[Descricao("Conceder desconto mesmo após vencimento.")]
		ConcederDescontoMesmoAposVencimento,
		// Token: 0x04000283 RID: 643
		[Descricao("Não receber após o vencimento.")]
		NaoReceberAposOVencimento,
		// Token: 0x04000284 RID: 644
		[Descricao("Conceder desconto conforme nota de crédito.")]
		ConcederDescontoConformeNotaDeCredito,
		// Token: 0x04000285 RID: 645
		[Descricao("Protesto para fins falimentares.")]
		ProtestoParaFinsFalimentares = 42,
		// Token: 0x04000286 RID: 646
		[Descricao("Sujeito a protesto se não for pago no vencimento.")]
		SujeitoAProtestoSeNaoForPagoNoVencimento,
		// Token: 0x04000287 RID: 647
		[Descricao("Tem dia da graça.")]
		TemDiaDaGraca = 45,
		// Token: 0x04000288 RID: 648
		[Descricao("Dispensar juros/comissão de permanência.")]
		DispensarJurosComissaoDePermanencia = 47,
		// Token: 0x04000289 RID: 649
		[Descricao("Receber somente com a parcela anterior quitada.")]
		ReceberSomenteComAParcelaAnteriorQuitada = 51,
		// Token: 0x0400028A RID: 650
		[Descricao("Efetuar o pagamento somente através deste boleto e na rede bancária.")]
		EfetuarOPagamentoSomenteAtravesDesteBoletoENaRedeBancaria,
		// Token: 0x0400028B RID: 651
		[Descricao("Após vencimento pagável somente na empresa.")]
		AposVencimentoPagavelSomenteNaEmpresa = 54,
		// Token: 0x0400028C RID: 652
		[Descricao("Somar valor do título ao valor do campo mora/multa caso exista.")]
		SomarValorDoTituloAoValorDoCampoMoraMultaCasoExista = 57,
		// Token: 0x0400028D RID: 653
		[Descricao("Devolver após 365 dias de vencido.")]
		DevolverApos365DiasDeVencido,
		// Token: 0x0400028E RID: 654
		[Descricao("Cobrança negociada. Pagável somente por este boleto na rede bancária.")]
		CobrancaNegociadaPagavelSomentePorEsteBoletoNaRedeBancaria,
		// Token: 0x0400028F RID: 655
		[Descricao("Título entregue em penhor em favor do cedente acima.")]
		TituloEntregueEmPenhorEmFavorDoCedenteAcima = 61,
		// Token: 0x04000290 RID: 656
		[Descricao("Título transferido a favor do cedente.")]
		TituloTransferidoAFavorDoCedente,
		// Token: 0x04000291 RID: 657
		[Descricao("Sujeito a negativação após o vencimento.")]
		SujeitoNegativacaoAposVencimento = 66,
		// Token: 0x04000292 RID: 658
		[Descricao("Valor da ida engloba multa de 10% pro rata.")]
		ValorDaIdaEnglobaMultaDe10ProRata = 78,
		// Token: 0x04000293 RID: 659
		[Descricao("Cobrar juros após 15 dias da emissão (para títulos com vencimento à vista).")]
		CobrarJurosApos15DiasDaEmissao,
		// Token: 0x04000294 RID: 660
		[Descricao("Pagamento em cheque: somente receber com cheque de emissão do sacado.")]
		PagamentoEmChequeSomenteReceberComChequeDeEmissaoDoSacado,
		// Token: 0x04000295 RID: 661
		[Descricao("81 Protestar após XX dias corridos do vencimento.")]
		ProtestarAposXXDiasCorridosDoVencimento81,
		// Token: 0x04000296 RID: 662
		[Descricao("82 Protestar após XX dias úteis do vencimento.")]
		ProtestarAposXXDiasUteisDoVencimento82,
		// Token: 0x04000297 RID: 663
		[Descricao("Operação Red a vendor.")]
		OperacaoRedAVendor,
		// Token: 0x04000298 RID: 664
		[Descricao("Após vencimento consultar a agência cedente.")]
		AposVencimentoConsultarAAgenciaCedente,
		// Token: 0x04000299 RID: 665
		[Descricao("Antes do vencimento ou após 15 dias, pagável somente em nossa sede.")]
		AntesDoVencimentoOuApos15DiasPagavelSomenteEmNossaSede = 86,
		// Token: 0x0400029A RID: 666
		[Descricao("Não receber antes do vencimento.")]
		NaoReceberAntesDoVencimento = 88,
		// Token: 0x0400029B RID: 667
		[Descricao("No vencimento pagável em qualquer agência bancária.")]
		NoVencimentoPagavelEmQualquerAgenciaBancaria,
		// Token: 0x0400029C RID: 668
		[Descricao("Não receber após XX dias do vencimento.")]
		NaoReceberAposXXDiasDoVencimento = 91,
		// Token: 0x0400029D RID: 669
		[Descricao("Devolver após XX dias do vencimento.")]
		DevolverAposXXDiasDoVencimento,
		// Token: 0x0400029E RID: 670
		[Descricao("Após DDMMAA cobrar multa de R$.")]
		Multa = 997,
		// Token: 0x0400029F RID: 671
		[Descricao("Após DDMMAA cobrar juros de R$ por dia de atraso.")]
		JurosDeMora,
		// Token: 0x040002A0 RID: 672
		[Descricao("Até DDMMAA, conceder desconto de R$.")]
		Desconto
	}
}

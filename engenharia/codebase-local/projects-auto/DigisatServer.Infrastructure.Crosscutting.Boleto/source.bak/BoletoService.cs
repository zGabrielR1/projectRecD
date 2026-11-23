using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Facade;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Model.Boletos;
using Pechkin;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto
{
	// Token: 0x02000003 RID: 3
	public static class BoletoService
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002190 File Offset: 0x00000390
		private static void Criar(IBoletoProxy boletoProxy)
		{
			SacadoFacade sacadoFacade;
			try
			{
				EnderecoFacade enderecoFacade = FluentFactory<EnderecoFacadeFactory>.Init().SetarLogradouro(boletoProxy.Sacado.EnderecoBoleto.Logradouro).SetarBairro(boletoProxy.Sacado.EnderecoBoleto.Bairro)
					.SetarCidade(boletoProxy.Sacado.EnderecoBoleto.Municipio)
					.SetarCep(boletoProxy.Sacado.EnderecoBoleto.CEP)
					.SetarComplemento(boletoProxy.Sacado.EnderecoBoleto.Complemento)
					.SetarNumero(boletoProxy.Sacado.EnderecoBoleto.Numero)
					.SetarUf(boletoProxy.Sacado.EnderecoBoleto.UF)
					.SetarEmail(boletoProxy.Sacado.EnderecoBoleto.Email)
					.Criar();
				sacadoFacade = FluentFactory<SacadoFacadeFactory>.Init().SetarCpfCnpj(boletoProxy.Sacado.Documento).SetarNome(boletoProxy.Sacado.Nome)
					.SetarEnderecoFacade(enderecoFacade)
					.SetarNaoProtestar(boletoProxy.Sacado.NaoProtestar)
					.Criar();
			}
			catch (Exception)
			{
				throw new Exception("Erro ao validar boleto. Cadastro incompleto da pessoa!");
			}
			BancoFacade bancoFacade = FluentFactory<BancoFacadeFactory>.Init().SetarCodigo(boletoProxy.ContaBoleto.CodigoBanco).SetarCodigoEspecieDocumento(boletoProxy.ContaBoleto.EspecieTitulo)
				.SetarCodigoFormaCadastramento(boletoProxy.ContaBoleto.FormaCadastramento)
				.SetarCodigoTipoCobranca(boletoProxy.ContaBoleto.TipoCobranca)
				.SetarCodigoTipoDistribuicao(boletoProxy.ContaBoleto.TipoDistribuicao)
				.SetarCodigoTipoEmissao(boletoProxy.ContaBoleto.TipoEmissao)
				.SetarCodigoTipoTransmissao(boletoProxy.ContaBoleto.TipoTransmissao)
				.SetarSolucao(boletoProxy.ContaBoleto.Solucao)
				.Criar();
			ContaBancariaFacade contaBancariaFacade = FluentFactory<ContaBancariaFacadeFactory>.Init().SetarAgencia(boletoProxy.ContaBoleto.NumeroAgencia).SetarDigitoAgencia(boletoProxy.ContaBoleto.DigitoAgencia)
				.SetarConta(boletoProxy.ContaBoleto.NumeroConta)
				.SetarDigitoConta(boletoProxy.ContaBoleto.DigitoConta)
				.SetarOperacaoConta(boletoProxy.ContaBoleto.Operacao)
				.Criar();
			EnderecoFacade enderecoFacade2 = FluentFactory<EnderecoFacadeFactory>.Init().SetarLogradouro(boletoProxy.Cedente.EnderecoBoleto.Logradouro).SetarBairro(boletoProxy.Cedente.EnderecoBoleto.Bairro)
				.SetarCidade(boletoProxy.Cedente.EnderecoBoleto.Municipio)
				.SetarCep(boletoProxy.Cedente.EnderecoBoleto.CEP)
				.SetarComplemento(boletoProxy.Cedente.EnderecoBoleto.Complemento)
				.SetarNumero(boletoProxy.Cedente.EnderecoBoleto.Numero)
				.SetarUf(boletoProxy.Cedente.EnderecoBoleto.UF)
				.SetarEmail(boletoProxy.Cedente.EnderecoBoleto.Email)
				.Criar();
			int num2;
			int num = (int.TryParse(boletoProxy.ContaBoleto.DigitoCodigoCedente, out num2) ? num2 : 0);
			CedenteFacade cedenteFacade = FluentFactory<CedenteFacadeFactory>.Init().SetarNome(boletoProxy.Cedente.Nome).SetarCarteira(boletoProxy.ContaBoleto.Carteira.ToString())
				.SetarCnpj(boletoProxy.Cedente.Documento)
				.SetarCodigo(boletoProxy.ContaBoleto.CodigoCedente)
				.SetarConvenio(boletoProxy.ContaBoleto.Convenio)
				.SetarContrato(boletoProxy.ContaBoleto.Contrato)
				.SetarDigito(num)
				.SetarFinalidadeArquivoRemessaRetorno(boletoProxy.ContaBoleto.FinalidadeArquivoRemessaRetorno)
				.SetarTipoSolucao(boletoProxy.ContaBoleto.Solucao)
				.SetarVariacaoCarteira(boletoProxy.ContaBoleto.VariacaoCarteira)
				.SetarContaBancariaFacade(contaBancariaFacade)
				.SetarEnderecoFacade(enderecoFacade2)
				.SetarBeneficiario(boletoProxy.ContaBoleto.Beneficiario)
				.Criar();
			InstrucaoFacade instrucaoFacade = FluentFactory<InstrucaoFacadeFactory>.Init().SetarCodigo(boletoProxy.ContaBoleto.PrimeiraInstrucao).SetarDias(boletoProxy.ContaBoleto.DiasPrimeiraInstrucao)
				.Criar();
			InstrucaoFacade instrucaoFacade2 = FluentFactory<InstrucaoFacadeFactory>.Init().SetarCodigo(boletoProxy.ContaBoleto.SegundaInstrucao).SetarDias(boletoProxy.ContaBoleto.DiasSegundaInstrucao)
				.Criar();
			BoletoService._boletoFacade = FluentFactory<BoletoFacadeFactory>.Init().SetarAceite(boletoProxy.ContaBoleto.Aceite).SetarBancoFacade(bancoFacade)
				.SetarCarteira(boletoProxy.ContaBoleto.Carteira.ToString())
				.SetarCedenteFacade(cedenteFacade)
				.SetarLocalPagamento(boletoProxy.ContaBoleto.LocalPagamento)
				.SetarPercentualJurosMora(boletoProxy.ContaBoleto.PercentualJuro)
				.SetarPercentualMulta(boletoProxy.ContaBoleto.PercentualMulta)
				.SetarSacadoFacade(sacadoFacade)
				.SetarVariacao(boletoProxy.ContaBoleto.VariacaoCarteira)
				.SetarPrimeiraInstrucaoFacade(instrucaoFacade)
				.SetarSegundaInstrucaoFacade(instrucaoFacade2)
				.SetarDataDocumento(boletoProxy.DataEfetivacao)
				.SetarDataProcessamento(boletoProxy.DataEmissao)
				.SetarDataVencimento(boletoProxy.DataVencimento)
				.SetarIdentificacaoTituloEmpresa(boletoProxy.IdentificacaoTituloNaEmpresa)
				.SetarValorBoleto(boletoProxy.Valor)
				.SetarValorJurosMora(boletoProxy.ValorJurosMora)
				.SetarValorMulta(boletoProxy.ValorMulta)
				.SetarNossoNumero(boletoProxy.NossoNumero)
				.SetarNumeroDocumento(boletoProxy.Documento)
				.SetarNumeroParcela(boletoProxy.Ordem)
				.SetarDiasIniciarCobrancaDeJuros(boletoProxy.ContaBoleto.DiasIniciarCobrancaDeJuros)
				.SetarDiasIniciarCobrancaDeMulta(boletoProxy.ContaBoleto.DiasIniciarCobrancaDeMulta)
				.SetarDiasAntesVencimentoParaAplicarDesconto(boletoProxy.ContaBoleto.DiasAntesVencimentoParaAplicarDesconto)
				.SetarPercentualDesconto(boletoProxy.ContaBoleto.PercentualDesconto)
				.SetarMostrarComprovanteEntrega(boletoProxy.ContaBoleto.MostrarComprovanteEntrega)
				.SetarFormatoCarne(boletoProxy.ContaBoleto.FormatoCarne)
				.SetarSolicitacaoDeBaixa(boletoProxy.SolicitacaoDeBaixa)
				.SetarAlteracaoVencimento(boletoProxy.AlteracaoVencimento)
				.SetarTipoArquivo(boletoProxy.ContaBoleto.TipoArquivo)
				.AdicionarInstrucao(boletoProxy.ContaBoleto.OutrasInstrucoes)
				.SetarCodigoExclusivoBanese(boletoProxy.ContaBoleto.CodigoExclusivoBanese);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002730 File Offset: 0x00000930
		private static string GerarHtml(IEnumerable<IBoletoProxy> boletosProxy)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IBoletoProxy boletoProxy in boletosProxy)
			{
				BoletoService.Criar(boletoProxy);
				stringBuilder.Append(BoletoService._boletoFacade.Criar().GerarHtml());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002798 File Offset: 0x00000998
		public static string ObterNossoNumeroFormatado(IBoletoProxy boletoProxy)
		{
			BoletoService.Criar(boletoProxy);
			return BoletoService._boletoFacade.Criar().NossoNumeroFormatado;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000027B0 File Offset: 0x000009B0
		public static string ObterArquivoPdf(List<IBoletoProxy> boletosProxy)
		{
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 77))
			{
				throw new Exception("Boletos do 'Banco Intermedium S.A.' devem ser gerados pelo portal do banco.");
			}
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 707))
			{
				if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.TipoDistribuicao.Equals(1)))
				{
					throw new Exception("Boletos do 'Daycoval' devem ser gerados pelo portal do banco.");
				}
			}
			string text = BoletoService.GerarHtml(boletosProxy);
			byte[] array = (Environment.Is64BitProcess ? BoletoService.ConverterComTuesPechkin(text) : BoletoService.ConverterComPechkin(text));
			string text2 = Path.GetTempFileName().Replace(".tmp", ".pdf");
			File.WriteAllBytes(text2, array);
			return text2;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002880 File Offset: 0x00000A80
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static byte[] ConverterComPechkin(string html)
		{
			ObjectConfig objectConfig = new ObjectConfig().SetLoadImages(true).SetZoomFactor(1.7).SetIntelligentShrinking(true)
				.SetPrintBackground(true)
				.SetScreenMediaType(true)
				.SetCreateExternalLinks(true);
			return new SimplePechkin(new GlobalConfig().SetOutputFormat(GlobalConfig.OutputFormat.Pdf).SetDocumentTitle("Desenvolvido por DigiSat Tecnologia - www.digisat.com.br")).Convert(objectConfig, html);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000028E0 File Offset: 0x00000AE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static byte[] ConverterComTuesPechkin(string html)
		{
			return ConversorHtmlPdfTuesPechkin.Converter(html, "Desenvolvido por DigiSat Tecnologia - www.digisat.com.br");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static string ObterArquivoPdfMobile(List<IBoletoProxy> boletosProxy)
		{
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 77))
			{
				throw new Exception("Boletos do 'Banco Intermedium S.A.' devem ser gerados pelo portal do banco.");
			}
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 707))
			{
				if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.TipoDistribuicao.Equals(1)))
				{
					throw new Exception("Boletos do 'Daycoval' devem ser gerados pelo portal do banco.");
				}
			}
			List<Boleto> list = new List<Boleto>();
			foreach (IBoletoProxy boletoProxy in boletosProxy)
			{
				BoletoService.Criar(boletoProxy);
				Boleto boleto = BoletoService._boletoFacade.Criar().Boleto;
				boleto.Banco.FormataNossoNumeroImpressao(boleto);
				list.Add(boleto);
			}
			XtraReport xtraReport = new Boleto79Mm80Mm(list, 0, 0).Processar();
			string text = string.Format("Boleto_{0}.pdf", Guid.NewGuid());
			string text2 = Path.Combine(Path.GetTempPath(), text);
			xtraReport.ExportToPdf(text2);
			return text2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002A28 File Offset: 0x00000C28
		public static string ObterArquivoPdfMobilePng(List<IBoletoProxy> boletosProxy)
		{
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 77))
			{
				throw new Exception("Boletos do 'Banco Intermedium S.A.' devem ser gerados pelo portal do banco.");
			}
			if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.CodigoBanco == 707))
			{
				if (boletosProxy.Any<IBoletoProxy>((IBoletoProxy x) => x.ContaBoleto.TipoDistribuicao.Equals(1)))
				{
					throw new Exception("Boletos do 'Daycoval' devem ser gerados pelo portal do banco.");
				}
			}
			List<Boleto> list = new List<Boleto>();
			foreach (IBoletoProxy boletoProxy in boletosProxy)
			{
				BoletoService.Criar(boletoProxy);
				list.Add(BoletoService._boletoFacade.Criar().Boleto);
			}
			XtraReport xtraReport = new Boleto79Mm80Mm(list, 0, 0).Processar();
			string text = string.Format("Boleto_{0}.png", Guid.NewGuid());
			string text2 = Path.Combine(Path.GetTempPath(), text);
			xtraReport.ExportToImage(text2, new ImageExportOptions
			{
				Resolution = 600,
				Format = ImageFormat.Png
			});
			return text2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002B6C File Offset: 0x00000D6C
		public static string GerarArquivoRemessa(IEnumerable<IBoletoProxy> boletosProxy, string diretorioArquivo, TipoArquivo tipoArquivo, int sequenciaRemessaDiaria, int numeroArquivoRemessa)
		{
			List<IBoletoFacade> list = new List<IBoletoFacade>();
			foreach (IBoletoProxy boletoProxy in boletosProxy)
			{
				BoletoService.Criar(boletoProxy);
				list.Add(BoletoService._boletoFacade.Criar());
			}
			return new ArquivoRemessaFacade().GerarArquivoRemessa(list, diretorioArquivo, tipoArquivo, sequenciaRemessaDiaria, numeroArquivoRemessa);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static void Validar(IBoletoProxy boletoProxy)
		{
			BoletoService.Criar(boletoProxy);
		}

		// Token: 0x04000001 RID: 1
		private const string TituloPdf = "Desenvolvido por DigiSat Tecnologia - www.digisat.com.br";

		// Token: 0x04000002 RID: 2
		private static BoletoFacadeFactory _boletoFacade;
	}
}

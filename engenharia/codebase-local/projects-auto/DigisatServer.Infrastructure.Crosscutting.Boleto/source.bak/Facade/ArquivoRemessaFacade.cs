using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;
using DigisatServer.Infrastructure.Crosscutting.Facade.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Facade
{
	// Token: 0x0200000D RID: 13
	public class ArquivoRemessaFacade : IArquivoRemessaFacade
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000043B0 File Offset: 0x000025B0
		public string GerarArquivoRemessa(IList<IBoletoFacade> boletosFacade, string diretorioArquivoRemessa, TipoArquivo layoutArquivo, int sequenciaRemessaDiaria, int numeroSequencialArquivo)
		{
			ArquivoRemessaFacade._arquivo = new ArquivoRemessa(layoutArquivo);
			IBoletoFacade boletoFacade2 = boletosFacade.FirstOrDefault<IBoletoFacade>();
			if (boletoFacade2 == null)
			{
				return string.Empty;
			}
			Boletos boletos = new Boletos();
			((BoletoFacade)boletoFacade2).Boleto.Banco.SequenciaRemessaDiaria = sequenciaRemessaDiaria;
			((BoletoFacade)boletoFacade2).Boleto.Banco.NumeroSequencialArquivo = numeroSequencialArquivo;
			boletos.AddRange(boletosFacade.Select<IBoletoFacade, Boleto>((IBoletoFacade boletoFacade) => ((BoletoFacade)boletoFacade).Boleto));
			if (File.Exists(Path.Combine(diretorioArquivoRemessa, ((BoletoFacade)boletoFacade2).Boleto.Banco.NomeArquivoRemessa)))
			{
				File.Move(Path.Combine(diretorioArquivoRemessa, ((BoletoFacade)boletoFacade2).Boleto.Banco.NomeArquivoRemessa), Path.Combine(diretorioArquivoRemessa, string.Format("Arquivo_antigo_renomeado_em_{0}_{1}", DateTime.Now.Year, ((BoletoFacade)boletoFacade2).Boleto.Banco.NomeArquivoRemessa)));
			}
			ArquivoRemessaFacade._arquivo.GerarArquivoRemessa(((BoletoFacade)boletoFacade2).Boleto.Cedente.Convenio, ((BoletoFacade)boletoFacade2).Boleto.Banco, ((BoletoFacade)boletoFacade2).Boleto.Cedente, boletos, diretorioArquivoRemessa, numeroSequencialArquivo);
			return Path.Combine(diretorioArquivoRemessa, ((BoletoFacade)boletoFacade2).Boleto.Banco.NomeArquivoRemessa);
		}

		// Token: 0x0400001F RID: 31
		private static ArquivoRemessa _arquivo;
	}
}

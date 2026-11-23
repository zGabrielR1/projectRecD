using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200017E RID: 382
	public class ArquivoRetornoCNAB400 : AbstractArquivoRetorno, IArquivoRetorno
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0006F1B8 File Offset: 0x0006D3B8
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0006F1C0 File Offset: 0x0006D3C0
		public List<DetalheRetornoCNAB400> ListaDetalhes { get; set; } = new List<DetalheRetornoCNAB400>();

		// Token: 0x06000876 RID: 2166 RVA: 0x0006F1C9 File Offset: 0x0006D3C9
		public ArquivoRetornoCNAB400()
		{
			this.TipoArquivo = TipoArquivo.CNAB400;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0006F1E4 File Offset: 0x0006D3E4
		public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
		{
			try
			{
				StreamReader streamReader = new StreamReader(arquivo, Encoding.UTF8);
				streamReader.ReadLine();
				string text = streamReader.ReadLine();
				while (DetalheRetornoCNAB400.PrimeiroCaracter(text) == "1" || DetalheRetornoCNAB400.PrimeiroCaracter(text) == "7")
				{
					DetalheRetornoCNAB400 detalheRetornoCNAB = banco.LerDetalheRetornoCNAB400(text);
					this.ListaDetalhes.Add(detalheRetornoCNAB);
					this.OnLinhaLida(detalheRetornoCNAB, text);
					text = streamReader.ReadLine();
				}
				streamReader.Close();
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao ler arquivo.", ex);
			}
		}
	}
}

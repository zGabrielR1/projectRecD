using System;
using System.IO;
using System.Text;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200017B RID: 379
	internal class ArquivoRemessaCNAB400 : AbstractArquivoRemessa
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0006ED64 File Offset: 0x0006CF64
		public ArquivoRemessaCNAB400()
		{
			this.TipoArquivo = TipoArquivo.CNAB400;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0006ED74 File Offset: 0x0006CF74
		public override void GerarArquivoRemessa(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, string diretorioArquivoRemessa, int numeroArquivoRemessa)
		{
			try
			{
				int num = 2;
				decimal num2 = 0m;
				StreamWriter streamWriter = new StreamWriter(Path.Combine(diretorioArquivoRemessa, banco.NomeArquivoRemessa), true, Encoding.GetEncoding("iso-8859-1"));
				string text = banco.GerarHeaderRemessa(numeroConvenio, cedente, TipoArquivo.CNAB400, numeroArquivoRemessa);
				streamWriter.WriteLine(text);
				foreach (Boleto boleto in boletos)
				{
					boleto.Banco = banco;
					text = boleto.Banco.GerarDetalheRemessa(boleto, num, TipoArquivo.CNAB400);
					streamWriter.WriteLine(text);
					num2 += boleto.ValorBoleto;
					num++;
					if (boleto.ValorMulta > 0m)
					{
						text = boleto.Banco.GerarDetalheDadosMultaRemessa(boleto, num, TipoArquivo.CNAB400);
						if (!string.IsNullOrEmpty(text))
						{
							streamWriter.WriteLine(text);
							num++;
						}
					}
					text = boleto.Banco.GerarDetalheDadosPagadorAvalistaRemessa(boleto, num, TipoArquivo.CNAB400);
					if (!string.IsNullOrEmpty(text))
					{
						streamWriter.WriteLine(text);
						num++;
					}
				}
				text = banco.GerarTrailerRemessa(num, TipoArquivo.CNAB400, cedente, num2);
				streamWriter.WriteLine(text);
				streamWriter.Close();
				streamWriter.Dispose();
			}
			catch (FileNotFoundException ex)
			{
				throw new Exception(string.Format("Caminho indicado para geração do arquivo de remessa {0}, não encontrado.", diretorioArquivoRemessa), ex);
			}
			catch (DirectoryNotFoundException ex2)
			{
				throw new Exception(string.Format("Caminho indicado para geração do arquivo de remessa {0}, não encontrado.", diretorioArquivoRemessa), ex2);
			}
			catch (DriveNotFoundException ex3)
			{
				throw new Exception(string.Format("Caminho indicado para geração do arquivo de remessa {0}, não encontrado.", diretorioArquivoRemessa), ex3);
			}
			catch (Exception ex4)
			{
				throw new Exception("Erro ao gerar arquivo remessa." + Environment.NewLine + ex4.Message);
			}
		}
	}
}

using System;
using System.IO;
using System.Text;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200017A RID: 378
	internal class ArquivoRemessaCNAB240 : AbstractArquivoRemessa
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0006E9A9 File Offset: 0x0006CBA9
		public ArquivoRemessaCNAB240()
		{
			this.TipoArquivo = TipoArquivo.CNAB240;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0006E9B8 File Offset: 0x0006CBB8
		public override void GerarArquivoRemessa(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, string diretorioArquivoRemessa, int numeroArquivoRemessa)
		{
			try
			{
				int num = 3;
				int num2 = 1;
				int num3 = 0;
				StreamWriter streamWriter = new StreamWriter(Path.Combine(diretorioArquivoRemessa, banco.NomeArquivoRemessa), true, Encoding.GetEncoding("iso-8859-1"));
				string text = banco.GerarHeaderRemessa(numeroConvenio, cedente, TipoArquivo.CNAB240, numeroArquivoRemessa);
				streamWriter.WriteLine(text);
				this.OnLinhaGerada(null, text, EnumTipodeLinha.HeaderDeArquivo);
				text = banco.GerarHeaderLoteRemessa(numeroConvenio, cedente, numeroArquivoRemessa, TipoArquivo.CNAB240);
				streamWriter.WriteLine(text);
				this.OnLinhaGerada(null, text, EnumTipodeLinha.HeaderDeLote);
				if (banco.Codigo != 341)
				{
					decimal num4 = 0m;
					foreach (Boleto boleto in boletos)
					{
						boleto.Banco = banco;
						num4 += boleto.ValorBoleto;
						text = boleto.Banco.GerarDetalheSegmentoPRemessa(boleto, num2, numeroConvenio);
						streamWriter.WriteLine(text);
						this.OnLinhaGerada(boleto, text, EnumTipodeLinha.DetalheSegmentoP);
						num++;
						if (banco.Codigo != 47)
						{
							num2++;
						}
						num3++;
						text = boleto.Banco.GerarDetalheSegmentoQRemessa(boleto, num2, TipoArquivo.CNAB240);
						streamWriter.WriteLine(text);
						this.OnLinhaGerada(boleto, text, EnumTipodeLinha.DetalheSegmentoQ);
						num++;
						if (banco.Codigo != 47)
						{
							num2++;
						}
						num3++;
						if (boleto.ValorMulta > 0m)
						{
							text = boleto.Banco.GerarDetalheSegmentoRRemessa(boleto, num2, TipoArquivo.CNAB240);
							if (!string.IsNullOrEmpty(text))
							{
								streamWriter.WriteLine(text);
								this.OnLinhaGerada(boleto, text, EnumTipodeLinha.DetalheSegmentoR);
								num++;
								if (banco.Codigo != 47)
								{
									num2++;
								}
								num3++;
							}
						}
						if (banco.Codigo == 47)
						{
							num2++;
						}
					}
					num--;
					text = banco.GerarTrailerLoteRemessa(num, num3, boletos.Count, num4);
					streamWriter.WriteLine(text);
					this.OnLinhaGerada(null, text, EnumTipodeLinha.TraillerDeLote);
					num++;
					num++;
					text = banco.GerarTrailerArquivoRemessa(num);
					streamWriter.WriteLine(text);
					this.OnLinhaGerada(null, text, EnumTipodeLinha.TraillerDeArquivo);
					streamWriter.Close();
				}
				else
				{
					foreach (Boleto boleto2 in boletos)
					{
						boleto2.Banco = banco;
						text = boleto2.Banco.GerarDetalheRemessa(boleto2, num2, TipoArquivo.CNAB240);
						streamWriter.WriteLine(text);
						this.OnLinhaGerada(boleto2, text, EnumTipodeLinha.DetalheSegmentoP);
						num++;
						num2++;
					}
					num--;
					text = banco.GerarTrailerLoteRemessa(num, 0, 0, 0m);
					streamWriter.WriteLine(text);
					this.OnLinhaGerada(null, text, EnumTipodeLinha.TraillerDeLote);
					num++;
					num++;
					text = banco.GerarTrailerArquivoRemessa(num);
					streamWriter.WriteLine(text);
					this.OnLinhaGerada(null, text, EnumTipodeLinha.TraillerDeArquivo);
					streamWriter.Close();
				}
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

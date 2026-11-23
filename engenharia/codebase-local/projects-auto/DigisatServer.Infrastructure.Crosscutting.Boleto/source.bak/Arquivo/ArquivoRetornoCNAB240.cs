using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Enums;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x0200017D RID: 381
	public class ArquivoRetornoCNAB240 : AbstractArquivoRetorno, IArquivoRetorno
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0006EF81 File Offset: 0x0006D181
		public Stream StreamArquivo { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0006EF89 File Offset: 0x0006D189
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0006EF91 File Offset: 0x0006D191
		public List<DetalheRetornoCNAB240> ListaDetalhes { get; set; } = new List<DetalheRetornoCNAB240>();

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0006EF9A File Offset: 0x0006D19A
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0006EFA2 File Offset: 0x0006D1A2
		public TipoSolucao Solucao { get; set; }

		// Token: 0x0600086F RID: 2159 RVA: 0x0006EFAB File Offset: 0x0006D1AB
		public ArquivoRetornoCNAB240()
		{
			this.TipoArquivo = TipoArquivo.CNAB240;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0006EFC5 File Offset: 0x0006D1C5
		public ArquivoRetornoCNAB240(Stream streamArquivo)
		{
			this.TipoArquivo = TipoArquivo.CNAB240;
			this.StreamArquivo = streamArquivo;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0006EFE6 File Offset: 0x0006D1E6
		public ArquivoRetornoCNAB240(string caminhoArquivo)
		{
			this.TipoArquivo = TipoArquivo.CNAB240;
			this.StreamArquivo = new StreamReader(caminhoArquivo).BaseStream;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0006F011 File Offset: 0x0006D211
		public void LerArquivoRetorno(IBanco banco)
		{
			this.LerArquivoRetorno(banco, this.StreamArquivo);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0006F020 File Offset: 0x0006D220
		public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
		{
			try
			{
				StreamReader streamReader = new StreamReader(arquivo, Encoding.UTF8);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					DetalheRetornoCNAB240 detalheRetornoCNAB = new DetalheRetornoCNAB240();
					string text2 = text.Substring(7, 1);
					if (!(text2 == "0"))
					{
						if (!(text2 == "1"))
						{
							if (!(text2 == "3"))
							{
								if (!(text2 == "5"))
								{
									if (text2 == "9")
									{
										this.OnLinhaLida(null, text, EnumTipodeLinhaLida.TraillerDeArquivo);
									}
								}
								else
								{
									this.OnLinhaLida(null, text, EnumTipodeLinhaLida.TraillerDeLote);
								}
							}
							else
							{
								text2 = text.Substring(13, 1);
								if (!(text2 == "W"))
								{
									if (!(text2 == "E"))
									{
										if (text2 == "T")
										{
											this.OnLinhaLida(detalheRetornoCNAB, text, EnumTipodeLinhaLida.DetalheSegmentoT);
											detalheRetornoCNAB.SegmentoT = banco.LerDetalheSegmentoTRetornoCNAB240(text);
											text = streamReader.ReadLine();
											this.OnLinhaLida(detalheRetornoCNAB, text, EnumTipodeLinhaLida.DetalheSegmentoU);
											detalheRetornoCNAB.SegmentoU = banco.LerDetalheSegmentoURetornoCNAB240(text);
										}
									}
									else
									{
										this.OnLinhaLida(detalheRetornoCNAB, text, EnumTipodeLinhaLida.DetalheSegmentoE);
										detalheRetornoCNAB.SegmentoE = new DetalheSegmentoERetornoCNAB240();
										detalheRetornoCNAB.SegmentoE.LerDetalheSegmentoERetornoCNAB240(text);
									}
								}
								else
								{
									this.OnLinhaLida(detalheRetornoCNAB, text, EnumTipodeLinhaLida.DetalheSegmentoW);
									detalheRetornoCNAB.SegmentoW.LerDetalheSegmentoWRetornoCNAB240(text);
								}
								this.ListaDetalhes.Add(detalheRetornoCNAB);
							}
						}
						else
						{
							this.OnLinhaLida(null, text, EnumTipodeLinhaLida.HeaderDeLote);
						}
					}
					else
					{
						this.OnLinhaLida(null, text, EnumTipodeLinhaLida.HeaderDeArquivo);
					}
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

using System;
using System.IO;
using System.Linq;
using DigisatServer.Infrastructure.Crosscutting.Service.ApiDePagar.Classes.Boleto;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto
{
	// Token: 0x02000002 RID: 2
	public static class BoletoDePagarService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string GerarArquivoLocal(ArquivoBoletoDto arquivo)
		{
			if (string.IsNullOrWhiteSpace((arquivo != null) ? arquivo.Base64 : null))
			{
				throw new Exception("Arquivo de boleto não foi retornado.");
			}
			byte[] array = Convert.FromBase64String(arquivo.Base64);
			string text = (string.IsNullOrWhiteSpace(arquivo.NomeArquivo) ? "Boleto.pdf" : arquivo.NomeArquivo);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
			string extension = Path.GetExtension(text);
			BoletoDePagarService.ExcluirArquivosExpirados(fileNameWithoutExtension, extension);
			string text2 = Path.Combine(Path.GetTempPath(), string.Format("{0}_{1}{2}", fileNameWithoutExtension, Guid.NewGuid(), extension));
			File.WriteAllBytes(text2, array);
			return text2;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DC File Offset: 0x000002DC
		private static void ExcluirArquivosExpirados(string prefixo, string extensao)
		{
			try
			{
				DateTime limite = DateTime.Now.AddDays(-1.0);
				foreach (string text in from x in Directory.GetFiles(Path.GetTempPath(), string.Format("{0}_*{1}", prefixo, extensao))
					where File.GetLastWriteTime(x) < limite
					select x)
				{
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}
	}
}

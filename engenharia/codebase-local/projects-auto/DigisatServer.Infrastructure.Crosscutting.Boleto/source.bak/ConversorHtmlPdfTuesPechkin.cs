using System;
using System.Drawing.Printing;
using TuesPechkin;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto
{
	// Token: 0x02000004 RID: 4
	internal static class ConversorHtmlPdfTuesPechkin
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public static byte[] Converter(string html, string titulo)
		{
			HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument
			{
				GlobalSettings = 
				{
					DocumentTitle = titulo,
					PaperSize = PaperKind.A4,
					ProduceOutline = new bool?(false)
				},
				Objects = 
				{
					new ObjectSettings
					{
						HtmlText = html.Replace("\"arial narrow\"", "\"arial narrow\", tahoma"),
						ProduceExternalLinks = new bool?(true),
						LoadSettings = 
						{
							ZoomFactor = new double?(1.34),
							ErrorHandling = new LoadSettings.ContentErrorHandling?(LoadSettings.ContentErrorHandling.Ignore)
						},
						WebSettings = 
						{
							DefaultEncoding = "utf-8",
							EnableIntelligentShrinking = new bool?(true),
							LoadImages = new bool?(true),
							PrintBackground = new bool?(true),
							PrintMediaType = new bool?(false)
						}
					}
				}
			};
			return ConversorHtmlPdfTuesPechkin.Conversor.Convert(htmlToPdfDocument);
		}

		// Token: 0x04000003 RID: 3
		private const double Zoom = 1.34;

		// Token: 0x04000004 RID: 4
		private static readonly IConverter Conversor = new ThreadSafeConverter(new PdfToolset(new WinAnyCPUEmbeddedDeployment(new TempFolderDeployment())));
	}
}

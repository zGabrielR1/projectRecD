using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao
{
	// Token: 0x02000154 RID: 340
	internal class ImagemCodigoBarraHandler : IHttpHandler
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0002797D File Offset: 0x00025B7D
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000279A8 File Offset: 0x00025BA8
		public void ProcessRequest(HttpContext context)
		{
			string text = context.Request.QueryString[0];
			context.Response.Write(text);
			context.Response.Clear();
			context.Response.ContentType = "image/jpeg";
			context.Response.AddHeader("content-disposition", "outline;filename=barcode2of5.jpg");
			Bitmap bitmap = new C2of5i(text, 1, 50, text.Length).ToBitmap();
			bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
			bitmap.Dispose();
			context.Response.Flush();
		}
	}
}

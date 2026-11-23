using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using QRCoder;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto
{
	// Token: 0x02000006 RID: 6
	public static class BoletoPagamentoImagemService
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000030C0 File Offset: 0x000012C0
		public static byte[] GerarQrCodePix(string pixCopiaECola, int pixelPorModulo = 10)
		{
			if (string.IsNullOrWhiteSpace(pixCopiaECola))
			{
				return null;
			}
			byte[] array;
			using (QRCodeGenerator qrcodeGenerator = new QRCodeGenerator())
			{
				using (QRCodeData qrcodeData = qrcodeGenerator.CreateQrCode(pixCopiaECola, QRCodeGenerator.ECCLevel.M, false, false, QRCodeGenerator.EciMode.Default, -1))
				{
					using (QRCode qrcode = new QRCode(qrcodeData))
					{
						using (Bitmap graphic = qrcode.GetGraphic(pixelPorModulo, Color.Black, Color.White, true))
						{
							array = BoletoPagamentoImagemService.ParaPng(graphic);
						}
					}
				}
			}
			return array;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000316C File Offset: 0x0000136C
		public static byte[] GerarCodigoBarras(string codigoBarras, int altura = 60, int larguraBarra = 1, float dpi = 96f)
		{
			if (string.IsNullOrWhiteSpace(codigoBarras) || codigoBarras.Length != 44)
			{
				return null;
			}
			byte[] array;
			using (Bitmap bitmap = new C2of5i(codigoBarras, larguraBarra, altura, codigoBarras.Length).ToBitmap())
			{
				bitmap.SetResolution(dpi, dpi);
				array = BoletoPagamentoImagemService.ParaPng(bitmap);
			}
			return array;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000031D0 File Offset: 0x000013D0
		private static byte[] ParaPng(Bitmap bitmap)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, ImageFormat.Png);
				array = memoryStream.ToArray();
			}
			return array;
		}
	}
}

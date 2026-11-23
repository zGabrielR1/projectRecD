using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Util
{
	// Token: 0x02000007 RID: 7
	public abstract class BarCodeBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003214 File Offset: 0x00001414
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003244 File Offset: 0x00001444
		public string Code
		{
			get
			{
				string text;
				try
				{
					text = this._code;
				}
				catch
				{
					text = "";
				}
				return text;
			}
			set
			{
				try
				{
					this._code = value;
				}
				catch
				{
					this._code = null;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003274 File Offset: 0x00001474
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000032A0 File Offset: 0x000014A0
		public int Width
		{
			get
			{
				int num;
				try
				{
					num = this._thin;
				}
				catch
				{
					num = 1;
				}
				return num;
			}
			set
			{
				try
				{
					this._thin = value;
					this._full = value * 3;
				}
				catch
				{
					this._thin = 1;
					this._full = 3;
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000032E4 File Offset: 0x000014E4
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003314 File Offset: 0x00001514
		public int Height
		{
			get
			{
				int num;
				try
				{
					num = this._height;
				}
				catch
				{
					num = 15;
				}
				return num;
			}
			set
			{
				try
				{
					this._height = value;
				}
				catch
				{
					this._height = 1;
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003344 File Offset: 0x00001544
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003370 File Offset: 0x00001570
		public int Digits
		{
			get
			{
				int num;
				try
				{
					num = this._digits;
				}
				catch
				{
					num = 0;
				}
				return num;
			}
			set
			{
				try
				{
					this._digits = value;
				}
				catch
				{
					this._digits = 0;
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000033A0 File Offset: 0x000015A0
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000033DC File Offset: 0x000015DC
		public string ContentType
		{
			get
			{
				string text;
				try
				{
					text = this._contenttype ?? "image/jpeg";
				}
				catch
				{
					text = "image/jpeg";
				}
				return text;
			}
			set
			{
				try
				{
					this._contenttype = value;
				}
				catch
				{
					this._contenttype = "image/jpeg";
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003410 File Offset: 0x00001610
		protected int Thin
		{
			get
			{
				int num;
				try
				{
					num = this._thin;
				}
				catch
				{
					num = 1;
				}
				return num;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000343C File Offset: 0x0000163C
		protected int Full
		{
			get
			{
				int num;
				try
				{
					num = this._full;
				}
				catch
				{
					num = 3;
				}
				return num;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003468 File Offset: 0x00001668
		protected virtual byte[] ToByte(Bitmap bitmap)
		{
			MemoryStream memoryStream = new MemoryStream();
			ImageCodecInfo encoderInfo = BarCodeBase.GetEncoderInfo(this.ContentType);
			EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, 100L);
			EncoderParameters encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = encoderParameter;
			EncoderParameters encoderParameters2 = encoderParameters;
			bitmap.Save(memoryStream, encoderInfo, encoderParameters2);
			return memoryStream.GetBuffer();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034B4 File Offset: 0x000016B4
		private static ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			for (int i = 0; i < imageEncoders.Length; i++)
			{
				if (imageEncoders[i].MimeType == mimeType)
				{
					return imageEncoders[i];
				}
			}
			return null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034EC File Offset: 0x000016EC
		protected virtual void DrawPattern(ref Graphics g, string pattern)
		{
			for (int i = 0; i < pattern.Length; i++)
			{
				int num = ((pattern[i] == '0') ? this._thin : this._full);
				g.FillRectangle((i % 2 == 0) ? this.BLACK : this.WHITE, this.xPos, this.yPos, num, this._height);
				this.xPos += num;
			}
		}

		// Token: 0x04000007 RID: 7
		private string _code;

		// Token: 0x04000008 RID: 8
		private int _height;

		// Token: 0x04000009 RID: 9
		private int _digits;

		// Token: 0x0400000A RID: 10
		private int _thin;

		// Token: 0x0400000B RID: 11
		private int _full;

		// Token: 0x0400000C RID: 12
		protected int xPos;

		// Token: 0x0400000D RID: 13
		protected int yPos;

		// Token: 0x0400000E RID: 14
		private string _contenttype;

		// Token: 0x0400000F RID: 15
		protected Brush BLACK = Brushes.Black;

		// Token: 0x04000010 RID: 16
		protected Brush WHITE = Brushes.White;
	}
}

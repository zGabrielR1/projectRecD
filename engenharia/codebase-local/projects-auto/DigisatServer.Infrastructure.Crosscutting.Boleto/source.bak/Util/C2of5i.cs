using System;
using System.Drawing;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Util
{
	// Token: 0x02000008 RID: 8
	public class C2of5i : BarCodeBase
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000357D File Offset: 0x0000177D
		public C2of5i()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003592 File Offset: 0x00001792
		public C2of5i(string code, int barWidth, int height)
		{
			base.Code = code;
			base.Height = height;
			base.Width = barWidth;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035BC File Offset: 0x000017BC
		public C2of5i(string code, int barWidth, int height, int digits)
		{
			base.Code = code;
			base.Height = height;
			base.Width = barWidth;
			base.Digits = digits;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000035F0 File Offset: 0x000017F0
		private void FillPatern()
		{
			if (this.cPattern[0] == null)
			{
				this.cPattern[0] = "00110";
				this.cPattern[1] = "10001";
				this.cPattern[2] = "01001";
				this.cPattern[3] = "11000";
				this.cPattern[4] = "00101";
				this.cPattern[5] = "10100";
				this.cPattern[6] = "01100";
				this.cPattern[7] = "00011";
				this.cPattern[8] = "10010";
				this.cPattern[9] = "01010";
				for (int i = 9; i >= 0; i--)
				{
					for (int j = 9; j >= 0; j--)
					{
						int num = i * 10 + j;
						string text = "";
						for (int k = 0; k < 5; k++)
						{
							text = text + this.cPattern[i][k].ToString() + this.cPattern[j][k].ToString();
						}
						this.cPattern[num] = text;
					}
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003704 File Offset: 0x00001904
		public Bitmap ToBitmap()
		{
			this.xPos = 0;
			this.yPos = 0;
			if (base.Digits == 0)
			{
				base.Digits = base.Code.Length;
			}
			if (base.Digits % 2 > 0)
			{
				int digits = base.Digits;
				base.Digits = digits + 1;
			}
			while (base.Code.Length < base.Digits || base.Code.Length % 2 > 0)
			{
				base.Code = "0" + base.Code;
			}
			int num = (2 * base.Full + 3 * base.Thin) * base.Digits + 7 * base.Thin + base.Full;
			this.bitmap = new Bitmap(num, base.Height);
			this.g = Graphics.FromImage(this.bitmap);
			this.DrawPattern(ref this.g, "0000");
			this.FillPatern();
			while (base.Code.Length > 0)
			{
				int num2 = Convert.ToInt32(base.Code.Substring(0, 2));
				if (base.Code.Length > 2)
				{
					base.Code = base.Code.Substring(2, base.Code.Length - 2);
				}
				else
				{
					base.Code = "";
				}
				string text = this.cPattern[num2];
				this.DrawPattern(ref this.g, text);
			}
			this.DrawPattern(ref this.g, "1000");
			return this.bitmap;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000387B File Offset: 0x00001A7B
		public byte[] ToByte()
		{
			return base.ToByte(this.ToBitmap());
		}

		// Token: 0x04000011 RID: 17
		private string[] cPattern = new string[100];

		// Token: 0x04000012 RID: 18
		private const string START = "0000";

		// Token: 0x04000013 RID: 19
		private const string STOP = "1000";

		// Token: 0x04000014 RID: 20
		private Bitmap bitmap;

		// Token: 0x04000015 RID: 21
		private Graphics g;
	}
}

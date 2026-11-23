using System;
using System.Web.UI.Design;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao
{
	// Token: 0x02000153 RID: 339
	internal class BoletoBancarioDesigner : ControlDesigner
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0002797D File Offset: 0x00025B7D
		public override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00027980 File Offset: 0x00025B80
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml("Boleto.Net.BoletoBancario");
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002798D File Offset: 0x00025B8D
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return string.Format("Boleto.Net while creating design time HTML:<br/>{0}", e.Message);
		}
	}
}

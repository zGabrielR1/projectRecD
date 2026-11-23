using System;
using System.Linq;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado
{
	// Token: 0x02000082 RID: 130
	public class InfoSacado
	{
		// Token: 0x0600025C RID: 604 RVA: 0x000083ED File Offset: 0x000065ED
		public InfoSacado(string info)
		{
			this._data = new string[] { info };
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008405 File Offset: 0x00006605
		public InfoSacado(string linha1, string linha2)
		{
			this._data = new string[] { linha1, linha2 };
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008421 File Offset: 0x00006621
		public InfoSacado(string[] linhas)
		{
			this._data = linhas;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00008430 File Offset: 0x00006630
		public string Html
		{
			get
			{
				return this._data.Aggregate("", (string current, string s) => current + "<br />" + s);
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008461 File Offset: 0x00006661
		public static string Render(string linha1, string linha2, bool novaLinha)
		{
			return InfoSacado.Render(new string[] { linha1, linha2 }, novaLinha);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008478 File Offset: 0x00006678
		public static string Render(string[] linhas, bool novaLinha)
		{
			string text = linhas.Aggregate("", (string current, string s) => current + "<br />" + s);
			if (!novaLinha)
			{
				text = text.Substring(6);
			}
			return text;
		}

		// Token: 0x040001EF RID: 495
		private readonly string[] _data;
	}
}

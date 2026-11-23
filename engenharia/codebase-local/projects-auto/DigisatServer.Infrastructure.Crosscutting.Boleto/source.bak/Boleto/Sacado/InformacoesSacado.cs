using System;
using System.Collections.Generic;
using System.Linq;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado
{
	// Token: 0x02000081 RID: 129
	public class InformacoesSacado : List<InfoSacado>
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00008394 File Offset: 0x00006594
		public string GeraHTML(bool novaLinha)
		{
			string text = "";
			if (base.Count <= 0)
			{
				return text;
			}
			text = this.Aggregate(text, (string current, InfoSacado I) => current + I.Html);
			if (!novaLinha)
			{
				text = text.Substring(6);
			}
			return text;
		}
	}
}

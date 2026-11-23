using System;
using System.Globalization;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Util
{
	// Token: 0x0200000B RID: 11
	internal class LeitorLinhaPosicao
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003984 File Offset: 0x00001B84
		public static string ExtrairDaPosicao(string linha, int de, int ate)
		{
			int num = de - 1;
			return linha.Substring(num, ate - num);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000399F File Offset: 0x00001B9F
		public static int ExtrairInt32DaPosicao(string linha, int de, int ate)
		{
			return int.Parse(LeitorLinhaPosicao.ExtrairDaPosicao(linha, de, ate));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000039AE File Offset: 0x00001BAE
		public static long ExtrairInt64DaPosicao(string linha, int de, int ate)
		{
			return long.Parse(LeitorLinhaPosicao.ExtrairDaPosicao(linha, de, ate));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000039BD File Offset: 0x00001BBD
		public static DateTime ExtrairDataDaPosicao(string linha, int de, int ate)
		{
			return DateTime.ParseExact(LeitorLinhaPosicao.ExtrairDaPosicao(linha, de, ate), "ddMMyyyy", null);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000039D4 File Offset: 0x00001BD4
		public static int? ExtrairInt32OpcionalDaPosicao(string linha, int de, int ate)
		{
			int num;
			if (int.TryParse(LeitorLinhaPosicao.ExtrairDaPosicao(linha, de, ate), out num))
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003A04 File Offset: 0x00001C04
		public static DateTime? ExtrairDataOpcionalDaPosicao(string linha, int de, int ate)
		{
			DateTime dateTime;
			if (DateTime.TryParseExact(LeitorLinhaPosicao.ExtrairDaPosicao(linha, de, ate), "ddMMyyyy", null, DateTimeStyles.None, out dateTime))
			{
				return new DateTime?(dateTime);
			}
			return null;
		}
	}
}

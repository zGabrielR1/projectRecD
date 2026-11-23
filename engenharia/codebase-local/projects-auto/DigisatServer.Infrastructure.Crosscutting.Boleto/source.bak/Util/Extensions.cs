using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Util
{
	// Token: 0x0200000A RID: 10
	public static class Extensions
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000388C File Offset: 0x00001A8C
		public static string Modulo11(this string str, int @base)
		{
			IEnumerable<int> enumerable = Enumerable.Repeat<IEnumerable<int>>(Enumerable.Range(2, @base - 1), 10).SelectMany<IEnumerable<int>, int>((IEnumerable<int> x) => x).Take<int>(str.Length);
			double num = 11.0 - str.Reverse<char>().Zip<char, int, double>(enumerable, (char x, int a) => char.GetNumericValue(x) * (double)a).Sum() % 11.0;
			if (num <= 9.0 && num > 1.0)
			{
				return num.ToString().Substring(0, 1);
			}
			return "1";
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000394A File Offset: 0x00001B4A
		public static T GetFirstAttribute<T>(this Type type)
		{
			return (T)((object)type.GetCustomAttributes(typeof(T), false).FirstOrDefault<object>());
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003967 File Offset: 0x00001B67
		public static T GetFirstAttribute<T>(this MemberInfo memberInfo)
		{
			return (T)((object)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault<object>());
		}
	}
}

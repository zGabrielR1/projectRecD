using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Util
{
	// Token: 0x0200000C RID: 12
	internal sealed class Utils
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003A3C File Offset: 0x00001C3C
		internal static long DateDiff(DateInterval interval, DateTime startDate, DateTime endDate)
		{
			long num = 0L;
			TimeSpan timeSpan = new TimeSpan(endDate.Ticks - startDate.Ticks);
			switch (interval)
			{
			case DateInterval.Second:
				num = (long)timeSpan.TotalSeconds;
				break;
			case DateInterval.Minute:
				num = (long)timeSpan.TotalMinutes;
				break;
			case DateInterval.Hour:
				num = (long)timeSpan.TotalHours;
				break;
			case DateInterval.Day:
				num = (long)timeSpan.Days;
				break;
			case DateInterval.Week:
				num = (long)(timeSpan.Days / 7);
				break;
			case DateInterval.Month:
				num = (long)(timeSpan.Days / 30);
				break;
			case DateInterval.Quarter:
				num = (long)(timeSpan.Days / 30 / 3);
				break;
			case DateInterval.Year:
				num = (long)(timeSpan.Days / 365);
				break;
			}
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003AF4 File Offset: 0x00001CF4
		internal static string FormatCode(string text, string with, int length, bool left)
		{
			length -= text.Length;
			if (left)
			{
				for (int i = 0; i < length; i++)
				{
					text = with + text;
				}
			}
			else
			{
				for (int j = 0; j < length; j++)
				{
					text += with;
				}
			}
			return text;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003B3B File Offset: 0x00001D3B
		internal static string FormatCode(string text, string with, int length)
		{
			return Utils.FormatCode(text, with, length, false);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B46 File Offset: 0x00001D46
		internal static string FormatCode(string text, int length)
		{
			return Utils.FormatCode(text, "0", length, true);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B58 File Offset: 0x00001D58
		internal static string RemoveAcento(string value)
		{
			IEnumerable<char> enumerable = value.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c2 in enumerable.Where<char>((char c) => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
			{
				stringBuilder.Append(c2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003BD8 File Offset: 0x00001DD8
		internal static string[] GetDados(string linha, string pattern)
		{
			pattern = pattern.Replace('-', ',');
			string[] array = pattern.Split(new char[] { ',' });
			string[] array2 = new string[array.Length / 2];
			int num = 0;
			for (int i = 0; i < array.Length; i += 2)
			{
				array2[num] = linha.Substring(Convert.ToInt32(array[i]) - 1, Convert.ToInt32(array[i + 1]) - Convert.ToInt32(array[i]) + 1);
				num++;
			}
			return array2;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal static bool IsNumber(string value)
		{
			Regex regex = new Regex("[^0-9.-]");
			Regex regex2 = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
			Regex regex3 = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
			Regex regex4 = new Regex("(^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$)|(^([-]|[0-9])[0-9]*$)");
			return !regex.IsMatch(value) && !regex2.IsMatch(value) && !regex3.IsMatch(value) && regex4.IsMatch(value);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003CA8 File Offset: 0x00001EA8
		internal static bool ToBool(object value)
		{
			bool flag;
			try
			{
				flag = Convert.ToBoolean(value);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003CD4 File Offset: 0x00001ED4
		internal static int ToInt32(string value)
		{
			int num;
			try
			{
				num = Convert.ToInt32(value);
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D00 File Offset: 0x00001F00
		internal static long ToInt64(string value)
		{
			long num;
			try
			{
				num = Convert.ToInt64(value);
			}
			catch
			{
				num = 0L;
			}
			return num;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003D30 File Offset: 0x00001F30
		internal static string ToString(object value)
		{
			string text;
			try
			{
				text = Convert.ToString(value).Trim();
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D68 File Offset: 0x00001F68
		internal static DateTime ToDateTime(object value)
		{
			DateTime dateTime;
			try
			{
				dateTime = Convert.ToDateTime(value);
			}
			catch
			{
				dateTime = new DateTime(1, 1, 1);
			}
			return dateTime;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal static string FormataCpfCnpj(string value)
		{
			int length = value.Trim().Length;
			if (length == 11)
			{
				return Utils.FormataCpf(value);
			}
			if (length != 14)
			{
				throw new Exception(string.Format("O CPF ou CNPJ: {0} é inválido.", value));
			}
			return Utils.FormataCnpj(value);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003DE0 File Offset: 0x00001FE0
		internal static string FormataCpf(string cpf)
		{
			string text;
			try
			{
				text = string.Format("{0}.{1}.{2}-{3}", new object[]
				{
					cpf.Substring(0, 3),
					cpf.Substring(3, 3),
					cpf.Substring(6, 3),
					cpf.Substring(9, 2)
				});
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E48 File Offset: 0x00002048
		internal static string FormataCnpj(string cnpj)
		{
			string text;
			try
			{
				text = string.Format("{0}.{1}.{2}/{3}-{4}", new object[]
				{
					cnpj.Substring(0, 2),
					cnpj.Substring(2, 3),
					cnpj.Substring(5, 3),
					cnpj.Substring(8, 4),
					cnpj.Substring(12, 2)
				});
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003EBC File Offset: 0x000020BC
		internal static string FormataCep(string cep)
		{
			string text;
			try
			{
				text = string.Format("{0}{1}-{2}", cep.Substring(0, 2), cep.Substring(2, 3), cep.Substring(5, 3));
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F08 File Offset: 0x00002108
		internal static string FormataAgenciaConta(string agencia, string digitoAgencia, string conta, string digitoConta)
		{
			string text2;
			try
			{
				string text = agencia;
				if (digitoAgencia != string.Empty)
				{
					text = text + "-" + digitoAgencia;
				}
				text = text + "/" + conta;
				if (digitoConta != string.Empty)
				{
					text = text + "-" + digitoConta;
				}
				text2 = text;
			}
			catch
			{
				text2 = string.Empty;
			}
			return text2;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F78 File Offset: 0x00002178
		public static string FitStringLength(string sringToBeFit, int maxLength, int minLength, char fitChar, int maxStartPosition, bool maxTest, bool minTest, bool isNumber)
		{
			string text2;
			try
			{
				string text = "";
				if (maxTest && sringToBeFit.Length > maxLength)
				{
					text += sringToBeFit.Substring(maxStartPosition, maxLength);
				}
				if (!minTest)
				{
					text2 = text;
				}
				else if (sringToBeFit.Length > minLength)
				{
					text2 = text;
				}
				else
				{
					if (isNumber)
					{
						text = text + new string(fitChar, minLength - sringToBeFit.Length) + sringToBeFit;
					}
					else
					{
						text = text + sringToBeFit + new string(fitChar, minLength - sringToBeFit.Length);
					}
					text2 = text;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Problemas ao Formatar a string. String = " + sringToBeFit, ex);
			}
			return text2;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004018 File Offset: 0x00002218
		public static string IdentificaTipoInscricaoSacado(string inscricao)
		{
			string text = string.Empty;
			inscricao = inscricao.Replace(".", "").Replace("-", "").Replace("/", "");
			int length = inscricao.Length;
			if (length != 11)
			{
				if (length == 14)
				{
					text = "02";
				}
			}
			else
			{
				text = "01";
			}
			return text;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004080 File Offset: 0x00002280
		public static string SubstituiCaracteresEspeciais(string strline)
		{
			string text;
			try
			{
				strline = strline.Replace("ã", "a");
				strline = strline.Replace('Ã', 'A');
				strline = strline.Replace('â', 'a');
				strline = strline.Replace('Â', 'A');
				strline = strline.Replace('á', 'a');
				strline = strline.Replace('Á', 'A');
				strline = strline.Replace('à', 'a');
				strline = strline.Replace('À', 'A');
				strline = strline.Replace('ç', 'c');
				strline = strline.Replace('Ç', 'C');
				strline = strline.Replace('é', 'e');
				strline = strline.Replace('É', 'E');
				strline = strline.Replace('ê', 'e');
				strline = strline.Replace('Ê', 'E');
				strline = strline.Replace('õ', 'o');
				strline = strline.Replace('Õ', 'O');
				strline = strline.Replace('ô', 'o');
				strline = strline.Replace('Ô', 'O');
				strline = strline.Replace('ó', 'o');
				strline = strline.Replace('Ó', 'O');
				strline = strline.Replace('ô', 'o');
				strline = strline.Replace('Ô', 'O');
				strline = strline.Replace('ú', 'u');
				strline = strline.Replace('Ú', 'U');
				strline = strline.Replace('ü', 'u');
				strline = strline.Replace('Ü', 'U');
				strline = strline.Replace('í', 'i');
				strline = strline.Replace('Í', 'I');
				strline = strline.Replace('î', 'i');
				strline = strline.Replace('Î', 'I');
				strline = strline.Replace('&', 'E');
				text = strline;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao formatar string.", ex);
			}
			return text;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000428C File Offset: 0x0000248C
		public static byte[] ConvertImageToByte(Image image)
		{
			if (image == null)
			{
				return null;
			}
			string text = image.GetType().ToString();
			if (text == "System.Drawing.Image")
			{
				return (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
			}
			if (!(text == "System.Drawing.Bitmap"))
			{
				throw new NotImplementedException("ConvertImageToByte invalid type " + image.GetType());
			}
			return (byte[])TypeDescriptor.GetConverter(image).ConvertTo(image, typeof(byte[]));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004312 File Offset: 0x00002512
		internal static bool DataValida(DateTime dateTime)
		{
			return !((dateTime.ToString("dd/MM/yyyy") == "01/01/1900") | (dateTime.ToString("dd/MM/yyyy") == "01/01/0001"));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004344 File Offset: 0x00002544
		public static string Right(string seq, int qtde, char ch, bool completaPelaEsquerda)
		{
			return Utils.FitStringLength(Strings.Right(seq, qtde), qtde, qtde, ch, 0, true, true, completaPelaEsquerda);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000435C File Offset: 0x0000255C
		internal static string RetornarDescricao(global::System.Enum valor)
		{
			if (valor == null)
			{
				throw new ArgumentNullException();
			}
			string text = valor.ToString();
			DescriptionAttribute[] array = (DescriptionAttribute[])valor.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length != 0)
			{
				text = array[0].Description;
			}
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000043A9 File Offset: 0x000025A9
		public static string FitStringLength(string sringToBeFit)
		{
			throw new NotImplementedException();
		}
	}
}

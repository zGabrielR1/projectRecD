using System;
using System.Collections.Generic;
using System.Globalization;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Arquivo
{
	// Token: 0x02000183 RID: 387
	public class DetalheSegmentoURetornoCNAB240
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x000707ED File Offset: 0x0006E9ED
		public DetalheSegmentoURetornoCNAB240(string registro)
		{
			this.Registro = registro;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00070807 File Offset: 0x0006EA07
		public DetalheSegmentoURetornoCNAB240()
		{
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0007081A File Offset: 0x0006EA1A
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00070822 File Offset: 0x0006EA22
		public decimal JurosMultaEncargos { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0007082B File Offset: 0x0006EA2B
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00070833 File Offset: 0x0006EA33
		public decimal ValorDescontoConcedido { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0007083C File Offset: 0x0006EA3C
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00070844 File Offset: 0x0006EA44
		public decimal ValorAbatimentoConcedido { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0007084D File Offset: 0x0006EA4D
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00070855 File Offset: 0x0006EA55
		public decimal ValorIOFRecolhido { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0007085E File Offset: 0x0006EA5E
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00070866 File Offset: 0x0006EA66
		public decimal ValorPagoPeloSacado { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0007086F File Offset: 0x0006EA6F
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00070877 File Offset: 0x0006EA77
		public decimal ValorLiquidoASerCreditado { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00070880 File Offset: 0x0006EA80
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00070888 File Offset: 0x0006EA88
		public decimal ValorOutrasDespesas { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00070891 File Offset: 0x0006EA91
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00070899 File Offset: 0x0006EA99
		public decimal ValorOutrosCreditos { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000708A2 File Offset: 0x0006EAA2
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x000708AA File Offset: 0x0006EAAA
		public DateTime DataOcorrencia { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000708B3 File Offset: 0x0006EAB3
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x000708BB File Offset: 0x0006EABB
		public DateTime DataCredito { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000708C4 File Offset: 0x0006EAC4
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000708CC File Offset: 0x0006EACC
		public string CodigoOcorrenciaSacado { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000708D5 File Offset: 0x0006EAD5
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x000708DD File Offset: 0x0006EADD
		public DateTime DataOcorrenciaSacado { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000708E6 File Offset: 0x0006EAE6
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000708EE File Offset: 0x0006EAEE
		public decimal ValorOcorrenciaSacado { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000708F7 File Offset: 0x0006EAF7
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x000708FF File Offset: 0x0006EAFF
		public List<DetalheSegmentoURetornoCNAB240> ListaDetalhe { get; set; } = new List<DetalheSegmentoURetornoCNAB240>();

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00070908 File Offset: 0x0006EB08
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00070910 File Offset: 0x0006EB10
		public string Registro { get; private set; }

		// Token: 0x06000995 RID: 2453 RVA: 0x0007091C File Offset: 0x0006EB1C
		public void LerDetalheSegmentoURetornoCNAB240(string registro)
		{
			try
			{
				this.Registro = registro;
				if (registro.Substring(13, 1) != "U")
				{
					throw new Exception("Registro inválido. O detalhe não possuí as características do segmento U.");
				}
				string text = "";
				if (registro.Substring(153, 4) != "    ")
				{
					text = registro.Substring(157, 8);
				}
				decimal num = Convert.ToInt64(registro.Substring(17, 15));
				this.JurosMultaEncargos = num / 100m;
				decimal num2 = Convert.ToInt64(registro.Substring(32, 15));
				this.ValorDescontoConcedido = num2 / 100m;
				decimal num3 = Convert.ToInt64(registro.Substring(47, 15));
				this.ValorAbatimentoConcedido = num3 / 100m;
				decimal num4 = Convert.ToInt64(registro.Substring(62, 15));
				this.ValorIOFRecolhido = num4 / 100m;
				decimal num5 = Convert.ToInt64(registro.Substring(77, 15));
				this.ValorPagoPeloSacado = num5 / 100m;
				decimal num6 = Convert.ToInt64(registro.Substring(92, 15));
				this.ValorLiquidoASerCreditado = num6 / 100m;
				decimal num7 = Convert.ToInt64(registro.Substring(107, 15));
				this.ValorOutrasDespesas = num7 / 100m;
				decimal num8 = Convert.ToInt64(registro.Substring(122, 15));
				this.ValorOutrosCreditos = num8 / 100m;
				string text2 = registro.Substring(137, 8);
				if (!text2.Equals("00000000"))
				{
					this.DataOcorrencia = DateTime.ParseExact(text2, "ddMMyyyy", CultureInfo.InvariantCulture);
				}
				string text3 = registro.Substring(145, 8).Trim();
				if (!string.IsNullOrEmpty(text3) && Convert.ToInt32(text3) != 0)
				{
					this.DataCredito = DateTime.ParseExact(text3, "ddMMyyyy", CultureInfo.InvariantCulture);
				}
				this.CodigoOcorrenciaSacado = registro.Substring(153, 4).Trim();
				if (!string.IsNullOrEmpty(text) && Convert.ToInt32(text) != 0)
				{
					this.DataOcorrenciaSacado = DateTime.ParseExact(text, "ddMMyyyy", CultureInfo.InvariantCulture);
				}
				decimal num9 = Convert.ToInt64(registro.Substring(165, 15));
				this.ValorOcorrenciaSacado = num9 / 100m;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO U.", ex);
			}
		}
	}
}

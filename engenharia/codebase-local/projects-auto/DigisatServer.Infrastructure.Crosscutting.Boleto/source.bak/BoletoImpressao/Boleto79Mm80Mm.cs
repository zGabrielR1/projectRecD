using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Report.Models;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao
{
	// Token: 0x02000155 RID: 341
	public class Boleto79Mm80Mm : Relatorio
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x00027A3D File Offset: 0x00025C3D
		public Boleto79Mm80Mm(List<global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto> boletosProxy, int margemEsquerda = 0, int margemSuperior = 0)
		{
			this._boletosProxy = boletosProxy;
			this._margemEsquerda = margemEsquerda;
			this._margemSuperior = margemSuperior;
			this.InitializeComponent();
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00027A60 File Offset: 0x00025C60
		public override string Descricao
		{
			get
			{
				return "Boleto 80mm";
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00009044 File Offset: 0x00007244
		public override bool ExibirNoModuloRelatorios
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00027A67 File Offset: 0x00025C67
		public override XtraReport Processar()
		{
			base.DataSource = this._boletosProxy;
			base.ShowPrintMarginsWarning = false;
			base.Margins = new Margins(this._margemEsquerda, 0, this._margemSuperior, 0);
			base.CreateDocument();
			return this;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00027A9C File Offset: 0x00025C9C
		private void logotipoBanco_BeforePrint(object sender, PrintEventArgs e)
		{
			try
			{
				Bitmap bitmap = new Bitmap(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("DigisatServer.Infrastructure.Crosscutting.Boleto.Imagens.{0}.jpg", this.codigoBanco.Text))));
				bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
				this.logotipoBanco.Image = bitmap;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00027AFC File Offset: 0x00025CFC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00027B1C File Offset: 0x00025D1C
		private void InitializeComponent()
		{
			this.components = new Container();
			Code128Generator code128Generator = new Code128Generator();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Boleto79Mm80Mm));
			this.Detail = new DetailBand();
			this.codigoBanco = new XRLabel();
			this.xrLabel19 = new XRLabel();
			this.xrLabel42 = new XRLabel();
			this.xrLabel25 = new XRLabel();
			this.xrLabel50 = new XRLabel();
			this.xrLabel49 = new XRLabel();
			this.xrLabel48 = new XRLabel();
			this.xrLabel47 = new XRLabel();
			this.xrLabel46 = new XRLabel();
			this.xrLabel45 = new XRLabel();
			this.xrBarCode1 = new XRBarCode();
			this.xrLabel44 = new XRLabel();
			this.xrLabel41 = new XRLabel();
			this.xrLabel40 = new XRLabel();
			this.xrLabel39 = new XRLabel();
			this.xrLabel38 = new XRLabel();
			this.xrLabel37 = new XRLabel();
			this.xrLabel36 = new XRLabel();
			this.xrLabel35 = new XRLabel();
			this.xrLabel34 = new XRLabel();
			this.xrLabel33 = new XRLabel();
			this.xrLabel32 = new XRLabel();
			this.xrLabel31 = new XRLabel();
			this.xrLabel30 = new XRLabel();
			this.xrLabel29 = new XRLabel();
			this.xrLabel28 = new XRLabel();
			this.xrLabel27 = new XRLabel();
			this.xrLabel24 = new XRLabel();
			this.xrLabel23 = new XRLabel();
			this.xrLabel21 = new XRLabel();
			this.xrLine1 = new XRLine();
			this.xrLabel18 = new XRLabel();
			this.xrLabel17 = new XRLabel();
			this.xrLabel16 = new XRLabel();
			this.xrLabel15 = new XRLabel();
			this.xrLabel13 = new XRLabel();
			this.xrLabel12 = new XRLabel();
			this.xrLabel11 = new XRLabel();
			this.xrLabel10 = new XRLabel();
			this.xrLabel9 = new XRLabel();
			this.xrLabel8 = new XRLabel();
			this.xrLabel7 = new XRLabel();
			this.xrLabel6 = new XRLabel();
			this.xrLabel3 = new XRLabel();
			this.xrLabel5 = new XRLabel();
			this.xrLabel1 = new XRLabel();
			this.xrLabel4 = new XRLabel();
			this.xrLabel14 = new XRLabel();
			this.xrLabel2 = new XRLabel();
			this.logotipoBanco = new XRPictureBox();
			this.xrLabel20 = new XRLabel();
			this.xrLabel22 = new XRLabel();
			this.xrLabel26 = new XRLabel();
			this.TopMargin = new TopMarginBand();
			this.BottomMargin = new BottomMarginBand();
			this.bindingSource = new BindingSource(this.components);
			((ISupportInitialize)this.bindingSource).BeginInit();
			((ISupportInitialize)this).BeginInit();
			this.Detail.Controls.AddRange(new XRControl[]
			{
				this.codigoBanco, this.xrLabel19, this.xrLabel42, this.xrLabel25, this.xrLabel50, this.xrLabel49, this.xrLabel48, this.xrLabel47, this.xrLabel46, this.xrLabel45,
				this.xrBarCode1, this.xrLabel44, this.xrLabel41, this.xrLabel40, this.xrLabel39, this.xrLabel38, this.xrLabel37, this.xrLabel36, this.xrLabel35, this.xrLabel34,
				this.xrLabel33, this.xrLabel32, this.xrLabel31, this.xrLabel30, this.xrLabel29, this.xrLabel28, this.xrLabel27, this.xrLabel24, this.xrLabel23, this.xrLabel21,
				this.xrLine1, this.xrLabel18, this.xrLabel17, this.xrLabel16, this.xrLabel15, this.xrLabel13, this.xrLabel12, this.xrLabel11, this.xrLabel10, this.xrLabel9,
				this.xrLabel8, this.xrLabel7, this.xrLabel6, this.xrLabel3, this.xrLabel5, this.xrLabel1, this.xrLabel4, this.xrLabel14, this.xrLabel2, this.logotipoBanco,
				this.xrLabel20, this.xrLabel22, this.xrLabel26
			});
			this.Detail.Dpi = 254f;
			this.Detail.Font = new Font("Arial", 6f);
			this.Detail.HeightF = 1715.284f;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			this.Detail.StylePriority.UseFont = false;
			this.Detail.TextAlignment = TextAlignment.TopLeft;
			this.codigoBanco.Angle = 90f;
			this.codigoBanco.Borders = BorderSide.Top | BorderSide.Bottom;
			this.codigoBanco.Dpi = 254f;
			this.codigoBanco.Font = new Font("Arial", 12f, FontStyle.Bold);
			this.codigoBanco.LocationFloat = new PointFloat(1.34574E-05f, 1660.069f);
			this.codigoBanco.Multiline = true;
			this.codigoBanco.Name = "codigoBanco";
			this.codigoBanco.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.codigoBanco.SizeF = new SizeF(71.43745f, 17.22229f);
			this.codigoBanco.StylePriority.UseBorders = false;
			this.codigoBanco.StylePriority.UseFont = false;
			this.codigoBanco.StylePriority.UseTextAlignment = false;
			this.codigoBanco.Text = "[Banco.Codigo!000]";
			this.codigoBanco.TextAlignment = TextAlignment.MiddleRight;
			this.codigoBanco.Visible = false;
			this.xrLabel19.Angle = 90f;
			this.xrLabel19.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CedenteImpresso.Nome")
			});
			this.xrLabel19.Dpi = 254f;
			this.xrLabel19.Font = new Font("Times New Roman", 7f);
			this.xrLabel19.LocationFloat = new PointFloat(711.7709f, 521.3352f);
			this.xrLabel19.Name = "xrLabel19";
			this.xrLabel19.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel19.SizeF = new SizeF(20.75165f, 970.2328f);
			this.xrLabel19.StylePriority.UseFont = false;
			this.xrLabel19.StylePriority.UseTextAlignment = false;
			this.xrLabel19.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel19.WordWrap = false;
			this.xrLabel42.Angle = 90f;
			this.xrLabel42.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "SacadoImpresso.CpfCnpjFormatado")
			});
			this.xrLabel42.Dpi = 254f;
			this.xrLabel42.Font = new Font("Times New Roman", 8f);
			this.xrLabel42.LocationFloat = new PointFloat(623.8541f, 448.4584f);
			this.xrLabel42.Name = "xrLabel42";
			this.xrLabel42.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel42.SizeF = new SizeF(28.00006f, 304.009f);
			this.xrLabel42.StylePriority.UseFont = false;
			this.xrLabel42.StylePriority.UseTextAlignment = false;
			this.xrLabel42.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel42.WordWrap = false;
			this.xrLabel25.Angle = 90f;
			this.xrLabel25.Dpi = 254f;
			this.xrLabel25.Font = new Font("Times New Roman", 7f);
			this.xrLabel25.LocationFloat = new PointFloat(711.7709f, 1491.568f);
			this.xrLabel25.Name = "xrLabel25";
			this.xrLabel25.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel25.SizeF = new SizeF(20.75165f, 175.0571f);
			this.xrLabel25.StylePriority.UseFont = false;
			this.xrLabel25.StylePriority.UseTextAlignment = false;
			this.xrLabel25.Text = "Sacador/Avalista";
			this.xrLabel25.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel50.Angle = 90f;
			this.xrLabel50.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CedenteImpresso.CpfCnpjFormatado")
			});
			this.xrLabel50.Dpi = 254f;
			this.xrLabel50.Font = new Font("Times New Roman", 8f);
			this.xrLabel50.LocationFloat = new PointFloat(155.7283f, 448.4584f);
			this.xrLabel50.Name = "xrLabel50";
			this.xrLabel50.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel50.SizeF = new SizeF(28f, 326.6876f);
			this.xrLabel50.StylePriority.UseFont = false;
			this.xrLabel50.StylePriority.UseTextAlignment = false;
			this.xrLabel50.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel50.WordWrap = false;
			this.xrLabel49.Angle = 90f;
			this.xrLabel49.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "ValorBoleto", "{0:#.00}")
			});
			this.xrLabel49.Dpi = 254f;
			this.xrLabel49.Font = new Font("Times New Roman", 8f);
			this.xrLabel49.LocationFloat = new PointFloat(270.7283f, 34.33333f);
			this.xrLabel49.Name = "xrLabel49";
			this.xrLabel49.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel49.SizeF = new SizeF(28f, 309.5833f);
			this.xrLabel49.StylePriority.UseFont = false;
			this.xrLabel49.StylePriority.UseTextAlignment = false;
			this.xrLabel49.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel49.WordWrap = false;
			this.xrLabel48.Angle = 90f;
			this.xrLabel48.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "NossoNumeroImpressao")
			});
			this.xrLabel48.Dpi = 254f;
			this.xrLabel48.Font = new Font("Times New Roman", 8f);
			this.xrLabel48.LocationFloat = new PointFloat(213.7283f, 34.33333f);
			this.xrLabel48.Name = "xrLabel48";
			this.xrLabel48.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel48.SizeF = new SizeF(28.00002f, 390.6119f);
			this.xrLabel48.StylePriority.UseFont = false;
			this.xrLabel48.StylePriority.UseTextAlignment = false;
			this.xrLabel48.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel48.WordWrap = false;
			this.xrLabel47.Angle = 90f;
			this.xrLabel47.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Banco.FormatoAgenciaCodigoCedente")
			});
			this.xrLabel47.Dpi = 254f;
			this.xrLabel47.Font = new Font("Times New Roman", 8f);
			this.xrLabel47.LocationFloat = new PointFloat(155.7283f, 34.33333f);
			this.xrLabel47.Name = "xrLabel47";
			this.xrLabel47.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel47.SizeF = new SizeF(28f, 390.6119f);
			this.xrLabel47.StylePriority.UseFont = false;
			this.xrLabel47.StylePriority.UseTextAlignment = false;
			this.xrLabel47.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel47.WordWrap = false;
			this.xrLabel46.Angle = 90f;
			this.xrLabel46.Borders = BorderSide.Top | BorderSide.Bottom;
			this.xrLabel46.Dpi = 254f;
			this.xrLabel46.Font = new Font("Times New Roman", 12f, FontStyle.Bold);
			this.xrLabel46.LocationFloat = new PointFloat(0f, 1175f);
			this.xrLabel46.Multiline = true;
			this.xrLabel46.Name = "xrLabel46";
			this.xrLabel46.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel46.SizeF = new SizeF(71.43745f, 153.0417f);
			this.xrLabel46.StylePriority.UseBorders = false;
			this.xrLabel46.StylePriority.UseFont = false;
			this.xrLabel46.StylePriority.UseTextAlignment = false;
			this.xrLabel46.Text = "[Banco.Codigo!000]-[Banco.Digito]";
			this.xrLabel46.TextAlignment = TextAlignment.MiddleRight;
			this.xrLabel45.Angle = 90f;
			this.xrLabel45.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "InstrucoesToText")
			});
			this.xrLabel45.Dpi = 254f;
			this.xrLabel45.Font = new Font("Times New Roman", 8f);
			this.xrLabel45.LocationFloat = new PointFloat(391.4375f, 448.4584f);
			this.xrLabel45.Multiline = true;
			this.xrLabel45.Name = "xrLabel45";
			this.xrLabel45.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel45.SizeF = new SizeF(189.051f, 1218.167f);
			this.xrLabel45.StylePriority.UseFont = false;
			this.xrLabel45.StylePriority.UseTextAlignment = false;
			this.xrLabel45.TextAlignment = TextAlignment.BottomLeft;
			this.xrBarCode1.AutoModule = true;
			this.xrBarCode1.BarCodeOrientation = BarCodeOrientation.RotateLeft;
			this.xrBarCode1.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CodigoBarra.Codigo")
			});
			this.xrBarCode1.Dpi = 254f;
			this.xrBarCode1.Font = new Font("Times New Roman", 6f);
			this.xrBarCode1.LocationFloat = new PointFloat(734.4393f, 521.335f);
			this.xrBarCode1.Module = 5.08f;
			this.xrBarCode1.Name = "xrBarCode1";
			this.xrBarCode1.Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			this.xrBarCode1.ShowText = false;
			this.xrBarCode1.SizeF = new SizeF(65.56079f, 1155.957f);
			this.xrBarCode1.StylePriority.UseFont = false;
			this.xrBarCode1.StylePriority.UsePadding = false;
			this.xrBarCode1.StylePriority.UseTextAlignment = false;
			this.xrBarCode1.Symbology = code128Generator;
			this.xrBarCode1.TextAlignment = TextAlignment.MiddleCenter;
			this.xrLabel44.Angle = 90f;
			this.xrLabel44.Borders = BorderSide.Top | BorderSide.Bottom;
			this.xrLabel44.Dpi = 254f;
			this.xrLabel44.Font = new Font("Times New Roman", 6f);
			this.xrLabel44.LocationFloat = new PointFloat(361.4375f, 437.5831f);
			this.xrLabel44.Multiline = true;
			this.xrLabel44.Name = "xrLabel44";
			this.xrLabel44.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel44.SizeF = new SizeF(232f, 1239.709f);
			this.xrLabel44.StylePriority.UseBorders = false;
			this.xrLabel44.StylePriority.UseFont = false;
			this.xrLabel44.StylePriority.UsePadding = false;
			this.xrLabel44.StylePriority.UseTextAlignment = false;
			this.xrLabel44.Text = "Instruções (Texto de responsabilidade do beneficiário)";
			this.xrLabel44.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel41.Angle = 90f;
			this.xrLabel41.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Especie")
			});
			this.xrLabel41.Dpi = 254f;
			this.xrLabel41.Font = new Font("Times New Roman", 8f);
			this.xrLabel41.LocationFloat = new PointFloat(329.4374f, 1026.085f);
			this.xrLabel41.Name = "xrLabel41";
			this.xrLabel41.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel41.SizeF = new SizeF(28f, 84.56189f);
			this.xrLabel41.StylePriority.UseFont = false;
			this.xrLabel41.StylePriority.UseTextAlignment = false;
			this.xrLabel41.TextAlignment = TextAlignment.MiddleCenter;
			this.xrLabel41.WordWrap = false;
			this.xrLabel40.Angle = 90f;
			this.xrLabel40.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Carteira")
			});
			this.xrLabel40.Dpi = 254f;
			this.xrLabel40.Font = new Font("Times New Roman", 8f);
			this.xrLabel40.LocationFloat = new PointFloat(329.4374f, 1128.491f);
			this.xrLabel40.Name = "xrLabel40";
			this.xrLabel40.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel40.SizeF = new SizeF(28f, 280.3535f);
			this.xrLabel40.StylePriority.UseFont = false;
			this.xrLabel40.StylePriority.UseTextAlignment = false;
			this.xrLabel40.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel40.WordWrap = false;
			this.xrLabel39.Angle = 90f;
			this.xrLabel39.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel39.Dpi = 254f;
			this.xrLabel39.Font = new Font("Times New Roman", 6f);
			this.xrLabel39.LocationFloat = new PointFloat(303.4375f, 437.5831f);
			this.xrLabel39.Multiline = true;
			this.xrLabel39.Name = "xrLabel39";
			this.xrLabel39.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel39.SizeF = new SizeF(58f, 260f);
			this.xrLabel39.StylePriority.UseBorders = false;
			this.xrLabel39.StylePriority.UseFont = false;
			this.xrLabel39.StylePriority.UsePadding = false;
			this.xrLabel39.StylePriority.UseTextAlignment = false;
			this.xrLabel39.Text = "Valor";
			this.xrLabel39.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel38.Angle = 90f;
			this.xrLabel38.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel38.Dpi = 254f;
			this.xrLabel38.Font = new Font("Times New Roman", 6f);
			this.xrLabel38.LocationFloat = new PointFloat(303.4375f, 697.5831f);
			this.xrLabel38.Multiline = true;
			this.xrLabel38.Name = "xrLabel38";
			this.xrLabel38.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel38.SizeF = new SizeF(58f, 315.8564f);
			this.xrLabel38.StylePriority.UseBorders = false;
			this.xrLabel38.StylePriority.UseFont = false;
			this.xrLabel38.StylePriority.UsePadding = false;
			this.xrLabel38.StylePriority.UseTextAlignment = false;
			this.xrLabel38.Text = "Quantidade";
			this.xrLabel38.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel37.Angle = 90f;
			this.xrLabel37.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel37.Dpi = 254f;
			this.xrLabel37.Font = new Font("Times New Roman", 6f);
			this.xrLabel37.LocationFloat = new PointFloat(303.4375f, 1013.44f);
			this.xrLabel37.Multiline = true;
			this.xrLabel37.Name = "xrLabel37";
			this.xrLabel37.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel37.SizeF = new SizeF(58f, 104.9916f);
			this.xrLabel37.StylePriority.UseBorders = false;
			this.xrLabel37.StylePriority.UseFont = false;
			this.xrLabel37.StylePriority.UsePadding = false;
			this.xrLabel37.StylePriority.UseTextAlignment = false;
			this.xrLabel37.Text = "Espécie";
			this.xrLabel37.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel36.Angle = 90f;
			this.xrLabel36.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel36.Dpi = 254f;
			this.xrLabel36.Font = new Font("Times New Roman", 6f);
			this.xrLabel36.LocationFloat = new PointFloat(303.4375f, 1118.431f);
			this.xrLabel36.Multiline = true;
			this.xrLabel36.Name = "xrLabel36";
			this.xrLabel36.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel36.SizeF = new SizeF(58f, 298.8608f);
			this.xrLabel36.StylePriority.UseBorders = false;
			this.xrLabel36.StylePriority.UseFont = false;
			this.xrLabel36.StylePriority.UsePadding = false;
			this.xrLabel36.StylePriority.UseTextAlignment = false;
			this.xrLabel36.Text = "Carteira";
			this.xrLabel36.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel35.Angle = 90f;
			this.xrLabel35.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "DataProcessamento", "{0:dd/MM/yyyy}")
			});
			this.xrLabel35.Dpi = 254f;
			this.xrLabel35.Font = new Font("Times New Roman", 8f);
			this.xrLabel35.LocationFloat = new PointFloat(270.7283f, 448.4584f);
			this.xrLabel35.Name = "xrLabel35";
			this.xrLabel35.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel35.SizeF = new SizeF(28f, 238.1004f);
			this.xrLabel35.StylePriority.UseFont = false;
			this.xrLabel35.StylePriority.UseTextAlignment = false;
			this.xrLabel35.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel35.WordWrap = false;
			this.xrLabel34.Angle = 90f;
			this.xrLabel34.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "Aceite")
			});
			this.xrLabel34.Dpi = 254f;
			this.xrLabel34.Font = new Font("Times New Roman", 8f);
			this.xrLabel34.LocationFloat = new PointFloat(270.7283f, 709.3912f);
			this.xrLabel34.Name = "xrLabel34";
			this.xrLabel34.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel34.SizeF = new SizeF(28f, 111.9023f);
			this.xrLabel34.StylePriority.UseFont = false;
			this.xrLabel34.StylePriority.UseTextAlignment = false;
			this.xrLabel34.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel34.WordWrap = false;
			this.xrLabel33.Angle = 90f;
			this.xrLabel33.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "EspecieDocumento.Sigla")
			});
			this.xrLabel33.Dpi = 254f;
			this.xrLabel33.Font = new Font("Times New Roman", 8f);
			this.xrLabel33.LocationFloat = new PointFloat(270.7283f, 844.5739f);
			this.xrLabel33.Name = "xrLabel33";
			this.xrLabel33.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel33.SizeF = new SizeF(28f, 197.1569f);
			this.xrLabel33.StylePriority.UseFont = false;
			this.xrLabel33.StylePriority.UseTextAlignment = false;
			this.xrLabel33.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel33.WordWrap = false;
			this.xrLabel32.Angle = 90f;
			this.xrLabel32.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "NumeroDocumento")
			});
			this.xrLabel32.Dpi = 254f;
			this.xrLabel32.Font = new Font("Times New Roman", 8f);
			this.xrLabel32.LocationFloat = new PointFloat(270.7283f, 1064.697f);
			this.xrLabel32.Name = "xrLabel32";
			this.xrLabel32.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel32.SizeF = new SizeF(28f, 344.1476f);
			this.xrLabel32.StylePriority.UseFont = false;
			this.xrLabel32.StylePriority.UseTextAlignment = false;
			this.xrLabel32.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel32.WordWrap = false;
			this.xrLabel31.Angle = 90f;
			this.xrLabel31.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel31.Dpi = 254f;
			this.xrLabel31.Font = new Font("Times New Roman", 6f);
			this.xrLabel31.LocationFloat = new PointFloat(245.4375f, 437.5831f);
			this.xrLabel31.Multiline = true;
			this.xrLabel31.Name = "xrLabel31";
			this.xrLabel31.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel31.SizeF = new SizeF(58f, 260f);
			this.xrLabel31.StylePriority.UseBorders = false;
			this.xrLabel31.StylePriority.UseFont = false;
			this.xrLabel31.StylePriority.UsePadding = false;
			this.xrLabel31.StylePriority.UseTextAlignment = false;
			this.xrLabel31.Text = "Data processamento";
			this.xrLabel31.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel30.Angle = 90f;
			this.xrLabel30.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel30.Dpi = 254f;
			this.xrLabel30.Font = new Font("Times New Roman", 6f);
			this.xrLabel30.LocationFloat = new PointFloat(244.7283f, 697.5831f);
			this.xrLabel30.Multiline = true;
			this.xrLabel30.Name = "xrLabel30";
			this.xrLabel30.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel30.SizeF = new SizeF(58.00002f, 132.7103f);
			this.xrLabel30.StylePriority.UseBorders = false;
			this.xrLabel30.StylePriority.UseFont = false;
			this.xrLabel30.StylePriority.UsePadding = false;
			this.xrLabel30.StylePriority.UseTextAlignment = false;
			this.xrLabel30.Text = "Aceite";
			this.xrLabel30.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel29.Angle = 90f;
			this.xrLabel29.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel29.Dpi = 254f;
			this.xrLabel29.Font = new Font("Times New Roman", 6f);
			this.xrLabel29.LocationFloat = new PointFloat(244.7283f, 830.2934f);
			this.xrLabel29.Multiline = true;
			this.xrLabel29.Name = "xrLabel29";
			this.xrLabel29.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel29.SizeF = new SizeF(58.00002f, 222.0072f);
			this.xrLabel29.StylePriority.UseBorders = false;
			this.xrLabel29.StylePriority.UseFont = false;
			this.xrLabel29.StylePriority.UsePadding = false;
			this.xrLabel29.StylePriority.UseTextAlignment = false;
			this.xrLabel29.Text = "Espécie";
			this.xrLabel29.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel28.Angle = 90f;
			this.xrLabel28.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel28.Dpi = 254f;
			this.xrLabel28.Font = new Font("Times New Roman", 6f);
			this.xrLabel28.LocationFloat = new PointFloat(244.7283f, 1052.301f);
			this.xrLabel28.Multiline = true;
			this.xrLabel28.Name = "xrLabel28";
			this.xrLabel28.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel28.SizeF = new SizeF(58.00005f, 364.9915f);
			this.xrLabel28.StylePriority.UseBorders = false;
			this.xrLabel28.StylePriority.UseFont = false;
			this.xrLabel28.StylePriority.UsePadding = false;
			this.xrLabel28.StylePriority.UseTextAlignment = false;
			this.xrLabel28.Text = "Nº documento";
			this.xrLabel28.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel27.Angle = 90f;
			this.xrLabel27.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CedenteImpresso.EnderecoToText")
			});
			this.xrLabel27.Dpi = 254f;
			this.xrLabel27.Font = new Font("Times New Roman", 8f);
			this.xrLabel27.LocationFloat = new PointFloat(213.7283f, 448.4584f);
			this.xrLabel27.Name = "xrLabel27";
			this.xrLabel27.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel27.SizeF = new SizeF(28.00002f, 1218.167f);
			this.xrLabel27.StylePriority.UseFont = false;
			this.xrLabel27.StylePriority.UseTextAlignment = false;
			this.xrLabel27.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel27.WordWrap = false;
			this.xrLabel24.Angle = 90f;
			this.xrLabel24.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
			this.xrLabel24.Dpi = 254f;
			this.xrLabel24.Font = new Font("Times New Roman", 6f);
			this.xrLabel24.LocationFloat = new PointFloat(303.4375f, 1417.292f);
			this.xrLabel24.Multiline = true;
			this.xrLabel24.Name = "xrLabel24";
			this.xrLabel24.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel24.SizeF = new SizeF(58f, 260f);
			this.xrLabel24.StylePriority.UseBorders = false;
			this.xrLabel24.StylePriority.UseFont = false;
			this.xrLabel24.StylePriority.UsePadding = false;
			this.xrLabel24.StylePriority.UseTextAlignment = false;
			this.xrLabel24.Text = "Uso do banco";
			this.xrLabel24.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel23.Angle = 90f;
			this.xrLabel23.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "DataDocumento", "{0:dd/MM/yyyy}")
			});
			this.xrLabel23.Dpi = 254f;
			this.xrLabel23.Font = new Font("Times New Roman", 8f);
			this.xrLabel23.LocationFloat = new PointFloat(270.7283f, 1429.781f);
			this.xrLabel23.Name = "xrLabel23";
			this.xrLabel23.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel23.SizeF = new SizeF(28f, 236.8444f);
			this.xrLabel23.StylePriority.UseFont = false;
			this.xrLabel23.StylePriority.UseTextAlignment = false;
			this.xrLabel23.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel23.WordWrap = false;
			this.xrLabel21.Angle = 90f;
			this.xrLabel21.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CedenteImpresso.Nome")
			});
			this.xrLabel21.Dpi = 254f;
			this.xrLabel21.Font = new Font("Times New Roman", 8f);
			this.xrLabel21.LocationFloat = new PointFloat(155.7283f, 785.1754f);
			this.xrLabel21.Name = "xrLabel21";
			this.xrLabel21.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel21.SizeF = new SizeF(28f, 881.4499f);
			this.xrLabel21.StylePriority.UseFont = false;
			this.xrLabel21.StylePriority.UseTextAlignment = false;
			this.xrLabel21.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel21.WordWrap = false;
			this.xrLine1.Dpi = 254f;
			this.xrLine1.LocationFloat = new PointFloat(623.8542f, 437.5833f);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new SizeF(86.91669f, 5f);
			this.xrLabel18.Angle = 90f;
			this.xrLabel18.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "SacadoImpresso.EnderecoToText")
			});
			this.xrLabel18.Dpi = 254f;
			this.xrLabel18.Font = new Font("Times New Roman", 8f);
			this.xrLabel18.LocationFloat = new PointFloat(651.8542f, 448.4585f);
			this.xrLabel18.Name = "xrLabel18";
			this.xrLabel18.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel18.SizeF = new SizeF(56f, 1218.167f);
			this.xrLabel18.StylePriority.UseFont = false;
			this.xrLabel18.StylePriority.UseTextAlignment = false;
			this.xrLabel18.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel17.Angle = 90f;
			this.xrLabel17.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "SacadoImpresso.Nome")
			});
			this.xrLabel17.Dpi = 254f;
			this.xrLabel17.Font = new Font("Times New Roman", 8f);
			this.xrLabel17.LocationFloat = new PointFloat(623.8541f, 763.4418f);
			this.xrLabel17.Name = "xrLabel17";
			this.xrLabel17.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel17.SizeF = new SizeF(28.00006f, 903.1835f);
			this.xrLabel17.StylePriority.UseFont = false;
			this.xrLabel17.StylePriority.UseTextAlignment = false;
			this.xrLabel17.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel17.WordWrap = false;
			this.xrLabel16.Angle = 90f;
			this.xrLabel16.Borders = BorderSide.All;
			this.xrLabel16.Dpi = 254f;
			this.xrLabel16.Font = new Font("Times New Roman", 6f);
			this.xrLabel16.LocationFloat = new PointFloat(593.4375f, 21.00001f);
			this.xrLabel16.Multiline = true;
			this.xrLabel16.Name = "xrLabel16";
			this.xrLabel16.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel16.SizeF = new SizeF(117.3334f, 1656.291f);
			this.xrLabel16.StylePriority.UseBorders = false;
			this.xrLabel16.StylePriority.UseFont = false;
			this.xrLabel16.StylePriority.UsePadding = false;
			this.xrLabel16.StylePriority.UseTextAlignment = false;
			this.xrLabel16.Text = "Pagador";
			this.xrLabel16.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel15.Angle = 90f;
			this.xrLabel15.Dpi = 254f;
			this.xrLabel15.Font = new Font("Times New Roman", 7f);
			this.xrLabel15.LocationFloat = new PointFloat(710.7709f, 25f);
			this.xrLabel15.Name = "xrLabel15";
			this.xrLabel15.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel15.SizeF = new SizeF(23.66833f, 238.1858f);
			this.xrLabel15.StylePriority.UseFont = false;
			this.xrLabel15.StylePriority.UseTextAlignment = false;
			this.xrLabel15.Text = "Ficha de Compensação";
			this.xrLabel15.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel13.Angle = 90f;
			this.xrLabel13.Dpi = 254f;
			this.xrLabel13.Font = new Font("Times New Roman", 7f);
			this.xrLabel13.LocationFloat = new PointFloat(710.7709f, 263.1859f);
			this.xrLabel13.Name = "xrLabel13";
			this.xrLabel13.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel13.SizeF = new SizeF(23.66833f, 244.92f);
			this.xrLabel13.StylePriority.UseFont = false;
			this.xrLabel13.StylePriority.UseTextAlignment = false;
			this.xrLabel13.Text = "Autenticação mecânica";
			this.xrLabel13.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel12.Angle = 90f;
			this.xrLabel12.Borders = BorderSide.Top;
			this.xrLabel12.Dpi = 254f;
			this.xrLabel12.Font = new Font("Times New Roman", 6f);
			this.xrLabel12.LocationFloat = new PointFloat(535.4375f, 21.00001f);
			this.xrLabel12.Multiline = true;
			this.xrLabel12.Name = "xrLabel12";
			this.xrLabel12.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel12.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel12.StylePriority.UseBorders = false;
			this.xrLabel12.StylePriority.UseFont = false;
			this.xrLabel12.StylePriority.UsePadding = false;
			this.xrLabel12.StylePriority.UseTextAlignment = false;
			this.xrLabel12.Text = "(=) Valor cobrado";
			this.xrLabel12.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel11.Angle = 90f;
			this.xrLabel11.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel11.Dpi = 254f;
			this.xrLabel11.Font = new Font("Times New Roman", 6f);
			this.xrLabel11.LocationFloat = new PointFloat(477.4375f, 21.00001f);
			this.xrLabel11.Multiline = true;
			this.xrLabel11.Name = "xrLabel11";
			this.xrLabel11.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel11.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel11.StylePriority.UseBorders = false;
			this.xrLabel11.StylePriority.UseFont = false;
			this.xrLabel11.StylePriority.UsePadding = false;
			this.xrLabel11.StylePriority.UseTextAlignment = false;
			this.xrLabel11.Text = "(+) Outros acréscimos";
			this.xrLabel11.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel10.Angle = 90f;
			this.xrLabel10.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel10.Dpi = 254f;
			this.xrLabel10.Font = new Font("Times New Roman", 6f);
			this.xrLabel10.LocationFloat = new PointFloat(419.4375f, 21.00001f);
			this.xrLabel10.Multiline = true;
			this.xrLabel10.Name = "xrLabel10";
			this.xrLabel10.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel10.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel10.StylePriority.UseBorders = false;
			this.xrLabel10.StylePriority.UseFont = false;
			this.xrLabel10.StylePriority.UsePadding = false;
			this.xrLabel10.StylePriority.UseTextAlignment = false;
			this.xrLabel10.Text = "(+) Mora/Multa/Juros";
			this.xrLabel10.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel9.Angle = 90f;
			this.xrLabel9.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel9.Dpi = 254f;
			this.xrLabel9.Font = new Font("Times New Roman", 6f);
			this.xrLabel9.LocationFloat = new PointFloat(361.4375f, 21.00001f);
			this.xrLabel9.Multiline = true;
			this.xrLabel9.Name = "xrLabel9";
			this.xrLabel9.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel9.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel9.StylePriority.UseBorders = false;
			this.xrLabel9.StylePriority.UseFont = false;
			this.xrLabel9.StylePriority.UsePadding = false;
			this.xrLabel9.StylePriority.UseTextAlignment = false;
			this.xrLabel9.Text = "(-) Outras deduções";
			this.xrLabel9.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel8.Angle = 90f;
			this.xrLabel8.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel8.Dpi = 254f;
			this.xrLabel8.Font = new Font("Times New Roman", 6f);
			this.xrLabel8.LocationFloat = new PointFloat(303.4375f, 21.00001f);
			this.xrLabel8.Multiline = true;
			this.xrLabel8.Name = "xrLabel8";
			this.xrLabel8.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel8.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel8.StylePriority.UseBorders = false;
			this.xrLabel8.StylePriority.UseFont = false;
			this.xrLabel8.StylePriority.UsePadding = false;
			this.xrLabel8.StylePriority.UseTextAlignment = false;
			this.xrLabel8.Text = "(-) Desconto/Abatimento";
			this.xrLabel8.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel7.Angle = 90f;
			this.xrLabel7.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel7.Dpi = 254f;
			this.xrLabel7.Font = new Font("Times New Roman", 6f);
			this.xrLabel7.LocationFloat = new PointFloat(245.4375f, 21.00001f);
			this.xrLabel7.Multiline = true;
			this.xrLabel7.Name = "xrLabel7";
			this.xrLabel7.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel7.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel7.StylePriority.UseBorders = false;
			this.xrLabel7.StylePriority.UseFont = false;
			this.xrLabel7.StylePriority.UsePadding = false;
			this.xrLabel7.StylePriority.UseTextAlignment = false;
			this.xrLabel7.Text = "(=) Valor documento";
			this.xrLabel7.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel6.Angle = 90f;
			this.xrLabel6.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel6.Dpi = 254f;
			this.xrLabel6.Font = new Font("Times New Roman", 6f);
			this.xrLabel6.LocationFloat = new PointFloat(187.4375f, 21.00001f);
			this.xrLabel6.Multiline = true;
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel6.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel6.StylePriority.UseBorders = false;
			this.xrLabel6.StylePriority.UseFont = false;
			this.xrLabel6.StylePriority.UsePadding = false;
			this.xrLabel6.StylePriority.UseTextAlignment = false;
			this.xrLabel6.Text = "Nosso número";
			this.xrLabel6.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel3.Angle = 90f;
			this.xrLabel3.Borders = BorderSide.Top | BorderSide.Right;
			this.xrLabel3.Dpi = 254f;
			this.xrLabel3.Font = new Font("Times New Roman", 6f);
			this.xrLabel3.LocationFloat = new PointFloat(129.4375f, 21.00009f);
			this.xrLabel3.Multiline = true;
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel3.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel3.StylePriority.UseBorders = false;
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UsePadding = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "Agência/Código Beneficiário";
			this.xrLabel3.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel5.Angle = 90f;
			this.xrLabel5.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "DataVencimento", "{0:dd/MM/yyyy}")
			});
			this.xrLabel5.Dpi = 254f;
			this.xrLabel5.Font = new Font("Times New Roman", 8f);
			this.xrLabel5.LocationFloat = new PointFloat(98.49914f, 34.33333f);
			this.xrLabel5.Name = "xrLabel5";
			this.xrLabel5.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel5.SizeF = new SizeF(28f, 309.5833f);
			this.xrLabel5.StylePriority.UseFont = false;
			this.xrLabel5.StylePriority.UseTextAlignment = false;
			this.xrLabel5.TextAlignment = TextAlignment.TopCenter;
			this.xrLabel5.WordWrap = false;
			this.xrLabel1.Angle = 90f;
			this.xrLabel1.Borders = BorderSide.Left | BorderSide.Top | BorderSide.Right;
			this.xrLabel1.Dpi = 254f;
			this.xrLabel1.Font = new Font("Times New Roman", 6f);
			this.xrLabel1.LocationFloat = new PointFloat(71.43746f, 21f);
			this.xrLabel1.Multiline = true;
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel1.SizeF = new SizeF(58f, 416.5833f);
			this.xrLabel1.StylePriority.UseBorders = false;
			this.xrLabel1.StylePriority.UseFont = false;
			this.xrLabel1.StylePriority.UsePadding = false;
			this.xrLabel1.StylePriority.UseTextAlignment = false;
			this.xrLabel1.Text = "Vencimento";
			this.xrLabel1.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel4.Angle = 90f;
			this.xrLabel4.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "LocalPagamento")
			});
			this.xrLabel4.Dpi = 254f;
			this.xrLabel4.Font = new Font("Times New Roman", 8f);
			this.xrLabel4.LocationFloat = new PointFloat(98.49914f, 448.4583f);
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new PaddingInfo(5, 5, 0, 0, 254f);
			this.xrLabel4.SizeF = new SizeF(27.99999f, 1218.167f);
			this.xrLabel4.StylePriority.UseFont = false;
			this.xrLabel4.StylePriority.UseTextAlignment = false;
			this.xrLabel4.TextAlignment = TextAlignment.BottomCenter;
			this.xrLabel4.WordWrap = false;
			this.xrLabel14.Angle = 90f;
			this.xrLabel14.Borders = BorderSide.All;
			this.xrLabel14.Dpi = 254f;
			this.xrLabel14.Font = new Font("Times New Roman", 6f);
			this.xrLabel14.LocationFloat = new PointFloat(71.43746f, 437.5833f);
			this.xrLabel14.Multiline = true;
			this.xrLabel14.Name = "xrLabel14";
			this.xrLabel14.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel14.SizeF = new SizeF(58f, 1239.708f);
			this.xrLabel14.StylePriority.UseBorders = false;
			this.xrLabel14.StylePriority.UseFont = false;
			this.xrLabel14.StylePriority.UsePadding = false;
			this.xrLabel14.StylePriority.UseTextAlignment = false;
			this.xrLabel14.Text = "Local de pagamento";
			this.xrLabel14.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel2.Angle = 90f;
			this.xrLabel2.Borders = BorderSide.None;
			this.xrLabel2.DataBindings.AddRange(new XRBinding[]
			{
				new XRBinding("Text", null, "CodigoBarra.LinhaDigitavel")
			});
			this.xrLabel2.Dpi = 254f;
			this.xrLabel2.Font = new Font("Times New Roman", 12f);
			this.xrLabel2.LocationFloat = new PointFloat(0f, 21.00008f);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new PaddingInfo(5, 5, 5, 0, 254f);
			this.xrLabel2.SizeF = new SizeF(71.43746f, 1154f);
			this.xrLabel2.StylePriority.UseBorders = false;
			this.xrLabel2.StylePriority.UseFont = false;
			this.xrLabel2.StylePriority.UsePadding = false;
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.TextAlignment = TextAlignment.TopRight;
			this.logotipoBanco.Dpi = 254f;
			this.logotipoBanco.Image = (Image)componentResourceManager.GetObject("logotipoBanco.Image");
			this.logotipoBanco.LocationFloat = new PointFloat(0f, 1328.042f);
			this.logotipoBanco.Name = "logotipoBanco";
			this.logotipoBanco.Padding = new PaddingInfo(6, 6, 6, 6, 254f);
			this.logotipoBanco.SizeF = new SizeF(71.43746f, 349.25f);
			this.logotipoBanco.Sizing = ImageSizeMode.StretchImage;
			this.logotipoBanco.StylePriority.UsePadding = false;
			this.logotipoBanco.BeforePrint += this.logotipoBanco_BeforePrint;
			this.xrLabel20.Angle = 90f;
			this.xrLabel20.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
			this.xrLabel20.Dpi = 254f;
			this.xrLabel20.Font = new Font("Times New Roman", 6f);
			this.xrLabel20.LocationFloat = new PointFloat(129.4375f, 437.5831f);
			this.xrLabel20.Multiline = true;
			this.xrLabel20.Name = "xrLabel20";
			this.xrLabel20.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel20.SizeF = new SizeF(58f, 1239.708f);
			this.xrLabel20.StylePriority.UseBorders = false;
			this.xrLabel20.StylePriority.UseFont = false;
			this.xrLabel20.StylePriority.UsePadding = false;
			this.xrLabel20.StylePriority.UseTextAlignment = false;
			this.xrLabel20.Text = "Beneficiário";
			this.xrLabel20.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel22.Angle = 90f;
			this.xrLabel22.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
			this.xrLabel22.Dpi = 254f;
			this.xrLabel22.Font = new Font("Times New Roman", 6f);
			this.xrLabel22.LocationFloat = new PointFloat(244.7283f, 1417.292f);
			this.xrLabel22.Multiline = true;
			this.xrLabel22.Name = "xrLabel22";
			this.xrLabel22.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel22.SizeF = new SizeF(58.00002f, 260f);
			this.xrLabel22.StylePriority.UseBorders = false;
			this.xrLabel22.StylePriority.UseFont = false;
			this.xrLabel22.StylePriority.UsePadding = false;
			this.xrLabel22.StylePriority.UseTextAlignment = false;
			this.xrLabel22.Text = "Data do documento";
			this.xrLabel22.TextAlignment = TextAlignment.BottomLeft;
			this.xrLabel26.Angle = 90f;
			this.xrLabel26.Borders = BorderSide.Top | BorderSide.Right | BorderSide.Bottom;
			this.xrLabel26.Dpi = 254f;
			this.xrLabel26.Font = new Font("Times New Roman", 6f);
			this.xrLabel26.LocationFloat = new PointFloat(187.4375f, 437.5831f);
			this.xrLabel26.Multiline = true;
			this.xrLabel26.Name = "xrLabel26";
			this.xrLabel26.Padding = new PaddingInfo(5, 5, 0, 3, 254f);
			this.xrLabel26.SizeF = new SizeF(58f, 1239.708f);
			this.xrLabel26.StylePriority.UseBorders = false;
			this.xrLabel26.StylePriority.UseFont = false;
			this.xrLabel26.StylePriority.UsePadding = false;
			this.xrLabel26.StylePriority.UseTextAlignment = false;
			this.xrLabel26.Text = "Endereço do Beneficiário";
			this.xrLabel26.TextAlignment = TextAlignment.BottomLeft;
			this.TopMargin.Dpi = 254f;
			this.TopMargin.Font = new Font("Times New Roman", 10f);
			this.TopMargin.HeightF = 79.57108f;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			this.TopMargin.StylePriority.UseFont = false;
			this.TopMargin.TextAlignment = TextAlignment.TopLeft;
			this.BottomMargin.Dpi = 254f;
			this.BottomMargin.HeightF = 50f;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new PaddingInfo(0, 0, 0, 0, 254f);
			this.BottomMargin.TextAlignment = TextAlignment.TopLeft;
			this.bindingSource.DataSource = typeof(Boleto);
			base.Bands.AddRange(new Band[] { this.Detail, this.TopMargin, this.BottomMargin });
			base.DataSource = this.bindingSource;
			this.Dpi = 254f;
			this.Font = new Font("Segoe UI", 6f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margins = new Margins(0, 0, 80, 50);
			base.PageHeight = 2970;
			base.PageWidth = 800;
			base.PaperKind = PaperKind.Custom;
			base.ReportPrintOptions.DetailCountOnEmptyDataSource = 0;
			base.ReportUnit = ReportUnit.TenthsOfAMillimeter;
			base.RollPaper = true;
			base.Version = "15.2";
			base.Watermark.Font = new Font("Times New Roman", 32f, FontStyle.Bold);
			base.Watermark.ForeColor = Color.Black;
			base.Watermark.TextTransparency = 0;
			((ISupportInitialize)this.bindingSource).EndInit();
			((ISupportInitialize)this).EndInit();
		}

		// Token: 0x040005E8 RID: 1512
		private readonly List<global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto> _boletosProxy;

		// Token: 0x040005E9 RID: 1513
		private readonly int _margemEsquerda;

		// Token: 0x040005EA RID: 1514
		private readonly int _margemSuperior;

		// Token: 0x040005EB RID: 1515
		private IContainer components;

		// Token: 0x040005EC RID: 1516
		private DetailBand Detail;

		// Token: 0x040005ED RID: 1517
		private TopMarginBand TopMargin;

		// Token: 0x040005EE RID: 1518
		private BottomMarginBand BottomMargin;

		// Token: 0x040005EF RID: 1519
		private BindingSource bindingSource;

		// Token: 0x040005F0 RID: 1520
		private XRPictureBox logotipoBanco;

		// Token: 0x040005F1 RID: 1521
		private XRLabel xrLabel2;

		// Token: 0x040005F2 RID: 1522
		private XRLabel xrLabel14;

		// Token: 0x040005F3 RID: 1523
		private XRLabel xrLabel4;

		// Token: 0x040005F4 RID: 1524
		private XRLabel xrLabel5;

		// Token: 0x040005F5 RID: 1525
		private XRLabel xrLabel1;

		// Token: 0x040005F6 RID: 1526
		private XRLine xrLine1;

		// Token: 0x040005F7 RID: 1527
		private XRLabel xrLabel18;

		// Token: 0x040005F8 RID: 1528
		private XRLabel xrLabel17;

		// Token: 0x040005F9 RID: 1529
		private XRLabel xrLabel16;

		// Token: 0x040005FA RID: 1530
		private XRLabel xrLabel15;

		// Token: 0x040005FB RID: 1531
		private XRLabel xrLabel13;

		// Token: 0x040005FC RID: 1532
		private XRLabel xrLabel12;

		// Token: 0x040005FD RID: 1533
		private XRLabel xrLabel11;

		// Token: 0x040005FE RID: 1534
		private XRLabel xrLabel10;

		// Token: 0x040005FF RID: 1535
		private XRLabel xrLabel9;

		// Token: 0x04000600 RID: 1536
		private XRLabel xrLabel8;

		// Token: 0x04000601 RID: 1537
		private XRLabel xrLabel7;

		// Token: 0x04000602 RID: 1538
		private XRLabel xrLabel6;

		// Token: 0x04000603 RID: 1539
		private XRLabel xrLabel3;

		// Token: 0x04000604 RID: 1540
		private XRLabel xrLabel45;

		// Token: 0x04000605 RID: 1541
		private XRBarCode xrBarCode1;

		// Token: 0x04000606 RID: 1542
		private XRLabel xrLabel44;

		// Token: 0x04000607 RID: 1543
		private XRLabel xrLabel41;

		// Token: 0x04000608 RID: 1544
		private XRLabel xrLabel40;

		// Token: 0x04000609 RID: 1545
		private XRLabel xrLabel39;

		// Token: 0x0400060A RID: 1546
		private XRLabel xrLabel38;

		// Token: 0x0400060B RID: 1547
		private XRLabel xrLabel37;

		// Token: 0x0400060C RID: 1548
		private XRLabel xrLabel36;

		// Token: 0x0400060D RID: 1549
		private XRLabel xrLabel35;

		// Token: 0x0400060E RID: 1550
		private XRLabel xrLabel34;

		// Token: 0x0400060F RID: 1551
		private XRLabel xrLabel33;

		// Token: 0x04000610 RID: 1552
		private XRLabel xrLabel32;

		// Token: 0x04000611 RID: 1553
		private XRLabel xrLabel31;

		// Token: 0x04000612 RID: 1554
		private XRLabel xrLabel30;

		// Token: 0x04000613 RID: 1555
		private XRLabel xrLabel29;

		// Token: 0x04000614 RID: 1556
		private XRLabel xrLabel28;

		// Token: 0x04000615 RID: 1557
		private XRLabel xrLabel27;

		// Token: 0x04000616 RID: 1558
		private XRLabel xrLabel24;

		// Token: 0x04000617 RID: 1559
		private XRLabel xrLabel23;

		// Token: 0x04000618 RID: 1560
		private XRLabel xrLabel21;

		// Token: 0x04000619 RID: 1561
		private XRLabel xrLabel20;

		// Token: 0x0400061A RID: 1562
		private XRLabel xrLabel22;

		// Token: 0x0400061B RID: 1563
		private XRLabel xrLabel26;

		// Token: 0x0400061C RID: 1564
		private XRLabel xrLabel46;

		// Token: 0x0400061D RID: 1565
		private XRLabel xrLabel49;

		// Token: 0x0400061E RID: 1566
		private XRLabel xrLabel48;

		// Token: 0x0400061F RID: 1567
		private XRLabel xrLabel47;

		// Token: 0x04000620 RID: 1568
		private XRLabel xrLabel50;

		// Token: 0x04000621 RID: 1569
		private XRLabel xrLabel25;

		// Token: 0x04000622 RID: 1570
		private XRLabel xrLabel42;

		// Token: 0x04000623 RID: 1571
		private XRLabel xrLabel19;

		// Token: 0x04000624 RID: 1572
		private XRLabel codigoBanco;
	}
}

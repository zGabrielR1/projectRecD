using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Carteira;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Instrucao;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Sacado;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Util;
using DigisatServer.Infrastructure.Crosscutting.Helper;
using Microsoft.VisualBasic;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao
{
	// Token: 0x02000152 RID: 338
	[Designer(typeof(BoletoBancarioDesigner))]
	[ToolboxBitmap(typeof(BoletoBancario))]
	[ToolboxData("<{0}:BoletoBancario Runat=\"server\"></{0}:BoletoBancario>")]
	[Serializable]
	public class BoletoBancario : Control
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x000266AC File Offset: 0x000248AC
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x000266B4 File Offset: 0x000248B4
		[Browsable(true)]
		[Description("Código do banco em que será gerado o boleto. Ex. 341-Itaú, 237-Bradesco")]
		public short CodigoBanco
		{
			get
			{
				return this._codigoBanco;
			}
			set
			{
				this._codigoBanco = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000266BD File Offset: 0x000248BD
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x000266C5 File Offset: 0x000248C5
		[Browsable(true)]
		[Description("Mostra a descrição da carteira")]
		public bool MostrarCodigoCarteira
		{
			get
			{
				return this._mostrarCodigoCarteira;
			}
			set
			{
				this._mostrarCodigoCarteira = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000266CE File Offset: 0x000248CE
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x000266D6 File Offset: 0x000248D6
		[Browsable(true)]
		[Description("Formata o boleto no layout de carnê")]
		public bool FormatoCarne
		{
			get
			{
				return this._formatoCarne;
			}
			set
			{
				this._formatoCarne = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000266DF File Offset: 0x000248DF
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x000266E8 File Offset: 0x000248E8
		[Browsable(false)]
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto Boleto
		{
			get
			{
				return this._boleto;
			}
			set
			{
				this._boleto = value;
				if (this._ibanco == null)
				{
					this._boleto.Banco = this.Banco;
				}
				this.Cedente = this._boleto.Cedente;
				this.Sacado = this._boleto.Sacado;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00026737 File Offset: 0x00024937
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0002673F File Offset: 0x0002493F
		[Browsable(false)]
		public Sacado Sacado { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00026748 File Offset: 0x00024948
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00026750 File Offset: 0x00024950
		[Browsable(false)]
		public Cedente Cedente { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0002675C File Offset: 0x0002495C
		[Browsable(false)]
		public global::DigisatServer.Infrastructure.Crosscutting.Boleto.Banco.Banco Banco
		{
			get
			{
				if (this._ibanco == null || this._ibanco.Codigo != (int)this._codigoBanco)
				{
					this._ibanco = new Banco((int)this._codigoBanco);
				}
				if (this._boleto != null)
				{
					this._boleto.Banco = this._ibanco;
				}
				return this._ibanco;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000267B4 File Offset: 0x000249B4
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000267CB File Offset: 0x000249CB
		[Browsable(true)]
		[Description("Mostra o comprovante de entrega sem dados para marcar")]
		public bool MostrarComprovanteEntregaLivre
		{
			get
			{
				return Utils.ToBool(this.ViewState["1"]);
			}
			set
			{
				this.ViewState["1"] = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000267E3 File Offset: 0x000249E3
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x000267FA File Offset: 0x000249FA
		[Browsable(true)]
		[Description("Mostra o comprovante de entrega")]
		public bool MostrarComprovanteEntrega
		{
			get
			{
				return Utils.ToBool(this.ViewState["2"]);
			}
			set
			{
				this.ViewState["2"] = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00026812 File Offset: 0x00024A12
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00026829 File Offset: 0x00024A29
		[Browsable(true)]
		[Description("Oculta as intruções do boleto")]
		public bool OcultarEnderecoSacado
		{
			get
			{
				return Utils.ToBool(this.ViewState["3"]);
			}
			set
			{
				this.ViewState["3"] = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00026841 File Offset: 0x00024A41
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00026858 File Offset: 0x00024A58
		[Browsable(true)]
		[Description("Oculta as intruções do boleto")]
		public bool OcultarInstrucoes
		{
			get
			{
				return Utils.ToBool(this.ViewState["4"]);
			}
			set
			{
				this.ViewState["4"] = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00026870 File Offset: 0x00024A70
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00026887 File Offset: 0x00024A87
		[Browsable(true)]
		[Description("Oculta o recibo do sacado do boleto")]
		public bool OcultarReciboSacado
		{
			get
			{
				return Utils.ToBool(this.ViewState["5"]);
			}
			set
			{
				this.ViewState["5"] = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0002689F File Offset: 0x00024A9F
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000268B6 File Offset: 0x00024AB6
		public bool MostrarContraApresentacaoNaDataVencimento
		{
			get
			{
				return Utils.ToBool(this.ViewState["7"]);
			}
			set
			{
				this.ViewState["7"] = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000268CE File Offset: 0x00024ACE
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000268E5 File Offset: 0x00024AE5
		[Browsable(true)]
		[Description("Mostra o endereço do Cedente")]
		public bool MostrarEnderecoCedente
		{
			get
			{
				return Utils.ToBool(this.ViewState["8"]);
			}
			set
			{
				this.ViewState["8"] = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000268FD File Offset: 0x00024AFD
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00026905 File Offset: 0x00024B05
		public List<IInstrucao> Instrucoes
		{
			get
			{
				return this._instrucoes;
			}
			set
			{
				this._instrucoes = value;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00026910 File Offset: 0x00024B10
		protected override void OnPreRender(EventArgs e)
		{
			LiteralControl literalControl = new LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + this.Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao.BoletoNet.css") + "\" />");
			this.Page.Header.Controls.Add(literalControl);
			base.OnPreRender(e);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00009047 File Offset: 0x00007247
		protected override void OnLoad(EventArgs e)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00026970 File Offset: 0x00024B70
		public string GeraHtmlInstrucoes()
		{
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Html.Instrucoes);
				stringBuilder.Append("<br />");
				text = stringBuilder.ToString().Replace("@TITULO", "Instruções de Impressão").Replace("@INSTRUCAO", "Imprimir em impressora jato de tinta (ink jet) ou laser em qualidade normal. (Não use modo econômico).<br>Utilize folha A4 (210 x 297 mm) ou Carta (216 x 279 mm) - Corte na linha indicada<br>");
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação.", ex);
			}
			return text;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000269E0 File Offset: 0x00024BE0
		private string GeraHtmlCarne(string telefone, string htmlBoleto)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Html.Carne);
			return stringBuilder.ToString().Replace("@TELEFONE", telefone).Replace("#BOLETO#", htmlBoleto);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00026A10 File Offset: 0x00024C10
		public string GeraHtmlReciboSacado()
		{
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Html.ReciboSacadoParte1);
				stringBuilder.Append("<br />");
				stringBuilder.Append(Html.ReciboSacadoParte2);
				stringBuilder.Append(Html.ReciboSacadoParte3);
				if (this.MostrarEnderecoCedente)
				{
					stringBuilder.Append(Html.ReciboSacadoParte10);
				}
				stringBuilder.Append(Html.ReciboSacadoParte4);
				stringBuilder.Append(Html.ReciboSacadoParte5);
				stringBuilder.Append(Html.ReciboSacadoParte6);
				stringBuilder.Append(Html.ReciboSacadoParte7);
				if (this.Instrucoes.Count == 0)
				{
					stringBuilder.Append(Html.ReciboSacadoParte8);
				}
				stringBuilder.Replace("Carteira /", "");
				this.MontaInstrucoes();
				text = stringBuilder.ToString().Replace("@INSTRUCOES", this._instrucoesHtml);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação.", ex);
			}
			return text;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00026B00 File Offset: 0x00024D00
		public string GeraHtmlReciboCedente()
		{
			string text;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Html.ReciboCedenteParte1);
				stringBuilder.Append(Html.ReciboCedenteParte2);
				stringBuilder.Append(this.FormatoCarne ? Html.ReciboCedenteParte3Carne : Html.ReciboCedenteParte3);
				stringBuilder.Append(this.FormatoCarne ? Html.ReciboCedenteParte4Carne : Html.ReciboCedenteParte4);
				stringBuilder.Append(Html.ReciboCedenteParte5);
				stringBuilder.Append(Html.ReciboCedenteParte6);
				stringBuilder.Append(Html.ReciboCedenteParte7);
				stringBuilder.Append(Html.ReciboCedenteParte8);
				stringBuilder.Append(Html.ReciboCedenteParte9);
				stringBuilder.Append(this.FormatoCarne ? Html.ReciboCedenteParte10Carne : Html.ReciboCedenteParte10);
				stringBuilder.Append(Html.ReciboCedenteParte11);
				if (this.FormatoCarne)
				{
					stringBuilder.Append(Html.ReciboCedenteParte12Carne);
				}
				else
				{
					stringBuilder.Append(this.MostrarComprovanteEntregaLivre ? Html.ReciboCedenteParte12b : Html.ReciboCedenteParte12);
				}
				if (this.Boleto.Banco.Codigo == 341)
				{
					stringBuilder.Replace("(Texto de responsabilidade do beneficiário)", "(Todas as informações deste bloqueto são de exclusiva responsabilidade do beneficiário)");
				}
				if (this.Boleto.Banco.Codigo == 4)
				{
					stringBuilder.Replace("(Texto de responsabilidade do beneficiário)", "(Todas as Informações deste bloqueto são de EXCLUSIVA responsabilidade do Cedente)");
				}
				stringBuilder.Replace("Carteira /", "");
				this.MontaInstrucoes();
				text = stringBuilder.ToString().Replace("@INSTRUCOES", this._instrucoesHtml);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro na execução da transação.", ex);
			}
			return text;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00026C9C File Offset: 0x00024E9C
		public string HtmlComprovanteEntrega
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(Html.ComprovanteEntrega1);
				stringBuilder.Append(Html.ComprovanteEntrega2);
				stringBuilder.Append(Html.ComprovanteEntrega3);
				stringBuilder.Append(Html.ComprovanteEntrega4);
				stringBuilder.Append(Html.ComprovanteEntrega5);
				stringBuilder.Append(Html.ComprovanteEntrega6);
				stringBuilder.Append("<br />");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00026D08 File Offset: 0x00024F08
		private void MontaInstrucoes()
		{
			if (!string.IsNullOrEmpty(this._instrucoesHtml))
			{
				return;
			}
			if (this.Boleto.Instrucoes.Count <= 0)
			{
				return;
			}
			this._instrucoesHtml = string.Empty;
			foreach (IInstrucao instrucao in this.Boleto.Instrucoes)
			{
				this._instrucoesHtml += string.Format("{0}<br />", instrucao.Descricao);
			}
			this._instrucoesHtml = Strings.Left(this._instrucoesHtml, this._instrucoesHtml.Length - 6);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00026DC8 File Offset: 0x00024FC8
		private string MontaHtml(string urlImagemLogo, string urlImagemBarra, string imagemCodigoBarras)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			if (!this.OcultarInstrucoes)
			{
				stringBuilder.Append(this.GeraHtmlInstrucoes());
			}
			if (this.MostrarEnderecoCedente && string.IsNullOrEmpty(this.Cedente.Beneficiario))
			{
				if (this.Cedente.Endereco == null)
				{
					throw new ArgumentNullException("Endereço do beneficiário não preenchido");
				}
				text = this.Cedente.Endereco.Logradouro + " , ";
				if (this.Cedente.Endereco.Numero != string.Empty)
				{
					text = text + this.Cedente.Endereco.Numero + " - ";
				}
				if (this.Cedente.Endereco.Cep == string.Empty)
				{
					text += string.Format("{0} - {1}/{2}", this.Cedente.Endereco.Bairro, this.Cedente.Endereco.Cidade, this.Cedente.Endereco.UF);
				}
				else
				{
					text += string.Format("{0} - {1}/{2} - CEP: {3}", new object[]
					{
						this.Cedente.Endereco.Bairro,
						this.Cedente.Endereco.Cidade,
						this.Cedente.Endereco.UF,
						Utils.FormataCep(this.Cedente.Endereco.Cep)
					});
				}
			}
			if (!this.FormatoCarne)
			{
				if (this.MostrarComprovanteEntrega | this.MostrarComprovanteEntregaLivre)
				{
					stringBuilder.Append(this.HtmlComprovanteEntrega);
					if (this.OcultarReciboSacado)
					{
						stringBuilder.Append(Html.ReciboSacadoParte8);
					}
				}
				if (!this.OcultarReciboSacado)
				{
					stringBuilder.Append(this.GeraHtmlReciboSacado());
				}
			}
			string text2 = this.Sacado.Nome.CopiarTexto(60);
			string text3;
			if (this.Sacado.CpfCnpj == string.Empty)
			{
				text3 = text2;
			}
			else
			{
				text3 = ((this.Sacado.CpfCnpj.Length <= 11) ? string.Format("{0}  CPF: {1}", text2, Utils.FormataCpf(this.Sacado.CpfCnpj)) : string.Format("{0}  CNPJ: {1}", text2, Utils.FormataCnpj(this.Sacado.CpfCnpj)));
			}
			string text4 = this.Sacado.InformacoesSacado.GeraHTML(false);
			if (!this.OcultarEnderecoSacado)
			{
				string text5 = ((!string.IsNullOrEmpty(this.Sacado.Endereco.Complemento)) ? string.Concat(new string[]
				{
					this.Sacado.Endereco.Logradouro,
					", ",
					this.Sacado.Endereco.Numero,
					" ",
					this.Sacado.Endereco.Complemento
				}) : (this.Sacado.Endereco.Logradouro + ", " + this.Sacado.Endereco.Numero));
				string text6 = string.Empty;
				if (string.IsNullOrEmpty(this.Sacado.Endereco.Cep))
				{
					text6 += string.Format("{0} - {1}/{2}", this.Sacado.Endereco.Bairro, this.Sacado.Endereco.Cidade, this.Sacado.Endereco.UF);
				}
				else
				{
					text6 += string.Format("{0} - {1}/{2} - CEP: {3}", new object[]
					{
						this.Sacado.Endereco.Bairro,
						this.Sacado.Endereco.Cidade,
						this.Sacado.Endereco.UF,
						Utils.FormataCep(this.Sacado.Endereco.Cep)
					});
				}
				text4 += InfoSacado.Render(text5, text6, false);
			}
			stringBuilder.Append((!this.FormatoCarne) ? this.GeraHtmlReciboCedente() : this.GeraHtmlCarne("", this.GeraHtmlReciboCedente()));
			string text7 = this.Boleto.DataVencimento.ToString("dd/MM/yyyy");
			if (this.MostrarContraApresentacaoNaDataVencimento)
			{
				text7 = "Contra Apresentação";
			}
			this.Boleto.Banco.FormataNossoNumeroImpressao(this.Boleto);
			return stringBuilder.Replace("@CODIGOBANCO", Utils.FormatCode(this._ibanco.Codigo.ToString(CultureInfo.InvariantCulture), 3)).Replace("@DIGITOBANCO", this._ibanco.Digito).Replace("@URLIMAGEMLOGO", urlImagemLogo)
				.Replace("@URLIMAGEMBARRA", urlImagemBarra)
				.Replace("@LINHADIGITAVEL", this.Boleto.CodigoBarra.LinhaDigitavel)
				.Replace("@LOCALPAGAMENTO", this.Boleto.LocalPagamento)
				.Replace("@DATAVENCIMENTO", text7)
				.Replace("@CEDENTE", (!string.IsNullOrEmpty(this.Cedente.Beneficiario)) ? (this.FormatoCarne ? this.Cedente.Beneficiario.CopiarTexto(56) : this.Cedente.Beneficiario) : (this.FormatoCarne ? this.Cedente.Nome.CopiarTexto(56) : this.Cedente.Nome))
				.Replace("@DATADOCUMENTO", this.Boleto.DataDocumento.ToString("dd/MM/yyyy"))
				.Replace("@NUMERODOCUMENTO", this.Boleto.NumeroDocumento)
				.Replace("@ESPECIEDOCUMENTO", EspecieDocumento.ValidaSigla(this.Boleto.EspecieDocumento))
				.Replace("@DATAPROCESSAMENTO", this.Boleto.DataProcessamento.ToString("dd/MM/yyyy"))
				.Replace("@DESCRICAOTITULOESPECIE", (this.Boleto.Banco.Codigo == 104) ? "Moeda" : "Espécie")
				.Replace("@NOSSONUMERO", this.Boleto.NossoNumeroImpressao)
				.Replace("@CARTEIRA", this.FormataDescricaoCarteira())
				.Replace("@ESPECIE", this.Boleto.Especie)
				.Replace("@QUANTIDADE", (this.Boleto.QuantidadeMoeda == 0) ? "" : this.Boleto.QuantidadeMoeda.ToString(CultureInfo.InvariantCulture))
				.Replace("@VALORDOCUMENTO", this.Boleto.ValorMoeda)
				.Replace("@=VALORDOCUMENTO", (this.Boleto.ValorBoleto == 0m) ? "" : this.Boleto.ValorBoleto.ToString("R$ ##,##0.00"))
				.Replace("@VALORCOBRADO", (this.Boleto.ValorCobrado == 0m) ? "" : this.Boleto.ValorCobrado.ToString("R$ ##,##0.00"))
				.Replace("@OUTROSACRESCIMOS", "")
				.Replace("@OUTRASDEDUCOES", "")
				.Replace("@DESCONTOS", "")
				.Replace("@AGENCIACONTA", this._ibanco.FormatoAgenciaCodigoCedente)
				.Replace("@SACADO", text3)
				.Replace("@INFOSACADO", text4)
				.Replace("@AGENCIACODIGOCEDENTE", this._ibanco.FormatoAgenciaCodigoCedente)
				.Replace("@CPFCNPJ", (!string.IsNullOrEmpty(this.Cedente.Beneficiario)) ? string.Empty : ((this.Cedente.Cpfcnpj.RemoveCaracteresAlfanumerico().Length == 11) ? ("CPF: " + Utils.FormataCpf(this.Cedente.Cpfcnpj)) : ("CNPJ: " + Utils.FormataCnpj(this.Cedente.Cpfcnpj))))
				.Replace("@MORAMULTA", "")
				.Replace("@AUTENTICACAOMECANICA", "")
				.Replace("@USODOBANCO", this.Boleto.UsoBanco)
				.Replace("@IMAGEMCODIGOBARRA", imagemCodigoBarras)
				.Replace("@ACEITE", this.Boleto.Aceite)
				.ToString()
				.Replace("@ENDERECOCEDENTE", this.MostrarEnderecoCedente ? text : "")
				.Replace("@INSTRUCAOSAC", this._ibanco.InstrucaoSacAtendimento)
				.Replace("@DESCRICAOBENEFICIARIOSACADORAVALISTA", this._ibanco.DescricaoBeneficiarioSacadorAvalista)
				.Replace("@AVALISTA", (!string.IsNullOrEmpty(this.Cedente.Beneficiario)) ? (this.Cedente.Nome + " CNPJ: " + Utils.FormataCnpj(this.Cedente.Cpfcnpj)) : "");
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00027678 File Offset: 0x00025878
		private string FormataDescricaoCarteira()
		{
			int num = Utils.ToInt32(this.Boleto.Carteira);
			int codigo = this.Banco.Codigo;
			string text;
			if (codigo != 4)
			{
				if (codigo != 104)
				{
					if (codigo != 399)
					{
						text = this.Boleto.Carteira;
					}
					else
					{
						text = new CarteiraHsbc(num).Codigo;
					}
				}
				else
				{
					text = new CarteiraCaixa(num, this.Boleto.FormaCadastramento.Codigo, this.Boleto.Cedente.TipoSolucao).Codigo;
				}
			}
			else
			{
				text = this.Boleto.TipoCobranca.Codigo.ToString();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception("O código da carteira não foi implementado.");
			}
			return text;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002772C File Offset: 0x0002592C
		protected StringBuilder HtmlOffLine(string textoNoComecoDoEmail, string srcLogo, string srcBarra, string srcCodigoBarra)
		{
			this.OnLoad(EventArgs.Empty);
			StringBuilder stringBuilder = new StringBuilder();
			this.HtmlOfflineHeader(stringBuilder);
			if (!string.IsNullOrEmpty(textoNoComecoDoEmail))
			{
				stringBuilder.Append(textoNoComecoDoEmail);
			}
			stringBuilder.Append(this.MontaHtml(srcLogo, srcBarra, "<img src=\"" + srcCodigoBarra + "\" alt=\"Código de Barras\" />"));
			this.HtmlOfflineFooter(stringBuilder);
			return stringBuilder;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0002778C File Offset: 0x0002598C
		protected void HtmlOfflineHeader(StringBuilder html)
		{
			html.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
			html.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
			html.Append("<head>");
			html.Append("    <title>Boleto</title>\n");
			using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("DigisatServer.Infrastructure.Crosscutting.Boleto.BoletoImpressao.BoletoNet.css")))
			{
				html.Append("<style>\n");
				html.Append(streamReader.ReadToEnd());
				html.Append("</style>\n");
				streamReader.Close();
				streamReader.Dispose();
			}
			html.Append("     </head>\n");
			html.Append("<body>\n");
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00027844 File Offset: 0x00025A44
		protected void HtmlOfflineFooter(StringBuilder saida)
		{
			saida.Append("</body>\n");
			saida.Append("</html>\n");
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0002785E File Offset: 0x00025A5E
		public string MontaHtml()
		{
			return this.MontaHtml(null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00027868 File Offset: 0x00025A68
		public string MontaHtml(string fileName)
		{
			if (fileName == null)
			{
				fileName = Path.GetTempPath();
			}
			this.OnLoad(EventArgs.Empty);
			string text = fileName + "BoletoNet" + Utils.FormatCode(this._ibanco.Codigo.ToString(CultureInfo.InvariantCulture), 3) + ".jpg";
			Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DigisatServer.Infrastructure.Crosscutting.Boleto.Imagens." + Utils.FormatCode(this._ibanco.Codigo.ToString(CultureInfo.InvariantCulture), 3) + ".jpg")).Save(text);
			string text2 = fileName + "BoletoNetBarra.gif";
			Html.barra.Save(text2);
			string tempFileName = Path.GetTempFileName();
			new C2of5i(this.Boleto.CodigoBarra.Codigo, 1, 50, this.Boleto.CodigoBarra.Codigo.Length).ToBitmap().Save(tempFileName);
			return this.HtmlOffLine(null, text, text2, tempFileName).ToString();
		}

		// Token: 0x040005DF RID: 1503
		private global::DigisatServer.Infrastructure.Crosscutting.Boleto.Banco.Banco _ibanco;

		// Token: 0x040005E0 RID: 1504
		private short _codigoBanco;

		// Token: 0x040005E1 RID: 1505
		private global::DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.Boleto _boleto;

		// Token: 0x040005E2 RID: 1506
		private List<IInstrucao> _instrucoes = new List<IInstrucao>();

		// Token: 0x040005E3 RID: 1507
		private string _instrucoesHtml = string.Empty;

		// Token: 0x040005E4 RID: 1508
		private bool _mostrarCodigoCarteira;

		// Token: 0x040005E5 RID: 1509
		private bool _formatoCarne;
	}
}

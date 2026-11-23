using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;
using DigisatServer.Infrastructure.Crosscutting.Enum;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000CA RID: 202
	public class EspecieDocumento : AbstractEspecieDocumento
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000DF7C File Offset: 0x0000C17C
		internal EspecieDocumento()
		{
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DF84 File Offset: 0x0000C184
		public EspecieDocumento(int codigoBanco)
		{
			this.InstanciaEspecieDocumento(codigoBanco, "0", TipoArquivo.CNAB240);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000DF99 File Offset: 0x0000C199
		public EspecieDocumento(int codigoBanco, string codigoEspecie)
		{
			this.InstanciaEspecieDocumento(codigoBanco, codigoEspecie, TipoArquivo.CNAB240);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DFAA File Offset: 0x0000C1AA
		public EspecieDocumento(int codigoBanco, string codigoEspecie, TipoArquivo tipoArquivo)
		{
			this.InstanciaEspecieDocumento(codigoBanco, codigoEspecie, tipoArquivo);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		public EspecieDocumento(int codigoBanco, int codigoEspecie)
		{
			this.InstanciaEspecieDocumento(codigoBanco, codigoEspecie.ToString(), TipoArquivo.CNAB240);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000DFD2 File Offset: 0x0000C1D2
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000DFDF File Offset: 0x0000C1DF
		public override IBanco Banco
		{
			get
			{
				return this._iEspecieDocumento.Banco;
			}
			set
			{
				this._iEspecieDocumento.Banco = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000DFED File Offset: 0x0000C1ED
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000DFFA File Offset: 0x0000C1FA
		public override string Codigo
		{
			get
			{
				return this._iEspecieDocumento.Codigo;
			}
			set
			{
				this._iEspecieDocumento.Codigo = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000E008 File Offset: 0x0000C208
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000E015 File Offset: 0x0000C215
		public override string Sigla
		{
			get
			{
				return this._iEspecieDocumento.Sigla;
			}
			set
			{
				this._iEspecieDocumento.Sigla = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000E023 File Offset: 0x0000C223
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000E030 File Offset: 0x0000C230
		public override string Especie
		{
			get
			{
				return this._iEspecieDocumento.Especie;
			}
			set
			{
				this._iEspecieDocumento.Especie = value;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000E040 File Offset: 0x0000C240
		private void InstanciaEspecieDocumento(int codigoBanco, string codigoEspecie, TipoArquivo tipoArquivo = TipoArquivo.CNAB240)
		{
			try
			{
				if (codigoBanco <= 104)
				{
					if (codigoBanco <= 41)
					{
						if (codigoBanco <= 4)
						{
							if (codigoBanco == 1)
							{
								this._iEspecieDocumento = new EspecieDocumentoBancoBrasil(codigoEspecie, tipoArquivo);
								goto IL_024C;
							}
							if (codigoBanco != 4)
							{
								goto IL_023B;
							}
							this._iEspecieDocumento = new EspecieDocumentoNordeste(codigoEspecie);
							goto IL_024C;
						}
						else if (codigoBanco != 8 && codigoBanco != 33)
						{
							if (codigoBanco != 41)
							{
								goto IL_023B;
							}
							this._iEspecieDocumento = new EspecieDocumentoBanrisul(codigoEspecie);
							goto IL_024C;
						}
					}
					else if (codigoBanco <= 77)
					{
						if (codigoBanco == 47)
						{
							this._iEspecieDocumento = new EspecieDocumentoBanese(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco != 77)
						{
							goto IL_023B;
						}
						this._iEspecieDocumento = new EspecieDocumentoIntermedium(codigoEspecie);
						goto IL_024C;
					}
					else
					{
						if (codigoBanco == 85)
						{
							this._iEspecieDocumento = new EspecieDocumentoAilos(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco == 97)
						{
							this._iEspecieDocumento = new EspecieDocumentoCredisis(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco != 104)
						{
							goto IL_023B;
						}
						this._iEspecieDocumento = new EspecieDocumentoCaixa(codigoEspecie);
						goto IL_024C;
					}
				}
				else if (codigoBanco <= 341)
				{
					if (codigoBanco <= 136)
					{
						if (codigoBanco == 133)
						{
							this._iEspecieDocumento = new EspecieDocumentoCresol(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco != 136)
						{
							goto IL_023B;
						}
						this._iEspecieDocumento = new EspecieDocumentoUnicred(codigoEspecie);
						goto IL_024C;
					}
					else
					{
						if (codigoBanco == 208)
						{
							this._iEspecieDocumento = new EspecieDocumentoBtg(codigoEspecie, tipoArquivo);
							goto IL_024C;
						}
						if (codigoBanco == 237)
						{
							this._iEspecieDocumento = new EspecieDocumentoBradesco(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco != 341)
						{
							goto IL_023B;
						}
						this._iEspecieDocumento = new EspecieDocumentoItau(codigoEspecie);
						goto IL_024C;
					}
				}
				else if (codigoBanco <= 422)
				{
					if (codigoBanco != 353)
					{
						if (codigoBanco == 399)
						{
							this._iEspecieDocumento = new EspecieDocumentoHsbc(codigoEspecie);
							goto IL_024C;
						}
						if (codigoBanco != 422)
						{
							goto IL_023B;
						}
						this._iEspecieDocumento = new EspecieDocumentoSafra(codigoEspecie);
						goto IL_024C;
					}
				}
				else
				{
					if (codigoBanco == 707)
					{
						this._iEspecieDocumento = new EspecieDocumentoDaycoval(codigoEspecie);
						goto IL_024C;
					}
					if (codigoBanco == 748)
					{
						this._iEspecieDocumento = new EspecieDocumentoSicredi(codigoEspecie);
						goto IL_024C;
					}
					if (codigoBanco != 756)
					{
						goto IL_023B;
					}
					this._iEspecieDocumento = new EspecieDocumentoSicoob(codigoEspecie, tipoArquivo);
					goto IL_024C;
				}
				this._iEspecieDocumento = new EspecieDocumentoSantander(codigoEspecie);
				goto IL_024C;
				IL_023B:
				throw new Exception("Espécie do documento não implementada: " + codigoEspecie);
				IL_024C:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro durante a execução da transação. " + ex.Message, ex);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public static string ValidaSigla(IEspecieDocumento especie)
		{
			string text;
			try
			{
				text = especie.Sigla;
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E300 File Offset: 0x0000C500
		public static string ValidaCodigo(IEspecieDocumento especie)
		{
			string text;
			try
			{
				text = especie.Codigo;
			}
			catch
			{
				text = "0";
			}
			return text;
		}

		// Token: 0x04000317 RID: 791
		private IEspecieDocumento _iEspecieDocumento;
	}
}

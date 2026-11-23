using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000CC RID: 204
	public class EspecieDocumentoCredisis : AbstractEspecieDocumento
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000E330 File Offset: 0x0000C530
		public EspecieDocumentoCredisis(string codigo)
		{
			try
			{
				this.Carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E36C File Offset: 0x0000C56C
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoCredisis();
				EnumEspecieDocumentoCredisis enumEspecieByCodigo = EspecieDocumentoCredisis.GetEnumEspecieByCodigo(idCodigo);
				if (enumEspecieByCodigo <= EnumEspecieDocumentoCredisis.NotaPromissoria)
				{
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.DuplicataMercantilPorIndicacao)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.DuplicataMercantilPorIndicacao);
						this.Especie = "Duplicata Mercantil p/ Indicação";
						this.Sigla = "DMI";
						goto IL_014C;
					}
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.DuplicataServicoPorIndicacao)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.DuplicataServicoPorIndicacao);
						this.Especie = "Duplicata de Serviço p/ Indicação";
						this.Sigla = "DSI";
						goto IL_014C;
					}
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.NotaPromissoria)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.NotaPromissoria);
						this.Especie = "Nota Promissória";
						this.Sigla = "NP";
						goto IL_014C;
					}
				}
				else
				{
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.Recibo)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.Recibo);
						this.Especie = "Recibo";
						this.Sigla = "RC";
						goto IL_014C;
					}
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.MensalidadeEscolar)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.MensalidadeEscolar);
						this.Especie = "Mensalidade Escolar";
						this.Sigla = "ME";
						goto IL_014C;
					}
					if (enumEspecieByCodigo == EnumEspecieDocumentoCredisis.NotaFiscal)
					{
						this.Codigo = EspecieDocumentoCredisis.GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis.NotaFiscal);
						this.Especie = "Nota Fiscal";
						this.Sigla = "NF";
						goto IL_014C;
					}
				}
				this.Codigo = "03";
				this.Especie = "Duplicata Mercantil p/ Indicação";
				this.Sigla = "DMI";
				IL_014C:;
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		public static string GetCodigoEspecieByEnum(EnumEspecieDocumentoCredisis especie)
		{
			if (especie <= EnumEspecieDocumentoCredisis.NotaPromissoria)
			{
				if (especie == EnumEspecieDocumentoCredisis.DuplicataMercantilPorIndicacao)
				{
					return "03";
				}
				if (especie == EnumEspecieDocumentoCredisis.DuplicataServicoPorIndicacao)
				{
					return "05";
				}
				if (especie == EnumEspecieDocumentoCredisis.NotaPromissoria)
				{
					return "12";
				}
			}
			else
			{
				if (especie == EnumEspecieDocumentoCredisis.Recibo)
				{
					return "17";
				}
				if (especie == EnumEspecieDocumentoCredisis.MensalidadeEscolar)
				{
					return "21";
				}
				if (especie == EnumEspecieDocumentoCredisis.NotaFiscal)
				{
					return "23";
				}
			}
			return "03";
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000E54C File Offset: 0x0000C74C
		public static EnumEspecieDocumentoCredisis GetEnumEspecieByCodigo(string codigo)
		{
			if (codigo == "3")
			{
				return EnumEspecieDocumentoCredisis.DuplicataMercantilPorIndicacao;
			}
			if (codigo == "5")
			{
				return EnumEspecieDocumentoCredisis.DuplicataServicoPorIndicacao;
			}
			if (codigo == "12")
			{
				return EnumEspecieDocumentoCredisis.NotaPromissoria;
			}
			if (codigo == "17")
			{
				return EnumEspecieDocumentoCredisis.Recibo;
			}
			if (codigo == "21")
			{
				return EnumEspecieDocumentoCredisis.MensalidadeEscolar;
			}
			if (!(codigo == "23"))
			{
				return EnumEspecieDocumentoCredisis.DuplicataMercantilPorIndicacao;
			}
			return EnumEspecieDocumentoCredisis.NotaFiscal;
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000CE RID: 206
	public class EspecieDocumentoAilos : AbstractEspecieDocumento
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		public EspecieDocumentoAilos(string codigo)
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

		// Token: 0x06000358 RID: 856 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoAilos();
				EnumEspecieDocumentoAilos enumEspecieByCodigo = EspecieDocumentoAilos.GetEnumEspecieByCodigo(idCodigo);
				if (enumEspecieByCodigo != EnumEspecieDocumentoAilos.DuplicataMercantil)
				{
					if (enumEspecieByCodigo != EnumEspecieDocumentoAilos.DuplicataServico)
					{
						this.Codigo = "02";
						this.Especie = "Duplicata Mercantil";
						this.Sigla = "DM";
					}
					else
					{
						this.Codigo = EspecieDocumentoAilos.GetCodigoEspecieByEnum(EnumEspecieDocumentoAilos.DuplicataServico);
						this.Especie = "Duplicata de Serviço";
						this.Sigla = "DS";
					}
				}
				else
				{
					this.Codigo = EspecieDocumentoAilos.GetCodigoEspecieByEnum(EnumEspecieDocumentoAilos.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public static string GetCodigoEspecieByEnum(EnumEspecieDocumentoAilos especie)
		{
			if (especie == EnumEspecieDocumentoAilos.DuplicataMercantil)
			{
				return "02";
			}
			if (especie != EnumEspecieDocumentoAilos.DuplicataServico)
			{
				return "02";
			}
			return "04";
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000E6C9 File Offset: 0x0000C8C9
		public static EnumEspecieDocumentoAilos GetEnumEspecieByCodigo(string codigo)
		{
			if (codigo == "2")
			{
				return EnumEspecieDocumentoAilos.DuplicataMercantil;
			}
			if (!(codigo == "4"))
			{
				return EnumEspecieDocumentoAilos.DuplicataMercantil;
			}
			return EnumEspecieDocumentoAilos.DuplicataServico;
		}
	}
}

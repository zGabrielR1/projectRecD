using System;
using System.Linq;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000D2 RID: 210
	public class EspecieDocumentoBankBoston : AbstractEspecieDocumento
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public EspecieDocumentoBankBoston(string codigo)
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

		// Token: 0x06000364 RID: 868 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoBankBoston()
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000F300 File Offset: 0x0000D500
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBankBoston.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoBankBoston.DuplicataServico:
				return "2";
			case EnumEspecieDocumentoBankBoston.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoBankBoston.ReciboEscolar:
				return "4";
			case EnumEspecieDocumentoBankBoston.ReciboAssociacao:
				return "5";
			case EnumEspecieDocumentoBankBoston.ReciboCondominio:
				return "6";
			case EnumEspecieDocumentoBankBoston.NotaDebito:
				return "7";
			case EnumEspecieDocumentoBankBoston.Outros:
				return "8";
			default:
				return "0";
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000F36C File Offset: 0x0000D56C
		public EnumEspecieDocumentoBankBoston GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 856466825U)
			{
				if (num <= 822911587U)
				{
					if (num != 806133968U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoBankBoston.ReciboEscolar;
							}
						}
					}
					else if (codigo == "5")
					{
						return EnumEspecieDocumentoBankBoston.ReciboAssociacao;
					}
				}
				else if (num != 839689206U)
				{
					if (num == 856466825U)
					{
						if (codigo == "6")
						{
							return EnumEspecieDocumentoBankBoston.ReciboCondominio;
						}
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoBankBoston.NotaDebito;
				}
			}
			else if (num <= 906799682U)
			{
				if (num != 873244444U)
				{
					if (num == 906799682U)
					{
						if (codigo == "3")
						{
							return EnumEspecieDocumentoBankBoston.NotaSeguro;
						}
					}
				}
				else if (codigo == "1")
				{
					return EnumEspecieDocumentoBankBoston.DuplicataMercantil;
				}
			}
			else if (num != 923577301U)
			{
				if (num == 1024243015U)
				{
					if (codigo == "8")
					{
						return EnumEspecieDocumentoBankBoston.Outros;
					}
				}
			}
			else if (codigo == "2")
			{
				return EnumEspecieDocumentoBankBoston.DuplicataServico;
			}
			return EnumEspecieDocumentoBankBoston.Outros;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000F480 File Offset: 0x0000D680
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoBankBoston();
				switch (this.GetEnumEspecieByCodigo(codigo))
				{
				case EnumEspecieDocumentoBankBoston.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.DuplicataMercantil);
					this.Sigla = "DM";
					this.Especie = "Duplicata mercantil";
					break;
				case EnumEspecieDocumentoBankBoston.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.DuplicataServico);
					this.Sigla = "DS";
					this.Especie = "Duplicata de serviço";
					break;
				case EnumEspecieDocumentoBankBoston.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.NotaSeguro);
					this.Sigla = "NS";
					this.Especie = "Nota de seguro";
					break;
				case EnumEspecieDocumentoBankBoston.ReciboEscolar:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.ReciboEscolar);
					this.Sigla = "RE";
					this.Especie = "Reciboescolar";
					break;
				case EnumEspecieDocumentoBankBoston.ReciboAssociacao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.ReciboAssociacao);
					this.Sigla = "RS";
					this.Especie = "Recibo Associação";
					break;
				case EnumEspecieDocumentoBankBoston.ReciboCondominio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.ReciboCondominio);
					this.Sigla = "RC";
					this.Especie = "Contrato";
					break;
				case EnumEspecieDocumentoBankBoston.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.NotaDebito);
					this.Sigla = "ND";
					this.Especie = "Nota débito";
					break;
				case EnumEspecieDocumentoBankBoston.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBankBoston.Outros);
					this.Sigla = "OT";
					this.Especie = "Outros";
					break;
				default:
					this.Codigo = "0";
					this.Especie = "( Selecione )";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000F644 File Offset: 0x0000D844
		public static EspeciesDocumento CarregaTodas()
		{
			EspeciesDocumento especiesDocumento = new EspeciesDocumento();
			EspecieDocumentoBankBoston ed = new EspecieDocumentoBankBoston();
			especiesDocumento.AddRange(from EnumEspecieDocumentoBankBoston item in Enum.GetValues(typeof(EnumEspecieDocumentoBankBoston))
				select new EspecieDocumentoBankBoston(ed.GetCodigoEspecieByEnum(item)));
			return especiesDocumento;
		}
	}
}

using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000D8 RID: 216
	public class EspecieDocumentoBradesco : AbstractEspecieDocumento
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoBradesco()
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public EspecieDocumentoBradesco(string codigo)
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

		// Token: 0x06000374 RID: 884 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoBradesco.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoBradesco.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoBradesco.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoBradesco.CobrancaSeriada:
				return "4";
			case EnumEspecieDocumentoBradesco.Recibo:
				return "5";
			case EnumEspecieDocumentoBradesco.LetraCambio:
				return "10";
			case EnumEspecieDocumentoBradesco.NotaDebito:
				return "11";
			case EnumEspecieDocumentoBradesco.DuplicataServico:
				return "12";
			case EnumEspecieDocumentoBradesco.Outros:
				return "99";
			default:
				return "99";
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FC48 File Offset: 0x0000DE48
		public EnumEspecieDocumentoBradesco GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 501951850U)
			{
				if (num <= 468396612U)
				{
					if (num != 352233207U)
					{
						if (num == 468396612U)
						{
							if (codigo == "10")
							{
								return EnumEspecieDocumentoBradesco.LetraCambio;
							}
						}
					}
					else if (codigo == "99")
					{
						return EnumEspecieDocumentoBradesco.Outros;
					}
				}
				else if (num != 485174231U)
				{
					if (num == 501951850U)
					{
						if (codigo == "12")
						{
							return EnumEspecieDocumentoBradesco.DuplicataServico;
						}
					}
				}
				else if (codigo == "11")
				{
					return EnumEspecieDocumentoBradesco.NotaDebito;
				}
			}
			else if (num <= 822911587U)
			{
				if (num != 806133968U)
				{
					if (num == 822911587U)
					{
						if (codigo == "4")
						{
							return EnumEspecieDocumentoBradesco.CobrancaSeriada;
						}
					}
				}
				else if (codigo == "5")
				{
					return EnumEspecieDocumentoBradesco.Recibo;
				}
			}
			else if (num != 873244444U)
			{
				if (num != 906799682U)
				{
					if (num == 923577301U)
					{
						if (codigo == "2")
						{
							return EnumEspecieDocumentoBradesco.NotaPromissoria;
						}
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoBradesco.NotaSeguro;
				}
			}
			else if (codigo == "1")
			{
				return EnumEspecieDocumentoBradesco.DuplicataMercantil;
			}
			return EnumEspecieDocumentoBradesco.Outros;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoBradesco();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoBradesco.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoBradesco.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoBradesco.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.NotaSeguro);
					this.Especie = "Nota de seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoBradesco.CobrancaSeriada:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.CobrancaSeriada);
					this.Especie = "Cobrança seriada";
					this.Sigla = "CS";
					break;
				case EnumEspecieDocumentoBradesco.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoBradesco.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.LetraCambio);
					this.Sigla = "LC";
					this.Especie = "Letra de câmbio";
					break;
				case EnumEspecieDocumentoBradesco.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.NotaDebito);
					this.Sigla = "ND";
					this.Especie = "Nota de débito";
					break;
				case EnumEspecieDocumentoBradesco.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.DuplicataServico);
					this.Sigla = "DS";
					this.Especie = "Duplicata de serviço";
					break;
				case EnumEspecieDocumentoBradesco.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.Outros);
					this.Especie = "Outros";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoBradesco.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DM";
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

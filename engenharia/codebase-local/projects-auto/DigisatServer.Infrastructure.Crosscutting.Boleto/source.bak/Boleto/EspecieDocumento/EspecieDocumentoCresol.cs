using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000F4 RID: 244
	public class EspecieDocumentoCresol : AbstractEspecieDocumento
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public EspecieDocumentoCresol()
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014D24 File Offset: 0x00012F24
		public EspecieDocumentoCresol(string codigo)
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

		// Token: 0x060003B7 RID: 951 RVA: 0x00014D60 File Offset: 0x00012F60
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoCresol.Cheque:
				return "1";
			case EnumEspecieDocumentoCresol.DuplicataMercantil:
				return "2";
			case EnumEspecieDocumentoCresol.DuplicataServico:
				return "4";
			case EnumEspecieDocumentoCresol.DuplicataRural:
				return "6";
			case EnumEspecieDocumentoCresol.LetraCambio:
				return "7";
			case EnumEspecieDocumentoCresol.NotaPromissoria:
				return "12";
			case EnumEspecieDocumentoCresol.Recibo:
				return "17";
			case EnumEspecieDocumentoCresol.NotaDebito:
				return "19";
			case EnumEspecieDocumentoCresol.Warrant:
				return "26";
			case EnumEspecieDocumentoCresol.DividaAtivaEstado:
				return "27";
			case EnumEspecieDocumentoCresol.DividaAtivaMunicipio:
				return "28";
			case EnumEspecieDocumentoCresol.DividaAtivaUniao:
				return "29";
			case EnumEspecieDocumentoCresol.EncargosCondominiais:
				return "30";
			case EnumEspecieDocumentoCresol.BoletoProposta:
				return "32";
			case EnumEspecieDocumentoCresol.Outros:
				return "99";
			default:
				return "99";
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00014E10 File Offset: 0x00013010
		public EnumEspecieDocumentoCresol GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 856466825U)
			{
				if (num <= 451618993U)
				{
					if (num != 350953279U)
					{
						if (num != 352233207U)
						{
							if (num == 451618993U)
							{
								if (codigo == "17")
								{
									return EnumEspecieDocumentoCresol.Recibo;
								}
							}
						}
						else if (codigo == "99")
						{
							return EnumEspecieDocumentoCresol.Outros;
						}
					}
					else if (codigo == "19")
					{
						return EnumEspecieDocumentoCresol.NotaDebito;
					}
				}
				else if (num <= 822911587U)
				{
					if (num != 501951850U)
					{
						if (num == 822911587U)
						{
							if (codigo == "4")
							{
								return EnumEspecieDocumentoCresol.DuplicataServico;
							}
						}
					}
					else if (codigo == "12")
					{
						return EnumEspecieDocumentoCresol.NotaPromissoria;
					}
				}
				else if (num != 839689206U)
				{
					if (num == 856466825U)
					{
						if (codigo == "6")
						{
							return EnumEspecieDocumentoCresol.DuplicataRural;
						}
					}
				}
				else if (codigo == "7")
				{
					return EnumEspecieDocumentoCresol.LetraCambio;
				}
			}
			else if (num <= 2280673654U)
			{
				if (num <= 923577301U)
				{
					if (num != 873244444U)
					{
						if (num == 923577301U)
						{
							if (codigo == "2")
							{
								return EnumEspecieDocumentoCresol.DuplicataMercantil;
							}
						}
					}
					else if (codigo == "1")
					{
						return EnumEspecieDocumentoCresol.Cheque;
					}
				}
				else if (num != 2247118416U)
				{
					if (num == 2280673654U)
					{
						if (codigo == "30")
						{
							return EnumEspecieDocumentoCresol.EncargosCondominiais;
						}
					}
				}
				else if (codigo == "32")
				{
					return EnumEspecieDocumentoCresol.BoletoProposta;
				}
			}
			else if (num <= 2347931225U)
			{
				if (num != 2331153606U)
				{
					if (num == 2347931225U)
					{
						if (codigo == "26")
						{
							return EnumEspecieDocumentoCresol.Warrant;
						}
					}
				}
				else if (codigo == "27")
				{
					return EnumEspecieDocumentoCresol.DividaAtivaEstado;
				}
			}
			else if (num != 2498929796U)
			{
				if (num == 2515707415U)
				{
					if (codigo == "28")
					{
						return EnumEspecieDocumentoCresol.DividaAtivaMunicipio;
					}
				}
			}
			else if (codigo == "29")
			{
				return EnumEspecieDocumentoCresol.DividaAtivaUniao;
			}
			return EnumEspecieDocumentoCresol.Outros;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001504C File Offset: 0x0001324C
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoCresol();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoCresol.Cheque:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.Cheque);
					this.Especie = "Cheque";
					this.Sigla = "CH";
					break;
				case EnumEspecieDocumentoCresol.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DuplicataMercantil);
					this.Especie = "Duplicata mercantil";
					this.Sigla = "DM";
					break;
				case EnumEspecieDocumentoCresol.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DuplicataServico);
					this.Especie = "Duplicata de serviço";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoCresol.DuplicataRural:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DuplicataRural);
					this.Especie = "Duplicata rural";
					this.Sigla = "DR";
					break;
				case EnumEspecieDocumentoCresol.LetraCambio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.LetraCambio);
					this.Sigla = "LC";
					this.Especie = "Letra de câmbio";
					break;
				case EnumEspecieDocumentoCresol.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.NotaPromissoria);
					this.Especie = "Nota promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoCresol.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "REC";
					break;
				case EnumEspecieDocumentoCresol.NotaDebito:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.NotaDebito);
					this.Especie = "Nota de débito";
					this.Sigla = "ND";
					break;
				case EnumEspecieDocumentoCresol.Warrant:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.Warrant);
					this.Especie = "Warrant";
					this.Sigla = "W";
					break;
				case EnumEspecieDocumentoCresol.DividaAtivaEstado:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DividaAtivaEstado);
					this.Sigla = "DE";
					this.Especie = "Dívida ativa de estado";
					break;
				case EnumEspecieDocumentoCresol.DividaAtivaMunicipio:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DividaAtivaMunicipio);
					this.Sigla = "DM";
					this.Especie = "Dívida ativa de município";
					break;
				case EnumEspecieDocumentoCresol.DividaAtivaUniao:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DividaAtivaUniao);
					this.Sigla = "DU";
					this.Especie = "Dívida ativa da união";
					break;
				case EnumEspecieDocumentoCresol.EncargosCondominiais:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.EncargosCondominiais);
					this.Sigla = "EC";
					this.Especie = "Encargos condominiais";
					break;
				case EnumEspecieDocumentoCresol.BoletoProposta:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.BoletoProposta);
					this.Sigla = "BP";
					this.Especie = "Boleto Proposta";
					break;
				case EnumEspecieDocumentoCresol.Outros:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.Outros);
					this.Especie = "Outros";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoCresol.DuplicataMercantil);
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

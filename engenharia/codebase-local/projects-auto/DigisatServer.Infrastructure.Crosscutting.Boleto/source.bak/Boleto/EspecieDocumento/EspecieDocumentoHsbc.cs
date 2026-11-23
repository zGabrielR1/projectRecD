using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.EspecieDocumento
{
	// Token: 0x020000DC RID: 220
	public class EspecieDocumentoHsbc : AbstractEspecieDocumento
	{
		// Token: 0x0600037B RID: 891 RVA: 0x000109C8 File Offset: 0x0000EBC8
		public EspecieDocumentoHsbc(string codigo)
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

		// Token: 0x0600037C RID: 892 RVA: 0x00010A04 File Offset: 0x0000EC04
		public string GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc especie)
		{
			switch (especie)
			{
			case EnumEspecieDocumentoHsbc.DuplicataMercantil:
				return "1";
			case EnumEspecieDocumentoHsbc.NotaPromissoria:
				return "2";
			case EnumEspecieDocumentoHsbc.NotaSeguro:
				return "3";
			case EnumEspecieDocumentoHsbc.Recibo:
				return "5";
			case EnumEspecieDocumentoHsbc.DuplicataServico:
				return "10";
			case EnumEspecieDocumentoHsbc.ComComplementacaoDoBloquetoPeloCliente:
				return "8";
			case EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloBanco:
				return "9";
			case EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloCliente:
				return "98";
			default:
				return "1";
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010A70 File Offset: 0x0000EC70
		public EnumEspecieDocumentoHsbc GetEnumEspecieByCodigo(string codigo)
		{
			uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
			if (num <= 873244444U)
			{
				if (num <= 468396612U)
				{
					if (num != 335455588U)
					{
						if (num == 468396612U)
						{
							if (codigo == "10")
							{
								return EnumEspecieDocumentoHsbc.DuplicataServico;
							}
						}
					}
					else if (codigo == "98")
					{
						return EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloCliente;
					}
				}
				else if (num != 806133968U)
				{
					if (num == 873244444U)
					{
						if (codigo == "1")
						{
							return EnumEspecieDocumentoHsbc.DuplicataMercantil;
						}
					}
				}
				else if (codigo == "5")
				{
					return EnumEspecieDocumentoHsbc.Recibo;
				}
			}
			else if (num <= 923577301U)
			{
				if (num != 906799682U)
				{
					if (num == 923577301U)
					{
						if (codigo == "2")
						{
							return EnumEspecieDocumentoHsbc.NotaPromissoria;
						}
					}
				}
				else if (codigo == "3")
				{
					return EnumEspecieDocumentoHsbc.NotaSeguro;
				}
			}
			else if (num != 1007465396U)
			{
				if (num == 1024243015U)
				{
					if (codigo == "8")
					{
						return EnumEspecieDocumentoHsbc.ComComplementacaoDoBloquetoPeloCliente;
					}
				}
			}
			else if (codigo == "9")
			{
				return EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloBanco;
			}
			return EnumEspecieDocumentoHsbc.DuplicataMercantil;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010B80 File Offset: 0x0000ED80
		private void Carregar(string idCodigo)
		{
			try
			{
				this.Banco = new BancoHsbc();
				switch (this.GetEnumEspecieByCodigo(idCodigo))
				{
				case EnumEspecieDocumentoHsbc.DuplicataMercantil:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DP";
					break;
				case EnumEspecieDocumentoHsbc.NotaPromissoria:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.NotaPromissoria);
					this.Especie = "Nota Promissória";
					this.Sigla = "NP";
					break;
				case EnumEspecieDocumentoHsbc.NotaSeguro:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.NotaSeguro);
					this.Especie = "Nota de Seguro";
					this.Sigla = "NS";
					break;
				case EnumEspecieDocumentoHsbc.Recibo:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.Recibo);
					this.Especie = "Recibo";
					this.Sigla = "RC";
					break;
				case EnumEspecieDocumentoHsbc.DuplicataServico:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.DuplicataServico);
					this.Especie = "Duplicata de Serviços";
					this.Sigla = "DS";
					break;
				case EnumEspecieDocumentoHsbc.ComComplementacaoDoBloquetoPeloCliente:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.ComComplementacaoDoBloquetoPeloCliente);
					this.Especie = "Com complementação do bloqueto pelo cliente";
					this.Sigla = "SD";
					break;
				case EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloBanco:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloBanco);
					this.Especie = "Cobrança com emissão total do bloqueto pelo Banco";
					this.Sigla = "CE";
					break;
				case EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloCliente:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.CobrancaComEmissaoTotalDoBloquetoPeloCliente);
					this.Especie = "Cobrança com emissão total do bloqueto pelo cliente";
					this.Sigla = "PD";
					break;
				default:
					this.Codigo = this.GetCodigoEspecieByEnum(EnumEspecieDocumentoHsbc.DuplicataMercantil);
					this.Especie = "Duplicata Mercantil";
					this.Sigla = "DP";
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

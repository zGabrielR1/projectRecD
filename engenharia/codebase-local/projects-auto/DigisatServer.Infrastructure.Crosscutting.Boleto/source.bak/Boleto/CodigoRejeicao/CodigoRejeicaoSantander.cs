using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000106 RID: 262
	public class CodigoRejeicaoSantander : AbstractCodigoRejeicao
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public CodigoRejeicaoSantander(string codigo)
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

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001E4EC File Offset: 0x0001C6EC
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoSantander();
				uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
				if (num <= 2279687916U)
				{
					if (num <= 468543707U)
					{
						if (num <= 418063755U)
						{
							if (num <= 367583803U)
							{
								if (num <= 350806184U)
								{
									if (num != 334175660U)
									{
										if (num == 350806184U)
										{
											if (codigo == "61")
											{
												this.Codigo = "61";
												this.Descricao = "Alteração da Agência Cobradora/DV Inválida";
											}
										}
									}
									else if (codigo == "18")
									{
										this.Codigo = "18";
										this.Descricao = "Vencimento fora do prazo de operação";
									}
								}
								else if (num != 350953279U)
								{
									if (num == 367583803U)
									{
										if (codigo == "60")
										{
											this.Codigo = "60";
											this.Descricao = "Movimento para Título Não Cadastrado";
										}
									}
								}
								else if (codigo == "19")
								{
									this.Codigo = "19";
									this.Descricao = "Título a Cargo de Bco Correspondentes c/ Vencto Inferior a XX Dias";
								}
							}
							else if (num <= 401139041U)
							{
								if (num != 384361422U)
								{
									if (num == 401139041U)
									{
										if (codigo == "62")
										{
											this.Codigo = "62";
											this.Descricao = "Tipo de Impressão Inválido";
										}
									}
								}
								else if (codigo == "63")
								{
									this.Codigo = "63";
									this.Descricao = "Entrada para Título já Cadastrado";
								}
							}
							else if (num != 401286136U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										this.Codigo = "15";
										this.Descricao = "Características Cobrança Incompatíveis";
									}
								}
							}
							else if (codigo == "14")
							{
								this.Codigo = "14";
								this.Descricao = "Identif. Da Distribuição do Bloqueto Inválida";
							}
						}
						else if (num <= 434988469U)
						{
							if (num <= 434694279U)
							{
								if (num != 418210850U)
								{
									if (num == 434694279U)
									{
										if (codigo == "64")
										{
											this.Codigo = "64";
											this.Descricao = "Número da Linha Inválido";
										}
									}
								}
								else if (codigo == "09")
								{
									this.Codigo = "09";
									this.Descricao = "Nosso Número Duplicado";
								}
							}
							else if (num != 434841374U)
							{
								if (num == 434988469U)
								{
									if (codigo == "08")
									{
										this.Codigo = "08";
										this.Descricao = "Nosso Número Inválido";
									}
								}
							}
							else if (codigo == "16")
							{
								this.Codigo = "16";
								this.Descricao = "Data de Vencimento Inválida";
							}
						}
						else if (num <= 451766088U)
						{
							if (num != 451618993U)
							{
								if (num == 451766088U)
								{
									if (codigo == "07")
									{
										this.Codigo = "07";
										this.Descricao = "Agencia/Conta/DV Inválidos";
									}
								}
							}
							else if (codigo == "17")
							{
								this.Codigo = "17";
								this.Descricao = "Data de Vencimento Anterior a Data de Emissão";
							}
						}
						else if (num != 468396612U)
						{
							if (num == 468543707U)
							{
								if (codigo == "06")
								{
									this.Codigo = "06";
									this.Descricao = "Tipo/Número Inscrição Cedente Inválido";
								}
							}
						}
						else if (codigo == "10")
						{
							this.Codigo = "10";
							this.Descricao = "Carteira Inválida";
						}
					}
					else if (num <= 552431802U)
					{
						if (num <= 502098945U)
						{
							if (num <= 485321326U)
							{
								if (num != 485174231U)
								{
									if (num == 485321326U)
									{
										if (codigo == "05")
										{
											this.Codigo = "05";
											this.Descricao = "Código do Movimento Inválido";
										}
									}
								}
								else if (codigo == "11")
								{
									this.Codigo = "11";
									this.Descricao = "Forma de Cadastramento do Título Inválida";
								}
							}
							else if (num != 501951850U)
							{
								if (num == 502098945U)
								{
									if (codigo == "04")
									{
										this.Codigo = "04";
										this.Descricao = "Código do Movimento não Permitido para Carteira";
									}
								}
							}
							else if (codigo == "12")
							{
								this.Codigo = "12";
								this.Descricao = "Tipo de Documento Inválido";
							}
						}
						else if (num <= 518876564U)
						{
							if (num != 518729469U)
							{
								if (num == 518876564U)
								{
									if (codigo == "03")
									{
										this.Codigo = "03";
										this.Descricao = "Código do Segmento Inválido";
									}
								}
							}
							else if (codigo == "13")
							{
								this.Codigo = "13";
								this.Descricao = "Identif. Da Emissão do Bloqueto Inválida";
							}
						}
						else if (num != 535654183U)
						{
							if (num == 552431802U)
							{
								if (codigo == "01")
								{
									this.Codigo = "01";
									this.Descricao = "Código do Banco Inválido";
								}
							}
						}
						else if (codigo == "02")
						{
							this.Codigo = "02";
							this.Descricao = "Código do Registro Detalhe Inválido";
						}
					}
					else if (num <= 2247118416U)
					{
						if (num <= 2229355059U)
						{
							if (num != 2212577440U)
							{
								if (num == 2229355059U)
								{
									if (codigo == "51")
									{
										this.Codigo = "51";
										this.Descricao = "CEP incompatível com a Unidade da Federação";
									}
								}
							}
							else if (codigo == "50")
							{
								this.Codigo = "50";
								this.Descricao = "CEP Referente a um Banco Correspondente";
							}
						}
						else if (num != 2246132678U)
						{
							if (num == 2247118416U)
							{
								if (codigo == "32")
								{
									this.Codigo = "32";
									this.Descricao = "Valor do IOF Inválido";
								}
							}
						}
						else if (codigo == "52")
						{
							this.Codigo = "52";
							this.Descricao = "Unidade de federação inválida";
						}
					}
					else if (num <= 2263057392U)
					{
						if (num != 2262910297U)
						{
							if (num == 2263057392U)
							{
								if (codigo == "43")
								{
									this.Codigo = "43";
									this.Descricao = "Prazo para Baixa/Devolução Inválido";
								}
							}
						}
						else if (codigo == "53")
						{
							this.Codigo = "53";
							this.Descricao = "Tipo/Número de Inscrição do Sacador/Avalista Inválidos";
						}
					}
					else if (num != 2263896035U)
					{
						if (num == 2279687916U)
						{
							if (codigo == "54")
							{
								this.Codigo = "54";
								this.Descricao = "Sacador/Avalista Não Informado";
							}
						}
					}
					else if (codigo == "33")
					{
						this.Codigo = "33";
						this.Descricao = "Valor do Abatimento Inválido";
					}
				}
				else if (num <= 2346945487U)
				{
					if (num <= 2313390249U)
					{
						if (num <= 2296612630U)
						{
							if (num <= 2280673654U)
							{
								if (num != 2279835011U)
								{
									if (num == 2280673654U)
									{
										if (codigo == "30")
										{
											this.Codigo = "30";
											this.Descricao = "Desconto a Conceder Não Confere";
										}
									}
								}
								else if (codigo == "42")
								{
									this.Codigo = "42";
									this.Descricao = "Código para Baixa/Devolução Inválido";
								}
							}
							else if (num != 2296465535U)
							{
								if (num == 2296612630U)
								{
									if (codigo == "41")
									{
										this.Codigo = "41";
										this.Descricao = "Pedido de Cancelamento/Sustação para Títulos sem Instrução de Protesto";
									}
								}
							}
							else if (codigo == "55")
							{
								this.Codigo = "55";
								this.Descricao = "Nosso número no Banco Correspondente Não Informado";
							}
						}
						else if (num <= 2297598368U)
						{
							if (num != 2297451273U)
							{
								if (num == 2297598368U)
								{
									if (codigo == "25")
									{
										this.Codigo = "25";
										this.Descricao = "Data da Emissão Posterior a Data de Entrada";
									}
								}
							}
							else if (codigo == "31")
							{
								this.Codigo = "31";
								this.Descricao = "Concessão de Desconto - Já Existe Desconto Anterior";
							}
						}
						else if (num != 2313243154U)
						{
							if (num == 2313390249U)
							{
								if (codigo == "40")
								{
									this.Codigo = "40";
									this.Descricao = "Título com Ordem de Protesto Emitida ";
								}
							}
						}
						else if (codigo == "56")
						{
							this.Codigo = "56";
							this.Descricao = "Código do Banco Correspondente Não Informado";
						}
					}
					else if (num <= 2330167868U)
					{
						if (num <= 2314375987U)
						{
							if (num != 2314228892U)
							{
								if (num == 2314375987U)
								{
									if (codigo == "24")
									{
										this.Codigo = "24";
										this.Descricao = "Data de Emissão Inválida";
									}
								}
							}
							else if (codigo == "36")
							{
								this.Codigo = "36";
								this.Descricao = "Concessão de Abatimento - Já Existe Abatimento Anterior";
							}
						}
						else if (num != 2330020773U)
						{
							if (num == 2330167868U)
							{
								if (codigo == "47")
								{
									this.Codigo = "47";
									this.Descricao = "Endereço do Sacado Não Informado";
								}
							}
						}
						else if (codigo == "57")
						{
							this.Codigo = "57";
							this.Descricao = "Código da Multa Inválido";
						}
					}
					else if (num <= 2331153606U)
					{
						if (num != 2331006511U)
						{
							if (num == 2331153606U)
							{
								if (codigo == "27")
								{
									this.Codigo = "27";
									this.Descricao = "Valor/Taxa de Juros de Mora Inválido";
								}
							}
						}
						else if (codigo == "37")
						{
							this.Codigo = "37";
							this.Descricao = "Código para Protesto Inválido";
						}
					}
					else if (num != 2346798392U)
					{
						if (num == 2346945487U)
						{
							if (codigo == "46")
							{
								this.Codigo = "46";
								this.Descricao = "Tipo/Número de Inscrição do Sacado Inválidos";
							}
						}
					}
					else if (codigo == "58")
					{
						this.Codigo = "58";
						this.Descricao = "Data da Multa Inválida";
					}
				}
				else if (num <= 2381486463U)
				{
					if (num <= 2363723106U)
					{
						if (num <= 2347931225U)
						{
							if (num != 2347784130U)
							{
								if (num == 2347931225U)
								{
									if (codigo == "26")
									{
										this.Codigo = "26";
										this.Descricao = "Código de Juros de Mora Inválido";
									}
								}
							}
							else if (codigo == "34")
							{
								this.Codigo = "34";
								this.Descricao = "Valor do Abatimento Maior ou Igual ao Valor do Título";
							}
						}
						else if (num != 2363576011U)
						{
							if (num == 2363723106U)
							{
								if (codigo == "45")
								{
									this.Codigo = "45";
									this.Descricao = "Nome do Sacado Não Informado";
								}
							}
						}
						else if (codigo == "59")
						{
							this.Codigo = "59";
							this.Descricao = "Valor/Percentual da Multa Inválido";
						}
					}
					else if (num <= 2364708844U)
					{
						if (num != 2364561749U)
						{
							if (num == 2364708844U)
							{
								if (codigo == "21")
								{
									this.Codigo = "21";
									this.Descricao = "Espécie do Título Inválida";
								}
							}
						}
						else if (codigo == "35")
						{
							this.Codigo = "35";
							this.Descricao = "Abatimento a Conceder Não Confere";
						}
					}
					else if (num != 2380500725U)
					{
						if (num == 2381486463U)
						{
							if (codigo == "20")
							{
								this.Codigo = "20";
								this.Descricao = "Valor do Título Inválido";
							}
						}
					}
					else if (codigo == "44")
					{
						this.Codigo = "44";
						this.Descricao = "Código da Moeda Inválido";
					}
				}
				else if (num <= 2430833582U)
				{
					if (num <= 2414894606U)
					{
						if (num != 2398264082U)
						{
							if (num == 2414894606U)
							{
								if (codigo == "38")
								{
									this.Codigo = "38";
									this.Descricao = "Prazo para Protesto Inválido";
								}
							}
						}
						else if (codigo == "23")
						{
							this.Codigo = "23";
							this.Descricao = "Aceite Inválido";
						}
					}
					else if (num != 2415041701U)
					{
						if (num == 2430833582U)
						{
							if (codigo == "49")
							{
								this.Codigo = "49";
								this.Descricao = "CEP Sem Praça de Cobrança (Não Localizado)";
							}
						}
					}
					else if (codigo == "22")
					{
						this.Codigo = "22";
						this.Descricao = "Espécie do Título Não Permitida para a Carteira";
					}
				}
				else if (num <= 2447611201U)
				{
					if (num != 2431672225U)
					{
						if (num == 2447611201U)
						{
							if (codigo == "48")
							{
								this.Codigo = "48";
								this.Descricao = "CEP Inválido";
							}
						}
					}
					else if (codigo == "39")
					{
						this.Codigo = "39";
						this.Descricao = "Pedido de Protesto Não Permitido para o Título";
					}
				}
				else if (num != 2498929796U)
				{
					if (num != 2515707415U)
					{
						if (num == 2530592872U)
						{
							if (codigo == "A4")
							{
								this.Codigo = "A4";
								this.Descricao = "Sacado DDA";
							}
						}
					}
					else if (codigo == "28")
					{
						this.Codigo = "28";
						this.Descricao = "Código do Desconto Inválido";
					}
				}
				else if (codigo == "29")
				{
					this.Codigo = "29";
					this.Descricao = "Valor do Desconto Maior ou Igual ao Valor do Título";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

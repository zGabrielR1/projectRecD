using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000101 RID: 257
	public class CodigoRejeicaoDaycoval : AbstractCodigoRejeicao
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x000176F8 File Offset: 0x000158F8
		public CodigoRejeicaoDaycoval(string codigo)
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x00017734 File Offset: 0x00015934
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoDaycoval();
				uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
				if (num <= 1087276353U)
				{
					if (num <= 601166687U)
					{
						if (num <= 434988469U)
						{
							if (num <= 367583803U)
							{
								if (num <= 350953279U)
								{
									if (num != 348914022U)
									{
										if (num == 350953279U)
										{
											if (codigo == "19")
											{
												this.Codigo = "19";
												this.Descricao = "Data de desconto inválida ou maior que a data de vencimento";
											}
										}
									}
									else if (codigo == "EB")
									{
										this.Codigo = "EB";
										this.Descricao = "HSBC não aceita endereço de sacado com mais de 38 caracteres";
									}
								}
								else if (num != 352233207U)
								{
									if (num == 367583803U)
									{
										if (codigo == "60")
										{
											this.Codigo = "60";
											this.Descricao = "CNPJ/CPF do sacado inválido – título recusado";
										}
									}
								}
								else if (codigo == "99")
								{
									this.Codigo = "99";
									this.Descricao = "Título não acatado pelo banco – entrar em contato Gerente da conta";
								}
							}
							else if (num <= 401286136U)
							{
								if (num != 400379712U)
								{
									if (num == 401286136U)
									{
										if (codigo == "14")
										{
											this.Codigo = "14";
											this.Descricao = "Registro em duplicidade";
										}
									}
								}
								else if (codigo == "BE")
								{
									this.Codigo = "BE";
									this.Descricao = "Análise gerencial-sacado difere do exigido";
								}
							}
							else if (num != 417157331U)
							{
								if (num != 433934950U)
								{
									if (num == 434988469U)
									{
										if (codigo == "08")
										{
											this.Codigo = "08";
											this.Descricao = "Nome do Sacado Não Informado";
										}
									}
								}
								else if (codigo == "BG")
								{
									this.Codigo = "BG";
									this.Descricao = "Análise gerencial-sacado com baixa liquidez";
								}
							}
							else if (codigo == "BD")
							{
								this.Codigo = "BD";
								this.Descricao = "Análise gerencial-sacado inadimplente";
							}
						}
						else if (num <= 485321326U)
						{
							if (num <= 468396612U)
							{
								if (num != 450712569U)
								{
									if (num == 468396612U)
									{
										if (codigo == "10")
										{
											this.Codigo = "10";
											this.Descricao = "Logradouro não informado";
										}
									}
								}
								else if (codigo == "BF")
								{
									this.Codigo = "BF";
									this.Descricao = "Análise gerencial-vencto excede vencto da operação de crédito";
								}
							}
							else if (num != 468543707U)
							{
								if (num == 485321326U)
								{
									if (codigo == "05")
									{
										this.Codigo = "05";
										this.Descricao = "Data de Vencimento inválida ou fora do prazo mínimo";
									}
								}
							}
							else if (codigo == "06")
							{
								this.Codigo = "06";
								this.Descricao = "Código do Banco Inválido";
							}
						}
						else if (num <= 502098945U)
						{
							if (num != 501045426U)
							{
								if (num == 502098945U)
								{
									if (codigo == "04")
									{
										this.Codigo = "04";
										this.Descricao = "Sigla do Estado Inválida";
									}
								}
							}
							else if (codigo == "BC")
							{
								this.Codigo = "BC";
								this.Descricao = "Análise gerencial-sacado inválido p/operação crédito";
							}
						}
						else if (num != 517278592U)
						{
							if (num != 518876564U)
							{
								if (num == 601166687U)
								{
									if (codigo == "AI")
									{
										this.Codigo = "AI";
										this.Descricao = "Nossa Cobrança inválida";
									}
								}
							}
							else if (codigo == "03")
							{
								this.Codigo = "03";
								this.Descricao = "CEP Inválido";
							}
						}
						else if (codigo == "AL")
						{
							this.Codigo = "AL";
							this.Descricao = "Sacado impedido de entrar nesta cobrança";
						}
					}
					else if (num <= 902722544U)
					{
						if (num <= 684613497U)
						{
							if (num <= 618488759U)
							{
								if (num != 617944306U)
								{
									if (num == 618488759U)
									{
										if (codigo == "BH")
										{
											this.Codigo = "BH";
											this.Descricao = "Análise gerencial-sacado excede concentração";
										}
									}
								}
								else if (codigo == "AJ")
								{
									this.Codigo = "AJ";
									this.Descricao = "Modalidade com bancos correspondentes inválida";
								}
							}
							else if (num != 668277163U)
							{
								if (num == 684613497U)
								{
									if (codigo == "DP")
									{
										this.Codigo = "DP";
										this.Descricao = "Data de Referência menor que a Data de Emissão do Título";
									}
								}
							}
							else if (codigo == "AE")
							{
								this.Codigo = "AE";
								this.Descricao = "Título não possui abatimento";
							}
						}
						else if (num <= 751723973U)
						{
							if (num != 735387639U)
							{
								if (num == 751723973U)
								{
									if (codigo == "DT")
									{
										this.Codigo = "DT";
										this.Descricao = "Nosso Número do Correspondente não deve ser informado";
									}
								}
							}
							else if (codigo == "AA")
							{
								this.Codigo = "AA";
								this.Descricao = "Serviço de Cobrança inválido";
							}
						}
						else if (num != 752165258U)
						{
							if (num != 852830972U)
							{
								if (num == 902722544U)
								{
									if (codigo == "DC")
									{
										this.Codigo = "DC";
										this.Descricao = "Análise gerencial - cedente com limite cancelado";
									}
								}
							}
							else if (codigo == "AX")
							{
								this.Codigo = "AX";
								this.Descricao = "Título em pagamento parcial";
							}
						}
						else if (codigo == "AB")
						{
							this.Codigo = "AB";
							this.Descricao = "Nossa Carteira inválida";
						}
					}
					else if (num <= 969833020U)
					{
						if (num <= 936277782U)
						{
							if (num != 919500163U)
							{
								if (num == 936277782U)
								{
									if (codigo == "DA")
									{
										this.Codigo = "DA";
										this.Descricao = "Análise gerencial – Entrada de Título Descontado com limite cancelado";
									}
								}
							}
							else if (codigo == "DB")
							{
								this.Codigo = "DB";
								this.Descricao = "Análise gerencial – Entrada de Título Descontado com limite vencido";
							}
						}
						else if (num != 936719067U)
						{
							if (num != 953496686U)
							{
								if (num == 969833020U)
								{
									if (codigo == "DG")
									{
										this.Codigo = "DG";
										this.Descricao = "Endereço sacador/avalista não informado";
									}
								}
							}
							else if (codigo == "AV")
							{
								this.Codigo = "AV";
								this.Descricao = "Valor de tarifa de cobrança inválida";
							}
						}
						else if (codigo == "AU")
						{
							this.Codigo = "AU";
							this.Descricao = "Data de ocorrência inválida";
						}
					}
					else if (num <= 1020165877U)
					{
						if (num != 1003388258U)
						{
							if (num == 1020165877U)
							{
								if (codigo == "DD")
								{
									this.Codigo = "DD";
									this.Descricao = "Análise gerencial – cedente é sacado e teve seu limite cancelado";
								}
							}
						}
						else if (codigo == "DE")
						{
							this.Codigo = "DE";
							this.Descricao = "Análise gerencial - apontamento no Serasa";
						}
					}
					else if (num != 1053721115U)
					{
						if (num != 1070498734U)
						{
							if (num == 1087276353U)
							{
								if (codigo == "DH")
								{
									this.Codigo = "DH";
									this.Descricao = "Cep do sacador/avalista não informado";
								}
							}
						}
						else if (codigo == "DI")
						{
							this.Codigo = "DI";
							this.Descricao = "Cidade do sacador/avalista não informado";
						}
					}
					else if (codigo == "DJ")
					{
						this.Codigo = "DJ";
						this.Descricao = "Estado do sacador/avalista inválido ou não informado";
					}
				}
				else if (num <= 2280673654U)
				{
					if (num <= 2094668968U)
					{
						if (num <= 1977225635U)
						{
							if (num <= 1137609210U)
							{
								if (num != 1120831591U)
								{
									if (num == 1137609210U)
									{
										if (codigo == "DM")
										{
											this.Codigo = "DM";
											this.Descricao = "Cliente sem Código de Flash cadastrado no cobrador";
										}
									}
								}
								else if (codigo == "DN")
								{
									this.Codigo = "DN";
									this.Descricao = "Título Descontado com Prazo ZERO – Recusado";
								}
							}
							else if (num != 1708783731U)
							{
								if (num == 1977225635U)
								{
									if (codigo == "CC")
									{
										this.Codigo = "CC";
										this.Descricao = "Valor de iof incompatível com a espécie documento";
									}
								}
							}
							else if (codigo == "CS")
							{
								this.Codigo = "CS";
								this.Descricao = "Título rejeitado pela checagem de duplicatas";
							}
						}
						else if (num <= 2044336111U)
						{
							if (num != 2027558492U)
							{
								if (num == 2044336111U)
								{
									if (codigo == "CG")
									{
										this.Codigo = "CG";
										this.Descricao = "Título não aceito – por análise gerencial";
									}
								}
							}
							else if (codigo == "CF")
							{
								this.Codigo = "CF";
								this.Descricao = "Excede prazo máximo da entrada ao vencimento";
							}
						}
						else if (num != 2061113730U)
						{
							if (num != 2077891349U)
							{
								if (num == 2094668968U)
								{
									if (codigo == "CJ")
									{
										this.Codigo = "CJ";
										this.Descricao = "Análise gerencial-vcto do titulo abaixo prz curto";
									}
								}
							}
							else if (codigo == "CE")
							{
								this.Codigo = "CE";
								this.Descricao = "Título não aceito - pessoa física";
							}
						}
						else if (codigo == "CD")
						{
							this.Codigo = "CD";
							this.Descricao = "Efetivação de protesto sem agenda válida";
						}
					}
					else if (num <= 2246132678U)
					{
						if (num <= 2128224206U)
						{
							if (num != 2111446587U)
							{
								if (num == 2128224206U)
								{
									if (codigo == "CH")
									{
										this.Codigo = "CH";
										this.Descricao = "Título em espera – em análise pelo banco";
									}
								}
							}
							else if (codigo == "CK")
							{
								this.Codigo = "CK";
								this.Descricao = "Análise gerencial-vcto do titulo abaixo prz longo";
							}
						}
						else if (num != 2229355059U)
						{
							if (num == 2246132678U)
							{
								if (codigo == "52")
								{
									this.Codigo = "52";
									this.Descricao = "Sacador/Avalista não informado";
								}
							}
						}
						else if (codigo == "51")
						{
							this.Codigo = "51";
							this.Descricao = "Tipo/Número de Inscrição Sacador/Avalista Inválido";
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
									this.Descricao = "Título enviado em duplicidade nesse movimento";
								}
							}
						}
						else if (codigo == "53")
						{
							this.Codigo = "53";
							this.Descricao = "Prazo de vencimento do título excede ao da contratação";
						}
					}
					else if (num != 2279687916U)
					{
						if (num != 2279835011U)
						{
							if (num == 2280673654U)
							{
								if (codigo == "30")
								{
									this.Codigo = "30";
									this.Descricao = "Valor do título não numérico ou inválido";
								}
							}
						}
						else if (codigo == "42")
						{
							this.Codigo = "42";
							this.Descricao = "Título já existente em nossos registros. Nosso número não aceito";
						}
					}
					else if (codigo == "54")
					{
						this.Codigo = "54";
						this.Descricao = "Banco informado não é nosso correspondente 140-142";
					}
				}
				else if (num <= 2346798392U)
				{
					if (num <= 2314228892U)
					{
						if (num <= 2297598368U)
						{
							if (num != 2296465535U)
							{
								if (num == 2297598368U)
								{
									if (codigo == "25")
									{
										this.Codigo = "25";
										this.Descricao = "CNPJ ou CPF do sacado inválido (aceito com restrições)";
									}
								}
							}
							else if (codigo == "55")
							{
								this.Codigo = "55";
								this.Descricao = "Banco correspondente informado não cobra este CEP ou não possui faixas de CEP cadastradas";
							}
						}
						else if (num != 2313243154U)
						{
							if (num == 2314228892U)
							{
								if (codigo == "36")
								{
									this.Codigo = "36";
									this.Descricao = "Valor de permanência não numérico";
								}
							}
						}
						else if (codigo == "56")
						{
							this.Codigo = "56";
							this.Descricao = "Nosso número no correspondente não foi informado";
						}
					}
					else if (num <= 2331006511U)
					{
						if (num != 2330020773U)
						{
							if (num == 2331006511U)
							{
								if (codigo == "37")
								{
									this.Codigo = "37";
									this.Descricao = "Valor de permanência inconsistente, pois, dentro de um mês, será maior que o valor do título";
								}
							}
						}
						else if (codigo == "57")
						{
							this.Codigo = "57";
							this.Descricao = "Remessa contendo duas instruções incompatíveis – não protestar e dias de protesto ou prazo para protesto inválido";
						}
					}
					else if (num != 2331153606U)
					{
						if (num != 2332139344U)
						{
							if (num == 2346798392U)
							{
								if (codigo == "58")
								{
									this.Codigo = "58";
									this.Descricao = "Entradas Rejeitadas – Reprovado no Represamento para Análise";
								}
							}
						}
						else if (codigo == "87")
						{
							this.Codigo = "87";
							this.Descricao = "Excede Prazo máximo entre emissão e vencimento";
						}
					}
					else if (codigo == "27")
					{
						this.Codigo = "27";
						this.Descricao = "Data de emissão do título inválida";
					}
				}
				else if (num <= 2381486463U)
				{
					if (num <= 2347931225U)
					{
						if (num != 2346945487U)
						{
							if (num == 2347931225U)
							{
								if (codigo == "26")
								{
									this.Codigo = "26";
									this.Descricao = "Espécies de documento inválida (difere de 01...10,13 e 99)";
								}
							}
						}
						else if (codigo == "46")
						{
							this.Codigo = "46";
							this.Descricao = "Título enviado fora da faixa de Nosso Número, estipulada para o cliente";
						}
					}
					else if (num != 2364708844U)
					{
						if (num != 2380500725U)
						{
							if (num == 2381486463U)
							{
								if (codigo == "20")
								{
									this.Codigo = "20";
									this.Descricao = "Valor de IOF não numérico";
								}
							}
						}
						else if (codigo == "44")
						{
							this.Codigo = "44";
							this.Descricao = "Título zerado ou em branco; ou não numérico na remessa";
						}
					}
					else if (codigo == "21")
					{
						this.Codigo = "21";
						this.Descricao = "Movimento para título não cadastrado no sistema";
					}
				}
				else if (num <= 2415041701U)
				{
					if (num != 2414894606U)
					{
						if (num == 2415041701U)
						{
							if (codigo == "22")
							{
								this.Codigo = "22";
								this.Descricao = "Valor de desconto + abatimento maior que o valor do título";
							}
						}
					}
					else if (codigo == "38")
					{
						this.Codigo = "38";
						this.Descricao = "Valor de desconto/abatimento não numérico ou inválido";
					}
				}
				else if (num != 2431672225U)
				{
					if (num != 2498929796U)
					{
						if (num == 2515707415U)
						{
							if (codigo == "28")
							{
								this.Codigo = "28";
								this.Descricao = "Seu número não informado";
							}
						}
					}
					else if (codigo == "29")
					{
						this.Codigo = "29";
						this.Descricao = "CEP é igual a espaço ou zeros; ou não numérico";
					}
				}
				else if (codigo == "39")
				{
					this.Codigo = "39";
					this.Descricao = "Valor de abatimento não numérico";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

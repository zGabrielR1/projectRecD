using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000102 RID: 258
	public class CodigoRejeicaoBanrisul : AbstractCodigoRejeicao
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00018A28 File Offset: 0x00016C28
		public CodigoRejeicaoBanrisul(string codigo)
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

		// Token: 0x060003EB RID: 1003 RVA: 0x00018A64 File Offset: 0x00016C64
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoBanrisul();
				uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
				if (num <= 2296612630U)
				{
					if (num <= 434694279U)
					{
						if (num <= 249381141U)
						{
							if (num <= 199048284U)
							{
								if (num <= 148715427U)
								{
									if (num != 131937808U)
									{
										if (num != 134124160U)
										{
											if (num == 148715427U)
											{
												if (codigo == "B4")
												{
													this.Codigo = "B4";
													this.Descricao = "Valor Máximo/Percentual Inválido";
												}
											}
										}
										else if (codigo == "94")
										{
											this.Codigo = "94";
											this.Descricao = "Código da Carteira inválido para envio e-mail";
										}
									}
									else if (codigo == "B5")
									{
										this.Codigo = "B5";
										this.Descricao = "Valor Mínimo/Percentual Inválido";
									}
								}
								else if (num <= 167679398U)
								{
									if (num != 150901779U)
									{
										if (num == 167679398U)
										{
											if (codigo == "96")
											{
												this.Codigo = "96";
												this.Descricao = "Número de contrato inválido";
											}
										}
									}
									else if (codigo == "95")
									{
										this.Codigo = "95";
										this.Descricao = "Contrato não permite o envio de e-mail";
									}
								}
								else if (num != 184457017U)
								{
									if (num == 199048284U)
									{
										if (codigo == "B1")
										{
											this.Codigo = "B1";
											this.Descricao = "Autoriza recebimento parcial";
										}
									}
								}
								else if (codigo == "97")
								{
									this.Codigo = "97";
									this.Descricao = "Rejeição da alteração do prazo limite de recebimento";
								}
							}
							else if (num <= 218012255U)
							{
								if (num != 201234636U)
								{
									if (num != 216585232U)
									{
										if (num == 218012255U)
										{
											if (codigo == "91")
											{
												this.Codigo = "91";
												this.Descricao = "E-mail/número do celular do sacado não informado";
											}
										}
									}
									else if (codigo == "69")
									{
										this.Codigo = "69";
										this.Descricao = "Débito Não Agendado - Erro nos Dados da Remessa";
									}
								}
								else if (codigo == "90")
								{
									this.Codigo = "90";
									this.Descricao = "E-mail devolvido - caixa postal cheia";
								}
							}
							else if (num <= 233362851U)
							{
								if (num != 232603522U)
								{
									if (num == 233362851U)
									{
										if (codigo == "68")
										{
											this.Codigo = "68";
											this.Descricao = "Débito Automático Agendado";
										}
									}
								}
								else if (codigo == "B3")
								{
									this.Codigo = "B3";
									this.Descricao = "Tipo de Pagamento Inválido";
								}
							}
							else if (num != 234789874U)
							{
								if (num == 249381141U)
								{
									if (codigo == "B2")
									{
										this.Codigo = "B2";
										this.Descricao = "Valor Nominal do Título Conflitante";
									}
								}
							}
							else if (codigo == "92")
							{
								this.Codigo = "92";
								this.Descricao = "Sacado optante por Bloqueto Eletrônico - e-mail não enviado";
							}
						}
						else if (num <= 367583803U)
						{
							if (num <= 335455588U)
							{
								if (num != 251567493U)
								{
									if (num != 334175660U)
									{
										if (num == 335455588U)
										{
											if (codigo == "98")
											{
												this.Codigo = "98";
												this.Descricao = "Rejeição de dispensa de prazo limite de recebimento";
											}
										}
									}
									else if (codigo == "18")
									{
										this.Codigo = "18";
										this.Descricao = "Vencimento fora do prazo de operação";
									}
								}
								else if (codigo == "93")
								{
									this.Codigo = "93";
									this.Descricao = "Código para emissão de bloqueto não permite envio de e-mail";
								}
							}
							else if (num <= 350953279U)
							{
								if (num != 350806184U)
								{
									if (num == 350953279U)
									{
										if (codigo == "19")
										{
											this.Codigo = "19";
											this.Descricao = "Título a Cargo de Bco Correspondentes c/ Vencto Inferior a XX Dias";
										}
									}
								}
								else if (codigo == "61")
								{
									this.Codigo = "61";
									this.Descricao = "Alteração da Agência Cobradora/DV Inválida";
								}
							}
							else if (num != 352233207U)
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
							else if (codigo == "99")
							{
								this.Codigo = "99";
								this.Descricao = "Rejeição da alteração do número do título dado pelo cedente";
							}
						}
						else if (num <= 401286136U)
						{
							if (num != 384361422U)
							{
								if (num != 401139041U)
								{
									if (num == 401286136U)
									{
										if (codigo == "14")
										{
											this.Codigo = "14";
											this.Descricao = "Identif. Da Distribuição do Bloqueto Inválida";
										}
									}
								}
								else if (codigo == "62")
								{
									this.Codigo = "62";
									this.Descricao = "Tipo de Impressão Inválido";
								}
							}
							else if (codigo == "63")
							{
								this.Codigo = "63";
								this.Descricao = "Entrada para Título já Cadastrado";
							}
						}
						else if (num <= 418063755U)
						{
							if (num != 417916660U)
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
							else if (codigo == "65")
							{
								this.Codigo = "65";
								this.Descricao = "Código do Banco para Débito Inválido";
							}
						}
						else if (num != 418210850U)
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
					else if (num <= 518876564U)
					{
						if (num <= 468396612U)
						{
							if (num <= 451471898U)
							{
								if (num != 434841374U)
								{
									if (num != 434988469U)
									{
										if (num == 451471898U)
										{
											if (codigo == "67")
											{
												this.Codigo = "67";
												this.Descricao = "Dados para Débito incompatível com a Identificação da Emissão do Bloqueto";
											}
										}
									}
									else if (codigo == "08")
									{
										this.Codigo = "08";
										this.Descricao = "Nosso Número Inválido";
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
							else if (num != 468249517U)
							{
								if (num == 468396612U)
								{
									if (codigo == "10")
									{
										this.Codigo = "10";
										this.Descricao = "Carteira Inválida";
									}
								}
							}
							else if (codigo == "66")
							{
								this.Codigo = "66";
								this.Descricao = "Agência/Conta/DV para Débito Inválido";
							}
						}
						else if (num <= 485321326U)
						{
							if (num != 468543707U)
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
									this.Descricao = "Forma de Cadastramento do Título Inválido";
								}
							}
							else if (codigo == "06")
							{
								this.Codigo = "06";
								this.Descricao = "Tipo Número Inscrição Cedente Inválido";
							}
						}
						else if (num <= 502098945U)
						{
							if (num != 501951850U)
							{
								if (num == 502098945U)
								{
									if (codigo == "04")
									{
										this.Codigo = "04";
										this.Descricao = "Código do Movimento não Permitido p/ Carteira";
									}
								}
							}
							else if (codigo == "12")
							{
								this.Codigo = "12";
								this.Descricao = "Tipo de Documento Inválido";
							}
						}
						else if (num != 518729469U)
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
					else if (num <= 2262910297U)
					{
						if (num <= 2212577440U)
						{
							if (num != 535654183U)
							{
								if (num != 552431802U)
								{
									if (num == 2212577440U)
									{
										if (codigo == "50")
										{
											this.Codigo = "50";
											this.Descricao = "CEP Referente a um Banco Correspondente";
										}
									}
								}
								else if (codigo == "01")
								{
									this.Codigo = "01";
									this.Descricao = "Código do Banco Inválido";
								}
							}
							else if (codigo == "02")
							{
								this.Codigo = "02";
								this.Descricao = "Código do Registro Inválido";
							}
						}
						else if (num <= 2246132678U)
						{
							if (num != 2229355059U)
							{
								if (num == 2246132678U)
								{
									if (codigo == "52")
									{
										this.Codigo = "52";
										this.Descricao = "Unidade da Federação Inválida";
									}
								}
							}
							else if (codigo == "51")
							{
								this.Codigo = "51";
								this.Descricao = "CEP incompatível com a Unidade da Federação";
							}
						}
						else if (num != 2247118416U)
						{
							if (num == 2262910297U)
							{
								if (codigo == "53")
								{
									this.Codigo = "53";
									this.Descricao = "Tipo/Número de Inscrição do Sacador/Avalista Inválidos";
								}
							}
						}
						else if (codigo == "32")
						{
							this.Codigo = "32";
							this.Descricao = "Valor do IOF Inválido";
						}
					}
					else if (num <= 2279687916U)
					{
						if (num != 2263057392U)
						{
							if (num != 2263896035U)
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
						else if (codigo == "43")
						{
							this.Codigo = "43";
							this.Descricao = "Prazo para Baixa/Devolução Inválido";
						}
					}
					else if (num <= 2280673654U)
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
				else if (num <= 2381486463U)
				{
					if (num <= 2346798392U)
					{
						if (num <= 2314375987U)
						{
							if (num <= 2313243154U)
							{
								if (num != 2297451273U)
								{
									if (num != 2297598368U)
									{
										if (num == 2313243154U)
										{
											if (codigo == "56")
											{
												this.Codigo = "56";
												this.Descricao = "Código do Banco Correspondente Não Informado";
											}
										}
									}
									else if (codigo == "25")
									{
										this.Codigo = "25";
										this.Descricao = "Data da Emissão Posterior a Data de Entrada";
									}
								}
								else if (codigo == "31")
								{
									this.Codigo = "31";
									this.Descricao = "Concessão de Desconto - Já Existe Desconto Anterior";
								}
							}
							else if (num <= 2313537344U)
							{
								if (num != 2313390249U)
								{
									if (num == 2313537344U)
									{
										if (codigo == "76")
										{
											this.Codigo = "76";
											this.Descricao = "Débito Não Agendado, Tipo/Num. Inscrição do Debitado, Inválido";
										}
									}
								}
								else if (codigo == "40")
								{
									this.Codigo = "40";
									this.Descricao = "Título com Ordem de Protesto Emitida ";
								}
							}
							else if (num != 2314228892U)
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
						else if (num <= 2330314963U)
						{
							if (num != 2330020773U)
							{
								if (num != 2330167868U)
								{
									if (num == 2330314963U)
									{
										if (codigo == "77")
										{
											this.Codigo = "77";
											this.Descricao = "Transferência para Desconto Não Permitida para a Carteira do Título";
										}
									}
								}
								else if (codigo == "47")
								{
									this.Codigo = "47";
									this.Descricao = "Endereço do Sacado Não Informado";
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
						else if (num != 2332139344U)
						{
							if (num == 2346798392U)
							{
								if (codigo == "58")
								{
									this.Codigo = "58";
									this.Descricao = "Data da Multa Inválida";
								}
							}
						}
						else if (codigo == "87")
						{
							this.Codigo = "87";
							this.Descricao = "E-mail/SMS enviado";
						}
					}
					else if (num <= 2363723106U)
					{
						if (num <= 2347784130U)
						{
							if (num != 2346945487U)
							{
								if (num != 2347092582U)
								{
									if (num == 2347784130U)
									{
										if (codigo == "34")
										{
											this.Codigo = "34";
											this.Descricao = "Valor do Abatimento Maior ou Igual ao Valor do Título";
										}
									}
								}
								else if (codigo == "74")
								{
									this.Codigo = "74";
									this.Descricao = "Débito Não Agendado - Data Vencimento Inválida";
								}
							}
							else if (codigo == "46")
							{
								this.Codigo = "46";
								this.Descricao = "Tipo/Número de Inscrição do Sacado Inválidos";
							}
						}
						else if (num <= 2348916963U)
						{
							if (num != 2347931225U)
							{
								if (num == 2348916963U)
								{
									if (codigo == "86")
									{
										this.Codigo = "86";
										this.Descricao = "Seu Número Inválido";
									}
								}
							}
							else if (codigo == "26")
							{
								this.Codigo = "26";
								this.Descricao = "Código de Juros de Mora Inválido";
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
						if (num != 2363870201U)
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
								this.Descricao = "Valor a Conceder Não Confere";
							}
						}
						else if (codigo == "75")
						{
							this.Codigo = "75";
							this.Descricao = "Débito Não Agendado, Conforme seu Pedido, Título Não Registrado";
						}
					}
					else if (num <= 2380500725U)
					{
						if (num != 2365694582U)
						{
							if (num == 2380500725U)
							{
								if (codigo == "44")
								{
									this.Codigo = "44";
									this.Descricao = "Código da Moeda Inválido";
								}
							}
						}
						else if (codigo == "85")
						{
							this.Codigo = "85";
							this.Descricao = "Título com Pagamento Vinculado";
						}
					}
					else if (num != 2380647820U)
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
					else if (codigo == "72")
					{
						this.Codigo = "72";
						this.Descricao = "Débito Não Agendado - Cedente Não Participa da Modalidade Débito Automático";
					}
				}
				else if (num <= 2449582677U)
				{
					if (num <= 2415041701U)
					{
						if (num <= 2398264082U)
						{
							if (num != 2382472201U)
							{
								if (num != 2397425439U)
								{
									if (num == 2398264082U)
									{
										if (codigo == "23")
										{
											this.Codigo = "23";
											this.Descricao = "Aceite Inválido";
										}
									}
								}
								else if (codigo == "73")
								{
									this.Codigo = "73";
									this.Descricao = "Débito Não Agendado - Código de Moeda Diferente de Real (R$)";
								}
							}
							else if (codigo == "84")
							{
								this.Codigo = "84";
								this.Descricao = "Número Autorização Inexistente";
							}
						}
						else if (num <= 2414203058U)
						{
							if (num != 2399249820U)
							{
								if (num == 2414203058U)
								{
									if (codigo == "70")
									{
										this.Codigo = "70";
										this.Descricao = "Débito Não Agendado - Sacado Não Consta do Cadastro de Autorizante";
									}
								}
							}
							else if (codigo == "83")
							{
								this.Codigo = "83";
								this.Descricao = "Limite Excedido";
							}
						}
						else if (num != 2414894606U)
						{
							if (num == 2415041701U)
							{
								if (codigo == "22")
								{
									this.Codigo = "22";
									this.Descricao = "Espécie do Título Não Permitida para a Carteira";
								}
							}
						}
						else if (codigo == "38")
						{
							this.Codigo = "38";
							this.Descricao = "Prazo para Protesto Inválido";
						}
					}
					else if (num <= 2430980677U)
					{
						if (num != 2416027439U)
						{
							if (num != 2430833582U)
							{
								if (num == 2430980677U)
								{
									if (codigo == "71")
									{
										this.Codigo = "71";
										this.Descricao = "Débito Não Agendado - Cedente Não Autorizadopelo Sacado";
									}
								}
							}
							else if (codigo == "49")
							{
								this.Codigo = "49";
								this.Descricao = "CEP Sem Praça de Cobrança (Não Localizado)";
							}
						}
						else if (codigo == "82")
						{
							this.Codigo = "82";
							this.Descricao = "Tentativas de Débito Esgotadas - Pendente";
						}
					}
					else if (num <= 2432805058U)
					{
						if (num != 2431672225U)
						{
							if (num == 2432805058U)
							{
								if (codigo == "81")
								{
									this.Codigo = "81";
									this.Descricao = "Tentativas de Débito Esgotadas - Baixado";
								}
							}
						}
						else if (codigo == "39")
						{
							this.Codigo = "39";
							this.Descricao = "Pedido de Protesto Não Permitido para o Título";
						}
					}
					else if (num != 2447611201U)
					{
						if (num == 2449582677U)
						{
							if (codigo == "80")
							{
								this.Codigo = "80";
								this.Descricao = "Data do Desconto Inválida";
							}
						}
					}
					else if (codigo == "48")
					{
						this.Codigo = "48";
						this.Descricao = "CEP Inválido";
					}
				}
				else if (num <= 2548424010U)
				{
					if (num <= 2498929796U)
					{
						if (num != 2463482396U)
						{
							if (num != 2480260015U)
							{
								if (num == 2498929796U)
								{
									if (codigo == "29")
									{
										this.Codigo = "29";
										this.Descricao = "Valor do Desconto Maior ou Igual ao Valor do Título";
									}
								}
							}
							else if (codigo == "A9")
							{
								this.Codigo = "A9";
								this.Descricao = "Não autoriza pagamento parcial";
							}
						}
						else if (codigo == "A8")
						{
							this.Codigo = "A8";
							this.Descricao = "Valor do Abatimento inválido para cancelamento";
						}
					}
					else if (num <= 2530592872U)
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
					else if (num != 2547370491U)
					{
						if (num == 2548424010U)
						{
							if (codigo == "78")
							{
								this.Codigo = "78";
								this.Descricao = "Data Inferior ou Igual ao Vencimento para Débito Automático";
							}
						}
					}
					else if (codigo == "A5")
					{
						this.Codigo = "A5";
						this.Descricao = "Registro Rejeitado – Título já Liquidado";
					}
				}
				else if (num <= 2580925729U)
				{
					if (num <= 2565201629U)
					{
						if (num != 2564148110U)
						{
							if (num == 2565201629U)
							{
								if (codigo == "79")
								{
									this.Codigo = "79";
									this.Descricao = "Data Juros de Mora Inválido";
								}
							}
						}
						else if (codigo == "A6")
						{
							this.Codigo = "A6";
							this.Descricao = "Código do Convenente Inválido ou Encerrado";
						}
					}
					else if (num != 2567026010U)
					{
						if (num == 2580925729U)
						{
							if (codigo == "A7")
							{
								this.Codigo = "A7";
								this.Descricao = "Título já se encontra na situação Pretendida";
							}
						}
					}
					else if (codigo == "89")
					{
						this.Codigo = "89";
						this.Descricao = "E-mail/SMS devolvido - endereço de e-mail ou número do celular incorreto";
					}
				}
				else if (num <= 2614480967U)
				{
					if (num != 2583803629U)
					{
						if (num == 2614480967U)
						{
							if (codigo == "A1")
							{
								this.Codigo = "A1";
								this.Descricao = "Rejeição da alteração do número controle do participante";
							}
						}
					}
					else if (codigo == "88")
					{
						this.Codigo = "88";
						this.Descricao = "E-mail Lido";
					}
				}
				else if (num != 2631258586U)
				{
					if (num == 2648036205U)
					{
						if (codigo == "A3")
						{
							this.Codigo = "A3";
							this.Descricao = "Rejeição da alteração dos dados do sacador/avalista";
						}
					}
				}
				else if (codigo == "A2")
				{
					this.Codigo = "A2";
					this.Descricao = "Rejeição da alteração dos dados do sacado";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

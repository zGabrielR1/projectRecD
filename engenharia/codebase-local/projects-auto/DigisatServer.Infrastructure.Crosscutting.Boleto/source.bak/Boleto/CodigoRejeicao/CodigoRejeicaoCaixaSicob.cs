using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000104 RID: 260
	public class CodigoRejeicaoCaixaSicob : AbstractCodigoRejeicao
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0001B77C File Offset: 0x0001997C
		public CodigoRejeicaoCaixaSicob(string codigo)
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

		// Token: 0x060003EF RID: 1007 RVA: 0x0001B7B8 File Offset: 0x000199B8
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoCaixa();
				uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
				if (num <= 2313537344U)
				{
					if (num <= 434841374U)
					{
						if (num <= 234789874U)
						{
							if (num <= 184457017U)
							{
								if (num <= 150901779U)
								{
									if (num != 134124160U)
									{
										if (num == 150901779U)
										{
											if (codigo == "95")
											{
												this.Codigo = "95";
												this.Descricao = "Quantidade de lotes do arquivo inválido";
											}
										}
									}
									else if (codigo == "94")
									{
										this.Codigo = "94";
										this.Descricao = "Quantidade de registros no lote divergente";
									}
								}
								else if (num != 167679398U)
								{
									if (num == 184457017U)
									{
										if (codigo == "97")
										{
											this.Codigo = "97";
											this.Descricao = "Quantidade de registros no arquivo inválido";
										}
									}
								}
								else if (codigo == "96")
								{
									this.Codigo = "96";
									this.Descricao = "Quantidade de lotes no arquivo divergente";
								}
							}
							else if (num <= 216585232U)
							{
								if (num != 201234636U)
								{
									if (num == 216585232U)
									{
										if (codigo == "69")
										{
											this.Codigo = "69";
											this.Descricao = "Alteração de dados inválida";
										}
									}
								}
								else if (codigo == "90")
								{
									this.Codigo = "90";
									this.Descricao = "Número seqüencial do registro inválido";
								}
							}
							else if (num != 218012255U)
							{
								if (num != 233362851U)
								{
									if (num == 234789874U)
									{
										if (codigo == "92")
										{
											this.Codigo = "92";
											this.Descricao = "Código de movimento divergente entre grupo de segmentos";
										}
									}
								}
								else if (codigo == "68")
								{
									this.Codigo = "68";
									this.Descricao = "Movimentação inválida para o título";
								}
							}
							else if (codigo == "91")
							{
								this.Codigo = "91";
								this.Descricao = "Erro na seqüência de segmento do registro detalhe";
							}
						}
						else if (num <= 384361422U)
						{
							if (num <= 335455588U)
							{
								if (num != 251567493U)
								{
									if (num == 335455588U)
									{
										if (codigo == "98")
										{
											this.Codigo = "98";
											this.Descricao = "Quantidade de registros no arquivo divergente";
										}
									}
								}
								else if (codigo == "93")
								{
									this.Codigo = "93";
									this.Descricao = "Quantidade de registros no lote inválido";
								}
							}
							else if (num != 350806184U)
							{
								if (num != 367583803U)
								{
									if (num == 384361422U)
									{
										if (codigo == "63")
										{
											this.Codigo = "63";
											this.Descricao = "Entrada para título já cadastrado";
										}
									}
								}
								else if (codigo == "60")
								{
									this.Codigo = "60";
									this.Descricao = "Movimento para título não cadastrado. Erro genérico para as situações: - “Cedente não cadastrado” ou - “Agência Cedente não cadastrada ou desativada”";
								}
							}
							else if (codigo == "61")
							{
								this.Codigo = "61";
								this.Descricao = "Agência cobradora inválida";
							}
						}
						else if (num <= 401286136U)
						{
							if (num != 401139041U)
							{
								if (num == 401286136U)
								{
									if (codigo == "14")
									{
										this.Codigo = "14";
										this.Descricao = "Identificação da distribuição do bloqueto inválida";
									}
								}
							}
							else if (codigo == "62")
							{
								this.Codigo = "62";
								this.Descricao = "Tipo de impressão inválido";
							}
						}
						else if (num != 418063755U)
						{
							if (num != 418210850U)
							{
								if (num == 434841374U)
								{
									if (codigo == "16")
									{
										this.Codigo = "16";
										this.Descricao = "Data de vencimento inválida";
									}
								}
							}
							else if (codigo == "09")
							{
								this.Codigo = "09";
								this.Descricao = "Nosso número duplicado";
							}
						}
						else if (codigo == "15")
						{
							this.Codigo = "15";
							this.Descricao = "Características da cobrança incompatíveis";
						}
					}
					else if (num <= 535654183U)
					{
						if (num <= 485174231U)
						{
							if (num <= 451766088U)
							{
								if (num != 434988469U)
								{
									if (num == 451766088U)
									{
										if (codigo == "07")
										{
											this.Codigo = "07";
											this.Descricao = "Agência/Conta/DV inválido";
										}
									}
								}
								else if (codigo == "08")
								{
									this.Codigo = "08";
									this.Descricao = "Nosso número inválido";
								}
							}
							else if (num != 468396612U)
							{
								if (num != 468543707U)
								{
									if (num == 485174231U)
									{
										if (codigo == "11")
										{
											this.Codigo = "11";
											this.Descricao = "Forma de cadastramento do título inválido";
										}
									}
								}
								else if (codigo == "06")
								{
									this.Codigo = "06";
									this.Descricao = "Tipo/número de inscrição do cedente inválido";
								}
							}
							else if (codigo == "10")
							{
								this.Codigo = "10";
								this.Descricao = "Carteira inválida";
							}
						}
						else if (num <= 501951850U)
						{
							if (num != 485321326U)
							{
								if (num == 501951850U)
								{
									if (codigo == "12")
									{
										this.Codigo = "12";
										this.Descricao = "Tipo de documento inválido";
									}
								}
							}
							else if (codigo == "05")
							{
								this.Codigo = "05";
								this.Descricao = "Código de movimento inválido";
							}
						}
						else if (num != 518729469U)
						{
							if (num != 518876564U)
							{
								if (num == 535654183U)
								{
									if (codigo == "02")
									{
										this.Codigo = "02";
										this.Descricao = "Código do registro inválido";
									}
								}
							}
							else if (codigo == "03")
							{
								this.Codigo = "03";
								this.Descricao = "Código do segmento inválido";
							}
						}
						else if (codigo == "13")
						{
							this.Codigo = "13";
							this.Descricao = "Identificação da emissão do bloqueto inválida";
						}
					}
					else if (num <= 2263057392U)
					{
						if (num <= 2246132678U)
						{
							if (num != 552431802U)
							{
								if (num == 2246132678U)
								{
									if (codigo == "52")
									{
										this.Codigo = "52";
										this.Descricao = "Unidade da federação inválida";
									}
								}
							}
							else if (codigo == "01")
							{
								this.Codigo = "01";
								this.Descricao = "Código do banco inválido";
							}
						}
						else if (num != 2247118416U)
						{
							if (num != 2262910297U)
							{
								if (num == 2263057392U)
								{
									if (codigo == "43")
									{
										this.Codigo = "43";
										this.Descricao = "Prazo para baixa/devolução inválido";
									}
								}
							}
							else if (codigo == "53")
							{
								this.Codigo = "53";
								this.Descricao = "Tipo/número de inscrição do sacador/avalista inválido";
							}
						}
						else if (codigo == "32")
						{
							this.Codigo = "32";
							this.Descricao = "Valor do IOF inválido";
						}
					}
					else if (num <= 2279835011U)
					{
						if (num != 2263896035U)
						{
							if (num == 2279835011U)
							{
								if (codigo == "42")
								{
									this.Codigo = "42";
									this.Descricao = "Código para baixa/devolução inválido";
								}
							}
						}
						else if (codigo == "33")
						{
							this.Codigo = "33";
							this.Descricao = "Valor do abatimento inválido";
						}
					}
					else if (num != 2280673654U)
					{
						if (num != 2313390249U)
						{
							if (num == 2313537344U)
							{
								if (codigo == "76")
								{
									this.Codigo = "76";
									this.Descricao = "Nome do banco inválido";
								}
							}
						}
						else if (codigo == "40")
						{
							this.Codigo = "40";
							this.Descricao = "Título com ordem de protesto emitida";
						}
					}
					else if (codigo == "30")
					{
						this.Codigo = "30";
						this.Descricao = "Desconto a conceder não confere";
					}
				}
				else if (num <= 2380647820U)
				{
					if (num <= 2346945487U)
					{
						if (num <= 2330314963U)
						{
							if (num <= 2330020773U)
							{
								if (num != 2314375987U)
								{
									if (num == 2330020773U)
									{
										if (codigo == "57")
										{
											this.Codigo = "57";
											this.Descricao = "Código da multa inválido";
										}
									}
								}
								else if (codigo == "24")
								{
									this.Codigo = "24";
									this.Descricao = "Data da emissão inválida";
								}
							}
							else if (num != 2330167868U)
							{
								if (num == 2330314963U)
								{
									if (codigo == "77")
									{
										this.Codigo = "77";
										this.Descricao = "Código da remessa inválido";
									}
								}
							}
							else if (codigo == "47")
							{
								this.Codigo = "47";
								this.Descricao = "Endereço do sacado não informado";
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
										this.Descricao = "Valor/Taxa de juros de mora inválido";
									}
								}
							}
							else if (codigo == "37")
							{
								this.Codigo = "37";
								this.Descricao = "Código para protesto inválido";
							}
						}
						else if (num != 2332139344U)
						{
							if (num != 2346798392U)
							{
								if (num == 2346945487U)
								{
									if (codigo == "46")
									{
										this.Codigo = "46";
										this.Descricao = "Tipo/número de inscrição do sacado inválido";
									}
								}
							}
							else if (codigo == "58")
							{
								this.Codigo = "58";
								this.Descricao = "Data da multa inválida";
							}
						}
						else if (codigo == "87")
						{
							this.Codigo = "87";
							this.Descricao = "Número da remessa inválido";
						}
					}
					else if (num <= 2363723106U)
					{
						if (num <= 2347931225U)
						{
							if (num != 2347092582U)
							{
								if (num == 2347931225U)
								{
									if (codigo == "26")
									{
										this.Codigo = "26";
										this.Descricao = "Código de juros de mora inválido";
									}
								}
							}
							else if (codigo == "74")
							{
								this.Codigo = "74";
								this.Descricao = "Cedente não pertence a cobrança eletrônica/apelido não confere com cedente";
							}
						}
						else if (num != 2348916963U)
						{
							if (num != 2363576011U)
							{
								if (num == 2363723106U)
								{
									if (codigo == "45")
									{
										this.Codigo = "45";
										this.Descricao = "Nome do sacado não informado";
									}
								}
							}
							else if (codigo == "59")
							{
								this.Codigo = "59";
								this.Descricao = "Valor/Percentual da multa inválido";
							}
						}
						else if (codigo == "86")
						{
							this.Codigo = "86";
							this.Descricao = "Forma de lançamento inválido";
						}
					}
					else if (num <= 2364708844U)
					{
						if (num != 2363870201U)
						{
							if (num == 2364708844U)
							{
								if (codigo == "21")
								{
									this.Codigo = "21";
									this.Descricao = "Espécie do título inválida";
								}
							}
						}
						else if (codigo == "75")
						{
							this.Codigo = "75";
							this.Descricao = "Nome da empresa inválido";
						}
					}
					else if (num != 2365694582U)
					{
						if (num != 2380500725U)
						{
							if (num == 2380647820U)
							{
								if (codigo == "72")
								{
									this.Codigo = "72";
									this.Descricao = "Lote de serviço inválido";
								}
							}
						}
						else if (codigo == "44")
						{
							this.Codigo = "44";
							this.Descricao = "Código da moeda inválido";
						}
					}
					else if (codigo == "85")
					{
						this.Codigo = "85";
						this.Descricao = "Tipo de serviço inválido";
					}
				}
				else if (num <= 2430980677U)
				{
					if (num <= 2399249820U)
					{
						if (num <= 2382472201U)
						{
							if (num != 2381486463U)
							{
								if (num == 2382472201U)
								{
									if (codigo == "84")
									{
										this.Codigo = "84";
										this.Descricao = "Tipo de operação inválido";
									}
								}
							}
							else if (codigo == "20")
							{
								this.Codigo = "20";
								this.Descricao = "Valor do título inválido";
							}
						}
						else if (num != 2397425439U)
						{
							if (num != 2398264082U)
							{
								if (num == 2399249820U)
								{
									if (codigo == "83")
									{
										this.Codigo = "83";
										this.Descricao = "Tipo/número de inscrição da empresa inválido";
									}
								}
							}
							else if (codigo == "23")
							{
								this.Codigo = "23";
								this.Descricao = "Aceite inválido";
							}
						}
						else if (codigo == "73")
						{
							this.Codigo = "73";
							this.Descricao = "Código do cedente inválido";
						}
					}
					else if (num <= 2414894606U)
					{
						if (num != 2414203058U)
						{
							if (num == 2414894606U)
							{
								if (codigo == "38")
								{
									this.Codigo = "38";
									this.Descricao = "Prazo para protesto inválido";
								}
							}
						}
						else if (codigo == "70")
						{
							this.Codigo = "70";
							this.Descricao = "Apelido do cliente não cadastrado";
						}
					}
					else if (num != 2416027439U)
					{
						if (num != 2430833582U)
						{
							if (num == 2430980677U)
							{
								if (codigo == "71")
								{
									this.Codigo = "71";
									this.Descricao = "Erro na composição do arquivo";
								}
							}
						}
						else if (codigo == "49")
						{
							this.Codigo = "49";
							this.Descricao = "CEP sem praça de cobrança (não localizado)";
						}
					}
					else if (codigo == "82")
					{
						this.Codigo = "82";
						this.Descricao = "Literal 'REMESSA-TESTE' obrigatório para fase de testes";
					}
				}
				else if (num <= 2515707415U)
				{
					if (num <= 2447611201U)
					{
						if (num != 2432805058U)
						{
							if (num == 2447611201U)
							{
								if (codigo == "48")
								{
									this.Codigo = "48";
									this.Descricao = "CEP inválido";
								}
							}
						}
						else if (codigo == "81")
						{
							this.Codigo = "81";
							this.Descricao = "Literal 'REMESSA-TESTE' válida somente para fase de testes";
						}
					}
					else if (num != 2449582677U)
					{
						if (num != 2498929796U)
						{
							if (num == 2515707415U)
							{
								if (codigo == "28")
								{
									this.Codigo = "28";
									this.Descricao = "Código do desconto inválido";
								}
							}
						}
						else if (codigo == "29")
						{
							this.Codigo = "29";
							this.Descricao = "Valor do desconto maior ou igual ao valor do título";
						}
					}
					else if (codigo == "80")
					{
						this.Codigo = "80";
						this.Descricao = "Número da versão do Layout do arquivo/lote inválido";
					}
				}
				else if (num <= 2548424010U)
				{
					if (num != 2530592872U)
					{
						if (num == 2548424010U)
						{
							if (codigo == "78")
							{
								this.Codigo = "78";
								this.Descricao = "Data/Hora de geração do arquivo inválida";
							}
						}
					}
					else if (codigo == "A4")
					{
						this.Codigo = "A4";
						this.Descricao = "Sacado DDA";
					}
				}
				else if (num != 2565201629U)
				{
					if (num != 2567026010U)
					{
						if (num == 2583803629U)
						{
							if (codigo == "88")
							{
								this.Codigo = "88";
								this.Descricao = "Número da remessa menor/igual que da remessa anterior";
							}
						}
					}
					else if (codigo == "89")
					{
						this.Codigo = "89";
						this.Descricao = "Lote de serviço divergente";
					}
				}
				else if (codigo == "79")
				{
					this.Codigo = "79";
					this.Descricao = "Número seqüencial do arquivo inválido";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

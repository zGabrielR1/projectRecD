using System;
using DigisatServer.Infrastructure.Crosscutting.Boleto.Banco;

namespace DigisatServer.Infrastructure.Crosscutting.Boleto.Boleto.CodigoRejeicao
{
	// Token: 0x02000107 RID: 263
	public class CodigoRejeicaoSicoob : AbstractCodigoRejeicao
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public CodigoRejeicaoSicoob(string codigo)
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

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001F608 File Offset: 0x0001D808
		private void Carregar(string codigo)
		{
			try
			{
				this.Banco = new BancoSicoob();
				uint num = global::__PrivateImplementationDetails.ComputeStringHash(codigo);
				if (num <= 2297598368U)
				{
					if (num <= 468543707U)
					{
						if (num <= 418063755U)
						{
							if (num <= 367583803U)
							{
								if (num != 334175660U)
								{
									if (num != 350806184U)
									{
										if (num == 367583803U)
										{
											if (codigo == "60")
											{
												this.Codigo = "60";
												this.Descricao = "Movimento para título não cadastrado";
											}
										}
									}
									else if (codigo == "61")
									{
										this.Codigo = "61";
										this.Descricao = "Alteração da cooperativa crédito/agência cobradora/DV inválida";
									}
								}
								else if (codigo == "18")
								{
									this.Codigo = "18";
									this.Descricao = "Vencimento fora do prazo de operação";
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
											this.Descricao = "Tipo de impressão inválido";
										}
									}
								}
								else if (codigo == "63")
								{
									this.Codigo = "63";
									this.Descricao = "Entrada para título já cadastrado";
								}
							}
							else if (num != 401286136U)
							{
								if (num == 418063755U)
								{
									if (codigo == "15")
									{
										this.Codigo = "15";
										this.Descricao = "Características da cobrança incompatíveis";
									}
								}
							}
							else if (codigo == "14")
							{
								this.Codigo = "14";
								this.Descricao = "Identificação da distribuição do bloqueto inválida";
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
											this.Descricao = "Número da linha inválido";
										}
									}
								}
								else if (codigo == "09")
								{
									this.Codigo = "09";
									this.Descricao = "Nosso número duplicado";
								}
							}
							else if (num != 434841374U)
							{
								if (num == 434988469U)
								{
									if (codigo == "08")
									{
										this.Codigo = "08";
										this.Descricao = "Nosso número inválido";
									}
								}
							}
							else if (codigo == "16")
							{
								this.Codigo = "16";
								this.Descricao = "Data de vencimento inválida";
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
										this.Descricao = "Cooperativa crédito/agência/conta/DV inválido";
									}
								}
							}
							else if (codigo == "17")
							{
								this.Codigo = "17";
								this.Descricao = "Data de vencimento anterior a data de emissão";
							}
						}
						else if (num != 468396612U)
						{
							if (num == 468543707U)
							{
								if (codigo == "06")
								{
									this.Codigo = "06";
									this.Descricao = "Tipo/número de inscrição do cedente inválidos";
								}
							}
						}
						else if (codigo == "10")
						{
							this.Codigo = "10";
							this.Descricao = "Carteira inválida";
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
											this.Descricao = "Código de movimento inválido";
										}
									}
								}
								else if (codigo == "11")
								{
									this.Codigo = "11";
									this.Descricao = "Forma de cadastramento do título inválido";
								}
							}
							else if (num != 501951850U)
							{
								if (num == 502098945U)
								{
									if (codigo == "04")
									{
										this.Codigo = "04";
										this.Descricao = "Código de movimento não permitido para carteira";
									}
								}
							}
							else if (codigo == "12")
							{
								this.Codigo = "12";
								this.Descricao = "Tipo de documento inválido";
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
										this.Descricao = "Código do segmento inválido";
									}
								}
							}
							else if (codigo == "13")
							{
								this.Codigo = "13";
								this.Descricao = "Identificação da emissão do bloqueto inválida";
							}
						}
						else if (num != 535654183U)
						{
							if (num == 552431802U)
							{
								if (codigo == "01")
								{
									this.Codigo = "01";
									this.Descricao = "Código do banco inválido";
								}
							}
						}
						else if (codigo == "02")
						{
							this.Codigo = "02";
							this.Descricao = "Código do registro detalhe inválido";
						}
					}
					else if (num <= 2280673654U)
					{
						if (num <= 2263896035U)
						{
							if (num != 2262910297U)
							{
								if (num == 2263896035U)
								{
									if (codigo == "33")
									{
										this.Codigo = "33";
										this.Descricao = "Valor do abatimento inválido";
									}
								}
							}
							else if (codigo == "53")
							{
								this.Codigo = "53";
								this.Descricao = "Tipo/número de inscrição do sacador/avalista inválidos";
							}
						}
						else if (num != 2279687916U)
						{
							if (num == 2280673654U)
							{
								if (codigo == "30")
								{
									this.Codigo = "30";
									this.Descricao = "Desconto a conceder não confere";
								}
							}
						}
						else if (codigo == "54")
						{
							this.Codigo = "54";
							this.Descricao = "Sacador/avalista não informado";
						}
					}
					else if (num <= 2296612630U)
					{
						if (num != 2296465535U)
						{
							if (num == 2296612630U)
							{
								if (codigo == "41")
								{
									this.Codigo = "41";
									this.Descricao = "Pedido de cancelamento/sustação para títulos sem instrução de protesto";
								}
							}
						}
						else if (codigo == "55")
						{
							this.Codigo = "55";
							this.Descricao = "Nosso número no banco correspondente não informado";
						}
					}
					else if (num != 2297451273U)
					{
						if (num == 2297598368U)
						{
							if (codigo == "25")
							{
								this.Codigo = "25";
								this.Descricao = "Data da emissão posterior a data de entrada";
							}
						}
					}
					else if (codigo == "31")
					{
						this.Codigo = "31";
						this.Descricao = "Concessão de desconto - já existe descontoanterior";
					}
				}
				else if (num <= 2363723106U)
				{
					if (num <= 2331006511U)
					{
						if (num <= 2314228892U)
						{
							if (num != 2313243154U)
							{
								if (num != 2313390249U)
								{
									if (num == 2314228892U)
									{
										if (codigo == "36")
										{
											this.Codigo = "36";
											this.Descricao = "Concessão de abatimento - já existe abatimento anterior";
										}
									}
								}
								else if (codigo == "40")
								{
									this.Codigo = "40";
									this.Descricao = "Título com ordem de protesto emitida";
								}
							}
							else if (codigo == "56")
							{
								this.Codigo = "56";
								this.Descricao = "Código do banco correspondente não informado";
							}
						}
						else if (num <= 2330020773U)
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
							if (num == 2331006511U)
							{
								if (codigo == "37")
								{
									this.Codigo = "37";
									this.Descricao = "Código para protesto inválido";
								}
							}
						}
						else if (codigo == "47")
						{
							this.Codigo = "47";
							this.Descricao = "Endereço do sacado não informado";
						}
					}
					else if (num <= 2347784130U)
					{
						if (num <= 2346798392U)
						{
							if (num != 2331153606U)
							{
								if (num == 2346798392U)
								{
									if (codigo == "58")
									{
										this.Codigo = "58";
										this.Descricao = "Data da multa inválida";
									}
								}
							}
							else if (codigo == "27")
							{
								this.Codigo = "27";
								this.Descricao = "Valor/taxa de juros de mora inválido";
							}
						}
						else if (num != 2346945487U)
						{
							if (num == 2347784130U)
							{
								if (codigo == "34")
								{
									this.Codigo = "34";
									this.Descricao = "Valor do abatimento maior ou igual ao valor do título";
								}
							}
						}
						else if (codigo == "46")
						{
							this.Codigo = "46";
							this.Descricao = "Tipo/número de inscrição do sacado inválidos";
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
									this.Descricao = "Seu número inválido";
								}
							}
						}
						else if (codigo == "26")
						{
							this.Codigo = "26";
							this.Descricao = "Código de juros de mora inválido";
						}
					}
					else if (num != 2363576011U)
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
						this.Descricao = "Valor/percentual da multa inválido";
					}
				}
				else if (num <= 2414894606U)
				{
					if (num <= 2380500725U)
					{
						if (num <= 2364708844U)
						{
							if (num != 2364561749U)
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
							else if (codigo == "35")
							{
								this.Codigo = "35";
								this.Descricao = "Valor a conceder não confere";
							}
						}
						else if (num != 2365694582U)
						{
							if (num == 2380500725U)
							{
								if (codigo == "44")
								{
									this.Codigo = "44";
									this.Descricao = "Código da moeda inválido";
								}
							}
						}
						else if (codigo == "85")
						{
							this.Codigo = "85";
							this.Descricao = "Título com pagamento vinculado";
						}
					}
					else if (num <= 2382472201U)
					{
						if (num != 2381486463U)
						{
							if (num == 2382472201U)
							{
								if (codigo == "84")
								{
									this.Codigo = "84";
									this.Descricao = "Número autorização inexistente";
								}
							}
						}
						else if (codigo == "20")
						{
							this.Codigo = "20";
							this.Descricao = "Valor do título inválido";
						}
					}
					else if (num != 2398264082U)
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
					else if (codigo == "23")
					{
						this.Codigo = "23";
						this.Descricao = "Aceite inválido";
					}
				}
				else if (num <= 2449582677U)
				{
					if (num <= 2431672225U)
					{
						if (num != 2415041701U)
						{
							if (num == 2431672225U)
							{
								if (codigo == "39")
								{
									this.Codigo = "39";
									this.Descricao = "Pedido de protesto não permitido para o título";
								}
							}
						}
						else if (codigo == "22")
						{
							this.Codigo = "22";
							this.Descricao = "Espécie do título não permitida para a carteira";
						}
					}
					else if (num != 2447611201U)
					{
						if (num == 2449582677U)
						{
							if (codigo == "80")
							{
								this.Codigo = "80";
								this.Descricao = "Data do desconto inválida";
							}
						}
					}
					else if (codigo == "48")
					{
						this.Codigo = "48";
						this.Descricao = "CEP inválido";
					}
				}
				else if (num <= 2515707415U)
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
				else if (num != 2530592872U)
				{
					if (num == 2565201629U)
					{
						if (codigo == "79")
						{
							this.Codigo = "79";
							this.Descricao = "Data juros de mora inválida";
						}
					}
				}
				else if (codigo == "A4")
				{
					this.Codigo = "A4";
					this.Descricao = "Sacado DDA";
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}
	}
}

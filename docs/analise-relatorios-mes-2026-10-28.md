# Divergência dos três relatórios "Movimentações por mês" de 28/10/2026

Os três PDFs enviados por Giovani Monay (SERVIDOR, CANGURU e ESCRITORIO) foram emitidos em 28/10/2026, para a empresa GATA MODA INTIMA, ano 2026, origem "Nota fiscal de consumidor eletrônica" e situação "Concluído". São idênticas 51 das 60 células (5 vendedores × 12 meses); nove divergem. O padrão é compatível com o defeito de concorrência identificado no build analisado de `ObterTotalMovimentacoesPorMesPorVendedor`. Os PDFs não comprovam igualdade dos dados, destino da consulta ou identidade do build remoto; a causa no cliente continua pendente de confirmação. Veja o [relato consolidado e procedimento de validação](consolidacao-re-2026-11-03.md).

O relatório do SERVIDOR não pode ser adotado automaticamente como referência. Em julho e agosto ele é o menor dos três; o destino efetivo das consultas precisa ser confirmado.

## Totais por mês

| Mês | SERVIDOR | CANGURU | ESCRITORIO | Maior valor observado |
|---|---:|---:|---:|---:|
| Janeiro | 106.670,14 | 106.670,14 | 106.537,21 | 106.670,14 |
| Fevereiro | 88.971,59 | 88.971,59 | 88.971,59 | 88.971,59 |
| Março | 135.532,19 | 135.532,19 | 135.532,19 | 135.532,19 |
| Abril | 194.728,17 | 194.728,17 | 194.728,17 | 194.728,17 |
| Maio | 228.260,29 | 226.236,95 | 228.260,29 | 228.260,29 |
| Junho | 227.941,21 | 224.541,50 | 227.941,21 | 227.941,21 |
| Julho | 229.043,64 | 231.891,70 | 229.686,62 | 231.891,70 |
| Agosto | 166.062,25 | 167.377,71 | 167.377,71 | 167.377,71 |
| Setembro | 170.356,74 | 170.356,74 | 169.053,25 | 170.356,74 |
| Outubro | 127.725,34 | 127.725,34 | 126.351,84 | 127.725,34 |
| **Total** | **1.675.291,56** | **1.674.032,03** | **1.674.440,08** | **1.679.455,08** |

Novembro e dezembro são zero nas três emissões. Outubro aparece na tabela apenas para completar a leitura; o próprio cliente pediu que ele fosse desconsiderado por ser o mês em andamento. As divergências existem fora de outubro, em janeiro, maio, junho, julho, agosto e setembro.

## Células divergentes

| Vendedor | Mês | SERVIDOR | CANGURU | ESCRITORIO | Maior valor | Quem ficou abaixo |
|---|---|---:|---:|---:|---:|---|
| VENDEDORA 1 | Maio | 89.076,71 | 87.053,37 | 89.076,71 | 89.076,71 | CANGURU −2.023,34 |
| VENDEDORA 1 | Junho | 90.680,90 | 87.281,19 | 90.680,90 | 90.680,90 | CANGURU −3.399,71 |
| VENDEDORA 1 | Julho | 94.043,17 | 94.043,17 | 92.160,81 | 94.043,17 | ESCRITORIO −1.882,36 |
| VENDEDORA 2 | Julho | 81.358,46 | 84.206,52 | 84.206,52 | 84.206,52 | SERVIDOR −2.848,06 |
| VENDEDORA 3 | Janeiro | 34.072,07 | 34.072,07 | 33.939,14 | 34.072,07 | ESCRITORIO −132,93 |
| VENDEDORA 3 | Agosto | 37.308,82 | 38.624,28 | 38.624,28 | 38.624,28 | SERVIDOR −1.315,46 |
| VENDEDORA 3 | Outubro | 20.653,32 | 20.653,32 | 19.279,82 | 20.653,32 | ESCRITORIO −1.373,50 |
| VENDEDORA 4 | Julho | 5.493,45 | 5.493,45 | 5.170,73 | 5.493,45 | ESCRITORIO −322,72 |
| VENDEDORA 4 | Setembro | 38.964,28 | 38.964,28 | 37.660,79 | 38.964,28 | ESCRITORIO −1.303,49 |

## Diferença em relação à soma dos máximos observados

| Relatório | Total impresso | Total consolidado | Falta | Onde |
|---|---:|---:|---:|---|
| SERVIDOR | 1.675.291,56 | 1.679.455,08 | 4.163,52 | VENDEDORA 2 Julho −2.848,06; VENDEDORA 3 Agosto −1.315,46 |
| CANGURU | 1.674.032,03 | 1.679.455,08 | 5.423,05 | VENDEDORA 1 Maio −2.023,34; VENDEDORA 1 Junho −3.399,71 |
| ESCRITORIO | 1.674.440,08 | 1.679.455,08 | 5.015,00 | VENDEDORA 1 Julho −1.882,36; VENDEDORA 3 Janeiro −132,93; VENDEDORA 3 Outubro −1.373,50; VENDEDORA 4 Julho −322,72; VENDEDORA 4 Setembro −1.303,49 |

Os três relatórios ficam entre 0,25% e 0,32% abaixo da soma dos máximos observados. Essa comparação não demonstra perda em relação a um total correto independente; toda célula é, por definição, menor ou igual ao máximo observado.

## Hipótese de defeito no cálculo

Em [ObterTotalMovimentacoesPorMesPorVendedor](../engenharia/sincronizacao-2026-10-30/fontes/MovimentacaoSaidaRelatorioAppService.cs:148), o serviço lê as movimentações do ano em páginas de 500, soma cada página em paralelo em dicionários locais por thread e depois mescla os resultados no dicionário global:

```csharp
acumuladoGlobal.AddOrUpdate(chave, parcial, (key, existing) => {
    for (int i = 0; i < 12; i++)
        existing[i] += parcial[i];
    return existing;
});
```

O `ConcurrentDictionary` protege o mapa, mas não a soma dentro do array `decimal[12]` compartilhado. Duas threads podem interferir na soma; o callback também pode ser reexecutado. O ensaio citado demonstra resultados menores para sua entrada positiva, mas não estabelece que qualquer execução real só possa subestimar o total.

O que os anexos mostram encaixa nesse mecanismo:

- Meses fechados mudam de um relatório para outro; ausência de alterações retroativas ainda não foi verificada na base.
- Os meses afetados são diferentes em cada máquina; a máquina mais baixa muda de mês (SERVIDOR em julho e agosto, CANGURU em maio e junho, ESCRITORIO em janeiro, julho, setembro e outubro).
- A comparação com o máximo descreve a divergência, mas não demonstra sua direção em relação ao total correto.
- A ordem de grandeza, isoladamente, não identifica a contribuição de uma thread ou página.
- O relatório do SERVIDOR é o menor em julho e agosto. Isso não descarta diferenças entre bases, filtros efetivos ou endpoints.
- Caso as consultas atinjam o mesmo servidor, haverá concorrência no processo; isso ainda exige confirmação do destino e dos horários de execução.

O campo "Acumulado" não confirma nada por si: ele é um campo calculado do próprio relatório, definido como `[Janeiro] + [Fevereiro] + ... + [Dezembro]` ([layout](../engenharia/codigo-descompilado/Modulo.Relatorios/Reports/Movimentacao/Saida/TotalMovimentacoesPorMesPorVendedor.Designer.cs:719)). A linha "Totais:" soma as mesmas células. Os dois herdam a perda dos meses.

A conferência de setembro enviada em 26/10 (R$ 170.356,74 em itens e pagamentos) coincide com o maior valor observado em setembro. O ESCRITORIO fica R$ 1.303,49 abaixo; confirmar equivalência de filtros e entrada é necessário para atribuir a diferença ao cálculo.

## Qual valor usar

Não foi estabelecido qual emissão está correta. Somando o maior valor observado de cada célula, obtém-se **R$ 1.679.455,08** como comparação entre PDFs, não como total oficial:

| Vendedor | SERVIDOR | CANGURU | ESCRITORIO | Consolidado |
|---|---:|---:|---:|---:|
| Não informado | 143,43 | 143,43 | 143,43 | 143,43 |
| VENDEDORA 1 | 603.240,76 | 597.817,71 | 601.358,40 | 603.240,76 |
| VENDEDORA 2 | 677.900,96 | 680.749,02 | 680.749,02 | 680.749,02 |
| VENDEDORA 3 | 330.883,45 | 332.198,91 | 330.692,48 | 332.198,91 |
| VENDEDORA 4 | 63.122,96 | 63.122,96 | 61.496,75 | 63.122,96 |
| **Total** | **1.675.291,56** | **1.674.032,03** | **1.674.440,08** | **1.679.455,08** |

Esse número só seria um piso sob a hipótese de perdas sobre a mesma entrada positiva, ainda não comprovada no cliente. O valor exato exige cálculo determinístico sobre entrada congelada e filtros equivalentes. Os rótulos “Falta” e “Consolidado” nas tabelas designam apenas diferenças e máximos observados.

## Correção e validação

1. Publicar um novo array em cada atualização (`novo[i] = existing[i] + parcial[i]`) ou guardar as partições e reduzi-las em série depois do paralelismo. O ensaio em [repro-monthly-array-race.cs](../engenharia/sincronizacao-2026-10-30/repro-monthly-array-race.cs) acertou 40 de 40 execuções com essa correção; o padrão atual acertou 1 de 40.
2. Repetir a auditoria nos outros totalizadores que usam o mesmo `AddOrUpdate` com array, entre eles `ObterTotalMovimentacoesPorMesPorTipo` e `ObterTotalMovimentadoPorVendedor`, no mesmo arquivo.
3. Homologar com entrada congelada, graus de paralelismo 1, 2, 3 e 8, e comparação contra soma serial, incluindo documentos com mais de um vendedor e cancelamentos.
4. Enquanto não houver build corrigido, gerar o mesmo relatório várias vezes e comparar as células é a forma de detectar a perda; a repetição da sincronização não altera esse defeito.

## Fontes

- [Método mensal por vendedor](../engenharia/sincronizacao-2026-10-30/fontes/MovimentacaoSaidaRelatorioAppService.cs:148)
- [Layout e campo acumulado](../engenharia/codigo-descompilado/Modulo.Relatorios/Reports/Movimentacao/Saida/TotalMovimentacoesPorMesPorVendedor.Designer.cs:719)
- [Diagnóstico de sincronização e reprodução](../docs/diagnostico-sincronizacao-e-mensal.md)
- PDFs comparados: `docs/Movimentações por mês SERVIDOR.pdf`, `docs/Movimentações por mês.pdf CANGURU.pdf` e `docs/Movimentações por mês.pdf ESCRITORIO.pdf`

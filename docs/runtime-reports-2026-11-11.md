# Verificação de comportamento dos relatórios — 11/11/2026

Continuação: [rateio a partir dos itens](runtime-allocation-2026-11-11.md) executa também `ObterRateiosPorVendedor` e a resolução de nomes. Os fatores manuais desta primeira rodada continuam descritos abaixo para preservar sua fronteira original.

Executamos métodos reais do DLL instalado `DigisatServer.Application.Main.dll`, SHA-256 `f27cb00fdbee2851ad9119a32a435943c58c072ca102fe88ef0e3f001938cd9d`, MVID `b30a6fa6-e83d-4926-b4d3-d4d1fc4370e9`. O processo separado usa CLR `4.0.30319.42000` de .NET Framework no Windows, em 64 bits. Não é uma tradução de CIL, recompilação de C# do fornecedor ou modelo Python.

O harness não instancia o serviço de relatórios nem chama consultas. Cria DTOs sintéticos, invoca o helper privado estático de rateio e os três callbacks originais de soma por delegates. Closures são alocadas sem construtores e recebem somente o `KeyValuePair<string, decimal[]>` sintético. O serviço não tem inicializador estático; isso é verificado antes dos ensaios. Não são carregados arquivos de configuração do SuiteG6 nem construídos MongoClient/repositórios.

## Resultado observado

31 assertions diagnósticas concluíram com sucesso. As assertions verificam identidades, mutação e controles; isso não significa que os cálculos originais sejam corretos. Houve 240 ensaios de soma: 120 com callback original e 120 com controle que clona a entrada antes de chamar o mesmo callback.

| Callback original | Token atual | Serial: divergentes / ensaios | Paralelo: divergentes / ensaios | Controle clone: divergentes / ensaios |
| --- | --- | --- | --- | --- |
| Mensal por vendedor | `0x0600330e` | 0 / 10 | 30 / 30 | 0 / 40 |
| Total por vendedor | `0x060033eb` | 0 / 10 | 30 / 30 | 0 / 40 |
| Mensal por tipo | `0x06003472` | 0 / 10 | 30 / 30 | 0 / 40 |

Cada ensaio adiciona 10.000 parciais de R$ 1,01 em cada uma de 12 posições, para uma única chave. A expectativa independente é **R$ 10.100,00 por posição**. Graus máximos de paralelismo: 1, 2, 3 e 8; dez ensaios por grau/variante. Divergência significa que pelo menos uma posição difere dessa expectativa. Os valores mínimo/máximo de cada grupo estão nos resultados preservados.

Uma chamada direta também confirmou nos três callbacks que o array inicial de 10 é alterado para 12 e devolvido pela mesma referência. Sob concorrência, os 90 ensaios originais com grau maior que um divergiram; os 120 controles clone ficaram exatos. Esses números descrevem esta execução e não uma frequência garantida em outros computadores ou entradas.

O controle clone executa `callback(key, existing.Clone())` no harness. Não modifica o DLL nem constitui correção instalada. Demonstra que a estratégia de não modificar o array publicado remove a divergência desta fixture; não homologa toda a rotina do fornecedor.

## Rateio real: centavo residual observado

Executado `RatearPorVendedor`, token `0x06001205`, com DTO de espécie de R$ 10,00 e três fatores iguais a `1m / 3m`:

- Resultado efetivo: **R$ 3,33 / R$ 3,33 / R$ 3,34**.
- Com somente um fator `1m / 3m`: **R$ 3,33**.
- Uma espécie de R$ 0,01 em dois fatores de 0,5 produz **R$ 0,01 / R$ 0,00**, conservando o centavo.
- R$ -10,00 com dois fatores de 0,5 produz **R$ -5,00 / R$ -5,00**; lista vazia de fatores produz zero linhas.

Isso confirma a regra de resíduo no helper real e a diferença de R$ 0,01 para entradas equivalentes à seleção de um vendedor. O helper `ObterRateiosPorVendedor` e o relatório inteiro **não foram executados**: os fatores foram fornecidos pelo harness. A cadeia real de filtro continua sustentada pelo C# decompilado, não por um E2E. A prioridade de nomes e a colisão de vendedores homônimos ainda não foram exercitadas.

## Evidência reproduzível e captura

Arquivos em [engenharia/rea-runtime-2026-11-11](../engenharia/rea-runtime-2026-11-11/):

- [Harness C#](../engenharia/rea-runtime-2026-11-11/ReportRuntimeProbe.cs) e [runner](../engenharia/rea-runtime-2026-11-11/run_reports.py).
- [Resultados completos](../engenharia/rea-runtime-2026-11-11/report-results.json), [manifesto de execução](../engenharia/rea-runtime-2026-11-11/run-manifest.json), stdout/stderr e log de compilação.
- O manifesto preserva horário UTC, hashes do compilador, fonte, EXE, stdout/stderr e DLL antes/depois. Os resultados registram também os assemblies da instalação efetivamente carregados, seus hashes e MVIDs.

Repetir com `python engenharia/rea-runtime-2026-11-11/run_reports.py`. O runner rejeita um hash de alvo diferente e limita o processo a 55 segundos. Reexecutar atualiza os resultados locais; preserve uma cópia antes de comparar rodadas. O EXE gerado é ignorado pelo Git.

A chamada `capture_process_scenario` do REA foi rejeitada por `capability_unavailable`: esta versão não oferece captura PTY nativa de Windows porque não verifica limpeza de descendentes. A [resposta exata](../engenharia/rea-runtime-2026-11-11/rea-capture-unavailable.json) foi preservada. O alvo não foi iniciado por essa chamada; o ensaio acima usa `subprocess` com timeout. Não atribuir a esses logs um Evidence ID de captura REA inexistente.

As tentativas de selecionar closures aninhadas pelo nome no provedor estático REA retornaram `unmatched_type_names`; não constituem validação canônica de seus corpos. A identificação dos callbacks nesta rodada vem do próprio CLR: assembly/MVID, tokens, assinatura, tipo de campo da closure e execução dos métodos reais. A evidência estática anterior do tipo principal continua separada em [investigação REA](rea-investigation-2026-11-11.md).

## Alcance e pendências

Confirmado: mutação original, divergência concorrente na fixture atual, controle clone e rateio privado real. Nenhum byte do alvo mudou. Não executados: emissão integral de relatório, MongoDB, Akka, WCF, servidor remoto ou dados de clientes. O host Framework instalado não prova a mesma revisão exata de CLR do servidor do cliente.

Ver também os [ensaios de sincronização](runtime-sync-2026-11-11.md), que executaram Data.Core atual e histórico com cursores sintéticos tanto em Windows/.NET 10 quanto no CLR4 nativo do .NET Framework. Os seis checks selecionados coincidiram nas quatro execuções. As fronteiras sintéticas continuam limitando a conclusão; não representam homologação integral.

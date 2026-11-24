# REA adicional — relatórios e sincronização, 11/11/2026

Continuação em runtime: [callbacks e rateio do DLL atual no .NET Framework](runtime-reports-2026-11-11.md) e [Data.Core com cursores sintéticos](runtime-sync-2026-11-11.md). Esses ensaios posteriores têm escopos próprios e não alteram a natureza estática dos registros abaixo.

Nova rodada: [cadeia de rateio desde os itens](runtime-allocation-2026-11-11.md) e [mensagens do emissor de replicação](runtime-replication-2026-11-11.md), executadas nos métodos originais com entradas sintéticas.

Esta rodada identifica novamente os artefatos instalados, revisa o serviço de relatórios de saída e aprofunda cinco questões de sincronização. Análise estática: nenhum assembly do SuiteG6 foi executado, nenhum banco foi consultado e nenhum binário foi substituído. C# é reconstrução do dnSpy, não fonte original. A investigação é parcial, não um inventário completo do produto.

## Identidade atual e evidência preservada

`Sistema/DigisatServer.Application.Main.dll` e `Servidor/DigisatServer.Application.Main.dll` têm os mesmos bytes: SHA-256 `f27cb00fdbee2851ad9119a32a435943c58c072ca102fe88ef0e3f001938cd9d`, MVID `b30a6fa6-e83d-4926-b4d3-d4d1fc4370e9`. A versão de arquivo/produto lida pelo Windows é `2.0.26307.4`; a versão do assembly continua `1.0.0.0`. REA identifica CIL, AnyCPU, .NET Framework 4.6, 3.984.872 bytes, 3.224 TypeDef e 14.937 MethodDef. Esses números incluem tipos gerados pelo compilador.

Evidências de identidade: `ev_fd5ad6c410a8884c5bb887eac105d95e9601817f35d78a2e10266b0ff74f2a2d` (Sistema), `ev_501e3891cc5ca81f0accf9d7fe3a287b88029523f1cb27d96fad19aa579e829a` (Servidor).

Data.Core atual: SHA-256 `51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b`, MVID `43370605-4c69-4adb-8203-5852a9304802`, 103.424 bytes; evidência `ev_a5e212ab7281a78384e7cb09f53129aa4445f621dddc163dbebba3f7038fef92`. Sua identidade mudou em relação à análise de outubro; seus comportamentos não foram novamente decompilados nesta rodada. Os achados de sincronização do relatório complementar continuam vinculados ao build antigo.

Isso demonstra diferença de artefatos em disco, não quem os instalou, quando foram carregados, nem o build remoto em execução. Tokens anteriores não são coordenadas válidas automaticamente para este build.

O [manifesto](../engenharia/rea-2026-11-11/source-manifest.json) registra hashes do DLL, ferramenta e C#. O [índice de 75 métodos](../engenharia/rea-2026-11-11/method-index.json) contém tokens, assinaturas e hashes CIL. Evidência de membros: `ev_3ea32e79be2d74c98e4fc356175cd525fd414bdd1c36373e9d1d9fde3ca60498`. A seleção cobre exatamente o tipo principal, não os corpos dos tipos aninhados/state machines. A leitura de strings é parcial: dois flags terminais #US divergentes da especificação foram preservados; isso não prova rejeição pelo CLR.

Bundle REA: `engenharia/rea-2026-11-11/rea-evidence.json`, 11 records, 21.337.664 bytes, SHA-256 `887476a19e60be9d92dbc4b820d6c00713766ce998d6ea9109b1e07d10e04ec5`. Não possuir unknowns registrados no ledger não significa ausência de lacunas analíticas.

## Totalizadores: o padrão de concorrência continua no C# atual

O [serviço decompilado](../engenharia/rea-2026-11-11/MovimentacaoSaidaRelatorioAppService.cs) mantém `Parallel.ForEach`, dicionário global com arrays `decimal[12]` e callback de `AddOrUpdate` que altera `existing[i]` e retorna o mesmo array:

| Método | Token de entrada atual | Linhas do C# |
| --- | --- | --- |
| ObterTotalMovimentacoesPorMesPorVendedor | `0x060011f8` | 148–224 |
| TotalMovimentadoPorVendedor | `0x0600121e` | 3830–3904 |
| ObterTotalMovimentacoesPorMesPorTipo | `0x0600123f` | 6031–6086 |

Observação de reconstrução: o padrão previamente reproduzido não desapareceu deste texto. Não houve novo ensaio de corrida nem autenticação do DLL carregado no servidor. Os tokens de entrada são stubs assíncronos; seus hashes não representam os callbacks aninhados. Não declarar equivalência de CIL entre builds só porque o C# conserva o padrão.

O relatório por vendedor exclui itens cancelados e exige, para documento auxiliar, movimento de estoque **e** financeiro (180–194). O mensal por tipo apenas exclui cancelados no laço (6055–6056), fixa origem `101` e situação concluída. Portanto não são somas intercambiáveis mesmo quando o paralelismo não altera o resultado. Ambos paginam em lotes de 500; o tamanho do lote não implica limite total de 500.

`ObterItensVendidos` ainda divide o total por `Math.Max(1m, somaQuantidade)` quando não agrupa por preço (2373–2374): quantidade fracionada positiva inferior a um continua sendo um caso a auditar. O aviso de `insight_upstream_issues.py` permanece corretamente vinculado ao hash histórico; esta rodada não o reatribui silenciosamente ao novo artefato.

## Regra de rateio por vendedor esclarecida

A documentação de pagamentos dizia que a regra exata era desconhecida. Agora a reconstrução atual permite especificá-la (1790–1903):

1. Excluir itens cancelados e agrupar por `VendedorReferencia`; somar `TotalLiquido` de cada grupo.
2. Guardar a quantidade de vendedores antes do filtro opcional. O denominador continua sendo o total de todos os itens válidos, mesmo quando um vendedor é selecionado.
3. Para total positivo, fator = total do vendedor / total de itens. Para total zero ou negativo, usar fatores iguais `1 / quantidadeVendedores`. Sem grupos e sem filtro, produzir “Não informado” com fator 1; sem grupo para o vendedor filtrado, retornar vazio.
4. Nome: cadastro atual primeiro, histórico do item depois, “Não informado” por último.
5. Multiplicar cada espécie de pagamento pelo fator e arredondar. Se houver mais de um rateio, o último recebe `valorOriginal - somaAnterior`, preservando o total da espécie.

Consequência aritmética derivada, não execução do fornecedor: em uma espécie de R$ 10,00 e três vendedores com bases iguais, o resultado é R$ 3,33 / R$ 3,33 / R$ 3,34, supondo arredondamento a duas casas como indicado pela chamada helper. Ao selecionar somente o último, há um único rateio e a regra de resíduo não é aplicada: R$ 3,33. Comparar a linha filtrada à linha sem filtro pode revelar R$ 0,01 sem diferença documental. A identidade do último depende da ordem dos grupos; não há ordenação explícita nesses helpers.

Outro limite: agrupamentos finais usam o **nome** do vendedor (1725–1766), enquanto os fatores usam IDs. Pessoas distintas com o mesmo nome podem terminar na mesma linha. `Quantidade` conta linhas rateadas, não necessariamente vendas únicas globais. Total de itens não positivo usa distribuição igual, não proporcional aos valores negativos. Estes detalhes precisam estar visíveis em qualquer futura implementação de paridade no painel; não foram implementados aqui.

## Diferença concreta entre as reconstruções de outubro e novembro

Uma comparação removendo apenas comentários de tokens encontrou alteração funcional em `ObterVendasPorProdutoMensal` (4924–4972): agora há parâmetros opcionais `pessoaReferencia` e `tipoSegmentacaoReferencia`, além dos quatro parâmetros anteriores. O código consulta pessoas da segmentação, retorna vazio se a segmentação solicitada não tiver pessoas e acrescenta `PorPessoa` / `PorPessoas` aos filtros. Se ambos forem fornecidos, ambos são acrescentados; a interpretação final depende das specifications.

Não atribuir essa mudança a correção dos totalizadores. Não afirmar compatibilidade WCF apenas pela assinatura da classe concreta: contrato instalado, cliente e serviço remoto precisam ser conferidos juntos. O host local resolve operações pelo contrato e exige parâmetros presentes (`native/NativeReportHost.cs`, 104–114); portanto mudanças de contrato exigem atualização/verificação do mapa, mesmo quando o C# da implementação marca defaults opcionais.

## Sincronização e próximos passos

O [relatório complementar](rea-sync-review-2026-11-11.md) registra cinco questões com linhas e evidências históricas: carga de coleção vazia, sobreposição replay/live, classificação textual de update, falhas absorvidas e retry com filtro anterior. Essas questões ainda exigem fixtures isoladas e reconferência no Data.Core atual.

Checklist: identidade em disco **respondida**; presença dos padrões no C# atual **respondida como reconstrução**; regra do rateio **esclarecida estaticamente**; mudança do mensal por produto **identificada**; equivalência dos callbacks CIL entre builds **pendente**; identidade do servidor carregado **pendente**; runtime de rateio e protocolo de sincronização atual **pendentes**. Nenhum resultado de consulta do Insight foi alterado.

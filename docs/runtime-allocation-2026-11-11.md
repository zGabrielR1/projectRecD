# Rateio a partir de itens: helpers originais executados

Continuação dos [primeiros ensaios de rateio](runtime-reports-2026-11-11.md). Agora os fatores não são fornecidos manualmente: executamos `ObterRateiosPorVendedor` e passamos seu resultado diretamente a `RatearPorVendedor`. Também exercitamos `ObterNomeVendedor`.

Alvo: DLL instalado `DigisatServer.Application.Main.dll`, SHA-256 `f27cb00fdbee2851ad9119a32a435943c58c072ca102fe88ef0e3f001938cd9d`, MVID `b30a6fa6-e83d-4926-b4d3-d4d1fc4370e9`. Tokens verificados antes dos casos: `0x06001203`, `0x06001204`, `0x06001205`. Runtime Windows CLR `4.0.30319.42000` do Framework instalado. **17 assertions diagnósticas passaram.**

## Resultados observados

| Entrada sintética | Resultado dos helpers reais |
| --- | --- |
| Três vendedores com total líquido 1 cada; quarto vendedor com 999 cancelado | Três fatores de 1/3; o cancelado não participa do denominador nem da contagem |
| Mesmo documento, selecionando o terceiro vendedor | Um fator de 1/3; o denominador não é recalculado para o vendedor selecionado |
| Espécie de R$ 10 nos dois casos acima | Todos: 3,33 / 3,33 / 3,34; terceiro selecionado: 3,33 |
| Selecionar vendedor que só possui item cancelado | Lista vazia |
| Sem itens e sem seleção | Um fator 1 para vendedor não informado |
| Sem itens e com vendedor selecionado | Lista vazia |
| Dois vendedores com totais 2 e -2 | Fatores iguais de 0,5, pois o total é zero |
| Dois vendedores com totais -1 e -3 | Fatores iguais de 0,5; não usa proporções de 1/4 e 3/4 |
| Dois vendedores com totais 2 e -1 | Fatores 2 e -1; uma espécie de R$ 10 vira R$ 20 e R$ -10 |
| Nome atual e histórico disponíveis | Nome atual prevalece |
| Nome atual ausente, histórico disponível | Nome histórico é usado |
| Referência vazia, histórico disponível | Retorna nome não informado; ignora o histórico |
| IDs distintos com o mesmo nome no cadastro | Permanecem dois fatores, mas ambos carregam o mesmo nome |

A diferença de R$ 0,01 foi observada na cadeia real dos dois helpers, não apenas inferida a partir da fórmula. Ainda não é emissão integral do relatório: filtros de consulta, pagamentos TEF e agrupamento final não foram executados. A fusão final de nomes iguais continua uma conclusão da reconstrução estática; o runtime desta rodada confirma somente que os fatores já perdem a distinção de ID no nome de saída.

Os cenários com valores negativos documentam o comportamento do helper sobre entradas explícitas. Não foi provado que todas essas combinações possam ser produzidas por documentos válidos na interface do produto. Não classificar o rateio 20/-10 como corrupção de dados ou incidente real sem verificar essa alcançabilidade e a regra de negócio pretendida.

## Fronteira sintética

O harness cria um subtipo em memória de `RegistroVenda` e sobrescreve somente o getter virtual `Itens`. A alocação sem inicialização evita todos os construtores do domínio. Cada item é um `RealProxy` da interface real `IItem`, que responde somente a `Cancelado`, `TotalLiquido` e `VendedorReferencia`; outros acessos falham. Cadastros de nomes são um dicionário em memória com ObjectIds sintéticos.

Esses componentes são **entradas substitutas**, não documentos reais completos. Os três helpers de aplicação executados são os métodos originais do DLL, sem tradução de IL ou reescrita. Nenhum serviço, repositório, consulta Mongo, configuração ou construtor de domínio foi chamado. O harness verifica que não existe inicializador estático do serviço de relatórios. Os assemblies carregados e seus hashes/MVIDs aparecem nos resultados; o hash do alvo permaneceu igual.

## Reprodução e evidência

Executar `python engenharia/runtime-allocation-2026-11-11/run_checks.py`.

- [Harness](../engenharia/runtime-allocation-2026-11-11/AllocationProbe.cs), [runner](../engenharia/runtime-allocation-2026-11-11/run_checks.py).
- [Resultados](../engenharia/runtime-allocation-2026-11-11/results.json), [manifesto](../engenharia/runtime-allocation-2026-11-11/manifest.json), stdout/stderr e log de compilação na mesma pasta.
- Manifesto registra horários UTC, código de saída, hashes de fonte/compilador/EXE/logs e alvo antes/depois. O processo tem timeout de 20 segundos. Reexecuções substituem os resultados locais; os EXEs são ignorados pelo Git.

Pendente: emissão completa, arredondamento em outras espécies, critérios de alcance dos documentos negativos, nomes após agrupamento final e comparação com instância remota autenticada. Nenhum resultado financeiro do Insight foi alterado.

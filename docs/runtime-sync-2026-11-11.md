# Sincronização: provas de comportamento com o DLL original — 11/11/2026

Continuação: [emissor original de replicação](runtime-replication-2026-11-11.md) verifica mensagens, marcadores e checkpoints com destinatários sintéticos, incluindo exclusão física e destinatários vazios.

Três hipóteses da [revisão estática](rea-sync-review-2026-11-11.md) foram exercitadas em runtime. Executamos o **Data.Core original**, sem reescrever os métodos analisados, em processos separados: artefato histórico e artefato instalado atual, primeiro no .NET 10 e depois no CLR Windows nativo. Cada um dos quatro processos passou seis assertions de diagnóstico. Aqui, “passou” significa que o comportamento problemático esperado foi reproduzido, não que o produto foi aprovado.

## Identidade, isolamento e reprodução

| Variante | SHA-256 do Data.Core | MVID observado no runtime |
| --- | --- | --- |
| Histórico | `c848fb91ea7394a94e9302645fdcb19022efdd8de12a7257179794ecb1f485d8` | `6ed13566-e1a8-4865-b36b-aafe253f5f11` |
| Atual | `51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b` | `43370605-4c69-4adb-8203-5852a9304802` |

O runner copia somente dez bibliotecas explícitas para uma pasta local de teste e verifica seus hashes antes/depois. Os tipos e métodos `OpLogTail.Run` e `RunBetween` vêm desse Data.Core; a assinatura real de `IMongoCollection<BsonDocument>` do driver do fornecedor é interceptada por `DispatchProxy`. `IAsyncCursor` e logger são fixtures em memória. Qualquer chamada de coleção diferente das leituras previstas falha imediatamente. Não há `MongoClient`, configuração da instalação, conexão, serviço ou dado de cliente neste harness.

Comando: `python engenharia/runtime-sync-2026-11-11/run_checks.py`. [Harness](../engenharia/runtime-sync-2026-11-11/SyncRuntimeProof.cs), [runner](../engenharia/runtime-sync-2026-11-11/run_checks.py), [manifesto](../engenharia/runtime-sync-2026-11-11/manifest.json), [resultado atual](../engenharia/runtime-sync-2026-11-11/results-current.json), [resultado histórico](../engenharia/runtime-sync-2026-11-11/results-historical.json). Logs de compilação/execução ficam no mesmo diretório; DLLs e executáveis ficam em `build/`, ignorado pelo Git.

Primeiro runtime: Windows, `.NET 10.0.12`, com compilador Roslyn do SDK já instalado. Reconferência nativa: CLR `4.0.30319.42000`, `mscorlib 4.0.0.0`, instalação Framework `4.8.09221`, release `533509` segundo o registro local. Não é uma instalação limpa Framework 4.6. Os substitutos de I/O continuam limitando a conclusão aos métodos e condições exercitados.

A reconferência é reproduzível com `python engenharia/runtime-sync-2026-11-11/run_framework_checks.py`. O [adapter exato](../engenharia/runtime-sync-2026-11-11/framework-adapter.patch) altera somente infraestrutura de proxy, serialização e descrição de runtime; preserva os checks e não reescreve o código do fornecedor. As seis observações são iguais entre runtimes, verificadas pelo runner. [Resultado Framework atual](../engenharia/runtime-sync-2026-11-11/results-framework-current.json), [histórico](../engenharia/runtime-sync-2026-11-11/results-framework-historical.json), [manifesto Framework](../engenharia/runtime-sync-2026-11-11/manifest-framework.json).

## 1. Falha inicial mantém o tail marcado como ativo

Fixture: `FindSync` do checkpoint lança `InvalidOperationException`, antes de qualquer leitura de oplog. Observado nos dois DLLs:

- A primeira chamada `Run()` lança a exceção e `IsRunning` fica `true`.
- A segunda chamada `Run()` retorna sem nova leitura: uma única chamada de checkpoint no total e zero chamadas assíncronas de oplog.

Isso confirma o bloqueio de recuperação **na mesma instância** após essa falha inicial. Não testa um supervisor que destrua e recrie a instância; não demonstra que a aplicação inteira fica bloqueada permanentemente.

## 2. Replay com leitura falha pode concluir sua Task normalmente

Fixture: `FindAsync` do oplog lança uma falha sintética. Um logger em memória é configurado via `LoggerFactory.SetCurrent`, sem gravar arquivos. Observado nos dois DLLs: uma leitura tentada, um erro registrado e `RunBetween(...).Status == RanToCompletion`.

Portanto a conclusão normal dessa Task, nessas condições, não prova que o replay foi aplicado. A conclusão depende de o logger não lançar: o teste não afirma que um logger ausente ou com falha também seria absorvido. Também não transforma a ausência de exceção na Task em confirmação/ACK de gravação.

## 3. Interrupção parcial repete eventos com o filtro anterior

Fixture: o primeiro cursor entrega eventos com incrementos `1` e `2` e falha na próxima leitura. O segundo cursor entrega os mesmos dois eventos; o harness cancela após a quarta notificação para limitar o ensaio. Observado nos dois DLLs:

- Sequência recebida pelo observer: **`[1, 2, 1, 2]`**.
- Exatamente duas chamadas `FindAsync`.
- Os dois filtros renderizados são idênticos: `{ "ts" : { "$gte" : MinKey } }`.
- O campo interno `_lastTs`, inspecionado após a notificação, contém incremento `2`.

O controle do cursor é sintético: não medimos a política de retry do MongoDB. A observação demonstra que o **método original** reutiliza seu filtro inicial nessa trajetória de falha, apesar de ter avançado o timestamp em memória, e pode notificar duplicatas quando a fonte as fornece novamente. Não comprova perda de dados, frequência real de falhas ou efeitos de aplicar modificadores duplicados.

## Estado das hipóteses

Falha inicial/`IsRunning`, absorção de falha no replay e reutilização do filtro foram reproduzidas **nos DLLs histórico e atual**, em .NET 10 e no CLR Framework nativo sob as condições descritas. As seis saídas de diagnóstico têm os mesmos resultados entre variantes e runtimes, embora os binários e MVIDs sejam diferentes. Isso é igualdade dos checks selecionados, não equivalência completa entre builds.

Carga vazia, ordenação replay/live, classificação `$set`/`$unset`, ACK de persistência e comportamento de modificadores continuam pendentes de ensaios específicos. Não foi aplicado patch ao fornecedor, ao banco ou ao fluxo de produção.

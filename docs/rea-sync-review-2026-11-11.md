# Revisão estática adicional da sincronização — 11/11/2026

Continuação: [ensaios executados de Data.Core](runtime-sync-2026-11-11.md) reconferiram os itens 4 e 5 no DLL atual e histórico usando fronteiras sintéticas. Carga vazia, classificação de updates e ordem integrada replay/live continuam pendentes.

Rodadas posteriores: [emissor](runtime-replication-2026-11-11.md) e [receptor atual](runtime-receiver-2026-11-11.md) exercitaram mensagens, checkpoints e classificação dos updates em métodos originais. A validação pelo MongoDB e a ordem integrada dos atores permanecem pendentes; o [índice atualizado](runtime-investigation-index-2026-11-11.md) distingue cada fronteira.

Esta revisão amplia o diagnóstico anterior com cinco pontos concretos para homologação. Reutiliza as reconstruções e evidências REA preservadas em 30/10; não é uma nova execução dos serviços, de MongoDB ou dos binários. Não confirma a causa de um incidente remoto e não aplica correções.

## Proveniência e alcance

O [manifesto de fontes](../engenharia/sincronizacao-2026-10-30/manifesto-fontes.json) vincula cada C# ao assembly analisado. Data.Core: SHA-256 `c848fb91ea7394a94e9302645fdcb19022efdd8de12a7257179794ecb1f485d8`; SincronizadorG6: `55121ea61548d948ac562b9be0729d3b6e4ba72f48da44f1b43d9ee7487d6d14`. As coordenadas abaixo são linhas das reconstruções deste repositório e tokens locais ao build.

Nesta revisão, os SHA-256 dos cinco fontes citados (`SincronizacaoClientActor`, `SincronizacaoServerActor`, `CollectionReplicator`, `OpLogTail` e `RepositorySincronizacao`) foram recalculados e todos coincidiram com o manifesto preservado. Isso verifica a identidade dos textos reutilizados; não reconfirma a identidade do assembly atualmente instalado nem do servidor remoto.

Evidências REA reutilizadas: Data.Core `ev_ef7248082602773024cc9d546c9fff7f1479d06ca848acbbea7c9ce057d90b96`; cliente `ev_71f65185ce2dd87514a37e771c826221e665f3bfaa95a6a4216ead9ba6daf1c3`; servidor `ev_94afb4f076ff5c19120da671107ccdf8a3e027431b9c35a3c90fe276a471d449`. O [índice](../engenharia/sincronizacao-2026-10-30/indice-evidencias.json) registra 45 records e o hash do bundle. C# decompilado é reconstrução; as consequências descritas são inferências estáticas, ainda sem novos testes de execução.

## 1. Carga completa não comunica uma coleção vazia

**Observado:** `SincronizacaoServerActor.EnviarCarga` termina a coleção antes do `Tell` quando a página está vazia (linhas 188–195). Só a primeira página não vazia recebe `DropCollection=true`. No cliente, uma mensagem sem dados retorna antes de limpar (linhas 139–148). A limpeza abrange sete coleções, incluindo `Usuarios`, `Perfis`, `Contas` e `OperacoesFiscais` (linhas 38–42).

**Inferência:** uma carga completa, isoladamente, não consegue transmitir a intenção de esvaziar uma dessas coleções quando a origem já está vazia. Registros antigos podem permanecer no destino se não houve limpeza prévia por outro fluxo. Não é uma afirmação sobre o caminho de restauração, que tem `DropAll` separado, nem sobre exclusão lógica.

**Prova pendente:** em instalação isolada, origem vazia e destino com um registro, solicitar somente `SincronizacaoDataRequest`; observar mensagens e estado final. Acrescentar caso de carga após restauração para distinguir os caminhos.

Fontes: [servidor](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoServerActor.cs), [cliente](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoClientActor.cs).

## 2. Reconexão reinsere o destinatário no live antes de terminar o replay

**Observado:** no servidor, `Running` mantém replicação live para a enumeração de clientes (linhas 120–125). A reconexão chama `InserirClient` e envia resposta de conexão antes de criar/iniciar `tailReconnect.RunBetween` (linhas 141–160). A carga completa também é `async void`, sem barreira de snapshot visível (linhas 171–209). `BulkUpdateAsync` faz replacements/upserts por `_id`, sem comparação de versão (repositório, linhas 182–191).

**Inferência:** o código examinado permite sobreposição de replay/carga e live; não mostra uma barreira que impeça um replacement histórico de chegar depois de uma versão mais recente. Idempotência de repetir o mesmo documento não garante convergência quando versões distintas são intercaladas. Isto concretiza a lacuna de ordenação já mencionada no diagnóstico anterior, mas não demonstra uma inversão efetiva no runtime Akka.

**Prova pendente:** controlar duas versões do mesmo `_id`, bloquear a leitura histórica e inserir atualização live; capturar a ordem de envio, recepção e persistência no mesmo ator. Verificar também updates com modificadores.

Fontes: [servidor](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoServerActor.cs), [repositório](../engenharia/sincronizacao-2026-10-30/fontes/RepositorySincronizacao.cs).

## 3. Escolha entre update e replacement usa busca textual no JSON

**Observado:** `HandleOpLogRequest`, token `0x06000016`, decide por `message.Data.Contains("$set") || message.Data.Contains("$unset")` (cliente, linha 313). Não analisa as chaves BSON para decidir o tipo da operação. O ramo update usa `FindOneAndUpdateAsync` com opções nulas (repositório, linhas 46–48); o replacement usa upsert (linhas 35–42). No emissor, o marcador de origem só é omitido quando existe a chave BSON `$set` (replicador, linhas 91–94), uma regra diferente.

**Inferência:** um replacement cujo valor textual contenha `$set` pode ser classificado como update. Um modificador que não contenha nenhuma das duas strings pode cair no replacement; o tipo exato de rejeição depende do driver. Um update recebido antes do insert correspondente não tem upsert explícito, podendo não criar documento. Esses são candidatos distintos da exclusão física já demonstrada.

**Prova pendente:** mensagens sintéticas com replacement contendo texto `$set`, `$unset` isolado, `$inc` isolado, combinação de modificadores e update de ID ausente. Usar BSON e driver do build, sem inferir suporte a formatos novos de oplog.

Fontes: [cliente](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoClientActor.cs), [repositório](../engenharia/sincronizacao-2026-10-30/fontes/RepositorySincronizacao.cs), [replicador](../engenharia/sincronizacao-2026-10-30/fontes/CollectionReplicator.cs).

## 4. Falha de replay pode terminar a Task sem propagar a falha original

**Observado:** `OpLogTail.RunBetween`, token `0x0600003F`, captura exceções e apenas registra log (linhas 164–167). A continuação do servidor apenas descarta o tail (linhas 158–160), sem verificar falha ou enviar confirmação de aplicação. No cliente, a continuação de `HandleOpLogRequest` registra `x.Exception`, mas não relança nem solicita retry (linhas 313–335).

**Inferência:** a conclusão da Task e a resposta inicial de conexão não podem ser usadas como prova de replay ou persistência completos. No caso de erro capturado, se o próprio logger não falhar, a Task do replay pode concluir normalmente. O protocolo mostrado não associa um timestamp recebido a um ACK de gravação.

**Prova pendente:** injetar falha na leitura durante replay e falha de escrita no destinatário; medir estados das Tasks, logs, repetição e checkpoints. Não presumir que o supervisor externo fornece o retry ausente nesses métodos.

Fontes: [tail](../engenharia/sincronizacao-2026-10-30/fontes/OpLogTail.cs), [servidor](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoServerActor.cs), [cliente](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoClientActor.cs).

## 5. Retentativa do cursor usa filtro anterior até o cursor concluir

**Observado:** `OpLogTail.Run`, token `0x0600003E`, cria inicialmente `ts >= _lastTs` (linha 96), atualiza `_lastTs` antes de notificar observadores (108–112), mas só reconstrói `ts > _lastTs` depois de `ForEachAsync` terminar sem erro (117). O `catch` registra e continua (120–123), sem reconstruir o filtro ou aplicar espera. `IsRunning` é definido antes da leitura inicial do checkpoint (80–85), que está fora desse tratamento assíncrono.

**Inferência:** uma falha parcial do cursor pode reabrir o filtro anterior e repetir eventos já processados. Não é correto concluir que todo retry retoma exatamente do último evento aplicado. Falhas imediatas persistentes podem gerar tentativas/logs frequentes; a frequência real depende do driver e do logger. Se a leitura inicial do checkpoint lançar, a instância fica marcada como running e uma segunda chamada `Run()` retorna cedo (76–80).

**Prova pendente:** cursor controlado entrega dois eventos e falha antes de terminar; registrar filtros das próximas tentativas e efeitos sobre modificadores. Testar separadamente exceção no `FindSync` inicial e recuperação da mesma instância versus recriação do ator.

Fonte: [tail](../engenharia/sincronizacao-2026-10-30/fontes/OpLogTail.cs).

## Limites da reconstrução existente e próximos ensaios

A recompilação completa de Data.Core cobre superfícies de tipos e 25 assertions selecionadas por variante; não valida o protocolo inteiro. O [README do rebuild](../engenharia/recompilacao-data-core-2026-10-30/README.md) declara execução .NET 8/Linux e ausência de homologação Framework/Windows/Mongo/SuiteG6. Não estender esses resultados aos candidatos acima.

Checklist desta revisão: contrato de carga vazia **respondido estaticamente**; janela replay/live **identificada, ordem efetiva pendente**; classificação de updates **respondida estaticamente, efeitos por formato pendentes**; propagação de falhas/ACK **respondida nos métodos examinados, supervisor externo pendente**; retry do cursor **respondido estaticamente, reprodução pendente**. Priorizar fixtures isoladas para os itens 1, 3 e 5 e depois o ensaio integrado do item 2. Nenhum dado de cliente ou serviço precisa ser alterado para construir essas provas.

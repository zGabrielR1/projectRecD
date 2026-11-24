# Sincronização DigiSat: conclusão e réplica executável

**Conclusão:** há um defeito demonstrado no seletor de exclusão do overload de replicação direta e uma omissão demonstrada na seleção do segundo final do replay. A réplica agora compila e exercita o handler direto recuperado completo, com as bibliotecas reais do instalador, diante de origem e coleções em memória. Isso confirma comportamento de código em condições controladas; não comprova que esses caminhos causaram o incidente do cliente.

O código, runner e resultados estão em [engenharia/sincronizacao-prototipo-2026-10-30](../engenharia/sincronizacao-prototipo-2026-10-30/). Nenhum serviço, banco ou binário instalado foi alterado.

## O que é bug e o que ainda é hipótese

| Achado | Comprovado | Ainda falta comprovar |
| --- | --- | --- |
| Exclusão procura `o2` em vez de `o` | O handler direto lança `KeyNotFoundException`, deixa o registro no destino e não grava o checkpoint daquele evento | Se esse overload é chamado na instalação do cliente; o ator examinado usa outro overload |
| Limite superior `Timestamp(segundoFinal, 1)` | O replay exclui ordinais 2, 3 etc. no mesmo segundo final | Se o fluxo ao vivo cobre essas operações e se houve perda líquida ou atraso no cliente |

O primeiro é um defeito de implementação para um registro normal de exclusão física. O segundo é uma expressão inadequada para representar a intenção de um replay até um instante com fração de segundo: seu intervalo corta o segundo após o primeiro ordinal. O impacto de ambos depende do caminho executado e das condições da instalação.

O fato de um relatório mensal divergir não demonstra que houve falha de sincronização. O defeito de concorrência do mensal já reproduz divergência sem alteração de dados. A reconciliação documental é que permite distinguir um valor calculado incorretamente de documentos ausentes ou antigos.

## Vínculo com o build

Assembly: `DigisatServer.Infrastructure.Data.Core.dll`, SHA-256 `c848fb91ea7394a94e9302645fdcb19022efdd8de12a7257179794ecb1f485d8`. A [proveniência do ensaio](../engenharia/sincronizacao-prototipo-2026-10-30/source-manifest.json) registra os hashes do assembly e dos três fontes recuperados compilados.

| Coordenada original | Significado |
| --- | --- |
| `0x06000009` | `CollectionReplicator.To(IMongoCollection<BsonDocument>, ...)` |
| `0x06000170` | Callback `<To>b__0` do overload direto; contém o acesso incorreto |
| `0x0600000A`, callback `0x06000172` | Overload que encaminha a atores; caminho distinto |
| `0x0600001A` | Construtor de `MongoOperation`, preserva `Source=operation` e `Data=operation["o"]` |
| `0x0600003F` | `OpLogTail.RunBetween`, constrói os limites BSON do replay |

Evidence REA parent `ev_e6d417d2c7d2ce5eef2b156b0d8547822357a14a8152a2461c57328e01eeb554`; [metadados/CIL públicos](../engenharia/reconstrucao-2026-10-30/sync-evidence.json), [callbacks](../engenharia/reconstrucao-2026-10-30/sync-closure-evidence.json) e [bundle completo](../engenharia/reconstrucao-2026-10-30/rea-evidence.json.gz). Tokens são coordenadas desse hash/MVID, não identificadores universais.

## Exclusão: original, correção e resultado

Um registro sintético normal tem este formato:

```javascript
{op: "d", ns: "DigisatServer.ProdutosServicos",
 ts: Timestamp(1700000000, 3), o: {_id: ObjectId("0123456789abcdef01234567")}}
```

O seletor de exclusão é `o`. O aplicador oficial do [MongoDB 3.4.24](https://github.com/mongodb/mongo/blob/r3.4.24/src/mongo/db/repl/oplog.cpp#L900) lê esse campo para exclusões; updates utilizam `o2`. O construtor recuperado de `MongoOperation` não transforma o documento para criar `o2`.

O handler original faz:

```csharp
target.DeleteOne(builder.Eq("_id", o.Source["o2"]["_id"]));
UpdateLastTs(o.Source);
```

A [correção mínima](../engenharia/sincronizacao-prototipo-2026-10-30/delete-selector.patch) usa o seletor já disponível em `Data`:

```csharp
target.DeleteOne(builder.Eq("_id", o.Data["_id"]));
UpdateLastTs(o.Source);
```

A réplica executa insert, replacement update e delete, pelo `CollectionReplicator.From(...).To(target)` recuperado completo. No original, o insert e update chegam ao destino; o delete lança `KeyNotFoundException`, o registro permanece e o checkpoint fica no update anterior. No corrigido, o registro é removido e o checkpoint avança após a exclusão. Repetir essa exclusão deixa o destino no mesmo estado.

Também foram injetadas falhas de escrita: se o destino falha, não há avanço do checkpoint; se a escrita do destino funciona e o checkpoint falha, o destino já está atualizado. Depois de uma nova assinatura explícita, o replay do mesmo replacement upsert recompõe o checkpoint sem duplicar o registro. Isso não é uma transação entre destino e checkpoint.

Com o Rx extraído do instalador, uma exceção no callback desconecta a assinatura neste harness. A recuperação foi, portanto, explicitamente reassinada. Não foi presumida uma recuperação automática do serviço real. É uma observação adicional do ensaio que merece homologação, não prova de uma interrupção que ocorreu no cliente.

## Replay: omissão e proposta conservadora

A expressão recuperada usa segundos inteiros e ordinal fixo 1:

```csharp
var upper = new BsonTimestamp((int)fim.ToUniversalTime()
    .Subtract(new DateTime(1970, 1, 1)).TotalSeconds, 1);
// ts <= upper
```

Um pedido que termina em `...00.900` inclui `Timestamp(...00, 1)` e exclui `Timestamp(...00, 2)`. O campo BSON separa segundos e ordinal; a comparação usa a biblioteca BSON real do instalador. A documentação oficial descreve essa [estrutura de timestamp](https://www.mongodb.com/docs/manual/reference/bson-types/#timestamps).

A réplica propõe um replay conservador: limite inferior no ordinal zero do segundo inicial e limite superior **exclusivo** no ordinal zero do segundo seguinte ao fim. Assim, cobre todos os ordinais do segundo final.

Essa proposta pode incluir operações ocorridas depois da fração de segundo pedida. O timestamp BSON, sozinho, não permite localizar cada ordinal dentro da fração do segundo. A proposta precisa de sobreposição idempotente e ordenação; não deve ser apresentada como um filtro temporal exato.

O cenário sintético tem três documentos: dois no segundo final com ordinais 1 e 2; um no segundo seguinte, entregue pelo fluxo ao vivo. O replay original seleciona um documento e o destino termina com dois, faltando o ordinal 2. O replay conservador seleciona dois e o destino termina com os três. Repetir os mesmos replacement upserts preserva o estado final do destino.

**O handoff desse cenário foi escolhido pelo teste.** Ele demonstra como uma lacuna pode aparecer se o fluxo ao vivo começar depois do segundo cortado. Não mede a sobreposição do ator real. O caller recuperado de `SincronizacaoServerActor` chama `RunBetween(offline, DateTime.Now)` e usa o overload de atores com `updateLastTs=false`; a réplica direta não substitui essa implementação de reconexão.

A implementação de produção deve preferir um limite de replay baseado num timestamp real de alta marca e uma transição definida entre histórico e fluxo ao vivo. A alternativa por segundo inteiro exige ensaiar idempotência e ordem para cada operação. O ensaio atual não cobre modificadores arbitrários, updates antigos fora de ordem nem monotonicidade de checkpoints. O código original pode gravar um checkpoint antigo ao repetir eventos; por isso a reconexão por atores desativa essas gravações. Nenhuma solução completa desse protocolo foi declarada pronta.

## Como executar e o que foi executado

```sh
python engenharia/sincronizacao-prototipo-2026-10-30/run_replica.py /tmp/digisat-monthly
```

O runner usa runtime .NET 8 e Roslyn já existentes em `/tmp`, sem SDK, downloads ou instalação de sistema. Compila **sem editar** os fontes recuperados de `CollectionReplicator`, `MongoOperation` e `Operations` para a variante original. Para a corrigida, altera somente a expressão do seletor de delete.

A compilação utiliza referências do .NET 8 no Linux e dependências originais extraídas do instalador. Ela não recompila a aplicação inteira nem valida o runtime .NET Framework/Windows de produção.

O que é real no ensaio: classe recuperada completa do replicador direto, enum e objeto de operação recuperados, interfaces e serialização BSON do driver, operadores Rx e tipos Akka das bibliotecas do instalador. O que foi substituído: cursor/tail por adaptador observável manual; destino e coleção de checkpoints por spies em memória que implementam a interface real `IMongoCollection<BsonDocument>`. Chamadas inesperadas aos spies falham. O ator Akka não é iniciado.

Os seletores de replay são uma reprodução separada das expressões originais e da proposta conservadora; não se executa o cursor assíncrono de `OpLogTail.RunBetween`.

Resultados: **14 assertions na variante original e 15 na corrigida**, todas aprovadas. Consulte [results-original.json](../engenharia/sincronizacao-prototipo-2026-10-30/results-original.json) e [results-corrected.json](../engenharia/sincronizacao-prototipo-2026-10-30/results-corrected.json). As assertions originais comprovam o erro esperado; não significam que a lógica original está correta.

Também há [projeto SDK](../engenharia/sincronizacao-prototipo-2026-10-30/SyncReplica.csproj) para um ambiente com SDK. Não é necessário instalar esse SDK para usar o runner existente.

## Utilidade para suporte e desenvolvimento

- **Suporte:** diferenças documentais podem ser investigadas por `_id`, estado no destino e último timestamp aplicado. A réplica fornece um caso mínimo reproduzível para abrir chamado de desenvolvimento, sem dados comerciais do cliente.
- **Desenvolvimento:** o defeito do seletor tem uma correção de uma linha, um cenário de falha e um cenário corrigido que executam o handler recuperado. O replay tem um teste de fronteira para evitar repetir o corte do segundo final.
- **Diagnóstico do incidente:** separar relatório não determinístico, documento ainda não recebido e registro antigo que deveria ter sido excluído. Cada hipótese pede evidência diferente; “sincronização concluída” não demonstra igualdade documental.

## O que falta para concluir impacto e homologar a correção

1. Conferir os hashes e os endpoints reais do cliente. Identificar qual overload de replicação recebe cada coleção e se a exclusão física faz parte da política suportada.
2. Em réplica isolada da instalação, preservar as duas bases e o oplog/checkpoints relevantes; comparar os IDs e versões dos documentos envolvidos. Não apagar checkpoints nem disparar carga completa como teste sobre dados pendentes.
3. No Windows com o runtime .NET Framework e serviços reais, repetir insert/update/delete e reconexão numa rajada com vários ordinais por segundo, observando origem, destinatário, ACK/aplicação, checkpoints, exceções e retomada das assinaturas.
4. Integrar a correção de seletor ao fonte oficial do caminho realmente usado. Para atores, definir mensagem e tratamento de exclusão antes de encaminhar deletes; trocar o seletor do overload direto não corrige sozinho o outro caminho.
5. Homologar uma transição replay/live com alta marca, ordem, idempotência e persistência. Incluir falhas de gravação e desconexões nas duas bordas. Só então empacotar e assinar o build oficial.

Esta investigação entrega os defeitos de código e uma réplica que os demonstra. A ocorrência no cliente, a quantidade de registros afetados e o build de produção homologado continuam em aberto.

# Replicação: comportamento do emissor original — 11/11/2026

Continuação: o [handler real do receptor atual](runtime-receiver-2026-11-11.md) foi posteriormente executado com o repositório original e interfaces de banco sintéticas. Essa rodada fecha a lacuna de escolha update/replacement; a aplicação por MongoDB continua pendente.

O overload de envio para destinatários Akka foi executado em dois DLLs Data.Core originais, com destinatários e checkpoint em memória. **13 assertions de diagnóstico passaram em cada build**. O achado de maior impacto é que um evento de exclusão física pode avançar o checkpoint desse emissor sem produzir mensagem para o destinatário.

## Evidência e limites

Método exercitado: `CollectionReplicator.From(...).To(IEnumerable<ICanTell>, IActorRef, string, bool)`. O código do fornecedor é carregado dos DLLs originais e executado; não é uma recompilação manual da lógica. Entradas sintéticas passam pelo Subject do `OpLogTail`, filtro de namespace e subscriber original. O método `OpLogTail.Run` não é chamado, nem é criado cursor real.

| Variante | SHA-256 do Data.Core | MVID observado |
| --- | --- | --- |
| Histórico | `c848fb91ea7394a94e9302645fdcb19022efdd8de12a7257179794ecb1f485d8` | `6ed13566-e1a8-4865-b36b-aafe253f5f11` |
| Atual | `51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b` | `43370605-4c69-4adb-8203-5852a9304802` |

Runtime: CLR Windows `4.0.30319.42000`; instalação Framework `4.8.09221`, release `533509`. Os destinatários implementam a interface real `Akka.Actor.ICanTell` e apenas capturam a mensagem. Não há ActorSystem, mailbox, transporte ou processo receptor. Checkpoints são escritas capturadas pelo proxy da interface original `IMongoCollection<BsonDocument>`, sem banco. O harness não cria MongoClient, não lê configuração da instalação e não executa entrypoint ou controle de serviço do Sincronizador.

Reprodução: `python engenharia/runtime-replication-2026-11-11/run_checks.py`. [Harness](../engenharia/runtime-replication-2026-11-11/ReplicationRuntimeProof.cs), [runner](../engenharia/runtime-replication-2026-11-11/run_checks.py), [manifesto](../engenharia/runtime-replication-2026-11-11/manifest.json), [resultado atual](../engenharia/runtime-replication-2026-11-11/results-current.json), [histórico](../engenharia/runtime-replication-2026-11-11/results-historical.json). O manifesto identifica os onze DLLs de cada execução e os hashes do harness, compilador, executáveis e resultados. Os arquivos da instalação foram verificados antes/depois; não foram modificados.

## Payloads observados

Os dois builds produziram os mesmos comportamentos nas entradas selecionadas:

| Entrada sintética | Mensagem para ICanTell | `__sourceid__` no payload | Checkpoint padrão |
| --- | --- | --- | --- |
| Insert de documento | `OpLogInsertRequest` com ID/coleção corretos | Acrescentado | Avança |
| Update `$set` | Enviado como JSON de modificador | Não acrescentado | Avança |
| Update `$unset` | Enviado como JSON de modificador | Acrescentado no nível superior | Avança |
| Update `$inc` | Enviado como JSON de modificador | Acrescentado no nível superior | Avança |
| Replacement cujo campo Memo contém `literal $set text` | Enviado como documento | Acrescentado; não existe chave `$set` | Avança |
| Delete físico com `_id` em `o` e sem `o2` | **Nenhuma mensagem** | Não aplicável | **Avança** |

O insert também confirmou que a inclusão do marcador altera o clone enviado e preserva o `BsonDocument` de entrada. O marcador de `$unset` e `$inc` aparece ao lado do modificador, por exemplo `{ "$inc": { "Count": 1 }, "__sourceid__": "source-fixture" }`. Este teste observa o payload produzido; não executa sua validação ou persistência por um servidor MongoDB.

O caso de replacement separa duas propriedades observadas: `BsonDocument.Contains("$set") == false`, mas o JSON enviado satisfaz `Data.Contains("$set") == true`. A [fonte histórica do receptor](../engenharia/sincronizacao-2026-10-30/fontes/SincronizacaoClientActor.cs), linhas 310–313, escolhe update por busca textual `$set`/`$unset`. Portanto há uma incompatibilidade estática candidata entre emissor e esse receptor. **O receptor não foi executado nem reautenticado no build atual nesta rodada**; não afirmar que o replacement foi efetivamente rejeitado, convertido ou aplicado no MongoDB.

## Exclusão e checkpoint

No evento físico de delete, o número de mensagens capturadas permaneceu inalterado, enquanto a última escrita de checkpoint passou ao timestamp do delete. A exclusão não provocou exceção nesse overload. É comportamento diferente do overload direto para coleção Mongo, cujo acesso a `o2` em delete já havia sido reproduzido anteriormente.

Isso demonstra ausência de propagação da exclusão **neste caminho** com a entrada testada. Não prova que exclusão lógica, carga completa ou outro fluxo do produto deixe de remover documentos. Não medir uma mensagem de delete aqui não equivale a examinar todo o protocolo Akka.

## Outras trajetórias de entrega

- **Lista vazia de destinatários:** um insert produziu escrita de checkpoint, mesmo sem nenhum destinatário disponível.
- **Sem processamento no receptor:** `Tell` apenas capturou a mensagem e retornou; o checkpoint já avançou. Essa trajetória não exige confirmação de persistência do destinatário.
- **`updateLastTs=false`:** o insert foi enviado, sem escrita de checkpoint.
- **Supressão de eco:** documento com `__sourceid__` igual ao `targetId` não foi enviado e não atualizou checkpoint nesse callback.
- **Falha no segundo destinatário:** o primeiro já recebeu a mensagem; a exceção sintética do segundo propagou e não houve escrita de checkpoint. Não existe rollback do envio anterior nesse callback.
- **Namespace estrangeiro:** evento de outra base não gerou mensagem ou checkpoint neste replicador.

Estes resultados delimitam a semântica do emissor. Eles não medem garantias de entrega ou ordenação do Akka, retries externos, confirmação durável do MongoDB, nem o efeito de uma queda entre Tell e checkpoint. Uma retomada após entrega parcial pode repetir o envio ao primeiro destinatário, mas esse cenário de reinício não foi executado aqui.

## Estado da investigação

Respondidos por execução dos DLLs originais: formato das mensagens, política do marcador para os quatro tipos de update selecionados, não envio do delete físico neste overload, avanço de checkpoint com destinatários vazios/sem processamento e efeitos de falha síncrona em um destinatário. As saídas dos 13 checks são iguais entre os builds analisados; isso não demonstra equivalência completa dos assemblies.

Pendentes: receptor efetivo do build atual; aplicação de `$inc`/`$unset` com marcador; replacements contendo literais `$set`; retry após entrega parcial; transporte e ordenação real; validação contra fixture MongoDB isolada. Nenhum patch foi aplicado ao fornecedor, serviço ou banco.

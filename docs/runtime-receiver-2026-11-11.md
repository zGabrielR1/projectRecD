# Receptor: classificação observada em runtime — 11/11/2026

A lacuna de classificação do receptor foi fechada para o método privado real do build local atual. **22 assertions passaram**, executando `SincronizacaoClientActor.HandleOpLogRequest` e `RepositorySincronizacao.UpdateOneAsync` / `ReplaceOneAsync` originais em instâncias isoladas. Um replacement com texto literal `$set` ou `$unset` em um campo foi efetivamente encaminhado ao método de update; `$inc` foi encaminhado ao de replacement.

## Identidade e isolamento

- `C:/Digisat/SuiteG6/Sincronizador/SincronizadorG6.exe`: SHA-256 `26ffed48484bdd9e9210034d2076c07f713fed969c9dd877039d28133c7e5386`, MVID observado `91c0799e-4f08-465f-aa32-5796302826d9`, assembly `SincronizadorG6, Version=2.0.0.0`. Handler privado token `0x06000016`, local a esse artefato.
- Data.Core usado pelo receptor: SHA-256 `51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b`, MVID `43370605-4c69-4adb-8203-5852a9304802`.
- Runtime: CLR Windows `4.0.30319.42000`, mesma instalação Framework local dos ensaios anteriores. Não é uma homologação de um servidor remoto nem uma instalação limpa Framework 4.6.

O ator e o repositório foram alocados por `FormatterServices.GetUninitializedObject`, sem executar seus construtores. Injetamos o repositório original, seu `_builder` e um `IMongoDatabase`/`IMongoCollection` em memória implementado com `RealProxy`. O método privado foi invocado por reflexão, não pelo Akka. Não foi criado ActorSystem, MongoClient ou cursor, não foi lida configuração do produto e nenhum entrypoint/controle de serviço foi chamado.

Todos os proxies retornam Tasks bem-sucedidas com resultado sem correspondência; o caminho de falha/log não foi executado. O logger do ator permaneceu nulo, conferido no final. A construção parcial limita a conclusão ao handler e às chamadas de repositório isoladas, sem validar inicialização ou invariantes do ator completo.

Reprodução: `python engenharia/runtime-receiver-2026-11-11/run_checks.py`. [Harness](../engenharia/runtime-receiver-2026-11-11/ReceiverRuntimeProof.cs), [runner](../engenharia/runtime-receiver-2026-11-11/run_checks.py), [hashes aceitos](../engenharia/runtime-receiver-2026-11-11/reviewed-inputs.json), [resultados](../engenharia/runtime-receiver-2026-11-11/results.json), [manifesto](../engenharia/runtime-receiver-2026-11-11/manifest.json).

Os treze arquivos explicitamente copiados são conferidos contra hashes aceitos antes de execução e novamente após o ensaio. O executável do fornecedor é carregado como assembly pelo harness; nunca é iniciado como aplicativo.

## Matriz observada

| JSON de entrada, forma resumida | Método real de coleção chamado | Opções observadas |
| --- | --- | --- |
| `$set: { Name: ... }` | `FindOneAndUpdateAsync` | `options == null` |
| `$unset` + marcador de origem no nível superior | `FindOneAndUpdateAsync` | `options == null` |
| `$inc` + marcador de origem no nível superior | `ReplaceOneAsync` | `IsUpsert == true` |
| Replacement comum + marcador | `ReplaceOneAsync` | `IsUpsert == true` |
| Replacement com `Memo: "literal $set text"` | **`FindOneAndUpdateAsync`** | `options == null` |
| Replacement com `Memo: "literal $unset text"` | **`FindOneAndUpdateAsync`** | `options == null` |
| Replacement com `Memo: "literal $SET text"` | `ReplaceOneAsync` | `IsUpsert == true` |

Para cada caso verificamos a rota, o filtro `_id` igual ao ID da mensagem e a política de opções. O filtro usa `ObjectId.Parse(message.Id)`. Todos os handlers concluíram com `RanToCompletion` após as Tasks sintéticas; isso não indica que algum documento foi modificado.

A seleção é sensível a maiúsculas/minúsculas e considera o texto serializado, não somente as chaves de operadores BSON. O ensaio prova o despacho real dos sete payloads escolhidos, incluindo os falsos positivos por texto em valores e o encaminhamento de `$inc` para replacement. Não prova aceitação de qualquer desses payloads por um servidor MongoDB, nem qual exceção surgiria no driver/servidor real. Em particular, o proxy permite renderizar/capturar payload misto; isso não é validação do protocolo de update do MongoDB.

## Relação com o emissor e proveniência estática

O [ensaio do emissor](runtime-replication-2026-11-11.md) observou `$unset` e `$inc` com marcador no nível superior e replacement com literal `$set`. O teste atual alimenta formas equivalentes no receptor original, sem transportar mensagens entre atores. Assim, ambos os lados têm observações de métodos originais, mas ainda não existe um ensaio ponta a ponta com Akka/MongoDB.

Antes da execução, o tipo do receptor atual foi descompilado pelo dnSpy sem iniciar o alvo. SHA-256 do texto local: `32cd95e82600c0fa338352742bf1ed652bd2ac854eeddc88f2a93658995dc67d`; ferramenta: `1f1e6ffb55388cea037bba17ba93b48d1886b5a47bf21d94b99064f5c85f44a2`. [Manifesto da extração](../engenharia/runtime-receiver-2026-11-11/source-manifest.json). O C# é reconstrução; os resultados posteriores são observações da execução do DLL original. O flag `target_executed=false` desse manifesto descreve a etapa de extração, não o ensaio posterior.

## Pendências

Respondidos: classificação dos sete payloads, filtro de documento e opções de update/replacement no handler/repositório atuais. Pendentes: aplicação em MongoDB isolado, erros do driver/servidor, logs e política de retry/ACK, inicialização e supervisão Akka, transporte real e identificação do build remoto. Nenhuma configuração, dado ou serviço foi alterado.

# Recompilação integral de Data.Core a partir de C# — 30/10/2026

**Resultado:** o assembly completo `DigisatServer.Infrastructure.Data.Core.dll` foi descompilado e recompilado de C# contra as referências do **.NET Framework 4.6**, no Linux. Não foi uma reescrita de CIL nem uma coleção de métodos com stubs. O assembly original permanece intacto.

Essa etapa avança da compilação de classes isoladas para uma biblioteca inteira. Os fontes e artefatos ficam em [engenharia/recompilacao-data-core-2026-10-30](../engenharia/recompilacao-data-core-2026-10-30/), com [runner reproduzível](../engenharia/recompilacao-data-core-2026-10-30/run_rebuild.py) e [manifesto](../engenharia/recompilacao-data-core-2026-10-30/rebuild-manifest.json). São arquivos locais de investigação; os fontes recuperados e recursos estão ignorados pelo Git para evitar publicação acidental de código do fornecedor.

## Identidade e cobertura

| Medida | Original | Recompilado |
| --- | --- | --- |
| SHA-256 | `c848fb91ea7394a94e9302645fdcb19022efdd8de12a7257179794ecb1f485d8` | `c3f9199eedd0c2ba6aa9469546ca0566a5329b85f6999229373137239b6cf77f` |
| MVID | `6ed13566-e1a8-4865-b36b-aafe253f5f11` | `cd032c37-1a01-4bf3-8655-e889b85424de` |
| Tipos, incluindo gerados pelo compilador e `<Module>` | 133 | 144 |
| Métodos, incluindo gerados | 519 | 562 |
| Tipos originais nomeados comparados | 66 | Todos encontrados com a superfície comparada igual |
| Referências de assemblies | 13 | Exatamente as mesmas identidades/versões |
| Recurso embutido | 697 bytes | Mesmo nome e SHA-256 |

O arquivo C# recuperado contém **2.892 linhas**. ILSpy reconstrói closures, iteradores e async em sintaxe C#; Roslyn gera outros nomes e quantidades de tipos/métodos auxiliares. Assim, contagens geradas e tokens não são preservados literalmente. As duas classes auxiliares nomeadas adicionais são `Microsoft.CodeAnalysis.EmbeddedAttribute` e `System.Runtime.CompilerServices.RefSafetyRulesAttribute`.

A comparação abrange os 66 tipos originais não nomeados como artefatos `<...>`, suas classes base/interfaces, **303 assinaturas de métodos públicos/protegidos** e **19 campos públicos/protegidos**. Isso não verifica toda a metadata, atributos, constraints, valores opcionais ou equivalência semântica de todos os corpos. Os inventários estão em [original-metadata.json](../engenharia/recompilacao-data-core-2026-10-30/original-metadata.json) e [rebuilt-metadata.json](../engenharia/recompilacao-data-core-2026-10-30/rebuilt-metadata.json).

## Uma correção de reconstrução, sem mudar o comportamento

A saída integral de ILSpy teve somente um erro de fonte depois de referenciar o framework correto: `TimeoutException` era ambíguo entre `System.TimeoutException` e `System.ServiceProcess.TimeoutException`.

O metadata original do método `EntityMongoMap.Initialize`, token `0x060000FF`, identifica o catch como **`System.TimeoutException`**. A única edição de fonte foi qualificá-lo:

```diff
- catch (TimeoutException)
+ catch (System.TimeoutException)
```

O [patch de reconstrução](../engenharia/recompilacao-data-core-2026-10-30/source-repair.patch) registra essa mudança. Ela corrige ambiguidade introduzida pelo decompilador; não é um bug corrigido no aplicativo original. Nenhuma rotina de conexão, exclusão, replay, mapeamento ou cálculo foi alterada.

As tentativas iniciais com referências de .NET 8 não ofereciam `ConfigurationManager` e `ServiceController`, além de indicarem APIs AppDomain incompatíveis com esse runtime. Em vez de remover rotinas ou inventar substitutos, foi usado o pacote de referências `Microsoft.NETFramework.ReferenceAssemblies.net46` 1.0.3, aproximadamente **18,9 MiB compactados**, extraído em `/tmp/digisat-monthly/net46-reference`. Nenhum SDK, Windows, Wine ou componente de sistema foi instalado.

O recurso binário original `DigisatServer.Infrastructure.Data.Core.Resources.Messages.resources` foi extraído e incluído com o mesmo nome; o payload tem SHA-256 `f517eee17be47802031c2329243d74ebee3279464fefcdb8749563683d7f916f`. As strings desse recurso foram exercitadas no teste.

## Comportamentos comparados entre o DLL original e o recompilado

Os dois assemblies foram carregados em **processos separados** no runtime portátil .NET 8/Linux, usando dependências reais do instalador. Cada variante passou pelas mesmas **25 assertions**, e o JSON dos resultados é igual:

- Composição e execução de especificações `True`, `Direct`, `And`, `Or` e `Not`, inclusive combinação de parâmetros de expressões diferentes.
- Estado de paginação em `Page<T>`.
- Parsing de insert/update/delete/operação não suportada em `MongoOperation`, preservação de `Source`/`Data` e namespace.
- Round-trip de quatro valores decimais por `DecimalSerializerEx`, com gravação BSON Double.
- Preservação de ticks e retorno `DateTimeKind.Unspecified` para datas Utc, Local e Unspecified.
- Leitura real das strings do recurso embutido.
- **O `CollectionReplicator` e o `OpLogTail` efetivamente carregados do DLL**, e não classes copiadas em um pequeno programa: insert e replacement update, bug original do delete e checkpoint anterior ao erro.

No último teste, apenas as fronteiras externas são controladas: `IMongoCollection` usa proxies em memória, e o subject interno do `OpLogTail` recebe eventos sintéticos por reflexão. Nenhum cursor, banco, serviço ou arquivo de produção é aberto. O caminho Rx e o handler do replicador são os do assembly carregado.

Original e recompilado reproduzem o mesmo delete defeituoso: `KeyNotFoundException`, registro ainda no destino e checkpoint no incremento 2 do update anterior. Isso é intencional: esta biblioteca é uma **reconstrução de referência**, não a variante corrigida. O [protótipo anterior de sincronização](bugs-sincronizacao-prototipo-2026-10-30.md) demonstra a correção de seletor separadamente.

Resultados: [behavior-original.json](../engenharia/recompilacao-data-core-2026-10-30/behavior-original.json) e [behavior-rebuilt.json](../engenharia/recompilacao-data-core-2026-10-30/behavior-rebuilt.json). Compilar para Framework 4.6 e testar operações selecionadas no .NET 8 não é homologar todo o assembly em Framework/Windows.

## Como repetir

```sh
python engenharia/recompilacao-data-core-2026-10-30/run_rebuild.py /tmp/digisat-monthly
```

O runner verifica o hash original, compila as ferramentas, descompila a biblioteca inteira, extrai o recurso, comprova o tipo do catch, aplica somente a qualificação, compila com `-nostdlib+` contra as referências reais do Framework 4.6, compara as superfícies/recursos/referências e executa ambos os testes de comportamento. Não faz downloads nem altera o assembly original. Todos os outputs ficam na própria pasta da investigação.

As ferramentas portáteis previamente fornecidas e as referências precisam existir em `/tmp/digisat-monthly`. O pacote de referências usado tem SHA-256 `dcbb79bb3868dbfbb64c643116faa63d888ee9eccba0c4e965e9992ed7c4e35d`; versões e hashes dos outros componentes ficam no manifesto. Dois arquivos de referência especiais (`System.EnterpriseServices.Thunk.dll`, nativo, e `System.EnterpriseServices.Wrapper.dll`, netmodule) são excluídos do conjunto de assemblies de referência.

Há também um [projeto SDK](../engenharia/recompilacao-data-core-2026-10-30/Data.Core.recovered.csproj), para ambientes que já possuem SDK. O caminho testado nesta máquina foi o runner Roslyn, sem instalar SDK.

## O que ainda falta

1. Executar a biblioteca reconstruída no .NET Framework real de uma instalação Windows isolada, incluindo os caminhos de `ConfigurationManager`, `ServiceController`, AppDomain e P/Invoke. Eles foram compilados, mas não executados aqui.
2. Homologar repositórios, cursores e serializadores com dados representativos, tipos reais de domínio, MongoDB da versão instalada, falhas de rede/autenticação e concorrência. Os testes atuais usam BSON em memória e um subconjunto de valores.
3. Verificar toda a superfície contratual necessária — atributos, constraints, parâmetros opcionais, serialização e referências externas a tokens — e ampliar a comparação semântica para os métodos ainda não exercitados.
4. Integrar com os demais assemblies recuperados, o host/DI/WCF e a aplicação completa. A igualdade das assinaturas comparadas não prova que mecanismos de verificação ou reflexão privados aceitarão o novo binário.
5. Aplicar correções de bugs como mudanças separadas da reconstrução, com testes próprios, antes de homologar um build oficial. O baseline recompilado preserva os comportamentos e defeitos do original.

Esta etapa conclui a **recompilação integral desta biblioteca** a partir do C# recuperado, com referências do target correto e comparação comportamental parcial. A reconstrução de todo o SuiteG6 e a homologação de produção continuam abertas.

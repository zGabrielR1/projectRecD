# Data.Core atual reconstruído como projeto local

O projeto contém o módulo completo recuperado do DLL instalado atual: SHA-256 `51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b`, MVID `43370605-4c69-4adb-8203-5852a9304802`, versão de arquivo `2.0.26307.4`. Não reutiliza o texto de um build anterior como se fosse atual.

Fonte editável: `source/Data.Core.recovered.cs`, 2.933 linhas. A extração ILSpy atual não exigiu reparos de sintaxe: `Data.Core.raw.cs` e `Data.Core.recovered.cs` têm SHA-256 `0df0df4a5610446dcf7ae985ebd3b7ff87854bb8531126a8d214a9db07d662cc`. `source-repair.patch` está vazio. `recovery-manifest.json` vincula artefato, ferramenta, referências, recurso e texto.

## Compilar e testar offline

Da raiz do repositório:

```powershell
python engenharia/codebase-local/tools/build_data_core.py --compile-only
python engenharia/codebase-local/tools/build_data_core.py --test
& 'C:/Windows/Microsoft.NET/Framework64/v4.0.30319/MSBuild.exe' engenharia/codebase-local/projects/DigisatServer.Infrastructure.Data.Core/DigisatServer.Infrastructure.Data.Core.csproj /t:Build
```

O `.csproj` expõe fonte/recurso/referências e usa o runner Python como target de build offline; não é um projeto SDK com restore. A saída é `bin/DigisatServer.Infrastructure.Data.Core.dll`. O build normal compila o arquivo editável existente e **não descompila novamente**. `--test` também executa as fixtures locais de comportamento em processos separados.

Os manifests comparam a superfície reconstruída com o original. Fontes editadas compilam com o resultado dessa comparação registrado, sem exigir que uma mudança intencional conserve a API antiga; os testes continuam verificando os comportamentos declarados em suas fixtures.

Ferramentas existentes utilizadas: Roslyn do SDK `11.0.100-rc.2.26504.105`, CLR Windows nativo `4.0.30319.42000` e assemblies de referência da instalação Framework 4.8 local. O atributo de destino recuperado permanece `.NETFramework,Version=v4.6`; compilar contra assemblies instalados Framework 4.8 não substitui uma homologação com o targeting pack net46 original. Nenhum pacote é baixado.

`references/` guarda cópias dos DLLs necessários e `resources/` o payload original. O build/teste normal não precisa dos DLLs na instalação do SuiteG6; todas as dependências do projeto estão locais. A localização do compilador/Framework continua específica deste Windows. O recurso é preservado byte a byte, sem converter ou editar seus valores.

## Verificação realizada

O módulo inteiro compilou. O comparador de metadata confirmou:

- As 66 superfícies originais de tipos nomeados, com bases, interfaces, métodos públicos/protegidos e campos correspondentes.
- A mesma identidade das referências de assembly e os mesmos payloads de recurso.
- Apenas dois tipos extras gerados pelo compilador: `Microsoft.CodeAnalysis.EmbeddedAttribute` e `System.Runtime.CompilerServices.RefSafetyRulesAttribute`.

O DLL reconstruído passou **19 assertions nativas** com observações iguais às do DLL atual original: seis de falha/checkpoint/retry do tail e treze de emissor para destinatários/checkpoint sintéticos. `tests/manifest.json` registra hashes de harness, baseline, resultados e DLL reconstruído. As assertions preservam inclusive os defeitos previamente reproduzidos; nenhum conserto de produção foi misturado à reconstrução.

Isso não demonstra equivalência integral de todos os métodos. Não testa MongoDB, configuração, serviços, WCF, transporte Akka, licenciamento, inicialização do produto ou dados de clientes. O DLL local tem MVID/hash próprios e não é um substituto aprovado de produção.

## Recuperar novamente

`--recover` usa o artefato atual fixado por hash e helpers locais; recusa sobrescrever uma fonte editável já existente. `--force-recover` descarta deliberadamente as edições da fonte e recria sua base; use somente quando essa substituição for desejada. A recuperação é uma etapa separada de compilação. Os helpers de metadata/descompilação estão em `tooling/`; a etapa de recuperação inicial os copiou de evidências anteriores, mas o conteúdo recuperado veio do DLL atual autenticado.

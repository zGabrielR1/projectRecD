dnSpy.Console.dll [options] [fileOrDir1] [fileOrDir2] [...]

  --sdk-project   gerar arquivos de projeto no estilo SDK
  --asm-path caminho   caminho de pesquisa de assembly. Caminhos podem ser separados com ';' ou você pode usar múltiplos --asm-path's
  --user-gac caminho   caminho GAC do usuário. Caminhos podem ser separados com ';' ou você pode usar múltiplos --user-gac's
  --no-gac       não use o GAC para procurar assemblies. Útil com --no-stdlib
  --no-stdlib    projetos não fazem referência a mscorlib
  --no-sln       não criar um arquivo .sln
  --sln-name nome   nome do arquivo. sln
  --threads N    número de threads de trabalho. O padrão é usar um thread por núcleo de CPU
  --no-resources   não descompacte recursos
  --no-resx      não crie arquivos .resx
  --no-baml      não descompile baml para xaml
  --no-color     não colorir o texto
  --spaces N     Tamanho de uma guia em espaços ou 0 para usar uma guia
  --vs N         Visual Studio versão, 2005, 2008, ..., 2017
  --project-guid N   projeto guid
  -t nome        descompile o tipo com o nome especificado para stdout. Ambos Namespace.Name ou Name, diferencia maiúsculas de minúsculas
  --type nome    mesmo que -t
  --md N         descompilar o membro com o token de metadados N para stdout
  --gac-file assembly   descompilar um assembly do GAC. Use o nome completo do assembly para usar uma versão exata.
  -r             pesquisa recursiva de arquivos .NET para descompilar
  -o outdir      diretório de saída
  -l lang        definir linguagem, padrão é C#. Guids podem ser usados.

Linguagens:
  C# ({4162dada-67c3-4de4-a5f3-6552c8353ece})
  Visual Basic ({bba40092-76b2-4184-8e81-0f1e3ed14e72})
  IL ({a4f35508-691f-4bd0-b74d-d5d5d1d0e8e6})
  IL with C# ({ca52a515-12ae-4182-bc88-81ed037c3d32})

Opções da linguagem:
Todas as opções booleanas podem ser desabilitadas usando 'no-' ou 'dont-', por exemplo. --dont-sort-members

  C# ({4162dada-67c3-4de4-a5f3-6552c8353ece})
  Visual Basic ({bba40092-76b2-4184-8e81-0f1e3ed14e72})
    --member-order	(String = mpeft) Ordem de descompilação
    --anon-methods	(Boolean = True) Descompilar métodos/lambdas anônimos
    --expr-trees	(Boolean = True) Descompilar árvores de expressão
    --yield	(Boolean = True) Descompilar enumeradores (yield return)
    --async	(Boolean = True) Descompilar métodos assíncronos (async/await)
    --auto-props	(Boolean = True) Descompilar propriedades automáticas
    --auto-events	(Boolean = True) Descompilar eventos automáticos
    --using-stmt	(Boolean = True) Descompilar usando instruções
    --foreach-stmt	(Boolean = True) Descompilar instruções foreach
    --lock-stmt	(Boolean = True) Descompilar instruções lock
    --switch-string	(Boolean = True) Descompilar switch on string
    --using-decl	(Boolean = True) Adicionar declarações 'using'
    --query-expr	(Boolean = True) Descompilar expressões query
    --ambig-full-names	(Boolean = True) Adicionar um namespace para tipos com o mesmo nome
    --full-names	(Boolean = False) Adicionar namespaces a todos os tipos
    --use-debug-syms	(Boolean = True) Utilizar nomes de variáveis de símbolos de depuração, se disponíveis
    --obj-inits	(Boolean = True) Descompilar objetos ou inicializadores de coleção
    --xml-doc	(Boolean = True) Exibir a documentação XML no código descompilado
    --remove-emtpy-ctors	(Boolean = True) Remover os construtores padrão vazio
    --inc-dec	(Boolean = True) Usar operadores de incremento e decremento
    --make-assign-expr	(Boolean = True) Utilize expressões de atribuição, como em while ((count = Do()) != 0) ;
    --always-create-ex-var	(Boolean = False) Sempre gerar variáveis de exceção em blocos catch, a menos que o tipo seja Object
    --tokens	(Boolean = True) Mostrar tokens, RVAs e arquivos offsets
    --sort-members	(Boolean = False) Classificar métodos, campos, propriedades, eventos e tipos
    --show-all	(Boolean = False) Mostrar tipos e métodos escondidos gerados pelo compilador
    --system-first	(Boolean = True) Posicionar diretivas do 'Sistema' primeiro ao classificar
    --max-array-elems	(Int32 = 10000) Número máximo de elementos do array para mostrar
    --max-string-length	(Int32 = 20000) Comprimento máximo do texto
    --sort-custom-attrs	(Boolean = False) Classificar atributos personalizados
    --src-order	(Boolean = True) Classificar membros em ordem de código fonte
    --field-initializers	(Boolean = True) Permitir inicializadores de campo
    --one-ca-per-line	(Boolean = True) Mostrar um atributo personalizado por linha
    --internal-modifier-type	(Boolean = True) Adicionar modificador 'interno' aos tipos
    --private-modifier-member	(Boolean = True) Adicionar modificador 'private' aos membros de tipo
    --remove-new-delegate-class	(Boolean = True) Substituir 'new delegate-class(xxx)' por 'xxx'
    --hex	(Boolean = False) Números hexadecimais
    --emit-calli-as-invoke	(Boolean = False) Decompile calli to invocation expression
    --insert-parentheses-for-readability	(Boolean = True) Insert parentheses for readability

  IL ({a4f35508-691f-4bd0-b74d-d5d5d1d0e8e6})
  IL with C# ({ca52a515-12ae-4182-bc88-81ed037c3d32})
    --comments	(Boolean = False) Mostrar comentários IL opcode
    --xml-doc	(Boolean = True) Exibir a documentação XML no código descompilado
    --tokens	(Boolean = True) Mostrar tokens, RVAs e arquivos offsets
    --bytes	(Boolean = True) Mostrar bytes de instruções IL
    --sort-members	(Boolean = False) Classificar métodos, campos, propriedades, eventos e tipos
    --pdb-info	(Boolean = True) Mostrar números de linhas e nomes de arquivos, se disponível
    --max-string-length	(Int32 = 20000) Comprimento máximo do texto
    --hex	(Boolean = False) Números hexadecimais

Exemplos:
  dnSpy.Console.dll -o C:\out\path C:\some\path
      Descompila todos os arquivos .NET no diretório acima e salva os arquivos em C:\out\path
  dnSpy.Console.dll -o C:\out\path -r C:\some\path
      Descompila todos os arquivos .NET no diretório acima e todos os sub-diretórios
  dnSpy.Console.dll -o C:\out\path C:\some\path\*.dll
      Descompila todos os arquivos *.dll .NET no diretório acima e salva os arquivos em C:\out\path
  dnSpy.Console.dll --md 0x06000123 file.dll
      Descompila o membro com o token 0x06000123
  dnSpy.Console.dll -t system.int32 --gac-file "mscorlib, Version=4.0.0.0"
      Descompila System.Int32 de mscorlib

ERRO: Diretório de saída ausente

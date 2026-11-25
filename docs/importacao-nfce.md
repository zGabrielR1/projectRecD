# Importação NF-e e NFC-e por XML

## 1.1.3 RC1

Importar XML fiscal aceita lotes mistos de NFC-e (modelo 65) e NF-e (modelo 55), com nfeProc autorizado, de saída e finalidade normal. A identificação automática usa somente a conexão selecionada no aplicativo: não exige Studio 3T, exportação BSON, cópia de outra base ou perfil manual. As amostras, o discriminador nativo e o TipoMovimentacao do XML ficam separados por modelo. A base precisa conter exemplos compatíveis da própria empresa, produtos e pagamentos; ausência ou ambiguidade bloqueia o arquivo.

## 1.1.3 RC4

No modo manual, as opções são salvas na criação do lote. Use **Revisar e cadastrar pessoas do lote** e a seção **Produtos** para conferir os cadastros separados antes de gravar os movimentos que dependem deles.

Para chaves arquivadas na base, selecione **Tipo das chaves informadas** se o lote precisar de um mapeamento de pasta específico. Uma nota de compra emitida pelo fornecedor deve usar **Nota de compra**. A direção de saída no XML é a do fornecedor. A importação por chave de CF-e exige a seção `cfe` no perfil.

| Opção | Configuração do perfil | Resultado |
| --- | --- | --- |
| Pessoas | `person_template.Cpf` e/ou `person_template.Cnpj` | Prepara o fornecedor da nota de compra ou o destinatário da NF-e para cadastro individual ou em lote. |
| Produtos | `product_catalog.product` e `product_catalog.company_product`, ou `product_template` com movimentações selecionadas | Prepara produto e vínculo por empresa para cadastro separado. Com `product_template`, os registros gerados entram na mesma aplicação da movimentação. |
| Movimentações de Entrada e Saída | Perfil da origem do XML | Grava a movimentação e arquiva o XML. Com a opção desmarcada, continua possível revisar o lote e cadastrar pessoas e produtos separadamente. |
| Movimentações Financeiras | `finance.payable` e/ou `finance.receivable` e vínculo da pessoa em `movement.Pessoa` | Prepara títulos em aberto para duplicatas com vencimento e valor. Pode ser usada separadamente para gravar apenas `Pagamentos` ou `Recebimentos`. |

Os modelos BSON do perfil devem representar a versão do SuiteG6 usada na base de destino. A identificação automática usa os vínculos existentes.

Compras/entrada/CF-e por perfil continuam suportados conforme o mapeamento declarado. A verificação das gravações nas rotas nativas do SuiteG6 instalado permanece pendente. Importar um documento histórico não executa os serviços nativos de estoque, caixa, numeração ou sincronização do SuiteG6; baixas financeiras não são inventadas.

Configure servidor, porta, banco, usuário, senha e autenticação em **Configurar conexão → Testar e salvar**. As opções do aplicativo permitem definir limite de linhas, endpoint/modelo/chave de IA e acesso ao painel. O aplicativo grava a configuração local; não é necessário editar config.env nem criar arquivo de senha. A senha MongoDB fica em MONGO_PASSWORD nesse arquivo local. Senhas não aparecem nas APIs de diagnóstico. Alterações no acesso ao painel são aplicadas ao reiniciar.

Instalações antigas ainda podem ser lidas; salvar pela interface migra a referência MONGO_PASSWORD_FILE para MONGO_PASSWORD, sem criar novas chaves separadas. Arquivos antigos do usuário não são apagados. Nunca distribua a configuração preenchida ou sua cópia .previous. O pacote contém somente um modelo sem credenciais.

Os testes desta versão gravam NFC-e e NF-e sintéticas em MongoDB descartável autenticado, conferem as duas coleções, o diário, a autenticação após salvar e a repetição sem duplicação. Não equivalem a homologação no SuiteG6 instalado.

A tela /nfce-import e o script tools/import-nfce.py preparam documentos para Movimentacoes, XmlMovimentacoes e, quando o perfil declarar, cadastros de produto e títulos financeiros. A inserção é histórica: não executa serviços nativos de estoque, caixa, numeração ou sincronização do SuiteG6. A compatibilidade nativa continua baseada na análise estática e nos formatos observados; não foi executada inserção em base de cliente durante o desenvolvimento.

## Buscar pela chave de acesso

Em **Importar XML fiscal**, cole uma ou mais chaves de 44 dígitos. A lista aceita quebras de linha, tabulações, vírgulas e ponto e vírgula. Chaves repetidas são consultadas uma vez. O Insight procura o XML autorizado de produção na coleção `XmlMovimentacoes` do banco de destino. O mesmo lote também aceita arquivos XML. A prévia aplica as mesmas regras de conversão e o usuário confirma o banco antes da gravação. O XML já arquivado é reaproveitado na inserção da movimentação.

Chaves inválidas, notas canceladas na base, versões conflitantes e notas sem XML autorizado são exibidas individualmente. Corrija as chaves ou retire as pendentes da lista para validar os documentos encontrados. Se o XML não estiver no banco, obtenha o `nfeProc` com o emissor ou pelo serviço fiscal ao qual você tem acesso e selecione o arquivo. A consulta usa o acervo do banco e funciona sem internet.

Para importar uma lista, selecione um TXT, CSV ou TSV com uma coluna de chaves. O cabeçalho `Chave de Acesso` é opcional. Salve a coluna como texto na planilha para preservar os 44 dígitos. O Insight combina as chaves do arquivo com as chaves coladas, remove repetições e deixa a lista editável se alguma chave precisar de correção.

## Volume e desempenho

Não há cotas de quantidade de arquivos, tamanho total do lote, XML de 1 MiB ou requisição de 8 MiB. A interface envia cada arquivo como corpo binário, sem montar um JSON/base64 de todo o lote. Até quatro transferências trabalham em paralelo; essa é uma janela de execução, não um limite do lote.

Uploads de tamanho conhecido usam BLOB incremental do SQLite. Transferências sem tamanho anunciado usam arquivo temporário. O progresso e os documentos convertidos são persistidos localmente; as consultas da tela retornam páginas e o BSON é carregado apenas quando um documento é selecionado. Não há expiração de cinco minutos para essas importações.

A validação processa um XML de cada vez e grava checkpoints em grupos. O exportador percorre os documentos em disco e escreve JSON dentro de um ZIP64. O navegador faz download nativo ou usa um fluxo para arquivo; não monta o ZIP inteiro em um Blob em memória.

O limite nativo do MongoDB permanece: cada documento BSON deve caber em 16 MiB, incluindo campos e estrutura BSON. Como o XML é armazenado em um único documento nativo, não é possível gravar arbitrariamente um XML maior dentro dele. Isso é uma restrição do formato de destino. Espaço em disco, memória do maior documento e velocidade do servidor também são recursos físicos, sem cotas adicionais de lote no aplicativo.

## Uso da tela

1. Selecione a conexão do cliente no Insight.
2. Em Importar NFC-e, mantenha a identificação automática ou escolha um perfil JSON revisado no modo avançado.
3. Escolha arquivos ou uma pasta. Arquivos já guardados numa importação interrompida não são retransmitidos ao retomá-la.
4. Acompanhe os contadores e os resultados paginados. Clique no nome de um arquivo convertido para conferir seu BSON.
5. Confirme IMPORTAR seguido do nome exato do banco para inserir os documentos revisados diretamente. A exportação ZIP é opcional para conferência.

Fechar a página não cancela uma validação já enviada. As importações recentes podem ser reabertas; o botão de pausa termina o arquivo em andamento e preserva os concluídos. Após reinício do aplicativo, retome a validação pelo progresso salvo. Envios interrompidos podem ser refeitos selecionando os mesmos arquivos.

As cópias locais ficam no diretório privado data/nfce-import e os metadados em data/nfce-import.sqlite3. Use Descartar arquivos locais para remover uma importação inativa. Essa ação não remove vendas do MongoDB. Os arquivos locais, credenciais e resultados de clientes não entram no pacote de distribuição.

## Descoberta e validação

A identificação automática usa consultas limitadas em leitura para reconhecer o CNPJ do emitente, exemplos de NFC-e concluídas e seus XMLs. Resolve referências de empresa, destinatário, produto e pagamento a partir dos formatos observados na própria base. Janelas de amostragem e caches controlam trabalho e memória; não impõem uma quantidade máxima de XMLs ao usuário.

Consultas individuais têm timeout. Não existe um prazo global de trinta segundos para o lote. Se uma conexão falhar, o progresso permanece e o processamento pode ser retomado. Vínculos ambíguos, emitente ausente e formatos não suportados são apresentados como impedimentos por arquivo.

NFe sem protocolo e nfeProc correspondente com o mesmo conteúdo geram somente uma venda autorizada. A identidade e os hashes são indexados em SQLite, inclusive quando os dois arquivos aparecem distantes no lote.

Nesta versão continuam bloqueados, sem conversor específico: tributos com valores/alíquotas positivos, troco, TEF incompleto e estoque com lote/grade/serial. O valor informativo vTotTrib fica no XML; a tela nativa pode recalcular os tributos aproximados pelo cadastro. Vendedor fica não informado. Dados de cartão, parcelas, caixa ou vendedor de uma venda anterior não são copiados.

Assinatura digital e situação atual na SEFAZ não são verificadas; cancelamentos/eventos exigem outro fluxo. Identificar o esquema e conciliar totais não equivale a validar todos os getters e relatórios do SuiteG6.

## Gravação e recuperação

A inserção exige autorização explícita e usa o diário do editor. Cada escrita tem uma cópia durável antes de ser enviada ao MongoDB. O caminho interno de importação aceita documentos até o limite BSON nativo; os limites dos endpoints comuns do editor continuam separados.

Primeiro é inserido o XML, depois a movimentação. MongoDB 3.4 não oferece uma transação entre essas duas coleções. Em falha pode restar apenas um XML ou uma escrita de resultado incerto. A linha é marcada e o processamento para; ela não é repetida automaticamente. Confira o diário e as duas coleções antes de outra tentativa.

As referências observadas são reconferidas antes da escrita. Uma mudança de conexão invalida a aplicação na conexão errada. Para jobs longos, o worker renova sua sessão privada pelas verificações normais de permissão; não prolonga o token geral do navegador. Um reinício nunca retoma automaticamente gravações incertas.

## Script

    .\.venv\Scripts\python.exe tools/import-nfce.py 'C:\Digisat\SuiteG6\Dfe\lote' --output 'C:\Privado\nfce-convertidas'

Sem --profile, a descoberta lê a conexão ativa. Com --profile 'C:\Privado\perfil-nfce.json', a conversão pode ser feita offline. O script enumera a pasta sem montar uma lista com todos os conteúdos. Não há cota de arquivos ou de bytes totais. A leitura da pasta é direta, sem recursão. A pasta de saída deve ser nova.

O identificador exibido pelo script permite continuar com --resume IDENTIFICADOR. Os JSONs são escritos incrementalmente. O script nunca grava MongoDB.

## Perfil avançado

O arquivo static/nfce-profile.example.json é uma estrutura para preenchimento, não um perfil pronto. Um backup BSON não é um perfil de mapeamento. Não copie valores fiscais de outra nota sem reconstruir bases, alíquotas e valores do XML atual.

O perfil usa Extended JSON e bindings declarativos, sem eval. Exemplos: profile.company, product.reference e xml:prod/vFrete. Campos monetários BSON usam double conforme DecimalSerializerEx. Datas de emissão conservam a hora civil do serializador nativo, sem deslocar dhEmi para outro fuso.

## Verificação

Os testes sintéticos cobrem lotes com mais de 250 arquivos e mais de 8 MiB, XML de 2 MiB, páginas, hashes, recuperação em outro processo, pausa/retomada, diário grande, isolamento de usuário/conexão e bloqueio de repetição de escrita incerta.

O smoke test de navegador usou 1.201 XMLs sintéticos, 43.155.166 bytes de origem e um arquivo de aproximadamente 2 MiB. Todos foram convertidos; a paginação mostrou cinquenta resultados por vez. Foram verificados temas claro/escuro e tamanhos de desktop/tablet/celular. Nenhuma escrita em MongoDB de cliente foi feita.

A evidência estática dos formatos nativos continua em engenharia/codebase-local/projects-auto e engenharia/assemblies-descompilados/2.0.26273.6: Movimentacao, XmlMovimentacao, TipoMovimentacao, EntityMongoMap, DecimalSerializerEx, DateTimeSerializerEx e Concluido. Ela não comprova equivalência de todos os campos com builds posteriores.

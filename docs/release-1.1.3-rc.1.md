# DigiSat Insight 1.1.3 RC1

- Importar XML fiscal: NFC-e 65 e NF-e 55 em lote, com prévia e gravação direta pelo diário. A descoberta usa a base conectada, sem Studio 3T ou outra base.
- Separação por modelo fiscal na descoberta, identidade e detecção de duplicados; TipoMovimentacao da NF-e confirmado pelo exemplo XML nativo, sem presumir um enum.
- Configuração de conexão e senha pelo GUI, com MONGO_PASSWORD no arquivo local. Salvar migra referências antigas sem criar arquivos de senha separados.
- Opções no GUI para relatórios, IA e acesso ao painel; autenticação do painel aplicada no próximo início.
- Valores literais de senha, inclusive aspas, barras, Unicode e ${...}, preservados no ciclo de configuração.

Escopo: XML autorizado de saída normal. Compras de fornecedores, devoluções, outros modelos/eventos e casos fiscais sem conversor continuam bloqueados. Importação histórica não executa estoque, caixa, financeiro ou sincronização. Homologação no SuiteG6 permanece pendente; testes usam dados sintéticos e MongoDB descartável.

Validação e artefatos: ver `.release-build/rc-artifacts/verification.json` e `docs/importacao-nfce.md`.

Verificações realizadas em 16/11/2026:

- 43 testes existentes de importação e 8 testes adicionais de XML/configuração/Mongo isolado.
- 6 testes de inspeção da conexão e 67 regressões de cálculos, pagamentos, ticket, editor e estabilidade.
- Pasta local de XMLs: 194 arquivos, 97 documentos autorizados e 97 cópias sem protocolo correspondentes, sem erro estrutural. Essa leitura não gravou dados e não comprova conversão nativa de todos esses documentos.
- Executável Windows x64 executado com PATH mínimo, autenticação, prévias NFC-e e NF-e, upload acima de 1 MiB, exportação e retomada após reinício; pacote e hashes conferidos.
- Tela de importação e opções da conexão verificadas em desktop e celular, claro/escuro, sem overflow da página ou erro JavaScript. O inspector mantém as exceções de gradientes/botões aprovadas para Pulse; a identificação compacta do produto é preservada.

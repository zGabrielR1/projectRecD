# DigiSat Insight 1.1.3 RC4

Prévia do importador DFe com cadastro de produtos, títulos financeiros e conferência antes de gravar.

## O que mudou

- **Cadastro de produto por perfil**: quando um item de compra não tem mapeamento em `products`, o perfil pode declarar `product_template` com documentos nativos para `ProdutosServicos`, `ProdutosServicosEmpresa`, `Precos`, `Estoques` e `EstoquesQuantidade`. Os IDs são gerados de forma determinística por empresa+código e a gravação respeita a ordem de dependência.
- **Títulos financeiros por duplicata**: quando o perfil declara `finance.payable` ou `finance.receivable`, o Insight cria parcelas pendentes (`Situacao Pendente`, código 1) a partir das duplicatas do XML (`cobr/dup`). Vencimento e valor vêm do XML; conta, plano, espécie e histórico inicial vêm do perfil. Nenhuma baixa/quitação é inventada.
- **Exportação e aplicação multi-coleção**: ZIP e aplicação incluem cadastros de produto e Pagamentos/Recebimentos quando preparados. A aplicação registra cada escrita no diário do editor e verifica IDs existentes antes de inserir; em conflito de documento diferente, a operação para com erro explícito.
- **Prévia**: o documento selecionado mostra cadastros de produto e títulos financeiros preparados, além de parcelas informadas no XML.

## Limites e pendências

- Cadastro de produto e financeiro exigem perfil JSON revisado com modelos nativos; a identificação automática não cria esses registros.
- CF-e continua exigindo perfil com mapeamento `cfe`; sem perfil, o arquivo fica em conferência.
- A paridade com ConversorGNew continua em desenvolvimento. A verificação das gravações nas novas rotas do SuiteG6 instalado permanece pendente.
- Inserção histórica direta: não executa serviços de estoque, caixa, numeração ou sincronização do SuiteG6. MongoDB 3.4 não oferece transação entre coleções; confira o diário em caso de escrita incerta.

## Validação

- Testes sintéticos NFC-e/DFe: `tests/test_nfce_import.py`, `tests/test_nfce_jobs.py`, `tests/test_nfce_discovery.py`.
- Não substitui homologação no SuiteG6 instalado com base de cliente.
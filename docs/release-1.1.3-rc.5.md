# DigiSat Insight 1.1.3 RC5

RC de continuidade do importador DFe com intake de arquivo DBF para conferência.

## O que mudou

- **Importação de arquivo DBF (conferência)**: upload de `.dbf` aceito na tela de importação; o arquivo é lido (dBase/FoxPro básico, sem dependência externa), colunas e amostra são exibidas na prévia. A conversão nativa cega não é aplicada; o DBF fica bloqueado para revisão/mapeamento antes de gravação.
- **Base RC4 mantida**: NF-e/NFC-e, compra/entrada/saída por perfil, cadastro de produto por `product_template`, títulos financeiros por duplicatas (`finance.payable/receivable`), custo de compra, CF-e por perfil, diário de escrita e exportação.

## Limites

- DBF: intake e conferência implementados; mapeamento/conversão nativa depende de perfil revisado e evidência do layout ConversorGNew. Sem mapeamento, não grava.
- Homologação SuiteG6 instalado/base real continua pendente.
- MongoDB 3.4 sem transação entre coleções; escrita incerta exige conferência do diário.

## Validação

- Testes sintéticos: `tests/test_nfce_import.py`, `tests/test_nfce_jobs.py`, `tests/test_nfce_discovery.py`, `tests/test_dbf_import.py`.
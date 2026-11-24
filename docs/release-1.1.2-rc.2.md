# DigiSat Insight 1.1.2 RC2

- Nova ferramenta **Importar NFC-e**, disponível no menu e em `/nfce-import`.
- Seleção de XMLs em qualquer pasta, prévia BSON, validação de chave/protocolo/valores, mapeamento explícito de empresa/produtos/pagamentos e exportação das coleções para Studio 3T.
- Inserção histórica em `Movimentacoes` e preservação do XML em `XmlMovimentacoes`, com verificação de duplicatas e diário do editor. Não executa serviços de estoque, caixa, financeiro ou numeração.
- Perfil de exemplo para preenchimento e instruções incluídos no aplicativo e no guia da distribuição. O perfil específico da instalação precisa ser configurado; não é possível importar usando os placeholders.

Validação Windows isolada: executável real, autenticação, assets, prévia e exportação de NFC-e sintética sem Mongo disponível. A importação em instalação SuiteG6/base do cliente permanece não validada. O arquivo `verification.json` registra o escopo do pacote.

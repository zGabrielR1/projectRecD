# DigiSat Insight 1.1.3 RC2

- Importe movimentações históricas de NF-e e NFC-e com chaves de acesso coladas ou com uma lista TXT, CSV ou TSV de uma coluna.
- Combine chaves e arquivos XML em um lote. Chaves repetidas são consultadas uma vez. O Insight indica chaves inválidas, notas canceladas na base e XMLs ausentes ou conflitantes.
- A busca usa o XML autorizado de produção já armazenado em `XmlMovimentacoes` no banco de destino. Consulte a prévia e confirme o banco antes de aplicar. Para uma nota sem XML arquivado, obtenha o `nfeProc` com o emissor ou serviço fiscal autorizado.

Pacote portátil Windows x64, verificado com dados sintéticos e sem conexão com uma base de cliente. A validação do executável cobre autenticação, upload de XML, prévia, exportação e retomada após reinício. A homologação em SuiteG6 de cliente permanece pendente.

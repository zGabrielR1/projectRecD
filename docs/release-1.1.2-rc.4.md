# DigiSat Insight 1.1.2 RC4

- Removidas as cotas de 250 arquivos, 1 MiB por XML e 8 MiB por lote. Não há cota de quantidade ou tamanho total na importação.
- Envio binário incremental, processamento por documento, jobs persistentes em SQLite, seleção de pastas, páginas de resultados e carregamento individual de BSON.
- Pausa/retomada e recuperação após reinício. Arquivos já recebidos não são retransmitidos ao retomar a mesma importação.
- Exportação JSON incremental em ZIP64, com download para disco. O ZIP e o lote completo não são montados em memória.
- Diário de importação suporta documentos até o limite BSON nativo do MongoDB, preservando os limites próprios dos endpoints comuns do editor. Escritas incertas não são repetidas automaticamente.
- O limite nativo de 16 MiB por documento BSON e as validações fiscais/referenciais continuam aplicáveis. A importação nativa em base de cliente permanece não homologada.

Smoke de navegador: 1.201 XMLs sintéticos, 43 MB de entrada e um XML de 2 MiB; conversão e exportação conferidas, sem escrita em MongoDB de cliente. O pacote Windows é verificado em ambiente isolado.

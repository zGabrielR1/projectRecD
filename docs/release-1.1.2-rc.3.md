# DigiSat Insight 1.1.2 RC3

- Importar NFC-e passa a usar **identificação automática na conexão ativa**, sem exportação BSON ou perfil JSON obrigatório.
- Descoberta somente em leitura: CNPJ do emitente, até 100 NFC-e concluídas com XML correspondente, vínculos de produto e formatos fiscais/pagamentos observados na própria empresa. Consultas têm limites de documentos, bytes e tempo.
- Aceita até 250 arquivos no modo automático; pares NFe/nfeProc idênticos geram apenas uma nota autorizada.
- Cliente identificado não é reutilizado em notas de consumidor anônimo. Histórico de cartão, caixa, parcelas, vendedor e estoque de outra venda não são copiados.
- Perfis JSON continuam disponíveis no modo avançado. Casos sem exemplos suficientes, referências ambíguas, lotes/grades/seriais, TEF, troco e tributos positivos sem conversor específico permanecem bloqueados com motivo por arquivo.
- A descoberta não valida todos os fluxos nativos do SuiteG6. A prévia e a aplicação continuam separadas; nenhuma escrita ocorre na identificação.

Smoke test dos XMLs locais: 194 arquivos, sendo 97 NFe sem protocolo e 97 nfeProc autorizados. O emitente não existe na base atualmente configurada: a descoberta bloqueia as 97 notas e identifica as 97 cópias sem protocolo. Nenhuma venda foi gravada.

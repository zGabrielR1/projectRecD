# DigiSat Insight 1.1.3 RC6

RC do importador com mapeamento DBF manual e sugestão automática por similaridade.

## O que mudou

- **Mapeamento DBF automático por nome similar**: ao abrir um `.dbf`, o Insight sugere o campo destino para cada coluna (chave, número, data, emitente, produto, quantidade, valores, CFOP, NCM etc.) usando normalização e similaridade de nomes.
- **Mapeamento manual pelo cliente**: cada coluna pode ser ajustada em um seletor de destino; o mapeamento pode ser baixado como JSON (`insight.dbf.mapping.v1`) para revisão/reuso.
- **DBF intake/conferência**: leitura dBase/FoxPro básica, prévia de colunas/amostra, sem gravação cega sem mapeamento revisado.

## Limites

- A conversão nativa campo-a-campo do DBF para documentos SuiteG6 depende do layout do sistema de origem e do perfil/mapeamento revisado. O RC6 entrega a base de mapeamento manual/automático.
- Homologação SuiteG6 instalado/base real continua pendente.
- Guardas automáticas do DFe continuam conservadoras por segurança.
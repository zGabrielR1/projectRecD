# DigiSat Insight 1.1.3 RC7

Mapeamento DBF com presets de sistema de origem e ajuste manual.

## O que mudou

- **Presets de origem**: ARPA, SGBR, Administrador G4, Gerencial G5 e Genérico como pontos de partida para mapeamento DBF.
- **Auto + manual**: sugestão automática por similaridade de nomes de coluna; cliente pode ajustar campo destino por coluna e baixar o mapeamento JSON.
- **Conferência segura**: DBF continua em prévia/revisão; sem mapeamento revisado não há gravação cega.

## Limites

- Presets são pontos de partida; layouts reais variam por instalação/sistema e devem ser revisados.
- Conversão nativa campo-a-campo completa depende do mapeamento revisado para o layout de origem.
# Compras de componentes de composição

Acesse `/composition-purchases` pelo atalho **Compras de composição** na página inicial. Selecione empresa, período e, opcionalmente, composição ou pesquisa por código/descrição. O resultado detalha cada componente com número do movimento, série, data, fornecedor, quantidade do documento e quantidade convertida para estoque. O CSV inclui também o número do item.

## Regras

- Composições ativas vinculadas à empresa em `ProdutosServicosEmpresa`; componentes do cadastro atual, sem reconstrução histórica.
- Compras dos tipos `NotaFiscalCompra` e `NotaFiscalEletronicaEntrada`, da empresa escolhida. Período pela entrada/saída, usando emissão se aquela data estiver ausente.
- Itens cancelados ficam fora. Por padrão, somente situação concluída; situação ausente não comprova conclusão e fica fora com aviso. Desmarcar a opção permite consultar outras situações.
- Quantidade para cálculo: `EstoqueMovimentacao.Quantidade`, depois `ConversaoQuantidadePreco.Quantidade`, quando não zero; sem conversão aplicável, usa `Quantidade` do documento.
- Para cada componente obrigatório com quantidade positiva, divide o comprado pela quantidade por composição e arredonda para baixo. O menor resultado limita a composição. Opcionais não limitam.
- O resultado representa capacidade teórica pelas compras do período; não consulta saldo disponível nem desconta vendas, consumo, reservas ou montagens. Compras compartilhadas entre composições são avaliadas independentemente, sem rateio. Subcomposições não são expandidas.
- Limites: 2.000 composições, 20.000 linhas de compra, período máximo de 3.660 dias e agregação com `maxTimeMS=20000`. Consultas grandes devem ser divididas.

## Integração e validação

API protegida pela autenticação configurada do Insight: `GET /api/workspace/composition/companies`, `GET /api/workspace/composition/options?company_id=...`, `POST /api/workspace/composition/purchases`. O POST recebe `company_id`, `start_date`, `end_date`, `composition_id`, `search`, `concluded_only` e `include_optional`.

O MongoDB é somente consultado. A execução e a proveniência são salvas no SQLite local do Insight. Assets e fontes são locais; não há dependência de CDN. Testes em `tests/test_composition.py` cobrem conversão de unidade, movimento, cancelamentos, situação, limites, componentes opcionais, cálculo e persistência com conexão simulada. A conferência com compras reais do cliente continua necessária antes de fechamento oficial.

Validação em 03/11/2026: **43 testes passaram** no conjunto composição/avisos nativos/auditorias, incluindo rotas HTTP, autenticação, validação do POST e entrega de assets locais. `node --check static/composition.js` passou. Inspeção visual desktop/mobile e claro/escuro permaneceu pendente: o serviço do navegador estava indisponível. Não houve consulta ao MongoDB do cliente nesses testes.

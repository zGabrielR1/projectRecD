# ConversorGNew · Importar XML DFe

## Identidade do CF-e

A projeção `CriarCupomFiscalEletronico` executa a projeção de movimentação e documento fiscal, depois preenche `NumeroFabricacao` e `ChaveAcesso`. A checagem de duplicidade do Insight usa a chave ou a combinação de número, série e número de fabricação dentro da empresa.

Evidências REA `ev_961884b99f77fd0b7110e093238bdcb1cc2a2d3197b81eb8766f14d4a6d425cf` e `ev_ec1ff2ddd3af571126ee3d2a9f2b68d78c5a15fb8ecdff410a4319f6916d7799`.

## Perfis de pasta no Insight

O perfil JSON pode definir `source_profiles` com configurações por pasta para `purchase`, `incoming`, `outgoing` e `nfce`. Cada configuração substitui somente os campos declarados. Banco, empresa de destino e ambiente são compartilhados pelo lote. Em `purchase`, `purchase_suppliers` aceita o CNPJ do fornecedor como chave, com `customer` e `products` próprios para aquele emissor.

## Movimentações financeiras

A projeção de pagamentos e recebimentos passa número, ordem, vencimento, valor, espécie e referência da pessoa ao modelo de conversão. O histórico recebe situação, data de recebimento e valor quitado separadamente. A projeção de movimento de conta também exige tipo, crédito ou débito, competência e valor. As duplicatas do XML oferecem vencimentos e valores, mas não comprovam baixas.

Evidência REA `ev_658a4a1df89c6921279e535175f6bd81f0ac35fe5150f29ebbcb77956fe7d48b`.

No SuiteG6 2.0.26322.10, o módulo `DigisatServer.Infrastructure.Data.Main` consulta `ParcelaRecebimento` por referência no repositório de boletos e mantém `ParcelaPagamento` em seu próprio repositório. O repositório de movimentações também lê `PagamentoRecebimento.Parcelas` no documento fiscal. Portanto, gravar apenas a lista embutida não reproduz o caminho de consulta de títulos. A próxima validação precisa conferir identidade e relações nos dois formatos nativos.

## Preço de custo

Em `DigisatConversor.ServiceDfe.dll`, `ProdutoServicoService.CarregarProdutosEServicos` carrega os documentos de compra separadamente e entrega as duas opções de produto a `ProdutoServicoLista.ProcessarProdutosServicos`. Os vínculos existentes consultam `PrecoCusto` e `PrecoVenda`. O processamento também tem caminhos que selecionam uma linha de compra com `LastOrDefault` e leem `Prod.vUnCom`. Isso identifica a fonte do custo no XML: o preço unitário comercial da compra.

A inspeção estática dos vínculos de chamada não confirma a ordem de desempate entre compras, nem todos os casos em que prevalece um cadastro existente. A opção do Insight deve atualizar apenas o preço de custo do produto da empresa e deve identificar, na prévia, a nota e a linha que fornecem o custo. O registro da venda não é a fonte desse valor.

Referência local: assembly SHA-256 `d6394e20d1975376fcf73127cd94b634514c09573c1448ca2828c1fe56682d02`. Evidências REA `ev_6a4b4f8cb5f1ec44c2d73eb59e32acecdacee2ded127029778c341380dcacb37` (serviço) e `ev_810a6950aed64e8ef4f99ea0a92e47b897a11cad9867a9c753156621d1ee19ab` (projeção).

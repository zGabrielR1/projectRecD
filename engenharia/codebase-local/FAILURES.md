# Remaining exported-project compilation failures

199 of 218 C# projects compile; 19 remain. Domain.Core additionally builds from editable CIL.

Diagnostic families; syntax errors may cascade. Obfuscation attribution requires original method evidence.

| Diagnostic family | Projects |
|---|---:|
| invalid decompiler syntax or identifiers | 19 |

## DigisatServer.Application.Core

invalid decompiler syntax or identifiers; 102 diagnostics.

- `CS0426` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Core\source\Projections\DashboardProjection\PessoaProjection.cs:23` — The type name '__c_DisplayClass0_0' does not exist in the type 'PessoaProjection'
- `CS0165` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Core\source\_long\99271cb491841f68.cs:114` — Use of unassigned local variable '__9__0'
- `CS0121` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Core\source\Projections\CadastrosProjection\Mobile\ProdutoServicoEmpresaMobileProjection.cs:148` — The call is ambiguous between the following methods or properties: 'DigisatServer.Application.Core.Projections.CadastrosProjection.Mobile.ProdutoEmpresaMobileProjection.ProjectedAsMobileDto(DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos.ProdutoServicoEmpresa, bool)' and 'DigisatServer.Application.Core.Projections.CadastrosProjection.Mobile.ProdutoServicoEmpresaMobileProjection.ProjectedAsMobileDto(DigisatServer.Domain.Main.Aggregates.CadastrosAgg.ProdutosServicos.ProdutoServicoEmpresa, bool)'
- `CS1061` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Core\source\Projections\CadastrosProjection\Mobile\PessoaMobileProjection.cs:139` — 'object' does not contain a definition for 'Add' and no accessible extension method 'Add' accepting a first argument of type 'object' could be found (are you missing a using directive or an assembly reference?)
- `CS0266` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Core\source\Projections\CadastrosProjection\Mobile\PessoaMobileProjection.cs:141` — Cannot implicitly convert type 'object' to 'System.Collections.Generic.List<DigisatServer.Domain.Main.Aggregates.AuxiliaresAgg.Autorizacoes.Autorizacao>'. An explicit conversion exists (are you missing a cast?)

## DigisatServer.Application.Main

invalid decompiler syntax or identifiers; 145 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Main\source\AuxiliaresModulo\Services\JornadaRelatorioAppService.cs:189` — Identifier expected
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Application.Main\source\MovimentacoesModulo\DevolucaoAppService.cs:78` — Invalid expression term '>'

## DigisatServer.Domain.Core

invalid decompiler syntax or identifiers; 1 diagnostics.

- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Core\source\EntityBaseSpecification.cs:60` — Invalid expression term '.'

## DigisatServer.Domain.Main

invalid decompiler syntax or identifiers; 14876 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Main\source\Aggregates\AuxiliaresAgg\Abastecimentos\RegistroAbastecimento.cs:278` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Main\source\Aggregates\AuxiliaresAgg\Abastecimentos\RegistroAbastecimento.cs:278` — Unexpected character '\u0020'
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Main\source\Aggregates\AuxiliaresAgg\Abastecimentos\RegistroAbastecimento.cs:284` — Invalid expression term ''
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Main\source\Aggregates\AuxiliaresAgg\Abastecimentos\RelatorioEncerrante.cs:75` — ; expected
- `CS0839` `engenharia\codebase-local\projects-auto\DigisatServer.Domain.Main\source\Aggregates\AuxiliaresAgg\CartasCorrecao\CartaCorrecao.cs:100` — Argument missing

## DigisatServer.Infrastructure.Crosscutting

invalid decompiler syntax or identifiers; 6329 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting\source\Config\ConfiguracaoImpressoraNaoFiscal.cs:260` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting\source\Config\ConfiguracaoImpressoraNaoFiscal.cs:260` — Unexpected character '\u0020'
- `CS0839` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting\source\Model\Industrias\CalculoProducao.cs:85` — Argument missing
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting\source\Model\Industrias\CalculoProducao.cs:126` — Invalid expression term ''
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting\source\Model\Industrias\CalculoProducao.cs:154` — ; expected

## DigisatServer.Infrastructure.Crosscutting.Boleto

invalid decompiler syntax or identifiers; 101 diagnostics.

- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Boleto\source\Banco\BancoBtg.cs:798` — Invalid expression term '<'

## DigisatServer.Infrastructure.Crosscutting.Concentrador

invalid decompiler syntax or identifiers; 104 diagnostics.

- `CS0121` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Concentrador\source\Classes\Cesna\Cesna.cs:17` — The call is ambiguous between the following methods or properties: 'DigisatServer.Infrastructure.Crosscutting.Helper.HelperSwitch.TypeSwitch.Case<T>(System.Action)' and 'DigisatServer.Infrastructure.Crosscutting.Helper.HelperSwitch.TypeSwitch.Case<T>(System.Action<T>)'
- `CS1061` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Concentrador\source\Classes\Cesna\CesnaDeclaracoes.cs:29` — 'Cesna' does not contain a definition for 'FcmOpen' and no accessible extension method 'FcmOpen' accepting a first argument of type 'Cesna' could be found (are you missing a using directive or an assembly reference?)
- `CS0165` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Concentrador\source\Classes\Cesna\CesnaIdCode.cs:147` — Use of unassigned local variable '__9__0'
- `CS0234` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Concentrador\source\Classes\Bico.cs:543` — The type or namespace name 'Move' does not exist in the namespace 'DigisatServer.Infrastructure.Crosscutting.File' (are you missing an assembly reference?)
- `CS0266` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Concentrador\source\Classes\VLTech\VLTechDefault.cs:137` — Cannot implicitly convert type 'int' to 'System.IntPtr'. An explicit conversion exists (are you missing a cast?)

## DigisatServer.Infrastructure.Crosscutting.Criptografia

invalid decompiler syntax or identifiers; 369 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Criptografia\source\OpenRsa.cs:224` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Criptografia\source\OpenRsa.cs:224` — Unexpected character '\u0020'
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Criptografia\source\Hash.cs:508` — Invalid expression term ''
- `CS1026` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Criptografia\source\Elg3oIqf6ie1lOCypi\EhwmYUPcqhCAiyacJu.cs:52` — ) expected
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Criptografia\source\Elg3oIqf6ie1lOCypi\EhwmYUPcqhCAiyacJu.cs:52` — ; expected

## DigisatServer.Infrastructure.Crosscutting.Ecf

invalid decompiler syntax or identifiers; 2400 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Ecf\source\Classes\Zpm\ZpmBase.cs:1006` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Ecf\source\Classes\Bematech\BematechDefault.cs:1393` — Unexpected character '\u0020'
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Ecf\source\Classes\Daruma\DarumaDefault.cs:793` — Invalid expression term ''
- `CS0839` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Ecf\source\Classes\Bematech\BematechDefault.cs:1478` — Argument missing
- `CS1026` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Ecf\source\Classes\Bematech\BematechDefault.cs:1844` — ) expected

## DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf

invalid decompiler syntax or identifiers; 23 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf\source\TransmissaoService.cs:82` — Identifier expected
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf\source\TransmissaoService.cs:83` — Invalid expression term '<'
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf\source\TransmissaoService.cs:606` — Unexpected character '\u0020'
- `CS0839` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf\source\TransmissaoServidorService.cs:58` — Argument missing
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.EnvioPafEcf\source\TransmissaoServidorService.cs:59` — ; expected

## DigisatServer.Infrastructure.Crosscutting.Farmacia

invalid decompiler syntax or identifiers; 668 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Farmacia\source\Pbms\VidaLink\ProdutoVendidoVidaLink.cs:30` — Identifier expected
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.Farmacia\source\Pbms\VidaLink\ProdutoVendidoVidaLink.cs:30` — Invalid expression term '>'

## DigisatServer.Infrastructure.Crosscutting.NetFramework

invalid decompiler syntax or identifiers; 105 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.NetFramework\source\Cryptography\AESCrypto.cs:258` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.NetFramework\source\Cryptography\AESCrypto.cs:258` — Unexpected character '\u0020'
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.NetFramework\source\Distribuicao\LancadorAtualizador.cs:177` — Invalid expression term ''
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.NetFramework\source\Distribuicao\ClienteDistribuicao.cs:290` — ; expected
- `CS0839` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.NetFramework\source\Cryptography\AESCrypto.cs:311` — Argument missing

## DigisatServer.Infrastructure.Crosscutting.RegistroSistema

invalid decompiler syntax or identifiers; 64 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.RegistroSistema\source\Function\ControleDeHash.cs:103` — Identifier expected
- `CS1056` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.RegistroSistema\source\Function\ControleDeHash.cs:103` — Unexpected character '\u0020'
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.RegistroSistema\source\Models\AtivacaoBase.cs:42` — Invalid expression term ''
- `CS1026` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.RegistroSistema\source\Service\RegistroWindowsService.cs:73` — ) expected
- `CS1002` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Crosscutting.RegistroSistema\source\Models\AtivacaoBase.cs:42` — ; expected

## DigisatServer.Infrastructure.Data.Core

invalid decompiler syntax or identifiers; 17 diagnostics.

- `CS0136` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Core\source\RepositorySincronizacao.cs:56` — A local or parameter named 'cursor2' cannot be declared in this scope because that name is used in an enclosing local scope to define a local or parameter
- `CS0426` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Core\source\RepositoryBaseWrite.cs:80` — The type name '__c_DisplayClass6_0' does not exist in the type 'RepositoryBaseWrite<TEntity>'
- `CS0103` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Core\source\RepositoryBaseWrite.cs:84` — The name 'methodof' does not exist in the current context
- `CS0119` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Core\source\RepositoryBaseWrite.cs:84` — 'ObjectId' is a type, which is not valid in the given context
- `CS0117` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Core\source\RepositoryBaseWrite.cs:84` — 'RepositoryBaseWrite<TEntity>' does not contain a definition for '__c_DisplayClass6_0'

## DigisatServer.Infrastructure.Data.Main

invalid decompiler syntax or identifiers; 1280 diagnostics.

- `CS1001` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Main\source\Repositories\ReceitaRepositoryRead.cs:57` — Identifier expected
- `CS1525` `engenharia\codebase-local\projects-auto\DigisatServer.Infrastructure.Data.Main\source\Repositories\RegistroPafEcfRepositoryWrite.cs:139` — Invalid expression term 'string'

## Modulo.Movimentacoes

invalid decompiler syntax or identifiers; 160 diagnostics.

- `CS0136` `engenharia\codebase-local\projects-auto\Modulo.Movimentacoes\source\Entradas\ViewModels\MovimentacaoEntradaListaViewModel.cs:298` — A local or parameter named 'obj' cannot be declared in this scope because that name is used in an enclosing local scope to define a local or parameter
- `CS0165` `engenharia\codebase-local\projects-auto\Modulo.Movimentacoes\source\Entradas\ViewModels\MovimentacaoEntradaListaViewModel.cs:309` — Use of unassigned local variable 'obj'
- `CS1656` `engenharia\codebase-local\projects-auto\Modulo.Movimentacoes\source\Entradas\ViewModels\NotaFiscalCompra\CompraFormacaoPrecoVendaViewModel.cs:360` — Cannot assign to 'formacaoPrecoVendaItem' because it is a 'foreach iteration variable'
- `CS0426` `engenharia\codebase-local\projects-auto\Modulo.Movimentacoes\source\Entradas\ViewModels\MovimentacaoEntradaListaViewModel.cs:717` — The type name '__c_DisplayClass51_0' does not exist in the type 'MovimentacaoEntradaListaViewModel'
- `CS0103` `engenharia\codebase-local\projects-auto\Modulo.Movimentacoes\source\Saidas\ViewModels\NotaManual\NotaManualViewModel.cs:565` — The name 'item2' does not exist in the current context

## Modulo.Pessoas

invalid decompiler syntax or identifiers; 63 diagnostics.

- `CS0426` `engenharia\codebase-local\projects-auto\Modulo.Pessoas\source\ViewModels\PessoaFichaConsultarServicoCreditoViewModel.cs:65` — The type name '__c_DisplayClass5_0' does not exist in the type 'PessoaFichaConsultarServicoCreditoViewModel'
- `CS0121` `engenharia\codebase-local\projects-auto\Modulo.Pessoas\source\ViewModels\PessoaFichaViewModelDetalhes\Cashback\CashbackViewModel.cs:56` — The call is ambiguous between the following methods or properties: 'Otus.Infraestrutura.Interfaces.IImpressoraNaoFiscalService.Imprimir(DigisatServer.Infrastructure.Crosscutting.Mvvm.Interfaces.IRelatorio)' and 'Otus.Infraestrutura.Interfaces.IImpressoraNaoFiscalService.Imprimir(DevExpress.XtraReports.IReport)'
- `CS0030` `engenharia\codebase-local\projects-auto\Modulo.Pessoas\source\ViewModels\PessoaFichaViewModelDetalhes\TiposCadastro\TiposJuridica\TipoTaxa.cs:20` — Cannot convert type 'Otus.Modulo.Pessoas.ViewModels.PessoaFichaViewModelDetalhes.TiposCadastro.TiposJuridica.TipoTaxa' to 'int'
- `CS7036` `engenharia\codebase-local\projects-auto\Modulo.Pessoas\source\ViewModels\PessoaFichaViewModelDetalhes\EnderecosEletronico\EnderecoEletronicoViewModel.cs:24` — There is no argument given that corresponds to the required parameter 'dto' of 'ViewModelDetalheBase<EnderecoEletronico, EnderecoEletronicoListaManutencaoDetalhe, EnderecoEletronicoFichaManutencaoDetalhe>.ViewModelDetalheBase(List<EnderecoEletronico>)'
- `CS0518` `engenharia\codebase-local\projects-auto\Modulo.Pessoas\source\ViewModels\PessoaFichaViewModelDetalhes\EnderecosEletronico\EnderecoEletronicoViewModel.cs:35` — Predefined type 'System.Range' is not defined or imported

## Modulo.PontoDeVendas

invalid decompiler syntax or identifiers; 202 diagnostics.

- `CS0426` `engenharia\codebase-local\projects-auto\Modulo.PontoDeVendas\source\ViewModels\PontoDeVendaViewModel.cs:169` — The type name '__c_DisplayClass16_0' does not exist in the type 'PontoDeVendaViewModel'
- `CS1729` `engenharia\codebase-local\projects-auto\Modulo.PontoDeVendas\source\Aggregates\MedicamentoFarmaciaPopularLocal.cs:20` — 'MedicamentoLocal' does not contain a constructor that takes 0 arguments
- `CS0518` `engenharia\codebase-local\projects-auto\Modulo.PontoDeVendas\source\Aggregates\MedicamentoFarmaciaPopularLocal.cs:24` — Predefined type 'System.Range' is not defined or imported
- `CS0175` `engenharia\codebase-local\projects-auto\Modulo.PontoDeVendas\source\Aggregates\MedicamentoFarmaciaPopularLocal.cs:24` — Use of keyword 'base' is not valid in this context
- `CS0103` `engenharia\codebase-local\projects-auto\Modulo.PontoDeVendas\source\Aggregates\MedicamentoFarmaciaPopularLocal.cs:24` — The name 'ctor' does not exist in the current context

## Modulo.Relatorios

invalid decompiler syntax or identifiers; 2689 diagnostics.

- `CS0165` `engenharia\codebase-local\projects-auto\Modulo.Relatorios\source\ViewModels\RelatorioListaViewModel.cs:65` — Use of unassigned local variable '__9__2'
- `CS0426` `engenharia\codebase-local\projects-auto\Modulo.Relatorios\source\ViewModels\RelatorioFichaViewModel.cs:359` — The type name '__c_DisplayClass55_0' does not exist in the type 'RelatorioFichaViewModel'
- `CS0176` `engenharia\codebase-local\projects-auto\Modulo.Relatorios\source\Reports\Devolucao\DevolucoesPorPeriodo.cs:243` — Member 'TextAlignment.TopLeft' cannot be accessed with an instance reference; qualify it with a type name instead
- `CS0104` `engenharia\codebase-local\projects-auto\Modulo.Relatorios\source\Reports\Delivery\EntregasPorEntregador.cs:394` — 'FontStyle' is an ambiguous reference between 'System.Drawing.FontStyle' and 'System.Windows.FontStyle'
- `CS1503` `engenharia\codebase-local\projects-auto\Modulo.Relatorios\source\Reports\Delivery\EntregasPorEntregador.cs:394` — Argument 1: cannot convert from 'string' to 'System.Drawing.FontFamily'

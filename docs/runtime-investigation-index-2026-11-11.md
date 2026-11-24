# Índice de verificações em runtime — 11/11/2026

As rodadas abaixo executam métodos originais dos DLLs locais, com entradas e fronteiras de I/O sintéticas. Não são homologação do produto completo, teste do servidor remoto ou evidência de alteração nos dados de um cliente. Nenhum patch foi aplicado à instalação, ao banco ou aos cálculos retornados pelo Insight.

| Rodada | Prova observada | Escopo executado | Reprodução |
| --- | --- | --- | --- |
| [Callbacks e rateio inicial](runtime-reports-2026-11-11.md) | 90/90 ensaios concorrentes originais divergiram; 30 seriais e 120 controles clone exatos; resíduo de centavo observado | DLL atual, três callbacks e helper de rateio; Framework nativo; 31 assertions e 240 ensaios | `python engenharia/rea-runtime-2026-11-11/run_reports.py` |
| [Tail de sincronização](runtime-sync-2026-11-11.md) | IsRunning permanece ativo após falha; replay falho conclui Task; cursor parcial repete eventos | Data.Core atual/histórico, .NET 10 e Framework nativo, seis assertions em cada uma de quatro execuções | `python engenharia/runtime-sync-2026-11-11/run_checks.py` e `python engenharia/runtime-sync-2026-11-11/run_framework_checks.py` |
| [Rateio desde itens](runtime-allocation-2026-11-11.md) | Cadeia real reproduz diferença de R$ 0,01; cancelamentos, denominador, totais não positivos e prioridade de nomes conferidos | Três helpers atuais, itens e getter de movimentação sintéticos; Framework nativo; 17 assertions | `python engenharia/runtime-allocation-2026-11-11/run_checks.py` |
| [Emissor de replicação](runtime-replication-2026-11-11.md) | Delete físico não é enviado neste overload, mas checkpoint avança; destinatários vazios também avançam; marcadores de modificadores diferem | Data.Core atual/histórico, destinatários ICanTell e checkpoint em memória; Framework nativo; 13 assertions por build | `python engenharia/runtime-replication-2026-11-11/run_checks.py` |
| [Receptor e repositório](runtime-receiver-2026-11-11.md) | Texto literal `$set`/`$unset` dentro de valores escolhe update; `$inc` com marcador escolhe replacement; métodos reais repassam essas escolhas ao driver | Handler privado atual e RepositorySincronizacao originais; construtores ignorados; interfaces de banco em memória; Framework nativo; 22 assertions | `python engenharia/runtime-receiver-2026-11-11/run_checks.py` |

Os relatórios vinculam resultados, fontes dos harnesses, logs e manifestos com hashes. Cada runner fixa a identidade principal antes de executar; reexecutar substitui os resultados locais. Preserve as saídas anteriores antes de comparar rodadas. “Assertion passou” em muitos casos significa que o comportamento problemático previsto foi reproduzido.

## Lacunas que permanecem

- Propagação e aplicação integrada entre atores reais, transporte, ordenação replay/live e confirmação durável de gravação.
- Carga completa com origem vazia, persistência dos payloads modificadores em MongoDB isolado e retry após entrega parcial.
- Emissão integral dos relatórios, identidade do servidor remoto, alcance dos documentos com itens negativos e agrupamento final de vendedores homônimos.
- Instalação limpa de Framework 4.6: os testes nativos usaram o Framework 4.8 instalado e CLR4; os testes .NET 10 têm runtime separado.

O capturador PTY do REA nesta versão não suporta Windows. Os ensaios usam runners locais com timeout, logs e hashes; não se atribui a eles um Evidence ID de captura REA. Evidências estáticas anteriores e suas identidades continuam no [relatório REA](rea-investigation-2026-11-11.md).

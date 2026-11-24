# Reconstruction readiness and independent Insight audits

**Atualização após a RC6:** a classe inteira de relatórios e o módulo Data.Core agora compilam de C# contra referências Framework4.6. O blocker de compilação da classe foi resolvido. Veja o [resultado atual e trabalho restante](rc6-e-recompilacao-2026-10-30.md); a evidência do wrapper mensal abaixo descreve a etapa anterior.

## What the monthly source build proves

The recovered monthly method compiles against the **actual extracted Digisat domain and repository interfaces**. Its method text was copied unchanged into a new laboratory class; its constructor and namespace provide isolation. The corrected variant adds a clone at the array-merge callback entry.

This proves that the selected source reconstruction is type-correct against those assemblies under the laboratory compiler/reference set. The original/fixed output DLLs and source hashes are recorded in [source-rebuild.json](../engenharia/reconstrucao-2026-10-30/source-rebuild.json), and [method-source-match.json](../engenharia/reconstrucao-2026-10-30/method-source-match.json) checks the unchanged method text.

It does **not** prove that this new class is registered with the original dependency injector or WCF host, that the whole application accepts it, or that it returns the correct customer report. The complete monthly method has not run against the actual repositories. It was compiled using Linux .NET 8 references, whereas production targets .NET Framework 4.6. The callback's actual CIL and corrected variants have been executed separately with synthetic data.

| Validation layer | Current state | Remaining evidence |
| --- | --- | --- |
| Build identity | Installer and investigated DLL hashes known | Hashes and endpoints of the customer's actual installed server/client |
| Source reconstruction | Full Data.Core module and full report-service class now compile | Recover remaining Application.Main and other required first-party modules |
| Calculation mechanism | Original callback fails under concurrency; corrected callbacks pass isolated trials | Run the complete method, repository selection and DTO construction together |
| Source compatibility | New full-component builds use Framework4.6 references and actual dependencies | Execute/homologate on the supported Framework runtime and Windows integration |
| Integration | Isolated experimental CIL DLL exists | Test dependency injection, WCF contracts, loading, serialization, metadata/protection checks and report designer |
| Business parity | Synthetic filters, seller attribution and monthly sums exercised | Frozen representative data, all required report filters, subtype/materialization and seller-name behavior |
| Deployment | Nothing installed | Approved vendor build, Windows homologation and controlled rollout/rollback |

The calculation fix is well supported. Whole-application compatibility remains a separate engineering question. A matching interface signature cannot establish the latter.

## What remains to rebuild the whole application

1. **Recover the required remaining first-party modules and resources.** Only the first installer payload and one extra MVVM dependency were selected here. The UI/report designer and other product components need their own inventory and identity checks. Original third-party binaries can remain dependencies; their source is not necessary to reconstruct the relevant first-party projects.
2. **Repair decompiler output systematically.** The selected report-service class's generated-expression blockers have now been repaired; other recovered modules still need their own compiler/CIL review. Resolve each remaining case preserving evaluation order, null behavior and filters. A compiler error in generated C# is not evidence of an original application bug. The full-service failure log remains available.
3. **Reconstruct build configuration.** Establish project references, resources, framework targets, public API/assembly identities, configuration transforms and required build tooling. Assembly namespaces suggest logical layers but do not reveal the original solution layout or build scripts.
4. **Build and compare artifacts.** Compile with the production target references; compare public signatures and selected behaviors against the supplied assemblies. Review metadata changes and any application protection/verification mechanisms. The experimental CIL rewrite is not a full-source rebuild.
5. **Homologate the Windows integration and representative data.** These tests require an isolated installation or an appropriate Windows runner. A production workstation is unnecessary; a test environment with the correct runtime, dependencies and a frozen database copy is sufficient.

The raw installer supports further static recovery now. Runtime integration and customer incident attribution need an additional environment and captured evidence; more decompilation alone cannot supply them.

## Concrete monthly acceptance tests

Use the same frozen data and exact company, endpoint, year, origin, status, seller and remaining report options throughout:

- Compare every seller/month cell against an independently computed serial reference; compare missing rows separately from rows containing zero.
- Include fewer than, exactly, and more than 500 movements; multiple parallel partitions contributing to the same seller/month; all twelve months and year boundaries.
- Cover cancelled items, auxiliary items with every stock/finance flag combination, multiple sellers in one document, unassigned sellers, discounts, taxes, expenses, negative/zero values and NFC-e financial duplicates.
- Verify the repository specifications and materialization: active/company filtering, origin/status special cases, normal purpose, date serialization, item inheritance and computed `TotalLiquido`. The current snapshot audit deliberately does not guess these mappings from raw BSON.
- Run the original and corrected full report repeatedly against the frozen data, with supported worker counts and on the production Framework runtime. A safe implementation must match every time. An original run that happens to match is not proof of safety.
- Check seller-name lookup, DTO fields, report sorting and printed totals, then repeat through WCF from server and terminal against the same confirmed endpoint.

The separate [synchronization report and runnable replica](bugs-sincronizacao-prototipo-2026-10-30.md) explain the corresponding delete/replay acceptance tests. The direct overload's reachability and the real replay/live handoff still need observation before attributing customer data loss.

## Insight now has independent calculations, not just notices

Two read-only endpoints are registered in the main application:

| Endpoint | Capability |
| --- | --- |
| `POST /api/audit/monthly` | Compute deterministic seller/month totals from a normalized, preselected document snapshot; explain inclusions/exclusions; compare supplied native runs |
| `POST /api/audit/unit-prices` | Compute unit price from total/quantity; identify fractional-price differences; treat quantity zero as undefined |

These are backend/API and offline-CLI capabilities. No new visual screen or automatic Mongo/WCF snapshot capture was added. Existing native-query responses link the relevant independent audit through structured metadata. The existing native totals are preserved.

### Monthly audit input and output

The [example snapshot](../engenharia/auditoria-calculos-2026-10-30/snapshot-example.json) contains only synthetic records. Its schema requires:

- Company/year and a description of the repository selection already applied by the exporter.
- Unique movement references, emission month, and item-level seller and normalized `TotalLiquido`.
- Explicit cancellation, auxiliary-document, stock and finance flags. Missing flags are rejected rather than silently assumed.
- Optional selected seller and optional native seller/month results from one or more runs.

The audit reproduces the recovered item-selection and seller/month reduction rules over that explicit input. It uses decimal arithmetic, no floating-point summation, and no parallel shared arrays. Duplicate movement references, duplicate native seller rows, invalid decimals and over-limit snapshots are rejected. Accumulations requiring inexact .NET Decimal representation are rejected rather than silently rounded. Inputs are bounded to 5,000 documents/50,000 items; larger data needs a separately designed streaming/snapshot workflow, not silently truncated totals.

The response includes:

- Exact decimal strings for twelve months and annual totals per seller.
- Per-reason excluded-item counts and bounded per-document contribution explanations.
- Differences from each supplied native run, including missing rows and `native - reference` values.
- Whether the supplied native runs vary; this is evidence about the supplied values, not proof of concurrency as their cause.
- A SHA-256 identity of the canonical calculation input. Document/item reordering does not change it; comparison values and display limits are excluded. It does not prove that an exporter captured an entire database atomically.
- Explicit coverage fields: completeness is **declared**, not independently verified; raw BSON mapping and remote build identity remain **unverified**.

Explanation/difference display limits do not change full totals or the full discrepancy count. If differences are hidden by a display limit, the comparison still fails and reports truncation.

The normalization boundary matters: the original method receives objects materialized by the domain/repository. A stored item may not contain `TotalLiquido` directly, and raw discriminator/fiscal-operation fields may require deserialization. A future live adapter must validate these operations against the recovered domain before claiming full native-scope parity. This endpoint gives a transparent, correct reference for the supplied normalized input; it does not claim to have solved that adapter already.

### Unit-price audit

The [example](../engenharia/auditoria-calculos-2026-10-30/unit-prices-example.json) submits quantity `0.5`, net total `10` and reported price `10`. The independent reference is `20.00`, while the recovered `total / Max(1, quantity)` expression yields `10.00`. The response reports both expressions and the difference without replacing the reported value.

Prices truncate toward zero to two decimals, following the recovered sold-items report's truncation rule. Zero quantity has no mathematically defined unit price; the audit reports it explicitly. Negative quantities remain visible. Input magnitudes/scales are bounded; this endpoint is not an unrestricted simulation of every .NET Decimal extreme or every report grouping.

### Run without a server or database

With the existing Insight Python dependencies:

```sh
/tmp/digisat-insight-tests/bin/python tools/audit-calculation-snapshot.py monthly \
  engenharia/auditoria-calculos-2026-10-30/snapshot-example.json
/tmp/digisat-insight-tests/bin/python tools/audit-calculation-snapshot.py unit-prices \
  engenharia/auditoria-calculos-2026-10-30/unit-prices-example.json
```

Both commands use the same functions as the endpoints. Neither connects to a database, executes native code, repairs synchronization nor changes the submitted figures. Example outputs are saved beside the inputs.

## Verification performed

- **30 Python tests passed** across independent calculation audits, upstream notices and native-report integration. Tests include actual ASGI request validation, application route registration, exact cents, per-item seller attribution, all twelve months, cancellation/fiscal flags, duplicates, missing-versus-zero rows, bounded differences, overflow rejection and fractional/negative/zero quantities.
- The API suite passed outside the sandbox. A sandbox-only stall in thread-to-event-loop wakeup prevented the ASGI case completing inside it; the same unmodified application/test passed outside. No actual Mongo/WCF service was used.
- A separately compiled [C# normalized-input oracle](../engenharia/auditoria-calculos-2026-10-30/MonthlySnapshotOracle.cs) matched the Python audit on **four cases**, each with **1,201 documents and 8,407 items**, twelve months, mixed sellers, flags and negative values. See [cross-language-results.json](../engenharia/auditoria-calculos-2026-10-30/cross-language-results.json). This validates the normalized arithmetic/filter boundary, not raw BSON/domain/repository parity.
- The stronger synchronization prototype passed **29 assertions**, compiling the full recovered direct handler and exercising original/corrected modes. Its selected replay/live schedule is a controlled possibility, not a recorded customer event.

Reproduce the independent cross-check with:

```sh
/tmp/digisat-insight-tests/bin/python tools/check-calculation-audit.py /tmp/digisat-monthly
python engenharia/sincronizacao-prototipo-2026-10-30/run_replica.py /tmp/digisat-monthly
```

The current work supplies useful independent references and reproducible development cases. Full Windows homologation and evidence of the specific customer's production paths remain the acceptance boundary.

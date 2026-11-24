# RC6 shipped, then source reconstruction advanced

The requested order was followed: the Insight RC was published first; vendor source reconstruction resumed afterward.

## Insight 1.1.1 RC6

[Published prerelease](https://github.com/zGabrielR1/projetoIADigisat/releases/tag/v1.1.1-rc.6), commit `4aeabf7bd4d64dbcd4b187767090bc47fac6d01a`, branch `release/1.1.1-rc.6`.

[Windows CI](https://github.com/zGabrielR1/projetoIADigisat/actions/runs/41156152400) passed:

- 111 Python source tests, plus JavaScript arithmetic/export checks.
- Real Windows x64 portable build and PE execution with minimal PATH.
- Authentication and both independent calculation APIs, invalid-input handling, native notices and static-asset checks.
- ZIP file allowlist, credential-free configuration and inner/outer SHA-256 verification.

The [portable ZIP](https://github.com/zGabrielR1/projetoIADigisat/releases/download/v1.1.1-rc.6/DigiSatInsight-1.1.1-rc.6-win64.zip) is about 26.9 MB. Its SHA-256 is `e3d02a0d9ac088e611256dd0e09c11481364e9054d1577b12233fdf7db5fa647`; executable SHA-256 is `b7a9bf9394b434b149046d2de3275ff4be1cdc80b5230f8df7432945af602658`. Downloaded artifacts were independently checked again on Linux in `release/DigiSatInsight-1.1.1-rc.6-win64/`.

The published `verification.json` identifies the profile as **windows-isolated**. Installed Digisat/WCF, customer-data parity, live Mongo/CRUD acceptance and full visual regression were not claimed. The original live-environment verifier/finalizer remain separate and now require the new audit gates too. The public release snapshot adds no vendor installer, recovered vendor source, private configuration or database files. Existing source/workspace changes were preserved; publication used an isolated curated branch rather than committing the working tree wholesale.

The audits remain API/CLI capabilities over normalized, preselected snapshots; they do not automatically capture Mongo/WCF data. [Usage and limits](auditorias-independentes.md).

## Additional source reconstruction completed after release

| Component | New result |
| --- | --- |
| Infrastructure.Data.Core | Entire 2,892-line module source-compiles against Framework4.6; 66 original named type surfaces match; resources/references match; 25 original +25 source-rebuilt behavior assertions produce equal results |
| MovimentacaoSaidaRelatorioAppService | Entire 4,109-line service plus its recovered internal helper source-compiles, original and corrected variants; 74 declared method signatures, fields and interfaces match |
| Selected calculation fixes | Corrected source-compiled callbacks pass 120 parallel trials; original source preserves the race in all 90 parallel trials of this run |
| Source project linkage | Corrected service compiles against source-rebuilt Data.Core; same service DLL bytes and 120 further corrected callback trials pass |

These are **C# source builds targeting .NET Framework 4.6**, not the earlier CIL-rewritten application DLL. The .NET 8 host runs Roslyn and selected tests on Linux; target references are Framework4.6. No SDK or Windows installation was added locally. The Data.Core runtime comparisons include actual loaded original/rebuilt replication classes with in-memory Mongo boundaries. The report tests execute selected compiled callback CIL, not the full report/database object graph.

Details, source repairs, runners and remaining work:

- [Full Data.Core reconstruction](recompilacao-data-core-2026-10-30.md)
- [Full report-service reconstruction](recompilacao-relatorios-2026-10-30.md)

Vendor reconstruction outputs remain local and separate from the public Insight release.

## What is still unfinished

The complete SuiteG6 application has not been rebuilt. Application.Main contains much more than this class; other required UI, host, application, domain and repository modules remain dependencies. Project graph, assembly/DI/WCF integration, all-method semantic parity, database/materialization behavior and actual printed reports still need work.

The remaining acceptance environment is an isolated Windows installation with matching vendor components and a stable database copy. Hosted Windows CI verified **Insight**, not the reconstructed vendor application. Neither the source builds nor the selected Linux tests prove production compatibility or the customer's synchronization incident.

The [production-validation checklist](validacao-reconstrucao-e-auditoria-independente.md) still applies to integration/data acceptance; its old full-class compilation blocker is now superseded by these new source builds.

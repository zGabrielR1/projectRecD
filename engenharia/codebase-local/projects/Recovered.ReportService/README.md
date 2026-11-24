# Current reconstructed report-service component

This project contains the entire selected current `MovimentacaoSaidaRelatorioAppService` class and its internal `Parcial` helper, recovered from the installed Application.Main DLL SHA-256 `f27cb00fdbee2851ad9119a32a435943c58c072ca102fe88ef0e3f001938cd9d`. It is a component, not the complete Application.Main assembly or a drop-in SuiteG6 application.

## Edit and build

Edit `MovimentacaoSaidaRelatorioAppService.cs` or `Parcial.cs`, then run from this workspace:

```powershell
python tools/build_reports.py --compile-only
```

After building the sibling Data.Core project, add `--source-linked` to compile the report component against its rebuilt DLL. The project `Build` target defaults to that source link; use `/p:UseRebuiltDataCore=false` for its recorded original dependency instead. `evidence/build-manifest.json` records the exact selected Data.Core hash. This proves compilation linkage, while the selected report helper checks do not exercise Data.Core behavior.

The default also compiles existing local source. It never regenerates or overwrites edited C#. The project `Build` target calls this offline runner. No NuGet restore or download is used. Compiling requires the existing Windows .NET Framework runtime/reference DLLs, installed Roslyn compiler and local `references/`, `baseline/` and `tooling/` snapshots.

The runner compiles `bin/Recovered.ReportService.dll`, compares 74 declared method signatures, fields and interfaces against the exact original, then runs 16 selected behavior assertions on original and rebuilt helpers under native Windows CLR4. A public API edit or change to the exercised baseline behavior will fail those baseline checks; inspect the logs rather than assuming all edits must preserve them.

## Explicit recovery

```powershell
python tools/build_reports.py --recover
```

Recovery refuses to overwrite an existing local main source file. `--recover --force` explicitly replaces recovered sources and reference/baseline/tool snapshots from the pinned installed artifact. It is not the normal edit/build command.

The original module targets Framework 4.6. This local build targets Framework 4.8 using the installed Windows runtime assemblies; no clean Framework 4.6 equivalence is claimed. The C# recovery repairs 553 generated identifier occurrences and five inconsistent lambda-parameter declarations only. The shared-array mutation and fractional-quantity formula remain present. There are no intentional calculation fixes in the recovered baseline.

`evidence/build-manifest.json`, `api-comparison.json`, `behavior.json`, compiler logs and `source-repairs.patch` preserve the provenance and selected verification. Assembly dependencies still supply domain/DTO/interface implementations as original DLLs. Full report execution, repositories, MongoDB, UI, WCF and production replacement have not been validated.

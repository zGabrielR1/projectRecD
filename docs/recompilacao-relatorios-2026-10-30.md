# Report-service class: complete C# source rebuild

**The full `MovimentacaoSaidaRelatorioAppService` class now compiles from C# against .NET Framework 4.6 references**, together with its recovered internal `Parcial` helper. Both an original-behavior version and a calculation-corrected version were built. This resolves the previous full-class compilation blocker.

This is a complete selected class/component, **not the entire `DigisatServer.Application.Main.dll`** or the complete SuiteG6 application. The rebuilt DLL is deliberately named `Recovered.FullReportService.dll`; it cannot replace the much larger application assembly by itself. No production binary was changed.

Files are in [engenharia/recompilacao-relatorios-2026-10-30](../engenharia/recompilacao-relatorios-2026-10-30/). Recovered vendor sources and review bundles remain local and are ignored by Git. They were not published with the Insight RC.

## Original artifact and recovery

Input assembly SHA-256: `471908afa8e8ae43aaab13722e7820006c4b7a221600983abc42e9c3b9039af4`, from the supplied installer. The rebuild runner rejects another input hash.

The source is recovered using ILSpy 8.2 with `QueryExpressions=false`. This leaves LINQ as method chains and makes the intermediate expression structure explicit. It still emits illegal C# names such as `<>h__TransparentIdentifier0`, and five lambdas mix explicit and implicit parameter typing.

The repeatable repair is narrow:

1. Rename **553 occurrences** of generated transparent-identifier names consistently in declarations, anonymous properties and references.
2. Remove the explicit type annotation from the typed side of **five mixed-typing lambdas**, letting the existing strongly typed LINQ operations infer both parameters.
3. Recover the real internal `Parcial` class from the same DLL instead of inventing a substitute or widening the original assembly's access checks.

The result is **4,109 lines** for the service, plus its helper. There are no empty method placeholders, domain stubs, removed report methods or CIL rewriting in these source builds. [source-repairs.patch](../engenharia/recompilacao-relatorios-2026-10-30/source-repairs.patch) preserves the exact syntax repairs. Syntax and signature recovery do not by themselves prove semantic equivalence of every method.

## Correct framework target

Roslyn runs on the existing portable .NET 8 tool host, but compiles the business component with `-nostdlib+` and **.NET Framework 4.6 reference assemblies**. The build does not substitute .NET 8 core libraries for the production target. The assembly carries `.NETFramework,Version=v4.6` metadata; REA independently classifies both outputs as Framework CIL/AnyCPU with complete identity metadata.

The reference package is `Microsoft.NETFramework.ReferenceAssemblies.net46` 1.0.3. No SDK, Windows installation, Wine or system-wide compiler was installed. Framework reference DLLs permit compilation on Linux; they do not execute Framework/Windows services.

## Baseline and corrected builds

| Output | SHA-256 | Behavior intent |
| --- | --- | --- |
| `build/Recovered.FullReportService.dll` | `8e9846841e491e630658439190fb1bbe3bf2699552b55da806066739c4c0fc78` | Preserve original source behavior, including known defects |
| `build/Recovered.FullReportService.fixed.dll` | `8662e7e2cbd0fd4f16898c7196b98bb9f976905d44bb8d5dc29ddd6e45234171` | Clone three shared monthly arrays before merging; replace three fractional-quantity denominator sites |

The calculation changes are separate from syntax repairs in [calculation-fixes.patch](../engenharia/recompilacao-relatorios-2026-10-30/calculation-fixes.patch). The corrected variant uses a small internal `QuantityGuard` helper; a zero denominator retains the original fallback of 1, while nonzero fractional/negative quantities retain their actual value. This preserves the earlier correction's scope; it does not make a new product-policy decision for zero quantities.

The component's references include the real Application.Core/Main.Dto, Domain.Core/Main, Crosscutting, Data.Core, MongoDB.Bson and DevExpress MVVM assemblies. It has **no assembly reference back to the original Application.Main**: the selected class and helper are source-compiled. Other layers remain dependency DLLs.

## Signature and behavior checks

The original type has 75 direct method definitions, one of which is a private compiler-generated lambda. In the source rebuild that lambda is generated under a different compiler container. The comparator therefore checks **all 74 declared method signatures/parameter flags/defaults**, field identities/flags/constants and implemented interfaces, and records the excluded private generated method explicitly. It refuses to exclude a non-private method under that rule.

Both source variants pass these comparisons. Compiler-generated types/tokens are not expected to be identical. This check does not compare all attributes or instruction sequences.

Selected calculation behavior is tested from the **CIL emitted by the new source compiler**, with runtime metadata relocated into an isolated .NET 8 harness:

- All three original-behavior callbacks mutate and return the same array. Across 120 stress trials, the 30 single-worker controls passed; all 90 parallel trials diverged in this run.
- All three corrected callbacks leave the input array unchanged and return the correct replacement. **All 120 trials passed**, across degrees 1, 2, 3 and 8, with 10,000 partial updates and twelve exact monthly cells per trial.
- The corrected compiled quantity helper returns the correct denominator for 0.5, zero and -2. Its fractional example yields 20 rather than 10, and all three intended price-selector call sites reference the tested helper.

The compiler output is read with Mono.Cecil; only admitted type/member/branch/local operands are translated for execution. The full report class, repository and domain object graph are **not** loaded/executed by this callback harness. It does not exercise all report methods or the printed report.

Recorded results: [original-source-tests.json](../engenharia/recompilacao-relatorios-2026-10-30/original-source-tests.json), [fixed-source-tests.json](../engenharia/recompilacao-relatorios-2026-10-30/fixed-source-tests.json), and [API comparisons](../engenharia/recompilacao-relatorios-2026-10-30/api-comparison-original.json).

## Linking two rebuilt source components

The corrected service was also compiled against the **complete source-rebuilt Data.Core module**, rather than the vendor Data.Core DLL. Signatures and another 120 corrected callback trials passed. The service bytes are identical to the vendor-Data.Core-linked build, consistent with the matching required dependency surfaces. See [source-link-manifest.json](../engenharia/recompilacao-relatorios-2026-10-30/source-link-manifest.json).

This is a meaningful compile-time step toward a reconstructed project graph. It is not a full runtime integration test of both components, the rest of the application, MongoDB or WCF.

The separate [Data.Core report](recompilacao-data-core-2026-10-30.md) documents its full module build and stronger original-versus-source-rebuilt runtime comparisons.

## Reproduce

```sh
python engenharia/recompilacao-data-core-2026-10-30/run_rebuild.py /tmp/digisat-monthly
python engenharia/recompilacao-relatorios-2026-10-30/run_rebuild.py /tmp/digisat-monthly
python engenharia/recompilacao-relatorios-2026-10-30/check_source_link.py /tmp/digisat-monthly
```

The runners use already available tools/references/dependencies, with no downloads. They generate original/fixed source, compile, compare signatures, run selected tests and save hashes/logs. The [SDK project](../engenharia/recompilacao-relatorios-2026-10-30/Recovered.FullReportService.csproj) also supports `UseCorrected=true` and `UseRebuiltDataCore=true` for a future SDK-equipped environment. The SDK project itself was supplied for review; execution here used the explicit Roslyn runners.

[build-manifest.json](../engenharia/recompilacao-relatorios-2026-10-30/build-manifest.json) records source/tool/reference hashes and outputs. REA identities are [original-behavior output](../engenharia/recompilacao-relatorios-2026-10-30/rea-identity-original.json), Evidence `ev_80ef657ac00ce2bafeec5322161ff389dfdde436159007dbd6d5c50e3e3bc721`, and [corrected output](../engenharia/recompilacao-relatorios-2026-10-30/rea-identity-fixed.json), Evidence `ev_a00816b689e40830930923113b36ebfb0cfc4bc63753c126fcf10db473c2c659`.

## Remaining work

- Recover the rest of Application.Main and other required first-party modules. This class cannot stand in for that whole assembly.
- Reconstruct project/assembly integration, DI registration and the WCF host's selection of the service implementation. The laboratory component has a different assembly identity.
- Validate broader behavior: all report branches, actual domain materialization, repository filtering, seller/name lookup, date serialization, origin/status special cases, DTOs and report designer.
- Run the Framework-targeted components in a representative Windows/Framework installation with copied data, then compare complete original/corrected reports repeatedly against a serial reference.
- Review the syntax repairs against relevant call/argument flows before asserting parity of untested methods. Public signature equality and successful compilation are necessary, not sufficient.
- Integrate deliberate fixes into a vendor-approved build and homologate it. Data.Core's baseline rebuild intentionally preserves its original defects for parity; its successful build is not a synchronization fix.

The earlier compilation blocker is resolved for the entire selected report-service class. Whole-system source recovery and production acceptance remain separate, explicitly unfinished work.

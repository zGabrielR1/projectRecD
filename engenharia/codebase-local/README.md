# SuiteG6 — local reconstructed codebase

This is a local source workspace recovered from the installed SuiteG6 managed artifacts. It contains editable C#, projects, resource payloads, dependency snapshots, offline build commands and behavioral checks. It is not the vendor's original repository: comments, original names removed by obfuscation, project organization and missing native sources cannot be recovered exactly.

Recovered coverage: **218 projects and 22,126 C# files** (139,265,045 bytes of C#), with **510 managed dependency names** inventoried. Only the two projects in the verified solution have successful source builds recorded.

## Open and navigate

- **[SuiteG6.Local.code-workspace](SuiteG6.Local.code-workspace)** opens the source trees and tools in VS Code.
- **[SuiteG6.Verified.sln](SuiteG6.Verified.sln)** contains the source projects whose offline builds have been verified.
- **[Recovered product solution](recovered/SuiteG6/SuiteG6.Recovered.sln)** contains the broad decompiler-exported product tree. Extraction does not mean all of these projects compile.
- **[PROJECTS.md](PROJECTS.md)** maps every recovered assembly to its project. [workspace-summary.json](workspace-summary.json) gives exact coverage; [artifact-inventory.json](artifact-inventory.json) records input origins, hashes and duplicate variants.

## Edit, build and check

From this directory:

```powershell
.\build.ps1
```

Or from the repository root:

```powershell
python engenharia/codebase-local/tools/build_workspace.py
```

Normal builds compile the local editable sources and preserve your changes. They do not repeat decompilation. Recovery is an explicit separate operation; project READMEs describe it. The build uses the Python, .NET SDK/Roslyn and native .NET Framework already installed on this machine, without NuGet restore or downloads.

| Project | Editable source | State |
| --- | --- | --- |
| [DigisatServer.Infrastructure.Data.Core](projects/DigisatServer.Infrastructure.Data.Core/README.md) | Complete current module; 2,933 recovered lines | Source compiles; 66 original named type surfaces/resources/references compared; 19 selected native behavior checks match the original |
| [Recovered.ReportService](projects/Recovered.ReportService/README.md) | Complete current report-service class plus Parcial helper | Source compiles; 74 declared methods compared; original/rebuilt pure helpers checked |
| [DigisatServer.Domain.Core](projects/DigisatServer.Domain.Core/README.md) | Complete current module extraction | Build attempted and blocked by obfuscator/decompiler constructs; see compiler log and manifest |
| [Broad product tree](PROJECTS.md) | Application, domain, infrastructure, server, synchronizer and UI modules | Decompiled projects for navigation/reconstruction; builds and full behavior not verified |

The report component is a selected class from Application.Main, not a rebuilt entire Application.Main assembly. Other layers remain supplied by explicitly snapshotted original DLLs until their source builds are recovered. Baseline reconstruction preserves observed original defects; a successful diagnostic assertion can mean the defect was reproduced. No experimental fixes are silently included.

The default workspace build links the report component against the sibling source-built Data.Core. Its exact dependency hash is recorded; this is verified compilation linkage, while the selected report checks do not exercise the complete repository stack. The combined build passed 19 Data.Core and 16 original/rebuilt report-helper assertions. Native Framework MSBuild also built `SuiteG6.Verified.sln` successfully.

## Layout

```text
SuiteG6.Verified.sln        verified editable components
recovered/SuiteG6/          broad exported source projects and resources
projects/                  individually recovered, buildable or diagnosed projects
baseline/                  managed dependency snapshots, no configuration files
tools/                     inventory, source recovery, indexing and offline builds
PROJECTS.md                project navigation
*-manifest.json            recovery/build identities and limitations
```

`baseline/`, broad recovered vendor sources, resource/tool binaries and build outputs remain local and are ignored by Git. No installer/release packaging was changed. Only the four explicit code directories were inventoried: Sistema, Servidor, Sincronizador and Sistema/Modulos. Configuration, logs, backups and customer data were not copied.

Six dependency names have distinct installed variants. The inventory records each; the broad snapshot uses the stated origin precedence. This is a deterministic analysis snapshot, not proof that one flattened dependency directory can launch every product process. Project-specific build snapshots are recorded separately.

The first export's Python index step hit Windows' legacy path-length limit after the decompiler had produced the tree. Finalization was resumed using extended paths without regenerating source. Its original decompiler exit code was not retained; the manifest records that as null. Export coverage is verified from the 218 existing projects and the file index, not inferred from an unavailable process exit code.

## What is runnable

The verified build executes selected reconstructed helpers and replication classes with synthetic boundaries. It does not start SistemaG6, ServidorG6 or SincronizadorG6, connect to MongoDB, control services or replace the installation. A complete application launch, all-module build and integrated behavioral equivalence remain open work. Framework compilation uses the installed Framework 4.8 assemblies; it is not a clean Framework 4.6 targeting-pack build.

See the [runtime investigation index](../../docs/runtime-investigation-index-2026-11-11.md) for the original-DLL checks and [local reconstruction report](../../docs/recreated-codebase-2026-11-11.md) for the resulting source workspace.

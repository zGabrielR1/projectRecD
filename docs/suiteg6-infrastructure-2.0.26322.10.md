# SuiteG6 infrastructure recovery for 2.0.26322.10

The Nov 24, 2026 capture from `C:\Digisat\SuiteG6` preserved the older recovery workspace alongside a new snapshot. Across the managed inventory, SHA-256 comparison found 228 changed binaries, 282 unchanged binaries, and one addition, `Modulo.Renave.dll`. All 35 selected infrastructure DLLs changed at the binary level. The new export contains 35 projects and 7,541 C# files.

## Verification

**27 of 35 infrastructure projects compile** against dependencies from the new snapshot. The last pass added ArquivoContabilidade, Boleto, Farmacia, ImpressoraCheque, ImpressoraNaoFiscal, Mvvm, Notificacao, Report, and Tef. Each matched the installed original on assembly identity, dependency references, embedded resources, and the named public and protected type surfaces checked.

- **Data.Core** compiled from the alternate whole-module export. Static comparison found the same 66 original named type surfaces, assembly identity, dependency references, and resource hashes. Roslyn added two compiler-generated attribute types.
- **DFe** matched the original references after the build selected the declared `System.Net.Http` identity, version `4.0.0.0`. The same-name DLL in the installation is version `4.1.1.2`. The build checks the original declaration and the selected framework DLL before using the override.
- **Notificacao** compiled after restoring 38 loads and stores of 19 WPF routed-event backing fields. The original CIL identifies the fields used by the event raisers and the static constructor.
- **Assembly names** now come from the pinned input filename. This preserves the original assembly name even when dnSpy shortens the project path.

Eight C# builds remain blocked: Crosscutting, Concentrador, Criptografia, Ecf, EnvioPafEcf, NetFramework, RegistroSistema, and Data.Main. Each has an editable CIL build described below. Integrated SuiteG6 behavior and hardware tests remain pending.

## Local workflow

Run from `engenharia/codebase-local` on Windows. Choose a fresh snapshot name for a later installation. Omitting `--source-prefix` exports all eligible managed assemblies.

```powershell
python tools/recover_workspace.py --workspace snapshots/installed-20261124 --source-prefix DigisatServer.Infrastructure.
python tools/compare_inventories.py artifact-inventory.json snapshots/installed-20261124/artifact-inventory.json -o snapshots/installed-20261124/inventory-delta.json
python tools/build_recovered_modules.py --workspace snapshots/installed-20261124 --prepare
```

The first build measures the raw export. The 27-project result includes local source repairs and alternate exports. The snapshot retains the recovered sources, compiler logs, per-project repair records, inventory delta, and static compatibility reports. Git tracks the recovery tooling and these findings. The local snapshot and installed binaries stay outside Git.

## Editing and checking Data.Core

In the prepared snapshot, edit `projects-auto/DigisatServer.Infrastructure.Data.Core/source/_alternative/WholeModule.cs`. Rebuild and compare the behavior with the original DLL from the same installation:

```powershell
python tools/build_recovered_modules.py --workspace snapshots/installed-20261124 --only DigisatServer.Infrastructure.Data.Core
python tools/test_snapshot_data_core.py --workspace snapshots/installed-20261124
```

The test checks the build hashes and runs the two existing synthetic harnesses in temporary folders under the Windows CLR. Six sync assertions and thirteen replication assertions matched the original `2.0.26322.10` DLL. The tests substitute in-memory collections, cursors and recipients. Live services, MongoDB, and integrated application startup remain outside their coverage.

## Editable CIL

All eight modules blocked in C# assemble from editable CIL, bringing source build coverage to **35 of 35 infrastructure modules**. Edit their `projects-il/<assembly>/source/` files in the prepared snapshot and rebuild using the exact DLL name:

```powershell
python tools/il_workspace.py --workspace snapshots/installed-20261124 DigisatServer.Infrastructure.Crosscutting.Concentrador.dll
```

All eight matched the original assembly identities, embedded resource hashes by name, and checked named API surfaces. Concentrador's embedded COM event interface retains the original import flag and 21 methods. Concentrador and Data.Main matched their full reference lists. The other six omit a duplicate `mscorlib` entry with version `65535.65535.65535.65535`. The eight CIL outputs have static verification only.

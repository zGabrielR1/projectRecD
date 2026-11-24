# Editable CIL rebuilds

Use this route when obfuscated metadata cannot be represented by the recovered C# export. The disassembler reads the local baseline as metadata and writes textual CIL plus separate resource files; it does not load or execute the target.

For the verified Domain.Core reconstruction, build and compare it with:

```powershell
python engenharia/codebase-local/tools/il_workspace.py DigisatServer.Domain.Core.dll
python engenharia/codebase-local/tools/verify_domain_core.py engenharia/codebase-local/projects-il/DigisatServer.Domain.Core/bin/DigisatServer.Domain.Core.dll
```

The editable assembly source is under `DigisatServer.Domain.Core/source/`. The helper preserves invalid obfuscator token bytes, exception-region offsets, generic variance constraints, and embedded resource payloads. The verification record compares public type surfaces, resource bytes, and 13 selected synthetic behavior checks. ILAsm omits one unused duplicate `mscorlib` reference; this difference is recorded in the build manifest. This rebuild does not establish full application equivalence.

`projects-auto/compilation-census.json` records the separate C# export builds. `build_recovered_modules.py` uses the CIL-built Domain.Core for `SistemaG6.exe` when that verified local output exists.

# Current Domain.Core — recovered source, build blocked

This project contains the complete current installed `DigisatServer.Domain.Core.dll` decompiled into editable C#. Its SHA-256 is `fcd29ec09a65eaa8397ccc6b63f6630c61b6fb99293c4a863162d7382928d9ff`; `recovery-manifest.json` preserves MVID, tooling and dependency identity. Original resources and a baseline DLL remain local. No vendor code was executed.

Unlike Data.Core, this assembly is obfuscated. The whole-module decompilation contains illegal generated identifiers, explicit `.ctor` pseudocalls and invalid `ref` expressions. A real offline compilation was attempted; `logs/compile.log` records its syntax errors. `build-manifest.json` records **source-recovered-build-blocked**. No licensing, initialization or obfuscation behavior was removed to force a successful compile.

`source/Domain.Core.recovered.cs` is currently an exact copy of the raw reconstruction: no repairs have been made. It is not the original development source and cannot yet be represented as a working replacement DLL.

Rebuild an edited source without overwriting it:

```powershell
python engenharia/codebase-local/tools/build_domain_core.py --compile-only
```

The command currently fails with the recorded compile blockers. `--recover` is only for an absent source tree and refuses to overwrite existing editable sources. The csproj opens the source and delegates Build to the offline script. It should remain outside the verified-build solution until the obfuscated IL is reconstructed and independently verified.

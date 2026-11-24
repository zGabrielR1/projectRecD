"""Read-only extraction of the installed report service; never executes SuiteG6."""
import hashlib
import json
import subprocess
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
OUT = ROOT / 'engenharia' / 'rea-2026-11-11'
ARTIFACT = Path('C:/Digisat/SuiteG6/Sistema/DigisatServer.Application.Main.dll')
DECOMPILER = Path('D:/Downloads/stuff/winUtils/dnSpy-net-win64/dnSpy.Console.exe')
TYPE = 'DigisatServer.Application.Main.AuxiliaresModulo.Services.MovimentacaoSaidaRelatorioAppService'


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    destination = OUT / 'MovimentacaoSaidaRelatorioAppService.cs'
    if destination.exists():
        raise SystemExit('Destination exists; preserve the previous extraction.')
    before = sha(ARTIFACT)
    result = subprocess.run([str(DECOMPILER), '--no-color', '--no-resources',
                             '--no-baml', '-t', TYPE, str(ARTIFACT)],
                            capture_output=True, timeout=180)
    if result.returncode or b'class ' not in result.stdout:
        raise SystemExit('Decompiler failed; no output preserved.')
    if before != sha(ARTIFACT):
        raise SystemExit('Artifact changed during inspection.')
    destination.write_bytes(result.stdout)
    manifest = {'artifact': str(ARTIFACT), 'artifact_sha256': before,
                'type': TYPE, 'source': destination.name,
                'source_sha256': sha(destination),
                'decompiler': str(DECOMPILER), 'decompiler_sha256': sha(DECOMPILER),
                'target_executed': False,
                'limitations': ['C# is decompiler reconstruction, not original source.',
                                'Installed file does not identify a running or remote server.']}
    (OUT / 'source-manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    print(json.dumps(manifest, indent=2))


if __name__ == '__main__':
    main()

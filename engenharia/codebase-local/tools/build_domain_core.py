"""Recover/build current Domain.Core offline; normal builds preserve source edits."""
from pathlib import Path
import argparse
import hashlib
import json
import os
import shutil
import subprocess

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / 'projects/DigisatServer.Domain.Core'
REPO = ROOT.parents[1]
ORIGIN = Path('C:/Digisat/SuiteG6/Sistema')
NAME = 'DigisatServer.Domain.Core.dll'
EXPECTED = 'fcd29ec09a65eaa8397ccc6b63f6630c61b6fb99293c4a863162d7382928d9ff'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
VENDOR = ['DigisatServer.Infrastructure.Crosscutting.dll', 'MongoDB.Bson.dll']
SYSTEM = ['mscorlib.dll', 'System.dll', 'System.Core.dll', 'System.ComponentModel.DataAnnotations.dll',
          'System.Runtime.Serialization.dll', 'System.Xml.dll']


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def run(args, log):
    env = os.environ.copy()
    env['DOTNET_ROLL_FORWARD'] = 'Major'
    result = subprocess.run([str(x) for x in args], cwd=PROJECT, env=env, capture_output=True, text=True, timeout=90)
    (PROJECT / 'logs' / log).write_text(result.stdout + result.stderr, encoding='utf-8')
    if result.returncode:
        raise RuntimeError('Command failed: see ' + str(PROJECT / 'logs' / log))
    return result.stdout


def metadata(path, label, extract=False):
    args = [DOTNET, PROJECT / 'tooling/ModuleProof.dll', path]
    if extract:
        args += ['resources', PROJECT / 'resources']
    data = json.loads(run(args, label + '-metadata.log'))
    (PROJECT / (label + '-metadata.json')).write_text(json.dumps(data, indent=2), encoding='utf-8')
    return data


def recover():
    if (PROJECT / 'source/Domain.Core.recovered.cs').exists():
        raise RuntimeError('Editable source exists; refusing to overwrite')
    if sha(ORIGIN / NAME) != EXPECTED:
        raise RuntimeError('Unreviewed target identity')
    tooling = REPO / 'engenharia/recompilacao-data-core-2026-10-30/build'
    for name in ['WholeModuleDecompiler.dll', 'WholeModuleDecompiler.runtimeconfig.json',
                 'ModuleProof.dll', 'ModuleProof.runtimeconfig.json', 'ICSharpCode.Decompiler.dll', 'Mono.Cecil.dll']:
        shutil.copyfile(tooling / name, PROJECT / 'tooling' / name)
    shutil.copyfile(ORIGIN / NAME, PROJECT / 'baseline' / NAME)
    references = []
    for name in VENDOR:
        shutil.copyfile(ORIGIN / name, PROJECT / 'references' / name)
        references.append({'name': name, 'sha256': sha(PROJECT / 'references' / name)})
    original = metadata(PROJECT / 'baseline' / NAME, 'original', True)
    raw = PROJECT / 'source/Domain.Core.raw.cs'
    run([DOTNET, PROJECT / 'tooling/WholeModuleDecompiler.dll', PROJECT / 'baseline' / NAME, raw], 'decompile.log')
    shutil.copyfile(raw, PROJECT / 'source/Domain.Core.recovered.cs')
    (PROJECT / 'recovery-manifest.json').write_text(json.dumps({
        'original_path': str(ORIGIN / NAME), 'original_sha256': EXPECTED, 'original_mvid': original['mvid'],
        'raw_source_sha256': sha(raw), 'initial_recovered_source_sha256': sha(raw),
        'source_lines': len(raw.read_text(encoding='utf-8-sig').splitlines()), 'repairs': [],
        'references': references, 'decompiler_sha256': sha(PROJECT / 'tooling/ICSharpCode.Decompiler.dll'),
        'target_executed': False}, indent=2), encoding='utf-8')
    if sha(ORIGIN / NAME) != EXPECTED:
        raise RuntimeError('Installed target changed')


def build():
    recovery = json.loads((PROJECT / 'recovery-manifest.json').read_text())
    if any(sha(PROJECT / 'references' / x['name']) != x['sha256'] for x in recovery['references']):
        raise RuntimeError('Project-local dependency identity changed')
    output = PROJECT / 'bin' / NAME
    refs = [FRAMEWORK / x for x in SYSTEM] + [PROJECT / 'references' / x for x in VENDOR]
    command = [DOTNET, CSC, '-nologo', '-nostdlib+', '-target:library', '-deterministic+', '-out:' + str(output)]
    command += ['-r:' + str(x) for x in refs]
    command += ['-resource:' + str(x) + ',' + x.name for x in (PROJECT / 'resources').iterdir()]
    command += [PROJECT / 'source/Domain.Core.recovered.cs']
    try:
        run(command, 'compile.log')
    except RuntimeError:
        (PROJECT / 'build-manifest.json').write_text(json.dumps({
            'status': 'source-recovered-build-blocked', 'source_sha256': sha(PROJECT / 'source/Domain.Core.recovered.cs'),
            'original_sha256': recovery['original_sha256'], 'compiler_sha256': sha(CSC),
            'repairs': [], 'compiled': False,
            'blocker': 'Obfuscated IL decompiles to illegal compiler-generated identifiers, explicit .ctor pseudo-calls and invalid ref expressions. No semantic rewrite performed.',
            'compile_log': 'logs/compile.log', 'production_replacement': False,
            'original_target_executed': False}, indent=2), encoding='utf-8')
        raise
    original = json.loads((PROJECT / 'original-metadata.json').read_text())
    rebuilt = metadata(output, 'rebuilt')
    before = {x['name']: x for x in original['api']}
    after = {x['name']: x for x in rebuilt['api']}
    manifest = {'source_sha256': sha(PROJECT / 'source/Domain.Core.recovered.cs'),
        'output_sha256': sha(output), 'output_mvid': rebuilt['mvid'], 'compiler_sha256': sha(CSC),
        'source_modified_since_recovery': sha(PROJECT / 'source/Domain.Core.recovered.cs') != recovery['initial_recovered_source_sha256'],
        'original_named_type_surfaces': len(before),
        'all_original_named_type_surfaces_equal': all(after.get(k) == v for k, v in before.items()),
        'extra_compiler_types': sorted(set(after) - set(before)),
        'embedded_resources_equal': original['resources'] == rebuilt['resources'],
        'assembly_references_equal': original['references'] == rebuilt['references'],
        'compile_reference_scope': 'Installed Windows Framework4.8 assemblies; not clean Framework4.6 reference pack',
        'original_target_executed': False, 'behavioral_equivalence': 'not established', 'production_replacement': False}
    (PROJECT / 'build-manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    if not manifest['all_original_named_type_surfaces_equal'] or not manifest['embedded_resources_equal']:
        raise RuntimeError('API/resource difference; see build-manifest.json')
    print('Current Domain.Core compiled from C#; original API surfaces and resources match.')


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--recover', action='store_true')
    parser.add_argument('--compile-only', action='store_true')
    args = parser.parse_args()
    for folder in ['source', 'resources', 'references', 'tooling', 'baseline', 'bin', 'logs']:
        (PROJECT / folder).mkdir(parents=True, exist_ok=True)
    if args.recover:
        recover()
    build()


if __name__ == '__main__':
    main()

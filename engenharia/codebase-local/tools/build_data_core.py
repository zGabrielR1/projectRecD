"""Recover/build/test the exact current Data.Core as an editable local project.

No downloads, installation, database or application startup. --recover is explicit;
normal builds preserve edited source. Uses existing local Roslyn/native CLR.
"""
from pathlib import Path
import argparse
import difflib
import json
import os
import shutil
import subprocess
import hashlib

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / 'projects/DigisatServer.Infrastructure.Data.Core'
REPO = ROOT.parents[1]
ORIGIN = Path('C:/Digisat/SuiteG6/Sistema')
NAME = 'DigisatServer.Infrastructure.Data.Core.dll'
EXPECTED = '51fb587ae4a006c95cf4bd7b36ba9fb65951f6893fbdadc76fdcb04f849c983b'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
VENDOR_REFS = ['Akka.dll', 'DigisatServer.Infrastructure.Crosscutting.dll',
    'DigisatServer.Infrastructure.Crosscutting.NetFramework.dll', 'MongoDB.Bson.dll',
    'MongoDB.Driver.dll', 'MongoDB.Driver.Core.dll', 'System.Reactive.Core.dll',
    'System.Reactive.Interfaces.dll', 'System.Reactive.Linq.dll', 'System.Reactive.PlatformServices.dll']
TEST_REFS = ['System.ValueTuple.dll']
SYSTEM_REFS = ['mscorlib.dll', 'System.dll', 'System.Core.dll', 'System.Configuration.dll',
    'System.ServiceProcess.dll', 'System.Runtime.Serialization.dll', 'System.Xml.dll', 'System.Data.dll']


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def execute(command, log, timeout=60):
    env = os.environ.copy()
    env['DOTNET_ROLL_FORWARD'] = 'Major'
    result = subprocess.run([str(p) for p in command], cwd=PROJECT, env=env,
        capture_output=True, text=True, timeout=timeout)
    (PROJECT / log).write_text(result.stdout + result.stderr, encoding='utf-8')
    if result.returncode:
        raise RuntimeError('Command failed; see ' + str(PROJECT / log))
    return result.stdout


def metadata(artifact, label, resources=False):
    command = [DOTNET, PROJECT / 'tooling/ModuleProof.dll', artifact]
    if resources:
        command += ['resources', PROJECT / 'resources']
    result = json.loads(execute(command, 'logs/' + label + '-metadata.log'))
    (PROJECT / (label + '-metadata.json')).write_text(json.dumps(result, indent=2), encoding='utf-8')
    return result


def recover(force):
    if sha(ORIGIN / NAME) != EXPECTED:
        raise RuntimeError('Unreviewed original Data.Core')
    recovered = PROJECT / 'source/Data.Core.recovered.cs'
    if recovered.exists() and not force:
        raise RuntimeError('Editable source exists; use --force-recover only to discard source edits')
    old = REPO / 'engenharia/recompilacao-data-core-2026-10-30'
    for name in ['WholeModuleDecompiler.dll', 'WholeModuleDecompiler.runtimeconfig.json',
                 'ModuleProof.dll', 'ModuleProof.runtimeconfig.json', 'ICSharpCode.Decompiler.dll', 'Mono.Cecil.dll']:
        if not (PROJECT / 'tooling' / name).exists():
            shutil.copyfile(old / 'build' / name, PROJECT / 'tooling' / name)
    for name in ['WholeModuleDecompiler.cs', 'ModuleProof.cs']:
        if not (PROJECT / 'tooling' / name).exists():
            shutil.copyfile(old / name, PROJECT / 'tooling' / name)
    references = []
    for name in VENDOR_REFS + TEST_REFS:
        shutil.copyfile(ORIGIN / name, PROJECT / 'references' / name)
        references.append({'name': name, 'sha256': sha(PROJECT / 'references' / name)})
    original_copy = PROJECT / 'baseline' / NAME
    shutil.copyfile(ORIGIN / NAME, original_copy)
    original = metadata(original_copy, 'original', True)
    execute([DOTNET, PROJECT / 'tooling/WholeModuleDecompiler.dll', ORIGIN / NAME,
        PROJECT / 'source/Data.Core.raw.cs'], 'logs/decompile.log')
    raw = (PROJECT / 'source/Data.Core.raw.cs').read_text(encoding='utf-8-sig')
    ambiguity_count = raw.count('catch (TimeoutException)')
    if ambiguity_count not in (0, 1):
        raise RuntimeError('Review changed decompiler ambiguity')
    matches = [e for e in original['exceptions'] if e['catchType'] == 'System.TimeoutException']
    if not matches:
        raise RuntimeError('Original catch type cannot justify qualification repair')
    fixed = raw.replace('catch (TimeoutException)', 'catch (System.TimeoutException)')
    recovered.write_text(fixed, encoding='utf-8')
    (PROJECT / 'source-repair.patch').write_text(''.join(difflib.unified_diff(raw.splitlines(True),
        fixed.splitlines(True), fromfile='Data.Core.raw.cs', tofile='Data.Core.recovered.cs')), encoding='utf-8')
    manifest = {'original_path': str(ORIGIN / NAME), 'original_sha256': EXPECTED,
        'original_mvid': original['mvid'], 'raw_source_sha256': sha(PROJECT / 'source/Data.Core.raw.cs'),
        'initial_recovered_source_sha256': sha(recovered), 'source_lines': len(fixed.splitlines()),
        'repairs': ['Qualify System.TimeoutException from original exception-handler metadata'] if ambiguity_count else [],
        'exception_metadata_support': matches, 'references': references,
        'decompiler_sha256': sha(PROJECT / 'tooling/ICSharpCode.Decompiler.dll'),
        'decompiler_wrapper_sha256': sha(PROJECT / 'tooling/WholeModuleDecompiler.dll'),
        'metadata_helper_sha256': sha(PROJECT / 'tooling/ModuleProof.dll'),
        'metadata_library_sha256': sha(PROJECT / 'tooling/Mono.Cecil.dll'),
        'resource_payloads': original['resources'], 'target_execution_during_recovery': False}
    (PROJECT / 'recovery-manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    if sha(ORIGIN / NAME) != EXPECTED:
        raise RuntimeError('Original changed during recovery')
    if any(sha(ORIGIN / r['name']) != r['sha256'] for r in references):
        raise RuntimeError('An installed dependency changed during recovery')


def build():
    recovery = json.loads((PROJECT / 'recovery-manifest.json').read_text(encoding='utf-8'))
    if any(sha(PROJECT / 'references' / r['name']) != r['sha256'] for r in recovery['references']):
        raise RuntimeError('Project-local reference identity changed')
    refs = [FRAMEWORK / n for n in SYSTEM_REFS] + [PROJECT / 'references' / n for n in VENDOR_REFS]
    output = PROJECT / 'bin' / NAME
    command = [DOTNET, CSC, '-nologo', '-target:library', '-nostdlib+', '-deterministic+',
        '-out:' + str(output)] + ['-r:' + str(p) for p in refs]
    command += ['-resource:' + str(p) + ',' + p.name for p in (PROJECT / 'resources').iterdir()]
    command.append(PROJECT / 'source/Data.Core.recovered.cs')
    execute(command, 'logs/compile.log')
    original = json.loads((PROJECT / 'original-metadata.json').read_text(encoding='utf-8'))
    rebuilt = metadata(output, 'rebuilt')
    before = {t['name']: t for t in original['api']}
    after = {t['name']: t for t in rebuilt['api']}
    surface_equal = all(after.get(k) == v for k, v in before.items())
    manifest = {'output_sha256': sha(output), 'output_mvid': rebuilt['mvid'],
        'source_sha256': sha(PROJECT / 'source/Data.Core.recovered.cs'),
        'source_modified_since_recovery': sha(PROJECT / 'source/Data.Core.recovered.cs') != recovery['initial_recovered_source_sha256'],
        'compiler_sha256': sha(CSC), 'framework_references': [{'path': str(p), 'sha256': sha(p)} for p in refs[:len(SYSTEM_REFS)]],
        'build_runner_sha256': sha(Path(__file__)),
        'metadata_helper_sha256': sha(PROJECT / 'tooling/ModuleProof.dll'),
        'metadata_library_sha256': sha(PROJECT / 'tooling/Mono.Cecil.dll'),
        'original_named_type_surfaces': len(before), 'all_original_named_type_surfaces_equal': surface_equal,
        'extra_compiler_types': sorted(set(after) - set(before)),
        'embedded_resources_equal': original['resources'] == rebuilt['resources'],
        'assembly_references_equal': original['references'] == rebuilt['references'],
        'target_attribute': '.NETFramework,Version=v4.6',
        'compile_reference_scope': 'Installed native Framework 4.8 runtime assemblies, not net46 reference pack',
        'production_replacement': False}
    (PROJECT / 'build-manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    if not manifest['source_modified_since_recovery'] and (not surface_equal or not manifest['embedded_resources_equal']):
        raise RuntimeError('Rebuild API/resource parity changed; review build-manifest.json')
    print('Current Data.Core rebuilt; API/resource comparison recorded in build-manifest.json.', flush=True)


def test():
    refs = [FRAMEWORK / n for n in SYSTEM_REFS + ['System.Web.Extensions.dll', 'System.Runtime.Remoting.dll']]
    refs += [PROJECT / 'references' / n for n in VENDOR_REFS]
    refs.append(PROJECT / 'bin' / NAME)
    checks = []
    for name, source, baseline in [
        ('SyncRuntimeProof', REPO / 'engenharia/runtime-sync-2026-11-11/SyncRuntimeProof.Framework.cs',
            REPO / 'engenharia/runtime-sync-2026-11-11/results-framework-current.json'),
        ('ReplicationRuntimeProof', REPO / 'engenharia/runtime-replication-2026-11-11/ReplicationRuntimeProof.cs',
            REPO / 'engenharia/runtime-replication-2026-11-11/results-current.json')]:
        copied = PROJECT / 'tests' / (name + '.cs')
        if not copied.exists():
            shutil.copyfile(source, copied)
        folder = PROJECT / 'tests/bin' / name
        folder.mkdir(parents=True, exist_ok=True)
        for dependency in (PROJECT / 'references').glob('*.dll'):
            shutil.copyfile(dependency, folder / dependency.name)
        shutil.copyfile(PROJECT / 'bin' / NAME, folder / NAME)
        tuple_lib = PROJECT / 'references/System.ValueTuple.dll'
        shutil.copyfile(tuple_lib, folder / tuple_lib.name)
        executable = folder / (name + '.exe')
        execute([DOTNET, CSC, '-nologo', '-nostdlib+', '-target:exe', '-out:' + str(executable)] +
            ['-r:' + str(p) for p in refs + [folder / tuple_lib.name]] + [copied], 'logs/compile-test-' + name + '.log')
        result = json.loads(execute([executable], 'logs/run-test-' + name + '.log', 15))
        local_baseline = PROJECT / 'tests' / (name + '.original-baseline.json')
        if not local_baseline.exists():
            shutil.copyfile(baseline, local_baseline)
        expected = json.loads(local_baseline.read_text(encoding='utf-8'))
        if result['checks'] != expected['checks']:
            raise RuntimeError('Selected behavior differs from actual current DLL baseline: ' + name)
        output = PROJECT / 'tests' / (name + '.results.json')
        output.write_text(json.dumps(result, indent=2), encoding='utf-8')
        checks.append({'name': name, 'assertions': result['assertions'],
            'harness_sha256': sha(copied), 'results_sha256': sha(output),
            'original_baseline_sha256': sha(local_baseline), 'checks_equal_to_original': True})
    (PROJECT / 'tests/manifest.json').write_text(json.dumps({'runs': checks,
        'rebuilt_target_sha256': sha(PROJECT / 'bin' / NAME),
        'scope': 'Native CLR, synthetic collections/cursors/recipients/logger; no services/database/network'}, indent=2), encoding='utf-8')
    print('Selected native CLR behavior assertions:', sum(r['assertions'] for r in checks), 'equal original current DLL.', flush=True)


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--recover', action='store_true')
    parser.add_argument('--force-recover', action='store_true')
    parser.add_argument('--compile-only', action='store_true')
    parser.add_argument('--test', action='store_true')
    args = parser.parse_args()
    for name in ['source', 'resources', 'references', 'tooling', 'baseline', 'bin', 'logs', 'tests']:
        (PROJECT / name).mkdir(parents=True, exist_ok=True)
    if args.recover or args.force_recover:
        recover(args.force_recover)
    build()
    if args.test:
        test()


if __name__ == '__main__':
    main()

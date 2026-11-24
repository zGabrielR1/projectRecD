"""Offline source rebuilds of current synchronizer/server actor modules.

Never launches the synchronizer entrypoint, services or ActorSystem. Runtime
checks invoke only constructor-bypassed receiver methods with synthetic I/O.
"""
from pathlib import Path
import argparse
import difflib
import hashlib
import json
import os
import shutil
import subprocess

ROOT = Path(__file__).resolve().parents[1]
REPO = ROOT.parents[1]
PROJECT = ROOT / 'projects/Rebuilt.Sincronizador'
BASELINE = ROOT / 'baseline'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
TARGETS = {
    'SincronizadorG6.exe': '26ffed48484bdd9e9210034d2076c07f713fed969c9dd877039d28133c7e5386',
    'DigisatServer.Distributed.Akka.Main.dll': 'f0cae4e37b786355493dd03d971e8747e4288887fcd08f56f2fc1ac43ab56f6f',
}
ORIGINS = {
    'SincronizadorG6.exe': Path('C:/Digisat/SuiteG6/Sincronizador'),
    'DigisatServer.Distributed.Akka.Main.dll': Path('C:/Digisat/SuiteG6/Servidor'),
}


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def run(args, log, timeout=90):
    env = os.environ.copy()
    env['DOTNET_ROLL_FORWARD'] = 'Major'
    process = subprocess.run([str(p) for p in args], cwd=PROJECT, env=env,
        capture_output=True, text=True, timeout=timeout)
    (PROJECT / 'logs' / log).write_text(process.stdout + process.stderr, encoding='utf-8')
    if process.returncode:
        raise RuntimeError('Command failed; see ' + str(PROJECT / 'logs' / log))
    return process.stdout


def metadata(artifact, name, resources=False):
    args = [DOTNET, PROJECT / 'tooling/ModuleProof.dll', artifact]
    if resources:
        args += ['resources', PROJECT / 'resources' / name]
    data = json.loads(run(args, name + '-metadata.log'))
    (PROJECT / 'metadata' / (name + '.json')).write_text(json.dumps(data, indent=2), encoding='utf-8')
    return data


def recover(force=False):
    helper = ROOT / 'projects/DigisatServer.Infrastructure.Data.Core/tooling'
    for name in ['WholeModuleDecompiler.dll', 'WholeModuleDecompiler.runtimeconfig.json',
                 'ModuleProof.dll', 'ModuleProof.runtimeconfig.json', 'ICSharpCode.Decompiler.dll', 'Mono.Cecil.dll']:
        if not (PROJECT / 'tooling' / name).exists():
            shutil.copyfile(helper / name, PROJECT / 'tooling' / name)
    records = []
    for name, expected in TARGETS.items():
        origin = ORIGINS[name]
        if sha(origin / name) != expected:
            raise RuntimeError('Unreviewed baseline artifact: ' + name)
        recovered = PROJECT / 'source' / (name + '.recovered.cs')
        if recovered.exists() and not force:
            raise RuntimeError('Refusing to overwrite source edits: ' + str(recovered))
        original = metadata(origin / name, name + '.original', True)
        raw = PROJECT / 'source' / (name + '.raw.cs')
        run([DOTNET, PROJECT / 'tooling/WholeModuleDecompiler.dll', origin / name, raw], name + '-decompile.log')
        shutil.copyfile(raw, recovered)
        (PROJECT / (name + '.source-repair.patch')).write_text('', encoding='utf-8')
        references = []
        for reference in original['references']:
            refname = reference.split(',')[0] + '.dll'
            if (FRAMEWORK / refname).exists():
                path = FRAMEWORK / refname
                references.append({'name': refname, 'framework': True, 'path': str(path), 'sha256': sha(path)})
            elif (origin / refname).exists() or (BASELINE / refname).exists():
                ref_origin = origin / refname if (origin / refname).exists() else BASELINE / refname
                path = PROJECT / 'references' / name / refname
                path.parent.mkdir(parents=True, exist_ok=True)
                shutil.copyfile(ref_origin, path)
                references.append({'name': refname, 'framework': False, 'path': str(path),
                    'original_path': str(ref_origin), 'sha256': sha(path)})
            else:
                raise RuntimeError('Missing offline reference ' + reference)
        records.append({'name': name, 'original_path': str(origin / name), 'original_sha256': expected, 'original_mvid': original['mvid'],
            'raw_sha256': sha(raw), 'initial_recovered_sha256': sha(recovered),
            'source_lines': len(raw.read_text(encoding='utf-8-sig').splitlines()),
            'references': references, 'repairs': [], 'target_execution': False})
    win32 = ROOT / 'recovered/SuiteG6/SincronizadorG6'
    for name in ['app.manifest', 'SincronizadorG6.ico']:
        shutil.copyfile(win32 / name, PROJECT / 'resources' / name)
    (PROJECT / 'recovery-manifest.json').write_text(json.dumps({'modules': records,
        'decompiler_sha256': sha(PROJECT / 'tooling/ICSharpCode.Decompiler.dll')}, indent=2), encoding='utf-8')


def build():
    recovery = json.loads((PROJECT / 'recovery-manifest.json').read_text(encoding='utf-8'))
    reports = []
    for module in recovery['modules']:
        name = module['name']
        source = PROJECT / 'source' / (name + '.recovered.cs')
        output = PROJECT / 'bin' / name
        references = [Path(r['path']) for r in module['references']]
        if any(sha(Path(r['path'])) != r['sha256'] for r in module['references']):
            raise RuntimeError('Reference changed for ' + name)
        extra_refs = [FRAMEWORK / 'System.dll']
        if name == 'DigisatServer.Distributed.Akka.Main.dll':
            for refname in ['System.Reactive.Interfaces.dll', 'System.Reactive.Core.dll']:
                local = PROJECT / 'references' / name / refname
                if not local.exists():
                    origin = ORIGINS[name] / refname
                    shutil.copyfile(origin if origin.exists() else BASELINE / refname, local)
                extra_refs.append(local)
        references += [p for p in extra_refs if p not in references]
        command = [DOTNET, CSC, '-nologo', '-nostdlib+', '-deterministic+',
            '-target:' + ('exe' if name.endswith('.exe') else 'library'), '-out:' + str(output)]
        command += ['-r:' + str(p) for p in references]
        command += ['-resource:' + str(p) + ',' + p.name for p in (PROJECT / 'resources' / (name + '.original')).glob('*')]
        if name.endswith('.exe'):
            command += ['-main:Sincronizador.Program', '-win32manifest:' + str(PROJECT / 'resources/app.manifest'),
                        '-win32icon:' + str(PROJECT / 'resources/SincronizadorG6.ico')]
        command.append(source)
        try:
            run(command, name + '-compile.log')
        except RuntimeError:
            reports.append({'name': name, 'compiled': False, 'source_sha256': sha(source),
                'compile_log': 'logs/' + name + '-compile.log'})
            continue
        original = json.loads((PROJECT / 'metadata' / (name + '.original.json')).read_text(encoding='utf-8'))
        rebuilt = metadata(output, name + '.rebuilt')
        before = {t['name']: t for t in original['api']}
        after = {t['name']: t for t in rebuilt['api']}
        report = {'name': name, 'compiled': True, 'source_sha256': sha(source),
            'source_modified': sha(source) != module['initial_recovered_sha256'],
            'output_sha256': sha(output), 'output_mvid': rebuilt['mvid'],
            'original_named_type_surfaces': len(before),
            'original_api_surfaces_equal': all(after.get(k) == v for k, v in before.items()),
            'embedded_resources_equal': original['resources'] == rebuilt['resources'],
            'assembly_references_equal': original['references'] == rebuilt['references'],
            'references_only_original': sorted(set(original['references']) - set(rebuilt['references'])),
            'references_only_rebuilt': sorted(set(rebuilt['references']) - set(original['references'])),
            'compiler_references': [{'path': str(p), 'sha256': sha(p)} for p in references],
            'entrypoint_never_executed': True, 'production_replacement': False}
        reports.append(report)
        print(name + ': compiled from current C#', flush=True)
    (PROJECT / 'build-manifest.json').write_text(json.dumps({'compiler_sha256': sha(CSC),
        'modules': reports}, indent=2), encoding='utf-8')
    if not all(r['compiled'] for r in reports):
        raise RuntimeError('Some source modules remain blocked; inspect module compile logs')


def test():
    folder = PROJECT / 'tests/bin'
    folder.mkdir(parents=True, exist_ok=True)
    # Original receiver fixture's dependencies, excluding the target being rebuilt.
    original_manifest = PROJECT / 'tests/original-fixture-inputs.json'
    if not original_manifest.exists():
        shutil.copyfile(REPO / 'engenharia/runtime-receiver-2026-11-11/manifest.json', original_manifest)
    manifest = json.loads(original_manifest.read_text(encoding='utf-8'))
    refs = []
    for dependency in manifest['inputs']:
        if dependency['name'] == 'SincronizadorG6.exe':
            continue
        local = PROJECT / 'references' / dependency['name']
        if not local.exists():
            shutil.copyfile(Path(dependency['source']), local)
        if sha(local) != dependency['sha256']:
            raise RuntimeError('Test dependency identity changed')
        shutil.copyfile(local, folder / local.name)
        refs.append(local)
    target = PROJECT / 'bin/SincronizadorG6.exe'
    shutil.copyfile(target, folder / target.name)
    source = PROJECT / 'tests/ReceiverRuntimeProof.cs'
    if not source.exists():
        shutil.copyfile(REPO / 'engenharia/runtime-receiver-2026-11-11/ReceiverRuntimeProof.cs', source)
    executable = folder / 'ReceiverRuntimeProof.exe'
    system = [FRAMEWORK / n for n in ['mscorlib.dll', 'System.dll', 'System.Core.dll',
        'System.Web.Extensions.dll', 'System.Runtime.Remoting.dll', 'System.Runtime.Serialization.dll']]
    run([DOTNET, CSC, '-nologo', '-nostdlib+', '-target:exe', '-out:' + str(executable)] +
        ['-r:' + str(p) for p in system + refs] + [source], 'test-compile.log')
    actual = json.loads(run([executable], 'test-run.log', 15))
    baseline = PROJECT / 'tests/original-receiver-baseline.json'
    if not baseline.exists():
        shutil.copyfile(REPO / 'engenharia/runtime-receiver-2026-11-11/results.json', baseline)
    expected = json.loads(baseline.read_text(encoding='utf-8'))
    if actual['checks'] != expected['checks']:
        raise RuntimeError('Rebuilt handler observations differ from original baseline')
    output = PROJECT / 'tests/receiver-results.json'
    output.write_text(json.dumps(actual, indent=2), encoding='utf-8')
    (PROJECT / 'tests/manifest.json').write_text(json.dumps({'rebuilt_actor_sha256': sha(target),
        'rebuilt_actor_mvid': actual['actorMvid'], 'assertions': actual['assertions'],
        'harness_sha256': sha(source), 'results_sha256': sha(output),
        'checks_equal_original_baseline': True,
        'scope': 'Rebuilt handler; actual original repository; uninitialized instances/synthetic driver; no actor startup'}, indent=2), encoding='utf-8')
    print(str(actual['assertions']) + ' rebuilt receiver assertions equal original.', flush=True)


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--recover', action='store_true')
    parser.add_argument('--force-recover', action='store_true')
    parser.add_argument('--compile-only', action='store_true')
    parser.add_argument('--test', action='store_true')
    args = parser.parse_args()
    for folder in ['source', 'tooling', 'resources', 'references', 'bin', 'logs', 'metadata', 'tests']:
        (PROJECT / folder).mkdir(parents=True, exist_ok=True)
    if args.recover or args.force_recover:
        recover(args.force_recover)
    build()
    if args.test:
        test()


if __name__ == '__main__':
    main()

"""Compile exported modules from C# without restore, stubs or original-binary outputs.

Default builds existing editable projects-auto source copies; --prepare creates
them from the untouched recovered export. Each module retains its compiler log.
Opaque resource bytes are extracted from exact original assemblies, not executed.
"""
from pathlib import Path
from concurrent.futures import ThreadPoolExecutor, as_completed
import argparse
import hashlib
import json
import os
import re
import shutil
import struct
import subprocess
import threading
import time
import xml.etree.ElementTree as ET

ROOT = Path(__file__).resolve().parents[1]
AUTO = ROOT / 'projects-auto'
BASELINE = ROOT / 'baseline'
EXPORT = ROOT / 'recovered' / 'SuiteG6'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
RUNTIME = Path('C:/Program Files/dotnet/shared/Microsoft.NETCore.App/10.0.12')
CECIL = ROOT.parents[1] / 'engenharia/recompilacao-data-core-2026-10-30/build/Mono.Cecil.dll'
METADATA_CACHE = AUTO / 'metadata-cache'
METADATA_LOCK = threading.Lock()
VERIFIED_LOCAL_DEPENDENCIES = {
    'DigisatServer.Domain.Core.dll': ROOT / 'projects-il/DigisatServer.Domain.Core/bin/DigisatServer.Domain.Core.dll'
}
ENV = dict(os.environ, DOTNET_ROLL_FORWARD='Major')


def longpath(path):
    value = str(path.resolve())
    return '\\\\?\\' + value if os.name == 'nt' and not value.startswith('\\\\?\\') else value


def sha(path):
    with open(longpath(path), 'rb') as stream:
        return hashlib.sha256(stream.read()).hexdigest()


def lower_ilspy_methodof(source):
    """Lower ILSpy's non-C# methodof placeholders to reflection metadata."""
    pattern = re.compile(r'methodof\(([^()\n]+)\.get_(\w+)\(\)\)')
    source, properties = pattern.subn(lambda m: 'typeof(' + m.group(1) + ').GetProperty("' + m.group(2) + '")', source)
    methods = re.compile(r'methodof\((string|object|char)\.(ToLower|ToUpper|GetType|ToString|Trim)\(\)\)')
    source, method_count = methods.subn(lambda m: 'typeof(' + m.group(1) + ').GetMethod("' + m.group(2) + '", Type.EmptyTypes)', source)
    return source, properties + method_count


def qualify_ambiguous_expression(folder):
    """Apply Roslyn's unambiguous LINQ expression qualification at each diagnostic."""
    log = folder / 'build' / 'compile.log'
    if not log.exists():
        return 0
    pattern = re.compile(r"^(.+?\.cs)\((\d+),(\d+)\): error CS0104: 'Expression' is an ambiguous reference between 'System\.Linq\.Expressions\.Expression' and 'System\.Windows\.Expression'", re.M)
    positions = {}
    for match in pattern.finditer(log.read_text(encoding='utf-8', errors='replace')):
        raw = match.group(1).replace('\\', '/')
        marker = '/source/'
        if marker not in raw:
            continue
        relative = raw.split(marker, 1)[1]
        path = folder / 'source' / Path(relative)
        if path.exists():
            positions.setdefault(path, set()).add((int(match.group(2))-1, int(match.group(3))-1))
    changed = 0
    for path, locations in positions.items():
        lines = path.read_text(encoding='utf-8').splitlines(keepends=True)
        by_line = {}
        for line, column in locations:
            by_line.setdefault(line, set()).add(column)
        for line_no, columns in by_line.items():
            line = lines[line_no]
            for column in sorted(columns, reverse=True):
                if line[column:column+10] == 'Expression':
                    line = line[:column] + 'System.Linq.Expressions.Expression' + line[column+10:]
                    changed += 1
            lines[line_no] = line
        if changed:
            path.write_text(''.join(lines), encoding='utf-8')
    return changed


def managed(path):
    try:
        d = path.read_bytes()
        p = struct.unpack_from('<I', d, 60)[0] + 24
        q = p + (96 if struct.unpack_from('<H', d, p)[0] == 0x10b else 112)
        return struct.unpack_from('<I', d, q + 112)[0] != 0
    except (OSError, struct.error):
        return False


def run(args, timeout=90):
    return subprocess.run([str(x) for x in args], capture_output=True, timeout=timeout, env=ENV)


def prepare_tool():
    folder = AUTO / 'tooling'
    folder.mkdir(parents=True, exist_ok=True)
    output = folder / 'RecoveredModuleResources.dll'
    src = ROOT / 'tools' / 'RecoveredModuleResources.cs'
    command = [DOTNET, CSC, '-nologo', '-target:exe', '-out:' + str(output)]
    command += ['-r:' + str(p) for p in RUNTIME.glob('*.dll') if managed(p)]
    command += ['-r:' + str(CECIL), src]
    result = run(command)
    (folder / 'compile.log').write_bytes(result.stdout + result.stderr)
    if result.returncode:
        raise RuntimeError('Resource metadata helper compilation failed')
    shutil.copyfile(CECIL, folder / CECIL.name)
    output.with_suffix('.runtimeconfig.json').write_text(json.dumps({'runtimeOptions': {
        'tfm': 'net10.0', 'framework': {'name': 'Microsoft.NETCore.App', 'version': '10.0.12'}}}), encoding='utf-8')
    return output


def local_path(base, value):
    path = (base / value.replace('\\', '/')).resolve()
    if not path.is_relative_to(base.resolve()):
        raise RuntimeError('Source include leaves recovered project')
    return path


def prepare(csproj, tool):
    name = csproj.stem
    folder = AUTO / name
    manifest_file = folder / 'recovery.json'
    if manifest_file.exists():
        return
    folder.mkdir(parents=True, exist_ok=True)
    tree = ET.parse(csproj).getroot()
    props = {n.tag.split('}')[-1]: n.text for g in tree if g.tag.endswith('PropertyGroup') for n in g}
    target_name = props.get('AssemblyName', name)
    target = BASELINE / (target_name + ('.dll' if props.get('OutputType', 'Library').lower() == 'library' else '.exe'))
    if not target.exists():
        raise RuntimeError('Missing baseline for ' + name)
    recovery = json.loads((ROOT / 'artifact-inventory.json').read_text(encoding='utf-8'))
    identity = next(x for x in recovery['records'] if x['name'] == target.name)
    if sha(target) != identity['sha256']:
        raise RuntimeError('Baseline changed: ' + target.name)
    source_records = []
    for node in tree.iter():
        if node.tag.split('}')[-1] != 'Compile':
            continue
        original = local_path(csproj.parent, node.attrib['Include'])
        relative = original.relative_to(csproj.parent.resolve())
        export_relative = str(relative)
        if len(str(folder / 'source' / relative)) > 235:
            relative = Path('_long') / (hashlib.sha256(export_relative.encode()).hexdigest()[:16] + '.cs')
        output = folder / 'source' / relative
        output.parent.mkdir(parents=True, exist_ok=True)
        if output.exists():
            if sha(output) != sha(original):
                raise RuntimeError('Existing editable source differs; refusing overwrite: ' + str(output))
        else:
            shutil.copyfile(longpath(original), longpath(output))
        source_records.append({'path': str(relative), 'export_path': export_relative, 'raw_sha256': sha(original)})
    resources = run([DOTNET, tool, target, folder / 'resources'])
    if resources.returncode:
        raise RuntimeError('Resource inventory failed: ' + name)
    metadata = json.loads(resources.stdout)
    (folder / 'metadata.json').write_text(json.dumps(metadata, indent=2), encoding='utf-8')
    manifest = {'name': name, 'baseline': str(target.relative_to(ROOT)), 'baseline_sha256': identity['sha256'],
                'export_project': str(csproj.relative_to(ROOT)), 'sources': source_records,
                'output_type': props.get('OutputType', 'Library'), 'repairs': [],
                'original_resources': metadata['resources'], 'generated_stubs': False}
    manifest_file.write_text(json.dumps(manifest, indent=2), encoding='utf-8')


def dependency_references(name, tool):
    if not re.fullmatch(r'[A-Za-z0-9_.-]+', name):
        return []
    target = BASELINE / (name + '.dll')
    cache = METADATA_CACHE / (name + '.json')
    if not target.exists():
        return []
    with METADATA_LOCK:
        if cache.exists():
            return json.loads(cache.read_text(encoding='utf-8')).get('references', [])
        output_dir = METADATA_CACHE / (name + '-metadata-only')
        result = run([DOTNET, tool, target, output_dir, '--metadata-only'])
        if result.returncode:
            return []
        cache.parent.mkdir(parents=True, exist_ok=True)
        cache.write_bytes(result.stdout)
        return json.loads(result.stdout.decode('utf-8')).get('references', [])


def refs_for(metadata, name, tool):
    framework_files = list(FRAMEWORK.glob('*.dll')) + list((FRAMEWORK / 'WPF').glob('*.dll')) + list((FRAMEWORK / 'Facades').glob('*.dll'))
    refs = {p.name: p for p in framework_files if managed(p) and p.name not in {
        'System.EnterpriseServices.Wrapper.dll', 'System.EnterpriseServices.Thunk.dll'}}
    missing = []
    for reference in metadata['references']:
        path = BASELINE / (reference + '.dll')
        local = VERIFIED_LOCAL_DEPENDENCIES.get(reference + '.dll')
        if local is not None and local.exists():
            refs[local.name] = local
            continue
        if path.exists() and reference not in {'mscorlib', 'System', 'System.Core'}:
            refs[path.name] = path
        elif reference + '.dll' not in refs:
            missing.append(reference)
    # C# must see transitively referenced assemblies when a public signature
    # exposes one of their types (Roslyn CS0012). Recurse over metadata only.
    queue = [r for r in metadata['references'] if r not in {'mscorlib', 'System', 'System.Core'}]
    visited = set()
    while queue:
        reference = queue.pop()
        if reference in visited:
            continue
        visited.add(reference)
        path = BASELINE / (reference + '.dll')
        local = VERIFIED_LOCAL_DEPENDENCIES.get(reference + '.dll')
        if local is not None and local.exists():
            refs[local.name] = local
            path = None
        if path is not None and path.exists() and path.name not in refs and reference != name:
            refs[path.name] = path
        queue.extend(r for r in dependency_references(reference, tool) if r not in visited and r not in {'mscorlib', 'System', 'System.Core'})
    refs.pop(name + '.dll', None)
    return list(refs.values()), missing


def build_one(folder, tool):
    started = time.monotonic()
    recovery = json.loads((folder / 'recovery.json').read_text(encoding='utf-8'))
    metadata = json.loads((folder / 'metadata.json').read_text(encoding='utf-8'))
    target = ROOT / recovery['baseline']
    if sha(target) != recovery['baseline_sha256']:
        raise RuntimeError('Baseline changed')
    refs, missing = refs_for(metadata, recovery['name'], tool)
    sources = [folder / 'source' / x['path'] for x in recovery['sources']]
    lowered_methodof = 0
    for source in sources:
        original_source = source.read_text(encoding='utf-8')
        updated_source, count = lower_ilspy_methodof(original_source)
        if count:
            source.write_text(updated_source, encoding='utf-8')
            lowered_methodof += count
    qualified_expression = qualify_ambiguous_expression(folder)
    source_hashes = {str(p.relative_to(folder)): sha(p) for p in sources}
    build = folder / 'build'
    build.mkdir(exist_ok=True)
    kind = recovery['output_type'].lower()
    output = build / (recovery['name'] + ('.dll' if kind == 'library' else '.exe'))
    command = ['-nologo', '-preferreduilang:en-US', '-nostdlib+', '-unsafe+', '-deterministic+',
               '-langversion:latest', '-nowarn:0436', '-target:' + ('library' if kind == 'library' else 'winexe' if kind == 'winexe' else 'exe'), '-out:' + str(output)]
    command += ['-r:' + str(p) for p in refs]
    command += ['-resource:' + str(folder / 'resources' / r['file']) + ',' + r['name'] for r in metadata['resources']]
    if kind != 'library' and metadata.get('entryPoint'):
        command.append('-main:' + metadata['entryPoint'].replace('/', '.'))
    command += [str(p) for p in sources]
    # Response files keep every path as a compiler argument, without shell interpolation.
    response = build / 'compile.rsp'
    response.write_text('\n'.join('"' + x.replace('"', '\\"') + '"' for x in command), encoding='utf-8')
    try:
        result = run([DOTNET, CSC, '@' + str(response)], timeout=90)
        stdout = result.stdout + result.stderr
        code = result.returncode
    except subprocess.TimeoutExpired as error:
        stdout = (error.stdout or b'') + (error.stderr or b'')
        code = 'timeout'
    (build / 'compile.log').write_bytes(stdout)
    errors = re.findall(r'error (CS\d+):', stdout.decode('utf-8', errors='replace'))
    record = {'name': recovery['name'], 'compiled': code == 0, 'exit_code': code,
              'source_files': len(sources), 'source_hashes': source_hashes,
              'source_changed_since_export': any(sha(folder / 'source' / x['path']) != x['raw_sha256'] for x in recovery['sources']),
              'baseline_sha256': recovery['baseline_sha256'], 'output_sha256': sha(output) if code == 0 else None,
              'compiler_sha256': sha(CSC), 'reference_hashes': {p.name: sha(p) for p in refs},
              'missing_declared_references': missing, 'error_counts': {x: errors.count(x) for x in sorted(set(errors))},
              'elapsed_seconds': round(time.monotonic() - started, 2),
              'resources': metadata['resources'], 'repairs': recovery['repairs'] + ([{'kind': 'ILSpy methodof lowering', 'occurrences': lowered_methodof}] if lowered_methodof else []) + ([{'kind': 'Roslyn ambiguous Expression qualification', 'occurrences': qualified_expression}] if qualified_expression else []),
              'validation': 'C# compilation only; runtime and complete API/behavior parity not established.',
              'original_binary_substituted_as_output': False, 'generated_stubs': False}
    (build / 'result.json').write_text(json.dumps(record, indent=2), encoding='utf-8')
    return record


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--prepare', action='store_true', help='Create editable copies once; never overwrite existing source')
    parser.add_argument('--only', action='append', help='Exact recovered project name; may repeat')
    parser.add_argument('--workers', type=int, default=2, choices=[1, 2])
    args = parser.parse_args()
    projects = sorted(EXPORT.glob('*/*.csproj'))
    if args.only:
        projects = [p for p in projects if p.stem in args.only]
    if not projects:
        raise RuntimeError('No matching exported projects')
    AUTO.mkdir(exist_ok=True)
    if args.prepare:
        tool = prepare_tool()
        for csproj in projects:
            prepare(csproj, tool)
    folders = [AUTO / p.stem for p in projects]
    if any(not (p / 'recovery.json').exists() for p in folders):
        raise RuntimeError('Prepare source copies first with --prepare')
    results = []
    tool = prepare_tool()
    with ThreadPoolExecutor(max_workers=args.workers) as executor:
        pending = {executor.submit(build_one, p, tool): p.name for p in folders}
        for future in as_completed(pending):
            record = future.result()
            results.append(record)
            print(record['name'] + ': ' + ('COMPILED' if record['compiled'] else 'blocked'), flush=True)
    previous_file = AUTO / 'compilation-census.json'
    previous = json.loads(previous_file.read_text(encoding='utf-8')) if previous_file.exists() else {'modules': []}
    merged = {r['name']: r for r in previous['modules']}
    merged.update({r['name']: r for r in results})
    census = {'modules': sorted(merged.values(), key=lambda r: r['name']),
              'attempted': len(merged), 'compiled': sum(r['compiled'] for r in merged.values()),
              'target_executed': False, 'scope': 'Actual source compilation against original managed dependency snapshots; not a source-only complete application.'}
    previous_file.write_text(json.dumps(census, indent=2), encoding='utf-8')
    print('Census: ' + str(census['compiled']) + '/' + str(census['attempted']) + ' source modules compile.')


if __name__ == '__main__':
    main()

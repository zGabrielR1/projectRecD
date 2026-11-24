"""Recover explicit SuiteG6 managed artifacts into an offline source workspace.

Does not run vendor entrypoints, read configuration, inspect databases or install
tools. Output is decompiler reconstruction, not the original source checkout.
"""
import argparse
import hashlib
import json
import os
import shutil
import struct
import subprocess
import time
import xml.etree.ElementTree as ET
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SUITE = Path('C:/Digisat/SuiteG6')
DNSPY = Path('D:/Downloads/stuff/winUtils/dnSpy-net-win64/dnSpy.Console.exe')
ORIGINS = ['Sistema', 'Servidor', 'Sincronizador', 'Sistema/Modulos']
PREFIXES = ('DigisatServer.', 'Modulo.', 'Otus.')
ENTRYPOINTS = {'SistemaG6.exe', 'ServidorG6.exe', 'SincronizadorG6.exe'}


def sha(path):
    return hashlib.sha256(native(path).read_bytes()).hexdigest()


def native(path):
    absolute = str(path.absolute())
    return Path('\\\\?\\' + absolute) if os.name == 'nt' and not absolute.startswith('\\\\?\\') else path


def managed(path):
    # File-backed PE CLI directory; no CLR load or execution.
    try:
        with path.open('rb') as stream:
            if stream.read(2) != b'MZ':
                return False
            stream.seek(60)
            pe = struct.unpack('<I', stream.read(4))[0]
            stream.seek(pe)
            if stream.read(4) != b'PE\0\0':
                return False
            optional = pe + 24
            stream.seek(optional)
            magic = struct.unpack('<H', stream.read(2))[0]
            if magic not in (0x10b, 0x20b):
                return False
            stream.seek(optional + (96 if magic == 0x10b else 112) + 14 * 8)
            rva, size = struct.unpack('<II', stream.read(8))
            return bool(rva and size)
    except (OSError, struct.error):
        return False


def inventory():
    selected, alternatives = {}, {}
    for relative in ORIGINS:
        for path in sorted((SUITE / relative).iterdir()):
            if not path.is_file() or path.suffix.lower() not in ('.dll', '.exe'):
                continue
            if not managed(path):
                continue
            entry = {'name': path.name, 'path': str(path), 'sha256': sha(path), 'bytes': path.stat().st_size}
            if path.name not in selected:
                selected[path.name] = entry
            else:
                alternatives.setdefault(path.name, []).append(entry)
    for name, entry in selected.items():
        entry['alternatives'] = alternatives.get(name, [])
        entry['variant_hashes'] = sorted({entry['sha256']} | {v['sha256'] for v in entry['alternatives']})
        entry['recover_source'] = name.startswith(PREFIXES) or name in ENTRYPOINTS
    return list(selected.values())


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--workspace', type=Path, default=ROOT)
    parser.add_argument('--source-prefix', action='append', help='Only export matching source targets; snapshot all managed dependencies.')
    parser.add_argument('--inventory-only', action='store_true')
    parser.add_argument('--threads', type=int, default=2)
    parser.add_argument('--finalize-existing', action='store_true', help='Index preserved export without executing the decompiler again.')
    args = parser.parse_args()
    workspace = args.workspace.resolve()
    destination = workspace / 'recovered' / 'SuiteG6'
    if destination.exists() and not (args.finalize_existing or args.inventory_only):
        raise SystemExit('Recovered tree already exists. Choose a new workspace for another recovery.')
    workspace.mkdir(parents=True, exist_ok=True)
    records = inventory()
    if args.source_prefix:
        for entry in records:
            entry['recover_source'] = entry['recover_source'] and entry['name'].startswith(tuple(args.source_prefix))
    manifest = {'target_executed': False, 'origin_precedence': ORIGINS,
                'selection': 'Top-level managed DLL/EXE in four explicit code directories; vendor-prefixed modules and three named entrypoints are source targets.',
                'excluded': ['configuration files', 'logs', 'backups', 'customer data', 'non-code directories', 'native binaries'],
                'records': records}
    inventory_path = workspace / 'artifact-inventory.json'
    if args.finalize_existing:
        if not inventory_path.is_file() or manifest != json.loads(inventory_path.read_text(encoding='utf-8')):
            raise SystemExit('Installed inventory differs from the existing export. Use a new workspace.')
    else:
        if destination.exists():
            raise SystemExit('Keep the existing export inventory pinned. Choose a new workspace.')
        inventory_path.write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    if args.inventory_only:
        print(json.dumps({'managed_names': len(records), 'source_targets': sum(x['recover_source'] for x in records),
                          'variant_names': sum(len(x['variant_hashes']) > 1 for x in records)}))
        return
    baseline = workspace / 'baseline'
    baseline.mkdir(exist_ok=True)
    targets = []
    for entry in records:
        source = Path(entry['path'])
        if sha(source) != entry['sha256']:
            raise RuntimeError('Artifact changed after inventory: ' + entry['name'])
        copy = baseline / entry['name']
        if copy.exists() and sha(copy) != entry['sha256']:
            raise RuntimeError('Existing snapshot differs; select a new workspace: ' + entry['name'])
        if not copy.exists():
            shutil.copyfile(source, copy)
        if sha(copy) != entry['sha256']:
            raise RuntimeError('Snapshot mismatch: ' + entry['name'])
        if entry['recover_source']:
            targets.append(copy)
    destination.mkdir(parents=True, exist_ok=True)
    command = [str(DNSPY), '--no-color', '--no-baml', '--no-resx', '--sdk-project',
               '--threads', str(max(1, min(args.threads, 4))), '--sln-name', 'SuiteG6.Recovered.sln',
               '--asm-path', str(baseline), '-o', str(destination)] + [p.name for p in targets]
    started = time.time()
    exit_code = None
    if not args.finalize_existing:
        with (workspace / 'recovery-stdout.log').open('wb') as stdout, (workspace / 'recovery-stderr.log').open('wb') as stderr:
            run = subprocess.run(command, cwd=baseline, stdout=stdout, stderr=stderr, timeout=1800)
        exit_code = run.returncode
        (workspace / 'recovery-process.json').write_text(json.dumps({'exit_code': exit_code,
            'elapsed_seconds': round(time.time() - started, 2)}, indent=2), encoding='utf-8')
    elif (workspace / 'recovery-process.json').exists():
        exit_code = json.loads((workspace / 'recovery-process.json').read_text())['exit_code']
    extensionless = destination / 'SuiteG6.Recovered'
    if extensionless.exists() and not (destination / 'SuiteG6.Recovered.sln').exists():
        extensionless.rename(destination / 'SuiteG6.Recovered.sln')
    projects = []
    for project in sorted(destination.rglob('*.csproj')):
        tree = ET.parse(native(project))
        # Point recovered references to the frozen local snapshot, never install paths.
        changed = 0
        for ref in tree.getroot().iter('Reference'):
            simple = ref.attrib.get('Include', '').split(',')[0]
            candidate = baseline / (simple + '.dll')
            if candidate.exists():
                hint = ref.find('HintPath')
                if hint is None:
                    hint = ET.SubElement(ref, 'HintPath')
                hint.text = os.path.relpath(candidate, project.parent).replace('/', '\\')
                changed += 1
        tree.write(native(project), encoding='utf-8', xml_declaration=True)
        sources = sorted(project.parent.rglob('*.cs'))
        projects.append({'name': project.stem, 'project': str(project.relative_to(workspace)),
                         'source_files': len(sources), 'source_bytes': sum(native(p).stat().st_size for p in sources),
                         'snapshot_references': changed, 'status': 'decompiled-not-build-verified'})
    for entry in records:
        if sha(Path(entry['path'])) != entry['sha256']:
            raise RuntimeError('Installed artifact changed during recovery: ' + entry['name'])
    result = {'target_executed': False, 'tool': str(DNSPY), 'tool_sha256': sha(DNSPY),
              'exit_code': exit_code, 'finalized_existing_export': args.finalize_existing,
              'exit_status_retention': 'producer code retained' if exit_code is not None else 'first export code unavailable after Windows path-index failure; preserved project tree indexed without rerunning producer',
              'elapsed_seconds_this_step': round(time.time() - started, 2),
              'selected_targets': len(targets), 'recovered_projects': len(projects),
              'source_files': sum(p['source_files'] for p in projects),
              'source_bytes': sum(p['source_bytes'] for p in projects), 'projects': projects,
              'limitations': ['Decompiler reconstruction, not original project structure or sources.',
                              'Project export is not proof of successful compilation or behavioral equivalence.',
                              'BAML retained as resources; original XAML/designer sources are not recovered.',
                              'Duplicate names select recorded origin precedence; distinct variants remain listed.',
                              'Resources are exported code artifacts; no customer/config directories copied.']}
    (workspace / 'recovery-manifest.json').write_text(json.dumps(result, indent=2), encoding='utf-8')
    print(json.dumps({k: v for k, v in result.items() if k not in ('projects', 'limitations')}))
    if exit_code not in (0, None) or len(projects) != len(targets):
        raise SystemExit('Recovery incomplete; inspect manifests/logs. Preserved tree has not been discarded.')


if __name__ == '__main__':
    main()

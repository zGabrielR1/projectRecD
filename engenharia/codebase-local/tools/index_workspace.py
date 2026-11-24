"""Index completed reconstruction and create the verified build solution."""
import hashlib
import json
import os
import uuid
import xml.etree.ElementTree as ET
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def native(path):
    return Path('\\\\?\\' + str(path.absolute())) if os.name == 'nt' else path


def main():
    recovery = json.loads((ROOT / 'recovery-manifest.json').read_text(encoding='utf-8'))
    inventory = json.loads((ROOT / 'artifact-inventory.json').read_text(encoding='utf-8'))
    lines = ['# Recovered project index', '',
             'All entries below are decompiler-exported source projects. Compilation is not inferred from extraction.', '',
             '| Assembly | Source files | Source bytes | Local project |', '| --- | ---: | ---: | --- |']
    for p in recovery['projects']:
        lines.append('| ' + p['name'] + ' | ' + str(p['source_files']) + ' | ' + str(p['source_bytes']) +
                     ' | [open project](' + p['project'].replace('\\', '/') + ') |')
    (ROOT / 'PROJECTS.md').write_text('\n'.join(lines) + '\n', encoding='utf-8')
    selected = ['DigisatServer.Infrastructure.Data.Core', 'Recovered.ReportService']
    sln = ['Microsoft Visual Studio Solution File, Format Version 12.00', '# Visual Studio Version 17',
           'VisualStudioVersion = 17.0.31903.59', 'MinimumVisualStudioVersion = 10.0.40219.1']
    ids = []
    for name in selected:
        project = ROOT / 'projects' / name / (name + '.csproj')
        if not project.exists():
            raise RuntimeError('Verified project missing: ' + str(project))
        project_xml = ET.parse(project).getroot()
        declared_guid = project_xml.find('.//{*}ProjectGuid')
        guid = declared_guid.text if declared_guid is not None else '{' + str(uuid.uuid5(uuid.NAMESPACE_URL, 'digisat-local/' + name)).upper() + '}'
        ids.append(guid)
        path = str(project.relative_to(ROOT)).replace('/', '\\')
        sln += ['Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "' + name + '", "' + path + '", "' + guid + '"']
        if name == 'Recovered.ReportService':
            sln += ['\tProjectSection(ProjectDependencies) = postProject', '\t\t' + ids[0] + ' = ' + ids[0], '\tEndProjectSection']
        sln += ['EndProject']
    sln += ['Global', '\tGlobalSection(SolutionConfigurationPlatforms) = preSolution',
            '\t\tDebug|Any CPU = Debug|Any CPU', '\tEndGlobalSection',
            '\tGlobalSection(ProjectConfigurationPlatforms) = postSolution']
    for guid in ids:
        sln += ['\t\t' + guid + '.Debug|Any CPU.ActiveCfg = Debug|AnyCPU',
                '\t\t' + guid + '.Debug|Any CPU.Build.0 = Debug|AnyCPU']
    sln += ['\tEndGlobalSection', 'EndGlobal']
    (ROOT / 'SuiteG6.Verified.sln').write_text('\n'.join(sln) + '\n', encoding='utf-8')
    def fingerprint(path):
        if not native(path).is_file():
            return None
        return {'path': path.relative_to(ROOT).as_posix(), 'bytes': native(path).stat().st_size,
                'sha256': hashlib.sha256(native(path).read_bytes()).hexdigest()}
    files = []
    paths = sorted((ROOT / 'recovered' / 'SuiteG6').rglob('*'))
    with ThreadPoolExecutor(max_workers=8) as pool:
        for record in pool.map(fingerprint, paths):
            if record:
                files.append(record)
                if len(files) % 5000 == 0:
                    print('Indexed ' + str(len(files)) + ' recovered files...', flush=True)
    encoded = json.dumps(files, ensure_ascii=False, separators=(',', ':')).encode('utf-8')
    (ROOT / 'recovered-file-hashes.json').write_bytes(encoded)
    summary = {'source_targets': recovery['selected_targets'], 'recovered_projects': recovery['recovered_projects'],
               'source_files': recovery['source_files'], 'source_bytes': recovery['source_bytes'],
               'managed_dependency_names': len(inventory['records']),
               'names_with_distinct_variants': sum(len(r['variant_hashes']) > 1 for r in inventory['records']),
               'recovered_tree_files': len(files), 'file_index_sha256': hashlib.sha256(encoded).hexdigest(),
               'verified_solution_projects': selected,
               'domain_core': 'Full source recovered; compilation blocked by obfuscator/decompiler constructs',
               'original_vendor_application_executed': False}
    (ROOT / 'workspace-summary.json').write_text(json.dumps(summary, indent=2), encoding='utf-8')
    print(json.dumps(summary))


if __name__ == '__main__':
    main()

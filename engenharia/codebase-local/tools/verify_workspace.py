"""Check local reconstruction coverage and recorded successful source builds."""
import hashlib
import json
import os
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def native(path):
    return Path('\\\\?\\' + str(path.absolute())) if os.name == 'nt' else path


def read(relative):
    return json.loads((ROOT / relative).read_text(encoding='utf-8'))


def main():
    recovery = read('recovery-manifest.json')
    assert recovery['selected_targets'] == recovery['recovered_projects'] == 218
    assert recovery['source_files'] == 22126
    for project in recovery['projects']:
        assert native(ROOT / project['project']).is_file(), project['name']
    for filename, expected in [('recovered/SuiteG6/SuiteG6.Recovered.sln', 218), ('SuiteG6.Verified.sln', 2)]:
        text = (ROOT / filename).read_text(encoding='utf-8-sig')
        paths = re.findall(r'^Project\([^\n]+?= "[^"]+", "([^"]+\.csproj)"', text, re.MULTILINE)
        assert len(paths) == expected, (filename, len(paths))
        assert all(native((ROOT / filename).parent / p).is_file() for p in paths)
    build = read('workspace-build.json')
    assert len(build['results']) == 2 and all(r['exit_code'] == 0 for r in build['results'])
    data = read('projects/DigisatServer.Infrastructure.Data.Core/build-manifest.json')
    tests = read('projects/DigisatServer.Infrastructure.Data.Core/tests/manifest.json')
    report = read('projects/Recovered.ReportService/evidence/build-manifest.json')
    assert data['all_original_named_type_surfaces_equal'] and data['embedded_resources_equal']
    assert sum(r['assertions'] for r in tests['runs']) == 19
    assert report['compared_declared_methods'] == 74 and report['behavior_assertions'] == 16
    assert report['source_linked_data_core']['sha256'] == data['output_sha256']
    assert hashlib.sha256((ROOT / 'projects/DigisatServer.Infrastructure.Data.Core/bin/DigisatServer.Infrastructure.Data.Core.dll').read_bytes()).hexdigest() == data['output_sha256']
    domain = read('projects/DigisatServer.Domain.Core/build-manifest.json')
    assert domain['status'] == 'source-recovered-build-blocked'
    result = {'recovered_projects_present': 218, 'csharp_files_recorded': recovery['source_files'],
              'verified_solution_projects': 2, 'selected_behavior_assertions': 35,
              'source_linked_data_core_matches': True, 'domain_core_build_block_recorded': True,
              'scope': 'Recorded coverage, solution paths, build outputs and selected proofs; not full product equivalence'}
    (ROOT / 'workspace-verification.json').write_text(json.dumps(result, indent=2), encoding='utf-8')
    print(json.dumps(result))


if __name__ == '__main__':
    main()

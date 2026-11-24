"""Compare offline Data.Core behavior in a pinned installed-build snapshot."""
import argparse
import json
import shutil
import subprocess
import tempfile
from pathlib import Path

from build_data_core import CSC, DOTNET, FRAMEWORK, NAME, PROJECT, SYSTEM_REFS, TEST_REFS, VENDOR_REFS, sha


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--workspace', type=Path, required=True)
    snapshot = parser.parse_args().workspace.resolve()
    baseline = snapshot / 'baseline'
    project = snapshot / 'projects-auto' / Path(NAME).stem
    build = json.loads((project / 'build/result.json').read_bytes())
    if not build['compiled']:
        raise SystemExit('Compile the editable Data.Core project before testing.')
    original = baseline / NAME
    rebuilt = project / build.get('output_file', 'build/' + NAME)
    if sha(original) != build['baseline_sha256'] or sha(rebuilt) != build['output_sha256']:
        raise SystemExit('The DLLs differ from the recorded build inputs or output.')

    dependencies = [baseline / name for name in VENDOR_REFS + TEST_REFS]
    references = [FRAMEWORK / name for name in SYSTEM_REFS + ['System.Web.Extensions.dll']]
    report = {'baseline_sha256': sha(original), 'rebuilt_sha256': sha(rebuilt),
              'dependency_hashes': {path.name: sha(path) for path in dependencies}, 'runs': {}}
    with tempfile.TemporaryDirectory(prefix='suiteg6-data-core-') as directory:
        sandbox = Path(directory)
        for path in dependencies:
            shutil.copyfile(path, sandbox / path.name)
        for name in ('SyncRuntimeProof', 'ReplicationRuntimeProof'):
            source = PROJECT / 'tests' / (name + '.cs')
            executable = sandbox / (name + '.exe')
            subprocess.run([str(DOTNET), str(CSC), '-nologo', '-nostdlib+', '-target:exe',
                            '-out:' + str(executable)] +
                           ['-r:' + str(path) for path in references + dependencies + [original]] +
                           [str(source)], cwd=sandbox, check=True, timeout=45)
            runs = {}
            for label, artifact in [('original', original), ('rebuilt', rebuilt)]:
                shutil.copyfile(artifact, sandbox / NAME)
                result = subprocess.run([str(executable)], cwd=sandbox, check=True,
                                        capture_output=True, text=True, timeout=18)
                runs[label] = json.loads(result.stdout)
            before, after = runs['original'], runs['rebuilt']
            equal = before['checks'] == after['checks'] and before['assertions'] == after['assertions']
            report['runs'][name] = {'source_sha256': sha(source), 'equal': equal, **runs}
            print(f"{name}: {before['assertions']} assertions, {'match' if equal else 'different'}", flush=True)
    (snapshot / 'data-core-runtime-parity.json').write_text(json.dumps(report, indent=2) + '\n', encoding='utf-8')
    if not all(result['equal'] for result in report['runs'].values()):
        raise SystemExit('Selected rebuilt behavior differs from the original.')


if __name__ == '__main__':
    main()

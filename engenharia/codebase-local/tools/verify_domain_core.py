"""Compare selected original Domain.Core surfaces/behavior to a local rebuilt DLL."""
from pathlib import Path
import argparse
from collections import Counter
import hashlib
import json
import os
import shutil
import subprocess

ROOT = Path(__file__).resolve().parents[1]
PROJECT = ROOT / 'projects/DigisatServer.Domain.Core'
ORIGIN = Path('C:/Digisat/SuiteG6/Sistema')
EXPECTED = 'fcd29ec09a65eaa8397ccc6b63f6630c61b6fb99293c4a863162d7382928d9ff'
NAME = 'DigisatServer.Domain.Core.dll'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
DEPENDENCIES = ['DigisatServer.Infrastructure.Data.Core.dll', 'DigisatServer.Infrastructure.Crosscutting.dll', 'MongoDB.Bson.dll']


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def command(args, log):
    env = os.environ.copy()
    env['DOTNET_ROLL_FORWARD'] = 'Major'
    result = subprocess.run([str(x) for x in args], env=env, capture_output=True, text=True, timeout=30)
    log.write_text(result.stdout + result.stderr, encoding='utf-8')
    if result.returncode:
        raise RuntimeError('Command failed; see ' + str(log))
    return result.stdout


def behavior(target, mode):
    destination = PROJECT / 'tests' / mode
    destination.mkdir(parents=True, exist_ok=True)
    sources = [target] + [ORIGIN / name for name in DEPENDENCIES]
    identity = []
    for source in sources:
        name = NAME if source == target else source.name
        expected = sha(source)
        shutil.copyfile(source, destination / name)
        if sha(destination / name) != expected:
            raise RuntimeError('Dependency copy mismatch')
        identity.append({'path': str(source), 'sha256': expected})
    refs = [FRAMEWORK / name for name in ['mscorlib.dll', 'System.dll', 'System.Core.dll', 'System.Web.Extensions.dll']]
    refs += [destination / name for name in [NAME] + DEPENDENCIES]
    executable = destination / 'DomainCoreBehavior.exe'
    command([DOTNET, CSC, '-nologo', '-nostdlib+', '-target:exe', '-out:' + str(executable)] +
            ['-r:' + str(ref) for ref in refs] + [PROJECT / 'DomainCoreBehavior.cs'],
            PROJECT / 'logs' / ('compile-behavior-' + mode + '.log'))
    data = json.loads(command([executable], PROJECT / 'logs' / ('behavior-' + mode + '.log')))
    output = PROJECT / 'tests' / (mode + '-behavior.json')
    output.write_text(json.dumps(data, indent=2), encoding='utf-8')
    if any(sha(Path(x['path'])) != x['sha256'] for x in identity):
        raise RuntimeError('Input changed during behavior verification')
    return data, {'mode': mode, 'inputs': identity, 'assertions': data['assertions'],
                  'results_sha256': sha(output), 'harness_sha256': sha(PROJECT / 'DomainCoreBehavior.cs')}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('rebuilt', type=Path)
    args = parser.parse_args()
    rebuilt = args.rebuilt.resolve()
    if sha(ORIGIN / NAME) != EXPECTED:
        raise RuntimeError('Unreviewed original target')
    original = json.loads((PROJECT / 'original-metadata.json').read_text())
    observed = json.loads(command([DOTNET, PROJECT / 'tooling/ModuleProof.dll', rebuilt], PROJECT / 'logs' / 'il-rebuilt-metadata.log'))
    (PROJECT / 'il-rebuilt-metadata.json').write_text(json.dumps(observed, indent=2), encoding='utf-8')
    original_api = {x['name']: x for x in original['api']}
    observed_api = {x['name']: x for x in observed['api']}
    original_resources = sorted(original['resources'], key=lambda x: x['name'])
    rebuilt_resources = sorted(observed['resources'], key=lambda x: x['name'])
    original_refs = Counter(original['references'])
    rebuilt_refs = Counter(observed['references'])
    omitted_refs = list((original_refs - rebuilt_refs).elements())
    added_refs = list((rebuilt_refs - original_refs).elements())
    expected_obfuscator_ref = 'mscorlib, Version=65535.65535.65535.65535, Culture=neutral, PublicKeyToken=b77a5c561934e089'
    comparison = {'all_original_named_type_surfaces_equal': all(observed_api.get(k) == v for k, v in original_api.items()),
                  'resources_equal': original_resources == rebuilt_resources,
                  'assembly_references_equal': original['references'] == observed['references'],
                  'only_reference_difference_is_unused_obfuscator_ref': omitted_refs == [expected_obfuscator_ref] and not added_refs,
                  'type_count_equal': original['typeCount'] == observed['typeCount'],
                  'method_count_equal': original['methodCount'] == observed['methodCount']}
    first, first_record = behavior(ORIGIN / NAME, 'original')
    second, second_record = behavior(rebuilt, 'il-rebuilt')
    comparison['selected_checks_equal'] = first['checks'] == second['checks']
    result = {'original_sha256': EXPECTED, 'rebuilt_sha256': sha(rebuilt),
              'rebuilt_mvid': observed['mvid'], 'comparison': comparison,
              'runs': [first_record, second_record],
              'scope': 'API/resource/reference metadata plus 13 selected pure domain checks; no complete application equivalence claim.'}
    (PROJECT / 'il-verification.json').write_text(json.dumps(result, indent=2), encoding='utf-8')
    if not all(comparison[k] for k in ['all_original_named_type_surfaces_equal', 'resources_equal',
                                       'only_reference_difference_is_unused_obfuscator_ref',
                                       'type_count_equal', 'method_count_equal', 'selected_checks_equal']):
        raise RuntimeError('Verification difference; see il-verification.json')
    print('Domain.Core CIL rebuild verified: API, resource bytes, and 13 native behavior checks match; one unused obfuscator reference was dropped.')


if __name__ == '__main__':
    main()

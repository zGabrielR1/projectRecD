"""Recreate the current report-service component from local installed artifacts.

Offline source compilation and selected baseline checks. Never starts SuiteG6.
"""
from pathlib import Path
import difflib
import hashlib
import json
import os
import re
import shutil
import struct
import subprocess
import argparse

ROOT = Path(__file__).resolve().parents[1]
REPO = ROOT.parents[1]
PROJECT = ROOT / 'projects' / 'Recovered.ReportService'
ORIGIN = Path('C:/Digisat/SuiteG6/Sistema')
EXPECTED = 'f27cb00fdbee2851ad9119a32a435943c58c072ca102fe88ef0e3f001938cd9d'
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
FRAMEWORK = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319')
OLD_TOOLS = REPO / 'engenharia/recompilacao-relatorios-2026-10-30/build'
NAMES = ['DevExpress.Mvvm.v15.2.dll', 'DigisatServer.Application.Core.dll',
         'DigisatServer.Application.Main.Dto.dll', 'DigisatServer.Domain.Core.dll',
         'DigisatServer.Domain.Main.dll', 'DigisatServer.Infrastructure.Crosscutting.dll',
         'DigisatServer.Infrastructure.Crosscutting.Concentrador.dll',
         'DigisatServer.Infrastructure.Data.Core.dll', 'MongoDB.Bson.dll']
NAMES += ['DigisatServer.Infrastructure.Crosscutting.ArquivoContabilidade.dll',
          'DigisatServer.Infrastructure.Crosscutting.RegistrosPafEcf.dll']


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def run(command, log, timeout=120):
    env = dict(os.environ, DOTNET_ROLL_FORWARD='Major')
    result = subprocess.run([str(x) for x in command], capture_output=True, timeout=timeout, env=env)
    log.write_bytes(result.stdout + result.stderr)
    if result.returncode:
        raise RuntimeError('Command failed; inspect ' + str(log))
    return result.stdout


def managed(path):
    data = path.read_bytes()
    try:
        optional = struct.unpack_from('<I', data, 60)[0] + 24
        directories = optional + (96 if struct.unpack_from('<H', data, optional)[0] == 0x10b else 112)
        return struct.unpack_from('<I', data, directories + 112)[0] != 0
    except (IndexError, struct.error):
        return False


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    mode = parser.add_mutually_exclusive_group()
    mode.add_argument('--recover', action='store_true')
    mode.add_argument('--compile-only', action='store_true', help='Default: compile local edited source, no recovery')
    parser.add_argument('--force', action='store_true', help='Allow explicit recovery to replace local source')
    parser.add_argument('--source-linked', action='store_true', help='Compile against the sibling source-rebuilt Data.Core DLL')
    args = parser.parse_args()
    PROJECT.mkdir(parents=True, exist_ok=True)
    for folder in ['references', 'bin', 'evidence', 'tooling', 'baseline']:
        (PROJECT / folder).mkdir(exist_ok=True)
    target = ORIGIN / 'DigisatServer.Application.Main.dll'
    if args.recover and sha(target) != EXPECTED:
        raise RuntimeError('Unreviewed current report assembly')
    raw = PROJECT / 'MovimentacaoSaidaRelatorioAppService.raw.cs'
    helper = PROJECT / 'Parcial.cs'
    source = PROJECT / 'MovimentacaoSaidaRelatorioAppService.cs'
    info = PROJECT / 'AssemblyInfo.cs'
    prior_path = PROJECT / 'evidence' / 'build-manifest.json'
    prior = json.loads(prior_path.read_text(encoding='utf-8')) if prior_path.exists() else {}
    renamed = prior.get('renamed_generated_identifiers')
    repairs = prior.get('lambda_typing_repairs', {})
    reference_records = prior.get('references', [])
    if args.recover:
        if source.exists() and not args.force:
            raise RuntimeError('Local source exists; explicit --recover --force required to overwrite it')
        for path, typename in [(raw, None), (helper, 'DigisatServer.Application.Main.AuxiliaresModulo.Services.Parcial')]:
            command = [DOTNET, OLD_TOOLS / 'RecoverReports.dll', target, path]
            if typename:
                command.append(typename)
            run(command, PROJECT / 'evidence' / ('decompile-' + path.stem + '.log'))
        original = raw.read_text(encoding='utf-8-sig')
        repaired, renamed = re.subn(r'<>h__TransparentIdentifier(\d+)', r'recoveredTransparent\1', original)
        if renamed != 553:
            raise RuntimeError('Unexpected generated identifier count; review required')
        repairs = {
            '(recoveredTransparent1, IItemDocumentoFiscalProduto item)': '(recoveredTransparent1, item)',
            '(recoveredTransparent3, IItemEstoque itemEstoque)': '(recoveredTransparent3, itemEstoque)',
            '(redeTipo, IEnumerable<IInformacaoTransacao> tefs)': '(redeTipo, tefs)',
            '(redeTipo, IEnumerable<Parcela> parcelas)': '(redeTipo, parcelas)',
            '(ObjectId objetoId, itens)': '(objetoId, itens)',
        }
        for before, after in repairs.items():
            if repaired.count(before) != 1:
                raise RuntimeError('Unexpected lambda recovery site')
            repaired = repaired.replace(before, after)
        source.write_text(repaired, encoding='utf-8')
        (PROJECT / 'source-repairs.patch').write_text(''.join(difflib.unified_diff(original.splitlines(True), repaired.splitlines(True), fromfile=raw.name, tofile=source.name)), encoding='utf-8')
        info.write_text('using System.Reflection;\nusing System.Runtime.Versioning;\n[assembly: AssemblyVersion("1.0.0.0")]\n[assembly: AssemblyTitle("Recovered current report service component")]\n[assembly: TargetFramework(".NETFramework,Version=v4.8")]\n', encoding='utf-8')
        reference_records = []
        for name in NAMES:
            path = ORIGIN / name
            digest = sha(path)
            destination = PROJECT / 'references' / name
            shutil.copyfile(path, destination)
            if sha(destination) != digest:
                raise RuntimeError('Reference copy changed')
            reference_records.append({'name': name, 'source': str(path), 'sha256': digest})
        shutil.copyfile(target, PROJECT / 'baseline' / target.name)
        for name in ['RecoverReports.dll', 'RecoverReports.runtimeconfig.json', 'ValidateReportBuild.dll', 'ValidateReportBuild.runtimeconfig.json', 'Mono.Cecil.dll', 'ICSharpCode.Decompiler.dll']:
            shutil.copyfile(OLD_TOOLS / name, PROJECT / 'tooling' / name)
    else:
        if not source.exists() or not helper.exists() or not info.exists() or not prior:
            raise RuntimeError('Local source missing; run --recover explicitly')
        for record in reference_records:
            if sha(PROJECT / 'references' / record['name']) != record['sha256']:
                raise RuntimeError('Local reference changed: ' + record['name'])
    target = PROJECT / 'baseline' / 'DigisatServer.Application.Main.dll'
    if sha(target) != EXPECTED:
        raise RuntimeError('Local baseline changed')
    compile_input_hash = sha(source)
    framework_refs = [p for p in FRAMEWORK.glob('*.dll') if managed(p) and p.name not in {'System.EnterpriseServices.Wrapper.dll', 'System.EnterpriseServices.Thunk.dll'}]
    references = framework_refs + [PROJECT / 'references' / name for name in NAMES]
    source_link = None
    if args.source_linked:
        rebuilt_data = ROOT / 'projects' / 'DigisatServer.Infrastructure.Data.Core' / 'bin' / 'DigisatServer.Infrastructure.Data.Core.dll'
        if not rebuilt_data.exists():
            raise RuntimeError('Build the sibling source Data.Core project first')
        references = [rebuilt_data if p.name == rebuilt_data.name else p for p in references]
        source_link = {'path': str(rebuilt_data), 'sha256': sha(rebuilt_data),
                       'scope': 'Compile-time linkage; selected report helper checks do not exercise Data.Core behavior.'}
    output = PROJECT / 'bin' / 'Recovered.ReportService.dll'
    run([DOTNET, CSC, '-nologo', '-target:library', '-nostdlib+', '-deterministic+', '-nowarn:0436', '-out:' + str(output)] + ['-r:' + str(p) for p in references] + [source, helper, info], PROJECT / 'evidence' / 'compile.log')
    comparison = run([DOTNET, PROJECT / 'tooling' / 'ValidateReportBuild.dll', target, output], PROJECT / 'evidence' / 'api-comparison.json')
    compared = json.loads(comparison)
    # Execute source-compiled helpers alongside the exact original, on CLR4.
    native_csc = FRAMEWORK / 'csc.exe'
    proof_source = PROJECT / 'RebuiltReportProof.cs'
    proof = PROJECT / 'bin' / 'RebuiltReportProof.exe'
    run([native_csc, '/nologo', '/r:System.Web.Extensions.dll', '/out:' + str(proof), proof_source], PROJECT / 'evidence' / 'compile-proof.log')
    behavior_bytes = run([proof, target, output], PROJECT / 'evidence' / 'behavior.log', timeout=20)
    behavior = json.loads(behavior_bytes)
    (PROJECT / 'evidence' / 'behavior.json').write_text(json.dumps(behavior, indent=2), encoding='utf-8')
    recovery_source_hash = sha(source) if args.recover else prior.get('recovery_source_sha256', prior.get('source_sha256'))
    manifest = {'artifact': str(target), 'origin_artifact_path': str(ORIGIN / target.name), 'artifact_sha256': EXPECTED, 'source_raw_sha256': sha(raw),
                'source_sha256': sha(source), 'helper_sha256': sha(helper), 'output_sha256': sha(output),
                'recovery_source_sha256': recovery_source_hash, 'source_changed_since_recovery': sha(source) != recovery_source_hash,
                'compiler': str(CSC), 'compiler_sha256': sha(CSC),
                'decompiler_helper_sha256': sha(PROJECT / 'tooling' / 'RecoverReports.dll'),
                'decompiler_library_sha256': sha(PROJECT / 'tooling' / 'ICSharpCode.Decompiler.dll'),
                'framework_references': 'Installed Windows .NET Framework 4.8 runtime assemblies',
                'source_target_framework': '.NETFramework,Version=v4.6', 'build_target_framework': '.NETFramework,Version=v4.8',
                'renamed_generated_identifiers': renamed, 'lambda_typing_repairs': repairs,
                'source_compilation': True, 'reconstruction_mode': 'compile local source; recovery uses syntax-only repairs', 'cil_rewriting': False,
                'selected_checks_equal_to_original': True,
                'compile_overwrites_source': False,
                'source_linked_data_core': source_link,
                'references': reference_records, 'compared_declared_methods': compared['comparedDeclaredMethods'],
                'behavior_assertions': behavior['assertions'], 'behavior_results_sha256': sha(PROJECT / 'evidence' / 'behavior.json'),
                'scope': 'Complete selected current service class and Parcial, not complete Application.Main assembly.',
                'limitations': ['Original DLLs still provide domain, DTOs and interfaces.', 'No production integration or full method equivalence claim.']}
    (PROJECT / 'evidence' / 'build-manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
    if sha(target) != EXPECTED or any(sha(PROJECT / 'references' / r['name']) != r['sha256'] for r in reference_records):
        raise RuntimeError('Original inputs changed')
    if sha(source) != compile_input_hash:
        raise RuntimeError('Local source changed during compilation')
    print('Compiled current report-service source; compared declared methods: ' + str(compared['comparedDeclaredMethods']))


if __name__ == '__main__':
    main()

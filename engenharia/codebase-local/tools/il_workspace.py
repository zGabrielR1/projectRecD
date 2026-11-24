"""Build original managed modules from textual IL, preserving opaque IL semantics."""
import argparse
import hashlib
import json
import re
import shutil
import subprocess
from pathlib import Path
from recover_workspace import managed

ROOT = Path(__file__).resolve().parents[1]
DOTNET = Path('C:/Program Files/dotnet/dotnet.exe')
CSC = Path('C:/Program Files/dotnet/sdk/11.0.100-rc.2.26504.105/Roslyn/bincore/csc.dll')
RUNTIME = Path('C:/Program Files/dotnet/shared/Microsoft.NETCore.App/10.0.12')
ILASM = Path('C:/Windows/Microsoft.NET/Framework64/v4.0.30319/ilasm.exe')
DEPENDENCIES = ROOT / 'projects/DigisatServer.Infrastructure.Data.Core/tooling'
TOOL = ROOT / 'il-roundtrip/tooling'


def sha(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def make_opaque_token_calls_assemblable(il):
    """Keep obfuscator-invalid call bytes verbatim in CIL data directives.

    ILSpy can display deliberately malformed metadata tokens, but ilasm rejects
    the symbolic token. These instructions live in unreachable obfuscator
    prologues. Emitting the original opcode/token bytes keeps them byte exact.
    """
    source = il.read_text(encoding='utf-8')
    pattern = re.compile(r'^(\s*)(IL_[0-9A-Fa-f]+:)\s+call\s+@([0-9A-Fa-f]{8})(\s+/\*.*?\*/)?\s*$', re.M)
    def replace(match):
        token = bytes.fromhex(match.group(3))[::-1]
        raw = bytes([0x28]) + token
        lines = [match.group(1) + match.group(2) + ' .emitbyte 0x%02X' % raw[0]]
        lines.extend(match.group(1) + '           .emitbyte 0x%02X' % byte for byte in raw[1:])
        return '\n'.join(lines)
    transformed, count = pattern.subn(replace, source)
    # An operand with an owner, member name and parameter list names a method.
    transformed, bad_method_tokens = re.subn(
        r'^(\s*IL_[0-9A-Fa-f]+:\s+ldtoken)[ \t]+(?![ \t]|(?:method|field)\b)(?=[^\n]+::[^\n]+\()',
        r'\1 method ', transformed, flags=re.M)
    # Place variance before generic type constraints, as required by ILAsm.
    transformed, bad_variance = re.subn(
        r'(`1)<\(([^()\n]+)\) -([A-Za-z_]\w*)>',
        lambda m: m.group(1) + '<-(class ' + m.group(2) + ') ' + m.group(3) + '>', transformed)
    resources = sorted(il.parent.glob('*.resources'))
    missing_resources = [p for p in resources if ('.mresource public ' + p.name) not in transformed]
    if missing_resources:
        declaration = ''.join('\n.mresource public ' + p.name + '\n{\n}\n' for p in missing_resources)
        first_type = re.search(r'^\.class\b', transformed, re.M)
        if not first_type:
            raise ValueError('No type declaration for resource insertion')
        transformed = transformed[:first_type.start()] + declaration + transformed[first_type.start():]
    # ILSpy renders exception regions as compact label ranges. ilasm requires
    # structured regions. Re-express their boundaries without moving any IL.
    regions = re.compile(r'^(\s*)\.try (IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+) (finally|fault) (IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+)\s*$', re.M)
    # IL offsets repeat for every method. Resolve boundary labels only in the
    # method that owns the exception region, then apply edits from the end.
    pending = []
    label_edits = {}
    for match in regions.finditer(transformed):
        indent, try_start, try_end, clause, handler_start, handler_end = match.groups()
        method_start = transformed.rfind('.method', 0, match.start())
        method_end = transformed.find('} // end of method', match.end())
        if method_start < 0 or method_end < 0:
            raise ValueError('Exception region is outside a method body')
        method_text = transformed[method_start:method_end]
        for label, replacement in ((try_start, indent + '.try {'),
                                   (try_end, indent + '} ' + clause + ' {'),
                                   (handler_end, indent + '}')):
            label_match = re.search(r'^\s*' + re.escape(label) + r':', method_text, re.M)
            if not label_match:
                raise ValueError('Exception boundary ' + label + ' has no instruction label in its method')
            absolute = method_start + label_match.start()
            line_start = transformed.rfind('\n', 0, absolute) + 1
            label_edits.setdefault(line_start, []).append(replacement)
        pending.append((match.start(), match.end(), ''))
    for pos, replacements in label_edits.items():
        pending.append((pos, pos, '\n'.join(replacements) + '\n'))
    for start, end, replacement_text in sorted(pending, key=lambda item: item[0], reverse=True):
        transformed = transformed[:start] + replacement_text + transformed[end:]
    # Label clauses also support nested catches and distinct handler boundaries.
    transformed, catch_regions = re.subn(
        r'^(\s*\.try )(IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+)( catch .+?) (IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+)[ \t]*$',
        r'\1\2 to \3\4 handler \5 to \6', transformed, flags=re.M)
    transformed, filter_regions = re.subn(
        r'^(\s*\.try )(IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+)( filter IL_[0-9A-Fa-f]+) handler +'
        r'(IL_[0-9A-Fa-f]+)-(IL_[0-9A-Fa-f]+)[ \t]*$',
        r'\1\2 to \3\4 handler \5 to \6', transformed, flags=re.M)
    if count or bad_method_tokens or bad_variance or missing_resources or label_edits or catch_regions or filter_regions:
        il.write_text(transformed, encoding='utf-8')
    return {'opaque_invalid_token_calls': count, 'method_token_discriminators': bad_method_tokens,
            'generic_variance_constraints': bad_variance, 'embedded_resources': len(missing_resources),
            'catch_regions': catch_regions, 'filter_regions': filter_regions}


def run(command, cwd, log, timeout=120):
    result = subprocess.run([str(x) for x in command], cwd=cwd, capture_output=True, timeout=timeout)
    log.write_bytes(result.stdout + result.stderr)
    if result.returncode:
        raise RuntimeError('Failed: ' + str(log))
    return result.stdout


def prepare():
    TOOL.mkdir(parents=True, exist_ok=True)
    for name in ['ICSharpCode.Decompiler.dll', 'Mono.Cecil.dll']:
        shutil.copyfile(DEPENDENCIES / name, TOOL / name)
    executable = TOOL / 'ManagedIlWorkspace.dll'
    refs = [p for p in RUNTIME.glob('*.dll') if managed(p)] + list(TOOL.glob('ICSharpCode.Decompiler.dll')) + list(TOOL.glob('Mono.Cecil.dll'))
    run([DOTNET, CSC, '-nologo', '-target:exe', '-out:' + str(executable)] +
        ['-r:' + str(p) for p in refs] + [ROOT / 'tools/ManagedIlWorkspace.cs'], TOOL, TOOL / 'compile.log')
    executable.with_suffix('.runtimeconfig.json').write_text(json.dumps({'runtimeOptions': {'tfm': 'net10.0',
        'framework': {'name': 'Microsoft.NETCore.App', 'version': '10.0.12'}}}), encoding='utf-8')
    return executable


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('name', help='Exact snapshotted DLL/EXE name')
    parser.add_argument('--workspace', type=Path, default=ROOT, help='Use a separate installed-build snapshot')
    parser.add_argument('--recover', action='store_true')
    args = parser.parse_args()
    workspace = args.workspace.resolve()
    original = workspace / 'baseline' / args.name
    if original.name != args.name or not original.exists():
        raise SystemExit('Choose an exact baseline artifact name.')
    inventory = json.loads((workspace / 'artifact-inventory.json').read_bytes())
    inputs = {row['name']: row['sha256'] for row in inventory['records']}
    if inputs.get(args.name) != sha(original):
        raise SystemExit('Baseline artifact differs from the pinned snapshot inventory.')
    project = workspace / 'projects-il' / original.stem
    source = project / 'source'
    logs = project / 'logs'
    binary = project / 'bin'
    for folder in (source, logs, binary):
        folder.mkdir(parents=True, exist_ok=True)
    il = source / (original.stem + '.il')
    if args.recover:
        if il.exists():
            raise SystemExit('Editable IL exists; recovery will not overwrite it.')
        helper = prepare()
        run([DOTNET, helper, original, source], source, logs / 'disassemble.log')
        il_repairs = make_opaque_token_calls_assemblable(il)
        (project / 'recovery-manifest.json').write_text(json.dumps({'original_sha256': sha(original),
            'original_path': str(original), 'il_sha256': sha(il), 'target_executed': False,
            'il_assembler_syntax_repairs': il_repairs,
            'disassembler_sha256': sha(TOOL / 'ICSharpCode.Decompiler.dll'),
            'helper_source_sha256': sha(ROOT / 'tools/ManagedIlWorkspace.cs')}, indent=2), encoding='utf-8')
    if not il.exists():
        raise SystemExit('Run --recover once to create editable IL.')
    output = binary / args.name
    run([ILASM, il, '/quiet', '/nologo', '/dll' if original.suffix.lower() == '.dll' else '/exe',
         '/output:' + str(output)], source, logs / 'ilasm.log')
    verification_file = ROOT / 'projects/DigisatServer.Domain.Core/il-verification.json'
    verification = None
    if verification_file.exists():
        observed = json.loads(verification_file.read_text(encoding='utf-8'))
        if observed.get('rebuilt_sha256') == sha(output):
            verification = {'evidence_file': str(verification_file.relative_to(ROOT)),
                             'comparison': observed.get('comparison'),
                             'selected_behavior_assertions': 13,
                             'selected_behavior_match': observed.get('comparison', {}).get('selected_checks_equal')}
    (project / 'build-manifest.json').write_text(json.dumps({'input_sha256': sha(original),
        'source_il_sha256': sha(il), 'output_sha256': sha(output), 'assembler_sha256': sha(ILASM),
        'build_language': 'CIL textual source', 'target_executed': False,
        'verification': verification,
        'limitations': ['Single-module assembly only.',
                        'Assembly layout and metadata can differ. Check compatibility against the input assembly.']}, indent=2), encoding='utf-8')
    print('Assembled IL source: ' + args.name)


if __name__ == '__main__':
    main()

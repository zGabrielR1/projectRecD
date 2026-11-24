"""Sanitize obfuscator/compiler-generated identifiers that are illegal C#.

The exported ILSpy sources keep CLR names such as `<>c__DisplayClass1_0`,
`CS$<>8__locals1`, `<>t__builder`, `<>h__TransparentIdentifier0` and
`<PrivateImplementationDetails>`. None of them are legal C# identifiers, so the
project cannot compile even though the rest of the decompiled text is valid.

Renaming is purely lexical and deterministic: the same generated name maps to
one replacement inside a project, and string literals, char literals and
comments are never touched. `<PrivateImplementationDetails>` dispatches are
rewritten to a synthesized FNV-1a helper, which is the exact algorithm Roslyn
emits for string switches (checked against the numeric case labels already
present in the decompiled source).

Usage:
    python repair_generated_identifiers.py [--only NAME] [--dry-run]
"""
from __future__ import annotations

import argparse
import hashlib
import json
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
AUTO = ROOT / "projects-auto"
HELPER_NAME = "__PrivateImplementationDetails"
HELPER_FILE = "__PrivateImplementationDetails.cs"
HELPER_SOURCE = """// Synthesized during reconstruction: Roslyn's generated string hash helper.
internal static class __PrivateImplementationDetails
{
\tinternal static uint ComputeStringHash(string s)
\t{
\t\tuint num = 0u;
\t\tif (s != null)
\t\t{
\t\t\tnum = 2166136261u;
\t\t\tfor (int i = 0; i < s.Length; i++)
\t\t\t{
\t\t\t\tnum = ((uint)s[i] ^ num) * 16777619u;
\t\t\t}
\t\t}
\t\treturn num;
\t}
}
"""

REPLACEMENTS = [
    (re.compile(r"CS\$<>(\d+)__locals(\d*)"), r"CS_\1_locals\2"),
    (re.compile(r"<>c__DisplayClass(\d+)_(\d+)"), r"__c_DisplayClass\1_\2"),
    (re.compile(r"<>c__DisplayClass(\d+)"), r"__c_DisplayClass\1"),
    (re.compile(r"<>f__AnonymousType(\d+)"), r"__f_AnonymousType\1"),
    (re.compile(r"<>h__TransparentIdentifier(\d+)"), r"__h_TransparentIdentifier\1"),
    (re.compile(r"<>t__builder"), r"__t__builder"),
    (re.compile(r"<>7__wrap(\d+)"), r"__7__wrap\1"),
    (re.compile(r"<>u__(\d+)"), r"__u__\1"),
    (re.compile(r"<>(\d+)__(\w*)"), r"__\1__\2"),
    (re.compile(r"<>c\b"), r"__c"),
    (re.compile(r"<PrivateImplementationDetails>"), r"global::" + HELPER_NAME),
    (re.compile(r"<Module>\{[0-9A-Fa-f-]+\}"), r"__Module"),
    (re.compile(r"<Module>"), r"__Module"),
]

GENERATED_TOKEN = re.compile(r"<[A-Za-z0-9_$]*>?[A-Za-z0-9_$]*|<>[A-Za-z0-9_$]*")
INVALID_RESIDUE = re.compile(r"(?<![A-Za-z0-9_])[A-Za-z_][A-Za-z0-9_]*[<>$][A-Za-z0-9_<>$]*")


def code_segments(text: str):
    """Split source into (text, is_code) runs, skipping comments and literals."""
    segments = []
    start = 0
    i = 0
    length = len(text)
    while i < length:
        ch = text[i]
        if ch == "/" and i + 1 < length and text[i + 1] == "/":
            end = text.find("\n", i)
            end = length if end < 0 else end
            segments.append((text[start:i], True))
            segments.append((text[i:end], False))
            start = i = end
            continue
        if ch == "/" and i + 1 < length and text[i + 1] == "*":
            end = text.find("*/", i + 2)
            end = length if end < 0 else end + 2
            segments.append((text[start:i], True))
            segments.append((text[i:end], False))
            start = i = end
            continue
        if ch in "@$" and i + 1 < length and text[i + 1] == '"' or ch == '"':
            # Verbatim/interpolated prefixes are included in the literal run.
            j = i
            verbatim = False
            while j < length and text[j] in "@$":
                verbatim = verbatim or text[j] == "@"
                j += 1
            if j >= length or text[j] != '"':
                i += 1
                continue
            j += 1
            while j < length:
                if verbatim and text[j] == '"':
                    if j + 1 < length and text[j + 1] == '"':
                        j += 2
                        continue
                    j += 1
                    break
                if not verbatim and text[j] == "\\":
                    j += 2
                    continue
                if not verbatim and text[j] == '"':
                    j += 1
                    break
                j += 1
            segments.append((text[start:i], True))
            segments.append((text[i:j], False))
            start = i = j
            continue
        if ch == "'":
            j = i + 1
            while j < length:
                if text[j] == "\\":
                    j += 2
                    continue
                if text[j] == "'":
                    j += 1
                    break
                j += 1
            segments.append((text[start:i], True))
            segments.append((text[i:j], False))
            start = i = j
            continue
        i += 1
    segments.append((text[start:], True))
    return segments


def sanitize_code(code: str) -> tuple[str, int, list[str]]:
    total = 0
    for pattern, replacement in REPLACEMENTS:
        code, count = pattern.subn(replacement, code)
        total += count
    residue = sorted(
        m.group(0)
        for m in INVALID_RESIDUE.finditer(code)
        if "$" in m.group(0) or "<>" in m.group(0) or m.group(0).startswith("<")
    )
    return code, total, residue


def repair_file(path: Path, dry_run: bool = False) -> tuple[int, list[str]]:
    text = path.read_text(encoding="utf-8")
    total = 0
    residue = []
    pieces = []
    for part, is_code in code_segments(text):
        if is_code:
            part, count, leftover = sanitize_code(part)
            total += count
            residue.extend(leftover)
        pieces.append(part)
    if total and not dry_run:
        path.write_text("".join(pieces), encoding="utf-8")
    return total, sorted(set(residue))


def failing_projects() -> list[str]:
    data = json.loads((ROOT / "failure-analysis.json").read_text(encoding="utf-8"))
    return [x["project"] for x in data["projects"]]


CS0165 = re.compile(
    r"^(.+?\.cs)\((\d+),(\d+)\): error CS0165: Use of unassigned local variable '(\w+)'", re.M
)


def fix_unassigned_locals(project: str, dry_run: bool = False) -> int:
    """Initialise generated locals that the constructor-less C# view reads first.

    Obfuscated cache delegates are emitted as locals without an initialiser;
    the original IL relied on `.locals init`, which zero-initialises them.
    """
    folder = AUTO / project
    log = folder / "build" / "compile.log"
    if not log.exists():
        return 0
    pending = {}
    for match in CS0165.finditer(log.read_text(encoding="utf-8", errors="replace")):
        path = Path(match.group(1).replace("\\", "/"))
        if not path.is_absolute():
            path = ROOT.parents[1] / match.group(1)
        pending.setdefault((path, match.group(4)), set()).add(int(match.group(2)))
    fixed = 0
    for (path, name), uses in pending.items():
        if not path.exists() or not name.startswith(("__", "CS_")):
            continue
        lines = path.read_text(encoding="utf-8").splitlines(keepends=True)
        declaration = re.compile(r"^(\s*[^=;{}]+?)\s+" + re.escape(name) + r"\s*;(\s*)$")
        edited = set()
        for use_line in sorted(uses):
            for index in range(min(use_line, len(lines)) - 1, -1, -1):
                if index in edited:
                    break
                match = declaration.match(lines[index].rstrip("\r\n"))
                if match:
                    ending = "\r\n" if lines[index].endswith("\r\n") else "\n"
                    lines[index] = f"{match.group(1)} {name} = default;{ending}"
                    edited.add(index)
                    fixed += 1
                    break
        if edited and not dry_run:
            path.write_text("".join(lines), encoding="utf-8")
    return fixed


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--only", action="append", help="Exact project name; may repeat")
    parser.add_argument("--dry-run", action="store_true")
    parser.add_argument(
        "--fix-unassigned",
        action="store_true",
        help="Initialise generated locals reported as CS0165 in the last compile log",
    )
    args = parser.parse_args()
    if args.fix_unassigned:
        total = 0
        for name in args.only or failing_projects():
            fixed = fix_unassigned_locals(name, args.dry_run)
            total += fixed
            if fixed:
                print(f"{name}: {fixed} generated locals initialised")
        print(f"total initialised: {total}")
        return
    projects = args.only or failing_projects()
    grand = 0
    for name in projects:
        folder = AUTO / name
        if not folder.exists():
            print(name + ": missing")
            continue
        recovery_file = folder / "recovery.json"
        recovery = json.loads(recovery_file.read_text(encoding="utf-8"))
        sources = {record["path"]: record for record in recovery["sources"]}
        changed = 0
        residue = []
        helper_used = False
        for path in sorted((folder / "source").rglob("*.cs")):
            count, leftover = repair_file(path, args.dry_run)
            changed += count
            residue.extend(leftover)
        helper_path = folder / "source" / HELPER_FILE
        helper_used = helper_path.exists() or any(
            HELPER_NAME in p.read_text(encoding="utf-8") for p in (folder / "source").rglob("*.cs")
        )
        if helper_used and not args.dry_run:
            helper_path.write_text(HELPER_SOURCE, encoding="utf-8")
            if HELPER_FILE not in sources:
                recovery["sources"].append(
                    {
                        "path": HELPER_FILE,
                        "export_path": HELPER_FILE,
                        "raw_sha256": hashlib.sha256(HELPER_SOURCE.encode("utf-8")).hexdigest(),
                        "generated": True,
                    }
                )
        if not args.dry_run and (changed or helper_used):
            recordings = {r.get("kind") for r in recovery["repairs"]}
            if changed and "generated identifier sanitization" not in recordings:
                recovery["repairs"].append(
                    {"kind": "generated identifier sanitization", "occurrences": changed}
                )
            if helper_used and "synthesized ComputeStringHash helper" not in recordings:
                recovery["repairs"].append(
                    {"kind": "synthesized ComputeStringHash helper", "occurrences": 1}
                )
            recovery_file.write_text(json.dumps(recovery, indent=2) + "\n", encoding="utf-8")
        grand += changed
        print(f"{name}: {changed} renames, {len(set(residue))} residue tokens" + (" (dry run)" if args.dry_run else ""))
        for token in sorted(set(residue))[:10]:
            print("   residue: " + repr(token))
    print(f"total renames: {grand}")


if __name__ == "__main__":
    main()

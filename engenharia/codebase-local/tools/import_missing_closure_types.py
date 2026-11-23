"""Synthesize compiler-generated closure classes omitted from the ILSpy project export.

ILSpy's project export drops nested compiler-generated types (display classes),
yet the decompiled method bodies still instantiate them and read their fields.
This tool reads the original binding assembly with Mono.Cecil, recovers the
field names and field types of each missing `<>c__DisplayClass...` type, and
inserts a metadata-derived declaration into the declaring type's source file.

The declarations hold only data, exactly as the compiler generated them; method
bodies are not synthesized because the decompiled source already inlines the
lambda bodies at their call sites.

Usage:
    python import_missing_closure_types.py [--only NAME]
"""
from __future__ import annotations

import argparse
import json
import re
import subprocess
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
AUTO = ROOT / "projects-auto"
DOTNET = Path("C:/Program Files/dotnet/dotnet.exe")
DUMPER = AUTO / "tooling" / "NestedClosureDump.dll"

sys.path.insert(0, str(Path(__file__).resolve().parent))
from repair_generated_identifiers import code_segments, sanitize_code  # noqa: E402


def code_flags(text: str) -> list[bool]:
    flags = [False] * len(text)
    offset = 0
    for part, is_code in code_segments(text):
        if is_code:
            for index in range(offset, offset + len(part)):
                flags[index] = True
        offset += len(part)
    return flags


def insert_before_class_close(text: str, class_name: str, declaration: str) -> str | None:
    match = re.search(r"\bclass\s+" + re.escape(class_name) + r"\b", text)
    while match:
        flags = code_flags(text)
        opening = text.find("{", match.end())
        if opening < 0:
            return None
        depth = 0
        for index in range(opening, len(text)):
            if not flags[index]:
                continue
            if text[index] == "{":
                depth += 1
            elif text[index] == "}":
                depth -= 1
                if depth == 0:
                    line_start = text.rfind("\n", 0, index) + 1
                    return text[:line_start] + declaration + text[line_start:]
        match = re.compile(r"\bclass\s+" + re.escape(class_name) + r"\b").search(text, match.end())
    return None


def dump_types(baseline: Path) -> list[dict]:
    env = {"DOTNET_ROLL_FORWARD": "Major"}
    result = subprocess.run(
        [str(DOTNET), str(DUMPER), str(baseline)], capture_output=True, text=True, env=env
    )
    if result.returncode:
        raise RuntimeError("NestedClosureDump failed: " + result.stderr.strip())
    return json.loads(result.stdout)["types"]


def field_type(type_name: str) -> str:
    if type_name.startswith("System.") and "<" not in type_name:
        return {
            "System.Object": "object",
            "System.String": "string",
        }.get(type_name, type_name)
    return type_name


KEYWORDS = {
    "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
    "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
    "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
    "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new",
    "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
    "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static",
    "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
    "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while",
}

MARKER = "// Synthesized compiler-generated closure type (metadata-derived)."


def escape(name: str) -> str:
    return "@" + name if name in KEYWORDS else name


def build_declaration(record: dict, indent: str) -> str:
    name, _ = sanitize_code(record["name"])[0:2]
    lines = [f"\n{indent}{MARKER}\n"]
    lines.append(f"{indent}private sealed class {name}\n{indent}{{\n")
    for field in record["fields"]:
        if field["isStatic"]:
            continue
        field_name, _ = sanitize_code(field["name"])[0:2]
        field_type_name, _ = sanitize_code(field["type"])[0:2]
        lines.append(
            f"{indent}\tprivate {field_type(field_type_name)} {escape(field_name)};\n"
        )
    lines.append(f"{indent}\tpublic {name}()\n{indent}\t{{\n{indent}\t}}\n")
    lines.append(f"{indent}}}\n")
    return "".join(lines)


def remove_synthesized(text: str) -> str:
    """Drop previously synthesized blocks so they can be regenerated."""
    while True:
        marker = text.find(MARKER)
        if marker < 0:
            return text
        start = text.rfind("\n", 0, marker)
        start = 0 if start < 0 else start
        opening = text.find("{", marker)
        flags = code_flags(text)
        depth = 0
        end = None
        for index in range(opening, len(text)):
            if not flags[index]:
                continue
            if text[index] == "{":
                depth += 1
            elif text[index] == "}":
                depth -= 1
                if depth == 0:
                    end = index + 1
                    break
        if end is None:
            return text
        while end < len(text) and text[end] in "\r\n":
            end += 1
        text = text[:start] + text[end:]


def find_declaring_file(folder: Path, declaring: str):
    simple = declaring.split(".")[-1]
    matches = []
    for path in (folder / "source").rglob("*.cs"):
        text = path.read_text(encoding="utf-8")
        if re.search(r"\bclass\s+" + re.escape(simple) + r"\b", text):
            matches.append((path, text))
    return matches


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--only", action="append", help="Exact project name; may repeat")
    parser.add_argument("--dry-run", action="store_true")
    args = parser.parse_args()
    data = json.loads((ROOT / "failure-analysis.json").read_text(encoding="utf-8"))
    projects = args.only or [x["project"] for x in data["projects"]]
    grand = 0
    for name in projects:
        folder = AUTO / name
        recovery = json.loads((folder / "recovery.json").read_text(encoding="utf-8"))
        baseline = ROOT / recovery["baseline"]
        if not baseline.exists():
            print(f"{name}: missing baseline")
            continue
        records = [x for x in dump_types(baseline) if "DisplayClass" in x["name"]]
        if not args.dry_run:
            for path in (folder / "source").rglob("*.cs"):
                original = path.read_text(encoding="utf-8")
                cleaned = remove_synthesized(original)
                if cleaned != original:
                    path.write_text(cleaned, encoding="utf-8")
        source_text = "".join(
            path.read_text(encoding="utf-8") for path in (folder / "source").rglob("*.cs")
        )
        synthesized = []
        for record in records:
            class_name = sanitize_code(record["name"])[0]
            if re.search(r"\bclass\s+" + re.escape(class_name) + r"\b", source_text):
                continue
            if record["isStatic"]:
                continue
            declaring = record["declaring"]
            files = find_declaring_file(folder, declaring)
            if len(files) != 1:
                files = [item for item in files if re.search(
                    r"\bclass\s+" + re.escape(declaring.split(".")[-1]) + r"\b", item[1])]
            if len(files) != 1:
                print(f"{name}: cannot place {record['name']} (declaring {declaring})")
                continue
            path, text = files[0]
            class_match = re.search(
                r"^([ \t]*)public class " + re.escape(declaring.split(".")[-1]) + r"\b",
                text,
                re.M,
            )
            indent = (class_match.group(1) if class_match else "") + "\t"
            declaration = build_declaration(record, indent)
            updated = insert_before_class_close(text, declaring.split(".")[-1], declaration)
            if updated is None:
                print(f"{name}: brace match failed for {record['name']}")
                continue
            if not args.dry_run:
                path.write_text(updated, encoding="utf-8")
            source_text = updated
            synthesized.append(sanitize_code(record["name"])[0])
        grand += len(synthesized)
        if synthesized:
            kinds = {r.get("kind") for r in recovery["repairs"]}
            if "synthesized closure type declarations" not in kinds and not args.dry_run:
                recovery["repairs"].append(
                    {
                        "kind": "synthesized closure type declarations",
                        "occurrences": len(synthesized),
                        "types": synthesized,
                    }
                )
                (folder / "recovery.json").write_text(
                    json.dumps(recovery, indent=2) + "\n", encoding="utf-8"
                )
            print(f"{name}: {len(synthesized)} closure types synthesized")
            for item in synthesized[:6]:
                print("   " + item)
    print(f"total synthesized: {grand}")


if __name__ == "__main__":
    main()

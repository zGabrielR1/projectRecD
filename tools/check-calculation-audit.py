"""Cross-check the Python audit with a separately compiled serial C# reference.

Run with the Insight dependencies installed:
python tools/check-calculation-audit.py /tmp/digisat-monthly
"""
from pathlib import Path
from decimal import Decimal
import hashlib
import json
import random
import subprocess
import sys

root = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(root))
from insight_calculation_audit import MonthlyAudit, monthly_audit

tools = Path(sys.argv[1]).resolve()
out = root / 'engenharia/auditoria-calculos-2026-10-30'
build = out / 'build'
build.mkdir(parents=True, exist_ok=True)
framework = tools / 'runtime/shared/Microsoft.NETCore.App'
runtime = max((p for p in framework.iterdir() if p.name.startswith('8.0.')),
              key=lambda p: tuple(map(int, p.name.split('.'))))
source = out / 'MonthlySnapshotOracle.cs'
executable = build / 'MonthlySnapshotOracle.dll'
subprocess.run([str(tools / 'runtime/dotnet'), str(tools / 'compiler/tasks/netcore/bincore/csc.dll'),
                '-nologo', '-target:exe', '-out:' + str(executable)] +
               ['-r:' + str(p) for p in sorted(runtime.glob('*.dll'))] + [str(source)], check=True)
executable.with_suffix('.runtimeconfig.json').write_text(json.dumps({'runtimeOptions': {
    'tfm': 'net8.0', 'framework': {'name': 'Microsoft.NETCore.App', 'version': runtime.name}}}))
rng = random.Random(26300)
docs = []
for index in range(1201):
    docs.append({'document_id': str(index), 'month': index % 12 + 1, 'items': [
        {'seller_id': rng.choice(['A', 'B', '']),
         'total_liquido': str(Decimal(rng.randrange(-10000, 10000)) / 100),
         'cancelled': rng.choice([False, False, True]),
         'auxiliary_document': rng.choice([True, False]),
         'moves_stock': rng.choice([True, False]),
         'moves_finance': rng.choice([True, False])} for _ in range(7)]})
cases = []
for seller in [None, 'A', 'B', 'missing']:
    payload = {'year': 2026, 'company_reference': 'synthetic-only',
               'repository_scope_description': 'Deterministic normalized fixture, not customer records',
               'documents': docs, 'seller_id': seller}
    fixture = build / 'snapshot.json'
    fixture.write_text(json.dumps(payload))
    expected = subprocess.run([str(tools / 'runtime/dotnet'), str(executable), str(fixture)],
                              check=True, text=True, capture_output=True)
    csharp = {row['seller_id'].casefold(): [Decimal(x) for x in row['months']] for row in json.loads(expected.stdout)}
    actual = monthly_audit(MonthlyAudit.model_validate(payload))
    python = {row['seller_id'].casefold(): [Decimal(x) for x in row['months']] for row in actual['rows']}
    if python != csharp:
        raise AssertionError('Python and C# audit results differ')
    cases.append({'seller': seller, 'documents': len(docs), 'items': sum(len(d['items']) for d in docs),
                  'seller_month_cells': len(python) * 12, 'matches': True,
                  'snapshot_sha256': actual['provenance']['snapshot_sha256']})
result = {'oracle_source_sha256': hashlib.sha256(source.read_bytes()).hexdigest(),
          'runtime': runtime.name, 'cases': cases,
          'scope': 'Normalized input/filter/serial decimal grouping equivalence; not raw BSON, repository, WCF or full production execution'}
(out / 'cross-language-results.json').write_text(json.dumps(result, indent=2) + '\n')
print('All four Python/C# comparisons passed (1,201 documents, 8,407 items per case).')

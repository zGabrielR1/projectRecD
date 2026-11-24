"""Build editable verified components offline; never regenerate their sources."""
import argparse
import json
import subprocess
import sys
from datetime import datetime, timezone
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
JOBS = {
    'data-core': ('build_data_core.py', ['--compile-only', '--test']),
    'reports': ('build_reports.py', ['--compile-only', '--source-linked']),
    'domain-core': ('build_domain_core.py', ['--compile-only']),
}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--project', choices=['verified'] + list(JOBS), default='verified')
    args = parser.parse_args()
    selected = ['data-core', 'reports'] if args.project == 'verified' else [args.project]
    results = []
    for name in selected:
        script, flags = JOBS[name]
        started = datetime.now(timezone.utc).isoformat()
        print('Building ' + name + ' from local editable source...', flush=True)
        run = subprocess.run([sys.executable, str(ROOT / 'tools' / script)] + flags, cwd=ROOT, timeout=300)
        results.append({'project': name, 'started_utc': started,
                        'finished_utc': datetime.now(timezone.utc).isoformat(), 'exit_code': run.returncode})
        if run.returncode:
            break
    (ROOT / 'workspace-build.json').write_text(json.dumps({'source_regenerated': False,
        'target_executed': 'Selected rebuilt synthetic behavior checks only; no vendor app launched',
        'results': results}, indent=2), encoding='utf-8')
    if any(r['exit_code'] for r in results):
        raise SystemExit('A selected build failed; source and logs are preserved.')


if __name__ == '__main__':
    main()

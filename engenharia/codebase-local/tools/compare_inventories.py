"""Compare selected assemblies and installed variants across recovery snapshots."""
import argparse
import json
from collections import Counter
from pathlib import Path


def compare(previous, current):
    old = {record['name']: record for record in previous['records']}
    new = {record['name']: record for record in current['records']}
    rows = []
    for name in sorted(old.keys() | new.keys()):
        before, after = old.get(name), new.get(name)
        before_hash = before['sha256'] if before else None
        after_hash = after['sha256'] if after else None
        if before is None:
            status = 'added'
        elif after is None:
            status = 'removed'
        elif before_hash != after_hash:
            status = 'changed'
        else:
            status = 'unchanged'
        rows.append({
            'name': name, 'status': status,
            'previous': before, 'current': after,
            'variant_hashes_changed': bool(before and after and
                set(before['variant_hashes']) != set(after['variant_hashes'])),
        })
    return {
        'counts': dict(Counter(row['status'] for row in rows)),
        'infrastructure_counts': dict(Counter(row['status'] for row in rows
            if row['name'].startswith('DigisatServer.Infrastructure.'))),
        'modules': rows,
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('previous', type=Path)
    parser.add_argument('current', type=Path)
    parser.add_argument('-o', '--output', type=Path)
    args = parser.parse_args()
    result = compare(json.loads(args.previous.read_text(encoding='utf-8')),
                     json.loads(args.current.read_text(encoding='utf-8')))
    if args.output:
        args.output.write_text(json.dumps(result, indent=2) + '\n', encoding='utf-8')
    print(json.dumps({key: result[key] for key in ('counts', 'infrastructure_counts')}))
    for row in result['modules']:
        if row['name'].startswith('DigisatServer.Infrastructure.') and (
                row['status'] != 'unchanged' or row['variant_hashes_changed']):
            print(f"{row['status']}: {row['name']}" +
                  (' (variants changed)' if row['variant_hashes_changed'] else ''))


if __name__ == '__main__':
    main()

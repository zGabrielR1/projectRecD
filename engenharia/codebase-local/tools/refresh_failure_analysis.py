"""Refresh failure evidence from actual per-project compiler results and logs."""
from pathlib import Path
import json,re,collections
root=Path(__file__).resolve().parents[1]
p=root/'failure-analysis.json';old=json.loads(p.read_text());census=json.loads((root/'projects-auto/compilation-census.json').read_text());prior={x['project']:x for x in old['projects']};projects=[]
for module in census['modules']:
 if module['compiled']:continue
 name=module['name'];folder=root/'projects-auto'/name;log=folder/'build/compile.log';s=log.read_text(encoding='utf-8',errors='replace');es=re.findall(r'^(.+?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*)$',s,re.M);examples=[]
 for file,line,col,code,msg in es:
  if code not in [e['code'] for e in examples]:examples.append(dict(code=code,file=file,line=int(line),column=int(col),message=msg))
 family=prior.get(name,{}).get('family','source reconstruction')
 if (folder/'recovery-original-export.json').exists():family='source reconstruction after fresh whole-module export'
 projects.append(dict(project=name,family=family,diagnostic_count=len(es),error_codes=dict(collections.Counter(e[3] for e in es)),examples=examples,log=str(log.relative_to(root.parent.parent))))
data=dict(compiled=census['compiled'],attempted=census['attempted'],remaining=len(projects),classification='Diagnostic families; syntax errors may cascade. Obfuscation attribution requires original method evidence.',families=dict(collections.Counter(x['family'] for x in projects)),projects=projects);p.write_text(json.dumps(data,indent=2),encoding='utf-8')
lines=['# Remaining exported-project compilation failures','',f"{data['compiled']} of {data['attempted']} C# projects compile; {data['remaining']} remain. Domain.Core additionally builds from editable CIL.",'',data['classification'],'','| Diagnostic family | Projects |','|---|---:|'];lines += [f'| {k} | {v} |' for k,v in data['families'].items()]
for x in projects:
 lines+=['','## '+x['project'],'',x['family']+'; '+str(x['diagnostic_count'])+' diagnostics.','']
 for e in x['examples'][:5]:lines += [f"- `{e['code']}` `{e['file']}:{e['line']}` — {e['message']}"]
(root/'FAILURES.md').write_text('\n'.join(lines)+'\n',encoding='utf-8');doc=root.parent.parent/'docs/recreated-codebase-2026-11-11.md';doc.write_text(re.sub(r'Roslyn compiled \d+/218',f"Roslyn compiled {data['compiled']}/218",doc.read_text(encoding='utf-8')),encoding='utf-8');print(f"{data['compiled']} compiled; {data['remaining']} remaining.")

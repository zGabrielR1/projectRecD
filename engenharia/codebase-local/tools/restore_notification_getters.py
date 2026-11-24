"""Restore missing notification getter expressions using preserved first-export operands."""
from pathlib import Path
import argparse,json,re
ROOT=Path(__file__).resolve().parents[1]
def main():
 parser=argparse.ArgumentParser();parser.add_argument('project');args=parser.parse_args();p=ROOT/'projects-auto'/args.project;f=p/'source/_alternative/WholeModule.cs';s=f.read_text(encoding='utf-8');pat=re.compile(r'System\.Linq\.Expressions\.Expression\.Lambda<Func<([^>]+)>>\(System\.Linq\.Expressions\.Expression\.Property\(System\.Linq\.Expressions\.Expression\.Constant\(this, typeof\((\w+)\)\), \(MethodInfo\)MethodBase\.GetMethodFromHandle\(\(RuntimeMethodHandle\)/\*OpCode not supported: LdMemberToken\*/\)\), Array\.Empty<ParameterExpression>\(\)\)');lines=s.splitlines(keepends=True);records=[]
 for i,line in enumerate(lines):
  if 'LdMemberToken' not in line:continue
  matches=list(pat.finditer(line));assert matches,'Unsupported placeholder pattern';key=re.search(r'RaisePropertiesChanged<(.+?)>\(',line)[1];first=re.search(r'\(\) => (?:\(\(Validation\)this\)\.)?(\w+)',line)[1];cls=matches[0][2];raw=next(x for x in (p/'source').rglob(cls+'.cs') if '_alternative' not in str(x));candidates=[]
  for old in raw.read_text(encoding='utf-8').splitlines():
   if 'RaisePropertiesChanged<'+key+'>(' not in old:continue
   getters=re.findall(r'methodof\([^\n]*?\.get_(\w+)\(\)\)',old)
   if getters and getters[0]==first:candidates.append(getters)
  assert candidates and all(x==candidates[0] for x in candidates),'Ambiguous original notification sequence';getters=candidates[0];starts=[m.start() for m in re.finditer(r'(?:Expression|Lambda)<Func<',line)];edits=[]
  for m in matches:
   ordinal=sum(pos<m.start() for pos in starts);prop=getters[ordinal];edits.append((m.start(),m.end(),'(Expression<Func<'+m[1]+'>>)(() => '+prop+')'));records.append({'class':cls,'property':prop,'type':m[1],'evidence':str(raw.relative_to(p))})
  for a,b,t in reversed(edits):line=line[:a]+t+line[b:]
  lines[i]=line
 output=''.join(lines);assert 'LdMemberToken' not in output;f.write_text(output,encoding='utf-8');manifest=p/'recovery.json';data=json.loads(manifest.read_text());data['repairs'].append({'kind':'Restore missing notification member tokens from uniquely matched original getter sequences','getters':records});manifest.write_text(json.dumps(data,indent=2),encoding='utf-8');print('Restored',len(records),'getter expressions')
if __name__=='__main__':main()

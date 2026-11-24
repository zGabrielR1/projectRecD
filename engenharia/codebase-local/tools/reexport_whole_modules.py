"""Recover an alternative whole-module C# export without overwriting the first export."""
import argparse,hashlib,json,os,pathlib,re,shutil,subprocess,sys
ROOT=pathlib.Path(__file__).resolve().parents[1]
def main():
 parser=argparse.ArgumentParser();parser.add_argument('names',nargs='+');args=parser.parse_args();inventory=json.loads((ROOT/'artifact-inventory.json').read_text());env=os.environ.copy();env['DOTNET_ROLL_FORWARD']='Major'
 for name in dict.fromkeys(args.names):
  prior=ROOT/'projects-auto'/name/'build/result.json'
  if prior.exists() and json.loads(prior.read_text()).get('compiled'):
   print(name+': already compiled; retaining current source',flush=True);continue
  project=ROOT/'projects-auto'/name;manifest=project/'recovery.json';m=json.loads(manifest.read_text());original=next(x for x in inventory['records'] if x['name']==name+'.dll');target=pathlib.Path(original['path']);assert hashlib.sha256(target.read_bytes()).hexdigest()==m['baseline_sha256']
  backup=project/'recovery-original-export.json';raw=project/'source-ilspy/WholeModule.cs';editable=project/'source/_alternative/WholeModule.cs'
  if not backup.exists():
   raw.parent.mkdir(exist_ok=True);editable.parent.mkdir(exist_ok=True)
   subprocess.run(['C:/Program Files/dotnet/dotnet.exe',str(ROOT.parent/'recompilacao-data-core-2026-10-30/build/WholeModuleDecompiler.dll'),str(target),str(raw)],env=env,check=True,capture_output=True,timeout=120)
   shutil.copyfile(manifest,backup);shutil.copyfile(raw,editable);m['original_export_sources']=m['sources'];m['sources']=[{'path':'_alternative/WholeModule.cs','export_path':'alternative whole-module ILSpy decompilation','raw_sha256':hashlib.sha256(raw.read_bytes()).hexdigest()}];m['repairs'].append({'kind':'Fresh whole-module ILSpy export; original source retained','input_sha256':m['baseline_sha256']});manifest.write_text(json.dumps(m,indent=2),encoding='utf-8')
  for attempt in range(3):
   subprocess.run([sys.executable,str(ROOT/'tools/build_recovered_modules.py'),'--only',name,'--workers','1'],check=True)
   result=json.loads((project/'build/result.json').read_text())
   if result['compiled']:break
   log=(project/'build/compile.log').read_text(errors='replace');s=editable.read_text(encoding='utf-8');changes=0
   for typ in set(re.findall(r"via a qualifier of type '([^']+)'",log)):
    for cast in ['(('+typ+')this).','(('+typ+')(object)this).']:
     changes+=s.count(cast);s=s.replace(cast,'base.')
   if not changes:break
   editable.write_text(s,encoding='utf-8');m['repairs'].append({'kind':'Replace illegal explicit base-type this qualifiers with base member access','occurrences':changes});manifest.write_text(json.dumps(m,indent=2),encoding='utf-8')
if __name__=='__main__':main()

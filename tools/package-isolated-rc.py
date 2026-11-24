"""Windows CI verification profile: portable PE + offline APIs, no live DigiSat.

The full live-database verifier/finalizer remain separate and unchanged.
"""
from pathlib import Path
import base64
import hashlib
import json
import os
import secrets
import shutil
import socket
import subprocess
import sys
import time
import urllib.error
import urllib.request
import zipfile
from datetime import datetime, timezone

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT))
from insight_version import VERSION

if os.name != 'nt':
    raise SystemExit('This profile must execute the Windows PE on Windows.')
BUILD = Path((ROOT / '.release-build/latest-build-path').read_text())
EXE = BUILD / 'dist/DigiSatInsight.exe'
OUT = ROOT / '.release-build/rc-artifacts'
OUT.mkdir(parents=True, exist_ok=True)
qa = BUILD / 'isolated-verification'
qa.mkdir(exist_ok=True)
password = secrets.token_urlsafe(24)
env = os.environ.copy()
for key in ['MONGO_USERNAME', 'MONGO_PASSWORD', 'MONGO_PASSWORD_FILE', 'PYTHONPATH', 'PYTHONHOME', 'VIRTUAL_ENV']:
    env.pop(key, None)
env.update(INSIGHT_DATA_DIR=str(qa / 'state'), INSIGHT_SCHEDULER='0', INSIGHT_HOST='127.0.0.1',
           INSIGHT_USER='rc-qa', INSIGHT_PASSWORD=password, AI_ENDPOINT='', AI_MODEL='', AI_API_KEY='',
           MONGO_URI='mongodb://127.0.0.1:1/?directConnection=true', MONGO_DATABASE='RC_OFFLINE',
           PATH=str(Path(os.environ['WINDIR']) / 'System32'))
version = subprocess.run([str(EXE), '--version'], capture_output=True, text=True, env=env, timeout=90)
assert version.returncode == 0 and VERSION in version.stdout
with socket.socket() as sock:
    sock.bind(('127.0.0.1', 0))
    port = sock.getsockname()[1]
base = 'http://127.0.0.1:' + str(port)
auth = 'Basic ' + base64.b64encode(('rc-qa:' + password).encode()).decode()


def request(path, data=None, authenticated=True, raw=False):
    headers = {'Content-Type': 'application/xml' if isinstance(data,bytes) else 'application/json'}
    if authenticated:
        headers['Authorization'] = auth
    req = urllib.request.Request(base + path, headers=headers,
                                 data=(data if isinstance(data,bytes) else json.dumps(data).encode()) if data is not None else None)
    try:
        with urllib.request.urlopen(req, timeout=20) as response:
            body = response.read()
            return response.status, body if raw else json.loads(body)
    except urllib.error.HTTPError as exc:
        return exc.code, json.loads(exc.read())


log = (qa / 'server.log').open('w')
process = subprocess.Popen([str(EXE), '--no-browser', '--port', str(port)], env=env, cwd=qa,
                           stdout=log, stderr=subprocess.STDOUT, creationflags=subprocess.CREATE_NO_WINDOW)
try:
    for _ in range(240):
        assert process.poll() is None, 'Packaged executable exited'
        try:
            if request('/', raw=True)[0] == 200:
                break
        except OSError:
            pass
        time.sleep(.25)
    else:
        raise AssertionError('Packaged server did not start')
    assert request('/api/nfce-import/jobs', authenticated=False)[0] == 401
    assert request('/api/native/catalog')[0] == 200
    for filename in ['theme.js', 'native.js', 'light.css', 'fonts/InterVariable.woff2',
                     'nfce-import.html', 'nfce-import.js', 'nfce-import.css', 'nfce-profile.example.json']:
        source = BUILD / 'static' / filename
        if not source.exists() and filename.startswith('fonts/'):
            continue  # Font filenames vary; all static files are hash-listed by staging.
        status, data = request('/static/' + filename, raw=True)
        assert status == 200 and hashlib.sha256(data).digest() == hashlib.sha256(source.read_bytes()).digest()
    status, page = request('/nfce-import', raw=True)
    assert status == 200 and b'id="keyFile"' in page
    sys.path.insert(0, str(ROOT / 'tests'))
    from test_nfce_import import fixture as nfce_fixture, profile as nfce_profile
    from bson import json_util
    import io

    large_xml=nfce_fixture().replace(b'</infNFe>',b'<infAdic><infCpl>'+b'x'*(2*1024*1024)+b'</infCpl></infAdic></infNFe>')
    status,job=request('/api/nfce-import/jobs',{'mode':'manual','profile':json_util.dumps(nfce_profile())})
    assert status==200
    job_path='/api/nfce-import/jobs/'+job['id']
    from insight_nfce import NS
    from xml.etree import ElementTree as ET
    key = ET.fromstring(large_xml).find('n:NFe/n:infNFe', NS).get('Id')[3:]
    assert request(job_path+'/keys', {'keys': [key]})[0] == 503
    assert request(job_path+'/files/large?name=synthetic-large.xml',large_xml)[0]==200
    assert request(job_path+'/files/large')[1]['stored'] is True
    assert request(job_path+'/validate',{})[0]==200
    for _ in range(400):
        status,current=request(job_path)
        if current['state'] not in {'queued','validating'}:break
        time.sleep(.05)
    assert current['state']=='ready' and current['counts']['ready']==1,current
    assert current['bytes']>1024*1024 and current['valid']
    status,stream_zip=request(job_path+'/export',raw=True)
    assert status==200
    with zipfile.ZipFile(io.BytesIO(stream_zip)) as exported:
        assert json_util.loads(exported.read('XmlMovimentacoes.json'))[0]['Xml']==large_xml.decode()
    # Hard restart: the job and BSON must survive a new bootloader/server process.
    subprocess.run(['taskkill','/PID',str(process.pid),'/T','/F'],capture_output=True)
    process.wait(timeout=20)
    process=subprocess.Popen([str(EXE),'--no-browser','--port',str(port)],env=env,cwd=qa,
        stdout=log,stderr=subprocess.STDOUT,creationflags=subprocess.CREATE_NO_WINDOW)
    for _ in range(240):
        assert process.poll() is None,'Packaged executable exited during restart'
        try:
            if request(job_path)[0]==200:break
        except OSError:pass
        time.sleep(.25)
    else:raise AssertionError('Packaged server did not restart')
    persisted=request(job_path)[1]
    assert persisted['counts']['ready']==1 and persisted['valid']
finally:
    subprocess.run(['taskkill', '/PID', str(process.pid), '/T', '/F'], capture_output=True)
    process.wait(timeout=20)
    log.close()

digest = hashlib.sha256(EXE.read_bytes()).hexdigest()
commit = subprocess.check_output(['git', 'rev-parse', 'HEAD'], cwd=ROOT, text=True).strip()
verification = {'version': VERSION, 'commit': commit, 'exe_sha256': digest,
    'validation_profile': 'windows-isolated', 'windows_pe_executed': True, 'minimal_path': True,
    'authentication': True, 'static_assets': True, 'native_catalog': True,
    'nfce_key_file_assets': True, 'nfce_key_lookup_connection_failure_reported': True,
    'nfce_stream_upload_over_one_mib': True, 'nfce_disk_backed_job': True,
    'nfce_streamed_json_export': True, 'nfce_job_restart_persistence': True,
    'live_digisat_validated': False, 'live_database_validated': False,
    'not_run': ['Installed SuiteG6/WCF invocation', 'Customer database parity', 'Live Mongo privilege/CRUD acceptance',
                'Full tools/verify-release.py live-data profile', 'Browser visual regression']}
(OUT / 'verification.json').write_text(json.dumps(verification, indent=2) + '\n')
shutil.copyfile(EXE, OUT / 'DigiSatInsight.exe')
shutil.copyfile(ROOT / 'docs/guia-1.0.md', OUT / 'LEIA-ME.md')
with (OUT / 'LEIA-ME.md').open('a', encoding='utf-8') as guide:
    guide.write('\n\n' + (ROOT / 'docs' / f'release-{VERSION}.md').read_text(encoding='utf-8'))
metadata = {'version': VERSION, 'status': 'release-candidate', 'platform': 'Windows x64',
            'python_required': False, 'built_at': datetime.now(timezone.utc).isoformat(),
            'validation_profile': 'windows-isolated', 'live_digisat_validated': False}
(OUT / 'VERSION.json').write_text(json.dumps(metadata, indent=2) + '\n')
# Explicit template independent of developer files or existing local settings.
template = ('MONGO_URI=mongodb://127.0.0.1:12220/?directConnection=true&readPreference=secondaryPreferred\n'
            'MONGO_DATABASE=DigisatServer\nMONGO_USERNAME=insight\nMONGO_AUTH_SOURCE=admin\nMONGO_PASSWORD_FILE=\n'
            'DIGISAT_MODE=live\nMAX_REPORT_ROWS=500\nINSIGHT_USER=\nINSIGHT_PASSWORD=\nINSIGHT_REQUIRE_READ_ONLY=1\n'
            'AI_ENDPOINT=\nAI_MODEL=\nAI_API_KEY=\n')
distribution = BUILD / 'isolated-distribution'
distribution.mkdir(exist_ok=True)
for filename in ['DigiSatInsight.exe', 'LEIA-ME.md', 'VERSION.json']:
    shutil.copyfile(OUT / filename, distribution / filename)
(distribution / 'config.env').write_text(template)
names = ['DigiSatInsight.exe', 'LEIA-ME.md', 'VERSION.json', 'config.env']
(distribution / 'SHA256SUMS').write_text(''.join(hashlib.sha256((distribution / n).read_bytes()).hexdigest() + '  ' + n + '\n' for n in names))
archive = OUT / ('DigiSatInsight-' + VERSION + '-win64.zip')
with zipfile.ZipFile(archive, 'w', zipfile.ZIP_DEFLATED) as package:
    for filename in names + ['SHA256SUMS']:
        package.write(distribution / filename, filename)
assets = ['DigiSatInsight.exe', 'LEIA-ME.md', 'VERSION.json', 'verification.json', archive.name]
(OUT / 'SHA256SUMS').write_text(''.join(hashlib.sha256((OUT / n).read_bytes()).hexdigest() + '  ' + n + '\n' for n in assets))
print('Verified isolated Windows RC:', VERSION, digest)

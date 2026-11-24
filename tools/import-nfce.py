"""Stream XMLs into a persistent local import job and export native JSON incrementally."""
import argparse
import json
from pathlib import Path
import os
import sys

sys.path.insert(0,str(Path(__file__).resolve().parents[1]))
import insight_nfce_jobs as jobs
from insight_nfce import export_json,NOTICE
from fastapi import HTTPException


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('folder',type=Path)
    parser.add_argument('--profile',type=Path,help='Opcional: perfil JSON; sem ele consulta a conexão ativa')
    parser.add_argument('--resume',help='Identificador de uma importação local interrompida')
    parser.add_argument('--output',type=Path,required=True,help='Nova pasta privada de saída')
    args=parser.parse_args()
    if not args.folder.is_dir():parser.error('Pasta de XMLs não encontrada.')
    if args.output.exists():parser.error('A pasta de saída já existe; escolha outra para preservar os arquivos.')
    owner='local-script'
    try:
        job=jobs.snapshot(args.resume,owner) if args.resume else jobs.create(owner,'manual' if args.profile else 'automatic',
            args.profile.read_text(encoding='utf-8-sig') if args.profile else None)
        job_id=job['id']
        print('Importação local:',job_id)
        if job['state']=='uploading':
            sent=0
            # scandir yields entries; neither XML payloads nor a complete folder
            # manifest are assembled into one request/list.
            with os.scandir(args.folder) as entries:
                for entry in entries:
                    if not entry.is_file() or not entry.name.lower().endswith('.xml'):continue
                    path=Path(entry.path);stat=entry.stat()
                    import hashlib
                    upload_id=hashlib.sha256((str(path.resolve())+'|'+str(stat.st_size)+'|'+str(stat.st_mtime_ns)).encode()).hexdigest()
                    jobs.upload_local(job_id,owner,path,upload_id=upload_id)
                    sent+=1
            if not sent:parser.error('Nenhum XML encontrado.')
        job=jobs.launch(job_id,owner,'validate',background=False)
        if job['state']!='ready' or job['counts'].get('invalid'):
            print('Resultado:',job['state'],json.dumps(job['counts']))
            parser.error((job.get('error') or 'Há arquivos bloqueados; nenhuma venda foi gravada.')+
                         ' Progresso preservado; use --resume '+job_id+' para continuar após corrigir a causa.')
        args.output.mkdir(parents=True,exist_ok=False)
        groups = {}
        for sale in jobs.iter_sales(job_id):
            from insight_nfce_mapping import sale_documents
            for collection, document in sale_documents(sale):
                groups.setdefault(collection, {})[document['_id']] = document
        count = 0
        for collection, documents in groups.items():
            with (args.output/(collection+'.json')).open('x',encoding='utf-8') as output:
                output.write('[');first=True
                for document in documents.values():
                    if not first:output.write(',\n')
                    output.write(export_json(document));first=False
                output.write(']')
            count = max(count, len(documents))
        (args.output/'LEIA-ME.txt').write_text(NOTICE+'\nDestino: '+job['database']+
            '\nOrdem: cadastros de produto (quando houver), XmlMovimentacoes, Movimentacoes, '
            'Pagamentos/Recebimentos (quando houver). Use inserção; nunca replace/upsert. '
            'Verifique chaves existentes. O script não grava MongoDB.\n'+
            'Importação local persistente: '+job_id,encoding='utf-8')
        print(str(count)+' documentos exportados por coleção. Nenhuma escrita no MongoDB.')
        return 0
    except (ValueError,OSError,HTTPException) as exc:
        parser.error(str(exc.detail) if isinstance(exc,HTTPException) else str(exc))


if __name__=='__main__':
    raise SystemExit(main())

"""Minimal DBF (dBase III/FoxPro) reader for legacy import files.

Reads records as dicts with decoded string values. No external dependency.
Used only to bring DBF rows into the same reviewed/profile-based import flow.
"""
from __future__ import annotations

from datetime import date
from decimal import Decimal, InvalidOperation
import struct


def _decode(raw: bytes) -> str:
    for enc in ('utf-8-sig', 'cp1252', 'latin-1'):
        try:
            return raw.decode(enc).strip()
        except UnicodeDecodeError:
            continue
    return raw.decode('latin-1', errors='replace').strip()


def read_dbf(data: bytes) -> list[dict]:
    if len(data) < 32:
        raise ValueError('Arquivo DBF inválido.')
    num_records, header_len, record_len = struct.unpack_from('<IHH', data, 4)
    if header_len < 32 or record_len <= 1 or header_len > len(data):
        raise ValueError('Cabeçalho DBF inválido.')
    fields = []
    offset = 32
    while offset + 32 <= header_len and data[offset] != 0x0D:
        name = data[offset:offset + 11].split(b'\x00', 1)[0].decode('ascii', errors='ignore').strip()
        ftype = chr(data[offset + 11])
        length = data[offset + 16]
        decimals = data[offset + 17]
        if name:
            fields.append((name, ftype, length, decimals))
        offset += 32
    if not fields:
        raise ValueError('DBF sem campos.')
    rows = []
    pos = header_len
    for _ in range(num_records):
        if pos + record_len > len(data):
            break
        rec = data[pos:pos + record_len]
        pos += record_len
        if not rec or rec[0:1] == b'*':
            continue
        row = {}
        at = 1
        for name, ftype, length, decimals in fields:
            raw = rec[at:at + length]
            at += length
            if ftype in 'CVMGP':
                value = _decode(raw)
            elif ftype == 'N':
                text = _decode(raw)
                if not text:
                    value = None
                else:
                    try:
                        value = int(text) if decimals == 0 and '.' not in text else float(Decimal(text))
                    except (InvalidOperation, ValueError):
                        value = text
            elif ftype == 'F':
                text = _decode(raw)
                value = float(text) if text else None
            elif ftype == 'L':
                value = raw[:1].upper() in b'YT'
            elif ftype == 'D':
                text = _decode(raw)
                if len(text) == 8 and text.isdigit() and text != '00000000':
                    value = f'{text[0:4]}-{text[4:6]}-{text[6:8]}'
                else:
                    value = None
            elif ftype == 'I':
                value = struct.unpack('<i', raw[:4])[0] if len(raw) >= 4 else None
            elif ftype in 'Y':
                if len(raw) >= 8:
                    n = struct.unpack('<q', raw[:8])[0]
                    value = str(Decimal(n) / Decimal(10000))
                else:
                    value = None
            else:
                value = _decode(raw)
            row[name] = value
        rows.append(row)
    return rows


import unicodedata
from difflib import SequenceMatcher


TARGET_FIELDS = [
    ('chave_acesso', 'Chave de acesso'),
    ('numero', 'Número do documento'),
    ('serie', 'Série'),
    ('data_emissao', 'Data de emissão'),
    ('emitente_cnpj', 'CNPJ/CPF emitente'),
    ('destinatario_cnpj', 'CNPJ/CPF destinatário'),
    ('cliente_nome', 'Nome cliente/fornecedor'),
    ('produto_codigo', 'Código do produto'),
    ('produto_descricao', 'Descrição do produto'),
    ('produto_ean', 'Código de barras/EAN'),
    ('quantidade', 'Quantidade'),
    ('valor_unitario', 'Valor unitário'),
    ('valor_total', 'Valor total'),
    ('desconto', 'Desconto'),
    ('cfop', 'CFOP'),
    ('ncm', 'NCM'),
    ('unidade', 'Unidade'),
    ('valor_nf', 'Valor total da nota'),
]


def normalize_name(value: str) -> str:
    value = unicodedata.normalize('NFKD', str(value or ''))
    value = ''.join(ch for ch in value if not unicodedata.combining(ch))
    value = value.lower()
    out = []
    for ch in value:
        out.append(ch if ch.isalnum() else ' ')
    return ' '.join(''.join(out).split())


def suggest_mapping(columns: list[str]) -> dict:
    aliases = {
        'chave_acesso': ['chave acesso', 'chave', 'chnfe', 'chave nfe', 'chave nf', 'access key'],
        'numero': ['numero', 'nro', 'num', 'nf', 'nfe', 'numero nota', 'numero documento'],
        'serie': ['serie', 'ser'],
        'data_emissao': ['data emissao', 'emissao', 'data', 'dt emissao', 'dtemissao'],
        'emitente_cnpj': ['cnpj emitente', 'emitente cnpj', 'cpf cnpj emitente', 'cnpj'],
        'destinatario_cnpj': ['cnpj destinatario', 'destinatario', 'cpf cnpj', 'cliente cnpj'],
        'cliente_nome': ['cliente', 'nome cliente', 'fornecedor', 'razao social', 'nome'],
        'produto_codigo': ['codigo produto', 'cod produto', 'produto', 'codigo', 'cprod'],
        'produto_descricao': ['descricao', 'descricao produto', 'produto descricao', 'xprod'],
        'produto_ean': ['ean', 'codigo barras', 'cod barras', 'gtin', 'cbarra'],
        'quantidade': ['quantidade', 'qtd', 'qtde', 'qcom'],
        'valor_unitario': ['valor unitario', 'vl unitario', 'preco', 'vuncom', 'unitario'],
        'valor_total': ['valor total', 'total item', 'vprod', 'vl total'],
        'desconto': ['desconto', 'vdesc', 'desc'],
        'cfop': ['cfop'],
        'ncm': ['ncm'],
        'unidade': ['unidade', 'un', 'ucom'],
        'valor_nf': ['valor nf', 'total nf', 'vnf', 'valor nota'],
    }
    suggestions = {}
    used = set()
    for col in columns:
        norm = normalize_name(col)
        best_target, best_score = None, 0.0
        for target, _label in TARGET_FIELDS:
            for alias in aliases.get(target, [target]):
                score = SequenceMatcher(None, norm, normalize_name(alias)).ratio()
                if norm == normalize_name(alias):
                    score = 1.0
                elif norm and (norm in normalize_name(alias) or normalize_name(alias) in norm):
                    score = max(score, 0.92)
                if score > best_score:
                    best_target, best_score = target, score
        if best_target and best_score >= 0.72 and best_target not in used:
            suggestions[col] = {'target': best_target, 'confidence': round(best_score, 3), 'auto': True}
            used.add(best_target)
        else:
            suggestions[col] = {'target': '', 'confidence': round(best_score, 3), 'auto': False}
    return suggestions


def dbf_preview(data: bytes, name: str = 'arquivo.dbf') -> dict:
    rows = read_dbf(data)
    columns = list(rows[0].keys()) if rows else []
    sample = rows[:50]
    return {
        'file': name, 'status': 'dbf', 'rows': len(rows), 'columns': columns, 'sample': sample,
        'suggested_mapping': suggest_mapping(columns),
        'target_fields': [{'id': k, 'label': v} for k, v in TARGET_FIELDS],
    }
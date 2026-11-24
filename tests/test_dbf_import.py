import struct, tempfile, os, unittest
from pathlib import Path

from insight_dbf import read_dbf, dbf_preview


def make_dbf(rows):
    # Minimal dBase III DBF with character fields.
    fields = []
    for name in rows[0].keys():
        fields.append((name, 'C', 20, 0))
    header_len = 32 + len(fields) * 32 + 1
    record_len = 1 + sum(f[2] for f in fields)
    buf = bytearray(header_len + record_len * len(rows) + 1)
    buf[0] = 0x03
    struct.pack_into('<IHH', buf, 4, len(rows), header_len, record_len)
    off = 32
    for name, ftype, length, dec in fields:
        buf[off:off+11] = name.encode('ascii').ljust(11, b'\x00')
        buf[off+11] = ord(ftype)
        buf[off+16] = length
        buf[off+17] = dec
        off += 32
    buf[off] = 0x0D
    pos = header_len
    for row in rows:
        buf[pos] = 0x20
        at = pos + 1
        for name, _, length, _ in fields:
            val = str(row[name]).encode('cp1252', errors='replace')[:length]
            buf[at:at+length] = val.ljust(length, b' ')
            at += length
        pos += record_len
    buf[pos] = 0x1A
    return bytes(buf)


class DbfTests(unittest.TestCase):
    def test_read_dbf_rows(self):
        data = make_dbf([{'CHAVE': '123', 'VALOR': '10,00'}, {'CHAVE': '456', 'VALOR': '20,00'}])
        rows = read_dbf(data)
        self.assertEqual(len(rows), 2)
        self.assertEqual(rows[0]['CHAVE'], '123')
        self.assertEqual(rows[1]['VALOR'], '20,00')

    def test_preview(self):
        data = make_dbf([{'A': '1'}])
        p = dbf_preview(data, 'x.dbf')
        self.assertEqual(p['status'], 'dbf')
        self.assertEqual(p['rows'], 1)
        self.assertIn('A', p['columns'])


if __name__ == '__main__':
    unittest.main()
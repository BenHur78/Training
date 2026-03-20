import sys
from pathlib import Path

md_path = Path(r"c:\Dev\Alberto\Training\Circle_Features_Statistics.md")
pdf_path = md_path.with_suffix('.pdf')

text = md_path.read_text(encoding='utf-8')
lines = text.splitlines()

def esc(s):
    return s.replace('\\', '\\\\').replace('(', '\\(').replace(')', '\\)')

# Build text stream using simple PDF text operators
font_size = 10
margin_left = 50
start_y = 820
leading = 14

stream_lines = []
stream_lines.append('BT')
stream_lines.append(f'/F1 {font_size} Tf')
stream_lines.append(f'{margin_left} {start_y} Td')
first = True
for line in lines:
    # keep line length reasonable
    text_line = esc(line)
    stream_lines.append(f'({text_line}) Tj')
    stream_lines.append(f'0 -{leading} Td')

stream_lines.append('ET')
stream = '\n'.join(stream_lines) + '\n'
stream_bytes = stream.encode('latin-1')

# PDF object templates
objs = []

# 1: Catalog
objs.append(b'1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n')
# 2: Pages
objs.append(b'2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n')
# 3: Page
objs.append(b'3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>\nendobj\n')
# 4: Font
objs.append(b'4 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n')
# 5: Contents (stream)
content_obj_header = f'5 0 obj\n<< /Length {len(stream_bytes)} >>\nstream\n'.encode('latin-1')
content_obj_footer = b'\nendstream\nendobj\n'
objs.append(content_obj_header + stream_bytes + content_obj_footer)

# Assemble PDF and compute xref
pdf_parts = []
pdf_parts.append(b'%PDF-1.4\n')
offsets = []
current_offset = 0
for part in pdf_parts:
    current_offset += len(part)

# write objects and record offsets
for obj in objs:
    offsets.append(current_offset)
    current_offset += len(obj)

body = b''.join(objs)
trailer = b''

# Build xref table
xref_offset = current_offset
xref_lines = [b'xref\n', f'0 {len(objs)+1}\n'.encode('latin-1'), b'0000000000 65535 f \n']
for off in offsets:
    xref_lines.append(f'{off:010d} 00000 n \n'.encode('latin-1'))

trailer = b''.join(xref_lines)
trailer += (b'trailer << /Root 1 0 R /Size ' + str(len(objs)+1).encode('latin-1') + b' >>\n')
trailer += (b'startxref\n' + str(xref_offset).encode('latin-1') + b'\n%%EOF\n')

pdf_all = b'%PDF-1.4\n' + body + trailer

pdf_path.write_bytes(pdf_all)
print(f'Wrote PDF: {pdf_path}')

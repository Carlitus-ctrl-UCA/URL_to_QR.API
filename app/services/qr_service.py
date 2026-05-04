import segno
import io

def generate_qr_code(data: str, scale: int):
    # Creamos el código QR
    qr = segno.make(data)
    
    # Lo guardamos en un "buffer" (memoria RAM) para no llenar el disco de archivos basura
    buffer = io.BytesIO()
    qr.save(buffer, kind='png', scale=scale)
    buffer.seek(0)
    
    return buffer
from fastapi import FastAPI, HTTPException
from fastapi.responses import StreamingResponse
from app.schemas.qr_schema import QRRequest
from app.services.qr_service import generate_qr_code
from fastapi.middleware.cors import CORSMiddleware


app = FastAPI(title="QR Generator API", version="1.0.0")

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"], # En producción pondrías tu dominio real
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.post("/generate-qr")
async def create_qr(request: QRRequest):
    try:
        # Convertimos la URL de Pydantic a string
        url_str = str(request.url)
        
        # Llamamos al servicio
        image_buffer = generate_qr_code(url_str, request.scale)  # El servicio devuelve un BytesIO con la imagen del QR
        
        # Devolvemos la imagen directamente al navegador
        return StreamingResponse(image_buffer, media_type="image/png")
        
    except Exception as e:
        raise HTTPException(status_code=500, detail="Error generando el QR")

@app.get("/")
def home():
    return {"message": "API de QRs funcionando. Ve a /docs para probarla."}
from pydantic import BaseModel, HttpUrl, Field

class QRRequest(BaseModel):
    # Validamos que sea una URL real. Si envían "hola", la API fallará con error 422.
    url: HttpUrl 
    # Podemos permitir que elijan el tamaño (opcional)
    scale: int = Field(default=10, ge=1, le=50)
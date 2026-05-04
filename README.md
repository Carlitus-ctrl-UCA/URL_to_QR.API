# URL_to_QR.API

A simple ASP.NET Core Web API that accepts a URL and returns a QR code image (PNG).

## Endpoints

### `GET /api/qr?url={url}`

Generates a QR code for the given URL and returns it as a `image/png` response.

**Query parameters**

| Parameter | Type   | Required | Description                             |
|-----------|--------|----------|-----------------------------------------|
| `url`     | string | Yes      | An absolute HTTP or HTTPS URL to encode |

**Example**

```
GET /api/qr?url=https://www.github.com
```

Returns a PNG image containing the QR code.

**Error responses**

| Status | Reason                                                   |
|--------|----------------------------------------------------------|
| 400    | `url` is missing, empty, or not a valid HTTP/HTTPS URL   |

## Running locally

```bash
cd URL_to_QR.API
dotnet run
```

The API will be available at `https://localhost:<port>/api/qr?url=https://example.com`.

## Running tests

```bash
dotnet test
```

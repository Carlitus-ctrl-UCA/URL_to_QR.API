using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace URL_to_QR.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QRController : ControllerBase
{
    /// <summary>
    /// Generates a QR code image for the given URL.
    /// </summary>
    /// <param name="url">The URL to encode as a QR code.</param>
    /// <returns>A PNG image containing the QR code.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Get([FromQuery] string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return BadRequest(new { error = "The 'url' query parameter is required." });

        if (!Uri.TryCreate(url, UriKind.Absolute, out var parsedUri)
            || (parsedUri.Scheme != Uri.UriSchemeHttp && parsedUri.Scheme != Uri.UriSchemeHttps))
            return BadRequest(new { error = "The 'url' parameter must be a valid absolute HTTP or HTTPS URL." });

        using var qrGenerator = new QRCodeGenerator();
        using var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        return File(qrCodeBytes, "image/png");
    }
}

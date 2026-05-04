using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace URL_to_QR.API.Tests;

public class QRControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public QRControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_WithValidUrl_ReturnsPngImage()
    {
        var response = await _client.GetAsync("/api/qr?url=https://www.google.com");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/png", response.Content.Headers.ContentType?.MediaType);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        Assert.True(bytes.Length > 0, "Response body should contain PNG bytes.");
    }

    [Fact]
    public async Task Get_WithMissingUrl_ReturnsBadRequest()
    {
        var response = await _client.GetAsync("/api/qr");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Get_WithEmptyUrl_ReturnsBadRequest()
    {
        var response = await _client.GetAsync("/api/qr?url=");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Get_WithInvalidUrl_ReturnsBadRequest()
    {
        var response = await _client.GetAsync("/api/qr?url=not-a-valid-url");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Get_WithHttpUrl_ReturnsPngImage()
    {
        var response = await _client.GetAsync("/api/qr?url=http://example.com/path?query=value");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/png", response.Content.Headers.ContentType?.MediaType);
    }
}

using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using WelcomeApi.Models;
using Xunit;

namespace WelcomeApi.Tests;

public class WelcomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WelcomeControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Returns_DefaultMessage()
    {
        var response = await _client.GetAsync("/api/welcome");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<WelcomeMessageResponse>();
        Assert.NotNull(result);
        Assert.Equal("Bienvenido a Dev Containers con .NET!", result.Message);
    }

    [Fact]
    public async Task Post_Updates_Message()
    {
        var request = new WelcomeMessageRequest { Message = "Hola desde las pruebas" };

        var response = await _client.PostAsJsonAsync("/api/welcome", request);
        response.EnsureSuccessStatusCode();

        var updated = await response.Content.ReadFromJsonAsync<WelcomeMessageResponse>();
        Assert.NotNull(updated);
        Assert.Equal(request.Message, updated.Message);

        var secondResponse = await _client.GetAsync("/api/welcome");
        secondResponse.EnsureSuccessStatusCode();

        var retrieved = await secondResponse.Content.ReadFromJsonAsync<WelcomeMessageResponse>();
        Assert.NotNull(retrieved);
        Assert.Equal(request.Message, retrieved.Message);
    }

    [Fact]
    public async Task Post_InvalidRequest_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/welcome", new WelcomeMessageRequest { Message = string.Empty });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

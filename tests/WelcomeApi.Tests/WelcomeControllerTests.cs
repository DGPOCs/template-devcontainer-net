using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
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
        result.Should().NotBeNull();
        result!.Message.Should().Be("Bienvenido a Dev Containers con .NET!");
    }

    [Fact]
    public async Task Post_Updates_Message()
    {
        var request = new WelcomeMessageRequest { Message = "Hola desde las pruebas" };

        var response = await _client.PostAsJsonAsync("/api/welcome", request);
        response.EnsureSuccessStatusCode();

        var updated = await response.Content.ReadFromJsonAsync<WelcomeMessageResponse>();
        updated.Should().NotBeNull();
        updated!.Message.Should().Be(request.Message);

        var secondResponse = await _client.GetAsync("/api/welcome");
        secondResponse.EnsureSuccessStatusCode();

        var retrieved = await secondResponse.Content.ReadFromJsonAsync<WelcomeMessageResponse>();
        retrieved.Should().NotBeNull();
        retrieved!.Message.Should().Be(request.Message);
    }

    [Fact]
    public async Task Post_InvalidRequest_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/welcome", new WelcomeMessageRequest { Message = string.Empty });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

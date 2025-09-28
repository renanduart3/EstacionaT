using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EstacionaT.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EstacionaT.Tests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_CreateTicket_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync("/tickets", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
        Assert.NotNull(ticket);
    }

    [Fact]
    public async Task Post_PayTicket_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.PostAsync("/tickets", null);
        var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
        Assert.NotNull(ticket);

        // Act
        var payResponse = await client.PostAsync($"/tickets/{ticket.Id}/pay", null);

        // Assert
        payResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, payResponse.StatusCode);
    }

    [Fact]
    public async Task Post_ValidateExit_FreeTicket_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var response = await client.PostAsync("/tickets", null);
        var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
        Assert.NotNull(ticket);

        // Act
        var validateResponse = await client.PostAsync($"/tickets/{ticket.Id}/validate", null);

        // Assert
        validateResponse.EnsureSuccessStatusCode();
        var content = await validateResponse.Content.ReadAsStringAsync();
        Assert.Equal("Exit allowed.", content);
    }

    [Fact]
    public async Task Post_ActivateCongestion_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync("/traffic/activate", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Congestion mode activated.", content);
    }

    [Fact]
    public async Task Post_DeactivateCongestion_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync("/traffic/deactivate", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Congestion mode deactivated.", content);
    }
}
using GameShop.API.Write.Contracts.Requests;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Write.IntegrationTests.Commands;

public sealed class AddGameConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_games_console(
        AddGameConsoleRequest addGameConsoleRequest)
    {
        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGameConsole", addGameConsoleRequest);

        var expected = new GameConsole()
        {
            Id = 1,
            Manufacturer = addGameConsoleRequest.Manufacturer,
            Name = addGameConsoleRequest.Name,
            Price = Price.Create(addGameConsoleRequest.Price),
        };

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.GameConsoles.SingleAsync();
        var actualWriteReadDb = await WriteReadDbContext.GameConsoles.SingleAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actualReadOnlyDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.Games));
        actualWriteReadDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.Games));
    }
}

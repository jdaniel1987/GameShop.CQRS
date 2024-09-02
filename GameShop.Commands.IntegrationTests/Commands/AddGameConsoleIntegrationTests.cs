using GameShop.Application.Commands.AddGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public sealed class AddGameConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_games_console(
        AddGameConsoleCommand addGameConsoleCommand)
    {
        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGameConsole", addGameConsoleCommand);

        var expected = new GameConsole()
        {
            Id = 1,
            Manufacturer = addGameConsoleCommand.Manufacturer,
            Name = addGameConsoleCommand.Name,
            Price = Price.Create(addGameConsoleCommand.Price),
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

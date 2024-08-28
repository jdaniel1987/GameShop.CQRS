using GameShop.Application.Commands.AddGamesConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public class AddGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_games_console(
        AddGamesConsoleCommand addGamesConsoleCommand)
    {
        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGamesConsole", addGamesConsoleCommand);

        var expected = new GamesConsole()
        {
            Id = 1,
            Manufacturer = addGamesConsoleCommand.Manufacturer,
            Name = addGamesConsoleCommand.Name,
            Price = Price.Create(addGamesConsoleCommand.Price),
        };

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.GamesConsoles.SingleAsync();
        var actualWriteReadDb = await WriteReadDbContext.GamesConsoles.SingleAsync();
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

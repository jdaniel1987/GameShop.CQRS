using GamesShop.Application.Commands.AddGame;
using GamesShop.Domain.Entities;
using GamesShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GamesShop.Commands.IntegrationTests.Commands;

public class AddGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_game(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        var addGameCommand = fixture.Build<AddGameCommand>()
            .With(g => g.GamesConsoleId, existingGamesConsole.Id)
            .Create();

        await ReadOnlyDbContext.AddAsync(existingGamesConsole);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new Game()
        {
            Id = 1,
            Publisher = addGameCommand.Publisher,
            GamesConsoleId = addGameCommand.GamesConsoleId,
            Name = addGameCommand.Name,
            Price = Price.Create(addGameCommand.Price),
        };

        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGame", addGameCommand);

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.Games.SingleAsync();
        var actualWriteReadDb = await WriteReadDbContext.Games.SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actualReadOnlyDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GamesConsole));
        actualWriteReadDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GamesConsole));
        MockEmailLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", "random@email.com"), Times.Once);
    }
}

using GameShop.Application.Commands.UpdateGame;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public class UpdateGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_game(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsoles = fixture
            .Build<GamesConsole>()
            .Without(c => c.Games)
            .CreateMany(2);

        var existingGame = fixture
            .Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsoles.First().Id)
            .Without(c => c.GamesConsole)
            .Create();

        await WriteReadDbContext.AddRangeAsync(existingGamesConsoles);
        await WriteReadDbContext.AddAsync(existingGame);
        await WriteReadDbContext.SaveChangesAsync();

        var updateGameCommand = fixture
            .Build<UpdateGameCommand>()
            .With(c => c.Id, existingGame.Id)
            .With(c => c.GamesConsoleId, existingGamesConsoles.Last().Id)
            .Create();

        var expected = new Game()
        {
            Id = updateGameCommand.Id,
            Name = updateGameCommand.Name,
            GamesConsoleId = updateGameCommand.GamesConsoleId,
            Price = Price.Create(updateGameCommand.Price),
            Publisher = updateGameCommand.Publisher,
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGame", updateGameCommand);

        // Assert
        var actual = await WriteReadDbContext
            .Games
            .AsNoTracking()
            .Where(g => g.Id == updateGameCommand.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEquivalentTo(expected);
    }
}

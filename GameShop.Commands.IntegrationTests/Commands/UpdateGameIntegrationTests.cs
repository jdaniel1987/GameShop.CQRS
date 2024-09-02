using GameShop.Application.Commands.UpdateGame;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public sealed class UpdateGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_game(
        IFixture fixture)
    {
        // Arrange
        var existingGameConsoles = fixture
            .Build<GameConsole>()
            .Without(c => c.Games)
            .CreateMany(2);

        var existingGame = fixture
            .Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsoles.First().Id)
            .Without(c => c.GameConsole)
            .Create();

        await WriteReadDbContext.AddRangeAsync(existingGameConsoles);
        await WriteReadDbContext.AddAsync(existingGame);
        await WriteReadDbContext.SaveChangesAsync();

        var updateGameCommand = fixture
            .Build<UpdateGameCommand>()
            .With(c => c.Id, existingGame.Id)
            .With(c => c.GameConsoleId, existingGameConsoles.Last().Id)
            .Create();

        var expected = new Game()
        {
            Id = updateGameCommand.Id,
            Name = updateGameCommand.Name,
            GameConsoleId = updateGameCommand.GameConsoleId,
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

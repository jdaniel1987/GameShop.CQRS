using GameShop.API.Write.Contracts.Requests;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Write.IntegrationTests.Commands;

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

        var updateGameRequest = fixture
            .Build<UpdateGameRequest>()
            .With(c => c.Id, existingGame.Id)
            .With(c => c.GameConsoleId, existingGameConsoles.Last().Id)
            .Create();

        var expected = new Game()
        {
            Id = updateGameRequest.Id,
            Name = updateGameRequest.Name,
            GameConsoleId = updateGameRequest.GameConsoleId,
            Price = Price.Create(updateGameRequest.Price),
            Publisher = updateGameRequest.Publisher,
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGame", updateGameRequest);

        // Assert
        var actual = await WriteReadDbContext
            .Games
            .AsNoTracking()
            .Where(g => g.Id == updateGameRequest.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEquivalentTo(expected);
    }
}

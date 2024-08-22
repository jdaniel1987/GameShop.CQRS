using GamesShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesShop.Commands.IntegrationTests.Commands;

public class DeleteGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_delete_game(
        IFixture fixture,
        int gameIdToDelete)
    {
        // Arrange
        var existingGamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        var existingGame = fixture.Build<Game>()
            .With(c => c.Id, gameIdToDelete)
            .With(c => c.GamesConsoleId, existingGamesConsole.Id)
            .Without(c => c.GamesConsole)
            .Create();

        await WriteReadDbContext.AddAsync(existingGamesConsole);
        await WriteReadDbContext.AddAsync(existingGame);
        await WriteReadDbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.DeleteAsync($"api/DeleteGame/{gameIdToDelete}");

        // Assert
        var actualReadOnlyDbContext = await ReadOnlyDbContext.Games.ToArrayAsync();
        var actualWriteReadDbContext = await WriteReadDbContext.Games.ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actualReadOnlyDbContext.Should().BeEmpty();
        actualWriteReadDbContext.Should().BeEmpty();
    }
}

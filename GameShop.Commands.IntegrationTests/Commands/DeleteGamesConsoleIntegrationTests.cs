using GameShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public class DeleteGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_delete_games_console(
        IFixture fixture,
        int gamesConsoleIdToDelete)
    {
        // Arrange
        var existingGamesConsole = fixture.Build<GamesConsole>()
            .With(c => c.Id, gamesConsoleIdToDelete)
            .Without(c => c.Games)
            .Create();
        await WriteReadDbContext.AddAsync(existingGamesConsole);
        await WriteReadDbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.DeleteAsync($"api/DeleteGamesConsole/{gamesConsoleIdToDelete}");

        // Assert
        var actual = await WriteReadDbContext.GamesConsoles.ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEmpty();
    }
}

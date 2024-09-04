using GameShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Write.IntegrationTests.Commands;

public sealed class DeleteGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_delete_game(
        IFixture fixture,
        int gameIdToDelete)
    {
        // Arrange
        var existingGameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        var existingGame = fixture.Build<Game>()
            .With(c => c.Id, gameIdToDelete)
            .With(c => c.GameConsoleId, existingGameConsole.Id)
            .Without(c => c.GameConsole)
            .Create();

        await WriteReadDbContext.AddAsync(existingGameConsole);
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

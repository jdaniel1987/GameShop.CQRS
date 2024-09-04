using GameShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Write.IntegrationTests.Commands;

public sealed class DeleteGameConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_delete_games_console(
        IFixture fixture,
        int gameConsoleIdToDelete)
    {
        // Arrange
        var existingGameConsole = fixture.Build<GameConsole>()
            .With(c => c.Id, gameConsoleIdToDelete)
            .Without(c => c.Games)
            .Create();
        await WriteReadDbContext.AddAsync(existingGameConsole);
        await WriteReadDbContext.SaveChangesAsync();

        // Act
        var response = await ApiClient.DeleteAsync($"api/DeleteGameConsole/{gameConsoleIdToDelete}");

        // Assert
        var actual = await WriteReadDbContext.GameConsoles.ToArrayAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEmpty();
    }
}

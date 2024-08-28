using GameShop.Application.Commands.UpdateGamesConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public class UpdateGamesConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_games_console(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsole = fixture
            .Build<GamesConsole>()
            .Without(c => c.Games)
            .Create();

        await WriteReadDbContext.AddAsync(existingGamesConsole);
        await WriteReadDbContext.SaveChangesAsync();

        var updateGamesConsoleCommand = fixture
            .Build<UpdateGamesConsoleCommand>()
            .With(c => c.Id, existingGamesConsole.Id)
            .Create();

        var expected = new GamesConsole()
        {
            Id = existingGamesConsole.Id,
            Name = updateGamesConsoleCommand.Name,
            Manufacturer = updateGamesConsoleCommand.Manufacturer,
            Price = Price.Create(updateGamesConsoleCommand.Price),
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGamesConsole", updateGamesConsoleCommand);

        // Assert
        var actual = await WriteReadDbContext
            .GamesConsoles
            .AsNoTracking()
            .Where(g => g.Id == updateGamesConsoleCommand.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEquivalentTo(expected);
    }
}

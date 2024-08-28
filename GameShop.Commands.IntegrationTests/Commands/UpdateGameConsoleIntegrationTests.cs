using GameShop.Application.Commands.UpdateGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Commands.IntegrationTests.Commands;

public class UpdateGameConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_update_games_console(
        IFixture fixture)
    {
        // Arrange
        var existingGameConsole = fixture
            .Build<GameConsole>()
            .Without(c => c.Games)
            .Create();

        await WriteReadDbContext.AddAsync(existingGameConsole);
        await WriteReadDbContext.SaveChangesAsync();

        var updateGameConsoleCommand = fixture
            .Build<UpdateGameConsoleCommand>()
            .With(c => c.Id, existingGameConsole.Id)
            .Create();

        var expected = new GameConsole()
        {
            Id = existingGameConsole.Id,
            Name = updateGameConsoleCommand.Name,
            Manufacturer = updateGameConsoleCommand.Manufacturer,
            Price = Price.Create(updateGameConsoleCommand.Price),
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGameConsole", updateGameConsoleCommand);

        // Assert
        var actual = await WriteReadDbContext
            .GameConsoles
            .AsNoTracking()
            .Where(g => g.Id == updateGameConsoleCommand.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEquivalentTo(expected);
    }
}

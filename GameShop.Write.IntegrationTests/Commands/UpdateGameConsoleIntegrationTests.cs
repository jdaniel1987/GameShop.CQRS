using GameShop.API.Write.Contracts.Requests;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Write.IntegrationTests.Commands;

public sealed class UpdateGameConsoleIntegrationTests : ApiBaseTests
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

        var updateGameConsoleRequest = fixture
            .Build<UpdateGameConsoleRequest>()
            .With(c => c.Id, existingGameConsole.Id)
            .Create();

        var expected = new GameConsole()
        {
            Id = existingGameConsole.Id,
            Name = updateGameConsoleRequest.Name,
            Manufacturer = updateGameConsoleRequest.Manufacturer,
            Price = Price.Create(updateGameConsoleRequest.Price),
        };

        // Act
        var response = await ApiClient.PutAsJsonAsync($"api/UpdateGameConsole", updateGameConsoleRequest);

        // Assert
        var actual = await WriteReadDbContext
            .GameConsoles
            .AsNoTracking()
            .Where(g => g.Id == updateGameConsoleRequest.Id)
            .SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        actual.Should().BeEquivalentTo(expected);
    }
}

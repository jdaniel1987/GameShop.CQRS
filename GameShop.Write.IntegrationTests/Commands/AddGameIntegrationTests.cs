using GameShop.API.Write.Contracts.Requests;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameShop.Write.IntegrationTests.Commands;

public sealed class AddGameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_add_game(
        IFixture fixture)
    {
        // Arrange
        var existingGameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        var addGameRequest = fixture.Build<AddGameRequest>()
            .With(g => g.GameConsoleId, existingGameConsole.Id)
            .Create();

        await ReadOnlyDbContext.AddAsync(existingGameConsole);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new Game()
        {
            Id = 1,
            Publisher = addGameRequest.Publisher,
            GameConsoleId = addGameRequest.GameConsoleId,
            Name = addGameRequest.Name,
            Price = Price.Create(addGameRequest.Price),
        };

        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGame", addGameRequest);

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.Games.SingleAsync();
        var actualWriteReadDb = await WriteReadDbContext.Games.SingleAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actualReadOnlyDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GameConsole));
        actualWriteReadDb.Should().BeEquivalentTo(expected,
            opts => opts
            .Excluding(g => g.Id)
            .Excluding(g => g.GameConsole));
        MockEmailLogger.VerifyLog(mock => mock.LogInformation("Sent mail to {Email} ", "random@email.com"), Times.Once);
    }
}

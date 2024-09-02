using GameShop.Application.Commands.AddGame;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameShop.Commands.IntegrationTests.Commands;

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

        var addGameCommand = fixture.Build<AddGameCommand>()
            .With(g => g.GameConsoleId, existingGameConsole.Id)
            .Create();

        await ReadOnlyDbContext.AddAsync(existingGameConsole);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new Game()
        {
            Id = 1,
            Publisher = addGameCommand.Publisher,
            GameConsoleId = addGameCommand.GameConsoleId,
            Name = addGameCommand.Name,
            Price = Price.Create(addGameCommand.Price),
        };

        // Act
        var response = await ApiClient.PostAsJsonAsync("api/AddGame", addGameCommand);

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

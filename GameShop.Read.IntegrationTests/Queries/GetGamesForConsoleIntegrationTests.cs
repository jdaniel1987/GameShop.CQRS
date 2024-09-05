using GameShop.API.Read.Contracts.Responses;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Read.IntegrationTests.Queries;

public sealed class GetGamesForConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_for_Console(
        IFixture fixture)
    {
        // Arrange
        var gameConsoleIdToFind = fixture.Create<int>();
        var existingGameConsole1 = fixture.Build<GameConsole>()
            .With(gc => gc.Id, gameConsoleIdToFind)
            .Without(gc => gc.Games)
            .Create();
        var existingGameConsole2 = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsole1.Id)
            .With(g => g.GameConsole, existingGameConsole1)
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsole2.Id)
            .With(g => g.GameConsole, existingGameConsole2)
            .CreateMany();
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddRangeAsync([existingGameConsole1, existingGameConsole2]);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetGamesForConsoleResponse(existingGames1.Select(g =>
            new GetGamesForConsoleResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
                .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesForConsoleResponse>($"api/GamesForConsole/{gameConsoleIdToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

using AutoFixture;
using AutoFixture.Xunit2;
using GameShop.Application.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Queries.IntegrationTests.Queries;

public class GetGamesForConsoleIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_for_Console(
        IFixture fixture)
    {
        // Arrange
        var gamesConsoleIdToFind = fixture.Create<int>();
        var existingGamesConsole1 = fixture.Build<GamesConsole>()
            .With(gc => gc.Id, gamesConsoleIdToFind)
            .Without(gc => gc.Games)
            .Create();
        var existingGamesConsole2 = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsole1.Id)
            .With(g => g.GamesConsole, existingGamesConsole1)
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsole2.Id)
            .With(g => g.GamesConsole, existingGamesConsole2)
            .CreateMany();
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddRangeAsync([existingGamesConsole1, existingGamesConsole2]);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetGamesForConsoleResponse(existingGames1.Select(g => 
            new GetGamesForConsoleResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
                .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesForConsoleResponse>($"api/GamesForConsole/{gamesConsoleIdToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

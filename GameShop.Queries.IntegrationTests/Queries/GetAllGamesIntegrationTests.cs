using AutoFixture;
using AutoFixture.Xunit2;
using GameShop.Application.Queries.GetAllGames;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Queries.IntegrationTests.Queries;

public class GetAllGamesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsoles = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .CreateMany();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsoles.First().Id)
            .With(g => g.GamesConsole, existingGamesConsoles.First())
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsoles.Last().Id)
            .With(g => g.GamesConsole, existingGamesConsoles.Last())
            .CreateMany();
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddRangeAsync(existingGamesConsoles);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetAllGamesResponse(allGames.Select(g =>
            new GetAllGamesResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGamesResponse>("api/Games");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

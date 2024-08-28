using AutoFixture;
using AutoFixture.Xunit2;
using GameShop.Application.Queries.GetGamesByName;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Queries.IntegrationTests.Queries;

public class GetGamesByNameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_by_name(
        IFixture fixture)
    {
        // Arrange
        var nameToFind = fixture.Create<string>();
        var existingGamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.Name, $"{fixture.Create<string>()}{nameToFind}{fixture.Create<string>()}")
            .With(g => g.GamesConsole, existingGamesConsole)
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GamesConsoleId, existingGamesConsole.Id)
            .With(g => g.GamesConsole, existingGamesConsole)
            .CreateMany(); 
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddAsync(existingGamesConsole);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetGamesByNameResponse(existingGames1.Select(g => 
            new GetGamesByNameResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
                .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesByNameResponse>($"api/GamesByName/{nameToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

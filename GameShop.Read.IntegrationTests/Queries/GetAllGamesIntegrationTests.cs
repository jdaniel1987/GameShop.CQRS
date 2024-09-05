using GameShop.API.Read.Contracts.Responses;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Read.IntegrationTests.Queries;

public sealed class GetAllGamesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games(
        IFixture fixture)
    {
        // Arrange
        var existingGameConsoles = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .CreateMany();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsoles.First().Id)
            .With(g => g.GameConsole, existingGameConsoles.First())
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsoles.Last().Id)
            .With(g => g.GameConsole, existingGameConsoles.Last())
            .CreateMany();
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddRangeAsync(existingGameConsoles);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetAllGamesResponse(allGames.Select(g =>
            new GetAllGamesResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
            .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGamesResponse>("api/Games");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

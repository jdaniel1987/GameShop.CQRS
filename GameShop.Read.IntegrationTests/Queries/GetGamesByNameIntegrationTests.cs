using GameShop.API.Read.Contracts.Responses;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Read.IntegrationTests.Queries;

public sealed class GetGamesByNameIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_games_by_name(
        IFixture fixture)
    {
        // Arrange
        var nameToFind = fixture.Create<string>();
        var existingGameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();
        var existingGames1 = fixture.Build<Game>()
            .With(g => g.Name, $"{fixture.Create<string>()}{nameToFind}{fixture.Create<string>()}")
            .With(g => g.GameConsole, existingGameConsole)
            .CreateMany();
        var existingGames2 = fixture.Build<Game>()
            .With(g => g.GameConsoleId, existingGameConsole.Id)
            .With(g => g.GameConsole, existingGameConsole)
            .CreateMany();
        var allGames = existingGames1.Concat(existingGames2);
        await ReadOnlyDbContext.AddAsync(existingGameConsole);
        await ReadOnlyDbContext.AddRangeAsync(allGames);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetGamesByNameResponse(existingGames1.Select(g =>
            new GetGamesByNameResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
                .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetGamesByNameResponse>($"api/GamesByName/{nameToFind}");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

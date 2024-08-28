using AutoFixture;
using AutoFixture.Xunit2;
using GameShop.Application.Queries.GetAllGamesConsoles;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Queries.IntegrationTests.Queries;

public class GetAllGamesConsolesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games_consoles(
        IFixture fixture)
    {
        // Arrange
        var existingGamesConsoles = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .CreateMany();
        await ReadOnlyDbContext.AddRangeAsync(existingGamesConsoles);
        await ReadOnlyDbContext.SaveChangesAsync();
        
        var expected = new GetAllGamesConsolesResponse(existingGamesConsoles.Select(gc => 
            new GetAllGamesConsolesResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count
            ))
            .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGamesConsolesResponse>("api/GamesConsoles");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

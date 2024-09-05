using GameShop.API.Read.Contracts.Responses;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Read.IntegrationTests.Queries;

public sealed class GetAllGameConsolesIntegrationTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_games_consoles(
        IFixture fixture)
    {
        // Arrange
        var existingGameConsoles = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .CreateMany();
        await ReadOnlyDbContext.AddRangeAsync(existingGameConsoles);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetAllGameConsolesResponse(existingGameConsoles.Select(gc =>
            new GetAllGameConsolesResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count
            ))
            .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllGameConsolesResponse>("api/GameConsoles");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}

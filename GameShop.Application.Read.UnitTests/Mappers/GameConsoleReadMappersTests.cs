using GameShop.Domain.Entities;
using System.Collections.Immutable;
using GameShop.Application.Read.Mappers;

namespace GameShop.Application.Read.UnitTests.Mappers;

public sealed class GameConsoleReadMappersTests
{
    public sealed class GameConsolesToGetAllGameConsolesQueryResponse
    {
        [Theory, AutoData]
        public void ToGetAllGameConsolesQueryResponse_ShouldConvertGameConsolesToGetAllGameConsolesQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var gameConsoles = fixture.Build<GameConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GameConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = gameConsoles.ToGetAllGameConsolesQueryResponse();

            // Assert
            queryResponse.GameConsoles.Should().HaveCount(gameConsoles.Length);
            queryResponse.GameConsoles.Select(g => g.Id).Should().BeEquivalentTo(gameConsoles.Select(g => g.Id));
            queryResponse.GameConsoles.Select(g => g.Name).Should().BeEquivalentTo(gameConsoles.Select(g => g.Name));
            queryResponse.GameConsoles.Select(g => g.Manufacturer).Should().BeEquivalentTo(gameConsoles.Select(g => g.Manufacturer));
            queryResponse.GameConsoles.Select(g => g.Price).Should().BeEquivalentTo(gameConsoles.Select(g => g.Price.Value));
            queryResponse.GameConsoles.Select(g => g.NumberOfGames).Should().BeEquivalentTo(gameConsoles.Select(g => g.Games.Count));
        }
    }
}

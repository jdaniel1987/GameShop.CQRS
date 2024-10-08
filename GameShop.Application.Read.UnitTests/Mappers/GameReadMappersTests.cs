using GameShop.Application.Read.Mappers;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.Read.UnitTests.Mappers;

public sealed class GameReadExtensionsTests
{
    public sealed class GamesToGetAllGamesQueryResponse
    {
        [Theory, AutoData]
        public void ToGetAllGamesResponse_ShouldConvertGamesToGetAllGamesQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetAllGamesQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }

    public sealed class GamesToGetGamesByNameQueryResponse
    {
        [Theory, AutoData]
        public void ToGetGamesByNameQueryResponse_ShouldConvertGamesToGetGamesByNameQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetGamesByNameQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }

    public sealed class GamesToGetGamesForConsoleQueryResponse
    {
        [Theory, AutoData]
        public void ToGetGamesForConsoleQueryResponse_ShouldConvertGamesToGetGamesForConsoleQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetGamesForConsoleQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }
}

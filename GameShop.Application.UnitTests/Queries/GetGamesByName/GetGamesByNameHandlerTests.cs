using GameShop.Application.Queries.GetGamesByName;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using System.Collections.Immutable;
namespace GameShop.Application.UnitTests.Queries.GetGamesByName;

public sealed class GetGamesByNameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreFoundByName(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetGamesByNameQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesByNameHandler(gameRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GameConsole, fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        var expected = new GetGamesByNameQueryResponse(games
            .Select(g => new GetGamesByNameQueryResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
            .ToImmutableArray());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetGamesByNameQuery query)
    {
        // Arrange
        var handler = new GetGamesByNameHandler(gameRepositoryMock.Object);

        gameRepositoryMock
            .Setup(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}

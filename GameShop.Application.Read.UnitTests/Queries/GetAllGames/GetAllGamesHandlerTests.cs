using GameShop.Application.Read.Queries.GetAllGames;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using System.Collections.Immutable;

namespace GameShop.Application.Read.UnitTests.Queries.GetAllGames;

public sealed class GetAllGamesHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreRetrieved(
        [Frozen] Mock<IGameReadRepository> gameReadRepositoryMock,
        GetAllGamesQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetAllGamesHandler(gameReadRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GameConsole, fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();
        gameReadRepositoryMock
            .Setup(repo => repo.GetAllGames(It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        var expected = new GetAllGamesQueryResponse(games
            .Select(g => new GetAllGamesQueryResponseItem(
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
        gameReadRepositoryMock.Verify(repo => repo.GetAllGames(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameReadRepository> gameReadRepositoryMock,
        GetAllGamesQuery query)
    {
        // Arrange
        var handler = new GetAllGamesHandler(gameReadRepositoryMock.Object);

        gameReadRepositoryMock
            .Setup(repo => repo.GetAllGames(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameReadRepositoryMock.Verify(repo => repo.GetAllGames(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}

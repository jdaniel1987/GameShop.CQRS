using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using System.Collections.Immutable;
using GameShop.Application.Queries.GetGamesForConsole;

namespace GameShop.Application.UnitTests.Queries.GetGamesForConsole;

public sealed class GetGamesForConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameConsoleIsNotFound(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetGamesForConsoleQuery query)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gameConsoleRepositoryMock.Object);

        gameConsoleRepositoryMock.Setup(repo => repo.GetGameConsole(query.GameConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameConsole?)null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Console with ID {query.GameConsoleId} does not exist");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreFoundForConsole(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        GetGamesForConsoleQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gameConsoleRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GameConsole, fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();

        var gameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesForConsole(query.GameConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        gameConsoleRepositoryMock
            .Setup(repo => repo.GetGameConsole(query.GameConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        var expected = new GetGamesForConsoleQueryResponse(games
            .Select(g => new GetGamesForConsoleQueryResponseItem(
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
        gameRepositoryMock.Verify(repo => repo.GetGamesForConsole(query.GameConsoleId, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        GetGamesForConsoleQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gameConsoleRepositoryMock.Object);

        var gameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesForConsole(query.GameConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        gameConsoleRepositoryMock
            .Setup(repo => repo.GetGameConsole(query.GameConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetGamesForConsole(query.GameConsoleId, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}

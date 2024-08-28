using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using Moq;
using FluentAssertions;
using System.Collections.Immutable;
using GameShop.Application.Queries.GetGamesForConsole;
using GameShop.Application.Queries.GetGamesByName;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GameShop.Application.UnitTests.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandlerTests
{
    private readonly IFixture _fixture;

    public GetGamesForConsoleHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGamesConsoleIsNotFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetGamesForConsoleQuery query)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object);

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((GamesConsole?)null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Console with ID {query.GamesConsoleId} does not exist");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreFoundForConsole(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        GetGamesForConsoleQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();

        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesForConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        gamesConsoleRepositoryMock
            .Setup(repo => repo.GetGamesConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        var expected = new GetGamesForConsoleResponse(games
            .Select(g => new GetGamesForConsoleResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetGamesForConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        GetGamesForConsoleQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesForConsoleHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object);

        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesForConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        gamesConsoleRepositoryMock
            .Setup(repo => repo.GetGamesConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetGamesForConsole(query.GamesConsoleId, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}

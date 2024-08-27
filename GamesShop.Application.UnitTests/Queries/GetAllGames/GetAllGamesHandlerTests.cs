using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using GamesShop.Application.Queries.GetAllGames;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using Moq;
using System.Collections.Immutable;
using FluentAssertions;

namespace GamesShop.Application.UnitTests.Queries.GetAllGames;

public class GetAllGamesHandlerTests
{
    private readonly IFixture _fixture;

    public GetAllGamesHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreRetrieved(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetAllGamesQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetAllGamesHandler(gameRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();
        gameRepositoryMock
            .Setup(repo => repo.GetAllGames(It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        var expected = new GetAllGamesResponse(games
            .Select(g => new GetAllGamesResponseItem(
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
        gameRepositoryMock.Verify(repo => repo.GetAllGames(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetAllGamesQuery query)
    {
        // Arrange
        var handler = new GetAllGamesHandler(gameRepositoryMock.Object);

        gameRepositoryMock
            .Setup(repo => repo.GetAllGames(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.GetAllGames(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}

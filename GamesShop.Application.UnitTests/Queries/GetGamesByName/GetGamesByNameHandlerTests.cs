using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using GamesShop.Application.Queries.GetGamesByName;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using Moq;
using FluentAssertions;
using System.Collections.Immutable;
using GamesShop.Application.Commands.AddGame;
using MediatR;

namespace GamesShop.Application.UnitTests.Queries.GetGamesByName;

public class GetGamesByNameHandlerTests
{
    private readonly IFixture _fixture;

    public GetGamesByNameHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreFoundByName(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        GetGamesByNameQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesByNameHandler(gameRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();

        gameRepositoryMock
            .Setup(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        var expected = new GetGamesByNameResponse(games
            .Select(g => new GetGamesByNameResponseItem(
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

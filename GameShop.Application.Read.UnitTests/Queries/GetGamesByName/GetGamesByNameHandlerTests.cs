﻿using GameShop.Application.Read.Queries.GetGamesByName;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using System.Collections.Immutable;

namespace GameShop.Application.Read.UnitTests.Queries.GetGamesByName;

public sealed class GetGamesByNameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesAreFoundByName(
        [Frozen] Mock<IGameReadRepository> gameReadRepositoryMock,
        GetGamesByNameQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetGamesByNameHandler(gameReadRepositoryMock.Object);

        var games = fixture.Build<Game>()
            .With(g => g.GameConsole, fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create())
            .CreateMany()
            .ToImmutableArray();

        gameReadRepositoryMock
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
        gameReadRepositoryMock.Verify(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameReadRepository> gameReadRepositoryMock,
        GetGamesByNameQuery query)
    {
        // Arrange
        var handler = new GetGamesByNameHandler(gameReadRepositoryMock.Object);

        gameReadRepositoryMock
            .Setup(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameReadRepositoryMock.Verify(repo => repo.GetGamesByName(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Games.Should().BeEmpty();
    }
}
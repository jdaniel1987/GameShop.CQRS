using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using System.Collections.Immutable;
using GameShop.Application.Queries.GetAllGameConsoles;

namespace GameShop.Application.UnitTests.Queries;

public sealed class GetAllGameConsolesHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGameConsolesAreRetrieved(
        [Frozen] Mock<IGameConsoleRepository> gameConsolesRepositoryMock,
        GetAllGameConsolesQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetAllGameConsolesHandler(gameConsolesRepositoryMock.Object);

        var gameConsoles = fixture.Build<GameConsole>()
            .With(g => g.Games, fixture.Build<Game>()
                .Without(gc => gc.GameConsole)
                .CreateMany()
                .ToImmutableArray())
            .CreateMany()
            .ToImmutableArray();
        gameConsolesRepositoryMock
            .Setup(repo => repo.GetAllGameConsoles(It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsoles);

        var expected = new GetAllGameConsolesQueryResponse(gameConsoles
            .Select(g => new GetAllGameConsolesQueryResponseItem(
                g.Id,
                g.Name,
                g.Manufacturer,
                g.Price.Value,
                g.Games.Count))
            .ToImmutableArray());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameConsolesRepositoryMock.Verify(repo => repo.GetAllGameConsoles(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGameConsoleRepository> gameConsolesRepositoryMock,
        GetAllGameConsolesQuery query)
    {
        // Arrange
        var handler = new GetAllGameConsolesHandler(gameConsolesRepositoryMock.Object);

        gameConsolesRepositoryMock
            .Setup(repo => repo.GetAllGameConsoles(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gameConsolesRepositoryMock.Verify(repo => repo.GetAllGameConsoles(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.GameConsoles.Should().BeEmpty();
    }
}

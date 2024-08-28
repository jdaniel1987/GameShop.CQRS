using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoFixture;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using Moq;
using System.Collections.Immutable;
using FluentAssertions;
using GameShop.Application.Queries.GetAllGamesConsoles;
using GameShop.Infrastructure.Repositories;

namespace GameShop.Application.UnitTests.Queries;

public class GetAllGamesConsolesHandlerTests
{
    private readonly IFixture _fixture;

    public GetAllGamesConsolesHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenGamesConsolesAreRetrieved(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsolesRepositoryMock,
        GetAllGamesConsolesQuery query,
        IFixture fixture)
    {
        // Arrange
        var handler = new GetAllGamesConsolesHandler(gamesConsolesRepositoryMock.Object);

        var gamesConsoles = fixture.Build<GamesConsole>()
            .With(g => g.Games, fixture.Build<Game>()
                .Without(gc => gc.GamesConsole)
                .CreateMany()
                .ToImmutableArray())
            .CreateMany()
            .ToImmutableArray();
        gamesConsolesRepositoryMock
            .Setup(repo => repo.GetAllGamesConsoles(It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsoles);

        var expected = new GetAllGamesConsolesResponse(gamesConsoles
            .Select(g => new GetAllGamesConsolesResponseItem(
                g.Id,
                g.Name,
                g.Manufacturer,
                g.Price.Value,
                g.Games.Count))
            .ToImmutableArray());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gamesConsolesRepositoryMock.Verify(repo => repo.GetAllGamesConsoles(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expected);
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnEmptyResponse_WhenNoGamesAreFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsolesRepositoryMock,
        GetAllGamesConsolesQuery query)
    {
        // Arrange
        var handler = new GetAllGamesConsolesHandler(gamesConsolesRepositoryMock.Object);

        gamesConsolesRepositoryMock
            .Setup(repo => repo.GetAllGamesConsoles(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        gamesConsolesRepositoryMock.Verify(repo => repo.GetAllGamesConsoles(It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.GamesConsoles.Should().BeEmpty();
    }
}

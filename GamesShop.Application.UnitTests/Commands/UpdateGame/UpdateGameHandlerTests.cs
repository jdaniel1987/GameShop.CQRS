using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.UpdateGame;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using Moq;

namespace GamesShop.Application.UnitTests.Commands.UpdateGame;

public class UpdateGameHandlerTests
{
    private readonly IFixture _fixture;

    public UpdateGameHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGamesConsoleIsNotFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        UpdateGameCommand command)
    {
        // Arrange
        var handler = new UpdateGameHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object);

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GamesConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GamesConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldUpdateGame_WhenGamesConsoleIsFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        UpdateGameCommand command,
        IFixture fixture)
    {
        // Arrange
        var game = fixture.Build<Game>()
            .Without(g => g.GamesConsole)
            .Create();

        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        gameRepositoryMock.Setup(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateGameHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.UpdateGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<UpdateGameResponse>();
    }
}

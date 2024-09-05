using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Commands.AddGame;
using GameShop.Application.Events.GameCreated;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.UnitTests.Commands.AddGame;

public sealed class AddGameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameConsoleIsNotFound(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command)
    {
        // Arrange
        var handler = new AddGameHandler(gameRepositoryMock.Object, gameConsoleRepositoryMock.Object, publisherMock.Object);

        gameConsoleRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GameConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldAddGameAndPublishEvent_WhenGameConsoleIsFound(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command,
        IFixture fixture)
    {
        // Arrange
        var game = fixture.Build<Game>()
            .Without(g => g.GameConsole)
            .Create();

        var gameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameConsoleRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        gameRepositoryMock.Setup(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        publisherMock.Setup(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddGameHandler(gameRepositoryMock.Object, gameConsoleRepositoryMock.Object, publisherMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        publisherMock.Verify(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGameResponse>();
    }
}

using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Events.GameCreated;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.UnitTests.Commands.AddGame;

public sealed class AddGameHandlerTests
{
    private readonly Fixture _fixture;

    public AddGameHandlerTests()
    {
        _fixture = new Fixture();
        _fixture.Customize<Game>(c => c
            .Without(g => g.GameConsole));

        _fixture.Customize<GameConsole>(c => c
            .Without(gc => gc.Games));
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameConsoleIsNotFound(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        [Frozen] Mock<IGameWriteRepository> gameWriteRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command)
    {
        // Arrange
        var handler = new AddGameHandler(gameWriteRepositoryMock.Object, gameConsoleWriteRepositoryMock.Object, publisherMock.Object);

        gameConsoleWriteRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GameConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldAddGameAndPublishEvent_WhenGameConsoleIsFound(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        [Frozen] Mock<IGameWriteRepository> gameWriteRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command)
    {
        // Arrange
        var game = _fixture.Create<Game>();
        var gameConsole = _fixture.Create<GameConsole>();

        gameConsoleWriteRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        gameWriteRepositoryMock.Setup(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        publisherMock.Setup(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddGameHandler(gameWriteRepositoryMock.Object, gameConsoleWriteRepositoryMock.Object, publisherMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameWriteRepositoryMock.Verify(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        publisherMock.Verify(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGameResponse>();
    }
}

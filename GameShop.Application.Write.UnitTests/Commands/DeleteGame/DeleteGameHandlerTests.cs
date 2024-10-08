using GameShop.Application.Write.Commands.DeleteGame;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Application.Write.UnitTests.Commands.DeleteGame;

public sealed class DeleteGameHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameIsNotFound(
        [Frozen] Mock<IGameWriteRepository> gameWriteRepositoryMock,
        DeleteGameCommand command)
    {
        // Arrange
        var handler = new DeleteGameHandler(gameWriteRepositoryMock.Object);

        gameWriteRepositoryMock.Setup(repo => repo.GetGame(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Game?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Game with ID: {command.GameId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldDeleteGame(
        [Frozen] Mock<IGameWriteRepository> gameWriteRepositoryMock,
        DeleteGameCommand command,
        IFixture fixture)
    {
        // Arrange
        var game = fixture.Build<Game>()
            .Without(g => g.GameConsole)
            .Create();

        gameWriteRepositoryMock.Setup(repo => repo.GetGame(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);

        gameWriteRepositoryMock.Setup(repo => repo.DeleteGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGameHandler(gameWriteRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameWriteRepositoryMock.Verify(repo => repo.DeleteGame(game, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<DeleteGameCommandResponse>();
    }
}

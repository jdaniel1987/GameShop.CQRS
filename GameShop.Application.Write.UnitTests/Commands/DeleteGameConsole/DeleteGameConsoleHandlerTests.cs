using GameShop.Application.Write.Commands.DeleteGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Application.Write.UnitTests.Commands.DeleteGameConsole;

public sealed class DeleteGameConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameConsoleIsNotFound(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        DeleteGameConsoleCommand command)
    {
        // Arrange
        var handler = new DeleteGameConsoleHandler(gameConsoleWriteRepositoryMock.Object);

        gameConsoleWriteRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GameConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldDeleteGameConsole(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        DeleteGameConsoleCommand command,
        IFixture fixture)
    {
        // Arrange
        var gameConsole = fixture.Build<GameConsole>()
            .Without(g => g.Games)
            .Create();

        gameConsoleWriteRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        gameConsoleWriteRepositoryMock.Setup(repo => repo.DeleteGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGameConsoleHandler(gameConsoleWriteRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameConsoleWriteRepositoryMock.Verify(repo => repo.DeleteGameConsole(gameConsole, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<DeleteGameConsoleCommandResponse>();
    }
}

using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Commands.DeleteGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Application.UnitTests.Commands.DeleteGameConsole;

public sealed class DeleteGameConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameConsoleIsNotFound(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        DeleteGameConsoleCommand command)
    {
        // Arrange
        var handler = new DeleteGameConsoleHandler(gameConsoleRepositoryMock.Object);

        gameConsoleRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GameConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldDeleteGameConsole(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        DeleteGameConsoleCommand command,
        IFixture fixture)
    {
        // Arrange
        var gameConsole = fixture.Build<GameConsole>()
            .Without(g => g.Games)
            .Create();

        gameConsoleRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        gameConsoleRepositoryMock.Setup(repo => repo.DeleteGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGameConsoleHandler(gameConsoleRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameConsoleRepositoryMock.Verify(repo => repo.DeleteGameConsole(gameConsole, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<DeleteGameConsoleResponse>();
    }
}

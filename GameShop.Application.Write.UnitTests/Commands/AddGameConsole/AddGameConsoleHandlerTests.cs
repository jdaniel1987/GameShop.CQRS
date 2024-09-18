using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Application.Write.UnitTests.Commands.AddGameConsole;

public sealed class AddGameConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldAddGameConsoleSuccessfully(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        AddGameConsoleCommand command)
    {
        // Arrange
        var handler = new AddGameConsoleHandler(gameConsoleWriteRepositoryMock.Object);

        gameConsoleWriteRepositoryMock
            .Setup(repo => repo.AddGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameConsoleWriteRepositoryMock.Verify(repo => repo.AddGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGameConsoleCommandResponse>();
    }
}

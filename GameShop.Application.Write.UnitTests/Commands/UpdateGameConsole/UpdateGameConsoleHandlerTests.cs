using GameShop.Application.Write.Commands.UpdateGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;

namespace GameShop.Application.Write.UnitTests.Commands.UpdateGameConsole;

public sealed class UpdateGameConsoleHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldUpdateGameConsole_WhenGameConsoleIsFound(
        [Frozen] Mock<IGameConsoleWriteRepository> gameConsoleWriteRepositoryMock,
        UpdateGameConsoleCommand command,
        IFixture fixture)
    {
        // Arrange
        var handler = new UpdateGameConsoleHandler(gameConsoleWriteRepositoryMock.Object);

        var game = fixture.Build<Game>()
            .Without(g => g.GameConsole)
            .Create();

        var gameConsole = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .Create();

        gameConsoleWriteRepositoryMock.Setup(repo => repo.GetGameConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gameConsole);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameConsoleWriteRepositoryMock.Verify(repo => repo.UpdateGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<UpdateGameConsoleCommandResponse>();
    }
}

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GameShop.Application.Commands.AddGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using Moq;

namespace GameShop.Application.UnitTests.Commands.AddGameConsole;

public class AddGameConsoleHandlerTests
{
    private readonly IFixture _fixture;

    public AddGameConsoleHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldAddGameConsoleSuccessfully(
        [Frozen] Mock<IGameConsoleRepository> gameConsoleRepositoryMock,
        AddGameConsoleCommand command)
    {
        // Arrange
        var handler = new AddGameConsoleHandler(gameConsoleRepositoryMock.Object);

        gameConsoleRepositoryMock
            .Setup(repo => repo.AddGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameConsoleRepositoryMock.Verify(repo => repo.AddGameConsole(It.IsAny<GameConsole>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGameConsoleResponse>();
    }
}

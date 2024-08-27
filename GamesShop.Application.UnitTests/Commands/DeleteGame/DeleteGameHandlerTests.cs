using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.DeleteGame;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using MediatR;
using Moq;

namespace GamesShop.Application.UnitTests.Commands.DeleteGame;

public class DeleteGameHandlerTests
{
    private readonly IFixture _fixture;

    public DeleteGameHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGameIsNotFound(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        DeleteGameCommand command)
    {
        // Arrange
        var handler = new DeleteGameHandler(gameRepositoryMock.Object);

        gameRepositoryMock.Setup(repo => repo.GetGame(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Game?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Game with ID: {command.GameId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldDeleteGame(
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        DeleteGameCommand command,
        IFixture fixture)
    {
        // Arrange
        var game = fixture.Build<Game>()
            .Without(g => g.GamesConsole)
            .Create();

        gameRepositoryMock.Setup(repo => repo.GetGame(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);

        gameRepositoryMock.Setup(repo => repo.DeleteGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGameHandler(gameRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.DeleteGame(game, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<DeleteGameResponse>();
    }
}

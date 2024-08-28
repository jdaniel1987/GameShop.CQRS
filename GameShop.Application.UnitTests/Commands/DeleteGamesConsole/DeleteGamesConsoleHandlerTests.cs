using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GameShop.Application.Commands.DeleteGamesConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using MediatR;
using Moq;

namespace GameShop.Application.UnitTests.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleHandlerTests
{
    private readonly IFixture _fixture;

    public DeleteGamesConsoleHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGamesConsoleIsNotFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        DeleteGamesConsoleCommand command)
    {
        // Arrange
        var handler = new DeleteGamesConsoleHandler(gamesConsoleRepositoryMock.Object);

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GamesConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GamesConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldDeleteGamesConsole(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        DeleteGamesConsoleCommand command,
        IFixture fixture)
    {
        // Arrange
        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(g => g.Games)
            .Create();

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        gamesConsoleRepositoryMock.Setup(repo => repo.DeleteGamesConsole(It.IsAny<GamesConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteGamesConsoleHandler(gamesConsoleRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gamesConsoleRepositoryMock.Verify(repo => repo.DeleteGamesConsole(gamesConsole, It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<DeleteGamesConsoleResponse>();
    }
}

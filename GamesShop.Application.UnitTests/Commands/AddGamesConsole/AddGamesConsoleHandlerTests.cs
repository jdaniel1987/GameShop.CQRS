using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.AddGamesConsole;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using Moq;

namespace GamesShop.Application.UnitTests.Commands.AddGamesConsole;

public class AddGamesConsoleHandlerTests
{
    private readonly IFixture _fixture;

    public AddGamesConsoleHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldAddGamesConsoleSuccessfully(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        AddGamesConsoleCommand command)
    {
        // Arrange
        gamesConsoleRepositoryMock
            .Setup(repo => repo.AddGamesConsole(It.IsAny<GamesConsole>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var handler = new AddGamesConsoleHandler(gamesConsoleRepositoryMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gamesConsoleRepositoryMock.Verify(repo => repo.AddGamesConsole(It.IsAny<GamesConsole>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGamesConsoleResponse>();
    }
}

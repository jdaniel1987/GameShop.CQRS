using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.UpdateGamesConsole;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using Moq;

namespace GamesShop.Application.UnitTests.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleHandlerTests
{
    private readonly IFixture _fixture;

    public UpdateGamesConsoleHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldUpdateGamesConsole_WhenGamesConsoleIsFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        UpdateGamesConsoleCommand command,
        IFixture fixture)
    {
        // Arrange
        var handler = new UpdateGamesConsoleHandler(gamesConsoleRepositoryMock.Object);

        var game = fixture.Build<Game>()
            .Without(g => g.GamesConsole)
            .Create();

        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gamesConsoleRepositoryMock.Verify(repo => repo.UpdateGamesConsole(It.IsAny<GamesConsole>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<UpdateGamesConsoleResponse>();
    }
}

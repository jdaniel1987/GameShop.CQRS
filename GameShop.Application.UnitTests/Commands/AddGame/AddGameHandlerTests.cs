﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using GameShop.Application.Commands.AddGame;
using GameShop.Application.Events.GameCreated;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using MediatR;
using Moq;

namespace GameShop.Application.UnitTests.Commands.AddGame;

public class AddGameHandlerTests
{
    private readonly IFixture _fixture;

    public AddGameHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenGamesConsoleIsNotFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command)
    {
        // Arrange
        var handler = new AddGameHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object, publisherMock.Object);

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GamesConsole?)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be($"Games Console with ID: {command.GamesConsoleId} not found.");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldAddGameAndPublishEvent_WhenGamesConsoleIsFound(
        [Frozen] Mock<IGamesConsoleRepository> gamesConsoleRepositoryMock,
        [Frozen] Mock<IGameRepository> gameRepositoryMock,
        [Frozen] Mock<IPublisher> publisherMock,
        AddGameCommand command,
        IFixture fixture)
    {
        // Arrange
        var game = fixture.Build<Game>()
            .Without(g => g.GamesConsole)
            .Create();

        var gamesConsole = fixture.Build<GamesConsole>()
            .Without(gc => gc.Games)
            .Create();

        gamesConsoleRepositoryMock.Setup(repo => repo.GetGamesConsole(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gamesConsole);

        gameRepositoryMock.Setup(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        publisherMock.Setup(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new AddGameHandler(gameRepositoryMock.Object, gamesConsoleRepositoryMock.Object, publisherMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        gameRepositoryMock.Verify(repo => repo.AddGame(It.IsAny<Game>(), It.IsAny<CancellationToken>()), Times.Once);
        publisherMock.Verify(pub => pub.Publish(It.IsAny<GameCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<AddGameResponse>();
    }
}
﻿using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.AddGame;
using GamesShop.Application.Commands.UpdateGame;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Entities;
using GamesShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GamesShop.Application.UnitTests.Extensions;

public class GameExtensionsTests
{
    private readonly IFixture _fixture;

    public GameExtensionsTests()
    {
        _fixture = new Fixture();
    }

    public class AddGameCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGameCommandToGame(AddGameCommand command)
        {
            // Arrange

            // Act
            var game = command.ToDomain();

            // Assert
            game.Name.Should().Be(command.Name);
            game.Publisher.Should().Be(command.Publisher);
            game.Price.Value.Should().Be(command.Price);
            game.GamesConsoleId.Should().Be(command.GamesConsoleId);
        }
    }

    public class UpdateGameCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertUpdateGameCommandToGame(UpdateGameCommand command)
        {
            // Arrange
            
            // Act
            var game = command.ToDomain();

            // Assert
            game.Id.Should().Be(command.Id);
            game.Name.Should().Be(command.Name);
            game.Publisher.Should().Be(command.Publisher);
            game.Price.Value.Should().Be(command.Price);
            game.GamesConsoleId.Should().Be(command.GamesConsoleId);
        }
    }

    public class GameToEvent
    {
        [Theory, AutoData]
        public void ToEvent_ShouldConvertGameToGameCreatedEvent(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var gameEvent = game.ToEvent();

            // Assert
            gameEvent.GameName.Should().Be(game.Name);
            gameEvent.Publisher.Should().Be(game.Publisher);
            gameEvent.PriceUSD.Should().Be(game.Price.Value);
            gameEvent.PriceEUR.Should().Be(((PriceEuros)game.Price).Value);
            gameEvent.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }

    public class GamesToResponses
    {
        [Theory, AutoData]
        public void ToGetAllGamesResponse_ShouldConvertGamesToGetAllGamesResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var response = games.ToGetAllGamesResponse();

            // Assert
            response.Games.Should().HaveCount(games.Count());
            response.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
        }

        [Theory, AutoData]
        public void ToGetGamesByNameResponse_ShouldConvertGamesToGetGamesByNameResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var response = games.ToGetGamesByNameResponse();

            // Assert
            response.Games.Should().HaveCount(games.Count());
            response.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
        }

        [Theory, AutoData]
        public void ToGetGamesForConsoleResponse_ShouldConvertGamesToGetGamesForConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var response = games.ToGetGamesForConsoleResponse();

            // Assert
            response.Games.Should().HaveCount(games.Count());
            response.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
        }
    }

    public class GameToResponse
    {
        [Theory, AutoData]
        public void ToAddGameResponse_ShouldConvertGameToAddGameResponse(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var response = game.ToAddGameResponse();

            // Assert
            response.Id.Should().Be(game.Id);
            response.Name.Should().Be(game.Name);
        }

        [Theory, AutoData]
        public void ToUpdateGameResponse_ShouldConvertGameToUpdateGameResponse(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var response = game.ToUpdateGameResponse();

            // Assert
            response.Id.Should().Be(game.Id);
            response.Name.Should().Be(game.Name);
        }

        [Theory, AutoData]
        public void ToDeleteGameResponse_ShouldConvertGameToDeleteGameResponse(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GamesConsole, fixture.Build<GamesConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var response = game.ToDeleteGameResponse();

            // Assert
            response.Should().NotBeNull();
        }
    }
}
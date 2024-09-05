using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Commands.AddGame;
using GameShop.Application.Commands.UpdateGame;
using GameShop.Application.Extensions;
using GameShop.Application.Queries.GetAllGames;
using GameShop.Application.Queries.GetGamesByName;
using GameShop.Application.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GameShop.Application.UnitTests.Extensions;

public sealed class GameExtensionsTests
{
    public sealed class AddGameRequestToCommand
    {
        [Theory, AutoData]
        public void ToCommand_ShouldConvertAddGameRequestToAddGameCommand(AddGameRequest request)
        {
            // Act
            var command = request.ToCommand();

            // Assert
            command.Name.Should().Be(request.Name);
            command.Publisher.Should().Be(request.Publisher);
            command.GameConsoleId.Should().Be(request.GameConsoleId);
            command.Price.Should().Be(request.Price);
        }
    }

    public sealed class UpdateGameRequestToCommand
    {
        [Theory, AutoData]
        public void ToCommand_ShouldConvertUpdateGameRequestToUpdateGameCommand(UpdateGameRequest request)
        {
            // Act
            var command = request.ToCommand();

            // Assert
            command.Id.Should().Be(request.Id);
            command.Name.Should().Be(request.Name);
            command.Publisher.Should().Be(request.Publisher);
            command.GameConsoleId.Should().Be(request.GameConsoleId);
            command.Price.Should().Be(request.Price);
        }
    }

    public sealed class AddGameCommandToDomain
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
            game.GameConsoleId.Should().Be(command.GameConsoleId);
        }
    }

    public sealed class UpdateGameCommandToDomain
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
            game.GameConsoleId.Should().Be(command.GameConsoleId);
        }
    }

    public sealed class GameToEvent
    {
        [Theory, AutoData]
        public void ToEvent_ShouldConvertGameToGameCreatedEvent(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
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

    public sealed class GamesToGetAllGamesQueryResponse
    {
        [Theory, AutoData]
        public void ToGetAllGamesResponse_ShouldConvertGamesToGetAllGamesQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetAllGamesQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }

    public sealed class GamesToGetGamesByNameQueryResponse
    {
        [Theory, AutoData]
        public void ToGetGamesByNameQueryResponse_ShouldConvertGamesToGetGamesByNameQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetGamesByNameQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }

    public sealed class GamesToGetGamesForConsoleQueryResponse
    {
        [Theory, AutoData]
        public void ToGetGamesForConsoleQueryResponse_ShouldConvertGamesToGetGamesForConsoleQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var games = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = games.ToGetGamesForConsoleQueryResponse();

            // Assert
            queryResponse.Games.Should().HaveCount(games.Length);
            queryResponse.Games.Select(g => g.Id).Should().BeEquivalentTo(games.Select(g => g.Id));
            queryResponse.Games.Select(g => g.Name).Should().BeEquivalentTo(games.Select(g => g.Name));
            queryResponse.Games.Select(g => g.Publisher).Should().BeEquivalentTo(games.Select(g => g.Publisher));
            queryResponse.Games.Select(g => g.Price).Should().BeEquivalentTo(games.Select(g => g.Price.Value));
            queryResponse.Games.Select(g => g.GameConsoleId).Should().BeEquivalentTo(games.Select(g => g.GameConsoleId));
            queryResponse.Games.Select(g => g.GameConsoleName).Should().BeEquivalentTo(games.Select(g => g.GameConsole!.Name));
        }
    }

    public sealed class GameToAddGameCommandResponse
    {
        [Theory, AutoData]
        public void ToAddGameCommandResponse_ShouldConvertGameToAddGameCommandResponse(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var CommandResponse = game.ToAddGameCommandResponse();

            // Assert
            CommandResponse.Id.Should().Be(game.Id);
            CommandResponse.Name.Should().Be(game.Name);
        }
    }

    public sealed class GameToUpdateGameCommandResponse
    {
        [Theory, AutoData]
        public void ToUpdateGameCommandResponse_ShouldConvertGameToUpdateGameCommandResponse(
            IFixture fixture)
        {
            // Arrange
            var game = fixture.Build<Game>()
                .With(g => g.GameConsole, fixture.Build<GameConsole>()
                    .Without(gc => gc.Games)
                    .Create())
                .Create();

            // Act
            var commandResponse = game.ToUpdateGameCommandResponse();

            // Assert
            commandResponse.Id.Should().Be(game.Id);
            commandResponse.Name.Should().Be(game.Name);
        }
    }

    public sealed class GameToGetAllGamesResponse
    {
        [Theory, AutoData]
        public void ToGetAllGamesResponse_ShouldConvertGameToGetAllGamesResponse(
            GetAllGamesQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetAllGamesResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }

    public sealed class GameToGetGamesByNameResponse
    {
        [Theory, AutoData]
        public void ToGetGamesByNameResponse_ShouldConvertGameToGetGamesByNameResponse(
            GetGamesByNameQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetGamesByNameResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }

    public sealed class GameToGetGamesForConsoleResponse
    {
        [Theory, AutoData]
        public void ToGetGamesForConsoleResponse_ShouldConvertGameToGetGamesForConsoleResponse(
            GetGamesForConsoleQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetGamesForConsoleResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }
}

using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Commands.UpdateGame;
using GameShop.Application.Write.Extensions;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.UnitTests.Extensions;

public sealed class GameWriteExtensionsTests
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
}

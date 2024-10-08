using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Commands.UpdateGame;
using GameShop.Application.Write.Mappers;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.UnitTests.Mappers;

public sealed class GameWriteMappersTests
{
    public sealed class AddGameCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGameCommand(AddGameCommand command)
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
        public void ToDomain_ShouldConvertUpdateGameCommand(UpdateGameCommand command)
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
        public void ToEvent_ShouldConvertGame(
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
        public void ToAddGameCommandResponse_ShouldConvertGame(
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

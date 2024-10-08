using GameShop.Domain.Entities;
using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Application.Write.Commands.UpdateGameConsole;
using GameShop.Application.Write.Mappers;

namespace GameShop.Application.Write.UnitTests.Mappers;

public sealed class GameConsoleWriteMappersTests
{
    public sealed class AddGameConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGameConsoleCommand(
            AddGameConsoleCommand command)
        {
            // Arrange

            // Act
            var gameConsole = command.ToDomain();

            // Assert
            gameConsole.Name.Should().Be(command.Name);
            gameConsole.Manufacturer.Should().Be(command.Manufacturer);
            gameConsole.Price.Value.Should().Be(command.Price);
            gameConsole.Games.Should().BeEmpty();
        }
    }

    public sealed class UpdateGameConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertUpdateGameConsoleCommand(
            UpdateGameConsoleCommand command)
        {
            // Arrange

            // Act
            var gameConsole = command.ToDomain();

            // Assert
            gameConsole.Id.Should().Be(command.Id);
            gameConsole.Name.Should().Be(command.Name);
            gameConsole.Manufacturer.Should().Be(command.Manufacturer);
            gameConsole.Price.Value.Should().Be(command.Price);
            gameConsole.Games.Should().BeEmpty();
        }
    }

    public sealed class GameConsoleToAddGameConsoleCommandResponse
    {
        [Theory, AutoData]
        public void ToAddGameConsoleCommandResponse_ShouldConvertGameConsole(
            IFixture fixture)
        {
            // Arrange
            var gameConsole = fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create();

            // Act
            var commandResponse = gameConsole.ToAddGameConsoleCommandResponse();

            // Assert
            commandResponse.Id.Should().Be(gameConsole.Id);
            commandResponse.Name.Should().Be(gameConsole.Name);
        }
    }

    public sealed class GameConsoleToUpdateGameConsoleCommandResponse
    {
        [Theory, AutoData]
        public void ToUpdateGameConsoleCommandResponse_ShouldConvertUpdateGameConsoleCommand(
            IFixture fixture)
        {
            // Arrange
            var gameConsole = fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create();

            // Act
            var commandResponse = gameConsole.ToUpdateGameConsoleCommandResponse();

            // Assert
            commandResponse.Id.Should().Be(gameConsole.Id);
            commandResponse.Name.Should().Be(gameConsole.Name);
        }
    }
}

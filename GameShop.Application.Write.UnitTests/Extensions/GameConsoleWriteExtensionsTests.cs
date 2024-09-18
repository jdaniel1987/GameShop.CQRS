using GameShop.Application.Write.Extensions;
using GameShop.API.Write.Contracts.Requests;
using GameShop.Domain.Entities;
using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Application.Write.Commands.UpdateGameConsole;

namespace GameShop.Application.Write.UnitTests.Extensions;

public sealed class GameConsoleWriteExtensionsTests
{
    public sealed class AddGameConsoleRequestToCommand
    {
        [Theory, AutoData]
        public void ToCommand_ShouldConvertAddGameConsoleRequestToAddGameConsoleCommand(
            AddGameConsoleRequest request)
        {
            // Act
            var command = request.ToCommand();

            // Assert
            command.Name.Should().Be(request.Name);
            command.Manufacturer.Should().Be(request.Manufacturer);
            command.Price.Should().Be(request.Price);
        }
    }

    public sealed class UpdateGameConsoleRequestToCommand
    {
        [Theory, AutoData]
        public void ToCommand_ShouldConvertUpdateGameConsoleRequestToUpdateGameConsoleCommand(
            UpdateGameConsoleRequest request)
        {
            // Act
            var command = request.ToCommand();

            // Assert
            command.Id.Should().Be(request.Id);
            command.Name.Should().Be(request.Name);
            command.Manufacturer.Should().Be(request.Manufacturer);
            command.Price.Should().Be(request.Price);
        }
    }

    public sealed class AddGameConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGameConsoleCommandToGameConsole(
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
        public void ToDomain_ShouldConvertUpdateGameConsoleCommandToGameConsole(
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
        public void ToDomain_ShouldConvertAddGameConsoleCommandToGameConsole(
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
        public void ToDomain_ShouldConvertUpdateGameConsoleCommandToGameConsole(
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

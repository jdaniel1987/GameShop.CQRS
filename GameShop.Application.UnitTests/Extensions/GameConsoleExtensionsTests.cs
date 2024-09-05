using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Commands.AddGameConsole;
using GameShop.Application.Commands.UpdateGameConsole;
using GameShop.Application.Extensions;
using GameShop.Application.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.UnitTests.Extensions;

public sealed class GameConsoleExtensionsTests
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

    public sealed class GameConsolesToGetAllGameConsolesQueryResponse
    {
        [Theory, AutoData]
        public void ToGetAllGameConsolesQueryResponse_ShouldConvertGameConsolesToGetAllGameConsolesQueryResponse(
            IFixture fixture)
        {
            // Arrange
            var gameConsoles = fixture.Build<GameConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GameConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var queryResponse = gameConsoles.ToGetAllGameConsolesQueryResponse();

            // Assert
            queryResponse.GameConsoles.Should().HaveCount(gameConsoles.Length);
            queryResponse.GameConsoles.Select(g => g.Id).Should().BeEquivalentTo(gameConsoles.Select(g => g.Id));
            queryResponse.GameConsoles.Select(g => g.Name).Should().BeEquivalentTo(gameConsoles.Select(g => g.Name));
            queryResponse.GameConsoles.Select(g => g.Manufacturer).Should().BeEquivalentTo(gameConsoles.Select(g => g.Manufacturer));
            queryResponse.GameConsoles.Select(g => g.Price).Should().BeEquivalentTo(gameConsoles.Select(g => g.Price.Value));
            queryResponse.GameConsoles.Select(g => g.NumberOfGames).Should().BeEquivalentTo(gameConsoles.Select(g => g.Games.Count));
        }
    }

    public sealed class GetAllGameConsolesQueryResponseToGetAllGameConsolesResponse
    {
        [Theory, AutoData]
        public void ToGetAllGameConsolesResponse_ShouldConvertGetAllGameConsolesQueryResponseToGetAllGameConsolesResponse(
            GetAllGameConsolesQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetAllGameConsolesResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }
}

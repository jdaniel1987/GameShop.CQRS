using GameShop.Application.Commands.AddGameConsole;
using GameShop.Application.Commands.DeleteGameConsole;
using GameShop.Application.Commands.UpdateGameConsole;
using GameShop.Application.Extensions;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.UnitTests.Extensions;

public sealed class GameConsoleExtensionsTests
{
    public sealed class AddGameConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGameConsoleCommandToGameConsole(AddGameConsoleCommand command)
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
        public void ToDomain_ShouldConvertUpdateGameConsoleCommandToGameConsole(UpdateGameConsoleCommand command)
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

    public sealed class GameConsolesToResponses
    {
        [Theory, AutoData]
        public void ToGetAllGameConsolesResponse_ShouldConvertGameConsolesToGetAllGameConsolesResponse(
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
            var response = gameConsoles.ToGetAllGameConsolesResponse();

            // Assert
            response.GameConsoles.Should().HaveCount(gameConsoles.Length);
            response.GameConsoles.Select(gc => gc.Id).Should().BeEquivalentTo(gameConsoles.Select(gc => gc.Id));
            response.GameConsoles.Select(gc => gc.Name).Should().BeEquivalentTo(gameConsoles.Select(gc => gc.Name));
            response.GameConsoles.Select(gc => gc.Manufacturer).Should().BeEquivalentTo(gameConsoles.Select(gc => gc.Manufacturer));
            response.GameConsoles.Select(gc => gc.Price).Should().BeEquivalentTo(gameConsoles.Select(gc => gc.Price.Value));
            response.GameConsoles.Select(gc => gc.NumberOfGames).Should().BeEquivalentTo(gameConsoles.Select(gc => gc.Games.Count));
        }
    }

    public sealed class GameConsoleToResponse
    {
        [Theory, AutoData]
        public void ToAddGameConsoleResponse_ShouldConvertGameConsoleToAddGameConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gameConsole = fixture.Build<GameConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GameConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = gameConsole.ToAddGameConsoleResponse();

            // Assert
            response.Id.Should().Be(gameConsole.Id);
            response.Name.Should().Be(gameConsole.Name);
        }

        [Theory, AutoData]
        public void ToUpdateGameConsoleResponse_ShouldConvertGameConsoleToUpdateGameConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gameConsole = fixture.Build<GameConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GameConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = gameConsole.ToUpdateGameConsoleResponse();

            // Assert
            response.Id.Should().Be(gameConsole.Id);
            response.Name.Should().Be(gameConsole.Name);
        }

        [Theory, AutoData]
        public void ToDeleteGameConsoleResponse_ShouldConvertGameConsoleToDeleteGameConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gameConsole = fixture.Build<GameConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GameConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = new DeleteGameConsoleResponse();

            // Assert
            response.Should().NotBeNull();
        }
    }
}

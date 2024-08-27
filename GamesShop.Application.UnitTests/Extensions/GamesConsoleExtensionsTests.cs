using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using GamesShop.Application.Commands.AddGamesConsole;
using GamesShop.Application.Commands.UpdateGamesConsole;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Entities;
using System.Collections.Immutable;

namespace GamesShop.Application.UnitTests.Extensions;

public class GamesConsoleExtensionsTests
{
    private readonly IFixture _fixture;

    public GamesConsoleExtensionsTests()
    {
        _fixture = new Fixture();
    }

    public class AddGamesConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertAddGamesConsoleCommandToGamesConsole(AddGamesConsoleCommand command)
        {
            // Arrange

            // Act
            var gamesConsole = command.ToDomain();

            // Assert
            gamesConsole.Name.Should().Be(command.Name);
            gamesConsole.Manufacturer.Should().Be(command.Manufacturer);
            gamesConsole.Price.Value.Should().Be(command.Price);
            gamesConsole.Games.Should().BeEmpty();
        }
    }

    public class UpdateGamesConsoleCommandToDomain
    {
        [Theory, AutoData]
        public void ToDomain_ShouldConvertUpdateGamesConsoleCommandToGamesConsole(UpdateGamesConsoleCommand command)
        {
            // Arrange

            // Act
            var gamesConsole = command.ToDomain();

            // Assert
            gamesConsole.Id.Should().Be(command.Id);
            gamesConsole.Name.Should().Be(command.Name);
            gamesConsole.Manufacturer.Should().Be(command.Manufacturer);
            gamesConsole.Price.Value.Should().Be(command.Price);
            gamesConsole.Games.Should().BeEmpty();
        }
    }

    public class GamesConsolesToResponses
    {
        [Theory, AutoData]
        public void ToGetAllGamesConsolesResponse_ShouldConvertGamesConsolesToGetAllGamesConsolesResponse(
            IFixture fixture)
        {
            // Arrange
            var gamesConsoles = fixture.Build<GamesConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GamesConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .CreateMany()
                .ToImmutableArray();

            // Act
            var response = gamesConsoles.ToGetAllGamesConsolesResponse();

            // Assert
            response.GamesConsoles.Should().HaveCount(gamesConsoles.Count());
            response.GamesConsoles.Select(gc => gc.Id).Should().BeEquivalentTo(gamesConsoles.Select(gc => gc.Id));
            response.GamesConsoles.Select(gc => gc.Name).Should().BeEquivalentTo(gamesConsoles.Select(gc => gc.Name));
            response.GamesConsoles.Select(gc => gc.Manufacturer).Should().BeEquivalentTo(gamesConsoles.Select(gc => gc.Manufacturer));
            response.GamesConsoles.Select(gc => gc.Price).Should().BeEquivalentTo(gamesConsoles.Select(gc => gc.Price.Value));
            response.GamesConsoles.Select(gc => gc.NumberOfGames).Should().BeEquivalentTo(gamesConsoles.Select(gc => gc.Games.Count));
        }
    }

    public class GamesConsoleToResponse
    {
        [Theory, AutoData]
        public void ToAddGamesConsoleResponse_ShouldConvertGamesConsoleToAddGamesConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gamesConsole = fixture.Build<GamesConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GamesConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = gamesConsole.ToAddGamesConsoleResponse();

            // Assert
            response.Id.Should().Be(gamesConsole.Id);
            response.Name.Should().Be(gamesConsole.Name);
        }

        [Theory, AutoData]
        public void ToUpdateGamesConsoleResponse_ShouldConvertGamesConsoleToUpdateGamesConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gamesConsole = fixture.Build<GamesConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GamesConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = gamesConsole.ToUpdateGamesConsoleResponse();

            // Assert
            response.Id.Should().Be(gamesConsole.Id);
            response.Name.Should().Be(gamesConsole.Name);
        }

        [Theory, AutoData]
        public void ToDeleteGamesConsoleResponse_ShouldConvertGamesConsoleToDeleteGamesConsoleResponse(
            IFixture fixture)
        {
            // Arrange
            var gamesConsole = fixture.Build<GamesConsole>()
                .With(gc => gc.Games, fixture.Build<Game>()
                    .Without(g => g.GamesConsole)
                    .CreateMany()
                    .ToImmutableArray())
                .Create();

            // Act
            var response = gamesConsole.ToDeleteGamesConsoleResponse();

            // Assert
            response.Should().NotBeNull();
        }
    }
}

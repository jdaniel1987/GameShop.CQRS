using GameShop.API.Write.Contracts.Requests;
using GameShop.API.Write.Mappers;

namespace GameShop.API.Write.UnitTests.Mappers;

public sealed class GameConsoleWriteMappersTests
{
    public sealed class AddGameConsoleRequestToAddGameConsoleCommand
    {
        [Theory, AutoData]
        public void ToAddGameConsoleCommand_ShouldConvertAddGameConsoleRequest(
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

    public sealed class UpdateGameConsoleRequestTopdateGameConsoleCommand
    {
        [Theory, AutoData]
        public void TopdateGameConsoleCommand_ShouldConvertUpdateGameConsoleRequest(
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
}

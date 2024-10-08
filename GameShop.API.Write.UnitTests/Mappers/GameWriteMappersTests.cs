using GameShop.API.Write.Contracts.Requests;
using GameShop.API.Write.Mappers;

namespace GameShop.API.Write.UnitTests.Mappers;

public sealed class GameWriteMappersTests
{
    public sealed class AddGameRequestToAddGameCommand
    {
        [Theory, AutoData]
        public void ToAddGameCommand_ShouldConvertAddGameRequest(AddGameRequest request)
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

    public sealed class UpdateGameRequestToUpdateGameCommand
    {
        [Theory, AutoData]
        public void ToUpdateGameCommand_ShouldConvertUpdateGameRequest(UpdateGameRequest request)
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
}

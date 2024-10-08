using GameShop.Application.Read.Queries.GetAllGames;
using GameShop.API.Read.Mappers;
using GameShop.Application.Read.Queries.GetGamesByName;
using GameShop.Application.Read.Queries.GetGamesForConsole;

namespace GameShop.API.Read.UnitTests.Mappers;

public sealed class GameReadMappersTests
{
    public sealed class GetAllGamesQueryResponseToGetAllGamesResponse
    {
        [Theory, AutoData]
        public void ToGetAllGamesResponse_ShouldConvertGetAllGamesQueryResponse(
            GetAllGamesQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetAllGamesResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }

    public sealed class GetGamesByNameQueryResponseToGetGamesByNameResponse
    {
        [Theory, AutoData]
        public void ToGetGamesByNameResponse_ShouldConvertGetGamesByNameQueryResponse(
            GetGamesByNameQueryResponse queryResponse)
        {
            // Arrange

            // Act
            var response = queryResponse.ToGetGamesByNameResponse();

            // Assert
            response.Should().BeEquivalentTo(queryResponse);
        }
    }

    public sealed class GetGamesForConsoleQueryResponseToGetGamesForConsoleResponse
    {
        [Theory, AutoData]
        public void ToGetGamesForConsoleResponse_ShouldConvertGetGamesForConsoleQueryResponse(
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

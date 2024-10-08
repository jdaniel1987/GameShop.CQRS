using GameShop.Application.Read.Queries.GetAllGameConsoles;
using GameShop.API.Read.Mappers;

namespace GameShop.API.Read.UnitTests.Mappers;

public sealed class GameConsoleReadMappersTests
{
    public sealed class GetAllGameConsolesQueryResponseToGetAllGameConsolesResponse
    {
        [Theory, AutoData]
        public void ToGetAllGameConsolesResponse_ShouldConvertGetAllGameConsolesQueryResponse(
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

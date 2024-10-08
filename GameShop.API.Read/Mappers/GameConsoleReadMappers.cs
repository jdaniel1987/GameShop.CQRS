using GameShop.API.Read.Contracts.Responses;
using GameShop.Application.Read.Queries.GetAllGameConsoles;
using System.Collections.Immutable;

namespace GameShop.API.Read.Mappers;

public static class GameConsoleReadMappers
{
    public static GetAllGameConsolesResponse ToGetAllGameConsolesResponse(this GetAllGameConsolesQueryResponse response) =>
        new(
            response.GameConsoles.Select(gc =>
                new GetAllGameConsolesResponseItem(
                    gc.Id,
                    gc.Name,
                    gc.Manufacturer,
                    gc.Price,
                    gc.NumberOfGames))
            .ToImmutableArray());
}

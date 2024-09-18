using GameShop.API.Read.Contracts.Responses;
using GameShop.Application.Read.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.Read.Extensions;

public static class GameConsoleReadExtensions
{
    public static GetAllGameConsolesQueryResponse ToGetAllGameConsolesQueryResponse(this IReadOnlyCollection<GameConsole> gameConsoles) =>
        new(gameConsoles.Select(gc =>
            new GetAllGameConsolesQueryResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count))
            .ToImmutableArray());

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

using GameShop.Application.Read.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.Read.Mappers;

public static class GameConsoleDomainToApplicationMappers
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

}

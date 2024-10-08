using GameShop.Application.Read.Queries.GetAllGames;
using GameShop.Application.Read.Queries.GetGamesByName;
using GameShop.Application.Read.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.Read.Mappers;

public static class GameDomainToApplicationReadMappers
{
    public static GetAllGamesQueryResponse ToGetAllGamesQueryResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g =>
            new GetAllGamesQueryResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
            .ToImmutableArray());

    public static GetGamesByNameQueryResponse ToGetGamesByNameQueryResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g =>
            new GetGamesByNameQueryResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
            .ToImmutableArray());

    public static GetGamesForConsoleQueryResponse ToGetGamesForConsoleQueryResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g =>
            new GetGamesForConsoleQueryResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GameConsoleId,
                g.GameConsole!.Name))
            .ToImmutableArray());
}

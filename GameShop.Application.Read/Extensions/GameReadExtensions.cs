using GameShop.API.Read.Contracts.Responses;
using GameShop.Application.Read.Queries.GetAllGames;
using GameShop.Application.Read.Queries.GetGamesByName;
using GameShop.Application.Read.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using System.Collections.Immutable;

namespace GameShop.Application.Read.Extensions;

public static class GameReadExtensions
{
    public static GetAllGamesResponse ToGetAllGamesResponse(this GetAllGamesQueryResponse response) =>
        new(
            response.Games.Select(gc =>
                new GetAllGamesResponseItem(
                    gc.Id,
                    gc.Name,
                    gc.Publisher,
                    gc.Price,
                    gc.GameConsoleId,
                    gc.GameConsoleName))
            .ToImmutableArray());

    public static GetGamesByNameResponse ToGetGamesByNameResponse(this GetGamesByNameQueryResponse response) =>
        new(
            response.Games.Select(gc =>
                new GetGamesByNameResponseItem(
                    gc.Id,
                    gc.Name,
                    gc.Publisher,
                    gc.Price,
                    gc.GameConsoleId,
                    gc.GameConsoleName))
            .ToImmutableArray());

    public static GetGamesForConsoleResponse ToGetGamesForConsoleResponse(this GetGamesForConsoleQueryResponse response) =>
        new(
            response.Games.Select(gc =>
                new GetGamesForConsoleResponseItem(
                    gc.Id,
                    gc.Name,
                    gc.Publisher,
                    gc.Price,
                    gc.GameConsoleId,
                    gc.GameConsoleName))
            .ToImmutableArray());

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

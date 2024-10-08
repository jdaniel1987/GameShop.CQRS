using GameShop.API.Read.Contracts.Responses;
using GameShop.Application.Read.Queries.GetAllGames;
using GameShop.Application.Read.Queries.GetGamesByName;
using GameShop.Application.Read.Queries.GetGamesForConsole;
using System.Collections.Immutable;

namespace GameShop.API.Read.Mappers;

public static class GameReadMappers
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
}

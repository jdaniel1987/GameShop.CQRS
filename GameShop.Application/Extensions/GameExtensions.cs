using GameShop.API.Read.Contracts.Responses;
using GameShop.API.Write.Contracts.Requests;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Commands.AddGame;
using GameShop.Application.Commands.UpdateGame;
using GameShop.Application.Events.GameCreated;
using GameShop.Application.Queries.GetAllGames;
using GameShop.Application.Queries.GetGamesByName;
using GameShop.Application.Queries.GetGamesForConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GameShop.Application.Extensions;

public static class GameExtensions
{
    public static AddGameCommand ToCommand(this AddGameRequest game) =>
        new(
            game.Name,
            game.Publisher,
            game.GameConsoleId,
            game.Price);

    public static UpdateGameCommand ToCommand(this UpdateGameRequest game) =>
        new(
            game.Id,
            game.Name,
            game.Publisher,
            game.GameConsoleId,
            game.Price);

    public static Game ToDomain(this AddGameCommand command) =>
        new()
        {
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GameConsoleId = command.GameConsoleId
        };

    public static Game ToDomain(this UpdateGameCommand command) =>
        new()
        {
            Id = command.Id,
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GameConsoleId = command.GameConsoleId
        };

    public static GameCreatedEvent ToEvent(this Game game) =>
        new(
            GameName: game.Name,
            Publisher: game.Publisher,
            PriceUSD: game.Price.Value,
            PriceEUR: ((PriceEuros)game.Price).Value, // Conversion is automatic due to ValueObject operator
            CreationDate: DateTime.UtcNow);

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

    public static AddGameResponse ToAddGameCommandResponse(this Game game) =>
        new(
            game.Id, 
            game.Name);

    public static UpdateGameResponse ToUpdateGameCommandResponse(this Game game) =>
        new(
            game.Id,
            game.Name);

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

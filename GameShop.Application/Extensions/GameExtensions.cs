using GameShop.Application.Commands.AddGame;
using GameShop.Application.Commands.DeleteGame;
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
    public static Game ToDomain(this AddGameCommand command) =>
        new()
        {
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GamesConsoleId = command.GamesConsoleId
        };

    public static Game ToDomain(this UpdateGameCommand command) =>
        new()
        {
            Id = command.Id,
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GamesConsoleId = command.GamesConsoleId
        };

    public static GameCreatedEvent ToEvent(this Game game) =>
        new(
            GameName: game.Name,
            Publisher: game.Publisher,
            PriceUSD: game.Price.Value,
            PriceEUR: ((PriceEuros)game.Price).Value, // Conversion is automatic due to ValueObject operator
            CreationDate: DateTime.UtcNow);

    public static GetAllGamesResponse ToGetAllGamesResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g => 
            new GetAllGamesResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static GetGamesByNameResponse ToGetGamesByNameResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g =>
            new GetGamesByNameResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static GetGamesForConsoleResponse ToGetGamesForConsoleResponse(this IReadOnlyCollection<Game> games) =>
        new(games.Select(g =>
            new GetGamesForConsoleResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static AddGameResponse ToAddGameResponse(this Game game) =>
        new(
            game.Id, 
            game.Name);

    public static UpdateGameResponse ToUpdateGameResponse(this Game game) =>
        new(
            game.Id,
            game.Name);

    public static DeleteGameResponse ToDeleteGameResponse(this Game game) =>
        new();
}

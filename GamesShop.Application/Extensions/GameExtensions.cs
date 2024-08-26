using GamesShop.Application.Commands.AddGame;
using GamesShop.Application.Commands.DeleteGame;
using GamesShop.Application.Commands.UpdateGame;
using GamesShop.Application.Events.GameCreated;
using GamesShop.Application.Queries.GetAllGames;
using GamesShop.Application.Queries.GetGamesByName;
using GamesShop.Application.Queries.GetGamesForConsole;
using GamesShop.Domain.Entities;
using GamesShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GamesShop.Application.Extensions;

public static class GameExtensions
{
    public static Game ToDomain(this AddGameCommand command)
        => new()
        {
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GamesConsoleId = command.GamesConsoleId
        };

    public static Game ToDomain(this UpdateGameCommand command)
        => new()
        {
            Id = command.Id,
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GamesConsoleId = command.GamesConsoleId
        };

    public static GameCreatedEvent ToEvent(this Game game)
        => new(
            Game: game,
            CreationDate: DateTime.UtcNow);

    public static GetAllGamesResponse ToGetAllGamesResponse(this IReadOnlyCollection<Game> games)
        => new(games.Select(g => 
            new GetAllGamesResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static GetGamesByNameResponse ToGetGamesByNameResponse(this IReadOnlyCollection<Game> games)
        => new(games.Select(g =>
            new GetGamesByNameResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static GetGamesForConsoleResponse ToGetGamesForConsoleResponse(this IReadOnlyCollection<Game> games)
        => new(games.Select(g =>
            new GetGamesForConsoleResponseItem(
                g.Id,
                g.Name,
                g.Publisher,
                g.Price.Value,
                g.GamesConsoleId,
                g.GamesConsole!.Name))
            .ToImmutableArray());

    public static AddGameResponse ToAddGameResponse(this Game game)
        => new(
            game.Id, 
            game.Name);

    public static UpdateGameResponse ToUpdateGameResponse(this Game game)
        => new(
            game.Id,
            game.Name);

    public static DeleteGameResponse ToDeleteGameResponse(this Game game)
        => new();
}

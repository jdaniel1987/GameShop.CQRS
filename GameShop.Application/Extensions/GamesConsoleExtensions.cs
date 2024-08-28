using GameShop.Application.Commands.AddGamesConsole;
using GameShop.Application.Commands.DeleteGamesConsole;
using GameShop.Application.Commands.UpdateGamesConsole;
using GameShop.Application.Queries.GetAllGamesConsoles;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GameShop.Application.Extensions;

public static class GamesConsoleExtensions
{
    public static GamesConsole ToDomain(this AddGamesConsoleCommand command) =>
        new()
        {
            Id = 0,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GamesConsole ToDomain(this UpdateGamesConsoleCommand command) =>
        new()
        {
            Id = command.Id,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GetAllGamesConsolesResponse ToGetAllGamesConsolesResponse(this IReadOnlyCollection<GamesConsole> gamesConsoles) =>
        new(gamesConsoles.Select(gc =>
            new GetAllGamesConsolesResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count))
            .ToImmutableArray());

    public static AddGamesConsoleResponse ToAddGamesConsoleResponse(this GamesConsole gamesConsole) =>
        new(
            gamesConsole.Id,
            gamesConsole.Name);

    public static UpdateGamesConsoleResponse ToUpdateGamesConsoleResponse(this GamesConsole gamesConsole) =>
        new(
            gamesConsole.Id,
            gamesConsole.Name);

    public static DeleteGamesConsoleResponse ToDeleteGamesConsoleResponse(this GamesConsole gamesConsole) =>
        new();
}

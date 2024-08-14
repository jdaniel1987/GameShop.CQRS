using GamesShop.Application.Commands.AddGamesConsole;
using GamesShop.Application.Commands.UpdateGamesConsole;
using GamesShop.Application.Queries.GetAllGamesConsoles;
using GamesShop.Domain.Entities;
using GamesShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GamesShop.Application.Extensions;

public static class GamesConsoleExtensions
{
    public static GamesConsole ToDomain(this AddGamesConsoleCommand command)
        => new()
        {
            Id = 0,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GamesConsole ToDomain(this UpdateGamesConsoleCommand command)
        => new()
        {
            Id = command.Id,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GetAllGamesConsolesResponse ToGetAllGamesConsolesResponse(this IReadOnlyCollection<GamesConsole> gamesConsoles)
        => new(gamesConsoles.Select(gc =>
            new GetAllGamesConsolesResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count))
            .ToImmutableArray());
}

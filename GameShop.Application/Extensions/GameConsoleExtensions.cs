using GameShop.Application.Commands.AddGameConsole;
using GameShop.Application.Commands.DeleteGameConsole;
using GameShop.Application.Commands.UpdateGameConsole;
using GameShop.Application.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GameShop.Application.Extensions;

public static class GameConsoleExtensions
{
    public static GameConsole ToDomain(this AddGameConsoleCommand command) =>
        new()
        {
            Id = 0,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GameConsole ToDomain(this UpdateGameConsoleCommand command) =>
        new()
        {
            Id = command.Id,
            Name = command.Name,
            Manufacturer = command.Manufacturer,
            Price = Price.Create(command.Price),
            Games = []
        };

    public static GetAllGameConsolesResponse ToGetAllGameConsolesResponse(this IReadOnlyCollection<GameConsole> gameConsoles) =>
        new(gameConsoles.Select(gc =>
            new GetAllGameConsolesResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count))
            .ToImmutableArray());

    public static AddGameConsoleResponse ToAddGameConsoleResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);

    public static UpdateGameConsoleResponse ToUpdateGameConsoleResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);

    public static DeleteGameConsoleResponse ToDeleteGameConsoleResponse(this GameConsole gameConsole) =>
        new();
}

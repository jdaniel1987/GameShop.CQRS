using GameShop.API.Read.Contracts.Responses;
using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Commands.AddGameConsole;
using GameShop.Application.Commands.UpdateGameConsole;
using GameShop.Application.Queries.GetAllGameConsoles;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;
using System.Collections.Immutable;

namespace GameShop.Application.Extensions;

public static class GameConsoleExtensions
{
    public static AddGameConsoleCommand ToCommand(this AddGameConsoleRequest gameConsole) =>
        new(
            gameConsole.Name,
            gameConsole.Manufacturer,
            gameConsole.Price);

    public static UpdateGameConsoleCommand ToCommand(this UpdateGameConsoleRequest gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name,
            gameConsole.Manufacturer,
            gameConsole.Price);

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

    public static AddGameConsoleCommandResponse ToAddGameConsoleCommandResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);

    public static UpdateGameConsoleCommandResponse ToUpdateGameConsoleCommandResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);

    public static GetAllGameConsolesQueryResponse ToGetAllGameConsolesQueryResponse(this IReadOnlyCollection<GameConsole> gameConsoles) =>
        new(gameConsoles.Select(gc =>
            new GetAllGameConsolesQueryResponseItem(
                gc.Id,
                gc.Name,
                gc.Manufacturer,
                gc.Price.Value,
                gc.Games.Count))
            .ToImmutableArray());

    public static GetAllGameConsolesResponse ToGetAllGameConsolesResponse(this GetAllGameConsolesQueryResponse response) =>
        new(
            response.GameConsoles.Select(gc =>
                new GetAllGameConsolesResponseItem(
                    gc.Id,
                    gc.Name,
                    gc.Manufacturer,
                    gc.Price,
                    gc.NumberOfGames))
            .ToImmutableArray());
}

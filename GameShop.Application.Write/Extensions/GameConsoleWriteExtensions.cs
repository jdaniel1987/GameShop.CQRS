using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Application.Write.Commands.UpdateGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.Extensions;

public static class GameConsoleWriteExtensions
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
}

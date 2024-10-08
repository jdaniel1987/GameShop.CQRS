using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Application.Write.Commands.UpdateGameConsole;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.Mappers;

public static class GameConsoleWriteMappers
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

    public static AddGameConsoleCommandResponse ToAddGameConsoleCommandResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);

    public static UpdateGameConsoleCommandResponse ToUpdateGameConsoleCommandResponse(this GameConsole gameConsole) =>
        new(
            gameConsole.Id,
            gameConsole.Name);
}

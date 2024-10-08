using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Write.Commands.AddGameConsole;
using GameShop.Application.Write.Commands.UpdateGameConsole;

namespace GameShop.API.Write.Mappers;

public static class GameConsoleWriteMappers
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
}
